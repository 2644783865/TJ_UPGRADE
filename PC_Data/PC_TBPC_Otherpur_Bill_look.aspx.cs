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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Otherpur_Bill_look : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tb_pid.Text = Server.UrlDecode(Request.QueryString["mp_id"].ToString());//计划号
                ViewState["action"] = Request.QueryString["action"];
                initpagemess();
                Bind_Audit_Info();//加载审核信息
                tbpc_otherpurbill_lookRepeaterdatabind();
                Control_Enabled();
                this.rblSHDJ_Changed(null, null);
                if (rblSHDJ.SelectedIndex == 0)
                {
                    if (rbl_first.SelectedIndex == 0)
                    {
                        ImageAUDIT.Visible = true;
                    }
                }
                else if (rblSHDJ.SelectedIndex == 1)
                {
                    if (rbl_second.SelectedIndex == 0)
                    {
                        ImageAUDIT.Visible = true;
                    }
                }
                else if (rblSHDJ.SelectedIndex == 2)
                {
                    if (rbl_third.SelectedIndex == 0)
                    {
                        ImageAUDIT.Visible = true;
                    }
                }
                if (lb_state.Text == "1")
                {
                    btnqx.Visible = false;
                }
            }
            Get_SHR();
        }

        private void initpagemess()
        {
            string sqltext = "select PCODE,PJID,PJNM,ENGID,ENGNM,SQRID,SQRNM,TJRID,TJRNM," +
                             "TJDATE,SHRID,SHRNM,DEPID,DEPNM,NOTE,TOTALSTATE,MP_SHAPE,MP_SPZT,MP_YFBG,MP_IFFAST  "
                             + "from View_TBPC_OTPURRVW where PCODE='" + tb_pid.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                tb_dep.Text = dr["DEPNM"].ToString();
                tb_depid.Text = dr["DEPID"].ToString();
                Tb_shijian.Text = dr["TJDATE"].ToString();

                tb_pjid.Text = dr["PJID"].ToString();
                tb_pjinfo.Text = tb_pjid.Text;

                //tb_eng.Text = dr["ENGNM"].ToString();
                tb_engid.Text = dr["ENGID"].ToString();
                tb_enginfo.Text = tb_engid.Text;
                Tb_fuziren.Text = dr["SHRNM"].ToString();
                Tb_fuzirenid.Text = dr["SHRID"].ToString();
                Tb_shenqingren.Text = dr["SQRNM"].ToString();
                Tb_shenqingrenid.Text = dr["SQRID"].ToString();
                tb_executor.Text = dr["TJRNM"].ToString();
                tb_executorid.Text = dr["TJRID"].ToString();
                tb_note.Text = dr["NOTE"].ToString();
                lb_state.Text = dr["TOTALSTATE"].ToString();
                lb_shape.Text = dr["MP_SHAPE"].ToString();
                lbl_spzt.Text = dr["MP_SPZT"].ToString();
                if (dr["MP_YFBG"].ToString() == "1")
                {
                    cb_bg.Checked = true;
                }

                if (dr["MP_IFFAST"].ToString() == "1")
                {
                    chkiffast.Checked = true;
                }
            }
            dr.Close();

        }

        //控制控件是否可用
        private void Control_Enabled()
        {
            if (ViewState["action"].ToString() == "view")
            {
                btn_Audit.Visible = true;
                if (lbl_spzt.Text == "0" || lbl_spzt.Text == "4")
                {
                    txt_first.Enabled = false;
                    txt_second.Enabled = false;
                    txt_third.Enabled = false;
                    first_opinion.Enabled = false;
                    second_opinion.Enabled = false;
                    third_opinion.Enabled = false;
                    rbl_first.Enabled = false;
                    rbl_second.Enabled = false;
                    rbl_third.Enabled = false;

                    rblSHDJ.Enabled = true;
                    hlSelect1.Visible = true;
                    hlSelect2.Visible = true;
                    hlSelect3.Visible = true;

                }
                else
                {
                    btn_Audit.Visible = false;
                    pal_first.Enabled = false;
                    pal_second.Enabled = false;
                    pal_third.Enabled = false;
                    rblSHDJ.Enabled = false;
                    hlSelect1.Visible = false;
                    hlSelect2.Visible = false;
                    hlSelect3.Visible = false;
                }

                //if (lb_state.Text == "0" && lbl_spzt.Text == "3" && (Session["UserID"].ToString() == tb_executorid.Text||Session["UserID"].ToString() == Tb_shenqingrenid.Text))//只有未提交且审批状态为审批通过时才允许下堆
                //{                    
                //    btn_confirm.Visible = true;
                //}
                //else
                //{                   
                //    btn_confirm.Visible = false;
                //}

            }
            else if (ViewState["action"].ToString() == "audit")
            {

                //btn_confirm.Visible = false;
                rblSHDJ.Enabled = false;
                hlSelect1.Visible = false;
                hlSelect2.Visible = false;
                hlSelect3.Visible = false;
                txt_first.Enabled = false;
                txt_second.Enabled = false;
                txt_third.Enabled = false;
            }
        }
        private void tbpc_otherpurbill_lookRepeaterdatabind()
        {


            string sqltext = "SELECT PTCODE AS MP_PTCODE,MARID AS MP_MARID,MARNM AS MP_MARNAME,MARGG AS MP_MARNORM,"
                            + "MARCZ AS MP_MARTERIAL,MARGB AS MP_MARGUOBIAO,WIDTH AS MP_WIDTH,"
                            + "LENGTH AS MP_LENGTH,NUM AS MP_NUMBER,FZNUM AS MP_FZNUM,TIMERQ AS MP_TIMERQ,UNIT AS MP_NUNIT,FZUNIT AS MP_FZNUNIT,NOTE AS MP_NOTE,DETAILSTATE AS MP_STATE,MP_TUHAO  "
                            + "FROM View_TBPC_OTPURPLAN WHERE PCODE='" + tb_pid.Text + "'";
            DBCallCommon.BindRepeater(tbpc_otherpurbill_lookRepeater, sqltext);

        }

        //下推
        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            string iffast = "";
            if (chkiffast.Checked)
            {
                iffast = "1";
            }
            double num = 0;
            double fznum = 0;
            string sqltext = "";
            List<string> sqlar = new List<string>();
            sqltext = "INSERT INTO TBPC_PCHSPLANRVW(PR_SHEETNO,PR_ENGID,PR_PJID,PR_DEPID,PR_SQREID,PR_FZREID,PR_SQTIME,PR_STATE,PR_NOTE,PUR_MASHAPE) " +
                      "VALUES('" + tb_pid.Text.ToString() + "' ,'" + tb_pjid.Text + "','" + tb_engid.Text + "','" + tb_depid.Text + "','" + Tb_shenqingrenid.Text +
                      "','" + Tb_fuzirenid.Text + "','" + Tb_shijian.Text + "','0','" + tb_note.Text + "','" + lb_shape.Text + "')";

            sqlar.Add(sqltext);
            foreach (RepeaterItem Reitem in tbpc_otherpurbill_lookRepeater.Items)
            {
                string PUR_PCODE = tb_pid.Text.ToString();
                string PUR_PTCODE = ((Label)Reitem.FindControl("MP_PTCODE")).Text;
                string PUR_MARID = ((Label)Reitem.FindControl("MP_MARID")).Text;
                string MP_FZNUNIT = ((Label)Reitem.FindControl("MP_FZNUNIT")).Text;
                string sub_marid = PUR_MARID.Substring(0, 5).ToString();
                double PUR_NUM = Convert.ToDouble(((Label)Reitem.FindControl("MP_NUMBER")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_NUMBER")).Text.Trim());
                double PUR_FZNUM = Convert.ToDouble(((Label)Reitem.FindControl("MP_FZNUM")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_FZNUM")).Text.Trim());
                if (sub_marid == "01.07")
                {
                    num = PUR_FZNUM;
                    fznum = PUR_NUM * 1000;
                }
                else
                {
                    num = PUR_NUM;
                    fznum = PUR_FZNUM;
                }
                //double PUR_RPNUM = Convert.ToDouble(((Label)Reitem.FindControl("MP_NUMBER")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_NUMBER")).Text.Trim());
                //double PUR_RPFZNUM = Convert.ToDouble(((Label)Reitem.FindControl("MP_FZNUM")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_FZNUM")).Text.Trim());
                double PUR_LENGTH = Convert.ToDouble(((Label)Reitem.FindControl("MP_LENGTH")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_LENGTH")).Text.Trim());
                double PUR_WIDTH = Convert.ToDouble(((Label)Reitem.FindControl("MP_WIDTH")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_WIDTH")).Text.Trim());
                string PUR_TIMEQ = ((Label)Reitem.FindControl("MP_TIMERQ")).Text.Trim();  //需要采购的时间  2013年4月26日 09:31:45  Meng
                //string PUR_PTASTIME = Tb_shijian.Text;
                string PUR_NOTE = ((Label)Reitem.FindControl("MP_NOTE")).Text;
                char PUR_STATE = '0';

                sqltext = "INSERT INTO TBPC_PURCHASEPLAN(PUR_PCODE,PUR_PTCODE,PUR_MARID,PUR_LENGTH,PUR_WIDTH,PUR_NUM,PUR_FZNUM,PUR_STATE,PUR_NOTE,PUR_MASHAPE,PUR_TIMEQ,PUR_RPNUM,PUR_RPFZNUM,PUR_TECUNIT,PUR_IFFAST) " +
                          "VALUES('" + PUR_PCODE + "','" + PUR_PTCODE + "','" + PUR_MARID + "','" + PUR_LENGTH + "','" + PUR_WIDTH +
                           "','" + num + "','" + fznum + "','" + PUR_STATE + "','" + PUR_NOTE + "','" + lb_shape.Text + "','" + PUR_TIMEQ + "','" + num + "','" + fznum + "','" + MP_FZNUNIT + "','" + iffast + "') ";
                sqlar.Add(sqltext);
            }
            sqltext = "update TBPC_OTPURRVW set MP_STATE='1',MP_STATE='0' where MP_PCODE='" + tb_pid.Text + "'";
            sqlar.Add(sqltext);
            DBCallCommon.ExecuteTrans(sqlar);
            Response.Redirect("~/PC_Data/PC_TBPC_Otherpur_Bill_List.aspx");
        }

        public string get_pr_state(string i)
        {
            string state = "";
            if (i == "0")
            {
                state = "初始化";
            }
            else if (i == "1")
            {
                state = "提交";
            }
            return state;
        }

        //提交审批
        protected void btn_Audit_Click(object sender, EventArgs e)
        {
            //邮件提醒参数
            string subject = "";
            string body_title = "";
            string sendto = "";

            if (ViewState["action"].ToString() == "view") //查看  ，提交到审批
            {
                List<string> sqlstr = new List<string>();
                if (lbl_spzt.Text == "0")
                {
                    if (check_audit_Per())  //检查是否选择了审核人
                    {
                        string pa_code = tb_pid.Text.Trim();
                        string pa_fir_per = firstid.Value;
                        string pa_sec_per = secondid.Value;
                        string pa_thi_per = thirdid.Value;

                        //审核人不能相同
                        if (((pa_fir_per != pa_sec_per) && (pa_sec_per != pa_thi_per)) || ((pa_sec_per == "") && (pa_thi_per == "")))
                        {
                            //string sqltext = "insert into TBPC_OTPUR_Audit (PA_CODE,PA_FIR_PER,PA_FIR_JG,PA_SEC_PER,PA_SEC_JG,PA_THI_PER,PA_THI_JG,PA_RANK)" +
                            //    " values ('" + pa_code + "','" + pa_fir_per + "','0','" + pa_sec_per + "','0','" + pa_thi_per + "','0','"+rblSHDJ.SelectedIndex.ToString()+"')";
                            string sqltext = "insert into TBPC_OTPUR_Audit (PA_CODE,PA_CGLY,PA_FIR_PER,PA_FIR_JG,PA_SEC_PER,PA_SEC_JG,PA_THI_PER,PA_THI_JG,PA_RANK)" +
                                " values ('" + pa_code + "','" + txtPA_CGLY.Text.Trim() + "','" + pa_fir_per + "','0','" + pa_sec_per + "','0','" + pa_thi_per + "','0','" + rblSHDJ.SelectedIndex.ToString() + "')";
                            sqlstr.Add(sqltext);
                            string sqlupdatezt = "update TBPC_OTPURRVW set MP_SPZT='1' where MP_PCODE='" + tb_pid.Text.ToString() + "'";
                            sqlstr.Add(sqlupdatezt);
                            DBCallCommon.ExecuteTrans(sqlstr);

                            //制单人提交后向第一个审批人发送邮件
                            subject = "您有新的采购计划需要审批——" + tb_pid.Text.ToString();
                            sendto = txt_first.Text.Trim();
                            this.SendMail(subject, body_title, sendto);

                            Response.Redirect("../PC_Data/PC_TBPC_Otherpur_Bill_List.aspx");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核人不能相同！');", true); return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核人！');", true); return;
                    }
                }
                else if (lbl_spzt.Text == "4")
                {
                    if (check_audit_Per())  //检查是否选择了审核人
                    {
                        string pa_code = tb_pid.Text.Trim();
                        string pa_fir_per = firstid.Value;
                        string pa_sec_per = secondid.Value;
                        string pa_thi_per = thirdid.Value;

                        //审核人不能相同
                        if (((pa_fir_per != pa_sec_per) && (pa_sec_per != pa_thi_per)) || ((pa_sec_per == "") && (pa_thi_per == "")))
                        {
                            string update1 = "update TBPC_OTPUR_Audit set PA_FIR_PER='" + pa_fir_per + "',PA_FIR_JG='0',PA_FIR_YJ='',PA_FIR_SJ='',PA_SEC_PER='" + pa_sec_per + "',PA_SEC_JG='0'" +
                            " ,PA_SEC_YJ='',PA_SEC_SJ='',PA_THI_PER='" + pa_thi_per + "',PA_THI_JG='0',PA_THI_YJ='',PA_THI_SJ='',PA_RANK='" + rblSHDJ.SelectedIndex.ToString() + "' where PA_CODE='" + tb_pid.Text.ToString() + "'";
                            string update2 = "update TBPC_OTPURRVW set MP_SPZT='1' where MP_PCODE='" + tb_pid.Text.ToString() + "'";
                            sqlstr.Add(update1);
                            sqlstr.Add(update2);
                            DBCallCommon.ExecuteTrans(sqlstr);

                            //制单人提交后向第一个审批人发送邮件
                            subject = "驳回后的采购计划已修改，请重新审批——" + tb_pid.Text.ToString();
                            sendto = txt_first.Text.Trim();
                            this.SendMail(subject, body_title, sendto);

                            Response.Redirect("../PC_Data/PC_TBPC_Otherpur_Bill_List.aspx");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审核人不能相同！');", true); return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核人！');", true); return;
                    }
                }
            }
            else if (ViewState["action"].ToString() == "audit") //审批 ，提交审批意见
            {
                string spyj = "";// 审批意见
                string spsj = DateTime.Now.ToShortDateString();//审批时间
                string spjg = "";//审批结果，0：未审批，1：同意，2：拒绝

                string set_text = "";//更新内容

                //记录审批人是第几级
                string submit_type = "";
                if (Session["UserID"].ToString() == firstid.Value.ToString())
                {
                    spyj = first_opinion.Text.ToString();
                    spjg = rbl_first.SelectedValue.ToString();
                    set_text = "set PA_FIR_YJ='" + spyj + "',PA_FIR_SJ='" + spsj + "',PA_FIR_JG='" + spjg + "' ";

                    submit_type = "1";
                }
                else if (Session["UserID"].ToString() == secondid.Value.ToString())
                {
                    spyj = second_opinion.Text.ToString();
                    spjg = rbl_second.SelectedValue.ToString();
                    set_text = "set PA_SEC_YJ='" + spyj + "',PA_SEC_SJ='" + spsj + "',PA_SEC_JG='" + spjg + "' ";

                    submit_type = "2";
                }
                else if (Session["UserID"].ToString() == thirdid.Value.ToString())
                {
                    spyj = third_opinion.Text.ToString();
                    spjg = rbl_third.SelectedValue.ToString();
                    set_text = "set PA_THI_YJ='" + spyj + "',PA_THI_SJ='" + spsj + "',PA_THI_JG='" + spjg + "' ";

                    submit_type = "3";
                }

                if (spjg == "" || spjg == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择【同意】或【不同意】！');", true); return;
                }
                List<string> sqlstr = new List<string>();
                string sqltext = "update TBPC_OTPUR_Audit " + set_text + " where PA_CODE='" + tb_pid.Text.ToString() + "'";
                sqlstr.Add(sqltext);

                //更新审批状态，0：保存；1：提交未审批；2：审批中；3：已通过；4：已驳回；
                string spzt = (spjg == "1") ? "2" : "4";
                string sqlupdatezt = "update TBPC_OTPURRVW set MP_SPZT='" + spzt + "' where MP_PCODE='" + tb_pid.Text.ToString() + "'";
                sqlstr.Add(sqlupdatezt);
                DBCallCommon.ExecuteTrans(sqlstr);

                //如果同意则向下一级审批人发送邮件通知
                if (spjg == "1")
                {
                    switch (submit_type)
                    {
                        case "1":
                            subject = "您有新的采购计划需要审批，请即时处理——" + tb_pid.Text.ToString();
                            sendto = txt_second.Text.Trim();
                            break;
                        case "2":
                            subject = "您有新的采购计划需要审批，请即时处理——" + tb_pid.Text.ToString();
                            sendto = txt_third.Text.Trim();
                            break;
                    }
                    if (sendto != "")
                    {
                        this.SendMail(subject, body_title, sendto);
                    }

                }
                //如果驳回则向制单人发送邮件通知
                else if (spjg == "2")
                {
                    subject = "采购计划已驳回——" + tb_pid.Text.ToString();
                    body_title = "您提交的采购申请于" + DateTime.Now.ToString() + "被驳回";
                    sendto = tb_executor.Text.Trim();
                    this.SendMail(subject, body_title, sendto);
                }

                //检查是否全部同意，如果全部同意则改为3。根据审核等级判断，只判断最后一个是否同意即可
                bool YesOrNo = false;
                string sqlJG = "select * from TBPC_OTPUR_Audit where PA_CODE='" + tb_pid.Text.ToString() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlJG);
                if (dt.Rows.Count > 0)
                {
                    switch (rblSHDJ.SelectedValue.ToString())
                    {
                        case "1":
                            if (dt.Rows[0]["PA_FIR_JG"].ToString() == "1")
                            { YesOrNo = true; }
                            break;
                        case "2":
                            if (dt.Rows[0]["PA_SEC_JG"].ToString() == "1")
                            { YesOrNo = true; }

                            break;
                        case "3":
                            if (dt.Rows[0]["PA_THI_JG"].ToString() == "1")
                            { YesOrNo = true; }
                            break;
                    }
                }
                if (YesOrNo)
                {
                    string strupdatezp2 = "update TBPC_OTPURRVW set MP_SPZT='3' where MP_PCODE='" + tb_pid.Text.ToString() + "'";
                    sqlstr.Clear();
                    sqlstr.Add(strupdatezp2);
                    DBCallCommon.ExecuteTrans(sqlstr);

                    this.XiaTui(tb_pid.Text);

                    //下推后通知制单人
                    subject = "您提交的采购计划已审批并下推至采购部——" + tb_pid.Text.ToString();
                    body_title = "您提交的采购申请已审批完，于" + DateTime.Now.ToString() + "下推至采购部";
                    sendto = tb_executor.Text.Trim();
                    this.SendMail(subject, body_title, sendto);
                }
                Response.Redirect("../PC_Data/PC_TBPC_Otherpur_Bill_Audit.aspx");

            }

        }

        //检查是否选择审批人，根据审批等级判断，默认三级
        protected bool check_audit_Per()
        {
            bool isselect = true;
            switch (rblSHDJ.SelectedIndex)
            {
                case 0:
                    if (txt_first.Text == "")
                    { isselect = false; }
                    break;
                case 1:
                    if (txt_first.Text == "" || txt_second.Text == "")
                    { isselect = false; }
                    break;
                case 2:
                    if (txt_first.Text == "" || txt_second.Text == "" || txt_third.Text == "")
                    { isselect = false; }
                    break;
            }
            return isselect;
        }

        protected void rblSHDJ_Changed(object sender, EventArgs e)
        {
            switch (rblSHDJ.SelectedIndex)
            {
                case 0:
                    pal_first.Visible = true;
                    pal_second.Visible = false;
                    secondid.Value = string.Empty;
                    txt_second.Text = string.Empty;
                    pal_third.Visible = false;
                    txt_third.Text = string.Empty;
                    thirdid.Value = string.Empty;
                    break;
                case 1:
                    pal_first.Visible = true;
                    pal_second.Visible = true;
                    pal_third.Visible = false;
                    txt_third.Text = string.Empty;
                    thirdid.Value = string.Empty;
                    break;
                case 2:
                    pal_first.Visible = true;
                    pal_second.Visible = true;
                    pal_third.Visible = true;
                    break;
            }
        }

        //审核信息加载
        private void Bind_Audit_Info()
        {
            string sqltext = "select * from VIEW_OTPUR_AUDIT where PA_CODE='" + tb_pid.Text.ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                txtPA_CGLY.Text = dt.Rows[0]["PA_CGLY"].ToString();

                firstid.Value = dt.Rows[0]["PA_FIR_PER"].ToString();
                txt_first.Text = dt.Rows[0]["name1"].ToString();
                if (dt.Rows[0]["PA_FIR_JG"].ToString() != "0")
                { rbl_first.SelectedValue = dt.Rows[0]["PA_FIR_JG"].ToString(); }
                first_time.Text = dt.Rows[0]["PA_FIR_SJ"].ToString();
                first_opinion.Text = dt.Rows[0]["PA_FIR_YJ"].ToString();

                secondid.Value = dt.Rows[0]["PA_SEC_PER"].ToString();
                txt_second.Text = dt.Rows[0]["name2"].ToString();
                if (dt.Rows[0]["PA_SEC_JG"].ToString() != "0")
                { rbl_second.SelectedValue = dt.Rows[0]["PA_SEC_JG"].ToString(); }
                second_time.Text = dt.Rows[0]["PA_SEC_SJ"].ToString();
                second_opinion.Text = dt.Rows[0]["PA_SEC_YJ"].ToString();

                thirdid.Value = dt.Rows[0]["PA_THI_PER"].ToString();
                txt_third.Text = dt.Rows[0]["name3"].ToString();
                if (dt.Rows[0]["PA_THI_JG"].ToString() != "0")
                { rbl_third.SelectedValue = dt.Rows[0]["PA_THI_JG"].ToString(); }
                third_time.Text = dt.Rows[0]["PA_THI_SJ"].ToString();
                third_opinion.Text = dt.Rows[0]["PA_THI_YJ"].ToString();

                Control_SHLC();
            }
        }

        private void Control_SHLC()//审核流程控制
        {
            string sqltext = "select * from VIEW_OTPUR_AUDIT where PA_CODE='" + tb_pid.Text.ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                if (Session["UserID"].ToString() == tb_executorid.Text.ToString() && (lbl_spzt.Text == "0" || lbl_spzt.Text == "4"))
                {
                    pal_first.Enabled = true;
                    pal_second.Enabled = true;
                    pal_third.Enabled = true;
                }
                else if (Session["UserID"].ToString() == firstid.Value)
                {
                    pal_second.Enabled = false;
                    pal_third.Enabled = false;
                    first_opinion.BackColor = System.Drawing.Color.Orange;
                }

                else if (Session["UserID"].ToString() == secondid.Value)
                {
                    pal_first.Enabled = false;
                    pal_third.Enabled = false;
                    if (dt.Rows[0]["PA_FIR_JG"].ToString() != "1")
                    {
                        pal_second.Enabled = false;
                        btn_Audit.Visible = false;
                    }
                    second_opinion.BackColor = System.Drawing.Color.Orange;
                }

                else if (Session["UserID"].ToString() == thirdid.Value)
                {
                    pal_first.Enabled = false;
                    pal_second.Enabled = false;
                    if (dt.Rows[0]["PA_SEC_JG"].ToString() != "1")
                    {
                        pal_third.Enabled = false;
                        btn_Audit.Visible = false;
                    }
                    third_opinion.BackColor = System.Drawing.Color.Orange;
                }
                else
                {
                    pal_first.Enabled = false;
                    pal_second.Enabled = false;
                    pal_third.Enabled = false;
                }
                rblSHDJ.SelectedIndex = Convert.ToInt32(dt.Rows[0]["PA_RANK"].ToString());
                this.rblSHDJ_Changed(null, null);
            }

        }


        //选择审核人panel
        private void Get_SHR()
        {
            //审核人1
            string sql1 = "select st_ID,st_name from TBDS_STAFFINFO where st_depid='" + tb_depid.Text.ToString() + "' and ST_PD='0'";// or ST_POSITION='0301'
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
            if (dt1.Rows.Count != 0)
            {
                Table t = new Table();
                TableRow tr = new TableRow();
                TableCell td = new TableCell();

                CheckBoxList cki = new CheckBoxList();
                cki.ID = "cki1";
                cki.DataSource = dt1;
                cki.DataTextField = "ST_NAME";//领导的姓名
                cki.DataValueField = "ST_ID";//领导的编号
                cki.DataBind();
                for (int k = 0; k < cki.Items.Count; k++)
                {
                    cki.Items[k].Attributes.Add("Onclick", "CheckBoxList_Click(this)");//使用了javascript使其只能勾选一个
                }
                cki.RepeatColumns = 5;//获取显示的列数

                td.Controls.Add(cki);
                tr.Cells.Add(td);
                t.Controls.Add(tr);
                pal_select1_inner.Controls.Add(t);
            }
            // 审核人2
            string sql2 = "select st_ID,st_name from TBDS_STAFFINFO where (st_depid='" + tb_depid.Text.ToString() + "' or st_depid='01') and ST_PD='0'";// or ST_POSITION='0301'
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql2);
            if (dt1.Rows.Count != 0)
            {
                Table t = new Table();
                TableRow tr = new TableRow();
                TableCell td = new TableCell();

                CheckBoxList cki = new CheckBoxList();
                cki.ID = "cki2";
                cki.DataSource = dt2;
                cki.DataTextField = "ST_NAME";//领导的姓名
                cki.DataValueField = "ST_ID";//领导的编号
                cki.DataBind();
                for (int k = 0; k < cki.Items.Count; k++)
                {
                    cki.Items[k].Attributes.Add("Onclick", "CheckBoxList_Click(this)");//使用了javascript使其只能勾选一个
                }
                cki.RepeatColumns = 5;//获取显示的列数

                td.Controls.Add(cki);
                tr.Cells.Add(td);
                t.Controls.Add(tr);
                pal_select2_inner.Controls.Add(t);
            }
            // 审核人3
            string sql3 = "select st_ID,st_name from TBDS_STAFFINFO where  (st_depid='" + tb_depid.Text.ToString() + "' or st_depid='01') and ST_PD='0'";//第三级审核加上了公司领导 or ST_POSITION='0301'
            DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sql3);
            if (dt3.Rows.Count != 0)
            {
                Table t = new Table();
                TableRow tr = new TableRow();
                TableCell td = new TableCell();

                CheckBoxList cki = new CheckBoxList();
                cki.ID = "cki3";
                cki.DataSource = dt3;
                cki.DataTextField = "ST_NAME";//领导的姓名
                cki.DataValueField = "ST_ID";//领导的编号
                cki.DataBind();
                for (int k = 0; k < cki.Items.Count; k++)
                {
                    cki.Items[k].Attributes.Add("Onclick", "CheckBoxList_Click(this)");//使用了javascript使其只能勾选一个
                }
                cki.RepeatColumns = 5;//获取显示的列数

                td.Controls.Add(cki);
                tr.Cells.Add(td);
                t.Controls.Add(tr);
                pal_select3_inner.Controls.Add(t);
            }

        }

        protected void btn_shr1_Click(object sender, EventArgs e)
        {

            CheckBoxList ck = (CheckBoxList)pal_select1_inner.FindControl("cki1");
            if (ck != null)
            {
                for (int j = 0; j < ck.Items.Count; j++)
                {
                    if (ck.Items[j].Selected == true)
                    {
                        firstid.Value = ck.Items[j].Value.ToString();
                        txt_first.Text = ck.Items[j].Text.ToString();
                        return;
                    }
                }
            }

        }

        protected void btn_shr2_Click(object sender, EventArgs e)
        {

            CheckBoxList ck = (CheckBoxList)pal_select2_inner.FindControl("cki2");
            if (ck != null)
            {
                for (int j = 0; j < ck.Items.Count; j++)
                {
                    if (ck.Items[j].Selected == true)
                    {
                        secondid.Value = ck.Items[j].Value.ToString();
                        txt_second.Text = ck.Items[j].Text.ToString();
                        return;
                    }
                }
            }

        }

        protected void btn_shr3_Click(object sender, EventArgs e)
        {

            CheckBoxList ck = (CheckBoxList)pal_select1_inner.FindControl("cki3");
            if (ck != null)
            {
                for (int j = 0; j < ck.Items.Count; j++)
                {
                    if (ck.Items[j].Selected == true)
                    {
                        thirdid.Value = ck.Items[j].Value.ToString();
                        txt_third.Text = ck.Items[j].Text.ToString();
                        return;
                    }
                }
            }

        }

        //审核全部通过后自动下推，下推时间为审核通过后的时间，而不是提交申请时间
        private void XiaTui(string ph)
        {
            string iffast = "";
            if (chkiffast.Checked)
            {
                iffast = "1";
            }
            double num = 0;
            double fznum = 0;
            string sqltext = "";

            string xiatui_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            List<string> sqlar = new List<string>();

            if (ph.ToString().Contains('_') && ph.ToString().Split('_')[0] == "BG" || cb_bg.Checked == true)
            {

                sqltext = "INSERT INTO TBPC_MPCHANGETOTAL(MP_CHPCODE,MP_CHPJID,MP_CHENGID,MP_CHSUBMITNMID,MP_CHSUBMITTM,MP_CHREVIEWA,MP_CHREVIEWB,MP_CHADATE,MP_CHSTATE,MP_CHNOTE,MP_MASHAPE) " +
                          "VALUES('" + tb_pid.Text.ToString() + "' ,'" + tb_pjid.Text + "','" + tb_engid.Text + "','" + Tb_shenqingrenid.Text +
                          "','" + xiatui_time + "','" + Tb_fuzirenid.Text + "',' ',' ','0','" + tb_note.Text + "','" + lb_shape.Text + "')";

                sqlar.Add(sqltext);
                foreach (RepeaterItem Reitem in tbpc_otherpurbill_lookRepeater.Items)
                {
                    double rate = 1;
                    string PUR_PCODE = tb_pid.Text.ToString();
                    string PUR_PTCODE = ((Label)Reitem.FindControl("MP_PTCODE")).Text;
                    string PUR_MARID = ((Label)Reitem.FindControl("MP_MARID")).Text;
                    string marnm = ((Label)Reitem.FindControl("MP_MARNAME")).Text;
                    string sub_marid = PUR_MARID.Substring(0, 5).ToString();
                    double PUR_NUM = Convert.ToDouble(((Label)Reitem.FindControl("MP_NUMBER")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_NUMBER")).Text.Trim());
                    double PUR_FZNUM = Convert.ToDouble(((Label)Reitem.FindControl("MP_FZNUM")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_FZNUM")).Text.Trim());
                    sqltext = "select CONVERTRATE from TBMA_MATERIAL where ID='" + PUR_MARID + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (dt.Rows.Count > 0)
                    {
                        rate = Convert.ToDouble(dt.Rows[0]["CONVERTRATE"].ToString());
                    }
                    num = PUR_NUM;
                    fznum = PUR_FZNUM;


                    //增加图号/标识号
                    string MP_TUHAO = ((Label)Reitem.FindControl("MP_TUHAO")).Text.Trim();

                    //double PUR_RPNUM = Convert.ToDouble(((Label)Reitem.FindControl("MP_NUMBER")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_NUMBER")).Text.Trim());
                    //double PUR_RPFZNUM = Convert.ToDouble(((Label)Reitem.FindControl("MP_FZNUM")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_FZNUM")).Text.Trim());
                    double PUR_LENGTH = Convert.ToDouble(((Label)Reitem.FindControl("MP_LENGTH")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_LENGTH")).Text.Trim());
                    double PUR_WIDTH = Convert.ToDouble(((Label)Reitem.FindControl("MP_WIDTH")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_WIDTH")).Text.Trim());
                    //string PUR_PTASTIME = xiatui_time;
                    string PUR_NOTE = ((Label)Reitem.FindControl("MP_NOTE")).Text;
                    char PUR_STATE = '0';


                    sqltext = "INSERT INTO TBPC_MPTEMPCHANGE(MP_CHPCODE,MP_CHPTCODE,MP_MARID,MP_PJID,MP_ENGID,MP_BGNUM,MP_BGFZNUM,MP_WIDTH,MP_LENGTH,MP_MASHAPE,MP_STATE,MP_NOTE,MP_TUHAO) " +
                              "VALUES('" + PUR_PCODE + "','" + PUR_PTCODE + "','" + PUR_MARID + "','" + tb_pjid.Text + "','" + tb_engid.Text + "','" + num + "','" + fznum +
                               "','" + PUR_LENGTH + "','" + PUR_WIDTH + "','" + lb_shape.Text + "','" + PUR_STATE + "','" + PUR_NOTE + "','" + MP_TUHAO + "') ";
                    sqlar.Add(sqltext);
                }
            }



            //非变更，正常下推的
            else
            {
                string sqlcheck = "select * from TBDS_STAFFINFO where ST_ID='" + Tb_shenqingrenid.Text + "'";
                DataTable dtcheck = DBCallCommon.GetDTUsingSqlText(sqlcheck);
                if (dtcheck.Rows[0]["ST_DEPID"].ToString() == "04" || dtcheck.Rows[0]["ST_DEPID"].ToString() == "10")
                {
                    sqltext = "INSERT INTO TBPC_PCHSPLANRVW(PR_SHEETNO,PR_ENGID,PR_PJID,PR_DEPID,PR_SQREID,PR_FZREID,PR_SQTIME,PR_STATE,PR_NOTE,PUR_MASHAPE) " +
                           "VALUES('" + tb_pid.Text.ToString() + "' ,'" + tb_pjid.Text + "','" + tb_engid.Text + "','" + tb_depid.Text + "','" + Tb_shenqingrenid.Text +
                           "','" + Tb_fuzirenid.Text + "','" + xiatui_time + "','5','" + tb_note.Text + "','" + lb_shape.Text + "')";
                }
                else
                {
                    sqltext = "INSERT INTO TBPC_PCHSPLANRVW(PR_SHEETNO,PR_ENGID,PR_PJID,PR_DEPID,PR_SQREID,PR_FZREID,PR_SQTIME,PR_STATE,PR_NOTE,PUR_MASHAPE) " +
                               "VALUES('" + tb_pid.Text.ToString() + "' ,'" + tb_pjid.Text + "','" + tb_engid.Text + "','" + tb_depid.Text + "','" + Tb_shenqingrenid.Text +
                               "','" + Tb_fuzirenid.Text + "','" + xiatui_time + "','0','" + tb_note.Text + "','" + lb_shape.Text + "')";
                }

                sqlar.Add(sqltext);
                foreach (RepeaterItem Reitem in tbpc_otherpurbill_lookRepeater.Items)
                {
                    double rate = 1;
                    string PUR_PCODE = tb_pid.Text.ToString();
                    string PUR_PTCODE = ((Label)Reitem.FindControl("MP_PTCODE")).Text;
                    string PUR_MARID = ((Label)Reitem.FindControl("MP_MARID")).Text;
                    string marnm = ((Label)Reitem.FindControl("MP_MARNAME")).Text;
                    string sub_marid = PUR_MARID.Substring(0, 5).ToString();
                    double PUR_NUM = Convert.ToDouble(((Label)Reitem.FindControl("MP_NUMBER")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_NUMBER")).Text.Trim());
                    double PUR_FZNUM = Convert.ToDouble(((Label)Reitem.FindControl("MP_FZNUM")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_FZNUM")).Text.Trim());
                    sqltext = "select CONVERTRATE from TBMA_MATERIAL where ID='" + PUR_MARID + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (dt.Rows.Count > 0)
                    {
                        rate = Convert.ToDouble(dt.Rows[0]["CONVERTRATE"].ToString());
                    }
                    num = PUR_NUM;
                    fznum = PUR_FZNUM;


                    //增加图号/标识号
                    string MP_TUHAO = ((Label)Reitem.FindControl("MP_TUHAO")).Text.Trim();

                    //double PUR_RPNUM = Convert.ToDouble(((Label)Reitem.FindControl("MP_NUMBER")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_NUMBER")).Text.Trim());
                    //double PUR_RPFZNUM = Convert.ToDouble(((Label)Reitem.FindControl("MP_FZNUM")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_FZNUM")).Text.Trim());
                    double PUR_LENGTH = Convert.ToDouble(((Label)Reitem.FindControl("MP_LENGTH")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_LENGTH")).Text.Trim());
                    double PUR_WIDTH = Convert.ToDouble(((Label)Reitem.FindControl("MP_WIDTH")).Text.Trim() == "" ? "0" : ((Label)Reitem.FindControl("MP_WIDTH")).Text.Trim());
                    //string PUR_PTASTIME = xiatui_time;
                    string PUR_TIMEQ = ((Label)Reitem.FindControl("MP_TIMERQ")).Text.Trim();  //需要采购的时间  2013年4月26日 09:31:45  Meng
                    string PUR_NOTE = ((Label)Reitem.FindControl("MP_NOTE")).Text;
                    char PUR_STATE = '0';
                    if (dtcheck.Rows[0]["ST_DEPID"].ToString() == "04" || dtcheck.Rows[0]["ST_DEPID"].ToString() == "10")
                    {
                        PUR_STATE = '3';
                    }
                    string MP_FZNUNIT = ((Label)Reitem.FindControl("MP_FZNUNIT")).Text.Trim();
                    sqltext = "INSERT INTO TBPC_PURCHASEPLAN(PUR_PCODE,PUR_PTCODE,PUR_MARID,PUR_LENGTH,PUR_WIDTH,PUR_NUM,PUR_FZNUM,PUR_STATE,PUR_NOTE,PUR_MASHAPE,PUR_TUHAO,PUR_TIMEQ,PUR_RPNUM,PUR_RPFZNUM,PUR_TECUNIT,PUR_IFFAST) " +
                              "VALUES('" + PUR_PCODE + "','" + PUR_PTCODE + "','" + PUR_MARID + "','" + PUR_LENGTH + "','" + PUR_WIDTH +
                               "','" + num + "','" + fznum + "','" + PUR_STATE + "','" + PUR_NOTE + "','" + lb_shape.Text + "','" + MP_TUHAO + "','" + PUR_TIMEQ + "','" + num + "','" + fznum + "','" + MP_FZNUNIT + "','"+iffast+"') ";
                    sqlar.Add(sqltext);
                }

            }
            sqltext = "update TBPC_OTPURRVW set MP_STATE='1' where MP_PCODE='" + tb_pid.Text + "'";
            sqlar.Add(sqltext);
            DBCallCommon.ExecuteTrans(sqlar);
        }
        //取消
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }

        //重置条件
        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void QueryButton_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            string shape;
            string pocode = "";
            if (isselected())
            {
                string pi_id = "";
                string tag_pi_id = "";
                if (txb_pjnm.Text != "" && txb_pjid.Text != "" && txb_engnm.Text != "" && txb_engid.Text != "")
                {
                    tag_pi_id = txb_pjnm.Text + "_" + txb_engnm.Text + "_" + txb_engid.Text;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('项目工程不能为空！');", true);
                    return;
                }
                if (DropDownList1.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择物料类型！');", true);
                    return;
                }
                else
                {
                    shape = DropDownList1.SelectedItem.Text.ToString();
                }
                string end_pi_id = "";
                string sql = "select TOP 1 right('000000000000000000000000'+isnull(right(INDEX_ID,charindex('_',reverse(INDEX_ID))-1),'1'),24) as TotalIndex from View_TM_BOMAllLotNum where CHARINDEX('_', INDEX_ID) > 0 order by right('000000000000000000000000'+right(INDEX_ID,charindex('_',reverse(INDEX_ID))-1),24) desc";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.HasRows)
                {
                    dr.Read();
                    int index = Convert.ToInt32(dr["TotalIndex"].ToString()) + 1;
                    dr.Close();
                    end_pi_id = index.ToString();
                }
                else
                {
                    end_pi_id = "0001";
                }
                pi_id = tag_pi_id + "_" + end_pi_id;
                pocode = pi_id;
                sqltext = "INSERT INTO TBPC_OTPURRVW(MP_PCODE,MP_SUBMITID,MP_SUBMITTM,MP_USEDEPID,MP_PJID,MP_ENGID,MP_SHAPE,MP_SQRENID,MP_REVIEWA,MP_NOTE) " +
                                          "VALUES('" + pocode + "' ,'" + Session["UserID"].ToString() + "'," +
                                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                          "'" + Session["UserDeptID"].ToString() + "','" + txb_pjid.Text + "','" + txb_engid.Text + "','" + shape + "','" + Session["UserID"].ToString() + "','" + Tb_fuzirenid.Text + "','" + tb_note.Text + "')";
                DBCallCommon.ExeSqlText(sqltext);
                foreach (RepeaterItem Reitem in tbpc_otherpurbill_lookRepeater.Items)
                {
                    string marid = ((Label)Reitem.FindControl("MP_MARID")).Text;
                    double length = Convert.ToDouble(((Label)Reitem.FindControl("MP_LENGTH")).Text);
                    double width = Convert.ToDouble(((Label)Reitem.FindControl("MP_WIDTH")).Text);
                    string tuhao = ((Label)Reitem.FindControl("MP_TUHAO")).Text;
                    double num = Convert.ToDouble(((Label)Reitem.FindControl("MP_NUMBER")).Text);
                    double fznum = Convert.ToDouble(((Label)Reitem.FindControl("MP_FZNUM")).Text);
                    string note = ((Label)Reitem.FindControl("MP_NOTE")).Text;
                    string time = ((Label)Reitem.FindControl("MP_TIMERQ")).Text;
                    string ptcode = pocode + "_" + (Reitem.ItemIndex + 1).ToString().PadLeft(4, '0');
                    CheckBox cbx = Reitem.FindControl("CHK") as CheckBox;
                    if (cbx.Checked)
                    {
                        sqltext = "INSERT INTO TBPC_OTPURPLAN(MP_PCODE,MP_PTCODE,MP_MARID,MP_WIDTH,MP_LENGTH,MP_TUHAO,MP_NUMBER,MP_FZNUM,MP_NOTE,MP_TIMERQ) " +
                                  "VALUES('" + pocode + "','" + ptcode + "','" + marid + "'," + width + "," +
                                   "" + length + ",'" + tuhao + "'," + num + "," + fznum + ",'" + note + "','" + time + "')";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
                Response.Redirect("~/PC_Data/PC_TBPC_Otherpur_Bill_List.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要复制的数据！');", true);
            }
        }

        //取消计划
        protected void btnqx_Click(object sender, EventArgs e)
        {

            string sqltext = "";
            string shape1 = "";
            string bh = "";
            shape1 = lb_shape.Text;
            bh = tb_pid.Text + "_BG" + "_" + "1";

            string sql = " select * from TBPC_OTPURRVW where MP_PCODE like 'BG_%" + tb_pid.Text + "' and MP_PCODE!='BG_" + tb_pid.Text + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                sql = " SELECT  TOP (1) MP_PCODE,SUBSTRING(MP_PCODE, CHARINDEX('_', MP_PCODE) + 1, CHARINDEX('_', MP_PCODE, 4) - CHARINDEX('_', MP_PCODE) - 1) AS num ";
                sql += " FROM   TBPC_OTPURRVW  where MP_PCODE like 'BG_%" + tb_pid.Text + "' and MP_PCODE!='BG_" + tb_pid.Text + "' ";
                sql += " ORDER BY SUBSTRING(MP_PCODE, CHARINDEX('_', MP_PCODE) + 1, CHARINDEX('_', MP_PCODE, 4) - CHARINDEX('_', MP_PCODE) - 1) DESC ";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                double num = Convert.ToDouble(dt.Rows[0]["num"].ToString()) + 1;

                bh = tb_pid.Text + "_BG" + "_" + num.ToString();

            }


            if (isselected() && isquxiao(bh))
            {
                sqltext = "INSERT INTO TBPC_OTPURRVW(MP_PCODE,MP_SUBMITID,MP_SUBMITTM,MP_USEDEPID,MP_PJID,MP_ENGID,MP_SHAPE,MP_SQRENID,MP_REVIEWA,MP_NOTE) " +
                                          "VALUES('" + bh + "' ,'" + Session["UserID"].ToString() + "'," +
                                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                          "'" + Session["UserDeptID"].ToString() + "','" + tb_pjid.Text + "','" + tb_engid.Text + "','" + shape1 + "','" + Session["UserID"].ToString() + "','" + Tb_fuzirenid.Text + "','" + tb_note.Text + "')";
                DBCallCommon.ExeSqlText(sqltext);
                foreach (RepeaterItem Reitem in tbpc_otherpurbill_lookRepeater.Items)
                {
                    string marid = ((Label)Reitem.FindControl("MP_MARID")).Text;
                    double length = Convert.ToDouble(((Label)Reitem.FindControl("MP_LENGTH")).Text);
                    double width = Convert.ToDouble(((Label)Reitem.FindControl("MP_WIDTH")).Text);
                    string tuhao = ((Label)Reitem.FindControl("MP_TUHAO")).Text;
                    double num = 0 - Convert.ToDouble(((Label)Reitem.FindControl("MP_NUMBER")).Text);
                    double fznum = Convert.ToDouble(((Label)Reitem.FindControl("MP_FZNUM")).Text);
                    string note = ((Label)Reitem.FindControl("MP_NOTE")).Text;
                    string time = ((Label)Reitem.FindControl("MP_TIMERQ")).Text;
                    string ptcode = ((Label)Reitem.FindControl("MP_PTCODE")).Text;
                    CheckBox cbx = Reitem.FindControl("CHK") as CheckBox;
                    if (cbx.Checked)
                    {

                        sqltext = "INSERT INTO TBPC_OTPURPLAN(MP_PCODE,MP_PTCODE,MP_MARID,MP_WIDTH,MP_LENGTH,MP_TUHAO,MP_NUMBER,MP_FZNUM,MP_NOTE,MP_TIMERQ) " +
                                      "VALUES('" + bh + "','" + ptcode + "','" + marid + "'," + width + "," +
                                       "" + length + ",'" + tuhao + "'," + num + "," + fznum + ",'" + note + "','" + time + "')";
                        DBCallCommon.ExeSqlText(sqltext);
                    }

                }
                Response.Redirect("~/PC_Data/PC_TBPC_Otherpur_Bill_List.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要复制的数据或者已经取消过该计划不能重复操作！');", true);
            }

        }


        //判断是否选择数据
        protected bool isselected()
        {
            int count = 0;
            bool temp = false;
            foreach (RepeaterItem Reitem in tbpc_otherpurbill_lookRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CHK") as CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                temp = true;
            }
            return temp;
        }

        protected void selectall_CheckedChanged(object sender, EventArgs e)
        {
            if (selectall.Checked)
            {
                foreach (RepeaterItem Reitem in tbpc_otherpurbill_lookRepeater.Items)
                {
                    CheckBox cbx = Reitem.FindControl("CHK") as CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = true;
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Reitem in tbpc_otherpurbill_lookRepeater.Items)
                {
                    CheckBox cbx = Reitem.FindControl("CHK") as CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = false;
                    }
                }
            }
        }

        //审批同意则发给下一审批人或者通知制单人，驳回则发送给制单人
        private void SendMail(string subject, string body_title, string sendto)
        {
            string body = body_title +
                        "\r\n编号：" + tb_pid.Text.ToString() +
                        "\r\n提交日期：" + Tb_shijian.Text.ToString() +
                        "\r\n项目名称：" + tb_pjinfo.Text.Trim() +
                        "\r\n工程名称：" + tb_enginfo.Text.Trim();
            string sql = "select DISTINCT [EMAIL] from TBDS_STAFFINFO where ST_NAME='" + sendto + "'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                DBCallCommon.SendEmail(dt.Rows[0][0].ToString(), null, null, subject, body);
            }
        }

        /// <summary>
        /// 采购申请导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_ExportExcel_OnClick(object sender, EventArgs e)
        {
            string type = lb_shape.Text.Trim();
            string mpno = tb_pid.Text.Trim();
            //  TM_Data.ExportTMDataFromDB.ExportPurPlan(type, mpno);
        }


        //判断是否已经下推过取消计划
        protected bool isquxiao(string ph)
        {
            bool temp1 = true;
            string sql = " select * from TBPC_OTPURRVW where MP_PCODE='" + ph + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                temp1 = false;
            }
            return temp1;
        }
    }
}
