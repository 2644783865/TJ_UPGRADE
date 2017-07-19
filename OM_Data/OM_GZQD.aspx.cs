﻿using System;
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
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;


namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_GZQD : BasicPage
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserName"].ToString().Trim() == "管理员")
                {
                    btnimport.Visible = true;
                    FileUpload1.Visible = true;
                }
                //工资查询记录
                string dengluid = Session["UserID"].ToString();
                string dengluname = Session["UserName"].ToString();
                string ym = "工资表";
                string sql = "insert OM_GZ_JL (USER_ID,USER_NAME,TIME,YM) values ('" + dengluid + "','" + dengluname + "','" + DateTime.Now.ToString() + "','" + ym + "')";
                DBCallCommon.ExeSqlText(sql);
                BindbmData();
                bindbz();
                bindgw();
                GetSele();
                this.BindYearMoth(ddlYear, ddlMonth);
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindrpt();
            }
            CheckUser(ControlFinder);
            this.InitVar();
        }
        /// <summary>
        /// 绑定年月
        /// </summary>
        /// <param name="dpl_Year"></param>
        /// <param name="dpl_Month"></param>
        private void BindYearMoth(DropDownList ddl_Year, DropDownList ddl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                ddl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            ddl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));

            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                ddl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            ddl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));

        }
        #region 分页
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            ddlYear.ClearSelection();
            foreach (ListItem li in ddlYear.Items)//显示当前年份
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }

            ddlMonth.ClearSelection();
            string month = (DateTime.Now.Month - 1).ToString();
            if (DateTime.Now.Month < 10 || DateTime.Now.Month == 10)//显示当前月份
            {
                month = "0" + month;
            }
            foreach (ListItem li in ddlMonth.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
        }
        //绑定基本信息
        private void BindbmData()
        {
            string stId = Session["UserId"].ToString();
            System.Data.DataTable dt = DBCallCommon.GetPermeision(16, stId);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
        }
        //绑定班组
        private void bindbz()
        {
            string sqlbz = "SELECT DISTINCT ST_DEPID1 FROM TBDS_STAFFINFO WHERE ST_DEPID='" + ddl_Depart.SelectedValue.ToString().Trim() + "'";
            System.Data.DataTable dtbz = DBCallCommon.GetDTUsingSqlText(sqlbz);
            DropDownListbanzu.DataSource = dtbz;
            DropDownListbanzu.DataTextField = "ST_DEPID1";
            DropDownListbanzu.DataValueField = "ST_DEPID1";
            DropDownListbanzu.DataBind();
            ListItem item = new ListItem("--请选择--", "000");
            DropDownListbanzu.Items.Insert(0, item);
        }
        //绑定岗位
        private void bindgw()
        {
            string sqlgw = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO WHERE len(DEP_CODE)=4 and DEP_CODE like '" + ddl_Depart.SelectedValue.ToString().Trim() + "%'";
            DataTable dtgw = DBCallCommon.GetDTUsingSqlText(sqlgw);
            DropDownListgw.DataSource = dtgw;
            DropDownListgw.DataTextField = "DEP_NAME";
            DropDownListgw.DataValueField = "DEP_CODE";
            DropDownListgw.DataBind();
            ListItem item = new ListItem("--请选择--", "000");
            DropDownListgw.Items.Insert(0, item);
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
            pager_org.TableName = "(select * from View_OM_GZQD left join OM_KQTJ on (View_OM_GZQD.QD_STID=OM_KQTJ.KQ_ST_ID and View_OM_GZQD.QD_YEARMONTH=OM_KQTJ.KQ_DATE))t";
            pager_org.PrimaryKey = "QD_ID";
            pager_org.ShowFields = "*,(QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY) as QD_YFHJ,(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG) as QD_DaiKouXJ,((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG)) as QD_ShiFaJE,((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ)-QD_KOUXIANG) as QD_KOUSJS ";
            pager_org.OrderField = "DEP_NAME,ST_WORKNO,QD_YEARMONTH";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = Convert.ToInt32(DropDownListCount.SelectedValue.ToString().Trim());
        }
        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            bindbz();
            bindgw();
            string sql = "1=1 and OM_GZSCBZ='1'";
            if (ddlYear.SelectedIndex != 0 && ddlMonth.SelectedIndex != 0)
            {
                sql += " and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "'";
            }
            else if (ddlYear.SelectedIndex == 0 && ddlMonth.SelectedIndex != 0)
            {
                sql += " and QD_YEARMONTH like '%-" + ddlMonth.SelectedValue.ToString().Trim() + "'";
            }
            else if (ddlYear.SelectedIndex != 0 && ddlMonth.SelectedIndex == 0)
            {
                sql += " and QD_YEARMONTH like '" + ddlYear.SelectedValue.ToString().Trim() + "-%'";
            }
            if (txtName.Text.Trim() != "")
            {
                sql += " and ST_NAME like '%" + txtName.Text.Trim() + "%'";
            }
            if (txtworkno.Text.Trim() != "")
            {
                sql += " and ST_WORKNO like '%" + txtworkno.Text.Trim() + "%'";
            }
            if (ddl_Depart.SelectedValue != "00")
            {
                sql += " and ST_DEPID='" + ddl_Depart.SelectedValue.ToString().Trim() + "'";
            }
            if (DropDownListbanzu.SelectedIndex != 0)
            {
                sql += " and ST_DEPID1='" + DropDownListbanzu.SelectedValue.ToString().Trim() + "'";
            }
            if (DropDownListgw.SelectedIndex != 0)
            {
                sql += " and DEP_NAME_POSITION='" + DropDownListgw.SelectedValue.ToString().Trim() + "'";
            }
            //筛选

            if (screen1.SelectedValue != "0" || screen2.SelectedValue != "0" || screen3.SelectedValue != "0" || screen4.SelectedValue != "0" || screen5.SelectedValue != "0" || screen6.SelectedValue != "0" || screen7.SelectedValue != "0" || screen8.SelectedValue != "0" || screen9.SelectedValue != "0" || screen10.SelectedValue != "0")
            {
                if (SelectStr(screen1, ddlRelation1, Txt1.Text.Trim(), "") != "")
                {
                    sql += " and (" + SelectStr(screen1, ddlRelation1, Txt1.Text.Trim(), "");
                }
                else
                {
                    sql += " and (1=1 ";
                }
                sql += SelectStr(screen2, ddlRelation2, Txt2.Text.Trim(), ddlLogic1.SelectedValue);
                sql += SelectStr(screen3, ddlRelation3, Txt3.Text.Trim(), ddlLogic2.SelectedValue);
                sql += SelectStr(screen4, ddlRelation4, Txt4.Text.Trim(), ddlLogic3.SelectedValue);
                sql += SelectStr(screen5, ddlRelation5, Txt5.Text.Trim(), ddlLogic4.SelectedValue);
                sql += SelectStr(screen6, ddlRelation6, Txt6.Text.Trim(), ddlLogic5.SelectedValue);
                sql += SelectStr(screen7, ddlRelation7, Txt7.Text.Trim(), ddlLogic6.SelectedValue);
                sql += SelectStr(screen8, ddlRelation8, Txt8.Text.Trim(), ddlLogic7.SelectedValue);
                sql += SelectStr(screen9, ddlRelation9, Txt9.Text.Trim(), ddlLogic8.SelectedValue);
                sql += SelectStr(screen10, ddlRelation10, Txt10.Text.Trim(), ddlLogic9.SelectedValue);
                sql += ")";
            }
            //
            return sql;
        }

        private string SelectStr(DropDownList ddl, DropDownList ddl1, string txt, string logic) //选择条件拼接字符串
        {
            string sqlstr = string.Empty;
            if (ddl.SelectedValue != "0")
            {
                switch (ddl1.SelectedValue)
                {
                    case "0":
                        sqlstr = string.Format("{0} {1} like '%{2}%' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "1":
                        sqlstr = string.Format("{0} {1} not like '%{2}%' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "2":
                        sqlstr = string.Format("{0} {1}={2} ", logic, ddl.SelectedValue, CommonFun.ComTryDecimal(txt));
                        break;
                    case "3":
                        sqlstr = string.Format("{0} {1}!={2} ", logic, ddl.SelectedValue, CommonFun.ComTryDecimal(txt));
                        break;
                    case "4":
                        sqlstr = string.Format("{0} {1}>{2} ", logic, ddl.SelectedValue, CommonFun.ComTryDecimal(txt));
                        break;
                    case "5":
                        sqlstr = string.Format("{0} {1}>={2} ", logic, ddl.SelectedValue, CommonFun.ComTryDecimal(txt));
                        break;
                    case "6":
                        sqlstr = string.Format("{0} {1}<{2} ", logic, ddl.SelectedValue, CommonFun.ComTryDecimal(txt));
                        break;
                    case "7":
                        sqlstr = string.Format("{0} {1}<={2} ", logic, ddl.SelectedValue, CommonFun.ComTryDecimal(txt));
                        break;
                }
            }
            return sqlstr;
        }
        private void GetSele()//绑定筛选信息
        {
            string sqlText = "select * from VIEW_OM_GZQD_SELECT";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            foreach (Control item in palORG.Controls)
            {
                if (item is DropDownList)
                {
                    if (item.ID.Contains("screen"))
                    {
                        ((DropDownList)item).DataSource = dt;
                        ((DropDownList)item).DataTextField = "name";
                        ((DropDownList)item).DataValueField = "id";
                        ((DropDownList)item).DataBind();
                        ((DropDownList)item).SelectedValue = "0";
                    }
                }
            }
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
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptGZQD, UCPaging1, palNodata);
            if (palNodata.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }
        #endregion

        /// <summary>
        /// 年份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();

        }
        /// <summary>
        /// 月份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
        /// <summary>
        /// 姓名查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void dplbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        //显示条数
        protected void Count_Change(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        #region 控制隐藏列
        /// <summary>
        /// 隐藏部门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxBumen_CheckedChanged(object sender, EventArgs e)
        {
            bindrpt();
        }
        /// <summary>
        /// 隐藏考勤
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxKaoqin_CheckedChanged(object sender, EventArgs e)
        {
            bindrpt();
        }
        /// <summary>
        /// 隐藏五险一金
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxWXYJ_CheckedChanged(object sender, EventArgs e)
        {
            bindrpt();
        }
        /// <summary>
        /// 隐藏班组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxbanzu_CheckedChanged(object sender, EventArgs e)
        {
            bindrpt();
        }
        /// <summary>
        /// 隐藏工号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxgh_CheckedChanged(object sender, EventArgs e)
        {
            bindrpt();
        }
        /// <summary>
        /// 隐藏岗位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxgw_CheckedChanged(object sender, EventArgs e)
        {
            bindrpt();
        }
        /// <summary>
        /// rpt内容控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptGZQD_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string sqltext = "select sum(QD_JCGZ) as QD_JCGZhj,sum(QD_GZGL) as QD_GZGLhj,sum(QD_GDGZ) as QD_GDGZhj,sum(QD_JXGZ) as QD_JXGZhj,sum(QD_JiangLi) as QD_JiangLihj,sum(QD_BingJiaGZ) as QD_BingJiaGZhj,sum(QD_JiaBanGZ) as QD_JiaBanGZhj,sum(QD_BFJB) as QD_BFJBhj,sum(QD_ZYBF) as QD_ZYBFhj,sum(QD_BFZYB) as QD_BFZYBhj,sum(QD_NianJiaGZ) as QD_NianJiaGZhj,sum(QD_YKGW) as QD_YKGWhj,sum(QD_TZBF) as QD_TZBFhj,sum(QD_TZBK) as QD_TZBKhj,sum(QD_JTBT) as QD_JTBThj,sum(QD_FSJW) as QD_FSJWhj,sum(QD_CLBT) as QD_CLBThj,sum(QD_QTFY) as QD_QTFYhj,(sum(QD_JCGZ)+sum(QD_GZGL)+sum(QD_GDGZ)+sum(QD_JXGZ)+sum(QD_JiangLi)+sum(QD_BingJiaGZ)+sum(QD_JiaBanGZ)+sum(QD_BFJB)+sum(QD_ZYBF)+sum(QD_BFZYB)+sum(QD_NianJiaGZ)+sum(QD_YKGW)+sum(QD_TZBF)+sum(QD_TZBK)+sum(QD_JTBT)+sum(QD_FSJW)+sum(QD_CLBT)+sum(QD_QTFY)) as QD_YFHJhj,sum(QD_YLBX) as QD_YLBXhj,sum(QD_SYBX) as QD_SYBXhj,sum(QD_YiLiaoBX) as QD_YiLiaoBXhj,sum(QD_DEJZ) as QD_DEJZhj,sum(QD_BuBX) as QD_BuBXhj,sum(QD_GJJ) as QD_GJJhj,sum(QD_BGJJ) as QD_BGJJhj,sum(QD_ShuiDian) as QD_ShuiDianhj,sum(QD_GeShui) as QD_GeShuihj,(sum(QD_YLBX)+sum(QD_SYBX)+sum(QD_YiLiaoBX)+sum(QD_DEJZ)+sum(QD_BuBX)+sum(QD_GJJ)+sum(QD_BGJJ)+sum(QD_ShuiDian)+sum(QD_GeShui)+sum(QD_KOUXIANG)) as QD_DaiKouXJhj,((sum(QD_JCGZ)+sum(QD_GZGL)+sum(QD_GDGZ)+sum(QD_JXGZ)+sum(QD_JiangLi)+sum(QD_BingJiaGZ)+sum(QD_JiaBanGZ)+sum(QD_BFJB)+sum(QD_ZYBF)+sum(QD_BFZYB)+sum(QD_NianJiaGZ)+sum(QD_YKGW)+sum(QD_TZBF)+sum(QD_TZBK)+sum(QD_JTBT)+sum(QD_FSJW)+sum(QD_CLBT)+sum(QD_QTFY))-(sum(QD_YLBX)+sum(QD_SYBX)+sum(QD_YiLiaoBX)+sum(QD_DEJZ)+sum(QD_BuBX)+sum(QD_GJJ)+sum(QD_BGJJ)+sum(QD_ShuiDian)+sum(QD_GeShui)+sum(QD_KOUXIANG))) as QD_ShiFaJEhj,((sum(QD_JCGZ)+sum(QD_GZGL)+sum(QD_GDGZ)+sum(QD_JXGZ)+sum(QD_JiangLi)+sum(QD_BingJiaGZ)+sum(QD_JiaBanGZ)+sum(QD_BFJB)+sum(QD_ZYBF)+sum(QD_BFZYB)+sum(QD_NianJiaGZ)+sum(QD_YKGW)+sum(QD_TZBF)+sum(QD_TZBK)+sum(QD_JTBT)+sum(QD_FSJW)+sum(QD_QTFY))-(sum(QD_YLBX)+sum(QD_SYBX)+sum(QD_YiLiaoBX)+sum(QD_DEJZ)+sum(QD_BuBX)+sum(QD_GJJ)+sum(QD_BGJJ))-sum(QD_KOUXIANG)) as QD_KOUSJShj  from (select * from View_OM_GZQD left join OM_KQTJ on (View_OM_GZQD.QD_STID=OM_KQTJ.KQ_ST_ID and View_OM_GZQD.QD_YEARMONTH=OM_KQTJ.KQ_DATE))t where " + StrWhere();
            System.Data.DataTable dt000 = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (e.Item.ItemType == ListItemType.Header)
            {
                if (cbxBumen.Checked)
                {
                    HtmlTableCell Bumen = e.Item.FindControl("tdBumen") as HtmlTableCell;
                    Bumen.Visible = false;
                }
                if (cbxbanzu.Checked)
                {
                    HtmlTableCell Banzu = e.Item.FindControl("tdBanzu") as HtmlTableCell;
                    Banzu.Visible = false;
                }
                if (cbxgh.Checked)
                {
                    HtmlTableCell gonghao = e.Item.FindControl("tdgonghao") as HtmlTableCell;
                    gonghao.Visible = false;
                }
                if (cbxgw.Checked)
                {
                    HtmlTableCell gangwei = e.Item.FindControl("tdgangwei") as HtmlTableCell;
                    gangwei.Visible = false;
                }
                if (cbxKaoqin.Checked)
                {
                    HtmlTableCell YCQHJ = e.Item.FindControl("tdYCQHJ") as HtmlTableCell;
                    HtmlTableCell JRwork = e.Item.FindControl("tdJRwork") as HtmlTableCell;
                    HtmlTableCell Zhouwork = e.Item.FindControl("tdZhouwork") as HtmlTableCell;
                    HtmlTableCell Riwork = e.Item.FindControl("tdRiwork") as HtmlTableCell;

                    HtmlTableCell Yeban = e.Item.FindControl("tdYeban") as HtmlTableCell;

                    HtmlTableCell Bingjia = e.Item.FindControl("tdBingjia") as HtmlTableCell;
                    HtmlTableCell Shijia = e.Item.FindControl("tdShijia") as HtmlTableCell;
                    HtmlTableCell Nianjia = e.Item.FindControl("tdNianjia") as HtmlTableCell;
                    YCQHJ.Visible = false;
                    JRwork.Visible = false;
                    Zhouwork.Visible = false;
                    Bingjia.Visible = false;
                    Yeban.Visible = false;
                    Shijia.Visible = false;
                    Nianjia.Visible = false;
                    Riwork.Visible = false;
                }
                if (cbxWXYJ.Checked)
                {
                    HtmlTableCell YangLBX = e.Item.FindControl("tdYangLBX") as HtmlTableCell;
                    HtmlTableCell SYBX = e.Item.FindControl("tdSYBX") as HtmlTableCell;
                    HtmlTableCell YiLBX = e.Item.FindControl("tdYiLBX") as HtmlTableCell;
                    HtmlTableCell DEJiuZhu = e.Item.FindControl("tdDEJiuZhu") as HtmlTableCell;
                    HtmlTableCell BuBX = e.Item.FindControl("tdBuBX") as HtmlTableCell;
                    HtmlTableCell GJJ = e.Item.FindControl("tdGJJ") as HtmlTableCell;
                    HtmlTableCell BGJJ = e.Item.FindControl("tdBGJJ") as HtmlTableCell;
                    YangLBX.Visible = false;
                    SYBX.Visible = false;
                    YiLBX.Visible = false;
                    DEJiuZhu.Visible = false;
                    BuBX.Visible = false;
                    GJJ.Visible = false;
                    BGJJ.Visible = false;
                }
            }


            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (cbxBumen.Checked)
                {
                    HtmlTableCell tdQD_BuMen = e.Item.FindControl("tdQD_BuMen") as HtmlTableCell;
                    tdQD_BuMen.Visible = false;
                }
                if (cbxbanzu.Checked)
                {
                    HtmlTableCell tdQD_BanZu = e.Item.FindControl("tdQD_BanZu") as HtmlTableCell;
                    tdQD_BanZu.Visible = false;
                }
                if (cbxgh.Checked)
                {
                    HtmlTableCell workno = e.Item.FindControl("tdQD_Worknumber") as HtmlTableCell;
                    workno.Visible = false;
                }
                if (cbxgw.Checked)
                {
                    HtmlTableCell gangw = e.Item.FindControl("tdQD_GangWe") as HtmlTableCell;
                    gangw.Visible = false;
                }
                if (cbxKaoqin.Checked)
                {
                    HtmlTableCell tdKQ_CHUQIN = e.Item.FindControl("tdKQ_CHUQIN") as HtmlTableCell;
                    HtmlTableCell tdKQ_JRJIAB = e.Item.FindControl("tdKQ_JRJIAB") as HtmlTableCell;
                    HtmlTableCell tdKQ_ZMJBAN = e.Item.FindControl("tdKQ_ZMJBAN") as HtmlTableCell;
                    HtmlTableCell tdKQ_YSGZ = e.Item.FindControl("tdKQ_YSGZ") as HtmlTableCell;
                    HtmlTableCell tdKQ_YEBAN = e.Item.FindControl("tdKQ_YEBAN") as HtmlTableCell;
                    HtmlTableCell tdKQ_BINGJ = e.Item.FindControl("tdKQ_BINGJ") as HtmlTableCell;
                    HtmlTableCell tdKQ_SHIJ = e.Item.FindControl("tdKQ_SHIJ") as HtmlTableCell;
                    HtmlTableCell tdKQ_NIANX = e.Item.FindControl("tdKQ_NIANX") as HtmlTableCell;
                    tdKQ_CHUQIN.Visible = false;
                    tdKQ_JRJIAB.Visible = false;
                    tdKQ_ZMJBAN.Visible = false;
                    tdKQ_YSGZ.Visible = false;
                    tdKQ_YEBAN.Visible = false;
                    tdKQ_BINGJ.Visible = false;
                    tdKQ_SHIJ.Visible = false;
                    tdKQ_NIANX.Visible = false;
                }
                if (cbxWXYJ.Checked)
                {
                    HtmlTableCell tdQD_YLBX = e.Item.FindControl("tdQD_YLBX") as HtmlTableCell;
                    HtmlTableCell tdQD_SYBX = e.Item.FindControl("tdQD_SYBX") as HtmlTableCell;
                    HtmlTableCell tdQD_YiLiaoBX = e.Item.FindControl("tdQD_YiLiaoBX") as HtmlTableCell;
                    HtmlTableCell tdQD_DEJZ = e.Item.FindControl("tdQD_DEJZ") as HtmlTableCell;
                    HtmlTableCell tdQD_BuBX = e.Item.FindControl("tdQD_BuBX") as HtmlTableCell;
                    HtmlTableCell tdQD_GJJ = e.Item.FindControl("tdQD_GJJ") as HtmlTableCell;
                    HtmlTableCell tdQD_BGJJ = e.Item.FindControl("tdQD_BGJJ") as HtmlTableCell;
                    tdQD_YLBX.Visible = false;
                    tdQD_SYBX.Visible = false;
                    tdQD_YiLiaoBX.Visible = false;
                    tdQD_DEJZ.Visible = false;
                    tdQD_BuBX.Visible = false;
                    tdQD_GJJ.Visible = false;
                    tdQD_BGJJ.Visible = false;
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                if (dt000.Rows.Count > 0)
                {
                    System.Web.UI.WebControls.Label lb_QD_JCGZhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_JCGZhj");

                    System.Web.UI.WebControls.Label lb_QD_GZGLhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_GZGLhj");
                    System.Web.UI.WebControls.Label lb_QD_GDGZhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_GDGZhj");
                    System.Web.UI.WebControls.Label lb_QD_JXGZhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_JXGZhj");
                    System.Web.UI.WebControls.Label lb_QD_JiangLihj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_JiangLihj");
                    System.Web.UI.WebControls.Label lb_QD_BingJiaGZhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_BingJiaGZhj");
                    System.Web.UI.WebControls.Label lb_QD_JiaBanGZhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_JiaBanGZhj");
                    System.Web.UI.WebControls.Label lb_QD_BFJBhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_BFJBhj");
                    System.Web.UI.WebControls.Label lb_QD_ZYBFhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_ZYBFhj");
                    System.Web.UI.WebControls.Label lb_QD_BFZYBhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_BFZYBhj");
                    System.Web.UI.WebControls.Label lb_QD_NianJiaGZhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_NianJiaGZhj");
                    System.Web.UI.WebControls.Label lb_QD_YKGWhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_YKGWhj");
                    System.Web.UI.WebControls.Label lb_QD_TZBFhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_TZBFhj");
                    System.Web.UI.WebControls.Label lb_QD_TZBKhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_TZBKhj");
                    System.Web.UI.WebControls.Label lb_QD_JTBThj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_JTBThj");
                    System.Web.UI.WebControls.Label lb_QD_FSJWhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_FSJWhj");
                    System.Web.UI.WebControls.Label lb_QD_CLBThj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_CLBThj");
                    System.Web.UI.WebControls.Label lb_QD_QTFYhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_QTFYhj");
                    System.Web.UI.WebControls.Label lb_QD_YFHJhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_YFHJhj");
                    System.Web.UI.WebControls.Label lb_QD_YLBXhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_YLBXhj");
                    System.Web.UI.WebControls.Label lb_QD_SYBXhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_SYBXhj");
                    System.Web.UI.WebControls.Label lb_QD_YiLiaoBXhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_YiLiaoBXhj");
                    System.Web.UI.WebControls.Label lb_QD_DEJZhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_DEJZhj");
                    System.Web.UI.WebControls.Label lb_QD_BuBXhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_BuBXhj");
                    System.Web.UI.WebControls.Label lb_QD_GJJhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_GJJhj");
                    System.Web.UI.WebControls.Label lb_QD_BGJJhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_BGJJhj");
                    System.Web.UI.WebControls.Label lb_QD_ShuiDianhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_ShuiDianhj");
                    System.Web.UI.WebControls.Label lb_QD_GeShuihj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_GeShuihj");
                    System.Web.UI.WebControls.Label lb_QD_DaiKouXJhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_DaiKouXJhj");
                    System.Web.UI.WebControls.Label lb_QD_ShiFaJEhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_ShiFaJEhj");
                    System.Web.UI.WebControls.Label lb_QD_KOUSJShj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_QD_KOUSJShj");
                    lb_QD_JCGZhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_JCGZhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_GZGLhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_GZGLhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_GDGZhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_GDGZhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_JXGZhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_JXGZhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_JiangLihj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_JiangLihj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_BingJiaGZhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_BingJiaGZhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_JiaBanGZhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_JiaBanGZhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_BFJBhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_BFJBhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_ZYBFhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_ZYBFhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_BFZYBhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_BFZYBhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_NianJiaGZhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_NianJiaGZhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_YKGWhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_YKGWhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_TZBFhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_TZBFhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_TZBKhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_TZBKhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_JTBThj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_JTBThj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_FSJWhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_FSJWhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_CLBThj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_CLBThj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_QTFYhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_QTFYhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_YFHJhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_YFHJhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_YLBXhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_YLBXhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_SYBXhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_SYBXhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_YiLiaoBXhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_YiLiaoBXhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_DEJZhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_DEJZhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_BuBXhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_BuBXhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_GJJhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_GJJhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_BGJJhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_BGJJhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_ShuiDianhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_ShuiDianhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_GeShuihj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_GeShuihj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_DaiKouXJhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_DaiKouXJhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_ShiFaJEhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_ShiFaJEhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_QD_KOUSJShj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_KOUSJShj"].ToString().Trim()), 2)).ToString().Trim();

                }
                if (cbxBumen.Checked)
                {
                    HtmlTableCell tdfootbm = e.Item.FindControl("tdfootbm") as HtmlTableCell;
                    tdfootbm.Visible = false;
                }

                if (cbxbanzu.Checked)
                {
                    HtmlTableCell tdfootbz = e.Item.FindControl("tdfootbz") as HtmlTableCell;
                    tdfootbz.Visible = false;
                }
                if (cbxgh.Checked)
                {
                    HtmlTableCell tdfoot1 = e.Item.FindControl("tdfoot1") as HtmlTableCell;
                    tdfoot1.Visible = false;
                }
                if (cbxgw.Checked)
                {
                    HtmlTableCell tdfootgw = e.Item.FindControl("tdfootgw") as HtmlTableCell;
                    tdfootgw.Visible = false;
                }
                if (cbxKaoqin.Checked)
                {
                    HtmlTableCell tdfoot2 = e.Item.FindControl("tdfoot2") as HtmlTableCell;
                    tdfoot2.Visible = false;
                }
                if (cbxWXYJ.Checked)
                {
                    HtmlTableCell tdQD_YLBXhj = e.Item.FindControl("tdQD_YLBXhj") as HtmlTableCell;
                    HtmlTableCell tdQD_SYBXhj = e.Item.FindControl("tdQD_SYBXhj") as HtmlTableCell;
                    HtmlTableCell tdQD_YiLiaoBXhj = e.Item.FindControl("tdQD_YiLiaoBXhj") as HtmlTableCell;
                    HtmlTableCell tdQD_DEJZhj = e.Item.FindControl("tdQD_DEJZhj") as HtmlTableCell;
                    HtmlTableCell tdQD_BuBXhj = e.Item.FindControl("tdQD_BuBXhj") as HtmlTableCell;
                    HtmlTableCell tdQD_GJJhj = e.Item.FindControl("tdQD_GJJhj") as HtmlTableCell;
                    HtmlTableCell tdQD_BGJJhj = e.Item.FindControl("tdQD_BGJJhj") as HtmlTableCell;
                    tdQD_YLBXhj.Visible = false;
                    tdQD_SYBXhj.Visible = false;
                    tdQD_YiLiaoBXhj.Visible = false;
                    tdQD_DEJZhj.Visible = false;
                    tdQD_BuBXhj.Visible = false;
                    tdQD_GJJhj.Visible = false;
                    tdQD_BGJJhj.Visible = false;
                }
            }
        }
        #endregion



        #region 全选，连选、取消
        /// <summary>
        /// 全选、反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cbxselectall_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxselectall.Checked)
            {
                foreach (RepeaterItem Reitem in rptGZQD.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("cbxNumber") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        if (cbx.Enabled != false)
                        {
                            cbx.Checked = true;
                        }
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Reitem in rptGZQD.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("cbxNumber") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = false;
                    }
                }
            }
        }
        /// <summary>
        /// 连选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLX_Onclick(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;
            int start = 0;
            int finish = 0;
            int k = 0;
            foreach (RepeaterItem Reitem in rptGZQD.Items)
            {
                j++;
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("cbxNumber") as System.Web.UI.WebControls.CheckBox;
                if (cbx.Checked)
                {
                    i++;
                    if (start == 0)
                    {
                        start = j;
                    }
                    else
                    {
                        finish = j;
                    }
                }
            }
            if (i == 2)
            {
                foreach (RepeaterItem Reitem in rptGZQD.Items)
                {
                    k++;
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("cbxNumber") as System.Web.UI.WebControls.CheckBox;
                    if (k >= start && k <= finish)
                    {
                        if (cbx.Enabled == true)
                        {
                            cbx.Checked = true;
                        }

                    }
                    if (k > finish)
                    {
                        cbx.Checked = false;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择连续的区间！');", true);
            }

        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQX_Onclick(object sender, EventArgs e)
        {
            foreach (RepeaterItem Reitem in rptGZQD.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("cbxNumber") as System.Web.UI.WebControls.CheckBox;
                cbx.Checked = false;
            }
        }
        #endregion

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDaochu_OnClick(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext = "select QD_YEARMONTH,";
            sqltext += "ST_WORKNO,";
            sqltext += "ST_NAME,";
            sqltext += "QD_HTZT,";
            sqltext += "QD_QFBS,";
            sqltext += "DEP_NAME,";
            sqltext += "ST_DEPID1,";
            sqltext += "DEP_NAME_POSITION,";
            sqltext += "KQ_CHUQIN,";
            sqltext += "KQ_JRJIAB,";
            sqltext += "KQ_ZMJBAN,";
            sqltext += "KQ_YSGZ,";
            sqltext += "KQ_YEBAN,";
            sqltext += "KQ_BINGJ,";
            sqltext += "KQ_SHIJ,";
            sqltext += "KQ_NIANX,";
            sqltext += "QD_JCGZ,";
            sqltext += "QD_GZGL,";
            sqltext += "QD_GDGZ,";
            sqltext += "QD_JXGZ,";
            sqltext += "QD_JiangLi,";
            sqltext += "QD_BingJiaGZ,";
            sqltext += "QD_JiaBanGZ,";
            sqltext += "QD_ZYBF,";
            sqltext += "QD_NianJiaGZ,";
            sqltext += "QD_YKGW,";
            sqltext += "QD_TZBF,";
            sqltext += "QD_TZBK,";
            sqltext += "QD_JTBT,";
            sqltext += "QD_FSJW,";
            sqltext += "QD_CLBT,";
            sqltext += "QD_QTFY,";
            sqltext += "(QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY) as QD_YFHJ,";
            sqltext += "QD_YLBX,";
            sqltext += "QD_SYBX,";
            sqltext += "QD_YiLiaoBX,";
            sqltext += "QD_DEJZ,";
            sqltext += "QD_BuBX,";
            sqltext += "QD_GJJ,";
            sqltext += "QD_BGJJ,";
            sqltext += "QD_ShuiDian,";
            sqltext += "QD_KOUXIANG,";
            sqltext += "QD_GeShui,";
            sqltext += "(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG) as QD_DaiKouXJ,";
            sqltext += "((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG)) as QD_ShiFaJE,";
            sqltext += "QD_NOTE";
            sqltext += " from (select * from View_OM_GZQD left join OM_KQTJ on (View_OM_GZQD.QD_STID=OM_KQTJ.KQ_ST_ID and View_OM_GZQD.QD_YEARMONTH=OM_KQTJ.KQ_DATE))t where " + StrWhere() + " order by DEP_NAME,ST_WORKNO,QD_YEARMONTH asc";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }

        private void ExportDataItem(System.Data.DataTable objdt)
        {
            string filename = "工资清单" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("工资清单.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2);
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));
                    row.CreateCell(1).SetCellValue(objdt.Rows[i]["QD_YEARMONTH"].ToString().Trim());
                    row.CreateCell(2).SetCellValue(objdt.Rows[i]["ST_WORKNO"].ToString().Trim());
                    row.CreateCell(3).SetCellValue(objdt.Rows[i]["ST_NAME"].ToString().Trim());
                    row.CreateCell(4).SetCellValue(objdt.Rows[i]["QD_HTZT"].ToString().Trim());

                    row.CreateCell(5).SetCellValue(objdt.Rows[i]["QD_QFBS"].ToString().Trim());
                    row.CreateCell(6).SetCellValue(objdt.Rows[i]["DEP_NAME"].ToString().Trim());
                    row.CreateCell(7).SetCellValue(objdt.Rows[i]["ST_DEPID1"].ToString().Trim());
                    row.CreateCell(8).SetCellValue(objdt.Rows[i]["DEP_NAME_POSITION"].ToString().Trim());
                    row.CreateCell(9).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["KQ_CHUQIN"].ToString().Trim()));
                    row.CreateCell(10).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["KQ_JRJIAB"].ToString().Trim()));
                    row.CreateCell(11).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["KQ_ZMJBAN"].ToString().Trim()));
                    row.CreateCell(12).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["KQ_YSGZ"].ToString().Trim()));

                    row.CreateCell(13).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["KQ_YEBAN"].ToString().Trim()));

                    row.CreateCell(14).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["KQ_BINGJ"].ToString().Trim()));
                    row.CreateCell(15).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["KQ_SHIJ"].ToString().Trim()));
                    row.CreateCell(16).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["KQ_NIANX"].ToString().Trim()));
                    row.CreateCell(17).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_JCGZ"].ToString().Trim()));
                    row.CreateCell(18).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_GZGL"].ToString().Trim()));
                    row.CreateCell(19).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_GDGZ"].ToString().Trim()));
                    row.CreateCell(20).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_JXGZ"].ToString().Trim()));
                    row.CreateCell(21).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_JiangLi"].ToString().Trim()));
                    row.CreateCell(22).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_BingJiaGZ"].ToString().Trim()));
                    row.CreateCell(23).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_JiaBanGZ"].ToString().Trim()));
                    row.CreateCell(24).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_ZYBF"].ToString().Trim()));
                    row.CreateCell(25).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_NianJiaGZ"].ToString().Trim()));
                    row.CreateCell(26).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_YKGW"].ToString().Trim()));
                    row.CreateCell(27).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_TZBF"].ToString().Trim()));
                    row.CreateCell(28).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_TZBK"].ToString().Trim()));
                    row.CreateCell(29).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_JTBT"].ToString().Trim()));
                    row.CreateCell(30).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_FSJW"].ToString().Trim()));
                    row.CreateCell(31).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_CLBT"].ToString().Trim()));
                    row.CreateCell(32).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_QTFY"].ToString().Trim()));
                    row.CreateCell(33).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_YFHJ"].ToString().Trim()));//应发合计
                    row.CreateCell(34).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_YLBX"].ToString().Trim()));
                    row.CreateCell(35).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_SYBX"].ToString().Trim()));
                    row.CreateCell(36).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_YiLiaoBX"].ToString().Trim()));
                    row.CreateCell(37).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_DEJZ"].ToString().Trim()));
                    row.CreateCell(38).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_BuBX"].ToString().Trim()));
                    row.CreateCell(39).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_GJJ"].ToString().Trim()));
                    row.CreateCell(40).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_BGJJ"].ToString().Trim()));
                    row.CreateCell(41).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_ShuiDian"].ToString().Trim()));
                    row.CreateCell(42).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_KOUXIANG"].ToString().Trim()));
                    row.CreateCell(43).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_GeShui"].ToString().Trim()));
                    row.CreateCell(44).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_DaiKouXJ"].ToString().Trim()));//代扣小计
                    row.CreateCell(45).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_ShiFaJE"].ToString().Trim()));//实发金额
                    row.CreateCell(46).SetCellValue(objdt.Rows[i]["QD_NOTE"].ToString().Trim());
                }

                for (int i = 0; i <= objdt.Columns.Count; i++)
                {
                    sheet0.AutoSizeColumn(i);
                }
                sheet0.ForceFormulaRecalculation = true;

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }

        //取消
        protected void btnqx_import_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }

        //工资临时导入
        protected void btnimport_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string sqlinsert = "";
            string strinsert = "";
            string getbh = "0" + (tb_yearmonth.Text.ToString().Trim()).Substring(5, 2).ToString().Trim();
            if (tb_yearmonth.Text.ToString().Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择起止年月！！！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }
            else if (tb_yearmonth.Text.ToString().Trim() != "")
            {
                string sqldelete1 = "delete from OM_GZQD where QD_YEARMONTH='" + tb_yearmonth.Text.ToString().Trim() + "'";
                string sqldelete2 = "delete from OM_GZQDeditJL where QD_YEARMONTH='" + tb_yearmonth.Text.ToString().Trim() + "'";
                string sqldelete3 = "delete from OM_GZHSB where GZ_YEARMONTH='" + tb_yearmonth.Text.ToString().Trim() + "'";
                DBCallCommon.ExeSqlText(sqldelete1);
                DBCallCommon.ExeSqlText(sqldelete2);
                DBCallCommon.ExeSqlText(sqldelete3);
            }
            string FilePath = @"E:\工资表临时导入\";
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            //将文件上传到服务器
            HttpPostedFile UserHPF = FileUpload1.PostedFile;
            try
            {
                string fileContentType = UserHPF.ContentType;// 获取客户端发送的文件的 MIME 内容类型   
                if (fileContentType == "application/vnd.ms-excel")
                {
                    if (UserHPF.ContentLength > 0)
                    {
                        UserHPF.SaveAs(FilePath + "//" + System.IO.Path.GetFileName(UserHPF.FileName));//将上传的文件存放在指定的文件夹中 
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('文件类型不符合要求，请您核对后重新上传！');", true);
                    ModalPopupExtenderSearch.Hide();
                    return;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('文件上传过程中出现错误！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }

            using (FileStream fs = File.OpenRead(FilePath + "//" + System.IO.Path.GetFileName(UserHPF.FileName)))
            {
                //根据文件流创建一个workbook
                IWorkbook wk = new HSSFWorkbook(fs);

                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 1; i < sheet1.LastRowNum + 1; i++)
                {
                    string gz_stid = "";
                    string gz_htzt = "";
                    string strcellname = "";
                    IRow row = sheet1.GetRow(i);
                    ICell cellname = row.GetCell(3);
                    try
                    {
                        strcellname = cellname.ToString().Trim();
                    }
                    catch
                    {
                        strcellname = "";
                    }
                    if (strcellname != "")
                    {
                        string sqltext = "select ST_ID,ST_CONTR from TBDS_STAFFINFO where ST_NAME='" + strcellname + "'";
                        System.Data.DataTable dttext = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dttext.Rows.Count > 0)
                        {
                            gz_stid = dttext.Rows[0]["ST_ID"].ToString().Trim();
                            gz_htzt = dttext.Rows[0]["ST_CONTR"].ToString().Trim();
                            strinsert = "'" + getbh + "','" + tb_yearmonth.Text.ToString().Trim() + "','" + gz_stid + "','" + gz_htzt + "',";
                            ICell cellqfbs = row.GetCell(4);
                            strinsert += "'" + cellqfbs.ToString().Trim() + "',";
                            ICell celljcgz = row.GetCell(14);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(celljcgz.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellglgz = row.GetCell(15);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellglgz.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellgwgz = row.GetCell(16);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellgwgz.NumericCellValue.ToString().Trim()), 2) + ",";

                            ICell celljxgz = row.GetCell(17);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(celljxgz.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellqt = row.GetCell(18);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellqt.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellbjgz = row.GetCell(19);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellbjgz.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell celljbgz = row.GetCell(20);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(celljbgz.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellbfjb = row.GetCell(21);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellbfjb.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellzybf = row.GetCell(22);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellzybf.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellbfzybf = row.GetCell(23);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellbfzybf.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellnjgz = row.GetCell(24);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellnjgz.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellykgw = row.GetCell(25);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellykgw.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell celltzbf = row.GetCell(27);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(celltzbf.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell celltzbk = row.GetCell(28);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(celltzbk.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell celljtbt = row.GetCell(29);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(celljtbt.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellfsjw = row.GetCell(30);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellfsjw.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellclbt = row.GetCell(31);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellclbt.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellyanglbx = row.GetCell(33);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellyanglbx.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellsiyebx = row.GetCell(34);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellsiyebx.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellyiliaobx = row.GetCell(35);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellyiliaobx.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell celldejz = row.GetCell(36);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(celldejz.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellbbx = row.GetCell(37);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellbbx.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellgjj = row.GetCell(38);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellgjj.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellbgjj = row.GetCell(39);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellbgjj.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellfzsd = row.GetCell(40);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellfzsd.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellkouxiang = row.GetCell(41);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellkouxiang.NumericCellValue.ToString().Trim()), 2) + ",";
                            ICell cellgeshui = row.GetCell(42);
                            strinsert += Math.Round(CommonFun.ComTryDecimal(cellgeshui.NumericCellValue.ToString().Trim()), 2) + "";

                            sqlinsert = "insert into OM_GZQD(QD_SHBH,QD_YEARMONTH,QD_STID,QD_HTZT,QD_QFBS,QD_JCGZ,QD_GZGL,QD_GDGZ,QD_JXGZ,QD_QTFY,QD_BingJiaGZ,QD_JiaBanGZ,QD_BFJB,QD_ZYBF,QD_BFZYB,QD_NianJiaGZ,QD_YKGW,QD_TZBF,QD_TZBK,QD_JTBT,QD_FSJW, QD_CLBT,QD_YLBX,QD_SYBX,QD_YiLiaoBX,QD_DEJZ,QD_BuBX,QD_GJJ,QD_BGJJ,QD_ShuiDian,QD_KOUXIANG,QD_GeShui) values(" + strinsert + ")";
                            list.Add(sqlinsert);
                        } 
                    }
                    else
                    {
                        break;
                    }
                }
                string sqlinsertgzhsb = "insert into OM_GZHSB(GZSH_BH,OM_GZSCBZ,GZSCTIME,GZ_YEARMONTH,GZ_SCRID,GZ_SCRNAME,GZ_SHRID1,GZ_SHRNAME1,GZ_SHSTATE1,GZ_SHTIME1,GZ_SHJS) values('" + getbh + "','1','2015-08-25 12:00:00','" + tb_yearmonth.Text.ToString().Trim() + "','151','李圆','3','蔡伟疆','1','2015-08-25 12:00:00','1')";
                list.Add(sqlinsertgzhsb);
            }
            DBCallCommon.ExecuteTrans(list);
            foreach (string fileName in Directory.GetFiles(FilePath))//清空该文件夹下的文件
            {
                string newName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                System.IO.File.Delete(FilePath + "\\" + newName);//删除文件下储存的文件
            }
            UCPaging1.CurrentPage = 1;
            bindrpt();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导入成功！！！');", true);
        }
    }
}
