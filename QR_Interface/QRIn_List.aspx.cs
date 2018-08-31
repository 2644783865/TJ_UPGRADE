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
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ZCZJ_DPF.QR_Interface
{
    public partial class QRIn_List : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();

        //添加类
        private List<QueryItemInfo> queryItems = new List<QueryItemInfo>();

        protected string PageName
        {
            get
            {
                return "SM_WarehouseIN_WGPush.aspx";
            }

        }

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
            //InitVar();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);

            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["FLAG"] != null)
                    {
                        if (Request.QueryString["FLAG"].ToString() == "PUSH")
                        {
                            cancel.Visible = false;
                        }
                        if (Request.QueryString["FLAG"].ToString() == "APPEND")
                        {
                            //下推隐藏

                            Push.Visible = false;
                            Push.Enabled = false;
                        }
                        getOrderInfo(true, false);
                    }
                    else
                    {
                        getOrderInfo(true, false);
                    }
                }
                catch
                {
                    getOrderInfo(true, false);
                }


                this.Form.DefaultButton = btnQuery.UniqueID;
                LabelMessage.Text = "页面加载完毕！";
            }
        }


        private void showQueryModal()
        {

            ModalPopupExtenderSearch.Show();

            string strScript = "viewCondition();";
            this.ClientScript.RegisterStartupScript(this.GetType(), "error", strScript, true);

            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "FixedGridView", strScript, true);

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
            if (type == "System.Web.UI.WebControls.TextBox")
            {
                (this.UpdatePanelCondition.FindControl(controlID) as System.Web.UI.WebControls.TextBox).Text = controlValue;
            }
            if (type == "System.Web.UI.WebControls.DropDownList")
            {
                (this.UpdatePanelCondition.FindControl(controlID) as System.Web.UI.WebControls.DropDownList).ClearSelection();
                (this.UpdatePanelCondition.FindControl(controlID) as System.Web.UI.WebControls.DropDownList).SelectedValue = controlValue;
            }
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
            BindItem();
        }

        protected void getOrderInfo(bool isFristPage, bool isUpdateCondition)
        {
            string condition = GetCondition();

            string TableName = "(select a.*,b.RESULT,s.PUR_NUM as dingenum,q.*,m.* from midTable_QRIn as q left join (select ID as MA_ID,MNAME as MA_MNAME,GUIGE as MA_GUIGE,CAIZHI as MA_CAIZHI,GB as MA_GB from TBMA_MATERIAL) as m on q.QRIn_MatCode=m.MA_ID left join (select * from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where ptcode!='' and ptcode is not null) as a on a.ptcode=q.QRIn_PTC left join (select PTC,RESULT,ISAGAIN,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc,BJSJ desc) as rn from View_TBQM_APLYFORITEM) as c where rn<=1) as b on a.ptcode=b.PTC left join TBPC_PURCHASEPLAN as s on a.ptcode=s.PUR_PTCODE)t";
            string PrimaryKey = "QRIn_ID";
            string ShowFields = "orderno AS Code,supplierid AS SupplierCode,suppliernm AS Supplier,LEFT(shtime,10) AS Date,ywyid AS ClerkCode,ywynm AS Clerk,depid AS DepCode,depnm AS Dep,totalstate AS OrderState,case when marnm is null then MA_MNAME else marnm end AS MaterialName,case when marcz is null then MA_CAIZHI else marcz end AS Attribute,case when margb is null then MA_GB else margb end AS GB,case when margg is null then MA_GUIGE else margg end AS MaterialStandard,length,width,marunit AS Unit,marfzunit,cast(zxnum as float) AS Number,dingenum,cast(zxfznum as float) AS QUANTITY,cast(recgdnum as float) AS ArrivedNumber,LEFT(recdate,10) AS ArrivedDate,cast(price as float) AS UnitPrice,taxrate AS TaxRate,cast(ctprice as float) AS CTUP,cast(amount as float) AS Amount,cast(ctamount as float) AS CTA,planmode AS PlanMode,ptcode AS PTC,detailstate AS PushState,detailnote,(case when RESULT='——' then '未报检' else RESULT end) as RESULT,PO_MASHAPE,PO_TUHAO,QRIn_ID,QRIn_MatCode as MaterialCode,QRIn_PTC,QRIn_Num,QRIn_Time,(case when QRIn_State='0' then '未入库' when QRIn_State='1' then '已入库' else '其他' end) as QRIn_State,QRIn_Note,QRIn_Person";
            string OrderField = " orderno DESC ,marnm,margg,ptcode";
            int OrderType = 0;
            string StrWhere = condition;
            int PageSize = ObjPageSize;

            InitVar(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize, isFristPage);

        }

        private string GetCondition()
        {
            //总表，从表都不等于0
            string condition = "1=1";
            string QRcondition = "";
            //订单状态
            if ((DropDownListOrderState.SelectedValue == "0") && (DropDownListPushState.SelectedValue == "0"))
            {
                //全部
                condition = " totalstate='1' AND detailstate<>'0' ";

                addQueryItem(PageName, "DropDownListOrderState", DropDownListOrderState.GetType().ToString(), DropDownListOrderState.SelectedValue);
                addQueryItem(PageName, "DropDownListPushState", DropDownListPushState.GetType().ToString(), DropDownListPushState.SelectedValue);

            }
            else if ((DropDownListOrderState.SelectedValue == "0") && (DropDownListPushState.SelectedValue == "1"))
            {

                condition = " totalstate='1' AND (detailstate='1' or detailstate='3') and detailcstate='0' ";
                addQueryItem(PageName, "DropDownListOrderState", DropDownListOrderState.GetType().ToString(), DropDownListOrderState.SelectedValue);
                addQueryItem(PageName, "DropDownListPushState", DropDownListPushState.GetType().ToString(), DropDownListPushState.SelectedValue);
            }
            else if ((DropDownListOrderState.SelectedValue == "0") && (DropDownListPushState.SelectedValue == "2"))
            {

                condition = " totalstate='1' AND detailstate='2' and detailcstate='0' ";
                addQueryItem(PageName, "DropDownListOrderState", DropDownListOrderState.GetType().ToString(), DropDownListOrderState.SelectedValue);
                addQueryItem(PageName, "DropDownListPushState", DropDownListPushState.GetType().ToString(), DropDownListPushState.SelectedValue);
            }
            else if ((DropDownListOrderState.SelectedValue == "0") && (DropDownListPushState.SelectedValue == "3"))
            {

                condition = " totalstate='1' AND detailstate='1' and detailcstate='1' ";
                addQueryItem(PageName, "DropDownListOrderState", DropDownListOrderState.GetType().ToString(), DropDownListOrderState.SelectedValue);
                addQueryItem(PageName, "DropDownListPushState", DropDownListPushState.GetType().ToString(), DropDownListPushState.SelectedValue);
            }

            else if ((DropDownListOrderState.SelectedValue == "1") && (DropDownListPushState.SelectedValue == "0"))
            {
                //订单采购中，明细全部
                condition = " totalstate='1' and totalcstate='0' AND detailstate<>'0' ";
                addQueryItem(PageName, "DropDownListOrderState", DropDownListOrderState.GetType().ToString(), DropDownListOrderState.SelectedValue);
                addQueryItem(PageName, "DropDownListPushState", DropDownListPushState.GetType().ToString(), DropDownListPushState.SelectedValue);
            }
            else if ((DropDownListOrderState.SelectedValue == "1") && (DropDownListPushState.SelectedValue == "1"))
            {
                //订单采购中，明细采购中
                condition = " totalstate='1' and totalcstate='0' AND (detailstate='1' or detailstate='3') and detailcstate='0'";
                addQueryItem(PageName, "DropDownListOrderState", DropDownListOrderState.GetType().ToString(), DropDownListOrderState.SelectedValue);
                addQueryItem(PageName, "DropDownListPushState", DropDownListPushState.GetType().ToString(), DropDownListPushState.SelectedValue);
            }
            else if ((DropDownListOrderState.SelectedValue == "1") && (DropDownListPushState.SelectedValue == "2"))
            {
                //订单采购中，明细采购完毕
                condition = " totalstate='1' and totalcstate='0' AND detailstate='2' and detailcstate='0'";
                addQueryItem(PageName, "DropDownListOrderState", DropDownListOrderState.GetType().ToString(), DropDownListOrderState.SelectedValue);
                addQueryItem(PageName, "DropDownListPushState", DropDownListPushState.GetType().ToString(), DropDownListPushState.SelectedValue);
            }
            else if ((DropDownListOrderState.SelectedValue == "1") && (DropDownListPushState.SelectedValue == "3"))
            {
                //订单采购中，明细采购手动关闭
                condition = " totalstate='1' and totalcstate='0' AND detailstate='1' and detailcstate='1'";
                addQueryItem(PageName, "DropDownListOrderState", DropDownListOrderState.GetType().ToString(), DropDownListOrderState.SelectedValue);
                addQueryItem(PageName, "DropDownListPushState", DropDownListPushState.GetType().ToString(), DropDownListPushState.SelectedValue);
            }
            else if ((DropDownListOrderState.SelectedValue == "2") && (DropDownListPushState.SelectedValue == "0"))
            {
                //订单采购完毕，明细全部
                condition = " totalstate='3' and totalcstate='0' AND detailstate<>'0' ";
                addQueryItem(PageName, "DropDownListOrderState", DropDownListOrderState.GetType().ToString(), DropDownListOrderState.SelectedValue);
                addQueryItem(PageName, "DropDownListPushState", DropDownListPushState.GetType().ToString(), DropDownListPushState.SelectedValue);

            }
            else if ((DropDownListOrderState.SelectedValue == "2") && (DropDownListPushState.SelectedValue == "1"))
            {
                //订单采购完毕，明细采购中
                condition = " totalstate='3' and totalcstate='0' AND (detailstate='1' or detailstate='3') and detailcstate='0'";
                addQueryItem(PageName, "DropDownListOrderState", DropDownListOrderState.GetType().ToString(), DropDownListOrderState.SelectedValue);
                addQueryItem(PageName, "DropDownListPushState", DropDownListPushState.GetType().ToString(), DropDownListPushState.SelectedValue);

            }
            else if ((DropDownListOrderState.SelectedValue == "2") && (DropDownListPushState.SelectedValue == "2"))
            {
                //订单采购完毕，明细采购完毕
                condition = " totalstate='3' and totalcstate='0' AND detailstate='2' and detailcstate='0'";
                addQueryItem(PageName, "DropDownListOrderState", DropDownListOrderState.GetType().ToString(), DropDownListOrderState.SelectedValue);
                addQueryItem(PageName, "DropDownListPushState", DropDownListPushState.GetType().ToString(), DropDownListPushState.SelectedValue);

            }
            else if ((DropDownListOrderState.SelectedValue == "2") && (DropDownListPushState.SelectedValue == "3"))
            {
                //订单采购完毕，明细手动关闭
                condition = " totalstate='3' and totalcstate='0' AND detailstate='1' and detailcstate='1'";
                addQueryItem(PageName, "DropDownListOrderState", DropDownListOrderState.GetType().ToString(), DropDownListOrderState.SelectedValue);
                addQueryItem(PageName, "DropDownListPushState", DropDownListPushState.GetType().ToString(), DropDownListPushState.SelectedValue);

            }
            else if ((DropDownListOrderState.SelectedValue == "3") && (DropDownListPushState.SelectedValue == "0"))
            {
                //订单手动关闭，明细全部
                condition = " totalstate='1' and totalcstate='1' AND detailstate<>'0' ";
                addQueryItem(PageName, "DropDownListOrderState", DropDownListOrderState.GetType().ToString(), DropDownListOrderState.SelectedValue);
                addQueryItem(PageName, "DropDownListPushState", DropDownListPushState.GetType().ToString(), DropDownListPushState.SelectedValue);

            }
            else if ((DropDownListOrderState.SelectedValue == "3") && (DropDownListPushState.SelectedValue == "1"))
            {
                //订单手动关闭，明细采购中
                condition = " totalstate='1' and totalcstate='1' AND (detailstate='1' or detailstate='3') and detailcstate='0'";
                addQueryItem(PageName, "DropDownListOrderState", DropDownListOrderState.GetType().ToString(), DropDownListOrderState.SelectedValue);
                addQueryItem(PageName, "DropDownListPushState", DropDownListPushState.GetType().ToString(), DropDownListPushState.SelectedValue);

            }
            else if ((DropDownListOrderState.SelectedValue == "3") && (DropDownListPushState.SelectedValue == "2"))
            {
                //订单手动关闭，明细采购完毕
                condition = " totalstate='1' and totalcstate='1' AND detailstate='2' and detailcstate='0'";
                addQueryItem(PageName, "DropDownListOrderState", DropDownListOrderState.GetType().ToString(), DropDownListOrderState.SelectedValue);
                addQueryItem(PageName, "DropDownListPushState", DropDownListPushState.GetType().ToString(), DropDownListPushState.SelectedValue);

            }
            else if ((DropDownListOrderState.SelectedValue == "3") && (DropDownListPushState.SelectedValue == "3"))
            {
                //订单手动关闭，明细手动关闭
                condition = " totalstate='1' and totalcstate='1' AND detailstate='1' and detailcstate='1'";
                addQueryItem(PageName, "DropDownListOrderState", DropDownListOrderState.GetType().ToString(), DropDownListOrderState.SelectedValue);
                addQueryItem(PageName, "DropDownListPushState", DropDownListPushState.GetType().ToString(), DropDownListPushState.SelectedValue);
            }
            //订单号条件
            if (TextBoxOrderCode.Text != "")
            {
                condition += " AND orderno LIKE '%" + TextBoxOrderCode.Text.Trim().PadLeft(8, '0') + "%'";
                addQueryItem(PageName, "TextBoxOrderCode", TextBoxOrderCode.GetType().ToString(), TextBoxOrderCode.Text.Trim());

            }
            //供应商条件
            if ((TextBoxSupplier.Text != "") && (condition != ""))
            {
                condition += " AND " + " suppliernm LIKE '%" + TextBoxSupplier.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxSupplier", TextBoxSupplier.GetType().ToString(), TextBoxSupplier.Text.Trim());
            }
            //部门条件
            if ((TextBoxDep.Text != "") && (condition != ""))
            {
                condition += " AND " + " depnm LIKE'%" + TextBoxDep.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxDep", TextBoxDep.GetType().ToString(), TextBoxDep.Text.Trim());
            }
            //业务员条件
            if ((TextBoxClerk.Text != "") && (condition != ""))
            {
                condition += " AND " + " ywynm LIKE'%" + TextBoxClerk.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxClerk", TextBoxClerk.GetType().ToString(), TextBoxClerk.Text.Trim());
            }
            //下单日期条件
            if ((TextBoxDate.Text != "") && (condition != ""))
            {
                condition += " AND " + " shtime LIKE '%" + TextBoxDate.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxDate", TextBoxDate.GetType().ToString(), TextBoxDate.Text.Trim());
            }
            //交货日期
            if ((TextBoxJhuo.Text != "") && (condition != ""))
            {
                condition += " AND " + " recdate LIKE '%" + TextBoxJhuo.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxJhuo", TextBoxJhuo.GetType().ToString(), TextBoxJhuo.Text.Trim());
            }
            //物料编码条件
            if ((TextBoxCode.Text != "") && (condition != ""))
            {
                condition += " AND " + " marid LIKE '%" + TextBoxCode.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxCode", TextBoxCode.GetType().ToString(), TextBoxCode.Text.Trim());

                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                if (DropDownListPlanno.SelectedValue != "1")
                {
                    QRcondition += " and marid like '%" + TextBoxCode.Text.Trim() + "%'";
                }
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            }
            //物料名称条件
            if ((TextBoxName.Text != "") && (condition != ""))
            {
                condition += " AND " + " marnm LIKE '%" + TextBoxName.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxName", TextBoxName.GetType().ToString(), TextBoxName.Text.Trim());

                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                if (DropDownListPlanno.SelectedValue != "1")
                {
                    QRcondition += " and marnm like '%" + TextBoxName.Text.Trim() + "%'";
                }
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            }
            //规格型号条件
            if ((TextBoxStandard.Text != "") && (condition != ""))
            {
                condition += " AND " + " margg LIKE '%" + TextBoxStandard.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxStandard", TextBoxStandard.GetType().ToString(), TextBoxStandard.Text.Trim());

                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                if (DropDownListPlanno.SelectedValue != "1")
                {
                    QRcondition += " and margg like '%" + TextBoxStandard.Text.Trim() + "%'";
                }
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            }
            //计划跟踪号条件
            if ((TextBoxPTC.Text != "") && (condition != ""))
            {
                condition += " AND patindex('%" + TextBoxPTC.Text.Trim() + "%',ptcode)>0";
                addQueryItem(PageName, "TextBoxPTC", TextBoxPTC.GetType().ToString(), TextBoxPTC.Text.Trim());
            }
            //材质
            if ((TextBoxcaizhi.Text != "") && (condition != ""))
            {
                condition += " AND " + " marcz LIKE '%" + TextBoxcaizhi.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxcaizhi", TextBoxcaizhi.GetType().ToString(), TextBoxcaizhi.Text.Trim());

                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                if (DropDownListPlanno.SelectedValue != "1")
                {
                    QRcondition += " and marcz like '%" + TextBoxcaizhi.Text.Trim() + "%'";
                }
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            }
            //国标
            if ((TextBoxgb.Text != "") && (condition != ""))
            {
                condition += " AND " + " margb LIKE '%" + TextBoxgb.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxgb", TextBoxgb.GetType().ToString(), TextBoxgb.Text.Trim());

                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                if (DropDownListPlanno.SelectedValue != "1")
                {
                    QRcondition += " and margb like '%" + TextBoxgb.Text.Trim() + "%'";
                }
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            }
            //订货数量
            if ((TextBoxDnum.Text != "") && (condition != ""))
            {
                condition += " AND " + " zxnum LIKE '%" + TextBoxDnum.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxDnum", TextBoxDnum.GetType().ToString(), TextBoxDnum.Text.Trim());
            }
            //到货数量
            if ((TextBoxAnum.Text != "") && (condition != ""))
            {
                condition += " AND " + " recgdnum LIKE '%" + TextBoxAnum.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxAnum", TextBoxAnum.GetType().ToString(), TextBoxAnum.Text.Trim());
            }
            //标识号
            if ((TextBoxBSHNUM.Text != "") && (condition != ""))
            {
                condition += " AND " + " PO_TUHAO LIKE '%" + TextBoxBSHNUM.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxBSHNUM", TextBoxBSHNUM.GetType().ToString(), TextBoxBSHNUM.Text.Trim());
            }
            //计划类型
            if ((TextBoxPlanTyple.Text != "") && (condition != ""))
            {
                condition += " AND " + " PO_MASHAPE LIKE '%" + TextBoxPlanTyple.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxPlanTyple", TextBoxPlanTyple.GetType().ToString(), TextBoxPlanTyple.Text.Trim());
            }
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            //是否入库（即是否生成入库单）
            if ((DropDownListQRInState.SelectedValue != "") && (condition != ""))
            {
                condition += " AND " + " QRIn_State='" + DropDownListQRInState.SelectedValue + "'";
                addQueryItem(PageName, "DropDownListQRInState", DropDownListQRInState.GetType().ToString(), DropDownListQRInState.SelectedValue);

                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                QRcondition += " and QRIn_State='" + DropDownListQRInState.SelectedValue + "'";
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            }
            if ((DropDownListQRInState.SelectedValue == "") && (condition != ""))
            {
                condition += " AND " + " (QRIn_State='0' or QRIn_State='1')";
                addQueryItem(PageName, "DropDownListQRInState", DropDownListQRInState.GetType().ToString(), DropDownListQRInState.SelectedValue);
            }
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            //质检结果
            if ((DropDownListQCResult.SelectedValue != "all") && (condition != ""))
            {
                if (DropDownListQCResult.SelectedValue.ToString().Trim() == "合格")
                {
                    condition += " AND " + " (RESULT='" + DropDownListQCResult.SelectedValue + "' or RESULT='免检')";
                }
                else
                {
                    condition += " AND " + " RESULT='" + DropDownListQCResult.SelectedValue + "'";
                }
            }
            if (DropDownListPlanno.SelectedValue == "1")
            {
                condition = condition;
            }
            else
            {
                condition = condition + " or ((QRIn_PTC is null or QRIn_PTC='')" + QRcondition + ")";
            }
            return condition;
        }

        private void addQueryItem(string pageName, string controlID, string controlType, string controlValue)
        {

            QueryItemInfo queryItem = new QueryItemInfo(Session["UserID"].ToString(), pageName, controlID, controlType, controlValue);

            queryItems.Add(queryItem);

        }

        protected string ConvertOS(string str)
        {
            switch (str)
            {
                case "0": return "未提交";
                case "1": return "采购中...";
                case "2": return "已关闭";
                case "3": return "已完成";
                default: return str;
            }
        }

        protected string ConvertLS(string str)
        {
            switch (str)
            {
                case "0": return "未提交";
                case "1": return "采购中...";
                case "2": return "已入库";
                case "3": return "未入库";
                case "4": return "手动关闭";
                default: return str;
            }
        }
        double dhnum = 0;
        double tohnum = 0;
        double dingenumhj = 0;
        double fzhnum = 0;
        double jine = 0;
        double taxjine = 0;
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("LabelResult")).Text.Trim() != "未报检")
                {
                    string key = string.Empty;

                    string ptc = ((Label)e.Item.FindControl("LabelPTC")).Text.Trim();

                    string sqlstr = "select AFI_ID from TBQM_APLYFORINSPCT  where UNIQUEID=(select top 1 UNIQUEID from  TBQM_APLYFORITEM where PTC='" + ptc + "' order by id desc)";

                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlstr);

                    if (dr.Read())
                    {
                        key = dr["AFI_ID"].ToString();
                    }

                    dr.Close();

                    ((Label)e.Item.FindControl("LabelResult")).Attributes.Add("onClick", "ShowResultModal('" + key + "');");
                    if ((((Label)e.Item.FindControl("LabelResult")).Text.Trim() == "合格" || ((Label)e.Item.FindControl("LabelResult")).Text == "免检" || ((Label)e.Item.FindControl("LabelResult")).Text.Trim() == "让步接收") && ((Label)e.Item.FindControl("LabelPushState")).Text.Trim() != "采购完成")
                    {
                        ((CheckBox)e.Item.FindControl("CheckBox1")).BackColor = System.Drawing.Color.Red;
                    }
                }
                if (((Label)e.Item.FindControl("LabelNumber")).Text.Trim() != string.Empty)
                {
                    dhnum += Convert.ToDouble(((Label)e.Item.FindControl("LabelNumber")).Text);
                }
                if (((Label)e.Item.FindControl("LabelArrivedNumber")).Text.Trim() != string.Empty)
                {
                    tohnum += Convert.ToDouble(((Label)e.Item.FindControl("LabelArrivedNumber")).Text);
                }
                if (((Label)e.Item.FindControl("lbdingenum")).Text.Trim() != string.Empty)
                {
                    dingenumhj += Convert.ToDouble(((Label)e.Item.FindControl("lbdingenum")).Text);
                }
                if (((Label)e.Item.FindControl("LabelQUANTITY")).Text.Trim() != string.Empty)
                {
                    fzhnum += Convert.ToDouble(((Label)e.Item.FindControl("LabelQUANTITY")).Text);
                }
                if (((Label)e.Item.FindControl("LabelAmount")).Text.Trim() != string.Empty)
                {
                    jine += Convert.ToDouble(((Label)e.Item.FindControl("LabelAmount")).Text);
                }
                if (((Label)e.Item.FindControl("LabelCTA")).Text.Trim() != string.Empty)
                {
                    taxjine += Convert.ToDouble(((Label)e.Item.FindControl("LabelCTA")).Text);
                }


                //到货数量小于订货数量
                if (((Label)e.Item.FindControl("LabelArrivedNumber")).Text.Trim() != string.Empty && ((Label)e.Item.FindControl("LabelArrivedNumber")).Text.Trim() != "0" && (CommonFun.ComTryDecimal(((Label)e.Item.FindControl("LabelArrivedNumber")).Text.Trim()) < CommonFun.ComTryDecimal(((Label)e.Item.FindControl("LabelNumber")).Text.Trim())))
                {
                    ((Label)e.Item.FindControl("LabelPTC")).BackColor = System.Drawing.Color.Yellow;
                }

                //作过物料减少
                string purchangeptc = ((Label)e.Item.FindControl("LabelPTC")).Text.Trim();
                string sqlpurchange = "select * from TBPC_BG where BG_PTC='" + purchangeptc + "'";
                System.Data.DataTable dtpurchange = DBCallCommon.GetDTUsingSqlText(sqlpurchange);
                if (dtpurchange.Rows.Count > 0)
                {
                    ((Label)e.Item.FindControl("lbpurchangestate")).Visible = true;
                    ((Label)e.Item.FindControl("lbpurchangestate")).BackColor = System.Drawing.Color.Red;
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                DataTable tb = (DataTable)Repeater1.DataSource;
                string count = tb.Rows.Count.ToString();
                ((Label)e.Item.FindControl("LabelCount")).Text = count;

                ((Label)e.Item.FindControl("Labeldhnum")).Text = Math.Round(dhnum, 4).ToString();
                ((Label)e.Item.FindControl("Labeltohnum")).Text = Math.Round(tohnum, 4).ToString();

                ((Label)e.Item.FindControl("lbdingenumhj")).Text = Math.Round(dingenumhj, 4).ToString();

                ((Label)e.Item.FindControl("Labelfzhnum")).Text = Math.Round(fzhnum, 4).ToString();
                ((Label)e.Item.FindControl("Labeljine")).Text = Math.Round(jine, 2).ToString();
                ((Label)e.Item.FindControl("Labeltaxjine")).Text = Math.Round(taxjine, 2).ToString();

            }
        }
        protected void Query_Click(object sender, EventArgs e)
        {


            LabelMessage.Text = "查询完毕";
            int count = 100;
            try
            {
                count = Convert.ToInt32(TextBoxCount.Text.Trim() == string.Empty ? "100" : TextBoxCount.Text.Trim());
            }
            catch
            {
                count = 100;
            }

            ObjPageSize = count;

            getOrderInfo(true, true);

            ModalPopupExtenderSearch.Hide();
        }
        //关闭
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();

        }
        //重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearCondition();
        }
        //清除条件
        private void clearCondition()
        {
            //领料单编号
            TextBoxOrderCode.Text = string.Empty;
            //加工单位
            TextBoxSupplier.Text = string.Empty;
            TextBoxClerk.Text = string.Empty;
            TextBoxDate.Text = string.Empty;
            TextBoxCode.Text = string.Empty;
            TextBoxName.Text = string.Empty;
            TextBoxStandard.Text = string.Empty;
            TextBoxPTC.Text = string.Empty;
            TextBoxAnum.Text = string.Empty;
            TextBoxBSHNUM.Text = string.Empty;
            TextBoxcaizhi.Text = string.Empty;
            TextBoxJhuo.Text = string.Empty;
            TextBoxPlanTyple.Text = string.Empty;
            TextBoxDep.Text = string.Empty;
            TextBoxDnum.Text = string.Empty;
            TextBoxgb.Text = string.Empty;

            DropDownListOrderState.ClearSelection();
            DropDownListOrderState.Items[1].Selected = true;

            DropDownListPushState.ClearSelection();
            DropDownListPushState.Items[1].Selected = true;

            DropDownListFatherLogic.ClearSelection();
            DropDownListFatherLogic.Items[0].Selected = true;

            DropDownListQCResult.ClearSelection();
            DropDownListQCResult.Items[0].Selected = true;

        }

        protected void Refresh_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.PathInfo);
        }


        //下推物料
        //需要更改的内容：1.参数传递增加InType,InType=0时，普通入库；InType=1时，扫码入库。
        protected void Push_Click(object sender, EventArgs e)
        {
            List<string> sqllist = new List<string>();

            //将所有的都初始化

            string sql = "UPDATE TBPC_PURORDERDETAIL SET PO_WHSTATE='1' WHERE PO_WHSTATE='BLUE" + Session["UserID"].ToString() + "'";

            sqllist.Add(sql);

            int count = 0;

            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            List<string> ptclist = new List<string>();
            int PTCcount = 0;
            int Dataccount = 0;
            int noPTCcount = 0;
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

            string suppliercode = "";

            string ptc = "";

            string ordercode = "";
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            string sqlQRIn = "";
            string QRIn_ID = "";
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

            for (int i = 0; i < Repeater1.Items.Count; i++)
            {

                if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    if (count == 0)
                    {
                        suppliercode = ((Label)Repeater1.Items[i].FindControl("LabelSupplierCode")).Text;
                    }
                    else
                    {
                        if (((Label)Repeater1.Items[i].FindControl("LabelSupplierCode")).Text != suppliercode)
                        {
                            string alert = "<script>alert('存在不同供应商，下推被阻止！')</script>";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);

                            //return; 表示语句跳转到函数末尾

                            return;
                        }
                    }

                    if ((((Label)Repeater1.Items[i].FindControl("LabelOS")).Text != "1") || ((((Label)Repeater1.Items[i].FindControl("LabelPS")).Text != "1") && (((Label)Repeater1.Items[i].FindControl("LabelPS")).Text != "3")))
                    {
                        //订单关闭，明细被关闭
                        string alert = "<script>alert('存在已完成或关闭条目，下推被阻止！')</script>";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                        return;
                    }
                    if ((((Label)Repeater1.Items[i].FindControl("LabelResult")).Text != "合格" && ((Label)Repeater1.Items[i].FindControl("LabelResult")).Text != "免检" && ((Label)Repeater1.Items[i].FindControl("LabelResult")).Text != "让步接收"))
                    {
                        string alert = "<script>alert('存在未质检合格的条目，下推被阻止！')</script>";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                        return;
                    }
                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    if (((Label)Repeater1.Items[i].FindControl("LabelQRIn_State")).Text == "已入库" || ((Label)Repeater1.Items[i].FindControl("LabelQRIn_State")).Text == "其他")
                    {
                        string alert = "<script>alert('存在已入库或未分配项目的条目，下推被阻止！')</script>";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                        return;
                    }
                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    suppliercode = ((Label)Repeater1.Items[i].FindControl("LabelSupplierCode")).Text;

                    ptc = ((Label)Repeater1.Items[i].FindControl("LabelPTC")).Text;

                    ordercode = ((Label)Repeater1.Items[i].FindControl("LabelCode")).Text;

                    //判断是否已经下推过，除型材以外(01.07)，有色金属01.14 不用判断
                    string materailcode = ((Label)Repeater1.Items[i].FindControl("LabelMaterialCode")).Text;//物料编码
                    double ordernum = 0;//订货数量
                    ordernum = Convert.ToDouble(((Label)Repeater1.Items[i].FindControl("LabelNumber")).Text); //订单数量
                    if ((materailcode.Substring(0, 5) != "01.07") || (materailcode.Substring(0, 5) != "01.14"))//非型材或者金属,只判断与订单数量一样时
                    {
                        string sqltext = "select isnull(sum(WG_RSNUM),0) from  tbws_indetail where WG_ORDERID='" + ordercode + "' and WG_PTCODE='" + ptc + "'";
                        double rsnum = 0;
                        DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqltext);
                        rsnum = Convert.ToDouble(dt0.Rows[0][0].ToString()); //入库单入库数量                         
                        if ((ordernum == rsnum) && (rsnum != 0)) //入库数量与订单数量比较
                        {
                            string alert = "<script>alert('下推计划跟踪号为(" + ptc + ")的物料已经在入库单中，请您检查入库单数据！')</script>";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                            return;
                        }
                    }

                    sql = "UPDATE TBPC_PURORDERDETAIL SET PO_WHSTATE='BLUE" + Session["UserID"].ToString() + "' WHERE PO_PTCODE='" + ptc + "' and PO_CODE='" + ordercode + "'";

                    //更新状态
                    //采购订单详细表中，计划跟踪号唯一

                    sqllist.Add(sql);
                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

                    if (ptc == "")
                    {
                        noPTCcount += 1;
                    }
                    Dataccount += 1;
                    if (!ptclist.Contains(ptc))
                    {
                        ptclist.Add(ptc);
                    }

                    QRIn_ID = ((Label)Repeater1.Items[i].FindControl("QRIn_ID2")).Text;
                    sqlQRIn = "update midTable_QRIn set QRIn_WHSTATE='1' where CONVERT(varchar(20),QRIn_ID)='" + QRIn_ID + "'";
                    sqllist.Add(sqlQRIn);
                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

                    count++;
                }
            }
            if (sqllist.Count < 2)
            {
                string alert = "<script>alert('请选择要下推的条目！')</script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                return;
            }
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            if (noPTCcount > 0)
            {
                string alert2 = "<script>alert('有选项未分配计划跟踪号，请分配后再下推！')</script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert2, false);
                return;
            }
            PTCcount = ptclist.Count;
            if (Dataccount > PTCcount)
            {
                string alert2 = "<script>alert('存在计划跟踪号相同的项，请分别下推！')</script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert2, false);
                return;
            }
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            DBCallCommon.ExecuteTrans(sqllist);
            Response.Redirect("~/SM_Data/SM_WarehouseIN_WG.aspx?FLAG=PUSHBLUE&&ID=NEW");
        }

        private void BindItem()
        {

            for (int i = 0; i < (Repeater1.Items.Count - 1); i++)
            {

                Label lbCode = (Repeater1.Items[i].FindControl("LabelCode") as Label);

                string NextCode = lbCode.Text;

                if (lbCode.Visible)
                {
                    for (int j = i + 1; j < Repeater1.Items.Count; j++)
                    {
                        string Code = (Repeater1.Items[j].FindControl("LabelCode") as Label).Text;

                        if (NextCode == Code)
                        {
                            (Repeater1.Items[j].FindControl("LabelSupplier") as Label).Visible = false;
                            (Repeater1.Items[j].FindControl("LabelCode") as Label).Style.Add("display", "none");
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        protected void BtnShowExport_Click(object sender, EventArgs e) // Type=7为订单导出
        {
            string condition = GetCondition().Replace("'", "^");
            List<string> sqllist = new List<string>();
            string sql = "delete from TBWS_EXPORTCONDITION where SessionID='" + Session["UserID"].ToString() + "' AND Type='7'";
            sqllist.Add(sql);
            sql = "insert into TBWS_EXPORTCONDITION (SessionID,Type,StrCondition) VALUES ('" + Session["UserID"].ToString() + "','7','" + condition + "')";
            sqllist.Add(sql);
            DBCallCommon.ExecuteTrans(sqllist);

            ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>orderexport();</script>");
        }
    }
}
