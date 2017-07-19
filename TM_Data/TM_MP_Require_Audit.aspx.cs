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
    public partial class TM_MP_Require_Audit : System.Web.UI.Page
    {
        string sqlText;
        string tablename;//材料计划
        string view_tablename;
        string mp_verifyid;
        string mp_verifyno;
        string mp_id;
        string[] fields;
        string[] cols;
        string mptable;//审核表
        string view_mptable;
        string strtable;//结构表
        string status = "";//审核状态
        string techunit;
        protected void Page_Load(object sender, EventArgs e)
        {
            mp_verifyid = Request.QueryString["id"];
            mp_verifyno = Request.QueryString["mp_audit_id"];
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);

            if (Request.QueryString["id"] != null)
            {
                mp_id = mp_verifyid;
            }
            else if (Request.QueryString["mp_audit_id"] != null)
            {
                mp_id = mp_verifyno;
            }

            if (!IsPostBack)
            {
                GetMPVerifyData();
                hpView.NavigateUrl = "TM_MP_Old_View.aspx?action=" + mp_no.Text;
            }
        }
        /// <summary>
        /// 审核权限检查
        /// </summary>
        /// <returns></returns>
        private bool AuditPowerCheck()
        {
            bool retVal = true;
            if (Request.QueryString["mp_audit_id"] != null)
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
        /// 初始化表名(获取是变更表还是正常表)
        /// </summary>
        private void InitList()
        {
            #region
            tablename = "TBPM_MPPLAN";
            view_tablename = "View_TM_MPPLAN";
            strtable = "TBPM_STRINFODQO";
            this.GetTabelNormalChange();
            #endregion
        }
        private void GetTabelNormalChange()
        {
            fields = mp_id.Split('.');
            cols = fields[1].ToString().Split('/');

            if (cols[0].ToString().Length > 6)
            {
                mptable = "TBPM_MPCHANGERVW";
                view_mptable = "View_TM_MPCHANGERVW";

                tablename = "TBPM_MPCHANGE";
                view_tablename = "View_TM_MPCHANGE";
            }
            else
            {
                mptable = "TBPM_MPFORALLRVW";
                view_mptable = "View_TM_MPFORALLRVW";
                hpView.Visible = false;
            }
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        private void GetMPVerifyData()
        {
            btnsubmit.Text = "提 交";
            string level = "";//审核级数

            this.InitList();
            //从审核表中读取基本信息
            sqlText = "select MP_ID,CM_PROJ as MP_PJNAME,TSA_ENGNAME as  MP_ENGNAME,MP_ENGTYPE,MP_SUBMITNM,MP_SUBMITTM,";//6
            sqlText += "MP_REVIEWANAME,MP_REVIEWAADVC,MP_REVIEWATIME,MP_REVIEWBNAME,";//4
            sqlText += "MP_REVIEWBADVC,MP_REVIEWBTIME,MP_REVIEWCNAME,MP_REVIEWCADVC,";//4
            sqlText += "MP_REVIEWCTIME,MP_ADATE,MP_STATE,MP_ENGID,MP_PJID,MP_SUBMITID,";//6
            sqlText += "MP_REVIEWA,MP_CHECKLEVEL,MP_NOTE,MP_REVIEWB,MP_REVIEWC,MP_MASHAPE,MP_MAP,MP_CHILDENGNAME,MP_IFFAST from " + view_mptable + " where MP_ID='" + mp_id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);

            if (dr.HasRows)
            {
                #region 读取数据
                dr.Read();
                mp_no.Text = dr["MP_ID"].ToString();
                lab_proname.Text = dr["MP_PJNAME"].ToString();
                proname.Text = dr["MP_PJNAME"].ToString();
                lab_engname.Text = dr["MP_CHILDENGNAME"].ToString();
                engname.Text = dr["MP_ENGNAME"].ToString();
                eng_type.Value = dr["MP_ENGTYPE"].ToString();
                txt_editor.Text = dr["MP_SUBMITNM"].ToString();
                txt_plandate.Text = dr["MP_SUBMITTM"].ToString();
                plandate.Text = dr["MP_SUBMITTM"].ToString();
                txt_first.Text = dr["MP_REVIEWANAME"].ToString();
                first_opinion.Text = dr["MP_REVIEWAADVC"].ToString();
                first_time.Text = dr["MP_REVIEWATIME"].ToString();
                txt_second.Text = dr["MP_REVIEWBNAME"].ToString();
                second_opinion.Text = dr["MP_REVIEWBADVC"].ToString();
                second_time.Text = dr["MP_REVIEWBTIME"].ToString();
                txt_third.Text = dr["MP_REVIEWCNAME"].ToString();
                third_opinion.Text = dr["MP_REVIEWCADVC"].ToString();
                third_time.Text = dr["MP_REVIEWCTIME"].ToString();
                txt_approval.Text = dr["MP_ADATE"].ToString();
                txtPlanType.Value = dr["MP_MASHAPE"].ToString();
                txtBZ.Text = dr["MP_NOTE"].ToString();
                txtBZ.ToolTip = dr["MP_NOTE"].ToString();
                status = dr["MP_STATE"].ToString();//当前批材料计划的审核状态
                lblStatus.Text = status;
                lblMap.Text = dr["MP_MAP"].ToString();

                if (dr["MP_IFFAST"].ToString().Trim() == "1")
                {
                    chkiffast.Checked = true;
                }
                else
                {
                    chkiffast.Checked = false;
                }
                //审核图片
                if (status == "3" || status == "5" || status == "7" || status == "8")
                {
                    ImageVerify.Visible = true;
                }
                else
                {
                    ImageVerify.Visible = false;
                }

                this.RblOfThreeStateConfirm(status, dr["MP_CHECKLEVEL"].ToString());
                level = dr["MP_CHECKLEVEL"].ToString();//审核级数

                rblSHJS.SelectedIndex = Convert.ToInt16(dr["MP_CHECKLEVEL"].ToString()) - 1;
                tsa_id.Text = dr["MP_ENGID"].ToString();
                proid.Value = dr["MP_PJID"].ToString();
                editorid.Value = dr["MP_SUBMITID"].ToString();
                firstid.Value = dr["MP_REVIEWA"].ToString();
                secondid.Value = dr["MP_REVIEWB"].ToString();
                thirdid.Value = dr["MP_REVIEWC"].ToString();
                #endregion

                #region 状态判断
                if (Request.QueryString["id"] != null)  //查看进入(针对提交人)
                {
                    if ((status == "1" || status == "0") && level == "3")//未指定审核人
                    {
                        rblSHJS.SelectedIndex = 1; //默认二级审核
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

                        txtBZ.Enabled = true;//备注
                        txtBZ.BorderStyle = BorderStyle.Solid;

                    }
                    else
                    {
                        txtBZ.ReadOnly = true;//备注
                        txtBZ.BorderStyle = BorderStyle.None;

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

                if (Request.QueryString["mp_audit_id"] != null)//审核人进入
                {
                    rblSHJS.Enabled = false;
                    txtBZ.ReadOnly = true;
                    txtBZ.BorderStyle = BorderStyle.None;
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
            dr.Close();
            this.GetCollList();
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

        /// <summary>
        /// 获取该批汇总的数据
        /// </summary>
        private void GetCollList()
        {
            this.InitList();
            DataTable dt = new DataTable();
            if (view_tablename == "View_TM_MPCHANGE")   //变更汇总
            {

            }
            else
            {
                try
                {
                    SqlConnection sqlConn = new SqlConnection();
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                    DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "TM_MP_COLLECT");
                    DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Table_Name", view_tablename, SqlDbType.VarChar, 1000);
                    DBCallCommon.AddParameterToStoredProc(sqlCmd, "@PiHao", mp_id, SqlDbType.VarChar, 1000);

                    dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                    sqlConn.Close();
                }
                catch (Exception)
                {
                    throw;
                }

            }
            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if ((rblfirst.SelectedIndex == 1 && first_opinion.Text.Trim() == "") || (rblsecond.SelectedIndex == 1 && second_opinion.Text.Trim() == "") || (rblthird.SelectedIndex == 1 && third_opinion.Text.Trim() == ""))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您的审核意见为【不同意】,请填写意见！！！');", true);
                return;
            }

            if (this.AuditPowerCheck())
            {
                this.InitList();
                status = lblStatus.Text;
                int checklevel = Convert.ToInt16(rblSHJS.SelectedValue);
                List<string> list_sql = new List<string>();
                string sqltext = "";
                if (this.CheckSelect(checklevel))
                {
                    // 提交状态(更新审核表及原始数据中的材料计划状态)
                    //其他状态：更新审核表、审核通过或驳回时更新原始数据、通过时插入采购部

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
                            sqltext = "update " + mptable + " set MP_REVIEWA='" + firstid.Value + "',MP_STATE='2',MP_CHECKLEVEL='" + rblSHJS.SelectedValue + "',MP_SUBMITTM='" + reviewdate + "',MP_NOTE='" + txtBZ.Text.Trim() + "' where MP_ID='" + mp_id + "'";
                            list_sql.Add(sqltext);
                            if (!mp_no.Text.Contains(" MPQX/")) //不是取消的批号，要对原始数据的审核状态修改，取消的不进行原始数据修改
                            {
                                sqltext = "update " + strtable + " set BM_MPREVIEW='1' where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in(select MP_NEWXUHAO from " + view_tablename + " where MP_PID='" + mp_id + "')";
                                list_sql.Add(sqltext);
                            }
                            flag = "0";
                            break;
                            #endregion
                        case "2":
                            #region 一级审核状态
                            if (checklevel == 1)
                            {
                                if (rblfirst.SelectedIndex == 0)//同意-审核通过
                                {
                                    sqltext = "update " + mptable + " set MP_ADATE='" + reviewdate + "',MP_STATE='8',MP_REVIEWAADVC='" + first_opinion.Text.Trim() + "',MP_REVIEWATIME='" + reviewdate + "' where MP_ID='" + mp_id + "'";
                                    list_sql.Add(sqltext);
                                    if (!mp_no.Text.Contains(" MPQX/"))  //正常审核和变更审核通过修改原始数据审核状态和变更状态
                                    {
                                        sqltext = "update " + strtable + " set BM_MPREVIEW='3',BM_MPSTATUS='0' where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in(select MP_NEWXUHAO from " + view_tablename + " where MP_PID='" + mp_id + "')";//更新原始数据
                                        list_sql.Add(sqltext);
                                    }
                                    else  //取消审核通过后将原始数据的提交、变更、审核状态更新为0
                                    {
                                        sqltext = " update " + strtable + " set BM_MPREVIEW='0',BM_MPSTATUS='0',BM_MPSTATE='0' where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in(select MP_NEWXUHAO from " + view_tablename + " where MP_PID='" + mp_id + "')";
                                        list_sql.Add(sqltext);
                                    }

                                    this.GoToPurshingDep(list_sql);
                                    this.UpdateMainPlanMp(list_sql);
                                    flag = "3";
                                }
                                else if (rblfirst.SelectedIndex == 1)//一级驳回
                                {
                                    sqltext = "update " + mptable + " set MP_ADATE='" + reviewdate + "',MP_STATE='" + rblfirst.SelectedValue + "',MP_REVIEWAADVC='" + first_opinion.Text.Trim() + "',MP_REVIEWATIME='" + reviewdate + "' where MP_ID='" + mp_id + "'";
                                    list_sql.Add(sqltext);

                                    if (!mp_no.Text.Contains(" MPQX/"))  //正常审核和变更审核通过修改原始数据审核状态和变更状态
                                    {
                                        sqltext = "update " + strtable + " set BM_MPREVIEW='2' where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in(select MP_NEWXUHAO from " + view_tablename + " where MP_PID='" + mp_id + "')";
                                        list_sql.Add(sqltext);
                                    }
                                    flag = "2";
                                }
                            }
                            else if (checklevel > 1)
                            {
                                if (rblfirst.SelectedIndex == 0)//同意-继续下推审核
                                {
                                    sqltext = "update " + mptable + " set MP_STATE='" + rblfirst.SelectedValue + "',MP_REVIEWAADVC='" + first_opinion.Text.Trim() + "',MP_REVIEWATIME='" + reviewdate + "',MP_REVIEWB='" + secondid.Value + "' where MP_ID='" + mp_id + "'";
                                    list_sql.Add(sqltext);
                                    flag = "1";
                                }
                                else if (rblfirst.SelectedIndex == 1)//一级驳回
                                {
                                    sqltext = "update " + mptable + " set MP_ADATE='" + reviewdate + "',MP_STATE='" + rblfirst.SelectedValue + "',MP_REVIEWAADVC='" + first_opinion.Text.Trim() + "',MP_REVIEWATIME='" + reviewdate + "' where MP_ID='" + mp_id + "'";
                                    list_sql.Add(sqltext);

                                    if (!mp_no.Text.Contains(" MPQX/"))  //正常审核和变更审核通过修改原始数据审核状态和变更状态
                                    {
                                        sqltext = "update " + strtable + " set BM_MPREVIEW='2' where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in(select MP_NEWXUHAO from " + view_tablename + " where MP_PID='" + mp_id + "')";
                                        list_sql.Add(sqltext);
                                    }

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
                                    sqltext = "update " + mptable + " set MP_ADATE='" + reviewdate + "',MP_STATE='8',MP_REVIEWBADVC='" + second_opinion.Text.Trim() + "',MP_REVIEWBTIME='" + reviewdate + "' where MP_ID='" + mp_id + "'";
                                    list_sql.Add(sqltext);

                                    if (!mp_no.Text.Contains(" MPQX/"))  //正常审核和变更审核通过修改原始数据审核状态和变更状态
                                    {
                                        sqltext = "update " + strtable + " set BM_MPREVIEW='3',BM_MPSTATUS='0' where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in(select MP_NEWXUHAO from " + view_tablename + " where MP_PID='" + mp_id + "')";
                                        list_sql.Add(sqltext);
                                    }
                                    else  //取消审核通过后将原始数据的提交、变更、审核状态更新为0
                                    {
                                        sqltext = " update " + strtable + " set BM_MPREVIEW='0',BM_MPSTATUS='0',BM_MPSTATE='0' where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in(select MP_NEWXUHAO from " + view_tablename + " where MP_PID='" + mp_id + "')";
                                        list_sql.Add(sqltext);
                                    }
                                    this.UpdateMainPlanMp(list_sql);
                                    this.GoToPurshingDep(list_sql);
                                    flag = "3";
                                }
                                else if (rblsecond.SelectedIndex == 1)//驳回
                                {
                                    sqltext = "update " + mptable + " set MP_ADATE='" + reviewdate + "',MP_STATE='" + rblsecond.SelectedValue + "',MP_REVIEWBADVC='" + second_opinion.Text.Trim() + "',MP_REVIEWBTIME='" + reviewdate + "' where MP_ID='" + mp_id + "'";
                                    list_sql.Add(sqltext);

                                    if (!mp_no.Text.Contains(" MPQX/"))  //正常审核和变更审核通过修改原始数据审核状态和变更状态
                                    {
                                        sqltext = "update " + strtable + " set BM_MPREVIEW='2' where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in(select MP_NEWXUHAO from " + view_tablename + " where MP_PID='" + mp_id + "')";
                                        list_sql.Add(sqltext);
                                    }
                                    flag = "2";
                                }
                            }
                            else if (checklevel > 2)
                            {
                                if (rblsecond.SelectedIndex == 0)//同意-继续下推审核
                                {
                                    sqltext = "update " + mptable + " set MP_STATE='" + rblsecond.SelectedValue + "',MP_REVIEWBADVC='" + second_opinion.Text.Trim() + "',MP_REVIEWBTIME='" + reviewdate + "',MP_REVIEWC='" + thirdid.Value + "' where MP_ID='" + mp_id + "'";
                                    list_sql.Add(sqltext);
                                    flag = "1";
                                }
                                else if (rblsecond.SelectedIndex == 1)//二级驳回
                                {
                                    sqltext = "update " + mptable + " set MP_ADATE='" + reviewdate + "',MP_STATE='" + rblsecond.SelectedValue + "',MP_REVIEWBADVC='" + second_opinion.Text.Trim() + "',MP_REVIEWBTIME='" + reviewdate + "',MP_REVIEWC='" + thirdid.Value + "' where MP_ID='" + mp_id + "'";
                                    list_sql.Add(sqltext);

                                    if (!mp_no.Text.Contains(" MPQX/"))  //正常审核和变更审核通过修改原始数据审核状态和变更状态
                                    {
                                        sqltext = "update " + strtable + " set BM_MPREVIEW='2' where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in(select MP_NEWXUHAO from " + view_tablename + " where MP_PID='" + mp_id + "')";
                                        list_sql.Add(sqltext);
                                    }
                                    flag = "2";
                                }
                            }
                            break;
                            #endregion
                        case "6":
                            #region 三级审核状态
                            if (rblthird.SelectedIndex == 0)//通过
                            {
                                sqltext = "update " + mptable + " set MP_ADATE='" + reviewdate + "',MP_STATE='8',MP_REVIEWCADVC='" + third_opinion.Text.Trim() + "',MP_REVIEWCTIME='" + reviewdate + "' where MP_ID='" + mp_id + "'";
                                list_sql.Add(sqltext);

                                if (!mp_no.Text.Contains(" MPQX/"))  //正常审核和变更审核通过修改原始数据审核状态和变更状态
                                {
                                    sqltext = "update " + strtable + " set BM_MPREVIEW='3',BM_MPSTATUS='0' where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in(select MP_NEWXUHAO from " + view_tablename + " where MP_PID='" + mp_id + "')";
                                    list_sql.Add(sqltext);
                                }
                                else  //取消审核通过后将原始数据的提交、变更、审核状态更新为0
                                {
                                    sqltext = " update " + strtable + " set BM_MPREVIEW='0',BM_MPSTATUS='0',BM_MPSTATE='0' where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in(select MP_NEWXUHAO from " + view_tablename + " where MP_PID='" + mp_id + "')";
                                    list_sql.Add(sqltext);
                                }
                                this.UpdateMainPlanMp(list_sql);
                                this.GoToPurshingDep(list_sql);
                                flag = "3";
                            }
                            else if (rblthird.SelectedIndex == 1)//三级驳回
                            {
                                sqltext = "update " + mptable + " set MP_ADATE='" + reviewdate + "',MP_STATE='" + rblthird.SelectedValue + "',MP_REVIEWCADVC='" + third_opinion.Text.Trim() + "',MP_REVIEWCTIME='" + reviewdate + "' where MP_ID='" + mp_id + "'";
                                list_sql.Add(sqltext);

                                if (!mp_no.Text.Contains(" MPQX/"))  //正常审核和变更审核通过修改原始数据审核状态和变更状态
                                {
                                    sqltext = "update " + strtable + " set BM_MPREVIEW='2' where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in(select MP_NEWXUHAO from " + view_tablename + " where MP_PID='" + mp_id + "')";
                                    list_sql.Add(sqltext);
                                }
                                flag = "2";
                            }
                            break;
                            #endregion
                        default: break;
                    }
                    DBCallCommon.ExecuteTrans(list_sql);

                    #region 提示信息
                    string _emailto = "";
                    string _body;
                    string _subject;
                    if (flag == "0")
                    {
                        if (ckbMessage.Checked)
                        {
                            _emailto = DBCallCommon.GetEmailAddressByUserID(firstid.Value.Trim());
                            _body = "材料计划（BOM）审批任务:"
                                + "\r\n编    号：" + mp_no.Text.Trim()
                                + "\r\n项目名称：" + lab_proname.Text.Trim() + "_" + proid.Value.Trim() + ""
                                + "\r\n设备名称：" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0]
                                + "\r\n计划类别: " + txtPlanType.Value.Trim()
                                + "\r\n技 术 员: " + txt_editor.Text.Trim()
                                + "\r\n编制时间:" + txt_plandate.Text.Trim();

                            _subject = "您有新的【材料计划】需要审批，请及时处理（BOM下推计划）" + lab_proname.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0];
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核任务已提交，请等待审核结果...');if(window.parent!=null)window.parent.location.href='TM_Task_View.aspx?action=" + tsa_id.Text.Trim() + "';else{window.location.href='TM_Task_View.aspx?action=" + tsa_id.Text.Trim() + "'}", true);
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
                            _body = "材料计划（BOM）审批任务:"
                                        + "\r\n编    号：" + mp_no.Text.Trim()
                                        + "\r\n项目名称：" + lab_proname.Text.Trim() + "_" + proid.Value.Trim() + ""
                                        + "\r\n设备名称：" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0]
                                        + "\r\n计划类别: " + txtPlanType.Value.Trim()
                                        + "\r\n技 术 员: " + txt_editor.Text.Trim()
                                        + "\r\n编制时间:" + txt_plandate.Text.Trim();

                            _subject = "您有新的【材料计划】需要审批，请及时处理（BOM下推计划）: " + lab_proname.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0];
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
                            _body = "材料计划（BOM）审批已驳回:"
                                + "\r\n编    号：" + mp_no.Text.Trim()
                                + "\r\n项目名称：" + lab_proname.Text.Trim() + "_" + proid.Value.Trim() + ""
                                + "\r\n设备名称：" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0]
                                + "\r\n计划类别: " + txtPlanType.Value.Trim()
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
                            _subject = "材料计划（BOM）审批已驳回: " + lab_proname.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0];
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
                            _body = "材料计划（BOM）审批已下推采购:"
                                + "\r\n编    号：" + mp_no.Text.Trim()
                                + "\r\n项目名称：" + lab_proname.Text.Trim() + "_" + proid.Value.Trim() + ""
                                + "\r\n设备名称：" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0]
                                + "\r\n计划类别: " + txtPlanType.Value.Trim()
                                + "\r\n技 术 员: " + txt_editor.Text.Trim()
                                + "\r\n编制时间:" + txt_plandate.Text.Trim()

                                + "\r\n\r\n为了避免给您的后续工作带来不便，请您确认计划的正确性，核实标识、备注等信息是否明确、完整，如有问题请及时通知采购将计划驳回";

                            _subject = "材料计划（BOM）审批已下推采购: " + lab_proname.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0];
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }



                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核通过，材料计划已提交采购!');location.href='TM_Leader_Task.aspx';", true);
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您已完成审核或无权审核该任务！！！');", true);
                return;
            }
        }
        /// <summary>
        /// 主生产计划预警状态变更【材料计划完成】
        /// </summary>
        /// <param name="list_sql"></param>
        private void UpdateMainPlanMp(List<string> list_sql)
        {

            sqlText = "update TBMP_MAINPLANDETAIL set MP_STATE=2,MP_ACTURALTIME='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where MP_ENGID='" + tsa_id.Text.Trim() + "' and MP_PLNAME='材料计划'";
            list_sql.Add(sqlText);
            sqlText = "update TBMP_MAINPLANDETAIL set MP_STATE=1 where MP_ENGID='" + tsa_id.Text.Trim() + "' and MP_TYPE='采购周期'";
            list_sql.Add(sqlText);

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
        /// 下推采购部
        /// </summary>
        /// <param name="list"></param>
        private void GoToPurshingDep(List<string> list)
        {
            if (mptable == "TBPM_MPFORALLRVW")
            {
                this.pushDown(list);//正常材料计划审核通过下推采购部
            }
            else if (mptable == "TBPM_MPCHANGERVW")
            {
                this.mpVar(list);//变更材料计划审核通过下推至采购部的变量
            }
        }

        /// <summary>
        /// 正常材料计划下推采购部
        /// </summary>
        private void pushDown(List<string> list)
        {

            DataTable dt = new DataTable();
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "TM_MP_COLLECT");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Table_Name", view_tablename, SqlDbType.VarChar, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@PiHao", mp_no.Text, SqlDbType.VarChar, 1000);

                dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }

            string iffast = "";
            if (chkiffast.Checked)
            {
                iffast = "1";
            }
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //采购需求计划表(TBPC_PURCHASEPLAN) --明细
                    if (dt.Rows[i]["MP_MASHAPE"].ToString() == "板")
                    {
                        if (dt.Rows[i]["MP_FIXEDSIZE"].ToString() == "N")
                        {
                            if (dt.Rows[i]["MP_PURCUNIT"].ToString().Contains("平米"))
                            {
                                techunit = dt.Rows[i]["CONVERTRATE"].ToString() == "1" ? "kg" : "T";

                                sqlText = "if not exists(select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + dt.Rows[i]["MP_TRACKNUM"].ToString() + "') ";
                                sqlText += " begin ";
                                sqlText += "insert into  TBPC_PURCHASEPLAN";
                                sqlText += "(PUR_PCODE,PUR_PTCODE,PUR_MARID,PUR_LENGTH,PUR_WIDTH,PUR_NUM,PUR_FZNUM,PUR_KEYCOMS,PUR_NOTE,PUR_TIMEQ,PUR_MASHAPE,PUR_TUHAO,PUR_RPNUM,PUR_RPFZNUM,PUR_TECUNIT,PUR_IFFAST)";
                                sqlText += " values('" + mp_no.Text + "','" + dt.Rows[i]["MP_TRACKNUM"].ToString() + "','" + dt.Rows[i]["MP_MARID"].ToString() + "',null,";
                                sqlText += "null,'" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "'," + Convert.ToDecimal(Convert.ToDouble(dt.Rows[i]["MP_WEIGHT"]) / Convert.ToDouble(dt.Rows[i]["CONVERTRATE"])) + ",'" + dt.Rows[i]["MP_KEYCOMS"].ToString() + "','" + dt.Rows[i]["MP_ALLBEIZHU"].ToString() + "','" + dt.Rows[i]["MP_TIMERQ"].ToString() + "','" + dt.Rows[i]["MP_MASHAPE"] + "','" + dt.Rows[i]["MP_TUHAO"].ToString() + "','" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "'," + Convert.ToDouble(dt.Rows[i]["MP_WEIGHT"]) / Convert.ToDouble(dt.Rows[i]["CONVERTRATE"]) + ",'" + techunit + "','" + iffast + "')";
                                sqlText += " end ";
                            }
                            else
                            {

                                string convertWeight = (Convert.ToDouble(dt.Rows[i]["MP_WEIGHT"]) / Convert.ToDouble(dt.Rows[i]["CONVERTRATE"])).ToString("0.0000");
                                if (Convert.ToDouble(dt.Rows[i]["MP_WEIGHT"]) > 0 && convertWeight == "0.0000")
                                {
                                    convertWeight = "0.0001";
                                }
                                sqlText = "if not exists(select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + dt.Rows[i]["MP_TRACKNUM"].ToString() + "') ";
                                sqlText += " begin ";
                                sqlText += "insert into  TBPC_PURCHASEPLAN";
                                sqlText += "(PUR_PCODE,PUR_PTCODE,PUR_MARID,PUR_LENGTH,PUR_WIDTH,PUR_NUM,PUR_FZNUM,PUR_KEYCOMS,PUR_NOTE,PUR_TIMEQ,PUR_MASHAPE,PUR_TUHAO,PUR_RPNUM,PUR_RPFZNUM,PUR_TECUNIT,PUR_IFFAST)";
                                sqlText += " values('" + mp_no.Text + "','" + dt.Rows[i]["MP_TRACKNUM"].ToString() + "','" + dt.Rows[i]["MP_MARID"].ToString() + "',null,";
                                sqlText += "null," + convertWeight + ",'" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "','" + dt.Rows[i]["MP_KEYCOMS"].ToString() + "','" + dt.Rows[i]["MP_ALLBEIZHU"].ToString() + "','" + dt.Rows[i]["MP_TIMERQ"].ToString() + "','" + dt.Rows[i]["MP_MASHAPE"] + "','" + dt.Rows[i]["MP_TUHAO"].ToString() + "'," + convertWeight + ",'" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "','" + dt.Rows[i]["MP_TECHUNIT"].ToString() + "','" + iffast + "')";
                                sqlText += " end ";
                            }

                        }
                        else
                        {
                            if (dt.Rows[i]["MP_PURCUNIT"].ToString().Contains("平米"))
                            {
                                techunit = dt.Rows[i]["CONVERTRATE"].ToString() == "1" ? "kg" : "T";
                                sqlText = "if not exists(select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + dt.Rows[i]["MP_TRACKNUM"].ToString() + "') ";
                                sqlText += " begin ";
                                sqlText += "insert into  TBPC_PURCHASEPLAN";
                                sqlText += "(PUR_PCODE,PUR_PTCODE,PUR_MARID,PUR_LENGTH,PUR_WIDTH,PUR_NUM,PUR_FZNUM,PUR_KEYCOMS,PUR_NOTE,PUR_TIMEQ,PUR_MASHAPE,PUR_TUHAO,PUR_RPNUM,PUR_RPFZNUM,PUR_TECUNIT,PUR_IFFAST)";
                                sqlText += " values('" + mp_no.Text + "','" + dt.Rows[i]["MP_TRACKNUM"].ToString() + "','" + dt.Rows[i]["MP_MARID"].ToString() + "','" + dt.Rows[i]["MP_LENGTH"].ToString() + "',";
                                sqlText += "'" + dt.Rows[i]["MP_WIDTH"].ToString() + "','" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "'," + Convert.ToDouble(dt.Rows[i]["MP_WEIGHT"]) / Convert.ToDouble(dt.Rows[i]["CONVERTRATE"]) + ",'" + dt.Rows[i]["MP_KEYCOMS"].ToString() + "','" + dt.Rows[i]["MP_ALLBEIZHU"].ToString() + "','" + dt.Rows[i]["MP_TIMERQ"].ToString() + "','" + dt.Rows[i]["MP_MASHAPE"] + "','" + dt.Rows[i]["MP_TUHAO"].ToString() + "','" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "'," + Convert.ToDouble(dt.Rows[i]["MP_WEIGHT"]) / Convert.ToDouble(dt.Rows[i]["CONVERTRATE"]) + ",'" + techunit + "','" + iffast + "')";
                                sqlText += " end ";
                            }
                            else
                            {


                                string convertWeight = (Convert.ToDouble(dt.Rows[i]["MP_WEIGHT"]) / Convert.ToDouble(dt.Rows[i]["CONVERTRATE"])).ToString("0.0000");
                                if (Convert.ToDouble(dt.Rows[i]["MP_WEIGHT"]) > 0 && convertWeight == "0.0000")
                                {
                                    convertWeight = "0.0001";
                                }
                                sqlText = "if not exists(select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + dt.Rows[i]["MP_TRACKNUM"].ToString() + "') ";
                                sqlText += " begin ";
                                sqlText += "insert into  TBPC_PURCHASEPLAN";
                                sqlText += "(PUR_PCODE,PUR_PTCODE,PUR_MARID,PUR_LENGTH,PUR_WIDTH,PUR_NUM,PUR_FZNUM,PUR_KEYCOMS,PUR_NOTE,PUR_TIMEQ,PUR_MASHAPE,PUR_TUHAO,PUR_RPNUM,PUR_RPFZNUM,PUR_TECUNIT,PUR_IFFAST)";
                                sqlText += " values('" + mp_no.Text + "','" + dt.Rows[i]["MP_TRACKNUM"].ToString() + "','" + dt.Rows[i]["MP_MARID"].ToString() + "','" + dt.Rows[i]["MP_LENGTH"].ToString() + "',";
                                sqlText += "'" + dt.Rows[i]["MP_WIDTH"].ToString() + "'," + convertWeight + ",'" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "','" + dt.Rows[i]["MP_KEYCOMS"].ToString() + "','" + dt.Rows[i]["MP_ALLBEIZHU"].ToString() + "','" + dt.Rows[i]["MP_TIMERQ"].ToString() + "','" + dt.Rows[i]["MP_MASHAPE"] + "','" + dt.Rows[i]["MP_TUHAO"].ToString() + "'," + convertWeight + ",'" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "','" + dt.Rows[i]["MP_TECHUNIT"].ToString() + "','" + iffast + "')";
                                sqlText += " end ";
                            }


                        }
                    }
                    else if (dt.Rows[i]["MP_MASHAPE"].ToString() == "型" || dt.Rows[i]["MP_MASHAPE"].ToString() == "圆")
                    {
                        string convertWeight = (Convert.ToDouble(dt.Rows[i]["MP_WEIGHT"]) / Convert.ToDouble(dt.Rows[i]["CONVERTRATE"])).ToString("0.0000");
                        if (Convert.ToDouble(dt.Rows[i]["MP_WEIGHT"]) > 0 && convertWeight == "0.0000")
                        {
                            convertWeight = "0.0001";
                        }
                        string length = dt.Rows[i]["MP_LENGTH"].ToString() == "" ? "null" : dt.Rows[i]["MP_LENGTH"].ToString();

                        sqlText = "if not exists(select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + dt.Rows[i]["MP_TRACKNUM"].ToString() + "') ";
                        sqlText += " begin ";
                        sqlText += "insert into  TBPC_PURCHASEPLAN";
                        sqlText += "(PUR_PCODE,PUR_PTCODE,PUR_MARID,PUR_LENGTH,PUR_WIDTH,PUR_NUM,PUR_FZNUM,PUR_KEYCOMS,PUR_NOTE,PUR_TIMEQ,PUR_MASHAPE,PUR_TUHAO,PUR_RPNUM,PUR_RPFZNUM,PUR_TECUNIT,PUR_IFFAST)";
                        sqlText += " values('" + mp_no.Text + "','" + dt.Rows[i]["MP_TRACKNUM"].ToString() + "','" + dt.Rows[i]["MP_MARID"].ToString() + "'," + length + ",";
                        sqlText += "null,'" + convertWeight + "','" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "','" + dt.Rows[i]["MP_KEYCOMS"].ToString() + "','" + dt.Rows[i]["MP_ALLBEIZHU"].ToString() + "','" + dt.Rows[i]["MP_TIMERQ"].ToString() + "','" + dt.Rows[i]["MP_MASHAPE"] + "','" + dt.Rows[i]["MP_TUHAO"].ToString() + "','" + convertWeight + "','" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "','" + dt.Rows[i]["MP_TECHUNIT"].ToString() + "','" + iffast + "')";
                        sqlText += " end ";

                    }
                    else if (dt.Rows[i]["MP_MASHAPE"].ToString() == "采")
                    {
                        sqlText = "if not exists(select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + dt.Rows[i]["MP_TRACKNUM"].ToString() + "') ";
                        sqlText += " begin ";
                        sqlText += "insert into  TBPC_PURCHASEPLAN";
                        sqlText += "(PUR_PCODE,PUR_PTCODE,PUR_MARID,PUR_LENGTH,PUR_WIDTH,PUR_NUM,PUR_FZNUM,PUR_KEYCOMS,PUR_NOTE,PUR_TIMEQ,PUR_MASHAPE,PUR_TUHAO,PUR_RPNUM,PUR_RPFZNUM,PUR_TECUNIT,PUR_IFFAST)";
                        sqlText += " values('" + mp_no.Text + "','" + dt.Rows[i]["MP_TRACKNUM"].ToString() + "','" + dt.Rows[i]["MP_MARID"].ToString() + "',null,";
                        sqlText += "null,'" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "','" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "','" + dt.Rows[i]["MP_KEYCOMS"].ToString() + "','" + dt.Rows[i]["MP_ALLBEIZHU"].ToString() + "','" + dt.Rows[i]["MP_TIMERQ"].ToString() + "','" + dt.Rows[i]["MP_MASHAPE"] + "','" + dt.Rows[i]["MP_TUHAO"].ToString() + "','" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "','" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "','" + dt.Rows[i]["MP_TECHUNIT"].ToString() + "','" + iffast + "')";
                        sqlText += " end ";
                    }
                    else
                    {
                        if (dt.Rows[i]["MP_PURCUNIT"].ToString().Contains("kg"))
                        {
                            sqlText = "if not exists(select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + dt.Rows[i]["MP_TRACKNUM"].ToString() + "') ";
                            sqlText += " begin ";
                            sqlText += "insert into  TBPC_PURCHASEPLAN";
                            sqlText += "(PUR_PCODE,PUR_PTCODE,PUR_MARID,PUR_LENGTH,PUR_WIDTH,PUR_NUM,PUR_FZNUM,PUR_KEYCOMS,PUR_NOTE,PUR_TIMEQ,PUR_MASHAPE,PUR_TUHAO,PUR_RPNUM,PUR_RPFZNUM,PUR_TECUNIT,PUR_IFFAST)";
                            sqlText += " values('" + mp_no.Text + "','" + dt.Rows[i]["MP_TRACKNUM"].ToString() + "','" + dt.Rows[i]["MP_MARID"].ToString() + "',null,";
                            sqlText += "null,'" + Convert.ToDouble(dt.Rows[i]["MP_WEIGHT"]) + "','" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "','" + dt.Rows[i]["MP_KEYCOMS"].ToString() + "','" + dt.Rows[i]["MP_ALLBEIZHU"].ToString() + "','" + dt.Rows[i]["MP_TIMERQ"].ToString() + "','" + dt.Rows[i]["MP_MASHAPE"] + "','" + dt.Rows[i]["MP_TUHAO"].ToString() + "','" + Convert.ToDouble(dt.Rows[i]["MP_WEIGHT"]) + "','" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "','" + dt.Rows[i]["MP_TECHUNIT"].ToString() + "','" + iffast + "')";
                            sqlText += " end ";
                        }
                        else
                        {
                            sqlText = "if not exists(select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + dt.Rows[i]["MP_TRACKNUM"].ToString() + "') ";
                            sqlText += " begin ";
                            sqlText += "insert into  TBPC_PURCHASEPLAN";
                            sqlText += "(PUR_PCODE,PUR_PTCODE,PUR_MARID,PUR_LENGTH,PUR_WIDTH,PUR_NUM,PUR_FZNUM,PUR_KEYCOMS,PUR_NOTE,PUR_TIMEQ,PUR_MASHAPE,PUR_TUHAO,PUR_RPNUM,PUR_RPFZNUM,PUR_TECUNIT,PUR_IFFAST)";
                            sqlText += " values('" + mp_no.Text + "','" + dt.Rows[i]["MP_TRACKNUM"].ToString() + "','" + dt.Rows[i]["MP_MARID"].ToString() + "',null,";
                            sqlText += "null,'" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "','" + Convert.ToDouble(dt.Rows[i]["MP_WEIGHT"]) + "','" + dt.Rows[i]["MP_KEYCOMS"].ToString() + "','" + dt.Rows[i]["MP_ALLBEIZHU"].ToString() + "','" + dt.Rows[i]["MP_TIMERQ"].ToString() + "','" + dt.Rows[i]["MP_MASHAPE"] + "','" + dt.Rows[i]["MP_TUHAO"].ToString() + "','" + dt.Rows[i]["MP_YONGLIANG"].ToString() + "','" + Convert.ToDouble(dt.Rows[i]["MP_WEIGHT"]) + "','kg','" + iffast + "')";
                            sqlText += " end ";
                        }

                    }

                    list.Add(sqlText);
                }
                //查询部门负责人
                string sql = "select ST_ID from View_PERSONS where ST_ID='67'";
                SqlDataReader reader = DBCallCommon.GetDRUsingSqlText(sql);
                string fuzeren = "";
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fuzeren = reader.GetValue(0).ToString();
                    }
                }
                reader.Close();

                //采购计划占用审核(TBPC_PCHSPLANRVW) --总表
                sqlText = "if not exists(select * from TBPC_PCHSPLANRVW where PR_SHEETNO='" + mp_no.Text + "') ";
                sqlText += " begin ";
                sqlText += "INSERT INTO TBPC_PCHSPLANRVW(PR_SHEETNO,PR_PJID,PR_ENGID,PR_DEPID,PR_SQREID,PR_SQTIME,PR_FZREID,PUR_MASHAPE,PR_NOTE,PR_MAP,PR_CHILDENGNAME) ";
                sqlText += " values('" + mp_no.Text + "','" + lab_proname.Text.Trim() + "',";
                sqlText += "'" + tsa_id.Text.Trim() + "','03',";
                sqlText += "'" + editorid.Value + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + fuzeren + "','','" + txtBZ.Text.Trim() + "','" + lblMap.Text.Trim() + "','" + lab_engname.Text.Trim() + "')";
                sqlText += " end";
                list.Add(sqlText);
            }
        }

        /// <summary>
        /// 变更材料的变量(变更审核通过)
        /// </summary>
        private void mpVar(List<string> list)
        {
            string pi_mptype = "";
            //查询某变更批号下对应的材料类别
            string sql_pi_mptype = "select MP_MASHAPE from TBPM_MPCHANGERVW where MP_ID='" + mp_no.Text.Trim() + "'";
            SqlDataReader dr_pi_mptype = DBCallCommon.GetDRUsingSqlText(sql_pi_mptype);

            if (dr_pi_mptype.HasRows)
            {
                dr_pi_mptype.Read();
                pi_mptype = dr_pi_mptype["MP_MASHAPE"].ToString();
                dr_pi_mptype.Close();
            }

            sqlText = "exec TM_MPVerify '" + mp_no.Text + "','" + pi_mptype + "'"; //将变更信息插入采购需用计划和变更表
            list.Add(sqlText);

            sqlText = "exec TM_MPBGOperate '" + mp_no.Text + "','" + strtable + "'";
            list.Add(sqlText);

        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnreturn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "try{history.go(-2);}catch(err){history.go(-1);}", true);
        }

        private double sum = 0;
        private double sumnum = 0;
        /// <summary>
        /// 该批数据汇总
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                //sum += Convert.ToDouble(e.Row.Cells[13].Text == "" ? "0" : e.Row.Cells[13].Text);
                //sumnum += Convert.ToDouble(e.Row.Cells[14].Text == "" ? "0" : e.Row.Cells[14].Text);
                string state = ((HtmlInputHidden)e.Row.FindControl("state")).Value;
                if (state=="1")
                {
                    e.Row.BackColor = Color.Yellow;
                    e.Row.Attributes.Add("title", "该条计划已被驳回");
                }
                else
                {
                    for (int i = 4; i < e.Row.Cells.Count; i++)//获取总列数
                    {
                        //如果是数据行则添加title
                        e.Row.Attributes["style"] = "Cursor:hand";
                        e.Row.Cells[i].Attributes.Add("title", "双击查看计划明细");
                    }
                }

           
                string tracknum_table = e.Row.Cells[e.Row.Cells.Count - 2].Text.Trim() + "$" + view_tablename;
                // 双击，设置 dbl_click=true，以取消单击响应
                e.Row.Attributes["ondblclick"] = String.Format("javascript:dbl_click=true;return Dblclik_ShowDetail('" + tracknum_table + "');");
            }
        }
        /// <summary>
        /// 一级审核意见
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblfirst_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblSHJS.SelectedValue == "2")
            {
                if (rblfirst.SelectedIndex == 0)
                {
                    hlSelect2.Visible = true;
                    first_opinion.Text = "同意";
                }
                else
                {
                    hlSelect2.Visible = false;
                    first_opinion.Text = "";
                }
            }
        }
        /// <summary>
        /// 二级审核意见
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblsecond_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblSHJS.SelectedValue == "3")
            {
                if (rblsecond.SelectedIndex == 0)
                {
                    hlSelect3.Visible = true;
                    second_opinion.Text = "同意";
                }
                else
                {
                    hlSelect3.Visible = false;
                    second_opinion.Text = "";
                }
            }
        }
        /// <summary>
        /// 三级审核意见
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblthird_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblthird.SelectedIndex == 0)
            {
                third_opinion.Text = "同意";
            }
            else
            {
                third_opinion.Text = "";
            }
        }

        #region ShowModual
        [System.Web.Services.WebMethodAttribute(),
           System.Web.Script.Services.ScriptMethodAttribute()]
        public static string GetAOGDynamicContent(string contextKey)
        {
            StringBuilder sTemp = new StringBuilder();
            sTemp.Append("<table style='background-color:#f3f3f3; border: #B9D3EE 3px solid;font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>");
            sTemp.Append("<tr><td colspan='6' style='background-color:#B9D3EE; color:white;'><b>计划跟踪号" + contextKey + "到货情况</b></td>");
            sTemp.Append("<td style='background-color:#B9D3EE; color:white;' align='right'  valign='middle'><a onclick='document.body.click(); return false;' style='cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a></td></tr>");

            string sql_gotopur = "select ptcode from View_TBPC_PURCHASEPLAN where ptcode='" + contextKey + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_gotopur);
            if (dr.HasRows)
            {
                string sql = "select '采购计划量' as RowName,length,width,sum(num) as num,marunit,sum(fznum) as fznum,marfzunit from View_TBPC_PURCHASEPLAN where ptcode='" + contextKey + "' group by length,width,marunit,marfzunit " +
                    " union all " +
                    "select '变更减少量' as RowName,length,width,chnum,unit,chfznum,fzunit from View_TBPC_MPTEMPCHANGE where chptcode='" + contextKey + "'" +
                    " union all " +
                    "select '计划占用量' as RowName,detaillength,detailwidth,detailnum,detailunit,detailfznum,detailfzunit from  View_TBPC_MARSTOUSEALLDETAIL where ptcode='" + contextKey + "'" +
                    " union all " +
                    "select '计划代用量' as RowName,length,width,num,marcgunit,fznum,fzunit from View_TBPC_MARREPLACE_total_all_detail where  ptcode='" + contextKey + "'" +
                    " union all " +
                    "select '订单计划量' as RowName,length,width,num,marunit,fznum,marfzunit from View_TBPC_PURCHASEPLAN where ptcode='" + contextKey + "' and PUR_CSTATE='0'" +
                    " union all " +
                    "select '订单执行量' as RowName,length,width,zxnum,marunit,zxfznum,marfzunit from View_TBPC_PURORDERDETAIL_PLAN_MPLAN where ptcode='" + contextKey + "'" +
                    " union all " +
                    "select '订单到货量' as RowName,length,width,recgdnum,marunit,recgdfznum,marfzunit from View_TBPC_PURORDERDETAIL_PLAN_MPLAN where ptcode='" + contextKey + "'";
                DataTable dt_sql = DBCallCommon.GetDTUsingSqlText(sql);

                //订单状态
                string sql_order_state = " select '订单状态',case when isnull(sum(num),0)=0 then '未下推订单' when sum(recgdnum)=0 then'未到货' when sum(recgdnum)-sum(num)>=0 then '全部到货'  when sum(recgdnum)-sum(num)<0 then '部分到货'  end from View_TBPC_PURORDERDETAIL_PLAN_MPLAN where ptcode='" + contextKey + "'";
                DataTable dt_sql_order_state = DBCallCommon.GetDTUsingSqlText(sql_order_state);
                if (dt_sql.Rows.Count > 0)
                {

                    sTemp.Append("<tr><td><b></b></td>");
                    sTemp.Append("<td align='center'><b>长(mm)</b></td>");
                    sTemp.Append("<td align='center'><b>宽(mm)</b></td>");
                    sTemp.Append("<td align='center'><b>数量</b></td>");
                    sTemp.Append("<td align='center'><b>单位</b></td>");
                    sTemp.Append("<td align='center'><b>辅助数量</b></td>");
                    sTemp.Append("<td align='center'><b>辅助单位</b></td></tr>");

                    for (int i = 0; i < dt_sql.Rows.Count; i++)
                    {
                        sTemp.Append("<tr style='border-width:1px;border-color:Black;'>");
                        for (int j = 0; j < dt_sql.Columns.Count; j++)
                        {
                            if (j == 0)
                            {
                                sTemp.Append("<td  style='white-space:pre;' align='center'><b>" + dt_sql.Rows[i][j].ToString() + "</b></td>");
                            }
                            else
                            {
                                sTemp.Append("<td  style='white-space:pre;' align='center'>" + dt_sql.Rows[i][j].ToString() + "</td>");
                            }
                        }
                        sTemp.Append("</tr>");
                    }

                    if (dt_sql_order_state.Rows.Count == 0)
                    {
                        sTemp.Append("<tr style='border-width:1px'>");
                        sTemp.Append("<td  style='white-space:pre;' align='center'><b>订单状态</b></td>");
                        sTemp.Append("<td  style='white-space:pre;color:Red;' align='center' colspan='6'><b>订单未生成</b></td></tr>");
                    }
                    else
                    {
                        sTemp.Append("<tr style='border-width:1px'>");
                        sTemp.Append("<td  style='white-space:pre;' align='center'><b>" + dt_sql_order_state.Rows[0][0].ToString() + "</b></td>");
                        sTemp.Append("<td  style='white-space:pre;color:Red;' align='center' colspan='6'><b>" + dt_sql_order_state.Rows[0][1].ToString() + "</b></td></tr>");
                    }
                }
            }
            else
            {
                sTemp.Append("<tr><td colspan='7'><i>未下推采购...</i></td></tr>");
            }
            sTemp.Append("</table>");

            return sTemp.ToString();
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Programmatically reference the PopupControlExtender
                PopupControlExtender pce = e.Row.FindControl("PopupControlExtender1") as PopupControlExtender;
                // Set the BehaviorID
                string behaviorID = string.Concat("pce", e.Row.RowIndex);
                pce.BehaviorID = behaviorID;

                // Programmatically reference the Image control
                HyperLink hpl = (HyperLink)e.Row.FindControl("hplAOG");

                // Add the clie nt-side attributes (onmouseover & onmouseout)
                string OnMouseOverScript = string.Format("$find('{0}').showPopup();", behaviorID);
                string OnMouseOutScript = string.Format("$find('{0}').hidePopup();", behaviorID);

                hpl.Attributes.Add("onmouseover", OnMouseOverScript);
                hpl.Attributes.Add("onmouseout", OnMouseOutScript);
            }
        }
        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            this.GetCollList();
            if (GridView1.Rows.Count > 0)
            {
                NoDataPanel.Visible = false;
            }
            else
            {
                NoDataPanel.Visible = true;
            }
        }
        /// <summary>
        /// 显示订单状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOrderStateShow_OnClick(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count > 0)
            {
                string tracknum;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    tracknum = GridView1.Rows[i].Cells[16].Text;
                    string sql_gotopur = "select ptcode from View_TBPC_PURCHASEPLAN where ptcode='" + tracknum + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_gotopur);
                    if (dr.HasRows)
                    {
                        //订单状态
                        string sql_order_state = " select '订单状态',case when isnull(sum(num),0)=0 then '未下推订单'  when sum(recgdnum)=0 then '未到货' when sum(recgdnum)-sum(num)>=0 then '全部到货' when sum(recgdnum)-sum(num)<0 then '部分到货' end  as OrderState from View_TBPC_PURORDERDETAIL_PLAN_MPLAN where ptcode='" + tracknum + "'";
                        DataTable dt_sql_order_state = DBCallCommon.GetDTUsingSqlText(sql_order_state);
                        if (dt_sql_order_state.Rows.Count > 0)
                        {
                            ((Label)GridView1.Rows[i].FindControl("Label1")).Text = dt_sql_order_state.Rows[0]["OrderState"].ToString();
                            if (dt_sql_order_state.Rows[0]["OrderState"].ToString() != "全部到货")
                            {
                                ((Label)GridView1.Rows[i].FindControl("Label1")).ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                    else
                    {
                        ((Label)GridView1.Rows[i].FindControl("Label1")).Text = "未下推采购";
                        ((Label)GridView1.Rows[i].FindControl("Label1")).ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }


        /// <summary>
        /// 返回变更批的实际物料需求
        /// </summary>
        /// <param name="bglotnum"></param>
        /// <returns></returns>
        private DataTable GetCurLotIncRedQutyOfMar(string bglotnum)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "PRO_TM_GetIncRedQtyOfMar");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Change_LotNum", bglotnum, SqlDbType.VarChar, 3000);
                dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
    }
}
