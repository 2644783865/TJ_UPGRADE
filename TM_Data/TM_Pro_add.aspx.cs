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
using System.Collections.Generic;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Pro_add : System.Web.UI.Page
    {
        string name = "";
        string eng_name = "";
        string eng_type = "";
        string code = "";
        string note = "";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void InitVar()
        {
            if (GridView1.Rows.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            else
            {
                NoDataPanel.Visible = false;
            }
        }
        protected DataTable GetDataFromGrid()
        {
            DataTable dt1 = new DataTable("Table1");
            dt1.Columns.Add("PDS_ID");
            dt1.Columns.Add("PDS_NAME");
            dt1.Columns.Add("PDS_ENGNAME");
            dt1.Columns.Add("PDS_ENGTYPE");
            dt1.Columns.Add("PDS_CODE");
            dt1.Columns.Add("PDS_NOTE");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                DataRow newRow = dt1.NewRow();
                newRow[0] = ((Label)gRow.FindControl("lblID")).Text;
                newRow[1] = ((TextBox)gRow.FindControl("txt_name")).Text;
                newRow[2] = ((TextBox)gRow.FindControl("txt_eng_name")).Text;
                newRow[3] = ((TextBox)gRow.FindControl("txt_eng_type")).Text;
                newRow[4] = ((TextBox)gRow.FindControl("txt_code")).Text;
                newRow[5] = ((TextBox)gRow.FindControl("txt_note")).Text;
                dt1.Rows.Add(newRow);
            }
            dt1.AcceptChanges();
            return dt1;
        }
        private void CreateNewRow(int num)
        {
            DataTable dt = this.GetDataFromGrid();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }
        private string[] GetNames()
        {
            List<string> Names = new List<string>();
            string strsql = "select PDS_NAME from TBPD_STRUINFO";
            DataTable dts = DBCallCommon.GetDTUsingSqlText(strsql);
            foreach (DataRow row in dts.Rows)
            {
                Names.Add(row[0].ToString().Trim());
            }
            return Names.ToArray();
        }
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dt = this.GetDataFromGrid();
            dt.Rows.RemoveAt(e.RowIndex);
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
            InitVar();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (add_id.Value== "1")
            {
                CreateNewRow(Convert.ToInt32(txtnum.Value));
                InitVar();
                txtnum.Value = "";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string[] ColNames = GetNames();
            DataTable dt = GetDataFromGrid();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][1].ToString().Trim() != "")
                {
                    if (Array.IndexOf(ColNames, dt.Rows[i][1].ToString().Trim()) >= 0)
                    {
                        Response.Write("<script>alert('数据库中已存在该产品！')</script>");
                        return;
                    }
                    else
                    {
                        GridViewRow gr = GridView1.Rows[i];
                        name = ((TextBox)gr.FindControl("txt_name")).Text;
                        eng_name = ((TextBox)gr.FindControl("txt_eng_name")).Text;
                        eng_type = ((TextBox)gr.FindControl("txt_eng_type")).Text;
                        code = ((TextBox)gr.FindControl("txt_code")).Text;
                        note = ((TextBox)gr.FindControl("txt_note")).Text;
                        string strsql = "insert into TBPD_STRUINFO ";
                        strsql += "(PDS_NAME,PDS_ENGNAME,PDS_ENGTYPE,PDS_CODE,PDS_FILLDATE,PDS_NOTE) ";
                        strsql += "values ('" + name + "','" + eng_name + "','" + eng_type + "','" + code + "','" + DateTime.Now.ToString() + "','" + note + "')";
                        DBCallCommon.ExeSqlText(strsql);
                    }
                }
            }
            Response.Redirect("TM_Pro_struinfo.aspx");
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }
    }
}
