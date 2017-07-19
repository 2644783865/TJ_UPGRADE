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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Ms_DownWardQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData();
            }
        }

        protected void BindData()
        {
            string xuhao_engid = Server.HtmlDecode(Request.QueryString["xuhao_engid_table"].ToString());
            string[] a = xuhao_engid.Split(',');
            ViewState["xuhao"] = a[0];
            ViewState["engid"] = a[1];

            switch (a[2])
            {
                case "TBPM_MSOFHZY":
                    ViewState["table"] = "View_TM_MSOFHZY";
                    break;
                case "TBPM_MSOFQLM":
                    ViewState["table"] = "View_TM_MSOFQLM";
                    break;
                case "TBPM_MSOFBLJ":
                    ViewState["table"] = "View_TM_MSOFBLJ";
                    break;
                case "TBPM_MSOFDQJ":
                    ViewState["table"] = "View_TM_MSOFDQJ";
                    break;
                case "TBPM_MSOFGFB":
                    ViewState["table"] = "View_TM_MSOFGFB";
                    break;
                case "TBPM_MSOFDQO":
                    ViewState["table"] = "View_TM_MSOFDQO";
                    break;
                default: break;
            }
            string sql = "select * from " + ViewState["table"] + " where MS_ENGID='" + a[1] + "' and MS_NEWINDEX='" + a[0] + "' and MS_REWSTATE!='9' and MS_STATUS='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            SmartGridView1.DataSource = dt;
            SmartGridView1.DataBind();
            if (SmartGridView1.Rows.Count > 0)
            {
                NoDataPanel1.Visible = false;

                btnShowMpChange.Visible = true;
            }
            else
            {
                btnShowMpChange.Visible = false;
                NoDataPanel1.Visible = true;
            }
        }
        /// <summary>
        /// 关联变更计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowMpChange_OnClick(object sender, EventArgs e)
        {
            btnShowMpChange.Visible = false;
            string chgpid = ((Label)SmartGridView1.Rows[0].FindControl("lblLotNum")).Text.Trim();
            string sql = "select * from " + ViewState["table"] + " where MS_ENGID='" + ViewState["engid"].ToString() + "' and MS_NEWINDEX='" + ViewState["xuhao"] + "' and MS_CHGPID='" + chgpid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            SmartGridView2.DataSource = dt;
            SmartGridView2.DataBind();
            if (SmartGridView2.Rows.Count > 0)
            {
                NoDataPanel2.Visible = false;
            }
            else
            {
                NoDataPanel2.Visible = true;
            }
        }
    }
}
