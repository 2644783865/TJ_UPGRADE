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
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_InvoiceDetail : System.Web.UI.Page
    {
        double je = 0;//汇总金额
        double se = 0;//税额
        double hsje = 0;//汇总含税金额
        protected void Page_Load(object sender, EventArgs e)
        {
            string fcode = string.Empty;
            if (Request.QueryString["incode"] != null)
            {
                fcode = Request.QueryString["incode"];
                hfdfp.Value = fcode;
            }
            if (!IsPostBack)
            {

                bindInInfo(fcode); 
                bindSave(fcode);
            }
        }
        private void bindSave(string fcode)
        {
            string sqltext = "select GI_GJFLAG from TBFM_GHINVOICETOTAL where GI_CODE='" + fcode + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    btnsave.Visible = true;
                }
                else
                {
                    btnsave.Visible = false;
                }
            }
            dr.Close();
        }

        private void bindInInfo(string fcode)
        {
            string sqltext = "select GI_UNICODE,left(GI_UNICODE,10) as WG_CODE ,GI_MATCODE,GI_NAME,GI_GUIGE,GI_UNIT,GI_NUM,GI_UNITPRICE,GI_TAXRATE,GI_CTAXUPRICE,GI_AMTMNY,GI_CTAMTMNY,(GI_CTAMTMNY-GI_AMTMNY) as GI_SE from TBFM_GHINVOICEDETAIL where GI_CODE='" + fcode + "' order by GI_UNICODE";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            grvInvDetail.DataSource = dt;
            grvInvDetail.DataBind();
        }

        protected void grvInvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtJE = (TextBox)e.Row.FindControl("txtGI_je");
                je += Convert.ToDouble(txtJE.Text);//金额

                TextBox txtSE = (TextBox)e.Row.FindControl("txtGI_se");
                se += Convert.ToDouble(txtSE.Text);//税额

                TextBox txtHSJE = (TextBox)e.Row.FindControl("txtGI_HJPRICE");
                hsje += Convert.ToDouble(txtHSJE.Text);//含税金额
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                Label lb_je = (Label)e.Row.FindControl("lbje");

                lb_je.Text = string.Format("{0:c2}", je);

                Label lb_se = (Label)e.Row.FindControl("lbse");

                lb_se.Text = string.Format("{0:c2}", se);

                Label lb_hsje = (Label)e.Row.FindControl("lbhsje");

                lb_hsje.Text = string.Format("{0:c2}", hsje);

            }

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            List<string> sql = new List<string>();
            double totalje = 0;
            double totalsje = 0;
            string sqlstring = "";
            string fp = hfdfp.Value.ToString();


            for (int i = 0; i < grvInvDetail.Rows.Count; i++)
            {

                string mid = ((Label)grvInvDetail.Rows[i].FindControl("lbmid")).Text.ToString();
                string uniqid = grvInvDetail.DataKeys[i]["GI_UNICODE"].ToString();

               
                double taxrate = Convert.ToDouble(((TextBox)grvInvDetail.Rows[i].FindControl("txtGI_TAXRATE")).Text.ToString());//税率

                double se = Convert.ToDouble(((TextBox)grvInvDetail.Rows[i].FindControl("txtGI_se")).Text.ToString());//税额
                double shul = Convert.ToDouble(((TextBox)grvInvDetail.Rows[i].FindControl("text_sj")).Text.ToString());//数量

                ////金额

                double je = Convert.ToDouble(((TextBox)grvInvDetail.Rows[i].FindControl("txtGI_je")).Text.ToString());//金额

                ////含税金额
                double hsje = Convert.ToDouble(((TextBox)grvInvDetail.Rows[i].FindControl("txtGI_HJPRICE")).Text.ToString());//含税金额

                double hsdj = hsje / shul;

                double dj = je / shul;

                sqlstring = " update TBFM_GHINVOICEDETAIL set GI_CTAXUPRICE='" + hsdj + "' , GI_UNITPRICE='" + dj + "',GI_TAXRATE='" + taxrate + "'  where GI_CODE='" + fp + "' and  GI_MATCODE='" + mid + "' and GI_UNICODE='"+uniqid+"' ";

                sql.Add(sqlstring);

                sqlstring = " update TBFM_GHINVOICEDETAIL set GI_AMTMNY=GI_UNITPRICE*GI_NUM,GI_CTAMTMNY=GI_CTAXUPRICE*GI_NUM  where GI_CODE='" + fp + "' and  GI_MATCODE='" + mid + "' and GI_UNICODE='" + uniqid + "' ";

                sql.Add(sqlstring);

                totalje += je;

                totalsje += hsje;


            }
            sqlstring = " update TBFM_GHINVOICETOTAL  set GI_AMTMNY ='" + totalje + "' , GI_CTAMTMNY ='" + totalsje + "' where  GI_CODE='" + fp + "' ";
            sql.Add(sqlstring);
            DBCallCommon.ExecuteTrans(sql);

            this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('操作成功！\\r\\r'); window.returnValue = true;window.close(); ", true);


        }
    }
}
