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

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_Menu : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
             //   DBCallCommon.SessionLostToLogIn(Session["UserID"]);
                if (Session.IsNewSession)
                {
                    Session.Abandon();
                    Application.Lock();
                    Application["online"] = (int)Application["online"] - 1;
                    Application.UnLock();
                    Response.Write("<script>if(window.parent!=null)window.parent.location.href='../Default.aspx';else{window.location.href='./Default.aspx'} </script>");

                }
                InitUrl();
                CheckUser(ControlFinder);
            }
         //   GetMyTask();
          //  GetMyTask1();
            GetMyBaoJianFG();
            GetMyBaoJian();
        //    GetMyTuZhi();
            GetTaskFenGong();
          //  GetNoRead();
            Get_CGSP();
            Reject_Pro();
         //   Discard_Pro();
         //   Getpernum();
          //  Getshnum();
            GET_FINISHED();
            GET_FHTZ();//发货通知
            GetlbCLD();
            GetlbLXD();
            GetlbGZLXD();
            GetlbTZTHTZD();
            GetBackTask();
            GetlbGKCC();//顾客财产
        }

        private void InitUrl()
        {
          
            HyperLink1.NavigateUrl = "QC_Task_Manage.aspx";
            HyperLink2.NavigateUrl = "QC_SetInspectPeo.aspx";
            HyperLink3.NavigateUrl = "QC_InspecManage.aspx";
            HyperLink4.NavigateUrl = "QC_Inspection_Manage.aspx";
            HyperLink5.NavigateUrl = "~/PC_Data/PC_TBPC_Otherpur_Bill_List.aspx";
            HyperLink6.NavigateUrl = "~/PC_Data/PC_TBPC_Otherpur_Bill_Audit.aspx";
            HyperLink7.NavigateUrl = "QC_Reject_Product.aspx";
            //HyperLink8.NavigateUrl = "QC_ZJXGSH_TOTAL.aspx";
            HyperLink9.NavigateUrl = "QC_TargetAnalyze_List.aspx";
            HyperLink21.NavigateUrl = "~/PM_Data/PM_FINISHED_IN_Audit.aspx";
        }


        //不合格品通知单
        private void Reject_Pro()
        {
            int num = 0;
            int num1 = 0;
            int num2 = 0;
            string userid = Session["UserID"].ToString();
            //先找出所有没审的
            string sqltext = "select count(1) from dbo.View_TBQC_RejectPro_Info_Detail where ( (state='7' and SPR_ZL_ID='" + Session["UserID"] + "') or  (STATE='1' and PSR_ID='" + Session["UserID"] + "') or (STATE='2' and SPR_ID='" + Session["UserID"] + "') or (STATE='3' and BZR='" + Session["UserID"] + "')  or (STATE='6' and ZGLD_ID='" + Session["UserID"] + "') ) ";
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

        /// <summary>
        /// 需用计划驳回
        /// </summary>
        private void GetBackTask()
        {
            string sql = "select * from TBPC_PLAN_BACK where state='0' and sqrid='" + Session["UserID"] + "'";

            lblBack.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count + "）";
        }

        //------------------------成品入库审批----------------------------------
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
            }
        }

        protected void GetlbGKCC()
        {
            string userid = Session["UserID"].ToString();
            string sql = " select * from TBCM_CUSTOMER where CM_ZJYUAN='" + userid + "' and (CM_CHECK is null or CM_CHECK='')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lbGKCC.Text = "(" + dt.Rows.Count.ToString() + ")";
            }
        }

        /*********************报检的数量*************************/

        /*得到报检分工数*/

        private void GetMyBaoJianFG()
        {
            string sqlText = "select count(*) from TBQM_APLYFORINSPCT where AFI_ASSGSTATE='0'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    lb_task_baojian.Visible = false;
                }
                else
                {
                    lb_task_baojian.Visible = true;
                    lb_task_baojian.Text = "(" + dr[0].ToString() + ")";
                }
            }
            dr.Close();
        }
        private void GET_FHTZ()//发货通知
        {
            string sql = string.Format("select CM_FID from View_CM_FaHuo where CM_CONFIRM='2' group by CM_FID");
            fhtz.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
        }

        /*********************得到我的质检修改审批任务的数量*************************/
        //private void Getshnum()
        //{
        //    string userid = Session["UserID"].ToString();
        //    string strid = userid.Substring(0, 2);
        //    if (strid == "05")
        //    {
        //        string sqltext = "select count(*) from View_ZJXGSH_APLYFORINSPECT where AFI_STATUS='0' AND AFI_FIR_PERID='" + userid + "' and  AFI_FIR_JG is null ";
        //        SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
        //        if (dr.Read())
        //        {
        //            string num = dr[0].ToString();
        //            if (num == "0")
        //            {
        //                lb_shnum.Visible = false;
        //            }
        //            else
        //            {
        //                lb_shnum.Visible = true;
        //                lb_shnum.Text = "(" + num + ")";
        //            }
        //        }
        //        dr.Close();

        //    }
        //    if (strid == "07")
        //    {
        //        string sqltext2 = "select count(*) from View_ZJXGSH_APLYFORINSPECT where AFI_STATUS='0' AND AFI_SEC_PERID='" + userid + "' and  AFI_SEC_JG is null ";
        //        SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext2);
        //        if (dr.Read())
        //        {
        //            string num = dr[0].ToString();
        //            if (num == "0")
        //            {
        //                lb_shnum.Visible = false;
        //            }
        //            else
        //            {
        //                lb_shnum.Visible = true;
        //                lb_shnum.Text = "(" + num + ")";
        //            }
        //        }
        //        dr.Close();
        //    }

        //}




        private void GetMyBaoJian()
        {
            string sqlText = "";
            string aa_js = "select r_name from TBDS_STAFFINFO where  st_id='" + Session["UserID"].ToString() + "' ";
            DataTable dt_r_js = DBCallCommon.GetDTUsingSqlText(aa_js);
            if (Session["UserDeptID"].ToString() != "12" && dt_r_js.Rows[0]["R_NAME"].ToString() != "'质量通用角色'")
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

     

              
        /*********************得到未分工的数量*************************/

        private void GetTaskFenGong()
        {
            string sqlText = "select count(*) from TBQM_QTSASSGN where QSA_STATE='0'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    lb_task_fengong.Visible = false;
                }
                else
                {
                    lb_task_fengong.Visible = true;
                    lb_task_fengong.Text = "(" + dr[0].ToString() + ")";
                }
            }
            dr.Close();
        }
      
        //采购审核任务
        private void Get_CGSP()
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


        //报废通知单
        private void Discard_Pro()
        {
            int num = 0;
            //先找出所有没审的
            string sqltext1 = "select Rev_id,Per_type from TBQC_DiscardPro_Rev where " +
                           " (Per_note='' or Per_note is null) and Per_id='" + Session["UserID"].ToString() + "'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                switch (dt1.Rows[i]["Per_type"].ToString())
                {
                    case "1":  //签发人
                        num++; break;
                    case "2":   //技术负责人
                        if (Check_DiscardRev("1", dt1.Rows[i]["Rev_id"].ToString()))
                        { num++; } break;
                    case "3":   //技术负责人
                        if (Check_DiscardRev("2", dt1.Rows[i]["Rev_id"].ToString()))
                        { num++; } break;
                    case "4":   //技术负责人
                        if (Check_DiscardRev("3", dt1.Rows[i]["Rev_id"].ToString()))
                        { num++; } break;
                }
            }
            //if (num > 0)
            //{
            //    lb_discardpro.Text = "(" + num.ToString() + ")";
            //}
        }

        private bool Check_RejectRev(string pertype,string revid)
        {
            string sql = "select Per_note from TBQC_RejectPro_Rev where Rev_id='" + revid + "' and Per_type=" +
                                    " '" + pertype+ "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0]["Per_note"].ToString() == "")
                return false;
            else
                return true;
        }

        private bool Check_DiscardRev(string pertype, string revid)
        {
            string sql = "select Per_note from TBQC_DiscardPro_Rev where Rev_id='" + revid + "' and Per_type=" +
                                    " '" + pertype + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0]["Per_note"].ToString() == "")
                return false;
            else
                return true;
        }

        private void GetlbCLD()//售后处理单
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
            sql += "(CLD_SPR2='" + username + "' and CLD_SPZT='1y') or ";
            sql += "(CLD_SPR4='" + username + "' and CLD_SPZT='2y') or ";
            sql += "(CLD_SPR5='" + username + "' and CLD_SPZT='4y')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            lbCLD.Text = "(" + dt.Rows[0][0].ToString() + ")";
        }

        private void GetlbLXD()//售后联系单
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

        private void GetlbGZLXD()//工作联系单
        {
            string username = Session["UserName"].ToString();
            string depid = Session["UserDeptID"].ToString();
            string sql = " select count(LXD_ID) from CM_GZLXD where (LXD_SPR1='" + username + "' and LXD_SPZT='0') or ";
            sql += "(LXD_SPR2='" + username + "' and LXD_SPZT='1')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            lbGZLXD.Text = "(" + dt.Rows[0][0].ToString() + ")";
        }

        private void GetlbTZTHTZD()//图纸替换通知
        {
            string username = Session["UserName"].ToString();
            string sql = " select count(TZD_ID) from CM_TZTHTZD where (TZD_SPR1='" + username + "' and TZD_SPZT='0') or ";
            sql += "(TZD_SPR2='" + username + "' and TZD_SPZT='1')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            lbTZTHTZD.Text = "(" + dt.Rows[0][0].ToString() + ")";
        }
    }
}
