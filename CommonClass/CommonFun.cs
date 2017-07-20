using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;



namespace ZCZJ_DPF
{
    public class CommonFun
    {

        /// <summary>
        ///   生成记录ID号（类别+日期+序号+版本号，共16位）   </summary>
        /// <param name="type">类别:用以判断对哪个表进行操作</param> 
        /// <param name="existID">已有ID:修改版本号时传值;新建ID时传空值</param> 
        public static string CreateID(string type, string existID)
        {
            if (existID.Trim() != "" && existID.Trim().Length != 16)
            {
                return "error";
            }

            string oldNO, oldNum, oldEditNum, newNO; //最近ID号、原序号、原版本号、新ID号
            string oldYear, oldMonth, oldDay;       //最近ID的年、月、日
            int newYear, newMonth, newDay;          //现在的年、月、日
            string newNum, newEditNum;              //新序号、版本号

            string sqlText;
            string tableName = "";

            switch (type)
            {
                case "xm":
                    {
                        tableName = "proj_info";
                        break;
                    }
                case "xl":
                    {
                        tableName = "stage_xl";
                        break;
                    }
                case "kj":
                    {
                        tableName = "stage_kj";
                        break;
                    }
                case "sp":
                    {
                        tableName = "stage_sp";
                        break;
                    }
                case "sq":
                    {
                        tableName = "stage_sq";
                        break;
                    }
                case "sy":
                    {
                        tableName = "stage_sy";
                        break;
                    }
                case "gy":
                    {
                        tableName = "process";
                        break;
                    }
                case "sj":
                    {
                        tableName = "design";
                        break;
                    }
                case "bm":
                    {
                        tableName = "bom_info";
                        break;
                    }
                case "cq":
                    {
                        tableName = "m_confirm_info";
                        break;
                    }
                case "sc":
                    {
                        tableName = "stage_sc";
                        break;
                    }
                default:
                    return "error";
            }

            oldYear = "2009";
            oldMonth = "01";
            oldDay = "01";
            oldNO = "";//原ID号
            oldNum = "000";//原序号
            oldEditNum = "000";//原版本号


            newNO = type;   //从前缀开始拼ID号
            newYear = System.DateTime.Now.Year;
            newMonth = System.DateTime.Now.Month;
            newDay = System.DateTime.Now.Day;
            if (newYear.ToString().Length != 4)
            {
                return "error";
            }

            if (existID.Trim() == "")
            {
                //取最近添加的一条记录
                sqlText = "select top 1 id from " + tableName + " order by substring(id,3,14) desc";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                if (dr.Read())
                {
                    oldNO = dr["id"].ToString();
                }

                dr.Close();
            }
            else
            {
                oldNO = existID;
            }

            if (oldNO != "")//不是增加第一条记录时
            {
                oldYear = oldNO.Substring(2, 4);
                oldMonth = oldNO.Substring(6, 2);
                oldDay = oldNO.Substring(8, 2);
                oldNum = oldNO.Substring(10, 3);
                oldEditNum = oldNO.Substring(13, 3);
            }


            //生成新序号，版本号
            if (existID.Trim() != "")//仅生成新版本
            {
                newNO += oldNO.Substring(2, 8);
                newNum = oldNum;
                newEditNum = NumToString(Convert.ToInt32(oldEditNum) + 1, 3);
            }
            else//生成新ID号
            {
                //同一天增加
                if (Convert.ToInt32(oldYear) == newYear && Convert.ToInt32(oldMonth) == newMonth && Convert.ToInt32(oldDay) == newDay)
                {
                    newNO += oldNO.Substring(2, 8);
                    newNum = NumToString(Convert.ToInt32(oldNum) + 1, 3);

                }
                else
                {
                    newNO += newYear.ToString() + NumToString(newMonth, 2) + NumToString(newDay, 2);
                    newNum = "001";
                }

                newEditNum = "000";
            }
            newNO += newNum + newEditNum;

            return newNO;
        }

