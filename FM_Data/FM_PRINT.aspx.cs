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
using System.Collections.Generic;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_PRINT : System.Web.UI.Page
    {
        double jehj = 0;
        double sehj = 0;
        double hsjehj = 0;
        string fpcode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            fpcode = Request.QueryString["fpbh"];
            string sql = "select * from Inv_View where GI_CODE='" + fpcode + "' ";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.HasRows)
            {
                dr.Read();
                //供应商名称
                lbGI_SUPPLIERNM.Text = dr["GI_SUPPLIERNM"].ToString();
                //发票号码
                lbGI_INVOICENO.Text = dr["GI_INVOICENO"].ToString();
                //日期
                lbGI_DATE.Text = dr["GI_DATE"].ToString();//发票创建日期
                //凭证号
                lbGI_PZH.Text = dr["GI_PZH"].ToString();
            }
            dr.Close();
            string sqltext = "select UNIQUEID,WG_CODE,WG_MARID,MNAME,GUIGE,PURCUNIT,cast(WG_RSNUM as float) as WG_RSNUM,WG_TAXRATE,cast(round(WG_UPRICE,4) as float) as WG_UPRICE,cast(round(WG_CTAXUPRICE,4) as float) as WG_CTAXUPRICE,cast(round(WG_AMOUNT,2) as float) as WG_AMOUNT,cast(round(WG_CTAMTMNY,2) as float) as WG_CTAMTMNY,round((round(WG_CTAMTMNY,2)-round(WG_AMOUNT,2)),2) as WG_SE,WG_CTYPE from dbo.MS_IN_GJFP ('" + fpcode + "') order by UNIQUEID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            rptProNumCost.DataSource = dt;
            rptProNumCost.DataBind();
        }
        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label wg_amount = (Label)e.Item.FindControl("lbwg_amount");
                jehj += Convert.ToDouble(wg_amount.Text);//金额
                Label wg_se = (Label)e.Item.FindControl("lbwg_se");
                sehj += Convert.ToDouble(wg_se.Text);//金额
                Label wg_ctamtmny = (Label)e.Item.FindControl("lbwg_ctamtmny");
                hsjehj += Convert.ToDouble(wg_ctamtmny.Text);//金额
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lbjehj = (Label)e.Item.FindControl("lbjehj");
                Label lbsehj = (Label)e.Item.FindControl("lbsehj");
                Label lbhsjehj = (Label)e.Item.FindControl("lbhsjehj");
                lbjehj.Text = string.Format("{0:c2}", jehj);
                lbhsjehj.Text = string.Format("{0:c2}", sehj);
                lbhsjehj.Text = string.Format("{0:c2}", hsjehj);
            }
        }
    }
}
