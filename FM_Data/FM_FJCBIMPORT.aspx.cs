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
    public partial class FM_FJCBIMPORT : System.Web.UI.Page
    {
        double yqylzj = 0;
        double yqhsjezj = 0;
        double xsjylzj=0;
        double xsjhsjezj=0;
        double hsjezj=0;
        double bhsjezj = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                this.BindYearMoth(Dpyear, Dpmonth);
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                ViewState["StrWhere"] = "1=1";
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
            Dpyear.SelectedIndex = 0;
            Dpmonth.SelectedIndex = 0;
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
            pager.TableName = "FM_FJCB";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "ID,FJCB_TSAID,FJCB_YQZL,FJCB_YQYL,FJCB_YQHSDJ,FJCB_YQHSJE,FJCB_XSJYL,FJCB_XSJHSDJ,FJCB_XSJHSJE,FJCB_HJHSJE,FJCB_HJBHSJE,(FJCB_YEAR+'-'+FJCB_MONTH) as FJCB_YEARMONTH";
            pager.OrderField = "FJCB_TSAID";
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
            string sql = "1=1";
            if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex == 0)
            {
                sql = "1=1";
            }
            else if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex == 0)
            {
                sql += " and FJCB_YEAR like '%" + dplYear.SelectedValue.ToString().Trim() + "%'";
            }
            else if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex != 0)
            {
                sql += " and FJCB_MONTH like '%" + dplMoth.SelectedValue.ToString().Trim() + "%'";
            }
            else
            {
                sql += " and FJCB_YEAR like '%" + dplYear.SelectedValue.ToString().Trim() + "%' and FJCB_MONTH like '%" + dplMoth.SelectedValue.ToString().Trim() + "%'";
            }
            if (txtrwh.Text != "")
            {
                sql += " and FJCB_TSAID like '%" + txtrwh.Text.ToString().Trim() + "%'";
            }
            if (tbzl.Text != "")
            {
                sql += " and FJCB_YQZL like '%" + tbzl.Text.ToString().Trim() + "%'";
            }
            ViewState["StrWhere"] = sql;
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

        protected void btnSC_Click(object sender, EventArgs e)
        {
            CommonFun.delMult("FM_FJCB", "ID", rptProNumCost);
            bindGrid();
            Response.Redirect(Request.Url.ToString());
        }

        protected string editDq(string DqId)
        {
            return "javascript:window.showModalDialog('FM_FJCB_DETAILL.aspx?id=" + DqId + "','','DialogWidth=650px;DialogHeight=400px')";
        }



        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Label lbrwh = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbrwh");
                System.Web.UI.WebControls.Label lbyqzl = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbyqzl");
                System.Web.UI.WebControls.Label lbyqyl = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbyqyl");
                System.Web.UI.WebControls.Label lbyqhsdj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbyqhsdj");
                System.Web.UI.WebControls.Label lbyqhsje = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbyqhsje");
                System.Web.UI.WebControls.Label lbxsjyl = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbxsjyl");
                System.Web.UI.WebControls.Label lbxsjhsdj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbxsjhsdj");
                System.Web.UI.WebControls.Label lbxsjhsje = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbxsjhsje");
                System.Web.UI.WebControls.Label lbhjhsje = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbhjhsje");
                System.Web.UI.WebControls.Label lbhjbhsje = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbhjbhsje");
                System.Web.UI.WebControls.Label lbny = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbny");
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select isnull(CAST(sum(FJCB_YQYL) AS FLOAT),0) as FJCB_YQYLZJ,isnull(CAST(sum(FJCB_YQHSJE) AS FLOAT),0) as FJCB_YQHSJEZJ,isnull(CAST(sum(FJCB_XSJYL) AS FLOAT),0) as FJCB_XSJYLZJ,isnull(CAST(sum(FJCB_XSJHSJE) AS FLOAT),0) as FJCB_XSJHSJEZJ,isnull(CAST(sum(FJCB_HJHSJE) AS FLOAT),0) as FJCB_HJHSJEZJ,isnull(CAST(sum(FJCB_HJBHSJE) AS FLOAT),0) as FJCB_HJBHSJEZJ from FM_FJCB where " + ViewState["StrWhere"].ToString();

                SqlDataReader drzj = DBCallCommon.GetDRUsingSqlText(sqlhj);
                if (drzj.Read())
                {
                    yqylzj = Convert.ToDouble(drzj["FJCB_YQYLZJ"]);
                    yqhsjezj = Convert.ToDouble(drzj["FJCB_YQHSJEZJ"]);

                    xsjylzj = Convert.ToDouble(drzj["FJCB_XSJYLZJ"]);
                    xsjhsjezj = Convert.ToDouble(drzj["FJCB_XSJHSJEZJ"]);
                    hsjezj = Convert.ToDouble(drzj["FJCB_HJHSJEZJ"]);
                    bhsjezj = Convert.ToDouble(drzj["FJCB_HJBHSJEZJ"]);
                }
                drzj.Close();
                System.Web.UI.WebControls.Label lbyqylzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbyqylzj");
                System.Web.UI.WebControls.Label lbyqhsjezj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbyqhsjezj");
                System.Web.UI.WebControls.Label lbxsjylzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbxsjylzj");
                System.Web.UI.WebControls.Label lbxsjhsjezj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbxsjhsjezj");
                System.Web.UI.WebControls.Label lbhsjezj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbhsjezj");
                System.Web.UI.WebControls.Label lbbhsjezj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbbhsjezj");
                lbyqylzj.Text = yqylzj.ToString();
                lbyqhsjezj.Text = yqhsjezj.ToString();
                lbxsjylzj.Text = xsjylzj.ToString();
                lbxsjhsjezj.Text = xsjhsjezj.ToString();
                lbhsjezj.Text = hsjezj.ToString();
                lbbhsjezj.Text = bhsjezj.ToString();

            }
        }









        protected void btn_export_Click(object sender, EventArgs e)
        {
            string condition = ViewState["StrWhere"].ToString();
            string sqlfjcb = "select FJCB_TSAID,FJCB_YQZL,FJCB_YQYL,FJCB_YQHSDJ,FJCB_YQHSJE,FJCB_XSJYL,FJCB_XSJHSDJ,FJCB_XSJHSJE,FJCB_HJHSJE,FJCB_HJBHSJE from FM_FJCB where " + condition;
            System.Data.DataTable dtfjcb = DBCallCommon.GetDTUsingSqlText(sqlfjcb);
            string filename = "分交成本.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("分交成本导出模版.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < dtfjcb.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);

                    for (int j = 0; j < dtfjcb.Columns.Count; j++)
                    {
                        string str = dtfjcb.Rows[i][j].ToString();
                        row.CreateCell(j).SetCellValue(str);
                    }

                }
                for (int r = 0; r <= dtfjcb.Columns.Count; r++)
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












        protected void btn_import_Click(object sender, EventArgs e)
        {
            if (Dpyear.SelectedIndex == 0 || Dpmonth.SelectedIndex == 0)
            {
                Response.Write("<script>alert('请填写分交成本的年月！');</script>"); return;
            }
            List<string> list = new List<string>();
            string FilePath = @"E:\分交成本表\";
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            //将文件上传到服务器
            HttpPostedFile UserHPF = FileUpload1.PostedFile;
            try
            {
                string fileContentType = UserHPF.ContentType;// 获取客户端发送的文件的 MIME 内容类型   
                if (fileContentType == "application/vnd.ms-excel")
                {
                    if (UserHPF.ContentLength > 0)
                    {
                        UserHPF.SaveAs(FilePath + "//" + System.IO.Path.GetFileName(UserHPF.FileName));//将上传的文件存放在指定的文件夹中 
                    }
                }
                else
                {
                    Response.Write("<script>alert('文件类型不符合要求，请您核对后重新上传！');</script>"); return;
                }
            }
            catch
            {
                Response.Write("<script>alert('文件上传过程中出现错误！');</script>"); return;
            }

            using (FileStream fs = File.OpenRead(FilePath + "//" + System.IO.Path.GetFileName(UserHPF.FileName)))
            {
                //根据文件流创建一个workbook
                IWorkbook wk = new HSSFWorkbook(fs);

                ISheet sheet1 = wk.GetSheetAt(0);

                //循环读取每一行数据，由于execel有列名以及序号，从1开始
                string year =Dpyear.SelectedValue.ToString();
                string month = Dpmonth.SelectedValue.ToString();

                string sqlTextchk = "select * from FM_FJCB where FJCB_YEAR='" + year + "' and FJCB_MONTH='" + month + "'";
                System.Data.DataTable dtchk = DBCallCommon.GetDTUsingSqlText(sqlTextchk);
                if (dtchk.Rows.Count > 0)
                {
                    Response.Write("<script>alert('该月数据已导入，若想重新导入，请删除原有数据！');</script>"); return;
                }
                //结构车间
                for (int i = 1; i < sheet1.LastRowNum+1; i++)
                {
                    string sql = "";
                    IRow row = sheet1.GetRow(i);
                    ICell cell00 = row.GetCell(2);
                    if (Convert.ToDouble(cell00.NumericCellValue.ToString()) > 0)
                    {
                        ICell cell0 = row.GetCell(0);
                        ICell cell1 = row.GetCell(1);
                        ICell cell2 = row.GetCell(2);
                        ICell cell3 = row.GetCell(3);
                        //ICell cell4 = row.GetCell(4);
                        ICell cell5 = row.GetCell(5);
                        ICell cell6 = row.GetCell(6);
                        //ICell cell7 = row.GetCell(7);
                        //ICell cell8 = row.GetCell(8);
                        //ICell cell9 = row.GetCell(9);
                        sql += "'" + cell0.StringCellValue.ToString() + "',";
                        sql += "'" + cell1.StringCellValue.ToString() + "',";
                        sql += "'" + Convert.ToDouble(cell2.NumericCellValue.ToString()) + "',";
                        sql += "'" + Convert.ToDouble(cell3.NumericCellValue.ToString()) + "',";
                        sql += "'" + ((Convert.ToDouble(cell2.NumericCellValue.ToString())) * (Convert.ToDouble(cell3.NumericCellValue.ToString()))) + "',";
                        sql += "'" + Convert.ToDouble(cell5.NumericCellValue.ToString()) + "',";
                        sql += "'" + Convert.ToDouble(cell6.NumericCellValue.ToString()) + "',";
                        sql += "'" + ((Convert.ToDouble(cell5.NumericCellValue.ToString())) * (Convert.ToDouble(cell6.NumericCellValue.ToString()))) + "',";
                        sql += "'" + (((Convert.ToDouble(cell5.NumericCellValue.ToString())) * (Convert.ToDouble(cell6.NumericCellValue.ToString()))) + ((Convert.ToDouble(cell2.NumericCellValue.ToString())) * (Convert.ToDouble(cell3.NumericCellValue.ToString())))) + "',";
                        sql += "'" + Math.Round(((((Convert.ToDouble(cell5.NumericCellValue.ToString())) * (Convert.ToDouble(cell6.NumericCellValue.ToString()))) + ((Convert.ToDouble(cell2.NumericCellValue.ToString())) * (Convert.ToDouble(cell3.NumericCellValue.ToString()))))/1.17),2) + "',";
                        sql += "'" + year.ToString() + "',";
                        sql += "'" + month.ToString() + "'";
                        string sqltext = "insert into FM_FJCB(FJCB_TSAID,FJCB_YQZL,FJCB_YQYL,FJCB_YQHSDJ,FJCB_YQHSJE,FJCB_XSJYL,FJCB_XSJHSDJ,FJCB_XSJHSJE,FJCB_HJHSJE,FJCB_HJBHSJE,FJCB_YEAR,FJCB_MONTH) values(" + sql + ")";
                        list.Add(sqltext);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            DBCallCommon.ExecuteTrans(list);

            foreach (string fileName in Directory.GetFiles(FilePath))//清空该文件夹下的文件
            {
                string newName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                System.IO.File.Delete(FilePath + "\\" + newName);//删除文件下储存的文件
            }
            bindGrid();
            Response.Redirect(Request.Url.ToString());
        }
    }
}
