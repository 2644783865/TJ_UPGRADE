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
    public partial class SM_WarehouseOUT_Red : System.Web.UI.Page
    {
        int count = 0;

        string flag = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["FLAG"]!=null)
                flag = Request.QueryString["FLAG"];
            if (!IsPostBack)
            {

                GetDep();//部门

                GetBZ();//制作班组

                getWarehouse();//仓库

                GetStaff();//收料员

                if (flag == "PUSHRED")
                {
                    InitGridview();

                    InitInfo();

                    btnantiverity.Enabled = false;
                    dj_delete.Enabled = false;
                    btnPrint.Disabled = true;

                   
                }
            }
        }

        //获取系统封账时间
        private void ClosingAccountDate(string ZDDate)
        {
            string NowDate = ZDDate;
            //string NowDate = "20111030";
            string sql = "select HS_TIME from TBFM_HSTOTAL where HS_YEAR='" + NowDate.Substring(0, 4) + "' and HS_MONTH='" + NowDate.Substring(5, 2) + "'";
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

        //绑定初始化信息

        private void bindInitInfo()
        {
            string outcode = "";
            if (Request.QueryString["ID"] != null)
              outcode = Request.QueryString["ID"];

            /*总表信息*/

              string sql = "SELECT Top 1 OutCode AS Code,DepCode AS LLDepCode,Dep AS LLDep,Date AS Date," +
                "TSAID as SCZH,WarehouseCode AS WarehouseCode,SenderCode AS SenderCode," +
                "Sender AS Sender,DocCode AS DocumentCode," +
                "Doc AS Document,VerifierCode AS VerifierCode," +
                "Verifier AS Verifier,ApprovedDate AS ApproveDate,ROB AS Colour,TotalState AS State," +
                "TotalNote AS Comment FROM View_SM_OUT WHERE OutCode='" + outcode + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    LabelCode.Visible = true;

                    LabelCode.Text = dr["Code"].ToString();//入库单号

                    LabelState.Text = dr["State"].ToString();//是否审核

                    InputColour.Value = dr["Colour"].ToString();//红蓝字

                    TextBoxDate.Text = dr["Date"].ToString();//日期

                    try { DropDownListDep.Items.FindByValue(dr["LLDepCode"].ToString()).Selected = true; }
                    catch { }
                    //try { DropDownListSCZH.Items.FindByValue(dr["SCZH"].ToString()).Selected = true; }
                    //catch { }
                    TextBoxSCZH.Text = dr["SCZH"].ToString();

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

                    //制作班组
                    try { DropDownListBZ.Items.FindByValue(dr["Comment"].ToString()).Selected = true; }
                    catch { }

                    //TextBoxComment.Text = dr["Comment"].ToString();

                    LabelDoc.Text = dr["Document"].ToString();
                    LabelDocCode.Text = dr["DocumentCode"].ToString();
                    try { DropDownListSender.Items.FindByValue(dr["SenderCode"].ToString()).Selected = true; }
                    catch { }

                    LabelVerifier.Text = dr["Verifier"].ToString();

                    LabelVerifierCode.Text = dr["VerifierCode"].ToString();

                    LabelApproveDate.Text = dr["ApproveDate"].ToString();

                    //仓库，仓位
                }
                dr.Close();

             /*详细表信息*/

                sql = "SELECT UniqueCode AS UniqueID,SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                    "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,Length AS Length,Width AS Width," +
                    "LotNumber AS LotNumber,Unit AS Unit,DueNumber AS DN,RealNumber AS RN,DueSupportNumber AS DQN,RealSupportNumber AS RQN," +
                    "UnitPrice AS UnitPrice,Amount AS Amount,WarehouseCode AS WarehouseCode," +
                    "Warehouse AS Warehouse,LocationCode AS PositionCode,Location AS Position," +
                    "PlanMode AS PlanMode,PTC AS PTC,OrderCode AS OrderID,DetailNote AS Comment " +
                    "FROM View_SM_OUT WHERE OutCode='" + outcode + "'";

                DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = tb;
                GridView1.DataBind();

                if ( InputColour.Value == "1")
                {
                    ImageRed.Visible = true;
                }

                if (LabelState.Text == "1")
                {
                    btnsave.Enabled = true;
                    btnverity.Enabled = true;
                    btnantiverity.Enabled = false;
                    dj_delete.Visible = true;//单据删除
                    btninsert.Visible = true;
                    btnappend.Visible = true;
                    btndelete.Visible = true;
                    btnPrint.Visible = false;

                }
                if (LabelState.Text == "2")
                {
                    ImageVerify.Visible = true;
                    btnsave.Enabled = false;
                    btnverity.Enabled = false;
                    btnantiverity.Enabled = true;
                    dj_delete.Visible = false;//单据删除
                    btninsert.Visible = false;
                    btnappend.Visible = false;
                    btndelete.Visible = false;
                }

               
        }


        private void InitInfo()
        {
            //初始化单号
            LabelCode.Text = generateRedCode();

            TextBoxDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

            ClosingAccountDate(TextBoxDate.Text.Trim());

            //制单人
            LabelDoc.Text = Session["UserName"].ToString();

            LabelDocCode.Text = Session["UserID"].ToString();

            //try { DropDownListSender.Items.FindByValue(LabelDocCode.Text.Trim()).Selected = true; }
            //catch { }  //制单人审核人与收料人不能是同一个人

            BindOriForm();

        }

        //绑定原始单据数据
        private void BindOriForm()
        {
            string Code = Request.QueryString["ID"].ToString();

            string sql = "SELECT OP_TSAID,OP_DEP,OP_NOTE FROM TBWS_OUT WHERE OP_CODE='" + Code + "'";
            DataTable dt=DBCallCommon.GetDTUsingSqlText(sql);
            if(dt.Rows.Count>0)
            {
                TextBoxSCZH.Text = dt.Rows[0]["OP_TSAID"].ToString();
                DropDownListDep.ClearSelection();

                try { DropDownListDep.Items.FindByValue(dt.Rows[0]["OP_DEP"].ToString()).Selected = true; }
                catch { }

                try { DropDownListBZ.Items.FindByValue(dt.Rows[0]["OP_NOTE"].ToString()).Selected = true; }
                catch { }

            }
        }





        //产生退库单号
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



        //发料人
        protected void GetStaff()
        {
            string sql = "SELECT DISTINCT ST_CODE,ST_NAME FROM TBDS_STAFFINFO WHERE ST_DEPID='07' AND ST_CODE<>'" + Session["UserID"].ToString() + "' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListSender.DataValueField = "ST_CODE";
            DropDownListSender.DataTextField = "ST_NAME";
            DropDownListSender.DataSource = dt;
            DropDownListSender.DataBind();
        }



        //得到部门

        protected void GetDep()
        {
            string sql = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO where DEP_FATHERID='04' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListDep.DataValueField = "DEP_CODE";
            DropDownListDep.DataTextField = "DEP_NAME";
            DropDownListDep.DataSource = dt;
            DropDownListDep.DataBind();
        }


        protected void GetBZ()
        {
            string sql = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO WHERE DEP_FATHERID='04' AND (DEP_BZYN='1' OR DEP_BZYN='2')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListBZ.DataValueField = "DEP_CODE";
            DropDownListBZ.DataTextField = "DEP_NAME";
            DropDownListBZ.DataSource = dt;
            DropDownListBZ.DataBind();
            ListItem item = new ListItem("--请选择--", "0");
            DropDownListBZ.Items.Insert(0, item);
        }

        ////得到生产制号
        //protected void GetTSAID()
        //{
        //    string sql = "SELECT DISTINCT a.TSA_ID AS SCZH,a.TSA_ID+'|'+b.PJ_NAME+a.TSA_ENGNAME AS ZHNM " +
        //        "FROM TBPM_TCTSASSGN a INNER JOIN TBPM_PJINFO b ON a.TSA_PJID=b.PJ_ID " +
        //        "WHERE CHARINDEX('-',a.TSA_ID)>0";
        //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
        //    DropDownListSCZH.DataValueField = "SCZH";
        //    DropDownListSCZH.DataTextField = "ZHNM";
        //    DropDownListSCZH.DataSource = dt;
        //    DropDownListSCZH.DataBind();
        //}

        //仓库
        protected void getWarehouse()
        {
            string sql = "SELECT DISTINCT WS_ID,WS_NAME FROM TBWS_WAREHOUSE WHERE WS_FATHERID<>'ROOT'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListWarehouse.DataTextField = "WS_NAME";
            DropDownListWarehouse.DataValueField = "WS_ID";
            DropDownListWarehouse.DataSource = dt;
            DropDownListWarehouse.DataBind();
        }
      

        protected void DropDownListWarehouse_SelecedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((TextBox)GridView1.Rows[i].FindControl("marid")).Text.Trim() != string.Empty)
                {

                    HtmlInputText ht = (HtmlInputText)GridView1.Rows[i].FindControl("ck");

                    ht.Value = DropDownListWarehouse.SelectedItem.Text;

                    HtmlInputText lb_code = (HtmlInputText)GridView1.Rows[i].FindControl("InputWarehouseCode");

                    lb_code.Value = DropDownListWarehouse.SelectedValue;

                    /*
                     * 默认仓位为待查
                     */

                    HtmlInputText ht_position = (HtmlInputText)GridView1.Rows[i].FindControl("cw");

                    ht_position.Value = "待查";

                    HtmlInputText ht_position_code = (HtmlInputText)GridView1.Rows[i].FindControl("InputPositionCode");

                    ht_position_code.Value = "0";
                }
            }

            getWarehousePosition();
        }

        protected void DropDownListPosition_SelecedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (((TextBox)GridView1.Rows[i].FindControl("marid")).Text.Trim() != string.Empty)
                {

                    HtmlInputText ht_position = (HtmlInputText)GridView1.Rows[i].FindControl("cw");

                    ht_position.Value = DropDownListPosition.SelectedItem.Text;

                    HtmlInputText ht_position_code = (HtmlInputText)GridView1.Rows[i].FindControl("InputPositionCode");

                    ht_position_code.Value = DropDownListPosition.SelectedValue;
                }
            }
        }



        //仓位
        protected void getWarehousePosition()
        {
            string sql = "SELECT DISTINCT WL_ID,WL_NAME FROM TBWS_LOCATION WHERE WL_WSID='" + DropDownListWarehouse.SelectedValue + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListPosition.DataSource = dt;
            DropDownListPosition.DataTextField = "WL_NAME";
            DropDownListPosition.DataValueField = "WL_ID";
            DropDownListPosition.DataBind();
        }





        /// <summary>
        /// 初始化Gridview
        /// </summary>
        private void InitGridview()
        {
            DataTable dt = this.GetDataFromGrid(true);

            GridView1.DataSource = dt;

            GridView1.DataBind();
        }
        /// <summary>
        /// 初始化表格
        /// </summary>
        /// <param name="isInit">是否是初始化</param>
        /// <returns></returns>
        protected DataTable GetDataFromGrid(bool isInit)
        {
            DataTable dt = new DataTable();

            #region

            dt.Columns.Add("MaterialCode");

            dt.Columns.Add("MaterialName");

            dt.Columns.Add("MaterialStandard");

            dt.Columns.Add("Attribute");

            dt.Columns.Add("GB");

            dt.Columns.Add("LotNumber");//批号

            dt.Columns.Add("Unit");

            dt.Columns.Add("RN");//实发重量==钢板=重量。标准间==个

            dt.Columns.Add("Length");

            dt.Columns.Add("Width");

            dt.Columns.Add("RQN");

            dt.Columns.Add("PTC");//计划跟踪号

            dt.Columns.Add("Comment");

            dt.Columns.Add("Warehouse");//仓库

            dt.Columns.Add("WarehouseCode");//仓库编号

            dt.Columns.Add("Position");//仓位

            dt.Columns.Add("PositionCode");//仓位编号

            dt.Columns.Add("UnitPrice");//单价

            dt.Columns.Add("Amount");//金额

            #endregion

            if (!isInit)
            {

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];

                    DataRow newRow = dt.NewRow();

                   
                    newRow["MaterialCode"] = ((TextBox)gRow.FindControl("marid")).Text;
                    newRow["MaterialName"] = ((HtmlInputText)gRow.FindControl("marnm")).Value;
                    newRow["MaterialStandard"] = ((HtmlInputText)gRow.FindControl("guige")).Value;
                    newRow["Attribute"] = ((HtmlInputText)gRow.FindControl("caizhi")).Value;
                    newRow["GB"] = ((HtmlInputText)gRow.FindControl("gb")).Value;
                    newRow["LotNumber"] = ((HtmlInputText)gRow.FindControl("ph")).Value;
                    newRow["Unit"] = ((HtmlInputText)gRow.FindControl("unit")).Value;
                    newRow["RN"] = ((HtmlInputText)gRow.FindControl("num")).Value;
                    newRow["Length"] = ((HtmlInputText)gRow.FindControl("length")).Value;
                    newRow["Width"] = ((HtmlInputText)gRow.FindControl("width")).Value;
                    newRow["RQN"] = ((HtmlInputText)gRow.FindControl("zhang")).Value;
                    newRow["PTC"] = ((HtmlInputText)gRow.FindControl("pt")).Value;//计划跟踪号
                    newRow["Comment"] = ((HtmlInputText)gRow.FindControl("beizhu")).Value;

                    newRow["Warehouse"] = ((HtmlInputText)gRow.FindControl("ck")).Value;

                    newRow["WarehouseCode"] = ((HtmlInputText)gRow.FindControl("InputWarehouseCode")).Value;

                    newRow["Position"] = ((HtmlInputText)gRow.FindControl("cw")).Value;//仓位名称

                    newRow["PositionCode"] = ((HtmlInputText)gRow.FindControl("InputPositionCode")).Value;//仓位编号

                    newRow["UnitPrice"] = ((HtmlInputText)gRow.FindControl("dj")).Value;

                    newRow["Amount"] = ((HtmlInputText)gRow.FindControl("je")).Value;

                    dt.Rows.Add(newRow);
                }
            }

            if (isInit)
            {
                for (int i = GridView1.Rows.Count; i < 10; i++)
                {
                    DataRow newRow = dt.NewRow();

                    newRow[7] = 0;
                    newRow[8] = 0;
                    newRow[9] = 0;
                    newRow[10] = 0;

                    dt.Rows.Add(newRow);
                }
            }

            dt.AcceptChanges();

            return dt;
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btninsert_Click(object sender, EventArgs e)
        {
           
                DataTable dt = this.GetDataFromGrid(false);

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];

                    CheckBox chk = (CheckBox)gRow.FindControl("CheckBox1");

                    if (chk.Checked)
                    {
                        DataRow newRow = dt.NewRow();

                        newRow[7] = 0;

                        newRow[8] = 0;

                        newRow[9] = 0;

                        newRow[10] = 0;

                        dt.Rows.InsertAt(newRow, i + 1);

                        count++;

                    }
            }
            this.GridView1.DataSource = dt;

            this.GridView1.DataBind();
        }

        /// <summary>
        /// 追加数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnadd_Click(object sender, EventArgs e)
        {
           CreateNewRow();

           //string script = @"alert('追加数据成功!');";
           //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);
        }
        //生成输入行函数
        private void CreateNewRow()
        {
            DataTable dt = this.GetDataFromGrid(false);

            DataRow newRow = dt.NewRow();

            newRow[7] = 0;
            newRow[8] = 0;
            newRow[9] = 0;
            newRow[10] = 0;

            dt.Rows.Add(newRow);

            this.GridView1.DataSource = dt;

            this.GridView1.DataBind();
        }

        /********************删除行******************************/

        protected void btndelete_Click(object sender, EventArgs e)
        {
            int count = 0;

            DataTable dt = this.GetDataFromGrid(false);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];

                CheckBox chk = (CheckBox)gr.FindControl("CheckBox1");

                if (chk.Checked)
                {
                    
                    dt.Rows.RemoveAt(i - count);
                    /**/
                    //删一行，插一行
                    //DataRow newRow = dt.NewRow();
                    //dt.Rows.InsertAt(newRow,dt.Rows.Count-1);
                    /*******************/
                    count++;
                }
            }

            //删除之后,如果少于10行，则需要增加行数
            for (int i = dt.Rows.Count; i < 10; i++)
            {
                DataRow newRow = dt.NewRow();

                newRow[7] = 0;
                newRow[8] = 0;
                newRow[9] = 0;
                newRow[10] = 0;

                dt.Rows.Add(newRow);
            }



            this.GridView1.DataSource = dt;

            this.GridView1.DataBind();

        }



        //保存操作

        protected void Save_Click(object sender, EventArgs e)
        {
            //此处是保存操作
            List<string> sqllist = new List<string>();

            string sql = "";

            string Code =LabelCode.Text;//单号
           
            string Date = TextBoxDate.Text;//日期

            string LLDepCode = DropDownListDep.SelectedValue;//领料部门

            string SCZH = TextBoxSCZH.Text;//生产制号

            string Comment = DropDownListBZ.SelectedValue;//制作班组

            string DocCode = LabelDocCode.Text;//制单单号

            string SendClerkCode = DropDownListSender.SelectedValue;//发料人编号

            string VerifierCode = LabelVerifierCode.Text;//审核人编号

            string ApproveDate = LabelApproveDate.Text;//审核日期

            string BillType = string.Empty;
            if (LabelCode.Text.Contains("QT"))
            {
                BillType = "6"; //其他出库中的特殊退库
            }
            else
            {
                BillType = "4";
            }

            //sql = "DELETE FROM TBWS_OUT WHERE OP_CODE='" + Code + "'";

            //sqllist.Add(sql);

            //sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_DEP,OP_DATE,OP_TSAID," +
            //    "OP_SENDER,OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
            //    "OP_STATE,OP_NOTE,OP_BILLTYPE) VALUES('" + Code + "','" + LLDepCode + "','" + Date + "','" + SCZH + "','" +
            //    SendClerkCode + "','" + DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','1','1','" + Comment + "','4')";
            //sqllist.Add(sql);

            
            sql = "select count(*) from TBWS_OUT where OP_CODE='" + Code + "'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            if (dt.Rows[0][0].ToString() == "0")
            {
                sql = "exec ResetSeed @tablename=TBWS_OUT";
                sqllist.Add(sql);

                sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_DEP,OP_DATE,OP_TSAID," +
                    "OP_SENDER,OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
                    "OP_STATE,OP_NOTE,OP_BILLTYPE) VALUES('" + Code + "','" + LLDepCode + "','" + Date + "','" + SCZH + "','" +
                    SendClerkCode + "','" + DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','1','1','" + Comment + "','"+BillType+"')";
                sqllist.Add(sql);
            }
            else
            {

                sql = "update TBWS_OUT set OP_DEP='" + LLDepCode + "',OP_DATE='" + Date + "',OP_TSAID='" + SCZH + "',";
                sql += "OP_SENDER='" + SendClerkCode + "',OP_DOC='" + DocCode + "',OP_VERIFIER='" + VerifierCode + "',OP_VERIFYDATE='" + ApproveDate + "',OP_HSFLAG='0',";
                sql += "OP_ROB='1',OP_STATE='1',OP_NOTE='" + Comment + "',OP_BILLTYPE='"+BillType+"' where OP_CODE='" + Code + "'";

                sqllist.Add(sql);
            }


            sql = "DELETE FROM TBWS_OUTDETAIL WHERE OP_CODE='" + Code + "'";

            sqllist.Add(sql);


            for (int i = 0; i < GridView1.Rows.Count; i++)
            {

                string MaterialCode = ((TextBox)this.GridView1.Rows[i].FindControl("marid")).Text.Trim();//物料代码

                if (MaterialCode != string.Empty)
                {
                    string UniqueID = Code + (i + 1).ToString().PadLeft(3, '0');//唯一号

                    string LotNumber = ((HtmlInputText)this.GridView1.Rows[i].FindControl("ph")).Value;//批号

                    string Fixed = "N";//是否定尺

                    float Length = 0;//长

                    try { Length = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("length")).Value); }
                    catch { Length = 0; }

                    float Width = 0;//宽
                    try { Width = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("width")).Value); }
                    catch { Width = 0; }

                    float DN = 0;//应发数量

                    float RN = 0;//实发数量

                    try
                    {
                        float temp = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("num")).Value);
                        if (temp > 0)
                        {
                            RN = -temp;
                        }
                        else
                        { RN = temp; }
                    }
                    catch { RN = 0; }

                    Int32 DQN = 0;//应发张（支）

                    Int32 RQN = 0;//实发张（支）

                    try
                    {
                        Int32 temp = Convert.ToInt32(((HtmlInputText)this.GridView1.Rows[i].FindControl("zhang")).Value);

                        if (temp > 0)
                        {
                            RQN = -temp;
                        }
                        else
                        { RQN = temp; }
                    }
                    catch { RQN = 0; }


                    string PlanMode = "MTO";//计划模式

                    string PTC = "备库";//计划跟踪号

                    string OrderID = "";//订单号

                    string Note = ((HtmlInputText)this.GridView1.Rows[i].FindControl("beizhu")).Value;//备注

                    string WarehouseOutCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputWarehouseCode")).Value;//仓库

                    string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;//仓位

                    float dj = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("dj")).Value);//单价

                    float je = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("je")).Value);//单价


                    //仓库编号=====重点生成

                    string SQCODE = MaterialCode + PTC + LotNumber + Length + Width + WarehouseOutCode + PositionCode;

                    //仓库唯一号

                    //@mid+@mptc+@mlotnum+CAST(@mlength AS VARCHAR(50))+CAST(@mwidth AS VARCHAR(50))+@mstid+@mwlid
                    //01.07.007437YB/REB/1-1_01_00010001.020

                    //物料编码+计划跟踪号+批号+长+宽+仓库编号+仓位编号==按照订单来入

                    sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                        "OP_FIXED,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                        "OP_UPRICE,OP_AMOUNT,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                        "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE) VALUES('" + Code + "','" + UniqueID + "','" + SQCODE + "','" +
                        MaterialCode + "','" +
                        Fixed + "','" + Length + "','" +
                        Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','"+dj+"','"+je+"','" +
                        WarehouseOutCode + "','" + PositionCode + "','" + PlanMode + "','" +
                        PTC + "','" + OrderID + "','" + Note + "','')";

                    sqllist.Add(sql);
                }
            }

            DBCallCommon.ExecuteTrans(sqllist);

            sqllist.Clear();


            string script = @"alert('数据保存成功!');";

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);
        }


        //审核操作

        protected void Verify_Click(object sender, EventArgs e)
        {
            //此处是审核操作
            List<string> sqllist = new List<string>();

            string sql = "";

            string Code = LabelCode.Text;//单号

            string sqlstate = "SELECT OP_STATE FROM TBWS_OUT WHERE OP_CODE='" + Code + "'";
            
            if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows.Count > 0)
            {
                if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows[0]["OP_STATE"].ToString() == "2")
                {

                    string script = @"alert('单据已审核，单据不能再审核！');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    return;
                }
            }


            string Date = TextBoxDate.Text;//日期

            string LLDepCode = DropDownListDep.SelectedValue;//领料部门

            string SCZH = TextBoxSCZH.Text;//生产制号

            string Comment = DropDownListBZ.SelectedValue;//制作班组

            string DocCode = LabelDocCode.Text;//制单单号

            string SendClerkCode = DropDownListSender.SelectedValue;//发料人编号
            string VerifierCode = Session["UserID"].ToString();
            string BillType = string.Empty;
            if (LabelCode.Text.Contains("QT"))
            {
                BillType = "6"; //其他出库中的特殊退库
            }
            else { BillType = "4"; } //领料单中特殊退库


            ClosingAccountDate(DateTime.Now.ToString("yyyy-MM-dd"));//获取系统封账时间

            string ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            //if(LabelApproveDate.Text.Trim())

            //上一次的审核日期LabelApproveDate.Text.Trim()，可能有值，也可能为空，有值表示反审，无值表示第一次审核

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

            //sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_DEP,OP_DATE,OP_TSAID," +
            //  "OP_SENDER,OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
            //  "OP_STATE,OP_NOTE,OP_BILLTYPE) VALUES('" + Code + "','" + LLDepCode + "','" + Date + "','" + SCZH + "','" +
            //  SendClerkCode + "','" + DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','1','1','" + Comment + "','4')";
            //sqllist.Add(sql);

            sql = "select count(*) from TBWS_OUT where OP_CODE='" + Code + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() == "0")
            {
                sql = "exec ResetSeed @tablename=TBWS_OUT";
                sqllist.Add(sql);

                sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_DEP,OP_DATE,OP_TSAID," +
                    "OP_SENDER,OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
                    "OP_STATE,OP_NOTE,OP_BILLTYPE) VALUES('" + Code + "','" + LLDepCode + "','" + Date + "','" + SCZH + "','" +
                    SendClerkCode + "','" + DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','1','1','" + Comment + "','"+BillType+"')";
                sqllist.Add(sql);
            }
            else
            {

                sql = "update TBWS_OUT set OP_DEP='" + LLDepCode + "',OP_DATE='" + Date + "',OP_TSAID='" + SCZH + "',";
                sql += "OP_SENDER='" + SendClerkCode + "',OP_DOC='" + DocCode + "',OP_VERIFIER='" + VerifierCode + "',OP_VERIFYDATE='" + ApproveDate + "',OP_HSFLAG='0',";
                sql += "OP_ROB='1',OP_STATE='1',OP_NOTE='" + Comment + "',OP_BILLTYPE='"+BillType+"' where OP_CODE='" + Code + "'";

                sqllist.Add(sql);
            }

            sql = "DELETE FROM TBWS_OUTDETAIL WHERE OP_CODE='" + Code + "'";

            sqllist.Add(sql);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {

                string MaterialCode = ((TextBox)this.GridView1.Rows[i].FindControl("marid")).Text.Trim();//物料代码

                if (MaterialCode != string.Empty)
                {
                    string UniqueID = Code + (i + 1).ToString().PadLeft(3, '0');//唯一号

                    string LotNumber = ((HtmlInputText)this.GridView1.Rows[i].FindControl("ph")).Value.Trim();//批号

                    string Fixed = "N";//是否定尺

                    float Length = 0;//长

                    try { Length = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("length")).Value.Trim()); }
                    catch { Length = 0; }

                    float Width = 0;//宽
                    try { Width = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("width")).Value.Trim()); }
                    catch { Width = 0; }

                    float DN = 0;//应发数量
                    try
                    {
                        float duetemp = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("num")).Value.Trim());
                        if (duetemp > 0)
                        {
                            DN = -duetemp;
                        }
                        else
                        { DN = duetemp; }
                    }
                    catch { DN = 0; }

                    float RN = 0;//实发数量
                    try
                    {
                        float temp = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("num")).Value);
                        if (temp > 0)
                        {
                            RN = -temp;
                        }
                        else
                        { RN = temp; }
                    }
                    catch { RN = 0; }

                    Int32 DQN = 0;//应发张（支）
                    try
                    {
                        Int32 duetemp = Convert.ToInt32(((HtmlInputText)this.GridView1.Rows[i].FindControl("zhang")).Value.Trim());

                        if (duetemp > 0)
                        {
                            DQN = -duetemp;
                        }
                        else
                        { DQN = duetemp; }
                    }
                    catch { DQN = 0; }

                    Int32 RQN = 0;//实发张（支）
                    try
                    {
                        Int32 temp = Convert.ToInt32(((HtmlInputText)this.GridView1.Rows[i].FindControl("zhang")).Value);

                        if (temp > 0)
                        {
                            RQN = -temp;
                        }
                        else
                        { RQN = temp; }
                    }
                    catch { RQN = 0; }


                    string PlanMode = "MTO";//计划模式

                    string PTC = "备库";//计划跟踪号

                    string OrderID = "";//订单号

                    string Note = ((HtmlInputText)this.GridView1.Rows[i].FindControl("beizhu")).Value;//备注

                    string WarehouseOutCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputWarehouseCode")).Value;//仓库

                    string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;//仓位

                    float dj = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("dj")).Value);//单价

                    float je = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("je")).Value);//金额

                    //仓库编号=====重点生成

                    string SQCODE = MaterialCode + PTC + LotNumber + Length + Width + WarehouseOutCode + PositionCode;

                    sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                       "OP_FIXED,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                       "OP_UPRICE,OP_AMOUNT,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                       "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE) VALUES('" + Code + "','" + UniqueID + "','" + SQCODE + "','" +
                       MaterialCode + "','" +
                       Fixed + "','" + Length + "','" +
                       Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','" + dj + "','" + je + "','" +
                       WarehouseOutCode + "','" + PositionCode + "','" + PlanMode + "','" +
                       PTC + "','" + OrderID + "','" + Note + "','')";

                    sqllist.Add(sql);
                }
            }


            DBCallCommon.ExecuteTrans(sqllist);

            sqllist.Clear();


            sql = DBCallCommon.GetStringValue("connectionStrings");
            SqlConnection con = new SqlConnection(sql);
            con.Open();
            SqlCommand cmd = new SqlCommand("StorageOut", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@OutCode", SqlDbType.VarChar, 50);			//增加参数
            cmd.Parameters["@OutCode"].Value = Code;							//为参数初始化

            cmd.Parameters.Add("@OutDate", SqlDbType.VarChar, 50);				//增加参数红字出库时间@InDate
            cmd.Parameters["@OutDate"].Value = ApproveDate;							//为参数初始化

            cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add("@ErrorRow", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            con.Close();

            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 0)
            {
                //Response.Redirect("SM_WarehouseOUT_Red.aspx?FLAG=OPEN&&ID=" + Code);
                if (LabelCode.Text.Contains("QT"))
                {
                    Response.Redirect("SM_WarehouseOUT_QT.aspx?FLAG=OPEN&&ID=" + Code);

                }
                else
                {
                    Response.Redirect("SM_WarehouseOUT_LL.aspx?FLAG=OPEN&&ID=" + Code);
                }
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
            {
                //LabelMessage.Text = "审核未通过：部分库存物料不存在！";
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
            {
                //LabelMessage.Text = "审核未通过：部分库存物料数量小于出库数量！";
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -1)
            {
                //LabelMessage.Text = "审核未通过：该出库单已经被审核！";
                string script = @"alert('审核未通过：该出库单已经被审核！');";

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);
            }
        }


        //反审当前当前出库单，调用出库单反审存储过程，反审前先判断是否满足反审条件。
        protected void AntiVerify_Click(object sender, EventArgs e)
        {
            if (LabelState.Text != "2")
            {
                string script = @"alert('当前出库单未审核无法反审！');";

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);
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
                Response.Redirect("SM_WarehouseOUT_Red.aspx?FLAG=OPEN&&ID=" + LabelCode.Text); 
            }

            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -1)
            {
                string script = @"alert('审核未通过：未审核的出库单无法反审！');";

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);

                return;
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
            {

                string script = @"alert('审核未通过：入库物料发生后续操作！');";

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);

                return;
            }

            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
            {

                string script = @"alert('审核未通过：部分入库物料发生后续操作！');";

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);

                return;
            }
        }



        //删除当前单据，删除单据前判断是否满足删除单据条件，删除单据后关闭当前页面。
        protected void DeleteBill_Click(object sender, EventArgs e)
        {
            if (LabelState.Text == "1")
            {
                List<string> sqllist = new List<string>();
                string sql = "DELETE FROM TBWS_OUT WHERE OP_CODE='" + LabelCode.Text + "'";
                sqllist.Add(sql);
                sql = "DELETE FROM TBWS_OUTDETAIL WHERE OP_CODE='" + LabelCode.Text + "'";
                sqllist.Add(sql);
                DBCallCommon.ExecuteTrans(sqllist);

                string script = @"window.close();";

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);

                return;
            }
            else if (LabelState.Text == "0")
            {

                string script = @"alert('当前出库单尚未保存！');";

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);

                return;
            }
        }

        protected void TextBoxMarid_TextChanged(object sender, EventArgs e)
        {
          


                GridViewRow gr = (sender as TextBox).Parent.Parent as GridViewRow;

                if ((sender as TextBox).Text.Trim().Length >= 12)
                {
                    string marid = (sender as TextBox).Text.Trim().Substring(0, 12);

                    string sqlText = "select MNAME,GUIGE,CAIZHI,GB,PURCUNIT from TBMA_MATERIAL where ID='" + marid + "' and STATE='1'";

                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);

                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            (gr.FindControl("marnm") as HtmlInputText).Value = dr["MNAME"].ToString();
                            (gr.FindControl("guige") as HtmlInputText).Value = dr["GUIGE"].ToString();
                            (gr.FindControl("caizhi") as HtmlInputText).Value = dr["CAIZHI"].ToString();
                            (gr.FindControl("gb") as HtmlInputText).Value = dr["GB"].ToString();
                            (gr.FindControl("unit") as HtmlInputText).Value = dr["PURCUNIT"].ToString();
                        }

                        dr.Close();

                        (sender as TextBox).Text = marid;

                        (gr.FindControl("pt") as HtmlInputText).Value = "备库";
                        
                        (gr.FindControl("ck") as HtmlInputText).Value = DropDownListWarehouse.SelectedItem.Text;
                        (gr.FindControl("InputWarehouseCode") as HtmlInputText).Value = DropDownListWarehouse.SelectedValue;
                        (gr.FindControl("cw") as HtmlInputText).Value = DropDownListPosition.SelectedItem.Text;
                        (gr.FindControl("InputPositionCode") as HtmlInputText).Value = DropDownListPosition.SelectedValue;
                    }
                    else
                    {

                        dr.Close();

                        (sender as TextBox).Text = string.Empty;
                        (gr.FindControl("marnm") as HtmlInputText).Value = string.Empty;
                        (gr.FindControl("guige") as HtmlInputText).Value = string.Empty;
                        (gr.FindControl("caizhi") as HtmlInputText).Value = string.Empty;
                        (gr.FindControl("gb") as HtmlInputText).Value = string.Empty;
                        (gr.FindControl("Unit") as HtmlInputText).Value = string.Empty;
                        (gr.FindControl("ptc") as HtmlInputText).Value = string.Empty;
                        (gr.FindControl("tr") as HtmlInputText).Value = string.Empty;

                        (gr.FindControl("ck") as HtmlInputText).Value = string.Empty;
                        (gr.FindControl("InputWarehouseCode") as HtmlInputText).Value = string.Empty;
                        (gr.FindControl("cw") as HtmlInputText).Value = string.Empty;
                        (gr.FindControl("InputPositionCode") as HtmlInputText).Value = string.Empty;

                        string script = @"alert('请输入正确的物料编码!');";

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);
                    }
                }
                else
                {
                    (sender as TextBox).Text = string.Empty;
                    (gr.FindControl("marnm") as HtmlInputText).Value = string.Empty;
                    (gr.FindControl("guige") as HtmlInputText).Value = string.Empty;
                    (gr.FindControl("caizhi") as HtmlInputText).Value = string.Empty;
                    (gr.FindControl("gb") as HtmlInputText).Value = string.Empty;
                    (gr.FindControl("Unit") as HtmlInputText).Value = string.Empty;
                    (gr.FindControl("ptc") as HtmlInputText).Value = string.Empty;
                    (gr.FindControl("tr") as HtmlInputText).Value = string.Empty;
                    (gr.FindControl("ck") as HtmlInputText).Value = string.Empty;
                    (gr.FindControl("InputWarehouseCode") as HtmlInputText).Value = string.Empty;
                    (gr.FindControl("cw") as HtmlInputText).Value = string.Empty;
                    (gr.FindControl("InputPositionCode") as HtmlInputText).Value = string.Empty;

                    string script = @"alert('请输入正确的助记符!');";

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);
                }

        }





        protected void backstorge_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_WarehouseOUT_Red.aspx?FLAG=PUSHRED");
        }

    }
}
