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
using ZCZJ_DPF;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_KTBGJZG : System.Web.UI.Page
    {
        double jehj = 0;
        double hsjehj = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                this.InitPage();
                UCPaging1.CurrentPage = 1;
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
        }//

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
            pager_org.TableName = "View_TBFM_YGJZG";
            pager_org.PrimaryKey = "GI_INCOED";
            pager_org.ShowFields = "GI_INCOED,GI_CODE,GI_DATE,left(CONVERT(CHAR(10),WG_VERIFYDATE,23),10) as WG_VERIFYDATE,GI_SUPPLIERNM,GI_MATCODE,GI_NAME,GI_GUIGE,GI_NUM,GI_INAMTMNY,GI_INCATAMTMNY";
            pager_org.OrderField = "GI_INCOED";
            pager_org.StrWhere = strstring();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 50;
        }

        private string strstring()
        {
            string sqlText = "1=1 and (substring(WG_VERIFYDATE,1,7)<>substring(GI_DATE,1,7)) and (WG_CODE like 'G%' or WG_CODE like 'T%') and WG_VERIFYDATE is not null ";
            if (dplYear.SelectedIndex != 0)
            {

                sqlText += " and WG_VERIFYDATE like '" + dplYear.SelectedValue + "-%' ";
            }

            if (dplMoth.SelectedIndex != 0)
            {
                sqlText += " and WG_VERIFYDATE like '%-" + dplMoth.SelectedValue + "-%' ";
            }
            if (dplwltype.SelectedValue == "0")
            {
                sqlText += " and GI_MATCODE like '01.07%'";
            }
            else if (dplwltype.SelectedValue == "1")
            {
                sqlText += " and GI_MATCODE not like '01.07%'";
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
         
                rptProNumCost.DataSource = null;
                rptProNumCost.DataBind();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
       
        }
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            rptProNumCost.DataSource = null;
            rptProNumCost.DataBind();
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }



        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string condition = strstring();
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Label lbrkdh = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbrkdh");
                System.Web.UI.WebControls.Label lbfpbh = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbfpbh");
                System.Web.UI.WebControls.Label lbfpdate = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbfpdate");

                System.Web.UI.WebControls.Label lbrkddate = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbrkddate");
                System.Web.UI.WebControls.Label lbgys = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbgys");
                System.Web.UI.WebControls.Label lbwlbm = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbwlbm");
                System.Web.UI.WebControls.Label lbwlname = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbwlname");
                System.Web.UI.WebControls.Label lbguige = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbguige");
                System.Web.UI.WebControls.Label lbsl = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbsl");
                System.Web.UI.WebControls.Label lbje = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbje");
                System.Web.UI.WebControls.Label lbhsje = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbhsje");
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select isnull(sum(GI_INAMTMNY),0) as GI_INAMTMNY,isnull(sum(GI_INCATAMTMNY),0) as GI_INCATAMTMNY from View_TBFM_YGJZG where " + condition;

                SqlDataReader drhj = DBCallCommon.GetDRUsingSqlText(sqlhj);

                if (drhj.Read())
                {
                    jehj = Convert.ToDouble(drhj["GI_INAMTMNY"].ToString());
                    hsjehj = Convert.ToDouble(drhj["GI_INCATAMTMNY"].ToString());
                }
                drhj.Close();
                System.Web.UI.WebControls.Label lbjehj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbjehj");
                System.Web.UI.WebControls.Label lbhsjehj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbhsjehj");

                lbjehj.Text = jehj.ToString();
                lbhsjehj.Text = hsjehj.ToString();
            }
        }




        //导出
        protected void btnexport_OnClick(object sender, EventArgs e)
        {
            string condition = strstring();

            string sqlzgmx = "select GI_INCOED,GI_CODE,GI_DATE,left(CONVERT(CHAR(10),WG_VERIFYDATE,23),10) as WG_VERIFYDATE,GI_SUPPLIERNM,GI_MATCODE,GI_NAME,GI_GUIGE,GI_NUM,GI_INAMTMNY,GI_INCATAMTMNY from View_TBFM_YGJZG where " + condition + " order by GI_INCOED";
            System.Data.DataTable dtzgmx = DBCallCommon.GetDTUsingSqlText(sqlzgmx);
            string filename = "已勾稽暂估单据明细导出.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("已勾稽暂估单据明细导出.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < dtzgmx.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);

                    for (int j = 0; j < dtzgmx.Columns.Count; j++)
                    {
                        string str = dtzgmx.Rows[i][j].ToString();
                        row.CreateCell(j).SetCellValue(str);
                    }

                }
                for (int r = 0; r <= dtzgmx.Columns.Count; r++)
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
