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
    public partial class SM_Warehouse_MTOAdjustManage : BasicPage
    {

        double twn = 0;
        Int32 twqn = 0;
        double tan = 0;
        Int32 taqn = 0;

        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {

            InitVar();

            if (!IsPostBack)
            {
                bindWarhouse();
                bindXposition();
                bindData();

                this.Form.DefaultButton = btnQuery.UniqueID;

              

                bindGV();

                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;

            }
            CheckUser(ControlFinder);
        }

        private void InitVar()
        {
            getMTOInfo(); 
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
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
            bindData();
        }

        protected void bindData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;

            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);

            CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);

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
        //仓库绑定
        protected void bindWarhouse()
        {
            string sqltext = "select distinct WS_ID,WS_NAME from TBWS_WAREHOUSE where WS_FATHERID!='ROOT'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            drp_Warhouse.DataSource = dt;
            drp_Warhouse.DataTextField = "WS_NAME";
            drp_Warhouse.DataValueField = "WS_ID";
            drp_Warhouse.DataBind();
            drp_Warhouse.Items.Insert(0, new ListItem("请选择", "请选择"));
            drp_Warhouse.SelectedIndex = 0;
        }
        protected void bindXposition()
        {
            if (drp_Warhouse.SelectedItem.Text != "请选择")
            {
                string sql = "SELECT DISTINCT WL_ID,WL_NAME FROM TBWS_LOCATION WHERE WL_WSID='" + drp_Warhouse.SelectedValue + "' ORDER BY WL_NAME";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                drp_xposition.DataSource = dt;
                drp_xposition.DataTextField = "WL_NAME";
                drp_xposition.DataValueField = "WL_ID";
                drp_xposition.DataBind();
            }
            else
            {
                drp_xposition.Items.Clear();
            }
            ListItem item = new ListItem("请选择", "0");
            drp_xposition.Items.Insert(0, item);
        }
        protected void drp_Warhouse_Changed(object sender, EventArgs e)
        {
            bindXposition();
        }
        protected string GetCondition() //
        {
            string state = DropDownListState.SelectedValue.ToString();
            string condition = "";
            //单号条件
            if (TextBoxMTOCode.Text != "")
            {
                condition = " MTOCode LIKE '%" + TextBoxMTOCode.Text.Trim() + "%'";
            }
            //日期条件
            if ((TextBoxDate.Text != "") && (condition != ""))
            {
                condition += " AND " + " Date LIKE '%" + TextBoxDate.Text.Trim() + "%'";
            }
            else if ((TextBoxDate.Text != "") && (condition == ""))
            {
                condition += " Date LIKE '%" + TextBoxDate.Text.Trim() + "%'";
            }
            //部门条件
            if ((TextBoxDep.Text != "") && (condition != ""))
            {
                condition += " AND " + " Dep LIKE '%" + TextBoxDep.Text.Trim() + "%'";
            }
            else if ((TextBoxDep.Text != "") && (condition == ""))
            {
                condition += " Dep LIKE '%" + TextBoxDep.Text.Trim() + "%'";
            }
            //计划员条件
            if ((TextBoxPlaner.Text != "") && (condition != ""))
            {
                condition += " AND " + " Planer LIKE '%" + TextBoxPlaner.Text.Trim() + "%'";
            }
            else if ((TextBoxPlaner.Text != "") && (condition == ""))
            {
                condition += " Planer LIKE '%" + TextBoxPlaner.Text.Trim() + "%'";
            }
            //物料代码条件
            if ((TextBoxCode.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialCode LIKE '%" + TextBoxCode.Text.Trim() + "%'";
            }
            else if ((TextBoxCode.Text != "") && (condition == ""))
            {
                condition += " MaterialCode LIKE '%" + TextBoxCode.Text.Trim() + "%'";
            }
            //物料名称条件
            if ((TextBoxName.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialName LIKE '%" + TextBoxName.Text.Trim() + "%'";
            }
            else if ((TextBoxName.Text != "") && (condition == ""))
            {
                condition += " MaterialName LIKE '%" + TextBoxName.Text.Trim() + "%'";
            }
            //从计划跟踪号条件
            if ((TextBoxPTCFrom.Text != "") && (condition != ""))
            {
                condition += " AND " + " PTCFrom LIKE '%" + TextBoxPTCFrom.Text.Trim() + "%'";
            }
            else if ((TextBoxPTCFrom.Text != "") && (condition == ""))
            {
                condition += " PTCFrom LIKE '%" + TextBoxPTCFrom.Text.Trim() + "%'";
            }
            //到计划跟踪号条件
            if ((TextBoxPTCTo.Text != "") && (condition != ""))
            {
                condition += " AND " + " PTCTo LIKE '%" + TextBoxPTCTo.Text.Trim() + "%'";
            }
            else if ((TextBoxPTCTo.Text != "") && (condition == ""))
            {
                condition += " PTCTo LIKE '%" + TextBoxPTCTo.Text.Trim() + "%'";
            }
            //规格型号
            if ((TextBoxGuiGe.Text != "") && (condition != ""))
            {
                condition += " AND " + " Standard LIKE '%" + TextBoxGuiGe.Text.Trim() + "%'";
            }
            else if ((TextBoxGuiGe.Text != "") && (condition == ""))
            {
                condition += " Standard LIKE '%" + TextBoxGuiGe.Text.Trim() + "%'";
            }

            //材质
            if ((TextBoxCaiZhi.Text != "") && (condition != ""))
            {
                condition += " AND " + " Attribute LIKE '%" + TextBoxCaiZhi.Text.Trim() + "%'";
            }
            else if ((TextBoxCaiZhi.Text != "") && (condition == ""))
            {
                condition += " Attribute LIKE '%" + TextBoxCaiZhi.Text.Trim() + "%'";
            }
            //仓库
            if (!(drp_Warhouse.SelectedIndex == 0 | drp_Warhouse.SelectedIndex == -1) && (condition != ""))
            {
                condition += " AND  Warehouse= '" + drp_Warhouse.SelectedItem.ToString() + "'";
            }
            else if (!(drp_Warhouse.SelectedIndex == 0 | drp_Warhouse.SelectedIndex == -1) && (condition == ""))
            {
                condition += " Warehouse = '" + drp_Warhouse.SelectedItem.ToString() + "'";
            }
            //仓位
            if (!(drp_xposition.SelectedIndex == 0 | drp_xposition.SelectedIndex == -1) && (condition != ""))
            {
                condition += " AND  Location= '" + drp_xposition.SelectedItem.ToString() + "'";
            }
            else if (!(drp_xposition.SelectedIndex == 0 | drp_xposition.SelectedIndex == -1) && (condition == ""))
            {
                condition += " Location = '" + drp_xposition.SelectedItem.ToString() + "'";
            }
          
            //调整数量
            if ((TextBoxTiaozh.Text != "") && (condition != ""))
            {
                condition += " AND " + " TZNUM LIKE '%" + TextBoxTiaozh.Text.Trim() + "%'";
            }
            else if ((TextBoxTiaozh.Text != "") && (condition == ""))
            {
                condition += "  TZNUM LIKE '%" + TextBoxTiaozh.Text.Trim() + "%'";
            }
            //可调数量
            if ((TextBoxKetiao.Text != "") && (condition != ""))
            {
                condition += " AND " + " KTNUM LIKE '%" + TextBoxKetiao.Text.Trim() + "%'";
            }
            else if ((TextBoxKetiao.Text != "") && (condition == ""))
            {
                condition += " KTNUM LIKE '%" + TextBoxKetiao.Text.Trim() + "%'";
            }
            //批号
            if ((TextBoxPhao.Text != "") && (condition != ""))
            {
                condition += " AND " + " LotNumber LIKE '%" + TextBoxPhao.Text.Trim() + "%'";
            }
            else if ((TextBoxPhao.Text != "") && (condition == ""))
            {
                condition += " LotNumber LIKE '%" + TextBoxPhao.Text.Trim() + "%'";
            }
            //制单人
            if ((TextBoxZHDan.Text != "") && (condition != ""))
            {
                condition += " AND " + " Doc LIKE '%" + TextBoxZHDan.Text.Trim() + "%'";
            }
            else if ((TextBoxZHDan.Text != "") && (condition == ""))
            {
                condition += " Doc LIKE '%" + TextBoxZHDan.Text.Trim() + "%'";
            }
            //审核人
            if ((TextBoxshher.Text != "") && (condition != ""))
            {
                condition += " AND " + " Verifier LIKE '%" + TextBoxshher.Text.Trim() + "%'";
            }
            else if ((TextBoxshher.Text != "") && (condition == ""))
            {
                condition += " Verifier LIKE '%" + TextBoxshher.Text.Trim() + "%'";
            }
            //备注
            if ((TextBoxBz.Text != "") && (condition != ""))
            {
                condition += " AND " + " DetailNote LIKE '%" + TextBoxBz.Text.Trim() + "%'";
            }
            else if ((TextBoxBz.Text != "") && (condition == ""))
            {
                condition += " DetailNote LIKE '%" + TextBoxBz.Text.Trim() + "%'";
            }
            switch (state)
            {
                case "0": break;
                case "1":
                    if (condition != "") { condition += " AND TotalState='1' "; } //AND DocCode='" + Session["UserID"].ToString() + "'
                    else { condition += " TotalState='1' "; } //AND DocCode='" + Session["UserID"].ToString() + "'
                    break;
                case "2":
                    if (condition != "") { condition += " AND TotalState='2' "; } //AND DocCode='" + Session["UserID"].ToString() + "'
                    else { condition += " TotalState='2' "; } //AND DocCode='" + Session["UserID"].ToString() + "'
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


        protected void getMTOInfo()
        {
            //string state = DropDownListState.SelectedValue.ToString();
           
            string condition = GetCondition();  
            if (condition == "")
            {
                string TableName = "View_SM_MTO";
                string PrimaryKey = "MTO_ID";
                string ShowFields = "MTOCode AS Code,Date AS Date,Dep AS Dep,Planer AS Planer,Doc AS Document,Verifier AS Verifier,left(VerifyDate,10) AS ApproveDate,TotalState AS State,TotalNote AS Comment,UniqueCode AS UniqueID,MaterialCode AS MaterialCode,MaterialName AS MaterialName,Standard AS MaterialStandard,Attribute AS Attribute,LotNumber AS LotNumber,Length,Width,PTCFrom AS PTCFrom,Warehouse AS Warehouse,Location AS Position,Unit AS Unit,KTNUM AS WN,TZNUM AS AdjN,KTFZNUM AS WQN,TZFZNUM AS AdjQN,PTCTo AS PTCTo,DetailState AS StateD,DetailNote AS Note ";
                string OrderField = "MTOCode DESC,SQCODE";
                int OrderType = 0;
                string StrWhere = "";
                int PageSize = 50;
                InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);

                //总计数量
                string sqtext = "select isnull(cast(round(sum(TZNUM),4) as float),0) as TotalTZNUM,isnull(cast(round(sum(KTNUM),4) as float),0) as TotalKTNUM,isnull(cast(sum(TZFZNUM) as float),0) as TotalTZFZNUM,isnull(cast(sum(KTFZNUM) as float),0) as TotalKTFZNUM from View_SM_MTO ";
                SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sqtext);
                if (sdr.Read())
                {
                    hfdTotalAdjN.Value = sdr["TotalTZNUM"].ToString(); //调整数量
                    hfdTotalWN.Value = sdr["TotalKTNUM"].ToString(); //可调整数量
                    hfdTotalAdjQN.Value = sdr["TotalTZFZNUM"].ToString(); //调整辅助数量
                    hfdTotalWQN.Value = sdr["TotalKTFZNUM"].ToString();// 可调整辅助数量
                }
                sdr.Close();

                //sql = "SELECT MTOCode AS Code,Date AS Date,Dep AS Dep,Planer AS Planer,Doc AS Document," +
                //    "Verifier AS Verifier,VerifyDate AS ApproveDate,TotalState AS State,TotalNote AS Comment,UniqueCode AS UniqueID," +
                //    "MaterialCode AS MaterialCode,MaterialName AS MaterialName,Standard AS MaterialStandard," +
                //    "Attribute AS Attribute,LotNumber AS LotNumber,PTCFrom AS PTCFrom,Warehouse AS Warehouse,Location AS Position,Unit AS Unit," +
                //    "KTNUM AS WN,TZNUM AS AdjN,KTFZNUM AS WQN,TZFZNUM AS AdjQN,PTCTo AS PTCTo,DetailState AS StateD,DetailNote AS Note " +
                //    "FROM View_SM_MTO";
            }
            else
            {
                string TableName = "View_SM_MTO";
                string PrimaryKey = "MTO_ID";
                string ShowFields = "MTOCode AS Code,Date AS Date,Dep AS Dep,Planer AS Planer,Doc AS Document,Verifier AS Verifier,left(VerifyDate,10) AS ApproveDate,TotalState AS State,TotalNote AS Comment,UniqueCode AS UniqueID,MaterialCode AS MaterialCode,MaterialName AS MaterialName,Standard AS MaterialStandard,Attribute AS Attribute,LotNumber AS LotNumber,Length,Width,PTCFrom AS PTCFrom,Warehouse AS Warehouse,Location AS Position,Unit AS Unit,KTNUM AS WN,TZNUM AS AdjN,KTFZNUM AS WQN,TZFZNUM AS AdjQN,PTCTo AS PTCTo,DetailState AS StateD,DetailNote AS Note ";
                string OrderField = "MTOCode DESC,SQCODE";
                int OrderType = 0;
                string StrWhere = condition;
                int PageSize = 50;
                InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);
                //总计数量
                string sqltext = "select isnull(cast(round(sum(TZNUM),4)as float),0) as TotalTZNUM,isnull(cast(round(sum(KTNUM),4) as float),0) as TotalKTNUM,isnull(cast(sum(TZFZNUM) as float),0) as TotalTZFZNUM,isnull(cast(sum(KTFZNUM) as float),0) as TotalKTFZNUM from View_SM_MTO  where " + condition;
                SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sqltext);
                if (sdr.Read())
                {
                    hfdTotalAdjN.Value = sdr["TotalTZNUM"].ToString(); //调整数量
                    hfdTotalWN.Value = sdr["TotalKTNUM"].ToString(); //可调整数量
                    hfdTotalAdjQN.Value = sdr["TotalTZFZNUM"].ToString(); //调整辅助数量
                    hfdTotalWQN.Value = sdr["TotalKTFZNUM"].ToString();// 可调整辅助数量
                }
                sdr.Close();
                //sql = "SELECT MTOCode AS Code,Date AS Date,Dep AS Dep,Planer AS Planer,Doc AS Document," +
                //    "Verifier AS Verifier,VerifyDate AS ApproveDate,TotalState AS State,TotalNote AS Comment,UniqueCode AS UniqueID," +
                //    "MaterialCode AS MaterialCode,MaterialName AS MaterialName,Standard AS MaterialStandard," +
                //    "Attribute AS Attribute,LotNumber AS LotNumber,PTCFrom AS PTCFrom,Warehouse AS Warehouse,Location AS Position,Unit AS Unit," +
                //    "KTNUM AS WN,TZNUM AS AdjN,KTFZNUM AS WQN,TZFZNUM AS AdjQN,PTCTo AS PTCTo,DetailState AS StateD,DetailNote AS Note " +
                //    "FROM View_SM_MTO WHERE" + condition;
            }         
           
        }


        //导出
        //库存=0；入库单=1；结转备库=2；领料单=3；项目完工=8(暂时未用)，项目结转=9 ,MTO=11
        protected void MTOOutExport_Click(object sender, EventArgs e)
        {
            string condition = GetCondition().Replace("'", "*");
            List<string> sqllist = new List<string>();
            string sql = "DELETE FROM TBWS_EXPORTCONDITION WHERE SessionID='" + Session["UserID"].ToString() + "' AND Type='11'";
            sqllist.Add(sql);
            sql = "INSERT INTO TBWS_EXPORTCONDITION (SessionID,Type,StrCondition) VALUES ('" + Session["UserID"].ToString() + "','11','" + condition + "')";
            sqllist.Add(sql);
            DBCallCommon.ExecuteTrans(sqllist);

            ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>MTOOutExport();</script>");
        }


        //查询
        protected void Query_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;

            bindData();
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
        //单据头显示
        protected void CheckBoxShow_CheckedChanged(object sender, EventArgs e)
        {
            bindData();

            UpdatePanelBody.Update();
        }

        //清除条件
        private void clearCondition()
        {

            //审核状态
            foreach (ListItem lt in DropDownListState.Items)
            {
                if (lt.Selected)
                {
                    lt.Selected = false;
                }
            }
            DropDownListState.Items[0].Selected = true;
            //领料单编号
            TextBoxMTOCode.Text = string.Empty;
            //加工单位
            TextBoxDate.Text = string.Empty;
            TextBoxDep.Text = string.Empty;
            TextBoxPlaner.Text = string.Empty;
            TextBoxCode.Text = string.Empty;
            TextBoxName.Text = string.Empty;
            TextBoxPTCFrom.Text = string.Empty;
            TextBoxPTCTo.Text = string.Empty;
            TextBoxGuiGe.Text = string.Empty;
            TextBoxCaiZhi.Text = string.Empty;
            drp_xposition.SelectedIndex = 0;
            drp_Warhouse.SelectedIndex = 0;
            TextBoxPhao.Text = string.Empty;
            TextBoxZHDan.Text = string.Empty;
            TextBoxTiaozh.Text = string.Empty;
            TextBoxKetiao.Text = string.Empty;
            TextBoxshher.Text = string.Empty;
            TextBoxBz.Text = string.Empty;

        }
       

        protected string convertState(string state)
        {
            switch (state)
            {
                case "1": return "<font color='#FF0000'>未审核</font>";
                case "2": return "已审核";
                default: return state;
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("LabelWN")).Text.ToString() != "")
                {
                    twn += Convert.ToDouble(((Label)e.Item.FindControl("LabelWN")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("LabelWQN")).Text.ToString() != "")
                {
                    twqn += Convert.ToInt32(((Label)e.Item.FindControl("LabelWQN")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("LabelAdjN")).Text.ToString() != "")
                {
                    tan += Convert.ToDouble(((Label)e.Item.FindControl("LabelAdjN")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("LabelAdjQN")).Text.ToString() != "")
                {
                    taqn += Convert.ToInt32(((Label)e.Item.FindControl("LabelAdjQN")).Text.ToString());
                }                 
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("LabelTotalWN")).Text = twn.ToString();
                ((Label)e.Item.FindControl("LabelTotalWQN")).Text = twqn.ToString();
                ((Label)e.Item.FindControl("LabelTotalAdjN")).Text = tan.ToString();
                ((Label)e.Item.FindControl("LabelTotalAdjQN")).Text = taqn.ToString();
                //总计
                ((Label)e.Item.FindControl("TotalAdjN")).Text = hfdTotalAdjN.Value; //调整数量
                ((Label)e.Item.FindControl("TotalWN")).Text = hfdTotalWN.Value; //可调整数量
                ((Label)e.Item.FindControl("TotalAdjQN")).Text = hfdTotalAdjQN.Value; //调整辅助数量
                ((Label)e.Item.FindControl("TotalWQN")).Text = hfdTotalWQN.Value;// 可调整辅助数量

            }
        }

        private void BindItem()
        {
            for (int i = 0; i < (Repeater1.Items.Count - 1); i++)
            {

                Label lbCode = (Repeater1.Items[i].FindControl("LabelCode") as Label);

                string NextCode = lbCode.Text;

                if (lbCode.Visible)
                {
                    for (int j = i + 1; j < Repeater1.Items.Count; j++)
                    {
                        string Code = (Repeater1.Items[j].FindControl("LabelCode") as Label).Text;

                        if (NextCode == Code)
                        {
                            (Repeater1.Items[j].FindControl("LabelCode") as Label).Style.Add("display", "none");
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


            for (int i = 0; i < 7; i++)
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

            //MTOCode AS Code,Date AS Date,Dep AS Dep,Planer AS Planer,Doc AS Document,Verifier AS Verifier,left(VerifyDate,10) AS ApproveDate,TotalState AS State,TotalNote AS Comment,UniqueCode AS UniqueID,MaterialCode AS MaterialCode,MaterialName AS MaterialName,Standard AS MaterialStandard,Attribute AS Attribute,LotNumber AS LotNumber,Length,Width,PTCFrom AS PTCFrom,Warehouse AS Warehouse,Location AS Position,Unit AS Unit,KTNUM AS WN,TZNUM AS AdjN,KTFZNUM AS WQN,TZFZNUM AS AdjQN,PTCTo AS PTCTo,DetailState AS StateD,DetailNote AS Note 
            
            //

            ItemList.Add("NO", "");
            ItemList.Add("MTOCode", "MTO单号");
            ItemList.Add("MaterialCode", "物料编码");
            ItemList.Add("MaterialName", "物料名称");
            ItemList.Add("Standard", "规格型号");
            ItemList.Add("Attribute", "材质");
            ItemList.Add("PTCFrom", "从计划跟踪号");
            ItemList.Add("PTCTo", "到计划跟踪号");
            ItemList.Add("Warehouse", "仓库");
            ItemList.Add("Location", "仓位");
            ItemList.Add("TZNUM", "调整数量");
            ItemList.Add("KTNUM", "可调数量");
            ItemList.Add("Doc", "制单人");
            ItemList.Add("Verifier", "审核人");
           

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





    }
}