        //将数字转换成指定长度的字符串
        public static string NumToString(int num, int strLen)
        {
            string str = num.ToString();
            //超出范围
            if (num < 0 || num >= Math.Pow(10, strLen))
            {
                str = "***";
            }
            else
            {
                while (str.Length < strLen)
                {
                    str = "0" + str;
                }
            }
            return str;
        }


        /// <summary>
        ///  级联删除    </summary>
        /// <param name="tabName">数据表名</param> 
        /// <param name="keyName">主键名</param> 
        /// <param name="repeater">Repeater控件</param>
        public static void delMult(string tabName, string keyName, Repeater repeater)
        {
            string sqlText = "";
            string strID = "";
            foreach (RepeaterItem repItem in repeater.Items)
            {
                CheckBox chk = (CheckBox)repItem.FindControl("chkDel");
                if (chk.Checked)
                {
                    //查找该CheckBox所对应纪录的id号,在labID中
                    strID += "'" + ((Label)repItem.FindControl("lblID")).Text + "',";
                }
            }

            if (strID.Length > 1)
            {
                //去掉最后的一个逗号
                strID = strID.Substring(0, strID.Length - 1);
                sqlText = "delete from " + tabName + " where " + keyName + " in (" + strID + ")";
                DBCallCommon.ExeSqlText(sqlText);
            }
        }

        /// <summary>
        ///  给RadioButtonList通过text赋初值    </summary> 
        /// <param name="rbl">RadioButtonList控件</param>
        /// <param name="value">值</param>
        public static void InitRBL(RadioButtonList rbl, string value)
        {
            for (int i = 0; i < rbl.Items.Count; i++)
            {
                if (rbl.Items[i].Text == value)
                {
                    rbl.Items[i].Selected = true;
                }
            }
        }
        /// <summary>
        ///  给RadioButtonList通过value赋初值    </summary> 
        /// <param name="rbl">RadioButtonList控件</param>
        /// <param name="value">值</param>
        public static void InitRBLByValue(RadioButtonList rbl, string value)
        {
            for (int i = 0; i < rbl.Items.Count; i++)
            {
                if (rbl.Items[i].Value == value)
                {
                    rbl.Items[i].Selected = true;
                }
            }
        }

        /// <summary>
        ///  给DropDownList赋初值    </summary> 
        /// <param name="rbl">DropDownList控件</param>
        /// <param name="value">值</param>
        public static void InitDDL(DropDownList ddl, string value)
        {
            ddl.SelectedValue = value;
        }

        /// <summary>
        ///  将dt中数据分页显示    </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="repeater">Repeater控件</param>
        /// <param name="ListPager">ListPager控件</param>
        /// <param name="panel">Panel控件</param> 
        public static void Paging(DataTable dt, Repeater repeater, ListPager listPager, Panel panel)
        {
            repeater.DataSource = dt;
            repeater.DataBind();
            int totalCount = 0;

            if (dt.Rows.Count > 0)
            {
                //totalCount = (int)dt.Rows[0]["TotalCount"];
                panel.Visible = false;
            }
            if (totalCount > listPager.PageSize)
            {
                listPager.Visible = true;
            }
            listPager.TotalItems = totalCount;

            if (dt.Rows.Count == 0)
            {
                panel.Visible = true;
            }
        }

        /// <summary>
        ///  将dt中数据分页显示    </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="repeater">Repeater控件</param>
        /// <param name="UCPaging">UCPaging控件</param>
        /// <param name="panel">Panel控件</param> 
        public static void Paging(DataTable dt, Repeater repeater, UCPaging ucp, Panel panel)
        {
            repeater.DataSource = dt;
            repeater.DataBind();
            int totalCount = 0;
            if (dt.Rows.Count > 0)
            {
                totalCount = (int)dt.Rows[0]["TotalCount"];
                ucp.TotalItems = totalCount;
                panel.Visible = false;
            }
            else
            {
                panel.Visible = true;
            }
            //if (dt.Rows.Count > 0)
            //{
            //    //totalCount = (int)dt.Rows[0]["TotalCount"];
            //    panel.Visible = false;
            //}
            //if (totalCount > listPager.PageSize)
            //{
            //    listPager.Visible = true;
            //}
            //listPager.TotalItems = totalCount;

            //if (dt.Rows.Count == 0)
            //{
            //    panel.Visible = true;
            //}
        }

