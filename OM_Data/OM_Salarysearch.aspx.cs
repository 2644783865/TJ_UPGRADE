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
using System.Collections.Generic;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_Salarysearch : BasicPage
    {
        public static DataTable dt = new DataTable();//定义datatable
        ArrayList Arrhead = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string dengluid = Session["UserID"].ToString();
                string dengluname = Session["UserName"].ToString();
                string ym = "工资汇总查询";
                string sql = "insert OM_GZ_JL (USER_ID,USER_NAME,TIME,YM) values ('" + dengluid + "','" + dengluname + "','" + DateTime.Now.ToString() + "','" + ym + "')";
                DBCallCommon.ExeSqlText(sql);
                bindgouxuanbm();
                bindgouxuanbz();
                bindgouxuangwxl();
                bindhetongzhuti();
                GetSele();
            }
            CheckUser(ControlFinder);
        }
        private void bindgouxuanbm()
        {
            string sqlbumen = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO WHERE len(DEP_CODE)=2";
            System.Data.DataTable dtbumen = DBCallCommon.GetDTUsingSqlText(sqlbumen);
            listdepartment.DataTextField = "DEP_NAME";
            listdepartment.DataValueField = "DEP_CODE";
            listdepartment.DataSource = dtbumen;
            listdepartment.DataBind();
            ListItem item = new ListItem("全部", "");
            listdepartment.Items.Insert(0, item);
        }

        private void bindgouxuanbz()
        {
            string sqlbanzu = "SELECT DISTINCT ST_DEPID1 FROM TBDS_STAFFINFO where ST_DEPID1!='' and ST_DEPID1 is not null and ST_DEPID1!='0'";
            System.Data.DataTable dtbanzu = DBCallCommon.GetDTUsingSqlText(sqlbanzu);
            listbanzu.DataTextField = "ST_DEPID1";
            listbanzu.DataValueField = "ST_DEPID1";
            listbanzu.DataSource = dtbanzu;
            listbanzu.DataBind();
            ListItem item = new ListItem("全部", "");
            listbanzu.Items.Insert(0, item);
        }

        private void bindgouxuangwxl()
        {
            string sqlposition = "SELECT DISTINCT ST_SEQUEN FROM TBDS_STAFFINFO WHERE ST_SEQUEN is not null and ST_SEQUEN!=''";
            System.Data.DataTable dtposition = DBCallCommon.GetDTUsingSqlText(sqlposition);
            listposition.DataTextField = "ST_SEQUEN";
            listposition.DataValueField = "ST_SEQUEN";
            listposition.DataSource = dtposition;
            listposition.DataBind();
            ListItem item = new ListItem("全部", "");
            listposition.Items.Insert(0, item);
        }

        private void bindhetongzhuti()
        {
            string sqlhetongzhuti = "SELECT DISTINCT ST_CONTR FROM TBDS_STAFFINFO where ST_CONTR is not null and ST_CONTR!=''";
            System.Data.DataTable dthetongzhuti = DBCallCommon.GetDTUsingSqlText(sqlhetongzhuti);
            listhetongzhuti.DataTextField = "ST_CONTR";
            listhetongzhuti.DataValueField = "ST_CONTR";
            listhetongzhuti.DataSource = dthetongzhuti;
            listhetongzhuti.DataBind();
            ListItem item = new ListItem("全部", "");
            listhetongzhuti.Items.Insert(0, item);
        }

        private void GetSele()//绑定筛选信息
        {
            string sqlText = "select * from VIEW_OM_GZQD_SELECT";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            foreach (Control item in palORG.Controls)
            {
                if (item is DropDownList)
                {
                    if (item.ID.Contains("screen"))
                    {
                        ((DropDownList)item).DataSource = dt;
                        ((DropDownList)item).DataTextField = "name";
                        ((DropDownList)item).DataValueField = "id";
                        ((DropDownList)item).DataBind();
                        ((DropDownList)item).SelectedValue = "0";
                    }
                }
            }
        }

        private void getdt()
        {
            if (startdate.Value.Trim() == "" || enddate.Value.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择起始年月！！！');", true);
                return;
            }
            string startyearmonth = startdate.Value.Trim();
            string endyearmonth = enddate.Value.Trim();

            string sql = "select distinct QD_STID,ST_NAME as 姓名,DEP_NAME as 部门,ST_DEPID1 as 班组,ST_CONTR as 合同主体 from View_OM_GZQD where " + StrWhere() + " order by DEP_NAME";
            string xuhjxiang = "(0";
            for (int i = 0; i < CheckBoxListhj.Items.Count; i++)
            {
                if (CheckBoxListhj.Items[i].Selected == true)
                {
                    xuhjxiang += "+" + CheckBoxListhj.Items[i].Value.ToString().Trim() + "";
                }
            }
            xuhjxiang += ")";

            dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                if (string.Compare(endyearmonth, startyearmonth) >= 0)
                {
                    try
                    {
                        DateTime datetimestart = DateTime.Parse(startyearmonth.ToString().Trim());
                        DateTime datetimeend = DateTime.Parse(endyearmonth.ToString().Trim());
                        int monthnum = (datetimeend.Year - datetimestart.Year) * 12 + datetimeend.Month - datetimestart.Month + 1;
                        if (monthnum > 18)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('查询时间超过一年半！！！');", true);
                            return;
                        }
                        for (int i = 0; i < monthnum; i++)
                        {
                            DateTime datethis = datetimestart.AddMonths(i);
                            string datethisyearmonth = datethis.ToString("yyyy-MM").Trim();
                            Arrhead.Add(datethisyearmonth);
                            DataColumn dc = new DataColumn(datethisyearmonth);
                            dt.Columns.Add(dc);
                        }
                        DataColumn dchj = new DataColumn("小计");
                        dt.Columns.Add(dchj);
                        DataColumn dcaver = new DataColumn("按月平均");
                        dt.Columns.Add(dcaver);
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('年月转换出现错误，请检查年月格式是否正确！！！');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('截止年月应大于或等于起始年月！！！');", true);
                    return;
                }
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    decimal perhj = 0;
                    decimal pernum = 0;
                    decimal persalary = 0;
                    if (Arrhead.Count > 0)
                    {
                        for (int k = 0; k < Arrhead.Count; k++)
                        {
                            string sqlgr = "select " + xuhjxiang + " as statics from OM_GZQD where QD_YEARMONTH='" + Arrhead[k].ToString().Trim() + "' and QD_STID='" + dt.Rows[j]["QD_STID"].ToString().Trim() + "' and QD_SHBH in(select GZSH_BH from OM_GZHSB where OM_GZSCBZ='1' and GZ_YEARMONTH='" + Arrhead[k].ToString().Trim() + "')";
                            DataTable dtgr = DBCallCommon.GetDTUsingSqlText(sqlgr);
                            if (dtgr.Rows.Count > 0)
                            {
                                dt.Rows[j][k + 5] = dtgr.Rows[0]["statics"].ToString().Trim() == "0" ? "" : (Math.Round(CommonFun.ComTryDecimal(dtgr.Rows[0]["statics"].ToString().Trim()),2)).ToString().Trim();
                                perhj += Math.Round(CommonFun.ComTryDecimal(dtgr.Rows[0]["statics"].ToString().Trim()), 2);
                                if (CommonFun.ComTryDecimal(dtgr.Rows[0]["statics"].ToString().Trim()) > 0 || CommonFun.ComTryDecimal(dtgr.Rows[0]["statics"].ToString().Trim()) < 0)
                                {
                                    pernum += 1;
                                }
                            }
                        }
                        dt.Rows[j]["小计"] = perhj.ToString().Trim() == "0" ? "" : perhj.ToString().Trim();
                        if (pernum > 0)
                        {
                            persalary = Math.Round((perhj / pernum), 2);
                            dt.Rows[j]["按月平均"] = persalary.ToString().Trim();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            string sql = "1=1 and OM_GZSCBZ='1'";
            if (txtName.Text.Trim() != "")
            {
                //sql += " and ST_NAME like '%" + txtName.Text.Trim() + "%'";
                string[] strarray = txtName.Text.Trim().Split(',', '，');
                string strname = "";
                for (int i = 0; i < strarray.Length; i++)
                {
                    strname += "'" + strarray[i] + "',";
                }
                strname = strname.Substring(0, strname.Length - 1);
                sql += " and ST_NAME in(" + strname + ")";
            }
            if (txtworkno.Text.Trim() != "")
            {
                sql += " and ST_WORKNO like '%" + txtworkno.Text.Trim() + "%'";
            }
            if (startdate.Value.Trim() != "")
            {
                sql += " and QD_YEARMONTH>='" + startdate.Value.Trim() + "'";
            }
            if (enddate.Value.Trim() != "")
            {
                sql += " and QD_YEARMONTH<='" + enddate.Value.Trim() + "'";
            }
            //部门
            for (int i = 0; i < listdepartment.Items.Count; i++)
            {
                if (listdepartment.Items[i].Selected == true)
                {
                    num1++;
                }
            }
            if (num1 > 0)
            {
                sql += " and (ST_DEPID='isnotexist'";
                for (int i = 0; i < listdepartment.Items.Count; i++)
                {
                    if (listdepartment.Items[i].Selected == true)
                    {
                        sql += " or ST_DEPID like '%" + listdepartment.Items[i].Value + "%'";
                    }
                }
                sql += ")";
            }
            //班组
            for (int j = 0; j < listbanzu.Items.Count; j++)
            {
                if (listbanzu.Items[j].Selected == true)
                {
                    num2++;
                }
            }
            if (num2 > 0)
            {
                sql += " and (ST_DEPID1='isnotexist'";
                for (int j = 0; j < listbanzu.Items.Count; j++)
                {
                    if (listbanzu.Items[j].Selected == true)
                    {
                        sql += " or ST_DEPID1 like '%" + listbanzu.Items[j].Value + "%'";
                    }
                }
                sql += ")";
            }

            //岗位序列
            for (int k = 0; k < listposition.Items.Count; k++)
            {
                if (listposition.Items[k].Selected == true)
                {
                    num3++;
                }
            }
            if (num3 > 0)
            {
                sql += " and (ST_SEQUEN='isnotexist'";
                for (int k = 0; k < listposition.Items.Count; k++)
                {
                    if (listposition.Items[k].Selected == true)
                    {
                        sql += " or ST_SEQUEN like '%" + listposition.Items[k].Value + "%'";
                    }
                }
                sql += ")";
            }

            //合同主体
            for (int m = 0; m < listhetongzhuti.Items.Count; m++)
            {
                if (listhetongzhuti.Items[m].Selected == true)
                {
                    num4++;
                }
            }
            if (num4 > 0)
            {
                sql += " and (ST_CONTR='isnotexist'";
                for (int m = 0; m < listhetongzhuti.Items.Count; m++)
                {
                    if (listhetongzhuti.Items[m].Selected == true)
                    {
                        sql += " or ST_CONTR like '%" + listhetongzhuti.Items[m].Value + "%'";
                    }
                }
                sql += ")";
            }
            //筛选

            if (screen1.SelectedValue != "0" || screen2.SelectedValue != "0" || screen3.SelectedValue != "0" || screen4.SelectedValue != "0" || screen5.SelectedValue != "0" || screen6.SelectedValue != "0" || screen7.SelectedValue != "0" || screen8.SelectedValue != "0" || screen9.SelectedValue != "0" || screen10.SelectedValue != "0")
            {
                if (SelectStr(screen1, ddlRelation1, Txt1.Text.Trim(), "") != "")
                {
                    sql += " and (" + SelectStr(screen1, ddlRelation1, Txt1.Text.Trim(), "");
                }
                else
                {
                    sql += " and (1=1 ";
                }
                sql += SelectStr(screen2, ddlRelation2, Txt2.Text.Trim(), ddlLogic1.SelectedValue);
                sql += SelectStr(screen3, ddlRelation3, Txt3.Text.Trim(), ddlLogic2.SelectedValue);
                sql += SelectStr(screen4, ddlRelation4, Txt4.Text.Trim(), ddlLogic3.SelectedValue);
                sql += SelectStr(screen5, ddlRelation5, Txt5.Text.Trim(), ddlLogic4.SelectedValue);
                sql += SelectStr(screen6, ddlRelation6, Txt6.Text.Trim(), ddlLogic5.SelectedValue);
                sql += SelectStr(screen7, ddlRelation7, Txt7.Text.Trim(), ddlLogic6.SelectedValue);
                sql += SelectStr(screen8, ddlRelation8, Txt8.Text.Trim(), ddlLogic7.SelectedValue);
                sql += SelectStr(screen9, ddlRelation9, Txt9.Text.Trim(), ddlLogic8.SelectedValue);
                sql += SelectStr(screen10, ddlRelation10, Txt10.Text.Trim(), ddlLogic9.SelectedValue);
                sql += ")";
            }
            //
            return sql;
        }


        private string SelectStr(DropDownList ddl, DropDownList ddl1, string txt, string logic) //选择条件拼接字符串
        {
            string sqlstr = string.Empty;
            if (ddl.SelectedValue != "0")
            {
                switch (ddl1.SelectedValue)
                {
                    case "0":
                        sqlstr = string.Format("{0} {1} like '%{2}%' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "1":
                        sqlstr = string.Format("{0} {1} not like '%{2}%' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "2":
                        sqlstr = string.Format("{0} {1}={2} ", logic, ddl.SelectedValue, CommonFun.ComTryDecimal(txt));
                        break;
                    case "3":
                        sqlstr = string.Format("{0} {1}!={2} ", logic, ddl.SelectedValue, CommonFun.ComTryDecimal(txt));
                        break;
                    case "4":
                        sqlstr = string.Format("{0} {1}>{2} ", logic, ddl.SelectedValue, CommonFun.ComTryDecimal(txt));
                        break;
                    case "5":
                        sqlstr = string.Format("{0} {1}>={2} ", logic, ddl.SelectedValue, CommonFun.ComTryDecimal(txt));
                        break;
                    case "6":
                        sqlstr = string.Format("{0} {1}<{2} ", logic, ddl.SelectedValue, CommonFun.ComTryDecimal(txt));
                        break;
                    case "7":
                        sqlstr = string.Format("{0} {1}<={2} ", logic, ddl.SelectedValue, CommonFun.ComTryDecimal(txt));
                        break;
                }
            }
            return sqlstr;
        }

        protected void btn_confirm1_Click(object sender, EventArgs e)
        {
            getdt();
        }


        protected void btn_clear1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listdepartment.Items.Count; i++)
            {
                listdepartment.Items[i].Selected = false;
            }
            for (int j = 0; j < listbanzu.Items.Count; j++)
            {
                listbanzu.Items[j].Selected = false;
            }
            for (int k = 0; k < listposition.Items.Count; k++)
            {
                listposition.Items[k].Selected = false;
            }
            for (int m = 0; m < listhetongzhuti.Items.Count; m++)
            {
                listhetongzhuti.Items[m].Selected = false;
            }
            getdt();
        }


        protected void btn_confirm2_Click(object sender, EventArgs e)
        {
            getdt();
        }

        protected void btn_clear2_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < CheckBoxListhj.Items.Count; k++)
            {
                listhetongzhuti.Items[k].Selected = false;
            }
            getdt();
        }

        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            getdt();
        }


        protected void btnexport_click(object sender, EventArgs e)
        {
            string filename = "工资汇总" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("工资汇总.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet

                IRow rowhead = sheet0.CreateRow(0);
                for (int h = 1; h < dt.Columns.Count; h++)
                {
                    ICell cellhead = rowhead.CreateCell(h);
                    cellhead.SetCellValue(dt.Columns[h].ColumnName.ToString().Trim());
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 1; j < 5; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString().Trim());
                    }
                    for (int k = 5; k < dt.Columns.Count; k++)
                    {
                        row.CreateCell(k).SetCellValue(CommonFun.ComTryDouble(dt.Rows[i][k].ToString().Trim()));
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
    }
}
