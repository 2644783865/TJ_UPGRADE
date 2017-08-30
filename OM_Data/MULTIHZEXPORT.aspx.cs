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
    public partial class MULTIHZEXPORT : BasicPage
    {
        PagerQueryParamGroupBy pager_org = new PagerQueryParamGroupBy();
        //PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //工资查询记录
                string dengluid = Session["UserID"].ToString();
                string dengluname = Session["UserName"].ToString();
                string ym = "工资数据综合查询";
                string sql = "insert OM_GZ_JL (USER_ID,USER_NAME,TIME,YM) values ('" + dengluid + "','" + dengluname + "','" + DateTime.Now.ToString() + "','" + ym + "')";
                DBCallCommon.ExeSqlText(sql);
                bindgouxuanbm();
                bindgouxuanbz();
                bindgouxuangwxl();
                bindhetongzhuti();
                GetSele();
                //UCPaging1.CurrentPage = 1;
                //this.InitVar();
                //this.bindrpt();
                //Acount();
            }
            CheckUser(ControlFinder);
            this.InitVar();
        }

        private void bindgouxuanbm()
        {
            string sqlbumen = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO WHERE len(DEP_CODE)=2";
            System.Data.DataTable dtbumen = DBCallCommon.GetDTUsingSqlText(sqlbumen);
            listdepartment.DataTextField = "DEP_NAME";
            listdepartment.DataValueField = "DEP_CODE";
            listdepartment.DataSource = dtbumen;
            listdepartment.DataBind();
            ListItem item = new ListItem("全部", "");
            listdepartment.Items.Insert(0, item);
        }

        private void bindgouxuanbz()
        {
            string sqlbanzu = "SELECT DISTINCT ST_DEPID1 FROM TBDS_STAFFINFO where ST_DEPID1!='' and ST_DEPID1 is not null and ST_DEPID1!='0'";
            System.Data.DataTable dtbanzu = DBCallCommon.GetDTUsingSqlText(sqlbanzu);
            listbanzu.DataTextField = "ST_DEPID1";
            listbanzu.DataValueField = "ST_DEPID1";
            listbanzu.DataSource = dtbanzu;
            listbanzu.DataBind();
            ListItem item = new ListItem("全部", "");
            listbanzu.Items.Insert(0, item);
        }

        private void bindgouxuangwxl()
        {
            string sqlposition = "SELECT DISTINCT ST_SEQUEN FROM TBDS_STAFFINFO WHERE ST_SEQUEN is not null and ST_SEQUEN!=''";
            System.Data.DataTable dtposition = DBCallCommon.GetDTUsingSqlText(sqlposition);
            listposition.DataTextField = "ST_SEQUEN";
            listposition.DataValueField = "ST_SEQUEN";
            listposition.DataSource = dtposition;
            listposition.DataBind();
            ListItem item = new ListItem("全部", "");
            listposition.Items.Insert(0, item);
        }

        private void bindhetongzhuti()
        {
            string sqlhetongzhuti = "SELECT DISTINCT ST_CONTR FROM TBDS_STAFFINFO where ST_CONTR is not null and ST_CONTR!=''";
            System.Data.DataTable dthetongzhuti = DBCallCommon.GetDTUsingSqlText(sqlhetongzhuti);
            listhetongzhuti.DataTextField = "ST_CONTR";
            listhetongzhuti.DataValueField = "ST_CONTR";
            listhetongzhuti.DataSource = dthetongzhuti;
            listhetongzhuti.DataBind();
            ListItem item = new ListItem("全部", "");
            listhetongzhuti.Items.Insert(0, item);
        }



        /// <summary>
        /// 初始化分布信息
        /// </summary>
        //private void InitVar()
        //{
        //    InitPager();
        //    UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
        //    UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数

        //}

        /// <summary>
        /// 分页初始化
        /// </summary>
        /// <param name="where"></param>
        //private void InitPager()
        //{
        //    pager_org.TableName = "(select * from View_OM_GZQD left join OM_KQTJ on (View_OM_GZQD.QD_STID=OM_KQTJ.KQ_ST_ID and View_OM_GZQD.QD_YEARMONTH=OM_KQTJ.KQ_DATE))t";
        //    pager_org.PrimaryKey = "QD_ID";
        //    pager_org.ShowFields = "*,(QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY) as QD_YFHJ,(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG) as QD_DaiKouXJ,((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG)) as QD_ShiFaJE,((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ)-QD_KOUXIANG) as QD_KOUSJS ";
        //    pager_org.OrderField = "DEP_NAME,ST_WORKNO";
        //    pager_org.StrWhere = StrWhere();
        //    pager_org.OrderType = 1;//升序排列
        //    pager_org.PageSize = Convert.ToInt32(DropDownListCount.SelectedValue.ToString().Trim());
        //}

        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }

        private void InitPager()
        {
            pager_org.TableName = "(select * from View_OM_GZQD left join OM_KQTJ on (View_OM_GZQD.QD_STID=OM_KQTJ.KQ_ST_ID and View_OM_GZQD.QD_YEARMONTH=OM_KQTJ.KQ_DATE))t";
            pager_org.PrimaryKey = "QD_STID";
            pager_org.ShowFields = "QD_STID,ST_WORKNO,ST_NAME,QD_HTZT,DEP_NAME,ST_DEPID1,DEP_NAME_POSITION,sum(KQ_CHUQIN) as KQ_CHUQIN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_NIANX) as KQ_NIANX,sum(QD_JCGZ) as QD_JCGZ,sum(QD_GZGL) as QD_GZGL,sum(QD_GDGZ) as QD_GDGZ,sum(QD_JXGZ) as QD_JXGZ,sum(QD_JiangLi) as QD_JiangLi,sum(QD_BingJiaGZ) as QD_BingJiaGZ,sum(QD_JiaBanGZ) as QD_JiaBanGZ,sum(QD_BFJB) as QD_BFJB,sum(QD_ZYBF) as QD_ZYBF,sum(QD_BFZYB) as QD_BFZYB,sum(QD_NianJiaGZ) as QD_NianJiaGZ,sum(QD_YKGW) as QD_YKGW,sum(QD_TZBF) as QD_TZBF,sum(QD_TZBK) as QD_TZBK,sum(QD_JTBT) as QD_JTBT,sum(QD_FSJW) as QD_FSJW,sum(QD_CLBT) as QD_CLBT,sum(QD_QTFY) as QD_QTFY,sum(QD_YLBX) as QD_YLBX,sum(QD_SYBX) as QD_SYBX,sum(QD_YiLiaoBX) as QD_YiLiaoBX,sum(QD_DEJZ) as QD_DEJZ,sum(QD_BuBX) as QD_BuBX,sum(QD_GJJ) as QD_GJJ,sum(QD_BGJJ) as QD_BGJJ,sum(QD_ShuiDian) as QD_ShuiDian,sum(QD_GeShui) as QD_GeShui,sum(QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY) as QD_YFHJ,sum(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG) as QD_DaiKouXJ,sum((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG)) as QD_ShiFaJE,sum((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ)-QD_KOUXIANG) as QD_KOUSJS";
            pager_org.OrderField = "DEP_NAME,ST_WORKNO";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;
            pager_org.PageSize = 300;
            pager_org.GroupField = "QD_STID,ST_WORKNO,ST_NAME,QD_HTZT,DEP_NAME,ST_DEPID1,DEP_NAME_POSITION";
        }

        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            string sql = "1=1 and OM_GZSCBZ='1'";
            if (txtName.Text.Trim() != "")
            {
                string[] strarray = txtName.Text.Trim().Split(',', '，');
                string strname = "";
                for (int i = 0; i < strarray.Length; i++)
                {
                    strname += "'" + strarray[i] + "',";
                }
                strname = strname.Substring(0, strname.Length - 1);
                sql += " and ST_NAME in(" + strname + ")";
            }
            if (txtworkno.Text.Trim() != "")
            {
                sql += " and ST_WORKNO like '%" + txtworkno.Text.Trim() + "%'";
            }
            if (startdate.Value.Trim() != "")
            {
                sql += " and QD_YEARMONTH>='" + startdate.Value.Trim() + "'";
            }
            if (enddate.Value.Trim() != "")
            {
                sql += " and QD_YEARMONTH<='" + enddate.Value.Trim() + "'";
            }
            //部门
            for (int i = 0; i < listdepartment.Items.Count; i++)
            {
                if (listdepartment.Items[i].Selected == true)
                {
                    num1++;
                }
            }
            if (num1 > 0)
            {
                sql += " and (ST_DEPID='isnotexist'";
                for (int i = 0; i < listdepartment.Items.Count; i++)
                {
                    if (listdepartment.Items[i].Selected == true)
                    {
                        sql += " or ST_DEPID like '%" + listdepartment.Items[i].Value + "%'";
                    }
                }
                sql += ")";
            }
            //班组
            for (int j = 0; j < listbanzu.Items.Count; j++)
            {
                if (listbanzu.Items[j].Selected == true)
                {
                    num2++;
                }
            }
            if (num2 > 0)
            {
                sql += " and (ST_DEPID1='isnotexist'";
                for (int j = 0; j < listbanzu.Items.Count; j++)
                {
                    if (listbanzu.Items[j].Selected == true)
                    {
                        sql += " or ST_DEPID1 like '%" + listbanzu.Items[j].Value + "%'";
                    }
                }
                sql += ")";
            }

            //岗位序列
            for (int k = 0; k < listposition.Items.Count; k++)
            {
                if (listposition.Items[k].Selected == true)
                {
                    num3++;
                }
            }
            if (num3 > 0)
            {
                sql += " and (ST_SEQUEN='isnotexist'";
                for (int k = 0; k < listposition.Items.Count; k++)
                {
                    if (listposition.Items[k].Selected == true)
                    {
                        sql += " or ST_SEQUEN like '%" + listposition.Items[k].Value + "%'";
                    }
                }
                sql += ")";
            }

            //合同主体
            for (int m = 0; m < listhetongzhuti.Items.Count; m++)
            {
                if (listhetongzhuti.Items[m].Selected == true)
                {
                    num4++;
                }
            }
            if (num4 > 0)
            {
                sql += " and (QD_HTZT='isnotexist'";
                for (int m = 0; m < listhetongzhuti.Items.Count; m++)
                {
                    if (listhetongzhuti.Items[m].Selected == true)
                    {
                        sql += " or QD_HTZT like '%" + listhetongzhuti.Items[m].Value + "%'";
                    }
                }
                sql += ")";
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
        //private void Pager_PageChanged(int pageNumber)
        //{
        //    bindrpt();
        //}

        //private void bindrpt()
        //{
        //    InitPager();
        //    pager_org.PageIndex = UCPaging1.CurrentPage;
        //    System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
        //    CommonFun.Paging(dt, rptGZQD, UCPaging1, palNodata);
        //    if (palNodata.Visible)
        //    {
        //        UCPaging1.Visible = false;
        //    }
        //    else
        //    {
        //        UCPaging1.Visible = true;
        //        UCPaging1.InitPageInfo();
        //    }
        //}
        void Pager_PageChanged(int pageNumber)
        {
            bindrpt();
        }
        private void bindrpt()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamGroupBy(pager_org);
            CommonFun.Paging(dt, rptGZQD, UCPaging1, palNodata);
            if (palNodata.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
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
            Acount();
        }

        //显示条数
        protected void Count_Change(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
            Acount();
        }


        protected void btn_confirm1_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
            Acount();
        }

        protected void btn_clear1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listdepartment.Items.Count; i++)
            {
                listdepartment.Items[i].Selected = false;
            }
            for (int j = 0; j < listbanzu.Items.Count; j++)
            {
                listbanzu.Items[j].Selected = false;
            }
            for (int k = 0; k < listposition.Items.Count; k++)
            {
                listposition.Items[k].Selected = false;
            }
            UCPaging1.CurrentPage = 1;
            bindrpt();
            Acount();
        }

        protected void btn_confirm2_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
            Acount();
        }

        protected void btn_clear2_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < listhetongzhuti.Items.Count; k++)
            {
                listhetongzhuti.Items[k].Selected = false;
            }
            UCPaging1.CurrentPage = 1;
            bindrpt();
            Acount();
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


        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDaochu_OnClick(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext += "select ST_WORKNO,ST_NAME,QD_HTZT,QD_QFBS,DEP_NAME,ST_DEPID1,DEP_NAME_POSITION,sum(KQ_CHUQIN) as KQ_CHUQIN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_NIANX) as KQ_NIANX,sum(QD_JCGZ) as QD_JCGZ,sum(QD_GZGL) as QD_GZGL,sum(QD_GDGZ) as QD_GDGZ,sum(QD_JXGZ) as QD_JXGZ,sum(QD_JiangLi) as QD_JiangLi,sum(QD_BingJiaGZ) as QD_BingJiaGZ,sum(QD_JiaBanGZ) as QD_JiaBanGZ,sum(QD_ZYBF) as QD_ZYBF,sum(QD_NianJiaGZ) as QD_NianJiaGZ,sum(QD_YKGW) as QD_YKGW,sum(QD_TZBF) as QD_TZBF,sum(QD_TZBK) as QD_TZBK,sum(QD_JTBT) as QD_JTBT,sum(QD_FSJW) as QD_FSJW,sum(QD_CLBT) as QD_CLBT,sum(QD_QTFY) as QD_QTFY,sum(QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY) as QD_YFHJ,sum(QD_YLBX) as QD_YLBX,sum(QD_SYBX) as QD_SYBX,sum(QD_YiLiaoBX) as QD_YiLiaoBX,sum(QD_DEJZ) as QD_DEJZ,sum(QD_BuBX) as QD_BuBX,sum(QD_GJJ) as QD_GJJ,sum(QD_BGJJ) as QD_BGJJ,sum(QD_ShuiDian) as QD_ShuiDian,sum(QD_KOUXIANG) as QD_KOUXIANG,sum(QD_GeShui) as QD_GeShui,sum(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG) as QD_DaiKouXJ,sum((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG)) as QD_ShiFaJE from (select * from View_OM_GZQD left join OM_KQTJ on (View_OM_GZQD.QD_STID=OM_KQTJ.KQ_ST_ID and View_OM_GZQD.QD_YEARMONTH=OM_KQTJ.KQ_DATE))t where " + StrWhere() + " group by QD_STID,ST_WORKNO,ST_NAME,QD_HTZT,QD_QFBS,DEP_NAME,ST_DEPID1,DEP_NAME_POSITION order by DEP_NAME,ST_WORKNO asc";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }

        private void ExportDataItem(System.Data.DataTable objdt)
        {
            string filename = "工资清单汇总" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("工资清单汇总.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2);
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));
                    row.CreateCell(1).SetCellValue(objdt.Rows[i]["ST_WORKNO"].ToString().Trim());
                    row.CreateCell(2).SetCellValue(objdt.Rows[i]["ST_NAME"].ToString().Trim());
                    row.CreateCell(3).SetCellValue(objdt.Rows[i]["QD_HTZT"].ToString().Trim());

                    row.CreateCell(4).SetCellValue(objdt.Rows[i]["QD_QFBS"].ToString().Trim());
                    row.CreateCell(5).SetCellValue(objdt.Rows[i]["DEP_NAME"].ToString().Trim());
                    row.CreateCell(6).SetCellValue(objdt.Rows[i]["ST_DEPID1"].ToString().Trim());
                    row.CreateCell(7).SetCellValue(objdt.Rows[i]["DEP_NAME_POSITION"].ToString().Trim());
                    row.CreateCell(8).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["KQ_CHUQIN"].ToString().Trim()));
                    row.CreateCell(9).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["KQ_JRJIAB"].ToString().Trim()));
                    row.CreateCell(10).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["KQ_ZMJBAN"].ToString().Trim()));
                    row.CreateCell(11).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["KQ_YSGZ"].ToString().Trim()));

                    row.CreateCell(12).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["KQ_YEBAN"].ToString().Trim()));

                    row.CreateCell(13).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["KQ_BINGJ"].ToString().Trim()));
                    row.CreateCell(14).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["KQ_SHIJ"].ToString().Trim()));
                    row.CreateCell(15).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["KQ_NIANX"].ToString().Trim()));
                    row.CreateCell(16).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_JCGZ"].ToString().Trim()));
                    row.CreateCell(17).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_GZGL"].ToString().Trim()));
                    row.CreateCell(18).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_GDGZ"].ToString().Trim()));
                    row.CreateCell(19).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_JXGZ"].ToString().Trim()));
                    row.CreateCell(20).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_JiangLi"].ToString().Trim()));
                    row.CreateCell(21).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_BingJiaGZ"].ToString().Trim()));
                    row.CreateCell(22).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_JiaBanGZ"].ToString().Trim()));
                    row.CreateCell(23).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_ZYBF"].ToString().Trim()));
                    row.CreateCell(24).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_NianJiaGZ"].ToString().Trim()));
                    row.CreateCell(25).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_YKGW"].ToString().Trim()));
                    row.CreateCell(26).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_TZBF"].ToString().Trim()));
                    row.CreateCell(27).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_TZBK"].ToString().Trim()));
                    row.CreateCell(28).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_JTBT"].ToString().Trim()));
                    row.CreateCell(29).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_FSJW"].ToString().Trim()));
                    row.CreateCell(30).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_CLBT"].ToString().Trim()));
                    row.CreateCell(31).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_QTFY"].ToString().Trim()));
                    row.CreateCell(32).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_YFHJ"].ToString().Trim()));//应发合计
                    row.CreateCell(33).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_YLBX"].ToString().Trim()));
                    row.CreateCell(34).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_SYBX"].ToString().Trim()));
                    row.CreateCell(35).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_YiLiaoBX"].ToString().Trim()));
                    row.CreateCell(36).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_DEJZ"].ToString().Trim()));
                    row.CreateCell(37).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_BuBX"].ToString().Trim()));
                    row.CreateCell(38).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_GJJ"].ToString().Trim()));
                    row.CreateCell(39).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_BGJJ"].ToString().Trim()));
                    row.CreateCell(40).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_ShuiDian"].ToString().Trim()));
                    row.CreateCell(41).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_KOUXIANG"].ToString().Trim()));
                    row.CreateCell(42).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_GeShui"].ToString().Trim()));
                    row.CreateCell(43).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_DaiKouXJ"].ToString().Trim()));//代扣小计
                    row.CreateCell(44).SetCellValue(CommonFun.ComTryDouble(objdt.Rows[i]["QD_ShiFaJE"].ToString().Trim()));//实发金额
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

        private void Acount()
        {
            //按部门汇总
            string sqltext1 = "select ST_DEPID,DEP_NAME,count(*) as pepcount,sum(QD_ShiFaJE) as totalgz,cast(avg(QD_ShiFaJE) as decimal(12,2)) as avggz from (select QD_STID,ST_WORKNO,ST_NAME,QD_HTZT,DEP_NAME,ST_DEPID,ST_DEPID1,DEP_NAME_POSITION,sum(KQ_CHUQIN) as KQ_CHUQIN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_NIANX) as KQ_NIANX,sum(QD_JCGZ) as QD_JCGZ,sum(QD_GZGL) as QD_GZGL,sum(QD_GDGZ) as QD_GDGZ,sum(QD_JXGZ) as QD_JXGZ,sum(QD_JiangLi) as QD_JiangLi,sum(QD_BingJiaGZ) as QD_BingJiaGZ,sum(QD_JiaBanGZ) as QD_JiaBanGZ,sum(QD_BFJB) as QD_BFJB,sum(QD_ZYBF) as QD_ZYBF,sum(QD_BFZYB) as QD_BFZYB,sum(QD_NianJiaGZ) as QD_NianJiaGZ,sum(QD_YKGW) as QD_YKGW,sum(QD_TZBF) as QD_TZBF,sum(QD_TZBK) as QD_TZBK,sum(QD_JTBT) as QD_JTBT,sum(QD_FSJW) as QD_FSJW,sum(QD_CLBT) as QD_CLBT,sum(QD_QTFY) as QD_QTFY,sum(QD_YLBX) as QD_YLBX,sum(QD_SYBX) as QD_SYBX,sum(QD_YiLiaoBX) as QD_YiLiaoBX,sum(QD_DEJZ) as QD_DEJZ,sum(QD_BuBX) as QD_BuBX,sum(QD_GJJ) as QD_GJJ,sum(QD_BGJJ) as QD_BGJJ,sum(QD_ShuiDian) as QD_ShuiDian,sum(QD_GeShui) as QD_GeShui,sum(QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY) as QD_YFHJ,sum(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG) as QD_DaiKouXJ,sum((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG)) as QD_ShiFaJE,sum((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ)-QD_KOUXIANG) as QD_KOUSJS from (select * from View_OM_GZQD left join OM_KQTJ on (View_OM_GZQD.QD_STID=OM_KQTJ.KQ_ST_ID and View_OM_GZQD.QD_YEARMONTH=OM_KQTJ.KQ_DATE))t where " + StrWhere() + " group by QD_STID,ST_WORKNO,ST_NAME,QD_HTZT,DEP_NAME,ST_DEPID,ST_DEPID1,DEP_NAME_POSITION)s group by DEP_NAME,ST_DEPID order by ST_DEPID asc";
            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext1);
            if (dt1.Rows.Count > 0)
            {
                rep_TJ1.DataSource = dt1;
                rep_TJ1.DataBind();
            }

            //按班组汇总
            string sqltext2 = "select ST_DEPID1,count(*) as pepcount,sum(QD_ShiFaJE) as totalgz,cast(avg(QD_ShiFaJE) as decimal(12,2)) as avggz from (select QD_STID,ST_WORKNO,ST_NAME,QD_HTZT,DEP_NAME,ST_DEPID,isnull(ST_DEPID1,'') as ST_DEPID1,DEP_NAME_POSITION,sum(KQ_CHUQIN) as KQ_CHUQIN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_NIANX) as KQ_NIANX,sum(QD_JCGZ) as QD_JCGZ,sum(QD_GZGL) as QD_GZGL,sum(QD_GDGZ) as QD_GDGZ,sum(QD_JXGZ) as QD_JXGZ,sum(QD_JiangLi) as QD_JiangLi,sum(QD_BingJiaGZ) as QD_BingJiaGZ,sum(QD_JiaBanGZ) as QD_JiaBanGZ,sum(QD_BFJB) as QD_BFJB,sum(QD_ZYBF) as QD_ZYBF,sum(QD_BFZYB) as QD_BFZYB,sum(QD_NianJiaGZ) as QD_NianJiaGZ,sum(QD_YKGW) as QD_YKGW,sum(QD_TZBF) as QD_TZBF,sum(QD_TZBK) as QD_TZBK,sum(QD_JTBT) as QD_JTBT,sum(QD_FSJW) as QD_FSJW,sum(QD_CLBT) as QD_CLBT,sum(QD_QTFY) as QD_QTFY,sum(QD_YLBX) as QD_YLBX,sum(QD_SYBX) as QD_SYBX,sum(QD_YiLiaoBX) as QD_YiLiaoBX,sum(QD_DEJZ) as QD_DEJZ,sum(QD_BuBX) as QD_BuBX,sum(QD_GJJ) as QD_GJJ,sum(QD_BGJJ) as QD_BGJJ,sum(QD_ShuiDian) as QD_ShuiDian,sum(QD_GeShui) as QD_GeShui,sum(QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY) as QD_YFHJ,sum(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG) as QD_DaiKouXJ,sum((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG)) as QD_ShiFaJE,sum((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ)-QD_KOUXIANG) as QD_KOUSJS from (select * from View_OM_GZQD left join OM_KQTJ on (View_OM_GZQD.QD_STID=OM_KQTJ.KQ_ST_ID and View_OM_GZQD.QD_YEARMONTH=OM_KQTJ.KQ_DATE))t where " + StrWhere() + " group by QD_STID,ST_WORKNO,ST_NAME,QD_HTZT,DEP_NAME,ST_DEPID,ST_DEPID1,DEP_NAME_POSITION)s group by ST_DEPID1 order by ST_DEPID1 desc";
            System.Data.DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext2);
            if (dt2.Rows.Count > 0)
            {
                rep_TJ2.DataSource = dt2;
                rep_TJ2.DataBind();
            }


            //按岗位序列汇总
            string sqltext3 = "select ST_SEQUEN,count(*) as pepcount,sum(QD_ShiFaJE) as totalgz,cast(avg(QD_ShiFaJE) as decimal(12,2)) as avggz from (select QD_STID,ST_WORKNO,ST_NAME,QD_HTZT,ST_SEQUEN,DEP_NAME,ST_DEPID,ST_DEPID1,DEP_NAME_POSITION,sum(KQ_CHUQIN) as KQ_CHUQIN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_NIANX) as KQ_NIANX,sum(QD_JCGZ) as QD_JCGZ,sum(QD_GZGL) as QD_GZGL,sum(QD_GDGZ) as QD_GDGZ,sum(QD_JXGZ) as QD_JXGZ,sum(QD_JiangLi) as QD_JiangLi,sum(QD_BingJiaGZ) as QD_BingJiaGZ,sum(QD_JiaBanGZ) as QD_JiaBanGZ,sum(QD_BFJB) as QD_BFJB,sum(QD_ZYBF) as QD_ZYBF,sum(QD_BFZYB) as QD_BFZYB,sum(QD_NianJiaGZ) as QD_NianJiaGZ,sum(QD_YKGW) as QD_YKGW,sum(QD_TZBF) as QD_TZBF,sum(QD_TZBK) as QD_TZBK,sum(QD_JTBT) as QD_JTBT,sum(QD_FSJW) as QD_FSJW,sum(QD_CLBT) as QD_CLBT,sum(QD_QTFY) as QD_QTFY,sum(QD_YLBX) as QD_YLBX,sum(QD_SYBX) as QD_SYBX,sum(QD_YiLiaoBX) as QD_YiLiaoBX,sum(QD_DEJZ) as QD_DEJZ,sum(QD_BuBX) as QD_BuBX,sum(QD_GJJ) as QD_GJJ,sum(QD_BGJJ) as QD_BGJJ,sum(QD_ShuiDian) as QD_ShuiDian,sum(QD_GeShui) as QD_GeShui,sum(QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY) as QD_YFHJ,sum(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG) as QD_DaiKouXJ,sum((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG)) as QD_ShiFaJE,sum((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ)-QD_KOUXIANG) as QD_KOUSJS from (select * from View_OM_GZQD left join OM_KQTJ on (View_OM_GZQD.QD_STID=OM_KQTJ.KQ_ST_ID and View_OM_GZQD.QD_YEARMONTH=OM_KQTJ.KQ_DATE))t where " + StrWhere() + " group by QD_STID,ST_WORKNO,ST_NAME,QD_HTZT,ST_SEQUEN,DEP_NAME,ST_DEPID,ST_DEPID1,DEP_NAME_POSITION)s group by ST_SEQUEN order by ST_SEQUEN desc";
            System.Data.DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sqltext3);
            if (dt3.Rows.Count > 0)
            {
                rep_TJ3.DataSource = dt3;
                rep_TJ3.DataBind();
            }


            //按合同主体汇总
            string sqltext4 = "select QD_HTZT,count(*) as pepcount,sum(QD_ShiFaJE) as totalgz,cast(avg(QD_ShiFaJE) as decimal(12,2)) as avggz from (select QD_STID,ST_WORKNO,ST_NAME,QD_HTZT,DEP_NAME,ST_DEPID,ST_DEPID1,DEP_NAME_POSITION,sum(KQ_CHUQIN) as KQ_CHUQIN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_NIANX) as KQ_NIANX,sum(QD_JCGZ) as QD_JCGZ,sum(QD_GZGL) as QD_GZGL,sum(QD_GDGZ) as QD_GDGZ,sum(QD_JXGZ) as QD_JXGZ,sum(QD_JiangLi) as QD_JiangLi,sum(QD_BingJiaGZ) as QD_BingJiaGZ,sum(QD_JiaBanGZ) as QD_JiaBanGZ,sum(QD_BFJB) as QD_BFJB,sum(QD_ZYBF) as QD_ZYBF,sum(QD_BFZYB) as QD_BFZYB,sum(QD_NianJiaGZ) as QD_NianJiaGZ,sum(QD_YKGW) as QD_YKGW,sum(QD_TZBF) as QD_TZBF,sum(QD_TZBK) as QD_TZBK,sum(QD_JTBT) as QD_JTBT,sum(QD_FSJW) as QD_FSJW,sum(QD_CLBT) as QD_CLBT,sum(QD_QTFY) as QD_QTFY,sum(QD_YLBX) as QD_YLBX,sum(QD_SYBX) as QD_SYBX,sum(QD_YiLiaoBX) as QD_YiLiaoBX,sum(QD_DEJZ) as QD_DEJZ,sum(QD_BuBX) as QD_BuBX,sum(QD_GJJ) as QD_GJJ,sum(QD_BGJJ) as QD_BGJJ,sum(QD_ShuiDian) as QD_ShuiDian,sum(QD_GeShui) as QD_GeShui,sum(QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY) as QD_YFHJ,sum(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG) as QD_DaiKouXJ,sum((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG)) as QD_ShiFaJE,sum((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ)-QD_KOUXIANG) as QD_KOUSJS from (select * from View_OM_GZQD left join OM_KQTJ on (View_OM_GZQD.QD_STID=OM_KQTJ.KQ_ST_ID and View_OM_GZQD.QD_YEARMONTH=OM_KQTJ.KQ_DATE))t where " + StrWhere() + " group by QD_STID,ST_WORKNO,ST_NAME,QD_HTZT,DEP_NAME,ST_DEPID,ST_DEPID1,DEP_NAME_POSITION)s group by QD_HTZT order by QD_HTZT desc";
            System.Data.DataTable dt4 = DBCallCommon.GetDTUsingSqlText(sqltext4);
            if (dt4.Rows.Count > 0)
            {
                rep_TJ4.DataSource = dt4;
                rep_TJ4.DataBind();
            }


            //合计
            string sqltexthj = "select count(*) as pepcount,sum(QD_ShiFaJE) as totalgz from (select QD_STID,ST_WORKNO,ST_NAME,QD_HTZT,DEP_NAME,ST_DEPID,ST_DEPID1,DEP_NAME_POSITION,sum(KQ_CHUQIN) as KQ_CHUQIN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_NIANX) as KQ_NIANX,sum(QD_JCGZ) as QD_JCGZ,sum(QD_GZGL) as QD_GZGL,sum(QD_GDGZ) as QD_GDGZ,sum(QD_JXGZ) as QD_JXGZ,sum(QD_JiangLi) as QD_JiangLi,sum(QD_BingJiaGZ) as QD_BingJiaGZ,sum(QD_JiaBanGZ) as QD_JiaBanGZ,sum(QD_BFJB) as QD_BFJB,sum(QD_ZYBF) as QD_ZYBF,sum(QD_BFZYB) as QD_BFZYB,sum(QD_NianJiaGZ) as QD_NianJiaGZ,sum(QD_YKGW) as QD_YKGW,sum(QD_TZBF) as QD_TZBF,sum(QD_TZBK) as QD_TZBK,sum(QD_JTBT) as QD_JTBT,sum(QD_FSJW) as QD_FSJW,sum(QD_CLBT) as QD_CLBT,sum(QD_QTFY) as QD_QTFY,sum(QD_YLBX) as QD_YLBX,sum(QD_SYBX) as QD_SYBX,sum(QD_YiLiaoBX) as QD_YiLiaoBX,sum(QD_DEJZ) as QD_DEJZ,sum(QD_BuBX) as QD_BuBX,sum(QD_GJJ) as QD_GJJ,sum(QD_BGJJ) as QD_BGJJ,sum(QD_ShuiDian) as QD_ShuiDian,sum(QD_GeShui) as QD_GeShui,sum(QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY) as QD_YFHJ,sum(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG) as QD_DaiKouXJ,sum((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG)) as QD_ShiFaJE,sum((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ)-QD_KOUXIANG) as QD_KOUSJS from (select * from View_OM_GZQD left join OM_KQTJ on (View_OM_GZQD.QD_STID=OM_KQTJ.KQ_ST_ID and View_OM_GZQD.QD_YEARMONTH=OM_KQTJ.KQ_DATE))t where " + StrWhere() + " group by QD_STID,ST_WORKNO,ST_NAME,QD_HTZT,DEP_NAME,ST_DEPID,ST_DEPID1,DEP_NAME_POSITION)s";
            System.Data.DataTable dthj = DBCallCommon.GetDTUsingSqlText(sqltexthj);
            if (dthj.Rows.Count > 0)
            {
                bm_rshj.InnerText = dthj.Rows[0]["pepcount"].ToString().Trim();
                bm_jehj.InnerText = dthj.Rows[0]["totalgz"].ToString().Trim();

                bz_rshj.InnerText = dthj.Rows[0]["pepcount"].ToString().Trim();
                bz_jehj.InnerText = dthj.Rows[0]["totalgz"].ToString().Trim();

                gwxl_rshj.InnerText = dthj.Rows[0]["pepcount"].ToString().Trim();
                gwxl_jehj.InnerText = dthj.Rows[0]["totalgz"].ToString().Trim();

                htzt_rshj.InnerText = dthj.Rows[0]["pepcount"].ToString().Trim();
                htzt_jehj.InnerText = dthj.Rows[0]["totalgz"].ToString().Trim();
            }
        }
    }
}
