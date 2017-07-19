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
using System.IO;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_GZTZedit : System.Web.UI.Page
    {
        string arryid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            arryid = Request.QueryString["arrayqdid"].ToString().Trim();
            string arrayidpage = "('" + arryid.Replace("/", "','") + "')";
            if (!IsPostBack)
            {
                bindinfo(arrayidpage);
            }
        }
        private void bindinfo(string idpage)
        {
            string sqltext = "select * from OM_GZQD as a left join TBDS_STAFFINFO as b on a.QD_STID=b.ST_ID left join TBDS_DEPINFO as c on b.ST_DEPID=c.DEP_CODE where QD_ID in" + idpage + "";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            rptProNumCost.DataSource = dt;
            rptProNumCost.DataBind();
        }
        protected void btnsave_click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string bh = "TZ"+DateTime.Now.ToString("yyyyMMddHHmmss").Trim();
            string stryearmonth="";
            for (int i = 0; i < rptProNumCost.Items.Count; i++)
            {
                Label lb_QD_YEARMONTH = (Label)rptProNumCost.Items[i].FindControl("lb_QD_YEARMONTH");
                stryearmonth=lb_QD_YEARMONTH.Text.Trim();
                Label lb_ST_ID = (Label)rptProNumCost.Items[i].FindControl("lb_ST_ID");

                TextBox tb_GZTZ_BFJBF = (TextBox)rptProNumCost.Items[i].FindControl("tb_GZTZ_BFJBF");
                TextBox tb_GZTZ_BFZYBF = (TextBox)rptProNumCost.Items[i].FindControl("tb_GZTZ_BFZYBF");

                TextBox tb_GZTZ_BF = (TextBox)rptProNumCost.Items[i].FindControl("tb_GZTZ_BF");
                TextBox tb_GZTZ_BK = (TextBox)rptProNumCost.Items[i].FindControl("tb_GZTZ_BK");
                TextBox tb_GZTZ_NOTE = (TextBox)rptProNumCost.Items[i].FindControl("tb_GZTZ_NOTE");
                if (CommonFun.ComTryDecimal(tb_GZTZ_BK.Text.Trim()) > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('补扣应为负数！');", true);
                    return;
                }
                string sql = "insert into OM_GZTZdetal(GZTZ_SPBH,GZTZ_YEARMONTH,GZTZ_STID,GZTZ_BFJBF,GZTZ_BFZYBF,GZTZ_BF,GZTZ_BK,GZTZ_NOTE) values('" + bh + "','" + stryearmonth + "','" + lb_ST_ID.Text.Trim() + "'," + CommonFun.ComTryDecimal(tb_GZTZ_BFJBF.Text.Trim()) + "," + CommonFun.ComTryDecimal(tb_GZTZ_BFZYBF.Text.Trim()) + "," + CommonFun.ComTryDecimal(tb_GZTZ_BF.Text.Trim()) + "," + CommonFun.ComTryDecimal(tb_GZTZ_BK.Text.Trim()) + ",'" + tb_GZTZ_NOTE.Text.Trim() + "')";
                list.Add(sql);
            }
            string sqlinsertsp = "insert into OM_GZTZSP(GZTZSP_SPBH,GZTZSP_YEARMONTH,GZTZSP_SQRSTID,GZTZSP_SQRNAME,GZTZSP_SQTIME) values('" + bh + "','" + stryearmonth + "','" + Session["UserID"].ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "')";
            list.Add(sqlinsertsp);
            DBCallCommon.ExecuteTrans(list);
            Response.Redirect("OM_GZTZSPdetail.aspx?spid=" + bh);
        }
    }
}
