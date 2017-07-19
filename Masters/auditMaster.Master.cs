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

namespace ZCZJ_DPF.Masters
{
    public partial class auditMaster : System.Web.UI.MasterPage
    {
        string action;
        string auditno;
        protected void Page_Load(object sender, EventArgs e)
        {
            //操作：包括添加add,修改edit,删除delete,查看view,审核audit
            action = Request.QueryString["action"].ToString().Trim();
            //审核编号：识别当前审批单号
            auditno = Request.QueryString["auditno"].ToString().Trim();
            //获取当前单据状态
            string sqlgetstate = "select * from AuditNew where auditno='" + auditno + "'";
            DataTable dtgetstate = DBCallCommon.GetDTUsingSqlText(sqlgetstate);
            if (dtgetstate.Rows.Count > 0)
            {
                hidstate.Value = dtgetstate.Rows[0]["totalstate"].ToString().Trim();
            }

            if (!IsPostBack)
            {
                //子函数，绑定审批数据
                if (action != "add")
                {
                    bindspdata();
                }
            }
            //控件可见性和可用性控制
            contrlable();
        }

        //控件可见性和可用性控制
        private void contrlable()
        {
            if (hidstate.Value == "2")
            {
                ImageVerify.Visible = true;
            }
            else
            {
                ImageVerify.Visible = false;
            }
            //添加
            if (action == "add")
            {
                btnAudit.Visible = false;
                rblSHJS.Enabled = true;
                tbzdryj.Enabled=true;
                hlSelect1.Visible = true;
                rblfirst.Enabled = false;
                opinion1.Enabled = false;
                hlSelect2.Visible = true;
                rblsecond.Enabled = false;
                opinion2.Enabled = false;
                hlSelect3.Visible = true;
                rblthird.Enabled = false;
                opinion3.Enabled = false;
            }
            //修改
            if (action == "edit")
            {
                btnAudit.Visible = true;
                rblSHJS.Enabled = true;
                tbzdryj.Enabled = true;
                hlSelect1.Visible = true;
                rblfirst.Enabled = false;
                opinion1.Enabled = false;
                hlSelect2.Visible = true;
                rblsecond.Enabled = false;
                opinion2.Enabled = false;
                hlSelect3.Visible = true;
                rblthird.Enabled = false;
                opinion3.Enabled = false;
            }
            //查看
            if (action == "view" || action == "fankui")
            {
                btnAudit.Visible = false;
                rblSHJS.Enabled = false;
                tbzdryj.Enabled = false;
                hlSelect1.Visible = false;
                rblfirst.Enabled = false;
                opinion1.Enabled = false;
                hlSelect2.Visible = false;
                rblsecond.Enabled = false;
                opinion2.Enabled = false;
                hlSelect3.Visible = false;
                rblthird.Enabled = false;
                opinion3.Enabled = false;
            }
            //审核
            if (action == "audit")
            {
                btnAudit.Visible = true;
                rblSHJS.Enabled = false;
                tbzdryj.Enabled = false;
                hlSelect1.Visible = false;
                hlSelect2.Visible = false;
                hlSelect3.Visible = false;
                if (Session["UserID"].ToString().Trim() == firstid.Value.Trim())
                {
                    rblfirst.Enabled = true;
                    opinion1.Enabled = true;
                    rblsecond.Enabled = false;
                    opinion2.Enabled = false;
                    rblthird.Enabled = false;
                    opinion3.Enabled = false;
                }
                if (Session["UserID"].ToString().Trim() == secondid.Value.Trim())
                {
                    rblfirst.Enabled = false;
                    opinion1.Enabled = false;
                    rblsecond.Enabled = true;
                    opinion2.Enabled = true;
                    rblthird.Enabled = false;
                    opinion3.Enabled = false;
                }
                if (Session["UserID"].ToString().Trim() == thirdid.Value.Trim())
                {
                    rblfirst.Enabled = false;
                    opinion1.Enabled = false;
                    rblsecond.Enabled = false;
                    opinion2.Enabled = false;
                    rblthird.Enabled = true;
                    opinion3.Enabled = true;
                }
            }
        }

