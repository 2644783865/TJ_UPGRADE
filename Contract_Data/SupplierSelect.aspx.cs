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

namespace ZCZJ_DPF.Contract_Data
{
    public partial class SupplierSelect : System.Web.UI.Page
    {      
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {

            this.Form.DefaultButton = btnQuery.UniqueID;
            if (!IsPostBack)
            {
                this.GetLocationData();
                this.btnQuery_Click(null, null);
            }
            this.InitVar();
        }

        /// <summary>
        /// 从地区信息表中绑定地区信息
        /// </summary>
        private void GetLocationData()
        {
            string sqltext = "select distinct CL_NAME,CL_CODE from TBCS_LOCINFO where CL_FATHERCODE='ROOT'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            dopLocation.Items.Add(new ListItem("-请选择-", "%"));
            while (dr.Read())
            {
                dopLocation.Items.Add(new ListItem(dr["CL_NAME"].ToString(), dr["CL_CODE"].ToString()));
            }
            dr.Close();
            dopLocation.SelectedIndex = 0;
        }
        /// <summary>
        /// 从地区表中绑定二级地区信息
        /// </summary>
        private void GetLocationNextData()
        {
            dopLocationNext.Items.Clear();
            dopLocationNext.Items.Add(new ListItem("-请选择-", "%"));
            if (dopLocation.SelectedIndex != 0)
            {
                string fathercode = dopLocation.SelectedValue;
                string sqltext = "select distinct CL_NAME from TBCS_LOCINFO where CL_FATHERCODE='" + fathercode + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr.Read())
                {
                    dopLocationNext.Items.Add(new ListItem(dr["CL_NAME"].ToString(), dr["CL_NAME"].ToString()));
                }
                dr.Close();
            }
            dopLocationNext.SelectedIndex = 0;
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
            pager.TableName = "TBCS_CUSUPINFO";
            pager.PrimaryKey = "CS_CODE";
            pager.ShowFields = "CS_CODE,CS_NAME,CS_HRCODE,CS_LOCATION,CS_TYPE";
            pager.OrderField = "CS_CODE";
            pager.StrWhere = ViewState["sqltext"].ToString();
            pager.OrderType = 0;//按时间升序序排列
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
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
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
        /// <summary>
        /// 获取查询条件
        /// </summary>
        private void GetSqlText()
        {
            string sql_txt = "1=1";
            if (dopLocation.SelectedIndex != 0)
            {
             string str = dopLocation.SelectedItem.Text + dopLocationNext.SelectedValue.ToString();
                sql_txt +=" and CS_LOCATION like '" + str + "'";
            }
            sql_txt += " and CS_TYPE LIKE '%"+ddl_cstype.SelectedValue+"%'";
            sql_txt += " and (CS_HRCODE LIKE '%" + txtZJM.Text.Trim() + "%' OR CS_NAME LIKE '%" + txtZJM.Text.Trim() + "%')";
            ViewState["sqltext"] = sql_txt;

        }

        #endregion

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string cs_code = "";
            string cs_name = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                CheckBox chk = (CheckBox)gr.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    cs_code = ((Label)gr.FindControl("lblID")).Text.Trim();
                    cs_name = ((Label)gr.FindControl("lblName")).Text.Trim();
                }
            }
            
            Response.Write("<script>javascript:var a=new Array('" + cs_code + "','" + cs_name + "');window.returnValue=a;window.close();</script>");
            
        }

        protected void dopLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetLocationNextData();
            this.btnQuery_Click(null, null);
        }

       
        
        //助记码查询
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.GetSqlText();           
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }
    }
}
