using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class WL_Type_List : BasicPage
    {
        System.Data.SqlClient.SqlConnection sqlConn = new SqlConnection();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            btnDelete.Click += new EventHandler(btnDelete_Click);
            btnDelete.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
            if (!IsPostBack)
            {
                GetParent();
                ddlTclass.Items[0].Selected = true;
                if (Request.QueryString["ID"] != null)
                {
                    string sql = "SELECT * FROM TBMA_TYPEINFO WHERE TY_ID='" + Request.QueryString["ID"] + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql); ;
                    if (dr.Read())
                    {
                        ddlTclass.SelectedValue = dr["TY_NAME"].ToString();
                    }
                    dr.Close();
                }
                GetData();
            }
            CheckUser(ControlFinder);
        }

        private void GetParent()
        {
            string sql = "SELECT DISTINCT TY_ID,TY_NAME FROM TBMA_TYPEINFO where TY_FATHERID='ROOT' ORDER BY TY_NAME";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddlTclass.DataSource = dt;
            ddlTclass.DataTextField = "TY_NAME";
            ddlTclass.DataValueField = "TY_ID";
            ddlTclass.DataBind();
            if (ddlTclass.Items.Count == 0)
            {
                ddlTclass.Items.Add("ROOT");
            }
        }

        private void GetData()
        {
            DataTable tb = new DataTable();
            string sql = "";
            sql = "SELECT TY_ID AS id,TY_NAME AS name,TY_FILLDATE AS filldate,TY_CLERK AS person,TY_STATE  AS status,TY_NOTE AS comment FROM TBMA_TYPEINFO WHERE TY_FATHERID='" + ddlTclass.SelectedItem.Value + "'";
            tb = DBCallCommon.GetDTUsingSqlText(sql);
            Repeater1.DataSource = tb;
            Repeater1.DataBind();
            if (tb.Rows.Count == 0)
            {
                NoDataPanel.Visible = true;
                CheckUser(ControlFinder);
            }
            tb.Clear();
        }

        //删除功能建议在使用时隐藏
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string strID = "";
            foreach (RepeaterItem LabelID in Repeater1.Items)
            {
                CheckBox chk = (CheckBox)LabelID.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    string check = ((Label)LabelID.FindControl("LabelID")).Text;
                    string sql = "SELECT * FROM TBMA_TYPEINFO WHERE TY_NAME IN ( SELECT DISTINCT TY_NAME FROM TBMA_TYPEINFO WHERE TY_FATHERID='" + check + "' )";
                    DataTable table = new DataTable();
                    table = DBCallCommon.GetDTUsingSqlText(sql);
                    if (table.Rows.Count != 0)
                    {
                       message.Text = "编号为" + check + "的大类存在子类！删除操作无法进行。";
                        return;
                    }
                    strID += "'" + ((Label)LabelID.FindControl("LabelID")).Text + "'" + ",";
                }
            }
            if (strID.Length > 1)
            {
                strID = strID.Substring(0, strID.Length - 1);
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("DELETE FROM TBMA_TYPEINFO WHERE TY_ID IN (" + strID + ")", sqlConn);
                sqlConn.Open();
                SqlDataReader dr = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                dr.Close();
                GetData();
            }
        }

        protected void ddlTclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        protected string convertStatus(string status)
        {
            if (status == "1")
                return "在用";
            else
                return "停用";
        }
        protected string editWl(string WlId)
        {
            return "javascript:window.showModalDialog('WL_Type_Edit.aspx?FLAG=MODI&&ID=" + WlId + "','','DialogWidth=650px;DialogHeight=400px')";
        }

    }
}
