using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_YOUQI_datail : System.Web.UI.Page
    {
        string sqlText;
        string action;
        string id = "";
        string level;
        string status;
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];
            id = Request.QueryString["id"];
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            if (!IsPostBack)
            {
                InitInfo();
            }
        }
        private void InitInfo()
        {
            if (Request.QueryString["id"] != null)
            {
                action = Request.QueryString["action"];
                id = Request.QueryString["id"];
            }

            sqlText = "select PS_ID,CM_PROJ,PS_PJID,TSA_ENGNAME,PS_PAINTBRAND,PS_SUBMITNM,";
            sqlText += "PS_SUBMITTM,PS_REVIEWANM,PS_REVIEWAADVC,PS_REVIEWATIME,";
            sqlText += "PS_REVIEWBNM,PS_REVIEWBADVC,PS_REVIEWBTIME,PS_REVIEWCNM,";
            sqlText += "PS_REVIEWCADVC,PS_REVIEWCTIME,PS_STATE,PS_CHECKLEVEL,PS_PJID,PS_SUBMITID ";
            sqlText += "from VIEW_TM_PAINTSCHEME where PS_ID='" + id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                ps_no.Text = dr["PS_ID"].ToString();
                proname.Text = dr["CM_PROJ"].ToString();
                tsaid.Text = dr["PS_PJID"].ToString();
                engname.Text = dr["TSA_ENGNAME"].ToString();
                paint_brand.Text = dr["PS_PAINTBRAND"].ToString();
                plandate.Text = dr["PS_SUBMITTM"].ToString();
                status = dr["PS_STATE"].ToString();
                ViewState["status"] = dr["PS_STATE"].ToString();
                ViewState["level"] = dr["PS_CHECKLEVEL"].ToString();
                level = dr["PS_CHECKLEVEL"].ToString();
                proid.Value = dr["PS_PJID"].ToString();
            }
            dr.Close();
            sqlText = "select * from View_TM_PAINTSCHEMEDETAIL where PS_PID='" + ps_no.Text + "' ";
            DBCallCommon.BindRepeater(Repeater1, sqlText);
        }

        protected void btnChuli_OnClick(object sender, EventArgs e)
        {
            string sqltxt = "select A.*,B.MTA_DUY from VIEW_TM_PAINTSCHEME AS A left join  TBMP_MANUTSASSGN AS B on A.PS_ENGID=B.MTA_ID where PS_ID='" + id + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltxt);
            string mtaduy = dt.Rows[0]["MTA_DUY"].ToString();
            string username = Session["UserName"].ToString();
            if (mtaduy == username)
            {
                string sqltext = "update TBPM_PAINTSCHEME set PS_LOOKSTATUS='1' where PS_ID='" + id + "'";
                DBCallCommon.ExeSqlText(sqltext);
                Response.Write("<script>alert('您已成功处理该项目！！！！')</script>");
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您已成功处理该项目！！！')", true);
            }
        }

        protected void rpt_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("tr");
                string PS_BGBEIZHU = ((Label)e.Item.FindControl("PS_BGBEIZHU")).Text.Trim();
                if (PS_BGBEIZHU != "")
                {
                    tr.BgColor = "Green";
                }
            }

        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("PM_YOUQI.aspx");
        }


    }
}
