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
using System.Data.SqlClient;
using ZCZJ_DPF;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace ZCZJ_DPF.Controls
{
    public partial class UploadAttachments : System.Web.UI.UserControl
    {
        #region Private Variables
        private int _at_type;//附件类型:合同文档、索赔文档
        private int _at_sp;
        private string _at_htbh;//合同编号
        private string attachPath;//附件上传的路径
        private string proj_type;
        #endregion

        #region Properties
        /// <summary>
        /// 附件合同编号及类型
        /// </summary>
        public int at_type
        {
            get { return _at_type; }
            set { _at_type = value; }
        }

        public string at_htbh
        {
            get 
            {
                return _at_htbh;
            }
            set { _at_htbh = value; }
        }
        public int at_sp
        {
            get { return _at_sp; }
            set
            {
                _at_sp = value;
            }
        }
        #endregion


        //DbAccess dbl = new DbAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
        }

        private void InitVar()
        {
            attachPath = @"E:/合同管理附件";//附件上传位置            

            if (!Directory.Exists(attachPath))
            {
                Directory.CreateDirectory(attachPath);
            } 
            
            if (_at_type == 1)
            {
                proj_type = "索赔文档";
            }
            else if (_at_type == 0)
            {
                proj_type = "合同文档";
            }
            else if (_at_type == 2)
            {
                proj_type = "评审合同文档";
            }
            bindGrid();
        }
        

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bindGrid();
        }

        private void bindGrid()
        {
            string sqlText = "select * from TBPM_ATTACHMENTS ";
            sqlText += " where AT_HTBH='" +_at_htbh+ "' and AT_TYPE="+_at_type+" and AT_SPLB="+_at_sp+"";
            GridView1.DataSource =DBCallCommon.FillDataSet(sqlText);
            GridView1.DataBind();
            Lbl_remind.Visible = false;
        }

        //外部调用
        public void InitData()
        {
            bindGrid();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

            //这个attachName表示原文件名。
            HttpPostedFile hpf = FileUpload1.PostedFile;
            //string attachName = FileUpload1.PostedFile.FileName.Substring(FileUpload1.PostedFile.FileName.LastIndexOf("\\")).Substring(1);
            string attachName = System.IO.Path.GetFileName(hpf.FileName);
            //把文件上传到Attachments目录下
            string path = CommonFun.CreateDirName(attachPath, proj_type);//带'\'
            string fileName = DBCallCommon.Savefile(FileUpload1, path);//fileName表示新文件名
            //把附件信息插入到attachment表
            if (attachName != "" || fileName != "")
            {
                string sqlText = "insert into TBPM_ATTACHMENTS";
                sqlText += "(AT_HTBH,AT_NAME,AT_DESCRIBE,AT_CREATDATE,AT_FILEPATH,AT_TYPE,AT_SPLB) " +
                    "Values('" + _at_htbh + "','" + attachName + "','" + txtDesc.Text.Trim() + "',getdate(),'" + fileName + "'," + _at_type + "," + _at_sp + ")";
                DBCallCommon.ExeSqlText(sqlText);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请添加附件！！！');", true);
            }
            //把所有附件信息显示出来
            bindGrid();
            txtDesc.Text = "";
            //txtBeizhu.Text = "";
            //Response.Redirect(Request.Url.ToString());//防止二次刷新
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "attachdel")
            {
                //获取当前操作的行索引
                GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = gvrow.RowIndex;

                //根据获取到得行索引，读出该条记录的ID
                string id = ((Label)(GridView1.Rows[index].FindControl("lblID"))).Text.Trim();

                //删除对应的文件
                string fileName = DBCallCommon.GetFieldValue("select AT_FILEPATH from TBPM_ATTACHMENTS where AT_ID=" + id);
                string filepath = CommonFun.CreateDirName(attachPath, proj_type) + fileName;

                //判断文件是否存在，如果不存在提示重新上传
                if (System.IO.File.Exists(filepath))
                {
                    DBCallCommon.DeleteFile(filepath);                   
                    //重新读出附件信息
                    
                }
                else
                {
                    Lbl_remind.Visible = true;
                }
                //根据记录的ID从数据库中删除该记录
                DBCallCommon.ExeSqlText("delete from TBPM_ATTACHMENTS where AT_ID=" + id);
                bindGrid();

            }
            if (e.CommandName == "attachview")
            {
                //获取当前操作的行索引
                GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = gvrow.RowIndex;

                //根据获取到得行索引，读出该条记录的ID
                string id = ((Label)(GridView1.Rows[index].FindControl("lblID"))).Text.Trim();

                //读出对应的文件名和路径
                string fileName = DBCallCommon.GetFieldValue("select AT_FILEPATH from TBPM_ATTACHMENTS where AT_ID=" + id);
                string filepath = CommonFun.CreateDirName(attachPath, proj_type) + fileName;

                //判断文件是否存在，如果不存在提示重新上传
                if (System.IO.File.Exists(filepath))
                {

                   DBCallCommon.FileDown_byte(fileName, filepath);
                }
                else
                {
                    Lbl_remind.Visible = true;
                }
            }
            //Response.Redirect(Request.Url.ToString());
        }

    }
}