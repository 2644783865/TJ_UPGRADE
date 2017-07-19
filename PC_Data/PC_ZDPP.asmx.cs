using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Collections;
using Microsoft.Office.Interop.MSProject;

namespace ZCZJ_DPF.PC_Data
{
    /// <summary>
    /// PC_ZDPP 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class PC_ZDPP : System.Web.Services.WebService
    {
        private string[] autoCompleteWordList = null;
        private string[] autoCompleteall = null;
        [WebMethod]
        public String[] GetGYS_NAME(string prefixText, int count)
        {
            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;//检测参数是否为空
            DataTable dt = new DataTable();
            if (autoCompleteWordList == null)
            {
                string sql = "";
                sql = "select CS_NAME,CS_ADDRESS,CS_PHONO,CS_FAX,CS_Bank,CS_Account,CS_TAX,CS_ZIP from TBCS_CUSUPINFO where CS_TYPE='2' and (CS_HRCODE LIKE '%" + prefixText + "%'or CS_NAME like '%" + prefixText + "%') AND CS_State='0' ORDER BY CS_CODE";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
            }
            //读取内容文件的数据到临时数组
            string[] temp = new string[dt.Rows.Count];
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                temp[i] = dt.Rows[i]["CS_NAME"].ToString();
                if (dt.Rows[i]["CS_ADDRESS"].ToString() != "")
                {
                    temp[i] += "|" + dt.Rows[i]["CS_ADDRESS"].ToString();
                }
                else
                {
                    temp[i] += "|无";
                }
                if (dt.Rows[i]["CS_PHONO"].ToString() != "")
                {
                    temp[i] += "|" + dt.Rows[i]["CS_PHONO"].ToString();
                }
                else
                {
                    temp[i] += "|无";
                }
                if (dt.Rows[i]["CS_FAX"].ToString() != "")
                {
                    temp[i] += "|" + dt.Rows[i]["CS_FAX"].ToString();
                }
                else
                {
                    temp[i] += "|无";
                }
                if (dt.Rows[i]["CS_Bank"].ToString() != "")
                {
                    temp[i] += "|" + dt.Rows[i]["CS_Bank"].ToString();
                }
                else
                {
                    temp[i] += "|无";
                }
                if (dt.Rows[i]["CS_Account"].ToString() != "")
                {
                    temp[i] += "|" + dt.Rows[i]["CS_Account"].ToString();
                }
                else
                {
                    temp[i] += "|无";
                }
                if (dt.Rows[i]["CS_TAX"].ToString() != "")
                {
                    temp[i] += "|" + dt.Rows[i]["CS_TAX"].ToString();
                }
                else
                {
                    temp[i] += "|无";
                }
                if (dt.Rows[i]["CS_ZIP"].ToString() != "")
                {
                    temp[i] += "|" + dt.Rows[i]["CS_ZIP"].ToString();
                }
                else
                {
                    temp[i] += "|无";
                }

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

        [WebMethod]
        public String[] GetDDBH0(string prefixText, int count)//查询未生成合同的订单
        {
            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;//检测参数是否为空
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            if (autoCompleteWordList == null)
            {
                string sql = "";
                string sql1 = "";
                sql = "select PO_CODE from TBPC_PURORDERTOTAL where PO_CODE like '%" + prefixText + "%'and PO_CODE is not null";
                sql1 = "select distinct HT_DDBH from PC_CGHT where HT_DDBH like '%" + prefixText + "%' and HT_DDBH is not null";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
            }
            //读取内容文件的数据到临时数组
            string[] ht_ddbh = new string[dt1.Rows.Count];
            List<string> list = new List<string>();
            string[] temp = new string[dt.Rows.Count - dt1.Rows.Count];
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                bool fuzhi = true;
                for (int j = 0, length1 = dt1.Rows.Count; j < length1; j++)
                {
                    if (dt1.Rows[j]["HT_DDBH"].ToString().Contains('|'))
                    {
                        ht_ddbh = dt1.Rows[j]["HT_DDBH"].ToString().Split('|');
                    }
                    else
                    {
                        ht_ddbh[0] = dt1.Rows[j]["HT_DDBH"].ToString();
                    }
                    if (ht_ddbh.Contains(dt.Rows[i]["PO_CODE"].ToString()))
                    {
                        fuzhi = false;
                    }
                }

                if (fuzhi == true)
                {
                    list.Add(dt.Rows[i]["PO_CODE"].ToString());
                }

            }
            for (int i = 0,length=temp.Length; i < length; i++)
            {
                if (i<=list.Count)
                {
                    temp[i] = list[i];
                }           
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

        [WebMethod]
        public String[] GetDDBH1(string prefixText, int count)//查询已生成合同的订单
        {
            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;//检测参数是否为空
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            if (autoCompleteWordList == null)
            {
                string sql = "";
                sql = "select distinct HT_DDBH from PC_CGHT where HT_DDBH like '%" + prefixText + "%' and HT_DDBH is not null";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
            }
            //读取内容文件的数据到临时数组
            string[] temp = new string[dt.Rows.Count];
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                temp[i] = dt.Rows[i]["HT_DDBH"].ToString();
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

        [WebMethod]
        public String[] GetHTBH(string prefixText, int count)//查询已生成合同的合同号
        {
            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;//检测参数是否为空
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            if (autoCompleteWordList == null)
            {
                string sql = "";
                sql = "select distinct HT_XFHTBH from PC_CGHT where HT_XFHTBH like '%" + prefixText + "%' and HT_XFHTBH is not null";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
            }
            //读取内容文件的数据到临时数组
            string[] temp = new string[dt.Rows.Count];
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                temp[i] = dt.Rows[i]["HT_XFHTBH"].ToString();
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

        //[WebMethod]
        //public String[] GetHTBH1(string prefixText, int count)//查询已生成合同的合同号
        //{
        //    if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;//检测参数是否为空
        //    DataTable dt = new DataTable();
        //    DataTable dt1 = new DataTable();
        //    if (autoCompleteWordList == null)
        //    {
        //        string sql = "";
        //        sql = "select distinct HT_HTBH from EQU_GXHT where HT_HTBH like '%" + prefixText + "%' and HT_HTBH is not null";
        //        dt = DBCallCommon.GetDTUsingSqlText(sql);
        //    }
        //    //读取内容文件的数据到临时数组
        //    string[] temp = new string[dt.Rows.Count];
        //    for (int i = 0, length = dt.Rows.Count; i < length; i++)
        //    {
        //        temp[i] = dt.Rows[i]["HT_HTBH"].ToString();
        //    }

        //    Array.Sort(temp, new CaseInsensitiveComparer());
        //    //将临时数组的内容赋给返回数组
        //    autoCompleteall = temp;

        //    int matchCount = autoCompleteall.Length >= count ? count : autoCompleteall.Length;
        //    string[] matchResultList = new string[matchCount];
        //    if (matchCount > 0)
        //    {   //复制搜索结果
        //        Array.Copy(autoCompleteall, 0, matchResultList, 0, matchCount);
        //    }
        //    return matchResultList;
        //}


        [WebMethod]
        public string[] GetGYS(string prefixText, int count)
        {
            if (string.IsNullOrEmpty(prefixText) == true || count <= 0) return null;//检测参数是否为空
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            if (autoCompleteWordList == null)
            {
                string sql = "";
                sql = "select distinct HT_GF from PC_CGHT where HT_GF like '%" + prefixText + "%' and HT_GF is not null";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
            }
            //读取内容文件的数据到临时数组
            string[] temp = new string[dt.Rows.Count];
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                temp[i] = dt.Rows[i]["HT_GF"].ToString();
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
