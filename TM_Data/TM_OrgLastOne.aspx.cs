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
    public partial class TM_OrgLastOne : System.Web.UI.Page
    {
        #region
       
     
        string marid;//物料编码
        string zongxu;//总序
        string ch_name;//中文名称
        string en_name;//英文名称
        string guige;//规格
        string cailiaoname;//材料名称
        double cailiaocd;//材料长度
        double cailiaokd;//材料宽度
        double cailiaodzh;//材料单重
        double cailiaozongzhong;//材料总重
        double cailiaozongchang;//材料总长
        double bgzmy;//面域
        double shuliang;//数量
        double dzh;//单重
        double zongzhong;//总重
        string xinzhuang;//形状
        string zhuangtai;//状态
        string process;//工艺流程
        string beizhu;//备注
        string tixian;//体现制作明细
        string ddlKeyComponents;//关键部件
        string ddlFixedSize;//是否定尺
        #endregion

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
            sqlText = "select CM_PROJ,TSA_ENGNAME,TSA_ENGSTRTYPE,TSA_PJID ";
            sqlText += "from View_TM_TaskAssign where TSA_ID='" + tsa_id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                tsaid.Text = tsa_id;

                proname.Text = dr[0].ToString();
                engname.Text = dr[1].ToString();
                eng_type.Value = dr[2].ToString();
            }
            dr.Close();
            this.GetListName();
            this.BindLastFive();
        }
        /// <summary>
        /// 初始化表名
        /// </summary>
        private void GetListName()
        {
            #region
            switch (eng_type.Value)
            {
                case "回转窑":
                    ViewState["viewtable"] = "View_TM_HZY";
                    ViewState["tablename"] = "TBPM_STRINFOHZY";
                    //mptable = "TBPM_MPFORHZY";
                    //mstable = "TBPM_MSOFHZY";
                    break;
                case "球、立磨":
                    ViewState["viewtable"] = "View_TM_QLM";
                    ViewState["tablename"] = "TBPM_STRINFOQLM";
                    //mptable = "TBPM_MPFORHZY";
                    //mstable = "TBPM_MSOFQLM";
                    break;
                case "篦冷机":
                    ViewState["viewtable"] = "View_TM_BLJ";
                    ViewState["tablename"] = "TBPM_STRINFOBLJ";
                    //mptable = "TBPM_MPFORHZY";
                    //mstable = "TBPM_MSOFBLJ";
                    break;
                case "堆取料机":
                    ViewState["viewtable"] = "View_TM_DQLJ";
                    ViewState["tablename"] = "TBPM_STRINFODQLJ";
                    //mptable = "TBPM_MPFORHZY";
                    //mstable = "TBPM_MSOFDQJ";
                    break;
                case "钢结构及非标":
                    ViewState["viewtable"] = "View_TM_GFB";
                    ViewState["tablename"] = "TBPM_STRINFOGFB";
                    //mptable = "TBPM_MPFORHZY";
                    //mstable = "TBPM_MSOFGFB";
                    break;
                case "电气及其他":
                    ViewState["viewtable"] = "View_TM_DQO";
                    ViewState["tablename"] = "TBPM_STRINFODQO";
                    //mptable = "TBPM_MPFORHZY";
                    //mstable = "TBPM_MSOFDQO";
                    break;
                default: break;
            }
            #endregion
        }
        /// <summary>
        /// 绑定最后输入的五条数据
        /// </summary>
        private void BindLastFive()
        {
            string sql_lastfive = "select top 20 *,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER from " + ViewState["viewtable"].ToString() + " where BM_ENGID='" + tsaid.Text + "' order by BM_ID DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_lastfive);
            grv.DataSource = dt;
            grv.DataBind();
            if (dt.Rows.Count > 0)
            {
                NoDataPanel.Visible = false;
            }
            else
            {
                NoDataPanel.Visible = true;
            }
        }
    }
}
