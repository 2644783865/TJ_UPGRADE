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
using System.IO;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchaseplan_ShowALL : System.Web.UI.Page
    {
        public string gloabshape
        {
            get
            {
                object str = ViewState["gloabshape"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabshape"] = value;
            }
        }
        public string gloabsheetno
        {
            get
            {
                object str = ViewState["gloabsheetno"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabsheetno"] = value;
            }
        }
        public string globlempcode
        {
            get
            {
                object str = ViewState["globlempcode"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["globlempcode"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["orderno"] != null)
                {
                    orderno.Text = Request.QueryString["orderno"].ToString();
                    gloabsheetno = Request.QueryString["orderno"].ToString();
                }
                if (Request.QueryString["shape"] != null)
                {
                    tb_shape.Text = Request.QueryString["shape"].ToString();
                    gloabshape = Request.QueryString["shape"].ToString();
                    if (tb_shape.Text == "非定尺板" || tb_shape.Text == "标(发运)" || tb_shape.Text == "标(组装)")
                    {
                        GridView1.Columns[14].Visible = false;
                        GridView1.Columns[15].Visible = false;
                    }
                }
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
                //e.Row.Attributes.Add("onclick", "window.returnValue=\"" + e.Row.Cells[0].Text.Trim() + "\";window.close();");
                string state = ((System.Web.UI.WebControls.Label)e.Row.FindControl("purstate")).Text;
                if (Convert.ToDouble(state) != 0 && Convert.ToDouble(state) != 3 && Convert.ToDouble(state) != 4)
                {
                    ((System.Web.UI.WebControls.CheckBox)e.Row.FindControl("CheckBox1")).Enabled = false;
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
            sqltext = "SELECT planno, pjid, pjnm, engid, engnm, ptcode, " +
                           "marid, marnm, margg, marcz, margb, marunit,marfzunit,length,width,num,fznum, " +
                           "rpnum, rpfznum,jstimerq,cgrid, cgrnm, isnull(purstate,0) as purstate, purnote, picno,orderno,PUR_MASHAPE,PUR_CSTATE,PUR_ZYDY,PUR_TUHAO  " +
                           "FROM  View_TBPC_PURCHASEPLAN_IRQ_ORDER   where planno='" + orderno.Text + "'";
            DBCallCommon.BindGridView(GridView1, sqltext);

        }
     
        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window.close();</script>");
        }
       
    }
}
