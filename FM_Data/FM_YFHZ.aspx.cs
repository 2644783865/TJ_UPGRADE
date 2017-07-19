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
using System.Collections.Generic;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_YFHZ : System.Web.UI.Page
    {
        double jehj = 0;
        double hsjehj = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["StrWhere"] = "1=1";
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
                bindGV();
                for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
                {
                    if (i == rptProNumCost.Items.Count - 2)
                    {
                        for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
                        {
                            Label lbjsd1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                            Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("lbjsdh");
                            if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                            {
                                lbjsd1.Visible = false;
                            }
                        }
                    }
                    Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                    Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbjsdh");
                    if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                    {
                        lbjsdh2.Visible = false;
                    }
                }
            }

            if (IsPostBack)
            {
                this.InitVar();
            }

        }

        #region  分页
        PagerQueryParam pager_org = new PagerQueryParam();
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        private void InitPager()
        {
            pager_org.TableName = "TBFM_YFHZ";
            pager_org.PrimaryKey = "YFHZ_PTC";
            pager_org.ShowFields = "YFHZ_JSDH,YFHZ_YEARMONTH,YFHZ_NUM,YFHZ_WIGHT,YFHZ_TSAID,cast(YFHZ_MONEY as decimal(12,2)) as YFHZ_MONEY,YFHZ_HSMONEY,YFHZ_BJBH,YFHZ_BJNAME,case when YFHZ_GJSTATE='3' then '是' else '否' end as YFHZ_GJSTATE";
            pager_org.OrderField = "YFHZ_JSDH";
            pager_org.StrWhere = ViewState["StrWhere"].ToString();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 30;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
            for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
            {
                if (i == rptProNumCost.Items.Count - 2)
                {
                    for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
                    {
                        Label lbjsd1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                        Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("lbjsdh");
                        if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                        {
                            lbjsd1.Visible = false;
                        }
                    }
                }
                Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbjsdh");
                if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                {
                    lbjsdh2.Visible = false;
                }
            }
        }
        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }

        }


        //给用于筛选的GRIDVIEW绑定一定行数条件/////////
        private void bindGV()
        {
            GridViewSearch.DataSource = CreateTable();

            GridViewSearch.DataBind();
        }

        private DataTable CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("index", typeof(int)));
            for (int i = 0; i < 8; i++)
            {
                DataRow row = dt.NewRow();
                row["index"] = i;
                dt.Rows.Add(row);
            }
            return dt;
        }

        protected void GridViewSearch_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                if (gr.RowIndex == (GridViewSearch.Rows.Count - 1))
                {
                    (gr.FindControl("tb_logic") as TextBox).Visible = false;
                }
            }
        }
        ///////////////////////////////////////////////
        //比较关系和数值obj////////////////////////////////////
        private string ConvertRelation(string field, string relation, string fieldValue)
        {
            string obj = string.Empty;
            switch (relation)
            {
                case "0":
                    {
                        //包含
                        obj = field + " LIKE '%" + fieldValue + "%'";
                        break;
                    }
                case "1":
                    {
                        //等于
                        obj = field + " = '" + fieldValue + "'";
                        break;
                    }
                case "2":
                    {
                        //不等于
                        obj = field + " != '" + fieldValue + "'";
                        break;
                    }
                case "3":
                    {
                        //大于
                        obj = field + " > '" + fieldValue + "'";
                        break;
                    }
                case "4":
                    {
                        //大于或等于
                        obj = field + " >= '" + fieldValue + "'";
                        break;
                    }
                case "5":
                    {
                        //小于
                        obj = field + " < '" + fieldValue + "'";
                        break;
                    }
                case "6":
                    {
                        //小于或等于
                        obj = field + " <= '" + fieldValue + "'";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return obj;
        }
        //////////////////////////////////////////////////////////////
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            string sqltext = "1=1";

            string conditon = GetGridViewCondition();//获取查询条件

            //判断逻辑条件是否存在
            if (conditon != "")
            {
                sqltext += " and (" + conditon + ")  ";
            }
            if (startdat.Text.Trim() != "")
            {
                sqltext += " and YFHZ_YEARMONTH >= '" + startdat.Text.Trim() + "' ";
            }
            if (enddat.Text.Trim() != "")
            {
                sqltext += " and YFHZ_YEARMONTH <= '" + enddat.Text.Trim() + "' ";
            }
            if (DropDownifgj.SelectedIndex != 0)
            {
                sqltext += " and YFHZ_GJSTATE='" + DropDownifgj.SelectedValue.ToString() + "'";
            }
            ViewState["StrWhere"] = sqltext;
            InitVar();
            UCPaging1.CurrentPage = 1;
            bindGrid();
            for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
            {
                if (i == rptProNumCost.Items.Count - 2)
                {
                    for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
                    {
                        Label lbjsd1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                        Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("lbjsdh");
                        if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                        {
                            lbjsd1.Visible = false;
                        }
                    }
                }
                Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbjsdh");
                if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                {
                    lbjsdh2.Visible = false;
                }
            }
            refreshStyle();
        }
        private string GetGridViewCondition()
        {
            string condition = "";
            for (int i = 0; i < GridViewSearch.Rows.Count; i++)
            {
                GridViewRow gr = GridViewSearch.Rows[i];
                DropDownList ddllistname = (DropDownList)gr.FindControl("DropDownListName");//名称
                if (ddllistname.SelectedValue != "NO")
                {
                    TextBox txtValue = (TextBox)gr.FindControl("TextBoxValue");//数值
                    DropDownList ddlRel = (DropDownList)gr.FindControl("DropDownListRelation");//比较关系
                    //获取下行的操作
                    DropDownList ddlnext = (DropDownList)GridViewSearch.Rows[i + 1].FindControl("DropDownListName");
                    if (ddlnext.SelectedValue != "NO")
                    {
                        DropDownList ddlLogic = (DropDownList)gr.FindControl("DropDownListLogic");
                        condition += ConvertRelation(ddllistname.SelectedValue, ddlRel.SelectedValue, txtValue.Text.Trim()) + " " + ddlLogic.SelectedValue + " ";
                        if (i == (GridViewSearch.Rows.Count - 1))
                        {
                            //获取最后一行的比较关系和数值
                            TextBox nextValue = (TextBox)GridViewSearch.Rows[GridViewSearch.Rows.Count - 1].FindControl("TextBoxValue");
                            DropDownList nextddlRel = (DropDownList)GridViewSearch.Rows[GridViewSearch.Rows.Count - 1].FindControl("DropDownListRelation");
                            condition += ConvertRelation(ddlnext.SelectedValue, nextddlRel.SelectedValue, nextValue.Text.Trim());
                            break;
                        }
                    }
                    else
                    {
                        condition += ConvertRelation(ddllistname.SelectedValue, ddlRel.SelectedValue, txtValue.Text.Trim());
                        break;
                    }
                }
            }
            return condition;
        }
        //重置////////////////////////////////////////////////////
        protected void btnReset_Click(object sender, EventArgs e)
        {
            startdat.Text = "";
            enddat.Text = "";
            bindGV();
        }
        private void refreshStyle()
        {
            for (int i = 0; i < GridViewSearch.Rows.Count; i++)
            {
                GridViewRow gr = GridViewSearch.Rows[i];
                if ((gr.FindControl("DropDownListName") as DropDownList).SelectedValue != "NO")
                {
                    if (gr.RowIndex != 0)
                    {
                        GridViewRow fgr = GridViewSearch.Rows[i - 1];
                        (fgr.FindControl("DropDownListLogic") as DropDownList).Style.Add("display", "block");
                        (fgr.FindControl("tb_logic") as TextBox).Style.Add("display", "none");
                    }

                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "block");
                    (gr.FindControl("tb_name") as TextBox).Style.Add("display", "none");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "block");
                    (gr.FindControl("tb_relation") as TextBox).Style.Add("display", "none");
                }
                else
                {
                    (gr.FindControl("DropDownListLogic") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_logic") as TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_name") as TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_relation") as TextBox).Style.Add("display", "block");
                }

            }
        }
        ////////////////////////////////////////////////////////////////////////////









        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbjsdh = (Label)e.Item.FindControl("lbjsdh");
                Label lbrwh = (Label)e.Item.FindControl("lbrwh");
                Label lbyearmonth = (Label)e.Item.FindControl("lbyearmonth");
                Label lbsl = (Label)e.Item.FindControl("lbsl");
                Label lbzl = (Label)e.Item.FindControl("lbzl");
                Label lbmoney = (Label)e.Item.FindControl("lbmoney");
                Label lbhsmoney = (Label)e.Item.FindControl("lbhsmoney");

                Label lbbjbh = (Label)e.Item.FindControl("lbbjbh");
                Label lbbjname = (Label)e.Item.FindControl("lbbjname");
                Label lbgjzt = (Label)e.Item.FindControl("lbgjzt");
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select isnull(CAST(sum(YFHZ_MONEY) AS FLOAT),0) as JEHJ,isnull(CAST(sum(YFHZ_HSMONEY) AS FLOAT),0) as HSJEHJ from TBFM_YFHZ where " + ViewState["StrWhere"].ToString();

                SqlDataReader drhj = DBCallCommon.GetDRUsingSqlText(sqlhj);

                if (drhj.Read())
                {
                    jehj = Convert.ToDouble(drhj["JEHJ"]);
                    hsjehj = Convert.ToDouble(drhj["HSJEHJ"]);
                }
                drhj.Close();
                Label lbjehj = (Label)e.Item.FindControl("lbjehj");
                Label lbhsjehj = (Label)e.Item.FindControl("lbhsjehj");

                lbjehj.Text = jehj.ToString("0.00");
                lbhsjehj.Text = hsjehj.ToString();
            }
        }
    }
}
        #endregion