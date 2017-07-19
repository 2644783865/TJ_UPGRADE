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

namespace ZCZJ_DPF.MT_Data
{
    public partial class MT_Menu : BasicPage
    {
        string sqlText = "";
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
            CheckUser(ControlFinder);
           if (!IsPostBack)
           {
               InitUrl();

               GetTask();   //技术任务
               GetMyTouBiao();//投标评审
               Get_CGSP();//采购审批
               getwlzy();//物料占用管理
               getxbjsh();//比价单数量
               getdydsh();//代用单数量
               getbggl();//采购变更管理
               GetMyBaoJianFG();//报检任务
               GetMyBaoJian();//我的报检任务
               Reject_Pro();//不合格品通知单
               Discard_Pro();//整改通知单
               GetMyViewTask();//合同审批任务
               CRUndo();//待办款项
               GetTiskNotice();//部门完工
               GetCusup_Review();//厂商审批
               GetShfw();//售后服务
           }
        }
        
        private void InitUrl()
        {

            HyperLink1.NavigateUrl = "~/CM_Data/PD_DocManage.aspx";
            HyperLink2.NavigateUrl = "~/TM_Data/TM_Leader_Task.aspx";
            HyperLink3.NavigateUrl = "~/PC_Data/PC_TBPC_Otherpur_Bill_Audit.aspx";
            HyperLink4.NavigateUrl = "~/PC_Data/PC_TBPC_Purchaseplan_start.aspx";
            HyperLink5.NavigateUrl = "~/PC_Data/PC_TBPC_Purchaseplan_check_list.aspx";
            HyperLink6.NavigateUrl = "~/PC_Data/TBPC_IQRCMPPRCLST_checked.aspx";
            HyperLink7.NavigateUrl = "~/PC_Data/PC_TBPC_Purchange_all_list.aspx";          
            HyperLink9.NavigateUrl = "~/QC_Data/QC_Inspection_Manage.aspx";
            HyperLink10.NavigateUrl = "~/QC_Data/QC_Reject_Product.aspx";
            HyperLink11.NavigateUrl = "~/QC_Data/QC_InspecManage.aspx";
            HyperLink12.NavigateUrl = "~/QC_Data/QC_DiscardPro_Info.aspx";
            HyperLink13.NavigateUrl = "~/Basic_Data/tbcs_cusupinfo_Review.aspx";        
            HyperLink14.NavigateUrl = "~/Contract_Data/CM_MyContractReviewTask.aspx";
            HyperLink15.NavigateUrl = "~/Contract_Data/CM_CRUndo.aspx";
            HyperLink8.NavigateUrl = "~/FM_Data/CB_Confirm.aspx";
            HyperLink16.NavigateUrl = "~/PC_Data/PC_TBPC_Marreplace_list.aspx";
            HyperLink17.NavigateUrl = "~/CM_Data/CM_shfwsq.aspx";
        }

