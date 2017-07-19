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

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM1 : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                this.rbl_type_OnSelectedIndexChanged(null, null);
            }
            InitVar();
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
            pager.TableName = "View_TBPM_SCWXRVW";
            pager.PrimaryKey = "PM_DocuNum";
            pager.ShowFields = "";
            pager.OrderField = "PM_SUBMITTM";
            pager.StrWhere = ViewState["sqlText"].ToString();
            pager.OrderType = 1;//按时间升序序排列
            pager.PageSize = 10;
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, tbpc_otherpurbill_list_Repeater, UCPaging1, NoDataPanel);
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

        //存储过程中strWhere参数长度有限制，不能用PM_DocuNum IN (具体值)来传递，要改成PM_DocuNum IN (SELECT 语句)
        protected void rbl_type_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //先找出审核人列表中包含当前登录人的单号，再根据审批状态进行筛选,审批结果2不通过，1通过，0未审核
            
            string userid = Session["UserID"].ToString();
            string sql_dsp = "select PM_DocuNum from TBPM_SCWXAUDIT where" +
                             "  (Fir_Per='" + userid + "' and Fir_Jg='0')" +
                             " or (Sec_Per='" + userid + "' and Sec_Jg='0')" +
                             " or (Thi_Per='" + userid + "' and Thi_Jg='0') ";
            string sql_all = "select PM_DocuNum from TBPM_SCWXAUDIT where" +
                             "  Fir_Per='" + userid + "' or Sec_Per='" + userid + "' or Thi_Per='" + userid + "' ";
            switch (rbl_type.SelectedIndex)
            {
                case 0:
                    ViewState["sqlText"] = " PM_DocuNum in (" + sql_dsp + ") and PM_SPZT in (1,2)";
                    break;
                case 1:
                    ViewState["sqlText"] = " PM_DocuNum in (" + sql_all + ") and PM_SPZT =1";
                    break;
                case 2:
                    ViewState["sqlText"] = " PM_DocuNum in (" + sql_all + ") and PM_SPZT =2";
                    break;
                case 3:
                    ViewState["sqlText"] = " PM_DocuNum in (" + sql_all + ") and PM_SPZT =3";
                    break;
                case 4:
                    ViewState["sqlText"] = " PM_DocuNum in (" + sql_all + ") and PM_SPZT =4";
                    break;
            }

            //单据编号
            if (txtMP_PCODE.Text.Trim() != "")
            {
                ViewState["sqlText"] += " and PM_DocuNum like '%" + txtMP_PCODE.Text.Trim() + "%'";
            }
            //合同名称
            if (tb_pjinfo.Text.Trim() != "")
            {
                ViewState["sqlText"] += " and PJ_NAME like '%" + tb_pjinfo.Text.Trim() + "%'";

            }
            //设备名称
            if (tb_enginfo.Text.Trim() != "")
            {
                ViewState["sqlText"] += " and TSA_ENGNAME like '%" + tb_enginfo.Text.Trim() + "%'";

            }

            string teststr = ViewState["sqlText"].ToString();

            InitVar();
            UCPaging1.CurrentPage = 1;
            bindGrid();

        }

        protected void EQU_Need_Audit_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableCell cellbaudit = (HtmlTableCell)e.Item.FindControl("baudit");
                HtmlTableCell cellblookup = (HtmlTableCell)e.Item.FindControl("blookup");
                string pcode = ((Label)e.Item.FindControl("MP_PCODE")).Text;
                ((Label)e.Item.FindControl("Label1")).ForeColor = System.Drawing.Color.Red;
                ((HyperLink)e.Item.FindControl("hyp_edit")).NavigateUrl = "PM_Xie_Audit.aspx?action=audit&id=" + Server.UrlEncode(pcode) + "";
                ((Label)e.Item.FindControl("PUR_DD")).ForeColor = System.Drawing.Color.Red;
                ((HyperLink)e.Item.FindControl("HyperLink_lookup")).NavigateUrl = "PM_Xie_Audit.aspx?action=view&id=" + Server.UrlEncode(pcode) + "";
                if (rbl_type.SelectedIndex == 0)
                {
                    cellbaudit.Visible = true;
                    cellblookup.Visible = false;

                    //标记当前可审批任务
                    Label mp_code = e.Item.FindControl("MP_PCODE") as Label;
                    if (Check_sp(mp_code.Text))
                    {
                        HtmlTableCell rownum = (HtmlTableCell)e.Item.FindControl("row_num");
                        rownum.BgColor = "#FF0000";
                    }
                }
                else
                {
                    cellbaudit.Visible = false;
                    cellblookup.Visible = true;
                }

            }
            if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlTableCell cellhaudit = (HtmlTableCell)e.Item.FindControl("haudit");
                HtmlTableCell cellhlookup = (HtmlTableCell)e.Item.FindControl("hlookup");
                if (rbl_type.SelectedIndex == 0)
                {
                    cellhaudit.Visible = true;
                    cellhlookup.Visible = false;
                }
                else
                {
                    cellhaudit.Visible = false;
                    cellhlookup.Visible = true;
                }
            }
        }

        private bool Check_sp(string id)
        {
            bool YesOrNO = false;
            string userid = Session["UserID"].ToString();
            string sqltext = "select PM_DocuNum,Fir_Per,Fir_Jg,Sec_Per,Sec_Jg,Thi_Per,Thi_Jg,Rank from" +
                   " TBPM_SCWXAUDIT where PM_DocuNum='" + id + "' ";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                if (userid == dt.Rows[0]["Fir_Per"].ToString())
                {
                    YesOrNO = true;
                }
                else if (userid == dt.Rows[0]["Sec_Per"].ToString()) //第二级审核看一级审核是否同意
                {
                    if (dt.Rows[0]["Fir_Jg"].ToString() == "1")
                    {
                        YesOrNO = true;
                    }
                }
                else if (userid == dt.Rows[0]["Thi_Per"].ToString()) //第三级审核看二级审核是否同意
                {
                    if (dt.Rows[0]["Sec_Jg"].ToString() == "1")
                    {
                        YesOrNO = true;
                    }
                }
            }
            return YesOrNO;
        }
        protected void btn_Reset_Click(object sender, EventArgs e)
        {
            txtMP_PCODE.Text = "";
            tb_pjinfo.Text = "";
            tb_enginfo.Text = "";
            this.rbl_type_OnSelectedIndexChanged(null, null);
        }
    }
}
