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
using System.Collections.Generic;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_YFInvoice_Managemnt : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitVar();
                this.bindGrid();
            }
            if (IsPostBack)
            {
                this.InitVar();
            }
            CheckUser(ControlFinder);
        }

        #region 分页
        PagerQueryParam pager = new PagerQueryParam();
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager.TableName = "View_YFInv";
            pager.PrimaryKey = "YFGI_CODE";
            pager.ShowFields = "YFGI_CODE,YFGI_GYSNAME,YFGI_FPNUM,cast(YFGI_MONEY as decimal(12,2)) as YFGI_MONEY,(isnull(YFGI_HSMONEY,0)-isnull(cast(YFGI_MONEY as decimal(12,2)),0)) as YFGI_SE,YFGI_HSMONEY,YFGI_PZH,YFGI_JZNAME,YFGI_ZDNAME,YFGI_DATE,YFGI_STATE, YFGI_GJFLAG,YFGJ_HSSTATE";
            pager.OrderField = "YFGI_CODE";
            pager.StrWhere = this.GetSqlText();
            pager.OrderType = 1;//按时间升序序排列
            pager.PageSize = 20;

            GetTotalAmount(pager.StrWhere);
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, grvInv, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns></returns>
        private string GetSqlText()
        {

            string sqltext = string.Empty;
            sqltext = "YFGJ_CODE in(select min(YFGJ_CODE) from View_YFInv group by YFGI_CODE) ";

            //供应商
            if (tb_supply.Text != "")
            {
                sqltext += "AND YFGI_GYSNAME like '%" + tb_supply.Text + "%'";
            }
            //审核
            if (rblSFSH.SelectedItem.Text != "全部" && sqltext != string.Empty)
            {
                sqltext += " AND YFGI_STATE='" + rblSFSH.SelectedValue + "'";
            }
            else if (rblSFSH.SelectedItem.Text != "全部" && sqltext == string.Empty)
            {
                sqltext += "YFGI_STATE='" + rblSFSH.SelectedValue + "'";
            }
            //勾稽
            if (rblSFGJ.SelectedItem.Text != "全部" && sqltext != string.Empty)
            {
                sqltext += " AND YFGI_GJFLAG='" + rblSFGJ.SelectedValue + "'";
            }
            else if (rblSFGJ.SelectedItem.Text != "全部" && sqltext == string.Empty)
            {
                sqltext += "YFGI_GJFLAG='" + rblSFGJ.SelectedValue + "'";
            }
            //发票号码
            if (txtfpCode.Text != "" && sqltext != string.Empty)
            {
                sqltext += " AND YFGI_FPNUM like '%" + txtfpCode.Text + "%'";
            }
            else if (txtfpCode.Text != "" && sqltext == string.Empty)
            {
                sqltext += "YFGI_FPNUM like '%" + txtfpCode.Text + "%'";
            }

            //凭证号
            if (txtpzh.Text != "" && sqltext != string.Empty)
            {
                sqltext += " AND YFGI_PZH like '%" + txtpzh.Text + "%'";
            }
            else if (txtpzh.Text != "" && sqltext == string.Empty)
            {
                sqltext += "YFGI_PZH like '%" + txtpzh.Text + "%'";
            }

            //日期
            if (sqltext != "")
            {
                if ((txtStartYearMonth.Text.Trim() == "") && (txtEndYearMonth.Text.Trim() == ""))
                {
                    //如果不选时间，默认为本期。
                    sqltext += " AND " + " YFGI_DATE like '" + System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString().PadLeft(2, '0') + "%'";//全部
                }
                if ((txtStartYearMonth.Text.Trim() == "") && (txtEndYearMonth.Text.Trim() != ""))
                {
                    sqltext += " AND " + " YFGI_DATE <= '" + txtEndYearMonth.Text.Trim() + "'";//
                }
                if ((txtStartYearMonth.Text.Trim() != "") && (txtEndYearMonth.Text.Trim() == ""))
                {
                    sqltext += " AND " + " YFGI_DATE >= '" + txtStartYearMonth.Text.Trim() + "'";//
                }
                if ((txtStartYearMonth.Text.Trim() != "") && (txtEndYearMonth.Text.Trim() != ""))
                {
                    sqltext += " AND " + " YFGI_DATE between '" + txtStartYearMonth.Text.Trim() + "' and '" + txtEndYearMonth.Text.Trim() + "'";
                }
            }
            else
            {
                if ((txtStartYearMonth.Text.Trim() == "") && (txtEndYearMonth.Text.Trim() == ""))
                {
                    //如果不选时间，默认为本期。
                    sqltext += " YFGI_DATE like '" + System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString().PadLeft(2, '0') + "%'";//全部
                }
                if ((txtStartYearMonth.Text.Trim() == "") && (txtEndYearMonth.Text.Trim() != ""))
                {
                    sqltext += " YFGI_DATE <= '" + txtEndYearMonth.Text.Trim() + "'";//
                }
                if ((txtStartYearMonth.Text.Trim() != "") && (txtEndYearMonth.Text.Trim() == ""))
                {
                    sqltext += " YFGI_DATE >= '" + txtStartYearMonth.Text.Trim() + "'";//
                }
                if ((txtStartYearMonth.Text.Trim() != "") && (txtEndYearMonth.Text.Trim() != ""))
                {
                    sqltext += " YFGI_DATE between '" + txtStartYearMonth.Text.Trim() + "' and '" + txtEndYearMonth.Text.Trim() + "'";
                }
            }

            return sqltext;
        }
        #endregion

        private void GetTotalAmount(string strWhere)
        {
            string sqltext = "select * from View_YFInv where " + strWhere;
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                string sql = "select isnull(CAST(round(sum(YFGI_MONEY),2) AS FLOAT),0) as TotalJE,((isnull(CAST(round(sum(YFGI_HSMONEY),2) AS FLOAT),0))-(isnull(CAST(round(sum(YFGI_MONEY),2) AS FLOAT),0))) as TotalSE,isnull(CAST(round(sum(YFGI_HSMONEY),2) AS FLOAT),0) as TotalHSJE from View_yfInv where " + strWhere;

                DataTable dtsum = DBCallCommon.GetDTUsingSqlText(sql);
                DataRow row = dtsum.Rows[0];
                if (dtsum.Rows.Count > 0)
                {
                    hfdTotalJE.Value = row["TotalJE"].ToString();

                    hfdTotalSE.Value = row["TotalSE"].ToString();

                    hfdTotalHSJE.Value = row["TotalHSJE"].ToString();
                }
            }
            else
            {
                hfdTotalJE.Value = "0";

                hfdTotalSE.Value = "0";

                hfdTotalHSJE.Value = "0";
            }
        }

        protected void grvInv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "合计:";
                e.Row.Cells[4].Text = (Convert.ToDouble(hfdTotalJE.Value)).ToString();
                e.Row.Cells[5].Text = (Convert.ToDouble(hfdTotalSE.Value)).ToString();
                e.Row.Cells[6].Text = (Convert.ToDouble(hfdTotalHSJE.Value)).ToString();
            }
        }
        //是否审核
        protected void rblSFSH_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }

        //是否钩稽
        protected void rblSFGJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }
        //单击查询按钮
        protected void btnCx_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }

        //到结算单
        protected void btnToRKD_Click(object sender, EventArgs e)
        {
            Response.Redirect("FM_YFFromJSD_ToInv.aspx");
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            List<string> sql = new List<string>();
            string sqlfptotal = "";
            string sqlfprelation = "";
            string sqltext = "";
            string strID = "";
            int i = 0;
            foreach (GridViewRow grsc in grvInv.Rows)
            {
                CheckBox cbx = (CheckBox)grsc.FindControl("checkbox");
                if (cbx.Checked)
                {
                    //查找该CheckBox所对应纪录的id号,在labID中
                    strID += "'" + grsc.Cells[1].Text.Trim() + "',";
                    i++;
                }
            }
            if (i >= 1)
            {
                //去掉最后的一个逗号
                strID = strID.Substring(0, strID.Length - 1);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('请选择要删除的发票!')</script>");
                return;
            }
            sqltext = "select YFGJ_JHGZH,YFGJ_YEAR,YFGJ_MONTH from TBFM_YFFPDETAIL where YFGJ_FPID in (" + strID + ")";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string sqlifhs = "select * from TBFM_YFHS where YFHS_YEAR='" + dt.Rows[j]["YFGJ_YEAR"].ToString() + "' and YFHS_MONTH='" + dt.Rows[j]["YFGJ_MONTH"].ToString() + "'";
                    DataTable dtifhs = DBCallCommon.GetDTUsingSqlText(sqlifhs);
                    if (dtifhs.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('已经核算过的发票不能删除!')</script>");
                        return;
                    }
                    string sqldel = "update PM_CPFYJSD set JS_GJSTATE='0' where JS_JHGZH='" + dt.Rows[j]["YFGJ_JHGZH"] + "'";
                    sql.Add(sqldel);
                }
            }
            sqlfptotal = "delete from TBFM_YFFPTOTAL where YFGI_CODE in (" + strID + ")";
            sql.Add(sqlfptotal);
            sqlfprelation = "delete from TBFM_YFFPDETAIL where YFGJ_FPID in (" + strID + ")";
            sql.Add(sqlfprelation);
            DBCallCommon.ExecuteTrans(sql);
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }
    }
}
