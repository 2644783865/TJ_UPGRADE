using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_ADDTASK : System.Web.UI.Page
    {
        string id = string.Empty;//全局变量id
        string action = string.Empty;
        string chid = string.Empty;
        List<string> str = new List<string>();
        Table t = new Table();
        Dictionary<string, string> reviewer = new Dictionary<string, string>();//用于存储审核的名单
        int rowsum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
                id = Request.QueryString["id"].ToString();//识别号
            if (Request.QueryString["action"] != null)
                action = Request.QueryString["action"].ToString();//操作
            if (Request.QueryString["chid"] != null)
                chid = Request.QueryString["chid"].ToString();//变更识别号
            getLeaderInfo();
            Get_Shr();
            if (!IsPostBack)
            {
                this.Title = "项目基本信息管理";
                CM_FILLDATA.Text = DateTime.Now.ToString();
                Hidden.Value = DateTime.Now.ToString("yyyyMMddHHmmssff");
                CM_MANCLERK.Text = Session["UserName"].ToString();
                UserID.Value = Session["UserId"].ToString();
                DepID.Value = Session["UserDeptID"].ToString();
                if (action == "edit" || action == "change")  //更新项目
                {
                    ShowData();
                    bindReviewer();
                    BindSelectReviewer();
                }
                InitVar();
                if (action == "change")
                {
                    //TSA_ID.Visible = false;
                    btnadd.Visible = false;
                    delete.Visible = false;
                    chan.Visible = true;
                }
                rblShdj_Changed(null, null);
                PowerControl();
            }
        }

        private void PowerControl()
        {
            if (action == "add" || action == "edit")
            {
                btnSubmit.Visible = true;
            }
            else
            {
                btnSubmit.Visible = false;
            }
            if (action == "edit")
            {
                asd.sfbc = "y";
                asd.time = id;
            }
        }

        #region 当编辑时，将数据绑定
        private void ShowData()
        {
            string str1 = "select a.*,b.ST_NAME as CM_NAME,c.ST_NAME as CM_NAME1,d.ST_NAME as CM_NAME2,e.ST_NAME as CM_NAME3 from TBCM_PLAN as a left join TBDS_STAFFINFO as b on a.CM_MANCLERK=b.ST_ID left join TBDS_STAFFINFO as c on a.CM_PS1=c.ST_ID left join TBDS_STAFFINFO as d on a.CM_PS2=d.ST_ID left join TBDS_STAFFINFO as e on a.CM_PS3=e.ST_ID where ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(str1);
            Assign(Panel, dt);
            Assign(Panel1, dt);
            CM_MANCLERK.Text = dt.Rows[0]["CM_NAME"].ToString();
            string str2 = "select CM_ID,TSA_ID,TSA_ENGNAME,TSA_MAP,TSA_NUMBER,TSA_UNIT,TSA_MATERIAL,TSA_IDNOTE,TSA_TYPE from TBCM_BASIC where ID='" + id + "' order by CM_ID";
            DataTable gr = DBCallCommon.GetDTUsingSqlText(str2);
            gr = GetDate(gr);
            this.GridView1.DataSource = gr;
            this.GridView1.DataBind();
            if (dt.Rows.Count > 0)
            {
                CM_Context = dt.Rows[0]["CM_CONTEXT"].ToString();
                Hidden.Value = dt.Rows[0]["CM_CONTEXT"].ToString();
                HiddenId.Value = dt.Rows[0]["ID"].ToString();
                string level = dt.Rows[0]["CM_PSJB"].ToString();
                rblShdj.SelectedValue = level;
                if (action == "edit")
                {
                    firstid.Value = dt.Rows[0]["CM_PS1"].ToString();
                    txt_first.Text = dt.Rows[0]["CM_NAME1"].ToString();
                    rbl_first.Text = dt.Rows[0]["CM_PSYJ1"].ToString();
                    first_time.Text = dt.Rows[0]["CM_PSSJ1"].ToString();
                    first_opinion.Text = dt.Rows[0]["CM_NOTE1"].ToString();
                    first_opinion.ReadOnly = true;

                    secondid.Value = dt.Rows[0]["CM_PS2"].ToString();
                    txt_second.Text = dt.Rows[0]["CM_NAME2"].ToString();
                    rbl_second.Text = dt.Rows[0]["CM_PSYJ2"].ToString();
                    second_time.Text = dt.Rows[0]["CM_PSSJ2"].ToString();
                    second_opinion.Text = dt.Rows[0]["CM_NOTE2"].ToString();
                    second_opinion.ReadOnly = true;

                    thirdid.Value = dt.Rows[0]["CM_PS3"].ToString();
                    txt_third.Text = dt.Rows[0]["CM_NAME3"].ToString();
                    rbl_third.Text = dt.Rows[0]["CM_PSYJ3"].ToString();
                    third_time.Text = dt.Rows[0]["CM_PSSJ3"].ToString();
                    third_opinion.Text = dt.Rows[0]["CM_NOTE3"].ToString();
                    third_opinion.ReadOnly = true;
                }
            }
            string str3 = "select * from TBCM_PSVIEW where CM_ID='" + id + "' and CM_PIDTYPE='0'";
            dt = DBCallCommon.GetDTUsingSqlText(str3);
            if (dt.Rows.Count > 0)
            {
                txt_zdrYJ.Text = dt.Rows[0]["CM_NOTE"].ToString();
            }
            if (action == "change")
            {
                txt_zdrYJ.Text = "";
                tx1.Visible = false;
                tx2.Visible = false;
                foreach (Control item in Panel.Controls)
                {
                    if (item is TextBox && item.ID.ToString() != "TSA_NOTE" && item.ID.ToString() != "CM_DFCONTR")
                    {
                        ((TextBox)item).ReadOnly = true;
                        ((TextBox)item).BorderStyle = BorderStyle.None;
                        ((TextBox)item).BackColor = System.Drawing.Color.Transparent;
                    }
                }
            }
        }

        public string CM_Context
        {
            get;
            set;
        }

        private DataTable GetDate(DataTable gr)
        {
            DataTable dt = GetTable();
            for (int i = 0; i < gr.Rows.Count; i++)
            {
                DataRow drow = gr.Rows[i];
                str.Add(drow["TSA_ID"].ToString());
            }
            str = str.Distinct<string>().ToList();
            for (int i = 0; i < gr.Rows.Count; i++)
            {
                DataRow dr = gr.Rows[i];
                DataRow newRow = dt.NewRow();
                if (str.Contains(dr["TSA_ID"].ToString()))
                {
                    newRow[0] = dr["TSA_ID"].ToString();
                    str.Remove(dr["TSA_ID"].ToString());
                }
                newRow[1] = dr["TSA_ENGNAME"].ToString();
                newRow[2] = dr["TSA_MAP"].ToString();
                newRow[3] = dr["TSA_NUMBER"].ToString();
                newRow[4] = dr["TSA_UNIT"].ToString();
                newRow[5] = dr["TSA_MATERIAL"].ToString();
                newRow[6] = dr["TSA_IDNOTE"].ToString();
                newRow[7] = dr["TSA_TYPE"].ToString();
                newRow[8] = dr["CM_ID"].ToString();

                dt.Rows.Add(newRow);
            }
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

        private void Assign(Panel panel, DataTable dt)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is TextBox)
                {
                    if (dt.Columns.Contains(((TextBox)control).ID))
                    {
                        ((TextBox)control).Text = dt.Rows[0][control.ID].ToString();
                    }
                }
            }
        }
        #endregion

        #region 增加行
        protected void btnadd_Click(object sender, EventArgs e)
        {
            //string SqlText = "select * from TBCM_BASIC where CM_CONTR='" + CM_CONTR.Text + "'";
            //DataTable dt = DBCallCommon.GetDTUsingSqlText(SqlText);
            //if (dt.Rows.Count > 0)
            //{
            //    //Response.Write("<script>alert(合同已添加，请重新选择合同！)</script>");
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('合同已添加，请重新选择合同！');", true);
            //}
            //else
            //{
            if (TSA_ID.Text.Trim() != "" && CM_CONTR.Text != "")
            {
                if (Regex.IsMatch(TSA_ID.Text.Trim(), @"^\d{5}([A-Za-z0-9-])+") || rblLX.SelectedValue != "zc")
                {
                    if (Regex.IsMatch(TSA_ID.Text.Trim(), @"[\u2E80-\u9FFF]+"))
                    {
                        Response.Write("<script>alert('任务号里请勿输入汉字！');</script>");//后台验证
                    }
                    else
                    {
                        int count = GridView1.Rows.Count;
                        AddStr();
                        CreateNewRow(Convert.ToInt32(num.Value), count);
                    }
                }
                else
                {
                    Response.Write("<script>alert('请输入正确的任务号（如14000SF1-1等）！');</script>");//后台验证
                }
            }
            //}
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

        private class asd
        {
            public static string time;
            public static string sfbc;
            public static string addsavevalid;
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            if (action == "edit")
            {
                if (asd.addsavevalid != "YS")
                {
                    Response.Write("<script>alert('您还未点击保存，请点击保存后再提交审批！！！');</script>"); return;
                }
            }
            if (asd.sfbc != "y")
            {
                Response.Write("<script>alert('您还未点击保存，请点击保存后再提交审批！！！');</script>"); return;
            }
            else
            {
                string sql = " update TBCM_PLAN set CM_SPSTATUS='1' where ID ='" + asd.time + "'";
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {
                    Response.Write("<script>alert('提交审批的语句出现问题，请与管理员联系！！！');</script>"); return;
                }
            }
            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(firstid.Value), new List<string>(), new List<string>(), "经营计划单审批", "您有经营计划单需要审批，请登录系统进行查看。");
            Response.Redirect("CM_PJinfo.aspx");
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
                        int int_htps_exist=0;
            string sql_htps_exist="select PCON_BCODE from (View_TBCR_CONTRACTREVIEW_ALL as a left join (select distinct ID as ID1 ,CM_XM as CM_XM1,CM_ZJ as CM_JLL from TBPM_HESUAN where CM_XM='净利率') as b on a.CR_ID=b.ID1 left join (select distinct ID as ID2,CM_XM as CM_XM2,CM_ZJ as CM_JLR from TBPM_HESUAN where CM_XM='净利润')as c on a.CR_ID=c.ID2)";
            DataTable dt_htps_exist=DBCallCommon.GetDTUsingSqlText(sql_htps_exist);
            if(dt_htps_exist.Rows.Count>0)
            {
                for (int h = 0; h < dt_htps_exist.Rows.Count; h++)
                {
                    string htps_exist = dt_htps_exist.Rows[h]["PCON_BCODE"].ToString();
                    if (dt_htps_exist.Rows[h]["PCON_BCODE"].ToString() == CM_CONTR.Text)
                    {
                        int_htps_exist = 1;
                        break;
                    }

                }
            }
            if (int_htps_exist == 1)
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
                    List<string> list_sql = new List<string>();
                    string code = DateTime.Now.ToString("yyyyMMddHHmmss");
                    if (action == "add" || action == "edit")
                    {
                        if (action == "edit")
                        {
                            asd.addsavevalid = "YS";
                        }
                        asd.sfbc = "y";
                        asd.time = code;
                        #region 存入计划单信息

                        if (GridView1.Rows.Count == 0)
                        {
                            Response.Write("<script>alert('请添加任务号！');</script>"); return;//后台验证
                        }
                        bool b = false;
                        bool c = false;
                        string rep = "";
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            GridViewRow gr = GridView1.Rows[i];
                            string tsa_id = ((ITextControl)gr.FindControl("TSA_ID")).Text;
                            if (tsa_id != "")
                            {
                                if (!Regex.IsMatch(tsa_id, @"^\d{5}([A-Za-z0-9-])+") && rblLX.SelectedValue == "zc")
                                {
                                    b = true;
                                }
                                else
                                {
                                    string sqlstr = "select * from TBPM_TCTSASSGN where TSA_ID='" + tsa_id + "'";
                                    if (DBCallCommon.GetDTUsingSqlText(sqlstr).Rows.Count > 0)
                                    {
                                        c = true;
                                        rep = tsa_id;
                                    }
                                }
                            }
                        }
                        if (b)
                        {
                            Response.Write("<script>alert('请输入正确的任务号（如14000SF1-1等）！');</script>"); return;//后台验证
                        }
                        else if (c)
                        {
                            Response.Write("<script>alert('任务号" + rep + "与已有重复，请更改！');</script>"); return;//后台验证
                        }
                        else
                        {
                            #region 编辑时触发，先将原数据删除，然后再插入
                            if (action == "edit")
                            {
                                string sqltext = "delete from TBCM_BASIC where ID='" + id + "'";//任务号基本信息表
                                list_sql.Add(sqltext);
                                sqltext = "delete from TBCM_PLAN where ID='" + id + "'";//任务号计划表
                                list_sql.Add(sqltext);
                                sqltext = "delete from TBCM_PSVIEW where CM_ID='" + id + "'";
                                list_sql.Add(sqltext);
                            }
                            #endregion

                            #region 存入TBCM_BASIC表
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
                                list_sql.Add(string.Format("insert into TBCM_BASIC(ID,TSA_ID,TSA_ENGNAME,TSA_MAP,TSA_NUMBER,TSA_UNIT,TSA_MATERIAL,TSA_IDNOTE,TSA_TYPE,CM_FILLDATA) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", code, tsa_id, ((ITextControl)gr.FindControl("TSA_ENGNAME")).Text.Trim().Replace(" ", ""), ((ITextControl)gr.FindControl("TSA_MAP")).Text.Trim(), ((ITextControl)gr.FindControl("TSA_NUMBER")).Text.Trim(), ((ITextControl)gr.FindControl("TSA_UNIT")).Text.Trim(), ((ITextControl)gr.FindControl("TSA_MATERIAL")).Text.Trim(), ((ITextControl)gr.FindControl("TSA_IDNOTE")).Text.Trim(), ((DropDownList)gr.FindControl("TSA_TYPE")).SelectedValue, CM_FILLDATA.Text.Trim()));
                                //DBCallCommon.ExeSqlText(SqlText);
                            }
                            #endregion

                            if (action == "add" || action == "edit")
                            {
                                #region 存入TBCM_PLAN表
                                StringBuilder AddCol = new StringBuilder();
                                StringBuilder AddValue = new StringBuilder();
                                foreach (Control control in Panel1.Controls)
                                {
                                    if (control is TextBox)
                                    {
                                        AddCol.Append(((TextBox)control).ID).Append(",");
                                        AddValue.Append("'").Append(((TextBox)control).Text.Trim()).Append("',");
                                    }
                                }
                                AddCol.Append("CM_COMP,CM_PROJ,CM_ID,CM_CONTR,CM_DFCONTR,CM_MANCLERK,TSA_NOTE,CM_CONTEXT,ID,CM_ZDTIME,CM_SPSTATUS,CM_PS1,CM_PS2,CM_PS3,CM_PSJB,CM_PSYJ1,CM_PSYJ2,CM_PSYJ3");
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
                                AddValue.Append("'" + CM_COMP.Text.Trim() + "','").Append(CM_PROJ.Text.Trim() + "','").Append(CM_ID.Text.Trim() + "','").Append(CM_CONTR.Text.Trim() + "','").Append(CM_DFCONTR.Text.Trim() + "','").Append(UserID.Value + "','").Append(TSA_NOTE.Text.Trim() + "','").Append(Hidden.Value + "','").Append(code + "','").Append(DateTime.Now.ToString("yyyy-MM-dd") + "',").Append("'0',").Append("'" + firstid.Value + "',").Append("'" + secondid.Value + "',").Append("'" + thirdid.Value + "',").Append("'" + rblShdj.SelectedValue + "',").Append("'" + yj[0] + "',").Append("'" + yj[1] + "',").Append("'" + yj[2] + "'");
                                list_sql.Add(string.Format("insert into TBCM_PLAN({0}) values({1})", AddCol, AddValue));
                                if (action != "add" && action != "edit")
                                {
                                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(firstid.Value), new List<string>(), new List<string>(), "经营计划单审批", "您有经营计划单需要审批，请登录系统进行查看。");
                                }
                                #endregion
                            }
                            else
                            {
                                #region 存入TBCM_PLAN表
                                StringBuilder AddCol = new StringBuilder();
                                StringBuilder AddValue = new StringBuilder();
                                foreach (Control control in Panel1.Controls)
                                {
                                    if (control is TextBox)
                                    {
                                        AddCol.Append(((TextBox)control).ID).Append(",");
                                        AddValue.Append("'").Append(((TextBox)control).Text.Trim()).Append("',");
                                    }
                                }
                                AddCol.Append("CM_COMP,CM_PROJ,CM_ID,CM_CONTR,CM_DFCONTR,CM_MANCLERK,TSA_NOTE,CM_CONTEXT,ID,CM_ZDTIME,CM_SPSTATUS,CM_PS1,CM_PS2,CM_PS3,CM_PSJB,CM_PSYJ1,CM_PSYJ2,CM_PSYJ3");
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
                                AddValue.Append("'" + CM_COMP.Text.Trim() + "','").Append(CM_PROJ.Text.Trim() + "','").Append(CM_ID.Text.Trim() + "','").Append(CM_CONTR.Text.Trim() + "','").Append(CM_DFCONTR.Text.Trim() + "','").Append(UserID.Value + "','").Append(TSA_NOTE.Text.Trim() + "','").Append(Hidden.Value + "','").Append(code + "','").Append(DateTime.Now.ToString("yyyy-MM-dd") + "',").Append("'1',").Append("'" + firstid.Value + "',").Append("'" + secondid.Value + "',").Append("'" + thirdid.Value + "',").Append("'" + rblShdj.SelectedValue + "',").Append("'" + yj[0] + "',").Append("'" + yj[1] + "',").Append("'" + yj[2] + "'");
                                list_sql.Add(string.Format("insert into TBCM_PLAN({0}) values({1})", AddCol, AddValue));

                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(firstid.Value), new List<string>(), new List<string>(), "经营计划单审批", "您有经营计划单需要审批，请登录系统进行查看。");

                                #endregion
                            }

                        }

                        #endregion
                    }
                    if (action == "change")
                    {
                        #region 变更

                        string sqltext = "delete from TBCM_CHANLIST where CH_ID='" + chid + "'";//变更台账表
                        list_sql.Add(sqltext);
                        sqltext = "delete from TBCM_CHANGE where CH_ID='" + chid + "'";//变更明细表
                        list_sql.Add(sqltext);
                        sqltext = "delete from TBCM_ChangePart where CH_ID='" + chid + "'";//变更明细表
                        list_sql.Add(sqltext);
                        sqltext = "delete from TBCM_PSVIEW where CM_ID='" + chid + "'";
                        list_sql.Add(sqltext);

                        //20150603改，备注。
                        sqltext = "update TBCM_PLAN set CM_BGBZ='" + TSA_NOTE.Text + "' where ID='" + id + "'";
                        list_sql.Add(sqltext);
                        sqltext = "update TBCM_PLAN set CM_BGYZHTH='" + CM_DFCONTR.Text + "' where ID='" + id + "'";
                        list_sql.Add(sqltext);

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
                        List<string> chan = new List<string>() { "TSA_ENGNAME", "TSA_MAP", "TSA_NUMBER", "TSA_UNIT", "TSA_MATERIAL", "TSA_IDNOTE", "TSA_TYPE" };
                        string sql_change = string.Format("insert into TBCM_CHANLIST(CH_ID,ID,CM_ITEM,CM_MANCLERK,CM_ZDTIME,CM_STATE,CM_PS1,CM_PS2,CM_PS3,CM_PSYJ1,CM_PSYJ2,CM_PSYJ3,CM_PSJB) values('{0}','{1}','{2}','{3}','{4}','1','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", code, HiddenId.Value, txt_change.Text.Trim(), UserID.Value, DateTime.Now.ToString("yyyy-MM-dd"), firstid.Value, secondid.Value, thirdid.Value, yj[0], yj[1], yj[2], rblShdj.SelectedValue);
                        list_sql.Add(sql_change);
                        int ch = 0;
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            GridViewRow gr = GridView1.Rows[i];
                            string a = ((HiddenField)gr.FindControl("hide")).Value;
                            string sqls = "select * from TBCM_BASIC where CM_ID='" + a + "'";
                            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqls);
                            bool bch = false;
                            if (dt.Rows.Count > 0)
                            {
                                for (int j = 0; j < chan.Count - 1; j++)
                                {
                                    if (((TextBox)gr.FindControl(chan[j])).Text.Trim() != dt.Rows[0][chan[j]].ToString().Trim())
                                    {
                                        bch = true;
                                    }
                                }
                                if (((DropDownList)gr.FindControl(chan[chan.Count - 1])).SelectedValue != dt.Rows[0][chan[chan.Count - 1]].ToString().Trim())
                                {
                                    bch = true;
                                }
                                if (bch)
                                {
                                    sql_change = string.Format("insert into TBCM_CHANGE values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','0')", code, dt.Rows[0]["ID"].ToString(), a, dt.Rows[0]["TSA_ID"].ToString(), dt.Rows[0]["TSA_ENGNAME"].ToString(), dt.Rows[0]["TSA_MAP"].ToString(), dt.Rows[0]["TSA_NUMBER"].ToString(), dt.Rows[0]["TSA_UNIT"].ToString(), dt.Rows[0]["TSA_MATERIAL"].ToString(), dt.Rows[0]["TSA_IDNOTE"].ToString(), dt.Rows[0]["TSA_TYPE"].ToString());
                                    list_sql.Add(sql_change);
                                    sql_change = string.Format("insert into TBCM_CHANGE values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','1')", code, dt.Rows[0]["ID"].ToString(), a, dt.Rows[0]["TSA_ID"].ToString(), ((TextBox)gr.FindControl("TSA_ENGNAME")).Text.Trim().Replace(" ", ""), ((TextBox)gr.FindControl("TSA_MAP")).Text.Trim(), ((TextBox)gr.FindControl("TSA_NUMBER")).Text.Trim(), ((TextBox)gr.FindControl("TSA_UNIT")).Text.Trim(), ((TextBox)gr.FindControl("TSA_MATERIAL")).Text.Trim(), ((TextBox)gr.FindControl("TSA_IDNOTE")).Text.Trim(), ((DropDownList)gr.FindControl("TSA_TYPE")).SelectedValue);
                                    list_sql.Add(sql_change);
                                    ch++;
                                }
                            }
                        }

                        string sqlstr = "select * from TBCM_PLAN where ID='" + id + "'";
                        DataTable plan = DBCallCommon.GetDTUsingSqlText(sqlstr);
                        Dictionary<string, string> dic = new Dictionary<string, string>() { { "CM_FHDATE", "交货日期" }, { "CM_LEVEL", "质量标准" }, { "CM_TEST", "质量校验与验收" }, { "CM_JHADDRESS", "交货地点及运输方式" }, { "CM_BZ", "包装要求" }, { "CM_JH", "交货要求" }, { "CM_YQ", "油漆要求" }, { "CM_DUTY", "买方责任人" }, { "CM_NOTE", "备注" }, { "CM_DFCONTR", "对方合同号" } };
                        if (plan.Rows.Count > 0)
                        {
                            for (int i = 0; i < dic.Count; i++)
                            {
                                string tb_id = dic.Keys.ElementAt(i);
                                TextBox tb = (TextBox)Panel1.FindControl(tb_id);
                                if (tb.Text.Trim() != plan.Rows[0][tb_id].ToString())
                                {
                                    list_sql.Add(string.Format("insert into TBCM_ChangePart values('{0}','{1}','{2}','0')", code, dic[tb_id], plan.Rows[0][tb_id]));
                                    list_sql.Add(string.Format("insert into TBCM_ChangePart values('{0}','{1}','{2}','1')", code, dic[tb_id], tb.Text.Trim()));
                                    ch++;
                                }
                            }

                            // 发送邮件
                            //string sql1 = "select ID,TSA_ID,TSA_ENGNAME,MTA_DUY,TSA_TCCLERKNM,QSA_QCCLERKNM from TBCM_BASIC as a left join View_TBMP_TASKASSIGN as b on a.TSA_ID=b.MTA_ID where ID=" + id;
                            //DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
                            //if (dt.Rows.Count > 0)
                            //{
                            //    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(GetUesrID(dt.Rows[0]["MTA_DUY"].ToString())), new List<string>(), new List<string>(), "经营计划单变更", "任务号“" + dt.Rows[0]["TSA_ID"].ToString() + "“产品名称为" + dt.Rows[0]["TSA_ENGNAME"].ToString() + "的经营计划单已经变更，请您上系统查看");
                            //    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(GetUesrID(dt.Rows[0]["TSA_TCCLERKNM"].ToString())), new List<string>(), new List<string>(), "经营计划单变更", "任务号“" + dt.Rows[0]["TSA_ID"].ToString() + "“产品名称为" + dt.Rows[0]["TSA_ENGNAME"].ToString() + "的经营计划单已经变更，请您上系统查看");
                            //    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(GetUesrID(dt.Rows[0]["QSA_QCCLERKNM"].ToString())), new List<string>(), new List<string>(), "经营计划单变更", "任务号“" + dt.Rows[0]["TSA_ID"].ToString() + "“产品名称为" + dt.Rows[0]["TSA_ENGNAME"].ToString() + "的经营计划单已经变更，请您上系统查看");
                            //}
                        }
                        if (ch == 0)
                        {
                            Response.Write("<script>alert('未变更任何内容！');</script>"); return;//后台验证
                        }
                        #endregion
                    }

                    //抄送人员信息详细表(TBCM_PSVIEW)
                    #region 抄送人

                    bindReviewer();
                    string sql = "insert into TBCM_PSVIEW values('" + code + "','" + UserID.Value + "','2','" + txt_zdrYJ.Text.Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + DepID.Value + "','0')";
                    list_sql.Add(sql);  //制单人意见
                    for (int i = 0; i < reviewer.Count; i++)
                    {
                        /******************通过键来找值******************************************************/
                        /**为了兼容领导同时为部门负责人的情况，评审人部门编号要以评审人员设置表中为准，而不以当前登录人部门编号为依据**/
                        string sql_dep = "select dep_id from TBCM_HT_SETTING where per_id='" + reviewer.Values.ElementAt(i) + "' and per_sfjy=0 and per_type='2'";
                        DataTable dt_dep = DBCallCommon.GetDTUsingSqlText(sql_dep);
                        if (dt_dep.Rows.Count > 0)
                        {
                            sql = "insert into TBCM_PSVIEW(CM_ID,CM_PID,CM_DEP,CM_PIDTYPE) values('" + code + "','" + reviewer.Values.ElementAt(i) + "','" + dt_dep.Rows[0]["dep_id"].ToString().Substring(0, 2) + "','1')";
                            list_sql.Add(sql);//其他人
                        }
                    }

                    #endregion

                    DBCallCommon.ExecuteTrans(list_sql);//执行事务
                    //if (Request.QueryString["action"].ToString() == "edit")
                    //{
                    //    //Response.Write("<script>alert('修改成功！');</script>");
                    //    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('修改成功！');", true);
                    //    Response.Redirect("CM_PJinfo.aspx");
                    //}
                    if (action == "change")
                    {
                        if (chid == "")
                        {
                            Response.Redirect("CM_TaskEdit.aspx");
                        }
                        else
                        {
                            Response.Redirect("CM_ChangePS.aspx");
                        }
                    }
                    //else
                    //{
                    //    Response.Redirect("CM_PJinfo.aspx");
                    //}
                    btnSave.Visible = false;
                }
            }
            else
            {
                Response.Write("<script>alert('不存在该合同，请先进行添加合同审批！');</script>"); return;//后台验证
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

        protected void Back_Click(object sender, EventArgs e)
        {
            if (action == "change")
            {
                Response.Redirect("CM_TaskEdit.aspx");
            }
            else
            {
                Response.Redirect("CM_PJinfo.aspx");
            }
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
            string myid = "";
            if (action == "change")
            {
                myid = chid;
            }
            else
            {
                myid = id;
            }
            string check_select = "select CM_PID from TBCM_PSVIEW where CM_ID='" + myid + "' and CM_PIDTYPE!='0'";
            DataTable sele = DBCallCommon.GetDTUsingSqlText(check_select);
            for (int i = 0; i < 6; i++)
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.ToString() == "DataRow")
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
                if (action == "change")
                    tb.ReadOnly = true;
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
            string sql = "select CM_TYPE,CM_REFER from TBCM_TYPE ORDER BY CM_TYPE";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql); 
            //ddl.DataSource = dt;
            //ddl.DataValueField = "CM_TYPE";
            //ddl.DataTextField = "CM_REFER";
            //ddl.DataBind();
            foreach (DataRow dr in dt.Rows)
            {
                ddl.Items.Add(new ListItem(" "+dr["CM_TYPE"].ToString() + " -- " + dr["CM_REFER"].ToString(), dr["CM_TYPE"].ToString()));
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
            string sql = "select st_ID,st_name from TBDS_STAFFINFO where (st_depid='07'or st_depid='01') and ST_PD='0'order by st_ID";
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
