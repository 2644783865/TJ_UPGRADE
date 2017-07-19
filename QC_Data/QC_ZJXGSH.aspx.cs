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
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_ZJXGSH : System.Web.UI.Page
    {
        string action = "";
        string id = "";
        string afiid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ACTION"] != null)
            {
                action = Request.QueryString["ACTION"].ToString();
            }
            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"].ToString();
            }
            if (Request.QueryString["AFIID"] != null)
            {
                afiid = Request.QueryString["AFIID"].ToString();
            }
            if (!IsPostBack)
            {
                HyperLink1.Attributes.Add("onClick", "ShowViewModal('" + afiid + "');");
                InitVar();
            }
        }

        private void InitVar()
        {
            if (action == "view")
            {
                submit.Visible = false;
                panel1.Visible = false;
                BindData();
                panel2.Enabled = false;
                panel3.Enabled = false;
            }
            if (action == "add")
            {
                submit.Text = "提交";
                Notes.Visible = true;
                panel2.Visible = false;
                HyperLink1.Visible = false;
                txtapplicant.Text = Session["UserName"].ToString();
                
                applicantid.Value = Session["UserID"].ToString();
                
                txtapplytime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BindPSR();

            }
            if (action == "review")
            {
                submit.Text = "审核";
                panel1.Visible = false;
                panel3.Enabled = false;
                BindData();
                shenheliucheng();

            }
        }

        protected void submit_click(object sender, EventArgs e)
        {
            if (action == "add")
            {

                if (txtREASON.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('请填写质检修改原因！！！');", true);
                    return;
                }
                if (cbl_qfr.SelectedValue == "" || cbl_jsfzr.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('请指定评审人！！！');", true);
                    return;
                }

                string sql = "insert into TBQM_ZJXGSH (AFI_ID,AFI_MANCLERK,AFI_MANCLERK_ID,AFI_CLERK_SJ,AFI_CLERK_REASON,AFI_FIR_PER,AFI_FIR_PERID,AFI_SEC_PER,AFI_SEC_PERID,AFI_STATUS) VALUES"+
                             "('"+afiid+"','" + txtapplicant.Text + "','" + applicantid.Value + "','" + txtapplytime.Text + "','" + txtREASON.Text + "','"+cbl_qfr.SelectedItem.Text+"','" + cbl_qfr.SelectedValue + "','"+cbl_jsfzr.SelectedItem.Text+"','" + cbl_jsfzr.SelectedValue + "','0')";

                DBCallCommon.ExeSqlText(sql);
                sql = "update TBQM_APLYFORINSPCT set AFI_ISSH='1' where AFI_ID='"+afiid+"' ";
                DBCallCommon.ExeSqlText(sql);
                Response.Redirect("QC_Inspection_Manage.aspx", false);

            }
            if (action == "review")
            {
                string sqltext = "select * from TBQM_ZJXGSH where ID='" + id + "'";
                
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    string fir_perid = dt.Rows[0]["AFI_FIR_PERID"].ToString();
                    string sec_perid = dt.Rows[0]["AFI_SEC_PERID"].ToString();
                    string thi_perid = dt.Rows[0]["AFI_THI_PERID"].ToString();
                    string sql = "";
                    string jg="";
                    if (fir_perid == Session["UserID"].ToString())
                    {
                        if (RadioTY1.Checked != true && RadioJJ1.Checked != true)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('请填写意见！！！');", true);
                            return;
                        }
                        if (RadioTY1.Checked == true)
                        {
                            jg = "0";
                        }
                        if (RadioJJ1.Checked == true)
                        {
                            jg = "1";
                            if (TextBZ1.Text.Trim() == "")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('请填写拒绝理由！！！');", true);
                                return;
                            }   
                        }
                        sql = "UPDATE TBQM_ZJXGSH SET AFI_FIR_PER='" + TextSHR1.Text + "',AFI_FIR_YJ='" + TextBZ1.Text + "',AFI_FIR_SJ='" + DateTime.Now.ToString("yyyy-MM-dd")+"',AFI_FIR_JG='"+jg+"' where ID='"+id+"'  ";
                        DBCallCommon.ExeSqlText(sql);
                    }
                    if (sec_perid == Session["UserID"].ToString())
                    {
                        if (RadioTY2.Checked != true && RadioJJ2.Checked != true)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('请填写意见！！！');", true);
                            return;
                        }
                        if (RadioTY2.Checked == true)
                        {
                            jg = "0";
                        }
                        if (RadioJJ2.Checked == true)
                        {
                            jg = "1";
                            if (TextBZ2.Text.Trim() == "")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('请填写拒绝理由！！！');", true);
                                return;
                            }   
                        }
                        sql = "UPDATE TBQM_ZJXGSH SET AFI_SEC_PER='" + TextSHR2.Text + "',AFI_SEC_YJ='" + TextBZ2.Text + "',AFI_SEC_SJ='" + DateTime.Now.ToString("yyyy-MM-dd") + "',AFI_SEC_JG='" + jg + "',AFI_STATUS='1',AFI_JG='" + jg + "' where ID='" + id + "'  ";
                        DBCallCommon.ExeSqlText(sql);
                        sql = "update TBQM_APLYFORINSPCT set AFI_ISSH='2',AFI_SHJG='" + jg + "' where AFI_ID='" + afiid + "' ";
                        DBCallCommon.ExeSqlText(sql);
                    }
                    //if (thi_perid == Session["UserID"].ToString())
                    //{
                    //    if (RadioTY3.Checked != true && RadioJJ3.Checked != true)
                    //    {
                    //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('请填写意见！！！');", true);
                    //        return;
                    //    }
                    //    if (RadioTY3.Checked == true)
                    //    {
                    //        jg = "0";
                    //    }
                    //    if (RadioJJ3.Checked == true)
                    //    {
                    //        jg = "1";
                    //        if (TextBZ3.Text.Trim() == "")
                    //        {
                    //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('请填写拒绝理由！！！');", true);
                    //            return;
                    //        }   
                    //    }
                    //    sql = "UPDATE TBQM_ZJXGSH SET AFI_THI_PER='" + TextSHR3.Text + "',AFI_THI_YJ='" + TextBZ3.Text + "',AFI_THI_SJ='" + DateTime.Now.ToString("yyyy-MM-dd") + "',AFI_THI_JG='" + jg + "',AFI_STATUS='1',AFI_JG='"+jg+"'  where ID='" + id + "' ";
                    //    DBCallCommon.ExeSqlText(sql);
                    //    sql = "update TBQM_APLYFORINSPCT set AFI_ISSH='2',AFI_SHJG='"+jg+"' where AFI_ID='"+afiid+"' ";
                    //    DBCallCommon.ExeSqlText(sql);
                    //}
                }
                Response.Redirect("QC_ZJXGSH_TOTAL.aspx", false);
            }
        }

        private void BindData()
        {
            string sql = "select * from TBQM_ZJXGSH where ID='" + id + "' ";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                txtapplicant.Text = dr["AFI_MANCLERK"].ToString();
                txtapplytime.Text = dr["AFI_CLERK_SJ"].ToString();
                txtREASON.Text = dr["AFI_CLERK_REASON"].ToString();
                TextSHR1.Text = dr["AFI_FIR_PER"].ToString();
                TextBZ1.Text = dr["AFI_FIR_YJ"].ToString();
                TextSHRQ1.Text = dr["AFI_FIR_SJ"].ToString();
                TextSHR2.Text = dr["AFI_SEC_PER"].ToString();
                TextBZ2.Text = dr["AFI_SEC_YJ"].ToString();
                TextSHRQ2.Text = dr["AFI_SEC_SJ"].ToString();
                //TextSHR3.Text = dr["AFI_THI_PER"].ToString(); 
                //TextBZ3.Text = dr["AFI_THI_YJ"].ToString();
                //TextSHRQ3.Text = dr["AFI_THI_SJ"].ToString();

                string jg1 = dr["AFI_FIR_JG"].ToString();
                string jg2 = dr["AFI_SEC_JG"].ToString();
                //string jg3 = dr["AFI_THI_JG"].ToString();
                if (jg1 == "0")
                {
                    RadioTY1.Checked = true;
                }
                if (jg1 == "1")
                {
                    RadioJJ1.Checked = true;
                }

                if (jg2 == "0")
                {
                    RadioTY2.Checked = true;
                }
                if (jg2 == "1")
                {
                    RadioJJ2.Checked = true;
                }

                //if (jg3 == "0")
                //{
                //    RadioTY3.Checked = true;
                //}
                //if (jg3 =="1")
                //{
                //    RadioJJ3.Checked = true;
                //}
                
            }
        }

        private void BindPSR()
        {
            string sql = "";
            sql = "select st_code,st_name from TBDS_STAFFINFO where st_code like '0501%'";
            BindCheckbox(cbl_qfr, sql);

            sql = "select st_code,st_name from TBDS_STAFFINFO where st_code like '07%' ";
            BindCheckbox(cbl_jsfzr, sql);

            //sql = "select st_code,st_name from TBDS_STAFFINFO where st_code like '01%'";
            //BindCheckbox(cbl_pzr, sql);
        }

        private void BindCheckbox(CheckBoxList cbl, string sqltext)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            cbl.DataSource = dt;
            cbl.DataTextField = "ST_NAME";//姓名
            cbl.DataValueField = "ST_CODE";//编号
            cbl.DataBind();

            for (int k = 0; k < cbl.Items.Count; k++)
            {
                cbl.Items[k].Attributes.Add("Onclick", "CheckBoxList_Click(this)");//使用了javascript使其只能勾选一个

               
            }
        }

        




        private void shenheliucheng() //控制审批流程
        {
            string sqltext = "select * from TBQM_ZJXGSH where ID='" + id + "'";
                
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                string fir_perid = dt.Rows[0]["AFI_FIR_PERID"].ToString();
                string sec_perid = dt.Rows[0]["AFI_SEC_PERID"].ToString();
                string thi_perid = dt.Rows[0]["AFI_THI_PERID"].ToString();
                if (fir_perid == Session["UserID"].ToString())
                {
                    ERJI.Enabled =false;
                    
                }
                if (sec_perid == Session["UserID"].ToString())
                {
                    string fir_jg = dt.Rows[0]["AFI_FIR_JG"].ToString();
                    if (fir_jg == "")
                    {
                        ERJI.Enabled = false;
                        submit.Enabled = false;
                    }
                    YIJI.Enabled = false;
                    
                }
               
            }
        }

        protected void Back_click(object sender, EventArgs e)
        {
            if (action == "add")
            {
                Response.Redirect("QC_Inspection_Manage.aspx", false);
            }
            if (action == "review"||action=="view")
            {
                Response.Redirect("QC_ZJXGSH_TOTAL.aspx", false);
            }
        }

          
        
    }
}
