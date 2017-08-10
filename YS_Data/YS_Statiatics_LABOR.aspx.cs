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
using System.Text;
using System.Data.SqlClient;
using ZCZJ_DPF;
using System.Collections.Generic;

namespace ZCZJ_DPF.YS_Data
{
    public partial class YS_Statiatics_LABOR : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitVar();
                this.GetBoundData();
            }
            InitVar();
        }

        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        private void InitPager()
        {
            pager.TableName = "View_YS_COST_REAL_TF";
            pager.PrimaryKey = "";
            pager.ShowFields = "GS_CUSNAME,GS_CONTR,GS_TSAID,GS_TUHAO,GS_TUMING,GS_MONEY,GS_TYPE,DATE";
            pager.OrderField = "";
            pager.StrWhere = StrWhere();
            pager.OrderType = 0;//按任务名称升序排列
            pager.PageSize = 15;
            string sqltext = "select sum(GS_MONEY) as je from View_YS_COST_REAL_TF where " + StrWhere() + "";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            je.Text = dt.Rows[0]["je"].ToString();
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetBoundData();
        }

        protected void GetBoundData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }

        }

        protected string StrWhere()
        {
            Encrypt_Decrypt ed = new Encrypt_Decrypt();
            string ContractNo = ed.DecryptText(Request.QueryString["ContractNo"].ToString());
            string FatherCode = ed.DecryptText(Request.QueryString["FatherCode"].ToString());
            //班组or厂内
            string PS_BZ = "";
            switch (FatherCode)
            {
                case "TEAM_CONTRACT": PS_BZ = " AND GS_TYPE = '直接人工' or GS_TYPE LIKE '%一%'";
                    LabelName.Text = "直接人工实际费用明细";
                    break;
                case "FAC_CONTRACT": PS_BZ = " AND GS_TYPE != '直接人工' and GS_TYPE NOT LIKE '%一%'";
                    LabelName.Text = "厂内分包实际费用明细";
                    break;
                default: break;
            }

            string ENGID = ContractNo;
            string strwhere = " 1=1 ";
            strwhere += " and GS_TSAID= '" + ENGID + "' and GS_TSAID!=''";
            strwhere += PS_BZ;
            ViewState["strwhere"] = strwhere;
            return strwhere;

        }

        protected void daochu_Click(object sender, EventArgs e)
        {
            string strwhere = ViewState["strwhere"].ToString();
            ExportDataFromYS.Export_LABOR(strwhere);
        }

    }
}
