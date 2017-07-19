using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Finished_OUTBILL : System.Web.UI.Page
    {
        string sqltext = "";
        string flag = string.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"] != null)
                flag = Request.QueryString["FLAG"].ToString();
               docnum.Text = Request.QueryString["docnum"].ToString();
            if (!IsPostBack)
            {
                initinfo();
                if (flag == "PUSH")
                {                  
                    initial();         
                    lblOutDate.Text = DateTime.Now.ToString();
                }
            } 
        }
        private void initinfo()
        {
                lblOutDate.Text = DateTime.Now.ToString();
                string sqltext = "select ST_ID,ST_NAME from TBDS_STAFFINFO WHERE ST_DEPID='" + Session["UserDeptID"].ToString() + "'and ST_PD='0'order by ST_ID DESC";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                cob_fuziren.DataSource = dt;
                cob_fuziren.DataTextField = "ST_NAME";
                cob_fuziren.DataValueField = "ST_ID";
                cob_fuziren.DataBind();
               // cob_fuziren.SelectedIndex = 0;
                cob_fuziren.SelectedValue = Session["UserID"].ToString();
                cob_sqren.DataSource = dt;
                cob_sqren.DataTextField = "ST_NAME";
                cob_sqren.DataValueField = "ST_ID";
                cob_sqren.DataBind();
                cob_sqren.SelectedValue = Session["UserID"].ToString();
                TextBoxexecutor.Text = Session["UserName"].ToString();
                TextBoxexecutorid.Text = Session["UserID"].ToString();
    
        }
        protected void initial()
        {
            sqltext = "SELECT * FROM TBMP_FINISHED_OUT AS A LEFT OUTER JOIN View_CM_FaHuo AS B ON (A.TSA_ID=B.TSA_ID and A.TFO_ENGNAME=B.TSA_ENGNAME and A.TFO_MAP=B.TSA_MAP and A.TFO_ZONGXU=B.ID ) left join TBMP_FINISHED_STORE as c on a.TSA_ID=c.KC_TSA and a.TFO_ZONGXU=KC_ZONGXU where TFO_DOCNUM='" + docnum.Text + "'";
            DBCallCommon.BindGridView(GridView1, sqltext);   
        }

        private void writedata()
        {
            string tsaid;
            List<string> list_sql = new List<string>();
            sqltext = "update TBMP_FINISHED_OUT set DocuPersonID='" + TextBoxexecutorid.Text + "',REVIEWA='" + cob_fuziren.SelectedValue.ToString() + "',SQRID='" + cob_sqren.SelectedValue.ToString() + "',OUTDATE='" + lblOutDate.Text + "',NOTE='" + txt_note.Text.ToString() + "'where TFO_DOCNUM='" + docnum.Text + "' ";
            list_sql.Add(sqltext);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                tsaid = ((Label)gr.FindControl("lbltsaid")).Text;
               // outnote = ((TextBox)gr.FindControl("txtnote")).Text;
                //string engname = ((Label)gr.FindControl("txtengname")).Text;
                //string map = ((Label)gr.FindControl("txtmap")).Text;
               // string bianhao = ((Label)gr.FindControl("lblbianhao")).Text;
                string fid = ((Label)gr.FindControl("lblfid")).Text;
                string cmid = ((Label)gr.FindControl("CM_ID")).Text;
                string sqltext2 = "update TBCM_FHBASIC set CM_STATUS='4' WHERE CM_ID='" + cmid + "' AND CM_FID='" + fid + "'AND CM_STATUS='2'";
              list_sql.Add(sqltext2);
             
            }
            DBCallCommon.ExecuteTrans(list_sql);
        }
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            string st = "OK";
            if (GridView1.Rows.Count == 0)
            {
                st = "NoData";
            }
            if (st == "OK")
            {
                writedata();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);
                Response.Redirect("~/PM_Data/PM_Finished_out_look.aspx?action=view&docnum=" + docnum.Text + "");
            }
            else if (st == "NoData")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！没有出库数据！！！');", true);
            }
        }
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("PM_Finished_OUT.aspx");
        }
    }
}
