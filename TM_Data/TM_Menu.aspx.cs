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

namespace ZCZJ_DPF.TM_Data
{

    public partial class TM_Menu : BasicPage
    {
        string sqlText;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {

                InitUrl();

            }
            GetAssign();
            GetTask();
            getdydsh();
            Get_CGSP1();
            SetTip();
            SetTip2();
            SetTip3();
            Reject_Pro();
            GetProcessCard();
            GetProcessCardGen();
            getxyjhbg();//需用计划变更
            GetBackTask();//需用计划驳回
      
            GetlbCLD();
            GetlbLXD();
            GetlbTZTHTZD();
            GET_FHTZ();
            CheckUser(ControlFinder);
            GetlbDYTZ();

            mp_ck_bt_f();//代用待查看
        }
      
        private void GetBackTask()
        {
            string sql = "select * from TBPC_PLAN_BACK where state='0' and sqrid='" + Session["UserID"] + "'";

            lblBack.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count + "）";
        }

        private void GetAssign()
        {
            sqlText = "select count(1) from TBPM_TCTSASSGN where TSA_STATE='0'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    lblTechAssign.Visible = false;
                }
                else
                {
                    lblTechAssign.Text = "(" + dr[0].ToString() + ")";
                }
            }
            dr.Close();
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

        private void InitUrl()
        {
            HyperLink1.NavigateUrl = "TM_Tech_assign.aspx";
            HyperLink9.NavigateUrl = "~/PC_Data/PC_TBPC_Marreplace_list.aspx";
            HyperLink3.NavigateUrl = "TM_Mytast_List.aspx";
            HyperLink4.NavigateUrl = "TM_Leader_Task.aspx";

            HyperLink6.NavigateUrl = "TM_ProcessCard_List.aspx";
            HyperLink16.NavigateUrl = "TM_ProcessCard_List_Gen.aspx";
            HyperLink17.NavigateUrl = "TM_GongShi_List.aspx";
            HyperLink8.NavigateUrl = "~/QC_Data/QC_Reject_Product.aspx";

            HyperLink10.NavigateUrl = "TM_Design_Bom.aspx";
            HyperLink11.NavigateUrl = "~/PC_Data/PC_TBPC_Otherpur_Bill_List.aspx";
          
            //   HyperLink14.NavigateUrl = "TM_PS_Search.aspx";
            HyperLink2.NavigateUrl = "~/PC_Data/PC_TBPC_Otherpur_Bill_Audit.aspx";
            //HyperLink16.NavigateUrl = "~/PM_Data/PM_PCMainPlan_View.aspx";
            HyperLink18.NavigateUrl = "TM_WorkLoad.aspx";
            HyperLink20.NavigateUrl = "~/PC_Data/PC_TBPC_Purchange_new.aspx";
            HyperLink21.NavigateUrl = "~/PC_Data/PC_TBPC_Purchaseplan_start.aspx";
            HyperLink19.NavigateUrl = "TM_MP_Back.aspx";
            HyperLink13.NavigateUrl = "TM_GSFPMANAGEMENT.aspx";

        }
        //标记审核任务
        private void GetTask()
        {
            sqlText = "select sum(num) from (";
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
                }
                else
                {
                    task.Text = "(" + dr[0].ToString() + ")";
                }
            }
            dr.Close();

        }

        //2016.11.11代用查看，艾广修
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


        //顾客服务通知
        protected void SetTip()
        {
            string sql = "select CM_ID from TBCM_APPLICA where CM_PART like '%03%' and (CM_DIRECTOR='' or CM_DIRECTOR is null)";
            lblFWTZ.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
        }
        //顾客服务处理
        protected void SetTip2()
        {
            string sql = "select CM_ID from TBCM_APPLICA where CM_CLPART like '%03%' and CM_CHULI='N'";
            lblFWCL.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
        }
        //计划单变更
        protected void SetTip3()
        {
            string sql = "select * from TBCM_CHANLIST where CM_STATE='2' and CM_BT1='0'";

            bgtz.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count + "）";
        }
        //质量问题处理
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
            sql += "(CLD_SPR2='" + username + "' and CLD_SPZT='1y') or ";
            sql += "(CLD_SPR4='" + username + "' and CLD_SPZT='2y') or ";
            sql += "(CLD_SPR5='" + username + "' and CLD_SPZT='4y')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            lbCLD.Text = "(" + dt.Rows[0][0].ToString() + ")";
        }
        //售后服务
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
        //图纸替换
        private void GetlbTZTHTZD()
        {
            string username = Session["UserName"].ToString();
            string sql = " select count(TZD_ID) from CM_TZTHTZD where (TZD_SPR1='" + username + "' and TZD_SPZT='0') or ";
            sql += "(TZD_SPR2='" + username + "' and TZD_SPZT='1')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            lbTZTHTZD.Text = "(" + dt.Rows[0][0].ToString() + ")";
        }
        private void GET_FHTZ()//发货通知
        {
            string sql = string.Format("select CM_FID from View_CM_FaHuo where CM_CONFIRM='2' and CM_FHZT='0' group by CM_FID");
            fhtz.Text = "（" + DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString() + "）";
        }


        //不合格品通知单
        private void Reject_Pro()
        {
            int num = 0;
            int num1 = 0;
            int num2 = 0;
            string userid = Session["UserID"].ToString();
            //先找出所有没审的
            string sqltext = "select count(1) from dbo.View_TBQC_RejectPro_Info_Detail where (state='7' and SPR_ZL_ID='" + userid + "') or (state='1' and PSR_ID='" + userid + "') or(state='2' and SPR_ID='" + userid + "') or (state='3' and BZR='" + userid + "')";
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

        private void GetProcessCard()
        {
            sqlText = "select sum(num) from (";
            sqlText += "select count(*) as num from TBPM_PROCESS_CARD where PRO_REVIEWA='" + Session["UserID"].ToString() + "' and PRO_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_PROCESS_CARD where PRO_REVIEWB='" + Session["UserID"].ToString() + "' and PRO_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_PROCESS_CARD where PRO_REVIEWC='" + Session["UserID"].ToString() + "' and PRO_STATE='6' ";
            sqlText += ") as temp ";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    lblProcessCard.Visible = false;
                }
                else
                {
                    lblProcessCard.Text = "(" + dr[0].ToString() + ")";
                }
            }
            dr.Close();

        }

        private void GetProcessCardGen()
        {
            sqlText = "select sum(num) from (";
            sqlText += "select count(*) as num from TBPM_PROCESS_CARD_GENERAL where PRO_REVIEWA='" + Session["UserID"].ToString() + "' and PRO_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_PROCESS_CARD_GENERAL where PRO_REVIEWB='" + Session["UserID"].ToString() + "' and PRO_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_PROCESS_CARD_GENERAL where PRO_REVIEWC='" + Session["UserID"].ToString() + "' and PRO_STATE='6' ";
            sqlText += ") as temp ";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    lblProcessCardGen.Visible = false;
                }
                else
                {
                    lblProcessCardGen.Text = "(" + dr[0].ToString() + ")";
                }
            }
            dr.Close();

        }

        private void GetGongShi()
        {
            sqlText = "select sum(num) from (";
            sqlText += "select count(*) as num from TBPM_GONGSHI where PRO_REVIEWA='" + Session["UserID"].ToString() + "' and PRO_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_PROCESS_CARD_GENERAL where PRO_REVIEWB='" + Session["UserID"].ToString() + "' and PRO_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_PROCESS_CARD_GENERAL where PRO_REVIEWC='" + Session["UserID"].ToString() + "' and PRO_STATE='6' ";
            sqlText += ") as temp ";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    lblProcessCard.Visible = false;
                }
                else
                {
                    lblProcessCard.Text = "(" + dr[0].ToString() + ")";
                }
            }
            dr.Close();

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

    }
}


