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
using System.IO;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Finished_out_look : System.Web.UI.Page
    {
        List<string> list = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                txt_docnum.Text = Server.UrlDecode(Request.QueryString["docnum"].ToString());//编号
                ViewState["action"] = Request.QueryString["action"];
                initpagemess();
                Bind_Audit_Info();//加载审核信息
                this.PM_Finished_lookRepeaterdatabind();
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
            string sqltext = "select * from View_TBMP_FINISHED_OUT where TFO_DOCNUM='" + txt_docnum.Text.Trim() + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                Tb_shijian.Text = dr["OUTDATE"].ToString();
                txt_note.Text = dr["NOTE"].ToString();
                Tb_fuziren.Text = dr["REVIEWANAME"].ToString();
                Tb_fuzirenid.Text = dr["REVIEWA"].ToString();
                Tb_shenqingren.Text = dr["SQRNAME"].ToString();
                Tb_shenqingrenid.Text = dr["SQRID"].ToString();
                tb_executor.Text = dr["DocuPersonNAME"].ToString();
                tb_executorid.Text = dr["DocuPersonID"].ToString();
                lbl_spzt.Text = dr["SPZT"].ToString();
            }
            dr.Close();
            if (ViewState["action"].ToString() == "audit")
            {
                string sql = "select TFO_DOCNUM,TSA_ID,ZXDSJID from TBMP_FINISHED_OUT_Audit where TFO_DOCNUM='" + txt_docnum.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                hidsj.Value = dt.Rows[0]["ZXDSJID"].ToString();
                BindrptJBXX();
            }
            else if (ViewState["action"].ToString() == "view")
            {
                string sql = "select TFO_DOCNUM,TSA_ID,ZXDSJID from TBMP_FINISHED_OUT_Audit where TFO_DOCNUM='" + txt_docnum.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["ZXDSJID"].ToString() != "")
                    {
                        hidsj.Value = dt.Rows[0]["ZXDSJID"].ToString();
                        BindrptJBXX();
                    }
                    else
                    {
                        hidsj.Value = DateTime.Now.ToString("yyyy-MM-dd:HH-mm-ss-fff");
                    }
                }
                else
                {
                    hidsj.Value = DateTime.Now.ToString("yyyy-MM-dd:HH-mm-ss-fff");
                }
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
        //提交审批
        protected void btn_Audit_Click(object sender, EventArgs e)
        {

            //上传装箱单
            if (ViewState["action"].ToString() == "view")
            {
                if (rptJBXX.Items.Count < 1)
                {
                    Response.Write("<script>alert('您未上传装箱单，请上传装箱单后再提交审批！！！')</script>");
                    return;
                }
            }

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
                        string pa_code = txt_docnum.Text.Trim();
                        string pa_fir_per = firstid.Value;
                        string pa_sec_per = secondid.Value;
                        string pa_thi_per = thirdid.Value;

                        //审核人不能相同
                        if (((pa_fir_per != pa_sec_per) && (pa_sec_per != pa_thi_per)) || ((pa_sec_per == "") && (pa_thi_per == "")))
                        {
                            string sqltext = "insert into TBMP_FINISHED_OUT_Audit (TFO_DOCNUM,Fir_Per,Fir_Jg,Sec_Per,Sec_Jg,Thi_Per,Thi_Jg,Rank,ZXDSJID)" +
                                " values ('" + pa_code + "','" + pa_fir_per + "','0','" + pa_sec_per + "','0','" + pa_thi_per + "','0','" + rblSHDJ.SelectedIndex.ToString() + "','"+hidsj.Value+"')";
                            sqlstr.Add(sqltext);
                            string sqlupdatezt = "update TBMP_FINISHED_OUT set SPZT='1' where TFO_DOCNUM='" + txt_docnum.Text.ToString() + "'";
                            sqlstr.Add(sqlupdatezt);
                            DBCallCommon.ExecuteTrans(sqlstr);

                            //制单人提交后向第一个审批人发送邮件
                            subject = "您有新的成品出库需要审批——" + txt_docnum.Text.ToString();
                            sendto = txt_first.Text.Trim();
                            this.SendMail(subject, body_title, sendto);
                            Response.Write("<script>alert('保存成功！！！');window.close();</script>");
                            //Response.Redirect("../PM_Data/PM_FINISHED_OUT.aspx");
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
                        string pa_code = txt_docnum.Text.Trim();
                        string pa_fir_per = firstid.Value;
                        string pa_sec_per = secondid.Value;
                        string pa_thi_per = thirdid.Value;

                        //审核人不能相同
                        if (((pa_fir_per != pa_sec_per) && (pa_sec_per != pa_thi_per)) || ((pa_sec_per == "") && (pa_thi_per == "")))
                        {
                            string update1 = "update TBMP_FINISHED_OUT_Audit set Fir_Per='" + pa_fir_per + "',Fir_Jg='0',Fir_Yj='',Fir_Sj='',Sec_Per='" + pa_sec_per + "',Sec_Jg='0'" +
                            " ,Sec_Yj='',Sec_Sj='',Thi_Per='" + pa_thi_per + "',Thi_Jg='0',Thi_Yj='',Thi_Sj='',Rank='" + rblSHDJ.SelectedIndex.ToString() + "' where TFO_DOCNUM='" + txt_docnum.Text.ToString() + "'";
                            string update2 = "update TBMP_FINISHED_OUT set SPZT='1' where TFO_DOCNUM='" + txt_docnum.Text.ToString() + "'";
                            sqlstr.Add(update1);
                            sqlstr.Add(update2);
                            DBCallCommon.ExecuteTrans(sqlstr);

                            //制单人提交后向第一个审批人发送邮件
                            subject = "驳回后的成品出库已修改，请重新审批——" + txt_docnum.Text.ToString();
                            sendto = txt_first.Text.Trim();
                            this.SendMail(subject, body_title, sendto);
                            Response.Write("<script>alert('保存成功！！！');window.close();</script>");
                            //Response.Redirect("../PM_Data/PM_FINISHED_OUT.aspx");
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
                // string spsj = DateTime.Now.ToShortDateString();//审批时间
                string spsj = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
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
                string sqltext = "update TBMP_FINISHED_OUT_Audit " + set_text + " where TFO_DOCNUM='" + txt_docnum.Text.ToString() + "'";
                sqlstr.Add(sqltext);

                //更新审批状态，0：保存；1：提交未审批；2：审批中；3：已通过；4：已驳回；
                string spzt = (spjg == "1") ? "2" : "4";
                string sqlupdatezt = "update TBMP_FINISHED_OUT set SPZT='" + spzt + "'where TFO_DOCNUM='" + txt_docnum.Text.ToString() + "'";
                sqlstr.Add(sqlupdatezt);
                DBCallCommon.ExecuteTrans(sqlstr);

                //如果同意则向下一级审批人发送邮件通知
                if (spjg == "1")
                {
                    switch (submit_type)
                    {
                        case "1":
                            subject = "您有新的成品出库需要审批，请即时处理——" + txt_docnum.Text.ToString();
                            sendto = txt_second.Text.Trim();
                            break;
                        case "2":
                            subject = "您有新的成品出库需要审批，请即时处理——" + txt_docnum.Text.ToString();
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
                    subject = "成品出库已驳回——" + txt_docnum.Text.ToString();
                    body_title = "您提交的成品出库于" + DateTime.Now.ToString() + "被驳回";
                    sendto = tb_executor.Text.Trim();
                    this.SendMail(subject, body_title, sendto);

                }

                //检查是否全部同意，如果全部同意则改为3。根据审核等级判断，只判断最后一个是否同意即可
                bool YesOrNo = false;
                string sqlJG = "select * from TBMP_FINISHED_OUT_Audit where TFO_DOCNUM='" + txt_docnum.Text.ToString() + "'";
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
                    // 给王艳辉发邮件
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("202"), new List<string>(), new List<string>(), "成品出库", "任务号“" + txt_docnum.Text.ToString() + "已经出库，请上系统查看！！！");
                    string strupdatezp2 = "update TBMP_FINISHED_OUT set SPZT='3' where TFO_DOCNUM='" + txt_docnum.Text.ToString() + "'";
                    sqlstr.Clear();
                    sqlstr.Add(strupdatezp2);
                    DBCallCommon.ExecuteTrans(sqlstr);
                    foreach (RepeaterItem Reitem in PM_Finished_lookRepeater.Items)
                    {
                        int cknum = Convert.ToInt32(((Label)Reitem.FindControl("lblNumber")).Text);//本次出库数量
                        // int kcnum = Convert.ToInt16(((Label)Reitem.FindControl("lblkcnum")).Text);
                        string tsaid = ((Label)Reitem.FindControl("lblTsa")).Text;
                        string engname = ((Label)Reitem.FindControl("lblEngname")).Text;
                        string map = ((Label)Reitem.FindControl("lblMap")).Text;
                        string fid = ((Label)Reitem.FindControl("lblfid")).Text;
                        string bianhao = ((Label)Reitem.FindControl("lblbianhao")).Text;
                        string cmid = ((Label)Reitem.FindControl("CM_ID")).Text;
                        string zongxu = ((Label)Reitem.FindControl("TFO_ZONGXU")).Text;
                        string sqltxt1 = "update TBCM_FHBASIC set CM_YFHNUM=" + cknum + " where CM_ID='" + cmid + "' and CM_FID='" + fid + "' and CM_STATUS='4' ";
                        DBCallCommon.ExeSqlText(sqltxt1);
                        string sqltxt = "select * from VIEW_CM_FAHUO where CM_FID='" + fid + "'and ID='" + zongxu + "' and CM_ID='" + cmid + "' and CM_STATUS='4' ";
                        DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sqltxt);
                        if (dt3.Rows.Count > 0)
                        {
                            int num_1 = Convert.ToInt32(dt3.Rows[0]["TSA_NUMBER"].ToString());
                            int yfhnum = Convert.ToInt32(dt3.Rows[0]["CM_YFHNUM"].ToString());
                            if (num_1 == yfhnum)
                            {
                                string sqltxt2 = "update TBCM_FHBASIC set CM_STATUS='6' where CM_ID='" + cmid + "' and CM_FID='" + fid + "' and CM_STATUS='4' ";//发货通知下该条设备出库已完结
                                list.Add(sqltxt2);
                            }

                        }

                        string sqltxt5 = "select * from TBMP_FINISHED_STORE where KC_TSA='" + tsaid + "' and KC_ZONGXU='" + zongxu + "'";
                        DataTable dt4 = DBCallCommon.GetDTUsingSqlText(sqltxt5);
                        if (dt4.Rows.Count > 0)
                        {
                            int kcnum = Convert.ToInt32(dt4.Rows[0]["KC_KCNUM"].ToString());
                            int ycknum = Convert.ToInt32(dt4.Rows[0]["KC_CKNUM"].ToString());//获取已出库数量
                            int talnum = Convert.ToInt32(dt4.Rows[0]["KC_TALNUM"].ToString());
                            int kc = kcnum - cknum;
                            int ck = ycknum + cknum;
                            kc = kc < 0 ? 0 : kc;
                            ck = ck > talnum ? talnum : ck;
                            string sqltxt3 = "update TBMP_FINISHED_STORE  set KC_KCNUM=" + kc + ",KC_CKNUM=" + ck + " where KC_TSA='" + tsaid + "' and KC_ZONGXU='" + zongxu + "'";

                            list.Add(sqltxt3);
                            string sqltxt4 = "update TBMP_FINISHED_STORE  set KC_KCNUM=case when (KC_KCNUM-KC_SINGNUMBER*" + cknum + ")<0 then 0 else KC_KCNUM-KC_SINGNUMBER*" + cknum + " end ,KC_CKNUM=case when (KC_CKNUM+KC_SINGNUMBER*" + cknum + ")>KC_TALNUM then KC_TALNUM else KC_CKNUM+KC_SINGNUMBER*" + cknum + " end where KC_TSA='" + tsaid + "' and KC_ZONGXU like '" + zongxu + ".%'";
                            list.Add(sqltxt4);
                        }
                        DBCallCommon.ExecuteTrans(list);




                        sqltext = "select BM_ZONGXU,case when KC_CKNUM is null then 0 else KC_CKNUM end,case when KC_KCNUM is null then 0 else KC_KCNUM end from TBPM_STRINFODQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU where BM_ENGID='" + tsaid + "' and dbo.Splitnum(BM_ZONGXU,'.')=0";

                        DataTable dtzx = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dtzx.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtzx.Rows.Count; i++)
                            {
                                sqltext = "select min((case when KC_CKNUM is null then 0 else KC_CKNUM end )/BM_SINGNUMBER),min((case when KC_KCNUM is null then 0 else KC_KCNUM end )/BM_SINGNUMBER) from TBPM_STRINFODQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU where BM_ENGID='" + tsaid + "' and BM_ZONGXU like '" + dtzx.Rows[i][0] + ".%' and (BM_KU<>'' or BM_MARID='' )";
                                dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                                //double ccknum = Math.Floor(float.Parse(dt.Rows[0][0].ToString()));
                                //double kcnum = Math.Floor(float.Parse(dt.Rows[0][1].ToString()));
                                double ccknum = Math.Floor(CommonFun.ComTryDouble(dt.Rows[0][0].ToString()));
                                double kcnum = Math.Floor(CommonFun.ComTryDouble(dt.Rows[0][1].ToString()));

                                if (double.Parse(dtzx.Rows[i][1].ToString()) < ccknum || double.Parse(dtzx.Rows[i][2].ToString()) > kcnum)
                                {
                                    
                                        sqltext = "update TBMP_FINISHED_STORE set KC_CKNUM=" + ccknum + ",KC_KCNUM=" + kcnum + " where KC_TSA='" + tsaid + "' and  KC_ZONGXU='" + dtzx.Rows[i][0].ToString() + "'";
                                       DBCallCommon.ExeSqlText(sqltext);
                                    
                                }

                            }
                        }
                    }
                }
                Response.Write("<script>alert('保存成功！！！');</script>");
                Response.Redirect("../PM_Data/PM_FINISHED_OUT_Audit.aspx");

            }
        }

        private void Get_SHR()
        {
            //审核人1
            string sql1 = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_DEPID='07' and ST_PD='0'";
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
            //string sql2 = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_DEPID='04' and ST_PD='0'";
            string sql2 = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_POSITION='0708'";
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
            string sql3 = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_DEPID='04' and ST_PD='0'";
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
            string sqltext = "select * from View_TBMP_FINISHED_OUT_Audit where TFO_DOCNUM='" + txt_docnum.Text.ToString() + "'";
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
            string sqltext = "select * from View_TBMP_FINISHED_OUT_Audit where TFO_DOCNUM='" + txt_docnum.Text.ToString() + "'";
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
                        "\r\n编号：" + txt_docnum.Text.ToString() +
                        "\r\n提交日期：" + Tb_shijian.Text.ToString();
            string sql = "select DISTINCT [EMAIL] from TBDS_STAFFINFO where ST_NAME='" + sendto + "'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                DBCallCommon.SendEmail(dt.Rows[0][0].ToString(), null, null, subject, body);
            }
        }
        private void PM_Finished_lookRepeaterdatabind()
        {
            string sqltext = "select B.CM_ID, B.CM_PROJ,B.CM_FHNUM,B.CM_BIANHAO,B.CM_FID,B.CM_CONTR,A.TSA_ID,B.TSA_MAP,B.TSA_ENGNAME,B.TSA_NUMBER,B.CM_JHTIME,A.*,c.* from TBMP_FINISHED_OUT AS A LEFT OUTER JOIN View_CM_FaHuo AS B ON (A.TSA_ID=B.TSA_ID AND A.TFO_ENGNAME=B.TSA_ENGNAME AND A.TFO_MAP=B.TSA_MAP AND A.TFO_FID=B.CM_FID AND A.TFO_ZONGXU=B.ID)  left join TBMP_FINISHED_STORE as c on a.TSA_ID=c.KC_TSA and a.TFO_ZONGXU=KC_ZONGXU where A.TFO_DOCNUM='" + txt_docnum.Text.ToString() + "'";
            DBCallCommon.BindRepeater(PM_Finished_lookRepeater, sqltext);
        }

        #region 上传装箱单附件
        protected void btnFU1_OnClick(object sender, EventArgs e)
        {
            int IntIsUF = 0;//判断用户是否上传了文件
            //获取文件保存的路径
            string FilePath = @"E:\装箱单\" + Convert.ToString(System.DateTime.Now.Year);
            if (!Directory.Exists(FilePath))//如果不存在文件路径就创建一个
            {
                Directory.CreateDirectory(FilePath);
            }
            //对客户端已上载的单独文件的访问
            HttpPostedFile UserHPF = FileUpload2.PostedFile;
            try
            {
                string fileContentType = UserHPF.ContentType;// 获取客户端发送的文件的 MIME 内容类型 
                if (fileContentType == "application/vnd.ms-excel" || fileContentType == "application/msword" || fileContentType == "application/pdf" || fileContentType == "application/octet-stream" || fileContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || fileContentType == "image/bmp" || fileContentType == "image/gif" || fileContentType == "image/jpeg" || fileContentType == "image/jpg" || fileContentType == "image/pjpeg")//传送文件类型
                {
                    if (UserHPF.ContentLength > 0)
                    {
                        //调用GetAutoID方法获取上传文件自动编号
                        //int IntFieldID = CC.GetAutoID("fileID", "tb_files");
                        //文件的真实名（格式：[文件编号]上传文件名）
                        //用于实现上传多个相同文件时，原有文件不被覆盖
                        string strOldFile = System.IO.Path.GetFileName(UserHPF.FileName);
                        string strExtent = strOldFile.Substring(strOldFile.LastIndexOf("."));
                        string strNewFile = System.DateTime.Now.ToString("yyyyMMddHHmmss") + strExtent;
                        if (!File.Exists(FilePath + "//" + strNewFile))
                        {
                            //定义插入字符串，将上传文件信息保存在数据库中
                            string sqlStr = "insert into CM_FILES(FILE_FATHERID,FILE_LOAD,FILE_UPDATE,FILE_NAME,FILE_SHOWNAME,FILE_TYPE)";
                            sqlStr += "values('" + hidsj.Value + "'";
                            sqlStr += ",'" + FilePath + "'";
                            sqlStr += ",'" + DateTime.Now.ToString("yyyy年MM月dd日") + "'";
                            sqlStr += ",'" + strNewFile + "','" + strOldFile + "',";
                            sqlStr += "'CM_ZXD_C')";
                            //打开与数据库的连接
                            DBCallCommon.ExeSqlText(sqlStr);
                            UserHPF.SaveAs(FilePath + "//" + strNewFile);//将上传的文件存放在指定的文件夹中
                            IntIsUF = 1;
                        }
                        else
                        {
                            filesError2.Visible = true;
                            filesError2.Text = "文件名与服务器某个合同名重名，请您核对后重新上传！";
                            IntIsUF = 1;
                        }
                    }
                }
                else
                {
                    filesError2.Visible = true;
                    filesError2.Text = "文件类型不符合要求，请您核对后重新上传！";
                    IntIsUF = 1;
                }
            }
            catch
            {
                filesError2.Text = "文件上传过程中出现错误！";
                filesError2.Visible = true;
                return;
            }
            if (IntIsUF == 1)
            {
                IntIsUF = 0;
            }
            else
            {
                filesError2.Visible = true;
                filesError2.Text = "请选择上传文件!";
            }
            BindrptJBXX();
        }

        private void BindrptJBXX()
        {
            string sql = "select * from CM_FILES where FILE_FATHERID='" + hidsj.Value + "' and FILE_TYPE='CM_ZXD_C'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            rptJBXX.DataSource = dt;
            rptJBXX.DataBind();
        }

        protected void lbtndelete2_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            //获取文件真实姓名
            string sqlStr = "select FILE_LOAD,FILE_NAME from CM_FILES where FILE_ID='" + id + "'";
            //在文件夹Files下，删除该文件
            //打开数据库
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取指定文件的路径
            string strFilePath = ds.Tables[0].Rows[0]["FILE_LOAD"].ToString() + @"\" + ds.Tables[0].Rows[0]["FILE_NAME"].ToString();
            //调用File类的Delete方法，删除指定文件
            if (System.IO.File.Exists(strFilePath))
            {
                File.Delete(strFilePath);//文件不存在也不会引发异常
            }
            string sqlDelStr = "delete from CM_FILES where FILE_ID='" + id + "'";//删除数据库中的记录
            DBCallCommon.ExeSqlText(sqlDelStr);
            BindrptJBXX();//删除添加的记录
            //GVBind(ViewGridViewFiles);//删除查看的记录
        }

        protected void lbtnonload2_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            string sqlStr = "select FILE_LOAD,FILE_NAME,FILE_SHOWNAME from CM_FILES where FILE_ID='" + id + "'";
            //打开数据库
            //Response.Write(sqlStr);         
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取文件路径
            string strFilePath = ds.Tables[0].Rows[0]["FILE_LOAD"].ToString() + @"\" + ds.Tables[0].Rows[0]["FILE_NAME"].ToString();
            //Response.Write(strFilePath);
            if (File.Exists(strFilePath))
            {
                System.IO.FileInfo file = new System.IO.FileInfo(strFilePath);
                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(ds.Tables[0].Rows[0]["FILE_NAME"].ToString()));
                Response.AppendHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(file.FullName);
                Response.Flush();
                Response.End();
            }
            else
            {
                filesError2.Visible = true;
                filesError2.Text = "文件已被删除，请通知相关人员上传文件！";
            }
        }
        #endregion
    }
}
