using System;
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
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Net.Mail;

namespace ZCZJ_DPF
{
    public class DBCallCommon
    {
        public DBCallCommon()
        {

        }
        #region general code
        public static string GetStringValue(string name)
        {
            SqlConnection.ClearAllPools();
            System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
            return ((string)(configurationAppSettings.GetValue(name, typeof(string))));
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <param name="conn"></param>
        public static void openConn(SqlConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 关闭数据库连接    
        /// </summary>
        /// <param name="conn"></param>
        public static void closeConn(SqlConnection conn)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            else
            {
                return;
            }
        }

        public static void PrepareStoredProc(SqlConnection conn, SqlCommand cmd, string procName)
        {
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procName;
            cmd.CommandTimeout = 0;

            cmd.Parameters.Clear();

            return;
        }


        public static void AddParameterToStoredProc(SqlCommand cmd, string paramName, string paramValue,
            SqlDbType paramType, int paramSize)
        {
            SqlParameter sqlParam = new SqlParameter();

            sqlParam.ParameterName = paramName;
            sqlParam.SqlDbType = paramType;
            sqlParam.Value = paramValue;

            if (paramType == SqlDbType.VarChar || paramType == SqlDbType.Char)
                sqlParam.Size = paramSize;

            cmd.Parameters.Add(sqlParam);
            cmd.CommandTimeout = 0;

            return;
        }


        public static DataTable GetDataTableUsingCmd(SqlCommand cmd)
        {
            return GetDataTableUsingCmd(cmd, 0);
        }

        public static DataTable GetDataTableUsingCmd(SqlCommand cmd, int nTableIndex)
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            da.Fill(ds);
            da.Dispose();

            return ds.Tables[nTableIndex];
        }

        public static string CheckNull(string str)
        {
            if (str == "")
                return "Null";
            return str;
        }
        #endregion general code


        public static SqlDataReader GetDataReader(SqlConnection conn, SqlCommand cmd)
        {
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            return dr;
        }


