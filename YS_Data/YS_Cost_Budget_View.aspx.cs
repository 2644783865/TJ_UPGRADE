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
using System.Data.SqlClient;

namespace ZCZJ_DPF.YS_Data
{
    public partial class YS_Cost_Budget_View : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        string userName, uid, depId, position, type;
        protected void Page_Load(object sender, EventArgs e)
        {
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
            userName = Session["UserName"].ToString();
            uid = Session["UserID"].ToString();
            depId = Session["UserDeptID"].ToString();
            position = Session["POSITION"].ToString(); ;
            type = Request.QueryString["type"].ToString();
           
            if (!IsPostBack)
            {
                BindPer();
                
                BindState();
                BindRevState();
                BindProject();
                BindEngineer();
                BindTsaId();
                InitVar();
                GetTechRequireData();
               
            }
            InitVar();
            SetGVColsVisible();
            //CheckUser(ControlFinder);
        }

        #region 判断gridview列的可见性
        protected void SetGVColsVisible()
        {

            if (type=="0")//由预算编制进入
            {
                //预算的状态全都显示出来
                //for (int i = 9; i < 16; i++)
                //{
                //    GridView1.Columns[i].Visible = true;
                //}
                
                switch(depId)
                {
                    case "06": GridView1.Columns[13].Visible = true; break;
                    case "05": GridView1.Columns[9].Visible = true; break;
                    case "04": GridView1.Columns[10].Visible = true; break;
                    default: break;
                }


            }
            else if (type == "1")//由审批进入
            {
                for (int i = 16; i < 22; i++)
                {
                    GridView1.Columns[i].Visible = false;
                }

                GridView1.Columns[12].Visible = true;
                ddl_YS_REVSTATE.Visible = false;
                lb_YS_REVSTATE.Visible = false;
                ddl_addper.Visible = false;
                lb_addper.Visible = false;

                switch (position)
                {
                    case "0102":
                        GridView1.Columns[14].Visible = true; break;
                    case "0101":
                        GridView1.Columns[15].Visible = true; break;
                    default:
                        GridView1.Columns[13].Visible = true; break;
                }
            }

        }


        #endregion

        #region 填充下拉框键值对

        /// <summary>
        /// 填充项目名称下拉框的数据
        /// </summary>
        protected void BindProject()
        {
            string sqltext = "SELECT DISTINCT YS_PROJECTNAME AS DDLVALUE,YS_PROJECTNAME AS DDLTEXT FROM YS_COST_BUDGET ORDER BY YS_PROJECTNAME";
            string dataText = "DDLTEXT";
            string dataValue = "DDLVALUE";
            DBCallCommon.BindDdl(ddl_project, sqltext, dataText, dataValue);
        }

        /// <summary>
        /// 填充设备名称下拉框的数据
        /// </summary>
        protected void BindEngineer()
        {
            string sqltext = "SELECT DISTINCT YS_ENGINEERNAME AS DDLVALUE,YS_ENGINEERNAME AS DDLTEXT FROM YS_COST_BUDGET where YS_PROJECTNAME='" + ddl_project.SelectedItem.ToString() + "' ORDER BY YS_ENGINEERNAME";
            string dataText = "DDLTEXT";
            string dataValue = "DDLVALUE";
            DBCallCommon.BindDdl(ddl_engineer, sqltext, dataText, dataValue);
        }

        /// <summary>
        /// 填充任务号下拉框的数据
        /// </summary>
        protected void BindTsaId()
        {
            string sqltext = "SELECT DISTINCT YS_TSA_ID AS DDLVALUE,YS_TSA_ID AS DDLTEXT FROM YS_COST_BUDGET ORDER BY YS_TSA_ID";
            string dataText = "DDLTEXT";
            string dataValue = "DDLVALUE";
            DBCallCommon.BindDdl(ddl_YS_TSA_ID, sqltext, dataText, dataValue);
        }

        /// <summary>
        /// 填充编制进度下拉框的数据
        /// </summary>
        protected void BindRevState()
        {
            string sqltext = "SELECT DISTINCT YS_STATE AS DDLVALUE,case when YS_STATE is null then '新增预算' when YS_STATE='0' then '新增预算'" +
            " when YS_STATE='1' then '财务填写中'when YS_STATE='2' then '部门反馈中'when YS_STATE='3' then '财务调整中'when YS_STATE='4' then '送审中' when YS_STATE='5' then '编制完成' end  AS DDLTEXT FROM YS_COST_BUDGET ORDER BY YS_STATE";
            string dataText = "DDLTEXT";
            string dataValue = "DDLVALUE";
            DBCallCommon.BindDdl(ddl_State, sqltext, dataText, dataValue);
        }

