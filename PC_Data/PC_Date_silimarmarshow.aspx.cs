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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_Date_silimarmarshow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ptcode"] != null)
                {
                    tb_ptcode.Text = Request.QueryString["ptcode"].ToString();
                }
                else
                {
                    tb_ptcode.Text = "";
                }
                if (Request.QueryString["marnm"] != null)
                {
                    tb_marnm.Text = Request.QueryString["marnm"].ToString();
                }
                else
                {
                    tb_marnm.Text = "";
                }
                if (Request.QueryString["marcz"] != null)
                {
                    tb_marcz.Text = Request.QueryString["marcz"].ToString();
                }
                else
                {
                    tb_marcz.Text = "";
                }
                if (Request.QueryString["marid"] != null)
                {
                    tb_marid.Text = Request.QueryString["marid"].ToString();
                }
                else
                {
                    tb_marid.Text = "";
                }
                
                bind();
                bind1();
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
                //e.Row.Attributes.Add("onclick", "window.returnValue=\"" + e.Row.Cells[0].Text.Trim() + "\";window.close();");
            }
            if (e.Row.RowIndex != -1)
            {
                int id = e.Row.RowIndex + 1;
                e.Row.Cells[0].Text = id.ToString();
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();
        }
        protected void bind()
        {
            string sqltext = "";
            sqltext = "select marid,marnm,margg,margb,marcz,marunit,marfzunit,length,width," +
                             "sum(num) as num,sum(fznum) as fznum  " +
                             "from View_TBPC_STORAGE  " +
                             "where marnm='" + tb_marnm.Text + "' and marcz = '"+tb_marcz.Text+"' and  ptcode='备库'  " +
                             "group by marid,marnm,margg,margb,marcz,marunit,marfzunit,length, width";
           
            DBCallCommon.BindGridView(GridView1, sqltext);
        }

        protected void bind1()
        {
            string sqltext = "";
            sqltext = "select marid,marnm,margg,margb,marcz,marunit,num  " +
                  "from View_TBPC_PURCHASEPLAN  " +
                  "where  marid='" + tb_marid.Text + "' and ptcode='"+tb_ptcode.Text+"'";
            DBCallCommon.BindGridView(GridView2, sqltext);
        }
    }
}
