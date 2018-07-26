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
using ZCZJ_DPF;
using System.Collections.Generic;

namespace ZCZJ_DPF.YS_Data.UI
{
    public partial class ys_task_budget_change : BasicPage
    {

        PagerQueryParam pager = new PagerQueryParam();
        YS_Data.BLL.ys_task_budget_list bll = new YS_Data.BLL.ys_task_budget_list();
        string contract_code;
        YS_Data.Model.TaskBudget tb;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            contract_code = Request.QueryString["contract_code"];
            //CheckUser(ControlFinder);


            initPager();
            initUCPaging();
            if (!IsPostBack)
            {
                initControl();
                UCPaging_PageChanged(1);
            }
            else
            {
                initDdl();
            }
        }



        #region 页面加载初始化代码
        /// <summary>
        /// 初始化页面查询对象
        /// </summary>
        private void initPager()
        {
            if (string.IsNullOrEmpty(contract_code))
            {
                //ddl_state.SelectedValue
                bll.initPager(pager, txt_task_code.Text.Trim(), txt_contract_code.Text.Trim(), txt_project_name.Text.Trim(), 5+"", rbl_myState.SelectedValue, Session["UserID"] + "");
            }
            else
            {
                bll.initPager(pager, null, contract_code, null, null, null, null);
            }
        }
        /// <summary>
        /// 初始化翻页控件
        /// </summary>
        private void initUCPaging()
        {
            UCPaging.PageChanged += new UCPaging.PageHandler(UCPaging_PageChanged);
            UCPaging.PageSize = pager.PageSize;
        }


        private void initControl()
        {
            if (string.IsNullOrEmpty(contract_code))
            {
                initDdl();
                pal_contract.Visible = false;
            }
            else
            {
                lb_contract_code.Text = contract_code;
                pal_search.Visible = false;
            }
        }
        /// <summary>
        /// 初始化状态下拉框
        /// </summary>
        private void initDdl()
        {
            string sqltext = "SELECT DISTINCT state AS DDLVALUE,case when state='1' then '初步预算'when state='2' then '部门反馈'when state='3' then '财务调整'when state='4' then '预算审核' when state='5' then '编制完成' end  AS DDLTEXT FROM ys_task_budget ORDER BY state";
            DBCallCommon.BindDdl(ddl_state, sqltext, "DDLTEXT", "DDLVALUE");
        }
        #endregion


        protected void rpt_task_list_databound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{

            //    HtmlTableCell cell1 = e.Item.FindControl("td_1") as HtmlTableCell;

            //    if (lab_status == "已确定变更")
            //    {
            //        td_12.BgColor = "Green";
            //    }
            //    // HtmlTableCell cell12 = e.Item.FindControl("td_12") as HtmlTableCell;
            //    if (ms_lookstatus.Text.ToString() == "1")
            //    {
            //        // lab_rownum.BackColor = System.Drawing.Color.Green;
            //        cell1.BgColor = "Green";
            //    }
            //    switch (msperson)
            //    {
            //        case "张骞": cell5.BgColor = "LightBlue";
            //            break;

            //        case "崔涛": cell5.BgColor = "LightGreen";
            //            break;
            //        case "吴红霞": cell5.BgColor = "Yellow";
            //            break;
            //    }
            //}

        }

        #region 操作事件处理程序

        protected void btn_search_Click(object sender, EventArgs e)
        {
            UCPaging_PageChanged(1);
        }

