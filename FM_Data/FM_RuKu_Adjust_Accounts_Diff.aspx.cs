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
    public partial class FM_RuKu_Adjust_Accounts_Diff : BasicPage
    {

        PagerQueryParam pager = new PagerQueryParam();
        string condition = "1=1";
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);

            if (!IsPostBack)
            {
                //bindDdlYear();
                GetGata();

                InitPager();

                bindGV();//绑定条件框

                bindGrid();

            }
        }

        private void GetGata()
        {
            string StrTime;
            string EndTime;
            StrTime = System.DateTime.Now.ToString("yyyy-MM");
            EndTime = System.DateTime.Now.ToString("yyyy-MM");
            string SubCondtion = GetSubCondtion();

            condition = "CONVERT(varchar(100),(DIFYEAR+'-'+DIFMONTH), 23)  between '" + StrTime + "' and '" + EndTime + "'AND round(DIFAMTMNY,2)!=0";

            if (SubCondtion != "")
            {
                condition += " AND (" + SubCondtion + ")";
            }
        }
        //绑定前五年
        #region
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

        //    //选定今年
        //    ddl_year.ClearSelection();
        //    ddl_year.Items.FindByValue(getYear()).Selected = true;

        //    //选的本月
        //    ddl_month.ClearSelection();
        //    ddl_month.Items.FindByValue(getMonth()).Selected = true;

        //}
        #endregion

        private void getData()
        {           
            string startime = string.Empty;
            string endtime = string.Empty;           
            if (txtStartTime.Text != "")
            {
                startime = txtStartTime.Text.Trim();               
                
            }
            else
            {
                startime = System.DateTime.Now.ToString("yyyy-MM");
            }
            if (txtEndTime.Text != "")
            {
                endtime = txtEndTime.Text.Trim();
            }
            else
            {
                endtime = System.DateTime.Now.ToString("yyyy-MM");
            }
            
            string SubCondtion = GetSubCondtion();

            condition = "CONVERT(varchar(100),(DIFYEAR+'-'+DIFMONTH), 23)  between '" + startime + "' and '" + endtime + "'AND round(DIFAMTMNY,2)!=0";

            if (SubCondtion != "")
            {
                condition += " AND (" + SubCondtion + ")";
            }
        }
        //初始化分布信息
        private void InitPager()
        {
            pager.TableName = "View_FM_INHSDETAILDIFMAR";
            pager.PrimaryKey = "MARID";
            pager.ShowFields = "MARID,MNAME,GUIGE,CAIZHI,GB,CAST(round(DIFAMTMNY,2) AS FLOAT) AS DIFAMTMNY,DIFYEAR,DIFMONTH  ";
            pager.OrderField = "MARID";
            pager.OrderType = 0;//项目编号的降序排列
            pager.PageSize = 15;
            pager.StrWhere = condition;
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数  


            string sql = "select  CAST(round(isnull(sum(DIFAMTMNY),0),2) AS FLOAT) as TotalAmount from View_FM_INHSDETAILDIFMAR where " + condition;

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.Read())
            {
                hfdTotalAmount.Value = sdr["TotalAmount"].ToString();
                
            }

            sdr.Close();

        }

        void Pager_PageChanged(int pageNumber)
        {
            getData();

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

        protected void Query_Click(object sender, EventArgs e)
        {
            getData();

            InitPager();
            ViewState["StrShere"] = condition;

            UCPaging1.CurrentPage = 1;

            bindGrid();

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


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#Efefef'");//当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");//当鼠标移开时还原背景色---白色
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[7].Text = "合计:";
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[8].Text = string.Format("{0:c2}", Convert.ToDouble(hfdTotalAmount.Value));
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Left;
            }
        }


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
            ItemList.Add("MARID", "物料编码");
            ItemList.Add("MNAME", "物料名称");
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

            if (field == "GI_INSTOREID")
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


        protected void btn_export_Click(object sender, EventArgs e)
        {
            string sqldif = "select DIFYEAR,DIFMONTH,MARID,MNAME,GUIGE,CAIZHI,GB,CAST(round(DIFAMTMNY,2) AS FLOAT) AS DIFAMTMNY from View_FM_INHSDETAILDIFMAR where " + ViewState["StrShere"].ToString() + "";
            System.Data.DataTable dtdif = DBCallCommon.GetDTUsingSqlText(sqldif);
            string filename = "暂估补差.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("暂估补差导出模版.xls")))
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
                        row.CreateCell(j+1).SetCellValue(str);
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
    }
}