        /// <summary>
        /// 使用sql语句获取SqlDataReader
        /// </summary>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public static SqlDataReader GetDRUsingSqlText(string sqlText)
        {
            SqlConnection sqlConn = new SqlConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            try
            {
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = sqlText;
                SqlDataReader dr = GetDataReader(sqlConn, sqlCmd);
                return dr;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 使用ql语句获取SqlDataReader
        /// </summary>
        /// <param name="sqlText"></param>
        /// <param name="time">等待时间，单位：毫秒</param>
        /// <returns></returns>
        public static SqlDataReader GetDRUsingSqlText(string sqlText, int time)
        {
            SqlConnection sqlConn = new SqlConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandTimeout = time;
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            try
            {
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = sqlText;
                SqlDataReader dr = GetDataReader(sqlConn, sqlCmd);
                return dr;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 使用sql语句获取SqlDataReader的参考代码
        /// </summary>
        /// <param name="MySQL"></param>
        /// <returns></returns>
        public SqlDataReader GetSqlReader(String MySQL)
        {
            //// 数据库连接参数(对客户端应用程序配置文件的访问)
            //string strConn = @ConfigurationManager.ConnectionStrings["connectionStrings"].ConnectionString;
            //// 创建连接数据库的一个打开连接
            //SqlConnection MyConn = new SqlConnection(strConn);
            SqlConnection MyConn = new SqlConnection();
            //SqlCommand sqlCmd = new SqlCommand();
            MyConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            try // 正常运行
            {
                // 使用 ConnectionString 所指定的属性设置打开数据库连接
                MyConn.Open();
                // 数据库SQL语句
                String strSQL = @MySQL.Trim().ToString();
                // 要对数据库执行的一个SQL语句或存储过程
                SqlCommand MyComm = new SqlCommand(strSQL, MyConn);
                // 提供一种从数据库读取只进的行流的一种方式
                SqlDataReader MyReader = MyComm.ExecuteReader();
                // 读取数据,判断是否有数据
                if (MyReader.HasRows)
                {
                    // 返回成功
                    return MyReader;
                }
                else
                {
                    Console.Write("<script language=JavaScript>");
                    Console.Write("alert('系统提示：数据读取失败或网络忙，请稍后再试！');");
                    Console.Write("</script>");
                    if ((MyReader != null) & (MyReader.IsClosed != true))
                    {
                        // 关闭
                        MyReader.Close();
                    }
                    // 判断数据库连接
                    if (MyConn.State == ConnectionState.Open)
                    {
                        // 关闭数据库连接
                        MyConn.Close();
                    }
                    // 返回失败
                    return null;
                }
            }
            catch (SqlException) // 数据库操作异常处理
            {
                Console.Write("<script language=JavaScript>");
                Console.Write("alert('系统提示：当前数据库操作失败或网络忙，请稍后再试！');");
                Console.Write("</script>");
                if (MyConn.State == ConnectionState.Open)
                {
                    // 关闭数据库连接
                    MyConn.Close();
                }
                // 返回失败
                return null;
            }
            catch // 异常处理
            {
                if (MyConn.State == ConnectionState.Open)
                {
                    // 关闭数据库连接
                    MyConn.Close();
                }
                // 返回失败
                return null;
            }
            finally // 执行完毕清除在try块中分配的任何资源
            {
                if (MyConn.State == ConnectionState.Open)
                {
                    // 关闭数据库连接
                    MyConn.Close();
                }
            }
        }

        /// <summary>
        /// 使用sql语句获取DataTable
        /// </summary>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public static DataTable GetDTUsingSqlText(string sqlText)
        {
            SqlConnection sqlConn = new SqlConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            try
            {
                openConn(sqlConn);
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = sqlText;
                sqlCmd.CommandTimeout = 1000;
                DataTable dt = GetDataTableUsingCmd(sqlCmd);
                sqlConn.Close();
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                closeConn(sqlConn);
            }
        }


        /// <summary>
        ///执行SQL语句，不返回值
        /// </summary>
        /// <param name="sqlText"></param>
        public static void ExeSqlText(string sqlText)
        {
            SqlConnection sqlConn = new SqlConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            try
            {
                openConn(sqlConn);
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = sqlText;
                sqlCmd.CommandTimeout = 600;
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                closeConn(sqlConn);
            }
        }

        /// <summary>
        /// 执行SQL语句，返回受影响行数值
        /// </summary>
        /// <param name="sqlText"></param>
        /// <returns></returns>
        public static int ExeSqlTextGetInt(string sqlText)
        {
            SqlConnection sqlConn = new SqlConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            try
            {
                openConn(sqlConn);
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = sqlText;
                sqlCmd.CommandTimeout = 600;
                return sqlCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                closeConn(sqlConn);
            }
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sqlTexts"></param>
        public static void ExecuteTrans(List<string> sqlTexts)
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            sqlConn.Open();

            //启用事务
            SqlTransaction sqlTran = sqlConn.BeginTransaction();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = sqlConn;
            sqlCmd.Transaction = sqlTran;
            sqlCmd.CommandTimeout = 0;
            try
            {
                foreach (string sqlText in sqlTexts)
                {
                    sqlCmd.CommandText = sqlText;
                    sqlCmd.ExecuteNonQuery();
                }
                sqlTran.Commit();
            }
            catch (Exception)
            {
                sqlTran.Rollback();
                throw;
            }
            finally
            {
                closeConn(sqlConn);
            }
        }

       


        /// <summary>
        /// 绑定操作
        /// </summary>
        /// <param name="cbl"></param>
        /// <param name="sql"></param>
        public static void FillCheckBoxList(CheckBoxList cbl, string sql)
        {
            DataSet dsDpl = new DataSet();
            dsDpl = FillDataSet(sql);
            cbl.Items.Clear();

            if (dsDpl.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsDpl.Tables[0].Rows.Count; i++)
                {
                    cbl.Items.Add(new ListItem(dsDpl.Tables[0].Rows[i][0].ToString(), dsDpl.Tables[0].Rows[i][1].ToString()));
                }
            }
        }

        /// <summary>  
        /// 根据comText查询数据库,将结果填入DataSet并返回 </summary>
        /// <param name="ComText">SQL语句</param>    
        /// <returns>填充后的dataset</returns>
        public static DataSet FillDataSet(String comText)
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            SqlDataAdapter sda = new SqlDataAdapter(comText, sqlConn);
            DataSet ds = new DataSet();
            ds.Tables.Clear();
            openConn(sqlConn);
            sda.Fill(ds);
            closeConn(sqlConn);
            return ds;
        }

        /// <summary>
        ///  绑定数据到DropDownList  </summary>
        /// <param name="ddl">Repeater控件名</param> 
        /// <param name="sqlText">SQL语句</param>  
        /// <param name="dataText">DataTextField绑定值</param> 
        /// <param name="dataValue">DataValueField绑定值</param> 
        public static void BindDdl(DropDownList ddl, string sqlText,
                                   string dataText, string dataValue)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddl.DataSource = dt;
            ddl.DataTextField = dataText;
            ddl.DataValueField = dataValue;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddl.SelectedIndex = 0;
        }

        public static void BindDDLData(DropDownList ddl, string sqlText,
                                   string dataText, string dataValue)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddl.DataSource = dt;
            ddl.DataTextField = dataText;
            ddl.DataValueField = dataValue;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("-请选择-", ""));
            ddl.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定单选按钮组的数据源
        /// </summary>
        /// <param name="rbl"></param>
        /// <param name="sql"></param>
        public static void FillRadioButtonList(RadioButtonList rbl, string sql)
        {
            DataSet dsDpl = new DataSet();
            dsDpl = FillDataSet(sql);
            rbl.Items.Clear();

            if (dsDpl.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsDpl.Tables[0].Rows.Count; i++)
                {
                    rbl.Items.Add(new ListItem(dsDpl.Tables[0].Rows[i][0].ToString(), dsDpl.Tables[0].Rows[i][1].ToString()));
                }
            }
        }


