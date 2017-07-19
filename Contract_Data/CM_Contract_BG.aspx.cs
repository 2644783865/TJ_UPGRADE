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
using System.Text;
using System.Collections.Generic;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_Contract_FY : BasicPage
    {
        /**************************************
         * 所接受上一页面的参数为合同类别ContractForm
         *******************************************/

        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ContractForm"] != null)
            {
                ViewState["contractForm"] = Request.QueryString["ContractForm"];
            }

            if (!IsPostBack)
            {
                lblContractTypeBT.Text = this.GetConForm(ViewState["contractForm"].ToString()) + "管理";
                BindDDL();
                BindHtnf();
                this.btn_search_Click(null, null);
            }
            this.InitVar();
            //CheckUser(ControlFinder);
        }
        private string GetConForm(string bh)
        {
            switch (bh)
            {
                case "1": return "委外合同";
                case "2": return "厂内分包";
                case "3": return "采购合同";
                case "4": return "办公合同";
                case "5": return "其他合同";
                default: return " ";
            }
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
            pager.TableName = "TBPM_CONPCHSINFO left join TBCR_CONTRACTREVIEW on TBPM_CONPCHSINFO.PCON_REVID=TBCR_CONTRACTREVIEW.CR_ID";
            pager.PrimaryKey = "PCON_BCODE";
            pager.ShowFields = "PCON_BCODE,PCON_PJNAME,PCON_NAME,PCON_YFK,PCON_JINE,PCON_SPJE,PCON_STATE,PCON_JECHG,PCON_QTCHG,PCON_ERROR,PCON_CUSTMNAME" +
                               " ,PCON_BALANCEACNT,CASE WHEN PCON_BALANCEACNT=0 THEN PCON_JINE ELSE PCON_BALANCEACNT END AS PCON_HTZJ,PCON_RESPONSER,PCON_FORM,CR_PSZT";
            pager.OrderField = "PCON_BCODE";
            pager.StrWhere = ViewState["sqlText"].ToString();
            pager.OrderType = 1;
            pager.PageSize = Convert.ToInt16(ddl_pageno.SelectedValue);

        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        /// <summary>
        /// 绑定GRV_CON数据
        /// </summary>
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GRV_CON, UCPaging1, NoDataPanel);
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

        /// <summary>
        /// 获取查询条件
        /// </summary>
        private void GetSqlText()
        {
            StringBuilder sqltext = new StringBuilder();
            //合同类别
            sqltext.Append(" PCON_FORM='" + ViewState["contractForm"].ToString() + "'");

            //项目名称
            if (txt_PJNAME.Text.Trim() != "")
            {
                sqltext.Append(" and PCON_PJNAME like '%" + txt_PJNAME.Text.Trim() + "%'");
            } 

            //负责人
            if (dplFZR.SelectedIndex != 0)
            {
                sqltext.Append(" and PCON_RESPONSER='" + dplFZR.SelectedValue.ToString() + "'");
            }

            //合同签订时间
            if (sta_time.Text.Trim() != "" || end_time.Text.Trim() != "")
            {
                string startTime = sta_time.Text.Trim() == "" ? DateTime.Now.AddYears(-100).ToShortDateString() : sta_time.Text.Trim();
                string endTime = end_time.Text.Trim() == "" ? DateTime.Now.AddYears(100).ToShortDateString() : end_time.Text.Trim();
                sqltext.Append(" and (PCON_FILLDATE>='" + startTime + "' AND PCON_FILLDATE<='" + endTime + "')");
            }
            //合同号
            if (txtHTH.Text.Trim() != "")
            {
                sqltext.Append(" and PCON_BCODE like '%" + txtHTH.Text.Trim() + "%'  ");
            }

            // 供应商
            if (txt_GHS.Text.Trim() != "")
            {
                sqltext.Append(" and PCON_CUSTMNAME like '%" + txt_GHS.Text.Trim() + "%'");
            }

            // 合同名称
            if (txt_HTNAME.Text.Trim() != "")
            {
                sqltext.Append(" and PCON_NAME like '%" + txt_HTNAME.Text.Trim() + "%'");
            }

            //支付比例
            if (fkbl.Text.Trim() != "" && fkbl.Text.Trim() != "0")
            {
                sqltext.Append(" and (PCON_YFK/(case (CASE WHEN PCON_BALANCEACNT=0 THEN PCON_JINE ELSE PCON_BALANCEACNT END) when 0 then 1 else (CASE WHEN PCON_BALANCEACNT=0 THEN PCON_JINE ELSE PCON_BALANCEACNT END) end)*100)" + ddlfkbl.SelectedValue.ToString() + Convert.ToDouble(fkbl.Text.Trim()));
            }
            if (htps_state.SelectedIndex != 0 && htps_state.SelectedIndex != 1)
            {
                sqltext.Append(" and CR_PSZT=" + htps_state.SelectedValue.ToString() + "");
            }
            if (htps_state.SelectedIndex == 1)
            {
                sqltext.Append(" and CR_PSZT is null");
            }
            //合同年份
            if (htnf.SelectedIndex != 0)
            {
                sqltext.Append(" and substring(PCON_BCODE,12,4)='" + htnf.SelectedValue.Trim() + "'");
            }
            ViewState["sqlText"] = sqltext.ToString();
        }

        #endregion

        private void BindDDL() //负责人
        {
            string sqltext = "select distinct(PCON_RESPONSER) from TBPM_CONPCHSINFO where PCON_FORM=3";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            dplFZR.DataSource = dt;
            dplFZR.DataTextField = "PCON_RESPONSER";
            dplFZR.DataValueField = "PCON_RESPONSER";
            dplFZR.DataBind();
            dplFZR.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));

        }

        private void BindHtnf() //合同年份
        {
            string sqltext = "SELECT distinct(substring(PCON_BCODE,12,4)) AS PCON_BCODE FROM  TBPM_CONPCHSINFO where PCON_FORM='" + ViewState["contractForm"].ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            htnf.DataSource = dt;
            htnf.DataTextField = "PCON_BCODE";
            htnf.DataValueField = "PCON_BCODE";
            htnf.DataBind();
            htnf.Items.Insert(0, new ListItem("-请选择-", "%"));
            htnf.SelectedIndex = 0;
        }
        protected void grv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lbl_id = (Label)e.Row.FindControl("lbl_id");//序号
                lbl_id.Text = ((this.pager.PageIndex - 1) * this.pager.PageSize + e.Row.RowIndex + 1).ToString();
                Label lbl_htbh = (Label)e.Row.FindControl("lbl_htbh");//合同编号
                Label lbl_htlx = (Label)e.Row.FindControl("lbl_htlx");//合同类型

                Label lbl_qkje = (Label)e.Row.FindControl("lbl_qkje");
                double qkje = 0;

                string sqlqkje = "select CR_BQSFK from TBPM_CHECKREQUEST where CR_HTBH='" + lbl_htbh.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlqkje);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //将该合同下的请款单的本期实付款金额相加，不论该请款单是否提交到财务
                    qkje += Convert.ToDouble(dt.Rows[i]["CR_BQSFK"].ToString());
                }
                lbl_qkje.Text = string.Format("{0:c}", qkje);

                Label lbl_kpje = (Label)e.Row.FindControl("lbl_kpje");//开票金额
                double kpje = 0;
                string sqlkpje = "select SUM(GIV_KPJE) AS KPJE from TBPM_GATHINVDOC where GIV_HTBH='" + lbl_htbh.Text + "'";
                DataTable dtkpje = DBCallCommon.GetDTUsingSqlText(sqlkpje);
                if (dtkpje.Rows.Count > 0)
                {
                    if (dtkpje.Rows[0]["KPJE"].ToString() != "")
                        kpje += Convert.ToDouble(dtkpje.Rows[0]["KPJE"].ToString());
                }

                lbl_kpje.Text = string.Format("{0:c}", kpje);

                e.Row.Attributes.Add("onclick", "RowClick(this)");

                e.Row.Attributes["style"] = "Cursor:pointer";

                e.Row.Attributes.Add("ondblclick", "ViewHT('" + lbl_htbh.Text.Trim() + "','" + lbl_htlx.Text.Trim() + "')");
                e.Row.Attributes.Add("title", "双击查看合同信息");


            }
        }

        protected void btn_search_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.GetSqlText();
            this.InitVar();
            this.bindGrid();
            this.Select_record();
        }

        //显示筛选后的记录条数和合计金额
        private void Select_record()
        {
            string sqltext = "select PCON_JINE,PCON_SPJE from TBPM_CONPCHSINFO left join TBCR_CONTRACTREVIEW on TBPM_CONPCHSINFO.PCON_REVID=TBCR_CONTRACTREVIEW.CR_ID where " + ViewState["sqlText"].ToString();
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            lb_select_num.Text = dt.Rows.Count.ToString();
            decimal tot_money = 0;
            decimal tot_sp = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tot_money += Convert.ToDecimal(dt.Rows[i]["PCON_JINE"].ToString());
                tot_sp += Convert.ToDecimal(dt.Rows[i]["PCON_SPJE"].ToString());
            }
            lb_select_money.Text = string.Format("{0:c}", tot_money);
            lb_select_sp.Text = string.Format("{0:c}", tot_sp);
        }

        //重置
        protected void btn_reset_Click(object sender, EventArgs e)
        {
            txtHTH.Text = "";//合同号            
            dplFZR.SelectedIndex = 0; //负责人
            htps_state.SelectedIndex = 0;//合同评审状态
            txt_PJNAME.Text = ""; // 项目名称
            txt_GHS.Text = "";//供货商
            txt_HTNAME.Text = ""; // 合同名称
            htnf.SelectedIndex = 0; 
            sta_time.Text = ""; // 签订开始时间
            end_time.Text = ""; // 签订结束时间
            fkbl.Text = "";//付款比例

            this.btn_search_Click(null, null);
        }

        #region 按钮事件
        //查看合同
        protected void btn_ViewHT_Click(object sender, EventArgs e)
        {
            Action_Click(1);
        }

        //编辑合同
        protected void btn_EditHT_Click(object sender, EventArgs e)
        {
            Action_Click(2);
        }

        //删除合同
        protected void btn_DelHT_Click(object sender, EventArgs e)
        {
            Action_Click(3);
        }

        //添加请款
        protected void btn_AddQK_Click(object sender, EventArgs e)
        {
            Action_Click(4);
        }

        //添加合同
        protected void btn_AddHT_Click(object sender, EventArgs e)
        {
            Action_Click(5);
        }

        //导出合同
        protected void btn_Export_Click(object sender, EventArgs e)
        {
            Action_Click(6);
        }
        #endregion

        //按钮事件
        //1查看合同，2编辑合同，3删除合同，4添加请款，5添加合同，6导出合同
        private void Action_Click(int clicktype)
        {
            //找到选定行的参数
            string htbh = "";
            string fzr = "";
            string htlx = "";
            bool check = false; ;
            for (int i = 0; i < GRV_CON.Rows.Count; i++)
            {
                CheckBox cbx = (CheckBox)GRV_CON.Rows[i].FindControl("CheckBox1");
                Label lbl_htbh = (Label)GRV_CON.Rows[i].FindControl("lbl_htbh");//合同编号
                Label lbl_fzr = (Label)GRV_CON.Rows[i].FindControl("lbl_fzr");//负责人
                Label lbl_htlx = (Label)GRV_CON.Rows[i].FindControl("lbl_htlx");//合同类型

                if (cbx.Checked == true)
                {
                    htbh = lbl_htbh.Text.Trim();
                    fzr = lbl_fzr.Text.Trim();
                    htlx = lbl_htlx.Text.Trim();
                    check = true;
                    break;
                }
            }

            //判断是否选择了数据行
            if (!check && clicktype != 5 && clicktype != 6)//没有选择行，且不是添加合同操作
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请勾选要操作的数据行！');", true); return;
            }
            else
            {
                switch (clicktype)
                {
                    case 1://查看合同
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "ViewHT('" + htbh + "','" + htlx + "');", true);
                        break;
                    case 2://编辑合同                        
                       
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "EditHT('" + htbh + "','" + htlx + "');", true);
                        break;
                    case 3://删除合同
                        if (CheckPower(fzr, htbh))
                        {
                            //还没有付款的，如果要删除，则同时删除该合同下的：
                            //合同评审主表、合同评审意见表、合同登记表、请款、发票登记、索赔、附件信息及附件
                            //附件包括：合同附件和索赔附件
                            List<string> sqlstr = new List<string>();
                            string sql_Del_Info = "delete from TBCR_CONTRACTREVIEW where CR_ID=(" + //删除合同评审主表信息
                                                "select PCON_REVID from TBPM_CONPCHSINFO where PCON_BCODE='" + htbh + "')";
                            string sql_Del_Rev = "delete from TBCR_CONTRACTREVIEW_DETAIL  where CRD_ID=(" +//删除合同评审意见表信息
                                                "select PCON_REVID from TBPM_CONPCHSINFO where PCON_BCODE='" + htbh + "')";
                            string sql_Del_ConInfo = "delete from TBPM_CONPCHSINFO where PCON_BCODE='" + htbh + "'";//删除合同登记信息
                            string sql_Del_YK = "delete from TBPM_CHECKREQUEST where CR_HTBH='" + htbh + "'";//删除请款记录
                            string sql_Del_FP = "delete from TBPM_GATHINVDOC where GIV_HTBH='" + htbh + "'";//删除发票记录
                            string sql_Del_SP = "delete from TBPM_SUBCLAIM where SPS_HTBH='" + htbh + "'";//删除索赔记录
                            string sql_Del_Conno = "delete from TBCM_TEMPCONNO where CON_NO='" + htbh + "' and USER_NAME='" + Session["UserName"].ToString() + "'";//删除已锁定的合同号

                            //删除附件及附件信息
                            Contract_Data.ContractClass.Del_ConAttachment(htbh);
                            Contract_Data.ContractClass.Del_SPAttachment(htbh);

                            sqlstr.Add(sql_Del_Info);
                            sqlstr.Add(sql_Del_Rev);
                            sqlstr.Add(sql_Del_ConInfo);
                            sqlstr.Add(sql_Del_YK);
                            sqlstr.Add(sql_Del_FP);
                            sqlstr.Add(sql_Del_SP);
                            sqlstr.Add(sql_Del_Conno);
                            DBCallCommon.ExecuteTrans(sqlstr);
                            Response.Redirect("CM_Contract_FY.aspx?ContractForm=3");
                        }
                        break;
                    case 4://添加请款
                        string sql = "select CR_PSZT from TBPM_CONPCHSINFO left join TBCR_CONTRACTREVIEW on TBPM_CONPCHSINFO.PCON_REVID=TBCR_CONTRACTREVIEW.CR_ID where PCON_BCODE='" + htbh + "'";
                        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        if (Convert.ToString(dt.Rows[0]["CR_PSZT"]) == "2")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "Add_QK('" + htbh + "','" + htlx + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('合同还未审批通过，无法添加请款！');", true); return;
                        }
                        break;
                    case 5://添加合同
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "Add_HT('" + ViewState["contractForm"].ToString() + "');", true);
                        break;
                    case 6://导出合同
                        string sqlexport = "SELECT * FROM View_CM_ExportHTToExcel WHERE " + ViewState["sqlText"].ToString() +
                                           " ORDER BY PCON_BCODE";
                        System.Data.DataTable dt_export = DBCallCommon.GetDTUsingSqlText(sqlexport);
                        ContractClass.ExportDataItem(dt_export);
                        break;
                }
            }
        }

        //检查是否可以执行删除的操作
        private bool CheckPower(string fzr, string htbh)
        {
            bool check = true;

            if (!(Session["UserName"].ToString() == fzr))//不是负责人
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('只有负责人才能进行此操作！');", true); return check;
            }


            //删除前先判断合同是否已付款，如果已付款，不允许删除
            string sql_YFK = "select PCON_YFK from TBPM_CONPCHSINFO where PCON_BCODE='" + htbh + "'";
            DataTable dtYFK = DBCallCommon.GetDTUsingSqlText(sql_YFK);
            if (dtYFK.Rows.Count > 0)
            {
                if (Convert.ToDouble(dtYFK.Rows[0]["PCON_YFK"]) > 0)
                {
                    check = false;
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该合同已付款金额不为0，不能删除！');", true); return check;
                }
            }

            
            return check;
        }
    }
}
