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
using System.Text;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_contract_task_view : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btn_search_Click(null, null);
            }
            this.InitVar();
        }
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
            pager.TableName = " TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID left join (select * from (select no=row_number() over(partition by CM_CONTR order by getdate()),* from TBPM_DETAIL) t where no=1) as c on a.PCON_BCODE=c.CM_CONTR left join (select distinct TFI_CONTR from TBMP_FINISHED_IN)t  on a.PCON_BCODE=t.TFI_CONTR";
            pager.PrimaryKey = "PCON_BCODE";
            pager.ShowFields = "PCON_BCODE ,PCON_ENGNAME,PCON_FORM";
            pager.OrderField = "PCON_BCODE";
            pager.StrWhere = strwhere();
            pager.OrderType = 1;
            pager.PageSize = Convert.ToInt16(ddl_pageno_change.SelectedValue);
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, details_repeater, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件                 
            }
        }
        protected void btn_search_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            bindGrid();
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        private string strwhere()
        {
            string sqltext = "";
            //合同类别
            sqltext += "PCON_FORM=0 ";
            //项目名称
            if (txt_PJNAME.Text.Trim() != "")
            {
                sqltext += " and PCON_ENGNAME like '%" + txt_PJNAME.Text.Trim() + "%'";
            }
            //合同号
            if (txtHTH.Text.Trim() != "")
            {
                sqltext += " and PCON_BCODE like '%" + txtHTH.Text.Trim() + "%'  ";
            }
            return sqltext;
        }
        //确定有没有数据进而作出处理
        protected void details_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string sqltext = "";
                string htbh = ((Label)e.Item.FindControl("MS_ID")).Text.Trim().Substring(5);
                //合同信息
                sqltext = "select * from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID left join (select * from (select no=row_number() over(partition by CM_CONTR order by getdate()),* from TBPM_DETAIL) t where no=1) as c on a.PCON_BCODE=c.CM_CONTR left join (select distinct TFI_CONTR from TBMP_FINISHED_IN)t  on a.PCON_BCODE=t.TFI_CONTR where patindex('%" + htbh + "%',PCON_BCODE)>0";
                DataTable dthtinfo = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dthtinfo.Rows.Count == 0)
                {
                    ((HyperLink)e.Item.FindControl("HyperLink8")).Visible = false;
                }
                //采购计划
                sqltext = "select * from TBPC_PURCHASEPLAN where patindex('%" + htbh + "%',PUR_PTCODE)>0";
                DataTable dtcgjh = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dtcgjh.Rows.Count == 0)
                {
                    ((HyperLink)e.Item.FindControl("HyperLink7")).Visible = false;
                }
                //物料出库
                sqltext = "select * from View_SM_OUT where patindex('%" + htbh + "%',TSAID)>0";
                DataTable dtwlck = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dtwlck.Rows.Count == 0)
                {
                    ((HyperLink)e.Item.FindControl("HyperLink2")).Visible = false;
                }
                //成品质检
                sqltext = "select * from TBQM_APLYFORINSPCT where patindex('%" + htbh + "%',AFI_ENGID)>0 and AFI_TSDEP='生产管理部' and AFI_STATE!='0'";
                DataTable dtcpzj = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dtcpzj.Rows.Count == 0)
                {
                    ((HyperLink)e.Item.FindControl("HyperLink4")).Visible = false;
                }
                //生产外协汇总和生产外协进度
                sqltext = "select * from (select distinct b.PM_DocuNum,b.PM_SPZT,b.PM_SUBMITID,b.PM_JHQ,a.MS_ID,a.MS_scwaixie,a.MS_CODE,a.MS_ENGID,a.MS_PJID,a.MS_ENGNAME,a.MS_PID,a.MS_TUHAO,a.MS_ZONGXU,a.MS_NAME,a.MS_GUIGE,a.MS_CAIZHI,a.MS_NUM,a.MS_TUWGHT,a.MS_TUTOTALWGHT,a.MS_MASHAPE,a.MS_LEN,a.MS_WIDTH,a.MS_PROCESS,a.MS_wxtype,CAST(a.MS_XHBZ as varchar(8000))MS_XHBZ,c.CM_PROJ from TBPM_WXDetail  as a left outer join  TBPM_SCWXRVW as b on a.MS_WSID=b.PM_DocuNum left join TBCM_PLAN as c on a.MS_PJID=c.CM_CONTR where MS_PID in (select distinct PM_PID from TBPM_SCWXRVW) and MS_scwaixie<>'0' and CM_PROJ in (select CM_PROJ from TBCM_PLAN where id in (select id from TBCM_BASIC where TSA_ID=MS_ENGID)))t where patindex('%" + htbh + "%',MS_ENGID)>0";
                DataTable dtscwxhz = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dtscwxhz.Rows.Count == 0)
                {
                    ((HyperLink)e.Item.FindControl("HyperLink3")).Visible = false;
                    ((HyperLink)e.Item.FindControl("HyperLink9")).Visible = false;
                }
                //成品入库
                sqltext = "select * from (select a.*,SPSJ=case when Rank='1' then SEC_SJ when rank='0' then FIR_SJ when rank='2' then THI_SJ end from TBMP_FINISHED_IN as a left join TBMP_FINISHED_IN_Audit as b on a.TFI_DOCNUM = b.FIA_DOCNUM)t where patindex('%" + htbh + "%',TSA_ID)>0";
                DataTable dtcprk = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dtcprk.Rows.Count == 0)
                {
                    ((HyperLink)e.Item.FindControl("HyperLink1")).Visible = false;
                }
                //成品出库
                sqltext = "select * from TBMP_FINISHED_OUT AS A LEFT OUTER JOIN View_CM_FaHuo AS B ON A.TSA_ID=B.TSA_ID AND A.TFO_ENGNAME=B.TSA_ENGNAME AND A.TFO_MAP=B.TSA_MAP AND A.TFO_FID=B.CM_FID AND A.TFO_ZONGXU=B.ID left join TBPM_TCTSASSGN as C on A.TSA_ID=C.TSA_ID left join TBCM_PLAN as D on C.ID=D.ID where patindex('%" + htbh + "%',a.TSA_ID)>0";
                DataTable dtcpck = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dtcpck.Rows.Count == 0)
                {
                    ((HyperLink)e.Item.FindControl("HyperLink6")).Visible = false;
                }
                //开票信息
                sqltext = "select * from (select * from CM_KAIPIAO as d left join (select a.cId , stuff((select sprId+',' from CM_KAIPIAO_HUISHEN b where b.cId =a.cId and( b.result is null or b.result='') for xml path('')),1,0,',') 'sprId ' from CM_KAIPIAO_HUISHEN  a  group by  a.cId)c on d.KP_TaskID=c.cId)e where patindex('%" + htbh + "%',KP_CONID)>0";
                DataTable dtkpinfo = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dtkpinfo.Rows.Count == 0)
                {
                    ((HyperLink)e.Item.FindControl("HyperLink5")).Visible = false;
                }
            }
        }
    }
}
