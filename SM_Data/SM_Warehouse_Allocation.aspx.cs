using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using AjaxControlToolkit;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_Allocation : System.Web.UI.Page
    {
        double tn = 0;//数量
        Int32 tqn = 0;//张（支），调拨张（支）数

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getWarehouse();
                getDep();
                initial();      //初始化调拨单
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

        //初始化
        protected void getWarehouse()
        {
            string sql = "SELECT DISTINCT WS_ID,WS_NAME FROM TBWS_WAREHOUSE WHERE WS_FATHERID<>'ROOT'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListWarehouseIn.DataTextField = "WS_NAME";
            DropDownListWarehouseIn.DataValueField = "WS_ID";
            DataRow row = dt.NewRow();
            row["WS_NAME"] = "--请选择--";
            row["WS_ID"] = "0";
            dt.Rows.InsertAt(row, 0);
            DropDownListWarehouseIn.DataSource = dt;
            DropDownListWarehouseIn.DataBind();
        }

        protected void getDep()
        {
            string sql = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO where len(DEP_CODE)=2";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListDep.DataTextField = "DEP_NAME";
            DropDownListDep.DataValueField = "DEP_CODE";
            DropDownListDep.DataSource = dt;
            DropDownListDep.DataBind();
           
        }


        protected  void initial()
        {   
            /*
             * 调拨单的审核没有设置权限
             */

            string code = Request.QueryString["ID"].ToString();
            string flag = Request.QueryString["FLAG"].ToString();


            //推调拨单的时候，不能审核

            if (flag == "PUSHAL")
            {
                LabelCode.Text = generateCode();

               string sql = "INSERT INTO TBWS_ALLOCATIONCODE (AL_CODE) VALUES ('" + LabelCode.Text + "')";

                DBCallCommon.ExeSqlText(sql);

                InputState.Value = "0";

                TextBoxDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                ClosingAccountDate(TextBoxDate.Text.Trim());

                DropDownListDep.ClearSelection();
                DropDownListDep.SelectedValue = Session["UserDeptID"].ToString().Trim();//部门


                LabelDoc.Text = Session["UserName"].ToString();//制单人
                LabelDocCode.Text = Session["UserID"].ToString();
                LabelVerifier.Text = Session["UserName"].ToString();
                LabelVerifierCode.Text = Session["UserID"].ToString();
                TextBoxAdvice.Text = "";
                LabelApproveDate.Text = "";

                sql = "SELECT UniqueID='',SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,Length AS Length," +
                    "Width AS Width,LotNumber AS LotNumber,PlanMode AS PlanMode,PTC AS PTC," +
                    "WarehouseCode AS OutWarehouseCode,Warehouse AS OutWarehouse,LocationCode AS OutPositionCode," +
                    "Location AS OutPosition,InWarehouseCode='0',InWarehouse='--请选择--',InPositionCode='0',InPosition='待查'," +
                    "Unit AS Unit,cast(Number as float) AS WN, cast(Number as float) AS Number,cast(SupportNumber as float) AS WQN,cast(SupportNumber as float) AS QN," +
                    "OrderCode AS OrderID,Note AS Comment " +
                    "FROM View_SM_Storage WHERE State='AL" + Session["UserID"].ToString() +"'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                sql = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='AL" + Session["UserID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
                Tab2.Enabled = false;
                AntiVerify.Enabled = false;
                DeleteBill.Enabled = false;
                AntiSubmit.Enabled = false;//反提交不能用
            
            
            }
            //打开的时候，
            if (flag == "OPEN")
            {
                string sql = "SELECT TOP 1 Date AS Date,WarehouseInCode AS WarehouseInCode,WarehouseIn AS WarehouseIn,LocationInCode," +
                    "DepCode AS DepCode,Dep AS Dep,AcceptanceCode AS AcceptanceCode,Acceptance AS Acceptance," +
                    "ClerkCode AS ClerkCode,Clerk AS Clerk,KeeperCode AS KeepingCode,Keeper AS Keeping," +
                    "DocCode AS DocCode,Doc AS Doc,VerifierCode AS VerifierCode,Verifier AS Verifier," +
                    "left(VerifyDate,10) AS ApproveDate,Advice AS Advice,TotalState AS State,TotalNote AS Comment " +
                    "FROM View_SM_Allocation WHERE ALCode='" + code + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    LabelCode.Text = code;


                    //调拨单的状态
                    InputState.Value = dr["State"].ToString();

                    TextBoxDate.Text = dr["Date"].ToString();//日期

                    //获取制单时的系统关账时间

                    ClosingAccountDate(System.DateTime.Now.ToString("yyyy-MM-dd"));

                    try { DropDownListWarehouseIn.Items.FindByValue(dr["WarehouseInCode"].ToString()).Selected = true; }
                    catch { }


                    //选择仓位
                    getWarehousePosition();

                    DropDownListPositionIn.ClearSelection();
                    try
                    {
                        DropDownListPositionIn.Items.FindByValue(dr["LocationInCode"].ToString()).Selected = true;//仓位
                    }
                    catch { }

                    try { DropDownListDep.Items.FindByValue(dr["DepCode"].ToString()).Selected = true; }
                    catch { }
                    LabelDoc.Text = dr["Doc"].ToString();
                    LabelDocCode.Text = dr["DocCode"].ToString();
                    LabelVerifier.Text = dr["Verifier"].ToString();
                    LabelVerifierCode.Text = dr["VerifierCode"].ToString();
                    TextBoxAdvice.Text = dr["Advice"].ToString();
                    LabelApproveDate.Text = dr["ApproveDate"].ToString();
                }
                dr.Close();

                sql = "SELECT UniqueCode AS UniqueID,SQCODE AS SQCODE,MaterialCode AS MaterialCode," +
                    "MaterialName AS MaterialName,Standard AS MaterialStandard,Fixed AS Fixed," +
                    "Length AS Length,Width AS Width,LotNumber AS LotNumber,Attribute AS Attribute," +
                    "GB AS GB,Unit AS Unit,KTNUM AS WN,TZNUM AS Number,KTFZNUM AS WQN,TZFZNUM AS QN," +
                    "WarehouseOutCode AS OutWarehouseCode,WarehouseOut AS OutWarehouse,LocationOutCode AS OutPositionCode," +
                    "LocationOut AS OutPosition,WarehouseInCode AS InWarehouseCode,WarehouseIn AS InWarehouse," +
                    "LocationInCode AS InPositionCode,LocationIn AS InPosition,OrderCode AS OrderID,PlanMode AS PlanMode," +
                    "PTC AS PTC,DetailNote AS Comment,DetailState AS State " +
                    "FROM View_SM_Allocation WHERE ALCode='" + code + "' order by UniqueCode";
                DataTable tb = new DataTable();
                tb = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = tb;
                GridView1.DataBind();

                Tab2.Enabled = false;

                //保存状态，未提交
                if (InputState.Value == "1")
                {
                    Tab2.Enabled = true;
                    Append.Enabled = true;
                    Delete.Enabled = true;
                    Save.Enabled = true;
                    Submit.Enabled = true;
                    Verify.Enabled = false;
                    Deny.Enabled = false;
                    AntiVerify.Enabled = false;
                    DeleteBill.Enabled = true;
                    //Print.Enabled = false;
                    Related.Enabled = true;
                    AntiSubmit.Enabled = false;//反提交不能用
                }
                //提交状态
                if (InputState.Value == "2")
                {
                    LabelVerifier.Text = Session["UserName"].ToString();
                    LabelVerifierCode.Text = Session["UserID"].ToString();
                    LabelApproveDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    Tab2.Enabled = true;
                    Append.Enabled = false;
                    Delete.Enabled = false;
                    Save.Enabled = false;
                    Submit.Enabled = false;
                    AntiSubmit.Enabled = true;

                    //审核按钮可用
                    Verify.Enabled = true;
                    Deny.Enabled = true;

                    
                    AntiVerify.Enabled = false;
                    DeleteBill.Enabled = false;
                    //Print.Enabled = false;
                    Related.Enabled = true;
                }
                if (InputState.Value == "3")
                {
                    Tab2.Enabled = true;
                    Append.Enabled = false;
                    Delete.Enabled = false;
                    Save.Enabled = false;
                    Submit.Enabled = false;
                    AntiSubmit.Enabled = false;//反提交不能用
                    //审核按钮不可用
                    Verify.Enabled = false;

                    Deny.Enabled = false;
                   
                    DeleteBill.Enabled = false;
                    //Print.Enabled = true;
                    Related.Enabled = true;
                    ImageVerify.Visible = true;
                    ImageVerify2.Visible = true;
                      //审核按钮可用
                    AntiVerify.Enabled = false;
                }
            }
        }

        //生成调拨单单号
        protected string generateCode()
        {
            string Code = "";
            string sql = "SELECT MAX(AL_CODE) AS MaxCode FROM TBWS_ALLOCATIONCODE";
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
                return "A000000001";
            }
            else
            {
                int tempnum = Convert.ToInt32((Code.Substring(1, 9)));
                tempnum++;
                Code = "A" + tempnum.ToString().PadLeft(9, '0');
                return Code;
            }
        }




        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tn += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Number"));
                tqn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "QN"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.Cells[13].FindControl("LabelTotalNumber")).Text = tn.ToString();
                ((Label)e.Row.Cells[15].FindControl("LabelTotalQN")).Text = tqn.ToString();
            }
        }

        protected void DropDownListWarehouseIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool IsSelect = true;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if ((GridView1.Rows[i].FindControl("CheckBox1") as CheckBox).Checked)
                {
                    Label warein = (Label)GridView1.Rows[i].FindControl("LabelInWarehouse");
                    Label wareincode = (Label)GridView1.Rows[i].FindControl("LabelInWarehouseCode");
                    TextBox posin = (TextBox)GridView1.Rows[i].FindControl("TextBoxInPosition");
                    HtmlInputText posincode = (HtmlInputText)GridView1.Rows[i].FindControl("InputInPositionCode");
                    warein.Text = DropDownListWarehouseIn.SelectedItem.Text;
                    wareincode.Text = DropDownListWarehouseIn.SelectedValue; 
                    IsSelect = false;
                }

                //posin.Text = "待查";
                //posincode.Value = "0";
            }
            if (IsSelect)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    Label warein = (Label)GridView1.Rows[i].FindControl("LabelInWarehouse");
                    Label wareincode = (Label)GridView1.Rows[i].FindControl("LabelInWarehouseCode");
                    TextBox posin = (TextBox)GridView1.Rows[i].FindControl("TextBoxInPosition");
                    HtmlInputText posincode = (HtmlInputText)GridView1.Rows[i].FindControl("InputInPositionCode");
                    warein.Text = DropDownListWarehouseIn.SelectedItem.Text;
                    wareincode.Text = DropDownListWarehouseIn.SelectedValue;

                    IsSelect = false;
                }
            }
            getWarehousePosition();
        }
        //仓位
        protected void getWarehousePosition()
        {
            DropDownListPositionIn.Items.Clear();

            string sql = "SELECT DISTINCT WL_ID,WL_NAME FROM TBWS_LOCATION WHERE WL_WSID='" + DropDownListWarehouseIn.SelectedValue + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DataRow row = dt.NewRow();
            row["WL_ID"] = "0";
            row["WL_NAME"] = "待查";
            dt.Rows.InsertAt(row, 0);//添加为待查
            DropDownListPositionIn.DataSource = dt;
            DropDownListPositionIn.DataTextField = "WL_NAME";
            DropDownListPositionIn.DataValueField = "WL_ID";
            DropDownListPositionIn.DataBind();
        }

        protected void DropDownListPositionIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool IsSelect = true;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if ((GridView1.Rows[i].FindControl("CheckBox1") as CheckBox).Checked)
                {
                    TextBox posin = (TextBox)GridView1.Rows[i].FindControl("TextBoxInPosition");
                    HtmlInputText posincode = (HtmlInputText)GridView1.Rows[i].FindControl("InputInPositionCode");
                    posin.Text = DropDownListPositionIn.SelectedItem.Text;
                    posincode.Value = DropDownListPositionIn.SelectedValue;
                    IsSelect = false;
                }
            }
            if (IsSelect)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    TextBox posin = (TextBox)GridView1.Rows[i].FindControl("TextBoxInPosition");
                    HtmlInputText posincode = (HtmlInputText)GridView1.Rows[i].FindControl("InputInPositionCode");
                    posin.Text = DropDownListPositionIn.SelectedItem.Text;
                    posincode.Value = DropDownListPositionIn.SelectedValue;
                    IsSelect = false;
                }
            }
        }



        /*******************Tab1****************************/


        //获取当前GridView1中的数据
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
            tb.Columns.Add("Unit", System.Type.GetType("System.String"));
            tb.Columns.Add("WN", System.Type.GetType("System.String"));
            tb.Columns.Add("Number", System.Type.GetType("System.String"));
            tb.Columns.Add("WQN", System.Type.GetType("System.String"));
            tb.Columns.Add("QN", System.Type.GetType("System.String"));
            tb.Columns.Add("Comment", System.Type.GetType("System.String"));
            tb.Columns.Add("OutWarehouse", System.Type.GetType("System.String"));
            tb.Columns.Add("OutWarehouseCode", System.Type.GetType("System.String"));
            tb.Columns.Add("OutPosition", System.Type.GetType("System.String"));
            tb.Columns.Add("OutPositionCode", System.Type.GetType("System.String"));
            tb.Columns.Add("InWarehouse", System.Type.GetType("System.String"));
            tb.Columns.Add("InWarehouseCode", System.Type.GetType("System.String"));
            tb.Columns.Add("InPosition", System.Type.GetType("System.String"));
            tb.Columns.Add("InPositionCode", System.Type.GetType("System.String")); 
            tb.Columns.Add("PlanMode", System.Type.GetType("System.String"));
            tb.Columns.Add("PTC", System.Type.GetType("System.String"));
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
                newRow["Unit"] = ((Label)gRow.FindControl("LabelUnit")).Text;
                newRow["WN"] = ((Label)gRow.FindControl("LabelWN")).Text; 
                newRow["Number"] = ((TextBox)gRow.FindControl("TextBoxNumber")).Text;
                newRow["WQN"] = ((Label)gRow.FindControl("LabelWQN")).Text;
                newRow["QN"] = ((TextBox)gRow.FindControl("TextBoxQN")).Text;
                newRow["Comment"] = ((TextBox)gRow.FindControl("TextBoxComment")).Text;
                newRow["OutWarehouse"] = ((Label)gRow.FindControl("LabelOutWarehouse")).Text;
                newRow["OutWarehouseCode"] = ((Label)gRow.FindControl("LabelOutWarehouseCode")).Text;
                newRow["OutPosition"] = ((Label)gRow.FindControl("LabelOutPosition")).Text;
                newRow["OutPositionCode"] = ((Label)gRow.FindControl("LabelOutPositionCode")).Text;
                newRow["InWarehouse"] = ((Label)gRow.FindControl("LabelInWarehouse")).Text;
                newRow["InWarehouseCode"] = ((Label)gRow.FindControl("LabelInWarehouseCode")).Text;
                newRow["InPosition"] = ((TextBox)gRow.FindControl("TextBoxInPosition")).Text;
                newRow["InPositionCode"] = ((HtmlInputText)gRow.FindControl("InputInPositionCode")).Value;
                newRow["PlanMode"] = ((Label)gRow.FindControl("LabelPlanMode")).Text;
                newRow["PTC"] = ((Label)gRow.FindControl("LabelPTC")).Text;
                newRow["OrderID"] = ((Label)gRow.FindControl("LabelOrderID")).Text;
                tb.Rows.Add(newRow);
            }
            return tb;
        }

        protected void Append_Click(object sender, EventArgs e)
        {
                DataTable dt = getDataFromGridView();
                string sql = "SELECT UniqueID='',SQCODE AS SQCODE,MaterialCode AS MaterialCode," +
                    "MaterialName AS MaterialName,Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard," +
                    "Fixed AS Fixed,CAST(Length AS VARCHAR(50)) AS Length," +
                    "CAST(Width AS VARCHAR(50)) AS Width,LotNumber AS LotNumber,PlanMode AS PlanMode,PTC AS PTC," +
                    "WarehouseCode AS OutWarehouseCode,Warehouse AS OutWarehouse,LocationCode AS OutPositionCode," +
                    "Location AS OutPosition,InWarehouseCode='" + DropDownListWarehouseIn.SelectedValue + "',InWarehouse='" + DropDownListWarehouseIn.SelectedItem.Text + "',InPositionCode='0',InPosition='待查'," +
                    "Unit AS Unit,CAST(Number AS VARCHAR(50)) AS WN,CAST(Number AS VARCHAR(50)) AS Number," +
                    "CAST(SupportNumber AS VARCHAR(50)) AS WQN,CAST(SupportNumber AS VARCHAR(50)) AS QN," +
                    "OrderCode AS OrderID,Note AS Comment " +
                    "FROM View_SM_Storage WHERE State='APPENDAL" + Session["UserID"].ToString() + "'";
                dt.Merge(DBCallCommon.GetDTUsingSqlText(sql));
                GridView1.DataSource = dt;
                GridView1.DataBind();
                sql = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='APPENDAL" + Session["UserID"].ToString() + "'";
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

 
        //调拨单保存
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
                string WarehouseInCode = DropDownListWarehouseIn.SelectedValue;
                string DepCode = DropDownListDep.SelectedValue;
                //string ClerkCode = DropDownListClerk.SelectedValue;
                string ClerkCode = "";
                string DocCode = LabelDocCode.Text;
                sql = "DELETE FROM TBWS_ALLOCATION WHERE AL_CODE='" + Code + "'";
                sqllist.Add(sql);
                sql = "INSERT INTO TBWS_ALLOCATION(AL_CODE,AL_DATE," +
                    "AL_WAREHOUSE,AL_DEP,AL_CLERK,AL_DOC," +
                    "AL_VERIFIER,AL_VERIFYDATE,AL_STATE,AL_NOTE) VALUES('" + Code + "','" + Date + "','" +
                    WarehouseInCode + "','" + DepCode + "','" +
                    ClerkCode + "','" +
                    DocCode + "','','','1','')";
                sqllist.Add(sql);
                sql = "DELETE FROM TBWS_ALLOCATIONDETAIL WHERE AL_CODE='" + Code + "'";
                sqllist.Add(sql);
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    string UniqueID = Code + (i + 1).ToString().PadLeft(3, '0');
                    string SQCODE = ((Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;
                    string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text;
                    string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;
                    float Length = 0;
                    try
                    { Length = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelLength")).Text); }
                    catch
                    { Length = 0; }
                    float Width = 0;
                    try
                    { Width = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelWidth")).Text); }
                    catch
                    { Width = 0; }
                    string LotNumber = ((Label)this.GridView1.Rows[i].FindControl("LabelLotNumber")).Text;
                    string Unit = ((Label)this.GridView1.Rows[i].FindControl("LabelUnit")).Text;
                    float WN = 0;
                    try
                    { WN = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelWN")).Text); }
                    catch
                    { WN = 0; }
                    float Number = 0;
                    try
                    { Number = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxNumber")).Text); }
                    catch
                    { Number = 0; }
                    Int32 WQN = 0;
                    try
                    { WQN = Convert.ToInt32(((Label)this.GridView1.Rows[i].FindControl("LabelWQN")).Text); }
                    catch
                    { WQN = 0; }
                    Int32 QN = 0;
                    try
                    { QN = Convert.ToInt32(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxQN")).Text); }
                    catch
                    { QN = 0; }
                    string Comment = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;
                    string OutWarehouse = ((Label)this.GridView1.Rows[i].FindControl("LabelOutWarehouse")).Text;
                    string OutWarehouseCode = ((Label)this.GridView1.Rows[i].FindControl("LabelOutWarehouseCode")).Text;
                    string OutPosition = ((Label)this.GridView1.Rows[i].FindControl("LabelOutPosition")).Text;
                    string OutPositionCode = ((Label)this.GridView1.Rows[i].FindControl("LabelOutPositionCode")).Text;
                    string InWarehouse = ((Label)this.GridView1.Rows[i].FindControl("LabelInWarehouse")).Text;
                    string InWarehouseCode = ((Label)this.GridView1.Rows[i].FindControl("LabelInWarehouseCode")).Text;
                    string InPosition = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxInPosition")).Text;
                    string InPositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputInPositionCode")).Value;
                    string PlanMode = ((Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text;
                    string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;
                    string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                    sql = "INSERT INTO TBWS_ALLOCATIONDETAIL(AL_CODE,AL_UNIQUEID,AL_SQCODE,AL_MARID," +
                        "AL_FIXED,AL_LENGTH,AL_WIDTH,AL_LOTNUM,AL_KTNUM,AL_TZNUM,AL_KTFZNUM,AL_TZFZNUM," +
                        "AL_WAREHOUSEFROM,AL_LOCATIONFROM,AL_WAREHOUSETO,AL_LOCATIONTO," +
                        "AL_ORDERID,AL_PMODE,AL_PTCODE,AL_NOTE,AL_STATE) VALUES('" + Code + "','" + UniqueID + "','" +
                        SQCODE + "','" + MaterialCode + "','" + Fixed + "','" + Length + "','" +
                        Width + "','" + LotNumber + "','" + WN + "','" + Number + "','" + WQN + "','" + QN + "','" +
                        OutWarehouseCode + "','" + OutPositionCode + "','" + InWarehouseCode + "','" +
                         InPositionCode + "','" + OrderID + "','" + PlanMode + "','" + PTC + "','" + Comment + "','')";
                    sqllist.Add(sql);
                }
                DBCallCommon.ExecuteTrans(sqllist);
                LabelMessage.Text = "保存成功！";

                //string script = @"alert('保存成功！');";

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

            }
        }

        //调拨单提交，还需要做提交检验！
        protected void Submit_Click(object sender, EventArgs e)
        {
            //提交操作
            List<string> sqllist = new List<string>();
            string sql = "";

            string Code = LabelCode.Text;
            string Date = TextBoxDate.Text;
            string WarehouseInCode = DropDownListWarehouseIn.SelectedValue;
            string DepCode = DropDownListDep.SelectedValue;
            //string ClerkCode = DropDownListClerk.SelectedValue;
            string ClerkCode = "";
            string DocCode = LabelDocCode.Text;
            sql = "DELETE FROM TBWS_ALLOCATION WHERE AL_CODE='" + Code + "'";
            sqllist.Add(sql);
            sql = "INSERT INTO TBWS_ALLOCATION(AL_CODE,AL_DATE," +
                "AL_WAREHOUSE,AL_DEP,AL_CLERK,AL_DOC," +
                "AL_VERIFIER,AL_VERIFYDATE,AL_STATE,AL_NOTE) VALUES('" + Code + "','" + Date + "','" +
                WarehouseInCode + "','" + DepCode + "','" +
                ClerkCode + "','" +
                DocCode + "','','','2','')";
            sqllist.Add(sql);
            sql = "DELETE FROM TBWS_ALLOCATIONDETAIL WHERE AL_CODE='" + Code + "'";
            sqllist.Add(sql);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string UniqueID = Code + (i + 1).ToString().PadLeft(3, '0');
                string SQCODE = ((Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;
                string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text;
                string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;
                float Length = 0;
                try
                { Length = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelLength")).Text); }
                catch
                { Length = 0; }
                float Width = 0;
                try
                { Width = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelWidth")).Text); }
                catch
                { Width = 0; }
                string LotNumber = ((Label)this.GridView1.Rows[i].FindControl("LabelLotNumber")).Text;
                string Unit = ((Label)this.GridView1.Rows[i].FindControl("LabelUnit")).Text;
                float WN = 0;
                try
                { WN = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelWN")).Text); }
                catch
                { WN = 0; }
                float Number = 0;
                try
                { Number = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxNumber")).Text); }
                catch
                { Number = 0; }
                Int32 WQN = 0;
                try
                { WQN = Convert.ToInt32(((Label)this.GridView1.Rows[i].FindControl("LabelWQN")).Text); }
                catch
                { WQN = 0; }
                Int32 QN = 0;
                try
                { QN = Convert.ToInt32(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxQN")).Text); }
                catch
                { QN = 0; }
                string Comment = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;
                string OutWarehouse = ((Label)this.GridView1.Rows[i].FindControl("LabelOutWarehouse")).Text;
                string OutWarehouseCode = ((Label)this.GridView1.Rows[i].FindControl("LabelOutWarehouseCode")).Text;
                string OutPosition = ((Label)this.GridView1.Rows[i].FindControl("LabelOutPosition")).Text;
                string OutPositionCode = ((Label)this.GridView1.Rows[i].FindControl("LabelOutPositionCode")).Text;
                string InWarehouse = ((Label)this.GridView1.Rows[i].FindControl("LabelInWarehouse")).Text;
                string InWarehouseCode = ((Label)this.GridView1.Rows[i].FindControl("LabelInWarehouseCode")).Text;
                string InPosition = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxInPosition")).Text;
                string InPositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputInPositionCode")).Value;
                string PlanMode = ((Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text;
                string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;
                string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                sql = "INSERT INTO TBWS_ALLOCATIONDETAIL(AL_CODE,AL_UNIQUEID,AL_SQCODE,AL_MARID," +
                    "AL_FIXED,AL_LENGTH,AL_WIDTH,AL_LOTNUM,AL_KTNUM,AL_TZNUM,AL_KTFZNUM,AL_TZFZNUM," +
                    "AL_WAREHOUSEFROM,AL_LOCATIONFROM,AL_WAREHOUSETO,AL_LOCATIONTO," +
                    "AL_ORDERID,AL_PMODE,AL_PTCODE,AL_NOTE,AL_STATE) VALUES('" + Code + "','" + UniqueID + "','" +
                    SQCODE + "','" + MaterialCode + "','" + Fixed + "','" + Length + "','" +
                    Width + "','" + LotNumber + "','" + WN + "','" + Number + "','" + WQN + "','" + QN + "','" +
                    OutWarehouseCode + "','" + OutPositionCode + "','" + InWarehouseCode + "','" +
                     InPositionCode + "','" + OrderID + "','" + PlanMode + "','" + PTC + "','" + Comment + "','')";
                sqllist.Add(sql);
            }
            DBCallCommon.ExecuteTrans(sqllist);
            LabelMessage.Text = "提交成功！";
            //Append.Enabled = false;
            //Delete.Enabled = false;
            //Save.Enabled = false;
            //Submit.Enabled = false;
            Response.Redirect("SM_Warehouse_Allocation.aspx?FLAG=OPEN&&ID=" + LabelCode.Text);
            //string script = @"alert('提交成功！');";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
        }

        //反提交
        protected void AntiSubmit_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE TBWS_ALLOCATION SET AL_STATE='1' WHERE AL_CODE='" + LabelCode.Text + "'";
            DBCallCommon.ExeSqlText(sql);
            Response.Redirect("SM_Warehouse_Allocation.aspx?FLAG=OPEN&&ID=" + LabelCode.Text);
        }

        /*******************Tab2****************************/

        protected void Verify_Click(object sender, EventArgs e)
        {
            if (isLock() == false)
            {
                string script = @"alert('系统正在结账,请稍后...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                LabelMessage.Text = "系统正在结账,请您稍后...";
                return;
            }
            else
            {

                List<string> sqllist = new List<string>();
                string sql = "";

                string Code = LabelCode.Text; //调拨单号
                string sqlstate = "SELECT AL_STATE FROM TBWS_ALLOCATION WHERE AL_CODE='" + Code + "'";

                if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows.Count > 0)
                {
                    if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows[0]["AL_STATE"].ToString() == "3")
                    {

                        string script = @"alert('单据已审核，不能再审核！');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                        return;
                    }
                }
                string Date = TextBoxDate.Text;
                string WarehouseInCode = DropDownListWarehouseIn.SelectedValue;
                string DepCode = DropDownListDep.SelectedValue;

                //string ClerkCode = DropDownListClerk.SelectedValue;

                string ClerkCode = "";
                string DocCode = LabelDocCode.Text;
                string Advice = TextBoxAdvice.Text;
                string VerifierCode = LabelVerifierCode.Text;

                string ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                //获取审核时的关账时间，看其是否核算

                ClosingAccountDate(DateTime.Now.ToString("yyyy-MM-dd"));//获取系统封账时间

                if (LabelClosingAccount.Text == "NoTime")
                {
                    ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); ;
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

                    }
                    else
                    {
                        //有反核算
                        //得看上次核算时间，是不是本月的
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
                            }
                        }
                        else
                        {
                            //第一次审核
                            ApproveDate = getNextMonth() + " 07:59:59";
                        }
                    }

                }

                sql = "DELETE FROM TBWS_ALLOCATION WHERE AL_CODE='" + Code + "'";

                sqllist.Add(sql);

                sql = "INSERT INTO TBWS_ALLOCATION(AL_CODE,AL_DATE," +
                    "AL_WAREHOUSE,AL_DEP,AL_CLERK,AL_DOC," +
                    "AL_VERIFIER,AL_ADVICE,AL_VERIFYDATE,AL_STATE,AL_NOTE,AL_RealTime) VALUES('" + Code + "','" + Date + "','" +
                    WarehouseInCode + "','" + DepCode + "','" +
                    ClerkCode + "','" +
                    DocCode + "','" + VerifierCode + "','" + Advice + "','" + ApproveDate + "','2','',convert(varchar(50),getdate(),120))";

                sqllist.Add(sql);

                sql = "DELETE FROM TBWS_ALLOCATIONDETAIL WHERE AL_CODE='" + Code + "'";

                sqllist.Add(sql);

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    string UniqueID = Code + (i + 1).ToString().PadLeft(3, '0');
                    //由于调拨不涉及到长宽，故可以直接读取仓库唯一号
                    string SQCODE = ((Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;
                    string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text;
                    string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;
                    float Length = 0;
                    try
                    { Length = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelLength")).Text); }
                    catch
                    { Length = 0; }
                    float Width = 0;
                    try
                    { Width = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelWidth")).Text); }
                    catch
                    { Width = 0; }
                    string LotNumber = ((Label)this.GridView1.Rows[i].FindControl("LabelLotNumber")).Text;
                    string Unit = ((Label)this.GridView1.Rows[i].FindControl("LabelUnit")).Text;
                    float WN = 0;
                    try
                    { WN = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelWN")).Text); }
                    catch
                    { WN = 0; }
                    float Number = 0;
                    try
                    { Number = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxNumber")).Text); }
                    catch
                    { Number = 0; }
                    Int32 WQN = 0;
                    try
                    { WQN = Convert.ToInt32(((Label)this.GridView1.Rows[i].FindControl("LabelWQN")).Text); }
                    catch
                    { WQN = 0; }
                    Int32 QN = 0;
                    try
                    { QN = Convert.ToInt32(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxQN")).Text); }
                    catch
                    { QN = 0; }
                    string Comment = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;
                    string OutWarehouse = ((Label)this.GridView1.Rows[i].FindControl("LabelOutWarehouse")).Text;
                    string OutWarehouseCode = ((Label)this.GridView1.Rows[i].FindControl("LabelOutWarehouseCode")).Text;
                    string OutPosition = ((Label)this.GridView1.Rows[i].FindControl("LabelOutPosition")).Text;
                    string OutPositionCode = ((Label)this.GridView1.Rows[i].FindControl("LabelOutPositionCode")).Text;
                    string InWarehouse = ((Label)this.GridView1.Rows[i].FindControl("LabelInWarehouse")).Text;
                    string InWarehouseCode = ((Label)this.GridView1.Rows[i].FindControl("LabelInWarehouseCode")).Text;
                    string InPosition = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxInPosition")).Text;
                    string InPositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputInPositionCode")).Value;
                    string PlanMode = ((Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text;
                    string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;
                    string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                    sql = "INSERT INTO TBWS_ALLOCATIONDETAIL(AL_CODE,AL_UNIQUEID,AL_SQCODE,AL_MARID," +
                        "AL_FIXED,AL_LENGTH,AL_WIDTH,AL_LOTNUM,AL_KTNUM,AL_TZNUM,AL_KTFZNUM,AL_TZFZNUM," +
                        "AL_WAREHOUSEFROM,AL_LOCATIONFROM,AL_WAREHOUSETO,AL_LOCATIONTO," +
                        "AL_ORDERID,AL_PMODE,AL_PTCODE,AL_NOTE,AL_STATE) VALUES('" + Code + "','" + UniqueID + "','" +
                        SQCODE + "','" + MaterialCode + "','" + Fixed + "','" + Length + "','" +
                        Width + "','" + LotNumber + "','" + WN + "','" + Number + "','" + WQN + "','" + QN + "','" +
                        OutWarehouseCode + "','" + OutPositionCode + "','" + InWarehouseCode + "','" +
                         InPositionCode + "','" + OrderID + "','" + PlanMode + "','" + PTC + "','" + Comment + "','')";
                    sqllist.Add(sql);
                }
                DBCallCommon.ExecuteTrans(sqllist);

                sql = DBCallCommon.GetStringValue("connectionStrings");
                SqlConnection con = new SqlConnection(sql);
                con.Open();
                SqlCommand cmd = new SqlCommand("Allocation", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@AllocationCode", SqlDbType.VarChar, 50);				//增加参数
                cmd.Parameters["@AllocationCode"].Value = LabelCode.Text;				        //为参数初始化
                cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;               //增加参数

                cmd.ExecuteNonQuery();
                con.Close();

                if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 0)
                {
                    Response.Redirect("SM_Warehouse_Allocation.aspx?FLAG=OPEN&&ID=" + LabelCode.Text);
                }
                if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
                {
                    LabelMessage.Text = "无法通过审核：部分物料不存在！";

                    string script = @"alert('无法通过审核：部分物料不存在！');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                }
                if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
                {
                    LabelMessage.Text = "无法通过审核：部分物料库存数量小于调拨数量！";

                    string script = @"alert('无法通过审核：无法通过审核：部分物料库存数量小于调拨数量！');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                }
            }
        }
        //驳回审核
        //将AL_STATE改为未提交状态
        protected void Deny_Click(object sender, EventArgs e)
        {

            string sql = "UPDATE TBWS_ALLOCATION SET AL_STATE='1',AL_VSTATE='1' WHERE AL_CODE='" + LabelCode.Text + "'";
            DBCallCommon.ExeSqlText(sql);
            Verify.Enabled = false;
            Deny.Enabled = false;
        }

        protected bool isLock()
        {
            string nowyear = DateTime.Now.Year.ToString();
            string nowmonth = DateTime.Now.Month.ToString();
            string sqllock = "select HS_KEY from TBWS_LOCKTABLE where HS_YEAR='" + nowyear + "' AND HS_MONTH='" + nowmonth + "'";
            bool flag = true;
            try
            {
                SqlDataReader drlock = DBCallCommon.GetDRUsingSqlText(sqllock, 5);

                if (drlock.Read())
                {
                    if (drlock["HS_KEY"].ToString() == "1")
                    {
                        flag = false;
                    }
                }
                return flag;
            }
            catch (Exception)
            {
                return false;
            }
        }

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
                if (InputState.Value == "0")
                {
                    LabelMessage.Text = "当前调拨单尚未保存！";
                    return;
                }
                if (InputState.Value == "1")
                {
                    List<string> sqllist = new List<string>();
                    string sql = "DELETE FROM TBWS_ALLOCATION WHERE AL_CODE='" + LabelCode.Text + "'";
                    sqllist.Add(sql);
                    sql = "DELETE FROM TBWS_ALLOCATIONDETAIL WHERE AL_CODE='" + LabelCode.Text + "'";
                    sqllist.Add(sql);
                    DBCallCommon.ExecuteTrans(sqllist);

                    //Response.Redirect("SM_Warehouse_AllocationManage.aspx");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "closewin();", true);
                }
                if (InputState.Value == "2")
                {
                    LabelMessage.Text = "待审核的调拨单无法删除！";
                    return;
                }
                if (InputState.Value == "3")
                {
                    LabelMessage.Text = "已审核的调拨单无法删除！";
                    return;
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
            }
            else
            {
                if (InputState.Value == "0")
                {
                    LabelMessage.Text = "当前调拨单尚未保存无法反审！";
                    return;
                }
                if (InputState.Value == "1")
                {
                    LabelMessage.Text = "当前调拨单尚未提交无法反审！";
                    return;
                }
                if (InputState.Value == "2")
                {
                    LabelMessage.Text = "当前调拨单尚未审核无法反审！";
                    return;
                }
                if (InputState.Value == "3")
                {
                    string sql = DBCallCommon.GetStringValue("connectionStrings");
                    SqlConnection con = new SqlConnection(sql);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("AntiVerifyAllocation", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@AllocationCode", SqlDbType.VarChar, 50);				//增加参数
                    cmd.Parameters["@AllocationCode"].Value = LabelCode.Text;				        //为参数初始化
                    cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;               //增加参数

                    cmd.ExecuteNonQuery();
                    con.Close();

                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 0)
                    {
                        Response.Redirect("SM_Warehouse_Allocation.aspx?FLAG=OPEN&&ID=" + LabelCode.Text);
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
                    {
                        LabelMessage.Text = "无法通过审核：部分物料不存在！";
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
                    {
                        LabelMessage.Text = "无法通过审核：部分物料库存数量小于调拨数量！";
                    }
                }
            }
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
            lt.Add("批号");
            lt.Add("是否定尺");
            lt.Add("长");
            lt.Add("宽");
            lt.Add("可调拨数量");
            lt.Add("可调拨张(支)数");
            lt.Add("订单单号");
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
        #endregion

    }
}
