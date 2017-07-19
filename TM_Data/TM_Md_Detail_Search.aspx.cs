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
using System.Collections.Generic;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Md_Detail_Search : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        string sqlText;
        string viewtablename;
        string tablename;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetProNameData();
                GetEngNameData();
                GetSbNameData();
                GetChdNameData();
                GetFillManData();
              
                //BandNum();
                ViewState["QueryType"] = "Conventional-Inquires";//常规查询或自定义查询
            }
            InitVar();
          
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
            pager.TableName = "View_TM_DQO";
            pager.PrimaryKey = "cast(BM_ID as varchar(50))+BM_ZONGXU";
            pager.ShowFields = "*,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar) AS NUMBER";
            pager.OrderField = "dbo.f_formatstr(BM_ZONGXU, '.')";
            pager.StrWhere = GetSiftData();
            pager.OrderType = 0;//按任务名称升序排列
            pager.PageSize = 250;
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
        /// <returns></returns>
        private string GetSiftData()
        {
            string str = " 1=1";
            //项目
            if (ddlProName.SelectedValue != "-请选择-")
            {
                str += " and BM_PJID='" + ddlProName.SelectedValue + "' ";
            }
            //查询工程
            if (ddlEngName.SelectedValue != "-请选择-")
            {
                str += " and BM_ENGID='" + ddlEngName.SelectedValue + "' ";
            }

            if (ViewState["QueryType"].ToString() == "Conventional-Inquires")
            {
                //查询设备
                if (ddlSBname.SelectedValue != "-请选择-")
                {
                    str += " and (BM_ZONGXU = '" + ddlSBname.SelectedValue + "' or BM_ZONGXU like '" + ddlSBname.SelectedValue + ".%') ";
                }
                //查询部件
                if (ddlBJname.SelectedValue != "-请选择-")
                {
                    str += " and (BM_ZONGXU = '" + ddlBJname.SelectedValue + "' or BM_ZONGXU like '" + ddlBJname.SelectedValue + ".%')";
                }
                //查询技术员
                if (ddlFillman.SelectedValue != "-请选择-")
                {
                    str += " and BM_FILLMAN='" + ddlFillman.SelectedValue + "' ";
                }
    

            }
            //自定义查询条件
            else if (ViewState["QueryType"].ToString() == "User-Defined")
            {
                if (txtQueryContent.Text.Trim() != "")
                {
                    if (ddlQueryType.SelectedValue == "BM_ZONGXU" || ddlQueryType.SelectedValue == "BM_XUHAO")
                    {
                        str += " and (" + ddlQueryType.SelectedValue + "='" + txtQueryContent.Text.Trim() + "' or " + ddlQueryType.SelectedValue + " like '" + txtQueryContent.Text.Trim() + ".%')";
                    }
                    else
                    {
                        str += " and " + ddlQueryType.SelectedValue + " like '%" + txtQueryContent.Text.Trim() + "%'";
                    }
                }
                else
                {
                    str += " and " + ddlQueryType.SelectedValue + "='" + txtQueryContent.Text.Trim() + "'";
                }
            }
            return str;
        }

        #region dropdownlist绑定
        //绑定项目名称
        private void GetProNameData()
        {
            sqlText = "select distinct TSA_PJID,TSA_PJID+'‖'+CM_PROJ as TSA_PJNAME from View_TM_TaskAssign ORDER BY TSA_PJID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlProName.DataSource = dt;
            ddlProName.DataTextField = "TSA_PJNAME";
            ddlProName.DataValueField = "TSA_PJID";
            ddlProName.DataBind();
            ddlProName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlProName.SelectedIndex = 0;
        }
        //绑定工程名称
        private void GetEngNameData()
        {
            sqlText = "select TSA_ID,TSA_ID+'‖'+TSA_ENGNAME as TSA_ENGNAME from TBPM_TCTSASSGN ";
            sqlText += "where TSA_PJID='" + ddlProName.SelectedValue + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlEngName.DataSource = dt;
            ddlEngName.DataTextField = "TSA_ENGNAME";
            ddlEngName.DataValueField = "TSA_ID";
            ddlEngName.DataBind();
            ddlEngName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlEngName.SelectedIndex = 0;
        }
        private void GetSbNameData()
        {
            sqlText = "select distinct BM_CHANAME collate  Chinese_PRC_CS_AS_KS_WS AS MS_NAME,BM_ZONGXU,dbo.f_FormatSTR(BM_ZONGXU,'.') from View_TM_DQO ";
            sqlText += "where BM_ENGID='" + ddlEngName.SelectedValue + "' and dbo.Splitnum(BM_ZONGXU,'.')=0  order by BM_CHANAME collate  Chinese_PRC_CS_AS_KS_WS";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlSBname.DataSource = dt;
            ddlSBname.DataTextField = "MS_NAME";
            ddlSBname.DataValueField = "BM_ZONGXU";
            ddlSBname.DataBind();
            ddlSBname.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlSBname.SelectedIndex = 0;
        }
        //绑定部件名称
        private void GetChdNameData()
        {
            sqlText = "select distinct BM_CHANAME collate  Chinese_PRC_CS_AS_KS_WS AS MS_NAME,BM_ZONGXU,dbo.f_FormatSTR(BM_ZONGXU,'.') from View_TM_DQO ";
            sqlText += "where BM_ENGID='" + ddlEngName.SelectedValue + "' and dbo.Splitnum(BM_ZONGXU,'.')=1  ";
            if (ddlSBname.SelectedValue != "-请选择-")
            {
                sqlText += " and BM_ZONGXU like '" + ddlSBname.SelectedValue + ".%'  ";
            }
            sqlText += "  order by BM_CHANAME collate  Chinese_PRC_CS_AS_KS_WS";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlBJname.DataSource = dt;
            ddlBJname.DataTextField = "MS_NAME";
            ddlBJname.DataValueField = "BM_ZONGXU";
            ddlBJname.DataBind();
            ddlBJname.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlBJname.SelectedIndex = 0;
        }
        //绑定技术员
        private void GetFillManData()
        {
            sqlText = "select distinct TSA_TCCLERK,TSA_TCCLERKNM from View_TM_TaskAssign order by TSA_TCCLERK,TSA_TCCLERKNM";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlFillman.DataSource = dt;
            ddlFillman.DataTextField = "TSA_TCCLERKNM";
            ddlFillman.DataValueField = "TSA_TCCLERK";
            ddlFillman.DataBind();
            ddlFillman.Items.Insert(0, new ListItem("", "-请选择-"));
            ddlFillman.SelectedIndex = 0;
        }

        #endregion

        /// <summary>
        /// ddl项目名称改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedValue == "-请选择-")
            {
                ddlEngName.SelectedIndex = 0;
                ddlBJname.SelectedIndex = 0;

            }
            else
            {
                GetEngNameData();//绑定工程
            }
        }
        /// <summary>
        /// ddl工程名称改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlEngName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEngName.SelectedValue == "-请选择-")
            {
                ddlBJname.SelectedIndex = 0;

            }
            else
            {
                GetSbNameData(); //绑定设备
                GetChdNameData();//绑定部件
                GetFillManData();//绑定技术员
            }
            GetBoundData();

        }
        /// <summary>
        /// ddl设备名称改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlSBname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、工程！！！');", true);
                return;
            }
            ViewState["QueryType"] = "Conventional-Inquires";//常规查询或自定义查询

            GetChdNameData();
            GetBoundData();
        }
        /// <summary>
        /// ddl部件名称改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlBJname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、设备！！！');", true);
                return;
            }
            ViewState["QueryType"] = "Conventional-Inquires";//常规查询或自定义查询

            GetBoundData();
        }
        /// <summary>
        /// ddl技术员改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlFillman_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、设备！！！');", true);
                return;
            }
            ViewState["QueryType"] = "Conventional-Inquires";//常规查询或自定义查询

            GetBoundData();
        }
        /// <summary>
        /// 自定义查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、工程！！！');", true);
                return;
            }
            ViewState["QueryType"] = "User-Defined";
            UCPaging1.CurrentPage = 1;
            InitPager();
            GetBoundData();
        }
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_OnClick(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择项目、工程，重置无效！！！');", true);
                return;
            }

            //自定义
            ddlQueryType.SelectedIndex = 0;
            txtQueryContent.Text = "";
            //常规
            ddlSBname.SelectedIndex = 0;
            ddlFillman.SelectedIndex = 0;
            ddlBJname.SelectedIndex = 0;

            UCPaging1.CurrentPage = 1;
            InitPager();
            GetBoundData();
        }

        /// <summary>
        /// 导出制作明细
        /// </summary>
        /// <param name="seneder"></param>
        /// <param name="e"></param>
        protected void lnkBtnExport_OnClick(object seneder, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、设备！！！');", true);
            }
            else
            {
                ExportTMDataFromDB.ExportAllMsData(ddlEngName.SelectedValue);
            }
        }
        /// <summary>
        /// 设置标签颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((HiddenField)e.Row.Cells[0].FindControl("hdfMSState")).Value == "1")
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Green;//已提
                }

                HiddenField hfmpchg = (HiddenField)e.Row.Cells[0].FindControl("hdfMSChg");

                if (hfmpchg.Value == "1")
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Gray;//制作明细变更删除
                }
                else if (hfmpchg.Value == "2")
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Orange;//制作明细变更增加
                }
                else if (hfmpchg.Value == "3")
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;//制作明细变更修改
                }
            }
        }


    }
}
