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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_Menu : BasicPage
    {
        string sqlText = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            if (!IsPostBack)
            {
                if (Session.IsNewSession)
                {
                    Session.Abandon();
                    Application.Lock();
                    Application["online"] = (int)Application["online"] - 1;
                    Application.UnLock();
                    Response.Write("<script>if(window.parent!=null)window.parent.location.href='../Default.aspx';else{window.location.href='./Default.aspx'} </script>");
                }
                InitUrl();
                InitUrl1();
                CheckUser(ControlFinder);
            }
            Reject_Pro();

            GetTask();
            getxbjsh();//比价单数量
            getdydsh();//代用单数量
            getxyjhsh();//未下推需用计划数量
            getrwfgsh();//任务分工，未分工
            getcgjhsh();//采购计划未询比价数量
            getddgl();//订单未提交数量
            getbggl();//变更管理
            getwlzy();//物料占用管理
          //  getzbwl();//招标物料
            GetMyViewTask();
            GetCusup_Review();

            //GetMyTask1();
            Get_CGSP1();
            getdydsh1();
            getBGdsh1();
            getsafe1();
            GetProjTemp1(); //项目结转备库

            GetCM_CM_CUSTOMER();//顾客财产
            CM_GKFW();//顾客服务单通知
            CghtSP();//采购合同数量
            GetKaiPiao();

            GET_FHTZ();//发货通知
            GetMyBaoJian();//我的报检任务

            getxyjhbg();//需用计划变更

            qitarukusp();//其他入库审核

            getfpwwjinfo();//采购发票未完结

            PC_Pur_inform_commit();//采购信息交流
        }

        //我的合同评审任务
        #region
        private void GetMyViewTask()
        {
            int NUM = 0;
            string sqltext = "";
            sqltext = "select * from View_TBCR_View_Detail_ALL where CRD_PID='" + Session["UserID"].ToString() + "' and CR_PSZT='1' AND CRD_PSYJ='0'";
            DataTable DT = DBCallCommon.GetDTUsingSqlText(sqltext);

            if (Session["UserDeptID"].ToString() == "01")  //当前用户为领导
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string sql_cw = "select CRD_PSYJ from TBCR_CONTRACTREVIEW_DETAIL where CRD_ID ='" + DT.Rows[i]["CR_ID"].ToString() + "' and CRD_DEP='14'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_cw);
                    if (dr.Read())
                    {
                        if (dr[0].ToString() == "2" || DT.Rows[i]["CRD_DEP"].ToString() != "01")//审计已审，或领导兼部门负责人
                        {
                            NUM++;
                        }
                        dr.Close();
                    }
                    else
                    {
                        //没有审计时，判断是不是所有部门都审核通过
                        string check_BMYJ = "select CRD_PSYJ from TBCR_CONTRACTREVIEW_DETAIL where CRD_ID='" + DT.Rows[i]["CR_ID"].ToString() + "' and CRD_DEP !='01' and CRD_PSYJ!='2' and CRD_PID!='" + Session["UserID"] + "'";
                        DataTable dtbmyj = DBCallCommon.GetDTUsingSqlText(check_BMYJ);
                        if (dtbmyj.Rows.Count <= 0)//没有不通过的
                        {
                            NUM++;
                        }
                    }
                }
            }
            else if (Session["UserDeptID"].ToString() == "14") //当前用户为财务
            {
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string sql_BM = "select CRD_PID, CRD_PSYJ from TBCR_CONTRACTREVIEW_DETAIL where CRD_ID ='" + DT.Rows[i]["CR_ID"].ToString() + "' and CRD_DEP not in ('01','14')";
                    DataTable dt_bm = DBCallCommon.GetDTUsingSqlText(sql_BM);
                    bool check = true;
                    for (int j = 0; j <= dt_bm.Rows.Count - 1; j++)
                    {
                        if (dt_bm.Rows[j]["CRD_PSYJ"].ToString() != "2")
                        {
                            check = false; break;
                        }
                    }
                    if (check)
                    {
                        NUM++;
                    }
                }
            }
            else
            {
                //除领导和财务以外的其他部门，审批流程为并行，提示所有可审批任务
                for (int i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    NUM++;
                }
            }
            if (NUM == 0)
            {
                MyViewTask.Visible = false;
            }
            else
            {
                MyViewTask.Text = "(" + NUM + ")";
            }
        }
        #endregion



        private void GetKaiPiao()
        {
            string sql = "select count(1) from (select * from CM_KAIPIAO as d left join (select a.cId , stuff((select sprId+',' from CM_KAIPIAO_HUISHEN b where b.cId =a.cId and( b.result is null or b.result='') for xml path('')),1,0,',') 'sprId ' from CM_KAIPIAO_HUISHEN  a  group by  a.cId)c on d.KP_TaskID=c.cId)e where ((KP_SPSTATE='1' and KP_SHRIDA='" + Session["UserID"].ToString() + "') or (KP_SPSTATE='2' and KP_SHRIDB='" + Session["UserID"].ToString() + "') or (KP_HSSTATE='1' and sprId like '%," + Session["UserID"].ToString() + ",%'))";
            lblKaiPiao.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows[0][0].ToString() + "）";
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
        }

        private void GetCusup_Review()//厂商审批
        {
            int Review_num = 0;

            string str_sql = "select * from TBCS_CUSUP_ADD_DELETE where CS_SPJG in ('0','1') and" +
                             " id in ( select distinct a.fatherid from TBCS_CUSUP_ReView a , TBCS_CUSUP_ReView b " +
                             " where (a.CSR_TYPE!='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "' and a.fatherid=b.fatherid and b.CSR_YJ!='0'" +
                             "  and cast(a.CSR_TYPE as int)-1=cast(b.CSR_TYPE as int) ) or " +
                             " (a.CSR_TYPE='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "')" +
                             " )";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(str_sql);
            if (dt.Rows.Count > 0)
            {
                Review_num += dt.Rows.Count;
                CUSUP_REVIEW.Text = "(" + Review_num + ")";
            }
            else
            {
                CUSUP_REVIEW.Visible = false;
            }
        }

        //不合格品通知单
        private void Reject_Pro()
        {
            int num = 0;
            int num1 = 0;
            int num2 = 0;
            string userid = Session["UserID"].ToString();
            //先找出所有没审的
            string sqltext = "select count(1) from dbo.View_TBQC_RejectPro_Info_Detail where (state='7' and SPR_ZL_ID='" + userid + "') or  (state='1' and PSR_ID='" + userid + "') or(state='2' and SPR_ID='" + userid + "') or (state='3' and BZR='" + userid + "')";
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
        }
        private void InitUrl()
        {

            HyperLink1.NavigateUrl = "PC_TBPC_Purchaseplan_start.aspx";//汇总
            HyperLink2.NavigateUrl = "PC_TBPC_Purchaseplan_check_list.aspx";//审核
            HyperLink3.NavigateUrl = "PC_TBPC_Purchaseplan_assign_list.aspx";
            HyperLink4.NavigateUrl = "PC_TBPC_Purchaseplan_assign.aspx";//我的任务
            HyperLink5.NavigateUrl = "TBPC_IQRCMPPRCLST_checked.aspx";
            HyperLink6.NavigateUrl = "TBPC_Purordertotal_list.aspx";
            HyperLink7.NavigateUrl = "PC_TBPC_Marreplace_list.aspx";
            HyperLink8.NavigateUrl = "PC_TBPC_Purchange_all_list.aspx";
            HyperLink9.NavigateUrl = "PC_History_price_analysis .aspx";
            HyperLink10.NavigateUrl = "PC_TBPC_Mar_Statistics.aspx";
          //  HyperLink13.NavigateUrl = "PC_TBPC_Toubiao_Manage.aspx";
            HyperLink14.NavigateUrl = "~/QC_Data/QC_Inspection_Manage.aspx";
            HyperLink15.NavigateUrl = "PC_TBPC_PurQuery.aspx";
            HyperLink16.NavigateUrl = "~/Basic_Data/tbcs_cusupinfo_Review.aspx";
            HyperLink17.NavigateUrl = "~/Contract_Data/CM_MyContractReviewTask.aspx";
            HyperLink39.NavigateUrl = "PC_CGHTGL.aspx";
            HyperLink34.NavigateUrl = "PC_CGHTFPGL.aspx";
            HyperLink20.NavigateUrl = "PC_TBPC_Purchange_new.aspx";
            HyperLink30.NavigateUrl = "PC_TBPC_CKQKCX.aspx";
            HyperLink31.NavigateUrl = "PC_TBPC_JCXXGL.aspx";
        }

        //比价单数量
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
            else if (Session["UserID"].ToString() == "311")//赵宏观
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
            }
            else
            {
                lb_bjdsh.Visible = true;
                lb_bjdsh.Text = "(" + num.ToString() + ")";
            }
        }

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
            }
            else
            {
                lb_dydsh.Visible = true;
                lb_dydsh.Text = "(" + num.ToString() + ")";
            }
        }

        //代用单待审核数量
        //标记审核任务
        private void GetTask()
        {
            //sqlText = "select count(*) from TBPM_MPFORALLRVW where MP_STATE='2'";
            //SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            //if (dr.Read())
            //{
            //    if (dr[0].ToString() == "0")
            //    {
            //        task.Visible = false;
            //    }
            //    else
            //    {
            //        task.Text = "(" + dr[0].ToString() + ")";
            //    }
            //}
        }
        //未下推需用计划
        private void getxyjhsh()
        {
            string sqltext = "";
            int num = 0;
            sqltext = "SELECT count(DISTINCT PUR_PCODE) FROM  TBPC_PURCHASEPLAN  WHERE  (PUR_STATE = '0') AND (PUR_CSTATE = '0') ";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            if (num == 0)
            {
                lb_XYplan.Visible = false;
                Label1.Visible = false;
            }
            else
            {
                lb_XYplan.Visible = true;
                lb_XYplan.Text = "(" + num.ToString() + ")";
                Label1.Visible = true;
                Label1.Text = "(" + num.ToString() + ")";
            }
        }

        //任务未分工数量
        private void getrwfgsh()
        {
            string sqltext = "";
            int num = 0;
            sqltext = "select count(distinct PUR_PCODE) from TBPC_PURCHASEPLAN  where PUR_STATE='3' and PUR_CSTATE='0' and (Pue_Closetype!='3' or Pue_Closetype is null)";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            if (num == 0)
            {
                lb_rwfg.Visible = false;
            }
            else
            {
                lb_rwfg.Visible = true;
                lb_rwfg.Text = "(" + num.ToString() + ")";
            }
        }

        private void CM_GKFW()
        {
            string sql = "select CM_ID from TBCM_APPLICA where CM_CLPART like '%05%' and CM_CHULI='N'";
            lblFWTZ.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
        }

        //采购计划管理数量
        private void getcgjhsh()
        {
            string sqltext = "";
            int num = 0;
            sqltext = "select  count(*) from TBPC_PURCHASEPLAN where (PUR_STATE='4'or  PUR_STATE='5') and PUR_CGMAN='" + Session["UserID"].ToString() + "' and PUR_CSTATE='0'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            if (num == 0)
            {
                lb_chjhgl.Visible = false;
            }
            else
            {
                lb_chjhgl.Visible = true;
                lb_chjhgl.Text = "(" + num.ToString() + ")";
            }
        }

        //采购订单数量
        private void getddgl()
        {
            string sqltext = "";
            int num = 0;
            sqltext = "select  count(*) from TBPC_PURORDERTOTAL where PO_STATE='0' and PO_CSTATE='0' and PO_ZDID='" + Session["UserID"].ToString() + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            if (num == 0)
            {
                lb_ddgl.Visible = false;
            }
            else
            {
                lb_ddgl.Visible = true;
                lb_ddgl.Text = "(" + num.ToString() + ")";
            }
        }

        //变更管理
        private void getbggl()
        {
            string sqltext = "";
            int num = 0;
            sqltext = "select  count(*) from TBPC_MPCHANGETOTAL where MP_CHSTATE!='2'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            if (num == 0)
            {
                lb_biangeng.Visible = false;
            }
            else
            {
                lb_biangeng.Visible = true;
                lb_biangeng.Text = "(" + num.ToString() + ")";
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
                }
                else
                {
                    lb_baojian.Visible = true;
                    lb_baojian.Text = "(" + dr[0].ToString() + ")";
                }
            }
            dr.Close();
        }

        //物料占用
        private void getwlzy()
        {
            string sqltext = "";
            int num = 0;
            sqltext = "select  count(distinct(PR_PCODE)) from View_TBPC_MARSTOUSE_TOTAL_ALL where allshstate='0' and left(PR_REVIEWATIME,10)>='2015-07-15'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            if (num == 0)
            {
                lb_wlzygl.Visible = false;
                lb_MyTask.Visible = false;
            }
            else
            {
                lb_wlzygl.Visible = true;
                lb_wlzygl.Text = "(" + num.ToString() + ")";
                lb_MyTask.Visible = true;
                lb_MyTask.Text = "(" + num.ToString() + ")";
            }
        }

        //采购审核任务

        ////物料占用
        //private void getzbwl()
        //{
        //    string sqltext = "";
        //    int num = 0;
        //    sqltext = "select  count(*) from View_TBPC_TOUBIAODETAIL where PUR_CSTATE='0' and purstate='4' and cgrid='" + Session["UserID"].ToString() + "'";
        //    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
        //    while (dr.Read())
        //    {
        //        num = Convert.ToInt32(dr[0].ToString());
        //    }
        //    dr.Close();
        //    if (num == 0)
        //    {
        //        lb_zbwl.Visible = false;
        //    }
        //    else
        //    {
        //        lb_zbwl.Visible = true;
        //        lb_zbwl.Text = "(" + num.ToString() + ")";
        //    }
        //}

        private void InitUrl1()
        {
            HyperLink18.NavigateUrl = "~/CM_Data/CM_Customer.aspx";
            HyperLink21.NavigateUrl = "~/SM_Data/SM_Warehouse_Query.aspx?FLAG=QUERY";
            HyperLink22.NavigateUrl = "~/SM_Data/SM_WarehouseIN_Index.aspx";
            HyperLink23.NavigateUrl = "~/SM_Data/SM_WarehouseOut_Index.aspx";
            HyperLink24.NavigateUrl = "~/SM_Data/SM_Warehouse_AllocationManage.aspx";
            HyperLink25.NavigateUrl = "~/SM_Data/SM_Warehouse_MTOAdjustManage.aspx";
            HyperLink26.NavigateUrl = "~/SM_Data/SM_Warehouse_MTONotes.aspx";
            HyperLink27.NavigateUrl = "~/SM_Data/SM_Warehouse_InventoryManage.aspx";
            HyperLink28.NavigateUrl = "~/SM_Data/SM_Warehouse_MaterialFlow.aspx";
            HyperLink29.NavigateUrl = "~/SM_Data/SM_Warehouse_Manage.aspx";
            HyperLink33.NavigateUrl = "~/SM_Data/SM_WarehouseIn_Other_Manage.aspx";
            HyperLink110.NavigateUrl = "~/SM_Data/SM_Trans_Management/SM_Trans_Manage_Index.aspx";
            HyperLink112.NavigateUrl = "~/PC_Data/PC_TBPC_Purchaseplan_check_list.aspx?action=ws";
            HyperLink113.NavigateUrl = "~/PC_Data/PC_TBPC_Marreplace_list.aspx";
            HyperLink114.NavigateUrl = "~/PC_Data/PC_TBPC_Otherpur_Bill_List.aspx";
            HyperLink115.NavigateUrl = "~/PC_Data/PC_TBPC_Otherpur_Bill_Audit.aspx";
            //HyperLink116.NavigateUrl = "~/SM_Data/SM_PURCHASEPLAN_VIEW.aspx";
            //HyperLink118.NavigateUrl = "SM_Tech_MTO.aspx";
            HyperLink119.NavigateUrl = "~/SM_Data/SM_Warehouse_Warning.aspx";
            HyperLink120.NavigateUrl = "~/SM_Data/SM_Warehouse_add_delete.aspx";
            HyperLink121.NavigateUrl = "~/SM_Data/SM_Warehouse_ProjOver.aspx";
            HyperLink122.NavigateUrl = "~/SM_Data/SM_Warehouse_ProjTempManage.aspx?";
            HyperLink123.NavigateUrl = "~/SM_Data/SM_YULIAO_LIST.aspx";
           // HyperLink124.NavigateUrl = "~/SM_Data/SM_YULIAO_IN.aspx";
           // HyperLink125.NavigateUrl = "~/SM_Data/SM_YULIAO_OUT.aspx";
            HyperLink126.NavigateUrl = "~/PC_Data/PC_TBPC_Purchaseplan_start.aspx";
        }

        private void GetCM_CM_CUSTOMER()//显示未入库数目
        {
            string sql = "select count(*) from TBCM_CUSTOMER where CM_BTIN ='0' and CM_CHECK='1'";
            int num = 0;
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            while (dr.Read())
            {
                num = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            if (num == 0)
            {
                lbNumber.Visible = false;
            }
            else
            {
                lbNumber.Text = "(" + num.ToString() + ")";
            }
        }

        /*********************得到我的任务的数量*************************/
        //private void GetMyTask1()
        //{
        //    string sqlText = "select count(distinct(PR_PCODE)) from View_TBPC_MARSTOUSE_TOTAL_ALL where allshstate='0' and PR_REVIEWB='" + Session["UserID"].ToString() + "'";
        //    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
        //    while (dr.Read())
        //    {
        //        if (dr[0].ToString() == "0")
        //        {
        //            lb_MyTask.Visible = false;
        //        }
        //        else
        //        {
        //            lb_MyTask.Visible = true;
        //            lb_MyTask.Text = "(" + dr[0].ToString() + ")";
        //        }
        //    }
        //    dr.Close();
        //}
        private void getsafe1()
        {
            string sqlText = "select count(*) from View_STORAGE_WARNING where WARNNUM>isnull(storagenum,0)";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            while (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    lbl_safe.Visible = false;
                }
                else
                {
                    lbl_safe.Visible = true;
                    lbl_safe.Text = "(" + dr[0].ToString() + ")";
                }
            }
            dr.Close();
        }
        private void getBGdsh1()
        {

            int mywtz = 0;

            int mytzz = 0;

            string sqltext_mywtz = "select count(*) from TBPC_MPTEMPCHANGE where  MP_EXECSTATE='1' AND MP_EXECID='" + Session["UserID"].ToString() + "'";

            string sqltext_mytzz = "select count(*) from TBPC_MPTEMPCHANGE where  MP_EXECSTATE='2' AND MP_EXECID='" + Session["UserID"].ToString() + "'";



            mywtz = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_mywtz).Rows[0][0].ToString());

            mytzz = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_mytzz).Rows[0][0].ToString());

            //LabelBG.Text = "(" + mywtz.ToString() +"/"+ mytzz.ToString()+ ")";
        }
        //代用单待审核数量
        private void getdydsh1()
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
                Label2.Visible = false;
            }
            else
            {
                lb_dydsh.Visible = true;
                lb_dydsh.Text = "(" + num.ToString() + ")";
                Label2.Visible = true;
                Label2.Text = "(" + num.ToString() + ")";
            }
        }

        private void GET_FHTZ()//发货通知
        {
            string sql = string.Format("select CM_FID from View_CM_FaHuo where CM_CONFIRM='2' and CM_FHZT='0' group by CM_FID");
            fhtz.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
        }

        //采购审核任务
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
            }

        }

        /*--------------- 结转备库我的任务 ------------------ */
        private void GetProjTemp1()
        {
            string userid = Session["Userid"].ToString();

            string sqltext2 = "select count(*) from tbws_projtemp where PT_STATE='0' and PT_DOC='" + userid + "'";
            SqlDataReader dr2 = DBCallCommon.GetDRUsingSqlText(sqltext2);
            string strnum2 = "";
            if (dr2.Read())
            {
                strnum2 = dr2[0].ToString();

            }
            dr2.Close();

            if (strnum2 != "0")
            {
                LabelProjTemp.Text = "(" + strnum2 + ")";
            }
            else { LabelProjTemp.Visible = false; }
        }




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


            string sqltextchangenote = "";
            if (depid == "03")
            {
                sqltextchangenote = "select * from TBPC_changebeizhu where changestate='2'";
                DataTable dtchangenote = DBCallCommon.GetDTUsingSqlText(sqltextchangenote);
                if (dtchangenote.Rows.Count > 0)
                {
                    Label5.Text = "(" + dtchangenote.Rows.Count.ToString().Trim() + ")";
                }
            }
            else if (depid == "05")
            {
                sqltextchangenote = "select * from TBPC_changebeizhu where changestate='0' or changestate='' or changestate is null";
                DataTable dtchangenote = DBCallCommon.GetDTUsingSqlText(sqltextchangenote);
                if (dtchangenote.Rows.Count > 0)
                {
                    Label5.Text = "(" + dtchangenote.Rows.Count.ToString().Trim() + ")";
                }
            }
            else
            {
                Label5.Visible = false;
            }
        }


        //其它入库审核
        private void qitarukusp()
        {
            string sqlotherin = "select distinct WG_CODE from View_SM_IN where WG_STATE='1' and WG_CODE like 'T%'";
            DataTable dtotherin = DBCallCommon.GetDTUsingSqlText(sqlotherin);
            if (dtotherin.Rows.Count > 0)
            {
                lbotherin.Text = "(" + dtotherin.Rows.Count.ToString().Trim() + ")";
            }
        }


        //采购发票未完结
        private void getfpwwjinfo()
        {
            if (Session["UserDeptID"].ToString().Trim() != "05")
            {
                HyperLink34.Visible = false;
            }
            else
            {
                //int num = 0;
                //double htmoney = 0;
                //double fpmoney = 0;

                //string sql00 = "select * from PC_CGHT where isnull(HT_FPZT,'n')!='y'";
                //DataTable dt00 = DBCallCommon.GetDTUsingSqlText(sql00);
                //if (dt00.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dt00.Rows.Count; i++)
                //    {
                //        htmoney = 0;
                //        fpmoney = 0;
                //        string[] a = dt00.Rows[i]["HT_HTZJ"].ToString().Trim().Split(new char[] { '(', ')' }, StringSplitOptions.None);
                //        for (int j = 0, length = a.Length; j < length; j++)
                //        {
                //            htmoney += Math.Round(CommonFun.ComTryDouble(a[j]), 2);
                //        }
                //        string sqlgetfpje = "select sum(BR_KPJE) as tolkpje from TBPC_PURBILLRECORD where BR_HTBH='" + dt00.Rows[i]["HT_XFHTBH"].ToString().Trim() + "'";
                //        DataTable dtgetfpje = DBCallCommon.GetDTUsingSqlText(sqlgetfpje);
                //        if (dtgetfpje.Rows.Count > 0)
                //        {
                //            fpmoney = Math.Round((CommonFun.ComTryDouble(dtgetfpje.Rows[0]["tolkpje"].ToString().Trim())) * 10000, 2);
                //        }
                //        if (fpmoney < htmoney)
                //        {
                //            num++;
                //        }
                //    }
                //}
                //if (num > 0)
                //{
                //    lbfpwwj.Text = "(" + num.ToString().Trim() + ")";
                //}
            }
        }

        //采购信息交流
        protected void PC_Pur_inform_commit()
        {
            string sql_pur_inform = "select * from PC_purinformcommitall where ( PC_PFT_STATE='0'  and PC_PFT_SPRA_ID='" + Session["UserID"].ToString() + "' )" +
                     " or ( PC_PFT_STATE='1' and PC_PFT_SPRB_ID='" + Session["UserID"].ToString() + "'  )" +
                      " or ( PC_PFT_STATE='2' and PC_PFT_SPRC_ID='" + Session["UserID"].ToString() + "')";
            DataTable dt_pur_inform = DBCallCommon.GetDTUsingSqlText(sql_pur_inform);
            if (dt_pur_inform.Rows.Count > 0)
            {
                lbpurinform.Text = "(" + dt_pur_inform.Rows.Count + ")";
            }
        }
    }
}
