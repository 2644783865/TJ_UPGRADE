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

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_CNFB : BasicPage
    {
        double myjehj = 0;
        double sjjehj = 0;

        protected void Page_Load(object sender, EventArgs e)
        {   
            if (!IsPostBack)
            {
                GETBZ();
                this.BindYearMoth(dplYear, dplMoth);
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
            CheckUser(ControlFinder);
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
            string sql = "1=1";
            if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex == 0)
            {
                sql = "1=1";
            }
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
            if (DropDownListZB.SelectedIndex != 0)
            {
                sql += " and CNFB_TYPE like '%"+DropDownListZB.SelectedValue.ToString().Trim()+"%'";
            }
            ViewState["StrWhere"] = sql;
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

        protected void btnSC_Click(object sender, EventArgs e)
        {
            CommonFun.delMult("TBMP_CNFB_LIST", "ID", rptProNumCost);
            bindGrid();
            Response.Redirect(Request.Url.ToString());
        }

        protected string editDq(string DqId)
        {
            return "javascript:window.showModalDialog('PM_CNFB_DETAILL.aspx?id=" + DqId + "','','DialogWidth=650px;DialogHeight=400px')";
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



        protected void GETBZ()
        {
            string sql = "SELECT DISTINCT CNFB_TYPE FROM TBMP_CNFB_LIST";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListZB.DataValueField = "CNFB_TYPE";
            DropDownListZB.DataTextField = "CNFB_TYPE";
            DropDownListZB.DataSource = dt;
            DropDownListZB.DataBind();
            ListItem item = new ListItem("--请选择--", "");
            DropDownListZB.Items.Insert(0, item);
        }



        protected void btn_export_Click(object sender, EventArgs e)
        {
            string condition = ViewState["StrWhere"].ToString();
            string sqljg = "select CNFB_PROJNAME,CNFB_HTID,CNFB_TSAID,CNFB_TH,CNFB_SBNAME,CNFB_NUM,CNFB_BYMYMONEY,CNFB_BYREALMONEY,CNFB_TYPE from TBMP_CNFB_LIST where CNFB_TYPE<>'喷漆喷砂' and " + condition;
            string sqlpqps = "select CNFB_PROJNAME,CNFB_HTID,CNFB_TSAID,CNFB_TH,CNFB_SBNAME,CNFB_NUM,CNFB_BYMYMONEY,CNFB_BYREALMONEY from TBMP_CNFB_LIST WHERE CNFB_TYPE='喷漆喷砂' and " + condition;
            System.Data.DataTable dtjg=DBCallCommon.GetDTUsingSqlText(sqljg);
            System.Data.DataTable dtpqps = DBCallCommon.GetDTUsingSqlText(sqlpqps);
            string filename = "厂内分包.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("厂内分包模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);//创建第一个sheet
                ISheet sheet2 = wk.GetSheetAt(1);
                if (dtjg.Rows.Count > 0)
                { 
                    for (int i = 0; i < dtjg.Rows.Count; i++)
                    {
                        IRow row = sheet1.GetRow(i + 2);
                        row = sheet1.CreateRow(i + 2);
                        row.CreateCell(0).SetCellValue(i + 1);
                        for (int j = 0; j < dtjg.Columns.Count - 3; j++)
                        {
                            row.CreateCell(j + 1).SetCellValue(dtjg.Rows[i][j].ToString());
                        }
                        for (int k = (dtjg.Columns.Count - 3); k < dtjg.Columns.Count; k++)
                        {
                            row.CreateCell(k + 5).SetCellValue(dtjg.Rows[i][k].ToString());
                        }

                    }
                    for (int r = 0; r <= dtjg.Columns.Count + 8; r++)
                    {
                        sheet1.AutoSizeColumn(r);
                    }
                }
                if (dtpqps.Rows.Count > 0)
                {
                    for (int i = 0; i < dtpqps.Rows.Count; i++)
                    {
                        IRow row = sheet2.GetRow(i + 2);
                        row = sheet2.CreateRow(i + 2);
                        row.CreateCell(0).SetCellValue(i + 1);
                        for (int j = 0; j < dtpqps.Columns.Count - 2; j++)
                        {
                            row.CreateCell(j + 1).SetCellValue(dtpqps.Rows[i][j].ToString());
                        }
                        row.CreateCell(10).SetCellValue(dtpqps.Rows[i]["CNFB_BYMYMONEY"].ToString());
                        row.CreateCell(12).SetCellValue(dtpqps.Rows[i]["CNFB_BYREALMONEY"].ToString());
                    }
                    for (int r = 0; r <= dtpqps.Columns.Count + 8; r++)
                    {
                        sheet2.AutoSizeColumn(r);
                    }
                }

                sheet1.ForceFormulaRecalculation = true;
                sheet2.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }

        }












        protected void btn_import_Click(object sender, EventArgs e)
        {
            if (tbyear.Text.ToString() == "" || tbmonth.Text.ToString() == "")
            {
                Response.Write("<script>alert('请填写数据对应的年月！');</script>"); return;
            }
            List<string> list = new List<string>();
            string FilePath = @"E:\厂内分包表\";
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

                //获取第一个工作表
                ISheet sheet1 = wk.GetSheetAt(0);
                ISheet sheet2 = wk.GetSheetAt(1);
                

                //循环读取每一行数据，由于execel有列名以及序号，从1开始
                string cnfbyear = tbyear.Text.ToString();
                string cnfbmonth = tbmonth.Text.ToString();

                string sqlTextchk = "select * from TBMP_CNFB_LIST where CNFB_YEAR='"+cnfbyear+"' and CNFB_MONTH='"+cnfbmonth+"'";
                System.Data.DataTable dtchk = DBCallCommon.GetDTUsingSqlText(sqlTextchk);
                if (dtchk.Rows.Count > 0)
                {
                    Response.Write("<script>alert('该月数据已导入，若想重新导入，请删除原有数据！');</script>"); return;
                }
                //结构车间
                for (int i = 2; i < sheet1.LastRowNum+1; i++)
                {
                    string sql1 = "";
                    IRow row1 = sheet1.GetRow(i);
                    ICell cell0 = row1.GetCell(0);
                    if (cell0.NumericCellValue>0)
                    {
                        ICell cell1 = row1.GetCell(1);
                        ICell cell2 = row1.GetCell(2);
                        ICell cell3 = row1.GetCell(3);
                        ICell cell4 = row1.GetCell(4);
                        ICell cell5 = row1.GetCell(5);
                        //ICell cell6 = row1.GetCell(6);
                        ICell cell11 = row1.GetCell(11);
                        ICell cell12 = row1.GetCell(12);
                        ICell cell13 = row1.GetCell(13);
                        if (cell1 == null)
                        {
                            sql1 += "'" + "" + "',";
                        }
                        else
                        {
                            sql1 += "'" + cell1.ToString().Trim() + "',";
                        }
                        if (cell2 == null)
                        {
                            sql1 += "'" + "" + "',";
                        }
                        else
                        {
                            sql1 += "'" + cell2.ToString().Trim() + "',";
                        }
                        if (cell3 == null)
                        {
                            sql1 += "'" + "" + "',";
                        }
                        else
                        {
                            sql1 += "'" + cell3.ToString().Trim() + "',";
                        }
                        if (cell4 == null)
                        {
                            sql1 += "'" + "" + "',";
                        }
                        else
                        {
                            string abcde = "";
                            try
                            {
                                sql1 += "'" + cell4.ToString().Trim() + "',";
                            }
                            catch
                            {
                                sql1 += "'" + cell4.NumericCellValue.ToString().Trim() + "',";
                            }
                        }
                        if (cell5 == null)
                        {
                            sql1 += "'" + "" + "',";
                        }
                        else
                        {
                            sql1 += "'" + cell5.ToString().Trim() + "',";
                        }
                        //sql1 += "'" + Convert.ToDouble(cell6.NumericCellValue.ToString()) + "',";
                        sql1 += "'" + Convert.ToDouble(cell11.NumericCellValue.ToString().Trim()) + "',";
                        sql1 += "'" + Convert.ToDouble(cell12.NumericCellValue.ToString().Trim()) + "',";
                        sql1 += "'" + cell13.ToString().Trim() + "',";
                        sql1 += "'" + cnfbyear.ToString().Trim() + "',";
                        sql1 += "'" + cnfbmonth.ToString().Trim() + "',GETDATE()";
                        string sqltext1 = "insert into TBMP_CNFB_LIST(CNFB_PROJNAME,CNFB_HTID,CNFB_TSAID,CNFB_TH,CNFB_SBNAME,CNFB_BYMYMONEY,CNFB_BYREALMONEY,CNFB_TYPE,CNFB_YEAR,CNFB_MONTH,CNFB_DRTIME) values(" + sql1 + ")";
                        list.Add(sqltext1);
                    }
                    else
                    {
                        break;
                    }
                }
                //喷漆喷砂
                for (int j = 2; j < sheet2.LastRowNum; j++)
                {
                    string sql2 = "";
                    IRow row2 = sheet2.GetRow(j);
                    ICell cell0 = row2.GetCell(0);
                    if (cell0.NumericCellValue>0)
                    {
                        ICell cell1 = row2.GetCell(1);
                        ICell cell2 = row2.GetCell(2);
                        ICell cell3 = row2.GetCell(3);
                        ICell cell4 = row2.GetCell(4);
                        ICell cell5 = row2.GetCell(5);
                        //ICell cell6 = row2.GetCell(6);
                        ICell cell10 = row2.GetCell(10);
                        ICell cell12 = row2.GetCell(12);
                        string zb = "喷漆喷砂";
                        if (cell1 == null)
                        {
                            sql2 += "'" + "" + "',";
                        }
                        else
                        {
                            sql2 += "'" + cell1.ToString().Trim() + "',";
                        }
                        if (cell2 == null)
                        {
                            sql2 += "'" + "" + "',";
                        }
                        else
                        {
                            sql2 += "'" + cell2.ToString().Trim() + "',";
                        }
                        if (cell3 == null)
                        {
                            sql2 += "'" + "" + "',";
                        }
                        else
                        {
                            sql2 += "'" + cell3.ToString().Trim() + "',";
                        }
                        if (cell4 == null)
                        {
                            sql2 += "'" + "" + "',";
                        }
                        else
                        {
                            sql2 += "'" + cell4.ToString().Trim() + "',";
                        }
                        if (cell5 == null)
                        {
                            sql2 += "'" + "" + "',";
                        }
                        else
                        {
                            sql2 += "'" + cell5.ToString().Trim() + "',";
                        }
                        //sql2 += "'" + Convert.ToDouble(cell6.NumericCellValue.ToString()) + "',";
                        sql2 += "'" + Convert.ToDouble(cell10.NumericCellValue.ToString().Trim()) + "',";
                        sql2 += "'" + Convert.ToDouble(cell12.NumericCellValue.ToString().Trim()) + "',";
                        sql2 += "'" + zb.ToString().Trim() + "',";
                        sql2 += "'" + cnfbyear.ToString().Trim() + "',";
                        sql2 += "'" + cnfbmonth.ToString().Trim() + "',GETDATE()";
                        string sqltext2 = "insert into TBMP_CNFB_LIST(CNFB_PROJNAME,CNFB_HTID,CNFB_TSAID,CNFB_TH,CNFB_SBNAME,CNFB_BYMYMONEY,CNFB_BYREALMONEY,CNFB_TYPE,CNFB_YEAR,CNFB_MONTH,CNFB_DRTIME) values(" + sql2 + ")";
                        list.Add(sqltext2);
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
