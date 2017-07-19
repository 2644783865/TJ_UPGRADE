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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MS_ShowNormalAdjustEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageInit();
            }
        }
        /// <summary>
        /// 绑定页面信息
        /// </summary>
        protected void PageInit()
        {
            /*BM_ID, BM_TUHAO, BM_MARID, BM_MASTATE, BM_ZONGXU, BM_CHANAME, BM_ENGSHNAME, BM_GUIGE, BM_PJID, BM_ENGID, BM_CONDICTIONATR, BM_ALLBEIZHU, BM_MALENGTH, BM_MAWIDTH, BM_MAUNITWGHT, BM_MATOTALWGHT, BM_MATOTALLGTH, BM_MABGZMY, BM_TUMAQLTY, BM_TUSTAD, BM_TUPROBLEM, BM_TUUNITWGHT, BM_CALUNITWGHT, BM_NUMBER, BM_UNITWGHT, BM_TOTALWGHT, BM_MASHAPE, BM_XIALIAO, BM_ISMANU, BM_PROCESS, BM_FILLMAN, BM_FILLDATE, BM_FIXEDSIZE, BM_MPSTATE, BM_MPSTATUS, BM_MSSTATE, BM_MSSTATUS, BM_MSTEMP, BM_OSSTATE, BM_STATUS, BM_KEYCOMS, BM_CALSTATUS, BM_NOTE, BM_WAIXINGCH, BM_MANAME, BM_MAGUIGE, BM_THRYWGHT, BM_MAQUALITY, BM_MAUNIT, BM_ENGNAME, BM_PJNAME, BM_FILLMANNAME, BM_STANDARD, BM_XUHAO, BM_MSREVIEW, BM_MPREVIEW, BM_OSSTATUS, BM_OSREVIEW, BM_KU, BM_MSXUHAO, BM_TUTOTALWGHT, BM_SIGTUUNITWGHT, BM_SIGTTOTALWGHT, BM_WMARPLAN, BM_LABOUR, BM_ORDERINDEX, BM_SINGNUMBER, BM_TASKID, BM_INTOCOUNT, BM_PNUMBER, BM_BJ, BM_FATHER, BM_MPMY, BM_SUBMITCHG, BM_OtherNote, BM_OLDINDEX */

          
            ViewState["xuhao"] = Request.QueryString["xuhao"];
            ViewState["taskid"] = Request.QueryString["taskid"];
            string sqltext = "select BM_MARID,BM_ZONGXU,BM_ISMANU,BM_CHANAME,BM_MANAME,BM_TUHAO,BM_MAGUIGE,BM_MAQUALITY,BM_KU,BM_NOTE,BM_XIALIAO,BM_PROCESS,BM_UNITWGHT,BM_TOTALWGHT,BM_TUUNITWGHT,BM_TUTOTALWGHT,BM_ALLBEIZHU from View_TM_DQO where  BM_ENGID='" + ViewState["taskid"].ToString() + "' AND BM_XUHAO='" + ViewState["xuhao"].ToString() + "'";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.HasRows)
            {
                dr.Read();
                string marid = dr["BM_MARID"].ToString();
                txtXuhao.Text = ViewState["xuhao"].ToString();
                txtXuhao.Enabled = false;
                ddlInMs.SelectedValue = dr["BM_ISMANU"].ToString();
               
                txtTuHao.Text = dr["BM_TUHAO"].ToString();
                txtCaiZhi.Text = dr["BM_MAQUALITY"].ToString();
                txtCaiZhi.Enabled = false;
                txtGuiGe.Text = dr["BM_MAGUIGE"].ToString();
                txtGuiGe.Enabled = false;
                txtKu.Text = dr["BM_KU"].ToString();
                xlNote.Text = dr["BM_NOTE"].ToString();
                txtXialiao.Text = dr["BM_XIALIAO"].ToString();
                txtProcess.Text = dr["BM_PROCESS"].ToString();
                txtUWGHT.Text = dr["BM_UNITWGHT"].ToString();
                txtWGHT.Text = dr["BM_TOTALWGHT"].ToString();
                txtTUUWGHT.Text = dr["BM_TUUNITWGHT"].ToString();
                txtTUWGTH.Text = dr["BM_TUTOTALWGHT"].ToString();
                txtBZ.Text = dr["BM_ALLBEIZHU"].ToString();

                if (marid!="")
                {
                   txtCHName.Text=dr["BM_MANAME"].ToString();
                   txtCHName.Enabled = false;
                }
                else
                {
                    txtCHName.Text = dr["BM_CHANAME"].ToString();
                }
              
               
                dr.Close();
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            string sql = "";
            sql = "update TBPM_STRINFODQO set [BM_TUHAO]='" + txtTuHao.Text.Trim() + "',[BM_CHANAME]='" + txtCHName.Text.Trim() + "',[BM_XIALIAO]='" + txtXialiao.Text.Trim() + "',[BM_PROCESS]='" + txtProcess.Text.Trim() + "',[BM_NOTE]='" + xlNote.Text.Trim() + "',[BM_KU]='" + txtKu.Text.Trim() + "',BM_ISMANU='" + ddlInMs.SelectedValue + "',BM_ALLBEIZHU='" + txtBZ.Text.Trim() + "',BM_UNITWGHT='" + txtUWGHT.Text.Trim() + "',BM_TOTALWGHT='" + txtWGHT.Text.Trim() + "',BM_TUUNITWGHT='" + txtTUUWGHT.Text.Trim() + "',BM_TUTOTALWGHT='" + txtTUWGTH.Text.Trim() + "'  where BM_ENGID='" + ViewState["taskid"].ToString() + "' AND BM_XUHAO='" + ViewState["xuhao"].ToString() + "'";
            list_sql.Add(sql);
            DBCallCommon.ExecuteTrans(list_sql);
            Response.Write("<script>alert('数据更新成功！！！');window.close();</script>");
        }
    }
}
