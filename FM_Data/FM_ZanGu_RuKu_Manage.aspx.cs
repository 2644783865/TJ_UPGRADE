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
    public partial class FM_ZanGu_RuKu_Manage : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {

            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);

            if (!IsPostBack)
            {

                bindDDLYear();

                InitPager();

                bindGrid();

                bindGV();
               
            }            

            //CheckUser(ControlFinder);
            hlksummary.NavigateUrl = "FM_ZanGu_RuKu_CompanySummary.aspx";
        }

        private void bindDDLYear()
        {
            string ymdhms = System.DateTime.Now.ToString("yyyyMMdd");
            string curYear = ymdhms.Substring(0, 4);
            ddlYear.Items.Add(new ListItem(curYear, curYear));

            //倒退9年
            for (int i = 1; i < 10; i++)
            {
                string year = (Convert.ToInt16(curYear) - i).ToString();
                ddlYear.Items.Add(new ListItem(year, year));
            }


            ddlYear.Items.FindByValue(getYear()).Selected = true;

            ddlMonth.ClearSelection();

            ddlMonth.Items.FindByValue(getMonth()).Selected = true;
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

        //初始化分布信息

        private void InitPager()
        {
            
            //单据类型为订单下推回来的入库单


            string condition = "ZGAMT<>0 and (WG_CAVFLAG='0' or WG_CODE in('G000002321R1','G000002952S1R1','G000002952S2R1'))";

            if (GetSubCondtion() != string.Empty)
            {
                condition += " AND " + GetSubCondtion();
            }

            pager.TableName = "dbo.FM_ZG('" + ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue + "')";//外购入库总表

            pager.PrimaryKey = "WG_CODE";

            pager.ShowFields = "WG_CODE," + ddlYear.SelectedValue + " as Year,'" + ddlMonth.SelectedValue + "' as Month,COMPANY,left(WG_VERIFYDATE,10) as WG_VERIFYDATE,DOC,VERIFIER,WG_TEARFLAG,WG_CAVFLAG,ZGAMT,ZGCTAMT,(case when ABS(ZGCTAMT)<ABS(INAMT) then 'B' else 'N' end) as GJSTATE";

            pager.OrderField = "WG_CODE";

            pager.OrderType = 0;

            pager.PageSize = 10;

            pager.StrWhere =condition;

            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数 

            string sql = "select  isnull(sum(ZGAMT),0) as TotalAmount,isnull(sum(ZGCTAMT),0) as TotalCTAmount from dbo.FM_ZG('" + ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue + "') WHERE " + pager.StrWhere;

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.Read())
            {
                hfdTotalAmount.Value = sdr["TotalAmount"].ToString();
                hfdTotalCTAmount.Value = sdr["TotalCTAmount"].ToString();
            }

            sdr.Close();

        }

        void Pager_PageChanged(int pageNumber)
        {
            InitPager();

            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;

            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);

            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#Efefef'");//当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");//当鼠标移开时还原背景色---白色

                string code = GridView1.DataKeys[e.Row.RowIndex]["WG_CODE"].ToString();

                (e.Row.FindControl("hlTask") as HyperLink).Attributes.Add("onClick", "ShowViewModal('" + GridView1.DataKeys[e.Row.RowIndex]["WG_CODE"].ToString() + "');");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "合计:";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[11].Text = string.Format("{0:c2}", Convert.ToDouble(hfdTotalAmount.Value.Trim()));
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[12].Text = string.Format("{0:c2}", Convert.ToDouble(hfdTotalCTAmount.Value.Trim()));
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Center;
            }

        }

        protected void Query_Click(object sender, EventArgs e)
        {
         
            InitPager();

            UCPaging1.CurrentPage = 1;

            bindGrid();

            ModalPopupExtenderSearch.Hide();

            UpdatePanelBody.Update();

            refreshStyle();        
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

        //清除条件
        private void clearCondition()
        {

            //选定今年
            ddlYear.ClearSelection();
            ddlYear.Items.FindByValue(getYear()).Selected = true;

            //选的本月
            ddlMonth.ClearSelection();
            ddlMonth.Items.FindByValue(getMonth()).Selected = true;

        }

        #region 导出
        protected void btn_export_Click(object sender, EventArgs e)
        {
            string condition = "ZGAMT<>0 and (WG_CAVFLAG='0' or WG_CODE in('G000002321R1','G000002952S1R1','G000002952S2R1')) ";

            if (GetSubCondtion() != string.Empty)
            {
                condition += " AND " + GetSubCondtion();
            }
            string sql = "";
          
            if (condition != "")
            {
                sql = "SELECT " + ddlYear.SelectedValue + " as Year,'" + ddlMonth.SelectedValue + "' as Month,WG_CODE,COMPANY,left(WG_VERIFYDATE,10) as WG_VERIFYDATE,DOC,VERIFIER,(CASE WHEN WG_TEARFLAG='0'  then 'N' else 'Y' end) AS WG_TEARFLAG,(CASE WHEN WG_CAVFLAG='0' THEN 'N' ELSE 'Y' END) AS  WG_CAVFLAG,(CASE WHEN ABS(ZGCTAMT)<ABS(INAMT) THEN 'B' ELSE 'N' END) AS GJSTATE,ZGAMT,ZGCTAMT" +
                 " FROM dbo.FM_ZG('" + ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue + "') WHERE "+condition;
            }
           

            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            ExportDataItem(dt);

        }
        private void ExportDataItem(System.Data.DataTable dt)
        {
            string filename = "暂估单据.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("暂估单据查询.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.GetRow(i + 4);
                    row = sheet0.CreateRow(i + 4);
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
        #endregion

        #region 条件框

        private void bindGV()
        {
            GridViewSearch.DataSource = CreateTable();

            GridViewSearch.DataBind();
        }
        private System.Data.DataTable CreateTable()
        {
            System.Data.DataTable dt = new System.Data.DataTable();

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
            ItemList.Add("NO", "");
            ItemList.Add("WG_CODE", "入库单号");
            ItemList.Add("COMPANY", "供应商");
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
                    (gr.FindControl("tb_logic") as System.Web.UI.WebControls.TextBox).Visible = false;
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

        private string ConvertRelation(string field, string relation, string fieldValue)
        {
            string obj = string.Empty;

            if (field == "WG_CODE")
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
                (gr.FindControl("TextBoxValue") as System.Web.UI.WebControls.TextBox).Text = string.Empty; ;
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
                        (gr.FindControl("tb_logic") as System.Web.UI.WebControls.TextBox).Style.Add("display", "none");
                    }

                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "block");
                    (gr.FindControl("tb_name") as System.Web.UI.WebControls.TextBox).Style.Add("display", "none");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "block");
                    (gr.FindControl("tb_relation") as System.Web.UI.WebControls.TextBox).Style.Add("display", "none");
                }
                else
                {
                    (gr.FindControl("DropDownListLogic") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_logic") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_name") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_relation") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                }
            }
        }

        #endregion
    }
}
