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

namespace ZCZJ_DPF.FM_Data
{
    public partial class SM_Trans_KHZTEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
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
                    TextBoxDate.Text = dr["KHZT_DATE"].ToString();
                    TextBoxGOODNAME.Text = dr["KHZT_NAME"].ToString();
                    TextBoxNUM.Text = dr["KHZT_NUM"].ToString();
                    TextBoxTJ.Text = dr["KHZT_LFM"].ToString();
                    TextBoxBZ.Text = dr["KHZT_BZ"].ToString();                   
                }
                dr.Close();
                LabelDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                LabelClerk.Text = Session["UserName"].ToString();
            }
        }

        protected void Continue_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO TBTM_KHZT(KHZT_YEAR,KHZT_DATE,KHZT_NAME,KHZT_NUM," +
                   "KHZT_LFM,KHZT_BZ)" +
                   "VALUES('" + TextBoxYEAR.Text + "','" + TextBoxDate.Text + "','" + TextBoxGOODNAME.Text + "','" +
                   TextBoxNUM.Text + "','" + TextBoxTJ.Text + "','" + TextBoxBZ.Text + "')";
            DBCallCommon.ExeSqlText(sql);
            Response.Redirect("SM_Trans_DDYSEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"].ToString() == "NEW")
            {
                string sql = "INSERT INTO TBTM_KHZT(KHZT_YEAR,KHZT_DATE,KHZT_NAME,KHZT_NUM," +
                    "KHZT_LFM,KHZT_BZ)" +
                    "VALUES('" + TextBoxYEAR.Text + "','" + TextBoxDate.Text + "','" + TextBoxGOODNAME.Text + "','" +
                    TextBoxNUM.Text + "','" + TextBoxTJ.Text + "','" + TextBoxBZ.Text + "')";
                DBCallCommon.ExeSqlText(sql);
                Confirm.Enabled = false;
            }
            if (Request.QueryString["FLAG"].ToString() == "EDIT")
            {
                string sql = "UPDATE TBTM_DDYS SET KHZT_YEAR='" + TextBoxYEAR.Text + "',KHZT_DATE='" + TextBoxDate.Text + "'," +
                    "KHZT_NAME='" + TextBoxGOODNAME.Text + "',KHZT_NUM='" + TextBoxNUM.Text + "'," +
                    "KHZT_LFM='" + TextBoxTJ.Text + "',KHZT_BZ='" + TextBoxBZ.Text + "'," +                   
                    " WHERE KHZT_ID='" + Request.QueryString["ID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
            }
            Response.Redirect("SM_Trans_Manage.aspx?TAB=5");
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_Manage.aspx?TAB=6");
        }
    }
}