        //审批数据绑定
        private void bindspdata()
        {
            string sqlgetdata = "select * from AuditNew where auditno='" + auditno + "'";
            DataTable dtgetdata = DBCallCommon.GetDTUsingSqlText(sqlgetdata);
            if (dtgetdata.Rows.Count > 0)
            {
                if (dtgetdata.Rows[0]["auditlevel"].ToString().Trim() != "" & dtgetdata.Rows[0]["auditlevel"].ToString().Trim() != "0")
                {
                    rblSHJS.SelectedValue = dtgetdata.Rows[0]["auditlevel"].ToString().Trim();
                }
                audittitle.Text = dtgetdata.Rows[0]["audittype"].ToString().Trim();
                lbauditno.Text = dtgetdata.Rows[0]["auditno"].ToString().Trim();
                lbzdr.Text = dtgetdata.Rows[0]["addpername"].ToString().Trim();
                hidzdrid.Value = dtgetdata.Rows[0]["addperid"].ToString().Trim();
                lbzdtime.Text = dtgetdata.Rows[0]["addtime"].ToString().Trim();
                tbzdryj.Text = dtgetdata.Rows[0]["addnote"].ToString().Trim();
                //第一级
                txt_first.Value = dtgetdata.Rows[0]["auditpername1"].ToString().Trim();
                firstid.Value = dtgetdata.Rows[0]["auditperid1"].ToString().Trim();
                if (dtgetdata.Rows[0]["auditstate1"].ToString().Trim() != "0")
                {
                    rblfirst.SelectedValue = dtgetdata.Rows[0]["auditstate1"].ToString().Trim();
                }
                first_time.Text = dtgetdata.Rows[0]["audittime1"].ToString().Trim();
                opinion1.Text = dtgetdata.Rows[0]["auditnote1"].ToString().Trim();
                //第二级
                txt_second.Value = dtgetdata.Rows[0]["auditpername2"].ToString().Trim();
                secondid.Value = dtgetdata.Rows[0]["auditperid2"].ToString().Trim();
                if (dtgetdata.Rows[0]["auditstate2"].ToString().Trim() != "0")
                {
                    rblsecond.SelectedValue = dtgetdata.Rows[0]["auditstate2"].ToString().Trim();
                }
                second_time.Text = dtgetdata.Rows[0]["audittime2"].ToString().Trim();
                opinion2.Text = dtgetdata.Rows[0]["auditnote2"].ToString().Trim();
                //第三级
                txt_third.Value = dtgetdata.Rows[0]["auditpername3"].ToString().Trim();
                thirdid.Value = dtgetdata.Rows[0]["auditperid3"].ToString().Trim();
                if (dtgetdata.Rows[0]["auditstate3"].ToString().Trim() != "0")
                {
                    rblthird.SelectedValue = dtgetdata.Rows[0]["auditstate3"].ToString().Trim();
                }
                third_time.Text = dtgetdata.Rows[0]["audittime3"].ToString().Trim();
                opinion3.Text = dtgetdata.Rows[0]["auditnote3"].ToString().Trim();
            }
        }
        //提交
        protected void btnAudit_OnClick(object sender,EventArgs e)
        {
            List<string> listsql = new List<string>();
            string sqltext = "";
            if (action == "edit")
            {
                if (rblSHJS.SelectedValue.Trim()=="1")
                {
                    if (txt_first.Value.Trim() == "" || firstid.Value.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                        return;
                    }
                }
                if (rblSHJS.SelectedValue.Trim() == "2")
                {
                    if (txt_first.Value.Trim() == "" || firstid.Value.Trim() == "" || txt_second.Value.Trim() == "" || secondid.Value.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                        return;
                    }
                }
                if (rblSHJS.SelectedValue.Trim() == "3")
                {
                    if (txt_first.Value.Trim() == "" || firstid.Value.Trim() == "" || txt_second.Value.Trim() == "" || secondid.Value.Trim() == "" || txt_third.Value.Trim() == "" || thirdid.Value.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                        return;
                    }
                }
                //在内容页保存时插入审批单号和审批类型
                sqltext = "update AuditNew set audittype='" + audittitle.Text.Trim() + "',auditlevel='" + rblSHJS.SelectedValue.Trim() + "',totalstate='1',addpername='" + Session["UserName"].ToString().Trim() + "',addperid='" + Session["UserID"].ToString().Trim() + "',addtime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',addnote='" + tbzdryj.Text.Trim() + "',auditpername1='" + txt_first.Value.Trim() + "',auditperid1='" + firstid.Value.Trim() + "',audittime1='',auditstate1='0',auditnote1='',auditpername2='" + txt_second.Value.Trim() + "',auditperid2='" + secondid.Value.Trim() + "',audittime2='',auditstate2='0',auditnote2='',auditpername3='" + txt_third.Value.Trim() + "',auditperid3='" + thirdid.Value.Trim() + "',audittime3='',auditstate3='0',auditnote3='' where auditno='"+auditno+"'";
                listsql.Add(sqltext);
                //邮件提醒
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(firstid.Value.Trim()), new List<string>(), new List<string>(), "" + audittitle.Text.Trim() + "单据审批！", "您有" + audittitle.Text.Trim() + "单据需要审批，单号:" + auditno + "，请登录查看！");
            }
            else
            {
                if (Session["UserID"].ToString().Trim() == firstid.Value.Trim())
                {
                    if (rblSHJS.SelectedValue.Trim() == "1")
                    {
                        if (rblfirst.SelectedValue == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批意见！');", true);
                            return;
                        }
                        else if (rblfirst.SelectedValue == "1")
                        {
                            sqltext = "update AuditNew set totalstate='2',audittime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',auditstate1='1',auditnote1='" + opinion1.Text.Trim() + "' where auditno='" + auditno + "'";
                            listsql.Add(sqltext);
                        }
                        else if (rblfirst.SelectedValue == "2")
                        {
                            if (opinion1.Text.Trim() == "")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写驳回意见！');", true);
                                return;
                            }
                            else
                            {
                                sqltext = "update AuditNew set totalstate='3',audittime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',auditstate1='2',auditnote1='" + opinion1.Text.Trim() + "' where auditno='" + auditno + "'";
                                listsql.Add(sqltext);
                                //邮件提醒
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(hidzdrid.Value.Trim()), new List<string>(), new List<string>(), "" + audittitle.Text.Trim() + "单据驳回！", "您有" + audittitle.Text.Trim() + "单据被驳回，单号:" + auditno + "，请登录查看！");
                            }
                        }
                    }
                    else if (rblSHJS.SelectedValue.Trim() == "2")
                    {
                        if (rblfirst.SelectedValue == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批意见！');", true);
                            return;
                        }
                        else if (rblfirst.SelectedValue == "1")
                        {
                            sqltext = "update AuditNew set audittime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',auditstate1='1',auditnote1='" + opinion1.Text.Trim() + "' where auditno='" + auditno + "'";
                            listsql.Add(sqltext);
                            //邮件提醒
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(secondid.Value.Trim()), new List<string>(), new List<string>(), "" + audittitle.Text.Trim() + "单据审批！", "您有" + audittitle.Text.Trim() + "单据需要审批，单号:" + auditno + "，请登录查看！");
                        }
                        else if (rblfirst.SelectedValue == "2")
                        {
                            if (opinion1.Text.Trim() == "")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写驳回意见！');", true);
                                return;
                            }
                            else
                            {
                                sqltext = "update AuditNew set totalstate='3',audittime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',auditstate1='2',auditnote1='" + opinion1.Text.Trim() + "' where auditno='" + auditno + "'";
                                listsql.Add(sqltext);
                                //邮件提醒
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(hidzdrid.Value.Trim()), new List<string>(), new List<string>(), "" + audittitle.Text.Trim() + "单据驳回！", "您有" + audittitle.Text.Trim() + "单据被驳回，单号:" + auditno + "，请登录查看！");
                            }
                        }
                    }
                    else if (rblSHJS.SelectedValue.Trim() == "3")
                    {
                        if (rblfirst.SelectedValue == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批意见！');", true);
                            return;
                        }
                        else if (rblfirst.SelectedValue == "1")
                        {
                            sqltext = "update AuditNew set audittime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',auditstate1='1',auditnote1='" + opinion1.Text.Trim() + "' where auditno='" + auditno + "'";
                            listsql.Add(sqltext);
                            //邮件提醒
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(secondid.Value.Trim()), new List<string>(), new List<string>(), "" + audittitle.Text.Trim() + "单据审批！", "您有" + audittitle.Text.Trim() + "单据需要审批，单号:" + auditno + "，请登录查看！");
                        }
                        else if (rblfirst.SelectedValue == "2")
                        {
                            if (opinion1.Text.Trim() == "")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写驳回意见！');", true);
                                return;
                            }
                            else
                            {
                                sqltext = "update AuditNew set totalstate='3',audittime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',auditstate1='2',auditnote1='" + opinion1.Text.Trim() + "' where auditno='" + auditno + "'";
                                listsql.Add(sqltext);
                                //邮件提醒
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(hidzdrid.Value.Trim()), new List<string>(), new List<string>(), "" + audittitle.Text.Trim() + "单据驳回！", "您有" + audittitle.Text.Trim() + "单据被驳回，单号:" + auditno + "，请登录查看！");
                            }
                        }
                    }
                }
                else if (Session["UserID"].ToString().Trim() == secondid.Value.Trim())
                {
                    if (rblSHJS.SelectedValue.Trim() == "2")
                    {
                        if (rblsecond.SelectedValue == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批意见！');", true);
                            return;
                        }
                        else if (rblsecond.SelectedValue == "1")
                        {
                            sqltext = "update AuditNew set totalstate='2',audittime2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',auditstate2='1',auditnote2='" + opinion2.Text.Trim() + "' where auditno='" + auditno + "'";
                            listsql.Add(sqltext);
                        }
                        else if (rblsecond.SelectedValue == "2")
                        {
                            if (opinion2.Text.Trim() == "")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写驳回意见！');", true);
                                return;
                            }
                            else
                            {
                                sqltext = "update AuditNew set totalstate='3',audittime2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',auditstate2='2',auditnote2='" + opinion2.Text.Trim() + "' where auditno='" + auditno + "'";
                                listsql.Add(sqltext);
                                //邮件提醒
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(hidzdrid.Value.Trim()), new List<string>(), new List<string>(), "" + audittitle.Text.Trim() + "单据驳回！", "您有" + audittitle.Text.Trim() + "单据被驳回，单号:" + auditno + "，请登录查看！");
                            }
                        }
                    }
                    else if (rblSHJS.SelectedValue.Trim() == "3")
                    {
                        if (rblsecond.SelectedValue == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批意见！');", true);
                            return;
                        }
                        else if (rblsecond.SelectedValue == "1")
                        {
                            sqltext = "update AuditNew set audittime2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',auditstate2='1',auditnote2='" + opinion2.Text.Trim() + "' where auditno='" + auditno + "'";
                            listsql.Add(sqltext);
                            //邮件提醒
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(thirdid.Value.Trim()), new List<string>(), new List<string>(), "" + audittitle.Text.Trim() + "单据审批！", "您有" + audittitle.Text.Trim() + "单据需要审批，单号:" + auditno + "，请登录查看！");
                        }
                        else if (rblsecond.SelectedValue == "2")
                        {
                            if (opinion2.Text.Trim() == "")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写驳回意见！');", true);
                                return;
                            }
                            else
                            {
                                sqltext = "update AuditNew set totalstate='3',audittime2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',auditstate2='2',auditnote2='" + opinion2.Text.Trim() + "' where auditno='" + auditno + "'";
                                listsql.Add(sqltext);
                                //邮件提醒
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(hidzdrid.Value.Trim()), new List<string>(), new List<string>(), "" + audittitle.Text.Trim() + "单据驳回！", "您有" + audittitle.Text.Trim() + "单据被驳回，单号:" + auditno + "，请登录查看！");
                            }
                        }
                    }
                }
                else if (Session["UserID"].ToString().Trim() == thirdid.Value.Trim())
                {
                    if (rblSHJS.SelectedValue.Trim() == "3")
                    {
                        if (rblthird.SelectedValue == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批意见！');", true);
                            return;
                        }
                        else if (rblthird.SelectedValue == "1")
                        {
                            sqltext = "update AuditNew set totalstate='2',audittime3='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',auditstate3='1',auditnote3='" + opinion3.Text.Trim() + "' where auditno='" + auditno + "'";
                            listsql.Add(sqltext);
                        }
                        else if (rblthird.SelectedValue == "2")
                        {
                            if (opinion3.Text.Trim() == "")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写驳回意见！');", true);
                                return;
                            }
                            else
                            {
                                sqltext = "update AuditNew set totalstate='3',audittime3='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',auditstate3='2',auditnote3='" + opinion3.Text.Trim() + "' where auditno='" + auditno + "'";
                                listsql.Add(sqltext);
                                //邮件提醒
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(hidzdrid.Value.Trim()), new List<string>(), new List<string>(), "" + audittitle.Text.Trim() + "单据驳回！", "您有" + audittitle.Text.Trim() + "单据被驳回，单号:" + auditno + "，请登录查看！");
                            }
                        }
                    }
                }
            }
            DBCallCommon.ExecuteTrans(listsql);
            Response.Write("<script type='text/javascript' language='javascript' >alert('提交成功!');window.opener.location = window.opener.location.href;window.close();</script>");
        }
    }
}
