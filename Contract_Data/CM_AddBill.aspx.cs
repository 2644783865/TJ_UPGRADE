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
using System.Text;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_AddBill : System.Web.UI.Page
    {
        string sqltext = "";
        string condetail_id = "";
        string contractform="";
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Form.DefaultButton = btnQuery.UniqueID;//默认按钮
            if (!IsPostBack)
            {
                this.BindPrjName();
                this.InitVar();
                this.bindGrid();
            }
            this.GetSqlText();
            this.InitVar();

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
            pager.ShowFields = "PCON_BCODE,PCON_FORM,PCON_NAME,PCON_PJNAME,PCON_ENGNAME,PCON_JINE,PCON_YFK";
            pager.OrderField = "PCON_FORM,PCON_BCODE";
            pager.StrWhere = sqltext;
            pager.OrderType = 0;//按时间升序序排列
            pager.PageSize = 5;
            //pager.PageIndex = 1;
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, grvHT, UCPaging1, NoDataPanel);
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
            StringBuilder str = new StringBuilder();
            str.Append(" PCON_BCODE like '%"+txtHTBH.Text.Trim()+"%'");
            if (dplHTLB.SelectedIndex != 0)
            {
                str.Append(" and PCON_FORM=" + dplHTLB.SelectedValue.ToString() + "");
            }
            if (cmbPCON_PJNAME.SelectedIndex != 0)
            {
                str.Append(" and PCON_PJID='"+cmbPCON_PJNAME.SelectedValue.ToString()+"'");
            }
            sqltext = str.ToString();
        }

        #endregion

        /// <summary>
        /// 绑定项目名称
        /// </summary>
        private void BindPrjName()
        {
            string sqlText = "select PJ_ID+'/'+PJ_NAME as PJ_NAME,PJ_ID from TBPM_PJINFO";//随着项目的增多，下拉框数据多，考虑将项目是否完工加入查询条件
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            cmbPCON_PJNAME.DataSource = dt;
            cmbPCON_PJNAME.DataTextField = "PJ_NAME";
            cmbPCON_PJNAME.DataValueField = "PJ_ID";
            cmbPCON_PJNAME.DataBind();
            cmbPCON_PJNAME.Items.Insert(0, new ListItem("-请选择-", "%"));
            cmbPCON_PJNAME.SelectedIndex = 0;
        }

        protected void cmbPCON_PJNAME_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.GetSqlText();
            this.InitVar();
            this.bindGrid();
        }

        protected void dplHTLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.GetSqlText();
            this.InitVar();
            this.bindGrid();
        }


        //合同编号
        public string CondetailID
        {
            get
            {
                return condetail_id;
            }
        }
        //合同类别
        public string ConForm
        {
            get
            {
                return contractform;
            }
        }

        protected void cbxState_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < grvHT.Rows.Count; i++)
            {
                CheckBox cbx = (CheckBox)grvHT.Rows[i].FindControl("cbxState");
                if (cbx.Checked)
                {
                    Label lblhtbh = (Label)grvHT.Rows[i].FindControl("lblHTBH");
                    condetail_id = lblhtbh.Text;
                    Label lblhtlb = (Label)grvHT.Rows[i].FindControl("lblHTLB");
                    contractform = lblhtlb.Text;
                    break;
                }
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.GetSqlText();
            this.InitVar();
            this.bindGrid();
        }
    }
}
