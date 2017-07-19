using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_ServiceConn : System.Web.UI.Page
    {
        string id = string.Empty;
        string market = "";
        string leader = "";
        public string sess = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText("select ST_ID from TBDS_STAFFINFO where ST_POSITION='0701' and ST_PD='0'");
            if (dt.Rows.Count > 0)
            {
                market = dt.Rows[0][0].ToString();
            }
            else
            {
                market = "47";
            }
            dt = DBCallCommon.GetDTUsingSqlText("select ST_ID from TBDS_STAFFINFO where ST_POSITION='0102' and ST_PD='0'");
            if (dt.Rows.Count > 0)
            {
                leader = dt.Rows[0][0].ToString();
            }
            else
            {
                leader = "2";
            }
            this.Title = "顾客服务联系单";
            id = Request.QueryString["id"].ToString();
            if (!IsPostBack)
            {
                GetDepartment();
                ShowData();
                if (Session["UserID"].ToString() != market && Session["UserID"].ToString() != leader)
                {
                    btnsubmit.Visible = false;
                    //btnconfuse.Visible = false;
                    if (Session["UserDeptID"].ToString() != "07")
                    {
                        CM_CLPART.Enabled = false;
                        CM_COMMAND.ReadOnly = true;
                    }
                }
                else
                {
                    btnShenP.Visible = false;
                    FileUp.Visible = false;
                    btnAddFU.Visible = false;
                    GridView.Columns[2].Visible = false;
                    if (Session["UserID"].ToString() == bmzgID.Value)
                    {
                        bmPanel.Enabled = true;
                    }
                    if (Session["UserID"].ToString() == zgldID.Value)
                    {
                        ldPanel.Enabled = true;
                    }
                }
                if (Session["UserDeptID"].ToString() != "07")
                {
                    FileUp.Visible = false;
                    btnAddFU.Visible = false;
                    GridView.Columns[2].Visible = false;
                }
                //if (Session["UserID"].ToString() != leader)
                //{
                //    CM_CLPART.Enabled = false;
                //}
            }
            sess = Session["POSITION"].ToString();
        }

        private void ShowData()
        {
            string sql = "select * from View_CM_CusApply where CM_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                foreach (Control item in panel.Controls)
                {
                    if (item is Label && item.ID != "filesError" || item is TextBox)
                    {
                        ((ITextControl)item).Text = dr[item.ID].ToString();
                    }
                }
                List<int> s = new List<int>();
                if (dr["CM_CLPART"].ToString() != "")
                {
                    string[] strs = dr["CM_CLPART"].ToString().Split(',');
                    for (int i = 0; i < strs.Length; i++)
                    {
                        for (int j = 0; j < CM_CLPART.Items.Count; j++)
                        {
                            if (CM_CLPART.Items[j].Value == strs[i])
                            {
                                CM_CLPART.Items[j].Selected = true;
                            }
                        }
                    }
                }
                //for (int i = 0; i < s.Count; i++)
                //{
                //    CM_CLPART.Items[s[i]].Selected = true;
                //}
                string bh = dt.Rows[0]["CM_BIANHAO"].ToString();
                Match match = Regex.Match(bh, "/d{5}");
                lb_bh.Text = "GKFWLX" + match.Value;

                if (Session["UserID"].ToString() == market && dr["CM_YJ11"].ToString() != "1")//审过之后无法修改
                {
                    btnsubmit.Visible = false;
                    //btnconfuse.Visible = false;
                }

                string direct_yj = dr["CM_BUMENYJ"].ToString();
                if (direct_yj.Length > 1)
                {
                    string[] yj1 = direct_yj.Split('△');
                    bmspnr.Text = yj1[0];
                    bmsprq.Text = yj1[1];
                    for (int i = 0; i < bmsp.Items.Count; i++)
                    {
                        if (bmsp.Items[i].Value == dr["CM_YJ11"].ToString())
                        {
                            bmsp.Items[i].Selected = true;
                        }
                    }
                }
                string lead_yj = dr["CM_LEADYJ"].ToString();
                if (lead_yj.Length > 1)
                {
                    string[] yj2 = lead_yj.Split('△');
                    ldspnr.Text = yj2[0];
                    lzsprq.Text = yj2[1];
                    for (int i = 0; i < ldsp.Items.Count; i++)
                    {
                        if (ldsp.Items[i].Value == dr["CM_YJ2"].ToString())
                        {
                            ldsp.Items[i].Selected = true;
                        }
                    }
                }

                bmzg.Text = dr["CM_BUMENER"].ToString();
                bmzgID.Value = dr["CM_BUMEN"].ToString();
                zgld.Text = dr["CM_LEADER"].ToString();
                zgldID.Value = dr["CM_LEADID"].ToString();

                if (bmzgID.Value != "" && dr["CM_STATE"].ToString() != "3" && dr["CM_YJ11"].ToString() != "1")
                {
                    btnShenP.Visible = false;
                }

                HiddenFieldContent.Value = dr["CM_CONTEXT"].ToString();
                GVBind(GridView);
            }
        }

        private void GetDepartment()//绑定部门
        {
            string sqlText = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]' AND DEP_CODE not in ('01','02') ";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            CM_CLPART.DataSource = dt;
            CM_CLPART.DataTextField = "DEP_NAME";
            CM_CLPART.DataValueField = "DEP_CODE";
            CM_CLPART.DataBind();
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string part = string.Empty;
            for (int i = 0; i < CM_CLPART.Items.Count; i++)
            {
                if (CM_CLPART.Items[i].Selected)
                {
                    part += "," + CM_CLPART.Items[i].Value;//这里是选中的项
                }
            }
            if (part != "")
            {
                part = part.Substring(1);
            }
            string sql = "";
            if (Session["UserID"].ToString() == market)
            {
                string review1 = bmspnr.Text + "△" + bmsprq.Text;
                if (bmsp.SelectedValue == "2")
                {
                    sql = string.Format("update TBCM_APPLICA set CM_COMMAND='{0}',CM_BUMENYJ='{1}',CM_CLPART='{2}',CM_YJ11='2' where CM_ID='{3}'", CM_COMMAND.Text, review1, part, id);
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(leader), new List<string>(), new List<string>(), "顾客联系单申请审批", "您有顾客服务联系单需要审批，请登录系统进行查看。");
                }
                else if (bmsp.SelectedValue == "3")
                {
                    sql = string.Format("update TBCM_APPLICA set CM_COMMAND='{0}',CM_BUMENYJ='{1}',CM_YJ11='3',CM_STATE='3' where CM_ID='{2}'", CM_COMMAND.Text, review1, id);
                }
            }
            else if (Session["UserID"].ToString() == leader)
            {
                string review2 = ldspnr.Text + "△" + lzsprq.Text;
                if (ldsp.SelectedValue == "2")
                {
                    sql = string.Format("update TBCM_APPLICA set CM_COMMAND='{0}',CM_LEADYJ='{1}',CM_CLPART='{2}',CM_YJ2='2',CM_STATE='2' where CM_ID='{3}'", CM_COMMAND.Text, review2, part, id);
                }
                else if (ldsp.SelectedValue == "3")
                {
                    sql = string.Format("update TBCM_APPLICA set CM_COMMAND='{0}',CM_LEADYJ='{1}',CM_CLPART='{2}',CM_YJ2='3',CM_STATE='3' where CM_ID='{3}'", CM_COMMAND.Text, review2, part, id);
                }
            }
            if (sql != "")
            {
                DBCallCommon.ExeSqlText(sql);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审批成功！');window.opener.location.reload();window.opener=null;window.open('','_self');window.close();", true);
        }

        protected void btntijiao_Click(object sender, EventArgs e)
        {
            string part = string.Empty;
            for (int i = 0; i < CM_CLPART.Items.Count; i++)
            {
                if (CM_CLPART.Items[i].Selected)
                {
                    part += "," + CM_CLPART.Items[i].Value;//这里是选中的项
                }
            }
            if (part != "")
            {
                part = part.Substring(1);
            }
            string sql = string.Format("update TBCM_APPLICA set CM_COMMAND='{0}',CM_BUMEN='{1}',CM_LEADER='{2}',CM_CLPART='{3}',CM_YJ11='1',CM_YJ2='1',CM_STATE='1' where CM_ID='{4}'", CM_COMMAND.Text, market, leader, part, id);
            DBCallCommon.ExeSqlText(sql);
            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(market), new List<string>(), new List<string>(), "顾客联系单申请审批", "您有顾客服务联系单需要审批，请登录系统进行查看。");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交成功！');window.opener.location.reload();window.opener=null;window.open('','_self');window.close();", true);
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
        }

        protected void btnconfuse_Click(object sender, EventArgs e)
        {
            string sql = "";
            if (Session["UserID"].ToString() == market)
            {
                sql = string.Format("update TBCM_APPLICA set CM_STATUS='{0}',CM_YJ1='3' where CM_ID='{1}'", 3, id);
            }

            if (Session["UserID"].ToString() == leader)
            {
                sql = string.Format("update TBCM_APPLICA set CM_STATUS='{0}',CM_YJ2='3' where CM_ID='{1}'", 3, id);
            }
            if (sql != "")
            {
                DBCallCommon.ExeSqlText(sql);
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener.location.reload();window.opener=null;window.open('','_self');window.close();", true);
        }

        #region 下载文件，删除文件

        protected void GVBind(GridView gv)
        {
            string sql = "select * from View_CM_CusApply where CM_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                sql = "select * from tb_files where BC_CONTEXT='" + dr["CM_CONTEXT"].ToString() + "'";
                DataSet ds = DBCallCommon.FillDataSet(sql);
                gv.DataSource = ds.Tables[0];
                gv.DataBind();
                gv.DataKeyNames = new string[] { "fileID" };
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

        protected void ldsp_SelectedIndexChanged(object sender, EventArgs e)//主管领导
        {
            if (ldsp.SelectedIndex == 0)
            {
                ldspnr.Text = "同意";
            }
            else
            {
                ldspnr.Text = "拒绝理由：";
            }
            lzsprq.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
        #endregion

    }
}
