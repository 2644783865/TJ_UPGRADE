using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_Kaipiao_Task : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        List<string> str = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {

                this.GetBoundData();
            }
        }

        #region 分页
        protected void GetBoundData()
        {
            InitPager();
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
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

        private void InitPager()
        {
            pager.TableName = "(select b.conId,b.TaskId,b.Proj,b.Engname,b.Unit,a.KP_KPNUMBER,a.KP_KPDATE,a.KP_CODE,sum(cast(b.Money as float)) as ZongMoney,sum(cast(b.kpmoney as float)) as kpZongMoney from CM_KAIPIAO as a left join dbo.CM_KAIPIAO_DETAIL as b on a.KP_TaskID=b.cId where KP_SPSTATE='3' group by b.conId,b.TaskId,b.Proj,b.Engname,b.Unit,a.KP_KPNUMBER,a.KP_KPDATE,a.KP_CODE,a.KP_KPNUMBER)c";
            pager.PrimaryKey = "TaskId";
            pager.ShowFields = "*";
            pager.OrderField = "KP_KPDATE";
            pager.StrWhere = CreateConStr();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 20;
            UCPaging1.PageSize = pager.PageSize;
        }

        private string CreateConStr()
        {
            string sql = " 1=1 ";
            if (txtCode.Text != "")
            {
                sql += " and KP_CODE like '%" + txtCode.Text.Trim() + "%'";
            }
            if (txtTaskId.Text != "")
            {
                sql += " and TaskId like '%" + txtTaskId.Text + "%'";
            }
            return sql;
        }

        void Pager_PageChanged(int pageNumber)
        {

            GetBoundData();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            this.GetBoundData();
        }
        #endregion

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            string sql = "select * from (select a.KP_CODE,b.TaskId,b.conId,b.Proj,b.Engname,sum(cast(b.number as float)) as number,b.Unit,sum(cast(b.kpmoney as float)) as kpZongMoney,sum(cast(b.Money as float)) as ZongMoney,a.KP_KPNUMBER,a.KP_KPDATE from CM_KAIPIAO as a left join dbo.CM_KAIPIAO_DETAIL as b on a.KP_TaskID=b.cId where KP_SPSTATE='3' group by b.conId,b.TaskId,b.Proj,b.Engname,b.Unit,a.KP_KPNUMBER,a.KP_KPDATE,a.KP_CODE,a.KP_KPNUMBER)c where " + CreateConStr() + " order by KP_KPDATE desc";


            string filename = "开票明细按任务导出.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("开票明细按任务导出.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet = wk.GetSheetAt(0);

                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count == 0)
                {

                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
                    return;
                }
                ////填充数据

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet.CreateRow(i + 1);

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }

                sheet.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
    }
}
