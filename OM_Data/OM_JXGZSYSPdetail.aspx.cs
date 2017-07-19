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
    public partial class OM_JXGZSYSPdetail : System.Web.UI.Page
    {
        string spbh = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddepartment();
                bindmxdata();//绑定数据
                bindspdata();//绑定审批数据
                contrlkjx();//控件可见性和可用性
                bindusermoneydata();
            }
        }

        //明细数据
        private void bindmxdata()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqltext = "select * from OM_JXGZSYSP where bh='" + spbh.ToString().Trim() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                tb_yearmonth.Text = dt.Rows[0]["yearmonth"].ToString().Trim();
                txt_contents.Text = dt.Rows[0]["things"].ToString().Trim();
                lbfqrname.Text = dt.Rows[0]["creatstname"].ToString().Trim();
                lbfqrid.Text = dt.Rows[0]["creatstid"].ToString().Trim();
                lbfqtime.Text = dt.Rows[0]["creattime"].ToString().Trim();

                drpdepartment.SelectedValue = dt.Rows[0]["jxadddepartmentId"].ToString().Trim();
                string sql = "SELECT DISTINCT DEP_CODE FROM TBDS_DEPINFO where DEP_CODE ='" + dt.Rows[0]["jxadddepartmentId"].ToString().Trim() + "'";
                DataTable dtw = DBCallCommon.GetDTUsingSqlText(sql);
                if (dtw.Rows.Count > 0)
                {
                    drpdepartment.SelectedValue = dtw.Rows[0]["DEP_CODE"].ToString().Trim();
                }
            }
        }

        //审批数据
        private void bindspdata()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqlshdata = "select * from OM_JXGZSYSP where bh='" + spbh + "'";
            DataTable dtsh = DBCallCommon.GetDTUsingSqlText(sqlshdata);
            if (dtsh.Rows.Count > 0)
            {
                txt_first.Text = dtsh.Rows[0]["shrname1"].ToString().Trim();
                firstid.Value = dtsh.Rows[0]["shrid1"].ToString().Trim();
                if (dtsh.Rows[0]["shstate1"].ToString().Trim() == "1" || dtsh.Rows[0]["shstate1"].ToString().Trim() == "2")
                {
                    rblfirst.SelectedValue = dtsh.Rows[0]["shstate1"].ToString().Trim();
                }
                first_time.Text = dtsh.Rows[0]["shtime1"].ToString().Trim();
                opinion1.Text = dtsh.Rows[0]["shnote1"].ToString().Trim();

                txt_second.Text = dtsh.Rows[0]["shrname2"].ToString().Trim();
                secondid.Value = dtsh.Rows[0]["shrid2"].ToString().Trim();

                if (dtsh.Rows[0]["shstate2"].ToString().Trim() == "1" || dtsh.Rows[0]["shstate2"].ToString().Trim() == "2")
                {
                    rblsecond.SelectedValue = dtsh.Rows[0]["shstate2"].ToString().Trim();
                }
                second_time.Text = dtsh.Rows[0]["shtime2"].ToString().Trim();
                opinion2.Text = dtsh.Rows[0]["shnote2"].ToString().Trim();
            }
        }


        //控件可用性
        private void contrlkjx()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqlkjdata = "select * from OM_JXGZSYSP where bh='" + spbh + "'";
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
                        drpdepartment.Enabled = true;
                        tb_yearmonth.Enabled = true;
                        txt_contents.Enabled = true;
                        btnSave.Visible = true;
                        txt_first.Enabled = true;
                        hlSelect1.Visible = true;
                        txt_second.Enabled = true;
                        hlSelect2.Visible = true;
                    }
                }
                else if (dtkj.Rows[0]["totalstate"].ToString().Trim() == "1" && dtkj.Rows[0]["shstate1"].ToString().Trim() == "0")//提交未审
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["shrid1"].ToString().Trim() && Session["UserID"].ToString().Trim() != dtkj.Rows[0]["shrid2"].ToString().Trim())
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
                    }
                    else if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["shrid1"].ToString().Trim() && Session["UserID"].ToString().Trim() == dtkj.Rows[0]["shrid2"].ToString().Trim())
                    {
                        btnSave.Visible = true;
                        rblfirst.Enabled = true;
                        opinion1.Enabled = true;
                        rblsecond.Enabled = true;
                        opinion2.Enabled = true;

                        tb_yearmonth.Enabled = false;
                        txt_contents.Enabled = false;
                        txt_first.Enabled = false;
                        hlSelect1.Visible = false;
                        txt_second.Enabled = false;
                        hlSelect2.Visible = false;
                    }

                }
                else if (dtkj.Rows[0]["totalstate"].ToString().Trim() == "1" && dtkj.Rows[0]["shstate1"].ToString().Trim() == "1" && dtkj.Rows[0]["shstate2"].ToString().Trim() == "0")//一级审核通过
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
                    }
                }

                else if (dtkj.Rows[0]["totalstate"].ToString().Trim() == "3")
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["creatstid"].ToString().Trim())
                    {
                        btnfanshen.Visible = true;
                        tb_yearmonth.Enabled = true;
                        txt_contents.Enabled = true;
                        btnSave.Visible = true;
                    }
                    else
                    {
                        btnfanshen.Visible = false;
                    }
                }
            }
        }



        //提交
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string sql000 = "";
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqlsavedata = "select * from OM_JXGZSYSP where bh='" + spbh + "'";
            DataTable dtsave = DBCallCommon.GetDTUsingSqlText(sqlsavedata);
            if (dtsave.Rows.Count > 0)
            {
                if (dtsave.Rows[0]["creatstid"].ToString().Trim() == Session["UserID"].ToString().Trim() && dtsave.Rows[0]["totalstate"].ToString().Trim() == "0")
                {
                    if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "" || txt_second.Text.Trim() == "" || secondid.Value.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                        return;
                    }
                    if (txt_contents.Text.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请填写发起内容！');", true);
                        return;
                    }
                    else
                    {
                        sql000 = "update OM_JXGZSYSP set creattime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',yearmonth='" + tb_yearmonth.Text.Trim() + "',things='" + txt_contents.Text.Trim() + "',jxadddepartment='" + drpdepartment.SelectedItem.Text.Trim() + "',jxadddepartmentId='" + drpdepartment.SelectedValue + "',totalstate='1',shrid1='" + firstid.Value.Trim() + "',shrname1='" + txt_first.Text.Trim() + "',shrid2='" + secondid.Value.Trim() + "',shrname2='" + txt_second.Text.Trim() + "' where bh='" + spbh + "'";
                        list.Add(sql000);

                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sprid = firstid.Value.Trim();
                        sptitle = "绩效工资使用审批";
                        spcontent = drpdepartment.SelectedItem.Text.Trim() + tb_yearmonth.Text.Trim() + "的绩效工资使用需要您审批，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                }

                if (dtsave.Rows[0]["shrid1"].ToString().Trim() == Session["UserID"].ToString().Trim() && dtsave.Rows[0]["totalstate"].ToString().Trim() == "1")
                {
                    if (rblfirst.SelectedValue.ToString().Trim() == "1")
                    {
                        sql000 = "update OM_JXGZSYSP set shstate1='1',shtime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',shnote1='" + opinion1.Text.Trim() + "' where bh='" + spbh + "'";
                        list.Add(sql000);


                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sprid = secondid.Value.Trim();
                        sptitle = "绩效工资使用审批";
                        spcontent = drpdepartment.SelectedItem.Text.Trim() + tb_yearmonth.Text.Trim() + "的绩效工资使用需要您审批，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                    else if (rblfirst.SelectedValue.ToString().Trim() == "2")
                    {
                        sql000 = "update OM_JXGZSYSP set totalstate='3',shstate1='2',shtime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',shnote1='" + opinion1.Text.Trim() + "' where bh='" + spbh + "'";
                        list.Add(sql000);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核意见！');", true);
                        return;
                    }
                }

                if (dtsave.Rows[0]["shrid2"].ToString().Trim() == Session["UserID"].ToString().Trim() && dtsave.Rows[0]["totalstate"].ToString().Trim() == "1")
                {
                    if (rblsecond.SelectedValue.ToString().Trim() == "1")
                    {
                        sql000 = "update OM_JXGZSYSP set totalstate='2',shstate2='1',shtime2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',shnote2='" + opinion2.Text.Trim() + "' where bh='" + spbh + "'";
                        list.Add(sql000);
                    }
                    else if (rblsecond.SelectedValue.ToString().Trim() == "2")
                    {
                        sql000 = "update OM_JXGZSYSP set totalstate='3',shstate2='2',shtime2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',shnote2='" + opinion2.Text.Trim() + "' where bh='" + spbh + "'";
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
                    sql000 = "update OM_JXGZSYSP set creattime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',yearmonth='" + tb_yearmonth.Text.Trim() + "',things='" + txt_contents.Text.Trim() + "',usermoney='" + txtJXSY.ToString().Trim() + "',jxadddepartment='" + drpdepartment.SelectedItem.Text.Trim() + "',jxadddepartmentId='" + drpdepartment.SelectedValue + "',totalstate='1',shstate1='0',shstate2='0' where bh='" + spbh + "'";
                    list.Add(sql000);

                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sprid = firstid.Value.Trim();
                    sptitle = "绩效工资使用审批";
                    spcontent = drpdepartment.SelectedItem.Text.Trim() + tb_yearmonth.Text.Trim() + "的绩效工资使用需要您审批，请登录查看！";
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                }
            }
            DBCallCommon.ExecuteTrans(list);
            //更新总余额
            string sqlgx = "select * from OM_JXGZSYSP where bh='" + spbh + "'";
            DataTable dtgx = DBCallCommon.GetDTUsingSqlText(sqlgx);
            DataRow dr = dtgx.Rows[0];
            string zhuangtai = dr["totalstate"].ToString();
            if (zhuangtai == "2")
            {

                string sqltxt = "";
                sqltxt = "select JiXiaoYE from TBDS_JXZYE where DepId='" + drpdepartment.SelectedValue + "'";
                DataTable dtye = DBCallCommon.GetDTUsingSqlText(sqltxt);
                string ye = dtye.Rows[0]["JiXiaoYE"].ToString().Trim();
                string gx = txtJXSY.Text.ToString().Trim();
                double a = CommonFun.ComTryDouble(ye);
                double b = CommonFun.ComTryDouble(gx);
                double c = a - b;
                string gxye = Convert.ToString(c);
                string sql = "";
                sql = "update  TBDS_JXZYE set JiXiaoYE='" + gxye + "' where DepId='" + drpdepartment.SelectedValue + "'";
                DBCallCommon.ExeSqlText(sql);

            }
            Response.Redirect("OM_JXGZSYSPdetail.aspx?spid=" + spbh);
        }


        private void binddepartment()
        {
            string sql = "SELECT DISTINCT DEP_NAME,DEP_CODE FROM TBDS_DEPINFO WHERE len(DEP_CODE)=2";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            drpdepartment.DataValueField = "DEP_CODE";
            drpdepartment.DataTextField = "DEP_NAME";
            drpdepartment.DataSource = dt;
            drpdepartment.DataBind();
            ListItem item = new ListItem("--请选择--", "0");
            drpdepartment.Items.Insert(0, item);
        }


        //反审
        protected void btnfanshen_OnClick(object sender, EventArgs e)
        {
            List<string> list0 = new List<string>();
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqltext = "update OM_JXGZSYSP set totalstate='0',shstate1='0',shstate2='0' where bh='" + spbh + "'";
            list0.Add(sqltext);
            DBCallCommon.ExecuteTrans(list0);
            Response.Redirect("OM_JXGZSYSPdetail.aspx?spid=" + spbh);
        }

        private void bindusermoneydata()
        {
            string sqltxt = "select usermoney from OM_JXGZSYSP where bh='" + spbh.ToString().Trim() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltxt);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                txtJXSY.Text = dr["usermoney"].ToString();
            }
        }
    }
}
