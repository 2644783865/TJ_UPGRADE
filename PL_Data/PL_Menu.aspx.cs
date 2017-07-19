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

namespace ZCZJ_DPF.PL_Data
{
    public partial class PL_Menu : BasicPage
    {
        string sqlText = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                InitUrl();

            }
            GetInnerAudit();
            GetGroupAudit();
            GetExterAudit();
            GetTargetAnalyze();
            CheckUser(ControlFinder);
        }

        private void InitUrl()
        {
            //HyperLink5.NavigateUrl = "~/PL_Data/MainPlan_View.aspx";
        }

        //private void GetNotMakePlan() {
        //    sqlText = "select count(1) from TBMP_MAINPLANTOTAL where MP_STATE='0'";
        //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
        //    string count = dt.Rows[0][0].ToString();
        //    if (count!="0")
        //    {
        //        lblPlan.Text = "(" + count + ")";
        //    }
        //}

        private void GetTargetAnalyze()
        {
            string monthvalid = "";
            if (DateTime.Now.Month.ToString() != "1")
            {
                sqlText = "select b.* from TBQC_TARGET_LIST as a left join TBQC_TARGET_DETAIL as b on a.TARGET_ID=b.TARGET_FID where a.TARGET_NAME like '" + DateTime.Now.Year.ToString() + "%'and b.TARGET_DEPID='" + Session["UserDeptID"].ToString() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    monthvalid = monthvalid + dt.Rows[i][DateTime.Now.Month + 3].ToString();
                }
                if (DateTime.Now.Day > 0 && DateTime.Now.Day < 11)
                    if (string.IsNullOrEmpty(monthvalid))
                        lblTargetAnalyze.Text = "(1)";
            }
            else
            {
                sqlText = "select b.* from TBQC_TARGET_LIST as a left join TBQC_TARGET_DETAIL as b on a.TARGET_ID=b.TARGET_FID where a.TARGET_NAME like '" + (DateTime.Now.Year - 1).ToString() + "%'and b.TARGET_DEPID='" + Session["UserDeptID"].ToString() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    monthvalid = monthvalid + dt.Rows[i][DateTime.Now.Month + 15].ToString();
                }
                if (DateTime.Now.Day > 0 && DateTime.Now.Day < 11)
                    if (string.IsNullOrEmpty(monthvalid))
                        lblTargetAnalyze.Text = "(1)";
            }
        }

        private void GetInnerAudit()
        {
            sqlText = "select count(1) from TBQC_INTERNAL_AUDIT where PRO_TYPE='inner' and  ((PRO_STATE='1' and PRO_SPR='" + Session["UserID"].ToString() + "' ) or( PRO_STATE='2' and PRO_ZGR='" + Session["UserID"].ToString() + "') or (PRO_STATE='4' and PRO_SPR='" + Session["UserID"].ToString() + "') or (PRO_STATE='5' and PRO_SHY='" + Session["UserID"].ToString() + "'))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            string count = dt.Rows[0][0].ToString();
            if (count != "0")
            {
                lblInnerAudit.Text = "(" + count + ")";
            }
        }
        private void GetGroupAudit()
        {
            sqlText = "select count(1) from TBQC_INTERNAL_AUDIT where PRO_TYPE='group' and  ((PRO_STATE='1' and PRO_SPR='" + Session["UserID"].ToString() + "' ) or( PRO_STATE='2' and PRO_ZGR='" + Session["UserID"].ToString() + "') or (PRO_STATE='4' and PRO_SPR='" + Session["UserID"].ToString() + "') or (PRO_STATE='5' and PRO_SHY='" + Session["UserID"].ToString() + "'))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            string count = dt.Rows[0][0].ToString();
            if (count != "0")
            {
                lblGroupAudit.Text = "(" + count + ")";
            }
        }
        private void GetExterAudit()
        {
            sqlText = "select count(1) from TBQC_EXTERNAL_AUDIT where  ((PRO_STATE='1' and PRO_SPR='" + Session["UserID"].ToString() + "' ) or( PRO_STATE='2' and PRO_ZGR='" + Session["UserID"].ToString() + "') or (PRO_STATE='4' and PRO_SPR='" + Session["UserID"].ToString() + "') or (PRO_STATE='5' and PRO_SHY='" + Session["UserID"].ToString() + "'))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            string count = dt.Rows[0][0].ToString();
            if (count != "0")
            {
                lblExterAudit.Text = "(" + count + ")";
            }
        }
    }
}
