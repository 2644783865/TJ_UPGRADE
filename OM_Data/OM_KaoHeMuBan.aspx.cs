using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_KaoHeMuBan : System.Web.UI.Page
    {
        string action = string.Empty;
        string key = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
                action = Request.QueryString["action"];
            if (Request.QueryString["fkey"] != null)
                key = Request.QueryString["fkey"];
            if (!IsPostBack)
            {
                if (action == "add")
                {


                    DataTable dt = new DataTable();
                    for (int i = 1; i < 7; i++)
                    {
                        dt.Columns.Add("col" + i);
                    }
                    this.Det_Repeater.DataSource = dt;
                    this.Det_Repeater.DataBind();
                    NoDataPanel.Visible = true;
                    BindPart();
                    hidAddPer.Value = Session["UserID"].ToString();
                }
                else if (action == "edit")
                {
                    hidAddPer.Value = Session["UserID"].ToString();
                    BindPart();
                    ShowData();
                }
                else if (action == "view")
                {
                    BindPart();
                    ShowData();
                    btnsubmit.Visible = false;

                }
            }



        }


        private void BindPart()
        {
            string sql = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
            ListItem itemNew = new ListItem();
            itemNew.Text = "通用模板";
            itemNew.Value = "TongY";
            ddl_Depart.Items.Insert(0, itemNew);
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddl_Depart.Items.Insert(0, item);
            ddl_Depart.SelectedValue = "00";
        }

        private void ShowData()
        {
            string sql = "select a.kh_Dep,a.kh_Type,b.DEP_NAME as Dep,c.DEP_NAME as Position,a.kh_Name,a.kh_BL from TBDS_KaoHeMBList as a left join TBDS_DEPINFO as b on a.kh_Dep=b.DEP_CODE left join TBDS_DEPINFO as c on a.kh_Type=c.DEP_CODE where kh_Fkey='" + key + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DataRow dr = dt.Rows[0];
            //ddl_Depart.Items.Add(new ListItem(dr["Dep"].ToString(), dr["kh_Dep"].ToString()));
            //ddl_Depart.Enabled = false;
            ddl_Depart.SelectedValue = dr["kh_Dep"].ToString().Trim();
            delete.Visible = true;

            sql = "select a.kh_Dep,a.kh_Type,a.kh_Type1,b.DEP_NAME as Dep,c.DEP_NAME as Position,a.kh_Name,a.kh_BL,a.kh_Note from TBDS_KaoHeMBList as a left join TBDS_DEPINFO as b on a.kh_Dep=b.DEP_CODE left join TBDS_DEPINFO as c on a.kh_Type=c.DEP_CODE where kh_Fkey='" + key + "'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            dr = dt.Rows[0];

            txtName.Text = dr["kh_Name"].ToString();
            string[] bl = dr["kh_BL"].ToString().Split('|');
            txtBl1.Text = bl[0];
            txtBl2.Text = bl[1];
            txtBl3.Text = bl[2];
            txtBl4.Text = bl[3];
            ddlType.SelectedValue = dr["kh_Type1"].ToString();
            txtPFBZ.Text = dr["kh_Note"].ToString();
            // btnedit.Visible = true;
            //btnadd.Visible = false;
            //delete.Visible = false;
            sql = "select * from TBDS_KaoHeMuB where kh_Fkey='" + key + "'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            Det_Repeater.DataSource = dt;
            Det_Repeater.DataBind();
            for (int i = 0; i < Det_Repeater.Items.Count; i++)
            {
                for (int j = 1; j < 6; j++)
                {
                    ITextControl ctrl = ((ITextControl)Det_Repeater.Items[i].FindControl("txt" + j));
                    ctrl.Text = ctrl.Text.Replace("<br />", "");
                }
            }
            NoDataPanel.Visible = false;
            sql = "select * from TBDS_KaoHeCol where kh_Fkey='" + key + "'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i < 6; i++)
                {
                    ((ITextControl)Det_Repeater.Controls[0].FindControl("kh_Col" + i)).Text = dt.Rows[0]["kh_Col" + i].ToString();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('数据有误，请联系管理员！');", true);
            }
        }




        #region 增加删除行

        protected void btnadd_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (int.TryParse(txtNum.Text, out a))
            {
                CreateNewRow(a);
            }
            else
            {
                Response.Write("<script>alert('请输入数字！')</script>");
            }
        }

        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            List<string> col = new List<string>();
            for (int i = 1; i < 6; i++)
            {
                col.Add(((ITextControl)Det_Repeater.Controls[0].FindControl("kh_Col" + i)).Text);
            }
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();
            InitVar(col);
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            for (int i = 1; i < 6; i++)
            {
                dt.Columns.Add("kh_Cont" + i);
            }
            dt.Columns.Add("kh_Weight");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                DataRow newRow = dt.NewRow();
                for (int i = 1; i < 7; i++)
                {
                    newRow[i - 1] = ((TextBox)retItem.FindControl("txt" + i)).Text;
                }
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        private void InitVar(List<string> col)
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
            for (int i = 1; i < 6; i++)
            {
                ((ITextControl)Det_Repeater.Controls[0].FindControl("kh_Col" + i)).Text = col[i - 1];
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            List<string> col = new List<string>();
            for (int i = 1; i < 6; i++)
            {
                col.Add(((ITextControl)Det_Repeater.Controls[0].FindControl("kh_Col" + i)).Text);
            }
            DataTable dt = new DataTable();
            for (int i = 1; i < 6; i++)
            {
                dt.Columns.Add("kh_Cont" + i);
            }
            dt.Columns.Add("kh_Weight");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    for (int i = 1; i < 7; i++)
                    {
                        newRow[i - 1] = ((TextBox)retItem.FindControl("txt" + i)).Text;
                    }
                    dt.Rows.Add(newRow);
                }
            }
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();
            InitVar(col);
        }

        #endregion

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string dep = ddl_Depart.SelectedValue;
            string pos = "";
            string name = txtName.Text.Trim();
            List<string> list = new List<string>();
            int bz = CommonFun.ComTryInt(txtBl1.Text.Trim()) + CommonFun.ComTryInt(txtBl2.Text.Trim()) + CommonFun.ComTryInt(txtBl3.Text.Trim()) + CommonFun.ComTryInt(txtBl4.Text.Trim());
            if (bz != 100)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请填写正确的权重！');", true);
                return;
            }
            string bl1 = txtBl1.Text.Trim() == "" ? "0" : txtBl1.Text.Trim();
            string bl2 = txtBl2.Text.Trim() == "" ? "0" : txtBl2.Text.Trim();
            string bl3 = txtBl3.Text.Trim() == "" ? "0" : txtBl3.Text.Trim();
            string bl4 = txtBl4.Text.Trim() == "" ? "0" : txtBl4.Text.Trim();
            string bl = bl1 + "|" + bl2 + "|" + bl3 + "|" + bl4;
            if (dep != "00" && pos != "00" && name != "")
            {
                if (action == "add")
                {
                    if (Det_Repeater.Items.Count != 0)
                    {
                        string fkey = DateTime.Now.ToString("yyyyMMddhhmmss");


                        string sql = string.Format("insert into TBDS_KaoHeMBList(kh_Dep,Kh_Type,kh_Fkey,kh_AddId,kh_Time,kh_State,kh_Name,kh_BL,kh_Type1,kh_Note) values('{0}','{1}','{2}','{3}','{4}','0','{5}','{6}','{7}','{8}')", dep, pos, fkey, hidAddPer.Value, DateTime.Now.ToString("yyyy-MM-dd"), name, bl, ddlType.SelectedValue, txtPFBZ.Text);
                        list.Add(sql);
                        List<string> col = new List<string>();
                        for (int i = 1; i < 6; i++)
                        {
                            col.Add(((ITextControl)Det_Repeater.Controls[0].FindControl("kh_Col" + i)).Text);
                        }
                        sql = string.Format("insert into TBDS_KaoHeCol values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')", col[0], col[1], col[2], col[3], col[4], "", "", "", "", "", "", "", fkey);
                        list.Add(sql);
                        double sum = 0;
                        for (int i = 0; i < Det_Repeater.Items.Count; i++)
                        {
                            List<string> txt = new List<string>();
                            for (int j = 1; j < 7; j++)
                            {
                                txt.Add(((ITextControl)Det_Repeater.Items[i].FindControl("txt" + j)).Text.Replace(Convert.ToString((char)13), "<br />"));
                            }
                            sql = string.Format("insert into TBDS_KaoHeMuB values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", i + 1, fkey, txt[0], txt[1], txt[2], txt[3], txt[4], "", "", "", "", "", "", "", txt[5]);
                            list.Add(sql);
                            sum += CommonFun.ComTryDouble(txt[5]);
                        }
                        if (sum != 100)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('子项权重之和需等于100！')", true); return;
                        }
                        DBCallCommon.ExecuteTrans(list);
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('添加成功！');window.location.href='OM_KaoHeMuBList.aspx'", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "2", "alert('未添加任何行！');", true);
                    }
                }
                else if (action == "edit")
                {

                    string sql = string.Format("update TBDS_KaoHeMBList set kh_Dep='{0}',kh_AddId='{1}',kh_Time='{2}',kh_name='{3}',kh_bl='{4}',kh_Type1='{5}',kh_Note='{7}' where kh_Fkey='{6}'", ddl_Depart.SelectedValue.ToString(), hidAddPer.Value, DateTime.Now.ToString("yyyy-MM-dd"), txtName.Text.Trim(), bl, ddlType.SelectedValue, key, txtPFBZ.Text);
                    list.Add(sql);
                    List<string> col = new List<string>();
                    for (int i = 1; i < 6; i++)
                    {
                        col.Add(((ITextControl)Det_Repeater.Controls[0].FindControl("kh_Col" + i)).Text);
                    }
                    sql = string.Format("update TBDS_KaoHeCol set kh_Col1='{0}',kh_Col2='{1}',kh_Col3='{2}',kh_Col4='{3}',kh_Col5='{4}',kh_Col6='{5}',kh_Col7='{6}',kh_Col8='{7}',kh_Col9='{8}',kh_Col10='{9}',kh_Col11='{10}',kh_Col12='{11}' where kh_fkey='{12}'", col[0], col[1], col[2], col[3], col[4], "", "", "", "", "", "", "", key);
                    list.Add(sql);
                    sql = "delete from TBDS_KaoHeMuB where kh_Fkey='" + key + "'";
                    list.Add(sql);
                    for (int i = 0; i < Det_Repeater.Items.Count; i++)
                    {
                        List<string> txt = new List<string>();
                        for (int j = 1; j < 7; j++)
                        {
                            txt.Add(((ITextControl)Det_Repeater.Items[i].FindControl("txt" + j)).Text);
                        }
                        sql = string.Format("insert into TBDS_KaoHeMuB values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')", i + 1, key, txt[0], txt[1], txt[2], txt[3], txt[4], "", "", "", "", "", "", "", txt[5]);
                        list.Add(sql);
                    }
                    DBCallCommon.ExecuteTrans(list);
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('编辑成功！');window.location.href='OM_KaoHeMuBList.aspx'", true);
                }
            }
            else
            {
                //Response.Write("<script>alert('请检查部门和职位！');</script>");
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请检查部门、职位和模板名称！');", true);
            }
        }


    }
}
