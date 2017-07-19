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
using System.Collections.Generic;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Itemsfilter : System.Web.UI.Page
    {
        public string tablenmid_code
        {
            get
            {
                object str = ViewState["tablenmid_code"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["tablenmid_code"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
            }
        }
        private void InitInfo()
        {
            if (Request.QueryString["tablenmid_code"] != null)
            {
                tablenmid_code = Request.QueryString["tablenmid_code"].ToString();
            }
            else
            {
                tablenmid_code = "";
            }
            //tablenmid_code = "View_TBPC_IQRCMPPRICE_RVW/0602004";
            databind();
        }
        private void databind()
        {
            string sqltext = "SELECT tableuser, connect, columnid, columnnm, filter, expression, value "+
                             "FROM TBPC_COLUMNFILTER_INFO where tableuser='" + tablenmid_code + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int rowsnum = dt.Rows.Count;
            for (int i = rowsnum + 1; i <= 10; i++)
            {
                DataRow dr = dt.NewRow();
                dr["tableuser"] = "";
                dr["connect"] = "";
                dr["columnid"] = "";
                dr["columnnm"] = "";
                dr["filter"] = "";
                dr["expression"] = "";
                dr["value"] = "";
                dt.Rows.Add(dr);
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        public DataTable ddlcolumnnmbind() 
        {
            string tableid = tablenmid_code.Split('/')[0].ToString();
            string code = tablenmid_code.Split('/')[1].ToString();
            string sqltext = "select distinct case when COLUMNNM is null then N'' else  COLUMNNM end AS ddlcolumnnm,"+
                             "case when COLUMNNM is null then N'' else COLUMNID end AS ddlcolumnid  " +
                             "from TBPC_TABLECOLUMN_INFO WHERE TABLENM='" + tableid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DataRow dr = dt.NewRow();
            dr["ddlcolumnnm"] = "";
            dr["ddlcolumnid"] = "";
            dt.Rows.InsertAt(dr, 0);
            return dt;
        }
        protected void btn_chongzhi_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext = "delete from TBPC_FILTER_INFO where tableuser='" + tablenmid_code + "'";
            DBCallCommon.ExeSqlText(sqltext);
            sqltext = "delete from TBPC_COLUMNFILTER_INFO where tableuser='" + tablenmid_code + "'";
            DBCallCommon.ExeSqlText(sqltext);
            databind();
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            CreateNewRow();
        }
        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            string allfilter = "";
            string tableuser = tablenmid_code;
            string connect; 
            string columnid;
            string columnnm;
            string filter;
            string expressionid;
            string expressionnmvalue;
            string value;
            int j = 0;
            int k = 0;
            List<string> sqltextlist = new List<string>();
            sqltext = "delete from TBPC_FILTER_INFO where tableuser='" + tableuser + "'";
            sqltextlist.Add(sqltext);
            sqltext = "delete  FROM TBPC_COLUMNFILTER_INFO where tableuser='" + tableuser + "'";
            sqltextlist.Add(sqltext);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                DropDownList drpconnect = (DropDownList)gr.FindControl("Drp_connect");
                DropDownList drpolumn = (DropDownList)gr.FindControl("Drp_columnnm");
                DropDownList drpexpressionid = (DropDownList)gr.FindControl("Drp_expression");
                TextBox hidcolumn = (TextBox)gr.FindControl("hid_column");
                TextBox tbcolumnnm = (TextBox)gr.FindControl("tb_columnnm");
                TextBox tbcolumnid = (TextBox)gr.FindControl("tb_columnid");
                TextBox hidconnect = (TextBox)gr.FindControl("hid_connect");
                TextBox tbconnectid = (TextBox)gr.FindControl("tb_connectid");
                TextBox hidexpression = (TextBox)gr.FindControl("hid_expression");
                TextBox tbexpressionid = (TextBox)gr.FindControl("tb_expressionid");
                filter = "";
                value = ((TextBox)gr.FindControl("tb_value")).Text;
                if (hidconnect.Text == "1")
                {
                    connect = drpconnect.SelectedValue.ToString();
                }
                else
                {
                    connect = tbconnectid.Text;
                }
                if (hidcolumn.Text == "1")
                {
                    columnid = drpolumn.SelectedValue.ToString();
                    columnnm = drpolumn.SelectedItem.Text;
                }
                else
                {
                    columnid = tbcolumnid.Text;
                    columnnm = tbcolumnnm.Text;
                }
                if (hidexpression.Text == "1")
                {
                    expressionid = drpexpressionid.SelectedValue.ToString();
                }
                else
                {
                    expressionid = tbexpressionid.Text;
                }
                if (connect != "" && columnid != "" && columnnm != "" && expressionid != "")
                {
                    j++;
                    k++;
                    switch (expressionid)
                    {
                        case "0":
                            {
                                expressionnmvalue = "=''" + value + "''";
                                break;
                            }
                        case "1":
                            {
                                expressionnmvalue = ">''" + value + "''";
                                break;
                            }
                        case "2":
                            {
                                expressionnmvalue = "<''" + value + "''";
                                break;
                            }
                        case "3":
                            {
                                expressionnmvalue = ">=''" + value + "''";
                                break;
                            }
                        case "4":
                            {
                                expressionnmvalue = "<=''" + value + "''";
                                break;
                            }
                        case "5":
                            {
                                expressionnmvalue = "LIKE ''%"+value+"%''";
                                break;
                            }
                        case "6":
                            {
                                expressionnmvalue = "NOT LIKE ''%" + value + "%''";
                                break;
                            }
                        default:
                            {
                                expressionnmvalue = "=''" + value + "''";
                                break;
                            }
                    }
                    if (j == 1)
                    {
                        allfilter = columnid + "  " + expressionnmvalue;
                    }
                    else
                    {
                        allfilter = allfilter + "  " + connect + "  " + columnid + "  " + expressionnmvalue;
                    }
                    sqltext = "insert into TBPC_COLUMNFILTER_INFO(tableuser, connect, columnid, columnnm, filter, expression, value)  " +
                              "values('" + tableuser + "','" + connect + "','" + columnid + "','" + columnnm + "','" + filter + "','" + expressionid + "','" + value + "')";
                    sqltextlist.Add(sqltext);
                }
            }
            if (k >= 1)
            {
                sqltext = "insert into TBPC_FILTER_INFO(tableuser, filter) values ('" + tableuser + "','" + allfilter + "')";
                sqltextlist.Add(sqltext);
            }
            DBCallCommon.ExecuteTrans(sqltextlist);
            //Response.Redirect("TBPC_IQRCMPPRCLST_checked.aspx");
            //Response.Write("<script>javascript:window.close();</script>");

            //Response.Write("<script>window.dialogArguments.location.assign('TBPC_IQRCMPPRCLST_checked.aspx'); self.close();</script>");
            Response.Write("<script>javascript:window.close();</script>");


        }
        protected void btn_concel_Click(object sender, EventArgs e)
        {
            //Response.Redirect("TBPC_IQRCMPPRCLST_checked.aspx");
            Response.Write("<script>javascript:window.close();</script>");
        }
        //生成输入1行函数
        private void CreateNewRow()
        {
            DataTable dt = this.GetDataTable();
            DataRow newRow = dt.NewRow();
            dt.Rows.Add(newRow);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        //定义DataTable
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("tableuser");
            dt.Columns.Add("connect");
            dt.Columns.Add("columnid");
            dt.Columns.Add("columnnm");
            dt.Columns.Add("filter");
            dt.Columns.Add("expression");
            dt.Columns.Add("value");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = "";
                newRow[1] = ((DropDownList)gr.FindControl("Drp_connect")).SelectedValue.ToString();
                newRow[2] = ((DropDownList)gr.FindControl("Drp_columnnm")).SelectedValue.ToString();
                newRow[3] = ((DropDownList)gr.FindControl("Drp_columnnm")).SelectedItem.Text;
                newRow[4] = "";
                newRow[5] = ((DropDownList)gr.FindControl("Drp_expression")).SelectedValue.ToString();
                newRow[6] = ((TextBox)gr.FindControl("tb_value")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }
        public string get_connecttext(string i)
        {
            string str="";
            switch (i)
            {
                case "and":
                    str = "且";
                    break;
                case "or":
                    str = "或";
                    break;
                default:
                    str = "";
                    break;
            }
            return str;
        }
        public string get_expressiontext(string i)
        {
            string str = "";
            switch (i)
            {
                case "0":
                    str = "等于";
                    break;
                case "1":
                    str = "大于";
                    break;
                case "2":
                    str = "小于";
                    break;
                case "3":
                    str = "大于等于";
                    break;
                case "4":
                    str = "小于等于";
                    break;
                case "5":
                    str = "包含";
                    break;
                case "6":
                    str = "不包含";
                    break;
                default:
                    str = "";
                    break;
            }
            return str;
        }

        protected void GridView1_PreRender(object sender, EventArgs e)
        {
            this.databind();
        }
    }
}
