using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.YS_Data
{
    public partial class YS_IN_Detail_WW : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                getWGInfo(true);
            }
        }

        #region 分页

        protected void getWGInfo(bool isFristPage)
        {

            string condition = GetStrWhere();

            GetTotalAmount(condition);

            string TableName = "View_TBMP_IQRCMPPRICE_RVW1";
            string PrimaryKey = "";
            string ShowFields = "MS_WSID,ptcode,PIC_ENGID,CM_CONTR,CM_PROJ,supplierresnm,marnm,PIC_TUHAO,PIC_ZONGXU,ICL_WXTYPE,MS_PROCESS,PIC_MASHAPE,MS_UWGHT,marzxnum,detamount ";
            string OrderField = "MS_WSID";
            int OrderType = 0;
            string StrWhere = GetStrWhere();
            int PageSize = 15;

            InitVar(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize, isFristPage);

        }

        protected string GetStrWhere()
        {
            Encrypt_Decrypt ed = new Encrypt_Decrypt();
            string ContractNo = ed.DecryptText(Request.QueryString["ContractNo"].ToString());
            string FatherCode = ed.DecryptText(Request.QueryString["FatherCode"].ToString());

            string ENGID = ContractNo;

            string strwhere = " 1=1 ";
            strwhere += " and PIC_ENGID = '" + ENGID + "' and PIC_ENGID!=''";
            strwhere += "and [totalstate] = '4'";
            ViewState["strwhere"] = strwhere;
            return strwhere;
        }

        private void GetTotalAmount(string strWhere)
        {
            string sql = "select isnull(CAST(sum(marzxnum) AS FLOAT),0) as TotalRN,isnull(CAST(sum(detamount) AS FLOAT),0) as TotalAmount from View_TBMP_IQRCMPPRICE_RVW1 where " + strWhere;

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.Read())
            {
                hfdTotalNum.Value = sdr["TotalRN"].ToString();
                hfdTotalAmount.Value = sdr["TotalAmount"].ToString();
            }
            sdr.Close();

        }

        private void InitVar(string tableName, string PrimaryKey, string ShowFields, string OrderField, int OrderType, string StrWhere, int PageSize, bool isFristPage)
        {

            InitPager(tableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);//初始化页面

            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数

            if (isFristPage)
            {
                UCPaging1.CurrentPage = 1;
            }

            //否则即为当前页

            bindData();
        }

        //初始化分页信息
        private void InitPager(string TableName, string PrimaryKey, string ShowFields, string OrderField, int OrderType, string StrWhere, int PageSize)
        {
            pager.TableName = TableName;

            pager.PrimaryKey = PrimaryKey;

            pager.ShowFields = ShowFields;

            pager.OrderField = OrderField;

            pager.OrderType = OrderType;

            pager.StrWhere = StrWhere;

            pager.PageSize = PageSize;
        }

        void Pager_PageChanged(int pageNumber)
        {
            getWGInfo(false);
        }

        protected void bindData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;

            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);

            CommonFun.Paging(dt, RepeaterWG, UCPaging1, NoDataPanel);

            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }

            BindItem();
        }

        #endregion

        protected void RepeaterWG_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("TotalNum")).Text = hfdTotalNum.Value;
                ((Label)e.Item.FindControl("TotalAmount")).Text = hfdTotalAmount.Value;
            }
        }

        private void BindItem()
        {

            for (int i = 0; i < (RepeaterWG.Items.Count - 1); i++)
            {

                Label lbCode = (RepeaterWG.Items[i].FindControl("LabelCode") as Label);

                string NextCode = lbCode.Text;

                if (lbCode.Visible)
                {
                    for (int j = i + 1; j < RepeaterWG.Items.Count; j++)
                    {
                        string Code = (RepeaterWG.Items[j].FindControl("LabelCode") as Label).Text;

                        if (NextCode == Code)
                        {
                            (RepeaterWG.Items[j].FindControl("LabelCode") as Label).Visible = false;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        protected string convertSH(string state)
        {
            switch (state)
            {
                case "1": return "未审核";
                case "2": return "已审核";
                default: return state;
            }
        }

        protected string convertGJ(string state)
        {
            switch (state)
            {
                case "0": return "未勾稽";
                case "1": return "待勾稽";
                case "2": return "已勾稽";
                default: return state;
            }
        }

        protected string convertHX(string state)
        {
            switch (state)
            {
                case "0": return "未核销";
                case "1": return "已核销";
                default: return state;
            }
        }

        protected void btnEXCEL_Click(object sender, EventArgs e)
        {
            string strwhere = ViewState["strwhere"].ToString();
            ExportDataFromYS.Export_OUT_LAB_MAR(strwhere);
        }
    }
}
