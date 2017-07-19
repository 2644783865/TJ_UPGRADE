using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ZCZJ_DPF.CM_Data
{
    public partial class pjinfoDetail_new : System.Web.UI.Page
    {
        static string pj_ID = string.Empty;//全局变量id
        static string action = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] == null && Request.QueryString["action"]==null)
                    throw new NoNullAllowedException("不允许空值传递！");
                else
                {
                    action=Request.QueryString["action"].ToString();
                    if (action == "update")  //更新项目
                    {
                        pj_ID = Request.QueryString["id"].ToString();
                        showcurrentPJ_INFO(pj_ID);
                    }
                }
            }
        }

        void showcurrentPJ_INFO(string id)
        {
            string cmdStr = " SELECT PJ_ID,PJ_NAME, PJ_FILLDATE, PJ_STARTDATE, PJ_CONTRACTDATE, PJ_REALFINISHDATE, PJ_MANCLERK, PJ_STA, PJ_NOTE FROM TBPM_PJINFO WHERE PJ_ID='"+id+"'  ";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(cmdStr);
            while (dr.Read())
            {
                tb_PJ_NAME.Text = dr[1].ToString();
                tb_PJ_CODE.Text = dr[0].ToString();
                cal_PJ_STARTDATE.Value = dr["PJ_STARTDATE"].ToString();
                cal_PJ_FILLDATE.Value = dr["PJ_FILLDATE"].ToString();
                cal_PJ_CONTRACTDATE.Value = dr["PJ_CONTRACTDATE"].ToString();
                cal_PJ_REALFINISHDATE.Value =  dr["PJ_REALFINISHDATE"].ToString();
                tb_PJ_MANCLERK.Text = dr["PJ_MANCLERK"].ToString();
                ddl_PJ_STATE.SelectedValue = dr["PJ_STA"].ToString();
                ta_PJ_NOTE.Text = dr["PJ_NOTE"].ToString();
            }
            dr.Close();
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            //获取参数
            string PJ_Name = tb_PJ_NAME.Text;
            string PJ_Code = tb_PJ_CODE.Text;
            string PJ_StartDate = cal_PJ_STARTDATE.Value;
            string PJ_FillDate = cal_PJ_FILLDATE.Value;
            string PJ_ContractDate = cal_PJ_CONTRACTDATE.Value;
            string PJ_RealFinishDate = cal_PJ_REALFINISHDATE.Value;
            string PJ_ManClerk = tb_PJ_MANCLERK.Text;
            string PJ_Sta = ddl_PJ_STATE.Text;
            string PJ_Note = ta_PJ_NOTE.Text;
            //int rowEffected;

            //构建cmd语句
            string cmdText = string.Empty;
            if (action == "update")  //表示更新项目
            {
                cmdText = "UPDATE TBPM_PJINFO SET " +
                          "PJ_NAME='" + PJ_Name + "', " +
                          "PJ_ID='" + PJ_Code + "', " +
                          "PJ_STARTDATE='" + PJ_StartDate + "', " +
                          "PJ_FILLDATE='" + PJ_FillDate + "'," +
                          "PJ_CONTRACTDATE='" + PJ_ContractDate + "', " +
                          "PJ_REALFINISHDATE='" + PJ_RealFinishDate + "', " +
                          "PJ_MANCLERK='" + PJ_ManClerk + "', " +
                          "PJ_STA='" + PJ_Sta + "', " +
                          "PJ_NOTE='" + PJ_Note + "' " +
                          " WHERE(PJ_ID = '" + pj_ID + "')";
            }
            else if (action == "add") //表示添加项目
            {
                cmdText = "INSERT INTO TBPM_PJINFO(PJ_NAME,PJ_ID,PJ_FILLDATE,PJ_STARTDATE,PJ_CONTRACTDATE,PJ_REALFINISHDATE,PJ_MANCLERK,PJ_STA,PJ_NOTE)  VALUES('"
                                                           +PJ_Name + "'," +
                                                        "'"+PJ_Code+"',"+
                                                        "'"+PJ_StartDate+"',"+
                                                        "'"+PJ_FillDate+"',"+
                                                        "'"+PJ_ContractDate+"',"+
                                                        "'"+PJ_RealFinishDate+"',"+
                                                        "'"+PJ_ManClerk+"',"+
                                                        "'"+PJ_Sta+"',"+
                                                        "'"+PJ_Note+"')";
            }

            try
            {
                //lbl_Info.Text = cmdText;
                DBCallCommon.ExeSqlText(cmdText);
            }
            catch (Exception sqlEx)
            {
                lbl_Info.Text = cmdText + "<br />抱歉，出现错误,请重试。错误详情：" + sqlEx.Message;
            }
            finally
            {
                lbl_Info.Text = "完成提交，正在返回……";
                Response.Redirect("pjinfo.aspx");
            }

        }
    }
}
