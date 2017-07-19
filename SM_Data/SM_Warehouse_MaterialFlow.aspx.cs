using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_MaterialFlow : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {

            InitVar();
            if (!IsPostBack)
            {
                bindData();
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
            }
        }
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindData();
        }

        private void InitPager()
        {
            pager.TableName = "View_SM_MaterialFlow";
            pager.PrimaryKey = "marid";
            pager.ShowFields = "RP_TSAID,pjid,pjnm,engid,engnm,marid,marnm,margg,marcz,margb,marunit,cast(num as float) as num,cast(isnull(ordernum,0) as float) as ordernum,cast(isnull(innum,0) as float) as innum,cast(isnull(outnum,0) as float) as outnum,cast((isnull(outnum,0)-isnull(num,0))  as float) as difnum,NOTE AS Note,NOTEDATE AS NoteDate,CAST(ISNULL(LINGYONG,0) AS float) AS LingYong";
            pager.OrderField = "marid";
            pager.OrderType = 1;
            pager.StrWhere = getStrwhere();
            pager.PageSize = 30;

            GetTotalAmount(pager.StrWhere);

        }
        private void GetTotalAmount(string strWhere)
        {
            string sql = string.Empty;
            if (string.IsNullOrEmpty(strWhere))
            {
                sql = "select cast(sum(num) as float) as num,cast(sum(ordernum) as float) as ordernum,cast(sum(innum) as float) as innum,cast(sum(outnum) as float) as outnum,cast(sum(isnull(outnum,0)-isnull(num,0)) as float) as difnum from View_SM_MaterialFlow ";
            }
            else
            {
                sql = "select cast(sum(num) as float) as num,cast(sum(ordernum) as float) as ordernum,cast(sum(innum) as float) as innum,cast(sum(outnum) as float) as outnum,cast(sum(isnull(outnum,0)-isnull(num,0)) as float) as difnum from View_SM_MaterialFlow where " + strWhere;
            }
             
            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.Read())
            {
                HiddenFieldNum.Value = sdr["num"].ToString();
                HiddenFieldOrderNum.Value = sdr["ordernum"].ToString();
                HiddenFieldInNum.Value = sdr["innum"].ToString();
                HiddenFieldOutNum.Value = sdr["outnum"].ToString();
                HiddenFieldDifNum.Value = sdr["difnum"].ToString();
            }

            sdr.Close();
        }



        protected void bindData()
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
       

        protected void TextBoxSCZH_TextChanged(object sender, EventArgs e)//生产制号条件
        {
            UCPaging1.CurrentPage = 1;
            bindData();
        }
        protected void TextBoxMar_TextChanged(object sender, EventArgs e) //物料代码条件
        {

            TextBoxMar.Text = TextBoxMar.Text.Trim().Split(' ')[0];

            UCPaging1.CurrentPage = 1;
            bindData();
        }

        private string getStrwhere()
        {
            string condition = string.Empty;

            if (RadioButtonList1.SelectedValue == "1")
            {
                condition = "(" + " NOTEDATE <>'' OR NOTEDATE IS NOT NULL " +")";
            }
            else if (RadioButtonList1.SelectedValue == "2")
            {
                condition = "(" + " NOTEDATE = '' OR NOTEDATE is NULL " + ")";
            }

            if ((TextBoxSCZH.Text.Trim() != string.Empty) && (condition != string.Empty))
            {
                condition += " AND RP_TSAID like '%" + TextBoxSCZH.Text.Trim() + "%'";
            }
            else if ((TextBoxSCZH.Text.Trim() != string.Empty) && (condition == string.Empty))
            {
                condition += " RP_TSAID like '%" + TextBoxSCZH.Text.Trim() + "%'";

            }

            if ((TextBoxMar.Text.Trim()!=string.Empty)&&(condition!=string.Empty))
            {
                condition += " AND marid like '" + TextBoxMar.Text.Trim() + "%'";
            }
            else if ((TextBoxMar.Text.Trim() != string.Empty) && (condition == string.Empty))
            {
                condition = "marid like '" + TextBoxMar.Text.Trim() + "%'";
            }

            if ((TextBoxMarNM.Text.Trim() != string.Empty) && (condition != string.Empty))
            {
                condition += " AND marnm like '%" + TextBoxMarNM.Text.Trim() + "%'";
            }
            else if ((TextBoxMarNM.Text.Trim() != string.Empty) && (condition == string.Empty))
            {
                condition = "marnm like '%" + TextBoxMarNM.Text.Trim() + "%'";
            }

            if ((TextBoxGG.Text.Trim() != string.Empty) && (condition != string.Empty))
            {
                condition += " AND margg like '%" + TextBoxGG.Text.Trim() + "%'";
            }
            else if ((TextBoxGG.Text.Trim() != string.Empty) && (condition == string.Empty))
            {
                condition = "margg like '%" + TextBoxGG.Text.Trim() + "%'";
            }
            if ((TextBoxCZ.Text.Trim() != string.Empty) && (condition != string.Empty))
            {
                condition += " AND marcz like '%" + TextBoxCZ.Text.Trim() + "%'";
            }
            else if ((TextBoxCZ.Text.Trim() != string.Empty) && (condition == string.Empty))
            {
                condition = "marcz like '%" + TextBoxCZ.Text.Trim() + "%'";
            }

            if (DropDownListDifnum.SelectedValue != "0" && (condition != string.Empty))
            {
                if (DropDownListDifnum.SelectedValue == "1")
                {
                    //正常
                    condition += " AND  (isnull(outnum, 0) - isnull(num, 0)) <= 0 ";
                }
                else if (DropDownListDifnum.SelectedValue == "2")
                {
                    //超领
                    condition += " AND  (isnull(outnum, 0) - isnull(num, 0)) >0 ";
                }
                else if (DropDownListDifnum.SelectedValue == "3")
                {
                    //未支领
                    condition += " AND isnull(outnum, 0)=0 ";
                }

                else if (DropDownListDifnum.SelectedValue == "4")
                {
                    //未支领-正常
                    condition += " AND (isnull(outnum, 0)=0 or (isnull(outnum, 0) - isnull(num, 0)) <= 0)";
                }
                else if (DropDownListDifnum.SelectedValue == "5")
                {
                    //未支领-超领
                    condition += " AND (isnull(outnum, 0)=0 or (isnull(outnum, 0) - isnull(num, 0)) > 0) ";
                }
                else if (DropDownListDifnum.SelectedValue == "6")
                {
                    //正常-超领
                    condition += " AND ((isnull(outnum, 0) - isnull(num, 0)) <= 0 or (isnull(outnum, 0) - isnull(num, 0)) > 0) ";
                }



            }
            else if (DropDownListDifnum.SelectedValue != "0" && (condition == string.Empty))
            {
                if (DropDownListDifnum.SelectedValue == "1")
                {
                    //正常
                    condition += " (isnull(outnum, 0) - isnull(num, 0)) <= 0 ";
                }
                else if (DropDownListDifnum.SelectedValue == "2")
                {
                    //超领
                    condition += " (isnull(outnum, 0) - isnull(num, 0)) >0 ";
                }
                else if (DropDownListDifnum.SelectedValue == "3")
                {
                    //未支领
                    condition += " isnull(outnum, 0)=0 ";
                }
                else if (DropDownListDifnum.SelectedValue == "4")
                {
                    
                    //未支领-正常
                    condition += " (isnull(outnum, 0)=0 or (isnull(outnum, 0) - isnull(num, 0)) <= 0)";
                }
                else if (DropDownListDifnum.SelectedValue == "5")
                {
                    //未支领-超领
                    condition += " (isnull(outnum, 0)=0 or (isnull(outnum, 0) - isnull(num, 0)) > 0) ";
                }
                else if (DropDownListDifnum.SelectedValue == "6")
                {
                    //正常-超领
                    condition += "  ((isnull(outnum, 0) - isnull(num, 0)) <= 0 or (isnull(outnum, 0) - isnull(num, 0)) > 0) ";
                }

            }


            return condition;
        }
        double ly = 0;
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[e.Row.Cells.Count-5].Text != "0")
                {
                    e.Row.Cells[e.Row.Cells.Count - 5].Attributes.Add("onClick", "ShowOutModal('" + Server.UrlEncode(GridView1.DataKeys[e.Row.RowIndex]["RP_TSAID"].ToString()) + "','" + Server.UrlEncode(GridView1.DataKeys[e.Row.RowIndex]["marid"].ToString()) + "');");
                }
                if (e.Row.Cells[e.Row.Cells.Count - 6].Text != "0")
                {
                    e.Row.Cells[e.Row.Cells.Count - 6].Attributes.Add("onClick", "ShowInModal('" + Server.UrlEncode(GridView1.DataKeys[e.Row.RowIndex]["engid"].ToString()) + "','" + Server.UrlEncode(GridView1.DataKeys[e.Row.RowIndex]["marid"].ToString()) + "');");
                }
                if (Convert.ToDouble(e.Row.Cells[e.Row.Cells.Count - 5].Text) > Convert.ToDouble(e.Row.Cells[e.Row.Cells.Count - 8].Text))
                { 
                    e.Row.Cells[0].BackColor=System.Drawing.Color.Red;
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Red;
                }
                ly += Convert.ToDouble(DataBinder.Eval(e.Row.DataItem, "LingYong"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                 e.Row.Cells[e.Row.Cells.Count - 3].Text = Math.Round(ly,4).ToString(); //领用计划
                 e.Row.Cells[e.Row.Cells.Count - 4].Text = HiddenFieldDifNum.Value;
                 e.Row.Cells[e.Row.Cells.Count - 5].Text=HiddenFieldOutNum.Value;
                 e.Row.Cells[e.Row.Cells.Count - 6].Text = HiddenFieldInNum.Value;
                 e.Row.Cells[e.Row.Cells.Count - 7].Text = HiddenFieldOrderNum.Value;
                 e.Row.Cells[e.Row.Cells.Count - 8].Text = HiddenFieldNum.Value;
                 
            }

        }
        protected void SaveNote_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridView1.Rows)
            {
                string taskid = GridView1.DataKeys[gr.RowIndex].Values["RP_TSAID"].ToString();
                string marid = GridView1.DataKeys[gr.RowIndex].Values["marid"].ToString();
                string note = ((System.Web.UI.WebControls.TextBox)gr.FindControl("TextBoxNote")).Text.Trim().ToString();
                double lynum = 0;
                try {lynum = Convert.ToDouble(((System.Web.UI.WebControls.TextBox)gr.FindControl("TextBoxLingYong")).Text.Trim().ToString()); }
                catch
                {
                    lynum = 0;
                }
                string sqltext = "select NOTE,ISNULL(LINGYONG,0) AS LINGYONG from tbws_xueyongnote where TASKID='" + taskid + "' and  MARID='" + marid + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                string oldenote = "";
                double oldly = 0;
                if (dt.Rows.Count > 0)
                {
                    oldenote = dt.Rows[0]["NOTE"].ToString();
                    oldly = Convert.ToDouble(dt.Rows[0]["LINGYONG"].ToString());
                }

                if ((note != oldenote) || (lynum != oldly))
                {
                    string sql="";
                    string notedate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string sqlstr = "SELECT * FROM tbws_xueyongnote WHERE TASKID='" + taskid + "' and  MARID='" + marid + "'";
                    System.Data.DataTable  dtb = DBCallCommon.GetDTUsingSqlText(sqlstr);
                    if (dtb.Rows.Count>0)
                    {                        
                        sql = "UPDATE tbws_xueyongnote set note='" + note + "',LINGYONG=" + lynum + ",notedate='" + notedate + "' where TASKID='" + taskid + "'and MARID='" + marid + "'";

                    }                       
                    else 
                    {
                        sql = "INSERT INTO tbws_xueyongnote(TASKID,MARID,LINGYONG,NOTE,NOTEDATE)VALUES('" + taskid + "','" + marid + "'," + lynum + ",'" + note + "','" + notedate + "')";

                    } 
                  
                    DBCallCommon.ExeSqlText(sql);
                                
                }
                
            }
            string sqlstring = "DELETE FROM tbws_xueyongnote WHERE (NOTE='' OR NOTE IS NULL) AND (LINGYONG=0 OR LINGYONG IS NULL)";
            DBCallCommon.ExeSqlText(sqlstring);
            bindData();

        }

        protected void DropDownListDifnum_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindData();
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindData();
        }

        #region 排序
        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (this.SortDire == "ASC")
            {
                this.SortDire = "DESC";
            }
            else
            {
                this.SortDire = "ASC";
            }
            System.Data.DataTable dt = getDataFromGridView();

            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dv.Sort = e.SortExpression + " " + this.SortDire;
                this.GridView1.DataSource = dv;
                this.GridView1.DataBind();
            }

        }

        public string SortDire
        {
            get
            {
                if (ViewState["sortDire"] == null)
                {
                    //默认是升序
                    ViewState["sortDire"] = "ASC";
                }
                return ViewState["sortDire"].ToString();
            }
            set
            {
                ViewState["sortDire"] = value;
            }
        }

        //获取当前GridView1中的数据
        protected System.Data.DataTable getDataFromGridView()
        {
            System.Data.DataTable tb = new System.Data.DataTable();
            tb.Columns.Add("RP_TSAID", System.Type.GetType("System.String"));
            tb.Columns.Add("marid", System.Type.GetType("System.String"));
            tb.Columns.Add("marnm", System.Type.GetType("System.String"));
            tb.Columns.Add("margg", System.Type.GetType("System.String"));
            tb.Columns.Add("marcz", System.Type.GetType("System.String"));
            tb.Columns.Add("margb", System.Type.GetType("System.String"));
            tb.Columns.Add("marunit", System.Type.GetType("System.String"));
            tb.Columns.Add("num", System.Type.GetType("System.String"));
            tb.Columns.Add("ordernum", System.Type.GetType("System.String"));
            tb.Columns.Add("innum", System.Type.GetType("System.String"));
            tb.Columns.Add("outnum", System.Type.GetType("System.String"));
            tb.Columns.Add("difnum", System.Type.GetType("System.String"));
            tb.Columns.Add("LingYong", System.Type.GetType("System.String"));
            tb.Columns.Add("Note", System.Type.GetType("System.String"));
            tb.Columns.Add("NoteDate", System.Type.GetType("System.String"));
            tb.Columns.Add("engid", System.Type.GetType("System.String"));
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow GRow = GridView1.Rows[i];
                DataRow row = tb.NewRow();
                row["RP_TSAID"] = GRow.Cells[1].Text.Trim();
                row["marid"] = GRow.Cells[2].Text.Trim();
                row["marnm"] = GRow.Cells[3].Text.Trim();
                row["margg"] = GRow.Cells[4].Text.Trim();
                row["marcz"] = GRow.Cells[5].Text.Trim();
                row["margb"] = GRow.Cells[6].Text.Trim();
                row["marunit"] = GRow.Cells[7].Text.Trim();
                row["num"] = GRow.Cells[8].Text.Trim();
                row["ordernum"] = GRow.Cells[9].Text.Trim();
                row["innum"] = GRow.Cells[10].Text.Trim();
                row["outnum"] = GRow.Cells[11].Text.Trim();
                row["difnum"] = GRow.Cells[12].Text.Trim();
                row["LingYong"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("LingYong")).Text;
                row["Note"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxNote")).Text;
                row["NoteDate"] = ((System.Web.UI.WebControls.TextBox)GRow.FindControl("TextBoxDate")).Text;
                row["engid"] = ((System.Web.UI.WebControls.Label)GRow.FindControl("Labelengid")).Text;
                tb.Rows.Add(row);
            }
            return tb;
        }

        #endregion

        #region 导出

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string sqlstr = string.Empty;

            if (getStrwhere() == string.Empty)
            {
                sqlstr = "select RP_TSAID as 生产制号,marid as 物料代码,marnm as 物料名称,margg as 规格型号,marcz as 材质,margb as 国标,marunit as 单位,cast(num as float) as 需用数量,cast(isnull(ordernum,0) as float) as 订单数量,cast(isnull(innum,0) as float) as 入库数量,cast(isnull(outnum,0) as float) as 出库数量,cast( (isnull(outnum,0)-isnull(num,0)) as float) as 差额,LINGYONG AS 领用计划,NOTE as 备注,NOTEDATE as 备注时间 from View_SM_MaterialFlow  order by marid DESC";
           
            }
            else
            {
                sqlstr = "select RP_TSAID as 生产制号,marid as 物料代码,marnm as 物料名称,margg as 规格型号,marcz as 材质,margb as 国标,marunit as 单位,cast(num as float) as 需用数量,cast(isnull(ordernum,0) as float) as 订单数量,cast(isnull(innum,0) as float) as 入库数量,cast(isnull(outnum,0) as float) as 出库数量,cast( (isnull(outnum,0)-isnull(num,0)) as float) as 差额,LINGYONG AS 领用计划,NOTE as 备注,NOTEDATE as 备注时间 from View_SM_MaterialFlow where " + getStrwhere() + " order by marid DESC";
            }

            ExportExcel(sqlstr);

        }

        private void ExportExcel(string strsql)
        {
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);

            ApplicationClass ac = new ApplicationClass();
            ac.Visible = false;     // Excel不显示  
            Workbook wkbook = ac.Workbooks.Add(true);   // 添加工作簿  
            Worksheet wksheet = (Worksheet)wkbook.ActiveSheet;    // 获得工作表  

            ac.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            int rowCount = dt.Rows.Count;

            int colCount = dt.Columns.Count;

            wksheet.get_Range("A1", wksheet.Cells[1 + rowCount, colCount + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            wksheet.get_Range("A1", wksheet.Cells[1 + rowCount, colCount + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            wksheet.get_Range("A1", wksheet.Cells[1 + rowCount, colCount + 1]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            wksheet.get_Range("A1", wksheet.Cells[1 + rowCount, colCount + 1]).NumberFormatLocal = "@";

            object[,] dataArray = new object[rowCount, colCount + 1];

            for (int i = 0; i < rowCount; i++)
            {
                dataArray[i, 0] = (object)(i + 1);

                for (int j = 0; j < colCount; j++)
                {

                    dataArray[i, j + 1] = dt.Rows[i][j];

                }
            }

            //设置表头
            wksheet.Cells[1, 1] = "序号";

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                wksheet.Cells[1, i + 2] = dt.Columns[i].ColumnName;

                //实际发料张数或支数
                if (dt.Columns[i].ColumnName == "需用数量" || dt.Columns[i].ColumnName == "订单数量" || dt.Columns[i].ColumnName == "入库数量" || dt.Columns[i].ColumnName == "出库数量" || dt.Columns[i].ColumnName == "差额")
                {
                    wksheet.get_Range(getExcelColumnLabel(i + 2) + "1", wksheet.Cells[1 + rowCount, i + 2]).NumberFormatLocal = "G/通用格式";
                }
            }
            //表体
            wksheet.get_Range("A2", wksheet.Cells[1 + rowCount, colCount + 1]).Value2 = dataArray;

            //表尾


            //设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = Server.MapPath("/SM_Data/ExportFile/" + "执行计划" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            ac.Quit();
            wkbook = null;
            ac = null;
            GC.Collect();    // 强制垃圾回收  

            if (File.Exists(filename))
            {
                DownloadFile.Send(Context, filename);
            }

        }


        //将EXCEL列数转换为列字母
        private string getExcelColumnLabel(int index)
        {
            String rs = "";

            do
            {
                index--;
                rs = ((char)(index % 26 + (int)'A')) + rs;
                index = (int)((index - index % 26) / 26);
            } while (index > 0);

            return rs;
        }

        #endregion

        protected void TextBoxCZ_TextChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindData();
        }

    }
}
