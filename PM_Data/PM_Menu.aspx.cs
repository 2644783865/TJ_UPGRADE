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

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Menu : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            if (!IsPostBack)
            {
                InitUrl();
            }
            CheckUser(ControlFinder);
            GET_FENGONG();
            GET_WXSP();
            //GET_GSSP();
           // GET_FAYUN();
            GET_FINISHED();
            GET_FINISHEDOUT();
            GET_MYTASK();
            Get_CGSP();
            GET_WXBJSP();
            Reject_Pro();
            GET_FHTZ();
            getdydsh();
            GET_FYSP();
            SetTip();
            GET_ZZMX();
            GET_wxbj();
            GetMyBaoJian();
            GetKaiPiao();
            GetlbDYTZ();
            GetBackTask();//需用计划驳回

            mp_ck_bt_f();//代用待查看
            zzmx_ck_bt_f();//制作明细查看


          //string  sql = string.Format("select CH_ID from View_CM_Change where CM_STATE='1' and ((CM_MANCLERK='{0}') or (CM_PS1='{0}' and CM_PSYJ1='1') or (CM_PS2='{0}' and CM_PSYJ2='1' and CM_PSYJ1='2') or (CM_PS3='{0}' and CM_PSYJ3='1' and CM_PSYJ1='2' and CM_PSYJ2='2'))", Session["UserID"].ToString());
            //03 技术--CM_BT1     04生产--CM_BT2      05 采购--CM_BT3
            string sql = "select * from TBCM_CHANLIST where CM_STATE='2' and CM_BT2='0'";
            //DBCallCommon.GetDTUsingSqlText(sql).Rows.Count;
          //  string sql = "select * from View_CM_Change where CM_STATE='2'";
            bgtz.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
        }

        private void GET_FHTZ()
        {
            string sql = string.Format("select CM_FID from View_CM_FaHuo where CM_CONFIRM='2' and CM_FHZT='0' group by CM_FID");
            fhtz.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
        }

        //2016.11.11制作明细查看，艾广修
        private void zzmx_ck_bt_f()
        {
            string sql_ck_bt = "select ((select count(*) from View_TM_MSFORALLRVW as A left join TBMP_MANUTSASSGN AS B on A.MS_ENGID=B.MTA_ID where MS_CK_BT='1' and MS_STATE='8' )+(select count(*) from View_TM_MSCHANGERVW as A left join TBMP_MANUTSASSGN AS B on A.MS_ENGID=B.MTA_ID where MS_CK_BT='1' and MS_STATE='8' )) as sum_count  ";
            DataTable dt_ck_bt = DBCallCommon.GetDTUsingSqlText(sql_ck_bt);
            if (dt_ck_bt.Rows[0][0].ToString().Trim()!="0" && Session["UserID"].ToString() == "73")
            {
                zzmx_ck_bt.Text = "(" + dt_ck_bt.Rows[0][0].ToString().Trim() + ")";
            }
            else
            {
                zzmx_ck_bt.Visible = false;
            }
        }


        private void GET_ZZMX()//制作明细变更
        {
            string sql = "select * from View_TM_MSCHANGERVW as A left join TBMP_MANUTSASSGN AS B on A.MS_ENGID=B.MTA_ID where MS_FINSTATUS='0'and MS_STATE='8' and MTA_DUY='" + Session["UserName"].ToString() + "'";
            zzmxgl.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
        }
        protected void SetTip()
        {
            //string dep = "04";
            string sql = "select CM_ID from TBCM_APPLICA where CM_CLPART like '%04%' and CM_CHULI='N' and CM_STATE='2'";
            fwsq.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
        }
        //开票管理
        private void GetKaiPiao()
        {
            string sql = "select count(1) from (select * from CM_KAIPIAO as d left join (select a.cId , stuff((select sprId+',' from CM_KAIPIAO_HUISHEN b where b.cId =a.cId and( b.result is null or b.result='') for xml path('')),1,0,',') 'sprId ' from CM_KAIPIAO_HUISHEN  a  group by  a.cId)c on d.KP_TaskID=c.cId)e where ((KP_SPSTATE='1' and KP_SHRIDA='" + Session["UserID"].ToString() + "') or (KP_SPSTATE='2' and KP_SHRIDB='" + Session["UserID"].ToString() + "') or (KP_HSSTATE='1' and sprId like '%," + Session["UserID"].ToString() + ",%'))";
            lblKaiPiao.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows[0][0].ToString() + "）";
        }
        private void GET_wxbj()
        {
            string sql = "select count(distinct MS_PID) as num from VIEW_TM_WXDetail where (MS_PID in (select distinct PM_PID from  TBPM_SCWXRVW where PM_SPZT=3 ) and MS_scwaixie='4')";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read() && dr["num"].ToString() != "0")
            {
                wxbj.Text = "(" + dr["num"].ToString() + ")";
            }
            dr.Close();
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

        //2016.11.14代用单查看，艾广修
        private void mp_ck_bt_f()
        {
            string sql_mp_ck = "select count(*) from TBPC_MARREPLACETOTAL where mp_ck_bt='1'";
            DataTable dt_mp_ck = DBCallCommon.GetDTUsingSqlText(sql_mp_ck);
            if (dt_mp_ck.Rows[0][0].ToString().Trim()!="0" && Session["UserID"].ToString() == "73")
            {
                lb_mp_ck.Text = "(" + dt_mp_ck.Rows[0][0].ToString().Trim() + ")";
            }
            else
            {
                lb_mp_ck.Visible = false;
            }
        }

        /// <summary>
        /// 代用通知
        /// </summary>
        private void GetlbDYTZ()
        {
            string username = Session["UserName"].ToString();
            string sql = "select count(TZD_ID) from PM_SCDYTZ where  ";
            sql += " (TZD_SPR1='" + username + "' and TZD_SPZT='0') or ";
            sql += "(TZD_SPR2='" + username + "' and TZD_SPZT='1y')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (Convert.ToInt32(dt.Rows[0][0].ToString()) > 0)
            {
                lbDYTZ.Text = "(" + dt.Rows[0][0].ToString() + ")";
            }
        }

        /// <summary>
        /// 代用单审核数量
        /// </summary>
        private void getdydsh()
        {
            string sqltext = "";
            int num = 0;
            sqltext = "select count(*) from TBPC_MARREPLACETOTAL where  " +
                      "(MP_FILLFMID='" + Session["UserID"].ToString() +
                      "' and (MP_STATE='000' or MP_STATE='200' or MP_STATE='300' or MP_STATE='002'))";
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
        /// <summary>
        /// 外协比价审批提醒
        /// </summary>
        private void GET_WXBJSP()
        {
            //string userid = Session["UserID"].ToString();
            //string sqltext = "select count (distinct picno) as num from View_TBMP_IQRCMPPRICE_RVW1 where (zdrid='" + userid + "' or shbid='" + userid + "' or shcid='" + userid + "' or shdid='" + userid + "' or sheid='" + userid + "' or shfid='" + userid + "' or shgid='" + userid + "') and ( totalstate='1' or totalstate='3')";
            //SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            //if (dr.Read() && dr["num"].ToString() != "0")
            //{
            //    lbl_wxbjsp.Text = "(" + dr["num"].ToString() + ")";
            //}
            //dr.Close();


            //string sqltext = "";
            //int num = 0;
            //if (Session["UserID"].ToString() == "95")//于来义
            //{
            //    sqltext = "SELECT count(*) from TBMP_IQRCMPPRCRVW where (ICL_STATE='1' or ICL_STATE='3') and ICL_REVIEWB='" + Session["UserID"].ToString() + "' and ICL_STATEA='0'";
            //    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            //    while (dr.Read())
            //    {
            //        num += Convert.ToInt32(dr[0].ToString());
            //    }
            //    dr.Close();
            //}

            //else if ( Session["UserID"].ToString() == "2")// 王总
            //{
            //    sqltext = "SELECT count(*) from TBMP_IQRCMPPRCRVW where (ICL_STATE='1' or ICL_STATE='3') and ICL_STATEA='2' and ICL_STATEB='0'";
            //    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            //    while (dr.Read())
            //    {
            //        num += Convert.ToInt32(dr[0].ToString());
            //    }
            //    dr.Close();
            //}
            //else
            //{
            //    //制单人任务
            //    sqltext = "select count(*) from TBMP_IQRCMPPRCRVW where  ICL_STATE='3' and  ICL_REVIEWA='" + Session["UserID"].ToString() + "'";
            //    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            //    while (dr.Read())
            //    {
            //        num += Convert.ToInt32(dr[0].ToString());
            //    }
            //    dr.Close();
            //}
            //if (num == 0)
            //{
            //    lbl_wxbjsp.Visible = false;
            //}
            //else
            //{
            //    lbl_wxbjsp.Visible = true;
            //    lbl_wxbjsp.Text = "(" + num.ToString() + ")";
            //}
            string sqltext = "SELECT count(*) as num from TBMP_IQRCMPPRCRVW where ((ICL_STATE='1' or ICL_STATE='3') and ((ICL_REVIEWB='" + Session["UserID"].ToString() + "' and ICL_STATEA='0')or (ICL_STATEA='2' and ICL_STATEB='0'and ICL_STATEB='0'and ICL_REVIEWC='" + Session["UserID"].ToString() + "')))or( ICL_STATE='3' and  ICL_REVIEWA='" + Session["UserID"].ToString() + "')";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read() && dr["num"].ToString() != "0")
            {
                lbl_wxbjsp.Text = "(" + dr["num"].ToString() + ")";
            }
            //没有任务就不可见
            else
            {
                lbl_wxbjsp.Visible = false;
            }
            dr.Close();
        }
        private void GET_FENGONG()
        {
            string sqltext = "select count(MTA_ID) as num from View_TBMP_TASKASSIGN where MTA_STATUS='0'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read()&&dr["num"].ToString() != "0")
            {
                lblfengong.Text = "("+dr["num"].ToString()+")";
            }
            dr.Close();
        }
        private void GET_MYTASK()
        {
            string username = Session["UserName"].ToString();
            string sqltext = "";
            sqltext += "select count(*) as num from View_TBMP_TASKASSIGN where  MTA_DUY like '%" +username + "%'and MTA_STATUS='1' and MTA_YNLOOK='0'";

            SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sqltext);
          
            if (dr1.Read() && dr1["num"].ToString() != "0")
            {
                lblmytask.Text = "(" + dr1["num"].ToString() + ")";
            }
            dr1.Close();

        }
        //private void GET_FAYUN()
        //{
        //    string sqltext = "select count(*) as num from  View_CM_FaHuo as A  join TBMP_FINISHED_STORE as B ON (A.TSA_ID=B.KC_TSA AND A.ID=B.KC_ZONGXU and A.CM_FHNUM<=B.KC_KCNUM) WHERE A.CM_CONFIRM ='2' AND A.CM_STATUS='0' OR A.CM_STATUS='3'";
        //    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
        //    if (dr.Read() && dr["num"].ToString() != "0")
        //    {
        //        lbl_fayun.Text = "(" + dr["num"].ToString() + ")";
        //    }
        //    dr.Close();
        //}
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
  /// <summary>
  /// 采购审批
  /// </summary>
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
            }
        }
        private void GetMyBaoJian()
        {

            if (Session["UserDept"].ToString() == " ")
            {
                //如果此人不是质量部的，也就是报检人，那么他默认看到的就是他自己报检之后的未质检的。

                //string sqlText = "select count(*) from TBQM_APLYFORINSPCT where AFI_STATE='1' AND AFI_MAN='" + Session["UserID"].ToString() + "'";

                //pager.StrWhere = "AFI_STATE='0' AND AFI_MAN='" + Session["UserID"].ToString() + "'";
            }
            else
            {   //如果此人是质量部的，那个他看到的是全部未质检的
                string sqlText = "select count(*) from TBQM_APLYFORINSPCT where AFI_STATE='0' and AFI_QCMAN='" + Session["UserID"].ToString() + "'";
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
        }
        private void InitUrl()
        {
            HyperLink1.NavigateUrl = "PM_Management_View.aspx";
            HyperLink2.NavigateUrl = "PM_Xie_Management.aspx";
            HyperLink3.NavigateUrl = "PM_Xie_Audit_List.aspx";
            HyperLink4.NavigateUrl = "PM_Xie_Mana_List.aspx";
            HyperLink5.NavigateUrl = "PM_FHList.aspx";
            HyperLink6.NavigateUrl = "PM_Xie_list.aspx";
            HyperLink7.NavigateUrl = "PM_GongShi_List.aspx";
            //HyperLink8.NavigateUrl = "PM_GongShi_Audit.aspx";
            HyperLink9.NavigateUrl = "PM_TOOL_LIST.aspx";
            HyperLink10.NavigateUrl = "PM_TOOL_IN.aspx";
            HyperLink11.NavigateUrl = "PM_TOOL_OUT.aspx";
            HyperLink12.NavigateUrl = "PM_Finished_LIST.aspx";
            HyperLink13.NavigateUrl = "PM_Finished_IN.aspx";
            HyperLink14.NavigateUrl = "PM_Finished_OUT.aspx";
            HyperLink15.NavigateUrl = "PM_Expense_List.aspx";
            HyperLink16.NavigateUrl = "PM_ProjectPlan.aspx";
            HyperLink17.NavigateUrl = "PM_fayun_list.aspx";
            HyperLink18.NavigateUrl = "~/QC_Data/QC_Reject_Product.aspx";
            HyperLink19.NavigateUrl = "PM_FINISHED_IN_Audit.aspx";
            HyperLink20.NavigateUrl = "PM_FINISHED_OUT_Audit.aspx";
            HyperLink21.NavigateUrl = "PM_Xie_union.aspx";
            HyperLink22.NavigateUrl = "PM_details_manage.aspx";
            HyperLink23.NavigateUrl="~/PC_Data/PC_TBPC_Otherpur_Bill_List.aspx";
            HyperLink24.NavigateUrl = "PM_Manut_Mytask_list.aspx";
            HyperLink25.NavigateUrl = "~/PC_Data/PC_TBPC_Otherpur_Bill_Audit.aspx";
            HyperLink26.NavigateUrl = "PM_CNFB.aspx";
            HyperLink27.NavigateUrl = "PM_BANZUJS.aspx";
            HyperLink29.NavigateUrl = "~/Contract_Data/CM_Contract_WW.aspx?ContractForm=1";
            HyperLink30.NavigateUrl = "~/PC_Data/PC_TBPC_Marreplace_list.aspx";
            HyperLink32.NavigateUrl = "PM_YOUQI.aspx";
            HyperLink33.NavigateUrl = "~/QC_Data/QC_Inspection_Manage.aspx";
            HyperLink35.NavigateUrl = "PM_ProcessCard.aspx";
            HyperLink36.NavigateUrl = "PM_ProcessCard_Gen.aspx";
            HyperLink37.NavigateUrl = "PM_Xie_Order.aspx";
            HyperLink38.NavigateUrl = "PM_Xie_Accounts.aspx";
            HyperLink39.NavigateUrl = "PM_Xie_Plan.aspx";
            HyperLink40.NavigateUrl = "PM_CPFYJSDGL.aspx";
            HyperLink44.NavigateUrl = "~/TM_Data/TM_MP_Back.aspx";
        }

        private void GetBackTask()
        {
            string sql = "select * from TBPC_PLAN_BACK where state='0' and sqrid='" + Session["UserID"] + "'";

            lblBack.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count + "）";
        }
    }
}
