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
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_CheckRequestRecord : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = btnQuery.UniqueID;
            if (!IsPostBack)
            {
                this.InitPage();
                ViewState["sqltext"] = "1=1";
                this.btnFKQuery_Click(null, null);                
                BindBusPayment(); 
                
            }
            this.InitVar();
            //CheckUser(ControlFinder);
        }

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
            pager.TableName = "CheckRequset";
            pager.PrimaryKey = "ZCDH";
            pager.ShowFields = "*";
            pager.OrderField = "HTBH,PJNAME,ENGNAME";
            pager.StrWhere = ViewState["sqltext"].ToString();
            pager.OrderType = 0;//按时间升序序排列
            pager.PageSize = 10;            
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, grvQKJL, UCPaging1, palNoData);
            if (palNoData.Visible)
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
            string sqltext = "select ZFJE from CheckRequset where " + ViewState["sqltext"].ToString();
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            lb_select_num.Text = dt.Rows.Count.ToString();
            decimal tot_money = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tot_money += Convert.ToDecimal(dt.Rows[i]["ZFJE"].ToString());
            }
            lb_select_money.Text = string.Format("{0:c}", tot_money);
        }
        #endregion

        private void InitPage()
        {
            //部门
            string sqltext_bm = "select distinct QKBM from CheckRequset";
            DataTable dt_bm = DBCallCommon.GetDTUsingSqlText(sqltext_bm);
            dplBM.DataSource = dt_bm;
            dplBM.DataTextField = "QKBM";
            dplBM.DataValueField = "QKBM";
            dplBM.DataBind();
            dplBM.Items.Insert(0, new ListItem("-请选择-", "%"));
            dplBM.SelectedIndex = 0;

           //请款人
            string sqltext = "select distinct(QKR) from CheckRequset";
           DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);           
           if (dt.Rows.Count > 0)
           {
               dplQKR.DataSource = dt;
               dplQKR.DataTextField = "QKR";
               dplQKR.DataValueField = "QKR";
               dplQKR.DataBind();
           }
           dplQKR.Items.Insert(0, new ListItem("-请选择-", ""));
           dplQKR.SelectedIndex = 0;

            //销售合同负责人（收款）
           string sqltext_SKR = "select distinct(PCON_RESPONSER) from TBPM_CONPCHSINFO where PCON_FORM=0"+
                            "AND PCON_BCODE IN (SELECT DISTINCT BP_HTBH FROM TBPM_BUSPAYMENTRECORD WHERE BP_STATE='1')";
           DataTable dt_SKR = DBCallCommon.GetDTUsingSqlText(sqltext_SKR);
           dplFZR.DataSource = dt_SKR;
           dplFZR.DataTextField = "PCON_RESPONSER";
           dplFZR.DataValueField = "PCON_RESPONSER";
           dplFZR.DataBind();
           dplFZR.Items.Insert(0, new ListItem("-请选择-", "%"));
           dplFZR.SelectedIndex = 0;
           
        }
        
        //获取查询条件
        private void GetSqlText()
        {            
            StringBuilder strb = new StringBuilder();
            strb.Append(" 1=1 ");
                      
            //部门
            if (dplBM.SelectedIndex != 0)
            {
                strb.Append(" AND QKBM='" + dplBM.SelectedItem.Text + "'");
            }
            //合同类别
            if (dplType.SelectedIndex != 0)
            {
                strb.Append(" AND HTLB='" + dplType.SelectedValue.ToString() + "'");
            }
            
            //请款人
            if (dplQKR.SelectedIndex != 0)
            {
                strb.Append(" AND QKR='"+dplQKR.SelectedItem.Text+"'");
            }
            //付款时间
            string startTime = txtStartTime.Text.Trim() == "" ? DateTime.Now.AddYears(-100).ToShortDateString() : txtStartTime.Text.Trim();
            string endTime = txtEndTime.Text.Trim() == "" ? DateTime.Now.AddYears(100).ToShortDateString() : txtEndTime.Text.Trim();
            
            //合同号、合同号
            if (search_Pay.Value != "项目或合同号")
            {
                strb.Append(" and (PJNAME like '%" + search_Pay.Value + "%' or HTBH like '%" + search_Pay.Value + "%')");
            }

            //收款单位
            strb.Append(" and SKDW like '%" + txtSKDW.Text.Trim() + "%'");

            //付款时间
            strb.Append(" and ZCRQ>='" + startTime + "' AND ZCRQ<='" + endTime + "'");

            //有无凭证
            if (ddl_pz.SelectedIndex > 0)
            {
                strb.Append(" and PZ=" +Convert.ToInt16(ddl_pz.SelectedValue.ToString()) + "");
            }
            ViewState["sqltext"] = strb.ToString();            
        }
       
       
        //查询付款
        protected void btnFKQuery_Click(object sender, EventArgs e)
        {
            this.GetSqlText();
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
            this.Select_record();
        }

        //重置付款
        protected void btn_reset_fk_Click(object sender, EventArgs e)
        {
            dplBM.SelectedIndex = 0;
            dplQKR.SelectedIndex = 0;
            dplType.SelectedIndex = 0;
            search_Pay.Value = "项目或合同号";
            ddl_pz.SelectedIndex = 0;
            txtSKDW.Text = "";
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            this.btnFKQuery_Click(null, null);
        }
        
        protected void grvQKJL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //相同请款单的重复内容置空
            
            if (e.Row.RowIndex >= 0)
            {
                for (int i = 0; i < grvQKJL.Rows.Count - 1; i++)
                {
                   
                    Label qkdhi = (Label)grvQKJL.Rows[i].FindControl("lblQKDH");
                    for (int j = i + 1; j < grvQKJL.Rows.Count; j++)
                    {
                        Label qkdhj = (Label)grvQKJL.Rows[j].FindControl("lblQKDH");
                        if (qkdhi.Text.Trim()==qkdhj.Text.Trim())
                        {
                            for (int k = 1; k < grvQKJL.Columns.Count; k++)
                            {
                                if (k != 7 && k != 8 && k != 9 && k != 14 && k != 15 && k != 17)
                                {
                                    grvQKJL.Rows[j].Cells[k].Text = "&nbsp;";
                                }
                            }
                        }
                    }
                }
                Label lblID = (Label)e.Row.FindControl("lblID");//序号
                lblID.Text = ((this.pager.PageIndex - 1) * this.pager.PageSize + e.Row.RowIndex + 1).ToString();
                //单击行变色
                e.Row.Attributes.Add("onclick", "RowClick(this)");
                e.Row.Attributes["style"] = "Cursor:pointer";
            }           
        }

        protected void grvSKJL_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //相同请款单的重复内容置空

            if (e.Row.RowIndex >= 0)
            {               
                
                //单击行变色
                e.Row.Attributes.Add("onclick", "RowClick(this)");
                e.Row.Attributes["style"] = "Cursor:pointer";
            }
        }


        private void BindBusPayment()
        {
            string sqlstr = "";

            sqlstr = "select a.BP_ID,a.BP_KXMC,a.BP_JE,a.BP_YKRQ,a.BP_SKRQ,a.BP_SFJE,b.PCON_YFK,b.PCON_JINE,b.PCON_PJNAME as PCON_PJNAME,b.PCON_BCODE ,PCON_YZHTH,PCON_NAME,PCON_RESPONSER,PCON_CUSTMNAME,BP_PZ from TBPM_BUSPAYMENTRECORD as a inner join TBPM_CONPCHSINFO as b on a.BP_HTBH=b.PCON_BCODE  where a.BP_STATE='1'";

            if (search_Get.Value != "项目或合同号")
            {
                sqlstr += " and ( b.PCON_BCODE like '%" + search_Get.Value + "%' or b.PCON_PJNAME like '%" + search_Get.Value + "%' )";
            }
            //收款日期            
            string startTime = txt_SK_starttime.Text.Trim() == "" ? DateTime.Now.AddYears(-100).ToShortDateString() : txt_SK_starttime.Text.Trim();
            string endTime = txt_SK_endtime.Text.Trim() == "" ? DateTime.Now.AddYears(100).ToShortDateString() : txt_SK_endtime.Text.Trim();

            sqlstr += " and BP_SKRQ >='" + startTime + "' AND BP_SKRQ<='" + endTime + "'";

            //有无凭证
            if (ddl_skpz.SelectedIndex > 0)
            {
                sqlstr += " and BP_PZ=" + Convert.ToInt16(ddl_skpz.SelectedValue.ToString()) + "";
            }

            //负责人
            if (dplFZR.SelectedIndex != 0)
            {
                sqlstr +=" and PCON_RESPONSER='" + dplFZR.SelectedValue.ToString() + "'";
            }

            //客户名称
            if (txtKHMC.Text.Trim() != "")
            {
                sqlstr += " and PCON_CUSTMNAME like '%"+txtKHMC.Text.Trim()+"%'";
            }
            
            DataTable dt_dbyk = DBCallCommon.GetDTUsingSqlText(sqlstr);
            if (dt_dbyk.Rows.Count > 0)
            {
                Panel_nodate.Visible = false;
                grvSKJL.DataSource = dt_dbyk;
                grvSKJL.DataBind();
            }
            else
            {
                grvSKJL.DataSource = null;
                grvSKJL.DataBind();
                Panel_nodate.Visible = true;
            }
            //分页是否可见
            if (this.grvSKJL.PageCount > 0)
            {
                this.txt_goto.Text = this.lbl_currentpage.Text = (this.grvSKJL.PageIndex + 1).ToString();
                this.lbl_totalpage.Text = this.grvSKJL.PageCount.ToString();
                Pal_page.Visible = true;
                if (this.grvSKJL.PageIndex == 0)
                {
                    this.lnkbtnFrist.Enabled = false;
                    this.lnkbtnPre.Enabled = false;
                }
                else
                {
                    this.lnkbtnFrist.Enabled = true;
                    this.lnkbtnPre.Enabled = true;
                }
                if (this.grvSKJL.PageIndex == this.grvSKJL.PageCount - 1)
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
                tot_money += Convert.ToDecimal(dt_dbyk.Rows[i]["BP_SFJE"].ToString());
            }
            lb_select_money2.Text = string.Format("{0:c}", tot_money);

            //CheckUser(ControlFinder);
        }

        //收款查询
        protected void btnSKQuery_Click(object sender, EventArgs e)
        {
            this.BindBusPayment();

        }

        //重置收款筛选
        protected void Btn_Reset_SK_Click(object sender, EventArgs e)
        {
            search_Get.Value = "项目或合同号";
            dplFZR.SelectedIndex = 0;
            ddl_skpz.SelectedIndex = 0;
            txt_SK_starttime.Text = "";
            txt_SK_endtime.Text = "";
            txtKHMC.Text = "";
            this.btnSKQuery_Click(null, null);
        }

        #region 商务要款分页

        protected void lnkbtnGoto_Click(object sender, EventArgs e)
        {
            int index = Convert.ToInt32(txt_goto.Text.ToString());
            if (index <= grvSKJL.PageCount && index > 0)
            {
                this.grvSKJL.PageIndex = index - 1;
                this.BindBusPayment();
            }
        }
        protected void lnkbtnFrist_Click(object sender, EventArgs e)
        {
            this.grvSKJL.PageIndex = 0;
            this.BindBusPayment();
        }
        protected void lnkbtnPre_Click(object sender, EventArgs e)
        {
            if (this.grvSKJL.PageIndex > 0)
            {
                this.grvSKJL.PageIndex = this.grvSKJL.PageIndex - 1;
                this.BindBusPayment();
            }
        }
        protected void lnkbtnNext_Click(object sender, EventArgs e)
        {
            if (this.grvSKJL.PageIndex < this.grvSKJL.PageCount)
            {
                this.grvSKJL.PageIndex = this.grvSKJL.PageIndex + 1;
                this.BindBusPayment();
            }
        }
        protected void lnkbtnLast_Click(object sender, EventArgs e)
        {
            this.grvSKJL.PageIndex = this.grvSKJL.PageCount;
            this.BindBusPayment();
        }

        #endregion 

        //删除付款记录
        protected void Lbtn_Del_OnClick(object sender, EventArgs e)
        {
            //1.请款单本期已支付=本期已支付-本次支出金额；请款单状态根据付款情况修改（未支付，部分支付）
            //2.合同已付款=已付款-本次支出金额
            //3.删除本支出单
            string zcdh = ((LinkButton)sender).CommandArgument.ToString().Trim();//支出单号
            string qkdh = ((LinkButton)sender).CommandName.ToString().Trim();
            if (Session["UserDeptID"].ToString() == "08")
            {

                List<string> sqlstr = new List<string>();

                if (zcdh!=""&&zcdh!=null) //支出单号不为空说明是合同请款
                {
                
                    string selectInfo = "SELECT PR_HTBH,PR_QKDH,PR_BCZFJE,CR_BQSFK FROM TBPM_PAYMENTSRECORD AS A ,TBPM_CHECKREQUEST AS B " +
                        " WHERE A.PR_QKDH=B.CR_ID AND PR_ID='" + zcdh + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(selectInfo);
                    if (dt.Rows.Count > 0)
                    {
                        string htbh = dt.Rows[0]["PR_HTBH"].ToString();//合同编号
                        //string qkdh = dt.Rows[0]["PR_QKDH"].ToString();//请款单号
                        double zcje = Convert.ToDouble(dt.Rows[0]["PR_BCZFJE"].ToString());//本次支出金额
                        double qkje = Convert.ToDouble(dt.Rows[0]["CR_BQSFK"].ToString());//请款金额
                        //请款金额与本次支出金额相同，表示该请款单为一次付清，将请款单状态改为未支付；不相同表示分期付款，改为部分支付
                        int state = qkje == zcje ? 2 : 3;

                        //金额计算要加括号，如果支出金额为负数，会出现两个减号，而造成语法错误（如果是--表示注释，如果是+-，则可以正常运行）
                        string sql1 = "UPDATE TBPM_CHECKREQUEST SET CR_BQYZF=CR_BQYZF-(" + zcje + "),CR_STATE=" + state + " WHERE CR_ID='" + qkdh + "'";
                        string sql2 = "UPDATE TBPM_CONPCHSINFO SET PCON_YFK=PCON_YFK-(" + zcje + ")  WHERE PCON_BCODE='" + htbh + "'";
                        string sql3 = "DELETE FROM TBPM_PAYMENTSRECORD WHERE PR_ID='" + zcdh + "'";
                        sqlstr.Add(sql1);
                        sqlstr.Add(sql2);
                        sqlstr.Add(sql3);
                    }
                
                }
                else  //支出单号为空说明是非合同请款
                {
                    string del_yfk = "update TBPM_FHT_CheckRequest set CR_ZFJE=0,CR_ZFRQ='',CR_PZ='',CR_PZH='',CR_NOTE='',CR_STATE='2' where CR_QKDH='" + qkdh + "'";
                    sqlstr.Add(del_yfk);
                }
                if (sqlstr.Count > 0)
                {
                    DBCallCommon.ExecuteTrans(sqlstr);
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('已删除该支出单');window.location.href='CM_CheckRequestRecord.aspx';", true);
                    //Response.Redirect("CM_CheckRequestRecord.aspx");
                }
                
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('您无权执行此操作\\r请联系财务相关人员');", true);
                  
            }
        
        }

        //导出付款记录
        protected void btn_Export_Click(object sender, EventArgs e)
        {
            string sqltext = "SELECT HTBH,PJNAME,ENGNAME,SKDW,QKJE,QKRQ,ZFJE,ZCRQ,QKYT,QKBM,QKR,"+
                             " CASE HTLB WHEN '0' THEN '销售' WHEN '1' THEN '委外' WHEN '3' THEN '采购' WHEN '4' THEN '办公' ELSE '其他' END AS HTLB,"+
                             " CASE PZ WHEN 1 THEN '有' WHEN 0 THEN '无' END AS PZ FROM CheckRequset WHERE" + ViewState["sqltext"].ToString();
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                ContractClass.ExportFKDataItem(dt);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('没有可以导出的数据，请重新筛选');", true);
            }
        }
    }
}
