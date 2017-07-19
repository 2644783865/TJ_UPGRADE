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
             //   CheckUser(ControlFinder);
            }
            //GetMyTask();
            //GetMyTask1();
            //GetMyBaoJianFG();
            //GetMyBaoJian();
            //GetMyTuZhi();
            //GetTaskFenGong();
            //GetNoRead();
            //Get_CGSP();
            //Reject_Pro();
            //Discard_Pro();
            //Getpernum();
            //Getshnum();

        }

        private void InitUrl()
        {
          
            HyperLink1.NavigateUrl = "QC_Task_Manage.aspx";
         
            HyperLink3.NavigateUrl = "QC_InspecManage.aspx";
            HyperLink4.NavigateUrl = "QC_Inspection_Manage.aspx";
            HyperLink5.NavigateUrl = "~/PC_Data/PC_TBPC_Otherpur_Bill_List.aspx";
            HyperLink6.NavigateUrl = "~/PC_Data/PC_TBPC_Otherpur_Bill_Audit.aspx";
            HyperLink7.NavigateUrl = "QC_Reject_Product.aspx";
            HyperLink8.NavigateUrl = "QC_ZJXGSH_TOTAL.aspx";
            HyperLink9.NavigateUrl = "QC_TargetAnalyze_List.aspx";
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

        /*********************得到我的质检审批任务的数量*************************/
        private void Getshnum()
        {
            string userid = Session["UserID"].ToString();
            string strid = userid.Substring(0, 2);
            if (strid == "05")
            {
                string sqltext = "select count(*) from View_ZJXGSH_APLYFORINSPECT where AFI_STATUS='0' AND AFI_FIR_PERID='" + userid + "' and  AFI_FIR_JG is null ";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                if (dr.Read())
                {
                    string num = dr[0].ToString();
                    if (num == "0")
                    {
                        lb_shnum.Visible = false;
                    }
                    else
                    {
                        lb_shnum.Visible = true;
                        lb_shnum.Text = "(" + num + ")";
                    }
                }
                dr.Close();

            }
            if (strid == "07")
            {
                string sqltext2 = "select count(*) from View_ZJXGSH_APLYFORINSPECT where AFI_STATUS='0' AND AFI_SEC_PERID='" + userid + "' and  AFI_SEC_JG is null ";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext2);
                if (dr.Read())
                {
                    string num = dr[0].ToString();
                    if (num == "0")
                    {
                        lb_shnum.Visible = false;
                    }
                    else
                    {
                        lb_shnum.Visible = true;
                        lb_shnum.Text = "(" + num + ")";
                    }
                }
                dr.Close();
            }

        }




        /*********************得到我的报检任务的数量*************************/
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

     

              
        /*********************得到未分工的数量*************************/
        private void GetTaskFenGong()
        {
            string sqlText = "select count(*) from TBQM_QTSASSGN where QSA_STATE='0' AND  QSA_CHGSTATE!='1'";
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

        //不合格品通知单
        private void Reject_Pro()
        {
            int num = 0;
            //先找出所有没审的
            string sqltext1= "select Rev_id,Per_type from TBQC_RejectPro_Rev where " +
                           " (Per_time='' or Per_time is null) and Per_id='" + Session["UserID"].ToString() + "'"+
                           " and Rev_id in (select Order_id from TBQC_RejectPro_Info where state !=2)";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                switch (dt1.Rows[i]["Per_type"].ToString())
                {
                    case "1":  //签发人
                        num++; break;
                    case "2":   //技术负责人,
                        if (Check_RejectRev("1", dt1.Rows[i]["Rev_id"].ToString()))
                        { num++; } break;
                    case "3":   //张总
                        if (Check_RejectRev("2", dt1.Rows[i]["Rev_id"].ToString()))
                        { num++; } break;
                    case "4":   //验证人
                        if (Check_RejectRev("3", dt1.Rows[i]["Rev_id"].ToString()))
                        { num++; } break;
                    case "5":   //复审申请人
                        if (Check_RejectRev("3", dt1.Rows[i]["Rev_id"].ToString()))
                        { num++; } break;
                    case "6":   //复审审核人
                        if (Check_RejectRev("5", dt1.Rows[i]["Rev_id"].ToString()))
                        { num++; } break;
                    case "7":   //邓总
                        if (Check_RejectRev("6", dt1.Rows[i]["Rev_id"].ToString()))
                        { num++; } break;

                }
            }
            if (num > 0)
            {
                lb_rejectPro.Text = "(" + num.ToString() + ")";
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
    }
}
