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
            string sql = "select * from YS_COST_BUDGET";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            rpt_YS_FERROUS_METAL.DataSource = dt;
            rpt_YS_FERROUS_METAL.DataBind();
           
        }
    }
}
