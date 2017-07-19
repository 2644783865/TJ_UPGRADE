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
    public partial class SM_WarehouseOUT_WW_Manage : BasicPage
    {

        private double trn = 0;
        private double ta = 0;

        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {

            InitVar();

            if (!IsPostBack)
            {
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
                bindData();
                bindGV();//绑定条件框
                CheckUser(ControlFinder);

                this.Form.DefaultButton = btnQuery.UniqueID;
            }

            //((System.Web.UI.WebControls.Image)this.Master.FindControl("Image2")).Visible = false;
            //((System.Web.UI.WebControls.Image)this.Master.FindControl("Image3")).Visible = false;

        }


        private void InitVar()
        {
            getWWInfo();
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

            CommonFun.Paging(dt, RepeaterWW, UCPaging1, NoDataPanel);

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
            string state = DropDownListState2.SelectedValue.ToString();
            string colour = DropDownListColour2.SelectedValue.ToString();

            string condition = " BillType='2' ";
            //单号条件
            if (TextBoxCode2.Text != "")
            {
                condition += " AND Code LIKE '%" + TextBoxCode2.Text.Trim().PadLeft(8, '0') + "%'";
            }

            if (TextBoxNewCode.Text != "")
            {
                condition += " AND id = '" + TextBoxNewCode.Text.Trim() + "'";
            }

            //审核时间条件

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

            //if ((TextBoxDate2.Text != "") && (condition != ""))
            //{
            //    condition += " AND " + " Date LIKE '%" + TextBoxDate2.Text.Trim() + "%'";
            //}
            //else if ((TextBoxDate2.Text != "") && (condition == ""))
            //{
            //    condition += " Date LIKE '%" + TextBoxDate2.Text.Trim() + "%'";
            //}

            //加工单位条件
            if ((TextBoxCompany2.Text != "") && (condition != ""))
            {
                condition += " AND " + " Company LIKE '%" + TextBoxCompany2.Text.Trim() + "%'";
            }
            else if ((TextBoxCompany2.Text != "") && (condition == ""))
            {
                condition += " Company LIKE '%" + TextBoxCompany2.Text.Trim() + "%'";
            }
            //发料仓库条件
            if ((TextBoxWarehouse2.Text != "") && (condition != ""))
            {
                condition += " AND " + " Warehouse LIKE'%" + TextBoxWarehouse2.Text.Trim() + "%'";
            }
            else if ((TextBoxWarehouse2.Text != "") && (condition == ""))
            {
                condition += " Warehouse LIKE '%" + TextBoxWarehouse2.Text.Trim() + "%'";
            }
            //物料代码条件
            if ((TextBoxMaterialCode2.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialCode LIKE '%" + TextBoxMaterialCode2.Text.Trim() + "%'";
            }
            else if ((TextBoxMaterialCode2.Text != "") && (condition == ""))
            {
                condition += " MaterialCode LIKE '%" + TextBoxMaterialCode2.Text.Trim() + "%'";
            }
            //物料名称条件
            if ((TextBoxMaterialName2.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialName LIKE '%" + TextBoxMaterialName2.Text.Trim() + "%'";
            }
            else if ((TextBoxMaterialName2.Text != "") && (condition == ""))
            {
                condition += " MaterialName LIKE '%" + TextBoxMaterialName2.Text.Trim() + "%'";
            }
            //规格型号条件
            if ((TextBoxStandard2.Text != "") && (condition != ""))
            {
                condition += " AND " + " Standard LIKE '%" + TextBoxStandard2.Text.Trim() + "%'";
            }
            else if ((TextBoxStandard2.Text != "") && (condition == ""))
            {
                condition += " Standard LIKE '%" + TextBoxStandard2.Text.Trim() + "%'";
            }
            //计划跟踪号条件
            if ((TextBoxPTC2.Text != "") && (condition != ""))
            {
                condition += " AND " + " PTC LIKE '%" + TextBoxPTC2.Text.Trim() + "%'";
            }
            else if ((TextBoxPTC2.Text != "") && (condition == ""))
            {
                condition += " PTC LIKE '%" + TextBoxPTC2.Text.Trim() + "%'";
            }


            //制单人

            if ((TextBoxZDR2.Text != "") && (condition != ""))
            {
                condition += " AND " + " Doc LIKE '%" + TextBoxZDR2.Text.Trim() + "%'";
            }
            else if ((TextBoxZDR2.Text != "") && (condition == ""))
            {
                condition += " Doc LIKE '%" + TextBoxZDR2.Text.Trim() + "%'";
            }
            //材质

            if ((TextBoxCZ2.Text != "") && (condition != ""))
            {
                condition += " AND " + " Attribute LIKE '%" + TextBoxCZ2.Text.Trim() + "%'";
            }
            else if ((TextBoxCZ2.Text != "") && (condition == ""))
            {
                condition += " Attribute LIKE '%" + TextBoxCZ2.Text.Trim() + "%'";
            }

            //生产制号
            if ((TextBoxEngid.Text != "") && (condition != ""))
            {
                condition += " AND " + " TSAID LIKE '%" + TextBoxEngid.Text.Trim().ToUpper() + "%'";
            }
            else if ((TextBoxEngid.Text != "") && (condition == ""))
            {
                condition += " TSAID LIKE '%" + TextBoxEngid.Text.Trim().ToUpper() + "%'";
            }

            //审核人
            if ((shenheRen.Text != "") && (condition != ""))
            {
                condition += " AND " + " Verifier LIKE '%" + shenheRen.Text.Trim() + "%'";
            }
            else if ((shenheRen.Text != "") && (condition == ""))
            {
                condition += " Verifier LIKE '%" + shenheRen.Text.Trim() + "%'";
            }

            //计划模式
            if ((JHmode.Text != "") && (condition != ""))
            {
                condition += " AND " + " PlanMode LIKE '%" + JHmode.Text.Trim().ToUpper() + "%'";
            }
            else if ((JHmode.Text != "") && (condition == ""))
            {
                condition += " PlanMode LIKE '%" + JHmode.Text.Trim().ToUpper() + "%'";
            }

            //实发数量
            if ((TextBoxRN.Text != "") && (condition != ""))
            {
                condition += " AND " + " RealNumber LIKE '%" + TextBoxRN.Text.Trim()+ "%'";
            }
            else if ((TextBoxRN.Text != "") && (condition == ""))
            {
                condition += " RealNumber LIKE '%" + TextBoxRN.Text.Trim() + "%'";
            }
             //单价
            if ((TextBoxUnitPrice.Text != "") && (condition != ""))
            {
                condition += " AND " + " UnitPrice LIKE '%" + TextBoxUnitPrice.Text.Trim()+ "%'";
            }
            else if ((TextBoxUnitPrice.Text != "") && (condition == ""))
            {
                condition += " UnitPrice LIKE '%" + TextBoxUnitPrice.Text.Trim() + "%'";
            }
            //金额
            if ((TextBoxAmount.Text != "") && (condition != ""))
            {
                condition += " AND " + " Amount LIKE '%" + TextBoxAmount.Text.Trim() + "%'";
            }
            else if ((TextBoxAmount.Text != "") && (condition == ""))
            {
                condition += " Amount LIKE '%" + TextBoxAmount.Text.Trim() + "%'";
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
                    if (condition != "") { condition += " AND TotalState='1' "; }
                    else { condition += " TotalState='1'  "; }
                    break;
                case "2":
                    if (condition != "") { condition += " AND TotalState='2' "; }
                    else { condition += " TotalState='2'  "; }
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

        protected void getWWInfo()
        {
            string condition = GetStrCondition();

            if (condition == "")
            {

                string TableName = "View_SM_OUTWW";
                string PrimaryKey = "OP_ID";
                string ShowFields = "id as TrueCode,Date,Doc,Verifier,left(VerifierDate,10) AS VerifierDate,TotalState AS State,Company,Code,ProcessType,WarehouseCode,Warehouse,SQCODE,UniqueCode AS UniqueID,MaterialCode,MaterialName,Standard AS MaterialStandard,Unit,cast(round(UnitPrice,4) as float) AS UnitPrice,cast(DueNumber as float) AS DN,cast(RealNumber as float) AS RN,cast(round(Amount,2) as float) AS Amount,OrderCode,PTC,DetailNote AS Note,TSAID,PlanMode ";
                string OrderField = "Code DESC,MaterialCode";
                int OrderType = 0;
                string StrWhere = "";
                int PageSize = 50;

                InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);

            }
            else
            {
                string TableName = "View_SM_OUTWW";
                string PrimaryKey = "OP_ID";
                string ShowFields = "id as TrueCode,Date,Doc,Verifier,left(VerifierDate,10) AS VerifierDate,TotalState AS State,Company,Code,ProcessType,WarehouseCode,Warehouse,SQCODE,UniqueCode AS UniqueID,MaterialCode,MaterialName,Standard AS MaterialStandard,Unit,cast(round(UnitPrice,4) as float) AS UnitPrice,cast(DueNumber as float) AS DN,cast(RealNumber as float) AS RN,cast(round(Amount,2) as float) AS Amount,OrderCode,PTC,DetailNote AS Note,TSAID,PlanMode ";
                string OrderField = "Code DESC,MaterialCode";
                int OrderType = 0;
                string StrWhere = condition;
                int PageSize = 50;

                InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);
            }

            string sql = "select isnull(cast(sum(RealNumber) as float),0) as TotalRN,isnull(cast(round(sum(Amount),2) as float),0) as TotalAmount from View_SM_OUTWW where " + condition;
            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);
            if (sdr.Read())
            {
                hfdTotalRN.Value = sdr["TotalRN"].ToString();
                hfdTotalAmount.Value = sdr["TotalAmount"].ToString();
            }
            sdr.Close();
        }

        protected void RepeaterWW_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("LabelRN")).Text.ToString() != "")
                {
                    trn += Convert.ToDouble(((Label)e.Item.FindControl("LabelRN")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("LabelAmount")).Text.ToString() != "")
                {
                    ta += Convert.ToDouble(((Label)e.Item.FindControl("LabelAmount")).Text.ToString());
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("LabelTotalRN")).Text =Math.Round(trn,4).ToString();
                ((Label)e.Item.FindControl("LabelTotalAmount")).Text = Math.Round(ta,2).ToString();

                ((Label)e.Item.FindControl("TotalRN")).Text = hfdTotalRN.Value;
                ((Label)e.Item.FindControl("TotalAmount")).Text = hfdTotalAmount.Value;
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
        }

        //单据头显示
        protected void CheckBoxShow_CheckedChanged(object sender, EventArgs e)
        {
            bindData();

            UpdatePanelBody.Update();
        }


        //导出
        //库存=0；入库单=1；结转备库=2；领料单=3；委外=4，项目完工=8(暂时未用)，项目结转=9

        protected void BtnShowExport_Click(object sender, EventArgs e)
        {
            string condition = GetStrCondition().Replace("'", "*");
            System.Collections.Generic.List<string> sqllist = new System.Collections.Generic.List<string>();
            string sql = "delete from TBWS_EXPORTCONDITION where SessionID='" + Session["UserID"].ToString() + "' AND Type='4'";
            sqllist.Add(sql);
            sql = "insert into TBWS_EXPORTCONDITION (SessionID,Type,StrCondition) VALUES ('" + Session["UserID"].ToString() + "','4','" + condition + "')";
            sqllist.Add(sql);
            DBCallCommon.ExecuteTrans(sqllist);

            ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>OutExport();</script>");
        }

        //清除条件
        private void clearCondition()
        {

            //审核状态
            foreach (ListItem lt in DropDownListState2.Items)
            {
                if (lt.Selected)
                {
                    lt.Selected = false;
                }
            }
            DropDownListState2.Items[0].Selected = true;

            //红蓝字
            foreach (ListItem lt in DropDownListColour2.Items)
            {
                if (lt.Selected)
                {
                    lt.Selected = false;
                }
            }
            DropDownListColour2.Items[0].Selected = true;

            //领料单编号
            TextBoxCode2.Text = string.Empty;
            //加工单位
            TextBoxCompany2.Text = string.Empty;
            TextBoxWarehouse2.Text = string.Empty;
            TextBoxStartDate.Text = string.Empty;
            TextBoxDate.Text = string.Empty;
            TextBoxMaterialCode2.Text = string.Empty;
            TextBoxMaterialName2.Text = string.Empty;
            TextBoxStandard2.Text = string.Empty;
            TextBoxPTC2.Text = string.Empty;
            TextBoxZDR2.Text = string.Empty;
            TextBoxCZ2.Text = string.Empty;
            TextBoxNewCode.Text = string.Empty;
            shenheRen.Text = string.Empty;
            JHmode.Text = string.Empty;
            TextBoxEngid.Text = string.Empty;
            TextBoxAmount.Text = string.Empty;
            TextBoxUnitPrice.Text = string.Empty;
            TextBoxRN.Text = string.Empty;      


        }



        protected string convertState2(string state)
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

            for (int i = 0; i < (RepeaterWW.Items.Count - 1); i++)
            {

                Label lbCode = (RepeaterWW.Items[i].FindControl("LabelCode") as Label);

                string NextCode = lbCode.Text;

                if (lbCode.Visible)
                {
                    for (int j = i + 1; j < RepeaterWW.Items.Count; j++)
                    {
                        string Code = (RepeaterWW.Items[j].FindControl("LabelCode") as Label).Text;

                        if (NextCode == Code)
                        {
                            (RepeaterWW.Items[j].FindControl("LabelTrueCode") as Label).Style.Add("display", "none");
                            (RepeaterWW.Items[j].FindControl("LabelCode") as Label).Style.Add("display", "none");
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


            for (int i = 0; i < 8; i++)
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
            ItemList.Add("Company", "加工单位");
            ItemList.Add("PTC", "计划跟踪号");             
            ItemList.Add("MaterialCode", "物料编码");
            ItemList.Add("MaterialName", "物料名称");
            ItemList.Add("Standard", "规格型号");
            ItemList.Add("Attribute", "材质");            
            ItemList.Add("GB", "国标");
            ItemList.Add("RealNumber", "实发数量");
            ItemList.Add("UnitPrice", "单价");
            ItemList.Add("Amount", "金额");
            ItemList.Add("Doc", "制单人");
            ItemList.Add("Date", "制单日期");            
            ItemList.Add("Verifier", "审核人");
            ItemList.Add("VerifierDate", "审核日期");            
            ItemList.Add("PlanMode", "计划模式");             
            ItemList.Add("Warehouse", "发料仓库"); 
            ItemList.Add("TSAID", "生产制号");
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
