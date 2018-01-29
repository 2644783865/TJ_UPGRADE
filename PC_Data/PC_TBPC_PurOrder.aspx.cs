using System;
using System.Collections;
using System.Collections.Generic;
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
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_PurOrder : System.Web.UI.Page
    {
        public string gloabsheetno
        {
            get
            {
                object str = ViewState["gloabsheetno"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabsheetno"] = value;
            }
        }
        public string gloabptc
        {
            get
            {
                object str = ViewState["gloabptc"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabptc"] = value;
            }
        }
        public string gloabflag
        {
            get
            {
                object str = ViewState["gloabflag"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabflag"] = value;
            }
        }
        public string NUM1
        {
            get
            {
                object str = ViewState["NUM1"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["NUM1"] = value;
            }
        }
        public string NUM2
        {
            get
            {
                object str = ViewState["NUM2"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["NUM2"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_nosto.Attributes.Add("style", "display:none");
            btn_shanchu.Attributes.Add("OnClick", "Javascript:return confirm('将删除整个订单，是否确定删除?删除之后会保留历史记录！');");
            btn_shangcha.Attributes.Add("onclick", "form.target='_blank'");
            btn_xiacha.Attributes.Add("onclick", "form.target='_blank'");
            
            //btn_biangeng.Attributes.Add("onclick", "form.target='_blank'");
            if (!IsPostBack)
            {

                if (Request.QueryString["orderno"] != null)
                {
                    gloabsheetno = Request.QueryString["orderno"].ToString();
                }
                else
                {
                    gloabsheetno = "";
                }
                if (Request.QueryString["ptc"] != null)
                {
                    gloabptc = Server.UrlDecode(Request.QueryString["ptc"].ToString());
                }
                else
                {
                    gloabptc = "";
                }
                if (Request.QueryString["FLAG"] != null)
                {
                    gloabflag = Request.QueryString["FLAG"].ToString();
                }
                else
                {
                    gloabflag = "";
                }
                if (Request.QueryString["num1"] != null)
                {
                    NUM1 = Request.QueryString["num1"].ToString();
                }
                else
                {
                    NUM1 = "";
                }
                if (Request.QueryString["num2"] != null)
                {
                    NUM2 = Request.QueryString["num2"].ToString();
                }
                else
                {
                    NUM2 = "";
                }
                Hyp_print.NavigateUrl = "PC_TBPC_PurOrderprint.aspx?orderno=" + gloabsheetno;
                initial();

            }
            //CheckUser(ControlFinder);
        }
        //页面初始化,请在此处做修改！！
        protected void initial()
        {
            initpage();
            initpower();
            repeaterdatabind();
        }
        private void initpage()
        {
            string orderno = gloabsheetno;
                        
            string sqltext = "";
            sqltext = "SELECT orderno AS Code,supplierid,suppliernm AS Supplier,substring(zdtime,1,10) AS Date,abstract AS Abstract," +
                "versionno AS VersionNo,changdate AS ChangeDate,changmanid,changnm AS ChangeMan,changreason AS ChangeReason," +
                "zdrid AS Documentid,zdrnm AS Document,ywyid AS ClerkID,ywynm AS Clerk,depid AS DepID,depnm AS Dep, " +
                "zgid AS ManagerID,zgnm AS Manager,totalnote AS note,isnull(totalstate,0) as state,isnull(totalcstate,0) as tcstate  " +
                "FROM View_TBPC_PURORDERTOTAL WHERE orderno='" + orderno + "'";
            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt1.Rows.Count > 0)
            {
                LabelCode.Text = dt1.Rows[0]["Code"].ToString();
                LabelDate.Text = dt1.Rows[0]["Date"].ToString();
                supplierid.Value = dt1.Rows[0]["supplierid"].ToString();
                LabelSupplier.Text = dt1.Rows[0]["Supplier"].ToString();
                LabelAbstract.Text = dt1.Rows[0]["Abstract"].ToString();
                LabelVersionNo.Text = dt1.Rows[0]["VersionNo"].ToString();
                LabelChangeDate.Text = dt1.Rows[0]["ChangeDate"].ToString();
                LabelChangeMan.Text = dt1.Rows[0]["ChangeMan"].ToString();
                LabelChangeReason.Text = dt1.Rows[0]["ChangeReason"].ToString();
                LabelManager.Text = dt1.Rows[0]["Manager"].ToString();
                LabelDep.Text = dt1.Rows[0]["Dep"].ToString();
                LabelClerk.Text = dt1.Rows[0]["Clerk"].ToString();
                LabelDocument.Text = dt1.Rows[0]["Document"].ToString();
                LabelDocumentid.Text = dt1.Rows[0]["Documentid"].ToString();
                tb_state.Text = dt1.Rows[0]["state"].ToString();
                tb_cstate.Text = dt1.Rows[0]["tcstate"].ToString();
                Tb_note.Text = dt1.Rows[0]["note"].ToString();
                if (Convert.ToInt32(tb_state.Text) >= 1)//审核通过
                {
                    ImageVerify.Visible = true;
                }
                else
                {
                    ImageVerify.Visible = false;
                }
            }
        }

        private void initpower()
        {
            if (Session["UserDeptID"].ToString() == "06" || Session["UserDeptID"].ToString() == "07")
            {
                btn_dhqr.Visible = true;
            }
            else
            {
                btn_dhqr.Visible = false;
            }
            if (gloabflag == "PUSHREAD")
            {
                btn_fanshen.Visible = false;
                //btn_finish.Visible = false;
                btn_shangcha.Visible = false;
                btn_xiacha.Visible = false;
                //btn_biangeng.Visible = false;
                //关闭与备库
                btn_close.Visible = false;
            }
            else
            {

                btn_fanshen.Visible = true;
                //btn_finish.Visible = true;
                btn_shangcha.Visible = true;
                btn_xiacha.Visible = true;
                //btn_biangeng.Visible = true;

            }
            if (tb_cstate.Text == "1" || tb_state.Text == "3")//关闭和终结均不能在进行反审和终结操作
            {
                btn_fanshen.Visible = false;
                //btn_finish.Visible = false;
            }
            if (tb_state.Text != "0" || LabelDocumentid.Text != Session["UserID"].ToString())//制单人不是登录人不能编辑提交
            {
                btn_edit.Enabled = false;
                btn_tijiao.Enabled = false;
            }
            else
            {
                btn_edit.Enabled = true;
                btn_tijiao.Enabled = true;
            }
            if (tb_state.Text == "1" && LabelDocumentid.Text == Session["UserID"].ToString())//
            {
                btn_fanshen.Enabled = true;
            }
            else
            {
                btn_fanshen.Enabled = false;
            }
            if (tb_cstate.Text == "2")
            {
                btn_dhqr.Enabled = false;
                btn_shanchu.Text = "取消删除";
                btn_marrep.Enabled = false;
                btn_edit.Enabled = false;
                btn_tijiao.Enabled = false;
                btn_fanshen.Enabled = false;
                btn_shangcha.Enabled = false;
                btn_xiacha.Enabled = false;
                btn_close.Enabled = false;
                btn_baojian.Enabled = false;
            }
        }
        private void repeaterdatabind()
        {
            string sqltext = "";
            string orderno = gloabsheetno;
            sqltext = "SELECT orderno as Code,picno as irqsheet,ptcode as PlanCode ," +
                     "marid as MaterialCode,marnm as MaterialName,margg as MaterialStandard," +
                     "marcz as MaterialTexture,margb as MaterialGb,marunit as Unit,convert(float,num) as Number," +
                     "convert(float,zxnum) as zxnum,case when margb='' then PO_TUHAO else '' end as PO_TUHAO," +
                     "convert(float,fznum) as fznum,convert(float,zxfznum) as zxFznum,marfzunit as Fzunit,cgtimerq as Cgtimerq," +
                     "recgdnum as arrivedNumber,PO_PZ,recdate as Rptime,convert(float,price) as UnitPrice,convert(float,ctprice) as CTUP," +
                     "convert(float,amount) as Amount,taxrate as TaxRate,convert(float,ctamount) as PricePlusTax," +
                     "detailnote as Comment,planmode as PlanMode,ptcode as ptcode,length as Length," +
                     "width as Width,keycoms as keycoms,detailstate as PO_STATE,detailcstate as pocstate," +
                     "engid as engid,pjid,PO_CGFS,PO_MASHAPE,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME,PO_IFFAST as 'IFFAST'  " +
                     "FROM View_TBPC_PURORDERDETAIL_PLAN WHERE orderno='" + orderno + "' and (detailcstate='0' or detailcstate='2') order by ptcode,marnm,margg asc";

            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            tbpc_order_detailRepeater.DataSource = dt;
            tbpc_order_detailRepeater.DataBind();
            if (tbpc_order_detailRepeater.Items.Count > 0)
            {
                NoDataPane.Visible = false;
            }
            else
            {
                NoDataPane.Visible = true;
            }
        }

        protected void goback_Click(object sender, EventArgs e)
        {
            comeback();
        }
        protected void btn_export_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext = "SELECT orderno, marnm, margg, margb, marcz, marunit, marfzunit, picno, ptcode, marid, num, fznum, zxnum, zxfznum, cgtimerq, " +
                          "recdate, recgdnum, recgdfznum, taxrate, detailnote, keycoms, detailstate, detailcstate, whstate, length, width, fixed, pjid, " +
                          "pjnm, engid, engnm, ctprice, planmode, ctamount, price, amount, supplierid, suppliernm, zdrid, zdrnm, substring(zdtime,0,11) as zdtime, shrid, " +
                          "shrnm, shtime, ywyid, ywynm, zgid, zgnm, depid, depnm, totalstate, totalcstate, totalnote, fax, phono, conname, abstract, " +
                          "PO_MASHAPE,PO_PZ,case when margb='' then PO_TUHAO else '' end as PO_TUHAO, PO_OperateState,PO_MAP,PO_TECUNIT,PO_CHILDENGNAME  " +
                          "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where orderno='" + LabelCode.Text + "' and detailcstate='0' order by ptcode,marnm,margg asc";
            //"from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where orderno='"+LabelCode.Text+"' and detailcstate='0' order by ptcode asc";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int m = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["PO_MASHAPE"].ToString().Contains("板") || dt.Rows[i]["PO_MASHAPE"].ToString().Contains("定尺板") || dt.Rows[i]["PO_MASHAPE"].ToString().Contains("圆") || dt.Rows[i]["PO_MASHAPE"].ToString().Contains("型"))
                {
                    m++;
                }
            }
            if (m == dt.Rows.Count)
            {
                ExportDataItemgc(dt);
            }
            else if (m == 0)
            {
                ExportDataItemfgc(dt);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导出数据中包含钢材和非钢材，无法导出！！！');", true);
            }

        }

        private void ExportDataItemgc(System.Data.DataTable dt)
        {
            #region
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //Worksheet wksheet;
            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("采购订单明细表模版") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //m_xlApp.Visible = false;    // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            //wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            //System.Data.DataTable dt = objdt;

            //// 填充数据
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    wksheet.Cells[i + 3, 1] = Convert.ToString(i + 1);//序号

            //    wksheet.Cells[i + 3, 2] = "'" + dt.Rows[i]["orderno"].ToString();//订单编号

            //    wksheet.Cells[i + 3, 3] = "'" + dt.Rows[i]["suppliernm"].ToString();//供应商

            //    wksheet.Cells[i + 3, 4] = "'" + dt.Rows[i]["zdtime"].ToString();//供应商

            //    wksheet.Cells[i + 3, 5] = "'" + dt.Rows[i]["ptcode"].ToString();//计划跟踪号

            //    wksheet.Cells[i + 3, 6] = "'" + dt.Rows[i]["pjnm"].ToString();//项目名称

            //    wksheet.Cells[i + 3, 7] = "'" + dt.Rows[i]["engnm"].ToString();//工程名称

            //    wksheet.Cells[i + 3, 8] = "'" + dt.Rows[i]["PO_TUHAO"].ToString();//图号/标识号

            //    wksheet.Cells[i + 3, 9] = "'" + dt.Rows[i]["marid"].ToString();//物料编码

            //    wksheet.Cells[i + 3, 10] = "'" + dt.Rows[i]["marnm"].ToString();//名称

            //    wksheet.Cells[i + 3, 11] = "'" + dt.Rows[i]["margg"].ToString();//规格

            //    wksheet.Cells[i + 3, 12] = "'" + dt.Rows[i]["marcz"].ToString();//材质

            //    wksheet.Cells[i + 3, 13] = "'" + dt.Rows[i]["margb"].ToString();//国标

            //    wksheet.Cells[i + 3, 14] = dt.Rows[i]["price"].ToString();//单价

            //    wksheet.Cells[i + 3, 15] = dt.Rows[i]["amount"].ToString();//金额

            //    wksheet.Cells[i + 3, 16] = dt.Rows[i]["ctprice"].ToString();//含税单价

            //    //wksheet.Cells[i + 3, 16] = "'" + dt.Rows[i]["taxrate"].ToString();//税率

            //    wksheet.Cells[i + 3, 17] = dt.Rows[i]["ctamount"].ToString();//价税合计

            //    wksheet.Cells[i + 3, 18] = dt.Rows[i]["zxnum"].ToString();//采购数量

            //    wksheet.Cells[i + 3, 19] = "'" + dt.Rows[i]["marunit"].ToString();//单位

            //    wksheet.Cells[i + 3, 20] = dt.Rows[i]["zxfznum"].ToString();//辅助数量

            //    wksheet.Cells[i + 3, 21] = "'" + dt.Rows[i]["marfzunit"].ToString();//辅助单位

            //    wksheet.Cells[i + 3, 22] = dt.Rows[i]["recgdnum"].ToString();//已到货数量

            //    wksheet.Cells[i + 3, 23] = "'" + dt.Rows[i]["recdate"].ToString();//交货日期

            //    wksheet.Cells[i + 3, 24] = "'" + dt.Rows[i]["cgtimerq"].ToString();//交货日期

            //    wksheet.Cells[i + 3, 25] = "'" + dt.Rows[i]["length"].ToString();//长度

            //    wksheet.Cells[i + 3, 26] = "'" + dt.Rows[i]["width"].ToString();//宽度

            //    wksheet.Cells[i + 3, 27] = "'" + dt.Rows[i]["detailnote"].ToString();//备注

            //    wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 27]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            //    wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 27]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            //    wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 27]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            //}
            ////设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            //string filename = Server.MapPath("/PC_Data/ExportFile/" + "采购订单明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
            #endregion
            string filename = "订单" + LabelCode.Text + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("采购订单钢材类.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据
                IRow row1 = sheet0.GetRow(1);
                IRow row10 = sheet0.GetRow(10);
                IRow row13 = sheet0.GetRow(13);
                row1.GetCell(18).SetCellValue(dt.Rows[0]["orderno"].ToString());//订单编号
                row10.GetCell(1).SetCellValue(dt.Rows[0]["suppliernm"].ToString());//供应商
                row13.GetCell(17).SetCellValue(dt.Rows[0]["zdtime"].ToString());//制单日期
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 18);
                    row.HeightInPoints = 14;//行高
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
                    row.CreateCell(2).SetCellValue(dt.Rows[i]["marnm"].ToString());//物料名称
                    row.CreateCell(3).SetCellValue(dt.Rows[i]["PO_TUHAO"].ToString());//图号
                    row.CreateCell(4).SetCellValue(dt.Rows[i]["margg"].ToString());//规格
                    row.CreateCell(5).SetCellValue(dt.Rows[i]["marcz"].ToString());//材质
                    row.CreateCell(6).SetCellValue(dt.Rows[i]["margb"].ToString());//国标
                    row.CreateCell(7).SetCellValue(dt.Rows[i]["PO_MASHAPE"].ToString());//类型
                    row.CreateCell(8).SetCellValue(dt.Rows[i]["marunit"].ToString());//单位
                    row.CreateCell(9).SetCellValue(dt.Rows[i]["zxnum"].ToString());//采购数量
                    row.CreateCell(10).SetCellValue(dt.Rows[i]["zxfznum"].ToString());//辅助数量
                    row.CreateCell(11).SetCellValue(dt.Rows[i]["length"].ToString());//长度
                    row.CreateCell(12).SetCellValue(dt.Rows[i]["width"].ToString());//宽度
                    row.CreateCell(13).SetCellValue(dt.Rows[i]["PO_PZ"].ToString());//片/支
                    row.CreateCell(14).SetCellValue(dt.Rows[i]["ctprice"].ToString());//含税单价
                    row.CreateCell(15).SetCellValue(dt.Rows[i]["ctamount"].ToString());//加税合计
                    row.CreateCell(16).SetCellValue(dt.Rows[i]["cgtimerq"].ToString());//交货日期
                    row.CreateCell(17).SetCellValue(dt.Rows[i]["detailnote"].ToString());//备注
                    row.CreateCell(18).SetCellValue(dt.Rows[i]["ptcode"].ToString());//计划跟踪号


                    #region
                    //row.CreateCell(5).SetCellValue(dt.Rows[i]["margb"].ToString());//国标
                    //row.CreateCell(6).SetCellValue(dt.Rows[i]["PO_TUHAO"].ToString());//图号/标识号
                    //row.CreateCell(3).SetCellValue(dt.Rows[i]["PO_CHILDENGNAME"].ToString());//部件名称
                    //row.CreateCell(7).SetCellValue(dt.Rows[i]["margg"].ToString());//规格
                    //row.CreateCell(8).SetCellValue(dt.Rows[i]["marcz"].ToString());//材质
                    //row.CreateCell(13).SetCellValue(dt.Rows[i]["PO_TECUNIT"].ToString());//辅助单位
                    //row.CreateCell(19).SetCellValue(dt.Rows[i]["price"].ToString());//单价（不含税）
                    //row.CreateCell(20).SetCellValue(dt.Rows[i]["amount"].ToString());//金额（不含税）
                    //row.CreateCell(22).SetCellValue(dt.Rows[i]["recdate"].ToString());//到货日期
                    //row.CreateCell(25).SetCellValue(dt.Rows[i]["taxrate"].ToString());//税率
                    //row.CreateCell(26).SetCellValue(dt.Rows[i]["recgdnum"].ToString());//已到货数量
                    #endregion

                    NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                    font1.FontName = "仿宋";//字体
                    font1.FontHeightInPoints = 11;//字号
                    ICellStyle cells = wk.CreateCellStyle();
                    cells.SetFont(font1);
                    cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                    for (int j = 0; j <= 18; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }
                }

                #endregion

                //页脚信息
                IRow row20 = sheet0.CreateRow(dt.Rows.Count + 20);
                row20.CreateCell(1).SetCellValue("部门：");
                row20.CreateCell(2).SetCellValue("采购部");
                row20.CreateCell(5).SetCellValue("部长：");
                row20.CreateCell(6).SetCellValue("高浩");
                row20.CreateCell(8).SetCellValue("业务员：");
                row20.CreateCell(9).SetCellValue(dt.Rows[0]["zdrnm"].ToString());
                row20.CreateCell(11).SetCellValue("制单：");
                row20.CreateCell(12).SetCellValue(dt.Rows[0]["zdrnm"].ToString());

                for (int i = 0; i <= 18; i++)
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
        private void ExportDataItemfgc(System.Data.DataTable dt)
        {
            string filename = "订单" + LabelCode.Text + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream

            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("采购订单非钢材类.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据
                IRow row1 = sheet0.GetRow(1);
                IRow row10 = sheet0.GetRow(10);
                IRow row13 = sheet0.GetRow(13);
                row1.GetCell(16).SetCellValue(dt.Rows[0]["orderno"].ToString());//订单编号
                row10.GetCell(2).SetCellValue(dt.Rows[0]["suppliernm"].ToString());//供应商
                row13.GetCell(15).SetCellValue(dt.Rows[0]["zdtime"].ToString());//制单日期
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 18);
                    row.HeightInPoints = 14;//行高
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号
                    row.CreateCell(1).SetCellValue(dt.Rows[i]["marid"].ToString());//物料编码
                    row.CreateCell(2).SetCellValue(dt.Rows[i]["marnm"].ToString());//物料名称
                    //row.CreateCell(3).SetCellValue(dt.Rows[i]["PO_TUHAO"].ToString() + " " + dt.Rows[i]["margg"].ToString() + " " + dt.Rows[i]["marcz"].ToString() + " " + dt.Rows[i]["margb"].ToString());//图号规格材质国标
                    row.CreateCell(3).SetCellValue(dt.Rows[i]["PO_TUHAO"].ToString());//图号
                    row.CreateCell(4).SetCellValue(dt.Rows[i]["margg"].ToString());//规格
                    row.CreateCell(5).SetCellValue(dt.Rows[i]["marcz"].ToString());//材质
                    row.CreateCell(6).SetCellValue(dt.Rows[i]["margb"].ToString());//国标
                    row.CreateCell(7).SetCellValue(dt.Rows[i]["PO_MASHAPE"].ToString());//类型
                    row.CreateCell(8).SetCellValue(dt.Rows[i]["marunit"].ToString());//单位
                    row.CreateCell(9).SetCellValue(dt.Rows[i]["zxnum"].ToString());//采购数量
                    row.CreateCell(10).SetCellValue(dt.Rows[i]["marfzunit"].ToString());//辅助单位
                    row.CreateCell(11).SetCellValue(dt.Rows[i]["zxfznum"].ToString());//辅助数量
                    //row.CreateCell(8).SetCellValue(dt.Rows[i]["length"].ToString());//长度
                    //row.CreateCell(9).SetCellValue(dt.Rows[i]["width"].ToString());//宽度
                    //row.CreateCell(10).SetCellValue(dt.Rows[i]["PO_PZ"].ToString());//片/支
                    row.CreateCell(12).SetCellValue(dt.Rows[i]["ctprice"].ToString());//含税单价
                    row.CreateCell(13).SetCellValue(dt.Rows[i]["ctamount"].ToString());//加税合计
                    row.CreateCell(14).SetCellValue(dt.Rows[i]["cgtimerq"].ToString());//交货日期
                    row.CreateCell(15).SetCellValue(dt.Rows[i]["detailnote"].ToString());//备注
                    row.CreateCell(16).SetCellValue(dt.Rows[i]["ptcode"].ToString());//计划跟踪号

                    NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                    font1.FontName = "仿宋";//字体
                    font1.FontHeightInPoints = 11;//字号
                    ICellStyle cells = wk.CreateCellStyle();
                    cells.SetFont(font1);
                    cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                    for (int j = 0; j <= 16; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }
                }

                #endregion
                //页脚信息
                IRow row20 = sheet0.CreateRow(dt.Rows.Count + 20);
                row20.CreateCell(1).SetCellValue("部门：");
                row20.CreateCell(2).SetCellValue("采购部");
                row20.CreateCell(5).SetCellValue("部长：");
                row20.CreateCell(6).SetCellValue("高浩");
                row20.CreateCell(8).SetCellValue("业务员：");
                row20.CreateCell(9).SetCellValue(dt.Rows[0]["zdrnm"].ToString());
                row20.CreateCell(11).SetCellValue("制单：");
                row20.CreateCell(12).SetCellValue(dt.Rows[0]["zdrnm"].ToString());


                for (int i = 0; i <= 16; i++)
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
                contextResponse.Redirect(string.Format("~/PC_Data/ExportFile/{0}", path.Name), false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        protected void comeback()
        {
            if (gloabflag == "PUSHREAD")
            {
                //仓库
                Response.Redirect("~/SM_Data/SM_WarehouseIN_WGPUSH.aspx?FLAG=PUSH");
            }
            else
            {
                Response.Redirect("~/PC_Data/TBPC_Purordertotal_list.aspx?num1=" + NUM1 + "&num2=" + NUM2 + "");
            }
        }
        protected void btn_edit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PC_Data/TBPC_Purorderdetail_xiugai.aspx?orderno=" + gloabsheetno + "");
        }
        protected void btn_tijiao_Click(object sender, EventArgs e)//审核完毕
        {
            string sqltext = "";
            //string keycoms = "";
            //string marid = "";
            int temp = cansave();
            if (temp == 0)
            {
                btn_edit.Enabled = false;
                List<string> sqltextlist = new List<string>();
                sqltext = "update TBPC_PURORDERTOTAL set PO_STATE='1',PO_SHTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                          "',PO_SHID='" + Session["UserID"].ToString() + "'  " +
                          "where PO_CODE='" + LabelCode.Text + "' and PO_STATE='0'";
                sqltextlist.Add(sqltext);
                //DBCallCommon.ExeSqlText(sqltext);
                sqltext = "update TBPC_PURORDERDETAIL set PO_STATE='1' where PO_CODE='" + LabelCode.Text + "' and PO_STATE='0'";
                //DBCallCommon.ExeSqlText(sqltext);
                sqltextlist.Add(sqltext);
                DBCallCommon.ExecuteTrans(sqltextlist);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交成功！');", true);
                initial();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('采购时间没有指定，操作失败！');", true);
            }
        }

        protected void btn_nosto_Click(object sender, EventArgs e)//无库存可用
        {
            Response.Redirect("~/PC_Data/TBPC_Purordertotal_list.aspx?num1=" + NUM1 + "&num2=" + NUM2 + "");
        }

        private int cansave()
        {
            int temp = 0;
            string cgrqtime = "";
            foreach (RepeaterItem retim in tbpc_order_detailRepeater.Items)
            {
                cgrqtime = ((System.Web.UI.WebControls.Label)retim.FindControl("Cgtimerq")).Text;
                if (cgrqtime == "")
                {
                    temp = 1;
                    break;
                }
            }
            return temp;
        }
        protected void btn_fanshen_Click(object sender, EventArgs e)
        {
            string sqltext1 = "";
            sqltext1 = "update TBPC_PURORDERTOTAL set PO_STATE='0' where PO_CODE='" + LabelCode.Text + "'";
            DBCallCommon.ExeSqlText(sqltext1);
            //comeback();
            Response.Redirect("~/PC_Data/TBPC_Purorderdetail_xiugai.aspx?orderno=" + LabelCode.Text + "");
        }
        private bool canfanshen()
        {
            bool temp = true;
            string sqltext = "";
            sqltext = "select count(*) from TBWS_STORAGE where SQ_ORDERID='" + gloabsheetno + "'";//关联入库单，判断是否入库
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                if (Convert.ToInt32(dr[0].ToString()) > 0)
                {
                    temp = false;
                }
            }
            dr.Close();
            return temp;
        }
        protected void btn_finish_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            foreach (RepeaterItem retim in tbpc_order_detailRepeater.Items)
            {
                string ptcode = ((System.Web.UI.WebControls.Label)retim.FindControl("PlanCode")).Text;
                System.Web.UI.WebControls.CheckBox cbx = retim.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    sqltext = "update TBPC_PURORDERDETAIL set PO_STATE='2' where PO_PTCODE='" + ptcode + "' and PO_CODE='" + LabelCode.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                }
            }
            sqltext = "SELECT PO_ID FROM TBPC_PURORDERDETAIL WHERE PO_CODE='" + LabelCode.Text + "' and PO_CSTATE!='0' and PO_STATE!='2'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count == 0)
            {
                sqltext = "update TBPC_PURORDERTOTAL set PO_STATE='3' where PO_CODE='" + LabelCode.Text + "'";
                DBCallCommon.ExeSqlText(sqltext);
            }
            comeback();
        }//完结
        protected void btn_shangcha_Click(object sender, EventArgs e)
        {
            int i = 0;
            string ptcode = "";
            string irqsheetno = "";
            foreach (RepeaterItem retim in tbpc_order_detailRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = retim.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        ptcode = ((System.Web.UI.WebControls.Label)retim.FindControl("PlanCode")).Text;
                        irqsheetno = ((System.Web.UI.WebControls.Label)retim.FindControl("irqsheet")).Text;
                    }
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
                return;
            }
            else if (i > 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择一条记录！');", true);
                return;
            }
            else
            {
                if (LabelCode.Text.Contains("ZB"))
                {
                    Response.Redirect("~/PC_Data/PC_TBPC_Toubiao_Manage.aspx?ptc=" + ptcode + "");
                }
                else
                {
                    Response.Redirect("~/PC_Data/TBPC_IQRCMPPRCLST_checked_detail.aspx?sheetno=" + irqsheetno + "&ptc=" + ptcode + "");
                }


                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "pur_bjdshangcha('" + ptcode + "');", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "<script>window.open('~/PC_Data/TBPC_IQRCMPPRCLST_checked.aspx?ptc="+ptcode+"')</script>", true);

            }
        }

        protected void btn_xiacha_Click(object sender, EventArgs e)//下查
        {
            int i = 0;
            string ptcode = "";
            foreach (RepeaterItem temrow in tbpc_order_detailRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = temrow.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        ptcode = ((System.Web.UI.WebControls.Label)temrow.FindControl("PlanCode")).Text;
                    }
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
                return;
            }
            else if (i > 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择一条记录！');", true);
                return;
            }
            else
            {
                Response.Redirect("~/SM_Data/SM_WarehouseIn_Manage.aspx?ptcode=" + ptcode + "");
            }
        }

        //protected void btn_biangeng_Click(object sender, EventArgs e)
        //{
        //int i = 0;
        //string ptcode = "";
        //foreach (GridViewRow temrow in GridView1.Rows)
        //{
        //    CheckBox cbx = temrow.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
        //    if (cbx != null)//存在行
        //    {
        //        if (cbx.Checked)
        //        {
        //            i++;
        //            ptcode = ((Label)temrow.FindControl("PlanCode")).Text;
        //        }
        //    }
        //}
        //if (i == 0)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
        //    return;
        //}
        //else if (i > 1)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择一条记录！');", true);
        //    return;
        //}
        //else
        //{
        //    Response.Redirect("");
        //}
        //}
        //protected void OnSelect(object sender, EventArgs e)
        //{

        //    lb_close.Text = ((LinkButton)sender).Text;

        //}
        protected void hclose()//行关闭
        {
            int i = 0;
            string sqltext = "";
            foreach (RepeaterItem Reitem in tbpc_order_detailRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        sqltext = "update TBPC_PURORDERDETAIL set PO_CSTATE='1' WHERE PO_CODE='" + gloabsheetno + "' " +
                                  "and PO_PCODE='" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("PlanCode")).Text.ToString() + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            if (i > 0)
            {
                sqltext = "SELECT PO_ID FROM TBPC_PURORDERDETAIL WHERE PO_CODE='" + gloabsheetno + "' and PO_CSTATE='0'";//是否还存在未关闭的，如果都关闭则整单关闭
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    sqltext = "update TBPC_PURORDERTOTAL set PO_CSTATE='1'  WHERE PO_CODE='" + gloabsheetno + "'";//单号关闭
                    DBCallCommon.ExeSqlText(sqltext);
                }
                repeaterdatabind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);

            }
        }
        protected void fhclose()//行反关闭
        {
            int i = 0;
            string sqltext = "";
            foreach (RepeaterItem Reitem in tbpc_order_detailRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        sqltext = "update TBPC_PURORDERDETAIL set PO_CSTATE='0' WHERE PO_CODE='" + gloabsheetno + "' " +
                                  "and PO_PCODE='" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("PlanCode")).Text.ToString() + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            if (i > 0)
            {
                sqltext = "SELECT PO_ID FROM TBPC_PURORDERDETAIL WHERE PO_CODE='" + gloabsheetno + "' and PO_CSTATE='0'";//是否还存在未关闭的，如果都存则整单未关闭
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    sqltext = "update TBPC_PURORDERTOTAL set PO_CSTATE='0'  WHERE PO_CODE='" + gloabsheetno + "'";//单号反关闭
                    DBCallCommon.ExeSqlText(sqltext);
                }
                repeaterdatabind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);

            }
        }
        protected void allclose()
        {
            string sqltext = "";
            List<string> sqltexts = new List<string>();
            sqltext = "update TBPC_PURORDERTOTAL set PO_CSTATE='1'  WHERE PO_CODE='" + gloabsheetno + "'";//单号关闭
            sqltexts.Add(sqltext);
            sqltext = "update  TBPC_PURORDERDETAIL set PO_CSTATE='1' WHERE PO_CODE='" + gloabsheetno + "'";//条目关闭
            sqltexts.Add(sqltext);
            DBCallCommon.ExecuteTrans(sqltexts);
        }//单子关闭
        protected void fallclose()
        {
            string sqltext = "";
            List<string> sqltexts = new List<string>();
            sqltext = "update TBPC_PURORDERTOTAL set PO_CSTATE='0'  WHERE PO_CODE='" + gloabsheetno + "'";//单号反关闭
            sqltexts.Add(sqltext);
            sqltext = "update  TBPC_PURORDERDETAIL set PO_CSTATE='0' WHERE PO_CODE='" + gloabsheetno + "'";//条目反关闭
            sqltexts.Add(sqltext);
            DBCallCommon.ExecuteTrans(sqltexts);
        }//单子反关闭

        private double sum = 0;
        private double num = 0;
        private double zxnum = 0;
        private decimal total = 0;
        private double recievenum = 0;
        protected void tbpc_order_detailRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string IFFAST = ((System.Web.UI.WebControls.Label)e.Item.FindControl("IFFAST")).Text.Trim();
                if (IFFAST == "1")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("lbUrgency")).Visible = true;
                }
            }
            if (e.Item.ItemIndex >= 0)
            {

                if (((System.Web.UI.WebControls.Label)e.Item.FindControl("PricePlusTax")).Text.ToString() == System.DBNull.Value.ToString() || ((System.Web.UI.WebControls.Label)e.Item.FindControl("PricePlusTax")).Text.ToString() == System.String.Empty || ((System.Web.UI.WebControls.Label)e.Item.FindControl("PricePlusTax")).Text.ToString() == "0")
                {
                    total = total + 0;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PricePlusTax")).Text = "0";
                }
                else
                {
                    total += Convert.ToDecimal(((System.Web.UI.WebControls.Label)e.Item.FindControl("PricePlusTax")).Text.ToString());
                }
                if (((System.Web.UI.WebControls.Label)e.Item.FindControl("Amount")).Text.ToString() == System.DBNull.Value.ToString() || ((System.Web.UI.WebControls.Label)e.Item.FindControl("Amount")).Text.ToString() == System.String.Empty || ((System.Web.UI.WebControls.Label)e.Item.FindControl("Amount")).Text.ToString() == "0")
                {
                    sum = sum + 0;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("Amount")).Text = "0";
                }
                else
                {
                    sum += Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("Amount")).Text.ToString());
                }
                if (((System.Web.UI.WebControls.Label)e.Item.FindControl("Number")).Text.ToString() == System.DBNull.Value.ToString() || ((System.Web.UI.WebControls.Label)e.Item.FindControl("Number")).Text.ToString() == System.String.Empty || ((System.Web.UI.WebControls.Label)e.Item.FindControl("Number")).Text.ToString() == "0")
                {
                    num = num + 0;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("Number")).Text = "0";
                }
                else
                {
                    num += Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("Number")).Text.ToString());
                }
                if (((System.Web.UI.WebControls.Label)e.Item.FindControl("zxnum")).Text.ToString() == System.DBNull.Value.ToString() || ((System.Web.UI.WebControls.Label)e.Item.FindControl("zxnum")).Text.ToString() == System.String.Empty || ((System.Web.UI.WebControls.Label)e.Item.FindControl("zxnum")).Text.ToString() == "0")
                {
                    zxnum = zxnum + 0;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("zxnum")).Text = "0";
                }
                else
                {
                    zxnum += Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("zxnum")).Text.ToString());
                }
                if (((System.Web.UI.WebControls.Label)e.Item.FindControl("arrivedNumber")).Text.ToString() == System.DBNull.Value.ToString() || ((System.Web.UI.WebControls.Label)e.Item.FindControl("arrivedNumber")).Text.ToString() == System.String.Empty || ((System.Web.UI.WebControls.Label)e.Item.FindControl("arrivedNumber")).Text.ToString() == "0")
                {
                    recievenum = recievenum + 0;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("arrivedNumber")).Text = "0";
                }
                else
                {
                    recievenum += Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("arrivedNumber")).Text.ToString());
                }

                double zxnum1 = Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("zxnum")).Text.ToString());
                double daonum = Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("arrivedNumber")).Text.ToString());
                string state = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PO_STATE")).Text.ToString();
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("row");
                HtmlTableCell ch = (HtmlTableCell)e.Item.FindControl("ch");
                if (state == "2")
                {
                    ch.BgColor = "Blue";
                }
                else if (state == "1")
                {
                    if (daonum != 0 && daonum < zxnum1)
                    {
                        ch.BgColor = "Red";
                    }
                }

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                ((System.Web.UI.WebControls.Label)(e.Item.FindControl("Labeltotal10"))).Text = Math.Round(sum, 2).ToString();
                ((System.Web.UI.WebControls.Label)(e.Item.FindControl("totalnum"))).Text = Convert.ToString(num);
                ((System.Web.UI.WebControls.Label)(e.Item.FindControl("zxnum"))).Text = Convert.ToString(zxnum);
                ((System.Web.UI.WebControls.Label)(e.Item.FindControl("total"))).Text = Math.Round(total, 2, MidpointRounding.AwayFromZero).ToString();
                ((System.Web.UI.WebControls.Label)(e.Item.FindControl("recievenum"))).Text = Convert.ToString(recievenum);
            }
        }
        public string get_po_state(string i)
        {
            string statestr = "";
            if (i == "2")
            {
                statestr = "是";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }

        //protected void btn_bk_Click(object sender, EventArgs e)
        //{
        //    string sqltext = "";
        //    string ptcode = "";
        //    string marid = "";
        //    string pjid = "";
        //    string engid = "";
        //    string type = "";
        //    string state = "";
        //    string note = "";
        //    SqlDataReader dr = null;
        //    int temp = isselected();
        //    if (temp == 0)
        //    {
        //        foreach (RepeaterItem Reitem in tbpc_order_detailRepeater.Items)
        //        {
        //            System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
        //            if (cbx.Checked)
        //            {
        //                if (((System.Web.UI.WebControls.Label)Reitem.FindControl("PO_STATE")).Text.ToString() != "0")//备库
        //                {
        //                    ptcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PlanCode")).Text;
        //                    marid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MaterialCode")).Text;
        //                    type = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PlanMode")).Text;
        //                    state = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PO_STATE")).Text;
        //                    note = ((System.Web.UI.WebControls.Label)Reitem.FindControl("Comment")).Text;
        //                    sqltext = "select pjid,engid,num,fznum,planno from View_TBPC_PURCHASEPLAN_RVW where ptcode='" + ptcode + "'";
        //                    string sql1 = "select MP_BGNUM,MP_BGFZNUM,MP_CHPCODE from TBPC_MPTEMPCHANGE where MP_CHPTCODE='" + ptcode + "' and MP_MARID='" + marid + "'";
        //                    dr = DBCallCommon.GetDRUsingSqlText(sqltext);
        //                    SqlDataReader dr2 = DBCallCommon.GetDRUsingSqlText(sql1);

        //                    if (dr.Read() && dr2.Read())
        //                    {
        //                        pjid = dr["pjid"].ToString();
        //                        engid = dr["engid"].ToString();
        //                        double bgnum = Convert.ToDouble(dr2["MP_BGNUM"].ToString());
        //                        double bgfznum = Convert.ToDouble(dr2["MP_BGFZNUM"].ToString());
        //                        string pcode = dr["planno"].ToString();
        //                        double purnum = Convert.ToDouble(dr["num"].ToString());
        //                        double purfznum = Convert.ToDouble(dr["fznum"].ToString());
        //                        double BKnum = 0 - bgnum;
        //                        double BKfznum = 0 - bgfznum;
        //                        sqltext = "update TBPC_PURORDERDETAIL set PO_STATE='2' where PO_PCODE='" + ptcode + "'";//备库
        //                        DBCallCommon.ExeSqlText(sqltext);
        //                        sqltext = "insert into TBPM_MPFORLIB(MP_CHCODE,MP_TSAID,MP_PTC,MP_MARID,MP_TZNUM,MP_TZFZNUM,MP_TZTYPE,MP_TZSTATE,MP_NOTE,MP_SUBMITID,MP_SUBMITTM) " +
        //                                  "values ('" + pcode + "','" + engid + "','" + ptcode + "','" + marid + "','" + BKnum + "','" +
        //                                  BKfznum + "','" + type + "','0','" + note + "','" + Session["UserID"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
        //                        DBCallCommon.ExeSqlText(sqltext);
        //                        sqltext = "update TBPC_MPCHANGEDETAIL set MP_STATE='1' where MP_OLDPTCODE='" + ptcode + "'";//变更执行
        //                        DBCallCommon.ExeSqlText(sqltext);
        //                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('备库成功！');", true);
        //                    }
        //                    else
        //                    {
        //                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无变更记录，不能备库！');", true);
        //                    }

        //                    dr.Close();
        //                    dr2.Close();
        //                }
        //                else
        //                {
        //                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('订单还未提交，变更直接改变订单数量，不需备库！');", true);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择备库的物料！');", true);
        //    }

        //}
        //全选
        protected void selectall_CheckedChanged(object sender, EventArgs e)
        {
            if (selectall.Checked)
            {
                foreach (RepeaterItem Reitem in tbpc_order_detailRepeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = true;
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Reitem in tbpc_order_detailRepeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = false;
                    }
                }
            }
        }

        protected void btn_LX_click(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;
            int start = 0;
            int finish = 0;
            int k = 0;
            foreach (RepeaterItem Reitem in tbpc_order_detailRepeater.Items)
            {
                j++;
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                if (cbx.Checked)
                {
                    i++;
                    if (start == 0)
                    {
                        start = j;
                    }
                    else
                    {
                        finish = j;
                    }
                }
            }
            if (i == 2)
            {
                foreach (RepeaterItem Reitem in tbpc_order_detailRepeater.Items)
                {
                    k++;
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                    if (k >= start && k <= finish)
                    {
                        cbx.Checked = true;
                    }
                    if (k > finish)
                    {
                        cbx.Checked = false;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择连续的区间！');", true);
            }
        }
        protected void btn_QX_click(object sender, EventArgs e)
        {
            foreach (RepeaterItem Reitem in tbpc_order_detailRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                cbx.Checked = false;
            }
        }

        protected int isselected()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            foreach (RepeaterItem Reitem in tbpc_order_detailRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                    }
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }

        #region 报检

        protected void btn_baojian_Click(object sender, EventArgs e)
        {
            //已有质检结果不允许再报检
            //for (int m = 0; m < tbpc_order_detailRepeater.Items.Count; m++)
            //{
            //    if ((tbpc_order_detailRepeater.Items[m].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
            //    {
            //        string ptcode = (tbpc_order_detailRepeater.Items[m].FindControl("ptcode") as System.Web.UI.WebControls.Label).Text.Trim();
            //        SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(" SELECT TOP 1 RESULT FROM View_TBQM_APLYFORITEM WHERE PTC='" + ptcode + "'");
            //        if (dr.Read())
            //        {
            //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示：存在已报检的条目！');", true);
            //            return;
            //        }
            //        dr.Close();
            //    }
            //}

            //一个订单有多个项目

            List<string> sqllist = new List<string>();

            string sql = "update TBPC_PURORDERDETAIL set PO_OperateState=NULL WHERE PO_OperateState='PUSH" + Session["UserID"].ToString() + "'";

            sqllist.Add(sql);

            for (int i = 0; i < tbpc_order_detailRepeater.Items.Count; i++)
            {
                if ((tbpc_order_detailRepeater.Items[i].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptc = (tbpc_order_detailRepeater.Items[i].FindControl("PlanCode") as System.Web.UI.WebControls.Label).Text;
                    sql = "update TBPC_PURORDERDETAIL set PO_OperateState='PUSH" + Session["UserID"].ToString() + "' WHERE PO_PCODE='" + ptc + "'";
                    sqllist.Add(sql);
                }

            }
            if (sqllist.Count > 1)
            {
                DBCallCommon.ExecuteTrans(sqllist);
                //Response.Redirect("~/QC_Data/QC_Inspection_Add.aspx?ACTION=NEW", false);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('/QC_Data/QC_Inspection_Add.aspx?ACTION=NEW&TYPE=PUR');", true);
            }
            else
            {
                string script = @"alert('请选择需要添加的条目!');";
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", script, true);
            }

        }
        #endregion


        //代用
        protected void btn_marrep_Click(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            int temp = isselected1();
            if (temp == 0)//是否选择数据
            {
                if (is_sameeng())//是否同一批同一工程
                {
                    string sqltext = "";
                    string ptcode = "";
                    string planpcode = "";
                    string marid = "";
                    double num = 0;
                    double fznum = 0;
                    string note = "";
                    string mpcode = generatecode();
                    foreach (RepeaterItem Reitem in tbpc_order_detailRepeater.Items)
                    {
                        System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                        if (cbx.Checked)
                        {
                            ptcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PlanCode")).Text;
                            marid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MaterialCode")).Text;
                            num = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("zxnum")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("zxnum")).Text);
                            fznum = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("zxFznum")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("zxFznum")).Text);
                            note = ((System.Web.UI.WebControls.Label)Reitem.FindControl("Comment")).Text;
                            //sqltext = "INSERT INTO TBPC_MARREPLACEALL(MP_CODE,MP_PTCODE,MP_MARID,MP_NUM,MP_FZNUM,MP_NOTE) " +
                            //           "VALUES('" + mpcode + "','" + ptcode + "','" + marid + "','" + num + "','" + fznum + "','" + note + "')";
                            //sqltextlist.Add(sqltext);

                            //已经入库的物料，无法进行代用   2013年5月9日11:06:29  Meng
                            string sql2 = "select * from TBWS_INDETAIL where WG_PTCODE ='" + ptcode + "'";
                            System.Data.DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql2);
                            if (dt2.Rows.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('所勾选物料已入库！\\r\\r请核对！！！');", true);
                                return;
                            }
                            else
                            {
                                sqltext = "INSERT INTO TBPC_MARREPLACEALL(MP_CODE,MP_PTCODE,MP_MARID,MP_NUM,MP_FZNUM,MP_NOTE) " +
                                           "VALUES('" + mpcode + "','" + ptcode + "','" + marid + "','" + num + "','" + fznum + "','" + note + "')";
                                sqltextlist.Add(sqltext);
                            }
                        }

                    }
                    if (ptcode.Contains("#"))
                    {
                        ptcode = ptcode.Substring(0, ptcode.IndexOf("#")).ToString();
                    }
                    if (ptcode.Contains("@"))
                    {
                        ptcode = ptcode.Substring(0, ptcode.IndexOf("@")).ToString();
                    }
                    sqltext = "select PUR_PCODE from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptcode + "'";
                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (dt.Rows.Count > 0)
                    {
                        planpcode = dt.Rows[0]["PUR_PCODE"].ToString();
                    }
                    sqltext = "INSERT INTO TBPC_MARREPLACETOTAL(MP_CODE,MP_PLANPCODE,MP_PJID,MP_ENGID," +
                             "MP_FILLFMID,MP_FILLFMTIME,MP_REVIEWAID,MP_CHARGEID)  " +
                             "select '" + mpcode + "',PR_SHEETNO,PR_PJID,PR_ENGID,'" + Session["UserID"].ToString() + "','" +
                             DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',PR_SQREID,PR_FZREID  " +
                             "from TBPC_PCHSPLANRVW where PR_SHEETNO='" + planpcode + "'";
                    sqltextlist.Add(sqltext);
                    DBCallCommon.ExecuteTrans(sqltextlist);
                    Response.Redirect("~/PC_Data/PC_TBPC_Marreplace_panel.aspx?mpno=" + mpcode);//转到代用页面 
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择同一项目同一工程下的物料,本次操作无效！');", true);
                }

            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
            }
        }

        protected int isselected1()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            foreach (RepeaterItem Reitem in tbpc_order_detailRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else
            {
                temp = 0;//可以下推
            }
            return temp;
        }

        private string generatecode()//生成代用单号
        {
            string subcode = "";
            string mpcode = "";
            string sqltext = "SELECT TOP 1 MP_CODE FROM TBPC_MARREPLACETOTAL WHERE MP_CODE LIKE '" + DateTime.Now.Year.ToString() + "2" + "%' ORDER BY MP_CODE DESC";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                subcode = Convert.ToString(dt.Rows[0][0]).Substring(Convert.ToString(dt.Rows[0][0]).ToString().Length - 5, 5);//后五位流水号
                subcode = Convert.ToString(Convert.ToInt32(subcode) + 1);
                subcode = subcode.PadLeft(5, '0');
            }
            else
            {
                subcode = "00001";
            }
            mpcode = DateTime.Now.Year.ToString() + "2" + subcode;
            return mpcode;
        }

        private bool is_sameeng()//判断是否同一批同一工程
        {
            string pjid = "";
            string engid = "";
            bool temp = true;
            int i = 0;
            foreach (RepeaterItem Reitem in tbpc_order_detailRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        if (i == 1)
                        {
                            pjid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("pjid")).Text.ToString();
                            engid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("engid")).Text.ToString();
                        }
                        else
                        {
                            if (pjid != ((System.Web.UI.WebControls.Label)Reitem.FindControl("pjid")).Text.ToString() || engid != ((System.Web.UI.WebControls.Label)Reitem.FindControl("engid")).Text.ToString())
                            {
                                temp = false;
                                break;
                            }
                        }
                    }
                }
            }
            return temp;
        }

        protected void btn_dhqr_Click(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            string sqltext = "";
            int i = 0;
            int j = 0;
            foreach (RepeaterItem Reitem in tbpc_order_detailRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                    string state = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PO_STATE")).Text;
                    if (state == "2")
                    {
                        j++;
                    }
                }
            }

            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
            }
            else if (j > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('含有已入库的数据，无需到货确认！');", true);
            }
            else
            {
                foreach (RepeaterItem Reitem in tbpc_order_detailRepeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx.Checked)
                    {
                        string ptcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PlanCode")).Text;
                        sqltext = "update TBPC_PURORDERDETAIL set PO_STATE='3' where PO_PTCODE='" + ptcode + "'";
                        sqltextlist.Add(sqltext);
                    }
                }
                DBCallCommon.ExecuteTrans(sqltextlist);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('到货确认成功！');", true);
            }
        }

        protected void btn_shanchu_Click(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            if (btn_shanchu.Text == "删除")
            {
                int temp = candelete();
                if (temp == 0)
                {
                    string code = LabelCode.Text;
                    string BZcode = code.Substring(0, 4).ToString();
                    if (BZcode == "PORD")
                    {
                        string sql = "select PO_PTCODE from TBPC_PURORDERDETAIL where PO_CODE='" + code + "'";
                        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string ptcode = dt.Rows[i]["PO_PTCODE"].ToString();
                            string sql1 = "update TBPC_PURCHASEPLAN set PUR_STATE='6' where PUR_PTCODE='" + ptcode + "'";
                            sqltextlist.Add(sql1);
                            string sql2 = "update TBPC_IQRCMPPRICE set PIC_STATE='0' where PIC_PTCODE='" + ptcode + "'";
                            sqltextlist.Add(sql2);
                        }
                    }
                    else if (BZcode == "ZBPO")
                    {
                        string sql = "select PO_PTCODE from TBPC_PURORDERDETAIL where PO_CODE='" + code + "'";
                        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string ptcode = dt.Rows[i]["PO_PTCODE"].ToString();
                            string sql1 = "update TBPC_PURCHASEPLAN set PUR_STATE='4' where PUR_PTCODE='" + ptcode + "'";
                            sqltextlist.Add(sql1);
                        }
                    }
                    string sqltext = "update TBPC_PURORDERDETAIL set PO_CSTATE='2' where PO_CODE='" + LabelCode.Text + "'";
                    sqltextlist.Add(sqltext);
                    string sqltext1 = "update TBPC_PURORDERTOTAL set PO_CSTATE='2' where PO_CODE='" + LabelCode.Text + "'";
                    sqltextlist.Add(sqltext1);
                    DBCallCommon.ExecuteTrans(sqltextlist);
                    Response.Redirect("~/PC_Data/TBPC_Purordertotal_list.aspx?num1=" + NUM1 + "&num2=" + NUM2 + "");
                }
                else if (temp == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('订单包含已入库的记录，不能删除！');", true);
                }
                else if (temp == 2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您不是制单人，无权删除！');", true);
                }
            }
            else if (btn_shanchu.Text == "取消删除")
            {

            }

        }

        //判断能否删除，需要注意
        private int candelete()
        {
            int temp = 0;
            int j = 0;//制单是否为登录用户
            int k = 0;
            string userid = Session["UserID"].ToString();
            foreach (RepeaterItem Reitem in tbpc_order_detailRepeater.Items)
            {
                double anum = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("arrivedNumber")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("arrivedNumber")).Text);
                if (anum > 0)
                {
                    j++;
                    break;
                }
            }
            if (userid != LabelDocumentid.Text)
            {
                k++;
            }

            if (j > 0)//包含入库记录
            {
                temp = 1;
            }
            else if (k > 0)//不是制单人
            {
                temp = 2;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }

        protected void btn_AddHTSP_Click(object sender, EventArgs e)
        {
            string orderno = LabelCode.Text.Trim();
            string gysid = supplierid.Value;
            string sql = "select count(HT_ID) from PC_CGHT where HT_DDBH like '%" + orderno + "%'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString()!="0")
            {
                Response.Write("<script>alert('您选择的订单包含已生成合同的部分！！！请重新选择')</script>");
                    return;
            }
            Response.Redirect("PC_CGHT.aspx?action=add1&ddbh=" + orderno + "&gysid=" + gysid);
        }

        //免检
        protected void btn_mianjian_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            int n = 0;
            foreach (RepeaterItem item in tbpc_order_detailRepeater.Items)
            {
                if ((item.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptc = (item.FindControl("txt_PlanCode") as System.Web.UI.WebControls.TextBox).Text.ToString().Trim();
                    string sql = "insert into dbo.TBQM_APLYFORITEM(RESULT,PTC) values ('免检','" + ptc + "')";
                    n++;
                    list.Add(sql);
                }
            }
            if (n == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择需要免检的项！');", true);
                return;
            }
            DBCallCommon.ExecuteTrans(list);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交成功！');", true);
            initial();
        }
    }
}
