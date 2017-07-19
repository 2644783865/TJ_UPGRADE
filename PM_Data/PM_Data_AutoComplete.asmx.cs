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
using System.Collections.Generic;

namespace ZCZJ_DPF.PM_Data
{
    /// <summary>
    /// PM_Data_AutoComplete 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class PM_Data_AutoComplete : System.Web.Services.WebService
    {
        private string[] autoCompleteWordList = null;
        private string[] autoCompleteall = null;
        [WebMethod]
        public string[] GetCompletebytsaid(string prefixText, int count)
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
                DataSet ds = new DataSet();
                DataTable glotb = new DataTable();
                ds = DBCallCommon.FillDataSet("SELECT distinct TSA_ID  as Expr1 from  View_CM_FaHuo  WHERE TSA_ID LIKE '%" + prefixText.Trim() + "%'");
                glotb.Merge(ds.Tables[0]);
                glotb.AcceptChanges();
                //读取内容文件的数据到临时数组
                string[] temp = new string[glotb.Rows.Count];
                int i = 0;
                foreach (DataRow dr in glotb.Rows)
                {
                    temp[i] = dr["Expr1"].ToString();
                    i++;
                }
                Array.Sort(temp, new CaseInsensitiveComparer());
                autoCompleteall = temp;
                //将临时数组的内容赋给返回数组
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
                          "FROM TBCS_CUSUPINFO WHERE (CS_TYPE='2' or CS_TYPE='8') and  " +
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

        [WebMethod]
        public String[] GetCusupinfo(string prefixText, int count)
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
                sqltext = "SELECT CS_NAME + ' | ' + CS_CODE + ' | ' + CS_HRCODE  AS Expr1 " +
                          "FROM TBCS_CUSUPINFO WHERE (CS_TYPE='2' OR CS_TYPE='3') and  (" +
                          "CS_HRCODE LIKE '%" + prefixText + "%') ORDER BY CS_CODE";
                //"CS_HRCODE LIKE '%" + prefixText + "%') AND CS_State='0' ORDER BY CS_CODE";
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


        [WebMethod]
        public String[] GetCompletemar(string prefixText, int count)
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
                DataTable glotb = new DataTable();
                dt = DBCallCommon.GetDTUsingSqlText("SELECT ISNULL(ID,'') + '|' + ISNULL(MNAME,'')  + '|' + ISNULL(GUIGE,'')+ '|' +ISNULL(CAIZHI,'')+ '|' +ISNULL(GB,'') as Expr1 FROM TBMA_MATERIAL WHERE (ID LIKE '%" + prefixText + "%' OR MNAME LIKE '%" + prefixText + "%' OR HMCODE LIKE '%" + prefixText + "%') AND STATE='1' ORDER BY ID");
                glotb = dt;
                //读取内容文件的数据到临时数组
                string[] temp = new string[glotb.Rows.Count];
                int i = 0;
                foreach (DataRow dr in glotb.Rows)
                {
                    temp[i] = dr["Expr1"].ToString();
                    i++;
                }
                Array.Sort(temp, new CaseInsensitiveComparer());
                autoCompleteall = temp;
                //将临时数组的内容赋给返回数组
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
        [WebMethod]
        public String[] GetCompletemarbyhc(string prefixText, int count)
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
                DataTable glotb = new DataTable();
                glotb = DBCallCommon.GetDTUsingSqlText("SELECT ISNULL(ID,'') + '|' + ISNULL(MNAME,'')  + '|' + ISNULL(GUIGE,'')+ '|' +ISNULL(CAIZHI,'')+ '|' +ISNULL(GB,'')+ '|' +ISNULL(PURCUNIT,'')+ '|' +ISNULL(FUZHUUNIT,'') as Expr1 FROM TBMA_MATERIAL WHERE (HMCODE LIKE '%" + prefixText.Trim() + "%' or ID like '%" + prefixText.Trim() + "%') and (STATE='1') ORDER BY ID");
                glotb.AcceptChanges();
                //读取内容文件的数据到临时数组
                string[] temp = new string[glotb.Rows.Count];
                int i = 0;
                foreach (DataRow dr in glotb.Rows)
                {
                    temp[i] = dr["Expr1"].ToString();
                    i++;
                }
                Array.Sort(temp, new CaseInsensitiveComparer());
                autoCompleteall = temp;
                //将临时数组的内容赋给返回数组
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
        [WebMethod]
        public String[] GetCompletemarbyco(string prefixText, int count)
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
                DataTable glotb = new DataTable();
                glotb = DBCallCommon.GetDTUsingSqlText("SELECT ISNULL(ID,'') + '|' + ISNULL(MNAME,'')  + '|' + ISNULL(GUIGE,'')+ '|' +ISNULL(CAIZHI,'')+ '|' +ISNULL(GB,'') as Expr1 FROM TBMA_MATERIAL WHERE ID LIKE '%" + prefixText + "%' ORDER BY ID");
                glotb.AcceptChanges();
                //读取内容文件的数据到临时数组
                string[] temp = new string[glotb.Rows.Count];
                int i = 0;
                foreach (DataRow dr in glotb.Rows)
                {
                    temp[i] = dr["Expr1"].ToString();
                    i++;
                }
                Array.Sort(temp, new CaseInsensitiveComparer());
                autoCompleteall = temp;
                //将临时数组的内容赋给返回数组
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
        [WebMethod]
        public string[] GetXszyData(string prefixText, int count, string contextKey)
        {
            ///检测参数是否为空
            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;
            //读取数据库的内容
            DataTable glotb = new DataTable();
            //string sqltext = "select isnull(marid,'')+'|'+isnull(marnm,'')+'|'+isnull(margg,'')+'|'+isnull(marcz,'')+'|'+isnull(margb,'')  " +
            //                 "from View_TBPC_STORAGE where marid like '%" + contextKey.Split('_')[0].ToString() + "%' " +
            //                 "and marnm like '%" + contextKey.Split('_')[1].ToString() + "%' and "+
            //                 "margg like '%" + contextKey.Split('_')[2].ToString() + "%' and "+
            //                 "marcz like '%" + contextKey.Split('_')[3].ToString() + "%' and "+
            //                 "margb like '%" + contextKey.Split('_')[4].ToString() + "%' and ptcode='备库'";
            string sqltext = "select isnull(marid,'')+'|'+isnull(marnm,'')+'|'+isnull(margg,'')+'|'+isnull(marcz,'')+'|'+isnull(margb,'')  " +
                             "from View_TBPC_STORAGE where  ptcode='备库'";
            glotb = DBCallCommon.GetDTUsingSqlText(sqltext);
            List<string> Xszydata = new List<string>();
            foreach (DataRow dr in glotb.Rows)
            {
                Xszydata.Add(dr[0].ToString());
            }
            if (Xszydata.Count > 0)
            {
                int rpcount = Xszydata.Count >= count ? count : Xszydata.Count;
                return Xszydata.Where(p => p.IndexOf(prefixText) >= 0).Take(rpcount).ToArray();
            }
            else
            {
                return null;
            }
        }


        [WebMethod]
        public String[] GetCompletemarbyco1(string prefixText, int count)
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
                DataTable glotb = new DataTable();
                glotb = DBCallCommon.GetDTUsingSqlText("SELECT ISNULL(SQ_MARID,'') + '|' + ISNULL(MNAME,'')  + '|' + ISNULL(GUIGE,'')+ '|' +ISNULL(CAIZHI,'')+ '|' +ISNULL(GB,'') as Expr1 FROM View_STORAGE_MATERIAL WHERE (HMCODE LIKE '%" + prefixText + "%' or SQ_MARID like '%" + prefixText + "%') and SQ_PTC='备库' ORDER BY SQ_MARID");
                glotb.AcceptChanges();
                //读取内容文件的数据到临时数组
                string[] temp = new string[glotb.Rows.Count];
                int i = 0;
                foreach (DataRow dr in glotb.Rows)
                {
                    temp[i] = dr["Expr1"].ToString();
                    i++;
                }
                Array.Sort(temp, new CaseInsensitiveComparer());
                autoCompleteall = temp;
                //将临时数组的内容赋给返回数组
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

        [WebMethod]
        public String[] GetPJNAME(string prefixText, int count)
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
                DataTable glotb = new DataTable();
                glotb = DBCallCommon.GetDTUsingSqlText("SELECT ISNULL(PJ_NAME,'') + '|' + ISNULL(PJ_ID,'')  as Expr1 FROM TBPM_PJINFO WHERE PJ_NAME LIKE '%" + prefixText + "%' or PJ_ID like '%" + prefixText + "%'");
                glotb.AcceptChanges();
                //读取内容文件的数据到临时数组
                string[] temp = new string[glotb.Rows.Count];
                int i = 0;
                foreach (DataRow dr in glotb.Rows)
                {
                    temp[i] = dr["Expr1"].ToString();
                    i++;
                }
                Array.Sort(temp, new CaseInsensitiveComparer());
                autoCompleteall = temp;
                //将临时数组的内容赋给返回数组
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

        [WebMethod]
        public String[] GetTask(string prefixText, int count)
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
                DataTable glotb = new DataTable();
                glotb = DBCallCommon.GetDTUsingSqlText("SELECT distinct  ISNULL(b.CM_CONTR,'') + '|' + ISNULL(a.TSA_ID,'')+ '|' + ISNULL(b.CM_PROJ,'')  as Expr1 FROM TBCM_Basic a, TBCM_Plan b WHERE a.ID=b.ID and a.TSA_ID LIKE '%" + prefixText + "%'");
                glotb.AcceptChanges();
                //读取内容文件的数据到临时数组
                string[] temp = new string[glotb.Rows.Count];
                int i = 0;
                foreach (DataRow dr in glotb.Rows)
                {
                    temp[i] = dr["Expr1"].ToString();
                    i++;
                }
                Array.Sort(temp, new CaseInsensitiveComparer());
                autoCompleteall = temp;
                //将临时数组的内容赋给返回数组
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


        [WebMethod]
        public String[] GetENGNAME(string prefixText, int count)
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
                DataTable glotb = new DataTable();
                glotb = DBCallCommon.GetDTUsingSqlText("SELECT ISNULL(TSA_ENGNAME,'') + '|' + ISNULL(TSA_ID,'')  as Expr1 FROM TBPM_TCTSASSGN WHERE (TSA_ENGNAME LIKE '%" + prefixText + "%' or TSA_ID like '%" + prefixText + "%') and TSA_ID not like '%-%'");
                glotb.AcceptChanges();
                //读取内容文件的数据到临时数组
                string[] temp = new string[glotb.Rows.Count];
                int i = 0;
                foreach (DataRow dr in glotb.Rows)
                {
                    temp[i] = dr["Expr1"].ToString();
                    i++;
                }
                Array.Sort(temp, new CaseInsensitiveComparer());
                autoCompleteall = temp;
                //将临时数组的内容赋给返回数组
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
