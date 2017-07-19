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

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_FromRK_ToInv : System.Web.UI.Page
    {
        PagerQueryParamGroupBy pager = new PagerQueryParamGroupBy();

        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                btnXTFP.Visible = false; //下推发票
                UCPaging1.Visible = false;
                bindGV();
            }
        }
        
        protected void btnXTFP_Click(object sender, EventArgs e)
        {
            string passParameters = "";
            foreach (GridViewRow gvr in grvRK.Rows)
            {
                CheckBox cbx = (CheckBox)gvr.FindControl("CKBOX_SELECT");
                if (cbx.Checked)
                {
                    HiddenField hdf = (HiddenField)gvr.FindControl("hdfRKBH");
                    passParameters += hdf.Value.ToString()+"/";
                }
            }
            if (passParameters.Length > 0)
            {
                passParameters = passParameters.Substring(0, passParameters.Length - 1);
            }
            Response.Redirect("FM_Create_WareHouseInvoice.aspx?arrayWG_CODE="+passParameters);
        }
        
        
        private void bindGV()
        {
            GridViewSearch.DataSource = CreateTable();

            GridViewSearch.DataBind();
        }

        private DataTable CreateTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("index", typeof(int)));


            for (int i = 0; i < 50; i++)
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
                if (gr.RowIndex == (GridViewSearch.Rows.Count-1))
                {
                    (gr.FindControl("tb_logic") as TextBox).Visible = false;
                }
            }
        }
        
        private string ConvertRelation(string field, string relation, string fieldValue)
        {
            string obj = string.Empty;

            //if (field == "WG_CODE")
            //{
            //    fieldValue = fieldValue.PadLeft(10, '0');
            //}


            switch (relation)
            {
                case "0":
                    {
                        //包含
                        obj = field + "  LIKE  '%" + fieldValue + "%'";
                        break;
                    }
                case "1":
                    {
                        //等于
                        obj = field + "  =  '" + fieldValue + "'";
                        break;
                    }
                case "2":
                    {
                        //不等于
                        obj = field + "  !=  '" + fieldValue + "'";
                        break;
                    }
                case "3":
                    {
                        //大于
                        obj = field + "  >  '" + fieldValue + "'";
                        break;
                    }
                case "4":
                    {
                        //大于或等于
                        obj = field + "  >=  '" + fieldValue + "'";
                        break;
                    }
                case "5":
                    {
                        //小于
                        obj = field + "  <  '" + fieldValue + "'";
                        break;
                    }
                case "6":
                    {
                        //小于或等于
                        obj = field + "  <=  '" + fieldValue + "'";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return obj;
        }
        //查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            //蓝字，为核销，审核，未勾稽，正常入库单（结转备库的不需要勾稽）

            //  //0为采购入库，1为结转备库，2为暂估冲回，3为外协入库
            //WG_GJSTATE=0 无发票，1为有发票未勾稽，2为已勾稽，3为部分勾稽

            string sqltext = " ((WG_ROB='0' and WG_CAVFLAG='0') or WG_CODE in('G000002321R1','G000002952S1R1','G000002952S2R1')) and WG_STATE='2'  and (WG_GJSTATE='0' or WG_GJSTATE='3') and (WG_BILLTYPE='0' OR WG_BILLTYPE='3') ";

            string conditon = GetGridViewCondition();

            //判断逻辑条件是否存在
            if (conditon != "")
            {
                sqltext += " and (" + conditon + ")  ";
            }
            if (starttime0.Text.Trim() != "")
            {
                sqltext += " and WG_DATE >= '" + starttime0.Text.Trim() + "' ";
            }
            if (finishtime0.Text.Trim() != "")
            {
                sqltext += " and WG_DATE <= '" + finishtime0.Text.Trim() + "' ";
            }
            ViewState["strWhere"] = sqltext;
            InitVar();
            UCPaging1.CurrentPage = 1;
            GetData();
            refreshStyle();
        }
        private string GetGridViewCondition()
        {
            string condition = "";
            for (int i = 0; i < GridViewSearch.Rows.Count; i++)
            {
                GridViewRow gr = GridViewSearch.Rows[i];
                DropDownList ddllistname = (DropDownList)gr.FindControl("DropDownListName");
                if (ddllistname.SelectedValue != "NO")
                {
                    TextBox txtValue = (TextBox)gr.FindControl("TextBoxValue");
                    DropDownList ddlRel = (DropDownList)gr.FindControl("DropDownListRelation");
                    //获取下行的操作
                    DropDownList ddlnext = (DropDownList)GridViewSearch.Rows[i + 1].FindControl("DropDownListName");
                    if (ddlnext.SelectedValue != "NO")
                    {
                        DropDownList ddlLogic = (DropDownList)gr.FindControl("DropDownListLogic");
                        condition += ConvertRelation(ddllistname.SelectedValue, ddlRel.SelectedValue, txtValue.Text.Trim()) + " " + ddlLogic.SelectedValue + " ";
                        if (i == (GridViewSearch.Rows.Count-1))
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
        //重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            starttime0.Text = "";
            finishtime0.Text = "";
            bindGV();
            grvRK.DataSource = null;
            grvRK.DataBind();
            btnXTFP.Visible = false;
            UCPaging1.Visible = false;
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
                        GridViewRow fgr = GridViewSearch.Rows[i-1];
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
        #region 分页
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        void Pager_PageChanged(int pageNumber)
        {
            InitPager();
            GetData();
        }
        //初始化分页信息
        private void InitPager()
        {
            pager.TableName = "View_SM_IN";
            pager.PrimaryKey = "WG_CODE";
            pager.ShowFields = "WG_CODE,WG_COMPANY,left(WG_VERIFYDATE,10) as WG_DATE,(case when WG_GJSTATE='3' then 'B' ELSE 'N' END) as WG_GJSTATE,DocName,cast(sum(WG_RSNUM-isnull(WG_GJNUM,0)) as float) AS Num,cast(sum(WG_AMOUNT-isnull(WG_GJMONY,0)) as float) as Amount";
            pager.OrderField = "WG_CODE,WG_COMPANY,WG_VERIFYDATE,DocName,ReveicerName,WG_BILLTYPE ";
            pager.GroupField = "WG_CODE,WG_COMPANY,WG_VERIFYDATE,DocName,ReveicerName,WG_BILLTYPE,WG_GJSTATE";
            pager.StrWhere = ViewState["strWhere"].ToString();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 200;

            string sql = "select  isnull(sum(WG_RSNUM-isnull(WG_GJNUM,0)),0) as TotalNum,isnull(sum(WG_AMOUNT-isnull(WG_GJMONY,0)),0) as TotalAmount from View_SM_IN where " + pager.StrWhere;

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.Read())
            {
                hfdTotalNum.Value = sdr["TotalNum"].ToString();
                hfdTotalAmount.Value = sdr["TotalAmount"].ToString();
            }
            sdr.Close();
        }
        protected void GetData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamGroupBy(pager);
            CommonFun.Paging(dt, grvRK, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
                btnXTFP.Visible = false;
            }
            else
            {
                
                UCPaging1.Visible = true;               
                UCPaging1.InitPageInfo();
                btnXTFP.Visible = true;
            }
        }
        #endregion

        protected void grvRK_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "合计:";
                e.Row.Cells[7].Text = hfdTotalNum.Value;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[8].Text = string.Format("{0:c2}", Convert.ToDouble(hfdTotalAmount.Value));
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Center;
            }
        }


    }
}
