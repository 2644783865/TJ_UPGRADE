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
    public partial class TM_OUT_ExportExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitInfo();
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
            sqlText = "select TSA_PJNAME,TSA_ENGNAME,TSA_ENGSTRTYPE,TSA_PJID ";
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
            rblOutType.SelectedIndex = 0;
            ddlouttype.SelectedIndex = 0;
            this.BindLotNums();
        }
        /// <summary>
        /// 查询条件改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlouttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindLotNums();
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOutExport_OnClick(object sender, EventArgs e)
        {
            if (ddlLotNumList.SelectedIndex != 0)
            {
              //  ExportTMDataFromDB.ExportOutData(ddlLotNumList.SelectedValue, tsaid.Text);
                this.ClientScript.RegisterStartupScript(GetType(), "js", "setTimeout('self.close()',2000);", true);
            }
            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('请选择批号！！！');self.close();", true);
            }
        }
        /// <summary>
        /// 绑定批号数据
        /// </summary>
        private void BindLotNums()
        {
            ddlLotNumList.Items.Clear();

            string sql_select_lotnum = "";
            if (ddlouttype.SelectedIndex == 0)
            {
                sql_select_lotnum = "select OST_OUTSOURCENO from  " + rblOutType.SelectedValue + " where OST_ENGID='" + tsaid.Text + "'";
            }
            else
            {
                sql_select_lotnum = "select OST_OUTSOURCENO from  " + rblOutType.SelectedValue + " where OST_ENGID='" + tsaid.Text + "' and OST_OUTTYPE='" + ddlouttype.SelectedValue + "'";
            }
            string dataText = "OST_OUTSOURCENO";
            string dataValue = "OST_OUTSOURCENO";
            DBCallCommon.BindDdl(ddlLotNumList, sql_select_lotnum, dataText, dataValue);
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
