using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_ZhengBuTask : System.Web.UI.Page
    {
        public string CM_Context;
        string id = string.Empty;//全局变量id
        string chId = string.Empty;
        string action = string.Empty;
        List<string> str = new List<string>();
        Table t = new Table();
        Dictionary<string, string> reviewer = new Dictionary<string, string>();//用于存储审核的名单
        int rowsum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
                id = Request.QueryString["id"].ToString();//识别号
            if (Request.QueryString["chId"] != null)
                chId = Request.QueryString["chid"].ToString();//变更识别号
            if (Request.QueryString["action"] != null)
                action = Request.QueryString["action"].ToString();//操作
            getLeaderInfo();
            Get_Shr();
            if (!IsPostBack)
            {
                Hidden.Value = DateTime.Now.ToString("yyyyMMddHHmmss");
                UserID.Value = Session["UserId"].ToString();
                ShowData();
                InitVar();
                BindSelectReviewer();
                rblShdj_Changed(null, null);
            }
        }

        private void ShowData()
        {
            string str1 = "select a.*,b.ST_NAME from TBCM_PLAN as a left join TBDS_STAFFINFO as b on b.ST_ID=a.CM_MANCLERK where ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(str1);
            if (dt.Rows.Count > 0)
            {
                Assign(Panel, dt);
                Assign(Panel1, dt);
                CM_NOTE.Text = dt.Rows[0]["CM_NOTE"].ToString();
            }
            else
            {
                LbtnYes.Visible = false;
                Response.Write("<script>alert('项目被删除，请查看原项目！')</script>");
                return;
            }
            if (chId != "")
            {
                str1 = "select * from TBCM_CHANGE where CH_ID='" + chId + "'";
                GridView1.DataSource = DBCallCommon.GetDTUsingSqlText(str1);
                GridView1.DataBind();
            }

            if (action != "add")
            {
                CM_Context = chId;
                Hidden.Value = chId;
            }
        }

        private void Assign(Panel panel, DataTable dt)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is Label)
                {
                    if (dt.Columns.Contains(((Label)control).ID))
                    {
                        ((Label)control).Text = dt.Rows[0][control.ID].ToString();
                    }
                }
            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            int err = CheckLev();
            if (CheckPs(true, new List<string>() { "cki0", "cki1", "cki2", "cki3", "cki4", "cki5" }))
            {
                Response.Write("<script>alert('请选择抄送人！');</script>"); return;//后台验证
            }
            else if (err != 0)
            {
                switch (err)
                {
                    case 1:
                        Response.Write("<script>alert('请选择评审人！');</script>"); return;//后台验证
                    case 2:
                        Response.Write("<script>alert('您选择的是两级审批,请选择第二个评审人！');</script>"); return;//后台验证
                    case 3:
                        Response.Write("<script>alert('您选择的是三级审批,请选择第二个评审人！');</script>"); return;//后台验证
                    case 4:
                        Response.Write("<script>alert('您选择的是三级审批,请选择第三个评审人！');</script>"); return;//后台验证
                    default:
                        Response.Write("<script>alert('请选择评审人！');</script>"); return;//后台验证
                }
            }
            else
            {
                List<string> list = new List<string>();
                if (GridView1.Rows.Count == 0)
                {
                    Response.Write("<script>alert('请添加任务号！');</script>"); return;//后台验证
                }
                else
                {
                    string sql = "";
                    if (action == "edit")
                    {
                        sql = "delete from TBCM_CHANLIST where CH_ID='" + chId + "' and CM_TYPE='1'";
                        list.Add(sql);
                        sql = "delete from TBCM_CHANGE where CH_ID='" + chId + "'";
                        list.Add(sql);
                        sql = "delete from TBCM_PSVIEW where CM_ID='" + chId + "'";
                        list.Add(sql);
                    }

                    #region 存入TBCM_CHANLIST

                    List<string> yj = new List<string>() { "", "", "" };
                    switch (rblShdj.SelectedValue)
                    {
                        case "1":
                            yj[0] = "1";
                            break;
                        case "2":
                            yj[0] = "1";
                            yj[1] = "1";
                            break;
                        case "3":
                            yj[0] = "1";
                            yj[1] = "1";
                            yj[2] = "1";
                            break;
                        default:
                            break;
                    }
                    sql = string.Format("insert into TBCM_CHANLIST(CH_ID,ID,CM_MANCLERK,CM_ZDTIME,CM_STATE,CM_PS1,CM_PS2,CM_PS3,CM_PSYJ1,CM_PSYJ2,CM_PSYJ3,CM_PSJB,CM_TYPE) values('{0}','{1}','{2}','{3}','1','{4}','{5}','{6}','{7}','{8}','{9}','{10}','1')", Hidden.Value, id, UserID.Value, DateTime.Now.ToString("yyyy-MM-dd"), firstid.Value, secondid.Value, thirdid.Value, yj[0], yj[1], yj[2], rblShdj.SelectedValue);
                    list.Add(sql);

                    #endregion

                    #region 存入TBCM_CHANGE

                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        GridViewRow gr = GridView1.Rows[i];
                        string tsa_id = ((ITextControl)gr.FindControl("TSA_ID")).Text.Trim();
                        if (tsa_id == "")
                        {
                            tsa_id = ((ITextControl)GridView1.Rows[i - 1].FindControl("TSA_ID")).Text.Trim();
                            ((ITextControl)gr.FindControl("TSA_ID")).Text = tsa_id;
                            ((DropDownList)gr.FindControl("TSA_TYPE")).SelectedValue = ((DropDownList)GridView1.Rows[i - 1].FindControl("TSA_TYPE")).SelectedValue;
                        }
                        sql = "select * from TBCM_BASIC where ID='" + id + "' and TSA_ID='" + tsa_id + "' and TSA_TYPE='" + ((DropDownList)gr.FindControl("TSA_TYPE")).SelectedValue + "'";
                        if (DBCallCommon.GetDTUsingSqlText(sql).Rows.Count != 0)
                        {
                            list.Add(string.Format("insert into TBCM_CHANGE(CH_ID,ID,TSA_ID,TSA_ENGNAME,TSA_MAP,TSA_NUMBER,TSA_UNIT,TSA_MATERIAL,TSA_IDNOTE,TSA_TYPE,CM_CHANITEM) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','2')", Hidden.Value, id, tsa_id, ((ITextControl)gr.FindControl("TSA_ENGNAME")).Text.Trim().Replace(" ", ""), ((ITextControl)gr.FindControl("TSA_MAP")).Text.Trim(), ((ITextControl)gr.FindControl("TSA_NUMBER")).Text.Trim(), ((ITextControl)gr.FindControl("TSA_UNIT")).Text.Trim(), ((ITextControl)gr.FindControl("TSA_MATERIAL")).Text.Trim(), ((ITextControl)gr.FindControl("TSA_IDNOTE")).Text.Trim(), ((DropDownList)gr.FindControl("TSA_TYPE")).SelectedValue));
                        }
                        else
                        {
                            Response.Write("<script>alert('该项目不存在此任务号或设备类型不一致！');</script>");//后台验证
                            return;
                        }
                    }

                    #endregion

                    #region 抄送人

                    bindReviewer();
                    sql = "insert into TBCM_PSVIEW values('" + Hidden.Value + "','" + UserID.Value + "','2','" + txt_zdrYJ.Text.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + Session["UserDeptID"].ToString() + "','0')";
                    list.Add(sql);  //制单人意见
                    for (int i = 0; i < reviewer.Count; i++)
                    {
                        /******************通过键来找值******************************************************/
                        /**为了兼容领导同时为部门负责人的情况，评审人部门编号要以评审人员设置表中为准，而不以当前登录人部门编号为依据**/
                        string sql_dep = "select dep_id from TBCM_HT_SETTING where per_id='" + reviewer.Values.ElementAt(i) + "' and per_sfjy=0 and per_type='2'";
                        DataTable dt_dep = DBCallCommon.GetDTUsingSqlText(sql_dep);
                        if (dt_dep.Rows.Count > 0)
                        {
                            sql = "insert into TBCM_PSVIEW(CM_ID,CM_PID,CM_DEP,CM_PIDTYPE) values('" + Hidden.Value + "','" + reviewer.Values.ElementAt(i) + "','" + dt_dep.Rows[0]["dep_id"].ToString().Substring(0, 2) + "','1')";
                            list.Add(sql);//其他人
                        }
                    }

                    #endregion

                    // 发送邮件
                    string sql1 = "select ID,TSA_ID,TSA_ENGNAME,MTA_DUY,TSA_TCCLERKNM,QSA_QCCLERKNM,MTA_PJID from TBCM_BASIC as a left join View_TBMP_TASKASSIGN as b on a.TSA_ID=b.MTA_ID where ID=" + id;
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
                    if (dt.Rows.Count > 0)
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(GetUesrID(dt.Rows[0]["MTA_DUY"].ToString())), new List<string>(), new List<string>(), "经营计划单变更", "合同号"+ dt.Rows[0]["MTA_PJID"].ToString() +"任务号“" + dt.Rows[0]["TSA_ID"].ToString() + "“产品名称为" + dt.Rows[0]["TSA_ENGNAME"].ToString() + "的经营计划单已经变更，请您上系统查看");
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(GetUesrID(dt.Rows[0]["TSA_TCCLERKNM"].ToString())), new List<string>(), new List<string>(), "经营计划单变更", "合同号" + dt.Rows[0]["MTA_PJID"].ToString() + "任务号“" + dt.Rows[0]["TSA_ID"].ToString() + "“产品名称为" + dt.Rows[0]["TSA_ENGNAME"].ToString() + "的经营计划单已经变更，请您上系统查看");
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(GetUesrID(dt.Rows[0]["QSA_QCCLERKNM"].ToString())), new List<string>(), new List<string>(), "经营计划单变更", "合同号" + dt.Rows[0]["MTA_PJID"].ToString() + "任务号“" + dt.Rows[0]["TSA_ID"].ToString() + "“产品名称为" + dt.Rows[0]["TSA_ENGNAME"].ToString() + "的经营计划单已经变更，请您上系统查看");
                    }
                    DBCallCommon.ExecuteTrans(list);//执行事务
                    if (action == "eidt")
                    {
                        Response.Redirect("CM_ZengBuPs.aspx");
                    }
                    else
                    {
                        Response.Redirect("CM_TaskEdit.aspx");
                    }
                }
            }
        }

        private string GetUesrID(string username)
        {
            string userid = "";
            string sql = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_NAME = '" + username + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                userid = dt.Rows[0]["ST_ID"].ToString();
            }
            return userid;
        }

        protected int CheckLev()
        {
            int b = 0;
            if (firstid.Value == "")
            {
                b = 1;
            }
            else
            {
                switch (rblShdj.SelectedValue)
                {
                    case "2":
                        if (secondid.Value == "")
                        {
                            b = 2;
                        }
                        break;
                    case "3":
                        if (secondid.Value == "")
                        {
                            b = 3;
                        }
                        else if (thirdid.Value == "" && secondid.Value != "")
                        {
                            b = 4;
                        }
                        break;
                    default:
                        break;
                }
            }
            return b;
        }

        protected bool CheckPs(bool n, List<string> id)
        {
            for (int i = 0; i < id.Count; i++)
            {
                CheckBoxList cbl = (CheckBoxList)Panel2.FindControl(id[i]);
                if (cbl != null)
                {
                    for (int j = 0; j < cbl.Items.Count; j++)
                    {
                        if (cbl.Items[j].Selected)
                        {
                            n = false;
                        }
                    }
                }
            }
            return n;
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            if (action != "edit")
            {
                Response.Redirect("CM_TaskEdit.aspx");
            }
            else
            {
                Response.Redirect("CM_ZengBuPs.aspx");
            }
        }

        #region 增加行
        public string addstr;
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (TSA_ID.Text.Trim() != "" && CM_CONTR.Text != "")
            {
                if (Regex.IsMatch(TSA_ID.Text.Trim(), @"^\d{5}([A-Za-z0-9-])+"))
                {
                    if (Regex.IsMatch(TSA_ID.Text.Trim(), @"[\u2E80-\u9FFF]+"))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), DateTime.Now.ToString("ssff"), "tiX('1');", true);//后台验证
                    }
                    else
                    {
                        string sql = "select * from TBCM_BASIC where ID='" + id + "' and TSA_ID='" + TSA_ID.Text.Trim() + "'";
                        if (DBCallCommon.GetDTUsingSqlText(sql).Rows.Count != 0)
                        {
                            int count = GridView1.Rows.Count;
                            AddStr();
                            CreateNewRow(Convert.ToInt32(num.Value), count);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), DateTime.Now.ToString("ssff"), "tiX('2');", true);//后台验证
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), DateTime.Now.ToString("ssff"), "tiX('3');", true);//后台验证
                }
            }
        }

        private void AddStr()
        {
            if (GridView1.Rows.Count > 0)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    string tsa_id = ((ITextControl)gr.FindControl("TSA_ID")).Text;
                    if (tsa_id != "")
                    {
                        str.Add(tsa_id);
                        str = str.Distinct<string>().ToList();
                    }
                }
            }
        }

        private void CreateNewRow(int num, int count) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable(num);
            if (str.IndexOf(TSA_ID.Text.Trim()) + 1 == str.Count || !str.Contains(TSA_ID.Text.Trim()))
            {
                for (int i = count; i < num + count; i++)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = "";
                    newRow[3] = 1;
                    dt.Rows.Add(newRow);
                    if (!str.Contains(TSA_ID.Text.Trim()))
                    {
                        dt.Rows[count][0] = TSA_ID.Text.Trim();
                    }
                }
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
            InitVar();
        }

        private DataTable GetDataTable(int num)
        {
            DataTable dt = GetTable();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                DataRow newRow = dt.NewRow();
                string tsa_id = ((ITextControl)gr.FindControl("TSA_ID")).Text;
                newRow[0] = "";
                if (tsa_id != "")
                {
                    newRow[0] = tsa_id;
                }
                newRow[1] = ((ITextControl)gr.FindControl("TSA_ENGNAME")).Text;
                newRow[2] = ((ITextControl)gr.FindControl("TSA_MAP")).Text;
                newRow[3] = ((ITextControl)gr.FindControl("TSA_NUMBER")).Text;
                newRow[4] = ((ITextControl)gr.FindControl("TSA_UNIT")).Text;
                newRow[5] = ((ITextControl)gr.FindControl("TSA_MATERIAL")).Text;
                newRow[6] = ((ITextControl)gr.FindControl("TSA_IDNOTE")).Text;
                newRow[7] = ((DropDownList)gr.FindControl("TSA_TYPE")).SelectedItem.Value;
                dt.Rows.Add(newRow);
                if (str.IndexOf(TSA_ID.Text.Trim()) + 1 < str.Count && i + 1 < GridView1.Rows.Count)
                {
                    if (((ITextControl)GridView1.Rows[i + 1].FindControl("TSA_ID")).Text == str[str.IndexOf(TSA_ID.Text.Trim()) + 1].ToString())
                    {
                        for (int j = 0; j < num; j++)
                        {
                            DataRow newRow1 = dt.NewRow();
                            newRow1[3] = 1;
                            dt.Rows.Add(newRow1);
                        }
                    }
                }
            }
            dt.AcceptChanges();
            return dt;
        }

        private DataTable GetTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TSA_ID");
            dt.Columns.Add("TSA_ENGNAME");
            dt.Columns.Add("TSA_MAP");
            dt.Columns.Add("TSA_NUMBER");
            dt.Columns.Add("TSA_UNIT");
            dt.Columns.Add("TSA_MATERIAL");
            dt.Columns.Add("TSA_IDNOTE");
            dt.Columns.Add("TSA_TYPE");
            dt.Columns.Add("CM_ID");
            return dt;
        }

        private void InitVar()
        {
            if (GridView1.Rows.Count == 0)
            {
                NoDataPanel.Visible = true;
                delete.Visible = false;
            }
            else
            {
                NoDataPanel.Visible = false;
                delete.Visible = true;
            }
        }
        #endregion

        protected void delete_Click(object sender, EventArgs e)
        {
            DataTable dt = GetTable();
            AddStr();
            str.Add("");//储存任务号
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gr.FindControl("chk");
                string tsa_id = ((ITextControl)gr.FindControl("TSA_ID")).Text;
                if (!chk.Checked || tsa_id != "")//删除勾选且不为任务号节点的项
                {
                    if (tsa_id != "" && chk.Checked)//勾选为任务号节点
                    {
                        str.Remove(tsa_id);
                        str.Remove("");
                    }
                    if (str.Contains(tsa_id))
                    {
                        DataRow newRow = dt.NewRow();
                        newRow[0] = "";
                        if (tsa_id != "")
                        {
                            newRow[0] = tsa_id;
                        }
                        newRow[1] = ((ITextControl)gr.FindControl("TSA_ENGNAME")).Text;
                        newRow[2] = ((ITextControl)gr.FindControl("TSA_MAP")).Text;
                        newRow[3] = ((ITextControl)gr.FindControl("TSA_NUMBER")).Text;
                        newRow[4] = ((ITextControl)gr.FindControl("TSA_UNIT")).Text;
                        newRow[5] = ((ITextControl)gr.FindControl("TSA_MATERIAL")).Text;
                        newRow[6] = ((ITextControl)gr.FindControl("TSA_IDNOTE")).Text;
                        newRow[7] = ((DropDownList)gr.FindControl("TSA_TYPE")).SelectedItem.Value;
                        dt.Rows.Add(newRow);
                        str.Add("");
                        str = str.Distinct<string>().ToList();
                    }
                }
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
            InitVar();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox tb = (TextBox)e.Row.FindControl("TSA_ID");
                if (tb.Text.Trim() != "")
                {
                    tb.ReadOnly = false;
                }
                else
                {
                    tb.ReadOnly = true;
                    tb.Enabled = false;
                }
                DropDownList ddl = (DropDownList)e.Row.FindControl("TSA_TYPE");
                HiddenField hd = (HiddenField)e.Row.FindControl("Hid_Type");
                BindType(ddl);
                ddl.SelectedValue = hd.Value;
                if (tb.Text.Trim() == "")
                {
                    ddl.Visible = false;
                }
            }
        }

        private void BindType(DropDownList ddl)
        {
            string sql = "select CM_TYPE,CM_REFER from TBCM_TYPE";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddl.DataSource = dt;
            ddl.DataValueField = "CM_TYPE";
            ddl.DataTextField = "CM_REFER";
            ddl.DataBind();
        }

        #region 得到领导信息

        protected void getLeaderInfo()
        {
            /******绑定人员信息*****/
            getStaffInfo("07", "市场部", 4);
            getStaffInfo("03", "技术质量部", 0);
            getStaffInfo("05", "采购部", 1);
            getStaffInfo("04", "生产管理部", 2);
            getStaffInfo("06", "财务部", 3);
            getStaffInfo("01", "公司领导", 5);
            //得到领导信息，根据金额
            Panel2.Controls.Add(t);
        }

        protected void getStaffInfo(string st_id, string DEP_NAME, int i)
        {
            string sql = string.Format("select ST_NAME,ST_ID,ST_DEPID from TBDS_STAFFINFO as a inner join TBCM_HT_SETTING as b on a.ST_ID=b.per_id where a.ST_PD='0'and b.dep_id='{0}' and b.per_sfjy='0' and b.per_type='2'", st_id);
            bindInfo(sql, i, DEP_NAME, st_id);
        }
        /**********************动态的绑定评审人员的信息*************************************/
        private void bindInfo(string sql, int i, string DEP_NAME, string st_id)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count != 0)
            {
                TableRow tr = new TableRow();
                TableCell td = new TableCell();
                td.Width = 100;
                TableCell td1 = new TableCell();//第一列为部门名称
                if (i == 4)
                {
                    td.Text = "抄送至:";
                }
                td1.Width = 85;
                Label lab = new Label();
                lab.Text = DEP_NAME + ":";
                Label lab1 = new Label();
                lab1.ID = "dep" + i.ToString();
                lab1.Text = st_id;
                lab1.Visible = false;
                td1.Controls.Add(lab);
                td1.Controls.Add(lab1);
                tr.Cells.Add(td);
                tr.Cells.Add(td1);

                CheckBoxList cki = new CheckBoxList();//第二列为领导的姓名
                cki.ID = "cki" + i.ToString();
                cki.DataSource = dt;
                cki.DataTextField = "ST_NAME";//领导的姓名
                cki.DataValueField = "ST_ID";//部门的编号
                cki.DataBind();
                if (i == 4)
                {
                    for (int k = 0; k < cki.Items.Count; k++)
                    {
                        //cki.Items[k].Attributes.Add("Onclick", "CheckBoxList_Click(this)");//使用了javascript使其只能勾选一个
                    }
                }
                cki.RepeatColumns = 5;//获取显示的列数
                TableCell td2 = new TableCell();
                td2.Controls.Add(cki);
                tr.Cells.Add(td2);
                t.Controls.Add(tr);
                rowsum++;
            }
        }

        #endregion

        private void bindReviewer()
        {
            List<string> list = new List<string>();
            foreach (Control item in Panel2.Controls)
            {
                list.Add(item.ID);
            }
            int count = 0;
            for (int i = 0; i < 6; i++)
            {
                CheckBoxList ck = (CheckBoxList)Panel2.FindControl("cki" + i.ToString());
                Label lb = (Label)Panel2.FindControl("dep" + i.ToString());
                if (ck != null)
                {
                    for (int j = 0; j < ck.Items.Count; j++)
                    {
                        if (ck.Items[j].Selected == true)
                        {
                            reviewer.Add(lb.Text + ck.Items[j].Value.ToString(), ck.Items[j].Value.ToString());//字典，绑定部门领导的编号
                            count++;
                        }
                    }
                }
            }
        }

        //对绑定已经勾选的评审人
        private void BindSelectReviewer()
        {
            string myid = chId;
            string check_select = "select CM_PID from TBCM_PSVIEW where CM_ID='" + myid + "' and CM_PIDTYPE!='0'";
            DataTable sele = DBCallCommon.GetDTUsingSqlText(check_select);
            for (int i = 0; i < 6; i++)
                if (i != 1)
                {
                    {
                        CheckBoxList ck = (CheckBoxList)Panel2.FindControl("cki" + i.ToString());
                        for (int j = 0; j < sele.Rows.Count; j++)
                        {
                            for (int k = 0; k < ck.Items.Count; k++)
                            {
                                if (ck.Items[k].Value == sele.Rows[j][0].ToString())
                                {
                                    ck.Items[k].Selected = true;
                                }
                            }
                        }
                    }
                }
        }

        protected void rblShdj_Changed(object sender, EventArgs e)
        {
            switch (rblShdj.SelectedIndex)
            {
                case 0:
                    tb1.Visible = true;
                    tb2.Visible = false;
                    tb3.Visible = false;

                    txt_second.Text = "";
                    secondid.Value = "";

                    txt_third.Text = "";
                    thirdid.Value = "";
                    break;
                case 1:
                    tb1.Visible = true;
                    tb2.Visible = true;
                    tb3.Visible = false;

                    txt_third.Text = "";
                    thirdid.Value = "";
                    break;
                case 2:
                    tb1.Visible = true;
                    tb2.Visible = true;
                    tb3.Visible = true;
                    break;
            }
        }

        protected void btn_shr_Click(object sender, EventArgs e)
        {
            string i = ((Button)sender).ID.Substring(7);
            CheckBoxList ck = (CheckBoxList)Pan_ShenHe.FindControl("cki" + i);
            if (ck != null)
            {
                for (int j = 0; j < ck.Items.Count; j++)
                {
                    if (ck.Items[j].Selected == true)
                    {
                        if (i == "1")
                        {
                            firstid.Value = ck.Items[j].Value.ToString();
                            txt_first.Text = ck.Items[j].Text.ToString();
                        }
                        if (i == "2")
                        {
                            secondid.Value = ck.Items[j].Value.ToString();
                            txt_second.Text = ck.Items[j].Text.ToString();
                        }
                        if (i == "3")
                        {
                            thirdid.Value = ck.Items[j].Value.ToString();
                            txt_third.Text = ck.Items[j].Text.ToString();
                        }
                        return;
                    }
                }
                if (i == "1")
                {
                    firstid.Value = "";
                    txt_first.Text = "";
                }
                if (i == "2")
                {
                    secondid.Value = "";
                    txt_second.Text = "";
                }
                if (i == "3")
                {
                    thirdid.Value = "";
                    txt_third.Text = "";
                }
            }
        }

        private void Get_Shr()
        {
            //审核人1
            pal_select1_inner.Controls.Add(ShrTable("1"));

            // 审核人2
            pal_select2_inner.Controls.Add(ShrTable("2"));

            // 审核人3
            pal_select3_inner.Controls.Add(ShrTable("3"));
        }

        private Table ShrTable(string i)
        {
            string sql = "select st_ID,st_name from TBDS_STAFFINFO where st_depid='07' and ST_PD='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            Table tctrl = new Table();
            if (dt.Rows.Count != 0)
            {
                TableRow tr = new TableRow();
                TableCell td = new TableCell();

                CheckBoxList cki = new CheckBoxList();
                cki.ID = "cki" + i;
                cki.DataSource = dt;
                cki.DataTextField = "ST_NAME";//领导的姓名
                cki.DataValueField = "ST_ID";//领导的编号
                cki.DataBind();
                for (int k = 0; k < cki.Items.Count; k++)
                {
                    cki.Items[k].Attributes.Add("Onclick", "CheckBoxList_Click(this)");//使用了javascript使其只能勾选一个
                }
                cki.RepeatColumns = 5;//获取显示的列数
                td.Controls.Add(cki);
                tr.Cells.Add(td);
                tctrl.Controls.Add(tr);
            }
            return tctrl;
        }

    }
}
