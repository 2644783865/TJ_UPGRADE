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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_Date_historypriceshow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tb_marid.Text = Request.QueryString["marid"].ToString();
                tb_marshijian.Text = DateTime.Now.ToString();
                bind();
            }
        }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            //取得显示分页界面的那一行
            GridViewRow pagerRow = GridView1.BottomPagerRow;
            if (pagerRow != null)
            {
                //取得第一页。上一页。下一页。最后一页的超级链接
                LinkButton lnkBtnFirst = (LinkButton)pagerRow.Cells[0].FindControl("lnkBtnFirst");
                LinkButton lnkBtnPrev = (LinkButton)pagerRow.Cells[0].FindControl("lnkBtnPrev");
                LinkButton lnkBtnNext = (LinkButton)pagerRow.Cells[0].FindControl("lnkBtnNext");
                LinkButton lnkBtnLast = (LinkButton)pagerRow.Cells[0].FindControl("lnkBtnLast");

                //设置何时应该禁用第一页。上一页。下一页。最后一页的超级链接
                if (GridView1.PageIndex == 0)
                {
                    lnkBtnFirst.Enabled = false;
                    lnkBtnPrev.Enabled = false;
                }
                else if (GridView1.PageIndex == GridView1.PageCount - 1)
                {
                    lnkBtnNext.Enabled = false;
                    lnkBtnLast.Enabled = false;
                }
                else if (GridView1.PageCount <= 0)
                {
                    lnkBtnFirst.Enabled = false;
                    lnkBtnPrev.Enabled = false;
                    lnkBtnNext.Enabled = false;
                    lnkBtnLast.Enabled = false;
                }
                //从显示分页的行中取得用来显示页次与切换分页的DropDownList控件
                DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("page_DropDownList");

                //根据欲显示的数据源的总页数，创建DropDownList控件的下拉菜单内容
                if (pageList != null)
                {
                    int intPage;
                    for (intPage = 0; intPage <= GridView1.PageCount - 1; intPage++)
                    {
                        //创建一个ListItem对象来存放分页列表
                        int pageNumber = intPage + 1;
                        ListItem item = new ListItem(pageNumber.ToString());

                        //交替显示背景颜色
                        switch (pageNumber % 2)
                        {
                            case 0: item.Attributes.Add("style", "background:#CDC9C2;");
                                break;
                            case 1: item.Attributes.Add("style", "color:red; background:white;");
                                break;
                        }
                        if (intPage == GridView1.PageIndex)
                        {
                            item.Selected = true;
                        }
                        pageList.Items.Add(item);
                    }
                }
                //显示当前所在页数与总页数
                Label pagerLabel = (Label)pagerRow.Cells[0].FindControl("lblCurrrentPage");

                if (pagerLabel != null)
                {

                    int currentPage = GridView1.PageIndex + 1;
                    pagerLabel.Text = "第" + currentPage.ToString() + "页（共" + GridView1.PageCount.ToString() + " 页）";

                }
            }

        }

        protected void page_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //取得显示分页界面的那一行
            GridViewRow pagerRow = GridView1.BottomPagerRow;
            //从显示页数的行中取得显示页数的DropDownList控件
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("page_DropDownList");
            //将GridView移至用户所选择的页数
            GridView1.PageIndex = pageList.SelectedIndex;
            bind();
            //getData();不用数据源需要绑定
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标经过时，行背景色变 
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E6F5FA'");
                //鼠标移出时，行背景色变 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");
                //点击返回当前行第6列的值
                e.Row.Attributes.Add("onclick", "window.returnValue=\"" + e.Row.Cells[0].Text.Trim() + "\";window.close();");

                ////当有编辑列时，避免出错，要加的RowState判断 
                //if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
                //{
                //    ((LinkButton)e.Row.Cells[6].Controls[0]).Attributes.Add("onclick", "javascript:return confirm('你确认要删除：\"" + e.Row.Cells[1].Text + "\"吗?')");
                //}

                ////////对指定单元格设置背景颜色
                ////////if (e.Row.Cells[8].Text == "待挂牌")
                ////////{
                ////////    e.Row.Cells[8].BackColor = System.Drawing.Color.Yellow;
                ////////}
                ////////else if (e.Row.Cells[8].Text == "已挂牌")
                ////////{
                ////////    e.Row.Cells[8].BackColor = System.Drawing.Color.YellowGreen;
                ////////}
                ////////else if (e.Row.Cells[8].Text == "已回退")
                ////////{
                ////////    e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
                ////////}
                ////////else if (e.Row.Cells[8].Text == "已签约")
                ////////{
                ////////    e.Row.Cells[8].BackColor = System.Drawing.Color.Violet;
                ////////}
                ////////else
                ////////{
                ////////    e.Row.Cells[8].BackColor = System.Drawing.Color.White;
                ////////}

            }
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            }
        }
        protected void change_BackColor()
        {
            for (int j = 0; j <= GridView1.PageCount - 1; j++)
            {
                for (int i = 0; i <= GridView1.PageSize - 1; i++)
                {
                    GridViewRow row = GridView1.Rows[i];
                    string gpzt = row.Cells[3].Text.ToString();     //获取挂牌状态所在单元格
                    if (gpzt == "0")
                    {
                        GridView1.Rows[i].Cells[3].BackColor = System.Drawing.Color.Yellow;
                    }
                    else if (gpzt == "1")
                    {
                        GridView1.Rows[i].Cells[3].BackColor = System.Drawing.Color.GreenYellow;
                    }
                    else if (gpzt == "2")
                    {
                        GridView1.Rows[i].Cells[3].BackColor = System.Drawing.Color.Red;
                    }
                    else if (gpzt == "3")
                    {
                        GridView1.Rows[i].Cells[3].BackColor = System.Drawing.Color.Violet;
                    }
                    else
                    {
                        GridView1.Rows[i].Cells[3].BackColor = System.Drawing.Color.White;
                    }
                }
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();
        }
        protected void bind()
        {
            //string sqltext = "SELECT MARID,MARNM,MARGG,MARCZ,PRICE,shuilv,IRQDATA,SUPPLIERRESNM,picno FROM View_TBPC_IQRCMPPRICE_RVW WHERE MARID ='" +
            //    tb_marid.Text + "' AND IRQDATA <='" + tb_marshijian.Text + "' ORDER BY IRQDATA DESC";
            
            string sqltext = "SELECT MARID,MARNM,MARGG,MARCZ,PRICE,shuilv,IRQDATA,SUPPLIERRESNM,picno FROM View_TBPC_IQRCMPPRICE_RVW WHERE MARID ='" +
                tb_marid.Text + "' AND totalstate='4' AND price is not null ORDER BY IRQDATA DESC";
          
            DBCallCommon.BindGridView(GridView1, sqltext);
        }
    }
}
