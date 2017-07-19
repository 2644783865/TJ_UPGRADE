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

namespace ZCZJ_DPF.ESM
{
    public partial class EQU_Persons : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                ReGetBoundData();
            }
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
            pager.TableName = "TBDS_STAFFINFO";
            pager.PrimaryKey = "ST_CODE";
            pager.ShowFields = "ST_CODE*1 as ST_CODE,ST_NAME,ST_GENDER,ST_POSITION";
            pager.OrderField = "";
            pager.StrWhere = "";
            pager.OrderType = 0;
            pager.PageSize = 5;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
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
        private void ReGetBoundData()
        {
            InitPager();
            GetBoundData();
        }
        #endregion
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
