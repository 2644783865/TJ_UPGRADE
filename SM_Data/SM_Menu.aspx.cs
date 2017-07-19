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

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Menu : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            if (!IsPostBack)
            {
                InitUrl();
            }

            CheckUser(ControlFinder);
            getxyjhsh();
            GetMyTask();
            Get_CGSP();
            getdydsh();
            getBGdsh();
            getsafe();
            GetProjTemp(); //项目结转备库
        }
        private void InitUrl()
        {
            HyperLink1.NavigateUrl = "SM_Warehouse_Query.aspx?FLAG=QUERY";
            HyperLink2.NavigateUrl = "SM_WarehouseIN_Index.aspx";
            HyperLink3.NavigateUrl = "SM_WarehouseOut_Index.aspx";
            HyperLink4.NavigateUrl = "SM_Warehouse_AllocationManage.aspx";
            HyperLink5.NavigateUrl = "SM_Warehouse_MTOAdjustManage.aspx";
            HyperLink6.NavigateUrl = "SM_Warehouse_MTONotes.aspx";
            HyperLink7.NavigateUrl = "SM_Warehouse_InventoryManage.aspx";
            HyperLink8.NavigateUrl = "SM_Warehouse_MaterialFlow.aspx";
            HyperLink9.NavigateUrl = "SM_Warehouse_Manage.aspx";
            HyperLink10.NavigateUrl = "SM_Trans_Management/SM_Trans_Manage_Index.aspx";            
            HyperLink12.NavigateUrl = "~/PC_Data/PC_TBPC_Purchaseplan_check_list.aspx?action=ws";
            HyperLink13.NavigateUrl = "~/PC_Data/PC_TBPC_Marreplace_list.aspx";
            HyperLink14.NavigateUrl = "~/PC_Data/PC_TBPC_Otherpur_Bill_List.aspx";
            HyperLink15.NavigateUrl = "~/PC_Data/PC_TBPC_Otherpur_Bill_Audit.aspx";
            HyperLink16.NavigateUrl = "SM_PURCHASEPLAN_VIEW.aspx";           
            //HyperLink18.NavigateUrl = "SM_Tech_MTO.aspx";
            HyperLink19.NavigateUrl = "SM_Warehouse_Warning.aspx";
            HyperLink20.NavigateUrl = "SM_Warehouse_add_delete.aspx";
            HyperLink21.NavigateUrl = "SM_Warehouse_ProjOver.aspx";
            HyperLink22.NavigateUrl = "SM_Warehouse_ProjTempManage.aspx?";
            HyperLink23.NavigateUrl = "SM_YULIAO_LIST.aspx";
            HyperLink24.NavigateUrl = "SM_YULIAO_IN.aspx";
            HyperLink25.NavigateUrl = "SM_YULIAO_OUT.aspx";
            HyperLink26.NavigateUrl = "~/PC_Data/PC_TBPC_Purchaseplan_start.aspx";
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

        /*********************得到我的任务的数量*************************/
        private void GetMyTask()
        {
            string sqlText = "select count(distinct(PR_PCODE)) from View_TBPC_MARSTOUSE_TOTAL_ALL where allshstate='0' and PR_REVIEWB='" + Session["UserID"].ToString() + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (dr[0].ToString() == "0")
                {
                    lb_MyTask.Visible = false;
                }
                else
                {
                    lb_MyTask.Visible = true;
                    lb_MyTask.Text = "(" + dr[0].ToString() + ")";
                }
            }
            dr.Close();
        }
        private void getsafe()
        {
            string sqlText = "select count(*) from View_STORAGE_WARNING where WARNNUM>isnull(storagenum,0)";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
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
        private void getBGdsh()
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

       /*--------------- 结转备库我的任务 ------------------ */
        private void GetProjTemp()
        {
            string userid = Session["Userid"].ToString();           

            string sqltext2 = "select count(*) from tbws_projtemp where PT_STATE='0' and PT_DOC='" + userid + "'";
            SqlDataReader dr2 = DBCallCommon.GetDRUsingSqlText(sqltext2);
            string strnum2="";
            if (dr2.Read())
            {
                 strnum2 = dr2[0].ToString();

            }
            dr2.Close();
           
            if (strnum2 != "0")
            {
                LabelProjTemp.Text = "(" +strnum2 + ")";
            }
            else { LabelProjTemp.Visible = false; }        

        }



    }
}
