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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Notice_Detail : System.Web.UI.Page
    {
        string sqlText = "";
        string id="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
            }
        }
        //初始化页面信息
        private void InitInfo()
        {
            id=Request.QueryString["id"];
            sqlText = "select a.DCS_ID,a.DCS_PROJECTID,a.DCS_PROJECT,a.DCS_DATE,";
            sqlText+="b.DCS_HDDEPID,b.DCS_HDDEPNAME,b.DCS_TYPE,b.DCS_STAFFID,";
            sqlText+="b.DCS_STAFFNAME,b.DCS_TIME,b.DCS_STATE,b.DCS_MENO "; 
            sqlText+="from TBPM_DEPCONSHEET as a,TBPM_DEPCONSTHNDOUT as b ";
            sqlText+="where a.DCS_ID=b.DCS_CSID and b.DCS_ID='"+id+"'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            while (dr.Read())
            {
                editid.Text = dr[0].ToString();
                proid.Text = dr[1].ToString();
                proname.Text = dr[2].ToString();
                sbtime.Text = dr[3].ToString();
                sdpid.Text = dr[4].ToString();
                sdpname.Text = dr[5].ToString();
                type.Text = dr[6].ToString();
                hdid.Text = dr[7].ToString();
                hdperson.Text = dr[8].ToString();
                hdtime.Text = dr[9].ToString();
                rblstatus.SelectedValue = dr[10].ToString();
                remark.Text = dr[11].ToString();
            }
            dr.Close();
            if (hdtime.Text != "")
            {
                hdtime.Enabled = false;
            }
            if (rblstatus.SelectedValue == "1")
            {
                rblstatus.Enabled = false;
            }
            if (remark.Text != "")
            {
                remark.Enabled = false;
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {

        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }
    }
}
