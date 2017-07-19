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

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_TempInManage : BasicPage
    {

        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {

            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
                bindWarhouse();
                bindXposition();
                getWGInfo(true);
                bindGV();//绑定条件框
                
            }
            CheckUser(ControlFinder);
        }
        //仓库绑定
        protected void bindWarhouse()
        {
            string sqltext = "select distinct WS_ID,WS_NAME from TBWS_WAREHOUSE where WS_FATHERID!='ROOT'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            drp_Warhouse.DataSource = dt;
            drp_Warhouse.DataTextField = "WS_NAME";
            drp_Warhouse.DataValueField = "WS_ID";
            drp_Warhouse.DataBind();
            drp_Warhouse.Items.Insert(0, new ListItem("请选择", "请选择"));
            drp_Warhouse.SelectedIndex = 0;
        }
        protected void bindXposition()
        {
            if (drp_Warhouse.SelectedItem.Text != "请选择")
            {
                string sql = "SELECT DISTINCT WL_ID,WL_NAME FROM TBWS_LOCATION WHERE WL_WSID='" + drp_Warhouse.SelectedValue + "' ORDER BY WL_NAME";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                drp_xposition.DataSource = dt;
                drp_xposition.DataTextField = "WL_NAME";
                drp_xposition.DataValueField = "WL_ID";
                drp_xposition.DataBind();
            }
            else
            {
                drp_xposition.Items.Clear();
            }
            ListItem item = new ListItem("请选择", "0");
            drp_xposition.Items.Insert(0, item);
        }
        protected void drp_Warhouse_Changed(object sender, EventArgs e)
        {
            bindXposition();
        }
        
        private string GetStrCondition()
        {

            string state = DropDownListState.SelectedValue;//审核状态
            string colour = DropDownListColour.SelectedValue;//颜色
            string condition = "";

            //单号条件
            if (TextBoxCodeWG.Text != "")
            {
                condition = " WG_CODE LIKE '%" + TextBoxCodeWG.Text.Trim().PadLeft(9, '0') + "%'";
            }

            //物料代码条件
            if ((TextBoxMCodeWG.Text != "") && (condition != ""))
            {
                condition += " AND " + " WG_MARID LIKE '" + TextBoxMCodeWG.Text.Trim() + "%'";
            }
            else if ((TextBoxMCodeWG.Text != "") && (condition == ""))
            {
                condition += " WG_MARID LIKE '" + TextBoxMCodeWG.Text.Trim() + "%'";
            }

            //物料名称条件
            if ((TextBoxMNameWG.Text != "") && (condition != ""))
            {
                condition += " AND " + "MNAME LIKE '%" + TextBoxMNameWG.Text.Trim() + "%'";
            }
            else if ((TextBoxMNameWG.Text != "") && (condition == ""))
            {
                condition += " MNAME LIKE '%" + TextBoxMNameWG.Text.Trim() + "%'";
            }

            //规格型号条件
            if ((TextBoxMStandardWG.Text != "") && (condition != ""))
            {
                condition += " AND " + " GUIGE LIKE '%" + TextBoxMStandardWG.Text.Trim() + "%'";
            }
            else if ((TextBoxMStandardWG.Text != "") && (condition == ""))
            {
                condition += " GUIGE LIKE '%" + TextBoxMStandardWG.Text.Trim() + "%'";
            }

            //制单人条件
            if ((TextBoxZDR.Text != "") && (condition != ""))
            {
                condition += " AND " + " DocName LIKE '%" + TextBoxZDR.Text.Trim() + "%'";
            }
            else if ((TextBoxZDR.Text != "") && (condition == ""))
            {
                condition += " DocName LIKE '%" + TextBoxZDR.Text.Trim() + "%'";
            }

            //制单时间条件
            if ((TextBoxDateWG.Text != "") && (condition != ""))
            {
                condition += " AND " + " WG_DATE LIKE '%" + TextBoxDateWG.Text.Trim() + "%'";
            }
            else if ((TextBoxDateWG.Text != "") && (condition == ""))
            {
                condition += " WG_DATE LIKE '%" + TextBoxDateWG.Text.Trim() + "%'";
            }
            //材质
            if ((TextBoxCAIZHI.Text != "") && (condition != ""))
            {
                condition += " AND " + " CAIZHI LIKE '%" + TextBoxCAIZHI.Text.Trim() + "%'";
            }
            else if ((TextBoxCAIZHI.Text != "") && (condition == ""))
            {
                condition += " CAIZHI LIKE '%" + TextBoxCAIZHI.Text.Trim() + "%'";
            }
            //实收数量
            if ((TextBoxSHnum.Text != "") && (condition != ""))
            {
                condition += " AND " + " WG_RSNUM LIKE '%" + TextBoxSHnum.Text.Trim() + "%'";
            }
            else if ((TextBoxSHnum.Text != "") && (condition == ""))
            {
                condition += " WG_RSNUM LIKE '%" + TextBoxSHnum.Text.Trim() + "%'";
            }
            //单价
            if ((TextBoxUprice.Text != "") && (condition != ""))
            {
                condition += " AND " + " WG_UPRICE LIKE '%" + TextBoxUprice.Text.Trim() + "%'";
            }
            else if ((TextBoxUprice.Text != "") && (condition == ""))
            {
                condition += " WG_UPRICE LIKE '%" + TextBoxUprice.Text.Trim() + "%'";
            }
            //金额
            if ((TextBoxSum.Text != "") && (condition != ""))
            {
                condition += " AND " + " WG_AMOUNT LIKE '%" + TextBoxSum.Text.Trim() + "%'";
            }
            else if ((TextBoxSum.Text != "") && (condition == ""))
            {
                condition += " WG_AMOUNT LIKE '%" + TextBoxSum.Text.Trim() + "%'";
            }
            //收料仓库
            if (!(drp_Warhouse.SelectedIndex == 0 | drp_Warhouse.SelectedIndex == -1) && (condition != ""))
            {
                condition += " AND  WS_NAME= '" + drp_Warhouse.SelectedItem.ToString() + "'";
            }
            else if (!(drp_Warhouse.SelectedIndex == 0 | drp_Warhouse.SelectedIndex == -1) && (condition == ""))
            {
                condition += " WS_NAME = '" + drp_Warhouse.SelectedItem.ToString() + "'";
            }
            //仓位
            if (!(drp_xposition.SelectedIndex == 0 | drp_xposition.SelectedIndex == -1) && (condition != ""))
            {
                condition += " AND  WL_NAME= '" + drp_xposition.SelectedItem.ToString() + "'";
            }
            else if (!(drp_xposition.SelectedIndex == 0 | drp_xposition.SelectedIndex == -1) && (condition == ""))
            {
                condition += " WL_NAME = '" + drp_xposition.SelectedItem.ToString() + "'";
            }
             //备注条件
            if ((TextBoxDetailNote.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " WG_NOTE LIKE '%" + TextBoxDetailNote.Text.Trim() + "%'";
            }
            else if ((TextBoxDetailNote.Text != "") && (condition == ""))
            {
                condition += " WG_NOTE LIKE '%" + TextBoxDetailNote.Text.Trim() + "%'";
            }

            

            switch (state)
            {
                case "": break;
                case "1":
                    if (condition != "") { condition += " AND WG_STATE='1' "; }
                    else { condition += " WG_STATE='1' "; }
                    break;
                case "2":
                    if (condition != "") { condition += " AND WG_STATE='2' "; }
                    else { condition += " WG_STATE='2' "; }
                    break;
                default: break;
            }

            //红蓝字条件
            switch (colour)
            {
                case "": break;
                case "0":
                    //蓝
                    if (condition != "") { condition += " AND WG_ROB='0'"; }
                    else { condition += " WG_ROB='0'"; }
                    break;
                case "1":
                    //红
                    if (condition != "") { condition += " AND WG_ROB='1'"; }
                    else { condition += " WG_ROB='1'"; }
                    break;
                default: break;
            }
            if (condition != "") { condition += " AND WG_BILLTYPE='1' "; }

            else { condition += " WG_BILLTYPE='1' "; }



            //审核时间条件

            if ((TextBoxStartDate.Text.Trim() == "") && (TextBoxEndDate.Text.Trim() == "") && (condition != ""))
            {
                condition += " AND " + " WG_VERIFYDATE >= '" + TextBoxStartDate.Text.Trim() + "'";//全部
            }
            if ((TextBoxStartDate.Text.Trim() == "") && (TextBoxEndDate.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " WG_VERIFYDATE <= '" + TextBoxEndDate.Text.Trim() + " 24:00:00'";//到24点结束
            }
            if ((TextBoxStartDate.Text.Trim() != "") && (TextBoxEndDate.Text.Trim() == "") && (condition != ""))
            {
                condition += " AND " + " WG_VERIFYDATE >= '" + TextBoxStartDate.Text.Trim() + "'";//从零时开始
            }
            if ((TextBoxStartDate.Text.Trim() != "") && (TextBoxEndDate.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " WG_VERIFYDATE between '" + TextBoxStartDate.Text.Trim() + "' and '" + TextBoxEndDate.Text.Trim() + " 24:00:00'";
            }
            string subcondition = GetSubCondtion();
            if (condition != "")
            {
                if (subcondition != "")
                    condition += DropDownListFatherLogic.SelectedValue + " (" + subcondition + ")";
            }
            else
            {
                if (subcondition != "")
                    condition += subcondition;
            }
            return condition;

        }


        protected void getWGInfo(bool isFristPage)
        {



            string condition = GetStrCondition();


            GetTotalAmount(condition);

            string TableName = "View_SM_IN";
            string PrimaryKey = "WG_ID";
            string ShowFields = "WG_CODE AS Code,SupplierName AS Supplier,WG_WAREHOUSE AS WarehouseCode,WS_NAME AS Warehouse,WL_NAME,WG_DATE AS Date,left(WG_VERIFYDATE,10) AS VerifyDate,DocName,VerfierName,WG_STATE AS State,WG_CAVFLAG AS HXState,WG_GJSTATE AS GJState,WG_UNIQUEID AS UniqueID,WG_MARID AS MaterialCode,MNAME AS MaterialName,GUIGE AS MaterialStandard,CAIZHI AS Attribute,CGDW AS Unit,cast(WG_RSNUM as float) AS RN,cast(WG_UPRICE as float) AS UnitPrice,cast(WG_AMOUNT as float) AS Amount,WG_PTCODE AS PTC,WG_ORDERID,WG_NOTE AS Comment ";
            string OrderField = "WG_CODE DESC,WG_UNIQUEID";
            int OrderType = 0;
            string StrWhere = condition;
            int PageSize = 50;

            InitVar(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize, isFristPage);

        }

        private void GetTotalAmount(string strWhere)
        {
            string sql = "select isnull(CAST(sum(WG_RSNUM) AS FLOAT),0) as TotalRN,isnull(CAST(round(sum(WG_AMOUNT),2) AS FLOAT),0) as TotalAmount from View_SM_IN where " + strWhere;

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.Read())
            {
                hfdTotalNum.Value = sdr["TotalRN"].ToString();
                hfdTotalAmount.Value = sdr["TotalAmount"].ToString();
            }
            sdr.Close();

        }




        private void InitVar(string tableName, string PrimaryKey, string ShowFields, string OrderField, int OrderType, string StrWhere, int PageSize, bool isFristPage)
        {

            InitPager(tableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);//初始化页面

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
            getWGInfo(false);
        }

        protected void bindData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;

            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);

            CommonFun.Paging(dt, RepeaterWG, UCPaging1, NoDataPanel);

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


        protected void RepeaterWG_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("TotalNum")).Text = hfdTotalNum.Value;
                ((Label)e.Item.FindControl("TotalAmount")).Text = hfdTotalAmount.Value;
            }
        }

        private void BindItem()
        {

            for (int i = 0; i < (RepeaterWG.Items.Count - 1); i++)
            {

                Label lbCode = (RepeaterWG.Items[i].FindControl("LabelCode") as Label);

                string NextCode = lbCode.Text;

                if (lbCode.Visible)
                {
                    for (int j = i + 1; j < RepeaterWG.Items.Count; j++)
                    {
                        string Code = (RepeaterWG.Items[j].FindControl("LabelCode") as Label).Text;

                        if (NextCode == Code)
                        {
                            (RepeaterWG.Items[j].FindControl("LabelCode") as Label).Visible = false;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }



        protected string convertSH(string state)
        {
            switch (state)
            {
                case "1": return "未审核";
                case "2": return "已审核";
                default: return state;
            }
        }

        protected string convertGJ(string state)
        {
            switch (state)
            {
                case "0": return "未勾稽";
                case "1": return "待勾稽";
                case "2": return "已勾稽";
                default: return state;
            }
        }

        protected string convertHX(string state)
        {
            switch (state)
            {
                case "0": return "未核销";
                case "1": return "已核销";
                default: return state;
            }
        }

        //查询

        protected void Query_Click(object sender, EventArgs e)
        {

            getWGInfo(true);//表示当前页为第一页

            ModalPopupExtenderSearch.Hide();

            UpdatePanelBody.Update();

            refreshStyle();
        }

        //关闭
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();

        }

        //重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            resetSubcondition();
            clearCondition();
        }

          //清除条件
        private void clearCondition()
        {
            //审核状态
            DropDownListState.ClearSelection();
            DropDownListState.Items[0].Selected = true;
            DropDownListColour.ClearSelection();
            DropDownListColour.Items[0].Selected = true;
            //入库单编号
            TextBoxCodeWG.Text = string.Empty;
            TextBoxMCodeWG.Text = string.Empty;
            TextBoxMNameWG.Text = string.Empty;
            TextBoxMStandardWG.Text = string.Empty;
            TextBoxDateWG.Text = string.Empty;
            TextBoxZDR.Text = string.Empty;
            TextBoxCAIZHI.Text = string.Empty;
            TextBoxSum.Text = string.Empty;
            TextBoxSHnum.Text = string.Empty;
            TextBoxUprice.Text = string.Empty;
            drp_xposition.SelectedIndex = 0;
            drp_Warhouse.SelectedIndex = 0;
            TextBoxDetailNote.Text = string.Empty;
            TextBoxEndDate.Text = string.Empty;
            TextBoxStartDate.Text = string.Empty;
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
            ItemList.Add("WG_CODE", "入库单号");
            ItemList.Add("WG_PTCODE", "计划跟踪号");
            ItemList.Add("WG_MARID", "物料编码");
            ItemList.Add("MNAME", "物料名称");
            ItemList.Add("GUIGE", "规格型号");
            ItemList.Add("CAIZHI", "材质");
            ItemList.Add("DocName", "制单人");
            ItemList.Add("VerfierName", "审核人");
            ItemList.Add("WG_DATE", "制单日期");
            ItemList.Add("WG_VERIFYDATE", "审核日期");
            ItemList.Add("WS_NAME", "仓库");
            ItemList.Add("WL_NAME", "仓位");
            ItemList.Add("WG_RSNUM", "实收数量");
            ItemList.Add("WG_NOTE", "备注");
            ItemList.Add("WG_AMOUNT", "金额");
            ItemList.Add("WG_LOTNUM","批号");
            
            
            //ItemList.Add("WG_LENGTH", "长");
            //ItemList.Add("WG_WIDTH", "宽");
            // ItemList.Add("WG_LOTNUM", "批号");
            //ItemList.Add("DepName", "部门");
            // ItemList.Add("ReveicerName", "收料人");           
            //ItemList.Add("WG_PMODE", "计划模式");
            // ItemList.Add("WG_CGMODE", "标识号");           

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

            if (field == "WG_CODE")
            {
                fieldValue = fieldValue.PadLeft(9, '0');
            }


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

        //导出
        //库存=0；入库单=1；结转备库=2；领料单=3；项目完工=8(暂时未用)，项目结转=9

        protected void BtnShowExport_Click(object sender, EventArgs e)
        {
            string condition = GetStrCondition().Replace("'", "^");
            List<string> sqllist = new List<string>();
            string sql = "delete from TBWS_EXPORTCONDITION where SessionID='" + Session["UserID"].ToString() + "' AND Type='2'";
            sqllist.Add(sql);
            sql = "insert into TBWS_EXPORTCONDITION (SessionID,Type,StrCondition) VALUES ('" + Session["UserID"].ToString() + "','2','" + condition + "')";
            sqllist.Add(sql);
            DBCallCommon.ExecuteTrans(sqllist);

            ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>inexport();</script>");
        }
    }
}
