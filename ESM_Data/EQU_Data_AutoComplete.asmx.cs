using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace ZCZJ_DPF.ESM_Data
{
    /// <summary>
    /// EQU_Data_AutoComplete 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class EQU_Data_AutoComplete : System.Web.Services.WebService
    {
        private string[] autoCompleteWordList = null;
        private string[] autoCompleteall = null;
        [WebMethod]
        public String[] GetNAME(string prefixText, int count)
        {
            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;

            if (autoCompleteWordList == null)
            {
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                sqlConn.Open();
                DataSet ds = new DataSet();
                DataTable glotb = new DataTable();
                ds = DBCallCommon.FillDataSet("select isnull(Name,'')+'|'+isnull(Specification,'') as Expr1 from EQU_tzsb where Name like'%" + prefixText + "%' union select isnull(AName,'')+'|'+isnull(Spec,'') as Expr1 from ESM_EQU where AName like'%" + prefixText + "%'");
                glotb.Merge(ds.Tables[0]);
                glotb.AcceptChanges();
                string[] temp = new string[glotb.Rows.Count];
                int i = 0;
                foreach (DataRow dr in glotb.Rows)
                {
                    temp[i] = dr["Expr1"].ToString();
                    i++;

                }
                Array.Sort(temp, new CaseInsensitiveComparer());
                autoCompleteall = temp;
                if (sqlConn.State == ConnectionState.Open)
                    sqlConn.Close();

            }
            int matchCount = autoCompleteall.Length >= count ? count : autoCompleteall.Length;
            string[] matchResultList = new string[matchCount];
            if (matchCount > 0)
            {
                Array.Copy(autoCompleteall, 0, matchResultList, 0, matchCount);
            }
            return matchResultList;
        }


        [WebMethod]

        //查询已生成合同的合同号
        public String[] GetHTBH(string prefixText, int count)
        {
            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;//检测参数是否为空
            if (autoCompleteWordList == null)
            {
                string sql = "select distinct HT_HTBH from EQU_GXHT where HT_HTBH like '%" + prefixText + "%' and HT_HTBH is not null";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                //读取内容文件的数据到临时数组
                string[] temp = new string[dt.Rows.Count];
                for (int i = 0, length = dt.Rows.Count; i < length; i++)
                {
                    temp[i] = dt.Rows[i]["HT_HTBH"].ToString();
                }

                Array.Sort(temp, new CaseInsensitiveComparer());
                //将临时数组的内容赋给返回数组
                autoCompleteall = temp;
            }
            int matchCount = autoCompleteall.Length >= count ? count : autoCompleteall.Length;
            string[] matchResultList = new string[matchCount];
            if (matchCount > 0)
            {   //复制搜索结果
                Array.Copy(autoCompleteall, 0, matchResultList, 0, matchCount);
            }
            return matchResultList;
        }
        [WebMethod]

        //查询供应商
        public String[] GetGYS(string prefixText, int count)
        {
            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;//检测参数是否为空
            if (autoCompleteWordList == null)
            {
                string sql = "select distinct HT_GF from EQU_GXHT where HT_GF like '%" + prefixText + "%' and HT_GF is not null";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                //读取内容文件的数据到临时数组
                string[] temp = new string[dt.Rows.Count];
                for (int i = 0, length = dt.Rows.Count; i < length; i++)
                {
                    temp[i] = dt.Rows[i]["HT_GF"].ToString();
                }

                Array.Sort(temp, new CaseInsensitiveComparer());
                //将临时数组的内容赋给返回数组
                autoCompleteall = temp;
            }
            int matchCount = autoCompleteall.Length >= count ? count : autoCompleteall.Length;
            string[] matchResultList = new string[matchCount];
            if (matchCount > 0)
            {   //复制搜索结果
                Array.Copy(autoCompleteall, 0, matchResultList, 0, matchCount);
            }
            return matchResultList;
        }
    }
}
