using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using ZCZJ_DPF;
using System.IO;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_task_detail : System.Web.UI.Page
    {
        string action = "";
        
        string id = "";

        float tt = 0; //总重

        //static string content = string.Empty;

        //static string content1 = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            int m = 5;

            if (Request.QueryString["id"] != null)
            {
                id = Request.QueryString["id"];
            }
            if (Request.QueryString["action"] != null)
            {
                action = Request.QueryString["action"];
            }
            if (!IsPostBack)
            {
                if (action == "add")
                {
                    HiddenFieldContent.Value = System.Guid.NewGuid().ToString();
                    //content = System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + Session["UserID"].ToString();//以当前时间为编号文档文本，合同文本的值必须唯一，要保证其唯一

                    CreateNewRow(m);

                    btnsubmit.Text = "添加";

                    //Execute_TaskID();
                    PT_DATE.Text = System.DateTime.Now.ToString("yyyy-MM-dd");//时间

                    bind_dep();

                    pl_js.Visible = false;//结算不可见

                    btnjs.Visible = false;

                    btnsubmit.Visible = true;

                    Panel_Type_In.Visible = true;//上载合同附件可见

                    Panel_view.Visible = false;//查看附件不可见

                    Panel_Type_In1.Visible = false;//结算上载附件不可见

                    Panel_view1.Visible = false;//结算查看附件不可见
                }
                else if (action == "js")
                {
                    //content1 = System.DateTime.Now.ToString("yyyyMMddHHmmss");//以当前时间为编号文档文本，合同文本的值必须唯一，要保证其唯一

                    HiddenFieldContent1.Value = System.Guid.NewGuid().ToString();
                    
                    btnsubmit.Visible = false;

                    bind_dep();

                    modification();

                    pl_js.Visible = true;//结算可见

                    btnjs.Visible = true;

                    Panel_Type_In.Visible = false;//上载合同附件不可见

                    Panel_view.Visible = true;//查看附件

                    Panel_Type_In1.Visible = true;//结算上载附件不可见

                    Panel_view1.Visible = false;//结算查看附件不可见

                }
                else if (action == "view")
                {

                    btnsubmit.Visible = false;

                    btnjs.Visible = false;

                    bind_dep();

                    modification();

                    Panel_Type_In.Visible = false;//上载合同附件不可见

                    Panel_view.Visible = true;//查看附件

                    Panel_Type_In1.Visible = false;//结算上载附件不可见

                    Panel_view1.Visible = true;//结算查看附件不可见
                }
                else
                {
                    btnsubmit.Text = "修改";
                    btnjs.Visible = false;

                    bind_dep();

                    modification();

                    Panel_Type_In.Visible = true;//上载合同附件不可见

                    Panel_view.Visible = true;//查看附件

                    Panel_Type_In1.Visible = true;//结算上载附件不可见

                    Panel_view1.Visible = true;//结算查看附件不可见
                }
            }
        }

        //修改
        public void modification()
        {
            string sql = "select * from TBMP_PRDCTTASKTOTAL where PT_CODE='"+id+"' ";

            SqlDataReader dr=DBCallCommon.GetDRUsingSqlText(sql);

            if (dr.Read())
            {
                PT_PLANT.Text = dr["PT_PLANT"].ToString();//生产单位

                PT_CODE.Text = dr["PT_CODE"].ToString();//任务单编号

                PT_DATE.Text = dr["PT_DATE"].ToString();//日期

                PT_STATE.Items.FindByValue(dr["PT_STATE"].ToString()).Selected = true;//是否需要结算

                PT_TYPE.Items.FindByValue(dr["PT_TYPE"].ToString()).Selected = true;//类别

                ddl_fzdep.Items.FindByText(dr["PT_ICKCOMNM"].ToString()).Selected = true;//负责部门

                tbproject.Text = dr["PT_PJNAME"].ToString();//项目名称

                PT_PMCHARGERNM.Text = dr["PT_PMCHARGERNM"].ToString();//业主负责人

                PT_PMEXECUTENM.Text = dr["PT_PMEXECUTENM"].ToString();//业主执行人

                PT_PDCHARGERNM.Text = dr["PT_PDCHARGERNM"].ToString();//生产负责人

                PT_PDEXECUTENM.Text = dr["PT_PDEXECUTENM"].ToString();//生产执行人

                tb_Comment.Text = dr["PT_STATEMENT"].ToString();

                lbreport.Text = dr["PT_FILE"].ToString();

                lbreport1.Text = dr["PT_FILE1"].ToString();

                /*
                 * 结算信息绑定
                 */

                lb_dep.Text = dr["PT_ICKCOMNM"].ToString();

                if (action == "view")
                {
                    if (dr["PT_STATE"].ToString() == "0")
                    {
                        //不需要结算
                        pl_js.Visible = false;//结算不可见
                    }
                    else
                    {
                        //需要结算
                        pl_js.Visible = true;//结算可见

                        txt_je.Text = dr["PT_JSJE"].ToString();//结算金额

                        if (dr["PT_ISJS"].ToString() != "")

                            ddl_isjs.Items.FindByValue(dr["PT_ISJS"].ToString()).Selected = true;//是否结算
                    }
                }

                GVBind(lbreport.Text,0);

                GVBind(lbreport1.Text, 1);

                if (action == "js")
                {
                    GVBind(lbreport1.Text);
                }
            }

            dr.Close();

            //查看相应的数据

            sql = "select PT_ENGCONTEXT,PT_QUANTITY, PT_ENGNUM,PT_DLVRYDATE,PT_DRAWINGDATE from TBMP_PRDCTTASKDETAIL where PT_CODE='" + id + "' ";

            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql);

            Task_Repeater.DataSource = dt2;

            Task_Repeater.DataBind();

        }

        private void CreateNewRow(int num)
        {
            DataTable dt = new DataTable();

            #region

            dt.Columns.Add("rownum");
            dt.Columns.Add("PT_ENGCONTEXT");
            dt.Columns.Add("PT_QUANTITY");
            dt.Columns.Add("PT_ENGNUM");
            dt.Columns.Add("PT_DLVRYDATE");
            dt.Columns.Add("PT_DRAWINGDATE");

            #endregion  

            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                
                dt.Rows.Add(newRow);
            }
            this.Task_Repeater.DataSource = dt;
            this.Task_Repeater.DataBind();
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("TM_HZY_info.aspx");
        }

        protected void btnjs_Click(object sender, EventArgs e)
        {
            string strsql = "update TBMP_PRDCTTASKTOTAL set PT_JSJE='" + txt_je.Text.Trim() + "',PT_ISJS='" + ddl_isjs.SelectedItem.Value + "',PT_FILE1='"+HiddenFieldContent1.Value+"' where PT_CODE='" + id + "'";
            DBCallCommon.ExeSqlText(strsql);
            Response.Redirect("TM_HZY_info.aspx");
        }


        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string sql = string.Empty;

            List<string> sqlList = new List<string>();

            if (action == "add")
            {
                //生产单位,任务单编号,日期，是否结算，分类，项目，业主负责人，业主经办人，生产负责人，生产经办人
                if (PT_STATE.SelectedItem.Value == "1")
                {
                    //需要结算
                    //初始状态就是没结算
                    sql = " insert into TBMP_PRDCTTASKTOTAL(PT_PLANT,PT_CODE,PT_DATE,PT_STATE,PT_TYPE,PT_PJNAME,PT_PMCHARGERNM,PT_PMEXECUTENM,PT_PDCHARGERNM,PT_PDEXECUTENM,PT_ICKCOMID,PT_ICKCOMNM,PT_FILE,PT_STATEMENT,PT_ISJS) ";
                    sql += " values('" + PT_PLANT.Text + "','" + PT_CODE.Text + "','" + PT_DATE.Text + "','" + PT_STATE.SelectedItem.Value + "','" + PT_TYPE.SelectedItem.Value + "' ";
                    sql += " ,'" + tbproject.Text + "','" + PT_PMCHARGERNM.Text + "' ";
                    sql += " ,'" + PT_PMEXECUTENM.Text + "','" + PT_PDCHARGERNM.Text + "','" + PT_PDEXECUTENM.Text + "','" + ddl_fzdep.SelectedItem.Value + "', '" + ddl_fzdep.SelectedItem.Text + "','" + HiddenFieldContent.Value + "','" + tb_Comment.Text + "','0')";
                }
                else
                {
                    //不需要结算
                    sql = " insert into TBMP_PRDCTTASKTOTAL(PT_PLANT,PT_CODE,PT_DATE,PT_STATE,PT_TYPE,PT_PJNAME,PT_PMCHARGERNM,PT_PMEXECUTENM,PT_PDCHARGERNM,PT_PDEXECUTENM,PT_ICKCOMID,PT_ICKCOMNM,PT_FILE,PT_STATEMENT,PT_ISJS) ";
                    sql += " values('" + PT_PLANT.Text + "','" + PT_CODE.Text + "','" + PT_DATE.Text + "','" + PT_STATE.SelectedItem.Value + "','" + PT_TYPE.SelectedItem.Value + "' ";
                    sql += " ,'" + tbproject.Text + "','" + PT_PMCHARGERNM.Text + "' ";
                    sql += " ,'" + PT_PMEXECUTENM.Text + "','" + PT_PDCHARGERNM.Text + "','" + PT_PDEXECUTENM.Text + "','" + ddl_fzdep.SelectedItem.Value + "', '" + ddl_fzdep.SelectedItem.Text + "','" + HiddenFieldContent.Value + "','" + tb_Comment.Text + "','2')";
                }

                sqlList.Add(sql);                

                foreach(RepeaterItem items in Task_Repeater.Items)
                {
                    string PT_ENGCONTEXT = ((TextBox)items.FindControl("PT_ENGCONTEXT")).Text;//工程内容
                    string PT_QUANTITY = ((TextBox)items.FindControl("PT_QUANTITY")).Text.Trim();//数量
                    string PT_ENGNUM = ((TextBox)items.FindControl("PT_ENGNUM")).Text.Trim();//工程量
                    string PT_DLVRYDATE = ((HtmlInputText)items.FindControl("PT_DLVRYDATE")).Value;//交货日期
                    string PT_DRAWINGDATE = ((HtmlInputText)items.FindControl("PT_DRAWINGDATE")).Value;//图纸交付时间

                    if (PT_ENGCONTEXT != "")
                    {

                        //float num = (PT_QUANTITY == string.Empty ? null:Convert.ToSingle(PT_QUANTITY));
                        //float eng = (PT_ENGNUM == string.Empty ? null : Convert.ToSingle(PT_ENGNUM));

                        //防止输出内容为0

                        if (PT_QUANTITY==string.Empty && PT_ENGNUM==string.Empty)
                        {
                            sql = " insert into TBMP_PRDCTTASKDETAIL(PT_CODE,PT_ENGCONTEXT,PT_DLVRYDATE,PT_DRAWINGDATE)  ";
                            sql += " values('" + PT_CODE.Text + "','" + PT_ENGCONTEXT + "','" + PT_DLVRYDATE + "','" + PT_DRAWINGDATE + "') ";
                        }
                        if (PT_QUANTITY == string.Empty && PT_ENGNUM != string.Empty)
                        {
                            sql = " insert into TBMP_PRDCTTASKDETAIL(PT_CODE,PT_ENGCONTEXT,PT_ENGNUM,PT_DLVRYDATE,PT_DRAWINGDATE)  ";
                            sql += " values('" + PT_CODE.Text + "','" + PT_ENGCONTEXT + "','" + PT_ENGNUM + "','" + PT_DLVRYDATE + "','" + PT_DRAWINGDATE + "') ";
                        }

                        if (PT_QUANTITY != string.Empty && PT_ENGNUM == string.Empty)
                        {
                            sql = " insert into TBMP_PRDCTTASKDETAIL(PT_CODE,PT_ENGCONTEXT,PT_QUANTITY,PT_DLVRYDATE,PT_DRAWINGDATE)  ";
                            sql += " values('" + PT_CODE.Text + "','" + PT_ENGCONTEXT + "','" + PT_QUANTITY + "','" + PT_DLVRYDATE + "','" + PT_DRAWINGDATE + "') ";
                        }

                        if (PT_QUANTITY != string.Empty && PT_ENGNUM != string.Empty)
                        {
                            sql = " insert into TBMP_PRDCTTASKDETAIL(PT_CODE,PT_ENGCONTEXT,PT_QUANTITY,PT_ENGNUM,PT_DLVRYDATE,PT_DRAWINGDATE)  ";
                            sql += " values('" + PT_CODE.Text + "','" + PT_ENGCONTEXT + "','" + PT_QUANTITY + "','" + PT_ENGNUM + "','" + PT_DLVRYDATE + "','" + PT_DRAWINGDATE + "') ";
                        }
                        sqlList.Add(sql);  
 
                    }
                }
            }
            else if (action == "update")
            {
                //生产单位,任务单编号,日期，是否结算，分类，项目，业主负责人，业主经办人，生产负责人，生产经办人
                if (PT_STATE.SelectedItem.Value == "1")
                {
                    //需要结算
                    //初始状态就是没结算

                    sql = "update TBMP_PRDCTTASKTOTAL set PT_PLANT='" + PT_PLANT.Text + "',PT_CODE='" + PT_CODE.Text + "',PT_DATE='" + PT_DATE.Text + "',PT_STATE='" + PT_STATE.SelectedItem.Value + "'," +
                        "PT_TYPE='" + PT_TYPE.SelectedItem.Value + "',PT_PJNAME='" + tbproject.Text + "',PT_PMCHARGERNM='" + PT_PMCHARGERNM.Text + "',PT_PMEXECUTENM='" + PT_PMEXECUTENM.Text + "'," +
                        "PT_PDCHARGERNM='" + PT_PDCHARGERNM.Text + "',PT_PDEXECUTENM='" + PT_PDEXECUTENM.Text + "',PT_ICKCOMID='" + ddl_fzdep.SelectedItem.Value + "',PT_ICKCOMNM='" + ddl_fzdep.SelectedItem.Text + "',PT_STATEMENT='" + tb_Comment.Text + "',PT_ISJS='0'  where PT_CODE='" + id + "'";
                }
                else
                {
                    //不需要结算
                    sql = "update TBMP_PRDCTTASKTOTAL set PT_PLANT='" + PT_PLANT.Text + "',PT_CODE='" + PT_CODE.Text + "',PT_DATE='" + PT_DATE.Text + "',PT_STATE='" + PT_STATE.SelectedItem.Value + "'," +
                         "PT_TYPE='" + PT_TYPE.SelectedItem.Value + "',PT_PJNAME='" + tbproject.Text + "',PT_PMCHARGERNM='" + PT_PMCHARGERNM.Text + "',PT_PMEXECUTENM='" + PT_PMEXECUTENM.Text + "'," +
                         "PT_PDCHARGERNM='" + PT_PDCHARGERNM.Text + "',PT_PDEXECUTENM='" + PT_PDEXECUTENM.Text + "',PT_ICKCOMID='" + ddl_fzdep.SelectedItem.Value + "',PT_ICKCOMNM='" + ddl_fzdep.SelectedItem.Text + "',PT_STATEMENT='" + tb_Comment.Text + "',PT_ISJS='2'  where PT_CODE='" + id + "'";
                }

                sqlList.Add(sql);

                foreach (RepeaterItem items in Task_Repeater.Items)
                {
                    string PT_ENGCONTEXT = ((TextBox)items.FindControl("PT_ENGCONTEXT")).Text;//工程内容
                    string PT_QUANTITY = ((TextBox)items.FindControl("PT_QUANTITY")).Text.Trim();//数量
                    string PT_ENGNUM = ((TextBox)items.FindControl("PT_ENGNUM")).Text.Trim();//工程量
                    string PT_DLVRYDATE = ((HtmlInputText)items.FindControl("PT_DLVRYDATE")).Value;//交货日期
                    string PT_DRAWINGDATE = ((HtmlInputText)items.FindControl("PT_DRAWINGDATE")).Value;//图纸交付时间

                    if (PT_ENGCONTEXT != "")
                    {

                        //防止输出内容为0

                        if (PT_QUANTITY == string.Empty && PT_ENGNUM == string.Empty)
                        {
                            sql = "update TBMP_PRDCTTASKDETAIL set PT_ENGCONTEXT='" + PT_ENGCONTEXT + "',PT_DLVRYDATE='" + PT_DLVRYDATE + "',PT_DRAWINGDATE='" + PT_DRAWINGDATE + "' where PT_CODE='" + id + "'";
                        }
                        if (PT_QUANTITY == string.Empty && PT_ENGNUM != string.Empty)
                        {
                            sql = "update TBMP_PRDCTTASKDETAIL set PT_ENGCONTEXT='" + PT_ENGCONTEXT + "',PT_ENGNUM='" + PT_ENGNUM + "',PT_DLVRYDATE='" + PT_DLVRYDATE + "',PT_DRAWINGDATE='" + PT_DRAWINGDATE + "' where PT_CODE='" + id + "'";
                        }

                        if (PT_QUANTITY != string.Empty && PT_ENGNUM == string.Empty)
                        {
                            sql = "update TBMP_PRDCTTASKDETAIL set PT_ENGCONTEXT='" + PT_ENGCONTEXT + "',PT_QUANTITY='" + PT_QUANTITY + "',PT_DLVRYDATE='" + PT_DLVRYDATE + "',PT_DRAWINGDATE='" + PT_DRAWINGDATE + "' where PT_CODE='" + id + "'";
                        }

                        if (PT_QUANTITY != string.Empty && PT_ENGNUM != string.Empty)
                        {
                            sql = "update TBMP_PRDCTTASKDETAIL set PT_ENGCONTEXT='" + PT_ENGCONTEXT + "',PT_QUANTITY='" + PT_QUANTITY + "',PT_ENGNUM='" + PT_ENGNUM + "',PT_DLVRYDATE='" + PT_DLVRYDATE + "',PT_DRAWINGDATE='" + PT_DRAWINGDATE + "' where PT_CODE='" + id + "'";
                        }
                        sqlList.Add(sql);

                    }
                }
            }
            DBCallCommon.ExecuteTrans(sqlList);  
            Response.Redirect("TM_HZY_info.aspx");
        }

        //自动生成任务单号

        public void Execute_TaskID()
        {
            string pi_id =string.Empty;
            string tag_pi_id = "T" + DateTime.Now.ToString("yyyy-MM-dd").Replace("-", "");
            string end_pi_id = "";
            string sqltext = "SELECT TOP 1 PT_CODE FROM TBMP_PRDCTTASKTOTAL WHERE PT_CODE LIKE '" + tag_pi_id + "%' ORDER BY PT_CODE DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                end_pi_id = Convert.ToString(Convert.ToInt32((dt.Rows[0]["PT_CODE"].ToString().Substring(dt.Rows[0]["PT_CODE"].ToString().Length - 4, 4))) + 1);
                end_pi_id = end_pi_id.PadLeft(4, '0');
            }
            else
            {
                end_pi_id = "0001";
            }

            pi_id = tag_pi_id + end_pi_id;

            PT_CODE.Text = pi_id;//任务单号

            

        }

        /*
         * 合计重量
         */
        protected void Task_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
           

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((TextBox)e.Item.FindControl("PT_ENGNUM")).Text.ToString() != "")
                {
                    tt += Convert.ToSingle(((TextBox)e.Item.FindControl("PT_ENGNUM")).Text.ToString());
                }
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("lbSummary")).Text = tt.ToString();
            }
        }

        /*
         * 绑定部门
         */

        private void bind_dep()
        {
            string strsql = "select DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_FATHERID='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            if (dt.Rows.Count > 0)
            {
                ddl_fzdep.DataSource = dt;
                ddl_fzdep.DataTextField = "DEP_NAME";
                ddl_fzdep.DataValueField = "DEP_CODE";
                ddl_fzdep.DataBind();
            }
        }

        #region 上传文件

        protected void bntupload_Click(object sender, EventArgs e)
        {
            //执行上传文件
            uploadFile();

            if (action == "add")

                GVBind(HiddenFieldContent.Value);

            else if (action == "js")

                GVBind(HiddenFieldContent1.Value);
        }

        private static int IntIsUF = 0;//用来绑定是否上载的状态
        /// <summary>
        /// 重点在于要给合同文本内容赋值BC_CONTEXT
        /// </summary>
        private void uploadFile()
        {
            //获取文件保存的路径 
            string FilePath = @"F:\市场管理附件\" + Convert.ToString(System.DateTime.Now.Year);

            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            HttpPostedFile UserHPF=null;

            if(action == "add")

               UserHPF = FileUploadupdate.PostedFile;

            else if (action == "js")

               UserHPF = FileUploadupdate1.PostedFile;

            try
            {
                string fileContentType = UserHPF.ContentType;// 获取客户端发送的文件的 MIME 内容类型  
                if (fileContentType == "application/msword" || fileContentType == "application/pdf" || fileContentType == "image/pjpeg" || fileContentType == "image/gif" || fileContentType == "image/png" || fileContentType == "image/bmp" || fileContentType == "application/octet-stream" || fileContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")//传送文件类型
                {
                    if (UserHPF.ContentLength > 0)
                    {
                        if (UserHPF.ContentLength < 5*(1024 * 1024))
                        {
                            string strFileTName = System.IO.Path.GetFileName(UserHPF.FileName);

                            if (!File.Exists(FilePath + "//" + strFileTName))
                            {
                                if (action == "add")
                                {
                                    //定义插入字符串，将上传文件信息保存在数据库中
                                    string sqlStr = "insert into tb_files (BC_CONTEXT,fileLoad,fileUpDate,fileName)";
                                    sqlStr += "values('" + HiddenFieldContent.Value+ "'";
                                    sqlStr += ",'" + FilePath + "'";
                                    sqlStr += ",'" + DateTime.Now.ToLongDateString() + "'";
                                    sqlStr += ",'" + strFileTName + "')";
                                    DBCallCommon.ExeSqlText(sqlStr);
                                }
                                else if (action == "js")
                                {
                                    string sqlStr = "insert into tb_files (BC_CONTEXT,fileLoad,fileUpDate,fileName)";
                                    sqlStr += "values('" + HiddenFieldContent1.Value + "'";
                                    sqlStr += ",'" + FilePath + "'";
                                    sqlStr += ",'" + DateTime.Now.ToLongDateString() + "'";
                                    sqlStr += ",'" + strFileTName + "')";
                                    DBCallCommon.ExeSqlText(sqlStr);
                                }
                                //打开与数据库的连接
                                
                                //将上传的文件存放在指定的文件夹中
                                UserHPF.SaveAs(FilePath + "//" + strFileTName);
                                IntIsUF = 1;
                            }
                            else
                            {
                                filesError.Visible = true;
                                filesError.Text = "上传文件名与服务器文件名重名，请您核对后重新命名上传！";
                                IntIsUF = 1;
                            }
                        }
                        else
                        {
                            filesError.Text = "上传文件过大，上传文件必须小于1M!";
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
            catch (Exception e)
            {
                filesError.Text = "文件上传过程中出现错误！" + e.ToString();
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

        #endregion

        private void GVBind(string content)
        {
            string sql = "select * from tb_files where BC_CONTEXT='" + content + "'";
            DataSet ds = DBCallCommon.FillDataSet(sql);
            if (action == "add")
            {
                gvfileslist.DataSource = ds.Tables[0];
                gvfileslist.DataBind();
                gvfileslist.DataKeyNames = new string[] { "fileID" };
            }
            else if (action == "js")
            {
                gvfileslist1.DataSource = ds.Tables[0];
                gvfileslist1.DataBind();
                gvfileslist1.DataKeyNames = new string[] { "fileID" };
            }
        }

        private void GVBind(string content,int state)
        {
            string sql = "select * from tb_files where BC_CONTEXT='" + content + "'";
            DataSet ds = DBCallCommon.FillDataSet(sql);
            if (ds.Tables[0].Rows.Count>0)
            {
                if (state == 0)
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                    GridView1.DataKeyNames = new string[] { "fileID" };
                }
                else
                {
                    GridView2.DataSource = ds.Tables[0];
                    GridView2.DataBind();
                    GridView2.DataKeyNames = new string[] { "fileID" };
                }
            }
        }

        #region 删除文件、下载文件

        protected void imgbtndelete_Click(object sender, ImageClickEventArgs e)
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

            if (action == "add")

                GVBind(HiddenFieldContent.Value);

            else if (action == "js")

                GVBind(HiddenFieldContent1.Value);

            //GVBind(PR_CONTENT);//删除添加的记录
        }
        protected void DeleteTFN(string sqlStr)
        {
            //打开数据库
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取指定文件的路径
            string strFilePath = ds.Tables[0].Rows[0]["fileload"].ToString() + @"\" + ds.Tables[0].Rows[0]["fileName"].ToString();
            //调用File类的Delete方法，删除指定文件
            File.Delete(strFilePath);//文件不存在也不会引发异常
        }
        protected void imgbtndownload_Click(object sender, ImageClickEventArgs e)
        {
            //获取imgbtnDelete的ImageButton对象
            ImageButton imgbtn = (ImageButton)sender;
            imgbtn.CausesValidation = false;
            //引用imgbtnDelete控件的父控件上一级控件
            GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
            GridView gv = (GridView)imgbtn.Parent.Parent.Parent.Parent;
            //获取文件真实姓名
            string sqlStr = "select fileload,fileName from tb_files where fileID='" + gv.DataKeys[gvr.RowIndex].Value.ToString() + "'";
            //打开数据库
            //Response.Write(sqlStr);         
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取文件路径
            if(ds.Tables[0].Rows.Count>0)
            {
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
        }

        #endregion
    }
}
