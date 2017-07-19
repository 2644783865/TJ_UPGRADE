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
using System.Collections.Generic;

namespace ZCZJ_DPF.ESM
{
    public partial class EQU_ApplyDetail : System.Web.UI.Page
    {
        string id = "";
        string state = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                { 
                    id = Request.QueryString["id"];
                }
                if (Request.QueryString["action"] == "add")
                {
                    span1.Visible = true;
                    Tab_spxx.Visible = false;
                    sqr.Text = Session["UserName"].ToString();
                    sqrq.Text = DateTime.Now.ToString();
                }
                else if (Request.QueryString["action"] == "view")
                {
                    bind_info();
                    txtCode.Enabled = false;
                    txtDepartment.Enabled = false;
                    txtName.Enabled = false;
                    txtNum.Enabled = false;
                    txtTime1.Enabled = false;
                    txtshr.Enabled = false;
                    txtType.Enabled = false;
                    txtCause.Enabled = false;
                    hlSelect0.Visible = false;
                   
                }
                else if (Request.QueryString["action"] == "mod")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('修改后将重新进入审批！');", true);
                    bind_info();
                    sqrq.Text = DateTime.Now.ToString();
                    txtCode.Enabled = false;
                    Tab_spxx.Visible = false;
                }
                else if (Request.QueryString["action"] == "review")
                {
                    bind_info();
                    txtCode.Enabled = false;
                    txtDepartment.Enabled = false;
                    txtName.Enabled = false;
                    txtNum.Enabled = false;
                    txtTime1.Enabled = false;
                    txtshr.Enabled = false;
                    txtType.Enabled = false;
                    txtCause.Enabled = false;
                    hlSelect0.Visible = false;
                   
                }
                else
                {
                    bind_info();
                    txtCode.Enabled = false;
                    txtDepartment.Enabled = false;
                    txtName.Enabled = false;
                    txtNum.Enabled = false;
                    txtTime1.Enabled = false;
                    txtshr.Enabled = false;
                    txtType.Enabled = false;
                    txtCause.Enabled = false;
                    hlSelect0.Visible = false;
                }
            }
        }
        public void bind_info()
        {
            string sqltext = "select Code,Dep,Name,Type,Num,Date,Cause,Applyer,SPZT,SQTIME,EQURVWA from EQU_Apply where Code='"+id+"'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read())
            {
                txtCode.Text = dr["Code"].ToString();
                txtDepartment.Text = dr["Dep"].ToString();
                txtName.Text = dr["Name"].ToString();
                txtType.Text = dr["Type"].ToString();
                txtNum.Text = dr["Num"].ToString();
                txtTime1.Text = dr["Date"].ToString();
                sqr.Text = dr["Applyer"].ToString();
                txtCause.Text = dr["Cause"].ToString();
                sqrq.Text = dr["SQTIME"].ToString();
                txtshr.Text = dr["EQURVWA"].ToString();
            }
            dr.Close();
            string sqltext1 = "select Code,Applyer,SQTIME,EQURVWA,EQURVWAADVC,EQURVWATIME,EQURVWB,EQURVWBADVC,EQURVWBTIME,STATE,NOTE from EQU_ApplyRVW where Code='" + id + "'";
            SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sqltext1);
            if (dr1.Read())
            {
                txt_first.Text = dr1["EQURVWA"].ToString();
                first_opinion.Text = dr1["EQURVWAADVC"].ToString();
                first_time.Text = dr1["EQURVWATIME"].ToString();
                txt_second.Text = dr1["EQURVWB"].ToString();
                second_opinion.Text = dr1["EQURVWBADVC"].ToString();
                second_time.Text = dr1["EQURVWBTIME"].ToString();
                lblState.Text = dr1["STATE"].ToString();
                state = lblState.Text;
                switch (state)
                { 
                    case "1"://二级在审
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = -1;
                        txt_first.Enabled = false;
                        hlSelect1.Visible = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;
                        if (Session["UserName"].ToString() == txt_first.Text)
                        {
                            txt_second.Enabled = false;
                            hlSelect2.Visible = false;
                            rblsecond.Enabled = false;
                            second_opinion.Enabled = false;
                        }
                        second_time.Text = DateTime.Now.ToString();
                        break;
                    case "2"://一级未通过
                        rblfirst.SelectedIndex = 1;
                        rblsecond.SelectedIndex = -1;
                        txt_first.Enabled = false;
                        hlSelect1.Visible = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;
                        txt_second.Enabled = false;
                        hlSelect2.Visible = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;
                        second_time.Text = DateTime.Now.ToString();
                        break;
                    case "3"://都已通过
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = 0;
                        txt_first.Enabled = false;
                        hlSelect1.Visible = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;
                        txt_second.Enabled = false;
                        hlSelect2.Visible = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;
                        break;
                    case "4"://二级驳回
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = -1;
                        txt_first.Enabled = false;
                        hlSelect1.Visible = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;
                        txt_second.Enabled = false;
                        hlSelect2.Visible = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;
                        break;
                    default://一级正在审批状态
                        rblfirst.SelectedIndex = -1;
                        rblsecond.SelectedIndex = -1;
                        txt_first.Enabled = false;
                        hlSelect1.Visible = false;
                        txt_second.Enabled = true;
                        hlSelect2.Visible = true;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;
                       first_time.Text = DateTime.Now.ToString();
                        break;
                }
            }
            dr1.Close();
        }
        protected void btnLoad_OnClick(object sender, EventArgs e)
        { 
            string sqltext = "";
            List<string> list_sql = new List<string>();
            if (Request.QueryString["action"] == "add")
            {
                sqltext = "insert into EQU_Apply(Code,Dep,Name,Type,Num,Date,Cause,Applyer,SPZT,SQTIME,EQURVWA) Values('" + txtCode.Text.Trim() + "','" + txtDepartment.Text.Trim() + "','" + txtName.Text.Trim() + "','" + txtType.Text.Trim() + "','" + txtNum.Text.Trim() + "','" + txtTime1.Text.Trim() + "','" + txtCause.Text.Trim() + "','" + sqr.Text.Trim() + "',1,'" + sqrq.Text.Trim() + "','" + txtshr.Text.Trim() + "')";
                list_sql.Add(sqltext);
                sqltext = "insert into EQU_ApplyRVW (Code,Applyer,SQTIME,STATE,EQURVWA) values('" + txtCode.Text.Trim() + "','" + sqr.Text.Trim() + "','" + sqrq.Text.Trim() + "',0,'" + txtshr.Text.Trim() + "')";
                list_sql.Add(sqltext);
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('申请成功，已发送邮件并进入审批！');window.opener=null;window.open('','_self');window.close();window.returnValue='refresh'", true);
                return;
            }
            else if (Request.QueryString["action"] == "mod")
            {
                sqltext = "update EQU_Apply set Dep='" + txtDepartment.Text.Trim() + "',Name='" + txtName.Text.Trim() + "',Type='" + txtType.Text.Trim() + "',Num='" + txtNum.Text.Trim() + "',Date='" + txtTime1.Text.Trim() + "',Cause='" + txtCause.Text.Trim() + "',EQURVWA='" + txtshr.Text.Trim() + "',SPZT=1,SQTIME='" + sqrq.Text.Trim() + "'where Code='" + txtCode.Text.Trim() + "'";
                list_sql.Add(sqltext);
                sqltext = "update EQU_ApplyRVW set SQTIME='" + sqrq.Text.Trim() + "',STATE=0 where Code='" + txtCode.Text.Trim() + "'";
                list_sql.Add(sqltext);
                DBCallCommon.ExecuteTrans(list_sql);
                Response.Redirect("EQU_Apply.aspx");
            }
            else if (Request.QueryString["action"] == "review")
            {
                if (lblState.Text == "1" && rblsecond.SelectedIndex == 0)
                {
                    sqltext = "update EQU_ApplyRVW set STATE=3,EQURVWBTIME='" + second_time.Text.Trim() + "',EQURVWBADVC='" + second_opinion.Text.Trim() + "' where Code='" + txtCode.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                    sqltext = "update EQU_Apply set SPZT=3 where Code='" + txtCode.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                }
                else if (lblState.Text == "1" && rblsecond.SelectedIndex == 1)
                {
                    sqltext = "update EQU_ApplyRVW set STATE=4,EQURVWBTIME='" + second_time.Text.Trim() + "',EQURVWBADVC='" + second_opinion.Text.Trim() + "' where Code='" + txtCode.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                    sqltext = "update EQU_Apply set SPZT=0 where Code='" + txtCode.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                }
                else if (lblState.Text == "0" && rblfirst.SelectedIndex == 0)
                {

                    sqltext = "update EQU_ApplyRVW set STATE=1,EQURVWB='" + txt_second.Text.Trim()+ "',EQURVWATIME='" + first_time.Text.Trim() + "',EQURVWAADVC='" + first_opinion.Text.Trim() + "' where Code='" + txtCode.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                    sqltext = "update EQU_Apply set SPZT=2 where Code='" + txtCode.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                }
                else if (lblState.Text == "0" && rblfirst.SelectedIndex == 1)
                {
                    
                    sqltext = "update EQU_ApplyRVW set STATE=2,,EQURVWATIME='" + first_time.Text.Trim() + "'EQURVWBTIME='" + second_time.Text.Trim() + "',EQURVWBADVC='" + second_opinion.Text.Trim() + "' where Code='" + txtCode.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                    sqltext = "update EQU_Apply set SPZT=0 where Code='" + txtCode.Text.Trim() + "'";
                    list_sql.Add(sqltext);
                }
                 DBCallCommon.ExecuteTrans(list_sql);
                Response.Redirect("EQU_Apply.aspx");
            }
        }
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
            Response.Redirect("EQU_Apply.aspx");
        }
    }
}
