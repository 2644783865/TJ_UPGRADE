using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_Task_Assign_piliang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string taskID = Request.QueryString["taskID"];
            if (!IsPostBack)
            {
                lblTaskId.Text = taskID.Trim('/');

            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string whereConstr = lblTaskId.Text.Replace("/", "','");
            whereConstr = "('" + whereConstr + "')";

            List<string> ltsql = new List<string>();
           
            string qcclerk = txtqcclerkid.Value;
            string qcclerknm = txtqcclerk.Value;
          

            //更新该生产制号的资料员、包装员以及分工状态

            string strsql = "update TBQM_QTSASSGN set ";

         
            strsql += "QSA_QCCLERK='" + qcclerk + "',";
            strsql += "QSA_QCCLERKNM='" + qcclerknm + "',";



            strsql += "QSA_TCCHGER='" + Session["UserID"] + "',";
            strsql += "QSA_TCCHGERNM='" + Session["UserName"] + "',";


            strsql += "QSA_DATE='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
            strsql += "QSA_STATE='1',";
            strsql += "QSA_BZ='" + bz.Text.Trim() + "'";
            strsql += " where QSA_ENGID in " + whereConstr;

            ltsql.Add(strsql);



            DBCallCommon.ExecuteTrans(ltsql);


            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:保存成功');", true);
            Response.Redirect("QC_Task_Manage.aspx");

        }

        //作废处理

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string whereConstr = lblTaskId.Text.Replace("/", "','");
            whereConstr = "('" + whereConstr + "')";
            List<string> ltsql = new List<string>();

            //作废处理为4;1已分工;2完工--即提交资料;3反馈;关闭为5
            string strsql = "update TBQM_QTSASSGN set QSA_STATE='4',QSA_BZ='" + bz.Text.Trim() + "' where QSA_ID in  " + whereConstr ;
            ltsql.Add(strsql);

            DBCallCommon.ExecuteTrans(ltsql);

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:保存成功');", true);
            //  Response.Write("<script>alert('提示:保存成功')</script>");
            Response.Redirect("QC_Task_Manage.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

            Response.Redirect("QC_Task_Manage.aspx");
        }
    }
}
