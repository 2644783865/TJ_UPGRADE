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
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_Contract_SW : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblContractTypeBT.Text = "销售合同管理";
                string PCON_BCODE = Request["PCON_BCODE"];
                if (!string.IsNullOrEmpty(PCON_BCODE))
                {
                    txtHTH.Text = PCON_BCODE.Trim();
                }
                BindDDL();
                BindDropDown();
                BindHtnf();
                this.btn_search_Click(null, null);
            }
            this.InitVar();
            CheckUser(ControlFinder);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "aa", "aa()", true);
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
            pager.TableName = " TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID left join (select * from (select no=row_number() over(partition by CM_CONTR order by getdate()),* from TBPM_DETAIL) t where no=1) as c on a.PCON_BCODE=c.CM_CONTR left join (select distinct TFI_CONTR from TBMP_FINISHED_IN)t  on a.PCON_BCODE=t.TFI_CONTR";
            pager.PrimaryKey = "PCON_BCODE";
            pager.ShowFields = "PCON_BCODE,PCON_SCH,PCON_ENGTYPE,(PCON_PJID+'/'+PCON_PJNAME) AS PCON_PJNAME,PCON_ENGNAME,PCON_YFK,PCON_JINE,PCON_SPJE,PCON_STATE,PCON_JECHG,PCON_QTCHG,PCON_FHSJ,PCON_ERROR,PCON_CUSTMNAME,PCON_BALANCEACNT,CASE WHEN convert(decimal(18,2),PCON_BALANCEACNT)=0 THEN PCON_JINE ELSE PCON_BALANCEACNT END AS PCON_HTZJ,PCON_YZHTH,OTHER_MONUNIT,PCON_MONUNIT,PCON_KPJE,PCON_RESPONSER,PCON_NOTE,PCON_YFKBL,PCON_WFKBL,PCON_ZBJ,PCON_ZBJBL,CR_PSZT,c.CM_DUTY,c.CM_MAP,t.TFI_CONTR";
            pager.OrderField = "PCON_BCODE";
            pager.StrWhere = ViewState["sqlText"].ToString();
            pager.OrderType = 1;
            pager.PageSize = Convert.ToInt16(ddl_pageno.SelectedValue);
        }
        void Pager_PageChanged(int pageNumber)
        {
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
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件                 
            }
            //CheckUser(ControlFinder);
        }

        #endregion

        private void BindDropDown()
        {
            Dictionary<string, DropDownList> dlist = new Dictionary<string, DropDownList>() { { "PCON_BCODE", ddl_HTH }, { "PCON_YZHTH", ddl_YZHTH }, { "PCON_ENGNAME", ddl_PJNAME }, { "PCON_CUSTMNAME", ddl_KH } };
            for (int i = 0; i < dlist.Count; i++)
            {
                string sql = string.Format("select distinct {0} from TBPM_CONPCHSINFO where  PCON_FORM=0", dlist.Keys.ElementAt(i));
                DropDownList drop = dlist.Values.ElementAt(i);
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                drop.DataSource = dt;
                drop.DataTextField = dlist.Keys.ElementAt(i);
                drop.DataValueField = dlist.Keys.ElementAt(i);
                drop.DataBind();
                drop.Items.Insert(0, new ListItem("-请选择-", "%"));
                drop.SelectedIndex = 0;
            }

        }
        private void BindDDL() //负责人
        {
            string sqltext = "select distinct(PCON_RESPONSER) from TBPM_CONPCHSINFO where PCON_FORM=0";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            dplFZR.DataSource = dt;
            dplFZR.DataTextField = "PCON_RESPONSER";
            dplFZR.DataValueField = "PCON_RESPONSER";
            dplFZR.DataBind();
            dplFZR.Items.Insert(0, new ListItem("-请选择-", "%"));
            dplFZR.SelectedIndex = 0;
        }
        private void BindHtnf() //合同年份
        {
            string sqltext = "SELECT distinct(substring(PCON_BCODE,6,2)) AS PCON_BCODE FROM TBPM_CONPCHSINFO where PCON_FORM=0";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            htnf.DataSource = dt;
            htnf.DataTextField = "PCON_BCODE";
            htnf.DataValueField = "PCON_BCODE";
            htnf.DataBind();
            htnf.Items.Insert(0, new ListItem("-请选择-", "%"));
            htnf.SelectedIndex = 0;
        }
        /// <summary>
        /// 获取查询条件
        /// </summary>
        private void GetSqlText()
        {
            StringBuilder sqltext = new StringBuilder();
            //合同类别
            sqltext.Append(" PCON_FORM=0 ");

            //项目名称
            if (txt_PJNAME.Text.Trim() != "")
            {
                sqltext.Append(" and PCON_ENGNAME like '%" + txt_PJNAME.Text.Trim() + "%'");
            }
            if (ddl_PJNAME.SelectedIndex != 0)
            {
                sqltext.Append(" and PCON_ENGNAME like '%" + ddl_PJNAME.SelectedValue + "%'");
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
            if (ddl_HTH.SelectedIndex != 0)
            {
                sqltext.Append(" and PCON_BCODE like '%" + ddl_HTH.SelectedValue.ToString() + "%' ");
            }
            // 客户
            if (txt_KH.Text.Trim() != "")
            {
                sqltext.Append(" and PCON_CUSTMNAME like '%" + txt_KH.Text.ToString() + "%'");
            }
            if (ddl_KH.SelectedIndex != 0)
            {
                sqltext.Append(" and PCON_CUSTMNAME like '%" + ddl_KH.SelectedValue.ToString() + "%' ");
            }
            // 业主合同号
            if (txt_YZHTH.Text.Trim() != "")
            {
                sqltext.Append(" and PCON_YZHTH like '%" + txt_YZHTH.Text.Trim() + "%' ");
            }
            if (ddl_YZHTH.SelectedIndex != 0)
            {
                sqltext.Append(" and PCON_YZHTH like '%" + ddl_YZHTH.SelectedValue.ToString() + "%' ");
            }
            //发货时间
            if (fhsta_time.Text.Trim() != "" || fhend_time.Text.Trim() != "")
            {
                string fhstartTime = fhsta_time.Text.Trim();
                string fhendTime = fhend_time.Text.Trim();
                sqltext.Append(" and (PCON_FHSJ>='" + fhstartTime + "' AND PCON_FHSJ<='" + fhendTime + "' and PCON_FHSJ <>'' and PCON_FHSJ is not null)");
            }

            //支付比例
            if (fkbl.Text.Trim() != "" && fkbl.Text.Trim() != "0")
            {
                string a = "";
                if (ddlfkbl.SelectedValue=="1")
                {
                    a = "=";
                }
                else if (ddlfkbl.SelectedValue=="2")
                {
                    a = ">";
                }
                else if (ddlfkbl.SelectedValue=="3")
                {
                    a = "<";
                }
                else if (ddlfkbl.SelectedValue=="4")
                {
                    a = ">=";
                }
                else if (ddlfkbl.SelectedValue=="5")
                {
                    a = "<=";
                }
                sqltext.Append(" and (PCON_YFK/(case (CASE WHEN convert(float,PCON_BALANCEACNT)=0 THEN convert(float,PCON_JINE) ELSE convert(float,PCON_BALANCEACNT) END) when 0 then 1 else (CASE WHEN convert(float,PCON_BALANCEACNT)=0 THEN convert(float,PCON_JINE) ELSE convert(float,PCON_BALANCEACNT) END) end)*100)" + a + CommonFun.ComTryDouble(fkbl.Text.Trim()));
            }
            if (htps_state.SelectedIndex != 0 && htps_state.SelectedIndex != 1)
            {
                sqltext.Append(" and CR_PSZT=" + htps_state.SelectedValue.ToString() + "");
            }
            //合同评审状态
            if (htps_state.SelectedIndex == 1)
            {
                sqltext.Append(" and CR_PSZT is null");
            }
            //合同年份
            if (htnf.SelectedIndex != 0)
            {
                sqltext.Append(" and substring(PCON_BCODE,6,2)='" + htnf.SelectedValue.Trim() + "'");
            }
            if (txt_SheBei.Text.Trim() != "")
            {
                sqltext.Append(" and PCON_BCODE in (select CM_CONTR from TBPM_DETAIL where CM_ENGNAME like '%" + txt_SheBei.Text.Trim() + "%')");
            }
            if (txt_Map.Text.Trim() != "")
            {
                sqltext.Append(" and PCON_BCODE in (select CM_CONTR from TBPM_DETAIL where CM_MAP like '%" + txt_Map.Text.Trim() + "%')");
            }
            if (ddlQD.SelectedValue=="1")
            {
                sqltext.Append(" and PCON_JINE ='0'");
            }
            if (ddlQD.SelectedValue=="2")
            {
                 sqltext.Append(" and PCON_JINE !='0'");
            }
            if (cbxConWarn.Checked == true)
            {
                sqltext.Append(" and PCON_JINE='0' and  TFI_CONTR is not null");
            }
            ViewState["sqlText"] = sqltext.ToString();
        }

        List<string> str = new List<string>();
        protected void grv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                Label lbl_id = (Label)e.Row.FindControl("lbl_id");//序号
                lbl_id.Text = ((this.pager.PageIndex - 1) * this.pager.PageSize + e.Row.RowIndex + 1).ToString();
                Label lbl_htbh = (Label)e.Row.FindControl("lbl_htbh");//合同编号

                Label lbl_kpje = (Label)e.Row.FindControl("lbl_kpje");//开票金额
                double kpje = 0;
                string sqlkpje = "select SUM(BR_KPJE) AS KPJE from TBPM_BUSBILLRECORD where BR_HTBH='" + lbl_htbh.Text + "'";
                DataTable dtkpje = DBCallCommon.GetDTUsingSqlText(sqlkpje);
                if (dtkpje.Rows.Count > 0)
                {
                    if (dtkpje.Rows[0]["KPJE"].ToString() != "")
                        kpje += Convert.ToDouble(dtkpje.Rows[0]["KPJE"].ToString());
                }

                lbl_kpje.Text = string.Format("{0:c}", kpje);

                e.Row.Attributes.Add("onclick", "RowClick(this)");

                e.Row.Attributes["style"] = "Cursor:pointer";

                e.Row.Attributes.Add("ondblclick", "ViewHT('" + lbl_htbh.Text.Trim() + "')");
                e.Row.Attributes.Add("title", "双击查看合同信息");
            }
            if (e.Row.RowType.ToString() == "DataRow")
            {
                //if (str.Count < 1)
                //{
                //    str.Add(e.Row.Cells[2].Text);
                //}
                //else
                //{
                //    if (str.Contains(e.Row.Cells[2].Text))
                //    {
                //        e.Row.Cells[1].Text = "&nbsp;";
                //        e.Row.Cells[2].Text = "&nbsp;";
                //        e.Row.Cells[4].Text = "&nbsp;";
                //        e.Row.Cells[24].Text = "&nbsp;";
                //        e.Row.Cells[25].Text = "&nbsp;";
                //        e.Row.Cells[26].Text = "&nbsp;";
                //        e.Row.Cells[27].Text = "&nbsp;";
                //        e.Row.Cells[28].Text = "&nbsp;";
                //        e.Row.Cells[29].Text = "&nbsp;";
                //    }
                //    else
                //    {
                //        str.Add(e.Row.Cells[2].Text);
                //    }
                //}
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
            string sqltext = "select PCON_JINE,PCON_SPJE from TBPM_CONPCHSINFO left join TBCR_CONTRACTREVIEW on TBPM_CONPCHSINFO.PCON_REVID=TBCR_CONTRACTREVIEW.CR_ID left join (select distinct TFI_CONTR from TBMP_FINISHED_IN)t  on TBPM_CONPCHSINFO.PCON_BCODE=t.TFI_CONTR where" + ViewState["sqlText"].ToString();
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            lb_select_num.Text = dt.Rows.Count.ToString();
            decimal tot_money = 0;
            decimal tot_sp = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tot_money += Convert.ToDecimal(dt.Rows[i]["PCON_JINE"].ToString());
                tot_sp += Convert.ToDecimal(dt.Rows[i]["PCON_SPJE"].ToString());
            }
            lb_select_money.Text = string.Format("{0}（万元）", tot_money);
            lb_select_sp.Text = string.Format("{0:c}", tot_sp);

            sqltext = "select CM_ALL from ( select *,'2' as CR_PSZT from TBPM_CONPCHSINFO)as a left join TBPM_DETAIL as b on a.PCON_BCODE=b.CM_CONTR left join (select distinct TFI_CONTR from TBMP_FINISHED_IN)t  on a.PCON_BCODE=t.TFI_CONTR where " + ViewState["sqlText"].ToString();
            dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            decimal a = 0;
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    if (dr[0].ToString() != "")
                    {
                        a += decimal.Parse(dr[0].ToString());
                    }
                }
                catch (Exception)
                {
                    lb_all_weight.Text = "数据有误";
                    return;
                }
            }
            lb_all_weight.Text = a.ToString() + "（吨）";
        }

        //重置
        protected void btn_reset_Click(object sender, EventArgs e)
        {
            txtHTH.Text = "";//合同号
            txt_YZHTH.Text = "";//业主合同号
            dplFZR.SelectedIndex = 0; //负责人
            htps_state.SelectedIndex = 0;//合同评审状态
            txt_PJNAME.Text = ""; // 项目名称
            txt_KH.Text = "";//客户名称
            htnf.SelectedIndex = 0;
            sta_time.Text = ""; // 签订开始时间
            end_time.Text = ""; // 签订结束时间
            txt_SheBei.Text = "";
            txt_Map.Text = "";

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

        //添加要款
        protected void btn_AddYK_Click(object sender, EventArgs e)
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
        //1查看合同，2编辑合同，3删除合同，4添加要款，5添加合同，6导出合同
        private void Action_Click(int clicktype)
        {
            //找到选定行的参数
            string htbh = "";
            string fzr = "";
            bool check = false; ;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox cbx = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
                Label lbl_htbh = (Label)GridView1.Rows[i].FindControl("lbl_htbh");//合同编号
                Label lbl_fzr = (Label)GridView1.Rows[i].FindControl("lbl_fzr");//负责人

                if (cbx.Checked == true)
                {
                    htbh = lbl_htbh.Text.Trim();
                    fzr = lbl_fzr.Text.Trim();
                    check = true;
                    break;
                }
            }

            //判断是否选择了数据行
            if (!check && clicktype != 5 && clicktype != 6)//没有选择行，且不是添加和导出合同操作
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请勾选要操作的数据行！');", true); return;
            }
            else
            {
                switch (clicktype)
                {
                    case 1://查看合同
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "ViewHT('" + htbh + "');", true);
                        break;
                    case 2://编辑合同 
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "EditHT('" + htbh + "');", true);
                        break;
                    case 3://删除合同
                        if (CheckPower(fzr, htbh))
                        {
                            //还没有付款的，如果要删除，则同时删除该合同下的：
                            //合同评审主表、合同评审意见表、合同登记表、商务要款、发票登记、索赔、附件信息及附件
                            //附件包括：合同附件和索赔附件
                            List<string> sqlstr = new List<string>();
                            string sql_Del_Info = "delete from TBCR_CONTRACTREVIEW where CR_ID=(" + //删除合同评审主表信息
                                                "select PCON_REVID from TBPM_CONPCHSINFO where PCON_BCODE='" + htbh + "')";
                            string sql_Del_Rev = "delete from TBCR_CONTRACTREVIEW_DETAIL  where CRD_ID=(" +//删除合同评审意见表信息
                                                "select PCON_REVID from TBPM_CONPCHSINFO where PCON_BCODE='" + htbh + "')";
                            string sql_Del_ConInfo = "delete from TBPM_CONPCHSINFO where PCON_BCODE='" + htbh + "'";//删除合同登记信息
                            string sql_Del_YK = "delete from TBPM_BUSPAYMENTRECORD where BP_HTBH='" + htbh + "'";//删除要款记录
                            string sql_Del_FP = "delete from TBPM_BUSBILLRECORD where BR_HTBH='" + htbh + "'";//删除发票记录
                            string sql_Del_SP = "delete from TBPM_SUBCLAIM where SPS_HTBH='" + htbh + "'";//删除索赔记录
                            string sql_Del_Conno = "delete from TBCM_TEMPCONNO where CON_NO='" + htbh + "' and USER_NAME='" + Session["UserName"].ToString() + "'";//删除已锁定的合同号
                            string sql_Del_Det = "delete from TBPM_DETAIL where CM_CONTR='" + htbh + "'";
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
                            sqlstr.Add(sql_Del_Det);
                            DBCallCommon.ExecuteTrans(sqlstr);
                            Response.Redirect("CM_Contract_SW.aspx");
                        }
                        break;
                    case 4://添加要款
                        string sql = "select CR_PSZT from TBPM_CONPCHSINFO left join TBCR_CONTRACTREVIEW on TBPM_CONPCHSINFO.PCON_REVID=TBCR_CONTRACTREVIEW.CR_ID where PCON_BCODE='" + htbh + "'";
                        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        if (Convert.ToString(dt.Rows[0]["CR_PSZT"]) == "2")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "Add_YK('" + htbh + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('合同还未审批通过，无法添加要款！');", true); return;
                        }
                        break;
                    case 5://添加合同
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "Add_HT();", true);
                        break;
                    case 6://导出合同
                        //string sqlexport = "SELECT PCON_ENGTYPE,PCON_ENGNAME,PCON_BCODE,PCON_YZHTH,PCON_NAME,PCON_JINE,PCON_BALANCEACNT,KPJE,PCON_FILLDATE,PCON_DELIVERYDATE,PCON_CUSTMNAME,PCON_RESPONSER,BP_JE,BP_SFJE,PCON_FORM FROM View_CM_ExportXSHTToExcel WHERE " + ViewState["sqlText"].ToString();
                        //string sqlexport = "select a.PCON_BCODE,b.TSA_ID,a.PCON_YZHTH,a.PCON_ENGNAME,a.PCON_ENGTYPE,a.PCON_JINE from TBPM_CONPCHSINFO as a left join TBPM_DETAIL as b on a.PCON_BCODE=b.CM_CONTR";//9月23日改
                        //string sqlexport = "select a.TSA_PJID,a.TSA_ID,b.CM_DFCONTR,b.CM_PROJ,TSA_ENGNAME,c.PCON_JINE from dbo.TBPM_TCTSASSGN as a left join TBCM_PLAN as b on a.id=b.id left join TBPM_CONPCHSINFO as c on b.CM_CONTR=c.PCON_BCODE where a.TSA_PJID not like 'JSB.%'";
                        string sqlexport = "select CM_CONTR,TSA_ID,case when (CM_YZHTH<>''or CM_YZHTH is not null) then CM_YZHTH else PCON_YZHTH end,CM_PROJ,substring(CM_ENGNAME,0,charindex('|',CM_ENGNAME)),JE,weight from (select a.CM_CONTR,a.TSA_ID,b.PCON_YZHTH,a.CM_YZHTH, a.CM_PROJ,stuff((select CM_ENGNAME +'|' from TBPM_DETAIL as d where (d.TSA_ID=a.TSA_ID) FOR xml path('')),1,0,'')as CM_ENGNAME,sum(convert(float,a.CM_COUNT)) as JE,sum(convert(float,isnull(a.CM_ALL,0))) as weight from TBPM_DETAIL as a left join TBPM_CONPCHSINFO as b on a.CM_CONTR=b.PCON_BCODE group by CM_CONTR,TSA_ID,CM_PROJ,PCON_YZHTH,CM_YZHTH)t";
                        System.Data.DataTable dt_export = DBCallCommon.GetDTUsingSqlText(sqlexport);
                        if (dt_export.Rows.Count > 0)
                        {
                            //ContractClass.ExportXSDataItem(dt_export);
                            ExportDataItem(dt_export);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('没有可以导出的数据！');", true); return;
                        }
                        break;
                }
            }
        }

        private void ExportDataItem(DataTable dt)
        {
            string filename = "合同导出明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("任务号模版.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);

                    ICellStyle ces = wk.CreateCellStyle();
                    ces.WrapText = true;

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                        row.Cells[j].CellStyle = ces;
                    }
                }

                #endregion

                //for (int i = 0; i <= dt.Columns.Count; i++)
                //{
                //    sheet0.AutoSizeColumn(i);
                //}
                sheet0.ForceFormulaRecalculation = true;

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
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

        protected void btn_Marker_Export_Click(object sender, EventArgs e)
        {
            //string sql = "select PCON_BCODE,PCON_SCH,(PCON_PJID+'/'+PCON_PJNAME) AS PCON_PJNAME,d.BR_KPJE,PCON_ENGNAME,PCON_YFK,PCON_JINE,PCON_SPJE,PCON_STATE,PCON_JECHG,PCON_QTCHG,PCON_FHSJ,PCON_ERROR,PCON_CUSTMNAME,PCON_BALANCEACNT,CASE WHEN convert(decimal(18,2),PCON_BALANCEACNT)=0 THEN PCON_JINE ELSE PCON_BALANCEACNT END AS PCON_HTZJ,PCON_YZHTH,OTHER_MONUNIT,PCON_MONUNIT,PCON_KPJE,PCON_RESPONSER,PCON_NOTE,CR_PSZT,c.* from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID left join TBPM_DETAIL as c on a.PCON_BCODE=c.CM_CONTR left join TBPM_BUSBILLRECORD as d on a.PCON_BCODE=d.BR_HTBH  order by  PCON_BCODE";
            string sql = "select PCON_CUSTMNAME,PCON_BCODE,TSA_ID,PCON_YZHTH,b.CM_PROJ,b.CM_ENGNAME,b.CM_MAP,b.CM_MATERIAL,b.CM_PRICE,b.CM_PRICE,b.CM_COUNT,b.CM_NUMBER,b.CM_UNIT,b.CM_WEIGHT,b.CM_ALL,b.CM_SIGN,b.CM_JIAO,b.CM_DUTY,b.CM_NOTE,b.CM_DTSJ,b.CM_CPRK,b.CM_TZFH,b.CM_CKSJ,b.CM_DFSM,PCON_YFK,CASE WHEN convert(decimal(18,2),PCON_BALANCEACNT)=0 THEN PCON_JINE ELSE PCON_BALANCEACNT END AS PCON_HTZJ,PCON_JINE,BR_KPJE from TBPM_CONPCHSINFO as a left join TBPM_DETAIL as b on a.PCON_BCODE=b.CM_CONTR left join (select BR_HTBH, sum(BR_KPJE)as BR_KPJE from TBPM_BUSBILLRECORD group by BR_HTBH) as d on a.PCON_BCODE=d.BR_HTBH order by PCON_BCODE";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            List<string> list = new List<string>();
            string filename = "销售合同导出" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("销售合同导出模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据

                IFont font1 = wk.CreateFont();
                font1.FontName = "仿宋";//字体
                font1.FontHeightInPoints = 11;//字号
                ICellStyle cells = wk.CreateCellStyle();
                cells.Alignment = HorizontalAlignment.CENTER;
                cells.SetFont(font1);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    IRow row = sheet0.CreateRow(i + 3);

                    row.CreateCell(0).SetCellValue(i + 1);
                    row.CreateCell(1).SetCellValue(dr["PCON_CUSTMNAME"].ToString());
                    row.CreateCell(2).SetCellValue(dr["PCON_BCODE"].ToString());
                    row.CreateCell(3).SetCellValue(dr["TSA_ID"].ToString());
                    row.CreateCell(4).SetCellValue(dr["PCON_YZHTH"].ToString());
                    row.CreateCell(5).SetCellValue(dr["CM_PROJ"].ToString());
                    row.CreateCell(6).SetCellValue(dr["CM_ENGNAME"].ToString());
                    row.CreateCell(7).SetCellValue(dr["CM_MAP"].ToString());
                    row.CreateCell(8).SetCellValue(dr["CM_MATERIAL"].ToString());
                    row.CreateCell(9).SetCellValue(dr["CM_PRICE"].ToString());
                    row.CreateCell(10).SetCellValue(dr["CM_COUNT"].ToString());
                    row.CreateCell(11).SetCellValue(dr["CM_NUMBER"].ToString());
                    row.CreateCell(12).SetCellValue(dr["CM_UNIT"].ToString());
                    row.CreateCell(13).SetCellValue(dr["CM_WEIGHT"].ToString());
                    row.CreateCell(14).SetCellValue(dr["CM_ALL"].ToString());
                    row.CreateCell(15).SetCellValue(dr["CM_SIGN"].ToString());
                    row.CreateCell(16).SetCellValue(dr["CM_JIAO"].ToString());
                    row.CreateCell(17).SetCellValue(dr["CM_DUTY"].ToString());
                    row.CreateCell(18).SetCellValue(dr["CM_NOTE"].ToString());
                    row.CreateCell(19).SetCellValue(dr["CM_DTSJ"].ToString());
                    row.CreateCell(20).SetCellValue(dr["CM_CPRK"].ToString());
                    row.CreateCell(21).SetCellValue(dr["CM_TZFH"].ToString());
                    row.CreateCell(22).SetCellValue(dr["CM_CKSJ"].ToString());
                    row.CreateCell(23).SetCellValue(dr["CM_DFSM"].ToString());
                    if (!list.Contains(dr["PCON_BCODE"].ToString()))
                    {
                        row.CreateCell(24).SetCellValue(string.Format("{0:N2}", Convert.ToDecimal(dr["PCON_YFK"].ToString()) / Convert.ToDecimal(Convert.ToDecimal(dr["PCON_HTZJ"].ToString()) == 0 ? "1" : dr["PCON_HTZJ"].ToString()) * 100) + "%");
                        row.CreateCell(25).SetCellValue(dr["PCON_YFK"].ToString());
                        row.CreateCell(26).SetCellValue(string.Format("{0:N2}", (1 - Convert.ToDecimal(dr["PCON_YFK"].ToString()) / Convert.ToDecimal(Convert.ToDecimal(dr["PCON_HTZJ"].ToString()) == 0 ? "1" : dr["PCON_HTZJ"].ToString())) * 100) + "%");
                        row.CreateCell(27).SetCellValue((Convert.ToDecimal(dr["PCON_JINE"].ToString()) - Convert.ToDecimal(dr["PCON_YFK"].ToString())).ToString());
                        row.CreateCell(28).SetCellValue(dr["BR_KPJE"].ToString());
                        list.Add(dr["PCON_BCODE"].ToString());
                    }
                    else
                    {
                        row.CreateCell(24).SetCellValue("");
                        row.CreateCell(25).SetCellValue("");
                        row.CreateCell(26).SetCellValue("");
                        row.CreateCell(27).SetCellValue("");
                        row.CreateCell(28).SetCellValue("");
                    }
                    for (int j = 0; j < 29; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }
                }

                #endregion

                sheet0.ForceFormulaRecalculation = true;

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        //未签订合同提醒
        private void Account()
        {
            string sql = "select count(ID) AS num FROM (TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID left join (select * from (select no=row_number() over(partition by CM_CONTR order by getdate()),* from TBPM_DETAIL) t where no=1) as c on a.PCON_BCODE=c.CM_CONTR left join (select distinct TFI_CONTR from TBMP_FINISHED_IN)t  on a.PCON_BCODE=t.TFI_CONTR )where PCON_JINE='0' and TFI_CONTR is not null";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            lblConWarn.Text = "(" + dt.Rows[0][0].ToString() + ")";

        }
        //未签订合同提醒
        protected void rblIfZaizhi_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.GetSqlText();
            this.InitVar();
            this.bindGrid();
            this.Select_record();
        }
    }
}
