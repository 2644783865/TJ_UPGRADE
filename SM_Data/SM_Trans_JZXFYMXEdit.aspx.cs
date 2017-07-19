using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Trans_JZXFYMXEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["FLAG"].ToString() == "NEW")
                {
                    string jzxid = Request.QueryString["PID"].ToString();
                    string sql = "SELECT * FROM TBTM_JZXFY WHERE JZXFY_ID='" + jzxid + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        LabelProject.Text = dr["JZXFY_PROJECT"].ToString();
                        LabelFYPC.Text = dr["JZXFY_FYPC"].ToString();
                        LabelTransDate.Text = dr["JZXFY_TRANSDATE"].ToString();
                    }
                    dr.Close(); LabelDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    LabelClerk.Text = Session["UserName"].ToString();
                }

                if (Request.QueryString["FLAG"] == "EDIT")
                {
                    Continue.Visible = false;
                    Continue.Enabled = false;
                    string sql = "SELECT * FROM TBTM_JZXFYDETAIL a INNER JOIN TBTM_JZXFY b ON a.JZXFYDETAIL_JZXFYID=b.JZXFY_ID WHERE JZXFYDETAIL_ID='" + Request.QueryString["ID"].ToString() + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        LabelProject.Text = dr["JZXFY_PROJECT"].ToString();
                        LabelFYPC.Text = dr["JZXFY_FYPC"].ToString();
                        LabelTransDate.Text = dr["JZXFY_TRANSDATE"].ToString();
                        TextBoxGOODNAME.Text = dr["JZXFYDETAIL_GOODNAME"].ToString();
                        TextBoxSCZH.Text = dr["JZXFYDETAIL_SCZH"].ToString();
                        TextBoxHWZL.Text = dr["JZXFYDETAIL_HWZL"].ToString();
                    }
                    dr.Close();
                    LabelDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    LabelClerk.Text = Session["UserName"].ToString();
                }
            }
        }

        protected void Continue_Click(object sender, EventArgs e)
        {
            string jzxid = Request.QueryString["PID"].ToString();
            string sql = "INSERT INTO TBTM_JZXFYDETAIL(JZXFYDETAIL_JZXFYID," +
                "JZXFYDETAIL_GOODNAME,JZXFYDETAIL_SCZH,JZXFYDETAIL_HWZL," +
                "JZXFYDETAIL_CLERKID,JZXFYDETAIL_DATE)" +
                "VALUES('" + jzxid + "','" + TextBoxGOODNAME.Text + "','" + TextBoxSCZH.Text + "','" +
                TextBoxHWZL.Text + "','" +
                Session["UserID"].ToString() + "','" + 
                DateTime.Now + "')";
            DBCallCommon.ExeSqlText(sql);
            Response.Redirect("SM_Trans_JZXFYMXEdit.aspx?FLAG=NEW&&ID=NEW&&PID=" + jzxid);
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"].ToString() == "NEW")
            {
                string jzxid = Request.QueryString["PID"].ToString();
                string sql = "INSERT INTO TBTM_JZXFYDETAIL(JZXFYDETAIL_JZXFYID," +
                    "JZXFYDETAIL_GOODNAME,JZXFYDETAIL_SCZH,JZXFYDETAIL_HWZL," +
                    "JZXFYDETAIL_CLERKID,JZXFYDETAIL_DATE)" +
                    "VALUES('" + jzxid + "','" + TextBoxGOODNAME.Text + "','" + TextBoxSCZH.Text + "','" +
                    TextBoxHWZL.Text + "','" +
                    Session["UserID"].ToString() + "','" +
                    DateTime.Now + "')";
                DBCallCommon.ExeSqlText(sql);
                Confirm.Enabled = false;
                Response.Redirect("SM_Trans_Manage.aspx?TAB=9");
            }
            if (Request.QueryString["FLAG"].ToString() == "EDIT")
            {
                string sql = "UPDATE TBTM_JZXFYDETAIL SET JZXFYDETAIL_GOODNAME='" + TextBoxGOODNAME.Text + "',JZXFYDETAIL_SCZH='" + TextBoxSCZH.Text + "'," +
                    "JZXFYDETAIL_HWZL='" + TextBoxHWZL.Text + "'," +
                    "JZXFYDETAIL_CLERKID='" + Session["UserID"].ToString() +  "'," +
                    "JZXFYDETAIL_DATE='" + DateTime.Now + "' WHERE JZXFYDETAIL_ID='" + Request.QueryString["ID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
                Response.Redirect("SM_Trans_Manage.aspx?TAB=10");
            }
            
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"].ToString() == "NEW")
            {
                Response.Redirect("SM_Trans_Manage.aspx?TAB=9");
            }
            else
            {
                Response.Redirect("SM_Trans_Manage.aspx?TAB=10");
            }            
        }


    }
}
