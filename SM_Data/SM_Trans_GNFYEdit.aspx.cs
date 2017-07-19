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
                        TextBoxProject.Text = dr["GNFY_PROJECT"].ToString();
                        TextBoxHTBH.Text = dr["GNFY_HTBH"].ToString();
                        TextBoxHTJE.Text = dr["GNFY_HTJE"].ToString();
                        TextBoxRZB.Text = dr["GNFY_RZB"].ToString();
                        TextBoxLLSTARTDATE.Text = dr["GNFY_LLSTARTDATE"].ToString();
                        TextBoxLLENDDATE.Text = dr["GNFY_LLENDDATE"].ToString();
                        TextBoxLLFDJZL.Text = dr["GNFY_LLFDJZL"].ToString();
                        TextBoxLLDJZL.Text = dr["GNFY_LLDJZL"].ToString();
                        TextBoxLLZTJ.Text = dr["GNFY_LLZTJ"].ToString();
                        TextBoxLLZZL.Text = dr["GNFY_LLZZL"].ToString();
                        TextBoxLLCC.Text = dr["GNFY_LLCC"].ToString();
                        TextBoxGBFDJZL.Text = dr["GNFY_GBFDJZL"].ToString();
                        TextBoxGBDJZL.Text = dr["GNFY_GBDJZL"].ToString();
                        TextBoxZZJSJE.Text = dr["GNFY_ZZJSJE"].ToString();
                        //TextBoxZCJSZL.Text = dr["GNFY_ZCJSZL"].ToString();
                        TextBoxZCJSJE.Text = dr["GNFY_ZCJSJE"].ToString();
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
            string sql = "INSERT INTO TBTM_GNFY(GNFY_PROJECT,GNFY_HTBH,GNFY_HTJE,GNFY_RZB," +
                "GNFY_LLSTARTDATE,GNFY_LLENDDATE,GNFY_LLFDJZL,GNFY_LLDJZL,GNFY_LLZTJ," +
                "GNFY_LLZZL,GNFY_LLCC,GNFY_GBFDJZL,GNFY_GBDJZL,GNFY_ZZJSJE," +
                "GNFY_ZCJSJE,GNFY_BZ,GNFY_YEAR,GNFY_CLERKID,GNFY_DATE)" +
                "VALUES('" + TextBoxProject.Text + "','" + TextBoxHTBH.Text + "','" + TextBoxHTJE.Text + "','" +
                TextBoxRZB.Text + "','" + TextBoxLLSTARTDATE.Text + "','" + TextBoxLLENDDATE.Text + "','" +
                TextBoxLLFDJZL.Text + "','" + TextBoxLLDJZL.Text + "','" + TextBoxLLZTJ.Text + "','" +
                TextBoxLLZZL.Text + "','" + TextBoxLLCC.Text + "','" + TextBoxGBFDJZL.Text + "','" +
                TextBoxGBDJZL.Text + "','" + TextBoxZZJSJE.Text + "','" +
                TextBoxZCJSJE.Text + "','" + TextBoxBZ.Text + "','" + TextBoxYEAR.Text + "','" +
                Session["UserID"].ToString() + "','" + DateTime.Now + "')";
            DBCallCommon.ExeSqlText(sql);
            Response.Redirect("SM_Trans_GNFYEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"].ToString() == "NEW")
            {
                string sql = "INSERT INTO TBTM_GNFY(GNFY_PROJECT,GNFY_HTBH,GNFY_HTJE,GNFY_RZB," +
                    "GNFY_LLSTARTDATE,GNFY_LLENDDATE,GNFY_LLFDJZL,GNFY_LLDJZL,GNFY_LLZTJ," +
                    "GNFY_LLZZL,GNFY_LLCC,GNFY_GBFDJZL,GNFY_GBDJZL,GNFY_ZZJSJE," +
                    "GNFY_ZCJSJE,GNFY_BZ,GNFY_YEAR,GNFY_CLERKID,GNFY_DATE)" +
                    "VALUES('" + TextBoxProject.Text + "','" + TextBoxHTBH.Text + "','" + TextBoxHTJE.Text + "','" +
                    TextBoxRZB.Text + "','" + TextBoxLLSTARTDATE.Text + "','" + TextBoxLLENDDATE.Text + "','" +
                    TextBoxLLFDJZL.Text + "','" + TextBoxLLDJZL.Text + "','" + TextBoxLLZTJ.Text + "','" +
                    TextBoxLLZZL.Text + "','" + TextBoxLLCC.Text + "','" + TextBoxGBFDJZL.Text + "','" +
                    TextBoxGBDJZL.Text + "','" + TextBoxZZJSJE.Text + "','" +
                    TextBoxZCJSJE.Text + "','" + TextBoxBZ.Text + "','" + TextBoxYEAR.Text + "','" +
                    Session["UserID"].ToString() + "','" + DateTime.Now + "')";
                DBCallCommon.ExeSqlText(sql);
                Confirm.Enabled = false;
            }
            if (Request.QueryString["FLAG"].ToString() == "EDIT")
            {
                string sql = "UPDATE TBTM_GNFY SET GNFY_PROJECT='" + TextBoxProject.Text + "',GNFY_HTBH='" + TextBoxHTBH.Text + "'," +
                    "GNFY_HTJE='" + TextBoxHTJE.Text + "',GNFY_RZB='" + TextBoxRZB.Text + "'," +
                    "GNFY_LLSTARTDATE='" + TextBoxLLSTARTDATE.Text + "',GNFY_LLENDDATE='" + TextBoxLLENDDATE.Text + "'," +
                    "GNFY_LLFDJZL='" + TextBoxLLFDJZL.Text + "',GNFY_LLDJZL='" + TextBoxLLDJZL.Text + "'," +
                    "GNFY_LLZTJ='" + TextBoxLLZTJ.Text + "',GNFY_LLZZL='" + TextBoxLLZZL.Text + "',GNFY_LLCC='" + TextBoxLLCC.Text + "'," +
                    "GNFY_GBFDJZL='" + TextBoxGBFDJZL.Text + "',GNFY_GBDJZL='" + TextBoxGBDJZL.Text + "'," +
                    "GNFY_ZZJSJE='" + TextBoxZZJSJE.Text + "'," +
                    "GNFY_ZCJSJE='" + TextBoxZCJSJE.Text + "',GNFY_BZ='" + TextBoxBZ.Text + "',GNFY_YEAR='" + TextBoxYEAR.Text + "'," +
                    "GNFY_CLERKID='" + Session["UserID"].ToString() + "'," +
                    "GNFY_DATE='" + DateTime.Now+ "' WHERE GNFY_ID='" + Request.QueryString["ID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
            }
            Response.Redirect("SM_Trans_Manage.aspx?TAB=4");
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_Manage.aspx?TAB=4");
        }


    }
}
