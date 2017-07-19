using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.SM_Data.SM_Trans_Management
{
    public partial class SM_Trans_GNFYEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["FLAG"].ToString() == "NEW")
                {
                    LabelDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    LabelClerk.Text = Session["UserName"].ToString();
                    TextBoxYEAR.Text = "格式：2000年";
                }

                if (Request.QueryString["FLAG"] == "EDIT")
                {
                    Continue.Visible = false;
                    Continue.Enabled = false;
                    string sql = "SELECT * FROM TBTM_GNFY WHERE GNFY_ID='" + Request.QueryString["ID"].ToString() + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        TextBoxYEAR.Text = dr["GNFY_YEAR"].ToString();
                        TextBoxProject.Text = dr["GNFY_PJNAME"].ToString();
                        TextBoxEngine.Text = dr["GNFY_ENGNAME"].ToString();
                        TextBoxHTBH.Text = dr["GNFY_HTBH"].ToString();
                        TextBoxHTJE.Text = dr["GNFY_HTJE"].ToString();
                        TextBoxZZL.Text = dr["GNFY_ZW"].ToString();
                        TextBoxZTJ.Text = dr["GNFY_ZV"].ToString();
                        TextBoxRZB.Text = dr["GNFY_RZB"].ToString();
                        TextBoxDJTIME.Text = dr["GNFY_DJTIME"].ToString();
                        TextBoxZL.Text = dr["GNFY_ZL1"].ToString();
                        TextBoxTJ.Text = dr["GNFY_TJ1"].ToString();
                        TextBoxZL1.Text = dr["GNFY_ZL2"].ToString();
                        TextBoxTJ1.Text = dr["GNFY_TJ2"].ToString();
                        TextBoxCC.Text = dr["GNFY_CC"].ToString();
                        TextBoxGBZL.Text = dr["GNFY_GBZL"].ToString();
                        TextBoxZZJSJE.Text = dr["GNFY_LJE"].ToString();
                        
                        TextBoxBZ.Text = dr["GNFY_BZ"].ToString();
                    }
                    dr.Close();
                    LabelDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    LabelClerk.Text = Session["UserName"].ToString();
                }
            }
        }

        protected void Continue_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO TBTM_GNFY( GNFY_YEAR,GNFY_PJNAME,GNFY_ENGNAME,GNFY_HTBH,GNFY_HTJE,GNFY_RZB," +
                "GNFY_ZW,GNFY_ZV,GNFY_DJTIME,GNFY_ZL1,GNFY_ZL2," +
                "GNFY_TJ1,GNFY_TJ2,GNFY_CC,GNFY_GBZL,GNFY_LJE," +
                "GNFY_BZ,GNFY_CLERKID,GNFY_DATE)" +
                "VALUES('" + TextBoxYEAR.Text + "','" + TextBoxProject.Text + "','" + TextBoxEngine.Text + "','" + TextBoxHTBH.Text + "','" +
                TextBoxHTJE.Text + "','" + TextBoxRZB.Text + "','" + TextBoxZZL.Text + "','" +
                TextBoxZTJ.Text + "','" + TextBoxDJTIME.Text + "','" + TextBoxZL.Text + "','" +
                TextBoxZL1.Text + "','" + TextBoxTJ.Text + "','" + TextBoxTJ1.Text + "','" +
                TextBoxCC.Text + "','" + TextBoxGBZL.Text + "','" + TextBoxZZJSJE.Text + "','" + 
                TextBoxBZ.Text + "','" + 
                Session["UserID"].ToString() + "','" + DateTime.Now + "')";
            DBCallCommon.ExeSqlText(sql);
            Response.Redirect("SM_Trans_GNFYEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            string a = Request.QueryString["FLAG"].ToString();
            if (Request.QueryString["FLAG"].ToString() == "NEW")
            {
                string sql = "INSERT INTO TBTM_GNFY(GNFY_YEAR,GNFY_PJNAME,GNFY_ENGNAME,GNFY_HTBH,GNFY_HTJE,GNFY_RZB," +
                "GNFY_ZW,GNFY_ZV,GNFY_DJTIME,GNFY_ZL1,GNFY_ZL2," +
                "GNFY_TJ1,GNFY_TJ2,GNFY_CC,GNFY_GBZL,GNFY_LJE," +
                "GNFY_BZ,GNFY_CLERKID,GNFY_DATE)" +
                    "VALUES('" + TextBoxYEAR.Text + "','" + TextBoxProject.Text + "','" + TextBoxEngine.Text + "','" + TextBoxHTBH.Text + "','" +
                TextBoxHTJE.Text + "','" + TextBoxRZB.Text + "','" + TextBoxZZL.Text + "','" +
                TextBoxZTJ.Text + "','" + TextBoxDJTIME.Text + "','" + TextBoxZL.Text + "','" +
                TextBoxZL1.Text + "','" + TextBoxTJ.Text + "','" + TextBoxTJ1.Text + "','" +
                TextBoxCC.Text + "','" + TextBoxGBZL.Text + "','" + TextBoxZZJSJE.Text + "','" +
                TextBoxBZ.Text + "','" + 
                    Session["UserID"].ToString() + "','" + DateTime.Now + "')";
                DBCallCommon.ExeSqlText(sql);
                Confirm.Enabled = false;
            }
            if (Request.QueryString["FLAG"].ToString() == "EDIT")
            {
                string sql = "UPDATE TBTM_GNFY SET  GNFY_PJNAME='" + TextBoxProject.Text + "',GNFY_HTBH='" + TextBoxHTBH.Text + "'," +
                    "GNFY_HTJE='" + TextBoxHTJE.Text + "',GNFY_RZB='" + TextBoxRZB.Text + "'," +
                    "GNFY_ENGNAME='" + TextBoxEngine.Text + "',GNFY_DJTIME='" + TextBoxDJTIME.Text + "'," +
                    "GNFY_ZW='" + TextBoxZZL.Text + "',GNFY_ZV='" + TextBoxZTJ.Text +"',"+
                    "GNFY_ZL1='" + TextBoxZL.Text + "',GNFY_ZL2='" + TextBoxZL1.Text + "'," +
                    "GNFY_TJ1='" + TextBoxTJ.Text + "',GNFY_TJ2='" + TextBoxTJ1.Text + "'," +
                    "GNFY_CC='" + TextBoxCC.Text + "',GNFY_GBZL='" + TextBoxGBZL.Text + "'," +
                    "GNFY_LJE='" + TextBoxZZJSJE.Text + "',GNFY_BZ='" + TextBoxBZ.Text + "'," +
                    "GNFY_YEAR='" + TextBoxYEAR.Text + "'," +
                    "GNFY_CLERKID='" + Session["UserID"].ToString() + "'," +
                    "GNFY_DATE='" + DateTime.Now+ "' WHERE GNFY_ID='" + Request.QueryString["ID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
            }
            Response.Redirect("SM_Trans_GNFY.aspx");
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_GNFY.aspx");
        }


    }
}
