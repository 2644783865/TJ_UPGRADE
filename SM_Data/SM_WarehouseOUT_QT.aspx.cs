using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_WarehouseOUT_QT : System.Web.UI.Page
    {
        double tdn = 0;
        double trn = 0;
        Int32 tdqn = 0;
        Int32 trqn = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                GetDep();

                GetBZ();

                GetStaff();

                initial();

            }

        }

        //获取系统封账时间
        private void ClosingAccountDate(string ZDDate)
        {
            string NowDate = ZDDate;
            //查找本期系统关账时间
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


        //领料部门

        protected void GetDep()
        {
            //DEP_SFJY是否禁用，DEP_CY是否为领料部门

            string sql = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO WHERE DEP_CY='1' and DEP_SFJY='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListDep.DataValueField = "DEP_CODE";
            DropDownListDep.DataTextField = "DEP_NAME";
            DropDownListDep.DataSource = dt;
            DropDownListDep.DataBind();
            ListItem item = new ListItem("--请选择--", "0");
            DropDownListDep.Items.Insert(0, item);
        }

        //制作班组

        protected void GetBZ()
        {
            // AND (DEP_BZYN='1' OR DEP_BZYN='2') 1=制作班组；2=供应商

            string sql = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO WHERE DEP_FATHERID='04' and DEP_CY='1' and DEP_SFJY='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListBZ.DataValueField = "DEP_CODE";
            DropDownListBZ.DataTextField = "DEP_NAME";
            DropDownListBZ.DataSource = dt;
            DropDownListBZ.DataBind();
            ListItem item = new ListItem("--请选择--", "0");
            DropDownListBZ.Items.Insert(0, item);
        }


        protected void GetStaff()
        {
            string sql = "SELECT DISTINCT ST_ID,ST_NAME FROM TBDS_STAFFINFO WHERE ST_DEPID='05'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListSender.DataValueField = "ST_ID";
            DropDownListSender.DataTextField = "ST_NAME";
            DropDownListSender.DataSource = dt;
            DropDownListSender.DataBind();
        }

        protected void initial()
        {
            string code = Request.QueryString["ID"].ToString();
            string flag = Request.QueryString["FLAG"].ToString();
            if (flag == "PUSHBLUE")
            {
                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", "bindunbeforunload();", true);

                string sql = "SELECT UniqueID='',SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,Length AS DueLength,Length AS Length," +
                    "Width AS Width,LotNumber AS LotNumber,PlanMode AS PlanMode,PTC AS PTC,OrderCode AS OrderID," +
                    "WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,LocationCode AS PositionCode," +
                    "Location AS Position,Unit AS Unit,cast(Number as float) AS DN,cast(Number as float) AS RN,cast(Number as float) AS DIFNUM,SupportNumber AS DQN,SupportNumber AS RQN," +
                    "Note AS Comment,CGMODE AS BSH FROM View_SM_Storage WHERE State='OUTQT" + Session["UserID"].ToString() + "' order by MaterialCode DESC";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = dt;
                GridView1.DataBind();

                sql = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='OUTQT" + Session["UserID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);

                LabelCode.Text = generateCode();

               // TextBoxPageNum.Text = "1";

                //this.Page.Title = "重机领料单(" + LabelCode.Text + ")";

                sql = "INSERT INTO TBWS_OUTCODE (OP_CODE,OP_BILLTYPE) VALUES ('" + LabelCode.Text + "','6')";

                DBCallCommon.ExeSqlText(sql);

                LabelState.Text = "0";
                InputColour.Value = "0";
                TextBoxDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                ClosingAccountDate(TextBoxDate.Text.Trim());

                //TextBoxComment.Text = "";

                LabelDoc.Text = Session["UserName"].ToString();
                LabelDocCode.Text = Session["UserID"].ToString();
                LabelVerifier.Text = "";
                LabelVerifierCode.Text = "";
                LabelApproveDate.Text = "";

                //发料人默认为制单人

                //DropDownListSender
                //try { DropDownListSender.Items.FindByValue(Session["UserID"].ToString()).Selected = true; }
                //catch { }


                Append.Visible = true;
                Split.Visible = true;
                Delete.Visible = true;
                Save.Visible = true;
                Verify.Visible = true;
                DeleteBill.Visible = false;
                AntiVerify.Visible = false;
                PushRed.Visible = false;
                BtnBackStorage.Visible = false;
                Print.Visible = false;
                SumPrint.Visible = false;
                Related.Visible = false;

            }
            if (flag == "PUSHRED")
            {

                //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", "bindunbeforunload();", true);

                string sql = "SELECT TOP 1 OutCode AS Code,DepCode AS LLDepCode,Dep AS LLDep,Date AS Date," +
               "TSAID AS SCZH,SenderCode AS SenderCode," +
               "Sender AS Sender,DocCode AS DocumentCode," +
               "Doc AS Document,VerifierCode AS VerifierCode," +
               "Verifier AS Verifier,LEFT(ApprovedDate,10) AS ApproveDate,ROB AS Colour,TotalState AS State," +
               "TotalNote AS Comment,OP_ZXMC,OP_NOTE1 FROM View_SM_OUT WHERE OutCode='" + code + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    LabelCode.Text = generateRedCode();

                    //this.Page.Title = "重机领料单(" + LabelCode.Text + ")";

                    //TextBoxPageNum.Text = "1";
                    LabelState.Text = "0";
                    InputColour.Value = "1";
                    TextBoxDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    ClosingAccountDate(TextBoxDate.Text.Trim());
                    //领料部门
                    try { DropDownListDep.Items.FindByValue(dr["LLDepCode"].ToString()).Selected = true; }
                    catch { }

                    //制作班组
                    try { DropDownListBZ.Items.FindByValue(dr["Comment"].ToString()).Selected = true; }
                    catch { }

                    TextBoxSCZH.Text = dr["SCZH"].ToString();
                    LabelSCZH.Text = dr["SCZH"].ToString();
                    TextBoxNOTE1.Text = dr["OP_NOTE1"].ToString();

                    //子项名称
                    //try { DropDownListZXMC.Items.FindByValue(dr["OP_ZXMC"].ToString()).Selected = true; }
                    //catch { }
                    //TextBoxComment.Text = "";

                    LabelDoc.Text = Session["UserName"].ToString();
                    LabelDocCode.Text = Session["UserID"].ToString();
                    try { DropDownListSender.Items.FindByValue(dr["SenderCode"].ToString()).Selected = true; }
                    catch { }
                    LabelVerifier.Text = "";
                    LabelVerifierCode.Text = "";
                    LabelApproveDate.Text = "";

                }
                dr.Close();
                //红单长等于原单长
                sql = "SELECT UniqueCode AS UniqueID,SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,DueLength,Length AS Length,Width AS Width," +
                    "LotNumber AS LotNumber,Unit AS Unit,-cast(RealNumber as float) AS DN,-cast(RealNumber as float) AS RN,cast((RealNumber-RealNumber) as float) AS DIFNUM," +
                    "-cast(RealSupportNumber as float) AS DQN,-cast(RealSupportNumber as float) AS RQN,cast(UnitPrice as float) AS UnitPrice," +
                    "cast(Amount as float) AS Amount,WarehouseCode AS WarehouseCode," +
                    "Warehouse AS Warehouse,LocationCode AS PositionCode,Location AS Position," +
                    "PlanMode AS PlanMode,PTC AS PTC,OrderCode AS OrderID," +
                    "DetailNote AS Comment,OP_BSH as BSH FROM View_SM_OUT WHERE DetailState='RED" + Session["UserID"].ToString() + "' AND OutCode='" + code + "'";
                DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = tb;
                GridView1.DataBind();
                sql = "UPDATE TBWS_OUTDETAIL SET OP_STATE='' WHERE OP_STATE='RED" + Session["UserID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
                ImageRed.Visible = true;
                //DropDownListDep.Enabled = false;
                //DropDownListSender.Enabled = false;
                Append.Visible = false;
                Split.Visible = true;
                Delete.Visible = true;
                Save.Visible = true;
                Verify.Visible = true;
                DeleteBill.Visible = false;
                AntiVerify.Visible = false;
                PushRed.Visible = false;
                BtnBackStorage.Visible = false;
                Print.Visible = false;
                SumPrint.Visible = false;
                Related.Visible = false;

                AdjustLenWid.Visible = true;//调整长宽，推红可用

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxPosition")).Enabled = true;
                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxLotNumber")).Enabled = true;
                }

            }
            if (flag == "OPEN")
            {
                string sql = "SELECT Top 1 id as TrueCode,OutCode AS Code,DepCode AS LLDepCode,Dep AS LLDep,Date AS Date," +
                "TSAID as SCZH,SenderCode AS SenderCode," +
                "Sender AS Sender,DocCode AS DocumentCode," +
                "Doc AS Document,VerifierCode AS VerifierCode," +
                "Verifier AS Verifier,LEFT(ApprovedDate,10) AS ApproveDate,ROB AS Colour,TotalState AS State,BillType," +
                "TotalNote AS Comment,OP_ZXMC,OP_NOTE1 FROM View_SM_OUT WHERE OutCode='" + code + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {

                    LabelCode.Text = dr["Code"].ToString();

                    LabelTrueCode.Text = dr["TrueCode"].ToString();

                    this.Page.Title = "其他出库(" + LabelTrueCode.Text + ")"; //新单编号

                    LabelState.Text = dr["State"].ToString();

                    InputColour.Value = dr["Colour"].ToString();

                    TextBoxDate.Text = dr["Date"].ToString();

                    if (TextBoxDate.Text.Trim() == string.Empty)
                    {
                        TextBoxDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    }

                    ClosingAccountDate(TextBoxDate.Text.Trim());

                    try { DropDownListDep.Items.FindByValue(dr["LLDepCode"].ToString()).Selected = true; }
                    catch { }

                    //try { DropDownListSCZH.Items.FindByValue(dr["SCZH"].ToString()).Selected = true; }
                    //catch { } 

                    TextBoxSCZH.Text = dr["SCZH"].ToString();

                    LabelSCZH.Text = dr["SCZH"].ToString();

                    TextBoxNOTE1.Text = dr["OP_NOTE1"].ToString();

                    // TextBoxPageNum.Text = dr["OP_PAGENUM"].ToString();

                    //TextBoxComment.Text = dr["Comment"].ToString();
                    //制作班组
                    try { DropDownListBZ.Items.FindByValue(dr["Comment"].ToString()).Selected = true; }
                    catch { }

                    //子项名称
                    //try { DropDownListZXMC.Items.FindByValue(dr["OP_ZXMC"].ToString()).Selected = true; }
                    //catch { }

                    LabelDoc.Text = dr["Document"].ToString();
                    LabelDocCode.Text = dr["DocumentCode"].ToString();
                    try { DropDownListSender.Items.FindByValue(dr["SenderCode"].ToString()).Selected = true; }
                    catch { }
                    LabelVerifier.Text = dr["Verifier"].ToString();
                    LabelVerifierCode.Text = dr["VerifierCode"].ToString();
                    LabelApproveDate.Text = dr["ApproveDate"].ToString();
                    LabelBillType.Text = dr["BillType"].ToString();
                   
                }
                dr.Close();

                sql = "SELECT UniqueCode AS UniqueID,SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,DueLength,Length AS Length,Width AS Width," +
                    "LotNumber AS LotNumber,Unit AS Unit,cast(DueNumber as float) AS DN,cast(RealNumber as float) AS RN,cast((case when TotalState='2' then (DueNumber-RealNumber) else DueNumber end) as float) AS DIFNUM,cast(DueSupportNumber as float) AS DQN,cast(RealSupportNumber as float) AS RQN," +
                    "cast(UnitPrice as float) AS UnitPrice,cast(Amount as float) AS Amount,WarehouseCode AS WarehouseCode," +
                    "Warehouse AS Warehouse,LocationCode AS PositionCode,Location AS Position," +
                    "PlanMode AS PlanMode,PTC AS PTC,OrderCode AS OrderID,DetailNote AS Comment,OP_BSH as BSH " +
                    "FROM View_SM_OUT WHERE OutCode='" + code + "' order by UniqueCode ";
                DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = tb;
                GridView1.DataBind();

                if (LabelState.Text == "1")
                {
                    //未审核
                    Append.Visible = true;
                    Split.Visible = true;
                    Delete.Visible = true;
                    Save.Visible = true;
                    Verify.Visible = true;
                    DeleteBill.Visible = true;
                    AntiVerify.Visible = false;
                    PushRed.Visible = false;
                    BtnBackStorage.Visible = false;
                    Print.Visible = false;
                    SumPrint.Visible = false;
                    //审核日期和审核人隐藏
                    LabelApproveDate.Visible = false;
                    LabelVerifier.Visible = false;

                    if (InputColour.Value == "1")
                    {
                        Append.Visible = false;
                        AdjustLenWid.Visible = true;//调整长宽，推红可用
                        ImageRed.Visible = true;

                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            if (InputColour.Value == "1")
                            {
                                ((TextBox)GridView1.Rows[i].FindControl("TextBoxPosition")).Enabled = true;
                                ((TextBox)GridView1.Rows[i].FindControl("TextBoxLotNumber")).Enabled = true;
                            }
                        }
                    }
                }
                if (LabelState.Text == "2")
                {
                   
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {

                        ((TextBox)GridView1.Rows[i].FindControl("TextBoxRN")).Enabled = false;
                        ((TextBox)GridView1.Rows[i].FindControl("TextBoxRQN")).Enabled = false;
                    }

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
                        copyform.Visible = true;
                        BtnBackStorage.Visible = true;
                        
                        if (InputColour.Value == "1")
                        {
                            btn_mto.Visible = true;
                        }
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
                        BtnBackStorage.Visible = false;
                        //PushRed.Enabled = false;
                        
                    }

                    if (InputColour.Value == "1")
                    {
                        ImageRed.Visible = true;
                    }
                }
            }

        }
     
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tdn += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "RN"));
                trn += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DN"));
                tdqn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DQN"));
                trqn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RQN"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.Cells[13].FindControl("LabelTotalDN")).Text = Math.Round(trn, 4).ToString();
                ((Label)e.Row.Cells[14].FindControl("LabelTotalRN")).Text = Math.Round(tdn, 4).ToString();
                ((Label)e.Row.Cells[15].FindControl("LabelTotalDQN")).Text = tdqn.ToString();
                ((Label)e.Row.Cells[16].FindControl("LabelTotalRQN")).Text = trqn.ToString();
            }
        }

        protected string generateCode()
        {
            string sql = "SELECT MAX(OP_CODE) AS MaxCode FROM TBWS_OUTCODE WHERE LEN(OP_CODE)=10 AND OP_BILLTYPE='6'";
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
                return "QT00000001";
            }
            else
            {
                int tempnum = Convert.ToInt32((code.Substring(2, 8)));
                tempnum++;
                code = "QT" + tempnum.ToString().PadLeft(8, '0');
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
            dr.Close();
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
            tb.Columns.Add("DueLength", System.Type.GetType("System.String"));
            tb.Columns.Add("Length", System.Type.GetType("System.String"));
            tb.Columns.Add("Width", System.Type.GetType("System.String"));
            tb.Columns.Add("Unit", System.Type.GetType("System.String"));
            tb.Columns.Add("DN", System.Type.GetType("System.String"));
            tb.Columns.Add("RN", System.Type.GetType("System.String"));
            tb.Columns.Add("DIFNUM", System.Type.GetType("System.String"));
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
            tb.Columns.Add("BSH", System.Type.GetType("System.String"));


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
                row["LotNumber"] = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text;
                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                row["DueLength"] = ((Label)GRow.FindControl("LabelLength")).Text;
                row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                row["DN"] = ((Label)GRow.FindControl("LabelDN")).Text;
                row["RN"] = ((TextBox)GRow.FindControl("TextBoxRN")).Text;
                row["DIFNUM"] = ((Label)GRow.FindControl("LabelDIFNUM")).Text;
                row["DQN"] = ((Label)GRow.FindControl("LabelDQN")).Text;
                row["RQN"] = ((TextBox)GRow.FindControl("TextBoxRQN")).Text;
                row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                row["OrderID"] = ((Label)GRow.FindControl("LabelOrderID")).Text;
                row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                row["Position"] = ((TextBox)GRow.FindControl("TextBoxPosition")).Text;
                row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                row["BSH"] = ((Label)GRow.FindControl("LabelBSH")).Text;

                tb.Rows.Add(row);
            }
            return tb;
        }

        protected void Append_Click(object sender, EventArgs e)
        {
            DataTable dt = getDataFromGridView();
            string sql = "SELECT UniqueID='',SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed," +
                    "CAST(Length AS VARCHAR(50)) AS DueLength,CAST(Length AS VARCHAR(50)) AS Length,CAST(Width  AS VARCHAR(50)) AS Width," +
                    "LotNumber AS LotNumber,PlanMode AS PlanMode,PTC AS PTC,OrderCode AS OrderID," +
                    "WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,LocationCode AS PositionCode," +
                    "Location AS Position,Unit AS Unit," +
                    " CAST(CAST(Number AS float) AS VARCHAR(50)) AS DN, CAST(CAST(Number AS float) AS VARCHAR(50)) AS RN,cast((Number-Number) as VARCHAR(50)) AS DIFNUM," +
                    " CAST(CAST(SupportNumber AS float) AS VARCHAR(50)) AS DQN,CAST(CAST(SupportNumber AS float) AS VARCHAR(50)) AS RQN," +
                    "Note AS Comment,CGMODE AS BSH " +
                    "FROM View_SM_Storage WHERE State='APPENDOUTQT" + Session["UserID"].ToString() + "'";
            dt.Merge(DBCallCommon.GetDTUsingSqlText(sql));
            GridView1.DataSource = dt;
            GridView1.DataBind();
            sql = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='APPENDOUTQT" + Session["UserID"].ToString() + "'";
            DBCallCommon.ExeSqlText(sql);
            LabelMessage.Text = "追加成功";

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (InputColour.Value == "1")
                {
                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxPosition")).Enabled = true;
                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxLotNumber")).Enabled = true;
                }

                ((TextBox)GridView1.Rows[i].FindControl("TextBoxRN")).Enabled = true;
                ((TextBox)GridView1.Rows[i].FindControl("TextBoxRQN")).Enabled = true;
            }
            CheckSQCODE();
            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", "bindunbeforunload();", true);

        }

        protected void CheckSQCODE() //有重复的计划跟踪号有颜色区分
        {

            for (int i = 0; i < (GridView1.Rows.Count - 1); i++)
            {
                GridViewRow gvrow0 = GridView1.Rows[i];
                string sqcode = ((Label)gvrow0.FindControl("LabelPTC")).Text;
                string materialcode = ((Label)gvrow0.FindControl("LabelMaterialCode")).Text; //物料代码
                if ((materialcode.Substring(0, 5) != "01.07")&&(materialcode.Substring(0, 5) != "01.14"))
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
            tb.Columns.Add("DueLength", System.Type.GetType("System.String"));
            tb.Columns.Add("Length", System.Type.GetType("System.String"));
            tb.Columns.Add("Width", System.Type.GetType("System.String"));
            tb.Columns.Add("Unit", System.Type.GetType("System.String"));
            tb.Columns.Add("DN", System.Type.GetType("System.String"));
            tb.Columns.Add("RN", System.Type.GetType("System.String"));
            tb.Columns.Add("DIFNUM", System.Type.GetType("System.String"));
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
            tb.Columns.Add("BSH", System.Type.GetType("System.String"));
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
                            try
                            {
                                dn = Convert.ToSingle(((Label)GRow.FindControl("LabelDN")).Text);//即时库存数量
                                rn = Convert.ToSingle(((TextBox)GRow.FindControl("TextBoxRN")).Text);//实际数量
                                dqn = Convert.ToInt32(((Label)GRow.FindControl("LabelDQN")).Text);//即时库存张(支)
                                rqn = Convert.ToInt32(((TextBox)GRow.FindControl("TextBoxRQN")).Text);//实发张（支）数
                            }
                            catch
                            {
                                LabelMessage.Text = "请正确填写实发数量！";
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
                               // row["LotNumber"] = ((Label)GRow.FindControl("LabelLotNumber")).Text;
                                if (LabelCode.Text.Contains('R')) //为红单审核是相当于入库，批号自动生成，可以修改仓库，以免在库存汇总
                                {
                                    if (j <= 8)
                                    {
                                        row["LotNumber"] = "00" + (j + 1).ToString() + "-" + LabelCode.Text;
                                    }
                                    if ((j > 8) && (j <= 98))
                                    {
                                        row["LotNumber"] = "0" + (j + 1).ToString() + LabelCode.Text;
                                    }
                                    if (98 < j)
                                    {
                                        row["LotNumber"] = (j + 1).ToString() + LabelCode.Text;
                                    }
                                }
                                else
                                {
                                    row["LotNumber"] = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text;

                                }
                                //if (((Label)GRow.FindControl("LabelLotNumber")).Text != "")
                                //{
                                //    string[] lsh = ((Label)GRow.FindControl("LabelLotNumber")).Text.Split('-');
                                //    string Lot = (Convert.ToInt32(lsh[0]) + count - 1).ToString().PadLeft(3, '0') + "-" + lsh[1];
                                //    row["LotNumber"] = Lot;
                                //}
                                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                                row["DueLength"] = ((Label)GRow.FindControl("LabelLength")).Text;
                                row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                                row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                                row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                                row["DN"] = Math.Round((dn / count), 4).ToString();//仓库数量
                                row["RN"] = Math.Round((rn / count), 4).ToString();//实收数量
                                row["DIFNUM"] = ((Label)GRow.FindControl("LabelDIFNUM")).Text;
                                row["DQN"] = Convert.ToInt32((dqn / count)).ToString();
                                row["RQN"] = Convert.ToInt32((rqn / count)).ToString();
                                row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                                row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                                row["OrderID"] = ((Label)GRow.FindControl("LabelOrderID")).Text;
                                row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                                row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                                row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                                //row["Position"] = ((Label)GRow.FindControl("LabelPosition")).Text;
                                //row["PositionCode"] = ((Label)GRow.FindControl("LabelPositionCode")).Text;

                                row["Position"] = ((TextBox)GRow.FindControl("TextBoxPosition")).Text;
                                row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                                row["BSH"] = ((Label)GRow.FindControl("LabelBSH")).Text;

                                tb.Rows.Add(row);
                            }
                            ////////////////////////////////////////////////////////////////////////////////////////////
                            /*
                             * 产生最后一行
                             */
                            DataRow row1 = tb.NewRow();
                            row1["UniqueID"] = ((Label)GRow.FindControl("LabelUniqueID")).Text;
                            row1["SQCODE"] = ((Label)GRow.FindControl("LabelSQCODE")).Text;
                            row1["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                            row1["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
                            row1["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;
                            row1["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
                            //row1["LotNumber"] = ((Label)GRow.FindControl("LabelLotNumber")).Text;

                            if (LabelCode.Text.Contains('R')) //为红单审核是相当于入库，批号自动生成，可以修改仓库，以免在库存汇总
                            {
                                if (count - 1 <= 8)
                                {
                                    row1["LotNumber"] = "00" + count.ToString() + "-" + LabelCode.Text;
                                }
                                if ((count - 1 > 8) && (count - 1 <= 98))
                                {
                                    row1["LotNumber"] = "0" + count.ToString() + LabelCode.Text;
                                }
                                if (98 < count - 1)
                                {
                                    row1["LotNumber"] = count.ToString() + LabelCode.Text;
                                }
                            }
                            else
                            {
                                row1["LotNumber"] = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text;

                            }
                            //if (((Label)GRow.FindControl("LabelLotNumber")).Text != "")
                            //{
                            //    string[] lsh = ((Label)GRow.FindControl("LabelLotNumber")).Text.Split('-');
                            //    string Lot = (Convert.ToInt32(lsh[0]) + count - 1).ToString().PadLeft(3, '0') + "-" + lsh[1];
                            //    row1["LotNumber"] = Lot;
                            //}

                            row1["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                            row1["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                            row1["DueLength"] = ((Label)GRow.FindControl("LabelLength")).Text;
                            row1["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                            row1["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                            row1["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                            //Math.Round((num - Math.Round((num / count), 4) * (count - 1)), 4).ToString();

                            row1["DN"] = Math.Round((dn - Math.Round((dn / count), 4) * (count - 1)), 4).ToString();//仓库数量
                            row1["RN"] = Math.Round((rn - Math.Round((rn / count), 4) * (count - 1)), 4).ToString();//实收数量
                            row1["DIFNUM"] = ((Label)GRow.FindControl("LabelDIFNUM")).Text;
                            row1["DQN"] = Convert.ToInt32((dqn / count)).ToString();
                            row1["RQN"] = Convert.ToInt32((rqn / count)).ToString();
                            row1["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                            row1["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                            row1["OrderID"] = ((Label)GRow.FindControl("LabelOrderID")).Text;
                            row1["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                            row1["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                            row1["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;

                            //row1["Position"] = ((Label)GRow.FindControl("LabelPosition")).Text;
                            //row1["PositionCode"] = ((Label)GRow.FindControl("LabelPositionCode")).Text;

                            row1["Position"] = ((TextBox)GRow.FindControl("TextBoxPosition")).Text;
                            row1["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                            row1["BSH"] = ((Label)GRow.FindControl("LabelBSH")).Text;

                            tb.Rows.Add(row1);

                            ////////////////////////////////////////////////////////////////////////////////////////////

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
                                //row["LotNumber"] = ((Label)GRow.FindControl("LabelLotNumber")).Text;
                                if (LabelCode.Text.Contains('R')) //为红单审核是相当于入库，批号自动生成，可以修改仓库，以免在库存汇总
                                {
                                    if (j <= 8)
                                    {
                                        row["LotNumber"] = "00" + (j + 1).ToString() + "-" + LabelCode.Text;
                                    }
                                    if ((j > 8) && (j <= 98))
                                    {
                                        row["LotNumber"] = "0" + (j + 1).ToString() + LabelCode.Text;
                                    }
                                    if (98 < j)
                                    {
                                        row["LotNumber"] = (j + 1).ToString() + LabelCode.Text;
                                    }
                                }
                                else
                                {
                                    row["LotNumber"] = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text;

                                }
                                //if (((Label)GRow.FindControl("LabelLotNumber")).Text != "")
                                //{
                                //    string[] lsh = ((Label)GRow.FindControl("LabelLotNumber")).Text.Split('-');
                                //    string Lot = (Convert.ToInt32(lsh[0]) + j).ToString().PadLeft(3, '0') + "-" + lsh[1];
                                //    row["LotNumber"] = Lot;
                                //}

                                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                                row["DueLength"] = ((Label)GRow.FindControl("LabelLength")).Text;
                                row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                                row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                                row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                                row["DN"] = ((Label)GRow.FindControl("LabelDN")).Text;
                                row["RN"] = ((TextBox)GRow.FindControl("TextBoxRN")).Text;
                                row["DIFNUM"] = ((Label)GRow.FindControl("LabelDIFNUM")).Text;
                                row["DQN"] = ((Label)GRow.FindControl("LabelDQN")).Text;
                                row["RQN"] = ((TextBox)GRow.FindControl("TextBoxRQN")).Text;
                                row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                                row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                                row["OrderID"] = ((Label)GRow.FindControl("LabelOrderID")).Text;
                                row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                                row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                                row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                                row["Position"] = ((TextBox)GRow.FindControl("TextBoxPosition")).Text;
                                row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                                row["BSH"] = ((Label)GRow.FindControl("LabelBSH")).Text;
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
                        row["LotNumber"] = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text;
                        row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                        row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                        row["DueLength"] = ((Label)GRow.FindControl("LabelLength")).Text;
                        row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                        row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                        row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                        row["DN"] = ((Label)GRow.FindControl("LabelDN")).Text;
                        row["RN"] = ((TextBox)GRow.FindControl("TextBoxRN")).Text;
                        row["DIFNUM"] = ((Label)GRow.FindControl("LabelDIFNUM")).Text;
                        row["DQN"] = ((Label)GRow.FindControl("LabelDQN")).Text;
                        row["RQN"] = ((TextBox)GRow.FindControl("TextBoxRQN")).Text;
                        row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                        row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                        row["OrderID"] = ((Label)GRow.FindControl("LabelOrderID")).Text;
                        row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                        row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                        row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                        row["Position"] = ((TextBox)GRow.FindControl("TextBoxPosition")).Text;
                        row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                        row["BSH"] = ((Label)GRow.FindControl("LabelBSH")).Text;

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
                            try
                            {
                                dn = Convert.ToSingle(((Label)GRow.FindControl("LabelDN")).Text);
                                rn = Convert.ToSingle(((TextBox)GRow.FindControl("TextBoxRN")).Text);
                                dqn = Convert.ToInt32(((Label)GRow.FindControl("LabelDQN")).Text);
                                rqn = Convert.ToInt32(((TextBox)GRow.FindControl("TextBoxRQN")).Text);
                            }
                            catch
                            {
                                LabelMessage.Text = "请正确填写实发数量！";
                                return;
                            }
                            for (int j = 0; j < count - 2; j++)
                            {
                                DataRow row = tb.NewRow();
                                row["UniqueID"] = ((Label)GRow.FindControl("LabelUniqueID")).Text;
                                row["SQCODE"] = ((Label)GRow.FindControl("LabelSQCODE")).Text;
                                row["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                                row["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
                                row["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;
                                row["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
                                row["LotNumber"] = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text.Trim();
                                //if (((Label)GRow.FindControl("LabelLotNumber")).Text != "")
                                //{
                                //    string[] lsh = ((Label)GRow.FindControl("LabelLotNumber")).Text.Split('-');
                                //    string Lot = (Convert.ToInt32(lsh[0]) + j + 1).ToString().PadLeft(3, '0') + "-" + lsh[1];
                                //    row["LotNumber"] = Lot;
                                //}

                                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                                row["DueLength"] = ((Label)GRow.FindControl("LabelLength")).Text;
                                row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                                row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                                row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                                row["DN"] = (dn / count).ToString();
                                tb.Rows[i]["DN"] = (dn / count).ToString();
                                row["RN"] = (rn / count).ToString();
                                tb.Rows[i]["RN"] = (rn / count).ToString();
                                row["DIFNUM"] = ((Label)GRow.FindControl("LabelDIFNUM")).Text;
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
                                row["Position"] = ((TextBox)GRow.FindControl("TextBoxPosition")).Text;
                                row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                                row["BSH"] = ((Label)GRow.FindControl("LabelBSH")).Text;
                                tb.Rows.Add(row);
                            }

                            //产生最后一行
                            DataRow row1 = tb.NewRow();
                            row1["UniqueID"] = ((Label)GRow.FindControl("LabelUniqueID")).Text;
                            row1["SQCODE"] = ((Label)GRow.FindControl("LabelSQCODE")).Text;
                            row1["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                            row1["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
                            row1["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;
                            row1["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
                            row1["LotNumber"] = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text.Trim();
                            //if (((Label)GRow.FindControl("LabelLotNumber")).Text != "")
                            //{
                            //    string[] lsh = ((Label)GRow.FindControl("LabelLotNumber")).Text.Split('-');
                            //    string Lot = (Convert.ToInt32(lsh[0]) + count - 1).ToString().PadLeft(3, '0') + "-" + lsh[1];
                            //    row1["LotNumber"] = Lot;
                            //}

                            row1["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                            row1["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                            row1["DueLength"] = ((Label)GRow.FindControl("LabelLength")).Text;
                            row1["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                            row1["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                            row1["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;

                            //Math.Round((num - Math.Round((num / count), 4) * (count - 1)), 4).ToString();

                            row1["DN"] = Math.Round((dn - Math.Round((dn / count), 4) * (count - 1)), 4).ToString();//仓库数量
                            row1["RN"] = Math.Round((rn - Math.Round((rn / count), 4) * (count - 1)), 4).ToString();//实收数量
                            row1["DIFNUM"] = ((Label)GRow.FindControl("LabelDIFNUM")).Text;
                            row1["DQN"] = Convert.ToInt32((dqn / count)).ToString();
                            row1["RQN"] = Convert.ToInt32((rqn / count)).ToString();
                            row1["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                            row1["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                            row1["OrderID"] = ((Label)GRow.FindControl("LabelOrderID")).Text;
                            row1["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                            row1["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                            row1["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;

                            //row1["Position"] = ((Label)GRow.FindControl("LabelPosition")).Text;
                            //row1["PositionCode"] = ((Label)GRow.FindControl("LabelPositionCode")).Text;

                            row1["Position"] = ((TextBox)GRow.FindControl("TextBoxPosition")).Text;
                            row1["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                            row1["BSH"] = ((Label)GRow.FindControl("LabelBSH")).Text;

                            tb.Rows.Add(row1);

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
                                row["LotNumber"] = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text.Trim();
                                //if (((Label)GRow.FindControl("LabelLotNumber")).Text != "")
                                //{
                                //    string[] lsh = ((Label)GRow.FindControl("LabelLotNumber")).Text.Split('-');
                                //    string Lot = (Convert.ToInt32(lsh[0]) + j + 1).ToString().PadLeft(3, '0') + "-" + lsh[1];
                                //    row["LotNumber"] = Lot;
                                //}

                                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                                row["DueLength"] = ((Label)GRow.FindControl("LabelLength")).Text;
                                row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                                row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                                row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                                row["DN"] = ((Label)GRow.FindControl("LabelDN")).Text;
                                row["RN"] = ((TextBox)GRow.FindControl("TextBoxRN")).Text;
                                row["DIFNUM"] = ((Label)GRow.FindControl("LabelDIFNUM")).Text;
                                row["DQN"] = ((Label)GRow.FindControl("LabelDQN")).Text;
                                row["RQN"] = ((TextBox)GRow.FindControl("TextBoxRQN")).Text;
                                row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                                row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                                row["OrderID"] = ((Label)GRow.FindControl("LabelOrderID")).Text;
                                row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                                row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                                row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                                row["Position"] = ((TextBox)GRow.FindControl("TextBoxPosition")).Text;
                                row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                                row["BSH"] = ((Label)GRow.FindControl("LabelBSH")).Text;
                                tb.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            GridView1.DataSource = tb;
            GridView1.DataBind();
            if (LabelCode.Text.Contains("R"))
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxPosition")).Enabled = true;
                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxLotNumber")).Enabled = true;
                }
            }

            //string NowCode = string.Empty;
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    //if ((GridView1.Rows[i].FindControl("CheckBox1") as CheckBox).Checked)
                
            //    Label lbCode = (GridView1.Rows[i].FindControl("LabelPTC") as Label);
            //    string lotnum0 = (GridView1.Rows[i].FindControl("LabelLotNumber") as Label).Text.Trim();
            //    if (lotnum0 != "")
            //    {
            //        string Code = lbCode.Text;

            //        if (Code != NowCode)
            //        {
            //            NowCode = lbCode.Text;

            //            string lotnum = (GridView1.Rows[i].FindControl("LabelLotNumber") as Label).Text.Trim();

            //            for (int j = i + 1; j < GridView1.Rows.Count; j++)
            //            {
            //                string NextCode = (GridView1.Rows[j].FindControl("LabelPTC") as Label).Text;

            //                if (Code == NextCode)
            //                {

            //                    string id = (Convert.ToInt32((GridView1.Rows[j - 1].FindControl("LabelLotNumber") as Label).Text.Trim().Split('-')[0]) + 1).ToString().PadLeft(3, '0');

            //                    (GridView1.Rows[j].FindControl("LabelLotNumber") as Label).Text = id + "-" + (GridView1.Rows[j - 1].FindControl("LabelLotNumber") as Label).Text.Trim().Split('-')[1];

            //                }
            //                else
            //                {

            //                    break;

            //                }
            //            }

            //        }
            //    }
            //}


            LabelMessage.Text = "拆分成功！";

            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", "bindunbeforunload();", true);
        }

        protected void ButtonSplitCancel_Click(object sender, EventArgs e)
        {
            RadioButtonListSplitMode.Items[0].Selected = true;
            RadioButtonListSplitMode2.Items[0].Selected = true;
            TextBoxSplitLineNum.Text = "2";

            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", "bindunbeforunload();", true);
        }

        protected void Delete_Click(object sender, EventArgs e)
        {

            string Code = LabelCode.Text;

            string sqlstate = "select OP_STATE from TBWS_OUT where OP_CODE='" + Code + "'";

            if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows.Count > 0)
            {
                if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows[0]["OP_STATE"].ToString() == "2")
                {

                    string script = @"alert('单据已审核，条目不能删除！');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    return;
                }
            }

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
            if (LabelCode.Text.Contains('R')) //为红单审核是相当于入库，可以修改仓库
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxPosition")).Enabled = true;
                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxLotNumber")).Enabled = true;
                }
            }

            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", "bindunbeforunload();", true);
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


        //保存操作
        protected void Save_Click(object sender, EventArgs e)
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

            string Code = LabelCode.Text;

            string sqlstate = "select OP_STATE from TBWS_OUT where OP_CODE='" + Code + "'";

            if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows.Count > 0)
            {
                if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows[0]["OP_STATE"].ToString() == "2")
                {

                    string script = @"alert('单据已审核，单据不能保存！');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    return;
                }
            }

            //此处是保存操作
            List<string> sqllist = new List<string>();
            string sql = "";
           
            string State = LabelState.Text;
            string Colour = InputColour.Value;
            string Date = TextBoxDate.Text;
            string LLDepCode = DropDownListDep.SelectedValue;
            string SCZH = TextBoxSCZH.Text;

            string Comment = DropDownListBZ.SelectedValue;

            //string ZXMC = DropDownListZXMC.SelectedValue;

            string DocCode = LabelDocCode.Text;
            string SendClerkCode = DropDownListSender.SelectedValue;
            string VerifierCode = LabelVerifierCode.Text;
            string ApproveDate = LabelApproveDate.Text;
            string NOTE1 = TextBoxNOTE1.Text; 

            string BillType = "6";

           
            //int pagenum = 0;

            //try
            //{
            //    pagenum = Convert.ToInt32(TextBoxPageNum.Text.Trim());
            //}

            //catch { pagenum = 0; }

            sql = "select count(*) from TBWS_OUT where OP_CODE='" + Code + "'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() == "0")
            {
                sql = "exec ResetSeed @tablename=TBWS_OUT";

                sqllist.Add(sql);

                sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_DEP,OP_DATE,OP_TSAID," +
                    "OP_SENDER,OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
                    "OP_STATE,OP_NOTE,OP_BILLTYPE,OP_RealTime,OP_NOTE1) VALUES('" + Code + "','" + LLDepCode + "','" + Date + "','" + SCZH + "','" +
                    SendClerkCode + "','" + DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','" +
                    Colour + "','1','" + Comment + "','" + BillType + "',convert(varchar(50),getdate(),120),'" + NOTE1 + "')";
               
                sqllist.Add(sql);
            }
            else
            {

                sql = "update TBWS_OUT set OP_DEP='" + LLDepCode + "',OP_DATE='" + Date + "',OP_TSAID='" + SCZH + "',";
                sql += "OP_SENDER='" + SendClerkCode + "',OP_DOC='" + DocCode + "',OP_VERIFIER='" + VerifierCode + "',OP_VERIFYDATE='" + ApproveDate + "',OP_HSFLAG='0',";
                sql += "OP_ROB='" + Colour + "',OP_STATE='1',OP_NOTE='" + Comment + "',OP_BILLTYPE='" + BillType + "',OP_RealTime=convert(varchar(50),getdate(),120),OP_NOTE1='" + NOTE1 + "' where OP_CODE='" + Code + "'";

                sqllist.Add(sql);
            }

            sql = "DELETE FROM TBWS_OUTDETAIL WHERE OP_CODE='" + Code + "'";

            sqllist.Add(sql);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string UniqueID = Code + (i + 1).ToString().PadLeft(3, '0');

                string SQCODE = ((Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;

                //旧的仓库唯一号

                string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text.Trim();
                string LotNumber = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLotNumber")).Text.Trim();
                string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;

                int DueLength = 0;
                try { DueLength = Convert.ToInt32(Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelLength")).Text)); }
                catch { DueLength = 0; }


                int Length = 0;
                try { Length = Convert.ToInt32(Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLength")).Text)); }
                catch { Length = 0; }
                int Width = 0;
                try { Width = Convert.ToInt32(Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxWidth")).Text)); }
                catch { Width = 0; }

                string WarehouseOutCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;

                string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;

                string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;
                
                //保存仓库号时需要重新获取


                //@mid+@mptc+@mlotnum+CAST(@mlength AS VARCHAR(50))+CAST(@mwidth AS VARCHAR(50))+@mstid+@mwlid（物料代码+计划跟踪号+批号+长+宽+仓库+仓位）

                //新的仓库唯一号
                //string SQCODE = MaterialCode + PTC + LotNumber + Convert.ToString(Length) + Convert.ToString(Width) + WarehouseOutCode + PositionCode;  

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
                string PlanMode = ((Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text;
                //string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;
                string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                string Note = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;
                string bsh = ((Label)this.GridView1.Rows[i].FindControl("LabelBSH")).Text; //标识号

                sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                    "OP_FIXED,OP_DUELENGTH,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                    "OP_UPRICE,OP_AMOUNT,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                    "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE,OP_BSH) VALUES('" + Code + "','" + UniqueID + "','" + SQCODE + "','" +
                    MaterialCode + "','" +
                    Fixed + "','" + DueLength + "','" + Length + "','" +
                    Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','0','0','" +
                    WarehouseOutCode + "','" + PositionCode + "','" + PlanMode + "','" +
                    PTC + "','" + OrderID + "','" + Note + "','','" + bsh + "')";
                sqllist.Add(sql);
            }
            DBCallCommon.ExecuteTrans(sqllist);
            sqllist.Clear();
            LabelState.Text = "1";
            LabelMessage.Text = "保存成功";
            DeleteBill.Enabled = true;
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

                bool HasError = false;
                int ErrorType = 0;


                //此处是审核操作
                List<string> sqllist = new List<string>();
                string sql = "";


                string State = LabelState.Text;
                string Colour = InputColour.Value;
                string Date = TextBoxDate.Text;
                string LLDepCode = DropDownListDep.SelectedValue;

                if (LLDepCode == "0")
                {
                    LabelMessage.Text = "领料部门为空，单据不能审核！";

                    string script = @"alert('领料部门为空，单据不能审核！');";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    return;
                }

                string SCZH = TextBoxSCZH.Text;

                if (SCZH == string.Empty)
                {
                    LabelMessage.Text = "生成制号为空，单据不能审核！";

                    string script = @"alert('生成制号为空，单据不能审核！');";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    return;
                }

                //string Comment = TextBoxComment.Text;

                string Comment = DropDownListBZ.SelectedValue;

                //string ZXMC = DropDownListZXMC.SelectedValue;

                string DocCode = LabelDocCode.Text;
                string SendClerkCode = DropDownListSender.SelectedValue;
                string VerifierCode = Session["UserID"].ToString();

                if ((SendClerkCode == DocCode) && (DocCode == VerifierCode)) //制单人，审核人，发料人不能相同
                {
                    LabelMessage.Text = "制单人，审核人，发料人不能相同！";

                    string script = @"alert('制单人，审核人，发料人不能相同！');";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    return;
                }
                string NOTE1 = TextBoxNOTE1.Text;
                string BillType = "6";
                
                //int pagenum = 0;

                //try
                //{
                //    pagenum = Convert.ToInt32(TextBoxPageNum.Text.Trim());
                //}

                //catch { pagenum = 0; }




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


                sql = "select count(*) from TBWS_OUT where OP_CODE='" + Code + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                if (dt.Rows[0][0].ToString() == "0")
                {
                    sql = "exec ResetSeed @tablename=TBWS_OUT";

                    sqllist.Add(sql);

                    sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_DEP,OP_DATE,OP_TSAID," +
                        "OP_SENDER,OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
                        "OP_STATE,OP_NOTE,OP_BILLTYPE,OP_RealTime,OP_NOTE1) VALUES('" + Code + "','" + LLDepCode + "','" + Date + "','" + SCZH + "','" +
                        SendClerkCode + "','" + DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','" +
                        Colour + "','1','" + Comment + "','" + BillType + "',convert(varchar(50),getdate(),120),'" + NOTE1 + "')";

                    sqllist.Add(sql);

                }
                else
                {

                    sql = "update TBWS_OUT set OP_DEP='" + LLDepCode + "',OP_DATE='" + Date + "',OP_TSAID='" + SCZH + "',";
                    sql += "OP_SENDER='" + SendClerkCode + "',OP_DOC='" + DocCode + "',OP_VERIFIER='" + VerifierCode + "',OP_VERIFYDATE='" + ApproveDate + "',OP_HSFLAG='0',";
                    sql += "OP_ROB='" + Colour + "',OP_STATE='1',OP_NOTE='" + Comment + "',OP_BILLTYPE='" + BillType + "',OP_RealTime=convert(varchar(50),getdate(),120),OP_NOTE1='" + NOTE1 + "' where OP_CODE='" + Code + "'";

                    sqllist.Add(sql);
                }

                //sql = "DELETE FROM TBWS_OUT WHERE OP_CODE='" + Code + "'";
                //sqllist.Add(sql);

                //重置标识符
                //sql = "exec ResetSeed @tablename=TBWS_OUT";
                //sqllist.Add(sql);

                //sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_DEP,OP_DATE,OP_TSAID," +
                //    "OP_SENDER,OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
                //    "OP_STATE,OP_NOTE,OP_BILLTYPE,OP_RealTime,OP_ZXMC,OP_PAGENUM) VALUES('" + Code + "','" + LLDepCode + "','" + Date + "','" + SCZH + "','" +
                //    SendClerkCode + "','" + DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','" +
                //    Colour + "','1','" + Comment + "','" + BillType + "',convert(varchar(50),getdate(),120),'" + ZXMC + "','"+pagenum+"')";

                //sql = "update TBWS_OUT set OP_DEP='" + LLDepCode + "',OP_DATE='" + Date + "',OP_TSAID='" + SCZH + "',";
                //sql += "OP_SENDER='" + SendClerkCode + "',OP_DOC='" + DocCode + "',OP_VERIFIER='" + VerifierCode + "',OP_VERIFYDATE='" + ApproveDate + "',OP_HSFLAG='0',";
                //sql += "OP_ROB='" + Colour + "',OP_STATE='1',OP_NOTE='" + Comment + "',OP_BILLTYPE='" + BillType + "'OP_RealTime=convert(varchar(50),getdate(),120),OP_ZXMC='" + ZXMC + "',OP_PAGENUM='" + pagenum + "' where OP_CODE='" + Code + "'";

                //sqllist.Add(sql);

                sql = "DELETE FROM TBWS_OUTDETAIL WHERE OP_CODE='" + Code + "'";
                sqllist.Add(sql);
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    string UniqueID = Code + (i + 1).ToString().PadLeft(3, '0');

                    string SQCODE = ((Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;

                    //旧的仓库唯一号

                    string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text.Trim();
                    string LotNumber = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLotNumber")).Text.Trim();
                    string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;

                    int DueLength = 0;
                    try { DueLength = Convert.ToInt32(Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelLength")).Text)); }
                    catch { DueLength = 0; }


                    int Length = 0;
                    try { Length = Convert.ToInt32(Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLength")).Text)); }
                    catch { Length = 0; }
                    int Width = 0;
                    try { Width = Convert.ToInt32(Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxWidth")).Text)); }
                    catch { Width = 0; }

                    string WarehouseOutCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;

                    string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;

                    //if (PositionCode == string.Empty || PositionCode == "0")
                    //{
                    //    HasError = true;
                    //    ErrorType = 4;
                    //    break;
                    //}

                    string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;

                    //保存仓库号时需要重新获取
                    //@mid+@mptc+@mlotnum+CAST(@mlength AS VARCHAR(50))+CAST(@mwidth AS VARCHAR(50))+@mstid+@mwlid（物料代码+计划跟踪号+批号+长+宽+仓库+仓位）

                    //string SQCODE = MaterialCode + PTC + LotNumber + Convert.ToString(Length) + Convert.ToString(Width) + WarehouseOutCode + PositionCode;

                    //新的仓库唯一号

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
                        float temp = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxRN")).Text.Trim());
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
                    if (Math.Abs(RN) > Math.Abs(DN))
                    {
                        HasError = true;
                        ErrorType = 2;
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
                        Int32 temp = Convert.ToInt32(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxRQN")).Text.Trim());
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

                    //if (Math.Abs(RQN) > Math.Abs(DQN))
                    //{
                    //    HasError = true;
                    //    ErrorType = 3;
                    //    break;
                    //}

                    string PlanMode = ((Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text;
                    //string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;
                    string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                    string Note = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;
                    string bsh = ((Label)this.GridView1.Rows[i].FindControl("LabelBSH")).Text;

                    sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                        "OP_FIXED,OP_DUELENGTH,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                        "OP_UPRICE,OP_AMOUNT,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                        "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE,OP_BSH) VALUES('" + Code + "','" + UniqueID + "','" + SQCODE + "','" +
                        MaterialCode + "','" +
                        Fixed + "','" + DueLength + "','" + Length + "','" +
                        Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','0','0','" +
                        WarehouseOutCode + "','" + PositionCode + "','" + PlanMode + "','" +
                        PTC + "','" + OrderID + "','" + Note + "','','" + bsh + "')";
                    sqllist.Add(sql);
                }

                if (HasError)
                {
                    sqllist.Clear();

                    if (ErrorType == 1)
                    {
                        LabelMessage.Text = "出库条目为0，单据不能审核！";

                        string script = @"alert('出库条目为0，单据不能审核！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                        return;
                    }
                    else if (ErrorType == 2)
                    {
                        LabelMessage.Text = "出库数大于库存数，单据不能审核！";

                        string script = @"alert('出库数大于库存数，单据不能审核！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                        return;
                    }
                    else if (ErrorType == 3)
                    {

                        LabelMessage.Text = "出库张数大于库存张数，单据不能审核！";

                        string script = @"alert('出库张数大于库存张数，单据不能审核！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                    else if (ErrorType == 4)
                    {

                        LabelMessage.Text = "出库条目仓位为空，单据不能审核！";

                        string script = @"alert('出库条目仓位为空，单据不能审核！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                        return;
                    }
                }
                else
                {
                    DBCallCommon.ExecuteTrans(sqllist);
                    sqllist.Clear();

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
                        Response.Redirect("SM_WarehouseOUT_QT.aspx?FLAG=OPEN&&ID=" + Code);
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
                    {
                        LabelMessage.Text = "审核未通过：第" + Convert.ToString(cmd.Parameters["@ErrorRow"].Value) + "行库存物料不存在！";
                        GridView1.Rows[Convert.ToInt32(cmd.Parameters["@ErrorRow"].Value) - 1].Cells[0].BackColor = System.Drawing.Color.Red;
                        GridView1.Rows[Convert.ToInt32(cmd.Parameters["@ErrorRow"].Value) - 1].Cells[1].BackColor = System.Drawing.Color.Red;
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
                    {
                        LabelMessage.Text = "审核未通过：第" + Convert.ToString(cmd.Parameters["@ErrorRow"].Value) + "行库存物料数量或辅助数量小于出库数量！";
                        GridView1.Rows[Convert.ToInt32(cmd.Parameters["@ErrorRow"].Value) - 1].Cells[0].BackColor = System.Drawing.Color.Red;
                        GridView1.Rows[Convert.ToInt32(cmd.Parameters["@ErrorRow"].Value) - 1].Cells[1].BackColor = System.Drawing.Color.Red;
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 3)
                    {
                        LabelMessage.Text = "审核未通过：张(支)数大于1的物料，更改了长度！";
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 4)
                    {
                        LabelMessage.Text = "长宽和数量不相符！";
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 5)
                    {
                        LabelMessage.Text = "审核未通过：该出库单已经被审核！";
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -1)
                    {
                        LabelMessage.Text = "服务器发生异常，请稍后再试！";
                    }
                }
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

                string Code = LabelCode.Text;

                string sqlstate = "select OP_STATE from TBWS_OUT where OP_CODE='" + Code + "'";

                if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows.Count > 0)
                {
                    if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows[0]["OP_STATE"].ToString() == "2")
                    {

                        string script = @"alert('单据已审核，单据不能删除！');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                        return;
                    }
                }



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
                return;
            }
            else
            {
                if (LabelState.Text != "2")
                {
                    LabelMessage.Text = "当前出库单未审核,无法反审！";
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
                    Response.Redirect("SM_WarehouseOUT_QT.aspx?FLAG=OPEN&&ID=" + LabelCode.Text);
                }
                if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -1)
                {
                    LabelMessage.Text = "反审核未通过：已存在相应的红联单据！";
                }
                if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -2)
                {
                    LabelMessage.Text = "反审核未通过：当前出库单未审核,无法反审！";
                }
                if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -3)
                {
                    LabelMessage.Text = "反审核未通过：跨月单据不能反审核！";
                }
                if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
                {
                    LabelMessage.Text = "反审核未通过：入库物料发生后续操作！";
                }
                if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
                {
                    LabelMessage.Text = "反审核未通过：部分入库物料发生后续操作！";
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
            string sql = "UPDATE TBWS_OUTDETAIL SET OP_STATE='' WHERE OP_STATE='RED" + Session["UserID"].ToString() + "'";
            sqllist.Add(sql);
            string uniqueid = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked == true)
                {
                    uniqueid = ((Label)GridView1.Rows[i].FindControl("LabelUniqueID")).Text;
                    sql = "UPDATE TBWS_OUTDETAIL SET OP_STATE='RED" + Session["UserID"].ToString() + "' WHERE OP_CODE='" + LabelCode.Text +
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
            Response.Redirect("SM_WarehouseOUT_QT.aspx?FLAG=PUSHRED&&ID=" + LabelCode.Text);
        }
        //特俗退库
        protected void BackStorage_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_WarehouseOUT_Red.aspx?FLAG=PUSHRED&&ID=" + LabelCode.Text);
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
            Response.Redirect("SM_Warehouse_Query.aspx?FLAG=PUSHQTOUT");
        }

        protected void AdjustLenWid_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                ((TextBox)GridView1.Rows[i].FindControl("TextBoxLength")).Enabled = true;
                ((TextBox)GridView1.Rows[i].FindControl("TextBoxWidth")).Enabled = true;
            }
        }

        protected void TextBoxSCZH_TextChanged(object sender, EventArgs e)
        {
            string engid = TextBoxSCZH.Text.Trim();

            engid = engid.Split('-')[engid.Split('-').Length - 1];

            string sql = "select TSA_PJNAME,TSA_ENGNAME,TSA_ID  FROM View_TM_TaskAssign WHERE TSA_FATHERNODE='0' and TSA_ID='" + engid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                TextBoxSCZH.Text = dt.Rows[0]["TSA_PJNAME"] + "-" + dt.Rows[0]["TSA_ENGNAME"] + "-" + dt.Rows[0]["TSA_ID"];
                LabelSCZH.Text = TextBoxSCZH.Text;
            }
            else
            {
                TextBoxSCZH.Text = string.Empty;
            }
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridView1.Rows)
            {
                if ((gr.FindControl("LabelDQN") as Label).Text.Trim() == "1"||(gr.FindControl("LabelDQN") as Label).Text.Trim() == "0")
                {
                    ((TextBox)gr.FindControl("TextBoxLength")).Enabled = true;
                    //((TextBox)GridView1.Rows[i].FindControl("TextBoxWidth")).Enabled = true;
                }
            }
        }


        private void ShowHide()
        {
            if (Convert.ToString(ViewState["ShowOrHide"]) == "Hide")
            {
                GridView1.Columns[3].HeaderStyle.CssClass = "hidecss";
                GridView1.Columns[3].ItemStyle.CssClass = "hidecss";
                GridView1.Columns[3].FooterStyle.CssClass = "hidecss";
            }
            else
            {
                GridView1.Columns[3].HeaderStyle.CssClass = "showcss";
                GridView1.Columns[3].ItemStyle.CssClass = "showcss";
                GridView1.Columns[3].FooterStyle.CssClass = "showcss";
            }
        }

        protected void copyform_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ACTION"] != null && Request.QueryString["ACTION"] == "COPY")
            {
               // Response.Redirect("SM_WarehouseOUT_LL_Auto.aspx?FLAG=COPY&&ID=" + LabelCode.Text.Trim());
            }
            else
            {

                LabelCode.Text = generateCode();
                LabelTrueCode.Text = string.Empty;

                this.Page.Title = "其他出库(" + LabelCode.Text + ")"; 

                string sql = "INSERT INTO TBWS_OUTCODE (OP_CODE,OP_BILLTYPE) VALUES ('" + LabelCode.Text + "','6')";

                DBCallCommon.ExeSqlText(sql);

                LabelState.Text = "1";
                if (LabelState.Text != "2")
                {
                    ImageVerify.Visible = false;
                }
                LabelVerifier.Text = "";
                LabelVerifierCode.Text = "";
                LabelApproveDate.Text = "";

                GridView1.DataSource = null;
                GridView1.DataBind();

                if (LabelState.Text == "1")
                {
                    //未审核
                    Append.Visible = true;
                    Split.Visible = true;
                    Delete.Visible = true;
                    Save.Visible = true;
                    Verify.Visible = true;
                    DeleteBill.Visible = true;
                    AntiVerify.Visible = false;
                    PushRed.Visible = false;
                    BtnBackStorage.Visible = false;
                    Print.Visible = false;
                    SumPrint.Visible = false;
                    btnstorge.Visible = false;
                  
                }
            }
        }
        protected void btn_mto_Click(object sender, EventArgs e)
        {

            btn_mto.Visible = false;

            List<string> sqllist = new List<string>();

            string Code = generateMTOCode();

            string strsql = "INSERT INTO TBWS_MTOCODE (MTO_CODE) VALUES ('" + Code + "')";

            DBCallCommon.ExeSqlText(strsql);

            string sql = "";

            string Date = TextBoxDate.Text;
            string TargetCode = "备库";

            string PlanerCode = LabelVerifierCode.Text;
            string DepCode = "07";
            string DocCode = LabelVerifierCode.Text;
            string VerifierCode = "";
            string ApproveDate = "";
            string Comment = "";

            sql = "DELETE FROM TBWS_MTO WHERE MTO_CODE='" + Code + "'";
            sqllist.Add(sql);

            sql = "INSERT INTO TBWS_MTO(MTO_CODE,MTO_DATE,MTO_TARGETID," +
                "MTO_DEP,MTO_PLANER,MTO_DOC,MTO_VERIFIER,MTO_VERIFYDATE," +
                "MTO_STATE,MTO_NOTE) VALUES('" + Code + "','" +
                Date + "','" + TargetCode + "','" + DepCode + "','" + PlanerCode + "','" + DocCode + "','" +
                VerifierCode + "','" + ApproveDate + "','1','" + Comment + "')";

            sqllist.Add(sql);

            sql = "DELETE FROM TBWS_MTODETAIL WHERE MTO_CODE='" + Code + "'";

            sqllist.Add(sql);


            for (int i = 0; i < GridView1.Rows.Count; i++)
            {

                string UniqueID = Code + (i + 1).ToString().PadLeft(3, '0');

                string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text.Trim();
               
                string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;

                int Length = 0;
                try { Length = Convert.ToInt32(Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLength")).Text)); }
                catch { Length = 0; }
                int Width = 0;

                try { Width = Convert.ToInt32(Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxWidth")).Text)); }
                catch { Width = 0; }

                string LotNumber = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLotNumber")).Text.Trim();

                string PTCFrom = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;

                string WarehouseCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;

                string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;

               

                //保存仓库号时需要重新获取
                //@mid+@mptc+@mlotnum+CAST(@mlength AS VARCHAR(50))+CAST(@mwidth AS VARCHAR(50))+@mstid+@mwlid（物料代码+计划跟踪号+批号+长+宽+仓库+仓位）

                string SQCODE = MaterialCode + PTCFrom + LotNumber + Convert.ToString(Length) + Convert.ToString(Width) + WarehouseCode + PositionCode;

                //新的仓库唯一号

                float RN = 0;
                try
                {
                    float temp = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxRN")).Text);
                    if ((InputColour.Value == "1") && (temp < 0))
                    {
                        RN = -temp;
                    }
                    else
                    { RN = temp; }
                }
                catch { RN = 0; }

              
                Int32 RQN = 0;
                try
                {
                    Int32 temp = Convert.ToInt32(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxRQN")).Text);
                    if ((InputColour.Value == "1") && (temp < 0))
                    {
                        RQN = -temp;
                    }
                    else
                    { RQN = temp; }
                }
                catch { RQN = 0; }


                string PlanMode = ((Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text;
                
                string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;

                string Note = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;

                sql = "INSERT INTO TBWS_MTODETAIL(MTO_UNIQUEID,MTO_CODE,MTO_SQCODE,MTO_MARID," +
                       "MTO_FIXED,MTO_LENGTH,MTO_WIDTH,MTO_PCODE,MTO_FROMPTCODE," +
                       "MTO_WAREHOUSE,MTO_LOCATION,MTO_KTNUM,MTO_KTFZNUM,MTO_TOPTCODE,MTO_TZNUM," +
                       "MTO_TZFZNUM,MTO_PMODE,MTO_ORDERID,MTO_NOTE,MTO_STATE) VALUES('" + UniqueID + "','" +
                       Code + "','" + SQCODE + "','" + MaterialCode + "','" + Fixed + "','" +
                       Length + "','" + Width + "','" + LotNumber + "','" + PTCFrom + "','" + WarehouseCode + "','" + PositionCode + "','" +
                       RN + "','" + RQN + "','备库','" + RN + "','" + RQN + "','" + PlanMode + "','" +
                       OrderID + "','" + Note + "','')";

                sqllist.Add(sql);

            }

            DBCallCommon.ExecuteTrans(sqllist);

            sqllist.Clear();

            //window.open("SM_Warehouse_MTOAdjust.aspx?FLAG=OPEN&&ID=" + Code);

            //Response.Write("<script type='text/javascript' language='javascript' >window.opener.location = window.opener.location.href;window.close();</script>");
            //Response.Redirect("SM_Warehouse_MTOAdjust.aspx?FLAG=OPEN&&ID=" + Code);

            string script = @"window.open('SM_Warehouse_MTOAdjust.aspx?FLAG=OPEN&&ID="+ Code+"');";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

        }

        //生成MTO单号
        protected string generateMTOCode()
        {
            string Code = "";
            string sql = "SELECT MAX(MTO_CODE) AS MaxCode FROM TBWS_MTOCODE WHERE LEN(MTO_CODE)=10";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                if (dr["MaxCode"] != DBNull.Value)
                {
                    Code = dr["MaxCode"].ToString();
                }
            }
            dr.Close();
            if (Code == "")
            {
                return "MTO0000001";
            }
            else
            {
                int tempnum = Convert.ToInt32((Code.Substring(3, 7)));
                tempnum++;
                Code = "MTO" + tempnum.ToString().PadLeft(7, '0');
                return Code;
            }

            
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {

            if (this.SortDire == "ASC")
            {
                //如果上一次为升序，则将其重置为降序
                this.SortDire = "DESC";
            }
            else
            {
                //如果上一次为降序，则将其重置为升序
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

        ////升降序
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


        #region ShowModual
        [System.Web.Services.WebMethodAttribute(),
           System.Web.Script.Services.ScriptMethodAttribute()]

        public static string GetDNInfo(string contextKey)
        {
            StringBuilder sTemp = new StringBuilder();

            sTemp.Append("<table style='background-color:#f3f3f3; border: #A8B7EC 3px solid;font-size:10pt;width: 300px; font-family:Verdana;' cellspacing='0' cellpadding='3'>");
            
            string sqlstr = "select isnull(SQ_NUM,0) as SQ_NUM from TBWS_STORAGE where SQ_CODE='" + contextKey + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlstr);
           if (dr.HasRows)
           {
               sTemp.Append("<tr><td width='50px'><b></b></td>");
               sTemp.Append("<td align='center' width='100px'><b>数量:</b></td>");
               sTemp.Append("<td align='center' width='100px'><b>" + dr["SQ_NUM"].ToString() + "</b></td>");
               sTemp.Append("<td width='50px'><b></b></td></tr>");

               dr.Close();
           }
            else
           {
               sTemp.Append("<tr><td width='50px'><b></b></td>");
               sTemp.Append("<td align='center' width='100px'><b>数量:</b></td>");
               sTemp.Append("<td align='center' width='100px'><b>0</b></td>");
               sTemp.Append("<td width='50px'><b></b></td></tr>");
           }
            sTemp.Append("</table>");

            return sTemp.ToString();
        }
        #endregion


        protected void SplitForm_Click(object sender, EventArgs e)
        {
            string outcode = LabelCode.Text.Trim();

            if (outcode.Contains("S") == true)
            {
                LabelMessage.Text = "子出库单不允许拆分！";
                string script = @"alert('子出库单不允许拆分！');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                return;
            }

            if (outcode.Contains("R") == true)
            {
                LabelMessage.Text = "红字出库单不允许拆分！";
                string script = @"alert('红字出库单不允许拆分！');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                return;
            }

            //string selsql = "select OP_HSFLAG from TBWS_OUT where OP_CODE='" + outcode + "'";

            //if (DBCallCommon.GetDTUsingSqlText(selsql).Rows.Count > 0)
            //{
            //    if (DBCallCommon.GetDTUsingSqlText(selsql).Rows[0]["OP_HSFLAG"].ToString() == "1")
            //    {
            //        LabelMessage.Text = "单据已跨月，不能再拆分！";
            //        string script = @"alert('单据已跨月，不能再拆分！');";
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

            //        return;
            //    }
            //}


            List<string> sqllist = new List<string>();


            string sql = "UPDATE TBWS_OUTDETAIL SET OP_STATE='' WHERE OP_STATE='SPLIT" + Session["UserID"].ToString() + "'";
            sqllist.Add(sql);

            string uniqueid = "";

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked == true)
                {
                    uniqueid = ((Label)GridView1.Rows[i].FindControl("LabelUniqueID")).Text;
                    //更新明细表中的状态
                    sql = "UPDATE TBWS_OUTDETAIL SET OP_STATE='SPLIT" + Session["UserID"].ToString() + "' WHERE OP_CODE='" + outcode + "' AND OP_UNIQUEID='" + uniqueid + "'";
                    sqllist.Add(sql);
                }
            }
            /*
             * 这里出现的1，是因为前面有一条更新语句
             */
            if (sqllist.Count == 1)
            {
                LabelMessage.Text = "请选择要拆分的条目！";
                string script = @"alert('请选择要拆分的条目！');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                return;
            }

            DBCallCommon.ExecuteTrans(sqllist);

            Response.Redirect("SM_WarehouseOUT_OPSplit.aspx?ID=" + outcode);

        }

        protected void MergeForm_Click(object sender, EventArgs e)
        {
            string outcode = LabelCode.Text.Trim();

            string selsql = "select OP_HSFLAG from TBWS_OUT where OP_CODE='" + outcode + "'";

            if (DBCallCommon.GetDTUsingSqlText(selsql).Rows.Count > 0)
            {
                if (DBCallCommon.GetDTUsingSqlText(selsql).Rows[0]["OP_HSFLAG"].ToString() == "1")
                {
                    LabelMessage.Text = "单据已跨月，不能再合并！";
                    string script = @"alert('单据已跨月，不能再合并！');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    return;
                }
            }

            if (outcode.Contains("R") == true)
            {
                LabelMessage.Text = "红字出库单不允许合并！";
                return;
            }
            else
            {
                if (outcode.Contains("S") == true)
                {
                    int count=0;
                    string strsqltex="SELECT COUNT(*) FROM TBWS_OUT WHERE OP_CODE LIKE '"+outcode+"%' AND OP_ROB='1' ";
                    DataTable dt=DBCallCommon.GetDTUsingSqlText(strsqltex);
                    if(dt.Rows.Count>0)
                    {
                      count = Convert.ToInt32(dt.Rows[0][0].ToString());
                    }
                    if(count>0)
                    {
                         string script = @"alert('此子单已经推红，不能再合并！');";
                         ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                        return;
                    }
                    else
                    {
                      string pcode = outcode.Substring(0, outcode.IndexOf("S", 0));
                      Response.Redirect("SM_WarehouseOut_LL_ActionResult.aspx?IDC=" + outcode + "&&IDP=" + pcode + "&&RES==M");
                    }
                }
                else
                {
                    LabelMessage.Text = "请选择子单进行合并操作！";
                    return;
                }
            }
        }

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
            lt.Add("长(mm)");
            lt.Add("宽(mm)");
            lt.Add("即时库存张(支)");
            lt.Add("实发张(支)");
            

            if ((sender as CheckBox).Checked)
            {
                CheckBox7.Enabled = false;
                CheckBox7.Checked = true;
                CheckBox8.Enabled = false;
                CheckBox8.Checked = true;
                CheckBox9.Enabled = false;
                CheckBox9.Checked = true;
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
                CheckBox9.Enabled = true;
                CheckBox9.Checked = false;
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

                        GridView1.Columns[i].HeaderStyle.CssClass = GridView1.Columns[i].HeaderStyle.RegisteredCssClass;
                        GridView1.Columns[i].ItemStyle.CssClass = GridView1.Columns[i].ItemStyle.RegisteredCssClass;
                        GridView1.Columns[i].FooterStyle.CssClass = GridView1.Columns[i].FooterStyle.RegisteredCssClass;
                    }
                }
            }
        }
        protected void ButtonSCHLL_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_WarehouseOUT_LL_Auto.aspx?FLAG=PUSHBLUE");
        }
    }
    
}
