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
    public partial class TM_Leader_Task_Change : System.Web.UI.Page
    {
        string tablename = "";
        string fields = "";
        string sqlText = "";
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                InitInfo();
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
            ReGetTechAuditData();
        }
        //查询任务类型
        private void GetTaskTypeData()
        {
            #region
            if (rbltask.SelectedValue == "0")
            {
                tablename = "TBPM_MPCHANGERVW";
                fields = "MP_CODE as CODE,MP_PJNAME as PJNAME,MP_ENGNAME as ENGNAME,";
                fields += "MP_SUBMITNM as SUBMITNM,MP_SUBMITTM as SUBMITTM,";
                fields += "MP_REVIEWANAME as REVIEWANAME,MP_REVIEWAADVC as REVIEWAADVC,";
                fields += "MP_REVIEWBNAME as REVIEWBNAME,MP_REVIEWBADVC as REVIEWBADVC,";
                fields += "MP_REVIEWCNAME as REVIEWCNAME,MP_REVIEWCADVC as REVIEWCADVC,";
                fields += "MP_STATE as STATE";
                if (rblstatus.SelectedValue == "0")
                {
                    sqlText = "MP_STATE='2'";
                }
                else if (rblstatus.SelectedValue == "1")
                {
                    sqlText = "MP_STATE in ('4','6')";
                }
                else if (rblstatus.SelectedValue == "2")
                {
                    sqlText = "MP_STATE in ('8','9')";
                }
                else
                {
                    sqlText = "MP_STATE not in ('0','1','2','4','6','8','9')";
                }
                pager.PrimaryKey = "MP_CODE";
                pager.OrderField = "cast(MP_SUBMITTM as datetime)";
                GridView1.Columns[13].Visible = true;
                GridView1.Columns[14].Visible = false;
                GridView1.Columns[15].Visible = false;
            }
            else if (rbltask.SelectedValue == "1")
            {
                tablename = "TBPM_MSCHANGERVW";
                fields = "MS_CODE as CODE,MS_PJNAME as PJNAME,MS_ENGNAME as ENGNAME,";
                fields += "MS_SUBMITNM as SUBMITNM,MS_SUBMITTM as SUBMITTM,";
                fields += "MS_REVIEWANAME as REVIEWANAME,MS_REVIEWAADVC as REVIEWAADVC,";
                fields += "MS_REVIEWBNAME as REVIEWBNAME,MS_REVIEWBADVC as REVIEWBADVC,";
                fields += "MS_REVIEWCNAME as REVIEWCNAME,MS_REVIEWCADVC as REVIEWCADVC,";
                fields += "MS_STATE as STATE";
                if (rblstatus.SelectedValue == "0")
                {
                    sqlText = "MS_STATE='2'";
                }
                else if (rblstatus.SelectedValue == "1")
                {
                    sqlText = "MS_STATE in ('4','6')";
                }
                else if (rblstatus.SelectedValue == "2")
                {
                    sqlText = "MS_STATE in ('8','9')";
                }
                else
                {
                    sqlText = "MS_STATE not in ('0','1','2','4','6','8','9')";
                }
                pager.PrimaryKey = "MS_CODE";
                pager.OrderField = "cast(MS_SUBMITTM as datetime)";
                GridView1.Columns[13].Visible = false;
                GridView1.Columns[14].Visible = true;
                GridView1.Columns[15].Visible = false;
            }
            else
            {
                tablename = "TBPM_OUTSCHANGERVW";
                fields = "OST_CHANGECODE as CODE,OST_PJNAME as PJNAME,OST_ENGNAME as ENGNAME,";
                fields += "OST_SUBMITERNM as SUBMITNM,OST_MDATE as SUBMITTM,";
                fields += "OST_REVIEWERANM as REVIEWANAME,OST_REVIEWAADVC as REVIEWAADVC,";
                fields += "OST_REVIEWERBNM as REVIEWBNAME,OST_REVIEWBADVC as REVIEWBADVC,";
                fields += "OST_REVIEWERCNM as REVIEWCNAME,OST_REVIEWCADVC as REVIEWCADVC,";
                fields += "OST_STATE as STATE";
                if (rblstatus.SelectedValue == "0")
                {
                    sqlText = "OST_STATE='2'";
                }
                else if (rblstatus.SelectedValue == "1")
                {
                    sqlText = "OST_STATE in ('4','6')";
                }
                else if (rblstatus.SelectedValue == "2")
                {
                    sqlText = "OST_STATE in ('8','9')";
                }
                else
                {
                    sqlText = "OST_STATE not in ('0','1','2','4','6','8','9')";
                }
                pager.PrimaryKey = "OST_CHANGECODE";
                pager.OrderField = "cast(OST_MDATE as datetime)";
                GridView1.Columns[13].Visible = false;
                GridView1.Columns[14].Visible = false;
                GridView1.Columns[15].Visible = true;
            }
            #endregion
        }

        //初始化分页信息
        private void InitPager()
        {
            GetTaskTypeData();
            pager.TableName = tablename;
            //pager.PrimaryKey = "ID";
            pager.ShowFields = fields;
            //pager.OrderField = "ID";
            pager.StrWhere = sqlText;
            pager.OrderType = 0;//按任务名称升序排列
            pager.PageSize = 10;
        }

        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            GetTechAuditData();
        }
        
        protected void GetTechAuditData()
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

        protected void ReGetTechAuditData()
        {
            InitPager();
            GetTechAuditData();
        }

        protected void rbltask_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReGetTechAuditData();
        }

        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReGetTechAuditData();
        }
    }
}
