using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_PURCHASEPLAN_STOUSE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                hfdptc.Value = Server.UrlDecode(Request.QueryString["ptc"].ToString());//计划跟踪号 
                tbpc_purshaseplancheck_datialRepeaterdatabind();
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
            }
        }

        private void tbpc_purshaseplancheck_datialRepeaterdatabind()
        {


            string sqltext = "SELECT a.ptcode AS PUR_PTCODE,a.pjnm as pjnm,a.engnm as engnm,a.marid AS PUR_MARID,a.marnm AS PUR_MARNAME," +
                        "a.margg AS PUR_MARNORM,a.marcz AS PUR_MARTERIAL,a.margb AS PUR_GUOBIAO," +
                        "a.num AS PUR_NUM,a.fznum AS PUR_FZNUM,a.marunit AS PUR_NUNIT," +
                        "a.usenum as PUR_USTNUM,a.allnote as PUR_NOTE,b.PR_REVIEWANM as tjr,b.PR_REVIEWATIME as tjtime,b.PR_REVIEWBNM as shr,b.PR_REVIEWBTIME as shtime FROM View_TBPC_MARSTOUSEALL as a " +
                        "LEFT OUTER JOIN View_TBPC_MARSTOUSETOTAL as b on a.planno=b.PR_PCODE where ptcode='" + hfdptc.Value + "' ";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            tbpc_purshaseplancheck_datialRepeater.DataSource = dt;
            tbpc_purshaseplancheck_datialRepeater.DataBind();

            if (tbpc_purshaseplancheck_datialRepeater.Items.Count > 0)
            {
                NoDataPane1.Visible = false;
            }
            else
            {
                NoDataPane1.Visible = true;
            }
        }
    }
}
