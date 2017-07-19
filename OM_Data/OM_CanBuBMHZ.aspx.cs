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
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Drawing;


namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CanBuBMHZ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                this.GetDep();
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }
            this.InitVar();
        }
        //初始化页面
        private void InitPage()
        {
            //显示当月
            dplYear.ClearSelection();
            foreach (ListItem li in dplYear.Items)
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }
            dplMoth.ClearSelection();
            string month = (DateTime.Now.Month - 1).ToString();
            if (DateTime.Now.Month < 10)
            {
                month = "0" + month;
            }
            foreach (ListItem li in dplMoth.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
        }
        protected void GetDep()
        {
            string sql = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO WHERE len(DEP_CODE)=2";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataBind();
            ListItem item = new ListItem("--请选择--", "0");
            ddl_Depart.Items.Insert(0, item);
        }

        #region  分页
        PagerQueryParamGroupBy pager_org = new PagerQueryParamGroupBy();

        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        private void InitPager()
        {
            pager_org.TableName = "(select * from OM_Canbusp left join OM_CanBu on OM_Canbusp.bh=OM_CanBu.detailbh left join OM_KQTJ on (OM_CanBu.CB_STID=OM_KQTJ.KQ_ST_ID and OM_CanBu.CB_YearMonth=OM_KQTJ.KQ_DATE) left join TBDS_STAFFINFO on OM_CanBu.CB_STID=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t";
            pager_org.PrimaryKey = "DEP_NAME";
            pager_org.ShowFields = "DEP_NAME,CB_YearMonth,sum(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)) as CB_MonthCBhj,sum(CB_BuShangYue) as CB_BuShangYuehj,sum(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)+CB_BuShangYue) as CB_HeJihj";
            pager_org.OrderField = "DEP_NAME";
            pager_org.StrWhere = strstring();
            pager_org.OrderType = 0;
            pager_org.PageSize = 100;
            pager_org.GroupField = "DEP_NAME,CB_YearMonth";
        }

        private string strstring()
        {
            string sqlText = "1=1 and detailbh in (select bh from OM_Canbusp where state='2')";
            if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
            {
                sqlText += " and CB_YearMonth='" + dplYear.SelectedValue + "-" + dplMoth.SelectedValue + "'";
            }
            else if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex == 0)
            {
                sqlText += " and CB_YearMonth like '" + dplYear.SelectedValue + "%'";
            }
            else if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex != 0)
            {
                sqlText += " and CB_YearMonth like '%" + dplMoth.SelectedValue + "'";
            }
            if (ddl_Depart.SelectedIndex != 0)
            {
                sqlText += " and ST_DEPID='" + ddl_Depart.SelectedValue.ToString().Trim() + "'";
            }
            return sqlText;
        }


        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamGroupBy(pager_org);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, palNoData);
            int zongji = 0;
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
                heji.Visible = false;
                zonge.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
                heji.Visible = true;
                zonge.Visible = true;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                zongji += (CommonFun.ComTryInt(dt.Rows[i]["CB_HeJihj"].ToString()));

            }
            heji.Text = zongji.ToString();
        }
        private void BindYearMoth(DropDownList dpl_Year, DropDownList dpl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                dpl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            dpl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }
        #endregion
        /// <summary>
        /// 年、月份改变 查询
        /// </summary>
        protected void OnSelectedIndexChanged_dplYearMoth(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

        protected void dplbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }
        protected void daochu_clicked(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext = "select DEP_NAME,CB_YearMonth,sum(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)) as CB_MonthCBhj,sum(CB_BuShangYue) as CB_BuShangYuehj,sum(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)+CB_BuShangYue) as CB_HeJihj from (select * from OM_Canbusp left join OM_CanBu on OM_Canbusp.bh=OM_CanBu.detailbh left join OM_KQTJ on (OM_CanBu.CB_STID=OM_KQTJ.KQ_ST_ID and OM_CanBu.CB_YearMonth=OM_KQTJ.KQ_DATE) left join TBDS_STAFFINFO on OM_CanBu.CB_STID=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t where " + strstring() + " group by DEP_NAME,CB_YearMonth ";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string filename = dplYear.SelectedValue.ToString() + "年" + dplMoth.SelectedValue.ToString() + "月餐补导出.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            Microsoft.Office.Interop.Excel.Application oXL;
            Microsoft.Office.Interop.Excel.Range oRang;
            Microsoft.Office.Interop.Excel._Workbook oWB;
            Microsoft.Office.Interop.Excel._Worksheet oSheet;
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("餐补汇总导出.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                IRow row1 = sheet0.CreateRow(0);

                //row1.HeightInPoints = 14;
                //oXL = new Microsoft.Office.Interop.Excel.Application();
                //oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet));
                //oSheet = (Microsoft.Office.Interop.Excel._Worksheet)(oWB.ActiveSheet);
                //oRang = (Microsoft.Office.Interop.Excel._Worksheet)(sheet0.SetActive); 


                //oRang = oSheet.get_Range("A1", "F1");
                //oRang.MergeCells = true;
                //oRang.Font.Bold = true;
                //oRang.Font.Size="20";
                //oRang.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                row1.CreateCell(0).SetCellValue(dplYear.SelectedValue.ToString() + "年" + dplMoth.SelectedValue.ToString() + "月餐补");
                //Microsoft.Office.Interop.Excel.Range Ran = (Microsoft.Office.Interop.Excel.Range)(row1.Cells[0]);
                //Ran.Font.Bold = true;
                //Ran.Font.Size = "20";
                NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                font1.FontName = "宋体";//字体
                font1.FontHeightInPoints = 20;//字号

                ICellStyle cells = wk.CreateCellStyle();
                cells.SetFont(font1);
                cells.Alignment = HorizontalAlignment.CENTER;
                row1.Cells[0].CellStyle = cells;
                int zonge = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2);
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));
                    row.CreateCell(1).SetCellValue("" + dt.Rows[i]["CB_YearMonth"].ToString());
                    row.CreateCell(2).SetCellValue("" + dt.Rows[i]["DEP_NAME"].ToString());
                    row.CreateCell(3).SetCellValue("" + dt.Rows[i]["CB_MonthCBhj"].ToString());
                    row.CreateCell(4).SetCellValue("" + dt.Rows[i]["CB_BuShangYuehj"].ToString());
                    row.CreateCell(5).SetCellValue("" + dt.Rows[i]["CB_HeJihj"].ToString());
                    zonge += (CommonFun.ComTryInt(dt.Rows[i]["CB_HeJihj"].ToString()));
                }
                IRow row2 = sheet0.CreateRow(dt.Rows.Count + 2);
                row2.CreateCell(5).SetCellValue("总额：" + zonge.ToString());
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
    }
}
