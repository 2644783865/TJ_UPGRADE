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

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_RuKu_Adjust_Accounts_View : System.Web.UI.Page
    {
        double Amount = 0;
        double CTAmount = 0;
        double InAmount = 0;
        double InCTAmount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string incode = string.Empty;
            string VoiceCode = string.Empty;

            if (!IsPostBack)
            {
                if (Request.QueryString["INID"] != null && Request.QueryString["VID"] != null)
                {
                    incode = Request.QueryString["INID"];
                    VoiceCode = Request.QueryString["VID"];
                    BindVoiceItem(VoiceCode,incode);
                    BindInItem(VoiceCode, incode);
                }
            }
        }
        private void BindItem(string code)
        {
            string sql = "select GI_INSTOREID,GJ_INVOICEID from TBFM_GJRELATION where GJ_CODE='" + code + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                BindInItem(dt.Rows[0]["GJ_INVOICEID"].ToString(), dt.Rows[0]["GI_INSTOREID"].ToString());
                BindVoiceItem(dt.Rows[0]["GJ_INVOICEID"].ToString(), dt.Rows[0]["GI_INSTOREID"].ToString());
            }
        }

        private void BindVoiceItem(string VoiceCode, string incode)
        {
            string sql = "select GI_MATCODE,GI_NAME,GI_GUIGE,GI_UNIT,cast(SUM(GI_NUM) as float) AS GI_NUM,cast(round(AVG(GI_UNITPRICE),4) as float) AS GI_UNITPRICE,cast(round(SUM(GI_AMTMNY),2) as float) AS GI_AMTMNY,GI_TAXRATE,cast(round(AVG(GI_CTAXUPRICE),4) as float) AS GI_CTAXUPRICE,cast(round(SUM(GI_CTAMTMNY),2) as float) AS GI_CTAMTMNY  from TBFM_GHINVOICEDETAIL where GI_CODE='" + VoiceCode + "' and GI_UNICODE like '" + incode.Split('S')[0] + "%' group by GI_MATCODE,GI_NAME,GI_GUIGE,GI_UNIT,GI_TAXRATE";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridViewInvoice.DataSource = dt;
            GridViewInvoice.DataBind();

           
        }

        private void BindInItem(string VoiceCode, string incode)
        {
            //string sql = "select WG_MARID,MNAME,GUIGE,CGDW,cast(sum(WG_RSNUM) as float) AS WG_RSNUM,cast(round(avg(WG_UPRICE),4) as float) as WG_UPRICE,cast(round(sum(WG_AMOUNT),2) as float) as WG_AMOUNT,WG_TAXRATE,cast(round(avg(WG_CTAXUPRICE),4) as float) as WG_CTAXUPRICE, cast(round(sum(WG_CTAMTMNY),2) as float) as WG_CTAMTMNY from View_SM_IN where WG_CODE='" + incode + "' group by WG_MARID,MNAME,GUIGE,CGDW,WG_TAXRATE";
            string sql = "select GI_MATCODE,GI_NAME,GI_GUIGE,GI_UNIT,cast(SUM(GI_NUM) as float) AS GI_NUM,cast(round(AVG(GI_UNITPRICE),4) as float) AS GI_UNITPRICE,cast(round(SUM(GI_INAMTMNY),2) as float) AS GI_AMTMNY,GI_TAXRATE,cast(round(AVG(GI_CTAXUPRICE),4) as float) AS GI_CTAXUPRICE,cast(round(SUM(GI_INCATAMTMNY),2) as float) AS GI_CTAMTMNY  from TBFM_GHINVOICEDETAIL where GI_CODE='" + VoiceCode + "' and GI_UNICODE like '" + incode.Split('S')[0] + "%' group by GI_MATCODE,GI_NAME,GI_GUIGE,GI_UNIT,GI_TAXRATE";
           
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridViewIn.DataSource = dt;
            GridViewIn.DataBind();
        }

        protected void GridViewInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Amount += Convert.ToDouble((e.Row.FindControl("LabelAmount") as Label).Text.Trim());  

                CTAmount += Convert.ToDouble((e.Row.FindControl("LabelCTAmount") as Label).Text.Trim());
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblAmount = (Label)e.Row.FindControl("TotalAmount");

                lblAmount.Text = Amount.ToString();

                Label lblCTAmount = (Label)e.Row.FindControl("TotalCTAmount");

                lblCTAmount.Text = CTAmount.ToString(); 
            }

        }

        protected void GridViewIn_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                InAmount += Convert.ToDouble(e.Row.Cells[5].Text);

                InCTAmount += Convert.ToDouble(e.Row.Cells[7].Text);
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "合计:";
                e.Row.Cells[5].Text = InAmount.ToString();
                e.Row.Cells[7].Text = InCTAmount.ToString();
            }

        }
      

    }
}
