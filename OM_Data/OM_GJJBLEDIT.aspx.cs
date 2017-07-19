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
    public partial class OM_GJJBLEDIT : System.Web.UI.Page
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
            string sqltext = "select (GJJ_DWBL*100) as GJJ_DWBL,(GJJ_GRBL*100) as GJJ_GRBL from OM_GJJBL";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                GJJ_DWBL.Text = dt.Rows[0]["GJJ_DWBL"].ToString().Trim();
                GJJ_GRBL.Text = dt.Rows[0]["GJJ_GRBL"].ToString().Trim();
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
            string sql = "select * from OM_GJJBL";
            DataTable dtifcz = DBCallCommon.GetDTUsingSqlText(sql);
            if (dtifcz.Rows.Count > 0)
            {
                sqllist.Add("update OM_GJJBL set GJJ_DWBL=" + (CommonFun.ComTryDecimal(GJJ_DWBL.Text.Trim())) / 100 + ",GJJ_GRBL=" + (CommonFun.ComTryDecimal(GJJ_GRBL.Text.Trim())) / 100 + "");
            }
            else
            {
                sqllist.Add("insert into OM_GJJBL(GJJ_DWBL,GJJ_GRBL) values(" + (CommonFun.ComTryDecimal(GJJ_DWBL.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(GJJ_GRBL.Text.Trim())) / 100 + ")");
            }


            sqllist.Add("insert into OM_GJJBLJL(GJJ_DWBL,GJJ_GRBL,GJJ_XGSJ,GJJ_XGR) values(" + (CommonFun.ComTryDecimal(GJJ_DWBL.Text.Trim())) / 100 + "," + (CommonFun.ComTryDecimal(GJJ_GRBL.Text.Trim())) / 100 + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + Session["UserName"].ToString().Trim() + "')");

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

            Response.Redirect("OM_GJJ.aspx");
        }
    }
}
