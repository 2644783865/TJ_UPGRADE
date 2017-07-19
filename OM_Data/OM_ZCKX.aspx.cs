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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_ZCKX : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.QueryString["FLAG"] != null)
            //    flag = Request.QueryString["FLAG"].ToString();


            if (!IsPostBack)
            {
                //if (flag == "ToStore")
                //{
                //    btnPush.Visible = true;
                //}

                //BindData();
                binddepartment();
                UCPaging1.CurrentPage = 1;
                InitVar();
                GetBoundData();

            }
            InitVar();
            //CheckUser(ControlFinder);
        }



        //绑定基本信息
        //private void BindData()
        //{

        //    string Stid = Session["UserId"].ToString();
        //    System.Data.DataTable dt = DBCallCommon.GetPermeision(21, Stid);
        //    ddl_Depart.DataSource = dt;
        //    ddl_Depart.DataTextField = "DEP_NAME";
        //    ddl_Depart.DataValueField = "DEP_CODE";
        //    ddl_Depart.DataBind();
        //}
        protected void binddepartment()
        {
            string sql = "SELECT DISTINCT DEP_NAME,DEP_CODE  FROM TBDS_DEPINFO WHERE len(DEP_CODE)=2";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            drpdepartment.DataValueField = "DEP_CODE";
            drpdepartment.DataTextField = "DEP_NAME";
            drpdepartment.DataSource = dt;
            drpdepartment.DataBind();

            ListItem item = new ListItem("--请选择--", "0");
            drpdepartment.Items.Insert(0, item);

        }
        protected void drpdepartment_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();

        }
        private string GetWhere()
        {
            string strWhere = "BIANHAO !='' and SPZT='y' and BFZT='0'";
            if (txtName.Text.Trim() != "")
            {
                strWhere += " AND NAME like '%" + txtName.Text.Trim() + "%'";
            }
            if (txtModel.Text.Trim() != "")
            {
                strWhere += " AND MODEL like '%" + txtModel.Text.Trim() + "%'";
            }
            if (txtType.Text.Trim() != "")
            {
                strWhere += " and TYPE like '%" + txtType.Text.Trim() + "%'";
            }
            if (txtPer.Text.Trim() != "")
            {
                strWhere += " and syr like '%" + txtPer.Text.Trim() + "%'";

            }
            //if (ddl_Depart.SelectedValue != "00")
            //{
            //    strWhere += " and SYBUMENID='" + ddl_Depart.SelectedValue + "'";
            //}
            if (drpdepartment.SelectedValue != "0")
            {
                strWhere += " and SYBUMENID='" + drpdepartment.SelectedValue + "'";
            }
            if (txtCode.Text != "")
            {
                strWhere += " and CODE like '%" + txtCode.Text + "%'";
            }
            if (txtAddress.Text != "")
            {
                strWhere += " and PLACE like '%" + txtAddress.Text + "%'";
            }
            if (txtType2.Text.Trim() != "")
            {
                strWhere += " and TYPE2 like '%" + txtType2.Text.Trim() + "%'";
            }
            return strWhere;
        }

        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
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
            pager.TableName = "TBOM_GDZCIN as a left join OM_SP as b on a.INCODE=b.SPFATHERID";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "*";
            pager.OrderField = "";
            pager.StrWhere = GetWhere();
            pager.OrderType = 0;
            pager.PageSize = 15;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);
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
        private void ReGetBoundData()
        {
            InitPager();
            GetBoundData();
        }
        #endregion
    }
}
