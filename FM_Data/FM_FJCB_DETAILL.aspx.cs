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
    public partial class FM_FJCB_DETAILL : System.Web.UI.Page
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
            string sqltext = "select FJCB_TSAID,FJCB_YQZL,FJCB_YQYL,FJCB_YQHSDJ,FJCB_YQHSJE,FJCB_XSJYL,FJCB_XSJHSDJ,FJCB_XSJHSJE,FJCB_HJHSJE,FJCB_HJBHSJE,FJCB_YEAR,FJCB_MONTH from FM_FJCB where ID='" + getid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            rptProNumCost.DataSource = dt;
            rptProNumCost.DataBind();
        }


        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string getid = Request.QueryString["id"].ToString();
            foreach (RepeaterItem item in rptProNumCost.Items)
            {
                TextBox tbrwh = (TextBox)item.FindControl("tbrwh");
                TextBox tbyqzl = (TextBox)item.FindControl("tbyqzl");
                TextBox tbyqyl = (TextBox)item.FindControl("tbyqyl");
                TextBox tbyqhsdj = (TextBox)item.FindControl("tbyqhsdj");
                TextBox tbyqhsje = (TextBox)item.FindControl("tbyqhsje");
                TextBox tbxsjyl = (TextBox)item.FindControl("tbxsjyl");
                TextBox tbxsjhsdj = (TextBox)item.FindControl("tbxsjhsdj");
                TextBox tbxsjhsje = (TextBox)item.FindControl("tbxsjhsje");
                TextBox tbhjhsje = (TextBox)item.FindControl("tbhjhsje");
                TextBox tbhjbhsje = (TextBox)item.FindControl("tbhjbhsje");
                TextBox tbnf = (TextBox)item.FindControl("tbnf");
                TextBox tbyf = (TextBox)item.FindControl("tbyf");
                string sqlxg = "update FM_FJCB set FJCB_TSAID='" + tbrwh.Text.ToString() + "',FJCB_YQZL='" + tbyqzl.Text.ToString() + "',FJCB_YQYL='" + tbyqyl.Text.ToString() + "',FJCB_YQHSDJ='" + tbyqhsdj.Text.ToString() + "',FJCB_YQHSJE='" + tbyqhsje.Text.ToString() + "',FJCB_XSJYL='" + tbxsjyl.Text.ToString() + "',FJCB_XSJHSDJ='" + tbxsjhsdj.Text.ToString() + "',FJCB_XSJHSJE='" + tbxsjhsje.Text.ToString() + "',FJCB_HJHSJE='" + tbhjhsje.Text.ToString() + "',FJCB_HJBHSJE='" + tbhjbhsje.Text.ToString() + "',FJCB_YEAR='" + tbnf.Text.ToString() + "',FJCB_MONTH='" + tbyf.Text.ToString() + "' where ID='" + getid + "'";
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
