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
    public partial class CM_HTBGTZD : System.Web.UI.Page
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
                sqltext = "select * from CM_HTBGTZD where TZD_ID=" + id;
                try
                {
                    dts = DBCallCommon.GetDTUsingSqlText(sqltext);
                    hidTZD_SJID.Value = dts.Rows[0]["TZD_SJID"].ToString();
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
                hidTZD_SJID.Value = DateTime.Now.ToString("yyyy-MM-dd:HH-mm-ss-fff");
                txtTZD_ZDR.Text = username;
                lbTZD_ZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                DataTable dt = new DataTable();
                rptHTBG.DataSource = dt;
                rptHTBG.DataBind();
            }
            else if (action == "alter")
            {
                BindrptHTBG();
                BindPanel(panJBXX);
                BindPanel(panZDR);
                BindPanel(panSPR1);
                if (rblTZD_SPLX.SelectedValue == "2")
                {
                    BindPanel(panSPR2);
                }
            }
            else if (action == "check")
            {
                BindrptHTBG();
                BindPanel(panJBXX);
                BindPanel(panZDR);
                BindPanel(panSPR1);
                if (rblTZD_SPLX.SelectedValue == "2")
                {
                    BindPanel(panSPR2);
                }
                if (username == dts.Rows[0]["TZD_SPR1"].ToString())
                {
                    lbTZD_SPR1_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else if (username == dts.Rows[0]["TZD_SPR2"].ToString())
                {
                    lbTZD_SPR2_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else if (action == "refuse" || action == "read" || action == "delete")
            {
                BindrptHTBG();
                BindPanel(panJBXX);
                BindPanel(panZDR);
                BindPanel(panSPR1);
                if (rblTZD_SPLX.SelectedValue == "2")
                {
                    BindPanel(panSPR2);
                }
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

        private void PowerControl()
        {
            btnRefuse.Visible = false;
            btnDelete.Visible = false;
            btnSubmit.Visible = false;
            btnBack.Visible = false;
            panJBXX.Enabled = false;
            panZDR.Enabled = false;
            panSPR1.Enabled = false;
            panSPR2.Enabled = false;
            if (action == "add" || action == "alter")
            {
                btnSubmit.Visible = true;
                btnBack.Visible = true;
                panJBXX.Enabled = true;
                panZDR.Enabled = true;
                panSPR1.Enabled = true;
                panSPR2.Enabled = true;
            }
            else if (action == "check")
            {
                btnSubmit.Visible = true;
                btnBack.Visible = true;
                if (username == dts.Rows[0]["TZD_SPR1"].ToString())
                {
                    panSPR1.Enabled = true;
                }
                else if (username == dts.Rows[0]["TZD_SPR2"].ToString())
                {
                    panSPR2.Enabled = true;
                }
            }
            else if (action == "refuse")
            {
                btnRefuse.Visible = true;
            }
            else if (action == "delete")
            {
                btnDelete.Visible = true;
            }
        }



        # region  上传文件-合同变更通知
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
                            sqlStr += "values('" + hidTZD_SJID.Value + "'";
                            sqlStr += ",'" + FilePath + "'";
                            sqlStr += ",'" + DateTime.Now.ToString("yyyy年MM月dd日") + "'";
                            sqlStr += ",'" + strNewFile + "','" + strOldFile + "',";
                            sqlStr += "'CM_HTBGTZD')";
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
            BindrptHTBG();
        }

        private void BindrptHTBG()
        {
            string sql = "select * from CM_FILES where FILE_FATHERID='" + hidTZD_SJID.Value + "' and FILE_TYPE='CM_HTBGTZD'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            rptHTBG.DataSource = dt;
            rptHTBG.DataBind();
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
            BindrptHTBG();//删除添加的记录
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

        protected void btnSubmit_onserverclick(object sender, EventArgs e)
        {
            if (action == "add")
            {
                if (rblTZD_SPLX.SelectedValue != "1" & rblTZD_SPLX.SelectedValue != "2")
                {
                    Response.Write("<script type='text/javascript'>alert('请选择审批类型后再提交')</script>");
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
            }
            else if (action == "alter")
            {
                if (rblTZD_SPLX.SelectedValue != "1" & rblTZD_SPLX.SelectedValue != "2")
                {
                    Response.Write("<script type='text/javascript'>alert('请选择审批类型后再提交')</script>");
                    return;
                }
                string sql = altersql();
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {
                    Response.Write("<script type='text/javascript'>alert('altersql出现问题，请与管理员联系！！！')</script>");
                    return;
                }

            }
            else if (action == "check")
            {
                if (username == dts.Rows[0]["TZD_SPR1"].ToString())
                {
                    if (rblTZD_SPR1_JL.SelectedValue != "1" & rblTZD_SPR1_JL.SelectedValue != "2")
                    {
                        Response.Write("<script type='text/javascript'>alert('请选择“同意”或“不同意”后再提交！！！')</script>");
                        return;
                    }
                }
                else if (username == dts.Rows[0]["TZD_SPR2"].ToString())
                {
                    if (rblTZD_SPR2_JL.SelectedValue != "1" & rblTZD_SPR2_JL.SelectedValue != "2")
                    {
                        Response.Write("<script type='text/javascript'>alert('请选择“同意”或“不同意”后再提交！！！')</script>");
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
            Response.Redirect("CM_HTBGTZDGL.aspx");
        }

        private string addsql()
        {
            string sql = "insert into CM_HTBGTZD (";
            sql += "TZD_SJID,";
            sql += "TZD_BH,";
            sql += "TZD_HTH,";
            sql += "TZD_XMMC,";
            sql += "TZD_GKMC,";
            sql += "TZD_NR,";
            string csbm = "";
            string csbmid = "";
            foreach (ListItem item in cbxlTZD_CSBM.Items)
            {
                if (item.Selected)
                {
                    csbm += item.Text + "|";
                    csbmid = item.Value + "|";
                }
            }
            sql += "TZD_CSBM,";
            sql += "TZD_CSBMID,";
            sql += "TZD_SPLX,";
            sql += "TZD_ZDR,";
            sql += "TZD_ZDJY,";
            sql += "TZD_ZDSJ,";
            if (rblTZD_SPLX.SelectedValue == "1")
            {
                sql += "TZD_SPR1,";
            }
            else
            {
                sql += "TZD_SPR1,";
                sql += "TZD_SPR2,";
            }
            sql += "TZD_SPZT";
            sql += ") values (";
            sql += "'" + hidTZD_SJID.Value + "',";
            sql += "'" + txtTZD_BH.Text.Trim() + "',";
            sql += "'" + txtTZD_HTH.Text.Trim() + "',";
            sql += "'" + txtTZD_XMMC.Text.Trim() + "',";
            sql += "'" + txtTZD_GKMC.Text.Trim() + "',";
            sql += "'" + txtTZD_NR.Text.Trim() + "',";
            sql += "'" + csbm + "',";
            sql += "'" + csbmid + "',";
            sql += "'" + rblTZD_SPLX.SelectedValue + "',";
            sql += "'" + txtTZD_ZDR.Text.Trim() + "',";
            sql += "'" + txtTZD_ZDJY.Text.Trim() + "',";
            sql += "'" + lbTZD_ZDSJ.Text + "',";
            if (rblTZD_SPLX.SelectedValue == "1")
            {
                sql += "'" + txtTZD_SPR1.Text.Trim() + "',";
            }
            else
            {
                sql += "'" + txtTZD_SPR1.Text.Trim() + "',";
                sql += "'" + txtTZD_SPR2.Text.Trim() + "',";
            }
            sql += "'0')";
            return sql;
        }

        private string altersql()
        {
            string sql = "update CM_HTBGTZD set ";
            sql += "TZD_BH='" + txtTZD_BH.Text.Trim() + "',";
            sql += "TZD_HTH='" + txtTZD_HTH.Text.Trim() + "',";
            sql += "TZD_XMMC='" + txtTZD_XMMC.Text.Trim() + "',";
            sql += "TZD_GKMC='" + txtTZD_GKMC.Text.Trim() + "',";
            sql += "TZD_NR='" + txtTZD_NR.Text.Trim() + "',";
            string csbm = "";
            string csbmid = "";
            foreach (ListItem item in cbxlTZD_CSBM.Items)
            {
                if (item.Selected)
                {
                    csbm += item.Text + "|";
                    csbmid = item.Value + "|";
                }
            }
            sql += "TZD_CSBM='" + csbm.Trim('|') + "',";
            sql += "TZD_CSBMID='" + csbmid.Trim('|') + "',";
            sql += "TZD_SPLX='" + rblTZD_SPLX.SelectedValue + "',";
            sql += "TZD_ZDR='" + txtTZD_ZDR.Text.Trim() + "',";
            sql += "TZD_ZDJY='" + txtTZD_ZDJY.Text.Trim() + "',";
            sql += "TZD_ZDSJ='" + lbTZD_ZDSJ.Text + "',";
            sql += "TZD_SPR1='" + txtTZD_SPR1.Text + "',";
            if (rblTZD_SPLX.SelectedValue == "2")
            {
                sql += "TZD_SPR2='" + txtTZD_SPR2.Text.Trim() + "',";
            }
            sql += "TZD_SPZT='0'";
            sql += " where TZD_ID=" + id;
            return sql;
        }

        private string checksql()
        {
            string sql = " update CM_HTBGTZD set ";
            if (dts.Rows[0]["TZD_SPLX"].ToString() == "1")
            {
                if (username == dts.Rows[0]["TZD_SPR1"].ToString())
                {
                    sql += "TZD_SPR1_JL='" + rblTZD_SPR1_JL.SelectedValue + "',";
                    sql += "TZD_SPR1_JY='" + txtTZD_SPR1_JY.Text.Trim() + "',";
                    sql += "TZD_SPR1_SJ='" + lbTZD_SPR1_SJ.Text + "',";
                    if (rblTZD_SPR1_JL.SelectedValue == "1")
                    {
                        sql += "TZD_SPZT='10'";
                    }
                    else
                    {
                        sql += "TZD_SPZT='11'";
                    }
                }
            }
            else if (dts.Rows[0]["TZD_SPLX"].ToString() == "2")
            {
                if (username == dts.Rows[0]["TZD_SPR1"].ToString())
                {
                    sql += "TZD_SPR1_JL='" + rblTZD_SPR1_JL.SelectedValue + "',";
                    sql += "TZD_SPR1_JY='" + txtTZD_SPR1_JY.Text.Trim() + "',";
                    sql += "TZD_SPR1_SJ='" + lbTZD_SPR1_SJ.Text + "',";
                    if (rblTZD_SPR1_JL.SelectedValue == "1")
                    {
                        sql += "TZD_SPZT='1'";
                    }
                    else
                    {
                        sql += "TZD_SPZT='11'";
                    }
                }
                if (username == dts.Rows[0]["TZD_SPR2"].ToString())
                {
                    sql += "TZD_SPR2_JL='" + rblTZD_SPR2_JL.SelectedValue + "',";
                    sql += "TZD_SPR2_JY='" + txtTZD_SPR2_JY.Text.Trim() + "',";
                    sql += "TZD_SPR2_SJ='" + lbTZD_SPR2_SJ.Text + "',";
                    if (rblTZD_SPR2_JL.SelectedValue == "1")
                    {
                        sql += "TZD_SPZT='10'";
                    }
                    else
                    {
                        sql += "TZD_SPZT='11'";
                    }
                }
            }
            sql += " where TZD_ID=" + id;
            return sql;
        }

        private void SendEmail()
        {
            if (action == "add" || action == "alter" && rblTZD_SPLX.SelectedValue == "2")
            {
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("86"), new List<string>(), new List<string>(), "合同变更通知单审批", "您有合同变更通知单“" + txtTZD_BH.Text + "”需要审批，请登录系统,进入市场管理模块的“合同变更通知单”进行查看。");//给李玲发邮件
            }
            if (action=="check")
            {
                if (username == dts.Rows[0]["TZD_SPR1"].ToString() && rblTZD_SPR1_JL.SelectedValue == "1" && dts.Rows[0]["TZD_SPLX"].ToString() == "2")
                {
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("47"), new List<string>(), new List<string>(), "合同变更通知单审批", "您有合同变更通知单“" + dts.Rows[0]["TZD_BH"].ToString() + "”需要审批，请登录系统,进入市场管理模块的“合同变更通知单”进行查看。");//给李利恒发邮件
                }
                if (username == dts.Rows[0]["TZD_SPR2"].ToString()&&rblTZD_SPR2_JL.SelectedValue=="1")
                {
                    if (dts.Rows[0]["TZD_CSBMID"].ToString().Contains('|'))
                    {
                        string[] csbmid = dts.Rows[0]["TZD_CSBMID"].ToString().Split('|');
                        if (csbmid.Contains("03"))
                        {
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("146"), new List<string>(), new List<string>(), "合同变更通知单通知", "您有合同变更通知单“" + dts.Rows[0]["TZD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“合同变更通知单”进行查看。");
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("67"), new List<string>(), new List<string>(), "合同变更通知单通知", "您有合同变更通知单“" + dts.Rows[0]["TZD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“合同变更通知单”进行查看。");
                        }
                        else if (csbmid.Contains("04"))
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
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lead), new List<string>(), new List<string>(), "合同变更通知单审批", "您有合同变更通知单“" + dts.Rows[0]["TZD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“合同变更通知单”进行查看。");
                            }
                            else
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Session["UserID"].ToString()), new List<string>(), new List<string>(), "合同变更通知单审批", "生产部可能暂未设部长，请及时与采购负责人沟通！！！");
                            }
                        }
                        else if (csbmid.Contains("05"))
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
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lead), new List<string>(), new List<string>(), "合同变更通知单审批", "您有合同变更通知单“" + dts.Rows[0]["TZD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“合同变更通知单”进行查看。");
                            }
                            else
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Session["UserID"].ToString()), new List<string>(), new List<string>(), "合同变更通知单审批", "采购部可能暂未设部长，请及时与采购负责人沟通！！！");
                            }
                        }
                        else if (csbmid.Contains("06"))
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
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("256"), new List<string>(), new List<string>(), "合同变更通知单审批", "您有合同变更通知单“" + dts.Rows[0]["TZD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“合同变更通知单”进行查看。");
                            }
                            else
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Session["UserID"].ToString()), new List<string>(), new List<string>(), "合同变更通知单审批", "财务部可能暂未设部长，请及时与采购负责人沟通！！！");
                            }
                        }
                        else if (csbmid.Contains("12"))
                        {
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("69"), new List<string>(), new List<string>(), "合同变更通知单审批", "您有合同变更通知单“" + dts.Rows[0]["TZD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“合同变更通知单”进行查看。");
                        }
                    }
                    else
                    {
                        if (dts.Rows[0]["TZD_CSBMID"].ToString() == "03")
                        {
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("146"), new List<string>(), new List<string>(), "合同变更通知单通知", "您有合同变更通知单“" + dts.Rows[0]["TZD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“合同变更通知单”进行查看。");
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("67"), new List<string>(), new List<string>(), "合同变更通知单通知", "您有合同变更通知单“" + dts.Rows[0]["TZD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“合同变更通知单”进行查看。");
                        }
                        else if (dts.Rows[0]["TZD_CSBMID"].ToString() == "04")
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
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lead), new List<string>(), new List<string>(), "合同变更通知单审批", "您有合同变更通知单“" + dts.Rows[0]["TZD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“合同变更通知单”进行查看。");
                            }
                            else
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Session["UserID"].ToString()), new List<string>(), new List<string>(), "合同变更通知单审批", "生产部可能暂未设部长，请及时与采购负责人沟通！！！");
                            }
                        }
                        else if (dts.Rows[0]["TZD_CSBMID"].ToString() == "05")
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
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lead), new List<string>(), new List<string>(), "合同变更通知单审批", "您有合同变更通知单“" + dts.Rows[0]["TZD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“合同变更通知单”进行查看。");
                            }
                            else
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Session["UserID"].ToString()), new List<string>(), new List<string>(), "合同变更通知单审批", "采购部可能暂未设部长，请及时与采购负责人沟通！！！");
                            }
                        }
                        else if (dts.Rows[0]["TZD_CSBMID"].ToString() == "06")
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
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("256"), new List<string>(), new List<string>(), "合同变更通知单审批", "您有合同变更通知单“" + dts.Rows[0]["TZD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“合同变更通知单”进行查看。");
                            }
                            else
                            {
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Session["UserID"].ToString()), new List<string>(), new List<string>(), "合同变更通知单审批", "财务部可能暂未设部长，请及时与采购负责人沟通！！！");
                            }
                        }
                        else if (dts.Rows[0]["TZD_CSBMID"].ToString() == "12")
                        {
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("69"), new List<string>(), new List<string>(), "合同变更通知单审批", "您有合同变更通知单“" + dts.Rows[0]["TZD_BH"].ToString() + "”需要查看，请登录系统,进入市场管理模块的“合同变更通知单”进行查看。");
                        }
                    }
                }
            }

        }

        protected void btnDelete_onserverclick(object sender, EventArgs e)
        {
            string sql = "delete from CM_HTBGTZD where TZD_ID=" + id;
            try
            {
                DBCallCommon.ExeSqlText(sql);
            }
            catch
            {
                Response.Write("<script type='text/javascript'>alert('删除的sql语句出现错误,请与管理员联系！！！')</script>");
                return;
            }
            Response.Redirect("CM_HTBGTZDGL.aspx");
        }

        protected void btnRefuse_onserverclick(object sender, EventArgs e)
        {
            string sql = "update CM_HTBGTZD set ";
            sql += "TZD_SPR1_JL=null,";
            sql += "TZD_SPR1_JY=null,";
            sql += "TZD_SPR1_SJ=null,";
            sql += "TZD_SPR2_JL=null,";
            sql += "TZD_SPR2_JY=null,";
            sql += "TZD_SPR2_SJ=null,";
            sql += "TZD_SPZT='0'";
            sql += " where TZD_ID="+id;
            try
            {
                DBCallCommon.ExeSqlText(sql);
            }
            catch
            {
                Response.Write("<script type='text/javascript'>alert('驳回的sql语句出现错误,请与管理员联系！！！')</script>");
                return;
            }
            Response.Redirect("CM_HTBGTZDGL.aspx");
        }

        protected void btnBack_onserverclick(object sender, EventArgs e)
        {
            Response.Redirect("CM_HTBGTZDGL.aspx");
        }
    }
}
