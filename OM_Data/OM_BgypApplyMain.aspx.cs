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
using System.Collections.Generic;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_BgypApplyMain : BasicPage
    {
        List<string> codeList = new List<string>();
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                GetBoundData();
                ddlbumen();
                count_all();
            }
            CheckUser(ControlFinder);
        }

        private void count_all()
        {
            string sql_count_all = "select SUM(GET_MONEY) from View_TBOM_BGYPAPPLY where WLBM not like '3-%' and  year='" + DateTime.Now.Year.ToString() + "' and month='" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "' and ST_DEPID='" + Session["UserDeptID"].ToString() + "'";
            DataTable dt_count_all = DBCallCommon.GetDTUsingSqlText(sql_count_all);
            lab_count.Text = dt_count_all.Rows[0][0].ToString();
            string sql_EDU = "select MONTH_MAX from TBOM_BGYP_Month_max where DEP_CODE='" + Session["UserDeptID"].ToString() + "' and type='0'";
            DataTable dt_EDU = DBCallCommon.GetDTUsingSqlText(sql_EDU);
            if (dt_EDU.Rows.Count > 0)
            {
                lblEDU.Text = dt_EDU.Rows[0][0].ToString();
            }
            //计算总计
            string sqlZong = "select sum(WLNUM),sum(WLJE),SUM(cast(WLSLS as int)),SUM(GET_MONEY) from View_TBOM_BGYPAPPLY where  WLBM not like '3-%' and   " + GetWhere();
            dt_EDU = DBCallCommon.GetDTUsingSqlText(sqlZong);
            if (dt_EDU.Rows.Count > 0)
            {
                lblAppNum.Text = dt_EDU.Rows[0][0].ToString();
                lblAppMoney.Text = dt_EDU.Rows[0][1].ToString();
                lblSlsNum.Text = dt_EDU.Rows[0][2].ToString();
                lblSlsMoney.Text = dt_EDU.Rows[0][3].ToString();
            }
        }

        private void ddlbumen()
        {
            string Stid = Session["UserId"].ToString();
            System.Data.DataTable dt = DBCallCommon.GetPermeision(4, Stid);
            ddl_bumen.DataSource = dt;
            ddl_bumen.DataTextField = "DEP_NAME";
            ddl_bumen.DataValueField = "DEP_CODE";
            ddl_bumen.DataBind();
        }
        private string GetWhere()
        {
            string strWhere = "";
            string depid = Session["UserDeptID"].ToString();
            string userid = Session["UserID"].ToString();
            string ddl_value = ddl_bumen.SelectedValue.ToString();

            strWhere = " WLNAME like '%" + txtName.Text.Trim() + "%' and WLMODEL like '%" + txtModel.Text.Trim() + "%' ";

            if (ddl_bumen.SelectedValue != "00")
            {
                strWhere += " and ST_DEPID LIKE '%" + ddl_value + "%'";
            }
            if (txt_starttime.Text.ToString() != "" && txt_endtime.Text.ToString() != "")
            {
                strWhere += " and (DATE>'" + txt_starttime.Text.ToString() + "' or DATE='" + txt_starttime.Text.ToString() + "') and (DATE<'" + txt_endtime.Text.ToString() + "' or DATE='" + txt_endtime.Text.ToString() + "')";
            }
            if (rblState.SelectedValue != "")
            {
                if (rblState.SelectedValue != "3")
                {
                    strWhere += " AND REVIEWSTATE LIKE '%" + rblState.SelectedValue.ToString() + "%' ";
                }
                else
                {
                    strWhere += " and ((REVIEWSTATE='0' and REVIEWID='" + userid + "') or (REVIEWSTATE='1' and APPLYID='" + userid + "') or (REVIEWSTATE='2' and (WLSLS is null or WLSLS='') and '行政专员' in (" + Session["UserGroup"].ToString() + ")))";
                }

            }
            if (ckbChange.Checked == true)
                strWhere += " AND IsChange ='1'";
            return strWhere;
        }
        protected void ddlbumen_click(object sender, EventArgs e)
        {

            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
            count_all();
        }
        protected void ddlstate_click(object sender, EventArgs e)
        {

            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
            count_all();
        }
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
            count_all();
        }
        protected void ckbChange_CheckedChanged(object sender, EventArgs e)
        {

            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
            count_all();
        }

        protected void btn_MONEY_MAX_click(object sender, EventArgs e)
        {
            Response.Redirect("OM_Bgyp_Real.aspx");
        }

        protected void btnReset_OnClick(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtModel.Text = "";
            rblState.SelectedValue = "";
            ddl_bumen.SelectedValue = "";
            txt_starttime.Text = "";
            txt_endtime.Text = "";
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
            count_all();
        }

        //protected void btnAdd_onclick(object sender, EventArgs e)
        //{
        //    string dep_id = Session["UserDeptID"].ToString();
        //    string dt_now = DateTime.Now.ToString("yyyy-MM-dd");
        //    string dt_first = dt_now.Substring(0, 8);
        //    dt_first += "-01";
        //    string sql_sum_real = "select SUM(GET_MONEY) from View_TBOM_BGYPAPPLY where ST_DEPID='" + dep_id + "' and ((DATE>'" + dt_first + "' or DATE='" + dt_first + "') and (DATE<'" + dt_now + "' or DATE='" + dt_now + "'))";
        //    DataTable dt_sum_real = DBCallCommon.GetDTUsingSqlText(sql_sum_real);
        //    double count_sum_real = dt_sum_real.Rows[0][0].ToString().Trim() == "" ? 0 : Convert.ToDouble(dt_sum_real.Rows[0][0].ToString().Trim());

        //    string sql_sum_max = "select MONTH_MAX from TBOM_BGYP_Month_max where DEP_CODE='" + dep_id + "'";
        //    DataTable dt_sum_max = DBCallCommon.GetDTUsingSqlText(sql_sum_max);
        //    if (dt_sum_max.Rows.Count > 0)
        //    {
        //        double count_sum_max = dt_sum_max.Rows[0][0].ToString().Trim() == "" ? 0 : Convert.ToDouble(dt_sum_max.Rows[0][0].ToString().Trim());
        //        if (count_sum_real >= count_sum_max)
        //        {
        //            Response.Write("<script>alert('请注意，已超最高额！！!')</script>");
        //        }
        //    }

        //    Response.Redirect("OM_BgypApply.aspx?action=add");
        //}
        #region 分页
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                string state = "";
                string applyid = "";
                string reviewid = "";
                string code = ((Label)e.Item.FindControl("CODE")).Text;
                string sql = "select distinct(CODE) as code,REVIEWSTATE,APPLYID,REVIEWID from View_TBOM_BGYPAPPLY where CODE='" + code + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    state = dt.Rows[0]["REVIEWSTATE"].ToString();
                    applyid = dt.Rows[0]["APPLYID"].ToString();
                    reviewid = dt.Rows[0]["REVIEWID"].ToString();
                    if (rblState.SelectedValue == "3")
                    {
                        if (Session["UserID"].ToString() == applyid)
                        {
                            if (state == "0")
                            {
                                ((Label)e.Item.FindControl("REVIEWSTATE")).Text = "修改";
                                //   ((Label)e.Item.FindControl("CODE")).BackColor = System.Drawing.Color.Red;
                                ((HyperLink)e.Item.FindControl("hlTask1")).NavigateUrl = "OM_BgypApply.aspx?action=view&id=" + code + "";
                                ((HyperLink)e.Item.FindControl("hlTask1")).Visible = true;

                            }
                            if (state == "1")
                            {
                                ((Label)e.Item.FindControl("REVIEWSTATE")).Text = "修改";
                                //((Label)e.Item.FindControl("CODE")).BackColor = System.Drawing.Color.Gray;
                                ((HyperLink)e.Item.FindControl("hlTask1")).NavigateUrl = "OM_BgypApply.aspx?action=mod&id=" + code + "";
                                ((HyperLink)e.Item.FindControl("hlTask1")).Visible = true;

                            }
                        }


                        if (Session["position"].ToString() == "0207")
                        {
                            if (state == "2")
                            {
                                // ((Label)e.Item.FindControl("CODE")).BackColor = System.Drawing.Color.Green;
                                ((HyperLink)e.Item.FindControl("hlTask1")).NavigateUrl = "OM_BgypApply.aspx?action=view&id=" + code + "";
                                if (((Label)e.Item.FindControl("WLSLS")).Text.ToString() == "")
                                {
                                    ((HyperLink)e.Item.FindControl("link_bh")).Visible = true;
                                }
                            }
                        }
                        if (Session["UserID"].ToString() == reviewid)
                        {
                            if (state == "0")
                            {
                                ((Label)e.Item.FindControl("REVIEWSTATE")).Text = "审核";
                                //   ((Label)e.Item.FindControl("CODE")).BackColor = System.Drawing.Color.Red;
                                ((HyperLink)e.Item.FindControl("hlTask1")).NavigateUrl = "OM_BgypApply.aspx?action=verify&id=" + code + "";
                                ((HyperLink)e.Item.FindControl("hlTask1")).Visible = true;

                            }

                        }

                    }


                }


                if (codeList.Contains(code))
                {
                    ((Label)e.Item.FindControl("CODE")).Text = "";
                }
                else
                {
                    codeList.Add(code);
                }
            }
        }
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = "View_TBOM_BGYPAPPLY";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "ID*1 as ID,WLCODE,WLBM,WLNAME,WLMODEL,WLUNIT,WLPRICE,WLNUM,WLJE,num,WLNOTE,APPLY,APPLYID,REVIEW,REVIEWID,CODE,DATE,DEPNAME,REVIEWSTATE,WLSLS,GET_MONEY,IsChange";
            pager.OrderField = "";
            pager.StrWhere = GetWhere();
            pager.OrderType = 1;
            pager.PageSize = 15;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
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
        private void ReGetBoundData()
        {
            InitPager();
            GetBoundData();
        }
        #endregion




        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDaochu_OnClick(object sender, EventArgs e)
        {

            string sqltext = ""; ;

            sqltext += "select * from View_TBOM_BGYPAPPLY where " + GetWhere() + " order by DATE desc";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }




        private void ExportDataItem(System.Data.DataTable objdt)
        {
            string filename = "办公用品使用申请" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("办公用品使用申请.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));
                    row.CreateCell(1).SetCellValue("" + objdt.Rows[i]["CODE"].ToString());
                    row.CreateCell(2).SetCellValue("" + objdt.Rows[i]["WLBM"].ToString());
                    row.CreateCell(3).SetCellValue("" + objdt.Rows[i]["WLNAME"].ToString());
                    row.CreateCell(4).SetCellValue("" + objdt.Rows[i]["WLMODEL"].ToString());
                    row.CreateCell(5).SetCellValue("" + objdt.Rows[i]["WLPRICE"].ToString());
                    row.CreateCell(6).SetCellValue("" + objdt.Rows[i]["WLNUM"].ToString());
                    row.CreateCell(7).SetCellValue("" + objdt.Rows[i]["WLJE"].ToString());
                    row.CreateCell(8).SetCellValue("" + objdt.Rows[i]["num"].ToString());
                    row.CreateCell(9).SetCellValue("" + objdt.Rows[i]["APPLY"].ToString());
                    row.CreateCell(10).SetCellValue("" + objdt.Rows[i]["REVIEW"].ToString());
                    row.CreateCell(11).SetCellValue("" + objdt.Rows[i]["DEPNAME"].ToString());
                    row.CreateCell(12).SetCellValue("" + objdt.Rows[i]["DATE"].ToString());
                    row.CreateCell(13).SetCellValue("" + objdt.Rows[i]["WLSLS"].ToString());
                    row.CreateCell(14).SetCellValue("" + objdt.Rows[i]["GET_MONEY"].ToString());
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
