using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Xie_Accounts : System.Web.UI.Page
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
            ViewState["ObjPageSize"] = Convert.ToDouble(DropDownList5.SelectedValue);
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                if (Session["UserDeptID"].ToString().Trim() == "06" || Session["UserName"].ToString().Trim() == "管理员" || Session["UserDeptID"].ToString().Trim() == "04")
                {
                    btnxtred.Visible = true;
                }
                BindYearMoth(ddlYear, ddlMonth);
                InitPage();
                getArticle(true);
            }
        }

        /// <summary>
        /// 绑定年月
        /// </summary>
        /// <param name="dpl_Year"></param>
        /// <param name="dpl_Month"></param>
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

        private void InitPage()
        {
            ddlYear.ClearSelection();
            foreach (ListItem li in ddlYear.Items)//显示当前年份
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }

            ddlMonth.ClearSelection();
            string month = (DateTime.Now.Month - 1).ToString();
            if (DateTime.Now.Month < 10 || DateTime.Now.Month == 10)//显示当前月份
            {
                month = "0" + month;
            }
            foreach (ListItem li in ddlMonth.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
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
        }

        void Pager_PageChanged(int pageNumber)
        {
            getArticle(false);
        }

        private void getArticle(bool isFirstPage)      //取得Article数据
        {
            CreateDataSource();

            string TableName = "VIEW_TBMP_ACCOUNTS AS A LEFT JOIN (select PTC,BJSJ,rn from (select *,row_number() over(partition by PTC order by ISAGAIN) as rn from View_TBQM_APLYFORITEM) as a where rn<=1 )B ON A.TA_PTC=B.PTC ";

            string PrimaryKey = "";

            string ShowFields = "B.BJSJ, PIC_JGNUM,TA_DOCNUM, TA_ORDERNUM, TA_PTC, TA_ZDR, Convert( varchar, TA_ZDTIME ,23) as TA_ZDTIME , TA_NUM, TA_MONEY, TA_WGHT, TA_AMOUNT, TA_TOTALWGHT, TA_NOTE, TA_TOTALNOTE, TA_ZDRNAME, TA_PROCESS, TA_SUPPLYNAME, TA_TUHAO, TA_MNAME, TA_ZXNUM, TA_JHQ, TA_PRICE, TA_ENGID, PIC_PTCODE, TA_WXTYPE, TA_GUIGE, TA_CAIZHI, TA_UWGHT";

            string OrderField = "TA_DOCNUM desc ,PIC_JGNUM";

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
            sqltext = "select * from VIEW_TBMP_ACCOUNTS where ";
            sqlwhere = "1=1 ";
            if (txt_docnum.Text != "")
            {
                sqlwhere = sqlwhere + "and TA_DOCNUM like '%" + txt_docnum.Text.Trim() + "%'";
            }
            if (rad_mypart.Checked)
            {
                sqlwhere = sqlwhere + " and TA_ZDR='" + Session["UserID"].ToString() + "'";
            }

            if (tb_supply.Text != "")
            {
                sqlwhere = sqlwhere + " and TA_SUPPLYNAME like '%" + tb_supply.Text.Trim() + "%'";
            }
            if (rbl_waixie.SelectedIndex != 0)
            {
                sqlwhere = sqlwhere + " and TA_WXTYPE ='" + rbl_waixie.SelectedValue.ToString() + "'";
            }
            if (ddlYear.SelectedValue != "-请选择-" && ddlMonth.SelectedValue != "-请选择-")
            {
                sqlwhere += " and Convert( varchar, TA_ZDTIME ,23) like '%" + ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue + "%'";
            }
            if (drp_type.SelectedIndex == 1)
            {
                sqlwhere += " and patindex('%RED%',TA_DOCNUM)=0";
            }
            if (drp_type.SelectedIndex == 2)
            {
                sqlwhere += " and patindex('%RED%',TA_DOCNUM)>0";// or TA_PTC in(select SUBSTRING(TBMP_ACCOUNTS.TA_PTC,1,LEN(TBMP_ACCOUNTS.TA_PTC) - 3) from TBMP_ACCOUNTS where patindex('%RED%',TA_PTC)>0))
            }
            //减轻模糊搜索占用服务器内存
            //if (drp_type.SelectedIndex == 1)
            //{
            //    sqlwhere += " and substring(TA_DOCNUM,11,3)<>'RED' and TA_PTC not in(select SUBSTRING(TBMP_ACCOUNTS.TA_PTC,1,LEN(TBMP_ACCOUNTS.TA_PTC) - 3) from TBMP_ACCOUNTS where right(TA_PTC,3)='RED')";
            //}
            //if (drp_type.SelectedIndex == 2)
            //{
            //    sqlwhere += " and substring(TA_DOCNUM,11,3)='RED' or TA_PTC in(select SUBSTRING(TBMP_ACCOUNTS.TA_PTC,1,LEN(TBMP_ACCOUNTS.TA_PTC) - 3) from TBMP_ACCOUNTS where right(TA_PTC,3)='RED')";
            //}
            //计划跟踪号
            if (txt_jhgzh.Text != "")
            {
                sqlwhere = sqlwhere + " and TA_PTC like '%" + txt_jhgzh.Text.Trim() + "%'";
            }
            sqltext = sqltext + sqlwhere + " order by TA_DOCNUM";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            double tot_money = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["TA_MONEY"].ToString() == "")
                {
                    tot_money += 0;
                }
                else
                {
                    tot_money += Convert.ToDouble(dt.Rows[i]["TA_MONEY"].ToString());
                }
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
        protected void Query(object sender, EventArgs e)
        {
            getArticle(true);
        }
        protected void Purordertotal_list_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }


        protected void drp_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            getArticle(true);
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
                string sqltext2 = "";
                string sqltext3 = "";
                string docnum = "";
                string money = "";
                foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT");
                    if (cb.Checked)
                    {
                        docnum = ((System.Web.UI.WebControls.Label)Reitem.FindControl("TA_DOCNUM")).Text;
                        //sqltext = "select TA_DOCNUM,  TA_ZDRNAME,CONVERT(varchar, TA_ZDTIME ,23) as TA_ZDTIME,TA_SUPPLYNAME,TA_AMOUNT,TA_TOTALWGHT,TA_TOTALNOTE,TA_PTC,TA_NUM,TA_WXTYPE,TA_PROCESS,TA_UWGHT,TA_WGHT,TA_PRICE,TA_MONEY,TA_JHQ,TA_SJJHQ,TA_NOTE from VIEW_TBMP_ACCOUNTS where TA_DOCNUM='" + docnum + "'";
                        sqltext = " select TA_ENGID,TA_TUHAO,TA_ZONGXU,TA_MNAME,TA_CAIZHI,TA_UNUM,TA_NUM,TA_UWGHT,TA_WGHT,TA_PROCESS,TA_PRICE,TA_MONEY,PIC_JGNUM ,TA_TOTALNOTE,TA_ZDRNAME,TA_SUPPLYNAME,TA_TOTALWGHT,TA_AMOUNT from VIEW_TBMP_ACCOUNTS where TA_DOCNUM='" + docnum + "' order by TA_ENGID ";
                        sqltext2 = "select TA_ENGID,TA_WXTYPE,'',TO_ENGNAME, SUM(TA_NUM),SUM(TA_WGHT),SUM(TA_MONEY),TA_JHQ,BJSJ,PIC_JGNUM from VIEW_TBMP_ACCOUNTS AS a left join (select PTC,BJSJ ,ISAGAIN,rn from (select *,row_number() over(partition by PTC order by ISAGAIN ) as rn from View_TBQM_APLYFORITEM) as a where rn<=1 ) as B ON A.TA_PTC=B.PTC WHERE TA_DOCNUM='" + docnum + "' group by TA_ENGID,TA_WXTYPE,TO_ENGNAME,TA_JHQ,BJSJ,PIC_JGNUM";
                        sqltext3 = "select dbo.MP_Transfor(TA_AMOUNT) as money FROM VIEW_TBMP_ACCOUNTS WHERE TA_DOCNUM='" + docnum + "'";
                        money = DBCallCommon.GetDTUsingSqlText(sqltext3).Rows[0]["money"].ToString();
                    }
                }
                ExportDataItem1(sqltext, sqltext2, docnum, money);
            }
        }
        private void ExportDataItem1(string sqltext, string sqltext2, string docnum, string money)
        {
            string filename = "外协结算单" + docnum + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("外协结算单.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ICellStyle style2 = wk.CreateCellStyle();
                style2.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                style2.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                style2.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                style2.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                ISheet sheet0 = wk.GetSheetAt(0);
                // ISheet sheet1 = wk.GetSheetAt(1);
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                System.Data.DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext2);
                if (dt.Rows.Count == 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');;</script>");
                    return;
                }
                IRow row1_2 = sheet0.GetRow(2);
                row1_2.GetCell(1).SetCellValue(docnum);
                row1_2.GetCell(5).SetCellValue(dt.Rows[0]["TA_TOTALNOTE"].ToString());
                row1_2.GetCell(9).SetCellValue(dt.Rows[0]["TA_SUPPLYNAME"].ToString());

                //IRow row2 = sheet1.GetRow(2);
                //row2.GetCell(1).SetCellValue(dt.Rows[0]["TA_DOCNUM"].ToString());
                //row2.GetCell(3).SetCellValue(dt.Rows[0]["TA_ZDRNAME"].ToString());
                //row2.GetCell(5).SetCellValue(dt.Rows[0]["TA_ZDTIME"].ToString());
                //IRow row3 = sheet1.GetRow(3);
                //row3.GetCell(1).SetCellValue(dt.Rows[0]["TA_SUPPLYNAME"].ToString());
                //row3.GetCell(3).SetCellValue(dt.Rows[0]["TA_AMOUNT"].ToString());
                //row3.GetCell(6).SetCellValue(dt.Rows[0]["TA_TOTALWGHT"].ToString());
                //row3.GetCell(9).SetCellValue(dt.Rows[0]["TA_TOTALNOTE"].ToString());
                for (int k = 0; k < dt2.Rows.Count; k++)
                {
                    IRow row = sheet0.CreateRow(k + 4);

                    row.CreateCell(0).SetCellValue(k + 1);
                    row.Cells[0].CellStyle = style2;
                    for (int j = 1; j <= 2; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt2.Rows[k][j - 1].ToString());
                        row.Cells[j].CellStyle = style2;
                    }
                    row.CreateCell(3);
                    row.Cells[3].CellStyle = style2;
                    for (int j = 4; j <= 5; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt2.Rows[k][j - 2].ToString());
                        row.Cells[j].CellStyle = style2;
                    }
                    row.CreateCell(6);
                    row.Cells[6].CellStyle = style2;
                    for (int j = 7; j <= 12; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt2.Rows[k][j - 3].ToString());
                        row.Cells[j].CellStyle = style2;
                    }

                    SetCellRangeAddress(sheet0, k + 4, k + 4, 2, 3);
                    SetCellRangeAddress(sheet0, k + 4, k + 4, 5, 6);

                }
                int rownum = dt2.Rows.Count;
                IRow row1_x = sheet0.CreateRow(rownum + 4);
                row1_x.CreateCell(1).SetCellValue("合计金额:" + money);
                row1_x.CreateCell(8).SetCellValue(dt.Rows[0]["TA_TOTALWGHT"].ToString());
                row1_x.CreateCell(9).SetCellValue(dt.Rows[0]["TA_AMOUNT"].ToString());
                IRow row1_y = sheet0.CreateRow(rownum + 7);
                row1_y.CreateCell(1).SetCellValue("结算单位:中材（天津）重型机械有限公司");
                row1_y.CreateCell(7).SetCellValue("生产单位:" + dt.Rows[0]["TA_SUPPLYNAME"].ToString());
                IRow row1_z = sheet0.CreateRow(rownum + 10);
                row1_z.CreateCell(1).SetCellValue("经办人：" + dt.Rows[0]["TA_ZDRNAME"].ToString());
                row1_z.CreateCell(3).SetCellValue("日期：");
                row1_z.CreateCell(7).SetCellValue("签字：");
                IRow row1_w = sheet0.CreateRow(rownum + 13);
                row1_w.CreateCell(1).SetCellValue("部门主管：");
                row1_w.CreateCell(3).SetCellValue("日期：");
                row1_w.CreateCell(7).SetCellValue("日期：");
                int detailnum = rownum + 18;
                IRow row_1 = sheet0.CreateRow(detailnum);
                row_1.CreateCell(0).SetCellValue("任务号");
                // row_1.CreateCell(1).SetCellValue("总图号");
                row_1.CreateCell(1).SetCellValue("图号");
                row_1.CreateCell(2).SetCellValue("图中序号");
                row_1.CreateCell(3).SetCellValue("名称");
                row_1.CreateCell(4).SetCellValue("材料");
                row_1.CreateCell(5).SetCellValue("单台数量");
                row_1.CreateCell(6).SetCellValue("总数量");
                row_1.CreateCell(7).SetCellValue("单件净重");
                row_1.CreateCell(8).SetCellValue("总重");
                // row_1.CreateCell(9).SetCellValue("规格");
                row_1.CreateCell(9).SetCellValue("加工内容");
                row_1.CreateCell(10).SetCellValue("单价");
                row_1.CreateCell(11).SetCellValue("金额");
                row_1.CreateCell(12).SetCellValue("备注");
                for (int m = 0; m <= 12; m++)
                {
                    row_1.Cells[m].CellStyle = style2;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + detailnum + 1);
                    for (int j = 0; j <= 12; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                        row.Cells[j].CellStyle = style2;
                    }
                }
                sheet0.ForceFormulaRecalculation = true;
                //sheet1.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public static void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }
        protected int isselected()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            int j = 0;//是否生成结算单
            foreach (RepeaterItem Reitem in Purordertotal_list_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        string docnum = ((System.Web.UI.WebControls.Label)Reitem.FindControl("TA_DOCNUM")).Text;
                        string sql = "select distinct TA_DOCNUM from TBMP_ACCOUNTS where TA_DOCNUM='" + docnum + "'";
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
            else if (j > 0)//选择了多条订单
            {
                temp = 2;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }

        public int Get_Int(string i)
        {

            int statestr = Convert.ToInt32(i);
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






        //生成红字单据
        protected void btnxtred_click(object sender, EventArgs e)
        {
            string zdrid = Session["UserID"].ToString().Trim();
            List<string> list_sql = new List<string>();
            list_sql.Clear();
            string sqltext = "";
            int num = 0;
            int docnum = 0;
            string checkdocnum = "";
            for (int i = 0; i < Purordertotal_list_Repeater.Items.Count; i++)
            {
                if (((CheckBox)Purordertotal_list_Repeater.Items[i].FindControl("CKBOX_SELECT")).Checked)
                {
                    if (checkdocnum == "")
                    {
                        checkdocnum = ((System.Web.UI.WebControls.Label)Purordertotal_list_Repeater.Items[i].FindControl("TA_DOCNUM")).Text.Trim();
                        docnum++;
                    }
                    if (checkdocnum != "" && ((System.Web.UI.WebControls.Label)Purordertotal_list_Repeater.Items[i].FindControl("TA_DOCNUM")).Text.Trim() != checkdocnum)
                    {
                        docnum++;
                    }
                    num++;
                }
            }
            if (docnum > 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请选择同一张单据的数据！');", true);
                return;
            }
            if (num == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请勾选要下推红字的数据！');", true);
                return;
            }
            else
            {
                //红字结算单需变化信息
                string redjsdh = "";
                string redorderid = "";
                string redjsdrq = "";
                string redzdr = "";
                string redjhgzh = "";

                string sqlgetrq = "select * from TBFM_WXHS where WXHS_STATE='1' and WXHS_YEAR+'-'+WXHS_MONTH like '" + DateTime.Now.ToString("yyyy-MM") + "%'";
                System.Data.DataTable dtgetrq = DBCallCommon.GetDTUsingSqlText(sqlgetrq);
                redjsdrq = DateTime.Now.ToString("yyyy-MM-dd");
                if (dtgetrq.Rows.Count > 0)
                {
                    if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 12)
                    {
                        redjsdrq = DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01 12:00";
                    }
                    else if (Convert.ToInt32(DateTime.Now.Month.ToString()) == 12)
                    {
                        redjsdrq = DateTime.Now.AddYears(1).AddMonths(-11).ToString("yyyy-MM") + "-01 12:00";
                    }
                }

                for (int i = 0; i < Purordertotal_list_Repeater.Items.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)Purordertotal_list_Repeater.Items[i].FindControl("CKBOX_SELECT")).Checked)
                    {
                        string jhgzhthis = ((System.Web.UI.WebControls.Label)Purordertotal_list_Repeater.Items[i].FindControl("TA_PTC")).Text.Trim();
                        string sqlcheck = "select * from TBMP_ACCOUNTS where TA_PTC='" + jhgzhthis + "RED'";
                        System.Data.DataTable dtcheck = DBCallCommon.GetDTUsingSqlText(sqlcheck);
                        if (dtcheck.Rows.Count > 0 || jhgzhthis.Contains("RED"))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('有数据已下推过红字或为红字数据，请不要重复操作！');", true);
                            return;
                        }

                        string sqlgetdatainfo = "";
                        sqlgetdatainfo = "select * from TBMP_ACCOUNTS where TA_PTC='" + jhgzhthis + "'";
                        System.Data.DataTable dtgetdatainfo = DBCallCommon.GetDTUsingSqlText(sqlgetdatainfo);
                        if (dtgetdatainfo.Rows.Count > 0)
                        {
                            redjsdh = dtgetdatainfo.Rows[0]["TA_DOCNUM"].ToString().Trim() + "RED" + DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + Session["UserID"].ToString();
                            redorderid = dtgetdatainfo.Rows[0]["TA_ORDERNUM"].ToString().Trim() + "RED" + DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + Session["UserID"].ToString();

                            redzdr = Session["UserID"].ToString();
                            redjhgzh = dtgetdatainfo.Rows[0]["TA_PTC"].ToString().Trim() + "RED";
                            sqltext = "insert into TBMP_ACCOUNTS(TA_DOCNUM,TA_ORDERNUM,TA_PTC,TA_SUPPLYID,TA_ZDR,TA_ZDTIME,TA_GJSTATE,TA_NUM,TA_MONEY,TA_WGHT,TA_AMOUNT,TA_TOTALWGHT,TA_NOTE,TA_TOTALNOTE,TA_XTSTATE) values('" + redjsdh + "','" + redorderid + "','" + redjhgzh + "','" + dtgetdatainfo.Rows[0]["TA_SUPPLYID"].ToString().Trim() + "','" + redzdr + "','" + redjsdrq + "','" + dtgetdatainfo.Rows[0]["TA_GJSTATE"].ToString().Trim() + "'," + (-(CommonFun.ComTryInt(dtgetdatainfo.Rows[0]["TA_NUM"].ToString().Trim()))) + "," + (-(CommonFun.ComTryDecimal(dtgetdatainfo.Rows[0]["TA_MONEY"].ToString().Trim()))) + "," + (-(CommonFun.ComTryDecimal(dtgetdatainfo.Rows[0]["TA_WGHT"].ToString().Trim()))) + "," + (-(CommonFun.ComTryDecimal(dtgetdatainfo.Rows[0]["TA_AMOUNT"].ToString().Trim()))) + "," + (-(CommonFun.ComTryDecimal(dtgetdatainfo.Rows[0]["TA_TOTALWGHT"].ToString().Trim()))) + ",'" + dtgetdatainfo.Rows[0]["TA_NOTE"].ToString().Trim() + "','" + dtgetdatainfo.Rows[0]["TA_TOTALNOTE"].ToString().Trim() + "','" + dtgetdatainfo.Rows[0]["TA_XTSTATE"].ToString().Trim() + "')";
                            list_sql.Add(sqltext);
                        }
                    }
                }
                DBCallCommon.ExecuteTrans(list_sql);
                Response.Redirect("PM_Xie_IntoAccounts.aspx?&orderno=" + redjsdh);
            }
        }
    }
}
