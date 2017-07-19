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
    public partial class TM_MS_ShowEdit : System.Web.UI.Page
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
            ViewState["mstable"] = Request.QueryString["tablename"];
            ViewState["strtable"] = Request.QueryString["strtable"];
            ViewState["xuhao"] = Request.QueryString["xuhao"];
            ViewState["taskid"] = Request.QueryString["taskid"];
            ViewState["ms_no"]=Request.QueryString["ms_no"];
            string sqltext = "select MS_MSXUHAO,MS_GUIGE,MS_PROCESS,MS_NOTE,MS_KU,MS_NAME,MS_TUHAO from " + ViewState["mstable"].ToString() + " where MS_PID='" + ViewState["ms_no"].ToString() + "' AND MS_NEWINDEX='" + ViewState["xuhao"].ToString() + "' and MS_ENGID='" + ViewState["taskid"].ToString() + "'";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.HasRows)
            {
                dr.Read();
                txtXuhao.Text = ViewState["xuhao"].ToString();
                txtXuhao.Enabled = false;
                txtMSXuhao.Text = dr["MS_MSXUHAO"].ToString();
                txtGuiGe.Text = dr["MS_GUIGE"].ToString();
                txtProcess.Text = dr["MS_PROCESS"].ToString();
                txtBZ.Text = dr["MS_NOTE"].ToString();
                txtKu.Text = dr["MS_KU"].ToString();
                txtCHName.Text = dr["MS_NAME"].ToString();
                txtTuHao.Text = dr["MS_TUHAO"].ToString();
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
            sql = "update  " + ViewState["strtable"].ToString() + "  set BM_MSXUHAO='"+txtMSXuhao.Text.Trim()+"',BM_TUHAO='"+txtTuHao.Text.Trim()+"',BM_CHANAME='"+txtCHName.Text.Trim()+"',BM_GUIGE='"+txtGuiGe.Text.Trim()+"',BM_PROCESS='"+txtProcess.Text.Trim()+"',BM_NOTE='"+txtBZ.Text.Trim()+"',BM_KU='"+txtKu.Text.Trim()+"'  where BM_ENGID='" + ViewState["taskid"].ToString() + "' AND BM_XUHAO='" + ViewState["xuhao"].ToString() + "'";
            list_sql.Add(sql);
            sql = "update " + ViewState["mstable"].ToString() + "  set MS_MSXUHAO='" + txtMSXuhao.Text.Trim() + "',MS_GUIGE='" + txtGuiGe.Text.Trim() + "',MS_PROCESS='" + txtProcess.Text.Trim() + "',MS_NOTE='" + txtBZ.Text.Trim() + "',MS_KU='" + txtKu.Text.Trim() + "',MS_NAME='" + txtCHName.Text.Trim() + "',MS_TUHAO='" + txtTuHao.Text.Trim() + "' where MS_PID='" + ViewState["ms_no"].ToString() + "' AND MS_NEWINDEX='" + ViewState["xuhao"].ToString() + "' and MS_ENGID='" + ViewState["taskid"].ToString() + "'";
            list_sql.Add(sql);
            DBCallCommon.ExecuteTrans(list_sql);
            Response.Write("<script>alert('数据更新成功！！！');window.close();</script>");
        }
    }
}
