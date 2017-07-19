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
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_CNFB : System.Web.UI.Page
    {
        double myjehj = 0;
        double sjjehj = 0;

        protected void Page_Load(object sender, EventArgs e)
        {   
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                //2017.04.10还原
                ViewState["StrWhere"] = "CNFB_TYPE not like '%N%'";
                //2016.12.27修改，以前喷漆喷砂属于该分包暂估，现在不属于（属于场内结构）
                //ViewState["StrWhere"] = "(CNFB_TYPE not like '%N%' and CNFB_YEAR<'2016') or (CNFB_TYPE not like '%N%' and CNFB_YEAR<='2016' and CNFB_MONTH<'12') or (CNFB_TYPE not like '%N%' and CNFB_TYPE not like '%喷漆喷砂%' and CNFB_YEAR='2016' and CNFB_MONTH='12') or (CNFB_TYPE not like '%N%' and CNFB_TYPE not like '%喷漆喷砂%' and CNFB_YEAR>='2016') ";
                //ViewState["StrWhere"] = "CNFB_TYPE not like '%N%'";//2016.12.27原始条件
                this.InitVar();
                this.bindGrid();
            }
            if (IsPostBack)
            {
                this.InitVar();
            }
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
            string month = (DateTime.Now.Month).ToString();
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
        }//


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


        #region 分页
        PagerQueryParam pager = new PagerQueryParam();
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager.TableName = "TBMP_CNFB_LIST";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "ID,CNFB_PROJNAME,CNFB_HTID,CNFB_TSAID,CNFB_TH,CNFB_SBNAME,CNFB_NUM,cast(isnull(CNFB_BYMYMONEY,0) as decimal(12,2)) as CNFB_BYMYMONEY,cast(isnull(CNFB_BYREALMONEY,0) as decimal(12,2)) as CNFB_BYREALMONEY,CNFB_YEAR,CNFB_MONTH,CNFB_TYPE";
            pager.OrderField = "CNFB_PROJNAME";
            pager.StrWhere = ViewState["StrWhere"].ToString();
            pager.OrderType = 0;
            pager.PageSize = 50;
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
        }
        #endregion


        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            //2017.04.10还原
            string sql = "CNFB_TYPE not like '%N%'";
            if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex == 0)
            {
                sql = "CNFB_TYPE not like '%N%'";
            }
            //2016.12.27修改，以前喷漆喷砂属于该分包暂估，现在不属于（属于场内结构）
            //string sql = " ((CNFB_TYPE not like '%N%' and CNFB_YEAR<'2016') or (CNFB_TYPE not like '%N%' and CNFB_YEAR<='2016' and CNFB_MONTH<'12') or (CNFB_TYPE not like '%N%' and CNFB_TYPE not like '%喷漆喷砂%' and CNFB_YEAR='2016' and CNFB_MONTH='12') or (CNFB_TYPE not like '%N%' and CNFB_TYPE not like '%喷漆喷砂%' and CNFB_YEAR>='2016')) ";
            ////string sql = "CNFB_TYPE not like '%N%'";//2016.12.27原始条件
            //if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex == 0)
            //{
            //    //2016.12.27修改，以前喷漆喷砂属于该分包暂估，现在不属于（属于场内结构）
            //    sql = " ((CNFB_TYPE not like '%N%' and CNFB_YEAR<'2016') or (CNFB_TYPE not like '%N%' and CNFB_YEAR<='2016' and CNFB_MONTH<'12') or (CNFB_TYPE not like '%N%' and CNFB_TYPE not like '%喷漆喷砂%' and CNFB_YEAR='2016' and CNFB_MONTH='12') or (CNFB_TYPE not like '%N%' and CNFB_TYPE not like '%喷漆喷砂%' and CNFB_YEAR>='2016')) ";
            //    //sql = "CNFB_TYPE not like '%N%'";//2016.12.27原始条件
            //}
            else if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex == 0)
            {
                sql += " and CNFB_YEAR like '%" + dplYear.SelectedValue.ToString().Trim() + "%'";
            }
            else if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex != 0)
            {
                sql += " and CNFB_MONTH like '%" + dplMoth.SelectedValue.ToString().Trim() + "%'";
            }
            else
            {
                sql += " and CNFB_YEAR like '%" + dplYear.SelectedValue.ToString().Trim() + "%' and CNFB_MONTH like '%" + dplMoth.SelectedValue.ToString().Trim() + "%'";
            }
            if (txtrwh.Text != "")
            {
                sql += " and CNFB_TSAID like '%"+txtrwh.Text.ToString().Trim()+"%'";
            }
            ViewState["StrWhere"] = sql;
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }



        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Label lbprojname = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbprojname");
                System.Web.UI.WebControls.Label lbprojid = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbprojid");
                System.Web.UI.WebControls.Label lbrwh = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbrwh");
                System.Web.UI.WebControls.Label lbth = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbth");
                System.Web.UI.WebControls.Label lbsbname = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbsbname");
                System.Web.UI.WebControls.Label lbsl = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbsl");
                System.Web.UI.WebControls.Label lbbymymoney = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbbymymoney");
                System.Web.UI.WebControls.Label lbbyrealmoney = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbbyrealmoney");
                System.Web.UI.WebControls.Label lbyear = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbyear");
                System.Web.UI.WebControls.Label lbmonth = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbmonth");
                System.Web.UI.WebControls.Label lbtype = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbtype");
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select cast(isnull(CAST(sum(CNFB_BYMYMONEY) AS FLOAT),0) as decimal(12,2)) as BYMYMONEYHJ,cast(isnull(CAST(sum(CNFB_BYREALMONEY) AS FLOAT),0) as decimal(12,2)) as BYREALMONEYHJ from TBMP_CNFB_LIST where " + ViewState["StrWhere"].ToString();

                SqlDataReader drhj = DBCallCommon.GetDRUsingSqlText(sqlhj);

                if (drhj.Read())
                {
                    myjehj = Convert.ToDouble(drhj["BYMYMONEYHJ"]);
                    sjjehj = Convert.ToDouble(drhj["BYREALMONEYHJ"]);
                }
                drhj.Close();
                System.Web.UI.WebControls.Label lbmyjehj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbmyjehj");
                System.Web.UI.WebControls.Label lbsjjehj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbsjjehj");
                lbmyjehj.Text = myjehj.ToString();
                lbsjjehj.Text = sjjehj.ToString();
            }
        }



        //按任务号，班组，月份导出
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (dplYear.SelectedIndex == 0 || dplMoth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月份！！！');", true);
                return;
            }
            string sqlcnfb = "select CNFB_PROJNAME,CNFB_TSAID,cast(sum(isnull(CNFB_BYMYMONEY,0)) as decimal(12,2)) as BYMYMONEY,cast(sum(isnull(CNFB_BYREALMONEY,0)) as decimal(12,2)) as BYREALMONEY,CNFB_YEAR,CNFB_MONTH,CNFB_TYPE from TBMP_CNFB_LIST where " + ViewState["StrWhere"].ToString()+" group by CNFB_PROJNAME,CNFB_TSAID,CNFB_YEAR,CNFB_MONTH,CNFB_TYPE";
            System.Data.DataTable dtcnfb = DBCallCommon.GetDTUsingSqlText(sqlcnfb);
            string filename = "厂内分包按任务号汇总.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("厂内分包按任务号汇总.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < dtcnfb.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dtcnfb.Columns.Count; j++)
                    {
                        string str = dtcnfb.Rows[i][j].ToString();
                        row.CreateCell(j + 1).SetCellValue(str);
                    }

                }
                for (int r = 0; r <= dtcnfb.Columns.Count; r++)
                {
                    sheet1.AutoSizeColumn(r);
                }
                sheet1.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
         
    }
}
