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
    public partial class CM_ProNum_ProfitLoss : BasicPage
    {
        double zjrghj = 0;
        double bzjhj = 0;
        double hclhj = 0;
        double hsjshj = 0;
        double zjhj = 0;
        double djhj = 0;
        double zchj = 0;
        double wgjhj = 0;
        double yqtlhj = 0;
        double qtclhj = 0;
        double clhj = 0;
        double zzfyhj = 0;
        double wxfyhj = 0;
        double cnfbhj = 0;
        double yfhj = 0;
        double fjcbhj = 0;
        double cbhj = 0;
        string conId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UCPaging1.CurrentPage = 1;
                if (Request["htcode"]!=null)
                {
                    conId = Request["htcode"].ToString();
                    txthth.Text = conId;
                }
                this.InitVar();
                this.bindGrid();
            }
            if (IsPostBack)
            {
                this.InitVar();
            }
        }
        #region  分页
        PagerQueryParam pager_org = new PagerQueryParam();

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
            pager_org.TableName = "(select * from (select PMS_TSAID,TSA_PJID,CM_PROJ,cast(sum(isnull(PMS_01_01,0)) as decimal(12,2)) as XJPMS_01_01,cast(sum(isnull(PMS_01_02,0)) as decimal(12,2)) as XJPMS_01_02,cast(sum(isnull(PMS_01_03,0)) as decimal(12,2)) as XJPMS_01_03,cast(sum(isnull(PMS_01_04,0)) as decimal(12,2)) as XJPMS_01_04,cast(sum(isnull(PMS_01_05,0)) as decimal(12,2)) as XJPMS_01_05,cast(sum(isnull(PMS_01_06,0)) as decimal(12,2)) as XJPMS_01_06,cast(sum(isnull(PMS_01_07,0)) as decimal(12,2)) as XJPMS_01_07,cast(sum(isnull(PMS_01_08,0)) as decimal(12,2)) as XJPMS_01_08,cast(sum(isnull(PMS_01_09,0)) as decimal(12,2)) as XJPMS_01_09,cast(sum(isnull(PMS_01_10,0)) as decimal(12,2)) as XJPMS_01_10,cast(sum(isnull(PMS_01_11,0)) as decimal(12,2)) as XJPMS_01_11,cast(sum(isnull(PMS_01_12,0)) as decimal(12,2)) as XJPMS_01_12,cast(sum(isnull(PMS_01_13,0)) as decimal(12,2)) as XJPMS_01_13,cast(sum(isnull(PMS_01_14,0)) as decimal(12,2)) as XJPMS_01_14,cast(sum(isnull(PMS_01_15,0)) as decimal(12,2)) as XJPMS_01_15,cast(sum(isnull(PMS_01_16,0)) as decimal(12,2)) as XJPMS_01_16,cast(sum(isnull(PMS_01_17,0)) as decimal(12,2)) as XJPMS_01_17,cast(sum(isnull(PMS_01_18,0)) as decimal(12,2)) as XJPMS_01_18,cast(sum(isnull(PMS_02_01,0)) as decimal(12,2)) as XJPMS_02_01,cast(sum(isnull(PMS_02_02,0)) as decimal(12,2)) as XJPMS_02_02,cast(sum(isnull(PMS_02_03,0)) as decimal(12,2)) as XJPMS_02_03,cast(sum(isnull(PMS_02_04,0)) as decimal(12,2)) as XJPMS_02_04,cast(sum(isnull(PMS_02_05,0)) as decimal(12,2)) as XJPMS_02_05,cast(sum(isnull(PMS_02_06,0)) as decimal(12,2)) as XJPMS_02_06,cast(sum(isnull(PMS_02_07,0)) as decimal(12,2)) as XJPMS_02_07,cast(sum(isnull(PMS_02_08,0)) as decimal(12,2)) as XJPMS_02_08,cast(sum(isnull(PMS_02_09,0)) as decimal(12,2)) as XJPMS_02_09,isnull(sum(isnull(AYTJ_GZ,0)),0) as AYTJ_GZXJ,isnull(sum(isnull(AYTJ_QT,0)),0) as AYTJ_QTXJ,sum(isnull(AYTJ_JJFY,0)) as AYTJ_JJFYXJ,sum(isnull(AYTJ_JGYZFY,0)) as AYTJ_JGYZFYXJ,sum(isnull(AYTJ_GDZZFY,0)) as AYTJ_GDZZFYXJ,sum(isnull(AYTJ_KBZZFY,0)) as AYTJ_KBZZFYXJ,sum(isnull(AYTJ_WXFY,0)+isnull(DIF_DIFMONEY,0)) as AYTJ_WXFYXJ,sum(isnull(AYTJ_CNFB,0)) as AYTJ_CNFBXJ,sum(isnull(AYTJ_YF,0)+isnull(DIFYF_DIFMONEY,0)) as AYTJ_YFXJ,sum(isnull(AYTJ_FJCB,0)) as AYTJ_FJCBXJ from (select * from VIEW_FM_AYTJ as a left join (select sum(cast(DIF_DIFMONEY as decimal(12,2))) as DIF_DIFMONEY,DIF_TSAID,DIF_YEAR,DIF_MONTH from TBFM_DIF group by DIF_TSAID,DIF_YEAR,DIF_MONTH)b on (a.PMS_TSAID=b.DIF_TSAID and a.AYTJ_YEARMONTH=b.DIF_YEAR+'-'+b.DIF_MONTH) left join (select sum(cast(DIFYF_DIFMONEY as decimal(12,2))) as DIFYF_DIFMONEY,DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF group by DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH)c on (a.PMS_TSAID=c.DIFYF_TSAID and a.AYTJ_YEARMONTH=c.DIFYF_YEAR+'-'+c.DIFYF_MONTH))s group by PMS_TSAID,TSA_PJID,CM_PROJ) as a left join (select TASK_ID,CWCB_STATE,CWCB_HSDATE from TBCB_BMCONFIRM) as b on a.PMS_TSAID=b.TASK_ID)t left join (select TaskId,sum(kpmoney) as kp_money_total from (select CM_KAIPIAO_DETAIL.TaskId as TaskId,cast(CM_KAIPIAO_DETAIL.kpmoney as float) as kpmoney  from  CM_KAIPIAO left join CM_KAIPIAO_DETAIL on CM_KAIPIAO.KP_TaskID=CM_KAIPIAO_DETAIL.cId where CM_KAIPIAO.KP_KPNUMBER is not null)h  group by TaskId)g on t.PMS_TSAID=g.taskid";
            pager_org.PrimaryKey = "PMS_TSAID";
            pager_org.ShowFields = "PMS_TSAID,TSA_PJID,CM_PROJ,cast((isnull(AYTJ_JJFYXJ,0)+isnull(AYTJ_JGYZFYXJ,0)) as decimal(12,2)) as RWHCB_ZJRG,isnull(XJPMS_01_11,0) as RWHCB_WGJ,isnull(XJPMS_01_07,0) as RWHCB_HSJS,isnull(XJPMS_01_05,0) as RWHCB_HCL,isnull(XJPMS_01_08,0) as RWHCB_ZJ,isnull(XJPMS_01_09,0) as RWHCB_DJ,isnull(XJPMS_01_10,0) as RWHCB_ZC,isnull(XJPMS_01_01,0) as RWHCB_BZJ,isnull(XJPMS_01_15,0) as RWHCB_YQTL,(isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0)) as RWHCB_QTCL,(isnull(XJPMS_01_01,0)+isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_05,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_07,0)+isnull(XJPMS_01_08,0)+isnull(XJPMS_01_09,0)+isnull(XJPMS_01_10,0)+isnull(XJPMS_01_11,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_15,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0)) as RWHCB_CL,cast((AYTJ_GDZZFYXJ+AYTJ_KBZZFYXJ) as decimal(12,2)) as RWHCB_ZZFY,cast(AYTJ_WXFYXJ as decimal(12,2)) as RWHCB_WXFY,cast(AYTJ_CNFBXJ as decimal(12,2)) as RWHCB_CNFB,cast(AYTJ_YFXJ as decimal(12,2)) as RWHCB_YF,cast(AYTJ_FJCBXJ as decimal(12,2)) as RWHCB_FJCB,cast((isnull(AYTJ_JJFYXJ,0)+isnull(AYTJ_JGYZFYXJ,0)+isnull(XJPMS_01_01,0)+isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_05,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_07,0)+isnull(XJPMS_01_08,0)+isnull(XJPMS_01_09,0)+isnull(XJPMS_01_10,0)+isnull(XJPMS_01_11,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_15,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0)+isnull(AYTJ_GDZZFYXJ,0)+isnull(AYTJ_KBZZFYXJ,0)+isnull(AYTJ_WXFYXJ,0)+isnull(AYTJ_CNFBXJ,0)+isnull(AYTJ_YFXJ,0)+isnull(AYTJ_FJCBXJ,0)) as decimal(12,2)) as RWHCB_CBZJ,case when CWCB_STATE='1' then '是' else '否' end as CWCB_STATE,CWCB_HSDATE,kp_money_total";
            pager_org.OrderField = "PMS_TSAID";
            pager_org.StrWhere = strstring();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 25;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_org);
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



        private string strstring()
        {
            string sqltext = "1=1";
            if (txtrwh.Text != "")
            {
                sqltext += " and PMS_TSAID like '%" + txtrwh.Text.ToString().Trim() + "%'";
            }
            if (txthth.Text != "")
            {
                sqltext += "and TSA_PJID like '%" + txthth.Text.ToString().Trim() + "%'";
            }
            if (txtxmmc.Text != "")
            {
                sqltext += " and CM_PROJ like '%" + txtxmmc.Text.ToString().Trim() + "%'";
            }
            if (hesuanbz.SelectedIndex != 0)
            {
                sqltext += " and CWCB_STATE='" + hesuanbz.SelectedValue.ToString().Trim() + "'";
            }
            if (tb_CXstarttime.Text.Trim()!="" && tb_CXendtime.Text.Trim()!="")
            {
                sqltext += " and CWCB_HSDATE>='" + tb_CXstarttime.Text.Trim() + "' and CWCB_HSDATE<='" + tb_CXendtime.Text.Trim() + "'";
            }
            return sqltext;
        }


        #endregion


        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Label lbzjrg = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzjrg");
                System.Web.UI.WebControls.Label lbwgj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbwgj");
                System.Web.UI.WebControls.Label lbhsjs = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbhsjs");
                System.Web.UI.WebControls.Label lbhcl = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbhcl");
                System.Web.UI.WebControls.Label lbzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzj");
                System.Web.UI.WebControls.Label lbdj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbdj");
                System.Web.UI.WebControls.Label lbzc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzc");
                System.Web.UI.WebControls.Label lbbzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbbzj");
                System.Web.UI.WebControls.Label lbyqtl = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbyqtl");
                System.Web.UI.WebControls.Label lbqtcl = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqtcl");
                System.Web.UI.WebControls.Label lbclxj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbclxj");
                System.Web.UI.WebControls.Label lbzzfy = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzzfy");
                System.Web.UI.WebControls.Label lbwxfy = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbwxfy");
                System.Web.UI.WebControls.Label lbcnfb = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbcnfb");
                System.Web.UI.WebControls.Label lbyf = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbyf");
                System.Web.UI.WebControls.Label lbfjcb = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbfjcb");
                System.Web.UI.WebControls.Label lbcbzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbcbzj");
                System.Web.UI.WebControls.Label lbhsbz = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbhsbz");
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select cast(isnull(sum((isnull(AYTJ_JJFYXJ,0)+isnull(AYTJ_JGYZFYXJ,0))),0) as decimal(12,2)) as RWHCB_ZJRGHJ,isnull(sum(isnull(XJPMS_01_11,0)),0) as RWHCB_WGJHJ,isnull(sum(isnull(XJPMS_01_07,0)),0) as RWHCB_HSJSHJ,isnull(sum(isnull(XJPMS_01_05,0)),0) as RWHCB_HCLHJ,isnull(sum(isnull(XJPMS_01_08,0)),0) as RWHCB_ZJHJ,isnull(sum(isnull(XJPMS_01_09,0)),0) as RWHCB_DJHJ,isnull(sum(isnull(XJPMS_01_10,0)),0) as RWHCB_ZCHJ,isnull(sum(isnull(XJPMS_01_01,0)),0) as RWHCB_BZJHJ,isnull(sum(isnull(XJPMS_01_15,0)),0) as RWHCB_YQTLHJ,isnull(sum(isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0)),0) as RWHCB_QTCLHJ,isnull(sum(isnull(XJPMS_01_01,0)+isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_05,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_07,0)+isnull(XJPMS_01_08,0)+isnull(XJPMS_01_09,0)+isnull(XJPMS_01_10,0)+isnull(XJPMS_01_11,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_15,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0)),0) as RWHCB_CLHJ,cast(isnull(sum(isnull(AYTJ_GDZZFYXJ,0)+isnull(AYTJ_KBZZFYXJ,0)),0) as decimal(12,2)) as RWHCB_ZZFYHJ,cast(isnull(sum(isnull(AYTJ_WXFYXJ,0)),0) as decimal(12,2)) as RWHCB_WXFYHJ,cast(isnull(sum(isnull(AYTJ_CNFBXJ,0)),0) as decimal(12,2)) as RWHCB_CNFBHJ,cast(isnull(sum(isnull(AYTJ_YFXJ,0)),0) as decimal(12,2)) as RWHCB_YFHJ,cast(isnull(sum(isnull(AYTJ_FJCBXJ,0)),0) as decimal(12,2)) as RWHCB_FJCBHJ,cast(isnull(sum(isnull(AYTJ_JJFYXJ,0)+isnull(AYTJ_JGYZFYXJ,0)+isnull(XJPMS_01_01,0)+isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_05,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_07,0)+isnull(XJPMS_01_08,0)+isnull(XJPMS_01_09,0)+isnull(XJPMS_01_10,0)+isnull(XJPMS_01_11,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_15,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0)+isnull(AYTJ_GDZZFYXJ,0)+isnull(AYTJ_KBZZFYXJ,0)+isnull(AYTJ_WXFYXJ,0)+isnull(AYTJ_CNFBXJ,0)+isnull(AYTJ_YFXJ,0)+isnull(AYTJ_FJCBXJ,0)),0) as decimal(12,2)) as RWHCB_CBZJHJ from (select * from (select PMS_TSAID,TSA_PJID,CM_PROJ,cast(sum(isnull(PMS_01_01,0)) as decimal(12,2)) as XJPMS_01_01,cast(sum(isnull(PMS_01_02,0)) as decimal(12,2)) as XJPMS_01_02,cast(sum(isnull(PMS_01_03,0)) as decimal(12,2)) as XJPMS_01_03,cast(sum(isnull(PMS_01_04,0)) as decimal(12,2)) as XJPMS_01_04,cast(sum(isnull(PMS_01_05,0)) as decimal(12,2)) as XJPMS_01_05,cast(sum(isnull(PMS_01_06,0)) as decimal(12,2)) as XJPMS_01_06,cast(sum(isnull(PMS_01_07,0)) as decimal(12,2)) as XJPMS_01_07,cast(sum(isnull(PMS_01_08,0)) as decimal(12,2)) as XJPMS_01_08,cast(sum(isnull(PMS_01_09,0)) as decimal(12,2)) as XJPMS_01_09,cast(sum(isnull(PMS_01_10,0)) as decimal(12,2)) as XJPMS_01_10,cast(sum(isnull(PMS_01_11,0)) as decimal(12,2)) as XJPMS_01_11,cast(sum(isnull(PMS_01_12,0)) as decimal(12,2)) as XJPMS_01_12,cast(sum(isnull(PMS_01_13,0)) as decimal(12,2)) as XJPMS_01_13,cast(sum(isnull(PMS_01_14,0)) as decimal(12,2)) as XJPMS_01_14,cast(sum(isnull(PMS_01_15,0)) as decimal(12,2)) as XJPMS_01_15,cast(sum(isnull(PMS_01_16,0)) as decimal(12,2)) as XJPMS_01_16,cast(sum(isnull(PMS_01_17,0)) as decimal(12,2)) as XJPMS_01_17,cast(sum(isnull(PMS_01_18,0)) as decimal(12,2)) as XJPMS_01_18,cast(sum(isnull(PMS_02_01,0)) as decimal(12,2)) as XJPMS_02_01,cast(sum(isnull(PMS_02_02,0)) as decimal(12,2)) as XJPMS_02_02,cast(sum(isnull(PMS_02_03,0)) as decimal(12,2)) as XJPMS_02_03,cast(sum(isnull(PMS_02_04,0)) as decimal(12,2)) as XJPMS_02_04,cast(sum(isnull(PMS_02_05,0)) as decimal(12,2)) as XJPMS_02_05,cast(sum(isnull(PMS_02_06,0)) as decimal(12,2)) as XJPMS_02_06,cast(sum(isnull(PMS_02_07,0)) as decimal(12,2)) as XJPMS_02_07,cast(sum(isnull(PMS_02_08,0)) as decimal(12,2)) as XJPMS_02_08,cast(sum(isnull(PMS_02_09,0)) as decimal(12,2)) as XJPMS_02_09,isnull(sum(isnull(AYTJ_GZ,0)),0) as AYTJ_GZXJ,isnull(sum(isnull(AYTJ_QT,0)),0) as AYTJ_QTXJ,sum(isnull(AYTJ_JJFY,0)) as AYTJ_JJFYXJ,sum(isnull(AYTJ_JGYZFY,0)) as AYTJ_JGYZFYXJ,sum(isnull(AYTJ_GDZZFY,0)) as AYTJ_GDZZFYXJ,sum(isnull(AYTJ_KBZZFY,0)) as AYTJ_KBZZFYXJ,sum(isnull(AYTJ_WXFY,0)+isnull(DIF_DIFMONEY,0)) as AYTJ_WXFYXJ,sum(isnull(AYTJ_CNFB,0)) as AYTJ_CNFBXJ,sum(isnull(AYTJ_YF,0)+isnull(DIFYF_DIFMONEY,0)) as AYTJ_YFXJ,sum(isnull(AYTJ_FJCB,0)) as AYTJ_FJCBXJ from (select * from VIEW_FM_AYTJ as a left join (select sum(cast(DIF_DIFMONEY as decimal(12,2))) as DIF_DIFMONEY,DIF_TSAID,DIF_YEAR,DIF_MONTH from TBFM_DIF group by DIF_TSAID,DIF_YEAR,DIF_MONTH)b on (a.PMS_TSAID=b.DIF_TSAID and a.AYTJ_YEARMONTH=b.DIF_YEAR+'-'+b.DIF_MONTH) left join (select sum(cast(DIFYF_DIFMONEY as decimal(12,2))) as DIFYF_DIFMONEY,DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF group by DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH)c on (a.PMS_TSAID=c.DIFYF_TSAID and a.AYTJ_YEARMONTH=c.DIFYF_YEAR+'-'+c.DIFYF_MONTH))s group by PMS_TSAID,TSA_PJID,CM_PROJ) as a left join (select TASK_ID,CWCB_STATE,CWCB_HSDATE from TBCB_BMCONFIRM) as b on a.PMS_TSAID=b.TASK_ID)t where " + strstring();
                SqlDataReader drhj = DBCallCommon.GetDRUsingSqlText(sqlhj);
                if (drhj.Read())
                {
                    zjrghj = Convert.ToDouble(drhj["RWHCB_ZJRGHJ"]);
                    bzjhj = Convert.ToDouble(drhj["RWHCB_BZJHJ"]);
                    hclhj = Convert.ToDouble(drhj["RWHCB_HCLHJ"]);
                    hsjshj = Convert.ToDouble(drhj["RWHCB_HSJSHJ"]);
                    zjhj = Convert.ToDouble(drhj["RWHCB_ZJHJ"]);
                    djhj = Convert.ToDouble(drhj["RWHCB_DJHJ"]);
                    zchj = Convert.ToDouble(drhj["RWHCB_ZCHJ"]);
                    wgjhj = Convert.ToDouble(drhj["RWHCB_WGJHJ"]);
                    yqtlhj = Convert.ToDouble(drhj["RWHCB_YQTLHJ"]);
                    qtclhj = Convert.ToDouble(drhj["RWHCB_QTCLHJ"]);
                    clhj = Convert.ToDouble(drhj["RWHCB_CLHJ"]);
                    zzfyhj = Convert.ToDouble(drhj["RWHCB_ZZFYHJ"]);
                    wxfyhj = Convert.ToDouble(drhj["RWHCB_WXFYHJ"]);
                    cnfbhj = Convert.ToDouble(drhj["RWHCB_CNFBHJ"]);
                    yfhj = Convert.ToDouble(drhj["RWHCB_YFHJ"]);
                    fjcbhj = Convert.ToDouble(drhj["RWHCB_FJCBHJ"]);
                    cbhj = Convert.ToDouble(drhj["RWHCB_CBZJHJ"]);
                }
                drhj.Close();

                System.Web.UI.WebControls.Label lbzjrghj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzjrghj");
                System.Web.UI.WebControls.Label lbwgjhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbwgjhj");
                System.Web.UI.WebControls.Label lbhsjshj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbhsjshj");
                System.Web.UI.WebControls.Label lbhclhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbhclhj");
                System.Web.UI.WebControls.Label lbzjhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzjhj");
                System.Web.UI.WebControls.Label lbdjhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbdjhj");
                System.Web.UI.WebControls.Label lbzchj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzchj");
                System.Web.UI.WebControls.Label lbbzjhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbbzjhj");
                System.Web.UI.WebControls.Label lbyqtlhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbyqtlhj");
                System.Web.UI.WebControls.Label lbqtclhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqtclhj");
                System.Web.UI.WebControls.Label lbclhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbclhj");
                System.Web.UI.WebControls.Label lbzzfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzzfyhj");
                System.Web.UI.WebControls.Label lbwxfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbwxfyhj");
                System.Web.UI.WebControls.Label lbcnfbhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbcnfbhj");
                System.Web.UI.WebControls.Label lbyfhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbyfhj");
                System.Web.UI.WebControls.Label lbfjcbhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbfjcbhj");
                System.Web.UI.WebControls.Label lbcbhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbcbhj");

                lbzjrghj.Text = zjrghj.ToString();
                lbwgjhj.Text = wgjhj.ToString();
                lbhsjshj.Text = hsjshj.ToString();
                lbhclhj.Text = hclhj.ToString();
                lbzjhj.Text = zjhj.ToString();
                lbdjhj.Text = djhj.ToString();
                lbzchj.Text = zchj.ToString();
                lbbzjhj.Text = bzjhj.ToString();
                lbyqtlhj.Text = yqtlhj.ToString();
                lbqtclhj.Text = qtclhj.ToString();
                lbclhj.Text = clhj.ToString();
                lbzzfyhj.Text = zzfyhj.ToString();
                lbwxfyhj.Text = wxfyhj.ToString();
                lbcnfbhj.Text = cnfbhj.ToString();
                lbyfhj.Text = yfhj.ToString();
                lbfjcbhj.Text = fjcbhj.ToString();
                lbcbhj.Text = cbhj.ToString();
            }
        }
        protected void btnCx_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }
        protected void hesuanbz_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
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
                        rwhdc += "'" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("lbrwh")).Text.ToString() + "'" + ",";
                    }
                }
                rwhdc = rwhdc.Substring(0, rwhdc.LastIndexOf(",")).ToString();
                string sqltext = "";
                sqltext = "select PMS_TSAID,TSA_PJID,CM_PROJ,cast((isnull(AYTJ_JJFYXJ,0)+isnull(AYTJ_JGYZFYXJ,0)) as decimal(12,2)) as RWHCB_ZJRG,isnull(XJPMS_01_11,0) as RWHCB_WGJ,isnull(XJPMS_01_07,0) as RWHCB_HSJS,isnull(XJPMS_01_05,0) as RWHCB_HCL,isnull(XJPMS_01_08,0) as RWHCB_ZJ,isnull(XJPMS_01_09,0) as RWHCB_DJ,isnull(XJPMS_01_10,0) as RWHCB_ZC,isnull(XJPMS_01_01,0) as RWHCB_BZJ,isnull(XJPMS_01_15,0) as RWHCB_YQTL,(isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0)) as RWHCB_QTCL,(isnull(XJPMS_01_01,0)+isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_05,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_07,0)+isnull(XJPMS_01_08,0)+isnull(XJPMS_01_09,0)+isnull(XJPMS_01_10,0)+isnull(XJPMS_01_11,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_15,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0)) as RWHCB_CL,cast((AYTJ_GDZZFYXJ+AYTJ_KBZZFYXJ) as decimal(12,2)) as RWHCB_ZZFY,cast(AYTJ_WXFYXJ as decimal(12,2)) as RWHCB_WXFY,cast(AYTJ_CNFBXJ as decimal(12,2)) as RWHCB_CNFB,cast(AYTJ_YFXJ as decimal(12,2)) as RWHCB_YF,cast(AYTJ_FJCBXJ as decimal(12,2)) as RWHCB_FJCB,cast((isnull(AYTJ_JJFYXJ,0)+isnull(AYTJ_JGYZFYXJ,0)+isnull(XJPMS_01_01,0)+isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_05,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_07,0)+isnull(XJPMS_01_08,0)+isnull(XJPMS_01_09,0)+isnull(XJPMS_01_10,0)+isnull(XJPMS_01_11,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_15,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0)+isnull(AYTJ_GDZZFYXJ,0)+isnull(AYTJ_KBZZFYXJ,0)+isnull(AYTJ_WXFYXJ,0)+isnull(AYTJ_CNFBXJ,0)+isnull(AYTJ_YFXJ,0)+isnull(AYTJ_FJCBXJ,0)) as decimal(12,2)) as RWHCB_CBZJ,kp_money_total,case when CWCB_STATE='1' then '是' else '否' end as CWCB_STATE,CWCB_HSDATE from (select * from (select PMS_TSAID,TSA_PJID,CM_PROJ,cast(sum(isnull(PMS_01_01,0)) as decimal(12,2)) as XJPMS_01_01,cast(sum(isnull(PMS_01_02,0)) as decimal(12,2)) as XJPMS_01_02,cast(sum(isnull(PMS_01_03,0)) as decimal(12,2)) as XJPMS_01_03,cast(sum(isnull(PMS_01_04,0)) as decimal(12,2)) as XJPMS_01_04,cast(sum(isnull(PMS_01_05,0)) as decimal(12,2)) as XJPMS_01_05,cast(sum(isnull(PMS_01_06,0)) as decimal(12,2)) as XJPMS_01_06,cast(sum(isnull(PMS_01_07,0)) as decimal(12,2)) as XJPMS_01_07,cast(sum(isnull(PMS_01_08,0)) as decimal(12,2)) as XJPMS_01_08,cast(sum(isnull(PMS_01_09,0)) as decimal(12,2)) as XJPMS_01_09,cast(sum(isnull(PMS_01_10,0)) as decimal(12,2)) as XJPMS_01_10,cast(sum(isnull(PMS_01_11,0)) as decimal(12,2)) as XJPMS_01_11,cast(sum(isnull(PMS_01_12,0)) as decimal(12,2)) as XJPMS_01_12,cast(sum(isnull(PMS_01_13,0)) as decimal(12,2)) as XJPMS_01_13,cast(sum(isnull(PMS_01_14,0)) as decimal(12,2)) as XJPMS_01_14,cast(sum(isnull(PMS_01_15,0)) as decimal(12,2)) as XJPMS_01_15,cast(sum(isnull(PMS_01_16,0)) as decimal(12,2)) as XJPMS_01_16,cast(sum(isnull(PMS_01_17,0)) as decimal(12,2)) as XJPMS_01_17,cast(sum(isnull(PMS_01_18,0)) as decimal(12,2)) as XJPMS_01_18,cast(sum(isnull(PMS_02_01,0)) as decimal(12,2)) as XJPMS_02_01,cast(sum(isnull(PMS_02_02,0)) as decimal(12,2)) as XJPMS_02_02,cast(sum(isnull(PMS_02_03,0)) as decimal(12,2)) as XJPMS_02_03,cast(sum(isnull(PMS_02_04,0)) as decimal(12,2)) as XJPMS_02_04,cast(sum(isnull(PMS_02_05,0)) as decimal(12,2)) as XJPMS_02_05,cast(sum(isnull(PMS_02_06,0)) as decimal(12,2)) as XJPMS_02_06,cast(sum(isnull(PMS_02_07,0)) as decimal(12,2)) as XJPMS_02_07,cast(sum(isnull(PMS_02_08,0)) as decimal(12,2)) as XJPMS_02_08,cast(sum(isnull(PMS_02_09,0)) as decimal(12,2)) as XJPMS_02_09,isnull(sum(isnull(AYTJ_GZ,0)),0) as AYTJ_GZXJ,isnull(sum(isnull(AYTJ_QT,0)),0) as AYTJ_QTXJ,sum(isnull(AYTJ_JJFY,0)) as AYTJ_JJFYXJ,sum(isnull(AYTJ_JGYZFY,0)) as AYTJ_JGYZFYXJ,sum(isnull(AYTJ_GDZZFY,0)) as AYTJ_GDZZFYXJ,sum(isnull(AYTJ_KBZZFY,0)) as AYTJ_KBZZFYXJ,sum(isnull(AYTJ_WXFY,0)+isnull(DIF_DIFMONEY,0)) as AYTJ_WXFYXJ,sum(isnull(AYTJ_CNFB,0)) as AYTJ_CNFBXJ,sum(isnull(AYTJ_YF,0)+isnull(DIFYF_DIFMONEY,0)) as AYTJ_YFXJ,sum(isnull(AYTJ_FJCB,0)) as AYTJ_FJCBXJ from (select * from VIEW_FM_AYTJ as a left join (select sum(cast(DIF_DIFMONEY as decimal(12,2))) as DIF_DIFMONEY,DIF_TSAID,DIF_YEAR,DIF_MONTH from TBFM_DIF group by DIF_TSAID,DIF_YEAR,DIF_MONTH)b on (a.PMS_TSAID=b.DIF_TSAID and a.AYTJ_YEARMONTH=b.DIF_YEAR+'-'+b.DIF_MONTH) left join (select sum(cast(DIFYF_DIFMONEY as decimal(12,2))) as DIFYF_DIFMONEY,DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF group by DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH)c on (a.PMS_TSAID=c.DIFYF_TSAID and a.AYTJ_YEARMONTH=c.DIFYF_YEAR+'-'+c.DIFYF_MONTH))s group by PMS_TSAID,TSA_PJID,CM_PROJ) as a left join (select TASK_ID,CWCB_STATE,CWCB_HSDATE from TBCB_BMCONFIRM) as b on a.PMS_TSAID=b.TASK_ID)t left join (select TaskId,sum(kpmoney) as kp_money_total from (select CM_KAIPIAO_DETAIL.TaskId as TaskId,cast(CM_KAIPIAO_DETAIL.kpmoney as float) as kpmoney  from  CM_KAIPIAO left join CM_KAIPIAO_DETAIL on CM_KAIPIAO.KP_TaskID=CM_KAIPIAO_DETAIL.cId where CM_KAIPIAO.KP_KPNUMBER is not null)h  group by TaskId)g on t.PMS_TSAID=g.taskid where PMS_TSAID in (" + rwhdc + ")";
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

            string filename = "任务号成本统计" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("任务号成本统计.xls")))
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
            sqltext = "select PMS_TSAID,TSA_PJID,CM_PROJ,cast((isnull(AYTJ_JJFYXJ,0)+isnull(AYTJ_JGYZFYXJ,0)) as decimal(12,2)) as RWHCB_ZJRG,isnull(XJPMS_01_11,0) as RWHCB_WGJ,isnull(XJPMS_01_07,0) as RWHCB_HSJS,isnull(XJPMS_01_05,0) as RWHCB_HCL,isnull(XJPMS_01_08,0) as RWHCB_ZJ,isnull(XJPMS_01_09,0) as RWHCB_DJ,isnull(XJPMS_01_10,0) as RWHCB_ZC,isnull(XJPMS_01_01,0) as RWHCB_BZJ,isnull(XJPMS_01_15,0) as RWHCB_YQTL,(isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0)) as RWHCB_QTCL,(isnull(XJPMS_01_01,0)+isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_05,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_07,0)+isnull(XJPMS_01_08,0)+isnull(XJPMS_01_09,0)+isnull(XJPMS_01_10,0)+isnull(XJPMS_01_11,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_15,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0)) as RWHCB_CL,cast((AYTJ_GDZZFYXJ+AYTJ_KBZZFYXJ) as decimal(12,2)) as RWHCB_ZZFY,cast(AYTJ_WXFYXJ as decimal(12,2)) as RWHCB_WXFY,cast(AYTJ_CNFBXJ as decimal(12,2)) as RWHCB_CNFB,cast(AYTJ_YFXJ as decimal(12,2)) as RWHCB_YF,cast(AYTJ_FJCBXJ as decimal(12,2)) as RWHCB_FJCB,cast((isnull(AYTJ_JJFYXJ,0)+isnull(AYTJ_JGYZFYXJ,0)+isnull(XJPMS_01_01,0)+isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_05,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_07,0)+isnull(XJPMS_01_08,0)+isnull(XJPMS_01_09,0)+isnull(XJPMS_01_10,0)+isnull(XJPMS_01_11,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_15,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0)+isnull(AYTJ_GDZZFYXJ,0)+isnull(AYTJ_KBZZFYXJ,0)+isnull(AYTJ_WXFYXJ,0)+isnull(AYTJ_CNFBXJ,0)+isnull(AYTJ_YFXJ,0)+isnull(AYTJ_FJCBXJ,0)) as decimal(12,2)) as RWHCB_CBZJ,kp_money_total,case when CWCB_STATE='1' then '是' else '否' end as CWCB_STATE,CWCB_HSDATE from (select * from (select PMS_TSAID,TSA_PJID,CM_PROJ,cast(sum(isnull(PMS_01_01,0)) as decimal(12,2)) as XJPMS_01_01,cast(sum(isnull(PMS_01_02,0)) as decimal(12,2)) as XJPMS_01_02,cast(sum(isnull(PMS_01_03,0)) as decimal(12,2)) as XJPMS_01_03,cast(sum(isnull(PMS_01_04,0)) as decimal(12,2)) as XJPMS_01_04,cast(sum(isnull(PMS_01_05,0)) as decimal(12,2)) as XJPMS_01_05,cast(sum(isnull(PMS_01_06,0)) as decimal(12,2)) as XJPMS_01_06,cast(sum(isnull(PMS_01_07,0)) as decimal(12,2)) as XJPMS_01_07,cast(sum(isnull(PMS_01_08,0)) as decimal(12,2)) as XJPMS_01_08,cast(sum(isnull(PMS_01_09,0)) as decimal(12,2)) as XJPMS_01_09,cast(sum(isnull(PMS_01_10,0)) as decimal(12,2)) as XJPMS_01_10,cast(sum(isnull(PMS_01_11,0)) as decimal(12,2)) as XJPMS_01_11,cast(sum(isnull(PMS_01_12,0)) as decimal(12,2)) as XJPMS_01_12,cast(sum(isnull(PMS_01_13,0)) as decimal(12,2)) as XJPMS_01_13,cast(sum(isnull(PMS_01_14,0)) as decimal(12,2)) as XJPMS_01_14,cast(sum(isnull(PMS_01_15,0)) as decimal(12,2)) as XJPMS_01_15,cast(sum(isnull(PMS_01_16,0)) as decimal(12,2)) as XJPMS_01_16,cast(sum(isnull(PMS_01_17,0)) as decimal(12,2)) as XJPMS_01_17,cast(sum(isnull(PMS_01_18,0)) as decimal(12,2)) as XJPMS_01_18,cast(sum(isnull(PMS_02_01,0)) as decimal(12,2)) as XJPMS_02_01,cast(sum(isnull(PMS_02_02,0)) as decimal(12,2)) as XJPMS_02_02,cast(sum(isnull(PMS_02_03,0)) as decimal(12,2)) as XJPMS_02_03,cast(sum(isnull(PMS_02_04,0)) as decimal(12,2)) as XJPMS_02_04,cast(sum(isnull(PMS_02_05,0)) as decimal(12,2)) as XJPMS_02_05,cast(sum(isnull(PMS_02_06,0)) as decimal(12,2)) as XJPMS_02_06,cast(sum(isnull(PMS_02_07,0)) as decimal(12,2)) as XJPMS_02_07,cast(sum(isnull(PMS_02_08,0)) as decimal(12,2)) as XJPMS_02_08,cast(sum(isnull(PMS_02_09,0)) as decimal(12,2)) as XJPMS_02_09,isnull(sum(isnull(AYTJ_GZ,0)),0) as AYTJ_GZXJ,isnull(sum(isnull(AYTJ_QT,0)),0) as AYTJ_QTXJ,sum(isnull(AYTJ_JJFY,0)) as AYTJ_JJFYXJ,sum(isnull(AYTJ_JGYZFY,0)) as AYTJ_JGYZFYXJ,sum(isnull(AYTJ_GDZZFY,0)) as AYTJ_GDZZFYXJ,sum(isnull(AYTJ_KBZZFY,0)) as AYTJ_KBZZFYXJ,sum(isnull(AYTJ_WXFY,0)+isnull(DIF_DIFMONEY,0)) as AYTJ_WXFYXJ,sum(isnull(AYTJ_CNFB,0)) as AYTJ_CNFBXJ,sum(isnull(AYTJ_YF,0)+isnull(DIFYF_DIFMONEY,0)) as AYTJ_YFXJ,sum(isnull(AYTJ_FJCB,0)) as AYTJ_FJCBXJ from (select * from VIEW_FM_AYTJ as a left join (select sum(cast(DIF_DIFMONEY as decimal(12,2))) as DIF_DIFMONEY,DIF_TSAID,DIF_YEAR,DIF_MONTH from TBFM_DIF group by DIF_TSAID,DIF_YEAR,DIF_MONTH)b on (a.PMS_TSAID=b.DIF_TSAID and a.AYTJ_YEARMONTH=b.DIF_YEAR+'-'+b.DIF_MONTH) left join (select sum(cast(DIFYF_DIFMONEY as decimal(12,2))) as DIFYF_DIFMONEY,DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF group by DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH)c on (a.PMS_TSAID=c.DIFYF_TSAID and a.AYTJ_YEARMONTH=c.DIFYF_YEAR+'-'+c.DIFYF_MONTH))s group by PMS_TSAID,TSA_PJID,CM_PROJ) as a left join (select TASK_ID,CWCB_STATE,CWCB_HSDATE from TBCB_BMCONFIRM) as b on a.PMS_TSAID=b.TASK_ID)t left join (select TaskId,sum(kpmoney) as kp_money_total from (select CM_KAIPIAO_DETAIL.TaskId as TaskId,cast(CM_KAIPIAO_DETAIL.kpmoney as float) as kpmoney  from  CM_KAIPIAO left join CM_KAIPIAO_DETAIL on CM_KAIPIAO.KP_TaskID=CM_KAIPIAO_DETAIL.cId where CM_KAIPIAO.KP_KPNUMBER is not null)h  group by TaskId)g on t.PMS_TSAID=g.taskid where " + strstring();
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string filename = "任务号成本统计" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("任务号成本统计.xls")))
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
    }
}
