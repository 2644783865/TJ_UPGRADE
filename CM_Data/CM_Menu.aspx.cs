using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_Menu : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sql = string.Format("select ID from View_CM_Task where CM_SPSTATUS='1' and ((CM_PS1='{0}' and CM_PSYJ1='1') or (CM_PS2='{0}' and CM_PSYJ2='1' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='1' and CM_PSYJ1='2' and CM_PSYJ2='2')) group by ID", Session["UserID"].ToString());
            jyjhd.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
            sql = string.Format("select CM_ID from TBCM_APPLICA where ((CM_MANCLERK='{0}' and CM_STATUS='1') or (CM_DIRECTOR='{0}' and CM_YJ1='1' and CM_STATUS='1') or (CM_LEADER='{0}' and CM_YJ11='2' and CM_YJ2='1') or (CM_BUMEN='{0}' and CM_YJ11='1')) ", Session["UserID"].ToString());
            fwsq.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
            sql = string.Format("select CM_FID from View_CM_FaHuo where CM_CONFIRM='1' and ((CM_MANCLERK='{0}') or (CM_BMZG='{0}' and CM_YJ1='1') or (CM_GSLD='{0}' and CM_YJ2='1' and CM_YJ1='2')) group by CM_FID", Session["UserID"].ToString());
            fhtz.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
            sql = string.Format("select CH_ID from View_CM_Change where CM_TYPE='0' and CM_STATE='1' and ((CM_MANCLERK='{0}') or (CM_PS1='{0}' and CM_PSYJ1='1') or (CM_PS2='{0}' and CM_PSYJ2='1' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='1' and CM_PSYJ1='2' and CM_PSYJ2='2'))", Session["UserID"].ToString());
            bgtz.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
            sql = string.Format("select CH_ID from View_CM_Change where CM_TYPE='1' and CM_STATE='1' and ((CM_PS1='{0}' and CM_PSYJ1='1') or (CM_PS2='{0}' and CM_PSYJ2='1' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='1' and CM_PSYJ1='2' and CM_PSYJ2='2'))", Session["UserID"].ToString());
            zbtz.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
            InitUrl();
            GetlbLXD();
            GetlbCLD();
            GetKaiPiao();
            GetlbGZLXD();
            GetlbHTBGTZD();
            GetlbTZTHTZD();
            GetlbQXPS();
            GET_FHTZ();//发货通知
            GET_FINISHEDOUT();//成品库
            GetMyViewTask();

            GetlbCLD_SP();
            GetlbCLD_FG();
            GetlbCLD_YYFX();
            GetlbCLD_CLYJ();
            GetlbCLD_CLFA();
            GetlbCLD_CLJG();
            GetlbTJ();

            CheckUser(ControlFinder);
        }

        private void GetKaiPiao()
        {
            string sql = "select count(1) from (select * from CM_KAIPIAO as d left join (select a.cId , stuff((select sprId+',' from CM_KAIPIAO_HUISHEN b where b.cId =a.cId and( b.result is null or b.result='') for xml path('')),1,0,',') 'sprId ' from CM_KAIPIAO_HUISHEN  a  group by  a.cId)c on d.KP_TaskID=c.cId)e where ((KP_SPSTATE='1' and KP_SHRIDA='" + Session["UserID"].ToString() + "') or (KP_SPSTATE='2' and KP_SHRIDB='" + Session["UserID"].ToString() + "') or (KP_HSSTATE='1' and sprId like '%," + Session["UserID"].ToString() + ",%'))";
            lblKaiPiao.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows[0][0].ToString() + "）";
        }

        protected void SetTip()
        {
            string dep = "";
            string sql = "select CM_ID from TBCM_APPLICA where CM_CLPART like'" + dep + "' and CM_CHULI='N'";
            fwsq.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
        }

        private void InitUrl()
        {
            string depid = Session["UserDeptID"].ToString();
            HyperLink12.NavigateUrl = "~/Contract_Data/CM_ContractView.aspx";
            HyperLink13.NavigateUrl = "~/Contract_Data/CM_MyContractReviewTask.aspx";
            if (depid == "01" || depid == "07" || Session["UserName"].ToString() == "管理员" || depid == "06")
            {
                HyperLink14.NavigateUrl = "~/Contract_Data/CM_Contract_SW.aspx";
            }
            HyperLink17.NavigateUrl = "~/PM_Data/PM_fayun_list.aspx";
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
            dr.Close();
        }

        protected void ChangeNum()
        {
            //03 技术--CM_BT1     04生产--CM_BT2      05 采购--CM_BT3
            string sql = "select * from TBCM_CHANLIST where CM_STATE='2' and CM_BT1='0'";
            //DBCallCommon.GetDTUsingSqlText(sql).Rows.Count;
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
            lbCLD.Text = "(" + dt.Rows[0][0].ToString() + ")";
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
            }
            else if (username == "曹卫亮" || username == "李小婷")
            {
                sql = " select * from CM_SHCLD where (CLD_CLYJ_TXR='' or CLD_CLYJ_TXR is null) and CLD_SPZT='1y' and (CLD_YYFX !='' and CLD_YYFX is not null)  union all select * from CM_SHCLD where (CLD_CLJG_TXR='' or CLD_CLJG_TXR is null)  and CLD_SPZT='y' and (CLD_FZBM like '%技术部%' ) ";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    lbCLD_FG.Text = "(" + dt.Rows.Count.ToString() + ")";
                }
            }
            else if (username == "曹卫亮"|| username == "陈永秀")
            {
                sql = "select * from CM_SHCLD where (CLD_YYFX_TXR='' or CLD_YYFX_TXR is null) and CLD_SPZT='1y'  union all select * from CM_SHCLD where (CLD_CLJG_TXR='' or CLD_CLJG_TXR is null)  and CLD_SPZT='y' and ( CLD_FZBM like '%质量部%') ";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    lbCLD_FG.Text = "(" + dt.Rows.Count.ToString() + ")";
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
            }
            else if (username == "高浩")
            {
                sql = "select * from CM_SHCLD where (CLD_CLJG_TXR='' or CLD_CLJG_TXR is null)  and (CLD_SPZT='y') and CLD_FZBM like '%采购部%'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    lbCLD_FG.Text = "(" + dt.Rows.Count.ToString()+ ")";
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
            lbLXD.Text = "(" + dt.Rows[0][0].ToString() + ")";
        }

        #endregion
        private void GetlbGZLXD()
        {
            string username = Session["UserName"].ToString();
            string depid = Session["UserDeptID"].ToString();
            string sql = " select count(LXD_ID) from CM_GZLXD where (LXD_SPR1='" + username + "' and LXD_SPZT='0') or ";
            sql += "(LXD_SPR2='" + username + "' and LXD_SPZT='1')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            lbGZLXD.Text = "(" + dt.Rows[0][0].ToString() + ")";
        }

        private void GetlbHTBGTZD()
        {
            string username = Session["UserName"].ToString();
            string sql = " select count(TZD_ID) from CM_HTBGTZD where (TZD_SPR1='" + username + "' and TZD_SPZT='0') or ";
            sql += "(TZD_SPR2='" + username + "' and TZD_SPZT='1')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            lbHTBGTZD.Text = "(" + dt.Rows[0][0].ToString() + ")";
        }

        private void GetlbTZTHTZD()
        {
            string username = Session["UserName"].ToString();
            string sql = " select count(TZD_ID) from CM_TZTHTZD where (TZD_SPR1='" + username + "' and TZD_SPZT='0') or ";
            sql += "(TZD_SPR2='" + username + "' and TZD_SPZT='1')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            lbTZTHTZD.Text = "(" + dt.Rows[0][0].ToString() + ")";
        }

        private void GetlbQXPS()
        {
            string username = Session["UserName"].ToString();
            string sql = " select count(SPID) from CM_SP where SPR1='" + username + "' and SPZT='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            lbQXPS.Text = "(" + dt.Rows[0][0].ToString() + ")";
        }

        private void GET_FHTZ()//发货通知
        {
            string sql = string.Format("select CM_FID from View_CM_FaHuo where CM_CONFIRM='2' and CM_FHZT='0' group by CM_FID");
            lbFHTZ1.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
        }

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
            }
        }


        //我的合同评审任务
        #region
        private void GetMyViewTask()
        {
            #region 以前的
            //int NUM = 0;
            //string sqltext = "";
            //sqltext = "select * from View_TBCR_View_Detail_ALL where CRD_PID='" + Session["UserID"].ToString() + "' and CR_PSZT='1' AND CRD_PSYJ='0'";
            //DataTable DT = DBCallCommon.GetDTUsingSqlText(sqltext);

            //if (Session["UserDeptID"].ToString() == "01")  //当前用户为领导
            //{
            //    for (int i = 0; i <= DT.Rows.Count - 1; i++)
            //    {
            //        string sql_cw = "select CRD_PSYJ from TBCR_CONTRACTREVIEW_DETAIL where CRD_ID ='" + DT.Rows[i]["CR_ID"].ToString() + "' and CRD_DEP='14'";
            //        SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_cw);
            //        if (dr.Read())
            //        {
            //            if (dr[0].ToString() == "2" || DT.Rows[i]["CRD_DEP"].ToString() != "01")//审计已审，或领导兼部门负责人
            //            {
            //                NUM++;
            //            }
            //            dr.Close();
            //        }
            //        else
            //        {
            //            //没有审计时，判断是不是所有部门都审核通过
            //            string check_BMYJ = "select CRD_PSYJ from TBCR_CONTRACTREVIEW_DETAIL where CRD_ID='" + DT.Rows[i]["CR_ID"].ToString() + "' and CRD_DEP !='01' and CRD_PSYJ!='2' and CRD_PID!='" + Session["UserID"] + "'";
            //            DataTable dtbmyj = DBCallCommon.GetDTUsingSqlText(check_BMYJ);
            //            if (dtbmyj.Rows.Count <= 0)//没有不通过的
            //            {
            //                NUM++;
            //            }
            //        }
            //    }
            //}
            //else if (Session["UserDeptID"].ToString() == "14") //当前用户为财务
            //{
            //    for (int i = 0; i <= DT.Rows.Count - 1; i++)
            //    {
            //        string sql_BM = "select CRD_PID, CRD_PSYJ from TBCR_CONTRACTREVIEW_DETAIL where CRD_ID ='" + DT.Rows[i]["CR_ID"].ToString() + "' and CRD_DEP not in ('01','14')";
            //        DataTable dt_bm = DBCallCommon.GetDTUsingSqlText(sql_BM);
            //        bool check = true;
            //        for (int j = 0; j <= dt_bm.Rows.Count - 1; j++)
            //        {
            //            if (dt_bm.Rows[j]["CRD_PSYJ"].ToString() != "2")
            //            {
            //                check = false; break;
            //            }
            //        }
            //        if (check)
            //        {
            //            NUM++;
            //        }
            //    }
            //}
            //else
            //{
            //    //除领导和财务以外的其他部门，审批流程为并行，提示所有可审批任务
            //    for (int i = 0; i <= DT.Rows.Count - 1; i++)
            //    {
            //        NUM++;
            //    }
            //}
            //if (NUM == 0)
            //{
            //    MyViewTask.Visible = false;
            //}
            //else
            //{
            //    MyViewTask.Text = "(" + NUM + ")";
            //}
            #endregion

            string sql = "select count(CR_ID) from View_TBCR_View_Detail_ALL where CRD_PSYJ='0' and CRD_PID='" + Session["UserID"].ToString() + "' and CR_PSZT in ('1','2') and CRD_PIDTYPE='1'";
            if (Session["UserDeptID"].ToString() != "01")
            {
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows[0][0].ToString() == "0")
                {
                    MyViewTask.Visible = false;
                }
                else
                {

                    MyViewTask.Text = "(" + dt.Rows[0][0] + ")";
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
                    MyViewTask.Visible = false;
                }
                else
                {
                    MyViewTask.Text = "(" + num.ToString() + ")";
                }
            }
        }
        #endregion
    }
}