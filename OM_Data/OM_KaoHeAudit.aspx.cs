using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_KaoHeAudit : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {

                databind();
            }
            CheckUser(ControlFinder);
        }

        #region 初始化分页

        void Pager_PageChanged(int pageNumber)
        {
            databind();
        }

        private void databind()
        {
            pager.TableName = "View_TBDS_KaoHe";
            pager.PrimaryKey = "kh_Id";
            pager.ShowFields = "*";
            pager.OrderField = "kh_Time";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;
            pager.PageSize = 100;
            UCPaging1.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, rep_Kaohe, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        private string strWhere()
        {
            string Id = Session["UserId"].ToString();
            string strWhere = " (kh_state='1' and kh_Id='" + Id + "') or (kh_state='2' and Kh_SPRA='" + Id + "') or (kh_state='3' and Kh_SPRB='" + Id + "') or (kh_state='4' and Kh_SPRC='" + Id + "') or (kh_state='5' and Kh_SPRD='" + Id + "')  or (kh_state='6' and kh_Id='" + Id + "') or ((kh_state='7' or kh_state='6') and Kh_shrid1='" + Id + "' and kh_shstate1='0' and Kh_shtoltalstate='1') or ((kh_state='7' or kh_state='6') and kh_shstate1='1' and Kh_shrid2='" + Id + "' and kh_shstate2='0' and Kh_shtoltalstate='1') or ((kh_state='7' or kh_state='6') and kh_shstate2='1' and Kh_shrid3='" + Id + "' and kh_shstate3='0' and Kh_shtoltalstate='1') or ((kh_state='7' or kh_state='6') and kh_shstate3='1' and Kh_shrid4='" + Id + "' and kh_shstate4='0' and Kh_shtoltalstate='1')";
            return strWhere;
        }

        #endregion
    }
}
