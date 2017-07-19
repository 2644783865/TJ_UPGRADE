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
    public partial class FM_HTHCEHNBFX : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
            pager_org.TableName = "(select PCON_CUSTMNAME,PCON_BCODE,PCON_YZHTH,PCON_ENGNAME,PCON_ENGTYPE,cast((cast(PCON_JINE as float)*10000)/1.17 as decimal(18,2)) as PCON_JINE,sum(cast((isnull(AYTJ_JJFYXJ,0)+isnull(AYTJ_JGYZFYXJ,0)) as decimal(12,2))) as RWHCB_ZJRG,sum(cast((AYTJ_GDZZFYXJ+AYTJ_KBZZFYXJ) as decimal(12,2))) as RWHCB_ZZFY,sum(cast(AYTJ_WXFYXJ as decimal(12,2))) as RWHCB_WXFY,sum(cast(AYTJ_CNFBXJ as decimal(12,2))) as RWHCB_CNFB,sum(cast(AYTJ_YFXJ as decimal(12,2))) as RWHCB_YF,sum(cast(AYTJ_FJCBXJ as decimal(12,2))) as RWHCB_FJCB,sum(cast((AYTJ_JJFYXJ+AYTJ_JGYZFYXJ+XJPMS_01_01+XJPMS_01_02+XJPMS_01_03+XJPMS_01_04+XJPMS_01_05+XJPMS_01_06+XJPMS_01_07+XJPMS_01_08+XJPMS_01_09+XJPMS_01_10+XJPMS_01_11+XJPMS_01_12+XJPMS_01_13+XJPMS_01_14+XJPMS_01_15+XJPMS_01_16+XJPMS_01_17+XJPMS_01_18+XJPMS_02_01+XJPMS_02_02+XJPMS_02_03+XJPMS_02_04+XJPMS_02_05+XJPMS_02_06+XJPMS_02_07+XJPMS_02_08+XJPMS_02_09+AYTJ_WXFYXJ+AYTJ_CNFBXJ+AYTJ_YFXJ+AYTJ_FJCBXJ) as decimal(12,2))) as RWHCB_CBZJ,sum(cast((isnull(AYTJ_GDZZFYXJ,0)+isnull(AYTJ_KBZZFYXJ,0)) as decimal(12,2))) as zzfyhj,kpZongMoney,kpstate from (select * from (select PMS_TSAID,cast(sum(PMS_01_01) as decimal(12,2)) as XJPMS_01_01,cast(sum(PMS_01_02) as decimal(12,2)) as XJPMS_01_02,cast(sum(PMS_01_03) as decimal(12,2)) as XJPMS_01_03,cast(sum(PMS_01_04) as decimal(12,2)) as XJPMS_01_04,cast(sum(PMS_01_05) as decimal(12,2)) as XJPMS_01_05,cast(sum(PMS_01_06) as decimal(12,2)) as XJPMS_01_06,cast(sum(PMS_01_07) as decimal(12,2)) as XJPMS_01_07,cast(sum(PMS_01_08) as decimal(12,2)) as XJPMS_01_08,cast(sum(PMS_01_09) as decimal(12,2)) as XJPMS_01_09,cast(sum(PMS_01_10) as decimal(12,2)) as XJPMS_01_10,cast(sum(PMS_01_11) as decimal(12,2)) as XJPMS_01_11,cast(sum(PMS_01_12) as decimal(12,2)) as XJPMS_01_12,cast(sum(PMS_01_13) as decimal(12,2)) as XJPMS_01_13,cast(sum(PMS_01_14) as decimal(12,2)) as XJPMS_01_14,cast(sum(PMS_01_15) as decimal(12,2)) as XJPMS_01_15,cast(sum(PMS_01_16) as decimal(12,2)) as XJPMS_01_16,cast(sum(PMS_01_17) as decimal(12,2)) as XJPMS_01_17,cast(sum(PMS_01_18) as decimal(12,2)) as XJPMS_01_18,cast(sum(PMS_02_01) as decimal(12,2)) as XJPMS_02_01,cast(sum(PMS_02_02) as decimal(12,2)) as XJPMS_02_02,cast(sum(PMS_02_03) as decimal(12,2)) as XJPMS_02_03,cast(sum(PMS_02_04) as decimal(12,2)) as XJPMS_02_04,cast(sum(PMS_02_05) as decimal(12,2)) as XJPMS_02_05,cast(sum(PMS_02_06) as decimal(12,2)) as XJPMS_02_06,cast(sum(PMS_02_07) as decimal(12,2)) as XJPMS_02_07,cast(sum(PMS_02_08) as decimal(12,2)) as XJPMS_02_08,cast(sum(PMS_02_09) as decimal(12,2)) as XJPMS_02_09,isnull(sum(isnull(AYTJ_GZ,0)),0) as AYTJ_GZXJ,isnull(sum(isnull(AYTJ_QT,0)),0) as AYTJ_QTXJ,sum(isnull(AYTJ_JJFY,0)) as AYTJ_JJFYXJ,sum(isnull(AYTJ_JGYZFY,0)) as AYTJ_JGYZFYXJ,sum(isnull(AYTJ_GDZZFY,0)) as AYTJ_GDZZFYXJ,sum(isnull(AYTJ_KBZZFY,0)) as AYTJ_KBZZFYXJ,sum(isnull(AYTJ_WXFY,0)+isnull(DIF_DIFMONEY,0)) as AYTJ_WXFYXJ,sum(isnull(AYTJ_CNFB,0)) as AYTJ_CNFBXJ,sum(isnull(AYTJ_YF,0)+isnull(DIFYF_DIFMONEY,0)) as AYTJ_YFXJ,sum(isnull(AYTJ_FJCB,0)) as AYTJ_FJCBXJ from View_FM_AYTJANDDIF group by PMS_TSAID) as a left join (select TASK_ID,CWCB_STATE,CWCB_HSDATE from TBCB_BMCONFIRM) as b on a.PMS_TSAID=b.TASK_ID)t left join (select distinct TSA_ID,CM_CONTR from View_CM_Task where CM_SPSTATUS='2')m on t.PMS_TSAID=m.TSA_ID left join  TBPM_CONPCHSINFO as c on m.CM_CONTR=c.PCON_BCODE left join (select b.conId as conid,sum(cast(b.kpmoney as float))*10000 as kpZongMoney,case when KP_SPSTATE='3' then '是' else '否' end as kpstate from CM_KAIPIAO as a left join dbo.CM_KAIPIAO_DETAIL as b on a.KP_TaskID=b.cId where KP_SPSTATE='3' group by b.conId,KP_SPSTATE)p on c.PCON_BCODE=p.conid group by PCON_CUSTMNAME,PCON_BCODE,PCON_YZHTH,PCON_ENGNAME,PCON_ENGTYPE,PCON_JINE,kpZongMoney,kpstate)s";
            pager_org.PrimaryKey = "PCON_BCODE";
            pager_org.ShowFields = "*,(RWHCB_CBZJ-RWHCB_ZJRG-RWHCB_WXFY-RWHCB_CNFB-RWHCB_YF-RWHCB_FJCB) as RWHCB_CL,(RWHCB_CBZJ+zzfyhj) as CBZJ,case when isnull(PCON_JINE,0)=0 then 0 else (PCON_JINE-RWHCB_CBZJ)*100/PCON_JINE end as maolilv1,case when isnull(PCON_JINE,0)=0 then 0 else (PCON_JINE-RWHCB_CBZJ-zzfyhj)*100/PCON_JINE end as maolilv2";
            pager_org.OrderField = "PCON_BCODE";
            pager_org.StrWhere = strstring();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 25;
            //其中视图View_FM_AYTJANDDIF为(select * from VIEW_FM_AYTJ as a left join (select sum(cast(DIF_DIFMONEY as decimal(12,2))) as DIF_DIFMONEY,DIF_TSAID,DIF_YEAR,DIF_MONTH from TBFM_DIF group by DIF_TSAID,DIF_YEAR,DIF_MONTH)b on (a.PMS_TSAID=b.DIF_TSAID and a.AYTJ_YEARMONTH=b.DIF_YEAR+'-'+b.DIF_MONTH) left join (select sum(cast(DIFYF_DIFMONEY as decimal(12,2))) as DIFYF_DIFMONEY,DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF group by DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH)c on (a.PMS_TSAID=c.DIFYF_TSAID and a.AYTJ_YEARMONTH=c.DIFYF_YEAR+'-'+c.DIFYF_MONTH))k，由于语句太长无法运行，所以作此更改
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
            if (txtyzname.Text.Trim() != "")
            {
                sqltext += " and PCON_CUSTMNAME like '%" + txtyzname.Text.Trim() + "%'";
            }
            if (txthtcode.Text.Trim() != "")
            {
                sqltext += " and PCON_BCODE like '%" + txthtcode.Text.Trim() + "%'";
            }
            if (txtyzhtcode.Text.Trim() != "")
            {
                sqltext += " and PCON_YZHTH like '%" + txtyzhtcode.Text.Trim() + "%'";
            }
            if (txtengname.Text.Trim() != "")
            {
                sqltext += " and PCON_ENGNAME like '%" + txtengname.Text.Trim() + "%'";
            }
            if (txtshebeiname.Text.Trim() != "")
            {
                sqltext += " and PCON_ENGTYPE like '%" + txtshebeiname.Text.Trim() + "%'";
            }

            if (radio_yikaip.Checked == true)
            {
                sqltext += " and kpstate='是'";
            }
            if (radio_weikaip.Checked == true)
            {
                sqltext += " and (kpstate is null or kpstate='' or kpstate='否')";
            }
            return sqltext;
        }


        #endregion


        protected void btnCx_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                if (canseeif.Checked == true)
                {
                    HtmlTableCell tdzjrgf = e.Item.FindControl("tdzjrgf") as HtmlTableCell;
                    HtmlTableCell tdzjclf = e.Item.FindControl("tdzjclf") as HtmlTableCell;
                    HtmlTableCell tdzzfy = e.Item.FindControl("tdzzfy") as HtmlTableCell;
                    HtmlTableCell tdwxfy = e.Item.FindControl("tdwxfy") as HtmlTableCell;
                    HtmlTableCell tdcnfb = e.Item.FindControl("tdcnfb") as HtmlTableCell;
                    HtmlTableCell tdyf = e.Item.FindControl("tdyf") as HtmlTableCell;
                    HtmlTableCell tdfjcb = e.Item.FindControl("tdfjcb") as HtmlTableCell;

                    tdzjrgf.Visible = false;
                    tdzjclf.Visible = false;
                    tdzzfy.Visible = false;
                    tdwxfy.Visible = false;
                    tdcnfb.Visible = false;
                    tdyf.Visible = false;
                    tdfjcb.Visible = false;
                }
            }


            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (canseeif.Checked == true)
                {
                    HtmlTableCell tdzjrg = e.Item.FindControl("tdzjrg") as HtmlTableCell;
                    HtmlTableCell tdcl = e.Item.FindControl("tdcl") as HtmlTableCell;
                    HtmlTableCell tdzzf = e.Item.FindControl("tdzzf") as HtmlTableCell;
                    HtmlTableCell tdwx = e.Item.FindControl("tdwx") as HtmlTableCell;
                    HtmlTableCell tdcnfb = e.Item.FindControl("tdcnfb") as HtmlTableCell;
                    HtmlTableCell tdyf = e.Item.FindControl("tdyf") as HtmlTableCell;
                    HtmlTableCell tdfjcb = e.Item.FindControl("tdfjcb") as HtmlTableCell;

                    tdzjrg.Visible = false;
                    tdcl.Visible = false;
                    tdzzf.Visible = false;
                    tdwx.Visible = false;
                    tdcnfb.Visible = false;
                    tdyf.Visible = false;
                    tdfjcb.Visible = false;
                }
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select sum(PCON_JINE) as xshtzje,sum(RWHCB_ZJRG) as zjrgfzj,sum(RWHCB_CL) as clfzj,sum(RWHCB_ZZFY) as zzfyzj,sum(RWHCB_WXFY) as wxfyzj,sum(RWHCB_CNFB) as cnfbzj,sum(RWHCB_YF) as yfzj,sum(RWHCB_FJCB) as fjcbzj,sum(RWHCB_CBZJ) as cbzje,sum(CBZJ) as cbtotal,sum(kpZongMoney) as kpzongjehj from (select PCON_CUSTMNAME,PCON_BCODE,PCON_YZHTH,PCON_ENGNAME,PCON_ENGTYPE,cast((cast(PCON_JINE as float)*10000)/1.17 as decimal(18,2)) as PCON_JINE,sum(cast((isnull(AYTJ_JJFYXJ,0)+isnull(AYTJ_JGYZFYXJ,0)) as decimal(12,2))) as RWHCB_ZJRG,sum((isnull(XJPMS_01_01,0)+isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_05,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_07,0)+isnull(XJPMS_01_08,0)+isnull(XJPMS_01_09,0)+isnull(XJPMS_01_10,0)+isnull(XJPMS_01_11,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_15,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0))) as RWHCB_CL,sum(cast((AYTJ_GDZZFYXJ+AYTJ_KBZZFYXJ) as decimal(12,2))) as RWHCB_ZZFY,sum(cast(AYTJ_WXFYXJ as decimal(12,2))) as RWHCB_WXFY,sum(cast(AYTJ_CNFBXJ as decimal(12,2))) as RWHCB_CNFB,sum(cast(AYTJ_YFXJ as decimal(12,2))) as RWHCB_YF,sum(cast(AYTJ_FJCBXJ as decimal(12,2))) as RWHCB_FJCB,sum(cast((isnull(AYTJ_JJFYXJ,0)+isnull(AYTJ_JGYZFYXJ,0)+isnull(XJPMS_01_01,0)+isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_05,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_07,0)+isnull(XJPMS_01_08,0)+isnull(XJPMS_01_09,0)+isnull(XJPMS_01_10,0)+isnull(XJPMS_01_11,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_15,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0)+isnull(AYTJ_WXFYXJ,0)+isnull(AYTJ_CNFBXJ,0)+isnull(AYTJ_YFXJ,0)+isnull(AYTJ_FJCBXJ,0)) as decimal(12,2))) as RWHCB_CBZJ,sum(cast((isnull(AYTJ_JJFYXJ,0)+isnull(AYTJ_JGYZFYXJ,0)+isnull(XJPMS_01_01,0)+isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_05,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_07,0)+isnull(XJPMS_01_08,0)+isnull(XJPMS_01_09,0)+isnull(XJPMS_01_10,0)+isnull(XJPMS_01_11,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_15,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0)+isnull(AYTJ_GDZZFYXJ,0)+isnull(AYTJ_KBZZFYXJ,0)+isnull(AYTJ_WXFYXJ,0)+isnull(AYTJ_CNFBXJ,0)+isnull(AYTJ_YFXJ,0)+isnull(AYTJ_FJCBXJ,0)) as decimal(12,2))) as CBZJ,kpZongMoney,kpstate from (select * from (select PMS_TSAID,cast(sum(isnull(PMS_01_01,0)) as decimal(12,2)) as XJPMS_01_01,cast(sum(isnull(PMS_01_02,0)) as decimal(12,2)) as XJPMS_01_02,cast(sum(isnull(PMS_01_03,0)) as decimal(12,2)) as XJPMS_01_03,cast(sum(isnull(PMS_01_04,0)) as decimal(12,2)) as XJPMS_01_04,cast(sum(isnull(PMS_01_05,0)) as decimal(12,2)) as XJPMS_01_05,cast(sum(isnull(PMS_01_06,0)) as decimal(12,2)) as XJPMS_01_06,cast(sum(isnull(PMS_01_07,0)) as decimal(12,2)) as XJPMS_01_07,cast(sum(isnull(PMS_01_08,0)) as decimal(12,2)) as XJPMS_01_08,cast(sum(isnull(PMS_01_09,0)) as decimal(12,2)) as XJPMS_01_09,cast(sum(isnull(PMS_01_10,0)) as decimal(12,2)) as XJPMS_01_10,cast(sum(isnull(PMS_01_11,0)) as decimal(12,2)) as XJPMS_01_11,cast(sum(isnull(PMS_01_12,0)) as decimal(12,2)) as XJPMS_01_12,cast(sum(isnull(PMS_01_13,0)) as decimal(12,2)) as XJPMS_01_13,cast(sum(isnull(PMS_01_14,0)) as decimal(12,2)) as XJPMS_01_14,cast(sum(isnull(PMS_01_15,0)) as decimal(12,2)) as XJPMS_01_15,cast(sum(isnull(PMS_01_16,0)) as decimal(12,2)) as XJPMS_01_16,cast(sum(isnull(PMS_01_17,0)) as decimal(12,2)) as XJPMS_01_17,cast(sum(isnull(PMS_01_18,0)) as decimal(12,2)) as XJPMS_01_18,cast(sum(isnull(PMS_02_01,0)) as decimal(12,2)) as XJPMS_02_01,cast(sum(isnull(PMS_02_02,0)) as decimal(12,2)) as XJPMS_02_02,cast(sum(isnull(PMS_02_03,0)) as decimal(12,2)) as XJPMS_02_03,cast(sum(isnull(PMS_02_04,0)) as decimal(12,2)) as XJPMS_02_04,cast(sum(isnull(PMS_02_05,0)) as decimal(12,2)) as XJPMS_02_05,cast(sum(isnull(PMS_02_06,0)) as decimal(12,2)) as XJPMS_02_06,cast(sum(isnull(PMS_02_07,0)) as decimal(12,2)) as XJPMS_02_07,cast(sum(isnull(PMS_02_08,0)) as decimal(12,2)) as XJPMS_02_08,cast(sum(isnull(PMS_02_09,0)) as decimal(12,2)) as XJPMS_02_09,isnull(sum(isnull(AYTJ_GZ,0)),0) as AYTJ_GZXJ,isnull(sum(isnull(AYTJ_QT,0)),0) as AYTJ_QTXJ,sum(isnull(AYTJ_JJFY,0)) as AYTJ_JJFYXJ,sum(isnull(AYTJ_JGYZFY,0)) as AYTJ_JGYZFYXJ,sum(isnull(AYTJ_GDZZFY,0)) as AYTJ_GDZZFYXJ,sum(isnull(AYTJ_KBZZFY,0)) as AYTJ_KBZZFYXJ,sum(isnull(AYTJ_WXFY,0)+isnull(DIF_DIFMONEY,0)) as AYTJ_WXFYXJ,sum(isnull(AYTJ_CNFB,0)) as AYTJ_CNFBXJ,sum(isnull(AYTJ_YF,0)+isnull(DIFYF_DIFMONEY,0)) as AYTJ_YFXJ,sum(isnull(AYTJ_FJCB,0)) as AYTJ_FJCBXJ from (select * from VIEW_FM_AYTJ as a left join (select sum(cast(DIF_DIFMONEY as decimal(12,2))) as DIF_DIFMONEY,DIF_TSAID,DIF_YEAR,DIF_MONTH from TBFM_DIF group by DIF_TSAID,DIF_YEAR,DIF_MONTH)b on (a.PMS_TSAID=b.DIF_TSAID and a.AYTJ_YEARMONTH=b.DIF_YEAR+'-'+b.DIF_MONTH) left join (select sum(cast(DIFYF_DIFMONEY as decimal(12,2))) as DIFYF_DIFMONEY,DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF group by DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH)c on (a.PMS_TSAID=c.DIFYF_TSAID and a.AYTJ_YEARMONTH=c.DIFYF_YEAR+'-'+c.DIFYF_MONTH))k group by PMS_TSAID) as a left join (select TASK_ID,CWCB_STATE,CWCB_HSDATE from TBCB_BMCONFIRM) as b on a.PMS_TSAID=b.TASK_ID)t left join (select distinct TSA_ID,CM_CONTR from View_CM_Task where CM_SPSTATUS='2')m on t.PMS_TSAID=m.TSA_ID left join  TBPM_CONPCHSINFO as c on m.CM_CONTR=c.PCON_BCODE left join (select b.conId as conid,sum(cast(b.kpmoney as float))*10000 as kpZongMoney,case when KP_SPSTATE='3' then '是' else '否' end as kpstate from CM_KAIPIAO as a left join dbo.CM_KAIPIAO_DETAIL as b on a.KP_TaskID=b.cId where KP_SPSTATE='3' group by b.conId,KP_SPSTATE)p on c.PCON_BCODE=p.conid group by PCON_CUSTMNAME,PCON_BCODE,PCON_YZHTH,PCON_ENGNAME,PCON_ENGTYPE,PCON_JINE,kpZongMoney,kpstate)s where " + strstring();

                System.Data.DataTable dthj = DBCallCommon.GetDTUsingSqlText(sqlhj);
                if (dthj.Rows.Count > 0)
                {
                    System.Web.UI.WebControls.Label lb_xshtzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_xshtzj");

                    System.Web.UI.WebControls.Label lb_zjrgzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_zjrgzj");
                    System.Web.UI.WebControls.Label lbclzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbclzj");
                    System.Web.UI.WebControls.Label lbzzfyzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzzfyzj");
                    System.Web.UI.WebControls.Label lbwxfyzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbwxfyzj");
                    System.Web.UI.WebControls.Label lbcnfbzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbcnfbzj");
                    System.Web.UI.WebControls.Label lbyfzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbyfzj");
                    System.Web.UI.WebControls.Label lbfjcbzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbfjcbzj");


                    System.Web.UI.WebControls.Label lb_cbzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_cbzj");
                    System.Web.UI.WebControls.Label lb_cbtotal = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_cbtotal");
                    System.Web.UI.WebControls.Label lbkpjezj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbkpjezj");


                    lb_xshtzj.Text = dthj.Rows[0]["xshtzje"].ToString().Trim();

                    lb_zjrgzj.Text = dthj.Rows[0]["zjrgfzj"].ToString().Trim();
                    lbclzj.Text = dthj.Rows[0]["clfzj"].ToString().Trim();
                    lbzzfyzj.Text = dthj.Rows[0]["zzfyzj"].ToString().Trim();
                    lbwxfyzj.Text = dthj.Rows[0]["wxfyzj"].ToString().Trim();
                    lbcnfbzj.Text = dthj.Rows[0]["cnfbzj"].ToString().Trim();
                    lbyfzj.Text = dthj.Rows[0]["yfzj"].ToString().Trim();
                    lbfjcbzj.Text = dthj.Rows[0]["fjcbzj"].ToString().Trim();

                    lb_cbzj.Text = dthj.Rows[0]["cbzje"].ToString().Trim();
                    lb_cbtotal.Text = dthj.Rows[0]["cbtotal"].ToString().Trim();

                    lbkpjezj.Text = dthj.Rows[0]["kpzongjehj"].ToString().Trim();
                }


                if (canseeif.Checked == true)
                {
                    HtmlTableCell tdzjrgf = e.Item.FindControl("tdzjrgf") as HtmlTableCell;
                    HtmlTableCell tdclf = e.Item.FindControl("tdclf") as HtmlTableCell;
                    HtmlTableCell tdzzfy = e.Item.FindControl("tdzzfy") as HtmlTableCell;
                    HtmlTableCell tdwxfy = e.Item.FindControl("tdwxfy") as HtmlTableCell;
                    HtmlTableCell tdcnfb = e.Item.FindControl("tdcnfb") as HtmlTableCell;
                    HtmlTableCell tdyf = e.Item.FindControl("tdyf") as HtmlTableCell;
                    HtmlTableCell tdfjcb = e.Item.FindControl("tdfjcb") as HtmlTableCell;

                    tdzjrgf.Visible = false;
                    tdclf.Visible = false;
                    tdzzfy.Visible = false;
                    tdwxfy.Visible = false;
                    tdcnfb.Visible = false;
                    tdyf.Visible = false;
                    tdfjcb.Visible = false;
                }
            }
        }



        #region 批量导出

        protected void btn_plexport_OnClick(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext = "select *,case when isnull(PCON_JINE,0)=0 then 0 else (PCON_JINE-RWHCB_CBZJ)*100/PCON_JINE end as maolilv1,case when isnull(PCON_JINE,0)=0 then 0 else (PCON_JINE-CBZJ)*100/PCON_JINE end as maolilv2 from (select PCON_CUSTMNAME,PCON_BCODE,PCON_YZHTH,PCON_ENGNAME,PCON_ENGTYPE,cast((cast(PCON_JINE as float)*10000)/1.17 as decimal(18,2)) as PCON_JINE,sum(cast((isnull(AYTJ_JJFYXJ,0)+isnull(AYTJ_JGYZFYXJ,0)) as decimal(12,2))) as RWHCB_ZJRG,sum((isnull(XJPMS_01_01,0)+isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_05,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_07,0)+isnull(XJPMS_01_08,0)+isnull(XJPMS_01_09,0)+isnull(XJPMS_01_10,0)+isnull(XJPMS_01_11,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_15,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0))) as RWHCB_CL,sum(cast((AYTJ_GDZZFYXJ+AYTJ_KBZZFYXJ) as decimal(12,2))) as RWHCB_ZZFY,sum(cast(AYTJ_WXFYXJ as decimal(12,2))) as RWHCB_WXFY,sum(cast(AYTJ_CNFBXJ as decimal(12,2))) as RWHCB_CNFB,sum(cast(AYTJ_YFXJ as decimal(12,2))) as RWHCB_YF,sum(cast(AYTJ_FJCBXJ as decimal(12,2))) as RWHCB_FJCB,sum(cast((isnull(AYTJ_JJFYXJ,0)+isnull(AYTJ_JGYZFYXJ,0)+isnull(XJPMS_01_01,0)+isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_05,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_07,0)+isnull(XJPMS_01_08,0)+isnull(XJPMS_01_09,0)+isnull(XJPMS_01_10,0)+isnull(XJPMS_01_11,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_15,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0)+isnull(AYTJ_WXFYXJ,0)+isnull(AYTJ_CNFBXJ,0)+isnull(AYTJ_YFXJ,0)+isnull(AYTJ_FJCBXJ,0)) as decimal(12,2))) as RWHCB_CBZJ,sum(cast((isnull(AYTJ_JJFYXJ,0)+isnull(AYTJ_JGYZFYXJ,0)+isnull(XJPMS_01_01,0)+isnull(XJPMS_01_02,0)+isnull(XJPMS_01_03,0)+isnull(XJPMS_01_04,0)+isnull(XJPMS_01_05,0)+isnull(XJPMS_01_06,0)+isnull(XJPMS_01_07,0)+isnull(XJPMS_01_08,0)+isnull(XJPMS_01_09,0)+isnull(XJPMS_01_10,0)+isnull(XJPMS_01_11,0)+isnull(XJPMS_01_12,0)+isnull(XJPMS_01_13,0)+isnull(XJPMS_01_14,0)+isnull(XJPMS_01_15,0)+isnull(XJPMS_01_16,0)+isnull(XJPMS_01_17,0)+isnull(XJPMS_01_18,0)+isnull(XJPMS_02_01,0)+isnull(XJPMS_02_02,0)+isnull(XJPMS_02_03,0)+isnull(XJPMS_02_04,0)+isnull(XJPMS_02_05,0)+isnull(XJPMS_02_06,0)+isnull(XJPMS_02_07,0)+isnull(XJPMS_02_08,0)+isnull(XJPMS_02_09,0)+isnull(AYTJ_GDZZFYXJ,0)+isnull(AYTJ_KBZZFYXJ,0)+isnull(AYTJ_WXFYXJ,0)+isnull(AYTJ_CNFBXJ,0)+isnull(AYTJ_YFXJ,0)+isnull(AYTJ_FJCBXJ,0)) as decimal(12,2))) as CBZJ,kpZongMoney,kpstate from (select * from (select PMS_TSAID,cast(sum(isnull(PMS_01_01,0)) as decimal(12,2)) as XJPMS_01_01,cast(sum(isnull(PMS_01_02,0)) as decimal(12,2)) as XJPMS_01_02,cast(sum(isnull(PMS_01_03,0)) as decimal(12,2)) as XJPMS_01_03,cast(sum(isnull(PMS_01_04,0)) as decimal(12,2)) as XJPMS_01_04,cast(sum(isnull(PMS_01_05,0)) as decimal(12,2)) as XJPMS_01_05,cast(sum(isnull(PMS_01_06,0)) as decimal(12,2)) as XJPMS_01_06,cast(sum(isnull(PMS_01_07,0)) as decimal(12,2)) as XJPMS_01_07,cast(sum(isnull(PMS_01_08,0)) as decimal(12,2)) as XJPMS_01_08,cast(sum(isnull(PMS_01_09,0)) as decimal(12,2)) as XJPMS_01_09,cast(sum(isnull(PMS_01_10,0)) as decimal(12,2)) as XJPMS_01_10,cast(sum(isnull(PMS_01_11,0)) as decimal(12,2)) as XJPMS_01_11,cast(sum(isnull(PMS_01_12,0)) as decimal(12,2)) as XJPMS_01_12,cast(sum(isnull(PMS_01_13,0)) as decimal(12,2)) as XJPMS_01_13,cast(sum(isnull(PMS_01_14,0)) as decimal(12,2)) as XJPMS_01_14,cast(sum(isnull(PMS_01_15,0)) as decimal(12,2)) as XJPMS_01_15,cast(sum(isnull(PMS_01_16,0)) as decimal(12,2)) as XJPMS_01_16,cast(sum(isnull(PMS_01_17,0)) as decimal(12,2)) as XJPMS_01_17,cast(sum(isnull(PMS_01_18,0)) as decimal(12,2)) as XJPMS_01_18,cast(sum(isnull(PMS_02_01,0)) as decimal(12,2)) as XJPMS_02_01,cast(sum(isnull(PMS_02_02,0)) as decimal(12,2)) as XJPMS_02_02,cast(sum(isnull(PMS_02_03,0)) as decimal(12,2)) as XJPMS_02_03,cast(sum(isnull(PMS_02_04,0)) as decimal(12,2)) as XJPMS_02_04,cast(sum(isnull(PMS_02_05,0)) as decimal(12,2)) as XJPMS_02_05,cast(sum(isnull(PMS_02_06,0)) as decimal(12,2)) as XJPMS_02_06,cast(sum(isnull(PMS_02_07,0)) as decimal(12,2)) as XJPMS_02_07,cast(sum(isnull(PMS_02_08,0)) as decimal(12,2)) as XJPMS_02_08,cast(sum(isnull(PMS_02_09,0)) as decimal(12,2)) as XJPMS_02_09,isnull(sum(isnull(AYTJ_GZ,0)),0) as AYTJ_GZXJ,isnull(sum(isnull(AYTJ_QT,0)),0) as AYTJ_QTXJ,sum(isnull(AYTJ_JJFY,0)) as AYTJ_JJFYXJ,sum(isnull(AYTJ_JGYZFY,0)) as AYTJ_JGYZFYXJ,sum(isnull(AYTJ_GDZZFY,0)) as AYTJ_GDZZFYXJ,sum(isnull(AYTJ_KBZZFY,0)) as AYTJ_KBZZFYXJ,sum(isnull(AYTJ_WXFY,0)+isnull(DIF_DIFMONEY,0)) as AYTJ_WXFYXJ,sum(isnull(AYTJ_CNFB,0)) as AYTJ_CNFBXJ,sum(isnull(AYTJ_YF,0)+isnull(DIFYF_DIFMONEY,0)) as AYTJ_YFXJ,sum(isnull(AYTJ_FJCB,0)) as AYTJ_FJCBXJ from (select * from VIEW_FM_AYTJ as a left join (select sum(cast(DIF_DIFMONEY as decimal(12,2))) as DIF_DIFMONEY,DIF_TSAID,DIF_YEAR,DIF_MONTH from TBFM_DIF group by DIF_TSAID,DIF_YEAR,DIF_MONTH)b on (a.PMS_TSAID=b.DIF_TSAID and a.AYTJ_YEARMONTH=b.DIF_YEAR+'-'+b.DIF_MONTH) left join (select sum(cast(DIFYF_DIFMONEY as decimal(12,2))) as DIFYF_DIFMONEY,DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH from TBFM_YFDIF group by DIFYF_TSAID,DIFYF_YEAR,DIFYF_MONTH)c on (a.PMS_TSAID=c.DIFYF_TSAID and a.AYTJ_YEARMONTH=c.DIFYF_YEAR+'-'+c.DIFYF_MONTH))t group by PMS_TSAID) as a left join (select TASK_ID,CWCB_STATE,CWCB_HSDATE from TBCB_BMCONFIRM) as b on a.PMS_TSAID=b.TASK_ID)t left join (select distinct TSA_ID,CM_CONTR from View_CM_Task where CM_SPSTATUS='2')m on t.PMS_TSAID=m.TSA_ID left join  TBPM_CONPCHSINFO as c on m.CM_CONTR=c.PCON_BCODE left join (select b.conId as conid,sum(cast(b.kpmoney as float))*10000 as kpZongMoney,case when KP_SPSTATE='3' then '是' else '否' end as kpstate from CM_KAIPIAO as a left join dbo.CM_KAIPIAO_DETAIL as b on a.KP_TaskID=b.cId where KP_SPSTATE='3' group by b.conId,KP_SPSTATE)p on c.PCON_BCODE=p.conid group by PCON_CUSTMNAME,PCON_BCODE,PCON_YZHTH,PCON_ENGNAME,PCON_ENGTYPE,PCON_JINE,kpZongMoney,kpstate)s where " + strstring();
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string filename = "按合同号成本汇总" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("按合同号成本汇总.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count - 2; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString().Trim());
                    }
                    for (int k = dt.Columns.Count - 2; k < dt.Columns.Count; k++)
                    {
                        row.CreateCell(k + 1).SetCellValue((CommonFun.ComTryDecimal(dt.Rows[i][k].ToString().Trim())).ToString("0.00")+"%");
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


        //隐藏列
        protected void displayif_onclick(object sender, EventArgs e)
        {
            this.InitVar();
            this.bindGrid();
        }


        protected void radio_kaipiaoif_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }
    }
}
