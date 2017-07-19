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
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_GZHZB : BasicPage
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            pager_org.OrderField = "QD_YEARMONTH,DEP_NAME,ST_DEPID1";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 20;
        }
        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string sql = "1=1 and QD_STID='" + Session["UserID"].ToString().Trim() + "' and OM_GZSCBZ='1'";
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
            return sql;
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
                    HtmlTableCell Banzu = e.Item.FindControl("tdBanzu") as HtmlTableCell;
                    Bumen.Visible = false;
                    Banzu.Visible = false;
                }
                if (cbxKaoqin.Checked)
                {
                    HtmlTableCell YCQHJ = e.Item.FindControl("tdYCQHJ") as HtmlTableCell;
                    HtmlTableCell JRwork = e.Item.FindControl("tdJRwork") as HtmlTableCell;
                    HtmlTableCell Zhouwork = e.Item.FindControl("tdZhouwork") as HtmlTableCell;
                    HtmlTableCell Riwork = e.Item.FindControl("tdRiwork") as HtmlTableCell;
                    HtmlTableCell Bingjia = e.Item.FindControl("tdBingjia") as HtmlTableCell;
                    HtmlTableCell Shijia = e.Item.FindControl("tdShijia") as HtmlTableCell;
                    HtmlTableCell Nianjia = e.Item.FindControl("tdNianjia") as HtmlTableCell;
                    YCQHJ.Visible = false;
                    JRwork.Visible = false;
                    Zhouwork.Visible = false;
                    Bingjia.Visible = false;
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
                    HtmlTableCell tdQD_BanZu = e.Item.FindControl("tdQD_BanZu") as HtmlTableCell;
                    tdQD_BuMen.Visible = false;
                    tdQD_BanZu.Visible = false;
                }
                if (cbxKaoqin.Checked)
                {
                    HtmlTableCell tdKQ_CHUQIN = e.Item.FindControl("tdKQ_CHUQIN") as HtmlTableCell;
                    HtmlTableCell tdKQ_JRJIAB = e.Item.FindControl("tdKQ_JRJIAB") as HtmlTableCell;
                    HtmlTableCell tdKQ_ZMJBAN = e.Item.FindControl("tdKQ_ZMJBAN") as HtmlTableCell;
                    HtmlTableCell tdKQ_YSGZ = e.Item.FindControl("tdKQ_YSGZ") as HtmlTableCell;
                    HtmlTableCell tdKQ_BINGJ = e.Item.FindControl("tdKQ_BINGJ") as HtmlTableCell;
                    HtmlTableCell tdKQ_SHIJ = e.Item.FindControl("tdKQ_SHIJ") as HtmlTableCell;
                    HtmlTableCell tdKQ_NIANX = e.Item.FindControl("tdKQ_NIANX") as HtmlTableCell;
                    tdKQ_CHUQIN.Visible = false;
                    tdKQ_JRJIAB.Visible = false;
                    tdKQ_ZMJBAN.Visible = false;
                    tdKQ_YSGZ.Visible = false;
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
                    lb_QD_GZGLhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt000.Rows[0]["QD_JCGZhj"].ToString().Trim()), 2)).ToString().Trim();
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
                    HtmlTableCell tdfoot1 = e.Item.FindControl("tdfoot1") as HtmlTableCell;
                    tdfoot1.Visible = false;
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
    }
}
