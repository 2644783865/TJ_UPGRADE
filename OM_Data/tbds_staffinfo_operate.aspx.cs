using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ZCZJ_DPF;
using System.Data.SqlClient;
using System.IO;

namespace testpage
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = "岗位信息管理";
                lblupdate.Visible = false;
                lblupload2.Visible = false;
                string actionstr = Request.QueryString["action"].ToString();
                GetDepartment1();                                                  //获取部门下拉框的值
                //getjobcat();                                                       //获取工种下拉框的值
                if (actionstr == "add")
                {
                    btnupdate.Text = "增加";
                    //ST_MANCLERK.Text = Session["UserName"].ToString();
                    ST_FILLDATE.Text = System.DateTime.Now.ToString();
                    Panel2.CssClass = "";
                    TextBoxdept.Visible = false;
                    Panel1.Enabled = true;
                }
                else if (actionstr == "update")
                {
                    //ST_MANCLERK.Text = Session["UserName"].ToString();
                    ST_FILLDATE.Text = System.DateTime.Now.ToString();
                    Panel2.CssClass = "";
                    TextBoxdept.Visible = false;
                    Panel1.Enabled = true;
                    Showdata();
                }
                else
                {
                    btnupdate.Visible = false;
                    DropDownListdept.Visible = false;
                    Showdata();
                }
            }
            DBCallCommon.closeConn(sqlConn);
        }

        protected void Showdata()         //显示出数据
        {
            string st_codestr = Request.QueryString["ST_CODE"].ToString();//得到修改人员编码
            ST_CODE.Text = st_codestr;
            ///Session["code"] = st_codestr;                                  //保存编码
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            SqlCommand sqlCmd = new SqlCommand("select * from TBDS_STAFFINFO where ST_ID='" + ST_CODE.Text + "'", sqlConn);
            sqlConn.Open();
            SqlDataReader dr = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
            if (dr.Read())                                                               //设置修改人员的初始值
            {
                setdeplistvalue(dr["ST_CODE"].ToString());                             //根据ST_DEPID设置部门下拉框的值
                ST_CODE.Text = dr["ST_CODE"].ToString();
                ST_NAME.Text = dr["ST_NAME"].ToString();
                ST_MANCLERK.Text = dr["ST_MANCLERK"].ToString();
                DropDownListgender.SelectedValue = dr["ST_GENDER"].ToString();
                ST_POSITION.Text = dr["ST_POSITION"].ToString();
                ST_SEQUEN.Text = dr["ST_SEQUEN"].ToString();
                ST_NOTE.Text = dr["ST_NOTE"].ToString();
                ST_FILLDATE.Text = dr["ST_FILLDATE"].ToString();
                DropDownListdept.SelectedValue = dr["ST_DEPID"].ToString();
                TextBoxdept.Text = DropDownListdept.SelectedItem.Text;
                ST_IDCARD.Text = dr["ST_IDCARD"].ToString();
                ST_TELE.Text = dr["ST_TELE"].ToString();
                ST_REGIST.Text = dr["ST_REGIST"].ToString();
                ST_MARRY.Text = dr["ST_MARRY"].ToString();
                ST_POLITICAL.Text = dr["ST_POLITICAL"].ToString();
                ST_AGE.Text = dr["ST_AGE"].ToString();
                showImage.ImageUrl = "~/staff_images/" + dr["JPGURL"].ToString();
                int i = 26;
                foreach (Control control in Panel4.Controls)
                {
                    if (control is TextBox)
                    {
                        ((TextBox)control).Text = dr[i].ToString();
                        i++;
                    }
                }
            }
            dr.Close();
            //sqlConn.Close();
            Console.WriteLine("读取完毕后的连接状态："+sqlConn.State.ToString());

        }

        protected void setdeplistvalue(string str)      //根据ST_DEPID设置部门下拉框的值
        {
            if (str.ToString() == "01")//若是公司领导
            {
                ListItem item = new ListItem();
                item.Text = "无";
                item.Value = "01";
                DropDownListdept.SelectedValue = "01";
            }
            else//若是其他部门
            {
                DropDownListdept.SelectedValue = str.ToString();
            }
            TextBoxdept.Text = DropDownListdept.SelectedValue.ToString();//暂存部门下拉框初始值
        }

        private void GetDepartment1()
        {
            string sqlText = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            DropDownListdept.DataSource = dt;
            DropDownListdept.DataTextField = "DEP_NAME";
            DropDownListdept.DataValueField = "DEP_CODE";
            DropDownListdept.DataBind();
            ListItem item = new ListItem();
            item.Text = "--请选择部门--";
            item.Value = "0";
            DropDownListdept.Items.Insert(0, item);
            DropDownListdept.SelectedValue = "0";
        }

        protected void btnupload2_Click(object sender, EventArgs e)
        {
            if (FileUploadupdate.HasFile)
            {
                string datatime = DateTime.Now.Year.ToString() + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ".jpg";
                string webfilepath = Server.MapPath("~/staff_images/" + datatime);
                string filecontent_type = FileUploadupdate.PostedFile.ContentType;
                if (filecontent_type == "image/bmp" || filecontent_type == "image/gif" || filecontent_type == "image/jpeg" || filecontent_type == "image/jpg" || filecontent_type == "image/pjpeg" )
                {
                    if (!File.Exists(webfilepath))
                    {
                        try
                        {
                            FileUploadupdate.SaveAs(webfilepath);
                            lblupdate.Visible = true;
                            lblupdate.Text = "上传成功";
                            showImage.ImageUrl = "~/staff_images/" + datatime;
                            SqlCommand sqlCmd = new SqlCommand();
                            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                            sqlCmd.CommandText = "update TBDS_STAFFINFO set JPGURL=@filename where ST_NAME="+"'"+ST_NAME.Text+"'";
                            sqlCmd.Parameters.Clear();
                            sqlCmd.Parameters.AddWithValue("@filename", datatime);
                            sqlCmd.Connection = sqlConn;
                            sqlConn.Open();
                            sqlCmd.ExecuteNonQuery();
                            sqlConn.Close();
                        }
                        catch (Exception ex)
                        {
                            lblupload2.Visible = true;
                            lblupload2.Text = "文件上传失败" + ex.Message;
                        }
                    }
                    else
                    {
                        lblupload2.Visible = true;
                        lblupload2.Text = "文件已经存在，请重新命名";
                    }
                }
                else
                {
                    lblupload2.Visible = true;
                    lblupload2.Text = "文件类型不符，请重新上传";
                }
            }
            else
            {
                lblupload2.Visible = true;
                lblupload2.Text = "请选择文件或输入文件路径及名称";
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            string cmdstring;
            if (btnupdate.Text == "增加")
            {
                lblupdate.Visible = true;
                DBCallCommon.closeConn(sqlConn);
                cmdstring = "insert into TBDS_STAFFINFO(ST_NAME,ST_GENDER,ST_CODE,ST_DEPID,ST_FILLDATE,ST_NOTE,ST_POSITION,ST_SEQUEN,ST_IDCARD,ST_TELE,ST_REGIST,ST_MARRY,ST_POLITICAL,ST_AGE,ST_XUELI,ST_XUEWEI,ST_BIYE,ST_ZHUANYE,ST_BIYETIME,ST_EQXUELI,ST_EQZHICH,ST_ZHICH) values (@ST_NAME,@DropDownListgender,@ST_CODE,@DropDownListdept,@ST_FILLDATE,@ST_NOTE,@ST_POSITION,@ST_SEQUEN,@ST_IDCARD,@ST_TELE,@ST_REGIST,@ST_MARRY,@ST_POLITICAL,@ST_AGE,@ST_XUELI,@ST_XUEWEI,@ST_BIYE,@ST_ZHUANYE,@ST_BIYETIME,@ST_EQXUELI,@ST_EQZHICH,@ST_ZHICH)";
                int dr1 = tbds_staffinf_update(ST_NAME.Text.ToString(), DropDownListgender.SelectedValue.ToString(), ST_CODE.Text.ToString(), DropDownListdept.SelectedValue.ToString(), DateTime.Now.ToString(), ST_NOTE.Text.ToString(), ST_POSITION.Text.ToString(), ST_SEQUEN.Text.ToString(), ST_IDCARD.Text.ToString(), ST_TELE.Text.ToString(), ST_REGIST.Text.ToString(), ST_MARRY.Text.ToString(), ST_POLITICAL.Text.ToString(), ST_AGE.Text.ToString(), ST_XUELI.Text.ToString(), ST_XUEWEI.Text.ToString(), ST_BIYE.Text.ToString(), ST_ZHUANYE.Text.ToString(), ST_BIYETIME.Text.ToString(), ST_EQXUELI.Text.ToString(), ST_EQZHICH.Text.ToString(), ST_ZHICH.Text.ToString(), cmdstring);
                //int dr1 = tbds_staffinf_update(ST_CODE.Text.ToString(), ST_NAME.Text.ToString(), DropDownListgender.SelectedValue.ToString(), ST_POSITION.Text.ToString(), "", DropDownListdept.SelectedValue.ToString(), "", ST_MANCLERK.Text.ToString(), ST_FILLDATE.Text.ToString(), ST_NOTE.Text.ToString(), "", ST_SEQUEN.Text.ToString(), cmdstring);
                if (dr1 == 1)
                {
                    Response.Write("<script>javascript:window.close();</script>");
                }
                else
                {
                    lblupdate.Visible = true;
                    lblupdate.Text = "操作失败";
                }
            }
            else
            {
                ///ST_MANCLERK.Text = Session["UserName"].ToString();
                cmdstring = "update TBDS_STAFFINFO SET ST_NAME=@ST_NAME,ST_CODE=@ST_CODE,ST_GENDER=@DropDownListgender,ST_DEPID=@DropDownListdept,ST_FILLDATE=@ST_FILLDATE,ST_NOTE=@ST_NOTE,ST_POSITION=@ST_POSITION,ST_SEQUEN=@ST_SEQUEN,ST_IDCARD=@ST_IDCARD,ST_TELE=@ST_TELE,ST_REGIST=@ST_REGIST,ST_MARRY=@ST_MARRY,ST_POLITICAL=@ST_POLITICAL,ST_AGE=@ST_AGE,ST_XUELI=@ST_XUELI,ST_XUEWEI=@ST_XUEWEI,ST_BIYE=@ST_BIYE,ST_ZHUANYE=@ST_ZHUANYE,ST_BIYETIME=@ST_BIYETIME,ST_EQXUELI=@ST_EQXUELI,ST_EQZHICH=@ST_EQZHICH,ST_ZHICH=@ST_ZHICH where ST_ID=" + Request.QueryString["ST_CODE"].ToString();
                //cmdstring = "update TBDS_STAFFINFO SET ST_NAME=@ST_NAME,ST_CODE=@ST_CODE,ST_GENDER=@DropDownListgender,ST_POSITION=@ST_POSITION,ST_JOBCAT=@ST_JOBCAT,ST_DEPID=@DropDownListdept,ST_STATE=@DropDownListstate,ST_MANCLERK=@ST_MANCLERK,ST_FILLDATE=@ST_FILLDATE,ST_NOTE=@ST_NOTE,R_NAME=@R_NAME,ST_SEQUEN=@ST_SEQUEN WHERE ST_CODE='" + Session["code"] + "'";
                int dr1 = tbds_staffinf_update(ST_NAME.Text.ToString(), DropDownListgender.SelectedValue.ToString(), ST_CODE.Text.ToString(), DropDownListdept.SelectedValue.ToString(), DateTime.Now.ToString(), ST_NOTE.Text.ToString(), ST_POSITION.Text.ToString(), ST_SEQUEN.Text.ToString(), ST_IDCARD.Text.ToString(), ST_TELE.Text.ToString(), ST_REGIST.Text.ToString(), ST_MARRY.Text.ToString(), ST_POLITICAL.Text.ToString(), ST_AGE.Text.ToString(), ST_XUELI.Text.ToString(), ST_XUEWEI.Text.ToString(), ST_BIYE.Text.ToString(), ST_ZHUANYE.Text.ToString(), ST_BIYETIME.Text.ToString(), ST_EQXUELI.Text.ToString(), ST_EQZHICH.Text.ToString(), ST_ZHICH.Text.ToString(), cmdstring);
                if (dr1 == 1)
                {
                    Response.Write("<script>javascript:window.close();</script>");
                }
                else
                {
                    lblupdate.Visible = true;
                    lblupdate.Text = "操作失败";
                }
            }
        }
        protected int tbds_staffinf_update(string ST_NAME, string DropDownListgender, string ST_CODE, string DropDownListdept, string ST_FILLDATE, string ST_NOTE, string ST_POSITION, string ST_SEQUEN, string ST_IDCARD, string ST_TELE, string ST_REGIST, string ST_MARRY, string ST_POLITICAL, string ST_AGE, string ST_XUELI, string ST_XUEWEI, string ST_BIYE, string ST_ZHUANYE, string ST_BIYETIME, string ST_EQXUELI, string ST_EQZHICH, string ST_ZHICH, string cmdstring)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            sqlCmd.CommandText = cmdstring;
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.AddWithValue("@ST_NAME", ST_NAME);
            sqlCmd.Parameters.AddWithValue("@ST_CODE", ST_CODE);
            sqlCmd.Parameters.AddWithValue("@DropDownListgender", DropDownListgender);
            sqlCmd.Parameters.AddWithValue("@DropDownListdept", DropDownListdept);
            sqlCmd.Parameters.AddWithValue("@ST_FILLDATE", Convert.ToDateTime(ST_FILLDATE));
            sqlCmd.Parameters.AddWithValue("@ST_NOTE", ST_NOTE);
            sqlCmd.Parameters.AddWithValue("@ST_SEQUEN", ST_SEQUEN);
            sqlCmd.Parameters.AddWithValue("@ST_IDCARD", ST_IDCARD);
            sqlCmd.Parameters.AddWithValue("@ST_TELE", ST_TELE);
            sqlCmd.Parameters.AddWithValue("@ST_POSITION", ST_POSITION);
            sqlCmd.Parameters.AddWithValue("@ST_REGIST", ST_REGIST);
            sqlCmd.Parameters.AddWithValue("@ST_MARRY", ST_MARRY);
            sqlCmd.Parameters.AddWithValue("@ST_POLITICAL", ST_POLITICAL);
            sqlCmd.Parameters.AddWithValue("@ST_AGE", ST_AGE);
            sqlCmd.Parameters.AddWithValue("@ST_XUELI", ST_XUELI);
            sqlCmd.Parameters.AddWithValue("@ST_XUEWEI", ST_XUEWEI);
            sqlCmd.Parameters.AddWithValue("@ST_BIYE", ST_BIYE);
            sqlCmd.Parameters.AddWithValue("@ST_ZHUANYE", ST_ZHUANYE);
            sqlCmd.Parameters.AddWithValue("@ST_BIYETIME", ST_BIYETIME);
            sqlCmd.Parameters.AddWithValue("@ST_EQXUELI", ST_EQXUELI);
            sqlCmd.Parameters.AddWithValue("@ST_EQZHICH", ST_EQZHICH);
            sqlCmd.Parameters.AddWithValue("@ST_ZHICH", ST_ZHICH);
            sqlConn.Open();
            sqlCmd.Connection = sqlConn;
            int  rowsnum = sqlCmd.ExecuteNonQuery();
            DBCallCommon.closeConn(sqlConn);
            return rowsnum;
        }
       
        protected void DropDownListdept_SelectedIndexChanged(object sender, EventArgs e)
        {
            ST_CODE.Text = "";                        //清除编码
            if (DropDownListdept.SelectedValue == "0")//若部门级下拉框没有选择部门，则岗位/班组下拉框显示"------",此时无编码产生
            {

                ST_CODE.Text = "";
            }
            else
            {
                if (DropDownListdept.SelectedValue == "01")//如果增加公司领导，则只有5位编码，其他则有7位编码，01为公司领导的部门编码
                {
                    ListItem item = new ListItem();
                    item.Text = "无";
                    item.Value = "01";
                    ST_CODE.Text = encode();//调用编码函数，根据DropDownListgroup的值进行编码
                }
                else
                {
                    string str = DropDownListdept.SelectedValue;                //绑定岗位/班组下拉框的值，初始值选定为index=0
                    ST_CODE.Text = encode();            //调用编码函数，根据DropDownListgroup的值进行编码
                }
            }
        }

        protected string encode()                       //根据DropDownListgroup选定值进行编码
        {
            string code;
            //加载到修改页面时，如果部门和岗位/班组信息和初始值相等，说明人员编码不变，此时不能对其重新编码，返回初始编码值。
            if ((btnupdate.Text == "修改") && (DropDownListdept.SelectedValue == TextBoxdept.Text))
            {
                code = Convert.ToString(Session["code"]);
            }
            else
            {
                string depvalue = DropDownListdept.SelectedValue;
                string groupvalue;
                groupvalue = DropDownListdept.SelectedValue;//显然DropDownListdept.SelectedValue="0"是不调用encode函数，所以这句可以不要
                string sqlText = "select distinct ST_CODE from TBDS_STAFFINFO where ST_CODE LIKE '" + groupvalue + "[0-9][0-9][0-9]' order by ST_CODE";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                int rows = dt.Rows.Count;
                if (rows == 0)
                {
                    code = groupvalue + "001";
                }
                else
                {
                    string subcode1 = Convert.ToString(dt.Rows[rows - 1][0]);
                    int subcode2 = Convert.ToInt32(subcode1.Substring((subcode1.Length - 3), 3)) + 1;//取后三位字符
                    if (subcode2 < 10)
                    {
                        code = groupvalue + "00" + Convert.ToString(subcode2);
                    }
                    else
                    {
                        if (subcode2 < 100)
                        {
                            code = groupvalue + "0" + Convert.ToString(subcode2);
                        }
                        else
                        {
                            if (subcode2 < 1000)
                            {
                                code = groupvalue + Convert.ToString(subcode2);
                            }
                            else
                            {
                                code = groupvalue + getsubcode(dt, 999);//如果编码超过三位，先检测编码记录个数与最大编码的值是否相等，相等，则提示可以让管理员删除已不用的记录，不等，则可以进行插入编码。
                            }
                        }
                    }
                }
            }
            return code;
        }

        private string getsubcode(DataTable dt, int maxcodenum)
        {
            string temcode = "";
            if (dt.Rows.Count < maxcodenum)   //可以插入
            {
                int startnum = 0;
                int endnum = dt.Rows.Count - 1;
                int midnum;
                string startsubcode;
                int startsubcodenum;
                string endsubcode;
                int endsubcodenum;
                string midsubcode;
                int midsubcodenum;
                #region
                while (endnum - startnum > 1)
                {
                    midnum = (startnum + endnum) / 2;
                    startsubcode = Convert.ToString(dt.Rows[startnum][0]);
                    startsubcodenum = Convert.ToInt32(startsubcode.Substring((startsubcode.Length - 3), 3));//取后三位字符
                    endsubcode = Convert.ToString(dt.Rows[endnum][0]);
                    endsubcodenum = Convert.ToInt32(endsubcode.Substring((endsubcode.Length - 3), 3));//取后三位字符
                    midsubcode = Convert.ToString(dt.Rows[midnum][0]);
                    midsubcodenum = Convert.ToInt32(midsubcode.Substring((midsubcode.Length - 3), 3));//取后三位字符
                    if ((midsubcodenum - startsubcodenum) > (midnum - startnum))
                    {
                        //startnum = startnum;
                        endnum = midnum;

                    }
                    else
                    {
                        if ((endsubcodenum - midsubcodenum) > endnum - midnum)
                        {
                            startnum = midnum;
                            //endnum = endnum;
                        }
                        else
                        {

                        }
                    }
                }
                #endregion
                string subcode1 = Convert.ToString(dt.Rows[startnum][0]);
                int subcodenum1 = Convert.ToInt32(subcode1.Substring((subcode1.Length - 3), 3));//取后三位字符
                int subcode2 = subcodenum1 + 1;
                if (subcode2 < 10)
                {
                    temcode = "00" + Convert.ToString(subcode2);
                }
                else
                {
                    if (subcode2 < 100)
                    {
                        temcode = "0" + Convert.ToString(subcode2);
                    }
                    else
                    {
                        if (subcode2 < 1000)
                        {
                            temcode = Convert.ToString(subcode2);
                        }
                    }
                }
            }
            else                        //人员超过999
            {
                Response.Write("<script>alert('人员编码已超过预定值（999），请通知管理员删除不必须的人员信息，然后在进行添加！')</script>");
                temcode = "";
            }
            return temcode;
        }
    }
}
