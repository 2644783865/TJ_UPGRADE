using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_EATNEW : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CreateNewRow(2);
                binddata();
            }
        }



        private void binddata()
        {
            lbsqdate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim();
            txt_apply.Text = Session["UserName"].ToString().Trim();
        }



        #region 增加删除行

        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            List<string> col = new List<string>();
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();
            InitVar(col);
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("detailthing");
            dt.Columns.Add("detailclass");
            dt.Columns.Add("detailnum");
            dt.Columns.Add("detailunit");
            dt.Columns.Add("detailprice");
            dt.Columns.Add("detailmoney");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                DataRow newRow = dt.NewRow();
                for (int i = 1; i < 6; i++)
                {
                    newRow[i - 1] = ((TextBox)retItem.FindControl("txt" + i)).Text;
                }
                newRow[5] = ((Label)retItem.FindControl("moneyone")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        private void InitVar(List<string> col)
        {
            if (Det_Repeater.Items.Count == 0)
            {
                delete.Visible = false;
            }
            else
            {
                delete.Visible = true;
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            List<string> col = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("detailthing");
            dt.Columns.Add("detailclass");
            dt.Columns.Add("detailnum");
            dt.Columns.Add("detailunit");
            dt.Columns.Add("detailprice");
            dt.Columns.Add("detailmoney");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    for (int i = 1; i < 6; i++)
                    {
                        newRow[i - 1] = ((TextBox)retItem.FindControl("txt" + i)).Text;
                    }
                    newRow[5] = ((Label)retItem.FindControl("moneyone")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();
            InitVar(col);
        }

        #endregion
        protected void btnsave_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            int num = 0;
            if (tb_ycdate.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请选择用餐时间！');", true);
                return;
            }
            if (rad_yctype.SelectedValue != "1" && rad_yctype.SelectedValue != "2")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请选择用餐类型！');", true);
                return;
            }
            if (tbycrens.Text.Trim() == "" || tbycguige.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请填写用餐规格和用餐人数！');", true);
                return;
            }
            string strspbh = "";
            string sqlzdbh = "select max(eatbh) as eatbhmax from OM_EATNEW where eatbh like '" + DateTime.Now.ToString("yyyyMMdd").Trim() + "%'";
            System.Data.DataTable dtzdbh = DBCallCommon.GetDTUsingSqlText(sqlzdbh);
            if (dtzdbh.Rows.Count > 0 && dtzdbh.Rows[0]["eatbhmax"].ToString().Trim().Length >= 12)
            {
                strspbh = DateTime.Now.ToString("yyyyMMdd").Trim() + "-" + (CommonFun.ComTryInt(dtzdbh.Rows[0]["eatbhmax"].ToString().Trim().Substring(9, 3)) + 1).ToString("000").Trim();
            }
            else
            {
                strspbh = DateTime.Now.ToString("yyyyMMdd").Trim() + "-" + "001";
            }

            string sqlinsertsh = "insert into OM_EATNEW(eatbh,eatyctime,eattype,eatsqrid,eatsqrname,eatsqtime,eatsqrphone,eatsqrnote) values('" + strspbh + "','" + tb_ycdate.Text.Trim() + "','" + rad_yctype.SelectedValue.ToString().Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + lbsqdate.Text.Trim() + "','" + txt_phone.Text.Trim() + "','" + txt_contents.Text.Trim() + "')";
            list.Add(sqlinsertsh);
            string sqlinsertyc = "insert into OM_EATNEWDETAIL(detailbh,detailnum,detailprice,detailifYP) values('" + strspbh + "'," + CommonFun.ComTryDecimal(tbycrens.Text.Trim()) + "," + CommonFun.ComTryDecimal(tbycguige.Text.Trim()) + ",'2')";
            list.Add(sqlinsertyc);
            for (int j = 0; j < Det_Repeater.Items.Count; j++)
            {
                if (CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[j].FindControl("txt3")).Text.Trim()) > 0 || CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[j].FindControl("txt5")).Text.Trim()) > 0)
                {
                    TextBox txt1 = (TextBox)Det_Repeater.Items[j].FindControl("txt1");
                    TextBox txt2 = (TextBox)Det_Repeater.Items[j].FindControl("txt2");
                    TextBox txt3 = (TextBox)Det_Repeater.Items[j].FindControl("txt3");
                    TextBox txt4 = (TextBox)Det_Repeater.Items[j].FindControl("txt4");
                    TextBox txt5 = (TextBox)Det_Repeater.Items[j].FindControl("txt5");
                    string sqlinsertdetail = "insert into OM_EATNEWDETAIL(detailbh,detailthing,detailclass,detailnum,detailunit,detailprice,detailifYP) values('" + strspbh + "','" + txt1.Text.Trim() + "','" + txt2.Text.Trim() + "'," + CommonFun.ComTryDecimal(txt3.Text.Trim()) + ",'" + txt4.Text.Trim() + "'," + CommonFun.ComTryDecimal(txt5.Text.Trim()) + ",'1')";
                    list.Add(sqlinsertdetail);
                }
            }
            DBCallCommon.ExecuteTrans(list);
            Response.Redirect("OM_EATSPdetailnew.aspx?spid=" + strspbh); 
        }
    }
}
