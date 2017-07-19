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
using System.Collections.Generic;
using System.IO;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PC_Data
{
    public partial class TBPC_IQRCMPPRCLST_checked : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();

        ////页数
        public int ObjPageSize
        {
            get
            {
                //默认是升序
                ViewState["ObjPageSize"] = Convert.ToDouble(DropDownList5.SelectedValue);
                return Convert.ToInt32(ViewState["ObjPageSize"]);
            }
            set
            {
                ViewState["ObjPageSize"] = value;
            }
        }

        public string gloabstate
        {
            get
            {
                object str = ViewState["gloabstate"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabstate"] = value;
            }
        }
        public string gloabsheetno
        {
            get
            {
                object str = ViewState["gloabsheetno"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabsheetno"] = value;
            }
        }

        public string gloabptc//计划跟踪号
        {
            get
            {
                object str = ViewState["gloabptc"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabptc"] = value;
            }
        }
        public string NUM1
        {
            get
            {
                object str = ViewState["NUM1"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["NUM1"] = value;
            }
        }
        public string NUM2
        {
            get
            {
                object str = ViewState["NUM2"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["NUM2"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = btn_search.UniqueID;  //2013年6月25日 16:25:07  Meng   回车键为查找
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            btn_delete.Attributes.Add("OnClick", "Javascript:return confirm('确定删除,执行此操作将直接删除选中物料?');");
            btn_cancel.Attributes.Add("OnClick", "Javascript:return confirm('确定取消,执行此操作将停止对选中物料的询比价?');");
            ViewState["ObjPageSize"] = Convert.ToDouble(DropDownList5.SelectedValue);

            if (!IsPostBack)
            {
                hid_filter.Text = "View_TBPC_IQRCMPPRICE_RVW1" + "/" + Session["UserID"].ToString();
                string sf = "";
                if (Request.QueryString["sf"] != null)
                {
                    sf = Request.QueryString["sf"].ToString();
                }
                if (sf != "1")
                {
                    string sqltext = "";
                    sqltext = "delete from TBPC_FILTER_INFO where tableuser='" + hid_filter.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    sqltext = "delete from TBPC_COLUMNFILTER_INFO where tableuser='" + hid_filter.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                }
                initpage1();
                initpower1();
                getArticle(true);

            }
            getbh();
            Warn();
            CheckUser(ControlFinder);
        }

        private void Warn()
        {
            string sql = "";
            if (rad_mypart.Checked)
            {
                if (Session["UserID"].ToString() == "2")
                {
                    sql = string.Format("select distinct picno from View_TBPC_IQRCMPPRICE_RVW1 where {0}", "(ICL_STATEA='2' and ICL_STATEB='0') and (totalstate='1' or totalstate='3')");
                }
                else if (Session["UserID"].ToString() == "311")
                {
                    sql = string.Format("select distinct picno from View_TBPC_IQRCMPPRICE_RVW1 where {0}", "(ICL_STATEB='2' and ICL_STATEC='0') and (totalstate='1' or totalstate='3')");
                }
                else
                {
                    sql = string.Format("select distinct picno from View_TBPC_IQRCMPPRICE_RVW1 where {0}", "((zdrid='" + Session["UserID"].ToString() + "') or (shbid='" + Session["UserID"].ToString() + "' and ICL_STATEA='0')) and (totalstate='1' or totalstate='3')");
                }
            }
            else
            {
                sql = "select distinct picno from View_TBPC_IQRCMPPRICE_RVW1 where totalstate='1' or totalstate='3'";
            }
            WarnNum.Text = string.Format("（{0}）", DBCallCommon.GetDTUsingSqlText(sql).Rows.Count);
        }

        private void initpage1()
        {
            string sqltext = "";
            sqltext = "select ST_NAME,ST_ID from TBDS_STAFFINFO WHERE ST_DEPID='05' and ST_PD='0'";
            string dataText = "ST_NAME";
            string dataValue = "ST_ID";
            DBCallCommon.BindDdl(drp_stu, sqltext, dataText, dataValue);
            drp_stu.SelectedIndex = 0;
            if (Request.QueryString["num1"] != null)
            {
                NUM1 = Request.QueryString["num1"].ToString();
            }
            else
            {
                NUM1 = "";
            }
            if (Request.QueryString["num2"] != null)
            {
                NUM2 = Request.QueryString["num2"].ToString();
            }
            else
            {
                NUM2 = "";
            }
            if (NUM1 == "1")
            {
                rad_all.Checked = true;
                rad_mypart.Checked = false;
            }
            else if (NUM1 == "2")
            {
                rad_all.Checked = false;
                rad_mypart.Checked = true;
            }
            if (NUM2 == "1")
            {
                rad_quanbu.Checked = true; rad_weitijiao.Checked = false; rad_shenhezhong.Checked = false; rad_bohui.Checked = false; rad_tongguo.Checked = false; rad_weizxing.Checked = false; rad_yizhixing.Checked = false; rad_tongguodingdan.Checked = false;
            }
            else if (NUM2 == "2")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = true; rad_shenhezhong.Checked = false; rad_bohui.Checked = false; rad_tongguo.Checked = false; rad_weizxing.Checked = false; rad_yizhixing.Checked = false; rad_tongguodingdan.Checked = false;
            }
            else if (NUM2 == "3")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = false; rad_shenhezhong.Checked = true; rad_bohui.Checked = false; rad_tongguo.Checked = false; rad_weizxing.Checked = false; rad_yizhixing.Checked = false; rad_tongguodingdan.Checked = false;
            }
            else if (NUM2 == "4")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = false; rad_shenhezhong.Checked = false; rad_bohui.Checked = true; rad_tongguo.Checked = false; rad_weizxing.Checked = false; rad_yizhixing.Checked = false; rad_tongguodingdan.Checked = false;
            }
            else if (NUM2 == "5")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = false; rad_shenhezhong.Checked = false; rad_bohui.Checked = false; rad_tongguo.Checked = true; rad_weizxing.Checked = false; rad_yizhixing.Checked = false; rad_tongguodingdan.Checked = false;
            }
            else if (NUM2 == "6")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = false; rad_shenhezhong.Checked = false; rad_bohui.Checked = false; rad_tongguo.Checked = false; rad_weizxing.Checked = true; rad_yizhixing.Checked = false; rad_tongguodingdan.Checked = false;
            }
            else if (NUM2 == "7")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = false; rad_shenhezhong.Checked = false; rad_bohui.Checked = false; rad_tongguo.Checked = false; rad_weizxing.Checked = false; rad_yizhixing.Checked = true; rad_tongguodingdan.Checked = false;
            }
            else if (NUM2 == "8")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = false; rad_shenhezhong.Checked = false; rad_bohui.Checked = false; rad_tongguo.Checked = false; rad_weizxing.Checked = false; rad_yizhixing.Checked = false; rad_tongguodingdan.Checked = true;
            }

        }
        //判断审核人信息，若为审核人，则选择审核中。
        private void initpower1()
        {
            if (Session["UserID"].ToString() == "0601002" || Session["UserID"].ToString() == "0907001" || Session["UserID"].ToString() == "01002" || Session["UserID"].ToString() == "01001" || Session["UserID"].ToString() == "01003" || Session["UserID"].ToString() == "01004" || Session["UserID"].ToString() == "01006")
            {
                rad_shenhezhong.Checked = true;
                rad_weitijiao.Checked = false;
                rad_all.Checked = false;
                rad_mypart.Checked = true;
                cb_sp.Visible = true;
                cb_sp.Checked = true;   //如果有审批权限，则默认被选中
            }
        }
        private void BindPage(string tableName, string PrimaryKey, string ShowFields, string OrderField, string GroupField, int OrderType, string StrWhere, int PageSize, bool isFristPage)
        {

            InitVar(tableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFristPage);

        }


        private void InitVar(string tableName, string PrimaryKey, string ShowFields, string OrderField, string GroupField, int OrderType, string StrWhere, int PageSize, bool isFristPage)
        {

            InitPager(tableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize);//初始化页面

            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数

            if (isFristPage)
            {
                UCPaging1.CurrentPage = 1;
            }

            bindData();
        }

        //初始化分页信息
        private void InitPager(string tableName, string PrimaryKey, string ShowFields, string OrderField, string GroupField, int OrderType, string StrWhere, int PageSize)
        {
            pager.TableName = tableName;

            pager.PrimaryKey = PrimaryKey;

            pager.ShowFields = ShowFields;

            if (string.IsNullOrEmpty(GroupField))

                pager.OrderField = OrderField;
            else

                pager.OrderField = GroupField;

            //pager.GroupField = GroupField;

            pager.OrderType = OrderType;

            pager.StrWhere = StrWhere;

            pager.PageSize = PageSize;
        }

        protected void bindData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            if (Chk_HZ.Checked && dt.Rows.Count > 0)
            {
                List<string> sumlist = new List<string>() { "picno", "marid", "length", "width", "PIC_MASHAPE", "marunit", "marfzunit" };
                List<int> index = new List<int>();
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    DataRow drpre = dt.Rows[i - 1];
                    DataRow dr = dt.Rows[i];
                    bool b = true;
                    for (int j = 0; j < sumlist.Count; j++)
                    {
                        if (drpre[sumlist[j]].ToString() != dr[sumlist[j]].ToString())
                        {
                            b = false;
                        }
                    }
                    if (b)
                    {
                        index.Add(i);
                    }
                }
                for (int i = 0; i < index.Count; i++)
                {
                    dt.Rows[index[i]].Delete();
                }
            }
            CommonFun.Paging(dt, checked_list_Repeater, UCPaging1, NoDataPane);
            if (NoDataPane.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            //clearData();
        }

        void Pager_PageChanged(int pageNumber)
        {
            getArticle(false);
            ScriptManager.RegisterStartupScript(this.Page, GetType(), DateTime.Now.ToString("ssffff"), "sTable()", true);
        }
        protected void btn_search_click(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void btn_clear_click(object sender, EventArgs e)
        {
            //Tb_PJNAME.Text = "";
            //Tb_ENGNAME.Text = "";
            tb_marnm.Text = "";
            tb_ptc.Text = "";
            Tb_pcode.Text = "";
            drp_stu.SelectedIndex = 0;
        }
        private void getArticle(bool isFirstPage)      //取得Article数据
        {
            //string sql="select a.picno ,a.marid ,a.marnum,b.zdrid,b.zdrnm,substring(zdtime,1,10)aszdtime,b.iclzgid,CONVERT(varchar(12),irqdata,102)asirqdata,b.iclzgnm,b.iclfzrid,b.iclfzrnm,b.iclamount,b.totalcstate,b.totalstate,b.totalnote,b.supplierresid,b.supplierresnm,b.PIC_ID,b.pjnm,b.engnm,b.ptcode,CONVERT(float,price)asprice,b.shuilv,b.marnm,b.margg,b.margb,b.marcz,b.marunit,b.marfzunit,b.length,b.width,b.marfznum,b.marzxnum,b.PIC_TUHAO,b.PIC_MASHAPE,b.shbid,b.shcid,b.shdid,b.sheid,b.shfid,b.shgid,b.marzxfznum,isnull(detailstate,0) as detailstate,isnull(detailcstate,0) as detailcstate,b.detailnote,b.zdjnum,b.zdjprice,b.orderno,b.zxrid,b.zxrnm from (select picno,marid,sum(marnum) as marnum,sum(zdjnum) as zdjnum,length,width,PIC_MASHAPE,marunit from View_TBPC_IQRCMPPRICE_RVW1 group by picno,marid,length,width,PIC_MASHAPE,marunit)as a left join View_TBPC_IQRCMPPRICE_RVW1 as b on a.picno=b.picno and a.marid=b.marid and a.length=b.length and a.width=b.width and a.PIC_MASHAPE=b.PIC_MASHAPE and a.marunit=b.marunit"
            CreateDataSource();

            string TableName = "View_TBPC_IQRCMPPRICE_RVW1";

            string PrimaryKey = "";

            //string ShowFields = "picno, zdrid, zdrnm, substring(zdtime,1,10) as zdtime, iclzgid,substring(irqdata,1,10) as irqdata, iclzgnm, iclfzrid, iclfzrnm, iclamount, " +
            string ShowFields = "picno, zdrid, zdrnm, substring(zdtime,1,10) as zdtime, iclzgid,CONVERT(varchar(12) , irqdata, 102 ) as irqdata, iclzgnm, iclfzrid, iclfzrnm, iclamount, " +
                            " totalcstate, totalstate, totalnote, supplierresid, supplierresnm,PIC_ID,  " +
                            "pjnm, engnm, ptcode,CONVERT(float, price) as price, shuilv, marid, marnm, margg, margb, " +
                            "marcz, marunit, marfzunit, length, width, CONVERT(float, marnum) AS marnum, marfznum, marzxnum,case when margb='' then PIC_TUHAO else '' end as PIC_TUHAO,PIC_MASHAPE,shbid,shcid,shdid,sheid,shfid,shgid " +
                            "marzxfznum, isnull(detailstate,0) as detailstate, isnull(detailcstate,0) as detailcstate, detailnote, zdjnum, zdjprice, orderno, zxrid, zxrnm,sqrnm,PIC_MAP,PIC_CHILDENGNAME,PIC_IFFAST  ";


            //数据库中的主键
            string OrderField = "picno desc,ptcode";

            string GroupField = "";

            int OrderType = 0;
            /**/
            string StrWhere = ViewState["sqlwhere"].ToString();

            int PageSize = ObjPageSize;

            if (Chk_HZ.Checked)
            {
                TableName = "(select picno,marid,sum(marnum) as marnum,sum(zdjnum) as zdjnum,length,width,PIC_MASHAPE,marunit from View_TBPC_IQRCMPPRICE_RVW1 group by picno,marid,length,width,PIC_MASHAPE,marunit,marfzunit)as a left join View_TBPC_IQRCMPPRICE_RVW1 as b on a.picno=b.picno and a.marid=b.marid and a.length=b.length and a.width=b.width and a.PIC_MASHAPE=b.PIC_MASHAPE and a.marunit=b.marunit";
                ShowFields = "a.picno ,a.marid ,a.marnum,a.zdjnum,b.zdrid,b.zdrnm,substring(zdtime,1,10) as zdtime,b.iclzgid,CONVERT(varchar(12),irqdata,102) as irqdata,b.iclzgnm,b.iclfzrid,b.iclfzrnm,b.iclamount,b.totalcstate,b.totalstate,b.totalnote,b.supplierresid,b.supplierresnm,'' as PIC_ID,b.pjnm,'' as engnm,'' as ptcode,CONVERT(float,price) as price,b.shuilv,b.marnm,b.margg,b.margb,b.marcz,b.marunit,b.marfzunit,b.length,b.width,'' as marfznum,'' as marzxnum,'' as PIC_TUHAO,b.PIC_MASHAPE,b.shbid,b.shcid,b.shdid,b.sheid,b.shfid,b.shgid,'' as marzxfznum,isnull(detailstate,0) as detailstate,isnull(detailcstate,0) as detailcstate,b.detailnote,b.zdjprice,b.orderno,b.zxrid,b.zxrnm,b.sqrnm,b.PIC_MAP,b.PIC_CHILDENGNAME,PIC_IFFAST";
            }

            BindPage(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFirstPage);
            CheckUser(ControlFinder);
        }
        public void CreateDataSource()
        {
            string sqlwhere = "";
            string sqltext = "";
            string tableuser = hid_filter.Text;
            string filter = "";
            sqltext = "SELECT tableuser, filter FROM TBPC_FILTER_INFO where tableuser='" + tableuser + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                filter = dr[1].ToString();
            }
            dr.Close();

            //sqltext = "SELECT picno, zdrid, zdrnm, substring(zdtime,1,10) as zdtime, iclzgid,substring(irqdata,1,10) as irqdata, iclzgnm, iclfzrid, iclfzrnm, iclamount, " +
            //            " totalcstate, totalstate, totalnote, supplierresid, supplierresnm, suppliernm, CONVERT(float, firprice) AS firprice, " +
            //            "CONVERT(float, secprice) AS secprice, CONVERT(float, lasprice) AS lasprice, pjnm, engnm, ptcode, price, shuilv, marid, marnm, margg, margb, " +
            //            "marcz, marunit, marfzunit, length, width, CONVERT(float, marnum) AS marnum, marfznum, marzxnum,PIC_TUHAO,PIC_MASHAPE,shbid,shcid,shdid,sheid,shfid, " +
            //            "marzxfznum, isnull(detailstate,0) as detailstate, isnull(detailcstate,0) as detailcstate, detailnote, zdjnum, zdjprice, orderno, zxrid, zxrnm  " +
            //           "FROM View_TBPC_IQRCMPPRICE_RVW where " + filter + " order by picno desc,ptcode asc";

            sqlwhere = "1=1 ";
            if (filter != "")
            {
                sqlwhere = sqlwhere + " and " + filter + "";
            }
            if (rad_mypart.Checked)
            {
                if (rad_shenhezhong.Checked)
                {
                    if (Session["UserID"].ToString() == "2")
                    {
                        sqlwhere = sqlwhere + " and ((zdrid='" + Session["UserID"].ToString() + "') or (shbid='" + Session["UserID"].ToString() + "' and ICL_STATEA='0') or (ICL_STATEA='2' and ICL_STATEB='0' )) ";
                    }
                    else if (Session["UserID"].ToString() == "311")
                    {
                        sqlwhere = sqlwhere + " and ((zdrid='" + Session["UserID"].ToString() + "') or (shbid='" + Session["UserID"].ToString() + "' and ICL_STATEA='0') or (ICL_STATEA='2' and ICL_STATEB='2' and ICL_STATEC='0' )) ";
                    }
                    else
                    {
                        sqlwhere = sqlwhere + " and ((zdrid='" + Session["UserID"].ToString() + "') or (shbid='" + Session["UserID"].ToString() + "' and ICL_STATEA='0')) ";
                    }
                }
                else
                {
                    sqlwhere += " and (zdrid='" + Session["UserID"].ToString() + "')";
                }
                if (rad_bohui.Checked)
                {
                    sqlwhere = sqlwhere + " and ((zdrid='" + Session["UserID"].ToString() + "') or (shbid='" + Session["UserID"].ToString() + "' and ICL_STATEA='1') or (shcid='" + Session["UserID"].ToString() + "' and ICL_STATEB='1'))";
                }
            }
            if (rad_weitijiao.Checked)
            {
                sqlwhere = sqlwhere + " and totalstate='0'";
            }
            if (rad_shenhezhong.Checked)
            {
                sqlwhere = sqlwhere + " and (totalstate='1' or totalstate='3')";
            }
            if (rad_bohui.Checked)
            {
                sqlwhere = sqlwhere + " and totalstate='2'";
            }
            if (rad_tongguo.Checked)
            {
                sqlwhere = sqlwhere + " and totalstate='4'";
            }
            if (rad_weizxing.Checked)
            {
                sqlwhere = sqlwhere + " and (totalstate='4' and detailstate='0')";
            }
            if (rad_yizhixing.Checked)
            {
                sqlwhere = sqlwhere + " and totalstate='4' and detailstate>=1";
            }
            if (tb_ptc.Text != "")
            {
                sqlwhere = sqlwhere + " and ptcode like '%" + tb_ptc.Text.Trim() + "%'";
            }
            if (tb_marnm.Text != "")
            {
                sqlwhere = sqlwhere + " and marnm like '%" + tb_marnm.Text.Trim() + "%'";
            }
            if (Tb_pcode.Text != "")
            {
                sqlwhere = sqlwhere + " and picno like '%" + Tb_pcode.Text.Trim() + "%'";
            }
            if (drp_stu.SelectedValue.ToString() != "-请选择-")
            {
                sqlwhere = sqlwhere + " and zdrid='" + drp_stu.SelectedValue.ToString() + "'";
            }
            if (tb_StartTime.Text != "" && tb_EndTime.Text != "")
            {
                sqlwhere = sqlwhere + " and irqdata>'" + tb_StartTime.Text.ToString() + "' and irqdata<'" + tb_EndTime.Text.ToString() + "'";
            }
            if (tb_StartTime.Text != "" && tb_EndTime.Text == "")
            {
                sqlwhere = sqlwhere + " and irqdata>'" + tb_StartTime.Text.ToString() + "' ";
            }
            if (tbwltype.Text.Trim() != "")
            {
                sqlwhere = sqlwhere + " and PIC_MASHAPE like '%" + tbwltype.Text.Trim() + "%'";
            }

            if (rad_tongguodingdan.Checked)
            {
                sqlwhere += " and detailstate<1 and  totalstate='4'";
            }
            if (chk_zhaobiao.Checked)
            {
                sqlwhere += " and ICL_TYPE='2'";
            }
            //供应商信息
            if (tb_Gongyingshang.Text != "")
            {
                sqlwhere = sqlwhere + " and supplierresnm like '%" + tb_Gongyingshang.Text.Trim() + "%'";
            }

            string sqltext2 = "select distinct picno from View_TBPC_IQRCMPPRICE_RVW1 where  ";
            string sqltext3 = "select ptcode from View_TBPC_IQRCMPPRICE_RVW1 where ";
            sqltext2 = sqltext2 + sqlwhere;
            sqltext3 = sqltext3 + sqlwhere;
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext2);
            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext3);

            Label1.Text = Convert.ToString(dt.Rows.Count);
            Label2.Text = Convert.ToString(dt1.Rows.Count);

            ViewState["sqlwhere"] = sqlwhere;
        }

        //审批人勾选状态改变的时候（只现在我的未审）
        protected void CBSP_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
            btn_cancel.Enabled = false;
        }

        protected void rad_all_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
            btn_cancel.Enabled = false;
        }
        protected void rad_mypart_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
            btn_cancel.Enabled = true;
        }
        protected void rad_quanbu_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
            btn_cancel.Enabled = false;
        }
        protected void rad_weitijiao_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
            btn_cancel.Enabled = true;
        }
        protected void rad_shenhezhong_CheckedChanged(object sender, EventArgs e)
        {
            if (Session["UserID"].ToString() == "0601002" || Session["UserID"].ToString() == "0907001" || Session["UserID"].ToString() == "01002" || Session["UserID"].ToString() == "01001" || Session["UserID"].ToString() == "01003" || Session["UserID"].ToString() == "01004" || Session["UserID"].ToString() == "01006")
            {
                cb_sp.Visible = true;
                cb_sp.Checked = true;
            }
            else
            {
                cb_sp.Visible = false;
                cb_sp.Checked = false;
            }
            getArticle(true);
            btn_cancel.Enabled = false;
        }
        protected void rad_bohui_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
            btn_cancel.Enabled = true;
        }
        protected void rad_tongguo_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
            btn_cancel.Enabled = false;
        }
        protected void rad_weizxing_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
            btn_cancel.Enabled = false;
        }
        protected void rad_yizhixing_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
            btn_cancel.Enabled = false;
        }
        protected void selectall_CheckedChanged(object sender, EventArgs e)
        {
            //if (selectall.Checked)
            //{
            //    foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
            //    {
            //        System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
            //        if (cbx != null)//存在行
            //        {
            //            cbx.Checked = true;
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
            //    {
            //        System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
            //        if (cbx != null)//存在行
            //        {
            //            cbx.Checked = false;
            //        }
            //    }
            //}
        }

        protected void btn_LX_click(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;
            int start = 0;
            int finish = 0;
            int k = 0;
            foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
            {
                j++;
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
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
                foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
                {
                    k++;
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                    if (k >= start && k <= finish)
                    {
                        cbx.Checked = true;
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
                getArticle(false);
            }
        }
        protected void btn_QX_click(object sender, EventArgs e)
        {
            foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                cbx.Checked = false;
            }
        }

        protected void btn_xiatui_Click(object sender, EventArgs e)//生成订单
        {
            int j = 0;
            double num = 0;
            foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
            {
                if (((System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT")).Checked)
                {
                    num = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("num")).Text);
                    if (num == 0)
                    {
                        j++;
                    }
                }
            }
            if (j > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('包含有数量为0的数据，不能下推订单！');", true);

            }
            else
            {
                int temp = isselected();
                if (temp == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
                }
                else if (temp == 2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您选择的数据包含未审核的记录,本次操作无效！');", true);
                }
                else if (temp == 3)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择的数据包含有不同的供应商的记录,本次操作无效！');", true);
                }
                else if (temp == 4)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择的数据包含已下订单的记录,本次操作无效！');", true);
                }
                else if (temp == 5)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择的数据包含已关闭的记录,本次操作无效！');", true);
                }
                else
                {
                    #region
                    string orderno = encodeorderno();//自动生成订单号
                    string sqltext;
                    string providerid = "";
                    string providernm = "";
                    string ptcode = "";
                    int i = 0;
                    List<string> sqltextlist = new List<string>();
                    //明细表，初始状态为0
                    foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
                    {
                        if (((System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT")).Checked)
                        {
                            i++;
                            if (i == 1)
                            {
                                providerid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("supplierid")).Text;
                                providernm = ((System.Web.UI.WebControls.Label)Reitem.FindControl("suppliernm")).Text;
                            }
                            ptcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("ptcode")).Text;
                            sqltext = "INSERT INTO TBPC_PURORDERDETAIL (PO_CODE, PO_ICLSHEETNO, " +
                                      "PO_PCODE, PO_MARID, PO_LENGTH, PO_WIDTH, PO_QUANTITY, PO_FZNUM," +
                                      "PO_ZXNUM, PO_ZXFZNUM,PO_CTAXUPRICE,PO_TAXRATE," +
                                      "PO_PMODE, PO_KEYCOMS, PO_PTCODE,PO_TUHAO,PO_MASHAPE,PO_NOTE,PO_PJID,PO_ENGID,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME,PO_IFFAST) " +
                                      "SELECT '" + orderno + "' AS Expr1,PIC_SHEETNO," +
                                      "PIC_PTCODE,PIC_MARID, PIC_LENGTH, PIC_WIDTH, " +
                                      "PIC_ZXNUM,PIC_ZXFUNUM,PIC_ZXNUM,PIC_ZXFUNUM, " +
                                      "PIC_PRICE,PIC_SHUILV,PIC_PMODE,PIC_KEYCOMS,PIC_PTCODE,PIC_TUHAO,PIC_MASHAPE,PIC_NOTE,PIC_PJID,PIC_ENGID,PIC_MAP,PIC_TECUNIT,PIC_CHILDENGNAME,PIC_IFFAST  " +
                                      "FROM TBPC_IQRCMPPRICE WHERE PIC_PTCODE='" + ptcode + "'";
                            sqltextlist.Add(sqltext);
                            sqltext = "update TBPC_IQRCMPPRICE set PIC_STATE='1' where PIC_PTCODE='" + ptcode + "'and  PIC_STATE='0'";//生成订单
                            sqltextlist.Add(sqltext);
                            if (ptcode.Contains("#"))
                            {
                                ptcode = ptcode.Substring(0, ptcode.IndexOf("#")).ToString();
                            }
                            sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE='7' WHERE " +
                                       "PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";//生成订单
                            sqltextlist.Add(sqltext);
                        }
                    }
                    //审核表
                    sqltext = "INSERT INTO TBPC_PURORDERTOTAL(PO_CODE,PO_SUPPLIERID,PO_ZDDATE,PO_ZDID,PO_DEPID)  " +
                              "VALUES('" + orderno + "','" + providerid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" +
                              Session["UserID"].ToString() + "','" + Session["UserDeptID"].ToString() + "')";
                    sqltextlist.Add(sqltext);
                    DBCallCommon.ExecuteTrans(sqltextlist);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('TBPC_Purorderdetail_xiugai.aspx?orderno=" + orderno + "');", true);
                    #endregion
                }
            }
            getArticle(false);
        }

        protected void btn_add_Click(object sender, EventArgs e)//追加订单
        {
            int j = 0;
            double num = 0;
            foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
            {
                if (((System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT")).Checked)
                {
                    num = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("num")).Text);
                    if (num == 0)
                    {
                        j++;
                    }
                }
            }
            if (j > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('包含数量为0的数据，不能追加订单！');", true);
            }
            else
            {
                int temp = isselected();
                string ptcode_rcode = "";
                string ptcode = "";
                if (temp == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
                }
                else if (temp == 2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您选择的数据包含未审核的记录,本次操作无效！');", true);
                }
                else if (temp == 3)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择的数据包含有不同的供应商的记录,本次操作无效！');", true);
                }
                else if (temp == 4)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择的数据包含已下订单的记录,本次操作无效！');", true);
                }
                else if (temp == 5)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择的数据包含已关闭的记录,本次操作无效！');", true);
                }
                else
                {
                    foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
                    {
                        if (((System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT")).Checked)
                        {
                            ptcode = ptcode + ((System.Web.UI.WebControls.Label)Reitem.FindControl("PIC_ID")).Text + ",";
                        }
                    }
                    ptcode_rcode = ptcode + Session["UserID"].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "mowinopen('" + ptcode_rcode + "');", true);
                }
                getArticle(false);
            }
        }
        protected void btn_cancel_Click(object sender, EventArgs e)//取消
        {
            int i = 0;
            int j = 0;
            string sqltext = "";
            string PTcode = "";
            string ptcode = "";
            string Marid = "";
            string Marname = "";
            foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox 
                if (cbx.Checked)
                {
                    i++;
                    PTcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("ptcode")).Text.ToString();
                    if (PTcode.Contains("#") && (!PTcode.Contains("@")))
                    {
                        ptcode = PTcode.Substring(0, PTcode.IndexOf("#"));
                    }
                    else
                    {
                        ptcode = PTcode;
                    }
                    Marid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("marid")).Text.ToString();
                    Marname = ((System.Web.UI.WebControls.Label)Reitem.FindControl("marnm")).Text.ToString();
                    sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE = '4' where PUR_PTCODE='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    sqltext = "DELETE FROM TBPC_IQRCMPPRICE WHERE PIC_PTCODE='" + PTcode + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    string sql1 = "select PIC_PTCODE from TBPC_IQRCMPPRICE where PIC_SHEETNO='" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("sheetno")).Text.ToString() + "'";
                    System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
                    if (dt1.Rows.Count == 0)
                    {
                        sqltext = "DELETE FROM TBPC_IQRCMPPRCRVW WHERE ICL_SHEETNO='" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("sheetno")).Text.ToString() + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            if (i > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
            }
            getArticle(false);
        }
        protected void btn_delete_Click(object sender, EventArgs e)//删除
        {
            int i = 0;
            string sqltext = "";
            foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        sqltext = "DELETE FROM TBPC_PURCHASEPLAN " +
                                  "WHERE   (PUR_PTCODE IN " +
                                  "(SELECT PIC_PTCODE FROM  TBPC_IQRCMPPRICE WHERE (PIC_SHEETNO = '" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("sheetno")).Text.ToString() + "')))";
                        DBCallCommon.ExeSqlText(sqltext);
                        sqltext = "DELETE FROM TBPC_IQRCMPPRICE WHERE PIC_SHEETNO='" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("sheetno")).Text.ToString() + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                        sqltext = "DELETE FROM TBPC_IQRCMPPRCRVW WHERE ICL_SHEETNO='" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("sheetno")).Text.ToString() + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            if (i > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
            }
            getArticle(false);
            //checked_list_Repeaterdatabind();
        }
        //生成订单号PORD+8位流水号
        private string encodeorderno()
        {
            string pi_id = "";
            string tag_pi_id = "PORD";
            string end_pi_id = "";
            string sqltext = "SELECT TOP 1 PO_CODE FROM TBPC_PURORDERTOTAL WHERE PO_CODE LIKE '" + tag_pi_id + "%' ORDER BY PO_CODE DESC";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                end_pi_id = Convert.ToString(Convert.ToInt32(dt.Rows[0]["PO_CODE"].ToString().Substring((dt.Rows[0]["PO_CODE"].ToString().Length - 8), 8)) + 1);
                end_pi_id = end_pi_id.PadLeft(8, '0');
            }
            else
            {
                end_pi_id = "00000001";
            }
            pi_id = tag_pi_id + end_pi_id;
            return pi_id;
        }
        protected int isselected()
        {
            int temp = 0;
            string providerid = "";
            int i = 0;//是否选择数据
            int j = 0;//是否审核
            int k = 0;//供应商是否相同
            int l = 0;//选择的数据中是否包含已生成订单数据
            int m = 0;//是否选择了已关闭数据
            int count = 0;
            foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        string ptcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("ptcode")).Text;
                        string sql = "select PO_CODE from TBPC_PURORDERDETAIL where PO_PTCODE='" + ptcode + "' and PO_CSTATE!='2'";
                        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        if (i == 1)
                        {
                            providerid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("supplierid")).Text;
                        }
                        if (Convert.ToInt32(((System.Web.UI.WebControls.Label)Reitem.FindControl("totalstate")).Text) < 4)//审核未通过
                        {
                            j++;
                            break;
                        }
                        else if (providerid != ((System.Web.UI.WebControls.Label)Reitem.FindControl("supplierid")).Text)
                        {
                            k++;
                            break;
                        }
                        else if (dt.Rows.Count > 0)
                        {
                            l++;
                            break;
                        }
                        else if (((System.Web.UI.WebControls.Label)Reitem.FindControl("gbstate")).Text == "1" || ((System.Web.UI.WebControls.Label)Reitem.FindControl("hgbstate")).Text == "1")
                        {
                            m++;
                            break;
                        }
                    }
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else if (j > 0)//是否审核
            {
                temp = 2;
            }
            else if (k > 0)//选择的供应商不同
            {
                temp = 3;
            }
            else if (l > 0)//选择的数据中包含已生成订单数据
            {
                temp = 4;
            }
            else if (m > 0)//选择了已关闭数据
            {
                temp = 5;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }

        List<string> list = new List<string>();
        protected void checked_list_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string totalstate = "";
            string detailstate = "";
            string ddsheetno = "";
            string ptcode = "";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                //订单处理
                #region
                totalstate = ((System.Web.UI.WebControls.Label)e.Item.FindControl("totalstate")).Text;
                detailstate = ((System.Web.UI.WebControls.Label)e.Item.FindControl("detailstate")).Text;
                ptcode = ((System.Web.UI.WebControls.Label)e.Item.FindControl("ptcode")).Text;
                if (totalstate == "4" && detailstate == "1")
                {
                    ddsheetno = ((System.Web.UI.WebControls.Label)e.Item.FindControl("orderno")).Text;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("ddstatetext")).ForeColor = System.Drawing.Color.Red;
                    ((HyperLink)e.Item.FindControl("Hypdd")).NavigateUrl = "PC_TBPC_PurOrder.aspx?orderno=" + ddsheetno + "&ptc=" + ptcode + "";
                }
                #endregion

                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "white";
                }
                else
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "#EFF3FB";
                }


                string PIC_IFFAST = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PIC_IFFAST")).Text.Trim();
                if (PIC_IFFAST == "1")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lbjiaji")).Visible = true;
                }
                //double num = Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("num")).Text);
                //if (num == 0)
                //{
                //    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "#FFFF00";
                //}

                if (Chk_HZ.Checked)
                {
                    List<string> row = new List<string>() { "bmr", "zdr", "rqr", "zgr", "jhr", "zxr", "ddr", "hgr", "bzr", "zdjr" };
                    for (int i = 0; i < row.Count; i++)
                    {
                        HtmlTableCell cell = (HtmlTableCell)e.Item.FindControl(row[i]);
                        cell.Visible = false;
                    }
                }

                System.Web.UI.WebControls.Label shno = (System.Web.UI.WebControls.Label)e.Item.FindControl("sheetno");
                System.Web.UI.WebControls.Label lb1 = (System.Web.UI.WebControls.Label)e.Item.FindControl("zdtime");
                System.Web.UI.WebControls.Label lb2 = (System.Web.UI.WebControls.Label)e.Item.FindControl("amount");
                if (list.Count == 0)
                {
                    list.Add(shno.Text);
                }
                else
                {
                    if (list.Contains(shno.Text))
                    {
                        shno.Visible = false;
                        lb1.Visible = false;
                        lb2.Visible = false;
                    }
                    else
                    {
                        list.Add(shno.Text);
                    }
                }
            }
            if (e.Item.ItemType == ListItemType.Header)
            {
                if (Chk_HZ.Checked)
                {
                    List<string> row = new List<string>() { "bm", "zd", "rq", "zg", "jh", "zx", "dd", "hg", "bz", "zdj" };
                    for (int i = 0; i < row.Count; i++)
                    {
                        HtmlTableCell cell = (HtmlTableCell)e.Item.FindControl(row[i]);
                        cell.Visible = false;
                    }
                }
            }
        }
        public string get_pur_bjdsh(string i)
        {
            string statestr = "";
            if (Convert.ToInt32(i) >= 4)
            {
                statestr = "是";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }
        public string get_pur_bjdgb(string i)
        {
            string statestr = "";
            if (Convert.ToInt32(i) == 1)
            {
                statestr = "是";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }
        public string get_pur_dd(string i)
        {
            string statestr = "";
            if (Convert.ToInt32(i) >= 1)
            {
                statestr = "是";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }
        public string get_pur_hgb(string i)
        {
            string statestr = "";
            if (Convert.ToInt32(i) == 1)
            {
                statestr = "是";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }



        private int ifdcselect()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }
        //批量导出
        protected void btn_daochu_all_Click(object sender, EventArgs e)
        {
            string sqltext = " select picno,ptcode,PIC_CHILDENGNAME,PIC_MAP,margb,PIC_TUHAO,marnm,margg,marcz,length,width,marnum,marunit,marfznum,marfzunit,supplierresnm, " +
                    " price,detamount,detailnote,supplieranm,qoutefstsa,qoutescdsa,qoutelstsa,supplierbnm,qoutefstsb,qoutescdsb,qoutelstsb,suppliercnm,qoutefstsc,qoutescdsc,qoutelstsc, " +
                    " supplierdnm,qoutefstsd,qoutescdsd,qoutelstsd,supplierenm,qoutefstse,qoutescdse,qoutelstse,supplierfnm,qoutefstsf,qoutescdsf,qoutelstsf " +
                      "from View_TBPC_IQRCMPPRICE_RVW1  where ";
            sqltext += ViewState["sqlwhere"].ToString();
            sqltext += " order by picno desc,ptcode asc ";



            System.Data.DataTable dt_daochu_all = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt_daochu_all.Rows.Count > 10000)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导出的数据量超过10000条，请添加导出条件，分多次导出！');", true);
            }
            else
            {
                string filename = "采购比价单明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
                exportCommanmethod.exporteasy(sqltext, filename, "采购比价单明细.xls", 3, true, true, true);
            }


        }
        //导出
        protected void btn_daochu_Click(object sender, EventArgs e)
        {
            int temp = ifdcselect();
            if (temp == 0)
            {
                string ordercode = "";
                string code = "";
                foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx.Checked)
                    {
                        if (ordercode == "" || code != ((System.Web.UI.WebControls.Label)Reitem.FindControl("sheetno")).Text.ToString())
                        {
                            ordercode += ((System.Web.UI.WebControls.Label)Reitem.FindControl("sheetno")).Text.ToString() + "/";
                        }
                        code = ((System.Web.UI.WebControls.Label)Reitem.FindControl("sheetno")).Text.ToString();
                    }
                }
                ordercode = ordercode.Replace("/", "','");
                ordercode = ordercode.Substring(0, ordercode.LastIndexOf(",")).ToString();
                ordercode = "'" + ordercode;
                string sqltext = "";
                sqltext = "SELECT   picno, zdrnm, supplierresnm, pjnm, engnm, ptcode, marid, marnm, margg, marcz, margb,case when margb='' then PIC_TUHAO else '' end as PIC_TUHAO, length, width, marnum, " +
                          "marunit, marfznum, marfzunit, price, detamount, detailnote , qoutefstsa, qoutescdsa, qoutelstsa, qoutefstsb, qoutescdsb, qoutelstsb, qoutefstsc, qoutescdsc," +
                          "qoutelstsc, qoutefstsd, qoutescdsd, qoutelstsd, qoutefstse, qoutescdse, qoutelstse, qoutefstsf, qoutescdsf, qoutelstsf, supplieranm, supplierbnm, " +
                          "suppliercnm, supplierdnm, supplierenm, supplierfnm,PIC_MAP,PIC_CHILDENGNAME  " +
                          "from View_TBPC_IQRCMPPRICE_RVW1  where picno in (" + ordercode + ") order by picno desc,ptcode asc";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                ExportDataItem(dt);
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要导出的比价单！！！');", true);
                getArticle(false);
            }

        }
        private void ExportDataItem(System.Data.DataTable dt)
        {
            #region
            //System.Data.DataTable dt = objdt;
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //Worksheet wksheet;

            //m_xlApp.Visible = false;    // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("采购比价单明细") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //wksheet = (Worksheet)workbook.Sheets.get_Item(1);
            //// 填充数据
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    wksheet.Cells[i + 3, 1] = Convert.ToString(i + 1);//序号

            //    wksheet.Cells[i + 3, 2] = "'" + dt.Rows[i]["picno"].ToString();//比价单号

            //    wksheet.Cells[i + 3, 3] = "'" + dt.Rows[i]["zdrnm"].ToString();//制单人

            //    wksheet.Cells[i + 3, 4] = "'" + dt.Rows[i]["supplierresnm"].ToString();//供应商

            //    wksheet.Cells[i + 3, 5] = "'" + dt.Rows[i]["pjnm"].ToString();//项目名称

            //    wksheet.Cells[i + 3, 6] = "'" + dt.Rows[i]["engnm"].ToString();//工程名称

            //    wksheet.Cells[i + 3, 7] = "'" + dt.Rows[i]["ptcode"].ToString();//计划跟踪号

            //    wksheet.Cells[i + 3, 8] = "'" + dt.Rows[i]["marid"].ToString();//物料编码

            //    wksheet.Cells[i + 3, 9] = "'" + dt.Rows[i]["marnm"].ToString();//名称

            //    wksheet.Cells[i + 3, 10] = "'" + dt.Rows[i]["margg"].ToString();//规格

            //    wksheet.Cells[i + 3, 11] = "'" + dt.Rows[i]["marcz"].ToString();//材质

            //    wksheet.Cells[i + 3, 12] = "'" + dt.Rows[i]["margb"].ToString();//国标

            //    wksheet.Cells[i + 3, 13] = "'" + dt.Rows[i]["PIC_TUHAO"].ToString();//图号

            //    wksheet.Cells[i + 3, 14] = "'" + dt.Rows[i]["length"].ToString();//长度

            //    wksheet.Cells[i + 3, 15] = "'" + dt.Rows[i]["width"].ToString();//宽度

            //    wksheet.Cells[i + 3, 16] = dt.Rows[i]["marnum"].ToString();//数量

            //    wksheet.Cells[i + 3, 17] = "'" + dt.Rows[i]["marunit"].ToString();//单位

            //    wksheet.Cells[i + 3, 18] = dt.Rows[i]["marfznum"].ToString();//辅助数量

            //    wksheet.Cells[i + 3, 19] = "'" + dt.Rows[i]["marfzunit"].ToString();//辅助单位

            //    wksheet.Cells[i + 3, 20] = dt.Rows[i]["price"].ToString();//单价

            //    wksheet.Cells[i + 3, 21] = dt.Rows[i]["detamount"].ToString();//金额

            //    wksheet.Cells[i + 3, 22] = "'" + dt.Rows[i]["detailnote"].ToString();//备注

            //    wksheet.Cells[i + 3, 23] = "'" + dt.Rows[i]["supplieranm"].ToString();//供应商1
            //    wksheet.Cells[i + 3, 24] = dt.Rows[i]["qoutefstsa"].ToString();//供应商1
            //    wksheet.Cells[i + 3, 25] = dt.Rows[i]["qoutescdsa"].ToString();//供应商1
            //    wksheet.Cells[i + 3, 26] = dt.Rows[i]["qoutelstsa"].ToString();//供应商1

            //    wksheet.Cells[i + 3, 27] = "'" + dt.Rows[i]["supplierbnm"].ToString();//供应商2
            //    wksheet.Cells[i + 3, 28] = dt.Rows[i]["qoutefstsb"].ToString();//供应商2
            //    wksheet.Cells[i + 3, 29] = dt.Rows[i]["qoutescdsb"].ToString();//供应商2
            //    wksheet.Cells[i + 3, 30] = dt.Rows[i]["qoutelstsb"].ToString();//供应商2

            //    wksheet.Cells[i + 3, 31] = "'" + dt.Rows[i]["suppliercnm"].ToString();//供应商3
            //    wksheet.Cells[i + 3, 32] = dt.Rows[i]["qoutefstsc"].ToString();//供应商3
            //    wksheet.Cells[i + 3, 33] = dt.Rows[i]["qoutescdsc"].ToString();//供应商3
            //    wksheet.Cells[i + 3, 34] = dt.Rows[i]["qoutelstsc"].ToString();//供应商3

            //    wksheet.Cells[i + 3, 35] = "'" + dt.Rows[i]["supplierdnm"].ToString();//供应商4
            //    wksheet.Cells[i + 3, 36] = dt.Rows[i]["qoutefstsd"].ToString();//供应商4
            //    wksheet.Cells[i + 3, 37] = dt.Rows[i]["qoutescdsd"].ToString();//供应商4
            //    wksheet.Cells[i + 3, 38] = dt.Rows[i]["qoutelstsd"].ToString();//供应商4

            //    wksheet.Cells[i + 3, 39] = "'" + dt.Rows[i]["supplierenm"].ToString();//供应商5
            //    wksheet.Cells[i + 3, 40] = dt.Rows[i]["qoutefstse"].ToString();//供应商5
            //    wksheet.Cells[i + 3, 41] = dt.Rows[i]["qoutescdse"].ToString();//供应商5
            //    wksheet.Cells[i + 3, 42] = dt.Rows[i]["qoutelstse"].ToString();//供应商5

            //    wksheet.Cells[i + 3, 43] = "'" + dt.Rows[i]["supplierfnm"].ToString();//供应商6
            //    wksheet.Cells[i + 3, 44] = dt.Rows[i]["qoutefstsf"].ToString();//供应商6
            //    wksheet.Cells[i + 3, 45] = dt.Rows[i]["qoutescdsf"].ToString();//供应商6
            //    wksheet.Cells[i + 3, 46] = dt.Rows[i]["qoutelstsf"].ToString();//供应商6

            //    wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 46]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            //    wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 46]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            //    wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 46]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            //}
            ////设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            //string filename = Server.MapPath("/PC_Data/ExportFile/" + "采购比价单明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
            #endregion

            string filename = "采购比价单明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("采购比价单明细.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 3);

                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号
                    row.CreateCell(1).SetCellValue(dt.Rows[i]["picno"].ToString());//比价单号
                    row.CreateCell(2).SetCellValue(dt.Rows[i]["ptcode"].ToString());//计划跟踪号
                    row.CreateCell(3).SetCellValue(dt.Rows[i]["PIC_CHILDENGNAME"].ToString());//部件名称
                    row.CreateCell(4).SetCellValue(dt.Rows[i]["PIC_MAP"].ToString());//部件图号
                    row.CreateCell(5).SetCellValue(dt.Rows[i]["margb"].ToString());//国标
                    row.CreateCell(6).SetCellValue(dt.Rows[i]["PIC_TUHAO"].ToString());//图号
                    row.CreateCell(7).SetCellValue(dt.Rows[i]["marnm"].ToString());//名称
                    row.CreateCell(8).SetCellValue(dt.Rows[i]["margg"].ToString());//规格
                    row.CreateCell(9).SetCellValue(dt.Rows[i]["marcz"].ToString());//材质
                    row.CreateCell(10).SetCellValue(dt.Rows[i]["length"].ToString());//长度
                    row.CreateCell(11).SetCellValue(dt.Rows[i]["width"].ToString());//宽度
                    row.CreateCell(12).SetCellValue(dt.Rows[i]["marnum"].ToString());//数量
                    row.CreateCell(13).SetCellValue(dt.Rows[i]["marunit"].ToString());//单位
                    row.CreateCell(14).SetCellValue(dt.Rows[i]["marfznum"].ToString());//辅助数量
                    row.CreateCell(15).SetCellValue(dt.Rows[i]["marfzunit"].ToString());//辅助单位
                    row.CreateCell(16).SetCellValue(dt.Rows[i]["supplierresnm"].ToString());//供应商
                    //row.CreateCell(4).SetCellValue( dt.Rows[i]["pjnm"].ToString());//项目名称
                    //row.CreateCell(5).SetCellValue( dt.Rows[i]["engnm"].ToString());//工程名称

                    //row.CreateCell(7).SetCellValue( dt.Rows[i]["marid"].ToString());//物料编码

                    row.CreateCell(17).SetCellValue(dt.Rows[i]["price"].ToString());//单价
                    row.CreateCell(18).SetCellValue(dt.Rows[i]["detamount"].ToString());//金额
                    row.CreateCell(19).SetCellValue(dt.Rows[i]["detailnote"].ToString());//备注
                    row.CreateCell(20).SetCellValue(dt.Rows[i]["supplieranm"].ToString());//供应商1
                    row.CreateCell(21).SetCellValue(dt.Rows[i]["qoutefstsa"].ToString());//供应商1
                    row.CreateCell(22).SetCellValue(dt.Rows[i]["qoutescdsa"].ToString());//供应商1
                    row.CreateCell(23).SetCellValue(dt.Rows[i]["qoutelstsa"].ToString());//供应商1
                    row.CreateCell(24).SetCellValue(dt.Rows[i]["supplierbnm"].ToString());//供应商2
                    row.CreateCell(25).SetCellValue(dt.Rows[i]["qoutefstsb"].ToString());//供应商2
                    row.CreateCell(26).SetCellValue(dt.Rows[i]["qoutescdsb"].ToString());//供应商2
                    row.CreateCell(27).SetCellValue(dt.Rows[i]["qoutelstsb"].ToString());//供应商2
                    row.CreateCell(28).SetCellValue(dt.Rows[i]["suppliercnm"].ToString());//供应商3
                    row.CreateCell(29).SetCellValue(dt.Rows[i]["qoutefstsc"].ToString());//供应商3
                    row.CreateCell(30).SetCellValue(dt.Rows[i]["qoutescdsc"].ToString());//供应商3
                    row.CreateCell(31).SetCellValue(dt.Rows[i]["qoutelstsc"].ToString());//供应商3
                    row.CreateCell(32).SetCellValue(dt.Rows[i]["supplierdnm"].ToString());//供应商4
                    row.CreateCell(33).SetCellValue(dt.Rows[i]["qoutefstsd"].ToString());//供应商4
                    row.CreateCell(34).SetCellValue(dt.Rows[i]["qoutescdsd"].ToString());//供应商4
                    row.CreateCell(35).SetCellValue(dt.Rows[i]["qoutelstsd"].ToString());//供应商4
                    row.CreateCell(36).SetCellValue(dt.Rows[i]["supplierenm"].ToString());//供应商5
                    row.CreateCell(37).SetCellValue(dt.Rows[i]["qoutefstse"].ToString());//供应商5
                    row.CreateCell(38).SetCellValue(dt.Rows[i]["qoutescdse"].ToString());//供应商5
                    row.CreateCell(39).SetCellValue(dt.Rows[i]["qoutelstse"].ToString());//供应商5
                    row.CreateCell(40).SetCellValue(dt.Rows[i]["supplierfnm"].ToString());//供应商6
                    row.CreateCell(41).SetCellValue(dt.Rows[i]["qoutefstsf"].ToString());//供应商6
                    row.CreateCell(42).SetCellValue(dt.Rows[i]["qoutescdsf"].ToString());//供应商6
                    row.CreateCell(43).SetCellValue(dt.Rows[i]["qoutelstsf"].ToString());//供应商6

                    NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                    font1.FontName = "仿宋";//字体
                    font1.FontHeightInPoints = 11;//字号
                    ICellStyle cells = wk.CreateCellStyle();
                    cells.SetFont(font1);

                    for (int j = 0; j < 44; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }
                }

                #endregion

                for (int i = 0; i <= dt.Columns.Count; i++)
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

        //询价单导出
        protected void btn_daochu_xjdclick(object sender, EventArgs e)
        {
            int temp = ifselect();
            if (temp == 0)
            {
                string ordercode = "";
                string code = "";
                foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx.Checked)
                    {
                        if (ordercode == "" || code != ((System.Web.UI.WebControls.Label)Reitem.FindControl("sheetno")).Text.ToString())
                        {
                            ordercode += ((System.Web.UI.WebControls.Label)Reitem.FindControl("sheetno")).Text.ToString() + "/";
                        }
                        code = ((System.Web.UI.WebControls.Label)Reitem.FindControl("sheetno")).Text.ToString();
                    }
                }
                ordercode = ordercode.Replace("/", "','");
                ordercode = ordercode.Substring(0, ordercode.LastIndexOf(",")).ToString();
                ordercode = "'" + ordercode;
                string sqltext = "";
                sqltext = "SELECT   picno, zdrid, zdrnm, substring(zdtime,1,10) as zdtime,iclzgid,CONVERT(varchar(12) , irqdata, 102 ) as irqdata, iclzgnm, iclfzrid, iclfzrnm, iclamount, " +
                            " totalcstate, totalstate, totalnote, supplierresid, supplierresnm,PIC_ID,  " +
                            "pjnm, engnm, ptcode,CONVERT(float, price) as price, shuilv, marid, marnm, margg, margb, " +
                            "marcz, marunit, marfzunit, length, width, CONVERT(float, marnum) AS marnum, marfznum, marzxnum,case when margb='' then PIC_TUHAO else '' end as PIC_TUHAO,PIC_MASHAPE,shbid,shcid,shdid,sheid,shfid,shgid, " +
                            "marzxfznum, isnull(detailstate,0) as detailstate, isnull(detailcstate,0) as detailcstate,detamount,detailnote, zdjnum, zdjprice, orderno, zxrid, zxrnm,sqrnm,PIC_MAP,PIC_CHILDENGNAME " +
                          "from View_TBPC_IQRCMPPRICE_RVW1  where picno in (" + ordercode + ") order by picno desc,ptcode asc";
                System.Data.DataTable xjddt = DBCallCommon.GetDTUsingSqlText(sqltext);
                int m = 0;
                for (int i = 0; i < xjddt.Rows.Count; i++)
                {
                    if (xjddt.Rows[i]["PIC_MASHAPE"].ToString().Contains("板") || xjddt.Rows[i]["PIC_MASHAPE"].ToString().Contains("定尺板") || xjddt.Rows[i]["PIC_MASHAPE"].ToString().Contains("圆") || xjddt.Rows[i]["PIC_MASHAPE"].ToString().Contains("型"))
                    {
                        m++;
                    }
                }
                if (m == xjddt.Rows.Count)
                {
                    ExportDataItemxjdgc(xjddt);
                }
                else if (m == 0)
                {
                    ExportDataItemxjdfgc(xjddt);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导出数据中包含钢材和非钢材，无法导出！！！');", true);
                }
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择要导出的比价单或勾选了多条！！！');", true);
                getArticle(false);
            }
        }
        //钢材类询价单导出
        private void ExportDataItemxjdgc(System.Data.DataTable xjddt)
        {
            string filename = "询价单" + xjddt.Rows[0]["picno"].ToString() + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("钢材类询价单模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                IRow row1 = sheet0.GetRow(1);
                IRow row13 = sheet0.GetRow(13);
                #region 写入数据
                //string jhtime = "";
                //string sqljhq = "select qoutefstsa,qoutefstsb,qoutefstsc,qoutefstsd, qoutefstse,qoutelstsf, supplieranm, supplierbnm,suppliercnm, supplierdnm, supplierenm, supplierfnm from View_TBPC_IQRCMPPRICE_RVW1 where picno='" + dt.Rows[0]["picno"].ToString() + "'";
                //System.Data.DataTable dtjhq=DBCallCommon.GetDTUsingSqlText(sqljhq);
                //if(dtjhq.Rows.Count>0)
                //{
                //    if (dt.Rows[0]["supplierresnm"] == dtjhq.Rows[0]["supplieranm"])
                //    {
                //        jhtime = dtjhq.Rows[0]["qoutefstsa"];
                //    }
                //    else if (dt.Rows[0]["supplierresnm"] == dtjhq.Rows[0]["supplierbnm"])
                //    {
                //        jhtime = dtjhq.Rows[0]["qoutefstsb"];
                //    }
                //    else if (dt.Rows[0]["supplierresnm"] == dtjhq.Rows[0]["suppliercnm"])
                //    {
                //        jhtime = dtjhq.Rows[0]["qoutefstsc"];
                //    }
                //    else if (dt.Rows[0]["supplierresnm"] == dtjhq.Rows[0]["supplierdnm"])
                //    {
                //        jhtime = dtjhq.Rows[0]["qoutefstsd"];
                //    }
                //    else if (dt.Rows[0]["supplierresnm"] == dtjhq.Rows[0]["supplierenm"])
                //    {
                //        jhtime = dtjhq.Rows[0]["qoutefstse"];
                //    }
                //    else
                //    {
                //        jhtime = dtjhq.Rows[0]["qoutelstsf"];
                //    }
                //}
                #endregion
                row1.GetCell(16).SetCellValue(xjddt.Rows[0]["picno"].ToString());//询价单号
                //row10.GetCell(1).SetCellValue(dt.Rows[0]["supplierresnm"].ToString());//供应商
                row13.GetCell(15).SetCellValue(xjddt.Rows[0]["irqdata"].ToString().Substring(0, 10).Trim());//制单日期
                for (int i = 0; i < xjddt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 19);
                    row.HeightInPoints = 14;//行高
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号
                    //row.CreateCell(1).SetCellValue(xjddt.Rows[i]["marid"].ToString());//物料编码
                    row.CreateCell(1).SetCellValue(xjddt.Rows[i]["marnm"].ToString());//物料名称
                    row.CreateCell(2).SetCellValue(xjddt.Rows[i]["PIC_TUHAO"].ToString());//图号,规格，材质，国标
                    row.CreateCell(3).SetCellValue(xjddt.Rows[i]["margg"].ToString());
                    row.CreateCell(4).SetCellValue(xjddt.Rows[i]["marcz"].ToString());
                    row.CreateCell(5).SetCellValue(xjddt.Rows[i]["margb"].ToString());
                    //row.CreateCell(7).SetCellValue(xjddt.Rows[i]["PIC_MASHAPE"].ToString());//类型
                    row.CreateCell(6).SetCellValue(xjddt.Rows[i]["marunit"].ToString());//单位
                    row.CreateCell(7).SetCellValue(xjddt.Rows[i]["marnum"].ToString());//数量
                    row.CreateCell(8).SetCellValue(xjddt.Rows[i]["marfznum"].ToString());//辅助数量
                    //if (xjddt.Rows[i]["PIC_MASHAPE"].ToString().Contains("板") || xjddt.Rows[i]["PIC_MASHAPE"].ToString().Contains("定尺板"))
                    //{
                    row.CreateCell(9).SetCellValue(xjddt.Rows[i]["length"].ToString());//长度
                    row.CreateCell(10).SetCellValue(xjddt.Rows[i]["width"].ToString());//宽度
                    //}
                    //else
                    //{
                    //    row.CreateCell(11).SetCellValue("");//长度
                    //    row.CreateCell(12).SetCellValue("");//宽度
                    //}
                    row.CreateCell(11).SetCellValue("");//片/支
                    row.CreateCell(12).SetCellValue("");//含税单价
                    row.CreateCell(13).SetCellValue("");//含税金额
                    row.CreateCell(14).SetCellValue("");//交货日期
                    row.CreateCell(15).SetCellValue(xjddt.Rows[i]["detailnote"].ToString());//备注
                    row.CreateCell(16).SetCellValue(xjddt.Rows[i]["ptcode"].ToString());//计划跟踪号
                    NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                    font1.FontName = "仿宋";//字体
                    font1.FontHeightInPoints = 11;//字号
                    ICellStyle cells = wk.CreateCellStyle();
                    cells.SetFont(font1);
                    cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                    for (int j = 0; j <= 16; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }
                }
                //页脚信息
                IRow row21 = sheet0.CreateRow(xjddt.Rows.Count + 21);
                row21.CreateCell(1).SetCellValue("部门：");
                row21.CreateCell(2).SetCellValue("采购部");
                row21.CreateCell(5).SetCellValue("部长：");
                row21.CreateCell(6).SetCellValue("高浩");
                row21.CreateCell(8).SetCellValue("业务员：");
                row21.CreateCell(9).SetCellValue(xjddt.Rows[0]["zxrnm"].ToString());
                row21.CreateCell(11).SetCellValue("制单：");
                row21.CreateCell(12).SetCellValue(xjddt.Rows[0]["zdrnm"].ToString());



                for (int i = 0; i <= 16; i++)
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
        //非钢材类询价单导出
        private void ExportDataItemxjdfgc(System.Data.DataTable xjddt)
        {

            string filename = "询价单" + xjddt.Rows[0]["picno"].ToString() + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("非钢材类询价单模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                IRow row2 = sheet0.GetRow(2);
                IRow row13 = sheet0.GetRow(13);
                #region 写入数据
                //string jhtime = "";
                //string sqljhq = "select qoutefstsa,qoutefstsb,qoutefstsc,qoutefstsd, qoutefstse,qoutelstsf, supplieranm, supplierbnm,suppliercnm, supplierdnm, supplierenm, supplierfnm from View_TBPC_IQRCMPPRICE_RVW1 where picno='" + dt.Rows[0]["picno"].ToString() + "'";
                //System.Data.DataTable dtjhq=DBCallCommon.GetDTUsingSqlText(sqljhq);
                //if(dtjhq.Rows.Count>0)
                //{
                //    if (dt.Rows[0]["supplierresnm"] == dtjhq.Rows[0]["supplieranm"])
                //    {
                //        jhtime = dtjhq.Rows[0]["qoutefstsa"];
                //    }
                //    else if (dt.Rows[0]["supplierresnm"] == dtjhq.Rows[0]["supplierbnm"])
                //    {
                //        jhtime = dtjhq.Rows[0]["qoutefstsb"];
                //    }
                //    else if (dt.Rows[0]["supplierresnm"] == dtjhq.Rows[0]["suppliercnm"])
                //    {
                //        jhtime = dtjhq.Rows[0]["qoutefstsc"];
                //    }
                //    else if (dt.Rows[0]["supplierresnm"] == dtjhq.Rows[0]["supplierdnm"])
                //    {
                //        jhtime = dtjhq.Rows[0]["qoutefstsd"];
                //    }
                //    else if (dt.Rows[0]["supplierresnm"] == dtjhq.Rows[0]["supplierenm"])
                //    {
                //        jhtime = dtjhq.Rows[0]["qoutefstse"];
                //    }
                //    else
                //    {
                //        jhtime = dtjhq.Rows[0]["qoutelstsf"];
                //    }
                //}
                #endregion
                row2.GetCell(12).SetCellValue(xjddt.Rows[0]["picno"].ToString());//询价单号
                //row10.GetCell(1).SetCellValue(dt.Rows[0]["supplierresnm"].ToString());//供应商
                row13.GetCell(11).SetCellValue(xjddt.Rows[0]["irqdata"].ToString().Substring(0, 10).Trim());//制单日期
                for (int i = 0; i < xjddt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 19);
                    row.HeightInPoints = 14;//行高
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号
                    //row.CreateCell(1).SetCellValue(xjddt.Rows[i]["marid"].ToString());//物料编码
                    row.CreateCell(1).SetCellValue(xjddt.Rows[i]["marnm"].ToString());//物料名称
                    row.CreateCell(2).SetCellValue(xjddt.Rows[i]["PIC_TUHAO"].ToString());//图号,规格，材质，国标
                    row.CreateCell(3).SetCellValue(xjddt.Rows[i]["margg"].ToString());
                    row.CreateCell(4).SetCellValue(xjddt.Rows[i]["marcz"].ToString());
                    row.CreateCell(5).SetCellValue(xjddt.Rows[i]["margb"].ToString());
                    //row.CreateCell(7).SetCellValue(xjddt.Rows[i]["PIC_MASHAPE"].ToString());//类型
                    row.CreateCell(6).SetCellValue(xjddt.Rows[i]["marunit"].ToString());//单位
                    row.CreateCell(7).SetCellValue(xjddt.Rows[i]["marnum"].ToString());//数量
                    row.CreateCell(8).SetCellValue("");//含税单价
                    row.CreateCell(9).SetCellValue("");//含税金额
                    row.CreateCell(10).SetCellValue("");//交货日期
                    row.CreateCell(11).SetCellValue(xjddt.Rows[i]["detailnote"].ToString());//备注
                    row.CreateCell(12).SetCellValue(xjddt.Rows[i]["ptcode"].ToString());//计划跟踪号
                    NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                    font1.FontName = "仿宋";//字体
                    font1.FontHeightInPoints = 11;//字号

                    ICellStyle cells = wk.CreateCellStyle();
                    cells.SetFont(font1);
                    cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                    for (int j = 0; j <= 12; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }
                }
                //页脚信息
                IRow row21 = sheet0.CreateRow(xjddt.Rows.Count + 21);
                row21.CreateCell(1).SetCellValue("部门：");
                row21.CreateCell(2).SetCellValue("采购部");
                row21.CreateCell(3).SetCellValue("部长：");
                row21.CreateCell(4).SetCellValue("高浩");
                row21.CreateCell(6).SetCellValue("业务员：");
                row21.CreateCell(7).SetCellValue(xjddt.Rows[0]["zxrnm"].ToString());
                row21.CreateCell(9).SetCellValue("制单：");
                row21.CreateCell(10).SetCellValue(xjddt.Rows[0]["zdrnm"].ToString());

                for (int i = 0; i <= 12; i++)
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






        /// <summary>
        /// 输出Excel文件并退出
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="workbook"></param>
        private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
        {
            try
            {

                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Workbooks.Close();
                m_xlApp.Quit();
                m_xlApp.Application.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);

                wksheet = null;
                workbook = null;
                m_xlApp = null;
                GC.Collect();

                //下载

                System.IO.FileInfo path = new System.IO.FileInfo(filename);

                //同步，异步都支持
                HttpResponse contextResponse = HttpContext.Current.Response;
                contextResponse.Redirect(string.Format("~/PC_Data/ExportFile/{0}", path.Name), false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private int ifselect()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                }
            }
            if (i == 0 || i > 1)//未选择数据
            {
                temp = 1;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }
        //比价单驳回数量
        private void getbh()
        {
            string sqltext = "";
            int num = 0;
            if (rad_mypart.Checked)
            {
                sqltext = "select count(*) from TBPC_IQRCMPPRCRVW where  " +
                             "(ICL_REVIEWA='" + Session["UserID"].ToString() + "' or ICL_REVIEWB='" + Session["UserID"].ToString() + "' or ICL_REVIEWC='" + Session["UserID"].ToString() + "' or ICL_REVIEWD='" + Session["UserID"].ToString() + "' or ICL_REVIEWE='" + Session["UserID"].ToString() + "' or ICL_REVIEWF='" + Session["UserID"].ToString() + "')" +
                             " and ICL_STATE='2'";
            }
            else if (rad_all.Checked)
            {
                sqltext = "select count(*) from TBPC_IQRCMPPRCRVW where  ICL_STATE='2'";
            }

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num += Convert.ToInt32(dr[0].ToString());
            }

            if (num == 0)
            {
                lb_bjdbh.Visible = false;
            }
            else
            {
                lb_bjdbh.Visible = true;
                lb_bjdbh.Text = "(" + num.ToString() + ")";
            }

            sqltext = "select ICL_SHEETNO from TBPC_IQRCMPPRCRVW where ICL_REVIEWA='" + Session["UserID"].ToString() + "' and ICL_STATE='4'";
            lb_bjdtg.Text = "(" + DBCallCommon.GetDTUsingSqlText(sqltext).Rows.Count + ")";
        }

        //供应商信息发生变化
        protected void tb_Gongyingshang_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (tb_Gongyingshang.Text.ToString().Contains("|"))
            {
                Cname = tb_Gongyingshang.Text.Substring(0, tb_Gongyingshang.Text.ToString().IndexOf("|"));
                tb_Gongyingshang.Text = Cname.Trim();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请正确填写供应商！');", true);
                getArticle(false);
            }
        }

        protected void Chk_HZ_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }


        protected void chk_zhaobiao_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
            btn_cancel.Enabled = false;
        }
    }
}
