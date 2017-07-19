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
    public partial class TM_Out_Source_Search : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        string sqlText;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetProNameData();
                GetEngNameData();
                GetSbNameData();
                GetChdNameData();
                GetFillManData();
                GetPCodeData();
                GetMaterData();
                GetOutShapeData();
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
            pager.TableName = "View_TM_OUTSOURCELIST";
            pager.PrimaryKey = "OSL_ID";
            pager.ShowFields = "OSL_ID,OSL_OUTSOURCENO,OSL_NEWXUHAO,OSL_BIAOSHINO,OSL_ZONGXU,OSL_NAME,OSL_GUIGE,OSL_CAIZHI,OSL_UNITWGHT,OSL_NUMBER,OSL_TOTALWGHTL,OSL_WDEPNAME,OSL_REQUEST,OSL_REQDATE,OSL_DELSITE,OSL_TRACKNUM,OST_OUTTYPE,OST_MDATE";
            pager.OrderField = "OSL_OUTSOURCENO,dbo.f_formatstr(OSL_NEWXUHAO, '.')";
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
            string str = " OSL_STATUS='0' and OST_STATE='8'  ";
            //项目
            if (ddlProName.SelectedValue != "-请选择-")
            {
                str += " and OSL_PJID='" + ddlProName.SelectedValue + "' ";
            }
            //查询工程
            if (ddlEngName.SelectedValue != "-请选择-")
            {
                str += " and dbo.GetNoEngid(OSL_ENGID,'-')='" + ddlEngName.SelectedValue + "' ";
            }
            if (ViewState["QueryType"].ToString() == "Conventional-Inquires")
            {

                //查询设备
                if (ddlSBname.SelectedValue != "-请选择-")
                {
                    str += " and (OSL_NEWXUHAO = '" + ddlSBname.SelectedValue + "' or OSL_NEWXUHAO like '" + ddlSBname.SelectedValue + ".%') ";
                }
                //查询部件
                if (ddlBJname.SelectedValue != "-请选择-")
                {
                    str += " and (OSL_NEWXUHAO = '" + ddlBJname.SelectedValue + "' or OSL_NEWXUHAO like '" + ddlBJname.SelectedValue + ".%')";
                }
                //查询技术员
                if (ddlFillman.SelectedValue != "-请选择-")
                {
                    str += " and OSL_ENGID='" + ddlFillman.SelectedValue + "' ";
                }
                //批号
                if (ddlPCode.SelectedValue != "-请选择-")
                {
                    str += " and OSL_OUTSOURCENO='" + ddlPCode.SelectedValue + "'";
                }
                //外协类型
                if (ddlOutType.SelectedValue != "-请选择-")
                {
                    if (ddlOutType.SelectedValue == "协A/协B")
                    {
                        str += " and OST_OUTTYPE in('协A','协B')";
                    }
                    else
                    {
                        str += " and OST_OUTTYPE='" + ddlOutType.SelectedValue + "'";
                    }
                }
                //材料形状
                if (ddlOrgShape.SelectedValue != "-请选择-")
                {
                    str += " and OSL_MASHAPE='" + ddlOrgShape.SelectedValue + "'";
                }
            }
            else if (ViewState["QueryType"].ToString() == "User-Defined")
            {
                if (txtQueryContent.Text.Trim() != "")
                {
                    if (ddlQueryType.SelectedValue == "OSL_NEWXUHAO" || ddlQueryType.SelectedValue == "OSL_ZONGXU")
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

        #region 筛选数据绑定
        /// <summary>
        /// 绑定项目名称
        /// </summary>
        private void GetProNameData()
        {
            sqlText = "select distinct OSL_PJID,OSL_PJID+'‖'+OSL_PJNAME as OSL_PJNAME from View_TM_OUTSOURCELIST where OSL_STATUS='0' and OST_STATE='8' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlProName.DataSource = dt;
            ddlProName.DataTextField = "OSL_PJNAME";
            ddlProName.DataValueField = "OSL_PJID";
            ddlProName.DataBind();
            ddlProName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlProName.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定工程名称
        /// </summary>
        private void GetEngNameData()
        {
            sqlText = "select TSA_ID,TSA_ID+'‖'+TSA_ENGNAME as TSA_ENGNAME from TBPM_TCTSASSGN ";
            sqlText += " where TSA_ID in (select distinct dbo.GetNoEngid(OSL_ENGID,'-') as OSL_ENGID from View_TM_OUTSOURCELIST ";
            sqlText += "where OSL_PJID='" + ddlProName.SelectedValue + "' and OSL_STATUS='0' and OST_STATE='8')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlEngName.DataSource = dt;
            ddlEngName.DataTextField = "TSA_ENGNAME";
            ddlEngName.DataValueField = "TSA_ID";
            ddlEngName.DataBind();
            ddlEngName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlEngName.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定设备
        /// </summary>
        private void GetSbNameData()
        {
            sqlText = "select distinct BM_CHANAME,BM_XUHAO,dbo.f_FormatSTR(BM_XUHAO,'.') from VWPM_PROSTRC ";
            sqlText += "where dbo.GetNoEngid(BM_ENGID,'-')='" + ddlEngName.SelectedValue + "' and dbo.Splitnum(BM_XUHAO,'.')=0 order by dbo.f_FormatSTR(BM_XUHAO,'.')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlSBname.DataSource = dt;
            ddlSBname.DataTextField = "BM_CHANAME";
            ddlSBname.DataValueField = "BM_XUHAO";
            ddlSBname.DataBind();
            ddlSBname.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlSBname.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定部件名称
        /// </summary>
        private void GetChdNameData()
        {
            sqlText = "select distinct BM_CHANAME collate  Chinese_PRC_CS_AS_KS_WS AS BM_CHANAME,BM_XUHAO,dbo.f_FormatSTR(BM_XUHAO,'.') from VWPM_PROSTRC ";
            sqlText += "where dbo.GetNoEngid(BM_ENGID,'-')='" + ddlEngName.SelectedValue + "' and dbo.Splitnum(BM_XUHAO,'.')=1 ";
            if (ddlSBname.SelectedValue != "-请选择-")
            {
                sqlText += " and BM_XUHAO like '" + ddlSBname.SelectedValue + ".%'  ";
            }
            sqlText += " order by BM_CHANAME collate  Chinese_PRC_CS_AS_KS_WS";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlBJname.DataSource = dt;
            ddlBJname.DataTextField = "BM_CHANAME";
            ddlBJname.DataValueField = "BM_XUHAO";
            ddlBJname.DataBind();
            ddlBJname.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlBJname.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定技术员
        /// </summary>
        private void GetFillManData()
        {
            sqlText = "select TSA_ID,TSA_TCCLERKNM from View_TM_TaskAssign where dbo.GetNoEngid(TSA_ID,'-')='" + ddlEngName.SelectedValue + "' and charindex('-',TSA_ID)>0";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlFillman.DataSource = dt;
            ddlFillman.DataTextField = "TSA_TCCLERKNM";
            ddlFillman.DataValueField = "TSA_ID";
            ddlFillman.DataBind();
            ddlFillman.Items.Insert(0, new ListItem("", "-请选择-"));
            ddlFillman.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定批号
        /// </summary>
        private void GetPCodeData()
        {
            sqlText = "select distinct OSL_OUTSOURCENO from View_TM_OUTSOURCELIST where OSL_NEWXUHAO like '" + ddlSBname.SelectedValue + ".%' and dbo.GetNoEngid(OSL_ENGID,'-')='" + ddlEngName.SelectedValue + "' and OSL_STATUS='0' and OST_STATE='8'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlPCode.DataSource = dt;
            ddlPCode.DataTextField = "OSL_OUTSOURCENO";
            ddlPCode.DataValueField = "OSL_OUTSOURCENO";
            ddlPCode.DataBind();
            ddlPCode.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlPCode.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定材料名称
        /// </summary>
        private void GetMaterData()
        {
            sqlText = "select distinct OSL_NAME collate  Chinese_PRC_CS_AS_KS_WS AS OSL_NAME from dbo.View_TM_OUTSOURCELIST where OSL_NEWXUHAO like '" + ddlBJname.SelectedValue + ".%' and OSL_NAME is not null and OSL_STATUS='0' and OST_STATE='8' order by OSL_NAME collate  Chinese_PRC_CS_AS_KS_WS";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlmatername.DataSource = dt;
            ddlmatername.DataTextField = "OSL_NAME";
            ddlmatername.DataValueField = "OSL_NAME";
            ddlmatername.DataBind();
            ddlmatername.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlmatername.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定外协形状
        /// </summary>
        private void GetOutShapeData()
        {
            sqlText = "select distinct OSL_MASHAPE  from View_TM_OUTSOURCELIST ";
            sqlText += " where OSL_NEWXUHAO like '" + ddlBJname.SelectedValue + ".%' and OSL_MASHAPE is not null order by OSL_MASHAPE ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlOrgShape.DataSource = dt;
            ddlOrgShape.DataTextField = "OSL_MASHAPE";
            ddlOrgShape.DataValueField = "OSL_MASHAPE";
            ddlOrgShape.DataBind();
            ddlOrgShape.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlOrgShape.SelectedIndex = 0;
        }
        #endregion

        /// <summary>
        /// 项目名称查询
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
        /// 工程名称查询
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
        /// 设备查询
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


            if (ddlSBname.SelectedValue == "-请选择-")
            {
                ddlFillman.SelectedValue = "-请选择-";
            }
            else
            {
                //获取带'-'的生产制号.从而绑定技术员
                string engcode = ddlEngName.SelectedValue + "-" + ddlSBname.SelectedValue;
                ddlFillman.SelectedValue = engcode;
            }
            GetChdNameData();
            GetPCodeData();
            GetBoundData();
        }
        
        /// <summary>
        /// 部件查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlBJname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、工程！！！');", true);
                return;
            }
            ViewState["QueryType"] = "Conventional-Inquires";//常规查询或自定义查询


            GetMaterData();
            GetOutShapeData();
            GetBoundData();
        }

        /// <summary>
        /// 技术员查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlFillman_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、工程！！！');", true);
                return;
            }
            ViewState["QueryType"] = "Conventional-Inquires";//常规查询或自定义查询


            if (ddlFillman.SelectedValue == "-请选择-")
            {
                ddlSBname.SelectedValue = "-请选择-";
            }
            else
            {
                string[] str = (ddlFillman.SelectedValue.ToString()).Split('-');
                ddlSBname.SelectedValue = str[1].ToString();
            }
            GetBoundData();
        }
        
        /// <summary>
        /// 批号查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlPCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、工程！！！');", true);
                return;
            }
            ViewState["QueryType"] = "Conventional-Inquires";//常规查询或自定义查询


            GetBoundData();
        }

        /// <summary>
        /// 外协类别查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlOutType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、工程！！！');", true);
                return;
            }
            ViewState["QueryType"] = "Conventional-Inquires";//常规查询或自定义查询

            GetBoundData();
        }
        
        /// <summary>
        /// Button查询
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
            ddlPCode.SelectedIndex = 0;
            ddlmatername.SelectedIndex = 0;
            ddlOrgShape.SelectedIndex = 0;

            UCPaging1.CurrentPage = 1;
            InitPager();
            GetBoundData();
        }
        /// <summary>
        /// 导出外协Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkBtnExport_OnClick(object sender, EventArgs e)
        {
            if (ddlProName.SelectedIndex == 0 || ddlEngName.SelectedIndex == 0||ddlOutType.SelectedIndex==0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择项目、工程及外协类别！！！');", true);
            }
            else
            {
                ExportTMDataFromDB.ExportAllOutData(ddlOutType.SelectedValue,ddlEngName.SelectedValue);
            }
        }


        protected void GridView1_OnPreRender(object sender, EventArgs e)
        {
            ExportTMDataFromDB.AbandonRepeatColumnCells(GridView1, 1);
            ExportTMDataFromDB.AbandonRepeatColumnCells(GridView1, 2);
        }
    }
}
