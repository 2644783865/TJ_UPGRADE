using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_Management_Audit_Detail : System.Web.UI.Page
    {
        string sqltext;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
                InitGridview();
            }
            
        }
        private void InitInfo()
        {
            string Id = Request.QueryString["ProId"];
            sqltext = "select * from TBQC_MANAGEMENT_AUDIT where PRO_ID=" + Id;
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                lblName.Text = dt.Rows[0]["PRO_NAME"].ToString();
                hidId.Value = dt.Rows[0]["PRO_ID"].ToString();
            }
        }
        private void InitGridview()
        {
            sqltext = "select * from TBQC_MANAGEMRNT_AUDIT_DETAIL where AU_FID=" + hidId.Value;
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count < 20)
            {
                for (int i = dt.Rows.Count; i < 20; i++)
                {
                    DataRow newRow = dt.NewRow();                   
                    newRow[5] = "0";
                    dt.Rows.Add(newRow);
                }
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            int rownum = 0;
            //查找在何处插入行
            foreach (GridViewRow grow in GridView1.Rows)
            {

                CheckBox ckb = (CheckBox)grow.FindControl("CheckBox1");
                if (ckb.Checked)
                {
                    rownum = Convert.ToInt32(((Label)grow.FindControl("lblIndex")).Text.Trim()) - 1;
                    break;
                }
            }
            //如果没有，在结尾处插入
            if (rownum == -1)
            {
                rownum = GridView1.Rows.Count;
            }
            DataTable dt = CreatDataTable();
            for (int i = 0; i < rownum; i++)
            {

                CreatNewRowByGridView(dt, i);
            }
            for (int i = 0; i < Convert.ToInt32(txtNum.Text.Trim()); i++)
            {
                DataRow newrow = dt.NewRow();
                newrow[4] = "0";
                dt.Rows.Add(newrow);
            }
            if (rownum != GridView1.Rows.Count)
            {
                for (int i = rownum; i < GridView1.Rows.Count; i++)
                {
                    CreatNewRowByGridView(dt, i);
                }
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        private void CreatNewRowByGridView(DataTable dt, int i)
        {
            GridViewRow gRow = GridView1.Rows[i];
            DataRow newRow = dt.NewRow();
            newRow[0] = ((HtmlTextArea)gRow.FindControl("txtIssue")).Value.Trim();
            newRow[1] = ((HtmlTextArea)gRow.FindControl("txtAction")).Value.Trim();
            newRow[2] = ((HtmlInputText)gRow.FindControl("txtContent")).Value.Trim();
            newRow[3] = ((HtmlInputText)gRow.FindControl("txtTime")).Value.Trim();
            newRow[4] = ((DropDownList)gRow.FindControl("ddlFinish")).SelectedValue;
          
            dt.Rows.Add(newRow);
        }

        private DataTable CreatDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("AU_ISSUE");
            dt.Columns.Add("AU_ACTION");
            dt.Columns.Add("AU_CONTENT");
            dt.Columns.Add("AU_TIME");
            dt.Columns.Add("AU_FINISH");
            return dt;
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DataTable dt = CreatDataTable();

            foreach (GridViewRow grow in GridView1.Rows)
            {
                CheckBox ckb = (CheckBox)grow.FindControl("CheckBox1");
                if (!ckb.Checked)
                {
                    CreatNewRowByGridView(dt, Convert.ToInt32(((Label)grow.FindControl("lblIndex")).Text.Trim()) - 1);

                }

            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
           
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            sqltext = "delete from TBQC_MANAGEMRNT_AUDIT_DETAIL where AU_FID=" + hidId.Value;
            list.Add(sqltext);
            foreach (GridViewRow gRow in GridView1.Rows)
            {
                string Issue = ((HtmlTextArea)gRow.FindControl("txtIssue")).Value.Trim();

                string Action = ((HtmlTextArea)gRow.FindControl("txtAction")).Value.Trim();
                string Content = ((HtmlInputText)gRow.FindControl("txtContent")).Value.Trim();
                string Time = ((HtmlInputText)gRow.FindControl("txtTime")).Value.Trim();
                string Finish = ((DropDownList)gRow.FindControl("ddlFinish")).SelectedValue;
                string fId = hidId.Value;
                string Id = fId;
                if (!(Issue == "" & Action == "" & Content == "" & Time == "") && !(Issue != "" & Action != ""))//四项中有1-4项为空
                {

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请将基本信息补全,第" + Convert.ToInt32(((Label)gRow.FindControl("lblIndex")).Text.Trim()) + "行');", true);
                    return;
                }
                if (Issue != "" & Action != "" )
                {
                    sqltext = "insert into TBQC_MANAGEMRNT_AUDIT_DETAIL values('" + Id + "','" + Issue + "','" + Action + "','" + Content + "','" + Time + "','" + Finish + "'," + fId + ")";

                    list.Add(sqltext);
                }
            }
            try
            {
                DBCallCommon.ExecuteTrans(list);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功');", true);
                return;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
