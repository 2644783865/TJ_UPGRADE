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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Pro_struinfo : System.Web.UI.Page
    {
        string sqlText = "";
        string lbid = "";
        string txt_cn_name = "";
        string txt_eng_name = "";
        string txt_code = "";
        string txt_note = "";
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                InitInfo();
            }
        }
        private void InitVar()
        {
            InitPager();
            //btnDelete.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        //初始化分页信息
        private void InitPager()
        {
            pager.TableName = "TBPD_STRUINFO";
            pager.PrimaryKey = "NodeID";
            pager.ShowFields = "";
            pager.OrderField = "dbo.f_formatstr(PDS_CODE, '.')";
            //pager.OrderField = "len(PDS_CODE) asc,PDS_CODE";
            pager.StrWhere = "PDS_ENGTYPE='" + ddlengtype.SelectedValue + "'";
            pager.OrderType = 0;//按编号升序排列
            pager.PageSize = 13;
            //pager.PageIndex = 1;
        }
        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            //绑定数据
            GetStruinfoData();
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetStruinfoData();
        }
        protected void GetStruinfoData()
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

        //点击查询时重新邦定GridView，添加查询条件
        private void ReGetStruinfoData()
        {
            InitPager();
            pager.StrWhere = CreateConStr();
            GetStruinfoData();
        }
        private string CreateConStr()
        {
            string strWhere = "";
            if (txtSearch.Text != "")
            {
                strWhere = ddlSearch.SelectedValue.Trim() + " like '" + txtSearch.Text.Trim() + "%'";
                strWhere += " and PDS_ENGTYPE='" + ddlengtype.SelectedValue + "'";
            }
            else
            {
                strWhere = "PDS_ENGTYPE='" + ddlengtype.SelectedValue + "'";
            }
            return strWhere;
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            InitInfo();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lbid=GridView1.DataKeys[e.RowIndex].Value.ToString();
            sqlText = "delete from TBPD_STRUINFO where NodeID='" + lbid + "'";
            DBCallCommon.ExeSqlText(sqlText);
            InitInfo();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lbid = GridView1.DataKeys[e.RowIndex].Value.ToString();
            txt_code = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].Controls[0])).Text.ToString().Trim();
            txt_cn_name = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[2].Controls[0])).Text.ToString().Trim();
            txt_eng_name = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[3].Controls[0])).Text.ToString().Trim();
            txt_note = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[4].Controls[0])).Text.ToString().Trim();
            sqlText = "update TBPD_STRUINFO set PDS_NAME='" + txt_cn_name + "',";
            sqlText += "PDS_ENGNAME='" + txt_eng_name + "',PDS_CODE='" + txt_code + "',";
            //strsql+="ParentNodeID='" + txt_father_id + "',PDS_ISYENODE='" + txt_node + "',";
            sqlText += "PDS_NOTE='" + txt_note + "' where NodeID='" + lbid + "'";
            DBCallCommon.ExeSqlText(sqlText);
            GridView1.EditIndex = -1;
            InitInfo();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetStruinfoData();
        }
       
        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            UCPaging1.CurrentPage = 1;
            ReGetStruinfoData();
        }

        protected void ddlengtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetStruinfoData();
        }
    }
}
