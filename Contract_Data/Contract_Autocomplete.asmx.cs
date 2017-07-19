using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Collections;

namespace ZCZJ_DPF.Contract_Data
{
    /// <summary>
    /// Contract_Autocomplete 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class Contract_Autocomplete : System.Web.Services.WebService
    {
        private string[] autoCompleteall = null;
        [WebMethod]
        public String[] GetHTBH(string prefixText, int count)//查询已生成合同的合同号
        {
            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;//检测参数是否为空
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string sql = "select distinct PCON_BCODE from TBPM_CONPCHSINFO  where PCON_BCODE like '%" + prefixText + "%' and PCON_BCODE is not null";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            //读取内容文件的数据到临时数组
            string[] temp = new string[dt.Rows.Count];
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                temp[i] = dt.Rows[i]["PCON_BCODE"].ToString();
            }

            Array.Sort(temp, new CaseInsensitiveComparer());
            //将临时数组的内容赋给返回数组
            autoCompleteall = temp;

            int matchCount = autoCompleteall.Length >= count ? count : autoCompleteall.Length;
            string[] matchResultList = new string[matchCount];
            if (matchCount > 0)
            {   //复制搜索结果
                Array.Copy(autoCompleteall, 0, matchResultList, 0, matchCount);
            }
            return matchResultList;
        }
        //业主合同号
        [WebMethod]
        public String[] GetYZHTBH(string prefixText, int count)
        {
            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;//检测参数是否为空
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string sql = "select distinct PCON_YZHTH from TBPM_CONPCHSINFO  where PCON_YZHTH like '%" + prefixText + "%'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            //读取内容文件的数据到临时数组
            string[] temp = new string[dt.Rows.Count];
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                temp[i] = dt.Rows[i]["PCON_YZHTH"].ToString();
            }

            Array.Sort(temp, new CaseInsensitiveComparer());
            //将临时数组的内容赋给返回数组
            autoCompleteall = temp;

            int matchCount = autoCompleteall.Length >= count ? count : autoCompleteall.Length;
            string[] matchResultList = new string[matchCount];
            if (matchCount > 0)
            {   //复制搜索结果
                Array.Copy(autoCompleteall, 0, matchResultList, 0, matchCount);
            }
            return matchResultList;
        }
        //项目名称
        [WebMethod]
        public String[] GetXMMC(string prefixText, int count)
        {
            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;//检测参数是否为空
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string sql = "select distinct PCON_ENGNAME from TBPM_CONPCHSINFO  where PCON_ENGNAME like '%" + prefixText + "%'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            //读取内容文件的数据到临时数组
            string[] temp = new string[dt.Rows.Count];
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                temp[i] = dt.Rows[i]["PCON_ENGNAME"].ToString();
            }

            Array.Sort(temp, new CaseInsensitiveComparer());
            //将临时数组的内容赋给返回数组
            autoCompleteall = temp;

            int matchCount = autoCompleteall.Length >= count ? count : autoCompleteall.Length;
            string[] matchResultList = new string[matchCount];
            if (matchCount > 0)
            {   //复制搜索结果
                Array.Copy(autoCompleteall, 0, matchResultList, 0, matchCount);
            }
            return matchResultList;
        }
        //设备名称
        [WebMethod]
        public String[] GetSBMC(string prefixText, int count)
        {
            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;//检测参数是否为空
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string sql = "select distinct PCON_ENGTYPE from TBPM_CONPCHSINFO  where PCON_ENGTYPE like '%" + prefixText + "%'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            //读取内容文件的数据到临时数组
            string[] temp = new string[dt.Rows.Count];
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                temp[i] = dt.Rows[i]["PCON_ENGTYPE"].ToString();
            }

            Array.Sort(temp, new CaseInsensitiveComparer());
            //将临时数组的内容赋给返回数组
            autoCompleteall = temp;

            int matchCount = autoCompleteall.Length >= count ? count : autoCompleteall.Length;
            string[] matchResultList = new string[matchCount];
            if (matchCount > 0)
            {   //复制搜索结果
                Array.Copy(autoCompleteall, 0, matchResultList, 0, matchCount);
            }
            return matchResultList;
        }

        //图号
        [WebMethod]
        public String[] GetTH(string prefixText, int count)
        {
            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;//检测参数是否为空
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string sql = "select distinct CM_MAP from TBPM_CONPCHSINFO  where CM_MAP like '%" + prefixText + "%'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            //读取内容文件的数据到临时数组
            string[] temp = new string[dt.Rows.Count];
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                temp[i] = dt.Rows[i]["CM_MAP"].ToString();
            }

            Array.Sort(temp, new CaseInsensitiveComparer());
            //将临时数组的内容赋给返回数组
            autoCompleteall = temp;

            int matchCount = autoCompleteall.Length >= count ? count : autoCompleteall.Length;
            string[] matchResultList = new string[matchCount];
            if (matchCount > 0)
            {   //复制搜索结果
                Array.Copy(autoCompleteall, 0, matchResultList, 0, matchCount);
            }
            return matchResultList;
        }
        //顾客名称
        [WebMethod]
        public String[] GetGKMC(string prefixText, int count)
        {
            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;//检测参数是否为空
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            string sql = "select distinct PCON_CUSTMNAME from TBPM_CONPCHSINFO  where PCON_CUSTMNAME like '%" + prefixText + "%'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            //读取内容文件的数据到临时数组
            string[] temp = new string[dt.Rows.Count];
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                temp[i] = dt.Rows[i]["PCON_CUSTMNAME"].ToString();
            }

            Array.Sort(temp, new CaseInsensitiveComparer());
            //将临时数组的内容赋给返回数组
            autoCompleteall = temp;

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
