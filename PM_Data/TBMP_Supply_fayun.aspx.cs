using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ZCZJ_DPF.PM_Data
{
    public partial class TBMP_Supply_fayun : System.Web.UI.Page
    {
        //全局变量定义
        public string gloabsheetno
        {
            get
            {
                object str = ViewState["gloabsheetno"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabsheetno"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["sheetno"] != null)
            {
                gloabsheetno = Request.QueryString["sheetno"].ToString();
            }
            else
            {
                gloabsheetno = "";
            }
        }
        //取消
        protected void btnClose_Click1(object sender, EventArgs e)
        {
            string sqltext = "update TBMP_FAYUNPRICE set  PM_SUPPLIERAID='" + PM_SUPPLIERAID.Text + "', PM_SUPPLIERBID='" + PM_SUPPLIERBID.Text + "', PM_SUPPLIERCID='" + PM_SUPPLIERCID.Text + "', PM_SUPPLIERDID='" + PM_SUPPLIERDID.Text + "', PM_SUPPLIEREID='" + PM_SUPPLIEREID.Text + "',  PM_SUPPLIERFID='" + PM_SUPPLIERFID.Text + "' where PM_SHEETNO='" + gloabsheetno + "'";
            DBCallCommon.ExeSqlText(sqltext);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.close();", true);
        }
    }
}
