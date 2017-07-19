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

namespace ZCZJ_DPF.SM_Data
{
    public partial class SMLL_PRINT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string lldh = Request.QueryString["id"];
            string zdr = Request.QueryString["zdr"];
            lbrq.Text = DateTime.Now.ToString("yyyy-MM-dd");
            lblldh.Text = lldh.ToString();
            string sqltext = "SELECT TSAID,Doc,OP_NOTE1,case when TotalNote='0' then '未选择' else TotalNote end AS TotalNote FROM  View_SM_OUT  where id='" + lldh.ToString() + "' ";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
            lbllbz.Text = dt1.Rows[0]["TotalNote"].ToString().Trim();
            lbrwh.Text=dt1.Rows[0]["TSAID"].ToString();
            lbzdr.Text = dt1.Rows[0]["Doc"].ToString();
            lbnote.Text = dt1.Rows[0]["OP_NOTE1"].ToString();
            string sql3 = "select CM_PROJ+'('+CM_CONTR+')'+TSA_ENGNAME as engname from TBCM_PLAN left join TBCM_BASIC on TBCM_PLAN.ID=TBCM_BASIC.ID where TSA_ID='" + dt1.Rows[0]["TSAID"].ToString() + "'";
            DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sql3);
            if (dt3.Rows.Count>0)
            {
                lbxmmc.Text = dt3.Rows[0]["engname"].ToString().Trim();
            }
            string sql = "select (MaterialName+Standard) as MS,GB,Attribute,Unit,OP_BSH,RealNumber FROM  View_SM_OUT  where id='" + lldh.ToString() + "'";
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql);
            rptProNumCost.DataSource = dt2;
            rptProNumCost.DataBind();
        }
    }
}
