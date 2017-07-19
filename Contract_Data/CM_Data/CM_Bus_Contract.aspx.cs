using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ZCZJ_DPF;
using System.Collections.Generic;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_Bus_Contract1 : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                ddlDetails.Items.Clear();
                ListItem item = new ListItem();
                item.Text = "全部";
                item.Value = "00";
                ddlDetails.Items.Insert(0, item);
                ddlDetails.SelectedValue = "00";
                bindGrid();
            }
            CheckUser(ControlFinder);

        }
        private void InitVar()
        {
            
            InitPager();
           
        }
        //初始化分布信息
        private void InitPager()
        {
            pager.TableName = "TBBS_BIDPRICEINFO";
            pager.PrimaryKey = "BP_ID";
            pager.ShowFields = "BP_ID,BP_BIDTYPE,BP_PRONAME,BP_CUSTMID,BP_BSCGCLERK,BP_TCCGCLERK,BP_BIDDATE,BP_STATUS,BP_NOTE";
            pager.OrderField = "BP_ID";
            pager.StrWhere = "";
            pager.OrderType = 1;
            pager.PageSize = 10;
            pager.PageIndex = 1;
        }
      
        private void bindGrid()
        {
            
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            foreach (RepeaterItem e_id in Repeater1.Items)
            {
                Label lbstate = (Label)e_id.FindControl("lbstate");
                int n = Convert.ToInt32(lbstate.Text);
                switch (n)
                {

                    case 0:
                        lbstate.Text = "正在跟踪";
                        break;
                    case 1:
                        lbstate.Text = "中标";
                        break;
                    case 2:
                        lbstate.Text = "项目未定";
                        break;
                    case 3:
                        lbstate.Text = "未中标";
                        break;
                    case 4:
                        lbstate.Text = "未参加投标";
                        break;
                    case 5:
                        lbstate.Text = "正在投标";
                        break;
                    case 6:
                        lbstate.Text = "转公司";
                        break;
                    case 7:
                        lbstate.Text = "取消报价";
                        break;
                }
            }
        }
        protected void ddlClassify_SelectedIndexChanged(object sender, EventArgs e)
        {
            string a = ddlClassify.SelectedValue.Trim();


            if (ddlClassify.SelectedItem.Text.Trim() != "全部" && ddlClassify.SelectedItem.Text.Trim() != "目前状态")
            {
                string sqlText = "select  distinct " + a + "  from TBBS_BIDPRICEINFO";
                //Response.Write(sqlText);
                //Response.End();
                DBCallCommon.BindDdl(ddlDetails, sqlText, ddlClassify.SelectedValue.Trim(), ddlClassify.SelectedValue.Trim());
            
            }
            else if (ddlClassify.SelectedItem.Text.Trim() == "目前状态")
            {
                ddlDetails.Items.Clear();
                ddlDetails.Items.Add("-请选择-");
                ddlDetails.Items.Add("未中标");
                ddlDetails.Items.Add("中标");
                ddlDetails.Items.Add("项目未定");
                ddlDetails.Items.Add("正在跟踪");
                ddlDetails.Items.Add("未参加投标");
                ddlDetails.Items.Add("正在投标");
                ddlDetails.Items.Add("转公司");
                ddlDetails.Items.Add("取消报价");
            }
            
            else
            {
                ddlDetails.Items.Clear();
                ListItem item = new ListItem();
                item.Text = "全部";
                item.Value = "00";
                ddlDetails.Items.Insert(0, item);
                ddlDetails.SelectedValue = "00";
                bindGrid();
            }
        }
        private void RebindGrid()
        {
            InitPager();
            pager.StrWhere = CreateConStr();
            bindGrid();
        }
        private string CreateConStr()
        {
            string strWhere = "";
            if (ddlClassify.SelectedItem.Text.Trim() == "目前状态")
            {
                
                string str = this.ddlDetails.SelectedItem.Value;
                string tree = "";
                switch (str)
                {

                    case "未中标":
                        tree = "0";
                        break;
                    case "中标":
                        tree = "1";
                        break;
                    case "项目未定":
                        tree = "2";
                        break;
                    case "正在跟踪":
                        tree = "3";
                        break;
                    case "未参加投标":
                        tree = "4";
                        break;
                    case "正在投标":
                        tree = "5";
                        break;
                    case "转公司":
                        tree = "6";
                        break;
                    case "取消报价":
                        tree = "7";
                        break;
                }
                //Response.Write(tree);
                //Response.End();
                strWhere = ddlClassify.SelectedValue.Trim() + " like '%" + tree + "%'";
            }
            else if (ddlDetails.SelectedItem.Text.Trim() != "全部" && ddlClassify.SelectedItem.Text.Trim() != "目前状态")
            {
                strWhere = ddlClassify.SelectedValue.Trim() + " like '%" + ddlDetails.Text.Trim() + "%'";
            }
            return strWhere;
        }

        protected void ddlDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            RebindGrid();
        }
        protected void delete_Click(object sender, EventArgs e)
        {
            List<string> bpId = new List<string>();
            string sqlText = "";
            string strID = "";
            //int rowEffected = 0;

            foreach (RepeaterItem labID in Repeater1.Items)
            {
                CheckBox chk = (CheckBox)labID.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    //查找该CheckBox所对应纪录的id号,在labID中
                    strID = ((Label)labID.FindControl("bp_ID")).Text;
                    bpId.Add(strID);
                }
            }
            lbl_Info.Text += strID;
            foreach (string id in bpId)
            {
                sqlText = "DELETE FROM TBBS_BIDPRICEINFO WHERE   BP_ID = '" +id+ "' ";
                DBCallCommon.GetDRUsingSqlText(sqlText);
                //rowEffected++;
            }
            //防止刷新
            Response.Redirect("CM_Bus_Contract.aspx?q=1");
        }
    }
}
