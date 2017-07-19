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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_Notice : System.Web.UI.Page
    {
        string sqlText = "";
        string tablename = "";
        string fields = "";
        string conditions = "";
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                GetProName();
                GetDepartment();
                GetpcdepInfoData();
            }
        }
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        void Pager_PageChanged(int pageNumber)
        {
            ReGetpcdepInfoData();
        }
        //初始化表
        private void InitInfo()
        {
            fields = "a.DCS_ID,a.DCS_PROJECTID,a.DCS_PROJECT,a.DCS_DEPNAME,a.DCS_TYPE,";
            fields += "a.DCS_EDITOR,a.DCS_DATE,b.DCS_STATE,a.DCS_ID+'_'+b.DCS_HDDEPID+'_'+b.DCS_STATE as ID";
            tablename = "TBPM_DEPCONSHEET as a,TBPM_DEPCONSTHNDOUT as b";
            conditions = "b.DCS_HDDEPID='" + Session["UserDeptID"] + "' and a.DCS_ID=b.DCS_CSID and b.DCS_STATE='" + rblstatus.SelectedValue + "'";
        }
        //初始化分页信息
        private void InitPager()
        {
            InitInfo();
            pager.TableName = tablename;
            pager.PrimaryKey = "a.DCS_ID";
            pager.ShowFields = fields;
            pager.OrderField = "a.DCS_DATE";
            pager.StrWhere = conditions;
            pager.OrderType = 0;//按任务名称升序排列
            pager.PageSize = 10;
        }

        protected void GetpcdepInfoData()
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

        //点击查询时重新邦定GridView，添加查询条件
        private void ReGetpcdepInfoData()
        {
            InitPager();
            pager.StrWhere = CreateConStr();
            GetpcdepInfoData();
        }
        private string CreateConStr()
        {
            string strWhere = "";
            if (ddlProName.SelectedItem.Text.Trim() != "-请选择-")
            {
                if (ddldepartment.SelectedItem.Text.Trim() != "-请选择-")
                {
                    strWhere = "b.DCS_HDDEPID='" + Session["UserDeptID"] + "' and a.DCS_ID=b.DCS_CSID and a.DCS_PROJECTID='" + ddlProName.SelectedValue.Trim() + "'";
                    strWhere += " and a.DCS_DEPID='" + ddldepartment.SelectedValue.Trim() + "' ";
                    strWhere += "and b.DCS_STATE='" + rblstatus.SelectedValue + "'";
                }
                else
                {
                    strWhere = "b.DCS_HDDEPID='" + Session["UserDeptID"] + "' and a.DCS_ID=b.DCS_CSID and a.DCS_PROJECTID='" + ddlProName.SelectedValue.Trim() + "'";
                    strWhere += " and b.DCS_STATE='" + rblstatus.SelectedValue + "'";
                }
            }
            else
            {
                if (ddldepartment.SelectedItem.Text.Trim() != "-请选择-")
                {
                    strWhere = "b.DCS_HDDEPID='" + Session["UserDeptID"] + "' and a.DCS_ID=b.DCS_CSID and a.DCS_DEPID='" + ddldepartment.SelectedValue.Trim() + "'";
                    strWhere += " and b.DCS_STATE='" + rblstatus.SelectedValue + "'";
                }
                else
                {
                    strWhere = "b.DCS_HDDEPID='" + Session["UserDeptID"] + "' and a.DCS_ID=b.DCS_CSID and b.DCS_STATE='" + rblstatus.SelectedValue + "'";
                }
            }
            return strWhere;
        }

        //绑定项目名称
        private void GetProName()
        {
            sqlText = "select PJ_ID+'/'+PJ_NAME as PJ_NAME,PJ_ID from TBPM_PJINFO";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlProName.DataSource = dt;
            ddlProName.DataTextField = "PJ_NAME";
            ddlProName.DataValueField = "PJ_ID";
            ddlProName.DataBind();
            ddlProName.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlProName.SelectedIndex = 0;
        }
        //绑定部门名称
        private void GetDepartment()
        {
            sqlText = "select DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_FATHERID='0' and DEP_CODE!='" + Session["UserDeptID"] + "'";
            string dataText = "DEP_NAME";
            string dataValue = "DEP_CODE";
            DBCallCommon.BindDdl(ddldepartment, sqlText, dataText, dataValue);
            ddldepartment.Items.Insert(4, new ListItem("商务组", "0302"));
        }
        protected void ddlProName_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetpcdepInfoData();
        }

        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetpcdepInfoData();
        }

        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetpcdepInfoData();
        }
    }
}
