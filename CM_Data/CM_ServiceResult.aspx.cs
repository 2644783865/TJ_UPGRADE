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
using System.Text;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_ServiceResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowDate();
            }
        }

        private void ShowDate()
        {
            string sql = "select * from View_CM_CusApply where CM_ID='" + Request.QueryString["id"] + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DataRow dr = dt.Rows[0];
            foreach (Control control in panel.Controls)
            {
                if (control is Label)
                {
                    ((Label)control).Text = dr[control.ID].ToString();
                }
                if (control is TextBox)
                {
                    ((TextBox)control).Text = dr[control.ID].ToString();
                }
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            StringBuilder col = new StringBuilder();
            foreach (Control control in panel.Controls)
            {
                if (control is TextBox)
                {
                    col.Append(control.ID + "='" + ((TextBox)control).Text + "',");
                }
            }
            string column = col.ToString(0, col.Length - 1);
            string sql = string.Format("update TBCM_APPLICA set {0} where CM_ID='{1}'", column, Request.QueryString["id"]);
            DBCallCommon.ExeSqlText(sql);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已成功更新！');window.opener=null;window.open('','_self');window.close();", true);
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
            this.ClientScript.RegisterStartupScript(GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
        }
    }
}
