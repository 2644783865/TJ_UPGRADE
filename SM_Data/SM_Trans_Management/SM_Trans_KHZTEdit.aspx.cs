using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ZCZJ_DPF.FM_Data.SM_Trans_Management
{
    public partial class SM_Trans_KHZTEdit : System.Web.UI.Page
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
                    string sql = "SELECT * FROM TBTM_KHZT WHERE KHZT_ID='" + Request.QueryString["ID"].ToString() + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        TextBoxYEAR.Text = dr["KHZT_YEAR"].ToString();
                        TextBoxPjname.Text = dr["KHZT_PJNAME"].ToString();
                        TextBoxENGNAME.Text = dr["KHZT_ENGNAME"].ToString();
                        TextBoxZZL.Text = dr["KHZT_ZW"].ToString();
                        TextBoxZTJ.Text = dr["KHZT_ZV"].ToString();
                        TextBoxRZB.Text = dr["KHZT_RZB"].ToString();
                        TextBoxDJTIME.Text = dr["KHZT_DJTIME"].ToString();
                        TextBoxZL.Text = dr["KHZT_ZL1"].ToString();
                        TextBoxTJ.Text = dr["KHZT_TJ1"].ToString();
                        TextBoxZL1.Text = dr["KHZT_ZL2"].ToString();
                        TextBoxTJ1.Text = dr["KHZT_TJ2"].ToString();

                        TextBoxBZ.Text = dr["KHZT_BZ"].ToString();
                    }
                    dr.Close();
                    LabelDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    LabelClerk.Text = Session["UserName"].ToString();
                }
            }
        }

        protected void Continue_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO TBTM_KHZT(KHZT_YEAR,KHZT_PJNAME,KHZT_ENGNAME,KHZT_ZW," +
                   "KHZT_ZV,KHZT_RZB,KHZT_DJTIME,KHZT_ZL1,KHZT_TJ1,KHZT_ZL2,KHZT_TJ2,KHZT_BZ)" +
                   "VALUES('" + TextBoxYEAR.Text + "','" + TextBoxPjname.Text + "','" + TextBoxENGNAME.Text + "','" +
                   TextBoxZZL.Text + "','" + TextBoxZTJ.Text + "','" + TextBoxRZB.Text + "','" +
           TextBoxDJTIME.Text + "','" + TextBoxZL.Text + "','" + TextBoxTJ.Text + "','" +
            TextBoxZL1.Text + "','" + TextBoxTJ1.Text + "','" + TextBoxBZ.Text + "')";
            DBCallCommon.ExeSqlText(sql);
            Response.Redirect("SM_Trans_KHZTEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"].ToString() == "NEW")
            {
                string sql = "INSERT INTO TBTM_KHZT(KHZT_YEAR,KHZT_PJNAME,KHZT_ENGNAME,KHZT_ZW," +
                   "KHZT_ZV,KHZT_RZB,KHZT_DJTIME,KHZT_ZL1,KHZT_TJ1,KHZT_ZL2,KHZT_TJ2,KHZT_BZ)" +
                   "VALUES('" + TextBoxYEAR.Text + "','" + TextBoxPjname.Text + "','" + TextBoxENGNAME.Text + "','" +
                   TextBoxZZL.Text + "','" + TextBoxZTJ.Text + "','" + TextBoxRZB.Text + "','" +
           TextBoxDJTIME.Text + "','" + TextBoxZL.Text + "','" + TextBoxTJ.Text + "','" +
            TextBoxZL1.Text + "','" + TextBoxTJ1.Text + "','" + TextBoxBZ.Text + "')";
                DBCallCommon.ExeSqlText(sql);
                Confirm.Enabled = false;
            }
            if (Request.QueryString["FLAG"].ToString() == "EDIT")
            {
                string sql = "UPDATE TBTM_KHZT SET KHZT_YEAR='" + TextBoxYEAR.Text + "',KHZT_PJNAME='" + TextBoxPjname.Text + "'," +
                    "KHZT_ENGNAME='" + TextBoxENGNAME.Text + "',KHZT_ZW='" + TextBoxZZL.Text + "'," +
                     "KHZT_ZV='" + TextBoxZTJ.Text + "',KHZT_RZB='" + TextBoxRZB.Text + "'," +
                      "KHZT_DJTIME='" + TextBoxDJTIME.Text + "',KHZT_ZL1='" + TextBoxZL.Text + "'," +
                    "KHZT_ZL2='" + TextBoxZL1.Text + "',KHZT_TJ1='" + TextBoxTJ.Text + "'," +
                     "KHZT_TJ2='" + TextBoxTJ1.Text + "',KHZT_BZ='" + TextBoxBZ.Text + "'" +  
                    " WHERE KHZT_ID='" + Request.QueryString["ID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
            }
            Response.Redirect("SM_Trans_KHZT.aspx");
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_KHZT.aspx");
        }
    }
}
