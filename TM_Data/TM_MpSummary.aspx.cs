using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MpSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            if (!IsPostBack)
            {
                string retvalue=this.GetQueryCondition();
                if (retvalue== "OK")
                {
                    this.InitVarBindData();
                }
                else if (retvalue == "Null")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法获取查询对象，请重新操作');window.close();", true);
                }
            }
        }

        protected string GetQueryCondition()
        {
            string sql = "select WhereCondition from TBPM_TEMPWHERECONDITION where USERID='"+Session["UserID"].ToString()+"'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                ViewState["Where"] = dt.Rows[0][0].ToString();
                string sql_delete = "delete from TBPM_TEMPWHERECONDITION where USERID='" + Session["UserID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql_delete);
                return "OK";
            }
            else
            {
                return "Null";
            }
        }

        protected void InitVarBindData()
        {
            string _table_where = ViewState["Where"].ToString().Replace("^","'");
            string[] a = _table_where.Split('$');
            string _table = a[0];
            string _where = a[1];
            string sql = "select * from "+_table+" where  "+_where+" order by "+ddlSort.SelectedValue+" "+ddlSortOrder.SelectedValue+"";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                NoDataPanel1.Visible = false;
            }
            else
            {
                NoDataPanel1.Visible = true;
            }
        }
        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitVarBindData();
        }

        protected double changdu = 0;//14
        protected double kuandu = 0;//15
        protected double cailiaodz = 0;//17
        protected double cailiaozongzhong = 0;//18
        protected double cailiaozongchang = 0;//19
        protected double my = 0;//20
        protected double mpmy = 0;//21
        protected double singnumber = 0;//23
        protected double number = 0;//24
        protected double totalnumber = 0;//25
        protected double dz = 0;//26
        protected double zongzhong = 0;//27

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                changdu += Convert.ToDouble(e.Row.Cells[14].Text == "&nbsp;" ? "0" : e.Row.Cells[14].Text);
                kuandu += Convert.ToDouble(e.Row.Cells[15].Text == "&nbsp;" ? "0" : e.Row.Cells[15].Text);
                cailiaodz += Convert.ToDouble(e.Row.Cells[17].Text == "&nbsp;" ? "0" : e.Row.Cells[17].Text);
                cailiaozongzhong += Convert.ToDouble(e.Row.Cells[18].Text == "&nbsp;" ? "0" : e.Row.Cells[18].Text);
                cailiaozongchang += Convert.ToDouble(e.Row.Cells[19].Text == "&nbsp;" ? "0" : e.Row.Cells[19].Text);
                my += Convert.ToDouble(e.Row.Cells[20].Text == "&nbsp;" ? "0" : e.Row.Cells[20].Text)*Convert.ToDouble(e.Row.Cells[25].Text == "&nbsp;" ? "0" : e.Row.Cells[25].Text);
                mpmy += Convert.ToDouble(e.Row.Cells[21].Text == "&nbsp;" ? "0" : e.Row.Cells[21].Text) * Convert.ToDouble(e.Row.Cells[25].Text == "&nbsp;" ? "0" : e.Row.Cells[25].Text);
                singnumber += Convert.ToDouble(e.Row.Cells[23].Text == "&nbsp;" ? "0" : e.Row.Cells[23].Text);
                number += Convert.ToDouble(e.Row.Cells[24].Text == "&nbsp;" ? "0" : e.Row.Cells[24].Text);
                totalnumber += Convert.ToDouble(e.Row.Cells[25].Text == "&nbsp;" ? "0" : e.Row.Cells[25].Text);
                dz += Convert.ToDouble(e.Row.Cells[26].Text == "&nbsp;" ? "0" : e.Row.Cells[26].Text);
                zongzhong += Convert.ToDouble(e.Row.Cells[27].Text == "&nbsp;" ? "0" : e.Row.Cells[27].Text);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[13].Text = "合计：";
                e.Row.Cells[14].Text = changdu.ToString("0.00");
                e.Row.Cells[15].Text = kuandu.ToString("0.00");
                e.Row.Cells[17].Text = cailiaodz.ToString("0.00");
                e.Row.Cells[18].Text = cailiaozongzhong.ToString("0.00");
                e.Row.Cells[19].Text = cailiaozongchang.ToString("0.00");
                e.Row.Cells[20].Text = my.ToString("0.00");
                e.Row.Cells[21].Text = mpmy.ToString("0.00");
                e.Row.Cells[23].Text = singnumber.ToString("0.00");
                e.Row.Cells[24].Text = number.ToString("0.00");
                e.Row.Cells[25].Text = totalnumber.ToString("0.00");
                e.Row.Cells[26].Text = dz.ToString("0.00");
                e.Row.Cells[27].Text = zongzhong.ToString("0.00");
            }
        }
    }
}
