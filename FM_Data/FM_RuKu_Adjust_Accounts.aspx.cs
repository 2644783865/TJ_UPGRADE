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
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_RuKu_Adjust_Accounts : BasicPage
    {
      
        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);

            if (!IsPostBack)
            {
                bindDdlYear();

                InitPager();

                bindGV();//绑定条件框

                bindControl();

                bindGrid();
            }
            CheckUser(ControlFinder);
        }

        //绑定前五年

        private void bindDdlYear()
        {
            Dictionary<int, string> dt = new Dictionary<int, string>();

            int year =Convert.ToInt32(getYear());

            for (int i = 0; i < 5; i++)
            {
                dt.Add(year - i, "-" + (year - i).ToString()+"-");
            }

            ddl_year.DataSource = dt;
            ddl_year.DataTextField = "value";
            ddl_year.DataValueField = "key";
            ddl_year.DataBind();

            //选定今年
            ddl_year.ClearSelection();
            ddl_year.Items.FindByValue(getYear()).Selected = true;

            //选的本月
            ddl_month.ClearSelection();
            ddl_month.Items.FindByValue(getMonth()).Selected = true;

        }

        private void bindControl()
        {
            string sql = "select count(*) from TBFM_HSTAOTALIN where HS_YEAR='" + getYear() + "' and HS_MONTH='" + getMonth() + "' and HS_STATE='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (Convert.ToInt16(dt.Rows[0][0]) >= 0)
            {

                btn_hs.Enabled = true;
                btn_antihs.Enabled = false;
            }
            dt.Clear();
            //有未勾稽的发票不能入库核算
            sql = "select count(*) from TBFM_GHINVOICETOTAL where GI_GJFLAG='0'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                if (Convert.ToInt32(dr[0]) > 0)
                {
                    btn_hs.Enabled = false;
                    btn_antihs.Enabled = false;
                    LabelMessage.Visible = true;
                }
            }
            dr.Close();

            sql = "select count(*) from TBFM_HSTAOTALIN where HS_YEAR='" + getYear() + "' and HS_MONTH='" + getMonth() + "' and HS_STATE='1'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (Convert.ToInt16(dt.Rows[0][0]) > 0)
            {
                btn_hs.Enabled = false;
                btn_antihs.Enabled = true;
            }
            dt.Clear();

            sql = "select count(*) from TBFM_HSTOTAL where HS_YEAR='" + getYear() + "' and HS_MONTH='" + getMonth() + "' and HS_STATE='2'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (Convert.ToInt16(dt.Rows[0][0]) > 0)
            {
                //入库核算和反入库核算都不能用
                btn_hs.Enabled = false;
                btn_antihs.Enabled = false;
            }
            dt.Clear();

        }

        //初始化分布信息
        private void InitPager()
        {
            //显示已勾稽的入库单

            //string condition = "GJ_YEAR='" + ddl_year.SelectedValue + "' AND GJ_PERIOD='" + ddl_month.SelectedValue + "' and WG_GJSTATE='2'";

            string condition = "GI_DATE>='" + GetLastHSDate(ddl_year.SelectedValue, ddl_month.SelectedValue) + "' AND left(GI_DATE,7)<='" + GetNextHSDate(ddl_year.SelectedValue, ddl_month.SelectedValue) + "'";
            
            string SubCondtion = GetSubCondtion();

            if (SubCondtion != "")

                condition +=" AND (" + SubCondtion + ")";

            ////勾稽关系图和购货发票总表

            pager.TableName = "View_TBFM_GJ_INDETAIL";
            pager.PrimaryKey = "GI_ID";
            pager.ShowFields = "GI_INCOED,GI_CODE,GI_SUPPLIERNM,GI_DATE,GI_ZDNM,GI_MATCODE,GI_NAME,GI_GUIGE,GI_UNIT,GI_NUM,GI_INAMTMNY,GI_INCATAMTMNY";
            pager.OrderField = "GI_ID";
            pager.StrWhere =condition;
            pager.OrderType = 0;//项目编号的降序排列
            pager.PageSize = 20;

            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数  


            string sql = "select  isnull(sum(GI_INAMTMNY),0) as TotalAmount,isnull(sum(GI_INCATAMTMNY),0) as TotalCTAmount from View_TBFM_GJ_INDETAIL where " + condition;

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.Read())
            {
                hfdTotalAmount.Value = sdr["TotalAmount"].ToString();
                hfdTotalCTAmount.Value = sdr["TotalCTAmount"].ToString();
            }

            sdr.Close();

        }

        private string GetLastHSDate(string tYear,string tMonth)
        {
            string LastHSDate = string.Empty;

            string sqlstr = "SELECT HS_DATE from TBFM_HSTOTAL where HS_MONTH='" + getLastMonth(tMonth) + "' AND HS_YEAR='" + getLastYear(tYear, tMonth) + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlstr);
            if (dt.Rows.Count > 0)
            {
                LastHSDate = dt.Rows[0]["HS_DATE"].ToString();
            }
            if (string.IsNullOrEmpty(LastHSDate))
            {
                LastHSDate = getLastYear(tYear, tMonth) + "-" + getLastMonth(tMonth);
            }

            return LastHSDate;

        }
        private string GetNextHSDate(string tYear, string tMonth)
        {
            string NextHSDate = string.Empty;

            string sqlstr = "SELECT HS_DATE from TBFM_HSTOTAL where HS_MONTH='" + tMonth + "' AND HS_YEAR='" + tYear + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlstr);
            if (dt.Rows.Count > 0)
            {
                NextHSDate = dt.Rows[0]["HS_DATE"].ToString();
            }
            if (string.IsNullOrEmpty(NextHSDate))
            {
                NextHSDate = tYear + "-" + tMonth;
            }

            return NextHSDate;

        }


        private string getLastYear(string tYear,string tMonth)
        {
            string LastYear = string.Empty;

            if (getLastMonth(tMonth) == "12")
            {
                LastYear = (Convert.ToInt32(tYear) - 1).ToString();
            }
            else
            {
                LastYear = tYear;
            }
            return LastYear;
        }

        private string getLastMonth(string tMonth)
        {
            string LastMonth = string.Empty;

            if ((Convert.ToInt32(tMonth)-1).ToString().Length == 1)
            {
                if ((Convert.ToInt32(tMonth) - 1) == 0)
                {
                    LastMonth = "12";
                }
                else
                {
                    LastMonth = "0" + (Convert.ToInt32(tMonth) - 1).ToString();
                }
               
            }
            else
            {
                LastMonth = (Convert.ToInt32(tMonth) - 1).ToString();
            }

            return LastMonth;
        }


        private string getNextYear(string tYear,string tMonth)
        {
            string NextYear = string.Empty;

            if (getNextMonth(tMonth) == "01")
            {
                NextYear = (Convert.ToInt32(tYear) + 1).ToString();
            }
            else
            {
                NextYear = tYear;
            }

            return NextYear;
        }

        private string getNextMonth(string tMonth)
        {
            string NextMonth = string.Empty;

            if ((Convert.ToInt32(tMonth) + 1).ToString().Length == 1)
            {
                NextMonth = "0" + (Convert.ToInt32(tMonth) + 1).ToString();
            }
            else
            {
                if ((Convert.ToInt32(tMonth) + 1) == 13)
                {
                    NextMonth = "01";
                }
                else
                {
                    NextMonth = (Convert.ToInt32(tMonth) + 1).ToString();
                }
            }

            return NextMonth;
        }

        protected string getYear()
        {
            string ymdhms = System.DateTime.Now.ToString("yyyyMMdd");
            return ymdhms.Substring(0,4);
        }
        protected string getMonth()
        {
            string ymdhms = System.DateTime.Now.ToString("yyyyMMdd");
            return ymdhms.Substring(4, 2);
        }

        void Pager_PageChanged(int pageNumber)
        {

            InitPager();

            bindGrid();

        }
        private void bindGrid()
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

            BindItem();
            CheckUser(ControlFinder);
         }
      
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#Efefef'");//当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");//当鼠标移开时还原背景色---白色



                (e.Row.FindControl("hlTask") as HyperLink).Attributes.Add("onClick", "ShowViewModal('" + GridView1.DataKeys[e.Row.RowIndex]["GI_INCOED"].ToString() + "','" + GridView1.DataKeys[e.Row.RowIndex]["GI_CODE"].ToString() + "');");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "合计:";
                e.Row.Cells[9].Text = string.Format("{0:c2}", Convert.ToDouble(hfdTotalAmount.Value));
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[10].Text = string.Format("{0:c2}", Convert.ToDouble(hfdTotalCTAmount.Value));
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Center;
            }
        }

        //private void getZanGuBuCha() //获取暂估差金额,,入库单金额=发票金额-暂估差
        //{
        //    //核算后获取发票金额

        //    //暂估差额
        //    string strperiod = ddl_year.SelectedValue + "-" + ddl_month.SelectedValue;
        //    string condition = "CONVERT(varchar(100),(DIFYEAR+'-'+DIFMONTH), 23)  between '" + strperiod + "' and '" + strperiod + "'AND round(DIFAMTMNY,2)!=0";
            
        //    string sql = "select  CAST(round(isnull(sum(DIFAMTMNY),0),2) AS FLOAT) as TotalAmount from View_FM_INHSDETAILDIFMAR where " + condition;

        //    SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

        //    if (sdr.Read())
        //    {
        //        hfdZanGuChaAmount.Value = sdr["TotalAmount"].ToString();

        //    }

        //    sdr.Close();
        //}

        /*****************************入库核算***************************/
        protected void btn_hs_Click(object sender, EventArgs e)
        {
            string sqlifrksh = "select * from View_SM_IN where WG_STATE<>'2'";
            DataTable dtifrksh = DBCallCommon.GetDTUsingSqlText(sqlifrksh);
            if (dtifrksh.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('存在未审核的入库单!')", true);
                return;
            }



            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            DBCallCommon.openConn(sqlConn);
            SqlCommand sqlCmd = new SqlCommand("GouJiRuKuHeSuan", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.Add("@gmonth", SqlDbType.VarChar, 2);
            sqlCmd.Parameters["@gmonth"].Value = getMonth();

            sqlCmd.Parameters.Add("@gyear", SqlDbType.VarChar, 4);
            sqlCmd.Parameters["@gyear"].Value = getYear();

            sqlCmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;			//增加返回值参数@retVal

            sqlCmd.ExecuteNonQuery();

            DBCallCommon.closeConn(sqlConn);

            if (Convert.ToInt32(sqlCmd.Parameters["@retVal"].Value) == 0)
            {
                string script = @"alert('入库核算成功!');location.href ='FM_RuKu_Adjust_Accounts.aspx'";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
            }
            else if (Convert.ToInt32(sqlCmd.Parameters["@retVal"].Value) == 1)
            {
                string script = @"alert('入库核算失败!');'";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
            }
        }

        protected void btn_antihs_Click(object sender, EventArgs e)
        {


            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            DBCallCommon.openConn(sqlConn);
            SqlCommand sqlCmd = new SqlCommand("GouJiRuKuHeSuanAnti", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = 0;
            sqlCmd.Parameters.Add("@gmonth", SqlDbType.VarChar, 2);
            sqlCmd.Parameters["@gmonth"].Value = getMonth();

            sqlCmd.Parameters.Add("@gyear", SqlDbType.VarChar, 4);
            sqlCmd.Parameters["@gyear"].Value = getYear();

            sqlCmd.Parameters.Add("@retVal", SqlDbType.Int, 1).Direction = ParameterDirection.ReturnValue;			//增加返回值参数@retVal

            sqlCmd.ExecuteNonQuery();

            DBCallCommon.closeConn(sqlConn);

            if (Convert.ToInt32(sqlCmd.Parameters["@retVal"].Value) == 0)
            {
                string script = @"alert('反入库核算成功!');location.href ='FM_RuKu_Adjust_Accounts.aspx'";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
            }
            else if (Convert.ToInt32(sqlCmd.Parameters["@retVal"].Value) == 1)
            {
                string script = @"alert('反入库核算失败!');'";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
            }

        }

        protected void bindVisual()
        {
            if (ddl_year.SelectedValue==getYear()&&ddl_month.SelectedValue == getMonth())
            {
                 btn_hs.Visible = true; 
            }
            else
            {
                 btn_hs.Visible = false;
            }
        }

        private void BindItem()
        {

            for (int i = 0; i < (GridView1.Rows.Count - 1); i++)
            {
                string Code = GridView1.Rows[i].Cells[1].Text;

                if (Code!=string.Empty)
                {
                    for (int j = i + 1; j < GridView1.Rows.Count; j++)
                    {
                        string NextCode = GridView1.Rows[j].Cells[1].Text;

                        if (NextCode == Code)
                        {
                            GridView1.Rows[j].Cells[1].Text = "";
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }


        protected void Query_Click(object sender, EventArgs e)
        {
            InitPager();

            UCPaging1.CurrentPage = 1;

            bindGrid();

            refreshStyle();

            ModalPopupExtenderSearch.Hide();

            bindVisual();

            UpdatePanelBody.Update();

            
        }

        //关闭
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }
        //重置
        protected void btnReset_Click(object sender, EventArgs e)
        {

            clearCondition();
            resetSubcondition();
        }

          //清除条件
        private void clearCondition()
        {

            //选定今年
            ddl_year.ClearSelection();
            ddl_year.Items.FindByValue(getYear()).Selected = true;

            //选的本月
            ddl_month.ClearSelection();
            ddl_month.Items.FindByValue(getMonth()).Selected = true;

        }

        #region 条件框

        private void bindGV()
        {
            GridViewSearch.DataSource = CreateTable();

            GridViewSearch.DataBind();
        }
        private DataTable CreateTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("index", typeof(int)));


            for (int i = 0; i < 7; i++)
            {
                DataRow row = dt.NewRow();
                row["index"] = i;
                dt.Rows.Add(row);
            }

            return dt;
        }
        private Dictionary<string, string> bindItemList()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();
            ItemList.Add("NO", "");
            ItemList.Add("GI_INCOED", "入库单号");
            ItemList.Add("GI_CODE", "发票单号");
            ItemList.Add("GI_SUPPLIERNM", "供应商");
            //GI_MATCODE,GI_NAME
            ItemList.Add("GI_MATCODE", "物料编码");
            ItemList.Add("GI_NAME", "物料名称");
            return ItemList;
        }




        protected void GridViewSearch_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                ddl.DataTextField = "value";
                ddl.DataValueField = "key";
                ddl.DataSource = bindItemList();
                ddl.DataBind();

                if (gr.RowIndex == 0)
                {
                    (gr.FindControl("tb_logic") as TextBox).Visible = false;
                }
            }
        }

        protected string GetSubCondtion()
        {
            string subCondition = "";

            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                if (gr.RowIndex == 0)
                {

                    DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                    if (ddl.SelectedValue != "NO")
                    {
                        TextBox txtValue = (gr.FindControl("TextBoxValue") as TextBox);

                        DropDownList ddlRel = (gr.FindControl("DropDownListRelation") as DropDownList);

                        subCondition = ConvertRelation(ddl.SelectedValue, ddlRel.SelectedValue, txtValue.Text.Trim());
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                    if (ddl.SelectedValue != "NO")
                    {
                        DropDownList ddlLogic = (gr.FindControl("DropDownListLogic") as DropDownList);

                        TextBox txtValue = (gr.FindControl("TextBoxValue") as TextBox);

                        DropDownList ddlRel = (gr.FindControl("DropDownListRelation") as DropDownList);

                        subCondition += " " + ddlLogic.SelectedValue + " " + ConvertRelation(ddl.SelectedValue, ddlRel.SelectedValue, txtValue.Text.Trim());
                    }

                    else
                    {
                        break;
                    }
                }
            }
            return subCondition;
        }

        private string ConvertRelation(string field, string relation, string fieldValue)
        {
            string obj = string.Empty;

            if (field == "GI_INSTOREID")
            {
                fieldValue = fieldValue.PadLeft(9, '0');
            }

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

        private void resetSubcondition()
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                DropDownList ddl = gr.FindControl("DropDownListName") as DropDownList;
                foreach (ListItem lt in ddl.Items)
                {
                    if (lt.Selected)
                        lt.Selected = false;
                }
                ddl.Items[0].Selected = true;
                (gr.FindControl("TextBoxValue") as TextBox).Text = string.Empty; ;
            }

            refreshStyle();
        }
        private void refreshStyle()
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                if ((gr.FindControl("DropDownListName") as DropDownList).SelectedValue != "NO")
                {
                    if (gr.RowIndex != 0)
                    {
                        (gr.FindControl("DropDownListLogic") as DropDownList).Style.Add("display", "block");
                        (gr.FindControl("tb_logic") as TextBox).Style.Add("display", "none");
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

        #endregion

    }
}
