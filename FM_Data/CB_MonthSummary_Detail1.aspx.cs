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
using System.Collections.Generic;
using ZCZJ_DPF;
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using System.Runtime.InteropServices;
using NPOI.HSSF.UserModel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;

namespace ZCZJ_DPF.FM_Data
{
    public partial class CB_MonthSummary_Detail1 : BasicPage
    {
        string startyearmonth = "";
        string endyearmonth = "";

        //double qjgzhj = 0;
        
        double qjjjfyhj = 0;
        double qjjgyzfyhj = 0;
        double qjzjrgfhj = 0;


        double qjbzjhj = 0;
        double qjhclhj = 0;
        double qjhsjshj = 0;
        double qjzjhj = 0;
        double qjdjhj = 0;
        double qjzchj = 0;
        double qjwgjhj = 0;
        double qjyqtlhj = 0;
        double qjqtclhj = 0;
        double qjclhj = 0;


        double qjgdzzfyhj = 0;
        double qjkbzzfyhj = 0;
        double qjzzfyhj = 0;
        double qjwxfyhj = 0;
        double qjcnfbhj = 0;
        double qjyfhj = 0;
        double qjfjcbhj = 0;
        double qjqthj = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["startyearmonth"] != null && Request.QueryString["endyearmonth"] != null)
            {
                startyearmonth = Request.QueryString["startyearmonth"];
                endyearmonth = Request.QueryString["endyearmonth"];
            }
            if (!IsPostBack)
            {
                tbstart.Text = startyearmonth.ToString();
                tbend.Text = endyearmonth.ToString();

                ViewState["sqltext"] = "(PMS_YEAR+'-'+PMS_MONTH)>='" + startyearmonth + "' and (PMS_YEAR+'-'+PMS_MONTH)<='" + endyearmonth + "'";
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }
            if (IsPostBack)
            {
                this.InitVar();
            }
        }

        #region  分页
        PagerQueryParamGroupBy pager_org = new PagerQueryParamGroupBy();

        // 初始化分布信息
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        // 分页初始化
        private void InitPager()
        {
            pager_org.TableName = "(select * from VIEW_FM_AYTJ as a left join (select sum(cast(DIF_DIFMONEY as decimal(12,2))) as DIF_DIFMONEY,DIF_TSAID,DIF_YEAR,DIF_MONTH from TBFM_DIF group by DIF_TSAID,DIF_YEAR,DIF_MONTH)b on (a.PMS_TSAID=b.DIF_TSAID and a.AYTJ_YEARMONTH=b.DIF_YEAR+'-'+b.DIF_MONTH) left join (select sum(cast(DIFYF_DIFMONEY as decimal(12,2))) as DIFYF_DIFMONEY,DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF group by DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH)c on (a.PMS_TSAID=c.DIFYF_TSAID and a.AYTJ_YEARMONTH=c.DIFYF_YEAR+'-'+c.DIFYF_MONTH))t";
            pager_org.PrimaryKey = "PMS_TSAID";
            pager_org.ShowFields = "PMS_TSAID as QJTSAID,(cast(sum(ISNULL(AYTJ_JJFY,0)) as decimal(12,2))) as QJJJFY,(cast(sum(ISNULL(AYTJ_JGYZFY,0)) as decimal(12,2))) as QJJGYZFY,(cast((sum(ISNULL(AYTJ_JJFY,0))+sum(ISNULL(AYTJ_JGYZFY,0))) as decimal(12,2))) as QJZJRGFXJ,(cast(sum(ISNULL(PMS_01_01,0)) as decimal(12,2))) as QJBZJ,(cast(sum(ISNULL(PMS_01_05,0)) as decimal(12,2))) as QJHCL,(cast(sum(ISNULL(PMS_01_07,0)) as decimal(12,2))) as QJHSJS,(cast(sum(ISNULL(PMS_01_08,0)) as decimal(12,2))) as QJZJ,(cast(sum(ISNULL(PMS_01_09,0)) as decimal(12,2))) as QJDJ,(cast(sum(ISNULL(PMS_01_10,0)) as decimal(12,2))) as QJZC,(cast(sum(ISNULL(PMS_01_15,0)) as decimal(12,2))) as QJYQTL,(cast(sum(ISNULL(PMS_01_11,0)) as decimal(12,2))) as QJWGJ,(cast(sum(PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_06+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) as decimal(12,2))) as QJQTCL,(cast(sum(PMS_01_01+PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_05+PMS_01_06+PMS_01_07+PMS_01_08+PMS_01_09+PMS_01_10+PMS_01_11+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_15+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) as decimal(12,2))) as QJCLXJ,(cast(sum(ISNULL(AYTJ_GDZZFY,0)) as decimal(12,2))) as QJGDZZFY,(cast(sum(ISNULL(AYTJ_KBZZFY,0)) as decimal(12,2))) as QJKBZZFY,(cast((sum(ISNULL(AYTJ_GDZZFY,0))+sum(ISNULL(AYTJ_KBZZFY,0))) as decimal(12,2))) as QJZZFYXJ,(cast(sum(ISNULL((isnull(AYTJ_WXFY,0)+isnull(DIF_DIFMONEY,0)),0)) as decimal(12,2))) as QJWXFY,cast(isnull(sum(ISNULL(AYTJ_CNFB,0)),0) as decimal(12,2)) as QJCNFB,(cast(sum(ISNULL((isnull(AYTJ_YF,0)+isnull(DIFYF_DIFMONEY,0)),0)) as decimal(12,2))) as QJYF,(cast(sum(ISNULL(AYTJ_FJCB,0)) as decimal(12,2))) as QJFJCB,(cast(sum(ISNULL(AYTJ_QT,0)) as decimal(12,2))) as QJQT";//sum(ISNULL(AYTJ_GZ,0)) as QJGZ,
            pager_org.OrderField = "PMS_TSAID";
            pager_org.StrWhere = ViewState["sqltext"].ToString();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 50;
            pager_org.GroupField = "PMS_TSAID";
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamGroupBy(pager_org);
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

            
        }
        #endregion

        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //System.Web.UI.WebControls.Label lbqjgz = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjgz");
                System.Web.UI.WebControls.Label lbqjjjfy = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjjjfy");
                System.Web.UI.WebControls.Label lbqjjgyzfy = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjjgyzfy");
                System.Web.UI.WebControls.Label lbqjzjrgf = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjzjrgf");

                System.Web.UI.WebControls.Label lbqjbzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjbzj");
                System.Web.UI.WebControls.Label lbqjhcl = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjhcl");
                System.Web.UI.WebControls.Label lbqjhsjs = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjhsjs");
                System.Web.UI.WebControls.Label lbqjzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjzj");
                System.Web.UI.WebControls.Label lbqjdj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjdj");
                System.Web.UI.WebControls.Label lbqjzc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjzc");
                System.Web.UI.WebControls.Label lbqjwgj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjwgj");

                System.Web.UI.WebControls.Label lbqjyqtl = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjyqtl");

                System.Web.UI.WebControls.Label lbqjqtcl = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjqtcl");
                System.Web.UI.WebControls.Label lbqjclxj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjclxj");
                System.Web.UI.WebControls.Label lbqjgdzzfy = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjgdzzfy");
                System.Web.UI.WebControls.Label lbqjkbzzfy = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjkbzzfy");
                System.Web.UI.WebControls.Label lbqjzzfyxj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjzzfyxj");

                System.Web.UI.WebControls.Label lbqjwxfy = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjwxfy");
                System.Web.UI.WebControls.Label lbqjcnfb = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjcnfb");
                System.Web.UI.WebControls.Label lbqjyf = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjyf");
                System.Web.UI.WebControls.Label lbqjfjcb = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjfjcb");
                System.Web.UI.WebControls.Label lbqjqt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjqt");
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {




                string sqlhj = "select (cast(isnull(CAST(sum(AYTJ_JJFY) AS FLOAT),0) as decimal(12,2))) as QJJJFYHJ,(cast(isnull(CAST(sum(AYTJ_JGYZFY) AS FLOAT),0) as decimal(12,2))) as QJJGYZFYHJ,(cast(isnull(CAST(sum(isnull(AYTJ_JJFY,0)+isnull(AYTJ_JGYZFY,0)) AS FLOAT),0) as decimal(12,2))) as QJZJRGFHJ,(cast(isnull(CAST(sum(PMS_01_01) AS FLOAT),0) as decimal(12,2))) as QJBZJHJ,(cast(isnull(CAST(sum(PMS_01_05) AS FLOAT),0) as decimal(12,2))) as QJHCLHJ,(cast(isnull(CAST(sum(PMS_01_07) AS FLOAT),0) as decimal(12,2))) as QJHSJSHJ,(cast(isnull(CAST(sum(PMS_01_08) AS FLOAT),0) as decimal(12,2))) as QJZJHJ,(cast(isnull(CAST(sum(PMS_01_09) AS FLOAT),0) as decimal(12,2))) as QJDJHJ,(cast(isnull(CAST(sum(PMS_01_10) AS FLOAT),0) as decimal(12,2))) as QJZCHJ,(cast(isnull(CAST(sum(PMS_01_11) AS FLOAT),0) as decimal(12,2))) as QJWGJHJ,(cast(isnull(CAST(sum(PMS_01_15) AS FLOAT),0) as decimal(12,2))) as QJYQTLHJ,(cast(isnull(CAST(sum(PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_06+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) AS FLOAT),0) as decimal(12,2))) as QJQTCLHJ,(cast(isnull(CAST(sum(PMS_01_01+PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_05+PMS_01_06+PMS_01_07+PMS_01_08+PMS_01_09+PMS_01_10+PMS_01_11+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_15+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) AS FLOAT),0) as decimal(12,2))) as QJCLHJ,(cast(isnull(CAST(sum(AYTJ_GDZZFY) AS FLOAT),0) as decimal(12,2))) as QJGDZZFYHJ,(cast(isnull(CAST(sum(AYTJ_KBZZFY) AS FLOAT),0) as decimal(12,2))) as QJKBZZFYHJ,(cast((isnull(CAST(sum(AYTJ_GDZZFY) AS FLOAT),0)+isnull(CAST(sum(AYTJ_KBZZFY) AS FLOAT),0)) as decimal(12,2))) as QJZZFYHJ,(cast(isnull(CAST(sum(isnull(AYTJ_WXFY,0)+isnull(DIF_DIFMONEY,0)) AS FLOAT),0) as decimal(12,2))) as QJWXFYHJ,(cast(isnull(CAST(sum(AYTJ_CNFB) AS FLOAT),0) as decimal(12,2))) as QJCNFBHJ,(cast(isnull(CAST(sum(isnull(AYTJ_YF,0)+isnull(DIFYF_DIFMONEY,0)) AS FLOAT),0) as decimal(12,2))) as QJYFHJ,(cast(isnull(CAST(sum(AYTJ_FJCB) AS FLOAT),0) as decimal(12,2))) as QJFJCBHJ,(cast(isnull(CAST(sum(AYTJ_QT) AS FLOAT),0) as decimal(12,2))) as QJQTHJ from (select * from VIEW_FM_AYTJ as a left join (select sum(cast(DIF_DIFMONEY as decimal(12,2))) as DIF_DIFMONEY,DIF_TSAID,DIF_YEAR,DIF_MONTH from TBFM_DIF group by DIF_TSAID,DIF_YEAR,DIF_MONTH)b on (a.PMS_TSAID=b.DIF_TSAID and a.AYTJ_YEARMONTH=b.DIF_YEAR+'-'+b.DIF_MONTH) left join (select sum(cast(DIFYF_DIFMONEY as decimal(12,2))) as DIFYF_DIFMONEY,DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF group by DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH)c on (a.PMS_TSAID=c.DIFYF_TSAID and a.AYTJ_YEARMONTH=c.DIFYF_YEAR+'-'+c.DIFYF_MONTH))t where " + ViewState["sqltext"].ToString();//isnull(CAST(sum(AYTJ_GZ) AS FLOAT),0) as QJGZHJ,

                SqlDataReader drhj = DBCallCommon.GetDRUsingSqlText(sqlhj);

                if (drhj.Read())
                {

                    //qjgzhj = Convert.ToDouble(drhj["QJGZHJ"]);
                    qjjjfyhj = Convert.ToDouble(drhj["QJJJFYHJ"]);
                    qjjgyzfyhj = Convert.ToDouble(drhj["QJJGYZFYHJ"]);
                    qjzjrgfhj = Convert.ToDouble(drhj["QJZJRGFHJ"]);

                    qjbzjhj = Convert.ToDouble(drhj["QJBZJHJ"]);
                    qjhclhj = Convert.ToDouble(drhj["QJHCLHJ"]);
                    qjhsjshj = Convert.ToDouble(drhj["QJHSJSHJ"]);
                    qjzjhj = Convert.ToDouble(drhj["QJZJHJ"]);
                    qjdjhj = Convert.ToDouble(drhj["QJDJHJ"]);
                    qjzchj = Convert.ToDouble(drhj["QJZCHJ"]);
                    qjwgjhj = Convert.ToDouble(drhj["QJWGJHJ"]);

                    qjyqtlhj = Convert.ToDouble(drhj["QJYQTLHJ"]);
                    qjqtclhj = Convert.ToDouble(drhj["QJQTCLHJ"]);
                    qjclhj = Convert.ToDouble(drhj["QJCLHJ"]);
                    qjgdzzfyhj = Convert.ToDouble(drhj["QJGDZZFYHJ"]);
                    qjkbzzfyhj = Convert.ToDouble(drhj["QJKBZZFYHJ"]);
                    qjzzfyhj = Convert.ToDouble(drhj["QJZZFYHJ"]);

                    qjwxfyhj = Convert.ToDouble(drhj["QJWXFYHJ"]);
                    qjcnfbhj = Convert.ToDouble(drhj["QJCNFBHJ"]);
                    qjyfhj = Convert.ToDouble(drhj["QJYFHJ"]);
                    qjfjcbhj = Convert.ToDouble(drhj["QJFJCBHJ"]);
                    qjqthj = Convert.ToDouble(drhj["QJQTHJ"]);
                }
                drhj.Close();



                //System.Web.UI.WebControls.Label lbqjgzhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjgzhj");
                System.Web.UI.WebControls.Label lbqjjjfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjjjfyhj");
                System.Web.UI.WebControls.Label lbqjjgyzfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjjgyzfyhj");
                System.Web.UI.WebControls.Label lbqjzjrgfhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjzjrgfhj");

                System.Web.UI.WebControls.Label lbqjbzjhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjbzjhj");
                System.Web.UI.WebControls.Label lbqjhclhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjhclhj");
                System.Web.UI.WebControls.Label lbqjhsjshj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjhsjshj");
                System.Web.UI.WebControls.Label lbqjzjhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjzjhj");
                System.Web.UI.WebControls.Label lbqjdjhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjdjhj");
                System.Web.UI.WebControls.Label lbqjzchj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjzchj");
                System.Web.UI.WebControls.Label lbqjwgjhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjwgjhj");
                System.Web.UI.WebControls.Label lbqjyqtlhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjyqtlhj");

                System.Web.UI.WebControls.Label lbqjqtclhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjqtclhj");
                System.Web.UI.WebControls.Label lbqjclhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjclhj");

                System.Web.UI.WebControls.Label lbqjgdzzfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjgdzzfyhj");
                System.Web.UI.WebControls.Label lbqjkbzzfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjkbzzfyhj");
                System.Web.UI.WebControls.Label lbqjzzfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjzzfyhj");

                System.Web.UI.WebControls.Label lbqjwxfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjwxfyhj");
                System.Web.UI.WebControls.Label lbqjcnfbhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjcnfbhj");
                System.Web.UI.WebControls.Label lbqjyfhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjyfhj");
                System.Web.UI.WebControls.Label lbqjfjcbhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjfjcbhj");
                System.Web.UI.WebControls.Label lbqjqthj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqjqthj");

                //lbqjgzhj.Text = qjgzhj.ToString();
                lbqjjjfyhj.Text = qjjjfyhj.ToString();
                lbqjjgyzfyhj.Text = qjjgyzfyhj.ToString();
                lbqjzjrgfhj.Text = qjzjrgfhj.ToString();

                lbqjbzjhj.Text = qjbzjhj.ToString();
                lbqjhclhj.Text = qjhclhj.ToString();
                lbqjhsjshj.Text = qjhsjshj.ToString();
                lbqjzjhj.Text = qjzjhj.ToString();
                lbqjdjhj.Text = qjdjhj.ToString();
                lbqjzchj.Text = qjzchj.ToString();
                lbqjwgjhj.Text = qjwgjhj.ToString();

                lbqjyqtlhj.Text = qjyqtlhj.ToString();

                lbqjqtclhj.Text = qjqtclhj.ToString();
                lbqjclhj.Text = qjclhj.ToString();

                lbqjgdzzfyhj.Text = qjgdzzfyhj.ToString();
                lbqjkbzzfyhj.Text = qjkbzzfyhj.ToString();
                lbqjzzfyhj.Text = qjzzfyhj.ToString();

                lbqjwxfyhj.Text = qjwxfyhj.ToString();
                lbqjcnfbhj.Text = qjcnfbhj.ToString();
                lbqjyfhj.Text = qjyfhj.ToString();
                lbqjfjcbhj.Text = qjfjcbhj.ToString();
                lbqjqthj.Text = qjqthj.ToString();

            }
        }






        #region 导出数据


        private int ifselect()
        {
            int flag = 0;
            int i = 0;//是否选择数据
            foreach (RepeaterItem Reitem in rptProNumCost.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("cbxSelect") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                }
            }
            if (i == 0)//未选择数据
            {
                flag = 1;
            }
            else
            {
                flag = 0;
            }
            return flag;
        }
        protected void btn_export_Click(object sender, EventArgs e)
        {
            int flag = ifselect();
            if (flag == 0)//判断是否有勾选框被勾选
            {
                string rwhdc = "";
                foreach (RepeaterItem Reitem in rptProNumCost.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("cbxSelect") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx.Checked)
                    {
                        rwhdc += "'" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("lbqjrwh")).Text.ToString() + "'" + ",";
                    }
                }
                rwhdc = rwhdc.Substring(0, rwhdc.LastIndexOf(",")).ToString();
                string sqltext = "";
                sqltext = "select PMS_TSAID as QJTSAID,cast(sum(ISNULL(AYTJ_JJFY,0)) as decimal(12,2)) as QJJJFY,cast(sum(ISNULL(AYTJ_JGYZFY,0)) as decimal(12,2)) as QJJGYZFY,cast((sum(ISNULL(AYTJ_JJFY,0))+sum(ISNULL(AYTJ_JGYZFY,0)))  as decimal(12,2)) as QJZJRGFXJ,cast(sum(ISNULL(PMS_01_01,0)) as decimal(12,2)) as QJBZJ,cast(sum(ISNULL(PMS_01_05,0)) as decimal(12,2)) as QJHCL,cast(sum(ISNULL(PMS_01_07,0)) as decimal(12,2)) as QJHSJS,cast(sum(ISNULL(PMS_01_08,0)) as decimal(12,2)) as QJZJ,cast(sum(ISNULL(PMS_01_09,0)) as decimal(12,2)) as QJDJ,cast(sum(ISNULL(PMS_01_10,0)) as decimal(12,2)) as QJZC,cast(sum(ISNULL(PMS_01_11,0)) as decimal(12,2)) as QJWGJ,cast(sum(ISNULL(PMS_01_15,0)) as decimal(12,2)) as QJYQTL,cast(sum(PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_06+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) as decimal(12,2)) as QJQTCL,cast(sum(PMS_01_01+PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_05+PMS_01_06+PMS_01_07+PMS_01_08+PMS_01_09+PMS_01_10+PMS_01_11+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_15+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) as decimal(12,2)) as QJCLXJ,cast(sum(ISNULL(AYTJ_GDZZFY,0)) as decimal(12,2)) as QJGDZZFY,cast(sum(ISNULL(AYTJ_KBZZFY,0)) as decimal(12,2)) as QJKBZZFY,cast((sum(ISNULL(AYTJ_GDZZFY,0))+sum(ISNULL(AYTJ_KBZZFY,0))) as decimal(12,2)) as QJZZFYXJ,cast(sum(ISNULL((isnull(AYTJ_WXFY,0)+isnull(DIF_DIFMONEY,0)),0)) as decimal(12,2)) as QJWXFY,cast(sum(ISNULL(AYTJ_CNFB,0)) as decimal(12,2)) as QJCNFB,cast(sum(ISNULL((isnull(AYTJ_YF,0)+isnull(DIFYF_DIFMONEY,0)),0)) as decimal(12,2)) as QJYF,cast(sum(ISNULL(AYTJ_FJCB,0)) as decimal(12,2)) as QJFJCB,cast(sum(ISNULL(AYTJ_QT,0)) as decimal(12,2)) as QJQT from (select * from VIEW_FM_AYTJ as a left join (select sum(cast(DIF_DIFMONEY as decimal(12,2))) as DIF_DIFMONEY,DIF_TSAID,DIF_YEAR,DIF_MONTH from TBFM_DIF group by DIF_TSAID,DIF_YEAR,DIF_MONTH)b on (a.PMS_TSAID=b.DIF_TSAID and a.AYTJ_YEARMONTH=b.DIF_YEAR+'-'+b.DIF_MONTH) left join (select sum(cast(DIFYF_DIFMONEY as decimal(12,2))) as DIFYF_DIFMONEY,DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF group by DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH)c on (a.PMS_TSAID=c.DIFYF_TSAID and a.AYTJ_YEARMONTH=c.DIFYF_YEAR+'-'+c.DIFYF_MONTH))t where (PMS_YEAR+'-'+PMS_MONTH)>='" + startyearmonth + "' and (PMS_YEAR+'-'+PMS_MONTH)<='" + endyearmonth + "' and PMS_TSAID in(" + rwhdc + ") group by PMS_TSAID";//sum(ISNULL(AYTJ_GZ,0)) as QJGZ,
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                ExportDataItem(dt);
            }
            else if (flag == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要导出的数据！！！');", true);
            }
        }


        private void ExportDataItem(System.Data.DataTable dt)
        {

            string filename = "任务号按期间统计" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("任务按期间统计.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2); 
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                    
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
        #endregion


        #region 批量导出

        protected void btn_plexport_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext = "select PMS_TSAID as QJTSAID,cast(sum(ISNULL(AYTJ_JJFY,0)) as decimal(12,2)) as QJJJFY,cast(sum(ISNULL(AYTJ_JGYZFY,0)) as decimal(12,2)) as QJJGYZFY,cast((sum(ISNULL(AYTJ_JJFY,0))+sum(ISNULL(AYTJ_JGYZFY,0))) as decimal(12,2)) as QJZJRGFXJ,cast(sum(ISNULL(PMS_01_01,0)) as decimal(12,2)) as QJBZJ,cast(sum(ISNULL(PMS_01_05,0)) as decimal(12,2)) as QJHCL,cast(sum(ISNULL(PMS_01_07,0)) as decimal(12,2)) as QJHSJS,cast(sum(ISNULL(PMS_01_08,0)) as decimal(12,2)) as QJZJ,cast(sum(ISNULL(PMS_01_09,0)) as decimal(12,2)) as QJDJ,cast(sum(ISNULL(PMS_01_10,0)) as decimal(12,2)) as QJZC,cast(sum(ISNULL(PMS_01_11,0)) as decimal(12,2)) as QJWGJ,cast(sum(ISNULL(PMS_01_15,0)) as decimal(12,2)) as QJYQTL,cast(sum(PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_06+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) as decimal(12,2)) as QJQTCL,cast(sum(PMS_01_01+PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_05+PMS_01_06+PMS_01_07+PMS_01_08+PMS_01_09+PMS_01_10+PMS_01_11+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_15+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) as decimal(12,2)) as QJCLXJ,cast(sum(ISNULL(AYTJ_GDZZFY,0)) as decimal(12,2)) as QJGDZZFY,cast(sum(ISNULL(AYTJ_KBZZFY,0)) as decimal(12,2)) as QJKBZZFY,cast((sum(ISNULL(AYTJ_GDZZFY,0))+sum(ISNULL(AYTJ_KBZZFY,0))) as decimal(12,2)) as QJZZFYXJ,cast(sum(ISNULL((isnull(AYTJ_WXFY,0)+isnull(DIF_DIFMONEY,0)),0)) as decimal(12,2)) as QJWXFY,cast(sum(ISNULL(AYTJ_CNFB,0)) as decimal(12,2)) as QJCNFB,cast(sum(ISNULL((isnull(AYTJ_YF,0)+isnull(DIFYF_DIFMONEY,0)),0)) as decimal(12,2)) as QJYF,cast(sum(ISNULL(AYTJ_FJCB,0)) as decimal(12,2)) as QJFJCB,cast(sum(ISNULL(AYTJ_QT,0)) as decimal(12,2)) as QJQT from (select * from VIEW_FM_AYTJ as a left join (select sum(cast(DIF_DIFMONEY as decimal(12,2))) as DIF_DIFMONEY,DIF_TSAID,DIF_YEAR,DIF_MONTH from TBFM_DIF group by DIF_TSAID,DIF_YEAR,DIF_MONTH)b on (a.PMS_TSAID=b.DIF_TSAID and a.AYTJ_YEARMONTH=b.DIF_YEAR+'-'+b.DIF_MONTH) left join (select sum(cast(DIFYF_DIFMONEY as decimal(12,2))) as DIFYF_DIFMONEY,DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF group by DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH)c on (a.PMS_TSAID=c.DIFYF_TSAID and a.AYTJ_YEARMONTH=c.DIFYF_YEAR+'-'+c.DIFYF_MONTH))t where (PMS_YEAR+'-'+PMS_MONTH)>='" + startyearmonth + "' and (PMS_YEAR+'-'+PMS_MONTH)<='" + endyearmonth + "' group by PMS_TSAID";//sum(ISNULL(AYTJ_GZ,0)) as QJGZ,
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string filename = "任务号按期间统计"+DateTime.Now.ToString("yyyyMMdd")+".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("任务按期间统计.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }

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
        #endregion
        private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet) //输出Excel文件并退出
        {
            try
            {

                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Workbooks.Close();
                m_xlApp.Quit();
                m_xlApp.Application.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);
                wksheet = null;
                workbook = null;
                m_xlApp = null;
                GC.Collect();
                //下载
                System.IO.FileInfo path = new System.IO.FileInfo(filename);
                //同步，异步都支持
                HttpResponse contextResponse = HttpContext.Current.Response;
                contextResponse.Redirect(string.Format("~/FM_Data/ExportFile/{0}", path.Name), false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
        