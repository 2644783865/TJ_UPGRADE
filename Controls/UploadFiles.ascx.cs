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
    public partial class UploadFiles : System.Web.UI.UserControl
    {
        #region Private Variables
        private int _uf_type;//文档类型（不合格品通知单）
        
        private string _uf_code;//单据编号
        private string attachPath;//附件上传的路径
        private string proj_type;
        //2017.1.10添加
        private bool _uf_upload_del;//删除和上传按钮属性
        #endregion

        #region Properties
        /// <summary>
        /// 附件合同编号及类型
        /// </summary>
        public int uf_type
        {
            get { return _uf_type; }
            set { _uf_type = value; }
        }

        public string uf_code
        {
            get
            {
                return _uf_code;
            }
            set { _uf_code = value; }
        }

        //2017.1.10添加
        public bool uf_upload_del
        {
            set
            {
                _uf_upload_del = value;
            }
        }
        
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            //2017.1.10添加
            upl_load_del();
        }

        private void InitVar()
        {
            attachPath = @"E:/质量管理附件";//附件上传位置            

            if (!Directory.Exists(attachPath))
            {
                Directory.CreateDirectory(attachPath);
            }

            if (_uf_type == 1)
            {
                proj_type = "不合格品通知单";
            }
            if (_uf_type == 2)
            {
                proj_type = "报废通知单";
            }
            bindGrid();
        }
        //2017.1.10添加
        private void upl_load_del()
        {
            btnUpload.Visible = _uf_upload_del;
            for (int i = 0; i < this.GridView1.Rows.Count; i++)
            {
                this.GridView1.Rows[i].FindControl("lblattachdel").Visible = _uf_upload_del;
            }
        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bindGrid();
            upl_load_del();
        }

        private void bindGrid()
        {
            string sqlText = "select * from TBQC_FILEUPLOAD ";
            sqlText += " where UF_CODE='" + _uf_code + "' and UF_TYPE=" + _uf_type + "";
            GridView1.DataSource = DBCallCommon.FillDataSet(sqlText);
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
            string attachName = System.IO.Path.GetFileName(hpf.FileName);
            //把文件上传到Attachments目录下
            string path = CommonFun.CreateDirName(attachPath, proj_type);//带'\'
            string fileName = DBCallCommon.Savefile(FileUpload1, path);//fileName表示新文件名
            //把附件信息插入到TBQC_FILEUPLOAD表
            if (attachName != "" || fileName != "")
            {
                string sqlText = "insert into TBQC_FILEUPLOAD";
                sqlText += "(UF_CODE,UF_NAME,UF_UPLOADTIME,UF_FILEPATH,UF_TYPE) " +
                    "Values('" + _uf_code + "','" + attachName + "',getdate(),'" + fileName + "'," + _uf_type + ")";
                DBCallCommon.ExeSqlText(sqlText);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请添加附件！！！');", true);
            }
            //把所有附件信息显示出来
            bindGrid();
            upl_load_del();
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
                string fileName = DBCallCommon.GetFieldValue("select UF_FILEPATH from TBQC_FILEUPLOAD where UF_ID=" + id);

                //2017.1.10修改
                string file_fat = "";
                if (!string.IsNullOrEmpty(fileName))
                {
                    file_fat = fileName.Substring(0, 4);
                }
                string filepath = attachPath + "\\" + file_fat + "\\" + proj_type + "\\" + fileName;
                //string filepath = CommonFun.CreateDirName(attachPath, proj_type) + fileName;

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
                DBCallCommon.ExeSqlText("delete from TBQC_FILEUPLOAD where UF_ID=" + id);
                bindGrid();
                upl_load_del();

            }
            if (e.CommandName == "attachview")
            {
                //获取当前操作的行索引
                GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = gvrow.RowIndex;

                //根据获取到得行索引，读出该条记录的ID
                string id = ((Label)(GridView1.Rows[index].FindControl("lblID"))).Text.Trim();

                //读出对应的文件名和路径
                string fileName = DBCallCommon.GetFieldValue("select UF_FILEPATH from TBQC_FILEUPLOAD where UF_ID=" + id);

                //2017.1.10修改
                string file_fat = "";
                if (!string.IsNullOrEmpty(fileName))
                {
                    file_fat = fileName.Substring(0, 4);
                }
                string filepath = attachPath + "\\" + file_fat + "\\" + proj_type + "\\" + fileName;
                //string filepath = CommonFun.CreateDirName(attachPath, proj_type) + fileName;

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
        }
    }
}