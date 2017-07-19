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
using NPOI.HSSF.UserModel;
using System.IO;
using System.Collections.Generic;
using NPOI.SS.UserModel;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_MatCompare : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }

        /// <summary>
        /// 分页初始化
        /// </summary>
        /// <param name="where"></param>
        private void InitPager()
        {
            pager_org.TableName = "(select PR_ENGID,PR_CHILDENGNAME,PR_MAP,PUR_MARID,MNAME,GUIGE,CAIZHI,GB,PURCUNIT,PUR_LENGTH,PUR_WIDTH,sum(PUR_NUM) as PUR_NUM,sum(PUR_FZNUM) as PUR_FZNUM from TBPC_PCHSPLANRVW as a left join TBPC_PURCHASEPLAN as b on a.PR_SHEETNO=b.PUR_PCODE left join TBMA_MATERIAL as c on b.PUR_MARID=c.ID where " + StrWhere1() + " group by PR_ENGID,PR_CHILDENGNAME,PR_MAP,PUR_MARID,MNAME,GUIGE,CAIZHI,GB,PURCUNIT,PUR_LENGTH,PUR_WIDTH) as m left join (select PR_ENGID,PR_CHILDENGNAME,PR_MAP,PUR_MARID,MNAME,GUIGE,CAIZHI,GB,PURCUNIT,PUR_LENGTH,PUR_WIDTH,sum(PUR_NUM) as PUR_NUM,sum(PUR_FZNUM) as PUR_FZNUM from TBPC_PCHSPLANRVW as a left join TBPC_PURCHASEPLAN as b on a.PR_SHEETNO=b.PUR_PCODE left join TBMA_MATERIAL as c on b.PUR_MARID=c.ID where " + StrWhere2() + " group by PR_ENGID,PR_CHILDENGNAME,PR_MAP,PUR_MARID,MNAME,GUIGE,CAIZHI,GB,PURCUNIT,PUR_LENGTH,PUR_WIDTH) as n on m.PR_CHILDENGNAME=n.PR_CHILDENGNAME and m.PR_MAP=n.PR_MAP and m.PUR_MARID=n.PUR_MARID";
            pager_org.PrimaryKey = "";
            pager_org.ShowFields = "m.PR_ENGID as yuanengid,n.PR_ENGID as biduiengid,m.PR_MAP+'('+m.PR_CHILDENGNAME+')' as bujianmapname,m.PUR_MARID as marid,m.MNAME as marname,m.GUIGE as guige,m.CAIZHI as caizhi,m.GB as guobiao,m.PUR_LENGTH as length,m.PUR_WIDTH as width,m.PURCUNIT as unit,m.PUR_NUM as ynum,m.PUR_FZNUM as yfznum,n.PUR_NUM as bdnum,n.PUR_FZNUM as bdfznum";
            pager_org.OrderField = "m.PUR_MARID";
            pager_org.StrWhere = strwhere();
            pager_org.OrderType = 1;
            pager_org.PageSize = 100;
        }

        private string strwhere()
        {
            string sqltext = "m.PR_ENGID is not null and n.PR_ENGID is not null";
            if (drprelation.SelectedValue == "1")
            {
                sqltext += " and m.PUR_NUM>=n.PUR_NUM";
            }
            else if (drprelation.SelectedValue == "2")
            {
                sqltext += " and m.PUR_NUM<n.PUR_NUM";
            }
            return sqltext;
        }

        //原任务号
        private string StrWhere1()
        {
            string sql = "1=1";

            if (yuantsaid.Value.Trim() != "")
            {
                sql += " and PR_ENGID like '%" + yuantsaid.Value.Trim() + "%'";
            }

            return sql;
        }

        //比对任务号
        private string StrWhere2()
        {
            string sql = "1=1";

            if (biduitsaid.Value.Trim() != "")
            {
                sql += " and PR_ENGID like '%" + biduitsaid.Value.Trim() + "%'";
            }

            return sql;
        }

        /// <summary>
        /// 换页事件
        /// </summary>
        private void Pager_PageChanged(int pageNumber)
        {
            bindrpt();
        }

        private void bindrpt()
        {
            InitPager();
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);
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

        //任务号比对
        protected void btntsaidcompare_click(object sender, EventArgs e)
        {
            if (yuantsaid.Value.Trim() == "" || biduitsaid.Value.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入任务号！！！');", true);
                return;
            }
            else
            {
                UCPaging1.CurrentPage = 1;
                InitVar();
                bindrpt();
            }
        }

        //导出
        protected void btnexport_click(object sender, EventArgs e)
        {
            string sql = "select m.PR_ENGID as yuanengid,n.PR_ENGID as biduiengid,m.PR_MAP+'('+m.PR_CHILDENGNAME+')' as bujianmapname,m.PUR_MARID as marid,m.MNAME as marname,m.GUIGE as guige,m.CAIZHI as caizhi,m.GB as guobiao,m.PUR_LENGTH as length,m.PUR_WIDTH as width,m.PURCUNIT as unit,m.PUR_NUM as ynum,m.PUR_FZNUM as yfznum,n.PUR_NUM as bdnum,n.PUR_FZNUM as bdfznum from (select PR_ENGID,PR_CHILDENGNAME,PR_MAP,PUR_MARID,MNAME,GUIGE,CAIZHI,GB,PURCUNIT,PUR_LENGTH,PUR_WIDTH,sum(PUR_NUM) as PUR_NUM,sum(PUR_FZNUM) as PUR_FZNUM from TBPC_PCHSPLANRVW as a left join TBPC_PURCHASEPLAN as b on a.PR_SHEETNO=b.PUR_PCODE left join TBMA_MATERIAL as c on b.PUR_MARID=c.ID where " + StrWhere1() + " group by PR_ENGID,PR_CHILDENGNAME,PR_MAP,PUR_MARID,MNAME,GUIGE,CAIZHI,GB,PURCUNIT,PUR_LENGTH,PUR_WIDTH) as m left join (select PR_ENGID,PR_CHILDENGNAME,PR_MAP,PUR_MARID,MNAME,GUIGE,CAIZHI,GB,PURCUNIT,PUR_LENGTH,PUR_WIDTH,sum(PUR_NUM) as PUR_NUM,sum(PUR_FZNUM) as PUR_FZNUM from TBPC_PCHSPLANRVW as a left join TBPC_PURCHASEPLAN as b on a.PR_SHEETNO=b.PUR_PCODE left join TBMA_MATERIAL as c on b.PUR_MARID=c.ID where " + StrWhere2() + " group by PR_ENGID,PR_CHILDENGNAME,PR_MAP,PUR_MARID,MNAME,GUIGE,CAIZHI,GB,PURCUNIT,PUR_LENGTH,PUR_WIDTH) as n on m.PR_CHILDENGNAME=n.PR_CHILDENGNAME and m.PR_MAP=n.PR_MAP and m.PUR_MARID=n.PUR_MARID where " + strwhere();
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            string filename = "物料比对导出.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("物料比对导出模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string str = dt.Rows[i][j].ToString();
                        row.CreateCell(j + 1).SetCellValue(str);
                    }

                }
                for (int r = 0; r <= dt.Columns.Count; r++)
                {
                    sheet1.AutoSizeColumn(r);
                }
                sheet1.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
 
        }
    }
}
