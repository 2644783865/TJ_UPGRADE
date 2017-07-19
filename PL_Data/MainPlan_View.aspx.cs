using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.IO;

namespace ZCZJ_DPF.PL_Data
{
    public partial class MainPlan_View : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        string sqlText;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitPage();
                GridView1.Columns[15].Visible = false;
            }
            UCPaging.PageChanged += new UCPaging.PageHandler(Pager_PageChangedMS);
        }
        private void InitPage()
        {

            UCPaging.CurrentPage = 1;
            this.InitPager();
            bindGrid();
        }
        private void InitPager()
        {
            //MP_CODE, MP_PJID, MP_PJNAME, MP_ENGNAME, MP_NOTE, MP_STATE
            pager.TableName = "VIEW_TBMP_MAINPLANTOTAL";
            pager.PrimaryKey = "MP_CODE";
            pager.ShowFields = "*";
            pager.OrderField = "MP_CODE";
            pager.StrWhere = ConstrWhere();
            pager.OrderType = 1;//升序排列
            pager.PageSize = 20;
            UCPaging.PageSize = pager.PageSize;    //每页显示的记录数
        }

        private string ConstrWhere()
        {
            string conStr = " MP_STATE = " + rblstatus.SelectedValue;
            return conStr;
        }
        private void bindGrid()
        {

            pager.PageIndex = UCPaging.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, GridView1, UCPaging, NoDataPanel);

            if (NoDataPanel.Visible)
            {
                UCPaging.Visible = false;

            }
            else
            {
                UCPaging.Visible = true;
                UCPaging.InitPageInfo();  //分页控件中要显示的控件

            }

        }
        private void Pager_PageChangedMS(int pageNumber)
        {
            this.InitPager();
            bindGrid();
        }
        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            string operate = ((LinkButton)sender).CommandName;
            string tsaId = ((LinkButton)sender).CommandArgument;
            string state = "";
            switch (operate)
            {
                case "Del":
                    state = "3";
                    break;
                case "Com":
                    state = "2";
                    break;
                case "Res":
                    state = "0";
                    break;
            }
            sqlText = "update TBMP_MAINPLANTOTAL set MP_STATE='" + state + "' where MP_CODE='" + tsaId + "'";
            try
            {
                DBCallCommon.ExeSqlText(sqlText);
                Response.Write("<script>alert('操作成功')</script>");
                this.InitPage();
            }
            catch (Exception)
            {

                Response.Write("<script>alert('程序出错，请联系管理员')</script>");
            }

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string taskId = e.Row.Cells[1].Text;
                string state = ((HtmlInputHidden)e.Row.FindControl("hidState")).Value.Trim();

                if (state == "1" || state == "2")
                {
                    e.Row.Attributes["style"] = "Cursor:hand";
                    e.Row.Attributes.Add("title", "双击查看具体进度情况");
                    e.Row.Attributes.Add("ondblclick", "Show('" + taskId + "')");

                    #region 查询进度

                    sqlText = "select MP_ENDTIME,MP_STATE,MP_ACTURALTIME,MP_WARNINGDAYS,MP_DAYS from TBMP_MAINPLANDETAIL where MP_ENGID='" + taskId + "' and MP_TYPE='技术准备' ";
                    ChangeTextAndBackColor(e, sqlText, 5);

                    sqlText = "select MP_ENDTIME,MP_STATE,MP_ACTURALTIME,MP_WARNINGDAYS,MP_DAYS from TBMP_MAINPLANDETAIL where MP_ENGID='" + taskId + "' and MP_TYPE='采购周期'";

                    ChangeTextAndBackColor(e, sqlText, 6);
                    sqlText = "select MP_ENDTIME,MP_STATE,MP_ACTURALTIME,MP_WARNINGDAYS,MP_DAYS from TBMP_MAINPLANDETAIL where MP_ENGID='" + taskId + "' and MP_TYPE='生产周期' ";
                    ChangeTextAndBackColor(e, sqlText, 7);
                    #endregion
                }

            }
        }

        private void ChangeTextAndBackColor(GridViewRowEventArgs e, string sqlText, int i)
        {

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            string techPrepare = CheckState(dt);
            if (techPrepare.Contains("6"))
            {
                e.Row.Cells[i].Text = "已超期";
                e.Row.Cells[i].BackColor = Color.OrangeRed;
            }
            else if (techPrepare.Contains("5"))
            {
                e.Row.Cells[i].Text = "预警中";
                e.Row.Cells[i].BackColor = Color.Yellow;
            }
            else if (techPrepare.Contains("4"))
            {
                e.Row.Cells[i].Text = "进行中";
                e.Row.Cells[i].BackColor = Color.LawnGreen;
            }
            else if (techPrepare.Contains("3"))
            {
                e.Row.Cells[i].Text = "未开始";
                e.Row.Cells[i].BackColor = Color.LawnGreen;
            }
            else if (techPrepare.Contains("2"))
            {
                e.Row.Cells[i].Text = "超期完成";
                e.Row.Cells[i].BackColor = Color.OrangeRed;
            }
            else if (techPrepare.Contains("1"))
            {
                e.Row.Cells[i].Text = "顺利完成";
                e.Row.Cells[i].BackColor = Color.LawnGreen;
            }
        }

        private string CheckState(DataTable dt1)
        {
            string result = "0";
            for (int i = 0; i < dt1.Rows.Count; i++)
            {

                DateTime endtime = new DateTime();
                if ( DateTime.TryParse(dt1.Rows[i][0].ToString(),out endtime))
                {
                  string state = dt1.Rows[i][1].ToString();
                  int warningdays = Convert.ToInt32(dt1.Rows[i][3]);
                     int plandays = Convert.ToInt32(dt1.Rows[i][4]);
                  string actural = dt1.Rows[i][2].ToString();
                  TimeSpan span = endtime - DateTime.Now;
                  if (state == "2")
                  {
                      try
                      {
                          DateTime a = Convert.ToDateTime(actural);
                          TimeSpan span1 = endtime - a;

                          if (span1.TotalDays >= 0)
                          {
                              result += "1";//顺利完成
                          }
                          else
                          {
                              result += "2";//超期完成
                          }
                      }
                      catch (Exception)
                      {
                          result += "2";
                          throw;
                      }
                  }
                  else
                  {
                      if (plandays!=0)
                      {
                          if (state == "0" && span.TotalDays > warningdays)
                          {
                              result += "3";//未开始
                          }
                          else if (state == "1" && span.TotalDays > warningdays)
                          {
                              result += "4";//正在进行中
                          }
                          else if (span.TotalDays <= warningdays && span.TotalDays >= -1)
                          {
                              result += "5";//预警中
                          }
                          else if (span.TotalDays < -1)
                          {
                              result += "6";//超期中              
                          }
                      }
                      else
                      {
                          result += "0";
                      }
                  }
                }
            }
            return result;
        }
        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.InitPage();
            if (rblstatus.SelectedValue == "0")
            {
                GridView1.Columns[12].Visible = true;
                GridView1.Columns[13].Visible = true;
                GridView1.Columns[14].Visible = true;
                GridView1.Columns[15].Visible = false;
            }
            else if (rblstatus.SelectedValue == "1")
            {
                GridView1.Columns[12].Visible = true;
                GridView1.Columns[13].Visible = true;
                GridView1.Columns[14].Visible = true;
                GridView1.Columns[15].Visible = false;
            }
            else if (rblstatus.SelectedValue == "2")
            {
                GridView1.Columns[12].Visible = false;
                GridView1.Columns[13].Visible = false;
                GridView1.Columns[14].Visible = false;
                GridView1.Columns[15].Visible = false;
            }
            else if (rblstatus.SelectedValue == "3")
            {
                GridView1.Columns[12].Visible = false;
                GridView1.Columns[13].Visible = false;
                GridView1.Columns[14].Visible = false;
                GridView1.Columns[15].Visible = true;
            }
            else
            {
                GridView1.Columns[12].Visible = false;
                GridView1.Columns[13].Visible = false;
                GridView1.Columns[14].Visible = false;
                GridView1.Columns[15].Visible = false;
            }
        }
    }
}
