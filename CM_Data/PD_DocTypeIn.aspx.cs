using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace ZCZJ_DPF.CM_Data
{
    public partial class PD_DocTypeIn : System.Web.UI.Page
    {
        Dictionary<string, string> reviewer = new Dictionary<string, string>();//用于存储审核的名单
        string action = string.Empty;
        string id = string.Empty;
        //用于显示评审信息
        Table tb = new Table();//由于其不为服务器控件，其的状态没有存储在VIEWSTATE里面，故再次请求服务器，table里面没有数据，要是想其有数据，则需要将其数据放在VIEWSTATE里面       
        Table t = new Table();
        string name = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
                action = Request.QueryString["action"].ToString();

            if (Request.QueryString["id"] != null)
                id = Request.QueryString["id"].ToString();

            /***************由于视图的存在，必须先赋值*************************/
            getLeaderInfo();
            /****************************************/
            if (!this.IsPostBack)
            {
                if (action == "add")
                {
                    HiddenFieldContent.Value = System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + Session["UserID"].ToString();//以当前时间为编号文档文本，合同文本的值必须唯一，要保证其唯一
                    //content = System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + Session["UserID"].ToString();//以当前时间为编号文档文本，合同文本的值必须唯一，要保证其唯一
                    conName.Text = Request.QueryString["conName"];
                    txtengnm.Text = Request.QueryString["txtengnm"];
                    txtyz.Text = Request.QueryString["txtyz"];
                    string sql = "select * from TBBS_CONREVIEW where PROJECT='" + conName.Text + "' and ENGINEER='" + txtengnm.Text + "' and YEZHU='" + txtyz.Text + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count == 0)
                    {
                        txtnumber.Text = "1";
                    }
                    else
                    {
                        txtnumber.Text = Convert.ToString(dt.Rows.Count + 1);
                    }
                }
                else if (action == "update")
                {
                    string sql = "select * from TBBS_CONREVIEW where BC_ID='" + id + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        ShowData(dt);
                        GVBind(AddGridViewFiles);
                    }
                }
            }
        }

        private void ShowData(DataTable dt)
        {
            int num = 0;
            conName.Text = dt.Rows[0]["PROJECT"].ToString();
            txtengnm.Text = dt.Rows[0]["ENGINEER"].ToString();
            txtyz.Text = dt.Rows[0]["YEZHU"].ToString();
            tbje.Text = dt.Rows[0]["JINE"].ToString();
            Txtmeno.Text = dt.Rows[0]["BP_NOTE"].ToString();
            HiddenFieldContent.Value = dt.Rows[0]["BC_CONTEXT"].ToString();
            string A = dt.Rows[0]["BC_REVIEWERA"].ToString();
            string B = dt.Rows[0]["BC_REVIEWERB"].ToString();
            string C = dt.Rows[0]["BC_REVIEWERC"].ToString();
            string D = dt.Rows[0]["BC_REVIEWERD"].ToString();
            string E = dt.Rows[0]["BC_REVIEWERE"].ToString();
            string G = dt.Rows[0]["BC_REVIEWERG"].ToString();
            string H = dt.Rows[0]["BC_REVIEWERH"].ToString();
            string I = dt.Rows[0]["BC_REVIEWERI"].ToString();
            string J = dt.Rows[0]["BC_REVIEWERJ"].ToString();
            num = G == "" ? num : num++;
            num = H == "" ? num : num++;
            num = I == "" ? num : num++;
            num = J == "" ? num : num++;
            Dictionary<int, string> subre = new Dictionary<int, string>();
            subre.Add(0, A);
            subre.Add(1, B);
            subre.Add(2, C);
            subre.Add(3, D);
            subre.Add(4, E);
            subre.Add(5, G);
            subre.Add(6, H);
            subre.Add(7, I);
            subre.Add(8, J);
            for (int i = 0; i < 5 + num; i++)
            {
                string shenheren = subre.Values.ElementAt(i);
                CheckBoxList ck = (CheckBoxList)Panel1.FindControl("cki" + i.ToString());
                for (int j = 0; j < ck.Items.Count; j++)
                {
                    if (ck.Items[j].Value == shenheren)
                    {
                        ck.Items[j].Selected = true;
                    }
                }
            }
        }

        #region 上传文件

        private static int IntIsUF = 0;
        /// <summary>
        /// 重点在于要给合同文本内容赋值BC_CONTEXT
        /// </summary>
        private void UpFile()
        {
            //获取文件保存的路径
            // @"F:\质量部附件\" + Convert.ToString(System.DateTime.Now.Year)
            string FilePath = @"F:\市场管理附件\" + Convert.ToString(System.DateTime.Now.Year);

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
                            string sqlStr = "insert into tb_files(BC_CONTEXT,fileLoad,fileUpDate,fileName)";
                            sqlStr += "values('" + HiddenFieldContent.Value + "'";
                            sqlStr += ",'" + FilePath + "'";
                            sqlStr += ",'" + DateTime.Now.ToLongDateString() + "'";
                            sqlStr += ",'" + strFileTName + "')";
                            //打开与数据库的连接
                            DBCallCommon.ExeSqlText(sqlStr);
                            //将上传的文件存放在指定的文件夹中
                            UserHPF.SaveAs(FilePath + "//" + strFileTName);
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
            GVBind(AddGridViewFiles);

        }
        #endregion

        #region 删除文件、下载文件

        void GVBind(GridView gv)
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
            GVBind(AddGridViewFiles);//删除添加的记录
            //GVBind(ViewGridViewFiles);//删除查看的记录
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

        #endregion

        #region 得到领导信息

        protected void getLeaderInfo()
        {
            /******绑定人员信息*****/
            getStaffInfo("03", "技术质量部", 0);
            getStaffInfo("05", "采购部", 1);
            getStaffInfo("04", "生产管理部", 2);
            getStaffInfo("06", "财务部", 3);
            getStaffInfo("07", "市场部", 4);
            getStaffInfo("01", "公司领导", 5);
            //得到领导信息，根据金额

            if (action == "add" || action == "update")
            {
                Panel1.Controls.Add(t);
            }
        }

        /**********************动态的绑定评审人员的信息*************************************/
        protected void getStaffInfo(string st_id, string DEP_NAME, int i)
        {
            string sql = string.Format("select ST_NAME,ST_ID,ST_DEPID from TBDS_STAFFINFO as a inner join TBCM_TB_SETTING as b on a.ST_ID=b.per_id where a.ST_PD='0'and b.dep_id='{0}' and per_sfjy='0'", st_id);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (st_id == "01")
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataTable ld = new DataTable();
                    ld.Columns.Add("ST_NAME");
                    ld.Columns.Add("ST_ID");
                    ld.Columns.Add("ST_DEPID");
                    ld.Rows.Add(dt.Rows[j][0].ToString(), dt.Rows[j][1].ToString(), dt.Rows[j][2].ToString());
                    bindDt(ld, st_id + j.ToString(), DEP_NAME, i);
                    i++;
                }
            }
            else
            {
                bindDt(dt, st_id, DEP_NAME, i);
            }
        }

        protected void bindDt(DataTable dt, string st_id, string DEP_NAME, int i)
        {
            if (dt.Rows.Count != 0)
            {
                TableRow tr = new TableRow();
                TableCell td1 = new TableCell();//第一列为部门名称
                td1.Width = 85;
                Label lab = new Label();
                lab.Text = DEP_NAME + ":";
                Label lab1 = new Label();
                lab1.ID = "dep" + i.ToString();
                lab1.Text = st_id;
                lab1.Visible = false;
                td1.Controls.Add(lab);
                td1.Controls.Add(lab1);
                tr.Cells.Add(td1);

                CheckBoxList cki = new CheckBoxList();//第二列为领导的姓名
                cki.ID = "cki" + i.ToString();
                cki.DataSource = dt;
                cki.DataTextField = "ST_NAME";//领导的姓名
                cki.DataValueField = "ST_ID";//部门的编号
                cki.DataBind();
                for (int k = 0; k < cki.Items.Count; k++)
                {
                    cki.Items[k].Attributes.Add("Onclick", "CheckBoxList_Click(this)");//使用了javascript使其只能勾选一个
                }
                cki.RepeatColumns = 5;//获取显示的列数
                TableCell td2 = new TableCell();
                td2.Controls.Add(cki);
                tr.Cells.Add(td2);
                t.Controls.Add(tr);
            }
        }
        #endregion

        protected void submint_Click(object sender, EventArgs e)
        {
            bindReviewer();//将数据读出
            if (reviewer.Count <= 10)
            {
                string pronm = conName.Text.Trim();//项目名称
                string engnm = txtengnm.Text.Trim();//工程名称
                string yz = txtyz.Text.Trim();//业主信息
                string je = tbje.Text.Trim();//金额
                string number = txtnumber.Text.Trim();//次数
                string BC_CONTEXT = HiddenFieldContent.Value;//读入数据
                string BC_REVIEWERA = string.Empty;// 技术部 将其设置为1
                string BC_REVIEWERB = string.Empty;//采购部2
                string BC_REVIEWERC = string.Empty;//生产部3
                string BC_REVIEWERD = string.Empty;//财务部4
                string BC_REVIEWERE = string.Empty;//市场部5
                string BC_REVIEWERF = string.Empty;//领导1-6  预留字段
                string BC_REVIEWERG = string.Empty;//领导2-7
                string BC_REVIEWERH = string.Empty;//领导3-8
                string BC_REVIEWERI = string.Empty;//领导4-9
                string BC_REVIEWERJ = string.Empty;//领导5-10
                string BC_DRAFTER = string.Empty;
                BC_DRAFTER = Session["UserID"].ToString();
                string BP_ACPDATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//起草时间
                string BP_STATUS = "0";//目前状态为0-----（处理中）
                string BP_NOTE = Txtmeno.Text;
                //评审状态，根据人数来添加字符串数
                //需要特别注意
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < reviewer.Count; i++)
                {
                    sb.Append(0);
                }
                string BP_SPSTATUS = sb.ToString();
                /******************************用于存储领导的信息**************************************************/
                List<string> subreviewer = new List<string>();
                /**************************************************************/
                #region 读取评审人员名单

                for (int i = 0; i < reviewer.Count; i++)
                {
                    /******************通过键来找值******************************************************/

                    if (reviewer.Keys.ElementAt(i).ToString() == "03")
                    {
                        BC_REVIEWERA = reviewer.Values.ElementAt(i);
                    }

                    if (reviewer.Keys.ElementAt(i).ToString() == "05")
                    {

                        BC_REVIEWERB = reviewer.Values.ElementAt(i);
                    }
                    if (reviewer.Keys.ElementAt(i).ToString() == "04")
                    {
                        BC_REVIEWERC = reviewer.Values.ElementAt(i);
                    }
                    if (reviewer.Keys.ElementAt(i).ToString() == "06")
                    {
                        BC_REVIEWERD = reviewer.Values.ElementAt(i);
                    }
                    if (reviewer.Keys.ElementAt(i).ToString() == "07")
                    {
                        BC_REVIEWERE = reviewer.Values.ElementAt(i);
                    }
                    //if (reviewer.Keys.ElementAt(i).ToString() == "01")
                    //{
                    //    BC_REVIEWERG = reviewer.Values.ElementAt(i);
                    //}
                    if (reviewer.Keys.ElementAt(i).Substring(0, 2).ToString() == "01")
                    {
                        /******************************添加领导的信息,存储领导的编号**************************************************/
                        subreviewer.Add(reviewer.Values.ElementAt(i));
                    }
                }
                /*********读取领导的编号*******/
                if (subreviewer.Count > 0)
                {
                    BC_REVIEWERG = subreviewer.ElementAt(0);
                }
                if (subreviewer.Count > 1)
                {
                    BC_REVIEWERH = subreviewer.ElementAt(1);
                }
                if (subreviewer.Count > 2)
                {
                    BC_REVIEWERI = subreviewer.ElementAt(2);
                }
                if (subreviewer.Count > 3)
                {
                    BC_REVIEWERJ = subreviewer.ElementAt(3);
                }
                #endregion
                if (action == "add")
                {
                    string insertsql = "insert into TBBS_CONREVIEW (PROJECT,ENGINEER,YEZHU,JINE,BC_NUMBER,BC_CONTEXT,BC_REVIEWERA,BC_REVIEWERB,BC_REVIEWERC,BC_REVIEWERD,BC_REVIEWERE,BC_REVIEWERF,BC_REVIEWERG,BC_REVIEWERH,BC_REVIEWERI,BC_REVIEWERJ,BC_DRAFTER,BP_ACPDATE,BP_STATUS,BP_NOTE,BP_SPSTATUS) ";
                    insertsql += "values ('" + pronm + "','" + engnm + "','" + yz + "','" + je + "','" + number + "','" + BC_CONTEXT + "','" + BC_REVIEWERA + "','" + BC_REVIEWERB + "','" + BC_REVIEWERC + "','" + BC_REVIEWERD + "','" + BC_REVIEWERE + "','" + BC_REVIEWERF + "','" + BC_REVIEWERG + "','" + BC_REVIEWERH + "','" + BC_REVIEWERI + "','" + BC_REVIEWERJ + "','" + BC_DRAFTER + "','" + BP_ACPDATE + "','" + BP_STATUS + "','" + BP_NOTE + "','" + BP_SPSTATUS + "') ";
                    DBCallCommon.ExeSqlText(insertsql);
                }
                else if (action == "update")
                {
                    //修改之后将前面评审的内容更新为空
                    string updatesql = "update TBBS_CONREVIEW set PROJECT='" + pronm + "',ENGINEER='" + engnm + "',YEZHU='" + yz + "',JINE='" + je + "',BC_REVIEWERA='" + BC_REVIEWERA + "',BC_REVIEWERB='" + BC_REVIEWERB + "',BC_REVIEWERC='" + BC_REVIEWERC + "',BC_REVIEWERD='" + BC_REVIEWERD + "',BC_REVIEWERE='" + BC_REVIEWERE + "',BC_REVIEWERF='" + BC_REVIEWERF + "',BP_NOTE='" + BP_NOTE + "',  " +
                    " BP_RVIEWA='',BP_RVIEWB='',BP_RVIEWC='',BP_RVIEWD='',BP_RVIEWE='',BP_RVIEWF='',BP_RVIEWG='',BP_RVIEWH='',BP_RVIEWI='',BP_RVIEWJ='',BP_SPSTATUS='" + BP_SPSTATUS + "',BP_STATUS='0',BP_YESORNO='Y'  where BC_ID='" + id + "'";
                    DBCallCommon.ExeSqlText(updatesql);
                }
                Response.Redirect("PD_DocManage.aspx");
            }
            else
            {
                errorlb.Visible = true;
                errorlb.Text = "评审人数过多，评审人数最多不超过10人！";
            }
        }

        /****************************对评审人进行勾选登记*************************************/
        private void bindReviewer()
        {
            int count = 0;
            string sql = string.Format("select ST_NAME,ST_ID,ST_DEPID from TBDS_STAFFINFO as a inner join TBCM_TB_SETTING as b on a.ST_ID=b.per_id where a.ST_PD='0'and b.dep_id='{0}' and per_sfjy='0'", "01");
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            int num = dt.Rows.Count;
            for (int i = 0; i < 5 + num; i++)
            {
                CheckBoxList ck = (CheckBoxList)Panel1.FindControl("cki" + i.ToString());
                Label lb = (Label)Panel1.FindControl("dep" + i.ToString());
                if (ck != null)
                {
                    for (int j = 0; j < ck.Items.Count; j++)
                    {
                        if (ck.Items[j].Selected == true)
                        {
                            reviewer.Add(lb.Text, ck.Items[j].Value.ToString());//字典，绑定部门领导的编号
                            count++;
                        }
                    }
                }
            }
        }

        protected void Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("PD_DocManage.aspx");
        }
    }
}
