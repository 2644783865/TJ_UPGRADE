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

namespace ZCZJ_DPF.Basic_Data
{
    public partial class BD_Menu : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            if (Session.IsNewSession)
            {
                Session.Abandon();
                Application.Lock();
                Application["online"] = (int)Application["online"] - 1;
                Application.UnLock();
                Response.Write("<script>if(window.parent!=null)window.parent.location.href='../Default.aspx';else{window.location.href='./Default.aspx'} </script>");
                
            }
            InitUrl();
            GetCusup_Review();
            CheckUser(ControlFinder);
        }

        private void InitUrl()
        {
            HyperLink2.NavigateUrl = "tbds_depinfo_detail.aspx";
            HyperLink3.NavigateUrl = "WL_Material_List.aspx";
            HyperLink4.NavigateUrl = "QX_Role_List.aspx";
            HyperLink5.NavigateUrl = "PassWordUpdate.aspx";
            HyperLink6.NavigateUrl = "WL_Type_List.aspx";
            HyperLink7.NavigateUrl = "tbcs_cusupinfo_detail.aspx";
            HyperLink8.NavigateUrl = "tbds_locinfo_List.aspx";
            HyperLink1.NavigateUrl = "Office_Material_List.aspx";
            HyperLink11.NavigateUrl = "QX_Power_List.aspx";
            HyperLink13.NavigateUrl = "Mar_zhaobiao_manage.aspx";
            HyperLink14.NavigateUrl = "tbcs_cusupinfo_Review.aspx";
            HyperLink10.NavigateUrl = "PassWordUpdateRecord.aspx";
        }

        //厂商添加删除-审批
        private void GetCusup_Review()
        {
            int Review_num = 0;
            
            string str_sql = "select * from TBCS_CUSUP_ADD_DELETE where ( CS_SPJG in ('0','1') and id in(" +
                        " select distinct a.fatherid from TBCS_CUSUP_ReView a , TBCS_CUSUP_ReView b " +
                                 " where (a.CSR_TYPE!='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "' and a.fatherid=b.fatherid and b.CSR_YJ!='0' and a.CSR_TYPE!='5'" +
                                 "  and cast(a.CSR_TYPE as int)-1=cast(b.CSR_TYPE as int) ) or " +
                                 " (a.CSR_TYPE='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "')" +
                                 ")) or ( CS_SPJG in ('0','1') and id in (" +
                                  " select distinct f.fatherid from TBCS_CUSUP_ReView f  " +
                                  " where (f.CSR_TYPE!='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid in ("+
                                  "select h.fatherid from TBCS_CUSUP_ReView h where h.CSR_TYPE='5' )) or" +
                                 " (f.CSR_TYPE='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid not in (" +
                                " (select d.fatherid from TBCS_CUSUP_ReView d " +
                                "  where d.CSR_TYPE!='5' and d.csr_yj='0' and d.fatherid  in( select e.fatherid from  " +
                                 " TBCS_CUSUP_ReView e where e.CSR_TYPE='5' and e.CSR_YJ='0' and e.CSR_PERSON='" + Session["UserID"].ToString() + "'" +
                                 "))))))";
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
    }
}
