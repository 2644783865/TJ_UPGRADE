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
    public partial class SM_Trans_LDHYEdit : System.Web.UI.Page
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
                    string sql = "SELECT * FROM TBTM_LDHY WHERE LDHY_ID='" + Request.QueryString["ID"].ToString() + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        TextBoxYEAR.Text = dr["LDHY_YEAR"].ToString();
                        TextBoxProject.Text = dr["LDHY_PROJECT"].ToString();
                        TextBoxGOODNAME.Text = dr["LDHY_GOODNAME"].ToString();
                        TextBoxNUM.Text = dr["LDHY_NUM"].ToString();
                        TextBoxBZXS.Text = dr["LDHY_BZXS"].ToString();
                        TextBoxTJ.Text = dr["LDHY_TJ"].ToString();
                        TextBoxZL.Text = dr["LDHY_ZL"].ToString();
                        TextBoxYFYF.Text = dr["LDHY_YFYF"].ToString();
                        TextBoxYSYF.Text = dr["LDHY_YSYF"].ToString();
                        TextBoxYSFS.Text = dr["LDHY_YSFS"].ToString();
                        TextBoxDate.Text = dr["LDHY_TRANSDATE"].ToString();
                        TextBoxCZR.Text = dr["LDHY_CZR"].ToString();
                        TextBoxBZ.Text = dr["LDHY_BZ"].ToString();
                        TextBoxYFJSQK.Text = dr["LDHY_YFJSQK"].ToString();
                    }
                    dr.Close();
                    LabelDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    LabelClerk.Text = Session["UserName"].ToString();
                }
            }
        }

        protected void Continue_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO TBTM_LDHY(LDHY_PROJECT,LDHY_GOODNAME,LDHY_NUM,LDHY_BZXS," +
                "LDHY_TJ,LDHY_ZL,LDHY_YFYF,LDHY_YSYF,LDHY_YSFS,LDHY_TRANSDATE," +
                "LDHY_CZR,LDHY_BZ,LDHY_YFJSQK,LDHY_YEAR,LDHY_CLERKID,LDHY_DATE)" +
                "VALUES('" + TextBoxProject.Text + "','" + TextBoxGOODNAME.Text + "','" + TextBoxNUM.Text + "','" +
                TextBoxBZXS.Text + "','" + TextBoxTJ.Text + "','" + TextBoxZL.Text + "','" +
                TextBoxYFYF.Text + "','" + TextBoxYSYF.Text + "','" + TextBoxYSFS.Text + "','" + TextBoxDate.Text + "','" +
                TextBoxCZR.Text + "','" + TextBoxBZ.Text + "','" + TextBoxYFJSQK.Text + "','" +
                TextBoxYEAR.Text + "','" +
                Session["UserID"].ToString() + "','" + 
                DateTime.Now + "')";
            DBCallCommon.ExeSqlText(sql);
            Response.Redirect("SM_Trans_LDHYEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"].ToString() == "NEW")
            {
                string sql = "INSERT INTO TBTM_LDHY(LDHY_PROJECT,LDHY_GOODNAME,LDHY_NUM,LDHY_BZXS," +
                    "LDHY_TJ,LDHY_ZL,LDHY_YFYF,LDHY_YSYF,LDHY_YSFS,LDHY_TRANSDATE," +
                    "LDHY_CZR,LDHY_BZ,LDHY_YFJSQK,LDHY_YEAR,LDHY_CLERKID,LDHY_DATE)" +
                    "VALUES('" + TextBoxProject.Text + "','" + TextBoxGOODNAME.Text + "','" + TextBoxNUM.Text + "','" +
                    TextBoxBZXS.Text + "','" + TextBoxTJ.Text + "','" + TextBoxZL.Text + "','" +
                    TextBoxYFYF.Text + "','" + TextBoxYSYF.Text + "','" + TextBoxYSFS.Text + "','" + TextBoxDate.Text + "','" +
                    TextBoxCZR.Text + "','" + TextBoxBZ.Text + "','" + TextBoxYFJSQK.Text + "','" +
                    TextBoxYEAR.Text + "','" +
                    Session["UserID"].ToString() + "','" +
                    DateTime.Now + "')";
                DBCallCommon.ExeSqlText(sql);
                Confirm.Enabled = false;
            }
            if (Request.QueryString["FLAG"].ToString() == "EDIT")
            {
                string sql = "UPDATE TBTM_LDHY SET LDHY_PROJECT='" + TextBoxProject.Text + "',LDHY_GOODNAME='" + TextBoxGOODNAME.Text + "'," +
                    "LDHY_NUM='" + TextBoxNUM.Text + "',LDHY_BZXS='" + TextBoxBZXS.Text + "'," +
                    "LDHY_TJ='" + TextBoxTJ.Text + "',LDHY_ZL='" + TextBoxZL.Text + "'," +
                    "LDHY_YFYF='" + TextBoxYFYF.Text + "',LDHY_YSYF='" + TextBoxYSYF.Text + "',LDHY_YSFS='" + TextBoxYSFS.Text + "'," +
                    "LDHY_TRANSDATE='" + TextBoxDate.Text + "',LDHY_CZR='" + TextBoxCZR.Text + "',LDHY_BZ='" + TextBoxBZ.Text + "'," +
                    "LDHY_YFJSQK='" + TextBoxYFJSQK.Text + "',LDHY_YEAR='" + TextBoxYEAR.Text + "'," +
                    "LDHY_CLERKID='" + Session["UserID"].ToString() + "'," +
                    "LDHY_DATE='" + DateTime.Now + "' WHERE LDHY_ID='" + Request.QueryString["ID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
            }
            Response.Redirect("SM_Trans_Manage.aspx?TAB=8");
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_Manage.aspx?TAB=8");
        }


    }
}
