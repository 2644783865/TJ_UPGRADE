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
using AjaxControlToolkit;
using System.Drawing;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MS_Detail_Audit : System.Web.UI.Page
    {
        string sqlText;
        string status;
        protected static string ms_id;
        string tablename;
        string viewtable;
        string strtable = "TBPM_STRINFODQO";
        string[] fields;
        string[] cols;
        string mstable;
        string view_mstable = "View_TM_MSFORALLRVW";
        protected static string bgmstablerv = "View_TM_MKDETAIL";
        string tstable = "TBMP_TASKDQO";
        string _emailto;
        string _body;
        string _subject;
        protected void Page_Load(object sender, EventArgs e)
        {
            Intvar();
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            if (!IsPostBack)
            {
                GetMSVerifyData();
                //hpView.NavigateUrl = "TM_MS_Old_View.aspx?action=" + ms_no.Text + "&ViewOrg=" + view_orgtable + "&Engid=" + tsa_id.Text.Trim();
                InitList();
                CalWeight();
            }
            if (IsPostBack)
            {
                InitList();
                //this.InitVarMS();
            }
        }

        private void CalWeight()
        {

            sqlText = "select sum(MS_TUTOTALWGHT),sum(cast(MS_MATOTALWGHT as float)) from " + tablename + " where MS_PID='" + ms_no.Text + "' and  MS_MARID<>'' and MS_MASHAPE in ('板','型','圆')";
            try
            {
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                lblMpUsagePercent.Text = (Math.Round((Convert.ToDouble(dt.Rows[0][0]) / Convert.ToDouble(dt.Rows[0][1])) * 100, 2)).ToString() + "%";
            }
            }
            catch (Exception)
            {
                lblMpUsagePercent.Text="";
            }
        }

        private void Intvar()  //得到制作明细的批号
        {
            if (Request.QueryString["id"] != null)
            {
                ms_id = Request.QueryString["id"];
            }
            else
            {
                ms_id = Request.QueryString["ms_audit_id"];
            }
        }
        //初始化表名
        private void InitList()
        {
            fields = ms_id.Split('.');
            cols = fields[1].ToString().Split('/');
            if (cols[0].ToString().Length > 6)  //变更
            {
                viewtable = "View_TM_MSCHANGE";
                mstable = "TBPM_MSCHANGERVW";
                tablename = "TBPM_MSCHANGE";

            }
            else
            {
                mstable = "TBPM_MSFORALLRVW";
                viewtable = "View_TM_MKDETAIL";
                tablename = "TBPM_MKDETAIL";
            }
            ViewState["mstable"] = tablename;
            ViewState["view_mstable"] = viewtable;
        }


        /// <summary>
        /// 审核权限检查
        /// </summary>
        /// <returns></returns>
        private bool AuditPowerCheck()
        {
            bool retVal = true;
            if (Request.QueryString["ms_audit_id"] != null)
            {
                if (lblStatus.Text == "2")
                {
                    if (Session["UserID"].ToString() != firstid.Value.Trim())
                    {
                        retVal = false;
                    }
                }
                else if (lblStatus.Text == "4")
                {
                    if (Session["UserID"].ToString() != secondid.Value.Trim())
                    {
                        retVal = false;
                    }
                }
                else if (lblStatus.Text == "6")
                {
                    if (Session["UserID"].ToString() != thirdid.Value.Trim())
                    {
                        retVal = false;
                    }
                }
            }
            return retVal;
        }
        private void GetMSVerifyData()
        {
            btnsubmit.Text = "提 交";
            fields = ms_id.Split('.');
            cols = fields[1].ToString().Split('/');
            if (cols[0].ToString().Length > 6)  //变更
            {
                view_mstable = "View_TM_MSCHANGERVW";
              //  GridView1.Columns[2].Visible = true;//查看变更记录
             //   GridView1.Columns[3].Visible = true;
                //hpView.Visible = true;
            }
            else
            {
                view_mstable = "View_TM_MSFORALLRVW";
                //增加的
             //   GridView1.Columns[2].Visible = false;//查看变更记录
             //   GridView1.Columns[3].Visible = false;//查看变更记录
                //hpView.Visible = false;
            }
            string level = "";
            sqlText = "select MS_ID,MS_PJNAME,MS_ENGNAME,MS_ENGTYPE,MS_SUBMITNM,";
            sqlText += "MS_SUBMITTM,MS_REVIEWANAME,MS_REVIEWAADVC,MS_REVIEWATIME,";
            sqlText += "MS_REVIEWBNAME,MS_REVIEWBADVC,MS_REVIEWBTIME,MS_REVIEWCNAME,";
            sqlText += "MS_REVIEWCADVC,MS_REVIEWCTIME,MS_ADATE,MS_STATE,MS_ENGID,MS_PJID,";
            sqlText += " MS_SUBMITID,MS_REVIEWA,MS_CHECKLEVEL,MS_NOTE,MS_REVIEWB,MS_REVIEWC,MS_MAP  from " + view_mstable + " where MS_ID='" + ms_id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                #region 页面初始化
                ms_no.Text = dr[0].ToString();
                lab_project.Text = dr[1].ToString();
                lab_contract.Text = dr["MS_PJID"].ToString();
                proname.Text = dr[1].ToString();
                lab_engname.Text = dr[2].ToString();
                engname.Text = dr[2].ToString();
                eng_type.Value = dr[3].ToString();
                txt_editor.Text = dr[4].ToString();
                txt_plandate.Text = dr[5].ToString();
                plandate.Text = dr[5].ToString();
                txt_first.Text = dr[6].ToString();
                first_opinion.Text = dr[7].ToString();
                first_time.Text = dr[8].ToString();
                txt_second.Text = dr[9].ToString();
                second_opinion.Text = dr[10].ToString();
                second_time.Text = dr[11].ToString();
                txt_third.Text = dr[12].ToString();
                third_opinion.Text = dr[13].ToString();
                third_time.Text = dr[14].ToString();
                txt_approval.Text = dr[15].ToString();
                status = dr[16].ToString();
                lblStatus.Text = status;
                tsa_id.Text = dr[17].ToString();
                ViewState["taskid"] = dr[17].ToString();
                pro_id.Value = dr[18].ToString();
                editorid.Value = dr["MS_SUBMITID"].ToString();
                firstid.Value = dr["MS_REVIEWA"].ToString();
                secondid.Value = dr["MS_REVIEWB"].ToString();
                thirdid.Value = dr["MS_REVIEWC"].ToString();
              //  txtBZ.Text = dr["MS_NOTE"].ToString();
                lblTuhao.Text = dr["MS_MAP"].ToString();
                //审核图片
                if (status == "3" || status == "5" || status == "7" || status == "8")
                {
                    ImageVerify.Visible = true;
                }
                else
                {
                    ImageVerify.Visible = false;
                }
                //批注保存按钮
                if ((status == "2" && firstid.Value.Trim() == Session["UserID"].ToString()) || (status == "4" && secondid.Value.Trim() == Session["UserID"].ToString()) || (status == "6" && secondid.Value.Trim() == Session["UserID"].ToString()))
                {
                }
                else
                {
                  //  btnPiZhuEdit.Disabled = true;
                }

                //if (cols[0].ToString().Length > 6)  //变更
                //{
                //    if (status != "8" && editorid.Value == Session["UserID"].ToString())
                //    {
                //        btnBianGengBZ.Enabled = true;
                //    }
                //}

                this.RblOfThreeStateConfirm(status, dr["MS_CHECKLEVEL"].ToString()); //确定每个审核意见的值
                level = dr["MS_CHECKLEVEL"].ToString();//审核级数
                rblSHJS.SelectedValue = level;
                
                #endregion
                #region  可用状态判断
                if (Request.QueryString["id"] != null)  //查看进入(针对提交人)
                {
                    if (status == "1" && level == "3")//未指定审核人
                    {
                        rblSHJS.SelectedIndex = 1; //默认二级级审核
                        plandate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        txt_plandate.Text = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");

                        //  hlSelect1.Visible = true;
                        first_opinion.Enabled = false;
                        rblfirst.Enabled = false;
                        txt_first.Enabled = false;

                        hlSelect2.Visible = false;
                        second_opinion.Enabled = false;
                        rblsecond.Enabled = false;
                        txt_second.Enabled = false;

                        hlSelect3.Visible = false;
                        third_opinion.Enabled = false;
                        rblthird.Enabled = false;
                        txt_third.Enabled = false;

                        //txtBZ.Enabled = true;//备注
                        //txtBZ.BorderStyle = BorderStyle.Solid;

                    }
                    else  //指定审核人，提交人查看
                    {
                       // txtBZ.ReadOnly = true;//备注
                       // txtBZ.BorderStyle = BorderStyle.None;

                        rblSHJS.Enabled = false;
                        btnsubmit.Visible = false;

                        hlSelect1.Visible = false;
                        first_opinion.Enabled = false;
                        rblfirst.Enabled = false;
                        txt_first.Enabled = false;

                        hlSelect2.Visible = false;
                        second_opinion.Enabled = false;
                        rblsecond.Enabled = false;
                        txt_second.Enabled = false;

                        hlSelect3.Visible = false;
                        third_opinion.Enabled = false;
                        rblthird.Enabled = false;
                        txt_third.Enabled = false;
                    }
                }

                if (Request.QueryString["ms_audit_id"] != null)//审核人进入
                {

                    rblSHJS.Enabled = false;
                    //txtBZ.ReadOnly = true;//备注
                   // txtBZ.BorderStyle = BorderStyle.None;

                    if (status == "2")//一级审核
                    {
                        if (level == "1")
                        {
                            hlSelect1.Visible = false;
                            first_opinion.Enabled = true;
                            rblfirst.Enabled = true;
                            txt_first.Enabled = false;

                            hlSelect2.Visible = false;
                            second_opinion.Enabled = false;
                            rblsecond.Enabled = false;
                            txt_second.Enabled = false;

                            hlSelect3.Visible = false;
                            third_opinion.Enabled = false;
                            rblthird.Enabled = false;
                            txt_third.Enabled = false;
                        }
                        else
                        {
                            hlSelect1.Visible = false;
                            first_opinion.Enabled = true;
                            rblfirst.Enabled = true;
                            txt_first.Enabled = false;

                            hlSelect2.Visible = true;
                            second_opinion.Enabled = false;
                            rblsecond.Enabled = false;
                            txt_second.Enabled = false;

                            hlSelect3.Visible = false;
                            third_opinion.Enabled = false;
                            rblthird.Enabled = false;
                            txt_third.Enabled = false;
                        }
                    }
                    else if (status == "4")//一级审核通过，二级审核
                    {
                        if (level == "2")
                        {
                            hlSelect1.Visible = false;
                            first_opinion.Enabled = false;
                            rblfirst.Enabled = false;
                            txt_first.Enabled = false;

                            hlSelect2.Visible = false;
                            second_opinion.Enabled = true;
                            rblsecond.Enabled = true;
                            txt_second.Enabled = false;

                            hlSelect3.Visible = false;
                            third_opinion.Enabled = false;
                            rblthird.Enabled = false;
                            txt_third.Enabled = false;
                        }
                        else
                        {
                            hlSelect1.Visible = false;
                            first_opinion.Enabled = false;
                            rblfirst.Enabled = false;
                            txt_first.Enabled = false;

                            hlSelect2.Visible = false;
                            second_opinion.Enabled = true;
                            rblsecond.Enabled = true;
                            txt_second.Enabled = false;

                            hlSelect3.Visible = true;
                            third_opinion.Enabled = false;
                            rblthird.Enabled = false;
                            txt_third.Enabled = false;
                        }
                    }
                    else if (status == "6")//二级审核通过，三级审核
                    {
                        hlSelect1.Visible = false;
                        first_opinion.Enabled = false;
                        rblfirst.Enabled = false;
                        txt_first.Enabled = false;

                        hlSelect2.Visible = false;
                        second_opinion.Enabled = false;
                        rblsecond.Enabled = false;
                        txt_second.Enabled = false;

                        hlSelect3.Visible = false;
                        third_opinion.Enabled = true;
                        rblthird.Enabled = true;
                        txt_third.Enabled = false;
                    }
                    else
                    {
                        btnsubmit.Visible = false;

                        hlSelect1.Visible = false;
                        first_opinion.Enabled = false;
                        rblfirst.Enabled = false;
                        txt_first.Enabled = false;

                        hlSelect2.Visible = false;
                        second_opinion.Enabled = false;
                        rblsecond.Enabled = false;
                        txt_second.Enabled = false;

                        hlSelect3.Visible = false;
                        third_opinion.Enabled = false;
                        rblthird.Enabled = false;
                        txt_third.Enabled = false;
                    }
                }
                #endregion
            }
            dr.Close();

          

            //绑定数据
            ViewState["sqlText"] = "MS_PID='" + ms_id + "'";
         //   UCPagingMS.CurrentPage = 1;
            //this.InitVarMS();
            //this.bindGridMS();
        }
        /// <summary>
        /// 用于前台绑定
        /// </summary>
        /// <returns></returns>
        public string view_table()
        {
            return bgmstablerv;
        }
        /// <summary>
        /// 读取RadioButtonList状态
        /// </summary>
        /// <param name="state"></param>
        private void RblOfThreeStateConfirm(string state, string level)
        {
            if (level != "0")
            {
                switch (state)
                {
                    case "3":
                        rblfirst.SelectedIndex = 1;
                        rblsecond.SelectedIndex = -1;
                        rblthird.SelectedIndex = -1;
                        break;
                    case "4":
                        rblfirst.SelectedIndex = 0;
                        break;
                    case "5":
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = 1;
                        rblthird.SelectedIndex = -1;
                        break;
                    case "6":
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = 0;
                        break;
                    case "7":
                        rblfirst.SelectedIndex = 0;
                        rblsecond.SelectedIndex = 0;
                        rblthird.SelectedIndex = 1;
                        break;
                    case "8":
                        if (level == "1")
                        {
                            rblfirst.SelectedIndex = 0;
                            rblsecond.SelectedIndex = -1;
                            rblthird.SelectedIndex = -1;
                        }
                        else if (level == "2")
                        {
                            rblfirst.SelectedIndex = 0;
                            rblsecond.SelectedIndex = 0;
                            rblthird.SelectedIndex = -1;
                        }
                        else if (level == "3")
                        {
                            rblfirst.SelectedIndex = 0;
                            rblsecond.SelectedIndex = 0;
                            rblthird.SelectedIndex = 0;
                        }
                        break;
                    case "0":
                    case "1":
                    case "2": break;
                    default: break;
                }
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (this.AuditPowerCheck())
            {
                InitList();
                status = lblStatus.Text;
                int checklevel = Convert.ToInt16(rblSHJS.SelectedValue);
                List<string> list_sql = new List<string>();
                if (this.CheckSelect(checklevel))
                {
                    // 提交状态(更新审核表及原始数据中的制作明细状态)
                    //其他状态：更新审核表、审核通过或驳回时更新原始数据

                    string reviewdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    //0:审核任务已提交，请等待审核结果...
                    //1:您已完成审核，已提交下一审核人!
                    //2:您的审核意见为【不同意】，审核已驳回!
                    //3:审核通过，材料计划已提交采购!

                    string flag = "";

                    switch (status)
                    {
                        case "1":
                            #region 提交状态
                            sqlText = "update " + mstable + " set MS_SUBMITID='" + Session["UserID"] + "',MS_REVIEWA='" + firstid.Value + "',MS_STATE='2',MS_CHECKLEVEL='" + rblSHJS.SelectedValue + "',MS_SUBMITTM='" + reviewdate + "' where MS_ID='" + ms_id + "'";
                            list_sql.Add(sqlText);
                            flag = "0";
                            break;
                            #endregion
                        case "2":
                            #region 一级审核状态
                            if (checklevel == 1)
                            {
                                if (rblfirst.SelectedIndex == 0)//同意-审核通过
                                {
                                    sqlText = "update " + mstable + " set MS_ADATE='" + reviewdate + "',MS_STATE='8',MS_CK_BT='1',MS_REVIEWAADVC='" + first_opinion.Text.Trim() + "',MS_REVIEWATIME='" + reviewdate + "' where MS_ID='" + ms_id + "'";
                                    list_sql.Add(sqlText);
                                    this.UpdateMainPlanMS(list_sql);
                                    this.AdjustOriginalData(list_sql);
                                    flag = "3";
                                }
                                else if (rblfirst.SelectedIndex == 1)//一级驳回
                                {
                                    sqlText = "update " + mstable + " set MS_ADATE='" + reviewdate + "',MS_STATE='" + rblfirst.SelectedValue + "',MS_REVIEWAADVC='" + first_opinion.Text.Trim() + "',MS_REVIEWATIME='" + reviewdate + "' where MS_ID='" + ms_id + "'";
                                    list_sql.Add(sqlText);

                                    this.BackAdjustOriginalData(list_sql);
                                    flag = "2";
                                }
                            }
                            else if (checklevel > 1)
                            {
                                if (rblfirst.SelectedIndex == 0)//同意-继续下推审核
                                {
                                    sqlText = "update " + mstable + " set MS_STATE='" + rblfirst.SelectedValue + "',MS_REVIEWAADVC='" + first_opinion.Text.Trim() + "',MS_REVIEWATIME='" + reviewdate + "',MS_REVIEWB='" + secondid.Value + "' where MS_ID='" + ms_id + "'";
                                    list_sql.Add(sqlText);
                                    flag = "1";
                                }
                                else if (rblfirst.SelectedIndex == 1)//一级驳回
                                {
                                    sqlText = "update " + mstable + " set MS_ADATE='" + reviewdate + "',MS_STATE='" + rblfirst.SelectedValue + "',MS_REVIEWAADVC='" + first_opinion.Text.Trim() + "',MS_REVIEWATIME='" + reviewdate + "' where MS_ID='" + ms_id + "'";
                                    list_sql.Add(sqlText);

                                    this.BackAdjustOriginalData(list_sql);
                                    flag = "2";
                                }
                            }
                            break;
                            #endregion
                        case "4":
                            #region 二级审核状态
                            if (checklevel == 2)
                            {
                                if (rblsecond.SelectedIndex == 0)//通过
                                {
                                    sqlText = "update " + mstable + " set MS_ADATE='" + reviewdate + "',MS_STATE='8',MS_CK_BT='1',MS_REVIEWBADVC='" + second_opinion.Text.Trim() + "',MS_REVIEWBTIME='" + reviewdate + "' where MS_ID='" + ms_id + "'";
                                    list_sql.Add(sqlText);
                                    this.UpdateMainPlanMS(list_sql);
                                    this.AdjustOriginalData(list_sql);
                                    flag = "3";
                                }
                                else if (rblsecond.SelectedIndex == 1)//驳回
                                {
                                    sqlText = "update " + mstable + " set MS_ADATE='" + reviewdate + "',MS_STATE='" + rblsecond.SelectedValue + "',MS_REVIEWBADVC='" + second_opinion.Text.Trim() + "',MS_REVIEWBTIME='" + reviewdate + "' where MS_ID='" + ms_id + "'";
                                    list_sql.Add(sqlText);

                                    this.BackAdjustOriginalData(list_sql);
                                    flag = "2";
                                }
                            }
                            else if (checklevel > 2)
                            {
                                if (rblsecond.SelectedIndex == 0)//同意-继续下推审核
                                {
                                    sqlText = "update " + mstable + " set MS_STATE='" + rblsecond.SelectedValue + "',MS_REVIEWBADVC='" + second_opinion.Text.Trim() + "',MS_REVIEWBTIME='" + reviewdate + "',MS_REVIEWC='" + thirdid.Value + "' where MS_ID='" + ms_id + "'";
                                    list_sql.Add(sqlText);
                                    flag = "1";
                                }
                                else if (rblsecond.SelectedIndex == 1)//二级驳回
                                {
                                    sqlText = "update " + mstable + " set MS_ADATE='" + reviewdate + "',MS_STATE='" + rblsecond.SelectedValue + "',MS_REVIEWBADVC='" + second_opinion.Text.Trim() + "',MS_REVIEWBTIME='" + reviewdate + "' where MS_ID='" + ms_id + "'";
                                    list_sql.Add(sqlText);

                                    this.BackAdjustOriginalData(list_sql);
                                    flag = "2";
                                }
                            }
                            break;
                            #endregion
                        case "6":
                            #region 三级审核状态
                            if (rblthird.SelectedIndex == 0)//通过
                            {
                                sqlText = "update " + mstable + " set MS_ADATE='" + reviewdate + "',MS_STATE='8',MS_CK_BT='1',MS_REVIEWCADVC='" + third_opinion.Text.Trim() + "',MS_REVIEWCTIME='" + reviewdate + "' where MS_ID='" + ms_id + "'";
                                list_sql.Add(sqlText);
                                this.UpdateMainPlanMS(list_sql);
                                this.AdjustOriginalData(list_sql);
                                flag = "3";
                            }
                            else if (rblthird.SelectedIndex == 1)//三级驳回
                            {
                                sqlText = "update " + mstable + " set MS_ADATE='" + reviewdate + "',MS_STATE='" + rblthird.SelectedValue + "',MS_REVIEWCADVC='" + third_opinion.Text.Trim() + "',MS_REVIEWCTIME='" + reviewdate + "' where MS_ID='" + ms_id + "'";
                                list_sql.Add(sqlText);

                                this.BackAdjustOriginalData(list_sql);
                                flag = "2";
                            }
                            break;
                            #endregion
                        default: break;
                    }
                    DBCallCommon.ExecuteTrans(list_sql);

                    #region 提示信息

                    //string _emailto="";
                    //string _body;
                    //string _subject;

                    if (flag == "0")
                    {
                        if (ckbMessage.Checked)
                        {
                            _emailto = DBCallCommon.GetEmailAddressByUserID(firstid.Value.Trim());
                            _body = "制作明细审批任务:"
                                + "\r\n编    号：" + ms_no.Text.Trim()
                                + "\r\n项目名称：" + proname.Text.Trim() + "_" + pro_id.Value.Trim() + ""
                                + "\r\n设备名称：" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0]
                                + "\r\n技 术 员: " + txt_editor.Text.Trim()
                                + "\r\n编制时间:" + txt_plandate.Text.Trim();

                            _subject = "您有新的【制作明细】需要审批，请及时处理 " + proname.Text.Trim() + "_" + pro_id.Value.Trim() + "" + "_" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0];
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核任务已提交，请等待审核结果...');location.href='TM_Mytast_List.aspx';", true);
                        return;
                    }
                    else if (flag == "1")
                    {
                        if (ckbMessage.Checked)
                        {
                            if (status == "2")//一级审核
                            {
                                _emailto = DBCallCommon.GetEmailAddressByUserID(secondid.Value.Trim());
                            }
                            else if (status == "4")//二级审核
                            {
                                _emailto = DBCallCommon.GetEmailAddressByUserID(thirdid.Value.Trim());
                            }
                            _body = "制作明细审批任务:"
                                        + "\r\n编号：" + ms_no.Text.Trim()
                                        + "\r\n项目名称：" + proname.Text.Trim() + "_" + pro_id.Value.Trim() + ""
                                        + "\r\n设备名称：" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0]
                                        + "\r\n技 术 员: " + txt_editor.Text.Trim()
                                        + "\r\n编制时间:" + txt_plandate.Text.Trim();

                            _subject = "您有新的【制作明细】需要审批，请及时处理: " + proname.Text.Trim() + "_" + pro_id.Value.Trim() + "" + "_" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0];
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您完成审核，已提交下一审核人!');location.href='TM_Leader_Task.aspx';", true);
                        return;
                    }
                    else if (flag == "2")
                    {
                        if (ckbMessage.Checked)
                        {
                            _emailto = DBCallCommon.GetEmailAddressByUserID(editorid.Value.Trim());
                            _body = "制作明细审批已驳回:"
                                + "\r\n编    号：" + ms_no.Text.Trim()
                                + "\r\n项目名称：" + proname.Text.Trim() + "_" + pro_id.Value.Trim() + ""
                                + "\r\n设备名称：" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0]
                                + "\r\n技 术 员: " + txt_editor.Text.Trim()
                                + "\r\n编制时间:" + txt_plandate.Text.Trim();

                            if (status == "2")//一级审核驳回
                            {
                                _body += "\r\n驳回信息: " + txt_first.Text.Trim() + "_一级审核驳回_" + first_opinion.Text.Trim();
                            }
                            else if (status == "4")//二级审核驳回
                            {
                                _body += "\r\n驳回信息: " + txt_second.Text.Trim() + "_二级审核驳回_" + second_opinion.Text.Trim();
                            }
                            else if (status == "6")//三级审核驳回
                            {
                                _body += "\r\n驳回信息: " + txt_third.Text.Trim() + "_三级审核驳回_" + third_opinion.Text.Trim();
                            }
                            _subject = "制作明细审批已驳回: " + proname.Text.Trim() + "_" + pro_id.Value.Trim() + "" + "_" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0];
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您的审核意见为【不同意】，审核已驳回!');location.href='TM_Leader_Task.aspx';", true);
                        return;
                    }
                    else if (flag == "3")
                    {
                        if (ckbMessage.Checked)
                        {
                            _emailto = DBCallCommon.GetEmailAddressByUserID(editorid.Value.Trim());
                            _body = "制作明细已下推生产:"
                                + "\r\n编    号：" + ms_no.Text.Trim()
                                + "\r\n项目名称：" + proname.Text.Trim() + "_" + pro_id.Value.Trim() + ""
                                + "\r\n设备名称：" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0]
                                + "\r\n技 术 员: " + txt_editor.Text.Trim()
                                + "\r\n编制时间:" + txt_plandate.Text.Trim();

                            _subject = "制作明细已下推生产: " + proname.Text.Trim() + "_" + pro_id.Value.Trim() + "" + "_" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0];
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                        //给指定调度员发邮件
                        _emailto = DBCallCommon.GetEmailAddressByUserID("73");
                        _body = "制作明细已下推生产:"
                            + "\r\n编    号：" + ms_no.Text.Trim()
                            + "\r\n项目名称：" + proname.Text.Trim() + "_" + pro_id.Value.Trim() + ""
                            + "\r\n设备名称：" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0]
                            + "\r\n技 术 员: " + txt_editor.Text.Trim()
                            + "\r\n编制时间:" + txt_plandate.Text.Trim();

                        _subject = "制作明细已下推生产: " + proname.Text.Trim() + "_" + pro_id.Value.Trim() + "" + "_" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0];
                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        //给质检人员发邮件
                        if (cols[0].ToString().Length > 6)  //变更
                        {
                            string sqlQPerson = "select QSA_QCCLERK from TBQM_QTSASSGN where QSA_ENGID='" + tsa_id.Text.ToString().Trim() + "'";
                            DataTable dtQPerson = DBCallCommon.GetDTUsingSqlText(sqlQPerson);
                            if (dtQPerson.Rows.Count == 1)
                            {
                                _emailto = DBCallCommon.GetEmailAddressByUserID(dtQPerson.Rows[0][0].ToString());
                                _body = "制作明细已变更:"
                                    + "\r\n编    号：" + ms_no.Text.Trim()
                                    + "\r\n项目名称：" + proname.Text.Trim() + "_" + pro_id.Value.Trim() + ""
                                    + "\r\n设备名称：" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0]
                                    + "\r\n技 术 员: " + txt_editor.Text.Trim()
                                    + "\r\n编制时间:" + txt_plandate.Text.Trim();

                                _subject = "制作明细发生变更: " + proname.Text.Trim() + "_" + pro_id.Value.Trim() + "" + "_" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0];
                                DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                            }
                        }
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核通过，制作明细已生成并提交生产部!');location.href='TM_Leader_Task.aspx';", true);
                        return;
                    }
                    #endregion
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请将审核人或审核意见填写完整！！！');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您已完成审核或无权审核该批任务！！！');", true);
                return;
            }
        }
        /// <summary>
        /// 更新主生产计划预警系统【制作明细】
        /// </summary>
        /// <param name="list_sql"></param>
        private void UpdateMainPlanMS(List<string> list_sql)
        {
            sqlText = "update TBMP_MAINPLANDETAIL set MP_STATE=2,MP_ACTURALTIME='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where MP_ENGID='" + tsa_id.Text.Trim() + "' and MP_PLNAME='制作明细'";
            list_sql.Add(sqlText);
            sqlText = "update TBMP_MAINPLANDETAIL set MP_STATE=1 where MP_ENGID='" + tsa_id.Text.Trim() + "' and MP_TYPE='生产周期'";
            list_sql.Add(sqlText);
        }
        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }
        /// <summary>
        /// 能否提交判断
        /// </summary>
        /// <returns></returns>
        private bool CheckSelect(int level)
        {
            bool ret = false;
            if (status == "1") //下推审核
            {
                if (firstid.Value != "")
                {
                    ret = true;
                }
            }
            else if (status != "0" && status != "1") //审核中
            {
                if (level == 1)
                {
                    if (rblfirst.SelectedIndex == 0 || rblfirst.SelectedIndex == 1)//一级审核
                    {
                        ret = true;
                    }
                }
                else if (level == 2)
                {
                    if (status == "2")//二级审核正在进行第一级审核
                    {
                        if (rblfirst.SelectedIndex == 1)//一级审核不同意
                        {
                            ret = true;
                        }
                        else if (rblfirst.SelectedIndex == 0)//一级审核同意
                        {
                            if (secondid.Value != "")//指定二级审核人
                            {
                                ret = true;
                            }
                        }
                    }
                    else if (status == "4")//二级审核正在进行第二级审核
                    {
                        if (rblsecond.SelectedIndex == 0 || rblsecond.SelectedIndex == 1)
                        {
                            ret = true;
                        }
                    }
                }
                else if (level == 3)
                {
                    if (status == "2")//三级审核正在进行第一级审核
                    {
                        if (rblfirst.SelectedIndex == 1)//一级审核不同意
                        {
                            ret = true;
                        }
                        else if (rblfirst.SelectedIndex == 0)//一级审核同意
                        {
                            if (secondid.Value != "")//指定二级审核人
                            {
                                ret = true;
                            }
                        }
                    }
                    else if (status == "4")//三级审核正在进行第二级审核
                    {
                        if (rblsecond.SelectedIndex == 1)//二级审核不同意
                        {
                            ret = true;
                        }
                        else if (rblsecond.SelectedIndex == 0)//二级审核同意
                        {
                            if (thirdid.Value != "")//指定三级审核人
                            {
                                ret = true;
                            }
                        }
                    }
                    else if (status == "6") //三级审核正在进行三级审核
                    {
                        if (rblthird.SelectedIndex == 0 || rblthird.SelectedIndex == 1)
                        {
                            ret = true;
                        }
                    }
                }
            }
            return ret;
        }
        /// <summary>
        /// 对通过进行调整原始数据和下推生产部
        /// 将原始数据，审核状态为'3'，变更状态为'0'
        /// </summary>
        /// <param name="list"></param>
        private void AdjustOriginalData(List<string> list)
        {

            if (viewtable == "View_TM_MSCHANGE")  //对于变更
            {
                //变更审核通过将变更明细下推生产部

                sqlText = "exec TM_MSGoPushing '" + ms_id + "','" + viewtable + "','" + tstable + "'";
                list.Add(sqlText);

                sqlText = "update " + strtable + " set BM_MSREVIEW='3',BM_MSSTATUS='0' where BM_ENGID='" + tsa_id.Text + "' and BM_MSSTATE='1' and BM_MSSTATUS!='0' and dbo.f_SubBJ(BM_XUHAO,'.') in (select distinct dbo.f_SubBJ(MS_NEWINDEX,'.') as MS_NEWINDEX from " + viewtable + " where MS_PID='" + ms_id + "' and dbo.f_SubBJ(MS_NEWINDEX,'.') is not null)";
                list.Add(sqlText);
            }
            else
            {
                sqlText = "update " + strtable + " set BM_MSREVIEW='3',BM_MSSTATUS='0' where BM_ENGID='" + tsa_id.Text + "' and BM_MSSTATE='1' and BM_MSSTATUS='0' and dbo.f_SubBJ(BM_XUHAO,'.') in (select distinct dbo.f_SubBJ(MS_NEWINDEX,'.') as MS_NEWINDEX from " + viewtable + " where MS_PID='" + ms_id + "' and dbo.f_SubBJ(MS_NEWINDEX,'.') is not null)";
                list.Add(sqlText);
                //sqlText = "update " + strtable + " set BM_MSREVIEW='3' where BM_TASKID='" + tsa_id.Text + "'and BM_ZONGXU in (select MS_ZONGXU from " + tablename + " where MS_PID='" + ms_id + "')";
                //  list.Add(sqlText);
                //正常审核通过将制作明细下推生产部
                sqlText = "exec TM_MSGoPushing '" + ms_id + "','" + viewtable + "','" + tstable + "'";
                list.Add(sqlText);
            }
            sqlText = "delete from TBPM_TEMPMARDATA where BM_ENGID='" + tsa_id.Text + "'";
            list.Add(sqlText);
            sqlText = "insert into  TBPM_TEMPMARDATA (BM_XUHAO, BM_ZONGXU, BM_MARID, BM_ENGID, BM_MAUNITWGHT, BM_MAWGHT, BM_NUMBER, BM_MAWIDTH, BM_MALENGTH, BM_MATOTALLGTH, BM_TASKID, BM_MIANYU, BM_MASHAPE, BM_FIXEDSIZE, BM_MPSTATUS, BM_ALLBEIZHU, BM_UNITWGHT, BM_OLDINDEX, BM_TUHAO, BM_TECHUNIT, BM_YONGLIANG, BM_WMARPLAN, BM_MPSTATE) select BM_XUHAO, BM_ZONGXU, BM_MARID, BM_ENGID, BM_MAUNITWGHT, BM_MATOTALWGHT, BM_NUMBER, BM_MAWIDTH, BM_MALENGTH, BM_MATOTALLGTH, BM_TASKID, BM_MPMY, BM_MASHAPE, BM_FIXEDSIZE, BM_MPSTATUS, BM_ALLBEIZHU, BM_UNITWGHT, BM_OLDINDEX, BM_TUHAO, BM_TECHUNIT, BM_YONGLIANG, BM_WMARPLAN, BM_MPSTATE from TBPM_STRINFODQO where BM_ENGID='" + tsa_id.Text + "' ";
            list.Add(sqlText);
        }
        /// <summary>
        /// 对驳回进行调整原始数据
        /// 将原始数据的提交状态变为'0'，审核状态为'0'
        /// </summary>
        /// <param name="list"></param>
        private void BackAdjustOriginalData(List<string> list)
        {

            if (viewtable == "View_TM_MSCHANGE")  //对于变更
            {
                sqlText = "update " + strtable + " set BM_MSSTATE='0', BM_MSREVIEW='0' where BM_ENGID='" + tsa_id.Text + "' and BM_MSSTATE='1'   and BM_MSSTATUS!='0' and dbo.f_SubBJ(BM_XUHAO,'.') in (select distinct dbo.f_SubBJ(MS_NEWINDEX,'.') as MS_NEWINDEX from " + viewtable + " where MS_PID='" + ms_id + "' and dbo.f_SubBJ(MS_NEWINDEX,'.') is not null)";
                list.Add(sqlText);
                //修改最顶级部件
            }
            else
            {
                sqlText = "update " + strtable + " set BM_MSSTATE='0', BM_MSREVIEW='0' where BM_ENGID='" + tsa_id.Text + "' and BM_MSSTATE='1'   and BM_MSSTATUS='0' and BM_XUHAO in (select MS_NEWINDEX from " + viewtable + " as b where MS_PID='" + ms_id + "' )";
                list.Add(sqlText);
            }
            list.Add(sqlText);

        }
        #region ShowModual
        [System.Web.Services.WebMethodAttribute(),
           System.Web.Script.Services.ScriptMethodAttribute()]
        public static string GetMsDynamicContent(string contextKey)
        {
            StringBuilder sTemp = new StringBuilder();
            string sql_getmsbefore = "select MS_TUHAO,MS_ZONGXU,MS_NAME,MS_MAGUIGE,MS_CAIZHI,cast(MS_UNUM as varchar)+' | '+cast(MS_NUM as varchar) AS NUMBER,MS_TUWGHT,MS_TUTOTALWGHT,MS_MASHAPE,MS_LENGTH,MS_WIDTH,MS_NOTE,MS_MAUNIT,MS_XIALIAO,MS_PROCESS,MS_WAIXINGCH,MS_KU,MS_UWGHT,MS_TLWGHT,MS_ALLBEIZHU from " + bgmstablerv + "  where MS_NEWINDEX='" + contextKey + "' and MS_CHGPID='" + ms_id + "'";

            DataTable dt_chgbef = DBCallCommon.GetDTUsingSqlText(sql_getmsbefore);

            sTemp.Append("<table style='background-color:#f3f3f3; border: #B9D3EE 3px solid;font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>");
            sTemp.Append("<tr><td colspan='17' style='background-color:#B9D3EE; color:white;' align='center'><b>变更前信息:</b></td></tr>");

            if (dt_chgbef.Rows.Count > 0)
            {
                sTemp.Append("<tr><td><b>图号</b></td>");
                sTemp.Append("<td align='center'><b>总序</b></td>");
                sTemp.Append("<td align='center'><b>名称</b></td>");
                sTemp.Append("<td align='center'><b>规格</b></td>");
                sTemp.Append("<td align='center'><b>材质</b></td>");
                sTemp.Append("<td align='center'><b>单台数量|总数量</b></td>");
                sTemp.Append("<td align='center'><b>图纸单重</b></td>");
                sTemp.Append("<td align='center'><b>图纸总重</b></td>");
                sTemp.Append("<td align='center'><b>材料种类</b></td>");
                sTemp.Append("<td align='center'><b>长度</b></td>");
                sTemp.Append("<td align='center'><b>宽度</b></td>");
                sTemp.Append("<td align='center'><b>下料备注</b></td>");
                sTemp.Append("<td align='center'><b>单位</b></td>");
                sTemp.Append("<td align='center'><b>下料</b></td>");
                sTemp.Append("<td align='center'><b>工艺流程</b></td>");
                sTemp.Append("<td align='center'><b>外形尺寸</b></td>");
                sTemp.Append("<td align='center'><b>入库级别</b></td>");
                sTemp.Append("<td align='center'><b>单重</b></td>");
                sTemp.Append("<td align='center'><b>总重</b></td>");
                sTemp.Append("<td align='center'><b>备注</b></td></tr>");

                for (int i = 0; i < dt_chgbef.Rows.Count; i++)
                {
                    sTemp.Append("<tr style='border-width:1px'>");

                    for (int j = 0; j < dt_chgbef.Columns.Count; j++)
                    {
                        sTemp.Append("<td  style='white-space:pre;' align='center'>" + dt_chgbef.Rows[i][j].ToString() + "</td>");
                    }
                    sTemp.Append("</tr>");
                }
            }
            else
            {
                sTemp.Append("<tr><td colspan='17'><i>没有记录...</i></td></tr>");
            }
            sTemp.Append("</table>");

            return sTemp.ToString();
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (ms_no.Text.Trim().Contains(".JSB MSBG/"))
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    // Programmatically reference the PopupControlExtender
                    PopupControlExtender pce = e.Row.FindControl("PopupControlExtender1") as PopupControlExtender;
                    // Set the BehaviorID
                    string behaviorID = string.Concat("pce", e.Row.RowIndex);
                    pce.BehaviorID = behaviorID;

                    // Programmatically reference the Image control
                    HyperLink hpl = (HyperLink)e.Row.FindControl("hplBeforeChg");

                    // Add the clie nt-side attributes (onmouseover & onmouseout)
                    string OnMouseOverScript = string.Format("$find('{0}').showPopup();", behaviorID);
                    string OnMouseOutScript = string.Format("$find('{0}').hidePopup();", behaviorID);

                    hpl.Attributes.Add("onmouseover", OnMouseOverScript);
                    hpl.Attributes.Add("onmouseout", OnMouseOutScript);
                }
            }


        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (lblStatus.Text.Trim() == "0" || lblStatus.Text.Trim() == "1" || (lblStatus.Text.Trim() == "2" && (Session["UserID"].ToString() == editorid.Value || Session["UserID"].ToString() == firstid.Value)))
                {
                    for (int i = 2; i < e.Row.Cells.Count; i++)//获取总列数
                    {
                        //如果是数据行则添加title
                        e.Row.Attributes["style"] = "Cursor:hand";
                        //  e.Row.Cells[i].Attributes.Add("title", "双击修改数据");
                    }
                    //鼠标经过时，行背景色变 
                    //    e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#EEE8AA'");
                    //鼠标移出时，行背景色变 
                    //  e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

                    string url = e.Row.Cells[3].Text.Trim() + "&strtable=" + strtable + "&tablename=" + tablename + "&taskid=" + ViewState["taskid"].ToString() + "&ms_no=" + ms_no.Text.Trim() + "";
                    //  e.Row.Attributes["ondblclick"] = String.Format("javascript:dbl_click=true;return openLink('" + url + "');");

                }
                if (((HtmlInputHidden)e.Row.FindControl("hidPzState")).Value == "1")
                {
                    ((CheckBox)e.Row.FindControl("cbxPzState")).Checked = true;
                    // e.Row.BackColor = Color.Turquoise;

                }
                else
                {
                    ((CheckBox)e.Row.FindControl("cbxPzState")).Checked = false;
                    e.Row.BackColor = Color.White;

                }

            }




        }
        #endregion

        #region  分页
        PagerQueryParam pager_ms = new PagerQueryParam();

        /// <summary>
        /// 初始化分布信息
        /// </summary>
        //private void InitVarMS()
        //{
        //    InitPagerMS();
        //    UCPagingMS.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);
        //    UCPagingMS.PageSize = pager_ms.PageSize;    //每页显示的记录数
        //}
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPagerMS()
        {
            pager_ms.TableName = tablename;
            pager_ms.PrimaryKey = "MS_ID";
            //pager_ms.ShowFields = "MS_MSXUHAO,MS_NEWINDEX,MS_TUHAO,MS_ZONGXU,MS_NAME,MS_GUIGE,MS_CAIZHI,MS_UNUM,MS_UWGHT,(CASE WHEN MS_KU='库' THEN  CAST(MS_TLWGHT AS VARCHAR) ELSE '' END) AS MS_TLWGHT,MS_MASHAPE,MS_MASTATE,MS_STANDARD,MS_KU,MS_PROCESS,MS_NOTE,MS_STATUS";
            pager_ms.ShowFields = "*,cast(MS_UNUM as varchar)+' | '+cast(MS_NUM as varchar) AS NUMBER";
            pager_ms.OrderField = "dbo.f_formatstr(MS_NEWINDEX, '.')";
            pager_ms.StrWhere = ViewState["sqlText"].ToString();
            pager_ms.OrderType = 0;//升序排列
            pager_ms.PageSize = 200;
        }
        //void Pager_PageChangedMS(int pageNumber)
        //{
        //    bindGridMS();
        //}
        //private void bindGridMS()
        //{
        //    pager_ms.PageIndex = UCPagingMS.CurrentPage;
        //    DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_ms);
        //    CommonFun.Paging(dt, GridView1, UCPagingMS, NoDataPanel);
        //    if (NoDataPanel.Visible)
        //    {
        //        UCPagingMS.Visible = false;
        //    }
        //    else
        //    {
        //        UCPagingMS.Visible = true;
        //        UCPagingMS.InitPageInfo();  //分页控件中要显示的控件
        //    }

        //}
        #endregion

     
        ///// <summary>
        ///// 保存批注
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnPiZhuSave_Click(object sender, EventArgs e)
        //{
        //    InitList();
        //    List<string> sqlist = new List<string>();
        //    for (int i = 0; i < GridView1.Rows.Count; i++)
        //    {
        //        GridViewRow gRow = GridView1.Rows[i];
        //        string zongxu = gRow.Cells[5].Text.Trim();
        //        string pizhubeizhu = ((HtmlInputText)gRow.FindControl("txtPiZhu")).Value;

        //        if (((CheckBox)gRow.FindControl("cbxPzState")).Checked)
        //        {
        //            sqlText = "update " + tablename + " set MS_PIZHUSTATE='1',MS_PIZHUBEIZHU='" + pizhubeizhu + "' where MS_PID='" + ms_no.Text.Trim() + "' and MS_ZONGXU='" + zongxu + "'";
        //        }
        //        else
        //        {
        //            sqlText = "update " + tablename + " set MS_PIZHUSTATE='0',MS_PIZHUBEIZHU='' where MS_PID='" + ms_no.Text.Trim() + "' and MS_ZONGXU='" + zongxu + "'";
        //        }
        //        sqlist.Add(sqlText);
        //    }

        //    try
        //    {
        //        DBCallCommon.ExecuteTrans(sqlist);

        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！！！');", true);
        //        this.InitVarMS();
        //    }
        //    catch (Exception)
        //    {

        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('程序异常，请稍后再试！！！');", true);
        //    }
        //}

        ///// <summary>
        ///// 保存变更备注
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnBianGengBZ_Click(object sender, EventArgs e)
        //{
        //    List<string> sqlist = new List<string>();
        //    for (int i = 0; i < GridView1.Rows.Count; i++)
        //    {
        //        GridViewRow gRow = GridView1.Rows[i];
        //        string zongxu = gRow.Cells[5].Text.Trim();
        //        string biangengbeizhu = ((HtmlInputText)gRow.FindControl("txtBianGengBZ")).Value;


        //        sqlText = "update TBPM_MSCHANGE set MS_BIANGENGBEIZHU='" + biangengbeizhu + "' where MS_PID='" + ms_no.Text.Trim() + "' and MS_ZONGXU='" + zongxu + "'";

        //        sqlist.Add(sqlText);
        //    }

        //    try
        //    {
        //        DBCallCommon.ExecuteTrans(sqlist);

        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！！！');", true);
        //        this.InitVarMS();
        //    }
        //    catch (Exception)
        //    {

        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('程序异常，请稍后再试！！！');", true);
        //    }
        //}

  
  

    
        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton1_OnClick(object sender, EventArgs e)
        {

            ExportTMDataFromDB.ExportMSData(ms_no.Text.Trim(), tsa_id.Text.Trim());
        }

    }
}
