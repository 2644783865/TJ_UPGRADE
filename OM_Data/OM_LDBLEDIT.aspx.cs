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
    public partial class OM_LDBLEDIT : System.Web.UI.Page
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
            string sqltext = "select (BLLD_YLBXD*100) as BLLD_YLBXD,(BLLD_SYBXD*100) as BLLD_SYBXD,(BLLD_GSBXD*100) as BLLD_GSBXD,(BLLD_SYD*100) as BLLD_SYD,(BLLD_YLD*100) as BLLD_YLD,(BLLD_GJJD*100) as BLLD_GJJD,(BLLD_YLGR*100) as BLLD_YLGR,(BLLD_SYGR*100) as BLLD_SYGR,(BLLD_JBYLGR*100) as BLLD_JBYLGR,(BLLD_GJJGR*100) as BLLD_GJJGR from OM_LDBXBL";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                BLLD_YLBXD.Text = dt.Rows[0]["BLLD_YLBXD"].ToString().Trim();
                BLLD_SYBXD.Text = dt.Rows[0]["BLLD_SYBXD"].ToString().Trim();
                BLLD_GSBXD.Text = dt.Rows[0]["BLLD_GSBXD"].ToString().Trim();
                BLLD_SYD.Text = dt.Rows[0]["BLLD_SYD"].ToString().Trim();
                BLLD_YLD.Text = dt.Rows[0]["BLLD_YLD"].ToString().Trim();
                BLLD_GJJD.Text = dt.Rows[0]["BLLD_GJJD"].ToString().Trim();
                BLLD_YLGR.Text = dt.Rows[0]["BLLD_YLGR"].ToString().Trim();
                BLLD_SYGR.Text = dt.Rows[0]["BLLD_SYGR"].ToString().Trim();
                BLLD_JBYLGR.Text = dt.Rows[0]["BLLD_JBYLGR"].ToString().Trim();
                BLLD_GJJGR.Text = dt.Rows[0]["BLLD_GJJGR"].ToString().Trim();
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
            string sql = "select * from OM_LDBXBL";
            DataTable dtifcz = DBCallCommon.GetDTUsingSqlText(sql);
            if (dtifcz.Rows.Count > 0)
            {
                sqllist.Add("update OM_LDBXBL set BLLD_YLBXD=" + (CommonFun.ComTryDecimal(BLLD_YLBXD.Text.Trim())) / 100 + ",BLLD_SYBXD=" + (CommonFun.ComTryDecimal(BLLD_SYBXD.Text.Trim())) / 100 + ",BLLD_GSBXD=" + (CommonFun.ComTryDecimal(BLLD_GSBXD.Text.Trim())) / 100 + ",BLLD_SYD=" + (CommonFun.ComTryDecimal(BLLD_SYD.Text.Trim())) / 100 + ",BLLD_YLD=" + (CommonFun.ComTryDecimal(BLLD_YLD.Text.Trim())) / 100 + ", BLLD_GJJD=" + (CommonFun.ComTryDecimal(BLLD_GJJD.Text.Trim())) / 100 + ", BLLD_YLGR=" + (CommonFun.ComTryDecimal(BLLD_YLGR.Text.Trim())) / 100 + ", BLLD_SYGR=" + (CommonFun.ComTryDecimal(BLLD_SYGR.Text.Trim())) / 100 + ",BLLD_JBYLGR=" + (CommonFun.ComTryDecimal(BLLD_JBYLGR.Text.Trim())) / 100 + ",BLLD_GJJGR=" + (CommonFun.ComTryDecimal(BLLD_GJJGR.Text.Trim())) / 100 + "");
            }
            else
            {
                sqllist.Add("insert into OM_LDBXBL(BLLD_YLBXD,BLLD_SYBXD,BLLD_GSBXD,BLLD_SYD,BLLD_YLD,BLLD_GJJD,BLLD_YLGR,BLLD_SYGR,BLLD_JBYLGR,BLLD_GJJGR) values(" + (CommonFun.ComTryDecimal(BLLD_YLBXD.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_SYBXD.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_GSBXD.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_SYD.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_YLD.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_GJJD.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_YLGR.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_SYGR.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_JBYLGR.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_GJJGR.Text.Trim())) / 100 + ")");
            }


            sqllist.Add("insert into OM_LDBXBLJL(BLLD_YLBXD,BLLD_SYBXD,BLLD_GSBXD,BLLD_SYD,BLLD_YLD,BLLD_GJJD,BLLD_YLGR,BLLD_SYGR,BLLD_JBYLGR,BLLD_GJJGR,BLLD_XGSJ,BLLD_XGR) values(" + (CommonFun.ComTryDecimal(BLLD_YLBXD.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_SYBXD.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_GSBXD.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_SYD.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_YLD.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_GJJD.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_YLGR.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_SYGR.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_JBYLGR.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(BLLD_GJJGR.Text.Trim())) / 100 + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + Session["UserName"].ToString().Trim() + "')");

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

            Response.Redirect("OM_LDBX.aspx");
        }
    }
}
