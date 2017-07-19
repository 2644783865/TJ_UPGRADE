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
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_KuCunYuEChaXun : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        Repeater CurRepeater;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                GetData();
                bindGV();
            }
            CheckUser(ControlFinder);
        }

        private void GetData()
        {
            string jibie = ddlType.SelectedValue;
            string StrTime;
            string EndTime;
            string condition = "";
            txtStartTime.Text = System.DateTime.Now.ToString("yyyy-MM");
            txtEndTime.Text= System.DateTime.Now.ToString("yyyy-MM");
            StrTime = txtStartTime.Text.Trim();
            EndTime = txtEndTime.Text.Trim();

            if (rbl1.SelectedValue == "month")
            {
                condition = "CONVERT(varchar(100), (SI_YEAR+'-'+SI_PERIOD), 23)  between '" + StrTime + "' and '" + EndTime + "'";
            }
            else if (rbl1.SelectedValue == "year")
            {
                condition = " SI_YEAR ='" + txtStartTime.Text.Split('-')[0] + "'";
            }
            string SubCondtion = GetSubCondtion();

            if (condition != "")
            {
                if (SubCondtion != "")
                {
                    condition += " AND (" + SubCondtion + " )";
                }
            }
            else
            {
                if (SubCondtion != "")
                {
                    condition += SubCondtion;
                }
            }

            if (rbl1.SelectedValue == "year")
            {

                BindYearData(jibie, condition);
            }
            else //按月
            {
                BindMonthData(jibie, condition);
            }           
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
        //    foreach (ListItem lt in ddl_year.Items)
        //    {
        //        if (lt.Value == getYear())
        //        {
        //            lt.Selected = true;
        //        }
        //    }
        //    foreach (ListItem lt in ddl_month.Items)
        //    {
        //        if (lt.Value == getMonth())
        //        {
        //            lt.Selected = true;
        //        }
        //    }
        //}
        #endregion
        #region 条件框


        //物料，三级
        private Dictionary<string, string> bindThreeList()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();

            ItemList.Add("NO", "");
            ItemList.Add("SI_MARID", "物料编码");
            ItemList.Add("SI_MARNM", "物料名称");
            ItemList.Add("SI_GUIGE", "规格型号");
            ItemList.Add("SI_CAIZHI", "材质");           

            return ItemList;
        }
        //小类，二级
        private Dictionary<string, string> bindTwoList()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();            
            ItemList.Add("NO", "");
            ItemList.Add("SI_TYPEID", "物料类别");

            return ItemList;
        }

        //大类，一级，
        private Dictionary<string, string> bindOneList()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();
            ItemList.Add("NO", "");
            ItemList.Add("substring(SI_TYPEID,1,2)", "物料类别");

            return ItemList;
        }

        protected void GridViewSearch_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                
                if (ddlType.SelectedValue == "03")
                {
                    DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                    ddl.DataTextField = "value";
                    ddl.DataValueField = "key";
                    ddl.DataSource = bindThreeList();
                    ddl.DataBind();
                }
                else 
                {
                    DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);
                    ddl.DataTextField = "value";
                    ddl.DataValueField = "key";
                    

                    //DropDownList ddlTypeValue = (gr.FindControl("ddlTypeValue") as DropDownList);
                    if (ddlType.SelectedValue == "02")
                    {
                        ddl.DataSource = bindTwoList();
                        ddl.DataBind();
                        //string sql = "select ty_name,TY_ID  from TBMA_typeinfo where ty_fatherid !='ROOT'";
                        //DBCallCommon.FillDroplist(ddlTypeValue, sql);
                    }
                    else
                    {
                        ddl.DataSource = bindOneList();
                        ddl.DataBind();
                        //ddlTypeValue.Items.Add(new ListItem("-请选择-", "-请选择-"));
                        //ddlTypeValue.Items.Add(new ListItem("原材料","01"));
                        //ddlTypeValue.Items.Add(new ListItem("低值易耗品", "02"));
                    }
                }

                if (gr.RowIndex == 4)
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
                if (ddlType.SelectedValue == "03")
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
                else
                {
                    if (gr.RowIndex == 0)
                    {

                        DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                        if (ddl.SelectedValue != "NO")
                        {
                            //DropDownList ddlValue = (gr.FindControl("ddlTypeValue") as DropDownList);
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

                            //DropDownList ddlValue = (gr.FindControl("ddlTypeValue") as DropDownList);
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
                
            }
            return subCondition;
        }

        
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGV();
            
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                System.Web.UI.WebControls.TextBox tb = (gr.FindControl("TextBoxValue") as System.Web.UI.WebControls.TextBox);
                //DropDownList ddl = (gr.FindControl("ddlTypeValue") as DropDownList);
                if (ddlType.SelectedValue == "03")
                {
                    //tb.Visible = true;
                    //ddl.Visible = false;
                    tb.Style.Add("display","block");
                    //ddl.Style.Add("display", "none");
                }
                else
                {
                    //tb.Visible = false;
                    //ddl.Visible = true;
                    tb.Style.Add("display", "block");
                    //ddl.Style.Add("display", "block");
                }
            }
        }

        #endregion

        protected void btnClose_Click(object sender, EventArgs e)
        {
            //resetSubcondition();
            ModalPopupExtenderSearch.Hide();
            UpdatePanelBody.Update();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            //resetSubcondition();
            refreshStyle();
            UpdatePanelBody.Update();
        }
        
        //查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            getData();
            ModalPopupExtenderSearch.Hide();
            UpdatePanelBody.Update();
        }

        #region 导出

        //导出
        protected void btn_export_Click(object sender, EventArgs e)
        {
            //rptProductNumStc.DataSource = null;
            //rptProductNumStc.DataBind();
            //this.ddlchaxun_SelectedIndexChanged(null, null);//防止长时间不导出而他人操作数据得表TBFM_PRIDSTATISTICS中数据改变，故重新执行一遍
            string condition = "";
            string sqltext="";
            System.Data.DataTable dt;
            string jibie = ddlType.SelectedValue;
            string startime = string.Empty;
            string endtime = string.Empty;
            string YEAR = txtStartTime.Text.Split('-')[0];
            startime = txtStartTime.Text.Trim();
            endtime = txtEndTime.Text.Trim();
            if (rbl1.SelectedValue == "month")
            {
                condition = "CONVERT(varchar(100), (SI_YEAR+'-'+SI_PERIOD), 23)  between '" + startime + "' and '" + endtime + "'";
            }
            else if (rbl1.SelectedValue == "year")
            {
                condition = " SI_YEAR ='" + txtStartTime.Text.Split('-')[0] + "'";
            }
            string SubCondtion = GetSubCondtion();

            if (condition != "")
            {
                if (SubCondtion != "")
                {
                    condition += " AND (" + SubCondtion + " ) ";
                }
            }
            else
            {
                if (SubCondtion != "")
                {
                    condition += SubCondtion;
                }
            }
            if (jibie == "03")
            {
                if (rbl1.SelectedValue == "year")
                {
                    sqltext = "select SI_MARID,SI_MARNM,SI_YEAR,SI_YBEGNUM ,round(SI_YBEGBAL,2) as SI_YBEGBAL,SI_YRCVNUM,round(SI_YRCVMNY,2) as SI_YRCVMNY ,SI_YSNDNUM,round(SI_YSNDMNY,2) as SI_YSNDMNY,SI_YSENDNUM,round(SI_YSENDMNY,2) as SI_YSENDMNY";
                    sqltext += " from dbo.ChuKuYueChaXun('" + YEAR + "') where " + condition + " order by SI_YEAR,SI_MARID";
                    dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                    ExportDataItem1(dt); ;
                }
                else if (rbl1.SelectedValue == "month")
                {
                    sqltext = "select SI_MARID,SI_MARNM,SI_CAIZHI,SI_GUIGE,GB,PURCUNIT,SI_YEAR,SI_PERIOD,SI_BEGNUM,round(SI_BEGBAL,2) as SI_BEGBAL,SI_CRCVNUM,round(SI_CRCVMNY,2) as SI_CRCVMNY,SI_CSNDNUM,round(SI_CSNDMNY,2) as SI_CSNDMNY,SI_ENDNUM,round(SI_ENDBAL,2) as SI_ENDBAL";
                    sqltext += " from View_FM_STORAGEBAL where " + condition + " order by CONVERT(varchar(100), (SI_YEAR+'-'+SI_PERIOD), 23),SI_MARID";
                    dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                    ExportDataItem2(dt);
                }

            }
            else
            {                
                if (rbl1.SelectedValue == "month")
                {
                    if (jibie == "02")
                    {
                        sqltext = "select SI_TYPEID,SI_TYNAME,SI_YEAR,SI_PERIOD, round(sum(SI_BEGNUM),4) as SI_BEGNUM,";
                        sqltext += "round(sum(SI_BEGBAL),2) as SI_BEGBAL,";
                        sqltext += "round(sum(SI_CRCVNUM),4) as SI_CRCVNUM,";
                        sqltext += "round(sum(SI_CRCVMNY),2) as SI_CRCVMNY,";
                        sqltext += "round(sum(SI_CSNDNUM),4) as SI_CSNDNUM, round(sum(SI_CSNDMNY),2) as SI_CSNDMNY,";
                        sqltext += "round(sum(SI_ENDNUM),2) as SI_ENDNUM,round(sum(SI_ENDBAL),2) as SI_ENDBAL from View_FM_STORAGEBAL ";
                        sqltext += " where " + condition + " group by SI_YEAR,SI_PERIOD,SI_TYPEID,SI_TYNAME";
                    }
                    if (jibie == "01")
                    {
                        sqltext = "select substring(SI_TYPEID,1,2) AS SI_TYPEID,(case when substring(SI_TYPEID,1,2)='01' then  '原材料'  when substring(SI_TYPEID,1,2)='02' then '低值易耗品' else  null end) as SI_TYNAME, ";
                        sqltext += "SI_YEAR,SI_PERIOD,round(sum(SI_BEGNUM),4) as SI_BEGNUM,";
                        sqltext += "round(sum(SI_BEGBAL),2) as SI_BEGBAL,";
                        sqltext += "round(sum(SI_CRCVNUM),4) as SI_CRCVNUM,";
                        sqltext += "round(sum(SI_CRCVMNY),2) as SI_CRCVMNY,";
                        sqltext += "round(sum(SI_CSNDNUM),4) as SI_CSNDNUM, round(sum(SI_CSNDMNY),2) as SI_CSNDMNY,";
                        sqltext += "round(sum(SI_ENDNUM),2) as SI_ENDNUM,round(sum(SI_ENDBAL),2) as SI_ENDBAL from View_FM_STORAGEBAL ";
                        sqltext += " where " + condition + " group by SI_YEAR,SI_PERIOD,substring(SI_TYPEID,1,2) ";
                    }
                    dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                    ExportDataItem3(dt);
                }
                else if (rbl1.SelectedValue == "year")
                {
                    if (jibie == "02")
                    {
                        //string sqltext = " select SI_MARID,SI_MARNM,SI_YBEGNUM,round(SI_YBEGBAL,2) as SI_YBEGBAL,SI_YRCVNUM,round(SI_YRCVMNY,2) as SI_YRCVMNY ,SI_YSNDNUM,round(SI_YSNDMNY,2) as SI_YSNDMNY,SI_YSENDNUM,round(SI_YSENDMNY,2) as SI_YSENDMNY,SI_YEAR from dbo.ChuKuYueChaXun('" + year + "') where " + condition;
                        sqltext = "select substring(SI_MARID,1,5) as SI_TYPE,SI_TYNAME, SI_YEAR,round(sum(SI_YBEGNUM),4) as SI_YBEGNUM, ";
                        sqltext += "round(sum(SI_YBEGBAL),2) as SI_YBEGBAL,round(sum(SI_YRCVNUM),4) as SI_YRCVNUM,";
                        sqltext += "round(sum(SI_YRCVMNY),2) as SI_YRCVMNY ,round(sum(SI_YSNDNUM),4) as SI_YSNDNUM,";
                        sqltext += "round(sum(SI_YSNDMNY),2) as SI_YSNDMNY,round(sum(SI_YSENDNUM),4) as SI_YSENDNUM,";
                        sqltext += "round(sum(SI_YSENDMNY),2) as SI_YSENDMNY from dbo.ChuKuYueChaXun('" + YEAR + "') ";
                        sqltext += " where " + condition + " group by SI_YEAR,substring(SI_MARID,1,5),SI_TYNAME";
                    }
                    if (jibie == "01")
                    {
                        sqltext = "select substring(SI_MARID,1,2) as SI_TYPE,(case when substring(SI_TYPEID,1,2)='01' then  '原材料'  when substring(SI_TYPEID,1,2)='02' then '低值易耗品' else  null end) as SI_TYNAME, ";
                        sqltext += "SI_YEAR, round(sum(SI_YBEGNUM),4) as SI_YBEGNUM,";
                        sqltext += "round(sum(SI_YBEGBAL),2) as SI_YBEGBAL,round(sum(SI_YRCVNUM),4) as SI_YRCVNUM,";
                        sqltext += "round(sum(SI_YRCVMNY),2) as SI_YRCVMNY ,round(sum(SI_YSNDNUM),4) as SI_YSNDNUM,";
                        sqltext += "round(sum(SI_YSNDMNY),2) as SI_YSNDMNY,round(sum(SI_YSENDNUM),4) as SI_YSENDNUM,";
                        sqltext += "round(sum(SI_YSENDMNY),2) as SI_YSENDMNY from dbo.ChuKuYueChaXun('" + YEAR + "') ";
                        sqltext += " where " + condition + " group by SI_YEAR,substring(SI_MARID,1,2),substring(SI_TYPEID,1,2)";
                    }
                    dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                    ExportDataItem1(dt);
                }
                
            }

        }

        private void ExportDataItem1(System.Data.DataTable dt)
        {
            string filename = "收发存汇总.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("库存余额按年查询.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.GetRow(i + 2);
                    row = sheet0.CreateRow(i + 2);
                    row.CreateCell(0).SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                    }

                }

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
        private void ExportDataItem2(System.Data.DataTable dt)
        {
            string filename = "收发存汇总.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("库存余额按月查询.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.GetRow(i + 2);
                    row = sheet0.CreateRow(i + 2);
                    row.CreateCell(0).SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                    }

                }

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
        private void ExportDataItem3(System.Data.DataTable dt)
        {
            string filename = "收发存汇总.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("库存余额按月(级别一，二)查询.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.GetRow(i + 2);
                    row = sheet0.CreateRow(i + 2);
                    row.CreateCell(0).SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                    }

                }

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




        #endregion

        private void getData()
        {
            string jibie = ddlType.SelectedValue;
            string startime = string.Empty;
            string endtime = string.Empty;
            string condition = "";
            startime = txtStartTime.Text.Trim();
            endtime = txtEndTime.Text.Trim();
            if (rbl1.SelectedValue == "month")
            {
               condition = "CONVERT(varchar(100), (SI_YEAR+'-'+SI_PERIOD), 23)  between '" + startime + "' and '" + endtime + "'";
            }
            else if (rbl1.SelectedValue == "year")
            {
               condition = " SI_YEAR ='" + txtStartTime.Text.Split('-')[0] + "'";
            }
            string SubCondtion = GetSubCondtion();

            if (condition != "")
            {
                if (SubCondtion != "")
                {
                  condition += " AND (" + SubCondtion + " )";
                }
            }
            else
            {
                if (SubCondtion != "")
                {
                    condition += SubCondtion;
                }
            }

            if (rbl1.SelectedValue == "year")
            {

                BindYearData(jibie, condition);
            }
            else //按月
            {
                BindMonthData(jibie, condition);
            }
        }

        //按年绑定数据
        private void BindYearData(string jibie, string condition)
        {
            GetYearTotalAmount(condition);//年汇总
            string sqltext="";
            string year = txtStartTime.Text.Split('-')[0];
            #region

            
            //if (jibie == "03")
            //{
            //    sqltext = " select SI_MARID,SI_MARNM,SI_YBEGNUM,";
            //    sqltext += "round(SI_YBEGBAL,2) as SI_YBEGBAL,SI_YRCVNUM,round(SI_YRCVMNY,2) as SI_YRCVMNY ,SI_YSNDNUM,";
            //    sqltext += "round(SI_YSNDMNY,2) as SI_YSNDMNY,SI_YSENDNUM,round(SI_YSENDMNY,2) as SI_YSENDMNY,";
            //    sqltext += "SI_YEAR from dbo.ChuKuYueChaXun('" + year + "') where " + condition;


            //}
            //else if (jibie == "02")
            //{
            //    //string sqltext = " select SI_MARID,SI_MARNM,SI_YBEGNUM,round(SI_YBEGBAL,2) as SI_YBEGBAL,SI_YRCVNUM,round(SI_YRCVMNY,2) as SI_YRCVMNY ,SI_YSNDNUM,round(SI_YSNDMNY,2) as SI_YSNDMNY,SI_YSENDNUM,round(SI_YSENDMNY,2) as SI_YSENDMNY,SI_YEAR from dbo.ChuKuYueChaXun('" + year + "') where " + condition;
            //    sqltext = "select substring(SI_MARID,1,5),SI_YEAR, ";
            //    sqltext += "round(sum(SI_YBEGBAL),2) as SI_YBEGBAL,round(sum(SI_YRCVNUM),2) as SI_YRCVNUM,";
            //    sqltext += "round(sum(SI_YRCVMNY),2) as SI_YRCVMNY ,round(sum(SI_YSNDNUM),2) as SI_YSNDNUM,";
            //    sqltext += "round(sum(SI_YSNDMNY),2) as SI_YSNDMNY,round(sum(SI_YSENDNUM),2) as SI_YSENDNUM,";
            //    sqltext += "round(sum(SI_YSENDMNY),2) as SI_YSENDMNY from dbo.ChuKuYueChaXun('" + year + "') ";
            //    sqltext += " where " + condition + " group by substring(SI_MARID,1,5),SI_YEAR";
            //}
            //else if (jibie == "01")
            //{
            //    sqltext = "select substring(SI_MARID,1,2),(case when substring(SI_TYPEID,1,2)='01' then  '原材料'  when substring(SI_TYPEID,1,2)='02' then '低值易耗品' else  null end) as SI_TYNAME, ";
            //    sqltext += "SI_YEAR, round(sum(SI_BEGDIFF),2) as SI_BEGDIFF";
            //    sqltext += "round(sum(SI_YBEGBAL),2) as SI_YBEGBAL,round(sum(SI_YRCVNUM),2) as SI_YRCVNUM,";
            //    sqltext += "round(sum(SI_YRCVMNY),2) as SI_YRCVMNY ,round(sum(SI_YSNDNUM),2) as SI_YSNDNUM,";
            //    sqltext += "round(sum(SI_YSNDMNY),2) as SI_YSNDMNY,round(sum(SI_YSENDNUM),2) as SI_YSENDNUM,";
            //    sqltext += "round(sum(SI_YSENDMNY),2) as SI_YSENDMNY from dbo.ChuKuYueChaXun('" + year + "') ";
            //    sqltext += " where " + condition + " group by substring(SI_MARID,1,2),SI_YEAR";
            //}

            //DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            //if (ddlType.SelectedValue == "03") //物料按年
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        rpt_yearMar.DataSource = dt;
            //        rpt_yearMar.DataBind();
            //        rpt_yearMar.Visible = true;
            //        rpt_monthMar.DataSource = null;
            //        rpt_monthMar.DataBind();
            //        rpt_monthMar.Visible = false;

            //        rpt_monthType.DataSource = null;
            //        rpt_monthType.DataBind();
            //        rpt_monthType.Visible = false;
            //        rpt_yearType.DataSource = null;
            //        rpt_yearType.DataBind();
            //        rpt_yearType.Visible = false;
            //        NoDataPanel.Visible = false;
            //        btn_export.Visible = true;


            //    }
            //    else
            //    {
            //        rpt_yearMar.DataSource = null;
            //        rpt_yearMar.DataBind();
            //        rpt_monthMar.DataSource = null;
            //        rpt_monthMar.DataBind();
            //        rpt_monthMar.Visible = false;

            //        rpt_monthType.DataSource = null;
            //        rpt_monthType.DataBind();
            //        rpt_monthType.Visible = false;
            //        rpt_yearType.DataSource = null;
            //        rpt_yearType.DataBind();
            //        rpt_yearType.Visible = false;
            //        NoDataPanel.Visible = true;
            //        btn_export.Visible = false;


            //    }
            //}
            //else //物料类别按年
            //{
            //    if (dt.Rows.Count > 0)
            //    {

            //        rpt_yearType.DataSource = dt;
            //        rpt_yearType.DataBind();
            //        rpt_yearType.Visible = true;

            //        rpt_monthType.DataSource = null;
            //        rpt_monthType.DataBind();
            //        rpt_monthType.Visible = false;

            //        rpt_yearMar.DataSource = null;
            //        rpt_yearMar.DataBind();
            //        rpt_monthMar.DataSource = null;
            //        rpt_monthMar.DataBind();
            //        rpt_monthMar.Visible = false;
            //        NoDataPanel.Visible = false;
            //        btn_export.Visible = true;
            //    }
            //    else
            //    {
            //        rpt_monthType.DataSource = null;
            //        rpt_monthType.DataBind();
            //        //rpt_monthType.Visible = false;
            //        rpt_yearType.DataSource = null;
            //        rpt_yearType.DataBind();
            //        rpt_yearType.Visible = false;

            //        rpt_yearMar.DataSource = null;
            //        rpt_yearMar.DataBind();
            //        rpt_monthMar.DataSource = null;
            //        rpt_monthMar.DataBind();
            //        rpt_monthMar.Visible = false;
            //        NoDataPanel.Visible = true;
            //        btn_export.Visible = false;


            //    }
            //}
            #endregion
            if (jibie == "03")
            {
                string TableName = "dbo.ChuKuYueChaXun('" + year + "')";
                string PrimaryKey = "";
                string ShowFields = "SI_MARID,SI_MARNM,'' as SI_CAIZHI,'' as SI_GUIGE,'' as GB,'' as PURCUNIT,SI_YBEGNUM,round(SI_YBEGBAL,2) as SI_YBEGBAL,SI_YRCVNUM,round(SI_YRCVMNY,2) as SI_YRCVMNY ,SI_YSNDNUM,round(SI_YSNDMNY,2) as SI_YSNDMNY,SI_YSENDNUM,round(SI_YSENDMNY,2) as SI_YSENDMNY,SI_YEAR ";
                string OrderField = "SI_YEAR,SI_MARID";
                int OrderType = 0;
                string StrWhere = condition;
                int PageSize = 100;

                CurRepeater = rpt_yearMar;
                InitVar(TableName, PrimaryKey, ShowFields, OrderField, StrWhere, OrderType, PageSize);
            }
            else
            {
                if (jibie == "02")
                {
                    //string sqltext = " select SI_MARID,SI_MARNM,SI_YBEGNUM,round(SI_YBEGBAL,2) as SI_YBEGBAL,SI_YRCVNUM,round(SI_YRCVMNY,2) as SI_YRCVMNY ,SI_YSNDNUM,round(SI_YSNDMNY,2) as SI_YSNDMNY,SI_YSENDNUM,round(SI_YSENDMNY,2) as SI_YSENDMNY,SI_YEAR from dbo.ChuKuYueChaXun('" + year + "') where " + condition;
                    sqltext = "select substring(SI_MARID,1,5) as SI_TYPE,SI_TYNAME, SI_YEAR,round(sum(SI_YBEGNUM),2) as SI_YBEGNUM, ";
                    sqltext += "round(sum(SI_YBEGBAL),2) as SI_YBEGBAL,round(sum(SI_YRCVNUM),2) as SI_YRCVNUM,";
                    sqltext += "round(sum(SI_YRCVMNY),2) as SI_YRCVMNY ,round(sum(SI_YSNDNUM),2) as SI_YSNDNUM,";
                    sqltext += "round(sum(SI_YSNDMNY),2) as SI_YSNDMNY,round(sum(SI_YSENDNUM),2) as SI_YSENDNUM,";
                    sqltext += "round(sum(SI_YSENDMNY),2) as SI_YSENDMNY from dbo.ChuKuYueChaXun('" + year + "') ";
                    sqltext += " where " + condition + " group by SI_YEAR,substring(SI_MARID,1,5),SI_TYNAME";
                }
                if (jibie == "01")
                {
                    sqltext = "select substring(SI_MARID,1,2) as SI_TYPE,(case when substring(SI_TYPEID,1,2)='01' then  '原材料'  when substring(SI_TYPEID,1,2)='02' then '低值易耗品' else  null end) as SI_TYNAME, ";
                    sqltext += "SI_YEAR, round(sum(SI_YBEGNUM),2) as SI_YBEGNUM,"; 
                    sqltext += "round(sum(SI_YBEGBAL),2) as SI_YBEGBAL,round(sum(SI_YRCVNUM),2) as SI_YRCVNUM,";
                    sqltext += "round(sum(SI_YRCVMNY),2) as SI_YRCVMNY ,round(sum(SI_YSNDNUM),2) as SI_YSNDNUM,";
                    sqltext += "round(sum(SI_YSNDMNY),2) as SI_YSNDMNY,round(sum(SI_YSENDNUM),2) as SI_YSENDNUM,";
                    sqltext += "round(sum(SI_YSENDMNY),2) as SI_YSENDMNY from dbo.ChuKuYueChaXun('" + year + "') ";
                    sqltext += " where " + condition + " group by SI_YEAR,substring(SI_MARID,1,2),substring(SI_TYPEID,1,2)";
                }
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
               
                if (dt.Rows.Count > 0)
                {

                    rpt_yearType.DataSource = dt;
                    rpt_yearType.DataBind();
                    rpt_yearType.Visible = true;

                    rpt_monthType.DataSource = null;
                    rpt_monthType.DataBind();
                    rpt_monthType.Visible = false;

                    rpt_yearMar.DataSource = null;
                    rpt_yearMar.DataBind();
                    rpt_monthMar.DataSource = null;
                    rpt_monthMar.DataBind();
                    rpt_monthMar.Visible = false;
                    NoDataPanel.Visible = false;
                    btn_export.Visible = true;
                    UCPaging1.Visible = false;
                }
                else
                {
                    rpt_monthType.DataSource = null;
                    rpt_monthType.DataBind();
                    //rpt_monthType.Visible = false;
                    rpt_yearType.DataSource = null;
                    rpt_yearType.DataBind();
                    rpt_yearType.Visible = false;

                    rpt_yearMar.DataSource = null;
                    rpt_yearMar.DataBind();
                    rpt_monthMar.DataSource = null;
                    rpt_monthMar.DataBind();
                    rpt_monthMar.Visible = false;
                    NoDataPanel.Visible = true;
                    btn_export.Visible = false;

                    UCPaging1.Visible = false;
                }
            }
        }

        private void GetYearTotalAmount(string strCondition) //年汇总
        {
            string year = txtStartTime.Text.Split('-')[0];
            string sql = "select isnull(CAST(SUM(SI_YBEGBAL) AS FLOAT),0) as TotalBEGBAL,isnull(CAST(SUM(SI_YSENDMNY) AS FLOAT),0) as TotalENDBAL, isnull(CAST(SUM(SI_YRCVMNY) AS FLOAT),0) as TotalCRCVMNY,isnull(CAST(SUM(SI_YSNDMNY) AS FLOAT),0) as TotalCSNDMNY from dbo.ChuKuYueChaXun('" + year + "') where " + strCondition;

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.Read())
            {
                hfdBEGBAL.Value = string.Format("{0:c2}", Convert.ToDouble(sdr["TotalBEGBAL"]));//年初金额
                hfdENDBAL.Value = string.Format("{0:c2}", Convert.ToDouble(sdr["TotalENDBAL"])); //年末金额
                hfdCRCVMNY.Value = string.Format("{0:c2}", Convert.ToDouble(sdr["TotalCRCVMNY"])); //本年收入
                hfdCSNDMNY.Value = string.Format("{0:c2}", Convert.ToDouble(sdr["TotalCSNDMNY"])); //本年发出
            }
            sdr.Close();
        }

        private void GetTotalAmount(string strWhere)
        {
            string sql = "select isnull(CAST(sum(SI_BEGBAL) AS FLOAT),0) as TotalBEGBAL,isnull(CAST(sum(SI_ENDBAL) AS FLOAT),0) as TotalENDBAL, isnull(CAST(sum(SI_CRCVMNY) AS FLOAT),0) as TotalCRCVMNY,isnull(CAST(sum(SI_CSNDMNY) AS FLOAT),0) as TotalCSNDMNY from View_FM_STORAGEBAL where " + strWhere;

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.Read())
            {
                
                hfdBEGBAL.Value = string.Format("{0:c2}",Convert.ToDouble(sdr["TotalBEGBAL"]));
                hfdENDBAL.Value = string.Format("{0:c2}", Convert.ToDouble(sdr["TotalENDBAL"]));
                hfdCRCVMNY.Value = string.Format("{0:c2}",  Convert.ToDouble(sdr["TotalCRCVMNY"]));
                hfdCSNDMNY.Value = string.Format("{0:c2}",  Convert.ToDouble(sdr["TotalCSNDMNY"]));
            }
            sdr.Close();

        }

        //按月绑定数据
        private void BindMonthData(string jibie,string condition)
        {
            string sqltext = "";
            #region

            //if (jibie == "03")
            //{
            //    sqltext = "select SI_MARID,SI_MARNM,SI_BEGNUM,round(SI_BEGBAL,2) as SI_BEGBAL,SI_BEGDIFF,SI_ENDNUM,round(SI_ENDBAL,2) as SI_ENDBAL,SI_CRCVNUM,round(SI_CRCVMNY,2) as SI_CRCVMNY, SI_CRVDIFF,SI_CSNDNUM,round(SI_CSNDMNY,2) as SI_CSNDMNY,SI_CSNDDIFF,SI_PTCODE,SI_YEAR,SI_PERIOD from View_FM_STORAGEBAL where " + condition;
            //}
            //else if (jibie == "02")
            //{
            //    sqltext = "select SI_TYPEID,SI_TYNAME,SI_YEAR,SI_PERIOD, round(sum(SI_BEGNUM),2) as SI_BEGNUM,";
            //    sqltext += "round(sum(SI_BEGBAL),2) as SI_BEGBAL,round(sum(SI_ENDNUM),2) as SI_ENDNUM,";
            //    sqltext += "round(sum(SI_ENDBAL),2) as SI_ENDBAL,round(sum(SI_CRCVNUM),2) as SI_CRCVNUM,";
            //    sqltext += "round(sum(SI_CRCVMNY),2) as SI_CRCVMNY, round(sum(SI_CRVDIFF),2) as SI_CRVDIFF,";
            //    sqltext += "round(sum(SI_CSNDNUM),2) as SI_CSNDNUM, round(sum(SI_CSNDMNY),2) as SI_CSNDMNY,";
            //    sqltext += "round(sum(SI_CSNDDIFF),2) as SI_CSNDDIFF  from View_FM_STORAGEBAL ";
            //    sqltext += " where " + condition + " group by SI_TYPEID,SI_TYNAME,SI_YEAR,SI_PERIOD";
            //}
            //else if (jibie == "01")
            //{
            //    sqltext = "select substring(SI_TYPEID,1,2) AS SI_TYPEID,(case when substring(SI_TYPEID,1,2)='01' then  '原材料'  when substring(SI_TYPEID,1,2)='02' then '低值易耗品' else  null end) as SI_TYNAME, ";
            //    sqltext += " round(sum(SI_BEGNUM),2) as SI_BEGNUM,SI_YEAR,SI_PERIOD,";
            //    sqltext += "round(sum(SI_BEGBAL),2) as SI_BEGBAL,round(sum(SI_ENDNUM),2) as SI_ENDNUM,";
            //    sqltext += "round(sum(SI_ENDBAL),2) as SI_ENDBAL,round(sum(SI_CRCVNUM),2) as SI_CRCVNUM,";
            //    sqltext += "round(sum(SI_CRCVMNY),2) as SI_CRCVMNY, round(sum(SI_CRVDIFF),2) as SI_CRVDIFF,";
            //    sqltext += "round(sum(SI_CSNDNUM),2) as SI_CSNDNUM, round(sum(SI_CSNDMNY),2) as SI_CSNDMNY,";
            //    sqltext += "round(sum(SI_CSNDDIFF),2) as SI_CSNDDIFF from View_FM_STORAGEBAL ";
            //    sqltext += " where " + condition + " group by substring(SI_TYPEID,1,2),SI_YEAR,SI_PERIOD ";
            //}
            
            //DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            //if (jibie == "03") //物料按月
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        rpt_monthMar.DataSource = dt;
            //        rpt_monthMar.DataBind();
            //        rpt_monthMar.Visible = true;
            //        rpt_yearMar.DataSource = null;
            //        rpt_yearMar.DataBind();
            //        rpt_yearMar.Visible = false;

            //        rpt_monthType.DataSource = null;
            //        rpt_monthType.DataBind();
            //        rpt_monthType.Visible = false;
            //        rpt_yearType.DataSource = null;
            //        rpt_yearType.DataBind();
            //        rpt_yearType.Visible = false;

            //        NoDataPanel.Visible = false;
            //        btn_export.Visible = true;
            //    }
            //    else
            //    {
            //        rpt_monthMar.DataSource = null;
            //        rpt_monthMar.DataBind();
            //        rpt_yearMar.DataSource = null;
            //        rpt_yearMar.DataBind();
            //        rpt_yearMar.Visible = false;

            //        rpt_monthType.DataSource = null;
            //        rpt_monthType.DataBind();
            //        rpt_monthType.Visible = false;
            //        rpt_yearType.DataSource = null;
            //        rpt_yearType.DataBind();
            //        rpt_yearType.Visible = false;

            //        NoDataPanel.Visible = true;
            //        btn_export.Visible = false;
            //    }
            //}
            //else //物料类别按月
            //{
            //    if (dt.Rows.Count > 0)
            //    {
                    
            //        rpt_monthType.DataSource = dt;
            //        rpt_monthType.DataBind();
            //        rpt_monthType.Visible = true;

            //        rpt_yearType.DataSource = null;
            //        rpt_yearType.DataBind();
            //        rpt_yearType.Visible = false;

            //        rpt_monthMar.DataSource = null;
            //        rpt_monthMar.DataBind();
            //        rpt_monthMar.Visible = false;
            //        rpt_yearMar.DataSource = null;
            //        rpt_yearMar.DataBind();
            //        rpt_yearMar.Visible = false;

            //        NoDataPanel.Visible = false;
            //        btn_export.Visible = true;
            //    }
            //    else
            //    {
            //        rpt_monthType.DataSource = null;
            //        rpt_monthType.DataBind();
            //        //rpt_monthType.Visible = false;
            //        rpt_yearType.DataSource = null;
            //        rpt_yearType.DataBind();
            //        rpt_yearType.Visible = false;

            //        rpt_monthMar.DataSource = null;
            //        rpt_monthMar.DataBind();
            //        rpt_monthMar.Visible = false;
            //        rpt_yearMar.DataSource = null;
            //        rpt_yearMar.DataBind();
            //        rpt_yearMar.Visible = false;

            //        NoDataPanel.Visible = true;
            //        btn_export.Visible = false;
            //    }
            //}
            #endregion

            GetTotalAmount(condition);


            if (jibie == "03")
            {
                string TableName = "View_FM_STORAGEBAL";
                string PrimaryKey = "";
                string ShowFields = "SI_MARID,SI_MARNM,SI_CAIZHI,SI_GUIGE,GB,PURCUNIT,cast(SI_BEGNUM as float) as SI_BEGNUM,cast(SI_BEGBAL as float) as SI_BEGBAL,SI_BEGDIFF,cast(SI_ENDNUM as float) as SI_ENDNUM,cast(SI_ENDBAL as float) as SI_ENDBAL,cast(SI_CRCVNUM as float) as SI_CRCVNUM,cast(SI_CRCVMNY as float) as SI_CRCVMNY, SI_CRVDIFF,cast(SI_CSNDNUM as float) as SI_CSNDNUM,cast(SI_CSNDMNY as float) as SI_CSNDMNY,SI_CSNDDIFF,SI_PTCODE,SI_YEAR,SI_PERIOD ";
                string OrderField = "CONVERT(varchar(100), (SI_YEAR+'-'+SI_PERIOD), 23),SI_MARID";
                int OrderType = 0;
                string StrWhere = condition;                
                int PageSize = 50;

                CurRepeater = rpt_monthMar;
                InitVar(TableName, PrimaryKey, ShowFields, OrderField, StrWhere, OrderType, PageSize);

            }else
            {
                if (jibie == "02")
                {
                    sqltext = "select SI_TYPEID,SI_TYNAME,SI_YEAR,SI_PERIOD, round(sum(SI_BEGNUM),4) as SI_BEGNUM,";
                    sqltext += "round(sum(SI_BEGBAL),2) as SI_BEGBAL,round(sum(SI_ENDNUM),4) as SI_ENDNUM,";
                    sqltext += "round(sum(SI_ENDBAL),2) as SI_ENDBAL,round(sum(SI_CRCVNUM),4) as SI_CRCVNUM,";
                    sqltext += "round(sum(SI_CRCVMNY),2) as SI_CRCVMNY, round(sum(SI_CRVDIFF),2) as SI_CRVDIFF,";
                    sqltext += "round(sum(SI_CSNDNUM),4) as SI_CSNDNUM, round(sum(SI_CSNDMNY),2) as SI_CSNDMNY,";
                    sqltext += "round(sum(SI_CSNDDIFF),2) as SI_CSNDDIFF  from View_FM_STORAGEBAL ";
                    sqltext += " where " + condition + " group by SI_YEAR,SI_PERIOD,SI_TYPEID,SI_TYNAME";
                }
                if (jibie == "01")
                {
                    sqltext = "select substring(SI_TYPEID,1,2) AS SI_TYPEID,(case when substring(SI_TYPEID,1,2)='01' then  '原材料'  when substring(SI_TYPEID,1,2)='02' then '低值易耗品' else  null end) as SI_TYNAME, ";
                    sqltext += " round(sum(SI_BEGNUM),4) as SI_BEGNUM,SI_YEAR,SI_PERIOD,";
                    sqltext += "round(sum(SI_BEGBAL),2) as SI_BEGBAL,round(sum(SI_ENDNUM),4) as SI_ENDNUM,";
                    sqltext += "round(sum(SI_ENDBAL),2) as SI_ENDBAL,round(sum(SI_CRCVNUM),4) as SI_CRCVNUM,";
                    sqltext += "round(sum(SI_CRCVMNY),2) as SI_CRCVMNY, round(sum(SI_CRVDIFF),2) as SI_CRVDIFF,";
                    sqltext += "round(sum(SI_CSNDNUM),4) as SI_CSNDNUM, round(sum(SI_CSNDMNY),2) as SI_CSNDMNY,";
                    sqltext += "round(sum(SI_CSNDDIFF),2) as SI_CSNDDIFF from View_FM_STORAGEBAL ";
                    sqltext += " where " + condition + " group by SI_YEAR,SI_PERIOD,substring(SI_TYPEID,1,2)";
                }


                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                
                if (dt.Rows.Count > 0)
                {
                    
                    rpt_monthType.DataSource = dt;
                    rpt_monthType.DataBind();
                    rpt_monthType.Visible = true;

                    rpt_yearType.DataSource = null;
                    rpt_yearType.DataBind();
                    rpt_yearType.Visible = false;

                    rpt_monthMar.DataSource = null;
                    rpt_monthMar.DataBind();
                    rpt_monthMar.Visible = false;
                    rpt_yearMar.DataSource = null;
                    rpt_yearMar.DataBind();
                    rpt_yearMar.Visible = false;

                    NoDataPanel.Visible = false;
                    btn_export.Visible = true;
                    UCPaging1.Visible = false;
                }
                else
                {
                    rpt_monthType.DataSource = null;
                    rpt_monthType.DataBind();
                    //rpt_monthType.Visible = false;
                    rpt_yearType.DataSource = null;
                    rpt_yearType.DataBind();
                    rpt_yearType.Visible = false;

                    rpt_monthMar.DataSource = null;
                    rpt_monthMar.DataBind();
                    rpt_monthMar.Visible = false;
                    rpt_yearMar.DataSource = null;
                    rpt_yearMar.DataBind();
                    rpt_yearMar.Visible = false;

                    NoDataPanel.Visible = true;
                    btn_export.Visible = false;
                    UCPaging1.Visible = false;
                }
            }
            
            #region

            //else if (jibie == "02")
            //{
            //    string TableName = "View_FM_STORAGEBAL";
            //    string PrimaryKey = "";
            //    string ShowFields = "SI_TYPEID,SI_TYNAME,SI_YEAR,SI_PERIOD, round(sum(SI_BEGNUM),2) as SI_BEGNUM,round(sum(SI_BEGBAL),2) as SI_BEGBAL,round(sum(SI_ENDNUM),2) as SI_ENDNUM, round(sum(SI_ENDBAL),2) as SI_ENDBAL,round(sum(SI_CRCVNUM),2) as SI_CRCVNUM,round(sum(SI_CRCVMNY),2) as SI_CRCVMNY, round(sum(SI_CRVDIFF),2) as SI_CRVDIFF,round(sum(SI_CSNDNUM),2) as SI_CSNDNUM, round(sum(SI_CSNDMNY),2) as SI_CSNDMNY,round(sum(SI_CSNDDIFF),2) as SI_CSNDDIFF  from View_FM_STORAGEBAL";
            //    string OrderField = "SI_TYPEID";
            //    int OrderType = 0;
            //    string StrWhere = condition + " group by "; 
            //    string  GroupBy = " SI_TYPEID,SI_TYNAME,SI_YEAR,SI_PERIOD";
            //    int PageSize = 50;

            //    CurRepeater = rpt_monthType;

            //    InitVar(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere,GroupBy, PageSize, isFristPage);
            //}
            //else if (jibie == "01")
            //{
            //    string TableName = "View_FM_STORAGEBAL";
            //    string PrimaryKey = "";
            //    string ShowFields = "select substring(SI_TYPEID,1,2) AS SI_TYPEID,(case when substring(SI_TYPEID,1,2)='01' then  '原材料'  when substring(SI_TYPEID,1,2)='02' then '低值易耗品' else  null end) as SI_TYNAME, round(sum(SI_BEGNUM),2) as SI_BEGNUM,SI_YEAR,SI_PERIOD,round(sum(SI_BEGBAL),2) as SI_BEGBAL,round(sum(SI_ENDNUM),2) as SI_ENDNUM,round(sum(SI_ENDBAL),2) as SI_ENDBAL,round(sum(SI_CRCVNUM),2) as SI_CRCVNUM,round(sum(SI_CRCVMNY),2) as SI_CRCVMNY, round(sum(SI_CRVDIFF),2) as SI_CRVDIFF,round(sum(SI_CSNDNUM),2) as SI_CSNDNUM, round(sum(SI_CSNDMNY),2) as SI_CSNDMNY,round(sum(SI_CSNDDIFF),2) as SI_CSNDDIFF ";
            //    string OrderField = "substring(SI_TYPEID,1,2)";
            //    int OrderType = 0;
            //    string StrWhere = condition;
            //    string GroupBy = " substring(SI_TYPEID,1,2),SI_YEAR,SI_PERIOD";
            //    int PageSize = 50;

            //    InitVar(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere,GroupBy, PageSize, isFristPage);
            //}
            #endregion

            CheckUser(ControlFinder);
        }

        protected void rpt_monthMar_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Footer)
            {

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelBEGBAL")).Text = hfdBEGBAL.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelCRCVMNY")).Text = hfdCRCVMNY.Value;

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelCSNDMNY")).Text = hfdCSNDMNY.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelENDBAL")).Text = hfdENDBAL.Value;
            }
        }
        protected void rpt_yearMar_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Footer)
            {

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelBEGBAL")).Text = hfdBEGBAL.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelCRCVMNY")).Text = hfdCRCVMNY.Value;

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelCSNDMNY")).Text = hfdCSNDMNY.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelENDBAL")).Text = hfdENDBAL.Value;
            }
        }

        protected void rpt_monthType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Footer)
            {

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelBEGBAL")).Text = hfdBEGBAL.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelCRCVMNY")).Text = hfdCRCVMNY.Value;

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelCSNDMNY")).Text = hfdCSNDMNY.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelENDBAL")).Text = hfdENDBAL.Value;
            }
        }

        protected void rpt_yearType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Footer)
            {

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelBEGBAL")).Text = hfdBEGBAL.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelCRCVMNY")).Text = hfdCRCVMNY.Value;

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelCSNDMNY")).Text = hfdCSNDMNY.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("LabelENDBAL")).Text = hfdENDBAL.Value;
            }
        }

        //刷新条件选择页面
        private void refreshStyle()
        {
            ddlType.SelectedValue = "03";
            rbl1.SelectedValue = "month";
            //txtStartTime.Text.Split('-')[1] = DateTime.Now.ToString("yyyymmdd").Substring(4, 2);
            //txtStartTime.Text.Split('-')[0] = DateTime.Now.ToString("yyyymmdd").Substring(0, 4);
            bindGV();
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
                    (gr.FindControl("TextBoxValue") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                    //(gr.FindControl("ddlTypeValue") as DropDownList).Style.Add("display", "none");
                }
                else
                {
                    (gr.FindControl("DropDownListLogic") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_logic") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_name") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_relation") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                    (gr.FindControl("TextBoxValue") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                    //(gr.FindControl("ddlTypeValue") as DropDownList).Style.Add("display", "none");
                }
            }
        }
        //重置条件选择页面
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

        private void bindGV()
        {
            GridViewSearch.DataSource = CreateTable();

            GridViewSearch.DataBind();
        }
        private System.Data.DataTable CreateTable()
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            dt.Columns.Add(new DataColumn("index", typeof(int)));


            for (int i = 0; i < 5; i++)
            {
                DataRow row = dt.NewRow();
                row["index"] = i;
                dt.Rows.Add(row);
            }

            return dt;
        }

        private string ConvertRelation(string field, string relation, string fieldValue)
        {
            string obj = string.Empty;

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
                case "7":
                    {
                        //不包含
                        obj = field + " not LIKE  '%" + fieldValue + "%'";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return obj;
        }

        #region  分页  UCPaging

        /// <summary>
        /// 初始化分布信息
        /// </summary>
         
        private void InitVar(string _tablename, string _primarykey, string _showfields, string _orderfield, string _strwhere, int _ordertype, int _pagesize)
        {
            InitPager(_tablename, _primarykey, _showfields, _orderfield, _strwhere,_ordertype, _pagesize);
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize; //每页显示的记录数
            bindGrid(CurRepeater);
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager(string _tablename, string _primarykey, string _showfields, string _orderfield, string _strwhere,int _ordertype, int _pagesize)
        {
            //this.GetListName();
            pager.TableName = _tablename;
            pager.PrimaryKey = _primarykey;
            pager.ShowFields = _showfields;
            pager.OrderField = _orderfield; //"dbo.f_formatstr(" + ddlSort.SelectedValue.ToString() + ", '.')";
            pager.StrWhere = _strwhere; // ViewState["sqlText"].ToString();
            pager.OrderType = _ordertype; //升序排列            
            pager.PageSize = _pagesize;
            
        }

        void Pager_PageChanged(int pageNumber)
        {
            //Control[] CRL = this.BindGridParamsRecord(ViewState["CurrentUCPaging"].ToString());
            //bindGrid((UCPaging)CRL[0], (GridView)CRL[1], (Panel)CRL[2]);
         
              getData();
            
        }

        private void bindGrid(Repeater ParamRepeater)
        {
            pager.PageIndex =UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, ParamRepeater, UCPaging1, NoDataPanel);
            ParamRepeater.Visible = true;
            if (NoDataPanel.Visible)
            {
              UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            if (ParamRepeater.Equals(rpt_monthMar))
            {
                              
                rpt_yearMar.DataSource = null;
                rpt_yearMar.DataBind();
                rpt_yearMar.Visible = false;

                rpt_monthType.DataSource = null;
                rpt_monthType.DataBind();
                rpt_monthType.Visible = false;
                rpt_yearType.DataSource = null;
                rpt_yearType.DataBind();
                rpt_yearType.Visible = false;
            }
            else
            {
                rpt_monthMar.DataSource = null;
                rpt_monthMar.DataBind();
                rpt_monthMar.Visible = false;

                rpt_monthType.DataSource = null;
                rpt_monthType.DataBind();
                rpt_monthType.Visible = false;
                rpt_yearType.DataSource = null;
                rpt_yearType.DataBind();
                rpt_yearType.Visible = false;
            }
        }
        #endregion







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






        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
            {
                string sqlifhs = "select * from TBFM_HSTOTAL where HS_YEAR='" + dplYear.SelectedValue.ToString().Trim() + "' and HS_MONTH='" + dplMoth.SelectedValue.ToString().Trim() + "'";
                System.Data.DataTable dtifhs=DBCallCommon.GetDTUsingSqlText(sqlifhs);
                if (dtifhs.Rows.Count > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('该月已核算，不能再更新!')</script>");
                    return;
                }
                else if(dtifhs.Rows.Count==0)
                {
                    updatesfchzsr();
                    updatesfchzfc();
                    GetData();
                    bindGV();
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('请选择年月!')</script>");
                return;
            }
        }

        private void updatesfchzsr()
        {
            List<string> sqlrklist = new List<string>();
           string yearmonth=dplYear.SelectedValue.ToString().Trim()+"-"+dplMoth.SelectedValue.ToString().Trim();
           string sqlrk = "select * from ((select * from TBFM_STORAGEBAL where (SI_YEAR+'-'+SI_PERIOD)='" + yearmonth + "')a left join (select isnull(sum(isnull(WG_RSNUM,0)),0) as WG_RSNUM,isnull(sum(isnull(WG_AMOUNT,0)),0) as WG_AMOUNT,WG_MARID,left(CONVERT(CHAR(10), WG_VERIFYDATE, 23),7) as WG_VERIFYDATE from View_SM_IN where left(CONVERT(CHAR(10), WG_VERIFYDATE, 23),7)='" + yearmonth + "' and WG_STATE='2' group by left(CONVERT(CHAR(10), WG_VERIFYDATE, 23),7),WG_MARID)b on a.SI_MARID=b.WG_MARID) where WG_VERIFYDATE='" + yearmonth + "' and (SI_YEAR+'-'+SI_PERIOD)='" + yearmonth + "'";
           System.Data.DataTable dtrk = DBCallCommon.GetDTUsingSqlText(sqlrk);
            if(dtrk.Rows.Count>0)
            {
                for (int i = 0; i < dtrk.Rows.Count; i++)
                {
                    string sqlupdatesr = "update TBFM_STORAGEBAL set SI_CRCVNUM='" + Convert.ToDouble(dtrk.Rows[i]["WG_RSNUM"].ToString()) + "',SI_CRCVMNY='" + Convert.ToDouble(dtrk.Rows[i]["WG_AMOUNT"].ToString()) + "' where (SI_YEAR+'-'+SI_PERIOD)='" + yearmonth + "' and SI_MARID='" + dtrk.Rows[i]["SI_MARID"].ToString() + "'";
                    sqlrklist.Add(sqlupdatesr);
                }
                DBCallCommon.ExecuteTrans(sqlrklist);
            }
        }
        private void updatesfchzfc()
        {
            List<string> sqlcklist = new List<string>();
            string yearmonth = dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim();
            string sqlck = "select * from ((select * from TBFM_STORAGEBAL where (SI_YEAR+'-'+SI_PERIOD)='" + yearmonth + "')a left join (select isnull(sum(isnull(RealNumber,0)),0) as RealNumber,isnull(sum(isnull(Amount,0)),0) as Amount,MaterialCode,left(CONVERT(CHAR(10), ApprovedDate, 23),7) as ApprovedDate from View_SM_OUT where left(CONVERT(CHAR(10), ApprovedDate, 23),7)='" + yearmonth + "' and TotalState='2' group by left(CONVERT(CHAR(10), ApprovedDate, 23),7),MaterialCode)b on a.SI_MARID=b.MaterialCode) where ApprovedDate='" + yearmonth + "' and (SI_YEAR+'-'+SI_PERIOD)='" + yearmonth + "'";
            System.Data.DataTable dtck = DBCallCommon.GetDTUsingSqlText(sqlck);
            if (dtck.Rows.Count > 0)
            {
                for (int i = 0; i < dtck.Rows.Count; i++)
                {
                    string sqlupdatefc = "update TBFM_STORAGEBAL set SI_CSNDNUM='" + Convert.ToDouble(dtck.Rows[i]["RealNumber"].ToString()) + "',SI_CSNDMNY='" + Convert.ToDouble(dtck.Rows[i]["Amount"].ToString()) + "' where (SI_YEAR+'-'+SI_PERIOD)='" + yearmonth + "' and SI_MARID='" + dtck.Rows[i]["SI_MARID"].ToString() + "'";
                    sqlcklist.Add(sqlupdatefc);
                }
                DBCallCommon.ExecuteTrans(sqlcklist);
            }
        }
    }
}