        /// <summary>
        ///  从数据库中读出记录并填充到repeater  </summary>
        /// <param name="dpl">Repeater控件名</param> 
        /// <param name="sql">SQL语句</param>     
        public static void BindRepeater(Repeater rep, string sqlText)
        {
            DataSet dsDpl = new DataSet();
            dsDpl = FillDataSet(sqlText);
            rep.DataSource = dsDpl;
            rep.DataBind();
        }

        /// <summary>
        ///  将Dataset中的数据绑定到GridView中  </summary>
        /// <param name="ds">dataset</param> 
        /// <param name="gv">gridview</param>    
        public static void BindGridView(GridView gv, string sqlText)
        {
            DataSet ds = new DataSet();
            ds = FillDataSet(sqlText);
            gv.DataSource = ds;
            gv.DataBind();
        }

        /// <summary>
        /// 创建树      </summary>
        /// <param name="entTree">TreeView</param>
        /// <param name="dsTree">DataSet</param>   
        public static void BindTree(TreeView entTree, DataSet dsTree)
        {
            entTree.Nodes.Clear();
            TreeNode root = new TreeNode();
            root.Text = dsTree.Tables[0].Rows[0]["name"].ToString();
            root.Value = dsTree.Tables[0].Rows[0]["id"].ToString();
            root.Expanded = true;
            root.ImageUrl = "~/images/folder.bmp";
            entTree.Nodes.Add(root);
            int rootId = Convert.ToInt16(root.Value.ToString());
            BindChild(rootId, root, dsTree);
        }

