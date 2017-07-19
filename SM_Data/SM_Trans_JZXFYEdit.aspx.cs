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
    public partial class SM_Trans_JZXFYEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["FLAG"].ToString() == "NEW")
                {
                    LabelDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    LabelClerk.Text = Session["UserName"].ToString();
                }

                if (Request.QueryString["FLAG"] == "EDIT")
                {
                    Continue.Visible = false;
                    Continue.Enabled = false;
                    string sql = "SELECT * FROM TBTM_JZXFY WHERE JZXFY_ID='" + Request.QueryString["ID"].ToString() + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        TextBoxYEAR.Text = dr["JZXFY_YEAR"].ToString();
                        TextBoxProject.Text = dr["JZXFY_PROJECT"].ToString();
                        TextBoxFYPC.Text = dr["JZXFY_FYPC"].ToString();
                        TextBoxDate.Text = dr["JZXFY_TRANSDATE"].ToString();
                        TextBoxHWMS.Text = dr["JZXFY_HWMS"].ToString();
                        TextBoxXS.Text = dr["JZXFY_XS"].ToString();
                        TextBoxLFM.Text = dr["JZXFY_LFM"].ToString();
                        TextBoxHZ.Text = dr["JZXFY_HZ"].ToString();
                        TextBoxRZB.Text = dr["JZXFY_RZB"].ToString();
                        TextBoxTJZXL.Text = dr["JZXFY_TJZXL"].ToString();
                        TextBoxZLZXL.Text = dr["JZXFY_ZLZXL"].ToString();
                        TextBoxXXJXS.Text = dr["JZXFY_XXJXS"].ToString();
                        TextBoxZXSYCL.Text = dr["JZXFY_ZXSYCL"].ToString();
                    }
                    dr.Close();
                    LabelDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    LabelClerk.Text = Session["UserName"].ToString();
                }
            }
        }

        protected void Continue_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO TBTM_JZXFY(JZXFY_PROJECT,JZXFY_FYPC,JZXFY_TRANSDATE,JZXFY_HWMS," +
                "JZXFY_XS,JZXFY_LFM,JZXFY_HZ,JZXFY_RZB,JZXFY_TJZXL," +
                "JZXFY_ZLZXL,JZXFY_XXJXS,JZXFY_ZXSYCL,JZXFY_YEAR,JZXFY_CLERKID,JZXFY_DATE)" +
                "VALUES('" + TextBoxProject.Text + "','" + TextBoxFYPC.Text + "','" + TextBoxDate.Text + "','" +
                TextBoxHWMS.Text + "','" + TextBoxXS.Text + "','" + TextBoxLFM.Text + "','" +
                TextBoxHZ.Text + "','" + TextBoxRZB.Text + "','" + TextBoxTJZXL.Text + "','" +
                TextBoxZLZXL.Text + "','" + TextBoxXXJXS.Text + "','" + TextBoxZXSYCL.Text + "','" +
                TextBoxYEAR.Text + "','" +
                Session["UserID"].ToString() + "','" +
                DateTime.Now + "')";
            DBCallCommon.ExeSqlText(sql);
            Response.Redirect("SM_Trans_JZXFYEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"].ToString() == "NEW")
            {
                string sql = "INSERT INTO TBTM_JZXFY(JZXFY_PROJECT,JZXFY_FYPC,JZXFY_TRANSDATE,JZXFY_HWMS," +
                    "JZXFY_XS,JZXFY_LFM,JZXFY_HZ,JZXFY_RZB,JZXFY_TJZXL," +
                    "JZXFY_ZLZXL,JZXFY_XXJXS,JZXFY_ZXSYCL,JZXFY_YEAR,JZXFY_CLERKID,JZXFY_DATE)" +
                    "VALUES('" + TextBoxProject.Text + "','" + TextBoxFYPC.Text + "','" + TextBoxDate.Text + "','" +
                    TextBoxHWMS.Text + "','" + TextBoxXS.Text + "','" + TextBoxLFM.Text + "','" +
                    TextBoxHZ.Text + "','" + TextBoxRZB.Text + "','" + TextBoxTJZXL.Text + "','" +
                    TextBoxZLZXL.Text + "','" + TextBoxXXJXS.Text + "','" + TextBoxZXSYCL.Text + "','" +
                    TextBoxYEAR.Text + "','" +
                    Session["UserID"].ToString() + "','" + 
                    DateTime.Now + "')";
                DBCallCommon.ExeSqlText(sql);
                Confirm.Enabled = false;
            }
            if (Request.QueryString["FLAG"].ToString() == "EDIT")
            {
                string sql = "UPDATE TBTM_JZXFY SET JZXFY_PROJECT='" + TextBoxProject.Text + "',JZXFY_FYPC='" + TextBoxFYPC.Text + "'," +
                    "JZXFY_TRANSDATE='" + TextBoxDate.Text + "',JZXFY_HWMS='" + TextBoxHWMS.Text + "'," +
                    "JZXFY_XS='" + TextBoxXS.Text + "',JZXFY_LFM='" + TextBoxLFM.Text + "'," +
                    "JZXFY_HZ='" + TextBoxHZ.Text + "',JZXFY_RZB='" + TextBoxRZB.Text + "'," +
                    "JZXFY_TJZXL='" + TextBoxTJZXL.Text + "',JZXFY_ZLZXL='" + TextBoxZLZXL.Text + "',JZXFY_XXJXS='" + TextBoxXXJXS.Text + "'," +
                    "JZXFY_ZXSYCL='" + TextBoxZXSYCL.Text + "',JZXFY_YEAR='" + TextBoxYEAR.Text + "'," +
                    "JZXFY_CLERKID='" + Session["UserID"].ToString() + "'," +
                    "JZXFY_DATE='" + DateTime.Now + "' WHERE JZXFY_ID='" + Request.QueryString["ID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
            }
            Response.Redirect("SM_Trans_Manage.aspx?TAB=9");
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_Manage.aspx?TAB=9");
        }

    }
}
