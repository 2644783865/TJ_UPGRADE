using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace ZCZJ_DPF.CM_Data
{
    /// <summary>
    /// CM_Applica 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class CM_Applica : System.Web.Services.WebService
    {
        private string[] autoCompleteWordList = null;
        private string[] autoCompleteall = null;
        [WebMethod]
        public string[] Getdata(string prefixText, int count, string contextKey)
        {
            string[] codes = getSuggestionList(prefixText, contextKey);
            return codes.Where(p => p.IndexOf(prefixText) >= 0).Take(count).ToArray();
        }

        private string[] getSuggestionList(string prefixText, string contextKey)
        {
            List<string> ID = new List<string>();
            string sql_text = string.Format("select {0} from TBCM_APPLICA", contextKey);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_text);
            foreach (DataRow row in dt.Rows)
            {
                ID.Add(row[0].ToString().Trim());
            }
            ID = ID.Distinct<string>().ToList();//去重复
            ID.Remove("");//去掉空值           
            return ID.ToArray();
        }


        [WebMethod]
        public String[] GetCompleteProvider(string prefixText, int count)
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
                DataTable dt = new DataTable();
                string sqltext = "";
                //2为供应商
                sqltext = "SELECT CS_CODE + ' | ' + CS_NAME + ' | ' + CS_HRCODE  AS Expr1 " +
                          "FROM TBCS_CUSUPINFO WHERE (CS_TYPE='1' or CS_TYPE='2') and  " +
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
