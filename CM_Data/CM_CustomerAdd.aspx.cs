using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_CustomerAdd : System.Web.UI.Page
    {
        Dictionary<string, string> reviewer = new Dictionary<string, string>();//用于存储审核的名单
        protected void Page_Load(object sender, EventArgs e)
        {
            getLeaderInfo();
            if (!IsPostBack)
            {
                UserID.Value = Session["UserID"].ToString();
                BindpanCS();
                BindpanTH();
                BindpanPD();
            }
        }

        #region 增加行
        protected void btnadd_Click(object sender, EventArgs e)
        {
            CreateNewRow(Convert.ToInt32(num.Value));
        }

        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();
            InitVar();
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TSA_ID");
            dt.Columns.Add("CM_PIC");
            dt.Columns.Add("CM_EQUIP");
            dt.Columns.Add("CM_APPNAME");
            dt.Columns.Add("CM_NUM");
            dt.Columns.Add("CM_PLACE");
            dt.Columns.Add("CM_NOTE");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)retItem.FindControl("TSA_ID")).Text;
                newRow[1] = ((TextBox)retItem.FindControl("CM_PIC")).Text;
                newRow[2] = ((TextBox)retItem.FindControl("CM_EQUIP")).Text;
                newRow[3] = ((TextBox)retItem.FindControl("CM_APPNAME")).Text;
                newRow[4] = ((TextBox)retItem.FindControl("CM_NUM")).Text;
                newRow[5] = ((TextBox)retItem.FindControl("CM_PLACE")).Text;
                newRow[6] = ((TextBox)retItem.FindControl("CM_NOTE")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        private void InitVar()
        {
            if (Det_Repeater.Items.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            else
            {
                NoDataPanel.Visible = false;
                delete.Visible = true;
            }
        }

        #endregion

        private void BindpanCS()
        {
            string sql = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_DEPID ='12'and ST_PD='0' order by ST_POSITION";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            cbxCS.DataSource = dt;
            cbxCS.DataTextField = "ST_NAME";
            cbxCS.DataValueField = "ST_ID";
            cbxCS.DataBind();
        }
        private void BindpanTH()
        {
            string sql = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_DEPID ='03' and ST_PD='0' order by ST_POSITION";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            cbxTH.DataSource = dt;
            cbxTH.DataTextField = "ST_NAME";
            cbxTH.DataValueField = "ST_ID";
            cbxTH.DataBind();
        }
        private void BindpanPD()
        {
            string sql = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_DEPID ='04' and ST_PD='0' order by ST_POSITION";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            cbxPD.DataSource = dt;
            cbxPD.DataTextField = "ST_NAME";
            cbxPD.DataValueField = "ST_ID";
            cbxPD.DataBind();
        }
        protected void delete_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TSA_ID");
            dt.Columns.Add("CM_PIC");
            dt.Columns.Add("CM_EQUIP");
            dt.Columns.Add("CM_APPNAME");
            dt.Columns.Add("CM_NUM");
            dt.Columns.Add("CM_PLACE");
            dt.Columns.Add("CM_NOTE");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((TextBox)retItem.FindControl("TSA_ID")).Text;
                    newRow[1] = ((TextBox)retItem.FindControl("CM_PIC")).Text;
                    newRow[2] = ((TextBox)retItem.FindControl("CM_EQUIP")).Text;
                    newRow[3] = ((TextBox)retItem.FindControl("CM_APPNAME")).Text;
                    newRow[4] = ((TextBox)retItem.FindControl("CM_NUM")).Text;
                    newRow[5] = ((TextBox)retItem.FindControl("CM_PLACE")).Text;
                    newRow[6] = ((TextBox)retItem.FindControl("CM_NOTE")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();
            InitVar();
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (InorOut.SelectedValue == "2")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目内/外！');", true);
            }
            else
            {
                List<string> list_sql = new List<string>();
                StringBuilder col = new StringBuilder();
                StringBuilder val = new StringBuilder();
                foreach (Control control in plbasic.Controls)
                {
                    if (control is TextBox && control.ID != "CM_INKEEP")
                    {
                        col.Append(control.ID + ",");
                        val.Append("'" + ((TextBox)control).Text + "',");
                    }
                    else if (control.ID == "CM_INKEEP")
                    {
                        col.Append(control.ID + ",");
                        val.Append("'" + inKeep.Value + "',");
                    }
                }
                col.AppendFormat("CM_PIC,CM_EQUIP,CM_APPNAME,CM_NUM,CM_PLACE,CM_NOTE,CM_INOROUT,CM_MANCLERK,CM_ZJYUAN,CM_KGYUAN,TSA_ID,CM_GUID,CM_PSR");

                string zjy = "";
                string psr = "";
                bindReviewer();
                for (int i = 0; i < reviewer.Count; i++)
                {
                    psr += "," + reviewer.ElementAt(i).Value;
                }
                if (psr != "")
                {
                    psr = psr.Substring(1);
                }
                foreach (RepeaterItem retItem in Det_Repeater.Items)
                {
                    string val1 = ((TextBox)retItem.FindControl("CM_PIC")).Text;
                    string val2 = ((TextBox)retItem.FindControl("CM_EQUIP")).Text;
                    string val3 = ((TextBox)retItem.FindControl("CM_APPNAME")).Text;
                    string val4 = ((TextBox)retItem.FindControl("CM_NUM")).Text;
                    string val5 = ((TextBox)retItem.FindControl("CM_PLACE")).Text;
                    string val6 = ((TextBox)retItem.FindControl("CM_NOTE")).Text;
                    string val7 = ((TextBox)retItem.FindControl("TSA_ID")).Text;
                    string choose = "select * from TBQM_QTSASSGN where QSA_ENGID='" + val7 + "'";

                    bool tj = false;
                    DataTable chdt = DBCallCommon.GetDTUsingSqlText(choose);
                    if (chdt.Rows.Count > 0)
                    {
                        zjy = chdt.Rows[0]["QSA_QCCLERK"].ToString();
                    }
                    else
                    {
                        choose = "select * from TBQM_SetInspectPerson where ID='16' and num='顾客财产'";
                        chdt = DBCallCommon.GetDTUsingSqlText(choose);
                        if (chdt.Rows.Count > 0)
                        {
                            zjy = chdt.Rows[0]["InspectPerson"].ToString();
                        }
                        else
                        {
                            tj = true;
                        }
                    }
                    if (zjy == "")
                    {
                        choose = "select * from TBQM_SetInspectPerson where ID='16' and num='顾客财产'";
                        chdt = DBCallCommon.GetDTUsingSqlText(choose);
                        if (chdt.Rows.Count > 0)
                        {
                            zjy = chdt.Rows[0]["InspectPerson"].ToString();
                        }
                        else
                        {
                            tj = true;
                        }
                    }
                    if (tj)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "tishi", "alert('找不到质检员，请联系质量部！');", true); return;
                    }
                    string sqlTxt = string.Format("insert into TBCM_CUSTOMER({0}) values({1}'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", col, val, val1, val2, val3, val4, val5, val6, InorOut.SelectedValue, UserID.Value, zjy, inKeep.Value, val7, Guid.NewGuid().ToString(), psr);
                    list_sql.Add(sqlTxt);
                }
                DBCallCommon.ExecuteTrans(list_sql);//执行事务
                SendEmail();
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(zjy), new List<string>(), new List<string>(), "顾客财产质检", "您有合同号为" + CM_CONTR.Text.Trim() + ",项目名称为" + CM_PJNAME.Text.Trim() + ",顾客为" + CM_COSTERM.Text.Trim() + "的顾客财产需要质检，请登录系统查看。");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('添加成功！');", true);
                Response.Redirect("CM_CustomerAdd.aspx");
            }
        }

        private void SendEmail()
        {
            foreach (ListItem item in cbxCS.Items)
            {
                if (item.Selected)
                {
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(item.Value), new List<string>(), new List<string>(), "顾客财产质检", "您有合同号为" + CM_CONTR.Text.Trim() + ",项目名称为" + CM_PJNAME.Text.Trim() + ",顾客为"+CM_COSTERM.Text.Trim()+"的顾客财产需要质检，请登录系统查看。");
                }
            }
            foreach (ListItem item in cbxTH.Items)
            {
                if (item.Selected)
                {
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(item.Value), new List<string>(), new List<string>(), "新增顾客财产查看", "合同号为" + CM_CONTR.Text.Trim() + ",项目名称为" + CM_PJNAME.Text.Trim() + ",顾客为" + CM_COSTERM.Text.Trim() + "的顾客财产下推至质检，请登录系统查看。");
                }
            }
            foreach (ListItem item in cbxPD.Items)
            {
                if (item.Selected)
                {
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(item.Value), new List<string>(), new List<string>(), "新增顾客财产查看", "合同号为" + CM_CONTR.Text.Trim() + ",项目名称为" + CM_PJNAME.Text.Trim() + ",顾客为" + CM_COSTERM.Text.Trim() + "的顾客财产下推至质检，请登录系统查看。");
                }
            }
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("CM_Customer.aspx");
        }

        #region 得到领导信息
        Table t = new Table();
        int rowsum = 0;
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
            string sql = string.Format("select ST_NAME,ST_ID,ST_DEPID from TBDS_STAFFINFO as a inner join TBCM_HT_SETTING as b on a.ST_ID=b.per_id where a.ST_PD='0'and b.dep_id='{0}' and b.per_sfjy='0' and b.per_type='4'", st_id);
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
                cki.CellSpacing = 10;
                if (i == 4)
                {
                    for (int k = 0; k < cki.Items.Count; k++)
                    {
                        //cki.Items[k].Attributes.Add("Onclick", "CheckBoxList_Click(this)");//使用了javascript使其只能勾选一个
                        cki.Items[k].Attributes.Add("width", "100px");
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
    }
}
