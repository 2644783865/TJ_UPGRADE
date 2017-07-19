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

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CR_NotContractOrder_Query : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindQKBM();
                BindQKR();
                this.btn_search_Click(null, null);
            }
            this.InitVar();

        }

        #region   数据绑定、分页

        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        private void InitPager()
        {
            pager.TableName = "TBPM_ORDER_CHECKREQUEST";
            pager.PrimaryKey = "DQ_ID";
            pager.ShowFields = "DQ_ID,DQ_DATA,DQ_QKBM,DQ_BMNAME,DQ_QKR,DQ_USE,DQ_DDCODE,DQ_DDZJE,DQ_CSNAME,DQ_CSBANK,DQ_CSACCOUNT,DQ_AMOUNT,DQ_AMOUNTDX,DQ_ZFFS,DQ_BILLCODE,DQ_STATE,DQ_NOTE";
            pager.OrderField = "";
            pager.StrWhere = ViewState["sqltext"].ToString();
            pager.OrderType = 1;
            pager.PageSize = Convert.ToInt16(ddl_pageno.SelectedValue);
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        //绑定请款部门
        private void BindQKBM()
        {
            string sqltext_bm = "select distinct a.DQ_QKBM,b.DEP_CODE,b.DEP_NAME from TBPM_ORDER_CHECKREQUEST a,TBDS_DEPINFO b where a.DQ_QKBM=b.DEP_CODE";
            DataTable dt_bm = DBCallCommon.GetDTUsingSqlText(sqltext_bm);
            if (dt_bm.Rows.Count > 0)
            {
                dplBM.DataSource = dt_bm;
                dplBM.DataTextField = "DEP_NAME";
                dplBM.DataValueField = "DQ_QKBM";
                dplBM.DataBind();
                dplBM.Items.Insert(0, new ListItem("-全部-", "%"));
                dplBM.SelectedIndex = 0;
            }
        }

        //绑定请款人
        private void BindQKR()
        {
            string sql_qkr = "select distinct DQ_QKR from TBPM_ORDER_CHECKREQUEST";
            if (dplBM.SelectedIndex != 0 && dplBM.SelectedIndex != -1)
            {
                sql_qkr += "where DQ_QKBM='" + dplBM.SelectedValue.ToString() + "'";
            }
            DataTable dt_qkr = DBCallCommon.GetDTUsingSqlText(sql_qkr);
            if (dt_qkr.Rows.Count > 0)
            {
                dplQKR.DataSource = dt_qkr;
                dplQKR.DataTextField = "DQ_QKR";
                dplQKR.DataValueField = "DQ_QKR";
                dplQKR.DataBind();
            }
            dplQKR.Items.Insert(0, new ListItem("-全部-", ""));
            //如果登陆人在请款人列表，则默认选择登陆人
            for (int i = 0; i < dplQKR.Items.Count; i++)
            {
                if (Session["UserName"].ToString() == dplQKR.Items[i].Value.Trim())
                {
                    dplQKR.Items[i].Selected = true;
                    break;
                }
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

        //获取查询数据
        private void GetSqlText()
        {
            StringBuilder str = new StringBuilder();
            str.Append("1=1 ");

            
            //请款单号
            str.Append("and DQ_ID like '%" + txt_DQCode.Text.Trim() + "%'");
            //请款单号
            str.Append("and DQ_DDCode like '%" + txt_DDCode.Text.Trim() + "%'");
            //请款部门
            if(dplBM.SelectedIndex!=-1&&dplBM.SelectedIndex!=0)
            {
                str.Append(" and DQ_QKBM like '%" + dplBM.SelectedItem.Text.Trim() + "%'");
            }
            //请款人
            if(dplQKR.SelectedIndex!=-1&&dplQKR.SelectedIndex!=0)
            {
                str.Append(" and DQ_QKR like '%" + dplQKR.SelectedItem.Text.Trim() + "%'");
            }
            //收款单位
            str.Append(" and DQ_CSNAME like '%" + txt_SKDW.Text.Trim() + "%'");
            
            ////如果财务登录，则筛选正在签字、未支付
            //if (Session["UserDeptID"].ToString() == "08")
            //{
            //    if (ddl_QKZT.SelectedIndex != -1)
            //    {
            //        if (ddl_QKZT.SelectedIndex != 0)
            //        {
            //            str.Append(" and DQ_STATE = '" + ddl_QKZT.SelectedValue.Trim() + "'");
            //        }
                    
            //    }
            //    else
            //        str.Append(" and DQ_STATE = '1' or DQ_STATE='2'"); 

            //}
            //请款状态
            if (ddl_QKZT.SelectedIndex != -1 && ddl_QKZT.SelectedIndex != 0)
            {
                str.Append(" and DQ_STATE = '" + ddl_QKZT.SelectedValue.Trim() + "'");
            }
            //请款时间
            string startTime = sta_time.Text.Trim() == "" ? DateTime.Now.AddYears(-100).ToShortDateString() : sta_time.Text.Trim();
            string endTime = end_time.Text.Trim() == "" ? DateTime.Now.AddYears(100).ToShortDateString() : end_time.Text.Trim();
            str.Append(" and DQ_DATA>='" + startTime + "' AND DQ_DATA<='" + endTime + "'");

            ViewState["sqltext"] = str.ToString();

        }
        

        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, grvQKD, UCPaging1, pal_NoData);
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

        //筛选订单金额统计
        private void Select_record()
        {
            string sqltext = "select DQ_DDZJE,DQ_AMOUNT from TBPM_ORDER_CHECKREQUEST where " + ViewState["sqltext"].ToString();
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            lb_select_num.Text = dt.Rows.Count.ToString();
            decimal tot_qkmoney = 0;
            decimal tot_fkmoney = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tot_qkmoney += Convert.ToDecimal(dt.Rows[i]["DQ_DDZJE"].ToString());
                tot_fkmoney += Convert.ToDecimal(dt.Rows[i]["DQ_AMOUNT"].ToString());
            }
            lb_total_qkmoney.Text = string.Format("{0:c}", tot_qkmoney);
            lb_total_fkmoney.Text = string.Format("{0:c}", tot_fkmoney);
        }

        #endregion

        protected void grvQKD_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                e.Row.Attributes.Add("onclick", "RowClick(this)");
                e.Row.Attributes["style"] = "Cursor:pointer";

                Label lbl_id = (Label)e.Row.FindControl("lbl_id");//序号
                Label lbl_qkdh = (Label)e.Row.FindControl("lbl_qkdh");//请款单号
                Label lbl_qkzt = (Label)e.Row.FindControl("lbl_qkzt");//请款状态
                lbl_id.Text = ((this.pager.PageIndex - 1) * this.pager.PageSize + e.Row.RowIndex + 1).ToString();
                
                int index = lbl_qkdh.Text.ToString().IndexOf('-') + 1;
                int lenth = lbl_qkdh.Text.ToString().Length - index;
            }
            //foreach (GridViewRow gr in grvQKD.Rows)
            //{
            //    if (Session["UserDeptID"].ToString() == "08")
            //    {
            //        ((HyperLink)gr.FindControl("btn_CWFK")).Visible = true;
            //    }
            //}

        }

        //重置筛选
        protected void btn_Reset_Click(object sender, EventArgs e)
        {
            txt_DDCode.Text = "";
            txt_DQCode.Text = "";
            dplBM.SelectedIndex = 0;
            dplQKR.SelectedIndex = 0;
            txt_SKDW.Text = "";
            ddl_QKZT.SelectedIndex = 0;
            sta_time.Text = "";
            end_time.Text = "";
            btn_search_Click(null,null);
        }
        
        #region 按钮事件
        //查看
        protected void btn_ViewQK_Click(object sender, EventArgs e)
        {
            Action_Click(1);
        }
        //编辑请款
        protected void btn_EditQK_Click(object sender, EventArgs e)
        {
            Action_Click(2);
        }
        //删除请款
        protected void btn_DelQK_Click(object sender, EventArgs e)
        {
            Action_Click(3);
        }
        //查看订单
        protected void btn_ViewDD_Click(object sender, EventArgs e)
        {
            Action_Click(4);
        }
        //导出
        protected void btn_ExportToExcel_Click(object sender, EventArgs e)
        {
            Action_Click(5);
        }
        //打印
        protected void btn_Print_Click(object sender, EventArgs e)
        {
            Action_Click(6);
        }
        //查看发票
        protected void btn_ViewBill_Click(object sender, EventArgs e)
        {
            Action_Click(7);
        }
        //财务付款
        protected void btn_CWFK_Click(object sender, EventArgs e)
        {
            Action_Click(8);
        }
        
        
        #endregion  

        //按钮点击事件
        private void Action_Click(int clicktype)
        {
            string qkdh = "";
            string qkr = "";
            string qkzt = "";
            string ddcode = "";
            string bill = "";
            bool check = false; ;
            for (int i = 0; i < grvQKD.Rows.Count; i++)
            {
                CheckBox cbx = (CheckBox)grvQKD.Rows[i].FindControl("CheckBox1");
                Label lbl_qkdh = (Label)grvQKD.Rows[i].FindControl("lbl_qkdh");//请款单号
                Label lbl_qkr = (Label)grvQKD.Rows[i].FindControl("lbl_qkr");//请款人
                Label lbl_qkzt = (Label)grvQKD.Rows[i].FindControl("lbl_qkzt");//请款状态
                Label lbl_ddcode = (Label)grvQKD.Rows[i].FindControl("lbl_ddcode");
                Label lbl_bill = (Label)grvQKD.Rows[i].FindControl("lbl_bill");

                if (cbx.Checked == true)
                {
                    qkdh = lbl_qkdh.Text.Trim();
                    qkr = lbl_qkr.Text.Trim();
                    qkzt = lbl_qkzt.Text.Trim();
                    ddcode = lbl_ddcode.Text.Trim();
                    bill = lbl_bill.Text.Trim();
                    
                    check = true;
                    break;
                }
            }
            //判断是否选择了数据行
            if (!check && clicktype != 5)//没有选择行，且不是导出操作
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择要操作的数据行！');", true); return;
            }
            else
            {
                switch (clicktype)
                {
                    case 1: //查看请款
                        
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "ViewQK('" + qkdh + "','View');", true);
                            break;
                    case 2: //编辑请款

                            if (CheckPower(qkr, qkzt, 2))
                            {
                                if (Session["UserDeptID"].ToString() == "08")
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "ViewQK('" + qkdh + "','EditCW');", true);
                                }
                                else if (Session["UserName"].ToString() == qkr)
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "ViewQK('" + qkdh + "','Edit');", true);
                                }
                            }
                            break;
                    case 3:  //删除请款
                            if (CheckPower(qkr, qkzt, 3))
                            {
                                List<string> sqlstr = new List<string>();
                                string sqltext = "";
                                sqltext = "delete from TBPM_ORDER_CHECKREQUEST where DQ_ID='" + qkdh + "'";
                                sqlstr.Add(sqltext);
                                DBCallCommon.ExecuteTrans(sqlstr);
                                Response.Redirect("CR_NotContractOrder_Query.aspx");
                            }
                            break;
                    case 4: //查看订单

                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "View_PurOrder('" + ddcode + "');", true);
                            break;
                    case 5: //导出
                            string sqlExport="select DQ_ID,DQ_DDCODE,DQ_DDZJE,DQ_DATA,DQ_BMNAME,DQ_QKR,"+
                                " CASE DQ_STATE WHEN 0 THEN '保存' WHEN 1 THEN '正在签字' WHEN 2 THEN '提交财务-未付款' WHEN 3 THEN '已支付' END AS DQ_STATE,"+
                                "DQ_USE,DQ_CSNAME,DQ_CSBank,DQ_CSACCOUNT from TBPM_ORDER_CHECKREQUEST"+
                                " WHERE " + ViewState["sqltext"].ToString();
                            DataTable dtExport = DBCallCommon.GetDTUsingSqlText(sqlExport);
                            if (dtExport.Rows.Count > 0)
                            {
                                ContractClass.ExportDDQKDataItem(dtExport);
                            }
                            break;
                    case 6:  //打印
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "PrintDDQK('" + qkdh + "');", true);
                            break;
                    case 7:  //查看发票
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "View_Bill('" + bill + "','DView');", true);
                            break;
                    case 8: //财务付款
                            if (CheckPower(qkr, qkzt, 4))
                            {
                                if (Session["UserDeptID"].ToString() == "08")
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "ViewQK('" + qkdh + "','EditCW');", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('您无权进行此操作！');", true);
                                }
                            }
                            break;
                }
            }

        }
        //检查是否可以执行编辑和删除的操作
        private bool CheckPower(string qkr, string qkzt, int action)
        {
            bool check = true;
            if (!(Session["UserName"].ToString() == qkr || Session["UserDeptID"].ToString() == "08"))//既不是请款人，也不是财务的
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('您无权进行此操作！');", true); return check;
            }
            if (qkzt == "0")//保存，尚未开始签字
            {
                if (action == 4)
                { 
                    check = false;
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该请款单尚未开始签字，不可付款！');", true); return check;
                }
            }
            if (qkzt == "1") //正在签字
            {
                if (action == 2)
                {
                    if (Session["UserDeptID"].ToString() != "08")
                    {
                        check = false;
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该状态的请款单只能由财务进行此操作！');", true); return check;
                    }
                }
                if (action == 3) //删除
                {
                    check = false;
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该请款单正在签字，不能删除！');", true); return check;
                }
            }
            if (qkzt == "2")//请款状态为提交财务未付款的，可由财务将状态改回正在签字 只能修改不能删！
            {
                if (action == 2 || action == 4)  //编辑 and 财务付款
                {
                    if (Session["UserDeptID"].ToString() != "08")
                    {
                        check = false;
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该状态的请款单只能由财务进行此操作！');", true); return check;
                    }
                }
                else if (action == 3) //删除
                {
                    check = false;
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该状态的请款单不能删除！');", true); return check;
                }
            }
            if (qkzt == "3")//请款状态为提交财务已支付
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该请款单财务已支付，不能进行此操作！');", true); return check;
            }
            return check;
        }

        //部门筛选
        protected void dplBM_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindQKR();
            this.btn_search_Click(null, null);
        }
    }
}
