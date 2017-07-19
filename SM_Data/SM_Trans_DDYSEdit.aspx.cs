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
    public partial class SM_Trans_DDYSEdit : System.Web.UI.Page
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
                    string sql = "SELECT * FROM TBTM_DDYS WHERE DDYS_ID='" + Request.QueryString["ID"].ToString() + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        TextBoxYEAR.Text = dr["DDYS_YEAR"].ToString();
                        TextBoxDate.Text = dr["DDYS_TRANSDATE"].ToString();
                        TextBoxGOODNAME.Text = dr["DDYS_GOODNAME"].ToString();
                        TextBoxUNIT.Text = dr["DDYS_UNIT"].ToString();
                        TextBoxNUM.Text = dr["DDYS_NUM"].ToString();
                        TextBoxJSDJ.Text = dr["DDYS_JSDJ"].ToString();
                        TextBoxJSZJ.Text = dr["DDYS_JSZJ"].ToString();
                        TextBoxCH.Text = dr["DDYS_CH"].ToString();
                        TextBoxQYD.Text = dr["DDYS_QYD"].ToString();
                        TextBoxMDD.Text = dr["DDYS_MDD"].ToString();
                        TextBoxBZ.Text = dr["DDYS_BZ"].ToString();
                    }
                    dr.Close();
                    LabelDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    LabelClerk.Text = Session["UserName"].ToString();
                }
            }
        }

        protected void Continue_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO TBTM_DDYS(DDYS_TRANSDATE,DDYS_GOODNAME,DDYS_UNIT,DDYS_NUM," +
                "DDYS_JSDJ,DDYS_JSZJ,DDYS_CH,DDYS_QYD,DDYS_MDD," +
                "DDYS_BZ,DDYS_YEAR,DDYS_CLERKID,DDYS_DATE)" +
                "VALUES('" + TextBoxDate.Text + "','" + TextBoxGOODNAME.Text + "','" + TextBoxUNIT.Text + "','" +
                TextBoxNUM.Text + "','" + TextBoxJSDJ.Text + "','" + TextBoxJSZJ.Text + "','" +
                TextBoxCH.Text + "','" + TextBoxQYD.Text + "','" + TextBoxMDD.Text + "','" +
                TextBoxBZ.Text + "','" + TextBoxYEAR.Text + "','" +
                Session["UserID"].ToString() + "','" + DateTime.Now + "')";
            DBCallCommon.ExeSqlText(sql);
            Response.Redirect("SM_Trans_DDYSEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"].ToString() == "NEW")
            {
                string sql = "INSERT INTO TBTM_DDYS(DDYS_TRANSDATE,DDYS_GOODNAME,DDYS_UNIT,DDYS_NUM," +
                    "DDYS_JSDJ,DDYS_JSZJ,DDYS_CH,DDYS_QYD,DDYS_MDD," +
                    "DDYS_BZ,DDYS_YEAR,DDYS_CLERKID,DDYS_DATE)" +
                    "VALUES('" + TextBoxDate.Text + "','" + TextBoxGOODNAME.Text + "','" + TextBoxUNIT.Text + "','" +
                    TextBoxNUM.Text + "','" + TextBoxJSDJ.Text + "','" + TextBoxJSZJ.Text + "','" +
                    TextBoxCH.Text + "','" + TextBoxQYD.Text + "','" + TextBoxMDD.Text + "','" +
                    TextBoxBZ.Text + "','" + TextBoxYEAR.Text + "','" +
                    Session["UserID"].ToString() + "','" + DateTime.Now + "')";
                DBCallCommon.ExeSqlText(sql);
                Confirm.Enabled = false;
            }
            if (Request.QueryString["FLAG"].ToString() == "EDIT")
            {
                string sql = "UPDATE TBTM_DDYS SET DDYS_TRANSDATE='" + TextBoxDate.Text + "',DDYS_GOODNAME='" + TextBoxGOODNAME.Text + "'," +
                    "DDYS_UNIT='" + TextBoxUNIT.Text + "',DDYS_NUM='" + TextBoxNUM.Text + "'," +
                    "DDYS_JSDJ='" + TextBoxJSDJ.Text + "',DDYS_JSZJ='" + TextBoxJSZJ.Text + "'," +
                    "DDYS_CH='" + TextBoxCH.Text + "',DDYS_QYD='" + TextBoxQYD.Text + "'," +
                    "DDYS_MDD='" + TextBoxMDD.Text + "',DDYS_BZ='" + TextBoxBZ.Text + "',DDYS_YEAR='" + TextBoxYEAR.Text + "'," +
                    "DDYS_CLERKID='" + Session["UserID"].ToString() + "'," +
                    "DDYS_DATE='" + DateTime.Now + "' WHERE DDYS_ID='" + Request.QueryString["ID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
            }
            Response.Redirect("SM_Trans_Manage.aspx?TAB=6");
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_Manage.aspx?TAB=6");
        }

    }
}
