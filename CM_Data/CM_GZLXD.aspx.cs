using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using Microsoft.Office.Interop.MSProject;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_GZLXD : System.Web.UI.Page
    {
        string action = string.Empty;
        string id = string.Empty;
        string username = string.Empty;
        string depid = string.Empty;
        string sqltext = string.Empty;
        DataTable dts = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];
            id = Request.QueryString["id"];
            username = Session["UserName"].ToString();
            depid = Session["UserDeptID"].ToString();
            if (action != "add")
            {
                sqltext = "select * from CM_GZLXD where LXD_ID=" + id;
                try
                {
                    dts = DBCallCommon.GetDTUsingSqlText(sqltext);
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
                PowerControl();
            }
        }
        private void BindData()
        {
            if (action == "add")
            {
                hidLXD_SJID.Value = DateTime.Now.ToString("yyyy-MM-dd:HH-mm-ss-fff");
                txtLXD_ZDR.Text = username;
                lbLXD_ZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                DataTable dt = new DataTable();
                rptGZLXD.DataSource = dt;
                rptGZLXD.DataBind();
                rptNR.DataSource = dt;
                rptNR.DataBind();
            }
            else if (action == "check")
            {
                hidLXD_SJID.Value = dts.Rows[0]["LXD_SJID"].ToString();
                BindPanel(panJBXX);
                BindrptNR();
                BindPanel(panZDR);
                BindPanel(panSPR1);
                BindPanel(panSPR2);
                BindrptGZLXD();
                if (dts.Rows[0]["LXD_SPR1"].ToString() == username)
                {
                    lbLXD_SPR1_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (dts.Rows[0]["LXD_SPR2"].ToString() == username)
                {
                    lbLXD_SPR2_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else
            {
                hidLXD_SJID.Value = dts.Rows[0]["LXD_SJID"].ToString();
                BindPanel(panJBXX);
                BindrptNR();
                BindPanel(panZDR);
                BindPanel(panSPR1);
                BindPanel(panSPR2);
                BindrptGZLXD();
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

        private void BindrptNR()
        {
            string sql = "select * from CM_GZLXD_NR where NR_FATHERID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            rptNR.DataSource = dt;
            rptNR.DataBind();
        }

        private void PowerControl()
        {
            btnRefuse.Visible = false;
            btnDelete1.Visible = true;
            btnSubmit.Visible = true;
            btnBack.Visible = true;
            panJBXX.Enabled = false;
            panZDR.Enabled = false;
            panSPR1.Enabled = false;
            panSPR2.Enabled = false;
            if (action == "add" || action == "alter")
            {
                btnDelete1.Visible = false;
                panJBXX.Enabled = true;
                panZDR.Enabled = true;
                panSPR1.Enabled = true;
                panSPR2.Enabled = true;
            }
            else if (action == "check")
            {
                btnDelete1.Visible = false;
                if (dts.Rows[0]["LXD_SPZT"].ToString() == "0")//未审批
                {
                    if (username == dts.Rows[0]["LXD_SPR1"].ToString())
                    {
                        panSPR1.Enabled = true;
                    }
                }
                else if (dts.Rows[0]["LXD_SPZT"].ToString() == "1")//审批人1通过
                {
                    if (username == dts.Rows[0]["LXD_SPR2"].ToString() && dts.Rows[0]["LXD_SPLX"].ToString() == "2")
                    {
                        panSPR2.Enabled = true;
                    }
                }
            }
            else if (action == "delete")
            {
                btnSubmit.Visible = false;
            }
            else if (action == "read")
            {
                btnDelete1.Visible = false;
                btnSubmit.Visible = false;
                btnBack.Visible = false;
            }
            else if (action=="refuse")
            {
                btnRefuse.Visible = true;
                btnDelete1.Visible = false;
                btnSubmit.Visible = false;
            }
        }

        # region  上传文件-工作联系单附件
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
                            sqlStr += "'CM_GZLXD')";
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
            BindrptGZLXD();
        }

        private void BindrptGZLXD()
        {
            string sql = "select * from CM_FILES where FILE_FATHERID='" + hidLXD_SJID.Value + "' and FILE_TYPE='CM_GZLXD'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            rptGZLXD.DataSource = dt;
            rptGZLXD.DataBind();
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
            BindrptGZLXD();//删除添加的记录
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
            this.rptNR.DataSource = dt;
            this.rptNR.DataBind();
            //InitVar();
        }

        private DataTable GetDataTable() //临时表
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NR_SBMC");
            dt.Columns.Add("NR_TH");
            dt.Columns.Add("NR_SL");
            dt.Columns.Add("NR_BZ");
            foreach (RepeaterItem retItem in rptNR.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)retItem.FindControl("NR_SBMC")).Text;
                newRow[1] = ((TextBox)retItem.FindControl("NR_TH")).Text;
                newRow[2] = ((TextBox)retItem.FindControl("NR_SL")).Text;
                newRow[3] = ((TextBox)retItem.FindControl("NR_BZ")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)//删除一行
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NR_SBMC");
            dt.Columns.Add("NR_TH");
            dt.Columns.Add("NR_SL");
            dt.Columns.Add("NR_BZ");
            foreach (RepeaterItem retItem in rptNR.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((TextBox)retItem.FindControl("NR_SBMC")).Text;
                    newRow[1] = ((TextBox)retItem.FindControl("NR_TH")).Text;
                    newRow[2] = ((TextBox)retItem.FindControl("NR_SL")).Text;
                    newRow[3] = ((TextBox)retItem.FindControl("NR_BZ")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.rptNR.DataSource = dt;
            this.rptNR.DataBind();
            NoDataPanelSee();
        }
        private void NoDataPanelSee()
        {
            if (rptNR.Items.Count > 0)
            {
                NoDataPanel.Visible = false;
            }
            else
            {
                NoDataPanel.Visible = true;
            }
        }
        #endregion

        protected void btnSubmit_onserverclick(object sender, EventArgs e)
        {
            if (action == "add")
            {
                if (rblLXD_SPLX.SelectedValue != "1" && rblLXD_SPLX.SelectedValue != "2")
                {
                    Response.Write("<script type='text/javascript'>alert('请选择审批类型后再提交！！！')</script>");
                    return;
                }
                string sql = addsql();
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {

                    Response.Write("<script type='text/javascript'>alert('addsql出现问题，请与管理员联系！！！')</script>");
                    return;
                }
                string sql1 = "select LXD_ID from CM_GZLXD where LXD_SJID='" + hidLXD_SJID.Value + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
                List<string> list = addlist(dt.Rows[0][0].ToString());
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script type='text/javascript'>alert('addlist出现问题，请与管理员联系！！！')</script>");
                    return;
                }
            }
            if (action == "alter")
            {
                if (rblLXD_SPLX.SelectedValue != "1" && rblLXD_SPLX.SelectedValue != "2")
                {
                    Response.Write("<script type='text/javascript'>alert('请选择审批类型后再提交！！！')</script>");
                    return;
                }
                List<string> list = alterlist();
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {

                    Response.Write("<script type='text/javascript'>alert('alterlist出现问题，请与管理员联系！！！')</script>");
                    return;
                }
            }
            if (action == "check")
            {
                if (username == dts.Rows[0]["LXD_SPR1"].ToString())
                {
                    if (rblLXD_SPR1_JL.SelectedValue != "1" && rblLXD_SPR1_JL.SelectedValue != "2")
                    {
                        Response.Write("<script type='text/javascript'>alert('请选择“同意”或“不同意”之后再提交')</script>");
                        return;
                    }
                }
                if (username == dts.Rows[0]["LXD_SPR2"].ToString())
                {
                    if (rblLXD_SPR2_JL.SelectedValue != "1" && rblLXD_SPR2_JL.SelectedValue != "2")
                    {
                        Response.Write("<script type='text/javascript'>alert('请选择“同意”或“不同意”之后再提交')</script>");
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

                    Response.Write("<script type='text/javascript'>alert('checksql出现问题，请与管理员联系！！！')</script>");
                    return;
                }
            }
            SendEmail();
            Response.Redirect("CM_GZLXDGL.aspx");
        }

        private string addsql()
        {
            string sql = "insert into CM_GZLXD (LXD_BH,";
            sql += "LXD_ZT,";
            sql += "LXD_DHDW,";
            sql += "LXD_XMMC,";
            sql += "LXD_HTH,";
            sql += "LXD_NR,";
            sql += "LXD_CSBM,";
            sql += "LXD_CSBMID,";
            sql += "LXD_SPLX,";
            sql += "LXD_ZDR,";
            sql += "LXD_ZDSJ,";
            sql += "LXD_ZDJY,";
            if (rblLXD_SPLX.SelectedValue == "1")
            {
                sql += "LXD_SPR1,";
            }
            else
            {
                sql += "LXD_SPR1,";
                sql += "LXD_SPR2,";
            }
            sql += "LXD_SPZT,";
            sql += "LXD_SJID";
            sql += ") values (";
            sql += "'" + txtLXD_BH.Text.Trim() + "',";
            sql += "'" + txtLXD_ZT.Text.Trim() + "',";
            sql += "'" + txtLXD_DHDW.Text.Trim() + "',";
            sql += "'" + txtLXD_XMMC.Text.Trim() + "',";
            sql += "'" + txtLXD_HTH.Text.Trim() + "',";
            sql += "'" + txtLXD_NR.Text.Trim() + "',";
            string LXD_CSBM = "";
            string LXD_CSBMID = "";
            foreach (ListItem item in cbxlLXD_CSBM.Items)
            {
                if (item.Selected)
                {
                    LXD_CSBM += item.Text + "|";
                    LXD_CSBMID += item.Value + "|";
                }
            }
            sql += "'" + LXD_CSBM.Trim('|') + "',";
            sql += "'" + LXD_CSBMID.Trim('|') + "',";
            sql += "'" + rblLXD_SPLX.SelectedValue + "',";
            sql += "'" + txtLXD_ZDR.Text.Trim() + "',";
            sql += "'" + lbLXD_ZDSJ.Text.Trim() + "',";
            sql += "'" + txtLXD_ZDJY.Text.Trim() + "',";
            if (rblLXD_SPLX.SelectedValue == "1")
            {
                sql += "'" + txtLXD_SPR1.Text.Trim() + "',";
            }
            else
            {
                sql += "'" + txtLXD_SPR1.Text.Trim() + "',";
                sql += "'" + txtLXD_SPR2.Text.Trim() + "',";
            }
            sql += "'0',";
            sql += "'" + hidLXD_SJID.Value + "')";
            return sql;
        }

        private List<string> addlist(string fatherid)
        {
            List<string> list = new List<string>();
            for (int i = 0, length = rptNR.Items.Count; i < length; i++)
            {
                string sql1 = "insert into CM_GZLXD_NR (";
                sql1 += "NR_FATHERID,";
                sql1 += "NR_SBMC,";
                sql1 += "NR_TH,";
                sql1 += "NR_SL,";
                sql1 += "NR_BZ";
                sql1 += ") values (";
                sql1 += "'" + fatherid + "',";
                sql1 += "'" + ((TextBox)rptNR.Items[i].FindControl("NR_SBMC")).Text.Trim() + "',";
                sql1 += "'" + ((TextBox)rptNR.Items[i].FindControl("NR_TH")).Text.Trim() + "',";
                sql1 += "'" + ((TextBox)rptNR.Items[i].FindControl("NR_SL")).Text.Trim() + "',";
                sql1 += "'" + ((TextBox)rptNR.Items[i].FindControl("NR_BZ")).Text.Trim() + "')";
                list.Add(sql1);
            }
            return list;
        }

        private List<string> alterlist()
        {
            List<string> list = new List<string>();
            string sql = "update CM_GZLXD set ";
            sql += "LXD_BH='" + txtLXD_BH.Text.Trim() + "',";
            sql += "LXD_ZT='" + txtLXD_ZT.Text.Trim() + "',";
            sql += "LXD_DHDW='" + txtLXD_DHDW.Text.Trim() + "',";
            sql += "LXD_XMMC='" + txtLXD_XMMC.Text.Trim() + "',";
            sql += "LXD_HTH='" + txtLXD_HTH.Text.Trim() + "',";
            sql += "LXD_NR='" + txtLXD_NR.Text.Trim() + "',";
            string LXD_CSBM = "";
            string LXD_CSBMID = "";
            foreach (ListItem item in cbxlLXD_CSBM.Items)
            {
                if (item.Selected)
                {
                    LXD_CSBM += item.Text + "|";
                    LXD_CSBMID += item.Value + "|";
                }
            }
            sql += "LXD_CSBM='" + LXD_CSBM.Trim('|') + "',";
            sql += "LXD_CSBMID='" + LXD_CSBMID.Trim('|') + "',";
            sql += "LXD_SPLX='" + rblLXD_SPLX.SelectedValue + "',";
            sql += "LXD_ZDSJ='" + lbLXD_ZDSJ.Text.Trim() + "',";
            sql += "LXD_ZDJY='" + txtLXD_ZDJY.Text.Trim() + "',";
            if (rblLXD_SPLX.SelectedValue == "1")
            {
                sql += "LXD_SPR1='" + txtLXD_SPR1.Text.Trim() + "',";
            }
            else
            {
                sql += "LXD_SPR1='" + txtLXD_SPR1.Text.Trim() + "',";
                sql += "LXD_SPR2='" + txtLXD_SPR2.Text.Trim() + "',";
            }
            sql += "LXD_SPZT='0'";
            sql += " where LXD_ID=" + id;
            list.Add(sql);
            sql = "delete from CM_GZLXD_NR where NR_FATHERID='" + id + "'";
            list.Add(sql);

            for (int i = 0, length = rptNR.Items.Count; i < length; i++)
            {
                string sql1 = "insert into CM_GZLXD_NR (";
                sql1 += "NR_FATHERID,";
                sql1 += "NR_SBMC,";
                sql1 += "NR_TH,";
                sql1 += "NR_SL,";
                sql1 += "NR_BZ";
                sql1 += ") values (";
                sql1 += "'" + id + "',";
                sql1 += "'" + ((TextBox)rptNR.Items[i].FindControl("NR_SBMC")).Text.Trim() + "',";
                sql1 += "'" + ((TextBox)rptNR.Items[i].FindControl("NR_TH")).Text.Trim() + "',";
                sql1 += "'" + ((TextBox)rptNR.Items[i].FindControl("NR_SL")).Text.Trim() + "',";
                sql1 += "'" + ((TextBox)rptNR.Items[i].FindControl("NR_BZ")).Text.Trim() + "')";
                list.Add(sql1);
            }
            return list;
        }

        private string checksql()
        {
            string sql = "update CM_GZLXD set ";
            if (dts.Rows[0]["LXD_SPLX"].ToString() == "1")
            {
                if (dts.Rows[0]["LXD_SPR1"].ToString() == username)
                {
                    sql += "LXD_SPR1_JL='" + rblLXD_SPR1_JL.SelectedValue + "',";
                    sql += "LXD_SPR1_SJ='" + lbLXD_SPR1_SJ.Text + "',";
                    sql += "LXD_SPR1_JY='" + txtLXD_SPR1_JY.Text.Trim() + "',";
                    if (rblLXD_SPR1_JL.SelectedValue == "1")
                    {
                        sql += "LXD_SPZT='10'";//审批通过
                    }
                    else
                    {
                        sql += "LXD_SPZT='11'";//审批未通过
                    }
                }
            }
            else if (dts.Rows[0]["LXD_SPLX"].ToString() == "2")
            {
                if (dts.Rows[0]["LXD_SPR1"].ToString() == username)
                {
                    sql += "LXD_SPR1_JL='" + rblLXD_SPR1_JL.SelectedValue + "',";
                    sql += "LXD_SPR1_SJ='" + lbLXD_SPR1_SJ.Text + "',";
                    sql += "LXD_SPR1_JY='" + txtLXD_SPR1_JY.Text.Trim() + "',";
                    if (rblLXD_SPR1_JL.SelectedValue == "1")
                    {
                        sql += "LXD_SPZT='1'";//审批人1通过，审批人2初始化
                    }
                    else
                    {
                        sql += "LXD_SPZT='11'";//审批未通过
                    }
                }
                else if (dts.Rows[0]["LXD_SPR2"].ToString() == username)
                {
                    sql += "LXD_SPR2_JL='" + rblLXD_SPR2_JL.SelectedValue + "',";
                    sql += "LXD_SPR2_SJ='" + lbLXD_SPR2_SJ.Text + "',";
                    sql += "LXD_SPR2_JY='" + txtLXD_SPR2_JY.Text.Trim() + "',";
                    if (rblLXD_SPR2_JL.SelectedValue == "1")
                    {
                        sql += "LXD_SPZT='10'";
                    }
                    else
                    {
                        sql += "LXD_SPZT='11'";
                    }
                }
            }
            sql += " where LXD_ID="+id;
            return sql;
        }

        private void SendEmail()
        {

            sqltext = "select * from CM_GZLXD where LXD_ID=" + id;
            try
            {
                dts = DBCallCommon.GetDTUsingSqlText(sqltext);
            }
            catch
            {
                Response.Write("<script type='text/javascript'>alert('sqltext语句出现问题，请与管理员联系')</script>");
                return;
            }
            if (action == "add" || action == "alter"&&rblLXD_SPLX.SelectedValue=="2")
            {
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("86"), new List<string>(), new List<string>(), "工作联系单审批", "您有工作联系单“" + txtLXD_BH.Text + "”需要审批，请登录系统,进入市场管理模块的“工作联系单”进行查看。");//给李玲发邮件
            }
            if (action == "check")
            {
                if (username == dts.Rows[0]["LXD_SPR1"].ToString() && rblLXD_SPR1_JL.SelectedValue == "1"&&dts.Rows[0]["LXD_SPLX"].ToString()=="2")
                {
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("47"), new List<string>(), new List<string>(), "工作联系单审批", "您有工作联系单“" + dts.Rows[0]["LXD_BH"].ToString() + "”需要审批，请登录系统,进入市场管理模块的“工作联系单”进行查看。");//给李利恒发邮件
                }
                if (dts.Rows[0]["LXD_SPZT"].ToString() == "10")
                {
                    if (dts.Rows[0]["LXD_CSBMID"].ToString().Contains('|'))
                    {
                        string[] csbm = dts.Rows[0]["LXD_CSBMID"].ToString().Split('|');
                        if (csbm.Contains("03"))
                        {
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("146"), new List<string>(), new List<string>(), "工作联系单通知", "您有工作联系单“" + dts.Rows[0]["LXD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“工作联系单”进行查看。");
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("67"), new List<string>(), new List<string>(), "工作联系单通知", "您有工作联系单“" + dts.Rows[0]["LXD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“工作联系单”进行查看。");
                        }
                         if (csbm.Contains("04"))
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
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lead), new List<string>(), new List<string>(), "工作联系单审批", "您有工作联系单“" + dts.Rows[0]["LXD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“工作联系单”进行查看。");
                            }
                            else
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Session["UserID"].ToString()), new List<string>(), new List<string>(), "系统提醒", "生产部可能暂未设部长，请及时与生产负责人沟通！！！");
                            }
                        }
                         if (csbm.Contains("05"))
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
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lead), new List<string>(), new List<string>(), "工作联系单审批", "您有工作联系单“" + dts.Rows[0]["LXD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“工作联系单”进行查看。");
                            }
                            else
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Session["UserID"].ToString()), new List<string>(), new List<string>(), "系统提醒", "采购部可能暂未设部长，请及时与采购负责人沟通！！！");
                            }
                        }
                         if (csbm.Contains("06"))
                        {
                             string str = "select ST_ID from TBDS_STAFFINFO where ST_POSITION='0601' and ST_PD='0'";
                            System.Data.DataTable leader = DBCallCommon.GetDTUsingSqlText(str);
                            string lead = "";
                            if (leader.Rows.Count > 0)
                            {
                                lead = leader.Rows[0][0].ToString();
                            }
                            if (!string.IsNullOrEmpty(lead))
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lead), new List<string>(), new List<string>(), "工作联系单审批", "您有工作联系单“" + dts.Rows[0]["LXD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“工作联系单”进行查看。");
                            }
                            else
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Session["UserID"].ToString()), new List<string>(), new List<string>(), "系统提醒", "财务部可能暂未设部长，请及时与财务负责人沟通！！！");
                            }
                        }
                         if (csbm.Contains("12"))
                        {
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("69"), new List<string>(), new List<string>(), "工作联系单审批", "您有工作联系单“" + dts.Rows[0]["LXD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“工作联系单”进行查看。");

                        }
                    }
                    else
                    {
                        if (dts.Rows[0]["LXD_CSBMID"].ToString() == "03")
                        {
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("146"), new List<string>(), new List<string>(), "工作联系单通知", "您有工作联系单“" + dts.Rows[0]["LXD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“工作联系单”进行查看。");
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("67"), new List<string>(), new List<string>(), "工作联系单通知", "您有工作联系单“" + dts.Rows[0]["LXD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“工作联系单”进行查看。");
                        }
                        else if (dts.Rows[0]["LXD_CSBMID"].ToString() == "04")
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
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lead), new List<string>(), new List<string>(), "工作联系单审批", "您有工作联系单“" + dts.Rows[0]["LXD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“工作联系单”进行查看。");
                            }
                            else
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Session["UserID"].ToString()), new List<string>(), new List<string>(), "系统提醒", "生产部可能暂未设部长，请及时与生产负责人沟通！！！");
                            }
                        }
                        else if (dts.Rows[0]["LXD_CSBMID"].ToString() == "05")
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
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lead), new List<string>(), new List<string>(), "工作联系单审批", "您有工作联系单“" + dts.Rows[0]["LXD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“工作联系单”进行查看。");
                            }
                            else
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Session["UserID"].ToString()), new List<string>(), new List<string>(), "系统提醒", "采购部可能暂未设部长，请及时与采购负责人沟通！！！");
                            }
                        }
                        else if (dts.Rows[0]["LXD_CSBMID"].ToString() == "06")
                        {
                            string str = "select ST_ID from TBDS_STAFFINFO where ST_POSITION='0601' and ST_PD='0'";
                            System.Data.DataTable leader = DBCallCommon.GetDTUsingSqlText(str);
                            string lead = "";
                            if (leader.Rows.Count > 0)
                            {
                                lead = leader.Rows[0][0].ToString();
                            }
                            if (!string.IsNullOrEmpty(lead))
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lead), new List<string>(), new List<string>(), "工作联系单审批", "您有工作联系单“" + dts.Rows[0]["LXD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“工作联系单”进行查看。");
                            }
                            else
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Session["UserID"].ToString()), new List<string>(), new List<string>(), "系统提醒", "财务部可能暂未设部长，请及时与财务负责人沟通！！！");
                            }
                        }
                        else if (dts.Rows[0]["LXD_CSBMID"].ToString() == "12")
                        {
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("69"), new List<string>(), new List<string>(), "工作联系单审批", "您有工作联系单“" + dts.Rows[0]["LXD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“工作联系单”进行查看。");
                        }
                    }
                }
            }
        }

        protected void btnDelete1_onserverclick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string sql = "delete from CM_GZLXD where LXD_ID=" + id;
            list.Add(sql);
            sql = " delete from CM_GZLXD_NR where NR_FATHERID='" + id + "'";
            list.Add(sql);
            try
            {
                DBCallCommon.ExecuteTrans(list);
            }
            catch
            {
                Response.Write("<script type='text/javascript'>alert('删除的sql语句出现问题，请与管理员联系！！！')</script>");
                return;
            }
            Response.Redirect("CM_GZLXDGL.aspx");
        }

        protected void btnRefuse_onserverclick(object sender, EventArgs e)
        {
            string sql = "update CM_GZLXD set LXD_SPR1_JL=null,LXD_SPR1_SJ=null,LXD_SPR1_JY=null,";
            sql += "LXD_SPR2_JL=null,LXD_SPR2_SJ=null,LXD_SPR2_JY=null,LXD_SPZT='0'";
            sql += " where LXD_ID="+id;
            try
            {
                DBCallCommon.ExeSqlText(sql);
            }
            catch 
            {
                Response.Write("<script type='text/javascript'>alert('驳回的sql语句出现问题，请与管理员联系！！！')</script>");
                return;
            }
            Response.Redirect("CM_GZLXDGL.aspx");
        }

        protected void btnBack_onserverclick(object sender, EventArgs e)
        {
            Response.Redirect("CM_GZLXDGL.aspx");
        }
    }
}
