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
    public partial class TM_Tech_Assign_Split : System.Web.UI.Page
    {
        string[] fields;
        string engid;
        string engcode;
        string engname;
        string engtype;
        int count = 0;
        string sqlText = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            tsaid.Text = Request.QueryString["tsaid"];
            if (!IsPostBack)
            {
                sqlText = "select TSA_PJNAME,TSA_ENGNAME from View_TM_TaskAssign where TSA_ID='" + tsaid.Text + "' ";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                if (dr.Read())
                {
                    proname.Value = dr["TSA_PJNAME"].ToString();
                    taskname.Value = dr["TSA_ENGNAME"].ToString();
                }
                dr.Close();
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
        /// 定义DataTable
        /// </summary>
        /// <returns></returns>
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ENG_ID");
            dt.Columns.Add("TSA_ID");
            dt.Columns.Add("ENG_NAME");
            dt.Columns.Add("TSA_ENGSTRSMTYPE");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = gr.Cells[2].Text;
                newRow[1] = gr.Cells[3].Text;
                newRow[2] = ((HtmlInputText)gr.FindControl("engname")).Value;
                newRow[3] = gr.Cells[5].Text;
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
        }

        /// <summary>
        /// 任务细分
        /// </summary>
        /// <param name="num"></param>
        private void TasCreateNewRow(int num)
        {
            DataTable dt = this.GetDataTable();
            for (int i = GridView1.Rows.Count; i < GridView1.Rows.Count + num; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow["ENG_ID"] = fields[0].ToString();
                newRow["TSA_ID"] = tsaid.Text + '-' + (count + i);
                newRow["TSA_ENGSTRSMTYPE"] = engtype;
                newRow["ENG_NAME"] = taskname.Value;
                dt.Rows.Add(newRow);
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }

        /// <summary>
        /// 已细分任务数
        /// </summary>
        private void TsaNumData()
        {
            sqlText = "select TOP 1 dbo.GetSplitTaskIndex(TSA_ID) AS SplitTaskIndex,TSA_ENGSTRTYPE,TSA_ENGSTRSMTYPE from View_TM_TaskAssign ";
            sqlText += "where TSA_ID like '" + tsaid.Text + "%' ORDER BY dbo.GetSplitTaskIndex(TSA_ID) DESC";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);

            if (dr.HasRows)
            {
                dr.Read();
                count = int.Parse(dr["SplitTaskIndex"].ToString()) + 1;
                engtype= dr["TSA_ENGSTRSMTYPE"].ToString();
                ViewState["engtype"] = dr["TSA_ENGSTRTYPE"].ToString();
                dr.Close();
            }
        }
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTsaAdd_Click(object sender, EventArgs e)
        {
            if (this.CheckFatherPDAssigned())
            {
                TsaNumData();
                fields = tsaid.Text.Split('/');
                TasCreateNewRow(Convert.ToInt32(tsanum.Value));
                InitVar();
            }
            else
            {
                string fathertaskid = tsaid.Text;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('生产制号\"" + fathertaskid + "\"未分工，无法拆分！！！');", true);
            }
        }
        /// <summary>
        /// 工程拆分后保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTsaSave_Click(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            string ret = "OK";

            //工程名称不能为空
            for (int j = 0; j < GridView1.Rows.Count; j++)
            {
                GridViewRow grow = GridView1.Rows[j];
                string na = ((HtmlInputText)grow.FindControl("engname")).Value.Trim();
                if (na == "")
                {
                    ret = "Empty";
                    break;
                }
            }

            //GridView没有数据
            if (GridView1.Rows.Count == 0)
            {
                ret = "NoData";
            }

            if (ret == "OK")
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    string sqlText_Assign = "";//任务分工
                    string[] quantityType = this.QuantityType();
                    GridViewRow gr = GridView1.Rows[i];
                    engid = gr.Cells[3].Text;
                    fields = tsaid.Text.Split('/');
                    engcode = fields[0].ToString();

                    engname = ((HtmlInputText)gr.FindControl("engname")).Value;
                    //技术员任务分工表(TBPM_TCTSASSGN)
                    sqlText_Assign="insert into TBPM_TCTSASSGN("+
                    "TSA_ID, TSA_PJID, TSA_ENGNAME, TSA_ENGSTRTYPE, TSA_ENGSTRSMTYPE, TSA_TCCLERK, TSA_DELIVERY, TSA_PREPARE, TSA_ENGTYPE," +
                    "TSA_STFORCODE, TSA_DRAWCODE, TSA_DEVICECODE, TSA_DESIGNCOM, TSA_MODELCODE, TSA_RECVDATE, TSA_CONFNSHDATE, TSA_CONTYPE," +
                    "TSA_DRAWSTATE, TSA_CLIENT, TSA_STARTDATE, TSA_PLANFSDATE, TSA_REALFSDATE, TSA_PAINTINGPLAN, TSA_TECHSHARING," +
                    "TSA_PLPRESCHEDULE, TSA_THIRDPART, TSA_MANCLERK, TSA_STATE, TSA_NOTE, TSA_FATHERNODE, TSA_STATUS, TSA_ASSIGNTOELC,TSA_ADDTIME,TSA_NUMBER) " +
                    " select "+
                    "'" + engid + "', TSA_PJID, TSA_ENGNAME, TSA_ENGSTRTYPE, TSA_ENGSTRSMTYPE, '', TSA_DELIVERY, TSA_PREPARE, TSA_ENGTYPE," +
                    "TSA_STFORCODE, TSA_DRAWCODE, TSA_DEVICECODE, TSA_DESIGNCOM, TSA_MODELCODE, TSA_RECVDATE, TSA_CONFNSHDATE, ''," +
                    "TSA_DRAWSTATE, TSA_CLIENT, TSA_STARTDATE, TSA_PLANFSDATE, TSA_REALFSDATE, TSA_PAINTINGPLAN, TSA_TECHSHARING," +
                    "TSA_PLPRESCHEDULE, TSA_THIRDPART, TSA_MANCLERK, '0', TSA_NOTE, '1', TSA_STATUS, TSA_ASSIGNTOELC,TSA_ADDTIME,TSA_NUMBER " +
                    " from TBPM_TCTSASSGN where TSA_ID='" + tsaid.Text.Trim() + "'";
                            
                    //////////sqlText_Assign = "insert into TBPM_TCTSASSGN ";
                    //////////sqlText_Assign += "(TSA_ID,TSA_PJID,TSA_ENGNAME,TSA_ENGSTRTYPE,";
                    //////////sqlText_Assign += "TSA_STFORCODE,TSA_FATHERNODE,TSA_ENGTYPE,TSA_ENGSTRSMTYPE) values('" + engid + "',";
                    //////////sqlText_Assign += "'" + fields[1].ToString() + "','" + engname + "',";
                    //////////sqlText_Assign += "'" + ViewState["engtype"].ToString() + "','" + engcode + "','1','" + quantityType[0] + "','" + quantityType[1] + "')";
                    list_sql.Add(sqlText_Assign);
                }

                DBCallCommon.ExecuteTrans(list_sql);
                Response.Redirect("TM_Tech_assign.aspx");
            }
            else if (ret == "Empty")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！\\r\\r填写工程名称！！！');", true);
            }
            else if (ret == "NoData")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！\\r\\r没有数据！！！');", true);
            }
        }
        /// <summary>
        /// 判断不带“-”生产制号是否任务分工
        /// </summary>
        /// <returns></returns>
        protected bool CheckFatherPDAssigned()
        {
            string fathertaskid=tsaid.Text;
            string sql_taskassign_state = "select isnull(TSA_STATE,'0') as TSA_STATE from View_TM_TaskAssign where TSA_ID='" + fathertaskid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_taskassign_state);
            if (dt.Rows[0]["TSA_STATE"].ToString() == "0")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 返回工程量统计类型及工程小类
        /// </summary>
        /// <returns></returns>
        protected string[] QuantityType()
        {
            string[] ret=new string[2];
            string sql_select_quantityType = "select TSA_ENGTYPE,TSA_ENGSTRSMTYPE from View_TM_TaskAssign where TSA_ID='" + tsaid.Text.Trim() + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_select_quantityType);
            if (dr.HasRows)
            {
                dr.Read();
                ret[0] = dr["TSA_ENGTYPE"].ToString();
                ret[1] = dr["TSA_ENGSTRSMTYPE"].ToString();
                dr.Close();
            }
            dr.Close();
            return ret;
        }
    }
}
