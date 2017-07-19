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
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_Task_Assign : System.Web.UI.Page
    {

        string qsaid = string.Empty;

       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Id"] != null)
            {
                qsaid = Request.QueryString["Id"].ToString();
            }

            if (!IsPostBack)
            {

                string sql = "select QSA_QCCLERKNM,QSA_BZ,QSA_QCCLERK from TBQM_QTSASSGN where QSA_ID='" + qsaid + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
          
                if (dt.Rows.Count > 0)
                {
                    bz.Text = dt.Rows[0][1].ToString();
                    txtqcclerkid.Value = dt.Rows[0][2].ToString();
                    txtqcclerk.Value = dt.Rows[0][0].ToString();
                 
                }
             
               txtqcclerk.Attributes.Add("readonly", "true");
          
            }




            //if (!IsPostBack)
            //{ 
            //    bindQntyInfo();
            //}

        }


        ///// <summary>
        ///// 重新分工时的修改数据的绑定
        ///// </summary>
        ///// <param name="id"></param>
        //private void bindQntyInfo()
        //{
        //    string selsql = "select QSA_QCDATAID,QSA_QCDATANM,QSA_QCCLERK,QSA_QCCLERKNM,QSA_QCCHGER,QSA_QCCHGERNM,QSA_BZ from TBQM_QTSASSGN where QSA_ID='" + qsaid + "'";
        //    DataSet ds = DBCallCommon.FillDataSet(selsql);
        //    if(ds.Tables[0].Rows.Count>0)
        //    {
        //        txtziliaoyuanid.Value = ds.Tables[0].Rows[0]["QSA_QCDATAID"].ToString();
        //        txtziliaoyuan.Value = ds.Tables[0].Rows[0]["QSA_QCDATANM"].ToString();
        //        txtqcclerkid.Value = ds.Tables[0].Rows[0]["QSA_QCCLERK"].ToString();
        //        txtqcclerk.Value = ds.Tables[0].Rows[0]["QSA_QCCLERKNM"].ToString();
        //        txtqcchgernmid.Value = ds.Tables[0].Rows[0]["QSA_QCCHGER"].ToString();
        //        txtqcchgernm.Value = ds.Tables[0].Rows[0]["QSA_QCCHGERNM"].ToString();
        //        bz.Text = ds.Tables[0].Rows[0]["QSA_BZ"].ToString();
        //    }
        //}

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
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
            strsql += " where QSA_ID='" + qsaid + "'";

            ltsql.Add(strsql);


            DBCallCommon.ExecuteTrans(ltsql);


            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:保存成功');", true);
            Response.Redirect("QC_Task_Manage.aspx");

        }

        //作废处理

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            List<string> ltsql = new List<string>();

            //作废处理为4;1已分工;2完工--即提交资料;3反馈;关闭为5
            string strsql = "update TBQM_QTSASSGN set QSA_STATE='4',QSA_BZ='" + bz.Text.Trim() + "' where QSA_ID='" + qsaid + "'";
            ltsql.Add(strsql);

            DBCallCommon.ExecuteTrans(ltsql);

           ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:保存成功');", true);
          //  Response.Write("<script>alert('提示:保存成功')</script>");
            Response.Redirect("QC_Task_Manage.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            //List<string> ltsql = new List<string>();

            //string strsql = "update TBQM_QTSASSGN set QSA_STATE='5',QSA_BZ='" + bz.Text.Trim() + "' where QSA_ID='" + qsaid + "'";
            //ltsql.Add(strsql);
            ////子任务也关闭
            //strsql = "update TBQM_QTSASSGN set QSA_STATE='5',QSA_BZ='" + bz.Text.Trim() + "' where QSA_ENGID like '" + engid + "-" + "%'";
            //ltsql.Add(strsql);

            //DBCallCommon.ExecuteTrans(ltsql);

            // ClientScript.RegisterStartupScript(this.GetType(), "onload", "okay();", true);
            Response.Redirect("QC_Task_Manage.aspx");
        }

    }
}
