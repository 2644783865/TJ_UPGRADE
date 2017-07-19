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
using ZCZJ_DPF;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_SHBXBLEDIT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddata();
            }
        }
        private void binddata()
        {
            string sqltext = "select (BL_QYYLBX*100) as BL_QYYLBX,(BL_SYBX*100) as BL_SYBX,(BL_JBYL*100) as BL_JBYL,(BL_GSBX*100) as BL_GSBX,(BL_SHENGYU*100) as BL_SHENGYU,(BL_QYYLGR*100) as BL_QYYLGR,(BL_SYBXGR*100) as BL_SYBXGR,(BL_JBYLGR*100) as BL_JBYLGR,(BL_ZZDEYL*100) as BL_ZZDEYL from OM_SHBXBL";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                BL_QYYLBX.Text = dt.Rows[0]["BL_QYYLBX"].ToString().Trim();
                BL_SYBX.Text = dt.Rows[0]["BL_SYBX"].ToString().Trim();
                BL_JBYL.Text = dt.Rows[0]["BL_JBYL"].ToString().Trim();
                BL_GSBX.Text = dt.Rows[0]["BL_GSBX"].ToString().Trim();
                BL_SHENGYU.Text = dt.Rows[0]["BL_SHENGYU"].ToString().Trim();
                BL_QYYLGR.Text = dt.Rows[0]["BL_QYYLGR"].ToString().Trim();
                BL_SYBXGR.Text = dt.Rows[0]["BL_SYBXGR"].ToString().Trim();
                BL_JBYLGR.Text = dt.Rows[0]["BL_JBYLGR"].ToString().Trim();
                BL_ZZDEYL.Text = dt.Rows[0]["BL_ZZDEYL"].ToString().Trim();
            }
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            List<string> sqllist = new List<string>();
            sqllist.Clear();
            string sql = "select * from OM_SHBXBL";
            DataTable dtifcz = DBCallCommon.GetDTUsingSqlText(sql);
            if (dtifcz.Rows.Count > 0)
            {
                sqllist.Add("update OM_SHBXBL set BL_QYYLBX=" + (CommonFun.ComTryDecimal(BL_QYYLBX.Text.Trim())) / 100 + ",BL_SYBX=" + (CommonFun.ComTryDecimal(BL_SYBX.Text.Trim())) / 100 + ",BL_JBYL=" + (CommonFun.ComTryDecimal(BL_JBYL.Text.Trim())) / 100 + ",BL_GSBX=" + (CommonFun.ComTryDecimal(BL_GSBX.Text.Trim())) / 100 + ",BL_SHENGYU=" + (CommonFun.ComTryDecimal(BL_SHENGYU.Text.Trim())) / 100 + ", BL_QYYLGR=" + (CommonFun.ComTryDecimal(BL_QYYLGR.Text.Trim())) / 100 + ", BL_SYBXGR=" + (CommonFun.ComTryDecimal(BL_SYBXGR.Text.Trim())) / 100 + ", BL_JBYLGR=" + (CommonFun.ComTryDecimal(BL_JBYLGR.Text.Trim())) / 100 + ",BL_ZZDEYL=" + (CommonFun.ComTryDecimal(BL_ZZDEYL.Text.Trim())) / 100 + "");
            }
            else
            {
                sqllist.Add("insert into OM_SHBXBL(BL_QYYLBX,BL_SYBX,BL_JBYL,BL_GSBX,BL_SHENGYU,BL_QYYLGR,BL_SYBXGR,BL_JBYLGR,BL_ZZDEYL) values(" + (CommonFun.ComTryDecimal(BL_QYYLBX.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BL_SYBX.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BL_JBYL.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BL_GSBX.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BL_SHENGYU.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BL_QYYLGR.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BL_SYBXGR.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BL_JBYLGR.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BL_ZZDEYL.Text.Trim())) / 100 + ")");
            }


            sqllist.Add("insert into OM_SHBXBLJL(BL_QYYLBX,BL_SYBX,BL_JBYL,BL_GSBX,BL_SHENGYU,BL_QYYLGR,BL_SYBXGR,BL_JBYLGR,BL_ZZDEYL,BL_XGSJ,BL_XGR) values(" + (CommonFun.ComTryDecimal(BL_QYYLBX.Text.Trim()))/100 + "," + (CommonFun.ComTryDecimal(BL_SYBX.Text.Trim()))/100 + "," + (CommonFun.ComTryDecimal(BL_JBYL.Text.Trim()))/100 + "," + (CommonFun.ComTryDecimal(BL_GSBX.Text.Trim()))/100 + "," + (CommonFun.ComTryDecimal(BL_SHENGYU.Text.Trim()))/100 + "," + (CommonFun.ComTryDecimal(BL_QYYLGR.Text.Trim()))/100 + "," + (CommonFun.ComTryDecimal(BL_SYBXGR.Text.Trim()))/100 + "," + (CommonFun.ComTryDecimal(BL_JBYLGR.Text.Trim()))/100 + "," + (CommonFun.ComTryDecimal(BL_ZZDEYL.Text.Trim()))/100 + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + Session["UserName"].ToString().Trim() + "')");

            DBCallCommon.ExecuteTrans(sqllist);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据已保存！！！');", true);

        }





        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_OnClick(object sender, EventArgs e)
        {

            Response.Redirect("OM_SHBX.aspx");
        }
    }
}
