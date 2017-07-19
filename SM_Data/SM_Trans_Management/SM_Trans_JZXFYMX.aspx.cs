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
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.SM_Data.SM_Trans_Management
{
    public partial class SM_Trans_JZXFYMX : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                databind();
            }
            CheckUser(ControlFinder);
        }

        private void databind()
        {
            GetYear();
            GetProject();
            GetEngeering();

            GetJZXFYMX();
        }

        protected void GetYear()
        {
            //集装箱发运明细按照集装箱发运总信息查询
            string sql = "SELECT DISTINCT JZXFY_YEAR  FROM TBTM_JZXFY";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListYear.DataTextField = "JZXFY_YEAR";
            DropDownListYear.DataValueField = "JZXFY_YEAR";
            DropDownListYear.DataSource = dt;
            DropDownListYear.DataBind();
        }

        protected void GetProject()
        {
            string sql = "SELECT DISTINCT JZXFY_PROJECT  FROM TBTM_JZXFY";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListproject.DataTextField = "JZXFY_PROJECT";
            DropDownListproject.DataValueField = "JZXFY_PROJECT";
            DropDownListproject.DataSource = dt;
            DropDownListproject.DataBind();
            DropDownListproject.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            DropDownListproject.SelectedIndex = 0;
        }

        protected void GetEngeering()
        {
            string sql = "SELECT DISTINCT JZXFY_FYPC  FROM TBTM_JZXFY where JZXFY_PROJECT='" + DropDownListproject.SelectedValue + "' ";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListengeering.DataTextField = "JZXFY_FYPC";
            DropDownListengeering.DataValueField = "JZXFY_FYPC";
            DropDownListengeering.DataSource = dt;
            DropDownListengeering.DataBind();
            DropDownListengeering.Items.Insert(0, new ListItem(" ", " "));
            DropDownListengeering.SelectedIndex = 0;
        }

        protected void GetJZXFYMX()
        {
            string sql = "";

            if (DropDownListproject.SelectedValue != "-请选择-")
            {
                if (DropDownListengeering.SelectedValue.Trim() != "")
                {
                    sql = "SELECT a.JZXFYDETAIL_ID AS JZXFYMXID,b.JZXFY_PROJECT AS JZXFYPROJECT,b.JZXFY_FYPC AS JZXFYFYPC,b.JZXFY_TRANSDATE AS JZXFYTRANSDATE," +
                  "a.JZXFYDETAIL_GOODNAME AS JZXFYMXGOODNAME,a.JZXFYDETAIL_SCZH AS JZXFYMXSCZH,a.JZXFYDETAIL_HWZL AS JZXFYMXHWZL " +
                  "FROM TBTM_JZXFYDETAIL a INNER JOIN TBTM_JZXFY b ON a.JZXFYDETAIL_JZXFYID=b.JZXFY_ID " +
                  "WHERE b.JZXFY_YEAR='" + DropDownListYear.SelectedValue.ToString() + "' and b.JZXFY_PROJECT='" + DropDownListproject.SelectedValue + "' and b.JZXFY_FYPC='" + DropDownListengeering.SelectedValue + "'";
                }
                else
                {
                    sql = "SELECT a.JZXFYDETAIL_ID AS JZXFYMXID,b.JZXFY_PROJECT AS JZXFYPROJECT,b.JZXFY_FYPC AS JZXFYFYPC,b.JZXFY_TRANSDATE AS JZXFYTRANSDATE," +
                   "a.JZXFYDETAIL_GOODNAME AS JZXFYMXGOODNAME,a.JZXFYDETAIL_SCZH AS JZXFYMXSCZH,a.JZXFYDETAIL_HWZL AS JZXFYMXHWZL " +
                   "FROM TBTM_JZXFYDETAIL a INNER JOIN TBTM_JZXFY b ON a.JZXFYDETAIL_JZXFYID=b.JZXFY_ID " +
                   "WHERE b.JZXFY_YEAR='" + DropDownListYear.SelectedValue.ToString() + "' and b.JZXFY_PROJECT='" + DropDownListproject.SelectedValue + "'";
                }
            }
            else
            {
                sql = "SELECT a.JZXFYDETAIL_ID AS JZXFYMXID,b.JZXFY_PROJECT AS JZXFYPROJECT,b.JZXFY_FYPC AS JZXFYFYPC,b.JZXFY_TRANSDATE AS JZXFYTRANSDATE," +
                  "a.JZXFYDETAIL_GOODNAME AS JZXFYMXGOODNAME,a.JZXFYDETAIL_SCZH AS JZXFYMXSCZH,a.JZXFYDETAIL_HWZL AS JZXFYMXHWZL " +
                  "FROM TBTM_JZXFYDETAIL a INNER JOIN TBTM_JZXFY b ON a.JZXFYDETAIL_JZXFYID=b.JZXFY_ID " +
                  "WHERE b.JZXFY_YEAR='" + DropDownListYear.SelectedValue.ToString() + "'";
            }
            
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterJZXFYMX.DataSource = dt;
            RepeaterJZXFYMX.DataBind();
            if (dt.Rows.Count == 0)
            {
                Panel10.Visible = true;
            }
            else
            {
                Panel10.Visible = false;
            }
        }

        protected void RepeaterJZXFYMX_ItemDataBound(object sender, EventArgs e)
        {

        }

        protected void DropDownListYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetJZXFYMX();
        }

        protected void DropDownListPJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetEngeering();
            GetJZXFYMX();
        }

        protected void DropDownListENG_SelectedIndexChanged(object sender, EventArgs e)
        {

            GetJZXFYMX();
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            string sql = "";
            List<string> sqllist = new List<string>();
            for (int i = 0; i < RepeaterJZXFYMX.Items.Count; i++)
            {
                if (((System.Web.UI.WebControls.CheckBox)RepeaterJZXFYMX.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    sql = "DELETE FROM TBTM_JZXFYDETAIL WHERE JZXFYDETAIL_ID='" + ((System.Web.UI.WebControls.Label)RepeaterJZXFYMX.Items[i].FindControl("LabelID")).Text + "'";
                    sqllist.Add(sql);
                }
            }
            if (sqllist.Count > 0)
            {
                DBCallCommon.ExecuteTrans(sqllist);
            }
            databind();
        }
    }
}
