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
using System.Data.SqlClient;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_Task_Persons : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {

            InitVar();

            if (!IsPostBack)
            {
                bindQCData();//绑定质量部的部门信息

                bindGrid();
                ((Image)this.Master.FindControl("Image2")).Visible = false;
                ((Image)this.Master.FindControl("Image3")).Visible = false;
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
            bindGrid();
        }

        //初始化分页信息
        private void InitPager()
        {
            pager.TableName = "TBDS_STAFFINFO";
            pager.PrimaryKey = "ST_ID";
            pager.ShowFields = "";
            pager.OrderField = "ST_ID";
            pager.StrWhere = CreateConStr();
            pager.OrderType = 0;
            pager.PageSize = 8;
        }
        protected void bindGrid()
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

        void bindQCData()
        {
            string sql = "select DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '03__'";//从部门信息表中读取质量部部门名称和部门编号
            DataSet ds = DBCallCommon.FillDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSearch.DataSource = ds;
                ddlSearch.DataTextField = "DEP_NAME";
                ddlSearch.DataValueField = "DEP_CODE";
                ddlSearch.DataBind();
            }
            ddlSearch.Items.Insert(0, new ListItem("全部", "0"));
        }

        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }
        private string CreateConStr()
        {
            string strWhere = "";
            string status = ddlSearch.SelectedValue;
            if (status == "0" || status == "")
            {
                strWhere = "ST_POSITION LIKE '03%' and ST_PD=0";
            }
            else
            {
                strWhere = "ST_POSITION LIKE'" + status + "%' and ST_PD=0";
            }
            return strWhere;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string qcNameId = "";
            string labTcName = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                CheckBox chk = (CheckBox)gr.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    qcNameId = ((Label)gr.FindControl("lblID")).Text.Trim();//绑定员工ID
                    labTcName = ((Label)gr.FindControl("lblName")).Text.Trim();//绑定员工姓名
                }
            }
            //从模式窗口向父窗口传值
            Response.Write("<script>javascript:window.returnValue =new Array('" + qcNameId + "','" + labTcName + "');window.close();</script>");
            //Response.Write("<script language=\"javascript\">window.opener.document.getElementById('<%=labperson.ClientID%>').value ='" + labTcName + "';window.close();</script>"); 
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window.returnValue =new Array('','');window.close();</script>");
            //Response.Redirect("QC_Task_Manage.aspx");
        }

    }
}
