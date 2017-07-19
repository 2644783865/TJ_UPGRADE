using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Collections;

namespace ZCZJ_DPF.QC_Data
{
    /// <summary>
    /// QC_Data 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
     [System.Web.Script.Services.ScriptService]
    public class QC_Data : System.Web.Services.WebService
    {
        private string[] autoCompleteWordList = null;
        private string[] autoCompleteall = null;

        /// <summary>
        /// 供应商
        /// </summary>
        /// <param name="prefixText"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [WebMethod]
        public String[] GetSUPPLERNM(string prefixText, int count)
        {
            ///检测参数是否为空

            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;

            // 如果数组为空
            if (autoCompleteWordList == null)
            {
                //读取数据库的内容
                SqlConnection sqlConn = new SqlConnection();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                sqlConn.Open();
                System.Data.DataTable dt = new System.Data.DataTable();
                string sqltext = "";
                //2为供应商
                sqltext = "SELECT  CS_NAME  AS Expr1 " +
                          "FROM TBCS_CUSUPINFO WHERE " +
                          "(CS_HRCODE LIKE '%" + prefixText + "%' or CS_NAME like '%" + prefixText + "%') AND CS_State='0' ORDER BY CS_CODE";
                dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                //读取内容文件的数据到临时数组
                string[] temp = new string[dt.Rows.Count];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    temp[i] = dr["Expr1"].ToString();
                    i++;
                }
                Array.Sort(temp, new CaseInsensitiveComparer());
                //将临时数组的内容赋给返回数组
                autoCompleteall = temp;
                if (sqlConn.State == ConnectionState.Open)
                    sqlConn.Close();
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
