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
    public partial class YS_Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            if (!IsPostBack)
            {
                InitUrl();
            }

        }

        private void InitUrl()
        {
            HyperLink1.NavigateUrl = "YS_Cost_Budget_View.aspx";
            HyperLink2.NavigateUrl = "YS_Cost_Budget_Audit_View.aspx";
            HyperLink3.NavigateUrl = "YS_Cost_Budget_A_M.aspx?type=1";
            HyperLink7.NavigateUrl = "YS_Cost_Budget_A_M.aspx?type=0";
            HyperLink5.NavigateUrl = "YS_Cost_Real_View.aspx";
            HyperLink6.NavigateUrl = "YS_Product_type.aspx?action=null";
            HyperLink4.NavigateUrl = "YS_Cost_Budget_O_M.aspx";
        }
    }
}
