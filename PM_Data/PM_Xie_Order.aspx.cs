using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Xie_Order : BasicPage
    {
        PagerQueryParamGroupBy pager = new PagerQueryParamGroupBy();
        public int ObjPageSize
        {
            get
            {
                if (ViewState["ObjPageSize"] == null)
                {
                    //默认是升序
                    ViewState["ObjPageSize"] = Convert.ToDouble(DropDownList5.SelectedValue);
                }

                return Convert.ToInt32(ViewState["ObjPageSize"]);
            }
            set
            {
                ViewState["ObjPageSize"] = value;
            }
        }
        public string gptcode
        {
            get
            {
                object str = ViewState["gptcode"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gptcode"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Form.DefaultButton = QueryButton.UniqueID;  //2013年5月24日 08:23:02  Meng
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            ViewState["ObjPageSize"] = Convert.ToDouble(DropDownList5.SelectedValue);
            if (!IsPostBack)
            {
                this.BindYearMoth(ddlYear, ddlMonth);
                getArticle(true);
            }
            CheckUser(ControlFinder);
        }
        /// <summary>
        /// 绑定年月

        private void BindYearMoth(DropDownList ddl_Year, DropDownList ddl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                ddl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            ddl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                ddl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            ddl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }
        protected void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void ddlMonth_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }

        private void BindPage(string tableName, string PrimaryKey, string ShowFields, string OrderField, string GroupField, int OrderType, string StrWhere, int PageSize, bool isFristPage)
        {

            InitVar(tableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFristPage);

        }


        private void InitVar(string tableName, string PrimaryKey, string ShowFields, string OrderField, string GroupField, int OrderType, string StrWhere, int PageSize, bool isFristPage)
        {

            InitPager(tableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize);//初始化页面

            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数

            if (isFristPage)
            {
                UCPaging1.CurrentPage = 1;
            }

            bindData();
        }

        //初始化分页信息
        private void InitPager(string tableName, string PrimaryKey, string ShowFields, string OrderField, string GroupField, int OrderType, string StrWhere, int PageSize)
        {
            pager.TableName = tableName;

            pager.PrimaryKey = PrimaryKey;

            pager.ShowFields = ShowFields;

            if (string.IsNullOrEmpty(GroupField))

                pager.OrderField = OrderField;
            else

                pager.OrderField = GroupField;

            pager.GroupField = GroupField;

            pager.OrderType = OrderType;

            pager.StrWhere = StrWhere;

            pager.PageSize = PageSize;
        }

        protected void bindData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamGroupBy(pager);

            CommonFun.Paging(dt, Purordertotal_list_Repeater, UCPaging1, NoDataPane);
            if (NoDataPane.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            //clearData();
        }

        void Pager_PageChanged(int pageNumber)
        {
            getArticle(false);
        }

        private void getArticle(bool isFirstPage)      //取得Article数据
        {
            CreateDataSource();

            string TableName = "VIEW_TBMP_Order AS A LEFT JOIN (select PTC,BJSJ,rn from (select *,row_number() over(partition by PTC order by ISAGAIN) as rn from View_TBQM_APLYFORITEM) as a where rn<=1 )B ON A.TO_PTC=B.PTC";

            string PrimaryKey = "";

            string ShowFields = "B.BJSJ,TO_ID,TO_DOCNUM, TO_BJDOCNUM, TO_PTC, TO_ZDR,CONVERT(varchar(12) , TO_ZDTIME,23) as TO_ZDTIME, TO_SUPPLYID, TO_AMOUNT,  CONVERT(varchar(12) , TO_JHQ,23) as TO_JHQ,CONVERT(varchar(12) , TO_SJJHQ,23) as TO_SJJHQ, TO_NOTE, TO_STATE, PIC_SHEETNO, PIC_JGNUM, PIC_JSNUM, PIC_BJSTATUS, PIC_WXTYPE, PIC_TUHAO, PIC_MNAME, PIC_ZXNUM, PIC_SUPPLYTIME, PIC_PRICE, TO_ZDRNAME, PIC_PTCODE, TO_PROCESS, TO_UWGHT, TO_MONEY, TO_SUPPLYNAME,TO_TOTALNOTE";

            //数据库中的主键
            //string OrderField = "orderno desc,marnm,margg,ptcode";
            string OrderField = "TO_DOCNUM DESC,TO_BJDOCNUM DESC,TO_PTC ";

            string GroupField = "";

            int OrderType = 1;
            /**/
            string StrWhere = ViewState["sqlwhere"].ToString();

            int PageSize = ObjPageSize;

            BindPage(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFirstPage);
        }
        public void CreateDataSource()
        {
            string sqlwhere = "";
            string sqltext = "";
            string nowtime = System.DateTime.Now.ToString("yyyy-MM-dd");
            sqltext = "select * from VIEW_TBMP_Order where ";
            sqlwhere = "1=1 ";
            if (txt_docnum.Text != "")
            {
                sqlwhere = sqlwhere + "and TO_DOCNUM like '%" + txt_docnum.Text.Trim() + "%'";
            }
            if (rad_mypart.Checked)
            {
                sqlwhere = sqlwhere + " and TO_ZDR='" + Session["UserID"].ToString() + "'";
            }

            if (tb_supply.Text != "")
            {
                sqlwhere = sqlwhere + " and TO_SUPPLYNAME like '%" + tb_supply.Text.Trim() + "%'";
            }
            if (ddl_bjstatus.SelectedIndex != 0)
            {

                sqlwhere = sqlwhere + " and PIC_BJSTATUS='" + ddl_bjstatus.SelectedValue.ToString() + "'";

            }
            if (rbl_waixie.SelectedIndex != 0)
            {
                sqlwhere = sqlwhere + " and PIC_WXTYPE ='" + rbl_waixie.SelectedValue.ToString() + "'";
            }
            if (ddlYear.SelectedIndex != 0 && ddlMonth.SelectedIndex != 0)
            {
                sqlwhere += " and substring(convert(varchar(50),TO_ZDTIME,23),1,7)='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "'";
            }
            else if (ddlYear.SelectedIndex == 0 && ddlMonth.SelectedIndex != 0)
            {
                sqlwhere += " and convert(varchar(50),TO_ZDTIME,23) like '%-" + ddlMonth.SelectedValue.ToString().Trim() + "-%'";
            }
            else if (ddlYear.SelectedIndex != 0 && ddlMonth.SelectedIndex == 0)
            {
                sqlwhere += " and convert(varchar(50),TO_ZDTIME,23) like '%" + ddlYear.SelectedValue.ToString().Trim() + "-%'";
            }

            //计划跟踪号
            if (txt_jhgzh.Text != "")
            {
                sqlwhere = sqlwhere + " and TO_PTC like '%" + txt_jhgzh.Text.Trim() + "%'";
            }
            sqltext = sqltext + sqlwhere + " order by TO_DOCNUM";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            double tot_money = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //tot_money += Convert.ToDouble(dt.Rows[i]["TO_MONEY"].ToString());
                tot_money += CommonFun.ComTryDouble(dt.Rows[i]["TO_MONEY"].ToString());
            }
            lb_select_num.Text = Convert.ToString(dt.Rows.Count);
            lb_select_money.Text = string.Format("{0:c}", tot_money);
            ViewState["sqlwhere"] = sqlwhere;
        }
        protected void rbl_waixie_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void rad_all_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void rad_mypart_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void rad_quanbu_CheckedChanged(object sender, EventArgs e)
        {
            getArticle(true);
        }
        /// <summary>
        /// 报检！！！！！！！！
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_baojian_Click(object sender, EventArgs e)
        {

        }
        //private double zxnum = 0;
        //private double dnum = 0;
        //private string state = "";
        //private string date = "";
        //private string baojian = "";
        protected void Purordertotal_list_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //查看报检信息
                string zlbjjg = ((System.Web.UI.WebControls.Label)e.Item.FindControl("zlbj")).Text;
                if (zlbjjg != "未报检")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("zlbj")).ForeColor = System.Drawing.Color.Red;
                    string ptc = ((System.Web.UI.WebControls.Label)e.Item.FindControl("TO_PTC")).Text;
                    string sql = "select AFI_ID from TBQM_APLYFORINSPCT  where UNIQUEID=(select top 1 UNIQUEID from  TBQM_APLYFORITEM where PTC='" + ptc + "' order by id desc)";
                    System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt1.Rows.Count > 0)
                    {
                        string afiid = dt1.Rows[0]["AFI_ID"].ToString();
                        ((HyperLink)e.Item.FindControl("Hypzlbj")).NavigateUrl = "~/QC_Data/QC_Inspection_Add.aspx?ACTION=VIEW&NUM=1&ID=" + afiid + "";
                    }
                }


            }
            if (e.Item.ItemType == ListItemType.Header)
            {

            }
        }
        protected void btn_xiatui_Click(object sender, EventArgs e)//生成订单
        {
            int temp = isselected1();
            if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
                return;
            }
            else if (temp == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择的数据包含有不同的供应商的记录,本次操作无效！');", true);
                return;
            }
            else if (temp == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择的数据包含已生成结算单的记录,本次操作无效！');", true);
                return;
            }
            else
            {
                #region
                string orderno = encodeorderno();//自动生成订单号
                string sqltext;
                string ptc = "";
                int i = 0;
                List<string> sqltextlist = new List<string>();
                //明细表，初始状态为0
                string sqlwxhs = "select * from TBFM_WXHS where WXHS_STATE='1' and WXHS_YEAR+'-'+WXHS_MONTH like '" + DateTime.Now.ToString("yyyy-MM") + "%'";
                System.Data.DataTable dtwxhs = DBCallCommon.GetDTUsingSqlText(sqlwxhs);
                string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
                {

                    System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT");
                    if (cb != null)
                    {
                        if (cb.Checked)
                        {
                            if (dtwxhs.Rows.Count > 0)
                            {
                                if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 12)
                                {
                                    date = DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01 12:00";
                                }
                                else if (Convert.ToInt32(DateTime.Now.Month.ToString()) == 12)
                                {
                                    date = DateTime.Now.AddYears(1).AddMonths(-11).ToString("yyyy-MM") + "-01 12:00";
                                }
                            }
                            i++;
                            ptc = ((System.Web.UI.WebControls.Label)Reitem.FindControl("TO_PTC")).Text;
                            sqltext = "INSERT INTO TBMP_ACCOUNTS (TA_DOCNUM, TA_ORDERNUM, TA_PTC, TA_ZDR,TA_ZDTIME,TA_NUM,TA_MONEY,TA_WGHT) select '" + orderno + "',TO_DOCNUM, TO_PTC,'" + Session["UserID"].ToString() + "','" + date + "',PIC_ZXNUM,CAST(PIC_PRICE * PIC_ZXNUM AS decimal(18,4)),CAST(TO_UWGHT * PIC_ZXNUM AS decimal(18,4)) from VIEW_TBMP_Order  WHERE TO_PTC='" + ptc + "'";
                            sqltextlist.Add(sqltext);
                            sqltext = "update TBMP_Order  set TO_STATE='1' where TO_PTC='" + ptc + "'";//生成订单
                            sqltextlist.Add(sqltext);
                        }
                    }
                }
                DBCallCommon.ExecuteTrans(sqltextlist);
                getArticle(true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('PM_Xie_IntoAccounts.aspx?orderno=" + orderno + "');", true);
                #endregion
            }
        }
        protected int isselected1()
        {
            int temp = 0;
            string providerid = "";
            int i = 0;//是否选择数据
            int j = 0;//是否审核
            int k = 0;//供应商是否相同
            int l = 0;//选择的数据中是否包含已生成订单数据

            foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        string ptc = ((System.Web.UI.WebControls.Label)Reitem.FindControl("TO_PTC")).Text;
                        string sql = "select TO_PTC from TBMP_Order where TO_PTC='" + ptc + "' and TO_STATE='1'";
                        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        if (i == 1)
                        {
                            providerid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("TO_SUPPLYID")).Text;
                        }
                        if (providerid != ((System.Web.UI.WebControls.Label)Reitem.FindControl("TO_SUPPLYID")).Text)
                        {
                            k++;
                            break;
                        }
                        else if (dt.Rows.Count > 0)
                        {
                            l++;
                            break;
                        }
                    }
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else if (k > 0)//选择的供应商不同
            {
                temp = 3;
            }
            else if (l > 0)//选择的数据中包含已生成结算单数据
            {
                temp = 4;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }
        private string encodeorderno()
        {
            string pi_id = "";
            string tag_pi_id = "JS";
            string end_pi_id = "";
            string sqltext = "SELECT TOP 1 TA_DOCNUM FROM TBMP_ACCOUNTS WHERE TA_DOCNUM LIKE '" + tag_pi_id + "%' ORDER BY TA_DOCNUM DESC";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                end_pi_id = Convert.ToString(Convert.ToInt32(dt.Rows[0]["TA_DOCNUM"].ToString().Substring((dt.Rows[0]["TA_DOCNUM"].ToString().Length - 8), 8)) + 1);
                end_pi_id = end_pi_id.PadLeft(8, '0');
            }
            else
            {
                end_pi_id = "00000001";
            }
            pi_id = tag_pi_id + end_pi_id;
            return pi_id;
        }
        protected void btn_daochu_Click(object sender, EventArgs e)
        {
            int temp = isselected();
            List<string> list = new List<string>();
            if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您选择的数据包含多条订单,本次操作无效！');", true);
            }
            else
            {
                string sqltext = "";
                string docnum = "";
                foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT");
                    if (cb.Checked)
                    {
                        docnum = ((System.Web.UI.WebControls.Label)Reitem.FindControl("TO_DOCNUM")).Text;
                        sqltext = "select TO_DOCNUM,  TO_ZDRNAME, TO_ZDTIME,TO_SUPPLYNAME,TO_AMOUNT,TO_TOTALNOTE, CAST(TO_BJDOCNUM AS BIGINT) as TO_BJDOCNUM,PIC_JGNUM, TO_PTC, PIC_TUHAO,PIC_MNAME,PIC_ZXNUM,PIC_WXTYPE,TO_PROCESS,PIC_PRICE,TO_MONEY,PIC_SUPPLYTIME,TO_SJJHQ,TO_NOTE,isnull(PIC_BJSTATUS,'未报检') from VIEW_TBMP_Order where TO_DOCNUM='" + docnum + "'";
                    }

                }
                ExportDataItem1(sqltext, docnum);
            }
        }
        private void ExportDataItem1(string sqltext, string docnum)
        {
            string filename = "外协订单" + docnum + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("外协订单.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');;</script>");
                    return;
                }
                IRow row2 = sheet0.GetRow(2);
                row2.GetCell(1).SetCellValue(dt.Rows[0]["TO_DOCNUM"].ToString());
                row2.GetCell(4).SetCellValue(dt.Rows[0]["TO_ZDRNAME"].ToString());
                row2.GetCell(7).SetCellValue(dt.Rows[0]["TO_ZDTIME"].ToString());
                IRow row3 = sheet0.GetRow(3);
                row3.GetCell(1).SetCellValue(dt.Rows[0]["TO_SUPPLYNAME"].ToString());
                row3.GetCell(4).SetCellValue(dt.Rows[0]["TO_AMOUNT"].ToString());
                row3.GetCell(7).SetCellValue(dt.Rows[0]["TO_TOTALNOTE"].ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 5);
                    for (int j = 0; j <= 13; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j + 6].ToString());
                    }
                }
                sheet0.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        protected void btn_Export_Click(object sender, EventArgs e)
        {
            string sqlexport = "select TO_DOCNUM, TO_ZDRNAME, TO_ZDTIME,TO_AMOUNT,TO_SUPPLYNAME, CAST(TO_BJDOCNUM AS BIGINT) as TO_BJDOCNUM,PIC_JGNUM, TO_PTC, PIC_TUHAO,PIC_MNAME,PIC_ZXNUM,PIC_WXTYPE,TO_PROCESS,PIC_PRICE,TO_MONEY,PIC_SUPPLYTIME,TO_SJJHQ,TO_NOTE,isnull(PIC_BJSTATUS,'未报检') from VIEW_TBMP_Order where " + ViewState["sqlwhere"] + "order by TO_DOCNUM";
            ExportDataItem(sqlexport, DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
        private void ExportDataItem(string sqltext, string exporttime)
        {
            string filename = "外协订单" + exporttime + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("外协订单(批量).xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');;</script>");
                    return;
                }
                IRow row2 = sheet0.GetRow(2);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2);
                    for (int j = 1; j <= 19; j++)
                    {
                        row.CreateCell(0).SetCellValue(i+1);
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j-1].ToString());
                    }
                }
                sheet0.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        /// <summary>
        /// 是否可导出条件
        /// </summary>
        /// <returns></returns>
        protected int isselected()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            int j = 0;//是否选择多条订单
            foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        string docnum = ((System.Web.UI.WebControls.Label)Reitem.FindControl("TO_DOCNUM")).Text;
                        string sql = "select distinct TO_DOCNUM from TBMP_Order where TO_DOCNUM='" + docnum + "'";
                        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                        if (dt.Rows.Count > 1)
                        {
                            j++;
                            break;
                        }
                    }
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else if (j > 0)//包含已经生成结算单的数据
            {
                temp = 2;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }
        protected void btn_add_Click(object sender, EventArgs e)//追加订单
        {
            int j = 0;
            int temp = isselected1();
            string ptcode_rcode = "";
            string ptcode = "";
            if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
                return;
            }
            else if (temp == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择的数据包含有不同的供应商的记录,本次操作无效！');", true);
                return;
            }
            else if (temp == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择的数据包含已生成结算单的记录,本次操作无效！');", true);
                return;
            }
            else
            {
                foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
                {
                    if (((System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT")).Checked)
                    {
                        ptcode = ptcode + ((System.Web.UI.WebControls.Label)Reitem.FindControl("TO_ID")).Text + ",";
                    }
                }
                ptcode_rcode = ptcode + Session["UserID"].ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "mowinopen('" + ptcode_rcode + "');", true);
            }
        }
        public string get_zlbj(string i)
        {
            string statestr = "";
            if (i == "")
            {
                statestr = "未报检";
            }
            else
            {
                statestr = i;
            }
            return statestr;
        }
        public int Get_Int(string i)
        {

            int statestr = Convert.ToInt32(i);
            return statestr;

        }
        public string Get_jsstate(string i)
        {
            string statestr = "";
            if (i == "1")
            {
                statestr = "<span style='color:red'>是</span>";
            }
            else
            {
                statestr = "否";
            }
            return statestr;

        }
        protected void selectall_CheckedChanged(object sender, EventArgs e)
        {
            if (selectall.Checked)
            {
                foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = true;
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = false;
                    }
                }
            }
        }

        protected void btn_LX_click(object sender, EventArgs e)//连选
        {
            int i = 0;
            int j = 0;
            int start = 0;
            int finish = 0;
            int k = 0;
            foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
            {
                j++;
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                if (cbx.Checked)
                {
                    i++;
                    if (start == 0)
                    {
                        start = j;
                    }
                    else
                    {
                        finish = j;
                    }
                }
            }
            if (i == 2)
            {
                foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
                {
                    k++;
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                    if (k >= start && k <= finish)
                    {
                        cbx.Checked = true;
                    }
                    if (k > finish)
                    {
                        cbx.Checked = false;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择连续的区间！');", true);
            }
        }
        protected void btn_QX_click(object sender, EventArgs e)
        {
            foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                cbx.Checked = false;
            }
        }

        //添加合同审批
        //protected void btn_AddHTSP_Click(object sender, EventArgs e)
        //{
        //    int checknum = ifselect();//判断是否选择数据行

        //    if (checknum == 0)
        //    {
        //        double ZJE = 0;
        //        string strb_orderid = "";
        //        foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
        //        {
        //            System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
        //            if (cbx.Checked)
        //            {
        //                System.Web.UI.WebControls.Label lb_shstate = Reitem.FindControl("PO_shbz") as System.Web.UI.WebControls.Label;

        //                if (lb_shstate.Text == "是")
        //                {
        //                    //去掉相同的订单号
        //                    string add_pocode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PO_CODE")).Text.ToString();
        //                    if (!strb_orderid.Contains(add_pocode))
        //                    {
        //                        strb_orderid += "'" + add_pocode + "',";
        //                    }
        //                    string sqltext = "select PCON_ORDERID from TBPM_CONPCHSINFO where PCON_ORDERID like '%" + add_pocode + "%'";
        //                    System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
        //                    if (dt1.Rows.Count > 0)
        //                    {
        //                        getArticle(true); //刷新
        //                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已添加合同，请勿重复添加！');", true);
        //                        return;
        //                    }

        //                }
        //                else if (lb_shstate.Text == "否")
        //                {
        //                    getArticle(true); //刷新
        //                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择已审核的订单！');", true);
        //                    return;
        //                }

        //            }
        //        }

        //        strb_orderid = strb_orderid.Substring(0, strb_orderid.Length - 1);

        //        //合同总价要先对订单分组求和（group by），并保留两位，再将各订单的总价相加，否则最后结果因四舍五入与订单相加结果有出入                
        //        string sql = "select round(sum(ctamount),2) as DDZJ from View_TBPC_PURORDERDETAIL_PLAN_MPLAN where orderno in (" + strb_orderid + ") and detailcstate='0' group by orderno";
        //        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                double JinE = Convert.ToDouble(dt.Rows[i]["DDZJ"].ToString());
        //                ZJE = ZJE + JinE;
        //            }
        //        }


        //        string sql_checkSupplier = "select  DISTINCT (SUPPLIERID) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL WHERE ORDERNO IN (" + strb_orderid + ")";
        //        System.Data.DataTable DT = DBCallCommon.GetDTUsingSqlText(sql_checkSupplier);
        //        if (DT.Rows.Count > 1)  //选择的订单包括多个供应商，不能添加
        //        {
        //            getArticle(true); //刷新
        //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择同一供应商的订单！');", true);
        //            return;
        //        }
        //        else
        //        {
        //            getArticle(true);
        //            strb_orderid = strb_orderid.Replace("'", "");   //将字符串中的单引号去掉，否则传递参数时会自动截断！
        //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "js", "Add_HTSP('" + strb_orderid + "','" + ZJE + "');", true);
        //        }
        //    }
        //    else if (checknum == 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要添加合同审批的数据行！');", true);
        //    }
        //}

        //重置条件
        protected void btnReset_Click(object sender, EventArgs e)
        {
            //tb_orderno.Text = string.Empty;
            //tb_supply.Text = string.Empty;
            //tb_StartTime.Text = string.Empty;
            //tb_EndTime.Text = string.Empty;
            //tb_name.Text = string.Empty;
            //tb_pj.Text = string.Empty;
            //tb_eng.Text = string.Empty;
            //tb_th.Text = string.Empty;
            //drp_stu.ClearSelection();
            //drp_stu.Items[0].Selected = true;
        }

        //取消
        protected void btnClose_Click(object sender, EventArgs e)
        {
            //ModalPopupExtenderSearch.Hide();
        }

        protected void QueryButton_Click(object sender, EventArgs e)
        {
            getArticle(true);
        }

        protected void tb_supply_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (tb_supply.Text.ToString().Contains("|"))
            {
                Cname = tb_supply.Text.Split('|')[1].ToString();
                tb_supply.Text = Cname.Trim();
            }
            else if (tb_supply.Text == "")
            {

            }
        }
        //添加订单请款
        //protected void btn_AddDDQK_Click(object sender, EventArgs e)
        //{
        //    int checknum = ifselect();//判断是否选择数据行

        //    if (checknum == 0)
        //    {
        //        double ZJE = 0;   //总金额
        //        string strb_orderid = "";  //订单号
        //        foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
        //        {
        //            System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
        //            if (cbx.Checked)
        //            {
        //                System.Web.UI.WebControls.Label lb_shstate = Reitem.FindControl("PO_shbz") as System.Web.UI.WebControls.Label;

        //                if (lb_shstate.Text == "是")
        //                {
        //                    //去掉相同的订单号
        //                    string add_pocode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PO_CODE")).Text.ToString();
        //                    if (!strb_orderid.Contains(add_pocode))
        //                    {
        //                        string sqltext = "select DQ_DDCode from TBPM_ORDER_CHECKREQUEST where DQ_DDCODE like '%" + add_pocode + "%'";
        //                        System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
        //                        if (dt1.Rows.Count > 0)
        //                        {
        //                            getArticle(true);//刷新
        //                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('所选订单已添加非合同采购订单请款，请勿重复添加！');", true);
        //                            return;
        //                        }
        //                        strb_orderid += "'" + add_pocode + "',";
        //                    }

        //                }
        //                else if (lb_shstate.Text == "否")
        //                {
        //                    getArticle(true);//刷新
        //                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择已审核的订单！');", true);
        //                    return;
        //                }

        //            }
        //        }

        //        strb_orderid = strb_orderid.Substring(0, strb_orderid.Length - 1);

        //        //合同总价要先对订单分组求和（group by），并保留两位，再将各订单的总价相加，否则最后结果因四舍五入与订单相加结果有出入                
        //        string sql = "select round(sum(ctamount),2) as DDZJ from View_TBPC_PURORDERDETAIL_PLAN_MPLAN where orderno in (" + strb_orderid + ") and detailcstate='0' group by orderno";
        //        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                double JinE = Convert.ToDouble(dt.Rows[i]["DDZJ"].ToString());
        //                ZJE = ZJE + JinE;
        //            }
        //        }


        //        string sql_checkSupplier = "select  DISTINCT (SUPPLIERID) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL WHERE ORDERNO IN (" + strb_orderid + ")";
        //        System.Data.DataTable DT = DBCallCommon.GetDTUsingSqlText(sql_checkSupplier);
        //        if (DT.Rows.Count > 1)  //选择的订单包括多个供应商，不能添加
        //        {
        //            getArticle(true);//刷新
        //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择同一供应商的订单！');", true);
        //            return;
        //        }
        //        else
        //        {
        //            getArticle(true);//刷新
        //            string ddcs_code = DT.Rows[0][0].ToString();
        //            strb_orderid = strb_orderid.Replace("'", "");   //将字符串中的单引号去掉，否则传递参数时会自动截断！
        //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "js", "Add_DDQK('" + strb_orderid + "','" + ZJE + "','" + ddcs_code + "');", true);
        //        }
        //    }
        //    else if (checknum == 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要添加合同审批的数据行！');", true);
        //    }
        //}

        //添加发票
        //protected void btn_AddDDFP_Click(object sender, EventArgs e)
        //{
        //    int checknum = ifselect();//判断是否选择数据行

        //    if (checknum == 0)
        //    {
        //        double ZJE = 0;   //总金额
        //        string strb_orderid = "";  //订单号
        //        foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
        //        {
        //            System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
        //            if (cbx.Checked)
        //            {
        //                System.Web.UI.WebControls.Label lb_shstate = Reitem.FindControl("PO_shbz") as System.Web.UI.WebControls.Label;

        //                if (lb_shstate.Text == "是")
        //                {
        //                    //去掉相同的订单号
        //                    string add_pocode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PO_CODE")).Text.ToString();
        //                    if (!strb_orderid.Contains(add_pocode))
        //                    {
        //                        string sqltext = "select OB_DDCode from TBPM_ORDER_BILL where OB_DDCODE like '%" + add_pocode + "%'";
        //                        System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
        //                        if (dt1.Rows.Count > 0)
        //                        {
        //                            getArticle(true);//刷新
        //                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('所选订单已添加采购订单发票信息，请勿重复添加！');", true);
        //                            return;
        //                        }
        //                        strb_orderid += "'" + add_pocode + "',";
        //                    }
        //                }
        //                else if (lb_shstate.Text == "否")
        //                {
        //                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择已审核的订单！');", true);
        //                    return;
        //                }

        //            }
        //        }

        //        strb_orderid = strb_orderid.Substring(0, strb_orderid.Length - 1);

        //        //合同总价要先对订单分组求和（group by），并保留两位，再将各订单的总价相加，否则最后结果因四舍五入与订单相加结果有出入                
        //        string sql = "select round(sum(ctamount),2) as DDZJ from View_TBPC_PURORDERDETAIL_PLAN_MPLAN where orderno in (" + strb_orderid + ") and detailcstate='0' group by orderno";
        //        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
        //        if (dt.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < dt.Rows.Count; i++)
        //            {
        //                double JinE = Convert.ToDouble(dt.Rows[i]["DDZJ"].ToString());
        //                ZJE = ZJE + JinE;
        //            }
        //        }
        //        string sql_checkSupplier = "select  DISTINCT (SUPPLIERID) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL WHERE ORDERNO IN (" + strb_orderid + ")";
        //        System.Data.DataTable DT = DBCallCommon.GetDTUsingSqlText(sql_checkSupplier);
        //        if (DT.Rows.Count > 1)  //选择的订单包括多个供应商，不能添加
        //        {
        //            getArticle(true);//刷新
        //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择同一供应商的订单！');", true);
        //            return;
        //        }
        //        else
        //        {
        //            getArticle(true); //页面刷新
        //            string ddcs_code = DT.Rows[0][0].ToString();
        //            strb_orderid = strb_orderid.Replace("'", "");   //将字符串中的单引号去掉，否则传递参数时会自动截断！
        //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "js", "Add_DDFP('" + strb_orderid + "','" + ZJE + "','" + ddcs_code + "');", true);
        //        }
        //    }
        //    else if (checknum == 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要添加订单发票记录的数据行！');", true);
        //    }
        //}
    }
}
