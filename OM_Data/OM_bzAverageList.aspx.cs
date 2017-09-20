using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_bzAverageList : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                databind();
                ControlVisible();
            }
            CheckUser(ControlFinder);
        }


        #region 初始化分页

        void Pager_PageChanged(int pageNumber)
        {
            databind();
        }

        private void databind()
        {
            pager.TableName = "TBDS_BZAVERAGE";
            pager.PrimaryKey = "Id";
            pager.ShowFields = "*";
            pager.OrderField = "Id";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;
            pager.PageSize = 20;
            UCPaging1.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, rep_Kaohe, UCPaging1, NoDataPanel);
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

        private string strWhere()
        {
            string strWhere = "1=1 ";

            if (txtStart.Text.Trim() != "")
            {
                strWhere += " and ZDTIME>='" + txtStart.Text.Trim() + "'";
            }
            if (txtEnd.Text.Trim() != "")
            {
                strWhere += " and ZDTIME<='" + txtEnd.Text.Trim() + "'";
            }
            if (dplYear.SelectedIndex != 0)
            {
                strWhere += " and Year='" + dplYear.SelectedValue + "'";
            }
            if (dplMoth.SelectedIndex != 0)
            {
                strWhere += " and Month='" + dplMoth.SelectedValue + "'";
            }
            if (rblState.SelectedValue == "0")
            {
                strWhere += " and State='0'";
            }
            else if (rblState.SelectedValue == "1")
            {
                strWhere += " and (State ='1' or State='2' )";
            }
            else if (rblState.SelectedValue == "2")
            {
                strWhere += " and State ='4'";
            }
            else if (rblState.SelectedValue == "3")
            {
                strWhere += " and State ='3'";
            }
            else if (rblState.SelectedValue == "4")
            {
                strWhere += " and  ((State ='1' and SPRID='" + Session["UserId"].ToString() + "') or ( State='2' and SPRIDB='" + Session["UserId"].ToString() + "'))";
            }
            return strWhere;
        }

        #endregion

        protected void ddl_Year_SelectedIndexChanged(object sender, EventArgs e)
        {

            UCPaging1.CurrentPage = 1;
            databind();
            ControlVisible();
        }

        private void ControlVisible()
        {
            foreach (RepeaterItem item in rep_Kaohe.Items)
            {
                HyperLink hlkEdit = item.FindControl("HyperLink2") as HyperLink;
                HyperLink hlkAudit = item.FindControl("HyperLink3") as HyperLink;
                if (rblState.SelectedValue == "0" || rblState.SelectedValue == "3")
                {
                    hlkEdit.Visible = true;
                }
                else
                {
                    hlkEdit.Visible = false;
                }
                if (rblState.SelectedValue == "4")
                {
                    hlkAudit.Visible = true;
                }
                else
                {
                    hlkAudit.Visible = false;
                }
            }
        }



        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDaochu_OnClick(object sender, EventArgs e)
        {

            string sqltext = "";
            sqltext = "select Year,";
            sqltext += "Month,";
            sqltext += "DepartNM,";
            sqltext += "Score,";
            sqltext += "Note ";

            sqltext += " from TBDS_KaoheDeaprtMonth as a left join TBDS_KaoheDeaprtMonth_Detail as b on a.ConText=b.Context where " + strWhere();
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }



        private void ExportDataItem(System.Data.DataTable objdt)
        {
            string filename = "部门月度考核表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("部门月度考核表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));
                    row.CreateCell(1).SetCellValue("" + objdt.Rows[i]["Year"].ToString());
                    row.CreateCell(2).SetCellValue("" + objdt.Rows[i]["Month"].ToString());
                    row.CreateCell(3).SetCellValue("" + objdt.Rows[i]["DepartNM"].ToString());
                    row.CreateCell(4).SetCellValue("" + objdt.Rows[i]["Score"].ToString());
                    row.CreateCell(5).SetCellValue("" + objdt.Rows[i]["Note"].ToString());
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
