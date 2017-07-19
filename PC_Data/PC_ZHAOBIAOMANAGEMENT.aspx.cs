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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_ZHAOBIAOMANAGEMENT : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindrpt();
            }
            this.InitVar();

            checksession();
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
            pager_org.TableName = "PC_POWERINFO as a left join TBPC_IQRCMPPRCRVW as b on a.poweriqcode=b.ICL_SHEETNO left join TBDS_STAFFINFO as c on b.ICL_REVIEWA=c.ST_ID";
            pager_org.PrimaryKey = "ICL_SHEETNO";
            pager_org.ShowFields = "*";
            pager_org.OrderField = "ICL_SHEETNO";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 100;
        }


        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string sql = "1=1 and powersupplyid='" + Session["supplyid"].ToString().Trim() + "'";
            if (rad_mypart.Checked)
            {
                sql += " and ICL_STATE='0'";
            }
            else if (rad_yiwancheng.Checked)
            {
                sql += " and ICL_STATE is not null and ICL_STATE!='0'";
            }
            return sql;
        }


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
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, checked_list_Repeater, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        private void checksession()
        {
            if (Session["supplyid"].ToString().Trim() == "" || Session["supplyid"] == null)
            {
                Response.Write("<script>alert('会话已过期,请重新登录！！！');if(window.parent!=null)window.parent.location.href='http://111.160.8.74:888/PC_Data/PC_ZHAOBIAOLOGIN.aspx';else{window.location.href='http://111.160.8.74:888/PC_Data/PC_ZHAOBIAOLOGIN.aspx'} </script>");
                //Response.Write("<script>alert('会话已过期,请重新登录！！！');if(window.parent!=null)window.parent.location.href='~/PC_Data/PC_ZHAOBIAOLOGIN.aspx';else{window.location.href='~/PC_Data/PC_ZHAOBIAOLOGIN.aspx'} </script>");
            }
        }

        protected void rad_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }


        protected void checked_list_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
           
        }
    }
}
