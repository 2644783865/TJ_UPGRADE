using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_SERVICE : System.Web.UI.Page
    {
        string id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserID.Value = Session["UserID"].ToString();
                if (Request.QueryString["id"] != null)
                {
                    id = Request.QueryString["id"];
                }
                if (Request.QueryString["action"] == "add")
                {
                    HiddenFieldContent.Value = System.Guid.NewGuid().ToString();
                    AddGridViewFiles.Columns[3].Visible = false;
                }
                else if (Request.QueryString["action"] == "look")
                {
                    ShowData();
                    btnsubmit.Visible = false;
                    Label1.Visible = false;
                    Label2.Visible = true;
                    FileUpload1.Visible = false;
                    btnAddFU.Visible = false;
                    ReadOnly(Panel1);
                    string sqlTxt = "select CM_CONTEXT from TBCM_RECORD where CM_ID='" + id + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlTxt);
                    HiddenFieldContent.Value = dt.Rows[0]["CM_CONTEXT"].ToString();
                    GVBind(AddGridViewFiles);
                    AddGridViewFiles.Columns[2].Visible = false;
                }
                else if (Request.QueryString["action"] == "edit")
                {
                    ShowData();
                    string sqlTxt = "select CM_CONTEXT from TBCM_RECORD where CM_ID='" + id + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlTxt);
                    HiddenFieldContent.Value = dt.Rows[0]["CM_CONTEXT"].ToString();
                    GVBind(AddGridViewFiles);
                }
            }
        }

        private void ShowData()
        {
            string sqlTxt = "select * from TBCM_RECORD where CM_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlTxt);
            DataRow dr = dt.Rows[0];
            foreach (Control item in Panel1.Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).Text = dr[item.ID].ToString();
                }
            }
        }

        protected void ReadOnly(Panel pal)
        {
            foreach (Control control in pal.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).ReadOnly = true;
                }
            }
        }

        #region 上传文件

        private static int IntIsUF = 0;
        /// <summary>
        /// 重点在于要给合同文本内容赋值BC_CONTEXT
        /// </summary>
        private void UpFile()
        {
            //获取文件保存的路径
            // @"F:\质量部附件\" + Convert.ToString(System.DateTime.Now.Year)
            string FilePath = @"E:\市场管理附件\" + Convert.ToString(System.DateTime.Now.Year);

            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            //    //对客户端已上载的单独文件的访问
            HttpPostedFile UserHPF = FileUpload1.PostedFile;
            try
            {
                string fileContentType = UserHPF.ContentType;// 获取客户端发送的文件的 MIME 内容类型   
                if (fileContentType == "application/vnd.ms-excel" || fileContentType == "application/msword" || fileContentType == "application/pdf" || fileContentType == "application/octet-stream" || fileContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || fileContentType == "image/pjpeg")//传送文件类型
                {
                    if (UserHPF.ContentLength > 0)
                    {
                        //调用GetAutoID方法获取上传文件自动编号
                        //int IntFieldID = CC.GetAutoID("fileID", "tb_files");
                        //文件的真实名（格式：[文件编号]上传文件名）
                        //用于实现上传多个相同文件时，原有文件不被覆盖
                        string strOldFile = System.IO.Path.GetFileName(UserHPF.FileName);
                        string strExtent = strOldFile.Substring(strOldFile.LastIndexOf("."));
                        string strNewFile = System.DateTime.Now.ToString("yyyyMMddHHmmss") + strExtent;

                        if (!File.Exists(FilePath + "//" + strNewFile))
                        {
                            //定义插入字符串，将上传文件信息保存在数据库中
                            string sqlStr = "insert into tb_files(BC_CONTEXT,fileLoad,fileUpDate,fileName,showName)";
                            sqlStr += "values('" + HiddenFieldContent.Value + "'";
                            sqlStr += ",'" + FilePath + "'";
                            sqlStr += ",'" + DateTime.Now.ToString("yyyy年MM月dd日") + "'";
                            sqlStr += ",'" + strNewFile + "','" + strOldFile + "')";
                            //打开与数据库的连接
                            DBCallCommon.ExeSqlText(sqlStr);
                            UserHPF.SaveAs(FilePath + "//" + strNewFile);//将上传的文件存放在指定的文件夹中
                            IntIsUF = 1;
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
            else
            {
                filesError.Visible = true;
                filesError.Text = "请选择上传文件!";
            }
        }

        protected void btnUp_Click(object sender, EventArgs e)
        {
            //执行上传文件
            UpFile();
            GVBind(AddGridViewFiles);

        }
        #endregion

        #region 删除文件、下载文件

        void GVBind(GridView gv)
        {
            string sql = "select * from tb_files where BC_CONTEXT='" + HiddenFieldContent.Value + "'";
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
            string sqlStr = "select fileload,fileName,showName from tb_files where fileID='" + gv.DataKeys[gvr.RowIndex].Value.ToString() + "'";
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
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(ds.Tables[0].Rows[0]["showName"].ToString()));
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

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] == "add")
            {
                string sql = string.Format("insert into TBCM_RECORD values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", CM_BIANHAO.Text, CM_CONTR.Text, CM_CUSNAME.Text, CM_EQUIP.Text, CM_EQUIPMAP.Text, CM_STARTTIME.Text, CM_ENDTIME.Text, CM_SERBASIC.Text, CM_RESULT.Text, CM_NOTE.Text, HiddenFieldContent.Value, UserID.Value);
                DBCallCommon.ExeSqlText(sql);
                Response.Redirect("CM_ServiceFile.aspx");
            }
            else
            {
                string sql = string.Format("update TBCM_RECORD set CM_STARTTIME='{0}',CM_ENDTIME='{1}',CM_SERBASIC='{2}',CM_RESULT='{3}',CM_NOTE='{4}',CM_BIANHAO='{5}',CM_CONTR='{6}',CM_CUSNAME='{7}',CM_EQUIP='{8}',CM_EQUIPMAP='{9}' where CM_ID={10}", CM_STARTTIME.Text, CM_ENDTIME.Text, CM_SERBASIC.Text, CM_RESULT.Text, CM_NOTE.Text, CM_BIANHAO.Text, CM_CONTR.Text, CM_CUSNAME.Text, CM_EQUIP.Text, CM_EQUIPMAP.Text, Request.QueryString["id"].ToString());
                DBCallCommon.ExeSqlText(sql);
                this.ClientScript.RegisterStartupScript(GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
            }
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] == "add")
            {
                string sql = "delete from tb_files where BC_CONTEXT='" + HiddenFieldContent.Value + "'";
                DBCallCommon.ExeSqlText(sql);
                Response.Redirect("CM_ServiceFile.aspx");
            }
            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
            }
        }
    }
}
