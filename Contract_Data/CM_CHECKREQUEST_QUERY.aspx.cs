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
    public partial class CM_CHECKREQUEST_QUERY : BasicPage
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

        private void GetSqlText()
        {
            //**********************            
            StringBuilder str = new StringBuilder();
            str.Append("1=1 ");

            //项目名称           
            str.Append(" and PJNAME like '%" + txt_PJNAME.Text.Trim() + "%'");

            //合同号
            str.Append(" and HTBH like '%" + txt_HTBH.Text.Trim() + "%'");

            //收款单位
            str.Append(" and SKDW like '%" + txt_SKDW.Text.Trim() + "%'");

            //请款部门
            if (dplBM.SelectedIndex != -1 && dplBM.SelectedIndex != 0)
            {
                str.Append(" and QKBM like '%" + dplBM.SelectedItem.Text.Trim() + "%'");
            }

            //请款人
            if (dplQKR.SelectedIndex != -1 && dplQKR.SelectedIndex != 0)
            {
                str.Append(" and QKR like '%" + dplQKR.SelectedItem.Text.Trim() + "%'");
            }

            //请款状态
            if (ddl_QKZT.SelectedIndex != -1 && ddl_QKZT.SelectedIndex != 0)
            {
                str.Append(" and QKZT = '" + ddl_QKZT.SelectedValue.Trim() + "'");
            }

            //请款时间
            string startTime = sta_time.Text.Trim() == "" ? DateTime.Now.AddYears(-100).ToShortDateString() : sta_time.Text.Trim();
            string endTime = end_time.Text.Trim() == "" ? DateTime.Now.AddYears(100).ToShortDateString() : end_time.Text.Trim();
            str.Append(" and QKRQ>='" + startTime + "' AND QKRQ<='" + endTime + "'");

            ViewState["sqltext"] = str.ToString();

        }

        protected void dplBM_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindQKR();
            this.btn_search_Click(null, null);
        }

        private void BindQKBM()
        {
            //部门
            string sqltext_bm = "select distinct QKBM,QKBMID from View_ALL_CheckRequest";
            DataTable dt_bm = DBCallCommon.GetDTUsingSqlText(sqltext_bm);
            if (dt_bm.Rows.Count > 0)
            {
                dplBM.DataSource = dt_bm;
                dplBM.DataTextField = "QKBM";
                dplBM.DataValueField = "QKBMID";
                dplBM.DataBind();
                dplBM.Items.Insert(0, new ListItem("-全部-", "%"));
                dplBM.SelectedIndex = 0;
            }

        }

        private void BindQKR()
        {
            //请款人
            string sqltext_qkr = "select distinct QKR from View_ALL_CheckRequest ";
            if (dplBM.SelectedIndex != 0 && dplBM.SelectedIndex != -1)
            {
                sqltext_qkr += " where QKBMID='" + dplBM.SelectedValue.ToString() + "'";
            }
            DataTable dt_qkr = DBCallCommon.GetDTUsingSqlText(sqltext_qkr);
            if (dt_qkr.Rows.Count > 0)
            {
                dplQKR.DataSource = dt_qkr;
                dplQKR.DataTextField = "QKR";
                dplQKR.DataValueField = "QKR";
                dplQKR.DataBind();
            }
            dplQKR.Items.Insert(0, new ListItem("-全部-", ""));
            //如果登录人在请款人列表中，则默认选择登录人
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

        //数据绑定、分页
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
            pager.TableName = "View_ALL_CheckRequest";
            pager.PrimaryKey = "QKDH";
            pager.ShowFields = "*";
            pager.OrderField = "";
            pager.StrWhere = ViewState["sqltext"].ToString();
            pager.OrderType = 0;
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

        //显示筛选后的记录条数和合计金额
        private void Select_record()
        {
            string sqltext = "select QKJE,YZF from View_ALL_CheckRequest where " + ViewState["sqltext"].ToString();
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            lb_select_num.Text = dt.Rows.Count.ToString();
            decimal tot_qkmoney = 0;
            decimal tot_fkmoney = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tot_qkmoney += Convert.ToDecimal(dt.Rows[i]["QKJE"].ToString());
                tot_fkmoney += Convert.ToDecimal(dt.Rows[i]["YZF"].ToString());
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
                Label lbl_htbh = (Label)e.Row.FindControl("lbl_htbh");//合同编号               
                Label lbl_qkqc = (Label)e.Row.FindControl("lbl_qkqc");//请款期次
                Label lbl_qkdh = (Label)e.Row.FindControl("lbl_qkdh");//请款单号

                lbl_id.Text = ((this.pager.PageIndex - 1) * this.pager.PageSize + e.Row.RowIndex + 1).ToString();

                if (lbl_htbh.Text.Trim() != "") //合同号不为空的
                {
                    int index = lbl_qkdh.Text.ToString().IndexOf('-') + 1;
                    int lenth = lbl_qkdh.Text.ToString().Length - index;
                    lbl_qkqc.Text = lbl_qkdh.Text.ToString().Substring(index, lenth);
                }
            }
        }

        #region 按钮事件
        //查看请款
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

        //打印请款
        protected void btn_Print_Click(object sender, EventArgs e)
        {
            Action_Click(4);
        }

        //查看合同
        protected void btn_ViewHT_Click(object sender, EventArgs e)
        {
            Action_Click(5);
        }

        //添加非合同请款
        protected void btn_AddFHTQK_Click(object sender, EventArgs e)
        {
            Action_Click(6);
        }

        //导出请款单
        protected void btn_ExportToExcel_Click(object sender, EventArgs e)
        {
            Action_Click(7);
        }
        #endregion

        //按钮事件
        //1查看请款，2编辑请款，3删除请款，4打印请款，5查看合同，6添加非合同请款
        private void Action_Click(int clicktype)
        {
            //找到选定行的参数
            string htbh = "";
            string qkdh = "";
            string qkr = "";
            string qkzt = "";
            string htlx = "";
            bool check = false; ;
            for (int i = 0; i < grvQKD.Rows.Count; i++)
            {
                CheckBox cbx = (CheckBox)grvQKD.Rows[i].FindControl("CheckBox1");
                Label lbl_htbh = (Label)grvQKD.Rows[i].FindControl("lbl_htbh");//合同编号
                Label lbl_qkdh = (Label)grvQKD.Rows[i].FindControl("lbl_qkdh");//请款单号
                Label lbl_qkr = (Label)grvQKD.Rows[i].FindControl("lbl_qkr");//请款人
                Label lbl_qkzt = (Label)grvQKD.Rows[i].FindControl("lbl_qkzt");//请款状态
                Label lbl_htlx = (Label)grvQKD.Rows[i].FindControl("lbl_htlx");//合同类型

                if (cbx.Checked == true)
                {
                    htbh = lbl_htbh.Text.Trim();
                    qkdh = lbl_qkdh.Text.Trim();
                    qkr = lbl_qkr.Text.Trim();
                    qkzt = lbl_qkzt.Text.Trim();
                    htlx = lbl_htlx.Text.Trim();
                    check = true;
                    break;
                }
            }

            //判断是否选择了数据行
            if (!check && clicktype != 6 && clicktype != 7)//没有选择行，且不是添加非合同请款，导出操作
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择要操作的数据行！');", true); return;
            }
            else
            {
                switch (clicktype)
                {
                    case 1://查看请款
                        if (htbh == "" || htbh == null)//非合同请款
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "FHTQK('" + qkdh + "','View');", true);
                        }
                        else //合同请款
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "HTQK('" + qkdh + "','View','" + htlx + "');", true);
                        }
                        break;
                    case 2://编辑请款
                        if (CheckPower(qkr, qkzt, 2))
                        {
                            if (htbh == "" || htbh == null)//非合同请款，这里的编辑，不能进行付款操作，如要付款，需要到待办款项处
                            {
                                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "FHTQK('" + qkdh + "','Edit');", true);
                            }
                            else //合同请款
                            {
                                //当前登录人为财务时更改action值
                                if (Session["UserDeptID"].ToString() == "06")
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "HTQK('" + qkdh + "','EditCW','" + htlx + "');", true);
                                }
                                else if (Session["UserName"].ToString() == qkr)
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "HTQK('" + qkdh + "','Edit','" + htlx + "');", true);
                                }
                            }
                        }
                        break;
                    case 3://删除请款
                        if (CheckPower(qkr, qkzt, 3))
                        {
                            List<string> sqlstr = new List<string>();
                            string sqltext = "";

                            if (htbh == "" || htbh == null)//没有合同号即为非合同请款
                            {
                                sqltext = "delete from TBPM_FHT_CheckRequest where CR_QKDH='" + qkdh + "'";
                            }
                            else
                            {
                                sqltext = "delete from TBPM_CHECKREQUEST where CR_ID='" + qkdh + "'";
                            }
                            sqlstr.Add(sqltext);
                            DBCallCommon.ExecuteTrans(sqlstr);
                            Response.Redirect("CM_CHECKREQUEST_QUERY.aspx");
                        }
                        break;
                    case 4://打印请款
                        if (htbh == "" || htbh == null)//非合同请款
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('非合同请款不能打印');", true);
                        }
                        else //合同请款
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "PrintQK('" + qkdh + "');", true);
                        }
                        break;
                    case 5://查看合同
                        if (htbh == "" || htbh == null)//非合同请款
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('您选择的是非合同请款\\r没有相关合同信息！');", true);
                        }
                        else //合同请款
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "ViewHT('" + htbh + "','" + htlx + "');", true);
                        }
                        break;
                    case 6://添加非合同请款
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "Add_FHTQK();", true);
                        break;
                    case 7:
                        string sqlExport = "SELECT HTBH,SUBSTRING (QKDH,CHARINDEX('-',QKDH)+1,LEN(QKDH)-CHARINDEX('-',QKDH)) AS QKQC," +
                                           " PJNAME,QKJE,QKRQ,YZF,QKBM,QKR," +
                                           " CASE QKZT WHEN 0 THEN '保存' WHEN 1 THEN '正在签字' WHEN 2 THEN '提交财务-未付款' WHEN 3 THEN '部分支付' WHEN 4 THEN '已支付' END AS QKZT," +
                                           " QKYT,SKDW FROM View_ALL_CheckRequest" +
                                           "  WHERE " + ViewState["sqltext"].ToString();
                        DataTable dtExport = DBCallCommon.GetDTUsingSqlText(sqlExport);
                        if (dtExport.Rows.Count > 0)
                        {
                            ContractClass.ExportQKDataItem(dtExport);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('没有可以导出的数据，请重新筛选！');", true); return;

                        }
                        break;
                }
            }
        }

        //检查是否可以执行编辑和删除的操作
        private bool CheckPower(string qkr, string qkzt, int action)
        {
            bool check = true;
            if (!(Session["UserName"].ToString() == qkr || Session["UserDeptID"].ToString() == "06"))//既不是请款人，也不是财务的
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('您无权进行此操作！');", true); return check;
            }
            if (qkzt == "2")//请款状态为提交财务未付款的，可由财务将状态改回正在签字 只能修改不能删！
            {
                if (action == 2)  //编辑
                {
                    if (Session["UserDeptID"].ToString() != "06")
                    {
                        check = false;
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该状态的请款单只能由财务进行此操作！');", true); return check;
                    }
                }
                else if (action == 3) //删除
                {
                    check = false;
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该状态的请款单不能进行此操作！');", true); return check;
                }
            }
            if (qkzt == "3" || qkzt == "4")//请款状态为提交财务未付款，部分支付，已支付
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该状态的请款单不能进行此操作！');", true); return check;
            }
            return check;
        }
    }
}
