using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_SaleCostSta : System.Web.UI.Page
    {
        PagerQueryParamGroupBy pager = new PagerQueryParamGroupBy();
        static String CurType;
        static String Condition = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                //bindDdlYear();
                bindGV();
                //CheckUser(ControlFinder);
            }            
        }

        protected string getYear()
        {
            string ymdhms = System.DateTime.Now.ToString("yyyyMMdd");
            return ymdhms.Substring(0, 4);
        }

        protected string getMonth()
        {
            string ymdhms = System.DateTime.Now.ToString("yyyyMMdd");
            return ymdhms.Substring(4, 2);
        }

        //绑定前五年
        //#region
        //private void bindDdlYear()
        //{
        //    Dictionary<int, string> dt = new Dictionary<int, string>();
        //    int year = Convert.ToInt32(getYear());
        //    for (int i = 0; i < 5; i++)
        //    {
        //        dt.Add(year - i, "-" + (year - i).ToString() + "-");
        //    }
        //    ddl_year.DataSource = dt;
        //    ddl_year.DataTextField = "value";
        //    ddl_year.DataValueField = "key";
        //    ddl_year.DataBind();
        //    foreach (ListItem lt in ddl_year.Items)
        //    {
        //        if (lt.Value == getYear())
        //        {
        //            lt.Selected = true;
        //        }
        //    }
        //    foreach (ListItem lt in ddl_month.Items)
        //    {
        //        if (lt.Value == getMonth())
        //        {
        //            lt.Selected = true;
        //        }
        //    }
        //}
        //#endregion

        //物料，三级

        private Dictionary<string, string> bindThreeList()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();

            ItemList.Add("NO", "");
            ItemList.Add("MATERIALCODE", "物料编码");
            ItemList.Add("MATERIALNAME", "物料名称");
            ItemList.Add("STANDARD", "规格型号");
            ItemList.Add("ATTRIBUTE", "材质");
            ItemList.Add("PTC", "计划跟踪号");

            return ItemList;
        }
        //小类，二级
        private Dictionary<string, string> bindTwoList()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();
            ItemList.Add("NO", "");
            ItemList.Add("TY_NAME", "物料类别");
            ItemList.Add("PTC", "计划跟踪号");

            return ItemList;
        }

        //大类，一级，
        private Dictionary<string, string> bindOneList()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();
            ItemList.Add("NO", "");
            ItemList.Add("substring(TYPEID,1,2)", "物料类别");
            ItemList.Add("PTC", "计划跟踪号");


            return ItemList;
        }

        protected void GridViewSearch_DataBound(object sender, EventArgs e)
        {
           

            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                

                if (ddlType.SelectedValue == "03")
                {
                    DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                    ddl.DataTextField = "value";
                    ddl.DataValueField = "key";
                    ddl.DataSource = bindThreeList();
                    ddl.DataBind();
                }
                else
                {
                    DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);
                    ddl.DataTextField = "value";
                    ddl.DataValueField = "key";


                    DropDownList ddlTypeValue = (gr.FindControl("ddlTypeValue") as DropDownList);
                    if (ddlType.SelectedValue == "02")
                    {
                        ddl.DataSource = bindTwoList();
                        ddl.DataBind();
                        //string sql = "select ty_name,TY_ID  from TBMA_typeinfo where ty_fatherid !='ROOT'";
                        //DBCallCommon.FillDroplist(ddlTypeValue, sql);
                    }
                    else
                    {
                        ddl.DataSource = bindOneList();
                        ddl.DataBind();
                        //ddlTypeValue.Items.Add(new ListItem("-请选择-", "-请选择-"));
                        //ddlTypeValue.Items.Add(new ListItem("原材料", "01"));
                        //ddlTypeValue.Items.Add(new ListItem("低值易耗品", "02"));
                    }
                }

                if (gr.RowIndex == 4)
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
                if (ddlType.SelectedValue != "01")
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
                else 
                {
                    if (gr.RowIndex == 0)
                    {

                        DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                        if (ddl.SelectedValue != "NO")
                        {
                            //DropDownList ddlValue = (gr.FindControl("ddlTypeValue") as DropDownList);
                            TextBox txtValue = (gr.FindControl("TextBoxValue") as TextBox);
                            string text = "";
                            if (txtValue.Text.Trim() != "")
                            {
                                if ("原材料".Contains(txtValue.Text.Trim()))
                                {
                                    text = "01";
                                }
                                else if ("低值易耗品".Contains(txtValue.Text.Trim()))
                                {
                                    text = "02";
                                }
                                else
                                {
                                    text = "05";//不存在该物料类别
                                }

                            }

                            DropDownList ddlRel = (gr.FindControl("DropDownListRelation") as DropDownList);

                            subCondition = ConvertRelation(ddl.SelectedValue, ddlRel.SelectedValue, text);
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

                            //DropDownList ddlValue = (gr.FindControl("ddlTypeValue") as DropDownList);

                            TextBox txtValue = (gr.FindControl("TextBoxValue") as TextBox);
                            string text = "";
                            if(txtValue.Text.Trim()!="")
                            {
                                if ("原材料".Contains(txtValue.Text.Trim()))
                                {
                                    text="01";
                                }
                                else if ("低值易耗品".Contains(txtValue.Text.Trim()))
                                {
                                    text = "02";
                                }
                                else
                                {
                                    text = "05";//不存在该物料类别
                                }

                            }


                            DropDownList ddlRel = (gr.FindControl("DropDownListRelation") as DropDownList);

                            subCondition += " " + ddlLogic.SelectedValue + " " + ConvertRelation(ddl.SelectedValue, ddlRel.SelectedValue, text);
                        }

                        else
                        {
                            break;
                        }
                    }
                }

            }
            return subCondition;
        }


        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGV();

            #region

            //foreach (GridViewRow gr in GridViewSearch.Rows)
            //{
            //    TextBox tb = (gr.FindControl("TextBoxValue") as TextBox);
            //    DropDownList ddl = (gr.FindControl("ddlTypeValue") as DropDownList);
            //    if (ddlType.SelectedValue == "03")
            //    {
            //        //tb.Visible = true;
            //        //ddl.Visible = false;
            //        tb.Style.Add("display", "block");
            //        ddl.Style.Add("display", "none");
            //    }
            //    else
            //    {
            //        //tb.Visible = false;
            //        //ddl.Visible = true;
            //        tb.Style.Add("display", "none");
            //        ddl.Style.Add("display", "block");
            //    }
            //}
            #endregion
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            //resetSubcondition();
            ModalPopupExtenderSearch.Hide();
            UpdatePanelBody.Update();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            //resetSubcondition();
            refreshStyle();
            UpdatePanelBody.Update();
        }

        //查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {

            getData();

            //refreshStyle();
            ModalPopupExtenderSearch.Hide();
            UpdatePanelBody.Update();
        }

        private void getData()
        {
            string jibie = ddlType.SelectedValue;

            string condition = "";

            string startime = string.Empty;
            string endtime = string.Empty;
            startime = txtStartTime.Text.Trim();
            endtime = txtEndTime.Text.Trim();
            if (rbl1.SelectedValue == "month")
            {
                condition = "Date between '" + startime + "' and '" + endtime + "'";
            }
            else if (rbl1.SelectedValue == "year")
            {
                condition = "Substring(Date,1,4)='" + txtStartTime.Text.Split('-')[0] + "'";
            }
            string SubCondtion = GetSubCondtion();

            if (condition != "")
            {
                if (SubCondtion != "")
                {
                    condition += " AND (" + SubCondtion + " ) ";
                }
            }
            else
            {
                if (SubCondtion != "")
                {
                    condition += SubCondtion;
                }
            }
            
            if (rbl1.SelectedValue == "year")
            {

                CurType = "year";
                BindYearData(jibie, condition);
                
            }
            else //按月
            {
                CurType = "month";
                BindMonthData(jibie, condition);
                
            }
        }

        //按年绑定数据
        private void BindYearData(string jibie, string condition)
        {
                      
            if (jibie == "03")
            {
                string TableName = "VIEW_SM_OUTXS";
                string PrimaryKey = "";
                string ShowFields = "Company,PTC, Substring(Date,1,4) as SLYEAR,''as SLMONTH, MATERIALCODE  AS MTID ,MATERIALNAME AS MTNAME,sum(RealNumber) as NUMBER,sum(Amount) AS AMOUNT ";
                string OrderField = "MATERIALCODE";
                int OrderType = 0;
                string StrWhere = condition;
                int PageSize = 100;
                string GroupField = "Company,MaterialCode,MaterialName,PTC,Substring(Date,1,4)";

                Condition = " WHERE " + condition + " group by MaterialCode,MaterialName,Company,PTC,Substring(Date,1,4) order by MATERIALCODE ASC";
                
                //CurRepeater = rpt_yearMar;
                InitVar(TableName, PrimaryKey, ShowFields, OrderField, StrWhere,GroupField, OrderType, PageSize);
            }
            else
            {
                if (jibie == "02")
                {
                    string TableName = "VIEW_SM_OUTXS";
                    string PrimaryKey = "";
                    string ShowFields = "Company,PTC, Substring(Date,1,4) as SLYEAR, ''as SLMONTH,TYPEID AS MTID ,TY_NAME AS MTNAME,sum(RealNumber) as NUMBER,sum(Amount) AS AMOUNT ";
                    string OrderField = "TYPEID";
                    int OrderType = 0;
                    string StrWhere = condition;
                    string GroupField = "Company,TYPEID,TY_NAME,PTC,Substring(Date,1,4)";
                    int PageSize = 50;

                    Condition = " WHERE " + condition + " group by TYPEID,TY_NAME,PTC,Substring(Date,1,4), Company order by TYPEID ASC";
                    
                    InitVar(TableName, PrimaryKey, ShowFields, OrderField, StrWhere, GroupField, OrderType, PageSize);

                }
                if (jibie == "01")
                {
                    string TableName = "VIEW_SM_OUTXS";
                    string PrimaryKey = "";
                    string ShowFields = "Company,PTC, Substring(Date,1,4) as SLYEAR,''as SLMONTH, substring(TYPEID,1,2)AS MTID,(case WHEN substring(TYPEID,1,2)='01' THEN '原材料' when substring(TYPEID,1,2)='01' THEN '低值易耗品' else null end) as MTNAME,sum(RealNumber) as NUMBER,sum(Amount) AS AMOUNT ";
                    string OrderField = "substring(TYPEID,1,2)";
                    int OrderType = 0;
                    string StrWhere = condition;
                    string GroupField = "Company,substring(TYPEID,1,2),PTC,Substring(Date,1,4)";
                    int PageSize = 50;

                    Condition = " WHERE " + condition + " group by substring(TYPEID,1,2),Company,PTC,Substring(Date,1,4) order by substring(TYPEID,1,2)";
                    
                    InitVar(TableName, PrimaryKey, ShowFields, OrderField, StrWhere, GroupField, OrderType, PageSize);

                }               
                
            }
        }


        //按月绑定数据
        private void BindMonthData(string jibie, string condition)
        {
           
            if (jibie == "03")
            {
                string TableName = "VIEW_SM_OUTXS";
                string PrimaryKey = "";
                string ShowFields = "Company,PTC, Substring(Date,1,4) as SLYEAR,Substring(Date,6,2) as SLMONTH, MATERIALCODE AS MTID ,MATERIALNAME AS MTNAME,sum(RealNumber) as NUMBER,sum(Amount) AS AMOUNT ";
                string OrderField = "MATERIALCODE";
                int OrderType = 0;
                string StrWhere = condition;
                string GroupField = "Company,MaterialCode,MaterialName,PTC,Substring(Date,1,4),Substring(Date,6,2)";
                int PageSize = 50;

                Condition = " WHERE " + condition + " group by Company,MaterialCode,MaterialName,PTC,Substring(Date,1,4),Substring(Date,6,2) order by MATERIALCODE ASC";
                
                InitVar(TableName, PrimaryKey, ShowFields, OrderField, StrWhere,GroupField, OrderType, PageSize);

            }
            else
            {
                if (jibie == "02")
                {
                    string TableName = "VIEW_SM_OUTXS";
                    string PrimaryKey = "";
                    string ShowFields = "Company,PTC, Substring(Date,1,4) as SLYEAR, Substring(Date,6,2) as SLMONTH,TYPEID AS MTID ,TY_NAME AS MTNAME,sum(RealNumber) as NUMBER,sum(Amount) AS AMOUNT ";
                    string OrderField = "TYPEID";
                    int OrderType = 0;
                    string StrWhere = condition;
                    string GroupField = "Company,TYPEID,TY_NAME,PTC,Substring(Date,1,4),Substring(Date,6,2)";
                    int PageSize = 50;

                    Condition = " WHERE " + condition + " group by Company,TYPEID,TY_NAME,PTC,Substring(Date,1,4),Substring(Date,6,2) order by TYPEID ASC";
                    InitVar(TableName, PrimaryKey, ShowFields, OrderField, StrWhere, GroupField, OrderType, PageSize);

                }
                if (jibie == "01")
                {
                    string TableName = "VIEW_SM_OUTXS";
                    string PrimaryKey = "";
                    string ShowFields = "Company,PTC, Substring(Date,1,4) as SLYEAR,Substring(Date,6,2) as SLMONTH, substring(TYPEID,1,2)AS MTID,(case WHEN substring(TYPEID,1,2)='01' THEN '原材料' when substring(TYPEID,1,2)='01' THEN '低值易耗品' else null end) as MTNAME,sum(RealNumber) as NUMBER,sum(Amount) AS AMOUNT ";
                    string OrderField = "substring(TYPEID,1,2)";
                    int OrderType = 0;
                    string StrWhere = condition;
                    string GroupField = "Company,substring(TYPEID,1,2),PTC,Substring(Date,1,4),Substring(Date,6,2)";
                    int PageSize = 50;

                    Condition = " WHERE " + condition + " group by Company,substring(TYPEID,1,2),PTC,Substring(Date,1,4),Substring(Date,6,2) order by substring(TYPEID,1,2) ASC";
                    
                    InitVar(TableName, PrimaryKey, ShowFields, OrderField, StrWhere, GroupField, OrderType, PageSize);

                }                
            }
            //CheckUser(ControlFinder);
        }

        //导出
        protected void btn_export_Click(object sender, EventArgs e)
        {
            //rptProductNumStc.DataSource = null;
            //rptProductNumStc.DataBind();
            //this.ddlchaxun_SelectedIndexChanged(null, null);//防止长时间不导出而他人操作数据得表TBFM_PRIDSTATISTICS中数据改变，故重新执行一遍
            string jishu = ddlType.SelectedValue;             
            ExportDataFromDB.ExportSaleCostSta(jishu,CurType,Condition);
        }

        //刷新条件选择页面
        private void refreshStyle()
        {
            ddlType.SelectedValue = "03";
            rbl1.SelectedValue = "month";
            //txtStartTime.Text.Split('-')[1] = DateTime.Now.ToString("yyyymmdd").Substring(4, 2);
            //txtStartTime.Text.Split('-')[0] = DateTime.Now.ToString("yyyymmdd").Substring(0, 4);
            bindGV();
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
                    (gr.FindControl("TextBoxValue") as TextBox).Style.Add("display", "block");
                    (gr.FindControl("ddlTypeValue") as DropDownList).Style.Add("display", "none");
                }
                else
                {
                    (gr.FindControl("DropDownListLogic") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_logic") as TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_name") as TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_relation") as TextBox).Style.Add("display", "block");
                    (gr.FindControl("TextBoxValue") as TextBox).Style.Add("display", "block");
                    (gr.FindControl("ddlTypeValue") as DropDownList).Style.Add("display", "none");
                }
            }
        }
        //重置条件选择页面
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

        private void bindGV()
        {
            GridViewSearch.DataSource = CreateTable();

            GridViewSearch.DataBind();
        }
        private DataTable CreateTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("index", typeof(int)));


            for (int i = 0; i < 5; i++)
            {
                DataRow row = dt.NewRow();
                row["index"] = i;
                dt.Rows.Add(row);
            }

            return dt;
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
                default:
                    {
                        break;
                    }
            }

            return obj;
        }

        public string getFlag()
        {
            if (CurType.Equals(String.Empty))
            {
                return "";
            }
            return CurType;
        }

        #region  分页  UCPaging

        /// <summary>
        /// 初始化分布信息
        /// </summary>

        private void InitVar(string _tablename, string _primarykey, string _showfields, string _orderfield, string _strwhere, string _groupfield,int _ordertype, int _pagesize)
        {
            InitPager(_tablename, _primarykey, _showfields, _orderfield, _strwhere,_groupfield, _ordertype, _pagesize);
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize; //每页显示的记录数
            bindGrid(CurType);
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager(string _tablename, string _primarykey, string _showfields, string _orderfield, string _strwhere, string _groupfield,int _ordertype, int _pagesize)
        {
            //this.GetListName();
            pager.TableName = _tablename;
            pager.PrimaryKey = _primarykey;
            pager.ShowFields = _showfields;
            pager.OrderField = _orderfield; //"dbo.f_formatstr(" + ddlSort.SelectedValue.ToString() + ", '.')";
            pager.StrWhere = _strwhere; // ViewState["sqlText"].ToString();
            pager.OrderType = _ordertype; //升序排列            
            pager.PageSize = _pagesize;
            pager.GroupField = _groupfield;
        }

        void Pager_PageChanged(int pageNumber)
        {
            //Control[] CRL = this.BindGridParamsRecord(ViewState["CurrentUCPaging"].ToString());
            //bindGrid((UCPaging)CRL[0], (GridView)CRL[1], (Panel)CRL[2]);
            getData();
        }

        private void bindGrid(string ym)
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamGroupBy(pager);
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
            GridView1.Visible = true;
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            if (ym == "year")
            {
                GridView1.Columns[3].Visible = false;
            }
            else if (ym == "month")
            {
                GridView1.Columns[3].Visible = true;
            }

          
        }
        #endregion


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)//获取总列数
            {
                //如果是数据行则添加title
                if (e.Row.RowType == DataControlRowType.DataRow)
                {//设置title为gridview的head的text
                    e.Row.Attributes["style"] = "Cursor:hand";
                    e.Row.Cells[i].Attributes.Add("title", "双击可以查看销售出库单的详细信息");
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标经过时，行背景色变 
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#EEE8AA'");
                //鼠标移出时，行背景色变 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");

                string url = e.Row.Cells[1].Text.Trim();
                string starttime = e.Row.Cells[2].Text.Trim() + "-" + e.Row.Cells[3].Text.Trim() + "-" + "01";

                int month1 = Convert.ToInt32(e.Row.Cells[3].Text.Trim()) + 1;

                string endtime = e.Row.Cells[2].Text.Trim() + "-" +"0"+month1.ToString()+ "-" + "01";

                ////e.Row.Attributes.Add("onclick", "return openLink('" + url + "')");
                e.Row.Attributes["onclick"] = String.Format("javascript:setTimeout(\"if(dbl_click){{dbl_click=false;}}else{{{0}}};\", 100000);", ClientScript.GetPostBackEventReference(GridView1, "Select$" + e.Row.RowIndex.ToString(), true));
                // 双击，设置 dbl_click=true，以取消单击响应
                e.Row.Attributes["ondblclick"] = String.Format("javascript:dbl_click=true;return show('" + url + "','" + starttime + "','" + endtime + "');");

            }
        }
    }
}
