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
    public partial class SM_WarehouseOUT_OPSplit : System.Web.UI.Page
    {
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
        //领料部门

        protected void GetDep()
        {
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
            string sql = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO WHERE DEP_FATHERID='04'";
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
            string code = Request.QueryString["ID"].ToString();
            if (code.Contains("QT"))
            {
                LabelTittle.Text = "其他出库";
            }
            else 
            {
                LabelTittle.Text = "重机领料单";
            }
            
            string selsql = "select OP_HSFLAG from TBWS_OUT where OP_CODE='" + code + "'";

            if (DBCallCommon.GetDTUsingSqlText(selsql).Rows.Count > 0)
            {
                if (DBCallCommon.GetDTUsingSqlText(selsql).Rows[0]["OP_HSFLAG"].ToString() == "1")
                {

                    TextBoxSCZH.Enabled = false;

                }

            }


            string sql = "SELECT Top 1 id as TrueCode,OutCode AS Code,DepCode AS LLDepCode,Dep AS LLDep,Date AS Date," +
               "TSAID as SCZH,SenderCode AS SenderCode," +
               "Sender AS Sender,DocCode AS DocumentCode," +
               "Doc AS Document,VerifierCode AS VerifierCode," +
               "Verifier AS Verifier,LEFT(ApprovedDate,10) AS ApproveDate,ROB AS Colour,TotalState AS State,BillType," +
               "TotalNote AS Comment,OP_ZXMC,OP_PAGENUM,OP_NOTE1  FROM View_SM_OUT WHERE OutCode='" + code + "' and DetailState='SPLIT" + Session["UserID"].ToString() + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {

                LabelCode.Text = dr["Code"].ToString();

                LabelNewCode.Text = generateSubCode();

                LabelState.Text = dr["State"].ToString();

                InputColour.Value = dr["Colour"].ToString();

                TextBoxDate.Text = dr["Date"].ToString();

                

                try { DropDownListDep.Items.FindByValue(dr["LLDepCode"].ToString()).Selected = true; }
                catch { }

               

                TextBoxSCZH.Text = dr["SCZH"].ToString();

                LabelSCZH.Text = dr["SCZH"].ToString();

                TextBoxNOTE1.Text = dr["OP_NOTE1"].ToString();
                TextBoxPageNum.Text ="1";

            
                try { DropDownListBZ.Items.FindByValue(dr["Comment"].ToString()).Selected = true; }
                catch { }

                //子项名称
                try { DropDownListZXMC.Items.FindByValue(dr["OP_ZXMC"].ToString()).Selected = true; }
                catch { }

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
                "LotNumber AS LotNumber,Unit AS Unit,cast(RealNumber as float) AS RN,cast(RealNumber as float) AS SN,cast(RealSupportNumber as float) AS RQN,cast(RealSupportNumber as float) AS SQN," +
                "cast(UnitPrice as float) AS UnitPrice,cast(Amount as float) AS Amount,WarehouseCode AS WarehouseCode," +
                "Warehouse AS Warehouse,LocationCode AS PositionCode,Location AS Position," +
                "PlanMode AS PlanMode,PTC AS PTC,OrderCode AS OrderID,DetailNote AS Comment,OP_BSH as BSH " +
                "FROM View_SM_OUT WHERE OutCode='" + code + "' and DetailState='SPLIT" + Session["UserID"].ToString() + "' order by UniqueCode ";
            
            DataTable tb = DBCallCommon.GetDTUsingSqlText(sql);
            GridView1.DataSource = tb;
            GridView1.DataBind();

             sql = "UPDATE TBWS_OUTDETAIL SET OP_STATE='' WHERE OP_STATE='SPLIT" + Session["UserID"].ToString() + "'";           
            DBCallCommon.ExeSqlText(sql);
        }

        protected string generateSubCode()
        {

            /*
             * sql语句
             */

            List<string> lt = new List<string>();

            string sql = "SELECT OP_CODE FROM TBWS_OUT where OP_CODE LIKE '" + LabelCode.Text + "S%' ";

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    lt.Add(sdr["OP_CODE"].ToString());
                }
            }

            string[] llidlist = lt.ToArray();

         
            if (llidlist.Count<string>() == 0)
            {
                return LabelCode.Text + "S1";
            }
            else
            {
                string tempstr = llidlist.Max<string>();
                if (tempstr.Contains('R'))
                {
                    string[] arr = tempstr.Split('R');
                    tempstr = arr[0];
                }
                int tempnum = Convert.ToInt32((tempstr.Substring(tempstr.IndexOf('S', 0) + 1, tempstr.Length - tempstr.IndexOf('S', 0) - 1)));
                tempnum++;
                tempstr = tempstr.Substring(0, tempstr.IndexOf('S', 0) + 1) + tempnum.ToString();
                return tempstr;
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



        /*
         * 不容许将原单全部拆给子单
         */

        protected void Confirm_Click(object sender, EventArgs e)
        {

            bool HasError = false;
            int ErrorType = 0;

            List<string> sqllist = new List<string>();

            string sql = "";
            string Code = LabelCode.Text;
            string NewCode = LabelNewCode.Text;

            string State = LabelState.Text;
            string Colour = InputColour.Value;
            string Date = TextBoxDate.Text;
            string LLDepCode = DropDownListDep.SelectedValue;
            string SCZH = TextBoxSCZH.Text;

            string Comment = DropDownListBZ.SelectedValue;

            string ZXMC = DropDownListZXMC.SelectedValue;

            string DocCode = LabelDocCode.Text;
            string SendClerkCode = DropDownListSender.SelectedValue;
            string VerifierCode = LabelVerifierCode.Text;
            string ApproveDate = LabelApproveDate.Text;
            string NOTE1 = TextBoxNOTE1.Text;
            string BillType = string.Empty;

            if (LabelBillType.Text.Trim() == string.Empty || LabelBillType.Text.Trim() == "1") //重机领料单
            {
                BillType = "1";
            }
            else if (LabelBillType.Text.Trim() == "4") //特殊退库
            {
                BillType = "4";
            }
            else if (LabelBillType.Text.Trim() == "6") //其他出库
            {
                BillType = "6";
            }

            int pagenum = 0;

            try
            {
                pagenum = Convert.ToInt32(TextBoxPageNum.Text.Trim());
            }

            catch { pagenum = 0; }

            sql = "select count(*) from TBWS_OUT where OP_CODE='" + NewCode + "'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() == "0")
            {
                sql = "exec ResetSeed @tablename=TBWS_OUT";

                sqllist.Add(sql);

                sql = "INSERT INTO TBWS_OUT(OP_CODE,OP_DEP,OP_DATE,OP_TSAID," +
                    "OP_SENDER,OP_DOC,OP_VERIFIER,OP_VERIFYDATE,OP_HSFLAG,OP_ROB," +
                    "OP_STATE,OP_NOTE,OP_BILLTYPE,OP_RealTime,OP_ZXMC,OP_PAGENUM,OP_NOTE1) VALUES('" + NewCode + "','" + LLDepCode + "','" + Date + "','" + SCZH + "','" +
                    SendClerkCode + "','" + DocCode + "','" + VerifierCode + "','" + ApproveDate + "','0','" +
                    Colour + "','2','" + Comment + "','" + BillType + "',convert(varchar(50),getdate(),120),'" + ZXMC + "','" + pagenum + "','" + NOTE1 + "')";

                sqllist.Add(sql);
            }
            else
            {

                sql = "update TBWS_OUT set OP_DEP='" + LLDepCode + "',OP_DATE='" + Date + "',OP_TSAID='" + SCZH + "',";
                sql += "OP_SENDER='" + SendClerkCode + "',OP_DOC='" + DocCode + "',OP_VERIFIER='" + VerifierCode + "',OP_VERIFYDATE='" + ApproveDate + "',OP_HSFLAG='0',";
                sql += "OP_ROB='" + Colour + "',OP_STATE='2',OP_NOTE='" + Comment + "',OP_BILLTYPE='" + BillType + "',OP_RealTime=convert(varchar(50),getdate(),120),OP_ZXMC='" + ZXMC + "',OP_PAGENUM='" + pagenum + "' where OP_CODE='" + NewCode + "'";

                sqllist.Add(sql);
            }

            sql = "DELETE FROM TBWS_OUTDETAIL WHERE OP_CODE='" + NewCode + "'";

            sqllist.Add(sql);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string UniqueID = ((Label)this.GridView1.Rows[i].FindControl("LabelUniqueID")).Text;

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

                string PositionCode = ((HtmlInputText)this.GridView1.Rows[i].FindControl("InputPositionCode")).Value;

                string PTC = ((Label)this.GridView1.Rows[i].FindControl("LabelPTC")).Text;

                //保存仓库号时需要重新获取


                //@mid+@mptc+@mlotnum+CAST(@mlength AS VARCHAR(50))+CAST(@mwidth AS VARCHAR(50))+@mstid+@mwlid（物料代码+计划跟踪号+批号+长+宽+仓库+仓位）

                //新的仓库唯一号
                //string SQCODE = MaterialCode + PTC + LotNumber + Convert.ToString(Length) + Convert.ToString(Width) + WarehouseOutCode + PositionCode;  

                float DN = 0;
                try
                {
                    float temp = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxSN")).Text);
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
                    float temp = Convert.ToSingle(((TextBox)this.GridView1.Rows[i].FindControl("TextBoxSN")).Text);
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
                if (RN ==0)
                {

                    HasError = true;
                    ErrorType = 1;
                    break;
                }

                Int32 DQN = 0;
                try
                {
                    Int32 temp = Convert.ToInt32(((TextBox)this.GridView1.Rows[i].FindControl("InputSQN")).Text);
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
                    Int32 temp = Convert.ToInt32(((TextBox)this.GridView1.Rows[i].FindControl("InputSQN")).Text);
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
                string bsh = ((Label)this.GridView1.Rows[i].FindControl("LabelBSH")).Text;

                sql = "INSERT INTO TBWS_OUTDETAIL(OP_CODE,OP_UNIQUEID,OP_SQCODE,OP_MARID," +
                    "OP_FIXED,OP_DUELENGTH,OP_LENGTH,OP_WIDTH,OP_LOTNUM,OP_DUENUM,OP_REALNUM,OP_DUEFZNUM,OP_REALFZNUM," +
                    "OP_UPRICE,OP_AMOUNT,OP_WAREHOUSE,OP_LOCATION,OP_PMODE," +
                    "OP_PTCODE,OP_ORDERID,OP_NOTE,OP_STATE,OP_BSH) VALUES('" + NewCode + "','" + UniqueID + "','" + SQCODE + "','" +
                    MaterialCode + "','" +
                    Fixed + "','" + DueLength + "','" + Length + "','" +
                    Width + "','" + LotNumber + "','" + DN + "','" + RN + "','" + DQN + "','" + RQN + "','0','0','" +
                    WarehouseOutCode + "','" + PositionCode + "','" + PlanMode + "','" +
                    PTC + "','" + OrderID + "','" + Note + "','','" + bsh + "')";
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
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "error", script, true);


                }

            }
            else
            {
                DBCallCommon.ExecuteTrans(sqllist);

                sql = DBCallCommon.GetStringValue("connectionStrings");
                SqlConnection con = new SqlConnection(sql);
                con.Open();
                SqlCommand cmd = new SqlCommand("splitout", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@OutCode", SqlDbType.VarChar, 50);				//增加参数入库单号@InCode
                cmd.Parameters["@OutCode"].Value = Code;						//为参数初始化
                cmd.Parameters.Add("@sOutCode", SqlDbType.VarChar, 50);				//增加参数入库单号@InDate
                cmd.Parameters["@sOutCode"].Value = NewCode;							//为参数初始化
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
                    Response.Redirect("SM_WarehouseOut_LL_ActionResult.aspx?IDC=" + NewCode + "&&IDP=" + Code + "&&RES=S");
                }
            }

        }

        protected void Cancel_Click(object sender, EventArgs e)
        {

            if (LabelCode.Text.Contains("QT"))
            {
                Response.Redirect("SM_WarehouseOUT_QT.aspx?FLAG=OPEN&&ID=" + LabelCode.Text.Trim());

            }
            else 
            {
                Response.Redirect("SM_WarehouseOUT_LL.aspx?FLAG=OPEN&&ID=" + LabelCode.Text.Trim());

            }
            
        }
    }
}
