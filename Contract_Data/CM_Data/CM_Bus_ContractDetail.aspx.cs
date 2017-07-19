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
    public partial class CM_Bus_ContractDetail : System.Web.UI.Page
    {
        static string bp_ID = string.Empty;//全局变量id
        static string action = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] == null && Request.QueryString["action"] == null)
                    throw new NoNullAllowedException("不允许空值传递！");
                else
                {
                    action = Request.QueryString["action"].ToString();
                    if (action == "update")  //更新项目
                    {
                        bp_ID = Request.QueryString["id"].ToString();
                        showcurrentBP_INFO(bp_ID);
                    }
                }
            }
        }
        void showcurrentBP_INFO(string id)
        {
            string cmdStr = "SELECT BP_BIDTYPE, BP_PRONAME, BP_DEVICENAME, BP_DVCNORM, BP_NUM, BP_CUSTMID, BP_BSCGCLERK, BP_TCCGCLERK, BP_ACPDATE, BP_YEAR, BP_BIDDATE, BP_BIDSTYLE,BP_ISSUER , BP_STATUS, BP_NOTE FROM TBBS_BIDPRICEINFO WHERE BP_ID=" + id;
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(cmdStr);
            while (dr.Read())
            {
                ddl_lx.SelectedItem.Value = dr["BP_BIDTYPE"].ToString();
                bp_PRONAME.Text = dr["BP_PRONAME"].ToString();
                bp_DEVICENAME.Text = dr["BP_DEVICENAME"].ToString();
                bp_DVCNORM.Text = dr["BP_DVCNORM"].ToString();
                bp_NUM.Text = dr["BP_NUM"].ToString();
                bp_CUSTMID.Text = dr["BP_CUSTMID"].ToString();
                buperson.Value = dr["BP_BSCGCLERK"].ToString();
                bp_CUSTMTeL.Text = dr["BP_TCCGCLERK"].ToString();
                bp_ACPDATE.Value = dr["BP_ACPDATE"].ToString();
                bp_YEAR.Value = dr["BP_YEAR"].ToString();
                bp_BIDDATE.Value = dr["BP_BIDDATE"].ToString();
                bp_BIDSTYLE.Text = dr["BP_BIDSTYLE"].ToString();
                bp_ISSUER.Text = dr["BP_ISSUER"].ToString();
                bp_STATUS.SelectedValue = dr["BP_STATUS"].ToString();
                bp_NOTE.Text = dr["BP_NOTE"].ToString();
            }
            dr.Close();
        }
         protected void btn_Submit_Click(object sender, EventArgs e)
        {
            //获取参数
            string BP_BidType = ddl_lx.SelectedItem.Value;
            string BP_ProName = bp_PRONAME.Text;
            string BP_DeviceEName= bp_DEVICENAME.Text;
            string BP_DvcNorm=  bp_DVCNORM.Text;
            string BP_Num= bp_NUM.Text;
            string BP_CustmId = bp_CUSTMID.Text;
            string BP_BscgClerk = buperson.Value;
            string BP_TccgClerk = bp_CUSTMTeL.Text;
            string BP_AcpDate = bp_ACPDATE.Value;
            string BP_Year = bp_YEAR.Value;
            string BP_BidDate =  bp_BIDDATE.Value;
            string BP_BidStyleE = bp_BIDSTYLE.Text;
            string BP_Issure = bp_ISSUER.Text;
            string BP_Status = bp_STATUS.Text;
            string BP_Note = bp_NOTE.Text;
            //构建cmd语句
            string cmdText = string.Empty;
            if (action == "update")  //表示更新项目
            {
                cmdText = "UPDATE TBBS_BIDPRICEINFO SET " +
                          "BP_BIDTYPE='" + BP_BidType + "', " +
                          "BP_PRONAME='" + BP_ProName + "', " +
                          "BP_DEVICENAME='" + BP_DeviceEName + "', " +
                          "BP_DVCNORM='" + BP_DvcNorm + "'," +
                          "BP_NUM='" + BP_Num + "', " +
                          "BP_CUSTMID='" + BP_CustmId + "', " +
                          "BP_BSCGCLERK='" + BP_BscgClerk + "', " +
                          "BP_TCCGCLERK='" + BP_TccgClerk + "', " +
                          "BP_ACPDATE='" + BP_AcpDate + "', " +
                          "BP_YEAR='" + BP_Year + "', " +
                          "BP_BIDDATE='" + BP_BidDate + "', " +
                          "BP_BIDSTYLE='" + BP_BidStyleE + "', " +
                          "BP_ISSUER='" + BP_Issure + "', " +
                          "BP_STATUS='" + BP_Status + "', " +
                          "BP_NOTE='" + BP_Note + "' " +
                          " WHERE(BP_ID = '" + bp_ID + "')";
            }
            else if (action == "add") //表示添加项目
            {
                cmdText = "INSERT INTO TBBS_BIDPRICEINFO(BP_BIDTYPE, BP_PRONAME, BP_DEVICENAME, BP_DVCNORM, BP_NUM, BP_CUSTMID, BP_BSCGCLERK, BP_TCCGCLERK, BP_ACPDATE, BP_YEAR, BP_BIDDATE, BP_BIDSTYLE,BP_ISSUER , BP_STATUS, BP_NOTE)  VALUES('"
                                                           + BP_BidType + "'," +
                                                        "'" + BP_ProName + "'," +
                                                        "'" + BP_DeviceEName + "'," +
                                                        "'" + BP_DvcNorm + "'," +
                                                        "'" + BP_Num + "'," +
                                                        "'" + BP_CustmId + "'," +
                                                        "'" + BP_BscgClerk + "'," +
                                                        "'" + BP_TccgClerk + "'," +
                                                        "'" + BP_AcpDate + "'," +
                                                        "'" + BP_Year + "'," +
                                                        "'" + BP_BidDate + "'," +
                                                        "'" + BP_BidStyleE + "'," +
                                                        "'" + BP_Issure + "'," +
                                                         "'" + BP_Status + "'," +
                                                        "'" + BP_Note + "')";
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
                Response.Redirect("CM_Bus_Contract.aspx");
            }

        }
    }
}
