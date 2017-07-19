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

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Expense_edit : System.Web.UI.Page
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
                txt_docunum.Text = newdocunum();
                CreateNewRow();
            }
            else
            {
                string sqltext;
                txt_docunum.Text = Request.QueryString["id"].ToString();
                sqltext = "select a.*,b.CM_CONTR,b.TSA_MAP,c.CM_CUSNAME from TBMP_GS_LIST AS a LEFT JOIN TBCM_FHBASIC AS b ON a.TSA_ID=b.TSA_ID LEFT JOIN TBCM_APPLICA AS c ON b.CM_CONTR=c.CM_CONTR where a.DOCUNUM='" + txt_docunum.Text.ToString() + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr.Read())
                {
                    ddlgongshiyear.SelectedValue = dr["DATEYEAR"].ToString();
                    ddlgongshimonth.SelectedValue = dr["DATEMONTH"].ToString();
                }
                dr.Close();
                DBCallCommon.BindRepeater(PM_GongShi_List_Repeater, sqltext);
            }
        }
        protected void TSA_ID_Textchanged(object sender, EventArgs e)
        {
            string tsaid = "";
            string sqltext = "";
            DataTable glotb = new DataTable();
            TextBox Tb_newtsaid = (TextBox)sender;//定义TextBox
            HtmlTableRow Reitem = ((HtmlTableRow)Tb_newtsaid.Parent.Parent);
            if (Tb_newtsaid.Text.ToString() != null)
            {
                tsaid = Tb_newtsaid.Text.ToString();

                sqltext = "SELECT a.TSA_ID,a.TSA_MAP,a.CM_CONTR,b.CM_CUSNAME from  TBCM_FHBASIC AS a  LEFT JOIN TBCM_APPLICA AS b ON a.CM_CONTR=b.CM_CONTR  WHERE a.TSA_ID ='" + tsaid + "'";
                glotb = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (glotb.Rows.Count > 0)
                {
                    Tb_newtsaid.Text = tsaid;
                    ((TextBox)Reitem.FindControl("TSA_ID")).Text = glotb.Rows[0]["TSA_ID"].ToString();
                    ((TextBox)Reitem.FindControl("CM_CUSNAME")).Text = glotb.Rows[0]["CM_CUSNAME"].ToString();
                    ((TextBox)Reitem.FindControl("CM_CONTR")).Text = glotb.Rows[0]["CM_CONTR"].ToString();
                    ((TextBox)Reitem.FindControl("TSA_MAP")).Text = glotb.Rows[0]["TSA_MAP"].ToString();

                }
                else
                {
                    showerrormessage(Tb_newtsaid, "输入的任务单号不存在，请重新输入！");
                    return;
                }
            }
        }
        protected void showerrormessage(TextBox tbx, string errormessage)
        {
            RepeaterItem Reitem = (RepeaterItem)tbx.Parent;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('" + errormessage + "');", true);

            ((TextBox)Reitem.FindControl("TSA_ID")).Text = "";
            ((HtmlInputText)Reitem.FindControl("CM_CUSNAME")).Value = "";
            ((HtmlInputText)Reitem.FindControl("CM_CONTR")).Value = "";

            ((HtmlInputText)Reitem.FindControl("TSA_MAP")).Value = "";

            tbx.Focus();
        }
        protected string newdocunum()
        {
            string sqltext = "";
            sqltext = "select top 1 DOCUNUM from TBMP_GS_LIST";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string max = "";
            if (dt.Rows.Count > 0)
            {
                int id = Convert.ToInt32((dt.Rows[0]["DOCUNUM"].ToString()));
                id++;
                max = Convert.ToString(id);
                max = max.PadLeft(4, '0');
            }
            else
            {
                max = "0001";
            }
            return max;
        }
        //定义DataTable
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TSA_ID");
            dt.Columns.Add("CM_CUSNAME");

            dt.Columns.Add("CM_CONTR");
            dt.Columns.Add("TSA_MAP");
            dt.Columns.Add("GS_NOTE");
            dt.Columns.Add("GS_HOURS");
            for (int i = 0; i < PM_GongShi_List_Repeater.Items.Count; i++)
            {
                RepeaterItem Reitem = PM_GongShi_List_Repeater.Items[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)Reitem.FindControl("TSA_ID")).Text;
                newRow[1] = ((TextBox)Reitem.FindControl("CM_CUSNAME")).Text;

                newRow[2] = ((TextBox)Reitem.FindControl("CM_CONTR")).Text;
                newRow[3] = ((TextBox)Reitem.FindControl("TSA_MAP")).Text;
                newRow[4] = ((TextBox)Reitem.FindControl("GS_NOTE")).Text;
                newRow[5] = ((TextBox)Reitem.FindControl("GS_HOURS")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }
        protected void PM_GongShi_List_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
        //生成输入1行函数
        private void CreateNewRow()
        {
            DataTable dt = this.GetDataTable();
            DataRow newRow = dt.NewRow();
            dt.Rows.Add(newRow);
            this.PM_GongShi_List_Repeater.DataSource = dt;
            this.PM_GongShi_List_Repeater.DataBind();
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["action"].ToString() == "add")
            {
                if (ddlgongshiyear.SelectedValue.ToString() != "%" && ddlgongshimonth.SelectedValue.ToString() != "%")
                {

                    string sqltext = "";
                    string TSA_ID = "";
                    string CM_CUSNAME = "";
                    string CM_CONTR = "";
                    string TSA_MAP = "";
                    string GS_NOTE = "";
                    string GS_HOURS = "";
                    foreach (RepeaterItem Reitem in PM_GongShi_List_Repeater.Items)
                    {
                        TSA_ID = ((TextBox)Reitem.FindControl("TSA_ID")).Text.Trim();
                        CM_CUSNAME = ((TextBox)Reitem.FindControl("CM_CUSNAME")).Text.Trim();
                        CM_CONTR = ((TextBox)Reitem.FindControl("CM_CONTR")).Text.Trim();
                        TSA_MAP = ((TextBox)Reitem.FindControl("TSA_MAP")).Text.Trim();
                        GS_NOTE = ((TextBox)Reitem.FindControl("GS_NOTE")).Text.Trim();
                        GS_HOURS = ((TextBox)Reitem.FindControl("GS_HOURS")).Text.Trim();
                        sqltext = "insert into TBMP_GS_LIST (DOCUNUM,TSA_ID,DATEYEAR,DATEMONTH,GS_NOTE,GS_HOURS,SPZT)" +
                        "values('" + txt_docunum.Text.Trim().ToString() + "','" + TSA_ID + "','" + ddlgongshiyear.SelectedValue.ToString() + "','" + ddlgongshimonth.SelectedValue.ToString() + "','" + GS_NOTE + "','" + GS_HOURS + "','0')";
                        DBCallCommon.ExeSqlText(sqltext);
                    }

                }

                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！');", true);
                }

            }
            else
            {
                string sqltext = "";
                string TSA_ID = "";
                string CM_CUSNAME = "";
                string CM_CONTR = "";
                string TSA_MAP = "";
                string GS_NOTE = "";
                string GS_HOURS = "";
                foreach (RepeaterItem Reitem in PM_GongShi_List_Repeater.Items)
                {
                    TSA_ID = ((TextBox)Reitem.FindControl("TSA_ID")).Text.Trim();
                    CM_CUSNAME = ((TextBox)Reitem.FindControl("CM_CUSNAME")).Text.Trim();
                    CM_CONTR = ((TextBox)Reitem.FindControl("CM_CONTR")).Text.Trim();
                    TSA_MAP = ((TextBox)Reitem.FindControl("TSA_MAP")).Text.Trim();
                    GS_NOTE = ((TextBox)Reitem.FindControl("GS_NOTE")).Text.Trim();
                    GS_HOURS = ((TextBox)Reitem.FindControl("GS_HOURS")).Text.Trim();
                }
                sqltext = "update TBMP_GS_LIST set GS_NOTE='" + GS_NOTE + "',GS_HOURS='" + GS_HOURS + "',CM_CUSNAME='" + CM_CUSNAME + "',CM_CONTR='" + CM_CONTR + "',TSA_MAP='" + TSA_MAP + "',SPZT='0' where  TSA_ID='" + TSA_ID.ToString() + "'";
                DBCallCommon.ExeSqlText(sqltext);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功,点击查看提交审核！');window.location.href='PM_GongShi_List.aspx'", true);
        }
        protected void btn_addrow_Click(object sender, EventArgs e)
        {
            CreateNewRow();
        }
        protected void btn_delectrow_Click(object sender, EventArgs e)
        {
            int count = 0;
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < PM_GongShi_List_Repeater.Items.Count; i++)
            {
                RepeaterItem Reitem = PM_GongShi_List_Repeater.Items[i];
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
            this.PM_GongShi_List_Repeater.DataSource = dt;
            this.PM_GongShi_List_Repeater.DataBind();
        }
    }
}
