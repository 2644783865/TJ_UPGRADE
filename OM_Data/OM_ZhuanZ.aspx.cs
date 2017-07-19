using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_ZhuanZ : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging2.PageChanged += new UCPaging.PageHandler(Pager_PageChanged1);
            if (!IsPostBack)
            {
                databind();
                databind1();
                doPost(rblSer);
            }
        }

        #region 初始化分页

        void Pager_PageChanged(int pageNumber)
        {
            databind();
        }

        private void databind()
        {
            pager.TableName = "View_TBDS_STAFFINFO";
            pager.PrimaryKey = "ST_ID";
            pager.ShowFields = "*";
            pager.OrderField = "ST_ID";
            pager.StrWhere = strWhere();
            pager.OrderType = 0;
            pager.PageSize = 10;
            UCPaging1.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, rep_ZZ, UCPaging1, NoDataPanel);
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

        private string strWhere()
        {
            string strWhere = "ST_NAME like '%" + txtName.Text.Trim() + "%'";
            if (rblSer.SelectedValue == "1")
            {
                strWhere += " and ST_ZHUANZ='0'";
            }
            else if (rblSer.SelectedValue == "2")
            {
                strWhere += " and ST_ZHUANZ='1'";
            }
            else if (rblSer.SelectedValue == "3")
            {
                strWhere += string.Format(" and DateDiff(DAY,'{0}',ST_ZHENG) between 0 and 14 and ST_ZHUANZ='0'", DateTime.Now.ToString("yyyyMMdd"));
            }
            return strWhere;
        }

        #endregion

        #region 初始化分页

        void Pager_PageChanged1(int pageNumber)
        {
            databind1();
        }

        private void databind1()
        {
            pager.TableName = "View_TBDS_STAFFINFO as a left join (select ST_ID,count(ST_ID) as ST_COUNT from (select ST_ID,ST_CONTRSTART as ST_START,ST_CONTREND as ST_END,ST_FILLDATE from TBDS_STAFFINFO union select * from TBDS_HETONG)t group by ST_ID) as b on a.ST_ID=b.ST_ID";
            pager.PrimaryKey = "ST_ID";
            pager.ShowFields = "a.*,b.ST_COUNT";
            pager.OrderField = "ST_ID";
            pager.StrWhere = strWhere1();
            pager.OrderType = 0;
            pager.PageSize = 10;
            UCPaging2.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging2.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, rep_HTong, UCPaging2, NoDataPane2);
            if (NoDataPane2.Visible)
            {
                UCPaging2.Visible = false;
            }
            else
            {
                UCPaging2.Visible = true;
                UCPaging2.InitPageInfo();
            }
        }

        private string strWhere1()
        {
            string strWhere = "ST_NAME like '%" + txtName1.Text.Trim() + "%' and ST_PD=0 ";
            if (rblHeTong.SelectedValue == "1")
            {
                strWhere += string.Format("and ST_CONTREND<>'' and (DateDiff(DAY,'{0}',ST_CONTREND)>60)", DateTime.Now.ToString("yyyyMMdd"));
            }
            else if (rblHeTong.SelectedValue == "2")
            {
                strWhere += string.Format("and ST_CONTREND<>'' and DateDiff(DAY,'{0}',ST_CONTREND) between 0 and 60", DateTime.Now.ToString("yyyyMMdd"));
            }
            else if (rblHeTong.SelectedValue == "3")
            {
                strWhere += string.Format("and ST_CONTREND<>'' and DateDiff(DAY,'{0}',ST_CONTREND)<0", DateTime.Now.ToString("yyyyMMdd"));
            }
            return strWhere;
        }

        #endregion

        protected void rep_ZZ_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.ToString() != "Header")
            {
                if (((DataRowView)e.Item.DataItem).Row["ST_ZHENG"].ToString() != "")
                {
                    TimeSpan tSpan = DateTime.Now.ToLocalTime() - Convert.ToDateTime(((DataRowView)e.Item.DataItem)["ST_ZHENG"]);
                    if (tSpan.Days <= 14 && ((DataRowView)e.Item.DataItem).Row["ST_ZHUANZ"].ToString() == "0")
                    {
                        ((Label)e.Item.FindControl("ST_NAME")).ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void rep_HTong_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.ToString() != "Header")
            {
                if (((DataRowView)e.Item.DataItem).Row["ST_CONTREND"].ToString() != "")
                {
                    TimeSpan tSpan = Convert.ToDateTime(((DataRowView)e.Item.DataItem)["ST_CONTREND"]) - DateTime.Now.ToLocalTime();
                    if (0 <= tSpan.Days && tSpan.Days <= 60)
                    {
                        ((Label)e.Item.FindControl("ST_NAME")).ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        protected void rblSer_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            databind();
        }

        protected void rblHeTong_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging2.CurrentPage = 1;
            databind1();
        }

        protected void doPost(RadioButtonList rbList)
        {
            foreach (ListItem item in rbList.Items)
            {
                //为预设项添加doPostBack JS  
                if (item.Selected)
                {
                    item.Attributes.Add("onclick", String.Format("javascript:setTimeout('__doPostBack(\\'{0}${1}\\',\\'\\')', 0)", rbList.UniqueID, rbList.Items.IndexOf(item)));
                }
            }
        }

        protected string Edit(string id)
        {
            return "javascript:window.showModalDialog('OM_ZZEdit.aspx?ST_ID=" + id + "','','dialogWidth=500px;dialogHeight=570px')";
        }

        protected string Show(string id)
        {
            return "javascript:window.showModalDialog('OM_HTEdit.aspx?ST_ID=" + id + "','','dialogWidth=500px;dialogHeight=600px')";
        }
    }
}
