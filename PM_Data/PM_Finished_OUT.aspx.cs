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
using System.IO;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Finished_OUT : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //2016.8.25修改
                string contract_task_view = Request["contract_task_view"];
                if (!string.IsNullOrEmpty(contract_task_view))
                {
                    txtName.Text = contract_task_view.Trim();
                }
                this.ShenHe();
                InitVar();
                GetBoundData();
            }
            InitVar();
            deletepower();
            CheckUser(ControlFinder);
        }

        private void deletepower()
        {
            if (rbl_shenhe.SelectedValue == "0")
            {
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
            }
        }

        private void ShenHe()
        {
            int a = 0;//初始化
            int b = 0;//未审批
            int c = 0;//审批中
            int d = 0;//已驳回
            string sqltext = "select SPZT from TBMP_FINISHED_OUT";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["SPZT"].ToString() == "0")
                {
                    a++;
                }
                if (dt.Rows[i]["SPZT"].ToString() == "1")
                {
                    b++;
                }
                if (dt.Rows[i]["SPZT"].ToString() == "2")
                {
                    c++;
                }
                if (dt.Rows[i]["SPZT"].ToString() == "4")
                {
                    d++;
                }
            }
            rbl_shenhe.Items.Clear();
            rbl_shenhe.Items.Add(new ListItem("全部", "5"));
            if (a != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("初始化" + "</label><label><font color=red>(" + a + ")</font>", "0"));
                rbl_shenhe.SelectedIndex = 1;
                btn_search1_click(null, null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("初始化", "0"));
            }
            if (b != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("未审批" + "</label><label><font color=red>(" + b + ")</font>", "1"));
                rbl_shenhe.SelectedIndex = 2;
                btn_search1_click(null, null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("未审批", "1"));
            }
            if (c != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("审批中" + "</label><label><font color=red>(" + c + ")</font>", "2"));
                rbl_shenhe.SelectedIndex = 3;
                btn_search1_click(null, null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("审批中", "2"));
            }
            rbl_shenhe.Items.Add(new ListItem("已通过", "3"));
            if (d != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("已驳回" + "</label><label><font color=red>(" + d + ")</font>", "4"));
                rbl_shenhe.SelectedIndex = 5;
                btn_search1_click(null, null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("已驳回", "4"));
            }
            rbl_shenhe.SelectedIndex = 0;
        }
        protected void btn_search1_click(object sender, EventArgs e)
        {
            InitPager();
            UCPaging1.CurrentPage = 1;
            GetBoundData();
        }
        private string GetWhere()
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" 0=0");
            if (rbl_shenhe.SelectedValue.ToString() != "5")
            {
                strWhere.Append(" and SPZT='" + rbl_shenhe.SelectedValue.ToString() + "' ");
            }
            if (txtName.Text.Trim() != "")
            {
                strWhere.Append(" and D.CM_CONTR like '%" + txtName.Text.Trim() + "%'");
            }
            if (txtRWH.Text.Trim() != "")
            {
                strWhere.Append(" and a.TSA_ID like '%" + txtRWH.Text.Trim() + "%'");
            }
            if (txtSBMC.Text.Trim() != "")
            {
                strWhere.Append(" and TFO_ENGNAME like '%" + txtSBMC.Text.Trim() + "%'");
            }
            if (txtYZ.Text.Trim() != "")
            {
                strWhere.Append(" and CM_CUSNAME like '%" + txtYZ.Text.Trim() + "%'");
            }
            if (txtXMMC.Text.Trim() != "")
            {
                strWhere.Append(" and D.CM_PROJ like '%" + txtXMMC.Text.Trim() + "%'");
            }
            if (txtTH.Text.Trim() != "")
            {
                strWhere.Append(" and TFO_MAP like '%" + txtTH.Text.Trim() + "%'");
            }
            return strWhere.ToString();
        }

        public string get_spzt(string i)
        {
            string state = "";
            if (i == "0")
            {
                state = "初始化";
            }
            else if (i == "1")
            {
                state = "提交未审批";
            }
            else if (i == "2")
            {
                state = "审批中";
            }
            else if (i == "3")
            {
                state = "已通过";
            }
            else if (i == "4")
            {
                state = "已驳回";
            }
            return state;
        }
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
        }
        protected void btnReset_OnClick(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtYZ.Text = "";
            txtXMMC.Text = "";
            txtSBMC.Text = "";
            txtRWH.Text = "";
            txtTH.Text = "";
            UCPaging1.CurrentPage = 1;
            ReGetBoundData();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label spzt = e.Item.FindControl("SPZT") as Label;
                string TSA_ID = ((Label)e.Item.FindControl("TSA_ID")).Text;
                string TFO_DOCNUM = ((Label)e.Item.FindControl("TFO_DOCNUM")).Text;
                ((Label)e.Item.FindControl("PUR_DD")).ForeColor = System.Drawing.Color.Red;
                ((HyperLink)e.Item.FindControl("HyperLink_lookup")).NavigateUrl = "PM_Finished_out_look.aspx?action=view&docnum=" + Server.UrlEncode(TFO_DOCNUM) + "";
                if (dh.num == TFO_DOCNUM)
                {
                    ((Label)e.Item.FindControl("TFO_DOCNUM")).Visible = false;
                }
                else
                {
                    dh.num = TFO_DOCNUM;
                }
            }
        }

        public class dh
        {
            public static string num; 
        }

        #region 分页
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {
            pager.TableName = "TBMP_FINISHED_OUT AS A LEFT OUTER JOIN View_CM_FaHuo AS B ON A.TSA_ID=B.TSA_ID AND A.TFO_ENGNAME=B.TSA_ENGNAME AND A.TFO_MAP=B.TSA_MAP AND A.TFO_FID=B.CM_FID AND A.TFO_ZONGXU=B.ID left join TBPM_TCTSASSGN as C on A.TSA_ID=C.TSA_ID left join TBCM_PLAN as D on C.ID=D.ID";
            pager.PrimaryKey = "";
            pager.ShowFields = "A.*,B.CM_PROJ,B.CM_CONTR,B.CM_CUSNAME,B.TSA_MAP,B.CM_FHNUM,B.TSA_NUMBER,B.CM_JHTIME,B.TSA_ENGNAME,D.CM_PROJ as proj,D.CM_CONTR as contr";
            pager.OrderField = "TFO_DOCNUM";
            pager.StrWhere = GetWhere();
            pager.OrderType = 1;
            pager.PageSize = 10;
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }
        protected void GetBoundData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);
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

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            for (int i = 0, length = Repeater1.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)Repeater1.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked == true)
                {
                    string id = ((Label)Repeater1.Items[i].FindControl("ID")).Text;
                    string sql = "delete from TBMP_FINISHED_OUT where ID='" + id + "'";
                    DBCallCommon.ExeSqlText(sql);
                    break;
                }
            }
            GetBoundData();
            InitVar();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string sqlFields = "TFO_DOCNUM,B.CM_PROJ,B.CM_CONTR,B.TSA_ID,TFO_ZONGXU,TFO_MAP,TFO_ENGNAME,CM_CUSNAME,TFO_CKNUM,CM_JHTIME,OUTDATE,NOTE";
            string sqlTableName = "TBMP_FINISHED_OUT AS A LEFT OUTER JOIN View_CM_FaHuo AS B ON A.TSA_ID=B.TSA_ID AND A.TFO_ENGNAME=B.TSA_ENGNAME AND A.TFO_MAP=B.TSA_MAP AND A.TFO_FID=B.CM_FID AND A.TFO_ZONGXU=B.ID left join TBPM_TCTSASSGN as C on A.TSA_ID=C.TSA_ID left join TBCM_PLAN as D on C.ID=D.ID";
            string sqlExport = string.Format("SELECT {0} FROM {1}", sqlFields, sqlTableName);
            string sqlWhere = GetWhere();

            if (!string.IsNullOrEmpty(sqlWhere))
            {
                sqlExport += " WHERE ";
                sqlExport += sqlWhere;
            }

            string filename = string.Format("成品出库管理{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            string filestandard = "成品出库管理.xls";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlExport);
            exportCommanmethod.exporteasy(dt, filename, filestandard, 1, true, false, true);
        }
    }
}
