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
    public partial class SM_Trans_SHCEdit : System.Web.UI.Page
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
                    string sql = "SELECT * FROM TBTM_SHC WHERE SHC_ID='" + Request.QueryString["ID"].ToString() + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        TextBoxYEAR.Text = dr["SHC_YEAR"].ToString();
                        TextBoxProject.Text = dr["SHC_PROJECT"].ToString();
                        TextBoxFYXS.Text = dr["SHC_FYXS"].ToString();
                        TextBoxFYPC.Text = dr["SHC_FYPC"].ToString();
                        TextBoxHWMS.Text = dr["SHC_HWMS"].ToString();
                        TextBoxJS.Text = dr["SHC_JS"].ToString();
                        TextBoxTJ.Text = dr["SHC_TJ"].ToString();
                        TextBoxMZ.Text = dr["SHC_MZ"].ToString();
                        TextBoxZYG.Text = dr["SHC_ZYG"].ToString();
                        TextBoxJGWB.Text = dr["SHC_JGWB"].ToString();
                        TextBoxZC.Text = dr["SHC_ZC"].ToString();
                        TextBoxHY.Text = dr["SHC_HY"].ToString();
                        TextBoxCM.Text = dr["SHC_CM"].ToString();
                        TextBoxBZ.Text = dr["SHC_BZ"].ToString();
                    }
                    dr.Close();
                    LabelDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    LabelClerk.Text = Session["UserName"].ToString();
                }
            }
        }

        protected void Continue_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO TBTM_SHC(SHC_PROJECT,SHC_FYXS,SHC_FYPC,SHC_HWMS," +
                "SHC_JS,SHC_TJ,SHC_MZ,SHC_ZYG,SHC_JGWB,SHC_ZC," +
                "SHC_HY,SHC_CM,SHC_BZ,SHC_YEAR,SHC_CLERKID," +
                "SHC_DATE)" +
                "VALUES('" + TextBoxProject.Text + "','" + TextBoxFYXS.Text + "','" + TextBoxFYPC.Text + "','" +
                TextBoxHWMS.Text + "','" + TextBoxJS.Text + "','" + TextBoxTJ.Text + "','" +
                TextBoxMZ.Text + "','" + TextBoxZYG.Text + "','" + TextBoxJGWB.Text + "','" +
                TextBoxZC.Text + "','" + TextBoxHY.Text + "','" + TextBoxCM.Text + "','" +
                TextBoxBZ.Text + "','" + TextBoxYEAR.Text + "','" +
                Session["UserID"].ToString() + "','" +
                DateTime.Now + "')";
            DBCallCommon.ExeSqlText(sql);
            Response.Redirect("SM_Trans_SHCEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"].ToString() == "NEW")
            {
                string sql = "INSERT INTO TBTM_SHC(SHC_PROJECT,SHC_FYXS,SHC_FYPC,SHC_HWMS," +
                    "SHC_JS,SHC_TJ,SHC_MZ,SHC_ZYG,SHC_JGWB,SHC_ZC," +
                    "SHC_HY,SHC_CM,SHC_BZ,SHC_YEAR,SHC_CLERKID," +
                    "SHC_DATE)" +
                    "VALUES('" + TextBoxProject.Text + "','" + TextBoxFYXS.Text + "','" + TextBoxFYPC.Text + "','" +
                    TextBoxHWMS.Text + "','" + TextBoxJS.Text + "','" + TextBoxTJ.Text + "','" +
                    TextBoxMZ.Text + "','" + TextBoxZYG.Text + "','" + TextBoxJGWB.Text + "','" +
                    TextBoxZC.Text + "','" + TextBoxHY.Text + "','" + TextBoxCM.Text + "','" +
                    TextBoxBZ.Text + "','" + TextBoxYEAR.Text + "','" +
                    Session["UserID"].ToString() + "','" +
                    DateTime.Now + "')";
                DBCallCommon.ExeSqlText(sql);
                Confirm.Enabled = false;
            }
            if (Request.QueryString["FLAG"].ToString() == "EDIT")
            {
                string sql = "UPDATE TBTM_SHC SET SHC_PROJECT='" + TextBoxProject.Text + "',SHC_FYXS='" + TextBoxFYXS.Text + "'," +
                    "SHC_FYPC='" + TextBoxFYPC.Text + "',SHC_HWMS='" + TextBoxHWMS.Text + "'," +
                    "SHC_JS='" + TextBoxJS.Text + "',SHC_TJ='" + TextBoxTJ.Text + "'," +
                    "SHC_MZ='" + TextBoxMZ.Text + "',SHC_ZYG='" + TextBoxZYG.Text + "'," +
                    "SHC_JGWB='" + TextBoxJGWB.Text + "',SHC_ZC='" + TextBoxZC.Text + "',SHC_HY='" + TextBoxHY.Text + "'," +
                    "SHC_CM='" + TextBoxCM.Text + "',SHC_BZ='" + TextBoxBZ.Text + "',SHC_YEAR='" + TextBoxYEAR.Text + "'," +
                    "SHC_CLERKID='" + Session["UserID"].ToString() + "'," +
                    "SHC_DATE='" + DateTime.Now + "' WHERE SHC_ID='" + Request.QueryString["ID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
            }
            Response.Redirect("SM_Trans_Manage.aspx?TAB=3");
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_Manage.aspx?TAB=3");
        }

    }
}
