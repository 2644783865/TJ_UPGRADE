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

namespace ZCZJ_DPF.ESM_Data
{
    public partial class EQU_Repair_List : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            delete.Attributes.Add("OnClick", "Javascript:return confirm('是否确定删除?\\r会将整批都删除!\\r若要删除其中一部分则点击修改进入修改页面进行删除！');");
            if (!IsPostBack)
            {
                string sqltext = "select distinct DEP_NAME from TBDS_DEPINFO where DEP_CODE='" + Session["UserDeptID"].ToString() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                dplBM.SelectedValue = dt.Rows[0]["DEP_NAME"].ToString();
                initpageinfo();
                this.ShenHe();
                this.GetSqlText();//获取查询条件
                InitVar();
                this.bindRepeater();
            }
            InitVar();
        }
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
            pager.TableName = "View_EQU_Repair";
            pager.PrimaryKey = "Id";
            pager.ShowFields = "Id AS Id,DocuNum,EquName,EquType,SQtime,Type,Reason,FeedBack,Opinion,SQRNAME,SPZT,USEDEPNAME,DocuPerson,DocuPersonID,REVIEWA,REVIEWANM";
            pager.OrderField = "SQtime";
            pager.StrWhere = ViewState["sqlText"].ToString();
            pager.OrderType = 1;//按时间降序排列
            pager.PageSize = 50;

        }
        void Pager_PageChanged(int pageNumber)
        {
            bindRepeater();
        }
        /// <summary>
        /// 绑定tbpc_otherpurbill_list_Repeater数据
        /// </summary>
        private void bindRepeater()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, EQU_Repair_List_Repeater, UCPaging1, NoDataPanel);
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
        private void GetSqlText()
        {
            StringBuilder sqltext = new StringBuilder();
            sqltext.Append("0=0");
            //审批状态
            if (rbl_shenhe.SelectedValue.ToString() != "5")
            {
                sqltext.Append("and SPZT='" + rbl_shenhe.SelectedValue.ToString() + "' ");              
            }
            if (dplBM.SelectedIndex != 0)
            {
                sqltext.Append("and USEDEPNAME='" + dplBM.SelectedItem.Text + "'");
            }
            //反馈状态
            if (dplBACK.SelectedIndex != 0)
            {
                sqltext.Append("and FeedBack='" + dplBACK.SelectedValue.ToString() + "'");
            }
            ViewState["sqlText"] = sqltext.ToString();
        }
        private void initpageinfo()
        {
            string sqltext = "select distinct DEP_NAME,DEP_CODE from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'order by DEP_CODE";
            DataTable dt_bm = DBCallCommon.GetDTUsingSqlText(sqltext);
            dplBM.DataSource = dt_bm;
            dplBM.DataTextField = "DEP_NAME";
            dplBM.DataValueField = "DEP_NAME";
            dplBM.DataBind();
            dplBM.Items.Insert(0, new ListItem("全部", "%"));
        }
        public string get_feedback(string i)
        {
            string state = "";
            if (i == "0")
            {
                state = "未反馈";
            }
            else if (i == "1")
            {
                state = "已反馈";
            }
            return state;
        }
        public string get_spzt(string i)
        {
            string state = "";
            if (i == "0")
            {
                state = "初始化";
            }
            else if (i == "1")
            {
                state = "提交未审批";
            }
            else if (i == "2")
            {
                state = "审批中";
            }
            else if (i == "3")
            {
                state = "已通过";
            }
            else if (i == "4")
            {
                state = "已驳回";
            }
            return state;
        }
        public string get_type(string i)
        {
            string state = "";
            if (i == "0")
            {
                state = "机械报修";
            }
            if (i == "1")
            {
                state = "电气报修";
            }
            if (i == "2")
            {
                state = "行政报修";
            }
            return state;
        }

        //判断能否删除
        private int candelete()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            int j = 0;//制单是否为登录用户
            int k = 0;//是否已提交审批 
            string postid = "";
            string userid = Session["UserID"].ToString();
            foreach (RepeaterItem Reitem in EQU_Repair_List_Repeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        postid = ((Label)Reitem.FindControl("DocuPersonID")).Text;
                        if (postid != userid)//登录人不是制单人
                        {
                            j++;
                            break;
                        }
                        Label spzt = Reitem.FindControl("SPZT") as Label;
                        if (spzt.Text != "0" && spzt.Text != "4")  //已提交审批
                        {
                            k++;
                            break;
                        }
                    }
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else if (j > 0)//登录人不是制单人
            {
                temp = 2;
            }
            else if (k > 0)//登录人不是制单人
            {
                temp = 3;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }
        protected void EQU_Repair_List_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableCell cellbedit = (HtmlTableCell)e.Item.FindControl("bedit");
                Label zdr = e.Item.FindControl("DocuPerson") as Label;
                Label spzt = e.Item.FindControl("SPZT") as Label;
                HyperLink hyp_edit = e.Item.FindControl("hyp_edit") as HyperLink;
                string pcode = ((Label)e.Item.FindControl("DocuNum")).Text;
                ((Label)e.Item.FindControl("PUR_DD")).ForeColor = System.Drawing.Color.Red;
                ((HyperLink)e.Item.FindControl("HyperLink_lookup")).NavigateUrl = "EQU_Repair_look.aspx?action=view&id=" + Server.UrlEncode(pcode) + "";
                
                    if (cellbedit != null)
                    {
                        cellbedit.Visible = true;
                    }
                    //只有制单人本人，且未提交审核或已驳回时才允许修改
                    if (zdr.Text != Session["UserID"].ToString() || (spzt.Text.ToString() != "0" && spzt.Text.ToString() != "4"))
                    {
                        hyp_edit.Visible = false;
                    }
                    else
                    {
                        ((Label)e.Item.FindControl("Label1")).ForeColor = System.Drawing.Color.Red;
                        ((HyperLink)e.Item.FindControl("hyp_edit")).NavigateUrl = "EQU_Repair_edit.aspx?action=edit&id=" + Server.UrlEncode(pcode) + "";
                    }
              
            }
            if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlTableCell cellhedit = (HtmlTableCell)e.Item.FindControl("hedit");
                    if (cellhedit != null)
                    {
                        cellhedit.Visible = true;
                    }
            }
        }
        protected void delete_Click(object sender, EventArgs e)
        {
            int temp = candelete();
            if (temp == 0)
            {
                foreach (RepeaterItem Retem in EQU_Repair_List_Repeater.Items)
                {
                    CheckBox cbk = Retem.FindControl("CKBOX_SELECT") as CheckBox;
                    if (cbk.Checked)
                    {
                        string code = ((Label)Retem.FindControl("DocuNum")).Text;
                        string sqltext = "delete from EQU_Repair_Need where DocuNum='" + code + "'";
                        string sqltext1 = "delete from EQU_Repair_Audit where DocuNum='" + code + "'";

                        DBCallCommon.ExeSqlText(sqltext);
                        DBCallCommon.ExeSqlText(sqltext1);

                    }
                }
                bindRepeater();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功！');window.location.href=window.location.href;", true);
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您不是制单人，无权删除！');", true);
            }
            else if (temp == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('正在审核中，不能删除！');", true);
            }
        }
        private void ShenHe()
        {
            int a = 0;//初始化
            int b = 0;//未审批
            int c = 0;//审批中
            int d = 0;//已驳回
            string sqltext = "select SPZT from View_EQU_Repair where  USEDEPNAME='" + dplBM.SelectedItem.Text + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["SPZT"].ToString() == "0")
                {
                    a++;
                }
                if (dt.Rows[i]["SPZT"].ToString() == "1")
                {
                    b++;
                }
                if (dt.Rows[i]["SPZT"].ToString() == "2")
                {
                    c++;
                }
                if (dt.Rows[i]["SPZT"].ToString() == "4")
                {
                    d++;
                }
            }
            rbl_shenhe.Items.Clear();
            rbl_shenhe.Items.Add(new ListItem("全部", "5"));
            if (a != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("初始化" + "</label><label><font color=red>(" + a + ")</font>", "0"));
                rbl_shenhe.SelectedIndex = 1;
                btn_search1_click(null, null);

            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("初始化", "0"));
            }
            if (b != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("未审批" + "</label><label><font color=red>(" + b + ")</font>", "1"));
                rbl_shenhe.SelectedIndex = 2;
                btn_search1_click(null, null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("未审批", "1"));
            }
            if (c != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("审批中" + "</label><label><font color=red>(" + c + ")</font>", "2"));
                rbl_shenhe.SelectedIndex = 3;
                btn_search1_click(null, null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("审批中", "2"));
            }
            rbl_shenhe.Items.Add(new ListItem("已通过", "3"));
            if (d != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("已驳回" + "</label><label><font color=red>(" + d + ")</font>", "4"));
                rbl_shenhe.SelectedIndex = 5;
                btn_search1_click(null, null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("已驳回", "4"));
            }
            rbl_shenhe.SelectedIndex = 0;
        }
        protected void dplBACK_Click(object sender, EventArgs e)
        {
            this.ShenHe();
            this.GetSqlText();
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindRepeater();
        }
        protected void QueryButton_Click(object sender, EventArgs e)
        {
            this.ShenHe();
            this.GetSqlText();
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindRepeater();
        }
        protected void btn_search1_click(object sender, EventArgs e)
        {
            this.GetSqlText();
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindRepeater();
        }
    }
}
