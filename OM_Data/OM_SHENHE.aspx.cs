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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_SHENHE : BasicPage
    {
        string sqltext;
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                InitVar();
                GetDataBind();
              
            }
            CheckUser(ControlFinder);
        }
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        //初始化分页信息
        private void InitPager()
        {
            pager.TableName = "View_TBOM_SHENHE";
            pager.PrimaryKey = "ID";
            pager.ShowFields = "ID*1 as ID,TYPE,TYPE_ID,FIRSTMANNM,SECONDMANNM,THIRDMANNM,AUDITLEVEL,CREATTM,CREATER,CREATERNM,STATE";
            pager.OrderField = "CREATTM";
            pager.StrWhere = "STATE='" + rblstate.SelectedValue + "'";
            pager.OrderType = 0;//按任务名称升序排列
            pager.PageSize = 10;
        }
        void Pager_PageChanged(int pageNumber)
        {
            InitVar();
            GetDataBind();
        }
        private void GetDataBind()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel1);
            if (NoDataPanel1.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }
        protected void rblstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblstate.SelectedValue == "1")
            {
                btndelete.Visible = false;
            }
            InitVar();
            GetDataBind();
        }
        protected void btndelete_Click(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            int n = 0;
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox cb = (CheckBox)gr.FindControl("cbchecked");
                if (cb.Checked)
                {
                    n++;
                    Label lblid = (Label)gr.FindControl("lblid");
                    sqltext = "update TBOM_SHENHE set STATE='1' where ID='" + lblid.Text + "'";
                    list_sql.Add(sqltext);
                }
            }
            if (n > 0)
            {
                DBCallCommon.ExecuteTrans(list_sql);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('你成功停用" + n.ToString() + "条工艺类型！')", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请勾选需要停用的工艺类型！')", true);
            }
            InitVar();
            GetDataBind();
        }
    }
}
