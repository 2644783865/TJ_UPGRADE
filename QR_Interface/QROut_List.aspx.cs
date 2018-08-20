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
    public partial class QROut_List : System.Web.UI.Page
    {
        PagerQueryParamGroupBy pager = new PagerQueryParamGroupBy();
        private List<QueryItemInfo> queryItems = new List<QueryItemInfo>();
        protected string PageName
        {
            get
            {
                return "SM_Warehouse_Query.aspx";
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
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);

            if (!IsPostBack)
            {
                InitControl();

                this.Form.DefaultButton = QueryButton.UniqueID;
                GetStorageInfo(true, false);//第一次加载

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
            else if (type == "System.Web.UI.WebControls.DropDownList")
            {
                if (controlID == "ChildWarehouseDropDownList")
                {
                    GetChildWarehouse();
                }
                else if (controlID == "SubTypeDropDownList")
                {
                    GetSubType();
                }

                (this.UpdatePanelCondition.FindControl(controlID) as System.Web.UI.WebControls.DropDownList).ClearSelection();
                (this.UpdatePanelCondition.FindControl(controlID) as System.Web.UI.WebControls.DropDownList).SelectedValue = controlValue;

            }
        }

        private void InitControl()
        {
            GetWarehouse();
            GetChildWarehouse();
            GetMaType();
            GetSubType();
            DateTextBox.Text = DateTime.Now.Date.ToString("yyyy-MM-dd");

            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            //string flag = Request.QueryString["FLAG"].ToString();
            string flag = "";
            try
            {
                flag = Request.QueryString["FLAG"].ToString();
            }
            catch
            {
                flag = "";
            }
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

            if (flag == "QUERY")
            {
                Close.Visible = false;
                bindControlOut(flag);

            }
            if (flag == "APPENDOUT" || flag == "APPENDOUTWW" || flag == "APPENDOUTXS" || flag == "APPENDMTO" || flag == "APPENDAL" || flag == "APPENDPROJTEMP" || flag == "APPENDOUTQT")
            {
                bindControlOut(flag);
            }
            if (flag == "PUSHLLOUT" || flag == "PUSHWWOUT" || flag == "PUSHXSOUT" || flag == "PUSHAL" || flag == "PUSHMTO" || flag == "PUSHPROJTEMP" || flag == "PUSHQTOUT")
            {
                bindControlOut(flag);
            }
        }

        private void bindControlOut(string flag)
        {
            switch (flag)
            {
                case "PUSHLLOUT":
                    btn_llout.Visible = true;
                    break;
                case "PUSHWWOUT":
                    btn_llout.Visible = false;
                    break;
                case "PUSHXSOUT":
                    btn_llout.Visible = false;
                    break;
                case "PUSHAL":
                    btn_llout.Visible = false;
                    break;
                case "PUSHMTO":
                    btn_llout.Visible = false;
                    break;
                case "PUSHPROJTEMP":
                    btn_llout.Visible = false;
                    break;
                case "PUSHQTOUT":
                    btn_llout.Visible = false;
                    break;
                default:
                    btn_llout.Visible = false;
                    break;
            }
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetStorageInfo(false, false);
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
            DataTable dt = CommonFun.GetDataByPagerQueryParamGroupBy(pager);
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
            //clearData();
        }


        //清理数据
        private void clearData()
        {
            string clearsql = "delete from TBWS_STORAGETEMP where SQ_OPERSTATE='Query" + Session["UserID"].ToString() + "'";
            DBCallCommon.ExeSqlText(clearsql);
        }

        private void GetWarehouse()
        {
            string sql = "SELECT DISTINCT WS_ID,WS_NAME FROM TBWS_WAREHOUSE WHERE WS_FATHERID='ROOT' ORDER BY WS_ID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            WarehouseDropDownList.DataSource = dt;
            WarehouseDropDownList.DataTextField = "WS_NAME";
            WarehouseDropDownList.DataValueField = "WS_ID";
            WarehouseDropDownList.DataBind();
            ListItem item = new ListItem("--请选择--", "0");
            WarehouseDropDownList.Items.Insert(0, item);
        }

        private void GetChildWarehouse()
        {
            if (WarehouseDropDownList.SelectedItem.Text != "--请选择--")
            {
                string sql = "SELECT DISTINCT WS_ID,WS_NAME FROM TBWS_WAREHOUSE WHERE WS_FATHERID='" + WarehouseDropDownList.SelectedItem.Value.ToString() + "' ORDER BY WS_ID";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                ChildWarehouseDropDownList.DataSource = dt;
                ChildWarehouseDropDownList.DataTextField = "WS_NAME";
                ChildWarehouseDropDownList.DataValueField = "WS_ID";
                ChildWarehouseDropDownList.DataBind();
            }
            else
            {
                ChildWarehouseDropDownList.Items.Clear();
            }
            ListItem item = new ListItem("--请选择--", "0");
            ChildWarehouseDropDownList.Items.Insert(0, item);
        }

        private void GetMaType()
        {
            string sql = "SELECT DISTINCT TY_ID,TY_NAME FROM TBMA_TYPEINFO WHERE TY_FATHERID='ROOT' ORDER BY TY_ID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            TypeDropDownList.DataSource = dt;
            TypeDropDownList.DataTextField = "TY_NAME";
            TypeDropDownList.DataValueField = "TY_ID";
            TypeDropDownList.DataBind();
            ListItem item = new ListItem("--请选择--", "0");
            TypeDropDownList.Items.Insert(0, item);
        }

        private void GetSubType()
        {
            if (TypeDropDownList.SelectedItem.Text != "--请选择--")
            {
                string sql = "SELECT DISTINCT TY_ID,TY_NAME FROM TBMA_TYPEINFO WHERE TY_FATHERID='" + TypeDropDownList.SelectedValue + "' ORDER BY TY_ID";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                SubTypeDropDownList.DataSource = dt;
                SubTypeDropDownList.DataTextField = "TY_NAME";
                SubTypeDropDownList.DataValueField = "TY_ID";
                SubTypeDropDownList.DataBind();
            }
            else
            {
                SubTypeDropDownList.Items.Clear();
            }
            ListItem item = new ListItem("--请选择--", "0");
            SubTypeDropDownList.Items.Insert(0, item);
        }

        protected void WarehouseDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetChildWarehouse();
        }

        protected void TypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetSubType();
        }

        protected void QueryButton_Click(object sender, EventArgs e)
        {
            int count = 50;
            try
            {
                count = Convert.ToInt32(TextBoxCount.Text.Trim() == string.Empty ? "50" : TextBoxCount.Text.Trim());
            }
            catch
            {
                count = 50;
            }

            ObjPageSize = count;

            //查询库存物料物料
            GetStorageInfo(true, true);
            ModalPopupExtenderSearch.Hide();
        }

        //重置条件
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearCondition();
        }


        //取消

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }


        /// <summary>
        /// 质检状态的查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            int count = 50;
            try
            {
                count = Convert.ToInt32(TextBoxCount.Text.Trim() == string.Empty ? "50" : TextBoxCount.Text.Trim());
            }
            catch
            {
                count = 50;
            }

            ObjPageSize = count;

            //查询库存物料物料
            GetStorageInfo(true, false);

        }

        //导出
        //库存=0；入库单=1；结转备库=2；领料单=3；

        protected void BtnShowExport_Click(object sender, EventArgs e)
        {
            string ordertype = string.Empty;

            string condition = GetStrCondition().Replace("'", "^");
            List<string> sqllist = new List<string>();
            string sql = "delete from TBWS_EXPORTCONDITION where SessionID='" + Session["UserID"].ToString() + "' AND Type='0'";
            sqllist.Add(sql);
            sql = "insert into TBWS_EXPORTCONDITION (SessionID,Type,StrCondition) VALUES ('" + Session["UserID"].ToString() + "','0','" + condition + "')";
            sqllist.Add(sql);
            DBCallCommon.ExecuteTrans(sqllist);

            string type = "0";

            if (Convert.ToDateTime(DateTextBox.Text) < DateTime.Now.Date)
            {
                type = "1";
            }
            else if (Convert.ToDateTime(DateTextBox.Text) >= DateTime.Now.Date)
            {
                type = "0";
            }


            ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>storageexport(" + type + "," + Server.UrlEncode(DateTextBox.Text.Trim()) + ");</script>");
        }

        //清除条件
        private void clearCondition()
        {
            WarehouseDropDownList.ClearSelection();
            WarehouseDropDownList.Items[0].Selected = true;

            ChildWarehouseDropDownList.ClearSelection();
            ChildWarehouseDropDownList.Items[0].Selected = true;

            TypeDropDownList.ClearSelection();
            TypeDropDownList.Items[0].Selected = true;

            SubTypeDropDownList.ClearSelection();
            SubTypeDropDownList.Items[0].Selected = true;

            DateTextBox.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

            TextBoxCode.Text = string.Empty;//物料代码
            TextBoxName.Text = string.Empty;//物料名称
            TextBoxPTC.Text = string.Empty;//计划跟踪号
            TextBoxNO.Text = string.Empty;  //标识号
            TextBoxLotNumber.Text = string.Empty;//批号
            TextBoxLength.Text = string.Empty;//长
            TextBoxWidth.Text = string.Empty;//宽
            TextBoxAttribute.Text = string.Empty;//材质
            TextBoxStandard.Text = string.Empty;//规格型号
            TextBoxFixed.Text = string.Empty;//是否定尺
            TextBoxNote.Text = string.Empty; //备注查询
            TextBoxOrderCode.Text = string.Empty;//订单编号
        }

        private string GetStrCondition()
        {
            string condition = "1=1";
            string QRcondition = "";
            //以下语句为准备查询条件
            //物料代码条件
            if (TextBoxCode.Text.Trim() != "")
            {
                condition = " QROut_MatCode LIKE '%" + TextBoxCode.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxCode", TextBoxCode.GetType().ToString(), TextBoxCode.Text.Trim());

                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                if (DropDownListPlanno.SelectedValue != "1")
                {
                    QRcondition += " and QROut_MatCode like '%" + TextBoxCode.Text.Trim() + "%'";
                }
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            }

            //物料名称条件
            if ((TextBoxName.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " MaterialName LIKE '%" + TextBoxName.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxName", TextBoxName.GetType().ToString(), TextBoxName.Text.Trim());
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                if (DropDownListPlanno.SelectedValue != "1")
                {
                    QRcondition += " and MaterialName like '%" + TextBoxName.Text.Trim() + "%'";
                }
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            }

            //标识号
            if ((TextBoxNO.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " CGMODE LIKE '%" + TextBoxNO.Text.Trim().ToUpper() + "%'";
                addQueryItem(PageName, "TextBoxNO", TextBoxNO.GetType().ToString(), TextBoxNO.Text.Trim());

            }
            //计划跟踪号条件
            if ((TextBoxPTC.Text.Trim() != "") && (condition != ""))
            {
                if (TextBoxPTC.Text.Trim() == "备库")
                {
                    condition += " AND " + " (PTC LIKE '%备库%' or PTC LIKE '%BEIKU%')";
                    addQueryItem(PageName, "TextBoxPTC", TextBoxPTC.GetType().ToString(), TextBoxPTC.Text.Trim());
                }
                else
                {
                    condition += " AND " + " PTC LIKE'%" + TextBoxPTC.Text.Trim() + "%'";
                    addQueryItem(PageName, "TextBoxPTC", TextBoxPTC.GetType().ToString(), TextBoxPTC.Text.Trim());
                }
            }
            //批号条件
            if ((TextBoxLotNumber.Text != "") && (condition != ""))
            {
                condition += " AND " + " PTC LIKE '%" + TextBoxLotNumber.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxLotNumber", TextBoxLotNumber.GetType().ToString(), TextBoxLotNumber.Text.Trim());

            }
            //型号规格条件
            if ((TextBoxStandard.Text != "") && (condition != ""))
            {
                condition += " AND " + " Standard LIKE '%" + TextBoxStandard.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxStandard", TextBoxStandard.GetType().ToString(), TextBoxStandard.Text.Trim());
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                if (DropDownListPlanno.SelectedValue != "1")
                {
                    QRcondition += " and Standard like '%" + TextBoxStandard.Text.Trim() + "%'";
                }
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            }
            //长度条件
            if ((TextBoxLength.Text != "") && (condition != ""))
            {
                condition += " AND " + " Length LIKE '%" + TextBoxLength.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxLength", TextBoxLength.GetType().ToString(), TextBoxLength.Text.Trim());

            }
            //宽度条件
            if ((TextBoxWidth.Text != "") && (condition != ""))
            {
                condition += " AND " + " Width LIKE '%" + TextBoxWidth.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxWidth", TextBoxWidth.GetType().ToString(), TextBoxWidth.Text.Trim());

            }
            //材质条件
            if ((TextBoxAttribute.Text != "") && (condition != ""))
            {
                condition += " AND " + " Attribute LIKE '%" + TextBoxAttribute.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxAttribute", TextBoxAttribute.GetType().ToString(), TextBoxAttribute.Text.Trim());
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                if (DropDownListPlanno.SelectedValue != "1")
                {
                    QRcondition += " and Attribute like '%" + TextBoxAttribute.Text.Trim() + "%'";
                }
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            }
            //是否定尺
            if ((TextBoxFixed.Text != "") && (condition != ""))
            {
                condition += " AND " + " Fixed LIKE '%" + TextBoxFixed.Text.Trim().ToUpper() + "%'";
                addQueryItem(PageName, "TextBoxFixed", TextBoxFixed.GetType().ToString(), TextBoxFixed.Text.Trim());

            }
            //备注条件
            if ((TextBoxNote.Text != "") && (condition != ""))
            {
                condition += " AND " + " Note LIKE '%" + TextBoxNote.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxNote", TextBoxNote.GetType().ToString(), TextBoxNote.Text.Trim());

            }

            //订单编号条件
            if ((TextBoxOrderCode.Text != "") && (condition != ""))
            {
                condition += " AND " + " OrderCode LIKE '%" + TextBoxOrderCode.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxOrderCode", TextBoxOrderCode.GetType().ToString(), TextBoxOrderCode.Text.Trim());

            }


            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            //是否出库（即是否生成入库单）
            if ((DropDownListQROutState.SelectedValue != "") && (condition != ""))
            {
                condition += " AND " + " QROut_State='" + DropDownListQROutState.SelectedValue + "'";
                addQueryItem(PageName, "DropDownListQROutState", DropDownListQROutState.GetType().ToString(), DropDownListQROutState.SelectedValue);

                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                QRcondition += " and QROut_State='" + DropDownListQROutState.SelectedValue + "'";
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            }
            if ((DropDownListQROutState.SelectedValue == "") && (condition != ""))
            {
                condition += " AND " + " (QROut_State='0' or QROut_State='1')";
                addQueryItem(PageName, "DropDownListQROutState", DropDownListQROutState.GetType().ToString(), DropDownListQROutState.SelectedValue);
            }
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

            //仓库条件
            if ((WarehouseDropDownList.SelectedValue != "0") && (ChildWarehouseDropDownList.SelectedValue != "0"))
            {
                if (condition != "")
                {
                    condition += " AND " + " WarehouseCode='" + ChildWarehouseDropDownList.SelectedValue + "'";
                    addQueryItem(PageName, "WarehouseDropDownList", WarehouseDropDownList.GetType().ToString(), WarehouseDropDownList.SelectedValue);
                    addQueryItem(PageName, "ChildWarehouseDropDownList", ChildWarehouseDropDownList.GetType().ToString(), ChildWarehouseDropDownList.SelectedValue);

                }
                else
                {
                    condition += " WarehouseCode='" + ChildWarehouseDropDownList.SelectedValue + "'";
                    addQueryItem(PageName, "WarehouseDropDownList", WarehouseDropDownList.GetType().ToString(), WarehouseDropDownList.SelectedValue);
                    addQueryItem(PageName, "ChildWarehouseDropDownList", ChildWarehouseDropDownList.GetType().ToString(), ChildWarehouseDropDownList.SelectedValue);


                }
            }
            else if ((WarehouseDropDownList.SelectedValue != "0") && (ChildWarehouseDropDownList.SelectedValue == "0"))
            {
                if (condition != "")
                {
                    condition += " AND " + " WarehouseCode LIKE '" + WarehouseDropDownList.SelectedValue + "%'";
                    addQueryItem(PageName, "WarehouseDropDownList", WarehouseDropDownList.GetType().ToString(), WarehouseDropDownList.SelectedValue);
                    addQueryItem(PageName, "ChildWarehouseDropDownList", ChildWarehouseDropDownList.GetType().ToString(), ChildWarehouseDropDownList.SelectedValue);
                }
                else
                {
                    condition += " WarehouseCode LIKE '" + WarehouseDropDownList.SelectedValue + "%'";
                    addQueryItem(PageName, "WarehouseDropDownList", WarehouseDropDownList.GetType().ToString(), WarehouseDropDownList.SelectedValue);
                    addQueryItem(PageName, "ChildWarehouseDropDownList", ChildWarehouseDropDownList.GetType().ToString(), ChildWarehouseDropDownList.SelectedValue);
                }
            }

            //物料条件
            if ((TypeDropDownList.SelectedValue != "0") && (SubTypeDropDownList.SelectedValue != "0"))
            {
                if (condition != "")
                {
                    condition += " AND " + " QROut_MatCode LIKE '" + SubTypeDropDownList.SelectedValue + "%'";
                    addQueryItem(PageName, "TypeDropDownList", TypeDropDownList.GetType().ToString(), TypeDropDownList.SelectedValue);
                    addQueryItem(PageName, "SubTypeDropDownList", SubTypeDropDownList.GetType().ToString(), SubTypeDropDownList.SelectedValue);
                }
                else
                {
                    condition += " QROut_MatCode LIKE '" + SubTypeDropDownList.SelectedValue + "%'";
                    addQueryItem(PageName, "TypeDropDownList", TypeDropDownList.GetType().ToString(), TypeDropDownList.SelectedValue);
                    addQueryItem(PageName, "SubTypeDropDownList", SubTypeDropDownList.GetType().ToString(), SubTypeDropDownList.SelectedValue);
                }
            }
            else if ((TypeDropDownList.SelectedValue != "0") && (SubTypeDropDownList.SelectedValue == "0"))
            {
                if (condition != "")
                {
                    condition += " AND " + " QROut_MatCode LIKE '" + TypeDropDownList.SelectedValue + "%'";
                    addQueryItem(PageName, "TypeDropDownList", TypeDropDownList.GetType().ToString(), TypeDropDownList.SelectedValue);
                    addQueryItem(PageName, "SubTypeDropDownList", SubTypeDropDownList.GetType().ToString(), SubTypeDropDownList.SelectedValue);
                }
                else
                {
                    condition += " QROut_MatCode LIKE '" + TypeDropDownList.SelectedValue + "%'";
                    addQueryItem(PageName, "TypeDropDownList", TypeDropDownList.GetType().ToString(), TypeDropDownList.SelectedValue);
                    addQueryItem(PageName, "SubTypeDropDownList", SubTypeDropDownList.GetType().ToString(), SubTypeDropDownList.SelectedValue);
                }
            }
            if (DropDownListPlanno.SelectedValue == "1")
            {
                condition = condition;
            }
            else
            {
                condition = condition + " or ((PTC is null or PTC='')" + QRcondition + ")";
            }
            return condition;

        }

        private void addQueryItem(string pageName, string controlID, string controlType, string controlValue)
        {

            QueryItemInfo queryItem = new QueryItemInfo(Session["UserID"].ToString(), pageName, controlID, controlType, controlValue);

            queryItems.Add(queryItem);

        }

        //根据条件获取物料信息的函数，本页面关键功能！
        protected void GetStorageInfo(bool isFirstPage, bool isUpdateCondition)
        {

            string condition = GetStrCondition();
            if (Convert.ToDateTime(DateTextBox.Text) == DateTime.Now.Date)
            {

                string TableName = "(select * from midTable_QROut as c left join (select ID as MA_ID,MNAME as MA_MNAME,GUIGE as MA_GUIGE,CAIZHI as MA_CAIZHI,GB as MA_GB from TBMA_MATERIAL) as m on c.QROut_MatCode=m.MA_ID left join View_SM_Storage as a on a.SQCODE=c.QROut_SQCODE left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as b on a.PTC=b.PTCFrom and a.MaterialCode=b.TzMaterialCode)t"; 

                string PrimaryKey = "SQCODE";

                string ShowFields = "SQCODE,MaterialCode,QROut_MatCode,case when MaterialName is null then MA_MNAME else MaterialName end AS MaterialName,case when Attribute is null then MA_CAIZHI else Attribute end AS Attribute,case when GB is null then MA_GB else GB end AS GB,case when Standard is null then MA_GUIGE else Standard end AS MaterialStandard,Fixed,Length,Width,LotNumber,PlanMode,PTC,WarehouseCode,Warehouse,LocationCode AS PositionCode,Location AS Position,Unit,cast(Number as float) AS Number,cast(SupportNumber as float) AS Quantity,Note AS Comment,CGMODE,OrderCode,OUTTZNUM,OUTTZFZNUM,QROut_ID,QROut_SQCODE,QROut_TaskID,QROut_Num,QROut_Time,(case when QROut_State='0' then '未出库' when QROut_State='1' then '已出库' else '其他' end) as QROut_State,QROut_WHSTATE,QROut_Note";
                //数据库中的主键
                string OrderField = "PTC,QROut_MatCode";

                string GroupField = "";

                int OrderType = 0;
                /**/
                string StrWhere = condition;

                int PageSize = ObjPageSize;

                GetTotalNum(StrWhere, "(select * from midTable_QROut as c left join (select ID as MA_ID,MNAME as MA_MNAME,GUIGE as MA_GUIGE,CAIZHI as MA_CAIZHI,GB as MA_GB from TBMA_MATERIAL) as m on c.QROut_MatCode=m.MA_ID left join View_SM_Storage as a on a.SQCODE=c.QROut_SQCODE left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as b on a.PTC=b.PTCFrom and a.MaterialCode=b.TzMaterialCode)t");

                BindPage(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFirstPage);
            }
        }


        private void GetTotalNum(string strWhere, string tableName)
        {
            string sql = "";
            if (string.IsNullOrEmpty(strWhere))
            {
                sql = "select isnull(cast(round(sum(Number),4) as float),0) as TotalNum,isnull(cast(round(sum(SupportNumber),4) as float),0) as TotalSNum from " + tableName;
            }
            else
            {
                sql = "select isnull(cast(round(sum(Number),4) as float),0) as TotalNum,isnull(cast(round(sum(SupportNumber),4) as float),0) as TotalSNum from " + tableName + " where " + strWhere;
            }

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.Read())
            {
                hfdtn.Value = sdr["TotalNum"].ToString();
                hfdtp.Value = sdr["TotalSNum"].ToString();
            }
            sdr.Close();

        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hid_ptc = (HiddenField)e.Item.FindControl("hid_ptc");
                Label lb_symbol = (Label)e.Item.FindControl("lb_symbol");
                string strptc = hid_ptc.Value.Trim();
                string sqltext = "select MP_CODE from TBPC_MARREPLACEALL where MP_PTCODE='" + strptc + "' and  substring(MP_CODE,5,1)='1' and MP_PTCODE not in(select PUR_PTCODE from TBPC_PURCHASEPLAN where PUR_CSTATE='1')";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    lb_symbol.Visible = true;
                    lb_symbol.BackColor = System.Drawing.Color.Red;
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("LabelTN")).Text = hfdtn.Value;
                ((Label)e.Item.FindControl("LabelTP")).Text = hfdtp.Value;
            }
        }
        //下推出库单
        protected void btn_out_Command(object sender, CommandEventArgs e)
        {
            string kind = e.CommandArgument.ToString();
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            string sqlQROut = "";
            string id = "0";
            int TaNum = 0;
            string CurTa = "";
            string QROut_ID = "";
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            switch (kind)
            {
                //下推出库单
                case "0":
                    List<string> sqllist = new List<string>();
                    string sql = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='OUT" + Session["UserID"].ToString() + "'";
                    sqllist.Add(sql);
                    string sqcode = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            //判斷是否為同一任务号
                            if (CurTa == "")
                            {
                                TaNum += 1;
                                CurTa = ((Label)Repeater1.Items[i].FindControl("LabelTaskID")).Text.Trim();
                            }
                            else
                            {
                                if (CurTa != ((Label)Repeater1.Items[i].FindControl("LabelTaskID")).Text.Trim())
                                {
                                    TaNum += 1;
                                    CurTa = ((Label)Repeater1.Items[i].FindControl("LabelTaskID")).Text.Trim();
                                }
                            }
                            if (TaNum > 1)
                            {
                                string alertTa = "<script>alert('勾选物料涉及多个任务号，无法生成领料单！')</script>";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alertTa, false);
                                return;
                            }


                            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                            if (((Label)Repeater1.Items[i].FindControl("LabelQROut_State")).Text == "已出库" || ((Label)Repeater1.Items[i].FindControl("LabelQROut_State")).Text == "其他")
                            {
                                string alert = "<script>alert('存在已出库或未分配项目的条目，下推被阻止！')</script>";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                                return;
                            }
                            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

                            sqcode = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql = "UPDATE TBWS_STORAGE SET SQ_STATE='OUT" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode + "'";
                            sqllist.Add(sql);


                            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                            //更新扫码出库物料中间表
                            QROut_ID = ((Label)Repeater1.Items[i].FindControl("QROut_ID2")).Text;
                            sqlQROut = "update midTable_QROut set QROut_WHSTATE='1' where QROut_ID=" + Convert.ToInt32(QROut_ID) + "";
                            sqllist.Add(sqlQROut);
                            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                        }
                    }
                    if (sqllist.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推出库单的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist);
                    Response.Redirect("~/SM_Data/SM_WarehouseOUT_LL.aspx?FLAG=PUSHBLUE&&ID=NEW", false);
                    break;
                default: break;
            }
        }
    }
}
