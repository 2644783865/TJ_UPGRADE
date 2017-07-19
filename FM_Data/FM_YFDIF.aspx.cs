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
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using ZCZJ_DPF;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_YFDIF : System.Web.UI.Page
    {
        double jsdjehj = 0;
        double fpjehj = 0;
        double cehj = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["StrWhere"] = "1=1";
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
                bindGV();
            }

            if (IsPostBack)
            {
                this.InitVar();
            }

        }

        #region  分页
        PagerQueryParam pager_org = new PagerQueryParam();
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        private void InitPager()
        {
            pager_org.TableName = "TBFM_YFDIF";
            pager_org.PrimaryKey = "DIFYF_JHGZH";
            pager_org.ShowFields = "DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH,DIFYF_JSDDATE,DIFYF_JSDH,DIFYF_JHGZH,DIFYF_BJNAME,DIFYF_BJID,cast(DIFYF_JSDYJE as decimal(12,2)) as DIFYF_JSDYJE,DIFYF_FPJE,cast(DIFYF_DIFMONEY as decimal(12,2)) as DIFYF_DIFMONEY";
            pager_org.OrderField = "DIFYF_JHGZH";
            pager_org.StrWhere = ViewState["StrWhere"].ToString();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 30;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }

        }



        //给用于筛选的GRIDVIEW绑定一定行数条件/////////
        private void bindGV()
        {
            GridViewSearch.DataSource = CreateTable();

            GridViewSearch.DataBind();
        }

        private System.Data.DataTable CreateTable()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add(new DataColumn("index", typeof(int)));
            for (int i = 0; i < 8; i++)
            {
                DataRow row = dt.NewRow();
                row["index"] = i;
                dt.Rows.Add(row);
            }
            return dt;
        }

        protected void GridViewSearch_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                if (gr.RowIndex == (GridViewSearch.Rows.Count - 1))
                {
                    (gr.FindControl("tb_logic") as System.Web.UI.WebControls.TextBox).Visible = false;
                }
            }
        }
        ///////////////////////////////////////////////
        //比较关系和数值obj////////////////////////////////////
        private string ConvertRelation(string field, string relation, string fieldValue)
        {
            string obj = string.Empty;
            switch (relation)
            {
                case "0":
                    {
                        //包含
                        obj = field + " LIKE '%" + fieldValue + "%'";
                        break;
                    }
                case "1":
                    {
                        //等于
                        obj = field + " = '" + fieldValue + "'";
                        break;
                    }
                case "2":
                    {
                        //不等于
                        obj = field + " != '" + fieldValue + "'";
                        break;
                    }
                case "3":
                    {
                        //大于
                        obj = field + " > '" + fieldValue + "'";
                        break;
                    }
                case "4":
                    {
                        //大于或等于
                        obj = field + " >= '" + fieldValue + "'";
                        break;
                    }
                case "5":
                    {
                        //小于
                        obj = field + " < '" + fieldValue + "'";
                        break;
                    }
                case "6":
                    {
                        //小于或等于
                        obj = field + " <= '" + fieldValue + "'";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return obj;
        }
        //////////////////////////////////////////////////////////////
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            string sqltext = "1=1";

            string conditon = GetGridViewCondition();//获取查询条件

            //判断逻辑条件是否存在
            if (conditon != "")
            {
                sqltext += " and (" + conditon + ")  ";
            }
            if (startdat.Text.Trim() != "")
            {
                sqltext += " and DIFYF_YEAR+'-'+DIFYF_MONTH >= '" + startdat.Text.Trim() + "' ";
            }
            if (enddat.Text.Trim() != "")
            {
                sqltext += " and DIFYF_YEAR+'-'+DIFYF_MONTH <= '" + enddat.Text.Trim() + "' ";
            }
            ViewState["StrWhere"] = sqltext;
            InitVar();
            UCPaging1.CurrentPage = 1;
            bindGrid();
            #region
            //for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
            //{
            //    if (i == rptProNumCost.Items.Count - 2)
            //    {
            //        for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
            //        {
            //            System.Web.UI.WebControls.Label lbjsd1 = (System.Web.UI.WebControls.Label)rptProNumCost.Items[i].FindControl("lbjsdh");
            //            System.Web.UI.WebControls.Label lbdat1 = (System.Web.UI.WebControls.Label)rptProNumCost.Items[i + 1].FindControl("lbdate");
            //            System.Web.UI.WebControls.Label lbgysm1 = (System.Web.UI.WebControls.Label)rptProNumCost.Items[i + 1].FindControl("lbgysmc");
            //            System.Web.UI.WebControls.Label lbzdrx1 = (System.Web.UI.WebControls.Label)rptProNumCost.Items[i + 1].FindControl("lbzdrxm");
            //            System.Web.UI.WebControls.Label lbjsd2 = (System.Web.UI.WebControls.Label)rptProNumCost.Items[j].FindControl("lbjsdh");
            //            if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
            //            {
            //                lbjsd1.Visible = false;
            //                lbdat1.Visible = false;
            //                lbgysm1.Visible = false;
            //                lbzdrx1.Visible = false;
            //            }
            //        }
            //    }
            //    System.Web.UI.WebControls.Label lbjsdh1 = (System.Web.UI.WebControls.Label)rptProNumCost.Items[i].FindControl("lbjsdh");
            //    System.Web.UI.WebControls.Label lbjsdh2 = (System.Web.UI.WebControls.Label)rptProNumCost.Items[i + 1].FindControl("lbjsdh");
            //    System.Web.UI.WebControls.Label lbdate2 = (System.Web.UI.WebControls.Label)rptProNumCost.Items[i + 1].FindControl("lbdate");
            //    System.Web.UI.WebControls.Label lbgysmc2 = (System.Web.UI.WebControls.Label)rptProNumCost.Items[i + 1].FindControl("lbgysmc");
            //    System.Web.UI.WebControls.Label lbzdrxm2 = (System.Web.UI.WebControls.Label)rptProNumCost.Items[i + 1].FindControl("lbzdrxm");
            //    if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
            //    {
            //        lbjsdh2.Visible = false;
            //        lbdate2.Visible = false;
            //        lbgysmc2.Visible = false;
            //        lbzdrxm2.Visible = false;
            //    }
            //}
            #endregion
            refreshStyle();
        }
        private string GetGridViewCondition()
        {
            string condition = "";
            for (int i = 0; i < GridViewSearch.Rows.Count; i++)
            {
                GridViewRow gr = GridViewSearch.Rows[i];
                DropDownList ddllistname = (DropDownList)gr.FindControl("DropDownListName");//名称
                if (ddllistname.SelectedValue != "NO")
                {
                    System.Web.UI.WebControls.TextBox txtValue = (System.Web.UI.WebControls.TextBox)gr.FindControl("TextBoxValue");//数值
                    DropDownList ddlRel = (DropDownList)gr.FindControl("DropDownListRelation");//比较关系
                    //获取下行的操作
                    DropDownList ddlnext = (DropDownList)GridViewSearch.Rows[i + 1].FindControl("DropDownListName");
                    if (ddlnext.SelectedValue != "NO")
                    {
                        DropDownList ddlLogic = (DropDownList)gr.FindControl("DropDownListLogic");
                        condition += ConvertRelation(ddllistname.SelectedValue, ddlRel.SelectedValue, txtValue.Text.Trim()) + " " + ddlLogic.SelectedValue + " ";
                        if (i == (GridViewSearch.Rows.Count - 1))
                        {
                            //获取最后一行的比较关系和数值
                            System.Web.UI.WebControls.TextBox nextValue = (System.Web.UI.WebControls.TextBox)GridViewSearch.Rows[GridViewSearch.Rows.Count - 1].FindControl("TextBoxValue");
                            DropDownList nextddlRel = (DropDownList)GridViewSearch.Rows[GridViewSearch.Rows.Count - 1].FindControl("DropDownListRelation");
                            condition += ConvertRelation(ddlnext.SelectedValue, nextddlRel.SelectedValue, nextValue.Text.Trim());
                            break;
                        }
                    }
                    else
                    {
                        condition += ConvertRelation(ddllistname.SelectedValue, ddlRel.SelectedValue, txtValue.Text.Trim());
                        break;
                    }
                }
            }
            return condition;
        }


        protected void btndc_click(object sender, EventArgs e)
        {
            string condition = ViewState["StrWhere"].ToString();
            string sqldif = "select DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH,sum(cast(isnull(DIFYF_DIFMONEY,0) as decimal(12,2))) from TBFM_YFDIF where " + condition + " group by DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH";
            System.Data.DataTable dtdif = DBCallCommon.GetDTUsingSqlText(sqldif);
            string filename = "运费差额.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("运费差额导出模版.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < dtdif.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(i + 1);
                    for (int j = 0; j < dtdif.Columns.Count; j++)
                    {

                        string str = dtdif.Rows[i][j].ToString();
                        row.CreateCell(j + 1).SetCellValue(str);
                    }

                }
                for (int r = 0; r <= dtdif.Columns.Count; r++)
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



        //重置////////////////////////////////////////////////////
        protected void btnReset_Click(object sender, EventArgs e)
        {
            startdat.Text = "";
            enddat.Text = "";
            bindGV();
        }
        private void refreshStyle()
        {
            for (int i = 0; i < GridViewSearch.Rows.Count; i++)
            {
                GridViewRow gr = GridViewSearch.Rows[i];
                if ((gr.FindControl("DropDownListName") as DropDownList).SelectedValue != "NO")
                {
                    if (gr.RowIndex != 0)
                    {
                        GridViewRow fgr = GridViewSearch.Rows[i - 1];
                        (fgr.FindControl("DropDownListLogic") as DropDownList).Style.Add("display", "block");
                        (fgr.FindControl("tb_logic") as System.Web.UI.WebControls.TextBox).Style.Add("display", "none");
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
        ////////////////////////////////////////////////////////////////////////////


        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Label lbrwh = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbrwh");
                System.Web.UI.WebControls.Label lbyear = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbyear");
                System.Web.UI.WebControls.Label lbmonth = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbmonth");
                System.Web.UI.WebControls.Label lbjsddate = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbjsddate");
                System.Web.UI.WebControls.Label lbjsdh = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbjsdh");
                System.Web.UI.WebControls.Label lbjhgzh = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbjhgzh");

                System.Web.UI.WebControls.Label lbsbmc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbsbmc");
                System.Web.UI.WebControls.Label lbsbbh = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbsbbh");
                System.Web.UI.WebControls.Label lbjsdje = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbjsdje");
                System.Web.UI.WebControls.Label lbfpje = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbfpje");
                System.Web.UI.WebControls.Label lbce = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbce");
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select cast(isnull(CAST(sum(DIFYF_JSDYJE) AS FLOAT),0) as decimal(12,2)) as JSDJEHJ,isnull(CAST(sum(DIFYF_FPJE) AS FLOAT),0) as FPJEHJ,cast(isnull(CAST(sum(DIFYF_DIFMONEY) AS FLOAT),0) as decimal(12,2)) as CEHJ from TBFM_YFDIF where " + ViewState["StrWhere"].ToString();

                SqlDataReader drhj = DBCallCommon.GetDRUsingSqlText(sqlhj);

                if (drhj.Read())
                {
                    jsdjehj = Convert.ToDouble(drhj["JSDJEHJ"]);
                    fpjehj = Convert.ToDouble(drhj["FPJEHJ"]);
                    cehj = Convert.ToDouble(drhj["CEHJ"]);
                }
                drhj.Close();
                System.Web.UI.WebControls.Label lbjsdjehj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbjsdjehj");
                System.Web.UI.WebControls.Label lbfpjehj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbfpjehj");
                System.Web.UI.WebControls.Label lbcehj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbcehj");
                lbjsdjehj.Text = jsdjehj.ToString();
                lbfpjehj.Text = fpjehj.ToString();
                lbcehj.Text = cehj.ToString();
            }
        }
    }
}
        #endregion