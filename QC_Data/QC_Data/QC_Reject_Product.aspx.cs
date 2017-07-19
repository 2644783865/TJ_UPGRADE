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

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_Reject_Product : System.Web.UI.Page
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
        }

        private void BindDll()
        {
            string sqltext = "select distinct ST_NAME ,BZR from View_TBQC_RejectPro_Info";
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
            pager.PageSize =Convert.ToInt16( ddl_pageno.SelectedValue.ToString());

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

          
            //判定等级
            if (ddl_rank.SelectedIndex != 0)
            {
                sqltext += " and Rank like '%" + ddl_rank.SelectedValue + "%'";
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
                    sqltext += "and CZFS="+ddl_czfs.SelectedValue;
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
                    sqltext += " and STATE<>'5'";
                    break;
            }
            //验证时间
            if (sta_time.Text.Trim() != "" || end_time.Text.Trim() != "")
            {
                sqltext += " and YZ_TIME>='" + sta_time.Text.Trim() + "' and YZ_TIME<='" + end_time.Text.Trim() + "'";
            }

            //审批状态

            if (ddl_state.SelectedIndex != 0)
            {
                switch (ddl_state.SelectedIndex)
                {
                    case 1://审批中                         
                        sqltext += " and STATE='1'or STATE='2' or STATE='3' "; 
                        break;
                    case 2://已审批
                        sqltext += " and STATE ='5'";
                        break;
                    case 3://待处理
                        sqltext += " and ((STATE='1' and PSR_ID='" + Session["UserID"] + "') or (STATE='2' and SPR_ID='" + Session["UserID"] + "') or (STATE='3' and BZR='" + Session["UserID"] + "')) ";
                        break;
                    case 4://已驳回
                        sqltext += " and STATE='4'"; break;
                   
                }
            }

            //项目、工程、部件、零件,单号
            //sqltext += " and (PJ_NAME like '%" + txt_XMMC.Text.Trim() + "%' and TSA_ENGNAME like '%" + txt_GCMC.Text.Trim() + "%'" +
            //" and BJMC like '%" + txt_BJMC.Text.Trim() + "%' and LJMC like '%" + txt_LJMC.Text.Trim() + "%' and Order_id like '%" + tb_orderid.Text.Trim() + "%')";

            //编制人
            if (dpl_zdr.SelectedIndex != 0)
            {
                sqltext += " and BZR = '" + dpl_zdr.SelectedValue.ToString() + "'";
            }

            //责任部门
            if (dpl_ZSBM.SelectedIndex != 0)
            {
                if (dpl_ZSBM.SelectedIndex != 6)
                {
                    sqltext += " and MAIN_DEP LIKE '%" + dpl_ZSBM.SelectedItem.Text + "%'";
                }
                else
                {
                    sqltext += " and (MAIN_DEP IS NULL OR MAIN_DEP='')";
                }
            }

            ViewState["sqlText"] = sqltext;
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindGrid();


            #region 只有点击待审批时才显示审批列，全部和已审批隐藏此列
            if (ddl_state.SelectedIndex == 3)
            {
              GV_RejectPro.Columns[10].Visible = true;
            }
            else
            {
              GV_RejectPro.Columns[10].Visible = false;
            }
          
            #endregion
        }    

        protected void GV_RejectPro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                string orderid = ((Label)e.Row.FindControl("lb_orderid")).Text;
                //Label lbl_id = (Label)e.Row.FindControl("lbl_id");//序号
                //lbl_id.Text = ((this.pager.PageIndex - 1) * this.pager.PageSize + e.Row.RowIndex + 1).ToString();
                //Label lb_bzr = (Label)e.Row.FindControl("lb_bzr");
                //string bzr = lb_bzr.Text.ToString();

            //    #region 一审前面没审的后面的人不可以审
            //    Label lb_orderid = (Label)e.Row.FindControl("lb_orderid");
             //   string orderid = lb_orderid.Text.Trim();
            //    string sqltext = "select * from TBQC_RejectPro_Rev where Rev_id='"+orderid+"' and Per_id='"+Session["UserID"].ToString()+"'";
            //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            //    if (dt.Rows.Count > 0)
            //    {
            //        if (dt.Rows[0]["Per_type"].ToString() != "1")
            //        {
            //            string sql = "select Per_note from TBQC_RejectPro_Rev where Rev_id='" + orderid + "' and Per_type=" +
            //                         " '" +( Convert.ToInt16(dt.Rows[0]["Per_type"].ToString()) - 1).ToString() + "'";
            //            if (!Check_Opinion(sql))
            //            {
            //                e.Row.FindControl("Lbtn_review").Visible = false;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        e.Row.FindControl("Lbtn_review").Visible = false;
            //    }
            //    #endregion
                
            //    #region 复审流程，前面没审的后面的不能审，未提出复审的不可以审
            //    string sqlfs = "select * from TBQC_RejectPro_Rev where Rev_id='" + orderid + "' and Per_id='" + Session["UserID"].ToString() + "' and Per_type>'4'";
            //    DataTable fsdt = DBCallCommon.GetDTUsingSqlText(sqlfs);
            //    if (fsdt.Rows.Count > 0)
            //    {
            //        string sqlfssh = "select Per_note from TBQC_RejectPro_Rev where Rev_id='" + orderid + "' and Per_type=" +
            //                         " '" + (Convert.ToInt16(dt.Rows[0]["Per_type"].ToString()) - 1).ToString() + "'";
            //        if (!Check_Opinion(sqlfssh))
            //        {
            //            e.Row.FindControl("Lbtn_FS").Visible = false;
            //        }
            //    }
            //    else
            //    {
            //        e.Row.FindControl("Lbtn_FS").Visible = false;
            //    }
            //    #endregion

            //    //验证人
            //    string sqlyzr = "select * from TBQC_RejectPro_Rev where Rev_id='" + orderid + "' and Per_id='" + Session["UserID"].ToString() + "' and Per_type='4' and (Per_note='' or Per_note is null)";
            //    DataTable yzr = DBCallCommon.GetDTUsingSqlText(sqlyzr);
            //    if (yzr.Rows.Count > 0)
            //    {
            //        e.Row.FindControl("Lbtn_FS").Visible = true;
            //    }

            //    #region  已经审过的不可以再审
            //    string check = "select Per_note from TBQC_RejectPro_Rev where Rev_id='" + orderid + "' and Per_id='" + Session["UserID"].ToString() + "' and Per_type<'5'";
            //    DataTable dt_check = DBCallCommon.GetDTUsingSqlText(check);
            //    if (dt_check.Rows.Count == 0 || dt_check.Rows[0]["Per_note"].ToString() != "")
            //    {
            //        e.Row.FindControl("Lbtn_review").Visible = false;
            //    }
            //    #endregion

            //    #region  已经【复审通过】的不可以再审
            //    string check2 = "select Per_note from TBQC_RejectPro_Rev where Rev_id='" + orderid + "' and Per_id='" + Session["UserID"].ToString() + "' and Per_type>'3'";
            //    DataTable dt_check2 = DBCallCommon.GetDTUsingSqlText(check2);
            //    if (dt_check2.Rows.Count == 0 || dt_check2.Rows[0]["Per_note"].ToString() != "")
            //    {
            //        e.Row.FindControl("Lbtn_FS").Visible = false;
            //    }
            //    #endregion

            //    #region  lb_ranktext
            //    Label lb_rank = (Label)e.Row.FindControl("lb_rank");
            //    Label lb_ranktext = (Label)e.Row.FindControl("lb_ranktext");

            //    switch (lb_rank.Text)
            //    {
            //        case "0":
            //            lb_ranktext.Text = "轻微不合格"; break;
            //        case "1":
            //            lb_ranktext.Text = "一般不合格"; break;
            //        case "2":
            //            lb_ranktext.Text = "一般质量事故"; break;
            //        case "3":
            //            lb_ranktext.Text = "重大质量事故"; break;
            //    }
            //    #endregion

            //    #region   技术部处置方式
            //    Label lb_jsbresult = (Label)e.Row.FindControl("lb_jsbresult");

            //    string sql_jsbyj = "select Per_result from TBQC_RejectPro_Rev where REV_ID='" + orderid + "' and Per_type=2"+
            //        " and REV_ID in (select REV_ID FROM TBQC_RejectPro_Rev where Per_result IS NOT NULL and  Per_type=3)";
            //    DataTable dt_jsbyj =DBCallCommon.GetDTUsingSqlText(sql_jsbyj);
            //    if (dt_jsbyj.Rows.Count > 0)
            //    {
            //        switch (dt_jsbyj.Rows[0]["Per_result"].ToString())
            //        {
            //            case "1":
            //                lb_jsbresult.Text = "报废"; break;
            //            case "2":
            //                lb_jsbresult.Text = "返修/返工"; break;
            //            case "3":
            //                lb_jsbresult.Text = "降级"; break;
            //            case "4":
            //                lb_jsbresult.Text = "让步"; break;
            //            default:
            //                lb_jsbresult.Text = "未鉴定"; break;
            //        }
            //    }
            //    #endregion

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
            ddl_rank.SelectedIndex = 0;//判定等级
            ddl_czfs.SelectedIndex = 0;//处置方式
            ddl_yz.SelectedIndex = 0;//是否验证
            ddl_state.SelectedIndex = 0;//评审状态
            dpl_zdr.SelectedIndex = 0;//编制人


            tb_conId.Text = "";//合同号
            tb_pjinfo.Text = "";//项目名称
            txt_BJMC.Text=""; //部件名称
          
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
        #endregion

        //1查看，2编辑，3删除，4打印，5修改审批意见
        private void Action_Click(int clicktype)
        {
            //找到选定行的参数
            string orderid = "";
            string zdr = "";
            string state="";
            
            bool check = false; ;
            for (int i = 0; i < GV_RejectPro.Rows.Count; i++)
            {
                CheckBox cbx = (CheckBox)GV_RejectPro.Rows[i].FindControl("CheckBox1");
                Label lb_orderid = (Label)GV_RejectPro.Rows[i].FindControl("lb_orderid");//单号
                Label lb_bzr = (Label)GV_RejectPro.Rows[i].FindControl("lb_bzr");//编制人               
                HtmlInputHidden st = (HtmlInputHidden)GV_RejectPro.Rows[i].FindControl("hidState");//状态
                if (cbx.Checked == true)
                {
                    orderid = lb_orderid.Text.Trim();
                    zdr = lb_bzr.Text.Trim();
                    state = st.Value;
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
                        else if (!(state=="0"||state=="1"))
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
                        else if (!(state=="1"||state=="0"))
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
                        string sqltext = "select SPR_ID,PSR_ID,STATE from View_TBQC_RejectPro_Info_Detail where Order_id='" + orderid + "'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dt.Rows.Count > 0)
                        {
                            string psrID=dt.Rows[0]["PSR_ID"].ToString();
                            string sprID=dt.Rows[0]["SPR_ID"].ToString();
                            string userID=Session["UserID"].ToString();
                             state =dt.Rows[0]["STATE"].ToString();
                            if ((state == "1" && psrID == userID) || (state == "2" && sprID == userID))
                            {
                                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('您还没有审批过这条记录，请直接点击该列后的审核！');", true);
                                return;
                            }
                            else if (state == "3" && psrID == userID)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('您的下一级已经审批通过，无法重审');", true);
                                return;
                            }
                           
                            else
                            {
                                if (state == "4" && psrID == userID)
                                {
                                    List<string> sqlist = new List<string>();
                                    string sql = "update TBQC_RejectPro_Rev set SPR_RESULT='',SPR_NOTE=''";
                                    sqlist.Add(sql);
                                    sql = "update TBQC_RejectPro_Info set STATE='1'";
                                    sqlist.Add(sql);
                                    DBCallCommon.ExecuteTrans(sqlist);

                                }
                                Response.Redirect("QC_Reject_Product_Add.aspx?action=change&ID=" + orderid + "");
                                break;
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('您不在这条记录的审批人列表中！');", true);
                            return;
                        }
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
            //用存储过程读出两个表中的所有信息
            DataTable dt = DBCallCommon.GetDTUsingSqlText("EXEC PRO_QC_Reject");
           
            //将该表中需要的信息进行转换并存入新Datatable中
            DataTable dt_Export = new DataTable();
            dt_Export.Columns.Add(new DataColumn("ORDER_ID", typeof(string)));//单号
            dt_Export.Columns.Add(new DataColumn("MAIN_DEP", typeof(string)));//主送部门
            dt_Export.Columns.Add(new DataColumn("COPY_DEP", typeof(string)));//抄送部门
            dt_Export.Columns.Add(new DataColumn("PJ_NAME", typeof(string)));//项目名称
            dt_Export.Columns.Add(new DataColumn("TSA_ENGNAME", typeof(string)));//工程名称
            dt_Export.Columns.Add(new DataColumn("BJMC", typeof(string)));//部件名称
            dt_Export.Columns.Add(new DataColumn("LJMC", typeof(string)));//零件名称
            dt_Export.Columns.Add(new DataColumn("REJECT_TYPE", typeof(string)));//不合格类型
            dt_Export.Columns.Add(new DataColumn("RANK", typeof(string)));//判定等级
            dt_Export.Columns.Add(new DataColumn("QKMS", typeof(string)));//情况描述
            dt_Export.Columns.Add(new DataColumn("REASON_TXT", typeof(string)));//产生原因
            dt_Export.Columns.Add(new DataColumn("DUTY_PER", typeof(string)));//负责人
            dt_Export.Columns.Add(new DataColumn("DUTY_DEP", typeof(string)));//负责部门

            dt_Export.Columns.Add(new DataColumn("QFR_NOTE", typeof(string)));//签发人意见 处置建议

            dt_Export.Columns.Add(new DataColumn("JSB_RESULT", typeof(string)));//技术部结论（让步、返修……）
            dt_Export.Columns.Add(new DataColumn("JSB_NOTE", typeof(string)));//技术部意见
            dt_Export.Columns.Add(new DataColumn("JSB_PER", typeof(string)));//技术部负责人
            dt_Export.Columns.Add(new DataColumn("JSB_TIME", typeof(string)));//技术部审批时间

            dt_Export.Columns.Add(new DataColumn("PZR_RESULT", typeof(string)));//批准人结论（同意，不同意）
            dt_Export.Columns.Add(new DataColumn("PZR_NOTE", typeof(string)));//批准人意见
            dt_Export.Columns.Add(new DataColumn("PZR_PER", typeof(string)));//批准人
            dt_Export.Columns.Add(new DataColumn("PZR_TIME", typeof(string)));//批准人审批时间

            dt_Export.Columns.Add(new DataColumn("YZR_NOTE", typeof(string)));//验证人意见
            dt_Export.Columns.Add(new DataColumn("YZR_PER", typeof(string)));//验证人
            dt_Export.Columns.Add(new DataColumn("YZR_TIME", typeof(string)));//验证人审批时间

            dt_Export.Columns.Add(new DataColumn("INFORM_DEP", typeof(string)));//通知部门
            dt_Export.Columns.Add(new DataColumn("INFORM_TIME", typeof(string)));//通知时间
            dt_Export.Columns.Add(new DataColumn("BZR", typeof(string)));//编制人
            dt_Export.Columns.Add(new DataColumn("QFR_PER", typeof(string)));//签发人

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt_Export.NewRow();
                row["ORDER_ID"] = dt.Rows[i]["ORDER_ID"].ToString();
                row["MAIN_DEP"] = dt.Rows[i]["MAIN_DEP"].ToString();
                row["COPY_DEP"] = dt.Rows[i]["COPY_DEP"].ToString();
                row["PJ_NAME"] = dt.Rows[i]["PJ_NAME"].ToString();
                row["TSA_ENGNAME"] = dt.Rows[i]["TSA_ENGNAME"].ToString();
                row["BJMC"] = dt.Rows[i]["BJMC"].ToString();
                row["LJMC"] = dt.Rows[i]["LJMC"].ToString();
                row["QKMS"] = dt.Rows[i]["QKMS"].ToString();
                row["REASON_TXT"] = dt.Rows[i]["REASON_TXT"].ToString();
                row["DUTY_PER"] = dt.Rows[i]["DUTY_PER"].ToString();
                row["INFORM_TIME"] = dt.Rows[i]["INFORM_TIME"].ToString();
                row["BZR"] = dt.Rows[i]["ST_NAME"].ToString();

                //不合格类型
                #region

                string[] type = dt.Rows[i]["Reject_Type"].ToString().Split('|');
                string rejecttype = "";
                for (int j = 0; j < type.Length; j++)
                {
                    switch (type[j])
                    {
                        case "0":
                            rejecttype += "原材料" + ",";
                            break;
                        case "1":
                            rejecttype += "下料" + ",";
                            break;
                        case "2":
                            rejecttype += "铸造" + ",";
                            break;
                        case "3":
                            rejecttype += "锻造" + ",";
                            break;
                        case "4":
                            rejecttype += "铆焊" + ",";
                            break;
                        case "5":
                            rejecttype += "机加" + ",";
                            break;
                        case "6":
                            rejecttype += "组装" + ",";
                            break;
                        case "7":
                            rejecttype += "试车" + ",";
                            break;
                        case "8":
                            rejecttype += "防腐" + ",";
                            break;
                        case "9":
                            rejecttype += "包装" + ",";
                            break;
                        case "10":
                            rejecttype += "探伤" + ",";
                            break;

                    }

                }
                rejecttype = rejecttype.Substring(0, rejecttype.Length - 1);
                row["REJECT_TYPE"] = rejecttype;
                #endregion

                //判定等级
                #region
                string rank = dt.Rows[i]["RANK"].ToString();
                string rank_txt = "";
                switch (rank)
                {
                    case "0":
                        rank_txt = "轻微不合格"; break;
                    case "1":
                        rank_txt = "一般不合格"; break;
                    case "2":
                        rank_txt = "一般质量事故"; break;
                    case "3":
                        rank_txt = "重大质量事故"; break;

                }
                row["RANK"] = rank_txt;
                #endregion

                //通知部门
                #region
                string[] inform_dep = dt.Rows[i]["Inform_dep"].ToString().Split('|');
                string informdep_txt = "";
                for (int k = 0; k < inform_dep.Length; k++)
                {
                    string informdep = "select DEP_NAME FROM TBDS_DEPINFO WHERE DEP_CODE='" + inform_dep[k] + "'";
                    DataTable dt_informdep = DBCallCommon.GetDTUsingSqlText(informdep);
                    if (dt_informdep.Rows.Count > 0)
                    {
                        informdep_txt += dt_informdep.Rows[0]["DEP_NAME"].ToString() + ",";
                    }
                }
                informdep_txt = informdep_txt.Substring(0, informdep_txt.Length - 1);
                row["INFORM_DEP"] = informdep_txt;
                #endregion

                //责任部门
                #region
                string[] duty_dep = dt.Rows[i]["Duty_dep"].ToString().Split('|');
                string dutydep_txt = "";
                for (int m = 0; m < duty_dep.Length; m++)
                {
                    string dutydep = "select DEP_NAME FROM TBDS_DEPINFO WHERE DEP_CODE='" + duty_dep[m] + "'";
                    DataTable dt_dutydep = DBCallCommon.GetDTUsingSqlText(dutydep);
                    if (dt_dutydep.Rows.Count > 0)
                    {
                        dutydep_txt += dt_dutydep.Rows[0]["DEP_NAME"].ToString() + ",";
                    }
                }
                if (dutydep_txt != "")
                {
                    dutydep_txt = dutydep_txt.Substring(0, dutydep_txt.Length - 1);
                }
                row["DUTY_DEP"] = dutydep_txt;
                #endregion

                //签发人，签发人意见
                row["QFR_NOTE"] = dt.Rows[i]["Per_note1"].ToString();
                row["QFR_PER"] = dt.Rows[i]["st_name1"].ToString();

                //技术部意见
                row["JSB_NOTE"] = dt.Rows[i]["Per_note2"].ToString();
                row["JSB_PER"] = dt.Rows[i]["st_name2"].ToString();
                row["JSB_TIME"] = dt.Rows[i]["Per_time2"].ToString();
                switch (dt.Rows[i]["Per_result2"].ToString())
                {
                    case "1":
                        row["JSB_RESULT"] = "报废"; break;
                    case "2":
                        row["JSB_RESULT"] = "返修/返工"; break;
                    case "3":
                        row["JSB_RESULT"] = "降级"; break;
                    case "4":
                        row["JSB_RESULT"] = "让步"; break;

                }

                //批准人意见
                row["PZR_NOTE"] = dt.Rows[i]["Per_note3"].ToString();
                row["PZR_PER"] = dt.Rows[i]["st_name3"].ToString();
                row["PZR_TIME"] = dt.Rows[i]["Per_time3"].ToString();
                switch (dt.Rows[i]["Per_result3"].ToString())
                {
                    case "1":
                        row["PZR_RESULT"] = "同意"; break;
                    case "2":
                        row["PZR_RESULT"] = "不同意"; break;

                }
                //验证人意见
                row["YZR_NOTE"] = dt.Rows[i]["Per_note4"].ToString();
                row["YZR_PER"] = dt.Rows[i]["st_name4"].ToString();
                row["YZR_TIME"] = dt.Rows[i]["Per_time4"].ToString();

                dt_Export.Rows.Add(row);
            }
            QC_Class.ExportDataItem(dt_Export);
            
        }
        //项目名称  改变
        protected void tb_pjinfo_Textchanged(object sender, EventArgs e)
        {


            string pjname = "";
            string pjid = "";
            if (tb_pjinfo.Text.ToString().Contains("|"))
            {
                pjid = tb_pjinfo.Text.Substring(0, tb_pjinfo.Text.ToString().IndexOf("|"));
                pjname = tb_pjinfo.Text.Substring(tb_pjinfo.Text.ToString().IndexOf("|") + 1);
                tb_pjinfo.Text = pjname;
                tb_conId.Text = pjid;
            }
            else if (tb_conId.Text.ToString().Contains("|"))
            {
                pjid = tb_conId.Text.Substring(0, tb_conId.Text.ToString().IndexOf("|"));
                pjname = tb_conId.Text.Substring(tb_conId.Text.ToString().IndexOf("|") + 1);
                tb_pjinfo.Text = pjname;
                tb_conId.Text = pjid;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请正确填写项目！');", true);
            }
        }
       
    }
}
