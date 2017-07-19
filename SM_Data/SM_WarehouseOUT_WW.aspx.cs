using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_WarehouseOUT_WW : System.Web.UI.Page
    {
        private double tdn = 0;
        private double trn = 0;
        private float tdqn = 0;
        private float trqn = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
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



        //发料人
        protected void GetStaff()
        {
            string sql = "SELECT DISTINCT ST_CODE,ST_NAME FROM TBDS_STAFFINFO WHERE ST_DEPID='07' AND ST_STATE='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListSender.DataValueField = "ST_CODE";
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
                //库存
                string sql = "SELECT UniqueID='',SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,Length AS DueLength,Length AS Length," +
                    "Width AS Width,LotNumber AS LotNumber,PlanMode AS PlanMode,PTC AS PTC,OrderCode AS OrderID," +
                    "WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,LocationCode AS PositionCode," +
                    "Location AS Position,Unit AS Unit,cast(Number as float) AS DN,cast(Number as float) AS RN,cast(SupportNumber as float) AS DQN,cast(SupportNumber as float) AS RQN," +
                    "Note AS Comment FROM View_SM_Storage WHERE State='OUTWW" + Session["UserID"].ToString() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                sql = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='OUTWW" + Session["UserID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);

                LabelCode.Text = generateCode();

                sql = "INSERT INTO TBWS_OUTCODE (OP_CODE,OP_BILLTYPE) VALUES ('" + LabelCode.Text + "','2')";

                DBCallCommon.ExeSqlText(sql);


                LabelState.Text = "0";
                InputColour.Value = "0";
                TextBoxDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                ClosingAccountDate(TextBoxDate.Text.Trim());

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
            if (flag == "PUSHRED")
            {
                //总表
                string sql = "SELECT TOP 1 Code AS Code,CompanyCode AS CompanyCode,Company AS Company," +
                    "ProcessType AS ProcessType,Date AS Date,TSAID AS SCZH,SenderCode,Sender,DocCode AS DocumentCode," +
                    "Doc AS Document,VerifierCode AS VerifierCode,Verifier AS Verifier,VerifierDate AS VerifierDate,ROB AS Colour,TotalState AS State" +
                    " FROM View_SM_OUTWW WHERE Code='" + code + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    LabelCode.Text = generateRedCode();
                    LabelState.Text = "0";
                    InputColour.Value = "1";
                    TextBoxDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    ClosingAccountDate(TextBoxDate.Text.Trim());

                    TextBoxCompany.Text = dr["Company"].ToString() + "|" + dr["CompanyCode"].ToString();

                    //try { DropDownListCompany.Items.FindByValue(dr["CompanyCode"].ToString()).Selected = true; }
                    //catch { }
                    try { DropDownListProcessType.Items.FindByValue(dr["ProcessType"].ToString()).Selected = true; }
                    catch { }
                    try { DropDownListSender.Items.FindByValue(dr["SenderCode"].ToString()).Selected = true; }
                    catch { }
                    TextBoxSCZH.Text = dr["SCZH"].ToString();
                    TextBoxSCZH.Enabled = false;
                    LabelDoc.Text = Session["UserName"].ToString();
                    LabelDocCode.Text = Session["UserID"].ToString();
                    LabelVerifier.Text = "";
                    LabelVerifierCode.Text = "";
                    LabelApproveDate.Text = "";
                }
                dr.Close();

                //明细

                sql = "SELECT UniqueCode AS UniqueID,SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,Length AS Length,Length AS DueLength,Width AS Width," +
                    "LotNumber AS LotNumber,Unit AS Unit,-cast(RealNumber as float) AS DN,-cast(RealNumber as float) AS RN," +
                    "-RealSupportNumber AS DQN,-RealSupportNumber AS RQN,UnitPrice AS UnitPrice," +
                    "Amount AS Amount,WarehouseCode AS WarehouseCode," +
                    "Warehouse AS Warehouse,LocationCode AS PositionCode,Location AS Position," +
                    "PlanMode AS PlanMode,PTC AS PTC,OrderCode AS OrderID," +
                    "DetailNote AS Comment FROM View_SM_OUTWW WHERE DetailState='REDWW" + Session["UserID"].ToString() + "' AND Code='" + code + "'";
                DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = tb;
                GridView1.DataBind();
                sql = "UPDATE TBWS_OUTDETAIL SET OP_STATE='' WHERE OP_STATE='REDWW" + Session["UserID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
                ImageRed.Visible = true;
                TextBoxCompany.Enabled = false;
                //DropDownListCompany.Enabled = false;
                DropDownListProcessType.Enabled = false;
                Append.Visible = false;
                Split.Visible = true;
                Delete.Visible = true;
                Save.Visible = true;
                Verify.Visible = true;
                DeleteBill.Visible = true;
                AntiVerify.Visible = false;
                PushRed.Visible = false;
                PushRed.Visible = false;
                SumPrint.Visible = false;
                AdjustLenWid.Visible = true;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {                    
                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxLotNumber")).Enabled = true;
                }

            }
            if (flag == "OPEN")
            {
                string sql = "SELECT Top 1 id as TrueCode, Code AS Code,CompanyCode AS CompanyCode,Company AS Company," +
                    "ProcessType AS ProcessType,Date AS Date,TSAID AS SCZH,SenderCode,Sender,DocCode AS DocumentCode," +
                    "Doc AS Document,VerifierCode AS VerifierCode,Verifier AS Verifier,left(VerifierDate,10) AS VerifierDate,ROB AS Colour,TotalState AS State" +
                    " FROM View_SM_OUTWW WHERE Code='" + code + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    LabelCode.Text = dr["Code"].ToString();

                    LabelTrueCode.Text = dr["TrueCode"].ToString();

                    LabelState.Text = dr["State"].ToString();
                    InputColour.Value = dr["Colour"].ToString();
                    TextBoxDate.Text = dr["Date"].ToString();

                    if (TextBoxDate.Text.Trim() == string.Empty)
                    {
                        TextBoxDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    }

                    ClosingAccountDate(TextBoxDate.Text.Trim());

                    TextBoxCompany.Text = dr["Company"].ToString() + "|" + dr["CompanyCode"].ToString();
                    //try { DropDownListCompany.Items.FindByValue(dr["CompanyCode"].ToString()).Selected = true; }
                    //catch { }
                    try { DropDownListProcessType.Items.FindByValue(dr["ProcessType"].ToString()).Selected = true; }
                    catch { }
                    try { DropDownListSender.Items.FindByValue(dr["SenderCode"].ToString()).Selected = true; }
                    catch { }
                    TextBoxSCZH.Text = dr["SCZH"].ToString();
                    LabelDoc.Text = dr["Document"].ToString();
                    LabelDocCode.Text = dr["DocumentCode"].ToString();
                    LabelVerifier.Text = dr["Verifier"].ToString();
                    LabelVerifierCode.Text = dr["VerifierCode"].ToString();
                    LabelApproveDate.Text = dr["VerifierDate"].ToString();//审核日期
                }
                dr.Close();

                sql = "SELECT UniqueCode AS UniqueID,SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,DueLength,Length AS Length,Width AS Width," +
                    "LotNumber AS LotNumber,Unit AS Unit,cast(DueNumber as float) AS DN,cast(RealNumber as float) AS RN,cast(DueSupportNumber as float) AS DQN,cast(RealSupportNumber as float) AS RQN," +
                    "cast(UnitPrice as float) AS UnitPrice,cast(Amount as float) AS Amount,WarehouseCode AS WarehouseCode," +
                    "Warehouse AS Warehouse,LocationCode AS PositionCode,Location AS Position," +
                    "PlanMode AS PlanMode,PTC AS PTC,OrderCode AS OrderID,DetailNote AS Comment" +
                    " FROM View_SM_OUTWW WHERE Code='" + code + "'";

                DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = tb;
                GridView1.DataBind();

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
                    if (InputColour.Value == "1")
                    {
                        Append.Visible = false;
                        AdjustLenWid.Visible = true;
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            ((TextBox)GridView1.Rows[i].FindControl("TextBoxLotNumber")).Enabled = true;
                        }
                    }
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
                tdn += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DN"));//即使库存数量
                trn += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "RN"));//实发数量
                tdqn += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "DQN"));//库存张
                trqn += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "RQN"));//应发张
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.Cells[11].FindControl("LabelTotalDN")).Text = Math.Round(tdn,4).ToString();
                ((Label)e.Row.Cells[15].FindControl("LabelTotalRN")).Text = Math.Round(trn, 4).ToString();
                ((Label)e.Row.Cells[11].FindControl("LabelTotalDQN")).Text = tdqn.ToString();
                ((Label)e.Row.Cells[15].FindControl("LabelTotalRQN")).Text = trqn.ToString();
            }
        }

        protected string generateCode()
        {
            string sql = "SELECT MAX(OP_CODE) AS MaxCode FROM TBWS_OUTCODE WHERE LEN(OP_CODE)=10 AND OP_BILLTYPE='2'";
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
                return "WW00000001";
            }
            else
            {
                int tempnum = Convert.ToInt32((code.Substring(2, 8)));
                tempnum++;
                code = "WW" + tempnum.ToString().PadLeft(8, '0');
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
            tb.Columns.Add("DueLength", System.Type.GetType("System.String"));
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
                row["DueLength"] = ((Label)GRow.FindControl("labellength")).Text;
                row["Length"] = ((TextBox)GRow.FindControl("TextBoxchangeLength")).Text;

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
                tb.Rows.Add(row);
            }
            return tb;
        }

        protected void Append_Click(object sender, EventArgs e)
        {
            DataTable dt = getDataFromGridView();
            string sql = "SELECT UniqueID='',SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed," +
                    "CAST(Length AS VARCHAR(50)) AS Length,CAST(Length AS VARCHAR(50)) AS DueLength,CAST(Width  AS VARCHAR(50)) AS Width," +
                    "LotNumber AS LotNumber,PlanMode AS PlanMode,PTC AS PTC,OrderCode AS OrderID," +
                    "WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,LocationCode AS PositionCode," +
                    "Location AS Position,Unit AS Unit," +
                    "CAST(Number AS VARCHAR(50)) AS DN,CAST(Number AS VARCHAR(50)) AS RN," +
                    "CAST(SupportNumber AS VARCHAR(50)) AS DQN,CAST(SupportNumber AS VARCHAR(50)) AS RQN," +
                    "Note AS Comment " +
                    "FROM View_SM_Storage WHERE State='APPENDOUTWW" + Session["UserID"].ToString() + "'";
            dt.Merge(DBCallCommon.GetDTUsingSqlText(sql));
            GridView1.DataSource = dt;
            GridView1.DataBind();
            sql = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='APPENDOUTWW" + Session["UserID"].ToString() + "'";
            DBCallCommon.ExeSqlText(sql);
            LabelMessage.Text = "追加成功";
            CheckSQCODE();//重复计划跟踪号颜色提醒
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
            tb.Columns.Add("DueLength", System.Type.GetType("System.String"));
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
                                dn = Convert.ToSingle(((Label)GRow.FindControl("LabelDN")).Text);
                                rn = Convert.ToSingle(((TextBox)GRow.FindControl("TextBoxRN")).Text);
                                dqn = Convert.ToInt32(((Label)GRow.FindControl("LabelDQN")).Text);
                                rqn = Convert.ToInt32(((TextBox)GRow.FindControl("TextBoxRQN")).Text);
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
                                //row["LotNumber"] = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text;
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
                                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                                row["DueLength"] = ((Label)GRow.FindControl("labellength")).Text;
                                row["Length"] = ((TextBox)GRow.FindControl("TextBoxchangeLength")).Text;

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
                                row["LotNumber"] = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text;
                                if (LabelCode.Text.Contains('R')) //为红单审核是相当于入库，批号自动生成，以免在库存汇总
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
                                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                                row["DueLength"] = ((Label)GRow.FindControl("labellength")).Text;
                                row["Length"] = ((TextBox)GRow.FindControl("TextBoxchangeLength")).Text;
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
                        row["DueLength"] = ((Label)GRow.FindControl("labellength")).Text;
                        row["Length"] = ((TextBox)GRow.FindControl("TextBoxchangeLength")).Text;
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
                                row["LotNumber"] = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text;
                                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                                row["DueLength"] = ((Label)GRow.FindControl("labellength")).Text;
                                row["Length"] = ((TextBox)GRow.FindControl("TextBoxchangeLength")).Text;
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
                                row["LotNumber"] = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text;
                                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                                row["DueLength"] = ((Label)GRow.FindControl("labellength")).Text;
                                row["Length"] = ((TextBox)GRow.FindControl("TextBoxchangeLength")).Text;
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
                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxLotNumber")).Enabled = true;
                }
            }
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
            if (LabelCode.Text.Contains('R')) //为红单审核是相当于入库，可以修改仓库
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {                    
                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxLotNumber")).Enabled = true;
                }
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
                //此处是保存操作
                List<string> sqllist = new List<string>();
                string sql = "";

                string Code = LabelCode.Text;
                string State = LabelState.Text;
                string Colour = InputColour.Value;
                string Date = TextBoxDate.Text;
                string  Companyall = TextBoxCompany.Text.Trim().ToString();
                string CompanyCode = "";
                if (Companyall != "")
                { 
                    CompanyCode = TextBoxCompany.Text.ToString().Split('|')[1]; 
                }             

                string ProcessType = DropDownListProcessType.SelectedValue;
                string SCZH = TextBoxSCZH.Text;

                string DocCode = LabelDocCode.Text;//制单人

                string SendClerkCode = DropDownListSender.SelectedValue;//发料人

                string VerifierCode = LabelVerifierCode.Text;//审核人

                string ApproveDate = LabelApproveDate.Text;//审核日期

                sql = "select count(*) from TBWS_OUT where OP_CODE='" + Code + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows[0][0].ToString() == "0")
                {
                    sql = "exec ResetSeed @tablename=TBWS_OUT";
                    sqllist.Add(sql);

                    sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_COMPANY,OP_PROCESSTYPE,OP_DATE,OP_TSAID," +
                        "OP_SENDER,OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
                        "OP_STATE,OP_BILLTYPE) VALUES('" + Code + "','" + CompanyCode + "','" + ProcessType + "','" + Date + "','" + SCZH + "','" +
                         SendClerkCode + "','" + DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','" +
                        Colour + "','1','2')";
                    sqllist.Add(sql);
                }
                else
                {
                    sql = "update TBWS_OUT set OP_COMPANY='" + CompanyCode + "',OP_PROCESSTYPE='" + ProcessType + "',OP_DATE='" + Date + "',OP_TSAID='" + SCZH + "',";
                    sql += "OP_SENDER='" + SendClerkCode + "',OP_DOC='" + DocCode + "',OP_VERIFIER='" + VerifierCode + "',OP_VERIFYDATE='" + ApproveDate + "',OP_HSFLAG='0',";
                    sql += "OP_ROB='" + Colour + "',OP_STATE='1',OP_BILLTYPE='2',OP_RealTime=convert(varchar(50),getdate(),120)  where OP_CODE='" + Code + "'";

                    sqllist.Add(sql);

                }



                sql = "DELETE FROM TBWS_OUTDETAIL WHERE OP_CODE='" + Code + "'";
                sqllist.Add(sql);
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    string UniqueID = Code + (i + 1).ToString();
                    string SQCODE = ((Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;
                    string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text;
                    string LotNumber = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLotNumber")).Text.Trim();
                    string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;
                    float DueLength = 0;
                    try { DueLength = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("labellength")).Text); }
                    catch { DueLength = 0; }
                    float Length = 0;
                    try { Length = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxchangeLength")).Text); }
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
                    string PlanMode = ((Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text;
                    string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;
                    string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                    string Note = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;
                    string WarehouseOutCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;
                    string PositionCode = ((Label)this.GridView1.Rows[i].FindControl("LabelPositionCode")).Text;
                    sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                        "OP_FIXED,OP_DUELENGTH,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                        "OP_UPRICE,OP_AMOUNT,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                        "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE) VALUES('" + Code + "','" + UniqueID + "','" + SQCODE + "','" +
                        MaterialCode + "','" +
                        Fixed + "','" + DueLength + "','" + Length + "','" +
                        Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','0','0','" +
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

                string State = LabelState.Text;
                string Colour = InputColour.Value;
                string Date = TextBoxDate.Text;
                string CompanyCode = TextBoxCompany.Text.Trim();
                if (CompanyCode != "")
                {
                    CompanyCode = TextBoxCompany.Text.ToString().Split('|')[1];
 
                }
                string ProcessType = DropDownListProcessType.SelectedValue;
                string SCZH = TextBoxSCZH.Text;

                string DocCode = LabelDocCode.Text;

                string SendClerkCode = DropDownListSender.SelectedValue;//发料人

                string VerifierCode = Session["UserID"].ToString();
                if ((SendClerkCode == DocCode) && (DocCode == VerifierCode)) //制单人，审核人，发料人不能相同
                {
                    LabelMessage.Text = "制单人，审核人，发料人不能相同！";

                    string script = @"alert('制单人，审核人，发料人不能相同！');";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    return;
                }

                ClosingAccountDate(DateTime.Now.ToString("yyyy-MM-dd"));//获取系统封账时间

                string ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


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

                ////在存储过程中修改状态

                //sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_COMPANY,OP_PROCESSTYPE,OP_DATE,OP_TSAID," +
                //    "OP_SENDER,OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
                //    "OP_STATE,OP_BILLTYPE,OP_RealTime) VALUES('" + Code + "','" + CompanyCode + "','" + ProcessType + "','" + ApproveDate.Substring(0,10) + "','" + SCZH + "','" +
                //     SendClerkCode + "','" + DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','" +
                //    Colour + "','1','2',convert(varchar(50),getdate(),120))";

                //sqllist.Add(sql);

                sql = "SELECT COUNT(*) FROM TBWS_OUT WHERE OP_CODE='" + Code + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows[0][0].ToString() == "0")
                {
                    sql = "exec ResetSeed @tablename=TBWS_OUT";

                    sqllist.Add(sql);

                    sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_COMPANY,OP_PROCESSTYPE,OP_DATE,OP_TSAID," +
                        "OP_SENDER,OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
                        "OP_STATE,OP_BILLTYPE,OP_RealTime) VALUES('" + Code + "','" + CompanyCode + "','" + ProcessType + "','" + Date + "','" + SCZH + "','" +
                         SendClerkCode + "','" + DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','" +
                        Colour + "','1','2',convert(varchar(50),getdate(),120))";

                    sqllist.Add(sql);

                }
                else
                {
                    sql = "update TBWS_OUT set OP_COMPANY='" + CompanyCode + "',OP_PROCESSTYPE='" + ProcessType + "',OP_DATE='" + Date + "',OP_TSAID='" + SCZH + "',";
                    sql += "OP_SENDER='" + SendClerkCode + "',OP_DOC='" + DocCode + "',OP_VERIFIER='" + VerifierCode + "',OP_VERIFYDATE='" + ApproveDate + "',OP_HSFLAG='0',";
                    sql += "OP_ROB='" + Colour + "',OP_STATE='1',OP_BILLTYPE='2',OP_RealTime=convert(varchar(50),getdate(),120)  where OP_CODE='" + Code + "'";

                    sqllist.Add(sql);

                }

                sql = "DELETE FROM TBWS_OUTDETAIL WHERE OP_CODE='" + Code + "'";
                sqllist.Add(sql);


                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    string UniqueID = Code + (i + 1).ToString();
                    string SQCODE = ((Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;
                    string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text;
                    string LotNumber = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLotNumber")).Text.Trim();
                    string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;

                    int DueLength = 0;
                    try { DueLength = Convert.ToInt32(Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("labellength")).Text)); }
                    catch { DueLength = 0; }

                    int Length = 0;
                    try { Length = Convert.ToInt32(Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxchangeLength")).Text)); }
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
                    string sqltext = "select count(*) from View_SM_PROJTEMP where PTCFrom='" + PTC + "' and state<='2' and  PTCFrom<>'备库' "; //待项目结转备库的物料不能出库
                    DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (Convert.ToInt32(dt1.Rows[0][0].ToString()) > 0)
                    {
                        LabelMessage.Text = "物料(" + PTC + ")在项目结转备库中待审批，现在不能出库此物料";
                        sqllist.Clear();
                        return;
                    }

                    string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                    string Note = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;
                    string WarehouseOutCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;
                    string PositionCode = ((Label)this.GridView1.Rows[i].FindControl("LabelPositionCode")).Text;

                    sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                       "OP_FIXED,OP_DUELENGTH,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                       "OP_UPRICE,OP_AMOUNT,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                       "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE) VALUES('" + Code + "','" + UniqueID + "','" + SQCODE + "','" +
                       MaterialCode + "','" +
                       Fixed + "','" + DueLength + "','" + Length + "','" +
                       Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','0','0','" +
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
                        Response.Redirect("SM_WarehouseOUT_WW.aspx?FLAG=OPEN&&ID=" + Code);
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
                    {
                        LabelMessage.Text = "审核未通过：部分库存物料不存在！";
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
                    {
                        LabelMessage.Text = "审核未通过：部分库存物料数量小于出库数量！";
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
                return false;
                
            }
            
        }
        protected void AdjustLenWid_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                ((TextBox)GridView1.Rows[i].FindControl("TextBoxchangeLength")).Enabled = true;
                ((TextBox)GridView1.Rows[i].FindControl("TextBoxWidth")).Enabled = true;
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
                return;
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
                    Response.Redirect("SM_WarehouseOUT_WW.aspx?FLAG=OPEN&&ID=" + LabelCode.Text);
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
                if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -3)
                {
                    LabelMessage.Text = "审核未通过：跨月单据不能反审！";
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
            string sql = "UPDATE TBWS_OUTDETAIL SET OP_STATE='' WHERE OP_STATE='REDWW" + Session["UserID"].ToString() + "'";

            sqllist.Add(sql);

            string uniqueid = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked == true)
                {
                    uniqueid = ((Label)GridView1.Rows[i].FindControl("LabelUniqueID")).Text;
                    sql = "UPDATE TBWS_OUTDETAIL SET OP_STATE='REDWW" + Session["UserID"].ToString() + "' WHERE OP_CODE='" + LabelCode.Text +
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
            Response.Redirect("SM_WarehouseOUT_WW.aspx?FLAG=PUSHRED&&ID=" + LabelCode.Text);
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
            Response.Redirect("SM_Warehouse_Query.aspx?FLAG=PUSHWWOUT");
        }

        protected void TextBoxSCZH_TextChanged(object sender, EventArgs e)
        {
            string engid = TextBoxSCZH.Text.Trim();

            //engid = engid.Split('-')[engid.Split('-').Length - 1];
            //string sql = "select count(*)  FROM TBPM_TCTSASSGN WHERE TSA_FATHERNODE='0' and TSA_ID='" + engid + "'";
            //DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            //if (Convert.ToInt32(dt.Rows[0][0]) == 0)
            //{
            //    TextBoxSCZH.Text = string.Empty;
            //}

            engid = engid.Split('|')[0];
            //string sql = "select TSA_PJNAME,TSA_ENGNAME,TSA_ID  FROM View_TM_TaskAssign WHERE TSA_FATHERNODE='0' and TSA_ID='" + engid + "'";
            //DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            //if (dt.Rows.Count > 0)
            //{
            //    TextBoxSCZH.Text = dt.Rows[0]["TSA_PJNAME"] + "-" + dt.Rows[0]["TSA_ENGNAME"] + "-" + dt.Rows[0]["TSA_ID"];
            //}
            //else
            //{
            TextBoxSCZH.Text = engid;
            //}


        }

        protected void TextBoxCompany_TextChanged(object sender, EventArgs e)
        {

            string Companyid = TextBoxCompany.Text.Trim().ToString();
            if (Companyid != "")
            {
                Companyid = Companyid.Split('|')[1];
            }             

            string sql = "select count(*) FROM TBCS_CUSUPINFO WHERE  CS_State='0' and CS_CODE='"+Companyid+"'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            if (Convert.ToInt32(dt.Rows[0][0]) == 0)
            {
                TextBoxCompany.Text = string.Empty;
            }
        }
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

        #region 升降序
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
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

        #endregion

    }
}
