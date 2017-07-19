using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_Notice_Main : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        //string sql = "";

        protected void Page_Load(object sender, EventArgs e)
        {
             InitVar();
             if (!IsPostBack)
             {
                 bindGrid();
                 comboBoxData();//绑定主查询--项目查询
             }
                      
        }

        private void InitVar()
        {
            //btnDelete.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数      
        }


        //初始化分布信息
        private void InitPager()
        {
            pager.TableName = "TBPM_DEPCONSHEET";
            pager.PrimaryKey = "DCS_ID";
            pager.ShowFields = "DCS_ID,DCS_PROJECT,DCS_PROJECTID,DCS_TYPE,DCS_DATE";
            pager.OrderField = "DCS_DATE";
            pager.StrWhere = "";
            pager.OrderType = 1;//按时间降序排列
            pager.PageSize = 10;
            //pager.PageIndex = 1;
        }


        void Pager_PageChanged(int pageNumber)
        {

            bindGrid();

        }


      
        private void bindGrid()
        {
            //DataTable dt = DBCProcPageing.Projects_Select(UCPaging1.CurrentPage, UCPaging1.PageSize);
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }

            foreach (RepeaterItem items in Repeater1.Items)
            {
                Label lb = (Label)items.FindControl("BCSTYPE");
                if (lb.Text != "")
                {
                    switch (Convert.ToInt32(lb.Text))
                    {
                        case 1:
                            lb.Text = "合同信息";
                            break;
                        case 2:
                            lb.Text = "任务单信息";
                            break;
                        case 3:
                            lb.Text = "其他信息";
                            break;

                    }
                }
            }
        }

        void comboBoxData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PT_PJID");
            dt.Columns.Add("PT_PJNAME");
            dt.Rows.Add("全部", "0");
            string sql = "select distinct DCS_PROJECTID, DCS_PROJECT from TBPM_DEPCONSHEET";//读取所有的项目名称
            SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sql);
            while (dr1.Read())
            {
                DataRow dr = dt.NewRow();
                dr[0] = dr1["DCS_PROJECTID"].ToString() + "|" + dr1["DCS_PROJECT"].ToString();
                dr[1] = dr1["DCS_PROJECTID"].ToString();
                dt.Rows.Add(dr);
            }
            dr1.Close();
            ComboBox2.DataSource = dt;
            ComboBox2.DataTextField = "PT_PJID";
            ComboBox2.DataValueField = "PT_PJNAME";
            ComboBox2.DataBind();

        }

        protected void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlstatus.SelectedItem.Value = "0";

            if (ComboBox2.SelectedValue != "0")//筛选
            {
                UCPaging1.CurrentPage = 1;
                RebindGrid();
            }
            else//全部
            {
                InitPager();//初始化页面
                bindGrid();//重新查询全部

            }
        }

        private void RebindGrid()
        {
            InitPager();
            pager.StrWhere = CreateConStr();
            bindGrid();
        }

        private string CreateConStr()
        {
            //Response.Write("d2quanbu");
            string status = ComboBox2.SelectedItem.Text.Split('|')[0].ToString();
            string strWhere = "";
            if (ComboBox2.SelectedValue != "0")
            {
                strWhere = "DCS_PROJECTID ='" + status + "'";
            }
            return strWhere;
        }

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox2.SelectedItem.Value = "全部";
            if (ddlstatus.SelectedValue != "0")//筛选
            {
                UCPaging1.CurrentPage = 1;
                RebindGrid1();
            }
            else//全部
            {
                InitPager();//初始化页面
                bindGrid();//重新查询全部

            }
        }

        private void RebindGrid1()
        {

            InitPager();
            pager.StrWhere = CreateConStr1("DCS_TYPE", ddlstatus.SelectedItem.Value);
            bindGrid();
        }

        private string CreateConStr1(string stw, string status)
        {

            string strWhere = "";
            if (status != "0")
            {
                strWhere = " " + stw + "= '" + status + "' ";
            }
            return strWhere;
        }

    }
}
