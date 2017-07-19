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

namespace ZCZJ_DPF.ESM
{
    public partial class EQU_Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            //Get_CGSP();
            Get_WXSP();
            Get_CSP();
            Get_HTSP();

        }
        //设备维修审批
        private void Get_WXSP()
        {
            //先找出审核人列表中包含当前登录人的单号，再根据审批状态进行筛选
            string userid = Session["UserID"].ToString();

            int num = 0;//待审批的单号数量，即包含此人且还没有填写意见，意见为0
            string sqlselect_code = "select DocuNum,Fir_Per,Fir_Jg,Sec_Per,Sec_Jg,Thi_Per,Thi_Jg from" +
                   " EQU_Repair_Audit where (Fir_Per='" + userid + "' and Fir_Jg='0')" +
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
        //设备采购审核任务
        //private void Get_CGSP()
        //{
        //    //先找出审核人列表中包含当前登录人的单号，再根据审批状态进行筛选
        //    string userid = Session["UserID"].ToString();

        //    int num = 0;//待审批的单号数量，即包含此人且还没有填写意见，意见为0
        //    string sqlselect_code = "select DocuNum,Fir_Per,Fir_Jg,Sec_Per,Sec_Jg,Thi_Per,Thi_Jg from" +
        //           " EQU_Need_Audit where (Fir_Per='" + userid + "' and Fir_Jg='0')" +
        //           " or (Sec_Per='" + userid + "' and Sec_Jg='0') or" +
        //           " (Thi_Per='" + userid + "' and  Thi_Jg='0')";
        //    DataTable dt_select_code = DBCallCommon.GetDTUsingSqlText(sqlselect_code);
        //    if (dt_select_code.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr_code in dt_select_code.Rows)
        //        {
        //            if (userid == dr_code["Fir_Per"].ToString())
        //            {
        //                num++;
        //            }
        //            else if (userid == dr_code["Sec_Per"].ToString()) //第二级审核看一级审核是否同意
        //            {
        //                if (dr_code["Fir_Jg"].ToString() == "1")
        //                {
        //                    num++;
        //                }
        //            }
        //            else if (userid == dr_code["Thi_Per"].ToString()) //第三级审核看二级审核是否同意
        //            {
        //                if (dr_code["Sec_Jg"].ToString() == "1")
        //                {
        //                    num++;
        //                }
        //            }
        //        }
        //        if (num > 0)
        //        {
        //            lbl_cgsp.Text = "(" + num.ToString() + ")";
        //        }
        //    }

        //}


        //采购审核任务
        private void Get_CSP()
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
                    lbl_csp.Text = "(" + num.ToString() + ")";
                }
            }

        }

        //设备合同审批任务
        private void Get_HTSP()
        {
            string userid = Session["UserID"].ToString();
            string sql = string.Format("select count (HT_ID) from EQU_GXHT where (HT_SHR1ID='{0}' and (HT_SHR1_JL is null or HT_SHR1_JL='') and HT_SPZT='1') or (HT_SHR2ID='{0}' and (HT_SHR2_JL='' or HT_SHR2_JL is null) and HT_SPZT='1y') or (HT_SHR3ID='{0}' and (HT_SHR3_JL='' or HT_SHR3_JL is null) and HT_SPZT='1y')or (HT_SHR2ID='{0}' and (HT_SHR2_JL='' or HT_SHR2_JL is null) and HT_SPZT='2.2y')or (HT_SHR3ID='{0}' and (HT_SHR3_JL='' or HT_SHR3_JL is null) and HT_SPZT='2.1y') or (HT_SHR4ID='{0}' and (HT_SHR4_JL is null or HT_SHR4_JL='') and HT_SPZT='2y') or (HT_SHR5ID='{0}' and (HT_SHR5_JL is null or HT_SHR5_JL='') and HT_SPZT='3y')", userid);

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

            if (Convert.ToInt32(dt.Rows[0][0].ToString()) > 0)
                lbl_ssp.Text = "(" + dt.Rows[0][0].ToString() + ")";

        }

    }
}
