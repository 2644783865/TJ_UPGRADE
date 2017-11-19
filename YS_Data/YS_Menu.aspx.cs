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
        string  depId, position;

        protected void Page_Load(object sender, EventArgs e)
        {
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            depId = Session["UserDeptID"].ToString();
            position = Session["POSITION"].ToString();
            if (!IsPostBack)
            {
                InitUrl();
            }

        }

        private void InitUrl()
        {
            HyperLink1.NavigateUrl = "ys_task_budget_list.aspx";
            HyperLink2.NavigateUrl = "ys_contract_budget_list.aspx";
            HyperLink3.NavigateUrl = "YS_Cost_A_M_CON.aspx";
            HyperLink7.NavigateUrl = "YS_Cost_Budget_A_M_CON.aspx?type=0";
            HyperLink5.NavigateUrl = "YS_Cost_Real_View.aspx";            
            //HyperLink4.NavigateUrl = "YS_Cost_Budget_O_M.aspx";
            HyperLink6.NavigateUrl = "YS_Cost_Real_Sta.aspx";
        }

    }
}
