using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_MaterialFlow_INDetail : System.Web.UI.Page
    {
        private double tn = 0;//数量
        private double ta = 0;//金额
        private double tz = 0;//合计张支数

        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {

            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);

            if (!IsPostBack)
            {
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
                if (Request.QueryString["ptcode"] != null)
                {
                    TextBoxMPTCWG.Text = Request.QueryString["ptcode"].ToString();
                }
                else
                {
                    TextBoxMPTCWG.Text = "";
                }

                bindGV();//绑定条件框

                //上查下查
                if (Request.QueryString["action"] != null)
                {
                    if ((Request.QueryString["action"] == "SearchUpOrDown"))
                    {
                        string objcode = Request.QueryString["id"];
                        BindUpOrDown(objcode);
                    }

                }

                getWGInfo(true);

               // CheckUser(ControlFinder);


            }

            this.Form.DefaultButton = btnQuery.UniqueID;
        }
        private void BindUpOrDown(string objcode)
        {
            objcode = objcode.Substring(0, objcode.Length - 1);

            for (int i = 0; i < objcode.Split('-').Length; i++)
            {
                GridViewRow gr = GridViewSearch.Rows[i];

                DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);
                ddl.ClearSelection();
                ddl.Items.FindByValue("WG_CODE").Selected = true;

                ddl = (gr.FindControl("DropDownListRelation") as DropDownList);
                ddl.ClearSelection();
                ddl.Items.FindByValue("0").Selected = true;

                (gr.FindControl("TextBoxValue") as TextBox).Text = objcode.Split('-')[i];

            }

            refreshStyle();
        }


        private void InitVar(string tableName, string PrimaryKey, string ShowFields, string OrderField, int OrderType, string StrWhere, int PageSize, bool isFristPage)
        {

            InitPager(tableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);//初始化页面

            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数

            if (isFristPage)
            {
                UCPaging1.CurrentPage = 1;
            }

            //否则即为当前页

            bindData();
        }


        //初始化分页信息
        private void InitPager(string TableName, string PrimaryKey, string ShowFields, string OrderField, int OrderType, string StrWhere, int PageSize)
        {
            pager.TableName = TableName;

            pager.PrimaryKey = PrimaryKey;

            pager.ShowFields = ShowFields;

            pager.OrderField = OrderField;

            pager.OrderType = OrderType;

            pager.StrWhere = StrWhere;

            pager.PageSize = PageSize;
        }

        void Pager_PageChanged(int pageNumber)
        {
            getWGInfo(false);
        }

        protected void bindData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;

            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);

            CommonFun.Paging(dt, RepeaterWG, UCPaging1, NoDataPanel);

            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            if (!CheckBoxShow.Checked)
            {
                BindItem();
            }
        }
        private string GetStrCondition()
        {
            string state = DropDownListState.SelectedValue;//审核状态

            string colour = DropDownListColour.SelectedValue;//颜色

            string gj = DropDownListGJ.SelectedValue;//勾稽

            string hx = DropDownListHX.SelectedValue;//核销

            string condition = " WG_BILLTYPE>='0' ";

            //单号条件
            if (TextBoxCodeWG.Text != "")
            {
                condition += " AND WG_CODE LIKE '%" + TextBoxCodeWG.Text.Trim().PadLeft(9, '0') + "%'";
            }

            //时间条件

            if ((TextBoxStartDate.Text.Trim() == "") && (TextBoxDateWG.Text.Trim() == "") && (condition != ""))
            {
                condition += " AND " + " WG_VERIFYDATE >= '" + TextBoxStartDate.Text.Trim() + "'";//全部
            }
            if ((TextBoxStartDate.Text.Trim() == "") && (TextBoxDateWG.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " WG_VERIFYDATE <= '" + TextBoxDateWG.Text.Trim() + " 24:00:00'";//到24点结束
            }
            if ((TextBoxStartDate.Text.Trim() != "") && (TextBoxDateWG.Text.Trim() == "") && (condition != ""))
            {
                condition += " AND " + " WG_VERIFYDATE >= '" + TextBoxStartDate.Text.Trim() + "'";//从零时开始
            }
            if ((TextBoxStartDate.Text.Trim() != "") && (TextBoxDateWG.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " WG_VERIFYDATE between '" + TextBoxStartDate.Text.Trim() + "' and '" + TextBoxDateWG.Text.Trim() + " 24:00:00'";
            }

            //供应商条件
            if ((TextBoxSupplierWG.Text != "") && (condition != ""))
            {
                condition += " AND " + " SupplierName LIKE '%" + TextBoxSupplierWG.Text.Trim() + "%'";
            }
            else if ((TextBoxSupplierWG.Text != "") && (condition == ""))
            {
                condition += " SupplierName LIKE '%" + TextBoxSupplierWG.Text.Trim() + "%'";
            }

            //业务员条件
            if ((TextBoxClerkWG.Text != "") && (condition != ""))
            {
                condition += " AND " + " ClerkName LIKE'%" + TextBoxClerkWG.Text.Trim() + "%'";
            }
            else if ((TextBoxClerkWG.Text != "") && (condition == ""))
            {
                condition += " ClerkName LIKE '%" + TextBoxClerkWG.Text.Trim() + "%'";
            }

            //物料代码条件
            if ((TextBoxMCodeWG.Text != "") && (condition != ""))
            {
                condition += " AND " + " WG_MARID LIKE '%" + TextBoxMCodeWG.Text.Trim() + "%'";
            }
            else if ((TextBoxMCodeWG.Text != "") && (condition == ""))
            {
                condition += " WG_MARID LIKE '%" + TextBoxMCodeWG.Text.Trim() + "%'";
            }
            //物料名称条件
            if ((TextBoxMNameWG.Text != "") && (condition != ""))
            {
                condition += " AND " + "MNAME LIKE '%" + TextBoxMNameWG.Text.Trim() + "%'";
            }
            else if ((TextBoxMNameWG.Text != "") && (condition == ""))
            {
                condition += " MNAME LIKE '%" + TextBoxMNameWG.Text.Trim() + "%'";
            }
            //规格型号条件
            if ((TextBoxMStandardWG.Text != "") && (condition != ""))
            {
                condition += " AND " + " GUIGE LIKE '%" + TextBoxMStandardWG.Text.Trim() + "%'";
            }
            else if ((TextBoxMStandardWG.Text != "") && (condition == ""))
            {
                condition += " GUIGE LIKE '%" + TextBoxMStandardWG.Text.Trim() + "%'";
            }
            //材质
            if ((TextBoxCZ.Text != "") && (condition != ""))
            {
                condition += " AND " + " CAIZHI LIKE '%" + TextBoxCZ.Text.Trim().ToUpper() + "%'";
            }
            else if ((TextBoxCZ.Text != "") && (condition == ""))
            {
                condition += " CAIZHI LIKE '%" + TextBoxCZ.Text.Trim().ToUpper() + "%'";
            }

            //标识号
            if ((TextBoxTH.Text != "") && (condition != ""))
            {
                condition += " AND " + " WG_CGMODE LIKE '%" + TextBoxTH.Text.Trim() + "%'";
            }
            else if ((TextBoxTH.Text != "") && (condition == ""))
            {
                condition += " WG_CGMODE LIKE '%" + TextBoxTH.Text.Trim() + "%'";
            }

            //计划跟踪号条件
            if ((TextBoxMPTCWG.Text != "") && (condition != ""))
            {
                condition += " AND " + " WG_PTCODE LIKE '%" + TextBoxMPTCWG.Text.Trim() + "%'";
            }
            else if ((TextBoxMPTCWG.Text != "") && (condition == ""))
            {
                condition += " WG_PTCODE LIKE '%" + TextBoxMPTCWG.Text.Trim() + "%'";
            }

            //批号
            if ((TextBoxPH.Text != "") && (condition != ""))
            {
                condition += " AND " + " WG_LOTNUM LIKE '%" + TextBoxPH.Text.Trim() + "%'";
            }
            else if ((TextBoxPH.Text != "") && (condition == ""))
            {
                condition += " WG_LOTNUM LIKE '%" + TextBoxPH.Text.Trim() + "%'";
            }


            //物料类型
            if ((TextBoxMType.Text != "") && (condition != ""))
            {
                condition += " AND " + " WG_PMODE LIKE '%" + TextBoxMType.Text.Trim() + "%'";
            }
            else if ((TextBoxMType.Text != "") && (condition == ""))
            {
                condition += " WG_PMODE LIKE '%" + TextBoxMType.Text.Trim() + "%'";
            }


            //制单人条件
            if ((TextBoxZDR.Text != "") && (condition != ""))
            {
                condition += " AND " + " DocName LIKE '%" + TextBoxZDR.Text.Trim() + "%'";
            }
            else if ((TextBoxZDR.Text != "") && (condition == ""))
            {
                condition += " DocName LIKE '%" + TextBoxZDR.Text.Trim() + "%'";
            }

            //货单编号条件
            if ((TextBoxHDBH.Text != "") && (condition != ""))
            {
                condition += " AND " + " WG_HDBH LIKE '%" + TextBoxHDBH.Text.Trim() + "%'";
            }
            else if ((TextBoxHDBH.Text != "") && (condition == ""))
            {
                condition += " WG_HDBH LIKE '%" + TextBoxHDBH.Text.Trim() + "%'";
            }

            //打印标识条件
            if ((CheckBoxPrint.Checked) && (condition != ""))
            {
                condition += " AND " + " WG_PRINTTIME<>0 ";
            }
            else if ((CheckBoxPrint.Checked == false) && (condition == ""))
            {
                condition += " WG_PRINTTIME=0 ";
            }

            //红蓝字条件
            switch (colour)
            {
                case "": break;
                case "0":
                    //蓝
                    if (condition != "") { condition += " AND WG_ROB='0'"; }
                    else { condition += " WG_ROB='0'"; }
                    break;
                case "1":
                    //红
                    if (condition != "") { condition += " AND WG_ROB='1'"; }
                    else { condition += " WG_ROB='1'"; }
                    break;
                default: break;
            }
            //勾稽条件
            switch (gj)
            {
                case "": break;
                case "0":
                    if (condition != "") { condition += " AND WG_GJSTATE!='2'"; }
                    else { condition += " WG_GJSTATE!='2'"; }
                    break;
                //勾稽条件的问题
                /*
                 * 未勾稽的事0,1
                 * 勾稽的状态为2
                 */
                case "1":
                    if (condition != "") { condition += " AND WG_GJSTATE='2'"; }
                    else { condition += " WG_GJSTATE='2'"; }
                    break;
                default: break;
            }
            //核销条件
            switch (hx)
            {
                case "": break;
                case "0":
                    if (condition != "") { condition += " AND WG_CAVFLAG='0'"; }
                    else { condition += " WG_CAVFLAG='0'"; }
                    break;
                case "1":
                    if (condition != "") { condition += " AND WG_CAVFLAG='1'"; }
                    else { condition += " WG_CAVFLAG='1'"; }
                    break;
                default: break;
            }
            //审核状态条件
            /*
             * 有session的概念
             */
            switch (state)
            {
                case "": break;
                case "1":
                    if (condition != "") { condition += " AND WG_STATE='1' "; }
                    else { condition += " WG_STATE='1' "; }
                    break;
                case "2":
                    if (condition != "") { condition += " AND WG_STATE='2' "; }
                    else { condition += " WG_STATE='2' "; }
                    break;
                default: break;
            }

            string SubCondtion = GetSubCondtion();

            if (condition != "")
            {

                //AND可以变化


                if (SubCondtion != "")

                    condition += DropDownListFatherLogic.SelectedValue + " (" + SubCondtion + ")";

            }
            else
            {
                if (SubCondtion != "")
                    condition += SubCondtion;
            }
            return condition;

        }

        //查询入库单记录
        protected void getWGInfo(bool isFristPage)
        {
            if (Request.QueryString["FLAG"] == "ZX") //需用执行
            {
                TextBoxMPTCWG.Text = Request.QueryString["eng"]; //需用执行 工程
                TextBoxMCodeWG.Text = Request.QueryString["mar"];  //需用执行 物料

            }
            if (Request.QueryString["FLAG"] == "XRIN") //需用计划查询
            {
                TextBoxMPTCWG.Text = Request.QueryString["PTC"];


            }


            string condition = GetStrCondition();

            GetTotalAmount(condition);

            if (condition == "")
            {
                /*
                 * 这里的时间为“收料日期”
                 * 
                 * 在这里涉及到的单位为采购单位，采购数量
                 * 
                 * 辅助单位，辅助数量是没有的
                 */
                string TableName = "View_SM_IN";
                string PrimaryKey = "WG_ID";
                string ShowFields = "id as NewCode,WG_CODE AS Code,SupplierName AS Supplier,WG_WAREHOUSE AS WarehouseCode,WS_NAME AS Warehouse,WL_NAME,WG_LOTNUM,WG_DATE AS Date,left(WG_VERIFYDATE,10) AS VerifyDate,ReveicerName,DocName,VerfierName,DepName AS Dep,ClerkName AS Clerk,WG_STATE AS State,WG_CAVFLAG AS HXState,WG_GJSTATE AS GJState,WG_UNIQUEID AS UniqueID,WG_MARID AS MaterialCode,MNAME AS MaterialName,GUIGE AS MaterialStandard,CAIZHI AS Attribute,WG_LENGTH,WG_WIDTH,CGDW AS Unit,cast(WG_RSNUM as float) AS RN,WG_RSFZNUM,cast(round(WG_UPRICE,4) as float) AS UnitPrice,cast(round(WG_AMOUNT,2) as float) AS Amount,WG_PTCODE AS PTC,WG_ORDERID,WG_NOTE AS Comment,WG_HDBH,WG_PMODE,WG_CGMODE ";
                string OrderField = "id DESC,WG_UNIQUEID";
                int OrderType = 0;
                string StrWhere = "";
                int PageSize = 50;

                InitVar(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize, isFristPage);
            }
            else
            {

                string TableName = "View_SM_IN";
                string PrimaryKey = "WG_ID";
                string ShowFields = "id as NewCode,WG_CODE AS Code,SupplierName AS Supplier,WG_WAREHOUSE AS WarehouseCode,WS_NAME AS Warehouse,WL_NAME,WG_LOTNUM,WG_DATE AS Date,left(WG_VERIFYDATE,10) AS VerifyDate,ReveicerName,DocName,VerfierName,DepName AS Dep,ClerkName AS Clerk,WG_STATE AS State,WG_CAVFLAG AS HXState,WG_GJSTATE AS GJState,WG_UNIQUEID AS UniqueID,WG_MARID AS MaterialCode,MNAME AS MaterialName,GUIGE AS MaterialStandard,CAIZHI AS Attribute,WG_LENGTH,WG_WIDTH,CGDW AS Unit,cast(WG_RSNUM as float) AS RN,WG_RSFZNUM,cast(round(WG_UPRICE,4) as float) AS UnitPrice,cast(round(WG_AMOUNT,2) as float) AS Amount,WG_PTCODE AS PTC,WG_ORDERID,WG_NOTE AS Comment,WG_HDBH,WG_PMODE,WG_CGMODE ";
                string OrderField = "id DESC,WG_UNIQUEID";
                int OrderType = 0;
                string StrWhere = condition;
                int PageSize = 50;


                InitVar(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize, isFristPage);
            }


        }


        private void GetTotalAmount(string strWhere)
        {
            string sql = "select isnull(CAST(sum(WG_RSNUM) AS FLOAT),0) as TotalRN,isnull(CAST(round(sum(WG_AMOUNT),2) AS FLOAT),0) as TotalAmount,isnull(cast(round(sum(WG_RSFZNUM),2) as float),0) as Totalzhang from View_SM_IN where " + strWhere;

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.Read())
            {
                hfdTotalNum.Value = sdr["TotalRN"].ToString();
                hfdTotalAmount.Value = sdr["TotalAmount"].ToString();
                hfTotalzhang.Value = sdr["Totalzhang"].ToString();
            }
            sdr.Close();
        }




        protected void RepeaterWG_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                if (((Label)e.Item.FindControl("LabelRN")).Text.ToString() != "")
                {
                    //数量---如果单位不统一，这数量是没意义的
                    tn += Convert.ToDouble(((Label)e.Item.FindControl("LabelRN")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("LabelAmount")).Text.ToString() != "")
                {
                    //金额---金额必须准
                    ta += Convert.ToDouble(((Label)e.Item.FindControl("LabelAmount")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("LabelRFN")).Text.ToString() != "")
                {

                    tz += Convert.ToDouble(((Label)e.Item.FindControl("LabelRFN")).Text.ToString());
                }

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {

                ((Label)e.Item.FindControl("LabelTotalNum")).Text = Math.Round(tn, 4).ToString();
                ((Label)e.Item.FindControl("LabelTotalAmount")).Text = Math.Round(ta, 2).ToString();
                ((Label)e.Item.FindControl("LabelSumzhang")).Text = Math.Round(tz, 2).ToString();

                ((Label)e.Item.FindControl("TotalNum")).Text = hfdTotalNum.Value;
                ((Label)e.Item.FindControl("TotalAmount")).Text = hfdTotalAmount.Value;
                ((Label)e.Item.FindControl("Labelsumzjzhang")).Text = hfTotalzhang.Value;
            }
        }

        //查询
        protected void Query_Click(object sender, EventArgs e)
        {

            getWGInfo(true);//表示当前页为第一页

            refreshStyle();

            ModalPopupExtenderSearch.Hide();

            UpdatePanelBody.Update();
        }

        //关闭
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();

        }
        //重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearCondition();
            resetSubcondition();
        }
        //导出
        //库存=0；入库单=1；结转备库=2；领料单=3；项目完工=8(暂时未用)，项目结转=9

        protected void BtnShowExport_Click(object sender, EventArgs e)
        {
            string condition = GetStrCondition().Replace("'", "^");
            List<string> sqllist = new List<string>();
            string sql = "delete from TBWS_EXPORTCONDITION where SessionID='" + Session["UserID"].ToString() + "' AND Type='1'";
            sqllist.Add(sql);
            sql = "insert into TBWS_EXPORTCONDITION (SessionID,Type,StrCondition) VALUES ('" + Session["UserID"].ToString() + "','1','" + condition + "')";
            sqllist.Add(sql);
            DBCallCommon.ExecuteTrans(sqllist);

            ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>inexport();</script>");
        }



        //单据头显示
        protected void CheckBoxShow_CheckedChanged(object sender, EventArgs e)
        {
            getWGInfo(false);

            UpdatePanelBody.Update();
        }



        //清除条件
        private void clearCondition()
        {

            //审核状态
            DropDownListState.ClearSelection();
            DropDownListState.Items[0].Selected = true;

            //红蓝字
            DropDownListColour.ClearSelection();
            DropDownListColour.Items[0].Selected = true;

            DropDownListGJ.ClearSelection();
            DropDownListGJ.Items[0].Selected = true;

            DropDownListHX.ClearSelection();
            DropDownListHX.Items[0].Selected = true;

            DropDownListFatherLogic.ClearSelection();
            DropDownListFatherLogic.Items[0].Selected = true;

            //入库单编号
            TextBoxCodeWG.Text = string.Empty;
            //供应商
            TextBoxSupplierWG.Text = string.Empty;

            TextBoxDateWG.Text = string.Empty;
            TextBoxStartDate.Text = string.Empty;
            //

            TextBoxMCodeWG.Text = string.Empty;
            TextBoxMNameWG.Text = string.Empty;
            TextBoxMStandardWG.Text = string.Empty;
            TextBoxCZ.Text = string.Empty;
            TextBoxTH.Text = string.Empty;


            TextBoxMPTCWG.Text = string.Empty;
            TextBoxPH.Text = string.Empty;
            TextBoxMType.Text = string.Empty;

            TextBoxZDR.Text = string.Empty;
            TextBoxClerkWG.Text = string.Empty;
            TextBoxHDBH.Text = string.Empty;
            CheckBoxPrint.Checked = false;
        }



        protected string convertSH(string state)
        {
            switch (state)
            {
                case "1": return "<font color='#FF0000'>未审核</font>";
                case "2": return "已审核";
                default: return state;
            }
        }

        protected string convertGJ(string state)
        {
            switch (state)
            {
                case "0": return "未勾稽";
                case "1": return "待勾稽";
                case "2": return "已勾稽";
                default: return state;
            }
        }

        protected string convertHX(string state)
        {
            switch (state)
            {
                case "0": return "未核销";
                case "1": return "已核销";
                default: return state;
            }
        }

        #region  不用


        /*
         * 反审
         */
        protected void AntiVerify_Click(object sender, EventArgs e)
        {
            string Code = "";

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

            for (int i = 0; i < RepeaterWG.Items.Count; i++)
            {
                if (((CheckBox)RepeaterWG.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    string gjstate = ((Label)RepeaterWG.Items[i].FindControl("LabelGJState")).Text;
                    string hxstate = ((Label)RepeaterWG.Items[i].FindControl("LabelHXState")).Text;
                    if (gjstate != "未勾稽")
                    {
                        LabelMessage.Text = "正在勾稽的入库单无法反审！";
                        return;
                    }
                    if (hxstate != "未核销")
                    {
                        LabelMessage.Text = "已核销的入库单无法反审！";
                        return;
                    }
                    Code = ((Label)RepeaterWG.Items[i].FindControl("LabelCode")).Text;
                }
            }
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

                //string alert = "<script> window.open('SM_WarehouseIN_WGPush.aspx?FLAG=READ&&ID='"+Code+");</script>";

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);

                Response.Redirect("SM_WarehouseIN_WG.aspx?FLAG=READ&&ID=" + Code);
            }
            /*
             * 这地方由于出库单关联不上入库单，所以不知道入库单是否出过物料
             */
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 1)
            {
                //物料不存在，也就是物料出完了

                LabelMessage.Text = "反审核未通过：入库物料发生后续操作！";
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == 2)
            {
                //物料已出了一部分

                LabelMessage.Text = "反审核未通过：部分入库物料发生后续操作！";
            }
            /*
             * 这三个可以不用执行存储过程，可以根据页面状态来减少页面回发
             * 
             */
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -1)
            {
                LabelMessage.Text = "反审核未通过：未审核入库单不允许反审！";
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -2)
            {
                LabelMessage.Text = "反审核未通过：正在进行勾稽的入库单不允许反审！";
            }
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -3)
            {
                LabelMessage.Text = "反审核未通过：已核销的入库单不允许反审！";
            }
            /*
             * 跨月事不能反审核的，由于跨越的它已经暂估，核算
             */
            if (Convert.ToInt32(cmd.Parameters["@retVal"].Value) == -4)
            {
                LabelMessage.Text = "反审核未通过：已核算的入库单不允许反审！";
            }
        }


        /*
         * 拆分
         */
        protected void Split_Click(object sender, EventArgs e)
        {
            List<string> sqllist = new List<string>();
            string sql = "UPDATE TBWS_INDETAIL SET WG_STATE='' WHERE WG_STATE='SPLIT" + Session["UserID"].ToString() + "'";
            sqllist.Add(sql);
            string incode = "";
            string uniqueid = "";
            for (int i = 0; i < RepeaterWG.Items.Count; i++)
            {
                if (((CheckBox)RepeaterWG.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    if ((incode == "") || (((Label)RepeaterWG.Items[i].FindControl("LabelCode")).Text == incode))
                    {
                        incode = ((Label)RepeaterWG.Items[i].FindControl("LabelCode")).Text;
                        string gjstate = ((Label)RepeaterWG.Items[i].FindControl("LabelGJState")).Text;
                        if (gjstate != "未勾稽")
                        {
                            LabelMessage.Text = "正在勾稽的入库单无法拆分！";
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
                            uniqueid = ((Label)RepeaterWG.Items[i].FindControl("LabelUniqueID")).Text;
                            //更新明细表中的状态
                            sql = "UPDATE TBWS_INDETAIL SET WG_STATE='SPLIT" + Session["UserID"].ToString() + "' WHERE WG_CODE='" + incode + "' AND WG_UNIQUEID='" + uniqueid + "'";
                            sqllist.Add(sql);
                        }
                    }
                    else
                    {
                        LabelMessage.Text = "拆分条目必须在同一张入库单上！";
                        return;
                    }
                }
            }
            if (sqllist.Count == 0)
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
        protected void Merge_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < RepeaterWG.Items.Count; i++)
            {
                if (((CheckBox)RepeaterWG.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    string code = ((Label)RepeaterWG.Items[i].FindControl("LabelCode")).Text;
                    string gjstate = ((Label)RepeaterWG.Items[i].FindControl("LabelGJState")).Text;
                    if (gjstate != "未勾稽")
                    {
                        LabelMessage.Text = "正在勾稽的入库单无法合并！";
                        return;
                    }
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
            LabelMessage.Text = "请选择要合并的子单！";
        }

        /*
         * 下推红字
         */
        protected void Red_Click(object sender, EventArgs e)
        {
            //此处为由蓝字入库单下推红字入库单操作
            List<string> sqllist = new List<string>();
            string sql = "UPDATE TBWS_INDETAIL SET WG_STATE='' WHERE WG_STATE='RED" + Session["UserID"].ToString() + "'";
            sqllist.Add(sql);
            string incode = "";
            string uniqueid = "";
            for (int i = 0; i < RepeaterWG.Items.Count; i++)
            {
                if (((CheckBox)RepeaterWG.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    if ((incode == "") || (((Label)RepeaterWG.Items[i].FindControl("LabelCode")).Text == incode))
                    {
                        incode = ((Label)RepeaterWG.Items[i].FindControl("LabelCode")).Text;
                        string gjstate = ((Label)RepeaterWG.Items[i].FindControl("LabelGJState")).Text;
                        if (gjstate != "未勾稽")
                        {
                            LabelMessage.Text = "正在勾稽的入库单无法下推红字入库单！";
                            return;
                        }
                        if (incode.Contains("R") == true)
                        {
                            LabelMessage.Text = "所选条目不能来自红字入库单，请检查！";
                            return;
                        }
                        else
                        {
                            uniqueid = ((Label)RepeaterWG.Items[i].FindControl("LabelUniqueID")).Text;
                            sql = "UPDATE TBWS_INDETAIL SET WG_STATE='RED" + Session["UserID"].ToString() + "' WHERE WG_CODE='" + incode + "' AND WG_UNIQUEID='" + uniqueid + "'";
                            sqllist.Add(sql);
                        }
                    }
                    else
                    {
                        LabelMessage.Text = "下推红字入库单的条目必须在同一张入库单上！";
                        return;
                    }
                }
            }
            if (sqllist.Count == 0)
            {
                LabelMessage.Text = "请选择要下推红字入库单的条目！";
                return;
            }
            DBCallCommon.ExecuteTrans(sqllist);
            Response.Redirect("~/SM_Data/SM_WarehouseIN_WG.aspx?FLAG=PUSHRED&&ID=" + incode);
        }

        /*
         * 核销
         */
        protected void Verification_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < RepeaterWG.Items.Count; i++)
            {
                if (((CheckBox)RepeaterWG.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    string code = ((Label)RepeaterWG.Items[i].FindControl("LabelCode")).Text;
                    string gjstate = ((Label)RepeaterWG.Items[i].FindControl("LabelGJState")).Text;
                    if (gjstate != "未勾稽")
                    {
                        LabelMessage.Text = "正在勾稽的入库单无法核销！";
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
            }
            LabelMessage.Text = "请选择要核销的红字入库单！";
        }

        //关联订单
        protected void Related_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < RepeaterWG.Items.Count; i++)
            {
                if (((CheckBox)RepeaterWG.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    /*
                     * //计划跟踪号
                     */
                    string ptc = ((Label)RepeaterWG.Items[i].FindControl("LabelPTC")).Text;

                    Response.Redirect("SM_Warehouse_RelatedDocument.aspx?PTC=" + ptc);

                    return;
                }
            }
            LabelMessage.Text = "请选择一条要查询的记录！";
        }


        #endregion


        private void BindItem()
        {

            for (int i = 0; i < (RepeaterWG.Items.Count - 1); i++)
            {

                Label lbCode = (RepeaterWG.Items[i].FindControl("LabelCode") as Label);

                string NextCode = lbCode.Text;

                if (lbCode.Visible)
                {
                    for (int j = i + 1; j < RepeaterWG.Items.Count; j++)
                    {
                        string Code = (RepeaterWG.Items[j].FindControl("LabelCode") as Label).Text;

                        if (NextCode == Code)
                        {
                            (RepeaterWG.Items[j].FindControl("LabelCode") as Label).Style.Add("display", "none");
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }





        #region 条件框


        private void bindGV()
        {
            GridViewSearch.DataSource = CreateTable();

            GridViewSearch.DataBind();
        }
        private DataTable CreateTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("index", typeof(int)));


            for (int i = 0; i < 9; i++)
            {
                DataRow row = dt.NewRow();
                row["index"] = i;
                dt.Rows.Add(row);
            }

            return dt;
        }



        private Dictionary<string, string> bindItemList()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();
            ItemList.Add("NO", "");
            ItemList.Add("WG_CODE", "入库单号");
            ItemList.Add("WG_PTCODE", "计划跟踪号");
            ItemList.Add("SupplierName", "供应商");
            ItemList.Add("WG_MARID", "物料编码");
            ItemList.Add("MNAME", "物料名称");
            ItemList.Add("GUIGE", "规格型号");
            ItemList.Add("CAIZHI", "材质");
            ItemList.Add("DocName", "制单人");
            ItemList.Add("WG_DATE", "制单日期");
            ItemList.Add("WG_ORDERID", "订单编号");
            ItemList.Add("ClerkName", "业务员");
            ItemList.Add("WS_NAME", "仓库");
            ItemList.Add("WL_NAME", "仓位");
            ItemList.Add("WG_LENGTH", "长");
            ItemList.Add("WG_WIDTH", "宽");
            ItemList.Add("WG_LOTNUM", "批号");
            ItemList.Add("DepName", "部门");
            ItemList.Add("ReveicerName", "收料人");
            ItemList.Add("VerfierName", "审核人");
            ItemList.Add("WG_PMODE", "计划模式");
            ItemList.Add("WG_CGMODE", "标识号");
            ItemList.Add("WG_RSNUM", "实收数量");
            return ItemList;
        }

        protected void GridViewSearch_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                ddl.DataTextField = "value";
                ddl.DataValueField = "key";
                ddl.DataSource = bindItemList();
                ddl.DataBind();

                if (gr.RowIndex == 0)
                {
                    (gr.FindControl("tb_logic") as TextBox).Visible = false;
                }
            }
        }

        protected string GetSubCondtion()
        {
            string subCondition = "";

            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                if (gr.RowIndex == 0)
                {

                    DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                    if (ddl.SelectedValue != "NO")
                    {
                        TextBox txtValue = (gr.FindControl("TextBoxValue") as TextBox);

                        DropDownList ddlRel = (gr.FindControl("DropDownListRelation") as DropDownList);

                        subCondition = ConvertRelation(ddl.SelectedValue, ddlRel.SelectedValue, txtValue.Text.Trim());
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                    if (ddl.SelectedValue != "NO")
                    {
                        DropDownList ddlLogic = (gr.FindControl("DropDownListLogic") as DropDownList);

                        TextBox txtValue = (gr.FindControl("TextBoxValue") as TextBox);

                        DropDownList ddlRel = (gr.FindControl("DropDownListRelation") as DropDownList);

                        subCondition += " " + ddlLogic.SelectedValue + " " + ConvertRelation(ddl.SelectedValue, ddlRel.SelectedValue, txtValue.Text.Trim());
                    }

                    else
                    {
                        break;
                    }
                }
            }
            return subCondition;
        }

        private string ConvertRelation(string field, string relation, string fieldValue)
        {
            string obj = string.Empty;

            if (field == "WG_CODE")
            {
                fieldValue = fieldValue.PadLeft(9, '0');
            }
            if (field == "WG_ORDERID")
            {
                fieldValue = fieldValue.PadLeft(8, '0');
            }

            switch (relation)
            {
                case "0":
                    {
                        //包含

                        obj = field + "  LIKE  '%" + fieldValue + "%'";
                        break;
                    }
                case "1":
                    {
                        //等于
                        obj = field + "  =  '" + fieldValue + "'";
                        break;
                    }
                case "2":
                    {
                        //不等于
                        obj = field + "  !=  '" + fieldValue + "'";
                        break;
                    }
                case "3":
                    {
                        //大于
                        obj = field + "  >  '" + fieldValue + "'";
                        break;
                    }
                case "4":
                    {
                        //大于或等于
                        obj = field + "  >=  '" + fieldValue + "'";
                        break;
                    }
                case "5":
                    {
                        //小于
                        obj = field + "  <  '" + fieldValue + "'";
                        break;
                    }
                case "6":
                    {
                        //小于或等于
                        obj = field + "  <=  '" + fieldValue + "'";
                        break;
                    }
                case "7":
                    {
                        //不包含
                        obj = field + " NOT LIKE  '%" + fieldValue + "%'";
                        break;
                    }
                case "8":
                    {
                        //左包含
                        obj = field + "  LIKE  '" + fieldValue + "%'";
                        break;
                    }
                case "9":
                    {
                        //右包含
                        obj = field + "  LIKE  '%" + fieldValue + "'";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return obj;
        }

        private void resetSubcondition()
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                DropDownList ddl = gr.FindControl("DropDownListName") as DropDownList;
                foreach (ListItem lt in ddl.Items)
                {
                    if (lt.Selected)
                        lt.Selected = false;
                }
                ddl.Items[0].Selected = true;
                (gr.FindControl("TextBoxValue") as TextBox).Text = string.Empty; ;
            }

            refreshStyle();
        }
        private void refreshStyle()
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                if ((gr.FindControl("DropDownListName") as DropDownList).SelectedValue != "NO")
                {
                    if (gr.RowIndex != 0)
                    {
                        (gr.FindControl("DropDownListLogic") as DropDownList).Style.Add("display", "block");
                        (gr.FindControl("tb_logic") as TextBox).Style.Add("display", "none");
                    }

                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "block");
                    (gr.FindControl("tb_name") as TextBox).Style.Add("display", "none");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "block");
                    (gr.FindControl("tb_relation") as TextBox).Style.Add("display", "none");
                }
                else
                {
                    (gr.FindControl("DropDownListLogic") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_logic") as TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_name") as TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_relation") as TextBox).Style.Add("display", "block");
                }
            }
        }

        #endregion


        //上查
        protected void ButtonSearchUp_Click(object sender, EventArgs e)
        {
            string ptc = string.Empty;

            for (int i = 0; i < RepeaterWG.Items.Count; i++)
            {
                if ((RepeaterWG.Items[i].FindControl("CheckBox1") as CheckBox).Checked)
                {

                    ptc = (RepeaterWG.Items[i].FindControl("LabelPTC") as Label).Text;

                    break;

                }
            }
            if (ptc == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('未选中相应单据!')</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>SearchUp('" + ptc + "');</script>");
            }
        }

        //下查
        protected void ButtonSearchDown_Click(object sender, EventArgs e)
        {
            string Code = string.Empty;

            string ObjCode = string.Empty;

            for (int i = 0; i < RepeaterWG.Items.Count; i++)
            {
                if ((RepeaterWG.Items[i].FindControl("CheckBox1") as CheckBox).Checked)
                {

                    Code = (RepeaterWG.Items[i].FindControl("LabelCode") as Label).Text;
                    break;

                }
            }

            if (Code == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('未选中相应单据!')</script>");
                return;
            }

            string sql = "select GJ_INVOICEID from TBFM_GJRELATION where GI_INSTOREID='" + Code + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            if (dt.Rows.Count > 0)
            {
                ObjCode = dt.Rows[0]["GJ_INVOICEID"].ToString();
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>SearchDown('" + ObjCode + "');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('无关联单据信息!')</script>");
            }

        }
    }
}
