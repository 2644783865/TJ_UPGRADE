using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_Driver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = "select * from TBOM_DriverList as a left join View_TBDS_STAFFINFO as b on a.DriverId=ST_ID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            if (((LinkButton)sender).CommandName == "Del")
            {
                string sqltext = "delete from TBOM_DriverList where Context='" + id + "'";
                DBCallCommon.ExeSqlText(sqltext);
                sqltext = "delete from TBOM_DriverInfo where Context='" + id + "'";
                DBCallCommon.ExeSqlText(sqltext);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已删除！！！');", true);
            }
        }



        protected void grid_databound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string context = ((HtmlInputHidden)e.Row.FindControl("hidContext")).Value;
                string time = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
                string sql = "select count(1) from TBOM_DriverInfo  where (dENDDATE<='" + time + "' and context='" + context + "')";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows[0][0].ToString() != "0")
                {
                    e.Row.Cells[0].BackColor = Color.Red;
                    e.Row.Cells[0].ToolTip = "该司机有证件需要更新！";
                }
            }
        }
    }
}
