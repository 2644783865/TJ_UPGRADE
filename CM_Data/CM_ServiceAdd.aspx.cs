using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.IO;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_ServiceAdd : System.Web.UI.Page
    {
        string action = string.Empty;
        string manclerk = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];
            if (!IsPostBack)
            {
                //ReadOnly(bmPanel);
                //ReadOnly(ldPanel);
                GetDepartment();
                ST_NAME.Text = Session["UserName"].ToString();
                UserID.Value = Session["UserID"].ToString();
                if (action == "Add")
                {
                    HiddenFieldContent.Value = DateTime.Now.ToString("yyyyMMddHHmmssff");
                    //bmSelect.Visible = true;
                    GridView.Columns[3].Visible = false;
                }
                else
                {
                    ShowData();
                    if (action == "Edit")
                    {
                        GridView.Columns[3].Visible = false;
                        if (manclerk == Session["UserID"].ToString() || Session["UserDeptID"] == "07")  //为制单人或市场部
                        {
                            //bmSelect.Visible = true;
                        }
                        else //非法登录的
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
                        }
                    }
                    else if (action == "Look")
                    {
                        btnsubmit.Visible = false;
                        ReadOnly(panel);
                        FileUp.Visible = false;
                        btnAddFU.Visible = false;
                        GridView.Columns[2].Visible = false;
                    }
                    else if (action == "ShenP")
                    {
                        FileUp.Visible = false;
                        btnAddFU.Visible = false;
                        GridView.Columns[2].Visible = false;
                        bmPanel.Enabled = true;
                        if (bmzgID.Value == Session["UserID"].ToString())
                        {
                            bmspnr.ReadOnly = false;
                            bmsp.Enabled = true;
                        }
                        else
                        {
                            btnsubmit.Visible = false;
                            ReadOnly(panel);
                        }
                    }
                }
            }
        }

        private void ShowData()
        {
            string sql = "select * from View_CM_CusApply where CM_ID='" + Request.QueryString["id"] + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                foreach (Control control in panel.Controls)
                {
                    if (control is TextBox)
                    {
                        ((TextBox)control).Text = dr[control.ID].ToString();
                    }
                    else if (control is Label && control.ID != "filesError")
                    {
                        ((Label)control).Text = dr[control.ID].ToString();
                    }
                }
                manclerk = dr["CM_MANCLERK"].ToString();
                bmzg.Text = dr["CM_DIRECTOR"].ToString();
                bmzgID.Value = dr["CM_DIRECTID"].ToString();
                //zgld.Text = dr["CM_LEADER"].ToString();
                //zgldID.Value = dr["CM_LEADID"].ToString();
                List<int> s = new List<int>();
                if (dr["CM_PART"].ToString() != "")
                {
                    string[] strs = dr["CM_PART"].ToString().Split(',');
                    for (int i = 0; i < strs.Length; i++)
                    {
                        CM_PART.SelectedValue = strs[i];
                        s.Add(CM_PART.SelectedIndex);
                    }
                }
                for (int i = 0; i < s.Count; i++)
                {
                    CM_PART.Items[i].Selected = true;
                }
                HiddenFieldContent.Value = dr["CM_CONTEXT"].ToString();
                GVBind(GridView);
                string direct_yj = dr["CM_DIRECTYJ"].ToString();
                if (direct_yj.Length > 1)
                {
                    string[] yj1 = direct_yj.Split('△');
                    bmspnr.Text = yj1[0];
                    bmsprq.Text = yj1[1];
                    for (int i = 0; i < bmsp.Items.Count; i++)
                    {
                        if (bmsp.Items[i].Value == dr["CM_YJ1"].ToString())
                        {
                            bmsp.Items[i].Selected = true;
                        }
                    }
                }
                //string lead_yj = dr["CM_LEADYJ"].ToString();
                //if (lead_yj.Length > 1)
                //{
                //    string[] yj2 = lead_yj.Split('△');
                //    ldspnr.Text = yj2[0];
                //    lzsprq.Text = yj2[1];
                //    for (int i = 0; i < ldsp.Items.Count; i++)
                //    {
                //        if (ldsp.Items[i].Value == dr["CM_YJ2"].ToString())
                //        {
                //            ldsp.Items[i].Selected = true;
                //        }
                //    }
                //}
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请刷新原页面！');window.opener=null;window.open('','_self');window.close();", true);
            }
        }

        private void GetDepartment()//绑定部门
        {
            string sqlText = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE='03' AND DEP_CODE not in ('01','02') ";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            CM_PART.DataSource = dt;
            CM_PART.DataTextField = "DEP_NAME";
            CM_PART.DataValueField = "DEP_CODE";
            CM_PART.DataBind();
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            string part = string.Empty;
            DataTable dt = DBCallCommon.GetDTUsingSqlText("select ST_ID from TBDS_STAFFINFO where ST_POSITION='0701' and ST_PD='0'");
            string market = "";
            if (dt.Rows.Count > 0)
            {
                market = dt.Rows[0][0].ToString();
            }
            else
            {
                market = "47";
            }

            for (int i = 0; i < CM_PART.Items.Count; i++)
            {
                if (CM_PART.Items[i].Selected)
                {
                    part = CM_PART.Items[i].Value;//这里是选中的项
                }
            }
            if (action == "Add")
            {
                StringBuilder col = new StringBuilder();
                StringBuilder val = new StringBuilder();
                foreach (Control control in panel.Controls)
                {
                    if (control is TextBox)
                    {
                        if (((TextBox)control).Text.Trim() != "")
                        {
                            col.Append(control.ID + ",");
                            val.Append("'" + ((TextBox)control).Text + "',");
                        }
                    }
                }


                col.Append("CM_DIRECTOR,");
                val.Append("'" + market + "',");

                if (part != "")
                {
                    col.Append("CM_PART,");
                    val.Append("'" + part + "',");
                }
                else
                {
                    col.Append("CM_COMMAND,");
                    val.Append("'" + CM_ASKFOR.Text + "',");
                }
                col.Append("CM_MANCLERK,CM_CONTEXT");
                val.Append("'" + UserID.Value + "','" + HiddenFieldContent.Value + "'");
                string sql = string.Format("insert into TBCM_APPLICA({0},CM_ZDTIME) values({1},'{2}')", col, val, DateTime.Now.ToString("yyyy-MM-dd"));
                DBCallCommon.ExeSqlText(sql);
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(market), new List<string>(), new List<string>(), "顾客服务申请审批", "您有顾客服务申请需要审批，请登录系统进行查看。");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('添加成功！');window.opener=null;window.open('','_self');window.close();", true);
                Response.Redirect("CM_ServiceList.aspx");
            }
            else if (action == "Edit")
            {
                StringBuilder update = new StringBuilder();
                foreach (Control control in panel.Controls)
                {
                    if (control is TextBox)
                    {
                        if (((TextBox)control).Text.Trim() != "")
                        {
                            update.Append("," + control.ID + "='" + ((TextBox)control).Text + "'");
                        }
                    }
                }
                update.Append(",CM_DIRECTOR='" + market + "',CM_PART='" + part + "'");
                //update.Append("CM_DIRECTOR='" + bmzgID.Value + "',CM_PART='" + part + "',").Append("CM_DIRECTYJ='',CM_LEADER='',CM_LEADYJ='',CM_STATUS='1'");
                string sql = string.Format("update TBCM_APPLICA set {0},CM_STATUS='1',CM_LEADER='',CM_URESULT='',CM_RESULT='',CM_CLPART='',CM_COMMAND='',CM_CHULI='N',CM_YJ1='1',CM_YJ2='1',CM_CONTEXT='{1}' where CM_ID='{2}'", update.ToString().Substring(1), HiddenFieldContent.Value, id);
                DBCallCommon.ExeSqlText(sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
            }
            #region MyRegion
            else if (action == "ShenP")
            {
                string sqlTxt = string.Empty;
                List<string> sqlstr = new List<string>();
                if (bmzgID.Value == UserID.Value)
                {
                    string review1 = bmspnr.Text + "△" + bmsprq.Text;
                    if (bmsp.SelectedValue == "2")
                    {
                        sqlTxt = string.Format("update TBCM_APPLICA set CM_DIRECTYJ='{0}',CM_YJ1='{1}', CM_STATUS='2' where CM_ID='{2}'", review1, bmsp.SelectedValue, id);
                        sqlstr.Add(sqlTxt);
                    }
                    else if (bmsp.SelectedValue == "3")
                    {
                        sqlTxt = string.Format("update TBCM_APPLICA set CM_DIRECTYJ='{0}',CM_YJ1='{1}',CM_STATUS='3' where CM_ID='{2}'", review1, bmsp.SelectedValue, id);
                        sqlstr.Add(sqlTxt);
                    }
                }
                //if (zgldID.Value == UserID.Value)
                //{
                //    string review2 = ldspnr.Text + "△" + lzsprq.Text;
                //    if (ldsp.SelectedValue == "2")
                //    {
                //        sqlTxt = string.Format("update TBCM_APPLICA set CM_LEADYJ='{0}', CM_STATUS='2',CM_YJ2='{1}' where CM_ID='{2}'", review2, ldsp.SelectedValue, id);
                //        sqlstr.Add(sqlTxt);
                //    }
                //    else if (ldsp.SelectedValue == "3")
                //    {
                //        sqlTxt = string.Format("update TBCM_APPLICA set CM_LEADYJ='{0}',CM_YJ2='{1}', CM_STATUS='3' where CM_ID='{2}'", review2, ldsp.SelectedValue, id);
                //        sqlstr.Add(sqlTxt);
                //    }
                //}
                if (sqlTxt != "")
                {
                    DBCallCommon.ExecuteTrans(sqlstr);
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('评审完成！');window.opener=null;window.open('','_self');window.close();", true);
                    if (bmzgID.Value == UserID.Value && bmsp.SelectedValue == "2")
                    {
                        //DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(zgldID.Value), new List<string>(), new List<string>(), "顾客服务申请审批", "您有顾客服务申请需要审批，请登录系统进行查看。");
                    }
                    Response.Write("<script>alert('评审完成！');window.opener.location.reload();window.opener=null;window.open('','_self');window.close();</script>");//刷新
                }
            }
            #endregion
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            if (action != "Add")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
            }
            else
            {
                Response.Redirect("CM_ServiceList.aspx");
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
                else if (control is RadioButtonList)
                {
                    ((RadioButtonList)control).Enabled = false;
                }
                else if (control is DropDownList)
                {
                    ((DropDownList)control).Enabled = false;
                }
            }
        }

        #region 审批意见
        protected void bmsp_SelectedIndexChanged(object sender, EventArgs e)//部门主管
        {
            if (bmsp.SelectedIndex == 0)
            {
                bmspnr.Text = "同意";
            }
            else
            {
                bmspnr.Text = "拒绝理由：";
            }
            bmsprq.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        //protected void ldsp_SelectedIndexChanged(object sender, EventArgs e)//主管领导
        //{
        //    if (ldsp.SelectedIndex == 0)
        //    {
        //        ldspnr.Text = "同意";
        //    }
        //    else
        //    {
        //        ldspnr.Text = "拒绝理由：";
        //    }
        //    lzsprq.Text = DateTime.Now.ToString("yyyy-MM-dd");
        //}
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
            string FilePath = @"E:\市场顾客服务附件\" + Convert.ToString(System.DateTime.Now.Year);

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

        #region 删除文件、下载文件

        protected void GVBind(GridView gv)
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
            GVBind(GridView);//删除添加的记录
            //GVBind(ViewGridViewFiles);//删除查看的记录
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
    }
}
