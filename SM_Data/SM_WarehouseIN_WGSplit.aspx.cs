using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Data.SqlClient;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_WarehouseIN_WGSplit : System.Web.UI.Page
    {
        private float tn = 0;
        private float tsn = 0;
        private int tqn = 0;
        private int tsqn = 0;
        private float ta = 0;
        private float tsa = 0;
        private float tcta = 0;
        private float tscta = 0;
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initial();
            }
        }

        protected void initial()
        {
            string code = Request.QueryString["ID"].ToString();
            string sql = "SELECT WG_CODE AS Code,(case when WG_BILLTYPE='3' or WG_BILLTYPE='4' then WG_COMPANYCODE else Supplier end) AS SupplierCode,(case when WG_BILLTYPE='3' or WG_BILLTYPE='4' then WG_COMPANY else SupplierName end) as Supplier," +
                "WG_WAREHOUSE AS WarehouseCode,WS_NAME AS Warehouse,WG_ABSTRACT AS Abstract,WG_DATE AS Date," +
                "Dep AS DepCode,DepName AS Dep,Clerk AS ClerkCode,ClerkName AS Clerk," +
                "WG_DOC AS DocCode,DocName AS Doc,WG_REVEICER AS AcceptanceCode,ReveicerName AS Acceptance," +
                "WG_VERIFIER AS VerifierCode,VerfierName AS Verifier,left(WG_VERIFYDATE,10) AS ApprovedDate," +
                "WG_STATE AS State,WG_HSFLAG,WG_TEARFLAG AS Split ,WG_CAVFLAG AS Articulation,WG_ROB AS Colour,WG_HDBH FROM View_SM_IN WHERE WG_CODE='" + code + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                LabelState.Text = dr["State"].ToString();

                LabelHSState.Text = dr["WG_HSFLAG"].ToString();//核算状态

                LabelCode.Text = dr["Code"].ToString();
                LabelDate.Text = dr["Date"].ToString();
                LabelNewCode.Text = generateSubCode();
                LabelSupplier.Text = dr["Supplier"].ToString();
                LabelSupplierCode.Text = dr["SupplierCode"].ToString();
                LabelWarehouse.Text = dr["Warehouse"].ToString();
                LabelWarehouseCode.Text = dr["WarehouseCode"].ToString();
                LabelAbstract.Text = dr["Abstract"].ToString();
                LabelDep.Text = dr["Dep"].ToString();
                LabelDepCode.Text = dr["DepCode"].ToString();                
                LabelClerk.Text = dr["Clerk"].ToString();
                LabelClerkCode.Text = dr["ClerkCode"].ToString();
                LabelDoc.Text = dr["Doc"].ToString();
                LabelDocCode.Text = dr["DocCode"].ToString();
                LabelAcceptance.Text = dr["Acceptance"].ToString();
                LabelAcceptanceCode.Text = dr["AcceptanceCode"].ToString();
                LabelVerifier.Text = dr["Verifier"].ToString();
                LabelVerifierCode.Text = dr["VerifierCode"].ToString();
                LabelApproveDate.Text = dr["ApprovedDate"].ToString();
                LabelHDBH.Text = dr["WG_HDBH"].ToString();
            }
            dr.Close();
            sql = "SELECT WG_UNIQUEID AS UniqueID,WG_MARID AS MaterialCode,MNAME AS MaterialName," +
                "CAIZHI AS Attribute,GB AS GB,GUIGE AS MaterialStandard,WG_FIXEDSIZE AS Fixed,WG_LENGTH AS Length,WG_WIDTH AS Width," +
                "WG_LOTNUM AS LotNumber,CGDW AS Unit,cast(WG_RSNUM as float) AS RN,cast(WG_RSNUM as float) AS SN,cast(WG_RSFZNUM as float) AS Quantity,cast(WG_RSFZNUM as float) AS SQN," +
                "cast(WG_UPRICE as float) AS UnitPrice,WG_TAXRATE AS TaxRate,cast(WG_CTAXUPRICE as float) AS CTUP," +
                "cast(WG_AMOUNT as float) AS Amount,cast(WG_AMOUNT as float) AS SAmount,cast(WG_CTAMTMNY as float) AS CTA,cast(WG_CTAMTMNY as float) AS SCTA,WG_WAREHOUSE as WarehouseCode,WS_NAME as Warehouse," +
                "WG_LOCATION AS PositionCode,WL_NAME AS Position,WG_ORDERID AS OrderCode," +
                "WG_PMODE AS PlanMode,WG_PTCODE AS PTC,WG_NOTE AS Comment,WG_STATE AS State,WG_CGMODE as CGMODE FROM View_SM_IN WHERE WG_CODE='" + code + "' AND DetailState='SPLIT" + Session["UserID"].ToString() + "'";
            DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
            GridView1.DataSource = tb;
            GridView1.DataBind();
            sql = "UPDATE TBWS_INDETAIL SET WG_STATE='' WHERE WG_STATE='SPLIT" + Session["UserID"].ToString() + "' AND WG_CODE='" + code + "'";
            DBCallCommon.ExeSqlText(sql);
            LabelMessage.Text = "数据加载成功";
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                tn += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "RN"));
                tsn += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "SN"));
                tqn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Quantity"));
                tsqn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SQN"));
                ta += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "Amount"));
                tsa += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "SAmount"));
                tcta += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "CTA"));
                tscta += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "SCTA"));
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.Cells[11].FindControl("LabelTotalNum")).Text = tn.ToString();
                ((Label)e.Row.Cells[12].FindControl("LabelTotalSN")).Text = tsn.ToString();
                ((Label)e.Row.Cells[13].FindControl("LabelTotalQuantity")).Text = tqn.ToString();
                ((Label)e.Row.Cells[14].FindControl("LabelTotalSQN")).Text = tsqn.ToString();
                ((Label)e.Row.Cells[18].FindControl("LabelTotalAmount")).Text = ta.ToString();
                ((Label)e.Row.Cells[19].FindControl("LabelTotalSAmount")).Text = tsa.ToString();
                ((Label)e.Row.Cells[20].FindControl("LabelTotalCTA")).Text = tcta.ToString();
                ((Label)e.Row.Cells[21].FindControl("LabelTotalSCTA")).Text = tscta.ToString();
            }
        }

        protected string generateSubCode()
        {

            /*
             * sql语句
             */

            List<string> lt = new List<string>();

            string sql = "SELECT WG_CODE FROM TBWS_IN where WG_CODE like '" + LabelCode.Text + "S%' ";

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    lt.Add(sdr["WG_CODE"].ToString());
                }
            }

            string[] llidlist = lt.ToArray();

            //LinqDataContext db = new LinqDataContext();
            //var result = from u in db.TBWS_INSTWGTOTALs
            //             where u.WG_CODE.StartsWith(LabelCode.Text + "S") 
            //             where u.WG_CODE.Contains("R")==false
            //             select u.WG_CODE;
            //string[] llidlist = result.ToArray<string>();
            if (llidlist.Count<string>() == 0)
            {
                return LabelCode.Text + "S1";
            }
            else
            {
                string tempstr = llidlist.Max<string>();
                if (tempstr.Contains('R'))
                {
                   string[] arr=tempstr.Split('R');
                   tempstr=arr[0];
                }
                int tempnum = Convert.ToInt32((tempstr.Substring(tempstr.IndexOf('S',0)+1, tempstr.Length-tempstr.IndexOf('S',0)-1)));
                tempnum++;
                tempstr = tempstr.Substring(0, tempstr.IndexOf('S', 0)+1) + tempnum.ToString();
                return tempstr;
            }
        }
        
        
        protected void Cancel_Click(object sender, EventArgs e)
        {
            if((LabelCode.Text.Trim()).Contains('G'))
            {
                Response.Redirect("SM_WarehouseIN_WG.aspx?FLAG=READ&&ID=" + LabelCode.Text.Trim());
            }
            else 
            {
                Response.Redirect("SM_WarehouseIN_WG_WTB.aspx?FLAG=READ&&ID=" + LabelCode.Text.Trim());

            }
        }

        //拆分单据


        /*
         * 不容许将原单全部拆给子单
         */

        protected void Confirm_Click(object sender, EventArgs e)
        {
            string rukucode = Request.QueryString["ID"].ToString();//获取入库单号
            bool HasError = false;
            int ErrorType = 0;

            List<string> sqllist = new List<string>();
            string sql = "";

            string Code = LabelCode.Text;
            string NewCode = LabelNewCode.Text;
            string Supplier = LabelSupplier.Text;
            string Suppliercode = LabelSupplierCode.Text;
            string Date = LabelDate.Text;
            string WarehouseIn = LabelWarehouse.Text;
            string WarehouseInCode = LabelWarehouseCode.Text;
            string Abstract = LabelAbstract.Text;
            string DepCode = LabelDepCode.Text;
            string Dep = LabelDep.Text;
            string ClerkCode = LabelClerkCode.Text;
            string Clerk = LabelClerk.Text;
            string Doc = LabelDoc.Text;
            string DocCode = LabelDocCode.Text;            
            string Acceptance = LabelAcceptance.Text;
            string AcceptanceCode = LabelAcceptanceCode.Text;
            string Verifier = LabelVerifier.Text;
            string VerifierCode = LabelVerifierCode.Text;
            string ApproveDate = LabelApproveDate.Text;
            string HDBH = LabelHDBH.Text;
            string Colour = "0";
            string State = "2";
            string HSState = LabelHSState.Text.Trim();

            sql = "exec ResetSeed @tablename=TBWS_IN";
            sqllist.Add(sql);
            string strcode = rukucode.Substring(0,1);
            if(strcode=="G")
            {
                      sql = "INSERT INTO TBWS_IN (WG_CODE,WG_ABSTRACT," +
                             "WG_DATE,WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE," +
                             "WG_STATE,WG_HSFLAG,WG_TEARFLAG,WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_HDBH,WG_RealTime,WG_BILLTYPE,WG_COMPANY,WG_COMPANYCODE) VALUES('" + NewCode + "','" + Abstract + "','" + Date + "'," +
                             "'" + DocCode + "','" + AcceptanceCode + "','" + VerifierCode + "','" + ApproveDate + "'," +
                             "'" + State + "','" + HSState + "','0','0','" + Colour + "','0','" + HDBH + "',convert(varchar(50),getdate(),120),'0','" + Supplier + "','" + Suppliercode + "')";
            }
            else if (strcode == "B")
            {
                sql = "INSERT INTO TBWS_IN (WG_CODE,WG_ABSTRACT," +
                             "WG_DATE,WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE," +
                             "WG_STATE,WG_HSFLAG,WG_TEARFLAG,WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_HDBH,WG_RealTime,WG_BILLTYPE) VALUES('" + NewCode + "','" + Abstract + "','" + Date + "'," +
                             "'" + DocCode + "','" + AcceptanceCode + "','" + VerifierCode + "','" + ApproveDate + "'," +
                             "'" + State + "','" + HSState + "','0','0','" + Colour + "','0','" + HDBH + "',convert(varchar(50),getdate(),120),'1')";

            }
            ////else if(strcode=="C")
            //{
            //    sql = "INSERT INTO TBWS_IN (WG_CODE,WG_WAREHOUSE,WG_ABSTRACT," +
            //                 "WG_DATE,WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE," +
            //                 "WG_STATE,WG_HSFLAG,WG_TEARFLAG,WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_HDBH,WG_RealTime,WG_BILLTYPE) VALUES('" + NewCode + "'," +
            //                 "'" + WarehouseInCode + "','" + Abstract + "','" + Date + "'," +
            //                 "'" + DocCode + "','" + AcceptanceCode + "','" + VerifierCode + "','" + ApproveDate + "'," +
            //                 "'" + State + "','" + HSState + "','0','0','" + Colour + "','0','" + HDBH + "',convert(varchar(50),getdate(),120),'2')";
            //}
            else if (strcode == "X")
            {
                sql = "INSERT INTO TBWS_IN (WG_CODE,WG_ABSTRACT," +
                             "WG_DATE,WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE," +
                             "WG_STATE,WG_HSFLAG,WG_TEARFLAG,WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_HDBH,WG_RealTime,WG_BILLTYPE,WG_COMPANY,WG_COMPANYCODE) VALUES('" + NewCode + "','" + Abstract + "','" + Date + "'," +
                             "'" + DocCode + "','" + AcceptanceCode + "','" + VerifierCode + "','" + ApproveDate + "'," +
                             "'" + State + "','" + HSState + "','0','0','" + Colour + "','0','" + HDBH + "',convert(varchar(50),getdate(),120),'3','" + Supplier + "','" + Suppliercode + "')";
 
            }
            else if (strcode == "T")
            {
                sql = "INSERT INTO TBWS_IN (WG_CODE,WG_ABSTRACT," +
                             "WG_DATE,WG_DOC,WG_REVEICER,WG_VERIFIER,WG_VERIFYDATE," +
                             "WG_STATE,WG_HSFLAG,WG_TEARFLAG,WG_CAVFLAG,WG_ROB,WG_GJSTATE,WG_HDBH,WG_RealTime,WG_BILLTYPE,WG_COMPANY,WG_COMPANYCODE) VALUES('" + NewCode + "','" + Abstract + "','" + Date + "'," +
                             "'" + DocCode + "','" + AcceptanceCode + "','" + VerifierCode + "','" + ApproveDate + "'," +
                             "'" + State + "','" + HSState + "','0','0','" + Colour + "','0','" + HDBH + "',convert(varchar(50),getdate(),120),'4','" + Supplier + "','" + Suppliercode + "')";

            }
            sqllist.Add(sql);

            

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                
                string UniqueID = ((Label)GridView1.Rows[i].FindControl("LabelUniqueID")).Text;
                string MarCode = ((Label)GridView1.Rows[i].FindControl("LabelMaterialCode")).Text;
                string MarName = ((Label)GridView1.Rows[i].FindControl("LabelMaterialName")).Text;
                string Standard = ((Label)GridView1.Rows[i].FindControl("LabelMaterialStandard")).Text;
                string Fixed = ((Label)GridView1.Rows[i].FindControl("LabelFixed")).Text;
                float Length = Convert.ToSingle(((Label)GridView1.Rows[i].FindControl("LabelLength")).Text);
                float Width = Convert.ToSingle(((Label)GridView1.Rows[i].FindControl("LabelWidth")).Text);           
                string GB = ((Label)GridView1.Rows[i].FindControl("LabelGB")).Text;
                string Attribute = ((Label)GridView1.Rows[i].FindControl("LabelAttribute")).Text;
                string LotNum = ((Label)GridView1.Rows[i].FindControl("LabelLotNumber")).Text;
                string Unit = ((Label)GridView1.Rows[i].FindControl("LabelUnit")).Text;
                float RN = Convert.ToSingle(((Label)GridView1.Rows[i].FindControl("LabelRN")).Text);

                float SN = Convert.ToSingle(((TextBox)GridView1.Rows[i].FindControl("TextBoxSN")).Text);

                if (SN < 0.000099999)
                {
                    
                    HasError = true;
                    ErrorType = 1;
                    break;
                }

                Int32 Quantity = Convert.ToInt32(((Label)GridView1.Rows[i].FindControl("LabelQuantity")).Text);
                Int32 SQN = Convert.ToInt32(((HtmlInputText)GridView1.Rows[i].FindControl("InputSQN")).Value);
                float UnitPrice = Convert.ToSingle(((Label)GridView1.Rows[i].FindControl("LabelUnitPrice")).Text);
                float TaxRate = Convert.ToSingle(((Label)GridView1.Rows[i].FindControl("LabelTaxRate")).Text);
                float CTUP = Convert.ToSingle(((Label)GridView1.Rows[i].FindControl("LabelCTUP")).Text);
                float Amount = Convert.ToSingle(((Label)GridView1.Rows[i].FindControl("LabelAmount")).Text);
                float SAmount = Convert.ToSingle(((HtmlInputText)GridView1.Rows[i].FindControl("InputSAmount")).Value);
                float CTA = Convert.ToSingle(((Label)GridView1.Rows[i].FindControl("LabelCTA")).Text);
                float SCTA = Convert.ToSingle(((HtmlInputText)GridView1.Rows[i].FindControl("InputSCTA")).Value);
                string Comment = ((Label)GridView1.Rows[i].FindControl("LabelComment")).Text;
                string Warehouse = ((Label)GridView1.Rows[i].FindControl("LabelWarehouse")).Text;
                string WarehouseCode = ((Label)GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;
                string Position = ((Label)GridView1.Rows[i].FindControl("LablePosition")).Text;
                string PositionCode = ((Label)GridView1.Rows[i].FindControl("LabelPositionCode")).Text;
                string OrderCode = ((Label)GridView1.Rows[i].FindControl("LabelOrderCode")).Text;
                string PlanMode = ((Label)GridView1.Rows[i].FindControl("LabelPlanMode")).Text;
                string CGMode = ((Label)GridView1.Rows[i].FindControl("LabelCGMode")).Text;
                string PTC = ((Label)GridView1.Rows[i].FindControl("LabelPTC")).Text;

                sql = "INSERT INTO TBWS_INDETAIL(WG_UNIQUEID,WG_CODE,WG_MARID,WG_LOTNUM,WG_FIXEDSIZE,WG_LENGTH," +
                "WG_WIDTH,WG_RSNUM,WG_RSFZNUM,WG_UPRICE,WG_TAXRATE,WG_CTAXUPRICE,WG_AMOUNT,WG_CTAMTMNY,WG_LOCATION," +
                "WG_ORDERID,WG_PMODE,WG_PTCODE,WG_NOTE,WG_STATE,WG_CGMODE,WG_WAREHOUSE) VALUES('" + UniqueID + "','" + NewCode + "','" +
                MarCode + "','" + LotNum +"','" + Fixed + "','" + Length + "','" + Width + "'," + SN + "," + SQN + "," +
                UnitPrice + ",'" + TaxRate + "'," + CTUP + "," + SAmount + "," + SCTA + ",'" + PositionCode +
                "','" + OrderCode + "','" + PlanMode + "','" + PTC + "','" + Comment + "','','" + CGMode + "','" + WarehouseInCode + "')";
                sqllist.Add(sql);
            }

            if (HasError)
            {
                sqllist.Clear();

                if (ErrorType == 1)
                {
                    LabelMessage.Text = "拆分条目条目为0，单据不能审核！";

                    string script = @"alert('拆分条目条目为0，单据不能审核！');";

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"error",script, true);
                
                
                }
               
            }
            else
            { 
                DBCallCommon.ExecuteTrans(sqllist);

                sql = DBCallCommon.GetStringValue("connectionStrings");
                SqlConnection con = new SqlConnection(sql);
                con.Open();
                SqlCommand cmd = new SqlCommand("splitin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@InCode", SqlDbType.VarChar, 50);				//增加参数入库单号@InCode
                cmd.Parameters["@InCode"].Value = Code;						//为参数初始化
                cmd.Parameters.Add("@sInCode", SqlDbType.VarChar, 50);				//增加参数入库单号@InDate
                cmd.Parameters["@sInCode"].Value = NewCode;							//为参数初始化
                cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;			//增加返回值参数@retVal
                cmd.ExecuteNonQuery();
                con.Close();


                if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -1)
                {
                    sqllist.Clear();

                    sql = "DELETE FROM TBWS_INDETAIL WHERE WG_CODE='" + NewCode + "'";
                    sqllist.Add(sql);
                    sql = "DELETE FROM TBWS_IN WHERE WG_CODE='" + NewCode + "'";
                    sqllist.Add(sql);

                    DBCallCommon.ExecuteTrans(sqllist);

                    string script = @"alert('不容许将母单数据全部拆给子单！');";

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);
                }
                else
                {
                    /*
                     * 拆分结果显示
                     */
                    Response.Redirect("SM_WarehouseIn_ActionResult.aspx?IDC=" + NewCode + "&&IDP=" + Code + "&&RES=S");
                }
            }
        }
    }
}
