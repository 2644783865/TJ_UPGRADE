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
    public partial class YS_Cost_Budget_Audit_View : BasicPage
    {

        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindPer();
                BindState();
                InitVar();
                GetTechRequireData();
                //control_visible();
            }
            InitVar();
            //CheckUser(ControlFinder);
        }




        /// <summary>
        /// 填充制单人下拉框的数据
        /// </summary>
        protected void BindPer()
        {
            string sqltext = "SELECT DISTINCT YS_ADDNAME AS DDLVALUE,YS_ADDNAME AS DDLTEXT FROM VIEW_YS_COST_BUDGET ORDER BY YS_ADDNAME";
            string dataText = "DDLTEXT";
            string dataValue = "DDLVALUE";
            DBCallCommon.BindDdl(ddl_addper, sqltext, dataText, dataValue);
        }

        /// <summary>
        /// 填充审核状态下拉框的数据
        /// </summary>
        protected void BindState()
        {
            string sqltext = "SELECT DISTINCT YS_STATE AS DDLVALUE,case when YS_STATE is null then '初始化' when YS_STATE='0' then '财务填写中'" +
            " when YS_STATE='1' then '部门反馈中'when YS_STATE='2' then '财务审核与调整中'when YS_STATE='3' then '领导审批中'when YS_STATE='4' then '驳回至财务' when YS_STATE='5' then '驳回至部门' end  AS DDLTEXT FROM VIEW_YS_COST_BUDGET ORDER BY YS_STATE";
            string dataText = "DDLTEXT";
            string dataValue = "DDLVALUE";
            DBCallCommon.BindDdl(ddl_State, sqltext, dataText, dataValue);
        }




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
            pager.TableName = "View_YS_COST_BUDGET";
            pager.PrimaryKey = "YS_CONTRACT_NO";
            pager.ShowFields = "YS_TSA_ID,YS_CONTRACT_NO,CM_PROJ,[TSA_ENGNAME],YS_BUDGET_INCOME,[YS_TOTALCOST_ALL],(YS_BUDGET_INCOME-YS_TOTALCOST_ALL) AS YS_PROFIT,(YS_BUDGET_INCOME-YS_TOTALCOST_ALL)/YS_BUDGET_INCOME AS YS_PROFIT_RATE,YS_FERROUS_METAL,YS_PURCHASE_PART,YS_MACHINING_PART,YS_PAINT_COATING,YS_ELECTRICAL,YS_OTHERMAT_COST,YS_TEAM_CONTRACT, " +
            "YS_FAC_CONTRACT,YS_PRODUCT_OUT,YS_TRANS_COST, " +
            "YS_ADDNAME,YS_ADDTIME,YS_ADDFINISHTIME,YS_STATE,YS_NOTE";
            pager.OrderField = "YS_ADDTIME";
            pager.StrWhere = this.GetStrWhere();
            pager.OrderType = 1;//按任务名称升序排列
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
            strwhere += "and YS_TSA_ID like '%" + txt_search.Text.ToString() + "%'";

            if (ddl_addper.SelectedIndex != 0)//制单人
            {
                strwhere += "and YS_ADDNAME='" + ddl_addper.SelectedValue + "'";
            }

            if (ddl_State.SelectedIndex != 0)//审核状态
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
            return strwhere;
        }
        #endregion



        /// <summary>
        /// 用于前端获取预算进度
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        protected string GetState(string State)
        {
            string retValue = "";
            switch (State)
            {
                case "0":
                    retValue = "初始化"; break;
                case "1":
                    retValue = "提交未审批"; break;
                case "2":
                    retValue = "审批中"; break;
                case "3":
                    retValue = "已通过"; break;
                case "4":
                    retValue = "已驳回"; break;
                case "":
                    retValue = "未提交"; break;
                default:
                    break;
            }
            return retValue;
        }

        //删除，用于前端确定完善状态
        protected string GetEditState(string editstate)
        {
            string retValue = "";
            string sqltext = "select YS_SHENGCHAN,YS_JISHU,YS_SHICHANG,YS_CAIWU from YS_COST_BUDGET where YS_CONTRACT_NO='" + editstate + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string shengchan = dt.Rows[0][0].ToString();
            string jishu = dt.Rows[0][1].ToString();
            string shichang = dt.Rows[0][2].ToString();
            string caiwu = dt.Rows[0][3].ToString();
            if ((shengchan == "0") && (jishu == "0") && (shichang == "0") && (caiwu == "0"))
            {
                retValue = "未完善";
            }
            else if ((shengchan == "1") && (jishu == "1") && (shichang == "1") && (caiwu == "1"))
            {
                retValue = "已完善";
            }
            else
            {
                retValue = "完善中";
            }
            return retValue;
        }

        /// <summary>
        /// 查询项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddl_project_OnSelectedIndexChanged(object sender, EventArgs e)
        {
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


        /// <summary>
        /// 勾选只查看我的审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckb_user_OnCheckedChanged(object sender, EventArgs e)
        {
            if (ckb_user.Checked)
            {
                pal.Enabled = false;
            }
            else
            {
                pal.Enabled = true;
            }
            UCPaging1.CurrentPage = 1;
            GetTechRequireData();
        }


        /// <summary>
        /// 跳转到预算明细页面
        /// </summary>
        /// <param name="operate">操作参数</param>
        /// <param name="contractno">合同号</param>
        /// <returns></returns>
        protected string GetEncodeUrl(string operate, string contractno)
        {
            string url = "";
            Encrypt_Decrypt ed = new Encrypt_Decrypt();
            url = "YS_Cost_Budget_Audit.aspx?action=" + ed.EncryptText(operate) + "&ContractNo=" + ed.EncryptText(contractno) + "&User=null";
            return url;
        }

    }
}
