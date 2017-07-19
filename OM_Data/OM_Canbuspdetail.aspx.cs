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
    public partial class OM_Canbuspdetail : System.Web.UI.Page
    {
        string spbh = "";
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                spbh = Request.QueryString["spid"].ToString().Trim();
                
                bindshenhdata();
                bindkjkyx();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindrpt();
            }
            if (Session["UserName"].ToString().Trim() == "管理员")
            {
                btnfanshen.Visible = true;
            }
        }


        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数

        }

        /// <summary>
        /// 分页初始化
        /// </summary>
        /// <param name="where"></param>
        private void InitPager()
        {
            pager_org.TableName = "(select * from OM_Canbusp left join OM_CanBu on OM_Canbusp.bh=OM_CanBu.detailbh left join OM_KQTJ on (OM_CanBu.CB_STID=OM_KQTJ.KQ_ST_ID and OM_CanBu.CB_YearMonth=OM_KQTJ.KQ_DATE) left join TBDS_STAFFINFO on OM_CanBu.CB_STID=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t";
            pager_org.PrimaryKey = "CB_ID";
            pager_org.ShowFields = "*,(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)) as CB_MonthCB,(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)+CB_BuShangYue) as CB_HeJi ";
            pager_org.OrderField = "CB_ID,DEP_NAME,CB_YearMonth";
            pager_org.StrWhere = "bh='" + Request.QueryString["spid"].ToString().Trim() + "'";
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 230;
        }

        /// <summary>
        /// 换页事件
        /// </summary>
        private void Pager_PageChanged(int pageNumber)
        {
            bindrpt();
        }

        private void bindrpt()
        {
            InitPager();
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptCanBu, UCPaging1, palNoData1);
            if (palNoData1.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        //控件可用性...
        private void bindkjkyx()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqlkjdata = "select * from OM_Canbusp where bh='" + spbh + "'";
            DataTable dtkj = DBCallCommon.GetDTUsingSqlText(sqlkjdata);
            //审核级数
            if (rblSPJB.SelectedValue == "3")
            {
                sjshh.Visible = true;
            }
            else
            {
                sjshh.Visible = false;
            }

            if (dtkj.Rows.Count > 0)
            {
                if (dtkj.Rows[0]["state"].ToString().Trim() == "2")
                {
                    ImageVerify.Visible = true;
                }
                if (dtkj.Rows[0]["state"].ToString().Trim() == "0" && dtkj.Rows[0]["SHSTATE1"].ToString().Trim() == "0")//初始化
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["SCRID"].ToString().Trim())
                    {
                        rblSPJB.Enabled = true;
                        btndelete.Visible = true;
                        btnSave.Visible = true;
                        txt_first.Enabled = true;
                        hlSelect1.Visible = true;
                        txt_second.Enabled = true;
                        hlSelect2.Visible = true;
                        txt_third.Enabled = true;
                        hlSelect3.Visible = true;
                        tbfqryj.Enabled = true;
                    }
                }
                else if (dtkj.Rows[0]["state"].ToString().Trim() == "1" && dtkj.Rows[0]["SHSTATE1"].ToString().Trim() == "0")//提交未审
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["SHRID1"].ToString().Trim())
                    {
                        rblSPJB.Enabled = false;
                        btndelete.Visible = false;
                        btnSave.Visible = true;
                        rblfirst.Enabled = true;
                        opinion1.Enabled = true;
                    }
                }
                else if (dtkj.Rows[0]["state"].ToString().Trim() == "1" && dtkj.Rows[0]["SHSTATE1"].ToString().Trim() == "1" && dtkj.Rows[0]["SHSTATE2"].ToString().Trim() == "0")//一级审核通过
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["SHRID2"].ToString().Trim())
                    {
                        rblSPJB.Enabled = false;
                        btndelete.Visible = false;
                        btnSave.Visible = true;
                        rblsecond.Enabled = true;
                        opinion2.Enabled = true;
                    }
                }
                else if (dtkj.Rows[0]["state"].ToString().Trim() == "1" && dtkj.Rows[0]["SHSTATE1"].ToString().Trim() == "1" && dtkj.Rows[0]["SHSTATE2"].ToString().Trim() == "1" && dtkj.Rows[0]["SHSTATE3"].ToString().Trim() == "0")//二级审核通过
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["SHRID3"].ToString().Trim())
                    {
                        rblSPJB.Enabled = false;
                        btndelete.Visible = false;
                        btnSave.Visible = true;
                        rblResult3.Enabled = true;
                        third_opinion.Enabled = true;
                    }
                }
                else if (dtkj.Rows[0]["state"].ToString().Trim() == "3")
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["SCRID"].ToString().Trim())
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

        //绑定审核数据
        private void bindshenhdata()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqlshdata = "select * from OM_Canbusp where bh='" + spbh + "'";
            DataTable dtsh = DBCallCommon.GetDTUsingSqlText(sqlshdata);
            if (dtsh.Rows.Count > 0)
            {
                lbzdr.Text = dtsh.Rows[0]["SCRNAME"].ToString().Trim();
                lbzdtime.Text = dtsh.Rows[0]["SCTIME"].ToString().Trim();
                tbfqryj.Text = dtsh.Rows[0]["SCRNOTE"].ToString().Trim();

                lbtitle_zdr.Text = dtsh.Rows[0]["SCRNAME"].ToString().Trim();
                lbtitle_zdsj.Text = dtsh.Rows[0]["SCTIME"].ToString().Trim();
                lb_title.Text = "(" + dtsh.Rows[0]["YEARMONTH"].ToString().Trim() + ")";

                txt_first.Text = dtsh.Rows[0]["SHRNAME1"].ToString().Trim();
                firstid.Value = dtsh.Rows[0]["SHRID1"].ToString().Trim();
                if (dtsh.Rows[0]["SHSTATE1"].ToString().Trim() == "1" || dtsh.Rows[0]["SHSTATE1"].ToString().Trim() == "2")
                {
                    rblfirst.SelectedValue = dtsh.Rows[0]["SHSTATE1"].ToString().Trim();
                }
                first_time.Text = dtsh.Rows[0]["SHTIME1"].ToString().Trim();
                opinion1.Text = dtsh.Rows[0]["SHNOTE1"].ToString().Trim();

                txt_second.Text = dtsh.Rows[0]["SHRNAME2"].ToString().Trim();
                secondid.Value = dtsh.Rows[0]["SHRID2"].ToString().Trim();
                if (dtsh.Rows[0]["SHSTATE2"].ToString().Trim() == "1" || dtsh.Rows[0]["SHSTATE2"].ToString().Trim() == "2")
                {
                    rblsecond.SelectedValue = dtsh.Rows[0]["SHSTATE2"].ToString().Trim();
                }
                second_time.Text = dtsh.Rows[0]["SHTIME2"].ToString().Trim();
                opinion2.Text = dtsh.Rows[0]["SHNOTE2"].ToString().Trim();

                txt_third.Text = dtsh.Rows[0]["SHRNAME3"].ToString().Trim();
                thirdid.Value = dtsh.Rows[0]["SHRID3"].ToString().Trim();
                if (dtsh.Rows[0]["SHSTATE3"].ToString().Trim() == "1" || dtsh.Rows[0]["SHSTATE3"].ToString().Trim() == "2")
                {
                    rblResult3.SelectedValue = dtsh.Rows[0]["SHSTATE3"].ToString().Trim();
                }
                third_time.Text = dtsh.Rows[0]["SHTIME3"].ToString().Trim();
                third_opinion.Text = dtsh.Rows[0]["SHNOTE3"].ToString().Trim();

                if (dtsh.Rows[0]["SHJISHU"].ToString().Trim() == "3")
                {
                    rblSPJB.SelectedValue = "3";
                }
                else
                {
                    rblSPJB.SelectedValue = "2";
                }
            }
        }
        //审核级别修改
        protected void rblSPJB_onchange(object sender,EventArgs e)
        {
            if(rblSPJB.SelectedValue == "3")
            {
                sjshh.Visible = true;
            }
            else
            {
                sjshh.Visible = false;
            }
        }

        //提交...
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqlsavedata = "select * from OM_Canbusp where bh='" + spbh + "'";
            DataTable dtsave = DBCallCommon.GetDTUsingSqlText(sqlsavedata);
            if (dtsave.Rows.Count > 0)
            {
                if (dtsave.Rows[0]["SCRID"].ToString().Trim() == Session["UserID"].ToString().Trim())
                {
                    if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "" || txt_second.Text.Trim() == "" || secondid.Value.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                        return;
                    }
                    else
                    {
                        string sqlupdate0 = "update OM_Canbusp set state='1',SCRNOTE='" + tbfqryj.Text.Trim() + "',SHRID1='" + firstid.Value.Trim() + "',SHRNAME1='" + txt_first.Text.Trim() + "',SHRID2='" + secondid.Value.Trim() + "',SHRNAME2='" + txt_second.Text.Trim() + "',SHRID3='" + thirdid.Value.Trim() + "',SHRNAME3='" + txt_third.Text.Trim() + "',SHJISHU='" + rblSPJB.SelectedValue.Trim() + "' where bh='" + spbh + "'";
                        DBCallCommon.ExeSqlText(sqlupdate0);

                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sprid = firstid.Value.Trim();
                        sptitle = "餐补数据审批";
                        spcontent = lb_title.Text.Trim() + "的餐补数据需要您审批，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                }
                if (dtsave.Rows[0]["SHRID1"].ToString().Trim() == Session["UserID"].ToString().Trim())
                {
                    string sqlupdate1 = "";
                    if (rblfirst.SelectedValue.ToString().Trim() == "1")
                    {
                        sqlupdate1 = "update OM_Canbusp set SHSTATE1='1',SHTIME1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SHNOTE1='" + opinion1.Text.Trim() + "' where bh='" + spbh + "'";
                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sprid = secondid.Value.Trim();
                        sptitle = "餐补数据审批";
                        spcontent = lb_title.Text.Trim() + "的餐补数据需要您审批，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                    else if (rblfirst.SelectedValue.ToString().Trim() == "2")
                    {
                        sqlupdate1 = "update OM_Canbusp set SHSTATE1='2',state='3',SHTIME1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SHNOTE1='" + opinion1.Text.Trim() + "' where bh='" + spbh + "'";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核意见！');", true);
                        return;
                    }
                    DBCallCommon.ExeSqlText(sqlupdate1);
                }
                if (dtsave.Rows[0]["SHRID2"].ToString().Trim() == Session["UserID"].ToString().Trim())
                {
                    string sqlupdate2 = "";
                    if (rblsecond.SelectedValue.ToString().Trim() == "1")
                    {
                        //邮件提醒
                        if (rblSPJB.SelectedValue.Trim() == "3")
                        {
                            sqlupdate2 = "update OM_Canbusp set SHSTATE2='1',SHTIME2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SHNOTE2='" + opinion2.Text.Trim() + "' where bh='" + spbh + "'";
                            string sprid = "";
                            string sptitle = "";
                            string spcontent = "";
                            sprid = thirdid.Value.Trim();
                            sptitle = "餐补数据审批";
                            spcontent = lb_title.Text.Trim() + "的餐补数据需要您审批，请登录查看！";
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                        }
                        else
                        {
                            sqlupdate2 = "update OM_Canbusp set SHSTATE2='1',state='2',SHTIME2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SHNOTE2='" + opinion2.Text.Trim() + "' where bh='" + spbh + "'";
                        }
                    }
                    else if (rblsecond.SelectedValue.ToString().Trim() == "2")
                    {
                        sqlupdate2 = "update OM_Canbusp set SHSTATE2='2',state='3',SHTIME2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SHNOTE2='" + opinion2.Text.Trim() + "' where bh='" + spbh + "'";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核意见！');", true);
                        return;
                    }
                    DBCallCommon.ExeSqlText(sqlupdate2);
                }
                if (dtsave.Rows[0]["SHRID3"].ToString().Trim() == Session["UserID"].ToString().Trim())
                {
                    string sqlupdate3 = "";
                    if (rblResult3.SelectedValue.ToString().Trim() == "1")
                    {
                        sqlupdate3 = "update OM_Canbusp set SHSTATE3='1',state='2',SHTIME3='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SHNOTE3='" + third_opinion.Text.Trim() + "' where bh='" + spbh + "'";
                        //邮件提醒
                    }
                    else if (rblResult3.SelectedValue.ToString().Trim() == "2")
                    {
                        sqlupdate3 = "update OM_Canbusp set SHSTATE3='2',state='3',SHTIME3='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SHNOTE3='" + third_opinion.Text.Trim() + "' where bh='" + spbh + "'";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核意见！');", true);
                        return;
                    }
                    DBCallCommon.ExeSqlText(sqlupdate3);
                }
            }
            Response.Redirect("OM_Canbuspdetail.aspx?spid=" + spbh);
        }


        //删除
        protected void btndelete_OnClick(object sender, EventArgs e)
        {
            List<string> sqltext = new List<string>();
            sqltext.Clear();
            foreach (RepeaterItem rptitem in rptCanBu.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)rptitem.FindControl("cbxNumber");
                System.Web.UI.WebControls.Label lb = (System.Web.UI.WebControls.Label)rptitem.FindControl("lbCB_ID");
                if (cbx.Checked == true)
                {
                    string sql = "";
                    sql = "delete from OM_CanBu where CB_ID='" + lb.Text + "'";
                    sqltext.Add(sql);
                }
            }
            DBCallCommon.ExecuteTrans(sqltext);
            bindrpt();
        }


        //反审
        protected void btnfanshen_OnClick(object sender, EventArgs e)
        {
            List<string> list0 = new List<string>();
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqltext = "update OM_Canbusp set state='0',SHSTATE1='0',SHSTATE2='0',SHSTATE3='0' where bh='" + spbh + "'";
            list0.Add(sqltext);
            DBCallCommon.ExecuteTrans(list0);
            Response.Redirect("OM_Canbuspdetail.aspx?spid=" + spbh);
        }


        protected void rptCanBu_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqltotal = "select sum(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)) as cbtotal,sum(CB_BuShangYue) as bftotal,sum(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)+CB_BuShangYue) as hjtotal from (select * from OM_Canbusp left join OM_CanBu on OM_Canbusp.bh=OM_CanBu.detailbh left join OM_KQTJ on (OM_CanBu.CB_STID=OM_KQTJ.KQ_ST_ID and OM_CanBu.CB_YearMonth=OM_KQTJ.KQ_DATE) left join TBDS_STAFFINFO on OM_CanBu.CB_STID=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t where bh='" + Request.QueryString["spid"].ToString().Trim() + "'";
                System.Data.DataTable dttotal = DBCallCommon.GetDTUsingSqlText(sqltotal);
                if (dttotal.Rows.Count > 0)
                {
                    System.Web.UI.WebControls.Label lb_ydcb = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_ydcb");
                    System.Web.UI.WebControls.Label lb_bf = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_bf");
                    System.Web.UI.WebControls.Label lb_cbzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_cbzj");


                    lb_ydcb.Text = dttotal.Rows[0]["cbtotal"].ToString().Trim();
                    lb_bf.Text = dttotal.Rows[0]["bftotal"].ToString().Trim();
                    lb_cbzj.Text = dttotal.Rows[0]["hjtotal"].ToString().Trim();
                }
            }
        }
    }
}
