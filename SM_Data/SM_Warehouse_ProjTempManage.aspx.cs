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
using System.Data.SqlClient;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_ProjTempManage : System.Web.UI.Page
    {
        double ajnum = 0;
        int ajfnum = 0;
        double ktn = 0;
        int ktqn = 0;
      

        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            InitVar();
            if (!IsPostBack)
            {
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
                bindData();
                this.Form.DefaultButton = QueryButton.UniqueID;
                bindGV();//绑定条件框
                //CheckUser(ControlFinder);
            }
        }

        
        private void InitVar()
        {

            if (MyTask.Checked)
            {
                TextBoxDoc.Text = Session["UserName"].ToString();
            }            
            
            getInfo(); 
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

            SetRadioButtonTip();
        }


        protected void SetRadioButtonTip()
        {
          
            int mywtj = 0;    //我的未提交
            int wtj = 0;
            int mydsh = 0;    //我的待审核
            int dsh = 0;
            int mydsp = 0;   //我的待审批
            int dsp = 0;  
            
            
           //未提交
            string sqltext_mywtj = "select count(*) from tbws_projtemp where  pt_state='0' and pt_doc='" + Session["UserId"].ToString() + "'";
            string sqltext_wtj = "select count(*) from tbws_projtemp where  pt_state='0'";
            //待审核
            string sqltext_mydsh = "select count(*) from tbws_projtemp where  pt_state='1' and pt_doc='" + Session["UserId"].ToString() + "'";
            string sqltext_dsh = "select count(*) from tbws_projtemp where  pt_state='1'";

           //待审批
            string sqltext_mydsp = "select count(*) from tbws_projtemp where pt_state='2' and pt_doc='" + Session["UserId"].ToString() + "' ";

            string sqltext_dsp = "select count(*) from tbws_projtemp where pt_state='2'";
           
            mywtj = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_mywtj).Rows[0][0].ToString());
            wtj = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_wtj).Rows[0][0].ToString());
            mydsh = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_mydsh).Rows[0][0].ToString());
            dsh = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_dsh).Rows[0][0].ToString());
            mydsp = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_mydsp).Rows[0][0].ToString());
            dsp = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_dsp).Rows[0][0].ToString());

            RadioButtonListState.Items.FindByValue("0").Text = "未提交<font color=red>(" + mywtj + "/" + wtj + ")</font>";
            RadioButtonListState.Items.FindByValue("1").Text = "已提交<font color=red>(" + mydsh + "/"+dsh+")</font>";
            RadioButtonListState.Items.FindByValue("2").Text = "待审批<font color=red>(" + mydsp + "/"+dsp+")</font>";

        }

        protected string GetStrCondition()
        {
            string state = RadioButtonListState.SelectedValue.ToString();

            string condition = "";
            //单号条件
            if (TextBoxPTCode.Text != "")
            {
                condition = " PT_CODE LIKE '%" + TextBoxPTCode.Text.Trim() + "%'";
            }
            //生产制号
            if ((TextBoxSCZH.Text != "") && (condition != ""))
            {
                condition += " AND " + " sczh LIKE '%" + TextBoxSCZH.Text.Trim() + "%'";
            }
            else if ((TextBoxSCZH.Text != "") && (condition == ""))
            {
                condition += " sczh LIKE '%" + TextBoxSCZH.Text.Trim() + "%'";
            }
            //项目名称
            if ((TextBoxProjname.Text != "") && (condition != ""))
            {
                condition += " AND " + " pjname LIKE '%" + TextBoxProjname.Text.Trim() + "%'";
            }
            else if ((TextBoxProjname.Text != "") && (condition == ""))
            {
                condition += " pjname LIKE '%" + TextBoxProjname.Text.Trim() + "%'";
            }
            //工程名称
            if ((TextBoxEngname.Text != "") && (condition != ""))
            {
                condition += " AND " + " engname LIKE '%" + TextBoxEngname.Text.Trim() + "%'";
            }
            else if ((TextBoxEngname.Text != "") && (condition == ""))
            {
                condition += " engname LIKE '%" + TextBoxEngname.Text.Trim() + "%'";
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
            //规格型号条件
            if ((TextBoxStandard.Text != "") && (condition != ""))
            {
                condition += " AND " + " Standard LIKE '%" + TextBoxStandard.Text.Trim() + "%'";
            }
            else if ((TextBoxStandard.Text != "") && (condition == ""))
            {
                condition += " Standard LIKE '%" + TextBoxStandard.Text.Trim() + "%'";
            }
            //材质条件
            if ((TextBoxcaizhi.Text != "") && (condition != ""))
            {
                condition += " AND " + " Attribute LIKE '%" + TextBoxcaizhi.Text.Trim() + "%'";
            }
            else if ((TextBoxcaizhi.Text != "") && (condition == ""))
            {
                condition += " Attribute LIKE '%" + TextBoxcaizhi.Text.Trim() + "%'";
            }
            //国标
            if ((TextBoxGB.Text != "") && (condition != ""))
            {
                condition += " AND " + " GB LIKE '%" + TextBoxGB.Text.Trim() + "%'";
            }
            else if ((TextBoxGB.Text != "") && (condition == ""))
            {
                condition += " GB LIKE '%" + TextBoxGB.Text.Trim() + "%'";
            }
            //批号
            if ((TextBoxPihao.Text != "") && (condition != ""))
            {
                condition += " AND " + " LotNumber LIKE '%" + TextBoxPihao.Text.Trim() + "%'";
            }
            else if ((TextBoxPihao.Text != "") && (condition == ""))
            {
                condition += " LotNumber LIKE '%" + TextBoxPihao.Text.Trim() + "%'";
            }
            //从计划跟踪号条件
            if ((TextBoxFromPTC.Text != "") && (condition != ""))
            {
                condition += " AND " + " PTCFrom LIKE '%" + TextBoxFromPTC.Text + "%'";
            }
            else if ((TextBoxFromPTC.Text != "") && (condition == ""))
            {
                condition += " PTCFrom LIKE '%" + TextBoxFromPTC.Text.Trim() + "%'";
            }
            //到计划跟踪号条件
            if ((TextBoxToPTC.Text != "") && (condition != ""))
            {
                condition += " AND " + " PTCTo LIKE '%" + TextBoxToPTC.Text + "%'";
            }
            else if ((TextBoxToPTC.Text != "") && (condition == ""))
            {
                condition += " PTCTo LIKE '%" + TextBoxToPTC.Text.Trim() + "%'";
            }
             //是否定尺
            if ((TextBoxFixed.Text != "") && (condition != ""))
            {
                condition += " AND " + " Fixed LIKE '%" + TextBoxFixed.Text.ToUpper() + "%'";
            }
            else if ((TextBoxFixed.Text != "") && (condition == ""))
            {
                condition += " Fixed LIKE '%" + TextBoxFixed.Text.Trim().ToUpper() + "%'";
            }
            //长
            if ((TextBoxLength.Text != "") && (condition != ""))
            {
                condition += " AND " + " Length LIKE '%" + TextBoxLength.Text + "%'";
            }
            else if ((TextBoxLength.Text != "") && (condition == ""))
            {
                condition += " Length LIKE '%" + TextBoxLength.Text.Trim() + "%'";
            }
            //宽
            if ((TextBoxWidth.Text != "") && (condition != ""))
            {
                condition += " AND " + " Width LIKE '%" + TextBoxLength.Text + "%'";
            }
            else if ((TextBoxWidth.Text != "") && (condition == ""))
            {
                condition += " Width LIKE '%" + TextBoxWidth.Text.Trim() + "%'";
            }
            
            //制单人
            if ((TextBoxDoc.Text != "") && (condition != ""))
            {
                condition += " AND " + " docname LIKE '%" + TextBoxDoc.Text.Trim() + "%'";
            }
            else if ((TextBoxDoc.Text != "") && (condition == ""))
            {
                condition += " docname LIKE '%" + TextBoxDoc.Text.Trim() + "%'";
            }
            
            //制单时间条件
            if ((TextBoxDate.Text != "") && (condition != ""))
            {
                condition += " AND " + " PT_DATE LIKE '%" + TextBoxDate.Text.Trim() + "%'";
            }
            else if ((TextBoxDate.Text != "") && (condition == ""))
            {
                condition += " PT_DATE LIKE '%" + TextBoxDate.Text.Trim() + "%'";
            }
            //技术员审核日期
            if ((TextBoxVerifyDate.Text != "") && (condition != ""))
            {
                condition += " AND " + " PT_VERIFYDATE LIKE '%" + TextBoxVerifyDate.Text.Trim() + "%'";
            }
            else if ((TextBoxVerifyDate.Text != "") && (condition == ""))
            {
                condition += " PT_VERIFYDATE LIKE '%" + TextBoxVerifyDate.Text.Trim() + "%'";
            }

            //审批时间
            if ((TextBoxShenPiDate.Text != "") && (condition != ""))
            {
                condition += " AND " + " PT_MANAGERTIME LIKE '%" + TextBoxShenPiDate.Text.Trim() + "%'";
            }
            else if ((TextBoxShenPiDate.Text != "") && (condition == ""))
            {
                condition += " PT_MANAGERTIME LIKE '%" + TextBoxShenPiDate.Text.Trim() + "%'";
            }
            //仓库条件
            if ((TextBoxWarehouse.Text != "") && (condition != ""))
            {
                condition += " AND " + " Warehouse LIKE '%" + TextBoxWarehouse.Text.Trim() + "%'";
            }
            else if ((TextBoxWarehouse.Text != "") && (condition == ""))
            {
                condition += " Warehouse LIKE '%" + TextBoxWarehouse.Text.Trim() + "%'";
            }
            //仓位条件
            if ((TextBoxLocation.Text != "") && (condition != ""))
            {
                condition += " AND " + " Location LIKE '%" + TextBoxLocation.Text.Trim() + "%'";
            }
            else if ((TextBoxLocation.Text != "") && (condition == ""))
            {
                condition += " Location LIKE '%" + TextBoxLocation.Text.Trim() + "%'";
            }
            //技术员就是审核人
            if ((TextBoxVerifier.Text != "") && (condition != ""))
            {
                condition += " AND " + " verifername LIKE '%" + TextBoxVerifier.Text.Trim() + "%'";
            }
            else if ((TextBoxVerifier.Text != "") && (condition == ""))
            {
                condition += " verifername LIKE '%" + TextBoxVerifier.Text.Trim() + "%'";
            }
            //调整数量
            if ((TextBoxTnum.Text != "") && (condition != ""))
            {
                condition += " AND " + " TNUM LIKE '%" + TextBoxTnum.Text.Trim() + "%'";
            }
            else if ((TextBoxTnum.Text != "") && (condition == ""))
            {
                condition += " TNUM LIKE '%" + TextBoxTnum.Text.Trim() + "%'";
            }
            //调整张支数量
            if ((TextBoxTFnum.Text != "") && (condition != ""))
            {
                condition += " AND " + " TFNUM LIKE '%" + TextBoxTFnum.Text.Trim() + "%'";
            }
            else if ((TextBoxTFnum.Text != "") && (condition == ""))
            {
                condition += " TFNUM LIKE '%" + TextBoxTFnum.Text.Trim() + "%'";
            }
            //审批意见
            if ((TextBoxSuggestion.Text != "") && (condition != ""))
            {
                condition += " AND " + " PT_MANAGERNOTE LIKE '%" + TextBoxSuggestion.Text.Trim() + "%'";
            }
            else if ((TextBoxSuggestion.Text != "") && (condition == ""))
            {
                condition += " PT_MANAGERNOTE LIKE '%" + TextBoxSuggestion.Text.Trim() + "%'";
            }
            //剩余原因
            if ((TextBoxShengYuNote.Text != "") && (condition != ""))
            {
                condition += " AND " + " shengyunote LIKE '%" + TextBoxShengYuNote.Text.Trim() + "%'";
            }
            else if ((TextBoxShengYuNote.Text != "") && (condition == ""))
            {
                condition += " shengyunote LIKE '%" + TextBoxShengYuNote.Text.Trim() + "%'";
            }

            switch (state)
            {
                case "":  
                        
                    break;
                case "0":
                   
                    if (condition != "") { condition += " AND state='0'"; }
                    else { condition += " state='0' "; }
                    break;
                case "1":
                    
                    if (condition != "") { condition += " AND state='1'"; }
                    else { condition += " state='1' "; }
                    break;
                case "2":
                    
                    if (condition != "") { condition += " AND state='2'"; }
                    else { condition += " state='2'"; }
                    break;
                case "3":

                    if (condition != "") { condition += " AND state='3'"; }
                    else { condition += " state='3'"; }
                    break;               
                default: break;
            }

            string subcondition = GetSubCondtion();
            if (condition != "")
            {
                if (subcondition != "")
                {
                    condition += DropDownListFatherLogic.SelectedValue +" ("+ subcondition+")";
                }
               
            }
            else
            {
                if (subcondition != "")
                {
                    condition = subcondition;
                }
            }
            return condition;
        }

        protected void getInfo()
        { 
            string condition = GetStrCondition();
            if (condition == "")
            {
                string TableName = "View_SM_PROJTEMP";
                string PrimaryKey = "PT_CODE";
                string ShowFields = "PT_CODE AS Code,PT_DATE as Date,docname AS Doc,verifername as Verifier,left(PT_VERIFYDATE,10) as VerifyDate,state AS State,uniqueid AS UniqueID,Warehouse AS Warehouse,Location as Location,MaterialCode AS MaterialCode,MaterialName AS MaterialName,Standard AS MaterialStandard,Fixed AS Fixed,Length,Width,Attribute AS Attribute,GB,Unit AS Unit,TNUM AS AJNUM,TFNUM as FAJNUM,LotNumber AS LotNumber,NUM AS Number,FNUM as Quantity,PTCTo,PTCFrom as PTCFrom,LEFT(PT_MANAGERTIME,10) AS ManagerDate,shengyunote,PT_MANAGERNOTE,pjname as Pjname,engname as Engname,manager as Managername";
                string OrderField = "PT_CODE DESC,uniqueid";
                int OrderType = 0;
                string StrWhere = "";
                int PageSize = 30;

                InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);

            }
            else
            {

                string TableName = "View_SM_PROJTEMP";
                string PrimaryKey = "PT_CODE";
                string ShowFields = "PT_CODE AS Code,PT_DATE as Date,docname AS Doc,verifername as Verifier,left(PT_VERIFYDATE,10) as VerifyDate,state AS State,uniqueid AS UniqueID,Warehouse AS Warehouse,Location as Location,MaterialCode AS MaterialCode,MaterialName AS MaterialName,Standard AS MaterialStandard,Fixed AS Fixed,Length,Width,Attribute AS Attribute,GB,Unit AS Unit, TNUM AS AJNUM,TFNUM as FAJNUM,LotNumber AS LotNumber,NUM AS Number,FNUM as Quantity,PTCTo,PTCFrom as PTCFrom,LEFT(PT_MANAGERTIME,10) AS ManagerDate,shengyunote,PT_MANAGERNOTE,pjname as Pjname,engname as Engname,manager as Managername";
                string OrderField = "PT_CODE DESC,uniqueid";
                int OrderType = 0;
                string StrWhere = condition;
                int PageSize = 30;

                InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);

            }

        }

        protected void RadioButtonListState_SelectedIndexChanged(object sender, EventArgs e)
        {            
          
            UCPaging1.CurrentPage = 1;
           
            bindData();
            
        }
        protected void MyTask_CheckedChanged(object sender, EventArgs e)
        {
            if (MyTask.Checked)
            {   
                TextBoxDoc.Text = Session["UserName"].ToString();
            }
            else
            {
                TextBoxDoc.Text = string.Empty;
                
            }
            UCPaging1.CurrentPage = 1;
            getInfo();
            bindData();

        }

        protected string convertState(string state)
        {
            switch (state)
            {
                case "0": return "<font color='Red'>未提交</font>";
                case "1": return "<font color='Red'>已提交</font>";
                case "2": return "<font color='Red'>待审批</font>";
                case "3": return "审批同意";
                default: return state;
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("LabelAJ")).Text.ToString() != "") //调整数量
                {
                    ajnum += Convert.ToDouble(((Label)e.Item.FindControl("LabelAJ")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("LabelFAJ")).Text.ToString() != "") //调整张（支）数
                {
                    ajfnum += Convert.ToInt32(((Label)e.Item.FindControl("LabelFAJ")).Text.ToString());
                }


                if (((Label)e.Item.FindControl("LabelNumber")).Text.ToString() != "") //可调整数量
                {
                    ktn += Convert.ToDouble(((Label)e.Item.FindControl("LabelNumber")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("LabelQuantity")).Text.ToString() != "") //可调整张（支）数
                {
                    ktqn += Convert.ToInt32(((Label)e.Item.FindControl("LabelQuantity")).Text.ToString());
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("labelsumaj")).Text = Math.Round(ajnum, 4).ToString();
                ((Label)e.Item.FindControl("labelsumfaj")).Text = ajfnum.ToString();
                ((Label)e.Item.FindControl("LabelTotalNumber")).Text = Math.Round(ktn, 4).ToString();
                ((Label)e.Item.FindControl("LabelTotalQuantity")).Text = ktqn.ToString();
            }
        }

        //查询
        protected void QueryButton_Click(object sender, EventArgs e)
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
            UCPaging1.CurrentPage = 1;
            bindData();
            UpdatePanelBody.Update();
        }

      
        

        //导出
        //库存=0；入库单=1；结转备库=2；领料单=3； 项目完工=8 ，项目结转=9

        protected void BtnShowExport_Click(object sender, EventArgs e)
        {
            string condition = GetStrCondition().Replace("'", "^");
            List<string> sqllist = new List<string>();
            string sql = "delete from TBWS_EXPORTCONDITION where SessionID='" + Session["UserID"].ToString() + "' AND Type='9'";
            sqllist.Add(sql);
            sql = "insert into TBWS_EXPORTCONDITION (SessionID,Type,StrCondition) VALUES ('" + Session["UserID"].ToString() + "','9','" + condition + "')";
            sqllist.Add(sql);
            DBCallCommon.ExecuteTrans(sqllist);

            ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>projtempexport();</script>");
        }




        //清除条件
        private void clearCondition()
        {

            //审核状态
            foreach (ListItem lt in RadioButtonListState.Items)
            {
                if (lt.Selected)
                {
                    lt.Selected = false;
                }
            }
            RadioButtonListState.Items[0].Selected = true;
            TextBoxCode.Text = string.Empty;
            TextBoxWarehouse.Text = string.Empty;
            TextBoxLocation.Text = string.Empty;
            TextBoxDate.Text = string.Empty;
            TextBoxToPTC.Text = string.Empty;
            TextBoxcaizhi.Text = string.Empty;
            TextBoxName.Text = string.Empty;
            TextBoxStandard.Text = string.Empty;
            TextBoxFromPTC.Text = string.Empty;
            TextBoxTFnum.Text = string.Empty;
            TextBoxTnum.Text = string.Empty;
            TextBoxPTCode.Text = string.Empty;
            TextBoxVerifier.Text = string.Empty;
            TextBoxShengYuNote.Text = string.Empty;
            TextBoxShenPiDate.Text = string.Empty;
            TextBoxSuggestion.Text = string.Empty; //审批意见
            TextBoxWidth.Text = string.Empty;
            TextBoxLength.Text = string.Empty;
            TextBoxProjname.Text = string.Empty;
            TextBoxSCZH.Text = string.Empty;
            TextBoxEngname.Text = string.Empty;
            TextBoxGB.Text = string.Empty;
            TextBoxPihao.Text = string.Empty;
            TextBoxFixed.Text = string.Empty;
            if (MyTask.Checked==false)
            {
                TextBoxDoc.Text = string.Empty;
                TextBoxVerifier.Text = string.Empty;
            }
            
            TextBoxVerifyDate.Text = string.Empty;
            
        }

        private void BindItem()//隐藏单据头 (可以用js实现)
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
                            (Repeater1.Items[j].FindControl("LabelPTCTo") as Label).Style.Add("display", "none");

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


            for (int i = 0; i < 11; i++)
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
            ItemList.Add("PT_CODE", "单号");
            ItemList.Add("pjname", "项目名称");
            ItemList.Add("engname", "工程名称");
            ItemList.Add("MaterialCode", "物料编码");
            ItemList.Add("MaterialName", "物料名称");
            ItemList.Add("Standard", "规格型号");
            ItemList.Add("Attribute", "材质");
            ItemList.Add("Length", "长");
            ItemList.Add("Width", "宽");
            ItemList.Add("GB", "国标");
            ItemList.Add("TNUM", "调整数量");
            ItemList.Add("TFNUM", "调整张(支)数量");
            ItemList.Add("LotNumber", "批号");
            ItemList.Add("docname", "制单人");          
            ItemList.Add("verifername", "审核人");
            ItemList.Add("PTCFrom", "从计划跟踪号");
            ItemList.Add("PTCTo", "到计划跟踪号");
            ItemList.Add("Warehouse", "仓库");
            ItemList.Add("Location", "仓位");
            ItemList.Add("manager", "审批人");
            ItemList.Add("PT_MANAGERTIME", "审批时间");
            ItemList.Add("PT_MANAGERNOTE", "审批意见");
            ItemList.Add("shengyunote", "剩余原因");

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
