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

namespace ZCZJ_DPF.PC_Data
{
    public partial class TBPC_ALLclosemarshow : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sqltext = "";
                sqltext = "select ST_NAME,ST_CODE from TBDS_STAFFINFO WHERE ST_DEPID='06'";
                string dataText = "ST_NAME";
                string dataValue = "ST_CODE";
                DBCallCommon.BindDdl(DropDownList1, sqltext, dataText, dataValue);
                DropDownList1.SelectedIndex = 0;
            }
            InitVar();
            if (!IsPostBack)
            {
                GetBoundData();
            }
            CheckUser(ControlFinder);
        }

        #region 分页
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
         //sqltext = "SELECT planno, pjid, pjnm, engid, engnm, ptcode, " +
         //      "marid, marnm, margg, marcz, margb, marunit,marfzunit,length,width,num,fznum, " +
         //      "rpnum, rpfznum,jstimerq,cgrid, cgrnm, isnull(purstate,0) as purstate, purnote, picno,orderno,PUR_MASHAPE,PUR_CSTATE,PUR_ZYDY,PUR_TUHAO  " +
         //      "FROM  View_TBPC_PURCHASEPLAN_IRQ_ORDER   where planno='" + orderno.Text + "' and PUR_CSTATE='1'";
        private void InitPager()
        {
            pager.TableName = "View_TBPC_PURCHASEPLAN_IRQ_ORDER";
            pager.PrimaryKey = "planno";
            pager.ShowFields = "*,isnull(cgrnm,'韩莉红') as cgrnm1";
            pager.OrderField = "planno,ptcode";
            pager.StrWhere = GetSiftData();
            pager.OrderType = 0;
            pager.PageSize = 50;
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
        #endregion
        private string GetSiftData()
        {
            string str = "";
            str = "(PUR_CSTATE='1' or PUR_CSTATE='2')";
            if (tb_pcode.Text != "")
            {
                str += " and planno like '%" + tb_pcode.Text + "%'";
            }
            if (tb_ptcode.Text != "")
            {
                str += " and ptcode like '%" + tb_ptcode.Text + "%'";
            }
            if (tb_pjnm.Text != "")
            {
                str += " and pjnm like '%" + tb_pjnm.Text + "%'";
            }
            if (tb_engnm.Text != "")
            {
                str += " and engnm like '%" + tb_engnm.Text + "%'";
            }
            if (tb_marnm.Text != "")
            {
                str += " and marnm like '%" + tb_marnm.Text + "%'";
            }
            if (tb_margg.Text != "")
            {
                str += " and margg like '%" + tb_margg.Text + "%'";
            }
            if (tb_marcz.Text != "")
            {
                str += " and marcz like '%" + tb_marcz.Text + "%'";
            }
            if (tb_margb.Text != "")
            {
                str += " and margb like '%" + tb_margb.Text + "%'";
            }
            if (DropDownList1.SelectedIndex != 0)
            {
                if (DropDownList1.SelectedItem.Text.ToString() == "韩莉红")
                {
                    str += " and cgrnm is null";
                }
                else
                {
                    str += " and cgrnm='" + DropDownList1.SelectedItem.Text + "'";
                }
            }
            return str;
        }

        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            ReGetBoundData();
        }

        protected void btnReset_OnClick(object sender, EventArgs e)
        {    
            ReGetBoundData();
        }

   
    }
}
