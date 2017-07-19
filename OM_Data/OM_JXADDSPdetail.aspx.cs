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
using System.Collections.Generic;
using System.IO;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_JXADDSPdetail : System.Web.UI.Page
    {
        string spbh = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindmxdata();//绑定数据
                bindspdata();//绑定审批数据
                contrlkjx();//控件可见性和可用性
                lbtitle.Text = ddlType.SelectedItem.Text.Trim();
            }
        }


        //明细数据
        private void bindmxdata()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqltext = "select * from OM_JXADDSP where bh='" + spbh.ToString().Trim() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                tb_yearmonth.Text = dt.Rows[0]["yearmonth"].ToString().Trim();
                txt_contents.Text = dt.Rows[0]["things"].ToString().Trim();
                lbfqrname.Text = dt.Rows[0]["creatstname"].ToString().Trim();
                lbfqrid.Text = dt.Rows[0]["creatstid"].ToString().Trim();
                lbfqtime.Text = dt.Rows[0]["creattime"].ToString().Trim();

                txtdepartment.Text = dt.Rows[0]["jxadddepartment"].ToString().Trim();
                txtname.Text = dt.Rows[0]["jxaddstname"].ToString().Trim();
                lbstid.Text = dt.Rows[0]["jxaddstid"].ToString().Trim();
                txt_jxgzxs.Text = dt.Rows[0]["jxgzxs"].ToString().Trim();


                ddlType.SelectedValue = dt.Rows[0]["type"].ToString().Trim();
            }
        }

        //审批数据
        private void bindspdata()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqlshdata = "select * from OM_JXADDSP where bh='" + spbh + "'";
            DataTable dtsh = DBCallCommon.GetDTUsingSqlText(sqlshdata);
            if (dtsh.Rows.Count > 0)
            {
                if (dtsh.Rows[0]["shjs"].ToString().Trim() != "")
                {
                    txt_first.Text = dtsh.Rows[0]["shrname1"].ToString().Trim();
                    firstid.Value = dtsh.Rows[0]["shrid1"].ToString().Trim();
                    if (dtsh.Rows[0]["shstate1"].ToString().Trim() == "1" || dtsh.Rows[0]["shstate1"].ToString().Trim() == "2")
                    {
                        rblfirst.SelectedValue = dtsh.Rows[0]["shstate1"].ToString().Trim();
                    }
                    first_time.Text = dtsh.Rows[0]["shtime1"].ToString().Trim();
                    opinion1.Text = dtsh.Rows[0]["shnote1"].ToString().Trim();
                    if (dtsh.Rows[0]["shjs"].ToString().Trim() == "2")
                    {
                        txt_second.Text = dtsh.Rows[0]["shrname2"].ToString().Trim();
                        secondid.Value = dtsh.Rows[0]["shrid2"].ToString().Trim();

                        if (dtsh.Rows[0]["shstate2"].ToString().Trim() == "1" || dtsh.Rows[0]["shstate2"].ToString().Trim() == "2")
                        {
                            rblsecond.SelectedValue = dtsh.Rows[0]["shstate2"].ToString().Trim();
                        }
                        second_time.Text = dtsh.Rows[0]["shtime2"].ToString().Trim();
                        opinion2.Text = dtsh.Rows[0]["shnote2"].ToString().Trim();
                    }

                    rblSHJS.SelectedValue = dtsh.Rows[0]["shjs"].ToString().Trim();
                }
            }
        }

        //控件可用性
        private void contrlkjx()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqlkjdata = "select * from OM_JXADDSP where bh='" + spbh + "'";
            DataTable dtkj = DBCallCommon.GetDTUsingSqlText(sqlkjdata);
            if (dtkj.Rows.Count > 0)
            {
                if (dtkj.Rows[0]["totalstate"].ToString().Trim() == "2")
                {
                    ImageVerify.Visible = true;
                }
                if (dtkj.Rows[0]["totalstate"].ToString().Trim() == "0")//初始化
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["creatstid"].ToString().Trim())
                    {
                        if (rblSHJS.SelectedValue.ToString().Trim() == "1")
                        {
                            tb_yearmonth.Enabled = true;
                            txt_contents.Enabled = true;
                            btnSave.Visible = true;
                            txt_first.Enabled = true;
                            hlSelect1.Visible = true;

                            txtname.Enabled = true;
                            txt_jxgzxs.Enabled = true;

                            ejshh.Visible = false;
                        }
                        else if (rblSHJS.SelectedValue.ToString().Trim() == "2")
                        {
                            tb_yearmonth.Enabled = true;
                            txt_contents.Enabled = true;
                            btnSave.Visible = true;
                            txt_first.Enabled = true;
                            hlSelect1.Visible = true;
                            txt_second.Enabled = true;
                            hlSelect2.Visible = true;

                            txtname.Enabled = true;
                            txt_jxgzxs.Enabled = true;

                            ejshh.Visible = true;
                        }
                    }
                    else
                    {
                        if (rblSHJS.SelectedValue.ToString().Trim() == "1")
                        {
                            ejshh.Visible = false;
                        }
                        else if(rblSHJS.SelectedValue.ToString().Trim() == "2")
                        {
                            ejshh.Visible = true;
                        }
                    }
                }
                else if (dtkj.Rows[0]["totalstate"].ToString().Trim() == "1" && dtkj.Rows[0]["shstate1"].ToString().Trim() == "0")//提交未审
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["shrid1"].ToString().Trim())
                    {
                        if (rblSHJS.SelectedValue.ToString().Trim() == "1")
                        {
                            btnSave.Visible = true;
                            rblfirst.Enabled = true;
                            opinion1.Enabled = true;

                            tb_yearmonth.Enabled = false;
                            txt_contents.Enabled = false;
                            txt_first.Enabled = false;
                            hlSelect1.Visible = false;

                            txtname.Enabled = false;
                            txt_jxgzxs.Enabled = false;

                            ejshh.Visible = false;
                            rblSHJS.Enabled = false;
                        }
                        else if (rblSHJS.SelectedValue.ToString().Trim() == "2")
                        {
                            btnSave.Visible = true;
                            rblfirst.Enabled = true;
                            opinion1.Enabled = true;

                            tb_yearmonth.Enabled = false;
                            txt_contents.Enabled = false;
                            txt_first.Enabled = false;
                            hlSelect1.Visible = false;
                            txt_second.Enabled = false;
                            hlSelect2.Visible = false;
                            rblsecond.Enabled = false;
                            opinion2.Enabled = false;

                            txtname.Enabled = false;
                            txt_jxgzxs.Enabled = false;

                            rblSHJS.Enabled = false;
                            ejshh.Visible = true;
                        }
                    }
                    else
                    {
                        if (rblSHJS.SelectedValue.ToString().Trim() == "1")
                        {
                            ejshh.Visible = false;
                            rblSHJS.Enabled = false;
                        }
                        else if (rblSHJS.SelectedValue.ToString().Trim() == "2")
                        {
                            rblSHJS.Enabled = false;
                            ejshh.Visible = true;
                        }
                    }
                }
                else if (dtkj.Rows[0]["totalstate"].ToString().Trim() == "1" && dtkj.Rows[0]["shstate1"].ToString().Trim() == "1" && dtkj.Rows[0]["shstate2"].ToString().Trim() == "0" && dtkj.Rows[0]["shjs"].ToString().Trim() == "2")//一级审核通过
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["shrid2"].ToString().Trim())
                    {
                        btnSave.Visible = true;
                        rblsecond.Enabled = true;
                        opinion2.Enabled = true;

                        tb_yearmonth.Enabled = false;
                        txt_contents.Enabled = false;
                        txt_first.Enabled = false;
                        hlSelect1.Visible = false;
                        txt_second.Enabled = false;
                        hlSelect2.Visible = false;
                        rblfirst.Enabled = false;
                        opinion1.Enabled = false;

                        txtname.Enabled = false;
                        txt_jxgzxs.Enabled = false;

                        rblSHJS.Enabled = false;
                    }
                }

                else if (dtkj.Rows[0]["totalstate"].ToString().Trim() == "2" && dtkj.Rows[0]["shjs"].ToString().Trim() == "1")
                {
                    ejshh.Visible = false;
                    rblSHJS.Enabled = false;
                }
                else if (dtkj.Rows[0]["totalstate"].ToString().Trim() == "2" && dtkj.Rows[0]["shjs"].ToString().Trim() == "2")
                {
                    rblSHJS.Enabled = false;
                    ejshh.Visible = true;
                }
                else if (dtkj.Rows[0]["totalstate"].ToString().Trim() == "3")
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["creatstid"].ToString().Trim())
                    {
                        btnfanshen.Visible = true;
                        if (rblSHJS.SelectedValue.ToString().Trim() == "1")
                        {
                            tb_yearmonth.Enabled = true;
                            txt_contents.Enabled = true;
                            btnSave.Visible = true;

                            txtname.Enabled = true;
                            txt_jxgzxs.Enabled = true;

                            ejshh.Visible = false;
                            rblSHJS.Enabled = false;
                        }
                        else if (rblSHJS.SelectedValue.ToString().Trim() == "2")
                        {
                            tb_yearmonth.Enabled = true;
                            txt_contents.Enabled = true;
                            btnSave.Visible = true;

                            txtname.Enabled = true;
                            txt_jxgzxs.Enabled = true;

                            rblSHJS.Enabled = false;
                            ejshh.Visible = true;
                        }
                    }
                    else
                    {
                        btnfanshen.Visible = false;
                        if (rblSHJS.SelectedValue.ToString().Trim() == "1")
                        {
                            ejshh.Visible = false;
                            rblSHJS.Enabled = false;
                        }
                        else if (rblSHJS.SelectedValue.ToString().Trim() == "2")
                        {
                            rblSHJS.Enabled = false;
                            ejshh.Visible = true;
                        }
                    }
                }
            }
        }



        //提交
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string sql000 = "";
            string sqlstaffinfo = "";
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqlsavedata = "select * from OM_JXADDSP where bh='" + spbh + "'";
            DataTable dtsave = DBCallCommon.GetDTUsingSqlText(sqlsavedata);
            if (dtsave.Rows.Count > 0)
            {
                if (tb_yearmonth.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请选择年月！');", true);
                    return;
                }
                if (lbstid.Text.Trim() == "" || txtname.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请填写人员信息！');", true);
                    return;
                }
                if (txt_jxgzxs.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请填写绩效工资系数！');", true);
                    return;
                }
                if (ddlType.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请选择类型！');", true);
                    return;
                }
                if (dtsave.Rows[0]["creatstid"].ToString().Trim() == Session["UserID"].ToString().Trim() && dtsave.Rows[0]["totalstate"].ToString().Trim() == "0")
                {
                    if (rblSHJS.SelectedValue.ToString().Trim() == "2")
                    {
                        if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "" || txt_second.Text.Trim() == "" || secondid.Value.Trim() == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                            return;
                        }
                        else
                        {
                            sql000 = "update OM_JXADDSP set creattime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',yearmonth='" + tb_yearmonth.Text.Trim() + "',things='" + txt_contents.Text.Trim() + "',jxaddstid='" + lbstid.Text.Trim() + "',jxaddstname='" + txtname.Text.Trim() + "',jxadddepartment='" + txtdepartment.Text.Trim() + "',jxgzxs='" + txt_jxgzxs.Text.Trim() + "',type='" + ddlType.SelectedValue.Trim() + "',totalstate='1',shrid1='" + firstid.Value.Trim() + "',shrname1='" + txt_first.Text.Trim() + "',shrid2='" + secondid.Value.Trim() + "',shrname2='" + txt_second.Text.Trim() + "',shjs='" + rblSHJS.SelectedValue.ToString().Trim() + "' where bh='" + spbh + "'";
                            list.Add(sql000);
                        }
                    }

                    else if (rblSHJS.SelectedValue.ToString().Trim() == "1")
                    {
                        if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                            return;
                        }
                        else
                        {
                            sql000 = "update OM_JXADDSP set creattime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',yearmonth='" + tb_yearmonth.Text.Trim() + "',things='" + txt_contents.Text.Trim() + "',jxaddstid='" + lbstid.Text.Trim() + "',jxaddstname='" + txtname.Text.Trim() + "',jxadddepartment='" + txtdepartment.Text.Trim() + "',jxgzxs='" + txt_jxgzxs.Text.Trim() + "',type='" + ddlType.SelectedValue.Trim() + "',totalstate='1',shrid1='" + firstid.Value.Trim() + "',shrname1='" + txt_first.Text.Trim() + "',shrid2='',shrname2='',shjs='" + rblSHJS.SelectedValue.ToString().Trim() + "' where bh='" + spbh + "'";
                            list.Add(sql000);
                        }
                    }

                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sprid = firstid.Value.Trim();
                    sptitle = "人员绩效审批";
                    spcontent = txtname.Text.Trim() + "的" + lbtitle.Text.Trim() + "需要您审批，请登录查看！";
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                }

                if (dtsave.Rows[0]["shrid1"].ToString().Trim() == Session["UserID"].ToString().Trim() && dtsave.Rows[0]["totalstate"].ToString().Trim() == "1")
                {
                    if (rblfirst.SelectedValue.ToString().Trim() == "1")
                    {
                        if (rblSHJS.SelectedValue.ToString().Trim() == "1")
                        {
                            sql000 = "update OM_JXADDSP set totalstate='2',shstate1='1',shtime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',shnote1='" + opinion1.Text.Trim() + "' where bh='" + spbh + "'";
                            sqlstaffinfo = "update TBDS_STAFFINFO set ST_GANGWEIXISHU=" + CommonFun.ComTryDecimal(txt_jxgzxs.Text.Trim()) + " where ST_ID=" + CommonFun.ComTryInt(lbstid.Text.Trim()) + "";
                            list.Add(sql000);
                            list.Add(sqlstaffinfo);
                        }
                        else if (rblSHJS.SelectedValue.ToString().Trim() == "2")
                        {
                            sql000 = "update OM_JXADDSP set shstate1='1',shtime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',shnote1='" + opinion1.Text.Trim() + "' where bh='" + spbh + "'";
                            list.Add(sql000);



                            //邮件提醒
                            string sprid = "";
                            string sptitle = "";
                            string spcontent = "";
                            sprid = secondid.Value.Trim();
                            sptitle = "人员绩效审批";
                            spcontent = txtname.Text.Trim() + "的" + lbtitle.Text.Trim() + "需要您审批，请登录查看！";
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                        }
                    }
                    else if (rblfirst.SelectedValue.ToString().Trim() == "2")
                    {
                        sql000 = "update OM_JXADDSP set totalstate='3',shstate1='2',shtime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',shnote1='" + opinion1.Text.Trim() + "' where bh='" + spbh + "'";
                        list.Add(sql000);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核意见！');", true);
                        return;
                    }
                }

                if (dtsave.Rows[0]["shrid2"].ToString().Trim() == Session["UserID"].ToString().Trim() && dtsave.Rows[0]["totalstate"].ToString().Trim() == "1" && rblSHJS.SelectedValue.ToString().Trim() == "2")
                {
                    if (rblsecond.SelectedValue.ToString().Trim() == "1")
                    {
                        sql000 = "update OM_JXADDSP set totalstate='2',shstate2='1',shtime2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',shnote2='" + opinion2.Text.Trim() + "' where bh='" + spbh + "'";
                        sqlstaffinfo = "update TBDS_STAFFINFO set ST_GANGWEIXISHU=" + CommonFun.ComTryDecimal(txt_jxgzxs.Text.Trim()) + " where ST_ID=" + CommonFun.ComTryInt(lbstid.Text.Trim()) + "";
                        list.Add(sql000);
                        list.Add(sqlstaffinfo);
                    }
                    else if (rblsecond.SelectedValue.ToString().Trim() == "2")
                    {
                        sql000 = "update OM_JXADDSP set totalstate='3',shstate2='2',shtime2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',shnote2='" + opinion2.Text.Trim() + "' where bh='" + spbh + "'";
                        list.Add(sql000);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核意见！');", true);
                        return;
                    }
                }

                if (dtsave.Rows[0]["creatstid"].ToString().Trim() == Session["UserID"].ToString().Trim() && dtsave.Rows[0]["totalstate"].ToString().Trim() == "3")
                {
                    sql000 = "update OM_JXADDSP set creattime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',yearmonth='" + tb_yearmonth.Text.Trim() + "',things='" + txt_contents.Text.Trim() + "',jxaddstid='" + lbstid.Text.Trim() + "',jxaddstname='" + txtname.Text.Trim() + "',jxadddepartment='" + txtdepartment.Text.Trim() + "',jxgzxs='" + txt_jxgzxs.Text.Trim() + "',type='" + ddlType.SelectedValue.Trim() + "',totalstate='1',shstate1='0',shstate2='0' where bh='" + spbh + "'";
                    list.Add(sql000);



                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sprid = firstid.Value.Trim();
                    sptitle = "人员绩效审批";
                    spcontent = txtname.Text.Trim() + "的" + lbtitle.Text.Trim() + "需要您审批，请登录查看！";
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                }
            }
            DBCallCommon.ExecuteTrans(list);
            Response.Redirect("OM_JXADDSPdetail.aspx?spid=" + spbh);
        }


        protected void rblSHJS_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            bindmxdata();//绑定数据
            
            contrlkjx();//控件可见性和可用性
        }



        protected void Textname_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);

            if (num > 0)
            {
                string stid = (sender as TextBox).Text.Trim().Substring(0, num);

                string sqlText = "select * from View_TBDS_STAFFINFO where ST_ID='" + stid + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                if (dt.Rows.Count > 0)
                {
                    lbstid.Text = stid;
                    txtname.Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    txtdepartment.Text = dt.Rows[0]["DEP_NAME"].ToString().Trim();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员不存在，请重新输入！');", true);
                }
            }
        }

        //反审
        protected void btnfanshen_OnClick(object sender, EventArgs e)
        {
            List<string> list0 = new List<string>();
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqltext = "update OM_JXADDSP set totalstate='0',shstate1='0',shstate2='0' where bh='" + spbh + "'";
            list0.Add(sqltext);
            DBCallCommon.ExecuteTrans(list0);
            Response.Redirect("OM_JXADDSPdetail.aspx?spid=" + spbh);
        }
    }
}
