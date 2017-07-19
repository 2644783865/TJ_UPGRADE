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
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_PLdaochu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initpage();
            }
        }

        private void initpage()
        {
            string sqlText = "select ST_NAME,ST_ID from TBDS_STAFFINFO WHERE ST_DEPID='06' and ST_PD='0'";
            string dataText = "ST_NAME";
            string dataValue = "ST_ID";
            DBCallCommon.BindDdl(drp_stu, sqlText, dataText, dataValue);
            drp_stu.SelectedIndex = 0;
        }


        protected void btn_daochu_Click(object sender, EventArgs e)
        {
            string startdate = txtStartTime.Value.ToString() == "" ? "1900-01-01" : txtStartTime.Value.ToString();
            string enddate = txtEndTime.Value.ToString() == "" ? "2100-01-01" : txtEndTime.Value.ToString();
            string sqltext = "";
            string conwhere = "";
            if (DropDownList2.SelectedIndex != 0)
            {
                conwhere = "PO_MASHAPE like '" + DropDownList2.SelectedItem.Text + "'";
            }
            else
            {
                conwhere = "PO_MASHAPE like '%%'";
            }

            if (drp_stu.SelectedIndex == 0)
            {
                if (tb_pj.Text == "" && tb_eng.Text == "")
                {
                    sqltext = "SELECT orderno, marnm, margg, margb, marcz, marunit, marfzunit, picno, ptcode, marid, num, fznum, zxnum, zxfznum, cgtimerq, " +
                          "recdate, recgdnum, recgdfznum, taxrate, detailnote, keycoms, detailstate, detailcstate, whstate, length, width, fixed, pjid, " +
                          "pjnm, engid, engnm, ctprice, planmode, ctamount, price, amount, supplierid, suppliernm, zdrid, zdrnm, substring(zdtime,0,11) as zdtime, shrid, " +
                          "shrnm, shtime, ywyid, ywynm, zgid, zgnm, depid, depnm, totalstate, totalcstate, totalnote, fax, phono, conname, abstract, " +
                          "PO_MASHAPE,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,PO_OperateState,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME  " +
                                "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where (substring(zdtime,0,11) between '" + startdate + "' and '" + enddate + "') and suppliernm like '%" + TextBox1.Text.Trim() + "%'  " +
                                " and margb like '%" + tb_margb.Text.Trim() + "%' and  orderno like '%" + tb_orderno.Text.Trim() + "%' and  marnm like '%" + tb_name.Text.Trim() + "%' and marcz like '%" + tb_cz.Text.Trim() + "%' and margg like '%" + tb_gg.Text.Trim() + "%' and marid like '%" + tb_gb.Text.Trim() + "%' and " + conwhere + " order by orderno desc,marnm,margg,ptcode asc";
                }
                else if (tb_pj.Text == "" && tb_eng.Text != "")
                {
                    sqltext = "SELECT orderno, marnm, margg, margb, marcz, marunit, marfzunit, picno, ptcode, marid, num, fznum, zxnum, zxfznum, cgtimerq, " +
                          "recdate, recgdnum, recgdfznum, taxrate, detailnote, keycoms, detailstate, detailcstate, whstate, length, width, fixed, pjid, " +
                          "pjnm, engid, engnm, ctprice, planmode, ctamount, price, amount, supplierid, suppliernm, zdrid, zdrnm, substring(zdtime,0,11) as zdtime, shrid, " +
                          "shrnm, shtime, ywyid, ywynm, zgid, zgnm, depid, depnm, totalstate, totalcstate, totalnote, fax, phono, conname, abstract, " +
                          "PO_MASHAPE,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,PO_OperateState,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME  " +
                               "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where (substring(zdtime,0,11) between '" + startdate + "' and '" + enddate + "') and suppliernm like '%" + TextBox1.Text.Trim() + "%'  " +
                               " and margb like '%" + tb_margb.Text.Trim() + "%' and  orderno like '%" + tb_orderno.Text.Trim() + "%' and  marnm like '%" + tb_name.Text.Trim() + "%' and marcz like '%" + tb_cz.Text.Trim() + "%' and margg like '%" + tb_gg.Text.Trim() + "%' and marid like '%" + tb_gb.Text.Trim() + "%'  and engnm like '%" + tb_eng.Text.Trim() + "%' and " + conwhere + " order by orderno desc,marnm,margg,ptcode asc";
                }
                else if (tb_pj.Text != "" && tb_eng.Text == "")
                {
                    sqltext = "SELECT orderno, marnm, margg, margb, marcz, marunit, marfzunit, picno, ptcode, marid, num, fznum, zxnum, zxfznum, cgtimerq, " +
                          "recdate, recgdnum, recgdfznum, taxrate, detailnote, keycoms, detailstate, detailcstate, whstate, length, width, fixed, pjid, " +
                          "pjnm, engid, engnm, ctprice, planmode, ctamount, price, amount, supplierid, suppliernm, zdrid, zdrnm, substring(zdtime,0,11) as zdtime, shrid, " +
                          "shrnm, shtime, ywyid, ywynm, zgid, zgnm, depid, depnm, totalstate, totalcstate, totalnote, fax, phono, conname, abstract, " +
                          "PO_MASHAPE,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,PO_OperateState,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME  " +
                               "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where (substring(zdtime,0,11) between '" + startdate + "' and '" + enddate + "') and suppliernm like '%" + TextBox1.Text.Trim() + "%'  " +
                               " and margb like '%" + tb_margb.Text.Trim() + "%' and  orderno like '%" + tb_orderno.Text.Trim() + "%' and  marnm like '%" + tb_name.Text.Trim() + "%' and marcz like '%" + tb_cz.Text.Trim() + "%' and margg like '%" + tb_gg.Text.Trim() + "%' and marid like '%" + tb_gb.Text.Trim() + "%' and pjnm like '%" + tb_pj.Text.Trim() + "%'  and " + conwhere + " order by orderno desc,marnm,margg,ptcode asc";
                }
                else if (tb_pj.Text != "" && tb_eng.Text != "")
                {
                    sqltext = "SELECT orderno, marnm, margg, margb, marcz, marunit, marfzunit, picno, ptcode, marid, num, fznum, zxnum, zxfznum, cgtimerq, " +
                          "recdate, recgdnum, recgdfznum, taxrate, detailnote, keycoms, detailstate, detailcstate, whstate, length, width, fixed, pjid, " +
                          "pjnm, engid, engnm, ctprice, planmode, ctamount, price, amount, supplierid, suppliernm, zdrid, zdrnm, substring(zdtime,0,11) as zdtime, shrid, " +
                          "shrnm, shtime, ywyid, ywynm, zgid, zgnm, depid, depnm, totalstate, totalcstate, totalnote, fax, phono, conname, abstract, " +
                          "PO_MASHAPE,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,PO_OperateState,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME  " +
                                 "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where (substring(zdtime,0,11) between '" + startdate + "' and '" + enddate + "') and suppliernm like '%" + TextBox1.Text.Trim() + "%'  " +
                                 " and margb like '%" + tb_margb.Text.Trim() + "%' and  orderno like '%" + tb_orderno.Text.Trim() + "%' and  marnm like '%" + tb_name.Text.Trim() + "%' and marcz like '%" + tb_cz.Text.Trim() + "%' and margg like '%" + tb_gg.Text.Trim() + "%' and marid like '%" + tb_gb.Text.Trim() + "%' and pjnm like '%" + tb_pj.Text.Trim() + "%' and engnm like '%" + tb_eng.Text.Trim() + "%' and  " + conwhere + " order by orderno desc,marnm,margg,ptcode asc";
                }
            }
            else
            {
                if (tb_pj.Text == "" && tb_eng.Text == "")
                {
                    sqltext = "SELECT orderno, marnm, margg, margb, marcz, marunit, marfzunit, picno, ptcode, marid, num, fznum, zxnum, zxfznum, cgtimerq, " +
                          "recdate, recgdnum, recgdfznum, taxrate, detailnote, keycoms, detailstate, detailcstate, whstate, length, width, fixed, pjid, " +
                          "pjnm, engid, engnm, ctprice, planmode, ctamount, price, amount, supplierid, suppliernm, zdrid, zdrnm, substring(zdtime,0,11) as zdtime, shrid, " +
                          "shrnm, shtime, ywyid, ywynm, zgid, zgnm, depid, depnm, totalstate, totalcstate, totalnote, fax, phono, conname, abstract, " +
                          "PO_MASHAPE,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,PO_OperateState,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME  " +
                               "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where (substring(zdtime,0,11) between '" + startdate + "' and '" + enddate + "') and suppliernm like '%" + TextBox1.Text.Trim() + "%' and zdrid like '" + drp_stu.SelectedValue.ToString() + "' " +
                               " and margb like '%" + tb_margb.Text.Trim() + "%' and  orderno like '%" + tb_orderno.Text.Trim() + "%' and  marnm like '%" + tb_name.Text.Trim() + "%' and marcz like '%" + tb_cz.Text.Trim() + "%' and margg like '%" + tb_gg.Text.Trim() + "%' and marid like '%" + tb_gb.Text.Trim() + "%' and " + conwhere + " order by orderno desc,marnm,margg,ptcode asc";
                }
                else if (tb_pj.Text == "" && tb_eng.Text != "")
                {
                    sqltext = "SELECT orderno, marnm, margg, margb, marcz, marunit, marfzunit, picno, ptcode, marid, num, fznum, zxnum, zxfznum, cgtimerq, " +
                          "recdate, recgdnum, recgdfznum, taxrate, detailnote, keycoms, detailstate, detailcstate, whstate, length, width, fixed, pjid, " +
                          "pjnm, engid, engnm, ctprice, planmode, ctamount, price, amount, supplierid, suppliernm, zdrid, zdrnm, substring(zdtime,0,11) as zdtime, shrid, " +
                          "shrnm, shtime, ywyid, ywynm, zgid, zgnm, depid, depnm, totalstate, totalcstate, totalnote, fax, phono, conname, abstract, " +
                          "PO_MASHAPE,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,PO_OperateState,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME  " +
                             "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where (substring(zdtime,0,11) between '" + startdate + "' and '" + enddate + "') and suppliernm like '%" + TextBox1.Text.Trim() + "%' and zdrid like '" + drp_stu.SelectedValue.ToString() + "' " +
                             " and margb like '%" + tb_margb.Text.Trim() + "%' and  orderno like '%" + tb_orderno.Text.Trim() + "%' and  marnm like '%" + tb_name.Text.Trim() + "%' and marcz like '%" + tb_cz.Text.Trim() + "%' and margg like '%" + tb_gg.Text.Trim() + "%' and marid like '%" + tb_gb.Text.Trim() + "%' and engnm like '%" + tb_eng.Text.Trim() + "%' and " + conwhere + " order by orderno desc,marnm,margg,ptcode asc";
                }
                else if (tb_pj.Text != "" && tb_eng.Text == "")
                {
                    sqltext = "SELECT orderno, marnm, margg, margb, marcz, marunit, marfzunit, picno, ptcode, marid, num, fznum, zxnum, zxfznum, cgtimerq, " +
                          "recdate, recgdnum, recgdfznum, taxrate, detailnote, keycoms, detailstate, detailcstate, whstate, length, width, fixed, pjid, " +
                          "pjnm, engid, engnm, ctprice, planmode, ctamount, price, amount, supplierid, suppliernm, zdrid, zdrnm, substring(zdtime,0,11) as zdtime, shrid, " +
                          "shrnm, shtime, ywyid, ywynm, zgid, zgnm, depid, depnm, totalstate, totalcstate, totalnote, fax, phono, conname, abstract, " +
                          "PO_MASHAPE,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,PO_OperateState,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME  " +
                                 "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where (substring(zdtime,0,11) between '" + startdate + "' and '" + enddate + "') and suppliernm like '%" + TextBox1.Text.Trim() + "%' and zdrid like '" + drp_stu.SelectedValue.ToString() + "' " +
                                 " and margb like '%" + tb_margb.Text.Trim() + "%' and  orderno like '%" + tb_orderno.Text.Trim() + "%' and  marnm like '%" + tb_name.Text.Trim() + "%' and marcz like '%" + tb_cz.Text.Trim() + "%' and margg like '%" + tb_gg.Text.Trim() + "%' and marid like '%" + tb_gb.Text.Trim() + "%' and pjnm like '%" + tb_pj.Text.Trim() + "%'  and  " + conwhere + " order by orderno desc,marnm,margg,ptcode asc";
                }
                else if (tb_pj.Text != "" && tb_eng.Text != "")
                {
                    sqltext = "SELECT orderno, marnm, margg, margb, marcz, marunit, marfzunit, picno, ptcode, marid, num, fznum, zxnum, zxfznum, cgtimerq, " +
                          "recdate, recgdnum, recgdfznum, taxrate, detailnote, keycoms, detailstate, detailcstate, whstate, length, width, fixed, pjid, " +
                          "pjnm, engid, engnm, ctprice, planmode, ctamount, price, amount, supplierid, suppliernm, zdrid, zdrnm, substring(zdtime,0,11) as zdtime, shrid, " +
                          "shrnm, shtime, ywyid, ywynm, zgid, zgnm, depid, depnm, totalstate, totalcstate, totalnote, fax, phono, conname, abstract, " +
                          "PO_MASHAPE,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,PO_OperateState,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME  " +
                                 "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where (substring(zdtime,0,11) between '" + startdate + "' and '" + enddate + "') and suppliernm like '%" + TextBox1.Text.Trim() + "%' and zdrid like '" + drp_stu.SelectedValue.ToString() + "' " +
                                 " and margb like '%" + tb_margb.Text.Trim() + "%' and  orderno like '%" + tb_orderno.Text.Trim() + "%' and  marnm like '%" + tb_name.Text.Trim() + "%' and marcz like '%" + tb_cz.Text.Trim() + "%' and margg like '%" + tb_gg.Text.Trim() + "%' and marid like '%" + tb_gb.Text.Trim() + "%' and pjnm like '%" + tb_pj.Text.Trim() + "%' and engnm like '%" + tb_eng.Text.Trim() + "%' and  " + conwhere + " order by orderno desc,marnm,margg,ptcode asc";
                }

            }

            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DataView dv = null;
            if (rad_quanbu.Checked)//全部
            {
                dv = dt.DefaultView;
                dv.RowFilter = "detailcstate='0'";
                dt = dv.ToTable();
            }
            if (rad_weidaohuo.Checked)//未到货
            {
                dv = dt.DefaultView;
                dv.RowFilter = "detailstate='1' and recgdnum=0 and detailcstate='0'";
                dt = dv.ToTable();
            }
            if (rad_bfdaohuo.Checked)//部分到货
            {
                dv = dt.DefaultView;
                dv.RowFilter = "detailstate='1' and recgdnum<zxnum and recgdnum>0 and detailcstate='0'";
                dt = dv.ToTable();
            }
            if (rad_yidaohuo.Checked)//已到货
            {
                dv = dt.DefaultView;
                dv.RowFilter = "(detailstate='2' or detailstate='3') and detailcstate='0'";
                dt = dv.ToTable();
            }
            if (tb_th.Text != "")//图号
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PO_TUHAO like '%" + tb_th.Text + "%'";
                dt = dv.ToTable();
            }
            if (dt.Rows.Count > 10000)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导出的数据量超过10000条，请添加导出条件，分多次导出！');", true);
            }
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有数据！');", true);
            }
            else
            {
                ExportMSData(dt);
            }
        }



        protected void TextBox1_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (TextBox1.Text.ToString().Contains("|"))
            {
                Cname = TextBox1.Text.Substring(0, TextBox1.Text.ToString().IndexOf("|"));
                TextBox1.Text = Cname.Trim();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请正确填写供应商！');", true);
            }
        }

        /// <summary>
        /// 采购订单
        /// </summary>
        public static void ExportMSData(System.Data.DataTable dt)
        {
            //Object Opt = System.Type.Missing;
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //Worksheet wksheet;
            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("采购订单明细表模版") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

            //m_xlApp.Visible = false;     // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）

            //wksheet = (Worksheet)workbook.Sheets.get_Item(1);
            ////根据批号查询数据


            ////设置工作薄名称


            //////填充数据

            //int rowCount = dt.Rows.Count;

            //int colCount = dt.Columns.Count;

            //object[,] dataArray = new object[rowCount, colCount];

            //for (int i = 0; i < rowCount; i++)
            //{
            //    dataArray[i, 0] = i + 1;
            //    for (int j = 0; j < colCount-2; j++)
            //    {
            //        dataArray[i, j+1] = dt.Rows[i][j];
            //    }
            //}

            //wksheet.get_Range("A3", wksheet.Cells[rowCount + 2, colCount-1]).Value2 = dataArray;
            //wksheet.get_Range("A3", wksheet.Cells[rowCount + 2, colCount-1]).Borders.LineStyle = 1;

            //////设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            //string filename = System.Web.HttpContext.Current.Server.MapPath("采购订单明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);

            string filename = "采购订单明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("采购订单明细表模版.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据
                IRow row1 = sheet0.GetRow(1);
                row1.GetCell(2).SetCellValue(dt.Rows[0]["orderno"].ToString());//订单编号
                row1.GetCell(7).SetCellValue(dt.Rows[0]["suppliernm"].ToString());//供应商
                row1.GetCell(13).SetCellValue(dt.Rows[0]["cgtimerq"].ToString());//交货日期
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 3);

                    #region MyRegion

                    //row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号
                    //row.CreateCell(1).SetCellValue("'" + dt.Rows[i]["orderno"].ToString());//订单编号
                    //row.CreateCell(2).SetCellValue("'" + dt.Rows[i]["suppliernm"].ToString());//供应商
                    //row.CreateCell(3).SetCellValue("'" + dt.Rows[i]["zdtime"].ToString());//制单日期
                    //row.CreateCell(4).SetCellValue("'" + dt.Rows[i]["ptcode"].ToString());//计划跟踪号
                    //row.CreateCell(5).SetCellValue("'" + dt.Rows[i]["pjnm"].ToString());//项目名称
                    //row.CreateCell(6).SetCellValue("'" + dt.Rows[i]["engnm"].ToString());//工程名称
                    //row.CreateCell(7).SetCellValue("'" + dt.Rows[i]["PO_TUHAO"].ToString());//图号/标识号
                    //row.CreateCell(8).SetCellValue("'" + dt.Rows[i]["marid"].ToString());//物料编码
                    //row.CreateCell(9).SetCellValue("'" + dt.Rows[i]["marnm"].ToString());//名称
                    //row.CreateCell(10).SetCellValue("'" + dt.Rows[i]["margg"].ToString());//规格
                    //row.CreateCell(11).SetCellValue("'" + dt.Rows[i]["marcz"].ToString());//材质
                    //row.CreateCell(12).SetCellValue("'" + dt.Rows[i]["margb"].ToString());//国标
                    //row.CreateCell(13).SetCellValue(dt.Rows[i]["price"].ToString());//单价
                    //row.CreateCell(14).SetCellValue(dt.Rows[i]["amount"].ToString());//金额
                    //row.CreateCell(15).SetCellValue(dt.Rows[i]["ctprice"].ToString());//含税单价
                    //row.CreateCell(16).SetCellValue(dt.Rows[i]["ctamount"].ToString());//价税合计
                    //row.CreateCell(17).SetCellValue(dt.Rows[i]["zxnum"].ToString());//采购数量
                    //row.CreateCell(18).SetCellValue("'" + dt.Rows[i]["marunit"].ToString());//单位
                    //row.CreateCell(19).SetCellValue(dt.Rows[i]["zxfznum"].ToString());//辅助数量
                    //row.CreateCell(20).SetCellValue("'" + dt.Rows[i]["marfzunit"].ToString());//辅助单位
                    //row.CreateCell(21).SetCellValue(dt.Rows[i]["recgdnum"].ToString());//已到货数量
                    //row.CreateCell(22).SetCellValue("'" + dt.Rows[i]["recdate"].ToString());//到货日期
                    //row.CreateCell(23).SetCellValue("'" + dt.Rows[i]["cgtimerq"].ToString());//交货日期
                    //row.CreateCell(24).SetCellValue("'" + dt.Rows[i]["length"].ToString());//长度
                    //row.CreateCell(25).SetCellValue("'" + dt.Rows[i]["width"].ToString());//宽度
                    //row.CreateCell(26).SetCellValue("'" + dt.Rows[i]["detailnote"].ToString());//备注

                    #endregion

                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号

                    row.CreateCell(1).SetCellValue(dt.Rows[i]["marid"].ToString());//物料编码
                    row.CreateCell(2).SetCellValue(dt.Rows[i]["marnm"].ToString());//名称
                    row.CreateCell(3).SetCellValue(dt.Rows[i]["PO_CHILDENGNAME"].ToString());//部件名称
                    row.CreateCell(4).SetCellValue(dt.Rows[i]["PO_MAP"].ToString());//部件图号
                    row.CreateCell(5).SetCellValue(dt.Rows[i]["margb"].ToString());//国标
                    row.CreateCell(6).SetCellValue(dt.Rows[i]["PO_TUHAO"].ToString());//图号/标识号

                    row.CreateCell(7).SetCellValue(dt.Rows[i]["margg"].ToString());//规格
                    row.CreateCell(8).SetCellValue(dt.Rows[i]["marcz"].ToString());//材质
                    row.CreateCell(9).SetCellValue(dt.Rows[i]["length"].ToString());//长度
                    row.CreateCell(10).SetCellValue(dt.Rows[i]["width"].ToString());//宽度
                    row.CreateCell(11).SetCellValue(dt.Rows[i]["marunit"].ToString());//单位
                    row.CreateCell(12).SetCellValue(dt.Rows[i]["zxnum"].ToString());//采购数量
                    row.CreateCell(13).SetCellValue(dt.Rows[i]["PO_TECUNIT"].ToString());//辅助单位
                    row.CreateCell(14).SetCellValue(dt.Rows[i]["zxfznum"].ToString());//辅助数量
                    row.CreateCell(15).SetCellValue(dt.Rows[i]["ctprice"].ToString());//含税单价
                    row.CreateCell(16).SetCellValue(dt.Rows[i]["ctamount"].ToString());//加税合计
                    //row.CreateCell(19).SetCellValue(dt.Rows[i]["price"].ToString());//单价（不含税）
                    //row.CreateCell(20).SetCellValue(dt.Rows[i]["amount"].ToString());//金额（不含税）

                    //row.CreateCell(22).SetCellValue(dt.Rows[i]["recdate"].ToString());//到货日期
                    row.CreateCell(17).SetCellValue(dt.Rows[i]["PO_MASHAPE"].ToString());//类型
                    row.CreateCell(18).SetCellValue(dt.Rows[i]["detailnote"].ToString());//备注
                    //row.CreateCell(25).SetCellValue(dt.Rows[i]["taxrate"].ToString());//税率
                    //row.CreateCell(26).SetCellValue(dt.Rows[i]["recgdnum"].ToString());//已到货数量
                    row.CreateCell(19).SetCellValue(dt.Rows[i]["ptcode"].ToString());//计划跟踪号

                    NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                    font1.FontName = "仿宋";//字体
                    font1.FontHeightInPoints = 11;//字号
                    ICellStyle cells = wk.CreateCellStyle();
                    cells.SetFont(font1);

                    for (int j = 0; j < 20; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }
                }

                #endregion

                for (int i = 0; i <= dt.Columns.Count; i++)
                {
                    sheet0.AutoSizeColumn(i);
                }
                sheet0.ForceFormulaRecalculation = true;

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        private static void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
        {
            try
            {
                System.IO.FileInfo path = new System.IO.FileInfo(filename);
                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Workbooks.Close();
                m_xlApp.Quit();
                m_xlApp.Application.Quit();
                CloseExeclProcess(m_xlApp);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);

                wksheet = null;
                workbook = null;
                m_xlApp = null;

                GC.Collect();
                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.Charset = "GB2312";
                System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpContext.Current.Server.UrlEncode(filename));
                System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载 

                System.Web.HttpContext.Current.Response.WriteFile(filename); // 把文件流发送到客户端 
                System.Web.HttpContext.Current.Response.Flush();
                path.Delete();//删除服务器文件
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private static void CloseExeclProcess(Application excelApp)
        {
            #region kill excel process

            System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            foreach (System.Diagnostics.Process p in procs)
            {
                int baseAdd = p.MainModule.BaseAddress.ToInt32();
                //oXL is Excel.ApplicationClass object 
                if (baseAdd == excelApp.Hinstance)
                {
                    p.Kill();
                    break;
                }
            }
            #endregion
        }

        protected void tb_pj_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (tb_pj.Text.ToString().Contains("|"))
            {
                Cname = tb_pj.Text.Substring(0, tb_pj.Text.ToString().IndexOf("|"));
                tb_pj.Text = Cname.Trim();
            }
            else if (tb_pj.Text == "")
            {

            }
        }
        protected void tb_eng_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (tb_eng.Text.ToString().Contains("|"))
            {
                Cname = tb_eng.Text.Substring(0, tb_eng.Text.ToString().IndexOf("|"));
                tb_eng.Text = Cname.Trim();
            }
            else if (tb_eng.Text == "")
            {

            }
        }

        //导出未到货数量清单
        protected void btn_weidaohuo_Click(object sender, EventArgs e)
        {
            string startdate = txtStartTime.Value.ToString() == "" ? "1900-01-01" : txtStartTime.Value.ToString();
            string enddate = txtEndTime.Value.ToString() == "" ? "2100-01-01" : txtEndTime.Value.ToString();
            string sqltext = "";
            string conwhere = "";

            //材质
            if (DropDownList2.SelectedIndex != 0)
            {
                conwhere = "PO_MASHAPE like '" + DropDownList2.SelectedItem.Text + "'";
            }
            else
            {
                conwhere = "PO_MASHAPE like '%%'";
            }

            if (drp_stu.SelectedIndex == 0)  //制单人
            {
                if (tb_pj.Text == "" && tb_eng.Text == "")
                {
                    sqltext = "SELECT   orderno,suppliernm,substring(zdtime,0,11) as zdtime,ptcode,pjnm,engnm,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,marid,marnm, margg,marcz,margb,price,amount,ctprice,ctamount,zxnum,marunit,zxfznum,marfzunit,recgdnum,recdate,(zxnum-recgdnum) as weidhnum,cgtimerq,length,width,detailnote," +
                                "detailstate,detailcstate,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME  " +
                                "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where detailcstate='0' and  (substring(zdtime,0,11) between '" + startdate + "' and '" + enddate + "') and suppliernm like '%" + TextBox1.Text.Trim() + "%'  " +
                                " and margb like '%" + tb_margb.Text.Trim() + "%' and  orderno like '%" + tb_orderno.Text.Trim() + "%' and  marnm like '%" + tb_name.Text.Trim() + "%' and marcz like '%" + tb_cz.Text.Trim() + "%' and margg like '%" + tb_gg.Text.Trim() + "%' and marid like '%" + tb_gb.Text.Trim() + "%' and " + conwhere + " and (zxnum-recgdnum)!=0  order by orderno desc,marnm,margg,ptcode asc";
                }
                else if (tb_pj.Text == "" && tb_eng.Text != "")
                {
                    sqltext = "SELECT   orderno,suppliernm,substring(zdtime,0,11) as zdtime,ptcode,pjnm,engnm,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,marid,marnm, margg,marcz,margb,price,amount,ctprice,ctamount,zxnum,marunit,zxfznum,marfzunit,recgdnum,recdate,(zxnum-recgdnum) as weidhnum,cgtimerq,length,width,detailnote," +
                                "detailstate,detailcstate,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME  " +
                               "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where detailcstate='0' and  (substring(zdtime,0,11) between '" + startdate + "' and '" + enddate + "') and suppliernm like '%" + TextBox1.Text.Trim() + "%'  " +
                               " and margb like '%" + tb_margb.Text.Trim() + "%' and  orderno like '%" + tb_orderno.Text.Trim() + "%' and  marnm like '%" + tb_name.Text.Trim() + "%' and marcz like '%" + tb_cz.Text.Trim() + "%' and margg like '%" + tb_gg.Text.Trim() + "%' and marid like '%" + tb_gb.Text.Trim() + "%'  and engnm like '%" + tb_eng.Text.Trim() + "%' and " + conwhere + " and (zxnum-recgdnum)!=0  order by orderno desc,marnm,margg,ptcode asc";
                }
                else if (tb_pj.Text != "" && tb_eng.Text == "")
                {
                    sqltext = "SELECT   orderno,suppliernm,substring(zdtime,0,11) as zdtime,ptcode,pjnm,engnm,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,marid,marnm, margg,marcz,margb,price,amount,ctprice,ctamount,zxnum,marunit,zxfznum,marfzunit,recgdnum,recdate,(zxnum-recgdnum) as weidhnum,cgtimerq,length,width,detailnote," +
                                   "detailstate,detailcstate,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME  " +
                               "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where detailcstate='0' and  (substring(zdtime,0,11) between '" + startdate + "' and '" + enddate + "') and suppliernm like '%" + TextBox1.Text.Trim() + "%'  " +
                               " and margb like '%" + tb_margb.Text.Trim() + "%' and  orderno like '%" + tb_orderno.Text.Trim() + "%' and  marnm like '%" + tb_name.Text.Trim() + "%' and marcz like '%" + tb_cz.Text.Trim() + "%' and margg like '%" + tb_gg.Text.Trim() + "%' and marid like '%" + tb_gb.Text.Trim() + "%' and pjnm like '%" + tb_pj.Text.Trim() + "%'  and " + conwhere + " and (zxnum-recgdnum)!=0  order by orderno desc,marnm,margg,ptcode asc";
                }
                else if (tb_pj.Text != "" && tb_eng.Text != "")
                {
                    sqltext = "SELECT   orderno,suppliernm,substring(zdtime,0,11) as zdtime,ptcode,pjnm,engnm,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,marid,marnm, margg,marcz,margb,price,amount,ctprice,ctamount,zxnum,marunit,zxfznum,marfzunit,recgdnum,recdate,(zxnum-recgdnum) as weidhnum,cgtimerq,length,width,detailnote," +
                                "detailstate,detailcstate,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME  " +
                                 "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where detailcstate='0' and  (substring(zdtime,0,11) between '" + startdate + "' and '" + enddate + "') and suppliernm like '%" + TextBox1.Text.Trim() + "%'  " +
                                 " and margb like '%" + tb_margb.Text.Trim() + "%' and  orderno like '%" + tb_orderno.Text.Trim() + "%' and  marnm like '%" + tb_name.Text.Trim() + "%' and marcz like '%" + tb_cz.Text.Trim() + "%' and margg like '%" + tb_gg.Text.Trim() + "%' and marid like '%" + tb_gb.Text.Trim() + "%' and pjnm like '%" + tb_pj.Text.Trim() + "%' and engnm like '%" + tb_eng.Text.Trim() + "%' and  " + conwhere + " and (zxnum-recgdnum)!=0  order by orderno desc,marnm,margg,ptcode asc";
                }
            }
            else
            {
                if (tb_pj.Text == "" && tb_eng.Text == "")
                {
                    sqltext = "SELECT   orderno,suppliernm,substring(zdtime,0,11) as zdtime,ptcode,pjnm,engnm,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,marid,marnm, margg,marcz,margb,price,amount,ctprice,ctamount,zxnum,marunit,zxfznum,marfzunit,recgdnum,recdate,(zxnum-recgdnum) as weidhnum,cgtimerq,length,width,detailnote," +
                                "detailstate,detailcstate,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME  " +
                               "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where detailcstate='0' and  (substring(zdtime,0,11) between '" + startdate + "' and '" + enddate + "') and suppliernm like '%" + TextBox1.Text.Trim() + "%' and zdrid like '" + drp_stu.SelectedValue.ToString() + "' " +
                               " and margb like '%" + tb_margb.Text.Trim() + "%' and  orderno like '%" + tb_orderno.Text.Trim() + "%' and  marnm like '%" + tb_name.Text.Trim() + "%' and marcz like '%" + tb_cz.Text.Trim() + "%' and margg like '%" + tb_gg.Text.Trim() + "%' and marid like '%" + tb_gb.Text.Trim() + "%' and " + conwhere + " and (zxnum-recgdnum)!=0  order by orderno desc,marnm,margg,ptcode asc";
                }
                else if (tb_pj.Text == "" && tb_eng.Text != "")
                {
                    sqltext = "SELECT   orderno,suppliernm,substring(zdtime,0,11) as zdtime,ptcode,pjnm,engnm,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,marid,marnm, margg,marcz,margb,price,amount,ctprice,ctamount,zxnum,marunit,zxfznum,marfzunit,recgdnum,recdate,(zxnum-recgdnum) as weidhnum,cgtimerq,length,width,detailnote," +
                                "detailstate,detailcstate,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME  " +
                             "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where detailcstate='0' and  (substring(zdtime,0,11) between '" + startdate + "' and '" + enddate + "') and suppliernm like '%" + TextBox1.Text.Trim() + "%' and zdrid like '" + drp_stu.SelectedValue.ToString() + "' " +
                             " and margb like '%" + tb_margb.Text.Trim() + "%' and  orderno like '%" + tb_orderno.Text.Trim() + "%' and  marnm like '%" + tb_name.Text.Trim() + "%' and marcz like '%" + tb_cz.Text.Trim() + "%' and margg like '%" + tb_gg.Text.Trim() + "%' and marid like '%" + tb_gb.Text.Trim() + "%' and engnm like '%" + tb_eng.Text.Trim() + "%' and " + conwhere + " and (zxnum-recgdnum)!=0  order by orderno desc,marnm,margg,ptcode asc";
                }
                else if (tb_pj.Text != "" && tb_eng.Text == "")
                {
                    sqltext = "SELECT   orderno,suppliernm,substring(zdtime,0,11) as zdtime,ptcode,pjnm,engnm,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,marid,marnm, margg,marcz,margb,price,amount,ctprice,ctamount,zxnum,marunit,zxfznum,marfzunit,recgdnum,recdate,(zxnum-recgdnum) as weidhnum,cgtimerq,length,width,detailnote," +
                                "detailstate,detailcstate,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME  " +
                                 "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where detailcstate='0' and  (substring(zdtime,0,11) between '" + startdate + "' and '" + enddate + "') and suppliernm like '%" + TextBox1.Text.Trim() + "%' and zdrid like '" + drp_stu.SelectedValue.ToString() + "' " +
                                 " and margb like '%" + tb_margb.Text.Trim() + "%' and  orderno like '%" + tb_orderno.Text.Trim() + "%' and  marnm like '%" + tb_name.Text.Trim() + "%' and marcz like '%" + tb_cz.Text.Trim() + "%' and margg like '%" + tb_gg.Text.Trim() + "%' and marid like '%" + tb_gb.Text.Trim() + "%' and pjnm like '%" + tb_pj.Text.Trim() + "%'  and  " + conwhere + " and (zxnum-recgdnum)!=0 order by orderno desc,marnm,margg,ptcode asc";
                }
                else if (tb_pj.Text != "" && tb_eng.Text != "")
                {
                    sqltext = "SELECT   orderno,suppliernm,substring(zdtime,0,11) as zdtime,ptcode,pjnm,engnm,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,marid,marnm, margg,marcz,margb,price,amount,ctprice,ctamount,zxnum,marunit,zxfznum,marfzunit,recgdnum,recdate,(zxnum-recgdnum) as weidhnum,cgtimerq,length,width,detailnote," +
                                "detailstate,detailcstate,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME  " +
                                 "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where detailcstate='0' and (substring(zdtime,0,11) between '" + startdate + "' and '" + enddate + "') and suppliernm like '%" + TextBox1.Text.Trim() + "%' and zdrid like '" + drp_stu.SelectedValue.ToString() + "' " +
                                 " and margb like '%" + tb_margb.Text.Trim() + "%' and  orderno like '%" + tb_orderno.Text.Trim() + "%' and  marnm like '%" + tb_name.Text.Trim() + "%' and marcz like '%" + tb_cz.Text.Trim() + "%' and margg like '%" + tb_gg.Text.Trim() + "%' and marid like '%" + tb_gb.Text.Trim() + "%' and pjnm like '%" + tb_pj.Text.Trim() + "%' and engnm like '%" + tb_eng.Text.Trim() + "%' and  " + conwhere + " and (zxnum-recgdnum)!=0 order by orderno desc,marnm,margg,ptcode asc";
                }

            }
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DataView dv = null;
            if (tb_th.Text != "")//图号
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PO_TUHAO like '%" + tb_th.Text + "%'";
                dt = dv.ToTable();
            }
            if (dt.Rows.Count > 10000)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导出的数据量超过10000条，请添加导出条件，分多次导出！');", true);
            }
            else
            {
                ExportWDData(dt);
            }
        }

        private void ExportWDData(System.Data.DataTable dt)
        {
            Object Opt = System.Type.Missing;
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("采购订单未到货明细表模版") + ".xls", Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt, Opt); ;

            m_xlApp.Visible = false;     // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);
            //根据批号查询数据


            //设置工作薄名称


            ////填充数据

            int rowCount = dt.Rows.Count;

            int colCount = dt.Columns.Count;

            object[,] dataArray = new object[rowCount, colCount];

            for (int i = 0; i < rowCount; i++)
            {
                dataArray[i, 0] = i + 1;
                for (int j = 0; j < colCount - 2; j++)
                {
                    dataArray[i, j + 1] = dt.Rows[i][j];
                }
            }

            wksheet.get_Range("A3", wksheet.Cells[rowCount + 2, colCount - 1]).Value2 = dataArray;
            wksheet.get_Range("A3", wksheet.Cells[rowCount + 2, colCount - 1]).Borders.LineStyle = 1;

            ////设置列宽
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应
            string filename = System.Web.HttpContext.Current.Server.MapPath("采购订单未到货明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }
    }
}
