using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ZCZJ_DPF.MT_Data
{
    public partial class MT_Menu1 : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session.IsNewSession)
            {
                Session.Abandon();

                Application.Lock();
                Application["online"] = (int)Application["online"] - 1;
                Application.UnLock();
                Response.Write("<script>if(window.parent!=null)window.parent.location.href='../Default.aspx';else{window.location.href='./Default.aspx'} </script>");

            }

            if (!IsPostBack)
            {
                InitUrl();
                OMdata();

            }

            CheckUser(ControlFinder);

            string sql = string.Format("select ID from View_CM_Task where CM_SPSTATUS='1' and ((CM_PS1='{0}' and CM_PSYJ1='1') or (CM_PS2='{0}' and CM_PSYJ2='1' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='1' and CM_PSYJ1='2' and CM_PSYJ2='2')) group by ID", Session["UserID"].ToString());
            if (DBCallCommon.GetDTUsingSqlText(sql).Rows.Count > 0)
            {
                jyjhd.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";

            }
            else
            {
                jyjhd.Visible = false;
                HyperLink1.Visible = false;
            }


            sql = string.Format("select CM_FID from View_CM_FaHuo where CM_CONFIRM='1' and ((CM_MANCLERK='{0}') or (CM_BMZG='{0}' and CM_YJ1='1') or (CM_GSLD='{0}' and CM_YJ2='1' and CM_YJ1='2')) group by CM_FID", Session["UserID"].ToString());

            if (DBCallCommon.GetDTUsingSqlText(sql).Rows.Count > 0)
            {
                fhtz.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";

            }
            else
            {
                fhtz.Visible = false;
                HyperLink3.Visible = false;
            }


            //string carapply = string.Format("select ID from View_TBOM_CARAPLLRVW  where (FIRSTMAN='" + Session["UserID"].ToString() + "' and STATE='0') or (SECONDMAN='" + Session["UserID"].ToString() + "' and STATE='1') or (THIRDMAN='" + Session["UserID"].ToString() + "' and STATE='2')  group by ID", Session["UserID"].ToString());
            //lblCars.Text = "（" + DBCallCommon.GetDTUsingSqlText(carapply).Rows.Count.ToString() + "）";

            GetTask();
            getxbjsh();
            GET_WXSP();
            GET_WXBJSP();
            Get_CGSP1();
            GET_SBCG();

            GetMyBaoJian();
            Reject_Pro();
            //getwlzy();
            GET_FYSP();
            GET_FINISHEDOUT();
            GetCusup_Review();
            GetlbHTSP();
            GetKaiPiao();
            GET_FINISHED();

            GetlbLXD();
            GetlbCLD();
            //GET_shitang();
            Get_cars();
            GetBackTask();

            CghtSP();//采购合同

            getdydsh();//代用单


            getxyjhbg();//需用计划变更
            GetInnerAudit();
            GetGroupAudit();
            GetExterAudit();
            GetlbTZTHTZD();//图纸替换通知单
            GetKaohe();//考核


            GetDepJXGZ();//部门绩效工资审核
            GetDepartMonth();//部门考核
            GetBZEverage();//班组平均数

            Getlbczgw();//操作岗位
            Getbgtz();//计划单变更审批
            Getzbtz();//计划单增补
            GetlbQXPS();//计划单取消

            Getgzyd();//薪酬异动
            Getgzqd();//工资清单
            GetBGYPSQ();
            GetBGYPPC();
            GetBGYPIN();
            GetlbNDPX_SQ();//年度配需计划申请

            GetlbNDPX_HZ();//年度配需计划汇总
            GetlbLSPX();//临时培训
            GetWXSQ();//维修申请
            GetBYSQ();//保养申请
            Getycsqnew();//用餐申请
            GetYY();

            GetTravelApply();//差旅申请
            GetTravelDelay();//差旅延期
            GetExpress();//快递管理

            GetComputer();//电子设备维修

            GetlbLZSX();//离职
            GetMove();//人员调动审批表

            GetJXADDSP();//人员绩效增加审批

            GetJXGZYESP();//绩效工资结余审批
            GetJXGZSYSP();//绩效工资使用审批
            GetZSSDFSP();//住宿水电费审批
            Getcanbusp();//餐补
            Getgdgzsp();//固定工资
            Getgdgzscsp();//固定工资删除审批

            GetGDZCBFSP();//固定资产报废审批
            GetFGDZCBFSP();//非固定资产报废审批

            GetpcFGDZC();//非固定资产采购
            GetpcGDZC();//固定资产采购

            Get_HTSP();//设备合同审批
            GetBGYPHZSP();
            GetlbCLD_SP();
            GetlbCLD_FG();
            GetlbCLD_YYFX();
            GetlbCLD_CLYJ();
            GetlbCLD_CLFA();
            GetlbCLD_CLJG();
            GetlbTJ();
            GetTargetAnalyze();

            Getxcjssp();//薪酬基数审批

            FixnofuAudit(); //固定设备资产入库审批

            GetFixfuAudit(); //固定设备资产入库审批

            Getcanbuyd();//餐补异动

            PC_Pur_inform_commit();//采购信息交流
            Getpower();//权限审批管理


            GetlbGDZCZY();//固定资产转移审批
        }

        private void GetlbLZSX()
        {
            string userid = Session["UserID"].ToString();
            string sql = "select * from OM_LIZHISHOUXU where (LZ_ZDRID='" + userid + "' and (LZ_GZJJZMR='' or LZ_WPJJZMR='' or LZ_ZLZMR='') and LZ_SPZT='0') or (LZ_PERSONID='" + userid + "' and (LZ_GZJJZMR='' or LZ_WPJJZMR='' or LZ_ZLZMR='') and LZ_SPZT='0') or (LZ_ZJLLID='" + userid + "' and LZ_ZJLLZT='' and (LZ_SPZT='' or LZ_SPZT='0')) or (LZ_SCBZID='" + userid + "' and LZ_SCBZZT='' and LZ_SPZT='1y') or (LZ_CGBZID='" + userid + "' and LZ_CGBZZT='' and LZ_SPZT='1y') or (LZ_JSBZID='" + userid + "' and LZ_JSBZZT='' and LZ_SPZT='1y') or (LZ_ZLBZID='" + userid + "' and LZ_ZLBZZT='' and LZ_SPZT='1y') or  (LZ_GCSBZID='" + userid + "' and LZ_GCSBZZT='' and LZ_SPZT='1y') or (LZ_SCBBZID='" + userid + "' and LZ_SCBBZZT='' and LZ_SPZT='1y') or (LZ_CWBZID='" + userid + "' and LZ_CWBZZT='' and LZ_SPZT='1y') or (LZ_SBBZID='" + userid + "' and LZ_SBBZZT='' and LZ_SPZT='1y') or (LZ_STJLID='" + userid + "' and LZ_STJLZT='' and LZ_SPZT='1y') or (LZ_CKGLYID='" + userid + "' and LZ_CKGLYZT='' and LZ_SPZT='2y') or (LZ_GKGLYID='" + userid + "' and LZ_GKGLYZT='' and LZ_SPZT='2y') or (LZ_GDZCGLYID='" + userid + "' and LZ_GDZCZT='' and LZ_SPZT='2y') or (LZ_TSGLYID='" + userid + "' and LZ_TSGLYZT='' and LZ_SPZT='2y') or (LZ_DWGLYID='" + userid + "' and LZ_DWGLYZT='' and LZ_SPZT='2y') or (LZ_DZSBGLYID='" + userid + "' and LZ_DZSBZT='' and LZ_SPZT='2y') or (LZ_KQGLYID='" + userid + "' and LZ_KQGLYZT='' and LZ_SPZT='2y') or (LZ_LDGXGLRID='" + userid + "' and LZ_LDGXGLRZT='' and LZ_SPZT='2y') or (LZ_SXGLRID='" + userid + "' and LZ_SXGLRZT='' and LZ_SPZT='2y') or (LZ_GJJGLRID='" + userid + "' and LZ_GJJGLRZT='' and LZ_SPZT='2y') or (LZ_DAGLRID='" + userid + "' and LZ_DAGLRZT='' and LZ_SPZT='2y') or (LZ_GRXXGLYID='" + userid + "' and LZ_GRXXGLYZT='' and LZ_SPZT='2y') or (LZ_ZHBBZID='" + userid + "' and LZ_ZHBSPZT='' and LZ_SPZT='3y') or (LZ_LDID='" + userid + "' and LZ_LDSPZT='' and LZ_SPZT='4y')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lbLZSX.Text = "(" + dt.Rows.Count + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink135.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink70.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink68.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink93.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink69.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink98.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink64.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink7.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink47.Visible = false;
            }
        }



        private void GetWXSQ()
        {
            string stId = Session["UserId"].ToString();
            string sql = "select count(1) from TBOM_CARWXSQ where ((STATE='0' and MANAGERID='" + stId + "') or ( STATE='2' and CONTROLLERID='" + stId + "')) and TYPEID='wx' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            lbWXSQ.Text = "(" + dt.Rows[0][0].ToString() + ")";


            string sql2 = "select count(1) from TBOM_CARWXSQ where ((STATE='0' and MANAGERID='" + stId + "') or ( STATE='2' and CONTROLLERID='" + stId + "')) and TYPEID='by' ";
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql2);
            if (dt.Rows[0][0].ToString().Trim() == "0" && dt2.Rows[0][0].ToString().Trim() == "0")
            {
                HyperLink50.Visible = false;
            }

        }

        private void GetBYSQ()
        {
            string stId = Session["UserId"].ToString();
            string sql = "select count(1) from TBOM_CARWXSQ where ((STATE='0' and MANAGERID='" + stId + "') or ( STATE='2' and CONTROLLERID='" + stId + "')) and TYPEID='by' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            lbBYSQ.Text = "(" + dt.Rows[0][0].ToString() + ")";

            string sql2 = "select count(1) from TBOM_CARWXSQ where ((STATE='0' and MANAGERID='" + stId + "') or ( STATE='2' and CONTROLLERID='" + stId + "')) and TYPEID='wx' ";
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql2);
            if (dt.Rows[0][0].ToString().Trim() == "0" && dt2.Rows[0][0].ToString().Trim() == "0")
            {
                HyperLink50.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink48.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink49.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink44.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink45.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink46.Visible = false;
            }
        }


        //变更评审
        private void Getbgtz()
        {
            string sql = string.Format("select CH_ID from View_CM_Change where CM_TYPE='0' and CM_STATE='1' and ((CM_MANCLERK='{0}') or (CM_PS1='{0}' and CM_PSYJ1='1') or (CM_PS2='{0}' and CM_PSYJ2='1' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='1' and CM_PSYJ1='2' and CM_PSYJ2='2'))", Session["UserID"].ToString());
            if (DBCallCommon.GetDTUsingSqlText(sql).Rows.Count > 0)
            {
                bgtz.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
            }
            //没有任务就不可见
            else
            {
                HyperLink2.Visible = false;
            }
        }

        //增补审批
        private void Getzbtz()
        {
            string sql = string.Format("select CH_ID from View_CM_Change where CM_TYPE='1' and CM_STATE='1' and ((CM_PS1='{0}' and CM_PSYJ1='1') or (CM_PS2='{0}' and CM_PSYJ2='1' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='1' and CM_PSYJ1='2' and CM_PSYJ2='2'))", Session["UserID"].ToString());
            if (DBCallCommon.GetDTUsingSqlText(sql).Rows.Count > 0)
            {
                zbtz.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
            }
            //没有任务就不可见
            else
            {
                HyperLink34.Visible = false;
            }
        }

        //取消审批
        private void GetlbQXPS()
        {
            string username = Session["UserName"].ToString();
            string sql = " select count(SPID) from CM_SP where SPR1='" + username + "' and SPZT='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lbQXPS.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink40.Visible = false;
            }
        }

        private void Get_cars()
        {
            string sqlText_cars = string.Format("select count(*) from View_TBOM_CARAPLLRVW where (FIRSTMAN='{0}' and STATE='0') or (SECONDMAN='{0}' and STATE='1') or (THIRDMAN='{0}' and STATE='2')", Session["UserID"].ToString());
            SqlDataReader dr_cars = DBCallCommon.GetDRUsingSqlText(sqlText_cars);
            if (dr_cars.Read())
            {
                if (dr_cars[0].ToString() == "0")
                {
                    lblCars.Visible = false;
                    HyperLink5.Visible = false;
                }
                else
                {
                    lblCars.Text = "(" + dr_cars[0].ToString() + ")";
                }
            }
        }

        /// <summary>
        /// 需用计划驳回
        /// </summary>
        private void GetBackTask()
        {
            string sql = "select * from TBPC_PLAN_BACK where state='0' and sqrid='" + Session["UserID"] + "'";

            lblBack.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count + "）";
            if (DBCallCommon.GetDTUsingSqlText(sql).Rows.Count == 0)
            {
                HyperLink29.Visible = false;
            }
        }
        //开票管理
        private void GetKaiPiao()
        {
            string sql = "select count(1) from (select * from CM_KAIPIAO as d left join (select a.cId , stuff((select sprId+',' from CM_KAIPIAO_HUISHEN b where b.cId =a.cId and( b.result is null or b.result='') for xml path('')),1,0,',') 'sprId ' from CM_KAIPIAO_HUISHEN  a  group by  a.cId)c on d.KP_TaskID=c.cId)e where ((KP_SPSTATE='1' and KP_SHRIDA='" + Session["UserID"].ToString() + "') or (KP_SPSTATE='2' and KP_SHRIDB='" + Session["UserID"].ToString() + "') or (KP_HSSTATE='1' and sprId like '%," + Session["UserID"].ToString() + ",%'))";
            if (DBCallCommon.GetDTUsingSqlText(sql).Rows[0][0].ToString() == "0")
            {
                lblKaiPiao.Visible = false;
                HyperLink15.Visible = false;
            }
            else
            {
                lblKaiPiao.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows[0][0].ToString() + "）";
            }

        }

        private void CghtSP()//采购合同审批
        {
            string username = Session["UserName"].ToString();
            string sql = string.Format("select HT_ID from PC_CGHT where (HT_SHR1='{0}' and (HT_SHR1_JL is null or HT_SHR1_JL='') and HT_SPZT='0') or (HT_SHR2='{0}' and (HT_SHR2_JL='' or HT_SHR2_JL is null) and HT_SPZT='1y') or (HT_SHR3='{0}' and (HT_SHR3_JL='' or HT_SHR3_JL is null) and HT_SPZT='2y') or (HT_SHRCG='{0}' and (HT_SHRCG_JL is null or HT_SHRCG_JL='') and HT_SPZT='0') or (HT_SHRShenC='{0}' and (HT_SHRShenC_JL is null or HT_SHRShenC_JL='') and HT_SPZT='0') or (HT_SHRJS='{0}' and (HT_SHRJS_JL='' or HT_SHRJS_JL is null) and HT_SPZT='0') or (HT_SHRZ='{0}' and (HT_SHRZ_JL='' or HT_SHRZ_JL is null) and HT_SPZT='0') or (HT_SHRShiC ='{0}' and (HT_SHRShiC_JL is null or HT_SHRShiC_JL='') and  HT_SPZT='0') or (HT_SHRCW='{0}' and (HT_SHRCW_JL='' or HT_SHRCW_JL is null) and HT_SPZT='0') or (HT_SHRFZ='{0}' and (HT_SHRFZ_JL is null or HT_SHRFZ_JL ='') and HT_SPZT='5y')", username);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lbCGHTGL.Text = "(" + dt.Rows.Count.ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink35.Visible = false;
            }
        }

        private void GetlbHTSP()
        {
            string sql = "select count(CR_ID) from View_TBCR_View_Detail_ALL where CRD_PSYJ='0' and CRD_PID='" + Session["UserID"].ToString() + "' and CR_PSZT in ('1','2') and CRD_PIDTYPE='1'";
            if (Session["UserDeptID"].ToString() != "01")
            {
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows[0][0].ToString() == "0")
                {
                    lbHTSP.Visible = false;
                    HyperLink20.Visible = false;
                }
                else
                {

                    lbHTSP.Text = "(" + dt.Rows[0][0] + ")";
                }
            }
            else
            {
                sql = "select distinct(CR_ID) from View_TBCR_View_Detail_ALL where CRD_PSYJ='0' and CRD_PID='" + Session["UserID"].ToString() + "' and CR_PSZT in ('1','2') and CRD_PIDTYPE='1'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                int num = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    string sql1 = "select CR_ID from View_TBCR_View_Detail_ALL where CRD_PSYJ='0' and CRD_PID not in ('1','2','310','311') and CR_PSZT in ('1','2') and CRD_PIDTYPE='1' and CR_ID='" + dr["CR_ID"].ToString() + "'";
                    DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
                    if (dt1.Rows.Count == 0)
                    {
                        num++;
                    }
                }
                if (num == 0)
                {
                    lbHTSP.Visible = false;
                    HyperLink20.Visible = false;
                }
                else
                {
                    lbHTSP.Text = "(" + num.ToString() + ")";
                }
            }
        }

        private void OMdata()
        {
            int om = 0;
            string om_lzsx = "";
            int om_yggx = 0;
            om_lzsx = OM_LZSX(Session["UserName"].ToString());
            if (om_lzsx != "")
            {
                Convert.ToInt32(om_lzsx);
                om_yggx += Convert.ToInt32(om_lzsx);
                om += om_yggx;
            }
            List<TreeNode> tnlist = new List<TreeNode>();
            TreeNode tnOM = new TreeNode("(" + om.ToString() + ")" + "人事行政管理");
            //tvOM.Nodes.Add(tnOM);
            tnlist.Add(tnOM);
            TreeNode tnYGXX = new TreeNode("(" + om_yggx.ToString() + ")" + "员工关系管理");
            tnOM.ChildNodes.Add(tnYGXX);
            TreeNode tnLZSX = new TreeNode("(" + om_lzsx + ")" + "员工离职手续办理", "员工离职手续办理", "", "../OM_Data/OM_LZSQJSX.aspx", "");
            tnYGXX.ChildNodes.Add(tnLZSX);
        }

        private string OM_LZSX(string username)//离职手续办理
        {
            string om_lzsx = "";
            List<string> list = new List<string>();
            string sql = "select ST_ID,ST_NAME,DEP_NAME,DEP_POSITION ,ST_DEPID from View_TBDS_STAFFINFO where DEP_POSITION='部长' or DEP_POSITION='副总经理' or DEP_POSITION='总经理' or ST_NAME='李圆'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr["ST_NAME"].ToString());
            }
            if (list.Contains(username))
            {
                if (Session["UserName"].ToString() == "蔡伟疆")
                {
                    sql = "select count(*) from OM_LIZHISHOUXU where LZ_ZHBSHZT='0' and (LZ_ZHBSPZT='' or LZ_ZHBSPZT is null)";
                    DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                    DataRow dr1 = dt1.Rows[0];
                    om_lzsx = dr1[0].ToString();
                }
                if (Session["UserName"].ToString() == "李利恒")
                {
                    sql = "select count(*) from OM_LIZHISHOUXU where LZ_SCBBZ is null or LZ_SCBBZ=''";
                    DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql);
                    DataRow dr2 = dt2.Rows[0];
                    om_lzsx = dr2[0].ToString();
                }
                if (Session["UserName"].ToString() == "高浩")
                {
                    sql = "select count(*) from OM_LIZHISHOUXU where LZ_CGBZ is null or LZ_CGBZ=''";
                    DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sql);
                    DataRow dr3 = dt3.Rows[0];
                    om_lzsx = dr3[0].ToString();
                }
                if (Session["UserName"].ToString() == "叶宝松")
                {
                    sql = "select count(*) from OM_LIZHISHOUXU where LZ_CWBZ is null or LZ_CWBZ=''";
                    DataTable dt4 = DBCallCommon.GetDTUsingSqlText(sql);
                    DataRow dr4 = dt4.Rows[0];
                    om_lzsx = dr4[0].ToString();
                }
                if (Session["UserName"].ToString() == "王金泽")
                {
                    sql = "select count(*) from OM_LIZHISHOUXU where LZ_SBBZ is null or LZ_SBBZ=''";
                    DataTable dt5 = DBCallCommon.GetDTUsingSqlText(sql);
                    om_lzsx = dt5.Rows[0][0].ToString();
                }
                if (Session["UserName"].ToString() == "于来义")
                {
                    sql = "select count(*) from OM_LIZHISHOUXU where LZ_SCBZ is null or LZ_SCBZ=''";
                    DataTable dt6 = DBCallCommon.GetDTUsingSqlText(sql);
                    om_lzsx = dt6.Rows[0][0].ToString();
                }
                if (Session["UserName"].ToString() == "李小婷")
                {
                    sql = "select count(*) from OM_LIZHISHOUXU where LZ_JSBZ is null or LZ_JSBZ=''";
                    DataTable dt7 = DBCallCommon.GetDTUsingSqlText(sql);
                    om_lzsx = dt7.Rows[0][0].ToString();
                }
                if (Session["UserName"].ToString() == "陈永秀")
                {
                    sql = "select count(*) from OM_LIZHISHOUXU where LZ_ZLBZ is null or LZ_ZLBZ=''";
                    DataTable dt7 = DBCallCommon.GetDTUsingSqlText(sql);
                    om_lzsx = dt7.Rows[0][0].ToString();
                }
            }
            return om_lzsx;
        }






        /// <summary>
        /// 技术任务审批
        /// </summary>
        private void GetTask()
        {

            string sqlText = "select sum(num) from (";
            sqlText += "select count(*) as num from TBPM_MPFORALLRVW where MP_REVIEWA='" + Session["UserID"].ToString() + "' and MP_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_MPFORALLRVW where MP_REVIEWB='" + Session["UserID"].ToString() + "' and MP_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_MPFORALLRVW where MP_REVIEWC='" + Session["UserID"].ToString() + "' and MP_STATE='6' ";
            sqlText += "union all select count(*) as num from TBPM_MPFORALLRVW where MP_SUBMITID='" + Session["UserID"].ToString() + "' and MP_STATE in ('3','5','7')";
            sqlText += " union all select count(*) as num from TBPM_MPCHANGERVW where MP_REVIEWA='" + Session["UserID"].ToString() + "' and MP_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_MPCHANGERVW where MP_REVIEWB='" + Session["UserID"].ToString() + "' and MP_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_MPCHANGERVW where MP_REVIEWC='" + Session["UserID"].ToString() + "' and MP_STATE='6' ";
            sqlText += " union all select count(*) as num from TBPM_MSFORALLRVW where MS_REVIEWA='" + Session["UserID"].ToString() + "' and MS_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_MSFORALLRVW where MS_REVIEWB='" + Session["UserID"].ToString() + "' and MS_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_MSFORALLRVW where MS_REVIEWC='" + Session["UserID"].ToString() + "' and MS_STATE='6' ";
            sqlText += "union all select count(*) as num from TBPM_MSFORALLRVW where MS_SUBMITID='" + Session["UserID"].ToString() + "' and MS_STATE in ('3','5','7')";
            sqlText += " union all select count(*) as num from TBPM_MSCHANGERVW where MS_REVIEWA='" + Session["UserID"].ToString() + "' and MS_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_MSCHANGERVW where MS_REVIEWB='" + Session["UserID"].ToString() + "' and MS_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_MSCHANGERVW where MS_REVIEWC='" + Session["UserID"].ToString() + "' and MS_STATE='6' ";
            sqlText += "union all select count(*) as num from TBPM_MSCHANGERVW where MS_SUBMITID='" + Session["UserID"].ToString() + "' and MS_STATE in ('3','5','7')";
            sqlText += " union all select count(*) as num from TBPM_PAINTSCHEME where PS_REVIEWA='" + Session["UserID"].ToString() + "' and PS_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_PAINTSCHEME where PS_REVIEWB='" + Session["UserID"].ToString() + "' and PS_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_PAINTSCHEME where PS_REVIEWC='" + Session["UserID"].ToString() + "' and PS_STATE='6' ";
            sqlText += "union all select count(*) as num from TBPM_PAINTSCHEME where PS_SUBMITID='" + Session["UserID"].ToString() + "' and PS_STATE in ('3','5','7')";
            sqlText += ") as temp ";


            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    task.Visible = false;
                    HyperLink8.Visible = false;
                }
                else
                {
                    task.Text = "(" + dr[0].ToString() + ")";
                }
            }
            dr.Close();

        }
        /// <summary>
        /// 比价单管理
        /// </summary>
        private void getxbjsh()
        {
            string sqltext = "";
            int num = 0;
            if (Session["UserID"].ToString() == "7")//高
            {
                sqltext = "SELECT count(*) from TBPC_IQRCMPPRCRVW where (ICL_STATE='1' or ICL_STATE='3') and ICL_REVIEWB='" + Session["UserID"].ToString() + "' and ICL_STATEA='0'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr.Read())
                {
                    num += Convert.ToInt32(dr[0].ToString());
                }
                dr.Close();
            }

            else if (Session["UserID"].ToString() == "2")//王福泉
            {
                sqltext = "SELECT count(*) from TBPC_IQRCMPPRCRVW where (ICL_STATE='1' or ICL_STATE='3') and ICL_STATEA='2' and ICL_STATEB='0'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr.Read())
                {
                    num += Convert.ToInt32(dr[0].ToString());
                }
                dr.Close();
            }
            else if (Session["UserID"].ToString() == "311")//王自清
            {
                sqltext = "SELECT count(*) from TBPC_IQRCMPPRCRVW where (ICL_STATE='1' or ICL_STATE='3') and ICL_STATEA='2' and ICL_STATEB='2' and ICL_STATEC='0'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr.Read())
                {
                    num += Convert.ToInt32(dr[0].ToString());
                }
                dr.Close();
            }
            else
            {
                //制单人任务
                sqltext = "select count(*) from TBPC_IQRCMPPRCRVW where (ICL_STATE='0' or ICL_STATE='2') and  ICL_REVIEWA='" + Session["UserID"].ToString() + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr.Read())
                {
                    num += Convert.ToInt32(dr[0].ToString());
                }
                dr.Close();
            }



            if (num == 0)
            {
                lb_bjdsh.Visible = false;
                HyperLink9.Visible = false;
            }
            else
            {
                lb_bjdsh.Visible = true;
                lb_bjdsh.Text = "(" + num.ToString() + ")";
            }
        }


        /// <summary>
        /// 生产外协审批
        /// </summary>
        private void GET_WXSP()
        {
            //先找出审核人列表中包含当前登录人的单号，再根据审批状态进行筛选
            string userid = Session["UserID"].ToString();

            int num = 0;//待审批的单号数量，即包含此人且还没有填写意见，意见为0
            string sqlselect_code = "select PM_DocuNum,Fir_Per,Fir_Jg,Sec_Per,Sec_Jg,Thi_Per,Thi_Jg from" +
                   " TBPM_SCWXAUDIT where (Fir_Per='" + userid + "' and Fir_Jg='0')" +
                   " or (Sec_Per='" + userid + "' and Sec_Jg='0') or" +
                   " (Thi_Per='" + userid + "' and  Thi_Jg='0')";
            DataTable dt_select_code = DBCallCommon.GetDTUsingSqlText(sqlselect_code);
            if (dt_select_code.Rows.Count > 0)
            {
                foreach (DataRow dr_code in dt_select_code.Rows)
                {
                    if (userid == dr_code["Fir_Per"].ToString())
                    {
                        num++;
                    }
                    else if (userid == dr_code["Sec_Per"].ToString()) //第二级审核看一级审核是否同意
                    {
                        if (dr_code["Fir_Jg"].ToString() == "1")
                        {
                            num++;
                        }
                    }
                    else if (userid == dr_code["Thi_Per"].ToString()) //第三级审核看二级审核是否同意
                    {
                        if (dr_code["Sec_Jg"].ToString() == "1")
                        {
                            num++;
                        }
                    }
                }
                if (num > 0)
                {
                    lbl_wxsp.Text = "(" + num.ToString() + ")";
                }
                //没有任务就不可见
                else
                {
                    HyperLink10.Visible = false;
                }
            }
            //没有任务就不可见
            else
            {
                HyperLink10.Visible = false;
            }
        }
        /// <summary>
        /// 外协比价审批
        /// </summary>

        private void GET_WXBJSP()
        {
            string sqltext = "SELECT count(*) as num from TBMP_IQRCMPPRCRVW where ((ICL_STATE='1' or ICL_STATE='3') and ((ICL_REVIEWB='" + Session["UserID"].ToString() + "' and ICL_STATEA='0')or (ICL_STATEA='2' and ICL_STATEB='0'and ICL_STATEB='0'and ICL_REVIEWC='" + Session["UserID"].ToString() + "')))or( ICL_STATE='3' and  ICL_REVIEWA='" + Session["UserID"].ToString() + "')";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read() && dr["num"].ToString() != "0")
            {
                lbl_wxbjsp.Text = "(" + dr["num"].ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink11.Visible = false;
            }
            dr.Close();
        }

        /// <summary>
        /// 发货比价审批
        /// </summary>
        private void GET_FYSP()
        {
            string sqltext = "select count(distinct ICL_SHEETNO) as num from View_TBMP_FAYUNPRICE_RVW where ((ICL_REVIEWA='" + Session["UserID"].ToString() + "' and ICL_STATE='0') or (ICL_REVIEWB='" + Session["UserID"].ToString() + "' and ICL_STATE='1') or( ICL_REVIEWC='" + Session["UserID"].ToString() + "'  and ICL_STATE='3'))";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read() && dr["num"].ToString() != "0")
            {
                lbl_fysp.Text = "(" + dr["num"].ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink12.Visible = false;
            }
            dr.Close();
        }


        /// <summary>
        /// 成品入库审批
        /// </summary>
        private void GET_FINISHED()
        {
            //先找出审核人列表中包含当前登录人的单号，再根据审批状态进行筛选
            string userid = Session["UserID"].ToString();

            int num = 0;//待审批的单号数量，即包含此人且还没有填写意见，意见为0
            string sqlselect_code = "select TSA_ID,Fir_Per,Fir_Jg,Sec_Per,Sec_Jg,Thi_Per,Thi_Jg from" +
                   " TBMP_FINISHED_IN_Audit where (Fir_Per='" + userid + "' and Fir_Jg='0')" +
                   " or (Sec_Per='" + userid + "' and Sec_Jg='0') or" +
                   " (Thi_Per='" + userid + "' and  Thi_Jg='0')";
            DataTable dt_select_code = DBCallCommon.GetDTUsingSqlText(sqlselect_code);
            if (dt_select_code.Rows.Count > 0)
            {
                foreach (DataRow dr_code in dt_select_code.Rows)
                {
                    if (userid == dr_code["Fir_Per"].ToString())
                    {
                        num++;
                    }
                    else if (userid == dr_code["Sec_Per"].ToString()) //第二级审核看一级审核是否同意
                    {
                        if (dr_code["Fir_Jg"].ToString() == "1")
                        {
                            num++;
                        }
                    }
                    else if (userid == dr_code["Thi_Per"].ToString()) //第三级审核看二级审核是否同意
                    {
                        if (dr_code["Sec_Jg"].ToString() == "1")
                        {
                            num++;
                        }
                    }
                }
                if (num > 0)
                {
                    lbl_finish.Text = "(" + num.ToString() + ")";
                }
                //没有任务就不可见
                else
                {
                    HyperLink27.Visible = false;
                }
            }
            //没有任务就不可见
            else
            {
                HyperLink27.Visible = false;
            }
        }

        /// <summary>
        /// 采购申请审批
        /// </summary>
        private void Get_CGSP1()
        {
            //先找出审核人列表中包含当前登录人的单号，再根据审批状态进行筛选
            string userid = Session["UserID"].ToString();

            int num = 0;//待审批的单号数量，即包含此人且还没有填写意见，意见为0
            string sqlselect_code = "select PA_CODE,PA_FIR_PER,PA_FIR_JG,PA_SEC_PER,PA_SEC_JG,PA_THI_PER,PA_THI_JG from" +
                   " TBPC_OTPUR_Audit where (PA_FIR_PER='" + userid + "' and PA_FIR_JG='0')" +
                   " or (PA_SEC_PER='" + userid + "' and PA_SEC_JG='0') or" +
                   " (PA_THI_PER='" + userid + "' and  PA_THI_JG='0')";
            DataTable dt_select_code = DBCallCommon.GetDTUsingSqlText(sqlselect_code);
            if (dt_select_code.Rows.Count > 0)
            {
                foreach (DataRow dr_code in dt_select_code.Rows)
                {
                    if (userid == dr_code["PA_FIR_PER"].ToString())
                    {
                        num++;
                    }
                    else if (userid == dr_code["PA_SEC_PER"].ToString()) //第二级审核看一级审核是否同意
                    {
                        if (dr_code["PA_FIR_JG"].ToString() == "1")
                        {
                            num++;
                        }
                    }
                    else if (userid == dr_code["PA_THI_PER"].ToString()) //第三级审核看二级审核是否同意
                    {
                        if (dr_code["PA_SEC_JG"].ToString() == "1")
                        {
                            num++;
                        }
                    }
                }
                if (num > 0)
                {
                    lbl_cgsp.Text = "(" + num.ToString() + ")";
                }
                //没有任务就不可见
                else
                {
                    HyperLink13.Visible = false;
                }
            }
            //没有任务就不可见
            else
            {
                HyperLink13.Visible = false;
            }
        }
        /// <summary>
        /// 成品出库审批
        /// </summary>
        private void GET_FINISHEDOUT()
        {
            //先找出审核人列表中包含当前登录人的单号，再根据审批状态进行筛选
            string userid = Session["UserID"].ToString();

            int num = 0;//待审批的单号数量，即包含此人且还没有填写意见，意见为0
            string sqlselect_code = "select TSA_ID,Fir_Per,Fir_Jg,Sec_Per,Sec_Jg,Thi_Per,Thi_Jg from" +
                   " TBMP_FINISHED_OUT_Audit where (Fir_Per='" + userid + "' and Fir_Jg='0')" +
                   " or (Sec_Per='" + userid + "' and Sec_Jg='0') or" +
                   " (Thi_Per='" + userid + "' and  Thi_Jg='0')";
            DataTable dt_select_code = DBCallCommon.GetDTUsingSqlText(sqlselect_code);
            if (dt_select_code.Rows.Count > 0)
            {
                foreach (DataRow dr_code in dt_select_code.Rows)
                {
                    if (userid == dr_code["Fir_Per"].ToString())
                    {
                        num++;
                    }
                    else if (userid == dr_code["Sec_Per"].ToString()) //第二级审核看一级审核是否同意
                    {
                        if (dr_code["Fir_Jg"].ToString() == "1")
                        {
                            num++;
                        }
                    }
                    else if (userid == dr_code["Thi_Per"].ToString()) //第三级审核看二级审核是否同意
                    {
                        if (dr_code["Sec_Jg"].ToString() == "1")
                        {
                            num++;
                        }
                    }
                }
                if (num > 0)
                {
                    lbl_out.Text = "(" + num.ToString() + ")";
                }
                //没有任务就不可见
                else
                {
                    HyperLink21.Visible = false;
                }
            }
            //没有任务就不可见
            else
            {
                HyperLink21.Visible = false;
            }
        }
        private void InitUrl()
        {
            HyperLink1.NavigateUrl = "../CM_Data/CM_PJinfo.aspx";

            HyperLink3.NavigateUrl = "../CM_Data/CM_FHList.aspx";
            HyperLink4.NavigateUrl = "../OM_Data/OM_ZhuanZPS.aspx";
            HyperLink5.NavigateUrl = "../OM_Data/OM_CarApplyAudit.aspx";
            HyperLink6.NavigateUrl = "../OM_Data/OM_GdzcPcPlan.aspx";
            HyperLink8.NavigateUrl = "../TM_Data/TM_Leader_Task.aspx";
            HyperLink9.NavigateUrl = "../PC_Data/TBPC_IQRCMPPRCLST_checked.aspx";
            HyperLink10.NavigateUrl = "../PM_Data/PM_Xie_Audit_List.aspx";
            HyperLink11.NavigateUrl = "../PM_Data/PM_Xie_list.aspx";
            HyperLink12.NavigateUrl = "../PM_Data/PM_fayun_list.aspx";
            HyperLink13.NavigateUrl = "../PC_Data/PC_TBPC_Otherpur_Bill_Audit.aspx";
            HyperLink14.NavigateUrl = "../ESM_Data/EQU_Need_Audit.aspx";
            HyperLink15.NavigateUrl = "../CM_Data/CM_Kaipiao_List.aspx";
            HyperLink16.NavigateUrl = "../QC_Data/QC_Inspection_Manage.aspx";
            HyperLink17.NavigateUrl = "../QC_Data/QC_Reject_Product.aspx";
            //HyperLink18.NavigateUrl = "../QC_Data/QC_ZJXGSH_TOTAL.aspx";
            HyperLink18.NavigateUrl = "../PC_Data/PC_TBPC_Purchaseplan_check_list.aspx";
            HyperLink19.NavigateUrl = "../PC_Data/PC_TBPC_Marreplace_list.aspx";
            HyperLink20.NavigateUrl = "../Contract_Data/CM_MyContractReviewTask.aspx";//我的评审任务
            HyperLink21.NavigateUrl = "../PM_Data/PM_FINISHED_OUT_Audit.aspx";
            HyperLink22.NavigateUrl = "../Basic_Data/tbcs_cusupinfo_Review.aspx";//厂商审批
            HyperLink23.NavigateUrl = "../PC_Data/PC_TBPC_Purchange_new.aspx";//物料减少审批
            HyperLink27.NavigateUrl = "../PM_Data/PM_FINISHED_IN_Audit.aspx";//成品入库审批
            HyperLink28.NavigateUrl = "../OM_Data/OM_EATNEWLIST.aspx";//食堂管理
        }
        /// <summary>
        /// 设备采购
        /// </summary>
        private void GET_SBCG()
        {
            //先找出审核人列表中包含当前登录人的单号，再根据审批状态进行筛选
            string userid = Session["UserID"].ToString();

            int num = 0;//待审批的单号数量，即包含此人且还没有填写意见，意见为0
            string sqlselect_code = "select DocuNum,Fir_Per,Fir_Jg,Sec_Per,Sec_Jg,Thi_Per,Thi_Jg from" +
                   " EQU_Need_Audit where (Fir_Per='" + userid + "' and Fir_Jg='0')" +
                   " or (Sec_Per='" + userid + "' and Sec_Jg='0') or" +
                   " (Thi_Per='" + userid + "' and  Thi_Jg='0')";
            DataTable dt_select_code = DBCallCommon.GetDTUsingSqlText(sqlselect_code);
            if (dt_select_code.Rows.Count > 0)
            {
                foreach (DataRow dr_code in dt_select_code.Rows)
                {
                    if (userid == dr_code["Fir_Per"].ToString())
                    {
                        num++;
                    }
                    else if (userid == dr_code["Sec_Per"].ToString()) //第二级审核看一级审核是否同意
                    {
                        if (dr_code["Fir_Jg"].ToString() == "1")
                        {
                            num++;
                        }
                    }
                    else if (userid == dr_code["Thi_Per"].ToString()) //第三级审核看二级审核是否同意
                    {
                        if (dr_code["Sec_Jg"].ToString() == "1")
                        {
                            num++;
                        }
                    }
                }
                if (num > 0)
                {
                    lblEquNeed.Text = "(" + num.ToString() + ")";
                }
                //没有任务就不可见
                else
                {
                    HyperLink14.Visible = false;
                }
            }
            //没有任务就不可见
            else
            {
                HyperLink14.Visible = false;
            }
        }

        /*********************得到我的报检任务的数量*************************/
        private void GetMyBaoJian()
        {
            string sqlText = "";
            if (Session["UserDeptID"].ToString() != "12")
            {
                //如果此人不是质量部的，也就是报检人，那么他默认看到的就是他自己报检之后的不合格的。

                sqlText = "select count(*) from TBQM_APLYFORINSPCT where AFI_STATE='1' AND AFI_MAN='" + Session["UserID"].ToString() + "' and AFI_ENDRESLUT='不合格' and AFI_ID not in(select AFI_ID from (select count(*) as countresult,AFI_ID from (select distinct * from (select case when RESULT='合格' then '让步接收' else RESULT end as RESULT,AFI_ID from (select * from TBQM_APLYFORINSPCT where UNIQUEID in(select UNIQUEID from TBQM_APLYFORITEM where RESULT='让步接收')) as a left join TBQM_APLYFORITEM as b on a.UNIQUEID=b.UNIQUEID)s)t group by AFI_ID)m where countresult=1)";

                //pager.StrWhere = "AFI_STATE='0' AND AFI_MAN='" + Session["UserID"].ToString() + "'";
            }
            else
            {   //如果此人是质量部的，那个他看到的是全部未质检的
                sqlText = "select count(*) from TBQM_APLYFORINSPCT where AFI_STATE='0' and AFI_QCMAN='" + Session["UserID"].ToString() + "'";

            }
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    lb_baojian.Visible = false;
                    HyperLink16.Visible = false;
                }
                else
                {
                    lb_baojian.Visible = true;
                    lb_baojian.Text = "(" + dr[0].ToString() + ")";
                }
            }
            else
            {
                lb_baojian.Visible = false;
                HyperLink16.Visible = false;
            }
            dr.Close();
        }
        /// <summary>
        /// 不合格通知单
        /// </summary>
        private void Reject_Pro()
        {
            int num = 0;
            int num1 = 0;
            int num2 = 0;
            string userid = Session["UserID"].ToString();
            //先找出所有没审的
            string sqltext = "select count(1) from dbo.View_TBQC_RejectPro_Info_Detail where  (state='7' and SPR_ZL_ID='" + userid + "') or (state='1' and PSR_ID='" + userid + "') or(state='2' and SPR_ID='" + userid + "') or (state='3' and BZR='" + userid + "') or (STATE='6' and ZGLD_ID='" + Session["UserID"] + "')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            num1 = Convert.ToInt32(dt.Rows[0][0]);

            sqltext = "select Copy_dep from dbo.View_TBQC_RejectPro_Info_Detail where state='3'";
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext);
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                if (dt2.Rows[i][0].ToString().Contains(Session["UserDeptID"].ToString()))
                {
                    num2++;
                }
            }
            num = num2 + num1;

            if (num > 0)
            {
                lb_rejectPro.Text = "(" + num.ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink17.Visible = false;
            }
        }
        ///// <summary>
        ///// 物料占用管理
        ///// </summary>
        //private void getwlzy()
        //{
        //    string sqltext = "";
        //    int num = 0;
        //    sqltext = "select  count(distinct(PR_PCODE)) from View_TBPC_MARSTOUSE_TOTAL_ALL where ((PR_STATE='1' or PR_STATE='3') and PR_REVIEWA='" + Session["UserID"].ToString() + "') or (allshstate='0' and PR_REVIEWB='" + Session["UserID"].ToString() + "')";
        //    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
        //    while (dr.Read())
        //    {
        //        num = Convert.ToInt32(dr[0].ToString());
        //    }
        //    dr.Close();
        //    if (num == 0)
        //    {
        //        lb_wlzygl.Visible = false;
        //    }
        //    else
        //    {
        //        lb_wlzygl.Visible = true;
        //        lb_wlzygl.Text = "(" + num.ToString() + ")";
        //    }
        //}
        private void GetCusup_Review()
        {
            int Review_num = 0;

            string str_sql = "select * from TBCS_CUSUP_ADD_DELETE where ( CS_SPJG in ('0','1') and id in(" +
                        " select distinct a.fatherid from TBCS_CUSUP_ReView a , TBCS_CUSUP_ReView b " +
                                 " where (a.CSR_TYPE!='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "' and a.fatherid=b.fatherid and b.CSR_YJ!='0' and a.CSR_TYPE!='5'" +
                                 "  and cast(a.CSR_TYPE as int)-1=cast(b.CSR_TYPE as int) ) or " +
                                 " (a.CSR_TYPE='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "')" +
                                 ")) or ( CS_SPJG in ('0','1') and id in (" +
                                  " select distinct f.fatherid from TBCS_CUSUP_ReView f  " +
                                  " where (f.CSR_TYPE!='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid in (" +
                                  "select h.fatherid from TBCS_CUSUP_ReView h where h.CSR_TYPE='5' )) or" +
                                 " (f.CSR_TYPE='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid not in (" +
                                " (select d.fatherid from TBCS_CUSUP_ReView d " +
                                "  where d.CSR_TYPE!='5' and d.csr_yj='0' and d.fatherid  in( select e.fatherid from  " +
                                 " TBCS_CUSUP_ReView e where e.CSR_TYPE='5' and e.CSR_YJ='0' and e.CSR_PERSON='" + Session["UserID"].ToString() + "'" +
                                 "))))))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(str_sql);
            if (dt.Rows.Count > 0)
            {
                Review_num += dt.Rows.Count;
                CUSUP_REVIEW.Text = "(" + Review_num + ")";
            }
            else
            {
                CUSUP_REVIEW.Visible = false;
                HyperLink22.Visible = false;
            }
        }

        #region 售后
        private void GetlbCLD()
        {
            string username = Session["UserName"].ToString();
            string depid = Session["UserDeptID"].ToString();
            string sql = "select count(CLD_ID) from CM_SHCLD where ";
            sql += "(CLD_YYFX_TXR='" + username + "' and (CLD_YYFX is null or CLD_YYFX='')) or ";
            sql += "(CLD_CLYJ_TXR='" + username + "' and (CLD_CLYJ is null or CLD_CLYJ='')) or ";
            sql += "(CLD_CLFA_TXR='" + username + "' and (CLD_CLFA is null or CLD_CLFA='')) or ";
            sql += "(CLD_FZBMID like '%" + depid + "%' and (CLD_CLJG is null or CLD_CLJG='')) or ";
            sql += "(CLD_FWFY_TJR='" + username + "' and CLD_FWZFY is null) or ";
            sql += "(CLD_SPR1='" + username + "' and CLD_SPZT='0') or ";
            sql += "(CLD_SPR2='" + username + "' and CLD_SPZT='1y' and (CLD_CLYJ is not null or CLD_CLYJ<>'') and (CLD_CLFA is not null or CLD_CLFA<>'')) or ";
            sql += "(CLD_SPR4='" + username + "' and CLD_SPZT='2y') or ";
            sql += "(CLD_SPR5='" + username + "' and CLD_SPZT='4y')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lbCLD.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink26.Visible = false;
            }
        }

        private void GetlbCLD_SP()
        {
            string username = Session["UserName"].ToString();
            string sql = string.Format("select count(CLD_ID) from CM_SHCLD where (CLD_SPR1='{0}' and CLD_SPZT='0') or (CLD_SPR2='{0}' and CLD_SPZT='1y' and (CLD_CLYJ is not null or CLD_CLYJ<>'') and (CLD_CLFA is not null or CLD_CLFA<>'')) or (CLD_SPR4='{0}' and CLD_SPZT='2y') or (CLD_SPR5='{0}' and CLD_SPZT='4y') ", username);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lbCLD_SP.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink59.Visible = false;
            }
        }
        private void GetlbCLD_FG()
        {
            string username = Session["UserName"].ToString();
            string sql = "";
            if (username == "李利恒")
            {
                sql = "select * from CM_SHCLD where (CLD_CLFA_TXR='' or CLD_CLFA_TXR is null) and CLD_SPZT='1y' and (CLD_CLYJ !='' and CLD_CLYJ is not null)";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    lbCLD_FG.Text = "(" + dt.Rows.Count.ToString() + ")";
                }
                //没有任务就不可见
                else
                {
                    HyperLink60.Visible = false;
                }
            }
            else if (username == "李小婷")
            {
                sql = "select * from CM_SHCLD where (CLD_CLYJ_TXR='' or CLD_CLYJ_TXR is null) and CLD_SPZT='1y' and (CLD_YYFX !='' and CLD_YYFX is not null)  union all select * from CM_SHCLD where (CLD_CLJG_TXR='' or CLD_CLJG_TXR is null)  and CLD_SPZT='y' and (CLD_FZBM like '%技术部%') ";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    lbCLD_FG.Text = "(" + dt.Rows.Count.ToString() + ")";
                }
                //没有任务就不可见
                else
                {
                    HyperLink60.Visible = false;
                }
            }
            else if (username == "陈永秀")
            {
                sql = "select * from CM_SHCLD where (CLD_YYFX_TXR='' or CLD_YYFX_TXR is null) and CLD_SPZT='1y' union all select * from CM_SHCLD where (CLD_CLJG_TXR='' or CLD_CLJG_TXR is null)  and CLD_SPZT='y' and ( CLD_FZBM like '%质量部%') ";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    lbCLD_FG.Text = "(" + dt.Rows.Count.ToString() + ")";
                }
                //没有任务就不可见
                else
                {
                    HyperLink60.Visible = false;
                }
            }
            else if (username == "于来义")
            {
                sql = "select * from CM_SHCLD where (CLD_CLJG_TXR='' or CLD_CLJG_TXR is null)  and (CLD_SPZT='y') and CLD_FZBM like '%生产部%'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    lbCLD_FG.Text = "(" + dt.Rows.Count.ToString() + ")";
                }
                //没有任务就不可见
                else
                {
                    HyperLink60.Visible = false;
                }
            }
            else if (username == "高浩")
            {
                sql = "select * from CM_SHCLD where (CLD_CLJG_TXR='' or CLD_CLJG_TXR is null)  and (CLD_SPZT='y') and CLD_FZBM like '%采购部%'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    lbCLD_FG.Text = "(" + dt.Rows.Count.ToString() + ")";
                }
                //没有任务就不可见
                else
                {
                    HyperLink60.Visible = false;
                }
            }
            else if (username == "叶宝松")
            {
                sql = "select * from CM_SHCLD where (CLD_FWFY_TJR='' or CLD_FWFY_TJR is null) and (CLD_SPZT='cljg_y')";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    lbCLD_FG.Text = "(" + dt.Rows.Count.ToString() + ")";
                }
                //没有任务就不可见
                else
                {
                    HyperLink60.Visible = false;
                }
            }
            //没有任务就不可见
            else
            {
                HyperLink60.Visible = false;
            }
        }

        private void GetlbCLD_YYFX()
        {
            string username = Session["UserName"].ToString();
            string sql = string.Format("select count(CLD_ID) from CM_SHCLD where CLD_YYFX_TXR='{0}' and (CLD_YYFX is null or CLD_YYFX='') and CLD_SPZT in ('1y','2y','4y','y','cljg_y','fytj_y')", username);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lbCLD_YYFX.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink61.Visible = false;
            }
        }
        private void GetlbCLD_CLYJ()
        {
            string username = Session["UserName"].ToString();
            string sql = string.Format("select count(CLD_ID) from CM_SHCLD where CLD_CLYJ_TXR='{0}' and (CLD_CLYJ is null or CLD_CLYJ='') and CLD_SPZT ='1y'", username);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lbCLD_CLYJ.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink62.Visible = false;
            }
        }
        private void GetlbCLD_CLFA()
        {
            string username = Session["UserName"].ToString();
            string sql = string.Format("select count(CLD_ID) from CM_SHCLD where CLD_CLFA_TXR='{0}' and (CLD_CLFA is null or CLD_CLFA='') and CLD_SPZT ='1y'", username);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lbCLD_CLFA.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink63.Visible = false;
            }
        }
        private void GetlbCLD_CLJG()
        {
            string username = Session["UserName"].ToString();
            string sql = string.Format("select count(CLD_ID) from CM_SHCLD where CLD_CLJG_TXR='{0}' and (CLD_CLJG is null or CLD_CLJG='') and CLD_SPZT ='y'", username);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lbCLD_CLJG.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink65.Visible = false;
            }
        }
        private void GetlbTJ()
        {
            string username = Session["UserName"].ToString();
            string sql = string.Format("select count(CLD_ID) from CM_SHCLD where CLD_FWFY_TJR='{0}' and (CLD_FWZFY is null or CLD_FWZFY=0) and CLD_SPZT in ('cljg_y','fytj_y')", username);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lbTJ.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink66.Visible = false;
            }
        }

        private void GetlbLXD()
        {
            string username = Session["UserName"].ToString();
            string depid = Session["UserDeptID"].ToString();
            string sql = " select count (LXD_ID) from CM_SHLXD where (LXD_SPR1='" + username + "' and LXD_SPZT='0') or ";
            sql += "(LXD_SPR2='" + username + "' and LXD_SPZT='1') or ";
            sql += "(LXD_SPR3='" + username + "' and LXD_SPZT='2') or ";
            sql += "(LXD_FZBMID like '%" + depid + "%' and (LXD_FWGC is null or LXD_FWGC='')) or ";
            sql += "(LXD_FWFYDEPID like'%" + depid + "%' and (LXD_FWZFY is null or LXD_FWZFY=''))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lbLXD.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink25.Visible = false;
            }
        }

        #endregion

        //需用计划审批任务
        private void getxyjhbg()
        {
            string username = Session["UserName"].ToString();
            string userid = Session["UserID"].ToString();
            string position = Session["POSITION"].ToString();
            string depid = Session["UserDeptID"].ToString();
            int i = 0;
            int j = 0;
            int k = 0;
            int g = 0;
            int n = 0;
            if (position != "0301" && position != "0501" && position != "0502" && position != "0506" && position != "0505" && userid != "67")
            {
                string sql1 = "select distinct BG_PH from TBPC_BG where BG_PTC is not null and BG_PTC!='' and ((BG_STATE='1' and BG_SHRA='" + username + "') or (BG_STATE='0' and BG_NAME='" + username + "'))";
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
                if (dt1.Rows.Count > 0)
                {
                    i = dt1.Rows.Count;
                    Label3.Text = "(" + i.ToString() + ")";
                }
                //没有任务就不可见
                else
                {
                    HyperLink23.Visible = false;
                }
            }
            else if (position == "0301")
            {
                string sql2 = "select distinct BG_PH from TBPC_BG where BG_PTC is not null and BG_PTC!='' and BG_STATE='2'";
                DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql2);
                if (dt2.Rows.Count > 0)
                {
                    j = dt2.Rows.Count;
                    Label3.Text = "(" + j.ToString() + ")";
                }
                //没有任务就不可见
                else
                {
                    HyperLink23.Visible = false;
                }
            }
            else if (position == "0501")
            {
                string sql3 = "select distinct BG_PH from TBPC_BG where BG_PTC is not null and BG_PTC!='' and BG_STATE='3'";
                DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sql3);
                if (dt3.Rows.Count > 0)
                {
                    k = dt3.Rows.Count;
                    Label3.Text = "(" + k.ToString() + ")";
                }
                //没有任务就不可见
                else
                {
                    HyperLink23.Visible = false;
                }
            }
            else if (position == "0502" || position == "0506" || position == "0505")
            {
                string sql4 = "select distinct BG_PH from TBPC_BG where BG_PTC is not null and BG_PTC!='' and BG_STATE='4' and RESULT='审核中' and BG_SHRD='" + username + "'";
                DataTable dt4 = DBCallCommon.GetDTUsingSqlText(sql4);
                if (dt4.Rows.Count > 0)
                {
                    g = dt4.Rows.Count;
                    Label3.Text = "(" + g.ToString() + ")";
                }
                //没有任务就不可见
                else
                {
                    HyperLink23.Visible = false;
                }
            }
            else if (userid == "67")
            {
                string sql5 = "select distinct BG_PH from TBPC_BG where ((BG_PTC is not null and BG_PTC!='' and BG_STATE='2') or (BG_PTC is not null and BG_PTC!='' and ((BG_STATE='1' and BG_SHRA='" + username + "') or (BG_STATE='0' and BG_NAME='" + username + "'))))";
                DataTable dt5 = DBCallCommon.GetDTUsingSqlText(sql5);
                if (dt5.Rows.Count > 0)
                {
                    n = dt5.Rows.Count;
                    Label3.Text = "(" + n.ToString() + ")";
                }
            }
            //没有任务就不可见
            else
            {
                HyperLink23.Visible = false;
            }


            string sqltextchangenote = "";
            if (depid == "03")
            {
                sqltextchangenote = "select * from TBPC_changebeizhu where changestate='2'";
                DataTable dtchangenote = DBCallCommon.GetDTUsingSqlText(sqltextchangenote);
                if (dtchangenote.Rows.Count > 0)
                {
                    Label5.Text = "(" + dtchangenote.Rows.Count.ToString().Trim() + ")";
                    HyperLink23.Visible = true;
                }
            }
            else if (depid == "05")
            {
                sqltextchangenote = "select * from TBPC_changebeizhu where changestate='0' or changestate='' or changestate is null";
                DataTable dtchangenote = DBCallCommon.GetDTUsingSqlText(sqltextchangenote);
                if (dtchangenote.Rows.Count > 0)
                {
                    Label5.Text = "(" + dtchangenote.Rows.Count.ToString().Trim() + ")";
                    HyperLink23.Visible = true;
                }
            }
            else
            {
                Label5.Visible = false;
                HyperLink23.Visible = false;
            }
        }

        ////食堂管理
        //private void GET_shitang()
        //{
        //    string count_task = "0";
        //    int count_task_int = 0;
        //    //部门领导审核
        //    string sql_dep_audit = "select distinct SHENHEID from TBOM_EAT where STATE='0'";
        //    DataTable dt_dep_audit = DBCallCommon.GetDTUsingSqlText(sql_dep_audit);
        //    for (int i = 0; i < dt_dep_audit.Rows.Count; i++)
        //    {
        //        //部门审核人登录，记录需要审核的数量
        //        if (Session["UserID"].ToString() == dt_dep_audit.Rows[i][0].ToString())
        //        {
        //            string sqlText_dep_audit = "select count(*) from TBOM_EAT where SHENHEID='" + Session["UserID"].ToString() + "' and ID IN(SELECT MIN(ID) FROM TBOM_EAT GROUP BY(CODE)) and STATE='0'";
        //            SqlDataReader dr_dep_audit = DBCallCommon.GetDRUsingSqlText(sqlText_dep_audit);
        //            if (dr_dep_audit.Read())
        //            {
        //                count_task = dr_dep_audit[0].ToString();
        //                count_task_int += Convert.ToInt32(count_task.ToString());
        //            }
        //            break;
        //        }
        //    }

        //    int eat_accept = 0;
        //    int eat_back = 0;
        //    //赵洪新
        //    if (Session["UserID"].ToString() == "260")
        //    {
        //        //食堂待接收
        //        string sqlText_eat_accept = "select count(*) from TBOM_EAT where ID IN(SELECT MIN(ID) FROM TBOM_EAT GROUP BY(CODE)) and STATE='2'";
        //        SqlDataReader dr_eat_accept = DBCallCommon.GetDRUsingSqlText(sqlText_eat_accept);
        //        //食堂未反馈
        //        string sqlText_eat_back = "select count(*) from TBOM_EAT where ID IN(SELECT MIN(ID) FROM TBOM_EAT GROUP BY(CODE)) and STATE='4'";
        //        SqlDataReader dr_eat_back = DBCallCommon.GetDRUsingSqlText(sqlText_eat_back);
        //        if (dr_eat_accept.Read())
        //        {
        //            eat_accept = Convert.ToInt32(dr_eat_accept[0].ToString());
        //        }
        //        if (dr_eat_back.Read())
        //        {
        //            eat_back = Convert.ToInt32(dr_eat_back[0].ToString());
        //        }
        //        count_task = (eat_accept + eat_back).ToString();
        //        count_task_int += Convert.ToInt32(count_task.ToString());
        //    }

        //    //用餐确认
        //    string sql_eat_confirm = "select distinct APPLYID from TBOM_EAT where STATE='6'";
        //    DataTable dt_eat_confirm = DBCallCommon.GetDTUsingSqlText(sql_eat_confirm);
        //    for (int i = 0; i < dt_eat_confirm.Rows.Count; i++)
        //    {
        //        if (Session["UserID"].ToString() == dt_eat_confirm.Rows[i][0].ToString())
        //        {
        //            string sqlText_eat_confirm = "select count(*) from TBOM_EAT where APPLYID='" + Session["UserID"].ToString() + "' and ID IN(SELECT MIN(ID) FROM TBOM_EAT GROUP BY(CODE)) and STATE='6'";
        //            SqlDataReader dr_eat_confirm = DBCallCommon.GetDRUsingSqlText(sqlText_eat_confirm);
        //            if (dr_eat_confirm.Read())
        //            {
        //                count_task = dr_eat_confirm[0].ToString();
        //                count_task_int += Convert.ToInt32(count_task.ToString());
        //            }
        //            break;
        //        }
        //    }
        //    if (count_task_int == 0)
        //    {
        //        Lbl_mess.Visible = false;
        //    }
        //    else
        //    {
        //        Lbl_mess.Text = "(" + Convert.ToString(count_task_int) + ")";
        //    }
        //}

        //代用单待审核数量
        private void getdydsh()
        {
            string sqltext = "";
            int num = 0;
            sqltext = "select count(*) from TBPC_MARREPLACETOTAL where  " +
                      "(MP_FILLFMID='" + Session["UserID"].ToString() +
                      "' and (MP_STATE='000' or MP_STATE='200' or MP_STATE='300'))";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num += Convert.ToInt32(dr[0].ToString());
            }

            dr.Close();
            sqltext = "select count(*) from TBPC_MARREPLACETOTAL where  " +
                    "(MP_LEADER='" + Session["UserID"].ToString() +
                    "' and MP_STATE='001')";
            dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num += Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            sqltext = "select count(*) from TBPC_MARREPLACETOTAL where  " +
                      "(MP_REVIEWAID='" + Session["UserID"].ToString() +
                      "' and MP_STATE='111')";
            dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num += Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            sqltext = "select count(*) from TBPC_MARREPLACETOTAL where  " +
                      "(MP_CHARGEID='" + Session["UserID"].ToString() +
                      "' and MP_STATE='211')";
            dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num += Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            sqltext = "select count(*) from TBPC_MARREPLACETOTAL where  " +
                      "(MP_CKSHRID='" + Session["UserID"].ToString() +
                      "' and MP_STATE='311' and (MP_CKSHRTIME='' or MP_CKSHRTIME is null))";
            dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num += Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            if (num == 0)
            {
                lb_dydsh.Visible = false;
                HyperLink19.Visible = false;
            }
            else
            {
                lb_dydsh.Visible = true;
                lb_dydsh.Text = "(" + num.ToString() + ")";
            }
        }




        private void GetInnerAudit()
        {
            string sql = "select count(1) from TBQC_INTERNAL_AUDIT where PRO_TYPE='inner' and  ((PRO_STATE='1' and PRO_SPR='" + Session["UserID"].ToString() + "' ) or( PRO_STATE='2' and PRO_ZGR='" + Session["UserID"].ToString() + "') or (PRO_STATE='4' and PRO_SPR='" + Session["UserID"].ToString() + "') or (PRO_STATE='5' and PRO_SHY='" + Session["UserID"].ToString() + "'))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            string count = dt.Rows[0][0].ToString();
            if (count != "0")
            {
                lblInnerAudit.Text = "(" + count + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink30.Visible = false;
            }
        }
        private void GetGroupAudit()
        {
            string sql = "select count(1) from TBQC_INTERNAL_AUDIT where PRO_TYPE='group' and  ((PRO_STATE='1' and PRO_SPR='" + Session["UserID"].ToString() + "' ) or( PRO_STATE='2' and PRO_ZGR='" + Session["UserID"].ToString() + "') or (PRO_STATE='4' and PRO_SPR='" + Session["UserID"].ToString() + "') or (PRO_STATE='5' and PRO_SHY='" + Session["UserID"].ToString() + "'))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            string count = dt.Rows[0][0].ToString();
            if (count != "0")
            {
                lblGroupAudit.Text = "(" + count + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink31.Visible = false;
            }
        }
        private void GetExterAudit()
        {
            string sql = "select count(1) from TBQC_EXTERNAL_AUDIT where  ((PRO_STATE='1' and PRO_SPR='" + Session["UserID"].ToString() + "' ) or( PRO_STATE='2' and PRO_ZGR='" + Session["UserID"].ToString() + "') or (PRO_STATE='4' and PRO_SPR='" + Session["UserID"].ToString() + "') or (PRO_STATE='5' and PRO_SHY='" + Session["UserID"].ToString() + "'))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            string count = dt.Rows[0][0].ToString();
            if (count != "0")
            {
                lblExterAudit.Text = "(" + count + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink32.Visible = false;
            }
        }
        //图纸替换通知单
        private void GetlbTZTHTZD()
        {
            string username = Session["UserName"].ToString();
            string sql = " select count(TZD_ID) from CM_TZTHTZD where (TZD_SPR1='" + username + "' and TZD_SPZT='0') or ";
            sql += "(TZD_SPR2='" + username + "' and TZD_SPZT='1')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0 && dt.Rows[0][0].ToString() != "0")
            {
                lbTZTHTZD.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink33.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink36.Visible = false;
            }
        }
        //班组平均绩效
        private void GetBZEverage()
        {
            string Id = Session["UserID"].ToString().Trim();
            string sql = "select count(1) from TBDS_BZAVERAGE  where (State ='1' and SPRID='" + Id + "') OR (State='3' and ZDRID='" + Id + "') or ( State='2' and SPRIDB='" + Session["UserId"].ToString() + "') ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblBZEverage.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink37.Visible = false;
            }
        }
        //部门月度考核
        private void GetDepartMonth()
        {
            string Id = Session["UserID"].ToString().Trim();
            string sql = "select count(1) from TBDS_KaoheDeaprtMonth  where (state='1' and SPRID='" + Id + "') or (state='3' and ZDRID='" + Id + "') ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblDepartMonth.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink38.Visible = false;
            }
        }
        //部门月度绩效工资
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
            //没有任务就不可见
            else
            {
                HyperLink39.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink41.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink42.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink43.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink28.Visible = false;
            }
        }


        //人员绩效增加审批
        private void GetJXADDSP()
        {
            string sqlgetjxadd = "select count(*) as count from OM_JXADDSP where ((creatstid='" + Session["UserID"].ToString().Trim() + "' and totalstate='0') or (shrid1='" + Session["UserID"].ToString().Trim() + "' and totalstate='1' and shstate1='0') or (shrid2='" + Session["UserID"].ToString().Trim() + "' and totalstate='1' and shstate1='1'and shstate2='0'))";
            DataTable dtgetjxadd = DBCallCommon.GetDTUsingSqlText(sqlgetjxadd);
            if (dtgetjxadd.Rows[0]["count"].ToString().Trim() != "0")
            {
                lbjxaddsp.Text = "(" + dtgetjxadd.Rows[0]["count"].ToString().Trim() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink51.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink52.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink56.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink79.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink53.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink54.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink67.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink82.Visible = false;
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
            //没有任务就不可见
            else
            {
                HyperLink83.Visible = false;
            }
        }

        //设备合同审批任务
        private void Get_HTSP()
        {
            string userid = Session["UserID"].ToString();
            string sql = string.Format("select count (HT_ID) from EQU_GXHT where (HT_SHR1ID='{0}' and (HT_SHR1_JL is null or HT_SHR1_JL='') and HT_SPZT='1') or (HT_SHR2ID='{0}' and (HT_SHR2_JL='' or HT_SHR2_JL is null) and HT_SPZT='1y') or (HT_SHR3ID='{0}' and (HT_SHR3_JL='' or HT_SHR3_JL is null) and HT_SPZT='1y')or (HT_SHR2ID='{0}' and (HT_SHR2_JL='' or HT_SHR2_JL is null) and HT_SPZT='2.2y')or (HT_SHR3ID='{0}' and (HT_SHR3_JL='' or HT_SHR3_JL is null) and HT_SPZT='2.1y') or (HT_SHR4ID='{0}' and (HT_SHR4_JL is null or HT_SHR4_JL='') and HT_SPZT='2y') or (HT_SHR5ID='{0}' and (HT_SHR5_JL is null or HT_SHR5_JL='') and HT_SPZT='3y')", userid);

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            if (Convert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                lbl_ssp.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                HyperLink55.Visible = false;
            }

        }

        private void GetTargetAnalyze()
        {
            string monthvalid = "";
            string sqlText = "";
            if (DateTime.Now.Month.ToString() != "1")
            {
                sqlText = "select b.* from TBQC_TARGET_LIST as a left join TBQC_TARGET_DETAIL as b on a.TARGET_ID=b.TARGET_FID where a.TARGET_NAME like '" + DateTime.Now.Year.ToString() + "%'and b.TARGET_DEPID='" + Session["UserDeptID"].ToString() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][DateTime.Now.Month + 3].ToString()))
                        monthvalid = "1";
                }
                if (DateTime.Now.Day > 0 && DateTime.Now.Day < 11)
                {
                    if (string.IsNullOrEmpty(monthvalid))
                    {
                        lblTargetAnalyze.Text = "(1)";
                        HyperLink71.Visible = true;
                    }
                    //没有任务就不可见
                    else
                    {
                        HyperLink71.Visible = false;
                    }
                }
                //没有任务就不可见
                else
                {
                    HyperLink71.Visible = false;
                }
            }
            else
            {
                sqlText = "select b.* from TBQC_TARGET_LIST as a left join TBQC_TARGET_DETAIL as b on a.TARGET_ID=b.TARGET_FID where a.TARGET_NAME like '" + (DateTime.Now.Year - 1).ToString() + "%'and b.TARGET_DEPID='" + Session["UserDeptID"].ToString() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][DateTime.Now.Month + 15].ToString()))
                        monthvalid = "1";
                }
                if (DateTime.Now.Day > 0 && DateTime.Now.Day < 11)
                {
                    if (string.IsNullOrEmpty(monthvalid))
                    {
                        lblTargetAnalyze.Text = "(1)";
                        HyperLink71.Visible = true;
                    }
                    //没有任务就不可见
                    else
                    {
                        HyperLink71.Visible = false;
                    }
                }
                //没有任务就不可见
                else
                {
                    HyperLink71.Visible = false;
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
            else
            {
                HyperLink72.Visible = false;
            }
        }

        //固定资产采购
        private void GetpcGDZC()
        {

            string sql = "select count(1) from View_TBOM_GDZCAPPLY where (STATUS='1' and CARRVWAID='" + Session["UserId"].ToString() + "') or ( STATUS='2' and  CARRVWBID='" + Session["UserId"].ToString() + "') and PCTYPE='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblFixedAssets.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
            else
            {
                HyperLink6.Visible = false;
            }
        }

        //办公设备资产采购
        private void GetpcFGDZC()
        {

            string sql = "select count(1) from View_TBOM_GDZCAPPLY where (STATUS='1' and CARRVWAID='" + Session["UserId"].ToString() + "') or ( STATUS='2' and  CARRVWBID='" + Session["UserId"].ToString() + "') and PCTYPE='1'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblFGDZCpc.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
            else
            {
                HyperLink73.Visible = false;
            }
        }

        //办公设备资产入库审批
        private void FixnofuAudit()
        {
            string sql = "select count(1) from OM_SP where SPLX='FGDZC' and ((SPZT='0' and SPR1ID='" + Session["UserId"].ToString() + "') or ( SPZT='1y' and  SPR2ID='" + Session["UserId"].ToString() + "')or ( SPZT='2y' and  SPR3ID='" + Session["UserId"].ToString() + "') )";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblnofixfuAudit.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
            else
            {
                HyperLink74.Visible = false;
            }
        }

        //固定设备资产入库审批
        public void GetFixfuAudit()
        {
            string sql = "select count(1) from OM_SP where SPLX='GDZC' and ((SPZT='0' and SPR1ID='" + Session["UserId"].ToString() + "') or ( SPZT='1y' and  SPR2ID='" + Session["UserId"].ToString() + "')or ( SPZT='2y' and  SPR3ID='" + Session["UserId"].ToString() + "') ) ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() != "0")
            {
                lblfixmoAudit.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
            else
            {
                HyperLink75.Visible = false;
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
            else
            {
                HyperLink76.Visible = false;
            }
        }

        //采购信息交流
        protected void PC_Pur_inform_commit()
        {
            if (Session["UserDeptID"].ToString().Trim() == "02")
            {
                HyperLink77.Visible = false;
            }
            string sql_pur_inform = "select * from PC_purinformcommitall where ( PC_PFT_STATE='0'  and PC_PFT_SPRA_ID='" + Session["UserID"].ToString() + "' )" +
                     " or ( PC_PFT_STATE='1' and PC_PFT_SPRB_ID='" + Session["UserID"].ToString() + "'  )" +
                      " or ( PC_PFT_STATE='2' and PC_PFT_SPRC_ID='" + Session["UserID"].ToString() + "')";
            DataTable dt_pur_inform = DBCallCommon.GetDTUsingSqlText(sql_pur_inform);
            if (dt_pur_inform.Rows.Count > 0)
            {
                lbpurinform.Text = "(" + dt_pur_inform.Rows.Count + ")";
            }
            ////没有任务就不可见
            //else
            //{
            //    HyperLink77.Visible = false;
            //}
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
            else
            {
                HyperLink102.Visible = false;
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
            else
            {
                HyperLink78.Visible = false;
            }
        }
    }
}
