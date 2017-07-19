using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_PurQuery : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                string PCON_BCODE = Request["PCON_BCODE"];
                BindPeople();
                if (!string.IsNullOrEmpty(PCON_BCODE))
                {
                    tb_ptcode.Text = PCON_BCODE.Trim();
                    GetBoundData();
                }
            }
        }

        #region 分页

        private void InitPager()
        {

            pager.TableName = "((select a.planno,a.ptcode,a.marid,a.marnm,a.margg,a.marcz,a.margb,a.marunit,a.marfzunit,a.length,a.width,a.num,a.fznum,a.rpnum,a.rpfznum,a.fgrid,a.prsqtime,a.fgrnm,a.fgtime,a.cgrid,a.cgrnm,a.purstate,a.purnote,a.OUTTZNUM,a.OUTTZFZNUM,a.Pue_Closetype,c.orderno,c.PO_CGTIMERQ,c.ctprice,c.ctamount,b.suppliernm,b.picno,b.irqdata,a.PUR_CSTATE,a.PUR_TUHAO,a.PUR_ID,a.sqrid,a.sqrnm,a.PR_MAP,a.PR_CHILDENGNAME,a.prstate,a.CM_FHDATE from (select a.PR_SHEETNO as planno,b.PUR_PTCODE as ptcode,b.PUR_MARID as marid,e.MNAME as marnm,e.GUIGE as margg,e.CAIZHI as marcz,e.GB as margb,e.PURCUNIT as marunit,b.PUR_TECUNIT as marfzunit,b.PUR_LENGTH as length,b.PUR_WIDTH as width,b.PUR_NUM as num,b.PUR_FZNUM as fznum,b.PUR_RPNUM as rpnum,b.PUR_RPFZNUM as rpfznum,b.PUR_PTASMAN as fgrid,c.ST_NAME as fgrnm,b.PUR_PTASTIME as fgtime,b.PUR_CGMAN as cgrid,f.ST_NAME as cgrnm,b.PUR_STATE as purstate,b.PUR_NOTE as purnote,PUR_CSTATE,PUR_TUHAO,PUR_ID,b.Pue_Closetype,a.PR_SQTIME as prsqtime,a.PR_SQREID as sqrid,d.ST_NAME as sqrnm,PR_MAP,PR_CHILDENGNAME,a.PR_STATE as prstate,OUTTZNUM,OUTTZFZNUM,CM_FHDATE from (select top(2000) * from TBPC_PCHSPLANRVW order by PR_SQTIME desc) as a left join TBPC_PURCHASEPLAN as b on a.PR_SHEETNO=b.PUR_PCODE left join TBDS_STAFFINFO as c on b.PUR_PTASMAN=c.ST_ID left join TBDS_STAFFINFO as d on a.PR_SQREID=d.ST_ID left join TBMA_MATERIAL as e on b.PUR_MARID=e.ID left join TBDS_STAFFINFO as f on b.PUR_CGMAN=f.ST_ID left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as g on b.PUR_PTCODE=g.PTCFrom and b.PUR_MARID=g.TzMaterialCode left join (select distinct CM_FHDATE,TSA_ID from TBCM_PLAN as a left join TBCM_BASIC as b on a.ID=b.ID) as h on a.PR_ENGID=h.TSA_ID) as a left join (select ICL_SHEETNO as picno,ICL_IQRDATE as irqdata,PIC_PTCODE as ptcode,CS_NAME as suppliernm from TBPC_IQRCMPPRCRVW left join TBPC_IQRCMPPRICE on ICL_SHEETNO=PIC_SHEETNO left join TBCS_CUSUPINFO on PIC_SUPPLIERRESID=CS_CODE) as b ON a.ptcode = b.ptcode left join (select PO_CODE as orderno,PO_PTCODE,PO_CSTATE as detailcstate,PO_CGTIMERQ,CAST(PO_ZXNUM * PO_CTAXUPRICE AS decimal(18, 4)) AS ctamount,PO_CTAXUPRICE AS ctprice from TBPC_PURORDERDETAIL) as c ON a.ptcode = c.PO_PTCODE AND c.detailcstate = '0') as a left join TBPC_IQRCMPPRCRVW as b on a.picno=b.ICL_SHEETNO left join TBPC_PURORDERTOTAL as c on a.orderno=c.PO_CODE left join (select ptc,result,ISAGAIN,bjsj,rn from  ((select PTC,RESULT,ISAGAIN,bjsj as bjsj_infact,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc,bjsj) as rn from View_TBQM_APLYFORITEM) as c where rn<=1)d  left join  (select ptc as PTC_two,bjsj  from (select *,row_number() over(partition by PTC order by  bjsj ) as rn from View_TBQM_APLYFORITEM) as c where rn<=1)f on d.ptc=f.PTC_two)) as d on a.ptcode=d.PTC left join TBPC_PURORDERDETAIL as e on a.ptcode=e.PO_PTCODE)";

            if (DropDownListrange.SelectedIndex == 1)
            {
                pager.TableName = "((select a.planno,a.ptcode,a.marid,a.marnm,a.margg,a.marcz,a.margb,a.marunit,a.marfzunit,a.length,a.width,a.num,a.fznum,a.rpnum,a.rpfznum,a.fgrid,a.prsqtime,a.fgrnm,a.fgtime,a.cgrid,a.cgrnm,a.purstate,a.purnote,a.OUTTZNUM,a.OUTTZFZNUM,a.Pue_Closetype,c.orderno,c.PO_CGTIMERQ,c.ctprice,c.ctamount,b.suppliernm,b.picno,b.irqdata,a.PUR_CSTATE,a.PUR_TUHAO,a.PUR_ID,a.sqrid,a.sqrnm,a.PR_MAP,a.PR_CHILDENGNAME,a.prstate,a.CM_FHDATE from (select a.PR_SHEETNO as planno,b.PUR_PTCODE as ptcode,b.PUR_MARID as marid,e.MNAME as marnm,e.GUIGE as margg,e.CAIZHI as marcz,e.GB as margb,e.PURCUNIT as marunit,b.PUR_TECUNIT as marfzunit,b.PUR_LENGTH as length,b.PUR_WIDTH as width,b.PUR_NUM as num,b.PUR_FZNUM as fznum,b.PUR_RPNUM as rpnum,b.PUR_RPFZNUM as rpfznum,b.PUR_PTASMAN as fgrid,c.ST_NAME as fgrnm,b.PUR_PTASTIME as fgtime,b.PUR_CGMAN as cgrid,f.ST_NAME as cgrnm,b.PUR_STATE as purstate,b.PUR_NOTE as purnote,PUR_CSTATE,PUR_TUHAO,PUR_ID,b.Pue_Closetype,a.PR_SQTIME as prsqtime,a.PR_SQREID as sqrid,d.ST_NAME as sqrnm,PR_MAP,PR_CHILDENGNAME,a.PR_STATE as prstate,OUTTZNUM,OUTTZFZNUM,CM_FHDATE from TBPC_PCHSPLANRVW as a left join TBPC_PURCHASEPLAN as b on a.PR_SHEETNO=b.PUR_PCODE left join TBDS_STAFFINFO as c on b.PUR_PTASMAN=c.ST_ID left join TBDS_STAFFINFO as d on a.PR_SQREID=d.ST_ID left join TBMA_MATERIAL as e on b.PUR_MARID=e.ID left join TBDS_STAFFINFO as f on b.PUR_CGMAN=f.ST_ID left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as g on b.PUR_PTCODE=g.PTCFrom and b.PUR_MARID=g.TzMaterialCode left join (select distinct CM_FHDATE,TSA_ID from TBCM_PLAN as a left join TBCM_BASIC as b on a.ID=b.ID) as h on a.PR_ENGID=h.TSA_ID) as a left join (select ICL_SHEETNO as picno,ICL_IQRDATE as irqdata,PIC_PTCODE as ptcode,CS_NAME as suppliernm from TBPC_IQRCMPPRCRVW left join TBPC_IQRCMPPRICE on ICL_SHEETNO=PIC_SHEETNO left join TBCS_CUSUPINFO on PIC_SUPPLIERRESID=CS_CODE) as b ON a.ptcode = b.ptcode left join (select PO_CODE as orderno,PO_PTCODE,PO_CSTATE as detailcstate,PO_CGTIMERQ,CAST(PO_ZXNUM * PO_CTAXUPRICE AS decimal(18, 4)) AS ctamount,PO_CTAXUPRICE AS ctprice from TBPC_PURORDERDETAIL) as c ON a.ptcode = c.PO_PTCODE AND c.detailcstate = '0') as a left join TBPC_IQRCMPPRCRVW as b on a.picno=b.ICL_SHEETNO left join TBPC_PURORDERTOTAL as c on a.orderno=c.PO_CODE left join (select ptc,result,ISAGAIN,bjsj,rn from  ((select PTC,RESULT,ISAGAIN,bjsj as bjsj_infact,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc,bjsj) as rn from View_TBQM_APLYFORITEM) as c where rn<=1)d  left join  (select ptc as PTC_two,bjsj  from (select *,row_number() over(partition by PTC order by  bjsj ) as rn from View_TBQM_APLYFORITEM) as c where rn<=1)f on d.ptc=f.PTC_two)) as d on a.ptcode=d.PTC left join TBPC_PURORDERDETAIL as e on a.ptcode=e.PO_PTCODE)";
            }

            pager.PrimaryKey = "PUR_ID";
            pager.ShowFields = "a.*,b.ICL_IQRDATE,c.PO_ZDDATE,d.RESULT,e.PO_STATE,e.PO_ZXNUM,e.PO_RECGDFZNUM";
            pager.OrderField = "prsqtime";
            pager.StrWhere = CreateConStr();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 50;
            UCPaging1.PageSize = pager.PageSize;
        }

        protected void GetBoundData()
        {
            InitPager();
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, Rep, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            string sqlyqwdh = "select count(*) as count from ((select a.planno,a.ptcode,a.marid,a.marnm,a.margg,a.marcz,a.margb,a.marunit,a.marfzunit,a.length,a.width,a.num,a.fznum,a.rpnum,a.rpfznum,a.fgrid,a.fgrnm,a.fgtime,a.cgrid,a.cgrnm,a.purstate,a.purnote,a.OUTTZNUM,a.OUTTZFZNUM,a.Pue_Closetype,c.orderno,c.PO_CGTIMERQ,c.ctprice,c.ctamount,b.picno,b.irqdata,b.suppliernm,a.PUR_CSTATE,a.PUR_TUHAO,a.PUR_ID,a.sqrid,a.sqrnm,a.PR_MAP,a.PR_CHILDENGNAME,a.prstate,a.CM_FHDATE from (select a.PR_SHEETNO as planno,b.PUR_PTCODE as ptcode,b.PUR_MARID as marid,e.MNAME as marnm,e.GUIGE as margg,e.CAIZHI as marcz,e.GB as margb,e.PURCUNIT as marunit,b.PUR_TECUNIT as marfzunit,b.PUR_LENGTH as length,b.PUR_WIDTH as width,b.PUR_NUM as num,b.PUR_FZNUM as fznum,b.PUR_RPNUM as rpnum,b.PUR_RPFZNUM as rpfznum,b.PUR_PTASMAN as fgrid,c.ST_NAME as fgrnm,b.PUR_PTASTIME as fgtime,b.PUR_CGMAN as cgrid,f.ST_NAME as cgrnm,b.PUR_STATE as purstate,b.PUR_NOTE as purnote,PUR_CSTATE,PUR_TUHAO,PUR_ID,b.Pue_Closetype,a.PR_SQREID as sqrid,d.ST_NAME as sqrnm,PR_MAP,PR_CHILDENGNAME,a.PR_STATE as prstate,OUTTZNUM,OUTTZFZNUM,CM_FHDATE from (select top(2000) * from TBPC_PCHSPLANRVW order by PR_SQTIME desc) as a left join TBPC_PURCHASEPLAN as b on a.PR_SHEETNO=b.PUR_PCODE left join TBDS_STAFFINFO as c on b.PUR_PTASMAN=c.ST_ID left join TBDS_STAFFINFO as d on a.PR_SQREID=d.ST_ID left join TBMA_MATERIAL as e on b.PUR_MARID=e.ID left join TBDS_STAFFINFO as f on b.PUR_CGMAN=f.ST_ID left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as g on b.PUR_PTCODE=g.PTCFrom and b.PUR_MARID=g.TzMaterialCode left join (select distinct CM_FHDATE,TSA_ID from TBCM_PLAN as a left join TBCM_BASIC as b on a.ID=b.ID) as h on a.PR_ENGID=h.TSA_ID) as a left join (select ICL_SHEETNO as picno,ICL_IQRDATE as irqdata,PIC_PTCODE as ptcode,CS_NAME as suppliernm from TBPC_IQRCMPPRCRVW left join TBPC_IQRCMPPRICE on ICL_SHEETNO=PIC_SHEETNO left join TBCS_CUSUPINFO on PIC_SUPPLIERRESID=CS_CODE) as b ON a.ptcode = b.ptcode left join (select PO_CODE as orderno,PO_PTCODE,PO_CSTATE as detailcstate,PO_CGTIMERQ,CAST(PO_ZXNUM * PO_CTAXUPRICE AS decimal(18, 4)) AS ctamount,PO_CTAXUPRICE AS ctprice from TBPC_PURORDERDETAIL) as c ON a.ptcode = c.PO_PTCODE AND c.detailcstate = '0') as a left join TBPC_IQRCMPPRCRVW as b on a.picno=b.ICL_SHEETNO left join TBPC_PURORDERTOTAL as c on a.orderno=c.PO_CODE left join (select ptc,result,ISAGAIN,bjsj,rn from  ((select PTC,RESULT,ISAGAIN,bjsj as bjsj_infact,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc,bjsj) as rn from View_TBQM_APLYFORITEM) as c where rn<=1)d  left join  (select ptc as PTC_two,bjsj  from (select *,row_number() over(partition by PTC order by  bjsj ) as rn from View_TBQM_APLYFORITEM) as c where rn<=1)f on d.ptc=f.PTC_two)) as d on a.ptcode=d.PTC left join TBPC_PURORDERDETAIL as e on a.ptcode=e.PO_PTCODE) where " + CreateConStr() + " and a.PO_CGTIMERQ<'" + DateTime.Now.ToString("yyyy-MM-dd").Trim() + "' and (RESULT is null or RESULT='') and e.PO_STATE!='2'";

            if (DropDownListrange.SelectedIndex == 1)
            {
                sqlyqwdh = "select count(*) as count from ((select a.planno,a.ptcode,a.marid,a.marnm,a.margg,a.marcz,a.margb,a.marunit,a.marfzunit,a.length,a.width,a.num,a.fznum,a.rpnum,a.rpfznum,a.fgrid,a.fgrnm,a.fgtime,a.cgrid,a.cgrnm,a.purstate,a.purnote,a.OUTTZNUM,a.OUTTZFZNUM,a.Pue_Closetype,c.orderno,c.PO_CGTIMERQ,c.ctprice,c.ctamount,b.picno,b.irqdata,b.suppliernm,a.PUR_CSTATE,a.PUR_TUHAO,a.PUR_ID,a.sqrid,a.sqrnm,a.PR_MAP,a.PR_CHILDENGNAME,a.prstate,a.CM_FHDATE from (select a.PR_SHEETNO as planno,b.PUR_PTCODE as ptcode,b.PUR_MARID as marid,e.MNAME as marnm,e.GUIGE as margg,e.CAIZHI as marcz,e.GB as margb,e.PURCUNIT as marunit,b.PUR_TECUNIT as marfzunit,b.PUR_LENGTH as length,b.PUR_WIDTH as width,b.PUR_NUM as num,b.PUR_FZNUM as fznum,b.PUR_RPNUM as rpnum,b.PUR_RPFZNUM as rpfznum,b.PUR_PTASMAN as fgrid,c.ST_NAME as fgrnm,b.PUR_PTASTIME as fgtime,b.PUR_CGMAN as cgrid,f.ST_NAME as cgrnm,b.PUR_STATE as purstate,b.PUR_NOTE as purnote,PUR_CSTATE,PUR_TUHAO,PUR_ID,b.Pue_Closetype,a.PR_SQREID as sqrid,d.ST_NAME as sqrnm,PR_MAP,PR_CHILDENGNAME,a.PR_STATE as prstate,OUTTZNUM,OUTTZFZNUM,CM_FHDATE from TBPC_PCHSPLANRVW as a left join TBPC_PURCHASEPLAN as b on a.PR_SHEETNO=b.PUR_PCODE left join TBDS_STAFFINFO as c on b.PUR_PTASMAN=c.ST_ID left join TBDS_STAFFINFO as d on a.PR_SQREID=d.ST_ID left join TBMA_MATERIAL as e on b.PUR_MARID=e.ID left join TBDS_STAFFINFO as f on b.PUR_CGMAN=f.ST_ID left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as g on b.PUR_PTCODE=g.PTCFrom and b.PUR_MARID=g.TzMaterialCode left join (select distinct CM_FHDATE,TSA_ID from TBCM_PLAN as a left join TBCM_BASIC as b on a.ID=b.ID) as h on a.PR_ENGID=h.TSA_ID) as a left join (select ICL_SHEETNO as picno,ICL_IQRDATE as irqdata,PIC_PTCODE as ptcode,CS_NAME as suppliernm from TBPC_IQRCMPPRCRVW left join TBPC_IQRCMPPRICE on ICL_SHEETNO=PIC_SHEETNO left join TBCS_CUSUPINFO on PIC_SUPPLIERRESID=CS_CODE) as b ON a.ptcode = b.ptcode left join (select PO_CODE as orderno,PO_PTCODE,PO_CSTATE as detailcstate,PO_CGTIMERQ,CAST(PO_ZXNUM * PO_CTAXUPRICE AS decimal(18, 4)) AS ctamount,PO_CTAXUPRICE AS ctprice from TBPC_PURORDERDETAIL) as c ON a.ptcode = c.PO_PTCODE AND c.detailcstate = '0') as a left join TBPC_IQRCMPPRCRVW as b on a.picno=b.ICL_SHEETNO left join TBPC_PURORDERTOTAL as c on a.orderno=c.PO_CODE left join (select ptc,result,ISAGAIN,bjsj,rn from  ((select PTC,RESULT,ISAGAIN,bjsj as bjsj_infact,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc,bjsj) as rn from View_TBQM_APLYFORITEM) as c where rn<=1)d  left join  (select ptc as PTC_two,bjsj  from (select *,row_number() over(partition by PTC order by  bjsj ) as rn from View_TBQM_APLYFORITEM) as c where rn<=1)f on d.ptc=f.PTC_two)) as d on a.ptcode=d.PTC left join TBPC_PURORDERDETAIL as e on a.ptcode=e.PO_PTCODE) where " + CreateConStr() + " and a.PO_CGTIMERQ<'" + DateTime.Now.ToString("yyyy-MM-dd").Trim() + "' and (RESULT is null or RESULT='') and e.PO_STATE!='2'";
            }

            DataTable dtyqwdh = DBCallCommon.GetDTUsingSqlText(sqlyqwdh);
            if (dtyqwdh.Rows.Count > 0)
            {
                lbyqwdh.Text = "(" + dtyqwdh.Rows[0]["count"].ToString().Trim() + ")";
            }
            else
            {
                lbyqwdh.Text = "";
            }



            string sqlwgm = "select count(*) as count from ((select a.planno,a.ptcode,a.marid,a.marnm,a.margg,a.marcz,a.margb,a.marunit,a.marfzunit,a.length,a.width,a.num,a.fznum,a.rpnum,a.rpfznum,a.fgrid,a.fgrnm,a.fgtime,a.cgrid,a.cgrnm,a.purstate,a.purnote,a.OUTTZNUM,a.OUTTZFZNUM,a.Pue_Closetype,c.orderno,c.PO_CGTIMERQ,c.ctprice,c.ctamount,b.picno,b.suppliernm,b.irqdata,a.PUR_CSTATE,a.PUR_TUHAO,a.PUR_ID,a.sqrid,a.sqrnm,a.PR_MAP,a.PR_CHILDENGNAME,a.prstate,a.CM_FHDATE from (select a.PR_SHEETNO as planno,b.PUR_PTCODE as ptcode,b.PUR_MARID as marid,e.MNAME as marnm,e.GUIGE as margg,e.CAIZHI as marcz,e.GB as margb,e.PURCUNIT as marunit,b.PUR_TECUNIT as marfzunit,b.PUR_LENGTH as length,b.PUR_WIDTH as width,b.PUR_NUM as num,b.PUR_FZNUM as fznum,b.PUR_RPNUM as rpnum,b.PUR_RPFZNUM as rpfznum,b.PUR_PTASMAN as fgrid,c.ST_NAME as fgrnm,b.PUR_PTASTIME as fgtime,b.PUR_CGMAN as cgrid,f.ST_NAME as cgrnm,b.PUR_STATE as purstate,b.PUR_NOTE as purnote,PUR_CSTATE,PUR_TUHAO,PUR_ID,b.Pue_Closetype,a.PR_SQREID as sqrid,d.ST_NAME as sqrnm,PR_MAP,PR_CHILDENGNAME,a.PR_STATE as prstate,OUTTZNUM,OUTTZFZNUM,CM_FHDATE from (select top(2000) * from TBPC_PCHSPLANRVW order by PR_SQTIME desc) as a left join TBPC_PURCHASEPLAN as b on a.PR_SHEETNO=b.PUR_PCODE left join TBDS_STAFFINFO as c on b.PUR_PTASMAN=c.ST_ID left join TBDS_STAFFINFO as d on a.PR_SQREID=d.ST_ID left join TBMA_MATERIAL as e on b.PUR_MARID=e.ID left join TBDS_STAFFINFO as f on b.PUR_CGMAN=f.ST_ID left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as g on b.PUR_PTCODE=g.PTCFrom and b.PUR_MARID=g.TzMaterialCode left join (select distinct CM_FHDATE,TSA_ID from TBCM_PLAN as a left join TBCM_BASIC as b on a.ID=b.ID) as h on a.PR_ENGID=h.TSA_ID) as a left join (select ICL_SHEETNO as picno,ICL_IQRDATE as irqdata,PIC_PTCODE as ptcode,CS_NAME as suppliernm from TBPC_IQRCMPPRCRVW left join TBPC_IQRCMPPRICE on ICL_SHEETNO=PIC_SHEETNO left join TBCS_CUSUPINFO on PIC_SUPPLIERRESID=CS_CODE) as b ON a.ptcode = b.ptcode left join (select PO_CODE as orderno,PO_PTCODE,PO_CSTATE as detailcstate,PO_CGTIMERQ,CAST(PO_ZXNUM * PO_CTAXUPRICE AS decimal(18, 4)) AS ctamount,PO_CTAXUPRICE AS ctprice from TBPC_PURORDERDETAIL) as c ON a.ptcode = c.PO_PTCODE AND c.detailcstate = '0') as a left join TBPC_IQRCMPPRCRVW as b on a.picno=b.ICL_SHEETNO left join TBPC_PURORDERTOTAL as c on a.orderno=c.PO_CODE left join (select ptc,result,ISAGAIN,bjsj,rn from  ((select PTC,RESULT,ISAGAIN,bjsj as bjsj_infact,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc,bjsj) as rn from View_TBQM_APLYFORITEM) as c where rn<=1)d  left join  (select ptc as PTC_two,bjsj  from (select *,row_number() over(partition by PTC order by  bjsj ) as rn from View_TBQM_APLYFORITEM) as c where rn<=1)f on d.ptc=f.PTC_two)) as d on a.ptcode=d.PTC left join TBPC_PURORDERDETAIL as e on a.ptcode=e.PO_PTCODE) where " + CreateConStr() + " and ((purstate<7 and purstate>=3) or purstate='0') and a.PUR_CSTATE='0'";

            if (DropDownListrange.SelectedIndex == 1)
            {
                sqlwgm = "select count(*) as count from ((select a.planno,a.ptcode,a.marid,a.marnm,a.margg,a.marcz,a.margb,a.marunit,a.marfzunit,a.length,a.width,a.num,a.fznum,a.rpnum,a.rpfznum,a.fgrid,a.fgrnm,a.fgtime,a.cgrid,a.cgrnm,a.purstate,a.purnote,a.OUTTZNUM,a.OUTTZFZNUM,a.Pue_Closetype,c.orderno,c.PO_CGTIMERQ,c.ctprice,c.ctamount,b.picno,b.suppliernm,b.irqdata,a.PUR_CSTATE,a.PUR_TUHAO,a.PUR_ID,a.sqrid,a.sqrnm,a.PR_MAP,a.PR_CHILDENGNAME,a.prstate,a.CM_FHDATE from (select a.PR_SHEETNO as planno,b.PUR_PTCODE as ptcode,b.PUR_MARID as marid,e.MNAME as marnm,e.GUIGE as margg,e.CAIZHI as marcz,e.GB as margb,e.PURCUNIT as marunit,b.PUR_TECUNIT as marfzunit,b.PUR_LENGTH as length,b.PUR_WIDTH as width,b.PUR_NUM as num,b.PUR_FZNUM as fznum,b.PUR_RPNUM as rpnum,b.PUR_RPFZNUM as rpfznum,b.PUR_PTASMAN as fgrid,c.ST_NAME as fgrnm,b.PUR_PTASTIME as fgtime,b.PUR_CGMAN as cgrid,f.ST_NAME as cgrnm,b.PUR_STATE as purstate,b.PUR_NOTE as purnote,PUR_CSTATE,PUR_TUHAO,PUR_ID,b.Pue_Closetype,a.PR_SQREID as sqrid,d.ST_NAME as sqrnm,PR_MAP,PR_CHILDENGNAME,a.PR_STATE as prstate,OUTTZNUM,OUTTZFZNUM,CM_FHDATE from TBPC_PCHSPLANRVW as a left join TBPC_PURCHASEPLAN as b on a.PR_SHEETNO=b.PUR_PCODE left join TBDS_STAFFINFO as c on b.PUR_PTASMAN=c.ST_ID left join TBDS_STAFFINFO as d on a.PR_SQREID=d.ST_ID left join TBMA_MATERIAL as e on b.PUR_MARID=e.ID left join TBDS_STAFFINFO as f on b.PUR_CGMAN=f.ST_ID left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as g on b.PUR_PTCODE=g.PTCFrom and b.PUR_MARID=g.TzMaterialCode left join (select distinct CM_FHDATE,TSA_ID from TBCM_PLAN as a left join TBCM_BASIC as b on a.ID=b.ID) as h on a.PR_ENGID=h.TSA_ID) as a left join (select ICL_SHEETNO as picno,ICL_IQRDATE as irqdata,PIC_PTCODE as ptcode,CS_NAME as suppliernm from TBPC_IQRCMPPRCRVW left join TBPC_IQRCMPPRICE on ICL_SHEETNO=PIC_SHEETNO left join TBCS_CUSUPINFO on PIC_SUPPLIERRESID=CS_CODE) as b ON a.ptcode = b.ptcode left join (select PO_CODE as orderno,PO_PTCODE,PO_CSTATE as detailcstate,PO_CGTIMERQ,CAST(PO_ZXNUM * PO_CTAXUPRICE AS decimal(18, 4)) AS ctamount,PO_CTAXUPRICE AS ctprice from TBPC_PURORDERDETAIL) as c ON a.ptcode = c.PO_PTCODE AND c.detailcstate = '0') as a left join TBPC_IQRCMPPRCRVW as b on a.picno=b.ICL_SHEETNO left join TBPC_PURORDERTOTAL as c on a.orderno=c.PO_CODE left join (select ptc,result,ISAGAIN,bjsj,rn from  ((select PTC,RESULT,ISAGAIN,bjsj as bjsj_infact,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc,bjsj) as rn from View_TBQM_APLYFORITEM) as c where rn<=1)d  left join  (select ptc as PTC_two,bjsj  from (select *,row_number() over(partition by PTC order by  bjsj ) as rn from View_TBQM_APLYFORITEM) as c where rn<=1)f on d.ptc=f.PTC_two)) as d on a.ptcode=d.PTC left join TBPC_PURORDERDETAIL as e on a.ptcode=e.PO_PTCODE) where " + CreateConStr() + " and ((purstate<7 and purstate>=3) or purstate='0') and a.PUR_CSTATE='0'";
            }

            DataTable dtwgm = DBCallCommon.GetDTUsingSqlText(sqlwgm);
            if (dtwgm.Rows.Count > 0)
            {
                lbwgm.Text = "(" + dtwgm.Rows[0]["count"].ToString().Trim() + ")";
            }
            else
            {
                lbwgm.Text = "";
            }

            //算总金额
            //string sqltexttal = "select a.*,b.ICL_IQRDATE,c.PO_ZDDATE,d.RESULT,e.PO_STATE,e.PO_ZXNUM,e.PO_RECGDFZNUM from ((select a.planno,a.ptcode,a.marid,a.marnm,a.margg,a.marcz,a.margb,a.marunit,a.marfzunit,a.length,a.width,a.num,a.fznum,a.rpnum,a.rpfznum,a.fgrid,a.fgrnm,a.fgtime,a.cgrid,a.cgrnm,a.purstate,a.purnote,a.OUTTZNUM,a.OUTTZFZNUM,a.Pue_Closetype,c.orderno,c.ctprice,c.ctamount,b.picno,b.irqdata,a.PUR_CSTATE,a.PUR_TUHAO,a.PUR_ID,a.sqrid,a.sqrnm,a.PR_MAP,a.PR_CHILDENGNAME,a.prstate from (select a.PR_SHEETNO as planno,b.PUR_PTCODE as ptcode,b.PUR_MARID as marid,e.MNAME as marnm,e.GUIGE as margg,e.CAIZHI as marcz,e.GB as margb,e.PURCUNIT as marunit,b.PUR_TECUNIT as marfzunit,b.PUR_LENGTH as length,b.PUR_WIDTH as width,b.PUR_NUM as num,b.PUR_FZNUM as fznum,b.PUR_RPNUM as rpnum,b.PUR_RPFZNUM as rpfznum,b.PUR_PTASMAN as fgrid,c.ST_NAME as fgrnm,b.PUR_PTASTIME as fgtime,b.PUR_CGMAN as cgrid,f.ST_NAME as cgrnm,b.PUR_STATE as purstate,b.PUR_NOTE as purnote,PUR_CSTATE,PUR_TUHAO,PUR_ID,b.Pue_Closetype,a.PR_SQREID as sqrid,d.ST_NAME as sqrnm,PR_MAP,PR_CHILDENGNAME,a.PR_STATE as prstate,OUTTZNUM,OUTTZFZNUM from TBPC_PCHSPLANRVW as a left join TBPC_PURCHASEPLAN as b on a.PR_SHEETNO=b.PUR_PCODE left join TBDS_STAFFINFO as c on b.PUR_PTASMAN=c.ST_ID left join TBDS_STAFFINFO as d on a.PR_SQREID=d.ST_ID left join TBMA_MATERIAL as e on b.PUR_MARID=e.ID left join TBDS_STAFFINFO as f on b.PUR_CGMAN=f.ST_ID left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as g on b.PUR_PTCODE=g.PTCFrom and b.PUR_MARID=g.TzMaterialCode) as a left join (select ICL_SHEETNO as picno,ICL_IQRDATE as irqdata,PIC_PTCODE as ptcode from TBPC_IQRCMPPRCRVW left join TBPC_IQRCMPPRICE on ICL_SHEETNO=PIC_SHEETNO) as b ON a.ptcode = b.ptcode left join (select PO_CODE as orderno,PO_PTCODE,PO_CSTATE as detailcstate,CAST(PO_ZXNUM * PO_CTAXUPRICE AS decimal(18, 4)) AS ctamount,PO_CTAXUPRICE AS ctprice from TBPC_PURORDERDETAIL) as c ON a.ptcode = c.PO_PTCODE AND c.detailcstate = '0') as a left join TBPC_IQRCMPPRCRVW as b on a.picno=b.ICL_SHEETNO left join TBPC_PURORDERTOTAL as c on a.orderno=c.PO_CODE left join (select PTC,RESULT,ISAGAIN,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc) as rn from View_TBQM_APLYFORITEM) as c where rn<=1) as d on a.ptcode=d.PTC left join TBPC_PURORDERDETAIL as e on a.ptcode=e.PO_PTCODE) where " + CreateConStr() + " order by orderno desc,ptcode asc";
            //System.Data.DataTable dttotal = DBCallCommon.GetDTUsingSqlText(sqltexttal);
            //double tot_money = 0;
            //for (int i = 0; i < dttotal.Rows.Count; i++)
            //{
            //    string cta = dttotal.Rows[i]["ctamount"].ToString();
            //    cta = cta == "" ? "0" : cta;
            //    tot_money += Convert.ToDouble(cta);
            //}
            //lb_select_num.Text = Convert.ToString(dttotal.Rows.Count);
            //lb_select_money.Text = string.Format("{0:c}", tot_money);
        }

        private string CreateConStr()
        {
            string sql = "1=1 and (purstate=7 or purstate=6 or purstate=5 or purstate=4 or purstate=3 or purstate=2 or purstate=1 or purstate=0) and purstate is not null and rpnum>0 ";
            string a = rbl_xiatui.SelectedValue;
            string b = rbl_state.SelectedValue;
            switch (a)
            {
                case "0":
                    sql += "and (a.prstate=0 or a.prstate=1 or a.prstate=2 or a.prstate=3 or a.prstate=4) ";
                    break;
                case "1":
                    sql += "and a.prstate=5";
                    break;
                default:
                    break;
            }
            switch (b)
            {
                case "0":
                    sql += "and (purstate=4 or purstate=5 or purstate=6 or purstate=7) ";
                    break;
                case "1":
                    sql += "and (purstate=6 or purstate=7) ";
                    break;
                case "2":
                    sql += "and purstate=7 ";
                    break;
                case "3":
                    sql += "and RESULT='合格' ";
                    break;
                case "4":
                    sql += "and e.PO_STATE='2' ";
                    break;
                default:
                    break;
            }
            foreach (Control control in panel_query.Controls)
            {
                if (control is TextBox)
                {
                    if (((TextBox)control).Text.ToString() != "")
                    {
                        sql += string.Format("and patindex('%{1}%',{0})>0 ", control.ID.Replace("tb_", ""), ((TextBox)control).Text.ToString());
                    }
                }
            }

            if (drp_stu.SelectedIndex != 0)
            {
                sql += "and cgrid=" + drp_stu.SelectedValue + "";
            }
            if (dpldaohuostate.SelectedValue.ToString().Trim() == "1")
            {
                sql += " and a.PO_CGTIMERQ<'" + DateTime.Now.ToString("yyyy-MM-dd").Trim() + "' and (RESULT is null or RESULT='') and e.PO_STATE!='2'";
            }
            if (dplweigoumai.SelectedValue.ToString().Trim() == "1")
            {
                sql += " and ((purstate<7 and purstate>=3) or purstate='0') and a.PUR_CSTATE='0'";
            }
            if (chkzanting.Checked)
            {
                sql += " and Pue_Closetype='3'";
            }
            else
            {
                sql += " and (Pue_Closetype is null or (Pue_Closetype is not null and Pue_Closetype<>'3'))";
            }

            //2016.11.10修改
            if (dpldaohuo_hou.SelectedValue.ToString().Trim() == "1")
            {
                sql += " and a.PO_CGTIMERQ<BJSJ ";
            }
            return sql;
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetBoundData();
        }

        #endregion

        private void BindPeople()
        {
            string sqltext = "";
            sqltext = "select distinct b.ST_NAME,b.ST_ID from TBPC_PURCHASEPLAN as a left join TBDS_STAFFINFO as b on a.PUR_CGMAN=b.ST_ID WHERE a.PUR_CGMAN<>'' and b.ST_PD='0'";
            string dataText = "ST_NAME";
            string dataValue = "ST_ID";
            DBCallCommon.BindDdl(drp_stu, sqltext, dataText, dataValue);
            drp_stu.SelectedIndex = 0;
        }//绑定评审人

        public string get_pur_fg(string fgst)
        {
            string statestr = "";
            if (Convert.ToInt32(fgst) >= 4)
            {
                statestr = "是";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }//分工
        public string get_pur_bjd(string bjdst)
        {
            string statestr = "";
            if (Convert.ToInt32(bjdst) >= 6)
            {
                statestr = "是";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }//比价单 
        public string get_pur_dd(string ddst)
        {
            string statestr = "";
            if (Convert.ToInt32(ddst) >= 7)
            {
                statestr = "是";
            }
            else
            {

                statestr = "否";

            }
            return statestr;
        }//订单
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
        }//质量报检

        protected void Rep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string code = "";
                string sqltext = "";
                string ptcode = "";
                string cstate = "";
                string prstate = "";
                string purcstate = "";
                ptcode = ((HiddenField)e.Item.FindControl("hid_ptcode")).Value;
                cstate = ((HiddenField)e.Item.FindControl("hid_purstate")).Value;
                prstate = ((HiddenField)e.Item.FindControl("hid_prstate")).Value;
                purcstate = ((HiddenField)e.Item.FindControl("hid_cstate")).Value;

                //占用库存
                sqltext = "select PUR_PCODE from TBPC_MARSTOUSEALL where PUR_PTCODE='" + ptcode + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0 && purcstate == "1")
                {
                    code = dt.Rows[0]["PUR_PCODE"].ToString();
                    ((Label)e.Item.FindControl("PUR_ZY")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("PUR_ZY")).Text = "是";
                    ((HyperLink)e.Item.FindControl("hp_zy")).NavigateUrl = "PC_TBPC_Purchaseplan_check_detail.aspx?sheetno=" + Server.UrlEncode(code) + "&ptc=" + Server.UrlEncode(ptcode) + "";
                }
                else
                {
                    ((Label)e.Item.FindControl("PUR_ZY")).Text = "否";
                }

                //相似代用
                sqltext = "select MP_CODE from TBPC_MARREPLACEALL where MP_PTCODE='" + ptcode + "' and  substring(MP_CODE,5,1)='0'";
                dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0 && purcstate == "1")
                {
                    ((Label)e.Item.FindControl("PUR_XSZY")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("PUR_XSZY")).Text = "是";
                    ((HyperLink)e.Item.FindControl("hp_xszy")).NavigateUrl = "~/PC_Data/PC_TBPC_PLAN_PLACE.aspx?ptc=" + Server.UrlEncode(ptcode) + "";
                }
                else
                {
                    ((Label)e.Item.FindControl("PUR_XSZY")).Text = "否";
                }

                //代用
                sqltext = "select MP_CODE from TBPC_MARREPLACEALL where MP_PTCODE='" + ptcode + "' and substring(MP_CODE,5,1)='1'";
                dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0 && purcstate == "0")
                {
                    ((Label)e.Item.FindControl("PUR_DY")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("PUR_DY")).Text = "是";
                    ((HyperLink)e.Item.FindControl("hp_dy")).NavigateUrl = "~/PC_Data/PC_TBPC_PLAN_PLACE.aspx?ptc=" + Server.UrlEncode(ptcode) + "";
                }
                else
                {
                    ((Label)e.Item.FindControl("PUR_DY")).Text = "否";
                }

                Label lb_xt = (Label)e.Item.FindControl("lb_xt");
                if (prstate == "5")
                {
                    lb_xt.Text = "已下推";
                }
                else
                {
                    lb_xt.Text = "未下推";
                }

                if (Convert.ToInt32(cstate) >= 4)
                {
                    string jhpno = ((HiddenField)e.Item.FindControl("hid_planno")).Value;
                    ((Label)e.Item.FindControl("PUR_FG")).ForeColor = System.Drawing.Color.Red;
                    ((HyperLink)e.Item.FindControl("hp_fg")).NavigateUrl = "PC_TBPC_Purchaseplan_assign.aspx?sheetno=" + jhpno + "&ptc=" + ptcode + "";
                }
                if (Convert.ToInt32(cstate) >= 6)
                {
                    string picno = ((Label)e.Item.FindControl("picno")).Text;
                    ((Label)e.Item.FindControl("PUR_BJD")).ForeColor = System.Drawing.Color.Red;
                    ((HyperLink)e.Item.FindControl("hp_bjd")).NavigateUrl = "TBPC_IQRCMPPRCLST_checked_detail.aspx?sheetno=" + picno + "&ptc=" + ptcode + "";
                }
                if (Convert.ToInt32(cstate) >= 7)
                {
                    string orderno = ((Label)e.Item.FindControl("orderno")).Text;
                    ((Label)e.Item.FindControl("PUR_DD")).ForeColor = System.Drawing.Color.Red;
                    ((HyperLink)e.Item.FindControl("hp_dd")).NavigateUrl = "PC_TBPC_PurOrder.aspx?orderno=" + orderno + "&ptc=" + ptcode + "";
                }

                //查看报检信息
                string zlbjjg = ((System.Web.UI.WebControls.Label)e.Item.FindControl("zlbj")).Text;
                if (zlbjjg != "未报检")
                {
                    ((Label)e.Item.FindControl("zlbj")).ForeColor = System.Drawing.Color.Red;
                    string sql = "select AFI_ID from TBQM_APLYFORINSPCT  where UNIQUEID=(select top 1 UNIQUEID from  TBQM_APLYFORITEM where PTC='" + ptcode + "' order by id desc)";
                    System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt1.Rows.Count > 0)
                    {
                        string afiid = dt1.Rows[0]["AFI_ID"].ToString();
                        ((HyperLink)e.Item.FindControl("hp_zlbj")).NavigateUrl = "~/QC_Data/QC_Inspection_Add.aspx?ACTION=UPDATE&NUM=1&ID=" + afiid + "";
                    }
                }

                string state = ((HiddenField)e.Item.FindControl("PO_STATE")).Value;
                string strzx = ((HiddenField)e.Item.FindControl("PO_ZXNUM")).Value;
                double zxnum = Convert.ToDouble(strzx == "" ? "0" : strzx);
                string strdn = ((HiddenField)e.Item.FindControl("PO_RECGDFZNUM")).Value;
                double dnum = Convert.ToDouble(strdn == "" ? "0" : strdn);

                if (state == "2")
                {
                    ((Label)e.Item.FindControl("rukuF")).Text = "已入库";
                    ((Label)e.Item.FindControl("rukuF")).ForeColor = System.Drawing.Color.Red;

                    //如果出现因MTO调整或拆分而导致的部分到货则显示“部分到货(因MTO调整或拆分)”
                    //double numkucun = 0;
                    //string sql001 = "select PUR_PTCODE from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptcode + "' and Pue_Closetype='3'";
                    //System.Data.DataTable dt001 = DBCallCommon.GetDTUsingSqlText(sql001);
                    //if (dt001.Rows.Count > 0)
                    //{
                    //    string sql000 = "select sum(Number) as SQ_NUM from View_SM_Storage where PTC='" + ptcode + "'";
                    //    System.Data.DataTable dt000 = DBCallCommon.GetDTUsingSqlText(sql000);
                    //    if (dt000.Rows.Count > 0)
                    //    {
                    //        numkucun = CommonFun.ComTryDouble(dt000.Rows[0]["SQ_NUM"].ToString().Trim());
                    //    }

                    //    if ((numkucun) < zxnum)
                    //    {
                    //        ((Label)e.Item.FindControl("daohuoF")).Text = "部分到货(任务暂停物料被其他项目使用)";
                    //        ((Label)e.Item.FindControl("daohuoF")).ForeColor = System.Drawing.Color.Red;
                    //    }
                    //    else
                    //    {
                    //        ((Label)e.Item.FindControl("daohuoF")).Text = "已到货";
                    //        ((Label)e.Item.FindControl("daohuoF")).ForeColor = System.Drawing.Color.Red;
                    //    }
                    //}
                    //else
                    //{
                    ((Label)e.Item.FindControl("daohuoF")).Text = "已到货";
                    ((Label)e.Item.FindControl("daohuoF")).ForeColor = System.Drawing.Color.Red;
                    //}
                }
                else if (state == "3")
                {
                    ((Label)e.Item.FindControl("daohuoF")).Text = "已到货";
                    ((Label)e.Item.FindControl("daohuoF")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("rukuF")).Text = "未入库";
                    //((Label)e.Item.FindControl("rukuF")).ForeColor = System.Drawing.Color.red;
                }
                else if (state == "1")
                {
                    if (dnum > 0 && dnum < zxnum)
                    {
                        ((Label)e.Item.FindControl("rukuF")).Text = "部分入库";
                        ((Label)e.Item.FindControl("rukuF")).ForeColor = System.Drawing.Color.Red;
                        ((Label)e.Item.FindControl("daohuoF")).Text = "部分到货";
                        ((Label)e.Item.FindControl("daohuoF")).ForeColor = System.Drawing.Color.Red;
                    }
                    else if (dnum == 0)
                    {
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).Text = "未到货";




                        if ((string.Compare(((System.Web.UI.WebControls.Label)e.Item.FindControl("cgtimerq")).Text.ToString().Trim(), DateTime.Now.ToString("yyyy-MM-dd").Trim())) < 0 && zlbjjg == "未报检")
                        {
                            ((HtmlTableCell)e.Item.FindControl("tdifdaohuo")).BgColor = "Yellow";//#00E600
                        }




                        //((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).ForeColor = System.Drawing.Color.Red;
                        if (zlbjjg != "未报检")
                        {
                            ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).Text = "已到货";
                            ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).ForeColor = System.Drawing.Color.Red;
                        }
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).Text = "未入库";
                        //((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).ForeColor = System.Drawing.Color.Red;
                        HtmlTableCell td2 = (HtmlTableCell)e.Item.FindControl("td2");
                        //if (Convert.ToDateTime(date) < Convert.ToDateTime(datime))
                        //{
                        //    td2.BgColor = "Red";
                        //}
                    }
                }

                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "white";
                }
                else
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "#EFF3FB";
                }

                if (Session["UserDeptID"].ToString().Trim() != "05" && Session["UserDeptID"].ToString().Trim() != "01")
                {
                    lbyqwdh.Visible = false;
                    lbwgm.Visible = false;
                }


                sqltext = "select b.CS_NAME,a.PIC_PRICE from TBPC_IQRCMPPRICE as a left join TBCS_CUSUPINFO as b on a.PIC_SUPPLIERRESID=b.CS_CODE where PIC_PTCODE='" + ptcode + "'";
                dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    ((Label)e.Item.FindControl("changshang")).Text = dt.Rows[0]["CS_NAME"].ToString();
                    //if (Session["POSITION"].ToString().Trim() == "0501" || Session["POSITION"].ToString().Trim() == "0101" || Session["POSITION"].ToString().Trim() == "0102")
                    //{
                    //    ((Label)e.Item.FindControl("danjia")).Text = dt.Rows[0]["PIC_PRICE"].ToString();
                    //}
                    //else
                    //{
                    //    ((Label)e.Item.FindControl("danjia")).Text = "";
                    //}
                }


                if (Session["POSITION"].ToString().Trim() == "0501" || Session["POSITION"].ToString().Trim() == "0101" || Session["POSITION"].ToString().Trim() == "0102")
                {
                    ((Label)e.Item.FindControl("ctprice")).Visible = true;
                    ((Label)e.Item.FindControl("ctamount")).Visible = true;
                }
                else
                {
                    ((Label)e.Item.FindControl("ctprice")).Visible = false;
                    ((Label)e.Item.FindControl("ctamount")).Visible = false;
                }
            }
        }

        protected void btn_search_click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetBoundData();
        }

        protected void dpldaohuostate_click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetBoundData();
        }

        //protected void btn_clear_click(object sender, EventArgs e)
        //{
        //    foreach (Control control in panel_query.Controls)
        //    {
        //        if (control is TextBox)
        //        {
        //            ((TextBox)control).Text = "";
        //        }
        //    }
        //    btn_search_click(null, null);
        //}

        protected void btn_ShowAll_Click(object sender, EventArgs e)
        {
            Response.Redirect("PC_TBPC_PurQuery.aspx");
        }

        protected void btn_Export_Click(object sender, EventArgs e)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText("select a.*,b.ICL_IQRDATE,c.PO_ZDDATE,d.RESULT,e.PO_STATE,e.PO_ZXNUM,e.PO_RECGDFZNUM from ((select a.planno,a.ptcode,a.marid,a.marnm,a.margg,a.marcz,a.margb,a.marunit,a.marfzunit,a.length,a.width,a.num,a.fznum,a.rpnum,a.rpfznum,a.fgrid,a.prsqtime,a.fgrnm,a.fgtime,a.cgrid,a.cgrnm,a.purstate,a.purnote,a.OUTTZNUM,a.OUTTZFZNUM,a.Pue_Closetype,c.orderno,c.PO_CGTIMERQ,c.ctprice,c.ctamount,b.suppliernm,b.picno,b.irqdata,a.PUR_CSTATE,a.PUR_TUHAO,a.PUR_ID,a.sqrid,a.sqrnm,a.PR_MAP,a.PR_CHILDENGNAME,a.prstate,a.CM_FHDATE from (select a.PR_SHEETNO as planno,b.PUR_PTCODE as ptcode,b.PUR_MARID as marid,e.MNAME as marnm,e.GUIGE as margg,e.CAIZHI as marcz,e.GB as margb,e.PURCUNIT as marunit,b.PUR_TECUNIT as marfzunit,b.PUR_LENGTH as length,b.PUR_WIDTH as width,b.PUR_NUM as num,b.PUR_FZNUM as fznum,b.PUR_RPNUM as rpnum,b.PUR_RPFZNUM as rpfznum,b.PUR_PTASMAN as fgrid,c.ST_NAME as fgrnm,b.PUR_PTASTIME as fgtime,b.PUR_CGMAN as cgrid,f.ST_NAME as cgrnm,b.PUR_STATE as purstate,b.PUR_NOTE as purnote,PUR_CSTATE,PUR_TUHAO,PUR_ID,b.Pue_Closetype,a.PR_SQTIME as prsqtime,a.PR_SQREID as sqrid,d.ST_NAME as sqrnm,PR_MAP,PR_CHILDENGNAME,a.PR_STATE as prstate,OUTTZNUM,OUTTZFZNUM,CM_FHDATE from (select top(2000) * from TBPC_PCHSPLANRVW order by PR_SQTIME desc) as a left join TBPC_PURCHASEPLAN as b on a.PR_SHEETNO=b.PUR_PCODE left join TBDS_STAFFINFO as c on b.PUR_PTASMAN=c.ST_ID left join TBDS_STAFFINFO as d on a.PR_SQREID=d.ST_ID left join TBMA_MATERIAL as e on b.PUR_MARID=e.ID left join TBDS_STAFFINFO as f on b.PUR_CGMAN=f.ST_ID left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as g on b.PUR_PTCODE=g.PTCFrom and b.PUR_MARID=g.TzMaterialCode left join (select distinct CM_FHDATE,TSA_ID from TBCM_PLAN as a left join TBCM_BASIC as b on a.ID=b.ID) as h on a.PR_ENGID=h.TSA_ID) as a left join (select ICL_SHEETNO as picno,ICL_IQRDATE as irqdata,PIC_PTCODE as ptcode,CS_NAME as suppliernm from TBPC_IQRCMPPRCRVW left join TBPC_IQRCMPPRICE on ICL_SHEETNO=PIC_SHEETNO left join TBCS_CUSUPINFO on PIC_SUPPLIERRESID=CS_CODE) as b ON a.ptcode = b.ptcode left join (select PO_CODE as orderno,PO_PTCODE,PO_CSTATE as detailcstate,PO_CGTIMERQ,CAST(PO_ZXNUM * PO_CTAXUPRICE AS decimal(18, 4)) AS ctamount,PO_CTAXUPRICE AS ctprice from TBPC_PURORDERDETAIL) as c ON a.ptcode = c.PO_PTCODE AND c.detailcstate = '0') as a left join TBPC_IQRCMPPRCRVW as b on a.picno=b.ICL_SHEETNO left join TBPC_PURORDERTOTAL as c on a.orderno=c.PO_CODE left join (select ptc,result,ISAGAIN,bjsj,rn from  ((select PTC,RESULT,ISAGAIN,bjsj as bjsj_infact,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc,bjsj) as rn from View_TBQM_APLYFORITEM) as c where rn<=1)d  left join  (select ptc as PTC_two,bjsj  from (select *,row_number() over(partition by PTC order by  bjsj ) as rn from View_TBQM_APLYFORITEM) as c where rn<=1)f on d.ptc=f.PTC_two)) as d on a.ptcode=d.PTC left join TBPC_PURORDERDETAIL as e on a.ptcode=e.PO_PTCODE) where " + CreateConStr());//left join TBCS_CUSUPINFO on PIC_SUPPLIERRESID=CS_CODE

            if (DropDownListrange.SelectedIndex == 1)
            {
                dt = DBCallCommon.GetDTUsingSqlText("select a.*,b.ICL_IQRDATE,c.PO_ZDDATE,d.RESULT,e.PO_STATE,e.PO_ZXNUM,e.PO_RECGDFZNUM from ((select a.planno,a.ptcode,a.marid,a.marnm,a.margg,a.marcz,a.margb,a.marunit,a.marfzunit,a.length,a.width,a.num,a.fznum,a.rpnum,a.rpfznum,a.fgrid,a.prsqtime,a.fgrnm,a.fgtime,a.cgrid,a.cgrnm,a.purstate,a.purnote,a.OUTTZNUM,a.OUTTZFZNUM,a.Pue_Closetype,c.orderno,c.PO_CGTIMERQ,c.ctprice,c.ctamount,b.suppliernm,b.picno,b.irqdata,a.PUR_CSTATE,a.PUR_TUHAO,a.PUR_ID,a.sqrid,a.sqrnm,a.PR_MAP,a.PR_CHILDENGNAME,a.prstate,a.CM_FHDATE from (select a.PR_SHEETNO as planno,b.PUR_PTCODE as ptcode,b.PUR_MARID as marid,e.MNAME as marnm,e.GUIGE as margg,e.CAIZHI as marcz,e.GB as margb,e.PURCUNIT as marunit,b.PUR_TECUNIT as marfzunit,b.PUR_LENGTH as length,b.PUR_WIDTH as width,b.PUR_NUM as num,b.PUR_FZNUM as fznum,b.PUR_RPNUM as rpnum,b.PUR_RPFZNUM as rpfznum,b.PUR_PTASMAN as fgrid,c.ST_NAME as fgrnm,b.PUR_PTASTIME as fgtime,b.PUR_CGMAN as cgrid,f.ST_NAME as cgrnm,b.PUR_STATE as purstate,b.PUR_NOTE as purnote,PUR_CSTATE,PUR_TUHAO,PUR_ID,b.Pue_Closetype,a.PR_SQTIME as prsqtime,a.PR_SQREID as sqrid,d.ST_NAME as sqrnm,PR_MAP,PR_CHILDENGNAME,a.PR_STATE as prstate,,OUTTZNUM,OUTTZFZNUM,CM_FHDATE from TBPC_PCHSPLANRVW as a left join TBPC_PURCHASEPLAN as b on a.PR_SHEETNO=b.PUR_PCODE left join TBDS_STAFFINFO as c on b.PUR_PTASMAN=c.ST_ID left join TBDS_STAFFINFO as d on a.PR_SQREID=d.ST_ID left join TBMA_MATERIAL as e on b.PUR_MARID=e.ID left join TBDS_STAFFINFO as f on b.PUR_CGMAN=f.ST_ID left join (select PTCFrom,MaterialCode as TzMaterialCode,sum(TZNUM) as OUTTZNUM,sum(TZFZNUM) as OUTTZFZNUM from View_SM_MTO group by PTCFrom,MaterialCode) as g on b.PUR_PTCODE=g.PTCFrom and b.PUR_MARID=g.TzMaterialCode left join (select distinct CM_FHDATE,TSA_ID from TBCM_PLAN as a left join TBCM_BASIC as b on a.ID=b.ID) as h on a.PR_ENGID=h.TSA_ID) as a left join (select ICL_SHEETNO as picno,ICL_IQRDATE as irqdata,PIC_PTCODE as ptcode,CS_NAME as suppliernm from TBPC_IQRCMPPRCRVW left join TBPC_IQRCMPPRICE on ICL_SHEETNO=PIC_SHEETNO left join TBCS_CUSUPINFO on PIC_SUPPLIERRESID=CS_CODE) as b ON a.ptcode = b.ptcode left join (select PO_CODE as orderno,PO_PTCODE,PO_CSTATE as detailcstate,PO_CGTIMERQ,CAST(PO_ZXNUM * PO_CTAXUPRICE AS decimal(18, 4)) AS ctamount,PO_CTAXUPRICE AS ctprice from TBPC_PURORDERDETAIL) as c ON a.ptcode = c.PO_PTCODE AND c.detailcstate = '0') as a left join TBPC_IQRCMPPRCRVW as b on a.picno=b.ICL_SHEETNO left join TBPC_PURORDERTOTAL as c on a.orderno=c.PO_CODE left join (select ptc,result,ISAGAIN,bjsj,rn from  ((select PTC,RESULT,ISAGAIN,bjsj as bjsj_infact,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc,bjsj) as rn from View_TBQM_APLYFORITEM) as c where rn<=1)d  left join  (select ptc as PTC_two,bjsj  from (select *,row_number() over(partition by PTC order by  bjsj ) as rn from View_TBQM_APLYFORITEM) as c where rn<=1)f on d.ptc=f.PTC_two)) as d on a.ptcode=d.PTC left join TBPC_PURORDERDETAIL as e on a.ptcode=e.PO_PTCODE) where " + CreateConStr());//left join TBCS_CUSUPINFO on PIC_SUPPLIERRESID=CS_CODE
            }

            if (dt.Rows.Count <= 1000)
            {
                string filename = "采购计划导出" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
                HttpContext.Current.Response.Clear();
                //1.读取Excel到FileStream 
                using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("采购计划导出.xls")))
                {
                    IWorkbook wk = new HSSFWorkbook(fs);
                    ISheet sheet0 = wk.GetSheetAt(0);

                    #region 写入数据

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        IRow row = sheet0.CreateRow(i + 2);

                        row.CreateCell(0).SetCellValue(i + 1);
                        row.CreateCell(1).SetCellValue(dr["ptcode"].ToString());
                        row.CreateCell(2).SetCellValue(dr["PR_CHILDENGNAME"].ToString());
                        row.CreateCell(3).SetCellValue(dr["PR_MAP"].ToString());
                        row.CreateCell(4).SetCellValue(dr["margb"].ToString());
                        row.CreateCell(5).SetCellValue(dr["PUR_TUHAO"].ToString());
                        row.CreateCell(6).SetCellValue(dr["marnm"].ToString());
                        row.CreateCell(7).SetCellValue(dr["marid"].ToString());
                        row.CreateCell(8).SetCellValue("规格：" + dr["margg"].ToString() + " ,材质：" + dr["marcz"].ToString() + " ,长度（mm）：" + dr["length"].ToString() + " ,宽度（mm）：" + dr["width"].ToString());
                        row.CreateCell(9).SetCellValue(dr["marunit"].ToString());
                        row.CreateCell(10).SetCellValue(dr["rpnum"].ToString());
                        row.CreateCell(11).SetCellValue(dr["marfzunit"].ToString());
                        row.CreateCell(12).SetCellValue(dr["rpfznum"].ToString());

                        //占用
                        string sqltext = "select PUR_PCODE from TBPC_MARSTOUSEALL where PUR_PTCODE='" + dr["ptcode"].ToString() + "'";
                        DataTable data = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (data.Rows.Count > 0 && dr["PUR_CSTATE"].ToString() == "1")
                        {
                            row.CreateCell(13).SetCellValue("是");
                        }
                        else
                        {
                            row.CreateCell(13).SetCellValue("否");
                        }

                        //相似占用
                        sqltext = "select MP_CODE from TBPC_MARREPLACEALL where MP_PTCODE='" + dr["ptcode"].ToString() + "' and  substring(MP_CODE,5,1)='0'";
                        data = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (data.Rows.Count > 0 && dr["PUR_CSTATE"].ToString() == "1")
                        {
                            row.CreateCell(14).SetCellValue("是");
                        }
                        else
                        {
                            row.CreateCell(14).SetCellValue("否");
                        }

                        //代用
                        sqltext = "select MP_CODE from TBPC_MARREPLACEALL where MP_PTCODE='" + dr["ptcode"].ToString() + "' and  substring(MP_CODE,5,1)='1' ";
                        data = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (data.Rows.Count > 0 && dr["PUR_CSTATE"].ToString() == "0")
                        {
                            row.CreateCell(15).SetCellValue("是");
                        }
                        else
                        {
                            row.CreateCell(15).SetCellValue("否");
                        }

                        //是否下推
                        if (dr["prstate"].ToString() == "5")
                        {
                            row.CreateCell(16).SetCellValue("已下推");
                        }
                        else
                        {
                            row.CreateCell(16).SetCellValue("未下推");
                        }

                        row.CreateCell(17).SetCellValue(dr["purnote"].ToString());
                        row.CreateCell(18).SetCellValue(dr["sqrnm"].ToString());
                        row.CreateCell(19).SetCellValue(dr["cgrnm"].ToString());

                        if (Convert.ToInt32(dr["purstate"].ToString()) >= 4)
                        {
                            row.CreateCell(20).SetCellValue("是");
                        }
                        else
                        {
                            row.CreateCell(20).SetCellValue("否");
                        }
                        row.CreateCell(21).SetCellValue(dr["fgtime"].ToString());

                        if (Convert.ToInt32(dr["purstate"].ToString()) >= 6)
                        {
                            row.CreateCell(22).SetCellValue("是");
                        }
                        else
                        {
                            row.CreateCell(22).SetCellValue("否");
                        }

                        row.CreateCell(23).SetCellValue(dr["ICL_IQRDATE"].ToString() == "" ? "" : Convert.ToDateTime(dr["ICL_IQRDATE"].ToString()).ToString("yyyy-MM-dd"));
                        row.CreateCell(24).SetCellValue(dr["picno"].ToString());

                        sqltext = "select b.CS_NAME,a.PIC_PRICE from TBPC_IQRCMPPRICE as a left join TBCS_CUSUPINFO as b on a.PIC_SUPPLIERRESID=b.CS_CODE where PIC_PTCODE='" + dr["ptcode"].ToString() + "'";
                        data = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (data.Rows.Count > 0)
                        {
                            row.CreateCell(25).SetCellValue(data.Rows[0]["CS_NAME"].ToString());
                            row.CreateCell(26).SetCellValue(data.Rows[0]["PIC_PRICE"].ToString());
                        }
                        else
                        {
                            row.CreateCell(25).SetCellValue("");
                            row.CreateCell(26).SetCellValue("");
                        }

                        if (Convert.ToInt32(dr["purstate"].ToString()) >= 7)
                        {
                            row.CreateCell(27).SetCellValue("是");
                        }
                        else
                        {
                            row.CreateCell(27).SetCellValue("否");
                        }
                        row.CreateCell(28).SetCellValue(dr["PO_ZDDATE"].ToString() == "" ? "" : Convert.ToDateTime(dr["PO_ZDDATE"].ToString()).ToString("yyyy-MM-dd"));
                        row.CreateCell(29).SetCellValue(dr["orderno"].ToString());
                        row.CreateCell(30).SetCellValue(dr["RESULT"].ToString() == "" ? "未报检" : dr["RESULT"].ToString());

                        string state = dr["PO_STATE"].ToString();
                        string strzx = dr["PO_ZXNUM"].ToString();
                        double zxnum = Convert.ToDouble(strzx == "" ? "0" : strzx);
                        string strdn = dr["PO_RECGDFZNUM"].ToString();
                        double dnum = Convert.ToDouble(strdn == "" ? "0" : strdn);
                        if (state == "2")
                        {
                            row.CreateCell(31).SetCellValue("已到货");
                            row.CreateCell(32).SetCellValue("已入库");
                        }
                        else if (state == "3")
                        {
                            row.CreateCell(31).SetCellValue("已到货");
                            row.CreateCell(32).SetCellValue("未入库");
                        }
                        else if (state == "1")
                        {
                            if (dnum > 0 && dnum < zxnum)
                            {
                                row.CreateCell(31).SetCellValue("部分到货");
                                row.CreateCell(32).SetCellValue("部分入库");
                            }
                            else if (dnum == 0)
                            {
                                row.CreateCell(31).SetCellValue("未到货");
                                row.CreateCell(32).SetCellValue("未入库");
                            }
                        }
                        else
                        {
                            row.CreateCell(31).SetCellValue("未到货");
                            row.CreateCell(32).SetCellValue("未入库");
                        }

                        IFont font1 = wk.CreateFont();
                        font1.FontName = "仿宋";//字体
                        font1.FontHeightInPoints = 11;//字号
                        ICellStyle cells = wk.CreateCellStyle();
                        cells.Alignment = HorizontalAlignment.CENTER;
                        cells.SetFont(font1);

                        for (int j = 0; j < 31; j++)
                        {
                            row.Cells[j].CellStyle = cells;
                        }
                    }

                    #endregion

                    sheet0.ForceFormulaRecalculation = true;

                    MemoryStream file = new MemoryStream();
                    wk.Write(file);
                    HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                    HttpContext.Current.Response.End();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导出数据大于1000，请分批导出！');", true);
            }
            GetBoundData();
        }
    }
}
