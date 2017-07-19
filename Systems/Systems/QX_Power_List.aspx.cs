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

namespace FSGD_PMS.Systems
{
    public partial class QX_Power_List : BasicPage
    {
        DbAccess dbl = new DbAccess();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGrid();
            }
            CheckUser(ControlFinder);
        }

        private void bindGrid()
        {
            string sqlText = "select *,'QX_Power_Detail.aspx?Action=Update&&page_id='+cast(a.page_id as varchar(20)) as QX_Power_Detail_URL, " ;
            sqlText += "b.name as fatherName from power_page a ,power_page b where b.page_id = a.fatherID order by b.fatherID";
            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;
            GridView1.DataSource = dbl.fillDataSet(sqlText);
            GridView1.DataBind();           
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            GridView theGrid = sender as GridView;  // refer to the GridView
            int newPageIndex = 0;

            if (-2 == e.NewPageIndex)
            { // when click the "GO" Button
                TextBox txtNewPageIndex = null;
                //GridViewRow pagerRow = theGrid.Controls[0].Controls[theGrid.Controls[0].Controls.Count - 1] as GridViewRow; // refer to PagerTemplate
                GridViewRow pagerRow = theGrid.BottomPagerRow; //GridView较DataGrid提供了更多的API，获取分页块可以使用BottomPagerRow 或者TopPagerRow，当然还增加了HeaderRow和FooterRow

                if (null != pagerRow)
                {
                    txtNewPageIndex = pagerRow.FindControl("txtNewPageIndex") as TextBox;   // refer to the TextBox with the NewPageIndex value
                }

                if (null != txtNewPageIndex)
                {
                    newPageIndex = int.Parse(txtNewPageIndex.Text) - 1; // get the NewPageIndex
                }

            }
            else
            {  // when click the first, last, previous and next Button
                newPageIndex = e.NewPageIndex;
            }

            // check to prevent form the NewPageIndex out of the range
            newPageIndex = newPageIndex < 0 ? 0 : newPageIndex;
            newPageIndex = newPageIndex >= theGrid.PageCount ? theGrid.PageCount - 1 : newPageIndex;

            // specify the NewPageIndex
            //Response.Write(newPageIndex);
            //Response.End();
            theGrid.PageIndex = newPageIndex;
            this.bindGrid();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "del")//删除
            {
                //获取当前操作的行索引
                GridViewRow gvrow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                int index = gvrow.RowIndex;

                //根据获取到得行索引，读出该条记录的ID
                string id = ((Label)(GridView1.Rows[index].FindControl("lblID"))).Text.Trim();

                //删除对应的零件
                //根据记录的ID从数据库中删除该记录
                dbl.exSQLv("delete from power_page where page_id='" + id + "'");
                //删除页面包含的控件
                dbl.exSQLv("delete from page_control where page_id='"+id+"'");

                //重新读出附件信息
                bindGrid();

            }
            if (e.CommandName == "Page")
            {
                TextBox tb = GridView1.BottomPagerRow.FindControl("txtNewPageIndex") as TextBox;

                int pageindex = Convert.ToInt32(tb.Text);
                GridView1.PageIndex = pageindex - 1;
                bindGrid();
            }
        }
               
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window:close();</script>");
        }
        
    }
}
