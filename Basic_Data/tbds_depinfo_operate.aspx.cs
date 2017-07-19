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

namespace ZCZJ_DPF.Basic_Data
{
    public partial class tbds_depinfo_operate : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = "组织结构管理";
                lblupdate.Visible = false;
                string actionstr = Request.QueryString["action"].ToString();
                GetDepartment();
                if (actionstr == "add")                       //添加功能页面
                {
                    DEP_FILLDATE.Text = System.DateTime.Now.ToString();
                    DEP_MANCLERK.Text = Session["UserName"].ToString();
                    #region
                    btnupdate.Text = "增加";
                    string addgropstr = Request.QueryString["selectaddgroup"].ToString();//获取添加级别（一级/二级）
                    if (addgropstr == "1")                                                //一级，添加部门，默认无父节点，编码为两位
                    {
                        fatherdept.Visible = false;
                        fatherdeptl.Visible = false;
                        this.Title = "添加部门";
                        Laddmessage.Text = "添加部门";
                        DEP_ISYENODE.SelectedValue = "N";
                        DEP_SFJY.SelectedValue = "0";
                        DEP_CY.SelectedValue = "0";
                        DEP_ISYENODE.Enabled = false;
                        getdepcode();
                        fatherdept.Items.Clear();
                        ListItem item = new ListItem();
                        item.Text = "0";
                        item.Value = "0";
                        fatherdept.Items.Insert(0, item);
                    }
                    else                                   //添加二级部门（班组/岗位），编码有父部门决定
                    {
                        if (addgropstr == "2")
                        {
                            fatherdept.Visible = true;
                            fatherdeptl.Visible = true;
                            Laddmessage.Text = "添加班组/岗位";
                            DEP_ISYENODE.SelectedValue = "Y";
                            DEP_SFJY.SelectedValue = "0";
                            DEP_CY.SelectedValue = "0";
                            DEP_ISYENODE.Enabled = false;
                        }
                        else
                        {
                            if (addgropstr == "3")
                            {
                                fatherdept.Visible = true;
                                fatherdeptl.Visible = true;
                                Laddmessage.Text = "添加班组";
                                DEP_ISYENODE.SelectedValue = "Y";
                                DEP_SFJY.SelectedValue = "0";
                                DEP_CY.SelectedValue = "0";
                                DEP_ISYENODE.Enabled = false;
                            }
                            else
                            {
                                if (addgropstr == "4")
                                {
                                    fatherdept.Visible = true;
                                    fatherdeptl.Visible = true;
                                    Laddmessage.Text = "添加工种";
                                    DEP_ISYENODE.SelectedValue = "Y";
                                    DEP_SFJY.SelectedValue = "0";
                                    DEP_CY.SelectedValue = "0";
                                    DEP_ISYENODE.Enabled = false;
                                }
                            }
                        }
                    }
                    #endregion
                }
                else if (actionstr == "update")                                   //修改功能页面
                {
                    DEP_FILLDATE.Text = System.DateTime.Now.ToString();
                    DEP_MANCLERK.Text = Session["UserName"].ToString();
                    string dep_codestr = Request.QueryString["DEP_CODE"].ToString();//获取修改的部门编码
                    Session["depcode"] = dep_codestr;                              //存储修改的部门编码
                    DEP_CODE.Text = dep_codestr;
                    fatherdept.Enabled = false;                                     //编码自动生成，不允许人工输入
                    sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlConn;
                    if (dep_codestr.Substring(0, 2) == "JC")//修改工种
                    {
                        #region
                        sqlCmd.CommandText = "select * from TBDS_JOBCATINFO where JC_ID='" + dep_codestr + "'";
                        sqlConn.Open();
                        SqlDataReader dr = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (dr.Read())                                                 //获取修改部门的原始信息
                        {

                            fatherdept.Visible = true;
                            fatherdept.SelectedValue = dr["JC_DEPID"].ToString();//显示父部门
                            Radiogrouportw.Visible = true;
                            Radiogrouportw.Enabled = false;
                            Radiogrouportw.SelectedValue = "0";
                            DEP_CODE.Text = dr["JC_ID"].ToString();        //显示其他信息
                            DEP_NAME.Text = dr["JC_NAME"].ToString();
                            DEP_ISYENODE.SelectedValue = "Y";
                            DEP_SFJY.SelectedValue = "0";
                            DEP_CY.SelectedValue = "0";
                            DEP_NOTE.Text = dr["JC_NOTE"].ToString();
                        }
                        dr.Close();
                        #endregion
                    }
                    else //修改部门/班组/岗位
                    {
                        #region
                        sqlCmd.CommandText = "select * from TBDS_DEPINFO where DEP_CODE='" + dep_codestr + "'";
                        sqlConn.Open();
                        SqlDataReader dr = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (dr.Read())                                                 //获取修改部门的原始信息
                        {
                            if (dr["DEP_FATHERID"].ToString() == "0")                  //无上级部门即为一级部门
                            {
                                fatherdept.Visible = false;                            //父部门选择下拉框隐藏，默认值为“0”
                                fatherdeptl.Visible = false;
                                fatherdept.SelectedValue = "0";
                            }
                            else                                                        //有上级部门即为二级部门
                            {
                                fatherdept.Visible = true;
                                fatherdept.SelectedValue = dr["DEP_CODE"].ToString().Substring(0, 2);//dr["DEP_FATHERID"].ToString();//显示父部门
                            }
                            DEP_CODE.Text = dr["DEP_CODE"].ToString();        //显示其他信息
                            DEP_NAME.Text = dr["DEP_NAME"].ToString();
                            DEP_ISYENODE.SelectedValue = dr["DEP_ISYENODE"].ToString();
                            DEP_SFJY.SelectedValue = dr["DEP_SFJY"].ToString();
                            DEP_CY.SelectedValue = dr["DEP_CY"].ToString();
                            DEP_NOTE.Text = dr["DEP_NOTE"].ToString();

                            if (dr["DEP_CODE"].ToString().Substring(0, 2) == "04" && Convert.ToInt32(dr["DEP_CODE"].ToString().Length) >= 3)
                            {
                                tr1.Visible = true;
                                rdbzyn.SelectedValue = dr["DEP_BZYN"].ToString().Trim();
                            }

                        }
                        dr.Close();
                        #endregion

                    }
                    DBCallCommon.closeConn(sqlConn);
                }
                else
                {
                    string dep_codestr = Request.QueryString["DEP_CODE"].ToString();//获取修改的部门编码
                    Session["depcode"] = dep_codestr;                              //存储修改的部门编码
                    DEP_CODE.Text = dep_codestr;
                    fatherdept.Enabled = false;                                     //编码自动生成，不允许人工输入
                    DEP_NAME.Enabled = false;
                    DEP_SFJY.Enabled = false;
                    DEP_CY.Enabled = false;
                    DEP_NOTE.Enabled = false;
                    rdbzyn.Enabled = false;
                    btnupdate.Visible = false;
                    sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                    SqlCommand sqlCmd = new SqlCommand();
                    sqlCmd.Connection = sqlConn;
                    if (dep_codestr.Substring(0, 2) == "JC")//修改工种
                    {
                        #region
                        sqlCmd.CommandText = "select * from TBDS_JOBCATINFO where JC_ID='" + dep_codestr + "'";
                        sqlConn.Open();
                        SqlDataReader dr = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (dr.Read())                                                 //获取修改部门的原始信息
                        {

                            fatherdept.Visible = true;
                            fatherdept.SelectedValue = dr["JC_DEPID"].ToString();//显示父部门
                            Radiogrouportw.Visible = true;
                            Radiogrouportw.Enabled = false;
                            Radiogrouportw.SelectedValue = "0";
                            DEP_CODE.Text = dr["JC_ID"].ToString();        //显示其他信息
                            DEP_NAME.Text = dr["JC_NAME"].ToString();
                            DEP_ISYENODE.SelectedValue = "Y";
                            DEP_SFJY.SelectedValue = "0";
                            DEP_CY.SelectedValue = "0";
                            DEP_NOTE.Text = dr["JC_NOTE"].ToString();
                            DEP_MANCLERK.Text=dr["JC_MANCLERK"].ToString();
                            DEP_FILLDATE.Text=dr["JC_FILLDATE"].ToString();
                        }
                        dr.Close();
                        #endregion
                    }
                    else //修改部门/班组/岗位
                    {
                        #region
                        sqlCmd.CommandText = "select * from TBDS_DEPINFO where DEP_CODE='" + dep_codestr + "'";
                        sqlConn.Open();
                        SqlDataReader dr = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                        if (dr.Read())                                                 //获取修改部门的原始信息
                        {
                            if (dr["DEP_FATHERID"].ToString() == "0")                  //无上级部门即为一级部门
                            {
                                fatherdept.Visible = false;                            //父部门选择下拉框隐藏，默认值为“0”
                                fatherdeptl.Visible = false;
                                fatherdept.SelectedValue = "0";
                            }
                            else                                                        //有上级部门即为二级部门
                            {
                                fatherdept.Visible = true;
                                fatherdept.SelectedValue = dr["DEP_CODE"].ToString().Substring(0, 2);//dr["DEP_FATHERID"].ToString();//显示父部门
                            }
                            DEP_CODE.Text = dr["DEP_CODE"].ToString();        //显示其他信息
                            DEP_NAME.Text = dr["DEP_NAME"].ToString();
                            DEP_ISYENODE.SelectedValue = dr["DEP_ISYENODE"].ToString();
                            DEP_SFJY.SelectedValue = dr["DEP_SFJY"].ToString();
                            DEP_CY.SelectedValue = dr["DEP_CY"].ToString();
                            DEP_NOTE.Text = dr["DEP_NOTE"].ToString();
                            DEP_FILLDATE.Text = dr["DEP_FILLDATE"].ToString();
                            DEP_MANCLERK.Text = dr["DEP_MANCLERK"].ToString();

                            if (dr["DEP_CODE"].ToString().Substring(0, 2) == "04" && Convert.ToInt32(dr["DEP_CODE"].ToString().Length) >= 3)
                            {
                                tr1.Visible = true;
                                rdbzyn.SelectedValue = dr["DEP_BZYN"].ToString().Trim();
                            }

                        }
                        dr.Close();
                        #endregion

                    }
                    DBCallCommon.closeConn(sqlConn);
                }
            }
        }

        private void getdepcode()                 //一级部门编码
        {
            string code;
            string sqlText = "select distinct DEP_CODE from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]' order by DEP_CODE";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            code = depsubencode(dt, 99);
            //int rows = dt.Rows.Count;
            //if (rows == 0)
            //{
            //    code = "01";
            //}
            //else
            //{
            //    int tempcode1 = Convert.ToInt32(dt.Rows[rows - 1][0])+1;

            //    if (tempcode1 < 10)
            //    {
            //        code = "0" + Convert.ToString(tempcode1);
            //    }
            //    else
            //    {
            //        if (tempcode1 < 100)
            //        {
            //            code =Convert.ToString(tempcode1);
            //        }
            //        else 
            //        {
                        
            //        }
            //    }
            //}
            DEP_CODE.Text = code;
        }
        protected void btnupdate_Click(object sender, EventArgs e)                             //修改/删除
        {
            string cmdstring;
            int dr1=-1;
            if (btnupdate.Text == "增加")
            {
                if(Radiogrouportw .SelectedValue =="0")//增加工种
                {
                    #region
                    if (addhasexist(DEP_NAME.Text.ToString().Trim(), "JC_NAME", "TBDS_JOBCATINFO"))        //判断增的部门名称是否存在，不允许添加相同名称的部门
                    {
                        Response.Write("<script>alert('工种名称“" + DEP_NAME.Text.ToString().Trim() + "”已存在，不允许添加相同名称的工种，请重新输入！')</script>");
                        DEP_NAME.Focus();
                        //lblupdate.Visible = true;
                        //lblupdate.Text = "操作失败";
                    }
                    else
                    {
                        cmdstring = "insert into TBDS_JOBCATINFO(JC_ID,JC_NAME,JC_DEPID,JC_MANCLERK,JC_FILLDATE,JC_NOTE) values(@JC_ID,@JC_NAME,@JC_DEPID,@JC_MANCLERK,@JC_FILLDATE,@JC_NOTE)";
                        dr1 =tbds_tyofwinf_update(DEP_CODE.Text.ToString(), DEP_NAME.Text.ToString().Trim(), fatherdept.SelectedValue.ToString(),DEP_MANCLERK.Text.ToString(), DEP_FILLDATE.Text.ToString(), DEP_NOTE.Text.ToString(), cmdstring);

                    }
                    #endregion
                }
                else //增加岗位/班组
                {
                    #region
                    //if (addhasexist(DEP_NAME.Text.ToString().Trim(), "DEP_NAME", "TBDS_DEPINFO"))        //判断增的部门名称是否存在，不允许添加相同名称的部门
                    //{
                    //    Response.Write("<script>alert('部门名称“" + DEP_NAME.Text.ToString().Trim() + "”已存在，不允许添加相同名称的部门，请重新输入！')</script>");
                    //    DEP_NAME.Focus();
                    //    lblupdate.Visible = true;
                    //    lblupdate.Text = "操作失败";
                    //}
                    //else
                    //{
                        //lblupdate.Visible = true;
                        cmdstring = "insert into TBDS_DEPINFO(DEP_CODE,DEP_NAME,DEP_FATHERID,DEP_ISYENODE,DEP_CY,DEP_SFJY,DEP_MANCLERK,DEP_FILLDATE,DEP_NOTE,DEP_BZYN) values( @DEP_CODE,@DEP_NAME,@fatherdept,@DEP_ISYENODE,@DEP_CY,@DEP_SFJY,@DEP_MANCLERK,@DEP_FILLDATE,@DEP_NOTE,@DEP_BZYN)";
                        dr1 = tbds_depinf_update(DEP_CODE.Text.ToString(), DEP_NAME.Text.ToString().Trim(), fatherdept.SelectedValue.ToString(), DEP_ISYENODE.SelectedValue.ToString(),DEP_CY.SelectedValue.ToString(),DEP_SFJY.SelectedValue.ToString(), DEP_MANCLERK.Text.ToString(), DEP_FILLDATE.Text.ToString(), DEP_NOTE.Text.ToString(),rdbzyn.SelectedValue, cmdstring);
                    //}
                    #endregion
                }
            }
            else//修改
            {
                if (Radiogrouportw.SelectedValue == "0")//修改工种
                {
                    #region
                    if (changehasexist(Convert.ToString(Session["depcode"]), "JC_ID", "JC_NAME", DEP_NAME.Text.ToString().Trim(), "TBDS_JOBCATINFO"))        //判断修改的部门名称是否存在，不允许添加相同名称的部门
                    {
                        Response.Write("<script>alert('工种名称“" + DEP_NAME.Text.ToString().Trim() + "”已存在，不允许有相同名称的工种，请重新输入！')</script>");
                        DEP_NAME.Focus();
                        lblupdate.Visible = true;
                        lblupdate.Text = "操作失败";
                    }
                    else
                    {
                        cmdstring = "update TBDS_JOBCATINFO SET JC_ID=@JC_ID,JC_NAME=@JC_NAME,JC_DEPID=@JC_DEPID,JC_MANCLERK=@JC_MANCLERK,JC_FILLDATE=@JC_FILLDATE,JC_NOTE=@JC_NOTE WHERE JC_ID='" + Convert.ToString(Session["depcode"]) + "'";
                        dr1 = tbds_tyofwinf_update(DEP_CODE.Text.ToString(), DEP_NAME.Text.ToString().Trim(), fatherdept.SelectedValue.ToString(),DEP_MANCLERK.Text.ToString(), DEP_FILLDATE.Text.ToString(), DEP_NOTE.Text.ToString(), cmdstring);
                    }
                    #endregion
                }
                else//修改部门/岗位/班组
                {
                    #region
                    if (changehasexist(Convert.ToString(Session["depcode"]), "DEP_CODE", "DEP_NAME", DEP_NAME.Text.ToString().Trim(), "TBDS_DEPINFO"))        //判断修改的部门名称是否存在，不允许添加相同名称的部门
                    {
                        Response.Write("<script>alert('部门名称“" + DEP_NAME.Text.ToString().Trim() + "”已存在，不允许有相同名称的部门，请重新输入！')</script>");
                        DEP_NAME.Focus();
                        lblupdate.Visible = true;
                        lblupdate.Text = "操作失败";
                    }
                    else
                    {
                        cmdstring = "update TBDS_DEPINFO SET DEP_CODE=@DEP_CODE,DEP_NAME=@DEP_NAME,DEP_FATHERID=@fatherdept,DEP_ISYENODE=@DEP_ISYENODE,DEP_CY=@DEP_CY,DEP_SFJY=@DEP_SFJY,DEP_MANCLERK=@DEP_MANCLERK,DEP_FILLDATE=@DEP_FILLDATE,DEP_NOTE=@DEP_NOTE,DEP_BZYN=@DEP_BZYN WHERE DEP_CODE='" + Convert.ToString(Session["depcode"]) + "'";
                        dr1 = tbds_depinf_update(DEP_CODE.Text.ToString(), DEP_NAME.Text.ToString().Trim(), fatherdept.SelectedValue.ToString(), DEP_ISYENODE.SelectedValue.ToString(),DEP_CY.SelectedValue.ToString(),DEP_SFJY.SelectedValue.ToString(), Session["UserName"].ToString(), DEP_FILLDATE.Text.ToString(), DEP_NOTE.Text.ToString(), rdbzyn.SelectedValue, cmdstring);
                        
                    }
                    #endregion
                }
            }
            if (dr1 == 1)
            {
               // Response.Write("<script>javascript:window.close();</script>");
                Response.Redirect("tbds_depinfo_detail.aspx");

            }
            else
            {
                lblupdate.Visible = true;
                lblupdate.Text = "操作失败";
            }
        }

        protected void btnback_Click(object sender, EventArgs e)            //返回到主功能页面
        {
          //  Response.Write("<script>javascript:window.close();</script>");
            Response.Redirect("tbds_depinfo_detail.aspx");
        }
        protected int tbds_depinf_update(string DEP_CODE, string DEP_NAME, string fatherdept, string DEP_ISYENODE,string DEP_CY,string DEP_SFJY, string DEP_MANCLERK, string DEP_FILLDATE, string DEP_NOTE, string DEP_BZYN, string cmdstring) //修改数据库数据
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            sqlCmd.CommandText = cmdstring;
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.AddWithValue("@DEP_CODE", DEP_CODE);
            sqlCmd.Parameters.AddWithValue("@DEP_NAME", ignoreSpaces(DEP_NAME));
            sqlCmd.Parameters.AddWithValue("@fatherdept", fatherdept);
            sqlCmd.Parameters.AddWithValue("@DEP_ISYENODE", DEP_ISYENODE);
            sqlCmd.Parameters.AddWithValue("@DEP_CY",DEP_CY);
            sqlCmd.Parameters.AddWithValue("@DEP_SFJY",DEP_SFJY);
            sqlCmd.Parameters.AddWithValue("@DEP_MANCLERK", DEP_MANCLERK);
            sqlCmd.Parameters.AddWithValue("@DEP_FILLDATE", Convert.ToDateTime(DEP_FILLDATE));
            sqlCmd.Parameters.AddWithValue("@DEP_NOTE", DEP_NOTE);
            sqlCmd.Parameters.AddWithValue("@DEP_BZYN", DEP_BZYN);
            sqlConn.Open();
            sqlCmd.Connection = sqlConn;
            int rowsnum = sqlCmd.ExecuteNonQuery();
            DBCallCommon.closeConn(sqlConn);
            return rowsnum;
        }
        protected int tbds_tyofwinf_update(string JC_ID, string JC_NAME, string JC_DEPID, string JC_MANCLERK, string JC_FILLDATE, string JC_NOTE, string cmdstring) //修改数据库数据
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            sqlCmd.CommandText = cmdstring;
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.AddWithValue("@JC_ID", JC_ID);
            sqlCmd.Parameters.AddWithValue("@JC_NAME", ignoreSpaces(JC_NAME));
            sqlCmd.Parameters.AddWithValue("@JC_DEPID", JC_DEPID);
            sqlCmd.Parameters.AddWithValue("@JC_MANCLERK", JC_MANCLERK);
            sqlCmd.Parameters.AddWithValue("@JC_FILLDATE", Convert.ToDateTime(JC_FILLDATE));
            sqlCmd.Parameters.AddWithValue("@JC_NOTE", JC_NOTE);
            sqlConn.Open();
            sqlCmd.Connection = sqlConn;
            int rowsnum = sqlCmd.ExecuteNonQuery();
            DBCallCommon.closeConn(sqlConn);
            return rowsnum;
        }

        protected void fatherdept_SelectedIndexChanged(object sender, EventArgs e)    //二级部门编码
        {
            Radiogrouportw.Visible = false;
            Radiogrouportw.SelectedValue = "1";
            //Radiogrouportw.Items[0].Selected = true;
            string code="";
            string depvalue = fatherdept.SelectedValue;//一级部门编码
            if (depvalue == "04")                      //选择生产部
            {
                Radiogrouportw.Visible = true;         //显示工种/班组选择项
                //Radiogrouportw.Items[0].Selected = true;//默认增加岗位/班组
                Radiogrouportw.SelectedValue = "1";

                tr1.Visible = true;

            }
            else                                        //选择其他部门
            {
                Radiogrouportw.Visible = false;         //隐藏工种/班组选择项
                //Radiogrouportw.Items[0].Selected = true;//默认增加岗位/班组
                Radiogrouportw.SelectedValue = "1";
                tr1.Visible = false;
            }
            if (depvalue != "0")                      //选择了部门，即选择的不是“0”（text="--请选择上级部门--"）
            {
                string sqlText = "select distinct DEP_CODE from TBDS_DEPINFO where DEP_CODE LIKE'" + depvalue + "[0-9][0-9]' and DEP_SFJY='0' order by DEP_CODE";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                code = depvalue + depsubencode(dt,99);//调用编码函数
                //int rows = dt.Rows.Count;
                //if (rows == 0)
                //{
                //    code = depvalue + "01";
                //}
                //else
                //{
                //    string subcode1 = Convert.ToString(dt.Rows[rows - 1][0]);
                //    int subcode2 = Convert.ToInt32(subcode1.Substring(2, 2)) + 1;

                //    if (subcode2 < 10)
                //    {
                //        code = depvalue + "0" + Convert.ToString(subcode2);
                //    }
                //    else
                //    {
                //        code = depvalue + Convert.ToString(subcode2);
                //    }
                //}
            }
            else
            {
                code = "";
            }
            DEP_CODE.Text = code;
        }
        protected void Radiogrouportw_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sqlText;
            string code;
            if (Radiogrouportw.SelectedValue == "1")//岗位/班组编码，生产部编码“04”
            {
                sqlText = "select distinct DEP_CODE from TBDS_DEPINFO where DEP_CODE LIKE '04[0-9][0-9]' and DEP_SFJY='0' order by DEP_CODE";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                code = "04" + depsubencode(dt, 99);
                DEP_CODE.Text = code;

                tr1.Visible = true;  //是否班组显示
            }
            else                                      //工种编码
            {
                tr1.Visible = false;  //是否班组隐藏

                //sqltext = "select JC_ID AS DEP_CODE,JC_NAME AS DEP_NAME,JC_NOTE AS DEP_NOTE from TBDS_JOBCATINFO WHERE JC_DEPID='04' order by JC_ID";
                sqlText = "select JC_ID from TBDS_JOBCATINFO WHERE JC_DEPID= '04' order by JC_ID";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                code = "JC" + depsubencode(dt, 99);
                DEP_CODE.Text = code;
            }
        }
        private void GetDepartment()            //部门下拉框绑定到部门表
        {
            string sqlText ="select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]' and DEP_SFJY='0' order by DEP_CODE";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            fatherdept.DataSource = dt;
            fatherdept.DataTextField = "DEP_NAME";
            fatherdept.DataValueField = "DEP_CODE";
            fatherdept.DataBind();
            ListItem item = new ListItem();
            item.Text = "--请选择上级部门--";
            item.Value = "0";
            fatherdept.Items.Insert(0, item);
        }
        protected bool addhasexist(string fieldvalue,string fieldname ,string tablename)
        {
            bool existornot=false;
            fieldvalue = ignoreSpaces(fieldvalue);
            string sqlText = "select distinct " + fieldname + " from " + tablename + " where " + fieldname + "='" + fieldvalue + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            int rowsnum = dt.Rows.Count;
            if(rowsnum >0)
            {
                existornot = true;
            }
            return existornot;
        }
        protected bool changehasexist(string codevalue, string codefield, string fieldname, string fieldvalue, string tablename)
        {
            bool existornot = false;
            fieldvalue = ignoreSpaces(fieldvalue);
            codevalue = ignoreSpaces(codevalue);
            string sqlText = "select distinct " + fieldname + " from " + tablename + " where " + fieldname + "='" + fieldvalue + "' and " + codefield + "<>'" + codevalue + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            int rowsnum = dt.Rows.Count;
            if (rowsnum > 0)
            {
                existornot = true;
            }
            return existornot;

        }
        protected string depsubencode(DataTable dt, int maxnum)
        {
            int rows = dt.Rows.Count;
            string depsubcode;
            if (rows == 0)
            {
                depsubcode ="01";
            }
            else
            {
                string subcode1 = Convert.ToString(dt.Rows[rows - 1][0]);
                int subcode2 = Convert.ToInt32(subcode1.Substring((subcode1.Length -2), 2)) + 1;
                if (subcode2 < 10)
                {
                    depsubcode = "0" + Convert.ToString(subcode2);
                }
                else
                {
                    if (subcode2 < 100)
                    {
                    depsubcode =Convert.ToString(subcode2);
                    }
                    else   //如果编码超过二位，先检测编码记录个数与最大编码的值是否相等，相等，则提示可以让管理员删除已不用的记录，不等，则可以进行插入编码。
                    {
                        depsubcode = getsubcode(dt, maxnum);
                    }
                }
            }
            return depsubcode;
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
                    startsubcodenum = Convert.ToInt32(startsubcode.Substring((startsubcode.Length - 2), 2));//取后二位字符
                    endsubcode = Convert.ToString(dt.Rows[endnum][0]);
                    endsubcodenum = Convert.ToInt32(endsubcode.Substring((endsubcode.Length - 2), 2));//取后二位字符
                    midsubcode = Convert.ToString(dt.Rows[midnum][0]);
                    midsubcodenum = Convert.ToInt32(midsubcode.Substring((midsubcode.Length - 2), 2));//取后二位字符
                    if ((midsubcodenum - startsubcodenum) > (midnum - startnum))
                    {
                        startnum = startnum;
                        endnum = midnum;

                    }
                    else
                    {
                        if ((endsubcodenum - midsubcodenum) > endnum - midnum)
                        {
                            startnum = midnum;
                            endnum = endnum;
                        }
                        else
                        {

                        }
                    }
                }
                #endregion
                string subcode1 = Convert.ToString(dt.Rows[startnum][0]);
                int subcodenum1 = Convert.ToInt32(subcode1.Substring((subcode1.Length - 2), 2));//取后三位字符
                int subcode2 = subcodenum1 + 1;
                if (subcode2 < 10)
                {
                    temcode = "0" + Convert.ToString(subcode2);
                }
                else
                {
                        temcode =Convert.ToString(subcode2);
                }
            }
            else                        //人员超过99
            {
                Response.Write("<script>alert('编码已超过预定值（99），请通知管理员删除不必须的信息，然后在进行添加！')</script>");
                temcode = "";
            }
            return temcode;
        }
        protected string ignoreSpaces(string str)//消除空格
        {
            string tem;
            tem = str.Replace(" ", "");
            return tem;
        }

        protected void btnback_Click1(object sender, EventArgs e)
        {
          //  Response.Write("<script>javascript:window.close();</script>");
            Response.Redirect("tbds_depinfo_detail.aspx");
        }
    }
}
