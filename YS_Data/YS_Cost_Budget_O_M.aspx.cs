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
using System.IO;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Collections.Generic;


namespace ZCZJ_DPF.YS_Data
{
    public partial class YS_Cost_Budget_O_M : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bind_ddl();//绑定项目名称和工程名称的下拉框
                InitVar();
                GetTechRequireData();
            }
            InitVar();
        }

        protected void bind_ddl()
        {
            string sqltext_PJ = "SELECT DISTINCT PCON_PJNAME ,PCON_PJNAME FROM View_YS_COST_BUDGET_REAL where 1=1";
            string sqltext_ENG = "SELECT DISTINCT PCON_ENGNAME,PCON_ENGNAME FROM View_YS_COST_BUDGET_REAL where 1=1";
            DBCallCommon.FillDroplist(ddl_project, sqltext_PJ);
            DBCallCommon.FillDroplist(ddl_engineer, sqltext_ENG);
           
        }

        protected void GridView1_onrowdatabound(object sender, GridViewRowEventArgs e)
        {
            String controlId = ((GridView)sender).ClientID;
            String uniqueId = "";
            string[] fathername = { "MAR_AMOUNT", "FERROUS_METAL", "PURCHASE_PART", "MACHINING_PART", "PAINT_COATING", "ELECTRICAL", "OTHERMAT_COST" };
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                uniqueId = String.Format("{0}{1}", controlId, e.Row.RowIndex);
                e.Row.Attributes.Add("onclick", String.Format("SelectRow('{0}', this);", uniqueId));
                //双击合同号，查看合同详细信息
                string lbl_CONTRACT_NO = ((System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_YS_CONTRACT_NO")).Text.ToString();
                string lbl_pcon_sch = ((System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_pcon_sch")).Text.ToString();
                //e.Row.Cells[1].Attributes.Add("ondblclick", "ShowContract('" + lbl_CONTRACT_NO + "')");//第二格，即合同号上添加双击事件
                //e.Row.Cells[1].Attributes.Add("title", "双击关联合同信息！");

                Encrypt_Decrypt ed = new Encrypt_Decrypt();
                string CONTRACT_NO = ed.EncryptText(lbl_CONTRACT_NO);
                string PCON_SCH = ed.EncryptText(lbl_pcon_sch);
                //红黄预警
                for (int i = 0; i < fathername.Length; i++)
                {
                    string per = ((HiddenField)e.Row.FindControl("hidden_" + fathername[i])).Value.ToString();
                    double percent_O_B = Convert.ToDouble(((HiddenField)e.Row.FindControl("hidden_" + fathername[i])).Value.ToString());
                    if (percent_O_B > 0.9 && percent_O_B < 1.0)
                    {
                        e.Row.Cells[i + 5].BackColor = System.Drawing.Color.PeachPuff;
                    }
                    if (percent_O_B > 1.0)
                    {
                        e.Row.Cells[i + 5].BackColor = System.Drawing.Color.Yellow;
                    }
                }
                //双击查看详细
                for (int j = 5; j < 12; j++)
                {
                    double percent_O_B = Convert.ToDouble(((HiddenField)e.Row.FindControl("hidden_" + fathername[j - 5])).Value.ToString());//订单完成百分比
                    double db_Budget = Math.Round(Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Row.FindControl("lab_" + fathername[j - 5])).Text.ToString()) / 10000, 1);//预算费用
                    double db_Order = Math.Round(Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Row.FindControl("lab_" + fathername[j - 5] + "_O")).Text.ToString()) / 10000, 1);//订单费用
                    double db_pass = Math.Round(db_Order - db_Budget, 1);
                    if (j < 6)
                    {
                        if (db_pass > 0)
                        {
                            //e.Row.Cells[j].Attributes.Add("ondblclick", "PurMarView_Amount('" + PCON_SCH + "')");
                            e.Row.Cells[j].Attributes["style"] = "Cursor:hand";
                            e.Row.Cells[j].Attributes.Add("title", "订单达到" + db_Order.ToString() + "万,达预算" + (100 * percent_O_B).ToString() + "%,超额" + db_pass.ToString() + "万，双击查看材料总额明细");
                        }
                        else
                        {
                            //e.Row.Cells[j].Attributes.Add("ondblclick", "PurMarView_Amount('" + PCON_SCH + "')");
                            e.Row.Cells[j].Attributes["style"] = "Cursor:hand";
                            e.Row.Cells[j].Attributes.Add("title", "订单达到" + db_Order.ToString() + "万,达预算" + (100 * percent_O_B).ToString() + "%,未超额，双击查看材料总额明细");
                        }

                    }
                    else
                    {
                        //e.Row.Cells[j].Attributes.Add("ondblclick", "PurMarView('" + PCON_SCH + "','" + ed.EncryptText(fathername[j - 5]) + "')");
                        e.Row.Cells[j].Attributes["style"] = "Cursor:hand";
                        if (db_pass > 0)
                        {
                            e.Row.Cells[j].Attributes.Add("title", "订单达到" + db_Order.ToString() + "万，达预算" + (100 * percent_O_B).ToString() + "%,超额" + db_pass.ToString() + "万");
                        }
                        else
                        {
                            db_pass = Math.Abs(db_pass);
                            e.Row.Cells[j].Attributes.Add("title", "订单达到" + db_Order.ToString() + "万，达预算" + (100 * percent_O_B).ToString() + "%,未超额");
                        }
                    }
                }
            }
        }

        //查询,下拉框的选择时间

        protected void btn_search_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            GetTechRequireData();
        }

        //初始化分页信息
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetTechRequireData();
        }

        protected void GetTechRequireData()
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

        //初始化分页信息
        private void InitPager()
        {
            pager.TableName = "View_YS_COST_BUDGET_ORDER";
            pager.PrimaryKey = "YS_CONTRACT_NO";
            pager.ShowFields = "YS_CONTRACT_NO,PCON_SCH,PCON_PJNAME,PCON_ENGNAME," +
                "Convert(decimal(16,2),(YS_FERROUS_METAL+YS_PURCHASE_PART+YS_MACHINING_PART+YS_PAINT_COATING+YS_ELECTRICAL+YS_OTHERMAT_COST)/((YS_FERROUS_METAL_BG+YS_PURCHASE_PART_BG+YS_MACHINING_PART_BG+YS_PAINT_COATING_BG+YS_ELECTRICAL_BG+YS_OTHERMAT_COST_BG)*1.17+1)) AS YS_MAR_AMOUNT_B_BG_hide_percent," +
                "Convert(decimal(16,2),(YS_FERROUS_METAL_BG+YS_PURCHASE_PART_BG+YS_MACHINING_PART_BG+YS_PAINT_COATING_BG+YS_ELECTRICAL_BG+YS_OTHERMAT_COST_BG)*1.17) AS YS_MAR_AMOUNT_B_BG," +
                "Convert(decimal(16,2),(YS_FERROUS_METAL+YS_PURCHASE_PART+YS_MACHINING_PART+YS_PAINT_COATING+YS_ELECTRICAL+YS_OTHERMAT_COST)/((YS_FERROUS_METAL_BG+YS_PURCHASE_PART_BG+YS_MACHINING_PART_BG+YS_PAINT_COATING_BG+YS_ELECTRICAL_BG+YS_OTHERMAT_COST_BG)*1.17+1)) AS YS_MAR_AMOUNT_B_BG_percent," +
                "Convert(decimal(16,2),(YS_FERROUS_METAL+YS_PURCHASE_PART+YS_MACHINING_PART+YS_PAINT_COATING+YS_ELECTRICAL+YS_OTHERMAT_COST)/((YS_FERROUS_METAL_BG+YS_PURCHASE_PART_BG+YS_MACHINING_PART_BG+YS_PAINT_COATING_BG+YS_ELECTRICAL_BG+YS_OTHERMAT_COST_BG)*1.17+1)) AS YS_MAR_AMOUNT_O_BG_hide_percent," +
                "(YS_FERROUS_METAL+YS_PURCHASE_PART+YS_MACHINING_PART+YS_PAINT_COATING+YS_ELECTRICAL+YS_OTHERMAT_COST) AS YS_MAR_AMOUNT_O_BG," +
                "Convert(decimal(16,2),(YS_FERROUS_METAL+YS_PURCHASE_PART+YS_MACHINING_PART+YS_PAINT_COATING+YS_ELECTRICAL+YS_OTHERMAT_COST)/((YS_FERROUS_METAL_BG+YS_PURCHASE_PART_BG+YS_MACHINING_PART_BG+YS_PAINT_COATING_BG+YS_ELECTRICAL_BG+YS_OTHERMAT_COST_BG)*1.17+1)) AS YS_MAR_AMOUNT_O_BG_percent," +
                
                "Convert(decimal(16,2), (YS_FERROUS_METAL/(YS_FERROUS_METAL_BG*1.17+1))) as YS_FERROUS_METAL_BG_percent,Convert(decimal(16,2), (YS_FERROUS_METAL/(YS_FERROUS_METAL_BG*1.17+1))) as YS_FERROUS_METAL_BG_hide_percent,YS_FERROUS_METAL_BG*1.17 as YS_FERROUS_METAL_BG," +
                "Convert(decimal(16,2), (YS_PURCHASE_PART/(YS_PURCHASE_PART_BG*1.17+1))) as YS_PURCHASE_PART_BG_percent,Convert(decimal(16,2), (YS_PURCHASE_PART/(YS_PURCHASE_PART_BG*1.17+1))) as YS_PURCHASE_PART_BG_hide_percent,YS_PURCHASE_PART_BG*1.17 as YS_PURCHASE_PART_BG," +
                "Convert(decimal(16,2), (YS_MACHINING_PART/(YS_MACHINING_PART_BG*1.17+1))) as YS_MACHINING_PART_BG_percent,Convert(decimal(16,2), (YS_MACHINING_PART/(YS_MACHINING_PART_BG*1.17+1))) as YS_MACHINING_PART_BG_hide_percent,YS_MACHINING_PART_BG*1.17 as YS_MACHINING_PART_BG," +
                "Convert(decimal(16,2), (YS_PAINT_COATING/(YS_PAINT_COATING_BG*1.17+1))) as YS_PAINT_COATING_BG_percent,Convert(decimal(16,2), (YS_PAINT_COATING/(YS_PAINT_COATING_BG*1.17+1))) as YS_PAINT_COATING_BG_hide_percent,YS_PAINT_COATING_BG*1.17 as YS_PAINT_COATING_BG," +
                "Convert(decimal(16,2), (YS_ELECTRICAL/(YS_ELECTRICAL_BG*1.17+1))) as YS_ELECTRICAL_BG_percent,Convert(decimal(16,2), (YS_ELECTRICAL/(YS_ELECTRICAL_BG*1.17+1))) as YS_ELECTRICAL_BG_hide_percent,YS_ELECTRICAL_BG*1.17 as YS_ELECTRICAL_BG," +
                "Convert(decimal(16,2), (YS_OTHERMAT_COST/(YS_OTHERMAT_COST_BG*1.17+1))) as YS_OTHERMAT_COST_BG_percent,Convert(decimal(16,2), (YS_OTHERMAT_COST/(YS_OTHERMAT_COST_BG*1.17+1))) as YS_OTHERMAT_COST_BG_hide_percent,YS_OTHERMAT_COST_BG*1.17 as YS_OTHERMAT_COST_BG," +
                "YS_FERROUS_METAL,YS_PURCHASE_PART,YS_MACHINING_PART,YS_PAINT_COATING,YS_ELECTRICAL,YS_OTHERMAT_COST";
            pager.OrderType = 0;
            pager.StrWhere = this.GetStrWhere();
            pager.OrderField = "YS_CONTRACT_NO";
            pager.PageSize = 10;
        }

        protected string GetStrWhere()
        {
            string strwhere = " 1=1";
            strwhere += " and PCON_SCH like '%" + txt_search.Text.ToString() + "%' and YS_XS_Finished is NULL  and YS_REVSTATE='2'";
            if (ddl_project.SelectedIndex != 0)//项目名称
            {
                strwhere += " and PCON_PJNAME='" + ddl_project.SelectedValue + "'";
            }
            if (ddl_engineer.SelectedIndex != 0)//工程名称
            {
                strwhere += " and PCON_ENGNAME='" + ddl_engineer.SelectedValue + "'";
            }
            return strwhere;
        }

        protected double GetIMGWidth(double IMG_width)
        {
            double width_img = 0;
            if (IMG_width > 1)
            {
                width_img = 100;
            }
            else if (IMG_width == 0)
            {
                width_img = 0;
            }
            else
            {
                width_img = IMG_width * 100;
            }
            return width_img;
        }

        protected double GetIMGWidth_YS_AMOUNT(double IMG_width)
        {
            double width_img = 100;
            if (IMG_width > 1)
            {
                width_img = 0;
            }
            else if (IMG_width == 0)
            {
                width_img = 100;
            }
            else
            {
                width_img = 100 - IMG_width * 100;
            }
            return width_img;
        }

        protected void Btn_update_OnClick(object sender, EventArgs e)
        {
            try
            {
                string sql = DBCallCommon.GetStringValue("connectionStrings");
                sql += "Asynchronous Processing=true;";
                SqlConnection sqlConn = new SqlConnection(sql);
                sqlConn.Open();
                SqlCommand sqlCmd = new SqlCommand("YS_COST_ORDER_PROCEDURE", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;			//增加返回值参数@retVal
                IAsyncResult result = sqlCmd.BeginExecuteNonQuery();
                sqlCmd.EndExecuteNonQuery(result);
                sqlConn.Close();
                if (Convert.ToInt32(sqlCmd.Parameters["@retVal"].Value) == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('更新成功！！！');", true);
                    UCPaging1.CurrentPage = 1;
                    InitVar();
                    GetTechRequireData();
                    //lab_updatetime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region 导出EXCEL
        protected void btn_daochu_Click(object sender, EventArgs e)
        {
            string str_where = GetStrWhere();
            string sqltext = "";
            sqltext = "select YS_CONTRACT_NO,PCON_SCH,PCON_PJNAME,PCON_ENGNAME," +
                "Convert(decimal(16,2),(YS_FERROUS_METAL_BG+YS_PURCHASE_PART_BG+YS_MACHINING_PART_BG+YS_PAINT_COATING_BG+YS_ELECTRICAL_BG+YS_OTHERMAT_COST_BG)*1.17) AS YS_MAR_AMOUNT_BG," +
                "YS_MAR_AMOUNT," +
                "YS_FERROUS_METAL_BG*1.17 as YS_FERROUS_METAL_BG,YS_PURCHASE_PART_BG*1.17 as YS_PURCHASE_PART_BG,YS_MACHINING_PART_BG*1.17 as YS_MACHINING_PART_BG,YS_PAINT_COATING_BG*1.17 as YS_PAINT_COATING_BG,YS_ELECTRICAL_BG*1.17 as YS_ELECTRICAL_BG,YS_OTHERMAT_COST_BG*1.17 as YS_OTHERMAT_COST_BG,YS_FERROUS_METAL,YS_PURCHASE_PART,YS_MACHINING_PART,YS_PAINT_COATING,YS_ELECTRICAL,YS_OTHERMAT_COST from View_YS_COST_BUDGET_ORDER";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }

        private void ExportDataItem(System.Data.DataTable objdt)
        {
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("订单监控表") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            System.Data.DataTable dt = objdt;

            // 填充数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                wksheet.Cells[i + 3, 1] = Convert.ToString(i + 1);//序号

                wksheet.Cells[i + 3, 2] = "'" + dt.Rows[i]["YS_CONTRACT_NO"].ToString();//合同号

                wksheet.Cells[i + 3, 3] = "'" + dt.Rows[i]["PCON_SCH"].ToString();//合同号

                wksheet.Cells[i + 3, 4] = "'" + dt.Rows[i]["PCON_PJNAME"].ToString();//项目名称

                wksheet.Cells[i + 3, 5] = "'" + dt.Rows[i]["PCON_ENGNAME"].ToString();//工程名称

                wksheet.Cells[i + 3, 6] = "'" + dt.Rows[i]["YS_MAR_AMOUNT_BG"].ToString();//材料预算总额

                wksheet.Cells[i + 3, 7] = "'" + dt.Rows[i]["YS_MAR_AMOUNT"].ToString();//材料订单总额

                //wksheet.Cells[i + 3, 7] = "'" + dt.Rows[i]["YS_OUT_LAB_MAR_BG"].ToString();//技术外协-预

                wksheet.Cells[i + 3, 8] = "'" + dt.Rows[i]["YS_FERROUS_METAL_BG"].ToString();//黑色金属-预

                wksheet.Cells[i + 3, 9] = "'" + dt.Rows[i]["YS_PURCHASE_PART_BG"].ToString();//外购件--预

                wksheet.Cells[i + 3, 10] = "'" + dt.Rows[i]["YS_MACHINING_PART_BG"].ToString();//加工件--预

                wksheet.Cells[i + 3, 11] = "'" + dt.Rows[i]["YS_PAINT_COATING_BG"].ToString();//油漆涂料-预

                wksheet.Cells[i + 3, 12] = "'" + dt.Rows[i]["YS_ELECTRICAL_BG"].ToString();//电气电料-预

                wksheet.Cells[i + 3, 13] = "'" + dt.Rows[i]["YS_OTHERMAT_COST_BG"].ToString();//其他材料费-预

                //wksheet.Cells[i + 3, 14] = dt.Rows[i]["YS_OUT_LAB_MAR"].ToString();//技术外协-订

                wksheet.Cells[i + 3, 14] = dt.Rows[i]["YS_FERROUS_METAL"].ToString();//黑色金属-订

                wksheet.Cells[i + 3, 15] = dt.Rows[i]["YS_PURCHASE_PART"].ToString();//外购件-订

                wksheet.Cells[i + 3, 16] = dt.Rows[i]["YS_MACHINING_PART"].ToString();//加工件-订

                wksheet.Cells[i + 3, 17] = dt.Rows[i]["YS_PAINT_COATING"].ToString();//油漆涂料-订

                wksheet.Cells[i + 3, 18] = "'" + dt.Rows[i]["YS_ELECTRICAL"].ToString();//电气电料-订

                wksheet.Cells[i + 3, 19] = dt.Rows[i]["YS_OTHERMAT_COST"].ToString();//其他材料费-订

                wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 19]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 19]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 19]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            }
            //设置列宽
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = Server.MapPath("/YS_Data/ExportFile/" + "订单监控表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
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
                contextResponse.Redirect(string.Format("~/YS_Data/ExportFile/{0}", path.Name), false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion

    }
}
