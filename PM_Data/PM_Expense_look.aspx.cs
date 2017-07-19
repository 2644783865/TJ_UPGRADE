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
using System.Collections.Generic;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Expense_look : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_docunum.Text = Server.UrlDecode(Request.QueryString["id"].ToString());//编号
                ViewState["action"] = Request.QueryString["action"];
                initpagemess();
                Bind_Audit_Info();//加载审核信息
                PM_GongShi_List_Repeaterdatabind();
                Control_Enable();
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
            }
            Get_SHR();
        }
        private void initpagemess()
        {
            //string sqltext = "select DATEYEAR,DATEMONTH,SPZT from TBMP_GS_LIST where DOCUNUM='" + txt_docunum.Text.Trim() + "'";
            //SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            //while (dr.Read())
            //{
            //    string year = dr["DATEYEAR"].ToString();
            //    string month = dr["DATEMONTH"].ToString();
            //    string date = year + '.' + month;
            //    txt_date.Text = date;
            //    lbl_spzt.Text = dr["SPZT"].ToString();
            //}
            //dr.Close();
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
                        string pa_code = txt_docunum.Text.Trim();
                        string pa_fir_per = firstid.Value;
                        string pa_sec_per = secondid.Value;
                        string pa_thi_per = thirdid.Value;

                        //审核人不能相同
                        if (((pa_fir_per != pa_sec_per) && (pa_sec_per != pa_thi_per)) || ((pa_sec_per == "") && (pa_thi_per == "")))
                        {
                            string sqltext = "insert into View_TBMP_GS_AUDIT (DOCUNUM,Fir_Per,Fir_Jg,Sec_Per,Sec_Jg,Thi_Per,Thi_Jg,Rank)" +
                                " values ('" + pa_code + "','" + pa_fir_per + "','0','" + pa_sec_per + "','0','" + pa_thi_per + "','0','" + rblSHDJ.SelectedIndex.ToString() + "')";
                            sqlstr.Add(sqltext);
                            string sqlupdatezt = "update TBMP_GS_LIST set SPZT='1' where DOCUNUM='" + txt_docunum.Text.ToString() + "'";
                            sqlstr.Add(sqlupdatezt);
                            DBCallCommon.ExecuteTrans(sqlstr);

                            //制单人提交后向第一个审批人发送邮件
                            subject = "您有新的采购计划需要审批——" + txt_docunum.Text.ToString();
                            sendto = txt_first.Text.Trim();
                            this.SendMail(subject, body_title, sendto);

                            Response.Redirect("../PM_Data/PM_GongShi_List.aspx");
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
                        string pa_code = txt_docunum.Text.Trim();
                        string pa_fir_per = firstid.Value;
                        string pa_sec_per = secondid.Value;
                        string pa_thi_per = thirdid.Value;

                        //审核人不能相同
                        if (((pa_fir_per != pa_sec_per) && (pa_sec_per != pa_thi_per)) || ((pa_sec_per == "") && (pa_thi_per == "")))
                        {
                            string update1 = "update TBMP_GS_AUDIT set Fir_Per='" + pa_fir_per + "',Fir_Jg='0',Fir_Yj='',Fir_Sj='',Sec_Per='" + pa_sec_per + "',Sec_Jg='0'" +
                            " ,Sec_Yj='',Sec_Sj='',Thi_Per='" + pa_thi_per + "',Thi_Jg='0',Thi_Yj='',Thi_Sj='',Rank='" + rblSHDJ.SelectedIndex.ToString() + "' where DOCUNUM='" + txt_docunum.Text.ToString() + "'";
                            string update2 = "update TBMP_GS_LIST set SPZT='1' where DOCUNUM='" + txt_docunum.Text.ToString() + "'";
                            sqlstr.Add(update1);
                            sqlstr.Add(update2);
                            DBCallCommon.ExecuteTrans(sqlstr);

                            //制单人提交后向第一个审批人发送邮件
                            subject = "驳回后的采购计划已修改，请重新审批——" + txt_docunum.Text.ToString();
                            sendto = txt_first.Text.Trim();
                            this.SendMail(subject, body_title, sendto);

                            Response.Redirect("../PM_Data/PM_GongShi_List.aspx");
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
                    set_text = "set Fir_Yj='" + spyj + "',Fir_Sj='" + spsj + "',Fir_Jg='" + spjg + "' ";

                    submit_type = "1";
                }
                else if (Session["UserID"].ToString() == secondid.Value.ToString())
                {
                    spyj = second_opinion.Text.ToString();
                    spjg = rbl_second.SelectedValue.ToString();
                    set_text = "set Sec_Yj='" + spyj + "',Sec_Sj='" + spsj + "',Sec_Jg='" + spjg + "' ";

                    submit_type = "2";
                }
                else if (Session["UserID"].ToString() == thirdid.Value.ToString())
                {
                    spyj = third_opinion.Text.ToString();
                    spjg = rbl_third.SelectedValue.ToString();
                    set_text = "set Thi_Yj='" + spyj + "',Thi_Sj='" + spsj + "',Thi_Jg='" + spjg + "' ";

                    submit_type = "3";
                }

                if (spjg == "" || spjg == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择【同意】或【不同意】！');", true); return;
                }
                List<string> sqlstr = new List<string>();
                string sqltext = "update TBMP_GS_AUDIT " + set_text + " where DOCUNUM='" + txt_docunum.Text.ToString() + "'";
                sqlstr.Add(sqltext);

                //更新审批状态，0：保存；1：提交未审批；2：审批中；3：已通过；4：已驳回；
                string spzt = (spjg == "1") ? "2" : "4";
                string sqlupdatezt = "update TBMP_GS_LIST set SPZT='" + spzt + "'where DOCUNUM='" + txt_docunum.Text.ToString() + "'";
                sqlstr.Add(sqlupdatezt);
                DBCallCommon.ExecuteTrans(sqlstr);

                //如果同意则向下一级审批人发送邮件通知
                if (spjg == "1")
                {
                    switch (submit_type)
                    {
                        case "1":
                            subject = "您有新的采购计划需要审批，请即时处理——" + txt_docunum.Text.ToString();
                            sendto = txt_second.Text.Trim();
                            break;
                        case "2":
                            subject = "您有新的采购计划需要审批，请即时处理——" + txt_docunum.Text.ToString();
                            sendto = txt_third.Text.Trim();
                            break;
                    }
                    if (sendto != "")
                    {
                        this.SendMail(subject, body_title, sendto);
                    }

                }
                ////如果驳回则向制单人发送邮件通知
                //else if (spjg == "2")
                //{
                //    subject = "采购计划已驳回——" + txt_docunum.Text.ToString();
                //    body_title = "您提交的采购申请于" + DateTime.Now.ToString() + "被驳回";
                //    sendto = tb_executor.Text.Trim();
                //    this.SendMail(subject, body_title, sendto);
                //}

                //检查是否全部同意，如果全部同意则改为3。根据审核等级判断，只判断最后一个是否同意即可
                bool YesOrNo = false;
                string sqlJG = "select * from TBMP_GS_AUDIT where DOCUNUM='" + txt_docunum.Text.ToString() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlJG);
                if (dt.Rows.Count > 0)
                {
                    switch (rblSHDJ.SelectedValue.ToString())
                    {
                        case "1":
                            if (dt.Rows[0]["Fir_Jg"].ToString() == "1")
                            { YesOrNo = true; }
                            break;
                        case "2":
                            if (dt.Rows[0]["Sec_Jg"].ToString() == "1")
                            { YesOrNo = true; }

                            break;
                        case "3":
                            if (dt.Rows[0]["Thi_Jg"].ToString() == "1")
                            { YesOrNo = true; }
                            break;
                    }
                }
                if (YesOrNo)
                {
                    string strupdatezp2 = "update TBMP_GS_LIST set SPZT='3' where DOCUNUM='" + txt_docunum.Text.ToString() + "'";
                    sqlstr.Clear();
                    sqlstr.Add(strupdatezp2);
                    DBCallCommon.ExecuteTrans(sqlstr);

                    //this.XiaTui(TextBox_pid.Text);

                    ////下推后通知制单人
                    //subject = "您提交的采购计划已审批并下推至采购部——" + TextBox_pid.Text.ToString();
                    //body_title = "您提交的采购申请已审批完，于" + DateTime.Now.ToString() + "下推至采购部";
                    //sendto = tb_executor.Text.Trim();
                    //this.SendMail(subject, body_title, sendto);
                }
                Response.Redirect("../PM_Data/PM_GongShi_Audit.aspx");

            }
        }

        private void Get_SHR()
        {
            //审核人1
            string sql1 = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_DEPID='04' and ST_PD='0'";
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
            string sql2 = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_DEPID='04' and ST_PD='0'";
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
            string sql3 = "select ST_ID,ST_NAME from TBDS_STAFFINFO where   ST_DEPID='04' and ST_PD='0'";
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
        //控制控件是否可用
        private void Control_Enable()
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
        private void Bind_Audit_Info()
        {
            string sqltext = "select * from View_TBMP_GS_AUDIT where DOCUNUM='" + txt_docunum.Text.ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                firstid.Value = dt.Rows[0]["Fir_Per"].ToString();
                txt_first.Text = dt.Rows[0]["name1"].ToString();
                if (dt.Rows[0]["Fir_Jg"].ToString() != "0")
                { rbl_first.SelectedValue = dt.Rows[0]["Fir_Jg"].ToString(); }
                first_time.Text = dt.Rows[0]["Fir_Sj"].ToString();
                first_opinion.Text = dt.Rows[0]["Fir_Yj"].ToString();

                secondid.Value = dt.Rows[0]["Sec_Per"].ToString();
                txt_second.Text = dt.Rows[0]["name2"].ToString();
                if (dt.Rows[0]["Sec_Jg"].ToString() != "0")
                { rbl_second.SelectedValue = dt.Rows[0]["Sec_Jg"].ToString(); }
                second_time.Text = dt.Rows[0]["Sec_Sj"].ToString();
                second_opinion.Text = dt.Rows[0]["Sec_Yj"].ToString();

                thirdid.Value = dt.Rows[0]["Thi_Per"].ToString();
                txt_third.Text = dt.Rows[0]["name3"].ToString();
                if (dt.Rows[0]["Thi_Jg"].ToString() != "0")
                { rbl_third.SelectedValue = dt.Rows[0]["Thi_Jg"].ToString(); }
                third_time.Text = dt.Rows[0]["Thi_Sj"].ToString();
                third_opinion.Text = dt.Rows[0]["Thi_Yj"].ToString();
                Control_SHLC();
            }
        }
        private void Control_SHLC()
        {
            string sqltext = "select * from View_TBMP_GS_AUDIT where DOCUNUM='" + txt_docunum.Text.ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                if (lbl_spzt.Text == "0" || lbl_spzt.Text == "4")
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
                    if (dt.Rows[0]["Fir_Jg"].ToString() != "1")
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
                    if (dt.Rows[0]["Sec_Jg"].ToString() != "1")
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
                rblSHDJ.SelectedIndex = Convert.ToInt32(dt.Rows[0]["Rank"].ToString());
                this.rblSHDJ_Changed(null, null);
            }
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
        private void SendMail(string subject, string body_title, string sendto)
        {
            string body = body_title +
                        "\r\n编号：" + txt_docunum.Text.ToString() +
                        "\r\n提交日期：" + txt_date.Text.ToString();
            string sql = "select DISTINCT [EMAIL] from TBDS_STAFFINFO where ST_NAME='" + sendto + "'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                DBCallCommon.SendEmail(dt.Rows[0][0].ToString(), null, null, subject, body);
            }
        }
        private void PM_GongShi_List_Repeaterdatabind()
        {
            string sqltext = "select a.*,b.CM_CONTR,b.TSA_MAP,c.CM_CUSNAME from TBMP_GS_LIST AS a LEFT JOIN TBCM_FHBASIC AS b ON a.TSA_ID=b.TSA_ID LEFT JOIN TBCM_APPLICA AS c ON b.CM_CONTR=c.CM_CONTR where a.DOCUNUM='" + txt_docunum.Text.ToString() + "'";
            DBCallCommon.BindRepeater(PM_GongShi_List_Repeater, sqltext);
        }
    }
}
