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
    public partial class TM_MS_ExprotExcel : System.Web.UI.Page
    {
        string mstable;
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
            sqlText = "select CM_PROJ as TSA_PJNAME,TSA_ENGNAME,TSA_ENGSTRTYPE,TSA_PJID ";
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
            this.BindLotNums();
        }
 
        /// <summary>
        /// 导出制作明细表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMsExport_OnClick(object sender, EventArgs e)
        {
       
           
                if (ddlLotNumList.SelectedIndex != 0)
                {
                    ExportTMDataFromDB.ExportMSData(ddlLotNumList.SelectedValue,tsaid.Text);
                }
                else 
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('按批号导出,请选择批号！！！');self.close();", true);
                }
            
        }


        /// <summary>
        /// 导出制作明细表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMsAllExport_OnClick(object sender, EventArgs e)
        {

            ExportTMDataFromDB.ExportMSAllData(tsaid.Text);
    
        }


        /// <summary>
        /// 绑定批号数据
        /// </summary>
        private void BindLotNums()
        {
            string sql_select_lotnum = "select MS_ID from  TBPM_MSFORALLRVW where MS_ENGID='" + tsaid.Text + "' and MS_STATE='8' union select MS_PID from TBPM_MSCHANGE where MS_ENGID='" + tsaid.Text + "' and MS_STATE='8'";
            string dataText = "MS_ID";
            string dataValue = "MS_ID";
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
