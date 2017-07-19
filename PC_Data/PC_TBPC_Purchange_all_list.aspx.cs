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
using System.Data.SqlClient;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchange_all_list : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hid_filter.Text = "View_TBPC_MPCHANGETOTAL" + "/" + Session["UserID"].ToString();
                drp_state.SelectedIndex = 1;
                initpagemess();
                getArticle();
            }
            CheckUser(ControlFinder);
        }

        //初始化页面信息,给申请人dropdownlist赋值
        private void initpagemess()
        {
            string sqlText = "select distinct chsubmitnm as ST_NAME,chsubmitid as ST_CODE from View_TBPC_MPCHANGETOTAL";
            string dataText = "ST_NAME";
            string dataValue = "ST_CODE";
            DBCallCommon.BindDdl(drp_st, sqlText, dataText, dataValue);
            drp_st.SelectedIndex = 0;
        }
        protected void rad_quanbu_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
        }
        protected void rad_mytask_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
        }
        protected void btn_search_click(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
        }
        protected void btn_clear_click(object sender, EventArgs e)
        {
            tb_riqit.Text = "";
            tb_riqif.Text = "";
            tb_danju.Text = "";
            drp_st.SelectedIndex = 0;
        }

        private void getArticle()      //取得Article数据
        {
            int cup = Convert.ToInt32(this.lb_CurrentPage.Text);  //当前页数,初始化为地1页
            PagedDataSource ps = new PagedDataSource();
            ps.DataSource = CreateDataSource().DefaultView;
            ps.AllowPaging = true;
            ps.PageSize = 15;     //每页显示的数据的行数
            ps.CurrentPageIndex = cup - 1;
            lb_count.Text = ps.DataSourceCount.ToString(); //获取记录总数
            lb_page.Text = ps.PageCount.ToString(); //获取总页数

            this.DropDownList1.Items.Clear();
            for (int i = 1; i < ps.PageCount + 1; i++)
            {
                this.DropDownList1.Items.Add(i.ToString());
            }
            LinkUp.Enabled = true;
            LinkDown.Enabled = true;

            try
            {
                DropDownList1.SelectedIndex = Convert.ToInt32(cup.ToString()) - 1;
                purch_list_Repeater.DataSource = ps;
                purch_list_Repeater.DataBind();
                if (purch_list_Repeater.Items.Count > 0)
                {
                    NoDataPane.Visible = false;
                }
                else
                {
                    NoDataPane.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable CreateDataSource()
        {
            string sqltext = "";
            string tableuser = hid_filter.Text;
            string filter = "";
            sqltext = "SELECT tableuser, filter FROM TBPC_FILTER_INFO where tableuser='" + tableuser + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                filter = dr[1].ToString();
            }
            dr.Close();
            sqltext = "select  chpcode as MP_CHPCODE, pjid as MP_CHPJID,pjnm as MP_CHPJNAME,engid as MP_CHENGID,"+
                      "engnm as MP_CHENGNAME,chsubmitid as MP_CHSUBMITNMID,chsubmitnm as MP_CHSUBMITNM,"+
                      "chsubmittime as MP_CHSUBMITTM,chreviewaid as MP_CHREVIEWA,chreviewanm as  MP_CHREVIEWANAME, " +
                      "chreviewbid as MP_CHREVIEWB,chreviewbnm as MP_CHREVIEWBNAME,chadate as MP_CHADATE,chstate as MP_CHSTATE,MP_MASHAPE, " +
                      "chnote as MP_CHNOTE from View_TBPC_MPCHANGETOTAL order by chsubmittime desc";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DataView dataView = dt.DefaultView;
            if (filter != "")
            {
                dataView = dt.DefaultView;
                dataView.RowFilter = filter;
                dt = dataView.ToTable();
            }
            if (rad_quanbu.Checked)
            {
                dt = dataView.ToTable();
            }
            else if (rad_mytask.Checked)
            {
                dataView.RowFilter = "MP_CHSTATE='0' OR MP_CHREVIEWB='" + Session["UserID"] .ToString()+ "'"; //对dataView进行筛选 ,未执行
                //dataView.Sort = "MP_CHPCODE ASC";
                dt = dataView.ToTable();
            }
            else
            {
                dt = dataView.ToTable();
            }
            if (drp_state.SelectedValue != "")
            {
                dataView = dt.DefaultView;
                dataView.RowFilter = "MP_CHSTATE='"+drp_state.SelectedValue.ToString()+"'"; //对dataView进行筛选 ,未执行
                //dataView.Sort = "MP_CHPCODE ASC";
                dt = dataView.ToTable();
            }
            if (Tb_PJNAME.Text != "")
            {
                dataView = dt.DefaultView;
                dataView.RowFilter = "MP_CHPJNAME like '%" + Tb_PJNAME.Text.Trim() + "%'"; //对dataView进行筛选 
                //dataView.Sort = "MP_CHPCODE ASC";
                dt = dataView.ToTable();
            }
            if (Tb_ENGNAME.Text != "")
            {
                dataView = dt.DefaultView;
                dataView.RowFilter = "MP_CHENGNAME like '%" + Tb_ENGNAME.Text.Trim() + "%'";//对dataView进行筛选 
                //dataView.Sort = "MP_CHPCODE ASC";
                dt = dataView.ToTable();
            }
            if (tb_riqif.Text != "")
            {
                dataView = dt.DefaultView;
                dataView.RowFilter = "MP_CHSUBMITTM>'" + tb_riqif.Text + "'"; //对dataView进行筛选 
                //dataView.Sort = "MP_CHPCODE ASC";
                dt = dataView.ToTable();
            }
            if (tb_riqit.Text != "")
            {
                dataView = dt.DefaultView;
                dataView.RowFilter = "MP_CHSUBMITTM<'" + tb_riqit.Text + "'"; //对dataView进行筛选 
                //dataView.Sort = "MP_CHPCODE ASC";
                dt = dataView.ToTable();
            }
            if (tb_danju.Text != "")
            {
                dataView = dt.DefaultView;
                dataView.RowFilter = "MP_CHPCODE like '%" + tb_danju.Text.Trim() + "%'"; //对dataView进行筛选 
                //dataView.Sort = "MP_CHPCODE ASC";
                dt = dataView.ToTable();
            }
            if (drp_st.SelectedValue.ToString() != "-请选择-")
            {
                dataView = dt.DefaultView;
                dataView.RowFilter = "MP_CHSUBMITNMID=" + drp_st.SelectedValue.ToString(); //对dataView进行筛选 
                //dataView.Sort = "MP_CHPCODE ASC";
                dt = dataView.ToTable();
            }
            return dt;
        }
        protected void LinkDown_Click(object sender, EventArgs e) //下一页按钮代码
        {
            if (lb_CurrentPage.Text.ToString() != lb_page.Text.ToString())
            {
                lb_CurrentPage.Text = Convert.ToString(Convert.ToInt32(lb_CurrentPage.Text) + 1);
                DropDownList1.SelectedIndex = Convert.ToInt32(lb_CurrentPage.Text) - 1;
                getArticle();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已经是最后一页');", true);
            }

        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e) //跳转到指定页代码
        {
            int page = Convert.ToInt16((DropDownList1.SelectedItem.Value));
            lb_CurrentPage.Text = page.ToString();
            getArticle();
        }
        protected void LinkUp_Click(object sender, EventArgs e)  //上一页按钮代码
        {
            if (Convert.ToInt32(lb_CurrentPage.Text) > 1)
            {
                lb_CurrentPage.Text = Convert.ToString(Convert.ToInt32(lb_CurrentPage.Text) - 1);
                DropDownList1.SelectedIndex = Convert.ToInt32(lb_CurrentPage.Text) - 1;
                getArticle();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已经是第一页');", true);
            }
        }
        protected void LinkFirst_Click(object sender, EventArgs e)  //跳到第一页代码
        {
            if (lb_CurrentPage.Text != "1")
            {
                lb_CurrentPage.Text = "1";
                getArticle();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已经是第一页');", true);
            }
        }
        protected void LinkLast_Click(object sender, EventArgs e)  //跳到最后一页代码
        {
            if (lb_CurrentPage.Text.ToString() != lb_page.Text.ToString())
            {
                lb_CurrentPage.Text = lb_page.Text.ToString();
                getArticle();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已经是最后一页');", true);
            }

        }
     
        protected void drp_state_SelectedIndexChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
        }
       
        protected void purch_list_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
               
                HtmlTableCell cellblookup = (HtmlTableCell)e.Item.FindControl("blookup");//找到内容行中的<td>的ID
                HtmlTableCell cellbedit = (HtmlTableCell)e.Item.FindControl("bedit");
                if (rad_mytask.Checked)//我的任务
                {
                    if (cellbedit != null)
                    {
                        cellbedit.Visible = true;
                        if (((Label)(e.Item.FindControl("MP_STATE"))).Text == "0")
                        {
                            ((HyperLink)(e.Item.FindControl("HyperLink_edit"))).Enabled = true;//状态为未审核时，可用
                        }
                        else
                        {
                            ((HyperLink)(e.Item.FindControl("HyperLink_edit"))).Enabled = false;
                        }
                    }
                }
                else
                {
                    if (cellbedit != null)
                    {
                        cellbedit.Visible = false;
                    }
                }
            }
            if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlTableCell cellhlookup = (HtmlTableCell)e.Item.FindControl("hlookup");//找到Header行中的<td>的ID
                HtmlTableCell cellhedit = (HtmlTableCell)e.Item.FindControl("hedit");
                if (rad_mytask.Checked)
                {
                    if (cellhedit != null)
                    {
                        cellhedit.Visible = true;
                    }
                }
                else
                {
                    if (cellhedit != null)
                    {
                        cellhedit.Visible = false;
                    }
                }
            }
        }
        public string get_ch_state(string i)
        {
            string statestr = "";
            if (i == "0")
            {
                statestr = "未执行";
            }
            else if (i == "1")
            {
                statestr = "进行中";
            }
            else
            {
                statestr = "完毕";
            }
            return statestr;
        }
    }
}
