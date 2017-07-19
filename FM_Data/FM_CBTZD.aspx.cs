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
using System.IO;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_CBTZD : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        Repeater CurRepeater;
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {

                bindGV();

                UCPaging1.CurrentPage = 1;
                this.getData();
                UpdatePanelBody.Update();


            }
        }
        private Dictionary<string, string> bindName()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();

            ItemList.Add("NO", "");
            ItemList.Add("SI_MARID", "物料编码");
            ItemList.Add("MNAME", "物料名称");
            ItemList.Add("GUIGE", "规格型号");
            ItemList.Add("CAIZHI", "材质");

            return ItemList;
        }

        protected void GridViewSearch_DataBound(object sender, EventArgs e)//对GRIDVIEW中的名称进行设置
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {


                DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                ddl.DataTextField = "value";
                ddl.DataValueField = "key";
                ddl.DataSource = bindName();
                ddl.DataBind();



                if (gr.RowIndex == 4)
                {
                    (gr.FindControl("tb_logic") as System.Web.UI.WebControls.TextBox).Visible = false;
                }
            }
        }

        protected string GetSubCondtion()//生成GridView中的查询条件
        {
            string subCondition = "";

            foreach (GridViewRow gr in GridViewSearch.Rows)
            {

                if (gr.RowIndex == 0)//为第一时不加逻辑
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
            return subCondition;
        }

        protected string GetCondition()
        {
            string Beg_time = string.Empty;
            string End_time = string.Empty;

            string condition = string.Empty;


            if ((beg_time.Text != "") && (end_time.Text != ""))
            {
                Beg_time = beg_time.Text.Substring(0, 7);
                End_time = end_time.Text.Substring(0, 7);
                condition = "SI_YEAR+'-'+SI_PERIOD between '" + Beg_time + "' and '" + End_time + "'";
            }
            if ((beg_time.Text == "") && (end_time.Text == ""))
            {
                condition = "SI_YEAR='" + System.DateTime.Now.Year.ToString() + "' and SI_PERIOD='" + System.DateTime.Now.Month.ToString().PadLeft(2, '0') + "'";
            }
            if ((beg_time.Text != "") && (end_time.Text == ""))
            {
                Beg_time = beg_time.Text.Substring(0, 7);
                End_time = System.DateTime.Now.Year.ToString() + '-' + System.DateTime.Now.Month.ToString().PadLeft(2, '0');
                condition = "SI_YEAR+'-'+SI_PERIOD between '" + Beg_time + "' and '" + End_time + "'";
            }
            if ((beg_time.Text == "") && (end_time.Text != ""))
            {
                Beg_time = "0000-00";
                End_time = end_time.Text.Substring(0, 7);
                condition = "SI_YEAR+'-'+SI_PERIOD between '" + Beg_time + "' and '" + End_time + "'";
            }

            if (ddlstatus.SelectedValue == "0")
            {
                //入库调整单
                condition += " and SI_PTCODE='1' and isnull(SI_CRVDIFF,0)<>0 ";
            }
            else if (ddlstatus.SelectedValue == "1")
            {
                //出库调整单
                condition += " and SI_PTCODE='1' and isnull(SI_CSNDDIFF,0)<>0 ";
            }
            else if (ddlstatus.SelectedValue == "2")
            {
                //期初调整单
                condition += " and SI_BEGNUM=0 and isnull(SI_BEGDIFF,0)<>0";
            }

            return condition;
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

        protected void btnQuery_Click(object sender, EventArgs e)
        {

            getData();

            //refreshStyle();
            ModalPopupExtenderSearch.Hide();
            UpdatePanelBody.Update();
        }

        private void getData()
        {

            string condition = GetCondition();

            string SubCondtion = GetSubCondtion();

            if (condition != "")
            {
                if (SubCondtion != "")
                {
                    condition += " AND (" + SubCondtion + " ) ";
                }
            }


            BindData(condition);
        }

        private void BindData(string condition)
        {
            string TableName = "View_STORAGEBAL_MAR";
            string PrimaryKey = "";
            string ShowFields = "";

            if (ddlstatus.SelectedValue == "0")
            {
                ShowFields = "SI_MARID,MNAME,CAIZHI,GUIGE,SI_YEAR,SI_PERIOD,SI_CRVDIFF as DIFF ";
            }
            else if (ddlstatus.SelectedValue == "1")
            {
                //出库调整单
                ShowFields += " SI_MARID,MNAME,CAIZHI,GUIGE,SI_YEAR,SI_PERIOD,SI_CSNDDIFF as DIFF";
            }
            else if (ddlstatus.SelectedValue == "2")
            {
                //期初调整单
                ShowFields = "SI_MARID,MNAME,CAIZHI,GUIGE,SI_YEAR,SI_PERIOD,SI_BEGDIFF as DIFF ";
            }

            string OrderField = "SI_MARID";
            int OrderType = 0;
            string StrWhere = condition;
            int PageSize = 50;
            CurRepeater = Repeater1;
            InitVar(TableName, PrimaryKey, ShowFields, OrderField, StrWhere, OrderType, PageSize);
        }

        private void refreshStyle()
        {
            //上面部分时间要进行重置
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

                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "block");//block显示出来，none不显示
                    (gr.FindControl("tb_name") as System.Web.UI.WebControls.TextBox).Style.Add("display", "none");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "block");
                    (gr.FindControl("tb_relation") as System.Web.UI.WebControls.TextBox).Style.Add("display", "none");
                    (gr.FindControl("TextBoxValue") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
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
                    (gr.FindControl("TextBoxValue") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                    (gr.FindControl("ddlTypeValue") as DropDownList).Style.Add("display", "none");
                }
            }
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
                (gr.FindControl("TextBoxValue") as System.Web.UI.WebControls.TextBox).Text = string.Empty; ;
            }


            refreshStyle();
        }

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

        private string ConvertRelation(string field, string relation, string fieldValue)//根据GridView中每行不同的数值设置查询条件
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

        private void InitVar(string _tablename, string _primarykey, string _showfields, string _orderfield, string _strwhere, int _ordertype, int _pagesize)
        {
            InitPager(_tablename, _primarykey, _showfields, _orderfield, _strwhere, _ordertype, _pagesize);
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize; //每页显示的记录数
            bindGrid(CurRepeater);
        }

        private void InitPager(string _tablename, string _primarykey, string _showfields, string _orderfield, string _strwhere, int _ordertype, int _pagesize)
        {
            //this.GetListName();
            pager.TableName = _tablename;
            pager.PrimaryKey = _primarykey;
            pager.ShowFields = _showfields;
            pager.OrderField = _orderfield; //"dbo.f_formatstr(" + ddlSort.SelectedValue.ToString() + ", '.')";
            pager.StrWhere = _strwhere; // ViewState["sqlText"].ToString();
            pager.OrderType = _ordertype; //升序排列            
            pager.PageSize = _pagesize;
            this.GetTotalAmount();
        }

        void Pager_PageChanged(int pageNumber)
        {
            //Control[] CRL = this.BindGridParamsRecord(ViewState["CurrentUCPaging"].ToString());
            //bindGrid((UCPaging)CRL[0], (GridView)CRL[1], (Panel)CRL[2]);
            getData();
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
        }

        protected void btn_export_Click(object sender, EventArgs e)//导出
        {
            //rptProductNumStc.DataSource = null;
            //rptProductNumStc.DataBind();
            //this.ddlchaxun_SelectedIndexChanged(null, null);//防止长时间不导出而他人操作数据得表TBFM_PRIDSTATISTICS中数据改变，故重新执行一遍
            string condition = this.GetCondition();
            string sqltext = "";
            System.Data.DataTable TotalDT;

            string SubCondtion = this.GetSubCondtion();



            if (SubCondtion != "")
            {
                condition += " AND (" + SubCondtion + " ) ";
            }

           

            if (ddlstatus.SelectedValue == "0")
            {
                sqltext = "select SI_MARID,MNAME,CAIZHI,GUIGE,SI_YEAR,SI_PERIOD,SI_CRVDIFF as DIFF";
                sqltext += " from View_STORAGEBAL_MAR where " + condition + " order by SI_MARID";
            }
            else if (ddlstatus.SelectedValue == "1")
            {
                //出库调整单
                sqltext = "select SI_MARID,MNAME,CAIZHI,GUIGE,SI_YEAR,SI_PERIOD,SI_CSNDDIFF as DIFF";
                sqltext += " from View_STORAGEBAL_MAR where " + condition + " order by SI_MARID";
            }
            else if (ddlstatus.SelectedValue == "2")
            {
                //期初调整单
                sqltext = "select SI_MARID,MNAME,CAIZHI,GUIGE,SI_YEAR,SI_PERIOD,SI_BEGDIFF as DIFF";
                sqltext += " from View_STORAGEBAL_MAR where " + condition + " order by SI_MARID";
            }
            TotalDT = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(TotalDT);
        }

        private void ExportDataItem(System.Data.DataTable TotalDT)
        {
            string filename = "成本调整单" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();

            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("成本调整单查询.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据
                for (int i = 0; i < TotalDT.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 3);
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号
                    row.CreateCell(1).SetCellValue(TotalDT.Rows[i]["SI_MARID"].ToString());
                    row.CreateCell(2).SetCellValue(TotalDT.Rows[i]["MNAME"].ToString());
                    row.CreateCell(3).SetCellValue(TotalDT.Rows[i]["CAIZHI"].ToString());
                    row.CreateCell(4).SetCellValue(TotalDT.Rows[i]["GUIGE"].ToString());
                    row.CreateCell(5).SetCellValue(TotalDT.Rows[i]["SI_YEAR"].ToString());
                    row.CreateCell(6).SetCellValue(TotalDT.Rows[i]["SI_PERIOD"].ToString());
                    row.CreateCell(7).SetCellValue(TotalDT.Rows[i]["DIFF"].ToString());


                    NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                    font1.FontName = "仿宋";//字体
                    font1.FontHeightInPoints = 11;//字号
                    ICellStyle cells = wk.CreateCellStyle();
                    cells.SetFont(font1);
                    cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                    for (int j = 0; j <= 7; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }
                }
                #endregion

                for (int i = 0; i <= 7; i++)
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






        private void GetTotalAmount()
        {
            string condition = this.GetCondition();
            string sql = "";


            string SubCondtion = this.GetSubCondtion();



            if (SubCondtion != "")
            {
                condition += " AND (" + SubCondtion + " ) ";

            }

            if (ddlstatus.SelectedValue == "0")
            {
                sql = "select isnull(CAST(sum(SI_CRVDIFF) AS FLOAT),0) as TotalDIFF from View_STORAGEBAL_MAR where " + condition;
            }
            else if (ddlstatus.SelectedValue == "1")
            {
                //出库调整单
                sql = "select isnull(CAST(sum(SI_CSNDDIFF) AS FLOAT),0) as TotalDIFF from View_STORAGEBAL_MAR where " + condition;

            }
            else if (ddlstatus.SelectedValue == "2")
            {
                //期初调整单
                sql = "select isnull(CAST(sum(SI_BEGDIFF) AS FLOAT),0) as TotalDIFF from View_STORAGEBAL_MAR where " + condition;
            }

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.Read())
            {

                hfdDIFF.Value = string.Format("{0:c2}", Convert.ToDouble(sdr["TotalDIFF"]));


            }
            sdr.Close();

        }

        protected void ddlstatus_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.getData();
            UpdatePanelBody.Update();

        }

        #region 汇总

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Footer)
            {

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelDIFF")).Text = hfdDIFF.Value;

            }
        }
        #endregion
    }
}