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
    public partial class CM_Bus_ContractChaKan : System.Web.UI.Page
    {
        static string bp_ID = string.Empty;//全局变量id
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] == null && Request.QueryString["action"] == null)
                    throw new NoNullAllowedException("不允许空值传递！");
                else
                {

                    bp_ID = Request.QueryString["id"].ToString();
                    showcurrentBP_INFO(bp_ID);

                }
            }
        }
        void showcurrentBP_INFO(string id)
        {
            string cmdStr = "SELECT BP_BIDTYPE, BP_PRONAME, BP_DEVICENAME, BP_DVCNORM, BP_NUM, BP_CUSTMID, BP_BSCGCLERK, BP_TCCGCLERK, BP_ACPDATE, BP_YEAR, BP_BIDDATE, BP_BIDSTYLE,BP_ISSUER , BP_STATUS, BP_NOTE FROM TBBS_BIDPRICEINFO WHERE BP_ID=" + id;
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(cmdStr);
            while (dr.Read())
            {
                bp_BIDTYPE.Text = dr["BP_BIDTYPE"].ToString();
                bp_PRONAME.Text = dr["BP_PRONAME"].ToString();
                bp_DEVICENAME.Text = dr["BP_DEVICENAME"].ToString();
                bp_DVCNORM.Text = dr["BP_DVCNORM"].ToString();
                bp_NUM.Text = dr["BP_NUM"].ToString();
                bp_CUSTMID.Text = dr["BP_CUSTMID"].ToString();
                bp_BSCGCLERK.Text = dr["BP_BSCGCLERK"].ToString();
                bp_TCCGCLERK.Text = dr["BP_TCCGCLERK"].ToString();
                bp_ACPDATE.Text = dr["BP_ACPDATE"].ToString();
                bp_YEAR.Text = dr["BP_YEAR"].ToString();
                bp_BIDDATE.Text = dr["BP_BIDDATE"].ToString();
                bp_BIDSTYLE.Text = dr["BP_BIDSTYLE"].ToString();
                bp_ISSUER.Text = dr["BP_ISSUER"].ToString();
                bp_STATUS.Text = dr["BP_STATUS"].ToString();
                bp_NOTE.Text = dr["BP_NOTE"].ToString();
               
            }
            dr.Close();
            int n = Convert.ToInt32(bp_STATUS.Text);
            switch (n)
            {

                case 0:
                    bp_STATUS.Text = "未中标";
                    break;
                case 1:
                    bp_STATUS.Text = "中标";
                    break;
                case 2:
                    bp_STATUS.Text = "项目未定";
                    break;
                case 3:
                    bp_STATUS.Text = "正在跟踪";
                    break;
                case 4:
                    bp_STATUS.Text = "未参加投标";
                    break;
                case 5:
                    bp_STATUS.Text = "正在投标";
                    break;
                case 6:
                    bp_STATUS.Text = "转公司";
                    break;
                case 7:
                    bp_STATUS.Text = "取消报价";
                    break;
            }
        }
    }
}
