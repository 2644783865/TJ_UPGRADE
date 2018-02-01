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
    public partial class PC_CGHTFPGL : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        string username = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            username = Session["UserName"].ToString();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                bindrpt();
            }
        }


        #region 分页
        private void Pager_PageChanged(int pageNumber)//换页事件
        {
            bindrpt();
        }

        private void bindrpt()
        {
            pager_org.TableName = "PC_CGHT";
            pager_org.PrimaryKey = "HT_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "HT_ID";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptCGHTGL, UCPaging1, palNoData);
            foreach (RepeaterItem item in rptCGHTGL.Controls)
            {
                if (item.ItemType == ListItemType.Footer)
                {
                    double money = 0;
                    int num = 0;
                    string sql = "select * from PC_CGHT ";
                    if (pager_org.StrWhere.Trim() != "")
                    {
                        sql += " where " + pager_org.StrWhere;
                    }
                    DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                    foreach (DataRow dr in dt1.Rows)
                    {
                        string[] a = dr["HT_HTZJ"].ToString().Split(new char[] { '(', ')', '（', '）'}, StringSplitOptions.None);
                        for (int i = 0, length = a.Length; i < length; i++)
                        {
                            money += CommonFun.ComTryDouble(a[i]);
                        }
                    }
                    num = dt1.Rows.Count;
                    ((Label)item.FindControl("lbNUM")).Text = num.ToString();
                    ((Label)item.FindControl("lbMONEY")).Text = money.ToString();
                    break;
                }
            }
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        private string StrWhere()
        {
            string sql = "0=0";
            if (rblSP.SelectedValue == "0")//未审批
            {
                sql += " and HT_SPZT='0'";
            }
            else if (rblSP.SelectedValue == "1")//审批中
            {
                sql += " and HT_SPZT in ('1y','2y','5y')";
            }
            else if (rblSP.SelectedValue == "2")//已通过
            {
                sql += " and HT_SPZT='y'";
            }
            else if (rblSP.SelectedValue == "3")//已驳回
            {
                sql += " and HT_SPZT='n'";
            }
            if (txtHTBH.Text.Trim() != "")
            {
                sql += " and HT_XFHTBH like'%" + txtHTBH.Text.Trim() + "%'";
            }
            if (txtDDBH.Text.Trim() != "")
            {
                sql += " and HT_DDBH like'%" + txtDDBH.Text.Trim() + "%'";
            }
            if (txtGYS.Text.Trim() != "")
            {
                sql += " and HT_GF like '%" + txtGYS.Text.Trim() + "%'";
            }
            if (txtQSSJ.Text.Trim() != "")
            {
                sql += " and HT_ZDSJ > '" + txtQSSJ.Text.Trim() + "'";
            }
            if (txtJZSJ.Text.Trim() != "")
            {
                sql += " and HT_ZDSJ < '" + txtJZSJ.Text.Trim() + "'";
            }
            string arrhtid = "0";
            double htmoney = 0;
            double fpmoney = 0;

            string sql00 = "select * from PC_CGHT where isnull(HT_FPZT,'n')!='y' and " + sql;
            DataTable dt00 = DBCallCommon.GetDTUsingSqlText(sql00);
            if (dt00.Rows.Count > 0)
            {
                for (int i = 0; i < dt00.Rows.Count; i++)
                {
                    htmoney = 0;
                    fpmoney = 0;
                    string[] a = dt00.Rows[i]["HT_HTZJ"].ToString().Trim().Split(new char[] { '(', ')' }, StringSplitOptions.None);
                    for (int j = 0, length = a.Length; j < length; j++)
                    {
                        htmoney += Math.Round(CommonFun.ComTryDouble(a[j]),2);
                    }
                    string sqlgetfpje = "select sum(BR_KPJE) as tolkpje from TBPC_PURBILLRECORD where BR_HTBH='" + dt00.Rows[i]["HT_XFHTBH"].ToString().Trim() + "'";
                    DataTable dtgetfpje = DBCallCommon.GetDTUsingSqlText(sqlgetfpje);
                    if (dtgetfpje.Rows.Count > 0)
                    {
                        fpmoney = Math.Round((CommonFun.ComTryDouble(dtgetfpje.Rows[0]["tolkpje"].ToString().Trim()))*10000,2);
                    }
                    if (fpmoney < htmoney)
                    {
                        arrhtid += "," + CommonFun.ComTryInt(dt00.Rows[i]["HT_ID"].ToString().Trim()) + "";
                    }
                }
            }
            if (Radiofpzt.SelectedIndex == 0)
            {
                sql += " and HT_ID not in(" + arrhtid + ")";
            }
            if (Radiofpzt.SelectedIndex == 1)
            {
                sql += " and HT_ID in(" + arrhtid + ")";
            }
            return sql;
        }
        #endregion

        protected void Query(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }



        protected void rptCGHTGL_OnItemDataBound(object sender, RepeaterItemEventArgs e)//控制审批权限
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string htbh = ((Label)e.Item.FindControl("lbHT_XFHTBH")).Text.Trim();
                string sqlgetdata="select sum(BR_KPJE) as tolkpje from TBPC_PURBILLRECORD where BR_HTBH='"+htbh+"'";
                DataTable dtgetdata = DBCallCommon.GetDTUsingSqlText(sqlgetdata);
                if (dtgetdata.Rows.Count > 0)
                {
                    ((Label)e.Item.FindControl("lbydfpje")).Text = ((CommonFun.ComTryDouble(dtgetdata.Rows[0]["tolkpje"].ToString().Trim()))*10000).ToString().Trim();
                }
                DataRowView dr = (DataRowView)e.Item.DataItem;
                if (username != "高浩" && username != "王福泉" && username != "周文轶" && username != "姜中毅" && username != "赵宏观" && username != dr["HT_ZDR"].ToString() && username != "李洪清")
                {
                    e.Item.FindControl("lbHT_HTZJ").Visible = false;
                }
            }
        }


       //发票完结
        protected void btnComplete_OnClick(object sender, EventArgs e)
        {
            string htid = (sender as LinkButton).CommandArgument.ToString().Trim();
            LinkButton linbtnsender = (LinkButton)sender;
            RepeaterItem retim=(RepeaterItem)linbtnsender.Parent;
            string fpnote = ((TextBox)retim.FindControl("fpnote")).Text.Trim();

            string sqlupdate = "update PC_CGHT set HT_FPZT='y',HT_FPNOTE='" + fpnote + "' where HT_ID='"+htid+"'";
            DBCallCommon.ExeSqlText(sqlupdate);
            bindrpt();
        }
    }
}
