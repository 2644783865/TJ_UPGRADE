﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_DriverADD : System.Web.UI.Page
    {
        string action = string.Empty;
        string id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
                action = Request.QueryString["action"].ToString();
            if (Request.QueryString["id"] != null)
                id = Request.QueryString["id"].ToString();
            if (!this.IsPostBack)
            {
                if (action == "add")
                {
                    HiddenFieldContent.Value = System.DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    //update_body.Visible = false;
                }
                else if (action == "update")
                {
                    txtDriverName.Enabled = false;
                    this.GetDataByID(id);
                    GVBind(AddGridViewFiles);
                    Showdata();

                }
            }
        }



        private void Showdata() //将数据绑定到textbox
        {
            string sqltext = "select * from TBOM_DriverInfo where Context = '" + HiddenFieldContent.Value + "'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            Det_Repeater.DataSource = dt;
            Det_Repeater.DataBind();
        }


        protected void GetDataByID(string id)
        {
            string sqltext = "select * from TBOM_DriverList as a left join View_TBDS_STAFFINFO as b on a.DriverId=ST_ID where DriverId='" + id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read())
            {
                txtDriverName.Text = dr["ST_NAME"].ToString();
              //  lblDep.Text = dr["DEP_NAME"].ToString();
                //lblPos.Text = dr["DEP_POSITION"].ToString();
                HiddenFieldContent.Value = dr["Context"].ToString();
            }
            dr.Close();
        }



        #region 上传文件

        private static int IntIsUF = 0;
        private void UpFile()
        {
            //获取文件保存的路径
            //string FilePath = Server.MapPath("../upfiles/") + Convert.ToString(System.DateTime.Now.Year);
            string FilePath = @"D:\数字平台\平台代码\TS_ZCZJ_DPF（2014-10-11)\ZCZJ_DPF\Assets\images\om\2014";
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
                if (fileContentType == "application/vnd.ms-excel" || fileContentType == "application/msword" || fileContentType == "application/pdf" || fileContentType == "application/octet-stream" || fileContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || fileContentType == "image/jpeg" || fileContentType == "image/gif" || fileContentType == "image/x-png" || fileContentType == "image/png")//传送文件类型
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

            string FilePath = Server.MapPath("../Assets/images/om/2014");
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            //对客户端已上载的单独文件的访问
            HttpPostedFile UserHPF = FileUpload1.PostedFile;
            try
            {
                string fileContentType = UserHPF.ContentType;// 获取客户端发送的文件的 MIME 内容类型
                if (fileContentType == "application/vnd.ms-excel" || fileContentType == "application/msword" || fileContentType == "application/pdf" || fileContentType == "application/octet-stream" || fileContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || fileContentType == "image/jpeg" || fileContentType == "image/gif" || fileContentType == "image/x-png" || fileContentType == "image/png")//传送文件类型
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
        #region 增加行
        protected void btnadd_Click(object sender, EventArgs e)
        {
            CreateNewRow(1);
        }



        protected void delete_Click(object sender, EventArgs e) //删除事件
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("dNAME");
            dt.Columns.Add("dYOUXIAOQI");
            dt.Columns.Add("dENDDATE");
            dt.Columns.Add("dNOTE");
            //dt.Columns.Add("ST_INDENTITY");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((TextBox)retItem.FindControl("dNAME")).Text;
                    newRow[1] = ((TextBox)retItem.FindControl("dYOUXIAOQI")).Text;
                    newRow[2] = ((TextBox)retItem.FindControl("dENDDATE")).Text;
                    newRow[3] = ((TextBox)retItem.FindControl("dNOTE")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();
        }

        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("dNAME");
            dt.Columns.Add("dYOUXIAOQI");
            dt.Columns.Add("dENDDATE");
            dt.Columns.Add("dNOTE");
            //dt.Columns.Add("ST_INDENTITY");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)retItem.FindControl("dNAME")).Text;
                newRow[1] = ((TextBox)retItem.FindControl("dYOUXIAOQI")).Text;
                newRow[2] = ((TextBox)retItem.FindControl("dENDDATE")).Text;
                newRow[3] = ((TextBox)retItem.FindControl("dNOTE")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }


        #endregion



        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
          
            string sqltext = "";
            //if (IntIsUF != 2)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请上传图片！');window.opener=null;window.open('','_self');window.close()", true);
            //}
            //else
            //{
            if (action == "update")
            {

                sqltext = "delete from TBOM_DriverInfo where Context='" + HiddenFieldContent.Value + "'";
                list_sql.Add(sqltext);
                foreach (RepeaterItem ri in Det_Repeater.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        TextBox dNAME = (TextBox)ri.FindControl("dNAME");
                        TextBox dYOUXIAOQI = (TextBox)ri.FindControl("dYOUXIAOQI");
                        TextBox dENDDATE = (TextBox)ri.FindControl("dENDDATE");
                        TextBox dNOTE = (TextBox)ri.FindControl("dNOTE");

                        string sqltxt = "insert into TBOM_DriverInfo (Context, dName, dYOUXIAOQI, dENDDATE, dNOTE) VALUES ('" + HiddenFieldContent.Value + "','" + dNAME.Text.Trim() + "','" + dYOUXIAOQI.Text.Trim() + "','" + dENDDATE.Text.Trim() + "','" + dNOTE.Text.Trim() + "')";
                        list_sql.Add(sqltxt);
                    }
                }
            }
            if (action == "add")
            {
                sqltext = "insert into TBOM_DriverList (DriverId,Context) values ('" + hidDriverId.Value + "','" + HiddenFieldContent.Value + "')";
                list_sql.Add(sqltext);
                foreach (RepeaterItem ri in Det_Repeater.Items)
                {
                    if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
                    {
                        TextBox dNAME = (TextBox)ri.FindControl("dNAME");
                        TextBox dYOUXIAOQI = (TextBox)ri.FindControl("dYOUXIAOQI");
                        TextBox dENDDATE = (TextBox)ri.FindControl("dENDDATE");
                        TextBox dNOTE = (TextBox)ri.FindControl("dNOTE");

                        string sqltxt = "insert into TBOM_DriverInfo (Context, dName, dYOUXIAOQI, dENDDATE, dNOTE) VALUES ('" + HiddenFieldContent.Value + "','" + dNAME.Text.Trim() + "','" + dYOUXIAOQI.Text.Trim() + "','" + dENDDATE.Text.Trim() + "','" + dNOTE.Text.Trim() + "')";
                        list_sql.Add(sqltxt);
                    }
                }
            }
            DBCallCommon.ExecuteTrans(list_sql);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('添加/修改档案成功！');window.opener=null;window.open('','_self');window.close();", true);
            Response.Redirect("OM_Driver.aspx");
            //}
        }
        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("OM_Driver.aspx");
        }
    }
}