        //创建树根子节点   
        public static void BindChild(int curId, TreeNode parentNode, DataSet dsTree)
        {
            DataRow[] drs = dsTree.Tables[0].Select("parentid=" + curId);
            foreach (DataRow dr in drs)
            {
                TreeNode tempNode = new TreeNode();

                if (dr["productKey"].ToString() == "")
                {
                    tempNode.Text = dr["name"].ToString();
                }
                else
                {
                    tempNode.Text = dr["productKey"].ToString();
                }
                tempNode.Value = dr["id"].ToString();

                if (dr["iscategory"].ToString() == "0")
                {
                    tempNode.ImageUrl = "~/images/yes.gif";
                }
                tempNode.ShowCheckBox = false;
                tempNode.Expanded = false;
                parentNode.ChildNodes.Add(tempNode);

                int parentId = Convert.ToInt16(dr["id"].ToString());
                BindChild(parentId, tempNode, dsTree);
            }
        }

        /// </summary>   上传文件并返回文件名  </summary>
        /// <param name="file">HtmlInputFile控件</param>
        /// <param name="Path">绝对路径.如:Server.MapPath(@"Image/upload")与Server.MapPath(@"Image/upload/")均可,用\符号亦可</param>
        /// <returns>返回的文件名即上传后的文件名</returns>
        public static string Savefile(FileUpload file, String path)
        {
            string strOldFile = "";　　//上传文件名
            string strExtent = "";     //上传文件扩展名
            string strNewFile = "";     //上传文件新名

            if (file.PostedFile.FileName != string.Empty)
            {
                strOldFile = file.PostedFile.FileName;
                strExtent = strOldFile.Substring(strOldFile.LastIndexOf("."));
                strNewFile = System.DateTime.Now.ToString("yyyyMMddHHmmss") + strExtent;

                if (path.LastIndexOf("\\") == path.Length)
                {
                    file.PostedFile.SaveAs(path + strNewFile);
                }
                else
                {
                    file.PostedFile.SaveAs(path + "\\" + strNewFile);
                }
            }
            return strNewFile;
        }

        /// </summary>   上传文件  </summary>
        /// <param name="file">FileUpload控件</param>
        /// <param name="Path">绝对路径.如:Server.MapPath(@"Image/upload")与Server.MapPath(@"Image/upload/")均可,用\符号亦可</param>
        public static void SaveFile(FileUpload file, String fileName, String path)
        {
            if (file.PostedFile.FileName != string.Empty)
            {
                //文件路径和文件名文件名
                string fName = path + fileName;
                file.PostedFile.SaveAs(fName);
            }
        }

        /// <summary>   删除文件  </summary>    
        /// <param name="name">文件绝对路径.如:Server.MapPath(@"Image/upload")与Server.MapPath(@"Image/upload/")均可,用\符号亦可</param>
        public static void DeleteFile(String name)
        {
            if (System.IO.File.Exists(name))
            {
                System.IO.File.Delete(name);
            }
        }

        /// <summary>   移动文件  </summary>    
        /// <param name="name">文件绝对路径.如:Server.MapPath(@"Image/upload")与Server.MapPath(@"Image/upload/")均可,用\符号亦可</param>
        public static void MoveFile(String oldPath, String newPath)
        {
            FileInfo fi = new FileInfo(oldPath);

            if (fi.Exists)
            {
                fi.MoveTo(newPath);
            }
        }