        //投标评审任务数
        private void GetMyTouBiao()
        {
            string userID = Session["UserID"].ToString();

            string sqlText = "select count(*) from TBBS_CONREVIEW where BP_STATUS ='0' AND BP_YESORNO='Y' and ((BC_REVIEWERA='" + userID + "' AND  (BP_RVIEWA=''or BP_RVIEWA is null)) or (BC_DRAFTER='" + userID + "') or (BC_REVIEWERB='" + userID + "' AND  (BP_RVIEWB='' or BP_RVIEWB is null)) or (BC_REVIEWERC='" + userID + "' AND  (BP_RVIEWC='' or BP_RVIEWC is null)) or (BC_REVIEWERD='" + userID + "' AND (BP_RVIEWD='' or BP_RVIEWD is null)) or (BC_REVIEWERE='" + userID + "' AND  (BP_RVIEWE='' or BP_RVIEWE is null)) or (BC_REVIEWERF='" + userID + "' AND  (BP_RVIEWF='' or BP_RVIEWF is null)) or (BC_REVIEWERG='" + userID + "' AND  (BP_RVIEWG='' or BP_RVIEWG is null)) OR (BC_REVIEWERH='" + userID + "' AND  (BP_RVIEWH='' or BP_RVIEWH is null)) OR (BC_REVIEWERI='" + userID + "' AND  (BP_RVIEWI='' or BP_RVIEWI is null)) OR (BC_REVIEWERJ='" + userID + "' AND  (BP_RVIEWJ='' or BP_RVIEWJ is null)))";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    lb_toubiao.Visible = false;
                }
                else
                {
                    lb_toubiao.Visible = true;
                    lb_toubiao.Text = "(" + dr[0].ToString() + ")";
                }
            }
            dr.Close();
        }


        //标记审核任务
        private void GetTask()
        {
            sqlText = "select sum(num) from (";
            sqlText += "select count(*) as num from TBPM_MPFORALLRVW where MP_REVIEWA='" + Session["UserID"].ToString() + "' and MP_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_MPFORALLRVW where MP_REVIEWB='" + Session["UserID"].ToString() + "' and MP_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_MPFORALLRVW where MP_REVIEWC='" + Session["UserID"].ToString() + "' and MP_STATE='6' ";
            sqlText += " union all select count(*) as num from TBPM_MPCHANGERVW where MP_REVIEWA='" + Session["UserID"].ToString() + "' and MP_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_MPCHANGERVW where MP_REVIEWB='" + Session["UserID"].ToString() + "' and MP_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_MPCHANGERVW where MP_REVIEWC='" + Session["UserID"].ToString() + "' and MP_STATE='6' ";
            sqlText += " union all select count(*) as num from TBPM_MSFORALLRVW where MS_REVIEWA='" + Session["UserID"].ToString() + "' and MS_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_MSFORALLRVW where MS_REVIEWB='" + Session["UserID"].ToString() + "' and MS_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_MSFORALLRVW where MS_REVIEWC='" + Session["UserID"].ToString() + "' and MS_STATE='6' ";
            sqlText += " union all select count(*) as num from TBPM_MSCHANGERVW where MS_REVIEWA='" + Session["UserID"].ToString() + "' and MS_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_MSCHANGERVW where MS_REVIEWB='" + Session["UserID"].ToString() + "' and MS_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_MSCHANGERVW where MS_REVIEWC='" + Session["UserID"].ToString() + "' and MS_STATE='6' ";
            sqlText += " union all select count(*) as num from TBPM_OUTSOURCETOTAL where OST_REVIEWERA='" + Session["UserID"].ToString() + "' and OST_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_OUTSOURCETOTAL where OST_REVIEWERB='" + Session["UserID"].ToString() + "' and OST_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_OUTSOURCETOTAL where OST_REVIEWERC='" + Session["UserID"].ToString() + "' and OST_STATE='6' ";
            sqlText += " union all select count(*) as num from TBPM_OUTSCHANGERVW where OST_REVIEWERA='" + Session["UserID"].ToString() + "' and OST_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_OUTSCHANGERVW where OST_REVIEWERB='" + Session["UserID"].ToString() + "' and OST_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_OUTSCHANGERVW where OST_REVIEWERC='" + Session["UserID"].ToString() + "' and OST_STATE='6' ";
            sqlText += " union all select count(*) as num from TBPM_PAINTSCHEME where PS_REVIEWA='" + Session["UserID"].ToString() + "' and PS_STATE='2' ";
            sqlText += " union all select count(*) as num from TBPM_PAINTSCHEME where PS_REVIEWB='" + Session["UserID"].ToString() + "' and PS_STATE='4' ";
            sqlText += " union all select count(*) as num from TBPM_PAINTSCHEME where PS_REVIEWC='" + Session["UserID"].ToString() + "' and PS_STATE='6' ";
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


        //未下推需用计划
        private void getxyjhsh()
        {
            string sqltext = "";
            int num = 0;
            sqltext = "select count(*) from TBPC_PCHSPLANRVW  where PR_STATE<=4 ";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            if (num == 0)
            {
                lb_XYplan.Visible = false;
            }
            else
            {
                lb_XYplan.Visible = true;
                lb_XYplan.Text = "(" + num.ToString() + ")";
            }
        }

        //物料占用
        private void getwlzy()
        {
            string sqltext = "";
            int num = 0;
            sqltext = "select  count(distinct(PR_PCODE)) from View_TBPC_MARSTOUSE_TOTAL_ALL where (PR_STATE='1' and PR_REVIEWA='" + Session["UserID"].ToString() + "') or (allshstate='0' and PR_REVIEWB='" + Session["UserID"].ToString() + "')";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            if (num == 0)
            {
                lb_wlzygl.Visible = false;
            }
            else
            {
                lb_wlzygl.Visible = true;
                lb_wlzygl.Text = "(" + num.ToString() + ")";
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
        //比价单数量
        private void getxbjsh()
        {
            string sqltext = "";
            int num = 0;
            if (Session["UserID"].ToString() == "0601001")//杨部长
            {
                sqltext = "SELECT count(*) from TBPC_IQRCMPPRCRVW where (ICL_STATE='1' or ICL_STATE='3') and ICL_REVIEWB='" + Session["UserID"].ToString() + "' and ICL_STATEA='0'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr.Read())
                {
                    num += Convert.ToInt32(dr[0].ToString());
                }
                dr.Close();
            }
            else if (Session["UserID"].ToString() == "01002")//于总
            {
                sqltext = "SELECT count(*) from TBPC_IQRCMPPRCRVW where (ICL_STATE='1' or ICL_STATE='3') and ICL_REVIEWC='" + Session["UserID"].ToString() + "' and ICL_STATEA='2' and ICL_STATEB='0'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr.Read())
                {
                    num += Convert.ToInt32(dr[0].ToString());
                }
                dr.Close();
            }
            else if (Session["UserID"].ToString() == "01001")//邓总
            {
                sqltext = "SELECT count(*) from TBPC_IQRCMPPRCRVW where (ICL_STATE='1' or ICL_STATE='3') and ICL_REVIEWD='" + Session["UserID"].ToString() + "' and ICL_STATEB='2' and ICL_STATEC='0'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr.Read())
                {
                    num += Convert.ToInt32(dr[0].ToString());
                }
                dr.Close();
            }
            else if (Session["UserID"].ToString() == "01003")//柳总
            {
                sqltext = "SELECT count(*) from TBPC_IQRCMPPRCRVW where (ICL_STATE='1' or ICL_STATE='3') and ICL_REVIEWE='" + Session["UserID"].ToString() + "' and ICL_STATEB='2' and ICL_STATED='0'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr.Read())
                {
                    num += Convert.ToInt32(dr[0].ToString());
                }
                dr.Close();
            }
            else if (Session["UserID"].ToString() == "01004")//柳总
            {
                sqltext = "SELECT count(*) from TBPC_IQRCMPPRCRVW where (ICL_STATE='1' or ICL_STATE='3') and ICL_REVIEWF='" + Session["UserID"].ToString() + "' and ICL_STATEB='2' and ICL_STATEE='0'";
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

        //采购变更管理
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

        /*********************得到我的报检任务的数量*************************/
        private void GetMyBaoJian()
        {

            if (Session["UserDept"].ToString() != "质量部")
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


        //不合格品通知单
        private void Reject_Pro()
        {
            int num = 0;
            //先找出所有没审的
            string sqltext1 = "select Rev_id,Per_type from TBQC_RejectPro_Rev where " +
                           " (Per_time='' or Per_time is null) and Per_id='" + Session["UserID"].ToString() + "'" +
                           " and Rev_id in (select Order_id from TBQC_RejectPro_Info where state !=2)";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                switch (dt1.Rows[i]["Per_type"].ToString())
                {
                    case "1":  //签发人
                        num++; break;
                    case "2":   //技术负责人
                        if (Check_RejectRev("1", dt1.Rows[i]["Rev_id"].ToString()))
                        { num++; } break;
                    case "3":   //技术负责人
                        if (Check_RejectRev("2", dt1.Rows[i]["Rev_id"].ToString()))
                        { num++; } break;
                    case "4":   //技术负责人
                        if (Check_RejectRev("3", dt1.Rows[i]["Rev_id"].ToString()))
                        { num++; } break;
                }
            }
            if (num > 0)
            {
                lb_rejectPro.Text = "(" + num.ToString() + ")";
            }
        }

        private bool Check_RejectRev(string pertype, string revid)
        {
            string sql = "select Per_note from TBQC_RejectPro_Rev where Rev_id='" + revid + "' and Per_type=" +
                                    " '" + pertype + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0]["Per_note"].ToString() == "")
                return false;
            else
                return true;
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
            if (num > 0)
            {
                lb_discardpro.Text = "(" + num.ToString() + ")";
            }
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

        //厂商添加删除-审批
        private void GetCusup_Review()
        {
            int Review_num = 0;
            //string str = "select * from TBCS_CUSUP_ADD_DELETE where CS_SPJG in ('0','1') and"+
            //    " id in ( select distinct a.fatherid from TBCS_CUSUP_ReView a inner join TBCS_CUSUP_ReView b " +
            //    "where a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "'" +
            //    " and a.fatherid=b.fatherid and b.CRS_YJ!='0' and if a.CRS_TYPE>'1' then a.CRS_TYPE>b.CRS_TYPE then else 1=1" +
            //    ")";
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


        //我的合同评审任务
        #region
        private void GetMyViewTask()
        {
            int NUM = 0;
            string sqltext = "";
            sqltext = "select * from View_TBCR_View_Detail_ALL " +
                " where CRD_PID='" + Session["UserID"].ToString() + "' and CR_PSZT='1' AND CRD_PSYJ='0'";
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

       
        //待办款项
        private void CRUndo()
        {
            int num_QK = 0;
            int num_YK = 0;
            string sqlstr1 = "";
            sqlstr1 = "select * from View_CM_DBQK";

            DataTable dt_undocr = DBCallCommon.GetDTUsingSqlText(sqlstr1);
            if (dt_undocr.Rows.Count > 0)
            {
                num_QK += dt_undocr.Rows.Count;
            }
            string sqlstr2 = "";
            sqlstr2 = "select a.BP_ID,a.BP_KXMC,a.BP_JE,a.BP_YKRQ,b.PCON_YFK,b.PCON_MONUNIT,b.PCON_JINE,b.PCON_PJNAME,b.PCON_BCODE from TBPM_BUSPAYMENTRECORD as a inner join TBPM_CONPCHSINFO as b on a.BP_HTBH=b.PCON_BCODE  where a.BP_STATE='0'";
            DataTable dt_undobus = DBCallCommon.GetDTUsingSqlText(sqlstr2);
            if (dt_undobus.Rows.Count > 0)
            {
                num_YK += dt_undobus.Rows.Count;
            }
            if (num_QK == 0 && num_YK == 0)
            {
                Undo_QK.Visible = false;
                Undo_YK.Visible = false;
            }
            else
            {
                Undo_QK.Text = "(" + num_QK + ")";
                Undo_YK.Text = "(" + num_YK + ")";
            }
        }
        //部门完工确认
        private void GetTiskNotice()
        {
            if (Session["UserDeptID"].ToString() == "03")
            {
                string sql = " select count(*) from TBCB_BMCONFIRM where JFY='否' or JWW='否' ";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    if (dr[0].ToString() == "0")
                    {
                        tisknotice.Visible = false;
                    }
                    else
                    {
                        tisknotice.Text = "(" + dr[0].ToString() + ")";
                    }
                }
                dr.Close();

             }
            else if (Session["UserDeptID"].ToString() == "12")
            {
                string sql = " select count(*) from TBCB_BMCONFIRM where JFY='是' and JWW='是' ";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                dr.Read();
                if (dr[0].ToString() != "0")
                {
                    tisknotice.Text = "(" + dr[0].ToString() + ")";
                }
               
                else
                {
                    tisknotice.Visible = false;
                }
                dr.Close();
                
            }
            else if (Session["UserDeptID"].ToString() == "07")
            {
                string sql = " select count(*) from TBCB_BMCONFIRM where PRICE !='0'  ";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                dr.Read();
                if (dr[0].ToString() != "0")
                {
                    tisknotice.Text = "(" + dr[0].ToString() + ")";
                }
               
                else
                {
                    tisknotice.Visible = false;
                }
                dr.Close();
                

            }
            else if (Session["UserDeptID"].ToString() == "04")
            {
                string sql = " select count(*) from TBCB_BMCONFIRM where CFY='是' and CFP='是' and  CCRK='是'  ";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                dr.Read();
                if (dr[0].ToString() != "0")
                {
                    tisknotice.Text = "(" + dr[0].ToString() + ")";
                }

                else
                {
                    tisknotice.Visible = false;
                }
                dr.Close();
              
             }
            else if (Session["UserDeptID"].ToString() == "06")
            {
                string sql = " select count(*) from TBCB_BMCONFIRM where SJS='是'  ";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                dr.Read();
                if (dr[0].ToString() != "0")
                {
                    tisknotice.Text = "(" + dr[0].ToString() + ")";
                }

                else
                {
                    tisknotice.Visible = false;
                }
                dr.Close();
                
            }
             else
            {
                tisknotice.Visible = false;
                
            }
                

             }

        private void GetShfw()
        {
            if (Session["UserName"].ToString() == "于建会")
            {
                string sqltext = "select * from CM_SHFWSQ where SH_SPZT=1 and SH_YZSP is null";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {

                    lb_shfw.Visible = false;
                }
                else
                {
                    lb_shfw.Text = "(" + dt.Rows.Count.ToString() + ")";
                }
            }
            else if (Session["UserName"].ToString() == "柳强")
            {
                string sqltext = "select * from CM_SHFWSQ where SH_SPZT=2 and SH_LZSP is null and SH_YZSP<>''";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    lb_shfw.Visible = false;
                }
                else
                {
                    lb_shfw.Text = "(" + dt.Rows.Count.ToString() + ")";
                }
            }
            else if (Session["UserName"].ToString() == "付春雨")
            {
                string sqltext = "select * from CM_SHFWSQ where SH_SPZT=2 and SH_FSP is null and SH_YZSP<>'' and SH_LZSP<>''";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    lb_shfw.Visible = false;
                }
                else
                {
                    lb_shfw.Text = "(" + dt.Rows.Count.ToString() + ")";
                }
            }
            else
            {
                string sqltext = "select * from CM_SHFWSQ where SH_SQR='" + Session["UserName"].ToString().Trim() + "' and (SH_SPZT=1 or SH_SPZT=2)";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    lb_shfw.Visible = false;
                }
                else
                {
                    lb_shfw.Text = "(" + dt.Rows.Count.ToString() + ")";
                }
            }
        }


        
         }


    }

