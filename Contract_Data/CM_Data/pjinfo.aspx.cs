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
    public partial class pjinfo : System.Web.UI.Page
    {
       
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                bindGrid();
            }
        }

        private void InitVar()
        {
            //btnDelete.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数      
        }

        //初始化分布信息
        private void InitPager()
        {
            pager.TableName = "TBPM_PJINFO";
            pager.PrimaryKey = "PJ_ID";
            pager.ShowFields = "PJ_ID,PJ_NAME,PJ_FILLDATE,PJ_STARTDATE,PJ_CONTRACTDATE,PJ_REALFINISHDATE,PJ_MANCLERK,PJ_STA,PJ_NOTE";
            pager.OrderField = "PJ_FILLDATE";
            pager.StrWhere = "";
            pager.OrderType = 1;//按时间降序排列
            pager.PageSize = 10;
            //pager.PageIndex = 1;
        }

        void Pager_PageChanged(int pageNumber)
        {

            bindGrid();
               
        }

        private void bindGrid()
        {
            //DataTable dt = DBCProcPageing.Projects_Select(UCPaging1.CurrentPage, UCPaging1.PageSize);
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, Reproject1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            foreach (RepeaterItem e_id in Reproject1.Items)
            {
                Label lbstate = (Label)e_id.FindControl("lbstate");
                //int n = Convert.ToInt32(lbstate.Text);
                switch (lbstate.Text)
                {

                    case "0":
                        lbstate.Text = "进行中";
                        break;
                    case "1":
                        lbstate.Text = "完工";
                        break;
                    case "2":
                        lbstate.Text = "停工";
                        break;
                }
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
            string a = dllPJSTATE.SelectedValue.ToString();
            string strWhere = "";
            if (dllPJSTATE.SelectedItem.Text.Trim() != "全部")
            {
                strWhere = "PJ_STA ='" + a + "'";
            }
            else
            {
                strWhere = "";
            }
            return strWhere;
        }

        protected void dllPJSTATE_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            RebindGrid();
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            List<string> PjId = new List<string>();
            //string sqlText = "";
            string strID = "";
            int rowEffected = 0;

            foreach (RepeaterItem labID in Reproject1.Items)
            {
                CheckBox chk = (CheckBox)labID.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    //查找该CheckBox所对应纪录的id号,在labID中
                    strID = ((Label)labID.FindControl("pj_ID")).Text ;
                    PjId.Add(strID);
                }
            }
            lbl_Info.Text += strID;
            foreach (string id in PjId)
            {
                DBCallCommon.DeletePJByPj_ID(id);
                rowEffected++;
            }
            //防止刷新
            Response.Redirect("pjinfo.aspx?q=1");
        }

        
    }
}
