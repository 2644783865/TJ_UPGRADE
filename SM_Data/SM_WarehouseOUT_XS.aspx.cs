using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_WarehouseOUT_XS : System.Web.UI.Page
    {
        private double tdn = 0;
        private double trn = 0;
        private float tdqn = 0;
        private float trqn = 0;
        private double xsje = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetCompany();
                //GetDep();
                GetStaff();
                initial();
            }
        }

        //获取系统封账时间
        private void ClosingAccountDate(string ZDDate)
        {
            string NowDate = ZDDate;
            //string NowDate = "20111030";
            string sql = "select HS_TIME from TBFM_HSTOTAL where  HS_YEAR='" + NowDate.Substring(0, 4) + "' and HS_MONTH='" + NowDate.Substring(5, 2) + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                LabelClosingAccount.Text = dt.Rows[0]["HS_TIME"].ToString();
                LabelClosingAccount.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                LabelClosingAccount.Text = "NoTime";
            }

        }

        //得到下个月的第一天

        protected string getNextMonth()
        {
            return System.DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01";
        }

        //购货单位
        protected void GetCompany()
        {
            string sql = "SELECT DISTINCT CS_CODE,CS_NAME FROM TBCS_CUSUPINFO WHERE CS_TYPE='6'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListCompany.DataValueField = "CS_CODE";
            DropDownListCompany.DataTextField = "CS_NAME";
            DropDownListCompany.DataSource = dt;
            DropDownListCompany.DataBind();
        }

      
        //员工
        protected void GetStaff()
        {
            string sql = "SELECT DISTINCT ST_CODE,ST_NAME FROM TBDS_STAFFINFO WHERE ST_DEPID='07' AND ST_STATE='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            //发料
            DropDownListSender.DataValueField = "ST_CODE";
            DropDownListSender.DataTextField = "ST_NAME";
            DropDownListSender.DataSource = dt;
            DropDownListSender.DataBind();

            //业务员
            DropDownListClerk.DataValueField = "ST_CODE";
            DropDownListClerk.DataTextField = "ST_NAME";
            DropDownListClerk.DataSource = dt;
            DropDownListClerk.DataBind();
        }

        protected void initial()
        {
            string code = Request.QueryString["ID"].ToString();
            string flag = Request.QueryString["FLAG"].ToString();
            if (flag == "PUSHBLUE")
            {
                string sql = "SELECT UniqueID='',SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,Length AS Length," +
                    "Width AS Width,LotNumber AS LotNumber,PlanMode AS PlanMode,PTC AS PTC,OrderCode AS OrderID," +
                    "WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,LocationCode AS PositionCode," +
                    "Location AS Position,Unit AS Unit,cast(Number as float) AS DN,cast(Number as float) AS RN,cast(SupportNumber as float) AS DQN,cast(SupportNumber as float) AS RQN," +
                    "Note AS Comment,Cost='0',UnitCost='' FROM View_SM_Storage WHERE State='OUTXS" + Session["UserID"].ToString() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                sql = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='OUTXS" + Session["UserID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);

                LabelCode.Text = generateCode();

                sql = "INSERT INTO TBWS_OUTCODE (OP_CODE,OP_BILLTYPE) VALUES ('" + LabelCode.Text + "','3')";

                DBCallCommon.ExeSqlText(sql);

                LabelState.Text = "0";
                InputColour.Value = "0";
                TextBoxDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ClosingAccountDate(TextBoxDate.Text.Trim());

                TextBoxAbstract.Text = "";
                LabelDoc.Text = Session["UserName"].ToString();
                LabelDocCode.Text = Session["UserID"].ToString();
                LabelVerifier.Text = "";
                LabelVerifierCode.Text = "";
                LabelApproveDate.Text = "";

                Append.Visible = true;
                Split.Visible = true;
                Delete.Visible = true;
                Save.Visible = true;
                Verify.Visible = true;
                DeleteBill.Visible = true;
                AntiVerify.Visible = false;
                PushRed.Visible = false;
                PushRed.Visible = false;
                SumPrint.Visible = false;   
            }


            //推红

            if (flag == "PUSHRED")
            {

                //总表
                string sql = "SELECT TOP 1 Code AS Code,DepCode AS DepCode,CompanyCode AS CompanyCode," +
                    "ProcessType AS ProcessType,SellWay AS SellWay,Date AS Date,CollectionDate AS CollectionDate," +
                    "DepCode AS DepCode,SenderCode AS SenderCode,ClerkCode AS ClerkCode,KeeperCode AS KeeperCode," +
                    "ManagerCode AS ManagerCode,DocCode AS DocCode,Doc AS Doc,VerifierCode AS VerifierCode," +
                    "Verifier AS Verifier,VerifierDate AS VerifierDate,ROB AS Colour,TotalState AS State," +
                    "PrintTime AS PrintTime,TotalNote AS Abstract FROM View_SM_OUTXS WHERE Code='" + code + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    LabelCode.Text = generateRedCode();
                    LabelState.Text = "0";
                    InputColour.Value = "1";
                    TextBoxDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    if (TextBoxDate.Text.Trim() == string.Empty)
                    {
                        TextBoxDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    }

                    ClosingAccountDate(TextBoxDate.Text.Trim());


                    TextBoxPrintTime.Text = dr["PrintTime"].ToString();
                    TextBoxAbstract.Text = dr["Abstract"].ToString();
                    try { DropDownListCompany.Items.FindByValue(dr["CompanyCode"].ToString()).Selected = true; }
                    catch { }
                    try { DropDownListProcessType.Items.FindByValue(dr["ProcessType"].ToString()).Selected = true; }
                    catch { }
                    try { DropDownListSellWay.Items.FindByValue(dr["SellWay"].ToString()).Selected = true; }
                    catch { }
                    TextBoxCollectionDate.Text = dr["CollectionDate"].ToString();

                    //try { DropDownListDep.Items.FindByValue(dr["DepCode"].ToString()).Selected = true; }
                    //catch { }

                    //发料
                    try { DropDownListSender.Items.FindByValue(dr["SenderCode"].ToString()).Selected = true; }
                    catch { }

                    //业务员
                    try { DropDownListClerk.Items.FindByValue(dr["ClerkCode"].ToString()).Selected = true; }
                    catch { }

                    //try { DropDownListKeeper.Items.FindByValue(dr["KeeperCode"].ToString()).Selected = true; }
                    //catch { }
                    //try { DropDownListManager.Items.FindByValue(dr["ManagerCode"].ToString()).Selected = true; }
                    //catch { }
                    //制单
                    LabelDoc.Text = Session["UserName"].ToString();
                    LabelDocCode.Text = Session["UserID"].ToString();
                    //审核人
                    LabelVerifier.Text = "";
                    LabelVerifierCode.Text = "";
                    //审核时间
                    LabelApproveDate.Text = "";
                }
                dr.Close();

                //从表

                sql = "SELECT UniqueCode AS UniqueID,SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,Length AS Length,Width AS Width," +
                    "LotNumber AS LotNumber,Unit AS Unit,-cast(RealNumber as float) AS DN,-cast(RealNumber as float) AS RN," +
                    "-cast(RealSupportNumber as float) AS DQN,-cast(RealSupportNumber as float) AS RQN,cast(UnitPrice as float) AS UnitPrice," +
                    " cast(Amount as float) AS Amount, cast(UnitCost as float) AS UnitCost,-cast(Cost as float) AS Cost,WarehouseCode AS WarehouseCode," +
                    "Warehouse AS Warehouse,LocationCode AS PositionCode,Location AS Position," +
                    "PlanMode AS PlanMode,PTC AS PTC,OrderCode AS OrderID," +
                    "DetailNote AS Comment FROM View_SM_OUTXS WHERE DetailState='REDXS" + Session["UserID"].ToString() + "' AND Code='" + code + "'";
                DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = tb;
                GridView1.DataBind();

                /*================销售单价与金额===================*/

                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxDJ")).Text = tb.Rows[i]["UnitCost"].ToString();

                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxJE")).Text = tb.Rows[i]["Cost"].ToString();
                    
                    // xsje += Convert.ToDouble(tb.Rows[i]["Cost"]);

                    //if (GridView1.Rows[i].RowType == DataControlRowType.Footer)
                    //{
                    //    ((Label)GridView1.Rows[i].FindControl("LabelTotalXSJE")).Text = Math.Round(xsje, 2).ToString();
                    //}
                }

                /*================销售单价与金额===================*/


                sql = "UPDATE TBWS_OUTDETAIL SET OP_STATE='' WHERE OP_STATE='REDXS" + Session["UserID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
                ImageRed.Visible = true;
                //DropDownListDep.Enabled = false;
                DropDownListSender.Enabled = false;
                DropDownListClerk.Enabled = false;
                Append.Visible = false;
                Split.Visible = false;
                Delete.Visible = false;
                Save.Visible = true;
                Verify.Visible = true;
                DeleteBill.Visible = true;
                AntiVerify.Visible = false;
                PushRed.Visible = false;
                PushRed.Visible = false;
                SumPrint.Visible = false;   
            }


            if (flag == "OPEN")
            {
                string sql = "SELECT TOP 1 id as TrueCode, Code AS Code,DepCode AS DepCode,CompanyCode AS CompanyCode," +
                    "ProcessType AS ProcessType,SellWay AS SellWay,Date AS Date,CollectionDate AS CollectionDate," +
                    "DepCode AS DepCode,SenderCode AS SenderCode,ClerkCode AS ClerkCode,KeeperCode AS KeeperCode," +
                    "ManagerCode AS ManagerCode,DocCode AS DocCode,Doc AS Doc,VerifierCode," +
                    "Verifier AS Verifier,left(VerifierDate,10) AS VerifierDate,ROB AS Colour,TotalState AS State," +
                    "TotalNote AS Abstract,PrintTime AS PrintTime FROM View_SM_OUTXS WHERE Code='" + code + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    LabelCode.Text = dr["Code"].ToString();//单号
                    LabelTrueCode.Text = dr["TrueCode"].ToString();//单号


                    LabelState.Text = dr["State"].ToString();//审核状态
                    InputColour.Value = dr["Colour"].ToString();//红蓝字

                    TextBoxDate.Text = dr["Date"].ToString();

                    ClosingAccountDate(TextBoxDate.Text.Trim());

                    TextBoxPrintTime.Text = dr["PrintTime"].ToString();//打印时间
                    TextBoxAbstract.Text = dr["Abstract"].ToString();//摘要
                    try { DropDownListCompany.Items.FindByValue(dr["CompanyCode"].ToString()).Selected = true; }
                    catch { }
                    try { DropDownListProcessType.Items.FindByValue(dr["ProcessType"].ToString()).Selected = true; }
                    catch { }
                    try { DropDownListSellWay.Items.FindByValue(dr["SellWay"].ToString()).Selected = true; }
                    catch { }
                    TextBoxCollectionDate.Text = dr["CollectionDate"].ToString();//收款时间

                    //try { DropDownListDep.Items.FindByValue(dr["DepCode"].ToString()).Selected = true; }
                    //catch { }

                   //发料员
                    try { DropDownListSender.Items.FindByValue(dr["SenderCode"].ToString()).Selected = true; }
                    catch { }

                    //业务员
                    try { DropDownListClerk.Items.FindByValue(dr["ClerkCode"].ToString()).Selected = true; }
                    catch { }

                    //try { DropDownListKeeper.Items.FindByValue(dr["KeeperCode"].ToString()).Selected = true; }
                    //catch { }
                    //try { DropDownListManager.Items.FindByValue(dr["ManagerCode"].ToString()).Selected = true; }
                    //catch { }
                    //制单人
                    LabelDoc.Text = dr["Doc"].ToString();
                    LabelDocCode.Text = dr["DocCode"].ToString();
                    //审核人
                    LabelVerifier.Text = dr["Verifier"].ToString();
                    LabelVerifierCode.Text = dr["VerifierCode"].ToString();
                    //审核时间
                    LabelApproveDate.Text = dr["VerifierDate"].ToString();
                }
                dr.Close();
                sql = "SELECT UniqueCode AS UniqueID,SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,Length AS Length,Width AS Width," +
                    "LotNumber AS LotNumber,Unit AS Unit,cast(DueNumber as float) AS DN,cast(RealNumber as float) AS RN,cast(DueSupportNumber as float) AS DQN,cast(RealSupportNumber as float) AS RQN," +
                    " cast(UnitPrice as float) AS UnitPrice,cast(Amount as float) AS Amount,cast(UnitCost as float) AS UnitCost,cast(Cost as float) AS Cost,WarehouseCode AS WarehouseCode," +
                    "Warehouse AS Warehouse,LocationCode AS PositionCode,Location AS Position," +
                    "PlanMode AS PlanMode,PTC AS PTC,OrderCode AS OrderID,DetailNote AS Comment " +
                    "FROM View_SM_OUTXS WHERE Code='" + code + "'";

                DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);

              

                GridView1.DataSource = tb;
                GridView1.DataBind();

                /*================销售单价与金额===================*/

                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxDJ")).Text = tb.Rows[i]["UnitCost"].ToString();

                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxJE")).Text = tb.Rows[i]["Cost"].ToString();

                    // xsje += Convert.ToDouble(tb.Rows[i]["Cost"]);

                    //if (GridView1.Rows[i+1]!=null)
                    //{
                    //    if (GridView1.Rows[i+1].RowType == DataControlRowType.Footer)
                    //    {
                    //        ((Label)GridView1.Rows[i+1].FindControl("LabelTotalXSJE")).Text = Math.Round(xsje, 2).ToString();
                    //    }
                    //}
                }

                /*================销售单价与金额===================*/


                if (InputColour.Value == "1")
                {
                    ImageRed.Visible = true;
                }
                if (LabelState.Text == "1")
                {
                    Append.Visible = true;
                    Split.Visible = true;
                    Delete.Visible = true;
                    Save.Visible = true;
                    Verify.Visible = true;
                    DeleteBill.Visible = true;
                    AntiVerify.Visible = false;
                    PushRed.Visible = false;
                }
                if (LabelState.Text == "2")
                {
                    if (Session["UserID"].ToString() == LabelVerifierCode.Text.Trim())
                    {
                        ImageVerify.Visible = true;
                        Append.Visible = false;
                        Split.Visible = false;
                        Delete.Visible = false;
                        Save.Visible = false;
                        Verify.Visible = false;
                        DeleteBill.Visible = false;
                        AntiVerify.Visible = true;
                        PushRed.Visible = true;
                    }
                    else
                    {
                        ImageVerify.Visible = true;
                        Append.Visible = false;
                        Split.Visible = false;
                        Delete.Visible = false;
                        Save.Visible = false;
                        Verify.Visible = false;
                        AntiVerify.Visible = false;
                        DeleteBill.Visible = false;
                        PushRed.Visible = false;
                    }
                }
            }

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                trn += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DN"));
                tdn += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "RN"));
                tdqn += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "DQN"));
                trqn += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "RQN"));
                xsje += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Cost"));

                //xsje += Convert.ToDouble((e.Row.FindControl("TextBoxJE") as TextBox).Text.Trim());
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.Cells[13].FindControl("LabelTotalDN")).Text = Math.Round(trn, 4).ToString();
                ((Label)e.Row.Cells[14].FindControl("LabelTotalRN")).Text = Math.Round(tdn, 4).ToString();
                ((Label)e.Row.Cells[15].FindControl("LabelTotalDQN")).Text = tdqn.ToString();
                ((Label)e.Row.Cells[16].FindControl("LabelTotalRQN")).Text = trqn.ToString();
                ((Label)e.Row.Cells[23].FindControl("LabelTotalXSJE")).Text = Math.Round(xsje, 4).ToString();

                //((Label)e.Row.Cells[23].FindControl("LabelTotalXSJE")).Text =Math.Round(xsje,2).ToString();
               
            }
        }

        protected string generateCode()
        {
            string sql = "SELECT MAX(OP_CODE) AS MaxCode FROM TBWS_OUTCODE WHERE LEN(OP_CODE)=10 AND OP_BILLTYPE='3'";
            string code = "";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                if (dr["MaxCode"] != DBNull.Value)
                {
                    code = dr["MaxCode"].ToString();
                }
            }
            dr.Close();
            if (code == "")
            {
                return "XS00000001";
            }
            else
            {
                int tempnum = Convert.ToInt32((code.Substring(2, 8)));
                tempnum++;
                code = "XS" + tempnum.ToString().PadLeft(8, '0');
                return code;
            }
        }

        protected string generateRedCode()
        {
            string Code = Request.QueryString["ID"].ToString();
            string sql = "SELECT MAX(OP_CODE) AS MaxCode FROM TBWS_OUT WHERE CHARINDEX('" + Code + "R" + "',OP_CODE)=1";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                if (dr["MaxCode"] != DBNull.Value)
                {
                    Code = dr["MaxCode"].ToString();
                }
            }
            if (Code.Contains("R") == false)
            {
                return Code + "R1";
            }
            else
            {
                int tempnum = Convert.ToInt32((Code.Substring(Code.IndexOf('R', 0) + 1, Code.Length - Code.IndexOf('R', 0) - 1)));
                tempnum++;
                Code = Code.Substring(0, Code.IndexOf('R', 0) + 1) + tempnum.ToString();
                return Code;
            }
        }

        protected DataTable getDataFromGridView()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("UniqueID", System.Type.GetType("System.String"));
            tb.Columns.Add("SQCODE", System.Type.GetType("System.String"));
            tb.Columns.Add("MaterialCode", System.Type.GetType("System.String"));
            tb.Columns.Add("MaterialName", System.Type.GetType("System.String"));
            tb.Columns.Add("Attribute", System.Type.GetType("System.String"));
            tb.Columns.Add("GB", System.Type.GetType("System.String"));
            tb.Columns.Add("LotNumber", System.Type.GetType("System.String"));
            tb.Columns.Add("MaterialStandard", System.Type.GetType("System.String"));
            tb.Columns.Add("Fixed", System.Type.GetType("System.String"));
            tb.Columns.Add("Length", System.Type.GetType("System.String"));
            tb.Columns.Add("Width", System.Type.GetType("System.String"));
            tb.Columns.Add("Unit", System.Type.GetType("System.String"));
            tb.Columns.Add("DN", System.Type.GetType("System.String"));
            tb.Columns.Add("RN", System.Type.GetType("System.String"));
            tb.Columns.Add("DQN", System.Type.GetType("System.String"));
            tb.Columns.Add("RQN", System.Type.GetType("System.String"));
            tb.Columns.Add("PlanMode", System.Type.GetType("System.String"));
            tb.Columns.Add("PTC", System.Type.GetType("System.String"));
            tb.Columns.Add("OrderID", System.Type.GetType("System.String"));
            tb.Columns.Add("Comment", System.Type.GetType("System.String"));
            tb.Columns.Add("Warehouse", System.Type.GetType("System.String"));
            tb.Columns.Add("WarehouseCode", System.Type.GetType("System.String"));
            tb.Columns.Add("Position", System.Type.GetType("System.String"));
            tb.Columns.Add("PositionCode", System.Type.GetType("System.String"));
            tb.Columns.Add("Cost", System.Type.GetType("System.String"));
            tb.Columns.Add("UnitCost", System.Type.GetType("System.String"));
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow GRow = GridView1.Rows[i];
                DataRow row = tb.NewRow();
                row["UniqueID"] = ((Label)GRow.FindControl("LabeLUniqueID")).Text;
                row["SQCODE"] = ((Label)GRow.FindControl("LabeLSQCODE")).Text;
                row["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                row["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
                row["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;
                row["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
                row["LotNumber"] = ((Label)GRow.FindControl("LabelLotNumber")).Text;
                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                row["DN"] = ((Label)GRow.FindControl("LabelDN")).Text;
                row["RN"] = ((TextBox)GRow.FindControl("TextBoxRN")).Text;
                row["DQN"] = ((Label)GRow.FindControl("LabelDQN")).Text;
                row["RQN"] = ((TextBox)GRow.FindControl("TextBoxRQN")).Text;
                row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                row["OrderID"] = ((Label)GRow.FindControl("LabelOrderID")).Text;
                row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                row["Position"] = ((Label)GRow.FindControl("LabelPosition")).Text;
                row["PositionCode"] = ((Label)GRow.FindControl("LabelPositionCode")).Text;
                row["Cost"] = ((TextBox)GRow.FindControl("TextBoxJE")).Text;
                row["UnitCost"] = ((TextBox)GRow.FindControl("TextBoxDJ")).Text;
                tb.Rows.Add(row);
            }
            return tb;
        }

        protected void Append_Click(object sender, EventArgs e)
        {
            DataTable dt = getDataFromGridView();
            string sql = "SELECT UniqueID='',SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed," +
                    "CAST(Length AS VARCHAR(50)) AS Length,CAST(Width  AS VARCHAR(50)) AS Width," +
                    "LotNumber AS LotNumber,PlanMode AS PlanMode,PTC AS PTC,OrderCode AS OrderID," +
                    "WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,LocationCode AS PositionCode," +
                    "Location AS Position,Unit AS Unit," +
                    "CAST(CAST(Number AS FLOAT) AS VARCHAR(50)) AS DN,CAST(CAST(Number AS FLOAT) AS VARCHAR(50)) AS RN," +
                    "CAST(CAST(SupportNumber AS FLOAT) AS VARCHAR(50)) AS DQN,CAST(SupportNumber AS VARCHAR(50)) AS RQN," +
                    "Note AS Comment,UnitCost='',Cost='0' " +
                    "FROM View_SM_Storage WHERE State='APPENDOUTXS" + Session["UserID"].ToString() + "'";
            dt.Merge(DBCallCommon.GetDTUsingSqlText(sql));
            GridView1.DataSource = dt;
            GridView1.DataBind();
            sql = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='APPENDOUTXS" + Session["UserID"].ToString() + "'";
            DBCallCommon.ExeSqlText(sql);
            LabelMessage.Text = "追加成功";
            CheckSQCODE();//重复计划跟踪号提醒
        }

        protected void CheckSQCODE() //有重复的计划跟踪号有颜色区分
        {

            for (int i = 0; i < (GridView1.Rows.Count - 1); i++)
            {
                GridViewRow gvrow0 = GridView1.Rows[i];
                string sqcode = ((Label)gvrow0.FindControl("LabelPTC")).Text;
                string materialcode = ((Label)gvrow0.FindControl("LabelMaterialCode")).Text; //物料代码
                if ((materialcode.Substring(0, 5) != "01.07") && (materialcode.Substring(0, 5) != "01.14"))
                {
                    for (int j = i + 1; j < GridView1.Rows.Count; j++)
                    {
                        GridViewRow gvrow1 = GridView1.Rows[j];
                        string nextsqcode = ((Label)gvrow1.FindControl("LabelPTC")).Text;
                        if (sqcode == nextsqcode)
                        {
                            //string script = @"alert('有重复计划跟踪号" + sqcode + "，请检查数据!');";
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                            //ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('有重复计划跟踪号'"+sqcode+"'，请检查数据!')</script>");
                            // ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>Checksqcode(" + i + "," + j + ");</script>");
                            gvrow0.BackColor = System.Drawing.Color.FromName("#e45f5f");
                            gvrow1.BackColor = System.Drawing.Color.FromName("#e45f5f");

                        }
                    }
                }
            }

        }
        protected void ButtonSplitOK_Click(object sender, EventArgs e)
        {
            //获得拆分数量
            int count = 1;
            try
            {
                count = Convert.ToInt32(TextBoxSplitLineNum.Text);
                TextBoxSplitLineNum.Text = "2";
                if (count <= 1)
                {
                    LabelMessage.Text = "请输入合适的行数！";
                    return;
                }
            }
            catch
            {
                LabelMessage.Text = "请输入正确的行数！";
            }
            //获取拆分模式
            string mode = RadioButtonListSplitMode.SelectedValue.ToString();
            string mode2 = RadioButtonListSplitMode2.SelectedValue.ToString();

            DataTable tb = new DataTable();
            tb.Columns.Add("UniqueID", System.Type.GetType("System.String"));
            tb.Columns.Add("SQCODE", System.Type.GetType("System.String"));
            tb.Columns.Add("MaterialCode", System.Type.GetType("System.String"));
            tb.Columns.Add("MaterialName", System.Type.GetType("System.String"));
            tb.Columns.Add("Attribute", System.Type.GetType("System.String"));
            tb.Columns.Add("GB", System.Type.GetType("System.String"));
            tb.Columns.Add("LotNumber", System.Type.GetType("System.String"));
            tb.Columns.Add("MaterialStandard", System.Type.GetType("System.String"));
            tb.Columns.Add("Fixed", System.Type.GetType("System.String"));
            tb.Columns.Add("Length", System.Type.GetType("System.String"));
            tb.Columns.Add("Width", System.Type.GetType("System.String"));
            tb.Columns.Add("Unit", System.Type.GetType("System.String"));
            tb.Columns.Add("DN", System.Type.GetType("System.String"));
            tb.Columns.Add("RN", System.Type.GetType("System.String"));
            tb.Columns.Add("DQN", System.Type.GetType("System.String"));
            tb.Columns.Add("RQN", System.Type.GetType("System.String"));
            tb.Columns.Add("PlanMode", System.Type.GetType("System.String"));
            tb.Columns.Add("PTC", System.Type.GetType("System.String"));
            tb.Columns.Add("OrderID", System.Type.GetType("System.String"));
            tb.Columns.Add("Comment", System.Type.GetType("System.String"));
            tb.Columns.Add("Warehouse", System.Type.GetType("System.String"));
            tb.Columns.Add("WarehouseCode", System.Type.GetType("System.String"));
            tb.Columns.Add("Position", System.Type.GetType("System.String"));
            tb.Columns.Add("PositionCode", System.Type.GetType("System.String"));
            tb.Columns.Add("Cost", System.Type.GetType("System.String"));
            tb.Columns.Add("UnitCost", System.Type.GetType("System.String"));

            //插入记录模式
            if (mode2 == "0")
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow GRow = GridView1.Rows[i];
                    if (((CheckBox)GRow.FindControl("CheckBox1")).Checked == true)
                    {
                        //数据均分模式
                        if (mode == "0")
                        {
                            float dn = 0;
                            float rn = 0;
                            Int32 dqn = 0;
                            Int32 rqn = 0;
                            double je = 0;
                            try
                            {
                                dn = Convert.ToSingle(((Label)GRow.FindControl("LabelDN")).Text);
                                rn = Convert.ToSingle(((TextBox)GRow.FindControl("TextBoxRN")).Text);
                                dqn = Convert.ToInt32(((Label)GRow.FindControl("LabelDQN")).Text);
                                rqn = Convert.ToInt32(((TextBox)GRow.FindControl("TextBoxRQN")).Text);
                                je = Convert.ToDouble(((TextBox)GRow.FindControl("TextBoxJE")).Text);
                                
                            }
                            catch
                            {
                                LabelMessage.Text = "请正确填写实收数量！";
                                return;
                            }
                            for (int j = 0; j < count; j++)
                            {
                                DataRow row = tb.NewRow();
                                row["UniqueID"] = ((Label)GRow.FindControl("LabelUniqueID")).Text;
                                row["SQCODE"] = ((Label)GRow.FindControl("LabelSQCODE")).Text;
                                row["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                                row["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
                                row["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;
                                row["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
                                row["LotNumber"] = ((Label)GRow.FindControl("LabelLotNumber")).Text;
                                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                                row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                                row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                                row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                                row["DN"] = (dn / count).ToString();
                                row["RN"] = (rn / count).ToString();
                                row["DQN"] = Convert.ToInt32((dqn / count)).ToString();
                                row["RQN"] = Convert.ToInt32((rqn / count)).ToString();
                                row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                                row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                                row["OrderID"] = ((Label)GRow.FindControl("LabelOrderID")).Text;
                                row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                                row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                                row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                                row["Position"] = ((Label)GRow.FindControl("LabelPosition")).Text;
                                row["PositionCode"] = ((Label)GRow.FindControl("LabelPositionCode")).Text;
                                row["Cost"] = Math.Round((je / count), 4).ToString();
                                row["UnitCost"] = ((TextBox)GRow.FindControl("TextBoxDJ")).Text; 
                                tb.Rows.Add(row);
                            }
                        }
                        //数据复制模式
                        if (mode == "1")
                        {
                            for (int j = 0; j < count; j++)
                            {
                                DataRow row = tb.NewRow();
                                row["UniqueID"] = ((Label)GRow.FindControl("LabelUniqueID")).Text;
                                row["SQCODE"] = ((Label)GRow.FindControl("LabelSQCODE")).Text;
                                row["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                                row["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
                                row["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;
                                row["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
                                row["LotNumber"] = ((Label)GRow.FindControl("LabelLotNumber")).Text;
                                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                                row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                                row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                                row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                                row["DN"] = ((Label)GRow.FindControl("LabelDN")).Text;
                                row["RN"] = ((TextBox)GRow.FindControl("TextBoxRN")).Text;
                                row["DQN"] = ((Label)GRow.FindControl("LabelDQN")).Text;
                                row["RQN"] = ((TextBox)GRow.FindControl("TextBoxRQN")).Text;
                                row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                                row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                                row["OrderID"] = ((Label)GRow.FindControl("LabelOrderID")).Text;
                                row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                                row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                                row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                                row["Position"] = ((Label)GRow.FindControl("LabelPosition")).Text;
                                row["PositionCode"] = ((Label)GRow.FindControl("LabelPositionCode")).Text;
                                row["Cost"] = ((TextBox)GRow.FindControl("TextBoxJE")).Text;
                                row["UnitCost"] = ((TextBox)GRow.FindControl("TextBoxDJ")).Text; 
                                tb.Rows.Add(row);
                            }
                        }
                    }
                    else
                    {
                        DataRow row = tb.NewRow();
                        row["UniqueID"] = ((Label)GRow.FindControl("LabelUniqueID")).Text;
                        row["SQCODE"] = ((Label)GRow.FindControl("LabelSQCODE")).Text;
                        row["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                        row["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
                        row["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;
                        row["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
                        row["LotNumber"] = ((Label)GRow.FindControl("LabelLotNumber")).Text;
                        row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                        row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                        row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                        row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                        row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                        row["DN"] = ((Label)GRow.FindControl("LabelDN")).Text;
                        row["RN"] = ((TextBox)GRow.FindControl("TextBoxRN")).Text;
                        row["DQN"] = ((Label)GRow.FindControl("LabelDQN")).Text;
                        row["RQN"] = ((TextBox)GRow.FindControl("TextBoxRQN")).Text;
                        row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                        row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                        row["OrderID"] = ((Label)GRow.FindControl("LabelOrderID")).Text;
                        row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                        row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                        row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                        row["Position"] = ((Label)GRow.FindControl("LabelPosition")).Text;
                        row["PositionCode"] = ((Label)GRow.FindControl("LabelPositionCode")).Text;
                        row["Cost"] = ((TextBox)GRow.FindControl("TextBoxJE")).Text;
                        row["UnitCost"] = ((TextBox)GRow.FindControl("TextBoxDJ")).Text; 
                        tb.Rows.Add(row);
                    }
                }
            }
            //记录追加模式
            if (mode2 == "1")
            {
                tb = getDataFromGridView();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow GRow = GridView1.Rows[i];
                    if (((CheckBox)GRow.FindControl("CheckBox1")).Checked == true)
                    {
                        //数据均分模式
                        if (mode == "0")
                        {
                            float dn = 0;
                            float rn = 0;
                            Int32 dqn = 0;
                            Int32 rqn = 0;
                            double je = 0;
                            try
                            {
                                dn = Convert.ToSingle(((Label)GRow.FindControl("LabelDN")).Text);
                                rn = Convert.ToSingle(((TextBox)GRow.FindControl("TextBoxRN")).Text);
                                dqn = Convert.ToInt32(((Label)GRow.FindControl("LabelDQN")).Text);
                                rqn = Convert.ToInt32(((TextBox)GRow.FindControl("TextBoxRQN")).Text);
                                je = Convert.ToDouble(((TextBox)GRow.FindControl("TextBoxJE")).Text);

                            }
                            catch
                            {
                                LabelMessage.Text = "请正确填写实收数量！";
                                return;
                            }
                            for (int j = 0; j < count - 1; j++)
                            {
                                DataRow row = tb.NewRow();
                                row["UniqueID"] = ((Label)GRow.FindControl("LabelUniqueID")).Text;
                                row["SQCODE"] = ((Label)GRow.FindControl("LabelSQCODE")).Text;
                                row["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                                row["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
                                row["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;
                                row["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
                                row["LotNumber"] = ((Label)GRow.FindControl("LabelLotNumber")).Text;
                                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                                row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                                row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                                row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                                row["DN"] = (dn / count).ToString();
                                tb.Rows[i]["DN"] = (dn / count).ToString();
                                row["RN"] = (rn / count).ToString();
                                tb.Rows[i]["RN"] = (rn / count).ToString();
                                row["DQN"] = Convert.ToInt32((dqn / count)).ToString();
                                tb.Rows[i]["DQN"] = Convert.ToInt32((dqn / count)).ToString();
                                row["RQN"] = Convert.ToInt32((rqn / count)).ToString();
                                tb.Rows[i]["RQN"] = Convert.ToInt32((rqn / count)).ToString();
                                row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                                row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                                row["OrderID"] = ((Label)GRow.FindControl("LabelOrderID")).Text;
                                row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                                row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                                row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                                row["Position"] = ((Label)GRow.FindControl("LabelPosition")).Text;
                                row["PositionCode"] = ((Label)GRow.FindControl("LabelPositionCode")).Text;
                                row["Cost"] = Math.Round((je / count), 4).ToString();
                                row["UnitCost"] = ((TextBox)GRow.FindControl("TextBoxDJ")).Text; 
                                tb.Rows.Add(row);
                            }
                        }
                        //数据复制模式
                        if (mode == "1")
                        {
                            for (int j = 0; j < count - 1; j++)
                            {
                                DataRow row = tb.NewRow();
                                row["UniqueID"] = ((Label)GRow.FindControl("LabelUniqueID")).Text;
                                row["SQCODE"] = ((Label)GRow.FindControl("LabelSQCODE")).Text;
                                row["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                                row["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
                                row["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;
                                row["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
                                row["LotNumber"] = ((Label)GRow.FindControl("LabelLotNumber")).Text;
                                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                                row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                                row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                                row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                                row["DN"] = ((Label)GRow.FindControl("LabelDN")).Text;
                                row["RN"] = ((TextBox)GRow.FindControl("TextBoxRN")).Text;
                                row["DQN"] = ((Label)GRow.FindControl("LabelDQN")).Text;
                                row["RQN"] = ((TextBox)GRow.FindControl("TextBoxRQN")).Text;
                                row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                                row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                                row["OrderID"] = ((Label)GRow.FindControl("LabelOrderID")).Text;
                                row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                                row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                                row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                                row["Position"] = ((Label)GRow.FindControl("LabelPosition")).Text;
                                row["PositionCode"] = ((Label)GRow.FindControl("LabelPositionCode")).Text;
                                row["Cost"] = ((TextBox)GRow.FindControl("TextBoxJE")).Text;
                                row["UnitCost"] = ((TextBox)GRow.FindControl("TextBoxDJ")).Text; 
                                tb.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            GridView1.DataSource = tb;
            GridView1.DataBind();
            LabelMessage.Text = "拆分成功！";
        }

        protected void ButtonSplitCancel_Click(object sender, EventArgs e)
        {
            RadioButtonListSplitMode.Items[0].Selected = true;
            RadioButtonListSplitMode2.Items[0].Selected = true;
            TextBoxSplitLineNum.Text = "2";
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            int count = 0;
            DataTable tb;
            tb = getDataFromGridView();
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                CheckBox chk = ((CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1"));
                if (chk.Checked == true)
                {
                    tb.Rows.RemoveAt(i - count);
                    count++;
                }
            }
            GridView1.DataSource = tb;
            GridView1.DataBind();
        }

        //保存操作
        protected void Save_Click(object sender, EventArgs e)
        {
            if (isLock() == false)
            {
                string script = @"alert('系统正在结账,请稍后...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                LabelMessage.Text = "系统正在结账,请稍后...";
            }
            else
            {
                //此处是保存操作
                List<string> sqllist = new List<string>();
                string sql = "";

                string Code = LabelCode.Text;
                string sqlstate = "select OP_STATE from TBWS_OUT where OP_CODE='" + Code + "'";
                if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows.Count > 0)
                {
                    if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows[0]["OP_STATE"].ToString() == "2")
                    {

                        string script = @"alert('单据已审核，单据不能再审核！');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                        return;
                    }
                }

                string State = LabelState.Text;//状态
                string Colour = InputColour.Value;//红蓝字
                string Date = TextBoxDate.Text;//日期
                string PrintTime = TextBoxPrintTime.Text;//打印时间
                string Abstract = TextBoxAbstract.Text;//摘要
                string CompanyCode = DropDownListCompany.SelectedValue;//公司编号
                string ProcessType = DropDownListProcessType.SelectedValue;//销售类型
                string SellWay = DropDownListSellWay.SelectedValue;//销售方式
                string CollectionDate = TextBoxCollectionDate.Text;//收款日期

                string DepCode = "";
                //string DepCode = DropDownListDep.SelectedValue;
                string SenderCode = DropDownListSender.SelectedValue;
                string ClerkCode = DropDownListClerk.SelectedValue;
                string KeeperCode = "";
                string ManagerCode = "";
                //string KeeperCode = DropDownListKeeper.SelectedValue;
                //string ManagerCode = DropDownListManager.SelectedValue;

                string DocCode = LabelDocCode.Text;
                string VerifierCode = LabelVerifierCode.Text;
                string ApproveDate = LabelApproveDate.Text;


                //sql = "DELETE FROM TBWS_OUT WHERE OP_CODE='" + Code + "'";
                //sqllist.Add(sql);
                //sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_PRINTTIME,OP_DATE,OP_COMPANY," +
                //    "OP_PROCESSTYPE,OP_SELLWAY,OP_COLLECTIONDATE," +
                //    "OP_DEP,OP_SENDER,OP_CLERK,OP_KEEPER,OP_MANAGER," +
                //    "OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
                //    "OP_STATE,OP_NOTE,OP_BILLTYPE) VALUES('" + Code + "','" + PrintTime + "','" + Date + "','" + CompanyCode + "','" +
                //    ProcessType + "','" + SellWay + "','" + CollectionDate + "','" + DepCode + "','" + SenderCode + "','" +
                //    ClerkCode + "','" + KeeperCode + "','" + ManagerCode + "','" + 
                //    DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','" +
                //    Colour + "','1','" + Abstract + "','3')";
                //sqllist.Add(sql);

                sql = "select count(*) from TBWS_OUT where OP_CODE='" + Code + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                if (dt.Rows[0][0].ToString() == "0")
                {
                    sql = "exec ResetSeed @tablename=TBWS_OUT";

                    sqllist.Add(sql);

                    sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_PRINTTIME,OP_DATE,OP_COMPANY," +
                        "OP_PROCESSTYPE,OP_SELLWAY,OP_COLLECTIONDATE," +
                        "OP_DEP,OP_SENDER,OP_CLERK,OP_KEEPER,OP_MANAGER," +
                        "OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
                        "OP_STATE,OP_NOTE,OP_BILLTYPE) VALUES('" + Code + "','" + PrintTime + "','" + Date + "','" + CompanyCode + "','" +
                        ProcessType + "','" + SellWay + "','" + CollectionDate + "','" + DepCode + "','" + SenderCode + "','" +
                        ClerkCode + "','" + KeeperCode + "','" + ManagerCode + "','" +
                        DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','" +
                        Colour + "','1','" + Abstract + "','3')";

                    sqllist.Add(sql);

                }
                else
                {
                    sql = "update TBWS_OUT set OP_DATE='" + Date + "', OP_COMPANY='" + CompanyCode + "',OP_PROCESSTYPE='" + ProcessType + "',OP_SELLWAY='" + SellWay + "',OP_COLLECTIONDATE='" + CollectionDate + "',";
                    sql += "OP_DEP='" + DepCode + "',OP_SENDER='" + SenderCode + "',OP_CLERK='" + ClerkCode + "',OP_KEEPER='" + KeeperCode + "',OP_MANAGER='" + ManagerCode + "',OP_DOC='" + DocCode + "',OP_VERIFIER='" + VerifierCode + "',OP_VERIFYDATE='" + ApproveDate + "',OP_HSFLAG='0',";
                    sql += "OP_ROB='" + Colour + "',OP_STATE='1',OP_NOTE='" + Abstract + "',OP_BILLTYPE='3' where OP_CODE='" + Code + "'";

                    sqllist.Add(sql);
                }



                sql = "DELETE FROM TBWS_OUTDETAIL WHERE OP_CODE='" + Code + "'";
                sqllist.Add(sql);

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    string UniqueID = Code + (i + 1).ToString().PadLeft(3, '0');
                    string SQCODE = ((Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;
                    string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text;
                    string LotNumber = ((Label)this.GridView1.Rows[i].FindControl("LabelLotNumber")).Text;
                    string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;
                    float Length = 0;
                    try { Length = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLength")).Text); }
                    catch { Length = 0; }
                    float Width = 0;
                    try { Width = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxWidth")).Text); }
                    catch { Width = 0; }
                    float DN = 0;
                    try
                    {
                        float temp = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelDN")).Text);
                        if ((InputColour.Value == "1") && (temp > 0))
                        {
                            DN = -temp;
                        }
                        else if ((InputColour.Value == "0") && (temp < 0))
                        {
                            DN = -temp;
                        }
                        else
                        { DN = temp; }
                    }
                    catch { DN = 0; }
                    float RN = 0;
                    try
                    {
                        float temp = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxRN")).Text);
                        if ((InputColour.Value == "1") && (temp > 0))
                        {
                            RN = -temp;
                        }
                        else if ((InputColour.Value == "0") && (temp < 0))
                        {
                            RN = -temp;
                        }
                        else
                        { RN = temp; }
                    }
                    catch { RN = 0; }
                    Int32 DQN = 0;
                    try
                    {
                        Int32 temp = Convert.ToInt32(((Label)this.GridView1.Rows[i].FindControl("LabelDQN")).Text);
                        if ((InputColour.Value == "1") && (temp > 0))
                        {
                            DQN = -temp;
                        }
                        else if ((InputColour.Value == "0") && (temp < 0))
                        {
                            DQN = -temp;
                        }
                        else
                        { DQN = temp; }
                    }
                    catch { DQN = 0; }
                    Int32 RQN = 0;
                    try
                    {
                        Int32 temp = Convert.ToInt32(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxRQN")).Text);
                        if ((InputColour.Value == "1") && (temp > 0))
                        {
                            RQN = -temp;
                        }
                        else if ((InputColour.Value == "0") && (temp < 0))
                        {
                            RQN = -temp;
                        }
                        else
                        { RQN = temp; }
                    }
                    catch { RQN = 0; }
                    string PlanMode = ((Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text;//计划模式
                    string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;//计划跟踪号
                    string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;//订单ID
                    string Note = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;//备注
                    string WarehouseOutCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;//仓库
                    string PositionCode = ((Label)this.GridView1.Rows[i].FindControl("LabelPositionCode")).Text;//仓位

                    double dj = Convert.ToDouble(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxDJ")).Text.Trim() == string.Empty ? "0" : ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxDJ")).Text.Trim());//单价
                    double je = Convert.ToDouble(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxJE")).Text.Trim() == string.Empty ? "0" : ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxJE")).Text.Trim());//金额



                    //----出红的时候单价是没有写进去的

                    sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                        "OP_FIXED,OP_DUELENGTH,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                        "OP_UPRICE,OP_AMOUNT,OP_UNITCOST,OP_COST,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                        "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE) VALUES('" + Code + "','" + UniqueID + "','" + SQCODE + "','" +
                        MaterialCode + "','" +
                        Fixed + "','" + Length + "','" + Length + "','" +
                        Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','0','0','" + dj + "','" + je + "','" +
                        WarehouseOutCode + "','" + PositionCode + "','" + PlanMode + "','" +
                        PTC + "','" + OrderID + "','" + Note + "','')";
                    sqllist.Add(sql);
                }

                DBCallCommon.ExecuteTrans(sqllist);
                sqllist.Clear();
                LabelState.Text = "1";
                LabelMessage.Text = "保存成功";

            }
        }

        //审核操作如何与库存信息关联
        protected void Verify_Click(object sender, EventArgs e)
        {
            if (isLock() == false)
            {
                string script = @"alert('系统正在结账,请稍后...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                LabelMessage.Text = "系统正在结账,请稍后...";
                return;
            }
            else
            {


                bool HasError = false;
                int ErrorType = 0;

                //此处是审核操作
                List<string> sqllist = new List<string>();
                string sql = "";

                string Code = LabelCode.Text;
                string State = LabelState.Text;
                string Colour = InputColour.Value;
                string Date = TextBoxDate.Text;
                string PrintTime = TextBoxPrintTime.Text;
                string Abstract = TextBoxAbstract.Text;
                string CompanyCode = DropDownListCompany.SelectedValue;
                string ProcessType = DropDownListProcessType.SelectedValue;
                string SellWay = DropDownListSellWay.SelectedValue;
                string CollectionDate = TextBoxCollectionDate.Text;
                string DepCode = "";
                string KeeperCode = "";
                string ManagerCode = "";
                //string DepCode = DropDownListDep.SelectedValue;
                string SenderCode = DropDownListSender.SelectedValue;
                string ClerkCode = DropDownListClerk.SelectedValue;
                //string KeeperCode = DropDownListKeeper.SelectedValue;
                //string ManagerCode = DropDownListManager.SelectedValue;
                string DocCode = LabelDocCode.Text;
                string VerifierCode = Session["UserID"].ToString();
                if ((SenderCode == DocCode) && (DocCode == VerifierCode)) //制单人，审核人，发料人不能相同
                {
                    LabelMessage.Text = "制单人，审核人，发料人不能相同！";

                    string script = @"alert('制单人，审核人，发料人不能相同！');";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    return;
                }
                ClosingAccountDate(DateTime.Now.ToString("yyyy-MM-dd"));//获取系统封账时间

                string ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                //if(LabelApproveDate.Text.Trim())

                //上一次的审核日期LabelApproveDate.Text.Trim()，可能有值，也可能为空，有值表示反审，无值表示第一次审核

                //如果核算之后，是不能反审的
                if (LabelClosingAccount.Text == "NoTime")
                {
                    //未封账
                    ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    //封账
                    sql = "SELECT COUNT(*) FROM TBFM_HSTOTAL WHERE HS_YEAR=substring(convert(varchar(50),getdate(),112),1,4) AND HS_MONTH=substring(convert(varchar(50),getdate(),112),5,2) AND HS_STATE='3'";
                    DataTable dtcount = DBCallCommon.GetDTUsingSqlText(sql);
                    if (Convert.ToInt32(dtcount.Rows[0][0]) == 0)
                    {
                        //无反核算
                        //if(LabelApproveDate.Text.Trim())
                        ApproveDate = getNextMonth() + " 07:59:59";
                        Date = getNextMonth();

                    }
                    else
                    {
                        //有反核算
                        //得看上次审核时间，是不是本月的
                        if (LabelApproveDate.Text.Trim().Length > 8)
                        {
                            //多次审核
                            if (Convert.ToInt32(LabelApproveDate.Text.Trim().Substring(0, 4)) == System.DateTime.Now.Year && Convert.ToInt32(LabelApproveDate.Text.Trim().Substring(5, 2)) == System.DateTime.Now.Month)
                            {
                                //是本月时间
                                if (CheckBoxDate.Checked)
                                {
                                    ApproveDate = getNextMonth() + " 07:59:59";
                                    Date = getNextMonth();
                                }
                                else
                                {
                                    ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                }
                            }
                            else
                            {
                                //不是本月
                                ApproveDate = getNextMonth() + " 07:59:59";
                                Date = getNextMonth();
                            }
                        }
                        else
                        {
                            //第一次审核
                            ApproveDate = getNextMonth() + " 07:59:59";
                            Date = getNextMonth();
                        }
                    }

                }


                //sql = "DELETE FROM TBWS_OUT WHERE OP_CODE='" + Code + "'";
                //sqllist.Add(sql);

                ////重置标识符
                //sql = "exec ResetSeed @tablename=TBWS_OUT";

                //sqllist.Add(sql);

                //sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_PRINTTIME,OP_DATE,OP_COMPANY," +
                //    "OP_PROCESSTYPE,OP_SELLWAY,OP_COLLECTIONDATE," +
                //    "OP_DEP,OP_SENDER,OP_CLERK,OP_KEEPER,OP_MANAGER," +
                //    "OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
                //    "OP_STATE,OP_NOTE,OP_BILLTYPE,OP_RealTime) VALUES('" + Code + "','" + PrintTime + "','" + ApproveDate.Substring(0,10) + "','" + CompanyCode + "','" +
                //    ProcessType + "','" + SellWay + "','" + CollectionDate + "','" + DepCode + "','" + SenderCode + "','" +
                //    ClerkCode + "','" + KeeperCode + "','" + ManagerCode + "','" +
                //    DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','" +
                //    Colour + "','1','" + Abstract + "','3',convert(varchar(50),getdate(),120))";
                //sqllist.Add(sql);

                sql = "select count(*) from TBWS_OUT where OP_CODE='" + Code + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                if (dt.Rows[0][0].ToString() == "0")
                {
                    sql = "exec ResetSeed @tablename=TBWS_OUT";

                    sqllist.Add(sql);

                    sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_PRINTTIME,OP_DATE,OP_COMPANY," +
                        "OP_PROCESSTYPE,OP_SELLWAY,OP_COLLECTIONDATE," +
                        "OP_DEP,OP_SENDER,OP_CLERK,OP_KEEPER,OP_MANAGER," +
                        "OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
                        "OP_STATE,OP_NOTE,OP_BILLTYPE,OP_RealTime) VALUES('" + Code + "','" + PrintTime + "','" + Date + "','" + CompanyCode + "','" +
                        ProcessType + "','" + SellWay + "','" + CollectionDate + "','" + DepCode + "','" + SenderCode + "','" +
                        ClerkCode + "','" + KeeperCode + "','" + ManagerCode + "','" +
                        DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','" +
                        Colour + "','1','" + Abstract + "','3',convert(varchar(50),getdate(),120))";

                    sqllist.Add(sql);

                }
                else
                {
                    sql = "update TBWS_OUT set OP_DATE='" + Date + "', OP_COMPANY='" + CompanyCode + "',OP_PROCESSTYPE='" + ProcessType + "',OP_SELLWAY='" + SellWay + "',OP_COLLECTIONDATE='" + CollectionDate + "',";
                    sql += "OP_DEP='" + DepCode + "',OP_SENDER='" + SenderCode + "',OP_CLERK='" + ClerkCode + "',OP_KEEPER='" + KeeperCode + "',OP_MANAGER='" + ManagerCode + "',OP_DOC='" + DocCode + "',OP_VERIFIER='" + VerifierCode + "',OP_VERIFYDATE='" + ApproveDate + "',OP_HSFLAG='0',";
                    sql += "OP_ROB='" + Colour + "',OP_STATE='1',OP_NOTE='" + Abstract + "',OP_BILLTYPE='3',OP_RealTime=convert(varchar(50),getdate(),120)  where OP_CODE='" + Code + "'";

                    sqllist.Add(sql);

                }


                sql = "DELETE FROM TBWS_OUTDETAIL WHERE OP_CODE='" + Code + "'";
                sqllist.Add(sql);
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    string UniqueID = Code + (i + 1).ToString().PadLeft(3, '0');
                    string SQCODE = ((Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;
                    string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text;
                    string LotNumber = ((Label)this.GridView1.Rows[i].FindControl("LabelLotNumber")).Text;
                    string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;
                    float Length = 0;
                    try { Length = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLength")).Text); }
                    catch { Length = 0; }
                    float Width = 0;
                    try { Width = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxWidth")).Text); }
                    catch { Width = 0; }
                    float DN = 0;
                    try
                    {
                        float temp = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelDN")).Text);
                        if ((InputColour.Value == "1") && (temp > 0))
                        {
                            DN = -temp;
                        }
                        else if ((InputColour.Value == "0") && (temp < 0))
                        {
                            DN = -temp;
                        }
                        else
                        { DN = temp; }
                    }
                    catch { DN = 0; }
                    float RN = 0;
                    try
                    {
                        float temp = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxRN")).Text);
                        if ((InputColour.Value == "1") && (temp > 0))
                        {
                            RN = -temp;
                        }
                        else if ((InputColour.Value == "0") && (temp < 0))
                        {
                            RN = -temp;
                        }
                        else
                        { RN = temp; }
                    }
                    catch { RN = 0; }

                    if (RN == 0)
                    {
                        HasError = true;
                        ErrorType = 1;
                        break;
                    }

                    Int32 DQN = 0;
                    try
                    {
                        Int32 temp = Convert.ToInt32(((Label)this.GridView1.Rows[i].FindControl("LabelDQN")).Text);
                        if ((InputColour.Value == "1") && (temp > 0))
                        {
                            DQN = -temp;
                        }
                        else if ((InputColour.Value == "0") && (temp < 0))
                        {
                            DQN = -temp;
                        }
                        else
                        { DQN = temp; }
                    }
                    catch { DQN = 0; }
                    Int32 RQN = 0;
                    try
                    {
                        Int32 temp = Convert.ToInt32(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxRQN")).Text);
                        if ((InputColour.Value == "1") && (temp > 0))
                        {
                            RQN = -temp;
                        }
                        else if ((InputColour.Value == "0") && (temp < 0))
                        {
                            RQN = -temp;
                        }
                        else
                        { RQN = temp; }
                    }
                    catch { RQN = 0; }
                    string PlanMode = ((Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text;
                    string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;
                    string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                    string Note = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;
                    string WarehouseOutCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;
                    string PositionCode = ((Label)this.GridView1.Rows[i].FindControl("LabelPositionCode")).Text;
                    double dj = Convert.ToDouble(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxDJ")).Text.Trim() == string.Empty ? "0" : ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxDJ")).Text.Trim());//单价
                    double je = Convert.ToDouble(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxJE")).Text.Trim() == string.Empty ? "0" : ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxJE")).Text.Trim());//金额


                    sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                        "OP_FIXED,OP_DUELENGTH,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                        "OP_UPRICE,OP_AMOUNT,OP_UNITCOST,OP_COST,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                        "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE) VALUES('" + Code + "','" + UniqueID + "','" + SQCODE + "','" +
                        MaterialCode + "','" +
                        Fixed + "','" + Length + "','" + Length + "','" +
                        Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','0','0','" + dj + "','" + je + "','" +
                        WarehouseOutCode + "','" + PositionCode + "','" + PlanMode + "','" +
                        PTC + "','" + OrderID + "','" + Note + "','')";
                    sqllist.Add(sql);
                }

                if (HasError)
                {
                    sqllist.Clear();

                    if (ErrorType == 1)
                    {
                        LabelMessage.Text = "出库条目为0，单据不能审核！";

                        //string script = @"alert('出库条目为0，单据不能审核！');";

                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                }
                else
                {

                    DBCallCommon.ExecuteTrans(sqllist);
                    sqllist.Clear();

                    //存储过程只是改了库存的数量

                    sql = DBCallCommon.GetStringValue("connectionStrings");
                    SqlConnection con = new SqlConnection(sql);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("StorageOut", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@OutCode", SqlDbType.VarChar, 50);			//增加参数
                    cmd.Parameters["@OutCode"].Value = Code;							//为参数初始化

                    cmd.Parameters.Add("@OutDate", SqlDbType.VarChar, 50);			//出库时间
                    cmd.Parameters["@OutDate"].Value = ApproveDate;

                    cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add("@ErrorRow", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 0)
                    {
                        Response.Redirect("SM_WarehouseOUT_XS.aspx?FLAG=OPEN&&ID=" + Code);
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
                    {
                        LabelMessage.Text = "审核未通过：部分库存物料不存在！";
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
                    {
                        LabelMessage.Text = "审核未通过：部分库存物料数量小于出库数量！";
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -1)
                    {
                        LabelMessage.Text = "审核未通过：该出库单已经被审核！";
                    }
                }
            }
        }

        protected bool isLock()
        {
            string nowyear = DateTime.Now.Year.ToString();
            string nowmonth = DateTime.Now.Month.ToString();
            string sqllock = "select HS_KEY from TBWS_LOCKTABLE where HS_YEAR='" + nowyear + "' AND HS_MONTH='" + nowmonth + "'";
            SqlDataReader drlock = DBCallCommon.GetDRUsingSqlText(sqllock, 5);
            bool flag = true;
            try
            {
                

                if (drlock.Read())
                {
                    if (drlock["HS_KEY"].ToString() == "1")
                    {
                        flag = false;
                    }
                }
                drlock.Close();
                return flag;
            }
            catch (Exception)
            {
                drlock.Close();
                return false;
            }
        }

        //删除当前单据，删除单据前判断是否满足删除单据条件，删除单据后关闭当前页面。
        protected void DeleteBill_Click(object sender, EventArgs e)
        {
            if (isLock() == false)
            {
                string script = @"alert('系统正在结账,请稍后...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                LabelMessage.Text = "系统正在结账,请稍后...";
            }
            else
            {
                if (LabelState.Text == "1")
                {
                    List<string> sqllist = new List<string>();
                    string sql = "DELETE FROM TBWS_OUT WHERE OP_CODE='" + LabelCode.Text + "'";
                    sqllist.Add(sql);
                    sql = "DELETE FROM TBWS_OUTDETAIL WHERE OP_CODE='" + LabelCode.Text + "'";
                    sqllist.Add(sql);
                    DBCallCommon.ExecuteTrans(sqllist);
                    Response.Write("<script type='text/javascript' language='javascript' >window.opener.location = window.opener.location.href;window.close();</script>");
                    //ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>window.close();</script>");
                    LabelMessage.Text = "Deleted!";
                }
                else if (LabelState.Text == "0")
                {
                    LabelMessage.Text = "当前出库单尚未保存！";
                    return;
                }
                else
                {
                    LabelMessage.Text = "当前出库单已审核无法删除！";
                    return;
                }

            }
        }

        //反审当前当前出库单，调用出库单反审存储过程，反审前先判断是否满足反审条件。
        protected void AntiVerify_Click(object sender, EventArgs e)
        {
            if (isLock() == false)
            {
                string script = @"alert('系统正在结账,请稍后...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                LabelMessage.Text = "系统正在结账,请稍后...";
            }
            else
            {
                if (LabelState.Text != "2")
                {
                    LabelMessage.Text = "当前出库单未审核无法反审！";
                    return;
                }
                string sql = DBCallCommon.GetStringValue("connectionStrings");
                SqlConnection con = new SqlConnection(sql);
                con.Open();
                SqlCommand cmd = new SqlCommand("AntiVerifyOut", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@OutCode", SqlDbType.VarChar, 50);				//增加参数入库单号@InCode
                cmd.Parameters["@OutCode"].Value = LabelCode.Text;						                //为参数初始化
                cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;			//增加返回值参数@retVal
                cmd.ExecuteNonQuery();
                con.Close();
                if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 0)
                {
                    Response.Redirect("SM_WarehouseOUT_XS.aspx?FLAG=OPEN&&ID=" + LabelCode.Text);
                }
                if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -1)
                {
                    LabelMessage.Text = "审核未通过：未审核的出库单无法反审！";
                }
                if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
                {
                    LabelMessage.Text = "审核未通过：入库物料发生后续操作！";
                }
                if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
                {
                    LabelMessage.Text = "审核未通过：部分入库物料发生后续操作！";
                }
            }
        }

        //下推红字出库单。先判断当前条件下是否能够下推红字出库单。
        protected void PushRed_Click(object sender, EventArgs e)
        {
            if (LabelState.Text != "2")
            {
                LabelMessage.Text = "当前出库单未审核无法下推红字出库单！";
                return;
            }
            if (InputColour.Value == "1")
            {
                LabelMessage.Text = "红字出库单无法下推红字出库单！";
                return;
            }
            List<string> sqllist = new List<string>();

            string sql = "UPDATE TBWS_OUTDETAIL SET OP_STATE='' WHERE OP_STATE='REDXS" + Session["UserID"].ToString() + "'";

            sqllist.Add(sql);

            string uniqueid = "";

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked == true)
                {
                    uniqueid = ((Label)GridView1.Rows[i].FindControl("LabelUniqueID")).Text;
                    sql = "UPDATE TBWS_OUTDETAIL SET OP_STATE='REDXS" + Session["UserID"].ToString() + "' WHERE OP_CODE='" + LabelCode.Text +
                        "' AND OP_UNIQUEID='" + uniqueid + "'";
                    
                    sqllist.Add(sql);
                }
            }
            if (sqllist.Count < 2)
            {
                LabelMessage.Text = "请选择要下推红字出库单的条目！";
                return;
            }
            DBCallCommon.ExecuteTrans(sqllist);
            Response.Redirect("SM_WarehouseOUT_XS.aspx?FLAG=PUSHRED&&ID=" + LabelCode.Text);
        }

        protected void Related_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked == true)
                {
                    string ptc = ((Label)GridView1.Rows[i].FindControl("LabelPTC")).Text;
                    Response.Redirect("SM_Warehouse_RelatedDocument.aspx?PTC=" + ptc);
                    return;
                }
            }
            LabelMessage.Text = "请选择一条要查询的记录！";
        }

        protected void tostorge_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Warehouse_Query.aspx?FLAG=PUSHXSOUT");
        }

        #region 升降序
        protected void GridView1_Sorting(object sender,GridViewSortEventArgs e)
        {
            if (this.SortDire == "ASC")
            {
                this.SortDire = "DESC";
            }
            else
            {
                this.SortDire = "ASC";
            }
            DataTable dt = getDataFromGridView();
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dv.Sort = e.SortExpression + " " + this.SortDire;
                this.GridView1.DataSource = dv;
                this.GridView1.DataBind();

            }

        }
        public string SortDire 
        {
            get
            {
                if (ViewState["sortDire"] == null)
                {
                    //默认是升序
                    ViewState["sortDire"] = "ASC";
                }
                return ViewState["sortDire"].ToString();
            }
            set
            {
                ViewState["sortDire"] = value;
            }
        }

        //protected void GetDataTable()
        //{
        //    DataTable tb = new DataTable();
        //    tb.Columns.Add("UniqueID", System.Type.GetType("System.String"));
        //    tb.Columns.Add("SQCODE", System.Type.GetType("System.String"));
        //    tb.Columns.Add("MaterialCode", System.Type.GetType("System.String"));
        //    tb.Columns.Add("MaterialName", System.Type.GetType("System.String"));
        //    tb.Columns.Add("Attribute", System.Type.GetType("System.String"));
        //    tb.Columns.Add("GB", System.Type.GetType("System.String"));
        //    tb.Columns.Add("LotNumber", System.Type.GetType("System.String"));
        //    tb.Columns.Add("MaterialStandard", System.Type.GetType("System.String"));
        //    tb.Columns.Add("Fixed", System.Type.GetType("System.String"));
        //    tb.Columns.Add("Length", System.Type.GetType("System.String"));
        //    tb.Columns.Add("Width", System.Type.GetType("System.String"));
        //    tb.Columns.Add("Unit", System.Type.GetType("System.String"));
        //    tb.Columns.Add("DN", System.Type.GetType("System.String"));
        //    tb.Columns.Add("RN", System.Type.GetType("System.String"));
        //    tb.Columns.Add("DQN", System.Type.GetType("System.String"));
        //    tb.Columns.Add("RQN", System.Type.GetType("System.String"));
        //    tb.Columns.Add("PlanMode", System.Type.GetType("System.String"));
        //    tb.Columns.Add("PTC", System.Type.GetType("System.String"));
        //    tb.Columns.Add("OrderID", System.Type.GetType("System.String"));
        //    tb.Columns.Add("Comment", System.Type.GetType("System.String"));
        //    tb.Columns.Add("Warehouse", System.Type.GetType("System.String"));
        //    tb.Columns.Add("WarehouseCode", System.Type.GetType("System.String"));
        //    tb.Columns.Add("Position", System.Type.GetType("System.String"));
        //    tb.Columns.Add("PositionCode", System.Type.GetType("System.String"));

        //    foreach (GridViewRow GROW in GridView1.Rows)
        //    {
        //        DataRow row = tb.NewRow();
        //        row["UniqueID"] = ((Label)GRow.FindControl("LabelUniqueID")).Text;
        //        row["SQCODE"] = ((Label)GRow.FindControl("LabelSQCODE")).Text;
        //        row["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
        //        row["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
        //        row["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;
        //        row["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
        //        row["LotNumber"] = ((Label)GRow.FindControl("LabelLotNumber")).Text;
        //        row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
        //        row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
        //        row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
        //        row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
        //        row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
        //        row["DN"] = ((Label)GRow.FindControl("LabelDN")).Text;
        //        row["RN"] = ((TextBox)GRow.FindControl("TextBoxRN")).Text;
        //        row["DQN"] = ((Label)GRow.FindControl("LabelDQN")).Text;
        //        row["RQN"] = ((TextBox)GRow.FindControl("TextBoxRQN")).Text;
        //        row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
        //        row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
        //        row["OrderID"] = ((Label)GRow.FindControl("LabelOrderID")).Text;
        //        row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
        //        row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
        //        row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
        //        row["Position"] = ((Label)GRow.FindControl("LabelPosition")).Text;
        //        row["PositionCode"] = ((Label)GRow.FindControl("LabelPositionCode")).Text;
        //        tb.Rows.Add(row);
        //    }
        //    return tb;
        //}
        #endregion

        #region  隐藏列
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Text.Trim() != "标准件")
            {
                HideControlCell(sender);
            }
            else
            {
                HideStrControlCell(sender);
            }
        }

        private void HideStrControlCell(object sender)
        {
            List<string> lt = new List<string>();
            lt.Add("批号");
            lt.Add("是否定尺");
            lt.Add("库存长(mm)");
            lt.Add("长");
            lt.Add("宽");
            lt.Add("即时库存张(支)数");
            lt.Add("实发张(支)数");


            if ((sender as CheckBox).Checked)
            {
                CheckBox7.Enabled = false;
                CheckBox7.Checked = true;
                CheckBox8.Enabled = false;
                CheckBox8.Checked = true;
                //CheckBox9.Enabled = false;
                //CheckBox9.Checked = true;
                CheckBox10.Enabled = false;
                CheckBox10.Checked = true;
                CheckBox11.Enabled = false;
                CheckBox11.Checked = true;
                CheckBox12.Enabled = false;
                CheckBox12.Checked = true;
                CheckBox13.Enabled = false;
                CheckBox13.Checked = true;
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
                CheckBox7.Enabled = true;
                CheckBox7.Checked = false;
                CheckBox8.Enabled = true;
                CheckBox8.Checked = false;
                //CheckBox9.Enabled = true;
                //CheckBox9.Checked = false;
                CheckBox10.Enabled = true;
                CheckBox10.Checked = false;
                CheckBox11.Enabled = true;
                CheckBox11.Checked = false;
                CheckBox12.Enabled = true;
                CheckBox12.Checked = false;
                CheckBox13.Enabled = true;
                CheckBox13.Checked = false;

                for (int i = 0; i < GridView1.Columns.Count; i++)
                {
                    foreach (string st in lt)
                    {
                        if (GridView1.Columns[i].HeaderText.Trim() == st)
                        {
                            GridView1.Columns[i].HeaderStyle.CssClass = "visible";
                            GridView1.Columns[i].ItemStyle.CssClass = "visible";
                            GridView1.Columns[i].FooterStyle.CssClass = "visible";
                        }
                    }

                }
            }

        }


        private void HideControlCell(object sender)
        {
            for (int i = 0; i < GridView1.Columns.Count; i++)
            {
                if (GridView1.Columns[i].HeaderText.Trim() == (sender as CheckBox).Text.Trim())
                {
                    if ((sender as CheckBox).Checked)
                    {

                        GridView1.Columns[i].HeaderStyle.CssClass = "hidden";
                        GridView1.Columns[i].ItemStyle.CssClass = "hidden";
                        GridView1.Columns[i].FooterStyle.CssClass = "hidden";
                    }
                    else
                    {

                        GridView1.Columns[i].HeaderStyle.CssClass = "visible";
                        GridView1.Columns[i].ItemStyle.CssClass = "visible";
                        GridView1.Columns[i].FooterStyle.CssClass = "visible";
                    }
                }
            }
        }
        #endregion

    }
}
