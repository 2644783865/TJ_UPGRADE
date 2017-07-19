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
using System.Collections.Generic;

namespace ZCZJ_DPF.PC_Data
{
    public partial class TBPC_Supply : System.Web.UI.Page
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
        protected void btnClose_Click(object sender, EventArgs e)
        {
            string sqltext = "update TBPC_IQRCMPPRICE set  PIC_SUPPLIERAID='" + PIC_SUPPLIERAID.Text + "', PIC_SUPPLIERBID='" + PIC_SUPPLIERBID.Text + "', PIC_SUPPLIERCID='" + PIC_SUPPLIERCID.Text + "', PIC_SUPPLIERDID='" + PIC_SUPPLIERDID.Text + "', PIC_SUPPLIEREID='" + PIC_SUPPLIEREID.Text + "',  PIC_SUPPLIERFID='" + PIC_SUPPLIERFID.Text + "' where PIC_SHEETNO='" + gloabsheetno + "'";
            DBCallCommon.ExeSqlText(sqltext);

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.close();", true);
            //Response.Write("<script>window.close();</script>");

        }
    }
}
