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
    public partial class SM_Warehouse_TempIn : System.Web.UI.Page
    {
        int count = 0;

        string flag = string.Empty;

        string incode = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["FLAG"] != null)
                flag = Request.QueryString["FLAG"];

            if (!IsPostBack)
            {
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;

                getWarehouse();

                GetStaff();

                if (flag == "PUSHIN")
                {
                    InitInfo();

                    InitGridview();

                }
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






        //绑定初始化信息

        private void bindInitInfo()
        {

            if (Request.QueryString["ID"] != null)

                incode = Request.QueryString["ID"];

            /*总表信息*/

            string sql = "SELECT Top 1 WG_CODE AS Code,WG_DATE AS Date," +
              "WG_WAREHOUSE AS WarehouseCode," +
              "WG_DOC AS DocumentCode," +
              "DocName AS Document,WG_VERIFIER AS VerifierCode," +
              "VerfierName AS Verifier,WG_VERIFYDATE AS ApproveDate,WG_STATE AS State," +
              "WG_ABSTRACT AS Abstract FROM View_SM_IN WHERE WG_CODE='" + incode + "'";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);

            if (dr.Read())
            {
                LabelCode.Visible = true;

                LabelCode.Text = dr["Code"].ToString();//入库单号

                LabelState.Text = dr["State"].ToString();//是否审核

                TextBoxDate.Text = dr["Date"].ToString();//日期

                TextBoxAbstract.Text = dr["Abstract"].ToString();//摘要

                try { DropDownListWarehouse.Items.FindByValue(dr["WarehouseCode"].ToString()).Selected = true; }//仓库
                catch { }

                //收料人

                LabelDoc.Text = dr["Document"].ToString();//制单

                LabelDocCode.Text = dr["DocumentCode"].ToString();

                LabelVerifier.Text = dr["Verifier"].ToString();

                LabelVerifierCode.Text = dr["VerifierCode"].ToString();

                LabelApproveDate.Text = dr["ApproveDate"].ToString();
            }

            dr.Close();

            /*详细表信息*/

            sql = "SELECT WG_UNIQUEID AS UniqueID,WG_MARID AS MaterialCode,MNAME AS MaterialName," +
                "CAIZHI AS Attribute,GB AS GB,GUIGE AS MaterialStandard,WG_FIXEDSIZE AS Fixed,WG_LENGTH AS Length,WG_WIDTH AS Width," +
                "WG_LOTNUM AS LotNumber,CGDW AS Unit,cast(WG_RSNUM as float) AS RN,WG_RSFZNUM AS RQN," +
                "cast(WG_UPRICE as float) AS UnitPrice,cast(WG_TAXRATE as float) as TAXRATE,cast(WG_CTAXUPRICE as float) as CTAXUPRICE,cast(WG_AMOUNT as float) AS Amount,cast(WG_CTAMTMNY as float) as CTAMTMNY,WG_WAREHOUSE AS WarehouseCode," +
                "WS_NAME AS Warehouse,WG_LOCATION AS PositionCode,WL_NAME AS Position," +
                "WG_PMODE AS PlanMode,WG_PTCODE AS PTC,WG_ORDERID AS OrderID,WG_NOTE AS Comment " +
                "FROM View_SM_IN WHERE WG_CODE='" + incode + "'";

            DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
            GridView1.DataSource = tb;
            GridView1.DataBind();

            if (LabelState.Text == "1")
            {
                btnsave.Enabled = true;
                btnverity.Enabled = true;

                btninsert.Visible = true;
                btnappend.Visible = true;
                btndelete.Visible = true;

            }
            if (LabelState.Text == "2")
            {
                ImageVerify.Visible = true;
                btnsave.Enabled = false;
                btnverity.Enabled = false;

                btninsert.Visible = false;
                btnappend.Visible = false;
                btndelete.Visible = false;
            }


        }


        private void InitInfo()
        {
            //初始化单号

            LabelCode.Text = generateTempCode();

            string sql = "INSERT INTO TBWS_INCODE (WG_CODE,WG_BILLTYPE) VALUES ('" + LabelCode.Text + "','1')";

            DBCallCommon.ExeSqlText(sql);

            //制单人
            LabelDoc.Text = Session["UserName"].ToString();

            LabelDocCode.Text = Session["UserID"].ToString();

            TextBoxDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

            ClosingAccountDate(TextBoxDate.Text.Trim());

            for (int i = 0; i < DropDownListAccepter.Items.Count - 1;i++ )
            {
                if (DropDownListAccepter.Items[i].Value == "LabelDocCode.Text.Trim()")
                {
                    DropDownListAccepter.Items.FindByValue(LabelDocCode.Text.Trim()).Selected = true;
                    break;
                }
            }
            
        }

        protected string generateTempCode()
        {

            List<string> lt = new List<string>();

            string sql = "SELECT WG_CODE FROM TBWS_INCODE WHERE len(WG_CODE)=10 AND WG_BILLTYPE='1'";

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
                return "B000000001";
            }
            else
            {
                string tempstr = wsidlist.Max<string>();
                int tempnum = Convert.ToInt32((tempstr.Substring(1, 9)));
                tempnum++;
                tempstr = "B" + tempnum.ToString().PadLeft(9, '0');
                return tempstr;
            }
        }

        //收料人
        protected void GetStaff()
        {
            string sql = "SELECT DISTINCT ST_ID,ST_NAME FROM TBDS_STAFFINFO WHERE ST_DEPID='05'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            //DropDownListSender.DataValueField = "ST_CODE";
            //DropDownListSender.DataTextField = "ST_NAME";
            //DropDownListSender.DataSource = dt;
            //DropDownListSender.DataBind();

            DropDownListAccepter.DataValueField = "ST_ID";
            DropDownListAccepter.DataTextField = "ST_NAME";
            DropDownListAccepter.DataSource = dt;
            DropDownListAccepter.DataBind();
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

            
        }


        protected void DropDownListWarehouse_SelecedIndexChanged(object sender, EventArgs e)
        {
              bool IsSelect = true;

              for (int i = 0; i < GridView1.Rows.Count; i++)
              {
                  if ((GridView1.Rows[i].FindControl("CheckBox1") as CheckBox).Checked)
                  {
                      if (((TextBox)GridView1.Rows[i].FindControl("marid")).Text.Trim() != string.Empty)
                      {

                          HtmlInputText ht = (HtmlInputText)GridView1.Rows[i].FindControl("ck");

                          ht.Value = DropDownListWarehouse.SelectedItem.Text;

                          HtmlInputText lb_code = (HtmlInputText)GridView1.Rows[i].FindControl("InputWarehouseCode");

                          lb_code.Value = DropDownListWarehouse.SelectedValue;

                          /*
                           * 仓位数据还没添加，所以出现的待查状态
                           */

                          //HtmlInputText ht_position = (HtmlInputText)GridView1.Rows[i].FindControl("cw");

                          //ht_position.Value = "待查";

                          //HtmlInputText ht_position_code = (HtmlInputText)GridView1.Rows[i].FindControl("InputPositionCode");

                          //ht_position_code.Value = "0";
                      }

                      IsSelect = false;
                  }
              }
              if (IsSelect)
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
                           * 仓位数据还没添加，所以出现的待查状态
                           */

                          //HtmlInputText ht_position = (HtmlInputText)GridView1.Rows[i].FindControl("cw");

                          //ht_position.Value = "待查";

                          //HtmlInputText ht_position_code = (HtmlInputText)GridView1.Rows[i].FindControl("InputPositionCode");

                          //ht_position_code.Value = "0";

                      }

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
                    if (((TextBox)GridView1.Rows[i].FindControl("marid")).Text.Trim() != string.Empty)
                    {

                        HtmlInputText ht_position = (HtmlInputText)GridView1.Rows[i].FindControl("cw");

                        ht_position.Value = DropDownListPosition.SelectedItem.Text;

                        HtmlInputText ht_position_code = (HtmlInputText)GridView1.Rows[i].FindControl("InputPositionCode");

                        ht_position_code.Value = DropDownListPosition.SelectedValue;
                    }

                    IsSelect = false;
                }
            }

            if (IsSelect)
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

            dt.Columns.Add("TAXRATE");//税率

            dt.Columns.Add("CTAXUPRICE");//含税单价

            dt.Columns.Add("CTAMTMNY");//含税金额



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

                    newRow["UnitPrice"] = ((HtmlInputText)gRow.FindControl("dj")).Value;//单价

                    newRow["Amount"] = ((HtmlInputText)gRow.FindControl("je")).Value;//金额

                    newRow["TAXRATE"] = ((HtmlInputText)gRow.FindControl("tr")).Value;//税率

                    newRow["CTAXUPRICE"] = ((HtmlInputText)gRow.FindControl("tdj")).Value;//含税单价

                    newRow["CTAMTMNY"] = ((HtmlInputText)gRow.FindControl("tje")).Value;//含税金额

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
                    DataRow newRow = dt.NewRow();
                    newRow[7] = 0;
                    newRow[8] = 0;
                    newRow[9] = 0;
                    newRow[10] = 0;
                    dt.Rows.InsertAt(newRow, dt.Rows.Count - 1);
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
            if (isLock() == false)
            {
                string script = @"alert('系统正在结账,请稍后...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                //LabelMessage.Text = "系统正在结账,请稍后...";
            }
            else
            {
                if (DropDownListWarehouse.SelectedValue == "0" || DropDownListPosition.SelectedValue == "0")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('请选择仓库和仓位!')</script>");
                    return;
                }



                bool HasError = false;
                int ErrorType = 0;


                //此处是保存操作
                List<string> sqllist = new List<string>();

                string sql = "";

                string Code = LabelCode.Text;//单号

                string WareHouse = DropDownListWarehouse.SelectedValue;

                string Date = TextBoxDate.Text;//日期

                string Abstract = TextBoxAbstract.Text;//摘要

                string DocCode = LabelDocCode.Text;//制单人单号

                string Acceptance = DropDownListAccepter.SelectedItem.Text;
                string AcceptanceCode = DropDownListAccepter.SelectedValue;



                sql = "DELETE FROM TBWS_IN WHERE WG_CODE='" + Code + "'";

                sqllist.Add(sql);

                //1为备库

                sql = "INSERT INTO TBWS_IN(WG_CODE,WG_ABSTRACT,WG_DATE," +
                    "WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG," +
                    "WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_ZGFLAG,WG_HSFLAG,WG_ACTPERIOD,WG_BILLTYPE) VALUES('" + Code + "','" + Abstract + "','" + Date + "','" +
                    DocCode + "','" + AcceptanceCode + "','','','1','0','0','0','0','0','0','','1')";

                sqllist.Add(sql);

                sql = "DELETE FROM TBWS_INDETAIL WHERE WG_CODE='" + Code + "'";

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

                        double RN = 0;//实收数量

                        try
                        {
                            double temp = Convert.ToDouble(((HtmlInputText)this.GridView1.Rows[i].FindControl("num")).Value);
                            if (temp < 0)
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


                        Int32 RQN = 0;//实收张（支）

                        try
                        {
                            Int32 temp = Convert.ToInt32(((HtmlInputText)this.GridView1.Rows[i].FindControl("zhang")).Value);

                            if (temp < 0)
                            {
                                RQN = -temp;
                            }
                            else
                            { RQN = temp; }
                        }
                        catch { RQN = 0; }


                        string PlanMode = "";//计划模式

                        string PTC = "备库";//计划跟踪号

                        string OrderID = "";//订单号

                        string Note = ((HtmlInputText)this.GridView1.Rows[i].FindControl("beizhu")).Value;//备注

                        string WarehouseOutCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputWarehouseCode")).Value;//仓库

                        string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;//仓位


                        if (PositionCode == string.Empty || PositionCode == "0")
                        {
                            HasError = true;
                            ErrorType = 2;
                            break;
                        }


                        double dj = 0;

                        try
                        {
                            double temp = Convert.ToDouble(((HtmlInputText)this.GridView1.Rows[i].FindControl("dj")).Value);//单价

                            dj = temp;
                        }
                        catch { dj = 0; }


                        //float dj = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("dj")).Value);//单价

                        double je = 0;

                        try
                        {
                            double temp = Convert.ToDouble(((HtmlInputText)this.GridView1.Rows[i].FindControl("je")).Value);//金额

                            je = temp;
                        }
                        catch { je = 0; }

                        //float je = Convert.ToSingle(((HtmlInputText)this.GridView1.Rows[i].FindControl("je")).Value);//金额

                        //税率

                        double tr = 0;

                        try
                        {
                            double temp = Convert.ToDouble(((HtmlInputText)this.GridView1.Rows[i].FindControl("tr")).Value);//税率

                            tr = temp;
                        }
                        catch { tr = 0; }


                        //含税单价

                        double tdj = 0;

                        try
                        {
                            double temp = Convert.ToDouble(((HtmlInputText)this.GridView1.Rows[i].FindControl("tdj")).Value);//含税单价

                            tdj = temp;
                        }
                        catch { tdj = 0; }

                        //含税金额

                        double tje = 0;

                        try
                        {
                            double temp = Convert.ToDouble(((HtmlInputText)this.GridView1.Rows[i].FindControl("tje")).Value);//含税金额

                            tje = temp;
                        }
                        catch { tje = 0; }


                        //仓库编号=====重点生成

                        string SQCODE = MaterialCode + PTC + LotNumber + Length + Width + WarehouseOutCode + PositionCode;

                        //仓库唯一号

                        //@mid+@mptc+@mlotnum+CAST(@mlength AS VARCHAR(50))+CAST(@mwidth AS VARCHAR(50))+@mstid+@mwlid
                        //01.07.007437YB/REB/1-1_01_00010001.020

                        //物料编码+计划跟踪号+批号+长+宽+仓库编号+仓位编号==按照订单来入

                        sql = "INSERT INTO TBWS_INDETAIL(WG_CODE,WG_UNIQUEID,WG_MARID," +
                            "WG_LOTNUM,WG_LENGTH,WG_WIDTH,WG_FIXEDSIZE,WG_RSNUM,WG_RSFZNUM,WG_UPRICE,WG_TAXRATE," +
                            "WG_CTAXUPRICE,WG_AMOUNT,WG_CTAMTMNY,WG_LOCATION,WG_ORDERID," +
                            "WG_PMODE,WG_PTCODE,WG_NOTE,WG_STATE,WG_WAREHOUSE) VALUES('" + Code + "','" + UniqueID + "','" + MaterialCode + "','" +
                             LotNumber + "','" + Length + "','" + Width + "','" + Fixed + "','" + RN + "','" + RQN + "','" + dj + "','" + tr + "','" + tdj + "','" +
                             je + "','" + tje + "','" + PositionCode + "','" + OrderID + "','" +
                             PlanMode + "','" + PTC + "','" + Note + "','','" + WareHouse + "')";

                        sqllist.Add(sql);
                    }
                }


                if (HasError)
                {
                    sqllist.Clear();

                    if (ErrorType == 1)
                    {


                        string script = @"alert('入库条目为0，单据不能保存！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                    else
                    {
                        ;



                        string script = @"alert('入库条目仓位为空，单据不能保存！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                }
                else
                {
                    DBCallCommon.ExecuteTrans(sqllist);

                    sqllist.Clear();

                    string script = @"alert('数据保存成功!');";

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);
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


        //审核操作

        protected void Verify_Click(object sender, EventArgs e)
        {
            if (isLock() == false)
            {
                string script = @"alert('系统正在结账,请稍后...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                //LabelMessage.Text = "系统正在结账,请稍后...";
                return;
            }
            else
            {
                if (DropDownListWarehouse.SelectedValue == "0" || DropDownListPosition.SelectedValue == "0")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('请选择仓库和仓位!')</script>");
                    return;
                }




                bool HasError = false;
                int ErrorType = 0;

                //此处是审核操作
                List<string> sqllist = new List<string>();

                string sql = "";

                string Code = LabelCode.Text;//单号

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
                string WareHouse = DropDownListWarehouse.SelectedValue; //仓库

                string Date = TextBoxDate.Text;//日期

                string Abstract = TextBoxAbstract.Text;//摘要

                string DocCode = LabelDocCode.Text;//制单人单号

                string VerifierCode = Session["UserID"].ToString();

                string ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                string Acceptance = DropDownListAccepter.SelectedItem.Text;
                string AcceptanceCode = DropDownListAccepter.SelectedValue;


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

                sql = "DELETE FROM TBWS_IN WHERE WG_CODE='" + Code + "'";

                sqllist.Add(sql);

                sql = "exec ResetSeed @tablename=TBWS_IN";
                sqllist.Add(sql);

                sql = "INSERT INTO TBWS_IN (WG_CODE,WG_ABSTRACT,WG_DATE," +
                    "WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE,WG_STATE,WG_TEARFLAG," +
                    "WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_ZGFLAG,WG_HSFLAG,WG_ACTPERIOD,WG_BILLTYPE,WG_RealTime) VALUES('" + Code + "','" + Abstract + "','" + Date + "','" +
                    DocCode + "','" + AcceptanceCode + "','" + VerifierCode + "','" + ApproveDate + "','2','0','0','0','0','0','0','','1',convert(varchar(50),getdate(),120))";

                sqllist.Add(sql);

                sql = "DELETE FROM TBWS_INDETAIL WHERE WG_CODE='" + Code + "'";

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

                        double RN = 0;//实发数量

                        try
                        {
                            double temp = Convert.ToDouble(((HtmlInputText)this.GridView1.Rows[i].FindControl("num")).Value);
                            if (temp < 0)
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


                        Int32 RQN = 0;//实发张（支）

                        try
                        {
                            Int32 temp = Convert.ToInt32(((HtmlInputText)this.GridView1.Rows[i].FindControl("zhang")).Value);

                            if (temp < 0)
                            {
                                RQN = -temp;
                            }
                            else
                            { RQN = temp; }
                        }
                        catch { RQN = 0; }


                        string PlanMode = "";//计划模式

                        string PTC = "备库";//计划跟踪号

                        string OrderID = "";//订单号

                        string Note = ((HtmlInputText)this.GridView1.Rows[i].FindControl("beizhu")).Value;//备注

                        string WarehouseOutCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputWarehouseCode")).Value;//仓库

                        string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;//仓位

                        if (PositionCode == string.Empty || PositionCode == "0")
                        {
                            HasError = true;
                            ErrorType = 2;
                            break;
                        }


                        double dj = 0;

                        try
                        {
                            double temp = Convert.ToDouble(((HtmlInputText)this.GridView1.Rows[i].FindControl("dj")).Value);//单价

                            dj = temp;
                        }
                        catch { dj = 0; }

                        double je = 0;

                        try
                        {
                            double temp = Convert.ToDouble(((HtmlInputText)this.GridView1.Rows[i].FindControl("je")).Value);//金额

                            je = temp;
                        }
                        catch { je = 0; }


                        //税率

                        double tr = 0;

                        try
                        {
                            double temp = Convert.ToDouble(((HtmlInputText)this.GridView1.Rows[i].FindControl("tr")).Value);//税率

                            tr = temp;
                        }
                        catch { tr = 0; }

                        //含税单价

                        double tdj = 0;

                        try
                        {
                            double temp = Convert.ToDouble(((HtmlInputText)this.GridView1.Rows[i].FindControl("tdj")).Value);//含税单价

                            tdj = temp;
                        }
                        catch { tdj = 0; }

                        //含税金额

                        double tje = 0;

                        try
                        {
                            double temp = Convert.ToDouble(((HtmlInputText)this.GridView1.Rows[i].FindControl("tje")).Value);//含税金额

                            tje = temp;
                        }
                        catch { tje = 0; }


                        //仓库编号=====重点生成

                        string SQCODE = MaterialCode + PTC + LotNumber + Length + Width + WarehouseOutCode + PositionCode;

                        sql = "INSERT INTO TBWS_INDETAIL(WG_CODE,WG_UNIQUEID,WG_MARID," +
                           "WG_LOTNUM,WG_LENGTH,WG_WIDTH,WG_FIXEDSIZE,WG_RSNUM,WG_RSFZNUM,WG_UPRICE,WG_TAXRATE," +
                           "WG_CTAXUPRICE,WG_AMOUNT,WG_CTAMTMNY,WG_LOCATION,WG_ORDERID," +
                           "WG_PMODE,WG_PTCODE,WG_NOTE,WG_STATE,WG_WAREHOUSE) VALUES('" + Code + "','" + UniqueID + "','" + MaterialCode + "','" +
                            LotNumber + "','" + Length + "','" + Width + "','" + Fixed + "','" + RN + "','" + RQN + "','" + dj + "','" + tr + "','" + tdj + "','" +
                            je + "','" + tje + "','" + PositionCode + "','" + OrderID + "','" +
                            PlanMode + "','" + PTC + "','" + Note + "','','" + WareHouse + "')";

                        sqllist.Add(sql);
                    }
                }
                if (HasError)
                {
                    sqllist.Clear();

                    if (ErrorType == 1)
                    {


                        string script = @"alert('入库条目为0，单据不能审核！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                    else
                    {

                        string script = @"alert('入库条目仓位为空，单据不能审核！');";

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
                    SqlCommand cmd = new SqlCommand("MaterialIn", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@InCode", SqlDbType.VarChar, 50);				//增加参数入库单号@InCode
                    cmd.Parameters["@InCode"].Value = LabelCode.Text;						//为参数初始化
                    cmd.Parameters.Add("@InDate", SqlDbType.VarChar, 50);				//增加参数入库单号@InDate
                    cmd.Parameters["@InDate"].Value = ApproveDate;							//为参数初始化
                    cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;			//增加返回值参数@retVal
                    cmd.ExecuteNonQuery();
                    con.Close();
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 0)
                    {
                        LabelState.Text = "2";
                        Response.Redirect("SM_WarehouseIN_WG_WTB.aspx?FLAG=READ&&ID=" + Code);
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
                    {
                        //LabelMessage.Text = "审核未通过：入库物料发生后续操作！";

                        string script = @"alert('审核未通过：入库物料发生后续操作！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
                    {
                        //LabelMessage.Text = "审核未通过：部分入库物料发生后续操作！";

                        string script = @"alert('审核未通过：部分入库物料发生后续操作！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                }
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


        protected void marid_TextChanged(object sender, EventArgs e)
        {
            GridViewRow gr = (sender as TextBox).Parent.Parent as GridViewRow;

            if ((sender as TextBox).Text.Trim().Length >= 12)
            {
                string marid = (sender as TextBox).Text.Trim().Split('|')[0].ToString();

                string sqlText = "select MNAME,GUIGE,CAIZHI,GB,PURCUNIT from TBMA_MATERIAL where ID='" + marid + "' and STATE='1'";//state=1在用的材料

                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);

                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        (gr.FindControl("marnm") as HtmlInputText).Value = dr["MNAME"].ToString();
                        (gr.FindControl("guige") as HtmlInputText).Value = dr["GUIGE"].ToString();
                        (gr.FindControl("caizhi") as HtmlInputText).Value = dr["CAIZHI"].ToString();
                        (gr.FindControl("gb") as HtmlInputText).Value = dr["GB"].ToString();
                        (gr.FindControl("Unit") as HtmlInputText).Value = dr["PURCUNIT"].ToString();
                    }

                    dr.Close();

                    (sender as TextBox).Text = marid;

                    //(gr.FindControl("ptc") as HtmlInputText).Value = TextBoxProject.Text.Trim().Split('|')[0] + "(" + TextBoxProject.Text.Trim().Split('|')[TextBoxProject.Text.Trim().Split('|').Length - 1] + ")" + "_" + DropDownListEng.Text.Split('|')[0] + "_" + DropDownListEng.Text + "_T" + Convert.ToInt32(LabelCode.Text.Trim().Substring(1, 9)).ToString() + "_" + (gr.RowIndex + 1).ToString().PadLeft(4, '0');
                    (gr.FindControl("pt") as HtmlInputText).Value = "备库";


                    (gr.FindControl("tr") as HtmlInputText).Value = "17";

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
                    (gr.FindControl("pt") as HtmlInputText).Value = string.Empty;
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



    }
}
