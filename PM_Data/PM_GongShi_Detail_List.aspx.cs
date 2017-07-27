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
using System.Text;



namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_GongShi_Detail_List : BasicPage
    {

        string customerName, contractNum, tsaId,year,month;

        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {
            //CheckUser(ControlFinder);            
            InitVar();

            if (!IsPostBack)
            {                
                this.bindRepeater();
            }
        }



        /// <summary>
        /// 初始化信息
        /// </summary>
        private void InitVar()
        {
            //获取传递参数
            customerName = Request.QueryString["customerName"].ToString();
            contractNum = Request.QueryString["contractNum"].ToString();
            tsaId = Request.QueryString["tsaId"].ToString();
            year = Request.QueryString["year"].ToString();
            month = Request.QueryString["month"].ToString();

            //初始化页面信息
            GS_CUSNAME.Text = customerName;
            GS_CONTR.Text = contractNum;
            GS_TSAID.Text = tsaId;
            lbYEAR.Text = year;
            lbMONTH.Text = month;

            InitPager();//初始化分页查询对象

            UCPaging1.PageSize = pager.PageSize; //设置页大小
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);//将处理程序加入到翻页控件的翻页委托中
        }

        /// <summary>
        /// 初始化分页查询对象
        /// </summary>
        private void InitPager()
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("0=0");
            strWhere.Append("GS_CUSNAME='" + customerName + "'");
            strWhere.Append("AND GS_CONTR='" + contractNum + "'");
            strWhere.Append("AND GS_TSAID='" + tsaId + "'");
            strWhere.Append("AND DATEYEAR='" + year + "'");
            strWhere.Append("AND DATEMONTH='" + month + "'");
            strWhere.Append("AND IsDel='0'");            

            pager.TableName = "TBMP_GS_DETAIL_LIST";
            pager.PrimaryKey = "";
            pager.ShowFields = "*";
            pager.OrderField = "DATEYEAR";
            pager.StrWhere = strWhere.ToString();
            pager.OrderType = 1;//按时间降序
            pager.PageSize = 50;//每页显示的条数
        }


        /// <summary>
        /// 翻页事件出发的处理程序
        /// </summary>
        /// <param name="pageNumber"></param>
        void Pager_PageChanged(int pageNumber)//？为什么要传入参数
        {
            bindRepeater();//绑定数据
        }


        /// <summary>
        /// 给PM_GongShi_Detial_List_Repeater绑定数据，并初始化分页控件的属性
        /// </summary>
        private void bindRepeater()
        {
            pager.PageIndex = UCPaging1.CurrentPage;//当前页数
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);//分页查询
            CommonFun.Paging(dt, PM_GongShi_Detial_List_Repeater, UCPaging1, NoDataPanel);//绑定数据
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;//如果无数据则分页控件不显示
            }
            else
            {
                UCPaging1.Visible = true;//否则显示分页控件                
                UCPaging1.InitPageInfo();//同时显示出分页控件中要显示的控件
            }
        }

        /// <summary>
        /// 双击修改事件
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        protected string showYg(string Id)
        {
            return "javascript:window.showModalDialog('PM_Gongshi_detail_edit.aspx?action=edit&&Id=" + Id + "','','DialogWidth=800px;DialogHeight=700px')";
        }
    }
}