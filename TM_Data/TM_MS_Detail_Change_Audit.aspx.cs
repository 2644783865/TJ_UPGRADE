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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MS_Detail_Change_Audit : System.Web.UI.Page
    {
        string sqlText = "";
        string change_id = "";
        string audit_id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
            }
        }
        //初始化页面
        private void InitInfo()
        {
            btnsubmit.Text = "提 交";
            change_id = Request.QueryString["id"];
            audit_id = Request.QueryString["ms_change_id"];
            if (change_id != null)
            {
                ms_no.Text = change_id;
            }
            else
            {
                ms_no.Text = audit_id;
            }
            sqlText = "select MS_PJNAME,MS_ENGNAME,MS_SUBMITNM,MS_SUBMITTM,";
            sqlText += "MS_REVIEWANAME,MS_REVIEWAADVC,MS_REVIEWATIME,";
            sqlText += "MS_REVIEWBNAME,MS_REVIEWBADVC,MS_REVIEWBTIME,";
            sqlText += "MS_REVIEWCNAME,MS_REVIEWCADVC,MS_REVIEWCTIME,";
            sqlText += "MS_ADATE,MS_STATE,MS_PJID,MS_ENGID ";
            sqlText += "from TBPM_MSCHANGERVW where MS_CODE='" + ms_no.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            while (dr.Read())
            {
                #region
                lab_proname.Text = dr[0].ToString();
                proname.Text = dr[0].ToString();
                lab_engname.Text = dr[1].ToString();
                engname.Text = dr[1].ToString();
                txt_editor.Text = dr[2].ToString();
                txt_plandate.Text = dr[3].ToString();
                plandate.Text = dr[3].ToString();
                txt_first.Text = dr[4].ToString();
                first_opinion.Text = dr[5].ToString();
                first_time.Text = dr[6].ToString();
                txt_second.Text = dr[7].ToString();
                second_opinion.Text = dr[8].ToString();
                second_time.Text = dr[9].ToString();
                txt_third.Text = dr[10].ToString();
                third_opinion.Text = dr[11].ToString();
                third_time.Text = dr[12].ToString();
                txt_approval.Text = dr[13].ToString();
                status.Value = dr[14].ToString();
                pro_id.Value = dr[15].ToString();
                tsa_id.Text = dr[16].ToString();
                #endregion
                if (int.Parse(status.Value) < 2)
                {
                    btnsave.Visible = true;
                }
                #region
                if (status.Value == "3")//一级审核驳回
                {
                    rblfirst.SelectedValue = "3";
                    btnsubmit.Enabled = false;
                    txt_first.Enabled = false;
                    hlSelect1.Enabled = false;
                    first_opinion.Enabled = false;
                    rblfirst.Enabled = false;
                    txt_second.Enabled = false;
                    hlSelect2.Enabled = false;
                    second_opinion.Enabled = false;
                    rblsecond.Enabled = false;
                    txt_third.Enabled = false;
                    hlSelect3.Enabled = false;
                    third_opinion.Enabled = false;
                    rblthird.Enabled = false;
                }
                if (status.Value == "5")//二级审核驳回
                {
                    rblfirst.SelectedValue = "4";
                    rblsecond.SelectedValue = "5";
                    btnsubmit.Enabled = false;
                    txt_first.Enabled = false;
                    hlSelect1.Enabled = false;
                    first_opinion.Enabled = false;
                    rblfirst.Enabled = false;
                    txt_second.Enabled = false;
                    hlSelect2.Enabled = false;
                    second_opinion.Enabled = false;
                    rblsecond.Enabled = false;
                    txt_third.Enabled = false;
                    hlSelect3.Enabled = false;
                    third_opinion.Enabled = false;
                    rblthird.Enabled = false;
                }
                if (status.Value == "7")//三级审核驳回
                {
                    rblfirst.SelectedValue = "4";
                    rblsecond.SelectedValue = "6";
                    rblthird.SelectedValue = "7";
                    btnsubmit.Enabled = false;
                    txt_first.Enabled = false;
                    hlSelect1.Enabled = false;
                    first_opinion.Enabled = false;
                    rblfirst.Enabled = false;
                    txt_second.Enabled = false;
                    hlSelect2.Enabled = false;
                    second_opinion.Enabled = false;
                    rblsecond.Enabled = false;
                    txt_third.Enabled = false;
                    hlSelect3.Enabled = false;
                    third_opinion.Enabled = false;
                    rblthird.Enabled = false;
                }
                if (status.Value == "4")//一级审核通过
                {
                    rblfirst.SelectedValue = "4";
                    txt_first.Enabled = false;
                    hlSelect1.Enabled = false;
                    first_opinion.Enabled = false;
                    rblfirst.Enabled = false;
                    txt_second.Enabled = false;
                }
                if (status.Value == "6")//二级审核通过
                {
                    rblfirst.SelectedValue = "4";
                    rblsecond.SelectedValue = "6";
                    btnsubmit.Text = "确 定";
                    txt_first.Enabled = false;
                    hlSelect1.Enabled = false;
                    first_opinion.Enabled = false;
                    rblfirst.Enabled = false;
                    txt_second.Enabled = false;
                    hlSelect2.Enabled = false;
                    second_opinion.Enabled = false;
                    rblsecond.Enabled = false;
                    txt_third.Enabled = false;
                }
                if (status.Value == "8" || status.Value == "9")//三级审核通过
                {
                    rblfirst.SelectedValue = "4";
                    rblsecond.SelectedValue = "6";
                    rblthird.SelectedValue = "8";
                    ImageVerify.Visible = true;
                    btnsubmit.Text = "确 定";
                    btnsubmit.Enabled = false;
                    txt_first.Enabled = false;
                    hlSelect1.Enabled = false;
                    first_opinion.Enabled = false;
                    rblfirst.Enabled = false;
                    txt_second.Enabled = false;
                    hlSelect2.Enabled = false;
                    second_opinion.Enabled = false;
                    rblsecond.Enabled = false;
                    txt_third.Enabled = false;
                    hlSelect3.Enabled = false;
                    third_opinion.Enabled = false;
                    rblthird.Enabled = false;
                }
                #endregion
            }
            dr.Close();
            sqlText = "select * from TBPM_MSCHANGE where MS_PID='" + ms_no.Text + "' and MS_STATE='0'";
            DBCallCommon.BindGridView(GridView2, sqlText);
            if (status.Value == "0" || status.Value == "1")
            {
                Panel4.Visible = false;
                sqlText = "select * from TBPM_MSCHANGE where MS_PID='" + ms_no.Text + "' and MS_STATE='" + status.Value + "'";
                DBCallCommon.BindGridView(GridView1, sqlText);
            }
            else
            {
                Panel1.Visible = false;
                sqlText = "select * from TBPM_MSCHANGE where MS_PID='" + ms_no.Text + "' and MS_STATE in ('2','3','4','5')";
                DBCallCommon.BindGridView(GridView3, sqlText);
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            #region
            string ms_id = "";
            string txt_tuhao = "";
            string txt_zongxu = "";
            string txt_name = "";
            string txt_guige = "";
            string txt_caizhi = "";
            string txt_num = "";
            string txt_uwght = "";
            string txt_tlwght = "";
            string txt_process = "";
            string txt_caseno = "";
            string txt_explain = "";
            string txt_note = "";
            #endregion
            #region
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                ms_id = ((Label)gr.FindControl("ms_id")).Text;
                txt_tuhao = ((TextBox)gr.FindControl("txt_tuhao")).Text;
                txt_zongxu = ((TextBox)gr.FindControl("txt_zongxu")).Text;
                txt_name = ((TextBox)gr.FindControl("txt_name")).Text;
                txt_guige = ((TextBox)gr.FindControl("txt_guige")).Text;
                txt_caizhi = ((TextBox)gr.FindControl("txt_caizhi")).Text;
                txt_num = ((TextBox)gr.FindControl("txt_num")).Text;
                txt_uwght = ((TextBox)gr.FindControl("txt_uwght")).Text;
                txt_tlwght = ((TextBox)gr.FindControl("txt_tlwght")).Text;
                txt_process = ((TextBox)gr.FindControl("txt_process")).Text;
                txt_caseno = ((TextBox)gr.FindControl("txt_caseno")).Text;
                txt_explain = ((TextBox)gr.FindControl("txt_explain")).Text;
                txt_note = ((TextBox)gr.FindControl("txt_note")).Text;
                sqlText = "insert into TBPM_MSCHANGE (MS_PID,MS_TUHAO,MS_ZONGXU,MS_PJID,MS_PJNAME,";
                sqlText += "MS_ENGID,MS_ENGNAME,MS_NAME,MS_GUIGE,MS_CAIZHI,MS_UNUM,MS_UWGHT,";
                sqlText += "MS_TLWGHT,MS_PROCESS,MS_TIMERQ,MS_ENVREFFCT,MS_NOTE,MS_STATE)";
                sqlText += " values ('" + ms_no.Text + "','" + txt_tuhao + "','" + txt_zongxu + "','" + pro_id.Value + "',";
                sqlText += "'" + lab_proname.Text + "','" + tsa_id.Text + "','" + lab_engname.Text + "','" + txt_name + "',";
                sqlText += "'" + txt_guige + "','" + txt_caizhi + "','" + txt_num + "','" + txt_uwght + "','" + txt_tlwght + "',";
                sqlText += "'" + txt_process + "','" + txt_caseno + "','" + txt_explain + "','" + txt_note + "','1')";
                DBCallCommon.ExeSqlText(sqlText);
            }
            #endregion
            sqlText = "update TBPM_MSCHANGERVW set MS_STATE='1' ";
            sqlText += "where MS_CODE='" + ms_no.Text + "' and MS_STATE='0'";
            DBCallCommon.ExeSqlText(sqlText);
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (txt_first.Text == "")
            {
                Response.Write("<script language=javascript>alert('提示:请选择下一审核人！');history.go(-1);</script>");
                return;
            }
            if (txt_first.Text != "" && rblfirst.SelectedValue == "")
            {
                sqlText = "update TBPM_MSCHANGE set MS_STATE='2' ";
                sqlText += "where MS_PID='" + ms_no.Text + "' and MS_STATE='1'";
                DBCallCommon.ExeSqlText(sqlText);
            }
            string TC_NAME = "";
            string TC_CODE = "";
            if (txt_first.Text != "" && txt_second.Text == "" && txt_third.Text == "")
            {
                TC_NAME = txt_first.Text;
                hlSelect2.Enabled = false;
                second_opinion.Enabled = false;
                rblsecond.Enabled = false;
                hlSelect3.Enabled = false;
                third_opinion.Enabled = false;
                rblthird.Enabled = false;
            }
            else if (txt_first.Text != "" && txt_second.Text != "" && txt_third.Text == "")
            {
                TC_NAME = txt_second.Text;
                second_opinion.Enabled = false;
                rblsecond.Enabled = false;
                hlSelect3.Enabled = false;
                third_opinion.Enabled = false;
                rblthird.Enabled = false;
            }
            else if (txt_first.Text != "" && txt_second.Text != "" && txt_third.Text != "")
            {
                TC_NAME = txt_third.Text;
                btnsubmit.Text = "确 定";
            }
            sqlText = "select ST_CODE from TBDS_STAFFINFO ";
            sqlText += "where ST_NAME='" + TC_NAME + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            while (dr.Read())
            {
                TC_CODE = dr[0].ToString();
            }
            dr.Close();
            if (txt_first.Text != "" && txt_second.Text == "" && txt_third.Text == "")
            {
                if (rblfirst.SelectedValue == "")
                {
                    sqlText = "update TBPM_MSCHANGERVW set MS_SUBMITTM='" + DateTime.Now.ToString() + "',";
                    sqlText += "MS_REVIEWANAME='" + TC_NAME + "',MS_REVIEWA='" + TC_CODE + "',MS_STATE='2' ";
                    sqlText += "where MS_CODE='" + ms_no.Text + "' and MS_STATE='1'";
                }
                if (rblfirst.SelectedValue == "3")
                {
                    sqlText = "update TBPM_MSCHANGERVW set ";
                    sqlText += "MS_REVIEWATIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "MS_REVIEWAADVC='" + first_opinion.Text + "',MS_STATE='" + rblfirst.SelectedValue + "' ";
                    sqlText += "where MS_CODE='" + ms_no.Text + "' and MS_STATE='2'";
                }
                else if (rblfirst.SelectedValue == "4")
                {
                    Response.Write("<script language=javascript>alert('请选择下一审核人！');history.go(-1);</script>");
                    return;
                }
            }
            else if (txt_first.Text != "" && txt_second.Text != "" && txt_third.Text == "")
            {
                if (rblfirst.SelectedValue == "4" && rblsecond.SelectedValue == "")
                {
                    sqlText = "update TBPM_MSCHANGE set MS_STATE='3' ";
                    sqlText += "where MS_PID='" + ms_no.Text + "' and MS_STATE='2'";
                    DBCallCommon.ExeSqlText(sqlText);
                    sqlText = "update TBPM_MSCHANGERVW set MS_REVIEWBNAME='" + TC_NAME + "',";
                    sqlText += "MS_REVIEWB='" + TC_CODE + "',MS_REVIEWATIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "MS_REVIEWAADVC='" + first_opinion.Text + "',MS_STATE='" + rblfirst.SelectedValue + "' ";
                    sqlText += "where MS_CODE='" + ms_no.Text + "' and MS_STATE='2'";
                }
                if (rblsecond.SelectedValue == "5")
                {
                    sqlText = "update TBPM_MSCHANGERVW set MS_REVIEWBTIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "MS_REVIEWBADVC='" + second_opinion.Text + "',MS_STATE='" + rblsecond.SelectedValue + "' ";
                    sqlText += "where MS_CODE='" + ms_no.Text + "' and MS_STATE='4'";
                }
                else if (rblsecond.SelectedValue == "6")
                {
                    Response.Write("<script language=javascript>alert('请选择下一审核人！');history.go(-1);</script>");
                    return;
                }
            }
            else if (txt_first.Text != "" && txt_second.Text != "" && txt_third.Text != "")
            {
                if (rblsecond.SelectedValue == "6" && rblthird.SelectedValue == "")
                {
                    sqlText = "update TBPM_MSCHANGERVW set MS_REVIEWCNAME='" + TC_NAME + "',";
                    sqlText += "MS_REVIEWC='" + TC_CODE + "',MS_REVIEWBTIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "MS_REVIEWBADVC='" + second_opinion.Text + "',MS_STATE='" + rblsecond.SelectedValue + "' ";
                    sqlText += "where MS_CODE='" + ms_no.Text + "' and MS_STATE='4'";
                }
                if (rblthird.SelectedValue == "8")
                {
                    sqlText = "update TBPM_MSCHANGERVW set MS_ADATE='" + DateTime.Now.ToString() + "',";
                    sqlText += "MS_REVIEWCTIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "MS_REVIEWCADVC='" + third_opinion.Text + "',MS_STATE='" + rblthird.SelectedValue + "' ";
                    sqlText += "where MS_CODE='" + ms_no.Text + "' and MS_STATE='6'";
                }
                else if (rblthird.SelectedValue == "7")
                {
                    sqlText = "update TBPM_MSCHANGERVW set MS_REVIEWCTIME='" + DateTime.Now.ToString() + "',";
                    sqlText += "MS_REVIEWCADVC='" + third_opinion.Text + "',MS_STATE='" + rblthird.SelectedValue + "' ";
                    sqlText += "where MS_CODE='" + ms_no.Text + "' and MS_STATE='6'";
                }
            }
            DBCallCommon.ExeSqlText(sqlText);
            if (rblthird.SelectedValue == "8")
            {
                btnsubmit.Enabled = false;
                sqlText = "update TBPM_MSCHANGE set MS_STATE='5' ";
                sqlText += "where MS_PID='" + ms_no.Text + "' and MS_STATE='3'";
                DBCallCommon.ExeSqlText(sqlText);
                Response.Write("<script>alert('制作明细变更审核通过!');location.href='TM_Leader_Task_Change.aspx';</script>");
            }
            if (rblfirst.SelectedValue == "3" || rblsecond.SelectedValue == "5" || rblthird.SelectedValue == "7")
            {
                btnsubmit.Enabled = false;
                sqlText = "update TBPM_MSCHANGE set MS_STATE='4' ";
                sqlText += "where MS_PID='" + ms_no.Text + "' and MS_STATE in ('2','3')";
                DBCallCommon.ExeSqlText(sqlText);
                Response.Write("<script>alert('制作明细变更驳回，审核终止!');location.href='TM_Leader_Task_Change.aspx';</script>");
            }
            else
            {
                Response.Write("<script>alert('制作明细变更审核已提交，请等待审核...');location.href='TM_Leader_Task_Change.aspx';</script>");
            }
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }
    }
}
