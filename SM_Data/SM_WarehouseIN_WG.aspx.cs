using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using AjaxControlToolkit;
using System.Text.RegularExpressions;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_WarehouseIN_WG : System.Web.UI.Page
    {
        private double tn = 0;//数量

        private Int32 tq = 0;//总重

        private double ta = 0;//金额

        private double tcta = 0;//含税金额

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //仓库
                getWarehouse();
                getStaff();
                initial();
            }
        }

        

        //获取系统关帐时间
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

            System.DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");

            return objymd;
        }




        //控制操作控件

        private void bindControlEnable(bool isVisual)
        {
            if (isVisual)
            {
                FS.Visible = true;
                CD.Visible = true;
                HB.Visible = true;
                HX.Visible = true;
                FHX.Visible = true;
                TH.Visible = true;
                DelForm.Visible = false;
                SumPrint.Visible = true;
                Print.Visible = true;
            }
            else
            {
                Save.Visible = true;
                Verify.Visible = true;
                FS.Visible = false;
                CD.Visible = false;
                HB.Visible = false;
                HX.Visible = false;
                FHX.Visible = false;
                TH.Visible = false;
                DelForm.Visible = true;
                SumPrint.Visible = false;
                Print.Visible = false;
            }
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

        //收料员
        protected void getStaff()
        {
            string sql = "SELECT DISTINCT ST_ID,ST_NAME FROM TBDS_STAFFINFO WHERE ST_DEPID='05'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListAcceptance.DataTextField = "ST_NAME";
            DropDownListAcceptance.DataValueField = "ST_ID";
            DropDownListAcceptance.DataSource = dt;
            DropDownListAcceptance.DataBind();
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

                    IsSelect = true;
                }
            }
            //取消勾选
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                (GridView1.Rows[i].FindControl("CheckBox1") as CheckBox).Checked = false;



                if (((Label)GridView1.Rows[i].FindControl("LabelWarehouse")).Text.Trim() != "--请选择--" && ((TextBox)GridView1.Rows[i].FindControl("TextBoxPosition")).Text.Trim() != "待查")
                {
                    ((Label)GridView1.Rows[i].FindControl("LabelPTC")).BackColor = System.Drawing.Color.Yellow;
                }
            }
        }



        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
              
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tn += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "RN"));
                tq += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Quantity"));
                ta += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                tcta += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CTA"));

                if (((System.Web.UI.WebControls.Label)e.Row.FindControl("LabelWG_QRUniqCode")).Text.Trim() != "")
                {
                    ((System.Web.UI.WebControls.TextBox)e.Row.FindControl("TextBoxRN")).Enabled = false;
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.Cells[12].FindControl("LabelTotalNum")).Text =Math.Round(tn,4).ToString();
                ((Label)e.Row.Cells[13].FindControl("LabelTotalQuantity")).Text = tq.ToString();
                ((Label)e.Row.Cells[17].FindControl("LabelTotalAmount")).Text = Math.Round(ta,2).ToString();
                ((Label)e.Row.Cells[18].FindControl("LabelTotalCTA")).Text = Math.Round(tcta,2).ToString();
            }
        }
        
        //初始化数据
        protected void initial()
        {
            string code = Request.QueryString["ID"].ToString();
            string flag = Request.QueryString["FLAG"].ToString();

            //下推红字入库
            /*
             * 这里的推红，还没有限制数量
             * 
             * 推红没有用到存储过程
             * 
             */
            if (code.Contains('X'))
            {
                this.Title = "外委入库(" + code + ")";
                LabelTittle.Text = "外委入库";
            }
            else if (code.Contains('B'))
            {
                this.Title = "结转备库(" + code + ")";
                LabelTittle.Text = "结转备库";
            }
            else if (code.Contains('T'))
            {
                this.Title = "其他入库(" + code + ")";
                LabelTittle.Text = "其他入库";
            }
            else
            {
                this.Title = "外购入库(" + code + ")";
            }
            
            if (flag == "PUSHRED")
            {

                //WG_NOTE AS Comment 备注，用于显示过磅和理论
                string sql = "SELECT WG_CODE AS Code,Supplier AS SupplierCode,(case when WG_BILLTYPE='3' or WG_BILLTYPE='4'  then WG_COMPANY else SupplierName end) as  Supplier," +
                    "WG_WAREHOUSE AS WarehouseCode,WS_NAME AS Warehouse,WG_LOCATION AS PositionCode,WG_ABSTRACT AS Abstract,WG_HDBH AS HDBH,WG_DATE AS Date," +
                    "Dep AS DepCode,DepName AS Dep,Clerk AS ClerkCode,ClerkName AS Clerk," +
                    "WG_DOC AS DocCode,DocName AS Doc,WG_REVEICER AS AcceptanceCode,ReveicerName AS Acceptance," +
                    "WG_VERIFIER AS VerifierCode,VerfierName AS Verifier,left(WG_VERIFYDATE,10) AS ApproveDate," +
                    "WG_STATE AS State,WG_TEARFLAG AS Split ,WG_CAVFLAG AS Articulation,WG_ROB AS Colour,WG_QRUniqCode FROM View_SM_IN WHERE WG_CODE='" + code + "'";

                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    LabelState.Text = "0";
                    InputColour.Value = "1";
                    LabelCode.Text = generateRedCode(code);                    
                    TextBoxDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    //获取制单时的系统关账时间
                    ClosingAccountDate(TextBoxDate.Text.Trim());

                    LabelSupplier.Text = dr["Supplier"].ToString();
                    InputSupplierCode.Value = dr["SupplierCode"].ToString();

                    try { DropDownListWarehouse.Items.FindByValue(dr["WarehouseCode"].ToString()).Selected = true; }
                    catch { }

                    //根据仓库选择仓位
                    getWarehousePosition();

                    DropDownListPosition.ClearSelection();

                    try
                    {
                        DropDownListPosition.Items.FindByValue(dr["PositionCode"].ToString()).Selected = true;//仓库
                    }
                    catch { }

                    TextBoxAbstract.Text = "";
                    TextBoxHDBH.Text = dr["HDBH"].ToString();
                    LabelDep.Text = dr["Dep"].ToString();
                    LabelDepCode.Text = dr["DepCode"].ToString();
                    LabelClerk.Text = dr["Clerk"].ToString();
                    LabelClerkCode.Text = dr["ClerkCode"].ToString();
                    LabelDoc.Text = Session["UserName"].ToString();
                    LabelDocCode.Text = Session["UserID"].ToString();
                    try { DropDownListAcceptance.Items.FindByValue(dr["AcceptanceCode"].ToString()).Selected = true; }
                    catch { }
                    //TextBoxReciever.Text = dr["Acceptance"].ToString();
                    //LabelRecieveCode.Text = dr["AcceptanceCode"].ToString();
                    LabelVerifier.Text = "";
                    LabelVerifierCode.Text = "";
                    LabelApproveDate.Text = "";
                }
                dr.Close();
                sql = "SELECT WG_UNIQUEID AS UniqueID,WG_MARID AS MaterialCode,MNAME AS MaterialName,CAIZHI AS Attribute,GB AS GB," +
                    "GUIGE AS MaterialStandard,WG_FIXEDSIZE AS Fixed,WG_LENGTH AS Length,WG_WIDTH AS Width,WG_LOTNUM AS LotNumber," +
                    "CGDW AS Unit,cast(WG_RSNUM as float)*(-1) AS RN,WG_RSFZNUM*(-1) AS Quantity," +
                    "cast(WG_UPRICE as float) AS UnitPrice,WG_TAXRATE AS TaxRate,cast(WG_CTAXUPRICE as float) AS CTUP,cast(WG_AMOUNT as float)*(-1) AS Amount,cast(WG_CTAMTMNY as float)*(-1) AS CTA,WG_WAREHOUSE AS WarehouseCode," +
                    "WS_NAME AS Warehouse,WG_LOCATION AS PositionCode,WL_NAME AS Position,WG_ORDERID AS OrderCode,WG_PMODE AS PlanMode," +
                    "WG_PTCODE AS PTC,WG_NOTE AS Comment,WG_STATE AS State,WG_CGMODE AS CGMODE,WG_QRUniqCode FROM View_SM_IN WHERE WG_CODE='" + code + "' AND DetailState='RED" + Session["UserID"].ToString() + "'";
                DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = tb;
                GridView1.DataBind();
                sql = "UPDATE TBWS_INDETAIL SET WG_STATE='' WHERE WG_STATE='RED" + Session["UserID"].ToString() + "' AND WG_CODE='" + code + "'";
                DBCallCommon.ExeSqlText(sql);
                LabelMessage.Text = "加载红字入库单成功！";
                ImageRed.Visible = true;
                DropDownListWarehouse.Enabled = false;
                DropDownListPosition.Enabled = false;
                DropDownListAcceptance.Enabled = false;
                Append.Visible = false;//推红，追加不可用
                Split.Visible = false;//推红，拆分不可用
                Delete.Visible = false;//推红，删除不可用

                foreach(GridViewRow gr in GridView1.Rows)
                {
                    //推红不容许改变数量
                    (gr.FindControl("TextBoxRN") as TextBox).Enabled = false;
                    (gr.FindControl("TextBoxQuantity") as TextBox).Enabled = false;
                    //TextBoxQuantity
                }




                bindControlEnable(false);
            }


            //下推蓝字入库
            if (flag == "PUSHBLUE")
            {
               //detailnote用于填写备注，理计和过磅
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
               //string sql = "SELECT orderno AS OrderCode,supplierid AS SupplierCode,suppliernm AS Supplier," +
               //     "ywyid AS ClerkCode,ywynm AS Clerk,depid AS DepCode,depnm AS Dep," +
               //     "marid AS MaterialCode,marnm AS MaterialName,margg AS MaterialStandard," +
               //     "fixed AS Fixed,length AS Length,width AS Width," +
               //     "marcz AS Attribute,margb AS GB,marunit AS Unit,cast((zxnum-recgdnum) as float) AS RN,0 AS Quantity,cast(price as float) AS UnitPrice,cast(ctprice as float) AS CTUP,cast(round((zxnum-recgdnum)*price,2) as float) AS Amount," +
               //     "taxrate AS TaxRate,cast(round((zxnum-recgdnum)*ctprice,2) as float) AS CTA,detailnote AS Comment,PO_MASHAPE AS PlanMode,ptcode AS PTC," +
               //     "totalstate AS State,UniqueID='',LotNumber='',WarehouseCode='0',Warehouse='--请选择--',PositionCode='0',Position='待查',PO_TUHAO AS CGMODE FROM View_TBPC_PURORDERDETAIL_PLAN_TOTAL WHERE whstate='BLUE" + Session["UserID"].ToString() + "'";
               string sql = "SELECT orderno AS OrderCode,supplierid AS SupplierCode,suppliernm AS Supplier," +
                   "ywyid AS ClerkCode,ywynm AS Clerk,depid AS DepCode,depnm AS Dep," +
                   "marid AS MaterialCode,marnm AS MaterialName,margg AS MaterialStandard," +
                   "fixed AS Fixed,length AS Length,width AS Width," +
                   "marcz AS Attribute,margb AS GB,marunit AS Unit,cast((zxnum-recgdnum) as float) AS RN,0 AS Quantity,cast(price as float) AS UnitPrice,cast(ctprice as float) AS CTUP,cast(round((zxnum-recgdnum)*price,2) as float) AS Amount," +
                   "taxrate AS TaxRate,cast(round((zxnum-recgdnum)*ctprice,2) as float) AS CTA,detailnote AS Comment,PO_MASHAPE AS PlanMode,ptcode AS PTC," +
                   "totalstate AS State,UniqueID='',LotNumber='',WarehouseCode='0',Warehouse='--请选择--',PositionCode='0',Position='待查',PO_TUHAO AS CGMODE,q.*,QRIn_ID as WG_QRUniqCode FROM View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a left join (select * from midTable_QRIn where QRIn_WHSTATE='1')q on a.ptcode=q.QRIn_PTC  WHERE whstate='BLUE" + Session["UserID"].ToString() + "'";
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                sql = "UPDATE TBPC_PURORDERDETAIL SET PO_WHSTATE='1' WHERE PO_WHSTATE='BLUE" + Session["UserID"].ToString() + "'";

                DBCallCommon.ExeSqlText(sql);

                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                string sqlQRIn = "update midTable_QRIn set QRIn_WHSTATE='0' where QRIn_WHSTATE='1'";
                string sqlGetRN = "";
                DBCallCommon.ExeSqlText(sqlQRIn);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["WG_QRUniqCode"].ToString().Trim() != "")
                    {
                        sqlGetRN = "select * from midTable_QRIn where CONVERT(varchar(20),QRIn_ID)='" + dt.Rows[i]["WG_QRUniqCode"].ToString().Trim() + "'";
                        DataTable dtGetRN = DBCallCommon.GetDTUsingSqlText(sqlGetRN);
                        if (dtGetRN.Rows.Count > 0)
                        {
                            dt.Rows[i]["RN"] = dtGetRN.Rows[0]["QRIn_Num"].ToString().Trim();
                        }
                    }
                }
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

                LabelState.Text = "0";
                InputColour.Value = "0";

                LabelCode.Text = generateBlueCode();

                sql = "INSERT INTO TBWS_INCODE (WG_CODE,WG_BILLTYPE) VALUES ('" + LabelCode.Text + "','0')";

                DBCallCommon.ExeSqlText(sql);

                TextBoxDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

                ClosingAccountDate(TextBoxDate.Text.Trim());

                LabelSupplier.Text = dt.Rows[0]["Supplier"].ToString();
                InputSupplierCode.Value = dt.Rows[0]["SupplierCode"].ToString();
                TextBoxAbstract.Text = "";
                TextBoxHDBH.Text = "";
                

                //根据情况决定是否加载部门和业务员

                LabelDep.Text = "";
                LabelDepCode.Text = "";

                DataView view = new DataView(dt);
                DataTable temp = view.ToTable(true, "DepCode"); 
                if (temp.Rows.Count == 1)
                {
                    LabelDep.Text = dt.Rows[0]["Dep"].ToString();
                    LabelDepCode.Text = dt.Rows[0]["DepCode"].ToString();
                }

                LabelClerk.Text = "";
                LabelClerkCode.Text = ""; 
               
                temp = view.ToTable(true, "ClerkCode");

                if (temp.Rows.Count == 1)
                {
                    LabelClerk.Text = dt.Rows[0]["Clerk"].ToString();
                    LabelClerkCode.Text = dt.Rows[0]["ClerkCode"].ToString();
                }

                //制单
                LabelDoc.Text = Session["UserName"].ToString();
                LabelDocCode.Text = Session["UserID"].ToString();

                //收料人，默认值为制单人，即session值
                try
                {
                    DropDownListAcceptance.Items.FindByValue(Session["UserID"].ToString()).Selected = true;
                }
                catch { }               
               

                //审核人
                LabelVerifier.Text = "";
                LabelVerifierCode.Text = "";

                //审核时间
                LabelApproveDate.Text = "";

                GridView1.DataSource = dt;

                GridView1.DataBind();

                LabelMessage.Text = "下推蓝字入库单完成！";

                bindControlEnable(false);
            }

            //入库单查看载入

            //反审核---只是提供查看的按钮

            if (flag == "READ")
            {

                string sql = "SELECT id as TrueCode ,WG_CODE AS Code,Supplier AS SupplierCode,(case when WG_BILLTYPE='3' or WG_BILLTYPE='4' then WG_COMPANY else SupplierName end) as Supplier," +
                   "WG_WAREHOUSE AS WarehouseCode,WS_NAME AS Warehouse,WG_LOCATION AS PositionCode,WG_ABSTRACT AS Abstract,WG_HDBH AS HDBH,WG_DATE AS Date," +
                   "Dep AS DepCode,DepName AS Dep,Clerk AS ClerkCode,ClerkName AS Clerk," +
                   "WG_DOC AS DocCode,DocName AS Doc,WG_REVEICER AS AcceptanceCode,ReveicerName AS Acceptance," +
                   "WG_VERIFIER AS VerifierCode,VerfierName AS Verifier,left(WG_VERIFYDATE,10) AS ApprovedDate," +
                   "WG_STATE AS State,WG_TEARFLAG AS Split ,WG_CAVFLAG AS Articulation,WG_ROB AS Colour,WG_GJSTATE as gj,WG_HSFLAG AS hs,WG_PRINTTIME,WG_QRUniqCode FROM View_SM_IN WHERE WG_CODE='" + code + "'";

                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);

                if (dr.Read())
                {

                    LabelState.Text = dr["State"].ToString();//审核状态

                    LabelGJState.Text = dr["gj"].ToString();//勾稽状态

                    LabelHXState.Text = dr["Articulation"].ToString();//核销

                    LabelHSState.Text = dr["hs"].ToString();//核算

                    InputColour.Value = dr["Colour"].ToString();

                    LabelCode.Text = dr["Code"].ToString();//入库单号
                    

                    //LabelTrueCode.Text = dr["TrueCode"].ToString();//入库单号

                    TextBoxDate.Text = dr["Date"].ToString();//日期

                    if (TextBoxDate.Text.Trim() == string.Empty)
                    {
                        TextBoxDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    //获取制单时的系统关账时间

                    ClosingAccountDate(TextBoxDate.Text.Trim());

                    LabelSupplier.Text = dr["Supplier"].ToString();//供应商

                    InputSupplierCode.Value = dr["SupplierCode"].ToString();//供应商编号

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

                    TextBoxAbstract.Text = dr["Abstract"].ToString();
                    TextBoxHDBH.Text = dr["HDBH"].ToString();
                    LabelDep.Text = dr["Dep"].ToString();
                    LabelDepCode.Text = dr["DepCode"].ToString();
                    LabelClerk.Text = dr["Clerk"].ToString();
                    LabelClerkCode.Text = dr["ClerkCode"].ToString();
                    LabelDoc.Text = dr["Doc"].ToString();
                    LabelDocCode.Text = dr["DocCode"].ToString();
                    try { DropDownListAcceptance.Items.FindByValue(dr["AcceptanceCode"].ToString()).Selected = true; }
                    catch { }
                        //TextBoxReciever.Text = dr["Acceptance"].ToString();//收料人姓名
                        //LabelRecieveCode.Text = dr["AcceptanceCode"].ToString();

                    LabelVerifier.Text = dr["Verifier"].ToString();
                    LabelVerifierCode.Text = dr["VerifierCode"].ToString();
                    LabelApproveDate.Text = dr["ApprovedDate"].ToString();
                    CheckBoxPrint.Text = "打印标识(<font color='red'>" + dr["WG_PRINTTIME"].ToString() + "</font>)";
                }
                dr.Close();


                //WG_NOTE AS Comment 备注，用于显示过磅和理论

                sql = "SELECT WG_UNIQUEID AS UniqueID,WG_MARID AS MaterialCode,MNAME AS MaterialName," +
                    "CAIZHI AS Attribute,GB AS GB,GUIGE AS MaterialStandard,WG_FIXEDSIZE AS Fixed,WG_LENGTH AS Length,WG_WIDTH AS Width," +
                    "WG_LOTNUM AS LotNumber,CGDW AS Unit,cast(WG_RSNUM as float) AS RN,WG_RSFZNUM AS Quantity," +
                    "cast(WG_UPRICE as float) AS UnitPrice,WG_TAXRATE AS TaxRate,cast(WG_CTAXUPRICE as float) AS CTUP," +
                    "cast(WG_AMOUNT as float) AS Amount,cast(WG_CTAMTMNY as float) AS CTA,WG_WAREHOUSE AS WarehouseCode," +
                    "WS_NAME AS Warehouse,WG_LOCATION AS PositionCode,WL_NAME AS Position," +
                    "WG_ORDERID AS OrderCode,WG_PMODE AS PlanMode,WG_PTCODE AS PTC,WG_NOTE AS Comment," +
                    "WG_STATE AS State,WG_CGMODE AS CGMODE,WG_QRUniqCode FROM View_SM_IN WHERE WG_CODE='" + code + "' order by WG_UNIQUEID";

                DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = tb;
                GridView1.DataBind();
                
                Save.Visible = false;//保存不可见
                Verify.Visible = false;//审核不可见

                if (LabelState.Text == "2")
                {
                    //审核图像可见
                    ImageVerify.Visible = true;

                    Append.Visible = false;//追加不可见
                    Split.Visible = false;//拆分不可见
                    Delete.Visible = false;//删除不可见
                    
                    //bindControlEnable(true);
                    if (Session["UserID"].ToString() == LabelVerifierCode.Text.Trim())
                    {
                        bindControlEnable(true);
                        btn_wxout.Visible = true;
                    }
                    else
                    {
                        FS.Visible = false;
                        CD.Visible = false;
                        HB.Visible = false;
                        HX.Visible = false;
                        FHX.Visible = false;
                        TH.Visible = false;
                        DelForm.Visible = false;
                    }

                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        ((TextBox)GridView1.Rows[i].FindControl("TextBoxLength")).Enabled = false;
                        ((TextBox)GridView1.Rows[i].FindControl("TextBoxWidth")).Enabled = false;
                        ((TextBox)GridView1.Rows[i].FindControl("TextBoxLotNumber")).Enabled = false;
                        ((TextBox)GridView1.Rows[i].FindControl("TextBoxRN")).Enabled = false;
                        ((TextBox)GridView1.Rows[i].FindControl("TextBoxQuantity")).Enabled = false;
                    }
                }
                else
                {
                    bindControlEnable(false);
                }

                if ( InputColour.Value == "1")
                {
                    //红字可见
                    ImageRed.Visible = true;
                }
                if (LabelCode.Text.Trim().Contains('G'))
                {
                    btnorder.Visible = true;
                }
                if (LabelCode.Text.Trim().Contains('B'))
                {
                    Append.Visible = false;
                    //Split.Enabled = false;
                    btnbk.Visible = true;
                }
                if (LabelCode.Text.Trim().Contains('X'))
                {
                    Append.Visible = false;
                    //Split.Enabled = false;
                   
                }
                if (LabelCode.Text.Trim().Contains('T'))
                {
                    Append.Visible = false;
                    //Split.Enabled = false;

                }
               
            }
        }

        protected string generateRedCode(string code)
        {
            /*
            * sql语句
            */
            List<string> lt = new List<string>();

            string sql = "SELECT WG_CODE FROM TBWS_IN where WG_CODE like '" + code + "R%'";

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    lt.Add(sdr["WG_CODE"].ToString());
                }
            }

            sdr.Close();

            string[] idlist = lt.ToArray();

            if (idlist.Count<string>() == 0)
            {
                return code + "R1";
            }
            else
            {
                string tempstr = idlist.Max<string>();
                int tempnum = Convert.ToInt32((tempstr.Substring(tempstr.IndexOf('R',0)+1, tempstr.Length-tempstr.IndexOf('R',0)-1)));
                tempnum++;
                tempstr = tempstr.Substring(0, tempstr.IndexOf('R',0)+1) + tempnum.ToString();
                return tempstr;
            }
        }

        //蓝单的产生规则需要重新考虑

        protected string generateBlueCode()
        {
            /*
             * sql语句
             */
            List<string> lt = new List<string>();

            string sql = "SELECT WG_CODE FROM TBWS_INCODE WHERE len(WG_CODE)=10 AND WG_BILLTYPE='0'";

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    lt.Add(sdr["WG_CODE"].ToString());
                }
            }

            sdr.Close();

            string[] wsidlist = lt.ToArray();

            if (wsidlist.Count<string>() == 0)
            {
                return "G000000001";
            }
            else
            {
                string tempstr = wsidlist.Max<string>();
                int tempnum = Convert.ToInt32((tempstr.Substring(1, 9)));
                tempnum++;
                tempstr = "G" + tempnum.ToString().PadLeft(9, '0');
                return tempstr;
            }
        }

        //获取当前GridView1中的数据
        protected DataTable getDataFromGridView()
        {
            DataTable tb = new DataTable();
            tb.Columns.Add("UniqueID", System.Type.GetType("System.String"));
            tb.Columns.Add("MaterialCode", System.Type.GetType("System.String"));
            tb.Columns.Add("MaterialName", System.Type.GetType("System.String"));
            tb.Columns.Add("MaterialStandard", System.Type.GetType("System.String"));
            tb.Columns.Add("Fixed", System.Type.GetType("System.String"));
            tb.Columns.Add("Length", System.Type.GetType("System.String"));
            tb.Columns.Add("Width", System.Type.GetType("System.String"));
            tb.Columns.Add("GB", System.Type.GetType("System.String")); 
            tb.Columns.Add("Attribute", System.Type.GetType("System.String"));
            tb.Columns.Add("LotNumber", System.Type.GetType("System.String"));
            tb.Columns.Add("Unit", System.Type.GetType("System.String"));
            //实发数量
            tb.Columns.Add("RN", System.Type.GetType("System.String"));
            //辅助数量
            tb.Columns.Add("Quantity", System.Type.GetType("System.String"));
            //单价
            tb.Columns.Add("UnitPrice", System.Type.GetType("System.String"));
            //税率
            tb.Columns.Add("TaxRate", System.Type.GetType("System.String"));
            //含税单价
            tb.Columns.Add("CTUP", System.Type.GetType("System.String"));
            //金额
            tb.Columns.Add("Amount", System.Type.GetType("System.String"));
            //含税金额
            tb.Columns.Add("CTA", System.Type.GetType("System.String"));
            tb.Columns.Add("Comment", System.Type.GetType("System.String"));
            tb.Columns.Add("Warehouse", System.Type.GetType("System.String"));
            tb.Columns.Add("WarehouseCode", System.Type.GetType("System.String"));
            tb.Columns.Add("Position", System.Type.GetType("System.String"));
            tb.Columns.Add("PositionCode", System.Type.GetType("System.String"));
            tb.Columns.Add("OrderCode", System.Type.GetType("System.String"));
            tb.Columns.Add("CGMODE", System.Type.GetType("System.String"));
            tb.Columns.Add("PlanMode", System.Type.GetType("System.String"));
            tb.Columns.Add("PTC", System.Type.GetType("System.String"));

            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            tb.Columns.Add("WG_QRUniqCode", System.Type.GetType("System.String"));
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow GRow = GridView1.Rows[i];
                DataRow row = tb.NewRow();
                row["UniqueID"] = ((Label)GRow.FindControl("LabelUniqueID")).Text;
                row["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                row["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                row["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
                row["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;
                row["LotNumber"] = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text;
                row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                row["RN"] = ((TextBox)GRow.FindControl("TextBoxRN")).Text;
                row["Quantity"] = ((TextBox)GRow.FindControl("TextBoxQuantity")).Text;
                row["UnitPrice"] = ((Label)GRow.FindControl("LabelUnitPrice")).Text;
                row["TaxRate"] = ((Label)GRow.FindControl("LabelTaxRate")).Text;
                row["CTUP"] = ((Label)GRow.FindControl("LabelCTUP")).Text;
                row["Amount"] = ((HtmlInputText)GRow.FindControl("InputAmount")).Value;
                row["CTA"] = ((HtmlInputText)GRow.FindControl("InputCTA")).Value;
                row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                row["Position"] = ((TextBox)GRow.FindControl("TextBoxPosition")).Text;
                row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                row["OrderCode"] = ((Label)GRow.FindControl("LabelOrderCode")).Text;
                row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                row["CGMODE"] = ((Label)GRow.FindControl("LabelCGMode")).Text;
                row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                row["WG_QRUniqCode"] = ((Label)GRow.FindControl("LabelWG_QRUniqCode")).Text;
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                tb.Rows.Add(row);
            }
            return tb;
        }

        //追加入库单条目

        /*
         * 订单视图
         */
        protected void Append_Click(object sender, EventArgs e)
        {

            /*这里的字段“PO_STATE”可能需要改*/
            /*
             * 先得到现有的数据，从GridView中
             */
            DataTable dt = getDataFromGridView();
            /*
             * sql语句,查找刚才追加的数据
             */
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            //string sql = "SELECT orderno AS OrderCode,supplierid AS SupplierCode,suppliernm AS Supplier," +
            //       "ywyid AS ClerkCode,ywynm AS Clerk,depid AS DepCode,depnm AS Dep," +
            //       "marid AS MaterialCode,marnm AS MaterialName,margg AS MaterialStandard," +
            //       "fixed AS Fixed,CAST(length AS VARCHAR(50)) AS Length,CAST(width AS VARCHAR(50)) AS Width," +
            //       "marcz AS Attribute,margb AS GB,marunit AS Unit,CAST(cast((zxnum-recgdnum) as float) AS VARCHAR(50)) AS RN,CAST(0 AS VARCHAR(50)) AS Quantity,CAST(price AS VARCHAR(50)) AS UnitPrice,CAST(ctprice AS VARCHAR(50)) AS CTUP,CAST(cast((zxnum-recgdnum)*price as float) AS VARCHAR(50)) AS Amount," +
            //       "CAST(taxrate AS VARCHAR(50)) AS TaxRate,CAST(cast((zxnum-recgdnum)*ctprice as float) AS VARCHAR(50)) AS CTA,detailnote AS Comment,PO_MASHAPE AS PlanMode,ptcode AS PTC," +
            //       "totalstate AS State,UniqueID='',LotNumber='',WarehouseCode='" + DropDownListWarehouse.SelectedValue + "',Warehouse='" + DropDownListWarehouse.SelectedItem.Text + "',PositionCode='" + DropDownListPosition.SelectedValue + "',Position='" + DropDownListPosition.SelectedItem.Text + "',PO_TUHAO AS CGMODE FROM View_TBPC_PURORDERDETAIL_PLAN_TOTAL WHERE whstate='APPENDIN" + Session["UserID"].ToString() + "'";
            string sql = "SELECT orderno AS OrderCode,supplierid AS SupplierCode,suppliernm AS Supplier," +
                   "ywyid AS ClerkCode,ywynm AS Clerk,depid AS DepCode,depnm AS Dep," +
                   "marid AS MaterialCode,marnm AS MaterialName,margg AS MaterialStandard," +
                   "fixed AS Fixed,CAST(length AS VARCHAR(50)) AS Length,CAST(width AS VARCHAR(50)) AS Width," +
                   "marcz AS Attribute,margb AS GB,marunit AS Unit,CAST(cast((zxnum-recgdnum) as float) AS VARCHAR(50)) AS RN,CAST(0 AS VARCHAR(50)) AS Quantity,CAST(price AS VARCHAR(50)) AS UnitPrice,CAST(ctprice AS VARCHAR(50)) AS CTUP,CAST(cast((zxnum-recgdnum)*price as float) AS VARCHAR(50)) AS Amount," +
                   "CAST(taxrate AS VARCHAR(50)) AS TaxRate,CAST(cast((zxnum-recgdnum)*ctprice as float) AS VARCHAR(50)) AS CTA,detailnote AS Comment,PO_MASHAPE AS PlanMode,ptcode AS PTC," +
                   "totalstate AS State,UniqueID='',LotNumber='',WarehouseCode='" + DropDownListWarehouse.SelectedValue + "',Warehouse='" + DropDownListWarehouse.SelectedItem.Text + "',PositionCode='" + DropDownListPosition.SelectedValue + "',Position='" + DropDownListPosition.SelectedItem.Text + "',PO_TUHAO AS CGMODE,q.*,QRIn_ID as WG_QRUniqCode FROM View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a left join (select * from midTable_QRIn where QRIn_WHSTATE='1')q on a.ptcode=q.QRIn_PTC WHERE whstate='APPENDIN" + Session["UserID"].ToString() + "'";
            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
            dt.Merge(DBCallCommon.GetDTUsingSqlText(sql));
            GridView1.DataSource = dt;
            GridView1.DataBind();
            sql = "UPDATE TBPC_PURORDERDETAIL SET PO_WHSTATE='1' WHERE PO_WHSTATE='APPENDIN" + Session["UserID"].ToString() + "'";
            DBCallCommon.ExeSqlText(sql);
            LabelMessage.Text = "追加成功！";
            CheckSQCODE();
        }


        protected void CheckSQCODE() //有重复的计划跟踪号有颜色区分
        {

            for (int i = 0; i < (GridView1.Rows.Count - 1); i++)
            {
                GridViewRow gvrow0 = GridView1.Rows[i];
                string sqcode = ((Label)gvrow0.FindControl("LabelPTC")).Text;
                //string materialcode = ((Label)gvrow0.FindControl("LabelMaterialCode")).Text;
                //if ((materialcode.Substring(0, 5) != "01.07") && (materialcode.Substring(0, 5) != "01.14"))
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


        //protected void TextBoxReciever_TextChanged(object sender, EventArgs e)
        //{
        //    string staffcode= TextBoxReciever.Text.Trim();
        //    staffcode = staffcode.Split('-')[staffcode.Split('-').Length - 1];

        //    string sql = "select st_name,st_code FROM  TBDS_STAFFINFO WHERE st_code='" + staffcode + "'";
        //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
        //    if (dt.Rows.Count > 0)
        //    {
        //        TextBoxReciever.Text = dt.Rows[0]["st_name"].ToString();
        //        LabelRecieveCode.Text = dt.Rows[0]["st_code"].ToString();
        //    }
        //    else
        //    {
        //        TextBoxReciever.Text = string.Empty;
        //        LabelRecieveCode.Text = string.Empty;
        //    }
        //}






        /*
         * 这里不需要修改
         */

        //拆分条目并且产生批号
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
            string mode = RadioButtonListSplitMode.SelectedValue.ToString();//记录拆分模式---均分，复制

            string mode2 = RadioButtonListSplitMode2.SelectedValue.ToString();//记录复制模式---插入，追加
            
            DataTable tb = new DataTable();

            tb.Columns.Add("UniqueID", System.Type.GetType("System.String")); 
            tb.Columns.Add("MaterialCode", System.Type.GetType("System.String"));
            tb.Columns.Add("MaterialName", System.Type.GetType("System.String"));
            tb.Columns.Add("MaterialStandard", System.Type.GetType("System.String"));
            tb.Columns.Add("Fixed", System.Type.GetType("System.String"));
            tb.Columns.Add("Length", System.Type.GetType("System.String"));
            tb.Columns.Add("Width", System.Type.GetType("System.String"));
            tb.Columns.Add("GB", System.Type.GetType("System.String"));
            tb.Columns.Add("Attribute", System.Type.GetType("System.String"));
            tb.Columns.Add("LotNumber", System.Type.GetType("System.String"));
            tb.Columns.Add("Unit", System.Type.GetType("System.String"));
            tb.Columns.Add("RN", System.Type.GetType("System.String"));
            tb.Columns.Add("Quantity", System.Type.GetType("System.String"));
            tb.Columns.Add("UnitPrice", System.Type.GetType("System.String"));
            tb.Columns.Add("TaxRate", System.Type.GetType("System.String"));
            tb.Columns.Add("CTUP", System.Type.GetType("System.String"));
            tb.Columns.Add("Amount", System.Type.GetType("System.String"));
            tb.Columns.Add("CTA", System.Type.GetType("System.String"));
            tb.Columns.Add("Comment", System.Type.GetType("System.String"));
            tb.Columns.Add("Warehouse", System.Type.GetType("System.String"));
            tb.Columns.Add("WarehouseCode", System.Type.GetType("System.String"));
            tb.Columns.Add("Position", System.Type.GetType("System.String"));
            tb.Columns.Add("PositionCode", System.Type.GetType("System.String"));
            tb.Columns.Add("OrderCode", System.Type.GetType("System.String"));
            tb.Columns.Add("PlanMode", System.Type.GetType("System.String"));
            tb.Columns.Add("CGMODE", System.Type.GetType("System.String"));
            tb.Columns.Add("PTC", System.Type.GetType("System.String"));

            //-----插入记录模式

            if (mode2 == "0")
            {
                #region

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow GRow = GridView1.Rows[i];
                    /*
                     * 
                     * **/
                    if (((CheckBox)GRow.FindControl("CheckBox1")).Checked == true)
                    {
                        //获取当前物料已有批号
                        string mcode = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                        string wcode = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                        string pcode = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;

                        #region 批号生成规则   获取同一种物料的最大批号
                       
                        string sql = "SELECT SUBSTRING(SQ_LOTNUM,0,4) AS LN  FROM TBWS_STORAGE WHERE SQ_MARID='" + mcode + "' AND SQ_WAREHOUSE='" + wcode + "' AND SQ_LOCATION='" + pcode + "'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        Int32 ln = 0;
                        if (dt.Rows.Count != 0)
                        {
                            try { ln = Convert.ToInt32(dt.Compute("Max(LN)", "")); }
                            catch { ln = 0; }
                        }

                        #endregion

                        //数据均分模式
                        if (mode == "0")
                        {
                            float num = 0;
                            Int32 quantity = 0;
                            float amount = 0;
                            float cta = 0;
                            try
                            {
                                num = Convert.ToSingle(((TextBox)GRow.FindControl("TextBoxRN")).Text);
                                quantity = Convert.ToInt32(((TextBox)GRow.FindControl("TextBoxQuantity")).Text);
                                amount = Convert.ToSingle(((HtmlInputText)GRow.FindControl("InputAmount")).Value);
                                cta = Convert.ToSingle(((HtmlInputText)GRow.FindControl("InputCTA")).Value);
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
                                row["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                                row["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
                                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                                row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                                row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                                row["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
                                row["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;

                                row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;

                                row["RN"] = Math.Round((num / count), 4).ToString();

                                //支（张）为整数

                                row["Quantity"] = Convert.ToInt32((quantity / count)).ToString();
                                string ordercode = ((Label)GRow.FindControl("LabelOrderCode")).Text;
                                if (ordercode != "")
                                { 
                                    //生成批号
                                        if (((TextBox)GRow.FindControl("TextBoxLotNumber")).Text == "")
                                        {
                                            //生成规则，如果没有输入，则是从数据库中提取最大的批号
                                            string Lot = (ln + j + 1).ToString().PadLeft(3, '0') + "-" + (Convert.ToInt32((LabelCode.Text).Substring(1, 9))).ToString() + "-" + (Convert.ToInt32(ordercode.Substring(ordercode.Length-8,8))).ToString();
                                            row["LotNumber"] = Lot;
                                        }
                                        else
                                        {
                                            string[] lsh = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text.Split('-');
                                            string Lot = (Convert.ToInt32(lsh[0]) + j).ToString().PadLeft(3, '0') + "-" + lsh[1] + "-" + lsh[2];
                                            row["LotNumber"] = Lot;
                                        }
                                }
                                row["UnitPrice"] = ((Label)GRow.FindControl("LabelUnitPrice")).Text;
                                row["TaxRate"] = ((Label)GRow.FindControl("LabelTaxRate")).Text;
                                row["CTUP"] = ((Label)GRow.FindControl("LabelCTUP")).Text;


                                row["Amount"] =Math.Round((amount / count),4).ToString();

                                row["CTA"] = Math.Round((cta / count),4).ToString();


                                row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                                row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                                row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                                row["Position"] = ((TextBox)GRow.FindControl("TextBoxPosition")).Text;
                                row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                                row["OrderCode"] = ordercode;
                               
                                row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                                row["CGMODE"] = ((Label)GRow.FindControl("LabelCGMode")).Text;
                                row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                                tb.Rows.Add(row);
                            }

                            ////////////////////////////////////////////////////////////////////////////////////////////
                            /*
                             * 产生最后一行
                             */
                            DataRow row1 = tb.NewRow();
                            row1["UniqueID"] = ((Label)GRow.FindControl("LabelUniqueID")).Text;
                            row1["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                            row1["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
                            row1["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                            row1["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                            row1["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                            row1["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                            row1["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
                            row1["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;

                            row1["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;

                            row1["RN"] = Math.Round((num - Math.Round((num / count), 4) * (count - 1)), 4).ToString();

                            row1["Quantity"] = Convert.ToInt32((quantity / count)).ToString();
                             string ordercode2=((Label)GRow.FindControl("LabelOrderCode")).Text;
                             if (ordercode2 != "")
                             { 
                                //生成批号
                                if (((TextBox)GRow.FindControl("TextBoxLotNumber")).Text == "")
                                {
                                    string Lot = (ln + count).ToString().PadLeft(3, '0') + "-" + (Convert.ToInt32((LabelCode.Text).Substring(1, 9))).ToString() + "-" + (Convert.ToInt32(ordercode2.Substring(ordercode2.Length - 8, 8))).ToString();
                                    row1["LotNumber"] = Lot;
                                }
                                else
                                {
                                    string[] lsh = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text.Split('-');
                                    string Lot = (Convert.ToInt32(lsh[0]) + count - 1).ToString().PadLeft(3, '0') + "-" + lsh[1] + "-" + lsh[2];
                                    row1["LotNumber"] = Lot;
                                }
                             }
                            row1["UnitPrice"] = ((Label)GRow.FindControl("LabelUnitPrice")).Text;
                            row1["TaxRate"] = ((Label)GRow.FindControl("LabelTaxRate")).Text;
                            row1["CTUP"] = ((Label)GRow.FindControl("LabelCTUP")).Text;


                            //Math.Round((num - Math.Round((num / count), 4) * (count - 1)), 4).ToString();

                            row1["Amount"] = Math.Round((amount-Math.Round((amount / count),4)* (count - 1)),4).ToString();

                            row1["CTA"] = Math.Round((cta - Math.Round((cta / count), 4) * (count - 1)), 4).ToString();

                            row1["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                            row1["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                            row1["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                            row1["Position"] = ((TextBox)GRow.FindControl("TextBoxPosition")).Text;
                            row1["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                            row1["OrderCode"] = ordercode2;
                            row1["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                            row1["CGMODE"] = ((Label)GRow.FindControl("LabelCGMode")).Text;
                            row1["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
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
                                row["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                                row["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
                                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                                row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                                row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                                row["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
                                row["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;

                                row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                                row["RN"] = ((TextBox)GRow.FindControl("TextBoxRN")).Text;
                                row["Quantity"] = ((TextBox)GRow.FindControl("TextBoxQuantity")).Text;
                                 string ordercode=((Label)GRow.FindControl("LabelOrderCode")).Text;
                                 if (ordercode != "")
                                 {
                                    if (((TextBox)GRow.FindControl("TextBoxLotNumber")).Text == "")
                                    {
                                        string Lot = (ln + j + 1).ToString().PadLeft(3, '0') + "-" + (Convert.ToInt32((LabelCode.Text).Substring(1, 9))).ToString() + "-" + (Convert.ToInt32(ordercode.Substring(ordercode.Length - 8, 8))).ToString();
                                        row["LotNumber"] = Lot;
                                    }
                                    else
                                    {
                                        string[] lsh = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text.Split('-');
                                        string Lot = (Convert.ToInt32(lsh[0]) + j).ToString().PadLeft(3, '0') + "-" + lsh[1] + "-" + lsh[2];
                                        row["LotNumber"] = Lot;
                                    }
                                 }
                                row["UnitPrice"] = ((Label)GRow.FindControl("LabelUnitPrice")).Text;
                                row["TaxRate"] = ((Label)GRow.FindControl("LabelTaxRate")).Text;
                                row["CTUP"] = ((Label)GRow.FindControl("LabelCTUP")).Text;
                                row["Amount"] = ((HtmlInputText)GRow.FindControl("InputAmount")).Value;
                                row["CTA"] = ((HtmlInputText)GRow.FindControl("InputCTA")).Value;
                                row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                                row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                                row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                                row["Position"] = ((TextBox)GRow.FindControl("TextBoxPosition")).Text;
                                row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                                row["OrderCode"] = ordercode;
                                row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                                row["CGMODE"] = ((Label)GRow.FindControl("LabelCGMode")).Text;
                                row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                                tb.Rows.Add(row);
                            }
                        }
                    }
                    else
                    {
                        //记录复制模式--插入

                        DataRow row = tb.NewRow();
                        row["UniqueID"] = ((Label)GRow.FindControl("LabelUniqueID")).Text;
                        row["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                        row["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
                        row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                        row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                        row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                        row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                        row["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
                        row["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;
                        row["LotNumber"] = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text;
                        row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                        row["RN"] = ((TextBox)GRow.FindControl("TextBoxRN")).Text;
                        row["Quantity"] = ((TextBox)GRow.FindControl("TextBoxQuantity")).Text;
                        row["UnitPrice"] = ((Label)GRow.FindControl("LabelUnitPrice")).Text;
                        row["TaxRate"] = ((Label)GRow.FindControl("LabelTaxRate")).Text;
                        row["CTUP"] = ((Label)GRow.FindControl("LabelCTUP")).Text;
                        row["Amount"] = ((HtmlInputText)GRow.FindControl("InputAmount")).Value;
                        row["CTA"] = ((HtmlInputText)GRow.FindControl("InputCTA")).Value;
                        row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                        row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                        row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                        row["Position"] = ((TextBox)GRow.FindControl("TextBoxPosition")).Text;
                        row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                        row["OrderCode"] = ((Label)GRow.FindControl("LabelOrderCode")).Text;
                        row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                        row["CGMODE"] = ((Label)GRow.FindControl("LabelCGMode")).Text;
                        row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                        tb.Rows.Add(row);
                    }
                }
                #endregion

            }

            //记录追加模式
           
            if (mode2 == "1")
            {
                #region

                tb = getDataFromGridView();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow GRow = GridView1.Rows[i];
                    if (((CheckBox)GRow.FindControl("CheckBox1")).Checked == true)
                    {
                        //获取当前物料已有批号
                        string mcode = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                        string wcode = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                        string pcode = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                        string sql = "SELECT SUBSTRING(SQ_LOTNUM,0,4) AS LN  FROM TBWS_STORAGE WHERE SQ_MARID='" + mcode + "' AND SQ_WAREHOUSE='" + wcode + "' AND SQ_LOCATION='" + pcode + "'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        Int32 ln = 0;
                        if (dt.Rows.Count != 0)
                        {
                            try { ln = Convert.ToInt32(dt.Compute("Max(LN)", "")); }
                            catch { ln = 0; }
                        }

                        //数据均分模式
                        if (mode == "0")
                        {
                            float num = 0;
                            Int32 quantity = 0;
                            float amount = 0;
                            float cta = 0;
                            try
                            {
                                num = Convert.ToSingle(((TextBox)GRow.FindControl("TextBoxRN")).Text);
                                quantity = Convert.ToInt32(((TextBox)GRow.FindControl("TextBoxQuantity")).Text);
                                amount = Convert.ToSingle(((HtmlInputText)GRow.FindControl("InputAmount")).Value);
                                cta = Convert.ToSingle(((HtmlInputText)GRow.FindControl("InputCTA")).Value);
                            }
                            catch
                            {
                                LabelMessage.Text = "请正确填写实收数量！";
                                return;
                            }

                            for (int j = 0; j < count - 2; j++)
                            {
                                DataRow row = tb.NewRow();
                                row["UniqueID"] = ((Label)GRow.FindControl("LabelUniqueID")).Text;
                                row["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                                row["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
                                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                                row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                                row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                                row["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
                                row["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;

                                row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                                row["RN"] = Math.Round((num / count), 4).ToString();

                                tb.Rows[i]["RN"] = Math.Round((num / count), 4).ToString();//改变原来行的数量

                                row["Quantity"] = Convert.ToInt32((quantity / count)).ToString();

                                tb.Rows[i]["Quantity"] = Convert.ToInt32((quantity / count)).ToString();//改变原来的行的辅助数量
                                string ordercode = ((Label)GRow.FindControl("LabelOrderCode")).Text;
                                if (ordercode != "")
                                { 
                                    if (((TextBox)GRow.FindControl("TextBoxLotNumber")).Text == "")
                                    {
                                        //找出原来行的批号====获取数据库中的最大批号

                                        string Lot = (ln + j + 2).ToString().PadLeft(3, '0') + "-" + (Convert.ToInt32((LabelCode.Text).Substring(1, 9))).ToString() + "-" + (Convert.ToInt32(ordercode.Substring(ordercode.Length - 8, 8))).ToString();
                                        row["LotNumber"] = Lot;//新的批号
                                        tb.Rows[i]["LotNumber"] = (ln + 1).ToString().PadLeft(3, '0') + "-" + (Convert.ToInt32((LabelCode.Text).Substring(1, 9))).ToString() + "-" + (Convert.ToInt32(ordercode.Substring(ordercode.Length - 8, 8))).ToString();//原来的批号
                                    }
                                    else
                                    {
                                        //获取Gridview中的行号
                                        string[] lsh = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text.Split('-');

                                        string Lot = (Convert.ToInt32(lsh[0]) + j + 1).ToString().PadLeft(3, '0') + "-" + lsh[1] + "-" + lsh[2];
                                        row["LotNumber"] = Lot;
                                        tb.Rows[i]["LotNumber"] = (Convert.ToInt32(lsh[0])).ToString().PadLeft(3, '0') + "-" + lsh[1] + "-" + lsh[2];
                                    }
                                }

                                row["UnitPrice"] = ((Label)GRow.FindControl("LabelUnitPrice")).Text;
                                row["TaxRate"] = ((Label)GRow.FindControl("LabelTaxRate")).Text;
                                row["CTUP"] = ((Label)GRow.FindControl("LabelCTUP")).Text;

                                row["Amount"] = Math.Round((amount / count), 4).ToString();//金额
                                tb.Rows[i]["Amount"] = Math.Round((amount / count), 4).ToString();//原来行的金额

                                row["CTA"] = Math.Round((cta / count), 4).ToString();//含税金额
                                tb.Rows[i]["CTA"] = Math.Round((cta / count), 4).ToString();//原来行的含税金额

                                row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                                row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                                row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                                row["Position"] = ((TextBox)GRow.FindControl("TextBoxPosition")).Text;
                                row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                                row["OrderCode"] = ordercode;
                                row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                                row["CGMODE"] = ((Label)GRow.FindControl("LabelCGMode")).Text;
                                row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                                tb.Rows.Add(row);
                            }
                            /*
                             * 产生最后一行
                             */

                            DataRow row1 = tb.NewRow();
                            row1["UniqueID"] = ((Label)GRow.FindControl("LabelUniqueID")).Text;
                            row1["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                            row1["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
                            row1["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                            row1["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                            row1["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                            row1["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                            row1["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
                            row1["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;

                            row1["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;

                            row1["RN"] = Math.Round((num - Math.Round((num / count), 4) * (count - 1)), 4).ToString();

                            row1["Quantity"] = Convert.ToInt32((quantity / count)).ToString();
                            string ordercode1 = ((Label)GRow.FindControl("LabelOrderCode")).Text;
                            if (ordercode1 != "")
                            { 
                                    //生成批号
                                    if (((TextBox)GRow.FindControl("TextBoxLotNumber")).Text == "")
                                    {

                                        string Lot = (ln + count).ToString().PadLeft(3, '0') + "-" + (Convert.ToInt32((LabelCode.Text).Substring(1, 9))).ToString() + "-" + (Convert.ToInt32(ordercode1.Substring(ordercode1.Length - 8, 8))).ToString();
                                        row1["LotNumber"] = Lot;//新的批号

                                    }
                                    else
                                    {
                                        string[] lsh = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text.Split('-');
                                        string Lot = (Convert.ToInt32(lsh[0]) + count - 1).ToString().PadLeft(3, '0') + "-" + lsh[1] + "-" + lsh[2];
                                        row1["LotNumber"] = Lot;
                                    }
                            }
                            row1["UnitPrice"] = ((Label)GRow.FindControl("LabelUnitPrice")).Text;
                            row1["TaxRate"] = ((Label)GRow.FindControl("LabelTaxRate")).Text;
                            row1["CTUP"] = ((Label)GRow.FindControl("LabelCTUP")).Text;

                            row1["Amount"] = Math.Round((amount - Math.Round((amount / count), 4) * (count - 1)), 4).ToString();

                            row1["CTA"] = Math.Round((cta - Math.Round((cta / count), 4) * (count - 1)), 4).ToString();

                            row1["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                            row1["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                            row1["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                            row1["Position"] = ((TextBox)GRow.FindControl("TextBoxPosition")).Text;
                            row1["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                            row1["OrderCode"] = ordercode1;
                            row1["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                            row1["CGMODE"] = ((Label)GRow.FindControl("LabelCGMode")).Text;

                            row1["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                            tb.Rows.Add(row1);

                            //==============================================    * 产生最后一行

                        }
                        //数据复制模式
                        if (mode == "1")
                        {
                            for (int j = 0; j < count - 1; j++)
                            {
                                DataRow row = tb.NewRow();
                                row["UniqueID"] = ((Label)GRow.FindControl("LabelUniqueID")).Text;
                                row["MaterialCode"] = ((Label)GRow.FindControl("LabelMaterialCode")).Text;
                                row["MaterialName"] = ((Label)GRow.FindControl("LabelMaterialName")).Text;
                                row["MaterialStandard"] = ((Label)GRow.FindControl("LabelMaterialStandard")).Text;
                                row["Fixed"] = ((Label)GRow.FindControl("LabelFixed")).Text;
                                row["Length"] = ((TextBox)GRow.FindControl("TextBoxLength")).Text;
                                row["Width"] = ((TextBox)GRow.FindControl("TextBoxWidth")).Text;
                                row["GB"] = ((Label)GRow.FindControl("LabelGB")).Text;
                                row["Attribute"] = ((Label)GRow.FindControl("LabelAttribute")).Text;
                                string ordercode = ((Label)GRow.FindControl("LabelOrderCode")).Text;
                                if (ordercode != "")
                                { 
                                    if (((TextBox)GRow.FindControl("TextBoxLotNumber")).Text == "")
                                    {
                                        string Lot = (ln + j + 2).ToString().PadLeft(3, '0') + "-" + (Convert.ToInt32((LabelCode.Text).Substring(1, 9))).ToString() + "-" + (Convert.ToInt32(ordercode.Substring(ordercode.Length - 8, 8))).ToString();
                                        row["LotNumber"] = Lot;
                                        tb.Rows[i]["LotNumber"] = (ln + 1).ToString().PadLeft(3, '0') + "-" + (Convert.ToInt32((LabelCode.Text).Substring(1, 9))).ToString() + "-" + (Convert.ToInt32(ordercode.Substring(ordercode.Length - 8, 8))).ToString();
                                    }
                                    else
                                    {
                                        string[] lsh = ((TextBox)GRow.FindControl("TextBoxLotNumber")).Text.Split('-');
                                        string Lot = (Convert.ToInt32(lsh[0]) + j + 1).ToString().PadLeft(3, '0') + "-" + lsh[1] + "-" + lsh[2];
                                        row["LotNumber"] = Lot;
                                        tb.Rows[i]["LotNumber"] = (Convert.ToInt32(lsh[0])).ToString().PadLeft(3, '0') + "-" + lsh[1] + "-" + lsh[2];
                                    }
                                }
                                row["OrderCode"] = ordercode;
                                row["Unit"] = ((Label)GRow.FindControl("LabelUnit")).Text;
                                row["RN"] = ((TextBox)GRow.FindControl("TextBoxRN")).Text;
                                row["Quantity"] = ((TextBox)GRow.FindControl("TextBoxQuantity")).Text;
                                row["UnitPrice"] = ((Label)GRow.FindControl("LabelUnitPrice")).Text;
                                row["TaxRate"] = ((Label)GRow.FindControl("LabelTaxRate")).Text;
                                row["CTUP"] = ((Label)GRow.FindControl("LabelCTUP")).Text;
                                row["Amount"] = ((HtmlInputText)GRow.FindControl("InputAmount")).Value;
                                row["CTA"] = ((HtmlInputText)GRow.FindControl("InputCTA")).Value;
                                row["Comment"] = ((TextBox)GRow.FindControl("TextBoxComment")).Text;
                                row["Warehouse"] = ((Label)GRow.FindControl("LabelWarehouse")).Text;
                                row["WarehouseCode"] = ((Label)GRow.FindControl("LabelWarehouseCode")).Text;
                                row["Position"] = ((TextBox)GRow.FindControl("TextBoxPosition")).Text;
                                row["PositionCode"] = ((HtmlInputText)GRow.FindControl("InputPositionCode")).Value;
                                
                                row["PlanMode"] = ((Label)GRow.FindControl("LabelPlanMode")).Text;
                                row["CGMODE"] = ((Label)GRow.FindControl("LabelCGMode")).Text;

                                row["PTC"] = ((Label)GRow.FindControl("LabelPTC")).Text;
                                tb.Rows.Add(row);
                            }
                        }
                    }
                }

                #endregion
            }

            GridView1.DataSource = tb;
            GridView1.DataBind();

           
            string NowCode = string.Empty;
           

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                //if ((GridView1.Rows[i].FindControl("CheckBox1") as CheckBox).Checked)
                //{
                    Label lbCode = (GridView1.Rows[i].FindControl("LabelPTC") as Label);

                    string Code = lbCode.Text;

                    if (Code != NowCode)
                    {
                        NowCode = lbCode.Text;


                        int m = 0;
                        string lotnum = (GridView1.Rows[i].FindControl("TextBoxLotNumber") as TextBox).Text.Trim();

                        string ordercode = (GridView1.Rows[i].FindControl("LabelOrderCode") as Label).Text.Trim();
                        if (ordercode != "")
                        { 
                                if (lotnum == string.Empty)
                                {
                                    (GridView1.Rows[i].FindControl("TextBoxLotNumber") as TextBox).Text = "001-" + (Convert.ToInt32((LabelCode.Text).Substring(1, 9))).ToString() + "-" + (Convert.ToInt32(ordercode.Substring(ordercode.Length - 8, 8))).ToString();
                                }
                                else
                                {
                                    Int32 intid = Convert.ToInt32(lotnum.Split('-')[0]);

                                    for (int j = i + 1; j < GridView1.Rows.Count; j++)
                                    {
                                        string NextCode = (GridView1.Rows[j].FindControl("LabelPTC") as Label).Text;

                                        
                                        if (Code == NextCode)
                                        {
                                            m = m + 1;
                                            string nextodercode = (GridView1.Rows[j].FindControl("LabelOrderCode") as Label).Text;
                                            string newid = (intid + m).ToString().PadLeft(3, '0');

                                            (GridView1.Rows[j].FindControl("TextBoxLotNumber") as TextBox).Text = newid + "-" + (Convert.ToInt32((LabelCode.Text).Substring(1, 9))).ToString() + "-" + (Convert.ToInt32(nextodercode.Substring(ordercode.Length - 8, 8))).ToString();

                                        }
                                       
                                       
                                        //else
                                        //{

                                        //    break;

                                        //}
                                    }
                                }
                            
                        }
                    }
            }

            LabelMessage.Text = "拆分成功！";
        }

        //取消拆分

        protected void ButtonSplitCancel_Click(object sender, EventArgs e)
        {
            RadioButtonListSplitMode.Items[0].Selected = true;
            RadioButtonListSplitMode2.Items[0].Selected = true;
            TextBoxSplitLineNum.Text = "2";
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            


                string Code = LabelCode.Text;

                string sqlstate = "select WG_STATE from TBWS_IN where WG_CODE='" + Code + "'";

                if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows.Count > 0)
                {
                    if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows[0]["WG_STATE"].ToString() == "2")
                    {

                        string script = @"alert('单据已审核，不能删除！');";
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
                LabelMessage.Text = "删除成功！";
            
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


        //保存当前入库单
        protected void Save_Click(object sender, EventArgs e)
        {
            //2016修改
            string errorjhsl = "";
            foreach (GridViewRow gr in GridView1.Rows)
            {
                string checkptc = (gr.FindControl("LabelPTC") as System.Web.UI.WebControls.Label).Text.Trim();
                string checksql = "select * from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where ptcode='" + checkptc + "' ";
                DataTable checkdt = DBCallCommon.GetDTUsingSqlText(checksql);
                if (checkdt.Rows.Count > 0)
                {
                    double plannum = CommonFun.ComTryDouble(checkdt.Rows[0]["zxnum"].ToString().Trim());
                    double realnum = CommonFun.ComTryDouble((gr.FindControl("TextBoxRN") as System.Web.UI.WebControls.TextBox).Text.Trim());
                    if (realnum > 1.05 * plannum)
                    {
                        errorjhsl = "计划跟踪号为：" + checkptc + ",实收数量超出计划数量5%，请检该条数据是否由几个计划跟踪号合并后入库，如果是，请分别入库！";

                    }
                }
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
                if (DropDownListWarehouse.SelectedValue == "0" || DropDownListPosition.SelectedValue == "0")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('请选择仓库和仓位!')</script>");
                    return;
                }


                string Code = LabelCode.Text;

                string sqlstate = "select WG_STATE from TBWS_IN where WG_CODE='" + Code + "'";

                if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows.Count > 0)
                {
                    if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows[0]["WG_STATE"].ToString() == "2")
                    {

                        string script = @"alert('单据已审核，不能保存！');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                        return;
                    }
                }

                if (GridView1.Rows.Count == 0)
                {
                    string script = @"alert('入库条目数为0，单据不能保存！');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    return;
                }

                bool HasError = false;
                int ErrorType = 0;

                /*
                 * 保存的时候是修改库存的数量的
                 * 也不汇总库存的数量
                 */

                //此处是保存操作
                List<string> sqllist = new List<string>();
                string sql = "";


                string Date = TextBoxDate.Text;//制单年月日
                string Supplier = LabelSupplier.Text;
                string SupplierCode = InputSupplierCode.Value;
                string Warehouse = DropDownListWarehouse.SelectedItem.Text;
                //string WarehouseCode = DropDownListWarehouse.SelectedValue;
                string Abstract = TextBoxAbstract.Text;
                string HDBH = TextBoxHDBH.Text;
                string Dep = LabelDep.Text;
                string DepCode = LabelDepCode.Text;
                string Clerk = LabelClerk.Text;
                string ClerkCode = LabelClerkCode.Text;
                string Document = LabelDoc.Text;
                string DocumentCode = LabelDocCode.Text;
                string Acceptance = DropDownListAcceptance.SelectedItem.Text;
                string AcceptanceCode = DropDownListAcceptance.SelectedValue;
                string Colour = InputColour.Value;

              


                sql = "select count(*) from TBWS_IN where WG_CODE='" + Code + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                if (dt.Rows[0][0].ToString() == "0")
                {
                    sql = "exec ResetSeed @tablename=TBWS_IN";

                    sqllist.Add(sql);

                    if (Code.Contains('B'))
                    {
                        
                        sql = "INSERT INTO TBWS_IN ( WG_CODE,WG_ABSTRACT,WG_DATE," +
                        "WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG," +
                        "WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_ZGFLAG,WG_HSFLAG,WG_ACTPERIOD,WG_BILLTYPE,WG_HDBH) VALUES('" + Code + "','" + Abstract + "','" + Date + "','" +
                        DocumentCode + "','','','','1','0','0','0','0','0','0','','1','" + HDBH + "')";
                    }
                    else if (Code.Contains('X'))
                    {
                        
                        sql = "INSERT INTO TBWS_IN(WG_CODE,WG_ABSTRACT,WG_DATE," +
                        "WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG," +
                        "WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_ZGFLAG,WG_HSFLAG,WG_ACTPERIOD,WG_BILLTYPE) VALUES('" + Code + "','" + Abstract + "','" + Date + "','" +
                        DocumentCode + "','','','','1','0','0','0','0','0','0','','4')";
                    }
                    else if (Code.Contains('T'))
                    {

                        sql = "INSERT INTO TBWS_IN(WG_CODE,WG_ABSTRACT,WG_DATE," +
                        "WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG," +
                        "WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_ZGFLAG,WG_HSFLAG,WG_ACTPERIOD,WG_BILLTYPE) VALUES('" + Code + "','" + Abstract + "','" + Date + "','" +
                        DocumentCode + "','','','','1','0','0','0','0','0','0','','3')";
                    }
                    else
                    {
                        
                        sql = "INSERT INTO TBWS_IN ( WG_CODE," +
                        "WG_ABSTRACT,WG_DATE," +
                        "WG_DOC," +
                        "WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG,WG_CAVFLAG," +
                        "WG_ROB,WG_GJSTATE,WG_HDBH,WG_COMPANY,WG_COMPANYCODE) VALUES('" + Code + "','" + Abstract + "','" + Date + "','" +
                         DocumentCode + "','" + AcceptanceCode + "','','','1','0','0','" +
                        Colour + "','0','" + HDBH + "','" + Supplier + "','" + SupplierCode + "')";
                    }

                    sqllist.Add(sql);

                }
                else
                {
                    if (Code.Contains('B'))
                    {
                        ////结转备库
                        //sql = "INSERT INTO TBWS_IN ( WG_CODE,WG_WAREHOUSE,WG_ABSTRACT,WG_DATE," +
                        //"WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG," +
                        //"WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_ZGFLAG,WG_HSFLAG,WG_ACTPERIOD,WG_BILLTYPE,WG_HDBH) VALUES
                        //('" + Code + "','" + WarehouseCode + "','" + Abstract + "','" + Date + "','" +
                        //DocumentCode + "','','','','1','0','0','0','0','0','0','','1','" + HDBH + "')";
                        sql = "update TBWS_IN set WG_ABSTRACT='" + Abstract + "',WG_DATE='" + Date + "',";
                        sql += "WG_DOC='" + DocumentCode + "',WG_REVEICER='" + AcceptanceCode + "',WG_VERIFIER='',WG_VERIFYDATE='',WG_STATE='1',WG_TEARFLAG='0',";
                        sql += "WG_CAVFLAG='0',WG_ROB='0',WG_GJSTATE='0',WG_ZGFLAG='0',WG_HSFLAG='0',WG_ACTPERIOD='',WG_BILLTYPE='1',WG_HDBH='" + HDBH + "'  where WG_CODE='" + Code + "'";




                    }
                    else if (Code.Contains('X'))
                    {
                        //外协入库
                        //sql = "INSERT INTO TBWS_IN(WG_CODE,WG_WAREHOUSE,WG_ABSTRACT,WG_DATE," +
                        //"WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG," +
                        //"WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_ZGFLAG,WG_HSFLAG,WG_ACTPERIOD,WG_BILLTYPE,WG_COMPANY) VALUES('" + Code + "','" + WarehouseCode + "','" + Abstract + "','" + Date + "','" +
                        //DocumentCode + "','','','','1','0','0','0','0','0','0','','3','" + Supplier + "')";

                        sql = "update TBWS_IN set WG_ABSTRACT='" + Abstract + "',WG_DATE='" + Date + "',";
                        sql += "WG_DOC='" + DocumentCode + "',WG_REVEICER='" + AcceptanceCode + "',WG_VERIFIER='',WG_VERIFYDATE='',WG_STATE='1',WG_TEARFLAG='0',";
                        sql += "WG_CAVFLAG='0',WG_ROB='0',WG_GJSTATE='0',WG_ZGFLAG='0',WG_HSFLAG='0',WG_ACTPERIOD='',WG_BILLTYPE='4' where WG_CODE='" + Code + "'";



                    }
                    else if (Code.Contains('T'))
                    {
                        //其他入库
                        //sql = "INSERT INTO TBWS_IN(WG_CODE,WG_WAREHOUSE,WG_ABSTRACT,WG_DATE," +
                        //"WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG," +
                        //"WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_ZGFLAG,WG_HSFLAG,WG_ACTPERIOD,WG_BILLTYPE,WG_COMPANY) VALUES('" + Code + "','" + WarehouseCode + "','" + Abstract + "','" + Date + "','" +
                        //DocumentCode + "','','','','1','0','0','0','0','0','0','','3','" + Supplier + "')";

                        sql = "update TBWS_IN set WG_ABSTRACT='" + Abstract + "',WG_DATE='" + Date + "',";
                        sql += "WG_DOC='" + DocumentCode + "',WG_REVEICER='" + AcceptanceCode + "',WG_VERIFIER='',WG_VERIFYDATE='',WG_STATE='1',WG_TEARFLAG='0',";
                        sql += "WG_CAVFLAG='0',WG_ROB='0',WG_GJSTATE='0',WG_ZGFLAG='0',WG_HSFLAG='0',WG_ACTPERIOD='',WG_BILLTYPE='3' where WG_CODE='" + Code + "'";



                    }
                    else
                    {
                        //采购入库

                        //sql = "INSERT INTO TBWS_IN ( WG_CODE,WG_WAREHOUSE," +
                        //"WG_ABSTRACT,WG_DATE," +
                        //"WG_DOC," +
                        //"WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG,WG_CAVFLAG," +
                        //"WG_ROB,WG_GJSTATE,WG_HDBH) VALUES('" + Code + "','" + WarehouseCode + "','" + Abstract + "','" + Date + "','" +
                        // DocumentCode + "','" + AcceptanceCode + "','','','1','0','0','" +
                        //Colour + "','0','" + HDBH + "')";

                        sql = "update TBWS_IN set WG_ABSTRACT='" + Abstract + "',WG_DATE='" + Date + "',";
                        sql += "WG_DOC='" + DocumentCode + "',WG_REVEICER='" + AcceptanceCode + "',WG_VERIFIER='',WG_VERIFYDATE='',WG_STATE='1',WG_TEARFLAG='0',";
                        sql += "WG_CAVFLAG='0',WG_ROB='0',WG_GJSTATE='0',WG_ZGFLAG='0',WG_HSFLAG='0',WG_ACTPERIOD='',WG_BILLTYPE='0',WG_COMPANY='" + Supplier + "',WG_COMPANYCODE='" + SupplierCode + "',WG_HDBH='" + HDBH + "' where WG_CODE='" + Code + "'";

                    }

                    sqllist.Add(sql);
                }

                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                string sqlUpdateQR3 = "update midTable_QRIn set QRIn_State='0' where CONVERT(varchar(20),QRIn_ID) in(select WG_QRUniqCode from TBWS_INDETAIL where WG_CODE='" + Code + "')";
                sqllist.Add(sqlUpdateQR3);
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


                sql = "DELETE FROM TBWS_INDETAIL WHERE WG_CODE='" + Code + "'";
                sqllist.Add(sql);
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    string UniqueID = "";
                    if (Colour == "0")
                    {
                        UniqueID = Code + (i + 1).ToString().PadLeft(3, '0');
                    }
                    else
                    {
                        UniqueID = ((Label)this.GridView1.Rows[i].FindControl("LabelUniqueID")).Text;
                    }
                    string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text;
                    string MaterialName = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialName")).Text;
                    string MaterialStandard = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialStandard")).Text;
                    string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;
                    float Length = 0;
                    try { Length = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLength")).Text); }
                    catch { Length = 0; }
                    float Width = 0;
                    try { Width = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxWidth")).Text); }
                    catch { Width = 0; }
                    string Attribute = ((Label)this.GridView1.Rows[i].FindControl("LabelAttribute")).Text;
                    string GB = ((Label)this.GridView1.Rows[i].FindControl("LabelGB")).Text;
                    string LotNumber = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLotNumber")).Text.Trim();
                    string Unit = ((Label)this.GridView1.Rows[i].FindControl("LabelUnit")).Text;
                    float RN = 0;
                    try
                    {
                        //蓝红的数量
                        float temp = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxRN")).Text.Trim());
                        if ((Colour == "0") && (temp < 0))
                        { RN = -temp; }
                        else if ((Colour == "1") && (temp > 0))
                        { RN = -temp; }
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

                    float Quantity = 0;
                    try
                    {
                        //蓝红总重
                        float temp = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxQuantity")).Text.Trim());
                        if ((Colour == "0") && (temp < 0))
                        { Quantity = -temp; }
                        else if ((Colour == "1") && (temp > 0))
                        { Quantity = -temp; }
                        else
                        { Quantity = temp; }
                    }
                    catch { Quantity = 0; }
                    float UnitPrice = 0;
                    try { UnitPrice = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelUnitPrice")).Text); }
                    catch { UnitPrice = 0; }
                    float TaxRate = 0;
                    try { TaxRate = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelTaxRate")).Text); }
                    catch { TaxRate = 0; }
                    float CTUP = 0;
                    try { CTUP = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelCTUP")).Text); }
                    catch { CTUP = 0; }
                    float Amount = 0;
                    try
                    {
                        float temp = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputAmount")).Value);
                        if ((Colour == "0") && (temp < 0))
                        { Amount = -temp; }
                        else if ((Colour == "1") && (temp > 0))
                        { Amount = -temp; }
                        else
                        { Amount = temp; }
                    }
                    catch { Amount = 0; }
                    float CTA = 0;
                    try
                    {
                        float temp = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputCTA")).Value);
                        if ((Colour == "0") && (temp < 0))
                        { CTA = -temp; }
                        else if ((Colour == "1") && (temp > 0))
                        { CTA = -temp; }
                        else
                        { CTA = temp; }
                    }
                    catch { CTA = 0; }

                    string Comment = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;
                    string WarehouseIn = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouse")).Text;
                    string WarehouseCodeIn = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;
                    string Position = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxPosition")).Text;

                    string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;
                    string WarehouseCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;
                    
                    if (PositionCode == string.Empty || PositionCode == "0")
                    {
                        HasError = true;
                        ErrorType = 2;
                        break;
                    }

                    string OrderCode = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderCode")).Text;
                    string PlanMode = ((Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text;
                    string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;
                    string CGMode = ((Label)this.GridView1.Rows[i].FindControl("LabelCGMode")).Text;


                    /*
                     * 
                     * WG_RSNUM---实收数量
                     * 
                     * WG_RSFZNUM---实收辅助数量
                     * 
                     */
                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    string WG_QRUniqCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWG_QRUniqCode")).Text;
                    string sqlUpdateQR2 = "";
                    //sql ="INSERT INTO TBWS_INDETAIL ( WG_UNIQUEID,WG_CODE,WG_MARID,WG_LOTNUM," +
                    //    "WG_FIXEDSIZE,WG_LENGTH,WG_WIDTH,WG_RSNUM,WG_RSFZNUM,WG_UPRICE,WG_TAXRATE," +
                    //    "WG_CTAXUPRICE,WG_AMOUNT,WG_CTAMTMNY,WG_LOCATION," +
                    //    "WG_ORDERID,WG_PMODE,WG_PTCODE,WG_NOTE,WG_STATE,WG_CGMODE,WG_WAREHOUSE) VALUES('" + UniqueID +
                    //    "','" + Code + "','" + MaterialCode + "','" + LotNumber + "','"
                    //    + Fixed + "','" + Length + "','" + Width + "','" + RN + "','" + Quantity + "','" + UnitPrice + "','" +
                    //    TaxRate + "','" + CTUP + "','" + Amount + "','" + CTA + "','" +
                    //    PositionCode + "','" + OrderCode + "','" +
                    //    PlanMode + "','" + PTC + "','" + Comment + "','','" + CGMode + "','" + WarehouseCode + "')";
                    if (WG_QRUniqCode != "")
                    {
                        sql = "INSERT INTO TBWS_INDETAIL ( WG_UNIQUEID,WG_CODE,WG_MARID,WG_LOTNUM," +
                                               "WG_FIXEDSIZE,WG_LENGTH,WG_WIDTH,WG_RSNUM,WG_RSFZNUM,WG_UPRICE,WG_TAXRATE," +
                                               "WG_CTAXUPRICE,WG_AMOUNT,WG_CTAMTMNY,WG_LOCATION," +
                                               "WG_ORDERID,WG_PMODE,WG_PTCODE,WG_NOTE,WG_STATE,WG_CGMODE,WG_WAREHOUSE,WG_QRUniqCode) VALUES('" + UniqueID +
                                               "','" + Code + "','" + MaterialCode + "','" + LotNumber + "','"
                                               + Fixed + "','" + Length + "','" + Width + "','" + RN + "','" + Quantity + "','" + UnitPrice + "','" +
                                               TaxRate + "','" + CTUP + "','" + Amount + "','" + CTA + "','" +
                                               PositionCode + "','" + OrderCode + "','" +
                                               PlanMode + "','" + PTC + "','" + Comment + "','','" + CGMode + "','" + WarehouseCode + "','" + WG_QRUniqCode + "')";
                        sqlUpdateQR2 = "update midTable_QRIn set QRIn_State='1' where CONVERT(varchar(20),QRIn_ID)='" + WG_QRUniqCode + "'";
                        sqllist.Add(sqlUpdateQR2);
                    }
                    else
                    {
                        sql = "INSERT INTO TBWS_INDETAIL ( WG_UNIQUEID,WG_CODE,WG_MARID,WG_LOTNUM," +
                            "WG_FIXEDSIZE,WG_LENGTH,WG_WIDTH,WG_RSNUM,WG_RSFZNUM,WG_UPRICE,WG_TAXRATE," +
                            "WG_CTAXUPRICE,WG_AMOUNT,WG_CTAMTMNY,WG_LOCATION," +
                            "WG_ORDERID,WG_PMODE,WG_PTCODE,WG_NOTE,WG_STATE,WG_CGMODE,WG_WAREHOUSE,WG_QRUniqCode) VALUES('" + UniqueID +
                            "','" + Code + "','" + MaterialCode + "','" + LotNumber + "','"
                            + Fixed + "','" + Length + "','" + Width + "','" + RN + "','" + Quantity + "','" + UnitPrice + "','" +
                            TaxRate + "','" + CTUP + "','" + Amount + "','" + CTA + "','" +
                            PositionCode + "','" + OrderCode + "','" +
                            PlanMode + "','" + PTC + "','" + Comment + "','','" + CGMode + "','" + WarehouseCode + "',NULL)";
                    }
                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    sqllist.Add(sql);

                }
                if (HasError)
                {
                    sqllist.Clear();

                    if (ErrorType == 1)
                    {
                        LabelMessage.Text = "入库条目为0，单据不能保存！";

                        string script = @"alert('入库条目为0，单据不能保存！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                    else
                    {
                        

                        LabelMessage.Text = "入库条目仓位为空，单据不能保存！";

                        string script = @"alert('入库条目仓位为空，单据不能保存！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                }
                else
                {
                    DBCallCommon.ExecuteTrans(sqllist);
                    sqllist.Clear();
                    LabelState.Text = "1";
                    LabelMessage.Text = "保存成功";
                    if (!string.IsNullOrEmpty(errorjhsl))
                    {
                        errorddsl.Text = errorjhsl;
                        lberrrorbottom.Text = errorjhsl;
                    }
                }
            }
        }     
        //此处调用了入库库存调整存储过程，需要严格测试
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
                if (DropDownListWarehouse.SelectedValue == "0" || DropDownListPosition.SelectedValue == "0")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('请选择仓库和仓位!')</script>");
                    return;
                }



                string Code = LabelCode.Text;

                string sqlstate = "select WG_STATE from TBWS_IN where WG_CODE='" + Code + "'";

                if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows.Count > 0)
                {
                    if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows[0]["WG_STATE"].ToString() == "2")
                    {

                        string script = @"alert('单据已审核，不能再审核！');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                        return;
                    }
                }

                if (GridView1.Rows.Count == 0)
                {
                    string script = @"alert('入库条目数为0，单据不能审核！');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    return;
                }


                bool HasError = false;
                int ErrorType = 0;


                //此处是审核前保存操作
                List<string> sqllist = new List<string>();
                string sql = "";


                string Date = TextBoxDate.Text;
                string Supplier = LabelSupplier.Text;
                string SupplierCode = InputSupplierCode.Value;
                string Warehouse = DropDownListWarehouse.SelectedItem.Text;
                //string WarehouseCode = DropDownListWarehouse.SelectedValue;
                string Abstract = TextBoxAbstract.Text;
                string HDBH = TextBoxHDBH.Text;
                string Dep = LabelDep.Text;
                string DepCode = LabelDepCode.Text;
                string Clerk = LabelClerk.Text;
                string ClerkCode = LabelClerkCode.Text;
                string Document = LabelDoc.Text;
                string DocumentCode = LabelDocCode.Text;

                string Acceptance = DropDownListAcceptance.SelectedItem.Text;
                string AcceptanceCode = DropDownListAcceptance.SelectedValue;
             

                string Colour = InputColour.Value;

                string Verifier = Session["UserName"].ToString();
                string VerifierCode = Session["UserID"].ToString();

                string ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                //获取审核时的关账时间，看其是否核算

                ClosingAccountDate(DateTime.Now.ToString("yyyy-MM-dd"));//获取系统封账时间

                if (CheckBoxDate.Checked)
                {
                    ApproveDate = getNextMonth() + " 07:59:59";
                    Date = getNextMonth();
                }
                else
                {
                    if (LabelClosingAccount.Text == "NoTime")
                    {
                        //无核算
                        ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        //有核算
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

                            //得看入库单是否审核过

                            //得看上次审核时间，是不是本月的

                            if (LabelApproveDate.Text.Trim().Length > 8)
                            {
                                //多次审核

                                if (Convert.ToInt32(LabelApproveDate.Text.Trim().Substring(0, 4)) == System.DateTime.Now.Year && Convert.ToInt32(LabelApproveDate.Text.Trim().Substring(5, 2)) == System.DateTime.Now.Month)
                                {
                                    //是本月时间审核的，即为当前时间
                                    if (CheckBoxDate.Checked)
                                    {
                                        //如果需要直接结转，才需要结转到下期
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
                                    //否则不是本月，则是下月
                                    //审核通过时，为系统核算过，所以这一次的审核时间还是下月的第一天
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
                }


                sql = "select count(*) from TBWS_IN where WG_CODE='" + Code + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                if (dt.Rows[0][0].ToString() == "0")
                {
                    sql = "exec ResetSeed @tablename=TBWS_IN";

                    sqllist.Add(sql);

                    if (Code.Contains('B'))
                    {
                        //结转备库
                        sql = "INSERT INTO TBWS_IN(WG_CODE,WG_ABSTRACT,WG_DATE," +
                        "WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG," +
                        "WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_ZGFLAG,WG_HSFLAG,WG_ACTPERIOD,WG_BILLTYPE,WG_HDBH,WG_RealTime) VALUES ('" + Code + "','" + Abstract + "','" + Date + "','" +
                        DocumentCode + "','" + AcceptanceCode + "','" + VerifierCode + "','" + ApproveDate + "','1','0','0','" + Colour + "','0','0','0','','1','" + HDBH + "',convert(varchar(50),getdate(),120))";

                    }
                    else if (Code.Contains('X'))
                    {
                        //外协入库
                        sql = "INSERT INTO TBWS_IN (WG_CODE,WG_ABSTRACT,WG_DATE," +
                       "WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG," +
                       "WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_ZGFLAG,WG_HSFLAG,WG_ACTPERIOD,WG_BILLTYPE,WG_RealTime) VALUES ('" + Code + "','" + Abstract + "','" + Date + "','" +
                       DocumentCode + "','" + AcceptanceCode + "','" + VerifierCode + "','" + ApproveDate + "','1','0','0','" + Colour + "','0','0','0','','4',convert(varchar(50),getdate(),120))";

                    }
                    else if (Code.Contains('T'))
                    {
                        //  其他入库
                        sql = "INSERT INTO TBWS_IN (WG_CODE,WG_ABSTRACT,WG_DATE," +
                       "WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG," +
                       "WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_ZGFLAG,WG_HSFLAG,WG_ACTPERIOD,WG_BILLTYPE,WG_RealTime) VALUES ('" + Code + "','" + Abstract + "','" + Date + "','" +
                       DocumentCode + "','" + AcceptanceCode + "','" + VerifierCode + "','" + ApproveDate + "','1','0','0','" + Colour + "','0','0','0','','3',convert(varchar(50),getdate(),120))";

                    }
                    else
                    {
                        //采购入库

                        sql = "INSERT INTO TBWS_IN (WG_CODE," +
                        "WG_ABSTRACT,WG_DATE," +
                        "WG_DOC," +
                        "WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG,WG_CAVFLAG," +
                        "WG_ROB,WG_GJSTATE,WG_HDBH,WG_RealTime,WG_COMPANY,WG_COMPANYCODE) VALUES('" + Code + "','" + Abstract + "','" + Date + "','" +
                         DocumentCode + "','" + AcceptanceCode + "','" + VerifierCode + "','" + ApproveDate + "','1','0','0','" +
                        Colour + "','0','" + HDBH + "',convert(varchar(50),getdate(),120),'" + Supplier + "','" + SupplierCode + "')";
                    }

                    sqllist.Add(sql);



                }
                else
                {
                    if (Code.Contains('B'))
                    {
                        ////结转备库

                        //   sql = "INSERT INTO TBWS_IN(WG_CODE,WG_WAREHOUSE,WG_ABSTRACT,WG_DATE," +
                        //"WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG," +
                        //"WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_ZGFLAG,WG_HSFLAG,WG_ACTPERIOD,WG_BILLTYPE,WG_HDBH,WG_RealTime) VALUES ('" + Code + "','" + WarehouseCode + "','" + Abstract + "','" + Date + "','" +
                        //DocumentCode + "','" + AcceptanceCode + "','" + VerifierCode + "','" + ApproveDate + "','1','0','0','" + Colour + "','0','0','0','','1','" + HDBH + "',convert(varchar(50),getdate(),120))";


                        sql = "update TBWS_IN set WG_ABSTRACT='" + Abstract + "',WG_DATE='" + Date + "',";
                        sql += "WG_DOC='" + DocumentCode + "',WG_REVEICER='" + AcceptanceCode + "',WG_VERIFIER='" + VerifierCode + "',WG_VERIFYDATE='" + ApproveDate + "',WG_STATE='1',WG_TEARFLAG='0',";
                        sql += "WG_CAVFLAG='0',WG_ROB='" + Colour + "',WG_GJSTATE='0',WG_ZGFLAG='0',WG_HSFLAG='0',WG_ACTPERIOD='',WG_BILLTYPE='1',WG_HDBH='" + HDBH + "',WG_RealTime=convert(varchar(50),getdate(),120) where WG_CODE='" + Code + "'";




                    }
                    else if (Code.Contains('X'))
                    {
                        //外协入库

                        // sql = "INSERT INTO TBWS_IN (WG_CODE,WG_WAREHOUSE,WG_ABSTRACT,WG_DATE," +
                        //"WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG," +
                        //"WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_ZGFLAG,WG_HSFLAG,WG_ACTPERIOD,WG_BILLTYPE,WG_RealTime,WG_COMPANY) VALUES ('" + Code + "','" + WarehouseCode + "','" + Abstract + "','" + Date + "','" +
                        //DocumentCode + "','" + AcceptanceCode + "','" + VerifierCode + "','" + ApproveDate + "','1','0','0','" + Colour + "','0','0','0','','3',convert(varchar(50),getdate(),120),'" + Supplier + "')";

                        sql = "update TBWS_IN set WG_ABSTRACT='" + Abstract + "',WG_DATE='" + Date + "',";
                        sql += "WG_DOC='" + DocumentCode + "',WG_REVEICER='" + AcceptanceCode + "',WG_VERIFIER='" + VerifierCode + "',WG_VERIFYDATE='" + ApproveDate + "',WG_STATE='1',WG_TEARFLAG='0',";
                        sql += "WG_CAVFLAG='0',WG_ROB='" + Colour + "',WG_GJSTATE='0',WG_ZGFLAG='0',WG_HSFLAG='0',WG_ACTPERIOD='',WG_BILLTYPE='4',WG_HDBH='" + HDBH + "',WG_RealTime= convert(varchar(50), getdate(), 120)  where WG_CODE='" + Code + "'";

                    }
                    else if (Code.Contains('T'))
                    {
                        //其他入库

                        // sql = "INSERT INTO TBWS_IN (WG_CODE,WG_WAREHOUSE,WG_ABSTRACT,WG_DATE," +
                        //"WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG," +
                        //"WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_ZGFLAG,WG_HSFLAG,WG_ACTPERIOD,WG_BILLTYPE,WG_RealTime,WG_COMPANY) VALUES ('" + Code + "','" + WarehouseCode + "','" + Abstract + "','" + Date + "','" +
                        //DocumentCode + "','" + AcceptanceCode + "','" + VerifierCode + "','" + ApproveDate + "','1','0','0','" + Colour + "','0','0','0','','3',convert(varchar(50),getdate(),120),'" + Supplier + "')";

                        sql = "update TBWS_IN set WG_ABSTRACT='" + Abstract + "',WG_DATE='" + Date + "',";
                        sql += "WG_DOC='" + DocumentCode + "',WG_REVEICER='" + AcceptanceCode + "',WG_VERIFIER='" + VerifierCode + "',WG_VERIFYDATE='" + ApproveDate + "',WG_STATE='1',WG_TEARFLAG='0',";
                        sql += "WG_CAVFLAG='0',WG_ROB='" + Colour + "',WG_GJSTATE='0',WG_ZGFLAG='0',WG_HSFLAG='0',WG_ACTPERIOD='',WG_BILLTYPE='3',WG_HDBH='" + HDBH + "',WG_RealTime= convert(varchar(50), getdate(), 120) where WG_CODE='" + Code + "'";

                    }
                    else
                    {
                        //采购入库
                        // sql = "INSERT INTO TBWS_IN (WG_CODE,WG_WAREHOUSE," +
                        //"WG_ABSTRACT,WG_DATE," +
                        //"WG_DOC," +
                        //"WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG,WG_CAVFLAG," +
                        //"WG_ROB,WG_GJSTATE,WG_HDBH,WG_RealTime) VALUES('" + Code + "','" + WarehouseCode + "','" + Abstract + "','" + Date + "','" +
                        // DocumentCode + "','" + AcceptanceCode + "','" + VerifierCode + "','" + ApproveDate + "','1','0','0','" +
                        //Colour + "','0','" + HDBH + "',convert(varchar(50),getdate(),120))";

                        sql = "update TBWS_IN set WG_ABSTRACT='" + Abstract + "',WG_DATE='" + Date + "',";
                        sql += "WG_DOC='" + DocumentCode + "',WG_REVEICER='" + AcceptanceCode + "',WG_VERIFIER='" + VerifierCode + "',WG_VERIFYDATE='" + ApproveDate + "',WG_STATE='1',WG_TEARFLAG='0',";
                        sql += "WG_CAVFLAG='0',WG_ROB='" + Colour + "',WG_GJSTATE='0',WG_ZGFLAG='0',WG_HSFLAG='0',WG_ACTPERIOD='',WG_BILLTYPE='0',WG_HDBH='" + HDBH + "',WG_RealTime=convert(varchar(50),getdate(),120),WG_PRINTTIME=0,WG_COMPANY='" + Supplier + "',WG_COMPANYCODE='" + SupplierCode + "'  where WG_CODE='" + Code + "'";

                    }

                    sqllist.Add(sql);
                }

                //在存储过程中修改状态


                sql = "DELETE FROM TBWS_INDETAIL WHERE WG_CODE='" + Code + "'";
                sqllist.Add(sql);
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    string UniqueID = Code + (i + 1).ToString().PadLeft(3, '0');
                    string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text;
                    string MaterialName = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialName")).Text;
                    string MaterialStandard = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialStandard")).Text;
                    string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;
                    int Length = 0;
                    try { Length = Convert.ToInt32(Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLength")).Text)); }
                    catch { Length = 0; }
                    int Width = 0;
                    try { Width = Convert.ToInt32(Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxWidth")).Text)); }
                    catch { Width = 0; }
                    string Attribute = ((Label)this.GridView1.Rows[i].FindControl("LabelAttribute")).Text;
                    string GB = ((Label)this.GridView1.Rows[i].FindControl("LabelGB")).Text;
                    string LotNumber = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLotNumber")).Text.Trim();
                    string Unit = ((Label)this.GridView1.Rows[i].FindControl("LabelUnit")).Text;
                    float RN = 0;
                    try
                    {
                        float temp = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxRN")).Text);
                        if ((Colour == "0") && (temp < 0))
                        { RN = -temp; }
                        else if ((Colour == "1") && (temp > 0))
                        { RN = -temp; }
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

                    float Quantity = 0;
                    try
                    {
                        float temp = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxQuantity")).Text);
                        if ((Colour == "0") && (temp < 0))
                        { Quantity = -temp; }
                        else if ((Colour == "1") && (temp > 0))
                        { Quantity = -temp; }
                        else
                        { Quantity = temp; }
                    }
                    catch { Quantity = 0; }
                    double UnitPrice = 0;
                    try { UnitPrice = Convert.ToDouble(((Label)this.GridView1.Rows[i].FindControl("LabelUnitPrice")).Text); }
                    catch { UnitPrice = 0; }
                    float TaxRate = 0;
                    try { TaxRate = Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelTaxRate")).Text); }
                    catch { TaxRate = 0; }
                    double CTUP = 0;
                    try { CTUP = Convert.ToDouble(((Label)this.GridView1.Rows[i].FindControl("LabelCTUP")).Text); }
                    catch { CTUP = 0; }
                    float Amount = 0;
                    try
                    {
                        float temp = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputAmount")).Value);
                        if ((Colour == "0") && (temp < 0))
                        { Amount = -temp; }
                        else if ((Colour == "1") && (temp > 0))
                        { Amount = -temp; }
                        else
                        { Amount = temp; }
                    }
                    catch { Amount = 0; }
                    float CTA = 0;
                    try
                    {
                        float temp = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("InputCTA")).Value);
                        if ((Colour == "0") && (temp < 0))
                        { CTA = -temp; }
                        else if ((Colour == "1") && (temp > 0))
                        { CTA = -temp; }
                        else
                        { CTA = temp; }
                    }
                    catch { CTA = 0; }

                    string Comment = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;
                    string WarehouseIn = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouse")).Text;
                    string WarehouseCodeIn = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;
                    string Position = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxPosition")).Text;

                    ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxPosition")).BackColor = System.Drawing.Color.White;

                    string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;
                    string WarehouseCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;
                    if (PositionCode == string.Empty || PositionCode == "0")
                    {
                        HasError = true;
                        ErrorType = 2;

                        ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxPosition")).BackColor = System.Drawing.Color.Red;

                        break;
                    }

                    string OrderCode = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderCode")).Text;
                    string PlanMode = ((Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text;

                    string CGMode = ((Label)this.GridView1.Rows[i].FindControl("LabelCGMode")).Text;
                    string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;

                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                    string WG_QRUniqCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWG_QRUniqCode")).Text;
                    string sqlUpdateQR2 = "";
                    if (WG_QRUniqCode != "")
                    {
                        sql =
                         "INSERT INTO TBWS_INDETAIL ( WG_UNIQUEID,WG_CODE,WG_MARID,WG_LOTNUM," +
                         "WG_FIXEDSIZE,WG_LENGTH,WG_WIDTH,WG_RSNUM,WG_RSFZNUM,WG_UPRICE,WG_TAXRATE," +
                         "WG_CTAXUPRICE,WG_AMOUNT,WG_CTAMTMNY,WG_LOCATION," +
                         "WG_ORDERID,WG_PMODE,WG_PTCODE,WG_NOTE,WG_STATE,WG_CGMODE,WG_WAREHOUSE,WG_QRUniqCode) VALUES('" + UniqueID +
                         "','" + Code + "','" + MaterialCode + "','" + LotNumber + "','"
                         + Fixed + "','" + Length + "','" + Width + "','" + RN + "','" + Quantity + "','" + UnitPrice + "','" +
                         TaxRate + "','" + CTUP + "','" + Amount + "','" + CTA + "','" +
                         PositionCode + "','" + OrderCode + "','" +
                         PlanMode + "','" + PTC + "','" + Comment + "','','" + CGMode + "','" + WarehouseCode + "','" + WG_QRUniqCode + "')";
                        sqlUpdateQR2 = "update midTable_QRIn set QRIn_State='1' where CONVERT(varchar(20),QRIn_ID)='" + WG_QRUniqCode + "'";
                        sqllist.Add(sqlUpdateQR2);
                    }
                    else
                    {
                        sql =
                         "INSERT INTO TBWS_INDETAIL ( WG_UNIQUEID,WG_CODE,WG_MARID,WG_LOTNUM," +
                         "WG_FIXEDSIZE,WG_LENGTH,WG_WIDTH,WG_RSNUM,WG_RSFZNUM,WG_UPRICE,WG_TAXRATE," +
                         "WG_CTAXUPRICE,WG_AMOUNT,WG_CTAMTMNY,WG_LOCATION," +
                         "WG_ORDERID,WG_PMODE,WG_PTCODE,WG_NOTE,WG_STATE,WG_CGMODE,WG_WAREHOUSE,WG_QRUniqCode) VALUES('" + UniqueID +
                         "','" + Code + "','" + MaterialCode + "','" + LotNumber + "','"
                         + Fixed + "','" + Length + "','" + Width + "','" + RN + "','" + Quantity + "','" + UnitPrice + "','" +
                         TaxRate + "','" + CTUP + "','" + Amount + "','" + CTA + "','" +
                         PositionCode + "','" + OrderCode + "','" +
                         PlanMode + "','" + PTC + "','" + Comment + "','','" + CGMode + "','" + WarehouseCode + "',NULL)";
                    }
                    //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%



                    sqllist.Add(sql);
                }
                if (HasError)
                {
                    sqllist.Clear();

                    if (ErrorType == 1)
                    {
                        LabelMessage.Text = "入库条目为0，单据不能审核！";

                        string script = @"alert('入库条目为0，单据不能审核！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                    else
                    {

                        LabelMessage.Text = "入库条目仓位为空，单据不能审核！";

                        string script = @"alert('入库条目仓位为空，单据不能审核！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                }
                else
                {
                    DBCallCommon.ExecuteTrans(sqllist);
                    sqllist.Clear();


                    /*
                     * 调用存储过程来修改库存的数量
                     */


                    sql = DBCallCommon.GetStringValue("connectionStrings");
                    SqlConnection con = new SqlConnection(sql);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("MaterialIn", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@InCode", SqlDbType.VarChar, 50);				//增加参数入库单号@InCode
                    cmd.Parameters["@InCode"].Value = LabelCode.Text;						//为参数初始化
                    cmd.Parameters.Add("@InDate", SqlDbType.VarChar, 50);				//审核日期@InDate
                    cmd.Parameters["@InDate"].Value = ApproveDate;							//为参数初始化
                    cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;			//增加返回值参数@retVal
                    cmd.ExecuteNonQuery();
                    con.Close();
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 0)
                    {
                        LabelState.Text = "2";

                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            string PLMode = ((Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text;

                            string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;


                            IEnumerable<string> tsa_id = PTC.Split('_').Reverse<string>();
                            string tsa = tsa_id.ElementAt(2).ToString();

                            string sqlr = " select * from  View_TBPC_PURORDERDETAIL_PLAN_TOTAL where totalstate != '3' and PO_MASHAPE='" + PLMode + "' and ptcode like '%" + tsa + "%' ";
                            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqlr);
                            if (dt1.Rows.Count == 0)
                            {
                                sql = " update TBMP_MAINPLANDETAIL set MP_STATE='2',MP_ACTURALTIME=Convert(varchar(50), getdate(), 120)  where MP_ENGID='" + tsa + "' and MP_PLNAME='" + PLMode + "' ";
                                sqllist.Add(sql);
                            }
                        }
                        DBCallCommon.ExecuteTrans(sqllist);
                        sqllist.Clear();



                        Response.Redirect("SM_WarehouseIN_WG.aspx?FLAG=READ&&ID=" + Code);
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
                    {
                        LabelMessage.Text = "审核未通过：入库物料发生后续操作！";

                        string script = @"alert('审核未通过：入库物料发生后续操作！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
                    {
                        LabelMessage.Text = "审核未通过：部分入库物料发生后续操作！";

                        string script = @"alert('审核未通过：部分入库物料发生后续操作！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -1)
                    {
                        LabelMessage.Text = "审核未通过：审核过程中发生异常，请检查之后重新审核！";

                        string script = @"alert('审核未通过：审核过程中发生异常，请检查之后重新审核！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    }
                }
            }
        }

        protected void FS_Click(object sender, EventArgs e)
        {

            /*
             * 核对是否可以反审
             * 
             * 然后记录入库单
             * 
             * ============================================
             * 跨月是不能反审
             * ============================================
             * 
             */
            if (isLock() == false)
            {
                string script = @"alert('系统正在结账,请稍后...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                LabelMessage.Text = "系统正在结账,请稍后...";
            }
            else
            {

                string Code = LabelCode.Text.Trim();
                /*
                 * 
                 * 调用存储过程
                 * 
                 */
                /*
                * 这三个可以不用执行存储过程，可以根据页面状态来减少页面回发
                * 
                */
                if (LabelState.Text.Trim() == "1")
                {
                    LabelMessage.Text = "反审核未通过：未审核入库单不允许反审！";

                    string script = @"alert('反审核未通过：未审核入库单不允许反审！');";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                }
                else if (LabelGJState.Text.Trim() != "0")
                {
                    LabelMessage.Text = "反审核未通过：正在进行勾稽的入库单不允许反审！";

                    string script = @"alert('反审核未通过：正在进行勾稽的入库单不允许反审！');";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                }
                else if (LabelHXState.Text.Trim() == "1")
                {
                    LabelMessage.Text = "反审核未通过：已核销的入库单不允许反审！";

                    string script = @"alert('反审核未通过：已核销的入库单不允许反审！');";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                }
                else if (LabelHSState.Text.Trim() == "1")
                {
                    LabelMessage.Text = "反审核未通过：跨月的入库单不允许反审！";

                    string script = @"alert('反审核未通过：跨月的入库单不允许反审！');";

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                }

                else
                {

                    string sql = DBCallCommon.GetStringValue("connectionStrings");
                    SqlConnection con = new SqlConnection(sql);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("AntiVerifyIn", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.Parameters.Add("@InCode", SqlDbType.VarChar, 50);				//增加参数入库单号@InCode
                    cmd.Parameters["@InCode"].Value = Code;     //为参数初始化

                    cmd.Parameters.Add("@ErrorRow", SqlDbType.Int, 1).Direction = ParameterDirection.Output;

                    cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;			//增加返回值参数@retVal

                    cmd.ExecuteNonQuery();

                    con.Close();

                    /*
                     * 返回值操作提示
                     */
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 0)
                    {
                        //审核图像可见

                        ImageVerify.Visible = false;

                        LabelMessage.Text = "反审核通过！";

                        Response.Redirect("SM_WarehouseIN_WG.aspx?FLAG=READ&&ID=" + Code);

                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
                    {

                        LabelMessage.Text = "反审核未通过:第" + Convert.ToString(cmd.Parameters["@ErrorRow"].Value) + "行库存物料不存在！";

                        GridView1.Rows[Convert.ToInt32(cmd.Parameters["@ErrorRow"].Value) - 1].Cells[0].BackColor = System.Drawing.Color.Red;
                        GridView1.Rows[Convert.ToInt32(cmd.Parameters["@ErrorRow"].Value) - 1].Cells[1].BackColor = System.Drawing.Color.Red;
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
                    {
                        LabelMessage.Text = "反审核未通过：第" + Convert.ToString(cmd.Parameters["@ErrorRow"].Value) + "行库存物料数量小于出库数量！";
                        GridView1.Rows[Convert.ToInt32(cmd.Parameters["@ErrorRow"].Value) - 1].Cells[0].BackColor = System.Drawing.Color.Red;
                        GridView1.Rows[Convert.ToInt32(cmd.Parameters["@ErrorRow"].Value) - 1].Cells[1].BackColor = System.Drawing.Color.Red;

                        //string script = @"alert('反审核未通过：部分入库物料发生后续操作！');";

                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }

                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -5)
                    {
                        LabelMessage.Text = "反审核未通过：反审过程中系统遇到异常，请核对数据之后重新反审！";

                        string script = @"alert('反审核未通过：反审过程中系统遇到异常，请核对数据之后重新反审！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                }

            }
        }
        

        protected void CD_Click(object sender, EventArgs e)
        {
            List<string> sqllist = new List<string>();

            string sql = "UPDATE TBWS_INDETAIL SET WG_STATE='' WHERE WG_STATE='SPLIT" + Session["UserID"].ToString() + "'";
            sqllist.Add(sql);
            string incode = LabelCode.Text.Trim();
            string uniqueid = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked == true)
                {
                    string gjstate =LabelGJState.Text;
                    string hxstate = LabelHXState.Text;
                    if (gjstate != "0")
                    {
                        LabelMessage.Text = "正在勾稽的入库单无法拆分！";
                        string script = @"alert('正在勾稽的入库单无法拆分！');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                        return;
                    }
                    if (hxstate != "0")
                    {
                        LabelMessage.Text = "正在核销的入库单无法拆分！";
                        string script = @"alert('正在核销的入库单无法拆分！');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                        return;
                    }

                    if (incode.Contains("R") == true)
                    {
                        LabelMessage.Text = "红字入库单不允许拆分！";

                        string script = @"alert('红字入库单不允许拆分！');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                        return;
                    }
                    else if (incode.Contains("S") == true)
                    {
                        LabelMessage.Text = "子入库单不允许拆分！";
                        string script = @"alert('子入库单不允许拆分！');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                        return;
                    }
                    else
                    {
                        uniqueid = ((Label)GridView1.Rows[i].FindControl("LabelUniqueID")).Text;
                        //更新明细表中的状态
                        sql = "UPDATE TBWS_INDETAIL SET WG_STATE='SPLIT" + Session["UserID"].ToString() + "' WHERE WG_CODE='" + incode + "' AND WG_UNIQUEID='" + uniqueid + "'";
                        sqllist.Add(sql);
                    }
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
            Response.Redirect("SM_WarehouseIN_WGSplit.aspx?ID=" + incode);
        }
        /*
       * 合并
       */
        protected void HB_Click(object sender, EventArgs e)
        {
            string code = LabelCode.Text.Trim();
            string gjstate = LabelGJState.Text;
            string hxstate = LabelHXState.Text;

            if (gjstate != "0")
            {
                LabelMessage.Text = "正在勾稽的入库单无法合并！";
                return;
            }

            if (hxstate != "0")
            {
                LabelMessage.Text = "正在核销的入库单无法合并！";
                return;
            }

            if (code.Contains("R") == true)
            {
                LabelMessage.Text = "红字入库单不允许合并！";
                return;
            }
            else
            {
                if (code.Contains("S") == true)
                {
                    int count=0;
                    string strsqltex="SELECT COUNT(*) FROM TBWS_IN WHERE WG_CODE LIKE '"+code+"%' AND WG_ROB='1' ";
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
                        string pcode = code.Substring(0, code.IndexOf("S", 0));
                        Response.Redirect("SM_WarehouseIn_ActionResult.aspx?IDP=" + pcode + "&&IDC=" + code + "&&RES=M");

                    }
                }
                else
                {
                    LabelMessage.Text = "请选择单号带有S的子单进行合并操作！";
                    return;
                }
            }
        }

        /*
         * 下推红字
         * 
         * 现在的推红方法是推的是物料--
         * 
         * 推红无法核销
         * 
         */

        protected void TH_Click(object sender, EventArgs e) 
        {

            //此处为由蓝字入库单下推红字入库单操作

            List<string> sqllist = new List<string>();

            string sql = "UPDATE TBWS_INDETAIL SET WG_STATE='' WHERE WG_STATE='RED" + Session["UserID"].ToString() + "'";

            sqllist.Add(sql);

            string incode = LabelCode.Text.Trim();

            string uniqueid = "";

            string gjstate = LabelGJState.Text;

            if (gjstate != "0")
            {
                LabelMessage.Text = "正在勾稽的入库单无法下推红字入库单！";
                return;
            }
            if (incode.Contains("R") == true)
            {
                LabelMessage.Text = "所选条目不能来自红字入库单，请检查！";
                return;
            }

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked == true)
                {
                    uniqueid = ((Label)GridView1.Rows[i].FindControl("LabelUniqueID")).Text;

                    sql = "UPDATE TBWS_INDETAIL SET WG_STATE='RED" + Session["UserID"].ToString() + "' WHERE WG_CODE='" + incode + "' AND WG_UNIQUEID='" + uniqueid + "'";

                    sqllist.Add(sql);
                }
            }

            if (sqllist.Count == 1)
            {
                sqllist.Clear();
                LabelMessage.Text = "请选择要推红的条目！";
                return;
            }

            if (((sqllist.Count - 1) < GridView1.Rows.Count) && incode.Contains('G'))
            {
                //(sqllist.Count - 1) < GridView1.Rows.Count表示部分推红
                //incode.Contains('S')表示是子单，!(incode.Contains('S'))表示为母单
                //必须从子单中推红，否则不容许推红
                //推红不能改变数量

                sqllist.Clear();
                LabelMessage.Text = "不容许从母单部分推红，必须拆成子单，然后再从子单推红！";
                string script = @"alert('不容许从母单部分推红，必须拆成子单，然后再从子单推红！');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                return;
            }
            

            DBCallCommon.ExecuteTrans(sqllist);

            Response.Redirect("~/SM_Data/SM_WarehouseIN_WG.aspx?FLAG=PUSHRED&&ID=" + incode);

        }


        /*
         * 核销
         */
        protected void HX_Click(object sender, EventArgs e)
        {
            string code = LabelCode.Text.Trim();

            string gjstate = LabelGJState.Text;

            string hxstate = LabelHXState.Text;

            if (gjstate != "0")
            {
                LabelMessage.Text = "已勾稽的入库单无法核销！";
                return;
            }

            if (hxstate != "0")
            {
                LabelMessage.Text = "已核销的入库单无法核销！";
                return;
            }

            if (code.Contains("R") == true)
            {
                string pcode = code.Substring(0, code.IndexOf("R", 0));
                Response.Redirect("SM_WarehouseIn_ActionResult.aspx?IDP=" + pcode + "&&IDC=" + code + "&&RES=V");
            }
            else
            {
                LabelMessage.Text = "请选择单号带有R的红字入库单进行核销操作！";
                return;
            }

        }


        /*
         * 反核销
         */
        protected void FHX_Click(object sender, EventArgs e)
        {
            string code = LabelCode.Text.Trim();

            string hxstate = LabelHXState.Text;

            if (hxstate == "0")
            {
                LabelMessage.Text = "未核销的入库单无法反核销！";
                return;
            }
            if (code.Contains("R") == true)
            {
                string pcode = code.Substring(0, code.IndexOf("R", 0));
                Response.Redirect("SM_WarehouseIn_ActionResult.aspx?IDP=" + pcode + "&&IDC=" + code + "&&RES=F");
            }
            else
            {
                LabelMessage.Text = "请选择单号带有R的红字入库单进行反核销操作！";
                return;
            }

        }


        /*
         * 以某物料的计划跟踪号来关联的，如果以整个入库单关联，可能会造成有许多个计划跟踪号
         * 
         * 
         * //关联订单
         */

        protected void GL_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((CheckBox)GridView1.Rows[i].FindControl("CheckBox1")).Checked == true)
                {
                    /*
                     * //计划跟踪号
                     */
                    string ptc = ((Label)GridView1.Rows[i].FindControl("LabelPTC")).Text;

                    Response.Redirect("SM_Warehouse_RelatedDocument.aspx?PTC=" + ptc);

                    return;
                }
            }
            LabelMessage.Text = "请选择一条要查询的记录！";
        }

        protected void torder_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_WarehouseIN_WGPush.aspx?FLAG=PUSH");
        }


        //备库tobk_Click
        protected void tobk_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Warehouse_TempIn.aspx?FLAG=PUSHIN");
        }

        //外协出库
        protected void wxout_Click(object sender, EventArgs e)
        {

            bool IsSelect = true;

            List<string> sqllist = new List<string>();

            string sql = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='OUT" + Session["UserID"].ToString() + "'";

            sqllist.Add(sql);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if ((GridView1.Rows[i].FindControl("CheckBox1") as CheckBox).Checked)
                {
                    string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text;

                    int Length = 0;
                    try { Length = Convert.ToInt32(Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLength")).Text)); }
                    catch { Length = 0; }

                    int Width = 0;
                    try { Width = Convert.ToInt32(Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxWidth")).Text)); }
                    catch { Width = 0; }
                    
                    string LotNumber = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLotNumber")).Text;

                    string WarehouseCodeIn = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;
                   
                    string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;
                    string WarehouseCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Value;
                    string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;

                    string SQCODE = MaterialCode + PTC + LotNumber + Length.ToString() + Width.ToString() + WarehouseCodeIn + PositionCode;

                    sql = "UPDATE TBWS_STORAGE SET SQ_STATE='OUT" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + SQCODE + "'";
                    
                    sqllist.Add(sql);

                    IsSelect = false;
                }
            }

            if (IsSelect)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    
                        string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text;

                        int Length = 0;
                        try { Length = Convert.ToInt32(Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLength")).Text)); }
                        catch { Length = 0; }

                        int Width = 0;
                        try { Width = Convert.ToInt32(Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxWidth")).Text)); }
                        catch { Width = 0; }

                        string LotNumber = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLotNumber")).Text;

                        string WarehouseCodeIn = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;

                        string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;
                        string WarehouseCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;
                        string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;

                        string SQCODE = MaterialCode + PTC + LotNumber + Length.ToString() + Width.ToString() + WarehouseCodeIn + PositionCode;

                        sql = "UPDATE TBWS_STORAGE SET SQ_STATE='OUT" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + SQCODE + "'";

                        sqllist.Add(sql);
                   
                }
            }


            if (sqllist.Count < 2)
            {
                sqllist.Clear();

                LabelMessage.Text = "请选择要下推出库单的条目！";

                string script = @"alert('请选择要下推出库单的条目！');";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
            }
            else
            {

                DBCallCommon.ExecuteTrans(sqllist);

                sqllist.Clear();

                Response.Redirect("~/SM_Data/SM_WarehouseOUT_LL.aspx?FLAG=PUSHBLUE&&ID=NEW", false);
            }
        }



        //删单

        protected void DelForm_Click(object sender, EventArgs e)
        {
            if (isLock() == false)
            {
                string script = @"alert('系统正在结账,请稍后...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                LabelMessage.Text = "系统正在结账,请稍后...";
            }
            else
            {

                string code = LabelCode.Text.Trim();

                string sqlstate = "select WG_STATE from TBWS_IN where WG_CODE='" + code + "'";

                if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows.Count > 0)
                {
                    if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows[0]["WG_STATE"].ToString() == "2")
                    {

                        string scriptstate = @"alert('单据已审核，不能删单！');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", scriptstate, true);

                        return;
                    }
                }

                string sql = string.Empty;

                List<string> temp = new List<string>();



                sql = "DELETE FROM TBWS_IN WHERE WG_CODE='" + code + "'";

                temp.Add(sql);

                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
                string sqlgetdata = "select * from TBWS_INDETAIL where WG_CODE='" + code + "'";
                DataTable dtgetdata = DBCallCommon.GetDTUsingSqlText(sqlgetdata);
                string sqlUpdateQR = "";
                if (dtgetdata.Rows.Count > 0)
                {
                    for (int k = 0; k < dtgetdata.Rows.Count; k++)
                    {
                        if (dtgetdata.Rows[k]["WG_QRUniqCode"].ToString().Trim() != "")
                        {
                            sqlUpdateQR = "update midTable_QRIn set QRIn_State='0' where CONVERT(varchar(20),QRIn_ID)='" + dtgetdata.Rows[k]["WG_QRUniqCode"].ToString().Trim() + "'";
                            temp.Add(sqlUpdateQR);
                        }
                    }
                }
                //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

                sql = "DELETE FROM TBWS_INDETAIL WHERE WG_CODE='" + code + "'";

                temp.Add(sql);

                DBCallCommon.ExecuteTrans(temp);

                //LabelMessage.Text = "删除成功";

                string script = @"closewin();";

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);
                //Response.Write("<script type='text/javascript' language='javascript' >window.close();</script>");
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
            lt.Add("长(mm)");
            lt.Add("宽(mm)");
            lt.Add("张(支)数");

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

                 for (int i = 0; i < GridView1.Columns.Count; i++)
                 {
                     foreach(string st in lt)
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

        protected void SumPrint_Click(object sender, EventArgs e)
        {
            if (CheckBoxPrint.Checked)
            {
                string sql = "UPDATE TBWS_IN SET WG_PRINTTIME=ISNULL(WG_PRINTTIME,0)+1 WHERE WG_CODE='" + LabelCode.Text.Trim() + "'";
                DBCallCommon.ExeSqlText(sql);
            }

            string script = @"btnPrint_onclick('sum');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
        }
        protected void Print_Click(object sender, EventArgs e)
        {
            if (CheckBoxPrint.Checked)
            {
                string sql = "UPDATE TBWS_IN SET WG_PRINTTIME=ISNULL(WG_PRINTTIME,0)+1 WHERE WG_CODE='" + LabelCode.Text.Trim() + "'";
                DBCallCommon.ExeSqlText(sql);
            }

            string script = @"btnPrint_onclick('detail');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
        }




        //导出二维码信息
        protected void btn_QRExport_Click(object sender, EventArgs e)
        {
            string sqltext = "SELECT WG_UNIQUEID AS UniqueID,WG_MARID AS MaterialCode,MNAME AS MaterialName," +
                    "CAIZHI AS Attribute,GB AS GB,GUIGE AS MaterialStandard,WG_FIXEDSIZE AS Fixed,WG_LENGTH AS Length,WG_WIDTH AS Width," +
                    "WG_LOTNUM AS LotNumber,CGDW AS Unit,cast(WG_RSNUM as float) AS RN,WG_RSFZNUM AS Quantity," +
                    "cast(WG_UPRICE as float) AS UnitPrice,WG_TAXRATE AS TaxRate,cast(WG_CTAXUPRICE as float) AS CTUP," +
                    "cast(WG_AMOUNT as float) AS Amount,cast(WG_CTAMTMNY as float) AS CTA,WG_WAREHOUSE AS WarehouseCode," +
                    "WS_NAME AS Warehouse,WG_LOCATION AS PositionCode,WL_NAME AS Position," +
                    "WG_ORDERID AS OrderCode,WG_PMODE AS PlanMode,WG_PTCODE AS PTC,WG_NOTE AS Comment," +
                    "WG_STATE AS State,WG_CGMODE AS CGMODE,WG_QRUniqCode,s.PUR_NUM as dingenum FROM View_SM_IN as a left join TBPC_PURCHASEPLAN as s on a.WG_PTCODE=s.PUR_PTCODE WHERE WG_CODE='" + LabelCode.Text.Trim() + "' order by WG_UNIQUEID";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 500)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导出条数大于500，请分批导出！');", true);
                return;
            }
            string filename = "入库单二维码物料信息" + DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("入库单二维码信息导出模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    row.HeightInPoints = 14;//行高
                    row.CreateCell(0).SetCellValue(dt.Rows[i]["MaterialCode"].ToString() + dt.Rows[i]["PTC"].ToString() + dt.Rows[i]["LotNumber"].ToString() + dt.Rows[i]["Length"].ToString() + dt.Rows[i]["Width"].ToString() + dt.Rows[i]["WarehouseCode"].ToString() + dt.Rows[i]["PositionCode"].ToString() + ";" + "2");//二维码类型
                    row.CreateCell(1).SetCellValue(dt.Rows[i]["MaterialCode"].ToString() + dt.Rows[i]["PTC"].ToString() + dt.Rows[i]["LotNumber"].ToString() + dt.Rows[i]["Length"].ToString() + dt.Rows[i]["Width"].ToString() + dt.Rows[i]["WarehouseCode"].ToString() + dt.Rows[i]["PositionCode"].ToString());//计划跟踪号
                    row.CreateCell(2).SetCellValue(dt.Rows[i]["PTC"].ToString());//计划跟踪号
                    row.CreateCell(3).SetCellValue(DateTime.Now.ToString("yyyyMMddHHmmss").Trim());//时间编码
                    row.CreateCell(4).SetCellValue(LabelCode.Text.Trim());//入库单号
                    row.CreateCell(5).SetCellValue(LabelSupplier.Text.Trim());//供应商
                    row.CreateCell(6).SetCellValue(dt.Rows[i]["MaterialCode"].ToString());//物料编码
                    row.CreateCell(7).SetCellValue(dt.Rows[i]["MaterialName"].ToString());//物料名称
                    row.CreateCell(8).SetCellValue(dt.Rows[i]["MaterialStandard"].ToString());//型号规格
                    row.CreateCell(9).SetCellValue(dt.Rows[i]["Attribute"].ToString());//材质
                    row.CreateCell(10).SetCellValue(dt.Rows[i]["GB"].ToString());//国标
                    row.CreateCell(11).SetCellValue(dt.Rows[i]["Length"].ToString());//长
                    row.CreateCell(12).SetCellValue(dt.Rows[i]["Width"].ToString());//宽
                    row.CreateCell(13).SetCellValue(dt.Rows[i]["Unit"].ToString());//单位
                    row.CreateCell(14).SetCellValue(dt.Rows[i]["dingenum"].ToString());//定额数量
                    row.CreateCell(15).SetCellValue(TextBoxDate.Text.Trim());//入库时间
                    row.CreateCell(16).SetCellValue("出库");//二维码类型
                    NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                    font1.FontName = "仿宋";//字体
                    font1.FontHeightInPoints = 11;//字号
                    ICellStyle cells = wk.CreateCellStyle();
                    cells.SetFont(font1);
                    cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                    for (int j = 0; j <= 16; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }
                }

                #endregion

                sheet0.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }

        //导出二维码合并信息
        protected void btn_QRHBExport_Click(object sender, EventArgs e)
        {
            string sqltext = "SELECT WG_UNIQUEID AS UniqueID,WG_MARID AS MaterialCode,MNAME AS MaterialName," +
                    "CAIZHI AS Attribute,GB AS GB,GUIGE AS MaterialStandard,WG_FIXEDSIZE AS Fixed,WG_LENGTH AS Length,WG_WIDTH AS Width," +
                    "WG_LOTNUM AS LotNumber,CGDW AS Unit,cast(WG_RSNUM as float) AS RN,WG_RSFZNUM AS Quantity," +
                    "cast(WG_UPRICE as float) AS UnitPrice,WG_TAXRATE AS TaxRate,cast(WG_CTAXUPRICE as float) AS CTUP," +
                    "cast(WG_AMOUNT as float) AS Amount,cast(WG_CTAMTMNY as float) AS CTA,WG_WAREHOUSE AS WarehouseCode," +
                    "WS_NAME AS Warehouse,WG_LOCATION AS PositionCode,WL_NAME AS Position," +
                    "WG_ORDERID AS OrderCode,WG_PMODE AS PlanMode,WG_PTCODE AS PTC,WG_NOTE AS Comment," +
                    "WG_STATE AS State,WG_CGMODE AS CGMODE,WG_QRUniqCode,s.PUR_NUM as dingenum FROM View_SM_IN as a left join TBPC_PURCHASEPLAN as s on a.WG_PTCODE=s.PUR_PTCODE WHERE WG_CODE='" + LabelCode.Text.Trim() + "' order by WG_UNIQUEID";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 500)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导出条数大于500，请分批导出！');", true);
                return;
            }
            string filename = "入库单二维码物料信息合并" + DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();

            string AllInfo = "";
            //1.读取Excel到FileStream
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("入库单二维码信息合并导出模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i);
                    row.HeightInPoints = 14;//行高
                    //AllInfo = dt.Rows[i]["MaterialCode"].ToString() + dt.Rows[i]["PTC"].ToString() + dt.Rows[i]["LotNumber"].ToString() + dt.Rows[i]["Length"].ToString() + dt.Rows[i]["Width"].ToString() + dt.Rows[i]["WarehouseCode"].ToString() + dt.Rows[i]["PositionCode"].ToString() + ";" + dt.Rows[i]["PTC"].ToString() + ";" + DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + ";" + LabelCode.Text.Trim() + ";" + LabelSupplier.Text.Trim() + ";" + dt.Rows[i]["MaterialCode"].ToString() + ";" + dt.Rows[i]["MaterialName"].ToString() + ";" + dt.Rows[i]["MaterialStandard"].ToString() + ";" + dt.Rows[i]["Attribute"].ToString() + ";" + dt.Rows[i]["GB"].ToString() + ";" + dt.Rows[i]["Length"].ToString() + ";" + dt.Rows[i]["Width"].ToString() + ";" + dt.Rows[i]["Unit"].ToString() + ";" + dt.Rows[i]["dingenum"].ToString() + ";" + TextBoxDate.Text.Trim() + ";" + "2";
                    //AllInfo = dt.Rows[i]["MaterialCode"].ToString() + dt.Rows[i]["PTC"].ToString() + dt.Rows[i]["LotNumber"].ToString() + dt.Rows[i]["Length"].ToString() + dt.Rows[i]["Width"].ToString() + dt.Rows[i]["WarehouseCode"].ToString() + dt.Rows[i]["PositionCode"].ToString() + ";" + dt.Rows[i]["PTC"].ToString() + ";" + DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + ";" + "2";
                    AllInfo = dt.Rows[i]["MaterialCode"].ToString() + dt.Rows[i]["PTC"].ToString() + dt.Rows[i]["LotNumber"].ToString() + dt.Rows[i]["Length"].ToString() + dt.Rows[i]["Width"].ToString() + dt.Rows[i]["WarehouseCode"].ToString() + dt.Rows[i]["PositionCode"].ToString() + ";" + "2";
                    row.CreateCell(0).SetCellValue(AllInfo);
                    NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                    font1.FontName = "仿宋";//字体
                    font1.FontHeightInPoints = 11;//字号
                    ICellStyle cells = wk.CreateCellStyle();
                    cells.SetFont(font1);
                    cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                    for (int j = 0; j <= 0; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }
                }

                #endregion

                sheet0.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
    }
}
