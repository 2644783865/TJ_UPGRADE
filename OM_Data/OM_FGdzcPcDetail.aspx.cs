using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_FGdzcPcDetail : System.Web.UI.Page
    {
        string sqltext = "";
        string action = string.Empty;
        string id = string.Empty;
        string name;
        string model;
        float num;
        string location;
        DateTime xqtime;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
                action = Request.QueryString["action"].ToString();
            if (Request.QueryString["id"] != null)
                id = Request.QueryString["id"].ToString();
            if (!IsPostBack)
            {
                if (action == "add")
                {
                    HiddenFieldContent.Value = System.DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    GetCode();
                    lblAgent.Text = Session["UserName"].ToString();
                    lblDepartment.Text = Session["UserDept"].ToString();
                    lblAddtime.Text = DateTime.Now.ToString();
                    lblAgent_id.Text = Session["UserID"].ToString();
                    CreateNewRow(3);
                }
                if (action == "mod")
                {
                    this.GetDataByID(id);
                    this.InitVar();
                    GVBind(AddGridViewFiles);
                }
            }
        }


        #region 上传文件

        private static int IntIsUF = 0;
        private void UpFile()
        {
            //获取文件保存的路径
            //string FilePath = Server.MapPath("../upfiles/") + Convert.ToString(System.DateTime.Now.Year);
            string FilePath = @"D:\数字平台\平台代码\TS_ZCZJ_DPF（2014-10-11)\ZCZJ_DPF\Assets\images\om\" + Convert.ToString(System.DateTime.Now.Year);
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            //string FilePath2 = Server.MapPath("../upfiles/") + Convert.ToString(System.DateTime.Now.Year);
            //if (!Directory.Exists(FilePath2))
            //{
            //    Directory.CreateDirectory(FilePath2);
            //}

            //对客户端已上载的单独文件的访问
            HttpPostedFile UserHPF = FileUpload1.PostedFile;
            try
            {
                string fileContentType = UserHPF.ContentType;// 获取客户端发送的文件的 MIME 内容类型
                string ss = "";
                if (fileContentType == "application/vnd.ms-excel" || fileContentType == "application/msword" || fileContentType == "application/pdf" || fileContentType == "application/octet-stream" || fileContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || fileContentType == "image/jpeg" || fileContentType == "image/gif" || fileContentType == "image/x-png")//传送文件类型
                {
                    if (UserHPF.ContentLength > 0)
                    {
                        //调用GetAutoID方法获取上传文件自动编号
                        //int IntFieldID = CC.GetAutoID("fileID", "tb_files");
                        //文件的真实名（格式：[文件编号]上传文件名）
                        //用于实现上传多个相同文件时，原有文件不被覆盖
                        string strFileTName = System.IO.Path.GetFileName(UserHPF.FileName);
                        if (!File.Exists(FilePath + "//" + strFileTName))
                        {
                            //定义插入字符串，将上传文件信息保存在数据库中
                            string sqlStr = "insert into tb_files(BC_CONTEXT,fileLoad,fileUpDate,fileName)";
                            sqlStr += "values('" + HiddenFieldContent.Value + "'";
                            sqlStr += ",'" + FilePath + "'";
                            sqlStr += ",'" + DateTime.Now.ToLongDateString() + "'";
                            sqlStr += ",'" + strFileTName + "')";
                            //打开与数据库的连接
                            DBCallCommon.ExeSqlText(sqlStr);
                            //将上传的文件存放在指定的文件夹中
                            UserHPF.SaveAs(FilePath + "//" + strFileTName);
                            //IntIsUF = 2;
                        }
                        else
                        {
                            filesError.Visible = true;
                            filesError.Text = "文件名与服务器某个合同名重名，请您核对后重新上传！";
                            //IntIsUF = 1;
                        }
                    }
                }
                else
                {
                    filesError.Visible = true;
                    filesError.Text = "文件类型不符合要求，请您核对后重新上传！";
                    //IntIsUF = 1;
                }
            }
            catch
            {
                filesError.Text = "文件上传过程中出现错误！";
                filesError.Visible = true;
                return;
            }
            if (IntIsUF == 1)
            {
                //IntIsUF = 0;
            }
            else if (IntIsUF == 0)
            {
                filesError.Visible = true;
                filesError.Text = "请选择上传文件!";
            }
        }
        private void UpFile2()
        {
            //获取文件保存的路径
            //string FilePath = Server.MapPath("../upfiles/") + Convert.ToString(System.DateTime.Now.Year);
            //string FilePath = @"E:\市场顾客服务附件\李西兴(中材重机-20140921)\TS_ZCZJ_DPF（2014-08-21)\Assets\images\om\" + Convert.ToString(System.DateTime.Now.Year);
            //if (!Directory.Exists(FilePath))
            //{
            //    Directory.CreateDirectory(FilePath);
            //}

            string FilePath = Server.MapPath("../Assets/images/om/") + Convert.ToString(System.DateTime.Now.Year);
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            //对客户端已上载的单独文件的访问
            HttpPostedFile UserHPF = FileUpload1.PostedFile;
            try
            {
                string fileContentType = UserHPF.ContentType;// 获取客户端发送的文件的 MIME 内容类型
                if (fileContentType == "application/vnd.ms-excel" || fileContentType == "application/msword" || fileContentType == "application/pdf" || fileContentType == "application/octet-stream" || fileContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || fileContentType == "image/jpeg" || fileContentType == "image/gif" || fileContentType == "image/x-png")//传送文件类型
                {
                    if (UserHPF.ContentLength > 0)
                    {
                        //调用GetAutoID方法获取上传文件自动编号
                        //int IntFieldID = CC.GetAutoID("fileID", "tb_files");
                        //文件的真实名（格式：[文件编号]上传文件名）
                        //用于实现上传多个相同文件时，原有文件不被覆盖
                        string strFileTName = System.IO.Path.GetFileName(UserHPF.FileName);
                        if (!File.Exists(FilePath + "//" + strFileTName))
                        {
                            //定义插入字符串，将上传文件信息保存在数据库中
                            string sqlStr = "insert into tb_files(BC_CONTEXT,fileLoad,fileUpDate,fileName)";
                            sqlStr += "values('" + HiddenFieldContent.Value + "'";
                            sqlStr += ",'" + FilePath + "'";
                            sqlStr += ",'" + DateTime.Now.ToLongDateString() + "'";
                            sqlStr += ",'" + strFileTName + "')";
                            //打开与数据库的连接
                            DBCallCommon.ExeSqlText(sqlStr);
                            //将上传的文件存放在指定的文件夹中
                            UserHPF.SaveAs(FilePath + "//" + strFileTName);
                            IntIsUF = 2;
                        }
                        else
                        {
                            filesError.Visible = true;
                            filesError.Text = "文件名与服务器某个合同名重名，请您核对后重新上传！";
                            IntIsUF = 1;
                        }
                    }
                }
                else
                {
                    filesError.Visible = true;
                    filesError.Text = "文件类型不符合要求，请您核对后重新上传！";
                    IntIsUF = 1;
                }
            }
            catch
            {
                filesError.Text = "文件上传过程中出现错误！";
                filesError.Visible = true;
                return;
            }
            if (IntIsUF == 1)
            {
                IntIsUF = 0;
            }
            else if (IntIsUF == 0)
            {
                filesError.Visible = true;
                filesError.Text = "请选择上传文件!";
            }
        }
        protected void btnUp_Click(object sender, EventArgs e)
        {
            //执行上传文件
            UpFile();
            UpFile2();
            GVBind(AddGridViewFiles);

        }
        #endregion

        #region 删除文件、下载文件

        void GVBind(GridView gv)
        {
            string sql = "select * from tb_files where BC_CONTEXT='" + HiddenFieldContent.Value + "' and fileload like '%ZCZJ_DPF%'";
            DataSet ds = DBCallCommon.FillDataSet(sql);
            gv.DataSource = ds.Tables[0];
            gv.DataBind();
            gv.DataKeyNames = new string[] { "fileID" };
        }

        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            //获取imgbtnDelete的ImageButton对象
            ImageButton imgbtn = (ImageButton)sender;
            //引用imgbtnDelete控件的父控件上一级控件
            GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
            GridView gv = (GridView)imgbtn.Parent.Parent.Parent.Parent;
            //获取文件真实姓名
            string sqlStr = "select fileload,fileName from tb_files where fileID='" + gv.DataKeys[gvr.RowIndex].Value.ToString() + "'";
            //在文件夹Files下，删除该文件
            DeleteTFN(sqlStr);
            string sqlDelStr = "delete from tb_files where fileID='" + gv.DataKeys[gvr.RowIndex].Value.ToString() + "'";//删除数据库中的记录
            DBCallCommon.ExeSqlText(sqlDelStr);
            GVBind(AddGridViewFiles);//删除添加的记录
            //GVBind(ViewGridViewFiles);//删除查看的记录
        }
        protected void DeleteTFN(string sqlStr)
        {
            //打开数据库
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取指定文件的路径
            string strFilePath = ds.Tables[0].Rows[0]["fileload"].ToString() + @"\" + ds.Tables[0].Rows[0]["fileName"].ToString();
            //调用File类的Delete方法，删除指定文件
            File.Delete(strFilePath);//文件不存在也不会引发异常
        }
        protected void imgbtnDF_Click(object sender, ImageClickEventArgs e)
        {
            //获取imgbtnDelete的ImageButton对象
            ImageButton imgbtn = (ImageButton)sender;
            //引用imgbtnDelete控件的父控件上一级控件
            GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
            //Response.Write("gvr");
            GridView gv = (GridView)imgbtn.Parent.Parent.Parent.Parent;
            //获取文件真实姓名
            string sqlStr = "select fileload,fileName from tb_files where fileID='" + gv.DataKeys[gvr.RowIndex].Value.ToString() + "'";
            //打开数据库
            //Response.Write(sqlStr);         
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取文件路径
            string strFilePath = ds.Tables[0].Rows[0]["fileload"].ToString() + @"\" + ds.Tables[0].Rows[0]["fileName"].ToString();
            //Response.Write(strFilePath);
            if (File.Exists(strFilePath))
            {
                System.IO.FileInfo file = new System.IO.FileInfo(strFilePath);
                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(file.Name));
                Response.AppendHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(file.FullName);
                Response.Flush();
                Response.End();
            }
            else
            {
                filesError.Visible = true;
                filesError.Text = "文件已被删除，请通知相关人员上传文件！";
            }
        }

        #endregion


        protected void GetDataByID(string id)
        {
            sqltext = "select CODE,DEPARTMENT,AGENTID,LINKMAN,PHONE,REASON,NOTE,AGENT,ADDTIME,BC_CONTEXT from TBOM_GDZCPCAPPLY where CODE='" + id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                lblCode.Text = dr["CODE"].ToString();
                lblDepartment.Text = dr["DEPARTMENT"].ToString();
                txtLinkman.Text = dr["LINKMAN"].ToString();
                txtPhone.Text = dr["PHONE"].ToString();
                txtReason.Text = dr["REASON"].ToString();
                txtNote.Text = dr["NOTE"].ToString();
                lblAgent.Text = dr["AGENT"].ToString();
                lblAddtime.Text = dr["ADDTIME"].ToString();
                lblAgent_id.Text = dr["AGENTID"].ToString();
                HiddenFieldContent.Value = dr["BC_CONTEXT"].ToString();
            }
            dr.Close();
            sqltext = "select NAME,MODEL,NUM,LOCATION,XQTIME from TBOM_GDZCPCAPPLY where CODE='" + id + "'";
            DBCallCommon.BindGridView(GridView1, sqltext);
        }
        private void GetCode()
        {
            sqltext = "select TOP 1 dbo.GetIndex(Code) AS TopIndex from TBOM_GDZCPCAPPLY where PCTYPE='1' ORDER BY dbo.GetIndex(Code) DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int index;
            if (dt.Rows.Count > 0)
            {
                index = Convert.ToInt16(dt.Rows[0]["TopIndex"].ToString());
            }
            else
            {
                index = 0;
            }
            string code = (index + 1).ToString();
            lblCode.Text = "FGDZC" + code.PadLeft(4, '0');
            sqltext = "insert into TBOM_GDZCPCAPPLY (CODE) values('" + lblCode.Text.Trim() + "')";
            DBCallCommon.ExeSqlText(sqltext);
        }
        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(txtLines.Text.Trim());
            CreateNewRow(num);
        }
        private void CreateNewRow(int num)
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
            InitVar();
        }
        private void InitVar()
        {
            if (GridView1.Rows.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            else
            {
                NoDataPanel.Visible = false;
            }
        }
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NAME");
            dt.Columns.Add("MODEL");
            dt.Columns.Add("NUM");
            dt.Columns.Add("LOCATION");
            dt.Columns.Add("XQTIME");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)gr.FindControl("txtName")).Text;
                newRow[1] = ((TextBox)gr.FindControl("txtModel")).Text;
                newRow[2] = ((TextBox)gr.FindControl("txtNum")).Text;
                newRow[3] = ((TextBox)gr.FindControl("txtLocation")).Text;
                newRow[4] = ((TextBox)gr.FindControl("txtXqtime")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }
        protected void btnDelRow_OnClick(object sender, EventArgs e)
        {
            int count = 0;
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                CheckBox chk = (CheckBox)gr.FindControl("chk");
                if (chk.Checked)
                {
                    dt.Rows.RemoveAt(i - count);
                    count++;
                }
            }
            if (count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要删除的行！');", true);
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }
        private void writedata()
        {
            string context = HiddenFieldContent.Value.ToString();
            List<string> list_sql = new List<string>();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                name = ((TextBox)gr.FindControl("txtName")).Text.Trim();
                if (name != "")
                {
                    model = ((TextBox)gr.FindControl("txtModel")).Text.Trim();
                    //num = ((TextBox)gr.FindControl("txtNum")).Text.Trim();
                    location = ((TextBox)gr.FindControl("txtLocation")).Text.Trim();
                    //xqtime = ((TextBox)gr.FindControl("txtXqtime")).Text.Trim();
                    if (!DateTime.TryParse(((TextBox)gr.FindControl("txtXqtime")).Text.Trim(),out xqtime))
                    {
                        Response.Write("<script>alert('需求时间填写格式有误!')</script>");
                        return;
                    }
                    if (!float.TryParse(((TextBox)gr.FindControl("txtNum")).Text.Trim(), out num))
                    {
                        Response.Write("<script>alert('数量填写格式有误!')</script>");
                        return;
                    }
                    if (model == "" || num.ToString() == "" || location == "" || xqtime.ToString() == "")
                    {
                        Response.Write("<script>alert('第" + (i + 1) + "行数据不能为空！')</script>");
                        return;
                    }
                    sqltext = "insert into TBOM_GDZCPCAPPLY(CODE,DEPARTMENT,LINKMAN,PHONE,NAME,MODEL,NUM,LOCATION,XQTIME,REASON,NOTE,AGENT,AGENTID,ADDTIME,STATE,BC_CONTEXT,PCTYPE)";
                    sqltext += "values('" + lblCode.Text + "','" + lblDepartment.Text + "','" + txtLinkman.Text.Trim() + "','" + txtPhone.Text.Trim() + "','" + name + "','" + model + "','" + num + "','" + location + "','" + xqtime.ToString() + "','" + txtReason.Text.Trim() + "','" + txtNote.Text.Trim() + "','" + lblAgent.Text + "','" + lblAgent_id.Text.Trim() + "','" + lblAddtime.Text + "',0,'" + context + "','1')";
                    list_sql.Add(sqltext);
                }
                //if (name == "")
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请添加物品名称！');", true);
                //    return;

                //}
            }
            sqltext = "insert into TBOM_GDZCRVW (CODE,STATUS) values('" + lblCode.Text.Trim() + "','0')";
            list_sql.Add(sqltext);

            DBCallCommon.ExecuteTrans(list_sql);
            Response.Write("<script>alert('保存成功！');window.location.href='OM_FGdzcPcPlan.aspx';</script>");
        }
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            string st = "OK";
            if (GridView1.Rows.Count == 0)
            {
                st = "NoData";
            }
            if (st == "OK")
            {
                if (action == "add")
                {
                    sqltext = "delete from TBOM_GDZCPCAPPLY where CODE='" + lblCode.Text.Trim() + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    writedata();
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功,点击查看生成订单！');window.location.href='OM_GdzcPcPlan.aspx'", true);

                }
                else if (action == "mod")
                {
                    sqltext = "delete from TBOM_GDZCPCAPPLY where CODE='" + id + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    sqltext = "delete from TBOM_GDZCRVW where CODE='" + id + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    writedata();
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('修改成功,点击查看生成订单！');window.location.href='OM_GdzcPcPlan.aspx'", true);
                }
            }
            else if (st == "NoData")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！没有数据！！！');", true);
            }
        }
        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("OM_FGdzcPcPlan.aspx");
        }
    }
}
