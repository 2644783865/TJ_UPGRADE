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
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_WarehouseIN_PY : System.Web.UI.Page
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

        //获取系统核算时间
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




        //控制操作控件

        private void bindControlEnable(bool isVisual)
        {
            if (isVisual)
            {
                FS.Enabled = true;
               
                btnPrint.Disabled = false;
                DelForm.Enabled = false;
            }
            else
            {
                Save.Enabled = true;
                Verify.Enabled = true;
                FS.Enabled = false;
               
                btnPrint.Disabled = true;
                DelForm.Enabled = true;

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
            string sql = "SELECT DISTINCT ST_CODE,ST_NAME FROM TBDS_STAFFINFO WHERE ST_DEPID='07'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListAcceptance.DataTextField = "ST_NAME";
            DropDownListAcceptance.DataValueField = "ST_CODE";
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

                    TextBox tb = (TextBox)GridView1.Rows[i].FindControl("TextBoxPosition");
                    tb.Text = "待查";

                    HtmlInputText hit = (HtmlInputText)GridView1.Rows[i].FindControl("InputPositionCode");
                    hit.Value = "0";

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

                    TextBox tb = (TextBox)GridView1.Rows[i].FindControl("TextBoxPosition");
                    tb.Text = "待查";

                    HtmlInputText hit = (HtmlInputText)GridView1.Rows[i].FindControl("InputPositionCode");
                    hit.Value = "0";

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
        }



        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //foreach (DataControlField dataControlField in GridView1.Columns)
                //{

                //    //if (this.SortDire.Equals("ASC") || this.SortDire.Equals("DESC"))
                //    //{
                //    //    Label label = new Label();
                //    //    label.Text = this.SortDire.Equals("ASC") ? "↓" : "↑";
                //    //    label.ForeColor = System.Drawing.Color.White;
                //    //    e.Row.Cells[GridView1.Columns.IndexOf(dataControlField)].Controls.Add(label);
                //    //}
                //}
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tn += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "RN"));
                tq += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Quantity"));
                ta += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "Amount"));
                tcta += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "CTA"));

                //e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#55DF55'");//当鼠标停留时更改背景色
                //e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#EFF3FB'");//当鼠标移开时还原背景色
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.Cells[12].FindControl("LabelTotalNum")).Text = Math.Round(tn, 4).ToString();
                ((Label)e.Row.Cells[13].FindControl("LabelTotalQuantity")).Text = tq.ToString();
                ((Label)e.Row.Cells[17].FindControl("LabelTotalAmount")).Text = Math.Round(ta, 2).ToString();
                ((Label)e.Row.Cells[18].FindControl("LabelTotalCTA")).Text = Math.Round(tcta, 2).ToString();
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

            if (flag == "PUSHRED")
            {

                //WG_NOTE AS Comment 备注，用于显示过磅和理论
                string sql = "SELECT WG_CODE AS Code,Supplier AS SupplierCode,SupplierName AS Supplier," +
                    "WG_WAREHOUSE AS WarehouseCode,WS_NAME AS Warehouse,WG_LOCATION AS PositionCode,WG_ABSTRACT AS Abstract,WG_HDBH AS HDBH,WG_DATE AS Date," +
                    "Dep AS DepCode,DepName AS Dep,Clerk AS ClerkCode,ClerkName AS Clerk," +
                    "WG_DOC AS DocCode,DocName AS Doc,WG_REVEICER AS AcceptanceCode,ReveicerName AS Acceptance," +
                    "WG_VERIFIER AS VerifierCode,VerfierName AS Verifier,left(WG_VERIFYDATE,10) AS ApproveDate," +
                    "WG_STATE AS State,WG_TEARFLAG AS Split ,WG_CAVFLAG AS Articulation,WG_ROB AS Colour FROM View_SM_IN WHERE WG_CODE='" + code + "'";

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
                    //选择仓位
                    getWarehousePosition();

                    DropDownListPosition.ClearSelection();
                    try
                    {
                        DropDownListPosition.Items.FindByValue(dr["PositionCode"].ToString()).Selected = true;//仓库
                    }
                    catch { }

                    TextBoxAbstract.Text = "";
                    TextBoxHDBH.Text = dr["HDBH"].ToString();
                   
                    LabelDoc.Text = Session["UserName"].ToString();
                    LabelDocCode.Text = Session["UserID"].ToString();
                    try { DropDownListAcceptance.Items.FindByValue(dr["AcceptanceCode"].ToString()).Selected = true; }
                    catch { }
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
                    "WG_PTCODE AS PTC,WG_NOTE AS Comment,WG_STATE AS State,WG_CGMODE AS CGMODE FROM View_SM_IN WHERE WG_CODE='" + code + "' AND DetailState='RED" + Session["UserID"].ToString() + "'";
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
              

                bindControlEnable(false);
            }


            //下推蓝字入库
            if (flag == "PUSHBLUE")
            {
                //detailnote用于填写备注，理计和过磅

                string sql = "SELECT orderno AS OrderCode,supplierid AS SupplierCode,suppliernm AS Supplier," +
                    "ywyid AS ClerkCode,ywynm AS Clerk,depid AS DepCode,depnm AS Dep," +
                    "marid AS MaterialCode,marnm AS MaterialName,margg AS MaterialStandard," +
                    "fixed AS Fixed,length AS Length,width AS Width," +
                    "marcz AS Attribute,margb AS GB,marunit AS Unit,cast((num-recgdnum) as float) AS RN,0 AS Quantity,cast(price as float) AS UnitPrice,cast(ctprice as float) AS CTUP,cast(round((num-recgdnum)*price,2) as float) AS Amount," +
                    "taxrate AS TaxRate,cast(round((num-recgdnum)*ctprice,2) as float) AS CTA,detailnote AS Comment,planmode AS PlanMode,ptcode AS PTC," +
                    "totalstate AS State,UniqueID='',LotNumber='',WarehouseCode='0',Warehouse='--请选择--',PositionCode='0',Position='待查',PO_CGFS AS CGMODE FROM View_TBPC_PURORDERDETAIL_PLAN_TOTAL WHERE whstate='BLUE" + Session["UserID"].ToString() + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                sql = "UPDATE TBPC_PURORDERDETAIL SET PO_WHSTATE='1' WHERE PO_WHSTATE='BLUE" + Session["UserID"].ToString() + "'";

                DBCallCommon.ExeSqlText(sql);

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

                DelForm.Enabled = false;//删单不可用

            }

            //入库单编辑载入

            //编辑已没有，已被读取数据状态取代

            #region 编辑状态已没有

            if (flag == "EDIT")
            {
                string sql = "SELECT WG_CODE AS Code,Supplier AS SupplierCode,SupplierName AS Supplier," +
                     "WG_WAREHOUSE AS WarehouseCode,WS_NAME AS Warehouse,WG_ABSTRACT AS Abstract,WG_HDBH AS HDBH,WG_DATE AS Date," +
                     "Dep AS DepCode,DepName AS Dep,Clerk AS ClerkCode,ClerkName AS Clerk," +
                     "WG_DOC AS DocCode,DocName AS Doc,WG_REVEICER AS AcceptanceCode,ReveicerName AS Acceptance," +
                     "WG_VERIFIER AS VerifierCode,VerfierName AS Verifier,WG_VERIFYDATE AS ApprovedDate," +
                     "WG_STATE AS State,WG_TEARFLAG AS Split ,WG_CAVFLAG AS Articulation,WG_ROB AS Colour FROM View_SM_IN WHERE WG_CODE='" + code + "'";

                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    LabelState.Text = dr["State"].ToString();
                    InputColour.Value = dr["Colour"].ToString();
                    LabelCode.Text = dr["Code"].ToString();
                    TextBoxDate.Text = dr["Date"].ToString();

                    //获取制单时的系统关账时间
                    ClosingAccountDate(TextBoxDate.Text.Trim());

                    LabelSupplier.Text = dr["Supplier"].ToString();
                    InputSupplierCode.Value = dr["SupplierCode"].ToString();
                    try { DropDownListWarehouse.Items.FindByValue(dr["WarehouseCode"].ToString()).Selected = true; }
                    catch { }
                    TextBoxAbstract.Text = dr["Abstract"].ToString();
                    TextBoxHDBH.Text = dr["HDBH"].ToString();
                  
                    LabelDoc.Text = dr["Doc"].ToString();
                    LabelDocCode.Text = dr["DocCode"].ToString();
                    try { DropDownListAcceptance.Items.FindByValue(dr["AcceptanceCode"].ToString()).Selected = true; }
                    catch { }
                    LabelVerifier.Text = dr["Verifier"].ToString();
                    LabelVerifierCode.Text = dr["VerifierCode"].ToString();
                    LabelApproveDate.Text = dr["ApprovedDate"].ToString();
                }
                dr.Close();

                sql = "SELECT WG_UNIQUEID AS UniqueID,WG_MARID AS MaterialCode,MNAME AS MaterialName," +
                    "CAIZHI AS Attribute,GB AS GB,GUIGE AS MaterialStandard,WG_FIXEDSIZE AS Fixed,WG_LENGTH AS Length,WG_WIDTH AS Width," +
                    "WG_LOTNUM AS LotNumber,CGDW AS Unit,WG_RSNUM AS RN,WG_RSFZNUM AS Quantity," +
                    "WG_UPRICE AS UnitPrice,WG_TAXRATE AS TaxRate,WG_CTAXUPRICE AS CTUP," +
                    "WG_AMOUNT AS Amount,WG_CTAMTMNY AS CTA,WG_WAREHOUSE AS WarehouseCode," +
                    "WS_NAME AS Warehouse,WG_LOCATION AS PositionCode,WL_NAME AS Position," +
                    "WG_ORDERID AS OrderCode,WG_PMODE AS PlanMode,WG_PTCODE AS PTC,WG_NOTE AS Comment," +
                    "WG_STATE AS State FROM  View_SM_IN WHERE WG_CODE='" + code + "'";

                DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = tb;
                GridView1.DataBind();
                if (InputColour.Value == "1")
                {
                    ImageRed.Visible = true;
                    DropDownListWarehouse.Enabled = false;
                    DropDownListAcceptance.Enabled = false;
                }
            }
            #endregion


            //入库单查看载入

            //反审核---只是提供查看的按钮

            if (flag == "READ")
            {

                string sql = "SELECT WG_CODE AS Code,Supplier AS SupplierCode,SupplierName AS Supplier," +
                   "WG_WAREHOUSE AS WarehouseCode,WS_NAME AS Warehouse,WG_LOCATION AS PositionCode,WG_ABSTRACT AS Abstract,WG_HDBH AS HDBH,WG_DATE AS Date," +
                   "Dep AS DepCode,DepName AS Dep,Clerk AS ClerkCode,ClerkName AS Clerk," +
                   "WG_DOC AS DocCode,DocName AS Doc,WG_REVEICER AS AcceptanceCode,ReveicerName AS Acceptance," +
                   "WG_VERIFIER AS VerifierCode,VerfierName AS Verifier,left(WG_VERIFYDATE,10) AS ApprovedDate," +
                   "WG_STATE AS State,WG_TEARFLAG AS Split ,WG_CAVFLAG AS Articulation,WG_ROB AS Colour,WG_GJSTATE as gj FROM View_SM_IN WHERE WG_CODE='" + code + "'";

                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);

                if (dr.Read())
                {
                    LabelState.Text = dr["State"].ToString();//状态

                    LabelGJState.Text = dr["gj"].ToString();//勾稽状态

                    LabelHXState.Text = dr["Articulation"].ToString();//勾稽状态

                    InputColour.Value = dr["Colour"].ToString();
                    LabelCode.Text = dr["Code"].ToString();//入库单号
                    TextBoxDate.Text = dr["Date"].ToString();//日期

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
                 
                    LabelDoc.Text = dr["Doc"].ToString();
                    LabelDocCode.Text = dr["DocCode"].ToString();
                    try { DropDownListAcceptance.Items.FindByValue(dr["AcceptanceCode"].ToString()).Selected = true; }
                    catch { }
                    LabelVerifier.Text = dr["Verifier"].ToString();
                    LabelVerifierCode.Text = dr["VerifierCode"].ToString();
                    LabelApproveDate.Text = dr["ApprovedDate"].ToString();
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
                    "WG_STATE AS State,WG_CGMODE AS CGMODE FROM View_SM_IN WHERE WG_CODE='" + code + "' order by WG_MARID";

                DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = tb;
                GridView1.DataBind();
                //Append.Enabled = false;//追加不可用
                //Split.Enabled = false;//拆分不可用
                //Delete.Enabled = false;//删除不可用
                Save.Enabled = false;//保存不可用
                Verify.Enabled = false;//审核不可用

                if (LabelState.Text == "2")
                {
                    //审核图像可见
                    ImageVerify.Visible = true;

                   
                    //bindControlEnable(true);
                    if (Session["UserID"].ToString() == LabelVerifierCode.Text.Trim())
                    {
                        bindControlEnable(true);
                    }
                    else
                    {
                        FS.Enabled = false;
                       
                        DelForm.Enabled = false;

                    }

                }
                else
                {
                    bindControlEnable(false);
                }

                if (InputColour.Value == "1")
                {
                    //红字可见
                    ImageRed.Visible = true;
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
                int tempnum = Convert.ToInt32((tempstr.Substring(tempstr.IndexOf('R', 0) + 1, tempstr.Length - tempstr.IndexOf('R', 0) - 1)));
                tempnum++;
                tempstr = tempstr.Substring(0, tempstr.IndexOf('R', 0) + 1) + tempnum.ToString();
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
            string sql = "SELECT orderno AS OrderCode,supplierid AS SupplierCode,suppliernm AS Supplier," +
                   "ywyid AS ClerkCode,ywynm AS Clerk,depid AS DepCode,depnm AS Dep," +
                   "marid AS MaterialCode,marnm AS MaterialName,margg AS MaterialStandard," +
                   "fixed AS Fixed,CAST(length AS VARCHAR(50)) AS Length,CAST(width AS VARCHAR(50)) AS Width," +
                   "marcz AS Attribute,margb AS GB,marunit AS Unit,CAST(cast((num-recgdnum) as float) AS VARCHAR(50)) AS RN,CAST(0 AS VARCHAR(50)) AS Quantity,CAST(price AS VARCHAR(50)) AS UnitPrice,CAST(ctprice AS VARCHAR(50)) AS CTUP,CAST((num-recgdnum)*price AS VARCHAR(50)) AS Amount," +
                   "CAST(taxrate AS VARCHAR(50)) AS TaxRate,CAST((num-recgdnum)*ctprice AS VARCHAR(50)) AS CTA,detailnote AS Comment,planmode AS PlanMode,ptcode AS PTC," +
                   "totalstate AS State,UniqueID='',LotNumber='',WarehouseCode='0',Warehouse='--请选择--',PositionCode='0',Position='待查',PO_CGFS AS CGMODE FROM View_TBPC_PURORDERDETAIL_PLAN_TOTAL WHERE whstate='APPENDIN" + Session["UserID"].ToString() + "'";

            dt.Merge(DBCallCommon.GetDTUsingSqlText(sql));
            GridView1.DataSource = dt;
            GridView1.DataBind();
            sql = "UPDATE TBPC_PURORDERDETAIL SET PO_WHSTATE='1' WHERE PO_WHSTATE='APPENDIN" + Session["UserID"].ToString() + "'";
            DBCallCommon.ExeSqlText(sql);
            LabelMessage.Text = "追加成功！";
        }


        /*
         * 这里不需要修改
         */

     
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

        //保存当前入库单
        protected void Save_Click(object sender, EventArgs e)
        {


            /*
             * 保存的时候是修改库存的数量的
             * 也不汇总库存的数量
             */

            //此处是保存操作
            List<string> sqllist = new List<string>();
            string sql = "";

            string Code = LabelCode.Text;
            string Date = TextBoxDate.Text;//制单年月日
            string Supplier = LabelSupplier.Text;
            string SupplierCode = InputSupplierCode.Value;
            string Warehouse = DropDownListWarehouse.SelectedItem.Text;
            string WarehouseCode = DropDownListWarehouse.SelectedValue;
            string Abstract = TextBoxAbstract.Text;
            string HDBH = TextBoxHDBH.Text;
            string Dep = "";
            string DepCode = "";
            string Clerk = "";
            string ClerkCode = "";
            string Document = LabelDoc.Text;
            string DocumentCode = LabelDocCode.Text;
            string Acceptance = DropDownListAcceptance.SelectedItem.Text;
            string AcceptanceCode = DropDownListAcceptance.SelectedValue;
            string Colour = InputColour.Value;

            sql = "DELETE FROM TBWS_IN WHERE WG_CODE='" + Code + "'";
            sqllist.Add(sql);

            sql = "exec ResetSeed @tablename=TBWS_IN";
            sqllist.Add(sql);


            if (Code.Contains('B'))
            {
                sql = "INSERT INTO TBWS_IN ( WG_CODE,WG_WAREHOUSE,WG_ABSTRACT,WG_DATE," +
                "WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG," +
                "WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_ZGFLAG,WG_HSFLAG,WG_ACTPERIOD,WG_BILLTYPE,WG_HDBH) VALUES('" + Code + "','" + WarehouseCode + "','" + Abstract + "','" + Date + "','" +
                DocumentCode + "','','','','1','0','0','0','0','0','0','','1','" + HDBH + "')";
            }
            else
            {
                sql = "INSERT INTO TBWS_IN ( WG_CODE,WG_WAREHOUSE," +
                "WG_ABSTRACT,WG_DATE," +
                "WG_DOC," +
                "WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG,WG_CAVFLAG," +
                "WG_ROB,WG_GJSTATE,WG_HDBH) VALUES('" + Code + "','" + WarehouseCode + "','" + Abstract + "','" + Date + "','" +
                 DocumentCode + "','" + AcceptanceCode + "','','','1','0','0','" +
                Colour + "','0','" + HDBH + "')";
            }



            sqllist.Add(sql);

            sql = "DELETE FROM TBWS_INDETAIL WHERE WG_CODE='" + Code + "'";
            sqllist.Add(sql);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string UniqueID = "";
                if (Colour == "0")
                {
                    UniqueID = Code + (i + 1).ToString();
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
                string LotNumber = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLotNumber")).Text;
                string Unit = ((Label)this.GridView1.Rows[i].FindControl("LabelUnit")).Text;
                float RN = 0;
                try
                {
                    //蓝红的数量
                    float temp = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxRN")).Text);
                    if ((Colour == "0") && (temp < 0))
                    { RN = -temp; }
                    else if ((Colour == "1") && (temp > 0))
                    { RN = -temp; }
                    else
                    { RN = temp; }
                }
                catch { RN = 0; }

                float Quantity = 0;
                try
                {
                    //蓝红总重
                    float temp = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxQuantity")).Text);
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

                sql =
                    "INSERT INTO TBWS_INDETAIL ( WG_UNIQUEID,WG_CODE,WG_MARID,WG_LOTNUM," +
                    "WG_FIXEDSIZE,WG_LENGTH,WG_WIDTH,WG_RSNUM,WG_RSFZNUM,WG_UPRICE,WG_TAXRATE," +
                    "WG_CTAXUPRICE,WG_AMOUNT,WG_CTAMTMNY,WG_LOCATION," +
                    "WG_ORDERID,WG_PMODE,WG_PTCODE,WG_NOTE,WG_STATE,WG_CGMODE) VALUES('" + UniqueID +
                    "','" + Code + "','" + MaterialCode + "','" + LotNumber + "','"
                    + Fixed + "','" + Length + "','" + Width + "','" + RN + "','" + Quantity + "','" + UnitPrice + "','" +
                    TaxRate + "','" + CTUP + "','" + Amount + "','" + CTA + "','" +
                    PositionCode + "','" + OrderCode + "','" +
                    PlanMode + "','" + PTC + "','" + Comment + "','','" + CGMode + "')";

                sqllist.Add(sql);
            }
            DBCallCommon.ExecuteTrans(sqllist);
            sqllist.Clear();
            LabelState.Text = "1";
            LabelMessage.Text = "保存成功";
        }

        //此处调用了入库库存调整存储过程，需要严格测试
        protected void Verify_Click(object sender, EventArgs e)
        {

            //此处是审核前保存操作
            List<string> sqllist = new List<string>();
            string sql = "";

            string Code = LabelCode.Text;
            string Date = TextBoxDate.Text;
            string Supplier = LabelSupplier.Text;
            string SupplierCode = InputSupplierCode.Value;
            string Warehouse = DropDownListWarehouse.SelectedItem.Text;
            string WarehouseCode = DropDownListWarehouse.SelectedValue;
            string Abstract = TextBoxAbstract.Text;
            string HDBH = TextBoxHDBH.Text;
            string Dep = "";
            string DepCode = "";
            string Clerk = "";
            string ClerkCode = "";
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

            if (LabelClosingAccount.Text == "NoTime")
            {
                //无核算
                ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                //核算
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
                                ApproveDate = getNextMonth() + " 07:59:59";
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
                        }
                    }
                    else
                    {
                        //第一次审核
                        ApproveDate = getNextMonth() + " 07:59:59";
                    }
                }

            }

            sql = "DELETE FROM TBWS_IN WHERE WG_CODE='" + Code + "'";
            sqllist.Add(sql);

            //重置标识符
            sql = "exec ResetSeed @tablename=TBWS_IN";
            sqllist.Add(sql);

            //在存储过程中修改状态
            if (Code.Contains('B'))
            {

                sql = "INSERT INTO TBWS_IN(WG_CODE,WG_WAREHOUSE,WG_ABSTRACT,WG_DATE," +
                "WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG," +
                "WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_ZGFLAG,WG_HSFLAG,WG_ACTPERIOD,WG_BILLTYPE,WG_HDBH,WG_RealTime) VALUES('" + Code + "','" + WarehouseCode + "','" + Abstract + "','" + Date + "','" +
                DocumentCode + "','" + AcceptanceCode + "','" + VerifierCode + "','" + ApproveDate + "','1','0','0','0','0','0','0','','1','" + HDBH + "',convert(varchar(50),getdate(),120))";

            }
            else
            {

                sql = "INSERT INTO TBWS_IN (WG_CODE,WG_WAREHOUSE," +
                "WG_ABSTRACT,WG_DATE," +
                "WG_DOC," +
                "WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG,WG_CAVFLAG," +
                "WG_ROB,WG_GJSTATE,WG_HDBH,WG_RealTime) VALUES('" + Code + "','" + WarehouseCode + "','" + Abstract + "','" + Date + "','" +
                 DocumentCode + "','" + AcceptanceCode + "','" + VerifierCode + "','" + ApproveDate + "','1','0','0','" +
                Colour + "','0','" + HDBH + "',convert(varchar(50),getdate(),120))";
            }

            //sql = "INSERT INTO TBWS_IN (WG_CODE,WG_WAREHOUSE," +
            //   "WG_ABSTRACT,WG_DATE," +
            //   "WG_DOC," +
            //   "WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG,WG_CAVFLAG," +
            //   "WG_ROB,WG_GJSTATE,WG_HDBH) VALUES('" + Code + "','" + WarehouseCode + "','" + Abstract + "','" + Date + "','" +
            //    DocumentCode + "','" + AcceptanceCode + "','" + VerifierCode + "','" + ApproveDate + "','1','0','0','" +
            //   Colour + "','0','"+HDBH+"')";

            sqllist.Add(sql);

            sql = "DELETE FROM TBWS_INDETAIL WHERE WG_CODE='" + Code + "'";
            sqllist.Add(sql);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string UniqueID = Code + (i + 1).ToString();
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
                string LotNumber = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLotNumber")).Text;
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
                string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;

                string OrderCode = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderCode")).Text;
                string PlanMode = ((Label)this.GridView1.Rows[i].FindControl("LabelPlanMode")).Text;

                string CGMode = ((Label)this.GridView1.Rows[i].FindControl("LabelCGMode")).Text;

                string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;

                /*
                 * 明细表中的数据
                 */
                sql =
                     "INSERT INTO TBWS_INDETAIL ( WG_UNIQUEID,WG_CODE,WG_MARID,WG_LOTNUM," +
                     "WG_FIXEDSIZE,WG_LENGTH,WG_WIDTH,WG_RSNUM,WG_RSFZNUM,WG_UPRICE,WG_TAXRATE," +
                     "WG_CTAXUPRICE,WG_AMOUNT,WG_CTAMTMNY,WG_LOCATION," +
                     "WG_ORDERID,WG_PMODE,WG_PTCODE,WG_NOTE,WG_STATE,WG_CGMODE) VALUES('" + UniqueID +
                     "','" + Code + "','" + MaterialCode + "','" + LotNumber + "','"
                     + Fixed + "','" + Length + "','" + Width + "','" + RN + "','" + Quantity + "','" + UnitPrice + "','" +
                     TaxRate + "','" + CTUP + "','" + Amount + "','" + CTA + "','" +
                     PositionCode + "','" + OrderCode + "','" +
                     PlanMode + "','" + PTC + "','" + Comment + "','','" + CGMode + "')";
                sqllist.Add(sql);
            }
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
        }

        protected void FS_Click(object sender, EventArgs e)
        {
            //string Code = "";

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
            string Code = LabelCode.Text.Trim();
            /*
             * 
             * 调用存储过程
             * 
             */
            string sql = DBCallCommon.GetStringValue("connectionStrings");
            SqlConnection con = new SqlConnection(sql);
            con.Open();
            SqlCommand cmd = new SqlCommand("AntiVerifyIn", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@InCode", SqlDbType.VarChar, 50);				//增加参数入库单号@InCode
            cmd.Parameters["@InCode"].Value = Code;						                //为参数初始化
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
            /*
             * 这地方由于出库单关联不上入库单，所以不知道入库单是否出过物料
             */
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
            {
                //物料不存在，也就是物料出完了

                LabelMessage.Text = "反审核未通过：入库物料发生后续操作！";

                string script = @"alert('反审核未通过：入库物料发生后续操作！');";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
            {
                //物料已出了一部分

                LabelMessage.Text = "反审核未通过：部分入库物料发生后续操作！";

                string script = @"alert('反审核未通过：部分入库物料发生后续操作！');";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
            }
            /*
             * 这三个可以不用执行存储过程，可以根据页面状态来减少页面回发
             * 
             */
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -1)
            {
                LabelMessage.Text = "反审核未通过：未审核入库单不允许反审！";

                string script = @"alert('反审核未通过：未审核入库单不允许反审！');";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -2)
            {
                LabelMessage.Text = "反审核未通过：正在进行勾稽的入库单不允许反审！";

                string script = @"alert('反审核未通过：正在进行勾稽的入库单不允许反审！');";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -3)
            {
                LabelMessage.Text = "反审核未通过：已核销的入库单不允许反审！";

                string script = @"alert('反审核未通过：已核销的入库单不允许反审！');";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
            }
            /*
             * 跨月是不能反审核的，由于跨越的它已经暂估（核算），即已经入帐，故不能反审
             */
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -4)
            {
                LabelMessage.Text = "反审核未通过：跨月的入库单不允许反审！";

                string script = @"alert('反审核未通过：跨月的入库单不允许反审！');";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
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
                    string gjstate = LabelGJState.Text;
                    string hxstate = LabelHXState.Text;
                    if (gjstate != "0")
                    {
                        LabelMessage.Text = "正在勾稽的入库单无法拆分！";
                        return;
                    }
                    if (hxstate != "0")
                    {
                        LabelMessage.Text = "正在核销的入库单无法拆分！";
                        return;
                    }

                    if (incode.Contains("R") == true)
                    {
                        LabelMessage.Text = "红字入库单不允许拆分！";
                        return;
                    }
                    else if (incode.Contains("S") == true)
                    {
                        LabelMessage.Text = "子入库单不允许拆分！";
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
                    string pcode = code.Substring(0, code.IndexOf("S", 0));
                    Response.Redirect("SM_WarehouseIn_ActionResult.aspx?IDP=" + pcode + "&&IDC=" + code + "&&RES=M");
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
                LabelMessage.Text = "请选择要推红的条目！";
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


        //删单

        protected void DelForm_Click(object sender, EventArgs e)
        {

            string sql = string.Empty;

            List<string> temp = new List<string>();

            string code = LabelCode.Text.Trim();

            sql = "DELETE FROM TBWS_IN WHERE WG_CODE='" + code + "'";

            temp.Add(sql);

            sql = "DELETE FROM TBWS_INDETAIL WHERE WG_CODE='" + code + "'";

            temp.Add(sql);

            DBCallCommon.ExecuteTrans(temp);

            //LabelMessage.Text = "删除成功";

            string script = @"closewin();";

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);
            //Response.Write("<script type='text/javascript' language='javascript' >window.close();</script>");
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


    }
}
