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
    public partial class SM_Warehouse_MTOSplit : System.Web.UI.Page
    {
        private float ktzn = 0;
        private float ktfzn = 0;
        private float tzn = 0;
        private float tsn = 0;
        private int tqn = 0;
        private int tsqn = 0;
       
        
        
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
            string sql = "SELECT MTOCode AS Code,TargetCode AS TargetCode," +
                "TotalNote AS Abstract,Date AS Date," +
                "DepCode AS DepCode,Dep AS Dep,PlanerCode AS PlanerCode,Planer AS Planer," +
                "DocCode AS DocCode,Doc AS Doc," +
                "VerifierCode AS VerifierCode,Verifier AS Verifier,left(VerifyDate,10) AS ApprovedDate," +
                "TotalState AS State FROM View_SM_MTO WHERE MTOCode='" + code + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                LabelState.Text = dr["State"].ToString(); //总的状态           
                LabelCode.Text = dr["Code"].ToString(); //编号
                LabelDate.Text = dr["Date"].ToString(); 
                LabelNewCode.Text = generateSubCode();  //生成新单号                            
                LabelAbstract.Text = dr["Abstract"].ToString();
                LabelDep.Text = dr["Dep"].ToString();
                LabelDepCode.Text = dr["DepCode"].ToString();                
                LabelPlaner.Text = dr["Planer"].ToString();
                LabelPlanerCode.Text = dr["PlanerCode"].ToString();
                LabelDoc.Text = dr["Doc"].ToString();
                LabelDocCode.Text = dr["DocCode"].ToString();
                LabelVerifier.Text = dr["Verifier"].ToString();
                LabelVerifierCode.Text = dr["VerifierCode"].ToString();
                LabelApproveDate.Text = dr["ApprovedDate"].ToString();
                LabelTargetCode.Text = dr["TargetCode"].ToString();
                
            }
            dr.Close();
            sql = "SELECT UniqueCode AS UniqueID,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,Length AS Length,Width AS Width," +
                "LotNumber AS LotNumber,Unit AS Unit,cast(TZNUM as float) AS TZN,cast(TZNUM as float) AS SN,cast(TZFZNUM as float) AS TZQuantity,cast(TZFZNUM as float) AS SQN," +
                "WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,KTNUM AS KTNum,KTFZNUM AS KTQuantity," +
                "LocationCode AS PositionCode,Location AS Position,OrderCode AS OrderCode,SQCODE AS SQCODE," +
                "PlanMode AS PlanMode,PTCTo AS PTCTo,DetailNote AS Comment,DetailState AS DetailState,PTCFrom as FromPTC FROM View_SM_MTO WHERE MTOCode='" + code + "' AND DetailState='SPLIT" + Session["UserID"].ToString() + "'";
            DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
            GridView1.DataSource = tb;
            GridView1.DataBind();
            sql = "UPDATE TBWS_MTODETAIL SET MTO_STATE='' WHERE MTO_STATE='SPLIT" + Session["UserID"].ToString() + "' AND MTO_CODE='" + code + "'";
            DBCallCommon.ExeSqlText(sql);
            LabelMessage.Text = "数据加载成功";
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ktzn += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "KTNum")); //可调整数量
                ktfzn += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "KTQuantity")); //可调整张(支)数
                tzn += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "TZN"));
                tsn += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "SN"));
                tqn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TZQuantity"));
                tsqn += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SQN"));
             
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.Cells[11].FindControl("LabelTotalKTNum")).Text = ktzn.ToString();
                ((Label)e.Row.Cells[12].FindControl("LabelTotalKTQuantity")).Text = ktfzn.ToString();
                ((Label)e.Row.Cells[13].FindControl("LabelTotalTZNum")).Text = tzn.ToString();
                ((Label)e.Row.Cells[14].FindControl("LabelTotalSN")).Text = tsn.ToString();
                ((Label)e.Row.Cells[15].FindControl("LabelTotalTZQuantity")).Text = tqn.ToString();
                ((Label)e.Row.Cells[16].FindControl("LabelTotalSQN")).Text = tsqn.ToString();
                
             
            }
        }

        protected string generateSubCode()
        {

            /*
             * sql语句
             */

            List<string> lt = new List<string>();

            string sql = "SELECT MTO_CODE FROM TBWS_MTO where MTO_CODE like '" + LabelCode.Text + "S%' ";

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    lt.Add(sdr["MTO_CODE"].ToString());
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
                int tempnum = Convert.ToInt32((tempstr.Substring(tempstr.IndexOf('S',0)+1, tempstr.Length-tempstr.IndexOf('S',0)-1)));
                tempnum++;
                tempstr = tempstr.Substring(0, tempstr.IndexOf('S', 0)+1) + tempnum.ToString();
                return tempstr;
            }
        }
        
        
        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Warehouse_MTOAdjust.aspx?FLAG=READ&&ID=" + LabelCode.Text.Trim());
        }

        //拆分单据


        /*
         * 不容许将原单全部拆给子单
         */

        protected void Confirm_Click(object sender, EventArgs e)
        {
            string MTOCode = Request.QueryString["ID"].ToString();//获取MTO单号
            bool HasError = false;
            int ErrorType = 0;

            List<string> sqllist = new List<string>();
            string sql = "";

            string Code = LabelCode.Text;
            string NewCode = LabelNewCode.Text;           
            string Date = LabelDate.Text;           
            string Abstract = LabelAbstract.Text; //主表备注
            string DepCode = LabelDepCode.Text;
            string Dep = LabelDep.Text;
            string PlanerCode = LabelPlanerCode.Text;
            string Planer = LabelPlaner.Text;
            string Doc = LabelDoc.Text;
            string DocCode = LabelDocCode.Text;            
            string Verifier = LabelVerifier.Text;
            string VerifierCode = LabelVerifierCode.Text;
            string ApproveDate = LabelApproveDate.Text;
            string TargetCode = LabelTargetCode.Text;
            string State = "2";          

            sql = "INSERT INTO TBWS_MTO (MTO_CODE,MTO_TARGETID," +
                             "MTO_DATE,MTO_DOC,MTO_VERIFIER,MTO_VERIFYDATE," +
                             "MTO_STATE,MTO_RealTime,MTO_NOTE,MTO_PLANER,MTO_DEP) VALUES('" + NewCode + "'," +
                             "'" + TargetCode + "','" + Date + "','" + DocCode + "'," +
                             "'" + VerifierCode + "','" + ApproveDate + "','" + State + "',convert(varchar(50), getdate(), 120)," +
                             "'" + Abstract + "','" + PlanerCode + "','" + DepCode + "')";
            
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
                string FromPTC = ((Label)GridView1.Rows[i].FindControl("LabelFromPTC")).Text;
                string KTZNum = ((Label)GridView1.Rows[i].FindControl("LabelKTNum")).Text;  //可调整数量
                string KTZQuantity = ((Label)GridView1.Rows[i].FindControl("LabelKTQuantity")).Text; //可调整张（支）数
                float TZN = Convert.ToSingle(((Label)GridView1.Rows[i].FindControl("LabelTZN")).Text); 
                float SN = Convert.ToSingle(((TextBox)GridView1.Rows[i].FindControl("TextBoxSN")).Text);

                if (SN < 0.000099999)
                {
                    
                    HasError = true;
                    ErrorType = 1;
                    break;
                }

                Int32 TZQuantity = Convert.ToInt32(((Label)GridView1.Rows[i].FindControl("LabelTZQuantity")).Text); //调整张（支）数
                Int32 SQN = Convert.ToInt32(((HtmlInputText)GridView1.Rows[i].FindControl("InputSQN")).Value); //拆分张（支）数
                string PTCTo = ((Label)GridView1.Rows[i].FindControl("LabelPTCTo")).Text;   
                string Warehouse = ((Label)GridView1.Rows[i].FindControl("LabelWarehouse")).Text;
                string WarehouseCode = ((Label)GridView1.Rows[i].FindControl("LabelWarehouseCode")).Text;
                string Position = ((Label)GridView1.Rows[i].FindControl("LablePosition")).Text;
                string PositionCode = ((Label)GridView1.Rows[i].FindControl("LabelPositionCode")).Text;
                string OrderCode = ((Label)GridView1.Rows[i].FindControl("LabelOrderCode")).Text;
                string PlanMode = ((Label)GridView1.Rows[i].FindControl("LabelPlanMode")).Text;
                string Comment = ((Label)GridView1.Rows[i].FindControl("LabelComment")).Text;
                string SQCODE = ((Label)GridView1.Rows[i].FindControl("LabelSQCODE")).Text;

                sql = "INSERT INTO TBWS_MTODETAIL(MTO_UNIQUEID,MTO_CODE,MTO_MARID,MTO_PCODE,MTO_FIXED,MTO_LENGTH," +
                "MTO_WIDTH,MTO_KTNUM,MTO_KTFZNUM,MTO_TZNUM,MTO_TZFZNUM,MTO_TOPTCODE,MTO_PMODE,MTO_LOCATION,MTO_WAREHOUSE,MTO_SQCODE," +
                "MTO_ORDERID,MTO_FROMPTCODE,MTO_NOTE,MTO_STATE) VALUES('" + UniqueID + "','" + NewCode + "','" +
                MarCode + "','" + LotNum +"','" + Fixed + "'," + Length + "," + Width + "," + KTZNum + "," + KTZQuantity + "," +
                SN + "," + SQN + ",'" + PTCTo + "','" + PlanMode + "','" + PositionCode + "','" + WarehouseCode +
                "','" + SQCODE + "','" + OrderCode + "','" + FromPTC + "','" + Comment + "','')";
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
                SqlCommand cmd = new SqlCommand("MTOSplit", con);
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

                    sql = "DELETE FROM TBWS_MTODETAIL WHERE MTO_CODE='" + NewCode + "'";
                    sqllist.Add(sql);
                    sql = "DELETE FROM TBWS_MTO WHERE MTO_CODE='" + NewCode + "'";
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
                    Response.Redirect("SM_Warehouse_MTOSplitResult.aspx?IDC=" + NewCode + "&&IDP=" + Code + "&&RES=S");
                }
            }
        }
    
    }
}
