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
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.FM_Data
{
    public partial class CB_MothSummary : BasicPage
    {

        //double gzhj = 0;
        double jjfyhj = 0;
        double jgyzfyhj = 0;
        double zjrghj = 0;


        double bzjhj = 0;
        double hclhj = 0;
        double hsjshj = 0;
        double zjhj = 0;
        double djhj = 0;
        double zchj = 0;
        double wgjhj = 0;
        double yqtlhj = 0;
        double qtclhj = 0;
        double clhj = 0;


        double gdzzfyhj = 0;
        double kbzzfyhj = 0;
        double zzfyhj = 0;

        double wxfyhj = 0;
        double cnfbhj = 0;
        double yfhj = 0;
        double fjcbhj = 0;
        double qthj = 0;


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);//查询下拉控件绑定

                this.BindYearMoth(ddlstartcx1, ddlstartcx2);

                this.BindYearMoth(ddlendcx1, ddlendcx2);//跨期间查询年月绑定
                this.InitPage();//初始化页面
                ViewState["sqltext"] = "AYTJ_YEARMONTH='" + dplYear.SelectedValue.ToString() + "-" + dplMoth.SelectedValue.ToString() + "'";




                string sql = "select * from TBFM_HSTOTAL where HS_STATE=2 and HS_YEAR='" + dplYear.SelectedValue.ToString() + "'and HS_MONTH='" + dplMoth.SelectedValue.ToString() + "'";//存货核算中的出库核算
                DataTable dt_HS = DBCallCommon.GetDTUsingSqlText(sql);//在财务核算总表中查找数据
                if (dt_HS.Rows.Count > 0)
                {
                    LabelDate.Text = dt_HS.Rows[0]["HS_DATE"].ToString();//HS_DATE具体到了时间
                    btnCurrentMonth.Enabled = true;
                    btnCurrentMonth.BackColor = System.Drawing.Color.White;
                    this.CheckCurrentExist();//判断当月是否已生成
                }//当月已核算
                else
                {
                    btnCurrentMonth.Enabled = false;//只有当每月核算结束后该按钮才可用，计算材料费
                    btnCurrentMonth.BackColor = System.Drawing.Color.Gray;
                }
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }
            if (IsPostBack)
            {
                this.InitVar();
            }
        }


        // 初始化页面
        private void InitPage()
        {
            txtrwh.Text = "";
            //刚开始加载页面时显示当月
            dplYear.ClearSelection();
            foreach (ListItem li in dplYear.Items)
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }
            dplMoth.ClearSelection();
            string month = (DateTime.Now.Month).ToString();
            if (DateTime.Now.Month < 10)
            {
                month = "0" + month;
            }
            foreach (ListItem li in dplMoth.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
        }

        #region
        PagerQueryParam pager_org = new PagerQueryParam();

        // 初始化分布信息

        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }


        // 分页初始化

        private void InitPager()
        {
            pager_org.TableName = "(select * from VIEW_FM_AYTJ as a left join (select sum(cast(DIF_DIFMONEY as decimal(12,2))) as DIF_DIFMONEY,DIF_TSAID,DIF_YEAR,DIF_MONTH from TBFM_DIF group by DIF_TSAID,DIF_YEAR,DIF_MONTH)b on (a.PMS_TSAID=b.DIF_TSAID and a.AYTJ_YEARMONTH=b.DIF_YEAR+'-'+b.DIF_MONTH) left join (select sum(cast(DIFYF_DIFMONEY as decimal(12,2))) as DIFYF_DIFMONEY,DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF group by DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH)c on (a.PMS_TSAID=c.DIFYF_TSAID and a.AYTJ_YEARMONTH=c.DIFYF_YEAR+'-'+c.DIFYF_MONTH))t";//按月统计视图
            pager_org.PrimaryKey = "PMS_TSAID";
            pager_org.ShowFields = "PMS_TSAID,AYTJ_YEARMONTH,TSA_PJID,CM_PROJ,cast(ISNULL(AYTJ_JJFY,0) as decimal(12,2)) as AYTJ_JJFY,cast(ISNULL(AYTJ_JGYZFY,0) as decimal(12,2)) as AYTJ_JGYZFY,cast((ISNULL(AYTJ_JJFY,0)+ISNULL(AYTJ_JGYZFY,0)) as decimal(12,2)) as AYTJ_ZJRGFXJ,ISNULL(PMS_01_01,0) as BZJ,ISNULL(PMS_01_05,0) as HCL,ISNULL(PMS_01_07,0) as HSJS,ISNULL(PMS_01_08,0) as ZJ,ISNULL(PMS_01_09,0) as DJ,ISNULL(PMS_01_10,0) as ZC,ISNULL(PMS_01_11,0) as WGJ,ISNULL(PMS_01_15,0) as YQTL,(PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_06+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) as QTCL,(PMS_01_01+PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_05+PMS_01_06+PMS_01_07+PMS_01_08+PMS_01_09+PMS_01_10+PMS_01_11+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_15+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) as CLXJ,cast(ISNULL(AYTJ_GDZZFY,0) as decimal(12,2)) as AYTJ_GDZZFY,cast(ISNULL(AYTJ_KBZZFY,0) as decimal(12,2)) as AYTJ_KBZZFY,cast(((ISNULL(AYTJ_KBZZFY,0))+(ISNULL(AYTJ_GDZZFY,0))) as decimal(12,2)) as ZZFYXJ,isnull(cast((isnull(AYTJ_WXFY,0)+isnull(DIF_DIFMONEY,0)) as decimal(12,2)),0) as AYTJ_WXFY,cast(ISNULL(AYTJ_CNFB,0) as decimal(12,2)) as AYTJ_CNFB,isnull((isnull(AYTJ_YF,0)+isnull(DIFYF_DIFMONEY,0)),0) as AYTJ_YF,ISNULL(AYTJ_FJCB,0) as AYTJ_FJCB,ISNULL(AYTJ_QT,0) as AYTJ_QT";//,ISNULL(AYTJ_GZ,0) as AYTJ_GZ
            pager_org.OrderField = "PMS_TSAID";
            pager_org.StrWhere = ViewState["sqltext"].ToString();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 20;
        }


        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_org);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
            CheckUser(ControlFinder);
        }
        #endregion
        // 绑定年月
        private void BindYearMoth(DropDownList dpl_Year, DropDownList dpl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                dpl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            dpl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// 年月份改变查询begin
        /// </summary>
        protected void OnSelectedIndexChanged_dplYearMoth(object sender, EventArgs e)
        {
            txtrwh.Text = "";//任务号输入文本框设置为空
            rptProNumCost.DataSource = null;
            rptProNumCost.DataBind();//清空repeater控件的数据绑定
            if (dplYear.SelectedIndex == 0 || dplMoth.SelectedIndex == 0)
            {
                palNoData.Visible = true;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！！！');", true);
            }//若年月份均未选择，提示错误！
            else
            {
                string sql = "select * from TBFM_HSTOTAL where HS_STATE=2 and HS_YEAR='" + dplYear.SelectedValue.ToString() + "'and HS_MONTH='" + dplMoth.SelectedValue.ToString() + "'";//存货核算中的出库核算
                DataTable dt_HS = DBCallCommon.GetDTUsingSqlText(sql);//在财务核算总表中查找数据
                if (dt_HS.Rows.Count > 0)
                {
                    LabelDate.Text = dt_HS.Rows[0]["HS_DATE"].ToString();//HS_DATE具体到了时间
                    btnCurrentMonth.Enabled = true;
                    btnCurrentMonth.BackColor = System.Drawing.Color.White;
                    this.CheckCurrentExist();//判断当月是否已生成
                }//当月已核算
                else
                {
                    LabelDate.Text = "";
                    btnCurrentMonth.Enabled = false;
                    btnCurrentMonth.BackColor = System.Drawing.Color.Gray;
                }//当月未核算
                ViewState["sqltext"] = "AYTJ_YEARMONTH='" + dplYear.SelectedValue.ToString() + "-" + dplMoth.SelectedValue.ToString() + "'";
                this.InitVar();
                this.bindGrid();
            }
        }
        /// <summary>
        /// 年月份改变查询end
        /// </summary>

        /// <summary>
        /// 任务号查询begin
        /// </summary>    
        protected void btnCx_OnClick(object sender, EventArgs e)
        {
            rptProNumCost.DataSource = null;
            rptProNumCost.DataBind();
            dplYear.SelectedIndex = 0;
            dplMoth.SelectedIndex = 0;
            if (txtrwh.Text == "请输入生产制号！")//任务号
            {
                ViewState["sqltext"] = "PMS_TSAID like '%'";
            }
            if (txthth.Text == "请输入合同号！")//合同号
            {
                ViewState["sqltext"] = "TSA_PJID like '%'";
            }
            if (txtxmmc.Text == "请输入项目名称！")//项目名称
            {
                ViewState["sqltext"] = "CM_PROJ like '%'";
            }
            else
            {
                ViewState["sqltext"] = "PMS_TSAID like '%" + txtrwh.Text.Trim() + "%' and TSA_PJID like '%" + txthth.Text.Trim() + "%'and CM_PROJ like '%" + txtxmmc.Text.Trim() + "%'";
            }

            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }
        /// <summary>
        /// 任务号查询end
        /// </summary>





        protected void btnCurrentMonth_onClick(object sender, EventArgs e)//只有当该月未生成才可操作
        {
            List<string> list_sql = new List<string>();
            List<string> list_sqlcl = new List<string>();
            list_sql.Clear();
            string YEAR = dplYear.SelectedValue.ToString();
            int nian = Convert.ToInt32(YEAR.ToString());
            string MONTH = dplMoth.SelectedValue.ToString();
            int yue = Convert.ToInt32(MONTH.ToString());
            string YEARMONTH = YEAR + "-" + MONTH;
            //代码块1


            //如果当月已生成，更新按月统计表和生产制号月汇总表中的数据
            if (btnCurrentMonth.CommandName == "update")
            {
                #region
                string sqlrwhupdate = "select PMS_TSAID from TBCB_PRD_MAT_SUMMARY_MONTH where  PMS_YEAR='" + YEAR + "'and PMS_MONTH='" + MONTH + "'";
                DataTable dtud = DBCallCommon.GetDTUsingSqlText(sqlrwhupdate);//需要更新的数据
                if (dtud.Rows.Count > 0)
                {
                    string sqldelete1 = "delete from TBCB_PRD_MAT_SUMMARY_MONTH where PMS_YEAR='" + YEAR + "'and PMS_MONTH='" + MONTH + "'";
                    string sqldelete2 = "delete from TBFM_AYTJ where AYTJ_YEARMONTH='" + YEAR + "-" + MONTH + "'";
                    DBCallCommon.ExeSqlText(sqldelete1);
                    DBCallCommon.ExeSqlText(sqldelete2);

                    //代码块2
                    string sql_rwhadd = "select TSA_ID from (select TSA_ID from TBPM_TCTSASSGN union select TSA_ID from TBPM_DETAIL)t where TSA_ID not in(select PMS_TSAID from TBCB_PRD_MAT_SUMMARY_MONTH where PMS_YEAR='" + YEAR + "' and PMS_MONTH='" + MONTH + "') and (TSA_ID in(select OP_TSAID from TBWS_OUT where OP_VERIFYDATE like '" + YEARMONTH + "%') or TSA_ID in(select CB_TSAID from CB_FT_JGYZJJ where CB_YEAR='" + YEAR + "' and CB_MONTH='" + MONTH + "') or TSA_ID in(select CB_ZZ_TSAID from CB_FT_ZZFY where CB_ZZ_YEAR='" + YEAR + "' and CB_ZZ_MONTH='" + MONTH + "') or TSA_ID in(select TAHZ_TSAID from TBFM_WXHZ where TAHZ_YEARMONTH='" + YEARMONTH + "') or TSA_ID in(select DIFYF_TSAID from TBFM_YFDIF where DIFYF_YEAR='" + YEAR + "' and DIFYF_MONTH='" + MONTH + "') or TSA_ID in(select DIF_TSAID from TBFM_DIF where DIF_YEAR='" + YEAR + "' and DIF_MONTH='" + MONTH + "') or TSA_ID in(select CNFB_TSAID from TBMP_CNFB_LIST where CNFB_YEAR='" + YEAR + "' and CNFB_MONTH='" + MONTH + "') or TSA_ID in(select YFHZ_TSAID from TBFM_YFHZ where YFHZ_YEARMONTH='" + YEARMONTH + "') or TSA_ID in(select FJCB_TSAID from FM_FJCB where FJCB_YEAR='" + YEAR + "' and FJCB_MONTH='" + MONTH + "')) and (TSA_ID in(select TSA_ID from TBPM_TCTSASSGN where TSA_STATE in('1','2','3') and TSA_PJID not like '%JSB.BOM%' and TSA_PJID not like '%GONGZHUANG%'))";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_rwhadd);
                    if (dt.Rows.Count > 0)
                    {
                        //代码块3
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string rwh = dt.Rows[i]["TSA_ID"].ToString();
                            //向生产制造材料月汇总表中插入数据
                            list_sql.Add("insert into TBCB_PRD_MAT_SUMMARY_MONTH(PMS_TSAID,PMS_YEAR,PMS_MONTH) Values('" + rwh + "','" + YEAR + "','" + MONTH + "')");
                            //向按月统计表中插入数据
                            list_sql.Add("insert into TBFM_AYTJ(AYTJ_TSAID,AYTJ_YEARMONTH) Values('" + rwh + "','" + YEARMONTH + "')");
                            //代码块4
                            string sqladd = update_clf(rwh);//材料月统计中的数据插入
                            list_sqlcl.Add(sqladd);
                        }
                        DBCallCommon.ExecuteTrans(list_sql);
                        DBCallCommon.ExecuteTrans(list_sqlcl);
                        fyupdate();
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('本月材料费统计完毕!');", true);
                        InitVar();
                        bindGrid();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('本月没有统计数据！！！');", true);
                    InitVar();
                    bindGrid();
                }
                this.CheckCurrentExist();
                #endregion
            }




            //如果当月未生成，向按月统计表和生产制号月汇总表中插入数据
            else if (btnCurrentMonth.CommandName == "insert")
            {
                //代码块2
                string sql_rwhadd = "select TSA_ID from (select TSA_ID from TBPM_TCTSASSGN union select TSA_ID from TBPM_DETAIL)t where TSA_ID not in(select PMS_TSAID from TBCB_PRD_MAT_SUMMARY_MONTH where PMS_YEAR='" + YEAR + "' and PMS_MONTH='" + MONTH + "') and (TSA_ID in(select OP_TSAID from TBWS_OUT where OP_VERIFYDATE like '" + YEARMONTH + "%') or TSA_ID in(select CB_TSAID from CB_FT_JGYZJJ where CB_YEAR='" + YEAR + "' and CB_MONTH='" + MONTH + "') or TSA_ID in(select CB_ZZ_TSAID from CB_FT_ZZFY where CB_ZZ_YEAR='" + YEAR + "' and CB_ZZ_MONTH='" + MONTH + "') or TSA_ID in(select TAHZ_TSAID from TBFM_WXHZ where TAHZ_YEARMONTH='" + YEARMONTH + "') or TSA_ID in(select DIFYF_TSAID from TBFM_YFDIF where DIFYF_YEAR='" + YEAR + "' and DIFYF_MONTH='" + MONTH + "') or TSA_ID in(select DIF_TSAID from TBFM_DIF where DIF_YEAR='" + YEAR + "' and DIF_MONTH='" + MONTH + "') or TSA_ID in(select CNFB_TSAID from TBMP_CNFB_LIST where CNFB_YEAR='" + YEAR + "' and CNFB_MONTH='" + MONTH + "') or TSA_ID in(select YFHZ_TSAID from TBFM_YFHZ where YFHZ_YEARMONTH='" + YEARMONTH + "') or TSA_ID in(select FJCB_TSAID from FM_FJCB where FJCB_YEAR='" + YEAR + "' and FJCB_MONTH='" + MONTH + "')) and (TSA_ID in(select TSA_ID from TBPM_TCTSASSGN where TSA_STATE in('1','2','3') and TSA_PJID not like '%JSB.BOM%' and TSA_PJID not like '%GONGZHUANG%'))";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_rwhadd);
                if (dt.Rows.Count > 0)
                {
                    //代码块3
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string rwh = dt.Rows[i]["TSA_ID"].ToString();
                        //向生产制造材料月汇总表中插入数据
                        list_sql.Add("insert into TBCB_PRD_MAT_SUMMARY_MONTH(PMS_TSAID,PMS_YEAR,PMS_MONTH) Values('" + rwh + "','" + YEAR + "','" + MONTH + "')");
                        //向按月统计表中插入数据
                        list_sql.Add("insert into TBFM_AYTJ (AYTJ_TSAID,AYTJ_YEARMONTH) Values('" + rwh + "','" + YEARMONTH + "')");
                        //代码块4
                        string sqladd = update_clf(rwh);//材料月统计中的数据插入
                        list_sqlcl.Add(sqladd);
                    }
                    DBCallCommon.ExecuteTrans(list_sql);
                    DBCallCommon.ExecuteTrans(list_sqlcl);
                    fyupdate();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('本月材料费统计完毕!');", true);
                    InitVar();
                    bindGrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('本月暂无要统计的生产制号！！！');", true);
                    InitVar();
                    bindGrid();
                }
                this.CheckCurrentExist();
            }

        }


        //除材料费外的其他费用更新
        private void fyupdate()
        {
            List<string> list = new List<string>();
            string YEAR = dplYear.SelectedValue.ToString();
            string MONTH = dplMoth.SelectedValue.ToString();
            string YEARMONTH = dplYear.SelectedValue.ToString() + "-" + dplMoth.SelectedValue.ToString();
            //2017.04.10还原
            string sqlsjy = "";
            if ((YEAR == "2016" && MONTH == "12") || (YEAR == "2017" && MONTH == "01") || (YEAR == "2017" && MONTH == "02") || (YEAR == "2017" && MONTH == "03"))
            {
                sqlsjy = "select AYTJ_TSAID,isnull(RWHFJCB_HJBHSJE,0) as RWHFJCB_HJBHSJE,isnull(WX_MONEY,0) as WX_MONEY,isnull(DIF_MONEY,0) as DIF_MONEY,isnull(CNFB_MONEY,0) as CNFB_MONEY,isnull(CB_CNJJ,0) as CB_CNJJ,isnull(CB_JGYZFT,0) as CB_JGYZFT,isnull(CB_ZZ_GDZZFY,0) as CB_ZZ_GDZZFY,isnull(CB_ZZ_KBZZFY,0) as CB_ZZ_KBZZFY,isnull(DIFYF_MONEY,0) as DIFYF_MONEY,isnull(YF_MONEY,0) as YF_MONEY from (select * from (select * from TBFM_AYTJ where AYTJ_YEARMONTH='" + YEARMONTH + "') as a left join (select isnull(sum(isnull(FJCB_HJBHSJE,0)),0) as RWHFJCB_HJBHSJE,FJCB_TSAID from FM_FJCB where FJCB_YEAR='" + YEAR + "' and FJCB_MONTH='" + MONTH + "' group by FJCB_TSAID) as b on a.AYTJ_TSAID=b.FJCB_TSAID left join (select TAHZ_TSAID,isnull(sum(isnull(DIF_DIFMONEY,0)),0) as DIF_MONEY,isnull(sum(isnull(TAHZ_MONEY,0)),0) as WX_MONEY from (select * from ((select TAHZ_TSAID,TAHZ_PTC,TAHZ_MONEY,TAHZ_YEARMONTH from TBFM_WXHZ where TAHZ_YEARMONTH='" + YEAR + "-" + MONTH + "')a left join (select DIF_JHGZH,DIF_DIFMONEY,DIF_YEAR,DIF_MONTH from TBFM_DIF where DIF_YEAR='" + YEAR + "' and DIF_MONTH='" + MONTH + "')b on a.TAHZ_PTC=b.DIF_JHGZH))c group by TAHZ_TSAID) as c on a.AYTJ_TSAID=c.TAHZ_TSAID left join (select isnull(sum(isnull(CNFB_BYREALMONEY,0)),0) as CNFB_MONEY,CNFB_TSAID from TBMP_CNFB_LIST where CNFB_YEAR='" + YEAR + "'and CNFB_MONTH='" + MONTH + "' and CNFB_TYPE not like '%N%' and CNFB_TYPE not like '%喷漆喷砂%' group by CNFB_TSAID) as d on a.AYTJ_TSAID=d.CNFB_TSAID left join (select CB_TSAID,isnull(CB_CNJJ,0) as CB_CNJJ,isnull(CB_JGYZFT,0) as CB_JGYZFT from CB_FT_JGYZJJ where CB_YEAR='" + YEAR + "'and CB_MONTH='" + MONTH + "') as e on a.AYTJ_TSAID=e.CB_TSAID left join (select CB_ZZ_TSAID,isnull(CB_ZZ_GDZZFY,0) as CB_ZZ_GDZZFY,isnull(CB_ZZ_KBZZFY,0) as CB_ZZ_KBZZFY from CB_FT_ZZFY where CB_ZZ_YEAR='" + YEAR + "'and CB_ZZ_MONTH='" + MONTH + "') as f on a.AYTJ_TSAID=f.CB_ZZ_TSAID left join (select YFHZ_TSAID,isnull(sum(isnull(DIFYF_DIFMONEY,0)),0) as DIFYF_MONEY,isnull(sum(isnull(YFHZ_MONEY,0)),0) as YF_MONEY from (select * from ((select YFHZ_TSAID,YFHZ_PTC,YFHZ_MONEY,YFHZ_YEARMONTH from TBFM_YFHZ  where YFHZ_YEARMONTH='" + YEAR + "-" + MONTH + "')a left join (select DIFYF_JHGZH,DIFYF_DIFMONEY,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF where DIFYF_YEAR='" + YEAR + "' and DIFYF_MONTH='" + MONTH + "')b on a.YFHZ_PTC=b.DIFYF_JHGZH))c group by YFHZ_TSAID) as g on a.AYTJ_TSAID=g.YFHZ_TSAID)t";
            }
            else
            {
                sqlsjy = "select AYTJ_TSAID,isnull(RWHFJCB_HJBHSJE,0) as RWHFJCB_HJBHSJE,isnull(WX_MONEY,0) as WX_MONEY,isnull(DIF_MONEY,0) as DIF_MONEY,isnull(CNFB_MONEY,0) as CNFB_MONEY,isnull(CB_CNJJ,0) as CB_CNJJ,isnull(CB_JGYZFT,0) as CB_JGYZFT,isnull(CB_ZZ_GDZZFY,0) as CB_ZZ_GDZZFY,isnull(CB_ZZ_KBZZFY,0) as CB_ZZ_KBZZFY,isnull(DIFYF_MONEY,0) as DIFYF_MONEY,isnull(YF_MONEY,0) as YF_MONEY from (select * from (select * from TBFM_AYTJ where AYTJ_YEARMONTH='" + YEARMONTH + "') as a left join (select isnull(sum(isnull(FJCB_HJBHSJE,0)),0) as RWHFJCB_HJBHSJE,FJCB_TSAID from FM_FJCB where FJCB_YEAR='" + YEAR + "' and FJCB_MONTH='" + MONTH + "' group by FJCB_TSAID) as b on a.AYTJ_TSAID=b.FJCB_TSAID left join (select TAHZ_TSAID,isnull(sum(isnull(DIF_DIFMONEY,0)),0) as DIF_MONEY,isnull(sum(isnull(TAHZ_MONEY,0)),0) as WX_MONEY from (select * from ((select TAHZ_TSAID,TAHZ_PTC,TAHZ_MONEY,TAHZ_YEARMONTH from TBFM_WXHZ where TAHZ_YEARMONTH='" + YEAR + "-" + MONTH + "')a left join (select DIF_JHGZH,DIF_DIFMONEY,DIF_YEAR,DIF_MONTH from TBFM_DIF where DIF_YEAR='" + YEAR + "' and DIF_MONTH='" + MONTH + "')b on a.TAHZ_PTC=b.DIF_JHGZH))c group by TAHZ_TSAID) as c on a.AYTJ_TSAID=c.TAHZ_TSAID left join (select isnull(sum(isnull(CNFB_BYREALMONEY,0)),0) as CNFB_MONEY,CNFB_TSAID from TBMP_CNFB_LIST where CNFB_YEAR='" + YEAR + "'and CNFB_MONTH='" + MONTH + "' and CNFB_TYPE not like '%N%' group by CNFB_TSAID) as d on a.AYTJ_TSAID=d.CNFB_TSAID left join (select CB_TSAID,isnull(CB_CNJJ,0) as CB_CNJJ,isnull(CB_JGYZFT,0) as CB_JGYZFT from CB_FT_JGYZJJ where CB_YEAR='" + YEAR + "'and CB_MONTH='" + MONTH + "') as e on a.AYTJ_TSAID=e.CB_TSAID left join (select CB_ZZ_TSAID,isnull(CB_ZZ_GDZZFY,0) as CB_ZZ_GDZZFY,isnull(CB_ZZ_KBZZFY,0) as CB_ZZ_KBZZFY from CB_FT_ZZFY where CB_ZZ_YEAR='" + YEAR + "'and CB_ZZ_MONTH='" + MONTH + "') as f on a.AYTJ_TSAID=f.CB_ZZ_TSAID left join (select YFHZ_TSAID,isnull(sum(isnull(DIFYF_DIFMONEY,0)),0) as DIFYF_MONEY,isnull(sum(isnull(YFHZ_MONEY,0)),0) as YF_MONEY from (select * from ((select YFHZ_TSAID,YFHZ_PTC,YFHZ_MONEY,YFHZ_YEARMONTH from TBFM_YFHZ  where YFHZ_YEARMONTH='" + YEAR + "-" + MONTH + "')a left join (select DIFYF_JHGZH,DIFYF_DIFMONEY,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF where DIFYF_YEAR='" + YEAR + "' and DIFYF_MONTH='" + MONTH + "')b on a.YFHZ_PTC=b.DIFYF_JHGZH))c group by YFHZ_TSAID) as g on a.AYTJ_TSAID=g.YFHZ_TSAID)t";
            }
            //2016.12.27修改，将喷漆喷砂包含在内
            //string sqlsjy = "";
            //if ((YEAR == "2016" && MONTH == "12") || (string.Compare(YEAR ,"2016")>0))
            //{
            //     sqlsjy = "select AYTJ_TSAID,isnull(RWHFJCB_HJBHSJE,0) as RWHFJCB_HJBHSJE,isnull(WX_MONEY,0) as WX_MONEY,isnull(DIF_MONEY,0) as DIF_MONEY,isnull(CNFB_MONEY,0) as CNFB_MONEY,isnull(CB_CNJJ,0) as CB_CNJJ,isnull(CB_JGYZFT,0) as CB_JGYZFT,isnull(CB_ZZ_GDZZFY,0) as CB_ZZ_GDZZFY,isnull(CB_ZZ_KBZZFY,0) as CB_ZZ_KBZZFY,isnull(DIFYF_MONEY,0) as DIFYF_MONEY,isnull(YF_MONEY,0) as YF_MONEY from (select * from (select * from TBFM_AYTJ where AYTJ_YEARMONTH='" + YEARMONTH + "') as a left join (select isnull(sum(isnull(FJCB_HJBHSJE,0)),0) as RWHFJCB_HJBHSJE,FJCB_TSAID from FM_FJCB where FJCB_YEAR='" + YEAR + "' and FJCB_MONTH='" + MONTH + "' group by FJCB_TSAID) as b on a.AYTJ_TSAID=b.FJCB_TSAID left join (select TAHZ_TSAID,isnull(sum(isnull(DIF_DIFMONEY,0)),0) as DIF_MONEY,isnull(sum(isnull(TAHZ_MONEY,0)),0) as WX_MONEY from (select * from ((select TAHZ_TSAID,TAHZ_PTC,TAHZ_MONEY,TAHZ_YEARMONTH from TBFM_WXHZ where TAHZ_YEARMONTH='" + YEAR + "-" + MONTH + "')a left join (select DIF_JHGZH,DIF_DIFMONEY,DIF_YEAR,DIF_MONTH from TBFM_DIF where DIF_YEAR='" + YEAR + "' and DIF_MONTH='" + MONTH + "')b on a.TAHZ_PTC=b.DIF_JHGZH))c group by TAHZ_TSAID) as c on a.AYTJ_TSAID=c.TAHZ_TSAID left join (select isnull(sum(isnull(CNFB_BYREALMONEY,0)),0) as CNFB_MONEY,CNFB_TSAID from TBMP_CNFB_LIST where CNFB_YEAR='" + YEAR + "'and CNFB_MONTH='" + MONTH + "' and CNFB_TYPE not like '%N%' and CNFB_TYPE not like '%喷漆喷砂%' group by CNFB_TSAID) as d on a.AYTJ_TSAID=d.CNFB_TSAID left join (select CB_TSAID,isnull(CB_CNJJ,0) as CB_CNJJ,isnull(CB_JGYZFT,0) as CB_JGYZFT from CB_FT_JGYZJJ where CB_YEAR='" + YEAR + "'and CB_MONTH='" + MONTH + "') as e on a.AYTJ_TSAID=e.CB_TSAID left join (select CB_ZZ_TSAID,isnull(CB_ZZ_GDZZFY,0) as CB_ZZ_GDZZFY,isnull(CB_ZZ_KBZZFY,0) as CB_ZZ_KBZZFY from CB_FT_ZZFY where CB_ZZ_YEAR='" + YEAR + "'and CB_ZZ_MONTH='" + MONTH + "') as f on a.AYTJ_TSAID=f.CB_ZZ_TSAID left join (select YFHZ_TSAID,isnull(sum(isnull(DIFYF_DIFMONEY,0)),0) as DIFYF_MONEY,isnull(sum(isnull(YFHZ_MONEY,0)),0) as YF_MONEY from (select * from ((select YFHZ_TSAID,YFHZ_PTC,YFHZ_MONEY,YFHZ_YEARMONTH from TBFM_YFHZ  where YFHZ_YEARMONTH='" + YEAR + "-" + MONTH + "')a left join (select DIFYF_JHGZH,DIFYF_DIFMONEY,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF where DIFYF_YEAR='" + YEAR + "' and DIFYF_MONTH='" + MONTH + "')b on a.YFHZ_PTC=b.DIFYF_JHGZH))c group by YFHZ_TSAID) as g on a.AYTJ_TSAID=g.YFHZ_TSAID)t";
            //}
            //else
            //{
            //     sqlsjy = "select AYTJ_TSAID,isnull(RWHFJCB_HJBHSJE,0) as RWHFJCB_HJBHSJE,isnull(WX_MONEY,0) as WX_MONEY,isnull(DIF_MONEY,0) as DIF_MONEY,isnull(CNFB_MONEY,0) as CNFB_MONEY,isnull(CB_CNJJ,0) as CB_CNJJ,isnull(CB_JGYZFT,0) as CB_JGYZFT,isnull(CB_ZZ_GDZZFY,0) as CB_ZZ_GDZZFY,isnull(CB_ZZ_KBZZFY,0) as CB_ZZ_KBZZFY,isnull(DIFYF_MONEY,0) as DIFYF_MONEY,isnull(YF_MONEY,0) as YF_MONEY from (select * from (select * from TBFM_AYTJ where AYTJ_YEARMONTH='" + YEARMONTH + "') as a left join (select isnull(sum(isnull(FJCB_HJBHSJE,0)),0) as RWHFJCB_HJBHSJE,FJCB_TSAID from FM_FJCB where FJCB_YEAR='" + YEAR + "' and FJCB_MONTH='" + MONTH + "' group by FJCB_TSAID) as b on a.AYTJ_TSAID=b.FJCB_TSAID left join (select TAHZ_TSAID,isnull(sum(isnull(DIF_DIFMONEY,0)),0) as DIF_MONEY,isnull(sum(isnull(TAHZ_MONEY,0)),0) as WX_MONEY from (select * from ((select TAHZ_TSAID,TAHZ_PTC,TAHZ_MONEY,TAHZ_YEARMONTH from TBFM_WXHZ where TAHZ_YEARMONTH='" + YEAR + "-" + MONTH + "')a left join (select DIF_JHGZH,DIF_DIFMONEY,DIF_YEAR,DIF_MONTH from TBFM_DIF where DIF_YEAR='" + YEAR + "' and DIF_MONTH='" + MONTH + "')b on a.TAHZ_PTC=b.DIF_JHGZH))c group by TAHZ_TSAID) as c on a.AYTJ_TSAID=c.TAHZ_TSAID left join (select isnull(sum(isnull(CNFB_BYREALMONEY,0)),0) as CNFB_MONEY,CNFB_TSAID from TBMP_CNFB_LIST where CNFB_YEAR='" + YEAR + "'and CNFB_MONTH='" + MONTH + "' and CNFB_TYPE not like '%N%' group by CNFB_TSAID) as d on a.AYTJ_TSAID=d.CNFB_TSAID left join (select CB_TSAID,isnull(CB_CNJJ,0) as CB_CNJJ,isnull(CB_JGYZFT,0) as CB_JGYZFT from CB_FT_JGYZJJ where CB_YEAR='" + YEAR + "'and CB_MONTH='" + MONTH + "') as e on a.AYTJ_TSAID=e.CB_TSAID left join (select CB_ZZ_TSAID,isnull(CB_ZZ_GDZZFY,0) as CB_ZZ_GDZZFY,isnull(CB_ZZ_KBZZFY,0) as CB_ZZ_KBZZFY from CB_FT_ZZFY where CB_ZZ_YEAR='" + YEAR + "'and CB_ZZ_MONTH='" + MONTH + "') as f on a.AYTJ_TSAID=f.CB_ZZ_TSAID left join (select YFHZ_TSAID,isnull(sum(isnull(DIFYF_DIFMONEY,0)),0) as DIFYF_MONEY,isnull(sum(isnull(YFHZ_MONEY,0)),0) as YF_MONEY from (select * from ((select YFHZ_TSAID,YFHZ_PTC,YFHZ_MONEY,YFHZ_YEARMONTH from TBFM_YFHZ  where YFHZ_YEARMONTH='" + YEAR + "-" + MONTH + "')a left join (select DIFYF_JHGZH,DIFYF_DIFMONEY,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF where DIFYF_YEAR='" + YEAR + "' and DIFYF_MONTH='" + MONTH + "')b on a.YFHZ_PTC=b.DIFYF_JHGZH))c group by YFHZ_TSAID) as g on a.AYTJ_TSAID=g.YFHZ_TSAID)t";
            //}
            //原始语句
            //string sqlsjy = "select AYTJ_TSAID,isnull(RWHFJCB_HJBHSJE,0) as RWHFJCB_HJBHSJE,isnull(WX_MONEY,0) as WX_MONEY,isnull(DIF_MONEY,0) as DIF_MONEY,isnull(CNFB_MONEY,0) as CNFB_MONEY,isnull(CB_CNJJ,0) as CB_CNJJ,isnull(CB_JGYZFT,0) as CB_JGYZFT,isnull(CB_ZZ_GDZZFY,0) as CB_ZZ_GDZZFY,isnull(CB_ZZ_KBZZFY,0) as CB_ZZ_KBZZFY,isnull(DIFYF_MONEY,0) as DIFYF_MONEY,isnull(YF_MONEY,0) as YF_MONEY from (select * from (select * from TBFM_AYTJ where AYTJ_YEARMONTH='" + YEARMONTH + "') as a left join (select isnull(sum(isnull(FJCB_HJBHSJE,0)),0) as RWHFJCB_HJBHSJE,FJCB_TSAID from FM_FJCB where FJCB_YEAR='" + YEAR + "' and FJCB_MONTH='" + MONTH + "' group by FJCB_TSAID) as b on a.AYTJ_TSAID=b.FJCB_TSAID left join (select TAHZ_TSAID,isnull(sum(isnull(DIF_DIFMONEY,0)),0) as DIF_MONEY,isnull(sum(isnull(TAHZ_MONEY,0)),0) as WX_MONEY from (select * from ((select TAHZ_TSAID,TAHZ_PTC,TAHZ_MONEY,TAHZ_YEARMONTH from TBFM_WXHZ where TAHZ_YEARMONTH='" + YEAR + "-" + MONTH + "')a left join (select DIF_JHGZH,DIF_DIFMONEY,DIF_YEAR,DIF_MONTH from TBFM_DIF where DIF_YEAR='" + YEAR + "' and DIF_MONTH='" + MONTH + "')b on a.TAHZ_PTC=b.DIF_JHGZH))c group by TAHZ_TSAID) as c on a.AYTJ_TSAID=c.TAHZ_TSAID left join (select isnull(sum(isnull(CNFB_BYREALMONEY,0)),0) as CNFB_MONEY,CNFB_TSAID from TBMP_CNFB_LIST where CNFB_YEAR='" + YEAR + "'and CNFB_MONTH='" + MONTH + "' and CNFB_TYPE not like '%N%' group by CNFB_TSAID) as d on a.AYTJ_TSAID=d.CNFB_TSAID left join (select CB_TSAID,isnull(CB_CNJJ,0) as CB_CNJJ,isnull(CB_JGYZFT,0) as CB_JGYZFT from CB_FT_JGYZJJ where CB_YEAR='" + YEAR + "'and CB_MONTH='" + MONTH + "') as e on a.AYTJ_TSAID=e.CB_TSAID left join (select CB_ZZ_TSAID,isnull(CB_ZZ_GDZZFY,0) as CB_ZZ_GDZZFY,isnull(CB_ZZ_KBZZFY,0) as CB_ZZ_KBZZFY from CB_FT_ZZFY where CB_ZZ_YEAR='" + YEAR + "'and CB_ZZ_MONTH='" + MONTH + "') as f on a.AYTJ_TSAID=f.CB_ZZ_TSAID left join (select YFHZ_TSAID,isnull(sum(isnull(DIFYF_DIFMONEY,0)),0) as DIFYF_MONEY,isnull(sum(isnull(YFHZ_MONEY,0)),0) as YF_MONEY from (select * from ((select YFHZ_TSAID,YFHZ_PTC,YFHZ_MONEY,YFHZ_YEARMONTH from TBFM_YFHZ  where YFHZ_YEARMONTH='" + YEAR + "-" + MONTH + "')a left join (select DIFYF_JHGZH,DIFYF_DIFMONEY,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF where DIFYF_YEAR='" + YEAR + "' and DIFYF_MONTH='" + MONTH + "')b on a.YFHZ_PTC=b.DIFYF_JHGZH))c group by YFHZ_TSAID) as g on a.AYTJ_TSAID=g.YFHZ_TSAID)t";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlsjy);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string rwh = dt.Rows[i]["AYTJ_TSAID"].ToString();
                    string sqltext = "update TBFM_AYTJ set AYTJ_FJCB=" + Convert.ToDouble(dt.Rows[i]["RWHFJCB_HJBHSJE"].ToString()) + ",AYTJ_WXFY=" + Convert.ToDouble(dt.Rows[i]["WX_MONEY"].ToString()) + ",AYTJ_CNFB=" + Convert.ToDouble(dt.Rows[i]["CNFB_MONEY"].ToString()) + ",AYTJ_JJFY=" + Convert.ToDouble(dt.Rows[i]["CB_CNJJ"].ToString()) + ",AYTJ_JGYZFY=" + Convert.ToDouble(dt.Rows[i]["CB_JGYZFT"].ToString()) + ",AYTJ_GDZZFY=" + Convert.ToDouble(dt.Rows[i]["CB_ZZ_GDZZFY"].ToString()) + ",AYTJ_KBZZFY=" + Convert.ToDouble(dt.Rows[i]["CB_ZZ_KBZZFY"].ToString()) + ",AYTJ_YF=" + Convert.ToDouble(dt.Rows[i]["YF_MONEY"].ToString()) + " where AYTJ_TSAID='" + rwh + "' and AYTJ_YEARMONTH='" + YEARMONTH + "'";
                    list.Add(sqltext);
                }
                DBCallCommon.ExecuteTrans(list);
            }

        }

        //用于更新生产制号材料月汇总表
        private string update_clf(string rwh)
        {
            string sqlupdate = "";
            string yearmonth = dplYear.SelectedValue.ToString() + "-" + dplMoth.SelectedValue.ToString();
            sqlupdate = "update TBCB_PRD_MAT_SUMMARY_MONTH set PMS_01_01=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "'and OP_TSAID like '%" + rwh + "' and  OP_MARID like '01.01%'),PMS_01_02=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '01.02%'),PMS_01_03=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '01.03%'),PMS_01_04=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '01.04%'),PMS_01_05=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '01.05%'),PMS_01_06=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '01.06%'),PMS_01_07=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '01.07%'),PMS_01_08=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '01.08%'),PMS_01_09=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '01.09%'),PMS_01_10=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '01.10%'),PMS_01_11=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '01.11%'),PMS_01_12=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '01.12%'),PMS_01_13=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '01.13%'),PMS_01_14=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '01.14%'),PMS_01_15=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '01.15%'),PMS_01_16=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '01.16%'),PMS_01_17=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '01.17%'),PMS_01_18=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '01.18%'),PMS_02_01=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '02.01%'),PMS_02_02=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '02.02%'),PMS_02_03=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '02.03%'),PMS_02_04=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '02.04%'),PMS_02_05=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '02.05%'),PMS_02_06=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '02.06%'),PMS_02_07=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '02.07%'),PMS_02_08=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '02.08%'),PMS_02_09=(select ISNULL(sum(OP_AMOUNT),0)  from (select OP_VERIFYDATE,OP_TSAID,OP_AMOUNT,OP_MARID from TBWS_OUT as a left join TBWS_OUTDETAIL as b on a.OP_CODE=b.OP_CODE)t where left(CONVERT(CHAR(10), OP_VERIFYDATE, 23),7)='" + yearmonth + "' and OP_TSAID like '%" + rwh + "' and  OP_MARID like  '02.09%') where PMS_TSAID='" + rwh + "' and PMS_YEAR='" + dplYear.SelectedValue.ToString() + "'and PMS_MONTH='" + dplMoth.SelectedValue.ToString() + "'";
            return sqlupdate;
        }


        /// <summary>
        /// 查询当月是否已生成begin
        /// </summary>
        private bool CheckCurrentExist()//布尔型返回值
        {

            bool ret = false;
            if (dplMoth.SelectedIndex != 0 && dplYear.SelectedIndex != 0)
            {
                string sqltext = "select AYTJ_TSAID from TBFM_AYTJ where AYTJ_YEARMONTH='" + dplYear.SelectedValue.ToString() + "-" + dplMoth.SelectedValue.ToString() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    ret = true;
                    btnCurrentMonth.Text = "更新" + dplMoth.SelectedValue + "月统计";
                    btnCurrentMonth.Enabled = true;
                    btnCurrentMonth.BackColor = System.Drawing.Color.White;
                    btnCurrentMonth.CommandName = "update";

                }
                else//允许添加未统计月份的数据
                {
                    //代码块5
                    btnCurrentMonth.Text = "添加" + dplMoth.SelectedValue + "月统计";
                    btnCurrentMonth.Enabled = true;
                    btnCurrentMonth.BackColor = System.Drawing.Color.White;
                    btnCurrentMonth.CommandName = "insert";
                }
            }
            else
            {

            }
            return ret;
        }
        /// <summary>
        /// 查询当月是否已生成end
        /// </summary>


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //数据汇总
        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //TextBox tbgz = (TextBox)e.Item.FindControl("tbgz");
                TextBox tbjjfy = (TextBox)e.Item.FindControl("tbjjfy");
                TextBox tbjgyzfy = (TextBox)e.Item.FindControl("tbjgyzfy");
                TextBox tbzjrgxj = (TextBox)e.Item.FindControl("tbzjrgxj");
                TextBox tbbzj = (TextBox)e.Item.FindControl("tbbzj");
                TextBox tbhcl = (TextBox)e.Item.FindControl("tbhcl");
                TextBox tbhsjs = (TextBox)e.Item.FindControl("tbhsjs");
                TextBox tbzj = (TextBox)e.Item.FindControl("tbzj");
                TextBox tbdj = (TextBox)e.Item.FindControl("tbdj");
                TextBox tbzc = (TextBox)e.Item.FindControl("tbzc");
                TextBox tbwgj = (TextBox)e.Item.FindControl("tbwgj");
                TextBox tbyqtl = (TextBox)e.Item.FindControl("tbyqtl");
                TextBox tbqtcl = (TextBox)e.Item.FindControl("tbqtcl");
                TextBox tbclxj = (TextBox)e.Item.FindControl("tbclxj");
                TextBox tbgdzzfy = (TextBox)e.Item.FindControl("tbgdzzfy");
                TextBox tbkbzzfy = (TextBox)e.Item.FindControl("tbkbzzfy");
                TextBox tbwxfy = (TextBox)e.Item.FindControl("tbwxfy");
                TextBox tbcnfb = (TextBox)e.Item.FindControl("tbcnfb");
                TextBox tbyf = (TextBox)e.Item.FindControl("tbyf");
                TextBox tbfjcb = (TextBox)e.Item.FindControl("tbfjcb");
                TextBox tbqt = (TextBox)e.Item.FindControl("tbqt");
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select cast(isnull(CAST(sum(AYTJ_JJFY) AS FLOAT),0) as decimal(12,2)) as JJFYHJ,cast(isnull(CAST(sum(AYTJ_JGYZFY) AS FLOAT),0) as decimal(12,2)) as JGYZFYHJ,cast(isnull(CAST(sum(isnull(AYTJ_JJFY,0)+isnull(AYTJ_JGYZFY,0)) AS FLOAT),0) as decimal(12,2)) as ZJRGHJ,isnull(CAST(sum(PMS_01_01) AS FLOAT),0) as BZJHJ,isnull(CAST(sum(PMS_01_05) AS FLOAT),0) as HCLHJ,isnull(CAST(sum(PMS_01_07) AS FLOAT),0) as HSJSHJ,isnull(CAST(sum(PMS_01_08) AS FLOAT),0) as ZJHJ,isnull(CAST(sum(PMS_01_09) AS FLOAT),0) as DJHJ,isnull(CAST(sum(PMS_01_10) AS FLOAT),0) as ZCHJ,isnull(CAST(sum(PMS_01_11) AS FLOAT),0) as WGJHJ,isnull(CAST(sum(PMS_01_15) AS FLOAT),0) as YQTLHJ,isnull(CAST(sum(PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_06+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) AS FLOAT),0) as QTCLHJ,isnull(CAST(sum(PMS_01_01+PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_05+PMS_01_06+PMS_01_07+PMS_01_08+PMS_01_09+PMS_01_10+PMS_01_11+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_15+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) AS FLOAT),0) as CLHJ,cast(isnull(CAST(sum(AYTJ_GDZZFY) AS FLOAT),0) as decimal(12,2)) as GDZZFYHJ,cast(isnull(CAST(sum(AYTJ_KBZZFY) AS FLOAT),0) as decimal(12,2)) as KBZZFYHJ,cast(((isnull(CAST(sum(AYTJ_GDZZFY) AS FLOAT),0))+(isnull(CAST(sum(AYTJ_KBZZFY) AS FLOAT),0))) as decimal(12,2)) as ZZFYHJ,isnull(CAST(sum((isnull(AYTJ_WXFY,0)+isnull(DIF_DIFMONEY,0))) AS decimal(12,2)),0) as WXFYHJ,cast(isnull(CAST(sum(AYTJ_CNFB) AS FLOAT),0) as decimal(12,2)) as CNFBHJ,isnull(CAST(sum((isnull(AYTJ_YF,0)+isnull(DIFYF_DIFMONEY,0))) AS FLOAT),0) as YFHJ,isnull(CAST(sum(AYTJ_FJCB) AS FLOAT),0) as FJCBHJ,isnull(CAST(sum(AYTJ_QT) AS FLOAT),0) as QTHJ from (select * from VIEW_FM_AYTJ as a left join (select sum(cast(DIF_DIFMONEY as decimal(12,2))) as DIF_DIFMONEY,DIF_TSAID,DIF_YEAR,DIF_MONTH from TBFM_DIF group by DIF_TSAID,DIF_YEAR,DIF_MONTH)b on (a.PMS_TSAID=b.DIF_TSAID and a.AYTJ_YEARMONTH=b.DIF_YEAR+'-'+b.DIF_MONTH) left join (select sum(cast(DIFYF_DIFMONEY as decimal(12,2))) as DIFYF_DIFMONEY,DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF group by DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH)c on (a.PMS_TSAID=c.DIFYF_TSAID and a.AYTJ_YEARMONTH=c.DIFYF_YEAR+'-'+c.DIFYF_MONTH))t where " + ViewState["sqltext"].ToString();//isnull(CAST(sum(AYTJ_GZ) AS FLOAT),0) as GZHJ,

                SqlDataReader drhj = DBCallCommon.GetDRUsingSqlText(sqlhj);

                if (drhj.Read())
                {

                    //gzhj = Convert.ToDouble(drhj["GZHJ"]);
                    jjfyhj = Convert.ToDouble(drhj["JJFYHJ"]);
                    jgyzfyhj = Convert.ToDouble(drhj["JGYZFYHJ"]);
                    zjrghj = Convert.ToDouble(drhj["ZJRGHJ"]);
                    bzjhj = Convert.ToDouble(drhj["BZJHJ"]);
                    hclhj = Convert.ToDouble(drhj["HCLHJ"]);
                    hsjshj = Convert.ToDouble(drhj["HSJSHJ"]);
                    zjhj = Convert.ToDouble(drhj["ZJHJ"]);
                    djhj = Convert.ToDouble(drhj["DJHJ"]);
                    zchj = Convert.ToDouble(drhj["ZCHJ"]);
                    wgjhj = Convert.ToDouble(drhj["WGJHJ"]);
                    yqtlhj = Convert.ToDouble(drhj["YQTLHJ"]);
                    qtclhj = Convert.ToDouble(drhj["QTCLHJ"]);
                    clhj = Convert.ToDouble(drhj["CLHJ"]);
                    gdzzfyhj = Convert.ToDouble(drhj["GDZZFYHJ"]);
                    kbzzfyhj = Convert.ToDouble(drhj["KBZZFYHJ"]);
                    zzfyhj = Convert.ToDouble(drhj["ZZFYHJ"]);
                    wxfyhj = Convert.ToDouble(drhj["WXFYHJ"]);
                    cnfbhj = Convert.ToDouble(drhj["CNFBHJ"]);
                    yfhj = Convert.ToDouble(drhj["YFHJ"]);
                    fjcbhj = Convert.ToDouble(drhj["FJCBHJ"]);
                    qthj = Convert.ToDouble(drhj["QTHJ"]);
                }
                drhj.Close();
                //Label lbgzhj = (Label)e.Item.FindControl("lbgzhj");
                Label lbjjfyhj = (Label)e.Item.FindControl("lbjjfyhj");
                Label lbjgyzfyhj = (Label)e.Item.FindControl("lbjgyzfyhj");
                Label lbzjrghj = (Label)e.Item.FindControl("lbzjrghj");

                Label lbbzjhj = (Label)e.Item.FindControl("lbbzjhj");
                Label lbhclhj = (Label)e.Item.FindControl("lbhclhj");
                Label lbhsjshj = (Label)e.Item.FindControl("lbhsjshj");
                Label lbzjhj = (Label)e.Item.FindControl("lbzjhj");
                Label lbdjhj = (Label)e.Item.FindControl("lbdjhj");
                Label lbzchj = (Label)e.Item.FindControl("lbzchj");
                Label lbwgjhj = (Label)e.Item.FindControl("lbwgjhj");
                Label lbyqtlhj = (Label)e.Item.FindControl("lbyqtlhj");
                Label lbqtclhj = (Label)e.Item.FindControl("lbqtclhj");
                Label lbclhj = (Label)e.Item.FindControl("lbclhj");

                Label lbgdzzfyhj = (Label)e.Item.FindControl("lbgdzzfyhj");
                Label lbkbzzfyhj = (Label)e.Item.FindControl("lbkbzzfyhj");
                Label lbzzfyhj = (Label)e.Item.FindControl("lbzzfyhj");

                Label lbwxfyhj = (Label)e.Item.FindControl("lbwxfyhj");
                Label lbcnfbhj = (Label)e.Item.FindControl("lbcnfbhj");
                Label lbyfhj = (Label)e.Item.FindControl("lbyfhj");
                Label lbfjcbhj = (Label)e.Item.FindControl("lbfjcbhj");
                Label lbqthj = (Label)e.Item.FindControl("lbqthj");

                //lbgzhj.Text = gzhj.ToString();
                lbjjfyhj.Text = jjfyhj.ToString();
                lbjgyzfyhj.Text = jgyzfyhj.ToString();
                lbzjrghj.Text = zjrghj.ToString();

                lbbzjhj.Text = bzjhj.ToString();
                lbhclhj.Text = hclhj.ToString();
                lbhsjshj.Text = hsjshj.ToString();
                lbzjhj.Text = zjhj.ToString();
                lbdjhj.Text = djhj.ToString();
                lbzchj.Text = zchj.ToString();
                lbwgjhj.Text = wgjhj.ToString();
                lbyqtlhj.Text = yqtlhj.ToString();
                lbqtclhj.Text = qtclhj.ToString();
                lbclhj.Text = clhj.ToString();

                lbgdzzfyhj.Text = gdzzfyhj.ToString();
                lbkbzzfyhj.Text = kbzzfyhj.ToString();
                lbzzfyhj.Text = zzfyhj.ToString();

                lbwxfyhj.Text = wxfyhj.ToString();
                lbcnfbhj.Text = cnfbhj.ToString();
                lbyfhj.Text = yfhj.ToString();
                lbfjcbhj.Text = fjcbhj.ToString();
                lbqthj.Text = qthj.ToString();

            }
        }
        //直接人工及工资费用分摊
        protected void btnFT_OnClick(object sender, EventArgs e)
        {
            string year = dplYear.SelectedValue;
            string month = dplMoth.SelectedValue;
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "FYFTGZRG('" + year + "','" + month + "');", true);

        }
        //制造费用分摊
        protected void btnFTZZ_OnClick(object sender, EventArgs e)
        {
            string year = dplYear.SelectedValue;
            string month = dplMoth.SelectedValue;
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "FYFTZZ('" + year + "','" + month + "');", true);

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////



        ////////////////////////////////////////////////////////////////////////////////////////////////

        protected void btnkqck_OnClick(object sender, EventArgs e) //跨期间查询确定
        {
            string startyearmonth = ddlstartcx1.SelectedValue + "-" + ddlstartcx2.SelectedValue;
            string endyearmonth = ddlendcx1.SelectedValue + "-" + ddlendcx2.SelectedValue;
            string sql = "select * from View_FM_AYTJ where (PMS_YEAR+'-'+PMS_MONTH)>='" + startyearmonth + "' and (PMS_YEAR+'-'+PMS_MONTH)<='" + endyearmonth + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "Kqjcx('" + startyearmonth + "','" + endyearmonth + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该期间没有数据!');", true);
            }

        }
        protected void btnback_Click(object sender, EventArgs e)//跨期间查询取消
        {
            ModalPopupExtender2.Hide();
        }
        protected void btnky_Click(object sender, EventArgs e)//空的跨期间查询
        {

        }
        /////////////////////////////////////////////////////////////////////////////////////////////////
     
        #region 导出功能

        protected void btnExport_Click(object sender, EventArgs e) //导出
        {
            string sqltext = "select PMS_TSAID,TSA_PJID,CM_PROJ,AYTJ_YEARMONTH,cast(ISNULL(AYTJ_JJFY,0) as decimal(12,2)) as AYTJ_JJFY,cast(ISNULL(AYTJ_JGYZFY,0) as decimal(12,2)) as AYTJ_JGYZFY,cast((ISNULL(AYTJ_JJFY,0)+ISNULL(AYTJ_JGYZFY,0)) as decimal(12,2)) as AYTJ_ZJRGFXJ,ISNULL(PMS_01_11,0) as WGJ,ISNULL(PMS_01_07,0) as HSJS,ISNULL(PMS_01_05,0) as HCL,ISNULL(PMS_01_08,0) as ZJ,ISNULL(PMS_01_09,0) as DJ,ISNULL(PMS_01_10,0) as ZC,ISNULL(PMS_01_01,0) as BZJ,ISNULL(PMS_01_15,0) as YQTL,(PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_06+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) as QTCL,(PMS_01_01+PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_05+PMS_01_06+PMS_01_07+PMS_01_08+PMS_01_09+PMS_01_10+PMS_01_11+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_15+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) as CLXJ,cast(ISNULL(AYTJ_GDZZFY,0) as decimal(12,2)) as AYTJ_GDZZFY,cast(ISNULL(AYTJ_KBZZFY,0) as decimal(12,2)) as AYTJ_KBZZFY,cast(((ISNULL(AYTJ_KBZZFY,0))+(ISNULL(AYTJ_GDZZFY,0))) as decimal(12,2)) as ZZFYXJ,isnull(cast((isnull(AYTJ_WXFY,0)+isnull(DIF_DIFMONEY,0)) as decimal(12,2)),0) as AYTJ_WXFY,cast(ISNULL(AYTJ_CNFB,0) as decimal(12,2)) as AYTJ_CNFB,isnull((isnull(AYTJ_YF,0)+isnull(DIFYF_DIFMONEY,0)),0) as AYTJ_YF,ISNULL(AYTJ_FJCB,0) as AYTJ_FJCB,ISNULL(AYTJ_QT,0) as AYTJ_QT from (select * from VIEW_FM_AYTJ as a left join (select sum(cast(DIF_DIFMONEY as decimal(12,2))) as DIF_DIFMONEY,DIF_TSAID,DIF_YEAR,DIF_MONTH from TBFM_DIF group by DIF_TSAID,DIF_YEAR,DIF_MONTH)b on (a.PMS_TSAID=b.DIF_TSAID and a.AYTJ_YEARMONTH=b.DIF_YEAR+'-'+b.DIF_MONTH) left join (select sum(cast(DIFYF_DIFMONEY as decimal(12,2))) as DIFYF_DIFMONEY,DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF group by DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH)c on (a.PMS_TSAID=c.DIFYF_TSAID and a.AYTJ_YEARMONTH=c.DIFYF_YEAR+'-'+c.DIFYF_MONTH))t where " + ViewState["sqltext"].ToString() + "order by PMS_TSAID ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string sqlfooter = "select cast(isnull(CAST(sum(AYTJ_JJFY) AS FLOAT),0) as decimal(12,2)) as JJFYHJ,cast(isnull(CAST(sum(AYTJ_JGYZFY) AS FLOAT),0) as decimal(12,2)) as JGYZFYHJ,cast(isnull(CAST(sum(isnull(AYTJ_JJFY,0)+isnull(AYTJ_JGYZFY,0)) AS FLOAT),0) as decimal(12,2)) as ZJRGHJ,isnull(CAST(sum(PMS_01_11) AS FLOAT),0) as WGJHJ,isnull(CAST(sum(PMS_01_07) AS FLOAT),0) as HSJSHJ,isnull(CAST(sum(PMS_01_05) AS FLOAT),0) as HCLHJ,isnull(CAST(sum(PMS_01_08) AS FLOAT),0) as ZJHJ,isnull(CAST(sum(PMS_01_09) AS FLOAT),0) as DJHJ,isnull(CAST(sum(PMS_01_10) AS FLOAT),0) as ZCHJ,isnull(CAST(sum(PMS_01_01) AS FLOAT),0) as BZJHJ,isnull(CAST(sum(PMS_01_15) AS FLOAT),0) as YQTLHJ,isnull(CAST(sum(PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_06+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) AS FLOAT),0) as QTCLHJ,isnull(CAST(sum(PMS_01_01+PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_05+PMS_01_06+PMS_01_07+PMS_01_08+PMS_01_09+PMS_01_10+PMS_01_11+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_15+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) AS FLOAT),0) as CLHJ,cast(isnull(CAST(sum(AYTJ_GDZZFY) AS FLOAT),0) as decimal(12,2)) as GDZZFYHJ,cast(isnull(CAST(sum(AYTJ_KBZZFY) AS FLOAT),0) as decimal(12,2)) as KBZZFYHJ,cast(((isnull(CAST(sum(AYTJ_GDZZFY) AS FLOAT),0))+(isnull(CAST(sum(AYTJ_KBZZFY) AS FLOAT),0))) as decimal(12,2)) as ZZFYHJ,isnull(CAST(sum((isnull(AYTJ_WXFY,0)+isnull(DIF_DIFMONEY,0))) AS decimal(12,2)),0) as WXFYHJ,cast(isnull(CAST(sum(AYTJ_CNFB) AS FLOAT),0) as decimal(12,2)) as CNFBHJ,isnull(CAST(sum((isnull(AYTJ_YF,0)+isnull(DIFYF_DIFMONEY,0))) AS FLOAT),0) as YFHJ,isnull(CAST(sum(AYTJ_FJCB) AS FLOAT),0) as FJCBHJ,isnull(CAST(sum(AYTJ_QT) AS FLOAT),0) as QTHJ from (select * from VIEW_FM_AYTJ as a left join (select sum(cast(DIF_DIFMONEY as decimal(12,2))) as DIF_DIFMONEY,DIF_TSAID,DIF_YEAR,DIF_MONTH from TBFM_DIF group by DIF_TSAID,DIF_YEAR,DIF_MONTH)b on (a.PMS_TSAID=b.DIF_TSAID and a.AYTJ_YEARMONTH=b.DIF_YEAR+'-'+b.DIF_MONTH) left join (select sum(cast(DIFYF_DIFMONEY as decimal(12,2))) as DIFYF_DIFMONEY,DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF group by DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH)c on (a.PMS_TSAID=c.DIFYF_TSAID and a.AYTJ_YEARMONTH=c.DIFYF_YEAR+'-'+c.DIFYF_MONTH))t where " + ViewState["sqltext"].ToString();
            DataTable dtfooter = DBCallCommon.GetDTUsingSqlText(sqlfooter);
            ExportDataItem(dt, dtfooter);
        }

        private void ExportDataItem(DataTable dt, DataTable dtfooter)
        {
            string filename = "成本按月统计表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("成本按月统计表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }

                IRow rowfooter = sheet0.CreateRow(dt.Rows.Count + 2);
                ICell cellfooter4 = rowfooter.CreateCell(4);
                cellfooter4.SetCellValue("合计：");
                for (int k = 0; k < dtfooter.Columns.Count; k++)
                {
                    rowfooter.CreateCell(k + 5).SetCellValue(dtfooter.Rows[0][k].ToString());
                }

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        #endregion
    }
}
