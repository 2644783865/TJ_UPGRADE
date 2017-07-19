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
namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchaseplan_Bill_summarizing : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Initpage();//初始化显示
            }
        }
        private void Initpage()
        {
            if (getdatacount() > 0)
            {
                message.Text = "当前有" + Convert.ToString(getdatacount()) + "条记录可以汇总，是否汇总？";
                btn_look.Enabled = true;
            }
            else
            {
                message.Text = "当前没有数据需要汇总";
                btn_look.Enabled = false;
            }
        }

        private int getdatacount()//是否有数据需要汇总
        {
            int num;
            string sqltext = "SELECT d.MP_ZONGXU,d.MP_MARID FROM (SELECT * FROM TBPM_MPFORBLJ UNION ALL SELECT * FROM TBPM_MPFORDQJ UNION ALL SELECT * FROM TBPM_MPFORDQO  UNION ALL SELECT * FROM TBPM_MPFORGFB  UNION ALL SELECT * FROM TBPM_MPFORHZY  UNION ALL SELECT * FROM TBPM_MPFORQLM ) AS d WHERE d.MP_PID IN (SELECT MP_ID AS MP_PID FROM TBPM_MPFORALLRVW WHERE MP_STATE='8')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                num = dt.Rows.Count;
            }
            else
            {
                num = 0;
            }
            return num;
        }
        protected void btn_look_Click(object sender, EventArgs e)
        {
            Paneleng_pj.Visible = true;//项目工程下拉框
            NoDataPane.Visible = false;//
            Panel1.Visible = true;//确定取消按钮
            initdropdownlist_PJ();
            billsummarizingRepeaterbind();
        }
        protected void backno_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PC_Data/PC_Index.aspx");
        }
        //初始化项目工程下拉框
        private void initdropdownlist_PJ()
        {
            string sqltext = "SELECT DISTINCT d.MP_PJID AS PUR_PJID,d.MP_PJNAME AS PUR_PJNAME " +
                                 "FROM (SELECT MP_PID,MP_PJID,MP_PJNAME FROM TBPM_MPFORBLJ " +
                                 "UNION ALL SELECT MP_PID,MP_PJID,MP_PJNAME FROM TBPM_MPFORDQJ " +
                                 "UNION ALL SELECT MP_PID,MP_PJID,MP_PJNAME FROM TBPM_MPFORDQO " +
                                 "UNION ALL SELECT MP_PID,MP_PJID,MP_PJNAME FROM TBPM_MPFORGFB " +
                                 "UNION ALL SELECT MP_PID,MP_PJID,MP_PJNAME FROM TBPM_MPFORHZY " +
                                 "UNION ALL SELECT MP_PID,MP_PJID,MP_PJNAME FROM TBPM_MPFORQLM) AS d " +
                                 "WHERE (MP_PID IN (SELECT  MP_ID AS MP_PID FROM  TBPM_MPFORALLRVW WHERE (MP_STATE = '8')))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)//有项目数据需要汇总
            {
                DropDownList_PJ.DataSource = dt;
                DropDownList_PJ.DataTextField = "PUR_PJNAME";
                DropDownList_PJ.DataValueField = "PUR_PJID";
                DropDownList_PJ.DataBind();
                DropDownList_PJ.Items[0].Selected = true;
                Initdropdownlist_project();
            }
            else
            {
                DropDownList_PJ.Items.Clear();
                downlist_eng.Items.Clear();
            }
        }
        //工程下拉框初始化
        private void Initdropdownlist_project()
        {
            if (DropDownList_PJ.Items.Count != 0)//有项目
            {
                string sqltext = "SELECT DISTINCT d.MP_ENGID AS PUR_ENGID,d.MP_ENGNAME AS PUR_ENGNAME " +
                                  "FROM (SELECT MP_PID,MP_PJID,MP_ENGID,MP_ENGNAME FROM TBPM_MPFORBLJ " +
                                  "UNION ALL SELECT MP_PID,MP_PJID,MP_ENGID,MP_ENGNAME FROM TBPM_MPFORDQJ " +
                                  "UNION ALL SELECT MP_PID,MP_PJID,MP_ENGID,MP_ENGNAME FROM TBPM_MPFORDQO " +
                                  "UNION ALL SELECT MP_PID,MP_PJID,MP_ENGID,MP_ENGNAME FROM TBPM_MPFORGFB " +
                                  "UNION ALL SELECT MP_PID,MP_PJID,MP_ENGID,MP_ENGNAME FROM TBPM_MPFORHZY " +
                                  "UNION ALL SELECT MP_PID,MP_PJID,MP_ENGID,MP_ENGNAME FROM TBPM_MPFORQLM) AS d " +
                                  "WHERE (MP_PID IN (SELECT  MP_ID AS MP_PID FROM  TBPM_MPFORALLRVW WHERE  (MP_STATE = '8')) AND MP_PJID='" + DropDownList_PJ.SelectedValue.ToString() + "') ";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)//有工程数据需要汇总
                {
                    downlist_eng.DataSource = dt;
                    downlist_eng.DataTextField = "PUR_ENGNAME";
                    downlist_eng.DataValueField = "PUR_ENGID";
                    downlist_eng.DataBind();
                    downlist_eng.Items[0].Selected = true;
                }
            }
        }
        protected void DropDownList_PJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            Initdropdownlist_project();
            billsummarizingRepeaterbind();
        }
        protected void downlist_eng_SelectedIndexChanged(object sender, EventArgs e)
        {
            billsummarizingRepeaterbind();
        }
        protected void save_Click(object sender, EventArgs e)
        {
            if (billsummarizingRepeater.Items.Count > 0)
            {
                insertinitialdata();//下推
                initdropdownlist_PJ();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有数据！');", true);
            }
        }
        protected void allsave_Click(object sender, EventArgs e)
        {
            insertallinitialdata();//全部下推
        }
        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PC_Data/PC_Index.aspx");
        }
        //绑定数据源
        protected void billsummarizingRepeaterbind()
        {
            DataTable dt = SetDatetable();
            if (dt.Rows.Count > 0)
            {
                DataView dataView = dt.DefaultView;//定义一个DataView为dtt2的默认视图 
                dataView.RowFilter = "PUR_PJID= '" + DropDownList_PJ.SelectedValue.ToString() + "' AND PUR_ENGID='" + downlist_eng.SelectedValue.ToString() + "'"; //对dataView进行筛选 
                dataView.Sort = "PUR_MARID ASC";
                billsummarizingRepeater.DataSource = dataView;
                billsummarizingRepeater.DataBind();
            }
        }
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PUR_PJID");
            dt.Columns.Add("PUR_PJNAME");
            dt.Columns.Add("PUR_ENGID");
            dt.Columns.Add("PUR_ENGNAME");
            dt.Columns.Add("PUR_MARID");
            dt.Columns.Add("PUR_MARNAME");
            dt.Columns.Add("PUR_MARNORM");
            dt.Columns.Add("PUR_MARTERIAL");
            dt.Columns.Add("PUR_FIXEDSIZE");
            dt.Columns.Add("PUR_NEDDNUM", typeof(double));
            dt.Columns.Add("PUR_NUNIT");
            dt.Columns.Add("PUR_NOTE");
            return dt;
        }
        private DataTable SetDatetable()
        {
            string sqltext = "SELECT MP_ID FROM TBPM_MPFORALLRVW WHERE MP_STATE='8'";
            DataTable dtt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DataTable glotb = GetDataTable();
            if (dtt.Rows.Count > 0)
            {
                DataTable dtt2 = new DataTable();
                sqltext = "SELECT d.MP_PJID AS PUR_PJID, d.MP_PJNAME AS PUR_PJNAME, d.MP_ENGID AS PUR_ENGID, d.MP_ENGNAME AS PUR_ENGNAME, d.MP_MARID AS PUR_MARID, d.MP_NAME AS PUR_MARNAME, SUBSTRING(d.MP_GUIGE, 1, CHARINDEX('X', d.MP_GUIGE) - 1) AS PUR_MARNORM,d.MP_CAIZHI AS PUR_MARTERIAL,  " +
                                 "d.MP_FIXEDSIZE AS PUR_FIXEDSIZE, SUM(d.MP_NUMBER) AS PUR_NEDDNUM, d.MP_UNIT AS PUR_NUNIT,d.MP_NOTE AS PUR_NOTE " +
                          "FROM  (SELECT * FROM TBPM_MPFORBLJ  WHERE (MP_FIXEDSIZE = '0') " +
                                 "UNION ALL SELECT * FROM TBPM_MPFORDQJ WHERE (MP_FIXEDSIZE = '0') " +
                                 "UNION ALL SELECT * FROM TBPM_MPFORDQO WHERE (MP_FIXEDSIZE = '0') " +
                                 "UNION ALL SELECT * FROM TBPM_MPFORGFB WHERE (MP_FIXEDSIZE = '0') " +
                                 "UNION ALL SELECT * FROM TBPM_MPFORHZY WHERE (MP_FIXEDSIZE = '0') " +
                                 "UNION ALL SELECT * FROM TBPM_MPFORQLM WHERE (MP_FIXEDSIZE = '0')) AS d " +
                                 "WHERE (MP_PID IN (SELECT  MP_ID AS MP_PID FROM  TBPM_MPFORALLRVW WHERE  (MP_STATE = '8'))) " +
                          "GROUP BY d.MP_PJID, d.MP_PJNAME, d.MP_ENGID, d.MP_ENGNAME, d.MP_MARID, d.MP_NAME, SUBSTRING(d.MP_GUIGE, 1, CHARINDEX('X', d.MP_GUIGE) - 1), " +
                                   "d.MP_CAIZHI, d.MP_FIXEDSIZE,d.MP_UNIT, d.MP_NOTE";
                dtt2 = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dtt2.Rows.Count > 0)
                {
                    glotb.Merge(dtt2);
                    glotb.AcceptChanges();
                }
                sqltext = "SELECT d.MP_PJID AS PUR_PJID, d.MP_PJNAME AS PUR_PJNAME, d.MP_ENGID AS PUR_ENGID, d.MP_ENGNAME AS PUR_ENGNAME, d.MP_MARID AS PUR_MARID, d.MP_NAME AS PUR_MARNAME,d.MP_GUIGE AS PUR_MARNORM,d.MP_CAIZHI AS PUR_MARTERIAL,  " +
                                 "d.MP_FIXEDSIZE AS PUR_FIXEDSIZE, SUM(d.MP_NUMBER) AS PUR_NEDDNUM, d.MP_UNIT AS PUR_NUNIT,d.MP_NOTE AS PUR_NOTE " +
                          "FROM  (SELECT * FROM TBPM_MPFORBLJ  WHERE (MP_FIXEDSIZE ='1') " +
                                 "UNION ALL SELECT * FROM TBPM_MPFORDQJ WHERE (MP_FIXEDSIZE ='1') " +
                                 "UNION ALL SELECT * FROM TBPM_MPFORDQO WHERE (MP_FIXEDSIZE ='1') " +
                                 "UNION ALL SELECT * FROM TBPM_MPFORGFB WHERE (MP_FIXEDSIZE ='1') " +
                                 "UNION ALL SELECT * FROM TBPM_MPFORHZY WHERE (MP_FIXEDSIZE ='1') " +
                                 "UNION ALL SELECT * FROM TBPM_MPFORQLM WHERE (MP_FIXEDSIZE ='1')) AS d " +
                          "WHERE (MP_PID IN (SELECT  MP_ID AS MP_PID FROM  TBPM_MPFORALLRVW WHERE  (MP_STATE = '8'))) " +
                          "GROUP BY d.MP_PJID, d.MP_PJNAME, d.MP_ENGID, d.MP_ENGNAME, d.MP_MARID, d.MP_NAME, d.MP_GUIGE, d.MP_CAIZHI, d.MP_FIXEDSIZE,d.MP_UNIT, d.MP_NOTE";
                dtt2 = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dtt2.Rows.Count > 0)
                {
                    glotb.Merge(dtt2);
                    glotb.AcceptChanges();
                }
                sqltext = "SELECT d.MP_PJID AS PUR_PJID, d.MP_PJNAME AS PUR_PJNAME, d.MP_ENGID AS PUR_ENGID, d.MP_ENGNAME AS PUR_ENGNAME, d.MP_MARID AS PUR_MARID, d.MP_NAME AS PUR_MARNAME,d.MP_GUIGE AS PUR_MARNORM,d.MP_CAIZHI AS PUR_MARTERIAL,  " +
                                 "d.MP_FIXEDSIZE AS PUR_FIXEDSIZE, SUM(d.MP_NUMBER) AS PUR_NEDDNUM, d.MP_UNIT AS PUR_NUNIT,d.MP_NOTE AS PUR_NOTE " +
                          "FROM  (SELECT * FROM TBPM_MPFORBLJ  WHERE (MP_FIXEDSIZE ='1') " +
                                 "UNION ALL SELECT * FROM TBPM_MPFORDQJ WHERE (MP_FIXEDSIZE is null OR MP_FIXEDSIZE='') " +
                                 "UNION ALL SELECT * FROM TBPM_MPFORDQO WHERE (MP_FIXEDSIZE is null OR MP_FIXEDSIZE='') " +
                                 "UNION ALL SELECT * FROM TBPM_MPFORGFB WHERE (MP_FIXEDSIZE is null OR MP_FIXEDSIZE='') " +
                                 "UNION ALL SELECT * FROM TBPM_MPFORHZY WHERE (MP_FIXEDSIZE is null OR MP_FIXEDSIZE='') " +
                                 "UNION ALL SELECT * FROM TBPM_MPFORQLM WHERE (MP_FIXEDSIZE is null OR MP_FIXEDSIZE='')) AS d " +
                          "WHERE (MP_PID IN (SELECT  MP_ID AS MP_PID FROM  TBPM_MPFORALLRVW WHERE  (MP_STATE = '8'))) " +
                          "GROUP BY d.MP_PJID, d.MP_PJNAME, d.MP_ENGID, d.MP_ENGNAME, d.MP_MARID, d.MP_NAME, d.MP_GUIGE, d.MP_CAIZHI, d.MP_FIXEDSIZE,d.MP_UNIT, d.MP_NOTE";
                dtt2 = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dtt2.Rows.Count > 0)
                {
                    glotb.Merge(dtt2);
                    glotb.AcceptChanges();
                }
            }
            return glotb;
        }
        private void insertinitialdata()
        {
            string sqltext = "";
            SqlConnection sqlConn = new SqlConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            sqlConn.Open();
            sqlCmd.Connection = sqlConn;
            generate_caigoudan(DropDownList_PJ.SelectedValue.ToString(), downlist_eng.SelectedValue.ToString());
            int i = 0;
            foreach (RepeaterItem Reitem in billsummarizingRepeater.Items)
            {
                i++;
                sqltext = "INSERT INTO TBPC_PURCHASEPLAN(PUR_PCODE,PUR_PJID,PUR_PJNAME,PUR_ENGID,PUR_ENGNAME,PUR_PTCODE,PUR_MARID,PUR_MARNAME,PUR_MARNORM,PUR_MARTERIAL,PUR_FIXEDSIZE,PUR_NUM,PUR_NUNIT,PUR_USTNUM,PUR_RPNUM,PUR_PUNIT,PUR_STDATE,PUR_PRONODE,PUR_STATE) " +
                                                            "VALUES(@PUR_PCODE,@PUR_PJID,@PUR_PJNAME,@PUR_ENGID,@PUR_ENGNAME,@PUR_PTCODE,@PUR_MARID,@PUR_MARNAME,@PUR_MARNORM,@PUR_MARTERIAL,@PUR_FIXEDSIZE,@PUR_NUM,@PUR_NUNIT,@PUR_USTNUM,@PUR_RPNUM,@PUR_PUNIT,@PUR_STDATE,@PUR_PRONODE,@PUR_STATE)";
                sqlCmd.CommandText = sqltext;
                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.AddWithValue("@PUR_PCODE", TextBox_pid.Text.ToString());
                sqlCmd.Parameters.AddWithValue("@PUR_PJID", DropDownList_PJ.SelectedValue.ToString());
                sqlCmd.Parameters.AddWithValue("@PUR_PJNAME", DropDownList_PJ.SelectedItem.Text);
                sqlCmd.Parameters.AddWithValue("@PUR_ENGID", downlist_eng.SelectedValue.ToString());
                sqlCmd.Parameters.AddWithValue("@PUR_ENGNAME", downlist_eng.SelectedItem.Text);
                sqlCmd.Parameters.AddWithValue("@PUR_PTCODE", TextBox_pid.Text.ToString() + "_" + i.ToString().PadLeft(4, '0'));
                sqlCmd.Parameters.AddWithValue("@PUR_MARID", ((Label)Reitem.FindControl("PUR_MARID")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_MARNAME", ((Label)Reitem.FindControl("PUR_MARNAME")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_MARNORM", ((Label)Reitem.FindControl("PUR_MARNORM")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_MARTERIAL", ((Label)Reitem.FindControl("PUR_MARTERIAL")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_FIXEDSIZE", ((Label)Reitem.FindControl("PUR_FIXEDSIZE")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_NUM", ((Label)Reitem.FindControl("PUR_NEDDNUM")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_NUNIT", ((Label)Reitem.FindControl("PUR_NUNIT")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_USTNUM", '0');
                sqlCmd.Parameters.AddWithValue("@PUR_RPNUM", ((Label)Reitem.FindControl("PUR_NEDDNUM")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_PUNIT", ((Label)Reitem.FindControl("PUR_NUNIT")).Text.Replace(" ", ""));
                sqlCmd.Parameters.AddWithValue("@PUR_STDATE", DateTime.Now.ToString("yyyy-MM-dd"));
                sqlCmd.Parameters.AddWithValue("@PUR_PRONODE", '0');
                sqlCmd.Parameters.AddWithValue("@PUR_STATE", '0');
                int rowsnum = sqlCmd.ExecuteNonQuery();
            }
            string sqltext1 = "INSERT INTO TBPC_PCHSPLANRVW(PR_SHEETNO,PR_REVIEWA,PR_REVIEWANM,PR_REVIEWATIME,PR_STATE,PR_PRONODE) " +
                                               "VALUES('" + TextBox_pid.Text.ToString() + "' ,'" + TextBoxexecutorid.Text.ToString() + "','" + TextBoxexecutor.Text.ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','000','0')";
            DBCallCommon.ExeSqlText(sqltext1);
            string sqltext2 = "UPDATE TBPM_MPFORALLRVW SET MP_STATE='9',MP_PRSHEETNO='" + TextBox_pid.Text.ToString() + "' WHERE MP_STATE='8' AND MP_PJID='" + DropDownList_PJ.SelectedValue.ToString() + "' AND MP_ENGID='" + downlist_eng.SelectedValue.ToString() + "'";
            DBCallCommon.ExeSqlText(sqltext2);
            DBCallCommon.closeConn(sqlConn);
            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_check_list.aspx");
        }
        private void insertallinitialdata()
        {
            string sqltext = "";
            SqlConnection sqlConn = new SqlConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            sqlConn.Open();
            sqlCmd.Connection = sqlConn;
            DataTable glotb = SetDatetable();
            for (int i = 0; i < DropDownList_PJ.Items.Count; i++)
            {
                sqltext = "SELECT DISTINCT d.MP_ENGID AS PUR_ENGID,d.MP_ENGNAME AS PUR_ENGNAME " +
                               "FROM (SELECT MP_PID,MP_PJID,MP_ENGID,MP_ENGNAME FROM TBPM_MPFORBLJ " +
                               "UNION ALL SELECT MP_PID,MP_PJID,MP_ENGID,MP_ENGNAME FROM TBPM_MPFORDQJ " +
                               "UNION ALL SELECT MP_PID,MP_PJID,MP_ENGID,MP_ENGNAME FROM TBPM_MPFORDQO " +
                               "UNION ALL SELECT MP_PID,MP_PJID,MP_ENGID,MP_ENGNAME FROM TBPM_MPFORGFB " +
                               "UNION ALL SELECT MP_PID,MP_PJID,MP_ENGID,MP_ENGNAME FROM TBPM_MPFORHZY " +
                               "UNION ALL SELECT MP_PID,MP_PJID,MP_ENGID,MP_ENGNAME FROM TBPM_MPFORQLM) AS d " +
                               "WHERE (MP_PID IN (SELECT  MP_ID AS MP_PID FROM  TBPM_MPFORALLRVW WHERE  (MP_STATE = '8')) AND MP_PJID='" + DropDownList_PJ.Items[i].Value.ToString() + "') ";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        generate_caigoudan(DropDownList_PJ.Items[i].Value.ToString(), dt.Rows[j]["PUR_ENGID"].ToString());
                        string expression = "PUR_PJID='" + DropDownList_PJ.Items[i].Value.ToString() + "' AND  PUR_ENGID ='" + dt.Rows[j]["PUR_ENGID"].ToString() + "'";
                        string sortOrder = "PUR_MARID ASC";
                        DataRow[] foundRows = glotb.Select(expression, sortOrder);
                        for (int k = 0; k < foundRows.Length; k++)
                        {

                            sqltext = "INSERT INTO TBPC_PURCHASEPLAN(PUR_PCODE,PUR_PJID,PUR_PJNAME,PUR_ENGID,PUR_ENGNAME,PUR_PTCODE,PUR_MARID,PUR_MARNAME,PUR_MARNORM,PUR_MARTERIAL,PUR_FIXEDSIZE,PUR_NUM,PUR_NUNIT,PUR_USTNUM,PUR_RPNUM,PUR_PUNIT,PUR_STDATE,PUR_PRONODE,PUR_STATE) " +
                                                             "VALUES(@PUR_PCODE,@PUR_PJID,@PUR_PJNAME,@PUR_ENGID,@PUR_ENGNAME,@PUR_PTCODE,@PUR_MARID,@PUR_MARNAME,@PUR_MARNORM,@PUR_MARTERIAL,@PUR_FIXEDSIZE,@PUR_NUM,@PUR_NUNIT,@PUR_USTNUM,@PUR_RPNUM,@PUR_PUNIT,@PUR_STDATE,@PUR_PRONODE,@PUR_STATE)";
                            sqlCmd.CommandText = sqltext;
                            sqlCmd.Parameters.Clear();
                            sqlCmd.Parameters.AddWithValue("@PUR_PCODE", TextBox_pid.Text.ToString());
                            sqlCmd.Parameters.AddWithValue("@PUR_PJID", DropDownList_PJ.Items[i].Value.ToString());
                            sqlCmd.Parameters.AddWithValue("@PUR_PJNAME", DropDownList_PJ.Items[i].Text.ToString());
                            sqlCmd.Parameters.AddWithValue("@PUR_ENGID", dt.Rows[j]["PUR_ENGID"].ToString());
                            sqlCmd.Parameters.AddWithValue("@PUR_ENGNAME", dt.Rows[j]["PUR_ENGNAME"].ToString());
                            sqlCmd.Parameters.AddWithValue("@PUR_PTCODE", TextBox_pid.Text.ToString() + "_" + (k + 1).ToString().PadLeft(4, '0'));
                            sqlCmd.Parameters.AddWithValue("@PUR_MARID", foundRows[k]["PUR_MARID"].ToString());
                            sqlCmd.Parameters.AddWithValue("@PUR_MARNAME", foundRows[k]["PUR_MARNAME"].ToString());
                            sqlCmd.Parameters.AddWithValue("@PUR_MARNORM", foundRows[k]["PUR_MARNORM"].ToString());
                            sqlCmd.Parameters.AddWithValue("@PUR_MARTERIAL", foundRows[k]["PUR_MARTERIAL"].ToString());
                            sqlCmd.Parameters.AddWithValue("@PUR_FIXEDSIZE", foundRows[k]["PUR_FIXEDSIZE"].ToString());
                            sqlCmd.Parameters.AddWithValue("@PUR_NUM", foundRows[k]["PUR_NEDDNUM"].ToString());
                            sqlCmd.Parameters.AddWithValue("@PUR_NUNIT", foundRows[k]["PUR_NUNIT"].ToString());
                            sqlCmd.Parameters.AddWithValue("@PUR_USTNUM", '0');
                            sqlCmd.Parameters.AddWithValue("@PUR_RPNUM", foundRows[k]["PUR_NEDDNUM"].ToString());
                            sqlCmd.Parameters.AddWithValue("@PUR_PUNIT", foundRows[k]["PUR_NUNIT"].ToString());
                            sqlCmd.Parameters.AddWithValue("@PUR_STDATE", DateTime.Now.ToString("yyyy-MM-dd"));
                            sqlCmd.Parameters.AddWithValue("@PUR_PRONODE", '0');
                            sqlCmd.Parameters.AddWithValue("@PUR_STATE", '0');
                            int rowsnum = sqlCmd.ExecuteNonQuery();
                        }
                        string sqltext1 = "INSERT INTO TBPC_PCHSPLANRVW(PR_SHEETNO,PR_REVIEWA,PR_REVIEWANM,PR_REVIEWATIME,PR_STATE,PR_PRONODE) " +
                                               "VALUES('" + TextBox_pid.Text.ToString() + "' ,'" + TextBoxexecutorid.Text.ToString() + "','" + TextBoxexecutor.Text.ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','000','0')";
                        DBCallCommon.ExeSqlText(sqltext1);
                    }
                }
            }
            string sqltext2 = "UPDATE TBPM_MPFORALLRVW SET MP_STATE='9',MP_PRSHEETNO='" + TextBox_pid.Text.ToString() + "'WHERE MP_STATE='8'";
            DBCallCommon.ExeSqlText(sqltext2);
            DBCallCommon.closeConn(sqlConn);
            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_check_list.aspx");
        }
        private void generate_caigoudan(string pjid,string engid)
        {
            string pi_id = "";
            string tag_pi_id = pjid + "/" + engid;
            string end_pi_id = "";
            string sqltext = "SELECT TOP 1 PR_SHEETNO FROM TBPC_PCHSPLANRVW WHERE PR_SHEETNO LIKE '" + tag_pi_id + "%' ORDER BY PR_SHEETNO DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                end_pi_id = Convert.ToString(Convert.ToInt32((dt.Rows[0]["PR_SHEETNO"].ToString().Substring(dt.Rows[0]["PR_SHEETNO"].ToString().Length - 4, 4))) + 1);
                end_pi_id = end_pi_id.PadLeft(4, '0');
            }
            else
            {
                end_pi_id = "0001";
            }
            pi_id = tag_pi_id + "_"+end_pi_id;
            TextBox_pid.Text = pi_id;
        }
        public string get_pur_fixed(string i)
        {
            string statestr = "";
            if (i == "0")
            {
                statestr = "不定尺";
            }
            else
            {
                if (i == "1")
                {
                    statestr = "定尺";
                }
            }
            return statestr;
        }
    }
}
