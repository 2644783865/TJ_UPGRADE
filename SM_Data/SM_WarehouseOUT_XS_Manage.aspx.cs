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
    public partial class SM_WarehouseOUT_XS_Manage : BasicPage
    {

        private float tdn = 0;
        private float trn = 0;
        private float tc = 0;
        private float ta = 0;

        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {

            InitVar();
            if (!IsPostBack)
            {
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
                bindData();
                bindGV();//绑定条件
                this.Form.DefaultButton = btnQuery.UniqueID;
               
            }
            CheckUser(ControlFinder);
        }

        private void InitVar()
        {
            getXSInfo();
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

            CommonFun.Paging(dt, RepeaterXS, UCPaging1, NoDataPanel);

            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }

            if (!CheckBoxShow.Checked)
            {
                BindItem();
            }
        }
        private string GetStrCondition() 
        {
            string condition = " BillType='3' ";
            string state = DropDownListState3.SelectedValue.ToString();
            string colour = DropDownListColour3.SelectedValue.ToString();

            //单号条件

            if (TextBoxCode3.Text != "")
            {
                condition += " AND Code LIKE '%" + TextBoxCode3.Text.Trim() + "%'";
            }

            if (TextBoxNewCode.Text != "")
            {
                condition += " AND id = '" + TextBoxNewCode.Text.Trim() + "'";
            }

            //时间条件

            if ((TextBoxStartDate.Text.Trim() == "") && (TextBoxDate.Text.Trim() == "") && (condition != ""))
            {
                condition += " AND " + " VerifierDate >= '" + TextBoxStartDate.Text.Trim() + "'";
            }
            if ((TextBoxStartDate.Text.Trim() == "") && (TextBoxDate.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " VerifierDate <= '" + TextBoxDate.Text.Trim() + " 24:00:00'";//到24点结束
            }
            if ((TextBoxStartDate.Text.Trim() != "") && (TextBoxDate.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " VerifierDate between '" + TextBoxStartDate.Text.Trim() + "' and '" + TextBoxDate.Text.Trim() + " 24:00:00'";//到24点结束
            }
            if ((TextBoxStartDate.Text.Trim() != "") && (TextBoxDate.Text.Trim() == "") && (condition != ""))
            {
                condition += " AND " + " VerifierDate >= '" + TextBoxStartDate.Text.Trim() + "'";
            }

            //制单时间条件
            if ((TextBoxDate3.Text != "") && (condition != ""))
            {
                condition += " AND " + " Date LIKE '%" + TextBoxDate3.Text.Trim() + "%'";
            }
            else if ((TextBoxDate3.Text != "") && (condition == ""))
            {
                condition += " Date LIKE '%" + TextBoxDate3.Text.Trim() + "%'";
            }

            //加工单位条件
            if ((TextBoxCompany3.Text != "") && (condition != ""))
            {
                condition += " AND " + " Company LIKE '%" + TextBoxCompany3.Text.Trim() + "%'";
            }
            else if ((TextBoxCompany3.Text != "") && (condition == ""))
            {
                condition += " Company LIKE '%" + TextBoxCompany3.Text.Trim() + "%'";
            }
            //发料仓库条件
            if ((TextBoxWarehouse3.Text != "") && (condition != ""))
            {
                condition += " AND " + " Warehouse LIKE'%" + TextBoxWarehouse3.Text.Trim() + "%'";
            }
            else if ((TextBoxWarehouse3.Text != "") && (condition == ""))
            {
                condition += " Warehouse LIKE '%" + TextBoxWarehouse3.Text.Trim() + "%'";
            }
            //物料代码条件
            if ((TextBoxMaterialCode3.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialCode LIKE '%" + TextBoxMaterialCode3.Text.Trim() + "%'";
            }
            else if ((TextBoxMaterialCode3.Text != "") && (condition == ""))
            {
                condition += " MaterialCode LIKE '%" + TextBoxMaterialCode3.Text.Trim() + "%'";
            }
            //物料名称条件
            if ((TextBoxMaterialName3.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialName LIKE '%" + TextBoxMaterialName3.Text.Trim() + "%'";
            }
            else if ((TextBoxMaterialName3.Text != "") && (condition == ""))
            {
                condition += " MaterialName LIKE '%" + TextBoxMaterialName3.Text.Trim() + "%'";
            }
            //规格型号条件
            if ((TextBoxStandard3.Text != "") && (condition != ""))
            {
                condition += " AND " + " Standard LIKE '%" + TextBoxStandard3.Text.Trim() + "%'";
            }
            else if ((TextBoxStandard3.Text != "") && (condition == ""))
            {
                condition += " Standard LIKE '%" + TextBoxStandard3.Text.Trim() + "%'";
            }
            //计划跟踪号条件
            if ((TextBoxPTC3.Text != "") && (condition != ""))
            {
                condition += " AND " + " PTC LIKE '%" + TextBoxPTC3.Text.Trim() + "%'";
            }
            else if ((TextBoxPTC3.Text != "") && (condition == ""))
            {
                condition += " PTC LIKE '%" + TextBoxPTC3.Text.Trim() + "%'";
            }

            //制单人

            if ((TextBoxZDR3.Text != "") && (condition != ""))
            {
                condition += " AND " + " Doc LIKE '%" + TextBoxZDR3.Text.Trim() + "%'";
            }
            else if ((TextBoxZDR3.Text != "") && (condition == ""))
            {
                condition += " Doc LIKE '%" + TextBoxZDR3.Text.Trim() + "%'";
            }

            //材质

            if ((TextBoxCZ3.Text != "") && (condition != ""))
            {
                condition += " AND " + " Attribute LIKE '%" + TextBoxCZ3.Text.Trim() + "%'";
            }
            else if ((TextBoxCZ3.Text != "") && (condition == ""))
            {
                condition += " Attribute LIKE '%" + TextBoxCZ3.Text.Trim() + "%'";
            }
            //业务员

            if ((TextBoxYWuYuan.Text != "") && (condition != ""))
            {
                condition += " AND " + " Clerk LIKE '%" + TextBoxYWuYuan.Text.Trim() + "%'";
            }
            else if ((TextBoxYWuYuan.Text != "") && (condition == ""))
            {
                condition += " Clerk LIKE '%" + TextBoxYWuYuan.Text.Trim() + "%'";
            }

            //审核人
            if ((TextBoxSHHRen.Text != "") && (condition != ""))
            {
                condition += " AND " + " Verifier LIKE '%" + TextBoxSHHRen.Text.Trim() + "%'";
            }
            else if ((TextBoxSHHRen.Text != "") && (condition == ""))
            {
                condition += " Verifier LIKE '%" + TextBoxSHHRen.Text.Trim() + "%'";
            }

            
            //批号
            if ((TextBoxpihao.Text != "") && (condition != ""))
            {
                condition += " AND " + " LotNumber LIKE '%" + TextBoxpihao.Text.Trim() + "%'";
            }
            else if ((TextBoxpihao.Text != "") && (condition == ""))
            {
                condition += " LotNumber LIKE '%" + TextBoxpihao.Text.Trim() + "%'";
            }
            //数量
            if ((TextBoxnum.Text != "") && (condition != ""))
            {
                condition += " AND " + " RealNumber LIKE '%" + TextBoxnum.Text.Trim() + "%'";
            }
            else if ((TextBoxnum.Text != "") && (condition == ""))
            {
                condition += " RealNumber LIKE '%" + TextBoxnum.Text.Trim() + "%'";
            }
            //单价
            if ((TextBoxunitprice.Text != "") && (condition != ""))
            {
                condition += " AND " + " UnitPrice LIKE '%" + TextBoxunitprice.Text.Trim() + "%'";
            }
            else if ((TextBoxunitprice.Text != "") && (condition == ""))
            {
                condition += " UnitPrice LIKE '%" + TextBoxunitprice.Text.Trim() + "%'";
            }
            //金额
            if ((TextBoxsum.Text != "") && (condition != ""))
            {
                condition += " AND " + " Amount LIKE '%" + TextBoxsum.Text.Trim() + "%'";
            }
            else if ((TextBoxsum.Text != "") && (condition == ""))
            {
                condition += " Amount LIKE '%" + TextBoxsum.Text.Trim() + "%'";
            }
            //备注条件
            if ((TextBoxDetailNote.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " DetailNote LIKE '%" + TextBoxDetailNote.Text.Trim() + "%'";
            }
            else if ((TextBoxDetailNote.Text != "") && (condition == ""))
            {
                condition += " DetailNote LIKE '%" + TextBoxDetailNote.Text.Trim() + "%'";
            }
            
            //红蓝字条件
            switch (colour)
            {
                case "": break;
                case "0":
                    if (condition != "") { condition += " AND ROB='0' "; }
                    else { condition += " ROB='0' "; }
                    break;
                case "1":
                    if (condition != "") { condition += " AND ROB='1' "; }
                    else { condition += " ROB='1' "; }
                    break;
                default: break;
            }
            //状态条件
            switch (state)
            {
                case "": break;
                case "1":
                    if (condition != "") { condition += " AND TotalState='1'  "; }
                    else { condition += " TotalState='1'  "; }
                    break;
                case "2":
                    if (condition != "") { condition += " AND TotalState='2'  "; }
                    else { condition += " TotalState='2' "; }
                    break;
                default: break;
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



        protected void getXSInfo()
        {
                       
            string condition = GetStrCondition();

            if (condition == "")
            {
                string TableName = "View_SM_OUTXS";
                string PrimaryKey = "OP_ID";
                string ShowFields = "id as TrueCode,Date AS Date,Verifier,left(VerifierDate,10) AS VerifierDate,TotalState AS State,Code AS Code,Company AS Company,WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,SQCODE AS SQCODE,UniqueCode AS UniqueID,PTC AS PTC,MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute AS Attribute,Standard AS MaterialStandard,Unit AS Unit,LotNumber AS LotNumber,cast(round(UnitPrice,4) as float) AS UnitPrice,DueNumber AS DN,cast(RealNumber as float) AS RN,cast(round(Amount,2) as float) AS Amount,cast(UnitCost as float) AS UnitCost,cast(Cost as float) AS Cost,Dep AS Dep,Clerk AS Clerk,DetailNote AS Note ";
                string OrderField = "id DESC,UniqueCode";
                int OrderType = 0;
                string StrWhere = "";
                int PageSize = 50;

                InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);

            }
            else
            {
                string TableName = "View_SM_OUTXS";
                string PrimaryKey = "OP_ID";
                string ShowFields = "id as TrueCode,Date AS Date,Verifier,left(VerifierDate,10) AS VerifierDate,TotalState AS State,Code AS Code,Company AS Company,WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,SQCODE AS SQCODE,UniqueCode AS UniqueID,PTC AS PTC,MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute AS Attribute,Standard AS MaterialStandard,Unit AS Unit,LotNumber AS LotNumber,cast(round(UnitPrice,4) as float) AS UnitPrice,DueNumber AS DN,cast(RealNumber as float) AS RN,cast(round(Amount,2) as float) AS Amount,cast(UnitCost as float) AS UnitCost,cast(Cost as float) AS Cost,Dep AS Dep,Clerk AS Clerk,DetailNote AS Note ";
                string OrderField = "id DESC,UniqueCode";
                int OrderType = 0;
                string StrWhere = condition;
                int PageSize = 50;

                InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);
             
            }

            string sql = "select isnull(cast(sum(RealNumber) as float),0) as TotalRN,isnull(cast(round(sum(Amount),2) as float),0) as TotalAmount,isnull(cast(round(sum(Cost),2) as float),0) as TotalCost from View_SM_OUTXS where " + condition;
            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);
            if (sdr.Read())
            {
                hfdTotalRN.Value = sdr["TotalRN"].ToString();
                hfdTotalAmount.Value = sdr["TotalAmount"].ToString();
                hfdTotalCost.Value = sdr["TotalCost"].ToString();
            }
            sdr.Close();

        }




        protected void RepeaterXS_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("LabelRN")).Text.ToString() != "")
                {
                    trn += Convert.ToSingle(((Label)e.Item.FindControl("LabelRN")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("LabelCost")).Text.ToString() != "")
                {
                    tc += Convert.ToSingle(((Label)e.Item.FindControl("LabelCost")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("LabelAmount")).Text.ToString() != "")
                {
                    ta += Convert.ToSingle(((Label)e.Item.FindControl("LabelAmount")).Text.ToString());
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("LabelTotalRN")).Text = trn.ToString();
                ((Label)e.Item.FindControl("LabelTotalCost")).Text = tc.ToString();
                ((Label)e.Item.FindControl("LabelTotalAmount")).Text = ta.ToString();

                ((Label)e.Item.FindControl("TotalRN")).Text = hfdTotalRN.Value.ToString();
                ((Label)e.Item.FindControl("TotalAmount")).Text = hfdTotalAmount.Value.ToString();
                ((Label)e.Item.FindControl("TotalCost")).Text = hfdTotalCost.Value.ToString();
              
            }

        }

        //查询
        protected void Query_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;

            bindData();

            refreshStyle();

            ModalPopupExtenderSearch.Hide();

            UpdatePanelBody.Update();
        }

        //关闭
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
            //UpdatePanelBody.Update();
        }
        //重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearCondition();
            resetSubcondition();
            //UpdatePanelBody.Update();
        }

        //单据头显示
        protected void CheckBoxShow_CheckedChanged(object sender, EventArgs e)
        {
            bindData();

            UpdatePanelBody.Update();
        }



        //导出
        //库存=0；入库单=1；结转备库=2；领料单=3；委外=4;销售=5

        protected void BtnShowExport_Click(object sender, EventArgs e)
        {
            string condition = GetStrCondition().Replace("'", "*");
            System.Collections.Generic.List<string> sqllist = new System.Collections.Generic.List<string>();
            string sql = "delete from TBWS_EXPORTCONDITION where SessionID='" + Session["UserID"].ToString() + "' AND Type='5'";
            sqllist.Add(sql);
            sql = "insert into TBWS_EXPORTCONDITION (SessionID,Type,StrCondition) VALUES ('" + Session["UserID"].ToString() + "','5','" + condition + "')";
            sqllist.Add(sql);
            DBCallCommon.ExecuteTrans(sqllist);

            ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>OutExport();</script>");
        }


        //清除条件
        private void clearCondition()
        {

            //审核状态
            foreach (ListItem lt in DropDownListState3.Items)
            {
                if (lt.Selected)
                {
                    lt.Selected = false;
                }
            }
            DropDownListState3.Items[0].Selected = true;

            //红蓝字
            foreach (ListItem lt in DropDownListColour3.Items)
            {
                if (lt.Selected)
                {
                    lt.Selected = false;
                }
            }
            DropDownListColour3.Items[0].Selected = true;

            //领料单编号
            TextBoxCode3.Text = string.Empty;
            //加工单位
            TextBoxCompany3.Text = string.Empty;
            TextBoxWarehouse3.Text = string.Empty;
            TextBoxDate3.Text = string.Empty;
            TextBoxMaterialCode3.Text = string.Empty;
            TextBoxMaterialName3.Text = string.Empty;
            TextBoxStandard3.Text = string.Empty;
            TextBoxPTC3.Text = string.Empty;
            TextBoxZDR3.Text = string.Empty;
            TextBoxCZ3.Text = string.Empty;
            TextBoxNewCode.Text = string.Empty;
           
            TextBoxSHHRen.Text = string.Empty;
            TextBoxYWuYuan.Text = string.Empty;
            TextBoxpihao.Text = string.Empty;
            TextBoxunitprice.Text = string.Empty;
            TextBoxsum.Text = string.Empty;
            TextBoxnum.Text = string.Empty;
            TextBoxStartDate.Text = string.Empty;
            TextBoxDate.Text = string.Empty;
            TextBoxDetailNote.Text = string.Empty;
        }

        protected string convertState3(string state)
        {
            switch (state)
            {
                case "1": return "未审核";
                case "2": return "已审核";
                default: return state;
            }
        }

        private void BindItem()
        {

            for (int i = 0; i < (RepeaterXS.Items.Count - 1); i++)
            {

                Label lbCode = (RepeaterXS.Items[i].FindControl("LabelCode") as Label);

                string NextCode = lbCode.Text;

                if (lbCode.Visible)
                {
                    for (int j = i + 1; j < RepeaterXS.Items.Count; j++)
                    {
                        string Code = (RepeaterXS.Items[j].FindControl("LabelCode") as Label).Text;

                        if (NextCode == Code)
                        {
                            (RepeaterXS.Items[j].FindControl("LabelCode") as Label).Style.Add("display", "none");
                            (RepeaterXS.Items[j].FindControl("LabelTrueCode") as Label).Style.Add("display", "none");
                        }
                        else
                        {
                            break;
                        }
                    }
                }
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


            for (int i = 0; i < 9; i++)
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
            ItemList.Add("id", "新出库单号");
            ItemList.Add("OutCode", "旧出库单号");
            ItemList.Add("Company", "购货单位");
            ItemList.Add("PTC", "计划跟踪号");
            ItemList.Add("MaterialCode", "物料编码");
            ItemList.Add("MaterialName", "物料名称");
            ItemList.Add("Standard", "规格型号");
            ItemList.Add("Attribute", "材质");            
            ItemList.Add("GB", "国标");        
            ItemList.Add("Doc", "制单人");
            ItemList.Add("Date", "制单日期");
            ItemList.Add("Clerk", "业务员");
            ItemList.Add("Verifier", "审核人");
            ItemList.Add("VerifierDate", "审核日期");
            ItemList.Add("PlanMode", "计划模式");            
            ItemList.Add("Warehouse", "发料仓库");            
            ItemList.Add("RealNumber", "实发数量");
            ItemList.Add("UnitPrice", "单价");
            ItemList.Add("Amount", "金额");
            ItemList.Add("LotNumber", "批号");
            ItemList.Add("DetailNote", "备注");
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

            if (field == "OutCode")
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

    }
}
