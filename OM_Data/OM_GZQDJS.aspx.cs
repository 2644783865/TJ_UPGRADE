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
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_GZQDJS : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserDeptID"].ToString().Trim() != "02" && Session["UserDeptID"].ToString().Trim() != "01")
                {
                    btnscgzqd.Visible = false;
                    btngztz.Visible = false;
                }
                BindbmData();
                bindbz();
                bindgw();
                GetSele();
                this.BindYearMoth(ddlYear, ddlMonth);
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindrpt();
                jsgztx();//计算工资提醒
            }
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
            string sql = "1=1";
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
            if (radio_weish.Checked == true)
            {
                sql += " and (OM_GZSCBZ is null or OM_GZSCBZ='')";
            }
            if (radio_shenhz.Checked == true)
            {
                sql += " and OM_GZSCBZ='0'";
            }
            if (radio_yitg.Checked == true)
            {
                sql += " and OM_GZSCBZ='1'";
            }
            if (radio_boh.Checked == true)
            {
                sql += " and OM_GZSCBZ='2'";
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
            jsgztx();//计算工资提醒

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
            jsgztx();//计算工资提醒
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


        protected void radio_CheckedChanged(object sender, EventArgs e)
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
                    lb_QD_GZGLhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_GZGLhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_GDGZhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_GDGZhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_JXGZhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_JXGZhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_JiangLihj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_JiangLihj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_BingJiaGZhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_BingJiaGZhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_JiaBanGZhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_JiaBanGZhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_BFJBhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_BFJBhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_ZYBFhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_ZYBFhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_BFZYBhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_BFZYBhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_NianJiaGZhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_NianJiaGZhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_YKGWhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_YKGWhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_TZBFhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_TZBFhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_TZBKhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_TZBKhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_JTBThj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_JTBThj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_FSJWhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_FSJWhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_CLBThj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_CLBThj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_QTFYhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_QTFYhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_YFHJhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_YFHJhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_YLBXhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_YLBXhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_SYBXhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_SYBXhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_YiLiaoBXhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_YiLiaoBXhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_DEJZhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_DEJZhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_BuBXhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_BuBXhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_GJJhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_GJJhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_BGJJhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_BGJJhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_ShuiDianhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_ShuiDianhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_GeShuihj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_GeShuihj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_DaiKouXJhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_DaiKouXJhj"].ToString().Trim()), 2)).ToString().Trim();
                    lb_QD_ShiFaJEhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_ShiFaJEhj"].ToString().Trim()), 2)).ToString().Trim();
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


        //生成工资清单
        protected void btnscgzqd_OnClick(object sender, EventArgs e)
        {
            if (ddlYear.SelectedIndex == 0 || ddlMonth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！');", true);
                return;
            }



            //获取保险公积金所取年月份
            string wxyjyear = "";
            string wxyjmonth = "";
            try
            {
                if (CommonFun.ComTryInt(ddlMonth.SelectedValue.ToString().Trim()) == 12)
                {
                    wxyjyear = (CommonFun.ComTryInt(ddlYear.SelectedValue.ToString().Trim()) + 1).ToString().Trim();
                    wxyjmonth = "01";
                }
                else
                {
                    wxyjyear = ddlYear.SelectedValue.ToString().Trim();
                    wxyjmonth = (CommonFun.ComTryInt(ddlMonth.SelectedValue.ToString().Trim()) + 1).ToString("00").Trim();
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('获取年月份出错！');", true);
                return;
            }



            List<string> list = new List<string>();
            string strspbh = "";
            string strdataifall = "";
            //判断当月是否有数据在审核中或已通过，若有则不允许再生成
            string sqlifcz = "select * from View_OM_GZQD where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and (OM_GZSCBZ='0' or OM_GZSCBZ='1')";
            System.Data.DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifcz);
            if (dtifsc.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月工资清单正在审核或已审核通过！');", true);
                return;
            }
            //判断是否有数据未提交，若有则删除原有数据(重新生成时可能导致工资清单编号出现修改记录中的一种和重新生成的一种)
            string sqltext = "select * from View_OM_GZQD where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and (OM_GZSCBZ='' or OM_GZSCBZ is null)";
            System.Data.DataTable dttext = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dttext.Rows.Count > 0)
            {
                string sqldelete0 = "delete from OM_GZQD where QD_STID not in(select QD_STID from OM_GZQDeditJL where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "') and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and (QD_SHBH in(select GZSH_BH from OM_GZHSB where (OM_GZSCBZ is null or OM_GZSCBZ='') and GZ_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "'))";
                string sqldelete1 = "delete from OM_GZHSB where GZ_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and (OM_GZSCBZ='' or OM_GZSCBZ is null)";
                DBCallCommon.ExeSqlText(sqldelete0);
                DBCallCommon.ExeSqlText(sqldelete1);
            }
            //判断数据是否完整，若不完整则提示
            //1.固定工资
            string sqlifgdgz = "select * from OM_GDGZ where ST_ID not in(select QD_STID from OM_GZQDeditJL where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "')";
            System.Data.DataTable dtifgdgz = DBCallCommon.GetDTUsingSqlText(sqlifgdgz);
            if (dtifgdgz.Rows.Count == 0)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请添加固定工资数据！');", true);
                //return;
                strdataifall += "固定工资,";
            }
            //2.考勤
            string sqlifkq = "select * from OM_KQTJ where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "'";
            System.Data.DataTable dtifkq = DBCallCommon.GetDTUsingSqlText(sqlifkq);
            if (dtifkq.Rows.Count == 0)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请添加考勤数据！');", true);
                //return;
                strdataifall += "考勤数据,";
            }
            //3.生产操作
            string sqlifsccz = "select * from OM_SCCZSH_TOTAL where SCCZTOL_NY='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and SCCZTOL_TOLSTATE='2' and TOLTYPE='0'";
            string sqlifsccz2 = "select * from OM_SCCZSH_TOTAL where SCCZTOL_NY='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and SCCZTOL_TOLSTATE='2' and TOLTYPE='1'";
            System.Data.DataTable dtifsccz = DBCallCommon.GetDTUsingSqlText(sqlifsccz);
            System.Data.DataTable dtifsccz2 = DBCallCommon.GetDTUsingSqlText(sqlifsccz2);
            if (dtifsccz.Rows.Count == 0 || dtifsccz2.Rows.Count == 0)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('缺少生产操作数据或数据未审核通过！');", true);
                //return;
                strdataifall += "班组绩效工资,";
            }
            //4.部门绩效
            string sqlifbmjx = "select * from TBDS_KaoHe_JXList where Year='" + ddlYear.SelectedValue.ToString().Trim() + "' and Month='" + ddlMonth.SelectedValue.ToString().Trim() + "' and State='2'";
            System.Data.DataTable dtifbmjx = DBCallCommon.GetDTUsingSqlText(sqlifbmjx);
            if (dtifbmjx.Rows.Count == 0)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('缺少部门绩效数据或数据未审核通过！');", true);
                //return;
                strdataifall += "各部门绩效,";
            }
            //5.社会保险
            string sqlifshbx = "select * from OM_SHBX where SH_DATE='" + wxyjyear + "-" + wxyjmonth + "'";
            System.Data.DataTable dtifshbx = DBCallCommon.GetDTUsingSqlText(sqlifshbx);
            if (dtifshbx.Rows.Count == 0)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请导入本月社会保险数据！');", true);
                //return;
                strdataifall += "社会保险,";
            }
            //6.公积金
            string sqlifgjj = "select * from OM_GJJ where GJ_DATE='" + wxyjyear + "-" + wxyjmonth + "'";
            System.Data.DataTable dtifgjj = DBCallCommon.GetDTUsingSqlText(sqlifgjj);
            if (dtifgjj.Rows.Count == 0)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请导入本月公积金数据！');", true);
                //return;
                strdataifall += "公积金,";
            }
            //7.派遣保险公积金
            string sqlifldbx = "select * from OM_LDBX where LD_DATE='" + wxyjyear + "-" + wxyjmonth + "'";
            System.Data.DataTable dtifldbx = DBCallCommon.GetDTUsingSqlText(sqlifldbx);
            if (dtifldbx.Rows.Count == 0)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请导入本月派遣人员保险公积金数据！');", true);
                //return;
                strdataifall += "派遣保险公积金.";
            }
            //插入人员信息和审核单据
            string sqlgetstaff = "select * from TBDS_STAFFINFO where ST_ID not in(select QD_STID from OM_GZQDeditJL where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "') and ST_NAME!='' and ST_NAME!='-' and ST_NAME is not null and (ST_ID in(select ST_ID from OM_GDGZ) or ST_ID in(select GJ_STID from OM_GJJ where GJ_DATE='" + wxyjyear + "-" + wxyjmonth + "') or ST_ID in(select LD_STID from OM_LDBX where LD_DATE='" + wxyjyear + "-" + wxyjmonth + "') or ST_ID in(select SH_STID from OM_SHBX where SH_DATE='" + wxyjyear + "-" + wxyjmonth + "') or ST_ID in(select FZBZ_STID from OM_SCCZ_FZBZ where FZBZ_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "') or ST_ID in(select YIXIAN_ST_ID from OM_SCCZ_YIXIAN where YIXIAN_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "') or ST_ID in(select ST_ID from TBDS_KaoHe_JXList as a left join TBDS_KaoHe_JXDetail as b on a.ConText=b.Context where Year='" + ddlYear.SelectedValue.ToString().Trim() + "' and Month='" + ddlMonth.SelectedValue.ToString().Trim() + "') or ST_ID in(select KQ_ST_ID from OM_KQTJ where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "')) and ST_ID not in(select ST_ID from View_OM_GDGZ where ST_PD='1' and ST_ID not in(select KQ_ST_ID from OM_KQTJ where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "'))";
            System.Data.DataTable dtgetstaff = DBCallCommon.GetDTUsingSqlText(sqlgetstaff);
            if (dtgetstaff.Rows.Count > 0)
            {
                string sqlzdbh = "select max(GZSH_BH) as GZSH_BHMAX from OM_GZHSB";
                System.Data.DataTable dtzdbh = DBCallCommon.GetDTUsingSqlText(sqlzdbh);
                if (dtzdbh.Rows.Count > 0 && CommonFun.ComTryInt(dtzdbh.Rows[0]["GZSH_BHMAX"].ToString().Trim()) > 0)
                {
                    strspbh = (CommonFun.ComTryInt(dtzdbh.Rows[0]["GZSH_BHMAX"].ToString().Trim()) + 1).ToString("000").Trim();
                }
                else
                {
                    strspbh = "001";
                }
                List<string> listgetstaff = new List<string>();
                string sqlinsertsh = "insert into OM_GZHSB(GZSH_BH,GZSCTIME,GZ_YEARMONTH,GZ_SCRID,GZ_SCRNAME) values('" + strspbh + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "')";
                for (int i = 0; i < dtgetstaff.Rows.Count; i++)
                {
                    string sqlinsertstaff = "insert into OM_GZQD(QD_SHBH,QD_YEARMONTH,QD_STID,QD_HTZT) values('" + strspbh + "','" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "','" + dtgetstaff.Rows[i]["ST_ID"].ToString().Trim() + "','" + dtgetstaff.Rows[i]["ST_CONTR"].ToString().Trim() + "')";
                    string sqlupdatebh = "update OM_GZQD set QD_SHBH='" + strspbh + "' where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                    listgetstaff.Add(sqlinsertstaff);
                    listgetstaff.Add(sqlupdatebh);
                }
                listgetstaff.Add(sqlinsertsh);
                DBCallCommon.ExecuteTrans(listgetstaff);
            }
            //插入已有数据
            List<string> listinsert = new List<string>();
            //1.固定工资（总是更新全部）
            for (int a = 0; a < dtifgdgz.Rows.Count; a++)
            {
                string insertgdgz = "update OM_GZQD set QD_GDGZ=" + Math.Round((CommonFun.ComTryDecimal(dtifgdgz.Rows[a]["GDGZ"].ToString().Trim())), 2) + " where QD_STID='" + dtifgdgz.Rows[a]["ST_ID"].ToString().Trim() + "' and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2') and QD_STID not in(select QD_STID from OM_GZQDeditJL where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "')";//2为被驳回状态
                listinsert.Add(insertgdgz);
            }
            //2.考勤(左联)
            //3.生产操作
            string sqlscczfzbz = "select * from OM_SCCZSH_TOTAL left join OM_SCCZ_FZBZ on SCCZTOL_BH=FZBZ_BH where SCCZTOL_NY='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and SCCZTOL_TOLSTATE='2' and FZBZ_STID not in(select QD_STID from OM_GZQDeditJL where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "')";//辅助班组
            string sqlscczyx = "select * from OM_SCCZSH_TOTAL left join OM_SCCZ_YIXIAN on SCCZTOL_BH=YIXIAN_BH where SCCZTOL_NY='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and SCCZTOL_TOLSTATE='2' and YIXIAN_ST_ID not in(select QD_STID from OM_GZQDeditJL where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "')";//一线
            System.Data.DataTable dtscczfzbz = DBCallCommon.GetDTUsingSqlText(sqlscczfzbz);
            System.Data.DataTable dtscczyx = DBCallCommon.GetDTUsingSqlText(sqlscczyx);
            if (dtscczfzbz.Rows.Count > 0)
            {
                for (int b = 0; b < dtscczfzbz.Rows.Count; b++)
                {
                    string insertfzbz = "update OM_GZQD set QD_GDGZ=" + Math.Round((CommonFun.ComTryDecimal(dtscczfzbz.Rows[b]["FZBZ_GWGZ"].ToString().Trim())), 2) + ",QD_JXGZ=" + Math.Round((CommonFun.ComTryDecimal(dtscczfzbz.Rows[b]["FZBZ_JXGZ"].ToString().Trim())), 2) + ",QD_QTFY=" + Math.Round((CommonFun.ComTryDecimal(dtscczfzbz.Rows[b]["FZBZ_QT"].ToString().Trim())), 2) + ",QD_JiaBanGZ=" + Math.Round((CommonFun.ComTryDecimal(dtscczfzbz.Rows[b]["FZBZ_JBGZ"].ToString().Trim())), 2) + " where QD_STID='" + dtscczfzbz.Rows[b]["FZBZ_STID"].ToString().Trim() + "' and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                    listinsert.Add(insertfzbz);
                }
            }
            if (dtscczyx.Rows.Count > 0)
            {
                for (int c = 0; c < dtscczyx.Rows.Count; c++)
                {
                    string insertyx = "update OM_GZQD set QD_GDGZ=" + Math.Round((CommonFun.ComTryDecimal(dtscczyx.Rows[c]["YIXIAN_GWGZ"].ToString().Trim())), 2) + ",QD_JXGZ=" + Math.Round((CommonFun.ComTryDecimal(dtscczyx.Rows[c]["YIXIAN_JXGZ"].ToString().Trim())), 2) + ",QD_QTFY=" + Math.Round((CommonFun.ComTryDecimal(dtscczyx.Rows[c]["YIXIAN_QT"].ToString().Trim())), 2) + " where QD_STID='" + dtscczyx.Rows[c]["YIXIAN_ST_ID"].ToString().Trim() + "' and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                    listinsert.Add(insertyx);
                }
            }
            //4.部门绩效
            string bujixdata = "select a.*,b.ST_ID,b.Money from TBDS_KaoHe_JXList as a left join TBDS_KaoHe_JXDetail as b on a.ConText=b.Context where Year='" + ddlYear.SelectedValue.ToString().Trim() + "' and Month='" + ddlMonth.SelectedValue.ToString().Trim() + "' and State='2' and ST_ID not in(select QD_STID from OM_GZQDeditJL where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "')";
            System.Data.DataTable dtbmjxdata = DBCallCommon.GetDTUsingSqlText(bujixdata);
            if (dtbmjxdata.Rows.Count > 0)
            {
                for (int d = 0; d < dtbmjxdata.Rows.Count; d++)
                {
                    string insertbmjx = "update OM_GZQD set QD_JXGZ=" + Math.Round((CommonFun.ComTryDecimal(dtbmjxdata.Rows[d]["Money"].ToString().Trim())), 2) + " where QD_STID='" + dtbmjxdata.Rows[d]["ST_ID"].ToString().Trim() + "' and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                    listinsert.Add(insertbmjx);
                }
            }




            //保险公积金的数值取下一个月的


            //5.社会保险
            string shehbxdata = "select * from OM_SHBX where SH_DATE='" + wxyjyear + "-" + wxyjmonth + "' and SH_STID not in(select QD_STID from OM_GZQDeditJL where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "')";
            System.Data.DataTable dtshehbxdata = DBCallCommon.GetDTUsingSqlText(shehbxdata);
            if (dtshehbxdata.Rows.Count > 0)
            {
                for (int i = 0; i < dtshehbxdata.Rows.Count; i++)
                {
                    string insertshehbx = "update OM_GZQD set QD_YLBX=" + CommonFun.ComTryDecimal(dtshehbxdata.Rows[i]["SH_QYYLGR"].ToString().Trim()) + ",QD_SYBX=" + CommonFun.ComTryDecimal(dtshehbxdata.Rows[i]["SH_SYBXGR"].ToString().Trim()) + ",QD_YiLiaoBX=" + CommonFun.ComTryDecimal(dtshehbxdata.Rows[i]["SH_JBYLGR"].ToString().Trim()) + ",QD_DEJZ=" + CommonFun.ComTryDecimal(dtshehbxdata.Rows[i]["SH_DEYLGR"].ToString().Trim()) + ",QD_BuBX=" + CommonFun.ComTryDecimal(dtshehbxdata.Rows[i]["SH_QYYLGRB"].ToString().Trim()) + "+" + CommonFun.ComTryDecimal(dtshehbxdata.Rows[i]["SH_SYBXGRB"].ToString().Trim()) + "+" + CommonFun.ComTryDecimal(dtshehbxdata.Rows[i]["SH_JBYLGRB"].ToString().Trim()) + " where QD_STID='" + dtshehbxdata.Rows[i]["SH_STID"].ToString().Trim() + "' and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                    listinsert.Add(insertshehbx);
                }
            }
            //6.公积金
            string gongjjdata = "select * from OM_GJJ where GJ_DATE='" + wxyjyear + "-" + wxyjmonth + "' and GJ_STID not in(select QD_STID from OM_GZQDeditJL where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "')";
            System.Data.DataTable dtgongjjdata = DBCallCommon.GetDTUsingSqlText(gongjjdata);
            if (dtgongjjdata.Rows.Count > 0)
            {
                for (int j = 0; j < dtgongjjdata.Rows.Count; j++)
                {
                    string insertgongjj = "update OM_GZQD set QD_GJJ=" + CommonFun.ComTryDecimal(dtgongjjdata.Rows[j]["GJ_GR"].ToString().Trim()) + ",QD_BGJJ=" + CommonFun.ComTryDecimal(dtgongjjdata.Rows[j]["GJ_GRB"].ToString().Trim()) + " where QD_STID='" + dtgongjjdata.Rows[j]["GJ_STID"].ToString().Trim() + "' and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                    listinsert.Add(insertgongjj);
                }
            }
            //7.派遣人员保险公积金
            string paiqiandata = "select * from OM_LDBX where LD_DATE='" + wxyjyear + "-" + wxyjmonth + "' and LD_STID not in(select QD_STID from OM_GZQDeditJL where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "')";
            System.Data.DataTable dtpaiqiandata = DBCallCommon.GetDTUsingSqlText(paiqiandata);
            if (dtpaiqiandata.Rows.Count > 0)
            {
                for (int k = 0; k < dtpaiqiandata.Rows.Count; k++)
                {
                    string insertpaiqian = "update OM_GZQD set QD_YLBX=" + CommonFun.ComTryDecimal(dtpaiqiandata.Rows[k]["LD_YLGR"].ToString().Trim()) + ",QD_SYBX=" + CommonFun.ComTryDecimal(dtpaiqiandata.Rows[k]["LD_SYGR"].ToString().Trim()) + ",QD_YiLiaoBX=" + CommonFun.ComTryDecimal(dtpaiqiandata.Rows[k]["LD_JBYLGR"].ToString().Trim()) + ",QD_DEJZ=" + CommonFun.ComTryDecimal(dtpaiqiandata.Rows[k]["LD_YLDE"].ToString().Trim()) + ",QD_GJJ=" + CommonFun.ComTryDecimal(dtpaiqiandata.Rows[k]["LD_GJJGR"].ToString().Trim()) + ",QD_BuBX=" + CommonFun.ComTryDecimal(dtpaiqiandata.Rows[k]["LD_GRBJ"].ToString().Trim()) + " where QD_STID='" + dtpaiqiandata.Rows[k]["LD_STID"].ToString().Trim() + "' and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                    listinsert.Add(insertpaiqian);
                }
            }

            //调整补发/补扣补发加班费及中夜班费
            string tzbfbk = "select GZTZSP_YEARMONTH,GZTZ_STID,sum(GZTZ_BFJBF) as GZTZ_BFJBF,sum(GZTZ_BFZYBF) as GZTZ_BFZYBF,sum(GZTZ_BF) as GZTZ_BF,sum(GZTZ_BK) as GZTZ_BK from OM_GZTZSP as a left join OM_GZTZdetal as b on a.GZTZSP_SPBH=b.GZTZ_SPBH where GZTZ_STID not in(select QD_STID from OM_GZQDeditJL where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "') and GZTZSP_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and TOTALSTATE='2' group by GZTZSP_YEARMONTH,GZTZ_STID";
            System.Data.DataTable dttzbfbk = DBCallCommon.GetDTUsingSqlText(tzbfbk);
            if (dttzbfbk.Rows.Count > 0)
            {
                for (int p = 0; p < dttzbfbk.Rows.Count; p++)
                {
                    string insertbfbk = "update OM_GZQD set QD_BFJB='" + CommonFun.ComTryDecimal(dttzbfbk.Rows[p]["GZTZ_BFJBF"].ToString().Trim()) + "',QD_BFZYB='" + CommonFun.ComTryDecimal(dttzbfbk.Rows[p]["GZTZ_BFZYBF"].ToString().Trim()) + "',QD_TZBF=" + CommonFun.ComTryDecimal(dttzbfbk.Rows[p]["GZTZ_BF"].ToString().Trim()) + ",QD_TZBK=" + CommonFun.ComTryDecimal(dttzbfbk.Rows[p]["GZTZ_BK"].ToString().Trim()) + " where QD_STID='" + dttzbfbk.Rows[p]["GZTZ_STID"].ToString().Trim() + "' and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                    listinsert.Add(insertbfbk);
                }
            }
            //水电费
            string sqlshuidian = " select IDsdmx,peopleid,realmoney from (select IDsdmx,IDSDF,ssrens,ssnum,OM_SDFY.startdate,OM_SDFY.enddate,stratdf,enddf,pricedf,startsf,endsf,pricesf,ST_NAME,note,peopleid,fangjnum,gscddl,gscdsl,realmoney,dairumonth,dairusalary,spbh,state from OM_SDFY left join OM_SDFdetail on (OM_SDFY.ssnum=OM_SDFdetail.fangjnum and OM_SDFY.startdate=OM_SDFdetail.startdate and OM_SDFY.enddate=OM_SDFdetail.enddate) left join TBDS_STAFFINFO on OM_SDFdetail.peopleid=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t where dairumonth='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and state='2' and peopleid not in(select QD_STID from OM_GZQDeditJL where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "')";
            System.Data.DataTable dtshuidian = DBCallCommon.GetDTUsingSqlText(sqlshuidian);
            if (dtshuidian.Rows.Count > 0)
            {
                for (int t = 0; t < dtshuidian.Rows.Count; t++)
                {
                    string insertshuidian = "update OM_GZQD set QD_ShuiDian=" + CommonFun.ComTryDecimal(dtshuidian.Rows[t]["realmoney"].ToString().Trim()) + " where QD_STID='" + dtshuidian.Rows[t]["peopleid"].ToString().Trim() + "' and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                    listinsert.Add(insertshuidian);
                }
            }
            DBCallCommon.ExecuteTrans(listinsert);



            //计算
            string sqldatasource = "select * from OM_GZQD left join OM_KQTJ on KQ_ST_ID=QD_STID and KQ_DATE=QD_YEARMONTH left join OM_SALARYBASEDATA on QD_STID=ST_ID where QD_STID not in(select QD_STID from OM_GZQDeditJL where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "') and  QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2') and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "'";
            System.Data.DataTable dtdatasource = DBCallCommon.GetDTUsingSqlText(sqldatasource);
            //司机中夜班费（只算夜班）
            string sqlzybmoney = "select * from View_OM_GZQD left join OM_KQTJ on KQ_ST_ID=QD_STID and KQ_DATE=QD_YEARMONTH where QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2') and QD_STID not in(select QD_STID from OM_GZQDeditJL where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "') and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and DEP_NAME_POSITION='司机'";
            System.Data.DataTable dtzybmoney = DBCallCommon.GetDTUsingSqlText(sqlzybmoney);
            if (dtzybmoney.Rows.Count > 0)
            {
                for (int y = 0; y < dtzybmoney.Rows.Count; y++)
                {
                    double sjzybf = (CommonFun.ComTryDouble(dtzybmoney.Rows[y]["KQ_YEBAN"].ToString().Trim())) * 60;


                    string insertsjzybf = "update OM_GZQD set QD_ZYBF=" + Math.Round(sjzybf, 2) + " where QD_STID='" + dtzybmoney.Rows[y]["KQ_ST_ID"].ToString().Trim() + "' and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                    list.Add(insertsjzybf);

                }
            }
            //1.交通补贴
            string strjtbt = "select * from OM_GZQD left join OM_KQTJ on KQ_ST_ID=QD_STID and KQ_DATE=QD_YEARMONTH where QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2') and (QD_STID='47' or QD_STID='52' or QD_STID='63' or QD_STID='72') and QD_STID not in(select QD_STID from OM_GZQDeditJL where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "') and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "'";
            System.Data.DataTable dtjtbtdata = DBCallCommon.GetDTUsingSqlText(strjtbt);
            if (dtjtbtdata.Rows.Count > 0)
            {
                for (int m = 0; m < dtjtbtdata.Rows.Count; m++)
                {
                    double jtbtmoney = (CommonFun.ComTryDouble(dtjtbtdata.Rows[m]["KQ_CHUQIN"].ToString().Trim())) * 20;
                    string insertjtbt = "update OM_GZQD set QD_JTBT=" + Math.Round(jtbtmoney, 2) + " where QD_STID='" + dtjtbtdata.Rows[m]["KQ_ST_ID"].ToString().Trim() + "' and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                    list.Add(insertjtbt);
                }
            }
            if (dtdatasource.Rows.Count > 0)
            {
                for (int n = 0; n < dtdatasource.Rows.Count; n++)
                {

                    //2.病假工资
                    double bingjiamoney = CommonFun.ComTryDouble(dtdatasource.Rows[n]["BINGJIA_BASEDATANEW"].ToString().Trim()) * 0.8 / 21.75 * (CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_BINGJ"].ToString().Trim()));
                    if (bingjiamoney > 0)
                    {
                        string insertbingjia = "update OM_GZQD set QD_BingJiaGZ=" + Math.Round(bingjiamoney, 2) + " where QD_STID='" + dtdatasource.Rows[n]["KQ_ST_ID"].ToString().Trim() + "' and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                        list.Add(insertbingjia);
                    }

                    //管理岗加班工资
                    double checkzf = CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_ZMJBAN"].ToString().Trim()) - CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_BINGJ"].ToString().Trim()) - CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_SHIJ"].ToString().Trim());
                    double jiabanmoney = 0;

                    if (checkzf > 0)
                    {
                        jiabanmoney = CommonFun.ComTryDouble(dtdatasource.Rows[n]["JIABAN_BASEDATANEW"].ToString().Trim()) / 21.75 * (CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_JRJIAB"].ToString().Trim())) * 3 + CommonFun.ComTryDouble(dtdatasource.Rows[n]["JIABAN_BASEDATANEW"].ToString().Trim()) / 21.75 * checkzf * 2 + CommonFun.ComTryDouble(dtdatasource.Rows[n]["JIABAN_BASEDATANEW"].ToString().Trim()) / 21.75 / 8 * (CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_YSGZ"].ToString().Trim())) * 1.5;
                    }
                    else
                    {
                        jiabanmoney = CommonFun.ComTryDouble(dtdatasource.Rows[n]["JIABAN_BASEDATANEW"].ToString().Trim()) / 21.75 * (CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_JRJIAB"].ToString().Trim())) * 3 + CommonFun.ComTryDouble(dtdatasource.Rows[n]["JIABAN_BASEDATANEW"].ToString().Trim()) / 21.75 / 8 * (CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_YSGZ"].ToString().Trim())) * 1.5;
                    }

                    if (jiabanmoney > 0 || jiabanmoney < 0)
                    {
                        string insertjiaban = "update OM_GZQD set QD_JiaBanGZ=" + Math.Round(jiabanmoney, 2) + " where QD_STID='" + dtdatasource.Rows[n]["KQ_ST_ID"].ToString().Trim() + "' and QD_STID!='83' and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2') and (QD_JiaBanGZ=0 or QD_JiaBanGZ='' or QD_JiaBanGZ is null) and QD_STID not in(select FZBZ_STID from OM_SCCZ_FZBZ where FZBZ_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "') and  QD_STID not in(select YIXIAN_ST_ID from OM_SCCZ_YIXIAN where YIXIAN_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "') and QD_STID!='83'";
                        list.Add(insertjiaban);
                    }

                    //生产操作加班工资
                    double banzcheckzf = CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_ZMJBAN"].ToString().Trim()) - CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_BINGJ"].ToString().Trim()) - CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_SHIJ"].ToString().Trim());
                    double banzjiabanmoney = 0;
                    if (banzcheckzf > 0)
                    {
                        banzjiabanmoney = CommonFun.ComTryDouble(dtdatasource.Rows[n]["JIABAN_BASEDATANEW"].ToString().Trim()) / 21.75 * (CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_JRJIAB"].ToString().Trim())) * 2 + CommonFun.ComTryDouble(dtdatasource.Rows[n]["JIABAN_BASEDATANEW"].ToString().Trim()) / 21.75 * banzcheckzf + CommonFun.ComTryDouble(dtdatasource.Rows[n]["JIABAN_BASEDATANEW"].ToString().Trim()) / 21.75 / 8 / 2 * (CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_YSGZ"].ToString().Trim()));
                    }
                    else
                    {
                        banzjiabanmoney = CommonFun.ComTryDouble(dtdatasource.Rows[n]["JIABAN_BASEDATANEW"].ToString().Trim()) / 21.75 * (CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_JRJIAB"].ToString().Trim())) * 2 + CommonFun.ComTryDouble(dtdatasource.Rows[n]["JIABAN_BASEDATANEW"].ToString().Trim()) / 21.75 / 8 / 2 * (CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_YSGZ"].ToString().Trim()));
                    }
                    if (banzjiabanmoney > 0 || banzjiabanmoney < 0)
                    {
                        string banzinsertjiaban = "update OM_GZQD set QD_JiaBanGZ=" + Math.Round(banzjiabanmoney, 2) + " where QD_STID='" + dtdatasource.Rows[n]["KQ_ST_ID"].ToString().Trim() + "' and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2') and (QD_JiaBanGZ=0 or QD_JiaBanGZ='' or QD_JiaBanGZ is null) and (QD_STID in(select FZBZ_STID from OM_SCCZ_FZBZ where FZBZ_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "') or  QD_STID in(select YIXIAN_ST_ID from OM_SCCZ_YIXIAN where YIXIAN_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "') or QD_STID='83')";//加上孙士超（siid=83）的
                        list.Add(banzinsertjiaban);
                    }
                    //应扣岗位
                    double checkzfykgw = CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_BINGJ"].ToString().Trim()) + CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_SHIJ"].ToString().Trim()) - CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_ZMJBAN"].ToString().Trim());
                    double yingkgwmoney = 0;
                    if (checkzfykgw > 0)
                    {
                        yingkgwmoney = -checkzfykgw * (CommonFun.ComTryDouble(dtdatasource.Rows[n]["YKGW_BASEDATANEW"].ToString().Trim())) / 21.75;
                    }
                    else
                    {
                        yingkgwmoney = 0;
                    }
                    if (yingkgwmoney < 0)
                    {
                        string insertyingkgw = "update OM_GZQD set QD_YKGW=" + Math.Round(yingkgwmoney, 2) + " where QD_STID='" + dtdatasource.Rows[n]["KQ_ST_ID"].ToString().Trim() + "' and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                        list.Add(insertyingkgw);
                    }

                    //年假工资
                    double nianjiamoney = CommonFun.ComTryDouble(dtdatasource.Rows[n]["NIANJIA_BASEDATANEW"].ToString().Trim()) / 21.75 * CommonFun.ComTryDouble(dtdatasource.Rows[n]["KQ_NIANX"].ToString().Trim());
                    if (nianjiamoney > 0 || nianjiamoney < 0)
                    {
                        string insertnianjia = "update OM_GZQD set QD_NianJiaGZ=" + Math.Round(nianjiamoney, 2) + " where QD_STID='" + dtdatasource.Rows[n]["KQ_ST_ID"].ToString().Trim() + "' and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2') and (QD_STID in(select FZBZ_STID from OM_SCCZ_FZBZ where FZBZ_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "') or QD_STID in(select YIXIAN_ST_ID from OM_SCCZ_YIXIAN where YIXIAN_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "')) and QD_STID not in(select ST_ID from View_TBDS_STAFFINFO where DEP_POSITION LIKE '%组长%')";
                        list.Add(insertnianjia);//操作岗位才算select
                    }
                }
            }
            DBCallCommon.ExecuteTrans(list);

            //个税
            updategeshui();
            UCPaging1.CurrentPage = 1;
            bindrpt();
            if (!string.IsNullOrEmpty(strdataifall))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('生成成功，缺失数据：" + strdataifall + "！');", true);
            }
            
        }

        private void updategeshui()
        {
            string gesdatasource = "select *,((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ)-QD_KOUXIANG) as QD_KOUSJS from OM_GZQD left join OM_KQTJ on KQ_ST_ID=QD_STID and KQ_DATE=QD_YEARMONTH where QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2') and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_STID not in(select QD_STID from OM_GZQDeditJL where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "')";
            System.Data.DataTable dtdatasource = DBCallCommon.GetDTUsingSqlText(gesdatasource);
            List<string> listgeshui = new List<string>();
            //个税
            if (dtdatasource.Rows.Count > 0)
            {
                for (int t = 0; t < dtdatasource.Rows.Count; t++)
                {
                    double ksjsmoney = CommonFun.ComTryDouble(dtdatasource.Rows[t]["QD_KOUSJS"].ToString().Trim());
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
                    string insertgeshui = "update OM_GZQD set QD_GeShui=" + Math.Round(geshui, 2) + " where QD_STID='" + dtdatasource.Rows[t]["KQ_ST_ID"].ToString().Trim() + "' and QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                    listgeshui.Add(insertgeshui);
                }
                DBCallCommon.ExecuteTrans(listgeshui);
            }
        }



        //工资调整
        protected void btngztz_OnClick(object sender, EventArgs e)
        {
            string sqlifcz = "select * from View_OM_GZQD where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and (OM_GZSCBZ='0' or OM_GZSCBZ='1')";
            System.Data.DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifcz);
            if (dtifsc.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月工资清单正在审核或已审核通过！');", true);
                return;
            }
            string passParameters = "";
            int i = 0;
            foreach (RepeaterItem Reitem in rptGZQD.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)Reitem.FindControl("cbxNumber");
                if (cbx.Checked)
                {
                    i++;
                    System.Web.UI.WebControls.Label labid = (System.Web.UI.WebControls.Label)Reitem.FindControl("lbQD_ID");
                    passParameters += labid.Text.ToString().Trim() + "/";
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择需要调整的数据！');", true);
                return;
            }
            if (passParameters.Length > 0)
            {
                passParameters = passParameters.Substring(0, passParameters.Length - 1);
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "js", "<Script language=JavaScript>window.open('OM_GZTZedit.aspx?arrayqdid=" + passParameters + "');</Script>");
        }

        //提交工资审批
        protected void btngzqdsh_OnClick(object sender, EventArgs e)
        {
            string sqltext = "select * from (select * from View_OM_GZQD left join OM_KQTJ on (View_OM_GZQD.QD_STID=OM_KQTJ.KQ_ST_ID and View_OM_GZQD.QD_YEARMONTH=OM_KQTJ.KQ_DATE))t where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and (OM_GZSCBZ is null or OM_GZSCBZ='')";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                System.Web.UI.WebControls.Label lbQD_SHBH = (System.Web.UI.WebControls.Label)rptGZQD.Items[0].FindControl("lbQD_SHBH");
                Response.Redirect("OM_GZQDSPdetail.aspx?idbh=" + lbQD_SHBH.Text.Trim());
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月没有数据！');", true);
                return;
            }
        }


        //删除
        protected void btndelete_OnClick(object sender, EventArgs e)
        {
            string sqlifcz = "select * from View_OM_GZQD where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and (OM_GZSCBZ='0' or OM_GZSCBZ='1')";
            System.Data.DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifcz);
            if (dtifsc.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月工资清单正在审核或已审核通过！');", true);
                return;
            }
            List<string> sqltextlist = new List<string>();
            foreach (RepeaterItem rptitem in rptGZQD.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)rptitem.FindControl("cbxNumber");
                System.Web.UI.WebControls.Label lbQD_ID = (System.Web.UI.WebControls.Label)rptitem.FindControl("lbQD_ID");
                if (cbx.Checked == true)
                {
                    string sqldelete = "delete from OM_GZQD where QD_ID='" + lbQD_ID.Text.Trim() + "'";
                    sqltextlist.Add(sqldelete);
                }
            }
            DBCallCommon.ExecuteTrans(sqltextlist);
            bindrpt();
        }


        //防暑降温费导入
        protected void btnimportfsjw_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            if (tb_yearmonth.Text.ToString().Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！！！');", true);
                return;
            }
            string sqlifsc = "select * from OM_GZHSB where GZ_YEARMONTH='" + tb_yearmonth.Text.ToString().Trim() + "' and (OM_GZSCBZ='1' or OM_GZSCBZ='0')";
            System.Data.DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifsc);
            if (dtifsc.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月工资已提交审核或审核通过，不能导入！！！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }
            string FilePath = @"E:\公积金表\";
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
            string weidaoru = "";
            using (FileStream fs = File.OpenRead(FilePath + "//" + System.IO.Path.GetFileName(UserHPF.FileName)))
            {
                //根据文件流创建一个workbook
                IWorkbook wk = new HSSFWorkbook(fs);

                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 3; i < sheet1.LastRowNum + 1; i++)
                {
                    string sh_stid = "";
                    string strcell01 = "";
                    IRow row = sheet1.GetRow(i);
                    ICell cell01 = row.GetCell(1);
                    try
                    {
                        strcell01 = cell01.ToString().Trim();
                    }
                    catch
                    {
                        strcell01 = "";
                    }
                    if (strcell01 != "")
                    {
                        ICell cell1 = row.GetCell(1);
                        string strcell1 = cell1.ToString().Trim();
                        string sqltext = "select QD_STID,ST_NAME from View_OM_GZQD where ST_WORKNO='" + strcell1 + "' and GZ_YEARMONTH='" + tb_yearmonth.Text.ToString().Trim() + "'";
                        System.Data.DataTable dttext = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dttext.Rows.Count > 0)
                        {
                            sh_stid = dttext.Rows[0]["QD_STID"].ToString().Trim();
                            ICell cell3 = row.GetCell(11);
                            string strcell3 = "";
                            try
                            {
                                strcell3 = cell3.NumericCellValue.ToString().Trim();
                            }
                            catch
                            {
                                strcell3 = cell3.ToString().Trim();
                            }
                            string sqlupdate = "update OM_GZQD set QD_FSJW='" + CommonFun.ComTryDecimal(strcell3) + "' where QD_STID='" + dttext.Rows[0]["QD_STID"].ToString().Trim() + "' and QD_YEARMONTH='" + tb_yearmonth.Text.ToString().Trim() + "'";
                            list.Add(sqlupdate);
                        }
                        else
                        {
                            weidaoru += strcell1 + ",";
                        }
                    }
                }
            }
            DBCallCommon.ExecuteTrans(list);

            updategeshui();
            foreach (string fileName in Directory.GetFiles(FilePath))//清空该文件夹下的文件
            {
                string newName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                System.IO.File.Delete(FilePath + "\\" + newName);//删除文件下储存的文件
            }
            
            ModalPopupExtenderSearch.Hide();
            UCPaging1.CurrentPage = 1;
            bindrpt();
            if (!string.IsNullOrEmpty(weidaoru))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('缺失数据：" + weidaoru.Substring(0, weidaoru.Length - 2) + "！');", true);
            }
        }

        //采暖补贴导入
        protected void btnimportcnbt_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            if (tb_yearmonth.Text.ToString().Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！！！');", true);
                return;
            }
            string sqlifsc = "select * from OM_GZHSB where GZ_YEARMONTH='" + tb_yearmonth.Text.ToString().Trim() + "' and (OM_GZSCBZ='1' or OM_GZSCBZ='0')";
            System.Data.DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifsc);
            if (dtifsc.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月工资已提交审核或审核通过，不能导入！！！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }
            string FilePath = @"E:\公积金表\";
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
                for (int i = 3; i < sheet1.LastRowNum + 1; i++)
                {
                    string sh_stid = "";
                    string strcell01 = "";
                    IRow row = sheet1.GetRow(i);
                    ICell cell01 = row.GetCell(1);
                    try
                    {
                        strcell01 = cell01.ToString().Trim();
                    }
                    catch
                    {
                        strcell01 = "";
                    }
                    if (strcell01 != "")
                    {
                        ICell cell1 = row.GetCell(1);
                        string strcell1 = cell1.ToString().Trim();
                        string sqltext = "select QD_STID from View_OM_GZQD where ST_WORKNO='" + strcell1 + "' and GZ_YEARMONTH='" + tb_yearmonth.Text.ToString().Trim() + "'";
                        System.Data.DataTable dttext = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dttext.Rows.Count > 0)
                        {
                            sh_stid = dttext.Rows[0]["QD_STID"].ToString().Trim();
                            ICell cell3 = row.GetCell(3);
                            string strcell3 = "";
                            try
                            {
                                strcell3 = cell3.NumericCellValue.ToString().Trim();
                            }
                            catch
                            {
                                strcell3 = cell3.ToString().Trim();
                            }
                            string sqlupdate = "update OM_GZQD set QD_CLBT='" + CommonFun.ComTryDecimal(strcell3) + "' where QD_STID='" + dttext.Rows[0]["QD_STID"].ToString().Trim() + "' and QD_YEARMONTH='" + tb_yearmonth.Text.ToString().Trim() + "'";
                            list.Add(sqlupdate);
                        }
                    }
                }
            }
            DBCallCommon.ExecuteTrans(list);

            updategeshui();
            foreach (string fileName in Directory.GetFiles(FilePath))//清空该文件夹下的文件
            {
                string newName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                System.IO.File.Delete(FilePath + "\\" + newName);//删除文件下储存的文件
            }
            ModalPopupExtenderSearch.Hide();
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }


        //取消
        protected void btnqx_import_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }


        private void jsgztx()
        {
            string strnamets5 = "";
            string sqlts5 = "select ST_NAME from OM_KQTJ as a left join TBDS_STAFFINFO as b on a.KQ_ST_ID=b.ST_ID where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and (KQ_BINGJ+KQ_SHIJ+KQ_NIANX)>5 and KQ_ST_ID in(select QD_STID from OM_GZQD where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "')";
            System.Data.DataTable dtts5 = DBCallCommon.GetDTUsingSqlText(sqlts5);
            if (dtts5.Rows.Count > 0)
            {
                for (int i = 0; i < dtts5.Rows.Count; i++)
                {
                    strnamets5 += dtts5.Rows[i]["ST_NAME"].ToString().Trim() + ",";
                }
                strnamets5 = strnamets5.Substring(0, strnamets5.Length - 1);
                lb_ts5.Text = strnamets5;
            }


            string strnamelzry = "";
            string sqllzry = "select ST_NAME from TBDS_STAFFINFO where ST_PD='1' and ST_ID in(select QD_STID from OM_GZQD where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "')";
            System.Data.DataTable dtlzry = DBCallCommon.GetDTUsingSqlText(sqllzry);
            if (dtlzry.Rows.Count > 0)
            {
                for (int j = 0; j < dtlzry.Rows.Count; j++)
                {
                    strnamelzry += dtlzry.Rows[j]["ST_NAME"].ToString().Trim() + ",";
                }
                strnamelzry = strnamelzry.Substring(0, strnamelzry.Length - 1);
                lb_lzry.Text = strnamelzry;
            }


            string strnamenianjia = "";
            string sqlnianjia = "select NJ_NAME from OM_NianJiaTJ where 1=1";
            //年假计算
            string str2 = "0";
            string syts = "";
            string sqltext2 = "select NJ_ST_ID,NJ_NAME,NJ_WORKNUMBER,NJ_BUMEN,NJ_BUMENID,NJ_RUZHITIME,NJ_LIZHITIME,NJ_TZTS,NJ_YSY,NJ_QINGL,NJ_LASTQL from OM_NianJiaTJ";
            System.Data.DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext2);
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    #region
                    //计算年假
                    DateTime datemin2;
                    DateTime datemax2;
                    try
                    {
                        datemin2 = DateTime.Parse(dt2.Rows[i]["NJ_RUZHITIME"].ToString().Trim());
                        if (dt2.Rows[i]["NJ_LIZHITIME"].ToString().Trim() != "")
                        {
                            datemax2 = DateTime.Parse(dt2.Rows[i]["NJ_LIZHITIME"].ToString().Trim());
                        }
                        else
                        {
                            datemax2 = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                        }
                    }
                    catch
                    {
                        datemin2 = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                        datemax2 = DateTime.Parse(DateTime.Now.ToString("yyyy.12.30").Trim());
                    }
                    decimal monthnum2 = datemax2.Month - datemin2.Month;
                    decimal yearnum2 = datemax2.Year - datemin2.Year;
                    decimal totalmonthnum2 = yearnum2 * 12 + monthnum2;
                    decimal ysynum2 = 0;
                    decimal qinglnum2 = 0;
                    try
                    {
                        ysynum2 = Convert.ToDecimal(dt2.Rows[i]["NJ_YSY"].ToString().Trim());
                        qinglnum2 = Convert.ToDecimal(dt2.Rows[i]["NJ_QINGL"].ToString().Trim());
                    }
                    catch
                    {
                        ysynum2 = 0;
                        qinglnum2 = 0;
                    }
                    //大于一年小于五年
                    if (totalmonthnum2 >= 12 && totalmonthnum2 < 60)
                    {
                        syts = (Math.Floor((totalmonthnum2 - 12) * 5 / 12) - qinglnum2 - ysynum2).ToString().Trim();
                    }
                    //大于五年小于十年
                    else if (totalmonthnum2 >= 60 && totalmonthnum2 < 120)
                    {
                        syts = (Math.Floor((60 - 12) * 5 / 12 + (totalmonthnum2 - 60) * 10 / 12) - qinglnum2 - ysynum2).ToString().Trim();
                    }
                    //十年以上
                    else if (totalmonthnum2 >= 120)
                    {
                        syts = (Math.Floor((60 - 12) * 5 / 12 + (120 - 60) * 10 / 12 + (totalmonthnum2 - 120) * 15 / 12) - qinglnum2 - ysynum2).ToString().Trim();
                    }
                    else
                    {
                        syts = (-(qinglnum2 + ysynum2)).ToString().Trim();
                    }
                    if (CommonFun.ComTryDecimal(syts) < 0)
                    {
                        str2 += ",'" + dt2.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                    }
                }
                    #endregion
            }
            sqlnianjia += " and NJ_ST_ID in(" + str2 + ") and NJ_ST_ID in(select QD_STID from OM_GZQD where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "')";
            System.Data.DataTable dtnianjia = DBCallCommon.GetDTUsingSqlText(sqlnianjia);
            if (dtnianjia.Rows.Count > 0)
            {
                for (int k = 0; k < dtnianjia.Rows.Count; k++)
                {
                    strnamenianjia += dtnianjia.Rows[k]["NJ_NAME"].ToString().Trim() + ",";
                }
                strnamenianjia = strnamenianjia.Substring(0, strnamenianjia.Length - 1);
                lb_nianjia.Text = strnamenianjia;
            }


            string strnamezhengry = "";
            string lastyear = "";
            string lastmonth = "";
            try
            {
                if (CommonFun.ComTryInt(ddlMonth.SelectedValue.ToString().Trim()) == 1)
                {
                    lastyear = (CommonFun.ComTryInt(ddlYear.SelectedValue.ToString().Trim()) - 1).ToString().Trim();
                    lastmonth = "12";
                }
                else
                {
                    lastyear = ddlYear.SelectedValue.ToString().Trim();
                    lastmonth = (CommonFun.ComTryInt(ddlMonth.SelectedValue.ToString().Trim()) - 1).ToString("00").Trim();
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('获取年月份出错！');", true);
                return;
            }
            string sqlzhengry = "select ST_NAME from TBDS_STAFFINFO where ST_ZHENG>='" + lastyear + "-" + lastmonth + "-21' and ST_ZHENG<='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "-20' and ST_ID in(select QD_STID from OM_GZQD where QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "')";
            System.Data.DataTable dtzhengry = DBCallCommon.GetDTUsingSqlText(sqlzhengry);
            if (dtzhengry.Rows.Count > 0)
            {
                for (int m = 0; m < dtzhengry.Rows.Count; m++)
                {
                    strnamezhengry += dtzhengry.Rows[m]["ST_NAME"].ToString().Trim() + ",";
                }
                strnamezhengry = strnamezhengry.Substring(0, strnamezhengry.Length - 1);
                lb_zhuanzheng.Text = strnamezhengry;
            }
        }

        protected void btnsdfdr_Click(object sender, EventArgs e)
        {
            List<string> listinsert = new List<string>();
            if (tb_yearmonth.Text.ToString().Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！！！');", true);
                return;
            }
            string sqlshuidian = " select IDsdmx,peopleid,realmoney from (select IDsdmx,IDSDF,ssrens,ssnum,OM_SDFY.startdate,OM_SDFY.enddate,stratdf,enddf,pricedf,startsf,endsf,pricesf,ST_NAME,note,peopleid,fangjnum,gscddl,gscdsl,realmoney,dairumonth,dairusalary,spbh,state from OM_SDFY left join OM_SDFdetail on (OM_SDFY.ssnum=OM_SDFdetail.fangjnum and OM_SDFY.startdate=OM_SDFdetail.startdate and OM_SDFY.enddate=OM_SDFdetail.enddate) left join TBDS_STAFFINFO on OM_SDFdetail.peopleid=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t where dairumonth='" + tb_yearmonth.Text.ToString().Trim() + "' and state='2'";
            System.Data.DataTable dtshuidian = DBCallCommon.GetDTUsingSqlText(sqlshuidian);
            if (dtshuidian.Rows.Count > 0)
            {
                for (int t = 0; t < dtshuidian.Rows.Count; t++)
                {
                    string insertshuidian = "update OM_GZQD set QD_ShuiDian=" + CommonFun.ComTryDecimal(dtshuidian.Rows[t]["realmoney"].ToString().Trim()) + " where QD_STID='" + dtshuidian.Rows[t]["peopleid"].ToString().Trim() + "' and QD_YEARMONTH='" + tb_yearmonth.Text.ToString().Trim() + "' and QD_SHBH not in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='2')";
                    listinsert.Add(insertshuidian);
                }
            }
            DBCallCommon.ExecuteTrans(listinsert);
            //个税
            updategeshui();
            UCPaging1.CurrentPage = 1;
            bindrpt();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('生成成功！');", true);
        }
    }
}
