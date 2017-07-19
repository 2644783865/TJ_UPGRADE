using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_Kaipiao_List : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        List<string> str = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                //2016.8.25修改
                string contract_task_view = Request["contract_task_view"];
                if (!string.IsNullOrEmpty(contract_task_view))
                {
                    rblState.SelectedValue = "8";
                    txtContract.Text = contract_task_view.Trim();
                }
                UserID.Value = Session["UserID"].ToString();
                this.GetBoundData();
            }
            CheckUser(ControlFinder);
        }

        #region 分页
        protected void GetBoundData()
        {
            InitPager();
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
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

        private void InitPager()
        {
            pager.TableName = "(select * from CM_KAIPIAO as d left join (select a.cId , stuff((select sprId+',' from CM_KAIPIAO_HUISHEN b where b.cId =a.cId and( b.result is null or b.result='') for xml path('')),1,0,',') 'sprId ' from CM_KAIPIAO_HUISHEN  a  group by  a.cId)c on d.KP_TaskID=c.cId)e";
            pager.PrimaryKey = "Id";
            pager.ShowFields = "*";
            pager.OrderField = "KP_ZDTIME";
            pager.StrWhere = CreateConStr(1);
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 10;
            UCPaging1.PageSize = pager.PageSize;
        }

        private string CreateConStr(int type)
        {
            string sql = " 1=1 ";
            string userID = UserID.Value;
            if (rblState.SelectedValue == "0")
            {
                sql += "and KP_ZONGSTATE='0'";
            }
            else if (rblState.SelectedValue == "1")
            {
                sql += "and KP_ZONGSTATE='1'";
            }
            else if (rblState.SelectedValue == "2")
            {
                sql += "and KP_ZONGSTATE='2'";
            }
            else if (rblState.SelectedValue == "3")
            {
                sql += "and KP_ZONGSTATE='1' and KP_SPSTATE='3'";
            }
            else if (rblState.SelectedValue == "4")
            {
                sql += "and ((KP_SPSTATE='1' and KP_SHRIDA='" + Session["UserID"].ToString() + "') or (KP_SPSTATE='2' and KP_SHRIDB='" + Session["UserID"].ToString() + "') or (KP_HSSTATE='1' and sprId like '%," + Session["UserID"].ToString() + ",%'))";
            }
            else if (rblState.SelectedValue == "5")
            {
                sql += "and KP_ZONGSTATE='3'";
            }
            else if (rblState.SelectedValue == "6")
            {
                sql += "and KP_KPNUMBER is not null";
            }
            else if (rblState.SelectedValue == "7")
            {
                sql += "and ((KP_SPSTATE='3' and KP_TIQIANKP='1' and KP_KPNUMBER is null) or (KP_ZONGSTATE='2' and KP_TIQIANKP='0' and KP_KPNUMBER is null))";
            }
            if (txtBH.Text.Trim() != "")
            {
                sql += " and KP_CODE like '%" + txtBH.Text.Trim() + "%'";
            }
            if (txtContract.Text != "")
            {
                sql += " and KP_CONID like '%" + txtContract.Text.Trim() + "%'";
            }

            if (txtStart.Text != "")
            {
                sql += " and KP_KPDATE > '" + txtStart.Text.Trim() + "'";
            }

            if (txtEnd.Text != "")
            {
                sql += " and KP_KPDATE < '" + txtEnd.Text.Trim() + "'";
            }

            return sql;
        }

        void Pager_PageChanged(int pageNumber)
        {

            GetBoundData();
        }

        private void ControlVisible()
        {
            if (rblState.SelectedValue == "0")
            {
                GridView1.Columns[15].Visible = true;
                GridView1.Columns[16].Visible = false;
                GridView1.Columns[17].Visible = false;
                GridView1.Columns[18].Visible = false;
            }
            else if (rblState.SelectedValue == "1")
            {
                GridView1.Columns[15].Visible = false;
                GridView1.Columns[16].Visible = false;
                GridView1.Columns[17].Visible = false;
                GridView1.Columns[18].Visible = false;
            }
            else if (rblState.SelectedValue == "2")
            {
                GridView1.Columns[15].Visible = false;
                GridView1.Columns[16].Visible = false;
                GridView1.Columns[17].Visible = true;
                GridView1.Columns[18].Visible = false;
            }
            else if (rblState.SelectedValue == "3")
            {
                GridView1.Columns[15].Visible = false;
                GridView1.Columns[16].Visible = false;
                GridView1.Columns[17].Visible = true;
                GridView1.Columns[18].Visible = false;
            }
            else if (rblState.SelectedValue == "4")
            {
                GridView1.Columns[15].Visible = false;
                GridView1.Columns[16].Visible = true;
                GridView1.Columns[17].Visible = false;
                GridView1.Columns[18].Visible = false;
            }
            else if (rblState.SelectedValue == "5")
            {
                GridView1.Columns[15].Visible = false;
                GridView1.Columns[16].Visible = false;
                GridView1.Columns[17].Visible = false;
                GridView1.Columns[18].Visible = true;
            }

        }

        #endregion

        protected void lnkReset_OnClick(object sender, EventArgs e)
        {
            string KP_TaskID = ((LinkButton)sender).CommandArgument.ToString();



            string sqltext = "update CM_KAIPIAO set KP_SPSTATE='0',KP_HSSTATE='0',KP_ZONGSTATE='0' where KP_TaskID='" + KP_TaskID + "'";//基本表
            DBCallCommon.ExeSqlText(sqltext);
            this.GetBoundData();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已重置！！！');", true);

        }

        protected void rblState_IndexChanged(object sender, EventArgs e)
        {
            ControlVisible();
            this.GetBoundData();
        }

        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            string KP_TaskID = ((LinkButton)sender).CommandArgument.ToString();


            List<string> list_sql = new List<string>();
            string sqltext = "delete from CM_KAIPIAO where KP_TaskID='" + KP_TaskID + "'";//基本表
            list_sql.Add(sqltext);
            sqltext = "delete from CM_KAIPIAO_DETAIL where cId='" + KP_TaskID + "'";//详细表
            list_sql.Add(sqltext);
            sqltext = "delete from CM_KAIPIAO_HUISHEN where cId='" + KP_TaskID + "'";//会审表
            list_sql.Add(sqltext);
            DBCallCommon.ExecuteTrans(list_sql);
            this.GetBoundData();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已删除！！！');", true);

        }


        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                string sql = "select sum(kpmoney) from (select e.*,f.kpmoney from (select * from CM_KAIPIAO as d left join (select a.cId , stuff((select sprId+',' from CM_KAIPIAO_HUISHEN b where b.cId =a.cId and( b.result is null or b.result='') for xml path('')),1,0,',') 'sprId ' from CM_KAIPIAO_HUISHEN  a  group by  a.cId)c on d.KP_TaskID=c.cId)e left join  (select sum(cast(kpmoney as float)) as kpmoney,cId from dbo.CM_KAIPIAO_DETAIL group by cid)f on e.cId=f.cId)g  where " + CreateConStr(1);
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    e.Row.Cells[3].Text = "开票总计:" + dt.Rows[0][0].ToString();
                }
            }
        }

    }
}
