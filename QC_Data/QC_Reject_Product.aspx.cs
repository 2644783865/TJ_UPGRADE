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
using System.Collections.Generic;
using System.Drawing;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_Reject_Product : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDll();
                btn_search_Click(null, null);
            }
            this.InitVar();
            CheckUser(ControlFinder);
        }

        private void BindDll()
        {
            string sqltext = "select distinct ST_NAME ,BZR from View_TBQC_RejectPro_Info_Detail";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            dpl_zdr.DataSource = dt;
            dpl_zdr.DataTextField = "ST_NAME";
            dpl_zdr.DataValueField = "BZR";
            dpl_zdr.DataBind();
            dpl_zdr.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
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
            pager.TableName = "View_TBQC_RejectPro_Info_Detail";
            pager.PrimaryKey = "Order_id";
            pager.ShowFields = "";
            pager.OrderField = "Inform_time";
            pager.StrWhere = ViewState["sqlText"].ToString();
            pager.OrderType = 1;//按时间升序序排列
            pager.PageSize = Convert.ToInt16(ddl_pageno.SelectedValue.ToString());

        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GV_RejectPro, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
        }
        #endregion

        protected void btn_search_Click(object sender, EventArgs e)
        {
            //yj1,yj2,yj3,yj4分别表示签发人，技术部负责人，领导，验证人（制单人）的意见
            string sqltext = "1=1";

            if (tb_orderid.Text != "")
            {
                sqltext += " and Order_id like '%" + tb_orderid.Text.Trim() + "%'";
            }

            ////判定等级
            //if (ddl_rank.SelectedIndex != 0)
            //{
            //    sqltext += " and Rank like '%" + ddl_rank.SelectedValue + "%'";
            //}
            if (txtTH.Text.Trim() != "")
            {
                sqltext += " and TH like '%" + txtTH.Text.Trim() + "%'";
            }
            //处置方式
            if (ddl_czfs.SelectedIndex != 0)
            {
                //if (ddl_czfs.SelectedIndex == 1)//未鉴定  领导还没审的都属于未鉴定，以领导意见为准
                //{
                //    sqltext += " and yj3 is null ";
                //}
                //else //处置方式确定的必须是领导审过的
                //{

                //  sqltext += " and yj2=" + ddl_czfs.SelectedValue + " and yj3 is not null";
                sqltext += "and CZFS=" + ddl_czfs.SelectedValue;
                //}

            }
            //是否验证 （只有返修返工时才有验证），1已验证，2未验证
            //选择返修返工时才判断是否验证，其他情况不判断验证情况，加快查询速度
            switch (ddl_yz.SelectedIndex)
            {
                case 1: // 已验证                        
                    sqltext += " and STATE='5'";
                    break;
                case 2: //未验证                        
                    sqltext += " and STATE in ('3','6')";
                    break;
            }
            //验证时间
            if (sta_time.Text.Trim() != "" || end_time.Text.Trim() != "")
            {
                sqltext += " and Inform_time>='" + sta_time.Text.Trim() + "' and Inform_time<='" + end_time.Text.Trim() + "'";
            }

            //审批状态

            if (ddl_state.SelectedIndex != 0)
            {
                switch (ddl_state.SelectedIndex)
                {
                    case 1://审批中                         
                        sqltext += " and STATE='1'or STATE='2' or STATE='3' or STATE='6' or STATE='7' ";
                        break;
                    case 2://已审批
                        sqltext += " and STATE ='5'";
                        break;
                    case 3://待处理
                        sqltext += " and ((STATE='7' and SPR_ZL_ID='" + Session["UserID"] + "') or (STATE='1' and PSR_ID='" + Session["UserID"] + "') or (STATE='2' and SPR_ID='" + Session["UserID"] + "') or (STATE='3' and BZR='" + Session["UserID"] + "') or (Copy_dep like '%" + Session["UserDeptID"].ToString() + "%' and STATE='3') or (STATE='6' and ZGLD_ID='" + Session["UserID"] + "') ) ";
                        break;
                    case 4://已驳回
                        sqltext += " and STATE='4'"; break;

                }
            }



            //编制人
            if (dpl_zdr.SelectedIndex != 0)
            {
                sqltext += " and BZR = '" + dpl_zdr.SelectedValue.ToString() + "'";
            }

            //责任部门
            if (dpl_CSBM.SelectedIndex != 0)
            {
                if (dpl_CSBM.SelectedIndex != 5)
                {
                    sqltext += " and Copy_Dep LIKE '%" + dpl_CSBM.SelectedItem.Value + "%'";
                }
                else
                {
                    sqltext += " and (Copy_Dep IS NULL OR Copy_Dep='')";
                }
            }
            if (tb_conId.Text.Trim() != "")
            {
                sqltext += " and CON_ID like '%" + tb_conId.Text.Trim() + "%'";
            }
            if (tb_pjinfo.Text.Trim() != "")
            {
                sqltext += " and PJ_NAME like '%" + tb_pjinfo.Text.Trim() + "%'";
            }
            if (txt_BJMC.Text.Trim() != "")
            {
                sqltext += " and BJMC like '%" + txt_BJMC.Text.Trim() + "%'";
            }
            if (txtZRBM.Text.Trim() != "")
            {
                sqltext += " and Duty_dep like '%" + txtZRBM.Text.Trim() + "%'";
            }
            if (txtZRF.Text.Trim() != "")
            {
                sqltext += " and ZRF like '%" + txtZRF.Text.Trim() + "%'";
            }

            ViewState["sqlText"] = sqltext;
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindGrid();


            #region 只有点击待审批时才显示审批列，全部和已审批隐藏此列
            if (ddl_state.SelectedIndex == 3)
            {
                GV_RejectPro.Columns[15].Visible = true;
            }
            else
            {
                GV_RejectPro.Columns[15].Visible = false;
            }

            #endregion
        }

        protected void GV_RejectPro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                string orderid = ((Label)e.Row.FindControl("lb_orderid")).Text;
                string mpState = ((Label)e.Row.FindControl("lblMPSTATE")).Text;
                if (mpState == "1" && Session["UserDeptID"].ToString() == "04")
                {
                    e.Row.Cells[0].BackColor = Color.Green;
                }

                string purName = e.Row.Cells[13].Text;

                if (Session["UserDeptID"].ToString() == "05")
                {
                    if (purName != "" && purName != "&nbsp;")
                    {
                        e.Row.Cells[0].BackColor = Color.Green;
                        e.Row.Cells[0].Attributes.Add("title", "已任务分工");

                    }
                }
                //勾选行事件，双击事件
                e.Row.Attributes.Add("onclick", "RowClick(this)");

                e.Row.Attributes["style"] = "Cursor:pointer";

                e.Row.Attributes.Add("ondblclick", "window.open('QC_Reject_Product_Add.aspx?action=view&ID=" + orderid + "');");
                e.Row.Attributes.Add("title", "双击查看详细信息");

            }
        }

        private bool Check_Opinion(string sqltext)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows[0]["Per_note"].ToString() == "")
                return false;
            else
                return true;
        }

        protected void btn_reset_Click(object sender, EventArgs e)
        {
            // ddl_rank.SelectedIndex = 0;//判定等级
            txtTH.Text = "";
            ddl_czfs.SelectedIndex = 0;//处置方式
            ddl_yz.SelectedIndex = 0;//是否验证
            ddl_state.SelectedIndex = 0;//评审状态
            dpl_zdr.SelectedIndex = 0;//编制人


            tb_conId.Text = "";//合同号
            tb_pjinfo.Text = "";//项目名称
            txt_BJMC.Text = ""; //部件名称

            tb_orderid.Text = "";//单据编号

            //验证时间
            sta_time.Text = "";
            end_time.Text = "";

            this.btn_search_Click(null, null);
        }

        #region 按钮事件
        protected void btn_View_Click(object sender, EventArgs e)
        {
            Action_Click(1);
        }

        protected void btn_Edit_Click(object sender, EventArgs e)
        {
            Action_Click(2);
        }

        protected void btn_Del_Click(object sender, EventArgs e)
        {
            Action_Click(3);
        }

        protected void btn_Print_Click(object sender, EventArgs e)
        {
            Action_Click(4);
        }

        protected void btn_ChgNote_Click(object sender, EventArgs e)
        {
            Action_Click(5);
        }
        protected void btnReject_Click(object sender, EventArgs e)
        {
            Action_Click(6);
        }
        #endregion

        //1查看，2编辑，3删除，4打印，5修改审批意见
        private void Action_Click(int clicktype)
        {
            //找到选定行的参数
            string orderid = "";
            string zdr = "";
            string state = "";

            bool check = false; ;
            for (int i = 0; i < GV_RejectPro.Rows.Count; i++)
            {
                CheckBox cbx = (CheckBox)GV_RejectPro.Rows[i].FindControl("CheckBox1");
                Label lb_orderid = (Label)GV_RejectPro.Rows[i].FindControl("lb_orderid");//单号
                Label lb_bzr = (Label)GV_RejectPro.Rows[i].FindControl("lb_bzr");//编制人               
                Label st = (Label)GV_RejectPro.Rows[i].FindControl("lb_state");//状态
                if (cbx.Checked == true)
                {
                    orderid = lb_orderid.Text.Trim();
                    zdr = lb_bzr.Text.Trim();
                    state = st.Text.Trim();
                    check = true;
                    break;
                }
            }

            //判断是否选择了数据行
            if (!check)//没有选择行，且不是添加合同操作
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请勾选要操作的数据行！');", true); return;
            }
            else
            {
                switch (clicktype)
                {
                    case 1://查看
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "window.open('QC_Reject_Product_Add.aspx?action=view&ID=" + orderid + "');", true);
                        break;

                    case 2://编辑
                        if (zdr != Session["UserID"].ToString())
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('只能编制人才能进行此操作！');", true);
                            return;
                        }
                        else if (!(state == "0" || state == "7"))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该单已进入审批阶段，无法重新编辑！');", true);
                            return;
                        }

                        else
                        {
                            Response.Redirect("QC_Reject_Product_Add.aspx?action=edit&ID=" + orderid + "");
                            break;
                        }

                    case 3://删除
                        if (zdr != Session["UserID"].ToString())
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('只能编制人才能进行此操作！');", true);
                            return;
                        }
                        else if (!(state == "7" || state == "0"))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该单已进入审批阶段，无法删除！');", true);
                            return;
                        }
                        else
                        {
                            string sqldel1 = "delete from TBQC_RejectPro_Info where Order_id='" + orderid + "'";
                            string sqldel2 = "delete from TBQC_RejectPro_Rev where Rev_id='" + orderid + "'";
                            DBCallCommon.ExeSqlText(sqldel1);
                            DBCallCommon.ExeSqlText(sqldel2);
                            bindGrid();
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('删除成功！');", true);
                            break;
                        }
                    case 4://打印
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "window.open('QC_Reject_Product_Print.aspx?ID=" + orderid + "');", true);
                        break;
                    case 5:
                        //修改审批意见，先检查此人是否在审批人列表当中，再检查他是否已经审批过了，满足条件才能进行操作
                        string sqltext = "select SPR_ZL_ID,SPR_ID,PSR_ID,STATE,ZGLD_ID from View_TBQC_RejectPro_Info_Detail where Order_id='" + orderid + "'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dt.Rows.Count > 0)
                        {
                            string spr_zl_ID = dt.Rows[0]["SPR_ZL_ID"].ToString();
                            string psrID = dt.Rows[0]["PSR_ID"].ToString();
                            string sprID = dt.Rows[0]["SPR_ID"].ToString();
                            string zgldID = dt.Rows[0]["ZGLD_ID"].ToString();
                            string userID = Session["UserID"].ToString();
                            state = dt.Rows[0]["STATE"].ToString();
                            if ((state == "1" || state == "2" || state == "3" || state == "4" || state == "5" || state == "6") && spr_zl_ID == userID)
                            {
                                List<string> sqlist = new List<string>();
                                string sql = "update TBQC_RejectPro_Rev set SPR_ZL_NOTE='',SPR_ZL_RESULT='',SPR_RESULT='',SPR_NOTE='',YZ_NOTE='',YZ_RESULT='', PSR_RESULT='',PSR_NOTE='', ZGLD_RESULT='',ZGLD_NOTE=''  where  Rev_id='" + orderid + "'";
                                sqlist.Add(sql);
                                sql = "update TBQC_RejectPro_Info set STATE='7',Rank='',CZFS='' where Order_id='" + orderid + "'";
                                sqlist.Add(sql);
                                DBCallCommon.ExecuteTrans(sqlist);
                                Response.Redirect("QC_Reject_Product_Add.aspx?action=audit&ID=" + orderid + "");

                            }

                            else if (state == "2" && psrID == userID)
                            {
                                List<string> sqlist = new List<string>();
                                string sql = "update TBQC_RejectPro_Rev set PSR_RESULT='',PSR_NOTE='' where  Rev_id='" + orderid + "'";
                                sqlist.Add(sql);
                                sql = "update TBQC_RejectPro_Info set STATE='1',Rank='',CZFS='' where Order_id='" + orderid + "'";
                                sqlist.Add(sql);
                                DBCallCommon.ExecuteTrans(sqlist);
                                Response.Redirect("QC_Reject_Product_Add.aspx?action=audit&ID=" + orderid + "");

                            }
                            else if ((state == "3" || state == "4" || state == "5" || state == "6") && sprID == userID)
                            {
                                List<string> sqlist = new List<string>();
                                string sql = "update TBQC_RejectPro_Rev set SPR_RESULT='',SPR_NOTE='',YZ_NOTE='',YZ_RESULT='' where  Rev_id='" + orderid + "'";
                                sqlist.Add(sql);
                                sql = "update TBQC_RejectPro_Info set STATE='2' where Order_id='" + orderid + "'";
                                sqlist.Add(sql);
                                DBCallCommon.ExecuteTrans(sqlist);
                                Response.Redirect("QC_Reject_Product_Add.aspx?action=audit&ID=" + orderid + "");
                            }
                            else if ((state == "3" || state == "4" || state == "5") && zgldID == userID)
                            {
                                List<string> sqlist = new List<string>();
                                string sql = "update TBQC_RejectPro_Rev set ZGLD_RESULT='',SPR_NOTE='',ZGLD_NOTE='',YZ_RESULT='' where  Rev_id='" + orderid + "'";
                                sqlist.Add(sql);
                                sql = "update TBQC_RejectPro_Info set STATE='6' where Order_id='" + orderid + "'";
                                sqlist.Add(sql);
                                DBCallCommon.ExecuteTrans(sqlist);
                                Response.Redirect("QC_Reject_Product_Add.aspx?action=audit&ID=" + orderid + "");
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('必须由审核人重审该条记录！');", true);
                            }


                            break;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('您不在这条记录的审批人列表中！');", true);
                            return;
                        }
                    case 6://驳回处理

                        if (zdr == Session["UserID"].ToString() && state == "4")
                        {
                            string sqlText = "update  TBQC_RejectPro_Info set state='7'  where Order_id='" + orderid + "'";
                            DBCallCommon.ExeSqlText(sqlText);
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该记录已处理，请到审核中找出重新编辑');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('只有制单人才能处理该条记录');", true);
                        }
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "window.open('QC_Reject_Product_Add.aspx?action=view&ID=" + orderid + "');", true);
                        break;
                }
            }
        }

        //审核
        protected void Lbtn_review_OnClick(object sender, EventArgs e)
        {
            string order_id = ((LinkButton)sender).CommandArgument.ToString();
            Response.Redirect("QC_Reject_Product_Add.aspx?action=audit&ID=" + order_id + "");
        }

        //复审  (部门主管领导、邓总)
        protected void Lbtn_FS_OnClick(object sender, EventArgs e)
        {
            string order_id = ((LinkButton)sender).CommandArgument.ToString();
            Response.Redirect("QC_Reject_Product_Add.aspx?action=fushen&ID=" + order_id + "");
        }


        //导出Excel
        protected void btn_ExportExcel_Click(object sender, EventArgs e)
        {
            string sql = "select Order_id,CON_ID,PJ_NAME,TSA_ENGNAME,BJMC,TH,REJECT_TYPE,REJECT_TYPE2,RANK,CZFS,ZRF,Duty_dep,Inform_time,ST_NAME,NUM,STATE,YZ_TIME from View_TBQC_RejectPro_Info_Detail where " + ViewState["sqlText"].ToString();
            DataTable dt_Export = DBCallCommon.GetDTUsingSqlText(sql);
            for (int i = 0; i < dt_Export.Rows.Count; i++)
            {
                DataRow row = dt_Export.Rows[i];

                //不合格类型1
                #region

                switch (row["REJECT_TYPE"].ToString())
                {
                    case "0":
                        row["REJECT_TYPE"] = "采购";
                        break;
                    case "1":
                        row["REJECT_TYPE"] = "外协";
                        break;
                    case "2":
                        row["REJECT_TYPE"] = "场内";
                        break;
                }
                #endregion

                //不合格类型2
                #region

                string[] type = row["Reject_Type2"].ToString().Split('|');
                string rejecttype = "";
                for (int j = 0; j < type.Length; j++)
                {
                    switch (type[j])
                    {
                        case "0":
                            rejecttype += "化学成分" + "/";
                            break;
                        case "1":
                            rejecttype += "物理性能" + "/";
                            break;
                        case "2":
                            rejecttype += "毛坯尺寸" + "/";
                            break;
                        case "3":
                            rejecttype += "钢材尺寸" + "/";
                            break;
                        case "4":
                            rejecttype += "铸造缺陷" + "/";
                            break;
                        case "5":
                            rejecttype += "锻造缺陷" + "/";
                            break;
                        case "6":
                            rejecttype += "制作质量" + "/";
                            break;
                        case "7":
                            rejecttype += "装配质量" + "/";
                            break;
                        case "8":
                            rejecttype += "油漆质量" + "/";
                            break;
                        case "9":
                            rejecttype += "外观质量" + "/";
                            break;
                        case "10":
                            rejecttype += "硬度问题" + "/";
                            break;
                        case "11":
                            rejecttype += "其他" + "/";
                            break;

                    }

                }
                rejecttype = rejecttype.Substring(0, rejecttype.Length - 1);
                row["REJECT_TYPE2"] = rejecttype;
                #endregion

                //判定等级
                #region
                string rank = row["RANK"].ToString();
                string rank_txt = "";
                switch (rank)
                {
                    case "0":
                        rank_txt = "一般不合格"; break;
                    case "1":
                        rank_txt = "严重不合格"; break;

                }
                row["RANK"] = rank_txt;
                #endregion

                //处置方式
                switch (row["CZFS"].ToString())
                {
                    case "1":
                        row["CZFS"] = "让步接收"; break;
                    case "2":
                        row["CZFS"] = "返修"; break;
                    case "3":
                        row["CZFS"] = "报废"; break;

                }

                //是否验证
                #region 是否验证  state
                switch (row["STATE"].ToString())
                {
                    case "5":
                        row["STATE"] = "是"; break;
                    default:
                        row["STATE"] = "否"; break;

                }
                #endregion

            }
            QC_Class.ExportDataItem(dt_Export);

        }


    }
}
