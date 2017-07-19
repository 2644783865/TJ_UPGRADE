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
using ZCZJ_DPF;
using System.Collections.Generic;

namespace ZCZJ_DPF.CM_Data
{
    public partial class enginfo : System.Web.UI.Page
    {
         PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            //ProjiectBind();
            InitVar();
            if (!IsPostBack)
            {
                bindGrid();
                ProjiectBind();
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
            pager.TableName = "TBPM_ENGINFO";
            pager.PrimaryKey = "ENG_ID";
            //在pager.ShowFields中加一个ENG_PJID
            pager.ShowFields = "ENG_ID,ENG_NAME,ENG_PJID,ENG_PJNAME,ENG_FULLNAME,ENG_STARTDATE,ENG_CONTRACTDATE,ENG_REALFINISHDATE,ENG_STATE,ENG_NOTE";
            pager.OrderField = "ENG_STARTDATE";
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

        protected void delete_Click(object sender, EventArgs e)
        {
            List<string> eng_ID = new List<string>();
            string strID = "";
            int rowEffected = 0;

            foreach (RepeaterItem labID in Reproject1.Items)
            {
                CheckBox chk = (CheckBox)labID.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    //查找该CheckBox所对应纪录的id号,在labID中
                    strID = ((Label)labID.FindControl("eng_ID")).Text;
                    eng_ID.Add(strID);
                }
            }
            lbl_Info.Text += strID;
            foreach (string id in eng_ID)
            {
                DBCallCommon.DeleteENGByENG_ID(id);
                rowEffected++;
            }
            //防止刷新
            Response.Redirect("enginfo.aspx?q=1");
        }

        private void RebindGrid()
        {
            InitPager();
            pager.StrWhere = CreateConStr();
            bindGrid();
        }

        private string CreateConStr()
        {
            string a = dllENG_STA.SelectedValue.ToString();
            string strWhere = "";
            if (dllENG_STA.SelectedItem.Text.Trim() != "全部")
            {
                strWhere = "ENG_STATE ='" + a + "'";
            }
            else
            {
                strWhere = "";
            }
            return strWhere;
        }

        protected void dllENG_STA_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            RebindGrid();
        }
        void ProjiectBind()
        {
            string sql = " select PJ_NAME FROM TBPM_PJINFO ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            dllENG_PJNAME.DataSource = dt;
            dllENG_PJNAME.DataTextField = "PJ_NAME";
            dllENG_PJNAME.DataValueField = "PJ_NAME";
            dllENG_PJNAME.DataBind();
            dllENG_PJNAME.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            dllENG_PJNAME.SelectedIndex = 0;
        }

        protected void dllENG_PJNAME_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            RebindGrid1();
        }
        private void RebindGrid1()
        {
            InitPager();
            pager.StrWhere = CreateConStr1();
            bindGrid();
        }

        private string CreateConStr1()
        {
            string a = dllENG_PJNAME.SelectedValue.ToString();
            string strWhere;
            if (dllENG_PJNAME.SelectedItem.Text.Trim() != "-请选择-")
            {
                strWhere = "ENG_PJNAME='" + a + "'";
            }
            else
            {
                strWhere = "";
            }
            return strWhere;

            //string strWhere = "ENG_PJNAME='" + a + "'";
            //return strWhere;
        }

    }
    
}
