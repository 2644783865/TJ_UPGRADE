using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Xie_list : BasicPage
    {
        PagerQueryParamGroupBy pager = new PagerQueryParamGroupBy();

        ////页数
        public int ObjPageSize
        {
            get
            {
                if (ViewState["ObjPageSize"] == null)
                {
                    //默认是升序
                    ViewState["ObjPageSize"] = Convert.ToDouble(DropDownList5.SelectedValue);
                }

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

        public string get_pur_bjdsh(string i)
        {
            string statestr = "";
            if (Convert.ToInt32(i) >= 4)
            {
                statestr = "<span style='color:red'>是</span>";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }
        public string get_orderstate(string i)
        {
            string statestr = "";
            if (i=="1")
            {
                statestr = "<span style='color:red'>是</span>";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }

        public string get_bjstatus(string i)
        {
            string statestr = "";
            if (i=="合格")
            {
                statestr = "<span style='color:red'>合格</span>";
            }
            else
            {
                statestr = i;
            }
            return statestr;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = btn_search.UniqueID;  //2013年6月25日 16:25:07  Meng   回车键为查找
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);

            ViewState["ObjPageSize"] = Convert.ToDouble(DropDownList5.SelectedValue);
            if (!IsPostBack)
            {
                string sf = "";
                if (Request.QueryString["sf"] != null)
                {
                    sf = Request.QueryString["sf"].ToString();
                }
                initpage1();
                initpower1();
                GetSele();
                getArticle(true);
            }
            getbh();

            if (rad_tongguo.Checked||rad_wbj.Checked)
            {
                btn_baojian.Visible = true;
            }
            else
            {
                btn_baojian.Visible = false;
            }
            //getArticle(true);
            CheckUser(ControlFinder);
        }

        private void initpage1()
        {
            string sqltext = "";
            sqltext = "select ST_NAME,ST_CODE from TBDS_STAFFINFO WHERE ST_DEPID='06' and ST_PD='0'";
            string dataText = "ST_NAME";
            string dataValue = "ST_CODE";
            //DBCallCommon.BindDdl(drp_stu, sqltext, dataText, dataValue);
            //drp_stu.SelectedIndex = 0;
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
                rad_quanbu.Checked = true; rad_weitijiao.Checked = false; rad_shenhezhong.Checked = false; rad_bohui.Checked = false; rad_tongguo.Checked = false;
            }
            else if (NUM2 == "2")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = true; rad_shenhezhong.Checked = false; rad_bohui.Checked = false; rad_tongguo.Checked = false;
            }
            else if (NUM2 == "3")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = false; rad_shenhezhong.Checked = true; rad_bohui.Checked = false; rad_tongguo.Checked = false;
            }
            else if (NUM2 == "4")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = false; rad_shenhezhong.Checked = false; rad_bohui.Checked = true; rad_tongguo.Checked = false;
            }
            else if (NUM2 == "5")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = false; rad_shenhezhong.Checked = false; rad_bohui.Checked = false; rad_tongguo.Checked = true;
            }
            else if (NUM2 == "6")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = false; rad_shenhezhong.Checked = false; rad_bohui.Checked = false; rad_tongguo.Checked = false;
            }
            else if (NUM2 == "7")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = false; rad_shenhezhong.Checked = false; rad_bohui.Checked = false; rad_tongguo.Checked = false;
            }
        }
        //判断审核人信息，若为审核人，则选择审核中。
        private void initpower1()
        {
            if (Session["POSITION"].ToString() == "0101" || Session["POSITION"].ToString() == "0102")
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

            { pager.OrderField = OrderField; }
            else

            { pager.OrderField = GroupField; }

            pager.GroupField = GroupField;

            pager.OrderType = OrderType;

            pager.StrWhere = StrWhere;

            pager.PageSize = PageSize;
        }

        protected void bindData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamGroupBy(pager);
            GetlbBJZJ(dt);
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
        }

        //报价总计
        private void GetlbBJZJ(System.Data.DataTable dt)
        {
            double detamounts = 0;
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                detamounts += CommonFun.ComTryDouble(dt.Rows[i]["detamount"].ToString());
            }
            lbBJZJ.Text = detamounts.ToString()+"元";
        }

        void Pager_PageChanged(int pageNumber)
        {
            getArticle(false);
        }
        protected void btn_search_click(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void btn_clear_click(object sender, EventArgs e)
        {
            //Tb_PJNAME.Text = "";
            //Tb_ENGNAME.Text = "";
            Tb_pcode.Text = "";
            tb_Gongyingshang.Text = "";
            getArticle(true);

        }
        private void getArticle(bool isFirstPage)      //取得Article数据
        {
            CreateDataSource();
            string TableName = "View_TBMP_IQRCMPPRICE_RVW1";
            string PrimaryKey = "";
            //string ShowFields = "picno, zdrid, zdrnm, substring(zdtime,1,10) as zdtime, iclzgid,substring(irqdata,1,10) as irqdata, iclzgnm, iclfzrid, iclfzrnm, iclamount, " +
            string ShowFields = "PIC_ZONGXU,CM_PROJ,CM_CONTR,TA_TOTALNOTE,ICL_STATEA,ICL_STATEB,MS_WSID, PIC_ORDERSTATE,PTC,MS_UWGHT,MS_PROCESS,PIC_ENGID,PIC_ENGNAME,PIC_JGNUM,PIC_JSNUM,PIC_BJSTATUS,PIC_RKSTATUS,picno,zdrid,detamount, zdrnm,CONVERT(varchar(12) , zdtime,23) as zdtime , iclzgid,CONVERT(varchar(12) , irqdata, 102 ) as irqdata, iclzgnm, iclfzrid, iclfzrnm, iclamount, " +
                            " totalcstate, totalstate, totalnote, supplierresid, supplierresnm,PIC_ID,  " +
                            "pjnm, engnm, ptcode,CONVERT(float, price) as price, shuilv, marid, marnm, margg, margb,ICL_WXTYPE, " +
                            "marcz, marunit, marfzunit, length, width, CONVERT(float, marnum) AS marnum, marfznum, marzxnum,PIC_TUHAO,PIC_MASHAPE,shbid,shcid,shdid,sheid,shfid,shgid " +
                            "marzxfznum, isnull(detailstate,0) as detailstate, isnull(detailcstate,0) as detailcstate, detailnote, zdjnum, zdjprice, zxrid, zxrnm  ";
            //数据库中的主键
            string OrderField = "picno desc,ptcode";
            string GroupField = "";
            int OrderType = 0;
            string StrWhere = ViewState["sqlwhere"].ToString();
            int PageSize = ObjPageSize;
            BindPage(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFirstPage);
        }
        public void CreateDataSource()
        {
            string sqlwhere = "";
            string filter = "";
            sqlwhere = "1=1 and (PIC_CFSTATE='0' OR  PIC_CFSTATE='2')";
            if (filter != "")
            {
                sqlwhere = sqlwhere + " and " + filter + "";
            }
            if (rad_mypart.Checked)
            {

                if (rad_shenhezhong.Checked)
                {
                    if (Session["POSITION"].ToString() == "0101" || Session["POSITION"].ToString() == "0102")
                    {
                        sqlwhere = sqlwhere + " and ((zdrid='" + Session["UserID"].ToString() + "') or (shbid='" + Session["UserID"].ToString() + "' and ICL_STATEA='0') or (ICL_STATEA='2' and ICL_STATEB='0' )) ";
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
            if (rad_wbj.Checked)
            {
                sqlwhere = sqlwhere + " and (totalstate='4' and PIC_BJSTATUS is NULL) "; 
            
            }
            if (rad_ybj.Checked)
            {
                sqlwhere = sqlwhere + " and (PIC_BJSTATUS <> 'null' and PIC_BJSTATUS<>'合格') "; 
            }
            if (rad_yhg.Checked)
            {
                sqlwhere = sqlwhere + " and (PIC_BJSTATUS = '合格'or PIC_BJSTATUS='让步接收')"; 
            }
            if (rad_yjs.Checked)
            {
                sqlwhere = sqlwhere + " and TA_TOTALNOTE  IS NOT NULL ";
            }
            if (Tb_pcode.Text != "")
            {
                sqlwhere = sqlwhere + " and cast(picno as bigint) = '" + Tb_pcode.Text.Trim() + "'";
            }
            //供应商信息
            if (tb_Gongyingshang.Text != "")
            {
                sqlwhere = sqlwhere + " and supplierresnm like '%" + tb_Gongyingshang.Text.Trim() + "%'";
            }          
            //生产外协类型
            if (ddltype.SelectedIndex != 0)
            {
                sqlwhere = sqlwhere + " and ICL_WXTYPE = '" + ddltype.SelectedValue.ToString() + "'";
            }
            if (PIC_ZONGXU.Text.Trim() != "")
            {
                sqlwhere += " and PIC_ZONGXU like '%" + PIC_ZONGXU.Text.Trim() + "%'";
            }
            if (marnm.Text.Trim() != "")
            {
                sqlwhere += " and marnm like '%" + marnm.Text + "%'";
            }
            //审批人增加限制，只显示需要审批的任务   2013年5月19日 11:49:52
            //筛选条件
            if (screen1.SelectedValue != "0" || screen2.SelectedValue != "0" || screen3.SelectedValue != "0" || screen4.SelectedValue != "0")
            {
                if (SelectStr(screen1, ddlRelation1, Txt1.Text, "") != "")
                {
                    sqlwhere += "and (" + SelectStr(screen1, ddlRelation1, Txt1.Text, "");
                }
                else
                {
                    sqlwhere += "and (1=1 ";
                }
                sqlwhere += SelectStr(screen2, ddlRelation2, Txt2.Text, ddlLogic1.SelectedValue);
                sqlwhere += SelectStr(screen3, ddlRelation3, Txt3.Text, ddlLogic2.SelectedValue);
                sqlwhere += SelectStr(screen4, ddlRelation4, Txt4.Text, ddlLogic3.SelectedValue);
                sqlwhere += SelectStr(screen5, ddlRelation5, Txt5.Text, ddlLogic4.SelectedValue);
                sqlwhere += SelectStr(screen6, ddlRelation6, Txt6.Text, ddlLogic5.SelectedValue);
                sqlwhere += ")";
            }
            //时间筛选条件
            if (txt_time.Text.Trim() != "")
             {
               // DateTime dt_1 = Convert.ToDateTime(Txt5.Text.ToString());
                 switch (ddl_time.SelectedIndex)
                 {
                     case 0: sqlwhere += "and convert(varchar(10),zdtime,23) like '%" + txt_time.Text.Trim().ToString() + "%'"; break;
                     //case 1: sqlwhere += "and convert(varchar(10),zdtime,112) >= " + dt_1 + ""; break;
                 }
             }
            string sqltext2 = "select distinct picno from View_TBMP_IQRCMPPRICE_RVW1 where  ";
            string sqltext3 = "select ptcode from View_TBMP_IQRCMPPRICE_RVW1 where ";
            sqltext2 = sqltext2 + sqlwhere;
            sqltext3 = sqltext3 + sqlwhere;
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext2);
            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext3);
            Label1.Text = Convert.ToString(dt.Rows.Count);
            Label2.Text = Convert.ToString(dt1.Rows.Count);
            if (Request.QueryString["TotalOrder"] != null)
            {
                string[] strs = Request.QueryString["TotalOrder"].ToString().Split('、');
                string str = "";
                for (int i = 0; i < strs.Length; i++)
                {
                    str += ",'" + strs[i] + "'";
                }
                sqlwhere = string.Format("picno in ({0})", str.Substring(1));
            }
            ViewState["sqlwhere"] = sqlwhere;
        }
        private void GetStrCondition()
        {
            string strWhere = "";
            if (screen1.SelectedValue != "0" || screen2.SelectedValue != "0" || screen3.SelectedValue != "0" || screen4.SelectedValue != "0")
            {
                if (SelectStr(screen1, ddlRelation1, Txt1.Text, "") != "")
                {
                    strWhere += "and (" + SelectStr(screen1, ddlRelation1, Txt1.Text, "");
                }
                else
                {
                    strWhere += "and (1=1 ";
                }
                strWhere += SelectStr(screen2, ddlRelation2, Txt2.Text, ddlLogic1.SelectedValue);
                strWhere += SelectStr(screen3, ddlRelation3, Txt3.Text, ddlLogic2.SelectedValue);
                strWhere += SelectStr(screen4, ddlRelation4, Txt4.Text, ddlLogic3.SelectedValue);
                strWhere += SelectStr(screen5, ddlRelation5, Txt5.Text, ddlLogic4.SelectedValue);
                strWhere += SelectStr(screen6, ddlRelation6, Txt6.Text, ddlLogic5.SelectedValue);
                strWhere += ")";
            }
            ViewState["sqlwhere"]=strWhere;
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
                        sqlstr = string.Format("{0} {1}='{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "3":
                        sqlstr = string.Format("{0} {1}!='{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "4":
                        sqlstr = string.Format("{0} {1}>'{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "5":
                        sqlstr = string.Format("{0} {1}>='{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "6":
                        sqlstr = string.Format("{0} {1}<'{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "7":
                        sqlstr = string.Format("{0} {1}<='{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                }
            }
            return sqlstr;
        }
        protected void reset_Click(object sender, EventArgs e)
        {
            foreach (Control item in select.Controls[1].Controls[0].Controls)
            {
                if (!string.IsNullOrEmpty(item.ID))
                {
                    if (item is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)item).Text = "";
                    }
                    if (item is DropDownList)
                    {
                        if (item.ID.Substring(0, 6).ToString() == "screen")
                        {
                            ((DropDownList)item).SelectedValue = "0";
                        }
                        else if (item.ID.Substring(0, 8).ToString() == "ddlLogic")
                        {
                            ((DropDownList)item).SelectedValue = "OR";
                        }
                        else if (item.ID.Substring(0, 6).ToString() == "ddlRelation")
                        {
                            ((DropDownList)item).SelectedValue = "0";
                        }
                    }
                }
            }
            //UCPaging1.CurrentPage = 1;
            //getArticle(true);
          //  bindData();
        }
        protected void search_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            getArticle(true);
            bindData();
        }
        private void GetSele()
        {
            foreach (Control item in select.Controls[1].Controls[0].Controls)
            {
                if (item is DropDownList)
                {
                    if (item.ID.Contains("screen"))
                    {
                        ((DropDownList)item).DataSource = bindItemList();
                        ((DropDownList)item).DataTextField = "value";
                        ((DropDownList)item).DataValueField = "key";
                        ((DropDownList)item).DataBind();
                        ((DropDownList)item).SelectedValue = "0";
                    }
                }
            }
        }
        private Dictionary<string, string> bindItemList()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();
            ItemList.Add("0", "请选择");
            ItemList.Add("ptcode", "批号");
            ItemList.Add("PIC_ENGID", "任务号");
            ItemList.Add("PIC_TUHAO", "图号");
            ItemList.Add("supplierresnm", "供应商");
            ItemList.Add("PIC_JGNUM", "加工单号");
            ItemList.Add("TA_ORDERNUM", "结算单号");
            ItemList.Add("MS_WSID", "外协单号");
            return ItemList;
        }
        //审批人勾选状态改变的时候（只现在我的未审）
        protected void CBSP_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }

        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }

        protected void rad_all_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
        }
        protected void rad_mypart_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
        }
        protected void rad_quanbu_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
        }
        protected void rad_weitijiao_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
        }
        protected void rad_shenhezhong_CheckedChanged(object sender, EventArgs e)
        {

            getArticle(true);
        }
        protected void rad_bohui_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
        }
        protected void rad_tongguo_CheckedChanged(object sender, EventArgs e)
        {
            //btn_input.Visible = true;
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
        }
        protected void rad_weizxing_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
        }
        protected void rad_yizhixing_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
        }
        protected void rad_ybj_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void selectall_CheckedChanged(object sender, EventArgs e)
        {
            if (selectall.Checked)
            {
                foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = true;
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = false;
                    }
                }
            }
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
        protected void checked_list_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

    #region 导出
        protected void btn_daochu_Click(object sender, EventArgs e)
        {
              CreateDataSource();
              string sqltext = "";  
              //string picid = "";
              sqltext = "select B.CM_PROJ,A.zdrnm, A.ptcode,A.PIC_ENGID,A.PIC_ENGNAME,A.PIC_TUHAO,B.MS_ZONGXU,MS_NAME+MS_GUIGE,MS_CAIZHI,MS_UNUM,MS_TUWGHT,MS_TUTOTALWGHT,MS_MASHAPE,MS_TECHUNIT,MS_YONGLIANG,MS_MATOTALWGHT,isnull(MS_LEN,''),isnull(MS_WIDTH,''),MS_NOTE,MS_XIALIAO,B.MS_PROCESS,MS_wxtype,isnull(MS_KU,'') as MS_KU,MS_ALLBEIZHU,A.price,A.detamount,A.marzxnum,A.supplierresnm,A.PIC_SUPPLYTIME from  View_TBMP_IQRCMPPRICE_RVW1 as A LEFT JOIN  View_TM_TASKDQO AS B ON (A.ptcode=B.MS_PID AND A.PIC_TUHAO=B.MS_TUHAO AND A.marnm=B.MS_NAME AND A.PIC_ZONGXU=B.MS_ZONGXU) where ";
                sqltext+=ViewState["sqlwhere"].ToString();
                ExportDataItem(sqltext);
        }
        private void ExportDataItem(string sqltext)
        {
            string filename = "外协计划单.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("外协计划单.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                //ISheet sheet0 = wk.GetSheetAt(0);
                ISheet sheet1 = wk.GetSheetAt(1);
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
                    return;
                }
                IRow row2 = sheet1.GetRow(2);
                row2.GetCell(1).SetCellValue(dt.Rows[0]["CM_PROJ"].ToString());
                IRow row3 = sheet1.GetRow(3);
                row3.GetCell(1).SetCellValue(dt.Rows[0]["zdrnm"].ToString());
                row3.GetCell(5).SetCellValue(DateTime.Now.ToString("yyyy/MM/dd"));
                //string basic_sql = "select MS_PJID, MS_ENGID,MS_PJNAME, MS_ENGNAME,MS_MAP from  where MS_ID='" + lotnum + "'";
                //System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(basic_sql);
                //IRow row2 = sheet0.GetRow(2);
                //row2.GetCell(1).SetCellValue(dt1.Rows[0]["MS_PJNAME"].ToString());
                //row2.GetCell(3).SetCellValue(dt1.Rows[0]["MS_PJID"].ToString());
                //row2.GetCell(5).SetCellValue(DateTime.Now.ToString("yyyy-MM-dd"));
                //IRow row3 = sheet0.GetRow(3);
                //row3.GetCell(1).SetCellValue(dt1.Rows[0]["MS_ENGID"].ToString());
                //IRow row4 = sheet0.GetRow(4);
                //row4.GetCell(1).SetCellValue(dt1.Rows[0]["MS_MAP"].ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 6);
                    for (int j = 0; j <= 26; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j+2].ToString());
                    }
                }
                 //sheet0.ForceFormulaRecalculation = true;
                sheet1.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
      #endregion
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string jgnum = txt_jgnum.Text.Trim().ToString();
            string code="";
            foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                if (cbx.Checked)
                {
                    System.Web.UI.WebControls.Label lb_picno = Reitem.FindControl("lbdh") as System.Web.UI.WebControls.Label;
                    code = lb_picno.Text.ToString();
                    string sqltxt = "";
                    if (jgnum != "")
                    {
                        sqltxt = " update TBMP_IQRCMPPRICE set PIC_JGNUM='" + jgnum + "' WHERE PIC_SHEETNO='" + code + "'";
                        DBCallCommon.ExeSqlText(sqltxt);
                        break;
                    }
                    else 
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有填写数据');", true);
                        return;
                    }
                }
            }
            
            getArticle(true);
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
        //比价单驳回数量
        private void getbh()
        {
            string sqltext = "";
            int num = 0;
            if (rad_mypart.Checked)
            {
                sqltext = "select count(*) from TBMP_IQRCMPPRCRVW where  " +
                             "(ICL_REVIEWA='" + Session["UserID"].ToString() + "' or ICL_REVIEWB='" + Session["UserID"].ToString() + "' or ICL_REVIEWC='" + Session["UserID"].ToString() + "' or ICL_REVIEWD='" + Session["UserID"].ToString() + "' or ICL_REVIEWE='" + Session["UserID"].ToString() + "' or ICL_REVIEWF='" + Session["UserID"].ToString() + "')" +
                             " and ICL_STATE='2'";
            }
            else if (rad_all.Checked)
            {
                sqltext = "select count(*) from TBMP_IQRCMPPRCRVW where ICL_STATE='2'";
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
        }
        //供应商信息发生变化
        protected void tb_Gongyingshang_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (tb_Gongyingshang.Text.ToString().Contains("|"))
            {
                Cname = tb_Gongyingshang.Text.Split('|')[1].ToString();
                tb_Gongyingshang.Text = Cname.Trim();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请正确填写供应商！');", true);
            }
        }

        protected void btn_baojian_click(object sender, EventArgs e)
        {
            string supplier = string.Empty;
            string pjnm = string.Empty;
            List<string> sqllist = new List<string>();

            string sql = "update TBMP_IQRCMPPRICE set PO_OperateState=NULL WHERE PO_OperateState='PUSH" + Session["UserID"].ToString() + "'";

            sqllist.Add(sql);

            for (int i = 0; i < checked_list_Repeater.Items.Count; i++)
            {
                if ((checked_list_Repeater.Items[i].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptc = (checked_list_Repeater.Items[i].FindControl("PIC_ID") as System.Web.UI.WebControls.Label).Text;
                    supplier += (checked_list_Repeater.Items[i].FindControl("supplierid") as System.Web.UI.WebControls.Label).Text + '_';
                    pjnm += (checked_list_Repeater.Items[i].FindControl("PIC_ENGID") as System.Web.UI.WebControls.Label).Text + '_';
                    sql = "update TBMP_IQRCMPPRICE set PO_OperateState='PUSH" + Session["UserID"].ToString() + "' WHERE PIC_ID='" + ptc + "'";
                    sqllist.Add(sql);
                }

            }
            if (sqllist.Count > 1)
            {
                string Supplybegin = supplier.Substring(0, supplier.IndexOf("_") + 1);
                string Supplyend = supplier.Replace(Supplybegin, "");
                if (Supplyend != "")
                {
                    getArticle(true); //刷新
                    string script = @"alert('请选择来自相同供货商的条目进行报检!');";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", script, true);
                    return;
                }
                string PJbegin = pjnm.Substring(0, pjnm.IndexOf("_") + 1);
                string PJend = pjnm.Replace(PJbegin, "");
                if (PJend != "")
                {
                    getArticle(true); //刷新
                    string script = @"alert('请选择来自相同任务号的条目进行报检!');";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", script, true);
                    return;
                }
                DBCallCommon.ExecuteTrans(sqllist);
                //Response.Redirect("~/QC_Data/QC_Inspection_Add.aspx?ACTION=NEW", false);
                //System.Web.UI.WebControls.CheckBox CKBOX_SELECT.Items.clear();
                getArticle(true);  //报检后刷新
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('/QC_Data/QC_Inspection_Add.aspx?ACTION=WXNEW&TYPE=WX');", true);
            }
            else
            {
                getArticle(true); //刷新
                string script = @"alert('请选择需要添加的条目!');";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", script, true);
            }



        }

        protected void btn_yuedaochuclick(object sender, EventArgs e)
        { 
          string sqltext = "";
          if (txt_year.Text.Trim().ToString() != "" && txt_year.Text.ToString().Contains("-"))
          {
              sqltext = "select picno,supplierresnm,PIC_JGNUM,PIC_BJSTATUS,PIC_JSNUM,CM_PROJ,PIC_ENGID,ptcode,PIC_TUHAO,engnm,marnm,ICL_WXTYPE,MS_PROCESS,marzxnum,PIC_SUPPLYTIME from View_TBMP_IQRCMPPRICE_RVW1 where convert(varchar(10),zdtime,23) like '%" + txt_year.Text.Trim().ToString() + "%' ";
              ExportDataItem1(sqltext);
          }
          else
          {
              System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('日期的格式输入有误！！\n请按正确格式输入！');</script>");
          }          
        }
        private void ExportDataItem1(string sqltext)
        {
            string year = txt_year.Text.Split('-')[0].ToString();
            string month = txt_year.Text.Split('-')[1].ToString();
            string filename = "" + year + "年" + month + "月外协任务合计.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("外协任务合计.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                // ISheet sheet1 = wk.GetSheetAt(1);
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');;</script>");
                    return;
                }
                IRow row1 = sheet0.GetRow(1);
                row1.GetCell(0).SetCellValue("" + year + "年" + month + "月外协任务合计");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 3);
                    for (int j = 0; j <= 14; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }
                sheet0.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }

        protected void btn_add_Click(object sender, EventArgs e)//追加订单
        {
            int j = 0;
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
        }
        protected void btn_xiatui_Click(object sender, EventArgs e)//生成订单
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
                else
                {
                    #region
                    string orderno = encodeorderno();//自动生成订单号
                    string sqltext;
                    string providerid = "";
                    string providernm = "";
                    string ptc = "";
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
                            ptc = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PTC")).Text;
                            sqltext = "INSERT INTO TBMP_ORDER (TO_DOCNUM, TO_BJDOCNUM, TO_PTC, TO_SUPPLYID,TO_ZDR,TO_ZDTIME) select '" + orderno + "',PIC_SHEETNO, PTC,PIC_SUPPLIERRESID,'" + Session["UserID"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "'from TBMP_IQRCMPPRICE  WHERE PTC='" + ptc + "'";
                            sqltextlist.Add(sqltext);
                            sqltext = "update TBMP_IQRCMPPRICE  set PIC_ORDERSTATE='1' where PTC='" + ptc + "'";//生成订单
                            sqltextlist.Add(sqltext);
                        }
                    }

                    DBCallCommon.ExecuteTrans(sqltextlist);
                    getArticle(true);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('PM_Xie_IntoOrder.aspx?orderno=" + orderno + "');", true);
                    #endregion
                }
        }
        /// <summary>
        /// 判断生成订单的数据
        /// </summary>
        /// <returns></returns>
        protected int isselected()
        {
            int temp = 0;
            string providerid = "";
            int i = 0;//是否选择数据
            int j = 0;//是否审核
            int k = 0;//供应商是否相同
            int l = 0;//选择的数据中是否包含已生成订单数据
          
            foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        string ptc = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PTC")).Text;
                        string sql = "select PTC from TBMP_IQRCMPPRICE where PTC='" + ptc + "' and PIC_ORDERSTATE='1'";
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
            else
            {
                temp = 0;
            }
            return temp;
        }
        /// <summary>
        /// 生成订单流水号
        /// </summary>
        /// <returns></returns>
        private string encodeorderno()
        {
            string pi_id = "";
            string tag_pi_id = "WX";
            string end_pi_id = "";
            string sqltext = "SELECT TOP 1 TO_DOCNUM FROM TBMP_Order WHERE TO_DOCNUM LIKE '" + tag_pi_id + "%' ORDER BY TO_DOCNUM DESC";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                end_pi_id = Convert.ToString(Convert.ToInt32(dt.Rows[0]["TO_DOCNUM"].ToString().Substring((dt.Rows[0]["TO_DOCNUM"].ToString().Length - 8), 8)) + 1);
                end_pi_id = end_pi_id.PadLeft(8, '0');
            }
            else
            {
                end_pi_id = "00000001";
            }
            pi_id = tag_pi_id + end_pi_id;
            return pi_id;
        }

       

    }
}
