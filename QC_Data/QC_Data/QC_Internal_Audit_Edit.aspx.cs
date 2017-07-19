using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_Internal_Audit_Edit : System.Web.UI.Page
    {
        string action = string.Empty;
        string id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"].ToString();
            id = Request.QueryString["id"].ToString();
            if (!IsPostBack)
            {
                if (action == "add")
                {

                }
                else
                {
                    this.InitPage(id);

                }
            }
        }

        private void InitPage()
        {
            throw new NotImplementedException();
        }
        private void InitPage(string proid)
        {

            string sqltext = "select * from TBQC_INTERNAL_AUDIT where PRO_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                TxtName.Text = row["PRO_NAME"].ToString();
                TxtTime.Text = row["PRO_TIME"].ToString();
                //HyperLink1.Text = row["fileName"].ToString();
                txtBZ.Text = row["PRO_NOTE"].ToString();
                filename.Text = row["PRO_FUJIAN"].ToString();
            }

        }
        protected void ButCon_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            //string method1 = method.Value;


            if (action == "add")
            {
                sqltext = "select max(PRO_ID) from TBQC_INTERNAL_AUDIT";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                int id = 1;
                if (dt.Rows[0][0].ToString() != "")
                {
                    id = int.Parse(dt.Rows[0][0].ToString()) + 1;
                }
                sqltext = string.Format("insert into TBQC_INTERNAL_AUDIT(PRO_ID,PRO_NAME,PRO_TIME,PRO_FUJIAN,PRO_NOTE,PRO_TYPE) values('{0}','{1}','{2}','{3}','{4}','1')", id, TxtName.Text.Trim(), TxtTime.Text.Trim(), filename.Text.Trim(), txtBZ.Text.Trim());
            }
            else
            {
                sqltext = "update TBQC_INTERNAL_AUDIT set PRO_NAME='" + TxtName.Text.Trim() + "',PRO_TIME='" + TxtTime.Text.Trim() + "',PRO_FUJIAN='" + filename.Text.Trim() + "',PRO_NOTE='" + txtBZ.Text.Trim() + "' where PRO_ID=" + id;
            }
            try
            {
                DBCallCommon.ExeSqlText(sqltext);
                Response.Write("<script>alert('数据更新成功！！！');window.close();</script>");
            }
            catch (Exception)
            {

                Response.Write("<script>alert('程序出错请稍后再试！！！');window.close();</script>");
            }
        }
        protected void ButUpload_Click(object sender, EventArgs e)
        {
            //执行上传文件
            UpFile();
        }

        private static int IntIsUF = 0;
        /// <summary>
        /// 重点在于要给合同文本内容赋值BC_CONTEXT
        /// </summary>
        private void UpFile()
        {
            //获取文件保存的路径
            // @"F:\质量部附件\" + Convert.ToString(System.DateTime.Now.Year)
            string FilePath = @"F:\质量管理附件\" + Convert.ToString(System.DateTime.Now.Year);

            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            //    //对客户端已上载的单独文件的访问
            HttpPostedFile UserHPF = FileUpload1.PostedFile;
            try
            {
                string fileContentType = UserHPF.ContentType;// 获取客户端发送的文件的 MIME 内容类型   
                if (fileContentType == "application/vnd.ms-excel" || fileContentType == "application/msword" || fileContentType == "application/pdf" || fileContentType == "application/octet-stream" || fileContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")//传送文件类型
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
                            string sqlStr = "insert into TBQC_FILES(BC_CONTEXT,fileLoad,fileUpDate,fileName)";
                            sqlStr += "values('" + HiddenFieldContent.Value + "'";
                            sqlStr += ",'" + FilePath + "'";
                            sqlStr += ",'" + DateTime.Now.ToLongDateString() + "'";
                            sqlStr += ",'" + strFileTName + "')";

                            //打开与数据库的连接
                            DBCallCommon.ExeSqlText(sqlStr);
                            //将上传的文件存放在指定的文件夹中
                            UserHPF.SaveAs(FilePath + "//" + strFileTName);
                            IntIsUF = 1;
                            filename.Text = strFileTName;//显示文件名
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
    }
}
