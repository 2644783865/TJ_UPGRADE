using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

namespace ZCZJ_DPF.Basic_Data
{
    /// <summary>
    /// Basic_Data_AutoComplete 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class Basic_Data_AutoComplete : System.Web.Services.WebService
    {
        private string[] autoCompleteWordList = null;
        private string[] autoCompleteall = null;

        [WebMethod]
        public String[] GetNAME(string prefixText, int count)
        {
            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;//检测参数是否为空
            if (autoCompleteWordList == null)
            {
                string sql = "select distinct stName from TBDS_EDITPASSWORDRECORD where stName like '%" + prefixText + "%' and stName is not null";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                //读取内容文件的数据到临时数组
                string[] temp = new string[dt.Rows.Count];
                for (int i = 0, length = dt.Rows.Count; i < length; i++)
                {
                    temp[i] = dt.Rows[i]["stName"].ToString();
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
