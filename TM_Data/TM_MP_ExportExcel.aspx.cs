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
    public partial class TM_MP_ExportExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["action"] != null)
                {
                    this.InitInfo();
                }
            }
        }
        /// <summary>
        /// 初始化页面(基本信息)
        /// </summary>
        private void InitInfo()
        {
            string tsa_id = "";
            string[] fields;
            string sqlText = "";
            tsa_id = Request.QueryString["action"];
            fields = tsa_id.Split('-');
            sqlText = "select CM_PROJ,TSA_ENGNAME,TSA_ENGSTRTYPE,TSA_PJID ";
            sqlText += "from View_TM_TaskAssign where TSA_ID='" + tsa_id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                tsaid.Text = tsa_id;

                proname.Text = dr[0].ToString();
                eng_type.Value = dr[2].ToString();
            }
            engname.Text = dr[1].ToString();
            dr.Close();
            ddlmptype.SelectedIndex = 0;

            this.BindLotNums();
        }
        /// <summary>
        /// 根据导出类别绑定批号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlmptype_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindLotNums();
        }
        /// <summary>
        /// 导出制作明细表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMpExport_OnClick(object sender, EventArgs e)
        {

            ExportTMDataFromDB.ExportMPData(ddlLotNumList.SelectedValue, tsaid.Text, ddlmptype.SelectedValue);
            this.ClientScript.RegisterStartupScript(GetType(), "js", "setTimeout('self.close()',2000);", true);
        }
        /// <summary>
        /// 绑定批号数据
        /// </summary>
        private void BindLotNums()
        {
            ddlLotNumList.Items.Clear();
            string sql_select_lotnum = "select MP_ID from TBPM_MPFORALLRVW where MP_ENGID='" + tsaid.Text + "' and MP_MASHAPE='" + ddlmptype.SelectedValue + "' and MP_STATE='8'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_select_lotnum);
            ddlLotNumList.DataSource = dt;
            ddlLotNumList.DataTextField = "MP_ID";
            ddlLotNumList.DataValueField = "MP_ID";
            ddlLotNumList.DataBind();
            string AllPh = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AllPh += dt.Rows[i][0] + "','";
            }
            if (AllPh.Length < 1)
            {
                ddlLotNumList.Items.Insert(0, new ListItem("-全部-", "全部"));
                ddlLotNumList.SelectedIndex = 0;
                ddlLotNumList.Attributes.Add("style ", "color:red;");
            }
            else
            {
                ddlLotNumList.Items.Insert(0, new ListItem("-全部-", AllPh.Substring(0, AllPh.Length - 3)));
                ddlLotNumList.SelectedIndex = 0;
                ddlLotNumList.Attributes.Remove("style ");
            }
        }

        public string GetTaskID
        {
            get
            {
                return Request.QueryString["action"];
            }
        }
    }
}
