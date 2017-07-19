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


namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_ShipTimeDetail :BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitVar();
                ReGetBoundData();
            }
           
        }
        private void InitVar()
        {   
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize; 
        }
        private void InitPager()
        {   
            string tsa_id =Request.QueryString["id"];
            pager.TableName = "VWPM_PROSTRC";
            pager.PrimaryKey = "BM_ID";
            pager.ShowFields = "";
            pager.OrderField = "BM_XUHAO";
            pager.StrWhere = this.StrWhere();
            pager.OrderType = 0;//按任务名称升序排列
            pager.PageSize = 20;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            string sbname = "";
            double shipweight = 0;
            string sbnamesum = "";
            double shipweightsum = 0;
             for(int i=0;i<GridView1.Rows.Count;i++) 
             {   
                 GridViewRow gr = GridView1.Rows[i];
                 CheckBox chk = (CheckBox)gr.FindControl("CheckBox1");
                 if (chk.Checked)
                 {
                     sbname = ((Label)gr.FindControl("lbchaname")).Text.Trim();
                     shipweight = Convert.ToDouble(((Label)gr.FindControl("lbtotalwght")).Text.Trim());
                     sbnamesum = sbnamesum  + sbname+";";
                     shipweightsum = shipweightsum + shipweight;
                 }
             }
             Response.Write("<script>javascript:window.returnValue ='" +sbnamesum+ "+"+shipweightsum+"';window.close();</script>");
        }

        protected void ddlOrgjishu_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ReGetBoundData();
        }

        protected string StrWhere()
        {
            System.Text.StringBuilder strb = new System.Text.StringBuilder();
            strb.Append(" BM_TASKID='" + Request.QueryString["id"].ToString()+ "'");
            if (ddlOrgJishu.SelectedIndex != 0)
            {
                strb.Append(" AND [dbo].[Splitnum](" + ddlShowType.SelectedValue + ",'.')=" + ddlOrgJishu.SelectedValue + "");
            }

            if (ddlQuery.SelectedIndex != 0)
            {
                if (ddlQuery.SelectedValue == "BM_XUHAO" || ddlQuery.SelectedValue == "BM_ZONGXU")
                {
                    strb.Append(" AND (" + ddlQuery.SelectedValue + "='" + txtContent.Text.Trim() + "' OR " + ddlQuery.SelectedValue + " LIKE '" + txtContent.Text.Trim() + ".%')");
                }
                else if (ddlQuery.SelectedValue == "BM_TUHAO")
                {
                    strb.Append(" AND "+ddlQuery.SelectedValue+" LIKE '"+txtContent.Text.Trim()+"%'");
                }
            }
            return strb.ToString();
        }
    }
}
