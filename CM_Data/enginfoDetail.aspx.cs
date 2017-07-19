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
using System.Text;

namespace ZCZJ_DPF.Basic_Data
{

    public partial class enginfoDetail : System.Web.UI.Page
    {
        static string eng_ID = string.Empty;//全局变量id
        //static string ENG_PJID = "";
        static string action = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = "工程基本信息管理";
                //ProjiectBind();
                //ProjiectBind1();
                if (Request.QueryString["id"] == null && Request.QueryString["action"] == null)
                    throw new NoNullAllowedException("不允许空值传递！");
                else
                {
                    action = Request.QueryString["action"].ToString();
                    if (action == "update")  //更新项目
                    {
                        eng_ID = Request.QueryString["id"].ToString();
                        ddlEnginType.Enabled = false;
                        eng_CODE.Enabled = false;
                        ddlEnginsmType.Enabled = false;
                        tbengType.Enabled = false;
                        RadioButton1.Visible = false;
                        RadioButton2.Visible = false;
                        showcurrentENG_INFO(eng_ID);

                        ////查看情况下添加任务单
                        //hpTask.NavigateUrl = "~/CM_Data/TM_HZY_infoDetail.aspx?action=add&project=" + ENG_PJID + "&eng_ID=" + eng_ID + "  ";
                    }
                }

            }
            
        }
              


       void showcurrentENG_INFO(string id)
        {
            string cmdStr = "SELECT  ENG_ID,ENG_STRTYPE,ENG_NAME,ENG_MANCLERK,ENG_MANDATE,ENG_NOTE FROM TBPM_ENGINFO WHERE ENG_ID='" + id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(cmdStr);
            while (dr.Read())
            {
                eng_NAME.Text = dr["ENG_NAME"].ToString();
                ddlEnginsmType.Items.Add(new ListItem(dr["ENG_STRTYPE"].ToString(),dr["ENG_STRTYPE"].ToString()));

                ddlEnginsmType.SelectedValue = dr["ENG_STRTYPE"].ToString();
                
                eng_CODE.Text = dr["ENG_ID"].ToString();
               
                eng_NOTE.Text = dr["ENG_NOTE"].ToString();
            }
            dr.Close();
            string sql_GCDL = "select ET_FTYPE from TBPM_ENGTYPE WHERE ET_STYPE='"+ddlEnginsmType.SelectedValue.ToString()+"'";
            SqlDataReader dr_DL = DBCallCommon.GetDRUsingSqlText(sql_GCDL);
            if (dr_DL.Read())
            {
                foreach (ListItem li in ddlEnginType.Items)
                {
                    if (li.Value == dr_DL["ET_FTYPE"].ToString())
                    {
                        li.Selected = true;
                        break;
                    }
                }
                dr_DL.Close();
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            //获取参数
            string ENG_Name = eng_NAME.Text;
            string ENG_Id = eng_CODE.Text;
            string ENG_Manclerk = Session["UserID"].ToString();
            string ENG_Mandate = System.DateTime.Now.ToString();
            string ENG_Note = eng_NOTE.Text;
            

            //构建cmd语句
            string cmdText = string.Empty;
            if (action == "update")  //表示更新项目
            {  
                cmdText = "UPDATE TBPM_ENGINFO SET " +
                                         "ENG_NAME='" + ENG_Name + "', " +                 
                                         //"ENG_ID='" + ENG_Id + "'," +
                                         "ENG_MANCLERK='" + ENG_Manclerk + "'," +
                                         "ENG_MANDATE='" + ENG_Mandate + "'," +
                                         "ENG_NOTE='" + ENG_Note + "' " +
                                         " WHERE(ENG_ID = '" + eng_ID + "')";
                DBCallCommon.ExeSqlText(cmdText);
                Response.Write("<script>javascript:window.close();</script>");        
            }
            else if (action == "add") //表示添加项目
            {
                if (ddlEnginType.SelectedItem.Text == "-请选择-")
                {
                    Response.Write("<script>javascript:alert('请选择工程大类');</script>");
                }
                else
                {
                    if (RadioButton1.Checked)
                    {
                        if (tbengType.Text != "")
                        {
                            string sqlte = "select ET_STYPE from TBPM_ENGTYPE where ET_STYPE='" + tbengType.Text + "' and ET_FTYPE='" + ddlEnginType.SelectedItem.Text + "'";
                            SqlDataReader dr3 = DBCallCommon.GetDRUsingSqlText(sqlte);
                            if (!dr3.Read())
                            {
                                string sql = "select ENG_ID from TBPM_ENGINFO where ENG_ID='" + ENG_Id + "'";
                                SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sql);
                                if (!dr1.Read())
                                {
                                    cmdText = "INSERT INTO TBPM_ENGINFO(ENG_NAME,ENG_ID,ENG_STRTYPE,ENG_MANCLERK,ENG_MANDATE,ENG_NOTE)  VALUES('"
                                                                                              + ENG_Name + "'," +
                                                                                              "'" + ENG_Id + "'," +
                                                                                               "'" + tbengType.Text.Trim() + "'," +
                                                                                           "'" + ENG_Manclerk + "'," +
                                                                                           "'" + ENG_Mandate + "'," +
                                                                                           "'" + ENG_Note + "')";
                                    DBCallCommon.ExeSqlText(cmdText);
                                    cmdText = "insert into TBPM_ENGTYPE(ET_STYPE,ET_FTYPE) values ('" + tbengType.Text.Trim() + "','" + ddlEnginType.SelectedItem.Text.ToString() + "')";
                                    DBCallCommon.ExeSqlText(cmdText);
                                    Response.Write("<script>javascript:window.close();</script>");
                                }
                                else
                                {
                                    Response.Write("<script>javascript:alert('工程编号('+'" + ENG_Id + "'+')已经存在，请做修改！');</script>");
                                }
                                dr1.Close();
                            }
                            else
                            {
                                Response.Write("<script>javascript:alert('工程小类('+'" + tbengType.Text + "'+')已经存在，请做修改！');</script>");
                            }
                            dr3.Close();
                        }
                        else
                        {
                            Response.Write("<script>javascript:alert('请输入工程小类');</script>");
                        }
                       
                    }
                    else if (RadioButton2.Checked)
                    {
                        if (ddlEnginsmType.SelectedValue == "-请选择-")
                        {
                            Response.Write("<script>javascript:alert('请选择工程小类');</script>");
                        }
                        else
                        {
                            string sql = "select ENG_ID from TBPM_ENGINFO where ENG_ID='" + ENG_Id + "'";
                            SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sql);
                            if (!dr1.Read())
                            {
                                cmdText = "INSERT INTO TBPM_ENGINFO(ENG_NAME,ENG_ID,ENG_STRTYPE,ENG_MANCLERK,ENG_MANDATE,ENG_NOTE)  VALUES('"
                                                                                          + ENG_Name + "'," +
                                                                                          "'" + ENG_Id + "'," +
                                                                                           "'" + ddlEnginsmType.SelectedValue + "'," +
                                                                                       "'" + ENG_Manclerk + "'," +
                                                                                       "'" + ENG_Mandate + "'," +
                                                                                       "'" + ENG_Note + "')";
                                DBCallCommon.ExeSqlText(cmdText);
                                Response.Write("<script>javascript:window.close();</script>");  
                            }
                            else
                            {
                                Response.Write("<script>javascript:alert('工程编号('+'" + ENG_Id + "'+')已经存在，请做修改！');</script>");
                            }
                            dr1.Close();
                        }
                    }
                     
                }
            }
        }

        protected void ddlEnginType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButton2.Checked)
            {
                string sqlText = "select ET_STYPE,ET_FTYPE from TBPM_ENGTYPE WHERE ET_FTYPE='" + ddlEnginType.SelectedValue.ToString() + "'";
                SqlDataReader dr3 = DBCallCommon.GetDRUsingSqlText(sqlText);
                if (dr3.Read() && dr3["ET_STYPE"].ToString() != "" && dr3["ET_STYPE"].ToString() != null)
                {
                    string dataText = "ET_STYPE";
                    string dataValue = "ET_STYPE";
                    DBCallCommon.BindDdl(ddlEnginsmType, sqlText, dataText, dataValue);
                    ddlEnginsmType.SelectedIndex = 0;
                }
                else
                {
                    Response.Write("<script>javascript:alert('工程大类('+'" + ddlEnginType.SelectedValue.ToString() + "'+')下没有工程小类，请选择添加小类！');</script>");
                }
                dr3.Close();
            }
  
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ddlEnginsmType.Visible = false;
            tbengType.Visible = true;
        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ddlEnginsmType.Visible = true;
            tbengType.Visible = false;
        }
    }
}
