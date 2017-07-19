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
using Microsoft.Office.Interop.Excel;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_Inspection_Detail : System.Web.UI.Page
    {
        string AFI_ID = string.Empty;
        string action = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            AFI_ID = Request.QueryString["id"].ToString();
            action = Request.QueryString["action"].ToString();

            if(!IsPostBack)
            {
               
                bindzhijiandan();
                bindData();
                bindGridV1();
            }
            GridView1_DataBound();
        }
        private void bindData()  //绑定数据
        {
            string sqlselect = "select AFI_ID,AFI_TSDEP,AFI_PJNAME,AFI_ENGNAME,AFI_PARTNAME,AFI_PARTNATURE,AFI_TUHAO,AFI_QUANTITY,AFI_CNTLTXT,AFI_DATACLCT,AFI_DATE,convert(CHAR,AFI_RQSTCDATE,102) AS AFI_RQSTCDATE,AFI_SUPPLERNM,AFI_ISPCTSITE,AFI_CONTACT,AFI_CONTEL,AFI_MANNM,AFI_NOTE,AFI_QCMANNM,AFI_SUBITEM from TBQM_APLYFORINSPCT where AFI_ID='" + AFI_ID + "'";
            DataSet ds = DBCallCommon.FillDataSet(sqlselect);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string contactName = ds.Tables[0].Rows[0]["AFI_CONTACT"].ToString();
                string contactTel = ds.Tables[0].Rows[0]["AFI_CONTEL"].ToString();
                lbid.Text = ds.Tables[0].Rows[0]["AFI_ID"].ToString();//报检编号
                lbdep.Text = ds.Tables[0].Rows[0]["AFI_TSDEP"].ToString();
                lbpjname.Text = ds.Tables[0].Rows[0]["AFI_PJNAME"].ToString();
                lbengname.Text = ds.Tables[0].Rows[0]["AFI_ENGNAME"].ToString();
                lbpartname.Text = ds.Tables[0].Rows[0]["AFI_PARTNAME"].ToString();
                lbpartnature.Text = ds.Tables[0].Rows[0]["AFI_PARTNATURE"].ToString();
                lbtuhao.Text = ds.Tables[0].Rows[0]["AFI_TUHAO"].ToString();
                lbquantity.Text = ds.Tables[0].Rows[0]["AFI_QUANTITY"].ToString();
                lbcontroltxt.Text = ds.Tables[0].Rows[0]["AFI_CNTLTXT"].ToString();
                lbdataclct.Text = ds.Tables[0].Rows[0]["AFI_DATACLCT"].ToString();
                lbpjdate.Text = ds.Tables[0].Rows[0]["AFI_DATE"].ToString();
                lbrqstcdate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["AFI_RQSTCDATE"]).ToString("yyyy-MM-dd");
                lbsupplyname.Text = ds.Tables[0].Rows[0]["AFI_SUPPLERNM"].ToString();//供应商名称---需要确定
                lbsite.Text = ds.Tables[0].Rows[0]["AFI_ISPCTSITE"].ToString();
                lbcontact.Text = contactName ;//联系人
                lbtel.Text = contactTel;//联系人电话
                //Label16.Text = ds.Tables[0].Rows[0]["AFI_CONTEL"].ToString();//联系电话
                lbmannm.Text = ds.Tables[0].Rows[0]["AFI_MANNM"].ToString();//根据Session值获取姓名
                lbmeno.Text = ds.Tables[0].Rows[0]["AFI_NOTE"].ToString();
                lbqcmn.Text = ds.Tables[0].Rows[0]["AFI_QCMANNM"].ToString();
                hfditem.Value = ds.Tables[0].Rows[0]["AFI_SUBITEM"].ToString();
            }
            bindzhijiandan();
            bindItem();
        }
        private void bindItem()
        {
            string sql = "select * from TBQM_APLYFORITEM where CONTID='" + hfditem.Value + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridViewItem.DataSource = dt;
            GridViewItem.DataBind();
        }

        protected void GridViewItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string isagain = (e.Row.FindControl("lbagain") as System.Web.UI.WebControls.Label).Text.Trim();
                if (isagain == "1")
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;//表示第二次报检
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Red;//表示第二次报检
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Red;//表示第二次报检
                }
            }
        }

        private void bindzhijiandan() //绑定质检表
        {
            //判断是否有质检结果 AND ISR_NUM='1'
            //根据此字段来判定AFI_ID
            string sqlselect1 = "select * from TBQM_ISPCTRESULT where ISR_CODE='" + AFI_ID + "' AND ISR_NUM='1'";
            DataSet ds = DBCallCommon.FillDataSet(sqlselect1);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //有质检结果
                LabelPD.Visible = true;
                //hlAdd.Visible = false;
                LabelPD.Text = "质检结果：";
                string result = ds.Tables[0].Rows[0]["ISR_RESULT"].ToString();
                if (result.Split('?')[0] == "0")

                    lbrusult.Text = "不合格";

                else
                {
                    lbrusult.Text = "合格";
                    //SubqualityPanel.Visible = false;
                }
                lbyjdate.Text = ds.Tables[0].Rows[0]["ISR_DATE"].ToString();
                lbreport.Text = ds.Tables[0].Rows[0]["ISR_REPORT"].ToString();
                lbinspectornm.Text = ds.Tables[0].Rows[0]["ISR_INSPCTORNM"].ToString();//检验人员姓名怎么获取！！！
                lbinspctdscp.Text = ds.Tables[0].Rows[0]["ISR_INSPCTDSCP"].ToString();//检验说明
                //lbfailedrson.Text = ds.Tables[0].Rows[0]["ISR_FAILEDRSON"].ToString();//不合格原因
                //lbsolution.Text = ds.Tables[0].Rows[0]["ISR_SOLUTION"].ToString();//解决办法
                GVBind(lbreport.Text);  
            }
            else
            {
                //无质检结果
                LabelPD.Visible = true;
                LabelPD.Text = "未质检";
                QCResultPanel.Visible = false;
            }
        }

        private void GVBind(string content)
        {
            string sql = "select * from TBQM_RPFILES where PR_CONTENT='" + content + "'";
            DataSet ds = DBCallCommon.FillDataSet(sql);
            gvfileslist.DataSource = ds.Tables[0];
            gvfileslist.DataBind();
            gvfileslist.DataKeyNames = new string[] { "RP_ID" };
        }



        //下载报表
        protected void imgbtndownload_Click(object sender, ImageClickEventArgs e)
        {
            //获取imgbtnDelete的ImageButton对象
            ImageButton imgbtn = (ImageButton)sender;
            //引用imgbtnDelete控件的父控件上一级控件
            GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
            GridView gv = (GridView)imgbtn.Parent.Parent.Parent.Parent;
            //获取文件真实姓名
            string sqlStr = "select RP_SAVEURL,RP_FILENAME from TBQM_RPFILES where RP_ID='" + gv.DataKeys[gvr.RowIndex].Value.ToString() + "'";
            //打开数据库
            //Response.Write(sqlStr);         
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取文件路径
            string strFilePath = ds.Tables[0].Rows[0]["RP_SAVEURL"].ToString() + @"\" + ds.Tables[0].Rows[0]["RP_FILENAME"].ToString();
            //Response.Write(strFilePath);
            if (File.Exists(strFilePath))
            {
                System.IO.FileInfo file = new System.IO.FileInfo(strFilePath);
                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(file.Name));
                Response.AppendHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(file.FullName);
                Response.Flush();
                Response.End();
            }
            else
            {
                filesError.Visible = true;
                filesError.Text = "文件已被删除，请通知相关人员上传文件！";
            }
        }


        protected void back_Click(object sender, EventArgs e)
        {
            if (action == "view")
            {
                Response.Redirect("QC_InspecManage.aspx");
            }
            else
            {
                Response.Redirect("QC_Inspection_Manage.aspx");
            }
        }

        protected void GridView1_DataBound()
        {
            foreach (GridViewRow gr in GridView1.Rows)
            {
                //HtmlTable table = (HtmlTable)gr.FindControl("table1");
                //HtmlTableRow tr = new HtmlTableRow();
                System.Web.UI.WebControls.Label lb = (System.Web.UI.WebControls.Label)gr.FindControl("lb_report");
                string sql = "select * from TBQM_RPFILES where PR_CONTENT='" + lb.Text.Trim() + "'order by RP_UPLOADDATE ";
                DataSet ds = DBCallCommon.FillDataSet(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i=0; i < ds.Tables[0].Rows.Count;i++ )
                    {
                        //HtmlTableCell td = new HtmlTableCell();
                        //td.InnerHtml = "<a target='_self' href=" + Server.MapPath("zhijianbaobiao") + @"\" + ds.Tables[0].Rows[0]["RP_FILENAME"].ToString() + ">" + "查看" + "</a>";
                        //tr.Cells.Add(td);
                        ImageButton ib = new ImageButton();
                        ib.Height=18;
                        ib.Width = 18;
                        lb.Parent.Controls.Add(ib);
                        ib.ImageUrl = "~/Assets/images/pdf.jpg";
                        ib.ToolTip = ds.Tables[0].Rows[i]["RP_FILENAME"].ToString();
                        ib.ValidationGroup = ds.Tables[0].Rows[i]["RP_ID"].ToString();
                        ib.Click += new ImageClickEventHandler(ib_Click);//注册事件 
                        this.RegisterRequiresPostBack(ib);
                    }
                }
                //table.Rows.Add(tr);
            } 
            
        }
        /// <summary>
        /// 绑定过程检验的结果图
        /// </summary>
        private void bindGridV1()
        {
            /************************根据检验时间升序排列***************************/
            string sqlselect1 = "select * from TBQM_ISPCTRESULT where ISR_CODE='" + AFI_ID + "' AND ISR_NUM='0' ORDER BY ISR_DATE ";
            DataSet ds = DBCallCommon.FillDataSet(sqlselect1);
            if(ds.Tables[0].Rows.Count>0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
        }
        protected void ib_Click(object sender, ImageClickEventArgs e)
        {
            //Response.Write("zhixing");
            //获取imgbtnDelete的ImageButton对象
            ImageButton imgbtn = (ImageButton)sender;
            string sqlStr = "select RP_SAVEURL,RP_FILENAME from TBQM_RPFILES where RP_ID='" + imgbtn.ValidationGroup + "'";
            //打开数据库
            Response.Write(sqlStr);         
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取文件路径
            string strFilePath = ds.Tables[0].Rows[0]["RP_SAVEURL"].ToString() + @"\" + ds.Tables[0].Rows[0]["RP_FILENAME"].ToString();
            //Response.Write(strFilePath);
            if (File.Exists(strFilePath))
            {
                System.IO.FileInfo file = new System.IO.FileInfo(strFilePath);
                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(file.Name));
                Response.AppendHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(file.FullName);
                Response.Flush();
                Response.End();
            }
            else
            {
                filesError.Visible = true;
                filesError.Text = "文件已被删除，请通知相关人员上传文件！";
            }
        }

        protected void btn_export_Click(object sender, EventArgs e)
        {
            string sql = "select AFI_PJNAME,AFI_ENGID,AFI_ENGNAME,AFI_TSDEP,AFI_MANNM,AFI_PARTNATURE,AFI_SUPPLERNM,AFI_ISPCTSITE,AFI_CONTACT,PARTNM,TUHAO,NUM,DANZHONG,NUM*DANZHONG as sumwgh from TBQM_APLYFORINSPCT as a left  OUTER JOIN TBQM_APLYFORITEM as b on a.AFI_SUBITEM=b.CONTID where AFI_ID='" + AFI_ID + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ExportData(dt);
        }

        private void ExportData(System.Data.DataTable objdt)
        {
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("报检通知单模版") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            System.Data.DataTable dt = objdt;

            // 填充数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    //表头

                    wksheet.Cells[i + 2, 3] = "'" + dt.Rows[i]["AFI_PJNAME"].ToString();//项目名称

                    wksheet.Cells[i + 2, 7] = "'" + dt.Rows[i]["AFI_ENGNAME"].ToString();//工程名称

                    wksheet.Cells[i + 2, 12] = "'" + dt.Rows[i]["AFI_TSDEP"].ToString();//部门

                    wksheet.Cells[i + 2, 15] = "'" + dt.Rows[i]["AFI_MANNM"].ToString();//报检人

                    wksheet.Cells[i + 3, 3] = "'" + dt.Rows[i]["AFI_PARTNATURE"].ToString();//部件性质

                    wksheet.Cells[i + 3, 7] = "'" + dt.Rows[i]["AFI_SUPPLERNM"].ToString();//供货单位

                    wksheet.Cells[i + 3, 12] = "'" + dt.Rows[i]["AFI_ISPCTSITE"].ToString();//地点

                    wksheet.Cells[i + 3, 15] = "'" + dt.Rows[i]["AFI_CONTACT"].ToString();//质检人



                    //表体
                    wksheet.Cells[i + 6, 1] = Convert.ToString(i + 1);//序号

                    wksheet.Cells[i + 6, 2] = "'" + dt.Rows[i]["PARTNM"].ToString();//子项名称

                    wksheet.Cells[i + 6, 4] = "'" + dt.Rows[i]["TUHAO"].ToString();//标识

                    wksheet.Cells[i + 6, 5] = "'" + dt.Rows[i]["NUM"].ToString();//数量

                    wksheet.Cells[i + 6, 6] = "'" + dt.Rows[i]["DANZHONG"].ToString();//单重

                    wksheet.Cells[i + 6, 7] = "'" + dt.Rows[i]["sumwgh"].ToString();//总重

                }
                else
                {
                    //表体
                    wksheet.Cells[i + 6, 1] = Convert.ToString(i + 1);//序号

                    wksheet.Cells[i + 6, 2] = "'" + dt.Rows[i]["PARTNM"].ToString();//子项名称

                    wksheet.Cells[i + 6, 4] = "'" + dt.Rows[i]["TUHAO"].ToString();//标识

                    wksheet.Cells[i + 6, 5] = "'" + dt.Rows[i]["NUM"].ToString();//数量

                    wksheet.Cells[i + 6, 6] = "'" + dt.Rows[i]["DANZHONG"].ToString();//单重

                    wksheet.Cells[i + 6, 7] = "'" + dt.Rows[i]["sumwgh"].ToString();//总重

                }

                wksheet.get_Range(wksheet.Cells[i + 6, 1], wksheet.Cells[i + 6, 6]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 6, 1], wksheet.Cells[i + 6, 6]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 6, 1], wksheet.Cells[i + 6, 6]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            }
            //设置列宽
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = Server.MapPath("/QC_Data/ExportFile/" + "报检通知单" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        /// <summary>
        /// 输出Excel文件并退出
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="workbook"></param>
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
                contextResponse.Redirect(string.Format("~/QC_Data/ExportFile/{0}", path.Name), false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        
    }
}
