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
    public partial class SM_Trans_CNJGEdit : System.Web.UI.Page
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
                    string sql = "SELECT * FROM TBTM_CNJG WHERE CNJG_ID='" + Request.QueryString["ID"].ToString() + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        TextBoxYEAR.Text = dr["CNJG_YEAR"].ToString();
                        TextBoxCC.Text = dr["CNJG_CC"].ToString();
                        TextBoxMDG.Text = dr["CNJG_MDG"].ToString();
                        TextBoxRZB.Text = dr["CNJG_RZB"].ToString();
                        TextBoxHTH.Text = dr["CNJG_HTH"].ToString();
                        TextBoxLLSTARTDATE.Text = dr["CNJG_LLSTARTDATE"].ToString();
                        TextBoxLLENDDATE.Text = dr["CNJG_LLENDDATE"].ToString();
                        TextBoxLLFDJZL.Text = dr["CNJG_LLFDJZL"].ToString();
                        TextBoxLLDJZL.Text = dr["CNJG_LLDJZL"].ToString();
                        TextBoxLLZTJ.Text = dr["CNJG_LLZTJ"].ToString();
                        TextBoxLLZZL.Text = dr["CNJG_LLZZL"].ToString();
                        TextBoxGBFDJZL.Text = dr["CNJG_GBFDJZL"].ToString();
                        TextBoxGBDJZL.Text = dr["CNJG_GBDJZL"].ToString();
                        TextBoxGBJE.Text = dr["CNJG_GBJE"].ToString();
                        TextBoxZCJSZL.Text = dr["CNJG_ZCJSZL"].ToString();
                        TextBoxZCJSJE.Text = dr["CNJG_ZCJSJE"].ToString();
                        TextBoxBZ.Text = dr["CNJG_BZ"].ToString();
                    }
                    dr.Close();
                    LabelDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    LabelClerk.Text = Session["UserName"].ToString();
                }
            }
        }

        protected void Continue_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO TBTM_CNJG(CNJG_CC,CNJG_MDG,CNJG_RZB,CNJG_HTH," +
                "CNJG_LLSTARTDATE,CNJG_LLENDDATE,CNJG_LLFDJZL,CNJG_LLDJZL,CNJG_LLZTJ," +
                "CNJG_LLZZL,CNJG_LLCC,CNJG_GBFDJZL,CNJG_GBDJZL,CNJG_GBJE,CNJG_ZCJSZL," +
                "CNJG_ZCJSJE,CNJG_BZ,CNJG_YEAR,CNJG_CLERK,CNJG_DATE)" +
                "VALUES('" + TextBoxCC.Text + "','" + TextBoxMDG.Text + "','" + TextBoxRZB.Text + "','" +
                TextBoxHTH.Text + "','" + TextBoxLLSTARTDATE.Text + "','" + TextBoxLLENDDATE.Text + "','" +
                TextBoxLLFDJZL.Text + "','" + TextBoxLLDJZL.Text + "','" + TextBoxLLZTJ.Text + "','" +
                TextBoxLLZZL.Text + "','" + TextBoxLLCC.Text + "','" + TextBoxGBFDJZL.Text + "','" +
                TextBoxGBDJZL.Text + "','" + TextBoxGBJE.Text + "','" + TextBoxZCJSZL.Text + "','" +
                TextBoxZCJSJE.Text + "','" + TextBoxBZ.Text + "','" + TextBoxYEAR.Text + "','" +
                Session["UserID"].ToString() + "','" + DateTime.Now + "')";
            DBCallCommon.ExeSqlText(sql);
            Response.Redirect("SM_Trans_CNJGEdit.aspx?FLAG=NEW&&ID=NEW");
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"].ToString() == "NEW")
            {
                string sql = "INSERT INTO TBTM_CNJG(CNJG_CC,CNJG_MDG,CNJG_RZB,CNJG_HTH," +
                    "CNJG_LLSTARTDATE,CNJG_LLENDDATE,CNJG_LLFDJZL,CNJG_LLDJZL,CNJG_LLZTJ," +
                    "CNJG_LLZZL,CNJG_LLCC,CNJG_GBFDJZL,CNJG_GBDJZL,CNJG_GBJE,CNJG_ZCJSZL," +
                    "CNJG_ZCJSJE,CNJG_BZ,CNJG_YEAR,CNJG_CLERK,CNJG_DATE)" +
                    "VALUES('" + TextBoxCC.Text + "','" + TextBoxMDG.Text + "','" + TextBoxRZB.Text + "','" +
                    TextBoxHTH.Text + "','" + TextBoxLLSTARTDATE.Text + "','" + TextBoxLLENDDATE.Text + "','" +
                    TextBoxLLFDJZL.Text + "','" + TextBoxLLDJZL.Text + "','" + TextBoxLLZTJ.Text + "','" +
                    TextBoxLLZZL.Text + "','" + TextBoxLLCC.Text + "','" + TextBoxGBFDJZL.Text + "','" +
                    TextBoxGBDJZL.Text + "','" + TextBoxGBJE.Text + "','" + TextBoxZCJSZL.Text + "','" +
                    TextBoxZCJSJE.Text + "','" + TextBoxBZ.Text + "','" + TextBoxYEAR.Text + "','" +
                    Session["UserID"].ToString() + "','" + DateTime.Now + "')";
                DBCallCommon.ExeSqlText(sql);
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>closewin();</script>");
            }
            if (Request.QueryString["FLAG"].ToString() == "EDIT")
            {
                string sql = "UPDATE TBTM_CNJG SET CNJG_CC='" + TextBoxCC.Text + "',CNJG_MDG='" + TextBoxMDG.Text + "'," +
                    "CNJG_RZB='" + TextBoxRZB.Text + "',CNJG_HTH='" + TextBoxHTH.Text + "'," +
                    "CNJG_LLSTARTDATE='" + TextBoxLLSTARTDATE.Text + "',CNJG_LLENDDATE='" + TextBoxLLENDDATE.Text + "'," +
                    "CNJG_LLFDJZL='" + TextBoxLLFDJZL.Text +"',CNJG_LLDJZL='" + TextBoxLLDJZL.Text + "'," +
                    "CNJG_LLZTJ='" + TextBoxLLZTJ.Text +"',CNJG_LLZZL='" + TextBoxLLZZL.Text + "',CNJG_LLCC='" + TextBoxLLCC.Text + "'," + 
                    "CNJG_GBFDJZL='" + TextBoxGBFDJZL.Text + "',CNJG_GBDJZL='" + TextBoxGBDJZL.Text + "'," +
                    "CNJG_GBJE='" + TextBoxGBJE.Text + "',CNJG_ZCJSZL='" + TextBoxZCJSZL.Text + "'," +
                    "CNJG_ZCJSJE='" + TextBoxZCJSJE.Text +"',CNJG_BZ='" + TextBoxBZ.Text + "',CNJG_YEAR='" + TextBoxYEAR.Text +"'," +
                    "CNJG_CLERK='" + Session["UserID"].ToString() +"'," +
                    "CNJG_DATE='" + DateTime.Now + "' WHERE CNJG_ID='" + Request.QueryString["ID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>closewin();</script>");
            }
            Response.Redirect("SM_Trans_CNJG.aspx");
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Trans_CNJG.aspx");
        }

    }
}
