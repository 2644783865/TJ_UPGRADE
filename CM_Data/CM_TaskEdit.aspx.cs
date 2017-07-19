using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_TaskEdit : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        List<string> str = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
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

        private void InitPager()
        {
            pager.TableName = "View_CM_Task";
            pager.PrimaryKey = "TSA_ID";
            pager.ShowFields = "*";
            pager.OrderField = "CM_CONTR";
            pager.StrWhere = CreateConStr();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 10;
            UCPaging1.PageSize = pager.PageSize;
        }

        private string CreateConStr()
        {
            string sql = string.Empty;
            string status = rbl_status.SelectedValue;
            sql = "CM_SPSTATUS='" + status + "' and CM_TASKTYPE='0' ";
            switch (ddlSearch.SelectedValue)
            {
                case "1":
                    sql += "and CM_CONTR LIKE '%" + searchcontent.Text.Trim() + "%'";
                    break;
                case "2":
                    sql += "and TSA_ENGNAME LIKE '%" + searchcontent.Text.Trim() + "%'";
                    break;
                case "3":
                    sql += "and CM_PROJ LIKE '%" + searchcontent.Text.Trim() + "%'";
                    break;
                case "4":
                    sql += "and TSA_MAP LIKE '%" + searchcontent.Text.Trim() + "%'";
                    break;
            }
            return sql;
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetBoundData();
        }

        #endregion

        protected void rbl_mytask_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetBoundData();
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetBoundData();
        }

        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.ToString() == "DataRow")
            {
                #region MyRegion

                string id = ((HiddenField)e.Row.FindControl("id")).Value;
                string CM_CANCEL = ((HiddenField)e.Row.FindControl("cancel")).Value;
                if (CM_CANCEL=="1")
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                }
                if (str.Count < 1)
                {
                    str.Add(id);
                    str.Add(e.Row.Cells[3].Text);
                }
                else
                {
                    if (str.Contains(id))
                    {
                        e.Row.Cells[1].Text = "";//合并
                        e.Row.Cells[2].Text = "";
                        e.Row.Cells[4].Text = "";
                        e.Row.Cells[5].Text = "";
                        e.Row.Cells[10].Text = "";
                        e.Row.Cells[11].Text = "";//制单日期
                        e.Row.Cells[12].Text = "";//合并制单人
                        e.Row.Cells[13].Text = "";//合并审核状态
                        e.Row.Cells[14].Text = "";//合并查看和评审
                        e.Row.Cells[15].Text = "";
                        e.Row.Cells[16].Text = "";
                        e.Row.Cells[17].Text = "";
                        if (str.Contains(e.Row.Cells[3].Text))
                        {
                            e.Row.Cells[3].Text = "";
                        }
                        else
                        {
                            str.Add(e.Row.Cells[3].Text);
                        }
                    }
                    else
                    {
                        str.Add(id);
                        str.Add(e.Row.Cells[3].Text);
                    }
                }

                #endregion

                DataRowView drv = (DataRowView)e.Row.DataItem;
                if (drv["CM_SPSTATUS"].ToString() != "2")
                {
                    e.Row.Cells[15].Text = "";
                    e.Row.Cells[16].Text = "";
                    e.Row.Cells[17].Text = "";
                }
            }
        }
    }
}
