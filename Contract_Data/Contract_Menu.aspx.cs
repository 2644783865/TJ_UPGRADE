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

namespace ZCZJ_DPF.Contract_Data
{
    public partial class Contract_Menu : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            if (!IsPostBack)
            {
                InitUrl();
            }
            GetMyViewTask();
            CRUndo();
            CheckUser(ControlFinder);
        }
        private void InitUrl()
        {
            HyperLink12.NavigateUrl = "CM_ContractView.aspx";//合同评审
            HyperLink13.NavigateUrl = "CM_MyContractReviewTask.aspx";//我的评审任务
            HyperLink1.NavigateUrl = "CM_Contract_SW.aspx";//销售
            HyperLink2.NavigateUrl = "CM_Contract_WW.aspx?ContractForm=1";//委外
            //HyperLink3.NavigateUrl = "CM_Contract_FB.aspx?ContractForm=2";//分包
            HyperLink4.NavigateUrl = "CM_Contract_CG.aspx?ContractForm=3";//采购
            HyperLink5.NavigateUrl = "CM_Contract_BG.aspx?ContractForm=4";//设备
            HyperLink6.NavigateUrl = "CM_Contract_QT.aspx?ContractForm=5";//其他
            HyperLink7.NavigateUrl = "CM_CHECKREQUEST_QUERY.aspx";//请款单
            HyperLink8.NavigateUrl = "CM_CRUndo.aspx";//待办请款
            HyperLink9.NavigateUrl = "CM_CheckRequestRecord.aspx";//请款记录
            HyperLink10.NavigateUrl = "CM_AllBill.aspx";//发票记录            
        }

        //我的合同评审任务
        #region
        private void GetMyViewTask()
        {
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
            //        bool check=true;
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

        //待办款项
        private void CRUndo()
        {
            int num_QK = 0;
            int num_YK = 0;
            string sqlstr1="";
            sqlstr1 = "select * from View_CM_DBQK";
            
            DataTable dt_undocr = DBCallCommon.GetDTUsingSqlText(sqlstr1);
            if (dt_undocr.Rows.Count > 0)
            {
                num_QK += dt_undocr.Rows.Count;
            }
            string sqlstr2 = "";
            sqlstr2 = "select a.*,b.* from TBPM_BUSPAYMENTRECORD as a inner join TBPM_CONPCHSINFO as b on a.BP_HTBH=b.PCON_BCODE  where a.BP_STATE='0'";
            DataTable dt_undobus = DBCallCommon.GetDTUsingSqlText(sqlstr2);
            if (dt_undobus.Rows.Count > 0)
            {
                num_YK += dt_undobus.Rows.Count;
            }
            if (num_QK==0 && num_YK == 0)
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
    }
}
