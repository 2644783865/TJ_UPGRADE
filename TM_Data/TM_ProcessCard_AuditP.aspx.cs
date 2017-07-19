using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_ProcessCard_AuditP : System.Web.UI.Page
    {
        string sqlText;
        string status;
        string level;
        string _emailto;
        string _body;
        string _subject;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Iinit();

            }
        }

        private void Iinit()
        {
            if (Request.QueryString["auditId"] != null)
            {
                hidProId.Value = Request.QueryString["auditId"].ToString();
            }
            else if (Request.QueryString["Id"] != null)
            {
                hidProId.Value = Request.QueryString["Id"].ToString();
            }

            sqlText = "select * from View_TM_PROCESS_CARD where PRO_ID='" + hidProId.Value + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

            // PRO_ENGNAME, PRO_ENGMODEL, PRO_PARTNAME, PRO_TUHAO, PRO_FUJIAN, PRO_NOTE, PRO_ISSUEDTIME, PRO_SUBMITID, PRO_REVIEWAADVC, PRO_REVIEWA, PRO_REVIEWATIME, PRO_REVIEWB, PRO_REVIEWBADVC, PRO_REVIEWBTIME, PRO_REVIEWC, PRO_REVIEWCADVC, PRO_REVIEWCTIME, PRO_STATE, PRO_ADATE, PRO_SUBMITNM, PRO_REVIEWBNM, PRO_REVIEWCNM, PRO_REVIEWANM, fileID, BC_CONTEXT, fileload, fileUpDate, fileName
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                lblEngname.Text = row["PRO_ENGNAME"].ToString();
                lblEngType.Text = row["PRO_ENGMODEL"].ToString();
                lblPartName.Text = row["PRO_PARTNAME"].ToString();
                lblMap.Text = row["PRO_TUHAO"].ToString();
                lblEditor.Text = row["PRO_SUBMITNM"].ToString();
                lblSubmitTime.Text = row["PRO_ISSUEDTIME"].ToString();
                txtNote.Text = row["PRO_NOTE"].ToString();
                txt_first.Text = row["PRO_REVIEWANM"].ToString();
                first_opinion.Text = row["PRO_REVIEWAADVC"].ToString();
                first_time.Text = row["PRO_REVIEWATIME"].ToString();
                txt_second.Text = row["PRO_REVIEWBNM"].ToString();
                second_opinion.Text = row["PRO_REVIEWBADVC"].ToString();
                second_time.Text = row["PRO_REVIEWBTIME"].ToString();
                txt_third.Text = row["PRO_REVIEWCNM"].ToString();
                third_opinion.Text = row["PRO_REVIEWCADVC"].ToString();
                third_time.Text = row["PRO_REVIEWCTIME"].ToString();


                status = row["PRO_STATE"].ToString();//当前批材料计划的审核状态
                lblStatus.Text = status;

                //审核图片
                if (status == "3" || status == "5" || status == "7" || status == "8")
                {
                    ImageVerify.Visible = true;
                }
                else
                {
                    ImageVerify.Visible = false;
                }
                this.RblOfThreeStateConfirm(status, row["PRO_CHECKLEVEL"].ToString());
                level = row["PRO_CHECKLEVEL"].ToString();//审核级数

                rblSHJS.SelectedIndex = Convert.ToInt16(row["PRO_CHECKLEVEL"].ToString()) - 1;

                editorid.Value = row["PRO_SUBMITID"].ToString();
                firstid.Value = row["PRO_REVIEWA"].ToString();
                secondid.Value = row["PRO_REVIEWB"].ToString();
                thirdid.Value = row["PRO_REVIEWC"].ToString();

                #region 状态判断
                if (Request.QueryString["Id"] != null)  //查看进入(针对提交人)
                {
                    if ((status == "1" || status == "0") && level == "2")//未指定审核人
                    {
                        rblSHJS.SelectedIndex = 1; //默认二级审核
                        hlSelect1.Visible = true;
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
                    else
                    {

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

                if (Request.QueryString["auditId"] != null)//审核人进入
                {
                    rblSHJS.Enabled = false;

                    if (status == "2")//一级审核
                    {
                        if (level == "1")
                        {
                            hlSelect1.Visible = false;
                            first_opinion.Enabled = true;
                            rblfirst.Enabled = true;
                            txt_first.Enabled = true;

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
                            txt_first.Enabled = true;

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
                            txt_second.Enabled = true;

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
                            txt_second.Enabled = true;

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
                        txt_third.Enabled = true;
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

        }

        private void RblOfThreeStateConfirm(string state, string p)
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
                            sqlText = "update TBPM_PROCESS_CARD set PRO_SUBMITID='" + Session["UserID"] + "',PRO_REVIEWA='" + firstid.Value + "',PRO_STATE='2',PRO_CHECKLEVEL='" + rblSHJS.SelectedValue + "',PRO_ISSUEDTIME='" + reviewdate + "'  where PRO_ID='" + hidProId.Value + "'";
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
                                    sqlText = "update TBPM_PROCESS_CARD set PRO_ADATE='" + reviewdate + "',PRO_STATE='8',PRO_REVIEWAADVC='" + first_opinion.Text.Trim() + "',PRO_REVIEWATIME='" + reviewdate + "' where PRO_ID='" + hidProId.Value + "'";
                                    list_sql.Add(sqlText);
                                    flag = "3";
                                }
                                else if (rblfirst.SelectedIndex == 1)//一级驳回
                                {
                                    sqlText = "update TBPM_PROCESS_CARD set PRO_ADATE='" + reviewdate + "',PRO_STATE='" + rblfirst.SelectedValue + "',PRO_REVIEWAADVC='" + first_opinion.Text.Trim() + "',PRO_REVIEWATIME='" + reviewdate + "' where PRO_ID='" + hidProId.Value + "'";
                                    list_sql.Add(sqlText);
                                    flag = "2";
                                }
                            }
                            else if (checklevel > 1)
                            {
                                if (rblfirst.SelectedIndex == 0)//同意-继续下推审核
                                {
                                    sqlText = "update TBPM_PROCESS_CARD set PRO_STATE='" + rblfirst.SelectedValue + "',PRO_REVIEWAADVC='" + first_opinion.Text.Trim() + "',PRO_REVIEWATIME='" + reviewdate + "',PRO_REVIEWB='" + secondid.Value + "' where PRO_ID='" + hidProId.Value + "'";
                                    list_sql.Add(sqlText);
                                    flag = "1";
                                }
                                else if (rblfirst.SelectedIndex == 1)//一级驳回
                                {
                                    sqlText = "update TBPM_PROCESS_CARD set PRO_ADATE='" + reviewdate + "',PRO_STATE='" + rblfirst.SelectedValue + "',PRO_REVIEWAADVC='" + first_opinion.Text.Trim() + "',PRO_REVIEWATIME='" + reviewdate + "' where PRO_ID='" + hidProId.Value + "'";
                                    list_sql.Add(sqlText);
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
                                    sqlText = "update TBPM_PROCESS_CARD set PRO_ADATE='" + reviewdate + "',PRO_STATE='8',PRO_REVIEWBADVC='" + second_opinion.Text.Trim() + "',PRO_REVIEWBTIME='" + reviewdate + "' where PRO_ID='" + hidProId.Value + "'";
                                    list_sql.Add(sqlText);

                                    flag = "3";
                                }
                                else if (rblsecond.SelectedIndex == 1)//驳回
                                {
                                    sqlText = "update TBPM_PROCESS_CARD set PRO_ADATE='" + reviewdate + "',PRO_STATE='" + rblsecond.SelectedValue + "',PRO_REVIEWBADVC='" + second_opinion.Text.Trim() + "',PRO_REVIEWBTIME='" + reviewdate + "' where PRO_ID='" + hidProId.Value + "'";
                                    list_sql.Add(sqlText);
                                    flag = "2";
                                }
                            }
                            else if (checklevel > 2)
                            {
                                if (rblsecond.SelectedIndex == 0)//同意-继续下推审核
                                {
                                    sqlText = "update TBPM_PROCESS_CARD set PRO_STATE='" + rblsecond.SelectedValue + "',PRO_REVIEWBADVC='" + second_opinion.Text.Trim() + "',PRO_REVIEWBTIME='" + reviewdate + "',PRO_REVIEWC='" + thirdid.Value + "' where PRO_ID='" + hidProId.Value + "'";
                                    list_sql.Add(sqlText);
                                    flag = "1";
                                }
                                else if (rblsecond.SelectedIndex == 1)//二级驳回
                                {
                                    sqlText = "update TBPM_PROCESS_CARD set PRO_ADATE='" + reviewdate + "',PRO_STATE='" + rblsecond.SelectedValue + "',PRO_REVIEWBADVC='" + second_opinion.Text.Trim() + "',PRO_REVIEWBTIME='" + reviewdate + "' where PRO_ID='" + hidProId.Value + "'";
                                    list_sql.Add(sqlText);
                                    flag = "2";
                                }
                            }
                            break;
                            #endregion
                        case "6":
                            #region 三级审核状态
                            if (rblthird.SelectedIndex == 0)//通过
                            {
                                sqlText = "update TBPM_PROCESS_CARD set PRO_ADATE='" + reviewdate + "',PRO_STATE='8',PRO_REVIEWCADVC='" + third_opinion.Text.Trim() + "',PRO_REVIEWCTIME='" + reviewdate + "' where PRO_ID='" + hidProId.Value + "'";
                                list_sql.Add(sqlText);

                                flag = "3";
                            }
                            else if (rblthird.SelectedIndex == 1)//三级驳回
                            {
                                sqlText = "update TBPM_PROCESS_CARD set PRO_ADATE='" + reviewdate + "',PRO_STATE='" + rblthird.SelectedValue + "',PRO_REVIEWCADVC='" + third_opinion.Text.Trim() + "',PRO_REVIEWCTIME='" + reviewdate + "' where PRO_ID='" + hidProId.Value + "'";
                                list_sql.Add(sqlText);
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
                                + "\r\n编    号：" + hidProId.Value
                                + "\r\n设备名称：" + lblEngname.Text.Trim()
                                + "\r\n技 术 员: " + lblEditor.Text.Trim()
                                + "\r\n编制时间:" + lblSubmitTime.Text.Trim();

                            _subject = "您有新的【工艺类卡片】需要审批，请及时处理 " + lblEngname.Text.Trim() + "_" + hidProId.Value.Trim();
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核任务已提交，请等待审核结果...');location.href='TM_ProcessCard_List.aspx';", true);
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
                                + "\r\n编    号：" + hidProId.Value
                                + "\r\n设备名称：" + lblEngname.Text.Trim()
                                + "\r\n技 术 员: " + lblEditor.Text.Trim()
                                + "\r\n编制时间:" + lblSubmitTime.Text.Trim();

                            _subject = "您有新的【工艺类卡片】需要审批，请及时处理 " + lblEngname.Text.Trim() + "_" + hidProId.Value.Trim();
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您完成审核，已提交下一审核人!');location.href='TM_ProcessCard_List.aspx';", true);
                        return;
                    }
                    else if (flag == "2")
                    {
                        if (ckbMessage.Checked)
                        {
                            _emailto = DBCallCommon.GetEmailAddressByUserID(editorid.Value.Trim());
                            _body = "工艺卡片审批已驳回:"
                          + "\r\n编    号：" + hidProId.Value
                                + "\r\n设备名称：" + lblEngname.Text.Trim()
                                + "\r\n技 术 员: " + lblEditor.Text.Trim()
                                + "\r\n编制时间:" + lblSubmitTime.Text.Trim();

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
                            _subject = "工艺类卡片已驳回: " + lblEngname.Text.Trim() + "_" + hidProId.Value.Trim() + "";
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您的审核意见为【不同意】，审核已驳回!');location.href='TM_ProcessCard_List.aspx';", true);
                        return;
                    }
                    else if (flag == "3")
                    {
                        if (ckbMessage.Checked)
                        {
                            _emailto = DBCallCommon.GetEmailAddressByUserID(editorid.Value.Trim());
                            _body = "工艺卡片审批已通过:"
                          + "\r\n编    号：" + hidProId.Value
                                + "\r\n设备名称：" + lblEngname.Text.Trim()
                                + "\r\n技 术 员: " + lblEditor.Text.Trim()
                                + "\r\n编制时间:" + lblSubmitTime.Text.Trim();


                            _subject = "工艺类卡片已经审核通过: " + lblEngname.Text.Trim() + "_" + hidProId.Value.Trim() + "";
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                        sqlText = "select ST_ID from TBDS_STAFFINFO where ST_POSITION='0404'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            _emailto = dt.Rows[i][0].ToString();
                            _body = "工艺卡片已经下发:"
                            + "\r\n编    号：" + hidProId.Value
                             + "\r\n设备名称：" + lblEngname.Text.Trim()
                             + "\r\n技 术 员: " + lblEditor.Text.Trim()
                             + "\r\n编制时间:" + lblSubmitTime.Text.Trim();
                            _subject = "工艺卡片已经下发: " + lblEngname.Text.Trim() + "_" + hidProId.Value.Trim() + "";
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);

                        }

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核通过，工艺卡片已生成并提交生产部!');location.href='TM_ProcessCard_List.aspx';", true);
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
        /// 审核权限检查
        /// </summary>
        /// <returns></returns>
        private bool AuditPowerCheck()
        {
            bool retVal = true;
            if (Request.QueryString["auditId"] != null)
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
        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }
    }
}
