using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;



namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_InventoryReport : System.Web.UI.Page
    {

        private float tnuminaccount = 0;
        private float tfzninaccount = 0;
        private float tamountinaccount = 0;
        private float tnotinnum = 0;
        private float tnotinfzn = 0;
        private float tnotinamount = 0;
        private float tnotoutnum = 0;
        private float tnotoutfzn = 0;
        private float tnotoutamount = 0;
        private float tduenum = 0;
        private float tduefzn = 0;
        private float tdueamount = 0;
        private float trealnum = 0;
        private float trealfzn = 0;
        private float trealamount = 0;
        private float tdiffnum = 0;
        private float tdifffzn = 0;
        private float tdiffamount = 0;

       
        PagerQueryParam pager = new PagerQueryParam();

   

        protected void Page_Load(object sender, EventArgs e)
        {

            InitVar();            

            LabelfanCode.Text= Request.QueryString["ID"];

            if (!IsPostBack)
            {
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;

                LabelMessage.Text = "点击下一页之前，请先点击保存！";

                //得到审核人
                GetStaff();

                //得到兼盘人
                GetMiddleStaff();

                BindInfo();
                bindGV();

                //得到盘点报告
                bindGrid();
               
            }
        }
      
        #region 盘点记录

        private void InitVar()
        {
            Report();
            UCPaging1.PageMSChanged += new UCPagingOfMS.PageHandlerOfMS(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;

            UCPaging2.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging2.PageSize = pager.PageSize;

        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {

            bindSunNum();
            if (Request.QueryString["FLAG"] == "READ")
            {
                pager.PageIndex = UCPaging2.CurrentPage;
                System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
                CommonFun.Paging(dt, GridView1, UCPaging2, NoDataPanel);
                if (NoDataPanel.Visible)
                {
                    UCPaging2.Visible = false;
                }
                else
                {
                    UCPaging2.Visible = true;
                    UCPaging2.InitPageInfo();
                }
            }
            else
            {
                pager.PageIndex = UCPaging1.CurrentPage;
                System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
                CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
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
        }

        #endregion



        private void bindSunNum()
        {
            string sql = "select cast(sum(NumberInAccount) as float) as NumberInAccount,cast(sum(SupportNumberInAccount) as float) as SupportNumberInAccount,cast(sum(AmountInAccount) as float) as AmountInAccount, cast(sum(NumberNotIn) as float) as NumberNotIn,cast(sum(SupportNumberNotIn) as float) as SupportNumberNotIn,cast(sum(AmountNotIn) as float) as AmountNotIn,cast(sum(NumberNotOut) as float) as NumberNotOut,cast(sum(SupportNumberNotOut) as float) as SupportNumberNotOut,cast(sum(AmountNotOut) as float) as AmountNotOut,cast(sum(DueNumber) as float) as DueNumber,cast(sum(DueSupportNumber) as float) as DueSupportNumber,cast(sum(DueAmount) as float) as DueAmount,cast(sum(RealNumber) as float) as RealNumber,cast(sum(RealSupportNumber) as float) as RealSupportNumber,cast(sum(RealAmount) as float) as RealAmount,cast(sum(DiffNumber) as float) as DiffNumber,cast(sum(DiffSupportNumber) as float) as DiffSupportNumber,cast(sum(DiffAmount) as float) as DiffAmount from View_SM_InventoryReport where " + pager.StrWhere;
             SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
             if (dr.Read())
             {
                 sumnuminaccount.Value = dr["NumberInAccount"].ToString();
                 sumfzninaccount.Value = dr["SupportNumberInAccount"].ToString();
                 sumamountinaccount.Value = dr["AmountInAccount"].ToString();
                 sumnotinnum.Value = dr["NumberNotIn"].ToString();
                 sumnotinfznum.Value = dr["SupportNumberNotIn"].ToString();
                 sumnotinamount.Value = dr["AmountNotIn"].ToString();
                 sumnotoutnum.Value = dr["NumberNotOut"].ToString();
                 sumnotoutfznum.Value = dr["SupportNumberNotOut"].ToString();
                 sumnotoutamount.Value = dr["AmountNotOut"].ToString();
                 sumduenum.Value = dr["DueNumber"].ToString();
                 sumduefznum.Value = dr["DueSupportNumber"].ToString();

                 sumdueamount.Value = dr["DueAmount"].ToString();

                 sumrealnum.Value = dr["RealNumber"].ToString();
                 sumrealfznum.Value = dr["RealSupportNumber"].ToString();
                 sumrealamount.Value = dr["RealAmount"].ToString();

                 sumdiffnum.Value= dr["DiffNumber"].ToString();
                 sumdifffznum.Value = dr["DiffSupportNumber"].ToString();
                 sumdiffamount.Value = dr["DiffAmount"].ToString();

             }
             dr.Close();

        }

        /**************************Tab1*********************************/

        //审核人
        protected void GetStaff()
        {
            string sql = "SELECT DISTINCT ST_ID,ST_NAME FROM TBDS_STAFFINFO WHERE ST_DEPID='05'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            //审核人
            DropDownListManager.DataTextField = "ST_NAME";
            DropDownListManager.DataValueField = "ST_ID";
            DropDownListManager.DataSource = dt;
            DropDownListManager.DataBind();

        }

        //兼盘人
        private void GetMiddleStaff()
        {
            string sql = "SELECT DISTINCT ST_ID,ST_NAME FROM TBDS_STAFFINFO WHERE ST_DEPID='05'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            //兼盘人,
            DropDownListKeeping.DataTextField = "ST_NAME";
            DropDownListKeeping.DataValueField = "ST_ID";
            DropDownListKeeping.DataSource = dt;
            DropDownListKeeping.DataBind();
            DropDownListKeeping.Items.Insert(0, new ListItem("", ""));
            DropDownListKeeping.SelectedIndex = 0;
        }

        private void BindInfo()
        {
            string flag = Request.QueryString["FLAG"];
            string code = Request.QueryString["ID"];

            //总表
            string sql = "SELECT PDCode AS Code,SchemaDate AS Date,ExportDate as ExportDate,PlanerCode AS PlanerCode,Planer AS Planer," +
                "WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,PJCode AS ProjectCode,PJ AS Project," +
                "MTCode AS MaterialTypeCode,MT AS MaterialType,ClerkCode AS ClerkCode,Clerk AS Clerk," +
                "ManagerCode AS ManagerCode,Manager AS Manager,KeeperCode AS KeepingCode,Keeper AS Keeping," +
                "PDDate AS DoneDate,State AS State,VerifierCode AS VerifierCode,Verifier AS Verifier," +
                "VerifierCode AS ApproveDate,Advice AS Advice,Note AS Comment,ZDRID AS ZDRID,ZDRNM AS ZDRNM,ISNULL(PD_OREDERFIELD,'') AS OREDERFIELD,ISNULL(PD_OREDERTYPE,'') AS OREDERTYPE,ISNULL(PD_OREDERFIELD1,'') AS OREDERFIELD1,ISNULL(PD_OREDERTYPE1,'') AS OREDERTYPE1,ISNULL(PD_OREDERFIELD2,'') AS OREDERFIELD2,ISNULL(PD_OREDERTYPE2,'') AS OREDERTYPE2  FROM View_SM_InventorySchema WHERE PDCode='" + code + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                LabelCode.Text = dr["Code"].ToString();

                hlksummary1.NavigateUrl = "~/SM_Data/SM_Warehouse_InventoryReport_Summary1.aspx?code=" + Server.UrlEncode(LabelCode.Text.Trim());
                hlksummary2.NavigateUrl = "~/SM_Data/SM_Warehouse_InventoryReport_Summary2.aspx?code=" + Server.UrlEncode(LabelCode.Text.Trim());

                LabelState.Text = dr["State"].ToString();

                //系统封账时间为生成盘点明细时修改的时间

                LabelTime.Text = dr["ExportDate"].ToString();//系统封账时间

                TextBoxDate.Text = dr["Date"].ToString();//盘点时间

                LabelWarehouse.Text = dr["Warehouse"].ToString();

                LabelEng.Text = dr["Project"].ToString();

                LabelMar.Text = dr["MaterialType"].ToString();


                LabelDoc.Text = dr["ZDRNM"].ToString();//制单人

                LabelDocCode.Text = dr["ZDRID"].ToString();

                LabelClert.Text = dr["Clerk"].ToString();//盘点人

                LabelClertCode.Text = dr["ClerkCode"].ToString();

                //审核人
                //try { DropDownListManager.Items.FindByValue(dr["ManagerCode"].ToString()).Selected = true; }
                //catch { }
                foreach (ListItem lt in DropDownListManager.Items)
                {
                    lt.Selected = false;
                    if (lt.Value == dr["ManagerCode"].ToString())
                    {
                        lt.Selected = true;
                    }
                }
                //兼盘人
                foreach (ListItem lt in DropDownListKeeping.Items)
                {
                    lt.Selected = false;
                    if (lt.Value == dr["KeepingCode"].ToString())
                    {
                        lt.Selected = true;
                    }
                }


                //第一排序
                try {

                    if (dr["OREDERFIELD"].ToString() != string.Empty)
                    {
                        DropDownListFirst.ClearSelection();
                        DropDownListFirst.Items.FindByValue(dr["OREDERFIELD"].ToString()).Selected = true;
                    }
                }
                catch { }


                //第一类型

                try {
                    if (dr["OREDERTYPE"].ToString() != string.Empty)
                    {
                        RadioButtonListFirstType.ClearSelection();
                        RadioButtonListFirstType.Items.FindByValue(dr["OREDERTYPE"].ToString()).Selected = true;
                    }
                    
                 }
                catch { }

                //第二排序
                try
                {

                    if (dr["OREDERFIELD1"].ToString() != string.Empty)
                    {
                        DropDownListSecond.ClearSelection();
                        DropDownListSecond.Items.FindByValue(dr["OREDERFIELD1"].ToString()).Selected = true;
                    }
                }
                catch { }


                //第二类型

                try
                {
                    if (dr["OREDERTYPE1"].ToString() != string.Empty)
                    {
                        RadioButtonListSecond.ClearSelection();
                        RadioButtonListSecond.Items.FindByValue(dr["OREDERTYPE1"].ToString()).Selected = true;
                    }

                }
                catch { }

                //第三排序
                try
                {

                    if (dr["OREDERFIELD2"].ToString() != string.Empty)
                    {
                        DropDownListThird.ClearSelection();
                        DropDownListThird.Items.FindByValue(dr["OREDERFIELD2"].ToString()).Selected = true;
                    }
                }
                catch { }


                //第三类型

                try
                {
                    if (dr["OREDERTYPE1"].ToString() != string.Empty)
                    {
                        RadioButtonListThird.ClearSelection();
                        RadioButtonListThird.Items.FindByValue(dr["OREDERTYPE2"].ToString()).Selected = true;
                    }

                }
                catch { }



                List<string> ltOrder = GetOrderField();

                pager.OrderField = ltOrder[0];

                pager.OrderType = Convert.ToInt32(ltOrder[1]);

                /*
                 *审核时用的信息 
                 */

                LabelVerifier.Text = dr["Verifier"].ToString();//审核人

                LabelVerifierCode.Text = dr["VerifierCode"].ToString();//审核人

                TextBoxAdvice.Text = dr["Advice"].ToString();

                LabelApproveDate.Text = dr["ApproveDate"].ToString();

            }
            dr.Close();

            //提交
            if (flag == "MODIFY")
            {
                Tab2.Enabled = false;
                Calculation.Enabled = true;
                Verify.Enabled = false;
                Deny.Enabled = false;
                Adjust.Enabled = false;

            }

            //审核
            if (flag == "VERIFY")
            {
                Calculation.Enabled = false;
                Save.Enabled = false;
                Submit.Enabled = false;
                Adjust.Enabled = false;

                //审核人
                LabelVerifier.Text = Session["UserName"].ToString();
                LabelVerifierCode.Text = Session["UserID"].ToString();

                LabelApproveDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }

            //调整
            if (flag == "ADJUST")
            {
                Calculation.Enabled = false;
                Save.Enabled = false;
                Submit.Enabled = false;
                Verify.Enabled = false;
                Deny.Enabled = false;
                btnYinKui.Disabled=false;

                if (LabelVerifierCode.Text == Session["UserID"].ToString())
                {
                    AntiVerify.Enabled = true;
                }

            }
            //查看
            if (flag == "READ")
            {
                //未提交，和未审核（=提交）不能显示盘盈亏单据

                if (LabelState.Text == "2")
                {
                    //审核（待调整），只有审核人才能显示盘盈亏单据
                    ImageVerify.Visible = true;
                    ImageVerify2.Visible = true;
                }

                else if (LabelState.Text == "3")
                {
                    //调整完事之后，大家都能看见
                    ImageAdjust.Visible = true;
                    btnYinKui.Visible = true;
                    btnYinKui.Disabled = false;
                }

                Calculation.Enabled = false;
                Save.Enabled = false;
                Submit.Enabled = false;
                Verify.Enabled = false;
                AntiVerify.Enabled = false;
                Deny.Enabled = false;
                Adjust.Enabled = false;

            }
        }

        //报告
        protected void Report()
        {
          string code = Request.QueryString["ID"];

          string condition = GetCondition();
            //此处应当根据盘点方案选择数据表并从中读取盘点报告数据

            //盘点记录表
         

            List<string> lt = GetOrderField();

            pager.OrderField = lt[0];

            pager.OrderType = Convert.ToInt32(lt[1]);

            pager.TableName = "View_SM_InventoryReport";
            pager.PrimaryKey = "SQCODE";
            pager.ShowFields = "SQCODE,MaterialCode,MaterialName,Attribute, GB,Standard AS MaterialStandard,Length,Width,LotNumber,Unit,cast(UnitPrice as float) as UnitPrice,cast(NumberInAccount as float) AS Number,cast(SupportNumberInAccount as float) AS FZN,cast(AmountInAccount as float) AS Amount,cast(NumberNotIn as float) AS NotInNumber,cast(SupportNumberNotIn as float) AS NotInFZN,cast(AmountNotIn as float) AS NotInAmount,cast(NumberNotOut as float) AS NotOutNumber,cast(SupportNumberNotOut as float) AS NotOutFZN,cast(AmountNotOut as float) AS NotOutAmount,cast(DueNumber as float) AS DueNumber,cast(DueSupportNumber as float) AS DueFZN,cast(DueAmount as float) AS DueAmount,cast(RealNumber as float) AS RN,cast(RealSupportNumber as float) AS RFZN,cast(RealAmount as float) AS RA, cast(DiffNumber as float) AS DiffNum,cast(DiffSupportNumber as float) AS DiffFZN,cast(DiffAmount as float) AS DiffAmount,State AS InventoryState,WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,LocationCode AS PositionCode,Location AS Position,PlanMode AS PlanMode,PTC AS PTC,Note AS Comment";

            pager.StrWhere = "PDCode='" + code + "' " +condition;
           
            pager.PageSize = 50;
          
          
               
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
            
           
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearCondition();
            resetSubcondition();
                        
        }

        private void clearCondition()
        {
            TextBoxPTC.Text = string.Empty;
            TextBoxMaterialCode.Text = string.Empty;
            TextBoxMaterialName.Text = string.Empty;
            TextBoxStandard.Text = string.Empty;
            TextBoxAttribute.Text = string.Empty;
            TextBoxGB.Text = string.Empty;
            TextBoxPlanMode.Text = string.Empty;
            TextBoxWarehouse.Text = string.Empty;
            TextBoxPosition.Text = string.Empty;
            

        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            UCPaging2.CurrentPage = 1;
            bindGrid();
            refreshStyle();
            ModalPopupExtenderSearch.Hide();
           
 
        }
        protected string GetCondition() //获取筛选的条件 
        {
            string condition = "";
            string subcondition=GetSubCondtion();

            if (TextBoxPTC.Text != "")
            {
                condition = "AND PTC LIKE '%" + TextBoxPTC.Text.Trim() + "%' ";
            }

            if (TextBoxMaterialCode.Text != "")
            {
                condition += "AND MaterialCode LIKE '%" + TextBoxMaterialCode.Text.Trim() + "%' ";
            }
            if (TextBoxMaterialName.Text != "")
            {
                condition += "AND MaterialName LIKE '%" + TextBoxMaterialName.Text.Trim() + "%' ";
            }

            if (TextBoxStandard.Text != "")
            {
                condition += "AND Standard LIKE '%" + TextBoxStandard.Text.Trim() + "%' ";
            }
            if (TextBoxAttribute.Text != "")
            {
                condition += "AND Attribute LIKE '%" + TextBoxAttribute.Text.Trim() + "%' ";
            }

            if (TextBoxGB.Text != "")
            {
                condition += "AND GB LIKE '%" + TextBoxGB.Text.Trim() + "%' ";
            }
            if (TextBoxPlanMode.Text != "")
            {
                condition += "AND PlanMode LIKE '%" + TextBoxPlanMode.Text.Trim() + "%' ";
            }

            if (TextBoxWarehouse.Text != "")
            {
                condition += "AND Warehouse LIKE '%" + TextBoxWarehouse.Text.Trim() + "%' ";
            }
            if (TextBoxPosition.Text != "")
            {
                condition += "AND Position LIKE '%" + TextBoxPosition.Text.Trim() + "%' ";
            }

            if (condition != "")
            {
                if (subcondition != "")
                {
                    condition += DropDownListFatherLogic.SelectedValue + " " + "(" + subcondition + ")";
                }
            }
            else
            {
                if (subcondition != "")
                {
                    condition +=DropDownListFatherLogic.SelectedValue + " " + "(" + subcondition + ")";
                }
            }
            return condition;

        }


        #region 条件框


        private void bindGV()
        {
            GridViewSearch.DataSource = CreateTable();

            GridViewSearch.DataBind();
        }

        private System.Data.DataTable CreateTable()
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            dt.Columns.Add(new DataColumn("index", typeof(int)));


            for (int i = 0; i < 6; i++)
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
            ItemList.Add("MaterialCode", "物料代码");
            ItemList.Add("MaterialName", "物料名称");
            ItemList.Add("Standard", "规格型号");
            ItemList.Add("Attribute", "材质");
            ItemList.Add("GB", "国标");
            ItemList.Add("PlanMode", "计划模式");
            ItemList.Add("Warehouse", "仓库");
            ItemList.Add("Position", "仓位");
            ItemList.Add("NumberInAccount", "数量");
            ItemList.Add("NumberNotIn", "尚未入库数");
            ItemList.Add("NumberNotOut", "尚未出库数");
            ItemList.Add("DiffNumber", "差异数量");

            ItemList.Add("Note", "差异原因");

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
                    (gr.FindControl("tb_logic") as System.Web.UI.WebControls.TextBox).Visible = false;
                }
            }
        }

        protected string GetSubCondtion()  //获取gridview中的条件
        {
            string subCondition = "";

            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                if (gr.RowIndex == 0)
                {

                    DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                    if (ddl.SelectedValue != "NO")
                    {
                        System.Web.UI.WebControls.TextBox txtValue = (gr.FindControl("TextBoxValue") as System.Web.UI.WebControls.TextBox);

                        DropDownList ddlRel = (gr.FindControl("DropDownListRelation") as DropDownList);

                        subCondition  = ConvertRelation(ddl.SelectedValue, ddlRel.SelectedValue, txtValue.Text.Trim());
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

                        System.Web.UI.WebControls.TextBox txtValue = (gr.FindControl("TextBoxValue") as System.Web.UI.WebControls.TextBox);

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

            if (field == "MaterialCode")
            {
                fieldValue = fieldValue.PadLeft(6, '0');
            }
           

            switch (relation)
            {
                case "0":
                    {
                        //包含

                        obj = field + "  LIKE  '%" + fieldValue + "%' ";
                        break;
                    }
                case "1":
                    {
                        //等于
                        obj = field + "  =  '" + fieldValue + "' ";
                        break;
                    }
                case "2":
                    {
                        //不等于
                        obj = field + "  !=  '" + fieldValue + "' ";
                        break;
                    }
                case "3":
                    {
                        //大于
                        obj = field + "  >  '" + fieldValue + "' ";
                        break;
                    }
                case "4":
                    {
                        //大于或等于
                        obj = field + "  >=  '" + fieldValue + "' ";
                        break;
                    }
                case "5":
                    {
                        //小于
                        obj = field + "  <  '" + fieldValue + "' ";
                        break;
                    }
                case "6":
                    {
                        //小于或等于
                        obj = field + "  <=  '" + fieldValue + "' ";
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
                (gr.FindControl("TextBoxValue") as System.Web.UI.WebControls.TextBox).Text = string.Empty; ;
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
                        (gr.FindControl("tb_logic") as System.Web.UI.WebControls.TextBox).Style.Add("display", "none");
                    }

                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "block");
                    (gr.FindControl("tb_name") as System.Web.UI.WebControls.TextBox).Style.Add("display", "none");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "block");
                    (gr.FindControl("tb_relation") as System.Web.UI.WebControls.TextBox).Style.Add("display", "none");
                }
                else
                {
                    (gr.FindControl("DropDownListLogic") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_logic") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_name") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_relation") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                }
            }
        }

        #endregion


        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                TableCellCollection tcFooter = e.Row.Cells;
                tcFooter.Add(new TableHeaderCell()); ;
                tcFooter[33].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[33].Text = "</th></tr><tr>";
                tcFooter.Add(new TableHeaderCell());
                tcFooter[34].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[34].Attributes.Add("colspan", "2");
                tcFooter[34].Attributes.Add("left", "expression(this.offsetParent.scrollLeft - 1)");
                tcFooter[34].Text = "";
                tcFooter.Add(new TableHeaderCell()); 
                tcFooter[35].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[35].Attributes.Add("position", "relative");
                tcFooter[35].Attributes.Add("left", "expression(this.offsetParent.scrollLeft - 1)");
                tcFooter[35].Attributes.Add("align", "left");
                tcFooter[35].Text = "总计";
                tcFooter.Add(new TableHeaderCell()); 
                tcFooter[36].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[36].Attributes.Add("colspan", "12");
                tcFooter[36].Text = "";

                tcFooter.Add(new TableHeaderCell());
                tcFooter[37].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[37].Attributes.Add("align", "left");
                tcFooter[37].Text = sumnuminaccount.Value;

                tcFooter.Add(new TableHeaderCell());
                tcFooter[38].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[38].Attributes.Add("align", "left");
                tcFooter[38].Text = sumfzninaccount.Value;

                tcFooter.Add(new TableHeaderCell());
                tcFooter[39].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[39].Attributes.Add("align", "left");
                tcFooter[39].Text = sumamountinaccount.Value;


                tcFooter.Add(new TableHeaderCell());
                tcFooter[40].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[40].Attributes.Add("align", "left");
                tcFooter[40].Text = sumnotinnum.Value;

                tcFooter.Add(new TableHeaderCell());
                tcFooter[41].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[41].Attributes.Add("align", "left");
                tcFooter[41].Text = sumnotinfznum.Value;

                tcFooter.Add(new TableHeaderCell());
                tcFooter[42].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[42].Attributes.Add("align", "left");
                tcFooter[42].Text = sumnotinamount.Value;

                tcFooter.Add(new TableHeaderCell());
                tcFooter[43].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[43].Attributes.Add("align", "left");
                tcFooter[43].Text = sumnotoutnum.Value;

                tcFooter.Add(new TableHeaderCell());
                tcFooter[44].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[44].Attributes.Add("align", "left");
                tcFooter[44].Text = sumnotoutfznum.Value;

                tcFooter.Add(new TableHeaderCell());
                tcFooter[45].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[45].Attributes.Add("align", "left");
                tcFooter[45].Text = sumnotoutamount.Value;

                tcFooter.Add(new TableHeaderCell());
                tcFooter[46].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[46].Attributes.Add("align", "left");
                tcFooter[46].Text = sumduenum.Value;

                tcFooter.Add(new TableHeaderCell());
                tcFooter[47].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[47].Attributes.Add("align", "left");
                tcFooter[47].Text = sumduefznum.Value;

                tcFooter.Add(new TableHeaderCell());
                tcFooter[48].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[48].Attributes.Add("align", "left");
                tcFooter[48].Text = sumdueamount.Value;

                tcFooter.Add(new TableHeaderCell());
                tcFooter[49].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[49].Attributes.Add("align", "left");
                tcFooter[49].Text = sumrealnum.Value;

                tcFooter.Add(new TableHeaderCell());
                tcFooter[50].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[50].Attributes.Add("align", "left");
                tcFooter[50].Text = sumrealfznum.Value;

                tcFooter.Add(new TableHeaderCell());
                tcFooter[51].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[51].Attributes.Add("align", "left");
                tcFooter[51].Text = sumrealamount.Value;

                tcFooter.Add(new TableHeaderCell());
                tcFooter[52].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[52].Attributes.Add("align", "left");
                tcFooter[52].Text = sumdiffnum.Value;

                tcFooter.Add(new TableHeaderCell());
                tcFooter[53].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[53].Attributes.Add("align", "left");
                tcFooter[53].Text = sumdifffznum.Value;

                tcFooter.Add(new TableHeaderCell());
                tcFooter[54].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[54].Attributes.Add("align", "left");
                tcFooter[54].Text = sumdiffamount.Value;

                tcFooter.Add(new TableHeaderCell());
                tcFooter[55].Attributes.Add("bgcolor", "#A8B7EC");
                tcFooter[55].Text = "</th></tr><tr>";
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    tnuminaccount += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "Number"));
            //    tfzninaccount += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "FZN"));
            //    tamountinaccount += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "Amount"));
            //    tnotinnum += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "NotInNumber"));
            //    tnotinfzn += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "NotInFZN"));
            //    tnotinamount += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "NotInAmount"));
            //    tnotoutnum += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "NotOutNumber"));
            //    tnotoutfzn += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "NotOutFZN"));
            //    tnotoutamount += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "NotOutAmount"));
            //    tduenum += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "DueNumber"));
            //    tduefzn += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "DueFZN"));
            //    tdueamount += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "DueAmount"));
            //    trealnum += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "RN"));
            //    trealfzn += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "RFZN"));
            //    trealamount += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "RA"));
            //    tdiffnum += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "DiffNum"));
            //    tdifffzn += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "DiffFZN"));
            //    tdiffamount += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "DiffAmount"));

            //}
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((System.Web.UI.WebControls.Label)e.Row.Cells[10].FindControl("LabelTotalNumber")).Text = sumnuminaccount.Value;
                ((System.Web.UI.WebControls.Label)e.Row.Cells[11].FindControl("LabelTotalFZN")).Text = sumfzninaccount.Value;
                ((System.Web.UI.WebControls.Label)e.Row.Cells[12].FindControl("LabelTotalAmount")).Text = sumamountinaccount.Value;
                ((System.Web.UI.WebControls.Label)e.Row.Cells[13].FindControl("LabelTotalNotInNumber")).Text = sumnotinnum.Value;
                ((System.Web.UI.WebControls.Label)e.Row.Cells[14].FindControl("LabelTotalNotInFZN")).Text = sumnotinfznum.Value;
                ((System.Web.UI.WebControls.Label)e.Row.Cells[15].FindControl("LabelTotalNotInAmount")).Text = sumnotinamount.Value;
                ((System.Web.UI.WebControls.Label)e.Row.Cells[16].FindControl("LabelTotalNotOutNumber")).Text = sumnotoutnum.Value;
                ((System.Web.UI.WebControls.Label)e.Row.Cells[16].FindControl("LabelTotalNotOutFZN")).Text = sumnotoutfznum.Value;
                ((System.Web.UI.WebControls.Label)e.Row.Cells[17].FindControl("LabelTotalNotOutAmount")).Text = sumnotoutamount.Value;
                ((System.Web.UI.WebControls.Label)e.Row.Cells[18].FindControl("LabelTotalDueNumber")).Text = sumduenum.Value;
                ((System.Web.UI.WebControls.Label)e.Row.Cells[19].FindControl("LabelTotalDueFZN")).Text = sumduefznum.Value;
                ((System.Web.UI.WebControls.Label)e.Row.Cells[20].FindControl("LabelTotalDueAmount")).Text = sumdueamount.Value;
                ((System.Web.UI.WebControls.Label)e.Row.Cells[21].FindControl("LabelTotalRN")).Text = sumrealnum.Value;
                ((System.Web.UI.WebControls.Label)e.Row.Cells[22].FindControl("LabelTotalRFZN")).Text = sumrealfznum.Value;
                ((System.Web.UI.WebControls.Label)e.Row.Cells[23].FindControl("LabelTotalRA")).Text = sumrealamount.Value;
                ((System.Web.UI.WebControls.Label)e.Row.Cells[24].FindControl("LabelTotalDiffNum")).Text = sumdiffnum.Value;
                ((System.Web.UI.WebControls.Label)e.Row.Cells[25].FindControl("LabelTotalDiffFZN")).Text = sumdifffznum.Value;
                ((System.Web.UI.WebControls.Label)e.Row.Cells[26].FindControl("LabelTotalDiffAmount")).Text = sumdiffamount.Value;
            }
        }


        //计算
        protected void Calculation_Click(object sender, EventArgs e)
        {

            string EndDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//应该为系统点击计算的时间

            string sqlstring = DBCallCommon.GetStringValue("connectionStrings");
            SqlConnection con = new SqlConnection(sqlstring);
            con.Open();
            SqlCommand cmd = new SqlCommand("Calculation", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Code", SqlDbType.VarChar, 50);				//增加参数截止日期@EndDate
            cmd.Parameters["@Code"].Value = LabelCode.Text;			        //系统封账时间
            cmd.Parameters.Add("@StartDate", SqlDbType.VarChar, 50);		//增加参数截止日期@EndDate
            cmd.Parameters["@StartDate"].Value = LabelTime.Text.Trim();		//点击系统核算时的时间

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            con.Dispose();

            InitVar();

            bindGrid();
        }



        //需要分页保存


        protected void Save_Click(object sender, EventArgs e)
        {
            //此处是保存操作
            List<string> sqllist = new List<string>();
            string sql = "";
            string Code = LabelCode.Text;
            string Date = TextBoxDate.Text;
            string ManagerCode = "";//审核人

            string AgentCode = LabelClertCode.Text;//盘点人

            string KeeperCode = "";//兼盘人
            string Doc = LabelDoc.Text;
            string DocCode = LabelDocCode.Text;

            //修改PD_CLERK--盘点人
            //PD_MANAGER--审核人
            //PD_KEEPER--兼盘人---只有兼盘人是制定
            //PD_PDDATE--盘点时间

            sql = "UPDATE TBWS_INVENTORYSCHEMA SET PD_CLERK='" + AgentCode + 
                "',PD_MANAGER='" + ManagerCode + "',PD_KEEPER='" + KeeperCode + 
                "',PD_PDDATE='" + Date + "'  WHERE PD_CODE='" + Code + "'";
            sqllist.Add(sql);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string SQCODE = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;
                
                float NumNotIn = 0;
                try { NumNotIn = Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxNotInNumber")).Text); }
                catch { NumNotIn = 0; }

                float FZNNotIn = 0;
                try { FZNNotIn = Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxNotInFZN")).Text); }
                catch { FZNNotIn = 0; }

                float AmountNotIn = 0;
                try { AmountNotIn = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputNotInAmount")).Value); }
                catch { AmountNotIn = 0; }

                float NumNotOut = 0;
                try { NumNotOut = Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxNotOutNumber")).Text); }
                catch { NumNotOut = 0; }

                float FZNNotOut = 0;
                try { FZNNotOut = Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxNotOutFZN")).Text); }
                catch { FZNNotOut = 0; }

                float AmountNotOut = 0;
                try { AmountNotOut = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputNotOutAmount")).Value); }
                catch { AmountNotOut = 0; }

                float DueNum = 0;
                try { DueNum = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputDueNumber")).Value); }
                catch { DueNum = 0; }

                float DueFZN = 0;
                try { DueFZN = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputDueFZN")).Value); }
                catch { DueFZN = 0; }

                float DueAmount = 0;
                try { DueAmount = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputDueAmount")).Value); }
                catch { DueAmount = 0; }

                float RN = 0;
                try { RN = Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxRN")).Text); }
                catch { RN = 0; }

                float RFZN = 0;
                try { RFZN = Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxRFZN")).Text); }
                catch { RFZN = 0; }   

                float RA = 0;
                try { RA = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputRA")).Value); }
                catch { RA = 0; }

                float DiffNum = 0;
                try { DiffNum = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputDiffNum")).Value); }
                catch { DiffNum = 0; }

                float DiffFZN = 0;
                try { DiffFZN = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputDiffFZN")).Value); }
                catch { DiffFZN = 0; }

                float DiffAmount = 0;
                try { DiffAmount = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputDiffAmount")).Value); }
                catch { DiffAmount = 0; }

                string Comment = ((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;
                sql =
                    "UPDATE TBWS_INVENTORYRECORD SET " +
                    "PD_NUMNOTIN='" + NumNotIn + "',PD_FZNUMNOTIN='" + FZNNotIn + "',PD_AMOUNTNOTIN='" + AmountNotIn +
                    "',PD_NUMNOTOUT='" + NumNotOut + "',PD_FZNUMNOTOUT='" + FZNNotOut + "',PD_AMOUNTNOTOUT='" + AmountNotOut +
                    "',PD_DUENUM='" + DueNum + "',PD_DUEFZNUM='" + DueFZN + "',PD_DUEAMOUNT='" + DueAmount +
                    "',PD_REALNUM='" + RN + "',PD_REALFZNUM='" + RFZN + "',PD_REALAMOUNT='" + RA +
                    "',PD_DIFFNUM='" + DiffNum + "',PD_DIFFFZNUM='" + DiffFZN + "',PD_DIFFAMOUNT='" + DiffAmount + 
                    "',PD_NOTE='" + Comment + "' WHERE PD_CODE='" + Code + "' AND PD_SQCODE='" + SQCODE + "'";
                sqllist.Add(sql);
            }
            DBCallCommon.ExecuteTrans(sqllist);
            sqllist.Clear();
            LabelMessage.Text = "保存成功";
        }



        //需要分页提交

        protected void Submit_Click(object sender, EventArgs e)
        {

            //此处是提交操作
            List<string> sqllist = new List<string>();
            string sql = "";
            string Code = LabelCode.Text;
            string Date = TextBoxDate.Text;

            string ManagerCode = DropDownListManager.SelectedValue;//审核人
            string AgentCode = LabelClertCode.Text;//盘点人

            string KeeperCode = DropDownListKeeping.SelectedValue;//兼盘人==财务

            string Doc = LabelDoc.Text;//制单人
            string DocCode = LabelDocCode.Text;//制单人

            //PD_DONESTATE='1'表示提交盘点方案

            sql = "UPDATE TBWS_INVENTORYSCHEMA SET PD_CLERK='" + AgentCode +
                "',PD_MANAGER='" + ManagerCode + "',PD_KEEPER='" + KeeperCode +
                "',PD_PDDATE='" + Date + "',PD_DONESTATE='1',PD_NOTE=null  WHERE PD_CODE='" + Code + "'";
            sqllist.Add(sql);


            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string SQCODE = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;

                float NumNotIn = 0;
                try { NumNotIn = Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxNotInNumber")).Text); }
                catch { NumNotIn = 0; }

                float FZNNotIn = 0;
                try { FZNNotIn = Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxNotInFZN")).Text); }
                catch { FZNNotIn = 0; }

                float AmountNotIn = 0;
                try { AmountNotIn = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputNotInAmount")).Value); }
                catch { AmountNotIn = 0; }

                float NumNotOut = 0;
                try { NumNotOut = Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxNotOutNumber")).Text); }
                catch { NumNotOut = 0; }

                float FZNNotOut = 0;
                try { FZNNotOut = Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxNotOutFZN")).Text); }
                catch { FZNNotOut = 0; }

                float AmountNotOut = 0;
                try { AmountNotOut = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputNotOutAmount")).Value); }
                catch { AmountNotOut = 0; }

                float DueNum = 0;
                try { DueNum = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputDueNumber")).Value); }
                catch { DueNum = 0; }

                float DueFZN = 0;
                try { DueFZN = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputDueFZN")).Value); }
                catch { DueFZN = 0; }

                float DueAmount = 0;
                try { DueAmount = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputDueAmount")).Value); }
                catch { DueAmount = 0; }

                float RN = 0;
                try { RN = Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxRN")).Text); }
                catch { RN = 0; }

                float RFZN = 0;
                try { RFZN = Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxRFZN")).Text); }
                catch { RFZN = 0; }

                float RA = 0;
                try { RA = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputRA")).Value); }
                catch { RA = 0; }

                float DiffNum = 0;
                try { DiffNum = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputDiffNum")).Value); }
                catch { DiffNum = 0; }

                float DiffFZN = 0;
                try { DiffFZN = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputDiffFZN")).Value); }
                catch { DiffFZN = 0; }

                float DiffAmount = 0;
                try { DiffAmount = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputDiffAmount")).Value); }
                catch { DiffAmount = 0; }

                string Comment = ((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;


                //明细状态---PD_STATE，没有用
                sql =
                    "UPDATE TBWS_INVENTORYRECORD SET " +
                    "PD_NUMNOTIN='" + NumNotIn + "',PD_FZNUMNOTIN='" + FZNNotIn + "',PD_AMOUNTNOTIN='" + AmountNotIn +
                    "',PD_NUMNOTOUT='" + NumNotOut + "',PD_FZNUMNOTOUT='" + FZNNotOut + "',PD_AMOUNTNOTOUT='" + AmountNotOut +
                    "',PD_DUENUM='" + DueNum + "',PD_DUEFZNUM='" + DueFZN + "',PD_DUEAMOUNT='" + DueAmount +
                    "',PD_REALNUM='" + RN + "',PD_REALFZNUM='" + RFZN + "',PD_REALAMOUNT='" + RA +
                    "',PD_DIFFNUM='" + DiffNum + "',PD_DIFFFZNUM='" + DiffFZN + "',PD_DIFFAMOUNT='" + DiffAmount +
                    "',PD_NOTE='" + Comment + "' WHERE PD_CODE='" + Code + "' AND PD_SQCODE='" + SQCODE + "'";
                sqllist.Add(sql);
            }
            DBCallCommon.ExecuteTrans(sqllist);
            sqllist.Clear();

            string script = "alert('提交成功！待审核!');window.opener.location = window.opener.location.href;window.close();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
        }





        //导出明细

        protected void Export_Click(object sender,EventArgs e)
        {
            List<string> lt = GetOrderField();
            string sql = "";
            string condition = GetCondition();
            if (condition != "")
            {
                sql = "SELECT MaterialCode,MaterialName,Attribute,Standard," +
                 "GB,LotNumber,Length,Width,Unit,UnitPrice," +
                 "cast(NumberInAccount as float) as NumberInAccount,cast(SupportNumberInAccount as float) as SupportNumberInAccount,AmountInAccount," +
                 "NumberNotIn,SupportNumberNotIn,AmountNotIn," +
                 "NumberNotOut,SupportNumberNotOut,AmountNotOut," +
                 "DueNumber,DueSupportNumber,DueAmount," +
                 "RealNumber,RealSupportNumber,RealAmount," +
                 "DiffNumber,DiffSupportNumber,DiffAmount," +
                 "Warehouse,Location,PTC,Note " +
                 " FROM View_SM_InventoryReport WHERE PDCode='" + Request.QueryString["ID"] + "' " + condition + " order by " + lt[0] + " " + switchDescAsc(lt[1]);
            }
            else
            {
                sql = "SELECT MaterialCode,MaterialName,Attribute,Standard," +
                "GB,LotNumber,Length,Width,Unit,UnitPrice," +
                "cast(NumberInAccount as float) as NumberInAccount,cast(SupportNumberInAccount as float) as SupportNumberInAccount,AmountInAccount," +
                "NumberNotIn,SupportNumberNotIn,AmountNotIn," +
                "NumberNotOut,SupportNumberNotOut,AmountNotOut," +
                "DueNumber,DueSupportNumber,DueAmount," +
                "RealNumber,RealSupportNumber,RealAmount," +
                "DiffNumber,DiffSupportNumber,DiffAmount," +
                "Warehouse,Location,PTC,Note " +
                " FROM View_SM_InventoryReport WHERE PDCode='" + Request.QueryString["ID"] + "' order by " + lt[0] + " " + switchDescAsc(lt[1]);
            }

            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            ExportDataItem(dt);

        }

        protected void INExport_Click(object sender, EventArgs e)
        {
            List<string> lt = GetOrderField();

            string sql = "SELECT SQCODE,PTC,MaterialCode,MaterialName,Attribute,Standard," +
                "GB,LotNumber,Length,Width,Warehouse,Location ," +
                "cast(NumberInAccount as float) as NumberInAccount,cast(SupportNumberInAccount as float) as SupportNumberInAccount," +
                "NumberNotIn,SupportNumberNotIn," +
                "NumberNotOut,SupportNumberNotOut," +
                "DueNumber,DueSupportNumber," +
                "RealNumber,RealSupportNumber," +
                "DiffNumber,DiffSupportNumber,Note" +

                " FROM View_SM_InventoryReport WHERE PDCode='" + Request.QueryString["ID"] + "'order by " + lt[0] + " " + switchDescAsc(lt[1]);

            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            ExportDataItemIn(dt);

        }
        private void ExportDataItemIn(System.Data.DataTable objdt)
        {
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("导入盘点模板") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            wksheet.Name = Request.QueryString["ID"];

            System.Data.DataTable dt = objdt;

            int rowCount = objdt.Rows.Count;

            int colCount = objdt.Columns.Count;

            wksheet.get_Range("A2", wksheet.Cells[1 + rowCount, colCount + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            wksheet.get_Range("A2", wksheet.Cells[1 + rowCount, colCount + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            wksheet.get_Range("A2", wksheet.Cells[1 + rowCount, colCount + 1]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            //wksheet.get_Range("A2", wksheet.Cells[1 + rowCount, colCount + 1]).NumberFormatLocal = "@";

            object[,] dataArray = new object[rowCount, colCount + 1];

            for (int i = 0; i < rowCount; i++)
            {
                dataArray[i, 0] = (object)(i + 1);

                for (int j = 0; j < colCount; j++)
                {

                    dataArray[i, j + 1] = objdt.Rows[i][j];

                }
            }

            wksheet.get_Range("A2", wksheet.Cells[1 + rowCount, colCount + 1]).Value2 = dataArray;
            //设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            wksheet.Protect("6123456", true, true, true, Type.Missing, Type.Missing, true, true, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, true, Type.Missing);

            string filename = Server.MapPath("/SM_Data/ExportFile/" + LabelCode.Text.Trim() + ".xls");

            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        private void ExportDataItem(System.Data.DataTable objdt)
        {
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("库存盘点基本模版明细") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            wksheet.get_Range("L4","L4").Value2 = "仓库名称：" + LabelWarehouse.Text.Trim();
            wksheet.get_Range("AC4", "AC4").Value2 = "盘点日期：" + LabelTime.Text.Trim().Substring(0,10);

            System.Data.DataTable dt = objdt;

            int rowCount = objdt.Rows.Count;

            int colCount = objdt.Columns.Count;

            wksheet.get_Range("A9", wksheet.Cells[8 + rowCount, colCount + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            wksheet.get_Range("A9", wksheet.Cells[8 + rowCount, colCount + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            wksheet.get_Range("A9", wksheet.Cells[8 + rowCount, colCount + 1]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            //wksheet.get_Range("A9", wksheet.Cells[8 + rowCount, colCount + 1]).NumberFormatLocal = "@";

            object[,] dataArray = new object[rowCount, colCount + 1];

            for (int i = 0; i < rowCount; i++)
            {
                dataArray[i, 0] = (object)(i + 1);

                for (int j = 0; j < colCount; j++)
                {

                    dataArray[i, j + 1] = objdt.Rows[i][j];

                }
            }

            wksheet.get_Range("A9", wksheet.Cells[8 + rowCount, colCount + 1]).Value2 = dataArray;
            //设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = Server.MapPath("/SM_Data/ExportFile/" + "Item" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
           
        }

        /// <summary>
        /// 输出Excel文件并退出
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="workbook"></param>
        private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
        {
            try
            {

                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Workbooks.Close();
                m_xlApp.Quit();
                m_xlApp.Application.Quit();

                #region kill excel process

                System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcessesByName("EXCEL");

                foreach (System.Diagnostics.Process p in procs)
                {
                    int baseAdd = p.MainModule.BaseAddress.ToInt32();
                    //oXL is Excel.ApplicationClass object 
                    if (baseAdd == m_xlApp.Hinstance)
                    {
                        p.Kill();
                        break;
                    }
                }

                #endregion

                System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);

                wksheet = null;
                workbook = null;
                m_xlApp = null;
                GC.Collect();

                DownloadFile.Send(Context, filename);
                //Response.Redirect(filename);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {

        }



        /**************************Tab2*********************************/

        protected void Verify_Click(object sender, EventArgs e)
        {
            string Verifiercode = LabelVerifierCode.Text;
            string Approvedate = LabelApproveDate.Text;
            string Advice = TextBoxAdvice.Text;
            string sql = "UPDATE TBWS_INVENTORYSCHEMA SET PD_DONESTATE='2',PD_VERIFIER='" + Verifiercode +
                "',PD_VERIFYDATE='" + Approvedate + "',PD_VERIFYADV='" + Advice +
                "' WHERE PD_CODE='" + LabelCode.Text + "'";
            DBCallCommon.ExeSqlText(sql);

            string script = "window.opener.location = window.opener.location.href;window.close();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
        }

        protected void Deny_Click(object sender, EventArgs e)
        {
            string Verifiercode = LabelVerifierCode.Text;
            string Approvedate = LabelApproveDate.Text;
            string Advice = TextBoxAdvice.Text;
            string sql = "UPDATE TBWS_INVENTORYSCHEMA SET PD_DONESTATE='0',PD_VERIFIER='" + Verifiercode +
                "',PD_VERIFYDATE='" + Approvedate + "',PD_VERIFYADV='" + Advice +
                "',PD_NOTE='驳回!' WHERE PD_CODE='" + LabelCode.Text + "'";
            DBCallCommon.ExeSqlText(sql);

            string script = "window.opener.location = window.opener.location.href;window.close();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

        }

        /*反审核*/
        protected void AntiVerify_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE TBWS_INVENTORYSCHEMA SET PD_DONESTATE='1',PD_VERIFIER=null,PD_VERIFYDATE=null,PD_VERIFYADV=null WHERE PD_CODE='" + LabelCode.Text + "'";
            DBCallCommon.ExeSqlText(sql);
            string script = "window.opener.location = window.opener.location.href;window.close();";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
        }

        //根据盘点结果调整库存
        protected void Adjust_Click(object sender, EventArgs e)
        {
            string sql = DBCallCommon.GetStringValue("connectionStrings");
            SqlConnection con = new SqlConnection(sql);
            con.Open();
            SqlCommand cmd = new SqlCommand("Adjust", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter spr;
            spr = cmd.Parameters.Add("@InventoryCode", SqlDbType.VarChar, 50);
            cmd.Parameters["@InventoryCode"].Value = LabelCode.Text;
            cmd.ExecuteNonQuery();
            con.Close();
            sql = "UPDATE TBWS_INVENTORYSCHEMA SET PD_DONESTATE='3' WHERE PD_CODE='" + LabelCode.Text + "'";
            DBCallCommon.ExeSqlText(sql);
            LabelMessage.Text = "库存盘点调整完毕！";
        }



        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as System.Web.UI.WebControls.CheckBox).Text.Trim() != "标准件" && (sender as System.Web.UI.WebControls.CheckBox).Text.Trim() != "财务")
            {
                HideControlCell(sender);
            }
            else if ((sender as System.Web.UI.WebControls.CheckBox).Text.Trim() == "标准件")
            {
                HideStrControlCell(sender);
            }
            else if ((sender as System.Web.UI.WebControls.CheckBox).Text.Trim() == "财务")
            {
                HideCWControlCell(sender);
            }
        }
        private void HideCWControlCell(object sender)
        {
            List<string> lt = new List<string>();

            lt.Add("单价");
            lt.Add("金额");
            lt.Add("尚未入库金额");
            lt.Add("尚未出库金额");
            lt.Add("应结存金额");
            lt.Add("盘点金额");
            lt.Add("差异金额");

            if ((sender as System.Web.UI.WebControls.CheckBox).Checked)
            {
                CheckBox11.Enabled = false;
                CheckBox11.Checked = true;
                CheckBox12.Enabled = false;
                CheckBox12.Checked = true;
                CheckBox13.Enabled = false;
                CheckBox13.Checked = true;
                CheckBox14.Enabled = false;
                CheckBox14.Checked = true;
                CheckBox15.Enabled = false;
                CheckBox15.Checked = true;
                CheckBox16.Enabled = false;
                CheckBox16.Checked = true;
                CheckBox17.Enabled = false;
                CheckBox17.Checked = true;


                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    foreach (string st in lt)
                    {
                        if (GridView1.Columns[i].HeaderText.Trim() == st)
                        {
                            GridView1.Columns[i].HeaderStyle.CssClass = "hidden";
                            GridView1.Columns[i].ItemStyle.CssClass = "hidden";
                            GridView1.Columns[i].FooterStyle.CssClass = "hidden";
                        }
                    }

                }
            }
            else
            {

                CheckBox11.Enabled = true;
                CheckBox11.Checked = false;
                CheckBox12.Enabled = true;
                CheckBox12.Checked = false;
                CheckBox13.Enabled = true;
                CheckBox13.Checked = false;
                CheckBox14.Enabled = true;
                CheckBox14.Checked = false;
                CheckBox15.Enabled = true;
                CheckBox15.Checked = false;
                CheckBox16.Enabled = true;
                CheckBox16.Checked = false;
                CheckBox17.Enabled = true;
                CheckBox17.Checked = false;

                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    foreach (string st in lt)
                    {
                        if (GridView1.Columns[i].HeaderText.Trim() == st)
                        {
                            GridView1.Columns[i].HeaderStyle.CssClass = GridView1.Columns[i].HeaderStyle.RegisteredCssClass;
                            GridView1.Columns[i].ItemStyle.CssClass = GridView1.Columns[i].ItemStyle.RegisteredCssClass;
                            GridView1.Columns[i].FooterStyle.CssClass = GridView1.Columns[i].FooterStyle.RegisteredCssClass;
                        }
                    }

                }
            }

        }

        private void HideStrControlCell(object sender)
        {
            List<string> lt = new List<string>();

            lt.Add("批号");
            lt.Add("长");
            lt.Add("宽");
            lt.Add("张(支)数");
            lt.Add("尚未入库张(支)数");
            lt.Add("尚未出库张(支)数");
            lt.Add("应结存张(支)数");
            lt.Add("盘点张(支)数");
            lt.Add("差异张(支)数");

         
            if ((sender as System.Web.UI.WebControls.CheckBox).Checked)
            {

                CheckBox2.Enabled = false;
                CheckBox2.Checked = true;
                CheckBox3.Enabled = false;
                CheckBox3.Checked = true;
                CheckBox4.Enabled = false;
                CheckBox4.Checked = true;
                CheckBox5.Enabled = false;
                CheckBox5.Checked = true;
                CheckBox6.Enabled = false;
                CheckBox6.Checked = true;

                CheckBox7.Enabled = false;
                CheckBox7.Checked = true;
                CheckBox8.Enabled = false;
                CheckBox8.Checked = true;
                CheckBox9.Enabled = false;
                CheckBox9.Checked = true;
                CheckBox10.Enabled = false;
                CheckBox10.Checked = true;

               

                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    foreach (string st in lt)
                    {
                        if (GridView1.Columns[i].HeaderText.Trim() == st)
                        {
                            GridView1.Columns[i].HeaderStyle.CssClass = "hidden";
                            GridView1.Columns[i].ItemStyle.CssClass = "hidden";
                            GridView1.Columns[i].FooterStyle.CssClass = "hidden";
                        }
                    }

                }
            }
            else
            {
                CheckBox2.Enabled = true;
                CheckBox2.Checked = false;
                CheckBox3.Enabled = true;
                CheckBox3.Checked = false;
                CheckBox4.Enabled = true;
                CheckBox4.Checked = false;
                CheckBox5.Enabled = true;
                CheckBox5.Checked = false;
                CheckBox6.Enabled = true;
                CheckBox6.Checked = false;

                CheckBox7.Enabled = true;
                CheckBox7.Checked = false;
                CheckBox8.Enabled = true;
                CheckBox8.Checked = false;
                CheckBox9.Enabled = true;
                CheckBox9.Checked = false;
                CheckBox10.Enabled = true;
                CheckBox10.Checked = false;
               
                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    foreach (string st in lt)
                    {
                        if (GridView1.Columns[i].HeaderText.Trim() == st)
                        {
                            GridView1.Columns[i].HeaderStyle.CssClass = GridView1.Columns[i].HeaderStyle.RegisteredCssClass;
                            GridView1.Columns[i].ItemStyle.CssClass = GridView1.Columns[i].ItemStyle.RegisteredCssClass;
                            GridView1.Columns[i].FooterStyle.CssClass = GridView1.Columns[i].FooterStyle.RegisteredCssClass;
                        }
                    }

                }
            }

        }


        private void HideControlCell(object sender)
        {
            for (int i = 0; i < GridView1.Columns.Count; i++)
            {
                if (GridView1.Columns[i].HeaderText.Trim() == (sender as System.Web.UI.WebControls.CheckBox).Text.Trim())
                {
                    if ((sender as System.Web.UI.WebControls.CheckBox).Checked)
                    {

                        GridView1.Columns[i].HeaderStyle.CssClass = "hidden";
                        GridView1.Columns[i].ItemStyle.CssClass = "hidden";
                        GridView1.Columns[i].FooterStyle.CssClass = "hidden";
                    }
                    else
                    {

                        GridView1.Columns[i].HeaderStyle.CssClass = GridView1.Columns[i].HeaderStyle.RegisteredCssClass;
                        GridView1.Columns[i].ItemStyle.CssClass = GridView1.Columns[i].ItemStyle.RegisteredCssClass;
                        GridView1.Columns[i].FooterStyle.CssClass = GridView1.Columns[i].FooterStyle.RegisteredCssClass;
                    }
                }
            }
        }

        protected void DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string firstvalue = DropDownListFirst.SelectedValue;

            if (DropDownListSecond.SelectedValue == firstvalue && DropDownListSecond.SelectedValue!=string.Empty)
            {
                DropDownListSecond.ClearSelection();
                foreach (ListItem lt in DropDownListSecond.Items)
                {
                    if (lt.Value != firstvalue)
                    {
                        lt.Selected = true;
                        break;
                    }
                   
                }
            }

            string secondvalue = DropDownListSecond.SelectedValue;

            if (DropDownListThird.SelectedValue == secondvalue && DropDownListThird.SelectedValue != string.Empty)
            {
                DropDownListThird.ClearSelection();

                foreach (ListItem lt in DropDownListThird.Items)
                {
                    if (lt.Value != firstvalue && lt.Value != secondvalue)
                    {
                        lt.Selected = true;
                        break;
                    }
                }
            }

            List<string> ltOrder = GetOrderField();

            pager.OrderField = ltOrder[0];

            pager.OrderType = Convert.ToInt32(ltOrder[1]);

            UCPaging1.CurrentPage=1;

            bindGrid();

        }
        private string switchDescAsc(string obj)
        {
            if (obj == "0")
            {
                return "ASC";
            }
            else
            {
                return "DESC";
            }
        }

        private List<string> GetOrderField()
        {
            List<string> lt = new List<string>();

            string orderfield = string.Empty;

            string ordertype = string.Empty;

            if (DropDownListThird.SelectedValue != string.Empty && DropDownListSecond.SelectedValue != string.Empty)
            {
                orderfield = DropDownListFirst.SelectedValue + " " + switchDescAsc(RadioButtonListFirstType.SelectedValue) + ", " + DropDownListSecond.SelectedValue + " " + switchDescAsc(RadioButtonListSecond.SelectedValue) + ", " + DropDownListThird.SelectedValue;
                
                ordertype = RadioButtonListThird.SelectedValue;
            }
            else if (DropDownListThird.SelectedValue != string.Empty && DropDownListSecond.SelectedValue == string.Empty)
            {
                orderfield = DropDownListFirst.SelectedValue + " " + switchDescAsc(RadioButtonListFirstType.SelectedValue) + ", " + DropDownListThird.SelectedValue;

                ordertype = RadioButtonListThird.SelectedValue;
            }

            else if (DropDownListSecond.SelectedValue != string.Empty && DropDownListThird.SelectedValue == string.Empty)
            {
                orderfield = DropDownListFirst.SelectedValue + " " + switchDescAsc(RadioButtonListFirstType.SelectedValue) + ", " + DropDownListSecond.SelectedValue;

                ordertype = RadioButtonListSecond.SelectedValue;
            }
            else if (DropDownListSecond.SelectedValue == string.Empty && DropDownListThird.SelectedValue == string.Empty)
            {
                orderfield = DropDownListFirst.SelectedValue;

                ordertype = RadioButtonListFirstType.SelectedValue;
            }


            lt.Add(orderfield);
            lt.Add(ordertype);

            return lt;

        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            string sql = "update TBWS_INVENTORYSCHEMA SET PD_OREDERFIELD='" + DropDownListFirst.SelectedValue + "',PD_OREDERTYPE='" + RadioButtonListFirstType.SelectedValue + "',PD_OREDERFIELD1='" + DropDownListSecond.SelectedValue + "',PD_OREDERTYPE1='" + RadioButtonListSecond.SelectedValue + "',PD_OREDERFIELD2='" + DropDownListThird.SelectedValue+ "',PD_OREDERTYPE2='" + RadioButtonListThird.SelectedValue + "' WHERE PD_CODE='" + LabelCode.Text.Trim() + "'";
           
            DBCallCommon.ExeSqlText(sql);
        }


        //

        protected void Uniprice_Click(object sender, EventArgs e)
        {
            string pdtype = string.Empty;
            List<string> strsql = new List<string>();

            string sqlText = "select PD_SCHEMATYPE from TBWS_INVENTORYSCHEMA where PD_CODE='" + LabelCode.Text.Trim() + "'";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);

            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    pdtype = "0";
                }
                else if (dr[0].ToString() == "1")
                {
                    pdtype = "1";
                }
            }
            dr.Close();

            if (pdtype == "0")
            {
                string sqlstring = DBCallCommon.GetStringValue("connectionStrings");
                SqlConnection con = new SqlConnection(sqlstring);
                con.Open();
                SqlCommand cmd = new SqlCommand("GetUniPrice", con);
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Code", SqlDbType.VarChar, 50);				//增加参数截止日期@EndDate
                    cmd.Parameters["@Code"].Value = LabelCode.Text.Trim();			        //系统封账时间
                    cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;			//增加返回值参数@retVal
                    cmd.ExecuteNonQuery();
                    con.Close();
                    con.Dispose();

                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 0)
                    {
                        cmd.Dispose();

                        InitVar();

                        bindGrid();
                    }
                    else
                    {
                        cmd.Dispose();
                        LabelMessage.Text = "单价计算错误，请重新计算！";
                    }

                }
                catch
                {
                    con.Close();
                    con.Dispose();
                    cmd.Dispose();
                }
            }
            else if (pdtype == "1")
            {
                string sql1 = " UPDATE TBWS_INVENTORYRECORD SET PD_AMOUNTNOTIN=PD_NUMNOTIN*PD_UNITPRICE,PD_AMOUNTNOTOUT=PD_NUMNOTOUT*PD_UNITPRICE,PD_DIFFAMOUNT=PD_DIFFNUM*PD_UNITPRICE WHERE PD_CODE='" + LabelCode.Text.Trim() + "'";
                strsql.Add(sql1);
                string sql2 = "UPDATE TBWS_INVENTORYRECORD SET PD_DUEAMOUNT=PD_AMOUNT+PD_NUMNOTIN-PD_NUMNOTOUT,PD_REALAMOUNT=PD_DIFFAMOUNT+PD_AMOUNT+PD_NUMNOTIN-PD_NUMNOTOUT WHERE PD_CODE='" + LabelCode.Text.Trim() + "'";
                strsql.Add(sql2);
                DBCallCommon.ExecuteTrans(strsql);

                InitVar();

                bindGrid();
              
            }

        }
    }
}
