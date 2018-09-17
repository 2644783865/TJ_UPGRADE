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
    public partial class PM_Finished_look : System.Web.UI.Page
    {
        string id;
        string docnum = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            docnum = Request.QueryString["docnum"];
            id = Request.QueryString["id"];
            lbl_docnum.Text = docnum;
            if (!IsPostBack)
            {
                //tb_tsaid.Text = Server.UrlDecode(Request.QueryString["id"].ToString());//编号
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
                //Bindcbx();
            }
            Get_SHR();
        }
        private void initpagemess()
        {
            string sqltext = "select * from View_TBMP_FINISHED_IN where TFI_DOCNUM='" + lbl_docnum.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {

                Tb_shijian.Text = dr["INDATE"].ToString();
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
                string sql = "select FIA_DOCNUM,TSA_ID,ZXDSJID from TBMP_FINISHED_IN_Audit where FIA_DOCNUM='" + lbl_docnum.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                hidsj.Value = dt.Rows[0]["ZXDSJID"].ToString();
                BindrptJBXX();
            }
            else if (ViewState["action"].ToString() == "view")
            {
                string sql = "select FIA_DOCNUM,TSA_ID,ZXDSJID from TBMP_FINISHED_IN_Audit where FIA_DOCNUM='" + lbl_docnum.Text + "'";
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
        //2016.6.23提交审批OnClientClick
        public string SfCzbfgx()
        {
            string tsaid = "";
            string resulttsaid = "";
            List<string> afiid = new List<string>();
            string resultafiid = "";
            string resultvalid = "";
            //检查是否存在质检未合格子项
            foreach (RepeaterItem item in PM_Finished_lookRepeater.Items)
            {
                tsaid = ((Label)item.FindControl("lblTsa")).Text;
            }
            if (tsaid != "")
            {
                string sqlquality = "select distinct AFI_ID,PTC,AFI_ENDDATE,AFI_QCMANNM from TBQM_APLYFORINSPCT as a left join TBQM_APLYFORITEM as b on a.UNIQUEID=b.UNIQUEID where PTC like '%" + tsaid + "%'and RESULT not  in ('合格','让步接收') order by AFI_ENDDATE desc ";
                DataTable dtquality = DBCallCommon.GetDTUsingSqlText(sqlquality);
                if (dtquality.Rows.Count > 0)
                {
                    string sqlselect = "";
                    DataTable dtselect;
                    DataView dvselect;
                    for (int i = 0; i < dtquality.Rows.Count; i++)
                    {
                        sqlselect = "select ISAGAIN,AFI_ID,PTC,isnull(RESULT,'')RESULT,AFI_ENDRESLUT,AFI_ENDDATE,AFI_QCMANNM from TBQM_APLYFORINSPCT as a left join TBQM_APLYFORITEM as b on a.UNIQUEID=b.UNIQUEID where PTC='" + dtquality.Rows[i]["PTC"] + "'and isnull(AFI_ENDDATE,'') =isnull((select top 1 AFI_ENDDATE from TBQM_APLYFORINSPCT as a left join TBQM_APLYFORITEM as b on a.UNIQUEID=b.UNIQUEID where PTC='" + dtquality.Rows[i]["PTC"] + "' order by AFI_ENDDATE desc),'') order by ISAGAIN desc";
                        dtselect = DBCallCommon.GetDTUsingSqlText(sqlselect);
                        if (dtselect.Rows[0]["RESULT"].ToString() != "合格" && dtselect.Rows[0]["RESULT"].ToString() != "让步接收")
                        {
                            resulttsaid = tsaid;
                            dvselect = dtselect.DefaultView;
                            dvselect.RowFilter = "RESULT not in ('合格','让步接收')";
                            for (int j = 0; j < dvselect.Count; j++)
                            {
                                if (!afiid.Contains(dvselect[j]["AFI_ID"].ToString().PadLeft(5, '0')))
                                    afiid.Add(dvselect[j]["AFI_ID"].ToString().PadLeft(5, '0'));
                            }
                        }
                    }
                    for (int j = 0; j < afiid.Count; j++)
                    {
                        resultafiid += afiid[j] + "/";
                    }
                }

            }
            return resultvalid = resulttsaid != "" ? resulttsaid + ',' + resultafiid.TrimEnd('/') : "";
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
            string resultvalid = "";
            //邮件提醒参数
            string subject = "";
            string body_title = "";
            string sendto = "";

            string tsaid = "";

            List<string> list = new List<string>();
            List<string> listTH = new List<string>();
            foreach (RepeaterItem item in PM_Finished_lookRepeater.Items)
            {

                int str_rknum = Convert.ToInt32(((TextBox)item.FindControl("lblRknum")).Text);//入库产品数量
                string zongxu = ((Label)item.FindControl("TFI_ZONGXU")).Text;
                tsaid = ((Label)item.FindControl("lblTsa")).Text;
                string id = ((Label)item.FindControl("lab_id")).Text.ToString();//唯一标识
                string sqltext = "update TBMP_FINISHED_IN set TFI_RKNUM=" + str_rknum + " where ID='" + id + "'";
                list.Add(sqltext);
                sqltext = "select * from (select a.BM_NUMBER,case when b.KC_RKNUM is null then 0 else KC_RKNUM end as RKNUM  from TBPM_STRINFODQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU where a.BM_ZONGXU='" + zongxu + "' and a.BM_ENGID='" + tsaid + "')c where BM_NUMBER-RKNUM>=" + str_rknum;
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('总序为【" + zongxu + "】的入库数量超出了最大允许入库数量，请检查');", true);
                    return;

                }

                //检查是否存在质检未合格子项
                //string sqlquality = "select AFI_ID,PTC,AFI_TSDEP,AFI_MANNM,AFI_RQSTCDATE,AFI_ENDDATE,AFI_QCMANNM,PARTNM,Marid,GUIGE,CAIZHI,RESULT,AFI_ASSGSTATE from TBQM_APLYFORINSPCT as a left join TBQM_APLYFORITEM as b on a.UNIQUEID=b.UNIQUEID left join dbo.TBMA_MATERIAL as c on b.Marid=c.ID where PTC like '%" + tsaid + "%'and RESULT not  in ('合格')";
                //DataTable dtquality = DBCallCommon.GetDTUsingSqlText(sqlquality);
                //if (dtquality.Rows.Count > 0)
                //{
                //    resultvalid = tsaid + ",";

                //}

                sqltext = "select BM_TUHAO from TBPM_STRINFODQO where BM_ENGID='" + tsaid + "' and (BM_ZONGXU='" + zongxu + "' or BM_ZONGXU like '" + zongxu + ".%') and dbo.Splitnum(BM_ZONGXU,'.')<=1";
                dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (!listTH.Contains(dt.Rows[i][0].ToString()) && dt.Rows[i][0].ToString() != "")
                        {
                            listTH.Add(dt.Rows[i][0].ToString());
                        }
                    }
                }

            }

            string sql = "select TH from View_TBQC_RejectPro_Info where ENGID='" + tsaid + "' and State not in ('4','5')";
            DataTable dtTH = DBCallCommon.GetDTUsingSqlText(sql);
            List<string> listReject = new List<string>();
            if (dtTH.Rows.Count > 0)
            {
                for (int i = 0; i < dtTH.Rows.Count; i++)
                {
                    if (!listReject.Contains(dtTH.Rows[i][0].ToString()) && dtTH.Rows[i][0].ToString() != "")
                    {
                        listReject.Add(dtTH.Rows[i][0].ToString());

                    }
                }
            }

            foreach (string thBOM in listTH)
            {
                string th = listReject.Find(r => r.Contains(thBOM));
                if (!string.IsNullOrEmpty(th))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('图号【" + th + "】的入库项存在未完成的不合格品单据，请完成后再入库！');", true);
                    return;
                }

            }
            DBCallCommon.ExecuteTrans(list);



            if (ViewState["action"].ToString() == "view") //查看  ，提交到审批
            {
                List<string> sqlstr = new List<string>();
                if (lbl_spzt.Text == "0")
                {
                    if (check_audit_Per())  //检查是否选择了审核人
                    {
                        //string pa_code = tb_tsaid.Text.Trim();
                        string pa_fir_per = firstid.Value;
                        string pa_sec_per = secondid.Value;
                        string pa_thi_per = thirdid.Value;

                        //审核人不能相同
                        if (((pa_fir_per != pa_sec_per) && (pa_sec_per != pa_thi_per)) || ((pa_sec_per == "") && (pa_thi_per == "")))
                        {
                            string sqltext = "insert into TBMP_FINISHED_IN_Audit (FIA_DOCNUM,Fir_Per,Fir_Jg,Sec_Per,Sec_Jg,Thi_Per,Thi_Jg,Rank,ZXDSJID)" + " values ('" + lbl_docnum.Text + "','" + pa_fir_per + "','0','" + pa_sec_per + "','0','" + pa_thi_per + "','0','" + rblSHDJ.SelectedIndex.ToString() + "','" + hidsj.Value + "')";
                            //string sqltext = "update TBMP_FINISHED_IN_Audit set TSA_ID='" + pa_code + "',Fir_Per='" + pa_fir_per + "',Fir_Jg='0',Sec_Per='" + pa_sec_per + "',Sec_Jg='0',Thi_Per='" + pa_thi_per + "',Thi_Jg='0', Rank='" + rblSHDJ.SelectedIndex.ToString() + "' where FIA_DOCNUM='" + lbl_docnum.Text + "'";
                            sqlstr.Add(sqltext);
                            string sqlupdatezt = "update TBMP_FINISHED_IN set SPZT='1' where TFI_DOCNUM='" + lbl_docnum.Text + "'";
                            sqlstr.Add(sqlupdatezt);
                            DBCallCommon.ExecuteTrans(sqlstr);
                            //制单人提交后向第一个审批人发送邮件
                            if (resultvalid.Length > 1)
                            {
                                subject = "您有新的成品入库需要审批——'" + lbl_docnum.Text.ToString() + "'，其中任务单号为'" + resultvalid.Substring(0, resultvalid.Length - 1) + "'未经质检或质检结果不合格，此处成品仍可入库，请相关人员进行质检处理！";
                            }
                            else
                                subject = "您有新的成品入库需要审批——" + lbl_docnum.Text.ToString();
                            sendto = txt_first.Text.Trim();
                            this.SendMail(subject, body_title, sendto);
                            //SendEmail();
                            string sqlsend = " select * from TBMP_FINISHED_IN where TFI_DOCNUM='" + lbl_docnum.Text + "'";
                            DataTable dtsend = DBCallCommon.GetDTUsingSqlText(sqlsend);
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("69"), new List<string>(), new List<string>(), "成品入库通知", "任务单号为“" + dtsend.Rows[0]["TSA_ID"].ToString() + "”总序为“" + dtsend.Rows[0]["TFI_ZONGXU"].ToString() + "“设备名称为“" + dtsend.Rows[0]["TFI_NAME"].ToString() + "“成品入库通知，请登录系统“成品管理”模块中的“成品入库管理中查看”");
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("249"), new List<string>(), new List<string>(), "成品入库通知", "任务单号为“" + dtsend.Rows[0]["TSA_ID"].ToString() + "”总序为“" + dtsend.Rows[0]["TFI_ZONGXU"].ToString() + "“设备名称为“" + dtsend.Rows[0]["TFI_NAME"].ToString() + "“成品入库通知，请登录系统“成品管理”模块中的“成品入库管理中查看”");
                            Response.Redirect("../PM_Data/PM_Finished_IN.aspx");
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
                        //string pa_code = tb_tsaid.Text.Trim();
                        string pa_fir_per = firstid.Value;
                        string pa_sec_per = secondid.Value;
                        string pa_thi_per = thirdid.Value;

                        //审核人不能相同
                        if (((pa_fir_per != pa_sec_per) && (pa_sec_per != pa_thi_per)) || ((pa_sec_per == "") && (pa_thi_per == "")))
                        {
                            string update1 = "update TBMP_FINISHED_IN_Audit set Fir_Per='" + pa_fir_per + "',Fir_Jg='0',Fir_Yj='',Fir_Sj='',Sec_Per='" + pa_sec_per + "',Sec_Jg='0'" +
                            " ,Sec_Yj='',Sec_Sj='',Thi_Per='" + pa_thi_per + "',Thi_Jg='0',Thi_Yj='',Thi_Sj='',Rank='" + rblSHDJ.SelectedIndex.ToString() + "'where FIA_DOCNUM='" + lbl_docnum.Text + "'";
                            string update2 = "update TBMP_FINISHED_IN set SPZT='1' where TFI_DOCNUM='" + lbl_docnum.Text + "'";
                            sqlstr.Add(update1);
                            sqlstr.Add(update2);
                            DBCallCommon.ExecuteTrans(sqlstr);

                            //制单人提交后向第一个审批人发送邮件
                            if (resultvalid.Length > 1)
                            {
                                subject = "驳回后的成品入库已修改，请重新审批——'" + lbl_docnum.Text.ToString() + "'，其中任务单号为'" + resultvalid.Substring(0, resultvalid.Length - 1) + "'未经质检或质检结果不合格，此处成品仍可入库，请相关人员进行质检处理！";
                            }
                            else
                                subject = "驳回后的成品入库已修改，请重新审批——" + lbl_docnum.Text.ToString();
                            sendto = txt_first.Text.Trim();
                            this.SendMail(subject, body_title, sendto);
                            Response.Redirect("../PM_Data/PM_Finished_IN.aspx");
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

                ////装箱单的时间id
                //string sqlzxd = "update TBMP_FINISHED_IN_Audit set ZXDSJID ='" + hidsj.Value + "' where FIA_DOCNUM='" + lbl_docnum.Text.Trim() + "'";
                //try
                //{
                //    DBCallCommon.ExeSqlText(sqlzxd);
                //}
                //catch
                //{
                //    Response.Write("<script>alert('装箱单的时间id无法绑定')</script>");
                //    return;
                //}
            }
            else if (ViewState["action"].ToString() == "audit") //审批 ，提交审批意见
            {

                string spyj = "";// 审批意见
                //string spsj = DateTime.Now.ToShortDateString();//审批时间
                string spsj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
                string sqltext = "update TBMP_FINISHED_IN_Audit " + set_text + " where  FIA_DOCNUM='" + lbl_docnum.Text + "'";
                sqlstr.Add(sqltext);

                //更新审批状态，0：保存；1：提交未审批；2：审批中；3：已通过；4：已驳回；
                string spzt = (spjg == "1") ? "2" : "4";
                string sqlupdatezt = "update TBMP_FINISHED_IN set SPZT='" + spzt + "'where  TFI_DOCNUM='" + lbl_docnum.Text + "'";
                sqlstr.Add(sqlupdatezt);

                //如果同意则向下一级审批人发送邮件通知
                if (spjg == "1")
                {
                    switch (submit_type)
                    {
                        case "1":
                            if (resultvalid.Length > 1)
                            {
                                subject = "您有新的成品入库需要审批，请及时处理——'" + lbl_docnum.Text.ToString() + "'，其中任务单号为'" + resultvalid.Substring(0, resultvalid.Length - 1) + "'未经质检或质检结果不合格，此处成品仍可入库，请相关人员进行质检处理！";
                            }
                            else
                                subject = "您有新的成品入库需要审批，请及时处理——" + lbl_docnum.Text.ToString();
                            sendto = txt_second.Text.Trim();
                            break;
                        case "2":
                            if (resultvalid.Length > 1)
                            {
                                subject = "您有新的成品入库需要审批，请及时处理——'" + lbl_docnum.Text.ToString() + "'，其中任务单号为'" + resultvalid.Substring(0, resultvalid.Length - 1) + "'未经质检或质检结果不合格，此处成品仍可入库，请相关人员进行质检处理！";
                            }
                            else
                                subject = "您有新的成品入库需要审批，请及时处理——" + lbl_docnum.Text.ToString();
                            sendto = txt_third.Text.Trim();
                            break;
                    }
                    if (sendto != "")
                    {
                        this.SendMail(subject, body_title, sendto);
                    }

                }
                //如果驳回则向制单人发送邮件通知,并将BOM表中入库数量改成0
                else if (spjg == "2")
                {
                    subject = "成品入库已驳回——" + lbl_docnum.Text.ToString();
                    body_title = "您提交的成品入库于" + DateTime.Now.ToString() + "被驳回";
                    sendto = tb_executor.Text.Trim();
                    this.SendMail(subject, body_title, sendto);


                    //foreach (RepeaterItem item in PM_Finished_lookRepeater.Items)
                    //{
                    //    string tsaid = ((Label)item.FindControl("lblTsa")).Text;
                    //    string str_zongxu = ((Label)item.FindControl("TFI_ZONGXU")).Text;
                    //    string sqlupdatenum = "update TBPM_STRINFODQO set BM_YRKNUM=0 where BM_ENGID='" + tsaid + "' and (BM_ZONGXU='" + str_zongxu + "' or BM_ZONGXU like '" + str_zongxu + ".%')";
                    //    sqlstr.Add(sqlupdatenum);
                    //}
                }
                DBCallCommon.ExecuteTrans(sqlstr);

                //检查是否全部同意，如果全部同意则改为3。根据审核等级判断，只判断最后一个是否同意即可
                bool YesOrNo = false;
                string sqlJG = "select * from TBMP_FINISHED_IN_Audit where FIA_DOCNUM='" + lbl_docnum.Text + "'";
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
                #region 成品出入库
                if (YesOrNo)
                {
                    // 给王艳辉发邮件
                    string sql1 = " select distinct TFI_DOCNUM,TSA_ID,TFI_PROJ from TBMP_FINISHED_IN where TFI_DOCNUM='" + docnum + "'";
                    DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("202"), new List<string>(), new List<string>(), "成品入库", "任务号“" + id + "“,项目名称”" + dt1.Rows[0]["TFI_PROJ"].ToString() + "“已经入库，请上系统查看！！！");
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("86"), new List<string>(), new List<string>(), "成品入库", "任务号“" + id + "”，项目名称”" + dt1.Rows[0]["TFI_PROJ"].ToString() + "“已经入库，请上系统查看！！！");
                    string strupdatezp2 = "update TBMP_FINISHED_IN set SPZT='3' where TFI_DOCNUM='" + lbl_docnum.Text + "'";
                    sqlstr.Clear();
                    sqlstr.Add(strupdatezp2);

                    foreach (RepeaterItem item in PM_Finished_lookRepeater.Items)
                    {
                        tsaid = ((Label)item.FindControl("lblTsa")).Text;
                        string str_zongxu = ((Label)item.FindControl("TFI_ZONGXU")).Text;
                        string str_proj = ((Label)item.FindControl("lblProj")).Text;
                        string str_map = ((Label)item.FindControl("lblMap")).Text;
                        string str_name = ((Label)item.FindControl("lblEngname")).Text;
                        int str_rknum = Convert.ToInt32(((TextBox)item.FindControl("lblRknum")).Text);//入库产品数量
                        int str_number = Convert.ToInt32(((Label)item.FindControl("lblNumber")).Text);//BOM产品总数
                        int str_singnumber = Convert.ToInt32(((Label)item.FindControl("lblsingnum")).Text);//BOM下单台数量
                        int parnumber = str_rknum * str_singnumber;//子设备入库数量
                        string note = ((Label)item.FindControl("lblnote")).Text;
                        string[] str = str_zongxu.Split('.');
                        string str_xu = str[0].ToString();
                        string sqltext1 = "select * from TBMP_FINISHED_STORE where KC_TSA='" + tsaid + "' and KC_ZONGXU='" + str_zongxu + "'";
                        dt = DBCallCommon.GetDTUsingSqlText(sqltext1);
                        if (dt.Rows.Count > 0)
                        {
                            string sqltext2 = "update TBMP_FINISHED_STORE set KC_RKNUM=KC_RKNUM+" + str_rknum + ",KC_KCNUM=KC_KCNUM+" + str_rknum + " where KC_TSA='" + tsaid + "' and  KC_ZONGXU='" + str_zongxu + "'";
                            sqlstr.Add(sqltext2);


                            string sqltext3 = "update a set a.KC_RKNUM=case when (KC_RKNUM+BM_SINGNUMBER*" + str_rknum + ") > KC_TALNUM then KC_TALNUM else (KC_RKNUM+BM_SINGNUMBER*" + str_rknum + ") end ,KC_KCNUM=case when (KC_KCNUM+BM_SINGNUMBER*" + str_rknum + ") > KC_TALNUM then KC_TALNUM else (KC_KCNUM+BM_SINGNUMBER*" + str_rknum + ") end from TBMP_FINISHED_STORE as a left join View_TM_DQO as b on a.KC_TSA=b.BM_ENGID and a.KC_ZONGXU=b.BM_ZONGXU  where KC_TSA='" + tsaid + "' and  KC_ZONGXU like '" + str_zongxu + ".%'";
                            sqlstr.Add(sqltext3);


                        }
                        else
                        {

                            string sqltext2 = "insert into TBMP_FINISHED_STORE (KC_SINGNUMBER,KC_TSA,KC_ZONGXU,KC_PROJ,KC_RKNUM,KC_KCNUM,KC_TALNUM,KC_MAP,KC_NAME,KC_NOTE) select BM_SINGNUMBER,BM_ENGID,BM_ZONGXU,'" + str_proj + "'," + str_rknum + "," + str_rknum + ",BM_NUMBER,BM_TUHAO,BM_CHANAME,'" + note + "' from View_TM_DQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU  where BM_ENGID='" + tsaid + "' and BM_ZONGXU='" + str_zongxu + "' and BM_RKSTATUS<>'2' and b.KC_ZONGXU is null  ";

                            sqlstr.Add(sqltext2);
                            string sqltext3 = "insert into TBMP_FINISHED_STORE (KC_SINGNUMBER,KC_TSA,KC_ZONGXU,KC_PROJ,KC_RKNUM,KC_KCNUM,KC_TALNUM,KC_MAP,KC_NAME,KC_NOTE) select BM_SINGNUMBER,BM_ENGID,BM_ZONGXU,'" + str_proj + "',case when BM_SINGNUMBER*" + str_rknum + ">=BM_NUMBER then BM_NUMBER else BM_SINGNUMBER*" + str_rknum + " end ,case when BM_SINGNUMBER*" + str_rknum + ">=BM_NUMBER then BM_NUMBER else BM_SINGNUMBER*" + str_rknum + " end,BM_NUMBER,BM_TUHAO,BM_CHANAME,'" + note + "' from View_TM_DQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU  where BM_ENGID='" + tsaid + "' and  BM_ZONGXU like '" + str_zongxu + ".%' and BM_RKSTATUS<>'2' and b.KC_ZONGXU is null  ";

                            sqlstr.Add(sqltext3);

                        }
                        string sqltext6 = "update TBPM_STRINFODQO set BM_RKSTATUS='2',BM_YRKNUM=BM_YRKNUM+" + str_rknum + " where BM_ENGID='" + tsaid + "' and BM_ZONGXU='" + str_zongxu + "'";
                        sqlstr.Add(sqltext6);
                        string sqltext7 = "update TBPM_STRINFODQO set BM_RKSTATUS='2',BM_YRKNUM=BM_YRKNUM+" + parnumber + " where BM_ENGID='" + tsaid + "' and  BM_ZONGXU like '" + str_zongxu + ".%'";
                        sqlstr.Add(sqltext7);
                    }

                    //sqltext = "select count(1) from TBPM_STRINFODQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU where BM_ENGID='"+tsaid+"' and (KC_ZONGXU is null or )";
                    DBCallCommon.ExecuteTrans(sqlstr);
                    sqltext = "select BM_ZONGXU,case when KC_RKNUM is null then 0 else KC_RKNUM end,case when KC_KCNUM is null then 0 else KC_KCNUM end from TBPM_STRINFODQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU where BM_ENGID='" + tsaid + "' and dbo.Splitnum(BM_ZONGXU,'.')=0";
                    DataTable dtzx = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (dtzx.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtzx.Rows.Count; i++)
                        {
                            sqltext = "select min((case when KC_RKNUM is null then 0 else KC_RKNUM end )/BM_SINGNUMBER),min((case when KC_KCNUM is null then 0 else KC_KCNUM end )/BM_SINGNUMBER) from TBPM_STRINFODQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU where BM_ENGID='" + tsaid + "' and BM_ZONGXU like '" + dtzx.Rows[i][0] + ".%' and (BM_KU like '%S%' or BM_MARID='' )";
                            dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                            //double rknum = Math.Floor(float.Parse(dt.Rows[0][0].ToString()));
                            //double kcnum = Math.Floor(float.Parse(dt.Rows[0][1].ToString()));
                            double rknum = CommonFun.ComTryDouble(dt.Rows[0][0].ToString());
                            double kcnum = CommonFun.ComTryDouble(dt.Rows[0][1].ToString());
                            //if (double.Parse(dtzx.Rows[i][1].ToString()) < rknum || double.Parse(dtzx.Rows[i][2].ToString()) < kcnum)
                            //{
                            //    if (double.Parse(dtzx.Rows[i][1].ToString()) == 0)
                            //    {
                            //        sqltext = "insert into TBMP_FINISHED_STORE (KC_SINGNUMBER,KC_TSA,KC_ZONGXU,KC_RKNUM,KC_KCNUM,KC_TALNUM,KC_MAP,KC_NAME) select BM_SINGNUMBER,BM_ENGID,BM_ZONGXU," + rknum + "," + rknum + ",BM_NUMBER,BM_TUHAO,BM_CHANAME from View_TM_DQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU  where BM_ENGID='" + tsaid + "' and BM_ZONGXU='" + dtzx.Rows[i][0].ToString() + "' and BM_RKSTATUS<>'2' and b.KC_ZONGXU is null ";
                            //        DBCallCommon.ExeSqlText(sqltext);
                            //    }
                            //    else
                            //    {
                            //        sqltext = "update TBMP_FINISHED_STORE set KC_RKNUM=" + rknum + ",KC_KCNUM=" + kcnum + " where KC_TSA='" + tsaid + "' and  KC_ZONGXU='" + dtzx.Rows[i][0].ToString() + "'";
                            //        DBCallCommon.ExeSqlText(sqltext);
                            //    }
                            //}

                            if (CommonFun.ComTryDouble(dtzx.Rows[i][1].ToString()) < rknum || CommonFun.ComTryDouble(dtzx.Rows[i][2].ToString()) < kcnum)
                            {
                                if (CommonFun.ComTryDouble(dtzx.Rows[i][1].ToString()) == 0)
                                {
                                    sqltext = "insert into TBMP_FINISHED_STORE (KC_SINGNUMBER,KC_TSA,KC_ZONGXU,KC_RKNUM,KC_KCNUM,KC_TALNUM,KC_MAP,KC_NAME) select BM_SINGNUMBER,BM_ENGID,BM_ZONGXU," + rknum + "," + rknum + ",BM_NUMBER,BM_TUHAO,BM_CHANAME from View_TM_DQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU  where BM_ENGID='" + tsaid + "' and BM_ZONGXU='" + dtzx.Rows[i][0].ToString() + "' and BM_RKSTATUS<>'2' and b.KC_ZONGXU is null ";
                                    DBCallCommon.ExeSqlText(sqltext);
                                }
                                else
                                {
                                    sqltext = "update TBMP_FINISHED_STORE set KC_RKNUM=" + rknum + ",KC_KCNUM=" + kcnum + " where KC_TSA='" + tsaid + "' and  KC_ZONGXU='" + dtzx.Rows[i][0].ToString() + "'";
                                    DBCallCommon.ExeSqlText(sqltext);
                                }
                            }

                        }
                    }



                #endregion
                    #region 判断是否全部入库
                    /* 判断任务号下所有一级明细入完则显示全部入库 */
                    string sqltxt_1 = "select * from (select case when b.KC_RKNUM is null then 0 else b.KC_RKNUM end as RKNUM,BM_NUMBER as NUMBER,BM_ENGID from TBPM_STRINFODQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU where dbo.Splitnum(BM_ZONGXU,'.')=0)a where RKNUM<>NUMBER and BM_ENGID ='" + tsaid + "'";
                    //string sqltxt_2 = "select count(*)as num2 from TBPM_STRINFODQO where BM_ENGID='" + tsaid + "' and BM_RKSTATUS='2' and  BM_PJID not like 'JSB.%' and BM_MSSTATUS<>'1' and dbo.Splitnum(BM_ZONGXU,'.')< 2";
                    DataTable dt_10 = DBCallCommon.GetDTUsingSqlText(sqltxt_1);
                    //DataTable dt_20 = DBCallCommon.GetDTUsingSqlText(sqltxt_2);
                    if (dt_10.Rows.Count == 0)
                    {
                        string sqltxt_3 = "update TBMP_MANUTSASSGN set MTA_YNRK='已入库' where MTA_ID='" + tsaid + "'";
                        DBCallCommon.ExeSqlText(sqltxt_3);
                        string sqltxt_4 = "update TBMP_MAINPLANDETAIL set MP_STATE='2',MP_ACTURALTIME=Convert(varchar(50), getdate(), 120)  where MP_ENGID='" + tsaid + "' and MP_PLNAME='成品入库' ";
                        DBCallCommon.ExeSqlText(sqltxt_4);
                    }
                    #endregion

                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交成功！！');", true);
            }
        }

        private void CheckNum()
        {


        }
        /// <summary>
        /// 选择审核人
        /// </summary>
        private void Get_SHR()
        {
            //审核人1--质检员
            // string sql1 = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_POSITION='0309' and ST_PD='0'"; 
            string sql1 = "select QSA_QCCLERK AS ST_ID,QSA_QCCLERKNM AS ST_NAME FROM TBQM_QTSASSGN WHERE QSA_STATE='1' AND QSA_ENGID='" + id + "' ";
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
            // 审核人2--生产部库管
            string sql2 = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_DEPID='07' and ST_PD='0'";
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql2);
            if (dt2.Rows.Count != 0)
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
            // 审核人3--生产部门
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

        /// <summary>
        /// 控制控件是否可用
        /// </summary>
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
        /// <summary>
        /// 绑定评审信息
        /// </summary>
        private void Bind_Audit_Info()
        {
            string sqltext = "select * from View_TBMP_FINISHED_IN_Audit where FIA_DOCNUM='" + lbl_docnum.Text + "'";
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

            }
            Control_SHLC();
        }

        /// <summary>
        /// 控制控件的显示
        /// </summary>
        private void Control_SHLC()
        {
            string sqltext = "select * from View_TBMP_FINISHED_IN_Audit where FIA_DOCNUM='" + lbl_docnum.Text + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string sqltxt = "select * from TBMP_FINISHED_IN where TFI_DOCNUM='" + lbl_docnum.Text + "'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltxt);
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
                if (Session["UserID"].ToString() == tb_executorid.Text && dt.Rows[0]["Fir_Jg"].ToString() == "0")
                {

                    btn_delete.Visible = true;
                    btn_save.Visible = true;
                }
            }
            if (dt1.Rows.Count > 0)
            {
                if (Session["UserID"].ToString() == tb_executorid.Text && dt1.Rows[0]["SPZT"].ToString() == "0")
                {
                    btn_delete.Visible = true;
                    btn_save.Visible = true;

                }
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
                        "\r\n编号：" + lbl_docnum.Text.ToString() +
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

            string sqltext = "select * from TBMP_FINISHED_IN where TFI_DOCNUM='" + lbl_docnum.Text.ToString() + "'";
            DBCallCommon.BindRepeater(PM_Finished_lookRepeater, sqltext);
        }
        protected void btn_delete_click(object sender, EventArgs e)
        {
            int i = 0;
            string sqltext = "";
            string sql2 = "";
            List<string> list = new List<string>();
            foreach (RepeaterItem item in PM_Finished_lookRepeater.Items)
            {
                CheckBox cb_1 = (CheckBox)item.FindControl("CheckBox1");
                if (cb_1 != null)
                {
                    if (cb_1.Checked)
                    {
                        i++;
                        string id = ((Label)item.FindControl("lab_id")).Text.ToString();//唯一标识
                        sqltext = "delete from TBMP_FINISHED_IN where ID='" + id + "'";
                        if (((Label)item.FindControl("lbQRInUniqCode")).Text.ToString() != "")
                        {
                            sql2 = "update midTable_Finished_QRIn set CPQRIn_State='0' where CPQRIn_ID=" + Convert.ToInt32(((Label)item.FindControl("lbQRInUniqCode")).Text.ToString()) + "";
                            list.Add(sql2);
                        }
                        list.Add(sqltext);
                    }
                }
            }
            if (i == 0)
            {

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据');", true);

            }
            else if (i > 0 && i < PM_Finished_lookRepeater.Items.Count)
            {
                DBCallCommon.ExecuteTrans(list);

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功!!!');", true);
            }
            else if (i == PM_Finished_lookRepeater.Items.Count)
            {
                sqltext = "delete from TBMP_FINISHED_IN_Audit where FIA_DOCNUM='" + lbl_docnum.Text.ToString() + "'";
                list.Add(sqltext);
                DBCallCommon.ExecuteTrans(list);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功!!!');", true);

            }
        }
        protected void btn_save_click(object sender, EventArgs e)
        {
            int i = 0;
            string sqltext = "";
            List<string> list = new List<string>();
            foreach (RepeaterItem item in PM_Finished_lookRepeater.Items)
            {
                CheckBox cb_1 = (CheckBox)item.FindControl("CheckBox1");
                if (cb_1 != null)
                {
                    if (cb_1.Checked)
                    {
                        i++;
                        string id = ((Label)item.FindControl("lab_id")).Text.ToString();//唯一标识
                        int str_rknum = Convert.ToInt32(((TextBox)item.FindControl("lblRknum")).Text);//入库产品数量
                        sqltext = "update TBMP_FINISHED_IN set TFI_RKNUM=" + str_rknum + " where ID='" + id + "'";
                        list.Add(sqltext);
                    }
                }
            }
            if (i == 0)
            {

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据');", true);

            }
            else if (i > 0)
            {
                DBCallCommon.ExecuteTrans(list);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功!!!');", true);
            }


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
                            sqlStr += "'CM_ZXD')";
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
            string sql = "select * from CM_FILES where FILE_FATHERID='" + hidsj.Value + "' and FILE_TYPE='CM_ZXD'";
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
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(ds.Tables[0].Rows[0]["FILE_SHOWNAME"].ToString()));
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

        //绑定抄送人员
        //private void Bindcbx()
        //{
        //    string sql = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_DEPID='03' or ST_DEPID='12' order by ST_POSITION";
        //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        cbxlCS.Items.Add(new ListItem(dr["ST_NAME"].ToString(), dr["ST_ID"].ToString()));
        //    }
        //}

        //private void SendEmail()
        //{
        //    string sql = " select * from TBMP_FINISHED_IN where TFI_DOCNUM='" + lbl_docnum.Text + "'";
        //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
        //    foreach (ListItem item in cbxlCS.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(item.Value), new List<string>(), new List<string>(), "成品入库通知", "任务单号为“" + dt.Rows[0]["TSA_ID"].ToString() + "”总序为“" + dt.Rows[0]["TFI_ZONGXU"].ToString() + "“设备名称为“" + dt.Rows[0]["TFI_NAME"].ToString() + "“成品入库通知，请登录系统“成品管理”模块中的“成品入库管理中查看”");
        //        }
        //    }
        //}
    }
}
