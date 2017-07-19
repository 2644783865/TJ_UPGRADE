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
using System.Collections.Generic;
using System.Text;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_PS_Search : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitPage();
            }
            InitVar();
            //////////CheckUser(ControlFinder);

        }
        /// <summary>
        /// 初始化页面信息
        /// </summary>
        protected void InitPage()
        {
            this.BindPjName();
            this.BindEngName();
            this.BindLotNum();

            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.GetBoundData();
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
            pager.TableName = "View_TM_PAINTSCHEMELIST";
            pager.PrimaryKey = "PS_ID";
            pager.ShowFields = "PS_ID*1 as PS_ID,PS_PID,PS_NAME,PS_LEVEL,PS_PRIMER,PS_PRIMERH,PS_MIDPRIMER,PS_MIDPRIMERH,PS_TOPCOAT,PS_TOPCOATH,PS_COLOR,PS_COLORLABEL,PS_PAINTTOTAL,PS_STATE";
            pager.OrderField = "PS_ID,PS_PID";
            pager.StrWhere = GetSiftData();
            pager.OrderType = 0;//按任务名称升序排列
            pager.PageSize = 100;
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

        /// <summary>
        /// 获取查询条件
        /// </summary>
        protected string  GetSiftData()
        {
            string strWhere = " PS_STATE='8'";
            if (ddlProName.SelectedIndex != 0)
            {
                strWhere += " and [PS_PJID]='"+ddlProName.SelectedValue+"'";
            }

            if (ddlEngName.SelectedIndex != 0)
            {
                strWhere += " and [PS_ENGID]='"+ddlEngName.SelectedValue+"'";
            }

            if (ddlLotNum.SelectedIndex != 0)
            {
                strWhere += " and [PS_PID]='"+ddlLotNum.SelectedValue+"'";
            }
            return strWhere;
        }
        /// <summary>
        /// 绑定项目名称
        /// </summary>
        protected void BindPjName()
        {
            string sqltext = "select distinct PS_PJID+'||'+PS_PJNAME as PS_PJNAME,PS_PJID from VIEW_TM_PAINTSCHEME where PS_STATE='8'";
            string dataText = "PS_PJNAME";
            string dataValue = "PS_PJID";
            DBCallCommon.BindAJAXCombox(ddlProName, sqltext, dataText, dataValue);
        }
        /// <summary>
        /// 绑定工程名称
        /// </summary>
        protected void BindEngName()
        {
            if (ddlProName.SelectedIndex != 0)
            {
                string sqltext = "select distinct PS_ENGID,PS_ENGID+'||'+PS_ENGNAME as PS_ENGNAME from VIEW_TM_PAINTSCHEME where PS_PJID='" + ddlProName.SelectedValue + "'";
                string dataText = "PS_ENGNAME";
                string dataValue = "PS_ENGID";
                DBCallCommon.BindAJAXCombox(ddlEngName, sqltext, dataText, dataValue);
            }
            else
            {
                ddlEngName.Items.Clear();
                ddlEngName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
                ddlEngName.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 绑定批号
        /// </summary>
        protected void BindLotNum()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append("select PS_ID from VIEW_TM_PAINTSCHEME where PS_STATE='8' ");
            if (ddlProName.SelectedIndex != 0)
            {
                strb.Append(" and PS_PJID='"+ddlProName.SelectedValue+"'");
            }

            if(ddlEngName.SelectedIndex!=0)
            {
                strb.Append(" and PS_ENGID='" + ddlEngName.SelectedValue + "'");
            }

            string dataText="PS_ID";
            string dataValue="PS_ID";

            DBCallCommon.BindAJAXCombox(ddlLotNum,strb.ToString(),dataText,dataValue);
        }
        /// <summary>
        /// 项目名称改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindEngName();
            this.BindLotNum();
            UCPaging1.CurrentPage = 1;
            this.GetBoundData();
        }
        /// <summary>
        /// 工程名称改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlEngName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindLotNum();
            UCPaging1.CurrentPage = 1;
            this.GetBoundData();
        }
        /// <summary>
        /// 批号改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlLotNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.GetBoundData();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkBtnExport_OnClick(object sender, EventArgs e)
        {
            if (ddlLotNum.SelectedIndex != 0)
            {
                ExportTMDataFromDB.ExportPSData(ddlLotNum.SelectedValue);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要导出的批号！！！');", true);
            }
        }

        protected void GridView1_OnPreRender(object sender, EventArgs e)
        {
            ExportTMDataFromDB.AbandonRepeatColumnCells(GridView1, 1);
        }
    }
}
