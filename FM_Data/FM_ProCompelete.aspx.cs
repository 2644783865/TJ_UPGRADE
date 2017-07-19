using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_ProCompelete : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        List<string> str = new List<string>();
        List<string> sql = new List<string>();
        string sqlText;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "任务号管理";
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                UserID.Value = Session["UserID"].ToString();
                this.GetBoundData();
            }

            CheckUser(ControlFinder);
        }
        #region 分页
        protected void GetBoundData()
        {
            InitPager();
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        private void InitPager()
        {
            pager.TableName = "View_TM_TaskAssign";
            pager.PrimaryKey = "TSA_ID";
            pager.ShowFields = "*";
            pager.OrderField = "TSA_PJID desc,TSA_ID";
            pager.StrWhere = CreateConStr();
            pager.OrderType = 0;//按任务名称升序排列
            pager.PageSize = 10;
            UCPaging1.PageSize = pager.PageSize;
        }

        private string CreateConStr()
        {
            string sql = string.Empty;
            string userID = UserID.Value;
            string status = rbl_status.SelectedValue;
            sql = "TSA_STATE='" + status + "' ";

            switch (ddlSearch.SelectedValue)
            {
                case "1":
                    sql += "and TSA_PJID LIKE '%" + searchcontent.Text.Trim() + "%'";
                    break;
                case "2":
                    sql += "and TSA_ENGNAME LIKE '%" + searchcontent.Text.Trim() + "%'";
                    break;
                case "3":
                    sql += "and CM_PROJ LIKE '%" + searchcontent.Text.Trim() + "%'";
                    break;
                case "4":
                    sql += "and TSA_ID LIKE '%" + searchcontent.Text.Trim() + "%'";
                    break;
            }
            return sql;
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetBoundData();
        }

        #endregion


        protected void btn_Search_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetBoundData();
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.ToString() == "DataRow")
            {
                #region MyRegion



                if (str.Count < 1)
                {

                    str.Add(e.Row.Cells[1].Text);

                }
                else
                {
                    if (str.Contains(e.Row.Cells[1].Text))
                    {

                        e.Row.Cells[1].Text = "";
                    }
                    else
                    {
                        str.Add(e.Row.Cells[1].Text);
                    }
                }

                #endregion
            }
        }
        protected void rbl_mytask_SelectedIndexChanged(object sender, EventArgs e)
        {

            UCPaging1.CurrentPage = 1;
            GetBoundData();
            if (rbl_status.SelectedValue == "1")
            {
                GridView1.Columns[8].Visible = true;
            }
            else
            {
                GridView1.Columns[8].Visible = false;
            }
        }
        protected void btnComplete_OnClick(object sender, EventArgs e)
        {
            string tsaId = (sender as LinkButton).CommandArgument.ToString();
            sqlText = "update TBPM_TCTSASSGN set TSA_STATE='2' where TSA_ID='" + tsaId + "'";
            //自动添加一个MTO调整单
            AddMTO(tsaId,sqlText);
        }
        #region 生成MTO调整单

        string TextBoxDate;
        string TextBoxPTCTo = "";
        string TextBoxComment;
        string LabelDoc;
        string LabelDocCode;
        string LabelVerifier;
        string LabelVerifierCode;
        string LabelApproveDate;
        string LabelClosingAccount;
        private void AddMTO(string tsa,string sql_Tct)
        {
            Initial(tsa);
            ClosingAccountDate(TextBoxDate);
            if (isLock() == false)
            {
                string script = @"alert('系统正在结账,请稍后...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                LabelMessage.Text = "系统正在结账,请稍后...";
                return;
            }
            else
            {
                string Code = GenerateCode();

                string sqlstate = "select mto_state from tbws_mto where mto_code='" + Code + "'";

                if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows.Count > 0)
                {
                    if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows[0]["mto_state"].ToString() == "2")
                    {

                        string script = @"alert('单据已审核，单据不能再审核！');";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                        return;
                    }
                }

                bool HasError = false;
                int ErrorType = 0;

                List<string> sqllist = new List<string>();
                string sql = "";

                string Date = TextBoxDate;
                string TargetCode = TextBoxPTCTo.Split('-')[TextBoxPTCTo.Split('-').Length - 1];
                string Comment = TextBoxComment;
                string PlanerCode = "";
                string DepCode = "";
                string DocCode = LabelDocCode;

                string VerifierCode = Session["UserID"].ToString();

                string ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                //if(LabelApproveDate.Text.Trim())

                //上一次的审核日期LabelApproveDate.Text.Trim()，可能有值，也可能为空，有值表示反审，无值表示第一次审核

                //如果核算之后，是不能反审的
                if (LabelClosingAccount == "NoTime")
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
                        if (LabelApproveDate.Trim().Length > 8)
                        {
                            //多次审核
                            if (Convert.ToInt32(LabelApproveDate.Trim().Substring(0, 4)) == System.DateTime.Now.Year && Convert.ToInt32(LabelApproveDate.Trim().Substring(5, 2)) == System.DateTime.Now.Month)
                            {
                                //是本月时间
                                ApproveDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
                        }
                    }

                }

                sql = "DELETE FROM TBWS_MTO WHERE MTO_CODE='" + Code + "'";
                sqllist.Add(sql);
                sql = "INSERT INTO TBWS_MTO(MTO_CODE,MTO_DATE,MTO_TARGETID," +
                    "MTO_DEP,MTO_PLANER,MTO_DOC,MTO_VERIFIER,MTO_VERIFYDATE," +
                    "MTO_STATE,MTO_NOTE,MTO_RealTime) VALUES('" + Code + "','" +
                    Date + "','" + TargetCode + "','" + DepCode + "','" + PlanerCode + "','" + DocCode + "','" +
                    VerifierCode + "','" + ApproveDate + "','1','" + Comment + "',convert(varchar(50),getdate(),120))";
                sqllist.Add(sql);
                sql = "DELETE FROM TBWS_MTODETAIL WHERE MTO_CODE='" + Code + "'";
                sqllist.Add(sql);
                for (int i = 0; i < mto.Rows.Count; i++)
                {
                    DataRow dr = mto.Rows[i];
                    string UniqueID = Code + (i + 1).ToString();
                    string SQCODE = dr["SQCODE"].ToString();
                    string MaterialCode = dr["MaterialCode"].ToString();
                    string Fixed = dr["Fixed"].ToString();
                    float Length = 0;
                    try { Length = Convert.ToSingle(dr["lLength"].ToString()); }
                    catch { }
                    float Width = 0;
                    try { Width = Convert.ToSingle(dr["Width"].ToString()); }
                    catch { }
                    string LotNumber = dr["LotNumber"].ToString();
                    string PTCFrom = dr["PTCFrom"].ToString();
                    string WarehouseCode = dr["WarehouseCode"].ToString();

                    string PositionCode = dr["PositionCode"].ToString();

                    if (PositionCode == string.Empty || PositionCode == "0")
                    {
                        HasError = true;
                        ErrorType = 3;
                        break;
                    }

                    float WN = 0;
                    try { WN = Convert.ToSingle(dr["WN"].ToString()); }
                    catch { }
                    Int32 WQN = 0;
                    try { WQN = Convert.ToInt32(dr["WQN"].ToString()); }
                    catch { }

                    string PTCTo = "备库-MTO" + (int.Parse(Code.Substring(6)) + i).ToString();
                   
                    if (PTCTo == "--请选择--" || PTCTo == string.Empty)
                    {
                        HasError = true;
                        ErrorType = 1;
                        break;
                    }

                    if (PTCTo == PTCFrom)
                    {
                        HasError = true;
                        ErrorType = 4;
                        break;
                    }
                    string PlanMode = dr["PlanMode"].ToString();
                    float AdjN = 0;
                    try { AdjN = Convert.ToSingle(dr["AdjN"].ToString()); }
                    catch { }

                    if (AdjN == 0)
                    {
                        HasError = true;
                        ErrorType = 2;
                        break;
                    }

                    Int32 AdjQN = 0;
                    try { AdjQN = Convert.ToInt32(dr["AdjQN"].ToString()); }
                    catch { }


                    string OrderID = dr["OrderID"].ToString();
                    string Note = dr["Note"].ToString();
                    sql = "INSERT INTO TBWS_MTODETAIL(MTO_UNIQUEID,MTO_CODE,MTO_SQCODE,MTO_MARID," +
                        "MTO_FIXED,MTO_LENGTH,MTO_WIDTH,MTO_PCODE,MTO_FROMPTCODE," +
                        "MTO_WAREHOUSE,MTO_LOCATION,MTO_KTNUM,MTO_KTFZNUM,MTO_TOPTCODE,MTO_TZNUM," +
                        "MTO_TZFZNUM,MTO_PMODE,MTO_ORDERID,MTO_NOTE,MTO_STATE) VALUES('" + UniqueID + "','" +
                        Code + "','" + SQCODE + "','" + MaterialCode + "','" + Fixed + "','" +
                        Length + "','" + Width + "','" + LotNumber + "','" + PTCFrom + "','" + WarehouseCode + "','" + PositionCode + "'," +
                        WN + "," + WQN + ",'" + PTCTo + "'," + AdjN + "," + AdjQN + ",'" + PlanMode + "','" +
                        OrderID + "','" + Note + "','')";
                    sqllist.Add(sql);
                }

                if (HasError)
                {
                    sqllist.Clear();

                    if (ErrorType == 1)
                    {
                        LabelMessage.Text = "计划跟踪号为空，单据不能审核！";

                        string script = @"alert('计划跟踪号为空，单据不能审核！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                    else if (ErrorType == 2)
                    {

                        LabelMessage.Text = "调整数量为0，单据不能审核！";

                        string script = @"alert('调整数量为0，单据不能审核！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                    else if (ErrorType == 3)
                    {

                        LabelMessage.Text = "仓位为空，单据不能审核！";

                        string script = @"alert('仓位为空，单据不能审核！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }
                    else if (ErrorType == 4)
                    {

                        LabelMessage.Text = "调整之后计划跟踪号，不能与调整之前的相同！";

                        string script = @"alert('调整之后计划跟踪号，不能与调整之前的相同！');";

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
                    }

                }
                else
                {
                    sqllist.Add(sql_Tct);
                    DBCallCommon.ExecuteTrans(sqllist);
                    sqllist.Clear();

                    sql = DBCallCommon.GetStringValue("connectionStrings");
                    SqlConnection con = new SqlConnection(sql);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("MTO", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MTOCode", SqlDbType.VarChar, 50);				//增加参数
                    cmd.Parameters["@MTOCode"].Value = Code;							//为参数初始化
                    cmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 0)
                    {
                        string action = "NOR";


                        sqllist.Clear();

                        string strsql = "update TBPC_MARSTOUSEALL set PUR_ISSTOUSE='2' where PUR_OPERSTATE='" + Code.Trim() + Session["UserID"].ToString() + "'";

                        sqllist.Add(strsql);


                        strsql = "update TBPC_MPTEMPCHANGE set MP_EXECSTATE='3' WHERE MP_MTO='" + Code.Trim() + "'";
                        sqllist.Add(strsql);

                        DBCallCommon.ExecuteTrans(sqllist);
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
                    {
                        LabelMessage.Text = "无法通过审核：部分物料不存在！";
                    }
                    if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
                    {
                        LabelMessage.Text = "无法通过审核：部分物料数量小于调整数量！";
                    }
                }
            }
        }

        //初始化数据
        DataTable mto = new DataTable();
        DataTable dtzy = new DataTable();
        private void Initial(string tsa)
        {
            List<string> list = new List<string>();
            //string sqltext = "SELECT marid FROM View_TBPC_MARSTOUSEALL where planno='" + TextBoxNO.Text.ToString() + "' order by ptcode asc";
            string sqltext = "select SQCODE from View_SM_Storage where SQCODE like '%" + tsa + "%'";
            dtzy = DBCallCommon.GetDTUsingSqlText(sqltext);

            for (int i = 0; i < dtzy.Rows.Count; i++)
            {
                sqltext = "UPDATE TBWS_STORAGE SET SQ_STATE='MTO" + Session["UserID"].ToString() + "' WHERE SQ_CODE='" + dtzy.Rows[i][0].ToString() + "'";
                list.Add(sqltext);
            }

            DBCallCommon.ExecuteTrans(list);

            string LabelCode = GenerateCode();
            TextBoxDate = DateTime.Now.ToString("yyyy-MM-dd");
            string sql = "insert into TBWS_MTOCODE (MTO_CODE) VALUES ('" + LabelCode + "')";

            DBCallCommon.ExeSqlText(sql);
            //LabelState.Text = "0";
            ClosingAccountDate(TextBoxDate.Trim());
            TextBoxComment = "";
            LabelDoc = Session["UserName"].ToString();
            LabelDocCode = Session["UserID"].ToString();
            LabelVerifier = "";
            LabelVerifierCode = "";
            LabelApproveDate = "";

            sqltext = "SELECT UniqueID='',SQCODE AS SQCODE,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                "Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Fixed AS Fixed,Length AS Length," +
                "Width AS Width,LotNumber AS LotNumber,PlanMode AS PlanMode,PTC AS PTCFrom," +
                "WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,LocationCode AS PositionCode," +
                "Location AS Position,Unit AS Unit,cast(Number as float) AS WN,cast(SupportNumber as float) AS WQN,PTCTO='--请选择--',cast(Number as float) AS AdjN," +
                "cast(SupportNumber as float) AS AdjQN,OrderCode AS OrderID,Note AS Note " +
                "FROM View_SM_Storage WHERE State='MTO" + Session["UserID"].ToString() + "'";


            mto = DBCallCommon.GetDTUsingSqlText(sqltext);

            sqltext = "UPDATE TBWS_STORAGE SET SQ_STATE='' WHERE SQ_STATE='MTO" + Session["UserID"].ToString() + "'";
            DBCallCommon.ExeSqlText(sqltext);
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

        //生成调整单编号
        protected string GenerateCode()
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

        //获取系统封账时间
        private void ClosingAccountDate(string ZDDate)
        {
            string NowDate = ZDDate;
            //查找本期系统关账时间
            string sql = "select HS_TIME from TBFM_HSTOTAL where  HS_YEAR='" + NowDate.Substring(0, 4) + "' and HS_MONTH='" + NowDate.Substring(5, 2) + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                LabelClosingAccount = dt.Rows[0]["HS_TIME"].ToString();
            }
            else
            {
                LabelClosingAccount = "NoTime";
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

        #endregion


    }
}
