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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_MenuGai1 : BasicPage
    {
        string username = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            username = Session["UserName"].ToString();
            if (!IsPostBack)
            {
                string depid = Session["UserDeptID"].ToString();
                string code = Session["UserID"].ToString();
            }
            CheckUser(ControlFinder);
            //qjsqsp_mytask();

            GetlbFGDZCZY();//非固定资产转移审批
            GetlbFGDZCRK();//非固定资产入库审批
            GetFGDZCBFSP();//非固定资产报废审批
            GetlbGDZCZY();//固定资产转移审批
            GetlbGDZCRK();//固定资产入库审批
            GetGDZCBFSP();//固定资产报废审批
            GetlbZPJH();//招聘计划
            GetlbLSZPJH();//临时招聘计划
            GetKaohe();//考核管理审批
            GetBZEverage();
            GetpcGDZC();//固定资产
            GetGDZCIN();//固定资产入库
            GetDepartMonth();//部门输入分数
            GetDepJXGZ();//部门绩效工资审核
            GetBGYPSQ();
            GetBGYPPC();
            GetBGYPIN();
            GetBGYPHZSP();
            Getlbczgw();//操作岗位
            GetBGYPSAFE();//办公用品安全库存
            GetYY();//用印管理
            GetMove();
            GetDorApply();
            GetCarBX();
            GetCarWH();
            Getgzyd();//薪酬异动
            Getgzqd();//工资清单
            GetWXSQ();
            GetBYSQ();
            Getycsqnew();//用餐申请

            GetlbNDPX_SQ();//年度配需计划申请
            GetlbNDPX_HZ();//年度培训计划汇总
            GetlbLSPX();//临时培训
            GetSJCandidate();
            GetFGDZCIN();//非固定资产
            GetpcFGDZC();//非固定资产采购
            GetComputer();

            GetlbLZSX();//离职

            GetJXADDSP();//人员绩效审批

            GetJXGZYESP();//绩效工资结余审批
            GetJXGZSYSP();//绩效工资使用审批
            GetZSSDFSP();//住宿水电费审批

            Getcanbusp();//餐补
            Getcanbuyd();//餐补异动
            Getgdgzsp();//固定工资
            Getgdgzscsp();//固定工资删除审批

            GetTravelApply();//差旅申请
            GetTravelDelay();//差旅延期
            GetExpress();//快递管理

            Getbingjianum();//病事假达到上限提醒

            Getqingltx();//清零提醒
            Getlbclrksp();//车辆入库审批
            Getlbclcksp();//车辆出库审批

            Getxcjssp();//薪酬基数审批
            Getjipiao();//机票管理
            Getpower();//权限审批管理
        }
        //车辆入库审批
        private void Getlbclrksp()
        {
            string sql = string.Format("select distinct(ZDR_SJ) from OM_CARRK_SP  where  ((SPR1_ID='{0}' and SPR1_JL='' and SPZT='0') or (SPR2_ID='{0}' and SPR2_JL='' and SPZT='1y'))", Session["UserID"].ToString());
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lbclrksp.Text = "(" + dt.Rows.Count.ToString() + ")";
            }
        }
        //车辆出库审批
        private void Getlbclcksp()
        {
            string sql = string.Format("select distinct(ZDR_SJ) from OM_CARCK_SP  where  ((SPR1_ID='{0}' and SPR1_JL='' and SPZT='0') or (SPR2_ID='{0}' and SPR2_JL='' and SPZT='1y'))", Session["UserID"].ToString());
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lbclcksp.Text = "(" + dt.Rows.Count.ToString() + ")";
            }
        }
        private void GetBGYPHZSP()
        {
            string sql = string.Format("select * from TBOM_BGYPPCHZ where  State ='1' and SHRFID='" + Session["UserId"].ToString() + "'");
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lblBGYPHZSP.Text = "(" + dt.Rows.Count.ToString() + ")";
            }
        }

        private void GetlbFGDZCZY()//非固定资产转移
        {
            string sql = string.Format("select distinct(DH) from TBOM_GDZCTRANSFER as a left join OM_SP as b on a.DH=b.SPFATHERID where TRANSFTYPE='1' and SPFATHERID is not null and SPLX='FGDZCZY' and ((SPR1ID='{0}' and SPR1_JL='' and SPZT='0') or (SPR2ID='{0}' and SPR2_JL='' and SPZT='1y') or (SPR3ID='{0}' and  SPR3_JL='' and SPZT='2y'))", Session["UserID"].ToString());
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lbFGDZCZY.Text = "(" + dt.Rows.Count.ToString() + ")";
            }
        }

        //非固定资产入库审批
        private void GetlbFGDZCRK()
        {
            string sql = string.Format("select distinct(INCODE) from TBOM_GDZCIN as a left join OM_SP as b on a.INCODE=b.SPFATHERID where ID is not null and SPLX='FGDZC' and ((SPR1ID='{0}' and SPR1_JL='' and SPZT='0') or (SPR2ID='{0}' and SPR2_JL='' and SPZT='1y') or (SPR3ID='{0}' and SPR3_JL='' and SPZT='2y'))", Session["UserID"].ToString());
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lbFGDZCRK.Text = "(" + dt.Rows.Count.ToString() + ")";
            }
        }

        //非固定资产报废审批
        protected void GetFGDZCBFSP()
        {
            string sqlgetfgdzcbf = "select count(*) as count from OM_FGDZCBFSP where ((ZDR_ID='" + Session["UserID"].ToString().Trim() + "' and SPZT='0') or (SHR1_ID='" + Session["UserID"].ToString().Trim() + "' and SPZT='1') or (SHR2_ID='" + Session["UserID"].ToString().Trim() + "' and SPZT='2'))";
            DataTable dtgetfgdzcbf = DBCallCommon.GetDTUsingSqlText(sqlgetfgdzcbf);
            if (dtgetfgdzcbf.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbFGDZCBF.Text = "(" + dtgetfgdzcbf.Rows[0]["count"].ToString().Trim() + ")";
            }
        }

        //固定资产转移审批
        private void GetlbGDZCZY()
        {
            string sql = string.Format("select distinct(DH) from TBOM_GDZCTRANSFER as a left join OM_SP as b on a.DH=b.SPFATHERID where TRANSFTYPE='0'  and SPLX='GDZCZY' and ((SPR1ID='{0}' and SPR1_JL='' and SPZT='0') or (SPR2ID='{0}' and SPR2_JL='' and SPZT='1y') or (SPR3ID='{0}' and  SPR3_JL='' and SPZT='2y'))", Session["UserID"].ToString());
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lbGDZCZY.Text = "(" + dt.Rows.Count.ToString() + ")";
            }
        }

        //固定资产入库审批
        private void GetlbGDZCRK()
        {
            string sql = string.Format("select distinct(INCODE) from TBOM_GDZCIN as a left join OM_SP as b on a.INCODE=b.SPFATHERID where ID is not null and SPLX='GDZC' and ((SPR1ID='{0}' and SPR1_JL='' and SPZT='0') or (SPR2ID='{0}' and SPR2_JL='' and SPZT='1y') or (SPR3ID='{0}' and SPR3_JL='' and SPZT='2y'))", Session["UserID"].ToString());
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lbGDZCRK.Text = "(" + dt.Rows.Count.ToString() + ")";
            }
        }

        //固定资产报废审批
        protected void GetGDZCBFSP()
        {
            string sqlgetgdzcbf = "select count(*) as count from OM_GDZCBFSP where ((ZDR_ID='" + Session["UserID"].ToString().Trim() + "' and SPZT='0') or (SHR1_ID='" + Session["UserID"].ToString().Trim() + "' and SPZT='1') or (SHR2_ID='" + Session["UserID"].ToString().Trim() + "' and SPZT='2'))";
            DataTable dtgetgdzcbf = DBCallCommon.GetDTUsingSqlText(sqlgetgdzcbf);
            if (dtgetgdzcbf.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbGDZCBF.Text = "(" + dtgetgdzcbf.Rows[0]["count"].ToString().Trim() + ")";
            }
        }

        private void GetlbNDPX_SQ()
        {
            string userid = Session["UserID"].ToString();
            string sql = "select count(distinct(PX_SJID)) from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID=b.SPFATHERID where SPLX='NDPXJH' and ((SPR1ID='" + userid + "' and SPZT='1') or (SPR2ID ='" + userid + "' and SPZT='1y') or (SPR3ID ='" + userid + "' and SPZT='2y'))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lbNDPX_SQ.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }


        private void GetlbLZSX()
        {
            string userid = Session["UserID"].ToString();
            string sql = "select * from OM_LIZHISHOUXU where (LZ_ZDRID='" + userid + "' and (LZ_GZJJZMR='' or LZ_WPJJZMR='' or LZ_ZLZMR='') and LZ_SPZT='0') or (LZ_PERSONID='" + userid + "' and (LZ_GZJJZMR='' or LZ_WPJJZMR='' or LZ_ZLZMR='') and LZ_SPZT='0') or (LZ_ZJLLID='" + userid + "' and LZ_ZJLLZT='' and (LZ_SPZT='' or LZ_SPZT='0')) or (LZ_SCBZID='" + userid + "' and LZ_SCBZZT='' and LZ_SPZT='1y') or (LZ_CGBZID='" + userid + "' and LZ_CGBZZT='' and LZ_SPZT='1y') or (LZ_JSBZID='" + userid + "' and LZ_JSBZZT='' and LZ_SPZT='1y')  or (LZ_ZLBZID='" + userid + "' and LZ_ZLBZZT='' and LZ_SPZT='1y')  or (LZ_GCSBZID='" + userid + "' and LZ_GCSBZZT='' and LZ_SPZT='1y') or (LZ_SCBBZID='" + userid + "' and LZ_SCBBZZT='' and LZ_SPZT='1y') or (LZ_CWBZID='" + userid + "' and LZ_CWBZZT='' and LZ_SPZT='1y') or (LZ_SBBZID='" + userid + "' and LZ_SBBZZT='' and LZ_SPZT='1y') or (LZ_STJLID='" + userid + "' and LZ_STJLZT='' and LZ_SPZT='1y') or (LZ_CKGLYID='" + userid + "' and LZ_CKGLYZT='' and LZ_SPZT='2y') or (LZ_GKGLYID='" + userid + "' and LZ_GKGLYZT='' and LZ_SPZT='2y') or (LZ_GDZCGLYID='" + userid + "' and LZ_GDZCZT='' and LZ_SPZT='2y') or (LZ_TSGLYID='" + userid + "' and LZ_TSGLYZT='' and LZ_SPZT='2y') or (LZ_DWGLYID='" + userid + "' and LZ_DWGLYZT='' and LZ_SPZT='2y') or (LZ_DZSBGLYID='" + userid + "' and LZ_DZSBZT='' and LZ_SPZT='2y') or (LZ_KQGLYID='" + userid + "' and LZ_KQGLYZT='' and LZ_SPZT='2y') or (LZ_LDGXGLRID='" + userid + "' and LZ_LDGXGLRZT='' and LZ_SPZT='2y') or (LZ_SXGLRID='" + userid + "' and LZ_SXGLRZT='' and LZ_SPZT='2y') or (LZ_GJJGLRID='" + userid + "' and LZ_GJJGLRZT='' and LZ_SPZT='2y') or (LZ_DAGLRID='" + userid + "' and LZ_DAGLRZT='' and LZ_SPZT='2y') or (LZ_GRXXGLYID='" + userid + "' and LZ_GRXXGLYZT='' and LZ_SPZT='2y') or (LZ_ZHBBZID='" + userid + "' and LZ_ZHBSPZT='' and LZ_SPZT='3y') or (LZ_LDID='" + userid + "' and LZ_LDSPZT='' and LZ_SPZT='4y')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lbLZSX.Text = "(" + dt.Rows.Count + ")";
            }
        }

        //驾驶证到期提示
        private void GetSJCandidate()
        {
            string time = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
            string sql = "select count(1) from TBOM_DriverInfo where (dENDDATE<='" + time + "') ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblSJ.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }

        private void GetlbNDPX_HZ()
        {
            string userid = Session["UserID"].ToString();
            string sql = "select count(distinct(PX_SJID1)) from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID1=b.SPFATHERID where SPLX='NDPXJH' and ((SPR1ID='" + userid + "' and SPZT='1') or (SPR2ID ='" + userid + "' and SPZT='1y') or (SPR3ID ='" + userid + "' and SPZT='2y'))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lbNDPX_HZ.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }

        private void GetlbLSPX()
        {
            string userid = Session["UserID"].ToString();
            string sql = "select count(distinct(PX_SJID)) from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID=b.SPFATHERID where SPLX='LSPX' and ((SPR1ID='" + userid + "' and SPZT='1') or (SPR2ID ='" + userid + "' and SPZT='1y') or (SPR3ID ='" + userid + "' and SPZT='2y'))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lbLSPX.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }

        private void GetWXSQ()
        {
            string stId = Session["UserId"].ToString();
            string sql = "select count(1) from TBOM_CARWXSQ where ((STATE='0' and MANAGERID='" + stId + "') or ( STATE='2' and CONTROLLERID='" + stId + "')) and TYPEID='wx' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            lbWXSQ.Text = "(" + dt.Rows[0][0].ToString() + ")";

        }

        private void GetBYSQ()
        {
            string stId = Session["UserId"].ToString();
            string sql = "select count(1) from TBOM_CARWXSQ where ((STATE='0' and MANAGERID='" + stId + "') or ( STATE='2' and CONTROLLERID='" + stId + "')) and TYPEID='by' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            lbBYSQ.Text = "(" + dt.Rows[0][0].ToString() + ")";

        }

        private void GetCarWH()
        {
            string sql = "select count(1) from TBOM_CARINFO as a left join (select CARID, cast(isnull(BYAFTER,0) as float)-cast(isnull(BYYJ,0) as float) as YJGL,PLACEDATE,ROW_NUMBER() OVER(PARTITION by CARID ORDER BY PLACEDATE DESC) AS rownum from TBOM_CARSAFE where TYPEID='by' group by CARID,BYAFTER,BYYJ,PLACEDATE)b on a.CARNUM=b.CARID where b.rowNUM<=1 and MILEAGE>YJGL and IsDel='正常'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblCarWH.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }

        }

        private void GetCarBX()
        {
            string time = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd");
            string sql = "select count(1) from (select a.CARNUM,BXNAME,STARTDATE,ENDDATE,IsDel,ROW_NUMBER() OVER(PARTITION by a.CARNUM,BXNAME ORDER BY STARTDATE DESC) AS rownum  from TBOM_CARBX as a left join TBOM_CARINFO as b on a.CARNUM=b.CARNUM)c where IsDel='正常' and rownum<=1 and ENDDATE<='" + time + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblCarBX.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }

        private void GetDorApply()
        {
            string NAME = Session["UserName"].ToString();
            string sql = "select count(1) from View_TBOM_DORAPPLY where (DORSTATE='0' and MP_REVIEWA='" + NAME + "') or (DORSTATE='1' and MP_REVIEWB='" + NAME + "')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblMove.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }

        private void GetMove()
        {
            string NAME = Session["UserName"].ToString();
            string sql = "select count(1) from OM_RenYuanDiaoDong where ((FIRST_PER='" + NAME.ToString() + "' and MOVE_STATE='0' and MOVE_STATE<MOVE_AUTH_RATING) or (SECOND_PER='" + NAME.ToString() + "' and MOVE_STATE='1' and MOVE_STATE<MOVE_AUTH_RATING) or (THIRD_PER='" + NAME.ToString() + "' and MOVE_STATE='2' and MOVE_STATE<MOVE_AUTH_RATING) or (FOURTH_PER='" + NAME.ToString() + "' and MOVE_STATE='3' and MOVE_STATE<MOVE_AUTH_RATING) or  (FIFTH_PER='" + NAME.ToString() + "' and MOVE_STATE='4' and MOVE_STATE<MOVE_AUTH_RATING) or  (SIXTH_PER='" + NAME.ToString() + "' and MOVE_STATE='5' and MOVE_STATE<MOVE_AUTH_RATING))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblMove.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }

        private void GetBGYPSAFE()
        {
            string sql = "select count(1) from (select a.maId as WLBM,'' as CODE,a.Id as WLCODE,name as WLNAME,canshu as WLMODEL,unit as WLUNIT,a.price as WLPRICE,case when num is null then '0' else num end as num,kc from dbo.TBMA_OFFICETH as a left join TBOM_BGYP_STORE as b on a.Id=b.mId where ((a.kc>b.num) or ((a.kc is not null and a.kc<>'' and a.kc<>'0') and (num is null or num='0')) and a.IsDel='0'))c ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblSafe.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }

        private void GetBZEverage()
        {
            string Id = Session["UserID"].ToString().Trim();
            string sql = "select count(1) from TBDS_BZAVERAGE  where (State ='1' and SPRID='" + Id + "') OR (State='3' and ZDRID='" + Id + "') or ( State='2' and SPRIDB='" + Session["UserId"].ToString() + "') ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblBZEverage.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }

        private void GetKaohe()
        {
            string Id = Session["UserID"].ToString().Trim();
            string sql = "select count(1) from View_TBDS_KaoHe  where (kh_state='1' and kh_Id='" + Id + "') or (kh_state='2' and Kh_SPRA='" + Id + "') or (kh_state='3' and Kh_SPRB='" + Id + "') or (kh_state='4' and Kh_SPRC='" + Id + "') or (kh_state='5' and kh_Id='" + Id + "') or (kh_state='6' and kh_Id='" + Id + "')  or ((kh_state='7' or kh_state='6') and Kh_shrid1='" + Id + "' and kh_shstate1='0' and Kh_shtoltalstate='1') or ((kh_state='7' or kh_state='6') and kh_shstate1='1' and Kh_shrid2='" + Id + "' and kh_shstate2='0' and Kh_shtoltalstate='1') or ((kh_state='7' or kh_state='6') and kh_shstate2='1' and Kh_shrid3='" + Id + "' and kh_shstate3='0' and Kh_shtoltalstate='1') or ((kh_state='7' or kh_state='6') and kh_shstate3='1' and Kh_shrid4='" + Id + "' and kh_shstate4='0' and Kh_shtoltalstate='1')";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblKaohe.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }

        private void GetBGYPSQ()
        {
            string userid = Session["UserId"].ToString();

            string sql = "select count(1) from View_TBOM_BGYPAPPLY where ((REVIEWSTATE='0' and REVIEWID='" + userid + "') or (REVIEWSTATE='1' and APPLYID='" + userid + "') or (REVIEWSTATE='2' and (WLSLS is null or WLSLS='') and " + Session["UserGroup"].ToString() + "='行政专员'))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblBGYPSQ.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }

        private void GetBGYPPC()
        {
            string userid = Session["UserId"].ToString();

            string sql = "select count(1) from TBOM_BGYPPCAPPLY where (SHRFID='" + Session["UserID"].ToString() + "' and STATE='1') or  (SHRSID='" + Session["UserID"].ToString() + "' and STATE='2')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblBGYPPC.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }

        private void GetBGYPIN()
        {
            string userid = Session["UserId"].ToString();

            string sql = "select count(1) from View_TBOM_BGYPPCAPPLYINFO where STATE='6' and STATE_rk is NULL";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblBGYPIN.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }

        //招聘计划
        private void GetlbZPJH()
        {
            string sql = string.Format("select distinct(JH_HZSJ) from OM_ZPJH as a left join OM_SP as b on a.JH_HZSJ=b.SPFATHERID where JH_HZSJ is not null and ((SPR1='{0}' and SPZT='1' ) or (SPR2='{0}' and SPZT='1y') or (SPR3='{0}' and SPZT='2y')) and JH_LX='ND'", username);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lbZPJH.Text = "(" + dt.Rows.Count + ")";
            }
        }

        //临时招聘计划
        private void GetlbLSZPJH()
        {
            string sql = string.Format("select distinct(JH_HZSJ) from OM_ZPJH as a left join OM_SP as b on a.JH_HZSJ=b.SPFATHERID where JH_HZSJ is not null and ((SPR1='{0}' and SPZT='1') or (SPR2='{0}' and SPZT='1y') or (SPR3='{0}' and SPZT='2y')) and JH_LX='LS'", username);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lbLSZPJH.Text = "(" + dt.Rows.Count + ")";
            }
        }

        private void GetpcGDZC()
        {

            string sql = "select count(1) from View_TBOM_GDZCAPPLY where (STATUS='1' and CARRVWAID='" + Session["UserId"].ToString() + "') or ( STATUS='2' and  CARRVWBID='" + Session["UserId"].ToString() + "') and PCTYPE='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblGDZCpc.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }

        private void GetpcFGDZC()
        {

            string sql = "select count(1) from View_TBOM_GDZCAPPLY where (STATUS='1' and CARRVWAID='" + Session["UserId"].ToString() + "') or ( STATUS='2' and  CARRVWBID='" + Session["UserId"].ToString() + "') and PCTYPE='1'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblFGDZCpc.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }
        private void GetGDZCIN()
        {

            if (Session["UserDeptID"].ToString() == "02")
            {
                string sql = "select count(1) from dbo.View_TBOM_GDZCAPPLY as a left join TBOM_GDZCIN as b on a.CODE=b.CODE where b.Id is  null and a.STATUS='6' and a.PCTYPE='0'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows[0][0].ToString() != "0")
                {
                    lblGDZCIN.Text = "(" + dt.Rows[0][0].ToString() + ")";
                }
            }
            else if (Session["UserDeptID"].ToString() == "06")
            {
                string sql = "select count(*) as num from TBOM_GDZCIN WHERE BIANHAO='' and INTYPE='1'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows[0][0].ToString() != "0")
                {
                    lblGDZCIN.Text = "(" + dt.Rows[0][0].ToString() + ")";
                }
            }
        }


        private void GetFGDZCIN()
        {

            if (Session["UserDeptID"].ToString() == "02")
            {
                string sql = "select count(1) from dbo.View_TBOM_GDZCAPPLY as a left join TBOM_GDZCIN as b on a.CODE=b.CODE where b.Id is  null and a.STATUS='6' and a.PCTYPE='1'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows[0][0].ToString() != "0")
                {
                    lblFGDZCIN.Text = "(" + dt.Rows[0][0].ToString() + ")";
                }
            }
            else if (Session["UserDeptID"].ToString() == "06")
            {
                string sql = "select count(*) as num from TBOM_GDZCIN WHERE BIANHAO='' and INTYPE='1'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows[0][0].ToString() != "0")
                {
                    lblFGDZCIN.Text = "(" + dt.Rows[0][0].ToString() + ")";
                }
            }
        }

        private void GetDepJXGZ()
        {
            string Id = Session["UserID"].ToString().Trim();
            string year = DateTime.Now.AddMonths(-1).Year.ToString();
            string month = DateTime.Now.AddMonths(-1).Month.ToString().PadLeft(2, '0');
            string sql = "select count(1) from TBDS_KaoHe_JXList  where (state='1' and SPRID='" + Id + "') or (state='3' and ZDRID='" + Id + "') ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            sql = "select count(1) from (select d.Id from dbo.TBDS_KaoheDeaprtMonth as a left join dbo.TBDS_BZAVERAGE as b on a.Year=b.Year and a.Month=b.Month  left join dbo.TBDS_KaoheDeaprtMonth_Detail as c on a.Context=c.Context left join dbo.TBDS_KaoHe_JXList as d on a.Year=d.Year and a.Month=b.Month and c.DepartId=d.DepId where a.state='2' and b.state='4' and a.Month='" + month + "' and a.Year='" + year + "' and (d.state='2' or d.state='0' or d.state='1' or d.state is null))e where e.Id is null and " + Session["UserGroup"].ToString() + "='人力资源专员'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);

            int i = CommonFun.ComTryInt(dt.Rows[0][0].ToString()) + CommonFun.ComTryInt(dt1.Rows[0][0].ToString());
            if (i.ToString() != "0")
            {
                lblDepJXGZ.Text = "(" + i.ToString() + ")";
            }
        }

        private void GetDepartMonth()
        {
            string Id = Session["UserID"].ToString().Trim();
            string sql = "select count(1) from TBDS_KaoheDeaprtMonth  where (state='1' and SPRID='" + Id + "') or (state='3' and ZDRID='" + Id + "') ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblDepartMonth.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }
        //操作岗位
        private void Getlbczgw()
        {
            string sqltext = "select count(*) as count from OM_SCCZSH_TOTAL where ((SCCZTOL_ZDRID='" + Session["UserID"].ToString().Trim() + "' and SCCZTOL_TOLSTATE='0') or (SCCZTOL_SHRID1='" + Session["UserID"].ToString().Trim() + "' and SCCZTOL_TOLSTATE='1' and SCCZTOL_SHRZT1='0') or (SCCZTOL_SHRID2='" + Session["UserID"].ToString().Trim() + "' and SCCZTOL_TOLSTATE='1' and SCCZTOL_SHRZT2='0' and SCCZTOL_SHRZT1='1') or (SCCZTOL_SHRID3='" + Session["UserID"].ToString().Trim() + "' and SCCZTOL_TOLSTATE='1' and SCCZTOL_SHRZT3='0' and SCCZTOL_SHRZT1='1' and SCCZTOL_SHRZT2='1'))";
            DataTable dttext = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dttext.Rows[0]["count"].ToString().Trim() != "0")
            {
                labelczgw.Text = "(" + dttext.Rows[0]["count"].ToString().Trim() + ")";
            }
        }

        //差旅申请
        private void GetTravelApply()
        {
            string sqlgettravelapply = "select count(*) as count from OM_TravelApply where ((TA_State ='0'or TA_State ='4' or TA_State ='5') and TA_ZDRID='" + Session["UserID"].ToString().Trim() + "') or (TA_State ='1' and TA_SHRIDA='" + Session["UserID"].ToString().Trim() + "') or ( TA_State='2' and TA_SHRIDB='" + Session["UserID"].ToString().Trim() + "')or ( TA_State='3' and TA_SHRIDC='" + Session["UserID"].ToString().Trim() + "')";
            DataTable dtgettravelapply = DBCallCommon.GetDTUsingSqlText(sqlgettravelapply);
            if (dtgettravelapply.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbTravelApply.Text = "(" + dtgettravelapply.Rows[0]["count"].ToString().Trim() + ")";
            }
        }

        //差旅延期
        private void GetTravelDelay()
        {
            string sqlgettraveldelay = "select count(*) as count from OM_TravelDelay where ((TD_State ='0'or TD_State ='5') and TD_ZDRID='" + Session["UserID"].ToString().Trim() + "') or (TD_State ='1' and TD_SHRIDA='" + Session["UserID"].ToString().Trim() + "') or ( TD_State='2' and TD_SHRIDB='" + Session["UserID"].ToString().Trim() + "')or ( TD_State='3' and TD_SHRIDC='" + Session["UserID"].ToString().Trim() + "')";
            DataTable dtgettraveldelay = DBCallCommon.GetDTUsingSqlText(sqlgettraveldelay);
            if (dtgettraveldelay.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbTravelDelay.Text = "(" + dtgettraveldelay.Rows[0]["count"].ToString().Trim() + ")";
            }
        }

        //快递管理
        private void GetExpress()
        {
            string sqlgetexpress = "select count(*) as count from OM_Express where ((E_State ='0'or E_State ='3' or E_State ='5') and E_ZDRID='" + Session["UserID"].ToString() + "') or (E_State ='1' and E_SHRID='" + Session["UserID"].ToString() + "') or ( E_State='2' and E_SurerID='" + Session["UserID"].ToString() + "')";
            DataTable dtgetexpress = DBCallCommon.GetDTUsingSqlText(sqlgetexpress);
            if (dtgetexpress.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbExpress.Text = "(" + dtgetexpress.Rows[0]["count"].ToString().Trim() + ")";
            }
        }

        //用印申请
        private void GetYY()
        {
            string Id = Session["UserID"].ToString().Trim();
            string sql = "select count(1) from OM_YONGYINLIST where ((State ='1' and SPRIDA='" + Id + "') or ( State='2' and SPRIDB='" + Id + "')or ( State='4' and splevel='8' and " + Session["UserID"].ToString() + "='171' )  or (State='4' and splevel<>'8' and " + Session["UserGroup"].ToString() + "='行政专员') or  ( State='3' and SPRIDC='" + Session["UserId"].ToString() + "')or ( State='7' and SPRIDD='" + Id + "'))";
           
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblYY.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }

        //电子设备维修
        private void GetComputer()
        {
            string Id = Session["UserID"].ToString().Trim();
            string sql = "select count(1) from OM_COMPUTERLIST where ((State ='1' and SPRIDA='" + Id + "') or ( State='2' and SPRIDB='" + Id + "') or (State='3' and " + Session["UserGroup"].ToString() + "='管理员'))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblComputer.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }
        //工资异动
        private void Getgzyd()
        {
            string sql = "select count(*) as count from OM_GZTZSP where ((GZTZSP_SQRSTID='" + Session["UserID"].ToString().Trim() + "' and TOTALSTATE='0') or (GZTZSP_SPRID='" + Session["UserID"].ToString().Trim() + "' and TOTALSTATE='1'))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbgzydsp.Text = "(" + dt.Rows[0]["count"].ToString().Trim() + ")";
            }
        }
        //工资清单
        private void Getgzqd()
        {
            string sqlgzqd = "select count(*) as count from OM_GZHSB where ((GZ_SCRID='" + Session["UserID"].ToString().Trim() + "' and (OM_GZSCBZ='' or OM_GZSCBZ is null)) or (GZ_SHRID1='" + Session["UserID"].ToString().Trim() + "' and OM_GZSCBZ='0' and GZ_SHSTATE1='0') or (GZ_SHRID2='" + Session["UserID"].ToString().Trim() + "' and OM_GZSCBZ='0' and GZ_SHSTATE1='1' and GZ_SHSTATE2='0') or (GZ_SHRID3='" + Session["UserID"].ToString().Trim() + "' and OM_GZSCBZ='0' and GZ_SHSTATE2='1' and GZ_SHSTATE3='0'))";
            DataTable dtgzqd = DBCallCommon.GetDTUsingSqlText(sqlgzqd);
            if (dtgzqd.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbgzqd.Text = "(" + dtgzqd.Rows[0]["count"].ToString().Trim() + ")";
            }
        }

        //申请用餐
        private void Getycsqnew()
        {
            string sqlsqycnew = "select count(*) as count from OM_EATNEW where ((eatsqrid='" + Session["UserID"].ToString().Trim() + "' and eatstate='0') or (eatshrid1='" + Session["UserID"].ToString().Trim() + "' and eatstate='1' and eatshstate1='0') or (eatshrid2='" + Session["UserID"].ToString().Trim() + "' and eatstate='1' and eatshstate1='1' and eatshstate2='0') or (eatsqrid='" + Session["UserID"].ToString().Trim() + "' and eatstate='3'))";
            DataTable dtsqycnew = DBCallCommon.GetDTUsingSqlText(sqlsqycnew);
            if (dtsqycnew.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbsqycnew.Text = "(" + dtsqycnew.Rows[0]["count"].ToString().Trim() + ")";
            }
        }

        //人员绩效审批
        private void GetJXADDSP()
        {
            string sqlgetjxadd = "select count(*) as count from OM_JXADDSP where ((creatstid='" + Session["UserID"].ToString().Trim() + "' and totalstate='0') or (shrid1='" + Session["UserID"].ToString().Trim() + "' and totalstate='1' and shstate1='0') or (shrid2='" + Session["UserID"].ToString().Trim() + "' and totalstate='1' and shstate1='1'and shstate2='0'))";
            DataTable dtgetjxadd = DBCallCommon.GetDTUsingSqlText(sqlgetjxadd);
            if (dtgetjxadd.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbjxaddsp.Text = "(" + dtgetjxadd.Rows[0]["count"].ToString().Trim() + ")";
            }
        }

        //绩效工资结余审批
        private void GetJXGZYESP()
        {
            string sqlgetjxadd = "select count(*) as count from OM_JXGZYESP where ((creatstid='" + Session["UserID"].ToString().Trim() + "' and totalstate='0') or (shrid1='" + Session["UserID"].ToString().Trim() + "' and totalstate='1' and shstate1='0') or (shrid2='" + Session["UserID"].ToString().Trim() + "' and totalstate='1' and shstate1='1'and shstate2='0'))";
            DataTable dtgetjxadd = DBCallCommon.GetDTUsingSqlText(sqlgetjxadd);
            if (dtgetjxadd.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbjxgzyesp.Text = "(" + dtgetjxadd.Rows[0]["count"].ToString().Trim() + ")";
            }
        }
        //绩效工资使用审批
        private void GetJXGZSYSP()
        {
            string sqlgetjxsp = "select count(*) as count from OM_JXGZSYSP where ((creatstid='" + Session["UserID"].ToString().Trim() + "' and totalstate='0') or (shrid1='" + Session["UserID"].ToString().Trim() + "' and totalstate='1' and shstate1='0') or (shrid2='" + Session["UserID"].ToString().Trim() + "' and totalstate='1' and shstate1='1'and shstate2='0'))";
            DataTable dtgetjxsp = DBCallCommon.GetDTUsingSqlText(sqlgetjxsp);
            if (dtgetjxsp.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbjxgzsysp.Text = "(" + dtgetjxsp.Rows[0]["count"].ToString().Trim() + ")";
            }
        }
        //住宿水电费审批
        private void GetZSSDFSP()
        {
            string sqlgetsdsp = "select count(*) as count from OM_SDFYSP where ((creatstid='" + Session["UserID"].ToString().Trim() + "' and state='0') or (shrid='" + Session["UserID"].ToString().Trim() + "' and state='1'))";
            DataTable dtgetsdsp = DBCallCommon.GetDTUsingSqlText(sqlgetsdsp);
            if (dtgetsdsp.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbZSSDFSP.Text = "(" + dtgetsdsp.Rows[0]["count"].ToString().Trim() + ")";
            }
        }

        //餐补
        private void Getcanbusp()
        {
            string sqlgetcbadd = "select count(*) as count from OM_Canbusp where ((SCRID='" + Session["UserID"].ToString().Trim() + "' and state='0') or (SHRID1='" + Session["UserID"].ToString().Trim() + "' and state='1' and SHSTATE1='0') or (SHRID2='" + Session["UserID"].ToString().Trim() + "' and state='1' and SHSTATE1='1' and SHSTATE2='0') or (SHRID3='" + Session["UserID"].ToString().Trim() + "' and state='1' and SHSTATE1='1' and SHSTATE2='1' and SHSTATE3='0'))";
            DataTable dtgetcbadd = DBCallCommon.GetDTUsingSqlText(sqlgetcbadd);
            if (dtgetcbadd.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbcanbusp.Text = "(" + dtgetcbadd.Rows[0]["count"].ToString().Trim() + ")";
            }
        }

        //餐补异动
        private void Getcanbuyd()
        {
            string sql = "select count(*) as count from OM_CanBuYDSP where ((SQR_ID='" + Session["UserID"].ToString().Trim() + "' and TotalState='0') or (SPR_ID='" + Session["UserID"].ToString().Trim() + "' and TotalState='1'))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbcanbuydsp.Text = "(" + dt.Rows[0]["count"].ToString().Trim() + ")";
            }
        }

        //固定工资
        protected void Getgdgzsp()
        {
            string sqlgetgdgz = "select count(*) as count from OM_GDGZSP where ((XGRST_ID='" + Session["UserID"].ToString().Trim() + "' and TOL_TOLSTATE='0') or (TOL_SHRID1='" + Session["UserID"].ToString().Trim() + "' and TOL_TOLSTATE='1' and TOL_SHRZT1='0') or (TOL_SHRID2='" + Session["UserID"].ToString().Trim() + "' and TOL_TOLSTATE='1' and TOL_SHRZT1='1' and TOL_SHRZT2='0') or(TOL_SHRID3='" + Session["UserID"].ToString().Trim() + "' and TOL_TOLSTATE='1' and TOL_SHRZT2='1' and TOL_SHRZT3='0'))";
            DataTable dtgetgdgz = DBCallCommon.GetDTUsingSqlText(sqlgetgdgz);
            if (dtgetgdgz.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbgdgzsp.Text = "(" + dtgetgdgz.Rows[0]["count"].ToString().Trim() + ")";
            }
        }


        //固定工资删除审批
        protected void Getgdgzscsp()
        {
            string sqlgetgdgzsc = "select count(*) as count from OM_GDGZSCSP where ((ZDR_ID='" + Session["UserID"].ToString().Trim() + "' and SPZT='0') or (SHR1_ID='" + Session["UserID"].ToString().Trim() + "' and SPZT='1') or (SHR2_ID='" + Session["UserID"].ToString().Trim() + "' and SPZT='2'))";
            DataTable dtgetgdgzsc = DBCallCommon.GetDTUsingSqlText(sqlgetgdgzsc);
            if (dtgetgdgzsc.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbgdgzscsp.Text = "(" + dtgetgdgzsc.Rows[0]["count"].ToString().Trim() + ")";
            }
        }

        private void Getbingjianum()
        {
            string sqltext = "select count(*) as count from (select KQ_ST_ID,ST_DEPID,ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1,sum(KQ_GNCC) as KQ_GNCC,sum(KQ_GWCC) as KQ_GWCC,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_KUANGG) as KQ_KUANGG,sum(KQ_DAOXIU) as KQ_DAOXIU,sum(KQ_CHANJIA) as KQ_CHANJIA,sum(KQ_PEICHAN) as KQ_PEICHAN,sum(KQ_HUNJIA) as KQ_HUNJIA,sum(KQ_SANGJIA) as KQ_SANGJIA,sum(KQ_GONGS) as KQ_GONGS,sum(KQ_NIANX) as KQ_NIANX,sum(KQ_BEIYONG1) as KQ_BEIYONG1,sum(KQ_BEIYONG2) as KQ_BEIYONG2,sum(KQ_BEIYONG3) as KQ_BEIYONG3,sum(KQ_BEIYONG4) as KQ_BEIYONG4,sum(KQ_BEIYONG5) as KQ_BEIYONG5,sum(KQ_BEIYONG6) as KQ_BEIYONG6,sum(KQ_QTJIA) as KQ_QTJIA,sum(KQ_JIEDIAO) as KQ_JIEDIAO,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZHIBAN) as KQ_ZHIBAN,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_ZHONGB) as KQ_ZHONGB,sum(KQ_CBTS) as KQ_CBTS,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_CHUQIN) as KQ_CHUQIN from View_OM_KQTJ where KQ_DATE like '" + DateTime.Now.Year.ToString().Trim() + "-%' group by KQ_ST_ID,ST_DEPID,ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1)t where (KQ_BINGJ+KQ_SHIJ)>=20";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbbingjiatx.Text = "(" + dt.Rows[0]["count"].ToString().Trim() + ")";
            }
        }


        private void Getqingltx()
        {
            string sqltextql = "select * from OM_QINGDATE";
            string strqldate = "";
            System.Data.DataTable dtql = DBCallCommon.GetDTUsingSqlText(sqltextql);
            if (dtql.Rows.Count > 0)
            {
                strqldate = dtql.Rows[0]["nextqingldate"].ToString().Trim();
            }
            if (strqldate != "")
            {
                DateTime dateql;
                DateTime datenow;
                dateql = DateTime.Parse(strqldate);
                datenow = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd").Trim());
                decimal monthnum = dateql.Month - datenow.Month;
                decimal yearnum = dateql.Year - datenow.Year;
                decimal totalmonthnum = yearnum * 12 + monthnum;
                if (totalmonthnum <= 1)
                {
                    lbqingltx.Text = "(1)";
                }
            }
        }


        //薪酬基数
        protected void Getxcjssp()
        {
            string sqlgetxcxs = "select count(*) as count from OM_SALARYBASEDATASP where ((CZRST_ID='" + Session["UserID"].ToString().Trim() + "' and TOL_TOLSTATE='0') or (TOL_SHRID1='" + Session["UserID"].ToString().Trim() + "' and TOL_TOLSTATE='1' and TOL_SHRZT1='0') or (TOL_SHRID2='" + Session["UserID"].ToString().Trim() + "' and TOL_TOLSTATE='1' and TOL_SHRZT1='1' and TOL_SHRZT2='0') or(TOL_SHRID3='" + Session["UserID"].ToString().Trim() + "' and TOL_TOLSTATE='1' and TOL_SHRZT2='1' and TOL_SHRZT3='0'))";
            DataTable dtgetxcxs = DBCallCommon.GetDTUsingSqlText(sqlgetxcxs);
            if (dtgetxcxs.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbxcxssp.Text = "(" + dtgetxcxs.Rows[0]["count"].ToString().Trim() + ")";
            }
        }

        //机票管理
        protected void Getjipiao()
        {
            string sql = "select count(*) as count from AuditNew as a left join JipiaoContent as b on a.auditno=b.jipiaocontentno where ((totalstate='1' and ((auditperid1=" + Session["UserID"].ToString().Trim() + " and auditstate1='0') or (auditperid2=" + Session["UserID"].ToString().Trim() + " and auditstate1='1' and auditstate2='0') or (auditperid3=" + Session["UserID"].ToString().Trim() + " and auditstate1='1' and auditstate2='1' and auditstate3='0'))) or (totalstate='0' and addperid=" + Session["UserID"].ToString().Trim() + ") or (totalstate='3' and addperid=" + Session["UserID"].ToString().Trim() + ") or (totalstate='2' and fankuistate='0' and fankui='是' and addperid=" + Session["UserID"].ToString().Trim() + ")) and audittype='机票申请'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbjipiao.Text = "(" + dt.Rows[0]["count"].ToString().Trim() + ")";
            }
        }

        //权限审批管理
        protected void Getpower()
        {
            string sql = "select count(*) as count from AuditNew as a left join JipiaoContent as b on a.auditno=b.jipiaocontentno where ((totalstate='1' and ((auditperid1=" + Session["UserID"].ToString().Trim() + " and auditstate1='0') or (auditperid2=" + Session["UserID"].ToString().Trim() + " and auditstate1='1' and auditstate2='0') or (auditperid3=" + Session["UserID"].ToString().Trim() + " and auditstate1='1' and auditstate2='1' and auditstate3='0'))) or (totalstate='0' and addperid=" + Session["UserID"].ToString().Trim() + ") or (totalstate='3' and addperid=" + Session["UserID"].ToString().Trim() + ") or (totalstate='2' and fankuistate='0' and fankui='是' and addperid=" + Session["UserID"].ToString().Trim() + ")) and audittype='权限变更申请'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbpower.Text = "(" + dt.Rows[0]["count"].ToString().Trim() + ")";
            }
        }

    }
}
