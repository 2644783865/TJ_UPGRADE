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
using System.Collections.Generic;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_InspecTaskAssign : System.Web.UI.Page
    {
        string qsaid = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
                qsaid = Request.QueryString["id"].ToString();


            if (!IsPostBack)
            {
                bindQntyInfo();
            }

          

        }

        /// <summary>
        /// 重新分工时的修改数据的绑定
        /// </summary>
        /// <param name="id"></param>
        private void bindQntyInfo()
        {
            string selsql = "select AFI_QCMAN,AFI_QCMANNM from TBQM_APLYFORINSPCT where AFI_ID='" + qsaid + "'";

            DataSet ds = DBCallCommon.FillDataSet(selsql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtqcclerknmid.Value = ds.Tables[0].Rows[0]["AFI_QCMAN"].ToString();
                txtqcclerknm.Value = ds.Tables[0].Rows[0]["AFI_QCMANNM"].ToString();
               
            }

        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string qcclerk = txtqcclerknmid.Value;

            string qcclerknm = txtqcclerknm.Value;

            if (qcclerk == string.Empty)
            {
                string script = "alert('未选择质检工程师，请重新选择！')";
                ClientScript.RegisterStartupScript(this.GetType(), "error", script, true);
            }
            else
            {
                string sql = "update TBQM_APLYFORINSPCT set AFI_QCMAN='" + qcclerk +"',AFI_QCMANNM='" + qcclerknm + "',AFI_ASSGSTATE='1' where AFI_ID='" + qsaid + "'";

                DBCallCommon.ExeSqlText(sql);

                //发送邮件给质量检测员
                string zjyEmail = "";
                sql = "select EMail from TBDS_STAFFINFO where ST_NAME='" + txtqcclerknm.Value + "' and ST_PD='0' ";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    zjyEmail = dt.Rows[0][0].ToString();
                }
                string to = zjyEmail;

                List<string> bjEmailCC = new List<string>();

                bjEmailCC.Add("sunlibo@cbmi.com.cn");


                List<string> mfEmail = null;

                string body = "您有新的质量报检分工任务";
                DBCallCommon.SendEmail(to, bjEmailCC, mfEmail, "数字平台质量报检分工", body);


                ClientScript.RegisterStartupScript(this.GetType(), "onload", "okay();",true);
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("QC_InspecManage.aspx");
        }

    }
}
