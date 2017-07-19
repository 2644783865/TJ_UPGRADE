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
using ZCZJ_DPF;
using System.Data.SqlClient;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_SALARYBASEDATAJLCK : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindrpt();
            }
            this.InitVar();
        }

        #region 分页
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            tbxName.Text = "";
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            dptbind();
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
            pager_org.TableName = "(select a.ID as ID,a.ST_ID as ST_ID,a.PERSON_GH as PERSON_GH,b.ST_NAME as ST_NAME,b.ST_DEPID as ST_DEPID,b.ST_SEQUEN,c.DEP_NAME as DEP_NAME,a.BASEDATANEW as BASEDATANEW,a.BASEDATAOLD as BASEDATAOLD,a.CZRST_NAME as CZRST_NAME,a.CZTIME as CZTIME,a.NOTE as NOTE,a.TYPE as TYPE from OM_SALARYBASEDATARECORD as a left join TBDS_STAFFINFO as b on a.ST_ID=b.ST_ID left join TBDS_DEPINFO as c on b.ST_DEPID = c.DEP_CODE)t";
            pager_org.PrimaryKey = "ID";
            pager_org.ShowFields = "*";
            pager_org.OrderField = "PERSON_GH,CZTIME";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 50;
        }
        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string stid = "";
            string datatype = "";
            try
            {
                stid = Request.QueryString["id"].ToString().Trim();
                datatype = Request.QueryString["datatype"].ToString().Trim();
            }
            catch
            {
                stid = "";
            }
            string sql = "1=1";
            if (stid != "" && datatype!="")
            {
                sql += " and ST_ID='" + stid + "' and TYPE='" + datatype + "'";
            }
            else
            {
                if (tbxName.Text.Trim() != "")
                {
                    sql += " and ST_NAME like '%" + tbxName.Text.Trim() + "%'";
                }
                if (txtStartTime.Text != "")
                {
                    sql += " and substring(CZTIME,1,10)>='" + txtStartTime.Text.ToString().Trim() + "'";
                }
                if (txtEndTime.Text.Trim() != "")
                {
                    sql += " and substring(CZTIME,1,10)<='" + txtEndTime.Text.ToString().Trim() + "'";
                }
                if (ddldpt.SelectedIndex != 0)
                {
                    sql += " and ST_DEPID='" + ddldpt.SelectedValue.Trim() + "'";
                }
                if (txtgwxl.Text.Trim() != "")
                {
                    sql += " and ST_SEQUEN like '%" + txtgwxl.Text.Trim() + "%'";
                }
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
            CommonFun.Paging(dt, rptGDGZrecord, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }
        #endregion

        private void dptbind()
        {
            string sql = "";
            sql = "select distinct DEP_NAME,DEP_CODE from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'order by DEP_CODE";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddldpt.DataSource = dt;
            ddldpt.DataTextField = "DEP_NAME";
            ddldpt.DataValueField = "DEP_CODE";
            ddldpt.DataBind();
            ddldpt.Items.Insert(0, new ListItem("全部", "%"));
        }

        protected void ddl_dpt_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }


        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        #region 批量导出

        protected void btn_plexport_OnClick(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext = "select PERSON_GH,ST_NAME,DEP_NAME,BASEDATANEW,BASEDATAOLD,CZRST_NAME,CZTIME,NOTE from (select a.ID as ID,a.ST_ID as ST_ID,a.PERSON_GH as PERSON_GH,b.ST_NAME as ST_NAME,b.ST_DEPID as ST_DEPID,b.ST_SEQUEN,c.DEP_NAME as DEP_NAME,a.BASEDATANEW as BASEDATANEW,a.BASEDATAOLD as BASEDATAOLD,a.CZRST_NAME as CZRST_NAME,a.CZTIME as CZTIME,a.NOTE as NOTE from OM_SALARYBASEDATARECORD as a left join TBDS_STAFFINFO as b on a.ST_ID=b.ST_ID left join TBDS_DEPINFO as c on b.ST_DEPID = c.DEP_CODE)t where " + StrWhere() + " order by PERSON_GH,CZTIME";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string filename = "薪酬基数修改记录" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("薪酬基数修改记录.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
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
    }
}
