using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using ZCZJ_DPF;
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using System.Runtime.InteropServices;
using NPOI.HSSF.UserModel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;


namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_CPFYJSDGL : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                bindrpt();
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            bindrpt();
        }

        private void bindrpt()
        {
            pager_org.TableName = "PM_CPFYJSD";
            pager_org.PrimaryKey = "JS_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "JS_ID";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = CommonFun.ComTryInt(ddl_pagesize.SelectedItem.Text.ToString());
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptJSDGL, UCPaging1, NoDataPane);

            //计算总金额
            string sqlzong = "select * from PM_CPFYJSD where " + StrWhere() + " ";
            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqlzong);
            double tot_money = 0;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows[i]["JS_HSJE"].ToString() == "")
                {
                    tot_money += 0;
                }
                else
                {
                    tot_money += Convert.ToDouble(dt1.Rows[i]["JS_HSJE"].ToString());
                }
            }
            lb_select_money.Text = tot_money.ToString("0.00");

            if (NoDataPane.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        private string StrWhere()
        {
            string sql = "JS_ID !=''";
            //if (txtZDRQ.Text.Trim()!="")
            //{
            //    sql += " and JS_RQ like '%" + txtZDRQ.Text.Trim() + "%'";
            //}
            if (txtDJBH.Text.Trim() != "")
            {
                sql += " and JS_BH like '%" + txtDJBH.Text.Trim() + "%'";
            }
            if (txtRWH.Text.Trim() != "")
            {
                sql += " and JS_RWH like '%" + txtRWH.Text.Trim() + "%'";
            }
            if (txtHTH.Text.Trim().Trim() != "")
            {
                sql += " and JS_HTH like '%" + txtHTH.Text.Trim() + "%'";
            }
            if (txtSHDW.Text.Trim() != "")
            {
                sql += " and JS_SHDW like '%" + txtSHDW.Text.Trim() + "%'";
            }
            if (txtFHS.Text.Trim() != "")
            {
                sql += " and JS_GYS like '%" + txtFHS.Text.Trim() + "%'";
            }

            if (txtZDRQ.Text.Trim() != "")
            {
                sql += " and left(JS_RQ,10)>='" + txtZDRQ.Text.Trim() + "'";
            }
            if (txtJZRQ.Text.Trim() != "")
            {
                sql += " and left(JS_RQ,10)<='" + txtJZRQ.Text.Trim() + "'";
            }

            if (radio1_gouji.Checked)
            {
                sql += " and JS_GJSTATE='3'";
            }
            if (radio1_weigouji.Checked)
            {
                sql += " and JS_GJSTATE='0'";
            }

            if (drp_type.SelectedIndex == 1)
            {
                sql += " and patindex('%RED%',JS_BH)=0";// and JS_JHGZH not in(select SUBSTRING(PM_CPFYJSD.JS_JHGZH,1,LEN(PM_CPFYJSD.JS_JHGZH) - 3) from PM_CPFYJSD where patindex('%RED%',JS_JHGZH)>0)
            }
            if (drp_type.SelectedIndex == 2)
            {
                sql += " and patindex('%RED%',JS_BH)>0";// or JS_JHGZH in(select SUBSTRING(PM_CPFYJSD.JS_JHGZH,1,LEN(PM_CPFYJSD.JS_JHGZH) - 3) from PM_CPFYJSD where patindex('%RED%',JS_JHGZH)>0))
            }
            return sql;
        }

        List<string> list = new List<string>();
        protected void rptJSDGL_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Label JS_BH = (System.Web.UI.WebControls.Label)e.Item.FindControl("JS_BH");
                System.Web.UI.WebControls.Label JS_ZDR = (System.Web.UI.WebControls.Label)e.Item.FindControl("JS_ZDR");
                System.Web.UI.WebControls.Label JS_RQ = (System.Web.UI.WebControls.Label)e.Item.FindControl("JS_RQ");

                if (list.Count == 0)
                {
                    list.Add(JS_BH.Text);
                }
                else
                {
                    if (list.Contains(JS_BH.Text))
                    {
                        JS_BH.Visible = false;
                        JS_ZDR.Visible = false;
                        JS_RQ.Visible = false;
                    }
                    else
                    {
                        list.Add(JS_BH.Text);
                    }
                }
            }
        }

        protected void btnSearch_btnSearch(object sender, EventArgs e)
        {
            bindrpt();
        }

        protected void btnReset_OnClick(object sender, EventArgs e)
        {
            txtRWH.Text = string.Empty;
            txtHTH.Text = string.Empty;
            txtSHDW.Text = string.Empty;
            txtFHS.Text = string.Empty;
        }
        protected void drp_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindrpt();
        }

        protected void ddl_pagesize_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindrpt();
        }
        protected void radiogjstate_CheckedChanged(object sender, EventArgs e)
        {
            bindrpt();
        }


        //生成红字单据
        protected void btnxtred_click(object sender, EventArgs e)
        {
            string zdrid = Session["UserID"].ToString().Trim();
            List<string> list_sql = new List<string>();
            list_sql.Clear();
            string sqltext = "";
            int num = 0;
            int docnum = 0;
            string checkdocnum = "";
            for (int i = 0; i < rptJSDGL.Items.Count; i++)
            {
                if (((System.Web.UI.WebControls.CheckBox)rptJSDGL.Items[i].FindControl("chk")).Checked)
                {
                    if (checkdocnum == "")
                    {
                        checkdocnum = ((System.Web.UI.WebControls.Label)rptJSDGL.Items[i].FindControl("JS_BH")).Text.Trim();
                        docnum++;
                    }
                    if (checkdocnum != "" && ((System.Web.UI.WebControls.Label)rptJSDGL.Items[i].FindControl("JS_BH")).Text.Trim() != checkdocnum)
                    {
                        docnum++;
                    }
                    num++;
                }
            }
            if (docnum > 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请选择同一张单据的数据！');", true);
                return;
            }
            if (num == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请勾选要下推红字的数据！');", true);
                return;
            }
            else
            {
                string redjsdrq = "";
                string redfatherid = "";
                string redjsdh = "";
                string redzdr = "";
                string redjhgzh = "";

                string sqlgetrq = "select * from TBFM_YFHS where YFHS_STATE='1' and YFHS_YEAR+'-'+YFHS_MONTH like '" + DateTime.Now.ToString("yyyy-MM") + "%'";
                System.Data.DataTable dtgetrq = DBCallCommon.GetDTUsingSqlText(sqlgetrq);
                redjsdrq = DateTime.Now.ToString("yyyy-MM-dd");
                if (dtgetrq.Rows.Count > 0)
                {
                    if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 12)
                    {
                        redjsdrq = DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01";
                    }
                    else if (Convert.ToInt32(DateTime.Now.Month.ToString()) == 12)
                    {
                        redjsdrq = DateTime.Now.AddYears(1).AddMonths(-11).ToString("yyyy-MM") + "-01";
                    }
                }
                for (int i = 0; i < rptJSDGL.Items.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)rptJSDGL.Items[i].FindControl("chk")).Checked)
                    {
                        string jhgzhthis = ((System.Web.UI.WebControls.Label)rptJSDGL.Items[i].FindControl("JS_JHGZH")).Text.Trim();
                        string sqlcheck = "select * from PM_CPFYJSD where JS_JHGZH='" + jhgzhthis + "RED'";
                        System.Data.DataTable dtcheck = DBCallCommon.GetDTUsingSqlText(sqlcheck);
                        if (dtcheck.Rows.Count > 0 || jhgzhthis.Contains("RED"))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('有数据已下推过红字或为红字数据，请不要重复操作！');", true);
                            return;
                        }

                        string sqlgetdatainfo = "";
                        sqlgetdatainfo = "select * from PM_CPFYJSD where JS_JHGZH='" + jhgzhthis + "'";
                        System.Data.DataTable dtgetdatainfo = DBCallCommon.GetDTUsingSqlText(sqlgetdatainfo);
                        if (dtgetdatainfo.Rows.Count > 0)
                        {
                            redfatherid = dtgetdatainfo.Rows[0]["JS_FATHERID"].ToString().Trim() + "RED" + DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + zdrid;
                            redjsdh = dtgetdatainfo.Rows[0]["JS_BH"].ToString().Trim() + "RED" + DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + zdrid;

                            redzdr = Session["UserName"].ToString();
                            redjhgzh = dtgetdatainfo.Rows[0]["JS_JHGZH"].ToString().Trim() + "RED";
                            sqltext = "insert into PM_CPFYJSD(JS_FATHERID,JS_BH,JS_RQ,JS_ZDR,JS_GYS,JS_BZ,JS_JHGZH,JS_HTH,JS_XMMC,JS_RWH,JS_ZX,JS_SBMC,JS_TUHAO,JS_JHQ,JS_SHDW,JS_BJSL,JS_JSSL,JS_DANZ,JS_ZONGZ,JS_SHUIL,JS_HSJE,JS_XTSTATE,JS_GJSTATE) values('" + redfatherid + "','" + redjsdh + "','" + redjsdrq + "','" + redzdr + "','" + dtgetdatainfo.Rows[0]["JS_GYS"].ToString().Trim() + "','" + dtgetdatainfo.Rows[0]["JS_BZ"].ToString().Trim() + "','" + redjhgzh + "','" + dtgetdatainfo.Rows[0]["JS_HTH"].ToString().Trim() + "','" + dtgetdatainfo.Rows[0]["JS_XMMC"].ToString().Trim() + "','" + dtgetdatainfo.Rows[0]["JS_RWH"].ToString().Trim() + "','" + dtgetdatainfo.Rows[0]["JS_ZX"].ToString().Trim() + "','" + dtgetdatainfo.Rows[0]["JS_SBMC"].ToString().Trim() + "','" + dtgetdatainfo.Rows[0]["JS_TUHAO"].ToString().Trim() + "','" + dtgetdatainfo.Rows[0]["JS_JHQ"].ToString().Trim() + "','" + dtgetdatainfo.Rows[0]["JS_SHDW"].ToString().Trim() + "'," + (-(CommonFun.ComTryDecimal(dtgetdatainfo.Rows[0]["JS_BJSL"].ToString().Trim()))) + "," + (-(CommonFun.ComTryDecimal(dtgetdatainfo.Rows[0]["JS_JSSL"].ToString().Trim()))) + "," + (-(CommonFun.ComTryDecimal(dtgetdatainfo.Rows[0]["JS_DANZ"].ToString().Trim()))) + "," + (-(CommonFun.ComTryDecimal(dtgetdatainfo.Rows[0]["JS_ZONGZ"].ToString().Trim()))) + "," + CommonFun.ComTryDecimal(dtgetdatainfo.Rows[0]["JS_SHUIL"].ToString().Trim()) + "," + (-(CommonFun.ComTryDecimal(dtgetdatainfo.Rows[0]["JS_HSJE"].ToString().Trim()))) + ",'" + dtgetdatainfo.Rows[0]["JS_XTSTATE"].ToString().Trim() + "','" + dtgetdatainfo.Rows[0]["JS_GJSTATE"].ToString().Trim() + "')";
                            list_sql.Add(sqltext);
                        }
                    }
                }
                DBCallCommon.ExecuteTrans(list_sql);
                Response.Redirect("PM_CPFYJSD.aspx?action=read&SHEETNO=" + redfatherid);
            }
        }



        //勾选导出
        #region 导出数据
        private int ifselect()
        {
            int flag = 0;
            int i = 0;//是否选择数据
            foreach (RepeaterItem Reitem in rptJSDGL.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("chk") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                }
            }
            if (i == 0)//未选择数据
            {
                flag = 1;
            }
            else
            {
                flag = 0;
            }
            return flag;
        }
        protected void btnexport_OnClick(object sender, EventArgs e)
        {
            int flag = ifselect();
            if (flag == 0)//判断是否有勾选框被勾选
            {
                string JS_JHGZHdc = "";
                foreach (RepeaterItem Reitem in rptJSDGL.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("chk") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx.Checked)
                    {
                        JS_JHGZHdc += "'" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("JS_JHGZH")).Text.ToString() + "'" + ",";
                    }
                }
                JS_JHGZHdc = JS_JHGZHdc.Substring(0, JS_JHGZHdc.LastIndexOf(",")).ToString();
                string sqltextdc = "";
                sqltextdc = "select JS_BH,JS_ZDR,JS_RQ,JS_JHGZH,JS_HTH,JS_RWH,JS_ZX,JS_GYS,JS_JHQ,JS_SHDW,JS_BJSL,JS_DANZ,JS_SHUIL,JS_HSJE from PM_CPFYJSD where JS_JHGZH in(" + JS_JHGZHdc + ") order by JS_ID desc";
                System.Data.DataTable dtdc = DBCallCommon.GetDTUsingSqlText(sqltextdc);
                ExportDataItem(dtdc);
            }
            else if (flag == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要导出的数据！！！');", true);
            }
        }
        private void ExportDataItem(System.Data.DataTable dt)
        {

            string filename = "运费均摊数据" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("运费均摊导出.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
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
    }
}