        /// <summary>
        ///  将dt中数据分页显示    </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="repeater">Repeater控件</param>
        /// <param name="UCPaging">UCPaging控件</param>
        /// <param name="panel">Panel控件</param> 
        public static void Paging(DataTable dt, GridView gv, UCPaging ucp, Panel panel)
        {
            gv.DataSource = dt;
            gv.DataBind();
            int totalCount = 0;
            if (dt.Rows.Count > 0)
            {
                totalCount = (int)dt.Rows[0]["TotalCount"];
                ucp.TotalItems = totalCount;
                panel.Visible = false;
            }
            else
            {
                panel.Visible = true;
            }
        }

        /// <summary>
        ///  将dt中数据分页显示    </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="repeater">Repeater控件</param>
        /// <param name="UCPaging">UCPaging控件</param>
        /// <param name="panel">Panel控件</param> 
        public static void Paging(DataTable dt, Repeater repeater, UCPagingOfMS ucp, Panel panel)
        {
            repeater.DataSource = dt;
            repeater.DataBind();
            int totalCount = 0;
            if (dt.Rows.Count > 0)
            {
                totalCount = (int)dt.Rows[0]["TotalCount"];
                ucp.TotalItems = totalCount;
                panel.Visible = false;
            }
            else
            {
                panel.Visible = true;
            }
            //if (dt.Rows.Count > 0)
            //{
            //    //totalCount = (int)dt.Rows[0]["TotalCount"];
            //    panel.Visible = false;
            //}
            //if (totalCount > listPager.PageSize)
            //{
            //    listPager.Visible = true;
            //}
            //listPager.TotalItems = totalCount;

            //if (dt.Rows.Count == 0)
            //{
            //    panel.Visible = true;
            //}
        }

        /// <summary>
        ///  将dt中数据分页显示    </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="repeater">Repeater控件</param>
        /// <param name="UCPaging">UCPaging控件</param>
        /// <param name="panel">Panel控件</param> 
        public static void Paging(DataTable dt, GridView gv, UCPagingOfMS ucp, Panel panel)
        {
            gv.DataSource = dt;
            gv.DataBind();
            int totalCount = 0;
            if (dt.Rows.Count > 0)
            {
                totalCount = (int)dt.Rows[0]["TotalCount"];
                ucp.TotalItems = totalCount;
                panel.Visible = false;
            }
            else
            {
                panel.Visible = true;
            }
        }
        /// <summary>
        ///  获取文件夹名，截取“\”     </summary>
        /// <param name="DirectoryPath">文件夹路径</param>
        public string GetDirectoryName(string DirectoryPath)//获取文件夹名，截取“\” 
        {
            int j = 0; char[] c = DirectoryPath.ToCharArray();
            for (int i = c.Length - 1; i >= 0; i--)//从后面截取 
            {
                j = i;
                if (c[i] == '\\')
                {
                    break;//遇"\"跳出,并返回"\"的位置 
                }
            }
            //return j+1;
            return DirectoryPath.Substring(j + 1);
        }
        /// <summary>
        ///  复制文件夹    </summary>
        /// <param name="DirectoryPath">对象文件夹路径</param>
        /// <param name="DirAddress">目标文件夹地址</param>
        public void CopyDirectory(string DirectoryPath, string DirAddress)//复制文件夹， 
        {

            string s = GetDirectoryName(DirectoryPath);//获取文件夹名 
            if (Directory.Exists(DirAddress + "\\" + s))
            {
                Directory.Delete(DirAddress + "\\" + s, true);//若文件夹存在，不管目录是否为空，删除 
                Directory.CreateDirectory(DirAddress + "\\" + s);//删除后，重新创建文件夹 
            }
            else
            {
                Directory.CreateDirectory(DirAddress + "\\" + s);//文件夹不存在，创建 
            }
            #region//递归
            DirectoryInfo DirectoryArray = new DirectoryInfo(DirectoryPath);
            FileInfo[] Files = DirectoryArray.GetFiles();//获取该文件夹下的文件列表 
            DirectoryInfo[] Directorys = DirectoryArray.GetDirectories();//获取该文件夹下的文件夹列表 
            foreach (FileInfo inf in Files)//逐个复制文件 
            {
                File.Copy(DirectoryPath + "\\" + inf.Name, DirAddress + "\\" + s + "\\" + inf.Name);
            }
            foreach (DirectoryInfo Dir in Directorys)//逐个获取文件夹名称，并递归调用方法本身 
            {
                CopyDirectory(DirectoryPath + "\\" + Dir.Name, DirAddress + "\\" + s);
            }
            #endregion
        }

