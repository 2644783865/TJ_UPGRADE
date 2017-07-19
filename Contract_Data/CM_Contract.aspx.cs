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
using System.Text;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_Contract : BasicPage
    {
        /**************************************
         * 所接受上一页面的参数为合同类别ContractForm
         *******************************************/
       
        private static string contractForm = ""; //合同类型
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
           contractForm = Request.QueryString["ContractForm"];
           //hlAdd.DataBind();
           
           Page.DataBind();
           
           if (!IsPostBack)
           {
              ViewState["sqlText"] = "PCON_FORM='" + contractForm + "'";//初始查询条件，根据合同类别
              InitVar();
              bindGrid();
              lblContractType.Text = this.GetConForm(contractForm) + "信息";
              lblContractTypeBT.Text =this.GetConForm(contractForm) + "管理";
              this.BindProject();
              this.BindGYS();
              this.BindDep();
              this.BindChargePer();
               //如果是委外合同，可按委外合同类别进行查询，若不是委外合同，则隐藏该条件
              if(contractForm=="1")
              {
                  lblWW.Visible = true;
                  dplWW.Visible = true;
              }
              UpdatePanelCondition.Update();

            }
           this.InitVar();
           CheckUser(ControlFinder);
        }
        private string GetConForm(string bh)
        {
            switch (bh)
            {
                //case "0": return "商务合同"; 
                case "1": return "委外合同"; 
                case "2": return "采购合同"; 
                case "3": return "发运合同"; 
                case "4": return "其他合同"; 
                default: return " ";
            }
        }

        //绑定属性
        public  string ConForm
        {
            get
            {
                return contractForm;
            }
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
            pager.TableName = "TBPM_CONPCHSINFO";
            pager.PrimaryKey = "PCON_BCODE";
            pager.ShowFields = "PCON_BCODE,PCON_PJNAME,PCON_NAME,PCON_YFK,PCON_JINE,PCON_SPJE,PCON_STATE,PCON_JECHG,PCON_QTCHG,PCON_ERROR";
            pager.OrderField = "PCON_BCODE";
            pager.StrWhere = ViewState["sqlText"].ToString();
            pager.OrderType = 0;//按时间升序序排列
            pager.PageSize = 10;
            //pager.PageIndex = 1;
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        /// <summary>
        /// 绑定GRV_CON数据
        /// </summary>
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GRV_CON, UCPaging1, NoDataPanel);
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
            StringBuilder sqltext = new StringBuilder();
            //合同类别
            sqltext.Append(" PCON_FORM='" + contractForm + "'");
            //项目名称
            if (ddlPCON_PJNAME.SelectedIndex != 0)
            {
                sqltext.Append(" and PCON_PJNAME='" + ddlPCON_PJNAME.SelectedItem.Text.ToString() + "'");
            }
            //合同状态
            if (rblstatus.SelectedIndex != 0)
            {
                sqltext.Append(" AND PCON_STATE="+rblstatus.SelectedValue+"");
            }
            //供应商
            if (dplGYS.SelectedIndex != 0)
            {
                sqltext.Append(" and PCON_CUSTMNAME='" + dplGYS.SelectedValue.ToString() + "'");
            }            

            //部门
            if (dplFZBM.SelectedIndex != 0)
            {
                sqltext.Append(" and PCON_RSPDEPID='" + dplFZBM.SelectedValue.ToString() + "'");
            }
            //负责人
            if (dplFZR.SelectedIndex != 0)
            {
                sqltext.Append(" and PCON_RESPONSER='" + dplFZR.SelectedValue.ToString() + "'");
            }
            //如果按委外合同的类别进行查询
            if (dplWW.SelectedIndex != 0)
            {
                sqltext.Append(" AND PCON_BCODE like '%" + dplWW.SelectedValue.ToString() + "%'");
            }
            //合同生效时间
            string startTime = txtStartTime.Text.Trim() == "" ? DateTime.Now.AddYears(-100).ToShortDateString() : txtStartTime.Text.Trim();
            string endTime = txtEndTime.Text.Trim() == "" ? DateTime.Now.AddYears(100).ToShortDateString() : txtEndTime.Text.Trim();
            //合同号
            string hth = txtHTH.Text.Trim();
            sqltext.Append(" and PCON_BCODE like '%" + hth + "%' and PCON_VALIDDATE>='" + startTime + "' AND PCON_VALIDDATE<='" + endTime + "'");
            
            ViewState["sqlText"] = sqltext.ToString();
           
        }

        #endregion

        /// <summary>
        /// 绑定项目名称
        /// </summary>
        private void BindProject()
        {
            string strsql = "select distinct PCON_PJID,PCON_PJNAME from TBPM_CONPCHSINFO WHERE PCON_FORM='"+contractForm+"'";//根据合同类别绑定合同
            DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);
            ddlPCON_PJNAME.DataSource = dt;
            ddlPCON_PJNAME.DataTextField = "PCON_PJNAME";
            ddlPCON_PJNAME.DataValueField = "PCON_PJID";
            ddlPCON_PJNAME.DataBind();
            ddlPCON_PJNAME.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddlPCON_PJNAME.SelectedIndex = 0;

        }
        //供应商
        private void BindGYS()
        {
            string sqltext = "select distinct(PCON_CUSTMNAME) from TBPM_CONPCHSINFO where PCON_FORM="+contractForm+" order by PCON_CUSTMNAME";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            dplGYS.DataSource = dt;
            dplGYS.DataTextField = "PCON_CUSTMNAME";
            dplGYS.DataValueField = "PCON_CUSTMNAME";
            dplGYS.DataBind();
            dplGYS.Items.Insert(0, new ListItem("-请选择-", "%"));
            dplGYS.SelectedIndex=0;
        }
        //责任部门
        private void BindDep()
        {
            string sqltext = "select DEP_NAME,DEP_CODE from  TBDS_DEPINFO where DEP_FATHERID='0'and DEP_CODE!='01'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            dplFZBM.DataSource = dt;
            dplFZBM.DataTextField = "DEP_NAME";
            dplFZBM.DataValueField = "DEP_CODE";
            dplFZBM.DataBind();
            dplFZBM.Items.Insert(0, new ListItem("-请选择-", "%"));
            dplFZBM.SelectedIndex = 0;            
        }
        //负责人
        private void BindChargePer()
        {
            string sqltext = "select distinct(PCON_RESPONSER) from TBPM_CONPCHSINFO WHERE PCON_FORM='" + contractForm + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            dplFZR.DataSource = dt;
            dplFZR.DataTextField = "PCON_RESPONSER";
            dplFZR.DataValueField = "PCON_RESPONSER";
            dplFZR.DataBind();
            dplFZR.Items.Insert(0, new ListItem("-请选择-", "%"));
            dplFZR.SelectedIndex = 0;   
        }

        //重置
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlPCON_PJNAME.SelectedIndex = 0;
            rblstatus.SelectedIndex = 0;
            dplGYS.SelectedIndex = 0;
            dplFZBM.SelectedIndex = 0;
            dplFZR.SelectedIndex = 0;
            dplWW.SelectedIndex = 0;
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            txtHTH.Text = "";
        }

        //查询Button btnQueryHT_OnClick
        protected void btnQueryHT_OnClick(object sender, EventArgs e)
        {
            this.GetSqlText();
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
            ModalPopupExtenderSearch.Hide();
            update_body.Update();
        }
        //取消

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }
    }
}
