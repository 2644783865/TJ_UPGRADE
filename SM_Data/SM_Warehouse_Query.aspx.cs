using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_Query : BasicPage
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
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
                InitControl();

                this.Form.DefaultButton = QueryButton.UniqueID;

                bindGV();

                bindPageCondition();

                GetStorageInfo(true, false);//第一次加载

                showQueryModal();

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
                else  if (controlID == "SubTypeDropDownList")
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
            string flag = Request.QueryString["FLAG"].ToString();

            if (flag == "QUERY")
            {
                Append.Enabled = false;
                Append.Visible = false;
                Cancel.Visible = false;
                Close.Visible = false;
                LabelPush.Visible = false;
                bindControlOut(flag);
              
            }
            if (flag == "APPENDOUT" || flag == "APPENDOUTWW" || flag == "APPENDOUTXS" || flag == "APPENDMTO" || flag == "APPENDAL" || flag == "APPENDPROJTEMP" || flag == "APPENDOUTQT")
            {
                LabelPush.Visible = false;
                bindControlOut(flag);
                Panel_Operation.Visible = true;
                PanelBody.Height = 500;
            }
            if (flag == "PUSHLLOUT" || flag == "PUSHWWOUT" || flag == "PUSHXSOUT" || flag == "PUSHAL" || flag == "PUSHMTO" || flag == "PUSHPROJTEMP" || flag == "PUSHQTOUT")
            {
                //出库单，调拨，MTO的控制控件
                Append.Enabled = false;
                Append.Visible = false;
                Cancel.Visible = false;

                bindControlOut(flag);

                Panel_Operation.Visible = true;

                PanelBody.Height = 530;
            }
        }


        private void bindControlOut(string flag)
        {
            switch (flag)
            {
                case "PUSHLLOUT":
                    btn_llout.Visible = true;
                    btn_wwout.Visible = false;
                    btn_xsout.Visible = false;
                    btn_alout.Visible = false;
                    btn_mtoout.Visible = false;
                    btn_projtemp.Visible = false;
                    btn_qtout.Visible = false;
                    break;
                case "PUSHWWOUT":
                    btn_llout.Visible = false;
                    btn_wwout.Visible = true;
                    btn_xsout.Visible = false;
                    btn_alout.Visible = false;
                    btn_mtoout.Visible = false;
                    btn_projtemp.Visible = false;
                    btn_qtout.Visible = false;
                    break;
                case "PUSHXSOUT":
                    btn_llout.Visible = false;
                    btn_wwout.Visible = false;
                    btn_xsout.Visible = true;
                    btn_alout.Visible = false;
                    btn_mtoout.Visible = false;
                    btn_projtemp.Visible = false;
                    btn_qtout.Visible = false;
                    break;
                case "PUSHAL":
                    btn_llout.Visible = false;
                    btn_wwout.Visible = false;
                    btn_xsout.Visible = false;
                    btn_alout.Visible = true;
                    btn_mtoout.Visible = false;
                    btn_projtemp.Visible = false;
                    btn_qtout.Visible = false;
                    break;
                case "PUSHMTO":
                    btn_llout.Visible = false;
                    btn_wwout.Visible = false;
                    btn_xsout.Visible = false;
                    btn_alout.Visible = false;
                    btn_mtoout.Visible = true;
                    btn_projtemp.Visible = false;
                    btn_qtout.Visible = false;
                    break;
                case "PUSHPROJTEMP":
                    btn_llout.Visible = false;
                    btn_wwout.Visible = false;
                    btn_xsout.Visible = false;
                    btn_alout.Visible = false;
                    btn_mtoout.Visible = false;
                    btn_projtemp.Visible = true;
                    btn_qtout.Visible = false;
                    break;
                case "PUSHQTOUT":
                    btn_llout.Visible = false;
                    btn_wwout.Visible = false;
                    btn_xsout.Visible = false;
                    btn_alout.Visible = false;
                    btn_mtoout.Visible = false;
                    btn_projtemp.Visible = false;
                    btn_qtout.Visible = true;
                    break;
                default:
                    btn_llout.Visible = false;
                    btn_wwout.Visible = false;
                    btn_xsout.Visible = false;
                    btn_alout.Visible = false;
                    btn_mtoout.Visible = false;
                    btn_projtemp.Visible = false;
                    btn_qtout.Visible = false;
                    break;
            }
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetStorageInfo(false,false);
        }

        private void BindPage(string tableName, string PrimaryKey, string ShowFields, string OrderField, string GroupField, int OrderType, string StrWhere, int PageSize,bool isFristPage)
        {

            InitVar(tableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFristPage);

        }


        private void InitVar(string tableName, string PrimaryKey, string ShowFields, string OrderField, string GroupField, int OrderType, string StrWhere, int PageSize,bool isFristPage)
        {

            InitPager(tableName, PrimaryKey, ShowFields, OrderField,GroupField, OrderType, StrWhere, PageSize);//初始化页面

            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数

            if (isFristPage)
            {
                UCPaging1.CurrentPage = 1;
            }

            bindData();
        }

        //初始化分页信息
        private void InitPager(string tableName, string PrimaryKey, string ShowFields, string OrderField,string GroupField,int OrderType, string StrWhere, int PageSize)
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
            string clearsql = "delete from TBWS_STORAGETEMP where SQ_OPERSTATE='Query" + Session["UserID"].ToString()+"'";
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
            WarehouseDropDownList.Items.Insert(0,item );
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
            GetStorageInfo(true,true);

            refreshStyle();

            ModalPopupExtenderSearch.Hide();

            UpdatePanelBody.Update();
        }

        //重置条件
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearCondition();

            resetSubcondition();
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
            GetStorageInfo(true,false);

        }

        protected void TypeOrOrderBy_SelectedIndexChanged(object sender, EventArgs e)
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
            GetStorageInfo(true,false);

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

            DropDownListPush.ClearSelection();
            DropDownListPush.Items[0].Selected = true;

            WarehouseDropDownList.ClearSelection();
            WarehouseDropDownList.Items[0].Selected = true;

            ChildWarehouseDropDownList.ClearSelection();
            ChildWarehouseDropDownList.Items[0].Selected = true;

            TypeDropDownList.ClearSelection();
            TypeDropDownList.Items[0].Selected = true;

            SubTypeDropDownList.ClearSelection();
            SubTypeDropDownList.Items[0].Selected = true;

            DateTextBox.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

            DropDownListMerge.ClearSelection();
            DropDownListMerge.Items[0].Selected = true;

            TextBoxCode.Text = string.Empty;//物料代码
            TextBoxName.Text = string.Empty;//物料名称
            TextBoxPTC.Text = string.Empty;//计划跟踪号
            TextBoxNO.Text=string.Empty;  //标识号
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
            string condition = "";

            //以下语句为准备查询条件
            //物料代码条件
            if (TextBoxCode.Text.Trim() != "")
            {
                condition = " MaterialCode LIKE '%" + TextBoxCode.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxCode", TextBoxCode.GetType().ToString(), TextBoxCode.Text.Trim());
            }

            //物料名称条件
            if ((TextBoxName.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " MaterialName LIKE '%" + TextBoxName.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxName", TextBoxName.GetType().ToString(), TextBoxName.Text.Trim());

            }
            else if ((TextBoxName.Text.Trim() != "") && (condition == ""))
            {
                condition += " MaterialName LIKE '%" + TextBoxName.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxName", TextBoxName.GetType().ToString(), TextBoxName.Text.Trim());

            }

            //标识号
            if ((TextBoxNO.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " CGMODE LIKE '%" + TextBoxNO.Text.Trim().ToUpper() + "%'";
                addQueryItem(PageName, "TextBoxNO", TextBoxNO.GetType().ToString(), TextBoxNO.Text.Trim());

            }
            else if ((TextBoxNO.Text.Trim() != "") && (condition == ""))
            {
                condition += " CGMODE LIKE '%" + TextBoxNO.Text.Trim().ToUpper() + "%'";
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
            else if ((TextBoxPTC.Text.Trim() != "") && (condition == ""))
            {
                if (TextBoxPTC.Text.Trim() == "备库")
                {
                    condition += " (PTC LIKE '%备库%' or PTC LIKE '%BEIKU%')";
                    addQueryItem(PageName, "TextBoxPTC", TextBoxPTC.GetType().ToString(), TextBoxPTC.Text.Trim());
                }
                else
                {
                    condition += " PTC LIKE '%" + TextBoxPTC.Text.Trim() + "%'";
                    addQueryItem(PageName, "TextBoxPTC", TextBoxPTC.GetType().ToString(), TextBoxPTC.Text.Trim());
                }
            }
            //批号条件
            if ((TextBoxLotNumber.Text != "") && (condition != ""))
            {
                condition += " AND " + " PTC LIKE '%" + TextBoxLotNumber.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxLotNumber", TextBoxLotNumber.GetType().ToString(), TextBoxLotNumber.Text.Trim());

            }
            else if ((TextBoxLotNumber.Text != "") && (condition == ""))
            {
                condition += " PTC LIKE '%" + TextBoxLotNumber.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxLotNumber", TextBoxLotNumber.GetType().ToString(), TextBoxLotNumber.Text.Trim());

            }
            //型号规格条件
            if ((TextBoxStandard.Text != "") && (condition != ""))
            {
                condition += " AND " + " Standard LIKE '%" + TextBoxStandard.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxStandard", TextBoxStandard.GetType().ToString(), TextBoxStandard.Text.Trim());

            }
            else if ((TextBoxStandard.Text != "") && (condition == ""))
            {
                condition += " Standard LIKE '%" + TextBoxStandard.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxStandard", TextBoxStandard.GetType().ToString(), TextBoxStandard.Text.Trim());

            }
            //长度条件
            if ((TextBoxLength.Text != "") && (condition != ""))
            {
                condition += " AND " + " Length LIKE '%" + TextBoxLength.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxLength", TextBoxLength.GetType().ToString(), TextBoxLength.Text.Trim());

            }
            else if ((TextBoxLength.Text != "") && (condition == ""))
            {
                condition += " Length LIKE '%" + TextBoxLength.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxLength", TextBoxLength.GetType().ToString(), TextBoxLength.Text.Trim());

            }
            //宽度条件
            if ((TextBoxWidth.Text != "") && (condition != ""))
            {
                condition += " AND " + " Width LIKE '%" + TextBoxWidth.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxWidth", TextBoxWidth.GetType().ToString(), TextBoxWidth.Text.Trim());

            }
            else if ((TextBoxWidth.Text != "") && (condition == ""))
            {
                condition += " Width LIKE '%" + TextBoxWidth.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxWidth", TextBoxWidth.GetType().ToString(), TextBoxWidth.Text.Trim());

            }
            //材质条件
            if ((TextBoxAttribute.Text != "") && (condition != ""))
            {
                condition += " AND " + " Attribute LIKE '%" + TextBoxAttribute.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxAttribute", TextBoxAttribute.GetType().ToString(), TextBoxAttribute.Text.Trim());

            }
            else if ((TextBoxAttribute.Text != "") && (condition == ""))
            {
                condition += " Attribute LIKE '%" + TextBoxAttribute.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxAttribute", TextBoxAttribute.GetType().ToString(), TextBoxAttribute.Text.Trim());

            }
            //是否定尺
            if ((TextBoxFixed.Text != "") && (condition != ""))
            {
                condition += " AND " + " Fixed LIKE '%" + TextBoxFixed.Text.Trim().ToUpper() + "%'";
                addQueryItem(PageName, "TextBoxFixed", TextBoxFixed.GetType().ToString(), TextBoxFixed.Text.Trim());

            }
            else if ((TextBoxFixed.Text != "") && (condition == ""))
            {
                condition += " Fixed LIKE '%" + TextBoxFixed.Text.Trim().ToUpper() + "%'";
                addQueryItem(PageName, "TextBoxFixed", TextBoxFixed.GetType().ToString(), TextBoxFixed.Text.Trim());

            }
            //备注条件
            if ((TextBoxNote.Text != "") && (condition != ""))
            {
                condition += " AND " + " Note LIKE '%" + TextBoxNote.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxNote", TextBoxNote.GetType().ToString(), TextBoxNote.Text.Trim());

            }
            else if ((TextBoxNote.Text != "") && (condition == ""))
            {
                condition += " Note LIKE '%" + TextBoxNote.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxNote", TextBoxNote.GetType().ToString(), TextBoxNote.Text.Trim());

            }

            //订单编号条件
            if ((TextBoxOrderCode.Text != "") && (condition != ""))
            {
                condition += " AND " + " OrderCode LIKE '%" + TextBoxOrderCode.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxOrderCode", TextBoxOrderCode.GetType().ToString(), TextBoxOrderCode.Text.Trim());

            }
            else if ((TextBoxOrderCode.Text != "") && (condition == ""))
            {
                condition += " OrderCode LIKE '%" + TextBoxOrderCode.Text.Trim() + "%'";
                addQueryItem(PageName, "TextBoxOrderCode", TextBoxOrderCode.GetType().ToString(), TextBoxOrderCode.Text.Trim());

            }

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
                    condition += " AND " + " MaterialCode LIKE '" + SubTypeDropDownList.SelectedValue + "%'";
                    addQueryItem(PageName, "TypeDropDownList", TypeDropDownList.GetType().ToString(), TypeDropDownList.SelectedValue);
                    addQueryItem(PageName, "SubTypeDropDownList", SubTypeDropDownList.GetType().ToString(), SubTypeDropDownList.SelectedValue);
                }
                else
                {
                    condition += " MaterialCode LIKE '" + SubTypeDropDownList.SelectedValue + "%'";
                    addQueryItem(PageName, "TypeDropDownList", TypeDropDownList.GetType().ToString(), TypeDropDownList.SelectedValue);
                    addQueryItem(PageName, "SubTypeDropDownList", SubTypeDropDownList.GetType().ToString(), SubTypeDropDownList.SelectedValue);
                }
            }
            else if ((TypeDropDownList.SelectedValue != "0") && (SubTypeDropDownList.SelectedValue == "0"))
            {
                if (condition != "")
                {
                    condition += " AND " + " MaterialCode LIKE '" + TypeDropDownList.SelectedValue + "%'";
                    addQueryItem(PageName, "TypeDropDownList", TypeDropDownList.GetType().ToString(), TypeDropDownList.SelectedValue);
                    addQueryItem(PageName, "SubTypeDropDownList", SubTypeDropDownList.GetType().ToString(), SubTypeDropDownList.SelectedValue);
                }
                else
                {
                    condition += " MaterialCode LIKE '" + TypeDropDownList.SelectedValue + "%'";
                    addQueryItem(PageName, "TypeDropDownList", TypeDropDownList.GetType().ToString(), TypeDropDownList.SelectedValue);
                    addQueryItem(PageName, "SubTypeDropDownList", SubTypeDropDownList.GetType().ToString(), SubTypeDropDownList.SelectedValue);
                }
            }

            string SubCondtion = GetSubCondtion();

            if (condition != "")
            {

                //AND可以变化


                if (SubCondtion != "")

                    condition += DropDownListFatherLogic.SelectedValue + " (" + SubCondtion + ")";

            }
            else
            {
                if (SubCondtion != "")

                    condition += SubCondtion;

            }
            return condition;

        }

        private void addQueryItem(string pageName, string controlID, string controlType, string controlValue)
        {

            QueryItemInfo queryItem = new QueryItemInfo(Session["UserID"].ToString(), pageName, controlID, controlType, controlValue);

            queryItems.Add(queryItem);

        }

        //根据条件获取物料信息的函数，本页面关键功能！
        protected void GetStorageInfo(bool isFirstPage,bool isUpdateCondition)
        {

            string condition = GetStrCondition();

            //=====之前为条件==========================================

            //按时间查询
            // 将日期和时间的指定 System.String 表示形式转换为等效的 System.DateTime。
            // 2011-10-08转换之后，2011-10-8 0:00:00
            // 新的 System.DateTime，其日期与此实例相同，时间值设置为午夜 12:00:00 (00:00:00)。

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



            if (Convert.ToDateTime(DateTextBox.Text) == DateTime.Now.Date)
            { 
                
              //如果当天查看，系统默认为当前库存

              //进来默认的是查即使库存

              //未合并

                if (DropDownListMerge.SelectedValue == "0")
                {
                    if (condition != "")
                    {

                        string TableName = "(select * from View_SM_Storage as a left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as b on a.PTC=b.PTCFrom and a.MaterialCode=b.TzMaterialCode)t";

                       string PrimaryKey = "SQCODE";

                       string ShowFields = "SQCODE,MaterialCode,MaterialName,Attribute,GB,Standard AS MaterialStandard,Fixed,Length,Width,LotNumber,PlanMode,PTC,WarehouseCode,Warehouse,LocationCode AS PositionCode,Location AS Position,Unit,cast(Number as float) AS Number,cast(SupportNumber as float) AS Quantity,Note AS Comment,CGMODE,OrderCode,OUTTZNUM,OUTTZFZNUM";
                        //数据库中的主键
                       string OrderField = DropDownListType.SelectedValue;

                       string GroupField = "";

                       int OrderType = Convert.ToInt32(RadioButtonListOrderBy.SelectedValue);
                        /**/
                       string StrWhere = condition;

                       int PageSize = ObjPageSize;

                       GetTotalNum(StrWhere, "(select * from View_SM_Storage as a left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as b on a.PTC=b.PTCFrom and a.MaterialCode=b.TzMaterialCode)t");

                       BindPage(TableName, PrimaryKey, ShowFields, OrderField,GroupField, OrderType, StrWhere, PageSize,isFirstPage);
                     
                    }
                    else
                    {

                        //Number为数量，Quantity为辅助数量

                        string TableName = "(select * from View_SM_Storage as a left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as b on a.PTC=b.PTCFrom and a.MaterialCode=b.TzMaterialCode)t";

                        string PrimaryKey = "SQCODE";

                        string ShowFields = "SQCODE,MaterialCode,MaterialName,Attribute,GB,Standard AS MaterialStandard,Fixed,Length,Width,LotNumber,PlanMode,PTC,WarehouseCode,Warehouse,LocationCode AS PositionCode,Location AS Position,Unit,cast(Number as float) AS Number,cast(SupportNumber as float) AS Quantity,Note AS Comment,CGMODE,OrderCode,OUTTZNUM,OUTTZFZNUM";
                        //数据库中的主键
                        string OrderField = DropDownListType.SelectedValue;

                        string GroupField = "";

                        int OrderType = Convert.ToInt32(RadioButtonListOrderBy.SelectedValue);
                        /**/
                        string StrWhere = "";

                        int PageSize = ObjPageSize;

                        GetTotalNum(StrWhere, "(select * from View_SM_Storage as a left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as b on a.PTC=b.PTCFrom and a.MaterialCode=b.TzMaterialCode)t");

                        BindPage(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFirstPage);

                    }
                }

                //合并

                //按照物料代码分组进行合并

                if (DropDownListMerge.SelectedValue == "1")
                {
                    if (condition != "")
                    {
                        //按照物料代码合并，则物料代码是唯一字段

                        string TableName = "(select * from View_SM_Storage as a left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as b on a.PTC=b.PTCFrom and a.MaterialCode=b.TzMaterialCode)t";

                        string PrimaryKey = "MaterialCode";

                        string ShowFields = "MaterialCode,MaterialName,Attribute,GB,Standard AS MaterialStandard,Fixed='',SQCODE='',Length='',Width='',LotNumber='',PlanMode='',PTC='',Warehouse='',Position='',Unit,Comment='',cast(SUM(Number) as float) AS Number,cast(SUM(SupportNumber) as float) AS Quantity,CGMODE='',OrderCode='',cast(SUM(OUTTZNUM) as float) AS OUTTZNUM,cast(SUM(OUTTZFZNUM) as float) AS OUTTZFZNUM";

                        string OrderField = DropDownListType.SelectedValue;


                        string GroupField = " MaterialCode,MaterialName,Attribute,GB,Standard,Unit";

                        int OrderType = Convert.ToInt32(RadioButtonListOrderBy.SelectedValue);
                        /**/
                        string StrWhere = condition;

                        int PageSize = ObjPageSize;

                        GetTotalNum(StrWhere, "(select * from View_SM_Storage as a left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as b on a.PTC=b.PTCFrom and a.MaterialCode=b.TzMaterialCode)t");

                        BindPage(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFirstPage);

                        //bindData();

                        //sql = "SELECT MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute AS Attribute,GB AS GB," +
                        //    "Standard AS MaterialStandard,Fixed='',SQCODE='',Length='',Width='',LotNumber='',PTC=''," +
                        //    "Warehouse='',Position='',Unit AS Unit," +
                        //    "Comment='',SUM(Number) AS Number,SUM(SupportNumber) AS Quantity  FROM (select * from View_SM_Storage as a left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as b on a.PTC=b.PTCFrom and a.MaterialCode=b.TzMaterialCode)t WHERE " +
                        //    condition + " GROUP BY MaterialCode,MaterialName,Attribute,GB,Standard,Unit";
                    }
                    else
                    {

                        string TableName = "(select * from View_SM_Storage as a left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as b on a.PTC=b.PTCFrom and a.MaterialCode=b.TzMaterialCode)t";

                        string PrimaryKey = "MaterialCode";

                        string ShowFields = "MaterialCode,MaterialName,Attribute,GB,Standard AS MaterialStandard,Fixed='',SQCODE='',Length='',Width='',LotNumber='',PlanMode='',PTC='',Warehouse='',Position='',Unit,Comment='',cast(SUM(Number) as float) AS Number,cast(SUM(SupportNumber) as float) AS Quantity,CGMODE='',OrderCode='',cast(SUM(OUTTZNUM) as float) AS OUTTZNUM,cast(SUM(OUTTZFZNUM) as float) AS OUTTZFZNUM";

                        string OrderField = DropDownListType.SelectedValue;

                        string GroupField = " MaterialCode,MaterialName,Attribute,GB,Standard,Unit";

                        int OrderType = Convert.ToInt32(RadioButtonListOrderBy.SelectedValue);
                        /**/
                        string StrWhere = "";

                        int PageSize = ObjPageSize;

                        GetTotalNum(StrWhere, "(select * from View_SM_Storage as a left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as b on a.PTC=b.PTCFrom and a.MaterialCode=b.TzMaterialCode)t");

                        BindPage(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFirstPage);

                        //bindData();
                        //sql = "SELECT MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute AS Attribute,GB AS GB," +
                        //    "Standard AS MaterialStandard,Fixed='',SQCODE='',Length='',Width='',LotNumber='',PTC=''," +
                        //    "Warehouse='',Position='',Unit AS Unit," +
                        //    "Comment='',SUM(Number) AS Number,SUM(SupportNumber) AS Quantity  FROM (select * from View_SM_Storage as a left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as b on a.PTC=b.PTCFrom and a.MaterialCode=b.TzMaterialCode)t " +
                        //    "GROUP BY MaterialCode,MaterialName,Attribute,GB,Standard,Unit";
                    }  
                }
            }

            //查询历史库存
            //查询历史库存，系统默认是年-月-日 24：00：00
            else if (Convert.ToDateTime(DateTextBox.Text) < DateTime.Now.Date)
            {

                string EndDate = DateTextBox.Text + " 23:59:59";//凌晨,导出的是2月28号凌晨的数据

                //搜索历史库存

                //EndDate可以带时分秒

                string sqlstring = DBCallCommon.GetStringValue("connectionStrings");
                SqlConnection con = new SqlConnection(sqlstring);
                con.Open();
                SqlCommand cmd = new SqlCommand("StorageHistory", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter spr;
                spr = cmd.Parameters.Add("@EndDate", SqlDbType.VarChar, 50);				//增加参数截止日期@EndDate
                cmd.Parameters["@EndDate"].Value = EndDate;							//为参数初始化
                spr = cmd.Parameters.Add("@OperateState", SqlDbType.VarChar, 50);				//操作状态
                cmd.Parameters["@OperateState"].Value = "Query" + Session["UserID"].ToString();	//为参数初始化
                cmd.ExecuteNonQuery();
                con.Close();

                //添加一个操作状态

                if (condition != "")
                {
                    condition += " AND " + " OperState='Query" + Session["UserID"].ToString() + "'";
                }
                else
                {
                    condition += " OperState='Query" + Session["UserID"].ToString() + "'";
                }

                //未合并
                if (DropDownListMerge.SelectedValue == "0")
                {
                    if (condition != "")
                    {

                        string TableName = "View_SM_StorageTemp";

                        string PrimaryKey = "SQCODE";

                        string ShowFields = "SQCODE,MaterialCode,MaterialName,Attribute,GB,Standard AS MaterialStandard,Fixed,Length,Width,LotNumber,PlanMode,PTC,WarehouseCode,Warehouse,LocationCode AS PositionCode,Location AS Position,Unit,cast(Number as float) AS Number,cast(SupportNumber as float) AS Quantity,Note AS Comment,CGMODE,OrderCode";

                        string OrderField = DropDownListType.SelectedValue;

                        string GroupField = "";

                        int OrderType = Convert.ToInt32(RadioButtonListOrderBy.SelectedValue);

                        string StrWhere = condition;

                        int PageSize = ObjPageSize;

                        GetTotalNum(StrWhere, "View_SM_StorageTemp");

                        BindPage(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFirstPage);

                    }
                    else
                    {
                        string TableName = "View_SM_StorageTemp";

                        string PrimaryKey = "MaterialCode";

                        string ShowFields = "SQCODE,MaterialCode,MaterialName,Attribute,GB,Standard AS MaterialStandard,Fixed,Length,Width,LotNumber,PlanMode,PTC,WarehouseCode,Warehouse,LocationCode AS PositionCode,Location AS Position,Unit,cast(Number as float) AS Number,cast(SupportNumber as float) AS Quantity,Note AS Comment,CGMODE,OrderCode";

                        string OrderField = DropDownListType.SelectedValue;

                        string GroupField = "";

                        int OrderType = Convert.ToInt32(RadioButtonListOrderBy.SelectedValue);

                        string StrWhere = "";

                        int PageSize = ObjPageSize;

                        GetTotalNum(StrWhere, "View_SM_StorageTemp");

                        BindPage(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFirstPage);

                    }
                }

                //合并
                if (DropDownListMerge.SelectedValue == "1")
                {
                    if (condition != "")
                    {

                        string TableName = "View_SM_StorageTemp";

                        string PrimaryKey = "MaterialCode";

                        string ShowFields = "MaterialCode,MaterialName,Attribute,GB,Standard AS MaterialStandard,Fixed='',SQCODE='',Length='',Width='',LotNumber='',PlanMode='',PTC='',Warehouse='',Position='',Unit AS Unit,Comment='',cast(SUM(Number) as float) AS Number,cast(SUM(SupportNumber) as float) AS Quantity,CGMODE='',OrderCode=''";

                        string OrderField = DropDownListType.SelectedValue;

                        string GroupField = " MaterialCode,MaterialName,Attribute,GB,Standard,Unit";

                        int OrderType = Convert.ToInt32(RadioButtonListOrderBy.SelectedValue);
                        
                        string StrWhere = condition;

                        int PageSize = ObjPageSize;

                        GetTotalNum(StrWhere, "View_SM_StorageTemp");

                        BindPage(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFirstPage);

                        //bindData();

                        //sql = "SELECT MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute AS Attribute,GB AS GB," +
                        //    "Standard AS MaterialStandard,Fixed='',SQCODE='',Length='',Width='',LotNumber='',PTC=''," +
                        //    "Warehouse='',Position='',Unit AS Unit," +
                        //    "Comment='',SUM(Number) AS Number,SUM(SupportNumber) AS Quantity  FROM View_SM_StorageTemp WHERE " +
                        //    condition + " GROUP BY MaterialCode,MaterialName,Attribute,GB,Standard,Unit";
                    }
                    else
                    {
                        string TableName = "View_SM_StorageTemp";

                        string PrimaryKey = "MaterialCode";

                        string ShowFields = "MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed='',SQCODE='',Length='',Width='',PlanMode='',LotNumber='',PTC='',Warehouse='',Position='',Unit AS Unit,Comment='', cast(SUM(Number) as float) AS Number,cast(SUM(SupportNumber) as float) AS Quantity,CGMODE='',OrderCode=''";

                        string OrderField = DropDownListType.SelectedValue;

                        string GroupField = " MaterialCode,MaterialName,Attribute,GB,Standard,Unit";

                        int OrderType = Convert.ToInt32(RadioButtonListOrderBy.SelectedValue);
                        /**/
                        string StrWhere = "";

                        int PageSize = ObjPageSize;

                        GetTotalNum(StrWhere, "View_SM_StorageTemp");

                        BindPage(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFirstPage);

                        //bindData();

                        //sql = "SELECT MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute AS Attribute,GB AS GB," +
                        //    "Standard AS MaterialStandard,Fixed='',SQCODE='',Length='',Width='',LotNumber='',PTC=''," +
                        //    "Warehouse='',Position='',Unit AS Unit," +
                        //    "Comment='',SUM(Number) AS Number,SUM(SupportNumber) AS Quantity  FROM View_SM_StorageTemp " +
                        //    "GROUP BY MaterialCode,MaterialName,Attribute,GB,Standard,Unit";
                    } 
                }
            }
            else
            {
                LabelMessage.Text = "截止日期必须小于等于当前日期";
                DateTextBox.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                return;
            }
        }







        private void GetTotalNum(string strWhere,string tableName)
        {
            string sql="";
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

        //下推出库单，调拨单和MTO调整单的各项操作
        protected void DropDownListPush_SelectedIndexChanged(object sender, EventArgs e) 
        {
            string kind = DropDownListPush.SelectedValue;

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
                            sqcode = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql = "UPDATE TBWS_STORAGE SET SQ_STATE='OUT" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode + "'";
                            sqllist.Add(sql);
                        }
                    }
                    if (sqllist.Count <2)
                    {
                        LabelMessage.Text = "请选择要下推出库单的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist);
                    Response.Redirect("~/SM_Data/SM_WarehouseOUT_LL.aspx?FLAG=PUSHBLUE&&ID=NEW");
                    break;
                case "1":
                    List<string> sqllist1 = new List<string>();
                    string sql1 = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='OUTWW" + Session["UserID"].ToString() + "'";
                    sqllist1.Add(sql1);
                    string sqcode1 = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            sqcode1 = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql1 = "UPDATE TBWS_STORAGE SET SQ_STATE='OUTWW" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode1 + "'";
                            sqllist1.Add(sql1);
                        }
                    }
                    if (sqllist1.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推出库单的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist1);
                    Response.Redirect("~/SM_Data/SM_WarehouseOUT_WW.aspx?FLAG=PUSHBLUE&&ID=NEW");
                    break;
                case "2":
                    List<string> sqllist2 = new List<string>();
                    string sql2 = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='OUTWW" + Session["UserID"].ToString() + "'";
                    sqllist2.Add(sql2);
                    string sqcode2 = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            sqcode2 = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql2 = "UPDATE TBWS_STORAGE SET SQ_STATE='OUTXS" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode2 + "'";
                            sqllist2.Add(sql2);
                        }
                    }
                    if (sqllist2.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推出库单的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist2);
                    Response.Redirect("~/SM_Data/SM_WarehouseOUT_XS.aspx?FLAG=PUSHBLUE&&ID=NEW");
                    break;
                //下推调拨单
                case "3":
                    List<string> sqllist3 = new List<string>();
                    string sql3 = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='AL" + Session["UserID"].ToString() + "'";
                    sqllist3.Add(sql3);
                    string sqcode3 = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            sqcode3 = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql3 = "UPDATE TBWS_STORAGE SET SQ_STATE='AL" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode3 + "'";
                            sqllist3.Add(sql3);
                        }
                    }
                    if (sqllist3.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推出库单的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist3);
                    Response.Redirect("~/SM_Data/SM_Warehouse_Allocation.aspx?FLAG=PUSHAL&&ID=NEW");
                    break;
                //下推MTO调整单
                case "4":
                    List<string> sqllist4 = new List<string>();
                    string sql4 = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='MTO" + Session["UserID"].ToString() + "'";
                    sqllist4.Add(sql4);
                    string sqcode4 = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            sqcode4 = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql4 = "UPDATE TBWS_STORAGE SET SQ_STATE='MTO" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode4 + "'";
                            sqllist4.Add(sql4);
                        }
                    }
                    if (sqllist4.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推出库单的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist4);
                    Response.Redirect("~/SM_Data/SM_Warehouse_MTOAdjust.aspx?FLAG=PUSHMTO&&ID=NEW");
                    break;
                default: break;
            }
        }

        protected void Append_Click(object sender,EventArgs e)
        {
            string flag = Request.QueryString["FLAG"].ToString();
            switch (flag)
            {
                case "APPENDOUT":
                    List<string> sqllist = new List<string>();
                    string sql = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='APPENDOUT" + Session["UserID"].ToString() + "'";
                    sqllist.Add(sql);
                    string sqcode = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            sqcode = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql = "UPDATE TBWS_STORAGE SET SQ_STATE='APPENDOUT" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode + "'";
                            sqllist.Add(sql);
                        }
                    }
                    if (sqllist.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推出库单的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist);
                    ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>confirmapp();</script>");
                    break;
                case "APPENDOUTWW":
                    List<string> sqllist1 = new List<string>();
                    string sql1 = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='APPENDOUTWW" + Session["UserID"].ToString() + "'";
                    sqllist1.Add(sql1);
                    string sqcode1 = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            sqcode1 = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql1 = "UPDATE TBWS_STORAGE SET SQ_STATE='APPENDOUTWW" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode1 + "'";
                            sqllist1.Add(sql1);
                        }
                    }
                    if (sqllist1.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推出库单的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist1);
                    ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>confirmapp();</script>");
                    break;
                case "APPENDOUTXS":
                    List<string> sqllist2 = new List<string>();
                    string sql2 = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='APPENDOUTXS" + Session["UserID"].ToString() + "'";
                    sqllist2.Add(sql2);
                    string sqcode2 = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            sqcode2 = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql2 = "UPDATE TBWS_STORAGE SET SQ_STATE='APPENDOUTXS" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode2 + "'";
                            sqllist2.Add(sql2);
                        }
                    }
                    if (sqllist2.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推出库单的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist2);
                    ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>confirmapp();</script>");
                    break;
                case "APPENDAL":
                    List<string> sqllist3 = new List<string>();
                    string sql3 = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='APPENDAL" + Session["UserID"].ToString() + "'";
                    sqllist3.Add(sql3);
                    string sqcode3 = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            sqcode3 = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql3 = "UPDATE TBWS_STORAGE SET SQ_STATE='APPENDAL" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode3 + "'";
                            sqllist3.Add(sql3);
                        }
                    }
                    if (sqllist3.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推调拨单的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist3);
                    ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>confirmapp();</script>");
                    break;
                case "APPENDMTO":
                    List<string> sqllist4 = new List<string>();
                    string sql4 = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='APPENDMTO" + Session["UserID"].ToString() + "'";
                    sqllist4.Add(sql4);
                    string sqcode4 = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            sqcode4 = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql4= "UPDATE TBWS_STORAGE SET SQ_STATE='APPENDMTO" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode4 + "'";
                            sqllist4.Add(sql4);
                        }
                    }
                    if (sqllist4.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推MTO调整单的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist4);
                    ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>confirmapp();</script>");
                    break;
                case "APPENDPROJTEMP":
                    List<string> sqllist5 = new List<string>();
                    string sql5 = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='APPENDPROJTEMP" + Session["UserID"].ToString() + "'";
                    sqllist5.Add(sql5);
                    string sqcode5 = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            sqcode5 = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql5 = "UPDATE TBWS_STORAGE SET SQ_STATE='APPENDPROJTEMP" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode5 + "'";
                            sqllist5.Add(sql5);
                        }
                    }
                    if (sqllist5.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推项目结转的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist5);
                    ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>confirmapp();</script>");
                    break;
                case "APPENDOUTQT":
                    List<string> sqllist6 = new List<string>();
                    string sql6 = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='APPENDOUTQT" + Session["UserID"].ToString() + "'";
                    sqllist6.Add(sql6);
                    string sqcode6 = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            sqcode6 = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql6 = "UPDATE TBWS_STORAGE SET SQ_STATE='APPENDOUTQT" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode6 + "'";
                            sqllist6.Add(sql6);
                        }
                    }
                    if (sqllist6.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推其他出库单的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist6);
                    ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>confirmapp();</script>");
                    break;
                default:
                    break;
            }
        }

        protected void btn_out_Command(object sender, CommandEventArgs e)
        {
            string kind = e.CommandArgument.ToString();

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
                            sqcode = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql = "UPDATE TBWS_STORAGE SET SQ_STATE='OUT" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode + "'";
                            sqllist.Add(sql);
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
                case "1":
                    List<string> sqllist1 = new List<string>();
                    string sql1 = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='OUTWW" + Session["UserID"].ToString() + "'";
                    sqllist1.Add(sql1);
                    string sqcode1 = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            sqcode1 = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql1 = "UPDATE TBWS_STORAGE SET SQ_STATE='OUTWW" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode1 + "'";
                            sqllist1.Add(sql1);
                        }
                    }
                    if (sqllist1.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推出库单的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist1);
                    Response.Redirect("~/SM_Data/SM_WarehouseOUT_WW.aspx?FLAG=PUSHBLUE&&ID=NEW", false);
                    break;
                case "2":
                    List<string> sqllist2 = new List<string>();
                    string sql2 = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='OUTWW" + Session["UserID"].ToString() + "'";
                    sqllist2.Add(sql2);
                    string sqcode2 = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            sqcode2 = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql2 = "UPDATE TBWS_STORAGE SET SQ_STATE='OUTXS" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode2 + "'";
                            sqllist2.Add(sql2);
                        }
                    }
                    if (sqllist2.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推出库单的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist2);
                    Response.Redirect("~/SM_Data/SM_WarehouseOUT_XS.aspx?FLAG=PUSHBLUE&&ID=NEW", false);
                    break;
                //下推调拨单
                case "3":
                    List<string> sqllist3 = new List<string>();
                    string sql3 = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='AL" + Session["UserID"].ToString() + "'";
                    sqllist3.Add(sql3);
                    string sqcode3 = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            sqcode3 = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql3 = "UPDATE TBWS_STORAGE SET SQ_STATE='AL" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode3 + "'";
                            sqllist3.Add(sql3);
                        }
                    }
                    if (sqllist3.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推出库单的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist3);
                    Response.Redirect("~/SM_Data/SM_Warehouse_Allocation.aspx?FLAG=PUSHAL&&ID=NEW", false);
                    break;
                //下推MTO调整单
                case "4":
                    List<string> sqllist4 = new List<string>();
                    string sql4 = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='MTO" + Session["UserID"].ToString() + "'";
                    sqllist4.Add(sql4);
                    string sqcode4 = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            sqcode4 = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql4 = "UPDATE TBWS_STORAGE SET SQ_STATE='MTO" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode4 + "'";
                            sqllist4.Add(sql4);
                        }
                    }
                    if (sqllist4.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推出库单的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist4);
                    Response.Redirect("~/SM_Data/SM_Warehouse_MTOAdjust.aspx?FLAG=PUSHMTO&&ID=NEW", false);
                    break;
                case "5":
                    List<string> sqllist5 = new List<string>();
                    string sql5= "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='PROJTEMP" + Session["UserID"].ToString() + "'";
                    sqllist5.Add(sql5);
                    string sqcode5 = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            sqcode5 = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql5 = "UPDATE TBWS_STORAGE SET SQ_STATE='PROJTEMP" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode5 + "'";
                            sqllist5.Add(sql5);
                        }
                    }
                    if (sqllist5.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推项目结转的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist5);
                    Response.Redirect("~/SM_Data/SM_Warehouse_ProjTemp.aspx?FLAG=PUSHPROJTEMP&&ID=NEW", false);
                    break;
                case "6":
                    List<string> sqllist6 = new List<string>();
                    string sql6 = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='OUTQT" + Session["UserID"].ToString() + "'";
                    sqllist6.Add(sql6);
                    string sqcode6 = "";
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                        {
                            sqcode6 = ((Label)Repeater1.Items[i].FindControl("LabelSQCODE")).Text;
                            sql6 = "UPDATE TBWS_STORAGE SET SQ_STATE='OUTQT" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + sqcode6 + "'";
                            sqllist6.Add(sql6);
                        }
                    }
                    if (sqllist6.Count < 2)
                    {
                        LabelMessage.Text = "请选择要下推其他出库单的条目！";
                        return;
                    }
                    DBCallCommon.ExecuteTrans(sqllist6);
                    Response.Redirect("~/SM_Data/SM_WarehouseOUT_QT.aspx?FLAG=PUSHBLUE&&ID=NEW", false);
                    break;
                default: break;
            }
        }


        #region 条件框


        private void bindGV()
        {
            GridViewSearch.DataSource = CreateTable();

            GridViewSearch.DataBind();
        }
        private DataTable CreateTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("index", typeof(int)));


            for (int i = 0; i < 7; i++)
            {
                DataRow row = dt.NewRow();
                row["index"] = i;
                dt.Rows.Add(row);
            }

            return dt;
        }



        private Dictionary<string, string> bindItemList()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();
        
            ItemList.Add("NO", "");
            ItemList.Add("PTC", "计划跟踪号");
            ItemList.Add("MaterialCode", "物料编码");
            ItemList.Add("MaterialName", "物料名称");
            ItemList.Add("Standard", "规格型号");
            ItemList.Add("Attribute", "材质");
            ItemList.Add("GB", "国标");
            ItemList.Add("Length", "长");
            ItemList.Add("Width", "宽");
            ItemList.Add("Fixed", "是否定尺");
            ItemList.Add("LotNumber", "批号");
            ItemList.Add("Warehouse", "仓库");
            ItemList.Add("Location", "仓位");
            ItemList.Add("Number", "数量");
            ItemList.Add("PlanMode", "计划模式");
            ItemList.Add("CGMODE", "标识号");
            ItemList.Add("Note", "备注");
            ItemList.Add("OrderCode", "订单号");            
            
            return ItemList;
        }

        protected void GridViewSearch_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                ddl.DataTextField = "value";
                ddl.DataValueField = "key";
                ddl.DataSource = bindItemList();
                ddl.DataBind();

                if (gr.RowIndex == 0)
                {
                    (gr.FindControl("tb_logic") as TextBox).Visible = false;
                }
            }
        }

        protected string GetSubCondtion()
        {
            string subCondition = "";

            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                if (gr.RowIndex == 0)
                {

                    DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                    if (ddl.SelectedValue != "NO")
                    {
                        TextBox txtValue = (gr.FindControl("TextBoxValue") as TextBox);

                        DropDownList ddlRel = (gr.FindControl("DropDownListRelation") as DropDownList);

                        subCondition = ConvertRelation(ddl.SelectedValue, ddlRel.SelectedValue, txtValue.Text.Trim());
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                    if (ddl.SelectedValue != "NO")
                    {
                        DropDownList ddlLogic = (gr.FindControl("DropDownListLogic") as DropDownList);

                        TextBox txtValue = (gr.FindControl("TextBoxValue") as TextBox);

                        DropDownList ddlRel = (gr.FindControl("DropDownListRelation") as DropDownList);

                        subCondition += " " + ddlLogic.SelectedValue + " " + ConvertRelation(ddl.SelectedValue, ddlRel.SelectedValue, txtValue.Text.Trim());
                    }

                    else
                    {
                        break;
                    }
                }
            }
            return subCondition;
        }

        private string ConvertRelation(string field, string relation, string fieldValue)
        {
            string obj = string.Empty;

            switch (relation)
            {
                case "0":
                    {
                        //包含

                        obj = field + "  LIKE  '%" + fieldValue + "%'";
                        break;
                    }
                case "1":
                    {
                        //等于
                        obj = field + "  =  '" + fieldValue + "'";
                        break;
                    }
                case "2":
                    {
                        //不等于
                        obj = field + "  !=  '" + fieldValue + "'";
                        break;
                    }
                case "3":
                    {
                        //大于
                        obj = field + "  >  '" + fieldValue + "'";
                        break;
                    }
                case "4":
                    {
                        //大于或等于
                        obj = field + "  >=  '" + fieldValue + "'";
                        break;
                    }
                case "5":
                    {
                        //小于
                        obj = field + "  <  '" + fieldValue + "'";
                        break;
                    }
                case "6":
                    {
                        //小于或等于
                        obj = field + "  <=  '" + fieldValue + "'";
                        break;
                    }
                case "7":
                    {
                        //不包含
                        obj = field + " NOT LIKE  '%" + fieldValue + "%'";
                        break;
                    }
                case "8":
                    {
                        //左包含
                        obj = field + "  LIKE  '" + fieldValue + "%'";
                        break;
                    }
                case "9":
                    {
                        //右包含
                        obj = field + "  LIKE  '%" + fieldValue + "'";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return obj;

        }

        private void resetSubcondition()
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                DropDownList ddl = gr.FindControl("DropDownListName") as DropDownList;
                foreach (ListItem lt in ddl.Items)
                {
                    if (lt.Selected)
                        lt.Selected = false;
                }
                ddl.Items[0].Selected = true;
                (gr.FindControl("TextBoxValue") as TextBox).Text = string.Empty; ;
            }

            refreshStyle();

        }
        private void refreshStyle()
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                if ((gr.FindControl("DropDownListName") as DropDownList).SelectedValue != "NO")
                {
                    if (gr.RowIndex != 0)
                    {
                        (gr.FindControl("DropDownListLogic") as DropDownList).Style.Add("display", "block");
                        (gr.FindControl("tb_logic") as TextBox).Style.Add("display", "none");
                    }

                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "block");
                    (gr.FindControl("tb_name") as TextBox).Style.Add("display", "none");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "block");
                    (gr.FindControl("tb_relation") as TextBox).Style.Add("display", "none");
                }
                else
                {
                    (gr.FindControl("DropDownListLogic") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_logic") as TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_name") as TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_relation") as TextBox).Style.Add("display", "block");
                }
            }
        }

        #endregion



        //导出二维码信息
        protected void btn_QRExport_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            string condition = GetStrCondition();
            if (condition != "")
            {
                sqltext = "select distinct MaterialCode,MaterialName,Attribute,GB,Standard AS MaterialStandard,Length,Width,Warehouse,Location AS Position,Unit from (select * from View_SM_Storage as a left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as b on a.PTC=b.PTCFrom and a.MaterialCode=b.TzMaterialCode)t where " + condition;
            }
            else
            {
                sqltext = "select distinct MaterialCode,MaterialName,Attribute,GB,Standard AS MaterialStandard,Length,Width,Warehouse,Location AS Position,Unit from (select * from View_SM_Storage as a left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as b on a.PTC=b.PTCFrom and a.MaterialCode=b.TzMaterialCode)t";
            }
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 500)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导出条数大于500，请分批导出！');", true);
                return;
            }
            string filename = "库存二维码信息" + DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("库存二维码信息导出模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    row.HeightInPoints = 14;//行高
                    row.CreateCell(0).SetCellValue(dt.Rows[i]["MaterialCode"].ToString());//物料编码
                    row.CreateCell(1).SetCellValue(dt.Rows[i]["MaterialName"].ToString());//物料名称
                    row.CreateCell(2).SetCellValue(dt.Rows[i]["MaterialStandard"].ToString());//型号规格
                    row.CreateCell(3).SetCellValue(dt.Rows[i]["Attribute"].ToString());//材质
                    row.CreateCell(4).SetCellValue(dt.Rows[i]["GB"].ToString());//国标
                    row.CreateCell(5).SetCellValue(dt.Rows[i]["Length"].ToString());//长
                    row.CreateCell(6).SetCellValue(dt.Rows[i]["Width"].ToString());//宽
                    row.CreateCell(7).SetCellValue(dt.Rows[i]["Unit"].ToString());//单位
                    row.CreateCell(8).SetCellValue(dt.Rows[i]["Warehouse"].ToString());//仓库
                    row.CreateCell(9).SetCellValue(dt.Rows[i]["Position"].ToString());//仓位
                    NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                    font1.FontName = "仿宋";//字体
                    font1.FontHeightInPoints = 11;//字号
                    ICellStyle cells = wk.CreateCellStyle();
                    cells.SetFont(font1);
                    cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                    for (int j = 0; j <= 9; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }
                }

                #endregion

                sheet0.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
    }
}