        /// <summary>
        /// 填充审核进度下拉框的数据
        /// </summary>
        protected void BindState()
        {
            string sqltext = "SELECT DISTINCT YS_REVSTATE AS DDLVALUE,case when YS_REVSTATE is null then '未送审' when YS_REVSTATE='0' then '未送审'" +
            " when YS_REVSTATE='1' then '审核中'when YS_REVSTATE='2' then '通过'when YS_REVSTATE='3' then '驳回' end  AS DDLTEXT FROM YS_COST_BUDGET ORDER BY YS_REVSTATE";
            string dataText = "DDLTEXT";
            string dataValue = "DDLVALUE";
            DBCallCommon.BindDdl(ddl_YS_REVSTATE, sqltext, dataText, dataValue);
        }
        /// <summary>
        /// 填充制单人下拉框的数据
        /// </summary>
        protected void BindPer()
        {
            string sqltext = "SELECT DISTINCT YS_ADDNAME AS DDLVALUE,YS_ADDNAME AS DDLTEXT FROM YS_COST_BUDGET ORDER BY YS_ADDNAME";
            string dataText = "DDLTEXT";
            string dataValue = "DDLVALUE";
            DBCallCommon.BindDdl(ddl_addper, sqltext, dataText, dataValue);
        }

        #endregion


