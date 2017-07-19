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

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_AllBill : BasicPage
    {

        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btn_search_Click(null, null);
            }
            this.InitVar();

            //CheckUser(ControlFinder);
        }



        /// <summary>
        /// 绑定其他发票记录
        /// </summary>
        private void GetSqlText()
        {
            //**********************            
            StringBuilder str = new StringBuilder();
            str.Append("1=1 ");

            //项目名称           
            str.Append(" and PJNAME like '%" + txt_PJNAME.Text.Trim() + "%'");

            //合同号
            str.Append(" and HTBH like '%" + txt_HTBH.Text.Trim() + "%'");



            //开票单位
            str.Append(" and KPDW like '%" + txt_KPDW.Text.Trim() + "%'");

            //开票时间
            string KPstartTime = kpsta_time.Text.Trim() == "" ? DateTime.Now.AddYears(-100).ToShortDateString() : kpsta_time.Text.Trim();
            string KPendTime = kpend_time.Text.Trim() == "" ? DateTime.Now.AddYears(100).ToShortDateString() : kpend_time.Text.Trim();
            str.Append(" and KPRQ>='" + KPstartTime + "' AND KPRQ<='" + KPendTime + "'");






            ViewState["sqltext"] = str.ToString();

        }


        protected void btn_search_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.GetSqlText();
            this.InitVar();
            this.bindGrid();
            this.Select_record();
        }


        protected void btnExport_Click(object sender, EventArgs e)
        {

            //MAINID, , , , , , , , , PZ, JBR, PJNAME, ENGNAME, , PCON_FORM, , 
            string sqlText = "select PZH,HTBH,PCON_YZHTH,KPDW,PJNAME,ENGNAME,KPJE,SL,BR_DANWEI,BR_WEIGHT,KPRQ,SPRQ,FPDH from View_CM_ALLBILL order by HTBH desc  where " + ViewState["sqltext"].ToString();
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            string filename = "开票明细.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();

            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("开票明细.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet = wk.GetSheetAt(0);


                if (dt.Rows.Count == 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
                    return;
                }


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet.CreateRow(i + 1);
                 
                    for (int j = 0; j < 12; j++)
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

        //重置
        protected void Btn_Reset_Click(object sender, EventArgs e)
        {
            txt_HTBH.Text = "";
            txt_PJNAME.Text = "";
            txt_KPDW.Text = "";
            kpsta_time.Text = "";
            kpend_time.Text = "";
            spsta_time.Text = "";
            spend_time.Text = "";


            this.btn_search_Click(null, null);
        }
        //商务发票绑定、分页
        #region "数据查询，分页"
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
            pager.TableName = "View_CM_ALLBILL";
            pager.PrimaryKey = "MAINID";
            pager.ShowFields = "*";
            pager.OrderField = "HTBH";
            pager.StrWhere = ViewState["sqltext"].ToString();
            pager.OrderType = 1;
            pager.PageSize = 12;

        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, grvFPJL, UCPaging1, pal_NoData);
            if (pal_NoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
            //CheckUser(ControlFinder);
        }

        //显示筛选后的记录条数和合计金额
        private void Select_record()
        {
            string sqltext = "select KPJE from View_CM_ALLBILL where " + ViewState["sqltext"].ToString();
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            lb_select_num.Text = dt.Rows.Count.ToString();
            decimal tot_money = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tot_money += Convert.ToDecimal(dt.Rows[i]["KPJE"].ToString());
            }
            lb_select_money.Text = string.Format("{0:c}", tot_money);
        }
        #endregion

        //修改发票
        protected void Lbtn_Edit_OnClick(object sender, EventArgs e)
        {
            string[] split = ((LinkButton)sender).CommandArgument.ToString().Split(new Char[] { '|' });

            int type = split[0].Contains("BR") ? 0 : 1;
            string id = split[1];

            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "FPEdit(" + type + ",'" + id + "');", true);

        }

        ////行绑定事件
        //protected void grvFPJL_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowIndex >= 0)
        //    {
        //        e.Row.Attributes.Add("onclick", "RowClick(this)");

        //        e.Row.Attributes["style"] = "Cursor:pointer";

        //        Label kprq = (Label)e.Row.FindControl("lbl_kprq");
        //        if (DateTime.Compare(Convert.ToDateTime(kprq.Text.Trim()).AddDays(150), DateTime.Now) < 0)
        //        {
        //            e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
        //        }

        //    }
        //}

    }
}
