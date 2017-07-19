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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_ShenheREN : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                GetPostData();
                //InitInfo();
                this.InitData(Request.QueryString["PerInChg"]);
            }
        }
        private void InitData(string perincharge)
        {
            switch (perincharge)
            {
                case "PIC":
                    ddlSearch.SelectedValue = "0201";
                    break;
                case "PID":
                    ddlSearch.SelectedValue = "0201";

                    break;
                case "PIM":
                    ddlSearch.SelectedValue = "01";
                    //ddlSearch.Enabled = false;
                    break;
                default: break;
            }
            this.ddlSearch_SelectedIndexChanged(null, null);
        }
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        //初始化分页信息
        private void InitPager()
        {
            pager.TableName = "TBDS_STAFFINFO";
            pager.PrimaryKey = "ST_DEPID";
            pager.ShowFields = "";
            pager.OrderField = "ST_DEPID";
            pager.StrWhere = "0=0";
            pager.OrderType = 0;//按人员编号升序排列
            pager.PageSize = 5;
        }
        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            //绑定数据
            GetTechPersonsData();
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetTechPersonsData();
        }
        protected void GetTechPersonsData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
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

        //点击查询时重新邦定GridView，添加查询条件
        private void ReGetTechPersonsData()
        {
            InitPager();
            pager.StrWhere = CreateConStr();
            GetTechPersonsData();
        }

        private string CreateConStr()
        {
            string strWhere = "";
            strWhere = "ST_POSITION like'" + ddlSearch.SelectedValue.Trim() + "%' and ST_PD='0'";
            return strWhere;
        }
        //绑定岗位
        private void GetPostData()
        {
            string sqlText = "select DEP_CODE,DEP_NAME from TBDS_DEPINFO ";
            sqlText += " where  DEP_CODE LIKE '020%' order by DEP_CODE";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlSearch.DataSource = dt;
            ddlSearch.DataTextField = "DEP_NAME";
            ddlSearch.DataValueField = "DEP_CODE";
            ddlSearch.DataBind();
        }
        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetTechPersonsData();
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string code;
            string labTcName;
            string NameCode = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                CheckBox chk = (CheckBox)gr.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    labTcName = ((Label)gr.FindControl("lblName")).Text.Trim();
                    code = ((Label)gr.FindControl("code")).Text.Trim();
                    NameCode = labTcName + " " + code;
                }
            }
            Response.Write("<script>javascript:window.returnValue ='" + NameCode + "';window.close();</script>");
        }
    }
}
