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
    public partial class TM_MS_Old_View : System.Web.UI.Page
    {
        string sqlText;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["action"] != null)
                {
                    ViewState["ViewOrg"] = Request.QueryString["ViewOrg"].ToString();
                    ViewState["Engid"] = Request.QueryString["Engid"].ToString();
                    this.GetStrWhere(Request.QueryString["action"].ToString());

                    UCPagingMS.CurrentPage = 1;
                    this.InitVarMS();
                    this.bindGridMS();
                }
            }

            if (IsPostBack)
            {
                this.InitVarMS();
            }
        }

        protected void GetStrWhere(string lotnum)
        {
            string retStrWhere = " BM_ENGID='" + ViewState["Engid"].ToString() + "' AND BM_ISMANU='Y' AND BM_MSSTATE!='0' AND BM_MSSTATUS!='1'";

            sqlText = "select MS_NEWINDEX FROM View_TM_MKDETAIL WHERE MS_PID='" + lotnum + "' AND dbo.Splitnum(MS_NEWINDEX,'.')=1";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                retStrWhere += "and (";
                string temp="";
                foreach (DataRow dr in dt.Rows)
                {
                    temp += " OR  BM_XUHAO='" + dr["MS_NEWINDEX"].ToString() + "' OR BM_XUHAO LIKE '" + dr["MS_NEWINDEX"].ToString() + ".%'";
                }
                temp = temp.Substring(3);
                retStrWhere =retStrWhere+temp+ ")";
            }
            else
            {
                retStrWhere = "1=2 ";
            }

            ViewState["StrWhere"]= retStrWhere;
        }

        #region  分页
        PagerQueryParam pager_ms = new PagerQueryParam();
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVarMS()
        {
            InitPagerMS();
            UCPagingMS.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);
            UCPagingMS.PageSize = pager_ms.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPagerMS()
        {
            pager_ms.TableName = ViewState["ViewOrg"].ToString();
            pager_ms.PrimaryKey = "BM_ID";
            pager_ms.ShowFields = "BM_MSXUHAO AS MS_MSXUHAO,BM_XUHAO AS MS_NEWINDEX,BM_TUHAO AS MS_TUHAO,BM_ZONGXU AS MS_ZONGXU,BM_CHANAME AS MS_NAME,BM_GUIGE AS MS_GUIGE,BM_MAQUALITY AS MS_CAIZHI,BM_NUMBER AS MS_UNUM,BM_UNITWGHT AS MS_UWGHT,(CASE WHEN BM_KU='库' THEN  CAST(BM_TOTALWGHT AS VARCHAR) ELSE '' END) AS MS_TLWGHT,BM_MASHAPE AS MS_MASHAPE,BM_MASTATE AS MS_MASTATE,BM_STANDARD AS MS_STANDARD,BM_KU AS MS_KU,BM_PROCESS AS MS_PROCESS,BM_NOTE AS MS_NOTE";
            pager_ms.OrderField = "dbo.f_formatstr(BM_XUHAO, '.')";
            pager_ms.StrWhere = ViewState["StrWhere"].ToString();
            pager_ms.OrderType = 0;//升序排列
            pager_ms.PageSize = 200;
        }
        void Pager_PageChangedMS(int pageNumber)
        {
            bindGridMS();
        }
        private void bindGridMS()
        {
            pager_ms.PageIndex = UCPagingMS.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_ms);
            CommonFun.Paging(dt, GridView1, UCPagingMS, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPagingMS.Visible = false;
            }
            else
            {
                UCPagingMS.Visible = true;
                UCPagingMS.InitPageInfo();  //分页控件中要显示的控件
            }
        }
        #endregion
    }
}
