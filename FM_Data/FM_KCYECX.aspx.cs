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
    public partial class FM_KCYECX : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        Repeater CurRepeater;
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                
                bindGV();

                UCPaging1.Visible = false;

            }
        }
        private Dictionary<string, string> bindThreeList()
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
                    ddl.DataSource = bindThreeList();
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
            if ((beg_time.Text!= "") && (end_time.Text!= ""))
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
                Beg_time ="0000-00";
                End_time = end_time.Text.Substring(0, 7);
                condition = "SI_YEAR+'-'+SI_PERIOD between '" + Beg_time + "' and '" + End_time + "'";
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

        protected void btn_export_Click(object sender, EventArgs e)//导出
        {
            //rptProductNumStc.DataSource = null;
            //rptProductNumStc.DataBind();
            //this.ddlchaxun_SelectedIndexChanged(null, null);//防止长时间不导出而他人操作数据得表TBFM_PRIDSTATISTICS中数据改变，故重新执行一遍
            string condition = this.GetCondition();
            string sqltext = "";
            System.Data.DataTable dt;

            string SubCondtion = this.GetSubCondtion();



            if (SubCondtion != "")
            {
                condition += " AND (" + SubCondtion + " ) ";
            }
           
            sqltext = "select SI_MARID,MNAME,CAIZHI,GUIGE,GB,PURCUNIT,SI_YEAR,SI_PERIOD,SI_BEGNUM,SI_BEGBAL,SI_CRCVNUM,SI_CRCVMNY,SI_CSNDNUM,SI_CSNDMNY,SI_ENDNUM,SI_ENDBAL";
            sqltext += " from View_STORAGEBAL_MAR where " + condition + " order by SI_MARID";
            dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }

        private void ExportDataItem(System.Data.DataTable dt)
        {
            string filename = "材料明细账.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("库存期间查询.xls")))
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
            string ShowFields = "SI_MARID,MNAME,CAIZHI,GUIGE,GB,PURCUNIT,cast(SI_BEGNUM as float) as SI_BEGNUM,cast(SI_BEGBAL as float) as SI_BEGBAL,cast(SI_ENDNUM as float) as SI_ENDNUM,cast(SI_ENDBAL as float) as SI_ENDBAL,cast(SI_CRCVNUM as float) as SI_CRCVNUM,cast(SI_CRCVMNY as float) as SI_CRCVMNY,cast(SI_CSNDNUM as float) as SI_CSNDNUM,cast(SI_CSNDMNY as float) as SI_CSNDMNY,SI_YEAR,SI_PERIOD ";
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


           


            
    }
}
