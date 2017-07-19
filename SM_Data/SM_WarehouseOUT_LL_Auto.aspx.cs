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
    public partial class SM_WarehouseOUT_LL_Auto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;

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

            string sql = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO WHERE DEP_CY='1' and DEP_SFJY='0'";
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

            string sql = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO WHERE DEP_FATHERID='04' and DEP_CY='1' and DEP_SFJY='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListBZ.DataValueField = "DEP_CODE";
            DropDownListBZ.DataTextField = "DEP_NAME";
            DropDownListBZ.DataSource = dt;
            DropDownListBZ.DataBind();
            ListItem item = new ListItem("--请选择--", "0");
            DropDownListBZ.Items.Insert(0, item);
        }


        protected void GetStaff()
        {
            string sql = "SELECT DISTINCT ST_CODE,ST_NAME FROM TBDS_STAFFINFO WHERE ST_DEPID='07'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListSender.DataValueField = "ST_CODE";
            DropDownListSender.DataTextField = "ST_NAME";
            DropDownListSender.DataSource = dt;
            DropDownListSender.DataBind();
        }

        protected void initial()
        {

            string flag = Request.QueryString["FLAG"].ToString();
            if (flag == "PUSHBLUE")
            {
                InitGridview();

                LabelCode.Text = generateCode();

                TextBoxPageNum.Text = "1";


                string  sql = "INSERT INTO TBWS_OUTCODE (OP_CODE,OP_BILLTYPE) VALUES ('" + LabelCode.Text + "','1')";

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

                //发料人默认为制单人

                //DropDownListSender
                try { DropDownListSender.Items.FindByValue(Session["UserID"].ToString()).Selected = true; }
                catch { }

            }
            else if (flag == "COPY")
            {
                InitGridview();

                LabelCode.Text = generateCode();

                TextBoxPageNum.Text = "1";

                LabelState.Text = "0";
                InputColour.Value = "0";

                string sql = "INSERT INTO TBWS_OUTCODE (OP_CODE,OP_BILLTYPE) VALUES ('" + LabelCode.Text + "','1')";

                DBCallCommon.ExeSqlText(sql);

                sql = "SELECT Top 1 DepCode AS LLDepCode,Dep AS LLDep," +
                "TSAID as SCZH,SenderCode AS SenderCode," +
                "Sender AS Sender,DocCode AS DocumentCode," +
                "Doc AS Document," +
                "TotalNote AS Comment,OP_NOTE1  FROM View_SM_OUT WHERE OutCode='" + Request.QueryString["ID"] + "'";

                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);

                if (dr.Read())
                {
                    TextBoxDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

                    ClosingAccountDate(TextBoxDate.Text.Trim());

                    try { DropDownListDep.Items.FindByValue(dr["LLDepCode"].ToString()).Selected = true; }
                    catch { }

                    TextBoxSCZH.Text = dr["SCZH"].ToString();

                    LabelSCZH.Text = dr["SCZH"].ToString();
                    TextBoxNOTE1.Text = dr["OP_NOTE1"].ToString();
                    try { DropDownListBZ.Items.FindByValue(dr["Comment"].ToString()).Selected = true; }
                    catch { }

                    //子项名称
                    //try { DropDownListZXMC.Items.FindByValue(dr["OP_ZXMC"].ToString()).Selected = true; }
                    //catch { }

                    LabelDoc.Text = dr["Document"].ToString();

                    LabelDocCode.Text = dr["DocumentCode"].ToString();

                    try { DropDownListSender.Items.FindByValue(dr["SenderCode"].ToString()).Selected = true; }
                    catch { }
                }
            }
        }

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
        /// 
        protected DataTable GetDataFromGrid(bool isInit)
        {
            DataTable dt = new DataTable();

            #region

            dt.Columns.Add("UniqueID");

            dt.Columns.Add("PTC");

            dt.Columns.Add("SQCODE");

            dt.Columns.Add("MaterialCode");

            dt.Columns.Add("MaterialName");

            dt.Columns.Add("MaterialStandard");

            dt.Columns.Add("Attribute");

            dt.Columns.Add("GB");

            dt.Columns.Add("LotNumber");

            dt.Columns.Add("Fixed");

            dt.Columns.Add("Length");

            dt.Columns.Add("Width");

            dt.Columns.Add("Unit");

            dt.Columns.Add("DN");

            dt.Columns.Add("RN");

            dt.Columns.Add("DQN");

            dt.Columns.Add("RQN");

            dt.Columns.Add("PlanMode");

            dt.Columns.Add("OrderCode");

            dt.Columns.Add("Warehouse");//仓库

            dt.Columns.Add("WarehouseCode");//仓库编号

            dt.Columns.Add("Position");//仓位

            dt.Columns.Add("PositionCode");//仓位编号

            dt.Columns.Add("BSH");

            dt.Columns.Add("Comment");


            #endregion

            if (!isInit)
            {

                foreach(GridViewRow gr in GridView1.Rows)
                {
                    DataRow newRow = dt.NewRow();

                    newRow["UniqueID"] = (gr.FindControl("LabelUniqueID") as Label).Text;

                    newRow["PTC"] = (gr.FindControl("LabelPTC") as Label).Text;

                    newRow["SQCODE"] = (gr.FindControl("LabelSQCODE") as Label).Text;

                    newRow["MaterialCode"] = (gr.FindControl("TextBoxMaterialCode") as TextBox).Text;

                    newRow["MaterialName"] = (gr.FindControl("LabelMaterialName") as Label).Text;

                    newRow["MaterialStandard"] = (gr.FindControl("LabelMaterialStandard") as Label).Text;

                    newRow["Attribute"] = (gr.FindControl("LabelAttribute") as Label).Text;

                    newRow["GB"] = (gr.FindControl("LabelGB") as Label).Text;

                    newRow["LotNumber"] = (gr.FindControl("LabelLotNumber") as Label).Text;

                    newRow["Fixed"] = (gr.FindControl("LabelFixed") as Label).Text;

                    newRow["Length"] = (gr.FindControl("LabelLength") as Label).Text;

                    newRow["Width"] = (gr.FindControl("LabelWidth") as Label).Text;

                    newRow["Unit"] = (gr.FindControl("LabelUnit") as Label).Text;

                    newRow["DN"] = (gr.FindControl("LabelDN") as Label).Text;

                    newRow["RN"] = (gr.FindControl("TextBoxRN") as TextBox).Text;

                    newRow["DQN"] = (gr.FindControl("LabelDQN") as Label).Text;

                    newRow["RQN"] = (gr.FindControl("TextBoxRQN") as TextBox).Text;

                    newRow["PlanMode"] = (gr.FindControl("LabelPlanMode") as Label).Text;

                    newRow["OrderCode"] = (gr.FindControl("LabelOrderID") as Label).Text;

                    newRow["Warehouse"] = (gr.FindControl("LabelWarehouse") as Label).Text;

                    newRow["WarehouseCode"] = (gr.FindControl("LabelWarehouseCode") as Label).Text;

                    newRow["Position"] = (gr.FindControl("LabelPosition") as Label).Text;

                    newRow["PositionCode"] = (gr.FindControl("LabelPositionCode") as Label).Text;

                    newRow["BSH"] = (gr.FindControl("LabelBSH") as Label).Text;

                    newRow["Comment"] = (gr.FindControl("TextBoxComment") as TextBox).Text;

                    dt.Rows.Add(newRow);
                }
            }


            if (isInit)
            {

                for (int i = GridView1.Rows.Count; i < 10; i++)
                {
                    DataRow newRow = dt.NewRow();

                    newRow[0] = 0;

                    dt.Rows.Add(newRow);
                }

            }

            dt.AcceptChanges();

            return dt;
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

        protected void TextBoxMaterialCode_TextChanged(object sender, EventArgs e)
        {
            Label1.Text = ac2Value.Value.Trim();

            string sqcode = ac2Value.Value.Trim();

            GridViewRow gr = (sender as TextBox).Parent.Parent as GridViewRow;

            string sql = "SELECT UniqueID='',SQCODE,MaterialCode,MaterialName,Standard as MaterialStandard," +
                   "Attribute,GB,Fixed,Length," +
                   "Width,LotNumber,PlanMode,PTC,OrderCode," +
                   "WarehouseCode,Warehouse,LocationCode AS PositionCode," +
                   "Location AS Position,Unit,cast(Number as float) AS DN,cast(Number as float) AS RN,SupportNumber AS DQN,SupportNumber AS RQN," +
                   "Note AS Comment,CGMODE AS BSH FROM View_SM_Storage WHERE SQCODE='" + sqcode + "' order by MaterialCode DESC";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);

            if (dr.Read())
            {
                (gr.FindControl("LabelUniqueID") as Label).Text = dr["UniqueID"].ToString();

                (gr.FindControl("LabelSQCODE") as Label).Text = dr["SQCODE"].ToString();

                (gr.FindControl("TextBoxMaterialCode") as TextBox).Text = dr["MaterialCode"].ToString();

                (gr.FindControl("LabelMaterialName") as Label).Text = dr["MaterialName"].ToString();

                (gr.FindControl("LabelMaterialStandard") as Label).Text = dr["MaterialStandard"].ToString();

                (gr.FindControl("LabelAttribute") as Label).Text = dr["Attribute"].ToString();

                (gr.FindControl("LabelGB") as Label).Text = dr["GB"].ToString();

                (gr.FindControl("LabelLotNumber") as Label).Text = dr["LotNumber"].ToString();

                (gr.FindControl("LabelFixed") as Label).Text = dr["Fixed"].ToString();

                (gr.FindControl("LabelLength") as Label).Text = dr["Length"].ToString();

                (gr.FindControl("LabelWidth") as Label).Text = dr["Width"].ToString();

                (gr.FindControl("LabelUnit") as Label).Text = dr["Unit"].ToString();

                (gr.FindControl("LabelDN") as Label).Text = dr["DN"].ToString();

                (gr.FindControl("TextBoxRN") as TextBox).Text = dr["RN"].ToString();

                (gr.FindControl("LabelDQN") as Label).Text = dr["DQN"].ToString();

                (gr.FindControl("TextBoxRQN") as TextBox).Text = dr["RQN"].ToString();

                (gr.FindControl("LabelPlanMode") as Label).Text = dr["PlanMode"].ToString();

                (gr.FindControl("LabelPTC") as Label).Text = dr["PTC"].ToString();
               

                (gr.FindControl("LabelOrderID") as Label).Text = dr["OrderCode"].ToString();

                (gr.FindControl("LabelWarehouseCode") as Label).Text = dr["WarehouseCode"].ToString();

                (gr.FindControl("LabelWarehouse") as Label).Text = dr["Warehouse"].ToString();

                (gr.FindControl("LabelPositionCode") as Label).Text = dr["PositionCode"].ToString();

                (gr.FindControl("LabelPosition") as Label).Text = dr["Position"].ToString();

                (gr.FindControl("LabelBSH") as Label).Text = dr["BSH"].ToString();

                (gr.FindControl("TextBoxComment") as TextBox).Text = dr["Comment"].ToString();
            }

            dr.Close();

            if(gr.RowIndex==GridView1.Rows.Count-1)
            {
                //追加一行
                AppendRow();
            }

        }

        private void AppendRow()
        {
            DataTable dt = this.GetDataFromGrid(false);

            DataRow newRow = dt.NewRow();

            newRow[0] = 0;

            dt.Rows.Add(newRow);

            this.GridView1.DataSource = dt;

            this.GridView1.DataBind();
        }

        //保存操作
        protected void Save_Click(object sender, EventArgs e)
        {
            //必须选择领料班组
            //if (DropDownListBZ.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择领料班组！');", true);
            //    return;
            //}

            if (isLock() == false)
            {
                string script = @"alert('正在核算,不允许操作!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

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

                sql = "DELETE FROM TBWS_OUTDETAIL WHERE OP_CODE='" + Code + "'";

                sqllist.Add(sql);

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    string UniqueID = Code + (i + 1).ToString().PadLeft(3, '0');

                    string SQCODE = ((Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;

                    //旧的仓库唯一号

                    string MaterialCode = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxMaterialCode")).Text.Trim();
                    string LotNumber = ((Label)this.GridView1.Rows[i].FindControl("LabelLotNumber")).Text.Trim();
                    string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;

                    int DueLength = 0;
                    try { DueLength = Convert.ToInt32(Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelLength")).Text)); }
                    catch { DueLength = 0; }


                    int Length = 0;
                    try { Length = Convert.ToInt32(Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelLength")).Text)); }
                    catch { Length = 0; }
                    int Width = 0;
                    try { Width = Convert.ToInt32(Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelWidth")).Text)); }
                    catch { Width = 0; }

                    string WarehouseOutCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;

                    string PositionCode = ((Label)this.GridView1.Rows[i].FindControl("LabelPositionCode")).Text;

                    string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;

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

                    string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                    string Note = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;
                    string bsh = ((Label)this.GridView1.Rows[i].FindControl("LabelBSH")).Text;

                    sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                        "OP_FIXED,OP_DUELENGTH,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                        "OP_UPRICE,OP_AMOUNT,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                        "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE,OP_BSH) VALUES('" + Code + "','" + UniqueID + "','" + SQCODE + "','" +
                        MaterialCode + "','" +
                        Fixed + "','" + DueLength + "','" + Length + "','" +
                        Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','0','0','" +
                        WarehouseOutCode + "','" + PositionCode + "','" + PlanMode + "','" +
                        PTC + "','" + OrderID + "','" + Note + "','','" + bsh + "')";
                    sqllist.Add(sql);
                }
                DBCallCommon.ExecuteTrans(sqllist);
                sqllist.Clear();
                LabelState.Text = "1";
                LabelMessage.Text = "保存成功";
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
                drlock.Close();
            }
        }



        //审核操作如何与库存信息关联
        protected void Verify_Click(object sender, EventArgs e)
        {
            if (isLock() == false)
            {
                string script = @"alert('正在核算,请您稍后操作...!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
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
                string SendClerkCode = DropDownListSender.SelectedValue;
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



                string VerifierCode = Session["UserID"].ToString();

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
                        "OP_STATE,OP_NOTE,OP_BILLTYPE,OP_RealTime,OP_PAGENUM,OP_NOTE1) VALUES('" + Code + "','" + LLDepCode + "','" + Date + "','" + SCZH + "','" +
                        SendClerkCode + "','" + DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','" +
                        Colour + "','1','" + Comment + "','" + BillType + "',convert(varchar(50),getdate(),120),'" + pagenum + "','" + NOTE1 + "')";

                    sqllist.Add(sql);

                }
                else
                {

                    sql = "update TBWS_OUT set OP_DEP='" + LLDepCode + "',OP_DATE='" + Date + "',OP_TSAID='" + SCZH + "',";
                    sql += "OP_SENDER='" + SendClerkCode + "',OP_DOC='" + DocCode + "',OP_VERIFIER='" + VerifierCode + "',OP_VERIFYDATE='" + ApproveDate + "',OP_HSFLAG='0',";
                    sql += "OP_ROB='" + Colour + "',OP_STATE='1',OP_NOTE='" + Comment + "',OP_BILLTYPE='" + BillType + "',OP_RealTime=convert(varchar(50),getdate(),120),OP_PAGENUM='" + pagenum + "',OP_NOTE1='" + NOTE1 + "'  where OP_CODE='" + Code + "'";

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
                    if (((Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text.Trim() != string.Empty)
                    {
                        string UniqueID = Code + (i + 1).ToString().PadLeft(3, '0');

                        string SQCODE = ((Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;

                        //旧的仓库唯一号

                        string MaterialCode = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxMaterialCode")).Text.Trim();
                        string LotNumber = ((Label)this.GridView1.Rows[i].FindControl("LabelLotNumber")).Text.Trim();
                        string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;

                        int DueLength = 0;
                        try { DueLength = Convert.ToInt32(Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelLength")).Text)); }
                        catch { DueLength = 0; }


                        int Length = 0;
                        try { Length = Convert.ToInt32(Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelLength")).Text)); }
                        catch { Length = 0; }
                        int Width = 0;
                        try { Width = Convert.ToInt32(Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelWidth")).Text)); }
                        catch { Width = 0; }

                        string WarehouseOutCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;

                        string PositionCode = ((Label)this.GridView1.Rows[i].FindControl("LabelPositionCode")).Text;

                        string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;

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

                        string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                        string Note = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;
                        string bsh = ((Label)this.GridView1.Rows[i].FindControl("LabelBSH")).Text;

                        sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                            "OP_FIXED,OP_DUELENGTH,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                            "OP_UPRICE,OP_AMOUNT,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                            "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE,OP_BSH) VALUES('" + Code + "','" + UniqueID + "','" + SQCODE + "','" +
                            MaterialCode + "','" +
                            Fixed + "','" + DueLength + "','" + Length + "','" +
                            Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','0','0','" +
                            WarehouseOutCode + "','" + PositionCode + "','" + PlanMode + "','" +
                            PTC + "','" + OrderID + "','" + Note + "','','" + bsh + "')";
                        sqllist.Add(sql);
                    }

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
                    else
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
                        Response.Redirect("SM_WarehouseOUT_LL.aspx?FLAG=OPEN&&ACTION=COPY&&ID=" + Code);
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
                    {
                        LabelMessage.Text = "审核未通过：第" + Convert.ToString(cmd.Parameters["@ErrorRow"].Value) + "行库存物料不存在！";
                        GridView1.Rows[Convert.ToInt32(cmd.Parameters["@ErrorRow"].Value) - 1].Cells[0].BackColor = System.Drawing.Color.Red;
                        GridView1.Rows[Convert.ToInt32(cmd.Parameters["@ErrorRow"].Value) - 1].Cells[1].BackColor = System.Drawing.Color.Red;
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
                    {
                        LabelMessage.Text = "审核未通过：第" + Convert.ToString(cmd.Parameters["@ErrorRow"].Value) + "行库存物料数量小于出库数量！";
                        GridView1.Rows[Convert.ToInt32(cmd.Parameters["@ErrorRow"].Value) - 1].Cells[0].BackColor = System.Drawing.Color.Red;
                        GridView1.Rows[Convert.ToInt32(cmd.Parameters["@ErrorRow"].Value) - 1].Cells[1].BackColor = System.Drawing.Color.Red;
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 3)
                    {
                        LabelMessage.Text = "审核未通过：张(支)数不为1的物料，更改了长度！";
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

        protected void DeleteItem_Click(object sender, EventArgs e)
        {
            int count = 0;
            DataTable tb = this.GetDataFromGrid(false);
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                CheckBox chk = ((CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1"));
                if (chk.Checked == true)
                {
                    tb.Rows.RemoveAt(i - count);
                    count++;
                }
            }

            for (int i = tb.Rows.Count; i < 10; i++)
            {
                DataRow newRow = tb.NewRow();

                newRow[0] = 0;

                tb.Rows.Add(newRow);
            }

            GridView1.DataSource = tb;
            GridView1.DataBind();
        }

        protected void DeleteFrom_Click(object sender, EventArgs e)
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
}
