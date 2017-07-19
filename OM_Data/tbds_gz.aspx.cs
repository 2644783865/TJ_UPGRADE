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
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    public partial class tbds_gz : System.Web.UI.Page
    {

        string year = "";
        string month = "";

        //记录查询状态（是按日期查询还是生产制号查询）
        bool queryState = true;//日期

        protected void Page_Load(object sender, EventArgs e)
        {
            year = (DateTime.Now.Year).ToString();
            month = (DateTime.Now.Month).ToString();
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                InitVar();
                bindGrid();

            }
            InitVar();
            //CheckUser(ControlFinder);

        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            txtSCZH.Text = "";
            queryState = true;
            //显示当月
            dplYear.ClearSelection();
            foreach (ListItem li in dplYear.Items)
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }

            dplMoth.ClearSelection();
            string month = (DateTime.Now.Month - 1).ToString();
            if (DateTime.Now.Month < 10 || DateTime.Now.Month == 10)
            {
                month = "0" + month;
            }
            foreach (ListItem li in dplMoth.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
        }

        /// <summary>
        /// 绑定年月
        /// </summary>
        private void BindYearMoth(DropDownList dpl_Year, DropDownList dpl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            //dpl_Year.SelectedIndex = 0;
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                dpl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            dpl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            //dpl_Month.SelectedIndex = 0;
        }

        #region
        PagerQueryParam pager_org = new PagerQueryParam();
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager("1=1");
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }


        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager(string where)
        {
            pager_org.TableName = "View_OM_GZManage";
            pager_org.PrimaryKey = "KQ_PersonID";
            pager_org.ShowFields = "*,ROUND(NJGZ,2) as NJGZ1, ROUND(isnull(JBGZ1,0),2) as JBGZ,'" + year + '-' + month + "' as time ";
            pager_org.OrderField = "KQ_PersonID";
            pager_org.StrWhere = where;
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            string sqlwhere = "1=1";
            sqlwhere += " and KQ_DATE='" + dplYear.SelectedValue + "-" + dplMoth.SelectedValue + "' ";
            if (txtSCZH.Text != "")
            {
                sqlwhere += " and (ST_NAME='" + txtSCZH.Text + "' or KQ_PersonID='" + txtSCZH.Text + "') ";
            }

            InitPager(sqlwhere);

            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_org);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, palNoData);
            if (palNoData.Visible)
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rptProNumCost.Items.Count; i++)
            {
                string gh = ((TextBox)rptProNumCost.Items[i].FindControl("TBGH")).Text;//工号

                double zybf = Convert.ToDouble(((TextBox)rptProNumCost.Items[i].FindControl("TB_ZYB")).Text);//中夜班费
                double tzbf = Convert.ToDouble(((TextBox)rptProNumCost.Items[i].FindControl("TB_TZBF")).Text);//调整补发
                double tzbk = Convert.ToDouble(((TextBox)rptProNumCost.Items[i].FindControl("TB_TZBK")).Text);//调整不扣
                double fzsd = Convert.ToDouble(((TextBox)rptProNumCost.Items[i].FindControl("TB_FZ")).Text);//房租/水电费

                string str2 = "update OM_GZ set GZ_ZYB='" + zybf + "',GZ_TZBF='" + zybf + "',GZ_TZBK='" + zybf + "',GZ_FZ='" + fzsd + "' where GZ_PersonID='" + gh + "'";

                //更新
                DBCallCommon.GetDRUsingSqlText(str2);
            }
            Response.Write("<script>javascript:alert('保存成功');</script>");
            
            bindGrid();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void dplMoth_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void cb_cc_CheckedChanged(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void cb_qj_CheckedChanged(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void cb_wx_CheckedChanged(object sender, EventArgs e)
        {
            bindGrid();
        }


        protected void rptProNumCost_ItemDataBound1(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                if (cb_cc.Checked == true)
                {
                    HtmlTableCell bm = e.Item.FindControl("Td1") as HtmlTableCell;
                    HtmlTableCell bz = e.Item.FindControl("Td2") as HtmlTableCell;

                    bm.Visible = false;
                    bz.Visible = false;

                }

                if (cb_qj.Checked == true)
                {
                    HtmlTableCell qcq = e.Item.FindControl("TD3") as HtmlTableCell;
                    HtmlTableCell jr = e.Item.FindControl("TD4") as HtmlTableCell;
                    HtmlTableCell zx = e.Item.FindControl("TD5") as HtmlTableCell;
                    HtmlTableCell ry = e.Item.FindControl("TD6") as HtmlTableCell;
                    HtmlTableCell bj = e.Item.FindControl("TD7") as HtmlTableCell;
                    HtmlTableCell sj = e.Item.FindControl("TD8") as HtmlTableCell;
                    HtmlTableCell nj = e.Item.FindControl("TD9") as HtmlTableCell;


                    qcq.Visible = false;
                    jr.Visible = false;
                    zx.Visible = false;
                    ry.Visible = false;
                    bj.Visible = false;
                    sj.Visible = false;
                    nj.Visible = false;

                }
                if (cb_wx.Checked == true)
                {

                    HtmlTableCell ylbx = e.Item.FindControl("TD10") as HtmlTableCell;
                    HtmlTableCell sybx = e.Item.FindControl("TD11") as HtmlTableCell;
                    HtmlTableCell ynbx = e.Item.FindControl("TD12") as HtmlTableCell;
                    HtmlTableCell dejz = e.Item.FindControl("TD13") as HtmlTableCell;
                    HtmlTableCell bbx = e.Item.FindControl("TD14") as HtmlTableCell;
                    HtmlTableCell gjj = e.Item.FindControl("TD15") as HtmlTableCell;


                    ylbx.Visible = false;
                    sybx.Visible = false;
                    ynbx.Visible = false;
                    dejz.Visible = false;
                    bbx.Visible = false;
                    gjj.Visible = false;
                }

            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (cb_cc.Checked == true)
                {
                    HtmlTableCell bm1 = e.Item.FindControl("Td1_1") as HtmlTableCell;
                    HtmlTableCell bz1 = e.Item.FindControl("Td2_1") as HtmlTableCell;

                    bm1.Visible = false;
                    bz1.Visible = false;

                }

                if (cb_qj.Checked == true)
                {
                    HtmlTableCell qcq1 = e.Item.FindControl("Td3_1") as HtmlTableCell;
                    HtmlTableCell jr1 = e.Item.FindControl("Td3_2") as HtmlTableCell;
                    HtmlTableCell zx1 = e.Item.FindControl("Td3_3") as HtmlTableCell;
                    HtmlTableCell ry1 = e.Item.FindControl("Td3_4") as HtmlTableCell;
                    HtmlTableCell bj1 = e.Item.FindControl("Td3_5") as HtmlTableCell;
                    HtmlTableCell sj1 = e.Item.FindControl("Td3_6") as HtmlTableCell;
                    HtmlTableCell nj1 = e.Item.FindControl("Td3_7") as HtmlTableCell;


                    qcq1.Visible = false;
                    jr1.Visible = false;
                    zx1.Visible = false;
                    ry1.Visible = false;
                    bj1.Visible = false;
                    sj1.Visible = false;
                    nj1.Visible = false;

                }
                if (cb_wx.Checked == true)
                {
                    HtmlTableCell ylbx1 = e.Item.FindControl("TD10_1") as HtmlTableCell;
                    HtmlTableCell sybx1 = e.Item.FindControl("TD11_1") as HtmlTableCell;
                    HtmlTableCell ynbx1 = e.Item.FindControl("TD12_1") as HtmlTableCell;
                    HtmlTableCell dejz1 = e.Item.FindControl("TD13_1") as HtmlTableCell;
                    HtmlTableCell bbx1 = e.Item.FindControl("TD14_1") as HtmlTableCell;
                    HtmlTableCell gjj1 = e.Item.FindControl("TD15_1") as HtmlTableCell;


                    ylbx1.Visible = false;
                    sybx1.Visible = false;
                    ynbx1.Visible = false;
                    dejz1.Visible = false;
                    bbx1.Visible = false;
                    gjj1.Visible = false;
                }
            }
        }
    }

}
