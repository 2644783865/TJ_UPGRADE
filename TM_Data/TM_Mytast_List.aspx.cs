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
using ZCZJ_DPF;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Mytast_List : BasicPage
    {
        string sqlText = "";
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitInfo();
                this.GetProNameData();
            }
            this.InitVar();
            CheckUser(ControlFinder);
        }

        private string GetMyTast()
        {
            string sql = "";
            sql += " TSA_TCCLERKNM ='" + Session["UserName"] + "' ";


            if (rblstatus.SelectedIndex == 0)
            {
                sql += " and TSA_STATE in(" + rblstatus.SelectedValue + ")";
            }
            else
            {
                sql += " and TSA_STATE='" + rblstatus.SelectedValue + "'";
            }
            //项目
            if (ddlProName.SelectedIndex != 0 && ddlProName.SelectedIndex != -1)
            {
                sql += " and TSA_PJID='" + ddlProName.SelectedValue.ToString() + "'";
            }
            //工程
            if (ddlEngName.SelectedIndex != 0 && ddlEngName.SelectedIndex != -1)
            {
                sql += " and TSA_ID='" + ddlEngName.SelectedValue.ToString() + "' OR TSA_ID LIKE '" + ddlEngName.SelectedValue + "-%'";
            }
            return sql;
        }


        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            this.GetBoundData();
        }

        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            this.GetBoundData();
        }

        //protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        string sczh = e.Row.Cells[1].Text.Trim();

        //        if (e.Row.Cells[1].Text.Contains("-"))
        //        {
        //            ((HyperLink)e.Row.FindControl("hlTask1")).Visible = true;
        //            ((HyperLink)e.Row.FindControl("hlTask2")).Visible = true;
        //            ((HyperLink)e.Row.FindControl("hlTask3")).Visible = true;
        //            ((HyperLink)e.Row.FindControl("hlTask4")).Visible = false;
        //            ((HyperLink)e.Row.FindControl("hlTask5")).Visible = true;
        //            ((HyperLink)e.Row.FindControl("hlTask6")).Visible = true;
        //            ((HyperLink)e.Row.FindControl("hlTask7")).Visible = false;

        //            this.SignReject(e.Row);

        //            e.Row.Attributes.Add("ondblclick", "ShowAllOrg('" + sczh + "')");
        //            e.Row.Attributes["style"] = "Cursor:hand";
        //            e.Row.Attributes.Add("title", "双击进入查看界面");
        //        }
        //        else
        //        {
        //            ((HyperLink)e.Row.FindControl("hlTask1")).Visible = false;
        //            ((HyperLink)e.Row.FindControl("hlTask2")).Visible = false;
        //            ((HyperLink)e.Row.FindControl("hlTask3")).Visible = false;
        //            ((HyperLink)e.Row.FindControl("hlTask4")).Visible = false;
        //            ((HyperLink)e.Row.FindControl("hlTask5")).Visible = false;
        //            ((HyperLink)e.Row.FindControl("hlTask6")).Visible = false;
        //            ((HyperLink)e.Row.FindControl("hlTask7")).Visible = true;
        //            //为总项添加查看原始数据界面
        //            string xm = sczh.Split('/')[1];
        //            string xm_gc = Server.HtmlEncode(xm + "_" + sczh);

        //            e.Row.Attributes.Add("ondblclick", "ShowAllData('" + xm_gc + "')");
        //            e.Row.Attributes["style"] = "Cursor:hand";
        //            for (int i = 0; i < 7; i++)
        //            {
        //                e.Row.Cells[i].Attributes.Add("title", "双击查看原始数据");
        //            }
        //        }
        //    }
        //}
        protected void SignReject(GridViewRow grow)
        {
            string taskid = grow.Cells[1].Text.Trim();
            //材料计划驳回
            string mp_reject = "select count(MP_STATE) from View_TM_MPFORALLRVW where MP_ENGID='" + taskid + "' and  MP_STATE in('3','5','7')";
            string mp_reject_bg = "select count(MP_STATE) from View_TM_MPCHANGERVW where MP_ENGID='" + taskid + "' and  MP_STATE in('3','5','7')";
            int num_mp = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(mp_reject).Rows[0][0].ToString());
            int num_mp_bg = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(mp_reject_bg).Rows[0][0].ToString());
            if (num_mp != 0 || num_mp_bg != 0)
            {
                ((Label)grow.FindControl("lblMP")).Text = "(" + num_mp + ") (" + num_mp_bg + ")";
            }

            //执行明细驳回
            string ms_reject = "select COUNT(MS_STATE) from TBPM_MSFORALLRVW where MS_ENGID='" + taskid + "' AND MS_STATE in('3','5','7')";
            string ms_reject_bg = "select COUNT(MS_STATE) from TBPM_MSCHANGERVW where MS_ENGID='" + taskid + "' AND MS_STATE in('3','5','7')";
            int num_ms = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(ms_reject).Rows[0][0].ToString());
            int num_ms_bg = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(ms_reject_bg).Rows[0][0].ToString());
            if (num_ms != 0 || num_ms_bg != 0)
            {
                ((Label)grow.FindControl("lblMS")).Text = "(" + num_ms + ") (" + num_ms_bg + ")";
            }

            //外协驳回
            string out_reject = "select COUNT(OST_STATE) from TBPM_OUTSOURCETOTAL where OST_ENGID='" + taskid + "' AND OST_STATE in('3','5','7')";
            string out_reject_bg = "select COUNT(OST_STATE) from TBPM_OUTSCHANGERVW where OST_ENGID='" + taskid + "' AND OST_STATE in('3','5','7')";
            int num_out = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(out_reject).Rows[0][0].ToString());
            int num_out_bg = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(out_reject_bg).Rows[0][0].ToString());
            if (num_out != 0 || num_out_bg != 0)
            {
                ((Label)grow.FindControl("lblOUT")).Text = "(" + num_out + ") (" + num_out_bg + ")";
            }

            //油漆方案
            string paint_reject = "select COUNT(PS_STATE) from TBPM_PAINTSCHEME where PS_ENGID='" + taskid + "' AND PS_STATE in('3','5','7')";
            string paint_reject_bg = "select COUNT(PS_STATE) from TBPM_PAINTSCHEME where PS_ENGID='" + taskid + "' AND PS_STATE in('3','5','7')";
            int num_paint = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(paint_reject).Rows[0][0].ToString());
            int num_paint_bg = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(paint_reject_bg).Rows[0][0].ToString());
            if (num_paint != 0 || num_paint_bg != 0)
            {
                ((Label)grow.FindControl("lblPAINT")).Text = "(" + num_paint + ") (" + num_paint_bg + ")";
            }
        }

        /// <summary>
        /// 绑定合同号
        /// </summary>
        private void GetProNameData()
        {
            sqlText = "select distinct TSA_PJID from View_TM_TaskAssign where TSA_TCCLERKNM='" + Session["UserName"].ToString() + "' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlProName.DataSource = dt;
            ddlProName.DataTextField = "TSA_PJID";
            ddlProName.DataValueField = "TSA_PJID";
            ddlProName.DataBind();
            ddlProName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlProName.SelectedIndex = 0;
        }
        /// <summary>
        /// 绑定设备名称
        /// </summary>
        private void GetEngNameData()
        {
            sqlText = "select TSA_ID,TSA_ID+'‖'+TSA_ENGNAME as TSA_ENGNAME from View_TM_TaskAssign ";
            sqlText += "where TSA_PJID='" + ddlProName.SelectedValue + "' and TSA_TCCLERKNM='" + Session["UserName"].ToString() + "' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlEngName.DataSource = dt;
            ddlEngName.DataTextField = "TSA_ENGNAME";
            ddlEngName.DataValueField = "TSA_ID";
            ddlEngName.DataBind();
            ddlEngName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlEngName.SelectedIndex = 0;
        }
        /// <summary>
        /// 项目名称改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlProName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetEngNameData();
            UCPaging1.CurrentPage = 1;
            InitVar();
            this.GetBoundData();
        }
        /// <summary>
        /// 工程名称改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlEngName_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
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
            pager.TableName = "View_TM_TaskAssign";
            pager.PrimaryKey = "TSA_ID";
            pager.ShowFields = "TSA_ID,CM_PROJ,TSA_PJID,TSA_ENGNAME,TSA_TCCLERKNM,TSA_STARTDATE,TSA_MANCLERKNAME,TSA_STATE,TSA_CONTYPE,ID,TSA_FINISHSTATE";
            pager.OrderField = "TSA_STARTDATE";
            pager.StrWhere = this.GetMyTast();
            pager.OrderType = 1;
            pager.PageSize = 10;
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
        /// 技术准备完成按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void lbtn_Finish_onclick(object sender, EventArgs e)
        {
            string tsaId = "";//任务号
            LinkButton finish = (LinkButton)sender;
            tsaId = finish.CommandArgument.ToString();//获取传递的任务号


            //检查预算主表中是否有重复任务号，如果没有重复任务号向预算主表插入数据，再成功后向预算明细表插入数据

            if (!TsaRepeatInMainTable(tsaId))
            {
                if (InsertIntoYSMainTableSuccessed(tsaId))
                {
                    InsertIntoYSDetailTable(tsaId);

                    string sqltext = "update TBPM_TCTSASSGN set TSA_FINISHSTATE ='1' where TSA_ID='" + tsaId + "'";
                    DBCallCommon.ExeSqlTextGetInt(sqltext);

                    finish.Visible = false;
                    ((Image)finish.Parent.FindControl("img_Finish")).Visible = true;
                    ((Label)finish.Parent.FindControl("lb_Finish")).Visible = true;

                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script>alert('技术准备状态更改完成，提交预算成功');</script>");

                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script>alert('预算提交失败');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "名称", "<script>alert('预算主表中已经有该任务号的预算编制，请勿重复提交');</script>");
                
                string sqltext = "update TBPM_TCTSASSGN set TSA_FINISHSTATE ='1' where TSA_ID='" + tsaId + "'";
                DBCallCommon.ExeSqlTextGetInt(sqltext);

                finish.Visible = false;
                ((Image)finish.Parent.FindControl("img_Finish")).Visible = true;
                ((Label)finish.Parent.FindControl("lb_Finish")).Visible = true;
            }
        }
















        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //判断技术准备是否已经完成，如果是则显示“√”图片，否则显示“点击完成”按钮
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string finish = ((HiddenField)e.Row.FindControl("hid_Finish")).Value.ToString();
                if (finish.Equals("1"))
                {
                    ((LinkButton)e.Row.FindControl("lbtn_Finish")).Visible = false;
                    ((Image)e.Row.FindControl("img_Finish")).Visible = true;
                    ((Label)e.Row.FindControl("lb_Finish")).Visible = true;
                }
            }
        }


        /// <summary>
        /// 检查预算主表中是否已经有该任务号
        /// </summary>
        /// <param name="tsaId">任务号</param>
        /// <returns>true：预算主表中已经有该任务号；false：预算主表中没有给任务号</returns>
        protected bool TsaRepeatInMainTable(string tsaId)
        {
            string sqlCheckMain = "SELECT YS_TSA_ID FROM dbo.YS_COST_BUDGET WHERE YS_TSA_ID='" + tsaId + "'";
            SqlDataReader drCheckMain = DBCallCommon.GetDRUsingSqlText(sqlCheckMain);
            if (drCheckMain.HasRows)
            {
                drCheckMain.Close();
                return true;
            }
            else
            {
                drCheckMain.Close();
                return false;
            }
        }



        /// <summary>
        /// 将某个任务号的预算信息插入到预算主表中
        /// </summary>
        /// <param name="tsaId">任务号</param>
        /// <returns>true：插入成功；fals：插入失败</returns>
        protected bool InsertIntoYSMainTableSuccessed(string tsaId)
        {
            string contractNo = "", projectName = "", engineerName = "";//合同号，项目名称，设备名称
            decimal budgetIncome = 0, transCost = 0;//任务号预算收入


            #region 获取任务号的相关信息（合同号，项目名称，设备名称）
            string sqlInfo = "SELECT TSA_ID,CM_PROJ,TSA_PJID,TSA_ENGNAME FROM dbo.View_TM_TaskAssign WHERE TSA_ID='" + tsaId + "'";
            SqlDataReader drInfo = DBCallCommon.GetDRUsingSqlText(sqlInfo);
            if (drInfo.Read())
            {
                contractNo = drInfo["TSA_PJID"].ToString();
                projectName = drInfo["CM_PROJ"].ToString();
                engineerName = drInfo["TSA_ENGNAME"].ToString();
            }
            drInfo.Close();
            #endregion



            #region 获取任务号的预算收入
            string sqlMoney = " SELECT * FROM  (SELECT a.TSA_ID, SUM(a.CM_COUNT)*10000/1.17 AS BUDGET_INCOME FROM dbo.TBPM_DETAIL AS a GROUP BY TSA_ID)b  WHERE b.TSA_ID='" + tsaId + "'";
            SqlDataReader drMoney = DBCallCommon.GetDRUsingSqlText(sqlMoney);
            if (drMoney.Read())
            {
                try
                {
                    budgetIncome = Convert.ToDecimal(drMoney["BUDGET_INCOME"]);
                }
                catch
                {
                    budgetIncome = 0;
                }
            }
            drMoney.Close();
            #endregion

            #region 获取合同的预计运费
            string sqlTrans = "SELECT TSA_Trans_Cost FROM dbo.TBPM_CONPCHSINFO WHERE PCON_BCODE='" + contractNo + "'";
            SqlDataReader drTrans = DBCallCommon.GetDRUsingSqlText(sqlTrans);
            if (drTrans.Read())
            {
                try
                {
                    transCost = Convert.ToDecimal(drTrans["TSA_Trans_Cost"]);
                }
                catch
                {

                    transCost = 0;
                }
            }
            #endregion


            #region 插入主表
            string sqlInsertMain = @"INSERT  INTO dbo.YS_COST_BUDGET
        ( YS_CONTRACT_NO ,
          YS_PROJECTNAME ,
          YS_TSA_ID ,
          YS_ENGINEERNAME ,
          YS_BUDGET_INCOME ,
YS_TRANS_COST,
          YS_CAIWU ,
          YS_CAIGOU ,
          YS_SHENGCHAN ,
          YS_STATE ,
          YS_FIRST_REVSTATE ,
          YS_SECOND_REVSTATE ,
          YS_REVSTATE ,
          YS_TEC_SUBMIT_NAME ,
          YS_ADDTIME ,
          YS_ADDFINISHTIME 
        )
VALUES  ( '" + contractNo + "' , '" + projectName + "' , '" + tsaId + "' , '" + engineerName + "' , '" + budgetIncome + "' , '" + transCost + @"' , 
          '0' , -- YS_CAIWU - char(1)
          '0' , -- YS_CAIGOU - char(1)
          '0' , -- YS_SHENGCHAN - char(1)
          '0' , -- YS_STATE - char(1)
          '0' , -- YS_FIRST_REVSTATE - char(1)
          '0' , -- YS_SECOND_REVSTATE - char(1)
          '0' , -- YS_REVSTATE - char(1)
          '" + Session["UserName"] + @"' , -- YS_TEC_SUBMIT_NAME 技术提交人姓名 - varchar(50)
          GETDATE() , --技术提交时间
          DATEADD(ww, 2, GETDATE()) --预算编制截止时间
        )";
            if (DBCallCommon.ExeSqlTextGetInt(sqlInsertMain) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            #endregion


        }


        /// <summary>
        /// 将某个任务号的预算信息插入到预算明细表中
        /// </summary>
        /// <param name="tsaId">任务号</param>
        /// <returns>true：插入成功；fals：插入失败</returns>
        protected void InsertIntoYSDetailTable(string tsaId)
        {
            string sqlInsertDetail = @"INSERT  INTO dbo.YS_COST_BUDGET_DETAIL
        ( YS_TSA_ID ,
          YS_CODE ,
          YS_NAME ,
          YS_Union_Amount ,
          YS_Average_Price,
          YS_Average_Price_FB 
        )
        SELECT  '" + tsaId + @"' AS YS_TSA_ID ,
                pp.SI_MARID ,
                pp.MNAME ,
                n.AMOUNT ,
                pp.SI_UPRICE,
                pp.SI_UPRICE
        FROM    ( SELECT    PO_MARID ,
                            SUM(PO_QUANTITY) AS AMOUNT
                  FROM      dbo.TBPC_PURORDERDETAIL
                  WHERE     PO_PCODE LIKE '%" + tsaId + @"%'
                  GROUP BY  PO_MARID
                ) n
                LEFT JOIN ( SELECT SI_MARID ,
                                    MNAME ,
                                    SI_UPRICE
                             FROM   ( SELECT    SI_MARID ,
                                                SI_UPRICE
                                      FROM      ( SELECT    SI_MARID ,
                                                            SI_UPRICE ,
                                                            ROW_NUMBER() OVER ( PARTITION BY SI_MARID ORDER BY SI_MARID, SI_ID DESC ) AS ccc
                                                  FROM      TBFM_STORAGEBAL
                                                  WHERE     SI_UPRICE > 0
                                                ) t
                                      WHERE     t.ccc = '1'
                                    ) p
                                    LEFT JOIN TBMA_MATERIAL ON p.SI_MARID = TBMA_MATERIAL.ID
                           ) pp ON pp.SI_MARID = n.PO_MARID ORDER BY pp.SI_MARID";

            DBCallCommon.ExeSqlText(sqlInsertDetail);
           
        }

    }
}
