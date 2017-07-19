using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.Data;
using System.Web.UI.HtmlControls;
using ZCZJ_DPF;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace testpage
{
    public partial class CM_Add_Assign : System.Web.UI.Page
    {
            string sqlText;
            ComboBox engid;
            string[] fields;
            string engcode;
            string taskid;
            string engname;
            string engtype;
            double engtotal = 0;
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    GetProID();
                }
            }
            /// <summary>
            /// panel是否显示
            /// </summary>
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
            /// <summary>
            /// 绑定项目编号
            /// </summary>
            private void GetProID()
            {
                sqlText = "select PJ_ID+'/'+PJ_NAME as PJ_ID from TBPM_PJINFO order by PJ_ID";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                proid.DataSource = dt;
                proid.DataTextField = "PJ_ID";
                proid.DataValueField = "PJ_ID";
                proid.DataBind();
                proid.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
                proid.SelectedIndex = 0;
            }
            /// <summary>
            /// 绑定工程代号
            /// </summary>
            private void GetEngID()
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    engid = (ComboBox)gr.FindControl("engid");
                    sqlText = "select distinct ENG_ID,ENG_ID+'‖'+ENG_NAME as ENG_NAME from TBPM_ENGINFO ORDER BY ENG_ID";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                    engid.DataSource = dt;
                    engid.DataTextField = "ENG_NAME";
                    engid.DataValueField = "ENG_ID";
                    engid.DataBind();
                    engid.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
                    engid.SelectedIndex = 0;
                }
            }
            /// <summary>
            /// 定义DataTable
            /// </summary>
            /// <returns></returns>
            private DataTable GetDataTable()
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ENG_ID");
                dt.Columns.Add("TSA_ID");
                dt.Columns.Add("ENG_NAME");
                dt.Columns.Add("TSA_TOTALWGHT");
                dt.Columns.Add("ENG_STRTYPE");
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((ComboBox)gr.FindControl("engid")).SelectedValue;
                    newRow[1] = gr.Cells[3].Text;
                    newRow[2] = ((HtmlInputText)gr.FindControl("engname")).Value;
                    newRow[3] = ((HtmlInputText)gr.FindControl("engtotal")).Value;
                    newRow[4] = gr.Cells[6].Text;

                    if (gr.Cells[3].Text == "&nbsp;")
                    {
                        newRow[1] = "";
                    }
                    if (gr.Cells[5].Text == "&nbsp;")
                    {
                        newRow[3] = "";
                    }
                    dt.Rows.Add(newRow);
                }
                dt.AcceptChanges();
                return dt;
            }
            /// <summary>
            /// 生成输入行函数
            /// </summary>
            /// <param name="num"></param>
            private void CreateNewRow(int num)
            {
                DataTable dt = this.GetDataTable();
                for (int i = 0; i < num; i++)
                {
                    DataRow newRow = dt.NewRow();
                    dt.Rows.Add(newRow);
                }
                this.GridView1.DataSource = dt;
                this.GridView1.DataBind();
                InitVar();
            }

            /// <summary>
            /// 给ComboBox赋值
            /// </summary>
            private void GetComboBox()
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    taskid = gr.Cells[3].Text;
                    if (taskid != "&nbsp;")
                    {
                        fields = taskid.Split('/');
                        ((ComboBox)gr.FindControl("engid")).SelectedItem.Text = fields[0].ToString();
                    }
                }
            }
            /// <summary>
            /// 增加行
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void btnadd_Click(object sender, EventArgs e)
            {
                num.Disabled = true;
                btnadd.Enabled = false;
                proid.Enabled = false;

                CreateNewRow(Convert.ToInt32(num.Value));
                GetEngID();
                GetComboBox();
            }
            /// <summary>
            /// 项目改变
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void proid_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (proid.SelectedValue != "-请选择-")
                {
                    fields = proid.SelectedValue.ToString().Split('/');
                    proid.SelectedItem.Text = fields[0].ToString();
                    proname.Text = fields[1].ToString();
                    ddlid.Value = "1";
                }
                else
                {
                    ddlid.Value = "0";
                }
            }
            /// <summary>
            /// 工程改变
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void engid_SelectedIndexChanged(object sender, EventArgs e)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    engid = (ComboBox)gr.FindControl("engid");
                    if (engid.SelectedValue != "-请选择-")
                    {
                        //已分配任务数
                        sqlText = "select TOP 1 dbo.GetTaskIndex(TSA_ID) AS TopIndex from TBPM_TCTSASSGN where TSA_PJID='" + proid.SelectedItem.Text + "' AND TSA_FATHERNODE='0' ORDER BY dbo.GetTaskIndex(TSA_ID) DESC";

                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                        int index;
                        if (dt.Rows.Count > 0)
                        {
                            index = Convert.ToInt16(dt.Rows[0]["TopIndex"].ToString());
                        }
                        else
                        {
                            index = 0;
                        }

                        fields = engid.SelectedValue.Split('‖');
                        engcode = fields[0].ToString();
                        engid.SelectedItem.Text = engcode;
                        gr.Cells[3].Text = engcode + '/' + proid.SelectedItem.Text + '/' + (i + index + 1);
                        if (ddlElecQup.SelectedValue == "Y")
                        {
                            gr.Cells[3].Text += "(DQ)";
                        }

                        sqlText = "select ENG_NAME,ENG_STRTYPE from TBPM_ENGINFO ";
                        sqlText += "where ENG_ID='" + engcode + "'";
                        SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);

                        if (dr.Read())
                        {
                            ((HtmlInputText)gr.FindControl("engname")).Value = dr[0].ToString();
                            gr.Cells[6].Text = dr[1].ToString();
                        }

                        dr.Close();
                    }
                }
            }
            /// <summary>
            /// 是否电器制号改变
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            protected void ddlElecQup_OnSelectedIndexChanged(object sender, EventArgs e)
            {
                lblRedTip.Font.Bold = ddlElecQup.SelectedValue == "Y" ? true : false;
                lblRedTip.ForeColor = ddlElecQup.SelectedValue == "Y" ? System.Drawing.Color.Red : System.Drawing.Color.FromName("#1E5C95");
                if (GridView1.Rows.Count > 0)
                {
                    string taskid = "";
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        taskid = GridView1.Rows[i].Cells[3].Text;
                        if (taskid.Contains("/"))//表明该行已有生产制号
                        {
                            if (ddlElecQup.SelectedValue == "Y")
                            {
                                if (!taskid.Contains("(DQ)"))
                                {
                                    GridView1.Rows[i].Cells[3].Text += "(DQ)";
                                }
                            }
                            else if (ddlElecQup.SelectedValue == "N")
                            {
                                if (taskid.Contains("DQ"))
                                {
                                    GridView1.Rows[i].Cells[3].Text = GridView1.Rows[i].Cells[3].Text.Replace("(DQ)", "");
                                }
                            }
                        }
                    }
                }
            }
            /// <summary>
            /// 保存
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
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
                        engname = ((HtmlInputText)gr.FindControl("engname")).Value;
                        engtotal = Convert.ToDouble(((HtmlInputText)gr.FindControl("engtotal")).Value);
                        engtype = gr.Cells[6].Text.Trim();
                        string smalltype = engtype;//小类
                        string sql_dlengtype = "select ET_FTYPE from TBPM_ENGTYPE where ET_STYPE='" + engtype + "'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_dlengtype);
                        engtype = dt.Rows[0]["ET_FTYPE"].ToString();
                        //插入技术任务分工表(TBPM_TCTSASSGN)
                        sqlText = "insert into TBPM_TCTSASSGN (TSA_ID,TSA_PJID,TSA_ENGNAME,";
                        sqlText += "TSA_ENGSTRTYPE,TSA_STFORCODE,TSA_ENGSTRSMTYPE,TSA_ASSIGNTOELC,TSA_ADDTIME) values('" + taskid + "',";
                        sqlText += "'" + proid.SelectedItem.Text + "','" + engname + "',";
                        sqlText += "'" + engtype + "','" + engcode + "','" + smalltype + "','" + ddlElecQup.SelectedValue + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                        list_sql.Add(sqlText);
                        //插入技术员工程量登记表(TBPM_TCTSENROLL)
                        sqlText = "insert into TBPM_TCTSENROLL(TSA_ID,TSA_TOTALWGHT) values('" + taskid + "'," + engtotal + ")";
                        list_sql.Add(sqlText);

                        ////////if (taskid.Contains("(DQ)"))
                        ////////{
                        //插入部门完工确认表(TBCB_BMCONFIRM)
                        //sqlText = "insert into TBCB_BMCONFIRM(TASK_ID,PRJ,ENG) values('" + taskid + "','" + proname.Text.Trim() + "','" + engname + "')";
                        //list_sql.Add(sqlText);
                        ////////}
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
