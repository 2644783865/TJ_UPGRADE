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
    public partial class SM_Warehouse_ProjOver : System.Web.UI.Page
    {
        
        private double num = 0;//数量
        private double fnum = 0;//辅助数量

        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {

            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);

            if (!IsPostBack)
            {
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
                
                
                bindGV();//绑定条件框
                GetWarehouse();
                GetwarehousePosition();
                getWGInfo(true);


               
            }

            this.Form.DefaultButton = btnQuery.UniqueID;
        }
            //private void BindUpOrDown(string objcode)
            //{
            //    objcode = objcode.Substring(0, objcode.Length - 1);

            //    for (int i = 0; i < objcode.Split('-').Length; i++)
            //    {
            //        GridViewRow gr = GridViewSearch.Rows[i];

            //        DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);
            //        ddl.ClearSelection();
            //        ddl.Items.FindByValue("WG_CODE").Selected = true;

            //        ddl = (gr.FindControl("DropDownListRelation") as DropDownList);
            //        ddl.ClearSelection();
            //        ddl.Items.FindByValue("0").Selected = true;

            //        (gr.FindControl("TextBoxValue") as TextBox).Text = objcode.Split('-')[i];
                    
            //    }

            //    refreshStyle();
            //}


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
            

            string condition = "";

            //生产制号条件
            if (TextBoxtaskid.Text.Trim() != "")
            {
                condition += " SQ_TASKID LIKE '%" + TextBoxtaskid.Text.Trim() + "%'";
            }



            //时间条件

            if ((TextBoxconfirmtime.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " SQ_CONFIRMTIME like '%" + TextBoxconfirmtime.Text.Trim() + "%'";
            }
            else if (TextBoxconfirmtime.Text.Trim() != "" && (condition == ""))
            {
                condition += " SQ_CONFIRMTIME like '%" + TextBoxconfirmtime.Text.Trim() + "%'";
            }
           

            //项目名称
            if ((TextBoxprj.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " SQ_PRJ LIKE '%" + TextBoxprj.Text.Trim() + "%'";
            }
            else if ((TextBoxprj.Text.Trim() != "") && (condition == ""))
            {
                condition += " SQ_PRJ LIKE '%" + TextBoxprj.Text.Trim() + "%'";
            }

            //工程名称
            if ((TextBoxeng.Text != "") && (condition != ""))
            {
                condition += " AND " + " SQ_ENG LIKE'%" + TextBoxeng.Text.Trim() + "%'";
            }
            else if ((TextBoxeng.Text != "") && (condition == ""))
            {
                condition += " SQ_ENG LIKE '%" + TextBoxeng.Text.Trim() + "%'";
            }

            //物料代码条件
            if ((TextBoxmarterialid.Text != "") && (condition != ""))
            {
                condition += " AND " + " SQ_MARID LIKE '%" + TextBoxmarterialid.Text.Trim() + "%'";
            }
            else if ((TextBoxmarterialid.Text != "") && (condition == ""))
            {
                condition += " SQ_MARID LIKE '%" + TextBoxmarterialid.Text.Trim() + "%'";
            }
            //物料名称条件
            if ((TextBoxmarterialname.Text != "") && (condition != ""))
            {
                condition += " AND " + " MNAME LIKE '%" + TextBoxmarterialname.Text.Trim() + "%'";
            }
            else if ((TextBoxmarterialname.Text != "") && (condition == ""))
            {
                condition += " MNAME LIKE '%" + TextBoxmarterialname.Text.Trim() + "%'";
            }
            //规格型号条件
            if ((TextBoxstandard.Text != "") && (condition != ""))
            {
                condition += " AND " + " GUIGE LIKE '%" + TextBoxstandard.Text.Trim() + "%'";
            }
            else if ((TextBoxstandard.Text != "") && (condition == ""))
            {
                condition += " GUIGE LIKE '%" + TextBoxstandard.Text.Trim() + "%'";
            }
            //材质
            if ((TextBoxattribute.Text != "") && (condition != ""))
            {
                condition += " AND " + " CAIZHI LIKE '%" + TextBoxattribute.Text.Trim().ToUpper() + "%'";
            }
            else if ((TextBoxattribute.Text != "") && (condition == ""))
            {
                condition += " CAIZHI LIKE '%" + TextBoxattribute.Text.Trim().ToUpper() + "%'";
            }

            //标识号
            if ((TextBoxcgmode.Text != "") && (condition != ""))
            {
                condition += " AND " + " SQ_CGMODE LIKE '%" + TextBoxcgmode.Text.Trim() + "%'";
            }
            else if ((TextBoxcgmode.Text != "") && (condition == ""))
            {
                condition += " SQ_CGMODE LIKE '%" + TextBoxcgmode.Text.Trim() + "%'";
            }

            //计划跟踪号条件
            if ((TextBoxptc.Text != "") && (condition != ""))
            {
                condition += " AND " + " SQ_PTC LIKE '%" + TextBoxptc.Text.Trim() + "%'";
            }
            else if ((TextBoxptc.Text != "") && (condition == ""))
            {
                condition += " SQ_PTC LIKE '%" + TextBoxptc.Text.Trim() + "%'";
            }
            //计划模式
            if ((TextBoxpmode.Text != "") && (condition != ""))
            {
                condition += " AND " + " SQ_PMODE LIKE '%" + TextBoxpmode.Text.Trim() + "%'";
            }
            else if ((TextBoxpmode.Text != "") && (condition == ""))
            {
                condition += " SQ_PMODE LIKE '%" + TextBoxpmode.Text.Trim() + "%'";
            }

            //仓库条件
            if ((DropDownListwarehouse.SelectedIndex!=0)  && (DropDownListposition.SelectedIndex!=0))
            {
                if (condition != "")
                {
                    condition += " AND " + " Warehouscode='" + DropDownListwarehouse.SelectedValue + "'" + " AND " + " Positioncode='" + DropDownListposition.SelectedValue + "' ";
                }
                else
                {
                    condition += " Warehouscode='" + DropDownListwarehouse.SelectedValue + "'" + " AND " + " Positioncode='" + DropDownListposition.SelectedValue + "' ";
                }
                }
            else if ((DropDownListwarehouse.SelectedIndex != 0) && (DropDownListposition.SelectedIndex == 0))
            {
                if (condition != "")
                {
                    condition += " AND " + " Warehouscode LIKE '" + DropDownListwarehouse.SelectedValue + "%'";
                }
                else
                {
                    condition += " Warehouscode LIKE '" + DropDownListwarehouse.SelectedValue + "%'";
 
                }
            }
            //数量
            if ((TextBoxnum.Text != "") && (condition != ""))
            {
                condition += " AND " + " SQ_NUM LIKE '%" + TextBoxnum.Text.Trim() + "%'";
            }
            else if ((TextBoxnum.Text != "") && (condition == ""))
            {
                condition += " SQ_NUM LIKE '%" + TextBoxnum.Text.Trim() + "%'";
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
            


            string condition = GetStrCondition();

          
            if (condition == "")
            {
                /*
                 * 这里的时间为“收料日期”
                 * 
                 * 在这里涉及到的单位为采购单位，采购数量
                 * 
                 * 辅助单位，辅助数量是没有的
                 */
                string TableName="View_SM_PROJOVER";
                string PrimaryKey = "SQ_CODE";
                string ShowFields = "SQ_CODE,SQ_TASKID,SQ_PRJ,SQ_ENG,SQ_CONFIRMTIME,SQ_PTC AS PTC,Warehouscode,WS_NAME as Warehousename,Positioncode,WL_NAME as Locationname,SQ_LOTNUM as Lotnum,SQ_MARID AS MaterialCode,MNAME AS MaterialName,GUIGE AS MaterialStandard,CAIZHI AS Attribute,GB,SQ_LENGTH as Length,SQ_WIDTH as Width,PURCUNIT AS Unit,SQ_NUM as Num,SQ_FZNUM AS Fznum,SQ_ORDERID as Orderid,SQ_FIXED,SQ_PMODE as Pmode,SQ_NOTE AS Note,SQ_CGMODE as Cgmode ";
                string OrderField = "SQ_CODE DESC,SQ_TASKID";
                int OrderType=0;
                string StrWhere="";
                int PageSize = 50;

                InitVar(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize, isFristPage);
            }
            else
            {

                string TableName = "View_SM_PROJOVER";
                string PrimaryKey = "SQ_CODE";
                string ShowFields = "SQ_CODE,SQ_TASKID,SQ_PRJ,SQ_ENG,SQ_CONFIRMTIME,SQ_PTC AS PTC,Warehouscode,WS_NAME as Warehousename,Positioncode,WL_NAME as Locationname,SQ_LOTNUM as Lotnum,SQ_MARID AS MaterialCode,MNAME AS MaterialName,GUIGE AS MaterialStandard,CAIZHI AS Attribute,GB,SQ_LENGTH as Length,SQ_WIDTH as Width,PURCUNIT AS Unit,SQ_NUM as Num,SQ_FZNUM AS Fznum,SQ_ORDERID as Orderid,SQ_FIXED,SQ_PMODE as Pmode,SQ_NOTE AS Note,SQ_CGMODE as Cgmode ";
                string OrderField = "SQ_CODE DESC,SQ_TASKID";
                int OrderType = 0;
                string StrWhere = condition;
                int PageSize = 50;

               
                InitVar(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize, isFristPage);
            }

          
        }


        




        protected void RepeaterWG_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                if (((Label)e.Item.FindControl("Labelnum")).Text.ToString() != "")
                {

                    num += Convert.ToDouble(((Label)e.Item.FindControl("Labelnum")).Text.ToString());
                }
                if (((Label)e.Item.FindControl("Labelfnum")).Text.ToString() != "")
                {
                    
                    fnum += Convert.ToDouble(((Label)e.Item.FindControl("Labelfnum")).Text.ToString());
                }

            }
            else  if (e.Item.ItemType == ListItemType.Footer)
            {

                ((Label)e.Item.FindControl("LabelSUMnum")).Text = Math.Round(num, 4).ToString();
                ((Label)e.Item.FindControl("Labelsumfnum")).Text = Math.Round(fnum, 2).ToString();

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
        //库存=0；入库单=1；结转备库=2；领料单=3； 项目完工=8

        protected void BtnShowExport_Click(object sender, EventArgs e)
        {
            string condition = GetStrCondition().Replace("'", "^");
            List<string> sqllist = new List<string>();
            string sql = "delete from TBWS_EXPORTCONDITION where SessionID='" + Session["UserID"].ToString() + "' AND Type='8'";
            sqllist.Add(sql);
            sql = "insert into TBWS_EXPORTCONDITION (SessionID,Type,StrCondition) VALUES ('" + Session["UserID"].ToString() + "','8','" + condition + "')";
            sqllist.Add(sql);
            DBCallCommon.ExecuteTrans(sqllist);

            ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>projoverexport();</script>");
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

           

            DropDownListFatherLogic.ClearSelection();
            DropDownListFatherLogic.Items[0].Selected = true;

            DropDownListwarehouse.ClearSelection();
            DropDownListwarehouse.Items[0].Selected = true;

            DropDownListposition.ClearSelection();
            DropDownListposition.Items[0].Selected=true;

            TextBoxattribute.Text = string.Empty;           
            TextBoxcgmode.Text = string.Empty;
            TextBoxconfirmtime.Text = string.Empty;
            TextBoxeng.Text = string.Empty;         
           
            TextBoxgb.Text = string.Empty;
            TextBoxmarterialid.Text = string.Empty;
            TextBoxmarterialname.Text = string.Empty;
            TextBoxnum.Text = string.Empty;
            TextBoxpmode.Text = string.Empty;
            TextBoxprj.Text = string.Empty;
            TextBoxptc.Text = string.Empty;
            TextBoxstandard.Text = string.Empty;
            TextBoxtaskid.Text = string.Empty;
            

        }

        private void BindItem()
        {

            for (int i = 0; i < (RepeaterWG.Items.Count - 1); i++)
            {

                Label lbtaskid = (RepeaterWG.Items[i].FindControl("LabelTaskID") as Label);

                string taskid = lbtaskid.Text;

                if (lbtaskid.Visible)
                {
                    for (int j = i + 1; j < RepeaterWG.Items.Count; j++)
                    {
                        string nexttaskid = (RepeaterWG.Items[j].FindControl("LabelTaskID") as Label).Text;

                        if (nexttaskid == taskid)
                        {
                            (RepeaterWG.Items[j].FindControl("LabelTaskID") as Label).Style.Add("display", "none");
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        protected void GetWarehouse()
        {
            string sqltext = "select distinct ws_id,ws_name from TBWS_WAREHOUSE where ws_fatherid<>'ROOT' order by ws_id";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DropDownListwarehouse.DataSource = dt;
            DropDownListwarehouse.DataTextField="ws_name";
            DropDownListwarehouse.DataValueField = "ws_id";
            DropDownListwarehouse.DataBind();
            ListItem lsitem = new ListItem("--请选择--","0");
            DropDownListwarehouse.Items.Insert(0,lsitem);
        }
        protected void GetwarehousePosition()
        {
            if (DropDownListwarehouse.SelectedIndex != 0)
            { 
                string sqltext = "select distinct wl_id,wl_name from TBWS_LOCATION where wl_wsid ='" + DropDownListwarehouse.SelectedValue + "' order by wl_name";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                DropDownListposition.DataSource = dt;
                DropDownListposition.DataTextField = "wl_name";
                DropDownListposition.DataValueField = "wl_id";
                DropDownListposition.DataBind();
            }
            else
            {
                DropDownListposition.Items.Clear();
             }
            ListItem lsitem = new ListItem("--请选择--", "0");
            DropDownListposition.Items.Insert(0,lsitem);
        }
        protected void DropDownListwarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetwarehousePosition();
        }

        protected void TextBoxtaskid_TextChanged(object sender, EventArgs e)
        {
            string engid = TextBoxtaskid.Text.Trim();

            engid = engid.Split('-')[engid.Split('-').Length - 1];

           
            if (engid!="")
            {
                //TextBoxSCZH.Text = dt.Rows[0]["TSA_PJNAME"] + "-" + dt.Rows[0]["TSA_ENGNAME"] + "-" + dt.Rows[0]["TSA_ID"];
                TextBoxtaskid.Text = engid;
            }
            else
            {
                TextBoxtaskid.Text = string.Empty;
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
            ItemList.Add("SQ_TASKID", "生产制号");
            ItemList.Add("SQ_PTC", "计划跟踪号");
            ItemList.Add("SQ_PRJ", "项目名称");
            ItemList.Add("SQ_ENG", "工程名称");
            ItemList.Add("SQ_MARID", "物料编码");
            ItemList.Add("MNAME", "物料名称");
            ItemList.Add("GUIGE", "规格型号");
            ItemList.Add("CAIZHI", "材质");
            
            ItemList.Add("SQ_ORDERID", "订单编号");

            ItemList.Add("WS_NAME", "仓库");
            ItemList.Add("WL_NAME", "仓位");
            ItemList.Add("SQ_LENGTH", "长");
            ItemList.Add("SQ_WIDTH", "宽");
            ItemList.Add("SQ_LOTNUM", "批号");
            
            ItemList.Add("SQ_FIXED", "是否定尺");
           
            ItemList.Add("SQ_PMODE", "计划模式");
            ItemList.Add("SQ_CGMODE", "标识号");
            ItemList.Add("SQ_NUM", "数量");
            ItemList.Add("SQ_FZNUM", "辅助数量");
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

        #region //无用
        //上查
        //   protected void ButtonSearchUp_Click(object sender, EventArgs e)
        //{
        //    string ptc = string.Empty;

        //    for (int i = 0; i < RepeaterWG.Items.Count; i++)
        //    {
        //        if ((RepeaterWG.Items[i].FindControl("CheckBox1") as CheckBox).Checked)
        //        {

        //            ptc = (RepeaterWG.Items[i].FindControl("LabelPTC") as Label).Text;

        //            break;
 
        //        }
        //    }
        //    if (ptc == string.Empty)
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('未选中相应单据!')</script>");
        //    }
        //    else
        //    {
        //        ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>SearchUp('" + ptc + "');</script>");
        //    }
        //}
        #endregion

        #region  //无用
//        //下查
//        //protected void ButtonSearchDown_Click(object sender, EventArgs e)
//        {
//            string Code = string.Empty;

//            string ObjCode = string.Empty;

//            for (int i = 0; i < RepeaterWG.Items.Count; i++)
//            {
//                if ((RepeaterWG.Items[i].FindControl("CheckBox1") as CheckBox).Checked)
//                {

//                    Code = (RepeaterWG.Items[i].FindControl("LabelCode") as Label).Text;
//                    break;

//                }
//            }

//            if(Code==string.Empty)
//            {
//                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('未选中相应单据!')</script>");
//                return;
//            }

//            string sql = "select GJ_INVOICEID from TBFM_GJRELATION where GI_INSTOREID='" + Code + "'";
//            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

//            if (dt.Rows.Count > 0)
//            {
//                ObjCode = dt.Rows[0]["GJ_INVOICEID"].ToString();
//                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>SearchDown('" + ObjCode + "');</script>");
//            }
//            else
//            {
//                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('无关联单据信息!')</script>");
//            }

//        }
#endregion

    }

        
    
}
