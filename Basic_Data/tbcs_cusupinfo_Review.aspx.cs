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
using System.Collections.Generic;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class tbcs_cusupinfo_Review : BasicPage
    {
        SqlConnection conn = new SqlConnection();
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.rblZT_OnSelectedIndexChanged(null, null);
                // ViewState["sqltext"] = "";
                txtCS_Name.Text = "请输入厂商名称！";
                //this.rblZT_OnSelectedIndexChanged(null, null);
            }
            InitVar();
            //CheckUser(ControlFinder);

        }

        #region "数据分页"
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
            pager.TableName = "TBCS_CUSUP_ADD_DELETE";
            pager.PrimaryKey = "CS_CODE";
            pager.ShowFields = "ID*(1) as ID,CS_CODE,CS_NAME,CS_SPJG,CS_LOCATION," +
                                " case when CS_TYPE='1' then '客户' when CS_TYPE='2' then '采购供应商' when CS_TYPE='3' then '运输公司'" +
                                " when CS_TYPE='4' then '技术外协分包商' when CS_TYPE='5' then '生产外协分包商' " +
                                " when CS_TYPE='6' then '原材料销售供应商'  when CS_TYPE='7' then '其它' end as CS_TYPE," +
                                " CS_COREBS,CS_RANK,CS_MANCLERK,CS_FILLDATE,CS_ACTION,CS_Scope ";
            pager.OrderField = "CS_CODE";
            pager.StrWhere = ViewState["sqlText"].ToString();
            pager.OrderType = 0;//按时间升序序排列
            pager.PageSize = 10;
            //pager.PageIndex = 1;
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, rptTBCS_CUSUPINFO, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;

            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
                //CheckUser(ControlFinder);
            }
        }
        /// <summary>
        /// 按条件绑定数据
        /// </summary>
        /// <param name="condition"></param>

        #endregion

        //不同状态：最近、待评审
        protected void rblZT_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string datetime = DateTime.Now.AddDays(-30).ToShortDateString();
            string cs_name = txtCS_Name.Text.ToString() == "请输入厂商名称！" ? "" : txtCS_Name.Text.ToString();
            switch (rblZT.SelectedValue)
            {
                case "0":  //全部
                    if (dropdownlist1.SelectedValue == "0")
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%'";
                    }
                    else
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and cs_type='" + dropdownlist1.SelectedValue + "'";
                    }
                    break;
                case "1":  //最近一个月审批过的
                    if (dropdownlist1.SelectedValue == "0")
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and id in ( select distinct fatherid from TBCS_CUSUP_ReView where CSR_TIME>='" + datetime + "' and CSR_YJ!='0')";
                    }
                    else
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and id in ( select distinct fatherid from TBCS_CUSUP_ReView where CSR_TIME>='" + datetime + "' and CSR_YJ!='0') and cs_type='" + dropdownlist1.SelectedValue + "'";
                    }
                    break;
                case "2":  //待审批 上一级没审批完的，下级审批将看不到该记录
                    string sqltext1 = "select * from TBCS_CUSUP_ADD_DELETE where (cs_name like '%" + cs_name + "%'  and CS_SPJG in ('0','1') and id in (" +
                        " select distinct a.fatherid from TBCS_CUSUP_ReView a , TBCS_CUSUP_ReView b " +
                                 " where (a.CSR_TYPE!='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "' and a.fatherid=b.fatherid and b.CSR_YJ!='0' and a.CSR_TYPE!='5'" +
                                 "  and cast(a.CSR_TYPE as int)-1=cast(b.CSR_TYPE as int) ) or " +
                                 " (a.CSR_TYPE='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "')" +
                                 ")) or (cs_name like '%" + cs_name + "%' and CS_SPJG in ('0','1') and id in (" +
                                  " select distinct f.fatherid from TBCS_CUSUP_ReView f  " +
                                  " where (f.CSR_TYPE!='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid in ("+
                                  "select h.fatherid from TBCS_CUSUP_ReView h where h.CSR_TYPE='5' )) or" +
                                 " (f.CSR_TYPE='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid not in ("+
                                " (select d.fatherid from TBCS_CUSUP_ReView d " +
                                "  where d.CSR_TYPE!='5' and d.csr_yj='0' and d.fatherid  in( select e.fatherid from  " +
                                 " TBCS_CUSUP_ReView e where e.CSR_TYPE='5' and e.CSR_YJ='0' and e.CSR_PERSON='" + Session["UserID"].ToString() + "'" +                       
                                 "))))))";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext1);
                    if (dt.Rows.Count > 0)
                    {
                        if (dropdownlist1.SelectedValue == "0")
                        {
                            ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG in ('0','1') and id in (" +
                                " select distinct a.fatherid from TBCS_CUSUP_ReView a , TBCS_CUSUP_ReView b " +
                                 " where (a.CSR_TYPE!='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "' and a.fatherid=b.fatherid and b.CSR_YJ!='0' and a.CSR_TYPE!='5'" +
                                 "  and cast(a.CSR_TYPE as int)-1=cast(b.CSR_TYPE as int) ) or " +
                                 " (a.CSR_TYPE='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "')" +
                                 ") or (cs_name like '%" + cs_name + "%' and CS_SPJG in ('0','1') and id in (" +
                                  " select distinct f.fatherid from TBCS_CUSUP_ReView f  " +
                                  " where (f.CSR_TYPE!='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid in (" +
                                  "select h.fatherid from TBCS_CUSUP_ReView h where h.CSR_TYPE='5' and h.CSR_YJ='0' )) or" +
                                 " (f.CSR_TYPE='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid not in (" +
                                " (select d.fatherid from TBCS_CUSUP_ReView d " +
                                "  where d.CSR_TYPE!='5' and d.csr_yj='0' and d.fatherid  in( select e.fatherid from  " +
                                 " TBCS_CUSUP_ReView e where e.CSR_TYPE='5' and e.CSR_YJ='0' and e.CSR_PERSON='" + Session["UserID"].ToString() + "'" +
                                 "))))))";
                        }
                        else
                        {
                            ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG  in ('0','1') and id in (" +
                                 " where (a.CSR_TYPE!='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "' and a.fatherid=b.fatherid and b.CSR_YJ!='0' and a.CSR_TYPE!='5'" +
                                 "  and cast(a.CSR_TYPE as int)-1=cast(b.CSR_TYPE as int) ) or " +
                                 " (a.CSR_TYPE='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "')" +
                                 ") or (cs_name like '%" + cs_name + "%' and CS_SPJG in ('0','1') and id in (" +
                                  " select distinct f.fatherid from TBCS_CUSUP_ReView f  " +
                                  " where (f.CSR_TYPE!='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid in (" +
                                  "select h.fatherid from TBCS_CUSUP_ReView h where h.CSR_TYPE='5' and h.CSR_YJ='0' and h.CSR_PERSON='" + Session["UserID"].ToString() + "' )) or" +
                                 " (f.CSR_TYPE='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid not in (" +
                                " (select d.fatherid from TBCS_CUSUP_ReView d " +
                                "  where d.CSR_TYPE!='5' and d.csr_yj='0' and d.fatherid  in( select e.fatherid from  " +
                                 " TBCS_CUSUP_ReView e where e.CSR_TYPE='5' and e.CSR_YJ='0' and e.CSR_PERSON='" + Session["UserID"].ToString() + "'" +
                                 ")))))"+
                                  " and cs_type='" + dropdownlist1.SelectedValue + "' )";
                        }
                    }
                    else
                    {
                        if (dropdownlist1.SelectedValue == "0")
                        {
                            ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG in ('0','1') and id in (" +
                                     " select distinct fatherid from TBCS_CUSUP_ReView " +
                                     " where CSR_TYPE!='1' and CSR_YJ='0') and CS_MANCLERK='" + Session["UserName"].ToString() + "'";
                        }
                        else
                        {
                            ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG in ('0','1') and id in (" +
                                     " select distinct fatherid from TBCS_CUSUP_ReView " +
                                     " where CSR_TYPE!='1' and CSR_YJ='0') and CS_MANCLERK='" + Session["UserName"].ToString() + "'" +
                                     " and cs_type='" + dropdownlist1.SelectedValue + "'";
                        }
                    }
                    break;
                case "3":
                    if (dropdownlist1.SelectedValue == "0")
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG in('0','1') ";
                    }
                    else
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG in('0','1') and cs_type='" + dropdownlist1.SelectedValue + "'";
                    }
                    break;
                case "4":
                    if (dropdownlist1.SelectedValue == "0")
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG ='3' ";
                    }
                    else
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG ='3' and cs_type='" + dropdownlist1.SelectedValue + "' ";
                    }
                    break;
                case "5":
                    if (dropdownlist1.SelectedValue == "0")
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG ='2' ";
                    }
                    else
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG ='2' and cs_type='" + dropdownlist1.SelectedValue + "' ";
                    }
                    break;

            }
            string sqltext = ViewState["sqlText"].ToString();
            InitVar();
            UCPaging1.CurrentPage = 1;
            bindGrid();
            //CheckUser(ControlFinder);
        }

        protected void Btn_Query_Click(object sender, EventArgs e)
        {
            this.rblZT_OnSelectedIndexChanged(null, null);
        }

        protected void rptTBCS_CUSUPINFO_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            /*等审批时才显示审批,不显示查看
                 其他情况不显示审批列，只显示查看列*/
            HtmlTableCell tdView = e.Item.FindControl("tdView") as HtmlTableCell;

            HtmlTableCell tdReview = e.Item.FindControl("tdReview") as HtmlTableCell;

            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.Header)
            {
                string cs_name = txtCS_Name.Text.ToString() == "请输入厂商名称！" ? "" : txtCS_Name.Text.ToString();
                string sqltext1 = "select * from TBCS_CUSUP_ADD_DELETE where (cs_name like '%" + cs_name + "%'  and  CS_SPJG in ('0','1') and id in (" +
                        " select distinct a.fatherid from TBCS_CUSUP_ReView a , TBCS_CUSUP_ReView b " +
                                 " where (a.CSR_TYPE!='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "' and a.fatherid=b.fatherid and b.CSR_YJ!='0' and a.CSR_TYPE!='5'" +
                                 "  and cast(a.CSR_TYPE as int)-1=cast(b.CSR_TYPE as int) ) or " +
                                 " (a.CSR_TYPE='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "')" +
                                 ")) or (cs_name like '%" + cs_name + "%' and CS_SPJG in ('0','1') and id in (" +
                                  " select distinct f.fatherid from TBCS_CUSUP_ReView f  " +
                                  " where (f.CSR_TYPE!='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid in (" +
                                  "select h.fatherid from TBCS_CUSUP_ReView h where h.CSR_TYPE='5' )) or" +
                                 " (f.CSR_TYPE='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid not in (" +
                                " (select d.fatherid from TBCS_CUSUP_ReView d " +
                                "  where d.CSR_TYPE!='5' and d.csr_yj='0' and d.fatherid  in( select e.fatherid from  " +
                                 " TBCS_CUSUP_ReView e where e.CSR_TYPE='5' and e.CSR_YJ='0' and e.CSR_PERSON='" + Session["UserID"].ToString() + "'" +
                                 "))))))";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext1);
                if (dt.Rows.Count > 0)
                {
                    if (rblZT.SelectedIndex == 2)
                    {
                        tdView.Visible = false;
                        tdReview.Visible = true;
                    }
                    else
                    {
                        tdView.Visible = true;
                        tdReview.Visible = false;
                    }
                }
                else
                {
                    tdView.Visible = true;
                    tdReview.Visible = false;
                }

                if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
                {
                    //只有申请人能编辑和修改,且审批通过的不能改
                    Label lblper = e.Item.FindControl("lbl_app_Per") as Label;
                    Label lblspjg = e.Item.FindControl("lbl_spzt") as Label;
                    HyperLink hlkedit = e.Item.FindControl("Hlk_Edit") as HyperLink;
                    LinkButton lbtndel = e.Item.FindControl("Lbtn_Del") as LinkButton;
                    if (lblper.Text == Session["UserName"].ToString())
                    {
                        if (lblspjg.Text == "审批通过")
                        {
                            hlkedit.Visible = false;
                            lbtndel.Visible = false;
                        }
                    }
                    else
                    {
                        hlkedit.Visible = false;
                        lbtndel.Visible = false;
                    }
                    //审批通过时间，如果审批通过就显示，否则不显示
                    Label lbl_passtime = e.Item.FindControl("lbl_passtime") as Label;
                    Label lbl_id = e.Item.FindControl("lbl_id") as Label;
                    //string sql_passtime = "select CSR_TIME FROM TBCS_CUSUP_ReView WHERE CSR_TYPE=3 AND FATHERID ="+
                    //    " "+Convert.ToInt32(lbl_id.Text.Trim())*(1)+"";
                    string sql_passtime = "select top(1) csr_time,csr_type from TBCS_CUSUP_ReView where fatherid =(select ID from TBCS_CUSUP_ADD_DELETE where id='" + lbl_id.Text.Trim() + "' and CS_SPJG in('2','3')) order by csr_type DESC";
                    DataTable dt_passtime = DBCallCommon.GetDTUsingSqlText(sql_passtime);
                    if (dt_passtime.Rows.Count > 0)
                    {
                        lbl_passtime.Text = dt_passtime.Rows[0]["CSR_TIME"].ToString();
                    }
                }

            }

        }

        //编辑、删除
        protected void lnkAction_OnClick(object sender, EventArgs e)
        {
            List<string> sqlstr = new List<string>();
            int id = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString()) * (1);
            string action = ((LinkButton)sender).CommandName.ToString();
            if (action == "del")
            {
                string del_Info = "delete from TBCS_CUSUP_ADD_DELETE where id=" + id + "";
                string del_Rev = "delete from TBCS_CUSUP_ReView where fatherid=" + id + "";
                sqlstr.Add(del_Info);
                sqlstr.Add(del_Rev);
                DBCallCommon.ExecuteTrans(sqlstr);
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('删除成功！');window.location.href='tbcs_cusupinfo_Review.aspx';", true);
            }
        }
        protected string viewSp(string Spaction, string SpId)
        {
            string SpJb_jg = "";
            string SpJb_cha = "select * from TBCS_CUSUP_ReView where fatherid='" + SpId + "'ORDER BY csr_type DESC";
            DataTable dt1_SpJb = DBCallCommon.GetDTUsingSqlText(SpJb_cha);
            if (dt1_SpJb.Rows.Count>0)
            {
                SpJb_jg=dt1_SpJb.Rows[0]["CSR_TYPE"].ToString();
            }
            if (SpJb_jg == "5")
            {
                return "javascript:void window.open('tbcs_cusup_add_delete.aspx?action=View&cs_action=" + Spaction + "&id=" + SpId + "','','')";
            }
            else
            {
                return "javascript:void window.open('tbcs_cusup_add_delete_old.aspx?action=View&cs_action=" + Spaction + "&id=" + SpId + "','','')";
            }
        }

        protected string reviewSp(string Spaction, string SpId)
        {
            string SpJb_jg = "";
            string SpJb_cha = "select * from TBCS_CUSUP_ReView where fatherid='" + SpId + "'ORDER BY csr_type DESC";
            DataTable dt1_SpJb = DBCallCommon.GetDTUsingSqlText(SpJb_cha);
            if (dt1_SpJb.Rows.Count>0)
            {
                SpJb_jg = dt1_SpJb.Rows[0]["CSR_TYPE"].ToString();
            }
            if (SpJb_jg == "5")
            {
                return "javascript:void window.open('tbcs_cusup_add_delete.aspx?action=Review&cs_action=" + Spaction + "&id=" + SpId + "','','')";
            }
            else
            {
                return "javascript:void window.open('tbcs_cusup_add_delete_old.aspx?action=Review&cs_action=" + Spaction + "&id=" + SpId + "','','')";
            }
        }
        protected string editSp(string Spaction, string SpId)
        {
            string SpJb_jg = "";
            string SpJb_cha = "select * from TBCS_CUSUP_ReView where fatherid='" + SpId + "'ORDER BY csr_type DESC";
            DataTable dt1_SpJb = DBCallCommon.GetDTUsingSqlText(SpJb_cha);
            if (dt1_SpJb.Rows.Count>0)
            {
                SpJb_jg = dt1_SpJb.Rows[0]["CSR_TYPE"].ToString();
            }
            if (SpJb_jg == "5")
            {
                return "javascript:void window.open('tbcs_cusup_add_delete.aspx?action=Edit&cs_action=" + Spaction + "&id=" + SpId + "','','')";
            }
            else
            {
                return "javascript:void window.open('tbcs_cusup_add_delete_old.aspx?action=Edit&cs_action=" + Spaction + "&id=" + SpId + "','','')";
            }
        }

        protected void dropdownlist1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.rblZT_OnSelectedIndexChanged(null, null);
        }
        protected void btnExport_OnClick(object sender, EventArgs e)
        {
            string datetime = DateTime.Now.AddDays(-30).ToShortDateString();
            string cs_name = txtCS_Name.Text.ToString() == "请输入厂商名称！" ? "" : txtCS_Name.Text.ToString();
            switch (rblZT.SelectedIndex)
            {
                case 0:  //全部
                    if (dropdownlist1.SelectedValue == "0")
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%'";
                    }
                    else
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and cs_type='" + dropdownlist1.SelectedValue + "'";
                    }
                    break;
                case 1:  //最近一个月审批过的
                    if (dropdownlist1.SelectedValue == "0")
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and id in ( select distinct fatherid from TBCS_CUSUP_ReView where CSR_TIME>='" + datetime + "' and CSR_YJ!='0'  and CSR_PERSON='" + Session["UserID"].ToString() + "')";
                    }
                    else
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and id in ( select distinct fatherid from TBCS_CUSUP_ReView where CSR_TIME>='" + datetime + "' and CSR_YJ!='0'  and CSR_PERSON='" + Session["UserID"].ToString() + "') and cs_type='" + dropdownlist1.SelectedValue + "'";
                    }
                    break;
                case 2:  //待审批 上一级没审批完的，下级审批将看不到该记录
                    string sqltext1 = "select * from TBCS_CUSUP_ADD_DELETE where (cs_name like '%" + cs_name + "%'   and CS_SPJG in ('0','1') and id in (" +
                        " select distinct a.fatherid from TBCS_CUSUP_ReView a , TBCS_CUSUP_ReView b " +
                                 " where (a.CSR_TYPE!='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "' and a.fatherid=b.fatherid and b.CSR_YJ!='0' and a.CSR_TYPE!='5'" +
                                 "  and cast(a.CSR_TYPE as int)-1=cast(b.CSR_TYPE as int) ) or " +
                                 " (a.CSR_TYPE='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "')" +
                                 ")) or (cs_name like '%" + cs_name + "%' and CS_SPJG in ('0','1') and id in (" +
                                  " select distinct f.fatherid from TBCS_CUSUP_ReView f  " +
                                  " where (f.CSR_TYPE!='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid in (" +
                                  "select h.fatherid from TBCS_CUSUP_ReView h where h.CSR_TYPE='5' )) or" +
                                 " (f.CSR_TYPE='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid not in (" +
                                " (select d.fatherid from TBCS_CUSUP_ReView d " +
                                "  where d.CSR_TYPE!='5' and d.csr_yj='0' and d.fatherid  in( select e.fatherid from  " +
                                 " TBCS_CUSUP_ReView e where e.CSR_TYPE='5' and e.CSR_YJ='0' and e.CSR_PERSON='" + Session["UserID"].ToString() + "'" +
                                 "))))))";
                    DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext1);
                    if (dt1.Rows.Count > 0)
                    {
                        if (dropdownlist1.SelectedValue == "0")
                        {
                            ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG in ('0','1') and id in (" +
                        " select distinct a.fatherid from TBCS_CUSUP_ReView a , TBCS_CUSUP_ReView b " +
                                 " where (a.CSR_TYPE!='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "' and a.fatherid=b.fatherid and b.CSR_YJ!='0' and a.CSR_TYPE!='5'" +
                                 "  and cast(a.CSR_TYPE as int)-1=cast(b.CSR_TYPE as int) ) or " +
                                 " (a.CSR_TYPE='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "')" +
                                 ") or (cs_name like '%" + cs_name + "%' and CS_SPJG in ('0','1') and id in (" +
                                  " select distinct f.fatherid from TBCS_CUSUP_ReView f  " +
                                  " where (f.CSR_TYPE!='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid in (" +
                                  "select h.fatherid from TBCS_CUSUP_ReView h where h.CSR_TYPE='5' )) or" +
                                 " (f.CSR_TYPE='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid not in (" +
                                " (select d.fatherid from TBCS_CUSUP_ReView d " +
                                "  where d.CSR_TYPE!='5' and d.csr_yj='0' and d.fatherid  in( select e.fatherid from  " +
                                 " TBCS_CUSUP_ReView e where e.CSR_TYPE='5' and e.CSR_YJ='0' and e.CSR_PERSON='" + Session["UserID"].ToString() + "'" +
                                 "))))))";
                        }
                        else
                        {
                            ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG in ('0','1') and id in (" +
                                 " where (a.CSR_TYPE!='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "' and a.fatherid=b.fatherid and b.CSR_YJ!='0' and a.CSR_TYPE!='5'" +
                                 "  and cast(a.CSR_TYPE as int)-1=cast(b.CSR_TYPE as int) ) or " +
                                 " (a.CSR_TYPE='1' and a.CSR_YJ='0' and a.CSR_PERSON='" + Session["UserID"].ToString() + "')" +
                                 ") or (cs_name like '%" + cs_name + "%' and CS_SPJG in ('0','1') and id in (" +
                                  " select distinct f.fatherid from TBCS_CUSUP_ReView f  " +
                                  " where (f.CSR_TYPE!='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid in (" +
                                  "select h.fatherid from TBCS_CUSUP_ReView h where h.CSR_TYPE='5' and h.CSR_YJ='0' and h.CSR_PERSON='" + Session["UserID"].ToString() + "' )) or" +
                                 " (f.CSR_TYPE='5' and f.CSR_YJ='0' and f.CSR_PERSON='" + Session["UserID"].ToString() + "' and fatherid not in (" +
                                " (select d.fatherid from TBCS_CUSUP_ReView d " +
                                "  where d.CSR_TYPE!='5' and d.csr_yj='0' and d.fatherid  in( select e.fatherid from  " +
                                 " TBCS_CUSUP_ReView e where e.CSR_TYPE='5' and e.CSR_YJ='0' and e.CSR_PERSON='" + Session["UserID"].ToString() + "'" +
                                 ")))))" +
                                     " and cs_type='" + dropdownlist1.SelectedValue + "' )";
                        }
                    }
                    else
                    {
                        if (dropdownlist1.SelectedValue == "0")
                        {
                            ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG in ('0','1') and id in (" +
                                     " select distinct fatherid from TBCS_CUSUP_ReView " +
                                     " where CSR_TYPE!='1' and CSR_YJ='0') and CS_MANCLERK='" + Session["UserName"].ToString() + "'";
                        }
                        else
                        {
                            ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG in ('0','1') and id in (" +
                                     " select distinct fatherid from TBCS_CUSUP_ReView " +
                                     " where CSR_TYPE!='1' and CSR_YJ='0') and CS_MANCLERK='" + Session["UserName"].ToString() + "'" +
                                     " and cs_type='" + dropdownlist1.SelectedValue + "'";
                        }
                    }
                    break;
                case 3:
                    if (dropdownlist1.SelectedValue == "0")
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG in('0','1') ";
                    }
                    else
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG in('0','1') and cs_type='" + dropdownlist1.SelectedValue + "'";
                    }
                    break;
                case 4:
                    if (dropdownlist1.SelectedValue == "0")
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG ='3' ";
                    }
                    else
                    {
                        ViewState["sqlText"] = " cs_name like '%" + cs_name + "%' and CS_SPJG ='3' and cs_type='" + dropdownlist1.SelectedValue + "' ";
                    }
                    break;
            }
            string sqlwhere = ViewState["sqlText"].ToString();
            string sqltext = "select CS_NAME,CS_ACTION,CSR_TIME from TBCS_CUSUP_ADD_DELETE right join TBCS_CUSUP_ReView on TBCS_CUSUP_ADD_DELETE.id=TBCS_CUSUP_ReView.fatherid where " + sqlwhere + " and csr_type='3' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            TM_Data.ExportTMDataFromDB.ExportCusupinfo_Review(sqltext);
        }

    }
}
