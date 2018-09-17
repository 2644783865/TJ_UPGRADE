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
    public partial class QRManage_DzFinished_List : System.Web.UI.Page
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

            string TableName = "(select OldYzName, OldHtCode, OldTaskCode, OldYzHtCode, OldProjName, ProdName, MapCode, Caizhi, InProdCode, SmPerson, InTime, InNum, InUnit, InMoney, SingleWeight, OutNum, OutMoney, ZnZw, IfERP, InNote,QRDzID*1 as QRDzID from midTable_DzFinished_management)t";
            string PrimaryKey = "QRDzID";
            string ShowFields = "*,(InNum-OutNum) as StorageNum,(InMoney-OutMoney) as StorageMoney";
            string OrderField = "OldTaskCode,MapCode,InProdCode";
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
                condition += " and ZnZw='" + rblZnZwState.SelectedValue.ToString().Trim() + "'";
            }
            //是否进入ERP
            if (rblIfERPState.SelectedValue.ToString().Trim() != "")
            {
                condition += " and IfERP='" + rblIfERPState.SelectedValue.ToString().Trim() + "'";
            }
            //起始时间
            if (txtStartYearMonth.Text.Trim() != "")
            {
                condition += " and InTime>='" + txtStartYearMonth.Text.Trim() + "'";
            }
            //终止时间
            if (txtEndYearMonth.Text.Trim() != "")
            {
                condition += " and InTime<='" + txtEndYearMonth.Text.Trim() + "'";
            }
            //原任务号
            if (tbOldTaskCode.Text.Trim() != "")
            {
                condition += " and OldTaskCode like '%" + tbOldTaskCode.Text.Trim() + "%'";
            }
            //原项目名称
            if (tbOldProjName.Text.Trim() != "")
            {
                condition += " and OldProjName like '%" + tbOldProjName.Text.Trim() + "%'";
            }
            //产品名称
            if (tbProdName.Text.Trim() != "")
            {
                condition += " and ProdName like '%" + tbProdName.Text.Trim() + "%'";
            }
            //图号
            if (tbMapCode.Text.Trim() != "")
            {
                condition += " and MapCode like '%" + tbMapCode.Text.Trim() + "%'";
            }
            //产品编号
            if (tbInProdCode.Text.Trim() != "")
            {
                condition += " and InProdCode like '%" + tbInProdCode.Text.Trim() + "%'";
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
