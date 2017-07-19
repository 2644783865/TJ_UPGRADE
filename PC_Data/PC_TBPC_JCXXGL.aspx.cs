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
using System.IO;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;


namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_JCXXGL : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();

        ////页数
        public int ObjPageSize
        {
            get
            {
                //默认是升序
                ViewState["ObjPageSize"] = Convert.ToDouble(DropDownList1.SelectedValue);
                return Convert.ToInt32(ViewState["ObjPageSize"]);
            }
            set
            {
                ViewState["ObjPageSize"] = value;
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
        public string gptcode
        {
            get
            {
                object str = ViewState["gptcode"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gptcode"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = QueryButton.UniqueID;  //2013年5月24日 08:23:02  Meng
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            btn_delete.Attributes.Add("OnClick", "Javascript:return confirm('将删除整个订单，是否确定删除?');");
            ViewState["ObjPageSize"] = Convert.ToDouble(DropDownList1.SelectedValue);
            if (!IsPostBack)
            {
                hid_filter.Text = "View_TBPC_PURORDERDETAIL_PLAN_TOTAL" + "/" + Session["UserID"].ToString();
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
                initpower1();
                initpage1();
                //检查是否从合同上查订单
                if (Request.QueryString["TotalOrder"] != null)
                {
                    string orderlist = Request.QueryString["TotalOrder"].ToString();

                    tb_orderno.Text = orderlist;
                    rad_quanbu.Checked = true;
                    rad_mypart.Checked = false;//显示全部单据

                    rad_all.Checked = true;//显示全部
                    rad_weitijiao.Checked = false;
                    rad_yiguanbi.Checked = false;
                    rad_weidaohuo.Checked = false;
                    rad_bfdaohuo.Checked = false;
                    rad_yidaohuo.Checked = false;
                }
                //从入库单上查

                if (Request.QueryString["action"] != null)
                {
                    if (Request.QueryString["action"] == "SearchUpOrDown")
                    {
                        string ptc = Request.QueryString["id"].ToString();

                        tb_ptc.Text = ptc;
                        rad_quanbu.Checked = true;
                        rad_mypart.Checked = false;//显示全部单据

                        rad_all.Checked = true;//显示全部
                        rad_weitijiao.Checked = false;
                        rad_yiguanbi.Checked = false;
                        rad_weidaohuo.Checked = false;
                        rad_bfdaohuo.Checked = false;
                        rad_yidaohuo.Checked = false;
                    }
                }
                getArticle(true);
            }
        }
        private void initpage1()
        {
            string sqltext = "";
            sqltext = "select ST_NAME,ST_ID from TBDS_STAFFINFO WHERE ST_DEPID='05' and ST_PD='0'";
            string dataText = "ST_NAME";
            string dataValue = "ST_ID";
            DBCallCommon.BindDdl(drp_stu, sqltext, dataText, dataValue);
            drp_stu.SelectedIndex = 0;
        }

        private void initpower1()
        {
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
                rad_quanbu.Checked = true; rad_weitijiao.Checked = false; rad_yiguanbi.Checked = false;
                rad_weidaohuo.Checked = false; rad_bfdaohuo.Checked = false; rad_yidaohuo.Checked = false;
            }
            else if (NUM2 == "2")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = true; rad_yiguanbi.Checked = false;
                rad_weidaohuo.Checked = false; rad_bfdaohuo.Checked = false; rad_yidaohuo.Checked = false;
            }
            else if (NUM2 == "4")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = false; rad_yiguanbi.Checked = true;
                rad_weidaohuo.Checked = false; rad_bfdaohuo.Checked = false; rad_yidaohuo.Checked = false;
            }
            else if (NUM2 == "5")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = false; rad_yiguanbi.Checked = false;
                rad_weidaohuo.Checked = true; rad_bfdaohuo.Checked = false; rad_yidaohuo.Checked = false;
            }
            else if (NUM2 == "6")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = false; rad_yiguanbi.Checked = false;
                rad_weidaohuo.Checked = false; rad_bfdaohuo.Checked = true; rad_yidaohuo.Checked = false;
            }
            else if (NUM2 == "7")
            {
                rad_quanbu.Checked = false; rad_weitijiao.Checked = false; rad_yiguanbi.Checked = false;
                rad_weidaohuo.Checked = false; rad_bfdaohuo.Checked = false; rad_yidaohuo.Checked = true;
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

            CommonFun.Paging(dt, Purordertotal_list_Repeater, UCPaging1, NoDataPane);
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

        private void getArticle(bool isFirstPage)      //取得Article数据
        {
            CreateDataSource();

            string TableName = "(select t.*,s.ICL_TYPE from ((select a.*,b.RESULT from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a left join (select PTC,RESULT,ISAGAIN,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc) as rn from View_TBQM_APLYFORITEM) as c where rn<=1) as b on a.ptcode=b.PTC)t left join (select * from TBPC_IQRCMPPRCRVW as m left join TBPC_IQRCMPPRICE as n on m.ICL_SHEETNO=n.PIC_SHEETNO)s on t.ptcode=s.PIC_PTCODE) where s.PIC_PTCODE is not null)q";

            string PrimaryKey = "";

            string ShowFields = "orderno,supplierid,suppliernm,zdtime,cgtimerq,pjid,pjnm,engid,engnm,whstate,convert(float,zxnum) as zxnum,recgdnum,detailnote,detailstate,detailcstate,marunit,convert(float,ctamount) as ctamount," +
                         "zdrid,zdrnm,shrid,shrnm,totalnote,totalstate,totalcstate,ptcode,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,marid,marnm,margg,marcz,margb,PO_MASHAPE,length,width,PO_ZJE,RESULT as PO_CGFS,ctprice,recdate,PO_MAP,PO_TECUNIT,fznum,PO_CHILDENGNAME ";

            //数据库中的主键
            //string OrderField = "orderno desc,marnm,margg,ptcode";
            string OrderField = "zdtime desc,ptcode,marnm,margg";

            string GroupField = "";

            int OrderType = 0;
            /**/
            string StrWhere = ViewState["sqlwhere"].ToString();

            int PageSize = ObjPageSize;

            BindPage(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFirstPage);
        }
        public void CreateDataSource()
        {
            string sqlwhere = "";
            string sqltext = "";
            string tableuser = hid_filter.Text;
            string filter = "";
            string nowtime = System.DateTime.Now.ToString("yyyy-MM-dd");

            sqltext = "SELECT tableuser, filter FROM TBPC_FILTER_INFO where tableuser='" + tableuser + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                filter = dr[1].ToString();
            }
            dr.Close();

            sqltext = "SELECT orderno,supplierid,suppliernm,zdtime,cgtimerq,pjid,pjnm,engid,engnm,whstate,convert(float,zxnum) as zxnum,recgdnum,detailnote,detailstate,detailcstate,marunit,convert(float,ctamount) as ctamount," +
                         "zdrid,zdrnm,shrid,shrnm,totalnote,totalstate,totalcstate,ptcode,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,marid,marnm,margg,marcz,margb,PO_MASHAPE,length,width,PO_ZJE,PO_CGFS,ctprice,recdate  " +
                         " FROM (select t.*,s.ICL_TYPE from ((select a.*,b.RESULT from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a left join (select PTC,RESULT,ISAGAIN,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc) as rn from View_TBQM_APLYFORITEM) as c where rn<=1) as b on a.ptcode=b.PTC)t left join (select * from TBPC_IQRCMPPRCRVW as m left join TBPC_IQRCMPPRICE as n on m.ICL_SHEETNO=n.PIC_SHEETNO)s on t.ptcode=s.PIC_PTCODE) where s.PIC_PTCODE is not null)q  where ";

            sqlwhere = "1=1 ";
            if (filter != "")
            {
                sqlwhere = sqlwhere + " and " + filter + "";
            }
            if (tb_ptc.Text.Trim() != "")
            {
                sqlwhere = sqlwhere + " and ptcode like '%" + tb_ptc.Text.Trim() + "%'";
            }
            if (tb_gg.Text.Trim() != "")
            {
                sqlwhere = sqlwhere + " and margg like '%" + tb_gg.Text.Trim() + "%'";
            }
            if (rad_mypart.Checked)
            {
                sqlwhere = sqlwhere + " and zdrid='" + Session["UserID"].ToString() + "'";
            }

            if (rad_jicai.Checked)
            {
                sqlwhere = sqlwhere + " and ICL_TYPE='1'";
            }
            if (rad_fjicai.Checked)
            {
                sqlwhere = sqlwhere + " and ICL_TYPE='0'";
            }

            if (rad_quanbu.Checked)
            {
                sqlwhere = sqlwhere + " and detailcstate='0'";
            }
            if (rad_weitijiao.Checked)
            {
                sqlwhere = sqlwhere + " and totalstate='0' and totalcstate='0' and detailcstate='0'";
            }
            if (rad_yiguanbi.Checked)
            {
                sqlwhere = sqlwhere + " and detailcstate='1'";
            }
            if (rad_weidaohuo.Checked)//未到货
            {
                sqlwhere = sqlwhere + " and detailstate='1' and recgdnum=0 and detailcstate='0'";
            }
            if (rad_yuqi_n.Checked)//逾期未到货
            {
                sqlwhere = sqlwhere + " and detailstate='1' and recgdnum=0 and detailcstate='0' and cgtimerq<'" + nowtime + "'";
            }
            if (rad_bfdaohuo.Checked)
            {
                sqlwhere = sqlwhere + " and detailstate='1' and recgdnum<zxnum and recgdnum>0 and detailcstate='0'";
            }
            if (rad_yidaohuo.Checked)//已到货
            {
                sqlwhere = sqlwhere + " and (detailstate='3' or detailstate='2') and detailcstate='0'";
            }
            if (rad_yuqi_y.Checked)//逾期到货
            {
                sqlwhere = sqlwhere + " and (detailstate='3' or detailstate='2') and detailcstate='0' and recdate>cgtimerq";
            }
            if (tb_supply.Text != "")
            {
                sqlwhere = sqlwhere + " and suppliernm like '%" + tb_supply.Text.Trim() + "%'";
            }
            if (tb_StartTime.Text != "")
            {
                sqlwhere = sqlwhere + " and zdtime>='" + tb_StartTime.Text.Trim() + "'";
            }
            if (tb_EndTime.Text != "")
            {
                string enddate = tb_EndTime.Text.ToString() == "" ? "2100-01-01" : tb_EndTime.Text.ToString();
                enddate = enddate + " 23:59:59";
                sqlwhere = sqlwhere + " and zdtime<='" + enddate + "'";
            }

            if (tb_orderno.Text != "")
            {
                //如果是从合同反查订单，可能有多个订单，筛选条件要用in 
                if (Request.QueryString["TotalOrder"] != null)
                {
                    string str_orderid = "'" + Request.QueryString["TotalOrder"].ToString().Replace(",", "','") + "'";//将订单号重组
                    sqlwhere = sqlwhere + " and orderno in (" + str_orderid + ")";
                }
                else
                {
                    sqlwhere = sqlwhere + " and orderno like '%" + tb_orderno.Text.Trim() + "'";
                }
            }
            if (tb_name.Text != "")
            {
                sqlwhere = sqlwhere + " and marnm like '%" + tb_name.Text.Trim() + "%'";
            }
            if (tb_cz.Text != "")
            {
                sqlwhere = sqlwhere + " and marcz like '%" + tb_cz.Text.Trim() + "%'";
            }
            if (tb_gb.Text != "")
            {
                sqlwhere = sqlwhere + " and margb like '%" + tb_gb.Text.Trim() + "%'";
            }
            if (tb_pj.Text != "")
            {
                sqlwhere = sqlwhere + " and pjnm like '%" + tb_pj.Text.Trim() + "%'";
            }
            if (tb_eng.Text != "")
            {
                sqlwhere = sqlwhere + " and engnm like '%" + tb_eng.Text.Trim() + "%'";
            }
            if (tb_th.Text != "")
            {
                sqlwhere = sqlwhere + " and PO_TUHAO like '%" + tb_th.Text.Trim() + "%'";
            }
            if (drp_stu.SelectedIndex != 0)
            {
                sqlwhere = sqlwhere + " and zdrnm like '%" + drp_stu.SelectedItem.Text + "%'";
            }
            if (DropDownList2.SelectedIndex != 0)
            {
                if (DropDownList2.SelectedItem.Text.Trim() == "其他")
                {
                    sqlwhere = sqlwhere + " and (PO_MASHAPE like '%其他%' or PO_MASHAPE like '%其它%')";
                }
                else
                {
                    sqlwhere = sqlwhere + " and PO_MASHAPE='" + DropDownList2.SelectedItem.Text.Trim() + "'";
                }
            }
            if (DropDownList3.SelectedIndex != 0)
            {
                if (DropDownList3.SelectedItem.Text == "未报检")
                {
                    sqlwhere = sqlwhere + " and PO_CGFS= '——'";
                }
                else
                {
                    sqlwhere = sqlwhere + " and PO_CGFS = '" + DropDownList3.SelectedItem.Text + "'";
                }
            }
            if (DropDownList4.SelectedIndex != 0)
            {
                if (DropDownList4.SelectedItem.Text == "已添加")
                {
                    sqlwhere = sqlwhere + " and ((SELECT count(PCON_ORDERID) FROM TBPM_CONPCHSINFO  WHERE (PCON_ORDERID LIKE '%'+orderno+'%')) !=0)";
                }
                else if (DropDownList4.SelectedItem.Text == "未添加")
                {
                    sqlwhere = sqlwhere + " and ((SELECT count(PCON_ORDERID) FROM TBPM_CONPCHSINFO  WHERE (PCON_ORDERID LIKE '%'+orderno+'%')) =0)";
                }
            }




            sqltext = sqltext + sqlwhere + " order by orderno desc,ptcode asc";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            double tot_money = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string cta = dt.Rows[i]["ctamount"].ToString();
                cta = cta == "" ? "0" : cta;
                tot_money += Convert.ToDouble(cta);
            }
            lb_select_num.Text = Convert.ToString(dt.Rows.Count);
            lb_select_money.Text = string.Format("{0:c}", tot_money);
            ViewState["sqlwhere"] = sqlwhere;
        }

        protected void rad_all_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void rad_mypart_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void rad_quanbu_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void rad_weitijiao_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void rad_yiguanbi_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void rad_weidaohuo_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void rad_bfdaohuo_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void rad_yidaohuo_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void rad_yuqi_n_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void rad_yuqi_y_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void rad_suoyou_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void rad_jicai_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void rad_fjicai_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        //protected void rad_yizhongjie_CheckedChanged(object sender, EventArgs e)
        //{
        //    lb_CurrentPage.Text = "1";
        //    getArticle(CreateDataSource());
        //}

        /// <summary>
        /// 报检！！！！！！！！
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_baojian_Click(object sender, EventArgs e)
        {

            //一个订单有多个项目
            string supplier = string.Empty;
            string pjnm = string.Empty;
            List<string> sqllist = new List<string>();

            string sql = "update TBPC_PURORDERDETAIL set PO_OperateState=NULL WHERE PO_OperateState='PUSH" + Session["UserID"].ToString() + "'";

            sqllist.Add(sql);

            for (int i = 0; i < Purordertotal_list_Repeater.Items.Count; i++)
            {
                if ((Purordertotal_list_Repeater.Items[i].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptc = (Purordertotal_list_Repeater.Items[i].FindControl("ptcode") as System.Web.UI.WebControls.Label).Text;
                    supplier += (Purordertotal_list_Repeater.Items[i].FindControl("PO_SUPPLIERID") as System.Web.UI.WebControls.Label).Text + '_';
                    pjnm += (Purordertotal_list_Repeater.Items[i].FindControl("pjnm") as System.Web.UI.WebControls.Label).Text + '_';
                    sql = "update TBPC_PURORDERDETAIL set PO_OperateState='PUSH" + Session["UserID"].ToString() + "' WHERE PO_PCODE='" + ptc + "'";
                    sqllist.Add(sql);
                }

            }
            if (sqllist.Count > 1)
            {
                string Supplybegin = supplier.Substring(0, supplier.IndexOf("_") + 1);
                string Supplyend = supplier.Replace(Supplybegin, "");
                if (Supplyend != "")
                {
                    //getArticle(true); //刷新
                    string script = @"alert('请选择来自相同供货商的条目进行报检!');";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", script, true);
                    getArticle(false);
                    return;
                }
                string PJbegin = pjnm.Substring(0, pjnm.IndexOf("_") + 1);
                string PJend = pjnm.Replace(PJbegin, "");
                if (PJend != "")
                {
                    //getArticle(true); //刷新
                    string script = @"alert('请选择来自相同项目的条目进行报检!');";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", script, true);
                    getArticle(false);
                    return;
                }
                DBCallCommon.ExecuteTrans(sqllist);
                //Response.Redirect("~/QC_Data/QC_Inspection_Add.aspx?ACTION=NEW", false);
                //System.Web.UI.WebControls.CheckBox CKBOX_SELECT.Items.clear();
                //getArticle(true);  //报检后刷新
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('/QC_Data/QC_Inspection_Add.aspx?ACTION=NEW&TYPE=PUR');", true);
                getArticle(false);
            }
            else
            {
                //getArticle(true); //刷新
                string script = @"alert('请选择需要添加的条目!');";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", script, true);
                getArticle(false);
            }

        }
        private double zxnum = 0;
        private double dnum = 0;
        private string state = "";
        private string date = "";
        private string baojian = "";
        List<string> list = new List<string>();
        protected void Purordertotal_list_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string zlbjjg = ((System.Web.UI.WebControls.Label)e.Item.FindControl("zlbj")).Text;//报检信息
                state = ((System.Web.UI.WebControls.Label)e.Item.FindControl("detailstate")).Text;
                zxnum = Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("zxnum")).Text);
                dnum = Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("recgdnum")).Text);
                date = ((System.Web.UI.WebControls.Label)e.Item.FindControl("cgtimerq")).Text == "" ? "2100-01-01" : ((System.Web.UI.WebControls.Label)e.Item.FindControl("cgtimerq")).Text;
                baojian = ((System.Web.UI.WebControls.Label)e.Item.FindControl("zlbj")).Text;
                string datime = DateTime.Now.ToString("yyyy-MM-dd");
                if (state == "2")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).Text = "已入库";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).Text = "已到货";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).ForeColor = System.Drawing.Color.Red;
                }
                else if (state == "3")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).Text = "已到货";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).Text = "未入库";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).ForeColor = System.Drawing.Color.Red;
                }
                else if (state == "1")
                {
                    if (dnum > 0 && dnum < zxnum)
                    {
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).Text = "部分入库";
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).Text = "部分到货";
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).ForeColor = System.Drawing.Color.Red;
                    }
                    else if (dnum == 0)
                    {
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).Text = "未到货";
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).ForeColor = System.Drawing.Color.Red;
                        if (zlbjjg != "未报检")
                        {
                            ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).Text = "已到货";
                        }
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).Text = "未入库";
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).ForeColor = System.Drawing.Color.Red;
                        HtmlTableCell td2 = (HtmlTableCell)e.Item.FindControl("td2");
                        if (Convert.ToDateTime(date) < Convert.ToDateTime(datime))
                        {
                            td2.BgColor = "Red";
                        }
                    }
                }
                string code = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PO_CODE")).Text;
                //查看合同信息
                //string sqltext = "select PCON_ORDERID,PCON_BCODE from TBPM_CONPCHSINFO where PCON_ORDERID like'%" + code + "%'";10月27日改
                string sqltext = "select HT_ID,HT_XFHTBH,HT_DDBH from PC_CGHT where HT_DDBH like '%" + code + "%'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    HtmlTableCell tdHT = (HtmlTableCell)e.Item.FindControl("tdHT");
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("hetonghao")).Text = dt.Rows[0]["HT_XFHTBH"].ToString();
                    tdHT.BgColor = "Green";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("hetonghao")).ForeColor = System.Drawing.Color.Red;
                    ((HyperLink)e.Item.FindControl("Hyphth")).NavigateUrl = "~/PC_Data/PC_CGHT.aspx?action=read&id=" + dt.Rows[0]["HT_ID"].ToString() + "";
                }
                else
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("hetonghao")).Text = "未添加";
                    ((HyperLink)e.Item.FindControl("Hyphth")).NavigateUrl = "~/PC_Data/PC_CGHT.aspx?action=add";
                }

                //2013年4月17日10:01:10  废弃
                //查看请款信息
                //string sqlqk = "select DQ_ID FROM TBPM_ORDER_CHECKREQUEST WHERE DQ_DDCODE LIKE '%" + code + "%'";
                //System.Data.DataTable qkdt = DBCallCommon.GetDTUsingSqlText(sqlqk);
                //if (qkdt.Rows.Count > 0)
                //{
                //    ((System.Web.UI.WebControls.Label)e.Item.FindControl("ddqkcode")).Text = qkdt.Rows[0]["DQ_ID"].ToString();
                //    ((System.Web.UI.WebControls.Label)e.Item.FindControl("ddqkcode")).ForeColor = System.Drawing.Color.Red;
                //    ((HyperLink)e.Item.FindControl("HypDDQK")).NavigateUrl = "~/Contract_Data/CR_NotContractOrder_Add.aspx?Action=View&DDQKCode=" + qkdt.Rows[0]["DQ_ID"].ToString() + "";
                //}
                //else
                //{
                //    ((System.Web.UI.WebControls.Label)e.Item.FindControl("ddqkcode")).Text = "未添加";
                //}

                //查看报检信息
                if (zlbjjg != "未报检")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("zlbj")).ForeColor = System.Drawing.Color.Red;
                    string ptcode = ((System.Web.UI.WebControls.Label)e.Item.FindControl("ptcode")).Text;
                    string sql = "select AFI_ID from TBQM_APLYFORINSPCT  where UNIQUEID=(select top 1 UNIQUEID from  TBQM_APLYFORITEM where PTC='" + ptcode + "' order by id desc)";
                    System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt1.Rows.Count > 0)
                    {
                        string afiid = dt1.Rows[0]["AFI_ID"].ToString();
                        ((HyperLink)e.Item.FindControl("Hypzlbj")).NavigateUrl = "~/QC_Data/QC_Inspection_Add.aspx?ACTION=UPDATE&NUM=1&ID=" + afiid + "";
                    }
                }

                System.Web.UI.WebControls.Label lb1 = (System.Web.UI.WebControls.Label)e.Item.FindControl("PO_CODE");
                System.Web.UI.WebControls.Label lb2 = (System.Web.UI.WebControls.Label)e.Item.FindControl("PO_ZDNM");
                System.Web.UI.WebControls.Label lb3 = (System.Web.UI.WebControls.Label)e.Item.FindControl("PO_SHTIME");
                System.Web.UI.WebControls.Label lb4 = (System.Web.UI.WebControls.Label)e.Item.FindControl("zje");
                System.Web.UI.WebControls.Label lb5 = (System.Web.UI.WebControls.Label)e.Item.FindControl("PO_SUPPLIERNM");
                if (list.Count == 0)
                {
                    list.Add(lb1.Text);
                }
                else
                {
                    if (list.Contains(lb1.Text))
                    {
                        lb1.Visible = false;
                        lb2.Visible = false;
                        lb3.Visible = false;
                        lb4.Visible = false;
                        lb5.Visible = false;
                    }
                    else
                    {
                        list.Add(lb1.Text);
                    }
                }

                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "white";
                }
                else
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "#EFF3FB";
                }
            }
            if (e.Item.ItemType == ListItemType.Header)
            {

            }
        }
        public string get_ordlistsh_state(string i)
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
        public string get_zlbj(string i)
        {
            string statestr = "";
            if (i == "")
            {
                statestr = "未报检";
            }
            else
            {
                statestr = i;
            }
            return statestr;
        }
        public string get_ordlistgb_state(string i)
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

        protected void btn_delete_Click(object sender, EventArgs e)
        {
            int temp = candelete();
            if (temp == 0)
            {
                foreach (RepeaterItem Retem in Purordertotal_list_Repeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbk = Retem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                    if (cbk.Checked)
                    {
                        string code = ((System.Web.UI.WebControls.Label)Retem.FindControl("PO_CODE")).Text;
                        string BZcode = code.Substring(0, 4).ToString();
                        if (BZcode == "PORD")
                        {
                            string sql = "select PO_PTCODE from TBPC_PURORDERDETAIL where PO_CODE='" + code + "'";
                            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string ptcode = dt.Rows[i]["PO_PTCODE"].ToString();
                                string sql1 = "update TBPC_PURCHASEPLAN set PUR_STATE='6' where PUR_PTCODE='" + ptcode + "'";
                                DBCallCommon.ExeSqlText(sql1);
                                string sql2 = "update TBPC_IQRCMPPRICE set PIC_STATE='0' where PIC_PTCODE='" + ptcode + "'";
                                DBCallCommon.ExeSqlText(sql2);
                            }
                        }
                        else if (BZcode == "ZBPO")
                        {
                            string sql = "select PO_PTCODE from TBPC_PURORDERDETAIL where PO_CODE='" + code + "'";
                            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string ptcode = dt.Rows[i]["PO_PTCODE"].ToString();
                                string sql1 = "update TBPC_PURCHASEPLAN set PUR_STATE='4' where PUR_PTCODE='" + ptcode + "'";
                                DBCallCommon.ExeSqlText(sql1);
                            }
                        }
                        string sqltext = "delete from TBPC_PURORDERTOTAL where PO_CODE='" + code + "'";
                        string sqltext1 = "delete from TBPC_PURORDERDETAIL where PO_CODE='" + code + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                        DBCallCommon.ExeSqlText(sqltext1);

                    }
                }
                getArticle(true);
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
                getArticle(false);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您不是制单人，无权删除！');", true);
                getArticle(false);
            }
            else if (temp == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('在审核中或审核通过，不能删除！');", true);
                getArticle(false);
            }
        }

        //判断能否删除，需要注意
        private int candelete()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            int j = 0;//制单是否为登录用户
            int k = 0;
            string state = "";//状态
            string postid = "";
            string userid = Session["UserID"].ToString();
            foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        state = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PO_STATE")).Text.ToString();
                        postid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PO_ZDID")).Text;
                        if (postid != userid)//登录人不是制单人
                        {
                            j++;
                            break;
                        }
                        else if (state == "1" || state == "2" || state == "3")//审核通过或审核中
                        {
                            k++;
                            break;
                        }

                    }
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else if (j > 0)//登录人不是制单人
            {
                temp = 2;
            }
            else if (k > 0)//选择的数据中包含审核通过内容
            {
                temp = 3;
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
                string code = "";
                foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx.Checked)
                    {
                        if (ordercode == "" || code != ((System.Web.UI.WebControls.Label)Reitem.FindControl("PO_CODE")).Text.ToString())
                        {
                            ordercode += ((System.Web.UI.WebControls.Label)Reitem.FindControl("PO_CODE")).Text.ToString() + "/";
                        }
                        code = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PO_CODE")).Text.ToString();
                    }
                }
                ordercode = ordercode.Replace("/", "','");
                ordercode = ordercode.Substring(0, ordercode.LastIndexOf(",")).ToString();
                ordercode = "'" + ordercode;
                string sqltext = "";
                sqltext = "SELECT orderno, marnm, margg, margb, marcz, marunit, marfzunit, picno, ptcode, marid, num, fznum, zxnum, zxfznum, cgtimerq, " +
                          "recdate, recgdnum, recgdfznum, taxrate, detailnote, keycoms, detailstate, detailcstate, whstate, length, width, fixed, pjid, " +
                          "pjnm, engid, engnm, ctprice, planmode, ctamount, price, amount, supplierid, suppliernm, zdrid, zdrnm, substring(zdtime,0,11) as zdtime, shrid, " +
                          "shrnm, shtime, ywyid, ywynm, zgid, zgnm, depid, depnm, totalstate, totalcstate, totalnote, fax, phono, conname, abstract, " +
                          "PO_MASHAPE,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,PO_OperateState,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME,PO_PZ  " +
                          "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where orderno in (" + ordercode + ") order by orderno desc,ptcode asc";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                int m = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["PO_MASHAPE"].ToString().Contains("板") || dt.Rows[i]["PO_MASHAPE"].ToString().Contains("定尺板") || dt.Rows[i]["PO_MASHAPE"].ToString().Contains("圆") || dt.Rows[i]["PO_MASHAPE"].ToString().Contains("型"))
                    {
                        m++;
                    }
                }
                if (m == dt.Rows.Count)
                {
                    ExportDataItemgc(dt);
                }
                else if (m == 0)
                {
                    ExportDataItemfgc(dt);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导出数据中包含钢材和非钢材，无法导出！！！');", true);
                }
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要导出的订单！！！');", true);
                getArticle(false);
            }
        }

        private int ifselect()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
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
        private void ExportDataItemgc(System.Data.DataTable dt)
        {
            #region
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //Worksheet wksheet;
            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("采购订单明细表模版") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //m_xlApp.Visible = false;    // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            //wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            //System.Data.DataTable dt = objdt;

            //// 填充数据
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    wksheet.Cells[i + 3, 1] = Convert.ToString(i + 1);//序号

            //    wksheet.Cells[i + 3, 2] = "'" + dt.Rows[i]["orderno"].ToString();//订单编号

            //    wksheet.Cells[i + 3, 3] = "'" + dt.Rows[i]["suppliernm"].ToString();//供应商

            //    wksheet.Cells[i + 3, 4] = "'" + dt.Rows[i]["zdtime"].ToString();//制单日期

            //    wksheet.Cells[i + 3, 5] = "'" + dt.Rows[i]["ptcode"].ToString();//计划跟踪号

            //    wksheet.Cells[i + 3, 6] = "'" + dt.Rows[i]["pjnm"].ToString();//项目名称

            //    wksheet.Cells[i + 3, 7] = "'" + dt.Rows[i]["engnm"].ToString();//工程名称

            //    wksheet.Cells[i + 3, 8] = "'" + dt.Rows[i]["PO_TUHAO"].ToString();//图号/标识号

            //    wksheet.Cells[i + 3, 9] = "'" + dt.Rows[i]["marid"].ToString();//物料编码

            //    wksheet.Cells[i + 3, 10] = "'" + dt.Rows[i]["marnm"].ToString();//名称

            //    wksheet.Cells[i + 3, 11] = "'" + dt.Rows[i]["margg"].ToString();//规格

            //    wksheet.Cells[i + 3, 12] = "'" + dt.Rows[i]["marcz"].ToString();//材质

            //    wksheet.Cells[i + 3, 13] = "'" + dt.Rows[i]["margb"].ToString();//国标

            //    wksheet.Cells[i + 3, 14] = dt.Rows[i]["price"].ToString();//单价

            //    wksheet.Cells[i + 3, 15] = dt.Rows[i]["amount"].ToString();//金额

            //    wksheet.Cells[i + 3, 16] = dt.Rows[i]["ctprice"].ToString();//含税单价

            //    wksheet.Cells[i + 3, 17] = dt.Rows[i]["ctamount"].ToString();//价税合计

            //    wksheet.Cells[i + 3, 18] = dt.Rows[i]["zxnum"].ToString();//采购数量

            //    wksheet.Cells[i + 3, 19] = "'" + dt.Rows[i]["marunit"].ToString();//单位

            //    wksheet.Cells[i + 3, 20] = dt.Rows[i]["zxfznum"].ToString();//辅助数量

            //    wksheet.Cells[i + 3, 21] = "'" + dt.Rows[i]["marfzunit"].ToString();//辅助单位

            //    wksheet.Cells[i + 3, 22] = dt.Rows[i]["recgdnum"].ToString();//已到货数量

            //    wksheet.Cells[i + 3, 23] = "'" + dt.Rows[i]["recdate"].ToString();//到货日期

            //    wksheet.Cells[i + 3, 24] = "'" + dt.Rows[i]["cgtimerq"].ToString();//交货日期

            //    wksheet.Cells[i + 3, 25] = "'" + dt.Rows[i]["length"].ToString();//长度

            //    wksheet.Cells[i + 3, 26] = "'" + dt.Rows[i]["width"].ToString();//宽度

            //    wksheet.Cells[i + 3, 27] = "'" + dt.Rows[i]["detailnote"].ToString();//备注

            //    wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 27]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            //    wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 27]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            //    wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 27]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            //}
            ////设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            //string filename = Server.MapPath("/PC_Data/ExportFile/" + "采购订单明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
            #endregion

            string filename = "订单" + dt.Rows[0]["orderno"].ToString() + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream

            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("采购订单钢材类.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据
                IRow row1 = sheet0.GetRow(1);
                IRow row10 = sheet0.GetRow(10);
                IRow row13 = sheet0.GetRow(13);
                row1.GetCell(18).SetCellValue(dt.Rows[0]["orderno"].ToString());//订单编号
                row10.GetCell(1).SetCellValue(dt.Rows[0]["suppliernm"].ToString());//供应商
                row13.GetCell(17).SetCellValue(dt.Rows[0]["zdtime"].ToString());//制单日期
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 18);
                    row.HeightInPoints = 14;//行高
                    #region MyRegion

                    //row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号
                    //row.CreateCell(1).SetCellValue("'" + dt.Rows[i]["orderno"].ToString());//订单编号
                    //row.CreateCell(2).SetCellValue("'" + dt.Rows[i]["suppliernm"].ToString());//供应商
                    //row.CreateCell(3).SetCellValue("'" + dt.Rows[i]["zdtime"].ToString());//制单日期
                    //row.CreateCell(4).SetCellValue("'" + dt.Rows[i]["ptcode"].ToString());//计划跟踪号
                    //row.CreateCell(5).SetCellValue("'" + dt.Rows[i]["pjnm"].ToString());//项目名称
                    //row.CreateCell(6).SetCellValue("'" + dt.Rows[i]["engnm"].ToString());//工程名称
                    //row.CreateCell(7).SetCellValue("'" + dt.Rows[i]["PO_TUHAO"].ToString());//图号/标识号
                    //row.CreateCell(8).SetCellValue("'" + dt.Rows[i]["marid"].ToString());//物料编码
                    //row.CreateCell(9).SetCellValue("'" + dt.Rows[i]["marnm"].ToString());//名称
                    //row.CreateCell(10).SetCellValue("'" + dt.Rows[i]["margg"].ToString());//规格
                    //row.CreateCell(11).SetCellValue("'" + dt.Rows[i]["marcz"].ToString());//材质
                    //row.CreateCell(12).SetCellValue("'" + dt.Rows[i]["margb"].ToString());//国标
                    //row.CreateCell(13).SetCellValue(dt.Rows[i]["price"].ToString());//单价
                    //row.CreateCell(14).SetCellValue(dt.Rows[i]["amount"].ToString());//金额
                    //row.CreateCell(15).SetCellValue(dt.Rows[i]["ctprice"].ToString());//含税单价
                    //row.CreateCell(16).SetCellValue(dt.Rows[i]["ctamount"].ToString());//价税合计
                    //row.CreateCell(17).SetCellValue(dt.Rows[i]["zxnum"].ToString());//采购数量
                    //row.CreateCell(18).SetCellValue("'" + dt.Rows[i]["marunit"].ToString());//单位
                    //row.CreateCell(19).SetCellValue(dt.Rows[i]["zxfznum"].ToString());//辅助数量
                    //row.CreateCell(20).SetCellValue("'" + dt.Rows[i]["marfzunit"].ToString());//辅助单位
                    //row.CreateCell(21).SetCellValue(dt.Rows[i]["recgdnum"].ToString());//已到货数量
                    //row.CreateCell(22).SetCellValue("'" + dt.Rows[i]["recdate"].ToString());//到货日期
                    //row.CreateCell(23).SetCellValue("'" + dt.Rows[i]["cgtimerq"].ToString());//交货日期
                    //row.CreateCell(24).SetCellValue("'" + dt.Rows[i]["length"].ToString());//长度
                    //row.CreateCell(25).SetCellValue("'" + dt.Rows[i]["width"].ToString());//宽度
                    //row.CreateCell(26).SetCellValue("'" + dt.Rows[i]["detailnote"].ToString());//备注

                    #endregion

                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号
                    row.CreateCell(1).SetCellValue(dt.Rows[i]["marid"].ToString());//物料编码
                    row.CreateCell(2).SetCellValue(dt.Rows[i]["marnm"].ToString());//物料名称

                    row.CreateCell(3).SetCellValue(dt.Rows[i]["PO_TUHAO"].ToString());//图号
                    row.CreateCell(4).SetCellValue(dt.Rows[i]["margg"].ToString());//规格
                    row.CreateCell(5).SetCellValue(dt.Rows[i]["marcz"].ToString());//材质
                    row.CreateCell(6).SetCellValue(dt.Rows[i]["margb"].ToString());//国标
                    row.CreateCell(7).SetCellValue(dt.Rows[i]["PO_MASHAPE"].ToString());//类型
                    row.CreateCell(8).SetCellValue(dt.Rows[i]["marunit"].ToString());//单位
                    row.CreateCell(9).SetCellValue(dt.Rows[i]["zxnum"].ToString());//采购数量
                    row.CreateCell(10).SetCellValue(dt.Rows[i]["zxfznum"].ToString());//辅助数量
                    row.CreateCell(11).SetCellValue(dt.Rows[i]["length"].ToString());//长度
                    row.CreateCell(12).SetCellValue(dt.Rows[i]["width"].ToString());//宽度
                    row.CreateCell(13).SetCellValue(dt.Rows[i]["PO_PZ"].ToString());//片/支
                    row.CreateCell(14).SetCellValue(dt.Rows[i]["ctprice"].ToString());//含税单价
                    row.CreateCell(15).SetCellValue(dt.Rows[i]["ctamount"].ToString());//加税合计
                    row.CreateCell(16).SetCellValue(dt.Rows[i]["cgtimerq"].ToString());//交货日期
                    row.CreateCell(17).SetCellValue(dt.Rows[i]["detailnote"].ToString());//备注
                    row.CreateCell(18).SetCellValue(dt.Rows[i]["ptcode"].ToString());//计划跟踪号


                    #region
                    //row.CreateCell(5).SetCellValue(dt.Rows[i]["margb"].ToString());//国标
                    //row.CreateCell(6).SetCellValue(dt.Rows[i]["PO_TUHAO"].ToString());//图号/标识号
                    //row.CreateCell(3).SetCellValue(dt.Rows[i]["PO_CHILDENGNAME"].ToString());//部件名称
                    //row.CreateCell(7).SetCellValue(dt.Rows[i]["margg"].ToString());//规格
                    //row.CreateCell(8).SetCellValue(dt.Rows[i]["marcz"].ToString());//材质
                    //row.CreateCell(13).SetCellValue(dt.Rows[i]["PO_TECUNIT"].ToString());//辅助单位
                    //row.CreateCell(19).SetCellValue(dt.Rows[i]["price"].ToString());//单价（不含税）
                    //row.CreateCell(20).SetCellValue(dt.Rows[i]["amount"].ToString());//金额（不含税）
                    //row.CreateCell(22).SetCellValue(dt.Rows[i]["recdate"].ToString());//到货日期
                    //row.CreateCell(25).SetCellValue(dt.Rows[i]["taxrate"].ToString());//税率
                    //row.CreateCell(26).SetCellValue(dt.Rows[i]["recgdnum"].ToString());//已到货数量
                    #endregion

                    NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                    font1.FontName = "仿宋";//字体
                    font1.FontHeightInPoints = 11;//字号
                    ICellStyle cells = wk.CreateCellStyle();
                    cells.SetFont(font1);
                    cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                    for (int j = 0; j <= 18; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }
                }

                #endregion
                //页脚信息
                IRow row20 = sheet0.CreateRow(dt.Rows.Count + 20);
                row20.CreateCell(1).SetCellValue("部门：");
                row20.CreateCell(2).SetCellValue("采购部");
                row20.CreateCell(5).SetCellValue("部门主管：");
                row20.CreateCell(6).SetCellValue("李鑫");
                row20.CreateCell(8).SetCellValue("业务员：");
                row20.CreateCell(9).SetCellValue(dt.Rows[0]["zdrnm"].ToString());
                row20.CreateCell(11).SetCellValue("制单：");
                row20.CreateCell(12).SetCellValue(dt.Rows[0]["zdrnm"].ToString());


                for (int i = 0; i <= 18; i++)
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

        private void ExportDataItemfgc(System.Data.DataTable dt)
        {
            string filename = "订单" + dt.Rows[0]["orderno"].ToString() + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream

            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("采购订单非钢材类.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据
                IRow row1 = sheet0.GetRow(1);
                IRow row10 = sheet0.GetRow(10);
                IRow row13 = sheet0.GetRow(13);
                row1.GetCell(16).SetCellValue(dt.Rows[0]["orderno"].ToString());//订单编号
                row10.GetCell(2).SetCellValue(dt.Rows[0]["suppliernm"].ToString());//供应商
                row13.GetCell(15).SetCellValue(dt.Rows[0]["zdtime"].ToString());//制单日期
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 18);
                    row.HeightInPoints = 14;//行高
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号
                    row.CreateCell(1).SetCellValue(dt.Rows[i]["marid"].ToString());//物料编码
                    row.CreateCell(2).SetCellValue(dt.Rows[i]["marnm"].ToString());//物料名称
                    row.CreateCell(3).SetCellValue(dt.Rows[i]["PO_TUHAO"].ToString());//图号
                    row.CreateCell(4).SetCellValue(dt.Rows[i]["margg"].ToString());//规格
                    row.CreateCell(5).SetCellValue(dt.Rows[i]["marcz"].ToString());//材质
                    row.CreateCell(6).SetCellValue(dt.Rows[i]["margb"].ToString());//国标
                    row.CreateCell(7).SetCellValue(dt.Rows[i]["PO_MASHAPE"].ToString());//类型
                    row.CreateCell(8).SetCellValue(dt.Rows[i]["marunit"].ToString());//单位
                    row.CreateCell(9).SetCellValue(dt.Rows[i]["zxnum"].ToString());//采购数量
                    row.CreateCell(10).SetCellValue(dt.Rows[i]["marfzunit"].ToString());//辅助单位
                    row.CreateCell(11).SetCellValue(dt.Rows[i]["zxfznum"].ToString());//辅助数量
                    //row.CreateCell(8).SetCellValue(dt.Rows[i]["length"].ToString());//长度
                    //row.CreateCell(9).SetCellValue(dt.Rows[i]["width"].ToString());//宽度
                    //row.CreateCell(10).SetCellValue(dt.Rows[i]["PO_PZ"].ToString());//片/支
                    row.CreateCell(12).SetCellValue(dt.Rows[i]["ctprice"].ToString());//含税单价
                    row.CreateCell(13).SetCellValue(dt.Rows[i]["ctamount"].ToString());//加税合计
                    row.CreateCell(14).SetCellValue(dt.Rows[i]["cgtimerq"].ToString());//交货日期
                    row.CreateCell(15).SetCellValue(dt.Rows[i]["detailnote"].ToString());//备注
                    row.CreateCell(16).SetCellValue(dt.Rows[i]["ptcode"].ToString());//计划跟踪号

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

                #endregion
                //页脚信息
                IRow row20 = sheet0.CreateRow(dt.Rows.Count + 20);
                row20.CreateCell(1).SetCellValue("部门：");
                row20.CreateCell(2).SetCellValue("采购部");
                row20.CreateCell(5).SetCellValue("部门主管：");
                row20.CreateCell(6).SetCellValue("李鑫");
                row20.CreateCell(8).SetCellValue("业务员：");
                row20.CreateCell(9).SetCellValue(dt.Rows[0]["zdrnm"].ToString());
                row20.CreateCell(11).SetCellValue("制单：");
                row20.CreateCell(12).SetCellValue(dt.Rows[0]["zdrnm"].ToString());
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

        protected void selectall_CheckedChanged(object sender, EventArgs e)
        {
            //if (selectall.Checked)
            //{
            //    foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
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
            //    foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
            //    {
            //        System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
            //        if (cbx != null)//存在行
            //        {
            //            cbx.Checked = false;
            //        }
            //    }
            //}
        }

        protected void btn_LX_click(object sender, EventArgs e)//连选
        {
            int i = 0;
            int j = 0;
            int start = 0;
            int finish = 0;
            int k = 0;
            foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
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
                foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
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
            foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                cbx.Checked = false;
            }
        }

        //添加合同审批
        protected void btn_AddHTSP_Click(object sender, EventArgs e)
        {
            int checknum = ifselect();//判断是否选择数据行

            if (checknum == 0)
            {
                double ZJE = 0;
                string strb_orderid = "";
                foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx.Checked)
                    {
                        System.Web.UI.WebControls.Label lb_shstate = Reitem.FindControl("PO_shbz") as System.Web.UI.WebControls.Label;

                        if (lb_shstate.Text == "是")
                        {
                            //去掉相同的订单号
                            string add_pocode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PO_CODE")).Text.ToString();
                            if (!strb_orderid.Contains(add_pocode))
                            {
                                strb_orderid += "'" + add_pocode + "',";
                            }
                            string sqltext = "select PCON_ORDERID from TBPM_CONPCHSINFO where PCON_ORDERID like '%" + add_pocode + "%'";
                            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
                            if (dt1.Rows.Count > 0)
                            {
                                //getArticle(true); //刷新
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已添加合同，请勿重复添加！');", true);
                                getArticle(false);
                                return;
                            }

                        }
                        else if (lb_shstate.Text == "否")
                        {
                            //getArticle(true); //刷新
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择已审核的订单！');", true);
                            getArticle(false);
                            return;
                        }

                    }
                }

                strb_orderid = strb_orderid.Substring(0, strb_orderid.Length - 1);

                //合同总价要先对订单分组求和（group by），并保留两位，再将各订单的总价相加，否则最后结果因四舍五入与订单相加结果有出入                
                string sql = "select round(sum(ctamount),2) as DDZJ from View_TBPC_PURORDERDETAIL_PLAN_MPLAN where orderno in (" + strb_orderid + ") and detailcstate='0' group by orderno";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double JinE = Convert.ToDouble(dt.Rows[i]["DDZJ"].ToString());
                        ZJE = ZJE + JinE;
                    }
                }


                string sql_checkSupplier = "select  DISTINCT (SUPPLIERID) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL WHERE ORDERNO IN (" + strb_orderid + ")";
                System.Data.DataTable DT = DBCallCommon.GetDTUsingSqlText(sql_checkSupplier);
                if (DT.Rows.Count > 1)  //选择的订单包括多个供应商，不能添加
                {
                    //getArticle(true); //刷新
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择同一供应商的订单！');", true);
                    getArticle(false);
                    return;
                }
                else
                {
                    //getArticle(true);
                    strb_orderid = strb_orderid.Replace("'", "");   //将字符串中的单引号去掉，否则传递参数时会自动截断！
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "js", "Add_HTSP('" + strb_orderid + "','" + ZJE + "');", true);
                    getArticle(false);
                }
            }
            else if (checknum == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要添加合同审批的数据行！');", true);
                getArticle(false);
            }
        }

        //重置条件
        protected void btnReset_Click(object sender, EventArgs e)
        {
            tb_orderno.Text = string.Empty;
            tb_supply.Text = string.Empty;
            tb_StartTime.Text = string.Empty;
            tb_EndTime.Text = string.Empty;
            tb_name.Text = string.Empty;
            tb_cz.Text = string.Empty;
            tb_gg.Text = string.Empty;
            tb_gb.Text = string.Empty;
            tb_pj.Text = string.Empty;
            tb_eng.Text = string.Empty;
            tb_th.Text = string.Empty;
            drp_stu.ClearSelection();
            drp_stu.Items[0].Selected = true;
        }

        //取消
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }

        protected void QueryButton_Click(object sender, EventArgs e)
        {
            getArticle(true);
            ModalPopupExtenderSearch.Hide();
        }

        protected void tb_supply_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (tb_supply.Text.ToString().Contains("|"))
            {
                Cname = tb_supply.Text.Substring(0, tb_supply.Text.ToString().IndexOf("|"));
                tb_supply.Text = Cname.Trim();
            }
            else if (tb_supply.Text == "")
            {

            }
        }
        protected void tb_pj_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (tb_pj.Text.ToString().Contains("|"))
            {
                Cname = tb_pj.Text.Substring(0, tb_pj.Text.ToString().IndexOf("|"));
                tb_pj.Text = Cname.Trim();
            }
            else if (tb_pj.Text == "")
            {

            }
        }
        protected void tb_eng_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (tb_eng.Text.ToString().Contains("|"))
            {
                Cname = tb_eng.Text.Substring(0, tb_eng.Text.ToString().IndexOf("|"));
                tb_eng.Text = Cname.Trim();
            }
            else if (tb_eng.Text == "")
            {

            }
        }

        //添加订单请款
        protected void btn_AddDDQK_Click(object sender, EventArgs e)
        {
            int checknum = ifselect();//判断是否选择数据行

            if (checknum == 0)
            {
                double ZJE = 0;   //总金额
                string strb_orderid = "";  //订单号
                foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx.Checked)
                    {
                        System.Web.UI.WebControls.Label lb_shstate = Reitem.FindControl("PO_shbz") as System.Web.UI.WebControls.Label;

                        if (lb_shstate.Text == "是")
                        {
                            //去掉相同的订单号
                            string add_pocode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PO_CODE")).Text.ToString();
                            if (!strb_orderid.Contains(add_pocode))
                            {
                                string sqltext = "select DQ_DDCode from TBPM_ORDER_CHECKREQUEST where DQ_DDCODE like '%" + add_pocode + "%'";
                                System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
                                if (dt1.Rows.Count > 0)
                                {
                                    getArticle(false);//刷新
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('所选订单已添加非合同采购订单请款，请勿重复添加！');", true);
                                    return;
                                }
                                strb_orderid += "'" + add_pocode + "',";
                            }

                        }
                        else if (lb_shstate.Text == "否")
                        {
                            getArticle(false);//刷新
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择已审核的订单！');", true);
                            return;
                        }

                    }
                }

                strb_orderid = strb_orderid.Substring(0, strb_orderid.Length - 1);

                //合同总价要先对订单分组求和（group by），并保留两位，再将各订单的总价相加，否则最后结果因四舍五入与订单相加结果有出入                
                string sql = "select round(sum(ctamount),2) as DDZJ from View_TBPC_PURORDERDETAIL_PLAN_MPLAN where orderno in (" + strb_orderid + ") and detailcstate='0' group by orderno";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double JinE = Convert.ToDouble(dt.Rows[i]["DDZJ"].ToString());
                        ZJE = ZJE + JinE;
                    }
                }


                string sql_checkSupplier = "select  DISTINCT (SUPPLIERID) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL WHERE ORDERNO IN (" + strb_orderid + ")";
                System.Data.DataTable DT = DBCallCommon.GetDTUsingSqlText(sql_checkSupplier);
                if (DT.Rows.Count > 1)  //选择的订单包括多个供应商，不能添加
                {
                    getArticle(false);//刷新
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择同一供应商的订单！');", true);
                    return;
                }
                else
                {
                    getArticle(false);//刷新
                    string ddcs_code = DT.Rows[0][0].ToString();
                    strb_orderid = strb_orderid.Replace("'", "");   //将字符串中的单引号去掉，否则传递参数时会自动截断！
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "js", "Add_DDQK('" + strb_orderid + "','" + ZJE + "','" + ddcs_code + "');", true);
                }
            }
            else if (checknum == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要添加合同审批的数据行！');", true);
                getArticle(false);
            }
        }

        //添加发票
        protected void btn_AddDDFP_Click(object sender, EventArgs e)
        {
            int checknum = ifselect();//判断是否选择数据行

            if (checknum == 0)
            {
                double ZJE = 0;   //总金额
                string strb_orderid = "";  //订单号
                foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx.Checked)
                    {
                        System.Web.UI.WebControls.Label lb_shstate = Reitem.FindControl("PO_shbz") as System.Web.UI.WebControls.Label;

                        if (lb_shstate.Text == "是")
                        {
                            //去掉相同的订单号
                            string add_pocode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PO_CODE")).Text.ToString();
                            if (!strb_orderid.Contains(add_pocode))
                            {
                                string sqltext = "select OB_DDCode from TBPM_ORDER_BILL where OB_DDCODE like '%" + add_pocode + "%'";
                                System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
                                if (dt1.Rows.Count > 0)
                                {
                                    getArticle(false);//刷新
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('所选订单已添加采购订单发票信息，请勿重复添加！');", true);
                                    return;
                                }
                                strb_orderid += "'" + add_pocode + "',";
                            }
                        }
                        else if (lb_shstate.Text == "否")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择已审核的订单！');", true);
                            getArticle(false);
                            return;
                        }

                    }
                }

                strb_orderid = strb_orderid.Substring(0, strb_orderid.Length - 1);

                //合同总价要先对订单分组求和（group by），并保留两位，再将各订单的总价相加，否则最后结果因四舍五入与订单相加结果有出入                
                string sql = "select round(sum(ctamount),2) as DDZJ from View_TBPC_PURORDERDETAIL_PLAN_MPLAN where orderno in (" + strb_orderid + ") and detailcstate='0' group by orderno";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        double JinE = Convert.ToDouble(dt.Rows[i]["DDZJ"].ToString());
                        ZJE = ZJE + JinE;
                    }
                }
                string sql_checkSupplier = "select  DISTINCT (SUPPLIERID) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL WHERE ORDERNO IN (" + strb_orderid + ")";
                System.Data.DataTable DT = DBCallCommon.GetDTUsingSqlText(sql_checkSupplier);
                if (DT.Rows.Count > 1)  //选择的订单包括多个供应商，不能添加
                {
                    getArticle(false);//刷新
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择同一供应商的订单！');", true);
                    return;
                }
                else
                {
                    getArticle(false); //页面刷新
                    string ddcs_code = DT.Rows[0][0].ToString();
                    strb_orderid = strb_orderid.Replace("'", "");   //将字符串中的单引号去掉，否则传递参数时会自动截断！
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "js", "Add_DDFP('" + strb_orderid + "','" + ZJE + "','" + ddcs_code + "');", true);
                }
            }
            else if (checknum == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要添加订单发票记录的数据行！');", true);
                getArticle(false);
            }
        }

        protected void btn_mianjian_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (RepeaterItem item in Purordertotal_list_Repeater.Items)
            {
                if ((item.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptc = (item.FindControl("ptcode") as System.Web.UI.WebControls.Label).Text;
                    string sql = "insert into dbo.TBQM_APLYFORITEM(RESULT,PTC) values ('免检','" + ptc + "')";
                    list.Add(sql);
                }
            }
            DBCallCommon.ExecuteTrans(list);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交成功！');", true);
            getArticle(false);
        }
    }
}
