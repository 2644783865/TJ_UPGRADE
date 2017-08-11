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
using System.Data.SqlClient;


namespace ZCZJ_DPF.YS_Data
{
    public partial class YS_Cost_Budget_Add_Detail : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //头部任务号相关信息
            string tsaId = Request.QueryString["tsaId"].ToString();
            string sql = "select * from YS_COST_BUDGET where YS_TSA_ID='" + tsaId + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            lb_YS_CONTRACT_NO.Text = dt.Rows[0]["YS_CONTRACT_NO"].ToString();
            lb_YS_PROJECTNAME.Text = dt.Rows[0]["YS_PROJECTNAME"].ToString();
            lb_YS_TSA_ID.Text = tsaId;

            //黑色金属相关信息
            string sql1 = @"select YS_CODE ,YS_NAME ,YS_Union_Amount ,YS_Average_Price,YS_Average_Price_FB from  YS_COST_BUDGET_DETAIL 
where YS_TSA_ID='" + tsaId + "' AND YS_CODE LIKE '01.07%' ORDER BY YS_CODE";

            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
            rpt_YS_FERROUS_METAL.DataSource = dt1;
            rpt_YS_FERROUS_METAL.DataBind();
        }

        public string GetProduct(string n1, string n2)
        {
            return (Convert.ToDouble(n1) * Convert.ToDouble(n2)).ToString("0.00");
        }
    }
}
