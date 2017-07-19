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


namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Out_Source_Audit : System.Web.UI.Page
    {
        #region
        string sqlText;
        string strtable;
        string viewtable;
        string auditlist;
        string wxcode;
        string wxnum;
        string viewaudit;
        string status;
        string level; 
        string[] fields;
        string[] cols;
        protected static  string OSL_NO;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            InVar();
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            if (!IsPostBack)
            {
                GetOutVerifyData();
            }
        }
        private void InVar()
        {
            string out_verifyno = Request.QueryString["id"];
            string out_verifyid = Request.QueryString["ost_audit_id"];
            if (out_verifyno != null)
            {
                OSL_NO = out_verifyno;   //正常
            }
            else
            {
                OSL_NO = out_verifyid;
            }
            fields = OSL_NO.Split('.');
            cols = fields[1].ToString().Split('/');
            if (cols[0].ToString().Length > 6)
            {
                auditlist = "TBPM_OUTSCHANGERVW";
                viewaudit = "View_TM_OUTSCHANGERVW";
                wxcode = "OST_CHANGECODE";  //记录变更总表的变更批号
                viewtable = "View_TM_OUTSCHANGE";
                wxnum = "OST_CHANGECODE";
                hpView.Visible = false;
                GridView1.Columns[1].Visible = true;

                lblNote.Text = "变更备注:";
                lblNote.Font.Bold = true;
                lblNote.ForeColor = System.Drawing.Color.Red;

            }
            else
            {
                auditlist = "TBPM_OUTSOURCETOTAL";
                viewaudit = "View_TM_OUTSOURCETOTAL";
                wxcode = "OST_OUTSOURCENO";    //记录外协总表的批号
                viewtable = "View_TM_OUTSOURCELIST";
                wxnum = "OSL_OUTSOURCENO";
                hpView.Visible = false;
                GridView1.Columns[1].Visible = false;

                lblNote.Text = "备注:";
            }
        }

        /// <summary>
        /// 审核权限检查
        /// </summary>
        /// <returns></returns>
        private bool AuditPowerCheck()
        {
            bool retVal = true;
            if (Request.QueryString["ost_audit_id"] != null)
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

        private void GetOutVerifyData()
        {
            btnsubmit.Text = "提 交";
            sqlText = "select OST_OUTSOURCENO,OST_PJNAME,OST_ENGNAME,OST_SUBMITERNM,";
            sqlText += "OST_MDATE,OST_ADATE,OST_REVIEWERANM,OST_REVIEWAADVC,";
            sqlText += "OST_REVIEWERBNM,OST_REVIEWBADVC,OST_REVIEWERCNM,OST_REVIEWCADVC,";
            sqlText += "OST_REVIEWADATE,OST_REVIEWBDATE,OST_REVIEWCDATE,OST_STATE,OST_ENGID,";
            sqlText += "OST_SUBMITER,OST_REVIEWERA,OST_PJID,OST_ENGTYPE,OST_CHECKLEVEL,OST_NOTE,OST_REVIEWERB,OST_REVIEWERC,OST_OUTTYPE ";
            sqlText += " from "+viewaudit+" where OST_OUTSOURCENO='" + OSL_NO + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            #region
            if (dr.Read())
            {
                osl_no1.Text = dr[0].ToString();
                lab_proname.Text = dr[1].ToString();
                proname.Text = dr[1].ToString();
                lab_engname.Text = dr[2].ToString();
                engname.Text = dr[2].ToString();
                txt_editor.Text = dr[3].ToString();
                txt_plandate.Text = dr[4].ToString();
                plandate.Text = dr[4].ToString();
                txt_approval.Text = dr[5].ToString();
                txt_first.Text = dr[6].ToString();
                first_opinion.Text = dr[7].ToString();
                txt_second.Text = dr[8].ToString();
                second_opinion.Text=dr[9].ToString();
                txt_third.Text = dr[10].ToString();
                third_opinion.Text = dr[11].ToString();
                first_time.Text = dr[12].ToString();
                second_time.Text = dr[13].ToString();
                third_time.Text = dr[14].ToString();
                status = dr[15].ToString();
                tsa_id.Text = dr[16].ToString();
                editorid.Value = dr[17].ToString();
                firstid.Value = dr[18].ToString();
                proid.Value = dr[19].ToString();
                eng_type.Value = dr[20].ToString();
                rblSHJS.SelectedValue = dr[21].ToString();
                level = dr[21].ToString();
                txtBZ.Text = dr["OST_NOTE"].ToString();
                ViewState["status"] = dr[15].ToString();
                lblStatus.Text = dr[15].ToString();
                ViewState["level"] = dr[21].ToString();
                secondid.Value = dr["OST_REVIEWERB"].ToString();
                thirdid.Value = dr["OST_REVIEWERC"].ToString();

                txt_PlanType.Value = dr["OST_OUTTYPE"].ToString();

                if (status == "1")
                {
                    hlSelect1.Visible = true;
                    rblSHJS.Enabled = true;
                }
                else
                {
                    rblSHJS.Enabled = false;
                    isenabled(status, level);
                }
                this.RblOfThreeStateConfirm(status, level); //确定每个审核意见的值
            }
            dr.Close();
            #endregion
            this.BindDataOutDetail();
        }
        /// <summary>
        /// 绑定某批号下外协明细
        /// </summary>
        private void BindDataOutDetail()
        {
            sqlText = "select * from " + viewtable + " ";
            sqlText += "where OSL_OUTSOURCENO='" + OSL_NO + "'";
            if (ddlQueryType.SelectedIndex != 0)
            {
                sqlText += " and " + ddlQueryType.SelectedValue + " like '%" + txtQueryText.Text.Trim() + "%'";
            }
            sqlText += "order by dbo.f_FormatSTR(OSL_OLDXUHAO,'.')";
            DBCallCommon.BindGridView(GridView1, sqlText);  //绑定数据

            if (viewaudit == "View_TM_OUTSCHANGERVW")
            {
                SmartGridView1.DataSource = this.GetChgMarofReal(OSL_NO);
                SmartGridView1.DataBind();

                lblAfter.Visible = true;
                lblBefore.Visible = true;
                SmartGridView1.Visible = true;

                if (SmartGridView1.Rows.Count > 0)
                {
                    Panel2.Visible = false;
                }
                else
                {
                    Panel2.Visible = true;
                }
            }
            else
            {
                lblAfter.Visible = false;
                lblBefore.Visible = false;
                SmartGridView1.Visible = false;
            }

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

        private void isenabled(string status,string level)
        {
            if (Request.QueryString["id"] != null)  //表示提交人进入
            {
                if ((status == "1" || status == "0") && level == "3")//未指定审核人
                {
                    rblSHJS.SelectedIndex = 2; //默认三级审核
                    rblSHJS.Enabled = true;
                    plandate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    txt_plandate.Text = DateTime.Now.ToString("yyyy-MM-dd  HH:mm:ss");

                    hlSelect1.Visible = true;

                    txtBZ.Enabled = true;//备注
                    txtBZ.BorderStyle = BorderStyle.Solid;

                }
                else
                {
                    txtBZ.ReadOnly=true;//备注
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
            if (Request.QueryString["ost_audit_id"] != null)//审核人进入
            {
                rblSHJS.Enabled = false;
                txtBZ.ReadOnly=true;//备注
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
        }

        //初始化表名
        private void InitList()
        {
            #region
            switch (eng_type.Value)
            {
                case "回转窑":
                    strtable = "TBPM_STRINFOHZY";
                    break;
                case "球、立磨":
                    strtable = "TBPM_STRINFOQLM";
                    break;
                case "篦冷机":
                    strtable = "TBPM_STRINFOBLJ";
                    break;
                case "堆取料机":
                    strtable = "TBPM_STRINFODQLJ";
                    break;
                case "钢结构及非标":
                    strtable = "TBPM_STRINFOGFB";
                    break;
                case "电气及其他":
                    strtable = "TBPM_STRINFODQO";
                    break;
            }
            #endregion
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
                status = (string)ViewState["status"];
                int level = Convert.ToInt16(rblSHJS.SelectedValue);
                List<string> list_sql = new List<string>();
                if (this.CheckSelect(level))
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
                            if (!osl_no1.Text.Contains(" WXQX/"))  //对于正常和变更的提交修改原始数据审核状态，对于取消的批号不进行修改
                            {
                                sqlText = "update " + strtable + " set BM_OSREVIEW='1' ";
                                sqlText += "where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in (select OSL_NEWXUHAO from " + viewtable + " ";
                                sqlText += " where OSL_OUTSOURCENO='" + osl_no1.Text + "')";
                                list_sql.Add(sqlText);
                            }

                            sqlText = "update " + auditlist + " set OST_MDATE='" + reviewdate + "',";
                            sqlText += "OST_REVIEWERA='" + firstid.Value + "',OST_CHECKLEVEL='" + rblSHJS.SelectedValue + "',";
                            sqlText += "OST_STATE='2',OST_NOTE='" + txtBZ.Text.Trim() + "' where " + wxcode + "='" + osl_no1.Text + "'";
                            list_sql.Add(sqlText);
                            flag = "0";
                            break;
                            #endregion
                        case "2":
                            #region 一级审核状态
                            if (level == 1)
                            {
                                if (rblfirst.SelectedIndex == 0)//同意-审核通过
                                {
                                    if (!osl_no1.Text.Contains(" WXQX/")) //对于正常和变更的审核通过修改原始数据的变更和审核状态
                                    {
                                        sqlText = "update " + strtable + " set BM_OSREVIEW='3',BM_OSSTATUS='0' ";
                                        sqlText += "where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in (select OSL_NEWXUHAO from " + viewtable + " ";
                                        sqlText += " where OSL_OUTSOURCENO='" + osl_no1.Text + "')";
                                        list_sql.Add(sqlText);
                                    }
                                    else  //对于取消审核通过的修改原始数据的提交、变更、审核状态和委外部门
                                    {
                                        sqlText = "update " + strtable + " set BM_OSREVIEW='0',BM_OSSTATUS='0',BM_OSSTATE='0',BM_CONDICTIONATR='' ";
                                        sqlText += "where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in (select OSL_NEWXUHAO from " + viewtable + " ";
                                        sqlText += " where OSL_OUTSOURCENO='" + osl_no1.Text + "')";
                                        list_sql.Add(sqlText);
                                    }

                                    sqlText = "update " + auditlist + " set OST_ADATE='" + reviewdate + "',";
                                    sqlText += "OST_REVIEWADATE='" + DateTime.Now.ToString() + "',";
                                    sqlText += "OST_REVIEWAADVC='" + first_opinion.Text + "',OST_STATE='8' ";
                                    sqlText += "where " + wxcode + "='" + osl_no1.Text + "'";
                                    list_sql.Add(sqlText);
                                    wxverify(list_sql);
                                    flag = "3";
                                }
                                else if (rblfirst.SelectedIndex == 1)//一级驳回
                                {
                                    if (!osl_no1.Text.Contains(" WXQX/")) //正常和变更的审核驳回修改原始数据
                                    {
                                        sqlText = "update " + strtable + " set BM_OSREVIEW='2' ";
                                        sqlText += "where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in (select OSL_NEWXUHAO from " + viewtable + " ";
                                        sqlText += " where OSL_OUTSOURCENO='" + osl_no1.Text + "')";
                                        list_sql.Add(sqlText);
                                    }

                                    sqlText = "update " + auditlist + " set ";
                                    sqlText += "OST_REVIEWADATE='" + reviewdate + "',";
                                    sqlText += "OST_REVIEWAADVC='" + first_opinion.Text + "',OST_STATE='3' ";
                                    sqlText += "where " + wxcode + "='" + osl_no1.Text + "'";
                                    list_sql.Add(sqlText);
                                    flag = "2";
                                }
                            }
                            else if (level > 1)
                            {
                                if (rblfirst.SelectedIndex == 0)//同意-继续下推审核
                                {
                                    sqlText = "update " + auditlist + " set OST_REVIEWERB='" + secondid.Value + "',";
                                    sqlText += "OST_REVIEWADATE='" + reviewdate + "',";
                                    sqlText += "OST_REVIEWAADVC='" + first_opinion.Text + "',OST_STATE='4' ";
                                    sqlText += "where " + wxcode + "='" + osl_no1.Text + "'";

                                    list_sql.Add(sqlText);
                                    flag = "1";
                                }
                                else if (rblfirst.SelectedIndex == 1)//一级驳回
                                {
                                    if (!osl_no1.Text.Contains(" WXQX/")) //正常和变更的审核驳回修改原始数据
                                    {
                                        sqlText = "update " + strtable + " set BM_OSREVIEW='2' ";
                                        sqlText += "where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in (select OSL_NEWXUHAO from " + viewtable + " ";
                                        sqlText += " where OSL_OUTSOURCENO='" + osl_no1.Text + "')";
                                        list_sql.Add(sqlText);
                                    }

                                    sqlText = "update " + auditlist + " set ";
                                    sqlText += "OST_REVIEWADATE='" + reviewdate + "',";
                                    sqlText += "OST_REVIEWAADVC='" + first_opinion.Text + "',OST_STATE='3' ";
                                    sqlText += "where " + wxcode + "='" + osl_no1.Text + "' ";
                                    list_sql.Add(sqlText);

                                    flag = "2";
                                }
                            }
                            break;
                            #endregion
                        case "4":
                            #region 二级审核状态
                            if (level == 2)
                            {
                                if (rblsecond.SelectedIndex == 0)//通过
                                {
                                    if (!osl_no1.Text.Contains(" WXQX/")) //对于正常和变更的审核通过修改原始数据的变更和审核状态
                                    {
                                        sqlText = "update " + strtable + " set BM_OSREVIEW='3',BM_OSSTATUS='0' ";
                                        sqlText += "where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in (select OSL_NEWXUHAO from " + viewtable + " ";
                                        sqlText += " where OSL_OUTSOURCENO='" + osl_no1.Text + "')";
                                        list_sql.Add(sqlText);
                                    }
                                    else  //对于取消审核通过的修改原始数据的提交、变更、审核状态和委外部门
                                    {
                                        sqlText = "update " + strtable + " set BM_OSREVIEW='0',BM_OSSTATUS='0',BM_OSSTATE='0',BM_CONDICTIONATR='' ";
                                        sqlText += "where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in (select OSL_NEWXUHAO from " + viewtable + " ";
                                        sqlText += " where OSL_OUTSOURCENO='" + osl_no1.Text + "')";
                                        list_sql.Add(sqlText);
                                    }

                                    sqlText = "update " + auditlist + " set OST_ADATE='" + reviewdate + "',";
                                    sqlText += "OST_REVIEWBDATE='" + reviewdate + "',";
                                    sqlText += "OST_REVIEWBADVC='" + second_opinion.Text + "',OST_STATE='8' ";
                                    sqlText += "where " + wxcode + "='" + osl_no1.Text + "' ";
                                    list_sql.Add(sqlText);
                                    wxverify(list_sql);
                                    flag = "3";
                                }
                                else if (rblsecond.SelectedIndex == 1)//驳回
                                {
                                    if (!osl_no1.Text.Contains(" WXQX/")) //正常和变更的审核驳回修改原始数据
                                    {
                                        sqlText = "update " + strtable + " set BM_OSREVIEW='2' ";
                                        sqlText += "where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in (select OSL_NEWXUHAO from " + viewtable + " ";
                                        sqlText += " where OSL_OUTSOURCENO='" + osl_no1.Text + "')";
                                        list_sql.Add(sqlText);
                                    }

                                    sqlText = "update " + auditlist + " set ";
                                    sqlText += "OST_REVIEWBDATE='" + reviewdate + "',";
                                    sqlText += "OST_REVIEWBADVC='" + second_opinion.Text + "',OST_STATE='5' ";
                                    sqlText += "where " + wxcode + "='" + osl_no1.Text + "' and OST_STATE='4'";
                                    list_sql.Add(sqlText);
                                    flag = "2";
                                }
                            }
                            else if (level > 2)
                            {
                                if (rblsecond.SelectedIndex == 0)//同意-继续下推审核
                                {
                                    sqlText = "update " + auditlist + " set OST_REVIEWERC='" + thirdid.Value + "', ";
                                    sqlText += "OST_REVIEWBDATE='" + reviewdate + "',";
                                    sqlText += "OST_REVIEWBADVC='" + second_opinion.Text + "',OST_STATE='6' ";
                                    sqlText += "where " + wxcode + "='" + osl_no1.Text + "' and OST_STATE='4'";
                                    list_sql.Add(sqlText);
                                    flag = "1";
                                }
                                else if (rblsecond.SelectedIndex == 1)//二级驳回
                                {
                                    if (!osl_no1.Text.Contains(" WXQX/")) //正常和变更的审核驳回修改原始数据
                                    {
                                        sqlText = "update " + strtable + " set BM_OSREVIEW='2' ";
                                        sqlText += "where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in (select OSL_NEWXUHAO from " + viewtable + " ";
                                        sqlText += " where OSL_OUTSOURCENO='" + osl_no1.Text + "')";
                                        list_sql.Add(sqlText);
                                    }

                                    sqlText = "update " + auditlist + " set ";
                                    sqlText += "OST_REVIEWBDATE='" + reviewdate + "',";
                                    sqlText += "OST_REVIEWBADVC='" + second_opinion.Text + "',OST_STATE='5' ";
                                    sqlText += "where " + wxcode + "='" + osl_no1.Text + "' and OST_STATE='4'";
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
                                if (!osl_no1.Text.Contains(" WXQX/")) //对于正常和变更的审核通过修改原始数据的变更和审核状态
                                {
                                    sqlText = "update " + strtable + " set BM_OSREVIEW='3',BM_OSSTATUS='0' ";
                                    sqlText += "where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in (select OSL_NEWXUHAO from " + viewtable + " ";
                                    sqlText += " where OSL_OUTSOURCENO='" + osl_no1.Text + "')";
                                    list_sql.Add(sqlText);
                                }
                                else  //对于取消审核通过的修改原始数据的提交、变更、审核状态和委外部门
                                {
                                    sqlText = "update " + strtable + " set BM_OSREVIEW='0',BM_OSSTATUS='0',BM_OSSTATE='0',BM_CONDICTIONATR='' ";
                                    sqlText += "where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in (select OSL_NEWXUHAO from " + viewtable + " ";
                                    sqlText += " where OSL_OUTSOURCENO='" + osl_no1.Text + "')";
                                    list_sql.Add(sqlText);
                                }

                                sqlText = "update " + auditlist + " set OST_ADATE='" + reviewdate + "',";
                                sqlText += "OST_REVIEWCDATE='" + reviewdate + "',";
                                sqlText += "OST_REVIEWCADVC='" + third_opinion.Text + "',OST_STATE='8' ";
                                sqlText += "where " + wxcode + "='" + osl_no1.Text + "' and OST_STATE='6'";
                                list_sql.Add(sqlText);
                                wxverify(list_sql);
                                flag = "3";
                            }
                            else if (rblthird.SelectedIndex == 1)//三级驳回
                            {
                                if (!osl_no1.Text.Contains(" WXQX/")) //正常和变更的审核驳回修改原始数据
                                {
                                    sqlText = "update " + strtable + " set BM_OSREVIEW='2' ";
                                    sqlText += "where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO in (select OSL_NEWXUHAO from " + viewtable + " ";
                                    sqlText += " where OSL_OUTSOURCENO='" + osl_no1.Text + "')";
                                    list_sql.Add(sqlText);
                                }

                                sqlText = "update " + auditlist + " set ";
                                sqlText += "OST_REVIEWCDATE='" + reviewdate + "',";
                                sqlText += "OST_REVIEWCADVC='" + second_opinion.Text + "',OST_STATE='7' ";
                                sqlText += "where " + wxcode + "='" + osl_no1.Text + "' ";
                                list_sql.Add(sqlText);
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
                            _body = "外协计划（BOM）审批任务:"
                                + "\r\n编    号：" + osl_no1.Text.Trim()
                                + "\r\n项目名称：" + lab_proname.Text.Trim() + "_" + proid.Value.Trim() + ""
                                + "\r\n工程名称：" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0]
                                + "\r\n计划类别: " + txt_PlanType.Value.Trim()
                                + "\r\n技 术 员: " + txt_editor.Text.Trim()
                                + "\r\n编制时间:" + txt_plandate.Text.Trim();


                            _subject = "您有新的【外协计划】需要审批，请及时处理（BOM下推计划）" + lab_proname.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0];
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核任务已提交，请等待审核结果...');if(window.parent!=null)window.parent.location.href='TM_Task_View.aspx?action=" + tsa_id.Text.Trim() + "';else{window.location.href='TM_Task_View.aspx?action=" + tsa_id.Text.Trim() + "'};", true);
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
                            _body = "外协计划（BOM）审批任务:"
                                        + "\r\n编    号：" + osl_no1.Text.Trim()
                                        + "\r\n项目名称：" + lab_proname.Text.Trim() + "_" + proid.Value.Trim() + ""
                                        + "\r\n工程名称：" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0]
                                        + "\r\n计划类别: " + txt_PlanType.Value.Trim()
                                        + "\r\n技 术 员: " + txt_editor.Text.Trim()
                                        + "\r\n编制时间:" + txt_plandate.Text.Trim();

                            _subject = "您有新的【外协计划】需要审批，请及时处理（BOM下推计划）: " + lab_proname.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0];
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
                            _body = "外协计划（BOM）审批已驳回:"
                                + "\r\n编    号：" + osl_no1.Text.Trim()
                                + "\r\n项目名称：" + lab_proname.Text.Trim() + "_" + proid.Value.Trim() + ""
                                + "\r\n工程名称：" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0]
                                + "\r\n计划类别: " + txt_PlanType.Value.Trim()
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
                            _subject = "外协计划（BOM）审批已驳回: " + lab_proname.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0];
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
                            _body = "外协计划（BOM）审批已完成:"
                                + "\r\n编    号：" + osl_no1.Text.Trim()
                                + "\r\n项目名称：" + lab_proname.Text.Trim() + "_" + proid.Value.Trim() + ""
                                + "\r\n工程名称：" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0]
                                + "\r\n计划类别: " + txt_PlanType.Value.Trim()
                                + "\r\n技 术 员: " + txt_editor.Text.Trim()
                                + "\r\n编制时间:" + txt_plandate.Text.Trim()

                                + "\r\n\r\n为了避免给您的后续工作带来不便，请您确认计划的正确性，核实标识、备注等信息是否明确、完整，如有问题请及时通知采购将计划驳回";

                            _subject = "外协计划（BOM）审批已完成: " + lab_proname.Text.Trim() + "_" + proid.Value.Trim() + "" + "_" + lab_engname.Text.Trim() + "_" + tsa_id.Text.Trim().Split('-')[0];
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核通过，外协已生成!');location.href='TM_Leader_Task.aspx';", true);
                        return;
                    }
                    #endregion
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请将审核人或审核意见填写完整！！！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您已完成审核或无权审核该批任务！！！');", true);
            }
        }
        /// <summary>
        /// 能否提交判断
        /// </summary>
        /// <returns></returns>
        private bool CheckSelect(int level)
        {
            status = (string)ViewState["status"];
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

        //审核通过
        private void wxverify(List<string> list)
        {
            if (auditlist == "TBPM_OUTSOURCETOTAL")
            {
                pushDown(list);//正常外协审核通过下推采购部
            }
            else
            {
                mpVar(list);//变更外协审核通过下推至采购部的变量
            }
        }
        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }

        //正常外协下推采购部
        private void pushDown(List<string> list)
        {
            ddlQueryType.SelectedIndex = 0;
            this.btnQuery_OnClick(null, null);

            int z = 0;
            string pur_note;
            string outtype = "";
            string select_out_type = "select ISNULL(OST_OUTTYPE,'') AS OST_OUTTYPE from " + auditlist + " where OST_OUTSOURCENO='" + osl_no1.Text.Trim() + "'";
            SqlDataReader dr_out_type = DBCallCommon.GetDRUsingSqlText(select_out_type);
            if (dr_out_type.HasRows)
            {
                dr_out_type.Read();
                outtype = dr_out_type["OST_OUTTYPE"].ToString();
                dr_out_type.Close();
            }
            foreach (GridViewRow gr in GridView1.Rows)
            {
                string osl_tracknum = ((Label)gr.FindControl("lbltasknum")).Text.ToString();
                string osl_keycoms = ((Label)gr.FindControl("lblkeycoms")).Text.ToString();
                string osl_lbllength = ((Label)gr.FindControl("lbllength")).Text.ToString();
                string osl_lblwidth = ((Label)gr.FindControl("lblwidth")).Text.ToString();
                string osl_marid = gr.Cells[3].Text.ToString() == "&nbsp;"?"": gr.Cells[3].Text.ToString();
                string osl_biaoshi = gr.Cells[5].Text.ToString() == "&nbsp;" ? "" : gr.Cells[5].Text.ToString();

                string osl_num = gr.Cells[11].Text.ToString()== "&nbsp;"?"": gr.Cells[11].Text.ToString();
                string osl_weight = gr.Cells[12].Text.ToString() == "&nbsp;" ? "" : gr.Cells[12].Text.ToString();
                string osl_reqdate = gr.Cells[16].Text.ToString() == "&nbsp;" ? "" : gr.Cells[16].Text.ToString();
                string osl_request = gr.Cells[15].Text.ToString() == "&nbsp;" ? "" : gr.Cells[15].Text.ToString();
                string osl_note = gr.Cells[18].Text.ToString() == "&nbsp;" ? "" : gr.Cells[18].Text.ToString();

                string osl_type = gr.Cells[6].Text.ToString() == "库" ? outtype+"-库" : outtype;
                if (osl_marid != "")
                {
                    z++;
                    if (osl_note == "")
                    {
                        pur_note = osl_request;
                    }
                    else
                    {
                        pur_note = osl_request + "; " +osl_note ;
                    }

                    sqlText = "if not exists(select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + osl_tracknum + "') ";
                    sqlText += " begin ";
                    sqlText += "insert into TBPC_PURCHASEPLAN";
                    sqlText += "(PUR_PCODE,PUR_PTCODE,PUR_MARID,PUR_NUM,PUR_FZNUM,PUR_TIMEQ,PUR_KEYCOMS,PUR_LENGTH,PUR_WIDTH,PUR_NOTE,PUR_MASHAPE,PUR_TUHAO)";
                    sqlText += " values('" + osl_no1.Text.Trim() + "','" + osl_tracknum + "','" + osl_marid + "',";
                    sqlText += "'" + osl_num + "','" + osl_weight + "','" + osl_reqdate + "',";
                    sqlText += "'" + osl_keycoms + "','" + osl_lbllength + "','" + osl_lblwidth + "','" + pur_note + "','"+osl_type+"','"+osl_biaoshi+"')";
                    sqlText += " end";
                    list.Add(sqlText);
                }
            }

            if (z != 0)
            {
                sqlText = "if not exists(select * from TBPC_PCHSPLANRVW where PR_SHEETNO='" + osl_no1.Text + "') ";
                sqlText += " begin ";
                sqlText += "INSERT INTO TBPC_PCHSPLANRVW(PR_SHEETNO,PR_PJID,PR_ENGID,PR_DEPID,PR_SQREID,PR_SQTIME,PR_FZREID,PUR_MASHAPE) ";
                sqlText += " values('" + osl_no1.Text + "','" + proid.Value + "',";
                sqlText += "'" + tsa_id.Text.Substring(0,tsa_id.Text.IndexOf('-')) + "','" + editorid.Value.Substring(0, 2) + "',";
                sqlText += "'" + editorid.Value + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + secondid.Value + "','" + outtype + "') ";
                sqlText += " end";
                list.Add(sqlText);
            }
        }

        //变更外协的变量
        private void mpVar(List<string> list)
        {
            InitList();
            sqlText = "exec TM_OutVerify '" + osl_no1.Text + "'";  //对于变更进行采购变更操作
            list.Add(sqlText);

            sqlText = "exec TM_OutBGOperate '" + osl_no1.Text + "','" + strtable + "'"; //将变更审核通过的任务添加到外协明细中
            list.Add(sqlText);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                for (int i = 4; i < e.Row.Cells.Count; i++)//获取总列数
                {
                    //如果是数据行则添加title
                    e.Row.Attributes["style"] = "Cursor:hand";
                    e.Row.Cells[i].Attributes.Add("title", "双击查看计划明细");
                }
                string tracknum_table = e.Row.Cells[e.Row.Cells.Count - 2].Text.Trim() + "$" + viewtable;
                // 双击，设置 dbl_click=true，以取消单击响应
                e.Row.Attributes["ondblclick"] = String.Format("javascript:dbl_click=true;return Dblclik_ShowDetail('" + tracknum_table + "');");
            }
        }

        #region ShowModual
        [System.Web.Services.WebMethodAttribute(),
           System.Web.Script.Services.ScriptMethodAttribute()]
        public static string GetOutDynamicContent(string contextKey)
        {
            StringBuilder sTemp = new StringBuilder();
            string sql_getoutbefore = "select OSL_MARID,OSL_NAME,OSL_BIAOSHINO,OSL_GUIGE,OSL_CAIZHI,OSL_UNITWGHT,OSL_NUMBER,OSL_TOTALWGHTL,OSL_WDEPNAME,OSL_REQUEST,OSL_REQDATE,OSL_DELSITE,OSL_NOTE,OSL_TRACKNUM from View_TM_OUTSOURCELIST where OSL_NEWXUHAO='" + contextKey + "' and OSL_OUTSOURCECHGNO='" + OSL_NO + "'";

            DataTable dt_chgbef = DBCallCommon.GetDTUsingSqlText(sql_getoutbefore);

            sTemp.Append("<table style='background-color:#f3f3f3; border: #B9D3EE 3px solid;font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>");
            sTemp.Append("<tr><td colspan='14' style='background-color:#B9D3EE; color:white;' align='center'><b>变更前信息:</b></td></tr>");


            if (dt_chgbef.Rows.Count > 0)
            {

                sTemp.Append("<tr><td><b>物料编码</b></td>");
                sTemp.Append("<td align='center'><b>部件名称</b></td>");
                sTemp.Append("<td align='center'><b>标识</b></td>");
                sTemp.Append("<td align='center'><b>规格</b></td>");
                sTemp.Append("<td align='center'><b>材质</b></td>");
                sTemp.Append("<td align='center'><b>单重(kg)</b></td>");
                sTemp.Append("<td align='center'><b>数量</b></td>");
                sTemp.Append("<td align='center'><b>总重(kg)</b></td>");
                sTemp.Append("<td align='center'><b>外委部门</b></td>");
                sTemp.Append("<td align='center'><b>加工要求</b></td>");
                sTemp.Append("<td align='center'><b>加工日期</b></td>");
                sTemp.Append("<td align='center'><b>交货地点</b></td>");
                sTemp.Append("<td align='center'><b>备注</b></td>");
                sTemp.Append("<td align='center'><b>计划跟踪号</b></td></tr>");

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
                sTemp.Append("<tr><td colspan='14'><i>没有记录...</i></td></tr>");
            }
            sTemp.Append("</table>");

            return sTemp.ToString();
        }

        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (osl_no1.Text.Trim().Contains(".JSB WXBG/") || osl_no1.Text.Trim().Contains(".JSB WXQX/"))
                {
                    PopupControlExtender pcet = e.Row.FindControl("PopupControlExtender1") as PopupControlExtender;
                    string behaviorID = string.Concat("pcet", e.Row.RowIndex);
                    pcet.BehaviorID = behaviorID;
                    HyperLink hpl = (HyperLink)e.Row.FindControl("hplBeforeChg");
                    string OnMouseOverScript = string.Format("$find('{0}').showPopup();", behaviorID);
                    string OnMouseOutScript = string.Format("$find('{0}').hidePopup();", behaviorID);
                    hpl.Attributes.Add("onmouseover", OnMouseOverScript);
                    hpl.Attributes.Add("onmouseout", OnMouseOutScript);
                }
                //
                PopupControlExtender pce1 = e.Row.FindControl("PopupControlExtender2") as PopupControlExtender;
                string behaviorID2 = string.Concat("pce", e.Row.RowIndex);
                pce1.BehaviorID = behaviorID2;
                HyperLink hpl2 = (HyperLink)e.Row.FindControl("hplAOG");
                string OnMouseOverScriptAOG = string.Format("$find('{0}').showPopup();", behaviorID2);
                string OnMouseOutScriptAOG = string.Format("$find('{0}').hidePopup();", behaviorID2);
                hpl2.Attributes.Add("onmouseover", OnMouseOverScriptAOG);
                hpl2.Attributes.Add("onmouseout", OnMouseOutScriptAOG);
            }
        }
        #endregion

        #region ShowModual
        [System.Web.Services.WebMethodAttribute(),
           System.Web.Script.Services.ScriptMethodAttribute()]
        public static string GetAOGOutDynamicContent(string contextKey)
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
                    " union all "+
                    "select '订单到货量' as RowName,length,width,recgdnum,marunit,recgdfznum,marfzunit from View_TBPC_PURORDERDETAIL_PLAN_MPLAN where ptcode='" + contextKey + "'";
                DataTable dt_sql = DBCallCommon.GetDTUsingSqlText(sql);

                //订单状态
                string sql_order_state = " select '订单状态',case when isnull(sum(num),0)=0 then '未下推订单'  when sum(recgdnum)=0 then'未到货' when sum(recgdnum)-sum(num)>=0 then '全部到货' when sum(recgdnum)-sum(num)<0 then '部分到货' end from View_TBPC_PURORDERDETAIL_PLAN_MPLAN where ptcode='" + contextKey + "'";
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

        #endregion

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            this.BindDataOutDetail();
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
                    tracknum = GridView1.Rows[i].Cells[19].Text;
                    string sql_gotopur = "select ptcode from View_TBPC_PURCHASEPLAN where ptcode='" + tracknum + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_gotopur);
                    if (dr.HasRows)
                    {
                        //订单状态
                        string sql_order_state = " select '订单状态',case when isnull(sum(num),0)=0 then '未下推订单'  when sum(recgdnum)=0 then '未到货' when sum(recgdnum)-sum(num)>=0 then '全部到货' when sum(recgdnum)-sum(num)<0 then '部分到货' end  as OrderState  from View_TBPC_PURORDERDETAIL_PLAN_MPLAN where ptcode='" + tracknum + "'";
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

        /// <summary>
        /// 返回变更批的实际物料需求
        /// </summary>
        /// <param name="bglotnum"></param>
        /// <returns></returns>
        private DataTable GetChgMarofReal(string bglotnum)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "PRO_TM_GetChgMarofReal");
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