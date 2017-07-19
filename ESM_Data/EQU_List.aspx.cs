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
using ZCZJ_DPF;

namespace ZCZJ_DPF.ESM
{
    public partial class EQU_List : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection();
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {    
            deletebt.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
            deletebt.Click += new EventHandler(deletebt_Click);
            InitVar();
            if (!IsPostBack)
            {
                initpageinfo();
                GetBoundData();
            } 
        }
        private string GetWhere()
        {
            string strWhere = string.Empty;
            if (name.Text.ToString().Trim() != "")
            {
                strWhere =  ddlSearch.SelectedValue.ToString() +" like '%" + name.Text.Trim().ToString() + "%'";
            }
            else if (ddlpartment.SelectedValue.ToString() != "")
            {
                strWhere = "UsDe like '" + ddlpartment.SelectedValue.ToString() + "'";
            }
            if (name.Text.ToString().Trim() != "" && ddlpartment.SelectedValue.ToString() != "")
            {
                strWhere = ddlSearch.SelectedValue.ToString() + " like '%" + name.Text.Trim().ToString() + "%'" + "and UsDe like '" + ddlpartment.SelectedValue.ToString() + "'";
            }
            return strWhere;
        }
        private void initpageinfo()
        {
            string sqltext = "select distinct UsDe from ESM_EQU where UsDe<>''";
            DataTable dt_bm = DBCallCommon.GetDTUsingSqlText(sqltext);
            ddlpartment.DataSource = dt_bm;
            ddlpartment.DataTextField = "UsDe";
            ddlpartment.DataValueField = "UsDe";
            ddlpartment.DataBind();
            ddlpartment.Items.Insert(0, new ListItem("全部", ""));
        }
       
        protected string editYg(string Id)
        {
            return "javascript:window.showModalDialog('EQU_operate.aspx?action=update&&Id=" + Id + "','','DialogWidth=800px;DialogHeight=700px')";
        }
        protected string showYg(string Id)
        {
            return "javascript:window.showModalDialog('EQU_operate.aspx?action=show&&Id=" + Id + "','','DialogWidth=800px;DialogHeight=700px')";
        }
        protected void search_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
           
        }
        protected void deletebt_Click(object sender, EventArgs e)
        {
            string strId = "";
            foreach (RepeaterItem e_id in equipmentRepeater.Items)
            {
                CheckBox chk = (CheckBox)e_id.FindControl("checkboxstaff");
                if (chk.Checked)
                {
                    strId += "'" + ((Label)e_id.FindControl("Id")).Text + "'" + ",";
                }
            }
            if (strId.Length > 1)
            {
                strId = strId.Substring(0, strId.Length - 1);
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("delete from ESM_EQU  where  Id in (" + strId + ")", sqlConn);
                DBCallCommon.openConn(sqlConn);
                SqlDataReader dr = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                dr.Close();
            }
            DBCallCommon.closeConn(sqlConn);
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
        }
        #region 分页
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = "ESM_EQU";
            pager.PrimaryKey = "Id";
            pager.ShowFields = "";
            pager.OrderField = "";
            pager.StrWhere = GetWhere();
            pager.OrderType = 0;
            pager.PageSize = 10;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, equipmentRepeater, UCPaging1, NoDataPane);
            if (NoDataPane.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }
        private void ReGetBoundData()
        {
            InitPager();
            GetBoundData();
        }
        #endregion

        protected void ddlpartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
        }

        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
        }
    }
}
