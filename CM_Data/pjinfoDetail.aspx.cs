using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using AjaxControlToolkit;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class pjinfoDetail_new : System.Web.UI.Page
    {
        public string PJ_Code;
        static string pj_ID = string.Empty;//全局变量id
        static string action = string.Empty;
        ComboBox engid;
        string[] fields;
        string sqlText;
        string engcode;
        string taskid;
        string engtype;
        double engtotal = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = "项目基本信息管理";
                GetDepartment();
                addtime.Text = DateTime.Now.ToLongDateString();
                //if (Request.QueryString["id"] == null && Request.QueryString["action"] == null)
                //    throw new NoNullAllowedException("不允许空值传递！");
                //else
                //{
                //    action = Request.QueryString["action"].ToString();
                //    if (action == "update")  //更新项目
                //    {
                //        pj_ID = Request.QueryString["id"].ToString();
                //    }
                //    else
                //    {
                //        tb_PJ_MANCLERK.Text = Session["UserName"].ToString();
                //    }
                //}
            }
        }

        private void GetDepartment()
        {
            string sqlText = "select PJ_ID from TBPM_PJINFO";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            DDL_NAME.DataSource = dt;
            DDL_NAME.DataTextField = "PJ_ID";
            DDL_NAME.DataBind();
            ListItem item = new ListItem();
            item.Text = "-请选择-";
            item.Value = "00";
            DDL_NAME.Items.Insert(0, item);
            DDL_NAME.SelectedValue = "00";
        }

        #region 增加行
        protected void btnadd_Click(object sender, EventArgs e)
        {
            //if (DDL_NAME.SelectedValue != "00")
            {
                int count = GridView1.Rows.Count;
                List<int> oldsele = GetOldSele(count);
                CreateNewRow(Convert.ToInt32(num.Value));
                GetEngID(count, oldsele);
                for (int i = count; i < count + Convert.ToInt32(num.Value); i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    BindEng(gr);
                    engid.SelectedIndex = 0;
                }
            }
            //GetComboBox();
        }

        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow[4] = 1;
                dt.Rows.Add(newRow);
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
            InitVar();
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ENG_ID");
            dt.Columns.Add("TSA_ID");
            dt.Columns.Add("TSA_TOTALWGHT");
            dt.Columns.Add("ENG_STRTYPE");
            dt.Columns.Add("TSA_NUMBER");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                DataRow newRow = dt.NewRow();
                newRow[1] = ((HtmlInputText)gr.FindControl("engname")).Value;
                newRow[2] = ((HtmlInputText)gr.FindControl("engtotal")).Value;
                if (gr.Cells[5].Text == "&nbsp;")
                {
                    newRow[3] = "";
                }
                else
                {
                    newRow[3] = gr.Cells[5].Text;
                }
                newRow[4] = ((HtmlInputText)gr.FindControl("number")).Value;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        private List<int> GetOldSele(int count)
        {
            List<int> ints = new List<int>();
            if (count > 0)
            { 
                for (int i = 0; i < count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    ints.Add(((ComboBox)gr.FindControl("engid")).SelectedIndex);
                }
                return ints;
            }
            else
            {
                ints.Add(0);
                return ints;
            }
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

        private void GetEngID(int count, List<int> str)
        {
            for (int i = 0; i < count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                BindEng(gr);
                engid.SelectedIndex = str[i];

                engid = (ComboBox)gr.FindControl("engid");
                if (engid.SelectedValue != "-请选择-")
                {
                    fields = engid.SelectedValue.Split('‖');
                    engcode = fields[0].ToString();
                    engid.SelectedItem.Text = engcode;
                }
            }
            
        }

        private void BindEng(GridViewRow gr)
        {
            engid = (ComboBox)gr.FindControl("engid");
            sqlText = "select distinct ENG_ID,ENG_ID+'‖'+ENG_NAME as ENG_NAME from TBPM_ENGINFO ORDER BY ENG_ID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            engid.DataSource = dt;
            engid.DataTextField = "ENG_NAME";
            engid.DataValueField = "ENG_ID";
            engid.DataBind();
            engid.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }
        #endregion

        protected void engid_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                engid = (ComboBox)gr.FindControl("engid");
                string SeleText = engid.SelectedValue;
                int SeleIndex = engid.SelectedIndex;
                BindEng(gr);
                engid.SelectedIndex = SeleIndex;
                if (SeleText != "-请选择-")
                {
                    fields = SeleText.Split('‖');
                    engcode = fields[0].ToString();
                    engid.SelectedItem.Text = engcode;

                    sqlText = "select ENG_NAME,ENG_STRTYPE from TBPM_ENGINFO ";
                    sqlText += "where ENG_ID='" + engcode + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                    if (dr.Read())
                    {
                        gr.Cells[5].Text = dr[1].ToString();
                    }

                    dr.Close();
                }
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ENG_ID");
            dt.Columns.Add("TSA_ID");
            dt.Columns.Add("TSA_TOTALWGHT");
            dt.Columns.Add("ENG_STRTYPE");
            dt.Columns.Add("TSA_NUMBER");
            int i = 0;
            List<int> ints = new List<int>();
            foreach (GridViewRow row in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chk");
                if (!chk.Checked)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    DataRow newRow = dt.NewRow();
                    newRow[1] = ((HtmlInputText)gr.FindControl("engname")).Value;
                    newRow[2] = ((HtmlInputText)gr.FindControl("engtotal")).Value;
                    if (gr.Cells[5].Text == "&nbsp;")
                    {
                        newRow[3] = "";
                    }
                    else
                    {
                        newRow[3] = gr.Cells[5].Text;
                    }
                    newRow[4] = ((HtmlInputText)gr.FindControl("number")).Value;
                    ints.Add(((ComboBox)gr.FindControl("engid")).SelectedIndex);
                    dt.Rows.Add(newRow);
                    dt.AcceptChanges();
                }
                i++;
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
            InitVar();
            GetEngID(GridView1.Rows.Count, ints);
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            string pattern2 = @"^[0-9]+(\.[0-9]+)?$";
            Regex rgx = new Regex(pattern2);
            //检查是否已选择工程
            string st = "OK";
            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                GridViewRow gr = GridView1.Rows[j];
                ComboBox engid = ((ComboBox)gr.FindControl("engid"));
                string engnm = ((HtmlInputText)gr.FindControl("engname")).Value.Trim();
                string engtotal = ((HtmlInputText)gr.FindControl("engtotal")).Value.Trim();
                if (engid.SelectedIndex == 0 || engnm == "")
                {
                    st = "UnSelect";
                    break;
                }
                if (!rgx.IsMatch(engtotal))
                {
                    st = "NumError";
                    break;
                }
            }

            if (GridView1.Rows.Count == 0)
            {
                st = "NoData";
            }

            if (st == "OK")
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    taskid = gr.Cells[3].Text;
                    fields = taskid.Split('/');
                    engcode = fields[0].ToString();
                    engtotal = Convert.ToDouble(((HtmlInputText)gr.FindControl("engtotal")).Value);
                    engtype = gr.Cells[6].Text.Trim();
                    string smalltype = engtype;//小类
                    string sql_dlengtype = "select ET_FTYPE from TBPM_ENGTYPE where ET_STYPE='" + engtype + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_dlengtype);
                    engtype = dt.Rows[0]["ET_FTYPE"].ToString();
                    //插入技术任务分工表(TBPM_TCTSASSGN)
                    sqlText = "insert into TBPM_TCTSASSGN (TSA_ID,TSA_PJID,";
                    sqlText += "TSA_ENGSTRTYPE,TSA_STFORCODE,TSA_ENGSTRSMTYPE,TSA_ADDTIME) values('" + taskid + "',";
                    sqlText += "'" + DDL_NAME.SelectedItem.Text + "',";
                    sqlText += "'" + engtype + "','" + engcode + "','" + smalltype + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    list_sql.Add(sqlText);
                    //插入技术员工程量登记表(TBPM_TCTSENROLL)
                    sqlText = "insert into TBPM_TCTSENROLL(TSA_ID,TSA_TOTALWGHT) values('" + taskid + "'," + engtotal + ")";
                    list_sql.Add(sqlText);
                }
                DBCallCommon.ExecuteTrans(list_sql);
                Response.Redirect("CM_Tech_assign.aspx");
            }
            else if (st == "UnSelect")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！\\r\\r请选择【工程简称】及填写【工程名称】！！！');", true);
            }
            else if (st == "NumError")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！\\r\\r输入正确的数值格式！！！');", true);
            }
            else if (st == "NoData")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！\\r\\r没有数据！！！');", true);
            }
        }
    }
}
