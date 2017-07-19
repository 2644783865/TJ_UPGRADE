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

namespace ZCZJ_DPF.ESM_Data
{
    public partial class EQU_Need_edit : System.Web.UI.Page
    {
        public string type;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                initinfo();
            }
        }
        private void initinfo()
        {
            string sqltext = "";
            
            sqltext = "select ST_ID,ST_NAME from TBDS_STAFFINFO WHERE ST_DEPID='" + Session["UserDeptID"].ToString() + "'and ST_PD='0'order by ST_ID DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            cob_fuziren.DataSource = dt;
            cob_fuziren.DataTextField = "ST_NAME";
            cob_fuziren.DataValueField = "ST_ID";
            cob_fuziren.DataBind();
            cob_fuziren.SelectedIndex = 0;
            cob_sqren.DataSource = dt;
            cob_sqren.DataTextField = "ST_NAME";
            cob_sqren.DataValueField = "ST_ID";
            cob_sqren.DataBind();
            cob_sqren.SelectedValue = Session["UserID"].ToString();
            if (Request.QueryString["action"].ToString() == "add")
            {
                if (Request.QueryString["Type"].ToString() != null)
                {
                    type = Request.QueryString["Type"].ToString();
                }
                string pi_id = generatecode();
                Tb_shijian.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                tb_dep.Text = Session["UserDept"].ToString();
                tb_depid.Text = Session["UserDeptID"].ToString();
                TextBoxexecutor.Text = Session["UserName"].ToString();
                TextBoxexecutorid.Text = Session["UserID"].ToString();
               
                TextBox_pid.Text = pi_id;
                CreateNewRow();
            }
            else
            {
                TextBox_pid.Enabled = false;
                TextBox_pid.Text = Request.QueryString["id"].ToString();
                sqltext = "select * from View_EQUNeed_RVW where DocuNum='" + TextBox_pid.Text + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr.Read())
                {
                    tb_dep.Text = dr["USEDEPNAME"].ToString();
                    tb_depid.Text = dr["USEDEPID"].ToString();
                    Tb_shijian.Text = dr["SQtime"].ToString();
                    cob_fuziren.SelectedValue = dr["REVIEWA"].ToString();
                    cob_sqren.SelectedValue = dr["SQRID"].ToString();
                    TextBoxexecutor.Text = dr["DocuPerson"].ToString();
                    TextBoxexecutorid.Text = dr["DocuPersonID"].ToString();
                }
                dr.Close();
                sqltext = "select EquName,EquType,EquNum,XQtime,Reason from View_EQUNeed_RVW where DocuNum='" + TextBox_pid.Text + "'";
                DBCallCommon.BindRepeater(EQU_Need_List_Repeater, sqltext);
            }
        }
        //定义DataTable
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EquName");
            dt.Columns.Add("EquType");
            dt.Columns.Add("EquNum");
            dt.Columns.Add("XQtime");
            dt.Columns.Add("Reason");
            for (int i = 0; i < EQU_Need_List_Repeater.Items.Count; i++)
            {
                RepeaterItem Reitem = EQU_Need_List_Repeater.Items[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)Reitem.FindControl("EquName")).Text;
                newRow[1] = ((TextBox)Reitem.FindControl("EquType")).Text;
                newRow[2] = ((TextBox)Reitem.FindControl("EquNum")).Text;
                newRow[3] = ((TextBox)Reitem.FindControl("XQtime")).Text;
                newRow[4] = ((TextBox)Reitem.FindControl("Reason")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }
        protected void EQU_Need_List_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
        //生成输入1行函数
        private void CreateNewRow()
        {
            DataTable dt = this.GetDataTable();
            DataRow newRow = dt.NewRow();
            dt.Rows.Add(newRow);
            this.EQU_Need_List_Repeater.DataSource = dt;
            this.EQU_Need_List_Repeater.DataBind();
        }
        protected string generatecode()
        {
            string i="";
            string pi_id = "";
            string end_pi_id = "";
            if (type.ToString() == "shebei")
            {
                i = "shebei";
                string sqltext = "SELECT TOP 1 DocuNum FROM EQU_Need WHERE DocuNum LIKE 'shebei%' ORDER BY DocuNum DESC";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    end_pi_id = Convert.ToString(Convert.ToInt32((dt.Rows[0]["DocuNum"].ToString().Substring(dt.Rows[0]["DocuNum"].ToString().Length - 4, 4))) + 1);
                    end_pi_id = end_pi_id.PadLeft(4, '0');
                }
                else
                {
                    end_pi_id = "0001";
                }
            }
            else if (type.ToString() == "beijian")
            {
                i = "beijian";
                string sqltext = "SELECT TOP 1 DocuNum FROM EQU_Need WHERE DocuNum LIKE 'beijian%' ORDER BY DocuNum DESC";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    end_pi_id = Convert.ToString(Convert.ToInt32((dt.Rows[0]["DocuNum"].ToString().Substring(dt.Rows[0]["DocuNum"].ToString().Length - 4, 4))) + 1);
                    end_pi_id = end_pi_id.PadLeft(4, '0');
                }
                else
                {
                    end_pi_id = "0001";
                }
            }
            pi_id = i + end_pi_id;
            return pi_id;
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["action"].ToString() == "add")
            {
                string sqltext = "";
                string EquName = "";
                string EquType = "";
                string EquNum = "";
                string XQtime = "";
                string Reason = "";
                foreach (RepeaterItem Reitem in EQU_Need_List_Repeater.Items)
                {
                    EquName = ((TextBox)Reitem.FindControl("EquName")).Text.Trim();
                    EquType = ((TextBox)Reitem.FindControl("EquType")).Text.Trim();
                    EquNum = ((TextBox)Reitem.FindControl("EquNum")).Text.Trim();
                    XQtime = ((TextBox)Reitem.FindControl("XQtime")).Text.Trim();
                    Reason = ((TextBox)Reitem.FindControl("Reason")).Text.Trim();
                }
                sqltext = "insert into EQU_Need (DocuNum,EquName,EquType,EquNum,SQtime,XQtime,Reason,SQRID,SPZT,USEDEPID,DocuPersonID,REVIEWA)" +
                    "values('" + TextBox_pid.Text.Trim().ToString() + "','" + EquName + "','" + EquType + "','" + EquNum + "','" + Tb_shijian.Text + "','" + XQtime + "','" + Reason + "','" + cob_sqren.SelectedValue.ToString() + "','0','" + tb_depid.Text.Trim().ToString() + "','" + TextBoxexecutorid.Text.Trim().ToString() + "','" + cob_fuziren.SelectedValue.ToString() + "')";
                DBCallCommon.ExeSqlText(sqltext);
            }
            else
            {
                string sqltext = "";
                string EquName = "";
                string EquType = "";
                string EquNum = "";
                string XQtime = "";
                string Reason = "";
                foreach (RepeaterItem Reitem in EQU_Need_List_Repeater.Items)
                {
                    EquName = ((TextBox)Reitem.FindControl("EquName")).Text.Trim();
                    EquType = ((TextBox)Reitem.FindControl("EquType")).Text.Trim();
                    EquNum = ((TextBox)Reitem.FindControl("EquNum")).Text.Trim();
                    XQtime = ((TextBox)Reitem.FindControl("XQtime")).Text.Trim();
                    Reason = ((TextBox)Reitem.FindControl("Reason")).Text.Trim();
                }
                sqltext = "update set EquName='" + EquName + "',EquType='" + EquType + "',EquNum='" + EquNum + "',XQtime='" + XQtime + "',Reason='" + Reason + "',SQRID='" + cob_sqren.SelectedValue.ToString() + "',SPZT='0',REVIEWA='" + cob_fuziren.SelectedValue.ToString() + "' where  DocuNum='" + TextBox_pid.Text.Trim().ToString() + "'";
                DBCallCommon.ExeSqlText(sqltext);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功,点击查看提交审核！');window.location.href='EQU_Need_List.aspx'", true);
        }
    }
}
