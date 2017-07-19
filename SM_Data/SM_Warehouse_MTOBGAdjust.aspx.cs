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
using System.Text;
using System.Drawing;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_MTOBGAdjust : System.Web.UI.Page
    {
        float twn = 0;
        Int32 twqn = 0;
        float tan = 0;
        Int32 taqn = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////GetPTCTo();
                GetDep();                
                GetStaff();
                initial();      //初始化调拨单
                LabelMessage.Text= "加载成功！";
            }
        }

        protected void GetPTCTo()
        {
            string sql = "SELECT DISTINCT a.TSA_ID AS SCZH,a.TSA_ID+'|'+b.PJ_NAME+a.TSA_ENGNAME AS ZHNM " +
                "FROM TBPM_TCTSASSGN a INNER JOIN TBPM_PJINFO b ON a.TSA_PJID=b.PJ_ID " +
                "WHERE CHARINDEX('-',a.TSA_ID)>0";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DataRow row = dt.NewRow();
            row["ZHNM"] = "备库";
            row["SCZH"] = "备库";
            dt.Rows.InsertAt(row, 0);
            DataRow row2 = dt.NewRow();
            row2["ZHNM"] = "--请选择--";
            row2["SCZH"] = "No";
            dt.Rows.InsertAt(row2, 0);
            DropDownListPTCTo.DataTextField = "ZHNM";
            DropDownListPTCTo.DataValueField = "SCZH";
            DropDownListPTCTo.DataSource = dt;
            DropDownListPTCTo.DataBind();
        }
        //部门
        protected void GetDep()
        {
            string sql = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListDep.DataTextField = "DEP_NAME";
            DropDownListDep.DataValueField = "DEP_CODE";
            DropDownListDep.DataSource = dt;
            DropDownListDep.DataBind();
        }
        //员工
        protected void GetStaff()
        {
            string sql = "SELECT DISTINCT ST_CODE,ST_NAME FROM TBDS_STAFFINFO";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListPlaner.DataTextField = "ST_NAME";
            DropDownListPlaner.DataValueField = "ST_CODE";
            DropDownListPlaner.DataSource = dt;
            DropDownListPlaner.DataBind();
        }

        //页面初始化
        protected void initial()
        {
            string code = Request.QueryString["ID"].ToString();

            string flag = Request.QueryString["FLAG"].ToString();

            if (flag == "PUSHBGMTO")
            {
                LabelCode.Text = generateCode();

                TextBoxDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                string sql = "insert into TBWS_MTOCODE (MTO_CODE) VALUES ('" + LabelCode.Text + "')";

                DBCallCommon.ExeSqlText(sql);
                LabelState.Text = "0";
                //ClosingAccountDate(TextBoxDate.Text.Trim());
                TextBoxComment.Text = "";
                LabelDoc.Text = Session["UserName"].ToString();
                LabelDocCode.Text = Session["UserID"].ToString();
                LabelVerifier.Text = "";
                LabelVerifierCode.Text = "";
                LabelApproveDate.Text = "";

                sql = "SELECT UniqueID='',SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,Length AS Length," +
                    "Width AS Width,LotNumber AS LotNumber,PlanMode AS PlanMode,PTC AS PTCFrom," +
                    "WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,LocationCode AS PositionCode," +
                    "Location AS Position,Unit AS Unit,cast(Number as float) AS WN,cast(SupportNumber as float) AS WQN,PTCTO='--请选择--',cast(Number as float) AS AdjN," +
                    "cast(SupportNumber as float) AS AdjQN,OrderCode AS OrderID,Note AS Note " +
                    "FROM View_SM_Storage WHERE PTC IN (SELECT MP_CHPTCODE FROM TBPC_MPTEMPCHANGE WHERE MP_OPESTATE='BGMTO" + Session["UserID"].ToString() + "')";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                GridView1.DataSource = dt;
                GridView1.DataBind();

                //sql = "update TBPC_MPTEMPCHANGE SET MP_OPESTATE='null' WHERE MP_OPESTATE='BGMTO" + Session["UserID"].ToString() + "'";

                //DBCallCommon.ExeSqlText(sql);

            }

            if (flag == "OPEN")
            {
                string sql = "SELECT TOP 1 MTOCode AS Code,Date AS Date,TargetCode AS PTCToCode," +
                    "DepCode AS DepCode,Dep AS DepName,PlanerCode AS PlanerCode," +
                    "Planer AS Planer,DocCode AS DocCode,Doc AS Doc,VerifierCode AS VerifierCode,Verifier AS Verifier," +
                    "VerifyDate AS ApproveDate,TotalState AS State,TotalNote AS Comment FROM View_SM_MTO WHERE MTOCode='" + code + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    LabelCode.Text = code;
                    LabelState.Text = dr["State"].ToString();
                    TextBoxDate.Text = dr["Date"].ToString();
                    try { DropDownListPTCTo.Items.FindByValue(dr["PTCToCode"].ToString()).Selected = true; }
                    catch { }
                    TextBoxComment.Text = dr["Comment"].ToString();

                    try { DropDownListPlaner.Items.FindByValue(dr["PlanerCode"].ToString()).Selected = true; }
                    catch { }
                    try { DropDownListDep.Items.FindByValue(dr["DepCode"].ToString()).Selected = true; }
                    catch { }
                    LabelDoc.Text = dr["Doc"].ToString();
                    LabelDocCode.Text = dr["DocCode"].ToString();
                    LabelVerifier.Text = dr["Verifier"].ToString();
                    LabelVerifierCode.Text = dr["VerifierCode"].ToString();
                    LabelApproveDate.Text = dr["ApproveDate"].ToString();
                }
                dr.Close();

                sql = "SELECT UniqueCode AS UniqueID,SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Standard AS MaterialStandard,Fixed AS Fixed,Length AS Length," +
                    "Width AS Width,Attribute AS Attribute,GB AS GB,LotNumber AS LotNumber," +
                    "PTCFrom AS PTCFrom,WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,LocationCode AS PositionCode,Location AS Position," +
                    "Unit AS Unit,KTNUM AS WN,KTFZNUM AS WQN,TZNUM AS AdjN,TZFZNUM AS AdjQN,PTCTo AS PTCTo," +
                    "PlanMode AS PlanMode,DetailState AS State,OrderCode AS OrderID,DetailNote AS Note FROM View_SM_MTO WHERE MTOCode='" + code + "'";
                DataTable tb = new DataTable();
                tb = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = tb;
                GridView1.DataBind();
                if (LabelState.Text == "1")
                {
                    Append.Enabled = true;
                    Delete.Enabled = true;
                    Save.Enabled = true; ;
                    Verifiy.Enabled = true;
                    AntiVerify.Enabled = false;
                    DeleteBill.Enabled = true;
                    Print.Enabled = false;
                    Related.Enabled = true;
                    ImageVerify.Visible = false;
                }
                if(LabelState.Text == "2")
                {
                    Append.Enabled = false;
                    Delete.Enabled = false;
                    Save.Enabled = false;
                    Verifiy.Enabled = false;
                    AntiVerify.Enabled = true;
                    DeleteBill.Enabled = false;
                    Print.Enabled = true;
                    Related.Enabled = true;
                    ImageVerify.Visible = true;
                }
            }
        }

        //生成MTO单号
        protected string generateCode()
        {
            string Code = "";
            string sql = "SELECT MAX(MTO_CODE) AS MaxCode FROM TBWS_MTO WHERE LEN(MTO_CODE)=10";
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                twn += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "WN"));
                twqn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "WQN"));
                tan += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "AdjN"));
                taqn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "AdjQN"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.Cells[15].FindControl("LabelTotalWN")).Text = twn.ToString();
                ((Label)e.Row.Cells[16].FindControl("LabelTotalWQN")).Text = twqn.ToString();
                ((Label)e.Row.Cells[19].FindControl("LabelTotalAdjN")).Text = tan.ToString();
                ((Label)e.Row.Cells[20].FindControl("LabelTotalAdjQN")).Text = taqn.ToString();
            }
        }

        //获取当前GridView1中的数据，根据表格内容调整
        protected DataTable getDataFromGridView()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("UniqueID", System.Type.GetType("System.String")); 
            tb.Columns.Add("SQCODE", System.Type.GetType("System.String")); 
            tb.Columns.Add("MaterialCode", System.Type.GetType("System.String"));
            tb.Columns.Add("MaterialName", System.Type.GetType("System.String"));
            tb.Columns.Add("Attribute", System.Type.GetType("System.String"));
            tb.Columns.Add("GB", System.Type.GetType("System.String"));
            tb.Columns.Add("MaterialStandard", System.Type.GetType("System.String"));
            tb.Columns.Add("Fixed", System.Type.GetType("System.String"));
            tb.Columns.Add("Length", System.Type.GetType("System.String"));
            tb.Columns.Add("Width", System.Type.GetType("System.String"));
            tb.Columns.Add("LotNumber", System.Type.GetType("System.String"));
            tb.Columns.Add("PTCFrom", System.Type.GetType("System.String"));
            tb.Columns.Add("Warehouse", System.Type.GetType("System.String"));
            tb.Columns.Add("WarehouseCode", System.Type.GetType("System.String"));
            tb.Columns.Add("Position", System.Type.GetType("System.String"));
            tb.Columns.Add("PositionCode", System.Type.GetType("System.String"));
            tb.Columns.Add("Unit", System.Type.GetType("System.String"));
            tb.Columns.Add("WN", System.Type.GetType("System.String"));
            tb.Columns.Add("WQN", System.Type.GetType("System.String"));
            tb.Columns.Add("PTCTo", System.Type.GetType("System.String"));
            tb.Columns.Add("PlanMode", System.Type.GetType("System.String"));
            tb.Columns.Add("AdjN", System.Type.GetType("System.String"));
            tb.Columns.Add("AdjQN", System.Type.GetType("System.String"));
            tb.Columns.Add("Note", System.Type.GetType("System.String"));
            tb.Columns.Add("OrderID", System.Type.GetType("System.String")); 
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                DataRow newRow = tb.NewRow();
                newRow["UniqueID"] = ((Label)gRow.FindControl("LabelUniqueID")).Text;  
                newRow["SQCODE"] = ((Label)gRow.FindControl("LabelSQCODE")).Text;                
                newRow["MaterialCode"] = ((Label)gRow.FindControl("LabelMaterialCode")).Text;
                newRow["MaterialName"] = ((Label)gRow.FindControl("LabelMaterialName")).Text;
                newRow["Attribute"] = ((Label)gRow.FindControl("LabelAttribute")).Text;
                newRow["GB"] = ((Label)gRow.FindControl("LabelGB")).Text;
                newRow["MaterialStandard"] = ((Label)gRow.FindControl("LabelMaterialStandard")).Text;
                newRow["Fixed"] = ((Label)gRow.FindControl("LabelFixed")).Text;
                newRow["Length"] = ((Label)gRow.FindControl("LabelLength")).Text;
                newRow["Width"] = ((Label)gRow.FindControl("LabelWidth")).Text;
                newRow["LotNumber"] = ((Label)gRow.FindControl("LabelLotNumber")).Text;
                newRow["PTCFrom"] = ((Label)gRow.FindControl("LabelPTCFrom")).Text;
                newRow["Warehouse"] = ((Label)gRow.FindControl("LabelWarehouse")).Text;
                newRow["WarehouseCode"] = ((Label)gRow.FindControl("LabelWarehouseCode")).Text;
                newRow["Position"] = ((Label)gRow.FindControl("LabelPosition")).Text;
                newRow["PositionCode"] = ((Label)gRow.FindControl("LabelPositionCode")).Text;
                newRow["Unit"] = ((Label)gRow.FindControl("LabelUnit")).Text;
                newRow["WN"] = ((Label)gRow.FindControl("LabelWN")).Text;
                newRow["WQN"] = ((Label)gRow.FindControl("LabelWQN")).Text;
                newRow["PTCTo"] = ((Label)gRow.FindControl("LabelPTCTo")).Text;
                newRow["PlanMode"] = ((Label)gRow.FindControl("LabelPlanMode")).Text;
                newRow["AdjN"] = ((TextBox)gRow.FindControl("TextBoxAdjN")).Text;
                newRow["AdjQN"] = ((TextBox)gRow.FindControl("TextBoxAdjQN")).Text;
                newRow["Note"] = ((TextBox)gRow.FindControl("TextBoxNote")).Text;
                newRow["OrderID"] = ((Label)gRow.FindControl("LabelOrderID")).Text;
                tb.Rows.Add(newRow);
            }
            return tb;
        }

        protected void Append_Click(object sender, EventArgs e)
        {
            DataTable tb;
            tb = getDataFromGridView();
            string sql = "SELECT UniqueID='',SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,CAST(Length AS CHAR(50)) AS Length," +
                "CAST(Width AS CHAR(50)) AS Width,LotNumber AS LotNumber,PTC AS PTCFrom,WarehouseCode AS WarehouseCode," +
                "Warehouse AS Warehouse,LocationCode AS PositionCode,Location AS Position,Unit AS Unit,CAST(Number AS CHAR(50)) AS WN," +
                "CAST(SupportNumber AS CHAR(50)) AS WQN,PTCTO='--请选择--',PlanMode AS PlanMode,CAST(Number AS CHAR(50)) AS AdjN," +
                "CAST(SupportNumber AS CHAR(50)) AS AdjQN,Note AS Note,OrderCode AS OrderID " +
                "FROM View_SM_Storage WHERE State='APPENDMTO" + Session["UserID"].ToString() + "'";
            DataTable temp = new DataTable();
            temp.Columns.Add("UniqueID", System.Type.GetType("System.String"));
            temp.Columns.Add("SQCODE", System.Type.GetType("System.String"));
            temp.Columns.Add("MaterialCode", System.Type.GetType("System.String"));
            temp.Columns.Add("MaterialName", System.Type.GetType("System.String"));
            temp.Columns.Add("Attribute", System.Type.GetType("System.String"));
            temp.Columns.Add("GB", System.Type.GetType("System.String"));
            temp.Columns.Add("MaterialStandard", System.Type.GetType("System.String"));
            temp.Columns.Add("Length", System.Type.GetType("System.String"));
            temp.Columns.Add("Width", System.Type.GetType("System.String"));
            temp.Columns.Add("LotNumber", System.Type.GetType("System.String"));
            temp.Columns.Add("PTCFrom", System.Type.GetType("System.String"));
            temp.Columns.Add("Warehouse", System.Type.GetType("System.String"));
            temp.Columns.Add("WarehouseCode", System.Type.GetType("System.String"));
            temp.Columns.Add("Position", System.Type.GetType("System.String"));
            temp.Columns.Add("PositionCode", System.Type.GetType("System.String"));
            temp.Columns.Add("Unit", System.Type.GetType("System.String"));
            temp.Columns.Add("WN", System.Type.GetType("System.String"));
            temp.Columns.Add("WQN", System.Type.GetType("System.String"));
            temp.Columns.Add("PTCTo", System.Type.GetType("System.String"));
            temp.Columns.Add("PlanMode", System.Type.GetType("System.String"));
            temp.Columns.Add("AdjN", System.Type.GetType("System.String"));
            temp.Columns.Add("AdjQN", System.Type.GetType("System.String"));
            temp.Columns.Add("Note", System.Type.GetType("System.String"));
            temp.Columns.Add("OrderID", System.Type.GetType("System.String"));
            temp = DBCallCommon.GetDTUsingSqlText(sql);
            tb.Merge(temp);
            GridView1.DataSource = tb;
            GridView1.DataBind();
            sql = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='APPENDMTO" + Session["UserID"].ToString() + "'";
            DBCallCommon.ExeSqlText(sql);
            LabelMessage.Text = "追加成功！";
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
            LabelMessage.Text = "删除成功！";
        }

        //MTO调整单保存
        protected void Save_Click(object sender, EventArgs e)
        {
            List<string> sqllist = new List<string>();
            string sql = "";

            string Code = LabelCode.Text;
            string Date = TextBoxDate.Text;
            string TargetCode = DropDownListPTCTo.SelectedValue;
            string Comment = TextBoxComment.Text;
            string PlanerCode = DropDownListPlaner.SelectedValue;
            string DepCode = DropDownListDep.SelectedValue;
            string DocCode = LabelDocCode.Text;
            string VerifierCode = LabelVerifierCode.Text;
            string ApproveDate = LabelApproveDate.Text;

            sql = "DELETE FROM TBWS_MTO WHERE MTO_CODE='" + Code + "'";
            sqllist.Add(sql);
            sql = "INSERT INTO TBWS_MTO(MTO_CODE,MTO_DATE,MTO_TARGETID," +
                "MTO_DEP,MTO_PLANER,MTO_DOC,MTO_VERIFIER,MTO_VERIFYDATE," +
                "MTO_STATE,MTO_NOTE) VALUES('" +Code + "','" + 
                Date + "','" + TargetCode + "','" + DepCode + "','" + PlanerCode + "','" + DocCode + "','" + 
                VerifierCode + "','" + ApproveDate + "','1','" + Comment + "')";
            sqllist.Add(sql);
            sql = "DELETE FROM TBWS_MTODETAIL WHERE MTO_CODE='" + Code  + "'";
            sqllist.Add(sql);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string UniqueID = Code + (i+1).ToString();
                string SQCODE = ((Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;
                string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text;
                string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;
                float Length = 0;
                try { Length = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelLength")).Text); }
                catch { }
                float Width = 0;
                try { Width = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelWidth")).Text); }
                catch { }
                string LotNumber = ((Label)this.GridView1.Rows[i].FindControl("LabelLotNumber")).Text;
                string PTCFrom = ((Label)this.GridView1.Rows[i].FindControl("LabelPTCFrom")).Text;
                string WarehouseCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;
                string PositionCode = ((Label)this.GridView1.Rows[i].FindControl("LabelPositionCode")).Text;
                float WN = 0;
                try { WN = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelWN")).Text); }
                catch { }
                Int32 WQN = 0;
                try { WQN = Convert.ToInt32(((Label)this.GridView1.Rows[i].FindControl("LabelWQN")).Text); }
                catch { }
                string PTCTo = ((Label)this.GridView1.Rows[i].FindControl("LabelPTCTo")).Text;
                string PlanMode = ((Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text; 
                float AdjN = 0;
                try { AdjN = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxAdjN")).Text); }
                catch { }
                Int32 AdjQN = 0;
                try { AdjQN = Convert.ToInt32(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxAdjQN")).Text); }
                catch { }
                string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                string Note = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxNote")).Text;
                sql = "INSERT INTO TBWS_MTODETAIL(MTO_UNIQUEID,MTO_CODE,MTO_SQCODE,MTO_MARID," +
                    "MTO_FIXED,MTO_LENGTH,MTO_WIDTH,MTO_PCODE,MTO_FROMPTCODE," +
                    "MTO_WAREHOUSE,MTO_LOCATION,MTO_KTNUM,MTO_KTFZNUM,MTO_TOPTCODE,MTO_TZNUM," +
                    "MTO_TZFZNUM,MTO_PMODE,MTO_ORDERID,MTO_NOTE,MTO_STATE) VALUES('" + UniqueID + "','" +
                    Code + "','" + SQCODE + "','" + MaterialCode + "','" + Fixed + "','" +
                    Length + "','" + Width + "','" + LotNumber + "','" + PTCFrom + "','" + WarehouseCode + "','" + PositionCode + "','" +
                    WN + "','" + WQN + "','" + PTCTo + "','" + AdjN + "','" +  AdjQN + "','" + PlanMode + "','" + 
                    OrderID + "','" + Note + "','')";
                sqllist.Add(sql);
            }
            DBCallCommon.ExecuteTrans(sqllist);
            sqllist.Clear();
            LabelState.Text = "1";
            LabelMessage.Text = "保存成功！";
        }

        protected void DropDownListPTCTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            generatePTCTo();
        }

       //自动生成到计划跟踪号
        protected void generatePTCTo()
        { 
            //根据选择的项目工程生成到计划跟踪号
            if ((DropDownListPTCTo.SelectedValue != "No") && (DropDownListPTCTo.SelectedValue != "备库"))
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((Label)GridView1.Rows[i].FindControl("LabelPTCTo")).Text = DropDownListPTCTo.SelectedValue + "_" + LabelCode.Text + "_" + (i + 1).ToString().PadLeft(3, '0');
                }
            }
            else if (DropDownListPTCTo.SelectedValue == "备库")
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((Label)GridView1.Rows[i].FindControl("LabelPTCTo")).Text = "备库";
                }
            }
            else
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((Label)GridView1.Rows[i].FindControl("LabelPTCTo")).Text = "--请选择--";
                }
            }
            LabelMessage.Text = "到计划跟踪号自动生成完毕";
        }

        protected void Verify_Click(object sender, EventArgs e)
        {
            List<string> sqllist = new List<string>();
            string sql = "";

            string Code = LabelCode.Text;
            string Date = TextBoxDate.Text;
            string TargetCode = DropDownListPTCTo.SelectedValue;
            string Comment = TextBoxComment.Text;
            string PlanerCode = DropDownListPlaner.SelectedValue;
            string DepCode = DropDownListDep.SelectedValue;
            string DocCode = LabelDocCode.Text;
            string VerifierCode = LabelVerifierCode.Text;
            string ApproveDate = LabelApproveDate.Text;

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
                string UniqueID = Code + (i + 1).ToString();
                string SQCODE = ((Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;
                string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text;
                string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;
                float Length = 0;
                try { Length = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelLength")).Text); }
                catch { }
                float Width = 0;
                try { Width = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelWidth")).Text); }
                catch { }
                string LotNumber = ((Label)this.GridView1.Rows[i].FindControl("LabelLotNumber")).Text;
                string PTCFrom = ((Label)this.GridView1.Rows[i].FindControl("LabelPTCFrom")).Text;
                string WarehouseCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;
                string PositionCode = ((Label)this.GridView1.Rows[i].FindControl("LabelPositionCode")).Text;
                float WN = 0;
                try { WN = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelWN")).Text); }
                catch { }
                Int32 WQN = 0;
                try { WQN = Convert.ToInt32(((Label)this.GridView1.Rows[i].FindControl("LabelWQN")).Text); }
                catch { }
                string PTCTo = ((Label)this.GridView1.Rows[i].FindControl("LabelPTCTo")).Text;
                string PlanMode = ((Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text;
                float AdjN = 0;
                try { AdjN = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxAdjN")).Text); }
                catch { }
                Int32 AdjQN = 0;
                try { AdjQN = Convert.ToInt32(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxAdjQN")).Text); }
                catch { }
                string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                string Note = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxNote")).Text;
                sql = "INSERT INTO TBWS_MTODETAIL(MTO_UNIQUEID,MTO_CODE,MTO_SQCODE,MTO_MARID," +
                    "MTO_FIXED,MTO_LENGTH,MTO_WIDTH,MTO_PCODE,MTO_FROMPTCODE," +
                    "MTO_WAREHOUSE,MTO_LOCATION,MTO_KTNUM,MTO_KTFZNUM,MTO_TOPTCODE,MTO_TZNUM," +
                    "MTO_TZFZNUM,MTO_PMODE,MTO_ORDERID,MTO_NOTE,MTO_STATE) VALUES('" + UniqueID + "','" +
                    Code + "','" + SQCODE + "','" + MaterialCode + "','" + Fixed + "','" +
                    Length + "','" + Width + "','" + LotNumber + "','" + PTCFrom + "','" + WarehouseCode + "','" + PositionCode + "','" +
                    WN + "','" + WQN + "','" + PTCTo + "','" + AdjN + "','" + AdjQN + "','" + PlanMode + "','" +
                    OrderID + "','" + Note + "','')";
                sqllist.Add(sql);
            }
            DBCallCommon.ExecuteTrans(sqllist);
            sqllist.Clear();

            sql = DBCallCommon.GetStringValue("connectionStrings");
            SqlConnection con = new SqlConnection(sql);
            con.Open();
            SqlCommand cmd = new SqlCommand("MTO", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@MTOCode", SqlDbType.VarChar, 50);				//增加参数
            cmd.Parameters["@MTOCode"].Value = LabelCode.Text;							//为参数初始化
            cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;
            cmd.ExecuteNonQuery();
            con.Close();
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 0)
            {
                Response.Redirect("SM_Warehouse_MTOAdjust.aspx?FLAG=OPEN&&ID=" + Code);            
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
            {
                LabelMessage.Text = "无法通过审核：部分物料不存在！";
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
            {
                LabelMessage.Text = "无法通过审核：部分物料数量小于调整数量！";
            }
        }

        protected void AntiVerify_Click(object sender, EventArgs e)
        {
            if (LabelState.Text == "0")
            {
                LabelMessage.Text = "MTO数量调整单尚未保存无法反审！";
                return;
            }
            if (LabelState.Text == "1")
            {
                LabelMessage.Text = "MTO数量调整单尚未审核无法反审！";
                return;
            }
            if (LabelState.Text == "2")
            {
                string sql = DBCallCommon.GetStringValue("connectionStrings");
                SqlConnection con = new SqlConnection(sql);
                con.Open();
                SqlCommand cmd = new SqlCommand("AntiVerifyMTO", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@MTOCode", SqlDbType.VarChar, 50);				//增加参数
                cmd.Parameters["@MTOCode"].Value = LabelCode.Text;							//为参数初始化
                cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;
                cmd.ExecuteNonQuery();
                con.Close();
                if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 0)
                {
                    Response.Redirect("SM_Warehouse_MTOAdjust.aspx?FLAG=OPEN&&ID=" + LabelCode.Text);
                }
                if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
                {
                    LabelMessage.Text = "无法反审：部分物料不存在！";
                }
                if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
                {
                    LabelMessage.Text = "无法反审：部分物料数量小于调整数量！";
                }
            }
        }

        protected void DeleteBill_Click(object sender, EventArgs e)
        {
            if (LabelState.Text == "0")
            {
                LabelMessage.Text = "该MTO数量调整单尚未保存！";
                return;
            }
            if (LabelState.Text == "2")
            {
                LabelMessage.Text = "已审核MTO数量调整单不允许删除！";
                return;
            }
            if (LabelState.Text == "1")
            {
                List<string> sqllist = new List<string>();
                string sql = "DELETE FROM TBWS_MTO WHERE MTO_CODE='" + LabelCode.Text + "'";
                sqllist.Add(sql);
                sql = "DELETE FROM TBWS_MTODETAIL WHERE MTO_CODE='" + LabelCode.Text + "'";
                sqllist.Add(sql);
                DBCallCommon.ExecuteTrans(sqllist);
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>closewin();</script>");
            }
        }

        protected void Print_Click(object sender, EventArgs e)
        {

        }

        protected void Related_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked == true)
                {
                    string ptc = ((Label)GridView1.Rows[i].FindControl("LabelPTCFrom")).Text;
                    Response.Redirect("SM_Warehouse_RelatedDocument.aspx?PTC=" + ptc);
                    return;
                }
            }
            LabelMessage.Text = "请选择一条要查询的记录！";
        }

    }
}
