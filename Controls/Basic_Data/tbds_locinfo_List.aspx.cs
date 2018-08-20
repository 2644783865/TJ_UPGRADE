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
using ZCZJ_DPF;
using System.Data.SqlClient;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class tbds_locinfo_List : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        string sqlText = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string sqltxt = "";
             InitVar();
            if (!IsPostBack)
            {               
                ddlSearchchild.Visible = false;
                sqltxt = "select distinct CL_NAME,CL_CODE from TBCS_LOCINFO where CL_FATHERCODE='ROOT'";
                DBCallCommon.FillDroplist(ddlSearch, sqltxt);
                selectaddgroupd.SelectedIndex = 0;
                bindGrid();
               
            }
            CheckUser(ControlFinder);
            
        }
        private void InitVar()
        {
            InitPager();
            //btnDelete.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        ////分页初始化
        private void InitPager()
        {
            pager.TableName = "TBCS_LOCINFO";
            pager.PrimaryKey = "CL_CODE";
            pager.ShowFields = "CL_CODE,CL_NAME,CL_FATHERCODE,CL_MANCLERK,CL_FILLDATE,CL_NOTE";
            pager.OrderField = "CL_CODE";
            pager.StrWhere = "CL_FATHERCODE='root'";
            pager.OrderType = 0;//按时间降序排列
            pager.PageSize = 10;
            //pager.PageIndex = 1;
        }

        private void InitPager1(string tablename, string key, string showField, string orderField, string where)
        {
            pager.TableName = tablename;
            pager.PrimaryKey = key;
            pager.ShowFields = showField;
            pager.OrderField = orderField;
            pager.StrWhere = where;
            pager.OrderType = 0;//按时间降序排列
            pager.PageSize = 10;
            //pager.PageIndex = 1;
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        void Pager_PageChanged(int pageNumber)
        {

            bindGrid();

        }
        private void bindGrid()
        {
            string shwhere = "";
            if (ddlSearch.SelectedValue != "-请选择-")
            {
                shwhere = "CL_CODE like '" + ddlSearch.SelectedValue + "%'";
            }
            else
            {
                shwhere = "CL_FATHERCODE='root'";
            }
            sqlText = " CL_CODE,CL_NAME,CL_FATHERCODE,CL_MANCLERK,CL_FILLDATE,CL_NOTE ";
            InitPager1("TBCS_LOCINFO", "CL_CODE", sqlText, "CL_CODE", shwhere);
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, Reproject1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
                CheckUser(ControlFinder);
            }
        }



        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ddlvalue = "";
            if (ddlSearch.SelectedItem.Text.Trim() == "-请选择-")
            {
                ddlSearchchild.Visible = false;
                bindGrid();
            }
            else
            {
                ddlSearchchild.Visible = true;
                ddlvalue = ddlSearch.SelectedItem.Value.ToString();
                sqlText = "select distinct CL_NAME,CL_CODE from TBCS_LOCINFO where CL_FATHERCODE='" + ddlvalue  + "' ";
                //Response.Write(ddlvalue);
                //Response.End();
                DBCallCommon.FillDroplist(ddlSearchchild, sqlText);
                RebindGrid();
            }
            
        }

        protected void ddlSearchchild_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;//设置当前页尾首页
            RebindGrid() ;          //重新绑定
        }

        //点击查询时重新邦定，添加查询条件
        private void RebindGrid()
        {
            selectaddgroupd.SelectedIndex = 0; //对添加下拉框进行初始化
            InitPager();
            pager.StrWhere = CreateConStr();
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, Reproject1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
                CheckUser(ControlFinder);
            }
        }

        private string CreateConStr()
        {
            string strWhere = "";

            if (ddlSearch.SelectedItem.Text.Trim() != "-请选择-")
            {
                if (ddlSearchchild.SelectedItem.Text.Trim() != "-请选择-")
                {
                    strWhere = "CL_CODE='" + ddlSearchchild.SelectedItem.Value.ToString() + "'";
                }
                else
                {
                    strWhere = "CL_CODE='" + ddlSearch.SelectedItem.Value.ToString() + "'or CL_FATHERCODE='"+ddlSearch.SelectedItem.Value.ToString()+"'";
                }
                
            }
            return strWhere;
        }


        protected void selectaddgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectaddgroup = selectaddgroupd.SelectedValue;
            if (selectaddgroup == "0")//无操作
            {

            }
            else
            {
                //Response.Write("<script>javascript:window.showModalDialog('tbds_locinfo_detail.aspx?action=add&selectaddgroup=" + selectaddgroup + "','','dialogWidth=650px;dialogHeight=400px',true);</script>");
            }
        }
       

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            CommonFun.delMult("TBCS_LOCINFO", "CL_CODE", Reproject1);
            bindGrid();
            Response.Redirect(Request.Url.ToString());
        }
        protected string editDq(string DqId)
        {
            return "javascript:window.showModalDialog('tbds_locinfo_detail.aspx?action=update&id=" + DqId + "','','DialogWidth=650px;DialogHeight=400px')";
        } 
    }
}
