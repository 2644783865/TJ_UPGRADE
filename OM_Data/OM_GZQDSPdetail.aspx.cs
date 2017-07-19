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
    public partial class OM_GZQDSPdetail : System.Web.UI.Page
    {
        string spbh = "";
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                spbh = Request.QueryString["idbh"].ToString().Trim();
                BindbmData();
                bindbz();
                bindgw();
                bindshenhdata();
                bindkjkyx();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindrpt();
            }
            this.InitVar();
        }
        #region 分页
        //绑定基本信息
        private void BindbmData()
        {
            string sql = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO WHERE len(DEP_CODE)=2";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataBind();
            ListItem item = new ListItem("--请选择--", "0");
            ddl_Depart.Items.Insert(0, item);
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
            spbh = Request.QueryString["idbh"].ToString().Trim();
            string sql = "1=1 and GZSH_BH='" + spbh + "'";
            if (txtName.Text != "")
            {
                sql += " and ST_NAME='" + txtName.Text.Trim() + "'";
            }
            if (ddl_Depart.SelectedIndex != 0)
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
            if (txtgdgzmin.Text.Trim() != "" && txtgdgzmax.Text.Trim() != "")
            {
                sql += " and QD_GDGZ>=" + CommonFun.ComTryDecimal(txtgdgzmin.Text.Trim()) + " and QD_GDGZ<=" + CommonFun.ComTryDecimal(txtgdgzmax.Text.Trim()) + "";
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
            CommonFun.Paging(dt, rptGZQD, UCPaging1, palNoData1);
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
        #endregion
        protected void dplbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
        protected void btnQuery_OnClick(object sender, EventArgs e)
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
        #endregion
        //控件可用性
        private void bindkjkyx()
        {
            spbh = Request.QueryString["idbh"].ToString().Trim();
            string sqlkjdata = "select * from OM_GZHSB where GZSH_BH='" + spbh + "'";
            DataTable dtkj = DBCallCommon.GetDTUsingSqlText(sqlkjdata);
            if (dtkj.Rows.Count > 0)
            {
                if (rblSHJS.SelectedValue.ToString().Trim() == "1")
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
                else if (rblSHJS.SelectedValue.ToString().Trim() == "3")
                {
                    yjshh.Visible = true;
                    ejshh.Visible = true;
                    sjshh.Visible = true;
                }
                
                //初始化
                if (dtkj.Rows[0]["OM_GZSCBZ"].ToString().Trim() == "")
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["GZ_SCRID"].ToString().Trim())
                    {
                        btnSave.Visible = true;
                        txt_first.Enabled = true;
                        hlSelect1.Visible = true;
                        txt_second.Enabled = true;
                        hlSelect2.Visible = true;
                        txt_third.Enabled = true;
                        hlSelect3.Visible = true;
                        tbfqryj.Enabled = true;
                        rblSHJS.Enabled=true;
                    }
                }
                else if (dtkj.Rows[0]["OM_GZSCBZ"].ToString().Trim() == "0")
                {
                    if (rblSHJS.SelectedValue.ToString().Trim() == "1"||rblSHJS.SelectedValue.ToString().Trim() == "2" || rblSHJS.SelectedValue.ToString().Trim() == "3")
                    {
                        if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["GZ_SHRID1"].ToString().Trim() && dtkj.Rows[0]["GZ_SHSTATE1"].ToString().Trim() == "0")
                        {
                            btnSave.Visible = true;
                            rblfirst.Enabled = true;
                            opinion1.Enabled = true;
                        }
                    }
                    if (rblSHJS.SelectedValue.ToString().Trim() == "2" || rblSHJS.SelectedValue.ToString().Trim() == "3")
                    {
                        if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["GZ_SHRID2"].ToString().Trim() && dtkj.Rows[0]["GZ_SHSTATE1"].ToString().Trim() == "1" && dtkj.Rows[0]["GZ_SHSTATE2"].ToString().Trim() == "0")
                        {
                            btnSave.Visible = true;
                            rblsecond.Enabled = true;
                            opinion2.Enabled = true;
                        }
                    }
                    if (rblSHJS.SelectedValue.ToString().Trim() == "3")
                    {
                        if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["GZ_SHRID3"].ToString().Trim() && dtkj.Rows[0]["GZ_SHSTATE1"].ToString().Trim() == "1" && dtkj.Rows[0]["GZ_SHSTATE2"].ToString().Trim() == "1" && dtkj.Rows[0]["GZ_SHSTATE3"].ToString().Trim() == "0")
                        {
                            btnSave.Visible = true;
                            rblthird.Enabled = true;
                            opinion3.Enabled = true;
                        }
                    }
                }
                else if (dtkj.Rows[0]["OM_GZSCBZ"].ToString().Trim() == "1")
                {
                    ImageVerify.Visible = true;
                }
                else if (dtkj.Rows[0]["OM_GZSCBZ"].ToString().Trim() == "2")
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["GZ_SCRID"].ToString().Trim())
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
            spbh = Request.QueryString["idbh"].ToString().Trim();
            string sqlshdata = "select * from OM_GZHSB where GZSH_BH='" + spbh + "'";
            DataTable dtsh = DBCallCommon.GetDTUsingSqlText(sqlshdata);
            if (dtsh.Rows.Count > 0)
            {
                lbzdr.Text = dtsh.Rows[0]["GZ_SCRNAME"].ToString().Trim();
                lbzdtime.Text = dtsh.Rows[0]["GZSCTIME"].ToString().Trim();
                tbfqryj.Text = dtsh.Rows[0]["GZ_SCRNOTE"].ToString().Trim();

                lbtitle_zdr.Text = dtsh.Rows[0]["GZ_SCRNAME"].ToString().Trim();
                lbtitle_zdsj.Text = dtsh.Rows[0]["GZSCTIME"].ToString().Trim();
                string sqlgetyearmonth = "select * from OM_GZQD where QD_SHBH='" + spbh + "'";
                DataTable dtyearmonth = DBCallCommon.GetDTUsingSqlText(sqlgetyearmonth);
                if (dtyearmonth.Rows.Count > 0)
                {
                    lb_title.Text = "(" + dtyearmonth.Rows[0]["QD_YEARMONTH"].ToString().Trim() + ")";
                }


                //审核级数绑定
                if (dtsh.Rows[0]["GZ_SHJS"].ToString().Trim() == "1")
                {
                    rblSHJS.SelectedValue = "1";
                    txt_first.Text = dtsh.Rows[0]["GZ_SHRNAME1"].ToString().Trim();
                    firstid.Value = dtsh.Rows[0]["GZ_SHRID1"].ToString().Trim();
                    if (dtsh.Rows[0]["GZ_SHSTATE1"].ToString().Trim() == "1" || dtsh.Rows[0]["GZ_SHSTATE1"].ToString().Trim() == "2")
                    {
                        rblfirst.SelectedValue = dtsh.Rows[0]["GZ_SHSTATE1"].ToString().Trim();
                    }
                    first_time.Text = dtsh.Rows[0]["GZ_SHTIME1"].ToString().Trim();
                    opinion1.Text = dtsh.Rows[0]["GZ_SHNOTE1"].ToString().Trim();
                }
                else if (dtsh.Rows[0]["GZ_SHJS"].ToString().Trim() == "2")
                {
                    rblSHJS.SelectedValue = "2";
                    txt_first.Text = dtsh.Rows[0]["GZ_SHRNAME1"].ToString().Trim();
                    firstid.Value = dtsh.Rows[0]["GZ_SHRID1"].ToString().Trim();
                    if (dtsh.Rows[0]["GZ_SHSTATE1"].ToString().Trim() == "1" || dtsh.Rows[0]["GZ_SHSTATE1"].ToString().Trim() == "2")
                    {
                        rblfirst.SelectedValue = dtsh.Rows[0]["GZ_SHSTATE1"].ToString().Trim();
                    }
                    first_time.Text = dtsh.Rows[0]["GZ_SHTIME1"].ToString().Trim();
                    opinion1.Text = dtsh.Rows[0]["GZ_SHNOTE1"].ToString().Trim();
                    txt_second.Text = dtsh.Rows[0]["GZ_SHRNAME2"].ToString().Trim();
                    secondid.Value = dtsh.Rows[0]["GZ_SHRID2"].ToString().Trim();
                    if (dtsh.Rows[0]["GZ_SHSTATE2"].ToString().Trim() == "1" || dtsh.Rows[0]["GZ_SHSTATE2"].ToString().Trim() == "2")
                    {
                        rblsecond.SelectedValue = dtsh.Rows[0]["GZ_SHSTATE2"].ToString().Trim();
                    }
                    second_time.Text = dtsh.Rows[0]["GZ_SHTIME2"].ToString().Trim();
                    opinion2.Text = dtsh.Rows[0]["GZ_SHNOTE2"].ToString().Trim();
                }
                else if (dtsh.Rows[0]["GZ_SHJS"].ToString().Trim() == "3")
                {
                    rblSHJS.SelectedValue = "3";
                    txt_first.Text = dtsh.Rows[0]["GZ_SHRNAME1"].ToString().Trim();
                    firstid.Value = dtsh.Rows[0]["GZ_SHRID1"].ToString().Trim();
                    if (dtsh.Rows[0]["GZ_SHSTATE1"].ToString().Trim() == "1" || dtsh.Rows[0]["GZ_SHSTATE1"].ToString().Trim() == "2")
                    {
                        rblfirst.SelectedValue = dtsh.Rows[0]["GZ_SHSTATE1"].ToString().Trim();
                    }
                    first_time.Text = dtsh.Rows[0]["GZ_SHTIME1"].ToString().Trim();
                    opinion1.Text = dtsh.Rows[0]["GZ_SHNOTE1"].ToString().Trim();
                    txt_second.Text = dtsh.Rows[0]["GZ_SHRNAME2"].ToString().Trim();
                    secondid.Value = dtsh.Rows[0]["GZ_SHRID2"].ToString().Trim();
                    if (dtsh.Rows[0]["GZ_SHSTATE2"].ToString().Trim() == "1" || dtsh.Rows[0]["GZ_SHSTATE2"].ToString().Trim() == "2")
                    {
                        rblsecond.SelectedValue = dtsh.Rows[0]["GZ_SHSTATE2"].ToString().Trim();
                    }
                    second_time.Text = dtsh.Rows[0]["GZ_SHTIME2"].ToString().Trim();
                    opinion2.Text = dtsh.Rows[0]["GZ_SHNOTE2"].ToString().Trim();
                    txt_third.Text = dtsh.Rows[0]["GZ_SHRNAME3"].ToString().Trim();
                    thirdid.Value = dtsh.Rows[0]["GZ_SHRID3"].ToString().Trim();
                    if (dtsh.Rows[0]["GZ_SHSTATE3"].ToString().Trim() == "1" || dtsh.Rows[0]["GZ_SHSTATE2"].ToString().Trim() == "3")
                    {
                        rblthird.SelectedValue = dtsh.Rows[0]["GZ_SHSTATE3"].ToString().Trim();
                    }
                    third_time.Text = dtsh.Rows[0]["GZ_SHTIME3"].ToString().Trim();
                    opinion3.Text = dtsh.Rows[0]["GZ_SHNOTE3"].ToString().Trim();
                } 
            }
        }
        //提交
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            spbh = Request.QueryString["idbh"].ToString().Trim();
            string sqlsavedata = "select * from OM_GZHSB where GZSH_BH='" + spbh + "'";
            DataTable dtsave = DBCallCommon.GetDTUsingSqlText(sqlsavedata);
            if (dtsave.Rows.Count > 0)
            {
                if (dtsave.Rows[0]["GZ_SCRID"].ToString().Trim() == Session["UserID"].ToString().Trim())
                {
                    if (rblSHJS.SelectedValue.ToString().Trim() == "1")
                    {
                        if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                            return;
                        }
                        else
                        {
                            string sqlupdate0 = "update OM_GZHSB set OM_GZSCBZ='0',GZ_SCRNOTE='" + tbfqryj.Text.Trim() + "',GZ_SHRID1='" + firstid.Value.Trim() + "',GZ_SHRNAME1='" + txt_first.Text.Trim() + "',GZ_SHRID2='',GZ_SHRNAME2='',GZ_SHRID3='',GZ_SHRNAME3='',GZ_SHJS='1' where GZSH_BH='" + spbh + "'";
                            DBCallCommon.ExeSqlText(sqlupdate0);
                        }
                    }
                    else if (rblSHJS.SelectedValue.ToString().Trim() == "2")
                    {
                        if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "" || txt_second.Text.Trim() == "" || secondid.Value.Trim() == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                            return;
                        }
                        else
                        {
                            string sqlupdate0 = "update OM_GZHSB set OM_GZSCBZ='0',GZ_SCRNOTE='" + tbfqryj.Text.Trim() + "',GZ_SHRID1='" + firstid.Value.Trim() + "',GZ_SHRNAME1='" + txt_first.Text.Trim() + "',GZ_SHRID2='" + secondid.Value.Trim() + "',GZ_SHRNAME2='" + txt_second.Text.Trim() + "',GZ_SHRID3='',GZ_SHRNAME3='',GZ_SHJS='2' where GZSH_BH='" + spbh + "'";
                            DBCallCommon.ExeSqlText(sqlupdate0);
                        }
                    }
                    else if (rblSHJS.SelectedValue.ToString().Trim() == "3")
                    {
                        if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "" || txt_second.Text.Trim() == "" || secondid.Value.Trim() == "" || txt_third.Text.Trim() == "" || thirdid.Value.Trim() == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                            return;
                        }
                        else
                        {
                            string sqlupdate0 = "update OM_GZHSB set OM_GZSCBZ='0',GZ_SCRNOTE='" + tbfqryj.Text.Trim() + "',GZ_SHRID1='" + firstid.Value.Trim() + "',GZ_SHRNAME1='" + txt_first.Text.Trim() + "',GZ_SHRID2='" + secondid.Value.Trim() + "',GZ_SHRNAME2='" + txt_second.Text.Trim() + "',GZ_SHRID3='" + thirdid.Value.Trim() + "',GZ_SHRNAME3='" + txt_third.Text.Trim() + "',GZ_SHJS='3' where GZSH_BH='" + spbh + "'";
                            DBCallCommon.ExeSqlText(sqlupdate0);
                        }
                    }

                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sprid = firstid.Value.Trim();
                    sptitle = "工资清单审批";
                    spcontent = "有工资清单需要您审批，请登录查看！";
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                }
                if (dtsave.Rows[0]["GZ_SHRID1"].ToString().Trim() == Session["UserID"].ToString().Trim())
                {
                    string sqlupdate1 = "";
                    if (rblfirst.SelectedValue.ToString().Trim() == "1")
                    {
                        if (rblSHJS.SelectedValue.ToString().Trim() == "1")
                        {
                            sqlupdate1 = "update OM_GZHSB set GZ_SHSTATE1='1',OM_GZSCBZ='1',GZ_SHTIME1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',GZ_SHNOTE1='" + opinion1.Text.Trim() + "' where GZSH_BH='" + spbh + "'";
                        }
                        else if (rblSHJS.SelectedValue.ToString().Trim() == "2" || rblSHJS.SelectedValue.ToString().Trim() == "3")
                        {
                            sqlupdate1 = "update OM_GZHSB set GZ_SHSTATE1='1',GZ_SHTIME1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',GZ_SHNOTE1='" + opinion1.Text.Trim() + "' where GZSH_BH='" + spbh + "'";

                            //邮件提醒
                            string sprid = "";
                            string sptitle = "";
                            string spcontent = "";
                            sprid = secondid.Value.Trim();
                            sptitle = "工资清单审批";
                            spcontent = "有工资清单需要您审批，请登录查看！";
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                        }
                    }
                    else if (rblfirst.SelectedValue.ToString().Trim() == "2")
                    {
                        sqlupdate1 = "update OM_GZHSB set GZ_SHSTATE1='2',OM_GZSCBZ='2',GZ_SHTIME1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',GZ_SHNOTE1='" + opinion1.Text.Trim() + "' where GZSH_BH='" + spbh + "'";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核意见！');", true);
                        return;
                    }
                    DBCallCommon.ExeSqlText(sqlupdate1);
                }
                if (dtsave.Rows[0]["GZ_SHRID2"].ToString().Trim() == Session["UserID"].ToString().Trim())
                {
                    string sqlupdate2 = "";
                    if (rblsecond.SelectedValue.ToString().Trim() == "1")
                    {
                        if (rblSHJS.SelectedValue.ToString().Trim() == "2")
                        {
                            sqlupdate2 = "update OM_GZHSB set GZ_SHSTATE2='1',OM_GZSCBZ='1',GZ_SHTIME2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',GZ_SHNOTE2='" + opinion2.Text.Trim() + "' where GZSH_BH='" + spbh + "'";
                        }
                        else if (rblSHJS.SelectedValue.ToString().Trim() == "3")
                        {
                            sqlupdate2 = "update OM_GZHSB set GZ_SHSTATE2='1',GZ_SHTIME2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',GZ_SHNOTE2='" + opinion2.Text.Trim() + "' where GZSH_BH='" + spbh + "'";

                            //邮件提醒
                            string sprid = "";
                            string sptitle = "";
                            string spcontent = "";
                            sprid = thirdid.Value.Trim();
                            sptitle = "工资清单审批";
                            spcontent = "有工资清单需要您审批，请登录查看！";
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                        }
                    }
                    else if (rblsecond.SelectedValue.ToString().Trim() == "2")
                    {
                        sqlupdate2 = "update OM_GZHSB set GZ_SHSTATE2='2',OM_GZSCBZ='2',GZ_SHTIME2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',GZ_SHNOTE2='" + opinion2.Text.Trim() + "' where GZSH_BH='" + spbh + "'";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核意见！');", true);
                        return;
                    }
                    DBCallCommon.ExeSqlText(sqlupdate2);
                }

                if (dtsave.Rows[0]["GZ_SHRID3"].ToString().Trim() == Session["UserID"].ToString().Trim())
                {
                    string sqlupdate3 = "";
                    if (rblthird.SelectedValue.ToString().Trim() == "1")
                    {
                        sqlupdate3 = "update OM_GZHSB set GZ_SHSTATE3='1',OM_GZSCBZ='1',GZ_SHTIME3='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',GZ_SHNOTE3='" + opinion3.Text.Trim() + "' where GZSH_BH='" + spbh + "'";
                    }
                    else if (rblthird.SelectedValue.ToString().Trim() == "2")
                    {
                        sqlupdate3 = "update OM_GZHSB set GZ_SHSTATE3='2',OM_GZSCBZ='2',GZ_SHTIME3='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',GZ_SHNOTE3='" + opinion3.Text.Trim() + "' where GZSH_BH='" + spbh + "'";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核意见！');", true);
                        return;
                    }
                    DBCallCommon.ExeSqlText(sqlupdate3);
                }

            }
            Response.Redirect("OM_GZQDSPdetail.aspx?idbh=" + spbh);
        }


        //反审
        protected void btnfanshen_OnClick(object sender, EventArgs e)
        {
            List<string> list0 = new List<string>();
            spbh = Request.QueryString["idbh"].ToString().Trim();
            string sqltext = "update OM_GZHSB set OM_GZSCBZ=NULL,GZ_SHSTATE1='0',GZ_SHSTATE2='0',GZ_SHSTATE2='0' where GZSH_BH='" + spbh + "'";
            list0.Add(sqltext);
            DBCallCommon.ExecuteTrans(list0);
            Response.Redirect("OM_GZQDSPdetail.aspx?idbh=" + spbh);
        }

        protected void rblSHJS_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            bindkjkyx();
        }
    }
}
