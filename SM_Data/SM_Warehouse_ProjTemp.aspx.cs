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
    public partial class SM_Warehouse_ProjTemp : System.Web.UI.Page
    {
        float twn = 0;
        Int32 twqn = 0;
        float tan = 0;
        Int32 taqn = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
           if (!IsPostBack)
           {
               ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
                getWarehouse();
                GetDep();
                GetStaff();
                initial();      //初始化
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
                        tb.Text = DropDownListPTCTo.SelectedItem.Text + "-PT" + Convert.ToInt64(LabelCode.Text.Trim().Replace("PT", "0")).ToString();
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
                        tb.Text = DropDownListPTCTo.SelectedItem.Text + "-PT" + Convert.ToInt64(LabelCode.Text.Trim().Replace("PT", "0")).ToString();
                        IsSelect = false;
                    }
                }
            }
        }

        protected void GetDep()
        {
            string sql = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO where DEP_CODE='03'or DEP_CODE='09' ";
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

            string sql = "SELECT DISTINCT ST_CODE,ST_NAME FROM TBDS_STAFFINFO WHERE  ST_DEPID='" + DropDownListDep.SelectedValue + "' and st_state='0' and (ST_POSITION<>'技术部部长' and ST_POSITION<>'电气制造部部长')";
            
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
            this.Page.Title = "项目结转备库(" + code + ")";

            if (flag == "PUSHPROJTEMP")
            {
                LabelCode.Text = generateCode();
                TextBoxDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                string sql = "insert into TBWS_PROJTEMPCODE (PT_CODE) VALUES ('" + LabelCode.Text + "')";

                DBCallCommon.ExeSqlText(sql);
                LabelState.Text = "0";
                ClosingAccountDate(TextBoxDate.Text.Trim());
                TextBoxComment.Text = "";
                LabelDoc.Text = Session["UserName"].ToString(); //制单人
                LabelDocCode.Text = Session["UserID"].ToString();
                LabelSubmitter.Text = Session["UserName"].ToString(); ;  //提交人
                LabelSubmitterCode.Text = Session["UserID"].ToString();
                LabelApproveDate.Text = "";
                LabelSubmitDate.Text = "";
                LabelVerifierCode.Text = ""; //审核人
                 
                sql = "SELECT UniqueID='',SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,Length AS Length," +
                    "Width AS Width,LotNumber AS LotNumber,PlanMode AS PlanMode,PTC AS PTCFrom," +
                    "WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,LocationCode AS PositionCode," +
                    "Location AS Position,Unit AS Unit,cast(Number as float) AS WN,cast(SupportNumber as float) AS WQN,PTCTO='--请选择--',cast(Number as float) AS AdjN," +
                    "cast(SupportNumber as float) AS AdjQN,OrderCode AS OrderID,CGMODE as Cgmode,Note AS Note,ShengYuNote='' " +
                    "FROM View_SM_Storage WHERE State='PROJTEMP" + Session["UserID"].ToString() + "'";


                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);


                GridView1.DataSource = dt;
                GridView1.DataBind();

                sql = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='PROJTEMP" + Session["UserID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
                if (LabelState.Text == "0") //储运未提交
                {
                   
                        Append.Visible = true;
                        Delete.Visible = true;
                        Save.Visible = true; ;
                        Verifiy.Visible = false;
                        AntiVerify.Visible = false;
                        DeleteBill.Visible = true;                       
                        ImageVerify.Visible = false;
                        Submit.Visible = true;
                        AntiSubmit.Visible = false;

                }
            }

            if (flag == "OPEN")
            {
                string sql = "SELECT TOP 1 PT_CODE AS Code,PT_DATE AS Date,PT_TARGETID AS PTCToCode," +
                    "depcode AS DepCode,depname AS DepName,submittercode AS SubmitterCode,submitter as Submitter,left(submitdate,10) as SubmitDate," +
                    "doccode AS DocCode,docname AS Doc,verifercode AS VerifierCode,verifername AS Verifier," +
                    "left(PT_VERIFYDATE,10) AS ApproveDate,state AS State,note AS Comment,warehousecode as WarehouseCode,locationcode as PositionCode,managercode,manager,PT_MANAGERNOTE,left(PT_MANAGERTIME,10) as managertime, " +
                     "sczh as SCZH,pjname AS PROJNAME,engname AS ENGNAME " +
                    "FROM View_SM_PROJTEMP WHERE PT_CODE='" + code + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    LabelCode.Text = code;
                   
                    LabelState.Text = dr["State"].ToString();

                    TextBoxDate.Text = dr["Date"].ToString();

                    ClosingAccountDate(System.DateTime.Now.ToString("yyyy-MM-dd"));

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

                    try { DropDownListPlaner.Items.FindByValue(dr["VerifierCode"].ToString()).Selected = true; }
                    catch { }

                    LabelDoc.Text = dr["Doc"].ToString();
                    LabelDocCode.Text = dr["DocCode"].ToString();
                    LabelSubmitter.Text = dr["Submitter"].ToString(); 
                    LabelSubmitterCode.Text = dr["SubmitterCode"].ToString();
                    LabelApproveDate.Text = dr["ApproveDate"].ToString();
                    LabelVerifierCode.Text = dr["VerifierCode"].ToString();
                    LabelSubmitDate.Text = dr["SubmitDate"].ToString();
                    LabelVerifierManager.Text = dr["manager"].ToString(); //审批人名
                    LabelManagercode.Text = dr["managercode"].ToString(); //审批人代码
                    TextBoxVerifyNote.Text = dr["PT_MANAGERNOTE"].ToString(); //审批意见
                    LabelTime.Text = dr["managertime"].ToString(); //审批时间
                    LabelShenpi.Text = dr["managertime"].ToString();
                    TextBoxSCZH.Text=dr["SCZH"].ToString(); //生产制号
                    TextBoxProjName.Text=dr["PROJNAME"].ToString();
                    TextBoxEngName.Text = dr["ENGNAME"].ToString();

                }
                dr.Close();

                sql = "SELECT uniqueid AS UniqueID,SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Standard AS MaterialStandard,Fixed AS Fixed,Length AS Length," +
                    "Width AS Width,Attribute AS Attribute,GB AS GB,LotNumber AS LotNumber," +
                    "PTCFrom AS PTCFrom,warehousecode AS WarehouseCode,Warehouse AS Warehouse,locationcode AS PositionCode,Location AS Position," +
                    "Unit AS Unit,cast(NUM as float) AS WN,cast(FNUM as float) AS WQN,cast(TNUM as float) AS AdjN,cast(TFNUM as float) AS AdjQN,PTCTo AS PTCTo," +
                    "PlanMode AS PlanMode,dstate AS State,OrderCode AS OrderID,dnote AS Note,PT_CGMODE as Cgmode,shengyunote as ShengYuNote   FROM View_SM_PROJTEMP WHERE PT_CODE='" + code + "'";
                DataTable tb = new DataTable();
                tb = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = tb;
                GridView1.DataBind();
                if (LabelState.Text == "0") //储运未提交
                {
                    ImageVerify.Visible = false;
                    if (Session["UserID"].ToString() == LabelDocCode.Text.Trim())
                    {                       
                        Verifiy.Visible = false;
                        AntiVerify.Visible = false;                     
                        AntiSubmit.Visible = false;

                    }
                    else
                    {
                        Append.Visible = false;
                        Delete.Visible = false;
                        Save.Visible = false; ;
                        Verifiy.Visible = false;
                        AntiVerify.Visible = false;
                        DeleteBill.Visible = false;
                        Split.Visible = false;                                     
                        Submit.Visible = false;
                        AntiSubmit.Visible = false;
                       
                        if (Session["UserDeptID"].ToString() != "07")
                        {
                            btnstorge.Visible = false;
                        }
                        
                        ControlEnable(false);
                    }


                }
                if (LabelState.Text == "1") //已经提交待技术部提交，
                {
                    ControlEnable(false);
                    if (Session["UserDeptID"].ToString() != "07")
                    {
                        btnstorge.Visible = false;
                    }                    
                    ImageVerify.Visible = false;
                    DeleteBill.Visible = false;
                    Save.Visible = false;                   
                    Append.Visible = false;
                    Split.Visible = false;
                    Submit.Visible = false;
                    AntiVerify.Visible = false;
                    if (Session["UserID"].ToString() == LabelDocCode.Text.Trim())
                    {             
                        Verifiy.Visible = false;
                        Delete.Visible = false;
                    }
                    else if (Session["UserID"].ToString() == LabelVerifierCode.Text.Trim())
                    {
                        AntiSubmit.Visible = false;
                        Append.Visible = true;
                        
                    }
                    else
                    {    
                        Verifiy.Visible = false;                     
                        AntiSubmit.Visible = false;
                        Delete.Visible = false;                 
                    }

                }
                if (LabelState.Text == "2")//技术员已经审核，待部长审核
                {
                    ControlEnable(false);
                    ImageVerify.Visible = false;
                    btnPrint.Visible = false;
                    Append.Visible = false;
                    Delete.Visible = false;
                    Save.Visible = false;
                    Verifiy.Visible = false;
                    AntiVerify.Visible = false; //部长审核后才能反审核
                    DeleteBill.Visible = false;
                    AntiSubmit.Visible = false;
                    Submit.Visible = false;
                    Split.Visible = false;
                    btnstorge.Visible = false;                    
                    if (Session["UserID"].ToString() == LabelManagercode.Text.Trim())
                    {
                        ManagerVerify.Visible = true;
                    }
                   
                }
                if (LabelState.Text == "3") //审批同意
                {
                    ControlEnable(false);
                    ImageVerify.Visible = true;
                    btnPrint.Visible = true;
                    Append.Visible = false;
                    Delete.Visible = false;
                    Save.Visible = false;
                    Verifiy.Visible = false;
                    DeleteBill.Visible = false;                 
                    AntiSubmit.Visible = false;
                    Submit.Visible = false;
                    Split.Visible = false;
                    ButtonVerify_Confirm.Enabled = false;//审批提交
                    RadioButtonListVerify.Enabled = false; //审批意见 
                    ButtonVerify_BoHui.Enabled = false;  //驳回
                    if (Session["UserDeptID"].ToString() != "07")
                    {
                        btnstorge.Visible = false;
                    }

                    if (Session["UserID"].ToString() == LabelVerifierCode.Text.Trim())
                    {
                        AntiVerify.Visible = true;
                    }
                    else
                    {
                        AntiVerify.Visible = false;

                    }

                    if (Session["UserID"].ToString() == LabelManagercode.Text.Trim())
                    {
                        ManagerVerify.Visible = true;
                    }
                    else
                    {
                        ManagerVerify.Visible = false;

                    }
                    RadioButtonListVerify.SelectedIndex=0;
                }
               
            }
        }
       
        private void ControlEnable(bool isVisual)
        { 
            if(isVisual==false)
            {
                TextBoxSCZH.Enabled = false;//生产制号
                TextBoxProjName.Enabled = false;
                TextBoxEngName.Enabled = false;
                DropDownListPTCTo.Enabled = false;
                DropDownListWarehouse.Enabled = false;
                DropDownListPosition.Enabled = false;
                DropDownListPlaner.Enabled = false;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];                   
                    ((TextBox)gRow.FindControl("TextBoxGVPTCTo")).Enabled=false;;
                    ((TextBox)gRow.FindControl("TextBoxPosition")).Enabled=false;
                    ((TextBox)gRow.FindControl("TextBoxAdjN")).Enabled=false;
                    ((TextBox)gRow.FindControl("TextBoxAdjQN")).Enabled=false;
                    ((TextBox)gRow.FindControl("TextBoxPlanMode")).Enabled=false;
                }
            }
        }


        //生成项目结转单号
        protected string generateCode()
        {
            string Code = "";
            string sql = "SELECT MAX(PT_CODE) AS MaxCode FROM TBWS_PROJTEMPCODE WHERE LEN(PT_CODE)=10";
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
                return "PT00000001";
            }
            else
            {
                int tempnum = Convert.ToInt32((Code.Substring(2,8)));
                tempnum++;
                Code = "PT" + tempnum.ToString().PadLeft(8, '0');
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
            tb.Columns.Add("Cgmode", System.Type.GetType("System.String"));
            tb.Columns.Add("ShengYuNote", System.Type.GetType("System.String"));

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
                newRow["Cgmode"] = ((Label)gRow.FindControl("Labelcgmode")).Text;
                newRow["ShengYuNote"] = ((TextBox)gRow.FindControl("TextBoxShengYuNote")).Text;

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
                "CAST(CAST(SupportNumber AS float) AS CHAR(50)) AS AdjQN,Note AS Note,ShengYuNote='',OrderCode AS OrderID,CGMODE as Cgmode " +
                "FROM View_SM_Storage WHERE State='APPENDPROJTEMP" + Session["UserID"].ToString() + "'";
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
            temp.Columns.Add("ShengYuNote", System.Type.GetType("System.String"));

            temp = DBCallCommon.GetDTUsingSqlText(sql);
            tb.Merge(temp);
            GridView1.DataSource = tb;
            GridView1.DataBind();
            sql = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='APPENDMTO" + Session["UserID"].ToString() + "'";
            DBCallCommon.ExeSqlText(sql);
            LabelMessage.Text = "追加成功！";
            CheckSQCODE();//检查是否有重复的计划跟踪号
        }

        protected void CheckSQCODE() //有重复的计划跟踪号有颜色区分
        {

            for (int i = 0; i < (GridView1.Rows.Count - 1); i++)
            {
                GridViewRow gvrow0 = GridView1.Rows[i];
                string sqcode = ((Label)gvrow0.FindControl("LabelPTCFrom")).Text;
                for (int j = i + 1; j < GridView1.Rows.Count; j++)
                {
                    GridViewRow gvrow1 = GridView1.Rows[j];
                    string nextsqcode = ((Label)gvrow1.FindControl("LabelPTCFrom")).Text;
                    if (sqcode == nextsqcode)
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

        //单保存
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
                string VerifierCode = DropDownListPlaner.SelectedValue;
                string DepCode = DropDownListDep.SelectedValue;
                string DocCode = LabelDocCode.Text;
                string SubmitterCode= LabelSubmitterCode.Text;
                string ApproveDate = LabelApproveDate.Text;
                string sczh = TextBoxSCZH.Text;
                string prjname = TextBoxProjName.Text;
                string engname = TextBoxEngName.Text;

                sql = "DELETE FROM TBWS_PROJTEMP WHERE PT_CODE='" + Code + "'";
                sqllist.Add(sql);
                sql = "INSERT INTO TBWS_PROJTEMP(PT_CODE,PT_DATE,PT_TARGETID," +
                    "PT_DEP,PT_SUBMITTER,PT_DOC,PT_VERIFIER,PT_VERIFYDATE," +
                    "PT_STATE,PT_NOTE,PT_SCZH,PT_PJNAME,PT_ENGNAME) VALUES('" + Code + "','" +
                    Date + "','" + TargetCode + "','" + DepCode + "','" + SubmitterCode + "','" + DocCode + "','" +
                    VerifierCode + "','" + ApproveDate + "','0','" + Comment + "','" + sczh + "','" + prjname + "','" + engname + "')";
                sqllist.Add(sql);
                sql = "DELETE FROM TBWS_PROJTEMPDETAIL WHERE PT_CODE='" + Code + "'";
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
                    string CgMode = ((Label)this.GridView1.Rows[i].FindControl("Labelcgmode")).Text; //标识号
                    string Note = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxNote")).Text;
                    sql = "INSERT INTO TBWS_PROJTEMPDETAIL(PT_UNIQUEID,PT_CODE,PT_SQCODE,PT_MARID," +
                        "PT_FIXED,PT_LENGTH,PT_WIDTH,PT_PCODE,PT_FROMPTCODE," +
                        "PT_WAREHOUSECODE,PT_LOCATIONCODE,PT_NUM,PT_FNUM,PT_TOPTCODE," +
                        "PT_TNUM,PT_TKNUM,PT_PMODE,PT_ORDERCODE,PT_NOTE,PT_STATE,PT_CGMODE) VALUES('" + UniqueID + "','" +
                        Code + "','" + SQCODE + "','" + MaterialCode + "','" + Fixed + "','" +
                        Length + "','" + Width + "','" + LotNumber + "','" + PTCFrom + "','" + WarehouseCode + "','" + PositionCode + "','" +
                        WN + "','" + WQN + "','" + PTCTo + "','" + AdjN + "','" + AdjQN + "','" + PlanMode + "','" +
                        OrderID + "','" + Note + "','','"+CgMode+"')";
                    sqllist.Add(sql);
                }
                DBCallCommon.ExecuteTrans(sqllist);
                sqllist.Clear();
                LabelState.Text = "0";
                LabelMessage.Text = "保存成功！";
            }
        }
        protected void TextBoxPTCTo_TextChanged(object sender, EventArgs e)
        {
            string engid = TextBoxPTCTo.Text.Split('-')[TextBoxPTCTo.Text.Split('-').Length - 1];

            if (engid != "备库")
            {
                string sql = "select TSA_PJNAME,TSA_ENGNAME,TSA_ID FROM View_TM_TaskAssign WHERE TSA_FATHERNODE='0' and TSA_ID='" + engid + "'";
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
                    ((TextBox)GridView1.Rows[i].FindControl("TextBoxGVPTCTo")).Text = TextBoxPTCTo.Text.Split('-')[TextBoxPTCTo.Text.Split('-').Length - 1] + "_" + "PT" + Convert.ToInt64(LabelCode.Text.Replace("PT", "000")) + "_" + (i + 1).ToString().PadLeft(3, '0');
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

        protected void Verify_Click(object sender, EventArgs e)  //技术员审核提交
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

                string sqlstate = "select PT_STATE from TBWS_PROJTEMP where PT_CODE='" + Code + "'";

                if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows.Count > 0)
                {
                    if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows[0]["PT_STATE"].ToString() == "2")
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
                string VerifierCode = DropDownListPlaner.SelectedValue;
                string DepCode = DropDownListDep.SelectedValue;
                string DocCode = LabelDocCode.Text;

                string SubmitCode = LabelSubmitterCode.Text; 
                string SubmitDate = LabelSubmitDate.Text;
                string ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); 
                string strManagercode="";
                string sql0 = "select  st_code from TBDS_STAFFINFO where (st_position='技术部部长' and st_depid='" + DropDownListDep.SelectedValue + "') or (st_position='电气制造部部长' and st_depid='" + DropDownListDep.SelectedValue + "')  and st_state='0' "; //提交技术部领导审核
                SqlDataReader dr0 = DBCallCommon.GetDRUsingSqlText(sql0);
                if (dr0.Read())
                 {
                       strManagercode = dr0["st_code"].ToString();
                 }
                 dr0.Close();
                 string sczh = TextBoxSCZH.Text;
                 string prjname = TextBoxProjName.Text;
                 string engname = TextBoxEngName.Text;
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
                            }
                        }
                        else
                        {
                            //第一次审核
                            ApproveDate = getNextMonth() + " 07:59:59";
                        }
                    }

                }

                sql = "DELETE FROM TBWS_PROJTEMP WHERE PT_CODE='" + Code + "'";
                sqllist.Add(sql);
                sql = "INSERT INTO TBWS_PROJTEMP(PT_CODE,PT_DATE,PT_TARGETID," +
                    "PT_DEP,PT_SUBMITTER,PT_DOC,PT_VERIFIER,PT_VERIFYDATE," +
                    "PT_STATE,PT_NOTE,PT_RealTime,PT_SUBMITDATE,PT_MANAGER,PT_SCZH,PT_PJNAME,PT_ENGNAME) VALUES('" + Code + "','" +
                    Date + "','" + TargetCode + "','" + DepCode + "','" + SubmitCode + "','" + DocCode + "','" +
                    VerifierCode + "','" + ApproveDate + "','1','" + Comment + "','','" +
                    SubmitDate + "','"+strManagercode+"','"+sczh+"','"+prjname+"','"+engname+"')";
                sqllist.Add(sql);
                sql = "DELETE FROM TBWS_PROJTEMPDETAIL WHERE PT_CODE='" + Code + "'";
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

                    string CgMode = ((Label)this.GridView1.Rows[i].FindControl("Labelcgmode")).Text;
                    string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                    string Note = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxNote")).Text;
                    string ShengYuNote = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxShengYuNote")).Text;
                    if (ShengYuNote == string.Empty)
                    {
                        HasError = true;
                        ErrorType = 5;
                        break;
                    }
                    sql = "INSERT INTO TBWS_PROJTEMPDETAIL(PT_UNIQUEID,PT_CODE,PT_SQCODE,PT_MARID," +
                        "PT_FIXED,PT_LENGTH,PT_WIDTH,PT_PCODE,PT_FROMPTCODE," +
                        "PT_WAREHOUSECODE,PT_LOCATIONCODE,PT_NUM,PT_FNUM,PT_TOPTCODE,PT_TNUM," +
                        "PT_TKNUM,PT_PMODE,PT_ORDERCODE,PT_NOTE,PT_STATE,PT_CGMODE,PT_ShengYuNote) VALUES('" + UniqueID + "','" +
                        Code + "','" + SQCODE + "','" + MaterialCode + "','" + Fixed + "','" +
                        Length + "','" + Width + "','" + LotNumber + "','" + PTCFrom + "','" + WarehouseCode + "','" + PositionCode + "','" +
                        WN + "','" + WQN + "','" + PTCTo + "','" + AdjN + "','" + AdjQN + "','" + PlanMode + "','" +
                        OrderID + "','" + Note + "','','"+CgMode+"','"+ShengYuNote+"')";
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
                    else if (ErrorType == 5)
                    {

                        LabelMessage.Text = "请填写剩余原因！";

                        string script = @"alert('请填写剩余原因！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }

                }
                else
                {
                    if (isLock() == false)
                    {
                        string script = @"alert('系统正在结账,请稍后...');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                        LabelMessage.Text = "系统正在结账,请稍后...";
                    }
                    else
                    {  
                        DBCallCommon.ExecuteTrans(sqllist);
                        sqllist.Clear();
                        string sqlstr = "update tbws_projtemp set pt_state='2' where pt_code='" + LabelCode.Text + "'";
                        DBCallCommon.ExeSqlText(sqlstr);

                        //邮件提醒
                        #region
                        string returnvalue = "";
                        string body = "";
                        string to = "";
                        string sqlemail = "select EMail from TBDS_STAFFINFO where ST_PD='0' and  ST_CODE='" + strManagercode + "' ";

                        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlemail);

                        if (dt.Rows.Count > 0)
                        {
                            to = dt.Rows[0][0].ToString();
                        }
                        List<string> bjEmailCC = new List<string>(); //抄送给...
                        bjEmailCC.Add("erp@ztsm.net");

                        List<string> mfEmail = new List<string>(); ; //密送给..
                        mfEmail.Add(to);

                        body = "您有项目结转备库单需及时审核" + "\n" + "项目结转单号：" + LabelCode.Text + "\n" + "生产制号：" + sczh + "\n" + "项  目  为:" + prjname + "\n" + "工  程  为：" + engname;
                        returnvalue = DBCallCommon.SendEmail(to, bjEmailCC, mfEmail, prjname + "-" + engname + "-" + sczh + "数字平台项目结转备库", body);

                        if (returnvalue == "邮件已发送!")
                        {
                            //string jascript = @"alert('邮件发送成功!');";

                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", jascript, true);

                            Response.Write("<script>alert('邮件发送成功');</script>");
                        }
                        else if (returnvalue == "邮件发送失败!")
                        {
                            Response.Write("<script>alert('邮件发送不成功');</script>");

                        }
                        #endregion
                        Response.Redirect("SM_Warehouse_ProjTemp.aspx?FLAG=OPEN&&ID=" + Code);

                        
                    }
                }
            }
        }


        protected void ButtonVerify_Confirm_Click(object sender, EventArgs e)
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

                List<string> sqllist = new List<string>();
                string sqltext = "";
                if (RadioButtonListVerify.SelectedValue == "0")
                {

                    string Code = LabelCode.Text;

                    string sqlstate = "select PT_STATE from TBWS_PROJTEMP where PT_CODE='" + Code + "'";

                    if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows.Count > 0)
                    {
                        if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows[0]["PT_STATE"].ToString() == "3")
                        {                                   
                            string script = @"alert('单据已审核，单据不能再审核！');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                            return;
                        }
                    }
                   
                    string sql = DBCallCommon.GetStringValue("connectionStrings");
                    SqlConnection con = new SqlConnection(sql);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("PROJTEMP", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PTCode", SqlDbType.VarChar, 50);				//增加参数
                    cmd.Parameters["@PTCode"].Value = LabelCode.Text;							//为参数初始化
                    cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add("@ErrorRow", SqlDbType.Int, 1).Direction = ParameterDirection.Output;                    
                    cmd.ExecuteNonQuery();
                    con.Close();
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 0)
                    {
                        string managertime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        sqltext = "update tbws_projtemp set PT_MANAGERTIME='" + managertime + "',PT_MANAGERNOTE='" + TextBoxVerifyNote.Text.Trim() + "',PT_RealTime=convert(varchar(50),getdate(),120)  WHERE PT_CODE='" + LabelCode.Text + "'";
                        sqllist.Add(sqltext);
                        DBCallCommon.ExecuteTrans(sqllist);
                        sqllist.Clear();
                        //邮件提醒功能
                        #region
                        string returnvalue = "";
                        string body = "";
                        string to = "zhangchaochen@cbmi.com.cn";
                        List<string> bjEmailCC = new List<string>(); //抄送给...
                        string strsql = "select EMail from TBDS_STAFFINFO where ST_PD='0' and  ST_CODE= '" + LabelVerifierCode.Text.Trim() + "' ";
                        System.Data.DataTable dtr = DBCallCommon.GetDTUsingSqlText(strsql);
                        if (dtr.Rows.Count > 0)
                        {
                            bjEmailCC.Add(dtr.Rows[0][0].ToString());
                        }

                        string sqlemail = "select EMail from TBDS_STAFFINFO where ST_PD='0' and  ST_CODE like '0702%' ";
                        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlemail);
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                bjEmailCC.Add(dt.Rows[i][0].ToString());
                            }

                        }

                        List<string> mfEmail = null; //密送给..                       

                        body = "项目结转备库单已经审核,剩余物料已转备库" + "\n" + "项目结转单号：" + LabelCode.Text + "\n" + "生产制号：" + TextBoxSCZH.Text + "\n" + "项  目  为:" + TextBoxProjName.Text + "\n" + "工  程  为：" + TextBoxEngName.Text;
                        returnvalue = DBCallCommon.SendEmail(to, bjEmailCC, mfEmail, TextBoxProjName.Text + "-" + TextBoxEngName.Text + "-" + TextBoxSCZH.Text + "数字平台项目结转备库", body);
                        if (returnvalue == "邮件已发送!")
                        {
                            //string jascript = @"alert('邮件发送成功!');";

                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", jascript, true);

                            Response.Write("<script>alert('邮件发送成功');</script>");
                        }
                        else if (returnvalue == "邮件发送失败!")
                        {
                            Response.Write("<script>alert('邮件发送不成功');</script>");

                        }
                        #endregion
                        Response.Redirect("SM_Warehouse_ProjTemp.aspx?FLAG=OPEN&&ID=" + LabelCode.Text);

                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
                    {
                        string script = @"alert('无法通过审核：第" + Convert.ToString(cmd.Parameters["@ErrorRow"].Value) + "行物料不存在！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                        LabelMessage.Text = "无法通过审核：第" + Convert.ToString(cmd.Parameters["@ErrorRow"].Value) + "行物料不存在！";
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
                    {
                        string script = @"alert('无法通过审核：第" + Convert.ToString(cmd.Parameters["@ErrorRow"].Value) + "行物料数量小于调整数量！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                        LabelMessage.Text = "无法通过审核：第" + Convert.ToString(cmd.Parameters["@ErrorRow"].Value) + "行物料数量小于调整数量！";
                    }

                }
                else
                {
                    string script = @"alert('对不起，您不能点击此按钮，不同意请您点驳回按钮');";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    return;
                }
                
            }
                       
        }

        protected void ButtonVerify_BoHui_Click(object sender, EventArgs e)
        {

            
            if (RadioButtonListVerify.SelectedValue == "")
            {
                string script = @"alert('请您选择审批结论');";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                return;
            }
          else  if (RadioButtonListVerify.SelectedValue == "0")
            {
                string script = @"alert('对不起，您不能点击此按钮，如同意请您点提交按钮');";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                return;

            }
            else
            {
                string strNote = TextBoxVerifyNote.Text.Trim().ToString();
                if (strNote == "")
                {
                    LabelMessage.Text = "请您填写意见";

                    string script = @"alert('请您填写意见');";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    return;

                }
                else
                {
                    List<string> sqllist = new List<string>();
                    string sqltext = "";
                    string managertime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    sqltext = "update tbws_projtemp set PT_STATE='0',PT_MANAGERTIME='" + managertime + "',PT_MANAGERNOTE='" + TextBoxVerifyNote.Text.Trim() + "' WHERE PT_CODE='" + LabelCode.Text + "'";
                    sqllist.Add(sqltext);
                    DBCallCommon.ExecuteTrans(sqllist);
                    sqllist.Clear();

                    //邮件提醒功能
                    #region
                    string returnvalue = "";
                    string body = "";
                    string to = "yinyan@cbmi.com.cn";

                    List<string> bjEmailCC = new List<string>(); //抄送给...
                    string sqlemail = "select EMail from TBDS_STAFFINFO where ST_PD='0' and  ST_CODE like '0702%' ";
                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlemail);
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            bjEmailCC.Add(dt.Rows[i][0].ToString());
                        }

                    }

                    List<string> mfEmail = null; //密送给..                       

                    body = "项目结转备库单被驳回" + "\n" + "项目结转单号：" + LabelCode.Text + "\n" + "生产制号：" + TextBoxSCZH.Text + "\n" + "项  目  为:" + TextBoxProjName.Text + "\n" + "工  程  为：" + TextBoxEngName.Text;
                    returnvalue = DBCallCommon.SendEmail(to, bjEmailCC, mfEmail, TextBoxProjName.Text + "-" + TextBoxEngName.Text + "-" + TextBoxSCZH.Text + "数字平台项目结转备库", body);
                    if (returnvalue == "邮件已发送!")
                    {
                        //string jascript = @"alert('邮件发送成功!');";

                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", jascript, true);

                        Response.Write("<script>alert('邮件发送成功');</script>");
                    }
                    else if (returnvalue == "邮件发送失败!")
                    {
                        Response.Write("<script>alert('邮件发送不成功');</script>");

                    }
                    #endregion

                    Response.Redirect("SM_Warehouse_ProjTemp.aspx?FLAG=OPEN&&ID=" + LabelCode.Text);

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
                if (LabelState.Text == "0")
                {
                    LabelMessage.Text = "项目结转单尚未保存无法反审！";
                    return;
                }
                if (LabelState.Text == "1")
                {
                    LabelMessage.Text = "项目结转单尚未提交无法反审！";
                    return;
                }
                if (LabelState.Text == "2") 
                {
                    LabelMessage.Text = "项目结转单尚未领导审批无法反审！";
                    return;
                }
                if (LabelState.Text == "3")
                {
                    string sql = DBCallCommon.GetStringValue("connectionStrings");
                    SqlConnection con = new SqlConnection(sql);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("AntiVerifyPROJTEMP", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@PTCode", SqlDbType.VarChar, 50);				//增加参数
                    cmd.Parameters["@PTCode"].Value = LabelCode.Text;							//为参数初始化
                    cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    con.Close();

                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 0)
                    {
                       
                        Response.Redirect("SM_Warehouse_ProjTemp.aspx?FLAG=OPEN&&ID=" + LabelCode.Text);

                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
                    {
                        string script = @"alert('无法反审：部分物料不存在！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                        LabelMessage.Text = "无法反审：部分物料不存在！";
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
                    {
                        string script = @"alert('无法反审：部分物料数量小于调整数量！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                        LabelMessage.Text = "无法反审：部分物料数量小于调整数量！";
                    }
                }
            }
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            if (isLock() == false)
            {
                string script = @"alert('系统正在结账,请稍后...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                LabelMessage.Text = "系统正在结账,请稍后...";
            }
            else
            {
                
                bool HasError = false;
                int ErrorType = 0;

                List<string> sqllist = new List<string>();
                string sql = "";
                string Code = LabelCode.Text;
                string Date = TextBoxDate.Text;

                string TargetCode = TextBoxPTCTo.Text.Split('-')[TextBoxPTCTo.Text.Split('-').Length - 1];

                string Comment = TextBoxComment.Text;
                string VerifierCode = DropDownListPlaner.SelectedValue; //指定审核人
                string DepCode = DropDownListDep.SelectedValue;
                string DocCode = LabelDocCode.Text;

                string SubmitCode = Session["UserID"].ToString(); //提交人

                string SubmitDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //提交日期
                string Taskid = TextBoxSCZH.Text.Trim().ToString();//生产制号
                string Projname = TextBoxProjName.Text.Trim().ToString();
                string Engname = TextBoxEngName.Text.Trim().ToString();
                if (Taskid==""|| Projname==""||Engname=="")
                {
                    LabelMessage.Text = "生产制号、项目名称、工程名称为空，单据不能提交！";

                    string script = @"alert('生产制号、项目名称、工程名称为空，单据不能提交！');";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    return;
                }

                sql = "DELETE FROM TBWS_PROJTEMP WHERE PT_CODE='" + Code + "'";
                sqllist.Add(sql);
                sql = "INSERT INTO TBWS_PROJTEMP(PT_CODE,PT_DATE,PT_TARGETID," +
                    "PT_DEP,PT_SUBMITTER,PT_DOC,PT_VERIFIER,PT_VERIFYDATE," +
                    "PT_STATE,PT_NOTE,PT_RealTime,PT_SUBMITDATE,PT_SCZH,PT_PJNAME,PT_ENGNAME) VALUES('" + Code + "','" +
                    Date + "','" + TargetCode + "','" + DepCode + "','" + SubmitCode + "','" + DocCode + "','" +
                    VerifierCode + "','','1','" + Comment + "','','"+SubmitDate+"','"+Taskid+"','"+Projname+"','"+Engname+"')";
                sqllist.Add(sql);
                sql = "DELETE FROM TBWS_PROJTEMPDETAIL WHERE PT_CODE='" + Code + "'";
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

                    string CgMode = ((Label)this.GridView1.Rows[i].FindControl("Labelcgmode")).Text;
                    string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                    string Note = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxNote")).Text;
                    sql = "INSERT INTO TBWS_PROJTEMPDETAIL(PT_UNIQUEID,PT_CODE,PT_SQCODE,PT_MARID," +
                        "PT_FIXED,PT_LENGTH,PT_WIDTH,PT_PCODE,PT_FROMPTCODE," +
                        "PT_WAREHOUSECODE,PT_LOCATIONCODE,PT_NUM,PT_FNUM,PT_TOPTCODE,PT_TNUM," +
                        "PT_TKNUM,PT_PMODE,PT_ORDERCODE,PT_NOTE,PT_STATE,PT_CGMODE) VALUES('" + UniqueID + "','" +
                        Code + "','" + SQCODE + "','" + MaterialCode + "','" + Fixed + "','" +
                        Length + "','" + Width + "','" + LotNumber + "','" + PTCFrom + "','" + WarehouseCode + "','" + PositionCode + "','" +
                        WN + "','" + WQN + "','" + PTCTo + "','" + AdjN + "','" + AdjQN + "','" + PlanMode + "','" +
                        OrderID + "','" + Note + "','','"+CgMode+"')";
                    sqllist.Add(sql);
                }

                if (HasError)
                {
                    sqllist.Clear();

                    if (ErrorType == 1)
                    {
                        LabelMessage.Text = "计划跟踪号为空，单据不能提交！";

                        string script = @"alert('计划跟踪号为空，单据不能提交！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                    else if (ErrorType == 2)
                    {

                        LabelMessage.Text = "调整数量为0，单据不能提交！";

                        string script = @"alert('调整数量为0，单据不能审核！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                    else if (ErrorType == 3)
                    {

                        LabelMessage.Text = "仓位为空，单据不能提交！";

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

                    //邮件提醒技术及时审核
                    #region
                    string returnvalue = "";
                    string body = "";


                    string strTechMan = DropDownListPlaner.SelectedValue;
                    string to = "";
                    string sqlemail = "select EMail from TBDS_STAFFINFO where ST_PD='0' and  ST_CODE='" + strTechMan + "' ";

                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlemail);

                    if (dt.Rows.Count > 0)
                    {
                        to = dt.Rows[0][0].ToString();
                    }

                    List<string> bjEmailCC = new List<string>(); //抄送给...
                    bjEmailCC.Add("erp@ztsm.net");
                    bjEmailCC.Add(to);
                    List<string> mfEmail = null; //密送给..


                    body = "您有项目结转备库单需及时审核" + "\n" + "项目结转单号：" + LabelCode.Text + "\n" + "生产制号：" + Taskid + "\n" + "项  目  为:" + Projname + "\n" + "工  程  为：" + Engname;
                    returnvalue = DBCallCommon.SendEmail(to, bjEmailCC, mfEmail, Projname + "-" + Engname + "-" + Taskid + "数字平台项目结转备库", body);

                    if (returnvalue == "邮件已发送!")
                    {
                        //string jascript = @"alert('邮件发送成功!');";

                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", jascript, true);

                        Response.Write("<script>alert('邮件发送成功');</script>");
                    }
                    else if (returnvalue == "邮件发送失败!")
                    {
                        Response.Write("<script>alert('邮件发送不成功');</script>");

                    }
                    #endregion

                    Response.Redirect("SM_Warehouse_ProjTemp.aspx?FLAG=OPEN&&ID=" + Code);
                }

               
            }

        }
        protected void AntiSubmit_Click(object sender, EventArgs e)
        {
            string sql = "UPDATE TBWS_PROJTEMP SET PT_STATE='0',PT_SUBMITDATE='' WHERE PT_CODE='" + LabelCode.Text + "'";
            DBCallCommon.ExeSqlText(sql);
            Response.Redirect("SM_Warehouse_ProjTemp.aspx?FLAG=OPEN&&ID=" + LabelCode.Text);


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
                if (LabelState.Text == "1")
                {
                    LabelMessage.Text = "该项目结转单已提交不允许删除！";
                    return;
                }
                if (LabelState.Text == "2")
                {
                    LabelMessage.Text = "该项目结转单已审核不允许删除！";
                    return;
                }
                if (LabelState.Text == "3")
                {
                    LabelMessage.Text = "该项目结转单已审批不允许删除！";
                    return;
                }
                if (LabelState.Text == "0")
                {

                    List<string> sqllist = new List<string>();

                    string sql = "DELETE FROM TBWS_PROJTEMP WHERE PT_CODE='" + LabelCode.Text + "'";
                    sqllist.Add(sql);
                    sql = "DELETE FROM TBWS_PROJTEMPDETAIL WHERE PT_CODE='" + LabelCode.Text + "'";
                    sqllist.Add(sql);

                    

                    DBCallCommon.ExecuteTrans(sqllist);
                    sqllist.Clear();

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
            tb.Columns.Add("Cgmode", System.Type.GetType("System.String"));
            tb.Columns.Add("ShengYuNote", System.Type.GetType("System.String"));



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
                                newRow["Cgmode"] = ((Label)gRow.FindControl("Labelcgmode")).Text;
                                newRow["ShengYuNote"] = ((TextBox)gRow.FindControl("TextBoxShengYuNote")).Text;

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
                            row1["Cgmode"] = ((Label)gRow.FindControl("Labelcgmode")).Text;
                            row1["ShengYuNote"] = ((TextBox)gRow.FindControl("TextBoxShengYuNote")).Text;

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
                                newRow["Cgmode"] = ((Label)gRow.FindControl("Labelcgmode")).Text;
                                newRow["ShengYuNote"] = ((TextBox)gRow.FindControl("TextBoxShengYuNote")).Text;

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
                        newRow["Cgmode"] = ((Label)gRow.FindControl("Labelcgmode")).Text;
                        newRow["ShengYuNote"] = ((TextBox)gRow.FindControl("TextBoxShengYuNote")).Text;

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
                                newRow["Cgmode"] = ((Label)gRow.FindControl("Labelcgmode")).Text;
                                newRow["ShengYuNote"] = ((TextBox)gRow.FindControl("TextBoxShengYuNote")).Text;

                                tb.Rows.Add(newRow);
                            }

                            tb.Rows[i]["WN"] = Convert.ToInt32(wn / count).ToString();//改变原来行的数量

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
                            row1["Cgmode"] = ((Label)gRow.FindControl("Labelcgmode")).Text;
                            row1["ShengYuNote"] = ((TextBox)gRow.FindControl("TextBoxShengYuNote")).Text;

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
                                newRow["Cgmode"] = ((Label)gRow.FindControl("Labelcgmode")).Text;
                                newRow["ShengYuNote"] = ((TextBox)gRow.FindControl("TextBoxShengYuNote")).Text;

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

        protected void ButtonSplitCancel_Click(object sender, EventArgs e) //取消
        {
            RadioButtonListSplitMode.Items[0].Selected = true;
            RadioButtonListSplitMode2.Items[0].Selected = true;
            TextBoxSplitLineNum.Text = "2";
        }

       

        protected void TextBoxSCZH_TextChanged(object sender, EventArgs e)
        {
            string strTaskid = TextBoxSCZH.Text.Trim().Split('`')[2]; //获取生产制号
            string strPjname = "";
            string strEngname = "";
            string strshy = "";
            string sql = "select TSA_PJNAME,TSA_ENGNAME,TSA_TCCLERK from View_TM_TaskAssign where TSA_ID='" + strTaskid + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                strPjname = dr["TSA_PJNAME"].ToString();
                strEngname = dr["TSA_ENGNAME"].ToString();
                strshy = dr["TSA_TCCLERK"].ToString();
            }
            dr.Close();
            TextBoxSCZH.Text = strTaskid;
            TextBoxProjName.Text = strPjname;
            TextBoxEngName.Text = strEngname;
            if (strshy.Substring(0, 2) == "03")
            {
                DropDownListDep.SelectedValue = "03";
            }
            else if (strshy.Substring(0, 2) == "09")
            {
                DropDownListDep.SelectedValue = "09";
            }

            GetStaff();
            //DropDownListPlaner.SelectedValue = strshy; //可能没有strshy对应的值，会报错
            DropDownListPlaner.SelectedIndex = DropDownListPlaner.Items.IndexOf(DropDownListPlaner.Items.FindByValue(strshy)); //没有值返回是第一个人

        }


    }
}
