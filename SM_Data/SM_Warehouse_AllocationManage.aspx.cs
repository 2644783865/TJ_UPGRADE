using System;
using System.Collections;
using System.Collections.Generic;
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

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_AllocationManage : System.Web.UI.Page
    {
        double tn = 0;
        int tqn = 0;

        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
                bindData();
                this.Form.DefaultButton = QueryButton.UniqueID;
                //CheckUser(ControlFinder);
            }
        }


        private void InitVar()
        {
            getAllocationInfo(); ;
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


        protected void getAllocationInfo()
        {
            string state = DropDownListState.SelectedValue.ToString();

            string condition = "";
            //单号条件
            if (TextBoxALCode.Text != "")
            {
                condition = " ALCode LIKE '%" + TextBoxALCode.Text + "%'";
            }
            //调出仓库条件
            if ((TextBoxWarehouseOut.Text != "") && (condition != ""))
            {
                condition += " AND " + " WarehouseOut LIKE '%" + TextBoxWarehouseOut.Text + "%'";
            }
            else if ((TextBoxWarehouseOut.Text != "") && (condition == ""))
            {
                condition += " WarehouseOut LIKE '%" + TextBoxWarehouseOut.Text + "%'";
            }
            //调入仓库条件
            if ((TextBoxWarehouseIn.Text != "") && (condition != ""))
            {
                condition += " AND " + " WarehouseIn LIKE '%" + TextBoxWarehouseIn.Text + "%'";
            }
            else if ((TextBoxWarehouseIn.Text != "") && (condition == ""))
            {
                condition += " WarehouseIn LIKE '%" + TextBoxWarehouseIn.Text + "%'";
            }
            //时间条件
            if ((TextBoxDate.Text != "") && (condition != ""))
            {
                condition += " AND " + " Date LIKE '%" + TextBoxDate.Text + "%'";
            }
            else if ((TextBoxDate.Text != "") && (condition == ""))
            {
                condition += " Date LIKE '%" + TextBoxDate.Text + "%'";
            }
            //物料代码条件
            if ((TextBoxCode.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialCode LIKE '%" + TextBoxCode.Text + "%'";
            }
            else if ((TextBoxCode.Text != "") && (condition == ""))
            {
                condition += " MaterialCode LIKE '%" + TextBoxCode.Text + "%'";
            }
            //物料名称条件
            if ((TextBoxName.Text != "") && (condition != ""))
            {
                condition += " AND " + " MaterialName LIKE '%" + TextBoxName.Text + "%'";
            }
            else if ((TextBoxName.Text != "") && (condition == ""))
            {
                condition += " MaterialName LIKE '%" + TextBoxName.Text + "%'";
            }
            //规格型号条件
            if ((TextBoxStandard.Text != "") && (condition != ""))
            {
                condition += " AND " + " Standard LIKE '%" + TextBoxStandard.Text + "%'";
            }
            else if ((TextBoxStandard.Text != "") && (condition == ""))
            {
                condition += " Standard LIKE '%" + TextBoxStandard.Text + "%'";
            }
            //计划跟踪号条件
            if ((TextBoxPTC.Text != "") && (condition != ""))
            {
                condition += " AND " + " PTC LIKE '%" + TextBoxPTC.Text + "%'";
            }
            else if ((TextBoxPTC.Text != "") && (condition == ""))
            {
                condition += " PTC LIKE '%" + TextBoxPTC.Text + "%'";
            } 

            switch (state)
            {
                case "": break;
                case "1":
                    //制单人
                    if (condition != "") { condition += " AND TotalState='1' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    else { condition += " TotalState='1' AND DocCode='" + Session["UserID"].ToString() + "'"; }
                    break;
                case "2":
                    //收料人
                    if (condition != "") { condition += " AND TotalState='2' AND AcceptanceCode='" + Session["UserID"].ToString() + "'"; }
                    else { condition += " TotalState='2' AND AcceptanceCode='" + Session["UserID"].ToString() + "'"; }
                    break;
                case "3":
                    //制单人或者收料人
                    if (condition != "") { condition += " AND TotalState='3' AND (DocCode='" + Session["UserID"].ToString() + "' OR AcceptanceCode='" + Session["UserID"].ToString() + "')"; }
                    else { condition += " TotalState='3' AND (DocCode='" + Session["UserID"].ToString() + "' OR AcceptanceCode='" + Session["UserID"].ToString() + "')"; }
                    break;
                default: break;
            }
            if (condition == "")
            {
                string TableName = "View_SM_Allocation";
                string PrimaryKey = "AL_ID";
                string ShowFields = "ALCode AS Code,Date,Keeper AS Keeping,Doc AS Document,Verifier,left(VerifyDate,10) as VerifyDate,TotalState AS State,VState,UniqueCode AS UniqueID,WarehouseOut AS WarehouseOut,LocationOut,LocationIn,WarehouseIn AS WarehouseIn,MaterialCode AS MaterialCode,MaterialName AS MaterialName,Standard AS MaterialStandard,Fixed AS Fixed,Length,Width,Attribute AS Attribute,Unit AS Unit, TZFZNUM AS Quantity,LotNumber AS LotNumber,TZNUM AS Number,PTC AS PTC";
                string OrderField = "ALCode DESC,UniqueCode";
                int OrderType = 0;
                string StrWhere = "";
                int PageSize = 50;

                InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);


                //sql = "SELECT ALCode AS Code,Date AS Date,Keeper AS Keeping,Doc AS Document," +
                //    "TotalState AS State,UniqueCode AS UniqueID,WarehouseOut AS WarehouseOut," +
                //    "WarehouseIn AS WarehouseIn,MaterialCode AS MaterialCode,MaterialName AS MaterialName," +
                //    "Standard AS MaterialStandard,Fixed AS Fixed,Attribute AS Attribute,Unit AS Unit," +
                //    "TZFZNUM AS Quantity,LotNumber AS LotNumber," +
                //    "TZNUM AS Number,PTC AS PTC FROM View_SM_Allocation";
            }
            else
            {

                string TableName = "View_SM_Allocation";
                string PrimaryKey = "AL_ID";
                string ShowFields = "ALCode AS Code,Date,Keeper AS Keeping,Doc AS Document,Verifier,left(VerifyDate,10) as VerifyDate,TotalState AS State,VState,UniqueCode AS UniqueID,WarehouseOut AS WarehouseOut,LocationOut,LocationIn,WarehouseIn AS WarehouseIn,MaterialCode AS MaterialCode,MaterialName AS MaterialName,Standard AS MaterialStandard,Fixed AS Fixed,Length,Width,Attribute AS Attribute,Unit AS Unit, TZFZNUM AS Quantity,LotNumber AS LotNumber,TZNUM AS Number,PTC AS PTC";
                string OrderField = "ALCode DESC,UniqueCode";
                int OrderType = 0;
                string StrWhere = condition;
                int PageSize = 50;

                InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);

            }
            
        }

        protected void DropDownListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            getAllocationInfo();
        }

        protected string convertState(string state)
        {
            switch(state)
            {
                case "10": return "未提交"; 
                case "20": return "待审核"; 
                case "30": return "已审核";
                case "11": return "驳回";
                default: return state;
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("LabelNumber")).Text.ToString() != "")
                {
                    tn += Convert.ToDouble(((Label)e.Item.FindControl("LabelNumber")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("LabelQuantity")).Text.ToString() != "")
                {
                    tqn += Convert.ToInt32(((Label)e.Item.FindControl("LabelQuantity")).Text.ToString());
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("LabelTotalNumber")).Text = Math.Round(tn,4).ToString();
                ((Label)e.Item.FindControl("LabelTotalQuantity")).Text = tqn.ToString();
            }
        }

        //查询
        protected void QueryButton_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;

            bindData();

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
            TextBoxALCode.Text = string.Empty;
            TextBoxWarehouseOut.Text = string.Empty;
            TextBoxWarehouseIn.Text = string.Empty;
            TextBoxDate.Text = string.Empty;
            TextBoxCode.Text = string.Empty;
            TextBoxName.Text = string.Empty;
            TextBoxStandard.Text = string.Empty;
            TextBoxPTC.Text = string.Empty;
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
    }
}
