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
using System.Collections.Generic;

namespace ZCZJ_DPF.FM_Data
{
    public partial class CWCB_Hesuan : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RadioButtonList1.SelectedIndex = 0;
                ViewState["StrWhere"] = " CWCB_STATE='" + RadioButtonList1.SelectedValue + "'";
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }
            if (IsPostBack)
            {
                this.InitVar();
            }
            CheckUser(ControlFinder);
        }

        /// <summary>
        ///状态改变查询 
        /// </summary>
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtrwh.Text = "";
            btnHS.Visible = RadioButtonList1.SelectedValue.ToString() == "0" ? true : false;
            btnFHS.Visible = RadioButtonList1.SelectedValue.ToString() == "0" ? false : true;
            ViewState["StrWhere"] = " CWCB_STATE='" + RadioButtonList1.SelectedValue + "'";
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();

        }
        /// <summary>
        /// txtbox查询
        /// </summary>
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            ViewState["StrWhere"] = " TASK_ID like '%" + txtrwh.Text.Trim() + "%' and CWCB_STATE='" + RadioButtonList1.SelectedValue.ToString() + "'";
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();

        }
        //核算
        protected void btnHS_Click(object sender, EventArgs e)
        {
            List<string> list_rwh = new List<string>();
            for (int i = 0; i < rptProNumCost.Items.Count; i++)
            {
                if (((CheckBox)rptProNumCost.Items[i].FindControl("checkbox")).Checked)
                {
                    list_rwh.Add(((Label)rptProNumCost.Items[i].FindControl("lbrwh")).Text);
                }
            }
            if (list_rwh.Count > 0)
            {
                string b = CheckOutAct(list_rwh);
                if (b == "")
                {
                    List<string> sql = new List<string>();
                    for (int k = 0; k < list_rwh.Count; k++)
                    {
                        string hsrwh = list_rwh[k].ToString();
                        string sqlcwhs = update_cwcb(hsrwh);
                        sql.Add(sqlcwhs);
                    }
                    DBCallCommon.ExecuteTrans(sql);
                }
            }
            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('请勾选要核算的任务号！！！')", true);
            }
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }




        private string update_cwcb(string hsrwh)
        {
            string sqlupdate = "";
            sqlupdate = "update TBCB_BMCONFIRM set CWCB_GZ=(select ISNULL(sum(AYTJ_GZ),0)  from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),       CWCB_JJFY=(select ISNULL(sum(AYTJ_JJFY),0) from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),CWCB_JGYZ=(select ISNULL(sum(AYTJ_JGYZFY),0)  from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),CWCB_WGJ=(select ISNULL(sum(PMS_01_11),0)  from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),CWCB_HSJS=(select ISNULL(sum(PMS_01_07),0)  from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),CWCB_HCL=(select ISNULL(sum(PMS_01_05),0)  from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),CWCB_ZJ=(select ISNULL(sum(PMS_01_08),0)  from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),CWCB_DJ=(select ISNULL(sum(PMS_01_09),0)  from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),CWCB_ZC=(select ISNULL(sum(PMS_01_10),0)  from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),CWCB_BZJ=(select ISNULL(sum(PMS_01_01),0)  from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),CWCB_QTL=(select ISNULL(sum(PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_06+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_15+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09),0)  from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),CWCB_GDZZ=(select ISNULL(sum(AYTJ_GDZZFY),0)  from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),CWCB_KBZZ=(select ISNULL(sum(AYTJ_KBZZFY),0)  from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),CWCB_WXFY=(select ISNULL(sum(AYTJ_WXFY),0)  from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),CWCB_CNFB=(select ISNULL(sum(AYTJ_CNFB),0)  from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),CWCB_YF=(select ISNULL(sum(AYTJ_YF),0)  from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),CWCB_FJCB=(select ISNULL(sum(AYTJ_FJCB),0)  from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),CWCB_QT=(select ISNULL(sum(AYTJ_QT),0)  from View_FM_AYTJ where PMS_TSAID='" + hsrwh + "'),CWCB_STATE='1',CWCB_HSDATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where TASK_ID='" + hsrwh + "'";
            return sqlupdate;
        }
        ////////////////////////////////////////////////////////////////////
        private string CheckOutAct(List<string> list_rwh)
        {
            string a = "";
            for (int j = 0; j < list_rwh.Count; j++)
            {
                string rwh = list_rwh[j].ToString();
                string sqltext = "select * from View_TBWS_OUTPRODTOTAL_OUTPRODDETAIL where OP_HSFLAG='0' and OP_TSAID like '%" + rwh + "%'";
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt1.Rows.Count > 0)
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('任务号：" + rwh + "下存在未出库核算的明细，该任务号不能核算！')", true);
                    a = "1";
                }
                else if (dt1.Rows.Count == 0)
                {
                    string sqlifchuku = "select * from View_TBWS_OUTPRODTOTAL_OUTPRODDETAIL where OP_HSFLAG='1' and OP_TSAID like '%" + rwh + "%'";
                    DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqlifchuku);
                    if (dt2.Rows.Count == 0)
                    {
                        this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('任务号：" + rwh + "未进行过出库核算！')", true);
                        a = "1";
                    }
                }

                string sqlChuKu = "select PTC from View_SM_Storage where PTC like '%" + rwh + "%'";
                DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sqlChuKu);
                if (dt3.Rows.Count>0)
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('任务号：" + rwh + "下存在未出库的明细，该任务号不能核算！')", true);
                    a = "1";
                }
            }
            return a;
        }
        /////////////////////////////////////////////////////////////////////////







        //反核算
        protected void btnFHS_Click(object sender, EventArgs e)
        {
            List<string> list_rwh = new List<string>();
            for (int i = 0; i < rptProNumCost.Items.Count; i++)
            {
                if (((CheckBox)rptProNumCost.Items[i].FindControl("checkbox")).Checked)
                {
                    list_rwh.Add(((Label)rptProNumCost.Items[i].FindControl("lbrwh")).Text);
                }
            }
            if (list_rwh.Count > 0)
            {
                CheckOutAct(list_rwh);
                List<string> sql = new List<string>();
                for (int k = 0; k < list_rwh.Count; k++)
                {
                    string fhsrwh = list_rwh[k].ToString();
                    string sqlcwfhs = delete_cwcb(fhsrwh);
                    sql.Add(sqlcwfhs);
                }
                DBCallCommon.ExecuteTrans(sql);
            }

            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('请勾选要反核算的任务号！！！')", true);
            }
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }
        private string delete_cwcb(string fhsrwh)
        {
            string sqldelete = "";
            sqldelete = "update TBCB_BMCONFIRM set CWCB_GZ=0,CWCB_JJFY=0,CWCB_JGYZ=0,CWCB_WGJ=0,CWCB_HSJS=0,CWCB_HCL=0,CWCB_ZJ=0,CWCB_DJ=0,CWCB_ZC=0,CWCB_BZJ=0,CWCB_QTL=0,CWCB_GDZZ=0,CWCB_KBZZ=0,CWCB_WXFY=0,CWCB_CNFB=0,CWCB_YF=0,CWCB_FJCB=0,CWCB_QT=0,CWCB_STATE='0' where TASK_ID='" + fhsrwh + "'";
            return sqldelete;
        }


        /// <summary>
        /// //////////////////////////////////////////////////////////////
        /// </summary>
        #region  分页
        PagerQueryParam pager_org = new PagerQueryParam();

        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager_org.TableName = "TBCB_BMCONFIRM";
            pager_org.PrimaryKey = "TASK_ID";
            pager_org.ShowFields = "TASK_ID,PRJ,ENG,CWCB_STATE,CWCB_HSDATE";
            pager_org.OrderField = "TASK_ID";
            pager_org.StrWhere = ViewState["StrWhere"].ToString();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 50;
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_org);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, NoDataPanel);
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
        #endregion
        /////////////////////////////////////////////////////////////////////
    }
}
