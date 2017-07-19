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
using System.Text;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Finished_IN : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //2016.8.25修改
                string contract_task_view = Request["contract_task_view"];
                if (!string.IsNullOrEmpty(contract_task_view))
                {
                    ddl_query.SelectedValue = "3";
                    txtName.Text = contract_task_view.Trim();
                }
                this.ShenHe();
                InitVar();
                GetBoundData();
            }
            InitVar();
            CheckUser(ControlFinder);
            //if (Session["POSITION"].ToString() == "1205"||Session["POSITION"].ToString() == "1201")
            //{
            //    Label.Visible = true;
            //}
        }
        private void ShenHe()
        {
            int a = 0;//初始化
            int b = 0;//未审批
            int c = 0;//审批中
            int d = 0;//已驳回
            string sqltext = "select SPZT from TBMP_FINISHED_IN";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["SPZT"].ToString() == "0")
                {
                    a++;
                }
                if (dt.Rows[i]["SPZT"].ToString() == "1")
                {
                    b++;
                }
                if (dt.Rows[i]["SPZT"].ToString() == "2")
                {
                    c++;
                }
                if (dt.Rows[i]["SPZT"].ToString() == "4")
                {
                    d++;
                }
            }
            rbl_shenhe.Items.Clear();
            rbl_shenhe.Items.Add(new ListItem("全部", "5"));
            if (a != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("初始化" + "</label><label><font color=red>(" + a + ")</font>", "0"));
                rbl_shenhe.SelectedIndex = 1;
                btn_search1_click(null, null);

            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("初始化", "0"));
            }
            if (b != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("未审批" + "</label><label><font color=red>(" + b + ")</font>", "1"));
                rbl_shenhe.SelectedIndex = 2;
                btn_search1_click(null, null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("未审批", "1"));
            }
            if (c != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("审批中" + "</label><label><font color=red>(" + c + ")</font>", "2"));
                rbl_shenhe.SelectedIndex = 3;
                btn_search1_click(null, null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("审批中", "2"));
            }
            rbl_shenhe.Items.Add(new ListItem("已通过", "3"));
            if (d != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("已驳回" + "</label><label><font color=red>(" + d + ")</font>", "4"));
                rbl_shenhe.SelectedIndex = 5;
                btn_search1_click(null, null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("已驳回", "4"));
            }
            rbl_shenhe.SelectedIndex = 0;
        }
        private string GetWhere()
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("1=1");
            if (rbl_shenhe.SelectedValue.ToString() != "5")
            {
                strWhere.Append("and SPZT='" + rbl_shenhe.SelectedValue.ToString() + "' ");
            }
            if (txtName.Text.Trim() != "")
            {
                switch (ddl_query.SelectedValue)
                {
                    case "1":
                        strWhere.Append("and TFI_PROJ like '%" + txtName.Text.Trim() + "%'");
                        break;
                    case "2":
                        strWhere.Append("and TFI_CONTR like '%" + txtName.Text.Trim() + "%'");
                        break;
                    case "3":
                        strWhere.Append("and TSA_ID like '%" + txtName.Text.Trim() + "%'");
                        break;
                    case "4":
                        strWhere.Append("and TFI_MAP like '%" + txtName.Text.Trim() + "%'");
                        break;
                    case "5":
                        strWhere.Append("and TFI_ENGNAME like '%" + txtName.Text.Trim() + "%'");
                        break;
                    case "0":
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择查询类型！！！');", true);
                        break;
                }
            }
            return strWhere.ToString();
        }
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label spzt = e.Item.FindControl("SPZT") as Label;
                string TSA_ID = ((Label)e.Item.FindControl("TSA_ID")).Text;
                string docnum = ((Label)e.Item.FindControl("docnum")).Text;
                ((Label)e.Item.FindControl("PUR_DD")).ForeColor = System.Drawing.Color.Red;
                ((HyperLink)e.Item.FindControl("HyperLink_lookup")).NavigateUrl = "PM_Finished_look.aspx?action=view&docnum=" + docnum + "&id=" + TSA_ID + "";
            }
        }
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
        }
        public string get_spzt(string i)
        {
            string state = "";
            if (i == "0")
            {
                state = "初始化";
            }
            else if (i == "1")
            {
                state = "提交未审批";
            }
            else if (i == "2")
            {
                state = "审批中";
            }
            else if (i == "3")
            {
                state = "已通过";
            }
            else if (i == "4")
            {
                state = "已驳回";
            }
            return state;
        }
        protected void btn_search1_click(object sender, EventArgs e)
        {
            InitPager();
            UCPaging1.CurrentPage = 1;
            GetBoundData();
        }
        #region 分页
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = "(select a.*,SPSJ=case when Rank='1' then SEC_SJ when rank='0' then FIR_SJ when rank='2' then THI_SJ end from TBMP_FINISHED_IN as a left join TBMP_FINISHED_IN_Audit as b on a.TFI_DOCNUM = b.FIA_DOCNUM)t";
            pager.PrimaryKey = "";
            pager.ShowFields = "*";
            pager.OrderField = "TFI_DOCNUM";
            pager.StrWhere = GetWhere();
            pager.OrderType = 1;
            pager.PageSize = 10;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
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
        protected void btndaochu_OnClick(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext = "select a.*,SPSJ=case when Rank='1' then SEC_SJ when rank='0' then FIR_SJ when rank='2' then THI_SJ end,b.*,c.ST_NAME as SPR1 ,d.ST_NAME as SPR2 from TBMP_FINISHED_IN as a left join TBMP_FINISHED_IN_Audit as b on a.TFI_DOCNUM=b.FIA_DOCNUM left join TBDS_STAFFINFO as c on b.Fir_Per=c.ST_ID left join TBDS_STAFFINFO as d on b.Sec_Per=d.ST_ID where " + GetWhere() + "";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string filename = "成品入库管理" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("成品入库管理.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    IRow row = sheet0.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));
                    row.CreateCell(1).SetCellValue("" + dt.Rows[i]["TFI_DOCNUM"].ToString());
                    row.CreateCell(2).SetCellValue("" + dt.Rows[i]["TFI_PROJ"].ToString());
                    row.CreateCell(3).SetCellValue("" + dt.Rows[i]["TSA_ID"].ToString());
                    row.CreateCell(4).SetCellValue("" + dt.Rows[i]["TFI_MAP"].ToString());
                    row.CreateCell(5).SetCellValue("" + dt.Rows[i]["TFI_ZONGXU"].ToString());
                    row.CreateCell(6).SetCellValue("" + dt.Rows[i]["TFI_NAME"].ToString());
                    row.CreateCell(7).SetCellValue("" + dt.Rows[i]["TFI_RKNUM"].ToString());
                    row.CreateCell(8).SetCellValue("" + dt.Rows[i]["TFI_NUMBER"].ToString());
                    row.CreateCell(9).SetCellValue("" + dt.Rows[i]["TFI_WGHT"].ToString());
                    row.CreateCell(10).SetCellValue("" + dt.Rows[i]["INDATE"].ToString());
                    row.CreateCell(11).SetCellValue("" + dt.Rows[i]["SPSJ"].ToString());
                    row.CreateCell(12).SetCellValue("" + dt.Rows[i]["NOTE"].ToString());
                    row.CreateCell(13).SetCellValue("" + dt.Rows[i]["SPR1"].ToString());
                    row.CreateCell(14).SetCellValue("" + dt.Rows[i]["SPR2"].ToString());

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