        /// <summary>
        ///  判断gridview控件中的记录是否被选定  </summary>
        /// <param name="gv">GridView</param> 
        /// <param name="ID">GridView中CheckBox控件的ID</param> 
        /// <returns>bool值</returns>
        public static bool IsSelected(GridView gv, string ID)
        {
            int i;
            for (i = 0; i < gv.Rows.Count; i++)
            {
                GridViewRow row = gv.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl(ID)).Checked;
                if (isChecked)
                {
                    break;
                }
            }
            if (i == gv.Rows.Count)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        ///  从数据库中读出记录并填充到dropdownlist  </summary>
        /// <param name="dpl">DropDownList控件名</param> 
        /// <param name="sql">SQL语句</param>   
        public static void FillDroplist(DropDownList dpl, string sql)
        {
            DataSet dsDpl = new DataSet();
            dsDpl = FillDataSet(sql);
            dpl.Items.Clear();
            dpl.Items.Add(new ListItem("-请选择-", "-请选择-"));
            if (dsDpl.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsDpl.Tables[0].Rows.Count; i++)
                {
                    dpl.Items.Add(new ListItem(dsDpl.Tables[0].Rows[i][0].ToString(), dsDpl.Tables[0].Rows[i][1].ToString()));
                }
            }
        }


        ///<summary>以分块的方式下载文件</summary>
        ///<param name="fileName">客户端保存的文件名</param>
        ///<param name="filePath">文件路径，如Server.MapPath("DownLoad/aaa.txt")</param>
        public static void FileDown_Buff(string fileName, string filePath)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

            if (fileInfo.Exists == true)
            {
                const long ChunkSize = 102400;//100K 每次读取文件，只读取100Ｋ，这样可以缓解服务器的压力
                byte[] buffer = new byte[ChunkSize];

                HttpContext.Current.Response.Clear();
                System.IO.FileStream iStream = System.IO.File.OpenRead(filePath);
                long dataLengthToRead = iStream.Length;//获取下载的文件总大小
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName));
                while (dataLengthToRead > 0 && HttpContext.Current.Response.IsClientConnected)
                {
                    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                    HttpContext.Current.Response.OutputStream.Write(buffer, 0, lengthRead);
                    HttpContext.Current.Response.Flush();
                    dataLengthToRead = dataLengthToRead - lengthRead;
                }
                HttpContext.Current.Response.Close();
            }
        }

        ///<summary>用于水晶报表导出，以分块的方式下载文件,并将服务器上文件删除</summary>
        ///<param name="fileName">客户端保存的文件名</param>
        ///<param name="filePath">文件路径，如Server.MapPath("DownLoad/aaa.txt")</param>
        public static void FileDown_Buff_Rpt(string fileName, string filePath)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

            if (fileInfo.Exists == true)
            {
                const long ChunkSize = 102400;//100K 每次读取文件，只读取100Ｋ，这样可以缓解服务器的压力
                byte[] buffer = new byte[ChunkSize];

                HttpContext.Current.Response.Clear();
                System.IO.FileStream iStream = System.IO.File.OpenRead(filePath);
                long dataLengthToRead = iStream.Length;//获取下载的文件总大小
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName));
                while (dataLengthToRead > 0 && HttpContext.Current.Response.IsClientConnected)
                {
                    int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
                    HttpContext.Current.Response.OutputStream.Write(buffer, 0, lengthRead);
                    HttpContext.Current.Response.Flush();
                    dataLengthToRead = dataLengthToRead - lengthRead;
                }
                HttpContext.Current.Response.Close();
                iStream.Close();
                File.Delete(filePath);
            }
        }

        ///<summary>以流方式的方式下载文件</summary>
        ///<param name="fileName">客户端保存的文件名</param>
        ///<param name="filePath">文件路径，如Server.MapPath("DownLoad/aaa.txt")</param>
        public static void FileDown_byte(string fileName, string filePath)
        {
            //以字符流的形式下载文件
            FileStream fs = new FileStream(filePath, FileMode.Open);
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;  filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.BinaryWrite(bytes);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        ///  用户注册或登陆后获取用户某个字段信息  </summary>
        /// <param name="ComText">SQL语句</param>    
        /// <returns>根据sql语句得到的字段信息</returns>
        public static string GetFieldValue(string ComText)
        {
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            SqlCommand cmd = new SqlCommand(ComText, sqlConn);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            openConn(sqlConn);
            sda.Fill(ds, "table");
            closeConn(sqlConn);
            string i;
            if (ds.Tables[0].Rows.Count > 0)
            {
                i = (ds.Tables[0].Rows[0][0].ToString());
            }
            else
            {
                i = "";
            }
            return i;
        }


        //通过cookie记录用户信息
        public static void Cookie(String userName)
        {
            HttpCookie cookie = new HttpCookie("user");
            cookie.Value = userName;
            cookie.Expires = DateTime.Now.AddHours(5);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        //通过cookie记录管理员信息
        public static void Mcookie(String userName, String roleId)
        {
            HttpCookie cookie = new HttpCookie("manager");
            cookie.Values["userName"] = userName;
            cookie.Values["roleId"] = roleId;
            cookie.Expires = DateTime.Now.AddHours(5);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        public static int DeletePJByPj_ID(string pj_ID)
        {
            string conStr = DBCallCommon.GetStringValue("connectionStrings");
            SqlConnection conn = new SqlConnection(conStr);
            SqlCommand sc = new SqlCommand();
            sc.CommandType = CommandType.Text;
            sc.Connection = conn;
            sc.CommandText = "DELETE FROM TBPM_PJINFO WHERE   PJ_ID = '" + pj_ID + "'";
            openConn(conn);
            int rowEffected = sc.ExecuteNonQuery();
            closeConn(conn);
            return rowEffected;
        }

        /// <summary>
        /// 通过工程ID删除工程 by ZJH  2011/3/19
        /// </summary>
        /// <param name="pj_ID"></param>
        /// <returns></returns>
        public static int DeleteENGByENG_ID(string eng_ID)
        {
            string conStr = DBCallCommon.GetStringValue("connectionStrings");
            SqlConnection conn = new SqlConnection(conStr);
            SqlCommand sc = new SqlCommand();
            sc.CommandType = CommandType.Text;
            sc.Connection = conn;
            sc.CommandText = "DELETE FROM TBPM_ENGINFO WHERE   ENG_ID = '" + eng_ID + "'";
            openConn(conn);
            int rowEffected = sc.ExecuteNonQuery();
            closeConn(conn);
            return rowEffected;
        }
        /// <summary>
        /// Session丢失时调转到登陆页
        /// </summary>
        public static void SessionLostToLogIn(object sessionUserID)
        {
            if (sessionUserID == null || sessionUserID.ToString() == "")
            {
                System.Web.HttpContext.Current.Response.Write("<script>alert('会话已过期,请重新登录！！！');if(window.parent!=null)window.parent.location.href='../Default.aspx';else{window.location.href='./Default.aspx'} </script>");
            }
        }

        public static void BindAJAXCombox(AjaxControlToolkit.ComboBox ddl, string sqlText,
                   string dataText, string dataValue)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddl.DataSource = dt;
            ddl.DataTextField = dataText;
            ddl.DataValueField = dataValue;
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddl.SelectedIndex = 0;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="to">收件箱地址，入sunlibo@sinoma-tj.cn</param>
        /// <param name="ccs">抄送人地址</param>
        /// <param name="bccs">密件抄送人地址</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="body">邮件内容</param>
        /// <returns></returns>
        /// 

        public static string SendEmail(string to, List<string> ccs, List<string> bccs, string subject, string body)
        {
            try
            {

                MailMessage email = new MailMessage();

                MailAddress fromAddress = new MailAddress("erp@ztsm.net", "数字管理平台");

                //MailAddress toAddress = new MailAddress(txtReceiver.Text.Trim(), "");

                // Set the properties of the MailMessage to the
                // values on the form


                email.IsBodyHtml = false;


                email.From = fromAddress;

                email.To.Add(to);

                #region 抄送

                // CC and BCC optional
                // MailAddressCollection class is used to send the email to various users
                // You can specify Address as new MailAddress("admin1@yoursite.com")
                if (ccs != null)
                {
                    foreach (string cc in ccs)
                    {
                        if (cc != "")
                            email.CC.Add(cc);
                    }
                }

                // You can specify Address directly as string
                if (bccs != null)
                {
                    foreach (string bcc in bccs)
                    {
                        if (bcc != "")
                            email.Bcc.Add(new MailAddress(bcc));
                    }
                }

                #endregion



                email.Subject = subject;

                email.Body = body + "\r\n\r\n******************************************************" + "\r\n" +
                            "这是一封系统自动发送的邮件通知，请不要直接回复，如有疑问，请联系开发人员。" + "\r\n" +
                            "\r\n祝 工作愉快！";




                // Set the SMTP server and send the email

                SmtpClient smtpClient = new SmtpClient();

                smtpClient.Host = "10.11.11.3";

                smtpClient.Port = 25;

                smtpClient.Credentials = new System.Net.NetworkCredential("erp@ztsm.net", "123456");

                //测试需要，将邮件服务注释,正式部署时请将下行反注释
                //smtpClient.Send(email);

                return "邮件已发送!";

            }
            catch (Exception ex)
            {
                return "邮件发送失败!";
            }

        }
        /// <summary>
        /// 根据用户ID获取邮件地址
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static string GetEmailAddressByUserID(string userid)
        {
            string retEmail = "";
            string sql = "SELECT [EMAIL] FROM [dbo].[TBDS_STAFFINFO] WHERE [ST_ID]='" + userid + "'";
            SqlDataReader dr_email = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr_email.HasRows)
            {
                dr_email.Read();
                retEmail = dr_email["EMAIL"].ToString();
            }
            dr_email.Close();
            return retEmail;
        }

        // 发送邮件第二种类型

        public static string dobSendEmail(List<string> to, List<string> ccs, List<string> bccs, string subject, string body)
        {
            try
            {

                MailMessage email = new MailMessage();

                MailAddress fromAddress = new MailAddress("erp@ztsm.net", "PMS项目管理系统");

                //MailAddress toAddress = new MailAddress(txtReceiver.Text.Trim(), "");

                // Set the properties of the MailMessage to the
                // values on the form


                email.IsBodyHtml = false;


                email.From = fromAddress;

                #region 发送
                if (to != null)
                {
                    foreach (string t in to)
                    {
                        if (t != "")
                            email.To.Add(t);
                    }
                }
                #endregion

                #region 抄送

                // CC and BCC optional
                // MailAddressCollection class is used to send the email to various users
                // You can specify Address as new MailAddress("admin1@yoursite.com")
                if (ccs != null)
                {
                    foreach (string cc in ccs)
                    {
                        if (cc != "")
                            email.CC.Add(cc);
                    }
                }

                // You can specify Address directly as string
                if (bccs != null)
                {
                    foreach (string bcc in bccs)
                    {
                        if (bcc != "")
                            email.Bcc.Add(new MailAddress(bcc));
                    }
                }

                #endregion



                email.Subject = subject;

                email.Body = body + "\r\n\r\n******************************************************" + "\r\n" +
                            "这是一封系统自动发送的邮件通知，请不要直接回复，如有疑问，请联系开发人员。" + "\r\n" +
                            "\r\n祝 工作愉快！";




                // Set the SMTP server and send the email

                SmtpClient smtpClient = new SmtpClient();

                smtpClient.Host = "10.6.0.1";

                smtpClient.Port = 25;

                smtpClient.Credentials = new System.Net.NetworkCredential("erp@ztsm.net", "123456");


                //测试需要，将邮件服务注释,正式部署时请将下行反注释
                //smtpClient.Send(email);

                return "邮件已发送!";

            }
            catch (Exception ex)
            {
                return "邮件发送失败!";
            }

        }
        /// <summary>
        /// 获取综合办公室查看权限
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static DataTable GetPermeision(int type, string StId)
        {
            string sql = "select DepId as DEP_CODE,DepName as DEP_NAME from OM_VIEWPERMEISION as a left join OM_VIEWPERMEISIONDEP as b on a.pId=b.pId where a.pType='" + type.ToString() + "' and b.StId='" + StId.ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count == 0)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "100".ToString();
                dr[1] = "请选择".ToString();
                dt.Rows.Add(dr);

            }
            return dt;
        }

    }
}
