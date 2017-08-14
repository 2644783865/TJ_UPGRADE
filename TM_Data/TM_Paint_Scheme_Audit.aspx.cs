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
using System.Text;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Paint_Scheme_Audit : System.Web.UI.Page
    {
        string sqlText;
        string action;
        string level;
        string status;
        string _emailto = "";
        string _body;
        string _subject;

        protected void Page_Load(object sender, EventArgs e)
        {
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            if (!IsPostBack)
            {
                InitInfo();
            }
        }
        /// <summary>
        /// 审核权限检查
        /// </summary>
        protected bool AuditPowerCheck()
        {
            bool retVal = true;
            if (Request.QueryString["ps_audit_id"] != null || Request.QueryString["id"] != null)
            {
                if (ViewState["status"].ToString() == "2")
                {
                    if (Session["UserName"].ToString() != txt_first.Text.Trim())
                    {
                        retVal = false;
                    }
                }
                else if (ViewState["status"].ToString() == "4")
                {
                    if (Session["UserName"].ToString() != txt_second.Text.Trim())
                    {
                        retVal = false;
                    }
                }
                else if (ViewState["status"].ToString() == "6")
                {
                    if (Session["UserName"].ToString() != txt_third.Text.Trim())
                    {
                        retVal = false;
                    }
                }
            }
            return retVal;
        }

        //初始化页面
        private void InitInfo()
        {
            if (Request.QueryString["id"] != null)
            {
                action = Request.QueryString["id"];
            }
            else if (Request.QueryString["ps_audit_id"] != null)
            {
                action = Request.QueryString["ps_audit_id"];
            }
            else if (Request.QueryString["rejectid"] != null)
            {
                action = Request.QueryString["rejectid"];
            }
            btnsubmit.Text = "提 交";
            sqlText = "select PS_ID,CM_PROJ,PS_PJID,TSA_ENGNAME,PS_PAINTBRAND,PS_SUBMITNM,";
            sqlText += "PS_SUBMITTM,PS_REVIEWANM,PS_REVIEWAADVC,PS_REVIEWATIME,";
            sqlText += "PS_REVIEWBNM,PS_REVIEWBADVC,PS_REVIEWBTIME,PS_REVIEWCNM,";
            sqlText += "PS_REVIEWCADVC,PS_REVIEWCTIME,PS_STATE,PS_CHECKLEVEL,PS_PJID,PS_SUBMITID ";
            sqlText += "from VIEW_TM_PAINTSCHEME where PS_ID='" + action + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                #region
                ps_no.Text = dr["PS_ID"].ToString();
                proname.Text = dr["CM_PROJ"].ToString();
                pro_name.Text = dr["CM_PROJ"].ToString();

                tsaid.Text = dr["PS_PJID"].ToString();
                engname.Text = dr["TSA_ENGNAME"].ToString();
                eng_name.Text = dr["TSA_ENGNAME"].ToString();
                paint_brand.Text = dr["PS_PAINTBRAND"].ToString();
                editor.Text = dr["PS_SUBMITNM"].ToString();
                editorid.Value = dr["PS_SUBMITID"].ToString();
                plandate.Text = dr["PS_SUBMITTM"].ToString();
                plan_date.Text = dr["PS_SUBMITTM"].ToString();
                txt_first.Text = dr["PS_REVIEWANM"].ToString();
                first_opinion.Text = dr["PS_REVIEWAADVC"].ToString();
                first_time.Text = dr["PS_REVIEWATIME"].ToString();
                txt_second.Text = dr["PS_REVIEWBNM"].ToString();
                second_opinion.Text = dr["PS_REVIEWBADVC"].ToString();
                second_time.Text = dr["PS_REVIEWBTIME"].ToString();
                txt_third.Text = dr["PS_REVIEWCNM"].ToString();
                third_opinion.Text = dr["PS_REVIEWCADVC"].ToString();
                third_time.Text = dr["PS_REVIEWCTIME"].ToString();
                state.Value = dr["PS_STATE"].ToString();
                rblSHJS.SelectedValue = dr["PS_CHECKLEVEL"].ToString();
                status = dr["PS_STATE"].ToString();
                ViewState["status"] = dr["PS_STATE"].ToString(); ;
                ViewState["level"] = dr["PS_CHECKLEVEL"].ToString();
                level = dr["PS_CHECKLEVEL"].ToString();
                proid.Value = dr["PS_PJID"].ToString();

                if (status == "1")
                {
                    rblSHJS.Enabled = true;

                    //hlSelect1.Enabled = true;                    
                    first_opinion.Enabled = false;
                    rblfirst.Enabled = false;

                    hlSelect2.Visible = false;
                    second_opinion.Enabled = false;
                    rblsecond.Enabled = false;
                    hlSelect3.Visible = false;
                    third_opinion.Enabled = false;
                    rblthird.Enabled = false;
                }
                else
                {
                    rblSHJS.Enabled = false;
                    isenabled(status, level);
                }
                #endregion
            }
            dr.Close();
            sqlText = "select * from View_TM_PAINTSCHEMEDETAIL where PS_PID='" + ps_no.Text + "' ";
            DBCallCommon.BindRepeater(Repeater1, sqlText);
        }

        private void isenabled(string status, string level)
        {
            if ((Request.QueryString["rejectid"] == null))
            {

                if (level == "1")
                {
                    if (status == "2")  //提交
                    {
                        //hlSelect1.Enabled = false;
                        txt_first.Enabled = false;
                        rblfirst.Enabled = true;
                        first_opinion.Enabled = true;

                        hlSelect2.Enabled = false;
                        txt_second.Enabled = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;

                        hlSelect3.Enabled = false;
                        txt_third.Enabled = false;
                        rblthird.Enabled = false;
                        third_opinion.Enabled = false;

                    }
                    if (status == "3")  //一级驳回
                    {
                        rblfirst.SelectedValue = "3";
                        btnsubmit.Visible = false;

                        //hlSelect1.Enabled = false;
                        txt_first.Enabled = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;

                        hlSelect2.Enabled = false;
                        txt_second.Enabled = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;

                        hlSelect3.Enabled = false;
                        txt_third.Enabled = false;
                        rblthird.Enabled = false;
                        third_opinion.Enabled = false;

                    }
                    if (status == "8")
                    {
                        rblfirst.SelectedValue = "4";
                        ImageVerify.Visible = true;
                        btnsubmit.Text = "确 定";
                        btnsubmit.Visible = false;

                        //hlSelect1.Visible = false;
                        txt_first.Enabled = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;

                        hlSelect2.Visible = false;
                        txt_second.Enabled = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;

                        hlSelect3.Visible = false;
                        txt_third.Enabled = false;
                        rblthird.Enabled = false;
                        third_opinion.Enabled = false;

                    }
                }
                if (level == "2")
                {
                    if (status == "2")
                    {

                        //hlSelect1.Visible = false;
                        txt_first.Enabled = false;
                        rblfirst.Enabled = true;
                        first_opinion.Enabled = true;

                        hlSelect2.Visible = true;
                        txt_second.Enabled = true;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;

                        hlSelect3.Visible = false;
                        txt_third.Enabled = false;
                        rblthird.Enabled = false;
                        third_opinion.Enabled = false;

                    }
                    if (status == "3")
                    {
                        rblfirst.SelectedValue = "3";
                        btnsubmit.Visible = false;

                        //hlSelect1.Visible = false;
                        txt_first.Enabled = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;

                        hlSelect2.Visible = false;
                        txt_second.Enabled = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;

                        hlSelect3.Visible = false;
                        txt_third.Enabled = false;
                        rblthird.Enabled = false;
                        third_opinion.Enabled = false;

                    }
                    if (status == "4")
                    {
                        rblfirst.SelectedValue = "4";

                        //hlSelect1.Visible = false;
                        txt_first.Enabled = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;

                        hlSelect2.Visible = false;
                        txt_second.Enabled = false;
                        rblsecond.Enabled = true;
                        second_opinion.Enabled = true;

                        hlSelect3.Visible = false;
                        txt_third.Enabled = false;
                        rblthird.Enabled = false;
                        third_opinion.Enabled = false;

                    }
                    if (status == "5")
                    {
                        rblfirst.SelectedValue = "4";
                        rblsecond.SelectedValue = "5";
                        btnsubmit.Visible = false;

                        //hlSelect1.Visible = false;
                        txt_first.Enabled = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;

                        hlSelect2.Visible = false;
                        txt_second.Enabled = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;

                        hlSelect3.Visible = false;
                        txt_third.Enabled = false;
                        rblthird.Enabled = false;
                        third_opinion.Enabled = false;

                    }
                    if (status == "8")
                    {
                        rblfirst.SelectedValue = "4";
                        rblsecond.SelectedValue = "6";
                        ImageVerify.Visible = true;
                        btnsubmit.Text = "确 定";
                        btnsubmit.Visible = false;


                        //hlSelect1.Visible = false;
                        txt_first.Enabled = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;

                        hlSelect2.Visible = false;
                        txt_second.Enabled = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;

                        hlSelect3.Visible = false;
                        txt_third.Enabled = false;
                        rblthird.Enabled = false;
                        third_opinion.Enabled = false;

                    }
                }
                if (level == "3")
                {
                    if (status == "2")
                    {

                        //hlSelect1.Visible = false;
                        txt_first.Enabled = false;
                        rblfirst.Enabled = true;
                        first_opinion.Enabled = true;

                        hlSelect2.Visible = true;
                        txt_second.Enabled = true;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;

                        hlSelect3.Visible = false;
                        txt_third.Enabled = false;
                        rblthird.Enabled = false;
                        third_opinion.Enabled = false;

                    }
                    if (status == "3")
                    {
                        rblfirst.SelectedValue = "3";
                        btnsubmit.Visible = false;

                        //hlSelect1.Visible = false;
                        txt_first.Enabled = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;

                        hlSelect2.Visible = false;
                        txt_second.Enabled = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;

                        hlSelect3.Visible = false;
                        txt_third.Enabled = false;
                        rblthird.Enabled = false;
                        third_opinion.Enabled = false;
                    }
                    if (status == "4")
                    {
                        rblfirst.SelectedValue = "4";

                        //hlSelect1.Visible = false;
                        txt_first.Enabled = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;

                        hlSelect2.Visible = false;
                        txt_second.Enabled = false;
                        rblsecond.Enabled = true;
                        second_opinion.Enabled = true;

                        hlSelect3.Visible = true;
                        txt_third.Enabled = true;
                        rblthird.Enabled = false;
                        third_opinion.Enabled = false;
                    }
                    if (status == "5")
                    {
                        rblfirst.SelectedValue = "4";
                        rblsecond.SelectedValue = "5";
                        btnsubmit.Visible = false;

                        //hlSelect1.Visible = false;
                        txt_first.Enabled = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;

                        hlSelect2.Visible = false;
                        txt_second.Enabled = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;

                        hlSelect3.Visible = false;
                        txt_third.Enabled = false;
                        rblthird.Enabled = false;
                        third_opinion.Enabled = false;

                    }
                    if (status == "6")
                    {
                        rblfirst.SelectedValue = "4";
                        rblsecond.SelectedValue = "6";

                        //hlSelect1.Visible = false;
                        rblfirst.Enabled = false;
                        txt_first.Enabled = false;
                        first_opinion.Enabled = false;

                        hlSelect2.Visible = false;
                        txt_second.Enabled = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;

                        hlSelect3.Visible = false;
                        txt_third.Enabled = false;
                        rblthird.Enabled = true;
                        third_opinion.Enabled = true;
                    }
                    if (status == "7")
                    {
                        rblfirst.SelectedValue = "4";
                        rblsecond.SelectedValue = "6";
                        rblthird.SelectedValue = "7";
                        btnsubmit.Visible = false;

                        //hlSelect1.Visible = false;
                        txt_first.Enabled = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;

                        hlSelect2.Visible = false;
                        txt_second.Enabled = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;

                        hlSelect3.Visible = false;
                        txt_third.Enabled = false;
                        rblthird.Enabled = false;
                        third_opinion.Enabled = false;

                    }
                    if (status == "8")
                    {
                        ImageVerify.Visible = true;
                        rblfirst.SelectedValue = "4";
                        rblsecond.SelectedValue = "6";
                        rblthird.SelectedValue = "8";
                        btnsubmit.Visible = false;
                        btnsubmit.Text = "确定";

                        //hlSelect1.Visible = false;
                        txt_first.Enabled = false;
                        rblfirst.Enabled = false;
                        first_opinion.Enabled = false;

                        hlSelect2.Visible = false;
                        txt_second.Enabled = false;
                        rblsecond.Enabled = false;
                        second_opinion.Enabled = false;

                        hlSelect3.Visible = false;
                        txt_third.Enabled = false;
                        rblthird.Enabled = false;
                        third_opinion.Enabled = false;

                    }
                }
            }
            else
            {
                rblSHJS.Enabled = true;


                txt_first.Text = "";
                //hlSelect1.Enabled = true;
                first_opinion.Enabled = false;
                rblfirst.Enabled = false;

                txt_second.Text = "";
                hlSelect2.Visible = false;
                second_opinion.Enabled = false;
                rblsecond.Enabled = false;

                txt_third.Text = "";
                hlSelect3.Visible = false;
                third_opinion.Enabled = false;
                rblthird.Enabled = false;
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {           

            if (this.AuditPowerCheck())
            {
                status = (string)ViewState["status"];
                level = (string)ViewState["level"];
                sqlText = "select count(*) from TBPM_PAINTSCHEME where PS_ID='" + ps_no.Text + "' AND PS_STATE='1'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                int num = 0;
                if (dr.Read())
                {
                    num = Convert.ToInt16(dr[0].ToString());
                }
                dr.Close();

                #region 编制人提交
                if (num > 0) // 未提交的数据进行提交
                {
                    if (txt_first.Text == "")
                    {
                        Response.Write("<script language=javascript>alert('请选择下一审核人！');history.go(-1);</script>");
                        return;
                    }
                    else
                    {
                        //总信息为提交
                        sqlText = "update TBPM_PAINTSCHEME set PS_SUBMITTM='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        sqlText += "PS_CHECKLEVEL='" + rblSHJS.SelectedValue + "',";
                        sqlText += "PS_STATE='2' where PS_ID='" + ps_no.Text + "' and PS_STATE='1'";
                        DBCallCommon.ExeSqlText(sqlText);

                        sqlText = "update TBPM_PAINTSCHEMELIST SET PS_STATE = '1'"; //1为保存，2为提交，3为审核中，4为驳回，5为通过
                        sqlText += " where PS_PID='" + ps_no.Text + "'";
                        DBCallCommon.ExeSqlText(sqlText);

                        #region 提交发送邮件(编制人提交)
                        if (ckbMessage.Checked)
                        {
                            _emailto = DBCallCommon.GetEmailAddressByUserID(firstid.Value.Trim());
                            _body = "油漆方案审批任务:"
                                + "\r\n编    号：" + ps_no.Text.Trim()
                                + "\r\n项目名称：" + pro_name.Text.Trim() + "_" + proid.Value.Trim() + ""
                                + "\r\n设备名称：" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0]
                                + "\r\n技 术 员: " + editor.Text.Trim()
                                + "\r\n编制时间:" + plan_date.Text.Trim();


                            _subject = "您有新的【油漆方案】需要审批，请及时处理 " + pro_name.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0];
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                        #endregion

                        Response.Write("<script>alert('油漆方案已提交，请等待审核...');location.href='TM_Mytast_List.aspx';</script>");
                    }
                }
                else if (Request.QueryString["rejectid"] != null)//驳回数据的提交
                {
                    if (firstid.Value == "")
                    {
                        Response.Write("<script language=javascript>alert('请选择下一审核人！');history.go(-1);</script>");
                        return;
                    }
                    else
                    {
                        //总信息为提交
                        sqlText = "update TBPM_PAINTSCHEME set PS_SUBMITTM='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                        sqlText += "PS_REVIEWA='" + firstid.Value + "',PS_CHECKLEVEL='" + rblSHJS.SelectedValue + "',";
                        sqlText += "PS_STATE='2' where PS_ID='" + ps_no.Text + "' and PS_STATE in('3','5','7')";
                        DBCallCommon.ExeSqlText(sqlText);

                        sqlText = "update TBPM_PAINTSCHEMELIST SET PS_STATE = '1'"; //1为保存，2为提交，3为审核中，4为驳回，5为通过
                        sqlText += " where PS_PID='" + ps_no.Text + "'";
                        DBCallCommon.ExeSqlText(sqlText);

                        #region 提交发送邮件(编制人提交)
                        if (ckbMessage.Checked)
                        {
                            _emailto = DBCallCommon.GetEmailAddressByUserID(firstid.Value.Trim());
                            _body = "油漆方案审批任务:"
                                + "\r\n编    号：" + ps_no.Text.Trim()
                                + "\r\n项目名称：" + pro_name.Text.Trim() + "_" + proid.Value.Trim() + ""
                                + "\r\n设备名称：" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0]
                                + "\r\n技 术 员: " + editor.Text.Trim()
                                + "\r\n编制时间:" + plan_date.Text.Trim();


                            _subject = "您有新的【油漆方案】需要审批，请及时处理 " + pro_name.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0];
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                        #endregion

                        Response.Write("<script>alert('油漆方案已提交，请等待审核...');location.href='TM_Mytast_List.aspx';</script>");
                        return;
                    }
                }
                #endregion

                #region 审核人提交
                else// 已提交的数据进行审核，分三级
                {
                    #region  一级审核
                    if (level == "1")
                    {
                        if (status == "2")
                        {
                            if (rblfirst.SelectedValue == "")
                            {
                                Response.Write("<script language=javascript>alert('请选择审核结果！');history.go(-1);</script>");
                                return;
                            }
                            else
                            {
                                if (rblfirst.SelectedValue == "3") //一级驳回
                                {
                                    sqlText = "update TBPM_PAINTSCHEME set PS_REVIEWATIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                    sqlText += "PS_REVIEWAADVC='" + first_opinion.Text + "',";
                                    sqlText += "PS_STATE='3' where PS_ID='" + ps_no.Text + "' and PS_STATE='2'";
                                    DBCallCommon.ExeSqlText(sqlText);

                                    sqlText = "update TBPM_PAINTSCHEMELIST SET PS_STATE = '4'"; //1为保存，2为提交，3为审核中，4为驳回，5为通过
                                    sqlText += " where PS_PID='" + ps_no.Text + "' and PS_STATE='2'";
                                    DBCallCommon.ExeSqlText(sqlText);


                                    #region 审核驳回
                                    if (ckbMessage.Checked)
                                    {
                                        _emailto = DBCallCommon.GetEmailAddressByUserID(editorid.Value.Trim());
                                        _body = "【油漆方案】审批已驳回:"
                                            + "\r\n编    号：" + ps_no.Text.Trim()
                                            + "\r\n项目名称：" + pro_name.Text.Trim() + "_" + proid.Value.Trim() + ""
                                            + "\r\n设备名称：" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0]
                                            + "\r\n技 术 员: " + editor.Text.Trim()
                                            + "\r\n编制时间:" + plan_date.Text.Trim();


                                        _subject = "【油漆方案审批】已驳回 " + pro_name.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0];
                                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                                    }
                                    #endregion

                                    Response.Write("<script>alert('油漆方案驳回，审核终止!');location.href='TM_Leader_Task.aspx';</script>");
                                }
                                else if (rblfirst.SelectedValue == "4")  //一级审核通过
                                {

                                    sqlText = "update TBPM_PAINTSCHEME set PS_ADATE='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                    sqlText += "PS_REVIEWATIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                    sqlText += "PS_REVIEWAADVC='" + first_opinion.Text + "',PS_STATE='8' ";
                                    sqlText += "where PS_ID='" + ps_no.Text + "' and PS_STATE='2'";
                                    DBCallCommon.ExeSqlText(sqlText);

                                    sqlText = "update TBPM_PAINTSCHEMELIST SET PS_STATE = '5'"; //1为保存，2为提交，3为审核中，4为驳回，5为通过
                                    sqlText += " where PS_PID='" + ps_no.Text + "' and PS_STATE='2'";
                                    DBCallCommon.ExeSqlText(sqlText);

                                    #region 审核通过
                                    if (ckbMessage.Checked)
                                    {
                                        SendSuccessMail();
                                    }
                                    #endregion

                                    Response.Write("<script>alert('油漆方案审核通过!');location.href='TM_Leader_Task.aspx';</script>");
                                    return;
                                }
                            }
                        }
                    }
                    #endregion

                    #region 二级审核
                    else if (level == "2")
                    {
                        if (status == "2")
                        {
                            if (rblfirst.SelectedValue == "")
                            {
                                Response.Write("<script language=javascript>alert('请选择审核结果！');history.go(-1);</script>");
                                return;
                            }
                            else
                            {
                                if (rblfirst.SelectedValue == "3") //一级驳回
                                {

                                    sqlText = "update TBPM_PAINTSCHEME set PS_REVIEWATIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                    sqlText += "PS_REVIEWAADVC='" + first_opinion.Text + "',";
                                    sqlText += "PS_STATE='3' where PS_ID='" + ps_no.Text + "' and PS_STATE='2'";
                                    DBCallCommon.ExeSqlText(sqlText);

                                    sqlText = "update TBPM_PAINTSCHEMELIST SET PS_STATE = '4'"; //1为保存，2为提交，3为审核中，4为驳回，5为通过
                                    sqlText += " where PS_PID='" + ps_no.Text + "' and PS_STATE='2'";
                                    DBCallCommon.ExeSqlText(sqlText);
                                    #region 审核驳回
                                    if (ckbMessage.Checked)
                                    {
                                        _emailto = DBCallCommon.GetEmailAddressByUserID(editorid.Value.Trim());
                                        _body = "【油漆方案】审批已驳回:"
                                            + "\r\n编    号：" + ps_no.Text.Trim()
                                            + "\r\n项目名称：" + pro_name.Text.Trim() + "_" + proid.Value.Trim() + ""
                                            + "\r\n设备名称：" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0]
                                            + "\r\n技 术 员: " + editor.Text.Trim()
                                            + "\r\n编制时间:" + plan_date.Text.Trim();


                                        _subject = "【油漆方案审批】已驳回 " + pro_name.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0];
                                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                                    }
                                    #endregion
                                    Response.Write("<script>alert('油漆方案驳回，审核终止!');location.href='TM_Leader_Task.aspx';</script>");
                                }
                                else if (rblfirst.SelectedValue == "4") //一级通过
                                {
                                    if (secondid.Value == "")
                                    {
                                        Response.Write("<script language=javascript>alert('请选择下一审核人！');history.go(-1);</script>");
                                        return;
                                    }
                                    else
                                    {

                                        sqlText = "update TBPM_PAINTSCHEME set PS_REVIEWB='" + secondid.Value + "',";
                                        sqlText += "PS_REVIEWATIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                        sqlText += "PS_REVIEWAADVC='" + first_opinion.Text + "',PS_STATE='4' ";
                                        sqlText += "where PS_ID='" + ps_no.Text + "' and PS_STATE='2'";
                                        DBCallCommon.ExeSqlText(sqlText);

                                        sqlText = "update TBPM_PAINTSCHEMELIST SET PS_STATE = '3'"; //1为保存，2为提交，3为审核中，4为驳回，5为通过
                                        sqlText += " where PS_PID='" + ps_no.Text + "' and PS_STATE='2'";
                                        DBCallCommon.ExeSqlText(sqlText);

                                        #region 提交至第二个审核人
                                        if (ckbMessage.Checked)
                                        {
                                            _emailto = DBCallCommon.GetEmailAddressByUserID(secondid.Value.Trim());
                                            _body = "油漆方案审批任务:"
                                                + "\r\n编    号：" + ps_no.Text.Trim()
                                                + "\r\n项目名称：" + pro_name.Text.Trim() + "_" + proid.Value.Trim() + ""
                                                + "\r\n设备名称：" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0]
                                                + "\r\n技 术 员: " + editor.Text.Trim()
                                                + "\r\n编制时间:" + plan_date.Text.Trim();


                                            _subject = "您有新的【油漆方案】需要审批，请及时处理 " + pro_name.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0];
                                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                                        }
                                        #endregion

                                        Response.Write("<script>alert('油漆方案审核已提交，请等待审核...');location.href='TM_Leader_Task.aspx';</script>");
                                        return;
                                    }
                                }
                            }
                        }
                        else if (status == "4")////一级通过
                        {
                            if (rblsecond.SelectedValue == "")
                            {
                                Response.Write("<script language=javascript>alert('请选择审核结果！');history.go(-1);</script>");
                                return;
                            }
                            else
                            {
                                if (rblsecond.SelectedValue == "5")//二级驳回
                                {


                                    sqlText = "update TBPM_PAINTSCHEME set ";
                                    sqlText += "PS_REVIEWBTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                    sqlText += "PS_REVIEWBADVC='" + second_opinion.Text + "',PS_STATE='5' ";
                                    sqlText += "where PS_ID='" + ps_no.Text + "' and PS_STATE='4'";
                                    DBCallCommon.ExeSqlText(sqlText);

                                    sqlText = "update TBPM_PAINTSCHEMELIST SET PS_STATE = '4'"; //1为保存，2为提交，3为审核中，4为驳回，5为通过
                                    sqlText += " where PS_PID='" + ps_no.Text + "' and PS_STATE='3'";
                                    DBCallCommon.ExeSqlText(sqlText);
                                    #region 审核驳回
                                    if (ckbMessage.Checked)
                                    {
                                        _emailto = DBCallCommon.GetEmailAddressByUserID(editorid.Value.Trim());
                                        _body = "【油漆方案】审批已驳回:"
                                            + "\r\n编    号：" + ps_no.Text.Trim()
                                            + "\r\n项目名称：" + pro_name.Text.Trim() + "_" + proid.Value.Trim() + ""
                                            + "\r\n设备名称：" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0]
                                            + "\r\n技 术 员: " + editor.Text.Trim()
                                            + "\r\n编制时间:" + plan_date.Text.Trim();


                                        _subject = "【油漆方案审批】已驳回 " + pro_name.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0];
                                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                                    }
                                    #endregion
                                    Response.Write("<script>alert('审批驳回，审核终止!');location.href='TM_Leader_Task.aspx';</script>");
                                }
                                else if (rblsecond.SelectedValue == "6")//二级通过
                                {

                                    sqlText = "update TBPM_PAINTSCHEME set PS_ADATE='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                    sqlText += "PS_REVIEWBTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                    sqlText += "PS_REVIEWBADVC='" + second_opinion.Text + "',PS_STATE='8' ";
                                    sqlText += "where PS_ID='" + ps_no.Text + "' and PS_STATE='4'";
                                    DBCallCommon.ExeSqlText(sqlText);

                                    sqlText = "update TBPM_PAINTSCHEMELIST SET PS_STATE = '5'"; //1为保存，2为提交，3为审核中，4为驳回，5为通过
                                    sqlText += " where PS_PID='" + ps_no.Text + "' and PS_STATE='3'";
                                    DBCallCommon.ExeSqlText(sqlText);
                                    #region 审核通过
                                    if (ckbMessage.Checked)
                                    {
                                        SendSuccessMail();
                                    }
                                    #endregion
                                    Response.Write("<script>alert('审核通过!');location.href='TM_Leader_Task.aspx';</script>");
                                    return;
                                }
                            }
                        }
                    }
                    #endregion

                    #region 三级审核
                    if (level == "3")
                    {
                        if (status == "2")
                        {
                            if (rblfirst.SelectedValue == "")
                            {
                                Response.Write("<script language=javascript>alert('请选择审核结果！');history.go(-1);</script>");
                                return;
                            }
                            else
                            {
                                if (rblfirst.SelectedValue == "3")
                                {
                                    sqlText = "update TBPM_PAINTSCHEME set PS_REVIEWATIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                    sqlText += "PS_REVIEWAADVC='" + first_opinion.Text + "',";
                                    sqlText += "PS_STATE='3' where PS_ID='" + ps_no.Text + "' and PS_STATE='2'";
                                    DBCallCommon.ExeSqlText(sqlText);

                                    sqlText = "update TBPM_PAINTSCHEMELIST SET PS_STATE = '4'"; //1为保存，2为提交，3为审核中，4为驳回，5为通过
                                    sqlText += " where PS_PID='" + ps_no.Text + "' and PS_STATE='2'";
                                    DBCallCommon.ExeSqlText(sqlText);

                                    #region 审核驳回
                                    if (ckbMessage.Checked)
                                    {
                                        _emailto = DBCallCommon.GetEmailAddressByUserID(editorid.Value.Trim());
                                        _body = "【油漆方案】审批已驳回:"
                                            + "\r\n编    号：" + ps_no.Text.Trim()
                                            + "\r\n项目名称：" + pro_name.Text.Trim() + "_" + proid.Value.Trim() + ""
                                            + "\r\n设备名称：" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0]
                                            + "\r\n技 术 员: " + editor.Text.Trim()
                                            + "\r\n编制时间:" + plan_date.Text.Trim();


                                        _subject = "【油漆方案审批】已驳回 " + pro_name.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0];
                                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                                    }
                                    #endregion

                                    Response.Write("<script>alert('审批驳回，审核终止!');location.href='TM_Leader_Task.aspx';</script>");
                                }
                                else if (rblfirst.SelectedValue == "4")
                                {
                                    if (secondid.Value == "")
                                    {
                                        Response.Write("<script language=javascript>alert('请选择下一审核人！');history.go(-1);</script>");
                                        return;
                                    }
                                    else
                                    {
                                        sqlText = "update TBPM_PAINTSCHEME set PS_REVIEWB='" + secondid.Value + "',";
                                        sqlText += "PS_REVIEWATIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                        sqlText += "PS_REVIEWAADVC='" + first_opinion.Text + "',PS_STATE='4' ";
                                        sqlText += "where PS_ID='" + ps_no.Text + "' and PS_STATE='2'";
                                        DBCallCommon.ExeSqlText(sqlText);

                                        sqlText = "update TBPM_PAINTSCHEMELIST SET PS_STATE = '3'"; //1为保存，2为提交，3为审核中，4为驳回，5为通过
                                        sqlText += " where PS_PID='" + ps_no.Text + "' and PS_STATE='2'";
                                        DBCallCommon.ExeSqlText(sqlText);

                                        #region 提交至第二个审核人
                                        if (ckbMessage.Checked)
                                        {
                                            _emailto = DBCallCommon.GetEmailAddressByUserID(secondid.Value.Trim());
                                            _body = "油漆方案审批任务:"
                                                + "\r\n编    号：" + ps_no.Text.Trim()
                                                + "\r\n项目名称：" + pro_name.Text.Trim() + "_" + proid.Value.Trim() + ""
                                                + "\r\n设备名称：" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0]
                                                + "\r\n技 术 员: " + editor.Text.Trim()
                                                + "\r\n编制时间:" + plan_date.Text.Trim();


                                            _subject = "您有新的【油漆方案】需要审批，请及时处理 " + pro_name.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0];
                                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                                        }
                                        #endregion

                                        Response.Write("<script>alert('油漆方案已提交，请等待审核...');location.href='TM_Leader_Task.aspx';</script>");
                                        return;
                                    }
                                }
                            }
                        }
                        else if (status == "4")
                        {
                            if (rblsecond.SelectedValue == "")
                            {
                                Response.Write("<script language=javascript>alert('请选择审核结果！');history.go(-1);</script>");
                                return;
                            }
                            else
                            {
                                if (rblsecond.SelectedValue == "5")
                                {
                                    sqlText = "update TBPM_PAINTSCHEME set ";
                                    sqlText += "PS_REVIEWBTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                    sqlText += "PS_REVIEWBADVC='" + second_opinion.Text + "',PS_STATE='5' ";
                                    sqlText += "where PS_ID='" + ps_no.Text + "' and PS_STATE='4'";
                                    DBCallCommon.ExeSqlText(sqlText);

                                    sqlText = "update TBPM_PAINTSCHEMELIST SET PS_STATE = '4'"; //1为保存，2为提交，3为审核中，4为驳回，5为通过
                                    sqlText += " where PS_PID='" + ps_no.Text + "' and PS_STATE='3'";
                                    DBCallCommon.ExeSqlText(sqlText);

                                    #region 审核驳回
                                    if (ckbMessage.Checked)
                                    {
                                        _emailto = DBCallCommon.GetEmailAddressByUserID(editorid.Value.Trim());
                                        _body = "【油漆方案】审批已驳回:"
                                            + "\r\n编    号：" + ps_no.Text.Trim()
                                            + "\r\n项目名称：" + pro_name.Text.Trim() + "_" + proid.Value.Trim() + ""
                                            + "\r\n设备名称：" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0]
                                            + "\r\n技 术 员: " + editor.Text.Trim()
                                            + "\r\n编制时间:" + plan_date.Text.Trim();


                                        _subject = "【油漆方案审批】已驳回 " + pro_name.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0];
                                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                                    }
                                    #endregion

                                    Response.Write("<script>alert('油漆方案驳回，审核终止!');location.href='TM_Leader_Task.aspx';</script>");
                                    return;
                                }
                                else if (rblsecond.SelectedValue == "6")
                                {
                                    if (thirdid.Value == "")
                                    {
                                        Response.Write("<script language=javascript>alert('请选择下一审核人！');history.go(-1);</script>");
                                        return;
                                    }
                                    else
                                    {

                                        sqlText = "update TBPM_PAINTSCHEME set PS_REVIEWC='" + thirdid.Value + "', ";
                                        sqlText += "PS_REVIEWBTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                        sqlText += "PS_REVIEWBADVC='" + second_opinion.Text + "',PS_STATE='6' ";
                                        sqlText += "where PS_ID='" + ps_no.Text + "' and PS_STATE='4'";
                                        DBCallCommon.ExeSqlText(sqlText);

                                        #region 提交至第三个审核人
                                        if (ckbMessage.Checked)
                                        {
                                            _emailto = DBCallCommon.GetEmailAddressByUserID(thirdid.Value.Trim());
                                            _body = "油漆方案审批任务:"
                                                + "\r\n编    号：" + ps_no.Text.Trim()
                                                + "\r\n项目名称：" + pro_name.Text.Trim() + "_" + proid.Value.Trim() + ""
                                                + "\r\n设备名称：" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0]
                                                + "\r\n技 术 员: " + editor.Text.Trim()
                                                + "\r\n编制时间:" + plan_date.Text.Trim();

                                            _subject = "您有新的【油漆方案】需要审批，请及时处理 " + pro_name.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0];
                                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                                        }
                                        #endregion

                                        Response.Write("<script>alert('油漆方案审核已提交，请等待审核...');location.href='TM_Leader_Task.aspx';</script>");
                                        return;
                                    }

                                }
                            }
                        }
                        else if (status == "6")
                        {
                            if (rblthird.SelectedValue == "")
                            {
                                Response.Write("<script language=javascript>alert('请选择审核结果！');history.go(-1);</script>");
                                return;
                            }
                            else
                            {
                                if (rblthird.SelectedValue == "7")
                                {

                                    sqlText = "update TBPM_PAINTSCHEME set ";
                                    sqlText += "PS_REVIEWCTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                    sqlText += "PS_REVIEWCADVC='" + third_opinion.Text + "',PS_STATE='7' ";
                                    sqlText += "where PS_ID='" + ps_no.Text + "' and PS_STATE='6'";
                                    DBCallCommon.ExeSqlText(sqlText);

                                    sqlText = "update TBPM_PAINTSCHEMELIST SET PS_STATE = '4'"; //1为保存，2为提交，3为审核中，4为驳回，5为通过
                                    sqlText += " where PS_PID='" + ps_no.Text + "' and PS_STATE='3'";
                                    DBCallCommon.ExeSqlText(sqlText);

                                    #region 审核驳回
                                    if (ckbMessage.Checked)
                                    {
                                        _emailto = DBCallCommon.GetEmailAddressByUserID(editorid.Value.Trim());
                                        _body = "【油漆方案】审批已驳回:"
                                            + "\r\n编    号：" + ps_no.Text.Trim()
                                            + "\r\n项目名称：" + pro_name.Text.Trim() + "_" + proid.Value.Trim() + ""
                                            + "\r\n设备名称：" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0]
                                            + "\r\n技 术 员: " + editor.Text.Trim()
                                            + "\r\n编制时间:" + plan_date.Text.Trim();


                                        _subject = "【油漆方案审批】已驳回 " + pro_name.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0];
                                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                                    }
                                    #endregion

                                    Response.Write("<script>alert('油漆方案驳回，审核终止!');location.href='TM_Leader_Task.aspx';</script>");
                                    return;
                                }
                                else if (rblthird.SelectedValue == "8")
                                {

                                    sqlText = "update TBPM_PAINTSCHEME set PS_ADATE='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                    sqlText += "PS_REVIEWCTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',";
                                    sqlText += "PS_REVIEWCADVC='" + third_opinion.Text + "',PS_STATE='8' ";
                                    sqlText += "where PS_ID='" + ps_no.Text + "' and PS_STATE='6'";
                                    DBCallCommon.ExeSqlText(sqlText);

                                    sqlText = "update TBPM_PAINTSCHEMELIST SET PS_STATE = '5'"; //1为保存，2为提交，3为审核中，4为驳回，5为通过
                                    sqlText += " where PS_PID='" + ps_no.Text + "' and PS_STATE='3'";
                                    DBCallCommon.ExeSqlText(sqlText);
                                    #region 审核通过
                                    if (ckbMessage.Checked)
                                    {
                                        SendSuccessMail();
                                    }
                                    #endregion
                                    Response.Write("<script>alert('油漆方案审核通过!');location.href='TM_Leader_Task.aspx';</script>");
                                    return;

                                }
                            }
                        }
                    }
                    #endregion
                }
                #endregion
            }
            else
            {
                Response.Write("<script>alert('您已完成审核或无权审核该任务！！！')</script>");
            }
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }


        protected void Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string bgBeiZhu = ((Label)e.Item.FindControl("lblBGBeiZhu")).Text;
                if (bgBeiZhu != "")
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "#00E600";
                }
            }
        }

        protected void SendSuccessMail()
        {
            SendSuccessMailToEditor();
            SendSuccessMailToQA();
        }

        protected void SendSuccessMailToEditor()
        {
            _emailto = DBCallCommon.GetEmailAddressByUserID(editorid.Value.Trim());
            _body = "油漆方案审批已通过:"
                + "\r\n编    号：" + ps_no.Text.Trim()
                + "\r\n项目名称：" + pro_name.Text.Trim() + "_" + proid.Value.Trim() + ""
                + "\r\n设备名称：" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0]
                + "\r\n技 术 员: " + editor.Text.Trim()
                + "\r\n编制时间:" + plan_date.Text.Trim();

            _subject = "【油漆方案】已通过审批 " + pro_name.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0];
            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
        }

        protected void SendSuccessMailToQA()
        {
            //抄送人
            List<string> cc = new List<string>();
            //获取质量部主管与质量部主管助理的邮箱
            StringBuilder sb = new StringBuilder();            
            sb.Append("SELECT TOP 1 T.EMAIL FROM TBQM_SetInspectPerson S INNER JOIN dbo.TBDS_STAFFINFO T ON s.InspectPerson=t.ST_ID WHERE num like '%油漆%';");
            sb.Append("SELECT [EMAIL] FROM [dbo].[TBDS_STAFFINFO] WHERE [ST_POSITION] IN ('1201','1205');");
            SqlDataReader dr_email = DBCallCommon.GetDRUsingSqlText(sb.ToString());
            if (dr_email.HasRows)
            {
                dr_email.Read();
                cc.Add(dr_email["EMAIL"].ToString());
            }
            dr_email.Close();
            _emailto = cc[0];

            _body = "有新的【油漆方案变更】，请及时查看:"
                + "\r\n编    号：" + ps_no.Text.Trim()
                + "\r\n项目名称：" + pro_name.Text.Trim() + "_" + proid.Value.Trim() + ""
                + "\r\n设备名称：" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0]
                + "\r\n技 术 员: " + editor.Text.Trim()
                + "\r\n编制时间:" + plan_date.Text.Trim();

            _subject = "【油漆方案变更】" + pro_name.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + eng_name.Text.Trim() + "_" + tsaid.Text.Trim().Split('-')[0];
            DBCallCommon.SendEmail(_emailto, cc, null, _subject, _body);
        }
    }
}
