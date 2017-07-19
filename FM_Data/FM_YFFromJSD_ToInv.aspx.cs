using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_YFFromJSD_ToInv : System.Web.UI.Page
    {
        double zlhj = 0;
        double hsjehj = 0;
        double jehj = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int Count = Convert.ToInt32(DropDownListCount.SelectedValue.ToString());
                ViewState["count"] = Count;
                ViewState["strWhere"] = "JS_GJSTATE='0'";
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
                            Label lbdat1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbdate");
                            Label lbgysm1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbgysmc");
                            Label lbzdrx1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbzdrxm");
                            Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("lbjsdh");
                            if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                            {
                                lbjsd1.Visible = false;
                                lbdat1.Visible = false;
                                lbgysm1.Visible = false;
                                lbzdrx1.Visible = false;
                            }
                        }
                    }
                    Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                    Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbjsdh");
                    Label lbdate2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbdate");
                    Label lbgysmc2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbgysmc");
                    Label lbzdrxm2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbzdrxm");
                    if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                    {
                        lbjsdh2.Visible = false;
                        lbdate2.Visible = false;
                        lbgysmc2.Visible = false;
                        lbzdrxm2.Visible = false;
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
            pager_org.TableName = "PM_CPFYJSD";
            pager_org.PrimaryKey = "JS_JHGZH";
            pager_org.ShowFields = "left(CONVERT(CHAR(10), JS_RQ, 23),10) as JS_RQ,JS_JHGZH,JS_RWH,JS_BH,JS_GYS,JS_ZDR,JS_TUHAO,JS_SBMC,JS_BJSL,JS_DANZ,cast(((isnull(cast(JS_HSJE as float),0))/(1+(isnull(JS_SHUIL,0))/100)) as decimal(12,2)) as MONEY,(cast(isnull(JS_SHUIL,0) as char(6))+'%') as JS_SHUIL,isnull(cast(JS_HSJE as float),0) as JS_HSJE";
            pager_org.OrderField = "JS_BH";
            pager_org.StrWhere = ViewState["strWhere"].ToString();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = Convert.ToInt32(ViewState["count"].ToString());
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
                        Label lbdat1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbdate");
                        Label lbgysm1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbgysmc");
                        Label lbzdrx1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbzdrxm");
                        Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("lbjsdh");
                        if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                        {
                            lbjsd1.Visible = false;
                            lbdat1.Visible = false;
                            lbgysm1.Visible = false;
                            lbzdrx1.Visible = false;
                        }
                    }
                }
                Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbjsdh");
                Label lbdate2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbdate");
                Label lbgysmc2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbgysmc");
                Label lbzdrxm2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbzdrxm");
                if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                {
                    lbjsdh2.Visible = false;
                    lbdate2.Visible = false;
                    lbgysmc2.Visible = false;
                    lbzdrxm2.Visible = false;
                }
            }
        }
        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }
        #endregion
        protected void Count_Change(object sender, EventArgs e)
        {
            int Count = Convert.ToInt32(DropDownListCount.SelectedValue.ToString());
            ViewState["count"] = Count;
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
            for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
            {
                if (i == rptProNumCost.Items.Count - 2)
                {
                    for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
                    {
                        Label lbjsd1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                        Label lbdat1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbdate");
                        Label lbgysm1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbgysmc");
                        Label lbzdrx1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbzdrxm");
                        Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("lbjsdh");
                        if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                        {
                            lbjsd1.Visible = false;
                            lbdat1.Visible = false;
                            lbgysm1.Visible = false;
                            lbzdrx1.Visible = false;
                        }
                    }
                }
                Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbjsdh");
                Label lbdate2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbdate");
                Label lbgysmc2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbgysmc");
                Label lbzdrxm2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbzdrxm");
                if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                {
                    lbjsdh2.Visible = false;
                    lbdate2.Visible = false;
                    lbgysmc2.Visible = false;
                    lbzdrxm2.Visible = false;
                }
            }
        }

        //定义查询条件

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
            string sqltext = "JS_GJSTATE='0'";

            string conditon = GetGridViewCondition();//获取查询条件

            //判断逻辑条件是否存在
            if (conditon != "")
            {
                sqltext += " and (" + conditon + ")  ";
            }
            if (startdat.Text.Trim() != "")
            {
                sqltext += " and left(CONVERT(CHAR(10), JS_RQ, 23),10) >= '" + startdat.Text.Trim() + "' ";
            }
            if (enddat.Text.Trim() != "")
            {
                sqltext += " and left(CONVERT(CHAR(10), JS_RQ, 23),10) <= '" + enddat.Text.Trim() + "' ";
            }
            ViewState["strWhere"] = sqltext;
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
                        Label lbdat1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbdate");
                        Label lbgysm1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbgysmc");
                        Label lbzdrx1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbzdrxm");
                        Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("lbjsdh");
                        if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                        {
                            lbjsd1.Visible = false;
                            lbdat1.Visible = false;
                            lbgysm1.Visible = false;
                            lbzdrx1.Visible = false;
                        }
                    }
                }
                Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbjsdh");
                Label lbdate2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbdate");
                Label lbgysmc2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbgysmc");
                Label lbzdrxm2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbzdrxm");
                if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                {
                    lbjsdh2.Visible = false;
                    lbdate2.Visible = false;
                    lbgysmc2.Visible = false;
                    lbzdrxm2.Visible = false;
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


        //下推发票
        protected void btnXTFP_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (RepeaterItem Reitem in rptProNumCost.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                if (cbx.Checked)
                {
                    count++;
                    string jhgzh = ((System.Web.UI.WebControls.Label)Reitem.FindControl("lbjhgzh")).Text.Trim().ToString();
                    string sql = "update PM_CPFYJSD set JS_XTSTATE='1' where JS_JHGZH='" + jhgzh + "'";
                    DBCallCommon.ExeSqlText(sql);
                }
            }
            if (count > 0)
            {
                Response.Redirect("FM_Create_YFInvoice.aspx?");
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('请选择要下推到发票的项!')</script>");
                return;
            }
        }




        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)//计算合计值
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label tbdate = (Label)e.Item.FindControl("lbdate");
                Label tbjhgzh = (Label)e.Item.FindControl("lbjhgzh");
                Label tbrwh = (Label)e.Item.FindControl("lbrwh");
                Label tbjsdh = (Label)e.Item.FindControl("lbjsdh");
                Label tbgysmc = (Label)e.Item.FindControl("lbgysmc");
                Label tbzdrxm = (Label)e.Item.FindControl("lbzdrxm");
                Label tbtuhao = (Label)e.Item.FindControl("lbtuhao");
                Label tbmname = (Label)e.Item.FindControl("lbmname");
                Label tbsl = (Label)e.Item.FindControl("lbsl");
                Label tbzl = (Label)e.Item.FindControl("lbzl");
                Label tbje = (Label)e.Item.FindControl("lbje");
                Label tbshuilv = (Label)e.Item.FindControl("lbshuilv");
                Label tbhsje = (Label)e.Item.FindControl("lbhsje");

            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select isnull(CAST(sum(cast(JS_ZONGZ as float)) AS FLOAT),0) as totalJS_ZONGZ,isnull(CAST(sum(cast(JS_HSJE as float)) AS FLOAT),0) as totalJS_HSJE,isnull(sum(cast((JS_HSJE/(((isnull(cast(JS_SHUIL as float),0))/100)+1)) as decimal(12,2))),0) as totalJS_JE from PM_CPFYJSD where " + ViewState["strWhere"].ToString();

                SqlDataReader drhj = DBCallCommon.GetDRUsingSqlText(sqlhj);
                if (drhj.Read())
                {
                    zlhj = Convert.ToDouble(drhj["totalJS_ZONGZ"]);
                    hsjehj = Convert.ToDouble(drhj["totalJS_HSJE"]);
                    jehj = Convert.ToDouble(drhj["totalJS_JE"]);
                }
                drhj.Close();
                Label lbzlhj = (Label)e.Item.FindControl("lbzlhj");
                Label lbhsjehj = (Label)e.Item.FindControl("lbhsjehj");
                Label lbjehj = (Label)e.Item.FindControl("lbjehj");
                lbzlhj.Text = zlhj.ToString();
                lbhsjehj.Text = hsjehj.ToString();
                lbjehj.Text = jehj.ToString();

            }
        }
    }
}
