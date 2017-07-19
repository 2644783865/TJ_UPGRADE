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

namespace ZCZJ_DPF.Basic_Data
{
    public partial class tbcs_cusupinfo_show : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"].ToString();
            if (!IsPostBack)
            {
                this.GetDataByID(id);
            }
        }
        /// <summary>
        /// 按id读出数据
        /// </summary>
        protected void GetDataByID(string id)
        {
            string sqltext = "select * from TBCS_CUSUPINFO where CS_CODE='" + id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            txtCS_CODE_Show.Text = id;
            txtCS_CODE_Show.Enabled = false;
            string[] txtdate = new string[2];//存放日期，如2011-9-12 00:00:00，获取日期部分
            while (dr.Read())
            {
                txtCS_NAME_Show.Text = dr["CS_NAME"].ToString();
                txtCS_LOCATION_Show.Text = dr["CS_LOCATION"].ToString();
                txtCS_HRCODE_Show.Text = dr["CS_HRCODE"].ToString();
                txtCS_TYPE_Show.Text = dr["CS_TYPE"].ToString();
                txtCS_COREBS_Show.Text = dr["CS_COREBS"].ToString();
                txtCS_ADDRESS_Show.Text = dr["CS_ADDRESS"].ToString();
                txtCS_PHONO_Show.Text = dr["CS_PHONO"].ToString();
                //txtCS_RANK_Show.Text = dr["CS_RANK"].ToString();
                txtCS_ZIP_Show.Text = dr["CS_ZIP"].ToString();
                txtCS_FAX_Show.Text=dr["CS_FAX"].ToString();
                txtCS_NOTE_Show.Text = dr["CS_NOTE"].ToString();
                txtCS_MANCLERK_Show.Text = dr["CS_MANCLERK"].ToString();
                txtCS_CONNAME_Show.Text = dr["CS_CONNAME"].ToString();
                txtCS_FILLDATE_Show.Text = dr["CS_FILLDATE"].ToString();
                txtCS_MAIL_Show.Text = dr["CS_MAIL"].ToString();
                txtCS_BANK.Text = dr["CS_BANK"].ToString();
                txtCS_ACCOUNT.Text = dr["CS_ACCOUNT"].ToString();
                txtCS_TAX.Text = dr["CS_TAX"].ToString();
                //txtCS_MCODE_Show.Text = dr["CS_MCODE"].ToString();
                TB_Scope.Text = dr["CS_Scope"].ToString();
                txtCS_NAME_Show.Enabled = false;
                txtCS_LOCATION_Show.Enabled = false;
                txtCS_HRCODE_Show.Enabled = false;
                txtCS_TYPE_Show.Enabled = false;
                txtCS_COREBS_Show.Enabled = false;
                txtCS_ADDRESS_Show.Enabled = false;
                txtCS_PHONO_Show.Enabled = false;
                //txtCS_RANK_Show.Enabled = false;
                txtCS_ZIP_Show.Enabled = false;
                txtCS_FAX_Show.Enabled = false;
                txtCS_NOTE_Show.Enabled = false;
                txtCS_MANCLERK_Show.Enabled = false;
                txtCS_CONNAME_Show.Enabled = false;
                txtCS_FILLDATE_Show.Enabled = false;
                txtCS_MAIL_Show.Enabled = false;
                txtCS_ACCOUNT.Enabled = false;
                txtCS_TAX.Enabled = false;
                txtCS_BANK.Enabled = false;
                //txtCS_MCODE_Show.Enabled = false;
                TB_Scope.Enabled = false;
            }
            dr.Close();
        }
    }
}
