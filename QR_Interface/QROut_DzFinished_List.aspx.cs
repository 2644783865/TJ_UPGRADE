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

namespace ZCZJ_DPF.QR_Interface
{
    public partial class QROut_DzFinished_List : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        ////页数
        public int ObjPageSize
        {
            get
            {
                if (ViewState["ObjPageSize"] == null)
                {
                    //默认是升序
                    ViewState["ObjPageSize"] = 50;
                }
                return Convert.ToInt32(ViewState["ObjPageSize"]);
            }
            set
            {
                ViewState["ObjPageSize"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);

            if (!IsPostBack)
            {
                getOrderInfo(true, false);
            }
        }
        protected void getOrderInfo(bool isFristPage, bool isUpdateCondition)
        {
            string condition = GetCondition();

            string TableName = "(select UniqID_Out, OldYzName_Out, OldHtCode_Out, OldTaskCode_Out, OldYzHtCode_Out, OldProjName_Out, ProdName_Out, MapCode_Out, Caizhi_Out, SingleWeight_Out, OutNum_Out, OutUnit_Out, OutProdCode_Out, Money_Out, TaskCode_Out, SmPerson_Out, OutTime_Out, RealAddrs_Out, ZnZw_Out, IfERP_Out, DfReason_Out, Note_Out,QRDzOutID*1 as QRDzOutID from midTable_DzFinished_Out)t";
            string PrimaryKey = "QRDzOutID";
            string ShowFields = "*";
            string OrderField = "TaskCode_Out,MapCode_Out,OutProdCode_Out";
            int OrderType = 0;
            string StrWhere = condition;
            int PageSize = ObjPageSize;

            InitVar(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize, isFristPage);
        }

        private string GetCondition()
        {
            //总表，从表都不等于0
            string condition = "1=1";
            //账内账外
            if (rblZnZwState.SelectedValue.ToString().Trim() != "")
            {
                condition += " and ZnZw_Out='" + rblZnZwState.SelectedValue.ToString().Trim() + "'";
            }
            //是否进入ERP
            if (rblIfERPState.SelectedValue.ToString().Trim() != "")
            {
                condition += " and IfERP_Out='" + rblIfERPState.SelectedValue.ToString().Trim() + "'";
            }
            //起始时间
            if (txtStartYearMonth.Text.Trim() != "")
            {
                condition += " and OutTime_Out>='" + txtStartYearMonth.Text.Trim() + "'";
            }
            //终止时间
            if (txtEndYearMonth.Text.Trim() != "")
            {
                condition += " and OutTime_Out<='" + txtEndYearMonth.Text.Trim() + "'";
            }
            //原任务号
            if (tbOldTaskCode_Out.Text.Trim() != "")
            {
                condition += " and OldTaskCode_Out like '%" + tbOldTaskCode_Out.Text.Trim() + "%'";
            }
            //原项目名称
            if (tbOldProjName_Out.Text.Trim() != "")
            {
                condition += " and OldProjName_Out like '%" + tbOldProjName_Out.Text.Trim() + "%'";
            }
            //产品名称
            if (tbProdName_Out.Text.Trim() != "")
            {
                condition += " and ProdName_Out like '%" + tbProdName_Out.Text.Trim() + "%'";
            }
            //图号
            if (tbMapCode_Out.Text.Trim() != "")
            {
                condition += " and MapCode_Out like '%" + tbMapCode_Out.Text.Trim() + "%'";
            }
            //产品编号
            if (tbOutProdCode_Out.Text.Trim() != "")
            {
                condition += " and OutProdCode_Out like '%" + tbOutProdCode_Out.Text.Trim() + "%'";
            }
            //实际发货地址
            if (tbRealAddrs_Out.Text.Trim() != "")
            {
                condition += " and RealAddrs_Out like '%" + tbRealAddrs_Out.Text.Trim() + "%'";
            }
            //实际任务号
            if (tbTaskCode_Out.Text.Trim() != "")
            {
                condition += " and TaskCode_Out like '%" + tbTaskCode_Out.Text.Trim() + "%'";
            }
            return condition;
        }
        private void InitVar(string TableName, string PrimaryKey, string ShowFields, string OrderField, int OrderType, string StrWhere, int PageSize, bool isFristPage)
        {
            InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);

            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数

            if (isFristPage)
            {
                UCPaging1.CurrentPage = 1;
            }

            //否则即为当前页

            bindData();
        }
        //初始化分页信息
        private void InitPager(string TableName, string PrimaryKey, string ShowFields, string OrderField, int OrderType, string StrWhere, int PageSize)
        {
            pager.TableName = TableName;

            pager.PrimaryKey = PrimaryKey;

            pager.ShowFields = ShowFields;

            pager.OrderField = OrderField;

            pager.OrderType = OrderType;

            pager.StrWhere = StrWhere;

            pager.PageSize = PageSize;
        }
        void Pager_PageChanged(int pageNumber)
        {
            getOrderInfo(false, false);
        }

        protected void bindData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;

            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);

            CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);

            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }
        protected void Query_Click(object sender, EventArgs e)
        {
            getOrderInfo(true, true);
        }
        protected void rblZnZwState_SelectedIndexChanged(object sender, EventArgs e)
        {
            getOrderInfo(true, true);
        }
        protected void rblIfERPState_SelectedIndexChanged(object sender, EventArgs e)
        {
            getOrderInfo(true, true);
        }
    }
}