        /// <summary>
        ///获取附件存入文件夹的名字，如果没有则创建该文件夹    </summary>
        /// <param name="DirectoryPath">对象文件夹路径</param>
        /// <param name="type_erji">项目二级类别，作为文件夹名字生成的依据</param>
        public static string CreateDirName(string DirectoryPath, string type)
        {
            string dirName1 = DateTime.Now.Year.ToString();//以年份作为第一层文件夹名
            string dirName2 = type;//以项目类名作为第二层文件夹名
            if (!Directory.Exists(DirectoryPath + "\\" + dirName1))//如果没有，则创建文件夹
            {
                Directory.CreateDirectory(DirectoryPath + "\\" + dirName1);//创建第一层文件夹
                Directory.CreateDirectory(DirectoryPath + "\\" + dirName1 + "\\" + dirName2);//创建第二层文件夹

            }
            else if (!Directory.Exists(DirectoryPath + "\\" + dirName1 + "\\" + dirName2))
            {
                Directory.CreateDirectory(DirectoryPath + "\\" + dirName1 + "\\" + dirName2);//创建第二层文件夹
            }

            return DirectoryPath + "\\" + dirName1 + "\\" + dirName2 + "\\";
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="Order_Field">排序字段</param>
        /// <param name="OrderType">排序类型（1:升序　0:降序）</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataByPagerQueryParam(PagerQueryParam pager)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "procpagingen");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Table_Name", pager.TableName, SqlDbType.VarChar, 5000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Primary_Key", pager.PrimaryKey, SqlDbType.VarChar, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Show_Fields", pager.ShowFields, SqlDbType.VarChar, 8000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Order_Field", pager.OrderField, SqlDbType.VarChar, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@OrderType", pager.OrderType.ToString(), SqlDbType.VarChar, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@where_Condition", pager.StrWhere, SqlDbType.VarChar, 4000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Page_Size", pager.PageSize.ToString(), SqlDbType.Int, 200);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Page_Index", pager.PageIndex.ToString(), SqlDbType.Int, 200);
                dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }


        /// <summary>
        /// 带有自动表示的主键的分页获取数据列表
        /// </summary>
        /// <param name="Order_Field">排序字段</param>
        /// <param name="OrderType">排序类型（1:升序　0:降序）</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataByPagerQueryParamWithPriKey(PagerQueryParam pager)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "procpagwithprikey");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Table_Name", pager.TableName, SqlDbType.VarChar, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Primary_Key", pager.PrimaryKey, SqlDbType.VarChar, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Show_Fields", pager.ShowFields, SqlDbType.VarChar, 8000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Order_Field", pager.OrderField, SqlDbType.VarChar, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@OrderType", pager.OrderType.ToString(), SqlDbType.VarChar, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@where_Condition", pager.StrWhere, SqlDbType.VarChar, 4000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Page_Size", pager.PageSize.ToString(), SqlDbType.Int, 200);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Page_Index", pager.PageIndex.ToString(), SqlDbType.Int, 200);
                dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="Order_Field">排序字段</param>
        /// <param name="OrderType">排序类型（1:升序　0:降序）</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataByPagerQueryParam1(PagerQueryParam pager)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "SplitPage");

