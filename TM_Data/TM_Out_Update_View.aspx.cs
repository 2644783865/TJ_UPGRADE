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
    public partial class TM_Out_Update_View : System.Web.UI.Page
    {
        string sqlText;
        string action;
        string[] fields;
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];
            if (!IsPostBack)
            {
                InitInfo();
            }
        }

        //初始化参数
        private void InitPanel()
        {
            if (GridView1.Rows.Count == 0)
            {
                Panel2.Visible = true;
            }
            else
            {
                Panel2.Visible = false;
            }
        }

        //初始化页面
        private void InitInfo()
        {
            tsa_id.Text = action;
            sqlText = "select TSA_PJID,TSA_PJNAME,TSA_ENGNAME from TBPM_TCTSASSGN ";
            sqlText += "where TSA_ID='" + action + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                pro_id.Value = dr[0].ToString();
                proname.Text = dr[1].ToString();
                engname.Text = dr[2].ToString();
            }
            dr.Close();
            sqlText = "select OSL_OUTSOURCENO,OSL_ZONGXU,OSL_NAME,OSL_BIAOSHINO,OSL_GUIGE,";
            sqlText += "OSL_CAIZHI,OSL_UNITWGHT,OSL_NUMBER,OSL_TOTALWGHTL,OSL_WDEPNAME,";
            sqlText += "OSL_REQUEST,OSL_REQDATE,OSL_DELSITE FROM TBPM_OUTSOURCELIST ";
            sqlText += "where OSL_ENGID='" + action + "' and OSL_STATUS='0' union ";
            sqlText += "select OSL_OUTSOURCENO,OSL_ZONGXU,OSL_NAME,OSL_BIAOSHINO,OSL_GUIGE,";
            sqlText += "OSL_CAIZHI,OSL_UNITWGHT,OSL_NUMBER,OSL_TOTALWGHTL,OSL_WDEPNAME,";
            sqlText += "OSL_REQUEST,OSL_REQDATE,OSL_DELSITE FROM TBPM_OUTSCHANGE ";
            sqlText += "where OSL_ENGID='" + action + "' order by OSL_OUTSOURCENO ";
            DBCallCommon.BindGridView(GridView1, sqlText);
            InitPanel();
        }
    }
}
