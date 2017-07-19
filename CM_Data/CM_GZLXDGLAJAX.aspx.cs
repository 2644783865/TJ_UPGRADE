using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Data;
using EasyUI;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_GZLXDGLAJAX : System.Web.UI.Page
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
            if (Request["id"]==null)
            {
                int page = Convert.ToInt32(Request["page"]);
                int rows = Convert.ToInt32(Request["rows"]);
                InitPager(rows, page);
                DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
                string sql = "select count(A.LXD_ID) from CM_GZLXD as A left join CM_GZLXD_NR as B on A.LXD_ID=B.NR_FATHERID";
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                int num = 0;
                if (dt1.Rows[0][0].ToString() != "" && dt1.Rows[0][0].ToString() != null)
                {
                    num = Convert.ToInt32(dt1.Rows[0][0].ToString());
                }
                string json = JsonHelper.CreateJsonParameters(dt, true, num);
                Response.Write(json);
            }
            else
            {
                string sql = "select a.*,b.* from CM_GZLXD as a left join CM_GZLXD_NR as b on a.LXD_ID=b.NR_FATHERID where a.LXD_ID="+Request["id"].ToString();
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                string json = JsonHelper.CreateJsonOne(dt, true);
                Response.Write(json);
            }
           
        }


        private void InitPager(int rows, int page)
        {
            pager.TableName = "(select a.*,stuff((select '|' + NR_FATHERID from CM_GZLXD_NR as b where (b.NR_FATHERID=a.LXD_ID)FOR xml path('')),1,1,'')as NR_FATHERID ,stuff((select '|'+ NR_SBMC from CM_GZLXD_NR as b where (b.NR_FATHERID=a.LXD_ID)FOR xml path('')),1,1,'')as NR_SBMC,stuff((select '|'+ NR_TH from CM_GZLXD_NR as b where (b.NR_FATHERID=a.LXD_ID)FOR xml path('')),1,1,'')as NR_TH,stuff((select '|'+ NR_SL from CM_GZLXD_NR as b where (b.NR_FATHERID=a.LXD_ID)FOR xml path('')),1,1,'')as NR_SL,stuff((select '|'+ NR_BZ from CM_GZLXD_NR as b where (b.NR_FATHERID=a.LXD_ID)FOR xml path('')),1,1,'')as NR_BZ, 'closed' as state,'"+Session["UserName"].ToString()+"' as username,'"+Session["UserDeptID"].ToString()+"'as depid from CM_GZLXD as a )t";
            pager.PrimaryKey = "LXD_ID";
            pager.ShowFields = "*";
            pager.OrderField = "LXD_ID";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;
            pager.PageSize = rows;
            pager.PageIndex = page;
        }

        private string strWhere()
        {
            string username = Session["UserName"].ToString();
            string spzt = Request["spzt"].ToString();
            string rw = Request["rw"].ToString();
            string lxd_bh = Request["lxd_bh"].ToString();
            string lxd_hth = Request["lxd_hth"].ToString();
            string lxd_xmmc = Request["lxd_xmmc"].ToString();
            string lxd_dhdw = Request["lxd_dhdw"].ToString();
            string sql = "LXD_ID is not null ";
            if (spzt=="1")
            {
                sql += " and LXD_ZPZT='0'";
            }
            else if (spzt=="2")
            {
                sql += " and LXD_SPZT='1'";
            }
            else if (spzt=="3")
            {
                sql += " and LXD_SPZT='10'";
            }
            else if (spzt=="4")
            {
                sql += " and LXD_SPZT='11'";
            }
            if (rw=="1")
            {
                sql += " and (LXD_SPR1 like '%"+username+"%' or LXD_SPR2 like '%"+username+"%' or LXD_ZDR like '%"+username+"%')";
            }
            if (lxd_bh!=null&lxd_bh!="")
            {
                sql += " and LXD_BH like '%"+lxd_bh+"%'";
            }
            if (lxd_hth!=null&lxd_hth!="")
            {
                sql += " and LXD_HTH like '%"+lxd_hth+"%'";
            }
            if (lxd_xmmc!=null&lxd_xmmc!="")
            {
                sql += " and LXD_XMMC like '%"+lxd_xmmc+"%'";
            }
            if (lxd_dhdw!=null&lxd_dhdw!="")
            {
                sql += " and LXD_DHDW like '%"+lxd_dhdw+"%'";
            }      
            return sql;
        }
    }
}