        #region 分页
        /// <summary>
        /// 初始化页面信息（1、初始化页面查询对象，2、将页面相关信息传递给页面控件）
        /// </summary>
        private void InitVar()
        {
            InitPager();

            //将页面相关信息传递给页码控件
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);//将翻页事件处理程序添加到翻页事件监听器上           
            UCPaging1.PageSize = pager.PageSize;//每页显示的记录数传递给页面控件
        }

        /// <summary>
        /// 翻页事件处理程序
        /// </summary>
        /// <param name="pageNumber"></param>
        void Pager_PageChanged(int pageNumber)
        {
            GetTechRequireData();
        }

        /// <summary>
        /// 初始化分页查询对象，用于编写sql命令
        /// </summary>
        private void InitPager()
        {
            pager.TableName = "YS_COST_BUDGET";
            pager.PrimaryKey = "YS_CONTRACT_NO";
            pager.ShowFields = "YS_TSA_ID,YS_CONTRACT_NO,YS_PROJECTNAME,[YS_ENGINEERNAME],YS_BUDGET_INCOME,(YS_MATERIAL_COST+YS_LABOUR_COST+YS_TRANS_COST) AS YS_TOTALCOST_ALL,(YS_BUDGET_INCOME-(YS_MATERIAL_COST+YS_LABOUR_COST+YS_TRANS_COST)) AS YS_PROFIT,(YS_BUDGET_INCOME-(YS_MATERIAL_COST+YS_LABOUR_COST+YS_TRANS_COST)-0.0001)/(YS_BUDGET_INCOME-0.0001) AS YS_PROFIT_RATE,YS_MATERIAL_COST,YS_LABOUR_COST,YS_TRANS_COST,YS_CAIWU, YS_CAIGOU,YS_SHENGCHAN,YS_REVSTATE,YS_FIRST_REVSTATE,YS_SECOND_REVSTATE," +
            "YS_TEC_SUBMIT_NAME, YS_ADDTIME,YS_ADDNAME,YS_ADDFINISHTIME,YS_STATE,YS_NOTE";
            pager.OrderField = "YS_ADDTIME";
            pager.StrWhere = this.GetStrWhere();
            pager.OrderType = 1; //按任务名称升序排列
            pager.PageSize = 10;
        }


        /// <summary>
        /// 更新GridView控件和页码控件数据，没有用的？？？
        /// </summary>
        private void InitInfo()
        {

            GetTechRequireData();
        }

        /// <summary>
        /// 使用页面查询对象更新GridView的数据和页码控件的数据
        /// </summary>
        protected void GetTechRequireData()
        {
            //将当前页数传递给页面查询对象
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


        /// <summary>
        /// 获取sql命令的where部分（查询条件）
        /// </summary>
        /// <returns></returns>
        protected string GetStrWhere()
        {
            string strwhere = " 1=1 ";

            if (ddl_YS_TSA_ID.SelectedIndex != 0)
            {
                strwhere += "and YS_TSA_ID = '" + ddl_YS_TSA_ID.SelectedValue + "'";
            }

            if (ddl_addper.SelectedIndex != 0)//制单人
            {
                strwhere += "and YS_ADDNAME='" + ddl_addper.SelectedValue + "'";
            }

            if (ddl_State.SelectedIndex != 0)//编制状态
            {
                if (ddl_State.SelectedValue == "")
                {
                    strwhere += "and YS_STATE is null";
                }
                else
                {
                    strwhere += "and YS_STATE='" + ddl_State.SelectedValue + "'";
                }
            }

            if (ddl_YS_REVSTATE.SelectedIndex != 0)//审核状态
            {
                if (ddl_YS_REVSTATE.SelectedValue == "")
                {
                    strwhere += "and YS_REVSTATE is null";
                }
                else
                {
                    strwhere += "and YS_REVSTATE='" + ddl_YS_REVSTATE.SelectedValue + "'";
                }
            }

            if (ddl_project.SelectedIndex != 0)//项目名称
            {
                strwhere += " and YS_PROJECTNAME='" + ddl_project.SelectedValue + "'";
            }
            if (ddl_engineer.SelectedIndex != 0)//设备名称
            {
                strwhere += " and YS_ENGINEERNAME='" + ddl_engineer.SelectedValue + "'";
            }

            if (ckb_time.Checked == true)
            {
                strwhere += " and YS_ADDFINISHTIME<GETDATE() and isnull(YS_STATE,'')!='5'";
            }

            return strwhere;
        }

        #endregion

        #region 前台获取进度状态

        /// <summary>
        /// 用于前端获取采购、生产反馈进度
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        protected string GetFeedBackState(string State)
        {
            string retValue = "";
            switch (State)
            {
                case "0":
                    retValue = "未下推"; break;
                case "1":
                    retValue = "待反馈"; break;
                case "2":
                    retValue = "已反馈"; break;
                case "3":
                    retValue = "驳回"; break;
                
                default:
                    break;
            }
            return retValue;
        }


      



        /// <summary>
        /// 用于前端获取预算编制进度
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        protected string GetState(string State)
        {
            string retValue = "";
            switch (State)
            {
                case "0":
                    retValue = "新增预算"; break;
                case "1":
                    retValue = "财务填写中"; break;
                case "2":
                    retValue = "部门反馈中"; break;
                case "3":
                    retValue = "财务调整中"; break;
                case "4":
                    retValue = "送审中"; break;
                case "5":
                    retValue = "编制完成"; break;
                default:
                    break;
            }
            return retValue;
        }


        /// <summary>
        /// 用于前端获取领导审核进度
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        protected string GetRevState(string RevState)
        {
            string retValue = "";
            switch (RevState)
            {
                case "0":
                    retValue = "未送审"; break;
                case "1":
                    retValue = "审核中"; break;
                case "2":
                    retValue = "同意"; break;
                case "3":
                    retValue = "驳回"; break;
                default:
                    break;
            }
            return retValue;
        }

        /// <summary>
        /// 用于前端获取财务调整与审核、一级审核、二级审核等各个部门的审核进度
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        protected string GetDisRevState(string State)
        {
            string retValue = "";
            switch (State)
            {
                case "0":
                    retValue = "未送审"; break;
                case "1":
                    retValue = "待审核"; break;
                case "2":
                    retValue = "同意"; break;
                case "3":
                    retValue = "驳回"; break;
                default:
                    break;
            }
            return retValue;
        }      

        #endregion

        #region 前台控件触发的事件
        /// <summary>
        /// 查询项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddl_project_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindEngineer();
            UCPaging1.CurrentPage = 1;
            InitVar();
            GetTechRequireData();
        }


        /// <summary>
        /// 查询事件，更新where条件，重新访问一次数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_search_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            GetTechRequireData();
        }


       
        #endregion





       protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string tsaId = e.Row.Cells[1].Text.Trim();

                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(string.Format("SELECT YS_REBUT FROM dbo.YS_COST_BUDGET WHERE YS_TSA_ID='{0}'", tsaId));
                if (type=="0"&&dr.Read() && dr["YS_REBUT"].ToString() == depId && position!="0601")
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                }
                dr.Close();
                //设置收入为0的利率
                if (e.Row.Cells[5].Text == "0.0000")
                {
                    e.Row.Cells[8].Text = "分母为0";
                }               
            }
        }

    }
}