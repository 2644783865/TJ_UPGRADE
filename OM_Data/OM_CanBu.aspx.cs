using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using System.Data.OleDb;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CanBu : BasicPage
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserDeptID"].ToString().Trim() != "02" && Session["UserDeptID"].ToString().Trim() != "01")
                {
                    hlAdd.Visible = false;
                    btnDelete.Visible = false;
                    btncreatdata.Visible = false;
                }
                this.BindYearMoth(ddlYear, ddlMonth);
                BindbmData();
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindrpt();

            }
            CheckUser(ControlFinder);
            this.InitVar();

            creatvisible();//生成餐补数据按钮可见性
        }

        private void creatvisible()
        {
            if (Session["UserDeptID"].ToString().Trim() != "02" && Session["UserDeptID"].ToString().Trim() != "01")
            {
                hlAdd.Visible = false;
                btnDelete.Visible = false;
                btncreatdata.Visible = false;
            }
            else
            {
                string sqltext1 = "select * from OM_Canbusp where YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and state='0'";
                System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext1);
                if (dt1.Rows.Count > 0)
                {
                    btncreatdata.Text = "重新生成本月数据";
                }
                else
                {
                    btncreatdata.Text = "生成餐补数据";
                }

                string sqltext2 = "select * from OM_Canbusp where YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and (state='1' or state='2')";
                System.Data.DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext2);
                if (dt2.Rows.Count > 0)
                {
                    btncreatdata.Visible = false;
                }
                else
                {
                    btncreatdata.Visible = true;
                }
            }
        }


        /// <summary>
        /// 绑定年月
        /// </summary>
        /// <param name="dpl_Year"></param>
        /// <param name="dpl_Month"></param>
        private void BindYearMoth(DropDownList ddl_Year, DropDownList ddl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                ddl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            ddl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));

            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                ddl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            ddl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));

        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            ddlYear.ClearSelection();
            foreach (ListItem li in ddlYear.Items)//显示当前年份
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }

            ddlMonth.ClearSelection();
            string month = (DateTime.Now.Month - 1).ToString();
            if (DateTime.Now.Month < 10 || DateTime.Now.Month == 10)//显示当前月份
            {
                month = "0" + month;
            }
            foreach (ListItem li in ddlMonth.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
        }
        //绑定基本信息
        private void BindbmData()
        {

            string stId = Session["UserId"].ToString();
            System.Data.DataTable dt = DBCallCommon.GetPermeision(6, stId);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
        }

        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数

        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        /// <param name="where"></param>
        private void InitPager()
        {
            string lastyear = "";
            string lastmonth = "";
            try
            {
                if (CommonFun.ComTryInt(ddlMonth.SelectedValue.ToString().Trim()) == 1)
                {
                    lastyear = (CommonFun.ComTryInt(ddlYear.SelectedValue.ToString().Trim()) - 1).ToString().Trim();
                    lastmonth = "12";
                }
                else
                {
                    lastyear = ddlYear.SelectedValue.ToString().Trim();
                    lastmonth = (CommonFun.ComTryInt(ddlMonth.SelectedValue.ToString().Trim()) - 1).ToString("00").Trim();
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('获取年月份出错！');", true);
                return;
            }
            pager_org.TableName = "(select *,zzZT=case when ST_ZHENG between ('" + lastyear + "-" + lastmonth + "-21') and ('" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "-20') then '1' else '0' end from OM_Canbusp left join OM_CanBu on OM_Canbusp.bh=OM_CanBu.detailbh left join OM_KQTJ on (OM_CanBu.CB_STID=OM_KQTJ.KQ_ST_ID and OM_CanBu.CB_YearMonth=OM_KQTJ.KQ_DATE) left join TBDS_STAFFINFO on OM_CanBu.CB_STID=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t";
            pager_org.PrimaryKey = "CB_ID";
            pager_org.ShowFields = "*,(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)) as CB_MonthCB,(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)+CB_BuShangYue) as CB_HeJi ";
            pager_org.OrderField = "DEP_NAME,CB_YearMonth";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 230;
        }
        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string sql = "1=1 and detailbh in (select bh from OM_Canbusp where state!='3')";
            if (ddlYear.SelectedIndex != 0 && ddlMonth.SelectedIndex != 0)
            {
                sql += " and CB_YearMonth='" + ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue + "'";
            }
            else if (ddlYear.SelectedIndex != 0 && ddlMonth.SelectedIndex == 0)
            {
                sql += " and CB_YearMonth like '" + ddlYear.SelectedValue + "%'";
            }
            else if (ddlYear.SelectedIndex == 0 && ddlMonth.SelectedIndex != 0)
            {
                sql += " and CB_YearMonth like '%" + ddlMonth.SelectedValue + "'";
            }
            if (txtName.Text != "")
            {
                sql += " and ST_NAME like '%" + txtName.Text.Trim() + "%'";
            }
            if (ddl_Depart.SelectedValue != "00")
            {
                sql += " and ST_DEPID='" + ddl_Depart.SelectedValue + "'";
            }
            if (tbcbbz.Text.Trim() != "")
            {
                sql += " and CB_BIAOZ=" + CommonFun.ComTryDecimal(tbcbbz.Text.Trim()) + "";
            }
            return sql;
        }

        /// <summary>
        /// 换页事件
        /// </summary>
        private void Pager_PageChanged(int pageNumber)
        {
            bindrpt();
        }

        private void bindrpt()
        {
            InitPager();
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptCanBu, UCPaging1, palNodata);
            if (palNodata.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        /// <summary>
        /// 年份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        /// <summary>
        /// 月份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMonth_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }


        protected void dplbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            List<string> sqltext = new List<string>();
            string sql00 = "select * from OM_Canbusp where YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and (state='2' or state='1')";
            System.Data.DataTable dt00 = DBCallCommon.GetDTUsingSqlText(sql00);
            if (dt00.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月数据已提交审核,不能删除！');", true);
                return;
            }
            sqltext.Clear();
            foreach (RepeaterItem rptitem in rptCanBu.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)rptitem.FindControl("cbxNumber");
                System.Web.UI.WebControls.Label lb = (System.Web.UI.WebControls.Label)rptitem.FindControl("lbCB_ID");
                if (cbx.Checked == true)
                {
                    string sql = "";
                    sql = "delete from OM_CanBu where CB_ID='" + lb.Text + "'";
                    sqltext.Add(sql);
                }
            }
            DBCallCommon.ExecuteTrans(sqltext);
            bindrpt();
        }

        protected void rptCanBu_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqltotal = "select sum(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)) as cbtotal,sum(CB_BuShangYue) as bftotal,sum(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)+CB_BuShangYue) as hjtotal from (select * from OM_Canbusp left join OM_CanBu on OM_Canbusp.bh=OM_CanBu.detailbh left join OM_KQTJ on (OM_CanBu.CB_STID=OM_KQTJ.KQ_ST_ID and OM_CanBu.CB_YearMonth=OM_KQTJ.KQ_DATE) left join TBDS_STAFFINFO on OM_CanBu.CB_STID=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t where " + StrWhere();
                System.Data.DataTable dttotal = DBCallCommon.GetDTUsingSqlText(sqltotal);
                if (dttotal.Rows.Count > 0)
                {
                    System.Web.UI.WebControls.Label lb_ydcb = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_ydcb");
                    System.Web.UI.WebControls.Label lb_bf = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_bf");
                    System.Web.UI.WebControls.Label lb_cbzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_cbzj");


                    lb_ydcb.Text = dttotal.Rows[0]["cbtotal"].ToString().Trim();
                    lb_bf.Text = dttotal.Rows[0]["bftotal"].ToString().Trim();
                    lb_cbzj.Text = dttotal.Rows[0]["hjtotal"].ToString().Trim();
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                Label lbST_NAME = (Label)e.Item.FindControl("lbST_NAME");
                Label lbzzZT = (Label)e.Item.FindControl("lbzzZT");
                HyperLink hlAlter = (HyperLink)e.Item.FindControl("hlAlter");
                if (lbzzZT.Text == "1")
                {
                    lbST_NAME.BackColor = System.Drawing.Color.Yellow;
                }
                if (ddlYear.SelectedIndex != 0 & ddlMonth.SelectedIndex != 0)
                {
                    string sql00 = "select * from OM_Canbusp where YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and (state='2' or state='1')";
                    System.Data.DataTable dt00 = DBCallCommon.GetDTUsingSqlText(sql00);
                    if (dt00.Rows.Count > 0)
                    {
                        hlAlter.Visible = false;
                    }
                    else
                    {
                        hlAlter.Visible = true;
                    }
                }
                else
                {
                    hlAlter.Visible = false;
                }
                //if (ddlYear.SelectedIndex != 0 && ddlMonth.SelectedIndex != 0)
                //{
                //    string lastyear = "";
                //    string lastmonth = "";
                //    try
                //    {
                //        if (CommonFun.ComTryInt(ddlMonth.SelectedValue.ToString().Trim()) == 1)
                //        {
                //            lastyear = (CommonFun.ComTryInt(ddlYear.SelectedValue.ToString().Trim()) - 1).ToString().Trim();
                //            lastmonth = "12";
                //        }
                //        else
                //        {
                //            lastyear = ddlYear.SelectedValue.ToString().Trim();
                //            lastmonth = (CommonFun.ComTryInt(ddlMonth.SelectedValue.ToString().Trim()) - 1).ToString("00").Trim();
                //        }
                //    }
                //    catch
                //    {
                //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('获取年月份出错！');", true);
                //        return;
                //    }
                //    string sql = "select ST_NAME from OM_CanBu left join TBDS_STAFFINFO on OM_CanBu.CB_STID=TBDS_STAFFINFO.ST_ID where ST_ZHENG between ('" + lastyear + "-" + lastmonth + "-21') and ('" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "-20')";
                //    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                //    if (dt.Rows.Count > 0)
                //    {
                //        System.Web.UI.WebControls.Label lbST_NAME = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbST_NAME");
                //        for (int m = 0; m < dt.Rows.Count; m++)
                //        {
                //            if (lbST_NAME.Text == dt.Rows[m]["ST_NAME"].ToString().Trim())
                //                lbST_NAME.BackColor = System.Drawing.Color.Yellow;
                //        }
                //    }
                //}
            }
        }
        //批量修改
        protected void btnplxg_OnClick(object sender, EventArgs e)
        {
            List<string> sql_list = new List<string>();
            string sql00 = "select * from OM_Canbusp where YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and (state='2' or state='1')";
            System.Data.DataTable dt00 = DBCallCommon.GetDTUsingSqlText(sql00);
            if (dt00.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月数据已提交审核,不能修改！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }

            int k = 0;//记录选中的行数
            for (int j = 0; j < rptCanBu.Items.Count; j++)
            {
                if (((System.Web.UI.WebControls.CheckBox)rptCanBu.Items[j].FindControl("cbxNumber")).Checked)
                {
                    k++;
                }
            }
            if (ddlYear.SelectedIndex == 0 || ddlMonth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }
            if (k > 0)
            {
                for (int i = 0; i < rptCanBu.Items.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)rptCanBu.Items[i].FindControl("cbxNumber")).Checked)
                    {
                        string id = ((System.Web.UI.WebControls.Label)rptCanBu.Items[i].FindControl("lbCB_ID")).Text.Trim();
                        string sqltext = "update OM_CanBu set CB_BIAOZ=" + CommonFun.ComTryDecimal(tbcbbzxg.Text.Trim()) + " where CB_ID='" + id + "'";
                        sql_list.Add(sqltext);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要调整的数据！！！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }
            //更新
            DBCallCommon.ExecuteTrans(sql_list);
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        //取消
        protected void btnclose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }


        protected void btncreatdata_OnClick(object sender, EventArgs e)
        {
            List<string> listsqltext = new List<string>();
            string creatbh = DateTime.Now.ToString("yyyyMMddHHmmss").Trim();
            string sql00 = "select * from OM_Canbusp where YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and (state='2' or state='1')";
            System.Data.DataTable dt00 = DBCallCommon.GetDTUsingSqlText(sql00);
            if (dt00.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月数据已提交审核,不能重新生成！');", true);
                return;
            }


            string sqlifexist = "select * from OM_CanBu where CB_YearMonth='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and detailbh in (select bh from OM_Canbusp where YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and (state='0' or state='3'))";
            System.Data.DataTable dtifexist = DBCallCommon.GetDTUsingSqlText(sqlifexist);
            if (dtifexist.Rows.Count > 0)
            {
                string sqldelete2 = "delete from OM_CanBu where CB_YearMonth='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and detailbh in (select bh from OM_Canbusp where YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and (state='0' or state='3'))";
                DBCallCommon.ExeSqlText(sqldelete2);

                string sqldelete1 = "delete from OM_Canbusp where YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and (state='0' or state='3')";
                DBCallCommon.ExeSqlText(sqldelete1);
            }

            //人员信息也取上个月的
            string lastyear = "";
            string lastmonth = "";
            try
            {
                if (CommonFun.ComTryInt(ddlMonth.SelectedValue.ToString().Trim()) == 1)
                {
                    lastyear = (CommonFun.ComTryInt(ddlYear.SelectedValue.ToString().Trim()) - 1).ToString().Trim();
                    lastmonth = "12";
                }
                else
                {
                    lastyear = ddlYear.SelectedValue.ToString().Trim();
                    lastmonth = (CommonFun.ComTryInt(ddlMonth.SelectedValue.ToString().Trim()) - 1).ToString("00").Trim();
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('获取年月份出错！');", true);
                return;
            }

            string sqllast = "select * from OM_CanBu where CB_YearMonth='" + lastyear + "-" + lastmonth + "' and detailbh in (select bh from OM_Canbusp where YEARMONTH='" + lastyear + "-" + lastmonth + "' and state='2') and CB_STID in(select KQ_ST_ID from OM_KQTJ where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "')";
            System.Data.DataTable dtlast = DBCallCommon.GetDTUsingSqlText(sqllast);
            if (dtlast.Rows.Count > 0)
            {
                for (int i = 0; i < dtlast.Rows.Count; i++)
                {
                    string sqlbiaoz = "select CB_BIAOZ from OM_CanBu where CB_STID='" + dtlast.Rows[i]["CB_STID"].ToString().Trim() + "' and CB_YearMonth='" + lastyear + "-" + lastmonth + "'";
                    System.Data.DataTable dtbiaoz = DBCallCommon.GetDTUsingSqlText(sqlbiaoz);
                    if (dtbiaoz.Rows.Count > 0)
                    {
                        string sqlinsert0 = "insert into OM_CanBu(CB_YearMonth,CB_STID,CB_BIAOZ,detailbh) values('" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "','" + dtlast.Rows[i]["CB_STID"].ToString().Trim() + "'," + CommonFun.ComTryDecimal(dtbiaoz.Rows[0]["CB_BIAOZ"].ToString().Trim()) + ",'" + creatbh + "')";
                        listsqltext.Add(sqlinsert0);
                    }
                    else
                    {
                        string sqlinsert1 = "insert into OM_CanBu(CB_YearMonth,CB_STID,detailbh) values('" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "','" + dtlast.Rows[i]["CB_STID"].ToString().Trim() + "','" + creatbh + "')";
                        listsqltext.Add(sqlinsert1);
                    }
                }

                string sqlinsertsp = "insert into OM_Canbusp(bh,SCTIME,YEARMONTH,SCRID,SCRNAME) values('" + creatbh + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "')";
                listsqltext.Add(sqlinsertsp);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有上月已审核的参考数据！');", true);
                return;
            }
            DBCallCommon.ExecuteTrans(listsqltext);
            Response.Redirect("OM_Canbuspdetail.aspx?spid=" + creatbh);
        }


        #region 批量导出

        protected void btn_plexport_OnClick(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext = "select CB_YearMonth,ST_WORKNO,ST_NAME,DEP_NAME,KQ_CHUQIN,KQ_CBTS,CB_TZTS,CB_BIAOZ,(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)) as CB_MonthCB,CB_BuShangYue,(CB_BIAOZ*(isnull(KQ_CBTS,0)+CB_TZTS)+CB_BuShangYue) as CB_HeJi,CB_BeiZhu from (select * from OM_CanBu left join OM_KQTJ on (OM_CanBu.CB_STID=OM_KQTJ.KQ_ST_ID and OM_CanBu.CB_YearMonth=OM_KQTJ.KQ_DATE) left join TBDS_STAFFINFO on OM_CanBu.CB_STID=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t  where " + StrWhere() + " order by DEP_NAME";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string filename = "餐补统计" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("餐补统计.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < 4; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString().Trim());
                    }
                    for (int k = 4; k < dt.Columns.Count - 1; k++)
                    {
                        row.CreateCell(k + 1).SetCellValue(CommonFun.ComTryDouble(dt.Rows[i][k].ToString().Trim()));
                    }
                    row.CreateCell(dt.Columns.Count).SetCellValue(dt.Rows[i][dt.Columns.Count - 1].ToString().Trim());
                }
                for (int i = 0; i <= dt.Columns.Count; i++)
                {
                    sheet0.AutoSizeColumn(i);
                }

                sheet0.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        #endregion
    }
}