        /// <summary>
        /// 翻页事件处理程序
        /// </summary>
        /// <param name="i">判读是否用代码触发，如果i=1,则是用代码触发</param>
        private void UCPaging_PageChanged(int i)
        {
            switch (i)
            {
                case 1: pager.PageIndex = 1; break;
                default: pager.PageIndex = UCPaging.CurrentPage; break;
            }
            CommonFun.Paging(rpt_task_list, CommonFun.GetDataByPagerQueryParam(pager), UCPaging, pal_container, NoDataPanel);
        }
        //ItemCreated事件
        protected void Repeater1_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //判断Button是否存在
                if (e.Item.FindControl("Button1") != null)
                {
                    //如果存在，把对象转换为Button。
                    Button InsusButton = (Button)e.Item.FindControl("Button1");
                    //产生Click事件
                    InsusButton.Click += new EventHandler(InsusButton_Click);
                }
            }
        }

        //如何获取主键
        private void InsusButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            //判断HiddenField是否存在
            if (button.NamingContainer.FindControl("HiddenField1") != null)
            {
                //存在，把对象转换为HiddenField控件
                HiddenField hf = (HiddenField)button.NamingContainer.FindControl("HiddenField1");
                //取出HiddenField的Value值。
                //得到更新的预算任务号
                string task_code = hf.Value;
                tb = new YS_Data.Model.TaskBudget(task_code);



            }
        }
        //任务预算编制更新
        public void Repeater1_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "change")
            {
               // string task_code = ((LinkButton)sender).CommandArgument + "";//获取传递的任务号
                string task_code = e.CommandArgument + "";


              
                List<string> listsql = new List<string>();
                //listsql.Add(string.Format("DELETE FROM dbo.YS_TASK_BUDGET WHERE task_code='{0}';", tsaId));//删除任务预算表内的信息
                listsql.Add(string.Format("DELETE FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='{0}';", task_code));//删除历史表内的信息
                //listsql.Add(string.Format("DELETE FROM dbo.YS_NODE_INSTANCE WHERE task_code='{0}';", tsaId));//删除node实例表中的信息
                listsql.Add(getInsertIntoHistoryInfoTableSqltext(task_code));//向历史表重新插入信息
               // listsql.Add(getInsertIntoTaskBudgetTableSqltext(tsaId));//向任务预算表重新插入信息
                //listsql.Add(string.Format("UPDATE dbo.TBPM_TCTSASSGN SET TSA_BUDGET_DATETIME=GETDATE() WHERE TSA_ID='{0}';", tsaId));//更新任务表中预算提交的时间
                //DBCallCommon.ExecuteTrans(listsql);
                //listsql.Clear();
                //string[] ids = new string[] { ConfigurationSettings.AppSettings["BudgetEditorId"] };//配置文件中读取预算编制人id
                //BudgetFlowEngine.activeFollowNode(tsaId, "0", ids);//向node实例表中插入第一个实例
                //更新任务预算表
                string sqlText = " SELECT TOP 1 '"+task_code+"', CM_CONTR, CM_PROJ , TSA_ENGNAME ,( SELECT BM_TUTOTALWGHT FROM TBPM_STRINFODQO WHERE BM_ZONGXU='1' AND BM_ENGID='"+task_code+"'), ( SELECT ISNULL(SUM(c_total_cost),0) FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='"+task_code+"' AND material_code LIKE '01.07%'),( SELECT ISNULL(SUM(c_total_cost),0) FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='"+task_code+"' AND material_code LIKE '01.11%'),( SELECT ISNULL(SUM(c_total_cost),0) FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='"+task_code+"' AND material_code LIKE '01.15%'),( SELECT ISNULL(SUM(c_total_cost),0) FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='"+task_code+"' AND material_code LIKE '01.03%'),( SELECT ISNULL(SUM(c_total_cost),0) FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='"+task_code+"' AND (material_code LIKE '01.08%' OR material_code LIKE '01.09%')),( SELECT ISNULL(SUM(c_total_cost),0) FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='"+task_code+"' AND material_code NOT  LIKE '01.07%' AND material_code NOT  LIKE '01.11%' AND material_code NOT  LIKE '01.15%' AND material_code NOT  LIKE '01.03%' AND material_code NOT  LIKE '01.08%' AND material_code NOT  LIKE '01.09%'), GETDATE(), 1,( SELECT  TSA_CONTYPE FROM TBPM_TCTSASSGN WHERE TSA_ID='"+task_code+"' ) FROM  dbo.View_TM_TaskAssign  WHERE   TSA_ID = '"+task_code+"'" ;
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                ////任务预算编制更新的时间和人员以及更新的各种材料价格
                listsql.Add(string.Format("UPDATE dbo.YS_TASK_BUDGET SET task_weight= '{0}',ys_ferrous_metal_dep='{1}',ys_ferrous_metal='{1}', ys_purchase_part_dep='{2}',ys_purchase_part='{2}', ys_paint_coating_dep='{3}',ys_paint_coating='{3}',ys_electrical_dep='{4}', ys_electrical='{4}', ys_casting_forging_dep='{5}',ys_casting_forging_cost='{5}',ys_othermat_cost_dep='{6}',ys_othermat_cost='{6}',change_time=GETDATE(),change_name='" + Session["UserName"] + "' WHERE task_code='{7}';", dt.Rows[0][4].ToString(), dt.Rows[0][5].ToString(), dt.Rows[0][6].ToString(), dt.Rows[0][7].ToString(), dt.Rows[0][8].ToString(), dt.Rows[0][9].ToString(), dt.Rows[0][10].ToString(), task_code));
                DBCallCommon.ExecuteTrans(listsql);
                listsql.Clear();
                tb = new YS_Data.Model.TaskBudget(task_code);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交成功!');window.location='ys_task_budget_change.aspx'", true);

            }   
        }
        /// <summary>
        /// 获取插入到历史信息表的sql语句
        /// </summary>
        /// <param name="tsaId">任务号</param>
        /// <returns></returns>
        protected string getInsertIntoHistoryInfoTableSqltext(string tsaId)
        {
            return string.Format(@"INSERT   INTO dbo.YS_MATERIAL_HISTORY_INFO
                ( task_code ,
                  material_code ,
                  name ,
                  standard ,
                  quality ,
                  unit ,
                  amount ,
                  weight ,
                  unit_price
                )
                SELECT  '{0}' AS task_code ,
                        np.PUR_MARID ,
                        m.MNAME ,
                        m.GUIGE ,
                        m.CAIZHI ,
                        m.PURCUNIT ,
                        np.AMOUNT ,
                        m.MWEIGHT ,
                        np.SI_UPRICE
                FROM    ( SELECT    n.PUR_MARID ,
                                    n.AMOUNT ,
                                    p.SI_UPRICE
                          FROM      ( SELECT    PUR_MARID ,
                                                SUM(PUR_NUM) AS AMOUNT
                                      FROM      dbo.TBPC_PURCHASEPLAN
                                      WHERE     PUR_PCODE LIKE '{0}%'
                                      GROUP BY  PUR_MARID
                                    ) n
                                    LEFT JOIN ( SELECT  SI_MARID ,--从库存存货余额表中查询物料编码、最新单价
                                                        SI_UPRICE
                                                FROM    ( SELECT
                                                              SI_MARID ,--从库存存货余额表中查询物料的编码、单价，并按编码分组，且每种物料的最新单价nu='1'
                                                              SI_UPRICE ,
                                                              ROW_NUMBER() OVER ( PARTITION BY SI_MARID ORDER BY SI_MARID, SI_ID DESC ) AS rn
                                                          FROM
                                                              TBFM_STORAGEBAL
                                                          WHERE
                                                              SI_UPRICE > 0
                                                        ) t
                                                WHERE   t.rn = '1'
                                              ) p ON n.PUR_MARID = p.SI_MARID
                        ) np
                        LEFT JOIN dbo.TBMA_MATERIAL m ON np.PUR_MARID = m.ID
                ORDER BY np.PUR_MARID;
        UPDATE  dbo.YS_MATERIAL_HISTORY_INFO
        SET     weight = 1
        WHERE   task_code = '{0}'
                AND material_code NOT LIKE '01.08%'
                AND material_code NOT LIKE '01.09%';
        UPDATE  dbo.YS_MATERIAL_HISTORY_INFO SET unit_price = {1} WHERE task_code = '{0}' AND  material_code LIKE '01.08%';
        UPDATE  dbo.YS_MATERIAL_HISTORY_INFO SET unit_price = {2} WHERE task_code = '{0}' AND  material_code LIKE '01.09%';
        ", tsaId, ConfigurationSettings.AppSettings["UnitPriceOfCasting"], ConfigurationSettings.AppSettings["UnitPriceOfForging"]);
        }
        /// <summary>
        /// 获取插入到任务预算表的sql语句
        /// </summary>
        /// <param name="tsaId">任务号</param>
        /// <returns></returns>
        protected string getInsertIntoTaskBudgetTableSqltext(string tsaId)
        {
            return string.Format(@"INSERT  INTO dbo.YS_TASK_BUDGET
        ( task_code ,
          contract_code ,
          project_name ,
          equipment_name ,  
          task_weight,
          ys_ferrous_metal ,
          ys_purchase_part ,
          ys_paint_coating ,
          ys_electrical ,
          ys_casting_forging_cost ,
          ys_othermat_cost ,
          maker_id ,
          start_time ,
          state,
          task_type     
        )
        SELECT TOP 1
                '{0}' ,
                CM_CONTR,
                CM_PROJ ,
                TSA_ENGNAME , 
                ( SELECT BM_TUTOTALWGHT FROM TBPM_STRINFODQO WHERE BM_ZONGXU='1' AND BM_ENGID='{0}'),               
                ( SELECT ISNULL(SUM(c_total_cost),0) FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='{0}' AND material_code LIKE '01.07%') ,
                ( SELECT ISNULL(SUM(c_total_cost),0) FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='{0}' AND material_code LIKE '01.11%'),
                ( SELECT ISNULL(SUM(c_total_cost),0) FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='{0}' AND material_code LIKE '01.15%'),
                ( SELECT ISNULL(SUM(c_total_cost),0) FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='{0}' AND material_code LIKE '01.03%'),
                ( SELECT ISNULL(SUM(c_total_cost),0) FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='{0}' AND (material_code LIKE '01.08%' OR material_code LIKE '01.09%')),
                ( SELECT ISNULL(SUM(c_total_cost),0) FROM dbo.YS_MATERIAL_HISTORY_INFO WHERE task_code='{0}' AND material_code NOT  LIKE '01.07%' AND material_code NOT  LIKE '01.11%' 
                  AND material_code NOT  LIKE '01.15%' AND material_code NOT  LIKE '01.03%' AND material_code NOT  LIKE '01.08%' AND material_code NOT  LIKE '01.09%'),
                {1},
                GETDATE(),
                1,
                ( SELECT  TSA_CONTYPE FROM TBPM_TCTSASSGN WHERE TSA_ID='{0}' )
        FROM    dbo.View_TM_TaskAssign
        WHERE   TSA_ID = '{0}'", tsaId, Session["UserID"]);
        }
        //protected void rpt_task_list_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        //if ((e.Item.ItemIndex + 1) % 2 == 0)

        //        if (((HtmlTableCell)e.Item.FindControl("td_state")).InnerText.Equals("初步预算"))
        //        {
        //            ((HtmlTableCell)e.Item.FindControl("td_state")).BgColor = "#f00";
        //        }


        //    }

        //}

        #endregion


    }
}
