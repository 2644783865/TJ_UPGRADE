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
using Microsoft.Office.Interop.Excel;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_InventoryManage : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
                bindData();
                this.Form.DefaultButton = btnQuery.UniqueID;
                CheckUser(ControlFinder);

            }
        }

        private void InitVar()
        {
            GetInventoryInfo();
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

            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);

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


        protected void GetInventoryInfo()
        {
            string state = DropDownListState.SelectedValue.ToString();
            //string sql = "";
            string condition = "";
            //盘点编号条件
            if (TextBoxCode.Text != "")
            {
                condition = " PDCode LIKE '%" + TextBoxCode.Text.Trim() + "%'";
            }
            //方案制定日期条件
            if ((TextBoxSchemaDate.Text != "") && (condition != ""))
            {
                condition += " AND " + " SchemaDate LIKE '%" + TextBoxSchemaDate.Text.Trim() + "%'";
            }
            else if ((TextBoxSchemaDate.Text != "") && (condition == ""))
            {
                condition += " SchemaDate LIKE '%" + TextBoxSchemaDate.Text.Trim() + "%'";
            }
            //盘点人条件
            if ((TextBoxClerk.Text != "") && (condition != ""))
            {
                condition += " AND " + " Clerk LIKE '%" + TextBoxClerk.Text.Trim() + "%'";
            }
            else if ((TextBoxClerk.Text != "") && (condition == ""))
            {
                condition += " Clerk LIKE '%" + TextBoxClerk.Text.Trim() + "%'";
            }
            //审核人条件
            if ((TextBoxVerifier.Text != "") && (condition != ""))
            {
                condition += " AND " + " Verifier LIKE '%" + TextBoxVerifier.Text.Trim() + "%'";
            }
            else if ((TextBoxVerifier.Text != "") && (condition == ""))
            {
                condition += " Verifier LIKE '%" + TextBoxVerifier.Text.Trim() + "%'";
            }



            /* 
             * 0表示 “未提交”===制单人
             * 1表示 “待审核”===审核人---去审核
             * 2表示 “待调整”===主管---去调整
             * 3表示 “已调整”===大家都能看
             */
            switch (state)
            {
                case "": break;
                case "0":
                    if (condition != "") { condition += " AND State='0' AND ZDRID='" + Session["UserID"].ToString() + "'"; } //AND ZDRID='" + Session["UserID"].ToString() + "'
                    else { condition += " State='0' AND ZDRID='" + Session["UserID"].ToString() + "' "; } //AND ZDRID='" + Session["UserID"].ToString() + "'
                    break;
                case "1":
                    if (condition != "") { condition += " AND State='1' AND ManagerCode='" + Session["UserID"].ToString() + "'"; }
                    else { condition += " State='1' AND ManagerCode='" + Session["UserID"].ToString() + "'"; }
                    break;
                case "2":
                    if (condition != "") { condition += " AND State='2' AND (ManagerCode='" + Session["UserID"].ToString() + "' or ZDRID='" + Session["UserID"].ToString() + "')"; }
                    else { condition += " State='2' AND ( ManagerCode='" + Session["UserID"].ToString() + "'or ZDRID='" + Session["UserID"].ToString() + "')"; }
                    break;
                case "3":
                    if (condition != "") { condition += " AND State='3' "; }
                    else { condition += " State='3' "; }
                    break;
                default: break;
            }

            //仓库盘点方案表

            if (condition == "")
            {
                string TableName = "View_SM_InventorySchema";
                string PrimaryKey = "PDCode";
                string ShowFields = "PDCode AS Code,SchemaDate AS Date,PlanerCode AS PlanerCode,Planer AS Planer,WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,PJCode AS ProjectCode,(case when PD_SCHEMATYPE='0' then '即时库存' else '财务核算' end) as PD_SCHEMATYPE,MTCode AS MaterialTypeCode,MT AS MaterialType,ClerkCode AS ClerkCode,Clerk AS Clerk,PDDate AS DoneDate,State AS State,VerifierCode AS VerifierCode,Verifier AS Verifier,ManagerCode AS ManagerCode,Manager AS Manager,VerifierDate AS ApproveDate,ZDRNM AS ZDRNM,Advice AS Advice,Note AS Comment ";
                string OrderField = "PDCode";
                int OrderType =1;
                string StrWhere = "";
                int PageSize = 10;

                InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);

            }
            else
            {
                string TableName = "View_SM_InventorySchema";
                string PrimaryKey = "PDCode";
                string ShowFields = "PDCode AS Code,SchemaDate AS Date,PlanerCode AS PlanerCode,Planer AS Planer,WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,PJCode AS ProjectCode,(case when PD_SCHEMATYPE='0' then '即时库存' else '财务核算' end) as PD_SCHEMATYPE,MTCode AS MaterialTypeCode,MT AS MaterialType,ClerkCode AS ClerkCode,Clerk AS Clerk,PDDate AS DoneDate,State AS State,VerifierCode AS VerifierCode,Verifier AS Verifier,ManagerCode AS ManagerCode,Manager AS Manager,VerifierDate AS ApproveDate,ZDRNM AS ZDRNM,Advice AS Advice,Note AS Comment ";
                string OrderField = "PDCode";
                int OrderType = 1;
                string StrWhere = condition;
                int PageSize = 10;

                InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);

            }

        
        }

        protected string convertState(string state)
        {
            switch (state)
            {
                case "0": return "正在编辑...";
                case "1": return "等待审核...";
                case "2": return "等待调整...";
                case "3": return "调整完毕";
                default: return state;
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                if (DropDownListState.SelectedValue == "0")
                {
                    HtmlTableCell cell1 = (HtmlTableCell)e.Item.FindControl("BJH");
                    cell1.Visible = true;
                    HtmlTableCell cell2 = (HtmlTableCell)e.Item.FindControl("SHH");
                    cell2.Visible = false;
                    HtmlTableCell cell3 = (HtmlTableCell)e.Item.FindControl("TZH");
                    cell3.Visible = false;
                    HtmlTableCell cell4 = (HtmlTableCell)e.Item.FindControl("CKH");
                    cell4.Visible = false;
                }
                if (DropDownListState.SelectedValue == "1")
                {
                    HtmlTableCell cell1 = (HtmlTableCell)e.Item.FindControl("BJH");
                    cell1.Visible = false;
                    HtmlTableCell cell2 = (HtmlTableCell)e.Item.FindControl("SHH");
                    cell2.Visible = true;
                    HtmlTableCell cell3 = (HtmlTableCell)e.Item.FindControl("TZH");
                    cell3.Visible = false; 
                    HtmlTableCell cell4 = (HtmlTableCell)e.Item.FindControl("CKH");
                    cell4.Visible = false;
                }
                if (DropDownListState.SelectedValue == "2")
                {
                    HtmlTableCell cell1 = (HtmlTableCell)e.Item.FindControl("BJH");
                    cell1.Visible = false;
                    HtmlTableCell cell2 = (HtmlTableCell)e.Item.FindControl("SHH");
                    cell2.Visible = false;
                    HtmlTableCell cell3 = (HtmlTableCell)e.Item.FindControl("TZH");
                    cell3.Visible = true;
                    HtmlTableCell cell4 = (HtmlTableCell)e.Item.FindControl("CKH");
                    cell4.Visible = false;
                }
                if (DropDownListState.SelectedValue == "3" || DropDownListState.SelectedValue == "")
                {
                    HtmlTableCell cell1 = (HtmlTableCell)e.Item.FindControl("BJH");
                    cell1.Visible = false;
                    HtmlTableCell cell2 = (HtmlTableCell)e.Item.FindControl("SHH");
                    cell2.Visible = false;
                    HtmlTableCell cell3 = (HtmlTableCell)e.Item.FindControl("TZH");
                    cell3.Visible = false;
                    HtmlTableCell cell4 = (HtmlTableCell)e.Item.FindControl("CKH");
                    cell4.Visible = true;
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (DropDownListState.SelectedValue == "0")
                {
                    HtmlTableCell cell1 = (HtmlTableCell)e.Item.FindControl("BJ");
                    cell1.Visible = true;
                    HtmlTableCell cell2 = (HtmlTableCell)e.Item.FindControl("SH");
                    cell2.Visible = false;
                    HtmlTableCell cell3 = (HtmlTableCell)e.Item.FindControl("TZ");
                    cell3.Visible = false;
                    HtmlTableCell cell4 = (HtmlTableCell)e.Item.FindControl("CK");
                    cell4.Visible = false;
                }
                if (DropDownListState.SelectedValue == "1")
                {
                    HtmlTableCell cell1 = (HtmlTableCell)e.Item.FindControl("BJ");
                    cell1.Visible = false;
                    HtmlTableCell cell2 = (HtmlTableCell)e.Item.FindControl("SH");
                    cell2.Visible = true;
                    HtmlTableCell cell3 = (HtmlTableCell)e.Item.FindControl("TZ");
                    cell3.Visible = false;
                    HtmlTableCell cell4 = (HtmlTableCell)e.Item.FindControl("CK");
                    cell4.Visible = false;
                }
                if (DropDownListState.SelectedValue == "2")
                {
                    HtmlTableCell cell1 = (HtmlTableCell)e.Item.FindControl("BJ");
                    cell1.Visible = false;
                    HtmlTableCell cell2 = (HtmlTableCell)e.Item.FindControl("SH");
                    cell2.Visible = false;
                    HtmlTableCell cell3 = (HtmlTableCell)e.Item.FindControl("TZ");
                    cell3.Visible = true;
                    HtmlTableCell cell4 = (HtmlTableCell)e.Item.FindControl("CK");
                    cell4.Visible = false;
                }
                if (DropDownListState.SelectedValue == "3" || DropDownListState.SelectedValue == "")
                {
                    HtmlTableCell cell1 = (HtmlTableCell)e.Item.FindControl("BJ");
                    cell1.Visible = false;
                    HtmlTableCell cell2 = (HtmlTableCell)e.Item.FindControl("SH");
                    cell2.Visible = false;
                    HtmlTableCell cell3 = (HtmlTableCell)e.Item.FindControl("TZ");
                    cell3.Visible = false;
                    HtmlTableCell cell4 = (HtmlTableCell)e.Item.FindControl("CK");
                    cell4.Visible = true;
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                if (DropDownListState.SelectedValue == "0")
                {
                    HtmlTableCell cell1 = (HtmlTableCell)e.Item.FindControl("BJF");
                    cell1.Visible = true;
                    HtmlTableCell cell2 = (HtmlTableCell)e.Item.FindControl("SHF");
                    cell2.Visible = false;
                    HtmlTableCell cell3 = (HtmlTableCell)e.Item.FindControl("TZF");
                    cell3.Visible = false;
                    HtmlTableCell cell4 = (HtmlTableCell)e.Item.FindControl("CKF");
                    cell4.Visible = false;
                }
                if (DropDownListState.SelectedValue == "1")
                {
                    HtmlTableCell cell1 = (HtmlTableCell)e.Item.FindControl("BJF");
                    cell1.Visible = false;
                    HtmlTableCell cell2 = (HtmlTableCell)e.Item.FindControl("SHF");
                    cell2.Visible = true;
                    HtmlTableCell cell3 = (HtmlTableCell)e.Item.FindControl("TZF");
                    cell3.Visible = false;
                    HtmlTableCell cell4 = (HtmlTableCell)e.Item.FindControl("CKF");
                    cell4.Visible = false;
                }
                if (DropDownListState.SelectedValue == "2")
                {
                    HtmlTableCell cell1 = (HtmlTableCell)e.Item.FindControl("BJF");
                    cell1.Visible = false;
                    HtmlTableCell cell2 = (HtmlTableCell)e.Item.FindControl("SHF");
                    cell2.Visible = false;
                    HtmlTableCell cell3 = (HtmlTableCell)e.Item.FindControl("TZF");
                    cell3.Visible = true;
                    HtmlTableCell cell4 = (HtmlTableCell)e.Item.FindControl("CKF");
                    cell4.Visible = false;
                }
                if (DropDownListState.SelectedValue == "3" || DropDownListState.SelectedValue == "")
                {
                    HtmlTableCell cell1 = (HtmlTableCell)e.Item.FindControl("BJF");
                    cell1.Visible = false;
                    HtmlTableCell cell2 = (HtmlTableCell)e.Item.FindControl("SHF");
                    cell2.Visible = false;
                    HtmlTableCell cell3 = (HtmlTableCell)e.Item.FindControl("TZF");
                    cell3.Visible = false;
                    HtmlTableCell cell4 = (HtmlTableCell)e.Item.FindControl("CKF");
                    cell4.Visible = true;
                }
            }
        }


        //查询
        protected void Query_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;

            bindData();

            ModalPopupExtenderSearch.Hide();

            UpdatePanelBody.Update();
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
            //审核状态
            foreach (ListItem lt in DropDownListState.Items)
            {
                if (lt.Selected)
                {
                    lt.Selected = false;
                }
            }
            DropDownListState.Items[0].Selected = true;

            TextBoxCode.Text = string.Empty;
            TextBoxSchemaDate.Text = string.Empty;
            TextBoxClerk.Text = string.Empty;
            TextBoxVerifier.Text = string.Empty;
        }


        //制定新的盘点方案
        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Warehouse_InventorySchema.aspx");
        }


        protected void BtnDelete_Click(object sender, EventArgs e)
        {

            List<string> strlt = new List<string>();

            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                if ((Repeater1.Items[i].FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    if ((Repeater1.Items[i].FindControl("LabelPlaner") as System.Web.UI.WebControls.Label).Text == Session["UserName"].ToString())
                    {
                        if ((Repeater1.Items[i].FindControl("LabelState") as System.Web.UI.WebControls.Label).Text.Trim() == "正在编辑...")
                        {
                            //LabelCode
                            string code = (Repeater1.Items[i].FindControl("LabelCode") as System.Web.UI.WebControls.Label).Text.Trim();
                            string sqlstr = "delete from TBWS_INVENTORYSCHEMA where PD_CODE='" + code + "'";
                            strlt.Add(sqlstr);
                            sqlstr = "delete from TBWS_INVENTORYRECORD where PD_CODE='" + code + "'";
                            strlt.Add(sqlstr);
                            DBCallCommon.ExecuteTrans(strlt);

                            Response.Redirect("SM_Warehouse_InventoryManage.aspx");

                        }
                        else
                        {
                            LabelMessage.Visible = true;
                            LabelMessage.Text = "方案已提交，无法删除!";
                        }
                    }
                    else
                    {
                        LabelMessage.Visible = true;
                        LabelMessage.Text = "方案制定人不是本人，无法删除!";
                       
                    }
                }
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            List<string> strlt = new List<string>();

            List<System.Data.DataTable> ltdt = new List<System.Data.DataTable>();
         
           
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                if ((Repeater1.Items[i].FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string code = (Repeater1.Items[i].FindControl("LabelCode") as System.Web.UI.WebControls.Label).Text.Trim();
                    strlt.Add(code);
                }
            }

            if (strlt.Count > 0)
            {
                for (int i = 0; i < strlt.Count;i++ )
                {
                    string sql = "SELECT MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute,Standard AS MaterialStandard,GB,Unit AS Unit,cast(UnitPrice as float) AS UnitPrice," +
                    "cast(SUM(NumberInAccount) as float) AS NumInAccount,cast(SUM(AmountInAccount) as float) AS AmountInAccount,cast(SUM(NumberNotIn) as float) AS NumNotIn,cast(SUM(AmountNotIn) as float) AS AmountNotIn," +
                    "cast(SUM(NumberNotOut) as float) AS NumNotOut,cast(SUM(AmountNotOut) as float) AS AmountNotOut,cast(SUM(DueNumber) as float) AS NumDueToAccount,cast(SUM(DueAmount) as float) AS AmountDueToAccount," +
                    " cast(SUM(RealNumber) as float) AS NumInventory,cast(SUM(RealAmount) as float) AS AmountInventory,cast(SUM(DiffNumber) as float) AS NumDiff,cast(SUM(DiffAmount) as float) AS AmountDiff " +
                    "FROM View_SM_InventoryReport " +
                    "WHERE PDCode='" + strlt[i] +
                    "' GROUP BY MaterialCode,MaterialName,Attribute,Standard,GB,Unit,UnitPrice order by MaterialCode,MaterialName,Attribute,Standard,GB,Unit,UnitPrice";

                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                    ltdt.Add(dt);
                }

                ExportExcelData(ltdt);
            }
        }

        private void ExportExcelData(List<System.Data.DataTable> objlt)
        {
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(Server.MapPath("库存盘点基本模版汇总2") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            //wksheet.get_Range("I4", "I4").Value2 = "仓库名称：" + LabelWarehouse.Text.Trim();
            //wksheet.get_Range("S4", "S4").Value2 = "盘点日期：" + LabelTime.Text.Trim().Substring(0, 10);

            int objrowCount = 0;

            int objcolCount = 0;

            for (int m = 0; m < objlt.Count; m++)
            {
                if (m == 0)
                {
                    System.Data.DataTable objdt = objlt[m];

                    int rowCount = objdt.Rows.Count;

                    int colCount = objdt.Columns.Count;

                    objcolCount = colCount;
                   
                    object[,] dataArray = new object[rowCount, colCount + 1];

                    for (int i = 0; i < rowCount; i++)
                    {
                        dataArray[i, 0] = (object)(i + 1);

                        for (int j = 0; j < colCount; j++)
                        {

                            dataArray[i, j + 1] = objdt.Rows[i][j];

                        }
                    }
                    //8+4行,12行
                    wksheet.get_Range("A9", wksheet.Cells[8 + rowCount, colCount + 1]).Value2 = dataArray;
                    //8+4+1 13行
                    objrowCount = objrowCount+rowCount + 8 + 1;//加1是为了空一行
                    if (rowCount > 0)
                    wksheet.get_Range("C" + (objrowCount).ToString(), "C" + (objrowCount).ToString()).Value2 = "合计";
                }
                else
                {
                    System.Data.DataTable objdt = objlt[m];

                    int rowCount = objdt.Rows.Count;

                    int colCount = objdt.Columns.Count;

                    object[,] dataArray = new object[rowCount, colCount + 1];

                    for (int i = 0; i < rowCount; i++)
                    {
                        dataArray[i, 0] = (object)(i + 1);

                        for (int j = 0; j < colCount; j++)
                        {

                            dataArray[i, j + 1] = objdt.Rows[i][j];

                        }
                    }
                    //13+1，14行开始
                    wksheet.get_Range("A" + (objrowCount+1).ToString(), wksheet.Cells[objrowCount + rowCount, colCount + 1]).Value2 = dataArray;

                    objrowCount = objrowCount + rowCount + 1;//加1是为了空一行
                    if (rowCount>0)
                    wksheet.get_Range("C" + (objrowCount).ToString(), "C" + (objrowCount).ToString()).Value2 = "合计";
                
                }
            }

            wksheet.get_Range("A9", wksheet.Cells[objrowCount, objcolCount + 3]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            wksheet.get_Range("A9", wksheet.Cells[objrowCount, objcolCount + 3]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            wksheet.get_Range("A9", wksheet.Cells[objrowCount, objcolCount + 3]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            wksheet.get_Range("A9", wksheet.Cells[objrowCount, objcolCount + 3]).NumberFormatLocal = "@";

            string filename = Server.MapPath("/SM_Data/ExportFile/" + "SummaryOne" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);

        }



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

                string script = String.Format("EndDownload()");

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", "EndDownload();", true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected void btnExportDif_Click(object sender, EventArgs e)
        {
            List<string> strlt = new List<string>();

            List<System.Data.DataTable> ltdt = new List<System.Data.DataTable>();


            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                if ((Repeater1.Items[i].FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string code = (Repeater1.Items[i].FindControl("LabelCode") as System.Web.UI.WebControls.Label).Text.Trim();
                    strlt.Add(code);
                }
            }

            if (strlt.Count > 0)
            {
                for (int i = 0; i < strlt.Count; i++)
                {
                    string sql = "SELECT MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute,Standard AS MaterialStandard,GB,Unit AS Unit,cast(UnitPrice as float) AS UnitPrice," +
                    "cast(NumberInAccount as float) AS NumInAccount,cast(AmountInAccount as float) AS AmountInAccount,cast(NumberNotIn as float) AS NumNotIn,cast(AmountNotIn as float) AS AmountNotIn," +
                    "cast(NumberNotOut as float) AS NumNotOut,cast(AmountNotOut as float) AS AmountNotOut,cast(DueNumber as float) AS NumDueToAccount,cast(DueAmount as float) AS AmountDueToAccount," +
                    " cast(RealNumber as float) AS NumInventory,cast(RealAmount as float) AS AmountInventory,cast(DiffNumber as float) AS NumDiff,cast(DiffAmount as float) AS AmountDiff ,Note " +
                    "FROM View_SM_InventoryReport " +
                    "WHERE PDCode='" + strlt[i] + "' and DiffNumber<>0";

                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                    ltdt.Add(dt);
                }

                ExportExcelData(ltdt);
            }
        }

    }
}
