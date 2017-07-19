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
    public partial class QC_TargetAnalyze_Edit : System.Web.UI.Page
    {
        string sqlText;
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
        private void InitGridview()
        {
            //sqlText = "select *,TBDS_STAFFINFO.ST_NAME from TBQC_TARGET_DETAIL left join TBDS_STAFFINFO on TBQC_TARGET_DETAIL.TARGET_MANAGER = TBDS_STAFFINFO.ST_ID where TARGET_FID=" + hidId.Value;
            sqlText = "select * from TBQC_TARGET_DETAIL where TARGET_FID=" + hidId.Value;
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count < 20)
            {
                for (int i = dt.Rows.Count; i < 20; i++)
                {
                    DataRow newRow = dt.NewRow();
                  

                    dt.Rows.Add(newRow);
                }
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        private void InitInfo()
        {
            string Id = Request.QueryString["tarId"];
            sqlText = "select * from TBQC_TARGET_LIST where TARGET_ID=" + Id;
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                lblName.Text = dt.Rows[0]["TARGET_NAME"].ToString();
                hidId.Value = dt.Rows[0]["TARGET_ID"].ToString();
            }

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
            newRow[1] = ((DropDownList)gRow.FindControl("ddlTiXi")).SelectedValue;
            newRow[2] = ((HtmlInputText)gRow.FindControl("txtManager")).Value.Trim();
            newRow[3] = ((HtmlInputText)gRow.FindControl("txtMuBiao")).Value.Trim();
            newRow[4] = ((HtmlInputText)gRow.FindControl("txtJan")).Value.Trim();
            newRow[5] = ((HtmlInputText)gRow.FindControl("txtFeb")).Value.Trim();
            newRow[6] = ((HtmlInputText)gRow.FindControl("txtMar")).Value.Trim();
            newRow[7] = ((HtmlInputText)gRow.FindControl("txtApr")).Value.Trim();
            newRow[8] = ((HtmlInputText)gRow.FindControl("txtMay")).Value.Trim();
            newRow[9] = ((HtmlInputText)gRow.FindControl("txtJun")).Value.Trim();
            newRow[10] = ((HtmlInputText)gRow.FindControl("txtJuy")).Value.Trim();
            newRow[11] = ((HtmlInputText)gRow.FindControl("txtAug")).Value.Trim();
            newRow[12] = ((HtmlInputText)gRow.FindControl("txtSep")).Value.Trim();
            newRow[13] = ((HtmlInputText)gRow.FindControl("txtOct")).Value.Trim();
            newRow[14] = ((HtmlInputText)gRow.FindControl("txtNov")).Value.Trim();
            newRow[15] = ((HtmlInputText)gRow.FindControl("txtDec")).Value.Trim();
            dt.Rows.Add(newRow);
        }

        private DataTable CreatDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TARGET_DEPID");
            dt.Columns.Add("TARGET_TIXI");
            dt.Columns.Add("TARGET_MANAGER");
            dt.Columns.Add("TARGET_MUBIAO");
            dt.Columns.Add("TARGET_JAN");
            dt.Columns.Add("TARGET_FEB");
            dt.Columns.Add("TARGET_MAR");
            dt.Columns.Add("TARGET_APR");
            dt.Columns.Add("TARGET_MAY");
            dt.Columns.Add("TARGET_JUN");
            dt.Columns.Add("TARGET_JUL");
            dt.Columns.Add("TARGET_AUG");
            dt.Columns.Add("TARGET_SEP");
            dt.Columns.Add("TARGET_OCT");
            dt.Columns.Add("TARGET_NOV");
            dt.Columns.Add("TARGET_DEC");
            return dt;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            DataTable dt = CreatDataTable();

            foreach (GridViewRow grow in GridView1.Rows)
            {
                //string delNum="";
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
            sqlText = "delete from TBQC_TARGET_DETAIL where TARGET_FID=" + hidId.Value;
            list.Add(sqlText);
            foreach (GridViewRow gRow in GridView1.Rows)
            {
                string depId = ((DropDownList)gRow.FindControl("ddlDepId")).SelectedValue;
                string TiXi = ((DropDownList)gRow.FindControl("ddlTiXi")).SelectedValue;
                string Manager = ((HtmlInputText)gRow.FindControl("txtManager")).Value.Trim();
                string MuBiao = ((HtmlInputText)gRow.FindControl("txtMuBiao")).Value.Trim();
                string Jan = ((HtmlInputText)gRow.FindControl("txtJan")).Value.Trim();
                string Feb = ((HtmlInputText)gRow.FindControl("txtFeb")).Value.Trim();
                string Mar = ((HtmlInputText)gRow.FindControl("txtMar")).Value.Trim();
                string Apr = ((HtmlInputText)gRow.FindControl("txtApr")).Value.Trim();
                string May = ((HtmlInputText)gRow.FindControl("txtMay")).Value.Trim();
                string Jun = ((HtmlInputText)gRow.FindControl("txtJun")).Value.Trim();
                string Juy = ((HtmlInputText)gRow.FindControl("txtJuy")).Value.Trim();
                string Aug = ((HtmlInputText)gRow.FindControl("txtAug")).Value.Trim();
                string Sep = ((HtmlInputText)gRow.FindControl("txtSep")).Value.Trim();
                string Oct = ((HtmlInputText)gRow.FindControl("txtOct")).Value.Trim();
                string Nov = ((HtmlInputText)gRow.FindControl("txtNov")).Value.Trim();
                string Dec = ((HtmlInputText)gRow.FindControl("txtDec")).Value.Trim();
                string fId = hidId.Value;
                if (!(depId == "00" & TiXi == "" & Manager == "" & MuBiao == "") && !(depId != "00" & TiXi != "" & Manager != "" & MuBiao != ""))//四项中有1-3项为空
                {

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请将基本信息补全,第" + Convert.ToInt32(((Label)gRow.FindControl("lblIndex")).Text.Trim()) + "行');", true);
                    return;
                }
                if (depId != "00" & TiXi != "" & Manager != "" & MuBiao != "")
                {
                    sqlText = "insert into TBQC_TARGET_DETAIL values('" + depId + "','" + TiXi + "','" + MuBiao + "','" + Manager + "','" + Jan + "','" + Feb + "','" + Mar + "','" + Apr + "','" + May + "','" + Jun + "','" + Juy + "','" + Aug + "','" + Sep + "','" + Oct + "','" + Nov + "','" + Dec + "'," + fId + ")";
                    list.Add(sqlText);
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
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        
        }
    }
}
