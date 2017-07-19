using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_RYPZQD : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        string username = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            username = Session["UserName"].ToString();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                BindDropdownList();
                bindrpt();
            }
        }

        private void BindDropdownList()
        {
            string sql = "select  DEP_CODE,DEP_NAME from TBDS_DEPINFO where  DEP_FATHERID=0";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddlBM.Items.Add(new ListItem("-全部-", "0"));
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                ddlBM.Items.Add(new ListItem(dt.Rows[i]["DEP_NAME"].ToString(), dt.Rows[i]["DEP_CODE"].ToString()));
            }
        }

        #region 分页
        private void Pager_PageChanged(int pageNumber)//换页事件
        {
            bindrpt();
        }

        private void bindrpt()
        {
            pager_org.TableName = "(select c.*,d.DEP_PZRS ,(DEP_PZRS-DEP_YDRS)as DEP_QBRS from (select ST_DEPID,DEP_NAME,ST_POSITION,DEP_POSITION,count(ST_NAME) as DEP_YDRS, stuff((select '，'+ST_NAME from TBDS_STAFFINFO as b where (b.ST_POSITION=a.ST_POSITION and b.ST_POSITION is not null and b.ST_PD='0' ) for xml path('')),1,1,'') as ST_PEAPLE from View_TBDS_STAFFINFO as a where ST_PD='0' and ST_DEPID is not null and ST_POSITION is not null group by ST_DEPID,DEP_NAME,ST_POSITION,DEP_POSITION)as c left join TBDS_DEPINFO as d on c.ST_POSITION = d.DEP_CODE)t";
            pager_org.PrimaryKey = "ST_POSITION";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "ST_DEPID";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 100;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptRYPZ, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            BindFooter();
        }

        private void BindFooter()
        {
            string sql = "select c.*,d.DEP_PZRS ,(DEP_PZRS-DEP_YDRS)as DEP_QBRS from (select ST_DEPID,DEP_NAME,ST_POSITION,DEP_POSITION,count(ST_NAME) as DEP_YDRS, stuff((select '，'+ST_NAME from TBDS_STAFFINFO as b where (b.ST_POSITION=a.ST_POSITION and b.ST_POSITION is not null ) for xml path('')),1,1,'') as ST_PEAPLE from View_TBDS_STAFFINFO as a where ST_PD='0' and ST_DEPID is not null and ST_POSITION is not null group by ST_DEPID,DEP_NAME,ST_POSITION,DEP_POSITION)as c left join TBDS_DEPINFO as d on c.ST_POSITION = d.DEP_CODE";
            if (ddlBM.SelectedValue != "0")
            {
                sql += " where ST_DEPID='" + ddlBM.SelectedValue + "'";
            }
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            int pzrs = 0;
            int ydrs = 0;
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                if (dt.Rows[i]["DEP_PZRS"].ToString() == "")
                {
                    pzrs += 0;
                }
                else
                {
                    pzrs += Convert.ToInt32(dt.Rows[i]["DEP_PZRS"].ToString());
                }
                ydrs += Convert.ToInt32(dt.Rows[i]["DEP_YDRS"].ToString());
            }
            foreach (RepeaterItem item in rptRYPZ.Controls)
            {
                if (item.ItemType == ListItemType.Footer)
                {
                    ((Label)item.FindControl("lbPZRS")).Text = pzrs.ToString();
                    ((Label)item.FindControl("lbYDRS")).Text = ydrs.ToString();
                    ((Label)item.FindControl("lbWDRS")).Text = (pzrs - ydrs).ToString();
                    break;
                }
            }
        }

        private string StrWhere()
        {
            string sql = "";
            if (ddlBM.SelectedValue != "0")
            {
                sql += " ST_DEPID='" + ddlBM.SelectedValue + "'";
            }
            return sql;
        }
        #endregion



        protected void rptRYPZ_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }

        protected void btnSave_onserverclick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0,length=rptRYPZ.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptRYPZ.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    string ST_POSITION = ((HiddenField)rptRYPZ.Items[i].FindControl("ST_POSITION")).Value;
                    string DEP_PZSR = ((TextBox)rptRYPZ.Items[i].FindControl("DEP_PZSR")).Text.Trim();
                    string sql = "update TBDS_DEPINFO set DEP_PZRS=" + DEP_PZSR + " where DEP_CODE='" + ST_POSITION+"'";
                    list.Add(sql);
                }
            }
            if (list.Count==0)
            {
                Response.Write("<script>alert('请勾选需要修改的数据行！！！')</script>");
                return;
            }
            try
            {
                DBCallCommon.ExecuteTrans(list);
            }
            catch 
            {
                Response.Write("<script>alert('保存的语句出现问题！！！请与管理员联系')</script>");
                return;
            }
            bindrpt();
        }

    }
}
