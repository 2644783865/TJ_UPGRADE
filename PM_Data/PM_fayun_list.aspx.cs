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
using Microsoft.Office.Interop.MSProject;
using System.Collections.Generic;
using System.Drawing;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_fayun_list : BasicPage
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

            ViewState["ObjPageSize"] = Convert.ToDouble(DropDownList5.SelectedValue);
            if (!IsPostBack)
            {
                //string sf = "";
                //if (Request.QueryString["sf"] != null)
                //{
                //    sf = Request.QueryString["sf"].ToString();
                //}

                initpage1();
                initpower1();
                getArticle(true);
            }
            string sql = "delete from PM_CPFYJSDBY";
            DBCallCommon.ExeSqlText(sql);
            //getbh();
            CheckUser(ControlFinder);
        }

        private void initpage1()
        {
            //string sqltext = "";
            //sqltext = "select ST_NAME,ST_CODE from TBDS_STAFFINFO WHERE ST_DEPID='06' and ST_PD='0'";
            //string dataText = "ST_NAME";
            //string dataValue = "ST_CODE";
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
            if (Session["UserID"].ToString() == "2" || Session["UserID"].ToString() == "95" || Session["UserID"].ToString() == "47")
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
        }
        protected void btn_search_click(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void btn_clear_click(object sender, EventArgs e)
        {
            //Tb_PJNAME.Text = "";
            //Tb_ENGNAME.Text = "";
            tb_tsaid.Text = "";
            Tb_pcode.Text = "";
            txtSBMC.Text = "";
            txtXMMC.Text = "";
        }
        private void getArticle(bool isFirstPage)      //取得Article数据
        {
            CreateDataSource();
            string TableName = "View_TBMP_FAYUNPRICE_RVW AS A LEFT OUTER JOIN View_CM_FaHuo AS B on (A.TSA_ID2=B.TSA_ID and A.PM_ZONGXU=B.ID and A.PM_FID=B.CM_FID)  left join View_CM_TSAJOINPROJ as C on a.TSA_ID2=C.TSA_ID left join (select distinct JS_FATHERID from PM_CPFYJSD) as D on (A.ICL_SHEETNO=D.JS_FATHERID)";
            string PrimaryKey = "";
            string ShowFields = "A.ICL_SHEETNO,A.PM_FYNUM, A.ICL_REVIEWA, A.zdrnm,A.PM_ZONGXU,A.ICL_REVIEWATIME,A.ICL_ZHUGUANID ,A.ICL_IQRDATE,A.iclzgnm,A.ICL_FUZRID, A.iclfzrnm,A.PM_PRICE,A.PM_STATE,A.ICL_STATE,A.PM_CSTATE,A.PM_NOTE, A.PM_SUPPLIERRESID,A.supplierresnm,A.PM_ID,A.PM_SHUILV,C.TSA_PJID,B.CM_FID,C.CM_PROJ,A.TSA_ID2,PM_ENGNAME,B.CM_JHTIME,B.CM_CUSNAME,A.PM_FHDETAIL,D.JS_FATHERID";


            //数据库中的主键
            string OrderField = "A.ICL_SHEETNO";

            string GroupField = "";

            int OrderType = 1;
            /**/
            string StrWhere = ViewState["sqlwhere"].ToString();

            int PageSize = ObjPageSize;

            BindPage(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFirstPage);
            //CheckUser(ControlFinder);
        }
        public void CreateDataSource()
        {
            string sqlwhere = "";
            string filter = "";
            sqlwhere = "1=1 ";
            if (filter != "")
            {
                sqlwhere = sqlwhere + " and " + filter + "";
            }
            if (rad_mypart.Checked)
            {
                sqlwhere = sqlwhere + " and (ICL_REVIEWA='" + Session["UserID"].ToString() + "' or ICL_REVIEWB='" + Session["UserID"].ToString() + "' or ICL_REVIEWC='" + Session["UserID"].ToString() + "' or ICL_REVIEWD='" + Session["UserID"].ToString() + "' or ICL_REVIEWE='" + Session["UserID"].ToString() + "' or ICL_REVIEWF='" + Session["UserID"].ToString() + "' or ICL_REVIEWG='" + Session["UserID"].ToString() + "') ";
            }
            if (rad_weitijiao.Checked)//未提交
            {
                sqlwhere = sqlwhere + " and ICL_STATE='0'";
            }
            if (rad_shenhezhong.Checked)//审核中
            {
                sqlwhere = sqlwhere + " and (ICL_STATE='1' or ICL_STATE='3')";
            }
            if (rad_bohui.Checked)
            {
                sqlwhere = sqlwhere + " and ICL_STATE='2'";
            }
            if (rad_tongguo.Checked)
            {
                sqlwhere = sqlwhere + " and ICL_STATE='4'";
            }
            //已分配
            if (rad_yifenpei.Checked)
            {
                sqlwhere = sqlwhere + " and JS_FATHERID is not null";
            }
            //未分配
            if (rad_weifenpei.Checked)
            {
                sqlwhere = sqlwhere + " and JS_FATHERID is  null";
            }
            if (cb_sp.Checked == true)
            {
                sqlwhere = sqlwhere + " and ((ICL_REVIEWA='" + Session["UserID"].ToString() + "' and ICL_STATE='0') or (ICL_REVIEWB='" + Session["UserID"].ToString() + "' and ICL_STATE='1') or( ICL_REVIEWC='" + Session["UserID"].ToString() + "'  and ICL_STATE='3'))";
            }

            if (tb_tsaid.Text != "")
            {
                sqlwhere = sqlwhere + " and TSA_ID2 like '%" + tb_tsaid.Text.Trim() + "%'";
            }
            if (Tb_pcode.Text != "")
            {
                sqlwhere = sqlwhere + " and ICL_SHEETNO like '%" + Tb_pcode.Text.Trim() + "%'";
            }


            //发货商信息
            if (tb_Gongyingshang.Text != "")
            {
                sqlwhere = sqlwhere + " and supplierresnm like '%" + tb_Gongyingshang.Text.Trim() + "%'";
            }

            //审批人增加限制，只显示需要审批的任务   2013年5月19日 11:49:52

            string sqltext2 = "select distinct ICL_SHEETNO from View_TBMP_FAYUNPRICE_RVW left join PM_CPFYJSD  on ICL_SHEETNO=JS_FATHERID where  ";
            string sqltext3 = "select ICL_SHEETNO from View_TBMP_FAYUNPRICE_RVW left join PM_CPFYJSD  on ICL_SHEETNO=JS_FATHERID where ";
            sqltext2 = sqltext2 + sqlwhere;
            sqltext3 = sqltext3 + sqlwhere;
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext2);
            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext3);

            Label1.Text = Convert.ToString(dt.Rows.Count);
            Label2.Text = Convert.ToString(dt1.Rows.Count);

            if (txtXMMC.Text.Trim() != "")
            {
                sqlwhere += " and C.CM_PROJ like '%" + txtXMMC.Text.Trim() + "%'";
            }

            if (txtSBMC.Text.Trim() != "")
            {
                sqlwhere += " and PM_ENGNAME like '%" + txtSBMC.Text.Trim() + "%'";
            }
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

        //审批人勾选状态改变的时候（只现在我的未审）
        protected void CBSP_CheckedChanged(object sender, EventArgs e)
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
            //if (Session["UserID"].ToString() == "0601002" || Session["UserID"].ToString() == "0907001" || Session["UserID"].ToString() == "01002" || Session["UserID"].ToString() == "01001" || Session["UserID"].ToString() == "01003" || Session["UserID"].ToString() == "01004" || Session["UserID"].ToString() == "01006")
            //{
            //    cb_sp.Visible = true;
            //    cb_sp.Checked = true;
            //}
            //else
            //{
            //    cb_sp.Visible = false;
            //    cb_sp.Checked = false;
            //}
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
        //已分配
        protected void rad_yifenpei_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
            getArticle(true);
        }
        //未分配
        protected void rad_weifenpei_CheckedChanged(object sender, EventArgs e)
        {
            cb_sp.Visible = false;
            cb_sp.Checked = false;
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
            string sql = "select JS_FATHERID from PM_CPFYJSD";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            List<string> list = new List<string>();
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                list.Add(dt.Rows[i]["JS_FATHERID"].ToString());
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string ICL_SHEETNO = ((Label)e.Item.FindControl("lbdh")).Text;
                if (list.Contains(ICL_SHEETNO))
                {
                    ((Label)e.Item.FindControl("lbJT")).Text = "已分配";
                    ((Label)e.Item.FindControl("lbJT")).ForeColor = Color.Red;
                }
                else
                {
                    ((Label)e.Item.FindControl("lbJT")).Text = "未分配";
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

        protected void btnJSD_OnClick(object sender, EventArgs e)//*******************生成结算单
        {
            List<string> list = new List<string>();
            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();
            List<string> list4 = new List<string>();
            List<string> list5 = new List<string>();
            List<string> list6 = new List<string>();
            List<string> list7 = new List<string>();
            List<string> list8 = new List<string>();
            string supplierresnm = "";
            string ICL_SHEETNO = "";
            string CM_CONTR = "";
            string CM_PROJ = "";
            string TSA_ID = "";
            string CM_CUSNAME = "";
            string CM_JHTIME = "";
            string PM_ZONGXU = "";
            string TSA_ENGNAME = "";
            for (int i = 0, length = checked_list_Repeater.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)checked_list_Repeater.Items[i].FindControl("CKBOX_SELECT");
                if (cbx.Checked == true && rad_tongguo.Checked == true)
                {
                    list.Add(((Label)checked_list_Repeater.Items[i].FindControl("lbdh")).Text.Trim());
                    list1.Add(((Label)checked_list_Repeater.Items[i].FindControl("PM_ZONGXU")).Text.Trim());
                    list2.Add(((Label)checked_list_Repeater.Items[i].FindControl("CM_CONTR")).Text.Trim());
                    list4.Add(((Label)checked_list_Repeater.Items[i].FindControl("CM_PROJ")).Text.Trim());
                    list5.Add(((Label)checked_list_Repeater.Items[i].FindControl("TSA_ID")).Text.Trim());
                    list6.Add(((Label)checked_list_Repeater.Items[i].FindControl("CM_CUSNAME")).Text.Trim());
                    list7.Add(((Label)checked_list_Repeater.Items[i].FindControl("CM_JHTIME")).Text.Trim());
                    list8.Add(((Label)checked_list_Repeater.Items[i].FindControl("TSA_ENGNAME")).Text.Trim());
                    supplierresnm = ((Label)checked_list_Repeater.Items[i].FindControl("suppliernm")).Text.Trim();
                }
            }
            if (rad_tongguo.Checked == false)
            {
                Response.Write("<script>alert('您选择的单据未审批通过，不能生成结算单！！！')</script>");
                return;
            }
            if (list.Count == 0)
            {
                Response.Write("<script>alert('您还未选择单据，不能生成结算单！！！')</script>");
                return;
            }
            if (list.Count > 1)
            {
                Response.Write("<script>alert('每次只能分配一条比价单，请只选择一条！！！')</script>");
                return;
            }
            for (int i = 0, length = list.Count; i < length; i++)
            {
                if (i == 0)
                {
                    ICL_SHEETNO += list[i].ToString();
                }
                if (i > 0 && list[i].ToString() != list[i - 1].ToString())
                {
                    ICL_SHEETNO += "|" + list[i].ToString();
                }
            }
            for (int i = 0, length = list1.Count; i < length; i++)
            {
                if (i == 0)
                {
                    PM_ZONGXU += list1[i];
                }
                else
                {
                    PM_ZONGXU += "|" + list1[i];
                }
            }
            for (int i = 0, length = list2.Count; i < length; i++)
            {
                if (i == 0)
                {
                    CM_CONTR += list2[i];
                }
                else
                {
                    CM_CONTR += "|" + list2[i];
                }
            }
            for (int i = 0, length = list4.Count; i < length; i++)
            {
                if (i == 0)
                {
                    CM_PROJ += list4[i];
                }
                else
                {
                    CM_PROJ += "|" + list4[i];
                }
            }
            for (int i = 0, length = list5.Count; i < length; i++)
            {
                if (i == 0)
                {
                    TSA_ID += list5[i];
                }
                else
                {
                    TSA_ID += "|" + list5[i];
                }
            }
            for (int i = 0, length = list6.Count; i < length; i++)
            {
                if (i == 0)
                {
                    TSA_ENGNAME += list6[i];
                }
                else
                {
                    TSA_ENGNAME += "|" + list6[i];
                }
            }

            for (int i = 0, length = list7.Count; i < length; i++)
            {
                if (i == 0)
                {
                    CM_JHTIME += list7[i];
                }
                else
                {
                    CM_JHTIME += "|" + list7[i];
                }
            }
            for (int i = 0, length = list8.Count; i < length; i++)
            {
                if (i == 0)
                {
                    TSA_ENGNAME += list8[i];
                }
                else
                {
                    TSA_ENGNAME += "|" + list7[i];
                }
            }
            string sql1 = "select distinct JS_FATHERID from PM_CPFYJSD";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
            List<string> list3 = new List<string>();
            for (int i = 0, length = dt1.Rows.Count; i < length; i++)
            {
                list3.Add(dt1.Rows[i]["JS_FATHERID"].ToString());
            }
            if (list3.Contains(ICL_SHEETNO))
            {
                Response.Write("<script>alert('该比价单已被分配过，请不要重复分配！！！')</script>");
                return;
            }
            Response.Redirect("PM_CPFYJSD.aspx?action=add&SHEETNO=" + ICL_SHEETNO + "&ZONGXU=" + PM_ZONGXU + "&CM_CONTR=" + CM_CONTR + "&CM_PROJ=" + CM_PROJ + "&TSA_ID=" + TSA_ID + "&TSA_ENGNAME=" + TSA_ENGNAME + "&CM_CUSNAME=" + CM_CUSNAME + "&CM_JHTIME=" + CM_JHTIME + "&supplierresnm=" + supplierresnm);
        }


        //导出
        //protected void btn_daochu_Click(object sender, EventArgs e)
        //{
        //    int temp = ifselect();
        //    if (temp == 0)
        //    {
        //        string ordercode = "";
        //        string code = "";
        //        foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
        //        {
        //            System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
        //            if (cbx.Checked)
        //            {
        //                if (ordercode == "" || code != ((System.Web.UI.WebControls.Label)Reitem.FindControl("sheetno")).Text.ToString())
        //                {
        //                    ordercode += ((System.Web.UI.WebControls.Label)Reitem.FindControl("sheetno")).Text.ToString() + "/";
        //                }
        //                code = ((System.Web.UI.WebControls.Label)Reitem.FindControl("sheetno")).Text.ToString();
        //            }
        //        }
        //        ordercode = ordercode.Replace("/", "','");
        //        ordercode = ordercode.Substring(0, ordercode.LastIndexOf(",")).ToString();
        //        ordercode = "'" + ordercode;
        //        string sqltext = "";
        //        sqltext = "SELECT   picno, zdrnm, supplierresnm, pjnm, engnm, ptcode, marid, marnm, margg, marcz, margb, PIC_TUHAO, length, width, marnum, " +
        //                  "marunit, marfznum, marfzunit, price, detamount, detailnote , qoutefstsa, qoutescdsa, qoutelstsa, qoutefstsb, qoutescdsb, qoutelstsb, qoutefstsc, qoutescdsc," +
        //                  "qoutelstsc, qoutefstsd, qoutescdsd, qoutelstsd, qoutefstse, qoutescdse, qoutelstse, qoutefstsf, qoutescdsf, qoutelstsf, supplieranm, supplierbnm, " +
        //                  "suppliercnm, supplierdnm, supplierenm, supplierfnm  " +
        //                  "from View_TBMP_IQRCMPPRICE_RVW1  where picno in (" + ordercode + ") order by picno desc,ptcode asc";
        //        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
        //        ExportDataItem(dt);
        //    }
        //    else if (temp == 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要导出的比价单！！！');", true);
        //    }

        //}
        //private void ExportDataItem(System.Data.DataTable objdt)
        //{
        //    System.Data.DataTable dt = objdt;
        //    Application m_xlApp = new Application();
        //    Workbooks workbooks = m_xlApp.Workbooks;
        //    Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
        //    Worksheet wksheet;

        //    m_xlApp.Visible = false;    // Excel不显示  
        //    m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

        //    workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("采购比价单明细") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //    wksheet = (Worksheet)workbook.Sheets.get_Item(1);
        //    // 填充数据
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        wksheet.Cells[i + 3, 1] = Convert.ToString(i + 1);//序号

        //        wksheet.Cells[i + 3, 2] = "'" + dt.Rows[i]["picno"].ToString();//比价单号

        //        wksheet.Cells[i + 3, 3] = "'" + dt.Rows[i]["zdrnm"].ToString();//制单人

        //        wksheet.Cells[i + 3, 4] = "'" + dt.Rows[i]["supplierresnm"].ToString();//发货商

        //        wksheet.Cells[i + 3, 5] = "'" + dt.Rows[i]["pjnm"].ToString();//项目名称

        //        wksheet.Cells[i + 3, 6] = "'" + dt.Rows[i]["engnm"].ToString();//工程名称

        //        wksheet.Cells[i + 3, 7] = "'" + dt.Rows[i]["ptcode"].ToString();//计划跟踪号

        //        wksheet.Cells[i + 3, 8] = "'" + dt.Rows[i]["marid"].ToString();//物料编码

        //        wksheet.Cells[i + 3, 9] = "'" + dt.Rows[i]["marnm"].ToString();//名称

        //        wksheet.Cells[i + 3, 10] = "'" + dt.Rows[i]["margg"].ToString();//规格

        //        wksheet.Cells[i + 3, 11] = "'" + dt.Rows[i]["marcz"].ToString();//材质

        //        wksheet.Cells[i + 3, 12] = "'" + dt.Rows[i]["margb"].ToString();//国标

        //        wksheet.Cells[i + 3, 13] = "'" + dt.Rows[i]["PIC_TUHAO"].ToString();//图号

        //        wksheet.Cells[i + 3, 14] = "'" + dt.Rows[i]["length"].ToString();//长度

        //        wksheet.Cells[i + 3, 15] = "'" + dt.Rows[i]["width"].ToString();//宽度

        //        wksheet.Cells[i + 3, 16] = dt.Rows[i]["marnum"].ToString();//数量

        //        wksheet.Cells[i + 3, 17] = "'" + dt.Rows[i]["marunit"].ToString();//单位

        //        wksheet.Cells[i + 3, 18] = dt.Rows[i]["marfznum"].ToString();//辅助数量

        //        wksheet.Cells[i + 3, 19] = "'" + dt.Rows[i]["marfzunit"].ToString();//辅助单位

        //        wksheet.Cells[i + 3, 20] = dt.Rows[i]["price"].ToString();//单价

        //        wksheet.Cells[i + 3, 21] = dt.Rows[i]["detamount"].ToString();//金额

        //        wksheet.Cells[i + 3, 22] = "'" + dt.Rows[i]["detailnote"].ToString();//备注

        //        wksheet.Cells[i + 3, 23] = "'" + dt.Rows[i]["supplieranm"].ToString();//发货商1
        //        wksheet.Cells[i + 3, 24] = dt.Rows[i]["qoutefstsa"].ToString();//发货商1
        //        wksheet.Cells[i + 3, 25] = dt.Rows[i]["qoutescdsa"].ToString();//发货商1
        //        wksheet.Cells[i + 3, 26] = dt.Rows[i]["qoutelstsa"].ToString();//发货商1

        //        wksheet.Cells[i + 3, 27] = "'" + dt.Rows[i]["supplierbnm"].ToString();//发货商2
        //        wksheet.Cells[i + 3, 28] = dt.Rows[i]["qoutefstsb"].ToString();//发货商2
        //        wksheet.Cells[i + 3, 29] = dt.Rows[i]["qoutescdsb"].ToString();//发货商2
        //        wksheet.Cells[i + 3, 30] = dt.Rows[i]["qoutelstsb"].ToString();//发货商2

        //        wksheet.Cells[i + 3, 31] = "'" + dt.Rows[i]["suppliercnm"].ToString();//发货商3
        //        wksheet.Cells[i + 3, 32] = dt.Rows[i]["qoutefstsc"].ToString();//发货商3
        //        wksheet.Cells[i + 3, 33] = dt.Rows[i]["qoutescdsc"].ToString();//发货商3
        //        wksheet.Cells[i + 3, 34] = dt.Rows[i]["qoutelstsc"].ToString();//发货商3

        //        wksheet.Cells[i + 3, 35] = "'" + dt.Rows[i]["supplierdnm"].ToString();//发货商4
        //        wksheet.Cells[i + 3, 36] = dt.Rows[i]["qoutefstsd"].ToString();//发货商4
        //        wksheet.Cells[i + 3, 37] = dt.Rows[i]["qoutescdsd"].ToString();//发货商4
        //        wksheet.Cells[i + 3, 38] = dt.Rows[i]["qoutelstsd"].ToString();//发货商4

        //        wksheet.Cells[i + 3, 39] = "'" + dt.Rows[i]["supplierenm"].ToString();//发货商5
        //        wksheet.Cells[i + 3, 40] = dt.Rows[i]["qoutefstse"].ToString();//发货商5
        //        wksheet.Cells[i + 3, 41] = dt.Rows[i]["qoutescdse"].ToString();//发货商5
        //        wksheet.Cells[i + 3, 42] = dt.Rows[i]["qoutelstse"].ToString();//发货商5

        //        wksheet.Cells[i + 3, 43] = "'" + dt.Rows[i]["supplierfnm"].ToString();//发货商6
        //        wksheet.Cells[i + 3, 44] = dt.Rows[i]["qoutefstsf"].ToString();//发货商6
        //        wksheet.Cells[i + 3, 45] = dt.Rows[i]["qoutescdsf"].ToString();//发货商6
        //        wksheet.Cells[i + 3, 46] = dt.Rows[i]["qoutelstsf"].ToString();//发货商6

        //        wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 46]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
        //        wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 46]).VerticalAlignment = XlVAlign.xlVAlignCenter;
        //        wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 46]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        //    }
        //    //设置列宽
        //    wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

        //    string filename = Server.MapPath("/PC_Data/ExportFile/" + "采购比价单明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

        //    ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        //}

        ///// <summary>
        ///// 输出Excel文件并退出
        ///// </summary>
        ///// <param name="filename"></param>
        ///// <param name="workbook"></param>
        //private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
        //{
        //    try
        //    {

        //        workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //        workbook.Close(Type.Missing, Type.Missing, Type.Missing);
        //        m_xlApp.Workbooks.Close();
        //        m_xlApp.Quit();
        //        m_xlApp.Application.Quit();

        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);

        //        wksheet = null;
        //        workbook = null;
        //        m_xlApp = null;
        //        GC.Collect();

        //        //下载

        //        System.IO.FileInfo path = new System.IO.FileInfo(filename);

        //        //同步，异步都支持
        //        HttpResponse contextResponse = HttpContext.Current.Response;
        //        contextResponse.Redirect(string.Format("~/PC_Data/ExportFile/{0}", path.Name), false);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        //private int ifselect()
        //{
        //    int temp = 0;
        //    int i = 0;//是否选择数据
        //    foreach (RepeaterItem Reitem in checked_list_Repeater.Items)
        //    {
        //        System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
        //        if (cbx.Checked)
        //        {
        //            i++;
        //        }
        //    }
        //    if (i == 0)//未选择数据
        //    {
        //        temp = 1;
        //    }
        //    else
        //    {
        //        temp = 0;
        //    }
        //    return temp;
        //}
        //比价单驳回数量
        //private void getbh()
        //{
        //    string sqltext = "";
        //    int num = 0;
        //    if (rad_mypart.Checked)
        //    {
        //        sqltext = "select count(*) from TBMP_IQRCMPPRCRVW where  " +
        //                     "(ICL_REVIEWA='" + Session["UserID"].ToString() + "' or ICL_REVIEWB='" + Session["UserID"].ToString() + "' or ICL_REVIEWC='" + Session["UserID"].ToString() + "' or ICL_REVIEWD='" + Session["UserID"].ToString() + "' or ICL_REVIEWE='" + Session["UserID"].ToString() + "' or ICL_REVIEWF='" + Session["UserID"].ToString() + "')" +
        //                     " and ICL_STATE='2'";
        //    }
        //    else if (rad_all.Checked)
        //    {
        //        sqltext = "select count(*) from TBMP_IQRCMPPRCRVW where  ICL_STATE='2'";
        //    }

        //    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
        //    while (dr.Read())
        //    {
        //        num += Convert.ToInt32(dr[0].ToString());
        //    }

        //    if (num == 0)
        //    {
        //        lb_bjdbh.Visible = false;
        //    }
        //    else
        //    {
        //        lb_bjdbh.Visible = true;
        //        lb_bjdbh.Text = "(" + num.ToString() + ")";
        //    }
        //}

        //发货商信息发生变化
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请正确填写发货商！');", true);
            }
        }
    }
}
