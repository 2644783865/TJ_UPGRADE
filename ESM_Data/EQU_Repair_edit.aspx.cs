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
    public partial class EQU_Repair_edit : System.Web.UI.Page
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
            string sqltext = "";
            string code = "";
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
                code = generatecode();
                Tb_shijian.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                tb_dep.Text = Session["UserDept"].ToString();
                tb_depid.Text = Session["UserDeptID"].ToString();
                TextBoxexecutor.Text = Session["UserName"].ToString();
                TextBoxexecutorid.Text = Session["UserID"].ToString();
                TextBox_pid.Text = code;
                CreateNewRow();
            }
            else
            {
                TextBox_pid.Enabled = false;
                TextBox_pid.Text = Request.QueryString["id"].ToString();
                sqltext = "select * from View_EQU_Repair where DocuNum='" + TextBox_pid.Text + "'";
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
                sqltext = "select EquName,EquType,Type,XQtime,Reason from View_EQU_Repair where DocuNum='" + TextBox_pid.Text + "'";
                SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sqltext);
                Type.SelectedValue = dr1["Type"].ToString();//0机修，1电修，2其他
                dr1.Close();
                DBCallCommon.BindRepeater(EQU_Repair_List_Repeater, sqltext);
            }
        }
        protected string generatecode()
        {
            string code = "";
            string sqltext = "select top 1 DocuNum from EQU_Repair_Need order by DocuNum desc";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                code = Convert.ToString((Convert.ToInt32(dt.Rows[0]["DocuNum"].ToString().Substring(dt.Rows[0]["DocuNum"].ToString().Length - 4, 4))) + 1);
                code = code.PadLeft(4, '0');
            }
            else
            { 
                code="0001";
            }
            code = "weixiu" + code;
            return code;
        }
        //定义DataTable
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EquName");
            dt.Columns.Add("EquType");
            dt.Columns.Add("XQtime");
            dt.Columns.Add("Reason");
            for (int i = 0; i < EQU_Repair_List_Repeater.Items.Count; i++)
            {
                RepeaterItem Reitem = EQU_Repair_List_Repeater.Items[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)Reitem.FindControl("EquName")).Text;
                newRow[1] = ((TextBox)Reitem.FindControl("EquType")).Text;
                newRow[2] = ((TextBox)Reitem.FindControl("XQtime")).Text;
                newRow[3] = ((TextBox)Reitem.FindControl("Reason")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }
        protected void EQU_Repair_List_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
        //生成输入1行函数
        private void CreateNewRow()
        {
            DataTable dt = this.GetDataTable();
            DataRow newRow = dt.NewRow();
            dt.Rows.Add(newRow);
            this.EQU_Repair_List_Repeater.DataSource = dt;
            this.EQU_Repair_List_Repeater.DataBind();
        }
        protected void EquName_TextChanged(object sender, EventArgs e)
        {
            string name = "";
            string type = "";
            for (int i = 0; i < EQU_Repair_List_Repeater.Items.Count; i++)
            {
                RepeaterItem Reitem = EQU_Repair_List_Repeater.Items[i];
                TextBox EquName = (TextBox)Reitem.FindControl("EquName");
                TextBox EquType = (TextBox)Reitem.FindControl("EquType");  
                if (EquName.Text.ToString().Contains("|"))
               {
                   name = EquName.Text.Substring(0, EquName.Text.ToString().IndexOf("|"));
                   type = EquName.Text.Substring(EquName.Text.ToString().IndexOf("|") + 1);
                   EquName.Text = name;
                   EquType.Text = type;
               }
               else
               {
                  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请正确填写名称！');", true);
               }
            }
           
            
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["action"].ToString() == "add")
            {
                if (Type.SelectedIndex != 0)
                {
                    string sqltext = "";
                    string EquName = "";
                    string EquType = "";
                    string Reason = "";
                    foreach (RepeaterItem Reitem in EQU_Repair_List_Repeater.Items)
                    {
                        EquName = ((TextBox)Reitem.FindControl("EquName")).Text.Trim();
                        EquType = ((TextBox)Reitem.FindControl("EquType")).Text.Trim();
                        Reason = ((TextBox)Reitem.FindControl("Reason")).Text.Trim();
                    }
                    if (Reason != "")
                    {
                        sqltext = "insert into EQU_Repair_Need (DocuNum,EquName,EquType,SQtime,Type,Reason,SQRID,SPZT,USEDEPID,DocuPersonID,REVIEWA)" +
                            "values('" + TextBox_pid.Text.Trim().ToString() + "','" + EquName + "','" + EquType + "','" + Tb_shijian.Text + "','" + Type.SelectedValue.ToString() + "','" + Reason + "','" + cob_sqren.SelectedValue.ToString() + "','0','" + tb_depid.Text.Trim().ToString() + "','" + TextBoxexecutorid.Text.Trim().ToString() + "','" + cob_fuziren.SelectedValue.ToString() + "')";
                        DBCallCommon.ExeSqlText(sqltext);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功,点击查看提交审核！');window.location.href='EQU_Repair_List.aspx'", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page,this.GetType(),"","alert('请填写报修内容！');",true);
                    }
                }
                else { ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择维修类型！');", true); }

            }
            else
            {
                string sqltext = "";
                string EquName = "";
                string EquType = "";
                string Reason = "";
                foreach (RepeaterItem Reitem in EQU_Repair_List_Repeater.Items)
                {
                    EquName = ((TextBox)Reitem.FindControl("EquName")).Text.Trim();
                    EquType = ((TextBox)Reitem.FindControl("EquType")).Text.Trim();
                    Reason = ((TextBox)Reitem.FindControl("Reason")).Text.Trim();
                }
                if (Reason != "")
                {
                    sqltext = "update set EquName='" + EquName + "',EquType='" + EquType + "',Reason='" + Reason + "',SQRID='" + cob_sqren.SelectedValue.ToString() + "',SPZT='0',REVIEWA='" + cob_fuziren.SelectedValue.ToString() + "' where  DocuNum='" + TextBox_pid.Text.Trim().ToString() + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功,点击查看提交审核！');window.location.href='EQU_Repair_List.aspx'", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写报修内容！');", true);
                }
            }
            
        }
    }
}
