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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_KaoHe_JXGZFinal : System.Web.UI.Page
    {
        string year = "";
        string month = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            year = (DateTime.Now.Year).ToString();
            month = (DateTime.Now.Month).ToString();

            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                BindData();
                this.InitPage();
                UCPaging1.CurrentPage = 1;

                InitVar();
                bindGrid();

            }
            CaculateBzAverage();
            InitVar();
        }


        //计算基数
        private string CaculateBzAverage()
        {
            string js = "0";

            string year = dplYear.SelectedValue;
            string month = dplMoth.SelectedValue;


            string sql = "select * from TBDS_BZAVERAGE where Year='" + year + "' and Month='" + month + "' and State='4'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                string money = dt.Rows[0]["MONEY"].ToString();
                js = (300 * CommonFun.ComTryDouble(money) / 3500).ToString("0.00");
              //  lblJS.Text = "300*" + money + "/3500=" + js;
                lblJS.Text = js;
                hidJS.Value = js;
            }

            return js;
        }

        //绑定基本信息
        private void BindData()
        {

            string stId = Session["UserId"].ToString();
            System.Data.DataTable dt = DBCallCommon.GetPermeision(5, stId);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
        }
        /// <summary>
        /// 绑定年月
        /// </summary>
        /// <param name="dpl_Year"></param>
        /// <param name="dpl_Month"></param>
        private void BindYearMoth(DropDownList dpl_Year, DropDownList dpl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            //dpl_Year.SelectedIndex = 0;
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
            //dpl_Month.SelectedIndex = 0;
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            txtname.Text = "";
            //显示当月
            dplYear.ClearSelection();
            foreach (ListItem li in dplYear.Items)
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }

            dplMoth.ClearSelection();
            string month = (DateTime.Now.Month - 1).ToString();
            if (DateTime.Now.Month < 10 || DateTime.Now.Month == 10)
            {
                month = "0" + month;
            }
            foreach (ListItem li in dplMoth.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
        }


        #region
        PagerQueryParam pager_org = new PagerQueryParam();
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager("1=1");
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }

        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager(string where)
        {
            pager_org.TableName = "(select Year, Month, State, b.DEP_ID, Zonghe,b.Id, Score, DepartScore, GangWeiXiShu, Money, PosName, b.ST_ID, b.ST_NAME,b.DEP_NAME,ST_WORKNO,ST_SEQUEN from TBDS_KaoHe_JXList as a left join dbo.TBDS_KaoHe_JXDetail as b on a.Context=b.Context  left join TBDS_DEPINFO as c on a.DepId=c.DEP_CODE left join TBDS_STAFFINFO as d on b.ST_ID=d.ST_ID where a.State='2')e";
            pager_org.PrimaryKey = "Id";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "DEP_ID,GangWeiXiShu";
            pager_org.StrWhere = where;
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 500;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }


        private string strWhere()
        {
            string sqlwhere = " 1=1";

            if (dplYear.SelectedIndex != 0)
            {
                sqlwhere += " and Year='" + dplYear.SelectedValue + "' ";
            }
            if (dplMoth.SelectedIndex != 0)
            {
                sqlwhere += " and Month='" + dplMoth.SelectedValue + "' ";
            }

            if (txtname.Text != "")
            {
                sqlwhere += " and (ST_NAME like '%" + txtname.Text.ToString().Trim() + "%' or ST_WORKNO like '%" + txtname.Text.ToString().Trim() + "%') ";
            }
            if (ddl_Depart.SelectedValue != "00")
            {
                sqlwhere += " and DEP_ID ='" + ddl_Depart.SelectedValue + "'";
            }
            return sqlwhere;
        }

        private void bindGrid()
        {


            InitPager(strWhere());

            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }

            string sqlText = "select sum(cast(GangWeiXiShu as float)),sum(cast(Money as float)) from (select Year, Month, State, DepId, Zonghe,b.Id, Score, DepartScore, GangWeiXiShu, Money, PosName, b.ST_ID, b.ST_NAME,b.DEP_NAME,ST_WORKNO,ST_SEQUEN,b.DEP_ID from TBDS_KaoHe_JXList as a left join dbo.TBDS_KaoHe_JXDetail as b on a.Context=b.Context  left join TBDS_STAFFINFO as d on b.ST_ID=d.ST_ID where a.State='2')e  where  " + strWhere();
            dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                lblXiShuTotal.Text = dt.Rows[0][0].ToString();
                lblGZTotal.Text = dt.Rows[0][1].ToString();
            }
        }
        #endregion

        protected void dplMoth_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }



        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDaochu_OnClick(object sender, EventArgs e)
        {

            string sqltext = "";;

            sqltext += "select * from (select Year, Month, State, b.DEP_ID, Zonghe,b.Id, Score, DepartScore, GangWeiXiShu, Money, PosName, b.ST_ID, b.ST_NAME,b.DEP_NAME,ST_WORKNO,ST_SEQUEN from TBDS_KaoHe_JXList as a left join dbo.TBDS_KaoHe_JXDetail as b on a.Context=b.Context  left join TBDS_DEPINFO as c on a.DepId=c.DEP_CODE left join TBDS_STAFFINFO as d on b.ST_ID=d.ST_ID where a.State='2')e where " + strWhere();
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }




        private void ExportDataItem(System.Data.DataTable objdt)
        {
            string filename = "人员月度绩效工资" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("人员月度绩效工资.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));
                    row.CreateCell(1).SetCellValue("" + objdt.Rows[i]["Year"].ToString());
                    row.CreateCell(2).SetCellValue("" + objdt.Rows[i]["Month"].ToString());
                    row.CreateCell(3).SetCellValue("" + objdt.Rows[i]["ST_NAME"].ToString());
                    row.CreateCell(4).SetCellValue("" + objdt.Rows[i]["DEP_NAME"].ToString());
                    row.CreateCell(5).SetCellValue("" + objdt.Rows[i]["PosName"].ToString());
                    row.CreateCell(6).SetCellValue("" + objdt.Rows[i]["ST_WORKNO"].ToString());
                    row.CreateCell(7).SetCellValue("" + objdt.Rows[i]["ST_SEQUEN"].ToString());
                    row.CreateCell(8).SetCellValue("" + objdt.Rows[i]["DepartScore"].ToString());
                    row.CreateCell(9).SetCellValue("" + objdt.Rows[i]["GangWeiXiShu"].ToString());
                    row.CreateCell(10).SetCellValue("" + objdt.Rows[i]["Score"].ToString());
                    row.CreateCell(11).SetCellValue("" + objdt.Rows[i]["Money"].ToString());
                }

                for (int i = 0; i <= objdt.Columns.Count; i++)
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
