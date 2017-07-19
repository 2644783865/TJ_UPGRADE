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
    public partial class SM_Warehouse_MTOAdjust : System.Web.UI.Page
    {
        float twn = 0;
        Int32 twqn = 0;
        float tan = 0;
        Int32 taqn = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //GetPTCTo();
                getWarehouse();
                GetStaff();
                initial();      //初始化调拨单
                LabelMessage.Text = "加载成功！";
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
            string objymd = string.Empty;

            string ymd = System.DateTime.Now.ToString("yyyyMMdd");
            //年
            string yy = ymd.Substring(0, 4);

            //月
            string mt = string.Empty;

            int m = Convert.ToInt16(ymd.Substring(4, 2));
            m = m + 1;
            if (m > 12)
            {
                m = 1;
                int y = Convert.ToInt32(yy);
                y = y + 1;
                yy = y.ToString();
            }
            if (m.ToString().Length < 2)
            {
                mt = "0" + m.ToString();
            }
            else
            {
                mt = m.ToString();
            }

            //返回值
            objymd = yy + "-" + mt + "-" + "01";

            return objymd;
        }

        //仓库
        protected void getWarehouse()
        {
            string sql = "SELECT DISTINCT WS_ID,WS_NAME FROM TBWS_WAREHOUSE WHERE WS_FATHERID<>'ROOT'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListWarehouse.DataTextField = "WS_NAME";
            DropDownListWarehouse.DataValueField = "WS_ID";
            DropDownListWarehouse.DataSource = dt;
            DropDownListWarehouse.DataBind();
            ListItem item = new ListItem("--请选择--", "0");
            DropDownListWarehouse.Items.Insert(0, item);
        }

        protected void DropDownListWarehouse_SelecedIndexChanged(object sender, EventArgs e)
        {
            bool IsSelect = true;

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if ((GridView1.Rows[i].FindControl("CheckBox1") as CheckBox).Checked)
                {
                    Label lb = (Label)GridView1.Rows[i].FindControl("LabelWarehouse");
                    lb.Text = DropDownListWarehouse.SelectedItem.Text;
                    lb = (Label)GridView1.Rows[i].FindControl("LabelWarehouseCode");
                    lb.Text = DropDownListWarehouse.SelectedValue;

                    /*
                     * 默认仓位为待查
                     */

                    //TextBox tb = (TextBox)GridView1.Rows[i].FindControl("TextBoxPosition");
                    //tb.Text = "待查";

                    //HtmlInputText hit = (HtmlInputText)GridView1.Rows[i].FindControl("InputPositionCode");
                    //hit.Value = "0";

                    IsSelect = false;
                }
            }
            if (IsSelect)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {

                    Label lb = (Label)GridView1.Rows[i].FindControl("LabelWarehouse");
                    lb.Text = DropDownListWarehouse.SelectedItem.Text;
                    lb = (Label)GridView1.Rows[i].FindControl("LabelWarehouseCode");
                    lb.Text = DropDownListWarehouse.SelectedValue;

                    /*
                     * 默认仓位为待查
                     */

                    //TextBox tb = (TextBox)GridView1.Rows[i].FindControl("TextBoxPosition");
                    //tb.Text = "待查";

                    //HtmlInputText hit = (HtmlInputText)GridView1.Rows[i].FindControl("InputPositionCode");
                    //hit.Value = "0";

                }
            }

            getWarehousePosition();
        }

        //仓位
        protected void getWarehousePosition()
        {

            DropDownListPosition.Items.Clear();

            string sql = "SELECT DISTINCT WL_ID,WL_NAME FROM TBWS_LOCATION WHERE WL_WSID='" + DropDownListWarehouse.SelectedValue + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            DataRow row = dt.NewRow();
            row["WL_ID"] = "0";
            row["WL_NAME"] = "待查";
            dt.Rows.InsertAt(row, 0);//添加为待查
            DropDownListPosition.DataSource = dt;
            DropDownListPosition.DataTextField = "WL_NAME";
            DropDownListPosition.DataValueField = "WL_ID";
            DropDownListPosition.DataBind();
        }

        protected void DropDownListPosition_SelecedIndexChanged(object sender, EventArgs e)
        {
            bool IsSelect = true;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if ((GridView1.Rows[i].FindControl("CheckBox1") as CheckBox).Checked)
                {

                    TextBox tb = (TextBox)GridView1.Rows[i].FindControl("TextBoxPosition");
                    tb.Text = DropDownListPosition.SelectedItem.Text;

                    HtmlInputText hit = (HtmlInputText)GridView1.Rows[i].FindControl("InputPositionCode");
                    hit.Value = DropDownListPosition.SelectedValue;

                    IsSelect = false;

                }
            }
            if (IsSelect)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    TextBox tb = (TextBox)GridView1.Rows[i].FindControl("TextBoxPosition");
                    tb.Text = DropDownListPosition.SelectedItem.Text;

                    HtmlInputText hit = (HtmlInputText)GridView1.Rows[i].FindControl("InputPositionCode");
                    hit.Value = DropDownListPosition.SelectedValue;
                }
            }
        }

        protected void DropDownListPTCTo_SelecedIndexChanged(object sender, EventArgs e)
        {

            bool IsSelect = true;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if ((GridView1.Rows[i].FindControl("CheckBox1") as CheckBox).Checked)
                {
                    if (DropDownListPTCTo.SelectedValue == "0")
                    {
                        //请选择
                        TextBox tb = (TextBox)GridView1.Rows[i].FindControl("TextBoxGVPTCTo");
                        tb.Text = DropDownListPTCTo.SelectedItem.Text;
                        IsSelect = false;
                    }
                    else
                    { 
                        //备库
                        TextBox tb = (TextBox)GridView1.Rows[i].FindControl("TextBoxGVPTCTo");
                        tb.Text = DropDownListPTCTo.SelectedItem.Text + "-MTO" + Convert.ToInt64(LabelCode.Text.Trim().Replace("MTO", "0")).ToString();
                        IsSelect = false;
                    }
                }
            }
            if (IsSelect)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    if (DropDownListPTCTo.SelectedValue == "0")
                    {
                        //请选择
                        TextBox tb = (TextBox)GridView1.Rows[i].FindControl("TextBoxGVPTCTo");
                        tb.Text = DropDownListPTCTo.SelectedItem.Text;
                        IsSelect = false;
                    }
                    else
                    {
                        //备库
                        TextBox tb = (TextBox)GridView1.Rows[i].FindControl("TextBoxGVPTCTo");
                        tb.Text = DropDownListPTCTo.SelectedItem.Text + "-MTO" + Convert.ToInt64(LabelCode.Text.Trim().Replace("MTO", "0")).ToString();
                        IsSelect = false;
                    }
                }
            }
        }

        protected void GetDep()
        {
            string sql = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO where DEP_CODE='03' or DEP_CODE='09' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListDep.DataTextField = "DEP_NAME";
            DropDownListDep.DataValueField = "DEP_CODE";
            DropDownListDep.DataSource = dt;
            DropDownListDep.DataBind();
        }

        protected void DropDownListDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetStaff();
        }

        protected void GetStaff()
        {
            DropDownListPlaner.ClearSelection();
            DropDownListPlaner.Items.Clear();

            string sql = "SELECT DISTINCT ST_CODE,ST_NAME FROM TBDS_STAFFINFO WHERE ST_DEPID='" + DropDownListDep.SelectedValue + "'";
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

            if (flag == "PUSHMTO")
            {
                LabelCode.Text = generateCode();

                TextBoxDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                string sql = "insert into TBWS_MTOCODE (MTO_CODE) VALUES ('" + LabelCode.Text + "')";

                DBCallCommon.ExeSqlText(sql);
                LabelState.Text = "0";
                ClosingAccountDate(TextBoxDate.Text.Trim());
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
                    "FROM View_SM_Storage WHERE State='MTO" + Session["UserID"].ToString() + "'";


                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);


                GridView1.DataSource = dt;
                GridView1.DataBind();

                sql = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='MTO" + Session["UserID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
            }

            if (flag == "OPEN")
            {
                string sql = "SELECT TOP 1 MTOCode AS Code,Date AS Date,TargetCode AS PTCToCode," +
                    "DepCode AS DepCode,Dep AS DepName,PlanerCode AS PlanerCode," +
                    "Planer AS Planer,DocCode AS DocCode,Doc AS Doc,VerifierCode AS VerifierCode,Verifier AS Verifier," +
                    "left(VerifyDate,10) AS ApproveDate,TotalState AS State,TotalNote AS Comment,WarehouseCode,LocationCode as PositionCode FROM View_SM_MTO WHERE MTOCode='" + code + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    LabelCode.Text = code;

                    LabelState.Text = dr["State"].ToString();

                    TextBoxDate.Text = dr["Date"].ToString();

                    ClosingAccountDate(TextBoxDate.Text.Trim());

                    //try { DropDownListPTCTo.Items.FindByValue(dr["PTCToCode"].ToString()).Selected = true; }
                    //catch { }

                    TextBoxPTCTo.Text = dr["PTCToCode"].ToString();

                    TextBoxComment.Text = dr["Comment"].ToString();

                    DropDownListWarehouse.ClearSelection();

                    try
                    {

                        DropDownListWarehouse.Items.FindByValue(dr["WarehouseCode"].ToString()).Selected = true;//仓库
                    }
                    catch { }

                    //选择仓位
                    getWarehousePosition();

                    DropDownListPosition.ClearSelection();

                    try
                    {
                        DropDownListPosition.Items.FindByValue(dr["PositionCode"].ToString()).Selected = true;//仓库
                    }
                    catch { }

                    DropDownListDep.ClearSelection();

                    try { DropDownListDep.Items.FindByValue(dr["DepCode"].ToString()).Selected = true; }
                    catch { }

                    GetStaff();

                    try { DropDownListPlaner.Items.FindByValue(dr["PlanerCode"].ToString()).Selected = true; }
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
                    "Unit AS Unit,cast(KTNUM as float) AS WN,cast(KTFZNUM as float) AS WQN,cast(TZNUM as float) AS AdjN,cast(TZFZNUM as float) AS AdjQN,PTCTo AS PTCTo," +
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
                    CD.Enabled = false;
                    HB.Enabled = false;
                    Related.Enabled = true;
                    ImageVerify.Visible = false;

                }
                if (LabelState.Text == "2")
                {
                    if (Session["UserID"].ToString() == LabelVerifierCode.Text.Trim())
                    {

                        Append.Enabled = false;
                        Delete.Enabled = false;
                        Save.Enabled = false;
                        Verifiy.Enabled = false;
                        AntiVerify.Enabled = true;
                        DeleteBill.Enabled = false;
                        Related.Enabled = true;
                        ImageVerify.Visible = true;

                    }
                    else
                    {

                        Append.Enabled = false;
                        Delete.Enabled = false;
                        Save.Enabled = false;
                        Verifiy.Enabled = false;
                        AntiVerify.Enabled = false;
                        DeleteBill.Enabled = false;
                        Related.Enabled = true;
                        ImageVerify.Visible = true;
                        CD.Enabled = false;
                        HB.Enabled = false;
                    }

                    btnPrint.Visible = true;
                    TextBoxPTCTo.Enabled = false;
                }
                if (Request.QueryString["action"] != null && Request.QueryString["action"] == "BG")
                {
                    Append.Enabled = false;
                    Delete.Enabled = false;
                    Save.Enabled = false;
                   
                }
            }
        }

        //生成MTO单号
        protected string generateCode()
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
                newRow["Position"] = ((TextBox)gRow.FindControl("TextBoxPosition")).Text;
                newRow["PositionCode"] = ((HtmlInputText)gRow.FindControl("InputPositionCode")).Value;
                newRow["Unit"] = ((Label)gRow.FindControl("LabelUnit")).Text;
                newRow["WN"] = ((Label)gRow.FindControl("LabelWN")).Text;
                newRow["WQN"] = ((Label)gRow.FindControl("LabelWQN")).Text;
                newRow["PTCTo"] = ((TextBox)gRow.FindControl("TextBoxGVPTCTo")).Text;
                newRow["PlanMode"] = ((TextBox)gRow.FindControl("TextBoxPlanMode")).Text;
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
                "Warehouse AS Warehouse,LocationCode AS PositionCode,Location AS Position,Unit AS Unit,CAST(CAST(Number AS float) AS CHAR(50)) AS WN," +
                "CAST(CAST(SupportNumber AS float) AS CHAR(50)) AS WQN,PTCTO='--请选择--',PlanMode AS PlanMode,CAST(CAST(Number AS float) AS CHAR(50)) AS AdjN," +
                "CAST(CAST(SupportNumber AS float) AS CHAR(50)) AS AdjQN,Note AS Note,OrderCode AS OrderID " +
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
            CheckMaterial(); //有重复的计划跟踪号和物料有颜色区分
        }

        protected void CheckMaterial() //有重复的计划跟踪号和物料有颜色区分
        {
            for (int i = 0; i < (GridView1.Rows.Count - 1); i++)
            {
                GridViewRow gvrow0 = GridView1.Rows[i];
                string ptcode = ((Label)gvrow0.FindControl("LabelPTCFrom")).Text; //从计划跟踪号
                string materialcode = ((Label)gvrow0.FindControl("LabelMaterialCode")).Text; //物料代码
                for (int j = i + 1; j < GridView1.Rows.Count; j++)
                {
                    GridViewRow gvrow1 = GridView1.Rows[j];
                    string nextptcode = ((Label)gvrow1.FindControl("LabelPTCFrom")).Text;
                    string nextmaterialcode = ((Label)gvrow1.FindControl("LabelMaterialCode")).Text;
                    if (ptcode == nextptcode && materialcode == nextmaterialcode)
                    {
                        gvrow0.BackColor = System.Drawing.Color.FromName("#e45f5f");
                        gvrow1.BackColor = System.Drawing.Color.FromName("#e45f5f");

                    }
                }
            }

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
            if (isLock() == false)
            {
                string script = @"alert('系统正在结账,请稍后...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                LabelMessage.Text = "系统正在结账,请稍后...";
            }
            else
            {
                List<string> sqllist = new List<string>();
                string sql = "";

                string Code = LabelCode.Text;
                string Date = TextBoxDate.Text;
                string TargetCode = TextBoxPTCTo.Text.Split('-')[TextBoxPTCTo.Text.Split('-').Length - 1];
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

                    string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;

                    float WN = 0;
                    try { WN = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelWN")).Text); }
                    catch { }
                    Int32 WQN = 0;
                    try { WQN = Convert.ToInt32(((Label)this.GridView1.Rows[i].FindControl("LabelWQN")).Text); }
                    catch { }
                    string PTCTo = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxGVPTCTo")).Text;
                    string PlanMode = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxPlanMode")).Text;
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
                LabelState.Text = "1";
                LabelMessage.Text = "保存成功！";
            }
        }
        protected void TextBoxPTCTo_TextChanged(object sender, EventArgs e)
        {
            string engid = TextBoxPTCTo.Text.Split('-')[TextBoxPTCTo.Text.Split('-').Length - 1];

            if (engid != "备库")
            {
                string sql = "select TSA_PJNAME,TSA_ENGNAME,TSA_ID  FROM View_TM_TaskAssign WHERE TSA_FATHERNODE='0' and TSA_ID='" + engid + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {

                    TextBoxPTCTo.Text = engid;
                    generatePTCTo();
                }
                else
                {
                    TextBoxPTCTo.Text = string.Empty;
                    generatePTCTo();
                }
            }
            else
            {
                TextBoxPTCTo.Text = engid;
                generatePTCTo();
            }


        }

        //自动生成到计划跟踪号
        protected void generatePTCTo()
        {
            //根据选择的项目工程生成到计划跟踪号
            if ((TextBoxPTCTo.Text != string.Empty) && (TextBoxPTCTo.Text != "备库"))
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxGVPTCTo")).Text = TextBoxPTCTo.Text.Split('-')[TextBoxPTCTo.Text.Split('-').Length - 1] + "_" + "MTO" + Convert.ToInt64(LabelCode.Text.Replace("MTO", "000")) + "_" + (i + 1).ToString().PadLeft(3, '0');
                }
            }
            else if (TextBoxPTCTo.Text == "备库")
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxGVPTCTo")).Text = "备库";
                }
            }
            else
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxGVPTCTo")).Text = "--请选择--";
                }
            }
            LabelMessage.Text = "到计划跟踪号自动生成完毕";
        }

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

                string sqlstate = "select mto_state from tbws_mto where mto_code='" + Code + "'";

                if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows.Count > 0)
                {
                    if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows[0]["mto_state"].ToString() == "2")
                    {

                        string script = @"alert('单据已审核，单据不能再审核！');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                        return;
                    }
                }

                bool HasError = false;
                int ErrorType = 0;

                List<string> sqllist = new List<string>();
                string sql = "";
                
                string Date = TextBoxDate.Text;
                string TargetCode = TextBoxPTCTo.Text.Split('-')[TextBoxPTCTo.Text.Split('-').Length - 1];
                string Comment = TextBoxComment.Text;
                string PlanerCode = DropDownListPlaner.SelectedValue;
                string DepCode = DropDownListDep.SelectedValue;
                string DocCode = LabelDocCode.Text;

                string VerifierCode = Session["UserID"].ToString();

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
                                ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
                        }
                    }

                }

                sql = "DELETE FROM TBWS_MTO WHERE MTO_CODE='" + Code + "'";
                sqllist.Add(sql);
                sql = "INSERT INTO TBWS_MTO(MTO_CODE,MTO_DATE,MTO_TARGETID," +
                    "MTO_DEP,MTO_PLANER,MTO_DOC,MTO_VERIFIER,MTO_VERIFYDATE," +
                    "MTO_STATE,MTO_NOTE,MTO_RealTime) VALUES('" + Code + "','" +
                    Date + "','" + TargetCode + "','" + DepCode + "','" + PlanerCode + "','" + DocCode + "','" +
                    VerifierCode + "','" + ApproveDate + "','1','" + Comment + "',convert(varchar(50),getdate(),120))";
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

                    string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;

                    if (PositionCode == string.Empty || PositionCode == "0")
                    {
                        HasError = true;
                        ErrorType = 3;
                        break;
                    }

                    float WN = 0;
                    try { WN = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelWN")).Text); }
                    catch { }
                    Int32 WQN = 0;
                    try { WQN = Convert.ToInt32(((Label)this.GridView1.Rows[i].FindControl("LabelWQN")).Text); }
                    catch { }
                    string PTCTo = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxGVPTCTo")).Text.Trim();

                    if (PTCTo == "--请选择--" || PTCTo == string.Empty)
                    {
                        HasError = true;
                        ErrorType = 1;
                        break;
                    }
                    else
                    {
                        PTCTo = PTCTo.Replace("--请选择--", " ").Trim();
                    }

                    if (PTCTo == PTCFrom)
                    {
                        HasError = true;
                        ErrorType = 4;
                        break;
                    }
                    string PlanMode = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxPlanMode")).Text;
                    float AdjN = 0;
                    try { AdjN = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxAdjN")).Text); }
                    catch { }

                    if (AdjN == 0)
                    {
                        HasError = true;
                        ErrorType = 2;
                        break;
                    }

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
                        Length + "','" + Width + "','" + LotNumber + "','" + PTCFrom + "','" + WarehouseCode + "','" + PositionCode + "'," +
                        WN + "," + WQN + ",'" + PTCTo + "'," + AdjN + "," + AdjQN + ",'" + PlanMode + "','" +
                        OrderID + "','" + Note + "','')";
                    sqllist.Add(sql);
                }

                if (HasError)
                {
                    sqllist.Clear();

                    if (ErrorType == 1)
                    {
                        LabelMessage.Text = "计划跟踪号为空，单据不能审核！";

                        string script = @"alert('计划跟踪号为空，单据不能审核！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                    else if (ErrorType == 2)
                    {

                        LabelMessage.Text = "调整数量为0，单据不能审核！";

                        string script = @"alert('调整数量为0，单据不能审核！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                    else if (ErrorType == 3)
                    {

                        LabelMessage.Text = "仓位为空，单据不能审核！";

                        string script = @"alert('仓位为空，单据不能审核！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                    else if (ErrorType == 4)
                    {

                        LabelMessage.Text = "调整之后计划跟踪号，不能与调整之前的相同！";

                        string script = @"alert('调整之后计划跟踪号，不能与调整之前的相同！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }

                }
                else
                {


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
                        string action = "NOR";
                        

                        sqllist.Clear();

                        string strsql = "update TBPC_MARSTOUSEALL set PUR_ISSTOUSE='2' where PUR_OPERSTATE='" + LabelCode.Text.Trim() + Session["UserID"].ToString() + "'";

                        sqllist.Add(strsql);

                       
                        strsql = "update TBPC_MPTEMPCHANGE set MP_EXECSTATE='3' WHERE MP_MTO='" + LabelCode.Text.Trim() + "'";
                        sqllist.Add(strsql);
                            
                        

                        DBCallCommon.ExecuteTrans(sqllist);


                        Response.Redirect("SM_Warehouse_MTOAdjust.aspx?FLAG=OPEN&&ACTION=" + action + "&&ID=" + Code);



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
            }
        }

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
                if (LabelClosingAccount.Text != "NoTime")
                {
                    LabelMessage.Text = "反审核未通过：跨月的入库单不允许反审！！";
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
                        string action = "NOR";

                        List<string> sqllist = new List<string>();

                        //占用转MTO

                        string strsql = "update TBPC_MARSTOUSEALL set PUR_ISSTOUSE='1' where PUR_OPERSTATE='" + LabelCode.Text.Trim() + Session["UserID"].ToString() + "'";

                        sqllist.Add(strsql);

                        
                            //变更转MTO
                            strsql = "update TBPC_MPTEMPCHANGE set MP_EXECSTATE='2' WHERE MP_MTO='" + LabelCode.Text.Trim() + "'";

                            sqllist.Add(strsql);                            
                            
                        

                        DBCallCommon.ExecuteTrans(sqllist);

                        Response.Redirect("SM_Warehouse_MTOAdjust.aspx?FLAG=OPEN&&ACTION=" + action + "&&ID=" + LabelCode.Text);

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
        }

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

                   
                        //变更转MTO
                        sql = "update TBPC_MPTEMPCHANGE set MP_EXECSTATE='2',MP_MTO=NULL WHERE MP_MTO='" + LabelCode.Text.Trim() + "'";

                        sqllist.Add(sql);
                    

                    DBCallCommon.ExecuteTrans(sqllist);


                    ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>window.returnValue = true;closewin();</script>");


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




            //-----插入记录模式

            if (mode2 == "0")
            {

                   for (int i = 0; i < GridView1.Rows.Count; i++)
                   {
                       GridViewRow gRow = GridView1.Rows[i];

                       if (((CheckBox)gRow.FindControl("CheckBox1")).Checked == true)
                       {
                           #region 均分与复制
                          
                           if (mode == "0")
                           {
                               //数据均分模式

                               float wn = 0; //可调数量
                               float an = 0; //调整数量
                               try
                               {
                                   wn = Convert.ToSingle(((Label)gRow.FindControl("LabelWN")).Text);
                                   an = Convert.ToSingle(((TextBox)gRow.FindControl("TextBoxAdjN")).Text);
                               }
                               catch
                               {
                                   LabelMessage.Text = "请正确填写实发数量！";
                                   return;
                               }
                               for (int j = 0; j < count - 1; j++)
                               {
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
                                   newRow["Position"] = ((TextBox)gRow.FindControl("TextBoxPosition")).Text;
                                   newRow["PositionCode"] = ((HtmlInputText)gRow.FindControl("InputPositionCode")).Value;
                                   newRow["Unit"] = ((Label)gRow.FindControl("LabelUnit")).Text;

                                   newRow["WN"] = Convert.ToInt32(wn / count).ToString();

                                   newRow["WQN"] = ((Label)gRow.FindControl("LabelWQN")).Text;
                                   newRow["PTCTo"] = ((TextBox)gRow.FindControl("TextBoxGVPTCTo")).Text;
                                   newRow["PlanMode"] = ((TextBox)gRow.FindControl("TextBoxPlanMode")).Text;

                                   newRow["AdjN"] = Convert.ToInt32(an / count).ToString();

                                   newRow["AdjQN"] = ((TextBox)gRow.FindControl("TextBoxAdjQN")).Text;
                                   newRow["Note"] = ((TextBox)gRow.FindControl("TextBoxNote")).Text;
                                   newRow["OrderID"] = ((Label)gRow.FindControl("LabelOrderID")).Text;

                                   tb.Rows.Add(newRow);
                               }

                               //最后一行

                               DataRow row1 = tb.NewRow();

                               row1["UniqueID"] = ((Label)gRow.FindControl("LabelUniqueID")).Text;
                               row1["SQCODE"] = ((Label)gRow.FindControl("LabelSQCODE")).Text;
                               row1["MaterialCode"] = ((Label)gRow.FindControl("LabelMaterialCode")).Text;
                               row1["MaterialName"] = ((Label)gRow.FindControl("LabelMaterialName")).Text;
                               row1["Attribute"] = ((Label)gRow.FindControl("LabelAttribute")).Text;
                               row1["GB"] = ((Label)gRow.FindControl("LabelGB")).Text;
                               row1["MaterialStandard"] = ((Label)gRow.FindControl("LabelMaterialStandard")).Text;
                               row1["Fixed"] = ((Label)gRow.FindControl("LabelFixed")).Text;
                               row1["Length"] = ((Label)gRow.FindControl("LabelLength")).Text;
                               row1["Width"] = ((Label)gRow.FindControl("LabelWidth")).Text;
                               row1["LotNumber"] = ((Label)gRow.FindControl("LabelLotNumber")).Text;
                               row1["PTCFrom"] = ((Label)gRow.FindControl("LabelPTCFrom")).Text;
                               row1["Warehouse"] = ((Label)gRow.FindControl("LabelWarehouse")).Text;
                               row1["WarehouseCode"] = ((Label)gRow.FindControl("LabelWarehouseCode")).Text;
                               row1["Position"] = ((TextBox)gRow.FindControl("TextBoxPosition")).Text;
                               row1["PositionCode"] = ((HtmlInputText)gRow.FindControl("InputPositionCode")).Value;
                               row1["Unit"] = ((Label)gRow.FindControl("LabelUnit")).Text;

                               row1["WN"] = Convert.ToInt32(wn - Convert.ToInt32((wn / count)) * (count - 1)).ToString();

                               row1["WQN"] = ((Label)gRow.FindControl("LabelWQN")).Text;
                               row1["PTCTo"] = ((TextBox)gRow.FindControl("TextBoxGVPTCTo")).Text;
                               row1["PlanMode"] = ((TextBox)gRow.FindControl("TextBoxPlanMode")).Text;

                               row1["AdjN"] = Convert.ToInt32(an - Convert.ToInt32((an / count)) * (count - 1)).ToString();

                               row1["AdjQN"] = ((TextBox)gRow.FindControl("TextBoxAdjQN")).Text;

                               row1["Note"] = ((TextBox)gRow.FindControl("TextBoxNote")).Text;
                               row1["OrderID"] = ((Label)gRow.FindControl("LabelOrderID")).Text;

                               tb.Rows.Add(row1);

                           }


                           else if (mode == "1")
                           {
                               //数据复制模式
                               for (int j = 0; j < count - 1; j++)
                               {
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
                                   newRow["Position"] = ((TextBox)gRow.FindControl("TextBoxPosition")).Text;
                                   newRow["PositionCode"] = ((HtmlInputText)gRow.FindControl("InputPositionCode")).Value;
                                   newRow["Unit"] = ((Label)gRow.FindControl("LabelUnit")).Text;

                                   newRow["WN"] = ((Label)gRow.FindControl("LabelWN")).Text;

                                   newRow["WQN"] = ((Label)gRow.FindControl("LabelWQN")).Text;
                                   newRow["PTCTo"] = ((TextBox)gRow.FindControl("TextBoxGVPTCTo")).Text;
                                   newRow["PlanMode"] = ((TextBox)gRow.FindControl("TextBoxPlanMode")).Text;
                                   newRow["AdjN"] = ((TextBox)gRow.FindControl("TextBoxAdjN")).Text;
                                   newRow["AdjQN"] = ((TextBox)gRow.FindControl("TextBoxAdjQN")).Text;
                                   newRow["Note"] = ((TextBox)gRow.FindControl("TextBoxNote")).Text;
                                   newRow["OrderID"] = ((Label)gRow.FindControl("LabelOrderID")).Text;

                                   tb.Rows.Add(newRow);
                               }
                           }

                           #endregion
                       }
                       else
                       {

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
                           newRow["Position"] = ((TextBox)gRow.FindControl("TextBoxPosition")).Text;
                           newRow["PositionCode"] = ((HtmlInputText)gRow.FindControl("InputPositionCode")).Value;
                           newRow["Unit"] = ((Label)gRow.FindControl("LabelUnit")).Text;
                           newRow["WN"] = ((Label)gRow.FindControl("LabelWN")).Text;
                           newRow["WQN"] = ((Label)gRow.FindControl("LabelWQN")).Text;
                           newRow["PTCTo"] = ((TextBox)gRow.FindControl("TextBoxGVPTCTo")).Text;
                           newRow["PlanMode"] = ((TextBox)gRow.FindControl("TextBoxPlanMode")).Text;
                           newRow["AdjN"] = ((TextBox)gRow.FindControl("TextBoxAdjN")).Text;
                           newRow["AdjQN"] = ((TextBox)gRow.FindControl("TextBoxAdjQN")).Text;
                           newRow["Note"] = ((TextBox)gRow.FindControl("TextBoxNote")).Text;
                           newRow["OrderID"] = ((Label)gRow.FindControl("LabelOrderID")).Text;
                           tb.Rows.Add(newRow);
                       }

                   }
            }

            //记录追加模式
            else if (mode2 == "1")
            {
                tb = getDataFromGridView();

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];

                    if (((CheckBox)gRow.FindControl("CheckBox1")).Checked == true)
                    {

                        if (mode == "0")
                        {
                            //数据均分模式

                            float wn = 0; //可调数量
                            float an = 0; //调整数量
                            try
                            {
                                wn = Convert.ToSingle(((Label)gRow.FindControl("LabelWN")).Text);
                                an = Convert.ToSingle(((TextBox)gRow.FindControl("TextBoxAdjN")).Text);
                            }
                            catch
                            {
                                LabelMessage.Text = "请正确填写实发数量！";
                                return;
                            }
                            for (int j = 0; j < count - 2; j++)
                            {
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
                                newRow["Position"] = ((TextBox)gRow.FindControl("TextBoxPosition")).Text;
                                newRow["PositionCode"] = ((HtmlInputText)gRow.FindControl("InputPositionCode")).Value;
                                newRow["Unit"] = ((Label)gRow.FindControl("LabelUnit")).Text;

                                newRow["WN"] = Convert.ToInt32(wn / count).ToString();

                                newRow["WQN"] = ((Label)gRow.FindControl("LabelWQN")).Text;
                                newRow["PTCTo"] = ((TextBox)gRow.FindControl("TextBoxGVPTCTo")).Text;
                                newRow["PlanMode"] = ((TextBox)gRow.FindControl("TextBoxPlanMode")).Text;

                                newRow["AdjN"] = Convert.ToInt32(an / count).ToString();

                                newRow["AdjQN"] = ((TextBox)gRow.FindControl("TextBoxAdjQN")).Text;

                                newRow["Note"] = ((TextBox)gRow.FindControl("TextBoxNote")).Text;
                                newRow["OrderID"] = ((Label)gRow.FindControl("LabelOrderID")).Text;

                                tb.Rows.Add(newRow);
                            }

                            tb.Rows[i]["WN"] =  Convert.ToInt32(wn / count).ToString();//改变原来行的数量

                            tb.Rows[i]["AdjN"] = Convert.ToInt32(an / count).ToString();//改变原来行的可调数量

                            //最后一行

                            DataRow row1 = tb.NewRow();

                            row1["UniqueID"] = ((Label)gRow.FindControl("LabelUniqueID")).Text;
                            row1["SQCODE"] = ((Label)gRow.FindControl("LabelSQCODE")).Text;
                            row1["MaterialCode"] = ((Label)gRow.FindControl("LabelMaterialCode")).Text;
                            row1["MaterialName"] = ((Label)gRow.FindControl("LabelMaterialName")).Text;
                            row1["Attribute"] = ((Label)gRow.FindControl("LabelAttribute")).Text;
                            row1["GB"] = ((Label)gRow.FindControl("LabelGB")).Text;
                            row1["MaterialStandard"] = ((Label)gRow.FindControl("LabelMaterialStandard")).Text;
                            row1["Fixed"] = ((Label)gRow.FindControl("LabelFixed")).Text;
                            row1["Length"] = ((Label)gRow.FindControl("LabelLength")).Text;
                            row1["Width"] = ((Label)gRow.FindControl("LabelWidth")).Text;
                            row1["LotNumber"] = ((Label)gRow.FindControl("LabelLotNumber")).Text;
                            row1["PTCFrom"] = ((Label)gRow.FindControl("LabelPTCFrom")).Text;
                            row1["Warehouse"] = ((Label)gRow.FindControl("LabelWarehouse")).Text;
                            row1["WarehouseCode"] = ((Label)gRow.FindControl("LabelWarehouseCode")).Text;
                            row1["Position"] = ((TextBox)gRow.FindControl("TextBoxPosition")).Text;
                            row1["PositionCode"] = ((HtmlInputText)gRow.FindControl("InputPositionCode")).Value;
                            row1["Unit"] = ((Label)gRow.FindControl("LabelUnit")).Text;

                            row1["WN"] = Convert.ToInt32(wn - Convert.ToInt32((wn / count)) * (count - 1)).ToString();

                            row1["WQN"] = ((Label)gRow.FindControl("LabelWQN")).Text;
                            row1["PTCTo"] = ((TextBox)gRow.FindControl("TextBoxGVPTCTo")).Text;
                            row1["PlanMode"] = ((TextBox)gRow.FindControl("TextBoxPlanMode")).Text;

                            row1["AdjN"] = Convert.ToInt32(an - Convert.ToInt32((an / count)) * (count - 1)).ToString();

                            row1["AdjQN"] = ((TextBox)gRow.FindControl("TextBoxAdjQN")).Text;

                            row1["Note"] = ((TextBox)gRow.FindControl("TextBoxNote")).Text;
                            row1["OrderID"] = ((Label)gRow.FindControl("LabelOrderID")).Text;

                            tb.Rows.Add(row1);

                        }


                        else if (mode == "1")
                        {
                            //数据复制模式
                            for (int j = 0; j < count - 1; j++)
                            {
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
                                newRow["Position"] = ((TextBox)gRow.FindControl("TextBoxPosition")).Text;
                                newRow["PositionCode"] = ((HtmlInputText)gRow.FindControl("InputPositionCode")).Value;
                                newRow["Unit"] = ((Label)gRow.FindControl("LabelUnit")).Text;

                                newRow["WN"] = ((Label)gRow.FindControl("LabelWN")).Text;

                                newRow["WQN"] = ((Label)gRow.FindControl("LabelWQN")).Text;
                                newRow["PTCTo"] = ((TextBox)gRow.FindControl("TextBoxGVPTCTo")).Text;
                                newRow["PlanMode"] = ((TextBox)gRow.FindControl("TextBoxPlanMode")).Text;
                                newRow["AdjN"] = ((TextBox)gRow.FindControl("TextBoxAdjN")).Text;
                                newRow["AdjQN"] = ((TextBox)gRow.FindControl("TextBoxAdjQN")).Text;
                                newRow["Note"] = ((TextBox)gRow.FindControl("TextBoxNote")).Text;
                                newRow["OrderID"] = ((Label)gRow.FindControl("LabelOrderID")).Text;

                                tb.Rows.Add(newRow);
                            }
                        }
                    }
                }
            }


            GridView1.DataSource = tb;
            GridView1.DataBind();
            LabelMessage.Text = "拆分成功！";
            
        }


        protected void CD_Click(object sender, EventArgs e) //拆单
        {

            string MTOCode = LabelCode.Text.Trim();

            if (MTOCode.Contains("S") == true)
            {
                LabelMessage.Text = "子出库单不允许拆分！";
                string script = @"alert('子出库单不允许拆分！');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                return;
            }
            List<string> sqllist = new List<string>();

            string sql = "UPDATE TBWS_MTODETAIL SET MTO_STATE='' WHERE MTO_STATE='SPLIT" + Session["UserID"].ToString() + "'";
            sqllist.Add(sql);
            
            string uniqueid = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked == true)
                {
                    uniqueid = ((Label)GridView1.Rows[i].FindControl("LabelUniqueID")).Text;
                    //更新明细表中的状态
                    sql = "UPDATE TBWS_MTODETAIL SET MTO_STATE='SPLIT" + Session["UserID"].ToString() + "' WHERE MTO_CODE='" + MTOCode + "' AND MTO_UNIQUEID='" + uniqueid + "'";
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
            Response.Redirect("SM_Warehouse_MTOSplit.aspx?ID=" + MTOCode);
        }

        protected void HB_Click(object sender, EventArgs e) //合并
        {
            string code = LabelCode.Text.Trim();

            if (code.Contains("S") == true)
            {
                string pcode = code.Substring(0, code.IndexOf("S", 0)); //原拆分前mto单
                Response.Redirect("SM_Warehouse_MTOSplitResult.aspx?IDP=" + pcode + "&&IDC=" + code + "&&RES=M");
            }
            else
            {
                LabelMessage.Text = "请选择单号带有S的子单进行合并操作！";
                return;
            }
            
        }

        protected void ButtonSplitCancel_Click(object sender, EventArgs e)
        {
            RadioButtonListSplitMode.Items[0].Selected = true;
            RadioButtonListSplitMode2.Items[0].Selected = true;
            TextBoxSplitLineNum.Text = "2";
        }

        #region 隐藏列
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
            lt.Add("从计划跟踪号");
            lt.Add("批号");
            lt.Add("是否定尺");
            lt.Add("长");
            lt.Add("宽");
            lt.Add("可调张(支)数");
            lt.Add("调整张(支)数");
            lt.Add("订单单号");
            if ((sender as CheckBox).Checked)
            {
                CheckBox1.Enabled = false;
                CheckBox1.Checked = true;
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
                CheckBox1.Enabled = true;
                CheckBox1.Checked = false;
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
        #endregion
    }
}
