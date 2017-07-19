using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EasyUI;
using System.Reflection;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_HTBGTZDAJAX : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            String methodName = Request["method"];
            MethodInfo method = this.GetType().GetMethod(methodName);
            if (method == null) throw new Exception("method is null");
            try
            {

                method.Invoke(this, null);
            }
            catch
            {
                throw;
            }
        }

        public void BindData()
        {
            int page = Convert.ToInt32(Request["page"]);
            int rows = Convert.ToInt32(Request["rows"]);
            InitPager(rows, page);
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            string sql = "select count(tzd_id) from CM_HTBGTZD ";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
            int num = 0;
            if (dt1.Rows[0][0].ToString() != "" && dt1.Rows[0][0].ToString() != null)
            {
                num = Convert.ToInt32(dt1.Rows[0][0].ToString());
            }
            string json = JsonHelper.CreateJsonParameters(dt, true, num);
            Response.Write(json);
        }

        private void InitPager(int rows, int page)
        {
            pager.TableName = "CM_HTBGTZD";
            pager.PrimaryKey = "tzd_id";
            pager.ShowFields = "*,'"+Session["UserName"].ToString()+"'as username ";
            pager.OrderField = "tzd_id";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;
            pager.PageSize = rows;
            pager.PageIndex = page;
        }
        private string strWhere()
        {
            string username = Session["UserName"].ToString();
            string depid = Session["UserDeptID"].ToString();
            string tzd_bh = Request["tzd_bh"].ToString();
            string tzd_hth = Request["tzd_hth"].ToString();
            string tzd_xmmc = Request["tzd_xmmc"].ToString();
            string tzd_gkmc = Request["tzd_gkmc"].ToString();
            string spzt = Request["spzt"].ToString();
            string rw = Request["rw"].ToString();
            string sql = " TZD_ID is not null ";
            if (spzt=="1")
            {
                sql += "and TZD_SPZT='0' ";
            }
            else if (spzt=="2")
            {
                sql += "and TZD_SPZT='1' ";
            }
            else if (spzt == "3")
            {
                sql += "and TZD_SPZT='10' ";
            }
            else if (spzt=="4")
            {
                sql += "and TZD_SPZT='11' ";
            }
            if (rw=="1")
            {
                sql += "and (TZD_ZDR='"+username+"' or TZD_SPR1='"+username+"' or TZD_SPR2='"+username+"' or TZD_CSBMID like '%"+depid+"%') ";
            }
            if (tzd_bh!=""&&tzd_bh!=null)
            {
                sql += "and TZD_BH='" + tzd_bh+"' ";
            }
            if (tzd_hth!=""&&tzd_hth!=null)
            {
                sql += "and TZD_HTH='"+tzd_hth+"' ";
            }
            if (tzd_xmmc!=""&&tzd_xmmc!=null)
            {
                sql += "and TZD_HTH='"+tzd_xmmc+"' ";
            }
            if (tzd_gkmc!=""&&tzd_gkmc!=null)
            {
                sql += "and TZD_GKMC='"+tzd_gkmc+"'";
            }
            return sql;
        }
    }
}
