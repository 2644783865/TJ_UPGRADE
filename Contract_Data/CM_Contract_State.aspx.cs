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

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_Contract_State : System.Web.UI.Page
    {
        string sqltext = "";
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InitVar();
            this.Form.DefaultButton = btnQuery.UniqueID;//默认按钮
            if (!IsPostBack)
            {
                this.InitVar();
                this.bindGrid();
            }
        }

        #region "数据查询，分页"
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager.TableName = "TBPM_CONPCHSINFO";
            pager.PrimaryKey = "PCON_BCODE";
            pager.ShowFields = "PCON_BCODE,PCON_NAME,PCON_PJNAME,PCON_FORM,PCON_STATE";
            pager.OrderField = "PCON_BCODE";
            pager.StrWhere = sqltext;
            pager.OrderType = 0;//按时间升序序排列
            pager.PageSize = 10;
            //pager.PageIndex = 1;
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, grvHT, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
        }
        /// <summary>
        /// 获取查询条件
        /// </summary>
        private void GetSqlText()
        {
            sqltext = " PCON_BCODE like '%" + txtHTBH.Text.Trim() + "%'";
        }

        #endregion
        //查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.GetSqlText();
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }
        //全部
        protected void btnAll_Click(object sender, EventArgs e)
        {
            txtHTBH.Text = "";
            this.GetSqlText();
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }
        //更新
        protected void grvHT_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string pcon_bcode=grvHT.Rows[e.RowIndex].Cells[1].Text;
            string state=((RadioButtonList)grvHT.Rows[e.RowIndex].Cells[3].FindControl("rblState")).SelectedValue.ToString();
            string sqltext = "update TBPM_CONPCHSINFO set PCON_STATE=" + state + "" +
                " where PCON_BCODE='" + pcon_bcode + "'";
            DBCallCommon.ExeSqlText(sqltext);
            grvHT.EditIndex = -1;
            this.GetSqlText();
            this.InitVar();
            this.bindGrid();
            this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('修改成功！')", true);
        }
        //Grv数据行绑定
        protected void grvHT_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int state =Convert.ToInt16(((HiddenField)e.Row.Cells[3].FindControl("hdfState")).Value.ToString());
                ((RadioButtonList)e.Row.Cells[3].FindControl("rblState")).SelectedIndex =state;
                if (state != 0)
                {
                    ((RadioButtonList)e.Row.Cells[3].FindControl("rblState")).Items[0].Enabled = false;
                }
            }
        }
        //取消
        protected void grvHT_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvHT.EditIndex = -1;
            this.GetSqlText();
            this.InitVar();
            this.bindGrid();
        }
        //编辑
        protected void grvHT_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvHT.EditIndex = e.NewEditIndex;
            this.GetSqlText();
            this.InitVar();
            this.bindGrid();
        }
    }
}
