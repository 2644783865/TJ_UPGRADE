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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CanBuYDADD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CreateNewRow(12);
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
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();
            InitVar(col);
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("cbyd_name");
            dt.Columns.Add("cbyd_stid");
            dt.Columns.Add("cbyd_gh");
            dt.Columns.Add("cbyd_bm");

            dt.Columns.Add("cbyd_tzts");
            dt.Columns.Add("cbyd_cbbz");

            dt.Columns.Add("cbyd_bf");
            dt.Columns.Add("cbyd_note");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                DataRow newRow = dt.NewRow();
                for (int i = 1; i < 9; i++)
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
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            List<string> col = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("cbyd_name");
            dt.Columns.Add("cbyd_stid");
            dt.Columns.Add("cbyd_gh");
            dt.Columns.Add("cbyd_bm");

            dt.Columns.Add("cbyd_tzts");
            dt.Columns.Add("cbyd_cbbz");

            dt.Columns.Add("cbyd_bf");
            dt.Columns.Add("cbyd_note");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    for (int i = 1; i < 9; i++)
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

        protected void Textname_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            TextBox Tb_newstid = (TextBox)sender;
            RepeaterItem Reitem = (RepeaterItem)Tb_newstid.Parent;

            if (num > 0)
            {
                string stid = (sender as TextBox).Text.Trim().Substring(0, num);

                string sqlText = "select * from View_TBDS_STAFFINFO where ST_ID='" + stid + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                if (dt.Rows.Count > 0)
                {
                    ((TextBox)Reitem.FindControl("txt2")).Text = stid;
                    ((TextBox)Reitem.FindControl("txt1")).Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txt3")).Text = dt.Rows[0]["ST_WORKNO"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txt4")).Text = dt.Rows[0]["DEP_NAME"].ToString().Trim();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员不存在，请重新输入！');", true);
                }
            }
        }


        protected void btnsave_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string bh = "CBYD" + DateTime.Now.ToString("yyyyMMddHHmmss").Trim();
            string stryearmonth = "";
            int num = 0;
            if (tb_yearmonth.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请选择年月！');", true);
                return;
            }
            for (int i = 0; i < Det_Repeater.Items.Count; i++)
            {
                if (((TextBox)Det_Repeater.Items[i].FindControl("txt2")).Text.Trim() != "" && ((TextBox)Det_Repeater.Items[i].FindControl("txt1")).Text.Trim() != "")
                {
                    num++;
                }
            }
            if (num == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请填写要调整的数据！');", true);
                return;
            }
            else
            {
                string sqlText = "select * from OM_CanBu where CB_YearMonth ='" + tb_yearmonth.Text.Trim() + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                if (dt.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月餐补数据未生成！');", true);
                    return;
                }

                for (int j = 0; j < Det_Repeater.Items.Count; j++)
                {
                    if (((TextBox)Det_Repeater.Items[j].FindControl("txt2")).Text.Trim() != "" && ((TextBox)Det_Repeater.Items[j].FindControl("txt1")).Text.Trim() != "")
                    {
                        stryearmonth = tb_yearmonth.Text.Trim();
                        TextBox txt2 = (TextBox)Det_Repeater.Items[j].FindControl("txt2");
                        TextBox txt5 = (TextBox)Det_Repeater.Items[j].FindControl("txt5");
                        TextBox txt6 = (TextBox)Det_Repeater.Items[j].FindControl("txt6");
                        TextBox txt7 = (TextBox)Det_Repeater.Items[j].FindControl("txt7");
                        TextBox txt8 = (TextBox)Det_Repeater.Items[j].FindControl("txt8");

                        string sql = "insert into OM_CanBuYDdetal(CBYD_SPBH,CBYD_YearMonth,CBYD_STID,CBYD_TZTS,CBYD_CBBZ,CBYD_BF,CBYD_Note) values('" + bh + "','" + stryearmonth + "','" + txt2.Text.Trim() + "'," + CommonFun.ComTryDecimal(txt5.Text.Trim()) + "," + CommonFun.ComTryDecimal(txt6.Text.Trim()) + "," + CommonFun.ComTryDecimal(txt7.Text.Trim()) + ",'" + txt8.Text.Trim() + "')";
                        list.Add(sql);
                    }
                }
                string sqlinsertsp = "insert into OM_CanBuYDSP(SPBH,YearMonth,SQR_ID,SQR_Name,SQ_Time) values('" + bh + "','" + stryearmonth + "','" + Session["UserID"].ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "')";
                list.Add(sqlinsertsp);
                DBCallCommon.ExecuteTrans(list);
                Response.Redirect("OM_CanBuYDSPdetail.aspx?spid=" + bh);
            }
        }
    }
}
