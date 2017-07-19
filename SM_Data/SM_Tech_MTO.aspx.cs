using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;
using System.IO;


namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Tech_MTO : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                bindGrid();
            }
        }
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数    
        }
        //初始化分布信息
        private void InitPager()
        {
            pager.TableName = "View_PC_MPTEMPCHANGE";//从视图中读取数据

            pager.PrimaryKey = "MP_ID";

            pager.ShowFields = "MP_ID,MP_CHPCODE,MP_PROJNAME,MP_ENGID+'-'+TSA_ENGNAME as MP_ENGNM,MP_CHPTCODE,MP_MARID,MNAME,GUIGE,CAIZHI,PURCUNIT,isnull(MP_BGNUM,0) as MP_BGNUM,isnull(MP_BKNUM,0) as MP_BKNUM,MP_SUBNAME,MP_EXECNAME,MP_EXECSTATE,MP_BKNOTE,ISNULL(MP_MTO,'') AS MTO,MP_SUBTIME,BianGtime ";

            //QSA_DATE表示任务分工时间

            pager.OrderField = "MP_SUBTIME DESC,MP_ID";

            if (CheckBoxMy.Checked)
            {
                TextBoxEXEC.Text = Session["UserName"].ToString();
            }
           
            

            pager.StrWhere = CreateConStr();

            pager.OrderType = 0;//项目编号的降序排列

            if (TextBoxnum.Text.Trim() != "")
            {
                pager.PageSize = Convert.ToInt32(TextBoxnum.Text.Trim());
            }
            else { pager.PageSize = 10; }

        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            

            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);

            //控制其不出现当前页码大于尾页情况

            while (dt.Rows.Count == 0 && pager.PageIndex > 1)
            {
                pager.PageIndex = UCPaging1.CurrentPage-1;

                UCPaging1.CurrentPage = pager.PageIndex;

                dt = CommonFun.GetDataByPagerQueryParam(pager);
                
            }

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
            bindControl();
            SetRadioButtonTip();
           
        }

        private string CreateConStr()
        {
            string strWhere = string.Empty;

            //string state = DropDownListState.SelectedValue;

            if (RadioButtonListState.SelectedValue == "0")
            {
                //全部
                strWhere = "MP_EXECSTATE>'0'";
            }
            else
            {
                //
                strWhere = "MP_EXECSTATE='" + RadioButtonListState.SelectedValue + "'";
            }
            

            if (TextBoxPro.Text.Trim() != string.Empty)
            {
                strWhere += " AND MP_PROJNAME like '%" + TextBoxPro.Text.Trim() + "%'";
            }
            if (TextBoxEng.Text.Trim() != string.Empty)
            {
                strWhere += " AND TSA_ENGNAME like '%" + TextBoxEng.Text.Trim() + "%'";
            }

            if (TextBoxTec.Text.Trim() != string.Empty)
            {
                strWhere += " AND MP_SUBNAME like '%" + TextBoxTec.Text.Trim() + "%'";
            }

            if (TextBoxPTC.Text.Trim() != string.Empty)
            {
                strWhere += " AND MP_CHPTCODE like '%" + TextBoxPTC.Text.Trim() + "%'";
            }

            if (TextBoxEXEC.Text.Trim() != string.Empty)
            {
                strWhere += " AND MP_EXECNAME like '%" + TextBoxEXEC.Text.Trim() + "%'";
            }

            //物料编码
            if (TextBoxMar.Text.Trim() != string.Empty)
            {
                strWhere += " AND MP_MARID like '%" + TextBoxMar.Text.Trim() + "%'";
            }
            if (TextBoxMarNM.Text.Trim() != string.Empty)
            {
                strWhere += " AND MNAME like '%" + TextBoxMarNM.Text.Trim() + "%'";
            }
            if (TextBoxGG.Text.Trim() != string.Empty)
            {
                strWhere += " AND GUIGE like '%" + TextBoxGG.Text.Trim() + "%'";
            }
            if (TextBoxCZ.Text.Trim() != string.Empty)
            {
                strWhere += " AND CAIZHI like '%" + TextBoxCZ.Text.Trim() + "%'";
            }

            return strWhere;
        }


        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
          
            UCPaging1.CurrentPage = 1 ;

            bindGrid();
        }
        protected void reSetCondition_Click(object sender, EventArgs e)
        {
            TextBoxPro.Text = string.Empty;
            TextBoxEng.Text = string.Empty;
            TextBoxTec.Text = string.Empty;
            TextBoxPTC.Text = string.Empty;            
            TextBoxMar.Text = string.Empty;
            TextBoxGG.Text = string.Empty;
            TextBoxMarNM.Text = string.Empty;
            TextBoxCZ.Text = string.Empty;
            TextBoxnum.Text = string.Empty;
            if (CheckBoxMy.Checked==false)
            {
                TextBoxEXEC.Text = string.Empty;
            }
            
        }
        double chnum = 0;
        double tzhnum = 0;

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (GridView1.DataKeys[e.Row.RowIndex]["MP_EXECSTATE"].ToString() == "1")
                {
                    e.Row.Attributes.Add("ondblclick", "ShowStoUseModal('" + Server.UrlEncode(GridView1.DataKeys[e.Row.RowIndex]["MP_CHPTCODE"].ToString()) + "','" + Server.UrlEncode(GridView1.DataKeys[e.Row.RowIndex]["MP_ID"].ToString()) + "');");
                }
                if ((e.Row.FindControl("LabelMTO") as System.Web.UI.WebControls.Label).Text.Trim()!=string.Empty)
                {
                    e.Row.Attributes.Add("ondblclick", "ShowMTOModal('" + (e.Row.FindControl("LabelMTO") as System.Web.UI.WebControls.Label).Text.Trim() + "');");
                }
                chnum += Convert.ToDouble(e.Row.Cells[e.Row.Cells.Count - 10].Text);
                tzhnum += Convert.ToDouble(e.Row.Cells[e.Row.Cells.Count - 9].Text);       
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[e.Row.Cells.Count - 10].Text = chnum.ToString();
                e.Row.Cells[e.Row.Cells.Count - 9].Text = tzhnum.ToString();
 
            }
        }


        protected void ButtonOperate_Click(object sender, EventArgs e)
        {
            bool IsFrist = true;

            string Code = generateMTOCode();
            string sql = "";
            List<string> sqllist = new List<string>();

            foreach (GridViewRow gr in GridView1.Rows)
            {
                if ((gr.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked && GridView1.DataKeys[gr.RowIndex].Values["MP_EXECSTATE"].ToString() == "2")
                {
                  

                    if (IsFrist)
                    {
                        string strsql = "insert into TBWS_MTOCODE (MTO_CODE) VALUES ('" + Code + "')";

                        DBCallCommon.ExeSqlText(strsql);

                        
                        string Date = System.DateTime.Now.ToString("yyyy-MM-dd");
                        string TargetCode = "备库";

                        string PlanerCode = Session["UserID"].ToString();
                        string DepCode = "07";
                        string DocCode = Session["UserID"].ToString();
                        string VerifierCode = "";
                        string ApproveDate = "";
                        string Comment = "";

                        sql = "DELETE FROM TBWS_MTO WHERE MTO_CODE='" + Code + "'";
                        sqllist.Add(sql);
                        sql = "DELETE FROM TBWS_MTODETAIL WHERE MTO_CODE='" + Code + "'";
                        sqllist.Add(sql);

                        sql = "INSERT INTO TBWS_MTO(MTO_CODE,MTO_DATE,MTO_TARGETID," +
                            "MTO_DEP,MTO_PLANER,MTO_DOC,MTO_VERIFIER,MTO_VERIFYDATE," +
                            "MTO_STATE,MTO_NOTE) VALUES('" + Code + "','" +
                            Date + "','" + TargetCode + "','" + DepCode + "','" + PlanerCode + "','" + DocCode + "','" +
                            VerifierCode + "','" + ApproveDate + "','1','" + Comment + "')";

                        sqllist.Add(sql);
                    }
                    //sql.ToString();
                    
                    IsFrist = false;

                    sql = "update TBPC_MPTEMPCHANGE set MP_MTO='" + Code + "' WHERE MP_ID='" + GridView1.DataKeys[gr.RowIndex].Values["MP_ID"].ToString() + "' and MP_EXECSTATE='2'";
                    
                    sqllist.Add(sql);

                    string selsql = "select * from TBWS_MTOBG_DETAIL where PUR_PCODE='" + GridView1.DataKeys[gr.RowIndex].Values["MP_ID"].ToString() + "'";

                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(selsql);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        string UniqueID = Code + (i + 1).ToString().PadLeft(3, '0');

                        string MaterialCode = dt.Rows[i]["PUR_MARID"].ToString();

                        string Fixed = dt.Rows[i]["PUR_FIXED"].ToString();

                        string Length = dt.Rows[i]["PUR_LENGTH"].ToString();

                        string Width = dt.Rows[i]["PUR_WIDTH"].ToString();

                        string LotNumber = dt.Rows[i]["PUR_LotNumber"].ToString();

                        string PTCFrom = dt.Rows[i]["PUR_PTCODE"].ToString();

                        string WarehouseCode = dt.Rows[i]["PUR_WarehouseCode"].ToString();

                        string PositionCode = dt.Rows[i]["PUR_PositionCode"].ToString();

                        string SQCODE = dt.Rows[i]["PUR_SQCODE"].ToString();

                        string DN = dt.Rows[i]["PUR_NUM"].ToString();

                        string RN = dt.Rows[i]["PUR_USTNUM"].ToString();

                        string PlanMode = dt.Rows[i]["PUR_PlanMode"].ToString();

                        string OrderID = dt.Rows[i]["PUR_OrderCode"].ToString();

                        string Note = "";

                        sql = "INSERT INTO TBWS_MTODETAIL(MTO_UNIQUEID,MTO_CODE,MTO_SQCODE,MTO_MARID," +
                               "MTO_FIXED,MTO_LENGTH,MTO_WIDTH,MTO_PCODE,MTO_FROMPTCODE," +
                               "MTO_WAREHOUSE,MTO_LOCATION,MTO_KTNUM,MTO_KTFZNUM,MTO_TOPTCODE,MTO_TZNUM," +
                               "MTO_TZFZNUM,MTO_PMODE,MTO_ORDERID,MTO_NOTE,MTO_STATE) VALUES('" + UniqueID + "','" +
                               Code + "','" + SQCODE + "','" + MaterialCode + "','" + Fixed + "','" +
                               Length + "','" + Width + "','" + LotNumber + "','" + PTCFrom + "','" + WarehouseCode + "','" + PositionCode + "','" +
                               DN + "','0','备库','" + RN + "','0','" + PlanMode + "','" +
                               OrderID + "','" + Note + "','')";

                        sqllist.Add(sql);
                    }
                }
            }

            if (sqllist.Count ==0) 
            {
                sqllist.Clear();

                string alert = "<script>alert('请选择要MTO的条目！')</script>";

                ClientScript.RegisterStartupScript(this.GetType(), "myscript", alert);

                return;
            }

            DBCallCommon.ExecuteTrans(sqllist);

            string script = "<script>ShowMTOModal('" + Code + "')</script>";

            ClientScript.RegisterStartupScript(this.GetType(), "myscript", script);

        }

        //生成MTO单号
        protected string generateMTOCode()
        {
            string Code = "";
            string sql = "SELECT MAX(MTO_CODE) AS MaxCode FROM TBWS_MTOCODE WHERE LEN(MTO_CODE)=10";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                if (dr["MaxCode"] != DBNull.Value)
                {
                    Code = dr["MaxCode"].ToString();
                }
            }
            dr.Close();
            if (Code == "")
            {
                return "MTO0000001";
            }
            else
            {
                int tempnum = Convert.ToInt32((Code.Substring(3, 7)));
                tempnum++;
                Code = "MTO" + tempnum.ToString().PadLeft(7, '0');
                return Code;
            }
        }


        //反调整
        protected void ButtonAntiAdjust_Click(object sender, EventArgs e)
        {

            bool NoSelet = true;

            List<string> sqllist = new List<string>();

            foreach (GridViewRow gr in GridView1.Rows)
            {
                if ((gr.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                   
                    //删除变更筛选库存条目

                    string strsql = "delete from TBWS_MTOBG_DETAIL where PUR_PCODE='" + GridView1.DataKeys[gr.RowIndex].Values["MP_ID"].ToString() + "'";

                    sqllist.Add(strsql);

                    //修改调整状态
                    strsql = "update TBPC_MPTEMPCHANGE set MP_EXECSTATE='1',MP_BKNUM=0,MP_EXECID='" + Session["UserID"].ToString() + "' where MP_ID='" + GridView1.DataKeys[gr.RowIndex].Values["MP_ID"].ToString() + "' and MP_EXECSTATE='2'";
                    
                    sqllist.Add(strsql);

                    NoSelet = false;
                }
            }

            if (NoSelet)
            {
                sqllist.Clear();

                string alert = "<script>alert('请选择需要反调整的条目！')</script>";

                ClientScript.RegisterStartupScript(this.GetType(), "myscript", alert);

                return;
            }

            DBCallCommon.ExecuteTrans(sqllist);

            bindGrid();
            

        }
        //反关闭
        protected void ButtonAntiClose_Click(object sender, EventArgs e)
        {
            bool NoSelet = true;
            List<string> sqllist = new List<string>();

            foreach (GridViewRow gr in GridView1.Rows)
            {
                if ((gr.FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked)
                {

                    //删除变更筛选库存条目

                    string strsql = "delete from TBWS_MTOBG_DETAIL where PUR_PCODE='" + GridView1.DataKeys[gr.RowIndex].Values["MP_ID"].ToString() + "'";

                    sqllist.Add(strsql);

                    //修改调整状态
                    strsql = "update TBPC_MPTEMPCHANGE set MP_EXECSTATE='1',MP_BKNUM=0,MP_EXECID='" + Session["UserID"].ToString() + "' where MP_ID='" + GridView1.DataKeys[gr.RowIndex].Values["MP_ID"].ToString() + "' and MP_EXECSTATE='4'";

                    sqllist.Add(strsql);

                    NoSelet = false;
                }
            }

            if (NoSelet)
            {
                sqllist.Clear();

                string alert = "<script>alert('请选择需要反关闭的条目！')</script>";

                ClientScript.RegisterStartupScript(this.GetType(), "myscript", alert);

                return;
            }

            DBCallCommon.ExecuteTrans(sqllist);

            bindGrid();
        }

        protected void RadioButtonListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }

        private void bindControl()
        {
            if (RadioButtonListState.SelectedValue == "1")
            {
                //未调整
                ButtonAntiAdjust.Enabled = false;
                ButtonClose.Enabled = false;
                ButtonMTO.Enabled = false;
            }
            else if (RadioButtonListState.SelectedValue == "2")
            {
                //调整中
                ButtonAntiAdjust.Enabled = true;
                ButtonClose.Enabled = false;
                ButtonMTO.Enabled = true;
            }
            else if (RadioButtonListState.SelectedValue == "3")
            {
                //已调整，只有删除MTO单据才能反调整
                ButtonAntiAdjust.Enabled = false;
                ButtonClose.Enabled = false;
                ButtonMTO.Enabled = false;
            }
            else if (RadioButtonListState.SelectedValue == "4")
            {
                //关闭
                ButtonAntiAdjust.Enabled = false;
                ButtonClose.Enabled = true;
                ButtonMTO.Enabled = false;
            }
            else
            {
                //全部
                ButtonAntiAdjust.Enabled = false;
                ButtonClose.Enabled = false;
                ButtonMTO.Enabled = false;
            }
        }


        protected void SetRadioButtonTip()
        {
            int wtz = 0;
            int mywtz = 0;
            int tzz = 0;
            int mytzz = 0;
            int ytz = 0;

            string sqltext_wtz = "select count(*) from TBPC_MPTEMPCHANGE where  MP_EXECSTATE='1'";
            string sqltext_mywtz = "select count(*) from TBPC_MPTEMPCHANGE where  MP_EXECSTATE='1' AND MP_EXECID='" + Session["UserID"].ToString() + "'";
            string sqltext_tzz = "select count(*) from TBPC_MPTEMPCHANGE where  MP_EXECSTATE='2'";
            string sqltext_mytzz = "select count(*) from TBPC_MPTEMPCHANGE where  MP_EXECSTATE='2' AND MP_EXECID='" + Session["UserID"].ToString() + "'";
            string sqltext_ytz = "select count(*) from TBPC_MPTEMPCHANGE where MP_EXECSTATE='3'";
            
            wtz = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_wtz).Rows[0][0].ToString());
            mywtz = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_mywtz).Rows[0][0].ToString());
            tzz = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_tzz).Rows[0][0].ToString());
            mytzz = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_mytzz).Rows[0][0].ToString());
            ytz = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_ytz).Rows[0][0].ToString());

            RadioButtonListState.Items.FindByValue("1").Text ="未调整<font color=red>(" + mywtz + "/" + wtz + ")</font>";
            RadioButtonListState.Items.FindByValue("2").Text = "调整中<font color=red>(" + mytzz + "/" + tzz + ")</font>";
            RadioButtonListState.Items.FindByValue("3").Text = "已调整<font color=red>(" + ytz + ")</font>";
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindGrid();
        }


        protected void CheckBoxMy_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxMy.Checked)
            {
                TextBoxEXEC.Text = Session["UserName"].ToString();

            }
            else
            {
                TextBoxEXEC.Text = string.Empty;
            }
         
            pager.StrWhere = CreateConStr();

            bindGrid();
        }
        #region 导出

        protected void ButtonDaoChu_Click(object sender, EventArgs e)
        {
            string sqlstr = string.Empty;

            if (CreateConStr() == string.Empty)
            {
                sqlstr = "select MP_CHPCODE as 变更批号,MP_PROJNAME as 项目名称,MP_ENGID+'-'+TSA_ENGNAME as 工程名称,MP_CHPTCODE as 计划跟踪号,MP_MARID as 物料编码,MNAME as 物料名称,GUIGE as 规格型号,CAIZHI as 材质,PURCUNIT as 单位,MP_BGNUM as 变更数量,MP_BKNUM as 调整数量,MP_SUBNAME as 提交人,MP_SUBTIME as 提交时间,BianGtime as 变更日期,MP_EXECNAME as 执行人,ISNULL(MP_MTO,'') AS MTO,MP_BKNOTE as 备注  from View_PC_MPTEMPCHANGE  order by MP_CHPCODE DESC";

            }
            else
            {
                sqlstr = "select MP_CHPCODE as 变更批号,MP_PROJNAME as 项目名称,MP_ENGID+'-'+TSA_ENGNAME as 工程名称,MP_CHPTCODE as 计划跟踪号,MP_MARID as 物料编码,MNAME as 物料名称,GUIGE as 规格型号,CAIZHI as 材质,PURCUNIT as 单位,MP_BGNUM as 变更数量,MP_BKNUM as 调整数量,MP_SUBNAME as 提交人,MP_SUBTIME as 提交时间,BianGtime as 变更日期,MP_EXECNAME as 执行人,ISNULL(MP_MTO,'') AS MTO,MP_BKNOTE as 备注  from View_PC_MPTEMPCHANGE where " + CreateConStr() + "  order by MP_CHPCODE DESC";
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
                //if (dt.Columns[i].ColumnName == "需用数量" || dt.Columns[i].ColumnName == "订单数量" || dt.Columns[i].ColumnName == "入库数量" || dt.Columns[i].ColumnName == "出库数量" || dt.Columns[i].ColumnName == "差额")
                //{
                //    wksheet.get_Range(getExcelColumnLabel(i + 2) + "1", wksheet.Cells[1 + rowCount, i + 2]).NumberFormatLocal = "G/通用格式";
                //}
            }
            //表体
            wksheet.get_Range("A2", wksheet.Cells[1 + rowCount, colCount + 1]).Value2 = dataArray;

            //表尾


            //设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = Server.MapPath("/SM_Data/ExportFile/" + "变更导出" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
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
    }
}
