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
using System.Collections.Generic;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_Internal_Audit_Detail : System.Web.UI.Page
    {
        string sqltext;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
                InitGridview();
                GetDepartment();
            }

        }
        private void GetDepartment()//绑定部门
        {
            string sqlText = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                DropDownList ddl = (DropDownList)(GridView1.Rows[i].FindControl("ddlDepId"));
                ddl.DataSource = dt;
                ddl.DataTextField = "DEP_NAME";
                ddl.DataValueField = "DEP_CODE";
                ddl.DataBind();
                ListItem it = new ListItem();
                it.Text = "全部";
                it.Value = "00";
                ddl.Items.Insert(0, it);
                ddl.SelectedValue = ((HtmlInputHidden)GridView1.Rows[i].FindControl("txtDepId")).Value;
            }
        }
        private void InitInfo()
        {
            string Id = Request.QueryString["ProId"];
            sqltext = "select * from TBQC_INTERNAL_AUDIT where PRO_ID=" + Id;
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                lblName.Text = dt.Rows[0]["PRO_NAME"].ToString();
                hidId.Value = dt.Rows[0]["PRO_ID"].ToString();
            }

        }
        private void InitGridview()
        {
            sqltext = "select * from TBQC_INTERNAL_AUDIT_DETAIL where AU_FID=" + hidId.Value;
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count < 20)
            {
                for (int i = dt.Rows.Count; i < 20; i++)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[1] = "00";
                    newRow[2] = "0";
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
                newrow[0] = "00";
                newrow[1] = "0";
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
            GetDepartment();
        }

        private void CreatNewRowByGridView(DataTable dt, int i)
        {
            GridViewRow gRow = GridView1.Rows[i];
            DataRow newRow = dt.NewRow();
            newRow[0] = ((DropDownList)gRow.FindControl("ddlDepId")).SelectedValue;
            newRow[1] = ((DropDownList)gRow.FindControl("ddlIssue")).SelectedValue;
            newRow[2] = ((HtmlInputText)gRow.FindControl("txtStandard")).Value.Trim();
            newRow[3] = ((HtmlInputText)gRow.FindControl("txtContent")).Value.Trim();
            newRow[4] = ((DropDownList)gRow.FindControl("ddlRespond")).SelectedValue;
            newRow[5] = ((HtmlInputText)gRow.FindControl("txtFinish")).Value.Trim();
            dt.Rows.Add(newRow);
        }


        private DataTable CreatDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("AU_DEPID");
            dt.Columns.Add("AU_ISSUE");
            dt.Columns.Add("AU_STANDARD");
            dt.Columns.Add("AU_CONTENT");
            dt.Columns.Add("AU_RESPOND");
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
            GetDepartment();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            sqltext = "delete from TBQC_INTERNAL_AUDIT_DETAIL where AU_FID=" + hidId.Value;
            list.Add(sqltext);
            foreach (GridViewRow gRow in GridView1.Rows)
            {
                string DepId = ((DropDownList)gRow.FindControl("ddlDepId")).SelectedValue;
                string Issue = ((DropDownList)gRow.FindControl("ddlIssue")).SelectedValue;
                string Standard = ((HtmlInputText)gRow.FindControl("txtStandard")).Value.Trim();
                string Content = ((HtmlInputText)gRow.FindControl("txtContent")).Value.Trim();
                string Respond = ((DropDownList)gRow.FindControl("ddlRespond")).SelectedValue;
                string Finish = ((HtmlInputText)gRow.FindControl("txtFinish")).Value.Trim();           
                string fId = hidId.Value;
                string Id = fId;
                if (!(Standard == "" & Content == "") && !(Standard != "" & Content != ""))//四项中有1-3项为空
                {

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请将基本信息补全,第" + Convert.ToInt32(((Label)gRow.FindControl("lblIndex")).Text.Trim()) + "行');", true);
                    return;
                }
                if (Standard != "" & Content != "")
                {
                    sqltext = "insert into TBQC_INTERNAL_AUDIT_DETAIL values('" + Id + "','" + DepId + "','" + Issue + "','" + Standard + "','" + Content + "','" + Respond + "','" + Finish + "'," + fId + ")";
                  
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
