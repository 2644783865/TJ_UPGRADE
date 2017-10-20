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
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;


namespace ZCZJ_DPF.OM_Data
{
    public partial class Sta_StaffEdit : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection();
        string actionstr = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            actionstr = Request.QueryString["action"].ToString();
            ST_FILLDATE.Text = DateTime.Now.ToString();
            if (!IsPostBack)
            {
                DropBind();
                //Salary.Visible = false;
                Hidden.Value = DateTime.Now.ToString("yyyyMMddHHmmssff");
                AddRole();
                ST_MANCLERK.Text = Session["UserName"].ToString();
                if (actionstr == "add")
                {
                    this.Title = "添加新员工";
                    confirm.Text = "添 加";
                    DEP_POSITION.Items.Clear();
                    AddNew(DEP_POSITION);
                }
                else if (actionstr == "edit")
                {
                    Showdata();
                }
                else if (actionstr == "view")
                {
                    Showdata();
                    confirm.Visible = false;
                    wenjian.Visible = false;
                }
            }
        }

        private void DropListBind(List<DropDownList> listDrop)//绑定部门职位
        {
            foreach (DropDownList ddl in listDrop)
            {
                string sqlText="";  //数据库执行语句
                if (((DropDownList)ddl).ID == "DEP_NAME")
                {
                    sqlText="select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                    ((DropDownList)ddl).DataSource = dt;
                    ((DropDownList)ddl).DataTextField = ddl.ID.ToString();
                    ((DropDownList)ddl).DataValueField = "DEP_CODE";
                    ((DropDownList)ddl).DataBind();
                }
                else
                {
                    sqlText = string.Format("select min(ST_ID) as ST_ID,{0},min(ST_DEPID) as ST_DEPID from TBDS_STAFFINFO as a left join TBDS_DEPINFO as b on a.ST_DEPID=b.DEP_CODE where {0} is not null and {0}<>'' group by {0} order by ST_ID ", ddl.ID.ToString());
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                    ((DropDownList)ddl).DataSource = dt;
                    ((DropDownList)ddl).DataTextField = ddl.ID.ToString();
                    ((DropDownList)ddl).DataBind();
                }
                AddNew(((DropDownList)ddl));
            }
        }

        private void DropBind() //绑定下拉框信息
        {
            List<DropDownList> ddl = new List<DropDownList>() { ST_GENDER, DEP_NAME, ST_SEQUEN };
            DropListBind(ddl);
            //绑定名族
            List<string> list = new List<string>() { "汉族", "蒙古族", "回族", "藏族", "维吾尔族", "苗族", "彝族", "壮族", "布依族", "朝鲜族", "满族", "侗族", "瑶族", "白族", "土家族", "哈尼族", "哈萨克族", "傣族", "黎族", "僳僳族", "佤族", "畲族", "高山族", "拉祜族", "水族", "东乡族", "纳西族", "景颇族", "柯尔克孜族", "土族", "达斡尔族", "仫佬族", "羌族", "布朗族", "撒拉族", "毛南族", "仡佬族", "锡伯族", "阿昌族", "普米族", "塔吉克族", "怒族", "乌孜别克族", "俄罗斯族", "鄂温克族", "德昂族", "保安族", "裕固族", "京族", "塔塔尔族", "独龙族", "鄂伦春族", "赫哲族", "门巴族", "珞巴族", "基诺族" };
            ST_PEOPLE.DataSource = list;
            ST_PEOPLE.DataBind();
            AddNew(ST_PEOPLE);
            //绑定政治面貌
            list = new List<string>() { "群众", "中共党员", "共青团员", "民主党派", "无党派", "农工民主党", "致公党", "九三学社", "预备党员" };
            ST_POLITICAL.DataSource = list;
            ST_POLITICAL.DataBind();
            AddNew(ST_POLITICAL);
            //婚姻状况
            list = new List<string>() { "未婚", "再婚", "丧偶", "离异", "已婚" };
            ST_MARRY.DataSource = list;
            ST_MARRY.DataBind();
            AddNew(ST_MARRY);
            ////第一学历  最高学历
            list = new List<string>() { "研究生", "在职研究生", "本科", "大普", "大专", "中专", "高中", "初中", "小学" };
            //ST_XUELI.DataSource = list;
            //ST_XUELI.DataBind();
            //AddNew(ST_XUELI);
            ST_XUELIHI.DataSource = list;
            ST_XUELIHI.DataBind();
            AddNew(ST_XUELIHI);
            //第一学位  最高学位
            list = new List<string>() { "博士后", "博士", "硕士", "双学士", "学士", "无" };
            ST_XUEWEI.DataSource = list;
            ST_XUEWEI.DataBind();
            AddNew(ST_XUEWEI);
            ST_XUEWEIHI.DataSource = list;
            ST_XUEWEIHI.DataBind();
            AddNew(ST_XUEWEIHI);
            //技术职务（称）
            list = new List<string>() { "教授级高级工程师", "高级工程师", "工程师", "助理工程师", "技术员", "高级经济师", "经济师", "助理经济师", "高级政工师", "政工师", "助理政工师", "高级会计师", "会计师", "助理会计师", "会计员", "高级国务商务师", "国际商务师", "助理国际商务师", "研究馆员", "副研究馆员", "馆员", "助理馆员", "主任护师", "主管护师", "护师", "统计师", "助理统计师" };
            ST_ZHICH.DataSource = list;
            ST_ZHICH.DataBind();
            AddNew(ST_ZHICH);
            //职能等级
            list = new List<string>() { "高级技师", "技师", "高级工", "中级工", "普通工", "初级工" };
            ST_ZHICHXU.DataSource = list;
            ST_ZHICHXU.DataBind();
            AddNew(ST_ZHICHXU);

            //学历类型
            list = new List<string>() { "普通统招", "成教", "自考", "专升本", "电大", "网络教育" };
            ST_XUELITYPE.DataSource = list;
            ST_XUELITYPE.DataBind();
            AddNew(ST_XUELITYPE);
            //最高学历类型
            list = new List<string>() { "普通统招", "成教", "自考", "专升本", "电大", "网络教育" };
            ST_XUELITYPEHI.DataSource = list;
            ST_XUELITYPEHI.DataBind();
            AddNew(ST_XUELITYPEHI);
        }

        private void AddNew(DropDownList ddl) //下拉框添加-请选择-
        {
            ListItem item = new ListItem();
            item.Text = "-请选择-";
            item.Value = "00";
            ddl.Items.Insert(0, item);
            ddl.SelectedValue = "00";
        }

        private void AddRole()
        {
            string sql = "select * from ROLE_INFO";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            chk_Role.DataSource = dt;
            chk_Role.DataTextField = "R_NAME";
            chk_Role.DataBind();
        }

        private void Showdata() //将数据绑定到textbox
        {
            string st_id = Request.QueryString["ST_ID"].ToString();//得到修改人员编码
            string sqlText = "select distinct a.*,b.DEP_NAME,d.DEP_NAME as DEP_POSITION from TBDS_STAFFINFO as a left join TBDS_DEPINFO as b on a.ST_DEPID=b.DEP_CODE left join TBDS_DEPINFO as d on a.ST_POSITION = d.DEP_CODE where ST_ID='" + st_id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            DataRow dr = dt.Rows[0];
            //绑定文件

            if (dt.Rows[0]["ST_WENJIAN"].ToString() != "")
            {
                Hidden.Value = dt.Rows[0]["ST_WENJIAN"].ToString();
            }
            foreach (Control control in Panel1.Controls)
            {
                if (control is TextBox)
                {

                    ((TextBox)control).Text = dr[((TextBox)control).ID.ToString()].ToString();
                }
                else if (control is DropDownList)
                {
                    DEP_NAME.ID = "ST_DEPID";
                    ((DropDownList)control).SelectedValue = dr[((DropDownList)control).ID.ToString()].ToString();
                    DEP_NAME.ID = "DEP_NAME";
                }
            }
            string role = dr["R_NAME"].ToString();
            if (!string.IsNullOrEmpty(role))
            {
                string[] roles = role.Split(',');
                string uRole = "";
                for (int i = 0; i < roles.Length; i++)
                {
                    uRole = roles[i].Substring(1, roles[i].Length - 2);
                    for (int j = 0; j < chk_Role.Items.Count; j++)
                    {
                        if (uRole == chk_Role.Items[j].Text)
                        {
                            chk_Role.Items[j].Selected = true;
                        }
                    }
                }
            }


            Ddl_Post();
            DEP_POSITION.SelectedValue = dr["ST_POSITION"].ToString();
            showImage.ImageUrl = "~/staff_images/" + dr["JPGURL"].ToString();
            sqlText = "select * from TBDS_WORKHIS where ST_ID='" + st_id + "'";
            Det_Repeater.DataSource = DBCallCommon.GetDTUsingSqlText(sqlText);
            Det_Repeater.DataBind();
            sqlText = "select * from TBDS_EDUCA where ST_ID='" + st_id + "'";
            Det_Repeater1.DataSource = DBCallCommon.GetDTUsingSqlText(sqlText);
            Det_Repeater1.DataBind();
            sqlText = "select * from TBDS_RELATION where ST_ID='" + st_id + "'";
            Det_Repeater2.DataSource = DBCallCommon.GetDTUsingSqlText(sqlText);
            Det_Repeater2.DataBind();
            sqlText = "select * from OM_RenYuanDiaoDong where MOVE_PERNAME='" + ST_NAME.Text + "' and MOVE_STATE=MOVE_AUTH_RATING";
            Det_Repeater3.DataSource = DBCallCommon.GetDTUsingSqlText(sqlText);
            Det_Repeater3.DataBind();
            InitVar();
            InitVar1();
            InitVar2();
            InitVar3();
        }

        protected void btnupload_Click(object sender, EventArgs e)//上传功能
        {
            #region 现在的
            string time1 = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string FilePath = @"D:\数字平台\平台代码\TS_ZCZJ_DPF（2015-07-06)\ZCZJ_DPF\staff_images";
            if (!Directory.Exists(FilePath))//如果不存在文件路径就创建一个
            {
                Response.Write("<script>alert('不存在此文件夹！！！请与管理员联系！！！')</script>");
                return;
            }
            //对客户端已上载的单独文件的访问
            HttpPostedFile UserHPF = FileUploadupdate.PostedFile;
            try
            {
                string fileContentType = UserHPF.ContentType;// 获取客户端发送的文件的 MIME 内容类型 
                if (fileContentType == "image/bmp" || fileContentType == "image/gif" || fileContentType == "image/jpeg" || fileContentType == "image/jpg" || fileContentType == "image/pjpeg")//传送文件类型
                {
                    if (UserHPF.ContentLength > 0)
                    {
                        //调用GetAutoID方法获取上传文件自动编号
                        //int IntFieldID = CC.GetAutoID("fileID", "tb_files");
                        //文件的真实名（格式：[文件编号]上传文件名）
                        //用于实现上传多个相同文件时，原有文件不被覆盖
                        string strOldFile = System.IO.Path.GetFileName(UserHPF.FileName);
                        string strExtent = strOldFile.Substring(strOldFile.LastIndexOf("."));
                        string strNewFile = time1 + strExtent;
                        if (!File.Exists(FilePath + "//" + strNewFile))
                        {
                            //定义插入字符串，将上传文件信息保存在数据库中
                            if (Request.QueryString["action"] != "add")
                            {
                                string st_id = Request.QueryString["ST_ID"].ToString();
                                string sql = string.Format("update TBDS_STAFFINFO set JPGURL='{0}' where ST_ID={1}", time1, st_id);
                                DBCallCommon.ExeSqlText(sql);
                            }
                            UserHPF.SaveAs(FilePath + "//" + strNewFile);//将上传的文件存放在指定的文件夹中
                        }
                        else
                        {
                            lblupload.Visible = true;
                            lblupload.Text = "文件名与服务器某个合同名重名，请您核对后重新上传！";
                        }

                    }
                }
                else
                {
                    lblupload.Visible = true;
                    lblupload.Text = "文件类型不符合要求，请您核对后重新上传！";

                }
            }
            catch
            {
                lblupload.Text = "文件上传过程中出现错误！";
                lblupload.Visible = true;
                return;
            }


            if (FileUploadupdate.HasFile)
            {
                string datatime = time1 + ".jpg";
                string webfilepath = Server.MapPath("~/staff_images/" + datatime);
                string filecontent_type = FileUploadupdate.PostedFile.ContentType;
                if (filecontent_type == "image/bmp" || filecontent_type == "image/gif" || filecontent_type == "image/jpeg" || filecontent_type == "image/jpg" || filecontent_type == "image/pjpeg")
                {
                    if (!File.Exists(webfilepath))
                    {
                        try
                        {
                            FileUploadupdate.SaveAs(webfilepath);
                            lblupload.Visible = true;
                            lblupload.Text = "上传成功";
                            showImage.ImageUrl = "~/staff_images/" + datatime;
                            if (Request.QueryString["action"] != "add")//也可用DBCallmand.Excyte。
                            {
                                string st_id = Request.QueryString["ST_ID"].ToString();
                                SqlCommand sqlCmd = new SqlCommand();
                                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                                sqlCmd.CommandText = "update TBDS_STAFFINFO set JPGURL=@filename where ST_ID=" + "'" + st_id + "'";
                                sqlCmd.Parameters.Clear();
                                sqlCmd.Parameters.AddWithValue("@filename", datatime);
                                sqlCmd.Connection = sqlConn;
                                sqlConn.Open();
                                sqlCmd.ExecuteNonQuery();
                                sqlConn.Close();
                            }
                            else
                            {
                                JPG.Visible = false;
                                JPG.Text = datatime;
                            }
                        }
                        catch (Exception ex)
                        {
                            lblupload.Visible = true;
                            lblupload.Text = "文件上传失败" + ex.Message;
                        }
                    }
                    else
                    {
                        lblupload.Visible = true;
                        lblupload.Text = "文件已经存在，重新命名!";
                    }
                }
                else
                {
                    lblupload.Visible = true;
                    lblupload.Text = "文件类型不符，重新上传!";
                }
            }
            else
            {
                lblupload.Visible = true;
                lblupload.Text = "请选择文件!";
            }
            #endregion

            #region 原来的
            //if (FileUploadupdate.HasFile)
            //{
            //    string datatime = DateTime.Now.ToString("yyyyMMddHHmmssff") + ".jpg";
            //    string webfilepath = Server.MapPath("~/staff_images/" + datatime);
            //    string filecontent_type = FileUploadupdate.PostedFile.ContentType;
            //    if (filecontent_type == "image/bmp" || filecontent_type == "image/gif" || filecontent_type == "image/jpeg" || filecontent_type == "image/jpg" || filecontent_type == "image/pjpeg")
            //    {
            //        if (!File.Exists(webfilepath))
            //        {
            //            try
            //            {
            //                FileUploadupdate.SaveAs(webfilepath);
            //                lblupload.Visible = true;
            //                lblupload.Text = "上传成功";
            //                showImage.ImageUrl = "~/staff_images/" + datatime;
            //                if (Request.QueryString["action"] != "add")//也可用DBCallmand.Excyte。
            //                {
            //                    string st_id = Request.QueryString["ST_ID"].ToString();
            //                    SqlCommand sqlCmd = new SqlCommand();
            //                    sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            //                    sqlCmd.CommandText = "update TBDS_STAFFINFO set JPGURL=@filename where ST_ID=" + "'" + st_id + "'";
            //                    sqlCmd.Parameters.Clear();
            //                    sqlCmd.Parameters.AddWithValue("@filename", datatime);
            //                    sqlCmd.Connection = sqlConn;
            //                    sqlConn.Open();
            //                    sqlCmd.ExecuteNonQuery();
            //                    sqlConn.Close();
            //                }
            //                else
            //                {
            //                    JPG.Visible = false;
            //                    JPG.Text = datatime;
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                lblupload.Visible = true;
            //                lblupload.Text = "文件上传失败" + ex.Message;
            //            }
            //        }
            //        else
            //        {
            //            lblupload.Visible = true;
            //            lblupload.Text = "文件已经存在，重新命名!";
            //        }
            //    }
            //    else
            //    {
            //        lblupload.Visible = true;
            //        lblupload.Text = "文件类型不符，重新上传!";
            //    }
            //}
            //else
            //{
            //    lblupload.Visible = true;
            //    lblupload.Text = "请选择文件!";
            //}
            #endregion
        }

        protected void DEP_NAME_SelectedIndexChanged(object sender, EventArgs e) //部门变化
        {
            Ddl_Post();
        }

        protected void Ddl_Post() //将职位信息绑定到职位下拉框
        {
            string sqlText = string.Format("select DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_FATHERID='{0}' and DEP_NAME<>'' and DEP_NAME is not null", DEP_NAME.SelectedValue);//按照表里面的顺序排。不然直接distinct会按拼音排序。
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            DEP_POSITION.DataSource = dt;
            DEP_POSITION.DataTextField = "DEP_NAME";
            DEP_POSITION.DataValueField = "DEP_CODE";
            DEP_POSITION.DataBind();
            AddNew(DEP_POSITION);
        }

        protected void btn_Back_Click(object sender, EventArgs e)
        {
            if (actionstr == "add")
            {
                Response.Redirect("Sta_detail.aspx");
            }
            else
            {
                Response.Write("<script>window.close()</script>");
            }
        }

        #region 增加行
        protected void btnadd_Click(object sender, EventArgs e)
        {
            CreateNewRow(1);
        }

        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();
            InitVar();
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ST_GZSTART");
            dt.Columns.Add("ST_GZDW");
            dt.Columns.Add("ST_POSITION");
            dt.Columns.Add("ST_REAOUT");
            dt.Columns.Add("ST_SALARY");
            dt.Columns.Add("ST_INDENTITY");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)retItem.FindControl("ST_GZSTART")).Text;
                newRow[1] = ((TextBox)retItem.FindControl("ST_GZDW")).Text;
                newRow[2] = ((TextBox)retItem.FindControl("ST_POSITION")).Text;
                newRow[3] = ((TextBox)retItem.FindControl("ST_REAOUT")).Text;
                newRow[4] = ((TextBox)retItem.FindControl("ST_SALARY")).Text;
                newRow[5] = ((TextBox)retItem.FindControl("ST_INDENTITY")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        private void InitVar()
        {
            if (Det_Repeater.Items.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            else
            {
                NoDataPanel.Visible = false;
                delete.Visible = true;
            }
        }

        #endregion

        #region 增加行
        protected void btnadd1_Click(object sender, EventArgs e)
        {
            CreateNewRow1(1);
        }

        private void CreateNewRow1(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable1();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            this.Det_Repeater1.DataSource = dt;
            this.Det_Repeater1.DataBind();
            InitVar1();
        }

        private DataTable GetDataTable1()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ST_JYTIME");
            dt.Columns.Add("ST_SCHOOL");
            dt.Columns.Add("ST_ZHUAN");
            dt.Columns.Add("ST_ENGLISH");
            foreach (RepeaterItem retItem in Det_Repeater1.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)retItem.FindControl("ST_JYTIME")).Text;
                newRow[1] = ((TextBox)retItem.FindControl("ST_SCHOOL")).Text;
                newRow[2] = ((TextBox)retItem.FindControl("ST_ZHUAN")).Text;
                newRow[3] = ((TextBox)retItem.FindControl("ST_ENGLISH")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        private void InitVar1()
        {
            if (Det_Repeater1.Items.Count == 0)
            {
                NoDataPanel1.Visible = true;
            }
            else
            {
                NoDataPanel1.Visible = false;
                delete1.Visible = true;
            }
        }

        #endregion

        #region 增加行
        protected void btnadd2_Click(object sender, EventArgs e)
        {
            CreateNewRow2(1);
        }

        private void CreateNewRow2(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable2();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            this.Det_Repeater2.DataSource = dt;
            this.Det_Repeater2.DataBind();
            InitVar2();
        }

        private DataTable GetDataTable2()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ST_NAME");
            dt.Columns.Add("ST_AGE");
            dt.Columns.Add("ST_RELATION");
            dt.Columns.Add("ST_WORK");
            dt.Columns.Add("ST_TELE");
            foreach (RepeaterItem retItem in Det_Repeater2.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)retItem.FindControl("ST_NAME")).Text;
                newRow[1] = ((TextBox)retItem.FindControl("ST_AGE")).Text;
                newRow[2] = ((TextBox)retItem.FindControl("ST_RELATION")).Text;
                newRow[3] = ((TextBox)retItem.FindControl("ST_WORK")).Text;
                newRow[4] = ((TextBox)retItem.FindControl("ST_TELE")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        private void InitVar2()
        {
            if (Det_Repeater2.Items.Count == 0)
            {
                NoDataPanel2.Visible = true;
            }
            else
            {
                NoDataPanel2.Visible = false;
                delete2.Visible = true;
            }
        }

        private void InitVar3()
        {
            if (Det_Repeater3.Items.Count == 0)
            {
                NoDataPanel3.Visible = true;
            }
            else
            {
                NoDataPanel3.Visible = false;

            }
        }

        #endregion

        protected void delete_Click(object sender, EventArgs e) //工作履历表的删除事件
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ST_GZSTART");
            dt.Columns.Add("ST_GZDW");
            dt.Columns.Add("ST_POSITION");
            dt.Columns.Add("ST_REAOUT");
            dt.Columns.Add("ST_SALARY");
            dt.Columns.Add("ST_INDENTITY");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((TextBox)retItem.FindControl("ST_GZSTART")).Text;
                    newRow[1] = ((TextBox)retItem.FindControl("ST_GZDW")).Text;
                    newRow[2] = ((TextBox)retItem.FindControl("ST_POSITION")).Text;
                    newRow[3] = ((TextBox)retItem.FindControl("ST_REAOUT")).Text;
                    newRow[4] = ((TextBox)retItem.FindControl("ST_SALARY")).Text;
                    newRow[5] = ((TextBox)retItem.FindControl("ST_INDENTITY")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();
            InitVar();
        }

        protected void delete1_Click(object sender, EventArgs e)  //教育及培训记录的删除事件
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ST_JYTIME");
            dt.Columns.Add("ST_SCHOOL");
            dt.Columns.Add("ST_ZHUAN");
            dt.Columns.Add("ST_ENGLISH");
            foreach (RepeaterItem retItem in Det_Repeater1.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((TextBox)retItem.FindControl("ST_JYTIME")).Text;
                    newRow[1] = ((TextBox)retItem.FindControl("ST_SCHOOL")).Text;
                    newRow[2] = ((TextBox)retItem.FindControl("ST_ZHUAN")).Text;
                    newRow[3] = ((TextBox)retItem.FindControl("ST_ENGLISH")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.Det_Repeater1.DataSource = dt;
            this.Det_Repeater1.DataBind();
            InitVar1();
        }

        protected void delete2_Click(object sender, EventArgs e) //家庭成员表的删除事件
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ST_NAME");
            dt.Columns.Add("ST_AGE");
            dt.Columns.Add("ST_RELATION");
            dt.Columns.Add("ST_WORK");
            dt.Columns.Add("ST_TELE");
            foreach (RepeaterItem retItem in Det_Repeater2.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((TextBox)retItem.FindControl("ST_NAME")).Text;
                    newRow[1] = ((TextBox)retItem.FindControl("ST_AGE")).Text;
                    newRow[2] = ((TextBox)retItem.FindControl("ST_RELATION")).Text;
                    newRow[3] = ((TextBox)retItem.FindControl("ST_WORK")).Text;
                    newRow[4] = ((TextBox)retItem.FindControl("ST_TELE")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.Det_Repeater2.DataSource = dt;
            this.Det_Repeater2.DataBind();
            InitVar2();
        }

        protected void confirm_Click(object sender, EventArgs e) //确定提交事件
        {
            string rName = "";
            for (int i = 0; i < chk_Role.Items.Count; i++)
            {
                if (chk_Role.Items[i].Selected)
                {
                    rName += ",''" + chk_Role.Items[i].Text + "''";
                }
            }
            if (rName != "")
            {
                rName = rName.Substring(1);
            }

            if (actionstr == "add")
            {
                #region 添加基本数据
                string addText;
                string addcol = string.Empty;
                string addvalue = string.Empty;
                int id = 0;

                List<string> list = new List<string>();
                Dictionary<string, object> dic = new Dictionary<string, object>();//用于存储输入的数据
                foreach (Control control in Panel1.Controls)
                {
                    if (control is TextBox)
                    {
                        if (((TextBox)control).Text != "")
                        {
                            dic.Add(control.ID, ((TextBox)control).Text);
                        }
                    }
                    else if (control is DropDownList)
                    {
                        if (((DropDownList)control).SelectedIndex != 0 && ((DropDownList)control).SelectedValue != "00")
                        {
                            if (DEP_NAME.ID != "ST_DEPID")
                                DEP_NAME.ID = "ST_DEPID";
                            dic.Add(control.ID, ((DropDownList)control).SelectedValue);
                        }
                    }
                }
                foreach (KeyValuePair<string, object> pair in dic)
                {
                    addcol += pair.Key.ToString() + ",";
                    addvalue += "'" + pair.Value + "',";
                }
                if (DEP_POSITION.SelectedIndex != 0)
                {
                    addcol += "ST_POSITION,";
                    addvalue += "'" + DEP_POSITION.SelectedValue + "',";
                }
                string maxid = "select max(ST_ID) from TBDS_STAFFINFO";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(maxid);
                id = Convert.ToInt32(dt.Rows[0][0].ToString());
                id++;
                addcol += "ST_ID,ST_FILLDATE," + "ST_MANCLERK," + "JPGURL,R_NAME,ST_WENJIAN";
                addvalue += "'" + id + "','" + ST_FILLDATE.Text + "','" + Session["UserID"].ToString() + "','" + JPG.Text + "','" + rName + "','" + Hidden.Value + "'";
                addText = string.Format("insert into TBDS_STAFFINFO({0}) values({1})", addcol, addvalue);
                list.Add(addText);
                //string addwenjian = string.Format("insert into TBDS_STAFFINFO({0}) values({1})", "ST_WENJIAN", Hidden.Value);//更新上传文件
                //list.Add(addwenjian);
                #endregion

                #region 添加履历和教育信息和家庭
                GetWork(list, id.ToString());
                #endregion

                //员工合同记录
                if (!string.IsNullOrEmpty(ST_CONTRSTART.Text.ToString().Trim()))
                {
                    string contractdate = ST_CONTRSTART.Text.ToString().Trim().Replace('-', '.') + "-" + ST_CONTREND.Text.ToString().Trim().Replace('-', '.');
                    string sqlcontractrecord = "insert into  OM_ContractRecord(C_STID,C_STName,C_STDep,C_STContract,C_Contract1,C_ImportPersonID,C_ImportPerson,C_ImportTime) values ('" + id + "','" + ST_NAME.Text.Trim() + "','" + DEP_NAME.SelectedItem.Text.Trim() + "','" + ST_CONTR.Text.ToString().Trim() + "','" + contractdate + "','" + Session["UserID"].ToString() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    list.Add(sqlcontractrecord);
                }

                string EditRecord = "insert into TBDS_STAFFINFO_EditRecord (bSTID, bSTNAME, Type, EditTime, EditPerId, EditPerName, Caozuo) values ('" + id + "','" + ST_NAME.Text.Trim() + "','人员信息表','" + DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss") + "','" + Session["UserId"].ToString() + "','" + Session["UserName"].ToString() + "','添加')";
                list.Add(EditRecord);
                DBCallCommon.ExecuteTrans(list);
                Response.Write("<script>alert('添加成功！')</script>");
                Response.Redirect("Sta_detail.aspx");
            }
            else
            {
                #region 更新代码
                string st_id = Request.QueryString["ST_ID"].ToString();
                string sql = string.Empty;
                List<string> list = new List<string>();
                string sqlText = "select distinct a.*,b.DEP_NAME,d.DEP_NAME as DEP_POSITION from TBDS_STAFFINFO as a left join TBDS_DEPINFO as b on a.ST_DEPID=b.DEP_CODE left join TBDS_DEPINFO as d on a.ST_POSITION = d.DEP_CODE where ST_ID='" + st_id + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                DataRow dr = dt.Rows[0];
                foreach (Control control in Panel1.Controls)
                {
                    if (control is TextBox)
                    {
                        if (((TextBox)control).Text != dr[((TextBox)control).ID.ToString()].ToString())
                        {
                            sql = string.Format("update TBDS_STAFFINFO set {0}='{1}' where ST_ID={2}", control.ID, ((TextBox)control).Text, st_id);
                            list.Add(sql);
                        }
                    }
                    else if (control is DropDownList)
                    {
                        if (((DropDownList)control).SelectedValue != dr[((DropDownList)control).ID.ToString()].ToString())
                        {
                            if (((DropDownList)control).ID == "DEP_NAME")
                            {
                                control.ID = "ST_DEPID";
                            }
                            string dropSele = (((DropDownList)control).SelectedValue == "00" ? "" : ((DropDownList)control).SelectedValue);
                            sql = string.Format("update TBDS_STAFFINFO set {0}='{1}' where ST_ID={2}", control.ID, dropSele, st_id);
                            list.Add(sql);
                        }
                    }
                }
                if (DEP_POSITION.SelectedIndex != 0)
                {
                    sql = string.Format("update TBDS_STAFFINFO set ST_POSITION='{0}' where ST_ID={1}", DEP_POSITION.SelectedValue, st_id);
                    list.Add(sql);
                }
                sql = string.Format("update TBDS_STAFFINFO set ST_WENJIAN='{0}' where ST_ID='{1}'", Hidden.Value, st_id);//更新上传文件
                list.Add(sql);
                sql = string.Format("update TBDS_STAFFINFO set ST_FILLDATE='{0}' where ST_ID='{1}'", ST_FILLDATE.Text, st_id);
                list.Add(sql);
                sql = string.Format("update TBDS_STAFFINFO set ST_MANCLERK='{0}' where ST_ID='{1}'", Session["UserID"].ToString(), st_id);
                list.Add(sql);
                sql = string.Format("update TBDS_STAFFINFO set R_NAME='{0}' where ST_ID='{1}'", rName, st_id);
                list.Add(sql);
                sql = string.Format("delete from TBDS_WORKHIS where ST_ID='{0}'", st_id);
                list.Add(sql);
                sql = string.Format("delete from TBDS_EDUCA where ST_ID='{0}'", st_id);
                list.Add(sql);
                sql = string.Format("delete from TBDS_RELATION where ST_ID='{0}'", st_id);
                list.Add(sql);
                GetWork(list, st_id);

                //员工合同记录
                if (!string.IsNullOrEmpty(ST_CONTRSTART.Text.ToString().Trim()))
                {
                    string contractdate = ST_CONTRSTART.Text.ToString().Trim().Replace('-', '.') + "-" + ST_CONTREND.Text.ToString().Trim().Replace('-', '.');
                    string sqlvalidcontract = "select * from OM_ContractRecord where C_STID='" + st_id + "' and C_STContract='" + ST_CONTR.Text.ToString().Trim() + "'";
                    DataTable dtvalidcontract = DBCallCommon.GetDTUsingSqlText(sqlvalidcontract);
                    if (dtvalidcontract.Rows.Count > 0)
                    {
                        int m = 0;
                        for (int k = 5; k < dtvalidcontract.Columns.Count - 3; k++)
                        {
                            if (string.IsNullOrEmpty(dtvalidcontract.Rows[0][k].ToString()))
                            {
                                m = k - 5;
                                break;
                            }
                            else
                                continue;
                        }
                        string keyvalid = "C_Contract1,C_Contract2,C_Contract3,C_Contract4,C_Contract5,C_Contract6,C_Contract7,C_Contract8,C_Contract9,C_Contract10,C_Contract11,C_Contract12,C_Contract13,C_Contract14,C_Contract15,C_Contract16,C_Contract17,C_Contract18,C_Contract19,C_Contract20,C_Contract21,C_Contract22,C_Contract23,C_Contract24,C_Contract25,C_Contract26,C_Contract27,C_Contract28,C_Contract29,C_Contract30";
                        if (m > 0)
                            if (dtvalidcontract.Rows[0][m + 4].ToString() != contractdate)
                            {
                                string sqlcontractrecord = "update OM_ContractRecord set " + keyvalid.Split(',')[m] + " ='" + contractdate + "'where C_STID='" + st_id + "' and C_STContract='" + ST_CONTR.Text.ToString().Trim() + "'";
                                list.Add(sqlcontractrecord);
                            }
                    }
                }

                string EditRecord = "insert into TBDS_STAFFINFO_EditRecord (bSTID, bSTNAME, Type, EditTime, EditPerId, EditPerName, Caozuo) values ('','" + ST_NAME.Text.Trim() + "','人员信息表','" + DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss") + "','" + Session["UserId"].ToString() + "','" + Session["UserName"].ToString() + "','修改')";
                list.Add(EditRecord);

                DBCallCommon.ExecuteTrans(list);
                Response.Write("<script>alert('修改成功！')</script>");
                Response.Write("<script>window.close()</script>");
                #endregion
            }
        }

        protected void GetWork(List<string> list, string id) //获取工作履历表和教育经历表以及家庭的数据
        {
            string a = string.Empty;
            string b = a;
            string c = a;
            string d = a;
            string e = a;
            string f = a;
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                a = ((TextBox)retItem.FindControl("ST_GZSTART")).Text;
                b = ((TextBox)retItem.FindControl("ST_GZDW")).Text;
                c = ((TextBox)retItem.FindControl("ST_POSITION")).Text;
                d = ((TextBox)retItem.FindControl("ST_REAOUT")).Text;
                e = ((TextBox)retItem.FindControl("ST_SALARY")).Text;
                f = ((TextBox)retItem.FindControl("ST_INDENTITY")).Text;
                if (a != "" || b != "")
                {
                    string sql = string.Format("insert into TBDS_WORKHIS values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", id, a, b, c, d, e, f);
                    list.Add(sql);
                }
            }
            foreach (RepeaterItem retItem in Det_Repeater1.Items)
            {
                a = ((TextBox)retItem.FindControl("ST_JYTIME")).Text;
                b = ((TextBox)retItem.FindControl("ST_SCHOOL")).Text;
                c = ((TextBox)retItem.FindControl("ST_ZHUAN")).Text;
                d = ((TextBox)retItem.FindControl("ST_ENGLISH")).Text;
                if (a != "" || b != "")
                {
                    string sql = string.Format("insert into TBDS_EDUCA values('{0}','{1}','{2}','{3}','{4}')", id, a, b, c, d);
                    list.Add(sql);
                }
            }
            foreach (RepeaterItem retItem in Det_Repeater2.Items)
            {
                a = ((TextBox)retItem.FindControl("ST_NAME")).Text;
                b = ((TextBox)retItem.FindControl("ST_AGE")).Text;
                c = ((TextBox)retItem.FindControl("ST_RELATION")).Text;
                d = ((TextBox)retItem.FindControl("ST_WORK")).Text;
                e = ((TextBox)retItem.FindControl("ST_TELE")).Text;
                if (a != "" || b != "")
                {
                    string sql = string.Format("insert into TBDS_RELATION values('{0}','{1}','{2}','{3}','{4}','{5}')", id, a, b, c, d, e);
                    list.Add(sql);
                }
            }
        }
    }
}
