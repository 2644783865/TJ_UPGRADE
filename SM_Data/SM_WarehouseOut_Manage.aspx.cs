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

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_WarehouseOut_Manage : System.Web.UI.Page
    {
        private float tdn = 0;
        private float trn = 0;
        private float tc = 0;
        private float ta = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //getLLInfo();
                initActiveTab();
            }
        }

        private void initActiveTab()
        {
            if (Request.QueryString["tab"] != null)
            {
                switch (Request.QueryString["tab"].ToString())
                {
                    case "1": TabContainer1.ActiveTab = Tab1; break;
                    case "2": TabContainer1.ActiveTab = Tab2; break;
                    case "3": TabContainer1.ActiveTab = Tab3; break;
                    case "4": TabContainer1.ActiveTab = Tab4; break;
                    default: break;
                }
            }
        }

        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            //switch (TabContainer1.ActiveTabIndex)
            //{

            //    case 0: getLLInfo(); break;
            //    case 1: getWWInfo(); break;
            //    case 2: getXSInfo(); break;

            //    case 3: getQTInfo(); break;

            //    default: break;

                    
            //}
        }

        /*********************************Tab1*************************************/
        
        protected void getLLInfo()
        {
            string state = DropDownListState.SelectedValue.ToString();
            string colour = DropDownListColour.SelectedValue.ToString();
            string sql = "";
            string condition = "";
            //单号条件
            if (TextBoxCode.Text != "")
            {
                condition = " OutCode LIKE '%" + TextBoxCode.Text.Trim() + "%'";
            }
            //时间条件
            if ((TextBoxDate.Text != "") && (condition != ""))
            {
                condition += " AND " + " Date LIKE '%" + TextBoxDate.Text.Trim() + "%'";
            }
            else if ((TextBoxDate.Text != "") && (condition == ""))
            {
                condition += " Date LIKE '%" + TextBoxDate.Text.Trim() + "%'";
            }
            //领料部门条件
            if ((TextBoxDep.Text != "") && (condition != ""))
            {
                condition += " AND " + " Dep LIKE '%" + TextBoxDep.Text.Trim() + "%'";
            }
            else if ((TextBoxDep.Text != "") && (condition == ""))
            {
                condition += " Dep LIKE '%" + TextBoxDep.Text.Trim() + "%'";
            }
            //发料仓库条件
            if ((TextBoxWarehouse.Text != "") && (condition != ""))
            {
                condition += " AND " + " Warehouse LIKE'%" + TextBoxWarehouse.Text.Trim() + "%'";
            }
            else if ((TextBoxWarehouse.Text != "") && (condition == ""))
            {
                condition += " Warehouse LIKE '%" + TextBoxWarehouse.Text.Trim() + "%'";
            }
            //物料代码条件
            if ((TextBoxMCode.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialCode LIKE '%" + TextBoxMCode.Text.Trim() + "%'";
            }
            else if ((TextBoxMCode.Text != "") && (condition == ""))
            {
                condition += " MaterialCode LIKE '%" + TextBoxMCode.Text.Trim() + "%'";
            }
            //物料名称条件
            if ((TextBoxMName.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialName LIKE '%" + TextBoxMName.Text.Trim() + "%'";
            }
            else if ((TextBoxMName.Text != "") && (condition == ""))
            {
                condition += " MaterialName LIKE '%" + TextBoxMName.Text.Trim() + "%'";
            }
            //规格型号条件
            if ((TextBoxMStandard.Text != "") && (condition != ""))
            {
                condition += " AND " + " Standard LIKE '%" + TextBoxMStandard.Text.Trim() + "%'";
            }
            else if ((TextBoxMStandard.Text != "") && (condition == ""))
            {
                condition += " Standard LIKE '%" + TextBoxMStandard.Text.Trim() + "%'";
            }
            //计划跟踪号条件
            if ((TextBoxMPTC.Text != "") && (condition != ""))
            {
                condition += " AND " + " PTC LIKE '%" + TextBoxMPTC.Text.Trim() + "%'";
            }
            else if ((TextBoxMPTC.Text != "") && (condition == ""))
            {
                condition += " PTC LIKE '%" + TextBoxMPTC.Text.Trim() + "%'";
            }

            //制单人

            if ((TextBoxZDR.Text != "") && (condition != ""))
            {
                condition += " AND " + " Doc LIKE '%" + TextBoxZDR.Text.Trim() + "%'";
            }
            else if ((TextBoxZDR.Text != "") && (condition == ""))
            {
                condition += " Doc LIKE '%" + TextBoxZDR.Text.Trim() + "%'";
            }
            //材质

            if ((TextBoxCZ.Text != "") && (condition != ""))
            {
                condition += " AND " + " Attribute LIKE '%" + TextBoxCZ.Text.Trim() + "%'";
            }
            else if ((TextBoxCZ.Text != "") && (condition == ""))
            {
                condition += " Attribute LIKE '%" + TextBoxCZ.Text.Trim() + "%'";
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
                    if (condition != "") { condition += " AND TotalState='1' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    else { condition += " TotalState='1' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    break;
                case "2":
                    if (condition != "") { condition += " AND TotalState='2' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    else { condition += " TotalState='2' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    break;
                default: break;
            }

            if (condition != "") { condition += " AND BillType='1' "; }
            else { condition += " BillType='1' "; }
            
            if (condition == "")
            {
                sql = "SELECT OutCode AS Code,DepCode AS DepCode,Dep AS Dep,WarehouseCode AS WarehouseCode,Warehouse AS Warehouse," +
                    "Date AS Date,SenderCode AS SenderCode,Sender AS Sender,DocCode AS DocCode,Doc AS Doc,VerifierCode AS VerifierCode," +
                    "Verifier AS Verifier,ApprovedDate AS ApproveDate,ROB AS Colour,TotalState AS State,TotalNote AS Comment," +
                    "SQCODE AS SQCODE,UniqueCode AS UniqueID,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Length AS Length,Width AS Width," +
                    "LotNumber AS LotNumber,Unit AS Unit,cast(UnitPrice as float) AS UnitPrice,DueNumber AS DN,cast(RealNumber as float) AS RN," +
                    "cast(Amount as float) AS Amount,WarehouseCode AS WarehouseOutCode,Warehouse AS WarehouseOut," +
                    "LocationCode AS PositionOutCode,Location AS PositionOut," +
                    "PlanMode AS PlanMode,PTC AS PTC,DetailNote AS Note FROM View_SM_OUT order by OutCode,MaterialCode";
            }
            else
            {
                sql = "SELECT OutCode AS Code,DepCode AS DepCode,Dep AS Dep,WarehouseCode AS WarehouseCode,Warehouse AS Warehouse," +
                    "Date AS Date,SenderCode AS SenderCode,Sender AS Sender,DocCode AS DocCode,Doc AS Doc,VerifierCode AS VerifierCode," +
                    "Verifier AS Verifier,ApprovedDate AS ApproveDate,ROB AS Colour,TotalState AS State,TotalNote AS Comment," +
                    "SQCODE AS SQCODE,UniqueCode AS UniqueID,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Length AS Length,Width AS Width," +
                    "LotNumber AS LotNumber,Unit AS Unit,cast(UnitPrice as float) AS UnitPrice,DueNumber AS DN,cast(RealNumber as float) AS RN," +
                    "cast(Amount as float) AS Amount,WarehouseCode AS WarehouseOutCode,Warehouse AS WarehouseOut," +
                    "LocationCode AS PositionOutCode,Location AS PositionOut," +
                    "PlanMode AS PlanMode,PTC AS PTC,DetailNote AS Note FROM View_SM_OUT WHERE " + condition + "order by OutCode,MaterialCode";
            }
            DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterLL.DataSource = tb;
            RepeaterLL.DataBind();
            if (tb.Rows.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            else
            {
                NoDataPanel.Visible = false;
            }
        }

        protected void RepeaterLL_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("LabelRN")).Text.ToString() != "")
                {
                    trn += Convert.ToSingle(((Label)e.Item.FindControl("LabelRN")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("LabelAmount")).Text.ToString() != "")
                {
                    ta += Convert.ToSingle(((Label)e.Item.FindControl("LabelAmount")).Text.ToString());
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("LabelTotalRN")).Text = trn.ToString();
                ((Label)e.Item.FindControl("LabelTotalAmount")).Text = ta.ToString();
            }
        }       
        
        protected void DropDownListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            getLLInfo();
        }

        protected void DropDownListColour_SelectedIndexChanged(object sender, EventArgs e)
        {
            getLLInfo();
        }

        protected void Query_Click(object sender, EventArgs e)
        {
            getLLInfo();
        }

        protected void Refresh_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_WarehouseOut_Manage.aspx?tab=1");
        }

        protected string convertState(string state)
        {
            switch (state)
            {
                case "1": return "未审核";
                case "2": return "已审核";
                default: return state;
            }
        }

        protected void Related_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < RepeaterLL.Items.Count; i++)
            {
                if (((CheckBox)RepeaterLL.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    string ptc = ((Label)RepeaterLL.Items[i].FindControl("LabelPTC")).Text;
                    Response.Redirect("SM_Warehouse_RelatedDocument.aspx?PTC=" + ptc);
                    return;
                }
            }

            string script = @"alert('请选择一条要查询的记录！');";

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);
        }

        /***********************************Tab2***********************************/

        protected void getWWInfo()
        {
            string state = DropDownListState2.SelectedValue.ToString();
            string colour = DropDownListColour2.SelectedValue.ToString();
            string sql = "";
            string condition = "";
            //单号条件
            if (TextBoxCode2.Text != "")
            {
                condition = " Code LIKE '%" + TextBoxCode2.Text + "%'";
            }
            //时间条件
            if ((TextBoxDate2.Text != "") && (condition != ""))
            {
                condition += " AND " + " Date LIKE '%" + TextBoxDate2.Text + "%'";
            }
            else if ((TextBoxDate2.Text != "") && (condition == ""))
            {
                condition += " Date LIKE '%" + TextBoxDate2.Text + "%'";
            }
            //加工单位条件
            if ((TextBoxCompany2.Text != "") && (condition != ""))
            {
                condition += " AND " + " Company LIKE '%" + TextBoxCompany2.Text + "%'";
            }
            else if ((TextBoxCompany2.Text != "") && (condition == ""))
            {
                condition += " Company LIKE '%" + TextBoxCompany2.Text + "%'";
            }
            //发料仓库条件
            if ((TextBoxWarehouse2.Text != "") && (condition != ""))
            {
                condition += " AND " + " Warehouse LIKE'%" + TextBoxWarehouse2.Text + "%'";
            }
            else if ((TextBoxWarehouse2.Text != "") && (condition == ""))
            {
                condition += " Warehouse LIKE '%" + TextBoxWarehouse2.Text + "%'";
            }
            //物料代码条件
            if ((TextBoxMaterialCode2.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialCode LIKE '%" + TextBoxMaterialCode2.Text + "%'";
            }
            else if ((TextBoxMaterialCode2.Text != "") && (condition == ""))
            {
                condition += " MaterialCode LIKE '%" + TextBoxMaterialCode2.Text + "%'";
            }
            //物料名称条件
            if ((TextBoxMaterialName2.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialName LIKE '%" + TextBoxMaterialName2.Text + "%'";
            }
            else if ((TextBoxMaterialName2.Text != "") && (condition == ""))
            {
                condition += " MaterialName LIKE '%" + TextBoxMaterialName2.Text + "%'";
            }
            //规格型号条件
            if ((TextBoxStandard2.Text != "") && (condition != ""))
            {
                condition += " AND " + " Standard LIKE '%" + TextBoxStandard2.Text + "%'";
            }
            else if ((TextBoxStandard2.Text != "") && (condition == ""))
            {
                condition += " Standard LIKE '%" + TextBoxStandard2.Text + "%'";
            }
            //计划跟踪号条件
            if ((TextBoxPTC2.Text != "") && (condition != ""))
            {
                condition += " AND " + " PTC LIKE '%" + TextBoxPTC2.Text + "%'";
            }
            else if ((TextBoxPTC2.Text != "") && (condition == ""))
            {
                condition += " PTC LIKE '%" + TextBoxPTC2.Text + "%'";
            }


            //制单人

            if ((TextBoxZDR.Text != "") && (condition != ""))
            {
                condition += " AND " + " Doc LIKE '%" + TextBoxZDR2.Text + "%'";
            }
            else if ((TextBoxZDR.Text != "") && (condition == ""))
            {
                condition += " Doc LIKE '%" + TextBoxZDR2.Text + "%'";
            }
            //材质

            if ((TextBoxCZ.Text != "") && (condition != ""))
            {
                condition += " AND " + " Attribute LIKE '%" + TextBoxCZ2.Text + "%'";
            }
            else if ((TextBoxCZ.Text != "") && (condition == ""))
            {
                condition += " Attribute LIKE '%" + TextBoxCZ2.Text + "%'";
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
                    if (condition != "") { condition += " AND TotalState='1' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    else { condition += " TotalState='1' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    break;
                case "2":
                    if (condition != "") { condition += " AND TotalState='2' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    else { condition += " TotalState='2' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    break;
                default: break;
            }

            if (condition != "") { condition += " AND BillType='2' "; }
            else { condition += " BillType='2' "; }

            if (condition == "")
            {
                sql = "SELECT Date AS Date,TotalState AS State,Company AS Company,Code AS Code,ProcessType AS ProcessType," +
                    "WarehouseCode AS WarehouseCode,Warehouse AS Warehouse," +
                    "SQCODE AS SQCODE,UniqueCode AS UniqueID,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Standard AS MaterialStandard," +
                    "Unit AS Unit,cast(UnitPrice as float) AS UnitPrice,cast(DueNumber as float) AS DN,cast(RealNumber as float) AS RN," +
                    "cast(Amount as float) AS Amount," +
                    "OrderCode AS OrderCode,PTC AS PTC,DetailNote AS Note FROM View_SM_OUTWW order by Code,MaterialCode";
            }
            else
            {
                sql = "SELECT Date AS Date,TotalState AS State,Company AS Company,Code AS Code,ProcessType AS ProcessType," +
                    "WarehouseCode AS WarehouseCode,Warehouse AS Warehouse," +
                    "SQCODE AS SQCODE,UniqueCode AS UniqueID,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Standard AS MaterialStandard," +
                    "Unit AS Unit,cast(UnitPrice as float) AS UnitPrice,cast(DueNumber as float) AS DN, cast(RealNumber as float) AS RN," +
                    "cast(Amount as float) AS Amount," +
                    "OrderCode AS OrderCode,PTC AS PTC,DetailNote AS Note FROM View_SM_OUTWW WHERE " + condition + "order by Code,MaterialCode";
            }
            DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterWW.DataSource = tb;
            RepeaterWW.DataBind();
            if (tb.Rows.Count == 0)
            {
                NoDataPanel2.Visible = true;
            }
            else
            {
                NoDataPanel2.Visible = false;
            }
        }

        protected void RepeaterWW_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("LabelRN")).Text.ToString() != "")
                {
                    trn += Convert.ToSingle(((Label)e.Item.FindControl("LabelRN")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("LabelAmount")).Text.ToString() != "")
                {
                    ta += Convert.ToSingle(((Label)e.Item.FindControl("LabelAmount")).Text.ToString());
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("LabelTotalRN")).Text = trn.ToString();
                ((Label)e.Item.FindControl("LabelTotalAmount")).Text = ta.ToString();
            }
        }

        protected void DropDownListState2_SelectedIndexChanged(object sender, EventArgs e)
        {
            getWWInfo();
        }

        protected void DropDownListColour2_SelectedIndexChanged(object sender, EventArgs e)
        {
            getWWInfo();
        }

        protected void Query2_Click(object sender, EventArgs e)
        {
            getWWInfo();
        }

        protected void Refresh2_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_WarehouseOut_Manage.aspx?tab=2");
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

        /***********************************Tab3***********************************/

        protected void getXSInfo()
        {
            string state = DropDownListState3.SelectedValue.ToString();
            string colour = DropDownListColour3.SelectedValue.ToString();
            string sql = "";
            string condition = "";
            //单号条件
            if (TextBoxCode3.Text != "")
            {
                condition = " Code LIKE '%" + TextBoxCode3.Text + "%'";
            }
            //时间条件
            if ((TextBoxDate3.Text != "") && (condition != ""))
            {
                condition += " AND " + " Date LIKE '%" + TextBoxDate3.Text + "%'";
            }
            else if ((TextBoxDate3.Text != "") && (condition == ""))
            {
                condition += " Date LIKE '%" + TextBoxDate3.Text + "%'";
            }
            //加工单位条件
            if ((TextBoxCompany3.Text != "") && (condition != ""))
            {
                condition += " AND " + " Company LIKE '%" + TextBoxCompany3.Text + "%'";
            }
            else if ((TextBoxCompany3.Text != "") && (condition == ""))
            {
                condition += " Company LIKE '%" + TextBoxCompany3.Text + "%'";
            }
            //发料仓库条件
            if ((TextBoxWarehouse3.Text != "") && (condition != ""))
            {
                condition += " AND " + " Warehouse LIKE'%" + TextBoxWarehouse3.Text + "%'";
            }
            else if ((TextBoxWarehouse3.Text != "") && (condition == ""))
            {
                condition += " Warehouse LIKE '%" + TextBoxWarehouse3.Text + "%'";
            }
            //物料代码条件
            if ((TextBoxMaterialCode3.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialCode LIKE '%" + TextBoxMaterialCode3.Text + "%'";
            }
            else if ((TextBoxMaterialCode3.Text != "") && (condition == ""))
            {
                condition += " MaterialCode LIKE '%" + TextBoxMaterialCode3.Text + "%'";
            }
            //物料名称条件
            if ((TextBoxMaterialName3.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialName LIKE '%" + TextBoxMaterialName3.Text + "%'";
            }
            else if ((TextBoxMaterialName3.Text != "") && (condition == ""))
            {
                condition += " MaterialName LIKE '%" + TextBoxMaterialName3.Text + "%'";
            }
            //规格型号条件
            if ((TextBoxStandard3.Text != "") && (condition != ""))
            {
                condition += " AND " + " Standard LIKE '%" + TextBoxStandard3.Text + "%'";
            }
            else if ((TextBoxStandard3.Text != "") && (condition == ""))
            {
                condition += " Standard LIKE '%" + TextBoxStandard3.Text + "%'";
            }
            //计划跟踪号条件
            if ((TextBoxPTC3.Text != "") && (condition != ""))
            {
                condition += " AND " + " PTC LIKE '%" + TextBoxPTC3.Text + "%'";
            }
            else if ((TextBoxPTC3.Text != "") && (condition == ""))
            {
                condition += " PTC LIKE '%" + TextBoxPTC3.Text + "%'";
            }

            //制单人

            if ((TextBoxZDR3.Text != "") && (condition != ""))
            {
                condition += " AND " + " Doc LIKE '%" + TextBoxZDR3.Text + "%'";
            }
            else if ((TextBoxZDR3.Text != "") && (condition == ""))
            {
                condition += " Doc LIKE '%" + TextBoxZDR3.Text + "%'";
            }

            //材质

            if ((TextBoxCZ3.Text != "") && (condition != ""))
            {
                condition += " AND " + " Attribute LIKE '%" + TextBoxCZ3.Text + "%'";
            }
            else if ((TextBoxCZ3.Text != "") && (condition == ""))
            {
                condition += " Attribute LIKE '%" + TextBoxCZ3.Text + "%'";
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
                    if (condition != "") { condition += " AND TotalState='1' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    else { condition += " TotalState='1' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    break;
                case "2":
                    if (condition != "") { condition += " AND TotalState='2' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    else { condition += " TotalState='2' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    break;
                default: break;
            }

            if (condition != "") { condition += " AND BillType='3' "; }
            else { condition += " BillType='3' "; }

            if (condition == "")
            {
                sql = "SELECT Date AS Date,TotalState AS State,Code AS Code,Company AS Company," +
                    "WarehouseCode AS WarehouseCode,Warehouse AS Warehouse," +
                    "SQCODE AS SQCODE,UniqueCode AS UniqueID,PTC AS PTC,MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute AS Attribute," +
                    "Standard AS MaterialStandard," +
                    "Unit AS Unit,LotNumber AS LotNumber,cast(UnitPrice as float) AS UnitPrice,DueNumber AS DN,cast(RealNumber as float) AS RN," +
                    "cast(Amount as float) AS Amount," +
                    "cast(UnitCost as float) AS UnitCost,cast(Cost as float) AS Cost," +
                    "Dep AS Dep,Clerk AS Clerk,DetailNote AS Note FROM View_SM_OUTXS order by Code,MaterialCode";
            }
            else
            {
                sql = "SELECT Date AS Date,TotalState AS State,Code AS Code,Company AS Company," +
                    "WarehouseCode AS WarehouseCode,Warehouse AS Warehouse," +
                    "SQCODE AS SQCODE,UniqueCode AS UniqueID,PTC AS PTC,MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute AS Attribute," +
                    "Standard AS MaterialStandard," +
                    "Unit AS Unit,LotNumber AS LotNumber,cast(UnitPrice as float) AS UnitPrice,DueNumber AS DN,cast(RealNumber as float) AS RN," +
                    "cast(Amount as float) AS Amount," +
                    "cast(UnitCost as float) AS UnitCost,cast(Cost as float) AS Cost," +
                    "Dep AS Dep,Clerk AS Clerk,DetailNote AS Note FROM View_SM_OUTXS WHERE " + condition + "order by Code,MaterialCode";
            }
            DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
                RepeaterXS.DataSource = tb;
                RepeaterXS.DataBind();

            if (tb.Rows.Count == 0)
            {
                NoDataPanel3.Visible = true;
            }
            else
            {
                NoDataPanel3.Visible = false;
            }
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
            }
        }

        protected void DropDownListState3_SelectedIndexChanged(object sender, EventArgs e)
        {
            getXSInfo();
        }

        protected void DropDownListColour3_SelectedIndexChanged(object sender, EventArgs e)
        {
            getXSInfo();
        }

        protected void Query3_Click(object sender, EventArgs e)
        {
            getXSInfo();
        }

        protected void Refresh3_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_WarehouseOut_Manage.aspx?tab=3");
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


        /***********************************Tab4  其他出库***********************************/


        protected void getQTInfo()
        {
            string state = DropDownListState4.SelectedValue.ToString();
            
            string sql = "";
            string condition = "";
            //单号条件
            if (TextBoxCode4.Text != "")
            {
                condition = " OutCode LIKE '%" + TextBoxCode4.Text + "%'";
            }
            //时间条件
            if ((TextBoxDate4.Text != "") && (condition != ""))
            {
                condition += " AND " + " Date LIKE '%" + TextBoxDate4.Text + "%'";
            }
            else if ((TextBoxDate4.Text != "") && (condition == ""))
            {
                condition += " Date LIKE '%" + TextBoxDate4.Text + "%'";
            }
            //领料部门条件
            if ((TextBoxDep4.Text != "") && (condition != ""))
            {
                condition += " AND " + " Dep LIKE '%" + TextBoxDep4.Text + "%'";
            }
            else if ((TextBoxDep4.Text != "") && (condition == ""))
            {
                condition += " Dep LIKE '%" + TextBoxDep4.Text + "%'";
            }
            //发料仓库条件
            if ((TextBoxWarehouse4.Text != "") && (condition != ""))
            {
                condition += " AND " + " Warehouse LIKE'%" + TextBoxWarehouse4.Text + "%'";
            }
            else if ((TextBoxWarehouse4.Text != "") && (condition == ""))
            {
                condition += " Warehouse LIKE '%" + TextBoxWarehouse4.Text + "%'";
            }
            //物料代码条件
            if ((TextBoxMCode4.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialCode LIKE '%" + TextBoxMCode4.Text + "%'";
            }
            else if ((TextBoxMCode4.Text != "") && (condition == ""))
            {
                condition += " MaterialCode LIKE '%" + TextBoxMCode4.Text + "%'";
            }
            //物料名称条件
            if ((TextBoxMName4.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialName LIKE '%" + TextBoxMName4.Text + "%'";
            }
            else if ((TextBoxMName4.Text != "") && (condition == ""))
            {
                condition += " MaterialName LIKE '%" + TextBoxMName4.Text + "%'";
            }
            //规格型号条件
            if ((TextBoxMStandard4.Text != "") && (condition != ""))
            {
                condition += " AND " + " Standard LIKE '%" + TextBoxMStandard4.Text + "%'";
            }
            else if ((TextBoxMStandard4.Text != "") && (condition == ""))
            {
                condition += " Standard LIKE '%" + TextBoxMStandard4.Text + "%'";
            }
            //计划跟踪号条件
            if ((TextBoxMPTC4.Text != "") && (condition != ""))
            {
                condition += " AND " + " PTC LIKE '%" + TextBoxMPTC4.Text + "%'";
            }
            else if ((TextBoxMPTC4.Text != "") && (condition == ""))
            {
                condition += " PTC LIKE '%" + TextBoxMPTC4.Text + "%'";
            }

            //制单人

            if ((TextBoxZDR4.Text != "") && (condition != ""))
            {
                condition += " AND " + " Doc LIKE '%" + TextBoxZDR4.Text + "%'";
            }
            else if ((TextBoxZDR4.Text != "") && (condition == ""))
            {
                condition += " Doc LIKE '%" + TextBoxZDR4.Text + "%'";
            }
            //材质

            if ((TextBoxCZ4.Text != "") && (condition != ""))
            {
                condition += " AND " + " Attribute LIKE '%" + TextBoxCZ4.Text + "%'";
            }
            else if ((TextBoxCZ4.Text != "") && (condition == ""))
            {
                condition += " Attribute LIKE '%" + TextBoxCZ4.Text + "%'";
            }

            //状态条件
            switch (state)
            {
                case "": break;
                case "1":
                    if (condition != "") { condition += " AND TotalState='1' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    else { condition += " TotalState='1' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    break;
                case "2":
                    if (condition != "") { condition += " AND TotalState='2' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    else { condition += " TotalState='2' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    break;
                default: break;
            }

            if (condition != "") { condition += " AND BillType='4' "; }
            else { condition += " BillType='4' "; }

            if (condition == "")
            {
                sql = "SELECT OutCode AS Code,DepCode AS DepCode,Dep AS Dep,WarehouseCode AS WarehouseCode,Warehouse AS Warehouse," +
                    "Date AS Date,SenderCode AS SenderCode,Sender AS Sender,DocCode AS DocCode,Doc AS Doc,VerifierCode AS VerifierCode," +
                    "Verifier AS Verifier,ApprovedDate AS ApproveDate,ROB AS Colour,TotalState AS State,TotalNote AS Comment," +
                    "SQCODE AS SQCODE,UniqueCode AS UniqueID,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Length AS Length,Width AS Width," +
                    "LotNumber AS LotNumber,Unit AS Unit,cast(UnitPrice as float) AS UnitPrice,DueNumber AS DN,cast(RealNumber as float) AS RN," +
                    "cast(Amount as float) AS Amount,WarehouseCode AS WarehouseOutCode,Warehouse AS WarehouseOut," +
                    "LocationCode AS PositionOutCode,Location AS PositionOut," +
                    "PlanMode AS PlanMode,PTC AS PTC,DetailNote AS Note FROM View_SM_OUT";
            }
            else
            {
                sql = "SELECT OutCode AS Code,DepCode AS DepCode,Dep AS Dep,WarehouseCode AS WarehouseCode,Warehouse AS Warehouse," +
                    "Date AS Date,SenderCode AS SenderCode,Sender AS Sender,DocCode AS DocCode,Doc AS Doc,VerifierCode AS VerifierCode," +
                    "Verifier AS Verifier,ApprovedDate AS ApproveDate,ROB AS Colour,TotalState AS State,TotalNote AS Comment," +
                    "SQCODE AS SQCODE,UniqueCode AS UniqueID,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Length AS Length,Width AS Width," +
                    "LotNumber AS LotNumber,Unit AS Unit,cast(UnitPrice as float) AS UnitPrice,DueNumber AS DN,cast(RealNumber as float) AS RN," +
                    "cast(Amount as float) AS Amount,WarehouseCode AS WarehouseOutCode,Warehouse AS WarehouseOut," +
                    "LocationCode AS PositionOutCode,Location AS PositionOut," +
                    "PlanMode AS PlanMode,PTC AS PTC,DetailNote AS Note FROM View_SM_OUT WHERE " + condition;
            }
            DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
            RepeaterQT.DataSource = tb;
            RepeaterQT.DataBind();
            if (tb.Rows.Count == 0)
            {
                NoDataPanel4.Visible = true;
            }
            else
            {
                NoDataPanel4.Visible = false;
            }
        }

        protected void RepeaterQT_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("LabelRN")).Text.ToString() != "")
                {
                    trn += Convert.ToSingle(((Label)e.Item.FindControl("LabelRN")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("LabelAmount")).Text.ToString() != "")
                {
                    ta += Convert.ToSingle(((Label)e.Item.FindControl("LabelAmount")).Text.ToString());
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("LabelTotalRN")).Text = trn.ToString();
                ((Label)e.Item.FindControl("LabelTotalAmount")).Text = ta.ToString();
            }
        }

        protected void DropDownListState4_SelectedIndexChanged(object sender, EventArgs e)
        {
            getQTInfo();
        }

        protected void Query4_Click(object sender, EventArgs e)
        {
            getQTInfo();
        }

        protected void Refresh4_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_WarehouseOut_Manage.aspx?tab=4");
        }

        protected string convertState4(string state)
        {
            switch (state)
            {
                case "1": return "未审核";
                case "2": return "已审核";
                default: return state;
            }
        }

    }
}
