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

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_Menu : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            if (!IsPostBack)
            {
                if (Session.IsNewSession)
                {
                    Session.Abandon();
                    Application.Lock();
                    Application["online"] = (int)Application["online"] - 1;
                    Application.UnLock();
                    Response.Write("<script>if(window.parent!=null)window.parent.location.href='../Default.aspx';else{window.location.href='./Default.aspx'} </script>");
                }
                InitUrl();
                CheckUser(ControlFinder);
            }
            GetMyTouBiao();
        }
        private void InitUrl()
        {
            //HyperLink1.NavigateUrl = "pjinfo.aspx";
            //HyperLink2.NavigateUrl = "enginfo.aspx";
            //HyperLink3.NavigateUrl = "CM_Bus_Contract.aspx";
            HyperLink1.NavigateUrl = "PD_DocManage.aspx";
            //HyperLink3.NavigateUrl = "CM_Bus_Contract.aspx";
            //HyperLink4.NavigateUrl = "PD_DocManage.aspx";
            //HyperLink5.NavigateUrl = "PD_DocPinshenManage.aspx";
            HyperLink2.NavigateUrl = "TM_HZY_info.aspx";
            HyperLink7.NavigateUrl = "CM_Notice_Main.aspx";
            //HyperLink8.NavigateUrl = "";
            //HyperLink9.NavigateUrl = "BD_Accessory_List.aspx";
            //HyperLink10.NavigateUrl = "BD_Accessory_List.aspx";
            //HyperLink11.NavigateUrl = "BD_Accessory_List.aspx";
            //HyperLink12.NavigateUrl = "BD_Accessory_List.aspx";
            //HyperLink13.NavigateUrl = "BD_Accessory_List.aspx";
            //HyperLink14.NavigateUrl = "BD_Accessory_List.aspx";
        }

        /*********************得到任务的数量*************************/

        private void GetMyTouBiao()
        {
            string userID = Session["UserID"].ToString();

            string sqlText = "select count(*) from TBBS_CONREVIEW where BP_STATUS ='0' AND BP_YESORNO='Y' and ((BC_REVIEWERA='" + userID + "' AND  (BP_RVIEWA=''or BP_RVIEWA is null)) or (BC_DRAFTER='" + userID + "') or (BC_REVIEWERB='" + userID + "' AND  (BP_RVIEWB='' or BP_RVIEWB is null)) or (BC_REVIEWERC='" + userID + "' AND  (BP_RVIEWC='' or BP_RVIEWC is null)) or (BC_REVIEWERD='" + userID + "' AND (BP_RVIEWD='' or BP_RVIEWD is null)) or (BC_REVIEWERE='" + userID + "' AND  (BP_RVIEWE='' or BP_RVIEWE is null)) or (BC_REVIEWERF='" + userID + "' AND  (BP_RVIEWF='' or BP_RVIEWF is null)) or (BC_REVIEWERG='" + userID + "' AND  (BP_RVIEWG='' or BP_RVIEWG is null)) OR (BC_REVIEWERH='" + userID + "' AND  (BP_RVIEWH='' or BP_RVIEWH is null)) OR (BC_REVIEWERI='" + userID + "' AND  (BP_RVIEWI='' or BP_RVIEWI is null)) OR (BC_REVIEWERJ='" + userID + "' AND  (BP_RVIEWJ='' or BP_RVIEWJ is null)))";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    lb_toubiao.Visible = false;
                }
                else
                {
                    lb_toubiao.Visible = true;
                    lb_toubiao.Text = "(" + dr[0].ToString() + ")";
                }
            }
            dr.Close();
        }
    }
}
