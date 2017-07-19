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
    public partial class CM_CustoInList : System.Web.UI.Page
    {
        public string declare;
        string id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["CM_ID"];
            if (!IsPostBack)
            {
                for (int i = 0; i < CM_TEST.Items.Count; i++)
                {
                    this.CM_TEST.Items[i].Attributes.Add("onclick", "CheckBoxList_Click(this)");
                }
                ShowDate();
                if (hdzj.Value == Session["UserID"].ToString())
                {
                    CM_BTIN.Enabled = false;
                    CM_KGDATE.Enabled = false;
                    CM_RKYJ.Enabled = false;
                    btnsubmit.Visible = true;
                }
                else
                {
                    if (inKeep == Session["UserID"].ToString() && CM_CHECK.SelectedValue == "1")
                    {
                        CM_TEST.Enabled = false;
                        CM_CHECK.Enabled = false;
                        CM_ZJDATE.Enabled = false;
                        btnsubmit.Visible = true;
                       
                    }
                    else
                    {
                        panel1.Enabled = false;
                    }
                }
                ST_ZJ.ReadOnly = true;
                ST_KG.ReadOnly = true;
            }
        }

        string inKeep;
        private void ShowDate()
        {
            string sql = "select a.*,CM_PIC+' '+CM_EQUIP as CM_INCONT,b.ST_NAME as ST_ZJ,c.ST_NAME as ST_KG from TBCM_CUSTOMER as a left join TBDS_STAFFINFO as b on b.ST_ID=a.CM_ZJYUAN left join TBDS_STAFFINFO as c on c.ST_ID=a.CM_KGYUAN where CM_ID='" + Request.QueryString["CM_ID"] + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Show(panel, dr);
                Show(panel1, dr);
                inKeep = dr["CM_INKEEP"].ToString();
                CM_BIANHAO.Text = dr["CM_BIANHAO"].ToString();
                hdzj.Value = dr["CM_ZJYUAN"].ToString();
                hdkg.Value = dr["CM_KGYUAN"].ToString();
                declare = dr["CM_RESULT"].ToString();
                Hid_ZJ.Value = declare;
                HiddenFieldContent.Value = dr["CM_GUID"].ToString();
                psr.Value = dr["CM_PSR"].ToString();
                zdr.Value = dr["CM_MANCLERK"].ToString();
                ReadOnly(panel);
                if (dr["CM_MANCLERK"].ToString() != Session["UserID"].ToString())
                {
                    FileUp.Visible = false;
                    btnAddFU.Visible = false;
                    GridView.Columns[2].Visible = false;
                }
                else
                {
                    btnedit.Visible = true;
                    CM_ZDYJ.ReadOnly = false;
                }
                GVBind(GridView);
                BindrptGKCC_ZJ();
            }
        }

        private void Show(Panel panel, DataRow dr)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = dr[control.ID].ToString();
                }
                if (control is CheckBoxList)
                {
                    ((CheckBoxList)control).SelectedValue = dr[control.ID].ToString();
                }
                if (control is CheckBox)
                {
                    if (dr[control.ID].ToString() == "1")
                    {
                        ((CheckBox)control).Checked = true;
                    }
                }
            }
            CM_CHECK.SelectedValue = dr["CM_CHECK"].ToString();
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

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string sql1;
            string sql2;
            List<string> list = new List<string>();
            if (CM_CHECK.SelectedValue != "2")
            {
                sql1 = "update TBCM_CUSTOMER set CM_CHECK='" + CM_CHECK.SelectedValue + "',CM_SFHG='" + CM_CHECK.SelectedValue + "',CM_TEST='" + CM_TEST.SelectedValue + "',CM_RESULT='" + Hid_ZJ.Value + "',CM_ZJDATE='" + CM_ZJDATE.Text + "' where CM_ID='" + Request.QueryString["CM_ID"] + "'";
                list.Add(sql1);
                if (CM_CHECK.SelectedValue == "1")
                {
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(zdr.Value), new List<string>(), new List<string>(), "顾客财产质检通过", "项目为 " + CM_PJNAME.Text + ",入库内容为 " + CM_INCONT.Text + ",已质检通过，请登录系统查看");
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(hdkg.Value), new List<string>(), new List<string>(), "顾客财产质检通过", "项目为 " + CM_PJNAME.Text + ",入库内容为 " + CM_INCONT.Text + ",已质检通过，请登录系统查看");
                    if (psr.Value != "")
                    {
                        string[] strs = psr.Value.Split(',');
                        for (int i = 0; i < strs.Length; i++)
                        {
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(strs[i]), new List<string>(), new List<string>(), "顾客财产质检通过", "项目为 " + CM_PJNAME.Text + ",入库内容为 " + CM_INCONT.Text + ",已质检通过，请登录系统查看");
                        }
                    }
                }
                else if (CM_CHECK.SelectedValue == "0")
                {
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(zdr.Value), new List<string>(), new List<string>(), "顾客财产质检未通过", "项目为 " + CM_PJNAME.Text + ",入库内容为 " + CM_INCONT.Text + ",未通过质检，请登录系统查看");
                }
            }
            if (CM_BTIN.Checked)
            {
                sql2 = "update TBCM_CUSTOMER set CM_BTIN='1',CM_RKYJ='" + CM_RKYJ.Text + "' where CM_ID='" + Request.QueryString["CM_ID"] + "'";
                list.Add(sql2);
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(zdr.Value), new List<string>(), new List<string>(), "顾客财产已入库", "项目为 " + CM_PJNAME.Text + ",入库内容为 " + CM_INCONT.Text + ",已入库通过，请登录系统查看");
            }
            DBCallCommon.ExecuteTrans(list);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('信息修改成功！');window.opener=null;window.open('','_self');window.close();", true);
        }

        protected void btnedit_Click(object sender, EventArgs e)
        {
            string sql = "update TBCM_CUSTOMER set CM_ZDYJ='" + CM_ZDYJ.Text + "',CM_SFHG='" + CM_SFHG.SelectedValue + "' where CM_ID='" + Request.QueryString["CM_ID"] + "'";
            if (CM_SFHG.SelectedValue == "1")
            {
                if (psr.Value != "")
                {
                    string[] strs = psr.Value.Split(',');
                    for (int i = 0; i < strs.Length; i++)
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(strs[i]), new List<string>(), new List<string>(), "顾客财产继续使用", "项目为 " + CM_PJNAME.Text + "，入库内容为 " + CM_INCONT.Text + "，已由市场部改为合格，可继续使用，请登录系统查看");
                    }
                }
            }
            DBCallCommon.ExeSqlText(sql);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('信息修改成功！');window.opener=null;window.open('','_self');window.close();", true);
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
        }

        #region 下载文件，删除文件

        protected void GVBind(GridView gv)
        {
            string sql = "select * from tb_files where BC_CONTEXT='" + HiddenFieldContent.Value + "'";
            DataSet ds = DBCallCommon.FillDataSet(sql);
            gv.DataSource = ds.Tables[0];
            gv.DataBind();
            gv.DataKeyNames = new string[] { "fileID" };
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

        protected void DeleteTFN(string sqlStr)
        {
            //打开数据库
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取指定文件的路径
            string strFilePath = ds.Tables[0].Rows[0]["fileload"].ToString() + @"\" + ds.Tables[0].Rows[0]["fileName"].ToString();
            //调用File类的Delete方法，删除指定文件
            if (System.IO.File.Exists(strFilePath))
            {
                File.Delete(strFilePath);//文件不存在也不会引发异常
            }
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
            GVBind(GridView);//删除添加的记录
            //GVBind(ViewGridViewFiles);//删除查看的记录
        }

        #endregion

        #region 上传文件

        private static int IntIsUF = 0;
        /// <summary>
        /// 重点在于要给合同文本内容赋值BC_CONTEXT
        /// </summary>
        private void UpFile()
        {
            //获取文件保存的路径
            // @"F:\质量部附件\" + Convert.ToString(System.DateTime.Now.Year)
            string FilePath = @"E:\市场顾客财产附件\" + Convert.ToString(System.DateTime.Now.Year);

            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            //    //对客户端已上载的单独文件的访问
            HttpPostedFile UserHPF = FileUp.PostedFile;
            try
            {
                string fileContentType = UserHPF.ContentType;// 获取客户端发送的文件的 MIME 内容类型   
                if (fileContentType == "application/vnd.ms-excel" || fileContentType == "application/msword" || fileContentType == "application/pdf" || fileContentType == "application/octet-stream" || fileContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || fileContentType == "image/bmp" || fileContentType == "image/gif" || fileContentType == "image/jpeg" || fileContentType == "image/jpg" || fileContentType == "image/pjpeg")//传送文件类型
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
            GVBind(GridView);

        }

        #endregion

        #region 顾客财产_质检
        protected void btnFU_OnClick(object sender, EventArgs e)
        {
            int IntIsUF = 0;//判断用户是否上传了文件
            //获取文件保存的路径
            string FilePath = @"E:\顾客财产质检\" + Convert.ToString(System.DateTime.Now.Year);
            if (!Directory.Exists(FilePath))//如果不存在文件路径就创建一个
            {
                Directory.CreateDirectory(FilePath);
            }
            //对客户端已上载的单独文件的访问
            HttpPostedFile UserHPF = FileUpload1.PostedFile;
            try
            {
                string fileContentType = UserHPF.ContentType;// 获取客户端发送的文件的 MIME 内容类型 
                if (fileContentType == "application/vnd.ms-excel" || fileContentType == "application/msword" || fileContentType == "application/pdf" || fileContentType == "application/octet-stream" || fileContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || fileContentType == "image/bmp" || fileContentType == "image/gif" || fileContentType == "image/jpeg" || fileContentType == "image/jpg" || fileContentType == "image/pjpeg")//传送文件类型
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
                            string sqlStr = "insert into CM_FILES(FILE_FATHERID,FILE_LOAD,FILE_UPDATE,FILE_NAME,FILE_SHOWNAME,FILE_TYPE)";
                            sqlStr += "values('" + id + "'";
                            sqlStr += ",'" + FilePath + "'";
                            sqlStr += ",'" + DateTime.Now.ToString("yyyy年MM月dd日") + "'";
                            sqlStr += ",'" + strNewFile + "','" + strOldFile + "',";
                            sqlStr += "'CM_GKCC_ZJ')";
                            //打开与数据库的连接
                            DBCallCommon.ExeSqlText(sqlStr);
                            UserHPF.SaveAs(FilePath + "//" + strNewFile);//将上传的文件存放在指定的文件夹中
                            IntIsUF = 1;
                        }
                        else
                        {
                            filesError1.Visible = true;
                            filesError1.Text = "文件名与服务器某个合同名重名，请您核对后重新上传！";
                            IntIsUF = 1;
                        }
                    }
                }
                else
                {
                    filesError1.Visible = true;
                    filesError1.Text = "文件类型不符合要求，请您核对后重新上传！";
                    IntIsUF = 1;
                }
            }
            catch
            {
                filesError1.Text = "文件上传过程中出现错误！";
                filesError1.Visible = true;
                return;
            }
            if (IntIsUF == 1)
            {
                IntIsUF = 0;
            }
            else
            {
                filesError1.Visible = true;
                filesError1.Text = "请选择上传文件!";
            }
            BindrptGKCC_ZJ();
        }

        private void BindrptGKCC_ZJ()
        {
            string sql = "select * from CM_FILES where FILE_FATHERID='" + id + "' and FILE_TYPE='CM_GKCC_ZJ'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            rptGKCC_ZJ.DataSource = dt;
            rptGKCC_ZJ.DataBind();
        }

        protected void lbtndelete1_OnClick(object sender, EventArgs e)
        {
            string fileid = ((LinkButton)sender).CommandArgument.ToString();
            //获取文件真实姓名
            string sqlStr = "select FILE_LOAD,FILE_NAME from CM_FILES where FILE_ID='" + fileid + "'";
            //在文件夹Files下，删除该文件
            //打开数据库
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取指定文件的路径
            string strFilePath = ds.Tables[0].Rows[0]["FILE_LOAD"].ToString() + @"\" + ds.Tables[0].Rows[0]["FILE_NAME"].ToString();
            //调用File类的Delete方法，删除指定文件
            if (System.IO.File.Exists(strFilePath))
            {
                File.Delete(strFilePath);//文件不存在也不会引发异常
            }
            string sqlDelStr = "delete from CM_FILES where FILE_ID='" + fileid + "'";//删除数据库中的记录
            DBCallCommon.ExeSqlText(sqlDelStr);
            BindrptGKCC_ZJ();//删除添加的记录
            //GVBind(ViewGridViewFiles);//删除查看的记录
        }

        protected void lbtnonload1_OnClick(object sender, EventArgs e)
        {
            string fileid = ((LinkButton)sender).CommandArgument.ToString();
            string sqlStr = "select FILE_LOAD,FILE_NAME,FILE_SHOWNAME from CM_FILES where FILE_ID='" + fileid + "'";
            //打开数据库
            //Response.Write(sqlStr);         
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取文件路径
            string strFilePath = ds.Tables[0].Rows[0]["FILE_LOAD"].ToString() + @"\" + ds.Tables[0].Rows[0]["FILE_NAME"].ToString();
            //Response.Write(strFilePath);
            if (File.Exists(strFilePath))
            {
                System.IO.FileInfo file = new System.IO.FileInfo(strFilePath);
                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(ds.Tables[0].Rows[0]["FILE_NAME"].ToString()));
                Response.AppendHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(file.FullName);
                Response.Flush();
                Response.End();
            }
            else
            {
                filesError1.Visible = true;
                filesError1.Text = "文件已被删除，请通知相关人员上传文件！";
            }
        }
        #endregion

    }
}
