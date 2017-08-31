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

            SetControlAmount(MyViewTask, GetApprovelsAmount(position));
            SetControlAmount(lab_view, GetEditAmount(depId, position));
        }

        private void InitUrl()
        {
            HyperLink1.NavigateUrl = "YS_Cost_Budget_View.aspx?type=0";
            HyperLink2.NavigateUrl = "YS_Cost_Budget_View.aspx?type=1";
            HyperLink3.NavigateUrl = "YS_Cost_Budget_A_M.aspx?type=1";
            HyperLink7.NavigateUrl = "YS_Cost_Budget_A_M.aspx?type=0";
            HyperLink5.NavigateUrl = "YS_Cost_Real_View.aspx";
            //HyperLink6.NavigateUrl = "YS_Product_type.aspx?action=null";
            HyperLink4.NavigateUrl = "YS_Cost_Budget_O_M.aspx";
        }

        protected int GetEditAmount(string depId, string posi)
        {
            switch (depId)
            {
                case "06"://财务
                    switch (posi)
                    {
                        case "0601":
                            return GetAmount("YS_CAIWU=1");
                        default:
                            return GetAmount("YS_STATE=1 OR YS_STATE=0");
                    };
                case "05"://采购
                    return GetAmount("YS_CAIGOU=1");
                case "04"://生产
                    return GetAmount("YS_SHENGCHAN=1");
                default:
                    return 0;
            }
        }


        /// <summary>
        /// 判断预算审批的待处理数
        /// </summary>
        /// <param name="posi">职位</param>
        /// <returns>待处理数</returns>
        protected int GetApprovelsAmount(string posi)
        {
            switch (posi)
            {
                case "0102"://副总经理
                    return GetAmount("YS_FIRST_REVSTATE=1");
                case "0101"://总经理
                    return GetAmount("YS_SECOND_REVSTATE=1");
                default:
                    return 0;
            }
        }


        /// <summary>
        /// 从预算主表中查询到符合查询条件的记录的总数
        /// </summary>
        /// <param name="sqlWhere">查询条件，不含where</param>
        /// <param name="amount">将符合条件的总数返回到一个变量中</param>
        protected int GetAmount(string sqlWhere)
        {
            int i = 0;
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(string.Format("SELECT COUNT(*) FROM dbo.YS_COST_BUDGET WHERE {0} ;", sqlWhere));
            if (dr.Read())
            {
                int.TryParse(dr[0].ToString(), out i);
            }
            dr.Close();
            return i;
        }

        /// <summary>
        /// 设置控件及其待处理数
        /// </summary>
        /// <param name="lb">控件id</param>
        /// <param name="i">待处理数</param>
        protected void SetControlAmount(Label lb, int i)
        {
            if (i > 0)
            {
                lb.Visible = true;
                lb.Text = "(" + i.ToString() + ")";
            }
            else
            {
                lb.Visible = false;
            }

        }


    }
}
