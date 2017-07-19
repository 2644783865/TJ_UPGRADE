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
using System.Collections.Generic;

/****数据库：TBPM_ORDER_BILL**********************/
/****MengQingTong  2013年1月17日 14:48:08*********/

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CR_OrderBill_Query : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindJBR();
                this.btn_search_Click(null, null);
            }
            this.InitVar();

        }

        //绑定经办人
        private void BindJBR()
        {
            string sql_jbr = "select distinct OB_JBR from TBPM_ORDER_BILL";
            DataTable dt_jbr = DBCallCommon.GetDTUsingSqlText(sql_jbr);
            if (dt_jbr.Rows.Count > 0)
            {
                dplJBR.DataSource = dt_jbr;
                dplJBR.DataTextField = "OB_JBR";
                dplJBR.DataValueField = "OB_JBR";
                dplJBR.DataBind();
            }
            dplJBR.Items.Insert(0, new ListItem("-全部-", ""));
            //如果登陆人在请款人列表，则默认选择登陆人
            for (int i = 0; i < dplJBR.Items.Count; i++)
            {
                if (Session["UserName"].ToString() == dplJBR.Items[i].Value.Trim())
                {
                    dplJBR.Items[i].Selected = true;
                    break;
                }
            }
        }

        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
       
        private void InitPager()
        {
            pager.TableName = "TBPM_ORDER_BILL";
            pager.PrimaryKey = "OB_CODE";
            pager.ShowFields = "OB_CODE,OB_DDCODE,OB_BILLCODE,OB_BILLNUM,OB_BILLJE,OB_KPDATE,OB_SPDATE,OB_CSNAME,OB_JBR,OB_PZH";
            pager.OrderField = "OB_CODE";
            pager.StrWhere = ViewState["sqltext"].ToString();
            pager.OrderType = 1;
            pager.PageSize = Convert.ToInt16(dpl_pageno.SelectedValue);
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        

        protected void btn_search_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.GetSqlText();
            this.InitVar();
            this.bindGrid();
            this.Select_record();   //筛选金额统计
        }

        private void GetSqlText()
        {
            StringBuilder str = new StringBuilder();
            str.Append(" 1=1 ");

            //发票记录编号
            str.Append(" and OB_CODE like '%" + txt_JLID.Text.Trim() + "%'");

            //订单号
            str.Append(" and OB_DDCODE like '%" + txt_DDCode.Text.Trim() + "%'");

            //经办人
            if (dplJBR.SelectedIndex != -1 && dplJBR.SelectedIndex != 0)
            {
                str.Append(" and OB_JBR LIKE '%" + dplJBR.SelectedItem.Text.Trim() + "%'");
            }

            //发票单号
            str.Append(" and OB_BILLCODE like '%" + txt_BillCode.Text.Trim() + "%' ");

            //开票单位
            str.Append(" and OB_CSNAME LIKE '%" + txt_KPDW.Text.Trim() + "%'");

            //开票时间
            string startKPTime = sta_KPtime.Text.Trim() == "" ? DateTime.Now.AddYears(-100).ToShortDateString() : sta_KPtime.Text.Trim();
            string endKPTime = end_KPtime.Text.Trim() == "" ? DateTime.Now.AddYears(100).ToShortDateString() : end_KPtime.Text.Trim();
            str.Append(" and OB_KPDATE>='" + startKPTime + "' AND OB_KPDATE<='" + endKPTime + "'");

            
            //收票时间
            if (sta_SPtime.Text.Trim() != "" || end_SPtime.Text.Trim() != "")
            {
                string startSPTime = sta_SPtime.Text.Trim() == "" ? DateTime.Now.AddYears(-100).ToShortDateString() : sta_SPtime.Text.Trim();
                string endSPTime = end_SPtime.Text.Trim() == "" ? DateTime.Now.AddYears(100).ToShortDateString() : end_SPtime.Text.Trim();
                str.Append(" and OB_SPDATE>='" + startSPTime + "' AND OB_SPDATE<='" + endSPTime + "'");
            }

            ViewState["sqltext"] = str.ToString();
        }


        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, grvDDBill, UCPaging1, pal_NoData);
            if (pal_NoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
        }
       
        protected void grvDDBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                e.Row.Attributes.Add("onclick", "RowClick(this)");
                e.Row.Attributes["style"] = "Cursor:pointer";
                Label lbl_id = (Label)e.Row.FindControl("lbl_id");//序号
                Label lbl_obcode = (Label)e.Row.FindControl("lbl_obcode");//请款单号
                lbl_id.Text = ((this.pager.PageIndex - 1) * this.pager.PageSize + e.Row.RowIndex + 1).ToString();

                int index = lbl_obcode.Text.ToString().IndexOf('-') + 1;
                int lenth = lbl_obcode.Text.ToString().Length - index;
            }
            foreach (GridViewRow gr in grvDDBill.Rows)
            {
                if (Session["UserDeptID"].ToString() == "08")
                {
                    ((HyperLink)gr.FindControl("bill_EditCW")).Visible = true;
                }
            }
            
        }
        private void Select_record()
        {
            string sqltext = "select OB_DDZJE,OB_BILLJE FROM TBPM_ORDER_BILL WHERE " + ViewState["sqltext"].ToString();
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            lb_select_num.Text = dt.Rows.Count.ToString();
            decimal tot_DDmoney = 0;
            decimal tot_KPmoney = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tot_DDmoney += Convert.ToDecimal(dt.Rows[i]["OB_DDZJE"].ToString());
                tot_KPmoney += Convert.ToDecimal(dt.Rows[i]["OB_BILLJE"].ToString());
            }
            lb_total_DDmoney.Text = string.Format("{0:c}", tot_DDmoney);
            lb_total_KPmoney.Text = string.Format("{0:c}", tot_KPmoney);
        }

        //重置筛选条件
        protected void btn_Reset_Click(object sender, EventArgs e)
        {
            txt_JLID.Text = "";
            txt_DDCode.Text = "";
            txt_BillCode.Text = "";
            txt_KPDW.Text = "";
            dplJBR.SelectedIndex = 0;
            sta_KPtime.Text = "";
            end_KPtime.Text = "";
            sta_SPtime.Text = "";
            end_SPtime.Text = "";
            btn_search_Click(null, null);
        }

        //查看
        protected void btn_ViewDDBill_Click(object sender, EventArgs e)
        {
            Action_Click(1);
        }
        //编辑
        protected void btn_EditDDBill_Click(object sender, EventArgs e)
        {
            Action_Click(2);
        }
        //删除
        protected void btn_DelDDBill_Click(object sender, EventArgs e)
        {
            Action_Click(3);
        }
        //查看订单
        protected void btn_ViewDD_Click(object sender, EventArgs e)
        {
            Action_Click(4);
        }

        //按钮点击事件
        private void Action_Click(int clicktype)
        {
            string obcode = "";
            string ddcode = "";
            string jbr="";
            bool check = false;
            for (int i = 0; i < grvDDBill.Rows.Count; i++)
            {
                CheckBox cbx = (CheckBox)grvDDBill.Rows[i].FindControl("CheckBox1");
                Label lbl_obcode = (Label)grvDDBill.Rows[i].FindControl("lbl_obcode");
                Label lbl_obddcode = (Label)grvDDBill.Rows[i].FindControl("lbl_obddcode");
                Label lbl_jbr = (Label)grvDDBill.Rows[i].FindControl("lbl_jbr");
                if (cbx.Checked == true)
                {
                    obcode = lbl_obcode.Text.Trim();
                    ddcode = lbl_obddcode.Text.Trim();
                    jbr=lbl_jbr.Text.Trim();
                    check = true;
                    break;
                }
            }
            if (!check)//没有选择行，且不是导出操作
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择要操作的数据行！');", true); return;
            }
            else
            {
                switch (clicktype)
                {
                    case 1: //查看
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "ViewBill('" + obcode + "','View');", true);
                        break;

                    case 2: //编辑
                        if (Session["UserDeptID"].ToString() == "08")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "ViewBill('" + obcode + "','EditCW');", true);
                        }
                        else if (Session["UserName"].ToString() == jbr )
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "ViewBill('" + obcode + "','Edit');", true);
                        }
                        else
                        {
                            check = false;
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('您无权进行此操作！');", true); 
                        }
                        break;

                    case 3: //删除
                        if (Session["UserDeptID"].ToString() == "08" || Session["UserName"].ToString() == jbr)
                        {
                            List<string> sqlstr = new List<string>();
                            string sqltext = "";
                            sqltext = "delete from TBPM_ORDER_BILL where OB_CODE='" + obcode + "'";
                            sqlstr.Add(sqltext);
                            DBCallCommon.ExecuteTrans(sqlstr);
                            Response.Redirect("CR_OrderBill_Query.aspx");
                        }
                        else
                        {
                            check = false;
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('您无权进行此操作！');", true);
                        }
                        break;

                    case 4: //查看订单
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "View_PurOrder('" + ddcode + "');", true);
                        break;

                }
            }      
        }
    }
}
