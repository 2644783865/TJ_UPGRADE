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
    public partial class SM_WarehouseOUT_LL_Manage : BasicPage
    {

        private double trn = 0;
        private double ta = 0;
        private double tfn = 0;

        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {

           
            InitVar();

            if (!IsPostBack)
            {
                //2016.8.25修改
                string contract_task_view = Request["contract_task_view"];
                if (!string.IsNullOrEmpty(contract_task_view))
                {
                    TextBoxEngid.Text = contract_task_view.Trim();
                }

                if (Session["POSITION"].ToString().Trim() == "0501")
                {
                    DropDownListState.SelectedValue = "1";
                }
                InitVar();
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
                bindData();

                this.Form.DefaultButton = btnQuery.UniqueID;

                bindGV();//绑定条件框
                GetWarehouse();
                GetChildWarehouse();    
            }
            CheckUser(ControlFinder);   
        }
       

        private void InitVar()
        {

            getLLInfo();
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

            CommonFun.Paging(dt, RepeaterLL, UCPaging1, NoDataPanel);

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

        private void GetWarehouse()
        {
            string sql = "SELECT DISTINCT WS_ID,WS_NAME FROM TBWS_WAREHOUSE  ORDER BY WS_ID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            WarehouseDropDownList.DataSource = dt;
            WarehouseDropDownList.DataTextField = "WS_NAME";
            WarehouseDropDownList.DataValueField = "WS_ID";
            WarehouseDropDownList.DataBind();
            ListItem item = new ListItem("--请选择--", "0");
            WarehouseDropDownList.Items.Insert(0, item);
        }

        private void GetChildWarehouse()
        {
            if (WarehouseDropDownList.SelectedItem.Text != "--请选择--")
            {
                string sql = "SELECT DISTINCT WL_ID,WL_NAME FROM TBWS_LOCATION WHERE WL_WSID='" + WarehouseDropDownList.SelectedValue + "' ORDER BY WL_NAME";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                ChildWarehouseDropDownList.DataSource = dt;
                ChildWarehouseDropDownList.DataTextField = "WL_NAME";
                ChildWarehouseDropDownList.DataValueField = "WL_ID";
                ChildWarehouseDropDownList.DataBind();
            }
            else
            {
                ChildWarehouseDropDownList.Items.Clear();
            }
            ListItem item = new ListItem("--请选择--", "0");
            ChildWarehouseDropDownList.Items.Insert(0, item);

        }

        private string GetStrCondition()
        {

            string state = DropDownListState.SelectedValue.ToString();
            string colour = DropDownListColour.SelectedValue.ToString();
            string lastcondition = "";
            if (CheckBoxSearch.Checked)
            {
                lastcondition = ViewState["lastcondition0"].ToString();
            }          

            string condition = " (BillType='1' OR BillType='4') ";

            //旧单单号
            if (TextBoxCode.Text != "" && (condition != ""))
            {
                condition += " AND OutCode LIKE '%" + TextBoxCode.Text.Trim().PadLeft(8, '0') + "%'";
            }

            //是否现场直发
            if (drpifxczf.SelectedValue == "0")
            {
                condition += " and (OP_XCZF is null or OP_XCZF='')";
            }
            else if (drpifxczf.SelectedValue == "1")
            {
                condition += " and OP_XCZF='1' and OP_XCZF is not null";
            }

            //新单编号
           
            if (TextBoxNewCode.Text != "" && (condition != ""))
            {
                condition += " AND id LIKE '%" + TextBoxNewCode.Text.Trim() + "%'";
            }
            else if (TextBoxNewCode.Text != "" && (condition == ""))
            {
                condition += " id LIKE '%" + TextBoxNewCode.Text.Trim() + "%'";
            }

            //制单开始结束时间条件

            //if ((TextBoxZhDStartTime.Text.Trim() == "") && (TextBoxZhDEnd.Text.Trim() == "") && (condition != ""))
            //{
            //    condition += " AND " + " Date >= '" + TextBoxZhDStartTime.Text.Trim() + "'";
            //}
            //if ((TextBoxZhDStartTime.Text.Trim() == "") && (TextBoxZhDEnd.Text.Trim() != "") && (condition != ""))
            //{
            //    condition += " AND " + " Date <= '" + TextBoxZhDEnd.Text.Trim() + " 24:00:00'";//到24点结束
            //}
            //if ((TextBoxZhDStartTime.Text.Trim() != "") && (TextBoxZhDEnd.Text.Trim() != "") && (condition != ""))
            //{
            //    condition += " AND " + " Date between '" + TextBoxZhDStartTime.Text.Trim() + "' and '" + TextBoxZhDEnd.Text.Trim() + " 24:00:00'";//到24点结束
            //}
            //if ((TextBoxZhDStartTime.Text.Trim() != "") && (TextBoxZhDEnd.Text.Trim() == "") && (condition != ""))
            //{
            //    condition += " AND " + " Date >= '" + TextBoxZhDStartTime.Text.Trim() + "'";
            //}


            //审核时间条件
            if ((TextBoxStartDate.Text.Trim() == "") && (TextBoxDate.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " ApprovedDate <= '" + TextBoxDate.Text.Trim() + " 24:00:00'";//到24点结束
            }
            if ((TextBoxStartDate.Text.Trim() != "") && (TextBoxDate.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " ApprovedDate between '" + TextBoxStartDate.Text.Trim() + "' and '" + TextBoxDate.Text.Trim() + " 24:00:00'";//到24点结束
            }
            if ((TextBoxStartDate.Text.Trim() != "") && (TextBoxDate.Text.Trim() == "") && (condition != ""))
            {
                condition += " AND " + " ApprovedDate >= '" + TextBoxStartDate.Text.Trim() + "'";
            }

            //领料部门条件
            if ((TextBoxDep.Text != "") && (condition != ""))
            {
                condition += " AND " + " Dep LIKE '%" + TextBoxDep.Text.Trim() + "%'";
            }
            else if ((TextBoxDep.Text != "") && (condition == ""))
            {
                condition += " Dep LIKE '%" + TextBoxDep.Text.Trim() + "%'";
            }

            //制单日期

            if ((TextBoxZDDate.Text != "") && (condition != ""))
            {
                condition += " AND " + " Date LIKE '%" + TextBoxZDDate.Text.Trim() + "%'";
            }
            else if ((TextBoxZDDate.Text != "") && (condition == ""))
            {
                condition += " Date LIKE '%" + TextBoxZDDate.Text.Trim() + "%'";
            }


            //审核人
            if ((TextBoxVrifier.Text != "") && (condition != ""))
            {
                condition += " AND " + " Verifier LIKE'%" + TextBoxVrifier.Text.Trim() + "%'";
            }
            else if ((TextBoxVrifier.Text != "") && (condition == ""))
            {
                condition += " Verifier LIKE '%" + TextBoxVrifier.Text.Trim() + "%'";
            }
            //物料代码条件
            if ((TextBoxMCode.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialCode LIKE '%" + TextBoxMCode.Text.Trim() + "%'";
            }
            else if ((TextBoxMCode.Text != "") && (condition == ""))
            {
                condition += " MaterialCode LIKE '%" + TextBoxMCode.Text.Trim() + "%'";
            }
            //物料名称条件
            if ((TextBoxMName.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialName LIKE '%" + TextBoxMName.Text.Trim() + "%'";
            }
            else if ((TextBoxMName.Text != "") && (condition == ""))
            {
                condition += " MaterialName LIKE '%" + TextBoxMName.Text.Trim() + "%'";
            }
            //规格型号条件
            if ((TextBoxMStandard.Text != "") && (condition != ""))
            {
                condition += " AND " + " Standard LIKE '%" + TextBoxMStandard.Text.Trim() + "%'";
            }
            else if ((TextBoxMStandard.Text != "") && (condition == ""))
            {
                condition += " Standard LIKE '%" + TextBoxMStandard.Text.Trim() + "%'";
            }
            //计划跟踪号条件
            if ((TextBoxMPTC.Text != "") && (condition != ""))
            {
                condition += " AND " + " PTC LIKE '%" + TextBoxMPTC.Text.Trim() + "%'";
            }
            else if ((TextBoxMPTC.Text != "") && (condition == ""))
            {
                condition += " PTC LIKE '%" + TextBoxMPTC.Text.Trim() + "%'";
            }

            //生产制号
            if ((TextBoxEngid.Text != "") && (condition != ""))
            {
                condition += " AND " + " TSAID LIKE '%" + TextBoxEngid.Text.Trim().ToUpper() + "%'";
            }
            else if ((TextBoxEngid.Text != "") && (condition == ""))
            {
                condition += " TSAID LIKE '%" + TextBoxEngid.Text.Trim().ToUpper() + "%'";
            }

            //制作班组 TextBoxZZBZ

            if ((TextBoxZZBZ.Text != "") && (condition != ""))
            {
                condition += " AND " + " TotalNote LIKE '%" + TextBoxZZBZ.Text.Trim() + "%'";
            }
            else if ((TextBoxZZBZ.Text != "") && (condition == ""))
            {
                condition += " TotalNote LIKE '%" + TextBoxZZBZ.Text.Trim() + "%'";
            }

            //制单人

            if ((TextBoxZDR.Text != "") && (condition != ""))
            {
                condition += " AND " + " Doc LIKE '%" + TextBoxZDR.Text.Trim() + "%'";
            }
            else if ((TextBoxZDR.Text != "") && (condition == ""))
            {
                condition += " Doc LIKE '%" + TextBoxZDR.Text.Trim() + "%'";
            }
            //材质

            if ((TextBoxCZ.Text != "") && (condition != ""))
            {
                condition += " AND " + " Attribute LIKE '%" + TextBoxCZ.Text.Trim() + "%'";
            }
            else if ((TextBoxCZ.Text != "") && (condition == ""))
            {
                condition += " Attribute LIKE '%" + TextBoxCZ.Text.Trim() + "%'";
            }

            //发料人

            if ((TextBoxSender.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " Sender LIKE '%" + TextBoxSender.Text.Trim() + "%'";
            }
            else if ((TextBoxSender.Text.Trim() != "") && (condition == ""))
            {
                condition += " Sender LIKE '%" + TextBoxSender.Text.Trim() + "%'";
            }

            //入库单号

            if ((TextBoxInCode.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " PTC in (select WG_PTCODE from TBWS_INDETAIL where WG_CODE LIKE '%" + TextBoxInCode.Text.Trim().PadLeft(9, '0') + "%' and left(WG_CODE,1)='G') ";
            }
            else if ((TextBoxInCode.Text.Trim() != "") && (condition == ""))
            {
                condition += " PTC in (select WG_PTCODE from TBWS_INDETAIL where WG_CODE LIKE '%" + TextBoxInCode.Text.Trim().PadLeft(9, '0') + "%' and left(WG_CODE,1)='G') ";
            }

            //计划模式

            if ((TextBoxJhua.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " PlanMode LIKE '%" + TextBoxJhua.Text.Trim() + "%' ";
            }
            else if ((TextBoxJhua.Text.Trim() != "") && (condition == ""))
            {
                condition += " PlanMode LIKE '%" + TextBoxJhua.Text.Trim() + "%'";
            }

            //标识号

            if ((TextBoxBshi.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " OP_BSH LIKE '%" + TextBoxBshi.Text.Trim() + "%'";
            }
            else if ((TextBoxBshi.Text.Trim() != "") && (condition == ""))
            {
                condition += " OP_BSH LIKE '%" + TextBoxBshi.Text.Trim() + "%' ";
            }
                ////仓库

                //if ((TextBoxcangku.Text.Trim() != "") && (condition != ""))
                //{
                //    condition += " AND " + " Warehouse LIKE '%" + TextBoxcangku.Text.Trim() + "%'";
                //}
                //else if ((TextBoxcangku.Text.Trim() != "") && (condition == ""))
                //{
                //    condition += " Warehouse LIKE '%" + TextBoxcangku.Text.Trim() + "%' ";
                //}
                ////仓位

                //if ((TextBoxwareposition.Text.Trim() != "") && (condition != ""))
                //{
                //    condition += " AND " + " Location LIKE '%" + TextBoxwareposition.Text.Trim() + "%'";
                //}
                //else if ((TextBoxwareposition.Text.Trim() != "") && (condition == ""))
                //{
                //    condition += " Location LIKE '%" + TextBoxwareposition.Text.Trim() + "%' ";
                //}

            //仓库条件
            if ((WarehouseDropDownList.SelectedValue != "0") && (ChildWarehouseDropDownList.SelectedValue != "0") && (WarehouseDropDownList.SelectedValue!=""))
            {
                if (condition != "")
                {
                    condition += " AND " + " WarehouseCode='" + WarehouseDropDownList.SelectedValue + " '" + " AND " + " LocationCode='"+ChildWarehouseDropDownList.SelectedValue+"' ";
                    
                }
                else
                {
                    condition += " WarehouseCode='" + WarehouseDropDownList.SelectedValue + " '" + " AND " + " LocationCode='" + ChildWarehouseDropDownList.SelectedValue + "' ";
                    

                }
            }
            else if ((WarehouseDropDownList.SelectedValue != "0") && (ChildWarehouseDropDownList.SelectedValue == "0") && (WarehouseDropDownList.SelectedValue != ""))
            {
                if (condition != "")
                {
                    condition += " AND " + " WarehouseCode LIKE '" + WarehouseDropDownList.SelectedValue + "%'";
                    
                }
                else
                {
                    condition += " WarehouseCode LIKE '" + WarehouseDropDownList.SelectedValue + "%'";
                    
                }
            }


            //实发数量

            if ((TextBoxShifanum.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " RealNumber LIKE '%" + TextBoxShifanum.Text.Trim() + "%'";
            }
            else if ((TextBoxShifanum.Text.Trim() != "") && (condition == ""))
            {
                condition += " RealNumber LIKE '%" + TextBoxShifanum.Text.Trim() + "%' ";
            }


            //红蓝字条件
            switch (colour)
            {
                case "": break;
                case "0":
                    if (condition != "") { condition += " AND ROB='0' "; }
                    else { condition += " ROB='0' "; }
                    break;
                case "1":
                    if (condition != "") { condition += " AND ROB='1' "; }
                    else { condition += " ROB='1' "; }
                    break;
                default: break;
            }
            //状态条件
            switch (state)
            {
                case "": break;
                case "1":
                    if (condition != "") { condition += " AND TotalState='1' "; }
                    else { condition += " TotalState='1' "; }
                    break;
                case "2":
                    if (condition != "") { condition += " AND TotalState='2' "; }
                    else { condition += " TotalState='2' "; }
                    break;
                default: break;
            }

            if (cz_gangcai.Checked == true && cz_wujin.Checked == false)
            {
                condition += " and  (MaterialCode like '01.07%') ";
            }
            if (cz_wujin.Checked == true && cz_gangcai.Checked == false)
            {

                condition += " and (MaterialCode not like '01.07%') ";
            }

            string SubCondtion = GetSubCondtion();

            if (condition != "")
            {               

                if (SubCondtion != "")

                    condition += DropDownListFatherLogic.SelectedValue + " (" + SubCondtion + ")";

            }
            else
            {
                if (SubCondtion != "")
                    condition += SubCondtion;
            }



            ViewState["lastcondition0"] = condition;

            if (CheckBoxSearch.Checked)
            {
                condition += " AND " + lastcondition;
                ViewState["lastcondition0"] = condition;

            }            

            return condition;
        
        }


        protected void getLLInfo()
        {
            if (Request.QueryString["FLAG"] != null)  //需用执行
            {
                TextBoxEngid.Text = Request.QueryString["eng"]; //需用执行 工程
                TextBoxMCode.Text = Request.QueryString["mar"]; //需用执行 物料代码
            }
            if (Request.QueryString["FLAG"] == "XROUT") // 需用计划查看
            {
               TextBoxMPTC.Text=Request.QueryString["PTC"];
            }

            string condition = GetStrCondition();

            if (condition == "")
            {

                string TableName = "View_SM_OUT";
                string PrimaryKey = "OP_ID";
                string ShowFields = "id as TrueCode,OutCode AS Code,DepCode AS DepCode,Dep AS Dep,WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,Date AS Date,left(ApprovedDate,10) AS ApprovedDate,SenderCode AS SenderCode,Sender AS Sender,DocCode,Doc,VerifierCode AS VerifierCode,Verifier,ApprovedDate AS ApproveDate,ROB AS Colour,TotalState AS State,case when TotalNote='0' then '未选择' else TotalNote end AS Comment,SQCODE AS SQCODE,UniqueCode AS UniqueID,MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Length AS Length,Width AS Width,LotNumber AS LotNumber,Unit AS Unit,cast(round(UnitPrice,4) as float) AS UnitPrice,DueNumber AS DN,cast(RealNumber as float) AS RN,RealSupportNumber,cast(round(Amount,2) as float) AS Amount,WarehouseCode AS WarehouseOutCode,Warehouse AS WarehouseOut,LocationCode AS PositionOutCode,Location AS PositionOut,PlanMode AS PlanMode,PTC AS PTC,TSAID,ZZBZNM,DetailNote AS Note,OP_BSH AS BSH,OP_PAGENUM,OP_XCZF";
                string OrderField = "id DESC,UniqueCode";
                int OrderType = 0;
                string StrWhere = "";
                int PageSize = 50;
                InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);

            }
            else
            {
                string TableName = "View_SM_OUT";
                string PrimaryKey = "OP_ID";
                string ShowFields = "id as TrueCode,OutCode AS Code,DepCode AS DepCode,Dep AS Dep,WarehouseCode AS WarehouseCode,Warehouse AS Warehouse,Date AS Date,left(ApprovedDate,10) AS ApprovedDate,SenderCode AS SenderCode,Sender AS Sender,DocCode AS DocCode,Doc AS Doc,VerifierCode AS VerifierCode,Verifier AS Verifier,ApprovedDate AS ApproveDate,ROB AS Colour,TotalState AS State,case when TotalNote='0' then '未选择' else TotalNote end AS Comment,SQCODE AS SQCODE,UniqueCode AS UniqueID,MaterialCode AS MaterialCode,MaterialName AS MaterialName,Attribute AS Attribute,GB AS GB,Standard AS MaterialStandard,Length AS Length,Width AS Width,LotNumber AS LotNumber,Unit AS Unit,cast(round(UnitPrice,4) as float) AS UnitPrice,DueNumber AS DN,cast(RealNumber as float) AS RN,RealSupportNumber,cast(round(Amount,2) as float) AS Amount,WarehouseCode AS WarehouseOutCode,Warehouse AS WarehouseOut,LocationCode AS PositionOutCode,Location AS PositionOut,PlanMode AS PlanMode,PTC AS PTC,TSAID,ZZBZNM,DetailNote AS Note,OP_BSH AS BSH,OP_PAGENUM,OP_XCZF";
                string OrderField = "id DESC,UniqueCode";
                int OrderType = 0;
                string StrWhere = condition;
                int PageSize = 50;
                InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);
            }



            string sql = "select sum(RealNumber) as TotalRN,round(sum(Amount),2) as TotalAmount,round(sum(RealSupportNumber),2) as TotalFRN from View_SM_OUT where " + condition;
            SqlDataReader sdr=DBCallCommon.GetDRUsingSqlText(sql);
            if (sdr.Read())
            {
                hfdTotalRN.Value = sdr["TotalRN"].ToString();
                hfdTotalAmount.Value = sdr["TotalAmount"].ToString();
                hfdTotalFRN.Value = sdr["TotalFRN"].ToString();
                
            }
            sdr.Close();

            //根据制单人，页数汇总

            string sqlnum = "select sum(OP_PAGENUM) as PAGENUM from TBWS_OUT where OP_DOC in (select ST_CODE from TBDS_STAFFINFO where ST_NAME LIKE '%" + TextBoxZDR.Text + "%' and ST_DEPID='07')";
            SqlDataReader sdrnum = DBCallCommon.GetDRUsingSqlText(sqlnum);
            if (sdrnum.Read())
            {
                hfdPageNum.Value = sdrnum["PAGENUM"].ToString();
            }

            sdrnum.Close();
        }

        protected void RepeaterLL_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("LabelRN")).Text.ToString() != "")
                {
                    trn += Convert.ToDouble(((Label)e.Item.FindControl("LabelRN")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("LabelAmount")).Text.ToString() != "")
                {
                    ta += Convert.ToDouble(((Label)e.Item.FindControl("LabelAmount")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("LabelAmount")).Text.ToString() != "")
                {
                    tfn += Convert.ToDouble(((Label)e.Item.FindControl("LabelFRN")).Text.ToString());
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {

                ((Label)e.Item.FindControl("LabelTotalRN")).Text = Math.Round(trn, 4).ToString();
                ((Label)e.Item.FindControl("LabelTotalAmount")).Text = Math.Round(ta, 2).ToString();
                ((Label)e.Item.FindControl("LabelTotalFRN")).Text = Math.Round(tfn, 2).ToString();

                ((Label)e.Item.FindControl("TotalRN")).Text = hfdTotalRN.Value;
                ((Label)e.Item.FindControl("TotalAmount")).Text = hfdTotalAmount.Value;
                ((Label)e.Item.FindControl("TotalPageNum")).Text = hfdPageNum.Value;
                ((Label)e.Item.FindControl("TotalFRN")).Text = hfdTotalFRN.Value;
            }
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

        //导出
        //库存=0；入库单=1；结转备库=2；领料单=3；项目完工=8(暂时未用)，项目结转=9

        protected void BtnShowExport_Click(object sender, EventArgs e)
        {
            string condition = GetStrCondition().Replace("'","*");
            List<string> sqllist = new List<string>();
            string sql = "delete from TBWS_EXPORTCONDITION where SessionID='" + Session["UserID"].ToString() + "' AND Type='3'";
            sqllist.Add(sql);
            sql = "insert into TBWS_EXPORTCONDITION (SessionID,Type,StrCondition) VALUES ('" + Session["UserID"].ToString() + "','3','" + condition + "')";
            sqllist.Add(sql);
            DBCallCommon.ExecuteTrans(sqllist);

            ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>OutExport();</script>");
        }

        private void BindItem()
        {

            for (int i = 0; i < (RepeaterLL.Items.Count - 1); i++)
            {

                Label lbCode = (RepeaterLL.Items[i].FindControl("LabelCode") as Label);

                string NextCode = lbCode.Text;

                if (lbCode.Visible)
                {
                    for (int j = i + 1; j < RepeaterLL.Items.Count; j++)
                    {
                        string Code = (RepeaterLL.Items[j].FindControl("LabelCode") as Label).Text;

                        if (NextCode == Code)
                        {
                            (RepeaterLL.Items[j].FindControl("LabelCode") as Label).Style.Add("display", "none");
                            (RepeaterLL.Items[j].FindControl("LabelTrueCode") as Label).Style.Add("display", "none");
                            (RepeaterLL.Items[j].FindControl("LabelPageNum") as Label).Style.Add("display", "none"); 
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        protected void WarehouseDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetChildWarehouse();
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

            //红蓝字
            foreach (ListItem lt in DropDownListColour.Items)
            {
                if (lt.Selected)
                {
                    lt.Selected = false;
                }
            }
            DropDownListColour.Items[0].Selected = true;


            //领料单编号
            TextBoxCode.Text = string.Empty;
            //领料部门
            TextBoxDep.Text = string.Empty;
            TextBoxVrifier.Text = string.Empty;
            TextBoxStartDate.Text = string.Empty;
            TextBoxDate.Text = string.Empty;
            //TextBoxZhDEnd.Text = string.Empty;
            //TextBoxZhDStartTime.Text = string.Empty;
            TextBoxMCode.Text = string.Empty;
            TextBoxMName.Text = string.Empty;
            TextBoxMStandard.Text = string.Empty;
            TextBoxMPTC.Text = string.Empty;
            TextBoxZDR.Text = string.Empty;
            TextBoxCZ.Text = string.Empty;
            TextBoxEngid.Text = string.Empty;
            TextBoxZDDate.Text = string.Empty;
            TextBoxZZBZ.Text = string.Empty;
            TextBoxSender.Text = string.Empty;
            TextBoxInCode.Text = string.Empty;
            TextBoxNewCode.Text = string.Empty;
               //TextBoxcangku.Text = string.Empty;
               //TextBoxwareposition.Text = string.Empty;
            TextBoxJhua.Text = string.Empty;
            TextBoxBshi.Text = string.Empty;              
            TextBoxShifanum.Text = string.Empty;

            WarehouseDropDownList.ClearSelection();
            WarehouseDropDownList.Items[0].Selected = true;

            ChildWarehouseDropDownList.ClearSelection();
            ChildWarehouseDropDownList.Items[0].Selected = true;
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


        //关联单据需要加上物料编码


        protected void Related_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < RepeaterLL.Items.Count; i++)
            {
                if (((CheckBox)RepeaterLL.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    string ptc = ((Label)RepeaterLL.Items[i].FindControl("LabelPTC")).Text;
                    Response.Redirect("SM_Warehouse_RelatedDocument.aspx?PTC=" + ptc);
                    return;
                }
            }

            string script = @"alert('请选择一条要查询的记录！');";

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "error", script, true);
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
            ItemList.Add("id", "新出库单号");
            ItemList.Add("OutCode", "旧出库单号");
            ItemList.Add("Dep", "领料部门");
            ItemList.Add("MaterialCode", "物料编码");
            ItemList.Add("MaterialName", "物料名称");
            ItemList.Add("Standard", "规格型号");
            ItemList.Add("Attribute", "材质");
            ItemList.Add("Length", "长");
            ItemList.Add("Width", "宽");
            ItemList.Add("GB", "国标");
            ItemList.Add("Doc", "制单人");
            ItemList.Add("Sender", "发料人");
            ItemList.Add("Verifier", "审核人");
            ItemList.Add("PTC", "计划跟踪号");
            ItemList.Add("TSAID", "任务号");
            ItemList.Add("PlanMode", "计划模式");
            ItemList.Add("OP_BSH", "标识号");
            ItemList.Add("Warehouse", "仓库");
            ItemList.Add("Location", "仓位");
            ItemList.Add("RealNumber", "实发数量");
            ItemList.Add("LotNumber", "批号");
            ItemList.Add("DetailNote", "备注");
            
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

            if (field == "OutCode")
            {
                fieldValue = fieldValue.PadLeft(9, '0');
            }
          

            switch (relation)
            {
                case "0":
                    {
                        //左包含

                        obj = field + "  LIKE  '" + fieldValue + "%'";
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
                //case "7":
                //    {
                //        //不包含
                //       //obj = "(patindex ('____[^7]%',MaterialCode )>0) or MaterialCode like '02.07%'";
                //       obj = field + " NOT LIKE  '%" + fieldValue + "%'";
                //        break;
                //    }
                case "8":
                    {
                        //左不包含
                        obj = field + " not LIKE  '" + fieldValue + "%'";
                        break;
                    }
                //case "9":
                //    {
                //        //左不包含
                //        obj = field + "  LIKE  '%" + fieldValue + "'";
                //        break;
                //    }
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
