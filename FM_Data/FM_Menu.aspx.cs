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

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_Menu : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.IsNewSession)
            {
                Session.Abandon();

                Application.Lock();
                Application["online"] = (int)Application["online"] - 1;
                Application.UnLock();
                Response.Write("<script>if(window.parent!=null)window.parent.location.href='../Default.aspx';else{window.location.href='./Default.aspx'} </script>");

            }
            if (!IsPostBack)
            {
                InitUrl();
                
            }

            CheckUser(ControlFinder);
           
        }
        private void InitUrl()
        {
           
            HyperLink1.NavigateUrl = "FM_Invoice_Managemnt.aspx";
            HyperLink9.NavigateUrl = "FM_wxInvoice_Managemnt.aspx";
            HyperLink2.NavigateUrl = "FM_RuKu_Adjust_Accounts.aspx";
            HyperLink3.NavigateUrl = "FM_ZanGu_RuKu_Manage.aspx";
            HyperLink4.NavigateUrl = "FM_YueMo_ChuKu_Adjust_Accounts.aspx";
            HyperLink5.NavigateUrl = "FM_ProductNumber_Statistics.aspx";
            HyperLink6.NavigateUrl = "FM_KuCunYuEChaXun.aspx";
            HyperLink7.NavigateUrl = "FM_ProCompelete.aspx";
            HyperLink8.NavigateUrl = "CWCB_Hesuan.aspx";
            HyperLink10.NavigateUrl = "CM_ProNum_ProfitLoss.aspx?Action=Edit";
            HyperLink11.NavigateUrl = "CB_MothSummary.aspx";
            HyperLink12.NavigateUrl = "FM_SaleCostSta.aspx";
            HyperLink13.NavigateUrl = "FM_RuKu_Adjust_Accounts_Diff.aspx";
            HyperLink14.NavigateUrl = "FM_YueMo_ChuKu_Accounting.aspx";
            HyperLink15.NavigateUrl = "FM_ZanGuMingXiChaXu.aspx";
            HyperLink16.NavigateUrl = "FM_KCYECX.aspx";
            HyperLink17.NavigateUrl = "FM_CBTZD.aspx";
            HyperLink18.NavigateUrl = "FM_Period_Begin_Adjust.aspx";
            HyperLink19.NavigateUrl = "FM_ZCFZ.aspx";
            HyperLink20.NavigateUrl = "FM_LRFP.aspx";
            HyperLink21.NavigateUrl = "FM_XJLL.aspx";
            HyperLink22.NavigateUrl = "FM_CWFX.aspx";
            HyperLink23.NavigateUrl = "FM_WXHS.aspx";
            HyperLink24.NavigateUrl = "FM_WXHZ.aspx";
            HyperLink25.NavigateUrl = "FM_WXDIF.aspx";
            HyperLink26.NavigateUrl = "FM_YFInvoice_Managemnt.aspx";
            HyperLink27.NavigateUrl = "FM_YFHS.aspx";
            HyperLink28.NavigateUrl = "FM_YFHZ.aspx";
            HyperLink29.NavigateUrl = "FM_YFDIF.aspx";
            HyperLink30.NavigateUrl = "~/CM_Data/CM_Kaipiao_List.aspx";
            HyperLink31.NavigateUrl = "FM_JJZBTJ.aspx";
            HyperLink32.NavigateUrl = "FM_HTHCEHNBFX.aspx";
            HyperLink33.NavigateUrl = "FM_HTZHIBIAO.aspx";
            HyperLink34.NavigateUrl = "FM_CWZHIBIAO.aspx";
            HyperLink35.NavigateUrl = "FM_HTZHIBIAOANY.aspx";
            HyperLink36.NavigateUrl = "FM_CWZHIBIAOANY.aspx";
            HyperLink37.NavigateUrl = "FM_CNFB.aspx";
        }
    }
}
