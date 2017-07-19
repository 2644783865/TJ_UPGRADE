using System;
using System.Collections;
using System.Collections.Generic;
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

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_Manage : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Delete.Click += new EventHandler(Delete_Click);
            Delete.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");

            if (!IsPostBack)
            {
                GetParent();
                if (Request.QueryString["ID"] != null)
                {
                    string pid = "";
                    string sql = "SELECT WS_FATHERID FROM TBWS_WAREHOUSE WHERE WS_ID='" + Request.QueryString["ID"] + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        pid = dr["WS_FATHERID"].ToString();
                    }
                    dr.Close();
                    DDLParent.Items.FindByValue(pid).Selected = true;
                }
                GetData();
            }

            CheckUser(ControlFinder);
        }

        //获取仓库大类编码和名称
        private void GetParent()
        {
            string sql = "SELECT DISTINCT WS_ID,WS_NAME FROM TBWS_WAREHOUSE WHERE WS_FATHERID='ROOT' ORDER BY WS_ID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DDLParent.DataSource = dt;
            DDLParent.DataTextField = "WS_NAME";
            DDLParent.DataValueField = "WS_ID";
            DDLParent.DataBind();
            ListItem item = new ListItem("ROOT", "ROOT");
            DDLParent.Items.Add(item);
        }

        private void GetData()
        {
            DataTable tb = new DataTable();
            string sql = "";
            if (DDLParent.SelectedValue == "ROOT")
            {
                sql = "SELECT a.WS_ID AS ID,a.WS_NAME AS NAME,a.WS_FILLDATE AS FILLDATE,b.ST_NAME AS CLERK,a.WS_NOTE AS NOTE " +
                    "FROM TBWS_WAREHOUSE a INNER JOIN TBDS_STAFFINFO b ON a.WS_CLERK=b.ST_ID " +
                    "WHERE a.WS_FATHERID='ROOT' ORDER BY a.WS_ID";
            }
            else
            {
                sql = "SELECT a.WS_ID AS ID,a.WS_NAME AS NAME,a.WS_FILLDATE AS FILLDATE,b.ST_NAME AS CLERK,a.WS_NOTE AS NOTE " +
                    "FROM TBWS_WAREHOUSE a INNER JOIN TBDS_STAFFINFO b ON a.WS_CLERK=b.ST_ID " + 
                    "WHERE a.WS_FATHERID='" + DDLParent.SelectedValue + "' ORDER BY a.WS_ID";
            }
            tb = DBCallCommon.GetDTUsingSqlText(sql);
            Repeater1.DataSource = tb;
            Repeater1.DataBind();
            if (tb.Rows.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            else
            {
                NoDataPanel.Visible = false;
            }
            tb.Clear();
        }

        /*添加父仓库*/
        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Warehouse_Edit.aspx?FLAG=ADD&&ID=NO");
        }

        //正常情况下不允许删除仓库信息
        protected void Delete_Click(object sender, EventArgs e)
        {
            List<string> sqllist = new List<string>();
            foreach (RepeaterItem LabelID in Repeater1.Items)
            {
                CheckBox chk = (CheckBox)LabelID.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    string check = ((Label)LabelID.FindControl("LabelID")).Text;
                    string sql = "SELECT * FROM TBWS_WAREHOUSE WHERE WS_FATHERID='" + check + "'";
                    DataTable table = new DataTable();
                    table = DBCallCommon.GetDTUsingSqlText(sql);
                    if (table.Rows.Count != 0)
                    {
                        LabelMessage.Text = "编号为" + check + "的仓库存在子库！删除操作无法进行。";
                        return;
                    }
                    sql = "DELETE FROM TBWS_WAREHOUSE WHERE WS_ID='" + check + "'";
                    sqllist.Add(sql);
                }
            }
            if (sqllist.Count > 0)
            {
                DBCallCommon.ExecuteTrans(sqllist);
                GetData();
                GetParent();
                return;
            }
            if(sqllist.Count == 0)
            {
                LabelMessage.Text = "请先选择要删除的仓库！";
            }
        }
        //直接到仓位管理
        protected void LinkTo_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Position_Manage.aspx");
        }

        protected void DDLParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
            CheckUser(ControlFinder);
        }

    }
}
