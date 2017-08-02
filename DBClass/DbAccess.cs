using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Net;

/// <summary>
/// DbAccess 的摘要说明
/// </summary>
public class DbAccess
{

    //SqlConnection con = new SqlConnection("Data Source=192.168.10.44;Database=TS;user Id=sa;password=123456;");
    SqlConnection con = new SqlConnection();
    SqlCommand cmd;
    SqlDataAdapter sda;
    
	public DbAccess()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public void fillCheckBoxList(CheckBoxList cbl, string sql)
    {
        DataSet dsDpl = new DataSet();
        dsDpl = fillDataSet(sql);
        cbl.Items.Clear();

        if (dsDpl.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsDpl.Tables[0].Rows.Count; i++)
            {
                cbl.Items.Add(new ListItem(dsDpl.Tables[0].Rows[i][0].ToString(), dsDpl.Tables[0].Rows[i][1].ToString()));
            }
        }
    }
    public void fillRadioButtonList(RadioButtonList rbl, string sql)
    {
        DataSet dsDpl = new DataSet();
        dsDpl = fillDataSet(sql);
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
    ///   执行SQL语句，返回结果DataReader   </summary>
    /// <param name="comText">SQL语句</param> 
    public SqlDataReader getDRExSQL(string comText)
    {
        openCon();
        cmd = new SqlCommand(comText, con);
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return dr;
    }
	public DataSet GetSqlDataSet(string sqlTest,string strName)
    {
        openCon();
        DataSet dts = new DataSet();
        sda = new SqlDataAdapter(sqlTest, con);
        sda.Fill(dts,strName);//strName 标记DataSet中Tables中的表名，比如Tables["strName"]
        closeCon();

        return dts;

    }
    /// <summary>
    ///  从数据库中读出记录并填充到repeater  </summary>
    /// <param name="dpl">Repeater控件名</param> 
    /// <param name="sql">SQL语句</param>     
    public void fillRepeater(Repeater rep, string sqlText)
    {
        DataSet dsDpl = new DataSet();
        dsDpl = fillDataSet(sqlText);
        rep.DataSource = dsDpl;
        rep.DataBind();
        //dpl.Items.Clear();
        //dpl.Items.Add("-请选择-");
        //if (dsDpl.Tables[0].Rows.Count > 0)
        //{
        //    for (int i = 0; i < dsDpl.Tables[0].Rows.Count; i++)
        //    {
        //        dpl.Items.Add("" + dsDpl.Tables[0].Rows[i][0].ToString() + "");
        //    }
        //}
    }
    
    //返回DataTable
    public DataTable GetSqlDataTable(string sqlText)
    {
        DataTable dt = new DataTable();
        openCon();
        cmd = new SqlCommand(sqlText, con);        
        dt = GetDataTableUsingCmd(cmd);

        return dt;
    }

    public DataTable GetDataTableUsingCmd(SqlCommand cmd)
    {
        return GetDataTableUsingCmd(cmd, 0);
    }

    public DataTable GetDataTableUsingCmd(SqlCommand cmd, int nTableIndex)
    {
        sda = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();

        sda.Fill(ds);
        sda.Dispose();
        closeCon();
        return ds.Tables[nTableIndex];
    }

    //打开数据库连接
    public void openCon()
    {
        string constr = System.Configuration.ConfigurationManager.AppSettings["connectionStrings"];
        con.ConnectionString = constr;
        if (con.State == ConnectionState.Closed)
        {
            con.Open();
        }
        else
        {
            return;
        }
    }

    // 关闭数据库连接    
    public void closeCon()
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
        }
        else
        {
            return;
        }
    }

    /// <summary>
    ///   执行SQL语句，不返回结果   </summary>
    /// <param name="comText">SQL语句</param>  
    public void exSQLv(string comText)
    {
        openCon();
        cmd = new SqlCommand(comText, con);
        cmd.ExecuteNonQuery();
        closeCon();
    }

    // 执行存储过程
    // <param name="comText">需要执行的存储过程名称</param>
    public void exStorproceduce(string comText)
    {
        openCon();
        cmd = con.CreateCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = comText;
        cmd.ExecuteNonQuery();
        closeCon();
    }

    /// <summary> 执行SQL语句并返回受影响的行 </summary>
    /// <param name="comText">SQL语句</param>  
    /// <returns>影响行数</returns>   
    public int exSQLr(string comText)
    {
        int i;
        openCon();
        cmd = new SqlCommand(comText, con);
        i = cmd.ExecuteNonQuery();
        closeCon();
        return i;
    }

    /// <summary>
    ///  填充Dataset  </summary>
    /// <param name="ComText">SQL语句</param>   
    public void fillDs(String comText, String tableName)
    {
        sda = new SqlDataAdapter(comText, con);
        DataSet ds = new DataSet();
        ds.Tables.Clear();
        ds.Clear();
        openCon();
        sda.Fill(ds, tableName);
        closeCon();
    }

    /// <summary>
    /// 根据comtext查询数据库，返回找到记录条数     </summary>
    /// <param name="ComText">SQL语句</param>    
    /// <returns>影响行数</returns>
    public object exQueryCommand(string ComText)
    {
        openCon();
        cmd = new SqlCommand(ComText, con);
        object i = cmd.ExecuteScalar();
        closeCon();
        return i;
    }

    /// <summary>
    ///  用户注册或登陆后获取用户某个字段信息  </summary>
    /// <param name="ComText">SQL语句</param>    
    /// <returns>根据sql语句得到的字段信息</returns>
    public string getFieldValue(string ComText)
    {
        cmd = new SqlCommand(ComText, con);
        sda = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        openCon();
        sda.Fill(ds, "table");
        closeCon();
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

    /// <summary>  
    /// 根据comText查询数据库,将结果填入DataSet并返回 </summary>
    /// <param name="ComText">SQL语句</param>    
    /// <returns>填充后的dataset</returns>
    public DataSet fillDataSet(String comText)
    {
        openCon();
        sda = new SqlDataAdapter(comText, con);
        DataSet ds = new DataSet();
        ds.Tables.Clear();       
        sda.Fill(ds);
        closeCon();
        return ds;
    }

    /// <summary>
    ///  将Dataset中的数据绑定到GridView中  </summary>
    /// <param name="ds">dataset</param> 
    /// <param name="gv">gridview</param>    
    public void bindGridView(DataSet ds, GridView gv)
    {
        gv.DataSource = ds;
        gv.DataBind();
    }

    /// <summary>
    /// 创建树      </summary>
    /// <param name="entTree">TreeView</param>
    /// <param name="dsTree">DataSet</param>   
    public void bindTree(TreeView entTree, DataSet dsTree)
    {
        entTree.Nodes.Clear();
        TreeNode root = new TreeNode();
        root.Text = dsTree.Tables[0].Rows[0]["name"].ToString();
        root.Value = dsTree.Tables[0].Rows[0]["id"].ToString();
        root.Expanded = true;
        root.ImageUrl = "~/images/folder.bmp";
        entTree.Nodes.Add(root);
        int rootId = Convert.ToInt16(root.Value.ToString());
        bindChild(rootId, root, dsTree);
    }

    //创建树根子节点   
    public void bindChild(int curId, TreeNode parentNode, DataSet dsTree)
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
            bindChild(parentId, tempNode, dsTree);
        }
    }  

    /// </summary>   上传文件并返回文件名  </summary>
    /// <param name="file">HtmlInputFile控件</param>
    /// <param name="Path">绝对路径.如:Server.MapPath(@"Image/upload")与Server.MapPath(@"Image/upload/")均可,用\符号亦可</param>
    /// <returns>返回的文件名即上传后的文件名</returns>
    public string Savefile(FileUpload file, String path)
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
    public void saveFile(FileUpload file, String fileName, String path)
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
    public void deleteFile(String name)
    {
        if (System.IO.File.Exists(name))
        {
            System.IO.File.Delete(name);
        }
    }

    /// <summary>   移动文件  </summary>    
    /// <param name="name">文件绝对路径.如:Server.MapPath(@"Image/upload")与Server.MapPath(@"Image/upload/")均可,用\符号亦可</param>
    public void moveFile(String oldPath, String newPath)
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
    public bool isSelected(GridView gv, string ID)
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
    public void fillDroplist(DropDownList dpl, string sql)
    {
        DataSet dsDpl = new DataSet();      
        dsDpl = fillDataSet(sql);
        dpl.Items.Clear();
        dpl.Items.Add(new ListItem("-请选择-", "-请选择-"));
        if (dsDpl.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsDpl.Tables[0].Rows.Count; i++)
            {
                dpl.Items.Add(new ListItem(dsDpl.Tables[0].Rows[i][0].ToString(),dsDpl.Tables[0].Rows[i][1].ToString()));              
            }
        }
    }

    public void fillDroplistSame(DropDownList dpl, string sql)
    {
        DataSet dsDpl = new DataSet();
        dsDpl = fillDataSet(sql);
        dpl.Items.Clear();
        dpl.Items.Add("-请选择-");
        if (dsDpl.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsDpl.Tables[0].Rows.Count; i++)
            {
                dpl.Items.Add("" + dsDpl.Tables[0].Rows[i][0].ToString() + "");
            }
        }
    } 

    ///<summary>以分块的方式下载文件</summary>
    ///<param name="fileName">客户端保存的文件名</param>
    ///<param name="filePath">文件路径，如Server.MapPath("DownLoad/aaa.txt")</param>
    public void fileDown_Buff(string fileName, string filePath)
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
    public void fileDown_Buff_Rpt(string fileName, string filePath)
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
    public void fileDown_byte(string fileName, string filePath)
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

  

    //通过cookie记录用户信息
    public void cookie(String userName)
    {
        HttpCookie cookie = new HttpCookie("user");
        cookie.Value = userName;
        cookie.Expires = DateTime.Now.AddHours(5);
        HttpContext.Current.Response.AppendCookie(cookie);
    }

    //通过cookie记录管理员信息
    public void mcookie(String userName, String roleId)
    {
        HttpCookie cookie = new HttpCookie("manager");
        cookie.Values["userName"] = userName;
        cookie.Values["roleId"] = roleId;
        cookie.Expires = DateTime.Now.AddHours(5);
        HttpContext.Current.Response.AppendCookie(cookie);
    }

}
