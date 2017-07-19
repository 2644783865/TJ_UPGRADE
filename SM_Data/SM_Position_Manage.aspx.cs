using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Position_Manage : System.Web.UI.Page
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
                    string tempstr = "";
                    string sql = "SELECT DISTINCT WL_WSID FROM TBWS_LOCATION WHERE WL_ID='" + Request.QueryString["ID"] + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        tempstr = dr["WL_WSID"].ToString();
                    }
                    dr.Close();
                    DropDownListParent.Items.FindByValue(tempstr).Selected = true;
                }
                GetData();
            }
        }

        private void GetParent()
        {
            //得到子仓库

            string sql = "SELECT DISTINCT WS_ID,WS_NAME FROM TBWS_WAREHOUSE WHERE WS_FATHERID<>'ROOT' ORDER BY WS_ID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListParent.DataSource = dt;
            DropDownListParent.DataTextField = "WS_NAME";
            DropDownListParent.DataValueField = "WS_ID";
            DropDownListParent.DataBind();
        }

        private void GetData()
        {
            DataTable tb = new DataTable();
            string sqlorder = " a.WL_ID ";
            if (DropDownListOrderBy.SelectedValue != "")
            {
                sqlorder = DropDownListOrderBy.SelectedValue;
            }
            string sql = "";
            sql = "SELECT a.WL_ID AS ID,a.WL_NAME AS NAME,a.WL_FILLDATE AS FILLDATE,b.ST_NAME AS CLERK,a.WL_NOTE AS NOTE " +
                "FROM TBWS_LOCATION a INNER JOIN TBDS_STAFFINFO b ON a.WL_CLERK=b.ST_ID " +
                "WHERE a.WL_WSID ='" + DropDownListParent.SelectedValue + "'and a.WL_NAME like '%" + txtname.Text + "%' ORDER BY  " + sqlorder + " ";
            tb = DBCallCommon.GetDTUsingSqlText(sql);
            Repeater1.DataSource = tb;
            Repeater1.DataBind();
            if (tb.Rows.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            tb.Clear();
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Position_Edit.aspx?FLAG=ADD&&ID=NO");
        }

        //正常情况下不允许删除仓位信息
        protected void Delete_Click(object sender, EventArgs e)
        {
            string sql = "";
            List<string> sqllist = new List<string>();
            foreach (RepeaterItem LabelID in Repeater1.Items)
            {
                CheckBox chk = (CheckBox)LabelID.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    string id = ((Label)LabelID.FindControl("LabelID")).Text;
                    sql = "DELETE FROM TBWS_LOCATION WHERE WL_ID='" + id + "'";
                    sqllist.Add(sql);
                }
            }
            if (sqllist.Count > 0)
            {
                DBCallCommon.ExecuteTrans(sqllist);
                GetData();
            }
        }

        protected void LinkTo_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Warehouse_Manage.aspx");
        }

        protected void DropDownListParent_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtname.Text = "";
            GetData();
        }

        protected void search_click(object sender, EventArgs e)
        {

            GetData();
        }
        protected void DropDownListOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }


    }
}
