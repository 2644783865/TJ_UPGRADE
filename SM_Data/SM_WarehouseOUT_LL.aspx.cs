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
using System.Text;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
//using System.Windows.Forms;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_WarehouseOUT_LL : System.Web.UI.Page
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

            string sql = "SELECT DISTINCT DEP_CODE,case when DEP_NAME='综合办公室A' then '综合办公室A(食堂)' when DEP_NAME='设备安全管理部A' then '设备安全管理部A(维修组)' else DEP_NAME end as DEP_NAME FROM TBDS_DEPINFO WHERE len(DEP_CODE)=2";
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

            //string sql = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO WHERE DEP_FATHERID='04' and DEP_CY='1' and DEP_SFJY='0'";
            //DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            //DropDownListBZ.DataValueField = "DEP_CODE";
            //DropDownListBZ.DataTextField = "DEP_NAME";
            //DropDownListBZ.DataSource = dt;
            //DropDownListBZ.DataBind();
            int index0 = 0;
            ListItem item = new ListItem("--请选择--", "0");
            ListItem item1 = new ListItem("机加组", "机加组");
            ListItem item2 = new ListItem("钳工装配组", "钳工装配组");
            ListItem item3 = new ListItem("结构一组", "结构一组");
            ListItem item4 = new ListItem("结构二组", "结构二组");
            ListItem item5 = new ListItem("结构三组", "结构三组");
            ListItem item6 = new ListItem("结构四组", "结构四组");
            ListItem item7 = new ListItem("结构五组", "结构五组");
            ListItem item8 = new ListItem("堆焊六组", "堆焊六组");
            ListItem item9 = new ListItem("下料组", "下料组");
            ListItem item10 = new ListItem("服务组", "服务组");
            ListItem item11 = new ListItem("喷漆组", "喷漆组");
            ListItem item12 = new ListItem("生产外协", "生产外协");

            ListItem item13 = new ListItem("技术部", "技术部");
            ListItem item14 = new ListItem("采购部", "采购部");
            ListItem item15 = new ListItem("生产管理部", "生产管理部");
            ListItem item16 = new ListItem("质量部", "质量部");
            ListItem item17 = new ListItem("财务部", "财务部");
            ListItem item18 = new ListItem("市场部", "市场部");
            ListItem item19 = new ListItem("维修组", "维修组");
            ListItem item20 = new ListItem("综合办公室", "综合办公室");
            ListItem item21 = new ListItem("工程师办公室", "工程师办公室");
            ListItem item22 = new ListItem("设备管理部", "设备管理部");
            ListItem item23 = new ListItem("其他", "其他");
            DropDownListBZ.Items.Insert(0, item);
            DropDownListBZ.Items.Insert(index0 + 1, item1);
            DropDownListBZ.Items.Insert(index0 + 2, item2);
            DropDownListBZ.Items.Insert(index0 + 3, item3);
            DropDownListBZ.Items.Insert(index0 + 4, item4);
            DropDownListBZ.Items.Insert(index0 + 5, item5);
            DropDownListBZ.Items.Insert(index0 + 6, item6);
            DropDownListBZ.Items.Insert(index0 + 7, item7);
            DropDownListBZ.Items.Insert(index0 + 8, item8);
            DropDownListBZ.Items.Insert(index0 + 9, item9);
            DropDownListBZ.Items.Insert(index0 + 10, item10);
            DropDownListBZ.Items.Insert(index0 + 11, item11);
            DropDownListBZ.Items.Insert(index0 + 12, item12);

            DropDownListBZ.Items.Insert(index0 + 13, item13);
            DropDownListBZ.Items.Insert(index0 + 14, item14);
            DropDownListBZ.Items.Insert(index0 + 15, item15);
            DropDownListBZ.Items.Insert(index0 + 16, item16);
            DropDownListBZ.Items.Insert(index0 + 17, item17);
            DropDownListBZ.Items.Insert(index0 + 18, item18);
            DropDownListBZ.Items.Insert(index0 + 19, item19);
            DropDownListBZ.Items.Insert(index0 + 20, item20);
            DropDownListBZ.Items.Insert(index0 + 21, item21);
            DropDownListBZ.Items.Insert(index0 + 22, item22);
            DropDownListBZ.Items.Insert(index0 + 23, item23);
        }


        protected void GetStaff()
        {
            //string sql = "SELECT DISTINCT ST_ID,ST_NAME FROM TBDS_STAFFINFO WHERE ST_DEPID='05' and ST_POSITION='0504' ";
            string sql = "SELECT DISTINCT ST_ID,ST_NAME FROM TBDS_STAFFINFO WHERE ST_DEPID='" + Session["UserDeptID"].ToString().Trim() + "'";
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
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                //string sql = "SELECT UniqueID='',a.SQCODE AS SQCODE,a.MaterialCode AS MaterialCode,a.MaterialName AS MaterialName," +
                //    "a.Attribute AS Attribute,a.GB AS GB,a.Standard AS MaterialStandard,a.Fixed AS Fixed,a.Length AS DueLength,a.Length AS Length," +
                //    "a.Width AS Width,a.LotNumber AS LotNumber,a.PlanMode AS PlanMode,a.PTC AS PTC,a.OrderCode AS OrderID," +
                //    "a.WarehouseCode AS WarehouseCode,a.Warehouse AS Warehouse,a.LocationCode AS PositionCode," +
                //    "a.Location AS Position,a.Unit AS Unit,cast(a.Number as float) AS DN,cast(a.Number as float) AS DNUM,cast(a.Number as float) AS DIFNUM,a.SupportNumber AS DQN,a.SupportNumber AS RQN," +
                //    "a.Note AS Comment,a.CGMODE AS BSH,isnull(b.zxnum,'0') as DNUM,OP_QRUniqCode FROM  View_SM_Storage as a left join  View_TBPC_PURORDERDETAIL_PLAN as b  on a.PTC=b.ptcode WHERE State='OUT" + Session["UserID"].ToString() + "' order by MaterialCode DESC";
                string sql = "SELECT UniqueID='',a.SQCODE AS SQCODE,a.MaterialCode AS MaterialCode,a.MaterialName AS MaterialName," +
                    "a.Attribute AS Attribute,a.GB AS GB,a.Standard AS MaterialStandard,a.Fixed AS Fixed,a.Length AS DueLength,a.Length AS Length," +
                    "a.Width AS Width,a.LotNumber AS LotNumber,a.PlanMode AS PlanMode,a.PTC AS PTC,a.OrderCode AS OrderID," +
                    "a.WarehouseCode AS WarehouseCode,a.Warehouse AS Warehouse,a.LocationCode AS PositionCode," +
                    "a.Location AS Position,a.Unit AS Unit,cast(a.Number as float) AS DN,cast(a.Number as float) AS DNUM,cast(a.Number as float) AS DIFNUM,a.SupportNumber AS DQN,a.SupportNumber AS RQN," +
                    "a.Note AS Comment,a.CGMODE AS BSH,isnull(b.zxnum,'0') as DNUM,q.QROut_ID as OP_QRUniqCode FROM  View_SM_Storage as a left join  View_TBPC_PURORDERDETAIL_PLAN as b  on a.PTC=b.ptcode left join (select * from midTable_QROut where QROut_WHSTATE='1')q on a.SQCODE=q.QROut_SQCODE WHERE State='OUT" + Session["UserID"].ToString() + "' order by MaterialCode DESC";
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                string sqlQRIn = "update midTable_QROut set QROut_WHSTATE='0' where QROut_WHSTATE='1'";
                string sqlGetRN = "";
                string QROut_TaskID = "";
                DBCallCommon.ExeSqlText(sqlQRIn);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["OP_QRUniqCode"].ToString().Trim() != "")
                    {
                        sqlGetRN = "select * from midTable_QROut where QROut_ID=" + Convert.ToInt32(dt.Rows[i]["OP_QRUniqCode"].ToString().Trim()) + "";
                        DataTable dtGetRN = DBCallCommon.GetDTUsingSqlText(sqlGetRN);
                        if (dtGetRN.Rows.Count > 0)
                        {
                            dt.Rows[i]["DNUM"] = dtGetRN.Rows[0]["QROut_Num"].ToString().Trim();
                            if (QROut_TaskID == "")
                            {
                                QROut_TaskID = dtGetRN.Rows[0]["QROut_TaskID"].ToString().Trim();
                            }
                        }
                    }
                }
                TextBoxSCZH.Text = QROut_TaskID;
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

                GridView1.DataSource = dt;
                GridView1.DataBind();

                sql = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='OUT" + Session["UserID"].ToString() + "'";
                DBCallCommon.ExeSqlText(sql);


                LabelCode.Text = generateCode();

                TextBoxPageNum.Text = "1";

                //this.Page.Title = "重机领料单(" + LabelCode.Text + ")";

                sql = "INSERT INTO TBWS_OUTCODE (OP_CODE,OP_BILLTYPE) VALUES ('" + LabelCode.Text + "','1')";

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
                try { DropDownListSender.Items.FindByValue(Session["UserID"].ToString()).Selected = true; }
                catch { }


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
                Btn_daochu.Visible = false;
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
               "TotalNote AS Comment,OP_ZXMC,OP_NOTE1,OP_QRUniqCode FROM View_SM_OUT WHERE OutCode='" + code + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    LabelCode.Text = generateRedCode();

                    //this.Page.Title = "重机领料单(" + LabelCode.Text + ")";

                    TextBoxPageNum.Text = "1";
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
                    "LotNumber AS LotNumber,Unit AS Unit,-cast(RealNumber as float) AS DNUM,-cast(RealNumber as float) AS DN,cast((RealNumber-RealNumber) as float) AS DIFNUM," +
                    "-cast(RealSupportNumber as float) AS DQN,-cast(RealSupportNumber as float) AS RQN,cast(UnitPrice as float) AS UnitPrice," +
                    "cast(Amount as float) AS Amount,WarehouseCode AS WarehouseCode," +
                    "Warehouse AS Warehouse,LocationCode AS PositionCode,Location AS Position," +
                    "PlanMode AS PlanMode,PTC AS PTC,OrderCode AS OrderID," +
                    "DetailNote AS Comment,OP_BSH as BSH,OP_QRUniqCode FROM View_SM_OUT WHERE DetailState='RED" + Session["UserID"].ToString() + "' AND OutCode='" + code + "'";
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
                Btn_daochu.Visible = false;
                SumPrint.Visible = false;
                Related.Visible = false;

                AdjustLenWid.Visible = true;//调整长宽，推红可用

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("TextBoxPosition")).Enabled = true;
                    ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("TextBoxLotNumber")).Enabled = true;

                }

            }
            if (flag == "OPEN")
            {
                string sql = "SELECT Top 1 id as TrueCode,OutCode AS Code,DepCode AS LLDepCode,Dep AS LLDep,Date AS Date," +
                "TSAID as SCZH,SenderCode AS SenderCode," +
                "Sender AS Sender,DocCode AS DocumentCode," +
                "Doc AS Document,VerifierCode AS VerifierCode," +
                "Verifier AS Verifier,LEFT(ApprovedDate,10) AS ApproveDate,ROB AS Colour,TotalState AS State,BillType," +
                "TotalNote AS Comment,OP_ZXMC,OP_PAGENUM,OP_NOTE1,OP_XCZF,OP_QRUniqCode FROM View_SM_OUT WHERE OutCode='" + code + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    if (dr["OP_XCZF"].ToString().Trim() == "1")
                    {
                        chkxczf.Checked = true;
                    }
                    else
                    {
                        chkxczf.Checked = false;
                    }


                    LabelCode.Text = dr["Code"].ToString();

                    LabelTrueCode.Text = dr["TrueCode"].ToString();

                    this.Page.Title = "重机领料单(" + LabelTrueCode.Text + ")";

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

                    TextBoxPageNum.Text = dr["OP_PAGENUM"].ToString();

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
                    "LotNumber AS LotNumber,Unit AS Unit,cast(DueNumber as float) AS DN,cast(RealNumber as float) AS DNUM,cast((case when TotalState='2' then (DueNumber-RealNumber) else DueNumber end) as float) AS DIFNUM,cast(DueSupportNumber as float) AS DQN,cast(RealSupportNumber as float) AS RQN," +
                    "cast(UnitPrice as float) AS UnitPrice,cast(Amount as float) AS Amount,WarehouseCode AS WarehouseCode," +
                    "Warehouse AS Warehouse,LocationCode AS PositionCode,Location AS Position," +
                    "PlanMode AS PlanMode,PTC AS PTC,OrderCode AS OrderID,DetailNote AS Comment,OP_BSH as BSH,OP_QRUniqCode " +
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
                    Btn_daochu.Visible = false;
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
                                ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("TextBoxPosition")).Enabled = true;
                                ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("TextBoxLotNumber")).Enabled = true;
                            }
                        }
                    }
                }
                if (LabelState.Text == "2")
                {
                   
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("TextBoxRN")).Enabled = false;
                        ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("TextBoxRQN")).Enabled = false;
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
                        BtnBackStorage.Visible = true;
                        copyform.Visible = true;
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
                if (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "DNUM").ToString()))
                {
                    tdn += CommonFun.ComTryDouble(DataBinder.Eval(e.Row.DataItem, "DNUM").ToString());
                    trn += CommonFun.ComTryDouble(DataBinder.Eval(e.Row.DataItem, "DN").ToString());
                    tdqn += CommonFun.ComTryInt(DataBinder.Eval(e.Row.DataItem, "DQN").ToString());
                    trqn += CommonFun.ComTryInt(DataBinder.Eval(e.Row.DataItem, "RQN").ToString());
                }
                if (((System.Web.UI.WebControls.Label)e.Row.FindControl("LabelOP_QRUniqCode")).Text.Trim() != "")
                {
                    ((System.Web.UI.WebControls.TextBox)e.Row.FindControl("TextBoxRN")).Enabled = false;
                    //TextBoxSCZH.Enabled = false;
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((System.Web.UI.WebControls.Label)e.Row.Cells[13].FindControl("LabelTotalDN")).Text = Math.Round(trn, 4).ToString();
                ((System.Web.UI.WebControls.Label)e.Row.Cells[14].FindControl("LabelTotalRN")).Text = Math.Round(tdn, 4).ToString();
                ((System.Web.UI.WebControls.Label)e.Row.Cells[15].FindControl("LabelTotalDQN")).Text = tdqn.ToString();
                ((System.Web.UI.WebControls.Label)e.Row.Cells[16].FindControl("LabelTotalRQN")).Text = trqn.ToString();
            }
        }

        protected string generateCode()
        {
            string sql = "SELECT MAX(OP_CODE) AS MaxCode FROM TBWS_OUTCODE WHERE LEN(OP_CODE)=10 AND OP_BILLTYPE='1'";
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
                return "LL00000001";
            }
            else
            {
                int tempnum = Convert.ToInt32((code.Substring(2, 8)));
                tempnum++;
                code = "LL" + tempnum.ToString().PadLeft(8, '0');
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
            dr.Close();
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
            tb.Columns.Add("DNUM", System.Type.GetType("System.String"));
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
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            tb.Columns.Add("OP_QRUniqCode", System.Type.GetType("System.String"));
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow GRow = GridView1.Rows[i];
                DataRow row = tb.NewRow();
                row["UniqueID"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabeLUniqueID")).Text;
                row["SQCODE"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabeLSQCODE")).Text;
                row["MaterialCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialCode")).Text;
                row["MaterialName"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialName")).Text;
                row["Attribute"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelAttribute")).Text;
                row["GB"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelGB")).Text;
                row["LotNumber"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxLotNumber")).Text;
                row["MaterialStandard"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialStandard")).Text;
                row["Fixed"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelFixed")).Text;
                row["DueLength"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLength")).Text;
                row["Length"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxLength")).Text;
                row["Width"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxWidth")).Text;
                row["Unit"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelUnit")).Text;
                row["DN"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDN")).Text;
                row["DNUM"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxRN")).Text;
                row["DIFNUM"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDIFNUM")).Text;
                row["DQN"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDQN")).Text;
                row["RQN"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxRQN")).Text;
                row["PlanMode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPlanMode")).Text;
                row["PTC"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPTC")).Text;
                row["OrderID"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelOrderID")).Text;
                row["Comment"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxComment")).Text;
                row["Warehouse"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelWarehouse")).Text;
                row["WarehouseCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelWarehouseCode")).Text;
                row["Position"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxPosition")).Text;
                row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                row["BSH"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelBSH")).Text;
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                row["OP_QRUniqCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelOP_QRUniqCode")).Text;
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
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
                    " CAST(CAST(Number AS float) AS VARCHAR(50)) AS DN,CAST(CAST(Number AS float) AS VARCHAR(50)) AS DNUM, CAST(CAST(Number AS float) AS VARCHAR(50)) AS RN,cast((Number-Number) as VARCHAR(50)) AS DIFNUM," +
                    " CAST(CAST(SupportNumber AS float) AS VARCHAR(50)) AS DQN,CAST(CAST(SupportNumber AS float) AS VARCHAR(50)) AS RQN," +
                    "Note AS Comment,CGMODE AS BSH " +
                    "FROM View_SM_Storage WHERE State='APPENDOUT" + Session["UserID"].ToString() + "'";
            dt.Merge(DBCallCommon.GetDTUsingSqlText(sql));
            GridView1.DataSource = dt;
            GridView1.DataBind();
            sql = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='APPENDOUT" + Session["UserID"].ToString() + "'";
            DBCallCommon.ExeSqlText(sql);
            LabelMessage.Text = "追加成功";

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (InputColour.Value == "1")
                {
                    ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("TextBoxPosition")).Enabled = true;
                    ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("TextBoxPosition")).Enabled = true;
                }

                ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("TextBoxRN")).Enabled = true;
                ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("TextBoxRQN")).Enabled = true;
            }
            CheckSQCODE();//重复计划跟踪号体现

            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", "bindunbeforunload();", true);

        }
        protected void CheckSQCODE() //有重复的计划跟踪号有颜色区分
        {

            for (int i = 0; i < (GridView1.Rows.Count - 1); i++)
            {
                GridViewRow gvrow0 = GridView1.Rows[i];
                string sqcode = ((System.Web.UI.WebControls.Label)gvrow0.FindControl("LabelPTC")).Text; //计划跟踪号
                string materialcode = ((System.Web.UI.WebControls.Label)gvrow0.FindControl("LabelMaterialCode")).Text; //物料代码
                if ((materialcode.Substring(0, 5) != "01.07") && (materialcode.Substring(0, 5) != "01.14"))
                {
                    for (int j = i + 1; j < GridView1.Rows.Count; j++)
                    {
                        GridViewRow gvrow1 = GridView1.Rows[j];
                        string nextsqcode = ((System.Web.UI.WebControls.Label)gvrow1.FindControl("LabelPTC")).Text;
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
            tb.Columns.Add("DNUM", System.Type.GetType("System.String"));
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
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            tb.Columns.Add("OP_QRUniqCode", System.Type.GetType("System.String"));
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            //插入记录模式
            if (mode2 == "0")
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow GRow = GridView1.Rows[i];
                    if (((System.Web.UI.WebControls.CheckBox)GRow.FindControl("CheckBox1")).Checked == true)
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
                                dn = Convert.ToSingle(((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDN")).Text);//即时库存数量
                                rn = Convert.ToSingle(((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxRN")).Text);//实际数量
                                dqn = Convert.ToInt32(((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDQN")).Text);//即时库存张(支)
                                rqn = Convert.ToInt32(((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxRQN")).Text);//实发张（支）数
                            }
                            catch
                            {
                                LabelMessage.Text = "请正确填写实发数量！";
                                return;
                            }
                            for (int j = 0; j < count - 1; j++)
                            {
                                DataRow row = tb.NewRow();
                                row["UniqueID"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelUniqueID")).Text;
                                row["SQCODE"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelSQCODE")).Text;
                                row["MaterialCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialCode")).Text;
                                row["MaterialName"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialName")).Text;
                                row["Attribute"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelAttribute")).Text;
                                row["GB"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelGB")).Text;
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
                                    row["LotNumber"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxLotNumber")).Text;

                                }
                                //row["LotNumber"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLotNumber")).Text;
                                //if (((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLotNumber")).Text != "")
                                //{
                                //    string[] lsh = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLotNumber")).Text.Split('-');
                                //    string Lot = (Convert.ToInt32(lsh[0]) + count - 1).ToString().PadLeft(3, '0') + "-" + lsh[1];
                                //    row["LotNumber"] = Lot;
                                //}
                                row["MaterialStandard"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelFixed")).Text;
                                row["DueLength"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLength")).Text;
                                row["Length"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxLength")).Text;
                                row["Width"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxWidth")).Text;
                                row["Unit"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelUnit")).Text;
                                row["DN"] = Math.Round((dn / count), 4).ToString();//仓库数量
                                row["DNUM"] = Math.Round((rn / count), 4).ToString();//实收数量
                                row["DIFNUM"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDIFNUM")).Text;
                                row["DQN"] = Convert.ToInt32((dqn / count)).ToString();
                                row["RQN"] = Convert.ToInt32((rqn / count)).ToString();
                                row["PlanMode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPlanMode")).Text;
                                row["PTC"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPTC")).Text;
                                row["OrderID"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelOrderID")).Text;
                                row["Comment"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxComment")).Text;
                                row["Warehouse"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelWarehouse")).Text;
                                row["WarehouseCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelWarehouseCode")).Text;
                                //row["Position"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPosition")).Text;
                                //row["PositionCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPositionCode")).Text;

                                row["Position"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxPosition")).Text;
                                row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                                row["BSH"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelBSH")).Text;
                                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                                row["OP_QRUniqCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelOP_QRUniqCode")).Text;
                                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                                tb.Rows.Add(row);
                            }
                            ////////////////////////////////////////////////////////////////////////////////////////////
                            /*
                             * 产生最后一行
                             */
                            DataRow row1 = tb.NewRow();
                            row1["UniqueID"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelUniqueID")).Text;
                            row1["SQCODE"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelSQCODE")).Text;
                            row1["MaterialCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialCode")).Text;
                            row1["MaterialName"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialName")).Text;
                            row1["Attribute"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelAttribute")).Text;
                            row1["GB"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelGB")).Text;
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
                                row1["LotNumber"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxLotNumber")).Text;

                            }
                            //row1["LotNumber"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLotNumber")).Text;
                            //if (((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLotNumber")).Text != "")
                            //{
                            //    string[] lsh = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLotNumber")).Text.Split('-');
                            //    string Lot = (Convert.ToInt32(lsh[0]) + count - 1).ToString().PadLeft(3, '0') + "-" + lsh[1];
                            //    row1["LotNumber"] = Lot;
                            //}

                            row1["MaterialStandard"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialStandard")).Text;
                            row1["Fixed"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelFixed")).Text;
                            row1["DueLength"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLength")).Text;
                            row1["Length"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxLength")).Text;
                            row1["Width"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxWidth")).Text;
                            row1["Unit"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelUnit")).Text;
                            //Math.Round((num - Math.Round((num / count), 4) * (count - 1)), 4).ToString();

                            row1["DN"] = Math.Round((dn - Math.Round((dn / count), 4) * (count - 1)), 4).ToString();//仓库数量
                            row1["DNUM"] = Math.Round((rn - Math.Round((rn / count), 4) * (count - 1)), 4).ToString();//实收数量
                            row1["DIFNUM"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDIFNUM")).Text;
                            row1["DQN"] = Convert.ToInt32((dqn / count)).ToString();
                            row1["RQN"] = Convert.ToInt32((rqn / count)).ToString();
                            row1["PlanMode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPlanMode")).Text;
                            row1["PTC"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPTC")).Text;
                            row1["OrderID"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelOrderID")).Text;
                            row1["Comment"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxComment")).Text;
                            row1["Warehouse"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelWarehouse")).Text;
                            row1["WarehouseCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelWarehouseCode")).Text;

                            //row1["Position"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPosition")).Text;
                            //row1["PositionCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPositionCode")).Text;

                            row1["Position"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxPosition")).Text;
                            row1["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                            row1["BSH"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelBSH")).Text;
                            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                            row1["OP_QRUniqCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelOP_QRUniqCode")).Text;
                            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                            tb.Rows.Add(row1);

                            ////////////////////////////////////////////////////////////////////////////////////////////

                        }
                        //数据复制模式
                        if (mode == "1")
                        {
                            for (int j = 0; j < count; j++)
                            {
                                DataRow row = tb.NewRow();
                                row["UniqueID"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelUniqueID")).Text;
                                row["SQCODE"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelSQCODE")).Text;
                                row["MaterialCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialCode")).Text;
                                row["MaterialName"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialName")).Text;
                                row["Attribute"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelAttribute")).Text;
                                row["GB"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelGB")).Text;
                               // row["LotNumber"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLotNumber")).Text;
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
                                    row["LotNumber"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxLotNumber")).Text;

                                }
                                //if (((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLotNumber")).Text != "")
                                //{
                                //    string[] lsh = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLotNumber")).Text.Split('-');
                                //    string Lot = (Convert.ToInt32(lsh[0]) + j).ToString().PadLeft(3, '0') + "-" + lsh[1];
                                //    row["LotNumber"] = Lot;
                                //}

                                row["MaterialStandard"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelFixed")).Text;
                                row["DueLength"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLength")).Text;
                                row["Length"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxLength")).Text;
                                row["Width"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxWidth")).Text;
                                row["Unit"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelUnit")).Text;
                                row["DN"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDN")).Text;
                                row["DNUM"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxRN")).Text;
                                row["DIFNUM"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDIFNUM")).Text;
                                row["DQN"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDQN")).Text;
                                row["RQN"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxRQN")).Text;
                                row["PlanMode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPlanMode")).Text;
                                row["PTC"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPTC")).Text;
                                row["OrderID"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelOrderID")).Text;
                                row["Comment"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxComment")).Text;
                                row["Warehouse"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelWarehouse")).Text;
                                row["WarehouseCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelWarehouseCode")).Text;
                                row["Position"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxPosition")).Text;
                                row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                                row["BSH"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelBSH")).Text;
                                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                                row["OP_QRUniqCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelOP_QRUniqCode")).Text;
                                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                                tb.Rows.Add(row);
                            }
                        }
                    }
                    else
                    {
                        DataRow row = tb.NewRow();
                        row["UniqueID"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelUniqueID")).Text;
                        row["SQCODE"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelSQCODE")).Text;
                        row["MaterialCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialCode")).Text;
                        row["MaterialName"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialName")).Text;
                        row["Attribute"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelAttribute")).Text;
                        row["GB"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelGB")).Text;
                        row["LotNumber"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxLotNumber")).Text;
                        row["MaterialStandard"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialStandard")).Text;
                        row["Fixed"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelFixed")).Text;
                        row["DueLength"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLength")).Text;
                        row["Length"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxLength")).Text;
                        row["Width"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxWidth")).Text;
                        row["Unit"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelUnit")).Text;
                        row["DN"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDN")).Text;
                        row["DNUM"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxRN")).Text;
                        row["DIFNUM"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDIFNUM")).Text;
                        row["DQN"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDQN")).Text;
                        row["RQN"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxRQN")).Text;
                        row["PlanMode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPlanMode")).Text;
                        row["PTC"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPTC")).Text;
                        row["OrderID"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelOrderID")).Text;
                        row["Comment"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxComment")).Text;
                        row["Warehouse"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelWarehouse")).Text;
                        row["WarehouseCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelWarehouseCode")).Text;
                        row["Position"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxPosition")).Text;
                        row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                        row["BSH"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelBSH")).Text;
                        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                        row["OP_QRUniqCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelOP_QRUniqCode")).Text;
                        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
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
                    if (((System.Web.UI.WebControls.CheckBox)GRow.FindControl("CheckBox1")).Checked == true)
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
                                dn = Convert.ToSingle(((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDN")).Text);
                                rn = Convert.ToSingle(((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxRN")).Text);
                                dqn = Convert.ToInt32(((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDQN")).Text);
                                rqn = Convert.ToInt32(((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxRQN")).Text);
                            }
                            catch
                            {
                                LabelMessage.Text = "请正确填写实发数量！";
                                return;
                            }
                            for (int j = 0; j < count - 2; j++)
                            {
                                DataRow row = tb.NewRow();
                                row["UniqueID"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelUniqueID")).Text;
                                row["SQCODE"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelSQCODE")).Text;
                                row["MaterialCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialCode")).Text;
                                row["MaterialName"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialName")).Text;
                                row["Attribute"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelAttribute")).Text;
                                row["GB"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelGB")).Text;
                                row["LotNumber"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxLotNumber")).Text;
                                //if (((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLotNumber")).Text != "")
                                //{
                                //    string[] lsh = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLotNumber")).Text.Split('-');
                                //    string Lot = (Convert.ToInt32(lsh[0]) + j + 1).ToString().PadLeft(3, '0') + "-" + lsh[1];
                                //    row["LotNumber"] = Lot;
                                //}

                                row["MaterialStandard"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelFixed")).Text;
                                row["DueLength"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLength")).Text;
                                row["Length"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxLength")).Text;
                                row["Width"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxWidth")).Text;
                                row["Unit"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelUnit")).Text;
                                row["DN"] = (dn / count).ToString();
                                tb.Rows[i]["DN"] = (dn / count).ToString();
                                row["DNUM"] = (rn / count).ToString();
                                tb.Rows[i]["DNUM"] = (rn / count).ToString();
                                row["DIFNUM"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDIFNUM")).Text;
                                row["DQN"] = Convert.ToInt32((dqn / count)).ToString();
                                tb.Rows[i]["DQN"] = Convert.ToInt32((dqn / count)).ToString();
                                row["RQN"] = Convert.ToInt32((rqn / count)).ToString();
                                tb.Rows[i]["RQN"] = Convert.ToInt32((rqn / count)).ToString();
                                row["PlanMode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPlanMode")).Text;
                                row["PTC"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPTC")).Text;
                                row["OrderID"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelOrderID")).Text;
                                row["Comment"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxComment")).Text;
                                row["Warehouse"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelWarehouse")).Text;
                                row["WarehouseCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelWarehouseCode")).Text;
                                row["Position"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxPosition")).Text;
                                row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                                row["BSH"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelBSH")).Text;
                                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                                row["OP_QRUniqCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelOP_QRUniqCode")).Text;
                                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                                tb.Rows.Add(row);
                            }

                            //产生最后一行
                            DataRow row1 = tb.NewRow();
                            row1["UniqueID"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelUniqueID")).Text;
                            row1["SQCODE"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelSQCODE")).Text;
                            row1["MaterialCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialCode")).Text;
                            row1["MaterialName"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialName")).Text;
                            row1["Attribute"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelAttribute")).Text;
                            row1["GB"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelGB")).Text;
                            row1["LotNumber"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxLotNumber")).Text;
                            //if (((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLotNumber")).Text != "")
                            //{
                            //    string[] lsh = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLotNumber")).Text.Split('-');
                            //    string Lot = (Convert.ToInt32(lsh[0]) + count - 1).ToString().PadLeft(3, '0') + "-" + lsh[1];
                            //    row1["LotNumber"] = Lot;
                            //}

                            row1["MaterialStandard"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialStandard")).Text;
                            row1["Fixed"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelFixed")).Text;
                            row1["DueLength"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLength")).Text;
                            row1["Length"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxLength")).Text;
                            row1["Width"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxWidth")).Text;
                            row1["Unit"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelUnit")).Text;

                            //Math.Round((num - Math.Round((num / count), 4) * (count - 1)), 4).ToString();

                            row1["DN"] = Math.Round((dn - Math.Round((dn / count), 4) * (count - 1)), 4).ToString();//仓库数量
                            row1["DNUM"] = Math.Round((rn - Math.Round((rn / count), 4) * (count - 1)), 4).ToString();//实收数量
                            row1["DIFNUM"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDIFNUM")).Text;
                            row1["DQN"] = Convert.ToInt32((dqn / count)).ToString();
                            row1["RQN"] = Convert.ToInt32((rqn / count)).ToString();
                            row1["PlanMode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPlanMode")).Text;
                            row1["PTC"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPTC")).Text;
                            row1["OrderID"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelOrderID")).Text;
                            row1["Comment"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxComment")).Text;
                            row1["Warehouse"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelWarehouse")).Text;
                            row1["WarehouseCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelWarehouseCode")).Text;

                            //row1["Position"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPosition")).Text;
                            //row1["PositionCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPositionCode")).Text;

                            row1["Position"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxPosition")).Text;
                            row1["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                            row1["BSH"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelBSH")).Text;
                            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                            row1["OP_QRUniqCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelOP_QRUniqCode")).Text;
                            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                            tb.Rows.Add(row1);

                        }
                        //数据复制模式
                        if (mode == "1")
                        {
                            for (int j = 0; j < count - 1; j++)
                            {
                                DataRow row = tb.NewRow();
                                row["UniqueID"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelUniqueID")).Text;
                                row["SQCODE"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelSQCODE")).Text;
                                row["MaterialCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialCode")).Text;
                                row["MaterialName"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialName")).Text;
                                row["Attribute"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelAttribute")).Text;
                                row["GB"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelGB")).Text;
                                row["LotNumber"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxLotNumber")).Text;
                                //if (((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLotNumber")).Text != "")
                                //{
                                //    string[] lsh = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLotNumber")).Text.Split('-');
                                //    string Lot = (Convert.ToInt32(lsh[0]) + j + 1).ToString().PadLeft(3, '0') + "-" + lsh[1];
                                //    row["LotNumber"] = Lot;
                                //}

                                row["MaterialStandard"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelFixed")).Text;
                                row["DueLength"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelLength")).Text;
                                row["Length"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxLength")).Text;
                                row["Width"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxWidth")).Text;
                                row["Unit"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelUnit")).Text;
                                row["DN"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDN")).Text;
                                row["DNUM"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxRN")).Text;
                                row["DIFNUM"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDIFNUM")).Text;
                                row["DQN"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelDQN")).Text;
                                row["RQN"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxRQN")).Text;
                                row["PlanMode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPlanMode")).Text;
                                row["PTC"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelPTC")).Text;
                                row["OrderID"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelOrderID")).Text;
                                row["Comment"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxComment")).Text;
                                row["Warehouse"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelWarehouse")).Text;
                                row["WarehouseCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelWarehouseCode")).Text;
                                row["Position"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxPosition")).Text;
                                row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                                row["BSH"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelBSH")).Text;
                                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                                row["OP_QRUniqCode"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("LabelOP_QRUniqCode")).Text;
                                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                                tb.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            GridView1.DataSource = tb;
            GridView1.DataBind();

            if (LabelCode.Text.Contains('R')) //为红单审核是相当于入库，可以修改仓库
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("TextBoxPosition")).Enabled = true;
                    ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("TextBoxLotNumber")).Enabled = true;
                }
            }
            #region  没用代码
            //string NowCode = string.Empty;
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    //if ((GridView1.Rows[i].FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked)
                
            //    System.Web.UI.WebControls.Label lbCode = (GridView1.Rows[i].FindControl("LabelPTC") as System.Web.UI.WebControls.Label);
            //    string lotnum0 = (GridView1.Rows[i].FindControl("LabelLotNumber") as System.Web.UI.WebControls.Label).Text.Trim();
            //    if (lotnum0 != "")
            //    {
            //        string Code = lbCode.Text;

            //        if (Code != NowCode)
            //        {
            //            NowCode = lbCode.Text;

            //            string lotnum = (GridView1.Rows[i].FindControl("LabelLotNumber") as System.Web.UI.WebControls.Label).Text.Trim();

            //            for (int j = i + 1; j < GridView1.Rows.Count; j++)
            //            {
            //                string NextCode = (GridView1.Rows[j].FindControl("LabelPTC") as System.Web.UI.WebControls.Label).Text;

            //                if (Code == NextCode)
            //                {

            //                    string id = (Convert.ToInt32((GridView1.Rows[j - 1].FindControl("LabelLotNumber") as System.Web.UI.WebControls.Label).Text.Trim().Split('-')[0]) + 1).ToString().PadLeft(3, '0');

            //                    (GridView1.Rows[j].FindControl("LabelLotNumber") as System.Web.UI.WebControls.Label).Text = id + "-" + (GridView1.Rows[j - 1].FindControl("LabelLotNumber") as System.Web.UI.WebControls.Label).Text.Trim().Split('-')[1];

            //                }
            //                else
            //                {

            //                    break;

            //                }
            //            }

            //        }
            //    }
            //}
            #endregion

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
                System.Web.UI.WebControls.CheckBox chk = ((System.Web.UI.WebControls.CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1"));
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
                    ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("TextBoxPosition")).Enabled = true;
                    ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("TextBoxLotNumber")).Enabled = true;
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

        //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "DNUM").ToString()))
        //        {
        //            tdn += CommonFun.ComTryDouble(DataBinder.Eval(e.Row.DataItem, "DNUM").ToString());
        //            trn += CommonFun.ComTryDouble(DataBinder.Eval(e.Row.DataItem, "DN").ToString());
        //            tdqn += CommonFun.ComTryInt(DataBinder.Eval(e.Row.DataItem, "DQN").ToString());
        //            trqn += CommonFun.ComTryInt(DataBinder.Eval(e.Row.DataItem, "RQN").ToString());
        //        }
        //    }
        //}

        //保存操作
        protected void Save_Click(object sender, EventArgs e)
        {
            //2016.6.8修改，用于查询该任务号下时候有下面的物料，和需用计划表关联
            string errorWlbmORsfwl="";
            if (!string.IsNullOrEmpty(TextBoxSCZH.Text.ToString()))
            {
                string errorMaterialCode="";
                string errorsuliang = "";
                string TextBoxSCZHcx = TextBoxSCZH.Text.ToString().Trim();
                string sqltext = "select PUR_MARID from TBPC_PURCHASEPLAN where PUR_PCODE like '" + TextBoxSCZHcx + "%'";
                DataTable drcx = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (drcx.Rows.Count > 0)
                {
                    if (GridView1.Rows.Count > 0)
                    {

                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {

                            System.Web.UI.WebControls.Label LabelMaterialCode = (System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("LabelMaterialCode");
                            for (int j = 0; j < drcx.Rows.Count; j++)
                            {
                                if ((LabelMaterialCode.Text.ToString()) == (drcx.Rows[j][0].ToString()))
                                {
                                    break;
                                }
                                if (j == drcx.Rows.Count - 1 && LabelMaterialCode.Text.Trim().ToString() != drcx.Rows[j][0].ToString())
                                {
                                    errorMaterialCode += LabelMaterialCode.Text.ToString();
                                    errorMaterialCode += ",";
                                }


                            }
                        }
                        if (!string.IsNullOrEmpty(errorMaterialCode))
                        {
                            string messagell = "物料编码为" + errorMaterialCode + "不存在于该任务号下，有可能导致出错，请检查";
                            errorWlbmORsfwl = messagell;

                        }
                        if (string.IsNullOrEmpty(errorMaterialCode))
                        {
                            for (int i = 0; i < GridView1.Rows.Count; i++)
                            {
                                double sumjh = 0.0000;
                                double sumsf = 0.0000;
                                System.Web.UI.WebControls.Label LabelMaterialCode = (System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("LabelMaterialCode");
                                string sqltextjhsl = "select sum(isnull(PUR_NUM,0)-isnull(BG_NUM,0)) as PUR_NUMR from TBPC_PURCHASEPLAN as a left join (select * from TBPC_BG where RESULT='已驳回') as b on a.PUR_PTCODE=b.BG_PTC where PUR_PCODE like '" + TextBoxSCZHcx + "%' and PUR_MARID='" + LabelMaterialCode.Text.Trim().ToString() + "'";
                                DataTable dtjhsl = DBCallCommon.GetDTUsingSqlText(sqltextjhsl);
                                sumjh = Convert.ToDouble(dtjhsl.Rows[0]["PUR_NUMR"].ToString().Trim());
                                string sqltextsfsl = "select sum(RealNumber) as RealNumber from View_SM_OUT where MaterialCode='" + LabelMaterialCode.Text.Trim().ToString() + "' and tsaid='" + TextBoxSCZHcx + "'";
                                DataTable dtsfsl = DBCallCommon.GetDTUsingSqlText(sqltextsfsl);
                                if (dtsfsl.Rows.Count > 0 &&(!string.IsNullOrEmpty( dtsfsl.Rows[0]["RealNumber"].ToString().Trim())))
                                {
                                    sumsf = Convert.ToDouble(dtsfsl.Rows[0]["RealNumber"].ToString().Trim());
                                }
                                if (sumjh < sumsf)
                                {
                                    errorsuliang += "任务号为：" + TextBoxSCZHcx + "," + "物料代码为：" + LabelMaterialCode.Text.Trim().ToString()+ "," + "计划总数量为：" + sumjh +","+ "实发总数量为："+sumsf+"；";
                               }

                            }
                            if (!string.IsNullOrEmpty(errorsuliang))
                            {
                                string messagesf = errorsuliang + "所有实发数量包括保存成功，未审批的领料单中实发数量，可能导致重复出库，请检查";
                                errorWlbmORsfwl = messagesf;

                            }

                            
                        }
                    }


                }

            }

            //必须选择领料班组
            if (DropDownListBZ.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择领料班组！');", true);
                return;
            }
            //填写超定额理由
            int t = 0;
            foreach (GridViewRow gr in GridView1.Rows)
            {
                string checkptc = (gr.FindControl("LabelPTC") as System.Web.UI.WebControls.Label).Text.Trim();
                string checksql = "select *,(isnull(PUR_NUM,0)-isnull(BG_NUM,0)) as PUR_NUMR from TBPC_PURCHASEPLAN as a left join (select * from TBPC_BG where RESULT='已驳回') as b on a.PUR_PTCODE=b.BG_PTC where PUR_PTCODE='" + checkptc + "' and PUR_PTCODE not like '%备库%' and PUR_PTCODE not like '%beiku%' and PUR_PTCODE not like '%BEIKU%'";
                DataTable checkdt = DBCallCommon.GetDTUsingSqlText(checksql);
                if (checkdt.Rows.Count > 0)
                {
                    double plannum = CommonFun.ComTryDouble(checkdt.Rows[0]["PUR_NUMR"].ToString().Trim());
                    double realnum = CommonFun.ComTryDouble((gr.FindControl("TextBoxRN") as System.Web.UI.WebControls.TextBox).Text.Trim());
                    if (realnum > 1.05 * plannum)
                    {
                        t++;
                    }
                }
            }
            if (t > 0 && TextBoxNOTE1.Text.Trim()=="")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('有超过定额5%的物料，请在备注里填写超定额原因！');", true);
                return;
            }



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

            string BillType = string.Empty;

            if (LabelBillType.Text.Trim() == string.Empty || LabelBillType.Text.Trim() == "1")
            {
                BillType = "1";
            }
            else if (LabelBillType.Text.Trim() == "4")
            {
                BillType = "4";
            }

            int pagenum = 0;

            try
            {
                pagenum = Convert.ToInt32(TextBoxPageNum.Text.Trim());
            }

            catch { pagenum = 0; }

            sql = "select count(*) from TBWS_OUT where OP_CODE='" + Code + "'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() == "0")
            {
                sql = "exec ResetSeed @tablename=TBWS_OUT";

                sqllist.Add(sql);

                sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_DEP,OP_DATE,OP_TSAID," +
                    "OP_SENDER,OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
                    "OP_STATE,OP_NOTE,OP_BILLTYPE,OP_RealTime,OP_PAGENUM,OP_NOTE1) VALUES('" + Code + "','" + LLDepCode + "','" + Date + "','" + SCZH + "','" +
                    SendClerkCode + "','" + DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','" +
                    Colour + "','1','" + Comment + "','" + BillType + "',convert(varchar(50),getdate(),120),'" + pagenum + "','" + NOTE1 + "')";
               
                sqllist.Add(sql);
            }
            else
            {

                sql = "update TBWS_OUT set OP_DEP='" + LLDepCode + "',OP_DATE='" + Date + "',OP_TSAID='" + SCZH + "',";
                sql += "OP_SENDER='" + SendClerkCode + "',OP_DOC='" + DocCode + "',OP_VERIFIER='" + VerifierCode + "',OP_VERIFYDATE='" + ApproveDate + "',OP_HSFLAG='0',";
                sql += "OP_ROB='" + Colour + "',OP_STATE='1',OP_NOTE='" + Comment + "',OP_BILLTYPE='" + BillType + "',OP_RealTime=convert(varchar(50),getdate(),120),OP_PAGENUM='" + pagenum + "',OP_NOTE1='" + NOTE1 + "' where OP_CODE='" + Code + "'";

                sqllist.Add(sql);
            }


            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            string sqlUpdateQR3 = "update midTable_QROut set QROut_State='0' where QROut_ID in(select OP_QRUniqCode from TBWS_OUTDETAIL where OP_CODE='" + Code + "')";
            sqllist.Add(sqlUpdateQR3);
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

            sql = "DELETE FROM TBWS_OUTDETAIL WHERE OP_CODE='" + Code + "'";

            sqllist.Add(sql);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string UniqueID = Code + (i + 1).ToString().PadLeft(3, '0');

                string SQCODE = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;

                //旧的仓库唯一号

                string MaterialCode = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text.Trim();
                string LotNumber = ((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxLotNumber")).Text.Trim();
                string Fixed = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;

                int DueLength = 0;
                try { DueLength = Convert.ToInt32(Convert.ToSingle(((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelLength")).Text)); }
                catch { DueLength = 0; }


                int Length = 0;
                try { Length = Convert.ToInt32(Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxLength")).Text)); }
                catch { Length = 0; }
                int Width = 0;
                try { Width = Convert.ToInt32(Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxWidth")).Text)); }
                catch { Width = 0; }

                string WarehouseOutCode = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;

                string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;

                string PTC = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;
                
                //保存仓库号时需要重新获取


                //@mid+@mptc+@mlotnum+CAST(@mlength AS VARCHAR(50))+CAST(@mwidth AS VARCHAR(50))+@mstid+@mwlid（物料代码+计划跟踪号+批号+长+宽+仓库+仓位）

                //新的仓库唯一号
                //string SQCODE = MaterialCode + PTC + LotNumber + Convert.ToString(Length) + Convert.ToString(Width) + WarehouseOutCode + PositionCode;  

                float DN = 0;
                try
                {
                    float temp = Convert.ToSingle(((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelDN")).Text);
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
                    float temp = Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxRN")).Text);
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
                    Int32 temp = Convert.ToInt32(((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelDQN")).Text);
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
                    Int32 temp = Convert.ToInt32(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxRQN")).Text);
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
                string PlanMode = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text;
                //string PTC = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;
                string OrderID = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                string Note = ((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;
                string bsh = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelBSH")).Text;
                string WG_UPRICE="0";
                string WG_AMOUNT="0";
                string sqltext = "select WG_UPRICE,WG_AMOUNT from TBWS_INDETAIL where WG_PTCODE='" + PTC + "'";
                System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
                string sqltext02 = "select WG_UPRICE,WG_AMOUNT from TBWS_INDETAIL where WG_MARID='"+MaterialCode.ToString().Trim()+"'";
                System.Data.DataTable dt02 = DBCallCommon.GetDTUsingSqlText(sqltext02);
                if (dt1.Rows.Count > 0)
                {
                    WG_UPRICE = dt1.Rows[0]["WG_UPRICE"].ToString();
                    WG_AMOUNT = dt1.Rows[0]["WG_AMOUNT"].ToString();
                }
                else if (dt1.Rows.Count == 0&&dt02.Rows.Count > 0)
                {
                    WG_UPRICE = dt02.Rows[0]["WG_UPRICE"].ToString();
                    WG_AMOUNT = dt02.Rows[0]["WG_AMOUNT"].ToString();
                }

                //2018.8.16修改出库金额
                double WG_AMOUNT1 = RN * Convert.ToDouble(WG_UPRICE);
                decimal WG_AMOUNT2 = Math.Round((decimal)WG_AMOUNT1, 2, MidpointRounding.AwayFromZero);
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                string OP_QRUniqCode = ((Label)this.GridView1.Rows[i].FindControl("LabelOP_QRUniqCode")).Text;
                string sqlUpdateQR2 = "";
                //sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                //    "OP_FIXED,OP_DUELENGTH,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                //    "OP_UPRICE,OP_AMOUNT,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                //    "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE,OP_BSH) VALUES('" + Code + "','" + UniqueID + "','" + SQCODE + "','" +
                //    MaterialCode + "','" +
                //    Fixed + "','" + DueLength + "','" + Length + "','" +
                //    Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','" + WG_UPRICE + "','" + WG_AMOUNT + "','" +
                //    WarehouseOutCode + "','" + PositionCode + "','" + PlanMode + "','" +
                //    PTC + "','" + OrderID + "','" + Note + "','','" + bsh + "')";
                
                if (OP_QRUniqCode != "")
                {
                    sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                    "OP_FIXED,OP_DUELENGTH,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                    "OP_UPRICE,OP_AMOUNT,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                    "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE,OP_BSH,OP_QRUniqCode) VALUES('" + Code + "','" + UniqueID + "','" + SQCODE + "','" +
                    MaterialCode + "','" +
                    Fixed + "','" + DueLength + "','" + Length + "','" +
                    Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','" + WG_UPRICE + "','" + WG_AMOUNT2 + "','" +
                    WarehouseOutCode + "','" + PositionCode + "','" + PlanMode + "','" +
                    PTC + "','" + OrderID + "','" + Note + "','','" + bsh + "'," + Convert.ToInt32(OP_QRUniqCode) + ")";
                    sqlUpdateQR2 = "update midTable_QROut set QROut_State='1' where QROut_ID=" + Convert.ToInt32(OP_QRUniqCode) + "";
                    sqllist.Add(sqlUpdateQR2);
                }
                else
                {
                    sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                    "OP_FIXED,OP_DUELENGTH,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                    "OP_UPRICE,OP_AMOUNT,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                    "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE,OP_BSH,OP_QRUniqCode) VALUES('" + Code + "','" + UniqueID + "','" + SQCODE + "','" +
                    MaterialCode + "','" +
                    Fixed + "','" + DueLength + "','" + Length + "','" +
                    Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','" + WG_UPRICE + "','" + WG_AMOUNT2 + "','" +
                    WarehouseOutCode + "','" + PositionCode + "','" + PlanMode + "','" +
                    PTC + "','" + OrderID + "','" + Note + "','','" + bsh + "',NULL)";
                }
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                sqllist.Add(sql);
            }
            DBCallCommon.ExecuteTrans(sqllist);
            sqllist.Clear();
            LabelState.Text = "1";
            LabelMessage.Text = "保存成功";
            if (!string.IsNullOrEmpty(errorWlbmORsfwl))
            {
                errorBMorSL.Text = errorWlbmORsfwl;

                lberrorbottom.Text = errorWlbmORsfwl;
            }
            DeleteBill.Enabled = true;
        }
        }
        protected void Btn_daochu_Click(object sender, EventArgs e)
        {
            string sqltext = "SELECT PTC,TSAID,id,Verifier,Dep,Sender FROM  View_SM_OUT  where id='" + LabelTrueCode.Text.ToString() + "' ";
            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
            string filename ="生产领料单";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("生产领料单模版2.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ICellStyle style2 = wk.CreateCellStyle();
                style2.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                style2.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                style2.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                style2.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                ISheet sheet0 = wk.GetSheetAt(0);
                string sql = "select MaterialName+Standard,GB,Attribute,Unit,DueNumber,RealNumber FROM  View_SM_OUT  where id='" + LabelTrueCode.Text.ToString() + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                ////填充数据
                string engname = "";
                if (dt1.Rows[0]["PTC"].ToString() != "备库")
                {
                     engname = dt1.Rows[0]["PTC"].ToString().Split('_')[0] + "_" + dt1.Rows[0]["PTC"].ToString().Split('_')[1];
                }
                else
                    if (dt1.Rows[0]["PTC"].ToString() == "备库")
                    {
                        string txt = "select '('+TSA_PJID+')'+TSA_ENGNAME as engname from TBPM_TCTSASSGN where TSA_ID='" + TextBoxSCZH.Text.ToString() + "'";
                        System.Data.DataTable dt4 = DBCallCommon.GetDTUsingSqlText(txt);
                        engname = dt4.Rows[0]["engname"].ToString();
                    }
                IRow row3 = sheet0.GetRow(1);
                IRow row2 = sheet0.GetRow(2);
                row3.GetCell(5).SetCellValue("单号：" + dt1.Rows[0]["id"].ToString());
                row2.GetCell(1).SetCellValue(engname);

                row2.GetCell(3).SetCellValue(dt1.Rows[0]["TSAID"].ToString());
                row2.GetCell(5).SetCellValue(LabelDoc.Text.ToString());
               
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 4);
                    row.Height = 20 * 20;
                   
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {

                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                        row.Cells[j].CellStyle = style2;
                    }
                }
                int k = dt.Rows.Count + 5;
                IRow row1 = sheet0.CreateRow(k);
                row1.CreateCell(0).SetCellValue("负责人：" + dt1.Rows[0]["Verifier"].ToString());
                row1.CreateCell(1).SetCellValue("领料：" + dt1.Rows[0]["Dep"].ToString());
                row1.CreateCell(2).SetCellValue("发料人：" + dt1.Rows[0]["Sender"].ToString());
                row1.CreateCell(4).SetCellValue("日期："+DateTime.Now.ToString("yyyy-MM-dd"));
                for (int i = 0; i <= dt.Columns.Count; i++)
                { 
                    sheet0.AutoSizeColumn(i);
                }
                sheet0.ForceFormulaRecalculation = true;

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        private static void CloseExeclProcess(Microsoft.Office.Interop.Excel.Application excelApp)
        {
            #region kill excel process

            System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            foreach (System.Diagnostics.Process p in procs)
            {
                int baseAdd = p.MainModule.BaseAddress.ToInt32();
                //oXL is Excel.ApplicationClass object 
                if (baseAdd == excelApp.Hinstance)
                {
                    p.Kill();
                    break;
                }
            }
            #endregion
        }

        //审核操作如何与库存信息关联
        protected void Verify_Click(object sender, EventArgs e)
        {
            //检查是否现场直发
            string xczfif = "";
            if (chkxczf.Checked)
            {
                xczfif = "1";
            }
            else
            {
                xczfif = "";
            }


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
                    LabelMessage.Text = "任务号为空，单据不能审核！";

                    string script = @"alert('任务号为空，单据不能审核！');";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    return;
                }

                //string Comment = TextBoxComment.Text;

                string Comment = DropDownListBZ.SelectedValue;

                //string ZXMC = DropDownListZXMC.SelectedValue;

                string DocCode = LabelDocCode.Text;
                string VerifierCode = Session["UserID"].ToString();
                string SendClerkCode = DropDownListSender.SelectedValue;

                if (SendClerkCode != DocCode) //制单人，审核人，发料人不能相同
                {
                    LabelMessage.Text = "请选择发料人！";

                    string script = @"alert('请选择发料人');";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    return;
                }
                string NOTE1 = TextBoxNOTE1.Text;
                string BillType = string.Empty;

                if (LabelBillType.Text.Trim() == string.Empty || LabelBillType.Text.Trim() == "1")
                {
                    BillType = "1";
                }
                else if (LabelBillType.Text.Trim() == "4")
                {
                    BillType = "4";
                }

                int pagenum = 0;

                try
                {
                    pagenum = Convert.ToInt32(TextBoxPageNum.Text.Trim());
                }

                catch { pagenum = 0; }
                

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
                        "OP_STATE,OP_NOTE,OP_BILLTYPE,OP_RealTime,OP_PAGENUM,OP_NOTE1,OP_XCZF) VALUES('" + Code + "','" + LLDepCode + "','" + Date + "','" + SCZH + "','" +
                        SendClerkCode + "','" + DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','" +
                        Colour + "','1','" + Comment + "','" + BillType + "',convert(varchar(50),getdate(),120),'" + pagenum + "','" + NOTE1 + "','" + xczfif + "')";

                    sqllist.Add(sql);

                }
                else
                {

                    sql = "update TBWS_OUT set OP_DEP='" + LLDepCode + "',OP_DATE='" + Date + "',OP_TSAID='" + SCZH + "',";
                    sql += "OP_SENDER='" + SendClerkCode + "',OP_DOC='" + DocCode + "',OP_VERIFIER='" + VerifierCode + "',OP_VERIFYDATE='" + ApproveDate + "',OP_HSFLAG='0',";
                    sql += "OP_ROB='" + Colour + "',OP_STATE='1',OP_NOTE='" + Comment + "',OP_BILLTYPE='" + BillType + "',OP_RealTime=convert(varchar(50),getdate(),120),OP_PAGENUM='" + pagenum + "',OP_NOTE1='" + NOTE1 + "',OP_XCZF='" + xczfif + "' where OP_CODE='" + Code + "'";

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

                    string SQCODE = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;

                    //旧的仓库唯一号

                    string MaterialCode = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text.Trim();
                    string LotNumber = ((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxLotNumber")).Text.Trim();
                    string Fixed = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;

                    int DueLength = 0;
                    try { DueLength = Convert.ToInt32(Convert.ToSingle(((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelLength")).Text)); }
                    catch { DueLength = 0; }


                    int Length = 0;
                    try { Length = Convert.ToInt32(Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxLength")).Text)); }
                    catch { Length = 0; }
                    int Width = 0;
                    try { Width = Convert.ToInt32(Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxWidth")).Text)); }
                    catch { Width = 0; }

                    string WarehouseOutCode = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;

                    string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;

                    //if (PositionCode == string.Empty || PositionCode == "0")
                    //{
                    //    HasError = true;
                    //    ErrorType = 4;
                    //    break;
                    //}

                    string PTC = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;

                    string sqltext = "select count(*) from View_SM_PROJTEMP where PTCFrom='" + PTC + "' and state<='2' and  PTCFrom<>'备库' "; //待项目结转备库的物料不能出库
                    DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (Convert.ToInt32(dt1.Rows[0][0].ToString()) > 0)
                    {
                        LabelMessage.Text = "物料(" + PTC + ")在项目结转备库中待审批，现在不能出库此物料";
                        sqllist.Clear();
                        return;
                    }
                    //保存仓库号时需要重新获取
                    //@mid+@mptc+@mlotnum+CAST(@mlength AS VARCHAR(50))+CAST(@mwidth AS VARCHAR(50))+@mstid+@mwlid（物料代码+计划跟踪号+批号+长+宽+仓库+仓位）

                    //string SQCODE = MaterialCode + PTC + LotNumber + Convert.ToString(Length) + Convert.ToString(Width) + WarehouseOutCode + PositionCode;

                    //新的仓库唯一号

                    float DN = 0;
                    try
                    {
                        float temp = Convert.ToSingle(((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelDN")).Text);
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
                        float temp = Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxRN")).Text);
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
                        Int32 temp = Convert.ToInt32(((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelDQN")).Text);
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
                        Int32 temp = Convert.ToInt32(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxRQN")).Text);
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

                    string PlanMode = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text;
                    //string PTC = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;
                    string OrderID = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                    string Note = ((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;
                    string bsh = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelBSH")).Text;
                    string WG_UPRICE = "0";
                    string WG_AMOUNT = "0";
                    string sqltext2 = "select WG_UPRICE,WG_AMOUNT from TBWS_INDETAIL where WG_PTCODE='" + PTC + "'";
                    System.Data.DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext2);
                    string sqltext02 = "select WG_UPRICE,WG_AMOUNT from TBWS_INDETAIL where WG_MARID='" + MaterialCode.ToString().Trim() + "'";
                    System.Data.DataTable dt02 = DBCallCommon.GetDTUsingSqlText(sqltext02);
                    if (dt2.Rows.Count > 0)
                    {
                        WG_UPRICE = dt2.Rows[0]["WG_UPRICE"].ToString();
                        WG_AMOUNT = dt2.Rows[0]["WG_AMOUNT"].ToString();
                    }
                    else if (dt2.Rows.Count == 0 && dt02.Rows.Count > 0)
                    {
                        WG_UPRICE = dt02.Rows[0]["WG_UPRICE"].ToString();
                        WG_AMOUNT = dt02.Rows[0]["WG_AMOUNT"].ToString();
                    }
                    double WG_AMOUNT1 = RN * Convert.ToDouble(WG_UPRICE);
                    decimal WG_AMOUNT2 = Math.Round((decimal)WG_AMOUNT1, 2, MidpointRounding.AwayFromZero);
                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    string OP_QRUniqCode = ((Label)this.GridView1.Rows[i].FindControl("LabelOP_QRUniqCode")).Text;
                    string sqlUpdateQR2 = "";
                    if (OP_QRUniqCode != "")
                    {
                        sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                            "OP_FIXED,OP_DUELENGTH,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                            "OP_UPRICE,OP_AMOUNT,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                            "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE,OP_BSH,OP_QRUniqCode) VALUES('" + Code + "','" + UniqueID + "','" + SQCODE + "','" +
                            MaterialCode + "','" +
                            Fixed + "','" + DueLength + "','" + Length + "','" +
                            Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','" + WG_UPRICE + "','" + WG_AMOUNT2 + "','" +
                            WarehouseOutCode + "','" + PositionCode + "','" + PlanMode + "','" +
                            PTC + "','" + OrderID + "','" + Note + "','','" + bsh + "'," + Convert.ToInt32(OP_QRUniqCode) + ")";
                        sqlUpdateQR2 = "update midTable_QROut set QROut_State='1' where QROut_ID=" + Convert.ToInt32(OP_QRUniqCode) + "";
                        sqllist.Add(sqlUpdateQR2);
                    }
                    else
                    {
                        sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                            "OP_FIXED,OP_DUELENGTH,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                            "OP_UPRICE,OP_AMOUNT,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                            "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE,OP_BSH,OP_QRUniqCode) VALUES('" + Code + "','" + UniqueID + "','" + SQCODE + "','" +
                            MaterialCode + "','" +
                            Fixed + "','" + DueLength + "','" + Length + "','" +
                            Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','" + WG_UPRICE + "','" + WG_AMOUNT2 + "','" +
                            WarehouseOutCode + "','" + PositionCode + "','" + PlanMode + "','" +
                            PTC + "','" + OrderID + "','" + Note + "','','" + bsh + "',NULL)";
                    }
                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
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
                    }
                    else if (ErrorType == 2)
                    {
                        LabelMessage.Text = "出库数大于库存数，单据不能审核！";

                        string script = @"alert('出库数大于库存数，单据不能审核！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
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
                        Response.Redirect("SM_WarehouseOUT_LL.aspx?FLAG=OPEN&&ID=" + Code);
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

                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    string sqlgetdata = "select * from TBWS_OUTDETAIL where OP_CODE='" + LabelCode.Text + "'";
                    DataTable dtgetdata = DBCallCommon.GetDTUsingSqlText(sqlgetdata);
                    string sqlUpdateQR = "";
                    if (dtgetdata.Rows.Count > 0)
                    {
                        for (int k = 0; k < dtgetdata.Rows.Count; k++)
                        {
                            if (dtgetdata.Rows[k]["OP_QRUniqCode"].ToString().Trim() != "")
                            {
                                sqlUpdateQR = "update midTable_QROut set QROut_State='0' where QROut_ID=" + Convert.ToInt32(dtgetdata.Rows[k]["OP_QRUniqCode"].ToString().Trim()) + "";
                                sqllist.Add(sqlUpdateQR);
                            }
                        }
                    }
                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

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
                    Response.Redirect("SM_WarehouseOUT_LL.aspx?FLAG=OPEN&&ID=" + LabelCode.Text);
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
                if (((System.Web.UI.WebControls.CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked == true)
                {
                    uniqueid = ((System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("LabelUniqueID")).Text;
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
            Response.Redirect("SM_WarehouseOUT_LL.aspx?FLAG=PUSHRED&&ID=" + LabelCode.Text);
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
                if (((System.Web.UI.WebControls.CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked == true)
                {
                    string ptc = ((System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("LabelPTC")).Text;
                    Response.Redirect("SM_Warehouse_RelatedDocument.aspx?PTC=" + ptc);
                    return;
                }
            }
            LabelMessage.Text = "请选择一条要查询的记录！";
        }

        protected void tostorge_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Warehouse_Query.aspx?FLAG=PUSHLLOUT");
        }

        protected void AdjustLenWid_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("TextBoxLength")).Enabled = true;
                ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("TextBoxWidth")).Enabled = true;
            }
        }

        protected void TextBoxSCZH_TextChanged(object sender, EventArgs e)
        {
            string engid = TextBoxSCZH.Text.Trim();

            //engid = engid.Split('-')[engid.Split('-').Length - 1];

            string sql = "select TSA_PJNAME,TSA_ENGNAME,TSA_ID  FROM View_TM_TaskAssign WHERE TSA_FATHERNODE='0' and TSA_ID='" + engid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                //TextBoxSCZH.Text = dt.Rows[0]["TSA_PJNAME"] + "-" + dt.Rows[0]["TSA_ENGNAME"] + "-" + dt.Rows[0]["TSA_ID"];
                TextBoxSCZH.Text = dt.Rows[0]["TSA_ID"].ToString();
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
                if ((gr.FindControl("LabelDQN") as System.Web.UI.WebControls.Label).Text.Trim() == "1"||(gr.FindControl("LabelDQN") as System.Web.UI.WebControls.Label).Text.Trim() == "0")
                {
                    ((System.Web.UI.WebControls.TextBox)gr.FindControl("TextBoxLength")).Enabled = true;
                    //((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("TextBoxWidth")).Enabled = true;
                }
                string checkptc = (gr.FindControl("LabelPTC") as System.Web.UI.WebControls.Label).Text.Trim();
                string checksql = "select *,(isnull(PUR_NUM,0)-isnull(BG_NUM,0)) as PUR_NUMR from TBPC_PURCHASEPLAN as a left join (select * from TBPC_BG where RESULT='已驳回') as b on a.PUR_PTCODE=b.BG_PTC where PUR_PTCODE='" + checkptc + "' and PUR_PTCODE not like '%备库%' and PUR_PTCODE not like '%beiku%' and PUR_PTCODE not like '%BEIKU%'";
                DataTable checkdt=DBCallCommon.GetDTUsingSqlText(checksql);
                if (checkdt.Rows.Count > 0)
                {
                    double plannum = CommonFun.ComTryDouble(checkdt.Rows[0]["PUR_NUMR"].ToString().Trim());
                    double realnum = CommonFun.ComTryDouble((gr.FindControl("TextBoxRN") as System.Web.UI.WebControls.TextBox).Text.Trim());
                    if(realnum>1.05*plannum)
                    {
                        (gr.FindControl("LabelPTC") as System.Web.UI.WebControls.Label).BackColor = System.Drawing.Color.Yellow;
                        (gr.FindControl("LabelPTC") as System.Web.UI.WebControls.Label).ToolTip = "定额数量：'" + plannum.ToString().Trim() + "'";
                    }
                    (gr.FindControl("lblDNUM") as System.Web.UI.WebControls.Label).Text = plannum.ToString().Trim();
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
                Response.Redirect("SM_WarehouseOUT_LL_Auto.aspx?FLAG=COPY&&ID=" + LabelCode.Text.Trim());
            }
            else
            {

                LabelCode.Text = generateCode();
                LabelTrueCode.Text = string.Empty;

                this.Page.Title = "天津重机领料单(" + LabelCode.Text + ")";

                string sql = "INSERT INTO TBWS_OUTCODE (OP_CODE,OP_BILLTYPE) VALUES ('" + LabelCode.Text + "','1')";

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
                    Btn_daochu.Visible = false;
                    SumPrint.Visible = false;
                    btnstorge.Visible = false;
                    copyform.Visible = false;
                }
            }
        }
        protected void btn_mto_Click(object sender, EventArgs e)
        {

            btn_mto.Visible = false;

            List<string> sqllist = new List<string>();

            string Code = generateMTOCode();

            string strsql = "insert into TBWS_MTOCODE (MTO_CODE) VALUES ('" + Code + "')";

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

                string MaterialCode = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text.Trim();
               
                string Fixed = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;

                int Length = 0;
                try { Length = Convert.ToInt32(Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxLength")).Text)); }
                catch { Length = 0; }
                int Width = 0;

                try { Width = Convert.ToInt32(Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxWidth")).Text)); }
                catch { Width = 0; }

                string LotNumber = ((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxLotNumber")).Text.Trim();

                string PTCFrom = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;

                string WarehouseCode = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;

                string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;

               

                //保存仓库号时需要重新获取
                //@mid+@mptc+@mlotnum+CAST(@mlength AS VARCHAR(50))+CAST(@mwidth AS VARCHAR(50))+@mstid+@mwlid（物料代码+计划跟踪号+批号+长+宽+仓库+仓位）

                string SQCODE = MaterialCode + PTCFrom + LotNumber + Convert.ToString(Length) + Convert.ToString(Width) + WarehouseCode + PositionCode;

                //新的仓库唯一号

                float RN = 0;
                try
                {
                    float temp = Convert.ToSingle(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxRN")).Text);
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
                    Int32 temp = Convert.ToInt32(((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxRQN")).Text);
                    if ((InputColour.Value == "1") && (temp < 0))
                    {
                        RQN = -temp;
                    }
                    else
                    { RQN = temp; }
                }
                catch { RQN = 0; }


                string PlanMode = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text;
                
                string OrderID = ((System.Web.UI.WebControls.Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;

                string Note = ((System.Web.UI.WebControls.TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;

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
                if (((System.Web.UI.WebControls.CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked == true)
                {
                    uniqueid = ((System.Web.UI.WebControls.Label)GridView1.Rows[i].FindControl("LabelUniqueID")).Text;
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
                    if (count > 0)
                    {
                        string script = @"alert('此单已经推红，不能再合并！');";
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
            if ((sender as System.Web.UI.WebControls.CheckBox).Text.Trim() != "标准件")
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
            

            if ((sender as System.Web.UI.WebControls.CheckBox).Checked)
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
        protected void ButtonSCHLL_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_WarehouseOUT_LL_Auto.aspx?FLAG=PUSHBLUE");
        }




        //发票打印
        protected void btnprint_click(object sender, EventArgs e)
        {
            string id = LabelTrueCode.Text.ToString().Trim();
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "llddy('" + id + "');", true);
        }
    }
}
