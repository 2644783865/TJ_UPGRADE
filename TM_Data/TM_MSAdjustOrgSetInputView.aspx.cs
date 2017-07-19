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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MSAdjustOrgSetInputView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            if (!IsPostBack)
            {
                string retvalue = this.GetQueryCondition();
                if (retvalue == "OK")
                {
                    this.InitVarBindData();
                }
                else if (retvalue == "Null")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法获取查询对象，请重新操作');window.close();", true);
                }
            }
        }

        protected string GetQueryCondition()
        {
            string sql = "select WhereCondition from TBPM_TEMPWHERECONDITION where USERID='" + Session["UserID"].ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                ViewState["Where"] = dt.Rows[0][0].ToString();
                string sql_delete = "delete from TBPM_TEMPWHERECONDITION where USERID='" + Session["UserID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql_delete);
                return "OK";
            }
            else
            {
                return "Null";
            }
        }

        protected void InitVarBindData()
        {
            string _table_where = ViewState["Where"].ToString().Replace("^", "'");
            string[] a = _table_where.Split('$');
            string _table = a[0];
            string _where = a[1];
            string sql = "select * from " + _table + " where  " + _where + "";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                NoDataPanel1.Visible = false;
            }
            else
            {
                NoDataPanel1.Visible = true;
            }
        }
    }
}
