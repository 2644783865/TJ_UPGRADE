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

namespace ZCZJ_DPF.Basic_Data
{
    public partial class PassWordUpdateRecord : BasicPage
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitVar();
                bindrpt();
                danyuangehebing("td1", "td2");
            }
            InitVar();
            CheckUser(ControlFinder);
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
            pager_org.TableName = "(select Id, TBDS_DEPINFO.DEP_NAME as depName, stName,editTime,oldPassword,newPassword from TBDS_EDITPASSWORDRECORD left join TBDS_STAFFINFO on(TBDS_EDITPASSWORDRECORD.stName=TBDS_STAFFINFO.ST_NAME) left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t";
            pager_org.PrimaryKey = "Id";
            pager_org.ShowFields = "Id,depName,stName,editTime,oldPassword,newPassword";
            pager_org.OrderField = "editTime";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;
            pager_org.PageSize = PageSize(); ;
        }

        private int PageSize()
        {
            int pagesize = Convert.ToInt32(ddlRowCount.SelectedItem.Text);
            return pagesize;
        }

        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string sql = "1=1";
            if (txtstName.Text.Trim() != "")
            {
                sql += " and stName like '%" + txtstName.Text.Trim() + "%'";
            }
            if (txtQsrq.Text.Trim() != "")
            {
                sql += " and editTime >= '" + txtQsrq.Text.Trim() + "'";
            }
            if (txtJzrq.Text.Trim() != "")
            {
                sql += " and editTime <= '" + txtJzrq.Text.Trim() + "'";
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

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }
        private void bindrpt()
        {
            InitPager();
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptRecord, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            for (int j = 0; j < rptRecord.Items.Count; j++)
            {
                Label s = (Label)rptRecord.Items[j].FindControl("lblXuHao");
                s.Text = (j + 1 + (pager_org.PageIndex - 1) * UCPaging1.PageSize).ToString();
            }
        }


        protected void btn_Search_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
            danyuangehebing("td1", "td2");
        }

        protected void btn_Reset_Click(object sender, EventArgs e)
        {
            txtstName.Text = "";
            txtQsrq.Text = "";
            txtJzrq.Text = "";
        }

        //合并单元格
        private void danyuangehebing(string tdIdName1, string tdIdName2)
        {
            for (int i = rptRecord.Items.Count - 1; i > 0; i--)
            {
                danyuangehebingSet(tdIdName1, tdIdName2, i);
            }
        }
        private void danyuangehebingSet(string tdIdName1, string tdIdName2, int i)
        {
            HtmlTableCell cellPrev = rptRecord.Items[i - 1].FindControl(tdIdName1) as HtmlTableCell;
            HtmlTableCell cell = rptRecord.Items[i].FindControl(tdIdName1) as HtmlTableCell;
            cell.RowSpan = (cell.RowSpan == -1) ? 1 : cell.RowSpan;
            cellPrev.RowSpan = (cellPrev.RowSpan == -1) ? 1 : cellPrev.RowSpan;
            if (cell.InnerText == cellPrev.InnerText)
            {
                cell.Visible = false;
                cellPrev.RowSpan += cell.RowSpan;
                //关键代码，再判断执行第2列的合并单元格方法
                if (tdIdName2 != "")
                    danyuangehebingSet(tdIdName2, "", i);
            }
        }
    }
}
