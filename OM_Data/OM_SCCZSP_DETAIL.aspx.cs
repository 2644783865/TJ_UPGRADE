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
using ZCZJ_DPF;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_SCCZSP_DETAIL : System.Web.UI.Page
    {
        string bh = "";
        PagerQueryParam pager_org1 = new PagerQueryParam();
        PagerQueryParam pager_org2 = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UCPaging1.CurrentPage = 1;
                InitVar1();
                bindGrid1();
                UCPaging2.CurrentPage = 1;
                InitVar2();
                bindGrid2();
                //审核数据绑定
                shenhebind();
                //控件可见性和可操作性
                kjdatabind();   
            }
            InitVar1();
            InitVar2();
        }


        /// <summary>
        /// 初始化分布信息(辅助班组)
        /// </summary>
        private void InitVar1()
        {
            InitPager1();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged1);
            UCPaging1.PageSize = pager_org1.PageSize;    //每页显示的记录数
        }

        /// <summary>
        /// 初始化分布信息(一线)
        /// </summary>
        private void InitVar2()
        {
            InitPager2();
            UCPaging2.PageChanged += new UCPaging.PageHandler(Pager_PageChanged2);
            UCPaging2.PageSize = pager_org2.PageSize;    //每页显示的记录数
        }

        /// <summary>
        /// 分页初始化(辅助班组)
        /// </summary>
        private void InitPager1()
        {
            bh = Request.QueryString["bh"].ToString().Trim();
            pager_org1.TableName = "(select a.*,KQ_ST_ID,KQ_DATE,KQ_CHUQIN,KQ_BINGJ,KQ_SHIJ,KQ_KUANGG,KQ_DAOXIU,KQ_CHANJIA,KQ_PEICHAN,KQ_HUNJIA,KQ_SANGJIA,KQ_GONGS,KQ_NIANX,KQ_ZMJBAN,KQ_JRJIAB,KQ_YSGZ from View_OM_SCCZ_FZBZ as a left join OM_KQTJ as b on (a.FZBZ_STID=b.KQ_ST_ID and a.FZBZ_YEARMONTH=b.KQ_DATE))t";
            pager_org1.PrimaryKey = "ID";
            pager_org1.ShowFields = "ID,FZBZ_BH,FZBZ_YEARMONTH,ST_NAME,DEP_NAME,ST_DEPID1,KQ_CHUQIN,KQ_BINGJ,KQ_SHIJ,KQ_KUANGG,KQ_DAOXIU,KQ_CHANJIA,KQ_PEICHAN,KQ_HUNJIA,KQ_SANGJIA,KQ_GONGS,KQ_NIANX,KQ_ZMJBAN,KQ_JRJIAB,KQ_YSGZ,cast(FZBZ_JXGZ as decimal(12,2)) as FZBZ_JXGZ,cast(FZBZ_GWGZ as decimal(12,2)) as FZBZ_GWGZ,cast(FZBZ_QT as decimal(12,2)) as FZBZ_QT,cast(FZBZ_JBGZ as decimal(12,2)) as FZBZ_JBGZ,cast((isnull(FZBZ_JXGZ,0)+isnull(FZBZ_GWGZ,0)+isnull(FZBZ_QT,0)+isnull(FZBZ_JBGZ,0)) as decimal(12,2)) as FZBZ_XJ";
            pager_org1.OrderField = "ST_DEPID1";
            pager_org1.StrWhere = "FZBZ_BH='" + bh + "'";
            pager_org1.OrderType = 0;
            pager_org1.PageSize = 30;
        }


        /// <summary>
        /// 分页初始化(一线)
        /// </summary>
        private void InitPager2()
        {
            bh = Request.QueryString["bh"].ToString().Trim();
            pager_org2.TableName = "View_OM_SCCZ_YIXIAN";
            pager_org2.PrimaryKey = "ID";
            pager_org2.ShowFields = "ID,YIXIAN_BH,YIXIAN_YEARMONTH,ST_NAME,DEP_NAME,ST_DEPID1,cast(YIXIAN_JXGZ as decimal(12,2)) as YIXIAN_JXGZ,cast(YIXIAN_GWGZ as decimal(12,2)) as YIXIAN_GWGZ,cast(YIXIAN_QT as decimal(12,2)) as YIXIAN_QT,cast((isnull(YIXIAN_JXGZ,0)+isnull(YIXIAN_GWGZ,0)+isnull(YIXIAN_QT,0)) as decimal(12,2)) as YIXIAN_XJ";
            pager_org2.OrderField = "ST_DEPID1";
            pager_org2.StrWhere = "YIXIAN_BH='" + bh + "'";
            pager_org2.OrderType = 0;
            pager_org2.PageSize = 30;
        }


        void Pager_PageChanged1(int pageNumber)
        {
            bindGrid1();
        }


        void Pager_PageChanged2(int pageNumber)
        {
            bindGrid2();
        }


        private void bindGrid1()
        {
            pager_org1.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt1 = CommonFun.GetDataByPagerQueryParam(pager_org1);
            CommonFun.Paging(dt1, rptProNumCost1, UCPaging1, palNoData1);
            if (palNoData1.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }

        }

        private void bindGrid2()
        {
            pager_org2.PageIndex = UCPaging2.CurrentPage;
            System.Data.DataTable dt2 = CommonFun.GetDataByPagerQueryParam(pager_org2);
            CommonFun.Paging(dt2, rptProNumCost2, UCPaging2, palNoData2);
            if (palNoData2.Visible)
            {
                UCPaging2.Visible = false;
            }
            else
            {
                UCPaging2.Visible = true;
                UCPaging2.InitPageInfo();  //分页控件中要显示的控件
            }

        }


        //控件可见性和可操作性,0初始化，1审核中，2通过，3驳回
        private void kjdatabind()
        {
            //判断控件可用性
            bh = Request.QueryString["bh"].ToString().Trim();
            string sql0 = "select * from OM_SCCZSH_TOTAL where SCCZTOL_BH='" + bh + "'";
            System.Data.DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sql0);
            if (dt0.Rows.Count > 0)
            {
                if (dt0.Rows[0]["SCCZTOL_SHJS"].ToString().Trim() == "1")
                {
                    yjshh.Visible = true;
                    ejshh.Visible = false;
                    sjshh.Visible = false;
                }
                else if (dt0.Rows[0]["SCCZTOL_SHJS"].ToString().Trim() == "2")
                {
                    yjshh.Visible = true;
                    ejshh.Visible = true;
                    sjshh.Visible = false;
                }
                //状态为初始化时
                if (dt0.Rows[0]["SCCZTOL_TOLSTATE"].ToString().Trim() == "0")
                {
                    if (rblSHJS.SelectedValue.ToString().Trim()=="1")
                    {
                        yjshh.Visible = true;
                        ejshh.Visible = false;
                        sjshh.Visible = false;
                    }
                    else if (rblSHJS.SelectedValue.ToString().Trim() == "2")
                    {
                        yjshh.Visible = true;
                        ejshh.Visible = true;
                        sjshh.Visible = false;
                    }
                    else
                    {
                        yjshh.Visible = true;
                        ejshh.Visible = true;
                        sjshh.Visible = true;
                    }
                    if (Session["UserID"].ToString().Trim() == dt0.Rows[0]["SCCZTOL_ZDRID"].ToString().Trim())
                    {
                        btnSave.Visible = true;
                        rblSHJS.Enabled = true;
                        rblfirst.Enabled = false;
                        opinion1.Enabled = false;
                        rblsecond.Enabled = false;
                        opinion2.Enabled = false;
                        rblthird.Enabled = false;
                        opinion3.Enabled = false;
                    }
                    else
                    {
                        btnSave.Visible = false;
                        rblSHJS.Enabled = false;
                        txt_first.Enabled = false;
                        hlSelect1.Visible = false;
                        rblfirst.Enabled = false;
                        opinion1.Enabled = false;
                        txt_second.Enabled = false;
                        hlSelect2.Visible = false;
                        rblsecond.Enabled = false;
                        opinion2.Enabled = false;
                        txt_third.Enabled = false;
                        hlSelect3.Visible = false;
                        rblthird.Enabled = false;
                        opinion3.Enabled = false;

                    }
                }
                //状态为审核中
                if (dt0.Rows[0]["SCCZTOL_TOLSTATE"].ToString().Trim() == "1")
                {
                    //提交
                    if (Session["UserID"].ToString().Trim() == dt0.Rows[0]["SCCZTOL_SHRID1"].ToString().Trim() && dt0.Rows[0]["SCCZTOL_SHRZT1"].ToString().Trim() == "0")
                    {
                        btnSave.Visible = true;
                        rblSHJS.Enabled = false;
                        txt_first.Enabled = false;
                        hlSelect1.Visible = false;
                        txt_second.Enabled = false;
                        hlSelect2.Visible = false;
                        rblsecond.Enabled = false;
                        opinion2.Enabled = false;
                        txt_third.Enabled = false;
                        hlSelect3.Visible = false;
                        rblthird.Enabled = false;
                        opinion3.Enabled = false;
                    }
                    //一级审核通过
                    else if (Session["UserID"].ToString().Trim() == dt0.Rows[0]["SCCZTOL_SHRID2"].ToString().Trim() && dt0.Rows[0]["SCCZTOL_SHRZT1"].ToString().Trim() == "1" && dt0.Rows[0]["SCCZTOL_SHRZT2"].ToString().Trim() == "0")
                    {
                        btnSave.Visible = true;
                        rblSHJS.Enabled = false;
                        txt_first.Enabled = false;
                        hlSelect1.Visible = false;
                        rblfirst.Enabled = false;
                        opinion1.Enabled = false;
                        txt_second.Enabled = false;
                        hlSelect2.Visible = false;
                        txt_third.Enabled = false;
                        hlSelect3.Visible = false;
                        rblthird.Enabled = false;
                        opinion3.Enabled = false;
                    }
                    //二级审核通过
                    else if (Session["UserID"].ToString().Trim() == dt0.Rows[0]["SCCZTOL_SHRID3"].ToString().Trim() && dt0.Rows[0]["SCCZTOL_SHRZT1"].ToString().Trim() == "1" && dt0.Rows[0]["SCCZTOL_SHRZT2"].ToString().Trim() == "1" && dt0.Rows[0]["SCCZTOL_SHRZT3"].ToString().Trim() == "0")
                    {
                        btnSave.Visible = true;
                        rblSHJS.Enabled = false;
                        txt_first.Enabled = false;
                        hlSelect1.Visible = false;
                        rblfirst.Enabled = false;
                        opinion1.Enabled = false;
                        txt_second.Enabled = false;
                        hlSelect2.Visible = false;
                        rblsecond.Enabled = false;
                        opinion2.Enabled = false;
                        txt_third.Enabled = false;
                        hlSelect3.Visible = false;
                    }
                    else
                    {
                        btnSave.Visible = false;
                        rblSHJS.Enabled = false;
                        txt_first.Enabled = false;
                        hlSelect1.Visible = false;
                        rblfirst.Enabled = false;
                        opinion1.Enabled = false;
                        txt_second.Enabled = false;
                        hlSelect2.Visible = false;
                        rblsecond.Enabled = false;
                        opinion2.Enabled = false;
                        txt_third.Enabled = false;
                        hlSelect3.Visible = false;
                        rblthird.Enabled = false;
                        opinion3.Enabled = false;
                    }
                }

                //已通过或驳回
                if (dt0.Rows[0]["SCCZTOL_TOLSTATE"].ToString().Trim() == "2")
                {
                    ImageVerify.Visible = true;
                    btnSave.Visible = false;
                    rblSHJS.Enabled = false;
                    txt_first.Enabled = false;
                    hlSelect1.Visible = false;
                    rblfirst.Enabled = false;
                    opinion1.Enabled = false;
                    txt_second.Enabled = false;
                    hlSelect2.Visible = false;
                    rblsecond.Enabled = false;
                    opinion2.Enabled = false;
                    txt_third.Enabled = false;
                    hlSelect3.Visible = false;
                    rblthird.Enabled = false;
                    opinion3.Enabled = false;
                }
                if (dt0.Rows[0]["SCCZTOL_TOLSTATE"].ToString().Trim() == "3")
                {
                    btnSave.Visible = false;
                    rblSHJS.Enabled = false;
                    txt_first.Enabled = false;
                    hlSelect1.Visible = false;
                    rblfirst.Enabled = false;
                    opinion1.Enabled = false;
                    txt_second.Enabled = false;
                    hlSelect2.Visible = false;
                    rblsecond.Enabled = false;
                    opinion2.Enabled = false;
                    txt_third.Enabled = false;
                    hlSelect3.Visible = false;
                    rblthird.Enabled = false;
                    opinion3.Enabled = false;
                    if (Session["UserID"].ToString().Trim() == dt0.Rows[0]["SCCZTOL_ZDRID"].ToString().Trim())
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


        //审核数据绑定
        private void shenhebind()
        {
            bh = Request.QueryString["bh"].ToString().Trim();
            string sql1 = "select * from OM_SCCZSH_TOTAL where SCCZTOL_BH='" + bh + "'";
            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
            if (dt1.Rows.Count > 0)
            {
                //审核级数绑定
                if (dt1.Rows[0]["SCCZTOL_SHJS"].ToString().Trim() == "1")
                {
                    rblSHJS.SelectedValue = "1";
                    txt_first.Text = dt1.Rows[0]["SCCZTOL_SHRNAME1"].ToString().Trim();
                    firstid.Value = dt1.Rows[0]["SCCZTOL_SHRID1"].ToString().Trim();
                    if (dt1.Rows[0]["SCCZTOL_SHRZT1"].ToString().Trim() == "1")
                    {
                        rblfirst.SelectedValue = "1";
                    }
                    else if (dt1.Rows[0]["SCCZTOL_SHRZT1"].ToString().Trim() == "2")
                    {
                        rblfirst.SelectedValue = "2";
                    }
                    first_time.Text = dt1.Rows[0]["SCCZTOL_SHTIME1"].ToString().Trim();
                    opinion1.Text = dt1.Rows[0]["SCCZTOL_SHRYJ1"].ToString().Trim();
                }
                else if (dt1.Rows[0]["SCCZTOL_SHJS"].ToString().Trim() == "2")
                {
                    rblSHJS.SelectedValue = "2";
                    txt_first.Text = dt1.Rows[0]["SCCZTOL_SHRNAME1"].ToString().Trim();
                    firstid.Value = dt1.Rows[0]["SCCZTOL_SHRID1"].ToString().Trim();
                    if (dt1.Rows[0]["SCCZTOL_SHRZT1"].ToString().Trim() == "1")
                    {
                        rblfirst.SelectedValue = "1";
                    }
                    else if (dt1.Rows[0]["SCCZTOL_SHRZT1"].ToString().Trim() == "2")
                    {
                        rblfirst.SelectedValue = "2";
                    }
                    first_time.Text = dt1.Rows[0]["SCCZTOL_SHTIME1"].ToString().Trim();
                    opinion1.Text = dt1.Rows[0]["SCCZTOL_SHRYJ1"].ToString().Trim();
                    txt_second.Text = dt1.Rows[0]["SCCZTOL_SHRNAME2"].ToString().Trim();
                    secondid.Value = dt1.Rows[0]["SCCZTOL_SHRID2"].ToString().Trim();
                    if (dt1.Rows[0]["SCCZTOL_SHRZT2"].ToString().Trim() == "1")
                    {
                        rblsecond.SelectedValue = "1";
                    }
                    else if (dt1.Rows[0]["SCCZTOL_SHRZT2"].ToString().Trim() == "2")
                    {
                        rblsecond.SelectedValue = "2";
                    }
                    second_time.Text = dt1.Rows[0]["SCCZTOL_SHTIME2"].ToString().Trim();
                    opinion2.Text = dt1.Rows[0]["SCCZTOL_SHRYJ2"].ToString().Trim();
                }
                else if (dt1.Rows[0]["SCCZTOL_SHJS"].ToString().Trim() == "3")
                {
                    rblSHJS.SelectedValue = "3";
                    txt_first.Text = dt1.Rows[0]["SCCZTOL_SHRNAME1"].ToString().Trim();
                    firstid.Value = dt1.Rows[0]["SCCZTOL_SHRID1"].ToString().Trim();
                    if (dt1.Rows[0]["SCCZTOL_SHRZT1"].ToString().Trim() == "1")
                    {
                        rblfirst.SelectedValue = "1";
                    }
                    else if (dt1.Rows[0]["SCCZTOL_SHRZT1"].ToString().Trim() == "2")
                    {
                        rblfirst.SelectedValue = "2";
                    }
                    first_time.Text = dt1.Rows[0]["SCCZTOL_SHTIME1"].ToString().Trim();
                    opinion1.Text = dt1.Rows[0]["SCCZTOL_SHRYJ1"].ToString().Trim();
                    txt_second.Text = dt1.Rows[0]["SCCZTOL_SHRNAME2"].ToString().Trim();
                    secondid.Value = dt1.Rows[0]["SCCZTOL_SHRID2"].ToString().Trim();
                    if (dt1.Rows[0]["SCCZTOL_SHRZT2"].ToString().Trim() == "1")
                    {
                        rblsecond.SelectedValue = "1";
                    }
                    else if (dt1.Rows[0]["SCCZTOL_SHRZT2"].ToString().Trim() == "2")
                    {
                        rblsecond.SelectedValue = "2";
                    }
                    second_time.Text = dt1.Rows[0]["SCCZTOL_SHTIME2"].ToString().Trim();
                    opinion2.Text = dt1.Rows[0]["SCCZTOL_SHRYJ2"].ToString().Trim();
                    txt_third.Text = dt1.Rows[0]["SCCZTOL_SHRNAME3"].ToString().Trim();
                    thirdid.Value = dt1.Rows[0]["SCCZTOL_SHRID3"].ToString().Trim();
                    if (dt1.Rows[0]["SCCZTOL_SHRZT3"].ToString().Trim() == "1")
                    {
                        rblthird.SelectedValue = "1";
                    }
                    else if (dt1.Rows[0]["SCCZTOL_SHRZT3"].ToString().Trim() == "2")
                    {
                        rblthird.SelectedValue = "2";
                    }
                    third_time.Text = dt1.Rows[0]["SCCZTOL_SHTIME3"].ToString().Trim();
                    opinion3.Text = dt1.Rows[0]["SCCZTOL_SHRYJ3"].ToString().Trim();
                }
            }
        }



        protected void btnSave_OnClick(object senser, EventArgs e)
        {
            List<string> list0 = new List<string>();
            bh = Request.QueryString["bh"].ToString().Trim();
            string sqlgetshdata = "select * from OM_SCCZSH_TOTAL where SCCZTOL_BH='" + bh + "'";
            System.Data.DataTable dtgetshdata = DBCallCommon.GetDTUsingSqlText(sqlgetshdata);
            if (dtgetshdata.Rows.Count > 0)
            {
                if (dtgetshdata.Rows[0]["SCCZTOL_TOLSTATE"].ToString().Trim() == "0")
                {
                    string sql0 = "";
                    if (rblSHJS.SelectedValue.ToString().Trim() == "1")
                    {
                        if (txt_first.Text.Trim() == "" || firstid.Value.Trim()=="")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                            return;
                        }
                        sql0 = "update OM_SCCZSH_TOTAL set SCCZTOL_SHRID1='" + firstid.Value.Trim() + "',SCCZTOL_SHRNAME1='" + txt_first.Text.Trim() + "',SCCZTOL_TOLSTATE='1',SCCZTOL_SHJS='" + rblSHJS.SelectedValue.ToString().Trim() + "' where SCCZTOL_BH='" + bh + "'";
                    }
                    if (rblSHJS.SelectedValue.ToString().Trim() == "2")
                    {
                        if (txt_first.Text.Trim() == "" || txt_second.Text.Trim() == "" || firstid.Value.Trim() == "" || secondid.Value.Trim()=="")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                            return;
                        }
                        sql0 = "update OM_SCCZSH_TOTAL set SCCZTOL_SHRID1='" + firstid.Value.Trim() + "',SCCZTOL_SHRNAME1='" + txt_first.Text.Trim() + "',SCCZTOL_SHRID2='" + secondid.Value.Trim() + "',SCCZTOL_SHRNAME2='" + txt_second.Text.Trim() + "',SCCZTOL_TOLSTATE='1',SCCZTOL_SHJS='" + rblSHJS.SelectedValue.ToString().Trim() + "' where SCCZTOL_BH='" + bh + "'";
                    }
                    if (rblSHJS.SelectedValue.ToString().Trim() == "3")
                    {
                        if (txt_first.Text.Trim() == "" || txt_second.Text.Trim() == "" || txt_third.Text.Trim() == "" || firstid.Value.Trim() == "" || secondid.Value.Trim() == "" || thirdid.Value.Trim()=="")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                            return;
                        }
                        sql0 = "update OM_SCCZSH_TOTAL set SCCZTOL_SHRID1='" + firstid.Value.Trim() + "',SCCZTOL_SHRNAME1='" + txt_first.Text.Trim() + "',SCCZTOL_SHRID2='" + secondid.Value.Trim() + "',SCCZTOL_SHRNAME2='" + txt_second.Text.Trim() + "',SCCZTOL_SHRID3='" + thirdid.Value.Trim() + "',SCCZTOL_SHRNAME3='" + txt_third.Text.Trim() + "',SCCZTOL_TOLSTATE='1',SCCZTOL_SHJS='" + rblSHJS.SelectedValue.ToString().Trim() + "' where SCCZTOL_BH='" + bh + "'";
                    }
                    list0.Add(sql0);

                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sprid = firstid.Value.Trim();
                    sptitle = "班组绩效工资审批";
                    string sqlgetnianyue = "select SCCZTOL_NY from OM_SCCZSH_TOTAL where SCCZTOL_BH='" + bh + "'";
                    System.Data.DataTable dtgetnianyue = DBCallCommon.GetDTUsingSqlText(sqlgetnianyue);
                    if (dtgetnianyue.Rows.Count > 0)
                    {
                        spcontent = dtgetnianyue.Rows[0]["SCCZTOL_NY"].ToString().Trim() + "班组绩效工资需要您审批，请登录查看！";
                    }
                    else
                    {
                        spcontent = "有班组绩效工资需要您审批，请登陆查看！";
                    }
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                }
                else if (dtgetshdata.Rows[0]["SCCZTOL_TOLSTATE"].ToString().Trim() == "1")
                {
                    //一级审核人
                    if (dtgetshdata.Rows[0]["SCCZTOL_SHRZT1"].ToString().Trim() == "0")
                    {
                        if (rblfirst.SelectedValue != "1" && rblfirst.SelectedValue != "2")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核结论！');", true);
                            return;
                        }
                        if (dtgetshdata.Rows[0]["SCCZTOL_SHJS"].ToString().Trim() == "1")
                        {
                            string sql1="";
                            if (rblfirst.SelectedValue.ToString().Trim() == "1")
                            {
                                sql1 = "update OM_SCCZSH_TOTAL set SCCZTOL_TOLSTATE='2',SCCZTOL_SHTIME1='"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim()+"',SCCZTOL_SHRYJ1='"+opinion1.Text.Trim()+"',SCCZTOL_SHRZT1='" + rblfirst.SelectedValue.ToString().Trim() + "' where SCCZTOL_BH='" + bh + "'";
                                updateykgw(bh);
                                //updategzqd(bh);
                            }
                            else if (rblfirst.SelectedValue.ToString().Trim() == "2")
                            {
                                sql1 = "update OM_SCCZSH_TOTAL set SCCZTOL_TOLSTATE='3',SCCZTOL_SHTIME1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SCCZTOL_SHRYJ1='" + opinion1.Text.Trim() + "',SCCZTOL_SHRZT1='" + rblfirst.SelectedValue.ToString().Trim() + "' where SCCZTOL_BH='" + bh + "'";
                            }
                            list0.Add(sql1);
                        }
                        else if (dtgetshdata.Rows[0]["SCCZTOL_SHJS"].ToString().Trim() == "2")
                        {
                            string sql2 = "";
                            if (rblfirst.SelectedValue.ToString().Trim() == "1")
                            {
                                sql2 = "update OM_SCCZSH_TOTAL set SCCZTOL_SHTIME1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SCCZTOL_SHRYJ1='" + opinion1.Text.Trim() + "',SCCZTOL_SHRZT1='" + rblfirst.SelectedValue.ToString().Trim() + "' where SCCZTOL_BH='" + bh + "'";

                                //邮件提醒
                                string sprid = "";
                                string sptitle = "";
                                string spcontent = "";
                                sprid = secondid.Value.Trim();
                                sptitle = "班组绩效工资审批";
                                string sqlgetnianyue = "select SCCZTOL_NY from OM_SCCZSH_TOTAL where SCCZTOL_BH='" + bh + "'";
                                System.Data.DataTable dtgetnianyue = DBCallCommon.GetDTUsingSqlText(sqlgetnianyue);
                                if (dtgetnianyue.Rows.Count > 0)
                                {
                                    spcontent = dtgetnianyue.Rows[0]["SCCZTOL_NY"].ToString().Trim() + "班组绩效工资需要您审批，请登录查看！";
                                }
                                else
                                {
                                    spcontent = "有班组绩效工资需要您审批，请登陆查看！";
                                }
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                            }
                            else if (rblfirst.SelectedValue.ToString().Trim() == "2")
                            {
                                sql2 = "update OM_SCCZSH_TOTAL set SCCZTOL_TOLSTATE='3',SCCZTOL_SHTIME1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SCCZTOL_SHRYJ1='" + opinion1.Text.Trim() + "',SCCZTOL_SHRZT1='" + rblfirst.SelectedValue.ToString().Trim() + "' where SCCZTOL_BH='" + bh + "'";
                            }
                            list0.Add(sql2);
                        }
                        else if (dtgetshdata.Rows[0]["SCCZTOL_SHJS"].ToString().Trim() == "3")
                        {
                            string sql3 = "";
                            if (rblfirst.SelectedValue.ToString().Trim() == "1")
                            {
                                sql3 = "update OM_SCCZSH_TOTAL set SCCZTOL_SHTIME1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SCCZTOL_SHRYJ1='" + opinion1.Text.Trim() + "',SCCZTOL_SHRZT1='" + rblfirst.SelectedValue.ToString().Trim() + "' where SCCZTOL_BH='" + bh + "'";

                                //邮件提醒
                                string sprid = "";
                                string sptitle = "";
                                string spcontent = "";
                                sprid = secondid.Value.Trim();
                                sptitle = "班组绩效工资审批";
                                string sqlgetnianyue = "select SCCZTOL_NY from OM_SCCZSH_TOTAL where SCCZTOL_BH='" + bh + "'";
                                System.Data.DataTable dtgetnianyue = DBCallCommon.GetDTUsingSqlText(sqlgetnianyue);
                                if (dtgetnianyue.Rows.Count > 0)
                                {
                                    spcontent = dtgetnianyue.Rows[0]["SCCZTOL_NY"].ToString().Trim() + "班组绩效工资需要您审批，请登录查看！";
                                }
                                else
                                {
                                    spcontent = "有班组绩效工资需要您审批，请登陆查看！";
                                }
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                            }
                            else if (rblfirst.SelectedValue.ToString().Trim() == "2")
                            {
                                sql3 = "update OM_SCCZSH_TOTAL set SCCZTOL_TOLSTATE='3',SCCZTOL_SHTIME1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SCCZTOL_SHRYJ1='" + opinion1.Text.Trim() + "',SCCZTOL_SHRZT1='" + rblfirst.SelectedValue.ToString().Trim() + "' where SCCZTOL_BH='" + bh + "'";
                            }
                            list0.Add(sql3);
                        }
                    }
                    //二级审核人
                    else if (dtgetshdata.Rows[0]["SCCZTOL_SHRZT1"].ToString().Trim() == "1" && dtgetshdata.Rows[0]["SCCZTOL_SHRZT2"].ToString().Trim() == "0")
                    {
                        if (rblsecond.SelectedValue != "1" && rblsecond.SelectedValue != "2")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核结论！');", true);
                            return;
                        }
                        if (dtgetshdata.Rows[0]["SCCZTOL_SHJS"].ToString().Trim() == "2")
                        {
                            string sql4 = "";
                            if (rblsecond.SelectedValue.ToString().Trim() == "1")
                            {
                                sql4 = "update OM_SCCZSH_TOTAL set SCCZTOL_TOLSTATE='2',SCCZTOL_SHTIME2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SCCZTOL_SHRYJ2='" + opinion2.Text.Trim() + "',SCCZTOL_SHRZT2='" + rblsecond.SelectedValue.ToString().Trim() + "' where SCCZTOL_BH='" + bh + "'";
                                updateykgw(bh);
                                //updategzqd(bh);
                            }
                            else if (rblsecond.SelectedValue.ToString().Trim() == "2")
                            {
                                sql4 = "update OM_SCCZSH_TOTAL set SCCZTOL_TOLSTATE='3',SCCZTOL_SHTIME2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SCCZTOL_SHRYJ2='" + opinion2.Text.Trim() + "',SCCZTOL_SHRZT2='" + rblsecond.SelectedValue.ToString().Trim() + "' where SCCZTOL_BH='" + bh + "'";
                            }
                            list0.Add(sql4);
                        }
                        else if (dtgetshdata.Rows[0]["SCCZTOL_SHJS"].ToString().Trim() == "3")
                        {
                            string sql5 = "";
                            if (rblsecond.SelectedValue.ToString().Trim() == "1")
                            {
                                sql5 = "update OM_SCCZSH_TOTAL set SCCZTOL_SHTIME2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SCCZTOL_SHRYJ2='" + opinion2.Text.Trim() + "',SCCZTOL_SHRZT2='" + rblsecond.SelectedValue.ToString().Trim() + "' where SCCZTOL_BH='" + bh + "'";

                                //邮件提醒
                                string sprid = "";
                                string sptitle = "";
                                string spcontent = "";
                                sprid = thirdid.Value.Trim();
                                sptitle = "班组绩效工资审批";
                                string sqlgetnianyue = "select SCCZTOL_NY from OM_SCCZSH_TOTAL where SCCZTOL_BH='" + bh + "'";
                                System.Data.DataTable dtgetnianyue = DBCallCommon.GetDTUsingSqlText(sqlgetnianyue);
                                if (dtgetnianyue.Rows.Count > 0)
                                {
                                    spcontent = dtgetnianyue.Rows[0]["SCCZTOL_NY"].ToString().Trim() + "班组绩效工资需要您审批，请登录查看！";
                                }
                                else
                                {
                                    spcontent = "有班组绩效工资需要您审批，请登陆查看！";
                                }
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                            }
                            else if (rblsecond.SelectedValue.ToString().Trim() == "2")
                            {
                                sql5 = "update OM_SCCZSH_TOTAL set SCCZTOL_TOLSTATE='3',SCCZTOL_SHTIME2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SCCZTOL_SHRYJ2='" + opinion2.Text.Trim() + "',SCCZTOL_SHRZT2='" + rblsecond.SelectedValue.ToString().Trim() + "' where SCCZTOL_BH='" + bh + "'";
                            }
                            list0.Add(sql5);
                        }
                    }

                    //三级审核人
                    else if (dtgetshdata.Rows[0]["SCCZTOL_SHRZT1"].ToString().Trim() == "1" && dtgetshdata.Rows[0]["SCCZTOL_SHRZT2"].ToString().Trim() == "1" && dtgetshdata.Rows[0]["SCCZTOL_SHRZT3"].ToString().Trim() == "0")
                    {
                        if (rblthird.SelectedValue != "1" && rblthird.SelectedValue != "2")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核结论！');", true);
                            return;
                        }
                        if (dtgetshdata.Rows[0]["SCCZTOL_SHJS"].ToString().Trim() == "3")
                        {
                            string sql6 = "";
                            if (rblthird.SelectedValue.ToString().Trim() == "1")
                            {
                                sql6 = "update OM_SCCZSH_TOTAL set SCCZTOL_TOLSTATE='2',SCCZTOL_SHTIME3='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SCCZTOL_SHRYJ3='" + opinion3.Text.Trim() + "',SCCZTOL_SHRZT3='" + rblthird.SelectedValue.ToString().Trim() + "' where SCCZTOL_BH='" + bh + "'";
                                updateykgw(bh);
                                //updategzqd(bh);
                            }
                            else if (rblthird.SelectedValue.ToString().Trim() == "2")
                            {
                                sql6 = "update OM_SCCZSH_TOTAL set SCCZTOL_TOLSTATE='3',SCCZTOL_SHTIME3='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SCCZTOL_SHRYJ3='" + opinion3.Text.Trim() + "',SCCZTOL_SHRZT3='" + rblthird.SelectedValue.ToString().Trim() + "' where SCCZTOL_BH='" + bh + "'";
                            }
                            list0.Add(sql6);
                        }
                    }
                }
            }
            DBCallCommon.ExecuteTrans(list0);
            Response.Redirect("OM_SCCZSP_DETAIL.aspx?bh=" + bh);
        }


        //更新班组长应扣岗位基数
        private void updateykgw(string shbh)
        {
            List<string> list2 = new List<string>();
            string sqlupdateykgw="";
            string sqlgetfzbzz = "select * from OM_SCCZ_FZBZ where FZBZ_BH='" + shbh + "' and FZBZ_STID in(select ST_ID from View_TBDS_STAFFINFO where DEP_POSITION LIKE '%组长%')";
            System.Data.DataTable dtgetfzbz = DBCallCommon.GetDTUsingSqlText(sqlgetfzbzz);
            if (dtgetfzbz.Rows.Count > 0)
            {
                for (int i = 0; i < dtgetfzbz.Rows.Count; i++)
                {
                    sqlupdateykgw = "update OM_SALARYBASEDATA set YKGW_BASEDATANEW=" + CommonFun.ComTryDouble(dtgetfzbz.Rows[i]["FZBZ_JXGZ"].ToString().Trim()) + " where ST_ID='" + dtgetfzbz.Rows[i]["FZBZ_STID"].ToString().Trim() + "'";
                    list2.Add(sqlupdateykgw);
                }
            }
            string sqlgetyixian = "select * from OM_SCCZ_YIXIAN where YIXIAN_BH='" + shbh + "' and YIXIAN_ST_ID in(select ST_ID from View_TBDS_STAFFINFO where DEP_POSITION LIKE '%组长%')";
            System.Data.DataTable dtgetyixian = DBCallCommon.GetDTUsingSqlText(sqlgetyixian);
            if (dtgetyixian.Rows.Count > 0)
            {
                for (int j = 0; j < dtgetyixian.Rows.Count; j++)
                {
                    sqlupdateykgw = "update OM_SALARYBASEDATA set YKGW_BASEDATANEW=" + CommonFun.ComTryDouble(dtgetyixian.Rows[j]["YIXIAN_JXGZ"].ToString().Trim()) + " where ST_ID='" + dtgetyixian.Rows[j]["YIXIAN_ST_ID"].ToString().Trim() + "'";
                    list2.Add(sqlupdateykgw);
                }
            }
            DBCallCommon.ExecuteTrans(list2);
        }

        //private void updategzqd(string bzbh)
        //{
        //    List<string> list1 = new List<string>();
        //    string sqlgetyearmonth = "select * from OM_SCCZSH_TOTAL where SCCZTOL_BH='" + bzbh + "'";
        //    System.Data.DataTable dtgetyearmonth = DBCallCommon.GetDTUsingSqlText(sqlgetyearmonth);
        //    if (dtgetyearmonth.Rows.Count > 0)
        //    {
        //        string sqlstaffinfo = "select * from OM_GZQD where QD_YEARMONTH='" + dtgetyearmonth.Rows[0]["SCCZTOL_NY"].ToString().Trim() + "'";
        //        System.Data.DataTable dtstaffinfo = DBCallCommon.GetDTUsingSqlText(sqlstaffinfo);
        //        if (dtstaffinfo.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dtstaffinfo.Rows.Count; i++)
        //            {
        //                string sqldata0 = "select * from OM_SCCZ_FZBZ where FZBZ_BH='" + bzbh + "' and FZBZ_STID='" + dtstaffinfo.Rows[i]["QD_STID"].ToString().Trim() + "'";
        //                System.Data.DataTable dtdata0 = DBCallCommon.GetDTUsingSqlText(sqldata0);
        //                if (dtdata0.Rows.Count > 0)
        //                {
        //                    string sqlupdate0 = "update OM_GZQD set QD_JXGZ=" + Convert.ToDecimal((CommonFun.ComTryDecimal(dtdata0.Rows[0]["FZBZ_JXGZ"].ToString().Trim())).ToString("0.00")) + ",QD_QTFY=" + Convert.ToDecimal((CommonFun.ComTryDecimal(dtdata0.Rows[0]["FZBZ_QT"].ToString().Trim())).ToString("0.00")) + ",QD_JiaBanGZ=" + Convert.ToDecimal((CommonFun.ComTryDecimal(dtdata0.Rows[0]["FZBZ_JBGZ"].ToString().Trim())).ToString("0.00")) + ",QD_GDGZ=" + Convert.ToDecimal((CommonFun.ComTryDecimal(dtdata0.Rows[0]["FZBZ_GWGZ"].ToString().Trim())).ToString("0.00")) + " where QD_YEARMONTH='" + dtgetyearmonth.Rows[0]["SCCZTOL_NY"].ToString().Trim() + "' and QD_STID='" + dtstaffinfo.Rows[i]["QD_STID"].ToString().Trim() + "'";
        //                    list1.Add(sqlupdate0);
        //                }
        //                else
        //                {
        //                    string sqldata1 = "select * from OM_SCCZ_YIXIAN where YIXIAN_BH='" + bzbh + "' and YIXIAN_ST_ID='" + dtstaffinfo.Rows[i]["QD_STID"].ToString().Trim() + "'";
        //                    System.Data.DataTable dtdata1 = DBCallCommon.GetDTUsingSqlText(sqldata1);
        //                    if (dtdata1.Rows.Count > 0)
        //                    {
        //                        string sqlupdate1 = "update OM_GZQD set QD_JXGZ=" + Convert.ToDecimal((CommonFun.ComTryDecimal(dtdata1.Rows[0]["YIXIAN_JXGZ"].ToString().Trim())).ToString("0.00")) + ",QD_QTFY=" + Convert.ToDecimal((CommonFun.ComTryDecimal(dtdata1.Rows[0]["YIXIAN_QT"].ToString().Trim())).ToString("0.00")) + ",QD_GDGZ=" + Convert.ToDecimal((CommonFun.ComTryDecimal(dtdata1.Rows[0]["YIXIAN_GWGZ"].ToString().Trim())).ToString("0.00")) + " where QD_YEARMONTH='" + dtgetyearmonth.Rows[0]["SCCZTOL_NY"].ToString().Trim() + "' and QD_STID='" + dtstaffinfo.Rows[i]["QD_STID"].ToString().Trim() + "'";
        //                        list1.Add(sqlupdate1);
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //插入人员信息数据
        //            List<string> list000 = new List<string>();
        //            string sql000 = "select * from TBDS_STAFFINFO where ST_PD='0' and ST_NAME!='' and ST_NAME is not null and ST_NAME!='-'";
        //            System.Data.DataTable dt000 = DBCallCommon.GetDTUsingSqlText(sql000);
        //            if (dt000.Rows.Count > 0)
        //            {
        //                for (int j = 0; j < dt000.Rows.Count; j++)
        //                {
        //                    string sqlinsert = "insert into OM_GZQD(QD_YEARMONTH,QD_STID) values('" + dtgetyearmonth.Rows[0]["SCCZTOL_NY"].ToString().Trim() + "','" + dt000.Rows[j]["ST_ID"].ToString().Trim() + "')";
        //                    list000.Add(sqlinsert);
        //                }
        //            }
        //            DBCallCommon.ExecuteTrans(list000);
        //            //重复前面代码
        //            string sqlstaffinfo1 = "select * from OM_GZQD where QD_YEARMONTH='" + dtgetyearmonth.Rows[0]["SCCZTOL_NY"].ToString().Trim() + "'";
        //            System.Data.DataTable dtstaffinfo1 = DBCallCommon.GetDTUsingSqlText(sqlstaffinfo1);
        //            if (dtstaffinfo1.Rows.Count > 0)
        //            {
        //                for (int i = 0; i < dtstaffinfo1.Rows.Count; i++)
        //                {
        //                    string sqldata01 = "select * from OM_SCCZ_FZBZ where FZBZ_BH='" + bzbh + "' and FZBZ_STID='" + dtstaffinfo1.Rows[i]["QD_STID"].ToString().Trim() + "'";
        //                    System.Data.DataTable dtdata01 = DBCallCommon.GetDTUsingSqlText(sqldata01);
        //                    if (dtdata01.Rows.Count > 0)
        //                    {
        //                        string sqlupdate01 = "update OM_GZQD set QD_JXGZ=" + Convert.ToDecimal((CommonFun.ComTryDecimal(dtdata01.Rows[0]["FZBZ_JXGZ"].ToString().Trim())).ToString("0.00")) + ",QD_QTFY=" + Convert.ToDecimal((CommonFun.ComTryDecimal(dtdata01.Rows[0]["FZBZ_QT"].ToString().Trim())).ToString("0.00")) + ",QD_JiaBanGZ=" + Convert.ToDecimal((CommonFun.ComTryDecimal(dtdata01.Rows[0]["FZBZ_JBGZ"].ToString().Trim())).ToString("0.00")) + ",QD_GDGZ=" + Convert.ToDecimal((CommonFun.ComTryDecimal(dtdata01.Rows[0]["FZBZ_GWGZ"].ToString().Trim())).ToString("0.00")) + " where QD_YEARMONTH='" + dtgetyearmonth.Rows[0]["SCCZTOL_NY"].ToString().Trim() + "' and QD_STID='" + dtstaffinfo1.Rows[i]["QD_STID"].ToString().Trim() + "'";
        //                        list1.Add(sqlupdate01);
        //                    }
        //                    else
        //                    {
        //                        string sqldata11 = "select * from OM_SCCZ_YIXIAN where YIXIAN_BH='" + bzbh + "' and YIXIAN_ST_ID='" + dtstaffinfo1.Rows[i]["QD_STID"].ToString().Trim() + "'";
        //                        System.Data.DataTable dtdata11 = DBCallCommon.GetDTUsingSqlText(sqldata11);
        //                        if (dtdata11.Rows.Count > 0)
        //                        {
        //                            string sqlupdate11 = "update OM_GZQD set QD_JXGZ=" + Convert.ToDecimal((CommonFun.ComTryDecimal(dtdata11.Rows[0]["YIXIAN_JXGZ"].ToString().Trim())).ToString("0.00")) + ",QD_QTFY=" + Convert.ToDecimal((CommonFun.ComTryDecimal(dtdata11.Rows[0]["YIXIAN_QT"].ToString().Trim())).ToString("0.00")) + ",QD_GDGZ=" + Convert.ToDecimal((CommonFun.ComTryDecimal(dtdata11.Rows[0]["YIXIAN_GWGZ"].ToString().Trim())).ToString("0.00")) + " where QD_YEARMONTH='" + dtgetyearmonth.Rows[0]["SCCZTOL_NY"].ToString().Trim() + "' and QD_STID='" + dtstaffinfo1.Rows[i]["QD_STID"].ToString().Trim() + "'";
        //                            list1.Add(sqlupdate11);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    DBCallCommon.ExecuteTrans(list1);
        //}


        protected void rblSHJS_OnSelectedIndexChanged(object senser, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid1();
            UCPaging2.CurrentPage = 1;
            bindGrid2();
            kjdatabind();
        }


        protected void rptProNumCost_itemdatabind1(object sender,RepeaterItemEventArgs e)
        {
            bh = Request.QueryString["bh"].ToString().Trim();
            double totaljxgz = 0;
            double totalgwgz = 0;
            double totalqt = 0;
            double totaljbgz = 0;
            double totalxj = 0;
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select cast(sum(FZBZ_JXGZ) as decimal(12,2)) as FZBZ_JXGZhj,cast(sum(FZBZ_GWGZ) as decimal(12,2)) as FZBZ_GWGZhj,cast(sum(FZBZ_QT) as decimal(12,2)) as FZBZ_QThj,cast(sum(FZBZ_JBGZ) as decimal(12,2)) as FZBZ_JBGZhj,cast((sum(FZBZ_JXGZ)+sum(FZBZ_GWGZ)+sum(FZBZ_QT)+sum(FZBZ_JBGZ)) as decimal(12,2)) as FZBZ_XJhj from View_OM_SCCZ_FZBZ where FZBZ_BH='" + bh + "'";

                System.Data.DataTable dthj = DBCallCommon.GetDTUsingSqlText(sqlhj);

                if (dthj.Rows.Count > 0)
                {
                    try
                    {
                        totaljxgz = Convert.ToDouble(dthj.Rows[0]["FZBZ_JXGZhj"].ToString().Trim());
                        totalgwgz = Convert.ToDouble(dthj.Rows[0]["FZBZ_GWGZhj"].ToString().Trim());
                        totalqt = Convert.ToDouble(dthj.Rows[0]["FZBZ_QThj"].ToString().Trim());
                        totaljbgz = Convert.ToDouble(dthj.Rows[0]["FZBZ_JBGZhj"].ToString().Trim());
                        totalxj = Convert.ToDouble(dthj.Rows[0]["FZBZ_XJhj"].ToString().Trim());
                    }
                    catch
                    {
                        totaljxgz = 0;
                        totalgwgz = 0;
                        totalqt = 0;
                        totaljbgz = 0;
                        totalxj = 0;
                    }

                }
                System.Web.UI.WebControls.Label lb_FZBZ_JXGZhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_FZBZ_JXGZhj");
                System.Web.UI.WebControls.Label lb_FZBZ_GWGZhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_FZBZ_GWGZhj");
                System.Web.UI.WebControls.Label lb_FZBZ_QThj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_FZBZ_QThj");
                System.Web.UI.WebControls.Label lb_FZBZ_JBGZhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_FZBZ_JBGZhj");
                System.Web.UI.WebControls.Label lb_FZBZ_XJhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_FZBZ_XJhj");
                lb_FZBZ_JXGZhj.Text = totaljxgz.ToString();
                lb_FZBZ_GWGZhj.Text = totalgwgz.ToString();
                lb_FZBZ_QThj.Text = totalqt.ToString();
                lb_FZBZ_JBGZhj.Text = totaljbgz.ToString();
                lb_FZBZ_XJhj.Text = totalxj.ToString();
            }
        }


        protected void rptProNumCost_itemdatabind2(object sender, RepeaterItemEventArgs e)
        {
            bh = Request.QueryString["bh"].ToString().Trim();
            double totalyxjxgz = 0;
            double totalyxgwgz = 0;
            double totalyxqt = 0;
            double totalyxxj = 0;
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select cast(sum(YIXIAN_JXGZ) as decimal(12,2)) as YIXIAN_JXGZhj,cast(sum(YIXIAN_GWGZ) as decimal(12,2)) as YIXIAN_GWGZhj,cast(sum(YIXIAN_QT) as decimal(12,2)) as YIXIAN_QThj,cast((sum(YIXIAN_JXGZ)+sum(YIXIAN_GWGZ)+sum(YIXIAN_QT)) as decimal(12,2)) as YIXIAN_XJhj from View_OM_SCCZ_YIXIAN where YIXIAN_BH='" + bh + "'";

                System.Data.DataTable dthj = DBCallCommon.GetDTUsingSqlText(sqlhj);

                if (dthj.Rows.Count > 0)
                {
                    try
                    {
                        totalyxjxgz = Convert.ToDouble(dthj.Rows[0]["YIXIAN_JXGZhj"].ToString().Trim());
                        totalyxgwgz = Convert.ToDouble(dthj.Rows[0]["YIXIAN_GWGZhj"].ToString().Trim());
                        totalyxqt = Convert.ToDouble(dthj.Rows[0]["YIXIAN_QThj"].ToString().Trim());
                        totalyxxj = Convert.ToDouble(dthj.Rows[0]["YIXIAN_XJhj"].ToString().Trim());
                    }
                    catch
                    {
                        totalyxjxgz = 0;
                        totalyxgwgz = 0;
                        totalyxqt = 0;
                        totalyxxj = 0;
                    }

                }
                System.Web.UI.WebControls.Label lb_YIXIAN_JXGZhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_YIXIAN_JXGZhj");
                System.Web.UI.WebControls.Label lb_YIXIAN_GWGZhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_YIXIAN_GWGZhj");
                System.Web.UI.WebControls.Label lb_YIXIAN_QThj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_YIXIAN_QThj");
                System.Web.UI.WebControls.Label lb_YIXIAN_XJhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_YIXIAN_XJhj");
                lb_YIXIAN_JXGZhj.Text = totalyxjxgz.ToString();
                lb_YIXIAN_GWGZhj.Text = totalyxgwgz.ToString();
                lb_YIXIAN_QThj.Text = totalyxqt.ToString();
                lb_YIXIAN_XJhj.Text = totalyxxj.ToString();
            }
        }


        #region 批量导出

        protected void btn_plexport_OnClick(object sender, EventArgs e)
        {
            bh = Request.QueryString["bh"].ToString().Trim();
            string sqltext1 = "";
            string sqltext2 = "";
            sqltext1 = "select FZBZ_YEARMONTH,ST_NAME,ST_DEPID1,KQ_CHUQIN,KQ_BINGJ,KQ_SHIJ,KQ_KUANGG,KQ_DAOXIU,KQ_CHANJIA,KQ_PEICHAN,KQ_HUNJIA,KQ_SANGJIA,KQ_GONGS,KQ_NIANX,KQ_ZMJBAN,KQ_JRJIAB,KQ_YSGZ,cast(FZBZ_JXGZ as decimal(12,2)) as FZBZ_JXGZ,cast(FZBZ_GWGZ as decimal(12,2)) as FZBZ_GWGZ,cast(FZBZ_QT as decimal(12,2)) as FZBZ_QT,cast(FZBZ_JBGZ as decimal(12,2)) as FZBZ_JBGZ,cast((isnull(FZBZ_JXGZ,0)+isnull(FZBZ_GWGZ,0)+isnull(FZBZ_QT,0)+isnull(FZBZ_JBGZ,0)) as decimal(12,2)) as FZBZ_XJ from (select a.*,KQ_ST_ID,KQ_DATE,KQ_CHUQIN,KQ_BINGJ,KQ_SHIJ,KQ_KUANGG,KQ_DAOXIU,KQ_CHANJIA,KQ_PEICHAN,KQ_HUNJIA,KQ_SANGJIA,KQ_GONGS,KQ_NIANX,KQ_ZMJBAN,KQ_JRJIAB,KQ_YSGZ from View_OM_SCCZ_FZBZ as a left join OM_KQTJ as b on (a.FZBZ_STID=b.KQ_ST_ID and a.FZBZ_YEARMONTH=b.KQ_DATE))t where FZBZ_BH='" + bh + "' order by ST_DEPID1";
            sqltext2 = "select YIXIAN_YEARMONTH,ST_NAME,ST_DEPID1,cast(YIXIAN_JXGZ as decimal(12,2)) as YIXIAN_JXGZ,cast(YIXIAN_GWGZ as decimal(12,2)) as YIXIAN_GWGZ,cast(YIXIAN_QT as decimal(12,2)) as YIXIAN_QT,cast((isnull(YIXIAN_JXGZ,0)+isnull(YIXIAN_GWGZ,0)+isnull(YIXIAN_QT,0)) as decimal(12,2)) as YIXIAN_XJ from View_OM_SCCZ_YIXIAN where YIXIAN_BH='" + bh + "' order by ST_DEPID1";
            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext1);
            System.Data.DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext2);
            string filename = "操作岗位" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("操作岗位.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);//创建第一个sheet
                ISheet sheet2 = wk.GetSheetAt(1);//创建第二个sheet
                //辅助班组
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 2);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int t = 0; t < dt1.Columns.Count; t++)
                    {
                        row.CreateCell(t + 1).SetCellValue(dt1.Rows[i][t].ToString());
                    }

                }
                for (int m = 0; m <= dt1.Columns.Count; m++)
                {
                    sheet1.AutoSizeColumn(m);
                }

                //一线
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    IRow row = sheet2.CreateRow(j + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(j + 1);
                    for (int k = 0; k < dt2.Columns.Count; k++)
                    {
                        row.CreateCell(k + 1).SetCellValue(dt2.Rows[j][k].ToString());
                    }

                }
                for (int n = 0; n <= dt2.Columns.Count; n++)
                {
                    sheet2.AutoSizeColumn(n);
                }

                sheet1.ForceFormulaRecalculation = true;
                sheet2.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        #endregion


        //反审
        protected void btnfanshen_OnClick(object sender, EventArgs e)
        {
            List<string> list0 = new List<string>();
            bh = Request.QueryString["bh"].ToString().Trim();
            string sqltext = "update OM_SCCZSH_TOTAL set SCCZTOL_TOLSTATE='0',SCCZTOL_SHRZT1='0',SCCZTOL_SHRZT2='0',SCCZTOL_SHRZT3='0' where SCCZTOL_BH='" + bh + "'";
            list0.Add(sqltext);
            DBCallCommon.ExecuteTrans(list0);
            Response.Redirect("OM_SCCZSP_DETAIL.aspx?bh=" + bh);
        }
    }
}
