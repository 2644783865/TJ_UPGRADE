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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchange_detail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pt_code.Text = Request.QueryString["ptcode"].ToString();//计划号
                initpagemess();
            }
        }
        private void initpagemess()
        {
            tbpc_puryclRepeaterdatabind();
            tbpc_purbgclRepeaterdatabind();
        }
        private void tbpc_puryclRepeaterdatabind()//采购计划repeater
        {
            //现在应该从View_TBPC_PURCHASEPLAN_RVW这个视图中查找
            string sqltext="SELECT planno as PUR_PCODE,pjid as PUR_PJID,pjnm as PUR_PJNAME,engid as PUR_ENGID,engnm as PUR_ENGNAME,marid as PUR_MARID," +
                             "marnm as PUR_MARNAME,margg as PUR_MARNORM,marcz as PUR_MARTERIAL,margb as PUR_GUOBIAO,num as PUR_NUM,rpnum as PUR_RPNUM," +
                             "marunit as PUR_NUNIT,length as PUR_LENGTH,width as PUR_WIDTH,ptcode as PUR_PTCODE,purnote as PUR_NOTE,cgzgid as PUR_CGMAN," +
                             "cgrnm as PUR_CGMANNM,purstate as PUR_STATE FROM View_TBPC_PURCHASEPLAN_RVW   " +
                             "WHERE ptcode='" + pt_code.Text + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DBCallCommon.BindRepeater(tbpc_puryclRepeater, sqltext);
            if (tbpc_puryclRepeater.Items.Count == 0)
            {
                Panel1.Visible = true;
            }
            else
            {
                Panel1.Visible = false;
            }
        }
        private void tbpc_purbgclRepeaterdatabind()//变更信息repeater
        {
            //应该从这个View_TBPC_MPTEMPCHANGE视图中查找
            string sqltext = "select  chpcode as BG_PCODE,chptcode as BG_PTCODE,marid as BG_MARID, marnm as BG_MARNAME, " +
                            "pjid as MP_PJID,pjnm as MP_PJNAME,engid as MP_ENGID,engnm as MP_ENGNAME,margg as BG_MARNORM, marcz as BG_MARTERIAL, " +
                            "unit as BG_NUNIT,length as LENGTH,width as WIDTH,chfznum as BG_FZNUM, chnum as BG_NUM, chcgid as MP_CGID,chcgnm as MP_CGNAME, " +
                            "margb as BG_GUOBIAO, chstate as MP_STATE, chnote as BG_NOTE  " +
                            "from View_TBPC_MPTEMPCHANGE  " +
                            "where chptcode='" + pt_code.Text + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DBCallCommon.BindRepeater(tbpc_purbgclRepeater, sqltext);
            if (tbpc_purbgclRepeater.Items.Count == 0)
            {
                NoDataPane2.Visible = true;
            }
            else
            {
                NoDataPane2.Visible = false;
            }
        }
    }
}
