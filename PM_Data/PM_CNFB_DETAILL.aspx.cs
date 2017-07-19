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

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_CNFB_DETAILL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataShow();
            }
        }
        private void DataShow()
        {
            string getid = Request.QueryString["id"].ToString();
            string sqltext = "select CNFB_PROJNAME,CNFB_HTID,CNFB_TSAID,CNFB_TH,CNFB_SBNAME,CNFB_NUM,CNFB_BYMYMONEY,CNFB_BYREALMONEY,CNFB_YEAR,CNFB_MONTH,CNFB_TYPE from TBMP_CNFB_LIST where ID='"+getid+"'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            rptProNumCost.DataSource = dt;
            rptProNumCost.DataBind();
        }


        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string getid = Request.QueryString["id"].ToString();
            foreach(RepeaterItem item in rptProNumCost.Items)
            {
                TextBox projname = (TextBox)item.FindControl("tbprojname");
                TextBox projid = (TextBox)item.FindControl("tbprojid");
                TextBox rwh = (TextBox)item.FindControl("tbrwh");
                TextBox th = (TextBox)item.FindControl("tbth");
                TextBox sbname = (TextBox)item.FindControl("tbsbname");
                TextBox sl = (TextBox)item.FindControl("tbsl");
                TextBox bymymoney = (TextBox)item.FindControl("tbbymymoney");
                TextBox byrealmoney = (TextBox)item.FindControl("tbbyrealmoney");
                TextBox year = (TextBox)item.FindControl("tbyear");
                TextBox month = (TextBox)item.FindControl("tbmonth");
                TextBox type = (TextBox)item.FindControl("tbtype");
                string sqlxg = "update TBMP_CNFB_LIST set CNFB_PROJNAME='" + projname.Text.ToString() + "',CNFB_HTID='" + projid.Text.ToString() + "',CNFB_TSAID='" + rwh.Text.ToString() + "',CNFB_TH='" + th.Text.ToString() + "',CNFB_SBNAME='" + sbname.Text.ToString() + "',CNFB_NUM='" + sl.Text.ToString() + "',CNFB_BYMYMONEY='" + bymymoney.Text.ToString() + "',CNFB_BYREALMONEY='" + byrealmoney.Text.ToString() + "',CNFB_YEAR='" + year.Text.ToString() + "',CNFB_MONTH='" + month.Text.ToString() + "',CNFB_TYPE='" + type.Text.ToString() + "' where ID='"+getid+"'";
                DBCallCommon.ExeSqlText(sqlxg);
                Response.Write("<script>alert('修改成功！')</script>");
                Response.Write("<script>window.close()</script>");
            }
            
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window.close();</script>");
        }
    }
}
