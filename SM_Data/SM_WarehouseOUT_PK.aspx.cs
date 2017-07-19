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
    public partial class SM_WarehouseOUT_PK : System.Web.UI.Page
    {
        double tdn = 0;
        double trn = 0;
        Int32 tdqn = 0;
        Int32 trqn = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            string code = Request.QueryString["ID"].ToString();
            string flag = Request.QueryString["FLAG"].ToString();
          
            if (flag == "OPEN")
            {
                string sql = "SELECT Top 1 OutCode AS Code,DepCode AS LLDepCode,Dep AS LLDep,Date AS Date," +
                "TSAID as SCZH,SenderCode AS SenderCode," +
                "Sender AS Sender,DocCode AS DocumentCode," +
                "Doc AS Document,VerifierCode AS VerifierCode," +
                "Verifier AS Verifier,LEFT(ApprovedDate,10) AS ApproveDate,ROB AS Colour,TotalState AS State,BillType," +
                "TotalNote AS Comment,OP_ZXMC FROM View_SM_OUT WHERE OutCode='" + code + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {

                    LabelCode.Text = dr["Code"].ToString();
                    LabelState.Text = dr["State"].ToString();
                    InputColour.Value = dr["Colour"].ToString();
                    TextBoxDate.Text = dr["Date"].ToString();
                    ClosingAccountDate(TextBoxDate.Text.Trim());
                    TextBoxSCZH.Text = dr["SCZH"].ToString();
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
                    "LotNumber AS LotNumber,Unit AS Unit,cast(DueNumber as float) AS DN,cast(RealNumber as float) AS RN,cast(DueSupportNumber as float) AS DQN,cast(RealSupportNumber as float) AS RQN," +
                    "cast(UnitPrice as float) AS UnitPrice,cast(Amount as float) AS Amount,WarehouseCode AS WarehouseCode," +
                    "Warehouse AS Warehouse,LocationCode AS PositionCode,Location AS Position," +
                    "PlanMode AS PlanMode,PTC AS PTC,OrderCode AS OrderID,DetailNote AS Comment " +
                    "FROM View_SM_OUT WHERE OutCode='" + code + "' order by MaterialCode ";
                DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
                GridView1.DataSource = tb;
                GridView1.DataBind();
                if (InputColour.Value == "1")
                {
                    ImageRed.Visible = true;
                }
                if (LabelState.Text == "1")
                {
                    Save.Enabled = true;
                    Verify.Enabled = true;
                    DeleteBill.Enabled = true;
                    AntiVerify.Enabled = false;
                    //审核日期和审核人隐藏
                    LabelApproveDate.Visible = false;
                    LabelVerifier.Visible = false;
                }
                if (LabelState.Text == "2")
                {
                    if (Session["UserID"].ToString() == LabelVerifierCode.Text.Trim())
                    {
                        ImageVerify.Visible = true;
                        Save.Enabled = false;
                        Verify.Enabled = false;
                        DeleteBill.Enabled = false;
                        AntiVerify.Enabled = true;
                    }
                    else
                    {
                        ImageVerify.Visible = true;
                        Save.Enabled = false;
                        Verify.Enabled = false;
                        AntiVerify.Enabled = false;
                        DeleteBill.Enabled = false;

                    }
                }
            }

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tdn += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "RN"));
                trn += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "DN"));
                tdqn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DQN"));
                trqn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RQN"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.Cells[13].FindControl("LabelTotalDN")).Text = Math.Round(trn, 4).ToString();
                ((Label)e.Row.Cells[14].FindControl("LabelTotalRN")).Text = Math.Round(tdn, 4).ToString();
                ((Label)e.Row.Cells[15].FindControl("LabelTotalDQN")).Text = tdqn.ToString();
                ((Label)e.Row.Cells[16].FindControl("LabelTotalRQN")).Text = trqn.ToString();
            }
        }

        //保存操作
        protected void Save_Click(object sender, EventArgs e)
        {
            //此处是保存操作
            List<string> sqllist = new List<string>();
            string sql = "";

            string Code = LabelCode.Text;
            string State = LabelState.Text;
            string Colour = InputColour.Value;
            string Date = TextBoxDate.Text;
            string LLDepCode = "";
            string SCZH = TextBoxSCZH.Text;
            string Comment = "";
            string ZXMC = "";
            string DocCode = LabelDocCode.Text;
            string SendClerkCode = DropDownListSender.SelectedValue;
            string VerifierCode = LabelVerifierCode.Text;
            string ApproveDate = LabelApproveDate.Text;
            string BillType = LabelBillType.Text.Trim();

            sql = "DELETE FROM TBWS_OUT WHERE OP_CODE='" + Code + "'";
            sqllist.Add(sql);
            sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_DEP,OP_DATE,OP_TSAID," +
                "OP_SENDER,OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
                "OP_STATE,OP_NOTE,OP_BILLTYPE,OP_ZXMC) VALUES('" + Code + "','" + LLDepCode + "','" + Date + "','" + SCZH + "','" +
                SendClerkCode + "','" + DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','" +
                Colour + "','1','" + Comment + "','" + BillType + "','" + ZXMC + "')";
            sqllist.Add(sql);
            sql = "DELETE FROM TBWS_OUTDETAIL WHERE OP_CODE='" + Code + "'";
            sqllist.Add(sql);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string UniqueID = Code + (i + 1).ToString();

                string SQCODE = ((Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;

                //旧的仓库唯一号

                string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text.Trim();
                string LotNumber = ((Label)this.GridView1.Rows[i].FindControl("LabelLotNumber")).Text.Trim();
                string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;

                int DueLength = 0;
                try { DueLength = Convert.ToInt32(Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelLength")).Text)); }
                catch { DueLength = 0; }


                int Length = 0;
                try { Length = Convert.ToInt32(Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLength")).Text)); }
                catch { Length = 0; }
                int Width = 0;
                try { Width = Convert.ToInt32(Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxWidth")).Text)); }
                catch { Width = 0; }

                string WarehouseOutCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;
                string PositionCode = ((Label)this.GridView1.Rows[i].FindControl("LabelPositionCode")).Text;
                string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;
                //保存仓库号时需要重新获取


                //@mid+@mptc+@mlotnum+CAST(@mlength AS VARCHAR(50))+CAST(@mwidth AS VARCHAR(50))+@mstid+@mwlid（物料代码+计划跟踪号+批号+长+宽+仓库+仓位）

                //新的仓库唯一号
                //string SQCODE = MaterialCode + PTC + LotNumber + Convert.ToString(Length) + Convert.ToString(Width) + WarehouseOutCode + PositionCode;  

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
                //string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;
                string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                string Note = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;

                sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                    "OP_FIXED,OP_DUELENGTH,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                    "OP_UPRICE,OP_AMOUNT,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                    "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE) VALUES('" + Code + "','" + UniqueID + "','" + SQCODE + "','" +
                    MaterialCode + "','" +
                    Fixed + "','" + DueLength + "','" + Length + "','" +
                    Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','0','0','" +
                    WarehouseOutCode + "','" + PositionCode + "','" + PlanMode + "','" +
                    PTC + "','" + OrderID + "','" + Note + "','')";
                sqllist.Add(sql);
            }
            DBCallCommon.ExecuteTrans(sqllist);
            sqllist.Clear();
            LabelState.Text = "1";
            LabelMessage.Text = "保存成功";
        }

        //审核操作如何与库存信息关联
        protected void Verify_Click(object sender, EventArgs e)
        {
            //此处是审核操作
            List<string> sqllist = new List<string>();
            string sql = "";

            string Code = LabelCode.Text;
            string State = LabelState.Text;
            string Colour = InputColour.Value;
            string Date = TextBoxDate.Text;
            string LLDepCode = "";
            string SCZH = TextBoxSCZH.Text;
            string Comment = "";
            string ZXMC ="";
            string DocCode = LabelDocCode.Text;
            string SendClerkCode = DropDownListSender.SelectedValue;
            string BillType =LabelBillType.Text.Trim();
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
                        }
                    }
                    else
                    {
                        //第一次审核
                        ApproveDate = getNextMonth() + " 07:59:59";
                    }
                }

            }

            sql = "DELETE FROM TBWS_OUT WHERE OP_CODE='" + Code + "'";
            sqllist.Add(sql);

            //重置标识符
            sql = "exec ResetSeed @tablename=TBWS_OUT";
            sqllist.Add(sql);

            sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_DEP,OP_DATE,OP_TSAID," +
                "OP_SENDER,OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
                "OP_STATE,OP_NOTE,OP_BILLTYPE,OP_RealTime,OP_ZXMC) VALUES('" + Code + "','" + LLDepCode + "','" + Date + "','" + SCZH + "','" +
                SendClerkCode + "','" + DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','" +
                Colour + "','1','" + Comment + "','" + BillType + "',convert(varchar(50),getdate(),120),'" + ZXMC + "')";
            sqllist.Add(sql);
            sql = "DELETE FROM TBWS_OUTDETAIL WHERE OP_CODE='" + Code + "'";
            sqllist.Add(sql);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string UniqueID = Code + (i + 1).ToString();

                string SQCODE = ((Label)this.GridView1.Rows[i].FindControl("LabelSQCODE")).Text;

                //旧的仓库唯一号

                string MaterialCode = ((Label)this.GridView1.Rows[i].FindControl("LabelMaterialCode")).Text.Trim();
                string LotNumber = ((Label)this.GridView1.Rows[i].FindControl("LabelLotNumber")).Text.Trim();
                string Fixed = ((Label)this.GridView1.Rows[i].FindControl("LabelFixed")).Text;

                int DueLength = 0;
                try { DueLength = Convert.ToInt32(Convert.ToSingle(((Label)this.GridView1.Rows[i].FindControl("LabelLength")).Text)); }
                catch { DueLength = 0; }


                int Length = 0;
                try { Length = Convert.ToInt32(Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxLength")).Text)); }
                catch { Length = 0; }
                int Width = 0;
                try { Width = Convert.ToInt32(Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxWidth")).Text)); }
                catch { Width = 0; }

                string WarehouseOutCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;
                string PositionCode = ((Label)this.GridView1.Rows[i].FindControl("LabelPositionCode")).Text;
                string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;

                //保存仓库号时需要重新获取
                //@mid+@mptc+@mlotnum+CAST(@mlength AS VARCHAR(50))+CAST(@mwidth AS VARCHAR(50))+@mstid+@mwlid（物料代码+计划跟踪号+批号+长+宽+仓库+仓位）

                //string SQCODE = MaterialCode + PTC + LotNumber + Convert.ToString(Length) + Convert.ToString(Width) + WarehouseOutCode + PositionCode;

                //新的仓库唯一号

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
                //string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;
                string OrderID = ((Label)this.GridView1.Rows[i].FindControl("LabelOrderID")).Text;
                string Note = ((TextBox)this.GridView1.Rows[i].FindControl("TextBoxComment")).Text;
                //string WarehouseOutCode = ((Label)this.GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;
                //string PositionCode = ((Label)this.GridView1.Rows[i].FindControl("LabelPositionCode")).Text;
                sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                    "OP_FIXED,OP_DUELENGTH,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                    "OP_UPRICE,OP_AMOUNT,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                    "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE) VALUES('" + Code + "','" + UniqueID + "','" + SQCODE + "','" +
                    MaterialCode + "','" +
                    Fixed + "','" + DueLength + "','" + Length + "','" +
                    Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','0','0','" +
                    WarehouseOutCode + "','" + PositionCode + "','" + PlanMode + "','" +
                    PTC + "','" + OrderID + "','" + Note + "','')";
                sqllist.Add(sql);
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

            cmd.Parameters.Add("@OutDate", SqlDbType.VarChar, 50);			//出库时间
            cmd.Parameters["@OutDate"].Value = ApproveDate;

            cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add("@ErrorRow", SqlDbType.Int, 1).Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();
            con.Close();
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 0)
            {
                sqllist.Clear();
                string sqlstr = string.Empty;

                if (Code.Contains('R'))
                {
                    sqlstr = "update TBWS_INVENTORYSCHEMA set PD_YSTATE='2' where PD_CODE='" + Code.Split('R')[0] + "'";
                    sqllist.Add(sqlstr);
                    sqlstr = "update TBWS_INVENTORYSCHEMA set PD_DONESTATE='3' where PD_CODE='" + Code.Split('R')[0] + "'AND PD_KSTATE='2' AND PD_YSTATE='2'";
                    sqllist.Add(sqlstr);
                }
                else
                {
                    sqlstr = "update TBWS_INVENTORYSCHEMA set PD_KSTATE='2' where PD_CODE='" + Code + "'";
                    sqllist.Add(sqlstr);
                    sqlstr = "update TBWS_INVENTORYSCHEMA set PD_DONESTATE='3' where PD_CODE='" + Code + "'AND PD_KSTATE='2' AND PD_YSTATE='2'";
                    sqllist.Add(sqlstr);
                }

                DBCallCommon.ExecuteTrans(sqllist);

                Response.Redirect("SM_WarehouseOUT_PK.aspx?FLAG=OPEN&&ID=" + Code);
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
            {
                LabelMessage.Text = "审核未通过：部分库存物料不存在！";
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
            {
                LabelMessage.Text = "审核未通过：部分库存物料数量小于出库数量！";
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 3)
            {
                LabelMessage.Text = "审核未通过：张(支)数不为1的物料，更改了长度！";
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 4)
            {
                LabelMessage.Text = "长宽和数量不相符！";
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -1)
            {
                LabelMessage.Text = "审核未通过：该出库单已经被审核！";
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

                Response.Write("<script type='text/javascript' language='javascript' >window.opener.location = window.opener.location.href;window.close();</script>");

                //ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>window.close();</script>");
                LabelMessage.Text = "Deleted!";
            }
            else if (LabelState.Text == "0")
            {
                LabelMessage.Text = "当前出库单尚未保存！";
                return;
            }
            else if (LabelState.Text == "2")
            {
                LabelMessage.Text = "当前出库单已审核无法删除！";
                return;
            }
        }

        //反审当前当前出库单，调用出库单反审存储过程，反审前先判断是否满足反审条件。
        protected void AntiVerify_Click(object sender, EventArgs e)
        {
            List<string> sqllist = new List<string>();

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
                string sqlstr = string.Empty;

                if (LabelCode.Text.Trim().Contains('R'))
                {
                    sqlstr = "update TBWS_INVENTORYSCHEMA set PD_YSTATE='1' where PD_CODE='" + LabelCode.Text.Split('R')[0] + "'";

                    sqllist.Add(sqlstr);

                    sqlstr = "update TBWS_INVENTORYSCHEMA set PD_DONESTATE='2' where PD_CODE='" + LabelCode.Text.Split('R')[0] + "' OR PD_KSTATE!='2'  OR  PD_YSTATE!='2'";

                    sqllist.Add(sqlstr);
                }
                else
                {
                    sqlstr = "update TBWS_INVENTORYSCHEMA set PD_KSTATE='1' where PD_CODE='" + LabelCode.Text + "'";

                    sqllist.Add(sqlstr);

                    sqlstr = "update TBWS_INVENTORYSCHEMA set PD_DONESTATE='2' where PD_CODE='" + LabelCode.Text + "' OR PD_KSTATE!='2'  OR  PD_YSTATE!='2'";

                    sqllist.Add(sqlstr);

                   
                }

                DBCallCommon.ExecuteTrans(sqllist);


                Response.Redirect("SM_WarehouseOUT_PK.aspx?FLAG=OPEN&&ID=" + LabelCode.Text);

            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -1)
            {
                LabelMessage.Text = "反审核未通过：已存在相应的红联单据！";
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -2)
            {
                LabelMessage.Text = "反审核未通过：当前出库单未审核,无法反审！";
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

        protected void tostorge_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Warehouse_Query.aspx?FLAG=PUSHLLOUT");
        }

        protected void AdjustLenWid_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                ((TextBox)GridView1.Rows[i].FindControl("TextBoxLength")).Enabled = true;
                ((TextBox)GridView1.Rows[i].FindControl("TextBoxWidth")).Enabled = true;
            }
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridView1.Rows)
            {
                if ((gr.FindControl("LabelDQN") as Label).Text.Trim() == "1")
                {
                    ((TextBox)gr.FindControl("TextBoxLength")).Enabled = true;
                    //((TextBox)GridView1.Rows[i].FindControl("TextBoxWidth")).Enabled = true;
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

        protected void TextBoxSCZH_TextChanged(object sender, EventArgs e)
        {
            string engid = TextBoxSCZH.Text.Trim();

            //engid=engid.Split('-')[engid.Split('-').Length-1];
            //string sql = "select count(*)  FROM TBPM_TCTSASSGN WHERE TSA_FATHERNODE='0' and TSA_ID='" + engid + "'";
            //DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            //if (Convert.ToInt32(dt.Rows[0][0]) == 0)
            //{
            //    TextBoxSCZH.Text = string.Empty;
            //}

            engid = engid.Split('-')[engid.Split('-').Length - 1];
            string sql = "select TSA_PJNAME,TSA_ENGNAME,TSA_ID  FROM View_TM_TaskAssign WHERE TSA_FATHERNODE='0' and TSA_ID='" + engid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                TextBoxSCZH.Text = dt.Rows[0]["TSA_PJNAME"] + "-" + dt.Rows[0]["TSA_ENGNAME"] + "-" + dt.Rows[0]["TSA_ID"];
                LabelSCZH.Text = TextBoxSCZH.Text;
            }
            else
            {
                TextBoxSCZH.Text = string.Empty;
            }
        }


    }
}
