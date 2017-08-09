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

namespace ZCZJ_DPF.YS_Data
{
    public partial class YS_OUT_Detail_MAR : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();

            if (!IsPostBack)
            {
                bindData();
            }

        }


        private void InitVar()
        {

            getLLInfo();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;


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
            bindData();
        }

        protected void bindData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;

            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, RepeaterLL, UCPaging1, NoDataPanel);

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

        protected void getLLInfo()
        {
            string condition = GetStrCondition();

            string TableName = "View_SM_OUT";
            string PrimaryKey = "OP_ID";
            string ShowFields = "id as TrueCode,OutCode AS Code,DepCode AS DepCode,Dep AS Dep,WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,CONVERT(varchar(50),Date,23) AS Date,left(ApprovedDate,10) AS ApprovedDate,SenderCode AS SenderCode,Sender AS Sender,DocCode,Doc,VerifierCode AS VerifierCode,Verifier,ApprovedDate AS ApproveDate,ROB AS Colour,TotalState AS State,TotalNote AS Comment,SQCODE AS SQCODE,UniqueCode AS UniqueID,MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Length AS Length,Width AS Width,LotNumber AS LotNumber,Unit AS Unit,cast(round(UnitPrice,4) as float) AS UnitPrice,DueNumber AS DN,cast(RealNumber as float) AS RN,RealSupportNumber,cast(round(Amount,2) as float) AS Amount,WarehouseCode AS WarehouseOutCode,Warehouse AS WarehouseOut,LocationCode AS PositionOutCode,Location AS PositionOut,PlanMode AS PlanMode,PTC AS PTC,TSAID,ZZBZNM,DetailNote AS Note,OP_BSH AS BSH,OP_PAGENUM,OP_NOTE1";
            string OrderField = "id DESC,UniqueCode";
            int OrderType = 0;
            string StrWhere = condition;
            int PageSize = 15;
            InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);


            string sql = "select isnull(cast(sum(RealNumber) as float),0) as TotalRN,isnull(cast(round(sum(Amount),2) as float),0) as TotalAmount,isnull(cast(round(sum(RealSupportNumber),2) as float),0) as TotalFRN from View_SM_OUT where " + condition;
            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);
            if (sdr.Read())
            {
                hfdTotalRN.Value = sdr["TotalRN"].ToString();
                hfdTotalAmount.Value = sdr["TotalAmount"].ToString();
                hfdTotalFRN.Value = sdr["TotalFRN"].ToString();

            }
            sdr.Close();
        }

        protected string GetStrCondition()
        {
            Encrypt_Decrypt ed = new Encrypt_Decrypt();
            string ContractNo = ed.DecryptText(Request.QueryString["ContractNo"].ToString());
            string FatherCode = ed.DecryptText(Request.QueryString["FatherCode"].ToString());
            string mar = "";
            switch (FatherCode)
            {
                case "FERROUS_METAL": mar = "01.07"; break;
                case "PURCHASE_PART": mar = "01.11"; break;
                case "MACHINING_PART": mar = "01.08"; break;
                case "PAINT_COATING": mar = "01.15"; break;
                case "ELECTRICAL": mar = "01.18"; break;
                default: break;
            }

            if (FatherCode != "OTHERMAT_COST")
            {
                string strwhere = " 1=1 and TSAID='" + ContractNo + "' ";
                strwhere += "and MaterialCode like '" + mar + "%'";
                ViewState["strwhere"] = strwhere;
                return strwhere;
            }

            else
            {
                string strwhere = " 1=1 and TSAID='" + ContractNo + "' ";
                strwhere += " and MaterialCode not like '01.07%' " +
                    " and MaterialCode not like '01.11%' " +
                    " and MaterialCode not like '01.08%' " +
                    " and MaterialCode not like '01.15%' " +
                    " and MaterialCode not like '01.18%' ";
                ViewState["strwhere"] = strwhere;
                return strwhere;
            }
        }

        protected void RepeaterLL_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("TotalRN")).Text = hfdTotalRN.Value;
                ((Label)e.Item.FindControl("TotalAmount")).Text = hfdTotalAmount.Value;
                ((Label)e.Item.FindControl("TotalPageNum")).Text = hfdPageNum.Value;
                ((Label)e.Item.FindControl("TotalFRN")).Text = hfdTotalFRN.Value;
            }

        }


        //导出
        //库存=0；入库单=1；结转备库=2；领料单=3；项目完工=8(暂时未用)，项目结转=9

        protected void BtnShowExport_Click(object sender, EventArgs e)
        {
            string strwhere = ViewState["strwhere"].ToString();
            ExportDataFromYS.Export_MAR(strwhere);
        }

        protected string convertState(string state)
        {
            switch (state)
            {
                case "1": return "<font color='#FF0000'>未审核</font>";
                case "2": return "已审核";
                default: return state;
            }
        }

    }
}
