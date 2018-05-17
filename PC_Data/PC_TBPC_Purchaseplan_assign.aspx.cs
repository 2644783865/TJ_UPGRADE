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
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchaseplan_assign : BasicPage
    {
        PagerQueryParamGroupBy pager = new PagerQueryParamGroupBy();
        //添加类
        private List<QueryItemInfo> queryItems = new List<QueryItemInfo>();
        protected string PageName
        {
            get
            {
                return " PC_TBPC_Purchaseplan_assign.aspx";
            }

        }
        ////页数
        public int ObjPageSize
        {
            get
            {
                //默认是升序
                ViewState["ObjPageSize"] = DropDownList5.SelectedValue;
                return Convert.ToInt32(ViewState["ObjPageSize"]);
            }
            set
            {
                ViewState["ObjPageSize"] = value;
            }
        }
        public string gloabstate//状态，询比价6、下订单7
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
        public System.Data.DataTable gloabt//变更表
        {
            get
            {
                object dt = ViewState["gloabt"];
                return dt == null ? null : (System.Data.DataTable)dt;
            }
            set
            {
                ViewState["gloabt"] = value;
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
        string ptc;
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_hclose.Attributes.Add("onclick", "form.target='_blank'");
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                hid_filter.Text = "View_TBPC_PURCHASEPLAN_IRQ_ORDER" + "/" + Session["UserID"].ToString();
                string sf = "";
                if (Request.QueryString["sf"] != null)
                {
                    sf = Request.QueryString["sf"].ToString();
                }
                if (Request.QueryString["ptc"] != null)
                {
                    ptc = Request.QueryString["ptc"].ToString();
                    rad_all.Checked = true;
                    rad_mypart.Checked = false;
                    rad_stall.Checked = true;
                    rad_allwzx.Checked = false;
                }
                if (sf != "1")
                {
                    string sqltext = "";
                    sqltext = "delete from TBPC_FILTER_INFO where tableuser='" + hid_filter.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    sqltext = "delete from TBPC_COLUMNFILTER_INFO where tableuser='" + hid_filter.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                }
                bindPageCondition();
                initpager1();
                getArticle(true, false);
            }
            CheckUser(ControlFinder);
            if (rad_mypart.Checked)
            {
                btn_zhuijia.Enabled = true;
            }
            else
            {
                btn_zhuijia.Enabled = false;
            }
        }

        private void bindPageCondition()
        {
            string sqlSelect = "SELECT controlID,controlType,controlValue from QueryInfo where [userID]='" + Session["UserID"] + "' and [pageName]='" + PageName + "' ";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlSelect);
            while (dr.Read())
            {
                ConditionToPageControl(dr["controlID"].ToString(), dr["controlType"].ToString(), dr["controlValue"].ToString());

            }
            dr.Close();

        }
        private void ConditionToPageControl(string controlID, string type, string controlValue)
        {
            if (controlValue == "1")
            {
                //(this.UpdatePanelCondition.FindControl(controlID) as System.Web.UI.WebControls.CheckBox).Checked = true;
            }
        }

        private void initpager1()
        {
            string sqltext = "";
            sqltext = "select ST_NAME,ST_ID from TBDS_STAFFINFO WHERE ST_DEPID='05' and ST_PD='0'";
            string dataText = "ST_NAME";
            string dataValue = "ST_ID";
            DBCallCommon.BindDdl(drp_stu, sqltext, dataText, dataValue);
            drp_stu.SelectedIndex = 0;
            if (Request.QueryString["ptc"] != null)
            {
                gloabptc = Request.QueryString["ptc"].ToString();
                btn_back.Visible = true;
            }
            else
            {
                gloabptc = "";
            }
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
                rad_stall.Checked = true; rad_stwzx.Checked = false; rad_stxbj.Checked = false; rad_stxdd.Checked = false;
            }
            else if (NUM2 == "2")
            {
                rad_stall.Checked = false; rad_stwzx.Checked = true; rad_stxbj.Checked = false; rad_stxdd.Checked = false;
            }
            else if (NUM2 == "3")
            {
                rad_stall.Checked = false; rad_stwzx.Checked = false; rad_stxbj.Checked = true; rad_stxdd.Checked = false;
            }
            else if (NUM2 == "4")
            {
                rad_stall.Checked = false; rad_stwzx.Checked = false; rad_stxbj.Checked = false; rad_stxdd.Checked = true;
            }
        }
        private void initglotb()
        {
            string sqltext = "SELECT MP_OLDPTCODE FROM TBPC_MPCHANGEDETAIL WHERE  MP_STATE='0'";
            gloabt = DBCallCommon.GetDTUsingSqlText(sqltext);//变更表

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

            pager.GroupField = GroupField;

            pager.OrderType = OrderType;

            pager.StrWhere = StrWhere;

            pager.PageSize = PageSize;
        }

        protected void bindData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamGroupBy(pager);
            CommonFun.Paging(dt, tbpc_purshaseplanassignRepeater, UCPaging1, NoDataPane);
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
            getArticle(false, false);
            ScriptManager.RegisterStartupScript(this.Page, GetType(), DateTime.Now.ToString("ssffff"), "sTable()", true);
        }

        protected void btn_search_click(object sender, EventArgs e)
        {
            getArticle(true, false);
        }
        protected void btn_clear_click(object sender, EventArgs e)
        {
            //Tb_PJNAME.Text = "";
            Tb_ENGNAME.Text = "";
            tb_marnm.Text = "";
            tb_ptc.Text = "";
            Tb_pcode.Text = "";
            tbtype.Text = "";
            drp_stu.SelectedIndex = 0;
        }
        private void addQueryItem(string pageName, string controlID, string controlType, string controlValue)
        {

            QueryItemInfo queryItem = new QueryItemInfo(Session["UserID"].ToString(), pageName, controlID, controlType, controlValue);

            queryItems.Add(queryItem);

        }

        private void getArticle(bool isFirstPage, bool isUpdateCondition)      //取得Article数据
        {
            if (isUpdateCondition)
            {

                List<string> ltsql = new List<string>();

                string strsql = "delete from QueryInfo where userID='" + Session["UserID"].ToString() + "' and pageName='" + PageName + "'";

                ltsql.Add(strsql);

                foreach (QueryItemInfo ItemInfo in queryItems)
                {
                    strsql = "insert into QueryInfo (userID,pageName,controlID,controlType,controlValue) VALUES ('" + ItemInfo.UserID + "','" + ItemInfo.PageName + "','" + ItemInfo.ControlID + "','" + ItemInfo.ControlType + "','" + ItemInfo.ControlValue + "')";
                    ltsql.Add(strsql);
                }

                DBCallCommon.ExecuteTrans(ltsql);

            }
            CreateDataSource();

            string TableName = "View_TBPC_PURCHASEPLAN_RVW";

            string PrimaryKey = "";

            string ShowFields = "planno AS PUR_PCODE, pjid as PUR_PJID, pjnm as PUR_PJNAME,engid as PUR_ENGID,PUR_ID,sqrid,sqrnm," +
                         "engnm as PUR_ENGNAME,ptcode as PUR_PTCODE,marid as PUR_MARID,marnm as PUR_MARNAME," +
                         "margg as PUR_MARNORM,marcz as PUR_MARTERIAL,margb as PUR_GUOBIAO,marunit as PUR_NUNIT," +
                         "marfzunit as FZUNIT,isnull(length,0) as PUR_LENGTH,isnull(width,0) as PUR_WIDTH,isnull(rpnum,0) as PUR_RPNUM," +
                         "rpfznum as PUR_RPFZNUM,jstimerq as PUR_TIMEQ,fgrnm AS PUR_PTASMAN,fgtime as PUR_PTASTIME, " +
                        "cgrid as PUR_CGMAN,cgrnm as PUR_CGMANNM,keycoms as PUR_KEYCOMS,purstate as PUR_STATE," +
                //"purnote as PUR_NOTE,picno as PIC_SHEETNO,SUBSTRING(irqdata,1,10) as ICL_IQRDATE," +CONVERT(varchar(12) , irqdata, 102 ) as ICL_IQRDATE,
                        "purnote as PUR_NOTE,'' as PIC_SHEETNO," +
                        "'' as ICL_REVIEWA,'' as ICL_REVIEWANM,'' as ZXNUM,'' as ZXFZNUM," +
                        "'' as PO_SHEETNO,PUR_TUHAO,PUR_MASHAPE,PR_MAP,PR_CHILDENGNAME,PUR_IFFAST ";

            //数据库中的主键
            string OrderField = "planno desc,ptcode";

            string GroupField = "";

            int OrderType = 0;
            /**/
            string StrWhere = ViewState["sqlwhere"].ToString();

            int PageSize = ObjPageSize;

            BindPage(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFirstPage);

        }

        public void CreateDataSource()
        {
            string sqltext = "";
            string sqlwhere = "";
            string tableuser = hid_filter.Text;
            string filter = "";
            sqltext = "SELECT tableuser, filter FROM TBPC_FILTER_INFO where tableuser='" + tableuser + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                filter = dr[1].ToString();
            }
            dr.Close();

            sqlwhere = "PUR_CSTATE='0' and purstate!='8' and purstate!='9'";
            if (filter != "")
            {
                sqlwhere = sqlwhere + " and " + filter + "";
            }
            if (rad_all.Checked)
            {
                sqlwhere = sqlwhere + " and purstate>='4'";
            }
            else if (rad_mypart.Checked)
            {
                sqlwhere = sqlwhere + " and purstate>='4' and cgrid='" + Session["UserID"].ToString() + "'";
            }

            //全部未执行（2013年4月22日 08:46:28）
            if (rad_allwzx.Checked)
            {
                sqlwhere = sqlwhere + " and (purstate='4' or purstate='5')";
            }

            //未执行（不包括招标物料）
            if (rad_stwzx.Checked)
            {
                sqlwhere = sqlwhere + " and (purstate='4' or purstate='5') and (not exists (select IB_MARID from TBPC_INVITEBID where TBPC_INVITEBID.IB_STATE = '0' and View_TBPC_PURCHASEPLAN_RVW.marid=TBPC_INVITEBID.IB_MARID))";
            }

            //未执行（招标物料）
            if (rad_szbwzx.Checked)
            {
                sqlwhere = sqlwhere + " and (purstate='4' or purstate='5') and (exists (select IB_MARID from TBPC_INVITEBID where TBPC_INVITEBID.IB_STATE = '0' and View_TBPC_PURCHASEPLAN_RVW.marid=TBPC_INVITEBID.IB_MARID))";
            }

            if (rad_stxbj.Checked)
            {
                sqlwhere = sqlwhere + " and purstate>='6'";
            }

            if (rad_stxdd.Checked)
            {
                sqlwhere = sqlwhere + " and purstate>='7'";
            }

            //if (Tb_PJNAME.Text != "")
            //{
            //    sqlwhere = sqlwhere + " and marnm like '%" + Tb_PJNAME.Text.Trim() + "%'";
            //}
            if (Tb_ENGNAME.Text != "")
            {
                sqlwhere = sqlwhere + " and ptcode like '%" + Tb_ENGNAME.Text.Trim() + "%'";
            }
            if (tbtype.Text != "")
            {
                sqlwhere = sqlwhere + " and PUR_MASHAPE like '%" + tbtype.Text.Trim() + "%'";
            }
            if (tb_ptc.Text != "")
            {
                sqlwhere = sqlwhere + " and margg like '%" + tb_ptc.Text.Trim() + "%'";
            }
            if (tb_bjname.Text != "")
            {
                sqlwhere = sqlwhere + " and PR_CHILDENGNAME like '%" + tb_bjname.Text.Trim() + "%'";
            }
            if (tb_marnm.Text != "")
            {
                sqlwhere = sqlwhere + " and marnm like '%" + tb_marnm.Text.Trim() + "%'";
            }
            if (Tb_pcode.Text != "")
            {
                sqlwhere = sqlwhere + " and planno like '%" + Tb_pcode.Text.Trim() + "%'";
            }
            if (drp_stu.SelectedValue.ToString() != "-请选择-")
            {
                sqlwhere = sqlwhere + " and cgrid='" + drp_stu.SelectedValue.ToString() + "'";
            }
            if (ptc != "")
            {
                sqlwhere += " and ptcode like '%" + ptc + "%'";
            }
            ViewState["sqlwhere"] = sqlwhere;
        }

        protected void selectall_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_selectall.Checked)
            {
                foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
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
                foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
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
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
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
                foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
                {
                    k++;
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
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
                getArticle(false, false);
            }
        }
        protected void btn_QX_click(object sender, EventArgs e)
        {
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                cbx.Checked = false;
            }
        }

        protected void rad_all_CheckedChanged(object sender, EventArgs e)//所有任务
        {
            btn_hclose.Enabled = false;
            getArticle(true, false);
        }
        protected void rad_mypart_CheckedChanged(object sender, EventArgs e)//我的任务
        {
            btn_hclose.Enabled = true;
            getArticle(true, false);
        }
        protected void rad_stall_CheckedChanged(object sender, EventArgs e)//全部
        {
            btn_hclose.Enabled = false;
            btn_Iqrcmpprc.Enabled = false;
            btn_zhuijia.Enabled = false;
            getArticle(true, false);
        }
        protected void rad_allwzx_CheckedChanged(object sender, EventArgs e)//全部未执行 （包括招标物料2013年4月22日 08:23:46）
        {
            btn_hclose.Enabled = true;
            btn_Iqrcmpprc.Enabled = true;
            btn_zhuijia.Enabled = true;
            getArticle(true, false);
        }
        protected void rad_stwzx_CheckedChanged(object sender, EventArgs e)//未执行 （非招标未执行）
        {
            btn_hclose.Enabled = true;
            btn_Iqrcmpprc.Enabled = true;
            btn_zhuijia.Enabled = true;
            getArticle(true, false);
        }
        protected void rad_stxbj_CheckedChanged(object sender, EventArgs e)//询比价
        {
            btn_Iqrcmpprc.Enabled = false;
            btn_zhuijia.Enabled = false;
            btn_hclose.Enabled = false;
            getArticle(true, false);
        }
        protected void rad_stxdd_CheckedChanged(object sender, EventArgs e)//下订单
        {
            btn_Iqrcmpprc.Enabled = false;
            btn_zhuijia.Enabled = false;
            btn_hclose.Enabled = false;
            getArticle(true, false);
        }

        protected void rad_szbwzx_CheckedChanged(object sender, EventArgs e)//  招标物料未执行
        {
            //btn_marrep.Enabled = false;
            //btn_Iqrcmpprc.Enabled = false;
            btn_zhuijia.Enabled = true;
            //btn_hclose.Enabled = false;
            getArticle(true, false);
        }

        //询比价
        protected void btn_Iqrcmpprc_Click(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            double rpnum = 0;
            int j = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
            {
                if (((System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT")).Checked)
                {
                    rpnum = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_RPNUM")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_RPNUM")).Text);
                    if (rpnum == 0)
                    {
                        j++;
                    }
                }
            }
            if (j > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('包含采购数量为0的数据，不能下推比价单！');", true);
                getArticle(false, false);
            }
            else
            {
                int temp = isselected();
                if (temp == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
                    getArticle(false, false);
                }
                else if (temp == 2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您选择的数据中包含已询比价的记录，请重新选择');", true);
                    getArticle(false, false);
                }
                else if (temp == 3)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您选择了已代用的数据，请重新选择');", true);
                    getArticle(false, false);
                }
                else
                {
                    string sheetcode = encodesheetno();//生成比价单号
                    string sqltext1;
                    string sqltext2;
                    string sqltext3;
                    string ptcode = "";
                    string marid = "";
                    double num = 0;
                    double fznum = 0;
                    double zdjweight = 0;
                    string fzdw = "";

                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string manid = Session["UserID"].ToString();
                    sqltext1 = "INSERT INTO TBPC_IQRCMPPRCRVW(ICL_SHEETNO,ICL_IQRDATE," +
                               "ICL_REVIEWA) VALUES('" + sheetcode + "','" + time + "','" + manid + "')";
                    sqltextlist.Add(sqltext1);

                    foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
                    {
                        if (((System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT")).Checked)
                        {
                            string pjid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PJID")).Text;
                            string engid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_ENGID")).Text;
                            string note = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_NOTE")).Text;
                            marid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_MARID")).Text;
                            ptcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PTCODE")).Text;
                            string tuhao = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_TUHAO")).Text;
                            num = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_RPNUM")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_RPNUM")).Text);
                            fznum = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_RPFZNUM")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_RPFZNUM")).Text);
                            zdjweight = fznum;
                            double length = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_LENGTH")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_LENGTH")).Text);
                            double width = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_WIDTH")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_WIDTH")).Text);
                            string keycoms = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_KEYCOMS")).Text;
                            string shape = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_MASHAPE")).Text;
                            string sqrid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("sqrid")).Text;
                            string map = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PR_MAP")).Text;
                            string engname = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PR_CHILDENGNAME")).Text;
                            string picode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PCODE")).Text;
                            fzdw = ((System.Web.UI.WebControls.Label)Reitem.FindControl("FZUNIT")).Text;
                            //更改税率16 2018.5.16
                            sqltext2 = "INSERT INTO TBPC_IQRCMPPRICE(PIC_SHEETNO,PIC_PJID,PIC_ENGID," +
                                       "PIC_MARID,PIC_QUANTITY,PIC_LENGTH,PIC_WIDTH,PIC_FZNUM,PIC_ZXNUM," +
                                       "PIC_ZXFUNUM,PIC_ZDJNUM,PIC_PTCODE,PIC_KEYCOMS,PIC_TUHAO,PIC_MASHAPE,PIC_NOTE,PIC_TECUNIT,PIC_SQRID,PIC_MAP,PIC_CHILDENGNAME,PIC_PICODE,PIC_IFFAST,PIC_SHUILV)  " +
                                       "VALUES('" + sheetcode + "','" + pjid + "','" + engid + "','" + marid + "'," +
                                       "'" + num + "','" + length + "','" + width + "','" + fznum + "','" + num + "', " +
                                       "'" + fznum + "','" + zdjweight + "','" + ptcode + "','" + keycoms + "','" + tuhao + "','" + shape + "','" + note + "','" + fzdw + "','" + sqrid + "','" + map + "','" + engname + "','" + picode + "','" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_IFFAST")).Text.Trim() + "','16')";
                            sqltextlist.Add(sqltext2);
                            sqltext3 = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE='6' WHERE " +
                                       "PUR_PTCODE='" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PTCODE")).Text + "' and PUR_CSTATE='0'";//生成比价单
                            sqltextlist.Add(sqltext3);
                        }
                    }
                    List<string> sqltextlistFCF = sqltextlist.Distinct().ToList(); //防止出现重复  2013年5月31日 16:41:07   Meng
                    DBCallCommon.ExecuteTrans(sqltextlistFCF);
                    getArticle(false, false);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('PC_TBPC_Purchaseplan_compareprice.aspx?sheetno=" + sheetcode + "');", true);
                }
            }

        }
        //代用
        protected void btn_marrep_Click(object sender, EventArgs e)
        {
            int temp = isselected();
            if (temp == 0)//是否选择数据
            {
                if (is_sameeng())//是否同一批同一工程
                {
                    string sqltext = "";
                    List<string> sqltextlist = new List<string>();
                    string planpcode = "";
                    string ptcode = "";
                    string marid = "";
                    double num = 0;
                    double fznum = 0;
                    string note = "";
                    string mpcode = generatecode();
                    string length = "";
                    string width = "";
                    foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
                    {
                        System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                        if (cbx != null)//存在行
                        {
                            if (cbx.Checked)
                            {
                                planpcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PCODE")).Text;
                                ptcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PTCODE")).Text;
                                marid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_MARID")).Text;
                                num = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_RPNUM")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_RPNUM")).Text);
                                fznum = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_RPFZNUM")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_RPFZNUM")).Text);
                                note = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_NOTE")).Text;
                                length = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_LENGTH")).Text;
                                width = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_WIDTH")).Text;

                                sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE='5' WHERE PUR_PTCODE='" + ptcode + "'";//计划状态改为5为代用
                                sqltextlist.Add(sqltext);
                                sqltext = "INSERT INTO TBPC_MARREPLACEALL(MP_CODE,MP_PTCODE,MP_MARID,MP_NUM,MP_FZNUM,MP_NOTE,MP_LENGTH,MP_WIDTH) " +
                                           "VALUES('" + mpcode + "','" + ptcode + "','" + marid + "','" + num + "','" + fznum + "','" + note + "','" + length + "','" + width + "')";
                                sqltextlist.Add(sqltext);
                            }
                        }
                    }

                    string str = "select ST_ID from TBDS_STAFFINFO where ST_POSITION='0501' and ST_PD='0'";
                    System.Data.DataTable leader = DBCallCommon.GetDTUsingSqlText(str);
                    string lead = "7";
                    if (leader.Rows.Count > 0)
                    {
                        lead = leader.Rows[0][0].ToString();
                    }
                    sqltext = "INSERT INTO TBPC_MARREPLACETOTAL(MP_CODE,MP_PLANPCODE,MP_PJID,MP_ENGID," +
                             "MP_FILLFMID,MP_FILLFMTIME,MP_REVIEWAID,MP_CHARGEID,MP_LEADER)  " +
                             "select '" + mpcode + "',PR_SHEETNO,PR_PJID,PR_ENGID,'" + Session["UserID"].ToString() + "','" +
                             DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',PR_SQREID,PR_FZREID,'" + lead + "' " +
                             "from TBPC_PCHSPLANRVW where PR_SHEETNO='" + planpcode + "'";

                    sqltextlist.Add(sqltext);
                    DBCallCommon.ExecuteTrans(sqltextlist);
                    //DBCallCommon.ExeSqlText(sqltext);
                    Response.Redirect("~/PC_Data/PC_TBPC_Marreplace_panel.aspx?mpno=" + mpcode);//转到代用页面
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择同一批同一工程下的物料,本次操作无效！');", true);
                    getArticle(false, false);
                }
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
                getArticle(false, false);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您选择了已询价的数据,本次操作无效！');", true);
                getArticle(false, false);
            }
            else if (temp == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您选择了代用审核中的数据,本次操作无效！');", true);
                getArticle(false, false);
            }
        }

        protected int isselected()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            int j = 0;//选择的数据中是否包含有已询价的数据
            int k = 0;
            //int count = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        string ptcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PTCODE")).Text;
                        string sql = "select PIC_PTCODE from TBPC_IQRCMPPRICE where PIC_PTCODE='" + ptcode + "'";
                        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        if (dt.Rows.Count > 0 || Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_STATE")).Text) >= 6)
                        {
                            j++;
                        }
                        sql = "select PUR_STATE from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";
                        System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                        if (dt1.Rows.Count > 0)
                        {
                            string state = dt1.Rows[0]["PUR_STATE"].ToString();
                            if (state == "5")
                            {
                                k++;
                            }
                        }
                    }
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else if (j > 0)//选择的数据中是否包含有已询价的数据
            {
                temp = 2;
            }
            else if (k > 0)
            {
                temp = 3;
            }
            else
            {
                temp = 0;//可以下推
            }
            return temp;
        }
        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_assign_list.aspx?ptc=" + gloabptc);
        }
        private bool is_sameeng()//判断是否同一批同一工程
        {
            string temppcode = "";
            bool temp = true;
            int i = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        if (i == 1)
                        {
                            temppcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PCODE")).Text.ToString();
                        }
                        else
                        {
                            if (temppcode != ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PCODE")).Text.ToString())
                            {
                                temp = false;
                                break;
                            }
                        }
                    }
                }
            }
            return temp;
        }
        protected string encodesheetno()//比价单编号8位
        {
            string sheetcode = "";
            string sqltext = "SELECT  top 1 ICL_SHEETNO FROM TBPC_IQRCMPPRCRVW where substring(ICL_SHEETNO,1,2)!='FZ'  ORDER BY ICL_SHEETNO DESC";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                sheetcode = dt.Rows[0][0].ToString();//单号
                sheetcode = Convert.ToString(Convert.ToInt32(sheetcode) + 1);
                sheetcode = sheetcode.PadLeft(8, '0');
            }
            else
            {
                sheetcode = "00000001";
            }
            return sheetcode;
        }

        private string generatecode()//生成代用单号
        {
            string subcode = "";
            string mpcode = "";
            string sqltext = "SELECT TOP 1 MP_CODE FROM TBPC_MARREPLACETOTAL WHERE MP_CODE LIKE '" + DateTime.Now.Year.ToString() + "1" + "%' ORDER BY MP_CODE DESC";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                subcode = Convert.ToString(dt.Rows[0][0]).Substring(Convert.ToString(dt.Rows[0][0]).ToString().Length - 5, 5);//后五位流水号
                subcode = Convert.ToString(Convert.ToInt32(subcode) + 1);
                subcode = subcode.PadLeft(5, '0');
            }
            else
            {
                subcode = "00001";
            }
            mpcode = DateTime.Now.Year.ToString() + "1" + subcode;
            return mpcode;
        }

        protected void tbpc_purshaseplanassignRepeaterbind(object sender, RepeaterItemEventArgs e)
        {
            string state = "";
            //string bjdsheetno = "";
            //string ddsheetno = "";
            string ptcode = "";
            if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlTableCell ph = (HtmlTableCell)e.Item.FindControl("ph");
                HtmlTableCell xm = (HtmlTableCell)e.Item.FindControl("xm");
                HtmlTableCell gc = (HtmlTableCell)e.Item.FindControl("gc");
                HtmlTableCell jhh = (HtmlTableCell)e.Item.FindControl("jhh");
                HtmlTableCell th = (HtmlTableCell)e.Item.FindControl("th");
                HtmlTableCell clbm = (HtmlTableCell)e.Item.FindControl("clbm");
                HtmlTableCell cz = (HtmlTableCell)e.Item.FindControl("cz");
                HtmlTableCell gb = (HtmlTableCell)e.Item.FindControl("gb");
                HtmlTableCell fgr = (HtmlTableCell)e.Item.FindControl("fgr");
                HtmlTableCell fgrq = (HtmlTableCell)e.Item.FindControl("fgrq");
                HtmlTableCell cgy = (HtmlTableCell)e.Item.FindControl("cgy");
                HtmlTableCell sqr = (HtmlTableCell)e.Item.FindControl("sqr");
                HtmlTableCell cd = (HtmlTableCell)e.Item.FindControl("cd");
                HtmlTableCell kd = (HtmlTableCell)e.Item.FindControl("kd");
                HtmlTableCell zxr = (HtmlTableCell)e.Item.FindControl("zxr");
                HtmlTableCell zxrq = (HtmlTableCell)e.Item.FindControl("zxrq");
                if (CheckBox1.Checked) { ph.Visible = false; }
                if (CheckBox2.Checked) { jhh.Visible = false; }
                if (CheckBox3.Checked) { xm.Visible = false; }
                if (CheckBox4.Checked) { gc.Visible = false; }
                if (CheckBox5.Checked) { th.Visible = false; }
                if (CheckBox6.Checked) { clbm.Visible = false; }
                if (CheckBox7.Checked) { cz.Visible = false; }
                if (CheckBox8.Checked) { gb.Visible = false; }
                if (CheckBox9.Checked) { fgr.Visible = false; }
                if (CheckBox10.Checked) { fgrq.Visible = false; }
                if (CheckBox11.Checked) { cgy.Visible = false; }
                if (CheckBox12.Checked) { sqr.Visible = false; }
                if (CheckBox13.Checked) { cd.Visible = false; }
                if (CheckBox14.Checked) { kd.Visible = false; }
                if (CheckBox15.Checked) { zxr.Visible = false; }
                if (CheckBox16.Checked) { zxrq.Visible = false; }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableCell ph1 = (HtmlTableCell)e.Item.FindControl("ph1");
                HtmlTableCell xm1 = (HtmlTableCell)e.Item.FindControl("xm1");
                HtmlTableCell gc1 = (HtmlTableCell)e.Item.FindControl("gc1");
                HtmlTableCell jhh1 = (HtmlTableCell)e.Item.FindControl("jhh1");
                HtmlTableCell th1 = (HtmlTableCell)e.Item.FindControl("th1");
                HtmlTableCell clbm1 = (HtmlTableCell)e.Item.FindControl("clbm1");
                HtmlTableCell cz1 = (HtmlTableCell)e.Item.FindControl("cz1");
                HtmlTableCell gb1 = (HtmlTableCell)e.Item.FindControl("gb1");
                HtmlTableCell fgr1 = (HtmlTableCell)e.Item.FindControl("fgr1");
                HtmlTableCell fgrq1 = (HtmlTableCell)e.Item.FindControl("fgrq1");
                HtmlTableCell cgy1 = (HtmlTableCell)e.Item.FindControl("cgy1");
                HtmlTableCell sqr1 = (HtmlTableCell)e.Item.FindControl("sqr1");
                HtmlTableCell cd1 = (HtmlTableCell)e.Item.FindControl("cd1");
                HtmlTableCell kd1 = (HtmlTableCell)e.Item.FindControl("kd1");
                HtmlTableCell zxr1 = (HtmlTableCell)e.Item.FindControl("zxr1");
                HtmlTableCell zxrq1 = (HtmlTableCell)e.Item.FindControl("zxrq1");
                if (CheckBox1.Checked) { ph1.Visible = false; }
                if (CheckBox2.Checked) { jhh1.Visible = false; }
                if (CheckBox3.Checked) { xm1.Visible = false; }
                if (CheckBox4.Checked) { gc1.Visible = false; }
                if (CheckBox5.Checked) { th1.Visible = false; }
                if (CheckBox6.Checked) { clbm1.Visible = false; }
                if (CheckBox7.Checked) { cz1.Visible = false; }
                if (CheckBox8.Checked) { gb1.Visible = false; }
                if (CheckBox9.Checked) { fgr1.Visible = false; }
                if (CheckBox10.Checked) { fgrq1.Visible = false; }
                if (CheckBox11.Checked) { cgy1.Visible = false; }
                if (CheckBox12.Checked) { sqr1.Visible = false; }
                if (CheckBox13.Checked) { cd1.Visible = false; }
                if (CheckBox14.Checked) { kd1.Visible = false; }
                if (CheckBox15.Checked) { zxr1.Visible = false; }
                if (CheckBox16.Checked) { zxrq1.Visible = false; }

                //订单、比价单处理
                #region
                state = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_STATE")).Text;
                //ptcode = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_PTCODE")).Text;
                //if (Convert.ToInt32(state) >= 6)
                //{
                //    bjdsheetno = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PIC_SHEETNO")).Text;
                //    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_BJD")).ForeColor = System.Drawing.Color.Red;
                //    ((HyperLink)e.Item.FindControl("Hypbjd")).NavigateUrl = "TBPC_IQRCMPPRCLST_checked_detail.aspx?sheetno=" + bjdsheetno + "&ptc=" + ptcode + "";
                //}
                //if (Convert.ToInt32(state) >= 7)
                //{
                //    ddsheetno = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PO_SHEETNO")).Text;
                //    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_DD")).ForeColor = System.Drawing.Color.Red;
                //    ((HyperLink)e.Item.FindControl("Hypdd")).NavigateUrl = "PC_TBPC_PurOrder.aspx?orderno=" + ddsheetno + "&ptc=" + ptcode + "";
                //}
                #endregion
                //单击一行变色
                ((HtmlTableRow)e.Item.FindControl("row")).Attributes.Add("onclick", "MouseClick1(this)");
                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "white";
                }
                else
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "#EFF3FB";
                }
                if (Convert.ToInt32(state) == 5)
                {
                    ((HtmlTableCell)e.Item.FindControl("ch")).BgColor = "Red";
                    ((System.Web.UI.WebControls.CheckBox)e.Item.FindControl("CKBOX_SELECT")).Enabled = false;
                }
                string sql = "select * from TBPC_MPTEMPCHANGE where MP_CHPTCODE='" + ptcode + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    ((HtmlTableCell)e.Item.FindControl("ch1")).BgColor = "Green";
                }
                decimal num = Convert.ToDecimal(((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_RPNUM")).Text);
                if (num == 0)
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "#FFFF00";
                }

                string PUR_IFFAST = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_IFFAST")).Text.Trim();
                if (PUR_IFFAST == "1")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lbjiaji")).Visible = true;
                }
            }
        }
        public string get_pur_bjd(string bjdst)
        {
            string statestr = "";
            if (Convert.ToInt32(bjdst) >= 6)
            {
                statestr = "是";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }
        public string get_pur_dd(string ddst)
        {
            string statestr = "";
            if (Convert.ToInt32(ddst) >= 7)
            {
                statestr = "是";
            }
            else
            {

                statestr = "否";

            }
            return statestr;
        }

        protected void btn_cxkc_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/SM_Data/SM_Warehouse_Query.aspx?FLAG=QUERY");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('../SM_Data/SM_Warehouse_Query.aspx?FLAG=QUERY');", true);
            getArticle(false, false);
        }

        protected void btn_hclose_Click(object sender, EventArgs e)
        {
            int j = 0;
            string pcode1 = "";
            string pcode2 = "";
            string shape = "";
            string ptcode = "";
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    j++;
                    pcode1 = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PCODE")).Text;
                    if (j == 1)
                    {
                        pcode2 = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PCODE")).Text;
                        shape = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_MASHAPE")).Text;
                    }
                    if (pcode1 != pcode2)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择同一批次的物料！');", true);
                        getArticle(false, false);
                        return;
                    }
                    else
                    {
                        ptcode = ptcode + ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_ID")).Text + ",";
                    }
                }
            }
            if (j == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择数据！');", true);
                getArticle(false, false);
            }
            else
            {
                ptcode = ptcode.Substring(0, ptcode.LastIndexOf(","));
                Response.Redirect("~/PC_Data/PC_Data_hangclose.aspx?num=2&shape=" + shape + "&orderno=" + pcode2 + "&arry=" + ptcode + "");
            }
        }

        protected void btn_zhuijia_Click(object sender, EventArgs e)//追加比价单
        {
            int j = 0;
            double num = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
            {
                if (((System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT")).Checked)
                {
                    num = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_RPNUM")).Text);
                    if (num == 0)
                    {
                        j++;
                    }
                }
            }
            if (j > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('包含采购数量为0的数据，不能追加比价单！');", true);
                getArticle(false, false);
            }
            else
            {
                int temp = isselected1();
                string ptcode = "";
                if (temp == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
                    getArticle(false, false);
                }
                else if (temp == 2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您选择的数据中包含已经生成比价单的数据,本次操作无效！');", true);
                    getArticle(false, false);
                }
                else if (temp == 0)
                {
                    foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
                    {
                        if (((System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT")).Checked)
                        {
                            ptcode = ptcode + ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_ID")).Text + ",";
                        }
                    }
                    ptcode = ptcode.Substring(0, ptcode.LastIndexOf(",")).ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "mowinopen('" + ptcode + "');", true);
                    getArticle(false, false);
                }
            }

        }
        protected int isselected1()
        {
            string ptc = "";
            string strtext = "";
            int temp = 0;
            int i = 0;//是否选择数据
            int j = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    ptc = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PTCODE")).Text;
                    strtext = "select PIC_PTCODE from TBPC_IQRCMPPRICE where PIC_PTCODE='" + ptc + "'";
                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(strtext);
                    if (dt.Rows.Count > 0)
                    {
                        j++;
                    }
                    i++;
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else if (j > 0)
            {
                temp = 2;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }
        private int ifselect()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
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
        //导出
        protected void btn_daochu_Click(object sender, EventArgs e)
        {
            int temp = ifselect();
            if (temp == 0)
            {
                string ordercode = "";
                foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx.Checked)
                    {
                        ordercode += "'" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PTCODE")).Text.ToString() + "'" + ",";
                    }
                }
                ordercode = ordercode.Substring(0, ordercode.LastIndexOf(",")).ToString();
                string sqltext = "";
                sqltext = "SELECT planno,pjnm,engnm,ptcode,PUR_TUHAO,marid,marnm," +
                             "margg ,marcz,margb,PUR_MASHAPE,length,width,rpnum,marunit,rpfznum,marfzunit, " +
                            "cgrnm,purnote,PUR_IFFAST as 'IFFAST' " +
                            "FROM  View_TBPC_PURCHASEPLAN_IRQ_ORDER where  ptcode in (" + ordercode + ") order by planno desc,ptcode asc";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                ExportDataItem(dt);
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "alert('请选择要导出的采购计划！！！');", true);
                getArticle(false, false);
            }
        }
        private void ExportDataItem(System.Data.DataTable dt)
        {
            string filename = "采购计划明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("采购计划明细.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                string Fast = string.Empty;

                #region 写入数据

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2);
                    Fast = dt.Rows[i]["IFFAST"].ToString() == "1" ? " 加急" : "";

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                    }

                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1) + Fast);//序号
                    row.CreateCell(1).SetCellValue("'" + dt.Rows[i]["planno"].ToString());//批号
                    row.CreateCell(2).SetCellValue("'" + dt.Rows[i]["pjnm"].ToString());//项目
                    row.CreateCell(3).SetCellValue("'" + dt.Rows[i]["engnm"].ToString());//工程
                    row.CreateCell(4).SetCellValue("'" + dt.Rows[i]["ptcode"].ToString());//计划号
                    row.CreateCell(5).SetCellValue("'" + dt.Rows[i]["PUR_TUHAO"].ToString());//图号
                    row.CreateCell(6).SetCellValue("'" + dt.Rows[i]["marid"].ToString());//物料编码
                    row.CreateCell(7).SetCellValue("'" + dt.Rows[i]["marnm"].ToString());//名称
                    row.CreateCell(8).SetCellValue("'" + dt.Rows[i]["margg"].ToString());//规格
                    row.CreateCell(9).SetCellValue("'" + dt.Rows[i]["marcz"].ToString());//材质
                    row.CreateCell(10).SetCellValue("'" + dt.Rows[i]["margb"].ToString());//国标
                    row.CreateCell(11).SetCellValue("'" + dt.Rows[i]["PUR_MASHAPE"].ToString());//类型
                    row.CreateCell(12).SetCellValue("'" + dt.Rows[i]["length"].ToString());//长度
                    row.CreateCell(13).SetCellValue("'" + dt.Rows[i]["width"].ToString());//宽度
                    row.CreateCell(14).SetCellValue(dt.Rows[i]["rpnum"].ToString());//数量
                    row.CreateCell(15).SetCellValue("'" + dt.Rows[i]["marunit"].ToString());//单位
                    row.CreateCell(16).SetCellValue(dt.Rows[i]["rpfznum"].ToString());//辅助数量
                    row.CreateCell(17).SetCellValue("'" + dt.Rows[i]["marfzunit"].ToString());//辅助单位
                    row.CreateCell(18).SetCellValue(dt.Rows[i]["cgrnm"].ToString());//采购人
                    row.CreateCell(19).SetCellValue(dt.Rows[i]["purnote"].ToString());//金额
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
        //protected void Tb_PJNAME_Textchanged(object sender, EventArgs e)
        //{
        //    string Cname = "";
        //    if (Tb_PJNAME.Text.ToString().Contains("|"))
        //    {
        //        Cname = Tb_PJNAME.Text.Substring(0, Tb_PJNAME.Text.ToString().IndexOf("|"));
        //        Tb_PJNAME.Text = Cname.Trim();
        //    }
        //    else if (Tb_PJNAME.Text == "")
        //    {

        //    }
        //}
        protected void Tb_ENGNAME_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (Tb_ENGNAME.Text.ToString().Contains("|"))
            {
                Cname = Tb_ENGNAME.Text.Substring(0, Tb_ENGNAME.Text.ToString().IndexOf("|"));
                Tb_ENGNAME.Text = Cname.Trim();
            }
            else if (Tb_ENGNAME.Text == "")
            {

            }
        }

        //重置条件
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;
            CheckBox4.Checked = false;
            CheckBox5.Checked = false;
            CheckBox6.Checked = false;
            CheckBox7.Checked = false;
            CheckBox8.Checked = false;
            CheckBox9.Checked = false;
            CheckBox10.Checked = false;
            CheckBox11.Checked = false;
            CheckBox12.Checked = false;
            CheckBox13.Checked = false;
            CheckBox14.Checked = false;
            CheckBox15.Checked = false;
            CheckBox16.Checked = false;
        }

        //取消
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }

        protected void QueryButton_Click(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                addQueryItem(PageName, "CheckBox1", CheckBox1.GetType().ToString(), "1");
            }
            if (CheckBox2.Checked)
            {
                addQueryItem(PageName, "CheckBox2", CheckBox2.GetType().ToString(), "1");
            }
            if (CheckBox3.Checked)
            {
                addQueryItem(PageName, "CheckBox3", CheckBox3.GetType().ToString(), "1");
            }
            if (CheckBox4.Checked)
            {
                addQueryItem(PageName, "CheckBox4", CheckBox4.GetType().ToString(), "1");
            }
            if (CheckBox5.Checked)
            {
                addQueryItem(PageName, "CheckBox5", CheckBox5.GetType().ToString(), "1");
            }
            if (CheckBox6.Checked)
            {
                addQueryItem(PageName, "CheckBox6", CheckBox6.GetType().ToString(), "1");
            }
            if (CheckBox7.Checked)
            {
                addQueryItem(PageName, "CheckBox7", CheckBox7.GetType().ToString(), "1");
            }
            if (CheckBox8.Checked)
            {
                addQueryItem(PageName, "CheckBox8", CheckBox8.GetType().ToString(), "1");
            }
            if (CheckBox9.Checked)
            {
                addQueryItem(PageName, "CheckBox9", CheckBox9.GetType().ToString(), "1");
            }
            if (CheckBox10.Checked)
            {
                addQueryItem(PageName, "CheckBox10", CheckBox10.GetType().ToString(), "1");
            }
            if (CheckBox11.Checked)
            {
                addQueryItem(PageName, "CheckBox11", CheckBox11.GetType().ToString(), "1");
            }
            if (CheckBox12.Checked)
            {
                addQueryItem(PageName, "CheckBox12", CheckBox12.GetType().ToString(), "1");
            }
            if (CheckBox13.Checked)
            {
                addQueryItem(PageName, "CheckBox13", CheckBox13.GetType().ToString(), "1");
            }
            if (CheckBox14.Checked)
            {
                addQueryItem(PageName, "CheckBox14", CheckBox14.GetType().ToString(), "1");
            }
            if (CheckBox15.Checked)
            {
                addQueryItem(PageName, "CheckBox15", CheckBox15.GetType().ToString(), "1");
            }
            if (CheckBox16.Checked)
            {
                addQueryItem(PageName, "CheckBox16", CheckBox16.GetType().ToString(), "1");
            }

            getArticle(true, true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('设置成功！');", true);
            ModalPopupExtenderSearch.Hide();
        }

        private void Action_Click()
        {
            //找到选定行的参数
            string mid = "";
            string gg = "";
            string gb = "";
            string ptc = "";
            bool check = false; ;

            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
            {
                if (((System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT")).Checked)
                {
                    check = true;
                    mid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_MARID")).Text;
                    gg = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_MARNORM")).Text;
                    gb = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_GUOBIAO")).Text;
                    ptc = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PTCODE")).Text;
                }
            }

            //判断是否选择了数据行
            if (!check)//没有选择行，且不是添加和导出合同操作
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请勾选要操作的数据行！');", true);
                getArticle(false, false);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "Add_HT('" + gg + "','" + gb + "','" + ptc + "','" + mid + "');", true);
                getArticle(false, false);
            }
        }

        //招标物料
        protected void btn_zb_Click(object sender, EventArgs e)
        {
            Action_Click();
        }

        protected void CancelFenG_Click(object sender, EventArgs e)
        {
            int temp = canfanshen();
            string sqltext = "";
            string state = "";
            string ptcode = "";
            string ptcodestr = "";
            if(tbreason.Text.Trim()=="")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写理由！');", true);
                return;
            }
            string quixaoreason = "理由：" + tbreason.Text.Trim() + "；取消人：" + Session["UserName"].ToString().Trim() + "";
            List<string> sqltextlist = new List<string>();
            if (temp == 0)
            {
                foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        if (cbx.Checked)
                        {
                            state = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_STATE")).Text;
                            ptcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PTCODE")).Text;
                            if (state == "4")
                            {
                                sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_CGMAN='',PUR_STATE='3',PUR_PTASMAN='',PUR_PTASTIME='',PUR_QUXIAOREASON='" + quixaoreason + "'  WHERE PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";
                                sqltextlist.Add(sqltext);
                                //DBCallCommon.ExeSqlText(sqltext);
                            }
                            ptcodestr += "," + ptcode;
                        }
                    }
                }
                if (ptcodestr != "")
                {
                    string perid = "7";
                    System.Data.DataTable perdt = DBCallCommon.GetDTUsingSqlText("select * from TBDS_STAFFINFO where ST_POSITION='0501' and ST_PD='0'");
                    if (perdt.Rows.Count > 0)
                    {
                        perid = perdt.Rows[0]["ST_ID"].ToString();
                    }
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(perid), new List<string>(), new List<string>(), "分工取消信息", "计划跟踪号：" + ptcodestr.Substring(1) + "  被 分工人 " + Session["UserName"].ToString() + "取消，请查看");
                }
                DBCallCommon.ExecuteTrans(sqltextlist);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('取消分工成功！');", true);
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('采购员不是登录用户，你无权修改！');", true);
            }
            else if (temp == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择了未分工的数据，操作失败！');", true);
            }
            else if (temp == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('任务已执行，操作失败！');", true);
            }
            getArticle(false, false);
            ModalPopupExtender1.Hide();
        }

        private int canfanshen()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            int j = 0;//分工人是否为登录用户
            int k = 0;//选择的数据中是否包含未分工数据
            int l = 0;//选择的数据是否包含已执行数据（询比价、代用）
            int state = 0;//状态
            string postid = "";
            string userid = Session["UserID"].ToString();
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        state = Convert.ToInt32(((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_STATE")).Text);
                        postid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_CGMAN")).Text;
                        if (postid != userid)
                        {
                            j++;
                            break;
                        }
                        else if (state <= 3)//未分工
                        {
                            k++;
                            break;
                        }
                        else if (state > 4)//询比价or代用
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
            else if (j > 0)
            {
                temp = 2;
            }
            else if (k > 0)//选择的数据中包含未分工数据
            {
                temp = 3;
            }
            else if (l > 0)//选择的数据中包含询比价or代用的数据
            {
                temp = 4;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }
        //取消
        protected void btnquixao_Click(object sender, EventArgs e)
        {
            ModalPopupExtender1.Hide();
            getArticle(false, false);
        }
    }
}
