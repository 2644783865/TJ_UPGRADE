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
    public partial class PC_TBPC_CHANGELOOK : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                System.Web.UI.WebControls.Label pagerLabel = (System.Web.UI.WebControls.Label)pagerRow.Cells[0].FindControl("lblCurrrentPage");

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
                string pcode = ((Label)e.Row.FindControl("chpcode")).Text;
                string sqltext = "select chstate from View_TBPC_MPCHANGETOTAL where chpcode='" + pcode + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    string state = dt.Rows[0]["chstate"].ToString();
                    if (state == "0")
                    {
                        ((Label)e.Row.FindControl("state")).Text = "未执行";
                        ((Label)e.Row.FindControl("state")).ForeColor = System.Drawing.Color.Red;
                    }
                    else if (state == "1")
                    {
                        ((Label)e.Row.FindControl("state")).Text = "进行中";
                        ((Label)e.Row.FindControl("state")).ForeColor = System.Drawing.Color.Red;
                    }
                    else if (state == "2")
                    {
                        ((Label)e.Row.FindControl("state")).Text = "完毕";
                        ((Label)e.Row.FindControl("state")).ForeColor = System.Drawing.Color.Red;
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
            string sqltext = "";
            sqltext = "SELECT  chpcode, pjid, pjnm, engid, engnm, chptcode, marid, marnm, margg, marcz, margb, "+
                      "length, width, unit, fzunit, chnum, chfznum, chcgid, chcgnm, chstate," +
                      "chnote, zxnum, zxfznum, MP_ID, MP_TUHAO, MP_FIXED, MP_MASHAPE, MP_KEYCOMS  " +
                      " FROM   View_TBPC_MPTEMPCHANGE";
            sqltext += " where 1=1";
            if (tb_ptcode.Text != "")
            {
                sqltext += " and chpcode like '%" + tb_ptcode.Text.Trim() + "%'";
            }
            if (tb_pjnm.Text != "")
            {
                sqltext += " and pjnm like '%" + tb_pjnm.Text.Trim() + "%'";
            }
            if (tb_engnm.Text != "")
            {
                sqltext += " and engnm like '%" + tb_engnm.Text.Trim() + "%'";
            }
            if (tb_marid.Text != "")
            {
                sqltext += " and marid like '%" + tb_marid.Text.Trim() + "%'";
            }
            if (tb_marnm.Text != "")
            {
                sqltext += " and marnm like '%" + tb_marnm.Text.Trim() + "%'";
            }
            if (tb_margg.Text != "")
            {
                sqltext += " and margg like '%" + tb_margg.Text.Trim() + "%'";
            }
            if (tb_marcz.Text != "")
            {
                sqltext += " and marcz like '%" + tb_marcz.Text.Trim() + "%'";
            }
            if (tb_margb.Text != "")
            {
                sqltext += " and margb like '%" + tb_margb.Text.Trim() + "%'";
            }
            //DBCallCommon.BindGridView(GridView1, sqltext);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        //取消
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }

        protected void QueryButton_Click(object sender, EventArgs e)
        {
            //GridView1.PageIndex = -1;
            bind();
        }

        
        //重置条件
        protected void btnReset_Click(object sender, EventArgs e)
        {
            tb_ptcode.Text = "";
            tb_pjnm.Text = "";
            tb_engnm.Text = "";
            tb_marid.Text = "";
            tb_marnm.Text = "";
            tb_margg.Text = "";
            tb_marcz.Text = "";
            tb_margb.Text = "";
            bind();
        }
    }
}
