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
        
        string depId, position, userid;

        protected void Page_Load(object sender, EventArgs e)
        {
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            depId = Session["UserDeptID"].ToString();
            position = Session["POSITION"].ToString();
            userid = Session["UserID"].ToString();
            if (!IsPostBack)
            {
                InitUrl();
            }
            initNum();


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

        /// <summary>
        /// 设置任务预算编制数目
        /// </summary>
        private void initNum()
        {
            int num = getActiveNodeInstanceNum(string.Format("SELECT COUNT(DISTINCT(task_code)) FROM dbo.YS_NODE_INSTANCE WHERE user_id={0} AND (state=1 OR state=3);", Session["UserId"]));            
            lb_num.Text = "（" + num + "）";
            lb_num.Visible=hasNum(num);
        }

        /// <summary>
        /// 判断是否有数目
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private bool hasNum(int num)
        {           
            if (num.Equals(0))
                return false;
            else
                return true;
        }
        /// <summary>
        /// 查询数目
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private int getActiveNodeInstanceNum(string sql)
        {
            int num;
            int.TryParse(BudgetFlowEngine.getFirstCellStringByDR(sql), out num);
            return num;
        }




    }
}
