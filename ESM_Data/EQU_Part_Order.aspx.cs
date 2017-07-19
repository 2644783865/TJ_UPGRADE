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

namespace ZCZJ_DPF.ESM_Data
{
    public partial class EQU_Part_Order : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                GetBoundData();
            }
        }
        protected void btnPush_OnClick(object sender, EventArgs e)
        {
            int count = 0;
            string code = "";
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    if (count == 0)
                    {
                        code = ((Label)Repeater1.Items[i].FindControl("lblCode")).Text;
                    }
                    else
                    {
                        if (((Label)Repeater1.Items[i].FindControl("lblCode")).Text != code)
                        {
                            string alert = "<script>alert('存在不同的订单，下推被阻止！')</script>";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                            return;//return: 表示语句跳转到函数末尾
                        }
                    }
                    count++;
                }
            }
            if (count < 1)
            {
                string alert = "<script>alert('请选择要下推的条目！')</script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                return;
            }
            else
            {
                Response.Redirect("EQU_Part_Inbill.aspx?FLAG=PUSH&&ID=" + code + "");
            }
        }
        #region 分页
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = "EQU_Need";
            pager.PrimaryKey = "Id";
            pager.ShowFields = "Id*1 as Id,DocuNum,EquName,EquType,EquNum,Reason,SPZT";
            pager.OrderField = "";
            pager.StrWhere = " SPZT='3' and DocuNum like 'beijian%' and DocuNum not in (select distinct Code from EQU_Part_In)";//审批已通过，是设备备件并且没有入过库的订单
            pager.OrderType = 0;
            pager.PageSize = 5;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
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
        }
        private void ReGetBoundData()
        {
            InitPager();
            GetBoundData();
        }
        #endregion
    }
}
