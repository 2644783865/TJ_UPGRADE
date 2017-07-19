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
using System.Data.SqlClient;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_TOOL_INBILL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initinfo();
            }
        }
        private void initinfo()
        {
            if (Request.QueryString["action"].ToString() == "add")
            {
                lblInDoc.Text = Session["UserName"].ToString();
                lblInDocID.Text = Session["UserID"].ToString();
                lblInDate.Text = DateTime.Now.ToString();
                newdocunum();
                CreateNewRow();
            }
        }
        private void newdocunum()
        {
            string sqltext;
            sqltext = "select TOP 1 dbo.GetCode(INCODE) AS TopIndex from TBMP_TOOL_IN ORDER BY dbo.GetCode(INCODE) DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int index;
            if (dt.Rows.Count > 0)
            {
                index = Convert.ToInt16(dt.Rows[0]["TopIndex"].ToString());
            }
            else
            {
                index = 0;
            }
            string code = (index + 1).ToString();
            lblInCode.Text = "TOOLIN" + code.PadLeft(4, '0');
        }
        //定义DataTable
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TYPE");
            dt.Columns.Add("NAME");

            dt.Columns.Add("MODEL");
            dt.Columns.Add("INNUM");
            dt.Columns.Add("NOTE");

            for (int i = 0; i < PM_TOOLIN_List_Repeater.Items.Count; i++)
            {
                RepeaterItem Reitem = PM_TOOLIN_List_Repeater.Items[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)Reitem.FindControl("TYPE")).Text;
                newRow[1] = ((TextBox)Reitem.FindControl("NAME")).Text;

                newRow[2] = ((TextBox)Reitem.FindControl("MODEL")).Text;
                newRow[3] = ((TextBox)Reitem.FindControl("INNUM")).Text;
                newRow[4] = ((TextBox)Reitem.FindControl("NOTE")).Text;

                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        //生成输入1行函数
        private void CreateNewRow()
        {
            DataTable dt = this.GetDataTable();
            DataRow newRow = dt.NewRow();
            dt.Rows.Add(newRow);
            this.PM_TOOLIN_List_Repeater.DataSource = dt;
            this.PM_TOOLIN_List_Repeater.DataBind();
        }


        protected void btn_save_Click(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            if (Request.QueryString["action"].ToString() == "add")
            {
                string sqltext = "";
                string TYPE = "";
                string NAME = "";
                string MODEL = "";
                string INNUM = "";
                string NOTE = "";
                foreach (RepeaterItem Reitem in PM_TOOLIN_List_Repeater.Items)
                {
                    TYPE = ((TextBox)Reitem.FindControl("TYPE")).Text.Trim();
                    NAME = ((TextBox)Reitem.FindControl("NAME")).Text.Trim();
                    MODEL = ((TextBox)Reitem.FindControl("MODEL")).Text.Trim();
                    INNUM = ((TextBox)Reitem.FindControl("INNUM")).Text.Trim();
                    NOTE = ((TextBox)Reitem.FindControl("NOTE")).Text.Trim();
                    sqltext = "insert into TBMP_TOOL_IN (INCODE,TYPE,NAME,MODEL,INNUM,RECEIVERID,INDATE,NOTE)" +
                    "values('" + lblInCode.Text.Trim().ToString() + "','" + TYPE + "','" + NAME + "','" + MODEL + "','" + INNUM + "','" + lblInDocID.Text.Trim().ToString() + "','" + lblInDate.Text.Trim().ToString() + "','" + NOTE + "')";
                    list_sql.Add(sqltext);
                    string sql = "select count(ID) as num FROM TBMP_TOOL_RESTORE WHERE TYPE='" + TYPE + "'AND NAME='" + NAME + "' AND MODEL='" + MODEL + "'";
                    DataTable dr1 = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dr1.Rows[0]["num"].ToString() !="0" )
                    {
                        string sqltext1 = "select distinct NUMBER  FROM TBMP_TOOL_RESTORE WHERE TYPE='" + TYPE + "'AND NAME='" + NAME + "' AND MODEL='" + MODEL + "'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext1);
                        int NUMBER = Convert.ToInt32(dt.Rows[0]["NUMBER"].ToString());
                        NUMBER = NUMBER + Convert.ToInt32(INNUM);
                        sqltext = "update TBMP_TOOL_RESTORE set NUMBER='" + NUMBER + "'WHERE TYPE='" + TYPE + "'AND NAME='" + NAME + "' AND MODEL='" + MODEL + "'";
                        list_sql.Add(sqltext);
                    }
                    else
                    {
                        sqltext = "insert into TBMP_TOOL_RESTORE (NUMBER,TYPE,NAME,MODEL) values('" + INNUM + "','" + TYPE + "','" + NAME + "','" + MODEL + "')";
                        list_sql.Add(sqltext);
                    }
                }
            }
            DBCallCommon.ExecuteTrans(list_sql);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功,点击查看提交审核！');window.location.href='PM_TOOL_IN.aspx'", true);
        }

        protected void btn_addrow_Click(object sender, EventArgs e)
        {
            CreateNewRow();
        }

        protected void btn_delectrow_Click(object sender, EventArgs e)
        {
            int count = 0;
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < PM_TOOLIN_List_Repeater.Items.Count; i++)
            {
                RepeaterItem Reitem = PM_TOOLIN_List_Repeater.Items[i];
                CheckBox chk = (CheckBox)Reitem.FindControl("CHK");
                if (chk.Checked)
                {
                    dt.Rows.RemoveAt(i - count);
                    count++;
                }
            }
            if (count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要删除的行！');", true);
            }
            this.PM_TOOLIN_List_Repeater.DataSource = dt;
            this.PM_TOOLIN_List_Repeater.DataBind();
        }
    }
}