                sqlCmd.Parameters.Add("@ExecSql", SqlDbType.VarChar, 10000);
                sqlCmd.Parameters["@ExecSql"].Value = pager.TableName;
                sqlCmd.Parameters.Add("@CurrentPage", SqlDbType.Int);
                sqlCmd.Parameters["@CurrentPage"].Value = pager.PageIndex;
                sqlCmd.Parameters.Add("@PageSize", SqlDbType.Int);
                sqlCmd.Parameters["@PageSize"].Value = pager.PageSize;

                dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="Order_Field">排序字段</param>
        /// <param name="OrderType">排序类型（1:升序　0:降序）</param>
        /// <param name="PageSize">页大小</param>
        /// <param name="PageIndex">页码</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns>DataTable</returns>
        public static DataTable GetDataByPagerQueryParamGroupBy(PagerQueryParamGroupBy pager)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlCmd = new SqlCommand();
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                DBCallCommon.PrepareStoredProc(sqlConn, sqlCmd, "procpagingGroupBy");
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Table_Name", pager.TableName, SqlDbType.VarChar, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Primary_Key", pager.PrimaryKey, SqlDbType.VarChar, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Show_Fields", pager.ShowFields, SqlDbType.VarChar, 8000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Order_Field", pager.OrderField, SqlDbType.VarChar, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Group_Field", pager.GroupField, SqlDbType.VarChar, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@OrderType", pager.OrderType.ToString(), SqlDbType.VarChar, 1000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@where_Condition", pager.StrWhere, SqlDbType.VarChar, 3000);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Page_Size", pager.PageSize.ToString(), SqlDbType.Int, 200);
                DBCallCommon.AddParameterToStoredProc(sqlCmd, "@Page_Index", pager.PageIndex.ToString(), SqlDbType.Int, 200);
                dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }




        /**/
        /// <summary>
        /// 判断字符是否英文半角字符或标点
        /// </summary>
        public static bool IsBjChar(char c)
        {
            int i = (int)c;
            return i >= 32 && i <= 126;
        }

        /**/
        /// <summary>
        /// 判断字符是否全角字符或标点
        /// </summary>
        public static bool IsQjChar(char c)
        {
            if (c == '\u3000') return true;

            int i = (int)c - 65248;
            if (i < 32) return false;
            return IsBjChar((char)i);
        }

        /**/
        /// <summary>
        /// 将字符串中的全角字符转换为半角
        /// </summary>
        public static string ToBanJiao(string s)
        {
            if (s == null || s.Trim() == string.Empty) return s;

            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '\u3000')
                    sb.Append('\u0020');
                else if (IsQjChar(s[i]))
                    sb.Append((char)((int)s[i] - 65248));
                else
                    sb.Append(s[i]);
            }

            return sb.ToString();
        }



        internal static void Paging(DataTable dt, YYControls.SmartGridView GridView1, PagerQueryParam pager_pro, Panel NoDataPanel)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 将string转double
        /// </summary>
        /// <param name="i">string</param>
        /// <returns>double</returns>
        public static double ComTryDouble(string i)
        {
            double j = 0;
            double.TryParse(i, out j);
            return j;
        }

        /// <summary>
        /// 将string转int
        /// </summary>
        /// <param name="i">string</param>
        /// <returns>int</returns>
        public static int ComTryInt(string i)
        {
            int j = 0;
            int.TryParse(i, out j);
            return j;
        }

        /// <summary>
        /// 将string转decimal
        /// </summary>
        /// <param name="i">string</param>
        /// <returns>decimal</returns>
        public static Decimal ComTryDecimal(string i)
        {
            Decimal j = 0;
            Decimal.TryParse(i, out j);
            return j;
        }
    }
}
