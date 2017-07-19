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
    public partial class FM_Invoice_Managemnt : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["action"] != null)
                {
                    if (Request.QueryString["action"] == "SearchUpOrDown")
                    {
                        txtCODE.Text = Request.QueryString["id"];
                        txtEndTime.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
                    }
                }

                //this.BindSF();
                //this.BindDQ("");
                //this.BindQYS("","");
                this.InitVar();
                this.bindGrid();
            }
            if (IsPostBack)
            {
                this.InitVar();
            }
            CheckUser(ControlFinder);
        }
        /// <summary>
        /// 供应商省份
        /// </summary>
        //private void BindSF()
        //{
        //    string sqltext="select distinct CL_NAME,CL_CODE from TBCS_LOCINFO where CL_FATHERCODE='ROOT'";
        //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
        //    dplSF.DataSource = dt;
        //    dplSF.DataTextField = "CL_NAME";
        //    dplSF.DataValueField = "CL_CODE";
        //    dplSF.DataBind();
        //    dplSF.Items.Insert(0,new ListItem("-全部-",""));
        //}
        /// <summary>
        /// 供应商地区
        /// </summary>
        //private void BindDQ(string sf)
        //{
        //    dplDQ.Items.Clear();
        //    if (sf == "")
        //    {
        //        dplDQ.Items.Add(new ListItem("-全部-", ""));
        //        dplDQ.SelectedIndex = 0;
        //    }
        //    else
        //    {
        //        string sqltext = "select distinct CL_NAME from TBCS_LOCINFO where CL_FATHERCODE='" + sf + "'";
        //        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
        //        dplDQ.DataSource = dt;
        //        dplDQ.DataTextField = "CL_NAME";
        //        dplDQ.DataValueField = "CL_NAME";
        //        dplDQ.DataBind();
        //        dplDQ.Items.Insert(0, new ListItem("-全部-", ""));
        //        dplDQ.SelectedIndex = 0;
        //    }
        //}
        /// <summary>
        /// 供应商
        /// </summary>
        /// <param name="sf_dq"></param>
        //private void BindQYS(string sf,string dq)
        //{
        //    string sf_dq ="%"+sf + dq+"%";
        //    string sqltext = "select CS_CODE,CS_NAME from TBCS_CUSUPINFO where CS_LOCATION LIKE '" + sf_dq + "'";
        //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
        //    dplGYS.DataSource = dt;
        //    dplGYS.DataTextField = "CS_NAME";
        //    dplGYS.DataValueField = "CS_CODE";
        //    dplGYS.DataBind();
        //    dplGYS.Items.Insert(0, new ListItem("全部", "%"));
        //    dplGYS.SelectedIndex = 0;
        //}

        #region "数据查询，分页"
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager.TableName = "Inv_View";
            pager.PrimaryKey = "GI_CODE";
            pager.ShowFields = "";
            pager.OrderField = "GI_CODE";
            pager.StrWhere = this.GetSqlText();
            pager.OrderType = 1;//按时间升序序排列
            pager.PageSize = 30;
            //pager.PageIndex = 1;

            GetTotalAmount(pager.StrWhere);
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt,grvInv, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
            CheckUser(ControlFinder);
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns></returns>
        private string GetSqlText()
        {

            string sqltext = string.Empty;

            //供应商
            if (tb_supply.Text != "")
            {
                sqltext += "GI_SUPPLIERNM='" + tb_supply.Text + "'";
            }
            //审核
            if (rblSSSH.SelectedItem.Text != "全部" && sqltext!=string.Empty)
            {
                sqltext += " AND GI_STATE='" + rblSSSH.SelectedValue + "'";
            }
            else if (rblSSSH.SelectedItem.Text!= "全部" && sqltext == string.Empty)
            {
                sqltext += "GI_STATE='" + rblSSSH.SelectedValue + "'";
            }
            //勾稽
            if (rblSSGJ.SelectedItem.Text != "全部" && sqltext != string.Empty)
            {
                sqltext += " AND GI_GJFLAG='" + rblSSGJ.SelectedValue + "'";
            }
            else if (rblSSGJ.SelectedItem.Text != "全部" && sqltext == string.Empty)
            {
                sqltext += "GI_GJFLAG='" + rblSSGJ.SelectedValue + "'";
            }
            //编号
            if (txtCODE.Text != "" && sqltext != string.Empty)
            {
                sqltext += " AND GI_CODE like '%" + txtCODE.Text + "%'";
            }
            else if (txtCODE.Text != "" && sqltext == string.Empty)
            {
                sqltext += "GI_CODE like '%" + txtCODE.Text + "%'";
            }

            //发票编号
            if (txtfpCode.Text != "" && sqltext != string.Empty)
            {
                sqltext += " AND GI_INVOICENO like '%" + txtfpCode.Text + "%'";
            }
            else if (txtfpCode.Text != "" && sqltext == string.Empty)
            {
                sqltext += "GI_INVOICENO like '%" + txtfpCode.Text + "%'";
            }

            //凭证号
            if (txtpzh.Text != "" && sqltext != string.Empty)
            {
                sqltext += " AND GI_PZH like '%" + txtpzh.Text + "%'";
            }
            else if (txtpzh.Text != "" && sqltext == string.Empty)
            {
                sqltext += "GI_PZH like '%" + txtpzh.Text + "%'";
            }


            if(sqltext != "")
            {
                if ((txtStartTime.Text.Trim() == "") && (txtEndTime.Text.Trim() == ""))
                {
                    //如果不选时间，默认为本期。
                    sqltext += " AND " + " GI_DATE like '" + System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString().PadLeft(2, '0') + "%'";//全部
                }
                if ((txtStartTime.Text.Trim() == "") && (txtEndTime.Text.Trim() != ""))
                {
                    sqltext += " AND " + " GI_DATE <= '" + txtEndTime.Text.Trim() + "'";//
                }
                if ((txtStartTime.Text.Trim() != "") && (txtEndTime.Text.Trim() == ""))
                {
                    sqltext += " AND " + " GI_DATE >= '" + txtStartTime.Text.Trim() + "'";//
                }
                if ((txtStartTime.Text.Trim() != "") && (txtEndTime.Text.Trim() != ""))
                {
                    sqltext += " AND " + " GI_DATE between '" + txtStartTime.Text.Trim() + "' and '" + txtEndTime.Text.Trim() + "'";
                }
            }
            else
            {
                if ((txtStartTime.Text.Trim() == "") && (txtEndTime.Text.Trim() == ""))
                {
                    //如果不选时间，默认为本期。
                    sqltext += " GI_DATE like '" + System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString().PadLeft(2, '0') + "%'";//全部
                }
                if ((txtStartTime.Text.Trim() == "") && (txtEndTime.Text.Trim() != ""))
                {
                    sqltext += " GI_DATE <= '" + txtEndTime.Text.Trim() + "'";//
                }
                if ((txtStartTime.Text.Trim() != "") && (txtEndTime.Text.Trim() == ""))
                {
                    sqltext += " GI_DATE >= '" + txtStartTime.Text.Trim() + "'";//
                }
                if ((txtStartTime.Text.Trim() != "") && (txtEndTime.Text.Trim() != ""))
                {
                    sqltext += " GI_DATE between '" + txtStartTime.Text.Trim() + "' and '" + txtEndTime.Text.Trim() + "'";
                }
            }

            return sqltext;
        }
        #endregion

        private void GetTotalAmount(string strWhere)
        {
            string sql = "select isnull(CAST(round(sum(GI_AMTMNY),2) AS FLOAT),0) as TotalAmount,isnull(CAST(round(sum(GI_SE),2) AS FLOAT),0) as TotalSE,isnull(CAST(round(sum(GI_CTAMTMNY),2) AS FLOAT),0) as TotalCTAmount from Inv_View where " + strWhere;

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);
            
            if (sdr.Read())
            {
                hfdTotalAmount.Value = sdr["TotalAmount"].ToString();

                hfdTotalSE.Value = sdr["TotalSE"].ToString();

                hfdTotalCTAmount.Value = sdr["TotalCTAmount"].ToString();
            }
            sdr.Close();
        }
        
        protected void tb_supply_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (tb_supply.Text.ToString().Contains("|"))
            {
                Cname = tb_supply.Text.Substring(0, tb_supply.Text.ToString().IndexOf("|"));
                tb_supply.Text = Cname.Trim();
            }
            else if (tb_supply.Text == "")
            {

            }
        }

        protected void grvInv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "合计:";
                e.Row.Cells[4].Text = string.Format("{0:c2}", Convert.ToDouble(hfdTotalAmount.Value));
                e.Row.Cells[5].Text = string.Format("{0:c2}", Convert.ToDouble(hfdTotalSE.Value));
                e.Row.Cells[6].Text = string.Format("{0:c2}", Convert.ToDouble(hfdTotalCTAmount.Value));
            }
        }
        //省份改变
        //protected void dplSF_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    this.BindDQ(dplSF.SelectedValue.ToString());
        //    if (dplSF.SelectedIndex == 0)
        //    {
        //        this.BindQYS("", dplDQ.SelectedValue.ToString());
        //    }
        //    else
        //    {
        //        this.BindQYS(dplSF.SelectedItem.Text, dplDQ.SelectedValue.ToString());
        //    }
        //}
        ////地区改变
        //protected void dplDQ_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (dplSF.SelectedIndex == 0)
        //    {
        //        this.BindQYS("", dplDQ.SelectedValue.ToString());
        //    }
        //    else
        //    {
        //        this.BindQYS(dplSF.SelectedItem.Text, dplDQ.SelectedValue.ToString());
        //    }
        //    ////this.BindInvTotal();
        //    //UCPaging1.CurrentPage = 1;
        //    //this.InitVar();
        //    //this.bindGrid();
        //}
        //供应商
        protected void dplGYS_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }
     
        //钩稽
        protected void rblSSGJ_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }
        //查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }
        //全部
        protected void btnAllData_Click(object sender, EventArgs e)
        {
           
            pager.StrWhere = string.Empty;
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }

        //到入库单
        protected void btnRKD_Click(object sender, EventArgs e)
        {
            Response.Redirect("FM_FromRK_ToInv.aspx");
        }

        protected void btnSC_Click(object sender,EventArgs e)
        {
            string code = string.Empty;

            string ObjCode = string.Empty;

            foreach (GridViewRow gr in grvInv.Rows)
            {
                CheckBox cbx = (CheckBox)gr.FindControl("checkbox");
                if (cbx.Checked)
                {
                    code = gr.Cells[1].Text.Trim();
                }

            }
            if (code == string.Empty)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('未选中相应单据!')</script>");
                return;
            }
            else
            {
                string sql = "select GI_INSTOREID from TBFM_GJRELATION where GJ_INVOICEID='" + code + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ObjCode += dt.Rows[i]["GI_INSTOREID"].ToString();
                        ObjCode += "-";
                    }


                    ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>SearchUp('" + ObjCode + "');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('无关联单据信息!')</script>");
                }

            }
        }


        protected void btngjlxcx_Click(object sender, EventArgs e)
        {
            Response.Redirect("FM_FPFL.aspx");
        }
    }
}
