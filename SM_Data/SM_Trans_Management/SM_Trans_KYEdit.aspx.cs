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
    public partial class SM_Trans_KYEdit : System.Web.UI.Page
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
                    string sql = "SELECT * FROM TBTM_KY WHERE KY_ID='" + Request.QueryString["ID"].ToString() + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        TextBoxYEAR.Text = dr["KY_YEAR"].ToString();
                        TextBoxProject.Text = dr["KY_PROJECT"].ToString();
                        TextBoxGOODNAME.Text = dr["KY_GOODNAME"].ToString();
                        TextBoxNUM.Text = dr["KY_NUM"].ToString();
                        TextBoxBZXS.Text = dr["KY_BZXS"].ToString();
                        TextBoxTJ.Text = dr["KY_TJ"].ToString();
                        TextBoxZL.Text = dr["KY_ZL"].ToString();
                        TextBoxYF.Text = dr["KY_YF"].ToString();
                        TextBoxYSGS.Text = dr["KY_YSGS"].ToString();
                        TextBoxDate.Text = dr["KY_TRANSDATE"].ToString();
                        TextBoxFYR.Text= dr["KY_FYR"].ToString();
                        TextBoxBZ.Text = dr["KY_BZ"].ToString();
                        TextBoxYFJSQK.Text = dr["KY_YFJSQK"].ToString();
                    }
                    dr.Close();
                    LabelDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    LabelClerk.Text = Session["UserName"].ToString();
                }
            }
        }

        protected void Continue_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO TBTM_KY(KY_PROJECT,KY_GOODNAME,KY_NUM,KY_BZXS," +
                "KY_TJ,KY_ZL,KY_YF,KY_YSGS,KY_TRANSDATE," +
                "KY_FYR,KY_BZ,KY_YFJSQK,KY_YEAR,KY_CLERKID,KY_DATE)" +
                "VALUES('" + TextBoxProject.Text + "','" + TextBoxGOODNAME.Text + "','" + TextBoxNUM.Text + "','" +
                TextBoxBZXS.Text + "','" + TextBoxTJ.Text + "','" + TextBoxZL.Text + "','" +
                TextBoxYF.Text + "','" + TextBoxYSGS.Text + "','" + TextBoxDate.Text + "','" +
                TextBoxFYR.Text + "','" + TextBoxBZ.Text + "','" + TextBoxYFJSQK.Text + "','" +
                TextBoxYEAR.Text + "','" +
                Session["UserID"].ToString() + "','" +
                DateTime.Now + "')";
            DBCallCommon.ExeSqlText(sql);
            Response.Redirect("SM_Trans_KYEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"].ToString() == "NEW")
            {
                string sql = "INSERT INTO TBTM_KY(KY_PROJECT,KY_GOODNAME,KY_NUM,KY_BZXS," +
                    "KY_TJ,KY_ZL,KY_YF,KY_YSGS,KY_TRANSDATE," +
                    "KY_FYR,KY_BZ,KY_YFJSQK,KY_YEAR,KY_CLERKID,KY_DATE)" +
                    "VALUES('" + TextBoxProject.Text + "','" + TextBoxGOODNAME.Text + "','" + TextBoxNUM.Text + "','" +
                    TextBoxBZXS.Text + "','" + TextBoxTJ.Text + "','" + TextBoxZL.Text + "','" +
                    TextBoxYF.Text + "','" + TextBoxYSGS.Text + "','" + TextBoxDate.Text + "','" +
                    TextBoxFYR.Text + "','" + TextBoxBZ.Text + "','" + TextBoxYFJSQK.Text + "','" +
                    TextBoxYEAR.Text + "','" +
                    Session["UserID"].ToString() + "','" +
                    DateTime.Now + "')";
                DBCallCommon.ExeSqlText(sql);
                Confirm.Enabled = false;
            }
            if (Request.QueryString["FLAG"].ToString() == "EDIT")
            {
                string sql = "UPDATE TBTM_KY SET KY_PROJECT='" + TextBoxProject.Text + "',KY_GOODNAME='" + TextBoxGOODNAME.Text + "'," +
                    "KY_NUM='" + TextBoxNUM.Text + "',KY_BZXS='" + TextBoxBZXS.Text + "'," +
                    "KY_TJ='" + TextBoxTJ.Text + "',KY_ZL='" + TextBoxZL.Text + "'," +
                    "KY_YF='" + TextBoxYF.Text + "',KY_YSGS='" + TextBoxYSGS.Text + "'," +
                    "KY_TRANSDATE='" + TextBoxDate.Text + "',KY_FYR='" + TextBoxFYR.Text + "',KY_BZ='" + TextBoxBZ.Text + "'," +
                    "KY_YFJSQK='" + TextBoxYFJSQK.Text + "',KY_YEAR='" + TextBoxYEAR.Text + "'," +
                    "KY_CLERKID='" + Session["UserID"].ToString() + "'," +
                    "KY_DATE='" + DateTime.Now + "' WHERE KY_ID='" + Request.QueryString["ID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
            }
            Response.Redirect("SM_Trans_KY.aspx");
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_KY.aspx");
        }
    }
}
