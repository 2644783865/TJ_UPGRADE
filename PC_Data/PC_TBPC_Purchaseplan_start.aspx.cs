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
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchaseplan_start : System.Web.UI.Page
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


        public string gloabstr
        {
            get
            {
                object str = ViewState["gloabstr"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabstr"] = value;
            }
        }

        string xywhere = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            ViewState["ObjPageSize"] = Convert.ToDouble(DropDownList5.SelectedValue);
            if (!IsPostBack)
            {
                if (Session["UserDeptID"].ToString() == "03")
                {
                    btn_BGdata.Visible = true;
                    btn_editbeizhu.Visible = true;
                }
                //hid_filter.Text = "(select top(10000) * from View_TBPC_PLAN_PLACE order by PUR_ID desc)t" + "/" + Session["UserID"].ToString();
                //if (DropDownListrange.SelectedIndex == 1)
                //{
                hid_filter.Text = "View_TBPC_PLAN_PLACE" + "/" + Session["UserID"].ToString();
                //}
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
                bindcloseper();
                initpageinfo();
                getArticle(true);
                //getClassTwo();

            }
            //CheckUser(ControlFinder);
        }

        //绑定关闭操作人
        private void bindcloseper()
        {
            string sql = "select distinct Pur_ClosePer from TBPC_PURCHASEPLAN";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            Drpcloseper.DataValueField = "Pur_ClosePer";
            Drpcloseper.DataTextField = "Pur_ClosePer";
            Drpcloseper.DataSource = dt;
            Drpcloseper.DataBind();
            ListItem item = new ListItem("--请选择--", "0");
            Drpcloseper.Items.Insert(0, item);
        }

        private void initpageinfo()
        {
            if (Request.QueryString["ptc"] != null)
            {
                gloabstr = Request.QueryString["ptc"].ToString();
            }
            else
            {
                gloabstr = "";
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

            pager.GroupField = GroupField;

            pager.OrderType = OrderType;

            pager.StrWhere = StrWhere;

            pager.PageSize = PageSize;
        }

        protected void bindData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamGroupBy(pager);
            CommonFun.Paging(dt, purchaseplan_start_list_Repeater, UCPaging1, NoDataPane);
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

        protected void radio_nxiatui_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void radio_xiatui_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }

        protected void radio_CSH_CheckedChanged(object sender, EventArgs e)
        {

            getArticle(true);
        }

        protected void radio_YXT_CheckedChanged(object sender, EventArgs e)
        {

            getArticle(true);
        }
        protected void rad_YGB_CheckedChanged(object sender, EventArgs e)
        {

            getArticle(true);
        }

        protected void purchaseplan_start_list_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string sqltext = "";
            string ptcode = "";
            string code = "";
            string cstate = "";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ptcode = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PTCODE")).Text;
                cstate = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_CSTATE")).Text;
                //占用库存
                sqltext = "select  PUR_PCODE from TBPC_MARSTOUSEALL where PUR_PTCODE='" + ptcode + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0 && cstate == "1")
                {
                    code = dt.Rows[0]["PUR_PCODE"].ToString();
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_ZY")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_ZY")).Text = "是";
                    ((HyperLink)e.Item.FindControl("HyperLink3")).NavigateUrl = "PC_TBPC_Purchaseplan_check_detail.aspx?sheetno=" + Server.UrlEncode(code) + "&ptc=" + Server.UrlEncode(ptcode) + "";
                }
                else
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_ZY")).Text = "否";
                }
                //相似代用
                sqltext = "select MP_CODE from TBPC_MARREPLACEALL where MP_PTCODE='" + ptcode + "' and  substring(MP_CODE,5,1)='0'";
                dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0 && cstate == "1")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_SSDY")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_SSDY")).Text = "是";
                    ((HyperLink)e.Item.FindControl("HyperLink1")).NavigateUrl = "~/PC_Data/PC_TBPC_PLAN_PLACE.aspx?ptc=" + Server.UrlEncode(ptcode) + "";
                }
                else
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_SSDY")).Text = "否";
                }
                //代用
                sqltext = "select MP_CODE from TBPC_MARREPLACEALL where MP_PTCODE='" + ptcode + "' and  substring(MP_CODE,5,1)='1' ";
                dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0 && cstate == "0")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_DY")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_DY")).Text = "是";
                    ((HyperLink)e.Item.FindControl("HyperLink2")).NavigateUrl = "~/PC_Data/PC_TBPC_PLAN_PLACE.aspx?ptc=" + Server.UrlEncode(ptcode) + "";
                }
                else
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_DY")).Text = "否";
                }
                System.Web.UI.WebControls.Label purstate = (System.Web.UI.WebControls.Label)e.Item.FindControl("purstate");
                if (purstate.Text.ToString().Trim() == "是")
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "#00E600";
                }
                string ptccode1 = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PTCODE")).Text.ToString();
                string zybz = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_ZY")).Text.ToString();
                string xszybz = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_SSDY")).Text.ToString();
                string sqlifck = "select * from View_SM_OUT where PTC='" + ptccode1 + "'";
                System.Data.DataTable dtifck = DBCallCommon.GetDTUsingSqlText(sqlifck);
                if (dt.Rows.Count > 0 && (zybz == "是"))
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_ZY")).BackColor = System.Drawing.Color.Blue;
                }
                else if (dt.Rows.Count > 0 && (xszybz == "是"))
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_SSDY")).BackColor = System.Drawing.Color.Blue;
                }


                string PUR_IFFAST = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_IFFAST")).Text.Trim();
                if (PUR_IFFAST == "1")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("rownum")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("rownum")).ToolTip = "该物料为加急物料";
                }
            }
            //foreach(RepeaterItem item in purchaseplan_start_list_Repeater.Items)
            //{
            //    Label purstate = (Label)item.FindControl("purstate");
            //    if (purstate.Text.ToString().Trim() == "是")
            //    {
            //        ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "Yellow";
            //    }
            //}
        }

        public string get_plan_shstate(string i)
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

        public string get_plan_xtstate(string i)
        {
            string statestr = "";
            if (Convert.ToInt32(i) >= 5)
            {
                statestr = "是";
            }
            else
            {

                statestr = "否";

            }
            return statestr;
        }







        private void getArticle(bool isFirstPage)      //取得Article数据
        {
            CreateDataSource();

            //string TableName = "(select top(10000) * from (select a.PR_SHEETNO as planno,a.PR_DEPID as depid,b.DEP_NAME as depnm,a.PR_SQREID as sqrid,c.ST_NAME as sqrnm,d.PUR_MASHAPE as PUR_MASHAPE,a.PR_SQTIME as sqrtime,a.PR_STATE as prstate,d.PUR_ZYDY as PUR_ZYDY,d.PUR_ID as PUR_ID,d.PUR_PTCODE as Aptcode,d.PUR_MARID as marid,e.MNAME as marnm,e.GUIGE as margg,e.CAIZHI as marcz,e.GB as margb,d.PUR_LENGTH as length,d.PUR_WIDTH as width,d.PUR_TUHAO as PUR_TUHAO,d.PUR_CSTATE as PUR_CSTATE,d.PUR_NUM as num,d.PUR_FZNUM as fznum,d.PUR_STATE as purstate,d.PUR_BACKNOTE as PUR_BACKNOTE,d.Pur_ClosePer as Pur_ClosePer,d.Pur_ColseTime as Pur_ColseTime,d.Pue_Closetype as Pue_Closetype from TBPC_PCHSPLANRVW as a left join TBDS_DEPINFO as b on a.PR_DEPID=b.DEP_CODE left join TBDS_STAFFINFO as c on a.PR_SQREID = c.ST_ID left join TBPC_PURCHASEPLAN as d on a.PR_SHEETNO=d.PUR_PCODE left join TBMA_MATERIAL as e on d.PUR_MARID=e.ID)s order by PUR_ID desc)t";

            //if (DropDownListrange.SelectedIndex == 1)
            //{
            string TableName = "(select a.PR_SHEETNO as planno,a.PR_DEPID as depid,b.DEP_NAME as depnm,a.PR_SQREID as sqrid,c.ST_NAME as sqrnm,d.PUR_MASHAPE as PUR_MASHAPE,a.PR_SQTIME as sqrtime,a.PR_STATE as prstate,d.PUR_ZYDY as PUR_ZYDY,d.PUR_ID as PUR_ID,d.PUR_PTCODE as Aptcode,d.PUR_MARID as marid,e.MNAME as marnm,e.GUIGE as margg,e.CAIZHI as marcz,e.GB as margb,d.PUR_LENGTH as length,d.PUR_WIDTH as width,d.PUR_TUHAO as PUR_TUHAO,d.PUR_CSTATE as PUR_CSTATE,d.PUR_NUM as num,d.PUR_FZNUM as fznum,d.PUR_STATE as purstate,d.PUR_BACKNOTE as PUR_BACKNOTE,d.Pur_ClosePer as Pur_ClosePer,d.Pur_ColseTime as Pur_ColseTime,d.Pue_Closetype as Pue_Closetype,PUR_XTR,PUR_XTTIME,PUR_IFFAST from TBPC_PCHSPLANRVW as a left join TBDS_DEPINFO as b on a.PR_DEPID=b.DEP_CODE left join TBDS_STAFFINFO as c on a.PR_SQREID = c.ST_ID left join TBPC_PURCHASEPLAN as d on a.PR_SHEETNO=d.PUR_PCODE left join TBMA_MATERIAL as e on d.PUR_MARID=e.ID)t";
            //}

            string PrimaryKey = "Aptcode";

            string ShowFields = " planno AS PCODE," +
                                "depid AS DEPID," +
                                "depnm AS DEPNAME,sqrid AS SQREID,sqrnm AS SQRENAME,PUR_MASHAPE as MASHAPE," +
                                "sqrtime AS SUBMITTM,ISNULL(prstate,0) AS STATE,PUR_ZYDY,PUR_ID," +
                                "Aptcode as ptcode, marid, marnm, margg, marcz, margb, length, width,sqrnm," +
                                "planno,PUR_TUHAO,PUR_CSTATE,prstate,num,fznum,(case purstate when '7' then '是' else '' end) as purstate,(case purstate when '4' then '是' when '5' then '是' when '6' then '是' when '7' then '是' else '否' end) as fgstate,PUR_BACKNOTE,Pur_ClosePer,Pur_ColseTime,(case Pue_Closetype when '0' then '意外关闭' when '1' then '相似占用关闭' when '2' then '占用关闭' when '3' then '任务暂停' else '' end) as Pue_Closetype,PUR_XTR,PUR_XTTIME,PUR_IFFAST ";

            //数据库中的主键
            string OrderField = "PUR_ID";

            string GroupField = "";

            int OrderType = 1;
            /**/
            string StrWhere = ViewState["sqlwhere"].ToString();

            int PageSize = ObjPageSize;

            BindPage(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFirstPage);

        }



        public void CreateDataSource()
        {
            string tableuser = hid_filter.Text;
            string filter = "";
            string sql = "SELECT tableuser, filter FROM TBPC_FILTER_INFO where tableuser='" + tableuser + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            while (dr.Read())
            {
                filter = dr[1].ToString();
            }
            dr.Close();
            int j = 0;
            string sqlwhere = "";
            //string sqltext = "select distinct planno from (select top(10000) * from View_TBPC_PLAN_PLACE order by PUR_ID desc)t where  ";
            //string sqltext1 = "select Aptcode from (select top(10000) * from View_TBPC_PLAN_PLACE order by PUR_ID desc)t where  ";
            //if (DropDownListrange.SelectedIndex == 1)
            //{
            string sqltext = "select distinct planno from (select a.PR_SHEETNO as planno,a.PR_DEPID as depid,b.DEP_NAME as depnm,a.PR_SQREID as sqrid,c.ST_NAME as sqrnm,d.PUR_MASHAPE as PUR_MASHAPE,a.PR_SQTIME as sqrtime,a.PR_STATE as prstate,d.PUR_ZYDY as PUR_ZYDY,d.PUR_ID as PUR_ID,d.PUR_PTCODE as Aptcode,d.PUR_MARID as marid,e.MNAME as marnm,e.GUIGE as margg,e.CAIZHI as marcz,e.GB as margb,d.PUR_LENGTH as length,d.PUR_WIDTH as width,d.PUR_TUHAO as PUR_TUHAO,d.PUR_CSTATE as PUR_CSTATE,d.PUR_NUM as num,d.PUR_FZNUM as fznum,d.PUR_STATE as purstate,d.PUR_BACKNOTE as PUR_BACKNOTE,d.Pur_ClosePer as Pur_ClosePer,d.Pur_ColseTime as Pur_ColseTime,d.Pue_Closetype as Pue_Closetype,PUR_IFFAST from TBPC_PCHSPLANRVW as a left join TBDS_DEPINFO as b on a.PR_DEPID=b.DEP_CODE left join TBDS_STAFFINFO as c on a.PR_SQREID = c.ST_ID left join TBPC_PURCHASEPLAN as d on a.PR_SHEETNO=d.PUR_PCODE left join TBMA_MATERIAL as e on d.PUR_MARID=e.ID)t where  ";
            string sqltext1 = "select Aptcode from (select a.PR_SHEETNO as planno,a.PR_DEPID as depid,b.DEP_NAME as depnm,a.PR_SQREID as sqrid,c.ST_NAME as sqrnm,d.PUR_MASHAPE as PUR_MASHAPE,a.PR_SQTIME as sqrtime,a.PR_STATE as prstate,d.PUR_ZYDY as PUR_ZYDY,d.PUR_ID as PUR_ID,d.PUR_PTCODE as Aptcode,d.PUR_MARID as marid,e.MNAME as marnm,e.GUIGE as margg,e.CAIZHI as marcz,e.GB as margb,d.PUR_LENGTH as length,d.PUR_WIDTH as width,d.PUR_TUHAO as PUR_TUHAO,d.PUR_CSTATE as PUR_CSTATE,d.PUR_NUM as num,d.PUR_FZNUM as fznum,d.PUR_STATE as purstate,d.PUR_BACKNOTE as PUR_BACKNOTE,d.Pur_ClosePer as Pur_ClosePer,d.Pur_ColseTime as Pur_ColseTime,d.Pue_Closetype as Pue_Closetype,PUR_IFFAST from TBPC_PCHSPLANRVW as a left join TBDS_DEPINFO as b on a.PR_DEPID=b.DEP_CODE left join TBDS_STAFFINFO as c on a.PR_SQREID = c.ST_ID left join TBPC_PURCHASEPLAN as d on a.PR_SHEETNO=d.PUR_PCODE left join TBMA_MATERIAL as e on d.PUR_MARID=e.ID)t where  ";
            //}
            sqlwhere = "1=1 and purstate not in('8','9')";
            if (filter != "")
            {
                sqlwhere = sqlwhere + " and " + filter + "";
            }
            if (tb_gg.Text != "")
            {
                sqlwhere = sqlwhere + " and  patindex('%" + tb_gg.Text.Trim() + "%',margg)>0";
            }
            if (radio_CSH.Checked)
            {
                sqlwhere = sqlwhere + " and prstate not in('5')";
            }
            if (radio_YXT.Checked)
            {
                sqlwhere = sqlwhere + " and prstate='5'";
            }
            if (rad_YGB.Checked)
            {
                sqlwhere = sqlwhere + " and (PUR_CSTATE='1' or PUR_CSTATE='2') and purstate='0'";
            }

            if (tb_th.Text != "")//图号
            {
                sqlwhere = sqlwhere + " and patindex('%" + tb_th.Text.Trim() + "%',PUR_TUHAO)>0";
            }
            if (tb_zdr.Text != "")//申请人
            {
                sqlwhere = sqlwhere + " and patindex('%" + tb_zdr.Text + "%',sqrnm)>0";
            }
            if (DropDownList3.SelectedIndex != 0)//申请部门
            {
                sqlwhere = sqlwhere + " and depid = '" + DropDownList3.SelectedValue + "'";
            }
            if (tb_StartTime.Text != "")//开始时间
            {
                string startdate = tb_StartTime.Text.ToString() == "" ? "1900-01-01" : tb_StartTime.Text.ToString();
                sqlwhere = sqlwhere + " and sqrtime>='" + startdate + "'";
            }
            if (tb_EndTime.Text != "")//结束时间
            {
                string enddate = tb_EndTime.Text.ToString() == "" ? "2100-01-01" : tb_EndTime.Text.ToString();
                enddate = enddate + " 23:59:59";
                sqlwhere = sqlwhere + " and sqrtime<='" + enddate + "'";
            }
            if (tb_orderno.Text != "")//批号
            {
                sqlwhere = sqlwhere + " and patindex('%" + tb_orderno.Text.Trim() + "%',planno)>0";
            }
            if (tb_supply.Text != "")//计划跟踪号
            {
                sqlwhere = sqlwhere + " and patindex('%" + tb_supply.Text.Trim() + "%',Aptcode)>0";
            }
            if (tb_marid.Text != "")//物料编码
            {
                sqlwhere = sqlwhere + " and patindex('%" + tb_marid.Text.Trim() + "%',marid)>0";
            }
            if (tb_name.Text != "")//名称
            {
                sqlwhere = sqlwhere + " and patindex('%" + tb_name.Text.Trim() + "%',marnm)>0";
            }
            if (tb_cz.Text != "")//材质
            {
                sqlwhere = sqlwhere + " and patindex('%" + tb_cz.Text.Trim() + "%',marcz)>0";
            }
            if (tb_shape.Text != "")//类型
            {
                sqlwhere = sqlwhere + " and patindex('%" + tb_shape.Text.Trim() + "%',PUR_MASHAPE)>0";
            }
            if (tb_gb.Text != "")//国标
            {
                sqlwhere = sqlwhere + " and patindex('%" + tb_gb.Text.Trim() + "%',margb)>0";
            }

            if (txtclosestartdate.Text.Trim() != "")
            {
                sqlwhere = sqlwhere + " and Pur_ColseTime>='" + txtclosestartdate.Text.Trim() + " 00:00:01'";
            }
            if (txtcloseenddate.Text.Trim() != "")
            {
                sqlwhere = sqlwhere + " and Pur_ColseTime<='" + txtcloseenddate.Text.Trim() + " 23:59:59'";
            }
            if (Drpclosetype.SelectedIndex != 0)
            {
                if (Drpclosetype.SelectedIndex == 1)
                {
                    sqlwhere = sqlwhere + " and Pue_Closetype='0'";
                }
                else if (Drpclosetype.SelectedIndex == 2)
                {
                    sqlwhere = sqlwhere + " and Pue_Closetype='1'";
                }
                else
                {
                    sqlwhere = sqlwhere + " and Pue_Closetype='2'";
                }
            }
            if (Drpcloseper.SelectedIndex != 0)
            {
                sqlwhere = sqlwhere + " and Pur_ClosePer='" + Drpcloseper.SelectedValue.ToString().Trim() + "'";
            }
            if (chkiffast.Checked)
            {
                sqlwhere = sqlwhere + " and PUR_IFFAST='1'";
            }

            //2013年4月19日 11:26:18
            //局限于标准件和油漆
            //if (Session["UserDeptID"].ToString() == "07")
            xywhere = Request.QueryString["Xywhere"];
            if (xywhere == "CY")
            {
                sqlwhere = sqlwhere + " and (substring(marid,0,6)='01.01' or substring(marid,0,6)='01.15')";
            }
            sqltext = sqltext + sqlwhere;
            sqltext1 = sqltext1 + sqlwhere;
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext1);

            Label1.Text = Convert.ToString(dt.Rows.Count);
            Label2.Text = Convert.ToString(dt1.Rows.Count);
            ViewState["sqlwhere"] = sqlwhere;
        }

        //查询
        protected void QueryButton_Click(object sender, EventArgs e)
        {

            getArticle(true);
        }

        //取消
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }

        //重置条件
        protected void btnReset_Click(object sender, EventArgs e)
        {
            tb_orderno.Text = "";
            tb_supply.Text = "";
            tb_StartTime.Text = "";
            tb_EndTime.Text = "";
            tb_name.Text = "";
            tb_cz.Text = "";
            tb_gg.Text = "";
            tb_gb.Text = "";
            //tb_pj.Text = "";
            //tb_eng.Text = "";
            tb_th.Text = "";
            //tb_zdr.Text = "";
        }





        protected void btn_daochu_click(object sender, EventArgs e)
        {
            string sqltext = "SELECT planno, ptcode,pjnm,engnm,depnm,sqrnm,sqrtime,PUR_TUHAO, " +
                                      "marid, marnm, margg, marcz, margb,PUR_MASHAPE,length,width,num, marunit,fznum,marfzunit,rpnum,rpfznum,cgrnm,case WHEN PUR_CSTATE='0' then '否' else '是' end as pur_cstate,PUR_ZYDY,purnote  " +
                                      "FROM  View_TBPC_PURCHASEPLAN_RVW   where " + ViewState["sqlwhere"].ToString();
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 10000)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导出的数据量超过10000条，请添加导出条件，分多次导出！');", true);
            }
            else
            {
                string filename = "材料需用计划" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
                HttpContext.Current.Response.Clear();
                //1.读取Excel到FileStream 
                using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("材料需用计划模版1.xls")))
                {
                    IWorkbook wk = new HSSFWorkbook(fs);
                    ISheet sheet0 = wk.GetSheetAt(0);

                    #region 写入数据

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet0.CreateRow(i + 2);
                        row.CreateCell(0).SetCellValue(i + 1);
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
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

        }





        protected void btn_bgdata_click(object sender, EventArgs e)
        {
            System.Data.DataTable dt_cgr_1 = new System.Data.DataTable();
            dt_cgr_1.Columns.Add("CGR_ID", typeof(string));
            dt_cgr_1.Columns.Add("CG_JHGZH", typeof(string));
            List<string> list0 = new List<string>();
            List<string> list1 = new List<string>();
            int k = 0;
            int m = 0;
            int n = 0;
            int s = 0;
            int b = 0;
            int c = 0;
            string engrwh = "";
            for (int r = 0; r < purchaseplan_start_list_Repeater.Items.Count; r++)
            {
                if ((purchaseplan_start_list_Repeater.Items[r].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    m++;
                }
            }
            if (m == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择任何项!')", true);
                return;
            }
            for (int f = 0; f < purchaseplan_start_list_Repeater.Items.Count; f++)
            {
                if ((purchaseplan_start_list_Repeater.Items[f].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptc6 = (purchaseplan_start_list_Repeater.Items[f].FindControl("PTCODE") as System.Web.UI.WebControls.Label).Text;
                    string sql6 = "select * from TBWS_INDETAIL where WG_PTCODE='" + ptc6 + "'";
                    System.Data.DataTable dt6 = DBCallCommon.GetDTUsingSqlText(sql6);
                    if (dt6.Rows.Count > 0)
                    {
                        s++;
                    }
                }
            }
            if (s > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('有物料已经入库，无法变更!')", true);
                return;
            }

            for (int a = 0; a < purchaseplan_start_list_Repeater.Items.Count; a++)
            {
                if ((purchaseplan_start_list_Repeater.Items[a].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptca = (purchaseplan_start_list_Repeater.Items[a].FindControl("PTCODE") as System.Web.UI.WebControls.Label).Text;
                    string sqla = "select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptca + "'";
                    System.Data.DataTable dta = DBCallCommon.GetDTUsingSqlText(sqla);
                    if (dta.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dta.Rows[0]["PUR_STATE"].ToString()) < 4)
                        {
                            b++;
                        }
                        else if (Convert.ToInt32(dta.Rows[0]["PUR_STATE"].ToString()) >= 4)
                        {
                            c++;
                        }
                    }
                }
            }
            if (b > 0 && c > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('同时存在已分工和未分工的物料，请分别提交!')", true);
                return;
            }
            //任务号不同不能同时提变更
            List<string> listx = new List<string>();
            for (int x = 0; x < purchaseplan_start_list_Repeater.Items.Count; x++)
            {
                if ((purchaseplan_start_list_Repeater.Items[x].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptcx = (purchaseplan_start_list_Repeater.Items[x].FindControl("PTCODE") as System.Web.UI.WebControls.Label).Text;
                    //string sqlx = "select * from (select top(10000) * from View_TBPC_PLAN_PLACE order by PUR_ID desc)t where Aptcode='" + ptcx + "'";
                    //if (DropDownListrange.SelectedIndex == 1)
                    //{
                    string sqlx = "select * from View_TBPC_PLAN_PLACE where Aptcode='" + ptcx + "'";
                    //}
                    System.Data.DataTable dtx = DBCallCommon.GetDTUsingSqlText(sqlx);
                    engrwh = dtx.Rows[0]["engid"].ToString();
                    listx.Add(engrwh);
                }
            }
            bool g = listx.TrueForAll(h => h == engrwh);
            if (g == false)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('任务号不同不能同时提变更!')", true);
                return;
            }



            for (int r = 0; r < purchaseplan_start_list_Repeater.Items.Count; r++)
            {
                if ((purchaseplan_start_list_Repeater.Items[r].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptc = (purchaseplan_start_list_Repeater.Items[r].FindControl("PTCODE") as System.Web.UI.WebControls.Label).Text;
                    string sql3 = "select * from TBPC_BG where BG_PTC='" + ptc + "' and RESULT!='已执行' and RESULT!='已驳回'";
                    System.Data.DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sql3);
                    if (dt0.Rows.Count > 0)
                    {
                        k++;
                    }
                }
            }
            if (k > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('变更重复!')", true);
                return;
            }

            for (int l = 0; l < purchaseplan_start_list_Repeater.Items.Count; l++)
            {
                if ((purchaseplan_start_list_Repeater.Items[l].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptc5 = (purchaseplan_start_list_Repeater.Items[l].FindControl("PTCODE") as System.Web.UI.WebControls.Label).Text;
                    string sql5 = "select * from TBPC_PURCHASEPLAN where (PUR_CSTATE='1' or PUR_CSTATE='2') and PUR_PTCODE='" + ptc5 + "'";
                    System.Data.DataTable dt5 = DBCallCommon.GetDTUsingSqlText(sql5);
                    if (dt5.Rows.Count > 0)
                    {
                        n++;
                    }
                }
            }
            if (n > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('有物料已关闭或占用!')", true);
                return;
            }

            //2016.12.8修改
            for (int w = 0; w < purchaseplan_start_list_Repeater.Items.Count; w++)
            {
                if ((purchaseplan_start_list_Repeater.Items[w].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptc_cgy = (purchaseplan_start_list_Repeater.Items[w].FindControl("PTCODE") as System.Web.UI.WebControls.Label).Text;
                    string sqlcgr = "select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptc_cgy + "'";
                    System.Data.DataTable dtcgr = DBCallCommon.GetDTUsingSqlText(sqlcgr);
                    string getcgrposition = "select ST_POSITION,ST_ID,ST_NAME from TBDS_STAFFINFO where ST_ID='" + dtcgr.Rows[0]["PUR_CGMAN"] + "'";
                    System.Data.DataTable dtgetposition = DBCallCommon.GetDTUsingSqlText(getcgrposition);
                    if (dtgetposition.Rows.Count > 0)
                    {
                        string cgrid = dtgetposition.Rows[0]["ST_ID"].ToString();
                        if (!string.IsNullOrEmpty(cgrid))
                        {
                            dt_cgr_1.Rows.Add(new object[] { cgrid, ptc_cgy });
                        }
                    }
                }
            }
            if (dt_cgr_1.Rows.Count > 0)
            {
                System.Data.DataTable dt_cgr_2 = dt_cgr_1.Clone();
                dt_cgr_2.PrimaryKey = new DataColumn[] { dt_cgr_2.Columns["CGR_ID"] };
                foreach (DataRow row in dt_cgr_1.Rows)
                {
                    DataRow srow = dt_cgr_2.Rows.Find(new object[] { row["CGR_ID"].ToString() });
                    if (srow == null)
                    {
                        dt_cgr_2.Rows.Add(row.ItemArray);
                    }
                    else
                    {
                        srow["CG_JHGZH"] = srow["CG_JHGZH"].ToString() + "," + row["CG_JHGZH"].ToString();
                    }
                }
                if (dt_cgr_2.Rows.Count > 1)
                {
                    string message_about_cgy = "";
                    for (int v = 0; v < dt_cgr_2.Rows.Count; v++)
                    {
                        message_about_cgy += dt_cgr_2.Rows[v]["CG_JHGZH"].ToString() + "\\n与\\n";
                    }
                    message_about_cgy = message_about_cgy.Remove(message_about_cgy.Length - 3);
                    message_about_cgy += "\\n有不同的采购员，请分别进行变更！";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('" + message_about_cgy + "')", true);
                    return;
                }

            }
            //else
            //{
            string bgrname = Session["UserName"].ToString().Trim();
            string bgrid = Session["UserID"].ToString().Trim();
            string ph = "BG" + DateTime.Now.ToString("yyyyMMddHHmmss") + Session["UserID"].ToString().Trim();
            for (int i = 0; i < purchaseplan_start_list_Repeater.Items.Count; i++)
            {
                if ((purchaseplan_start_list_Repeater.Items[i].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptcinsert = (purchaseplan_start_list_Repeater.Items[i].FindControl("PTCODE") as System.Web.UI.WebControls.Label).Text;
                    string sql0 = "insert into TBPC_BG(BG_PH,BG_PTC,BG_NAME,BG_NAMEID) values('" + ph + "','" + ptcinsert + "','" + bgrname + "','" + bgrid + "')";
                    list0.Add(sql0);
                }
            }
            DBCallCommon.ExecuteTrans(list0);
            for (int j = 0; j < purchaseplan_start_list_Repeater.Items.Count; j++)
            {
                if ((purchaseplan_start_list_Repeater.Items[j].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string jishushenheren = "";
                    string jishushenherenid = "";
                    if (engrwh.Contains("BEIKU") || engrwh.Contains("beiku") || engrwh.Contains("备库"))
                    {
                        string sqlgetshr1 = "select ST_NAME,ST_ID from TBPC_OTPUR_AUDIT as a left join TBPC_OTPURPLAN as b on a.PA_CODE=b.MP_PCODE left join TBDS_STAFFINFO as c on a.PA_FIR_PER=c.ST_ID where MP_PTCODE='" + (purchaseplan_start_list_Repeater.Items[j].FindControl("PTCODE") as System.Web.UI.WebControls.Label).Text.Trim() + "'";
                        System.Data.DataTable dtgetshr1 = DBCallCommon.GetDTUsingSqlText(sqlgetshr1);
                        if (dtgetshr1.Rows.Count > 0)
                        {
                            jishushenheren = dtgetshr1.Rows[0]["ST_NAME"].ToString().Trim();
                            jishushenherenid = dtgetshr1.Rows[0]["ST_ID"].ToString().Trim();
                        }
                    }
                    else
                    {
                        string sqlshr1 = "select TSA_REVIEWER,TSA_REVIEWERID from TBPM_TCTSASSGN where TSA_ID='" + engrwh + "'";
                        System.Data.DataTable dtshr1 = DBCallCommon.GetDTUsingSqlText(sqlshr1);
                        if (dtshr1.Rows.Count > 0)
                        {
                            jishushenheren = dtshr1.Rows[0]["TSA_REVIEWER"].ToString().Trim();
                            jishushenherenid = dtshr1.Rows[0]["TSA_REVIEWERID"].ToString().Trim();
                        }
                    }

                    string sqlshr2 = "select ST_NAME,ST_ID from TBDS_STAFFINFO where ST_ID='67'";
                    System.Data.DataTable dtshr2 = DBCallCommon.GetDTUsingSqlText(sqlshr2);
                    string sqlshr3 = "select ST_NAME,ST_ID from TBDS_STAFFINFO where ST_POSITION='0701'";
                    System.Data.DataTable dtshr3 = DBCallCommon.GetDTUsingSqlText(sqlshr3);
                    string ptcupdate = (purchaseplan_start_list_Repeater.Items[j].FindControl("PTCODE") as System.Web.UI.WebControls.Label).Text;
                    string sql1 = "update TBPC_BG set BG_XTZT='1',BG_SHRA='" + jishushenheren + "',BG_SHRIDA='" + jishushenherenid + "',BG_SHRB='" + dtshr2.Rows[0]["ST_NAME"].ToString() + "',BG_SHRIDB='" + dtshr2.Rows[0]["ST_ID"].ToString() + "',BG_SHRC='" + dtshr3.Rows[0]["ST_NAME"].ToString() + "',BG_SHRIDC='" + dtshr3.Rows[0]["ST_ID"].ToString() + "' where BG_PTC='" + ptcupdate + "' and BG_STATE='0' and BG_PH='" + ph + "'";
                    list1.Add(sql1);
                }
            }
            DBCallCommon.ExecuteTrans(list1);
            Response.Redirect("PC_BGYM.aspx?bgph=" + ph + "");
        }
        // }



        //protected void tb_pj_Textchanged(object sender, EventArgs e)
        //{
        //    string Cname = "";
        //    if (tb_pj.Text.ToString().Contains("|"))
        //    {
        //        Cname = tb_pj.Text.Substring(0, tb_pj.Text.ToString().IndexOf("|"));
        //        tb_pj.Text = Cname.Trim();
        //    }
        //    else if (tb_pj.Text == "")
        //    {

        //    }
        //}

        //protected void tb_eng_Textchanged(object sender, EventArgs e)
        //{
        //    string Cname = "";
        //    if (tb_eng.Text.ToString().Contains("|"))
        //    {
        //        Cname = tb_eng.Text.Substring(0, tb_eng.Text.ToString().IndexOf("|"));
        //        tb_eng.Text = Cname.Trim();
        //    }
        //    else if (tb_eng.Text == "")
        //    {

        //    }
        //}


        protected void btn_email_click(object sender, EventArgs e)
        {
            //邮件提醒
            string sprid = "";
            string sptitle = "";
            string spcontent = "";
            string sqltext = "select * from TBDS_STAFFINFO where EMAIL is not null and EMAIL!=''";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sprid = dt.Rows[i]["ST_ID"].ToString().Trim();
                    sptitle = "关于ERP程序修改";
                    spcontent = "在提交申请选择审批人时，因为ERP程序上的修改，需要将浏览器缓存清理一下，才能正常选人，所以请大家将浏览器缓存清理一下，给大家带来不便敬请谅解！";
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                }
            }
        }
        //修改备注
        protected void btn_queren_click(object sender, EventArgs e)
        {
            ModalPopupExtenderchange.Hide();
            List<string> listsql = new List<string>();
            int m = 0;
            for (int r = 0; r < purchaseplan_start_list_Repeater.Items.Count; r++)
            {
                if ((purchaseplan_start_list_Repeater.Items[r].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    m++;
                }
            }
            if (m == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择任何项!')", true);
                return;
            }

            if (tbcontent.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写变更内容!')", true);
                return;
            }

            string changername = Session["UserName"].ToString().Trim();
            string changeid = Session["UserID"].ToString().Trim();
            for (int i = 0; i < purchaseplan_start_list_Repeater.Items.Count; i++)
            {
                if ((purchaseplan_start_list_Repeater.Items[i].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptcodechange = (purchaseplan_start_list_Repeater.Items[i].FindControl("PTCODE") as System.Web.UI.WebControls.Label).Text;
                    string sql = "insert into TBPC_changebeizhu(change_PTC,changername,changeid,changetime,changecontent,changestate) values('" + ptcodechange + "','" + changername + "','" + changeid + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + tbcontent.Text.Trim() + "','0')";
                    listsql.Add(sql);
                }
            }
            DBCallCommon.ExecuteTrans(listsql);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功!')", true);
        }
        protected void btnquxiao_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderchange.Hide();
        }
    }
}
