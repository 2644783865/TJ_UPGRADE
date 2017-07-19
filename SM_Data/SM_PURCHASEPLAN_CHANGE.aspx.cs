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
using System.Data.SqlClient;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_PURCHASEPLAN_CHANGE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hfdptc.Value = Request.QueryString["ptc"].ToString();//计划跟踪号 
                tbpc_purbgclallRepeaterdatabind();
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
                getPrenum();
            }
        }
      
        private void tbpc_purbgclallRepeaterdatabind()//上面的那个repeater
        {
            string sqltext = "";
            string ptc = hfdptc.Value;
            sqltext = "select a.chpcode as MP_NEWPID,a.chptcode as BG_PTCODE,a.pjid as MP_PJID,a.pjnm as MP_PJNAME,a.engid as MP_ENGID,a.engnm as MP_ENGNAME,a.marid as BG_MARID, a.marnm as BG_MARNAME, " +
                      " a.margg as BG_MARNORM, a.marcz as BG_MARTERIAL, " +
                      "a.unit as BG_NUNIT, a.chnum as BG_NUM,a.fzunit,a.chfznum as BG_FZNUM,a.length as LENGTH,a.width as WIDTH,a.chcgid as BG_ZXRENID, " +
                      "a.chcgnm as BG_ZXRENNM, a.margb as BG_GUOBIAO, a.chstate as BG_STATE,a.chnote as MP_NOTE,a.zxnum as BG_ZXNUM,a.zxfznum as BG_ZXFZNUM , " +
                      " b.chsubmitnm as jsy,left(b.chsubmittime,10) as jhtime from View_TBPC_MPTEMPCHANGE as a LEFT OUTER JOIN View_TBPC_MPCHANGETOTAL as b on a.chpcode=b.chpcode where a.chptcode='" + ptc + "'";

            DBCallCommon.BindRepeater(tbpc_purbgclallRepeater, sqltext);
            if (tbpc_purbgclallRepeater.Items.Count == 0)
            {
                NoDataPanebg.Visible = true;
            }
            else
            {
                NoDataPanebg.Visible = false;
            }
        }
        protected void getPrenum() //获得原计划数量=计划数量(变更之前的计划数量)+执行数量
        {
            string ptc = hfdptc.Value;
            string strsql = "select sum(isnull(pur_num,0)) as num from TBPC_PURCHASEPLAN where pur_ptcode='" + ptc + "'";  //获得计划数量（变更之前的计划数量）,可能变更多次，因此用sum
            DataTable dt0 = DBCallCommon.GetDTUsingSqlText(strsql);
            string num = "";
            if (dt0.Rows.Count > 0)
            {
                num = dt0.Rows[0][0].ToString();
            }
            string sqltext = "select cast(sum(isnull(MP_BGZXNUM,0)) as  float) as zxnum from TBPC_MPTEMPCHANGE where MP_CHPTCODE='" + ptc + "'"; //获得执行数量
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string zxnum = "";
            if (dt.Rows.Count > 0)
            {
                zxnum = dt.Rows[0][0].ToString();
            }
            for (int i = 0; i < tbpc_purbgclallRepeater.Items.Count; i++)
            {

                ((Label)tbpc_purbgclallRepeater.Items[i].FindControl("LabelPrenum")).Text = (Convert.ToDouble(num) + Convert.ToDouble(zxnum)).ToString();

            }
        }

    }
}
