using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Text;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_CRUndo : BasicPage
    {        
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                ViewState["sqltext"] = "1=1";
                this.InitVar();
                InitPage();
                this.bindGrid();
                this.BindUndoBus();
                this.Select_record();
            }
            this.InitVar();
            //CheckUser(ControlFinder);
        }

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
            pager.TableName = "View_CM_DBQK";
            pager.PrimaryKey = "CR_ID";
            pager.ShowFields = "*";
            pager.OrderField = "CR_HTBH";
            pager.StrWhere = ViewState["sqltext"].ToString();
            pager.OrderType = 1;//升序序排列
            pager.PageSize = 10;
            //pager.PageIndex = 1;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        //待办请款
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, grvFKJL, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
            //CheckUser(ControlFinder);
        }

        //显示筛选后的记录条数和合计金额
        private void Select_record()
        {
            string sqltext = "select CR_BQSFK,CR_BQYZF from View_CM_DBQK where " + ViewState["sqltext"].ToString();
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            lb_select_num.Text = dt.Rows.Count.ToString();
            decimal tot_money = 0;
            decimal tot_yzf = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tot_money += Convert.ToDecimal(dt.Rows[i]["CR_BQSFK"].ToString());
                tot_yzf += Convert.ToDecimal(dt.Rows[i]["CR_BQYZF"].ToString());
            }
            lb_select_money.Text = string.Format("{0:c}", tot_money);
            lb_select_hjdf.Text = string.Format("{0:c}", tot_money-tot_yzf);
        }

        private void InitPage()
        {            
            //部门
            string sqltext_bm = "select distinct DEP_NAME from View_CM_DBQK";
            DataTable dt_bm = DBCallCommon.GetDTUsingSqlText(sqltext_bm);
            dplBM.DataSource = dt_bm;
            dplBM.DataTextField = "DEP_NAME";
            dplBM.DataValueField = "DEP_NAME";
            dplBM.DataBind();
            dplBM.Items.Insert(0, new ListItem("-请选择-", "%"));
            dplBM.SelectedIndex = 0;

            //请款人
            string sqltext_qkr = "select distinct(CR_JBR) from View_CM_DBQK ";
            DataTable dt_qkr = DBCallCommon.GetDTUsingSqlText(sqltext_qkr);
            if (dt_qkr.Rows.Count > 0)
            {
                dplQKR.DataSource = dt_qkr;
                dplQKR.DataTextField = "CR_JBR";
                dplQKR.DataValueField = "CR_JBR";
                dplQKR.DataBind();
            }
            dplQKR.Items.Insert(0, new ListItem("-请选择-", ""));
            dplQKR.SelectedIndex = 0;

            //销售合同负责人（收款）
            string sqltext_SKR = "select distinct(PCON_RESPONSER) from TBPM_CONPCHSINFO where PCON_FORM=0" +
                             "AND PCON_BCODE IN (SELECT DISTINCT BP_HTBH FROM TBPM_BUSPAYMENTRECORD WHERE BP_STATE='0')";
            DataTable dt_SKR = DBCallCommon.GetDTUsingSqlText(sqltext_SKR);
            dplFZR.DataSource = dt_SKR;
            dplFZR.DataTextField = "PCON_RESPONSER";
            dplFZR.DataValueField = "PCON_RESPONSER";
            dplFZR.DataBind();
            dplFZR.Items.Insert(0, new ListItem("-请选择-", "%"));
            dplFZR.SelectedIndex = 0;
        }
        
        //获取查询条件
        private void  GetSqlText()
        {           
            StringBuilder strb = new StringBuilder();
            strb.Append("CR_HTBH IS NOT NULL ");
            //项目            
            strb.Append(" AND PCON_PJNAME like '%" + txt_XMMC.Text.Trim() + "%' ");            
            
            //部门
            if (dplBM.SelectedIndex != 0)
            {
                strb.Append(" AND DEP_NAME='" + dplBM.SelectedItem.Text + "'");
            }
            //合同类别
            if (dplType.SelectedIndex != 0)
            {
                strb.Append(" AND CR_HTLB=" + dplType.SelectedValue.ToString() + "");
            }
           
            //请款人
            if (dplQKR.SelectedIndex != 0)
            {
                strb.Append(" AND CR_JBR='" + dplQKR.SelectedItem.Text + "'");
            }
            ////请款时间
            string startTime = txtStartTime.Text.Trim() == "" ? DateTime.Now.AddYears(-100).ToShortDateString() : txtStartTime.Text.Trim();
            string endTime = txtEndTime.Text.Trim() == "" ? DateTime.Now.AddYears(100).ToShortDateString() : txtEndTime.Text.Trim();
            //合同号
            string hth = txtHTH.Text.Trim();

            //供应商
            string custmname = tb_CUSTMNAME.Text.Trim();
            strb.Append(" and CR_HTBH like '%" + hth + "%' and CR_DATE>='" + startTime + "' AND CR_DATE<='" + endTime + "' and CUSTMNAME like '%" + custmname + "%'");
            ViewState["sqltext"] = strb.ToString();
            
        }        
       
        //查询Button btnQuery_OnClick
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            this.GetSqlText();
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindGrid();

            this.Select_record(); 
        }
        
        //重置
        protected void Btn_Reset_FK_Click(object sender, EventArgs e)
        {
            dplBM.SelectedIndex = 0;
            dplQKR.SelectedIndex = 0;
            dplType.SelectedIndex = 0;
            txt_XMMC.Text = "";
            txtHTH.Text = "";
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            tb_CUSTMNAME.Text = "";
            this.btnQuery_OnClick(null, null);
        }

        /// <summary>
        /// 财务待办商务要款
        /// </summary>
        private void BindUndoBus()
        {
            string sqlstr="";
            sqlstr = "select a.BP_ID,a.BP_KXMC,a.BP_JE,a.BP_YKRQ,b.PCON_YFK,b.PCON_JINE,b.PCON_PJNAME as PCON_PJNAME," +
            " b.PCON_BCODE,PCON_YZHTH,PCON_NAME,PCON_RESPONSER,PCON_CUSTMNAME from TBPM_BUSPAYMENTRECORD as a inner join TBPM_CONPCHSINFO as b on a.BP_HTBH=b.PCON_BCODE  where a.BP_STATE='0'";

            if (search_box.Value != "项目或合同号")
            {
                sqlstr += " and ( b.PCON_BCODE like '%" + search_box.Value + "%' or b.PCON_PJNAME like '%" + search_box.Value + "%' )";
            }

            //客户
            sqlstr += " and ( PCON_CUSTMNAME like '%" + txt_KH.Text.Trim() + "%')";

            //负责人
            if (dplFZR.SelectedIndex != 0)
            {
                sqlstr += " and PCON_RESPONSER='" + dplFZR.SelectedValue.ToString() + "'";
            }

            DataTable dt_dbyk = DBCallCommon.GetDTUsingSqlText(sqlstr);
            if (dt_dbyk.Rows.Count > 0)
            {
                Panel1.Visible = false;
                grvDBSWYK.DataSource = dt_dbyk;
                grvDBSWYK.DataBind();
            }
            else
            {
                grvDBSWYK.DataSource = null;
                grvDBSWYK.DataBind();
                Panel1.Visible = true;
            }

            //分页是否可见
            if (this.grvDBSWYK.PageCount > 0)
            {
                this.txt_goto.Text = this.lbl_currentpage.Text = (this.grvDBSWYK.PageIndex + 1).ToString();
                this.lbl_totalpage.Text = this.grvDBSWYK.PageCount.ToString();
                Pal_page.Visible = true;
                if (this.grvDBSWYK.PageIndex == 0)
                {
                    this.lnkbtnFrist.Enabled = false;
                    this.lnkbtnPre.Enabled = false;
                }
                else
                {
                    this.lnkbtnFrist.Enabled = true;
                    this.lnkbtnPre.Enabled = true;
                }
                if (this.grvDBSWYK.PageIndex == this.grvDBSWYK.PageCount - 1)
                {
                    this.lnkbtnNext.Enabled = false;
                    this.lnkbtnLast.Enabled = false;
                }
                else
                {
                    this.lnkbtnNext.Enabled = true;
                    this.lnkbtnLast.Enabled = true;
                }
            }
            else
            {
                Pal_page.Visible = false;
            }


            //筛选结果统计
            lb_select_num2.Text = dt_dbyk.Rows.Count.ToString();
            decimal tot_money = 0;

            for (int i = 0; i < dt_dbyk.Rows.Count; i++)
            {
                tot_money += Convert.ToDecimal(dt_dbyk.Rows[i]["BP_JE"].ToString());
            }
            lb_select_money2.Text = string.Format("{0:c}", tot_money);
            //CheckUser(ControlFinder);
        }

        
        //待办商务要款
        protected void Btn_Query_Click(object sender, EventArgs e)
        {
            this.BindUndoBus();
        }

        //重置要款筛选
        protected void Btn_Reset_SK_Click(object sender, EventArgs e)
        {
            search_box.Value = "项目或合同号";
            dplFZR.SelectedIndex = 0;
            txt_KH.Text = "";
            this.Btn_Query_Click(null, null);
        }

        protected void grvFKJL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lbl_htbh = (Label)e.Row.FindControl("lbl_htbh");//合同编号                
                Label lbl_QKDH = (Label)e.Row.FindControl("lbl_QKDH");
                string qkdh = lbl_QKDH.Text.Trim();
                Label lbl_QKQC = (Label)e.Row.FindControl("lbl_QKQC");
                if (lbl_htbh.Text.Trim() != "")
                {

                    lbl_QKQC.Text = qkdh.Substring(qkdh.Length - 1, 1);
                }

                Label lblIndex = (Label)e.Row.FindControl("lblIndex");//序号
                lblIndex.Text = ((this.pager.PageIndex - 1) * this.pager.PageSize + e.Row.RowIndex + 1).ToString();
                e.Row.Attributes["style"] = "Cursor:pointer";
                e.Row.Attributes.Add("onclick", "RowClick(this)");
            }
        }

        protected void grvDBSWYK_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                e.Row.Attributes["style"] = "Cursor:pointer";
                e.Row.Attributes.Add("onclick", "RowClick(this)");
            }
        }

       #region 商务要款分页
       
        protected void lnkbtnGoto_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(txt_goto.Text.ToString());
            if (index <= grvDBSWYK.PageCount && index > 0)
            {
                this.grvDBSWYK.PageIndex = index - 1;
                this.BindUndoBus();
            }
        }
        protected void lnkbtnFrist_Click(object sender, EventArgs e)
        {
            this.grvDBSWYK.PageIndex = 0;
            this.BindUndoBus();
        }
        protected void lnkbtnPre_Click(object sender, EventArgs e)
        {
            if (this.grvDBSWYK.PageIndex > 0)
            {
                this.grvDBSWYK.PageIndex = this.grvDBSWYK.PageIndex - 1;
                this.BindUndoBus();
            }
        }
        protected void lnkbtnNext_Click(object sender, EventArgs e)
        {
            if (this.grvDBSWYK.PageIndex < this.grvDBSWYK.PageCount)
            {
                this.grvDBSWYK.PageIndex = this.grvDBSWYK.PageIndex + 1;
                this.BindUndoBus();
            }
        }
        protected void lnkbtnLast_Click(object sender, EventArgs e)
        {
            this.grvDBSWYK.PageIndex = this.grvDBSWYK.PageCount;
            this.BindUndoBus();
        }
 
        #endregion 
    }
}
