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
using Microsoft.Office.Interop.Excel;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_ZanGuMingXiChaXu : System.Web.UI.Page
    {
       
        PagerQueryParam pager = new PagerQueryParam();
        Repeater CurRepeater;
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);

            if (!IsPostBack)
            {
                //bindDdlYear(); 
                GetGata();

                bindGrid(CurRepeater);
                bindGV();
            }

        }
        #region
        //绑定前五年
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
        #endregion

        private void GetGata()
        {
            string jibie = ddlType.SelectedValue;
            string condition = "";
            string StrTime;
            string EndTime;

            txtStartTime.Text = System.DateTime.Now.ToString("yyyy-MM");
            txtEndTime.Text = System.DateTime.Now.ToString("yyyy-MM");
            StrTime = txtStartTime.Text.Trim();
            EndTime = txtEndTime.Text.Trim();

            string SubCondtion = GetSubCondtion();

            condition = "CONVERT(varchar(100),(SI_YEAR+'-'+SI_PERIOD), 23)  between '" + StrTime + "' and '" + EndTime + "'";
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

            BindMonthData(jibie, condition);
        }
        
        //物料，三级
        private Dictionary<string, string> bindThreeList()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();

            ItemList.Add("NO", "");
            ItemList.Add("SI_MARID", "物料编码");
            ItemList.Add("MNAME", "物料名称");
            return ItemList;
        }
        //小类，二级
        private Dictionary<string, string> bindTwoList()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();
            ItemList.Add("NO", "");
            ItemList.Add("substring(SI_MARID,1,5)", "类别编码");

            return ItemList;
        }

        //大类，一级，
        private Dictionary<string, string> bindOneList()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();
            ItemList.Add("NO", "");
            ItemList.Add("substring(SI_MARID,1,2)", "类别编码");

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
                        string sql = "select ty_name,TY_ID  from TBMA_typeinfo where ty_fatherid !='ROOT'";
                        DBCallCommon.FillDroplist(ddlTypeValue, sql);
                    }
                    else
                    {
                        ddl.DataSource = bindOneList();
                        ddl.DataBind();
                        ddlTypeValue.Items.Add(new ListItem("-请选择-", "-请选择-"));
                        ddlTypeValue.Items.Add(new ListItem("原材料", "01"));
                        ddlTypeValue.Items.Add(new ListItem("低值易耗品", "02"));
                    }
                }
                if (gr.RowIndex == 4)
                {
                    (gr.FindControl("tb_logic") as System.Web.UI.WebControls.TextBox).Visible = false;
                }
            }
        }

        //刷新条件选择页面
        private void refreshStyle()
        {
            ddlType.SelectedValue = "03";
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
                        (gr.FindControl("tb_logic") as System.Web.UI.WebControls.TextBox).Style.Add("display", "none");
                    }

                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "block");
                    (gr.FindControl("tb_name") as System.Web.UI.WebControls.TextBox).Style.Add("display", "none");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "block");
                    (gr.FindControl("tb_relation") as System.Web.UI.WebControls.TextBox).Style.Add("display", "none");
                    (gr.FindControl("System.Web.UI.WebControls.TextBoxValue") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                    (gr.FindControl("ddlTypeValue") as DropDownList).Style.Add("display", "none");
                }
                else
                {
                    (gr.FindControl("DropDownListLogic") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_logic") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_name") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_relation") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                    (gr.FindControl("System.Web.UI.WebControls.TextBoxValue") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
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
                (gr.FindControl("System.Web.UI.WebControls.TextBoxValue") as System.Web.UI.WebControls.TextBox).Text = string.Empty; ;
            }

            refreshStyle();
        }

        protected void Query_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            getData();
            ModalPopupExtenderSearch.Hide();
            UpdatePanelBody.Update();
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
        private void getData()
        {
            string jibie = ddlType.SelectedValue;
            //string marid = string.Empty;
            string condition = "";
            string startime = string.Empty;
            string endtime = string.Empty;
            startime = txtStartTime.Text.Trim();
            endtime = txtEndTime.Text.Trim();

            //marid = txtmarid.Text.Trim();
            string SubCondtion = GetSubCondtion();

            condition = "CONVERT(varchar(100),(SI_YEAR+'-'+SI_PERIOD), 23)  between '" + startime + "' and '" + endtime + "'";
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
            
            BindMonthData(jibie, condition);
        }
  
        private void BindMonthData(string jibie, string condition)
        {
            string sqltext = "";

            GetTotalAmount(condition);

            if (jibie == "03")
            {              
                string TableName = "View_ZGBAL";
                string PrimaryKey = "";
                string ShowFields = "SI_MARID,MNAME,cast(SI_BEGNUM as float) as SI_BEGNUM,cast(SI_BEGBAL as float) as SI_BEGBAL,cast(SI_ENDNUM as float) as SI_ENDNUM,cast(SI_ENDBAL as float) as SI_ENDBAL,cast(SI_CRCVNUM as float) as SI_CRCVNUM,cast(SI_CRCVMNY as float) as SI_CRCVMNY, cast(SI_GJNUM as float) as SI_GJNUM,cast(SI_GJMNY as float) as SI_GJMNY,SI_YEAR,SI_PERIOD";
                string OrderField = "CONVERT(varchar(100), (SI_YEAR+'-'+SI_PERIOD), 23),SI_MARID";
                int OrderType = 0;
                string StrWhere = condition;
                int PageSize = 50;
                CurRepeater = rpt_monthMar;

                InitVar(TableName, PrimaryKey, ShowFields, OrderField, StrWhere, OrderType, PageSize);

            }
            else
            {
                if (jibie == "02")
                {
                    sqltext = "select substring(SI_MARID,1,5) as SI_MARID,TY_NAME,SI_YEAR,SI_PERIOD,cast(round(sum(SI_BEGNUM),4) as float) as SI_BEGNUM,";
                    sqltext += "cast(round(sum(SI_BEGBAL),2) as float) as SI_BEGBAL,cast(round(sum(SI_ENDNUM),4) as float) as SI_ENDNUM,";
                    sqltext += "cast(round(sum(SI_ENDBAL),2) as float) as SI_ENDBAL,cast(round(sum(SI_CRCVNUM),4) as float) as SI_CRCVNUM,";
                    sqltext += "cast(round(sum(SI_CRCVMNY),2) as float) as SI_CRCVMNY, cast(round(sum(SI_GJNUM),4) as float) as SI_GJNUM,";
                    sqltext += " cast(round(sum(SI_GJMNY),2) as float) as SI_GJMNY from View_ZGBAL ";
                    sqltext += " where " + condition + " group by substring(SI_MARID,1,5),TY_NAME,SI_YEAR,SI_PERIOD order by CONVERT(varchar(100), (SI_YEAR+'-'+SI_PERIOD), 23),substring(SI_MARID,1,5)";
                }
                if (jibie == "01")
                {
                    sqltext = "select substring(SI_MARID,1,2) as SI_MARID,(case when substring(SI_MARID,1,2)='01' then  '原材料'  when substring(SI_MARID,1,2)='02' then '低值易耗品' else  null end) as TY_NAME, ";
                    sqltext += " cast(round(sum(SI_BEGNUM),4) as float) as SI_BEGNUM,SI_YEAR,SI_PERIOD,";
                    sqltext += "cast(round(sum(SI_BEGBAL),2) as float) as SI_BEGBAL,cast(round(sum(SI_ENDNUM),4) as float) as SI_ENDNUM,";
                    sqltext += "cast(round(sum(SI_ENDBAL),2)as float) as SI_ENDBAL,cast(round(sum(SI_CRCVNUM),4) as float) as SI_CRCVNUM,";
                    sqltext += "cast(round(sum(SI_CRCVMNY),2)as float) as SI_CRCVMNY,cast(round(sum(SI_GJNUM),4) as float) as SI_GJNUM,";
                    sqltext += " cast (round(sum(SI_GJMNY),2)as float) as SI_GJMNY from  View_ZGBAL ";
                    sqltext += " where " + condition + " group by substring(SI_MARID,1,2),SI_YEAR,SI_PERIOD order by CONVERT(varchar(100), (SI_YEAR+'-'+SI_PERIOD), 23),substring(SI_MARID,1,2) ";
                }
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

                if (dt.Rows.Count > 0)
                {
                    rpt_monthType.DataSource = dt;
                    rpt_monthType.DataBind();
                    rpt_monthType.Visible = true;
 
                    rpt_monthMar.DataSource = null;
                    rpt_monthMar.DataBind();
                    rpt_monthMar.Visible = false;

                    NoDataPanel.Visible = false;
                    //btn_export.Visible = true;
                    UCPaging1.Visible = false;
                }
                else
                {
                    rpt_monthType.DataSource = null;
                    rpt_monthType.DataBind();
                    //rpt_monthType.Visible = false;

                    rpt_monthMar.DataSource = null;
                    rpt_monthMar.DataBind();
                    rpt_monthMar.Visible = false;

                    NoDataPanel.Visible = true;
                    //btn_export.Visible = false;
                    UCPaging1.Visible = false;
                }

            }
        }



        #region  分页  UCPaging

        private void InitVar(string _tablename, string _primarykey, string _showfields, string _orderfield, string _strwhere, int _ordertype, int _pagesize)
        {
            InitPager(_tablename, _primarykey, _showfields, _orderfield, _strwhere, _ordertype, _pagesize);
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize; //每页显示的记录数
            bindGrid(CurRepeater);
        }

        private void InitPager(string _tablename, string _primarykey, string _showfields, string _orderfield, string _strwhere, int _ordertype, int _pagesize)
        {
            pager.TableName = _tablename;
            pager.PrimaryKey = _primarykey;
            pager.ShowFields = _showfields;
            pager.OrderField = _orderfield;
            pager.StrWhere = _strwhere; 
            pager.OrderType = _ordertype;
            pager.PageSize = _pagesize;
        }

        void Pager_PageChanged(int pageNumber)
        {
            getData();
            bindGrid(CurRepeater);
        }

        private void bindGrid(Repeater ParamRepeater)
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, ParamRepeater, UCPaging1, NoDataPanel);
            ParamRepeater.Visible = true;
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }

            if (ParamRepeater.Equals(rpt_monthMar))
            {      
                rpt_monthType.DataSource = null;
                rpt_monthType.DataBind();
                rpt_monthType.Visible = false;
                
            }
            else
            {
                rpt_monthMar.DataSource = null;
                rpt_monthMar.DataBind();
                rpt_monthMar.Visible = false;
      
            }
        }

      
        #endregion

        private void bindGV()
        {
            GridViewSearch.DataSource = CreateTable();

            GridViewSearch.DataBind();
        }
        private System.Data.DataTable CreateTable()
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            dt.Columns.Add(new DataColumn("index", typeof(int)));


            for (int i = 0; i < 5; i++)
            {
                DataRow row = dt.NewRow();
                row["index"] = i;
                dt.Rows.Add(row);
            }

            return dt;
        }
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGV();
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                System.Web.UI.WebControls.TextBox tb = (gr.FindControl("TextBoxValue") as System.Web.UI.WebControls.TextBox);
                DropDownList ddl = (gr.FindControl("ddlTypeValue") as DropDownList);
                if (ddlType.SelectedValue == "03")
                {
                    //tb.Visible = true;
                    //ddl.Visible = false;
                    tb.Style.Add("display", "block");
                    ddl.Style.Add("display", "none");
                }
                else
                {
                    //tb.Visible = false;
                    //ddl.Visible = true;
                    tb.Style.Add("display", "none");
                    ddl.Style.Add("display", "block");
                }
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
       

        private void GetTotalAmount(string strWhere)
        {
            string sql = "select isnull(CAST(sum(SI_BEGBAL) AS FLOAT),0) as TotalBEGBAL,isnull(CAST(sum(SI_CRCVMNY) AS FLOAT),0) as TotalCRCVMNY, isnull(CAST(sum(SI_GJMNY) AS FLOAT),0) as TotalGJMNY,isnull(CAST(sum(SI_ENDBAL) AS FLOAT),0) as TotalENDBAL from View_ZGBAL where " + strWhere;

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.Read())
            {

                hfdBEGBAL.Value = string.Format("{0:c2}", Convert.ToDouble(sdr["TotalBEGBAL"]));
                hfdENDBAL.Value = string.Format("{0:c2}", Convert.ToDouble(sdr["TotalENDBAL"]));
                hfdCRCVMNY.Value = string.Format("{0:c2}", Convert.ToDouble(sdr["TotalCRCVMNY"]));
                hfdGJMNY.Value = string.Format("{0:c2}", Convert.ToDouble(sdr["TotalGJMNY"]));
            }
            sdr.Close();

        }

        #region 汇总

        protected void rpt_monthMar_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Footer)
            {

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelBEGBAL")).Text = hfdBEGBAL.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelCRCVMNY")).Text = hfdCRCVMNY.Value;

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelGJMNY")).Text = hfdGJMNY.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelENDBAL")).Text = hfdENDBAL.Value;
            }
        }
        
        protected void rpt_monthType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Footer)
            {

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelBEGBAL")).Text = hfdBEGBAL.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelCRCVMNY")).Text = hfdCRCVMNY.Value;

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelGJMNY")).Text = hfdGJMNY.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelENDBAL")).Text = hfdENDBAL.Value;
            }
        }

        protected string GetSubCondtion()
        {
            string subCondition = "";

            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                if (ddlType.SelectedValue == "03")
                {
                    if (gr.RowIndex == 0)
                    {

                        DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                        if (ddl.SelectedValue != "NO")
                        {
                            System.Web.UI.WebControls.TextBox txtValue = (gr.FindControl("TextBoxValue") as System.Web.UI.WebControls.TextBox);

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

                            System.Web.UI.WebControls.TextBox txtValue = (gr.FindControl("TextBoxValue") as System.Web.UI.WebControls.TextBox);

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
                            DropDownList ddlValue = (gr.FindControl("ddlTypeValue") as DropDownList);

                            DropDownList ddlRel = (gr.FindControl("DropDownListRelation") as DropDownList);

                            subCondition = ConvertRelation(ddl.SelectedValue, ddlRel.SelectedValue, ddlValue.SelectedValue);
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

                            DropDownList ddlValue = (gr.FindControl("ddlTypeValue") as DropDownList);

                            DropDownList ddlRel = (gr.FindControl("DropDownListRelation") as DropDownList);

                            subCondition += " " + ddlLogic.SelectedValue + " " + ConvertRelation(ddl.SelectedValue, ddlRel.SelectedValue, ddlValue.SelectedValue);
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

        #endregion
        
        //导出
        protected void btn_export_Click(object sender, EventArgs e)
        {
            string condition = "";
            string startime = string.Empty;
            string endtime = string.Empty;
            string marid = string.Empty;
            startime = txtStartTime.Text.Trim();
            endtime = txtEndTime.Text.Trim();
            //marid = txtmarid.Text.Trim();
            condition = "CONVERT(varchar(100), (SI_YEAR+'-'+SI_PERIOD), 23)  between '" + startime + "' and '" + endtime + "'and SI_MARID like '%" + marid + "%'";
            //condition = " SI_YEAR ='" + ddl_year.SelectedValue + "' AND SI_PERIOD ='" + ddl_month.SelectedValue + "'";
            string sqltext = "";
            System.Data.DataTable dt;
            string jibie = ddlType.SelectedValue;
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
            if (jibie == "03")
            {
                sqltext = "select SI_MARID,MNAME,SI_YEAR,SI_PERIOD,cast(SI_BEGNUM as float) as SI_BEGNUM,cast(SI_BEGBAL as float) as SI_BEGBAL,cast(SI_CRCVNUM as float) as SI_CRCVNUM,cast(SI_CRCVMNY as float) as SI_CRCVMNY, cast(SI_GJNUM as float) as SI_GJNUM,cast(SI_GJMNY as float) as SI_GJMNY,cast(SI_ENDNUM as float) as SI_ENDNUM,cast(SI_ENDBAL as float) as SI_ENDBAL";
                sqltext += " from  View_ZGBAL where " + condition + " order by CONVERT(varchar(100), (SI_YEAR+'-'+SI_PERIOD), 23),SI_MARID";
                dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                ExportDataItem(dt);
            }
            else
            {
                if (jibie == "02")
                {
                    sqltext = "select substring(SI_MARID,1,5) as SI_MARID,TY_NAME,SI_YEAR,SI_PERIOD,cast(round(sum(SI_BEGNUM),4) as float) as SI_BEGNUM,";
                    sqltext += "cast(round(sum(SI_BEGBAL),2) as float) as SI_BEGBAL,cast(round(sum(SI_ENDNUM),4) as float) as SI_ENDNUM,";
                    sqltext += "cast(round(sum(SI_ENDBAL),2) as float) as SI_ENDBAL,cast(round(sum(SI_CRCVNUM),4) as float) as SI_CRCVNUM,";
                    sqltext += "cast(round(sum(SI_CRCVMNY),2) as float) as SI_CRCVMNY, cast(round(sum(SI_GJNUM),4) as float) as SI_GJNUM,";
                    sqltext += " cast(round(sum(SI_GJMNY),2) as float) as SI_GJMNY from View_ZGBAL ";
                    sqltext += " where " + condition + " group by substring(SI_MARID,1,5),TY_NAME,SI_YEAR,SI_PERIOD order by CONVERT(varchar(100), (SI_YEAR+'-'+SI_PERIOD), 23),substring(SI_MARID,1,5)";
                }
                if (jibie == "01")
                {
                    sqltext = "select substring(SI_MARID,1,2) as SI_MARID,(case when substring(SI_MARID,1,2)='01' then  '原材料'  when substring(SI_MARID,1,2)='02' then '低值易耗品' else  null end) as TY_NAME, ";
                    sqltext += " SI_YEAR,SI_PERIOD,cast(round(sum(SI_BEGNUM),4) as float) as SI_BEGNUM,";
                    sqltext += "cast(round(sum(SI_BEGBAL),2) as float) as SI_BEGBAL,cast(round(sum(SI_CRCVNUM),4) as float) as SI_CRCVNUM,";
                    sqltext += "cast(round(sum(SI_CRCVMNY),2)as float) as SI_CRCVMNY,cast(round(sum(SI_GJNUM),4) as float) as SI_GJNUM,";
                    sqltext += "cast(round(sum(SI_GJMNY),2)as float) as SI_GJMNY,cast(round(sum(SI_ENDNUM),4) as float) as SI_ENDNUM,";
                    sqltext += " cast(round(sum(SI_ENDBAL),2)as float) as SI_ENDBAL from  View_ZGBAL ";
                    sqltext += " where " + condition + " group by substring(SI_MARID,1,2),SI_YEAR,SI_PERIOD order by CONVERT(varchar(100), (SI_YEAR+'-'+SI_PERIOD), 23),substring(SI_MARID,1,2) ";
                }
                dt  = DBCallCommon.GetDTUsingSqlText(sqltext);
                ExportDataItem(dt);
            }
        }
        private void ExportDataItem(System.Data.DataTable dt)
        {
            string filename = "暂估明细汇总.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("暂估明细查询.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.GetRow(i + 3);
                    row = sheet0.CreateRow(i + 3);
                    row.CreateCell(0).SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                    }

                }

                for (int i = 0; i <= dt.Columns.Count; i++)
                {
                    sheet0.AutoSizeColumn(i);
                }

                sheet0.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }

        }
        private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet) //输出Excel文件并退出
        {
            try
            {

                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Workbooks.Close();
                m_xlApp.Quit();
                m_xlApp.Application.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);
                wksheet = null;
                workbook = null;
                m_xlApp = null;
                GC.Collect();
                //下载
                System.IO.FileInfo path = new System.IO.FileInfo(filename);
                //同步，异步都支持
                HttpResponse contextResponse = HttpContext.Current.Response;
                contextResponse.Redirect(string.Format("~/FM_Data/ExportFile/{0}", path.Name), false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
