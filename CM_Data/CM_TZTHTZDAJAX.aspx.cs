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
    public partial class CM_TZTHTZDAJAX : System.Web.UI.Page
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
            if (Request["id"] == null)
            {
                int page = Convert.ToInt32(Request["page"]);
                int rows = Convert.ToInt32(Request["rows"]);
                InitPager(rows, page);
                DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
                DataTable dt2 = new DataTable();
                for (int i = 0, length = dt.Columns.Count; i < length; i++)
                {
                    dt2.Columns.Add(dt.Columns[i].ColumnName);
                }
                for (int j = 0, length1 = dt.Rows.Count; j < length1; j++)
                {
                    DataRow dr = dt2.NewRow();
                    for (int i = 0, length = dt.Columns.Count; i < length; i++)
                    {
                        dr[dt.Columns[i].ColumnName] = dt.Rows[j][i].ToString().Replace(" ", "").Trim().Replace("\r\n", "").Replace("\n", "").Replace("</br>", "").Replace("<br/>", "");
                    }
                    dt2.Rows.Add(dr);
                }
                string sql = "select count(A.TZD_ID) from CM_TZTHTZD as A left join CM_TZTHTZD_NR as B on A.TZD_SJID=B.NR_FATHERID";
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                int num = 0;
                if (dt1.Rows[0][0].ToString() != "" && dt1.Rows[0][0].ToString() != null)
                {
                    num = Convert.ToInt32(dt1.Rows[0][0].ToString());
                }
                string json = JsonHelper.CreateJsonParameters(dt2, true, num);
                Response.Write(json);
            }
            else
            {
                string sql = "select a.*,b.* from CM_TZTHTZD as a left join CM_TZTHTZD_NR as b on a.TZD_SJID=b.NR_FATHERID where a.TZD_ID=" + Request["id"].ToString();
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                DataTable dt2 = new DataTable();
                for (int i = 0, length = dt.Columns.Count; i < length; i++)
                {
                    dt2.Columns.Add(dt.Columns[i].ColumnName);
                }
                for (int j = 0, length1 = dt.Rows.Count; j < length1; j++)
                {
                    DataRow dr = dt2.NewRow();
                    for (int i = 0, length = dt.Columns.Count; i < length; i++)
                    {
                        dr[dt.Columns[i].ColumnName] = dt.Rows[j][i].ToString().Replace(" ", "").Trim().Replace("\r\n", "").Replace("\n", "").Replace("</br>", "").Replace("<br/>", "");
                    }
                    dt2.Rows.Add(dr);
                }
                string json = JsonHelper.CreateJsonOne(dt2, true);
                Response.Write(json);
            }
        }

        private void InitPager(int rows, int page)
        {
            //pager.TableName = "(select a.*,stuff((select '|'+ replace(NR_TH,' ','') from CM_TZTHTZD_NR as b where (b.NR_FATHERID=a.TZD_SJID)FOR xml path('')),1,1,'')as NR_TH,stuff((select '|'+ replace(NR_TM,' ','') from CM_TZTHTZD_NR as b where (b.NR_FATHERID=a.TZD_SJID)FOR xml path('')),1,1,'')as NR_TM,stuff((select '|'+ replace(NR_BZ,' ','') from CM_TZTHTZD_NR as b where (b.NR_FATHERID=a.TZD_SJID)FOR xml path('')),1,1,'')as NR_BZ,'closed' as state ,'李玲' as username,'07'as depid from CM_TZTHTZD as a )t";
            pager.TableName = "(select a.*,stuff((select '|'+ replace(NR_TH,' ','') from CM_TZTHTZD_NR as b where (b.NR_FATHERID=a.TZD_SJID)FOR xml path('')),1,1,'')as NR_TH,stuff((select '|'+ replace(NR_TM,' ','') from CM_TZTHTZD_NR as b where (b.NR_FATHERID=a.TZD_SJID)FOR xml path('')),1,1,'')as NR_TM,stuff((select '|'+ replace(NR_BZ,' ','') from CM_TZTHTZD_NR as b where (b.NR_FATHERID=a.TZD_SJID)FOR xml path('')),1,1,'')as NR_BZ,'closed' as state ,'" + Session["UserName"].ToString() + "' as username,'07'as depid from CM_TZTHTZD as a )t";
            pager.PrimaryKey = "TZD_ID";
            pager.ShowFields = "*";
            pager.OrderField = "TZD_ID";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;
            pager.PageSize = rows;
            pager.PageIndex = page;
        }

        private string strWhere()
        {
            string username = Session["UserName"].ToString();
            string tzd_bh = Request["tzd_bh"].ToString();
            string spzt = Request["spzt"].ToString();
            string rw = Request["rw"].ToString();
            string sql = "TZD_ID is not null";
            if (spzt=="1")
            {
                sql += " and TZD_SPZT='0' ";
            }
            else if (spzt=="2")
            {
                sql += " and TZD_SPZT='1'";
            }
            else if (spzt=="3")
            {
                sql += " and TZD_SPZT='10'";
            }
            else if (spzt=="4")
            {
                sql += " and TZD_SPZT='11'";
            }
            if (rw=="1")
            {
                sql += " and (TZD_ZDR='" + username + "' or TZD_SPR1='" + username + "' or TZD_SPR2='"+username+"')";
            }
            if (tzd_bh!=""&&tzd_bh!=null)
            {
                sql += " and TZD_BH='"+tzd_bh+"'";
            }
            return sql;
        }

    }
}
