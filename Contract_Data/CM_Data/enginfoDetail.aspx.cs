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

namespace ZCZJ_DPF.CM_Data
{

    public partial class enginfoDetail : System.Web.UI.Page
    {
        static string eng_ID = string.Empty;//全局变量id
        static string ENG_PJID = "";
        static string action = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProjiectBind();
                ProjiectBind1();
                if (Request.QueryString["id"] == null && Request.QueryString["action"] == null)
                    throw new NoNullAllowedException("不允许空值传递！");
                else
                {
                    action = Request.QueryString["action"].ToString();
                    if (action == "update")  //更新项目
                    {
                        eng_ID = Request.QueryString["id"].ToString();
                        ENG_PJID = Request.QueryString["ENG_PJID"].ToString();
                        showcurrentENG_INFO(eng_ID, ENG_PJID);

                        //查看情况下添加任务单
                        hpTask.NavigateUrl = "~/CM_Data/TM_HZY_infoDetail.aspx?action=add&project=" + ENG_PJID + "&eng_ID=" + eng_ID + "  ";
                    }
                }

               
                  
                
            }
            
        }

        public void ProjiectBind()
        {
            string sql = " select PJ_ID FROM TBPM_PJINFO ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            eng_PJID.DataSource = dt;
            eng_PJID.DataTextField = "PJ_ID";
            eng_PJID.DataValueField = "PJ_ID";
            eng_PJID.DataBind();
            eng_PJID.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            eng_PJID.SelectedIndex = 0;
        }
       public void ProjiectBind1()
        {
            string sql = " select PJ_NAME FROM TBPM_PJINFO ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            eng_PJNAME.DataSource = dt;
            eng_PJNAME.DataTextField = "PJ_NAME";
            eng_PJNAME.DataValueField = "PJ_NAME";
            eng_PJNAME.DataBind();
            eng_PJNAME.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            eng_PJNAME.SelectedIndex = 0;
        }


       void showcurrentENG_INFO(string id, string ENG_PJID)
        {
            string cmdStr = "SELECT  ENG_NAME,ENG_PJID,ENG_PJNAME,ENG_FULLNAME,ENG_ID,ENG_STARTDATE,ENG_CONTRACTDATE,ENG_REALFINISHDATE,ENG_MANCLERK,ENG_STATE,ENG_NOTE FROM TBPM_ENGINFO WHERE ENG_ID='" + id + "' and ENG_PJID='" + ENG_PJID + "'  ";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(cmdStr);
            while (dr.Read())
            {
                eng_NAME.Text = dr["ENG_NAME"].ToString();
                eng_PJID.Text = dr["ENG_PJID"].ToString();
                eng_PJNAME.Text = dr["ENG_PJNAME"].ToString();
                eng_FULLNAME.Text = dr["ENG_FULLNAME"].ToString();
                eng_CODE.Text = dr["ENG_ID"].ToString();
                eng_STARTDATE.Value = dr["ENG_STARTDATE"].ToString();
                eng_CONTRACTDATE.Value = dr["ENG_CONTRACTDATE"].ToString();
                eng_REALFINISHDATE.Value = dr["ENG_REALFINISHDATE"].ToString();
                eng_MANCLERK.Text = dr["ENG_MANCLERK"].ToString();
                eng_STATE.Text = dr["ENG_STATE"].ToString();
                eng_NOTE.Text = dr["ENG_NOTE"].ToString();
            }
            dr.Close();
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            //获取参数
            string ENG_Name = eng_NAME.Text;
            string ENG_Pjid = eng_PJID.SelectedItem.Text.ToString();
            string ENG_Pjname = eng_PJNAME.SelectedItem.Text.ToString();
            string ENG_Fullname = eng_FULLNAME.Text;
            string ENG_Code = eng_CODE.Text;
            string ENG_Manclerk = eng_MANCLERK.Text;
            string ENG_State = eng_STATE.Text;
            string ENG_Startdate = eng_STARTDATE.Value;
            string ENG_Contractdate = eng_CONTRACTDATE.Value;
            string ENG_Realfinishdate = eng_REALFINISHDATE.Value;
            string ENG_Note = eng_NOTE.Text;
            //int rowEffected;

            //构建cmd语句
            string cmdText = string.Empty;
            if (action == "update")  //表示更新项目
            {
                cmdText = "UPDATE TBPM_ENGINFO SET " +
                          "ENG_NAME='" + ENG_Name + "', " +
                          "ENG_PJID='" + ENG_Pjid + "', " +
                          "ENG_PJNAME='" + ENG_Pjname + "', " +
                          "ENG_FULLNAME='" + ENG_Fullname + "', " +                        
                          "ENG_ID='" + ENG_Code + "'," +
                          "ENG_MANCLERK='" + ENG_Manclerk + "'," +
                          "ENG_STATE='" + ENG_State + "'," +
                          "ENG_STARTDATE='" + ENG_Startdate + "', " +
                          "ENG_CONTRACTDATE='" + ENG_Contractdate + "', " +
                          "ENG_REALFINISHDATE='" + ENG_Realfinishdate + "', " +
                          "ENG_NOTE='" + ENG_Note + "' " +
                          " WHERE(ENG_ID = '" + eng_ID + "')";
            }
            else if (action == "add") //表示添加项目
            {
                cmdText = "INSERT INTO TBPM_ENGINFO(ENG_NAME,ENG_PJID,ENG_PJNAME,ENG_FULLNAME,ENG_ID,ENG_MANCLERK,ENG_STATE,ENG_STARTDATE,ENG_CONTRACTDATE,ENG_REALFINISHDATE,ENG_NOTE)  VALUES('"
                                                           + ENG_Name + "'," +
                                                        "'" + ENG_Pjid + "'," +
                                                         "'" + ENG_Pjname + "'," +
                                                        "'" + ENG_Fullname + "'," +
                                                        "'" + ENG_Code + "'," +
                                                        "'" + ENG_Manclerk + "'," +
                                                        "'" + ENG_State + "'," +
                                                        "'" + ENG_Startdate + "'," +
                                                        "'" + ENG_Contractdate + "'," +
                                                        "'" + ENG_Realfinishdate + "'," +
                                                        "'" + ENG_Note + "')";
            }

            try
            {
                DBCallCommon.ExeSqlText(cmdText);//执行SQL语句
            }
            catch (Exception sqlEx)
            {
                lbl_Info.Text = cmdText + "<br />抱歉，出现错误,请重试。错误详情：" + sqlEx.Message;
            }
            finally
            {
                lbl_Info.Text = "完成提交，正在返回……";
                Response.Redirect("enginfo.aspx");
            }

        }
    }
}
