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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_sdpriceedit : System.Web.UI.Page
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
            string sqltext = "select * from OM_SDPRICE";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                dianprice.Text = dt.Rows[0]["dianprice"].ToString().Trim();
                shuiprice.Text = dt.Rows[0]["shuiprice"].ToString().Trim();
                lbxgtime.Text = dt.Rows[0]["xiugaidate"].ToString().Trim();
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
            string sql = "select * from OM_SDPRICE";
            DataTable dtifcz = DBCallCommon.GetDTUsingSqlText(sql);
            if (dtifcz.Rows.Count > 0)
            {
                sqllist.Add("update OM_SDPRICE set dianprice=" + CommonFun.ComTryDecimal(dianprice.Text.Trim()) + ",shuiprice=" + CommonFun.ComTryDecimal(shuiprice.Text.Trim()) + ",xiugaidate='"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim()+"'");
            }
            else
            {
                sqllist.Add("insert into OM_SDPRICE(dianprice,shuiprice,xiugaidate) values(" + CommonFun.ComTryDecimal(dianprice.Text.Trim()) + "," + CommonFun.ComTryDecimal(shuiprice.Text.Trim()) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "')");
            }

            DBCallCommon.ExecuteTrans(sqllist);
            Response.Write("<script>alert('操作已成功!');window.close();window.dialogArguments.location.href='~/OM_Data/OM_SHUIDFLIST.aspx';</script>");
        }
    }
}
