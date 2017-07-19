using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_SHLXD : System.Web.UI.Page
    {
        string action = string.Empty;
        string id = string.Empty;
        DataTable dt = new DataTable();
        string sqldt = string.Empty;
        string username = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];
            id = Request.QueryString["id"];
            username = Session["UserName"].ToString();
            if (action != "add")
            {
                sqldt = "select * from CM_SHLXD where LXD_ID='" + id + "'";
                try
                {
                    dt = DBCallCommon.GetDTUsingSqlText(sqldt);
                    hidLXD_SJID.Value = dt.Rows[0]["LXD_SJID"].ToString();
                }
                catch
                {
                    Response.Write("<script type='text/javascript'>alert('sqltext语句出现问题，请与管理员联系')</script>");
                    return;
                }
                dt = DBCallCommon.GetDTUsingSqlText(sqldt);
            }
            if (!IsPostBack)
            {
                BindData();
            }
            PowerControl();
            NoDataPanelSee();
        }

        private void BindData()
        {
            BindrptJBXX();
            BindrptCLJG();
            if (action == "add")
            {
                hidLXD_SJID.Value = DateTime.Now.ToString("yyyy-MM-dd:HH-mm-ss-fff");
                txtLXD_ZDR.Text = username;
                lbLXD_ZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtLXD_SPR1.Text = "李利恒";
            }
            else if (action == "alter")
            {
                BindPanel(dt, Panel0);
                BindPanel(dt, Panel1);
                BindPanel(dt, Panel2);
                BindPanel(dt, panSPR1);
            }
            else if (action == "check")
            {
                BindPanel(dt, Panel0);
                BindPanel(dt, Panel1);
                BindPanel(dt, Panel2);
                if (username == dt.Rows[0]["LXD_SPR1"].ToString())
                {
                    txtLXD_SPR1.Text = dt.Rows[0]["LXD_SPR1"].ToString();
                    lbLXD_SPR1_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (username == dt.Rows[0]["LXD_SPR2"].ToString())
                {
                    BindPanel(dt, panSPR1);
                    BindPanel(dt, panFZBM);
                    txtLXD_SPR2.Text = dt.Rows[0]["LXD_SPR2"].ToString();
                    lbLXD_SPR2_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (username == dt.Rows[0]["LXD_SPR3"].ToString())
                {
                    BindPanel(dt, panSPR1);
                    BindPanel(dt, panFZBM);
                    BindPanel(dt, panSPR2);
                    txtLXD_SPR3.Text = dt.Rows[0]["LXD_SPR3"].ToString();
                    lbLXD_SPR3_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else if (action == "read")
            {
                BindPanel(dt, Panel0);
                BindPanel(dt, Panel1);
                BindPanel(dt, Panel2);
                BindPanel(dt, panSPR1);
                BindPanel(dt, panFZBM);
                if (dt.Rows[0]["LXD_SPLX"].ToString() == "1")
                {
                    BindPanel(dt, panSPR2);
                }
                if (dt.Rows[0]["LXD_SPLX"].ToString() == "2")
                {
                    BindPanel(dt, panSPR2);
                    BindPanel(dt, panSPR3);
                }
                BindPanel(dt, panFWJG);
                BindPanel(dt, panFYTJ);
                string sql = "select * from CM_SHLXD_FY where FY_LXDID='" + id + "'";
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                rptFWFY.DataSource = dt1;
                rptFWFY.DataBind();
                NoDataPanelSee();
            }
            else if (action == "write")
            {
                BindPanel(dt, Panel0);
                BindPanel(dt, Panel1);
                BindPanel(dt, Panel2);
                BindPanel(dt, panSPR1);
                BindPanel(dt, panFZBM);
                if (dt.Rows[0]["LXD_SPLX"].ToString() == "1")
                {
                    BindPanel(dt, panSPR2);
                }
                if (dt.Rows[0]["LXD_SPLX"].ToString() == "2")
                {
                    BindPanel(dt, panSPR2);
                    BindPanel(dt, panSPR3);
                }
                txtLXD_FWJGTXR.Text = username;
                lbLXD_FWJGTXSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else if (action == "count")
            {
                BindPanel(dt, Panel0);
                BindPanel(dt, Panel1);
                BindPanel(dt, Panel2);
                BindPanel(dt, panSPR1);
                BindPanel(dt, panFZBM);
                if (dt.Rows[0]["LXD_SPLX"].ToString() == "1")
                {
                    BindPanel(dt, panSPR2);
                }
                if (dt.Rows[0]["LXD_SPLX"].ToString() == "2")
                {
                    BindPanel(dt, panSPR2);
                    BindPanel(dt, panSPR3);
                }
                BindPanel(dt, panFWJG);
                txtLXD_FWFYTJR.Text = username;
                lbLXD_FWFYTJSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }

        }

        private void BindPanel(DataTable dt, Panel panel)
        {
            for (int i = 0, length = panel.Controls.Count; i < length; i++)
            {
                if (panel.Controls[i] is TextBox)
                {
                    TextBox txt = (TextBox)panel.Controls[i];
                    if (dt.Columns.Contains(txt.ID.Substring(3)))
                    {
                        txt.Text = dt.Rows[0][txt.ID.Substring(3)].ToString();
                    }
                }
                if (panel.Controls[i] is Label)
                {
                    Label lb = (Label)panel.Controls[i];
                    if (dt.Columns.Contains(lb.ID.Substring(2)))
                    {
                        lb.Text = dt.Rows[0][lb.ID.Substring(2)].ToString();
                    }
                }
                if (panel.Controls[i] is RadioButtonList)
                {
                    RadioButtonList rbl = (RadioButtonList)panel.Controls[i];
                    if (dt.Columns.Contains(rbl.ID.Substring(3)))
                    {
                        for (int j = 0, a = rbl.Items.Count; j < a; j++)
                        {
                            if (rbl.Items[j].Value == dt.Rows[0][rbl.ID.Substring(3)].ToString())
                            {
                                rbl.Items[j].Selected = true;
                                break;
                            }
                        }
                    }
                }
                if (panel.Controls[i] is CheckBoxList)
                {
                    CheckBoxList cbxl = (CheckBoxList)panel.Controls[i];
                    if (dt.Columns.Contains(cbxl.ID.Substring(4)))
                    {
                        if (dt.Rows[0][cbxl.ID.Substring(4)].ToString().Contains('|') == true)
                        {
                            string[] str = dt.Rows[0][cbxl.ID.Substring(4)].ToString().Split('|');
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
                                if (cbxl.Items[k].Text == dt.Rows[0][cbxl.ID.Substring(4)].ToString())
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

        private void PowerControl()//绑定数据时的权限控制
        {
            Panel0.Enabled = false;
            Panel1.Enabled = false;
            Panel2.Enabled = false;
            panSPR1.Enabled = false;
            panFZBM.Enabled = false;
            panSPR2.Enabled = false;
            panSPR3.Enabled = false;
            panFWJG.Enabled = false;
            panFYTJ.Enabled = false;
            if (action == "add" || action == "alter")
            {
                Panel0.Enabled = true;
                Panel1.Enabled = true;
                Panel2.Enabled = true;
                panSPR1.Enabled = true;
            }
            else if (action == "check")
            {
                if (dt.Rows[0]["LXD_SPR1"].ToString() == username)
                {
                    panSPR1.Enabled = true;
                    panFZBM.Enabled = true;
                    panSPR2.Enabled = true;
                    panSPR3.Enabled = true;
                }
                else if (dt.Rows[0]["LXD_SPR2"].ToString() == username)
                {
                    panSPR2.Enabled = true;
                }
                else if (dt.Rows[0]["LXD_SPR3"].ToString() == username)
                {
                    panSPR3.Enabled = true;
                }
            }
            else if (action == "read")
            {
                btnSubmit.Visible = false;
                btnBack.Visible = false;
            }
            else if (action == "write")
            {
                panFWJG.Enabled = true;
            }
            else if (action == "count")
            {
                panFYTJ.Enabled = true;
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
                            sqlStr += "values('" + hidLXD_SJID.Value + "'";
                            sqlStr += ",'" + FilePath + "'";
                            sqlStr += ",'" + DateTime.Now.ToString("yyyy年MM月dd日") + "'";
                            sqlStr += ",'" + strNewFile + "','" + strOldFile + "',";
                            sqlStr += "'CM_SHLXD_JBXX')";
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
            string sql = "select * from CM_FILES where FILE_FATHERID='" + hidLXD_SJID.Value + "' and FILE_TYPE='CM_SHLXD_JBXX'";
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
                            sqlStr += "values('" + hidLXD_SJID.Value + "'";
                            sqlStr += ",'" + FilePath + "'";
                            sqlStr += ",'" + DateTime.Now.ToString("yyyy年MM月dd日") + "'";
                            sqlStr += ",'" + strNewFile + "','" + strOldFile + "',";
                            sqlStr += "'CM_SHLXD_CLJG')";
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
            string sql = "select * from CM_FILES where FILE_FATHERID='" + hidLXD_SJID.Value + "' and FILE_TYPE='CM_SHLXD_CLJG'";
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
            if (action == "add")
            {
                if (txtLXD_HTH.Text.Trim() == "")
                {
                    Response.Write("<script>alert('合同号不能为空，请填写合同号之后再提交')</script>");
                    return;
                }
                if (txtLXD_RWH.Text.Trim() == "")
                {
                    Response.Write("<script>alert('任务号不能为空，请填写任务号之后再提交')</script>");
                    return;
                }
                string sql = addsql();
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {

                    Response.Write("<script>alert('addsql语句出现问题，请联系管理员！！！')</script>");
                    return;
                }
            }
            if (action == "alter")
            {
                if (txtLXD_HTH.Text.Trim() == "")
                {
                    Response.Write("<script>alert('合同号不能为空，请填写合同号之后再提交')</script>");
                    return;
                }
                if (txtLXD_RWH.Text.Trim() == "")
                {
                    Response.Write("<script>alert('任务号不能为空，请填写任务号之后再提交')</script>");
                    return;
                }
                string sql = altersql();
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {
                    Response.Write("<script>alert('altersql语句出现问题，请联系管理员！！！')</script>");
                    return;
                }

            }
            if (action == "check")
            {
                if (username == dt.Rows[0]["LXD_SPR1"].ToString())
                {
                    if (rblLXD_SPR1_JL.SelectedValue != "1" && rblLXD_SPR1_JL.SelectedValue != "2")
                    {
                        Response.Write("<script>alert('未选择审批结果，不能提交，请选择同意或者不同意后再提交！！！')</script>");
                        return;
                    }
                    if (cbxlLXD_FZBM.SelectedValue == "")
                    {
                        Response.Write("<script>alert('未选择负责部门，不能提交，请选择负责部门之后后再提交！！！')</script>");
                        return;
                    }

                }
                if (username == dt.Rows[0]["LXD_SPR2"].ToString())
                {
                    if (rblLXD_SPR2_JL.SelectedValue != "1" && rblLXD_SPR2_JL.SelectedValue != "2")
                    {
                        Response.Write("<script>alert('未选择审批结果，不能提交，请选择同意或者不同意后再提交！！！')</script>");
                        return;
                    }
                }
                if (username == dt.Rows[0]["LXD_SPR3"].ToString())
                {
                    if (rblLXD_SPR3_JL.SelectedValue != "1" && rblLXD_SPR3_JL.SelectedValue != "2")
                    {
                        Response.Write("<script>alert('未选择审批结果，不能提交，请选择同意或者不同意后再提交！！！')</script>");
                        return;
                    }
                }
                string sql = checksql();
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {

                    Response.Write("<script>alert('checksql语句出现问题，请联系管理员！！！')</script>");
                    return;
                }
            }
            if (action == "write")
            {
                if (txtLXD_FWGC.Text.Trim() == "")
                {
                    Response.Write("<script>alert('未填写服务结果，请填写服务结果后再提交！！！')</script>");
                    return;
                }
                string sql = writesql();
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {

                    Response.Write("<script>alert('writesql语句出现问题，请联系管理员！！！')</script>");
                    return;
                }

            }
            if (action == "count")
            {
                List<string> list = new List<string>();
                for (int i = 0, length = rptFWFY.Items.Count; i < length; i++)
                {
                    string sqltext = "insert into CM_SHLXD_FY (";
                    sqltext += "FY_LXDID,FY_FYMC,FY_FYJE,FY_BZ";
                    sqltext += ")values(";
                    sqltext += "'" + id + "',";
                    sqltext += "'" + ((TextBox)rptFWFY.Items[i].FindControl("FY_FYMC")).Text.Trim() + "',";
                    sqltext += ((TextBox)rptFWFY.Items[i].FindControl("FY_FYJE")).Text.Trim() + ",";
                    sqltext += "'" + ((TextBox)rptFWFY.Items[i].FindControl("FY_BZ")).Text.Trim() + "'";
                    sqltext += ")";
                    list.Add(sqltext);
                }
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('插入服务费用时sql语句出现问题，请联系管理员')</script>");
                    return;
                }
                string sql = countsql();
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {

                    Response.Write("<script>alert('checksql语句出现问题，请联系管理员！！！')</script>");
                    return;
                }
            }
            Response.Redirect("CM_SHLXDGL.aspx");
        }

        private string addsql()
        {
            string sql = "insert into CM_SHLXD (";
            sql += "LXD_SJID,";
            sql += "LXD_BH,";//文件编号
            sql += "LXD_XMMC,";//项目名称
            sql += "LXD_HTH,";//合同号
            sql += "LXD_SBMC,";//设备名称
            sql += "LXD_GKMC,";//顾客名称
            sql += "LXD_RWH,";//任务号
            sql += "LXD_XXJJ,";//信息简介
            sql += "LXD_FWYQ,";//服务要求
            sql += "LXD_FWDZ,";//服务地址
            sql += "LXD_LXFS,";//联系方式
            sql += "LXD_SJYQ,";//时间要求
            sql += "LXD_ZDR,";//制单人
            sql += "LXD_ZDSJ,";//制单时间
            sql += "LXD_ZDJY,";//制单建议
            sql += "LXD_SPR1,";
            sql += "LXD_SPR1_JL,";
            sql += "LXD_SPZT";
            sql += ") values (";
            sql += "'" + hidLXD_SJID.Value + "',";
            sql += "'" + txtLXD_BH.Text.Trim() + "',";
            sql += "'" + txtLXD_XMMC.Text.Trim() + "',";
            sql += "'" + txtLXD_HTH.Text.Trim() + "',";
            sql += "'" + txtLXD_SBMC.Text.Trim() + "',";
            sql += "'" + txtLXD_GKMC.Text.Trim() + "',";
            sql += "'" + txtLXD_RWH.Text.Trim() + "',";
            sql += "'" + txtLXD_XXJJ.Text.Trim() + "',";
            sql += "'" + txtLXD_FWYQ.Text.Trim() + "',";
            sql += "'" + txtLXD_FWDZ.Text.Trim() + "',";
            sql += "'" + txtLXD_LXFS.Text.Trim() + "',";
            sql += "'" + txtLXD_SJYQ.Text.Trim() + "',";
            sql += "'" + txtLXD_ZDR.Text.Trim() + "',";
            sql += "'" + lbLXD_ZDSJ.Text + "',";
            sql += "'" + txtLXD_ZDJY.Text.Trim() + "',";
            sql += "'" + txtLXD_SPR1.Text.Trim() + "',";
            sql += "'0','0'";
            sql += ")";
            return sql;
        }//新增时的sql语句

        private string altersql()
        {
            string sql = "";
            sql += "update CM_SHLXD set ";
            sql += " LXD_BH='" + txtLXD_BH.Text.Trim() + "',";
            sql += "LXD_XMMC='" + txtLXD_XMMC.Text.Trim() + "',";
            sql += "LXD_HTH='" + txtLXD_HTH.Text.Trim() + "',";
            sql += "LXD_SBMC='" + txtLXD_SBMC.Text.Trim() + "',";
            sql += "LXD_GKMC='" + txtLXD_GKMC.Text.Trim() + "',";
            sql += "LXD_RWH='" + txtLXD_RWH.Text.Trim() + "',";
            sql += "LXD_XXJJ='" + txtLXD_XXJJ.Text.Trim() + "',";
            sql += "LXD_FWYQ='" + txtLXD_FWYQ.Text.Trim() + "',";
            sql += "LXD_FWDZ='" + txtLXD_FWDZ.Text.Trim() + "',";
            sql += "LXD_LXFS='" + txtLXD_LXFS.Text.Trim() + "',";
            sql += "LXD_SJYQ='" + txtLXD_SJYQ.Text.Trim() + "'";
            sql += " where LXD_ID='" + id + "'";
            return sql;
        }

        private string checksql()
        {
            string LXD_FZBM = "";
            string LXD_FZBMID = "";
            string sql = " update CM_SHLXD set ";
            if (username == dt.Rows[0]["LXD_SPR1"].ToString())
            {
                sql += "LXD_SPR1_JL='" + rblLXD_SPR1_JL.SelectedValue + "',";
                sql += "LXD_SPR1_SJ='" + lbLXD_SPR1_SJ.Text + "',";
                sql += "LXD_SPR1_JY='" + txtLXD_SPR1_JY.Text.Trim() + "',";
                if (rblLXD_SPR1_JL.SelectedValue == "1")
                {
                    sql += "LXD_SPR2_JL='0',";
                    foreach (ListItem item in cbxlLXD_FZBM.Items)
                    {
                        if (item.Selected == true)
                        {
                            LXD_FZBM += item.Text.Trim() + "|";
                            LXD_FZBMID += item.Value.Trim() + "|";
                        }
                    }
                    sql += "LXD_FZBM='" + LXD_FZBM.Trim('|') + "',";
                    sql += "LXD_FZBMID='" + LXD_FZBMID.Trim('|') + "',";
                    sql += "LXD_SPLX='" + rblLXD_SPLX.SelectedValue + "',";
                    if (rblLXD_SPLX.SelectedValue == "1")
                    {
                        sql += "LXD_SPR2='"+txtLXD_SPR2.Text.Trim()+"',";
                    }
                    if (rblLXD_SPLX.SelectedValue == "2")
                    {
                        sql += "LXD_SPR2='"+txtLXD_SPR2.Text.Trim()+"',";
                        sql += "LXD_SPR3='"+txtLXD_SPR3.Text.Trim()+"',";
                    }
                    sql += "LXD_SPZT='1'";//审批人1通过，审批人2初始化
                }
                if (rblLXD_SPR1_JL.SelectedValue == "2")
                {
                    sql += "LXD_SPZT='11'";//未通过
                }
            }
            if (username == dt.Rows[0]["LXD_SPR2"].ToString())
            {
                sql += "LXD_SPR2_JL='" + rblLXD_SPR2_JL.SelectedValue + "',";
                sql += "LXD_SPR2_SJ='" + lbLXD_SPR2_SJ.Text + "',";
                sql += "LXD_SPR2_JY='" + txtLXD_SPR2_JY.Text.Trim() + "',";
                if (rblLXD_SPR2_JL.SelectedValue == "1")
                {
                    sql += "LXD_SPR3_JL='0',";
                    if (dt.Rows[0]["LXD_SPLX"].ToString() == "1")
                    {
                        sql += "LXD_SPZT='10'";
                    }
                    if (dt.Rows[0]["LXD_SPLX"].ToString() == "2")
                    {
                        sql += "LXD_SPZT='2'";//审批人2通过,审批人三初始化
                    }
                }
                if (rblLXD_SPR2_JL.SelectedValue == "2")
                {
                    sql += "LXD_SPZT='11'";//未通过
                }
            }
            if (username == dt.Rows[0]["LXD_SPR3"].ToString())
            {
                sql += "LXD_SPR3_JL='" + rblLXD_SPR3_JL.SelectedValue + "',";
                sql += "LXD_SPR3_SJ='" + lbLXD_SPR3_SJ.Text + "',";
                sql += "LXD_SPR3_JY='" + txtLXD_SPR3_JY.Text.Trim() + "',";
                if (rblLXD_SPR3_JL.SelectedValue == "1")
                {
                    sql += "LXD_SPZT='10'";
                }
                if (rblLXD_SPR3_JL.SelectedValue == "2")
                {
                    sql += "LXD_SPZT='11'";//未通过
                }
            }
            sql += " where LXD_ID='" + id + "'";
            return sql;
        }

        private string writesql()
        {
            string sql = " update CM_SHLXD set ";
            sql += "LXD_FWGC='" + txtLXD_FWGC.Text.Trim() + "',";
            sql += "LXD_FWJGTXR='" + txtLXD_FWJGTXR.Text.Trim() + "',";
            sql += "LXD_FWJGTXSJ='" + lbLXD_FWJGTXSJ.Text + "',";
            sql += "LXD_FWFYDEPID='06',";
            sql += "LXD_SPZT='12'";//已填写处理方案
            sql += " where LXD_ID='" + id + "'";
            return sql;
        }

        private string countsql()
        {
            string sql = "update CM_SHLXD set ";
            sql += "LXD_FWZFY=" + txtLXD_FWZFY.Text.Trim() + ",";
            sql += "LXD_FWFYTJR='" + txtLXD_FWFYTJR.Text.Trim() + "',";
            sql += "LXD_FWFYTJSJ='" + lbLXD_FWFYTJSJ.Text + "',";
            sql += "LXD_SPZT='13'";//已填写费用统计
            sql += " where LXD_ID='" + id + "'";
            return sql;
        }

        private void SendEmail()
        {
            if (action == "add")
            {
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("47"), new List<string>(), new List<string>(), "服务联系单审批", "您有服务联系单“" + txtLXD_BH.Text.Trim() + "”需要审批，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给李利恒发邮件通知其审批
            }
            if (action == "check")
            {
                if (username == dt.Rows[0]["LXD_SPR1"].ToString() && rblLXD_SPR1_JL.SelectedValue == "1")
                {
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("2"), new List<string>(), new List<string>(), "服务联系单审批", "您有服务联系单“" + dt.Rows[0]["LXD_BH"].ToString() + "”需要审批，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给王福泉发邮件通知其审批
                }
                if (username == dt.Rows[0]["LXD_SPR2"].ToString() && rblLXD_SPR2_JL.SelectedValue == "1" && dt.Rows[0]["LXD_SPLX"].ToString() == "2")
                {
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("311"), new List<string>(), new List<string>(), "服务联系单审批", "您有服务联系单“" + dt.Rows[0]["LXD_BH"].ToString() + "”需要审批，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给周文轶发邮件通知其审批
                }
                if (username == dt.Rows[0]["LXD_SPR2"].ToString() && rblLXD_SPR2_JL.SelectedValue == "1" && dt.Rows[0]["LXD_SPLX"].ToString() == "1")
                {
                    if (dt.Rows[0]["LXD_FZBM"].ToString().Contains('|'))
                    {
                        string[] fzbmid = dt.Rows[0]["LXD_FZBMID"].ToString().Split('|');
                        for (int i = 0, a = fzbmid.Length; i < a; i++)
                        {
                            if (fzbmid[i] == "04")
                            {
                                string str = "select ST_ID from TBDS_STAFFINFO where ST_POSITION='0401' and ST_PD='0'";
                                System.Data.DataTable leader = DBCallCommon.GetDTUsingSqlText(str);
                                fzbmid[i] = "95";
                                if (leader.Rows.Count > 0)
                                {
                                    fzbmid[i] = leader.Rows[0][0].ToString();
                                }

                            }
                            if (fzbmid[i] == "03")
                            {
                                fzbmid[i] = "67";
                            }
                            if (fzbmid[i] == "05")
                            {
                                string str = "select ST_ID from TBDS_STAFFINFO where ST_POSITION='0501' and ST_PD='0'";
                                System.Data.DataTable leader = DBCallCommon.GetDTUsingSqlText(str);
                                fzbmid[i] = "72";
                                if (leader.Rows.Count > 0)
                                {
                                    fzbmid[i] = leader.Rows[0][0].ToString();
                                }
                            }
                            if (fzbmid[i] == "12")
                            {
                                fzbmid[i] = "69";
                            }
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(fzbmid[i]), new List<string>(), new List<string>(), "服务联系单处理过程及结果", "您有服务联系单“" + dt.Rows[0]["LXD_BH"].ToString() + "”需要填写处理过程及结果，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给负责部门发邮件
                        }

                    }
                    else
                    {
                        if (dt.Rows[0]["LXD_FZBMID"].ToString() == "04")
                        {
                            //2017.5.12修改，修改发给部长，没部长发给人本人通知
                            string str = "select ST_ID from TBDS_STAFFINFO where ST_POSITION='0401' and ST_PD='0'";
                            System.Data.DataTable leader = DBCallCommon.GetDTUsingSqlText(str);
                            string lead = "";
                            if (leader.Rows.Count > 0)
                            {
                                lead = leader.Rows[0][0].ToString();
                            }
                            if (!string.IsNullOrEmpty(lead))
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lead), new List<string>(), new List<string>(), "服务联系单处理过程及结果", "您有服务联系单“" + dt.Rows[0]["LXD_BH"].ToString() + "”需要填写处理过程及结果，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给负责部门发邮件
                            }
                            else
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Session["UserID"].ToString()), new List<string>(), new List<string>(), "服务联系单处理过程及结果", "生产部可能暂未设部长，请及时与采购负责人沟通！！！");
                            }
                        }
                        if (dt.Rows[0]["LXD_FZBM"].ToString() == "03")
                        {
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("146"), new List<string>(), new List<string>(), "服务联系单处理过程及结果", "您有服务联系单“" + dt.Rows[0]["LXD_BH"].ToString() + "”需要填写处理过程及结果，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给负责部门发邮件
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("67"), new List<string>(), new List<string>(), "服务联系单处理过程及结果", "您有服务联系单“" + dt.Rows[0]["LXD_BH"].ToString() + "”需要填写处理过程及结果，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给负责部门发邮件
                        }
                        if (dt.Rows[0]["LXD_FZBM"].ToString() == "12")
                        {
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("69"), new List<string>(), new List<string>(), "服务联系单处理过程及结果", "您有服务联系单“" + dt.Rows[0]["LXD_BH"].ToString() + "”需要填写处理过程及结果，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给负责部门发邮件

                        }
                        if (dt.Rows[0]["LXD_FZBM"].ToString() == "采购部")//此处采购部为原来设置，为测试是否有误
                        {
                            //2017.5.12修改，修改发给部长，没部长发给人本人通知
                            string str = "select ST_ID from TBDS_STAFFINFO where ST_POSITION='0501' and ST_PD='0'";
                            System.Data.DataTable leader = DBCallCommon.GetDTUsingSqlText(str);
                            string lead = "";
                            if (leader.Rows.Count > 0)
                            {
                                lead = leader.Rows[0][0].ToString();
                            }
                            if (!string.IsNullOrEmpty(lead))
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lead), new List<string>(), new List<string>(), "服务联系单处理过程及结果", "您有服务联系单“" + dt.Rows[0]["LXD_BH"].ToString() + "”需要填写处理过程及结果，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给负责部门发邮件
                            }
                            else
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Session["UserID"].ToString()), new List<string>(), new List<string>(), "服务联系单处理过程及结果", "采购部可能暂未设部长，请及时与采购负责人沟通！！！");
                            }
                        }
                    }
                }
                if (username == dt.Rows[0]["LXD_SPR3"].ToString() && rblLXD_SPR3_JL.SelectedValue == "1")
                {
                    if (dt.Rows[0]["LXD_FZBM"].ToString().Contains('|'))
                    {
                        string[] fzbmid = dt.Rows[0]["LXD_FZBMID"].ToString().Split('|');
                        for (int i = 0, a = fzbmid.Length; i < a; i++)
                        {
                            if (fzbmid[i] == "04")
                            {
                                string str = "select ST_ID from TBDS_STAFFINFO where ST_POSITION='0401' and ST_PD='0'";
                                System.Data.DataTable leader = DBCallCommon.GetDTUsingSqlText(str);
                                fzbmid[i] = "95";
                                if (leader.Rows.Count > 0)
                                {
                                    fzbmid[i] = leader.Rows[0][0].ToString();
                                }

                            }
                            if (fzbmid[i] == "03")
                            {
                                fzbmid[i] = "67";
                            }
                            if (fzbmid[i] == "05")
                            {
                                string str = "select ST_ID from TBDS_STAFFINFO where ST_POSITION='0501' and ST_PD='0'";
                                System.Data.DataTable leader = DBCallCommon.GetDTUsingSqlText(str);
                                fzbmid[i] = "72";
                                if (leader.Rows.Count > 0)
                                {
                                    fzbmid[i] = leader.Rows[0][0].ToString();
                                }
                            }
                            if (fzbmid[i] == "12")
                            {
                                fzbmid[i] = "69";
                            }
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(fzbmid[i]), new List<string>(), new List<string>(), "服务联系单处理过程及结果", "您有服务联系单“" + dt.Rows[0]["LXD_BH"].ToString() + "”需要填写处理过程及结果，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给负责部门发邮件
                        }

                    }
                    else
                    {
                        if (dt.Rows[0]["LXD_FZBMID"].ToString() == "04")
                        {
                            //2017.5.12修改，修改发给部长，没部长发给人本人通知
                            string str = "select ST_ID from TBDS_STAFFINFO where ST_POSITION='0401' and ST_PD='0'";
                            System.Data.DataTable leader = DBCallCommon.GetDTUsingSqlText(str);
                            string lead = "";
                            if (leader.Rows.Count > 0)
                            {
                                lead = leader.Rows[0][0].ToString();
                            }
                            if (!string.IsNullOrEmpty(lead))
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lead), new List<string>(), new List<string>(), "服务联系单处理过程及结果", "您有服务联系单“" + dt.Rows[0]["LXD_BH"].ToString() + "”需要填写处理过程及结果，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给负责部门发邮件
                            }
                            else
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Session["UserID"].ToString()), new List<string>(), new List<string>(), "服务联系单处理过程及结果", "生产部可能暂未设部长，请及时与采购负责人沟通！！！");
                            }
                        }
                        if (dt.Rows[0]["LXD_FZBM"].ToString() == "03")
                        {
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("146"), new List<string>(), new List<string>(), "服务联系单处理过程及结果", "您有服务联系单“" + dt.Rows[0]["LXD_BH"].ToString() + "”需要填写处理过程及结果，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给负责部门发邮件
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("67"), new List<string>(), new List<string>(), "服务联系单处理过程及结果", "您有服务联系单“" + dt.Rows[0]["LXD_BH"].ToString() + "”需要填写处理过程及结果，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给负责部门发邮件
                        }
                        if (dt.Rows[0]["LXD_FZBM"].ToString() == "12")
                        {
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("69"), new List<string>(), new List<string>(), "服务联系单处理过程及结果", "您有服务联系单“" + dt.Rows[0]["LXD_BH"].ToString() + "”需要填写处理过程及结果，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给负责部门发邮件
                       
                        }
                        if (dt.Rows[0]["LXD_FZBM"].ToString() == "采购部")//此处采购部为原来设置，为测试是否有误
                        {
                            //2017.5.12修改，修改发给部长，没部长发给人本人通知
                            string str = "select ST_ID from TBDS_STAFFINFO where ST_POSITION='0501' and ST_PD='0'";
                            System.Data.DataTable leader = DBCallCommon.GetDTUsingSqlText(str);
                            string lead = "";
                            if (leader.Rows.Count > 0)
                            {
                                lead = leader.Rows[0][0].ToString();
                            }
                            if (!string.IsNullOrEmpty(lead))
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lead), new List<string>(), new List<string>(), "服务联系单处理过程及结果", "您有服务联系单“" + dt.Rows[0]["LXD_BH"].ToString() + "”需要填写处理过程及结果，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给负责部门发邮件
                            }
                            else
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Session["UserID"].ToString()), new List<string>(), new List<string>(), "服务联系单处理过程及结果", "采购部可能暂未设部长，请及时与采购负责人沟通！！！");
                            }
                        }
                    }
                }
            }
            if (action == "write")
            {
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("256"), new List<string>(), new List<string>(), "服务联系单服务费用统计", "您有服务联系单“" + dt.Rows[0]["LXD_BH"].ToString() + "”需要统计服务费用，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");
            }
            if (action == "count")
            {
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("47"), new List<string>(), new List<string>(), "服务联系单通知", "服务联系单“" + txtLXD_BH.Text.Trim() + "”已处理完毕，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给李利恒发邮件通知其审批
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("2"), new List<string>(), new List<string>(), "服务联系单审批", "服务联系单“" + dt.Rows[0]["LXD_BH"].ToString() + "”已处理完毕，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给王福泉发邮件通知其审批
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("311"), new List<string>(), new List<string>(), "服务联系单审批", "服务联系单“" + dt.Rows[0]["LXD_BH"].ToString() + "”已处理完毕，请登录系统,进入市场管理模块的“服务联系单页面”进行查看。");//给周文轶发邮件通知其审批
            }
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("CM_SHLXDGL.aspx");
        }
    }
}
