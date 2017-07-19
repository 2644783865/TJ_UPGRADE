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
    public partial class CM_SHCLD : System.Web.UI.Page
    {
        string action = string.Empty;
        string id = string.Empty;
        string username = string.Empty;
        string sqltext = string.Empty;
        DataTable dts = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];
            id = Request.QueryString["id"];
            username = Session["UserName"].ToString();
            if (action != "add")
            {
                sqltext = "select * from CM_SHCLD where CLD_ID='" + id + "'";
                try
                {
                    dts = DBCallCommon.GetDTUsingSqlText(sqltext);
                    hidCLD_SJID.Value = dts.Rows[0]["CLD_SJID"].ToString();
                }
                catch
                {
                    Response.Write("<script type='text/javascript'>alert('sqltext语句出现问题，请与管理员联系')</script>");
                    return;
                }
            }
            if (!IsPostBack)
            {
                BindData();
                IfableControl();
            }
        }

        private void BindData()
        {
            BindrptCLFA();
            BindrptYYFX();
            BindrptJBXX();
            BindrptCLYJ();
            BindrptCLJG();
            string sql = "select * from CM_SHCLD_FY where FY_CLDID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            rptFWFY.DataSource = dt;
            rptFWFY.DataBind();
            NoDataPanelSee();
            if (action == "add")
            {
                hidCLD_SJID.Value = DateTime.Now.ToString("yyyy-MM-dd:HH-mm-ss-fff");
                txtCLD_ZDR.Text = username;
                lbCLD_ZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtCLD_SPR1.Text = "李利恒";
            }
            else if (action == "alter")
            {
                BindPanel(panJBXX);
                BindPanel(pan0);
                BindPanel(panZDR);
                BindPanel(panSPR1);
            }
            else if (action == "check")
            {
                if (dts.Rows[0]["CLD_SPZT"].ToString() == "0")
                {
                    if (username == dts.Rows[0]["CLD_SPR1"].ToString())
                    {
                        BindPanel(pan0);
                        BindPanel(panJBXX);
                        BindPanel(panZDR);
                        BindPanel(panSPR1);
                        lbCLD_SPR1_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                }
                if (dts.Rows[0]["CLD_SPZT"].ToString() == "1y")
                {
                    if (username == dts.Rows[0]["CLD_SPR2"].ToString())
                    {
                        BindPanel(pan0);
                        BindPanel(panJBXX);
                        BindPanel(panYYFX);
                        BindPanel(panCLYJ);
                        BindPanel(panCLFA);
                        BindPanel(panZDR);
                        BindPanel(panSPR1);
                        BindPanel(panSPR2);
                        lbCLD_SPR2_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                }
                if (username == dts.Rows[0]["CLD_SPR4"].ToString())
                {
                    BindPanel(pan0);
                    BindPanel(panJBXX);
                    BindPanel(panYYFX);
                    BindPanel(panCLYJ);
                    BindPanel(panCLFA);
                    BindPanel(panZDR);
                    BindPanel(panSPR1);
                    BindPanel(panSPR2);
                    BindPanel(panSPR4);
                    lbCLD_SPR4_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (username == dts.Rows[0]["CLD_SPR5"].ToString())
                {
                    BindPanel(pan0);
                    BindPanel(panJBXX);
                    BindPanel(panYYFX);
                    BindPanel(panCLYJ);
                    BindPanel(panCLFA);
                    BindPanel(panZDR);
                    BindPanel(panSPR1);
                    BindPanel(panSPR2);
                    BindPanel(panSPR4);
                    BindPanel(panSPR5);
                    lbCLD_SPR5_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else if (action == "writeyy")
            {
                BindPanel(pan0);
                BindPanel(panJBXX);
                BindPanel(panYYFX);
                BindPanel(panCLYJ);
                BindPanel(panCLFA);
                BindPanel(panZDR);
                BindPanel(panSPR1);
                lbCLD_YYFX_TXSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else if (action == "writeyj")
            {
                BindPanel(pan0);
                BindPanel(panJBXX);
                BindPanel(panYYFX);
                BindPanel(panCLYJ);
                BindPanel(panCLFA);
                BindPanel(panZDR);
                BindPanel(panSPR1);
                BindPanel(panSPR2);
                lbCLD_CLYJ_TXSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else if (action == "writefa")
            {
                BindPanel(pan0);
                BindPanel(panJBXX);
                BindPanel(panYYFX);
                BindPanel(panCLYJ);
                BindPanel(panCLFA);
                BindPanel(panZDR);
                BindPanel(panSPR1);
                lbCLD_CLFA_TXSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtCLD_SPR2.Text = "李利恒";
            }
            else if (action == "writejg")
            {
                BindPanel(pan0);
                BindPanel(panJBXX);
                BindPanel(panYYFX);
                BindPanel(panCLYJ);
                BindPanel(panCLFA);
                BindPanel(panCLJG);
                BindPanel(panZDR);
                BindPanel(panSPR1);
                BindPanel(panSPR2);
                BindPanel(panSPR4);
                BindPanel(panSPR5);
                txtCLD_CLJG_TXR.Text = username;
                lbCLD_CLJG_TXSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else if (action == "count")
            {
                BindPanel(pan0);
                BindPanel(panJBXX);
                BindPanel(panYYFX);
                BindPanel(panCLYJ);
                BindPanel(panCLFA);
                BindPanel(panCLJG);
                BindPanel(panZDR);
                BindPanel(panSPR1);
                BindPanel(panSPR2);
                BindPanel(panSPR4);
                BindPanel(panSPR5);
                txtCLD_FWFY_TJR.Text = username;
                lbCLD_FWFY_TJSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else if (action == "read")
            {
                BindPanel(pan0);
                BindPanel(panJBXX);
                BindPanel(panYYFX);
                BindPanel(panCLYJ);
                BindPanel(panCLFA);
                BindPanel(panCLJG);
                BindPanel(panFYTJ);
                BindPanel(panZDR);
                BindPanel(panSPR1);
                BindPanel(panSPR2);
                BindPanel(panSPR4);
                BindPanel(panSPR5);
            }
        }

        private void BindPanel(Panel panel)
        {
            foreach (Control ctr in panel.Controls)
            {
                if (ctr is TextBox)
                {
                    TextBox txt = (TextBox)ctr;
                    if (dts.Columns.Contains(txt.ID.Substring(3)))
                    {
                        txt.Text = dts.Rows[0][txt.ID.Substring(3)].ToString();
                    }
                }
                if (ctr is Label)
                {
                    Label lb = (Label)ctr;
                    if (dts.Columns.Contains(lb.ID.Substring(2)))
                    {
                        lb.Text = dts.Rows[0][lb.ID.Substring(2)].ToString();
                    }
                }
                if (ctr is RadioButtonList)
                {
                    RadioButtonList rbl = (RadioButtonList)ctr;
                    if (dts.Columns.Contains(rbl.ID.Substring(3)))
                    {
                        rbl.SelectedValue = dts.Rows[0][rbl.ID.Substring(3)].ToString();
                    }
                }
                if (ctr is CheckBoxList)
                {
                    CheckBoxList cbxl = (CheckBoxList)ctr;
                    if (dts.Columns.Contains(cbxl.ID.Substring(4)))
                    {
                        if (dts.Rows[0][cbxl.ID.Substring(4)].ToString().Contains('|'))
                        {
                            string[] str = dts.Rows[0][cbxl.ID.Substring(4)].ToString().Split('|');
                            for (int j = 0, a = str.Length; j < a; j++)
                            {
                                for (int k = 0, b = cbxl.Items.Count; k < b; k++)
                                {
                                    if (cbxl.Items[k].Text == str[j])
                                    {
                                        cbxl.Items[k].Selected = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int k = 0, b = cbxl.Items.Count; k < b; k++)
                            {
                                if (cbxl.Items[k].Text == dts.Rows[0][cbxl.ID.Substring(4)].ToString())
                                {
                                    cbxl.Items[k].Selected = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void IfableControl()
        {
            btnSubmit.Visible = true;
            btnBack.Visible = true;
            panJBXX.Enabled = false;
            panYYFX.Enabled = false;
            panCLYJ.Enabled = false;
            panCLFA.Enabled = false;
            panCLJG.Enabled = false;
            panFYTJ.Enabled = false;
            panZDR.Enabled = false;
            panSPR1.Enabled = false;
            panSPR2.Enabled = false;
            panSPR4.Enabled = false;
            panSPR5.Enabled = false;
            if (action == "add")
            {
                panJBXX.Enabled = true;
                panZDR.Enabled = true;
                panSPR1.Enabled = true;
            }
            if (action == "alter")
            {
                panJBXX.Enabled = true;
                panZDR.Enabled = true;
                panSPR1.Enabled = true;
            }
            if (action == "check")
            {
                if (dts.Rows[0]["CLD_SPZT"].ToString() == "0")
                {
                    if (username == dts.Rows[0]["CLD_SPR1"].ToString())
                    {
                        panSPR1.Enabled = true;
                    }
                }
                else if (dts.Rows[0]["CLD_SPZT"].ToString() == "1y")
                {
                    if (username == dts.Rows[0]["CLD_SPR2"].ToString())
                    {
                        panSPR2.Enabled = true;
                        panSPR4.Enabled = true;
                        panSPR5.Enabled = true;
                    }
                }
                if (username == dts.Rows[0]["CLD_SPR4"].ToString())
                {
                    panSPR4.Enabled = true;
                }
                if (username == dts.Rows[0]["CLD_SPR5"].ToString())
                {
                    panSPR5.Enabled = true;
                }
            }
            if (action == "writeyy")
            {
                panYYFX.Enabled = true;
            }
            if (action == "writeyj")
            {
                panCLYJ.Enabled = true;
            }
            if (action == "writefa")
            {
                panCLFA.Enabled = true;
                panSPR2.Enabled = true;
            }
            if (action == "writejg")
            {
                panCLJG.Enabled = true;
            }
            if (action == "count")
            {
                panFYTJ.Enabled = true;
            }
            if (action == "read")
            {
                btnSubmit.Visible = false;
                btnBack.Visible = false;
            }
        }



        #region 上传基本信息附件
        protected void btnFU1_OnClick(object sender, EventArgs e)
        {
            int IntIsUF = 0;//判断用户是否上传了文件
            //获取文件保存的路径
            string FilePath = @"E:\售后质量问题处理\" + Convert.ToString(System.DateTime.Now.Year);
            if (!Directory.Exists(FilePath))//如果不存在文件路径就创建一个
            {
                Directory.CreateDirectory(FilePath);
            }
            //对客户端已上载的单独文件的访问
            HttpPostedFile UserHPF = FileUpload2.PostedFile;
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
                            sqlStr += "values('" + hidCLD_SJID.Value + "'";
                            sqlStr += ",'" + FilePath + "'";
                            sqlStr += ",'" + DateTime.Now.ToString("yyyy年MM月dd日") + "'";
                            sqlStr += ",'" + strNewFile + "','" + strOldFile + "',";
                            sqlStr += "'CM_SHCLD_JBXX')";
                            //打开与数据库的连接
                            DBCallCommon.ExeSqlText(sqlStr);
                            UserHPF.SaveAs(FilePath + "//" + strNewFile);//将上传的文件存放在指定的文件夹中
                            IntIsUF = 1;
                        }
                        else
                        {
                            filesError2.Visible = true;
                            filesError2.Text = "文件名与服务器某个合同名重名，请您核对后重新上传！";
                            IntIsUF = 1;
                        }
                    }
                }
                else
                {
                    filesError2.Visible = true;
                    filesError2.Text = "文件类型不符合要求，请您核对后重新上传！";
                    IntIsUF = 1;
                }
            }
            catch
            {
                filesError2.Text = "文件上传过程中出现错误！";
                filesError2.Visible = true;
                return;
            }
            if (IntIsUF == 1)
            {
                IntIsUF = 0;
            }
            else
            {
                filesError2.Visible = true;
                filesError2.Text = "请选择上传文件!";
            }
            BindrptJBXX();
        }

        private void BindrptJBXX()
        {
            string sql = "select * from CM_FILES where FILE_FATHERID='" + hidCLD_SJID.Value + "' and FILE_TYPE='CM_SHCLD_JBXX'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            rptJBXX.DataSource = dt;
            rptJBXX.DataBind();
        }

        protected void lbtndelete2_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            //获取文件真实姓名
            string sqlStr = "select FILE_LOAD,FILE_NAME from CM_FILES where FILE_ID='" + id + "'";
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
            string sqlDelStr = "delete from CM_FILES where FILE_ID='" + id + "'";//删除数据库中的记录
            DBCallCommon.ExeSqlText(sqlDelStr);
            BindrptJBXX();//删除添加的记录
            //GVBind(ViewGridViewFiles);//删除查看的记录
        }

        protected void lbtnonload2_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            string sqlStr = "select FILE_LOAD,FILE_NAME,FILE_SHOWNAME from CM_FILES where FILE_ID='" + id + "'";
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
                filesError2.Visible = true;
                filesError2.Text = "文件已被删除，请通知相关人员上传文件！";
            }
        }
        #endregion

        #region 上传原因分析
        protected void btnFU2_OnClick(object sender, EventArgs e)
        {
            int IntIsUF = 0;//判断用户是否上传了文件
            //获取文件保存的路径
            string FilePath = @"E:\售后质量问题处理\" + Convert.ToString(System.DateTime.Now.Year);
            if (!Directory.Exists(FilePath))//如果不存在文件路径就创建一个
            {
                Directory.CreateDirectory(FilePath);
            }
            //对客户端已上载的单独文件的访问
            HttpPostedFile UserHPF = FileUpload3.PostedFile;
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
                            sqlStr += "values('" + hidCLD_SJID.Value + "'";
                            sqlStr += ",'" + FilePath + "'";
                            sqlStr += ",'" + DateTime.Now.ToString("yyyy年MM月dd日") + "'";
                            sqlStr += ",'" + strNewFile + "','" + strOldFile + "',";
                            sqlStr += "'CM_SHCLD_YYFX')";
                            //打开与数据库的连接
                            DBCallCommon.ExeSqlText(sqlStr);
                            UserHPF.SaveAs(FilePath + "//" + strNewFile);//将上传的文件存放在指定的文件夹中
                            IntIsUF = 1;
                        }
                        else
                        {
                            filesError3.Visible = true;
                            filesError3.Text = "文件名与服务器某个合同名重名，请您核对后重新上传！";
                            IntIsUF = 1;
                        }
                    }
                }
                else
                {
                    filesError3.Visible = true;
                    filesError3.Text = "文件类型不符合要求，请您核对后重新上传！";
                    IntIsUF = 1;
                }
            }
            catch
            {
                filesError3.Text = "文件上传过程中出现错误！";
                filesError3.Visible = true;
                return;
            }
            if (IntIsUF == 1)
            {
                IntIsUF = 0;
            }
            else
            {
                filesError3.Visible = true;
                filesError3.Text = "请选择上传文件!";
            }
            BindrptYYFX();
        }

        private void BindrptYYFX()
        {
            string sql = "select * from CM_FILES where FILE_FATHERID='" + hidCLD_SJID.Value + "' and FILE_TYPE='CM_SHCLD_YYFX'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            rptYYFX.DataSource = dt;
            rptYYFX.DataBind();
        }

        protected void lbtndelete3_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            //获取文件真实姓名
            string sqlStr = "select FILE_LOAD,FILE_NAME from CM_FILES where FILE_ID='" + id + "'";
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
            string sqlDelStr = "delete from CM_FILES where FILE_ID='" + id + "'";//删除数据库中的记录
            DBCallCommon.ExeSqlText(sqlDelStr);
            BindrptYYFX();//删除添加的记录
            //GVBind(ViewGridViewFiles);//删除查看的记录
        }

        protected void lbtnonload3_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            string sqlStr = "select FILE_LOAD,FILE_NAME,FILE_SHOWNAME from CM_FILES where FILE_ID='" + id + "'";
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
                filesError3.Visible = true;
                filesError3.Text = "文件已被删除，请通知相关人员上传文件！";
            }
        }

        #endregion

        # region 上传文件-处理意见
        protected void btnUp_Click(object sender, EventArgs e)//上传文件
        {
            int IntIsUF = 0;//判断用户是否上传了文件
            //获取文件保存的路径
            // @"F:\质量部附件\" + Convert.ToString(System.DateTime.Now.Year)
            string FilePath = @"E:\售后质量问题处理\" + Convert.ToString(System.DateTime.Now.Year);
            if (!Directory.Exists(FilePath))//如果不存在文件路径就创建一个
            {
                Directory.CreateDirectory(FilePath);
            }

            //对客户端已上载的单独文件的访问
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
                            string sqlStr = "insert into CM_FILES(FILE_FATHERID,FILE_LOAD,FILE_UPDATE,FILE_NAME,FILE_SHOWNAME,FILE_TYPE)";
                            sqlStr += "values('" + hidCLD_SJID.Value + "'";
                            sqlStr += ",'" + FilePath + "'";
                            sqlStr += ",'" + DateTime.Now.ToString("yyyy年MM月dd日") + "'";
                            sqlStr += ",'" + strNewFile + "','" + strOldFile + "',";
                            sqlStr += "'CM_SHCLD_CLYJ')";
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
            BindrptCLYJ();
        }

        private void BindrptCLYJ()//绑定gridview
        {
            string sql = "select * from CM_FILES where FILE_FATHERID='" + hidCLD_SJID.Value + "' and FILE_TYPE='CM_SHCLD_CLYJ'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            rptCLYJ.DataSource = dt;
            rptCLYJ.DataBind();
        }

        protected void lbtndelete_OnClick(object sender, EventArgs e)//删除
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            //获取文件真实姓名
            string sqlStr = "select FILE_LOAD,FILE_NAME from CM_FILES where FILE_ID='" + id + "'";
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
            string sqlDelStr = "delete from CM_FILES where FILE_ID='" + id + "'";//删除数据库中的记录
            DBCallCommon.ExeSqlText(sqlDelStr);
            BindrptCLYJ(); ;//删除添加的记录
            //GVBind(ViewGridViewFiles);//删除查看的记录
        }
        protected void lbtnonload_OnClick(object sender, EventArgs e)//下载
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            string sqlStr = "select FILE_LOAD,FILE_NAME,FILE_SHOWNAME from CM_FILES where FILE_ID='" + id + "'";
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
                filesError.Visible = true;
                filesError.Text = "文件已被删除，请通知相关人员上传文件！";
            }
        }
        #endregion

        #region 上传处理方案
        protected void btnFU3_OnClick(object sender, EventArgs e)
        {
            int IntIsUF = 0;//判断用户是否上传了文件
            //获取文件保存的路径
            string FilePath = @"E:\售后质量问题处理\" + Convert.ToString(System.DateTime.Now.Year);
            if (!Directory.Exists(FilePath))//如果不存在文件路径就创建一个
            {
                Directory.CreateDirectory(FilePath);
            }
            //对客户端已上载的单独文件的访问
            HttpPostedFile UserHPF = FileUpload4.PostedFile;
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
                            sqlStr += "'CM_SHCLD_CLFA')";
                            //打开与数据库的连接
                            DBCallCommon.ExeSqlText(sqlStr);
                            UserHPF.SaveAs(FilePath + "//" + strNewFile);//将上传的文件存放在指定的文件夹中
                            IntIsUF = 1;
                        }
                        else
                        {
                            filesError4.Visible = true;
                            filesError4.Text = "文件名与服务器某个合同名重名，请您核对后重新上传！";
                            IntIsUF = 1;
                        }
                    }
                }
                else
                {
                    filesError4.Visible = true;
                    filesError4.Text = "文件类型不符合要求，请您核对后重新上传！";
                    IntIsUF = 1;
                }
            }
            catch
            {
                filesError4.Text = "文件上传过程中出现错误！";
                filesError4.Visible = true;
                return;
            }
            if (IntIsUF == 1)
            {
                IntIsUF = 0;
            }
            else
            {
                filesError4.Visible = true;
                filesError4.Text = "请选择上传文件!";
            }
            BindrptCLFA();
        }

        private void BindrptCLFA()
        {
            string sql = "select * from CM_FILES where FILE_FATHERID='" + id + "' and FILE_TYPE='CM_SHCLD_CLFA'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            rptCLFA.DataSource = dt;
            rptCLFA.DataBind();
        }

        protected void lbtndelete4_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            //获取文件真实姓名
            string sqlStr = "select FILE_LOAD,FILE_NAME from CM_FILES where FILE_ID='" + id + "'";
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
            string sqlDelStr = "delete from CM_FILES where FILE_ID='" + id + "'";//删除数据库中的记录
            DBCallCommon.ExeSqlText(sqlDelStr);
            BindrptCLFA();//删除添加的记录
            //GVBind(ViewGridViewFiles);//删除查看的记录
        }

        protected void lbtnonload4_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            string sqlStr = "select FILE_LOAD,FILE_NAME,FILE_SHOWNAME from CM_FILES where FILE_ID='" + id + "'";
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
                filesError4.Visible = true;
                filesError4.Text = "文件已被删除，请通知相关人员上传文件！";
            }
        }

        #endregion

        # region  上传文件-处理结果
        protected void btnFU_OnClick(object sender, EventArgs e)
        {
            int IntIsUF = 0;//判断用户是否上传了文件
            //获取文件保存的路径
            string FilePath = @"E:\售后质量问题处理\" + Convert.ToString(System.DateTime.Now.Year);
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
                            sqlStr += "'CM_SHCLD_CLJG')";
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
            BindrptCLJG();
        }

        private void BindrptCLJG()
        {
            string sql = "select * from CM_FILES where FILE_FATHERID='" + id + "' and FILE_TYPE='CM_SHCLD_CLJG'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            rptCLJG.DataSource = dt;
            rptCLJG.DataBind();
        }

        protected void lbtndelete1_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            //获取文件真实姓名
            string sqlStr = "select FILE_LOAD,FILE_NAME from CM_FILES where FILE_ID='" + id + "'";
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
            string sqlDelStr = "delete from CM_FILES where FILE_ID='" + id + "'";//删除数据库中的记录
            DBCallCommon.ExeSqlText(sqlDelStr);
            BindrptCLJG();//删除添加的记录
            //GVBind(ViewGridViewFiles);//删除查看的记录
        }

        protected void lbtnonload1_OnClick(object sender, EventArgs e)
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            string sqlStr = "select FILE_LOAD,FILE_NAME,FILE_SHOWNAME from CM_FILES where FILE_ID='" + id + "'";
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
                filesError.Visible = true;
                filesError.Text = "文件已被删除，请通知相关人员上传文件！";
            }
        }

        #endregion

        #region 增加、删除行
        protected void btnAdd_OnClick(object sender, EventArgs e) //增加行的函数
        {
            CreateNewRow(1);
            NoDataPanelSee();
        }

        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            this.rptFWFY.DataSource = dt;
            this.rptFWFY.DataBind();
            //InitVar();
        }

        private DataTable GetDataTable() //临时表
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FY_FYMC");
            dt.Columns.Add("FY_FYJE");
            dt.Columns.Add("FY_BZ");
            foreach (RepeaterItem retItem in rptFWFY.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)retItem.FindControl("FY_FYMC")).Text;
                newRow[1] = ((TextBox)retItem.FindControl("FY_FYJE")).Text;
                newRow[2] = ((TextBox)retItem.FindControl("FY_BZ")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)//删除一行
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FY_FYMC");
            dt.Columns.Add("FY_FYJE");
            dt.Columns.Add("FY_BZ");
            foreach (RepeaterItem retItem in rptFWFY.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((TextBox)retItem.FindControl("FY_FYMC")).Text;
                    newRow[1] = ((TextBox)retItem.FindControl("FY_FYJE")).Text;
                    newRow[2] = ((TextBox)retItem.FindControl("FY_BZ")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.rptFWFY.DataSource = dt;
            this.rptFWFY.DataBind();
            NoDataPanelSee();
        }
        private void NoDataPanelSee()
        {
            if (rptFWFY.Items.Count > 0)
            {
                NoDataPanel.Visible = false;
            }
            else
            {
                NoDataPanel.Visible = true;
            }
        }
        #endregion


        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            #region 提交的条件控制
            int ctr = SubmitControl();
            if (ctr == 1)
            {
                Response.Write("<script type='text/javascript'>alert('合同号不能为空，请输入合同号后再提交')</script>");
                return;
            }
            else if (ctr == 2)
            {
                Response.Write("<script type='text/javascript'>alert('审批结论不能为空，请选择审批结论后再提交')</script>");
                return;
            }
            else if (ctr == 3)
            {
                Response.Write("<script type='text/javascript'>alert('处理方案不能为空，请填写处理方案后再提交')</script>");
                return;
            }
            else if (ctr == 4)
            {
                Response.Write("<script type='text/javascript'>alert('负责部门不能为空，请填写负责部门后再提交')</script>");
                return;
            }
            else if (ctr == 5)
            {
                Response.Write("<script type='text/javascript'>alert('“处理方案”和“处理意见”还未填写完整，此时不能选择审批类型')</script>");
                return;
            }
            else if (ctr == 6)
            {
                Response.Write("<script type='text/javascript'>alert('“原因分析”不能为空，请填写之后再提交')</script>");
                return;
            }
            else if (ctr == 7)
            {
                Response.Write("<script type='text/javascript'>alert('“处理意见”不能为空，请填写之后再提交')</script>");
                return;
            }
            else if (ctr == 8)
            {
                Response.Write("<script type='text/javascript'>alert('“处理过程及结果”不能为空，请填写之后再提交')</script>");
                return;
            }
            else if (ctr == 9)
            {
                Response.Write("<script type='text/javascript'>alert('未选择审批类型，请选择审批类型之后再提交')</script>");
                return;
            }
            #endregion

            #region 执行各种情况下的sql语句
            if (action == "add")
            {
                string sql = addsql();
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {
                    Response.Write("<script type='text/javascript'>alert('addsql()出现问题，请与管理员联系')</script>");
                    return;
                }
            }
            if (action == "alter")
            {
                string sql = altersql();
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {
                    Response.Write("<script type='text/javascript'>alert('altersql()出现问题，请与管理员联系')</script>");
                    return;
                }
            }
            if (action == "writeyy")
            {
                string sql = writeyysql();
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {
                    Response.Write("<script type='text/javascript'>alert('writeyysql()出现问题，请与管理员联系')</script>");
                    return;
                }
            }
            if (action == "writeyj")
            {
                string sql = writeyjsql();
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {
                    Response.Write("<script type='text/javascript'>alert('writeyysql()出现问题，请与管理员联系')</script>");
                    return;
                }
            }
            if (action == "check")
            {
                string sql = checksql();
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {
                    Response.Write("<script type='text/javascript'>alert('checksql()出现问题，请与管理员联系')</script>");
                    return;
                }
            }
            if (action == "writefa")
            {
                string sql = writefasql();
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {
                    Response.Write("<script type='text/javascript'>alert('writefasql()出现问题，请与管理员联系')</script>");
                    return;
                }
            }
            if (action == "writejg")
            {
                string sql = writejgsql();
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {
                    Response.Write("<script type='text/javascript'>alert('writejgsql()出现问题，请与管理员联系')</script>");
                    return;
                }
            }
            if (action == "count")
            {
                List<string> list = countsql();
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script type='text/javascript'>alert('countsql()出现问题，请与管理员联系')</script>");
                    return;
                }
            }
            #endregion

            SendEmail();//发送邮件
            Response.Write("<script type='text/javascript'>alert('提交成功！！！')</script>");
            Response.Redirect("CM_SHCLDGL.aspx");
        }

        private int SubmitControl()
        {
            panJBXX.Enabled = true;
            panYYFX.Enabled = true;
            panCLYJ.Enabled = true;
            panCLFA.Enabled = true;
            panCLJG.Enabled = true;
            panFYTJ.Enabled = true;
            panZDR.Enabled = true;
            panSPR1.Enabled = true;
            panSPR2.Enabled = true;
            panSPR4.Enabled = true;
            panSPR5.Enabled = true;
            int ctr = 0;
            if (action == "add" || action == "alter")
            {
                if (txtCLD_HTH.Text == "")
                {
                    ctr = 1;//合同号不能为空
                }
            }
            if (action == "check")
            {
                if (dts.Rows[0]["CLD_SPZT"].ToString() == "0")
                {
                    if (username == dts.Rows[0]["CLD_SPR1"].ToString())
                    {
                        if (rblCLD_SPR1_JL.SelectedValue != "1" && rblCLD_SPR1_JL.SelectedValue != "2")
                        {
                            ctr = 2;//审批结论不能为空
                        }
                    }
                }
                if (dts.Rows[0]["CLD_SPZT"].ToString() == "1y")
                {
                    if (username == dts.Rows[0]["CLD_SPR2"].ToString())
                    {
                        if (rblCLD_SPR2_JL.SelectedValue != "1" && rblCLD_SPR2_JL.SelectedValue != "2")
                        {
                            ctr = 2;//审批结论不能为空
                        }
                        else if (cbxlCLD_FZBM.SelectedValue == "")
                        {
                            ctr = 4;//责任部门不能为空
                        }
                        else if (rblCLD_SPLX.SelectedValue != "" && (dts.Rows[0]["CLD_CLFA"].ToString() == "" || dts.Rows[0]["CLD_CLYJ"].ToString() == ""))
                        {
                            ctr = 5;//处理意见和处理方案未填写时是不能提交审批的。
                        }
                        else if (rblCLD_SPLX.SelectedValue != "1" && rblCLD_SPLX.SelectedValue != "2")
                        {
                            ctr = 9;//未勾选审批类型不能提交
                        }
                    }
                }
                if (username == dts.Rows[0]["CLD_SPR4"].ToString())
                {
                    if (rblCLD_SPR4_JL.SelectedValue != "1" && rblCLD_SPR4_JL.SelectedValue != "2")
                    {
                        ctr = 2;//审批结论不能为空
                    }
                }
                if (username == dts.Rows[0]["CLD_SPR5"].ToString())
                {
                    if (rblCLD_SPR5_JL.SelectedValue != "1" && rblCLD_SPR5_JL.SelectedValue != "2")
                    {
                        ctr = 2;//审批结论不能为空
                    }
                }
            }
            if (action == "writeyy")
            {
                if (txtCLD_YYFX.Text.Trim() == "")
                {
                    ctr = 6;//原因分析不能为空
                }
            }
            if (action == "writeyj")
            {
                if (txtCLD_CLYJ.Text.Trim() == "")
                {
                    ctr = 7;//处理意见不能为空
                }
            }
            if (action == "writefa")
            {
                if (txtCLD_CLFA.Text.Trim() == "")
                {
                    ctr = 3;//处理方案不能为空
                }
            }
            if (action == "writejg")
            {
                if (txtCLD_CLJG.Text.Trim() == "")
                {
                    ctr = 8;//处理结果不能为空
                }
            }
            return ctr;
        }

        private string addsql()
        {
            string sql = "insert into CM_SHCLD (";
            sql += "CLD_SJID,";
            sql += "CLD_BH,";
            sql += "CLD_XMMC,";
            sql += "CLD_HTH,";
            sql += "CLD_SBMC,";
            sql += "CLD_GKMC,";
            sql += "CLD_RWH,";
            sql += "CLD_XXJJ,";
            sql += "CLD_WTMS,";
            sql += "CLD_YZDZ,";
            sql += "CLD_LXR,";
            sql += "CLD_LXFS,";
            sql += "CLD_SJYQ,";
            sql += "CLD_ZDR,";
            sql += "CLD_ZDSJ,";
            sql += "CLD_ZDJY,";
            sql += "CLD_SPR1,";
            sql += "CLD_SPZT";
            sql += ")values(";
            sql += "'" + hidCLD_SJID.Value + "',";
            sql += "'" + txtCLD_BH.Text.Trim() + "',";
            sql += "'" + txtCLD_XMMC.Text.Trim() + "',";
            sql += "'" + txtCLD_HTH.Text.Trim() + "',";
            sql += "'" + txtCLD_SBMC.Text.Trim() + "',";
            sql += "'" + txtCLD_GKMC.Text.Trim() + "',";
            sql += "'" + txtCLD_RWH.Text.Trim() + "',";
            sql += "'" + txtCLD_XXJJ.Text.Trim() + "',";
            sql += "'" + txtCLD_WTMS.Text.Trim() + "',";
            sql += "'" + txtCLD_YZDZ.Text.Trim() + "',";
            sql += "'" + txtCLD_LXR.Text.Trim() + "',";
            sql += "'" + txtCLD_LXFS.Text.Trim() + "',";
            sql += "'" + txtCLD_SJYQ.Text.Trim() + "',";
            sql += "'" + txtCLD_ZDR.Text.Trim() + "',";
            sql += "'" + lbCLD_ZDSJ.Text.Trim() + "',";
            sql += "'" + txtCLD_ZDJY.Text.Trim() + "',";
            sql += "'" + txtCLD_SPR1.Text.Trim() + "',";
            sql += "'0')";
            return sql;
        }

        private string altersql()
        {
            string sql = "update CM_SHCLD set ";
            sql += "CLD_BH='" + txtCLD_BH.Text.Trim() + "',";
            sql += "CLD_XMMC='" + txtCLD_XMMC.Text.Trim() + "',";
            sql += "CLD_HTH='" + txtCLD_HTH.Text.Trim() + "',";
            sql += "CLD_SBMC='" + txtCLD_SBMC.Text.Trim() + "',";
            sql += "CLD_GKMC='" + txtCLD_GKMC.Text.Trim() + "',";
            sql += "CLD_RWH='" + txtCLD_RWH.Text.Trim() + "',";
            sql += "CLD_XXJJ='" + txtCLD_XXJJ.Text.Trim() + "',";
            sql += "CLD_WTMS='" + txtCLD_WTMS.Text.Trim() + "',";
            sql += "CLD_YZDZ='" + txtCLD_YZDZ.Text.Trim() + "',";
            sql += "CLD_LXFS='" + txtCLD_LXFS.Text.Trim() + "',";
            sql += "CLD_SJYQ='" + txtCLD_SJYQ.Text.Trim() + "',";
            sql += "CLD_ZDJY='" + txtCLD_ZDJY.Text.Trim() + "',";
            sql += "CLD_SPR1='" + txtCLD_SPR1.Text.Trim() + "'";
            sql += " where CLD_ID='" + id + "'";
            return sql;
        }

        private string checksql()
        {
            string sql = "update CM_SHCLD set ";
            if (dts.Rows[0]["CLD_SPZT"].ToString() == "0")
            {
                if (username == dts.Rows[0]["CLD_SPR1"].ToString())
                {
                    sql += "CLD_SPR1_JL='" + rblCLD_SPR1_JL.SelectedValue + "',";
                    sql += "CLD_SPR1_SJ='" + lbCLD_SPR1_SJ.Text + "',";
                    sql += "CLD_SPR1_JY='" + txtCLD_SPR1_JY.Text.Trim() + "',";
                    if (rblCLD_SPR1_JL.SelectedValue == "1")
                    {
                        sql += "CLD_SPZT='1y'";//审批人1通过,审批人2初始化
                    }
                    if (rblCLD_SPR1_JL.SelectedValue == "2")
                    {
                        sql += "CLD_SPZT='1n'";//审批未通过
                    }
                }
            }
            else if (dts.Rows[0]["CLD_SPZT"].ToString() == "1y")
            {
                if (username == dts.Rows[0]["CLD_SPR2"].ToString())
                {
                    sql += "CLD_SPR2_JL='" + rblCLD_SPR2_JL.SelectedValue + "',";
                    sql += "CLD_SPR2_SJ='" + lbCLD_SPR2_SJ.Text + "',";
                    sql += "CLD_SPR2_JY='" + txtCLD_SPR2_JY.Text.Trim() + "',";
                    if (rblCLD_SPR1_JL.SelectedValue == "1")
                    {
                        sql += "CLD_SPZT='2y',";//审批人1通过,审批人2初始化
                        string fzbm = "";
                        string fzbmid = "";
                        foreach (ListItem item in cbxlCLD_FZBM.Items)
                        {
                            if (item.Selected)
                            {
                                fzbm += item.Text + "|";
                                fzbmid += item.Value + "|";
                            }
                        }
                        sql += "CLD_FZBM='" + fzbm.Trim('|') + "',";
                        sql += "CLD_FZBMID='" + fzbmid.Trim('|') + "'";
                        if (rblCLD_SPLX.SelectedValue == "1")
                        {
                            sql += ", CLD_SPLX='" + rblCLD_SPLX.SelectedValue + "',";
                            sql += "CLD_SPR4='" + txtCLD_SPR4.Text.Trim() + "'";
                        }
                        if (rblCLD_SPLX.SelectedValue == "2")
                        {
                            sql += ", CLD_SPLX='" + rblCLD_SPLX.SelectedValue + "',";
                            sql += "CLD_SPR4='" + txtCLD_SPR4.Text.Trim() + "',";
                            sql += "CLD_SPR5='" + txtCLD_SPR5.Text.Trim() + "'";
                        }
                    }
                    if (rblCLD_SPR1_JL.SelectedValue == "2")
                    {
                        sql += "CLD_SPZT='2n'";//审批未通过
                    }
                }
            }
            if (dts.Rows[0]["CLD_SPLX"].ToString() == "1")
            {
                if (username == dts.Rows[0]["CLD_SPR4"].ToString())
                {
                    sql += "CLD_SPR4_JL='" + rblCLD_SPR4_JL.SelectedValue + "',";
                    sql += "CLD_SPR4_SJ='" + lbCLD_SPR4_SJ.Text + "',";
                    sql += "CLD_SPR4_JY='" + txtCLD_SPR4_JY.Text.Trim() + "',";
                    if (rblCLD_SPR4_JL.SelectedValue == "1")
                    {
                        sql += "CLD_SPZT='y'";//审批通过
                    }
                    if (rblCLD_SPR4_JL.SelectedValue == "2")
                    {
                        sql += "CLD_SPZT='4n'";
                    }
                }
            }
            else if (dts.Rows[0]["CLD_SPLX"].ToString() == "2")
            {
                if (username == dts.Rows[0]["CLD_SPR4"].ToString())
                {
                    sql += "CLD_SPR4_JL='" + rblCLD_SPR4_JL.SelectedValue + "',";
                    sql += "CLD_SPR4_SJ='" + lbCLD_SPR4_SJ.Text + "',";
                    sql += "CLD_SPR4_JY='" + txtCLD_SPR4_JY.Text.Trim() + "',";
                    if (rblCLD_SPR4_JL.SelectedValue == "1")
                    {
                        sql += "CLD_SPZT='4y'";//审批2通过，3初始化
                    }
                    if (rblCLD_SPR4_JL.SelectedValue == "2")
                    {
                        sql += "CLD_SPZT='4n'";
                    }
                }
                if (username == dts.Rows[0]["CLD_SPR5"].ToString())
                {
                    sql += "CLD_SPR5_JL='" + rblCLD_SPR5_JL.SelectedValue + "',";
                    sql += "CLD_SPR5_SJ='" + lbCLD_SPR5_SJ.Text + "',";
                    sql += "CLD_SPR5_JY='" + txtCLD_SPR5_JY.Text.Trim() + "',";
                    if (rblCLD_SPR5_JL.SelectedValue == "1")
                    {
                        sql += "CLD_SPZT='y'";//审批人3通过，审批人4初始化
                    }
                    if (rblCLD_SPR5_JL.SelectedValue == "2")
                    {
                        sql += "CLD_SPZT='5n'";
                    }
                }
            }
            sql += " where CLD_ID='" + id + "'";
            return sql;
        }

        private string writeyysql()
        {
            string sql = "update CM_SHCLD set ";
            sql += "CLD_YYFX='" + txtCLD_YYFX.Text.Trim() + "',";
            if (dts.Rows[0]["CLD_FWFY_TJSJ"].ToString() != "" && dts.Rows[0]["CLD_FWFY_TJSJ"].ToString() != null)
            {
                if (string.Compare(lbCLD_YYFX_TXSJ.Text, dts.Rows[0]["CLD_FWFY_TJSJ"].ToString()) >= 0)
                {
                    sql += "CLD_SPZT='over',";
                }
            }
            sql += "CLD_YYFX_TXSJ='" + lbCLD_YYFX_TXSJ.Text + "'";
            sql += " where CLD_ID='" + id + "'";
            return sql;
        }

        private string writeyjsql()
        {
            string sql = "update CM_SHCLD set ";
            sql += "CLD_CLYJ='" + txtCLD_CLYJ.Text.Trim() + "',";
            sql += "CLD_CLYJ_TXSJ='" + lbCLD_CLYJ_TXSJ.Text + "'";
            sql += " where CLD_ID='" + id + "'";
            return sql;
        }

        private string writefasql()
        {
            string sql = "update CM_SHCLD set ";
            sql += "CLD_CLFA='" + txtCLD_CLFA.Text.Trim() + "',";
            sql += "CLD_CLFA_TXSJ='" + lbCLD_CLFA_TXSJ.Text.Trim() + "',";
            sql += "CLD_SPR2='" + txtCLD_SPR2.Text.Trim() + "'";
            sql += " where CLD_ID='" + id + "'";
            return sql;
        }

        private string writejgsql()
        {
            string sql = "update CM_SHCLD set ";
            sql += "CLD_CLJG='" + txtCLD_CLJG.Text.Trim() + "',";
            sql += "CLD_CLJG_TXR='" + username + "',";
            sql += "CLD_CLJG_TXSJ='" + lbCLD_CLJG_TXSJ.Text + "',";
            sql += "CLD_FWFY_DEPID='06',";
            sql += "CLD_SPZT='cljg_y'";
            sql += " where CLD_ID='" + id + "'";
            return sql;
        }

        private List<string> countsql()
        {
            List<string> list = new List<string>();
            string sql = "update CM_SHCLD set ";
            sql += "CLD_FWZFY=" + txtCLD_FWZFY.Text.Trim() + ",";
            sql += "CLD_FWFY_TJR='" + txtCLD_FWFY_TJR.Text.Trim() + "',";
            sql += "CLD_FWFY_TJSJ='" + lbCLD_FWFY_TJSJ.Text.Trim() + "',";
            if (dts.Rows[0]["CLD_YYFX_TXSJ"].ToString() != "" && dts.Rows[0]["CLD_YYFX_TXSJ"].ToString() != null)
            {
                if (string.Compare(lbCLD_FWFY_TJSJ.Text, dts.Rows[0]["CLD_YYFX_TXSJ"].ToString()) >= 0)
                {
                    sql += "CLD_SPZT='over'";
                }
                else
                {
                    sql += "CLD_SPZT='fytj_y'";
                }
            }
            else
            {
                sql += "CLD_SPZT='fytj_y'";
            }
            sql += " where CLD_ID='" + id + "'";
            list.Add(sql);
            string sql1 = "";
            for (int i = 0, length = rptFWFY.Items.Count; i < length; i++)
            {
                string FY_FYMC = ((TextBox)rptFWFY.Items[i].FindControl("FY_FYMC")).Text.Trim();
                string FY_FYJE = ((TextBox)rptFWFY.Items[i].FindControl("FY_FYJE")).Text.Trim();
                string FY_BZ = ((TextBox)rptFWFY.Items[i].FindControl("FY_BZ")).Text.Trim();
                sql1 = "insert into CM_SHCLD_FY (FY_CLDID,FY_FYMC,FY_FYJE,FY_BZ) values (";
                sql1 += "'" + id + "',";
                sql1 += "'" + FY_FYMC + "',";
                sql1 += FY_FYJE + ",";
                sql1 += "'" + FY_BZ + "')";
                list.Add(sql1);
            }
            return list;
        }

        private void SendEmail()
        {
            if (action == "add")
            {
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(GetST_ID(txtCLD_SPR1.Text.Trim())), new List<string>(), new List<string>(), "售后质量问题处理审批", "您有售后质量问题处理单“" + txtCLD_BH.Text + "”需要审批，请登录系统,进入市场管理模块的“售后质量问题处理单页面”进行查看。");//给李利恒发邮件通知其审批
            }
            if (action == "writeyy")
            {
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(GetST_ID(txtCLD_SPR1.Text.Trim())), new List<string>(), new List<string>(), "售后质量问题处理通知", "服务联系单“" + dts.Rows[0]["CLD_BH"].ToString() + "”已填写“原因分析”，请登录系统,进入市场管理模块的“售后质量问题处理单页面”进行查看。");//给李利恒发邮件通知其审批
            }
            if (action == "writeyj")
            {
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(GetST_ID(txtCLD_SPR1.Text.Trim())), new List<string>(), new List<string>(), "售后质量问题处理通知", "服务联系单“" + dts.Rows[0]["CLD_BH"].ToString() + "”已填写“处理意见”，请登录系统,进入市场管理模块的“售后质量问题处理单页面”进行查看。");//给李利恒发邮件通知其审批
            }
            if (action == "writefa" && rblCLD_SPLX.SelectedValue != "")
            {
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(GetST_ID(txtCLD_SPR1.Text.Trim())), new List<string>(), new List<string>(), "售后质量问题处理审批", "售后质量问题处理单“" + dts.Rows[0]["CLD_BH"].ToString() + "”已填写“处理方案”，请登录系统,进入市场管理模块的“售后质量问题处理单页面”进行审批。");
            }
            if (action == "check")
            {
                if (dts.Rows[0]["CLD_SPZT"].ToString()=="0")
                {
                    if (username == dts.Rows[0]["CLD_SPR1"].ToString())
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("69"), new List<string>(), new List<string>(), "售后质量问题处理", "您有售后质量问题处理单“" + dts.Rows[0]["CLD_BH"].ToString() + "”需要分工“原因分析”，请登录系统,进入市场管理模块的“售后质量问题处理单页面”进行查看。");//给陈永秀发邮件通知其审批
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("67"), new List<string>(), new List<string>(), "售后质量问题处理", "您有售后质量问题处理单“" + dts.Rows[0]["CLD_BH"].ToString() + "”需要分工“处理意见”，请登录系统,进入市场管理模块的“售后质量问题处理单页面”进行查看。");//给李小婷发邮件通知其审批
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("47"), new List<string>(), new List<string>(), "售后质量问题处理", "您有售后质量问题处理单“" + dts.Rows[0]["CLD_BH"].ToString() + "”需要分工“处理方案”，请登录系统,进入市场管理模块的“售后质量问题处理单页面”进行查看。");//给李利恒发邮件通知其审批
                    }
                }
                else if (dts.Rows[0]["CLD_SPZT"].ToString()=="1y")
                {
                    if (username == dts.Rows[0]["CLD_SPR2"].ToString()&&rblCLD_SPR2_JL.SelectedValue=="1")
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(GetST_ID(txtCLD_SPR4.Text.Trim())), new List<string>(), new List<string>(), "售后质量问题处理审批", "售后质量问题处理单“" + dts.Rows[0]["CLD_BH"].ToString() + "”已填写“处理方案”，请登录系统,进入市场管理模块的“售后质量问题处理单页面”进行审批。");
                    }
                }
                else if (dts.Rows[0]["CLD_SPZT"].ToString()=="2y")
                {
                    if (username == dts.Rows[0]["CLD_SPR4"].ToString() && dts.Rows[0]["CLD_SPLX"].ToString() == "2")
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("311"), new List<string>(), new List<string>(), "售后质量问题处理审批", "售后质量问题处理单“" + dts.Rows[0]["CLD_BH"].ToString() + "”需要审批，请登录系统,进入市场管理模块的“售后质量问题处理单页面”进行查看。");//给周文轶发邮件通知其查阅
                    }
                }
               
            }
            if (action == "writejg")
            {
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("311"), new List<string>(), new List<string>(), "售后质量问题处理通知", "售后质量问题处理单“" + dts.Rows[0]["CLD_BH"].ToString() + "”已填写“处理过程及结果”，请登录系统,进入市场管理模块的“售后质量问题处理单页面”进行查看。");//给周文轶发邮件通知其查阅
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("2"), new List<string>(), new List<string>(), "售后质量问题处理审批", "售后质量问题处理单“" + dts.Rows[0]["CLD_BH"].ToString() + "”已填写“处理过程及结果”,进入市场管理模块的“售后质量问题处理单页面”进行查看。");//给王福泉发邮件通知其查阅
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("47"), new List<string>(), new List<string>(), "售后质量问题处理审批", "售后质量问题处理单“" + dts.Rows[0]["CLD_BH"].ToString() + "”已填写“费用统计”,进入市场管理模块的“售后质量问题处理单页面”进行查看。");//给王福泉发邮件通知其查阅
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("256"), new List<string>(), new List<string>(), "售后质量问题处理单服务费用统计", "您有售后质量问题处理单“" + dts.Rows[0]["CLD_BH"].ToString() + "”需要分工“费用统计”，请登录系统,进入市场管理模块的“服务联系单页面”进行分工。");//给叶宝松发邮件通知其查阅
            }
            if (action == "count")
            {
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("311"), new List<string>(), new List<string>(), "售后质量问题处理通知", "售后质量问题处理单“" + dts.Rows[0]["CLD_BH"].ToString() + "”已填写“费用统计”，请登录系统,进入市场管理模块的“售后质量问题处理单页面”进行查看。");//给周文轶发邮件通知其查阅
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("2"), new List<string>(), new List<string>(), "售后质量问题处理审批", "售后质量问题处理单“" + dts.Rows[0]["CLD_BH"].ToString() + "”已填写“费用统计”,进入市场管理模块的“售后质量问题处理单页面”进行查看。");//给王福泉发邮件通知其查阅
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("47"), new List<string>(), new List<string>(), "售后质量问题处理审批", "售后质量问题处理单“" + dts.Rows[0]["CLD_BH"].ToString() + "”已填写“费用统计”,进入市场管理模块的“售后质量问题处理单页面”进行查看。");//给王福泉发邮件通知其查阅
            }
        }

        private string GetST_ID(string ST_NAME)
        {
            string st_id = "";
            string sql = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_NAME='" + ST_NAME + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            st_id = dt.Rows[0]["ST_ID"].ToString();
            return st_id;
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            if (action == "read")
            {
                Response.Write("<script>window.opener=null;window.close();</script>");
            }
            else
            {
                Response.Redirect("CM_SHCLDGL.aspx");
            }
        }
    }
}
