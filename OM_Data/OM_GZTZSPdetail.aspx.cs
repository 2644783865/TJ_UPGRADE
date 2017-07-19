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
    public partial class OM_GZTZSPdetail : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        string spbh = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindmxdata();//绑定数据
                bindspdata();//绑定审批数据
                contrlkjx();//控件可见性和可用性
            }
        }
        private void bindmxdata()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqltextmx = "select * from OM_GZTZSP as a left join OM_GZTZdetal as b on a.GZTZSP_SPBH=b.GZTZ_SPBH left join TBDS_STAFFINFO as c on b.GZTZ_STID=c.ST_ID left join TBDS_DEPINFO as d on c.ST_DEPID=d.DEP_CODE where GZTZSP_SPBH='" + spbh.ToString().Trim() + "'";
            DataTable dtmx = DBCallCommon.GetDTUsingSqlText(sqltextmx);
            if (dtmx.Rows.Count > 0)
            {
                rptProNumCost.DataSource = dtmx;
                rptProNumCost.DataBind();
            }
        }
        private void bindspdata()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqltextsp = "select * from OM_GZTZSP where GZTZSP_SPBH='" + spbh.ToString().Trim() + "'";
            DataTable dtsp = DBCallCommon.GetDTUsingSqlText(sqltextsp);
            if (dtsp.Rows.Count > 0)
            {
                lbtitle_zdr.Text = dtsp.Rows[0]["GZTZSP_SQRNAME"].ToString().Trim();
                lbtitle_zdsj.Text = dtsp.Rows[0]["GZTZSP_SQTIME"].ToString().Trim();
                lb_title.Text = "(" + dtsp.Rows[0]["GZTZSP_YEARMONTH"].ToString().Trim() + ")";


                tb_yearmonth.Text = dtsp.Rows[0]["GZTZSP_YEARMONTH"].ToString().Trim();
                lbzdr.Text = dtsp.Rows[0]["GZTZSP_SQRNAME"].ToString().Trim();
                lbzdtime.Text = dtsp.Rows[0]["GZTZSP_SQTIME"].ToString().Trim();
                tbfqryj.Text = dtsp.Rows[0]["GZTZSP_SQRNOTE"].ToString().Trim();
                txt_first.Text = dtsp.Rows[0]["GZTZSP_SPRNAME"].ToString().Trim();
                firstid.Value = dtsp.Rows[0]["GZTZSP_SPRID"].ToString().Trim();
                if (dtsp.Rows[0]["GZTZSP_SPRSTATE"].ToString().Trim() == "1" || dtsp.Rows[0]["GZTZSP_SPRSTATE"].ToString().Trim() == "2")
                {
                    rblfirst.SelectedValue = dtsp.Rows[0]["GZTZSP_SPRSTATE"].ToString().Trim();
                }
                first_time.Text = dtsp.Rows[0]["GZTZSP_SPTIME"].ToString().Trim();
                opinion1.Text = dtsp.Rows[0]["GZTZSP_SPRNOTE"].ToString().Trim();
            }
        }
        private void contrlkjx()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqltextkj = "select * from OM_GZTZSP where GZTZSP_SPBH='" + spbh.ToString().Trim() + "'";
            DataTable dtkj = DBCallCommon.GetDTUsingSqlText(sqltextkj);
            if (dtkj.Rows.Count > 0)
            {
                if (dtkj.Rows[0]["TOTALSTATE"].ToString().Trim() == "2")
                {
                    ImageVerify.Visible = true;
                }
                if (dtkj.Rows[0]["TOTALSTATE"].ToString().Trim() == "0" && dtkj.Rows[0]["GZTZSP_SPRSTATE"].ToString().Trim() == "0")//初始化
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["GZTZSP_SQRSTID"].ToString().Trim())
                    {
                        btnadd.Visible = true;
                        btnbaocun.Visible = true;
                        tb_yearmonth.Enabled = true;
                        btnSave.Visible = true;
                        txt_first.Enabled = true;
                        hlSelect1.Visible = true;
                        tbfqryj.Enabled = true;
                        btndelete.Visible = true;
                    }
                }
                else if (dtkj.Rows[0]["TOTALSTATE"].ToString().Trim() == "1" && dtkj.Rows[0]["GZTZSP_SPRSTATE"].ToString().Trim() == "0")//提交未审
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["GZTZSP_SPRID"].ToString().Trim())
                    {
                        btnSave.Visible = true;
                        rblfirst.Enabled = true;
                        opinion1.Enabled = true;
                        for (int i = 0; i < rptProNumCost.Items.Count; i++)
                        {
                            TextBox tb_GZTZ_BFJBF = (TextBox)rptProNumCost.Items[i].FindControl("tb_GZTZ_BFJBF");
                            TextBox tb_GZTZ_BFZYBF = (TextBox)rptProNumCost.Items[i].FindControl("tb_GZTZ_BFZYBF");

                            TextBox tb_GZTZ_BF = (TextBox)rptProNumCost.Items[i].FindControl("tb_GZTZ_BF");
                            TextBox tb_GZTZ_BK = (TextBox)rptProNumCost.Items[i].FindControl("tb_GZTZ_BK");
                            TextBox tb_GZTZ_NOTE = (TextBox)rptProNumCost.Items[i].FindControl("tb_GZTZ_NOTE");
                            tb_GZTZ_BFJBF.Enabled = false;
                            tb_GZTZ_BFZYBF.Enabled = false;

                            tb_GZTZ_BF.Enabled = false;
                            tb_GZTZ_BK.Enabled = false;
                            tb_GZTZ_NOTE.Enabled = false;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < rptProNumCost.Items.Count; j++)
                    {
                        TextBox tb_GZTZ_BFJBF = (TextBox)rptProNumCost.Items[j].FindControl("tb_GZTZ_BFJBF");
                        TextBox tb_GZTZ_BFZYBF = (TextBox)rptProNumCost.Items[j].FindControl("tb_GZTZ_BFZYBF");

                        TextBox tb_GZTZ_BF = (TextBox)rptProNumCost.Items[j].FindControl("tb_GZTZ_BF");
                        TextBox tb_GZTZ_BK = (TextBox)rptProNumCost.Items[j].FindControl("tb_GZTZ_BK");
                        TextBox tb_GZTZ_NOTE = (TextBox)rptProNumCost.Items[j].FindControl("tb_GZTZ_NOTE");
                        tb_GZTZ_BFJBF.Enabled = false;
                        tb_GZTZ_BFZYBF.Enabled = false;

                        tb_GZTZ_BF.Enabled = false;
                        tb_GZTZ_BK.Enabled = false;
                        tb_GZTZ_NOTE.Enabled = false;
                    }
                    if (dtkj.Rows[0]["TOTALSTATE"].ToString().Trim() == "3" && Session["UserID"].ToString().Trim() == dtkj.Rows[0]["GZTZSP_SQRSTID"].ToString().Trim())
                    {
                        btnfanshen.Visible = true;
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
            string sql001 = "";
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqltext = "select * from OM_GZTZSP where GZTZSP_SPBH='" + spbh.ToString().Trim() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["GZTZSP_SQRSTID"].ToString().Trim() == Session["UserID"].ToString().Trim())
                {
                    if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                        return;
                    }
                    else
                    {
                        for (int k = 0; k < rptProNumCost.Items.Count; k++)
                        {
                            TextBox tb_GZTZ_BK = (TextBox)rptProNumCost.Items[k].FindControl("tb_GZTZ_BK");
                            if (CommonFun.ComTryDecimal(tb_GZTZ_BK.Text.Trim()) > 0)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('补扣应为负数！');", true);
                                return;
                            }
                        }
                        sql001 = "update OM_GZTZSP set TOTALSTATE='1',GZTZSP_SQRNOTE='" + tbfqryj.Text.Trim() + "',GZTZSP_SPRID='" + firstid.Value.Trim() + "',GZTZSP_SPRNAME='" + txt_first.Text.Trim() + "' where GZTZSP_SPBH='" + spbh.ToString().Trim() + "'";
                        list.Add(sql001);

                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sprid = firstid.Value.Trim();
                        sptitle = "薪酬异动审批";
                        spcontent = tb_yearmonth.Text.Trim() + "的薪酬异动需要您审批，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                }
                if (dt.Rows[0]["GZTZSP_SPRID"].ToString().Trim() == Session["UserID"].ToString().Trim())
                {
                    if (rblfirst.SelectedValue.ToString().Trim() == "1")
                    {
                        sql001 = "update OM_GZTZSP set GZTZSP_SPRSTATE='" + rblfirst.SelectedValue.ToString().Trim() + "',TOTALSTATE='2',GZTZSP_SPTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',GZTZSP_SPRNOTE='" + opinion1.Text.Trim() + "' where GZTZSP_SPBH='" + spbh.ToString().Trim() + "'";
                        list.Add(sql001);
                        updategzqd(spbh);
                        updategeshui(spbh);
                    }
                    else if (rblfirst.SelectedValue.ToString().Trim() == "2")
                    {
                        sql001 = "update OM_GZTZSP set GZTZSP_SPRSTATE='" + rblfirst.SelectedValue.ToString().Trim() + "',TOTALSTATE='3',GZTZSP_SPTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',GZTZSP_SPRNOTE='" + opinion1.Text.Trim() + "' where GZTZSP_SPBH='" + spbh.ToString().Trim() + "'";
                        list.Add(sql001);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核意见！');", true);
                        return;
                    }
                }
            }
            DBCallCommon.ExecuteTrans(list);
            Response.Redirect("OM_GZTZSPdetail.aspx?spid=" + spbh);
        }
        //更改工资清单数据
        private void updategzqd(string spbh)
        {
            List<string> listgzqd = new List<string>();
            string sqltext00="select * from OM_GZTZSP as a left join OM_GZTZdetal as b on a.GZTZSP_SPBH=b.GZTZ_SPBH where GZTZSP_SPBH='"+spbh.Trim()+"'";
            DataTable dt00=DBCallCommon.GetDTUsingSqlText(sqltext00);
            if(dt00.Rows.Count>0)
            {
                string sqlcheck = "select * from OM_GZHSB where OM_GZSCBZ='1' and GZ_YEARMONTH='" + dt00.Rows[0]["GZTZSP_YEARMONTH"].ToString().Trim() + "'";
                DataTable dtcheck=DBCallCommon.GetDTUsingSqlText(sqlcheck);
                if (dtcheck.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月工资已审核通过！');", true);
                    return;
                }
                else
                {
                    for (int i = 0; i < dt00.Rows.Count; i++)
                    {
                        string sqlgzqd = "update OM_GZQD set QD_BFJB='" + CommonFun.ComTryDecimal(dt00.Rows[i]["GZTZ_BFJBF"].ToString().Trim()) + "',QD_BFZYB='" + CommonFun.ComTryDecimal(dt00.Rows[i]["GZTZ_BFZYBF"].ToString().Trim()) + "',QD_TZBF=" + CommonFun.ComTryDecimal(dt00.Rows[i]["GZTZ_BF"].ToString().Trim()) + ",QD_TZBK=" + CommonFun.ComTryDecimal(dt00.Rows[i]["GZTZ_BK"].ToString().Trim()) + " where QD_YEARMONTH='" + dt00.Rows[i]["GZTZSP_YEARMONTH"].ToString().Trim() + "' and QD_STID='" + dt00.Rows[i]["GZTZ_STID"].ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                        listgzqd.Add(sqlgzqd);
                    }
                }
            }
            DBCallCommon.ExecuteTrans(listgzqd);
        }


        private void updategeshui(string spbh)
        {
            List<string> listgeshui = new List<string>();
            string sqltext01="select * from OM_GZTZSP as a left join OM_GZTZdetal as b on a.GZTZSP_SPBH=b.GZTZ_SPBH where GZTZSP_SPBH='"+spbh.Trim()+"'";
            DataTable dt01=DBCallCommon.GetDTUsingSqlText(sqltext01);
            if (dt01.Rows.Count > 0)
            {
                for (int j = 0; j < dt01.Rows.Count; j++)
                {
                    string gesdatasource = "select *,((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ)-QD_KOUXIANG) as QD_KOUSJS from OM_GZQD left join OM_KQTJ on KQ_ST_ID=QD_STID and KQ_DATE=QD_YEARMONTH where QD_YEARMONTH='" + dt01.Rows[j]["GZTZSP_YEARMONTH"].ToString().Trim() + "' and QD_STID='" + dt01.Rows[j]["GZTZ_STID"].ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                    System.Data.DataTable dtdatasource = DBCallCommon.GetDTUsingSqlText(gesdatasource);
                    //个税
                    if (dtdatasource.Rows.Count > 0)
                    {
                        double ksjsmoney = CommonFun.ComTryDouble(dtdatasource.Rows[0]["QD_KOUSJS"].ToString().Trim());
                        double jsmoney = ksjsmoney - 3500;
                        double geshui = 0;
                        if (jsmoney > 0)
                        {
                            if (jsmoney < 1500)
                            {
                                geshui = jsmoney * 0.03;
                            }
                            else if (jsmoney >= 1500 && jsmoney < 4500)
                            {
                                geshui = jsmoney * 0.1 - 105;
                            }
                            else if (jsmoney >= 4500 && jsmoney < 9000)
                            {
                                geshui = jsmoney * 0.2 - 555;
                            }
                            else if (jsmoney >= 9000 && jsmoney < 35000)
                            {
                                geshui = jsmoney * 0.25 - 1005;
                            }
                            else
                            {
                                geshui = 0;
                            }
                        }
                        string insertgeshui = "update OM_GZQD set QD_GeShui=" + Math.Round(geshui, 2) + " where QD_YEARMONTH='" + dt01.Rows[j]["GZTZSP_YEARMONTH"].ToString().Trim() + "' and QD_STID='" + dt01.Rows[j]["GZTZ_STID"].ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                        listgeshui.Add(insertgeshui);
                    }
                }
                DBCallCommon.ExecuteTrans(listgeshui);
            }
        }


        //添加行
        protected void btnadd_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (int.TryParse(txtNum.Text, out a))
            {
                CreateNewRow(a);
            }
            else
            {
                Response.Write("<script>alert('请输入数字！')</script>");
            }
        }

        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            List<string> col = new List<string>();
            this.rptProNumCost.DataSource = dt;
            this.rptProNumCost.DataBind();
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("GZTZ_ID");
            dt.Columns.Add("ST_NAME");
            dt.Columns.Add("GZTZ_STID");
            dt.Columns.Add("ST_WORKNO");
            dt.Columns.Add("DEP_NAME");

            dt.Columns.Add("GZTZ_BFJBF");
            dt.Columns.Add("GZTZ_BFZYBF");

            dt.Columns.Add("GZTZ_BF");
            dt.Columns.Add("GZTZ_BK");
            dt.Columns.Add("GZTZ_NOTE");
            foreach (RepeaterItem retItem in rptProNumCost.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((Label)retItem.FindControl("lb_GZTZ_ID")).Text;
                newRow[1] = ((TextBox)retItem.FindControl("lb_ST_NAME")).Text;
                newRow[2] = ((Label)retItem.FindControl("lbstid")).Text;
                newRow[3] = ((TextBox)retItem.FindControl("lb_ST_WORKNO")).Text;
                newRow[4] = ((TextBox)retItem.FindControl("lbDEP_NAME")).Text;

                newRow[5] = ((TextBox)retItem.FindControl("tb_GZTZ_BFJBF")).Text;
                newRow[6] = ((TextBox)retItem.FindControl("tb_GZTZ_BFZYBF")).Text;

                newRow[7] = ((TextBox)retItem.FindControl("tb_GZTZ_BF")).Text;
                newRow[8] = ((TextBox)retItem.FindControl("tb_GZTZ_BK")).Text;
                newRow[9] = ((TextBox)retItem.FindControl("tb_GZTZ_NOTE")).Text;
                
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        protected void Textname_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            TextBox Tb_newstid = (TextBox)sender;
            RepeaterItem Reitem = (RepeaterItem)Tb_newstid.Parent;

            if (num > 0)
            {
                string stid = (sender as TextBox).Text.Trim().Substring(0, num);

                string sqlText = "select * from View_TBDS_STAFFINFO where ST_ID='" + stid + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                if (dt.Rows.Count > 0)
                {
                    ((Label)Reitem.FindControl("lbstid")).Text = stid;
                    ((TextBox)Reitem.FindControl("lb_ST_NAME")).Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("lbDEP_NAME")).Text = dt.Rows[0]["DEP_NAME"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("lb_ST_WORKNO")).Text = dt.Rows[0]["ST_WORKNO"].ToString().Trim();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员不存在，请重新输入！');", true);
                }
            }
        }


        //删除
        protected void btndelete_OnClick(object sender, EventArgs e)
        {
            List<string> col = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("GZTZ_ID");
            dt.Columns.Add("ST_NAME");
            dt.Columns.Add("GZTZ_STID");
            dt.Columns.Add("ST_WORKNO");
            dt.Columns.Add("DEP_NAME");

            dt.Columns.Add("GZTZ_BFJBF");
            dt.Columns.Add("GZTZ_BFZYBF");

            dt.Columns.Add("GZTZ_BF");
            dt.Columns.Add("GZTZ_BK");
            dt.Columns.Add("GZTZ_NOTE");
            foreach (RepeaterItem retItem in rptProNumCost.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("CKBOX_SELECT");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((Label)retItem.FindControl("lb_GZTZ_ID")).Text;
                    newRow[1] = ((TextBox)retItem.FindControl("lb_ST_NAME")).Text;
                    newRow[2] = ((Label)retItem.FindControl("lbstid")).Text;
                    newRow[3] = ((TextBox)retItem.FindControl("lb_ST_WORKNO")).Text;
                    newRow[4] = ((TextBox)retItem.FindControl("lbDEP_NAME")).Text;

                    newRow[5] = ((TextBox)retItem.FindControl("tb_GZTZ_BFJBF")).Text;
                    newRow[6] = ((TextBox)retItem.FindControl("tb_GZTZ_BFZYBF")).Text;

                    newRow[7] = ((TextBox)retItem.FindControl("tb_GZTZ_BF")).Text;
                    newRow[8] = ((TextBox)retItem.FindControl("tb_GZTZ_BK")).Text;
                    newRow[9] = ((TextBox)retItem.FindControl("tb_GZTZ_NOTE")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.rptProNumCost.DataSource = dt;
            this.rptProNumCost.DataBind();
        }

        //保存
        protected void btnbaocun_click(object sender,EventArgs e)
        {
            List<string> list = new List<string>();
            int num = 0;
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sql000 = "";
            string sqlifcz = "select * from View_OM_GZQD where QD_YEARMONTH='" + tb_yearmonth.Text.Trim() + "' and (OM_GZSCBZ='0' or OM_GZSCBZ='1')";
            System.Data.DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifcz);
            if (dtifsc.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月工资清单正在审核或已审核通过！');", true);
                return;
            }
            for (int i = 0; i < rptProNumCost.Items.Count; i++)
            {
                if (((Label)rptProNumCost.Items[i].FindControl("lbstid")).Text.Trim() != "")
                {
                    num++;
                }
            }
            if (num == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请填写要调整的数据！');", true);
                return;
            }

            if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                return;
            }
            else if (tb_yearmonth.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月份！');", true);
                return;
            }
            else
            {
                //删除已有数据
                string sqldelete = "delete from OM_GZTZdetal where GZTZ_SPBH='" + spbh + "'";
                DBCallCommon.ExeSqlText(sqldelete);
                //重新插入数据
                for (int k = 0; k < rptProNumCost.Items.Count; k++)
                {
                    Label lbstid = (Label)rptProNumCost.Items[k].FindControl("lbstid");

                    TextBox tb_GZTZ_BFJBF = (TextBox)rptProNumCost.Items[k].FindControl("tb_GZTZ_BFJBF");
                    TextBox tb_GZTZ_BFZYBF = (TextBox)rptProNumCost.Items[k].FindControl("tb_GZTZ_BFZYBF");

                    TextBox tb_GZTZ_BF = (TextBox)rptProNumCost.Items[k].FindControl("tb_GZTZ_BF");
                    TextBox tb_GZTZ_BK = (TextBox)rptProNumCost.Items[k].FindControl("tb_GZTZ_BK");
                    TextBox tb_GZTZ_NOTE = (TextBox)rptProNumCost.Items[k].FindControl("tb_GZTZ_NOTE");
                    if (CommonFun.ComTryDecimal(tb_GZTZ_BK.Text.Trim()) > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('补扣应为负数！');", true);
                        return;
                    }
                    else
                    {
                        if (((Label)rptProNumCost.Items[k].FindControl("lbstid")).Text.Trim() != "" || ((TextBox)rptProNumCost.Items[k].FindControl("lb_ST_NAME")).Text.Trim() != "")
                        {
                            sql000 = "insert into OM_GZTZdetal(GZTZ_SPBH,GZTZ_YEARMONTH,GZTZ_STID,GZTZ_BFJBF,GZTZ_BFZYBF,GZTZ_BF,GZTZ_BK,GZTZ_NOTE) values('" + spbh + "','" + tb_yearmonth.Text.Trim() + "','" + lbstid.Text.Trim() + "'," + CommonFun.ComTryDecimal(tb_GZTZ_BFJBF.Text.Trim()) + "," + CommonFun.ComTryDecimal(tb_GZTZ_BFZYBF.Text.Trim()) + "," + CommonFun.ComTryDecimal(tb_GZTZ_BF.Text.Trim()) + "," + CommonFun.ComTryDecimal(tb_GZTZ_BK.Text.Trim()) + ",'" + tb_GZTZ_NOTE.Text.Trim() + "')";
                            list.Add(sql000);
                        }
                    }
                }
                string sqlupdatesp = "update OM_GZTZSP set TOTALSTATE='0',GZTZSP_SQRNOTE='" + tbfqryj.Text.Trim() + "',GZTZSP_SPRID='" + firstid.Value.Trim() + "',GZTZSP_SPRNAME='" + txt_first.Text.Trim() + "' where GZTZSP_SPBH='" + spbh + "'";
                list.Add(sqlupdatesp);
                DBCallCommon.ExecuteTrans(list);
                Response.Redirect("OM_GZTZSPdetail.aspx?spid=" + spbh);
            }
        }


        //反审
        protected void btnfanshen_OnClick(object sender, EventArgs e)
        {
            List<string> list0 = new List<string>();
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqltext = "update OM_GZTZSP set TOTALSTATE='0',GZTZSP_SPRSTATE='0' where GZTZSP_SPBH='" + spbh + "'";
            list0.Add(sqltext);
            DBCallCommon.ExecuteTrans(list0);
            Response.Redirect("OM_GZTZSPdetail.aspx?spid=" + spbh);
        }
    }
}
