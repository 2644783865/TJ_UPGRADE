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
using System.Collections.Generic;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_FHList : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                UserID.Value = Session["UserID"].ToString();
                bindGrid();
            }
            Warn();
            CheckUser(ControlFinder);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "aa", "aa()", true);
        }

        private void Warn()
        {
            string con = "";
            if (rbl_mytask.SelectedValue == "1")
            {
                con = string.Format("and ((CM_MANCLERK='{0}') or (CM_BMZG='{0}' and CM_YJ1='1') or (CM_GSLD='{0}' and CM_YJ2='1' and CM_YJ1='2'))", UserID.Value);
            }
            string sum = string.Format("select CM_FID from View_CM_FaHuo where CM_CONFIRM='1' {0} group by CM_FID", con);
            rbl_status.Items[0].Text = string.Format("待审核（<font color='red'>{0}</font>）", DBCallCommon.GetDTUsingSqlText(sum).Rows.Count);
            if (rbl_mytask.SelectedValue == "1")
            {
                con = string.Format("and ((CM_MANCLERK='{0}') or (CM_BMZG='{0}' and CM_YJ1='2') or (CM_GSLD='{0}' and CM_YJ2='2' and CM_YJ1='2'))", UserID.Value);
            }
            sum = string.Format("select CM_FID from View_CM_FaHuo where CM_CONFIRM='2' {0} group by CM_FID", con);
            rbl_status.Items[1].Text = string.Format("已通过（<font color='red'>{0}</font>）", DBCallCommon.GetDTUsingSqlText(sum).Rows.Count);
            if (rbl_mytask.SelectedValue == "1")
            {
                con = string.Format("and ((CM_MANCLERK='{0}') or (CM_BMZG='{0}' and CM_YJ1='3') or (CM_GSLD='{0}' and CM_YJ2='3' and CM_YJ1='2'))", UserID.Value);
            }
            sum = string.Format("select CM_FID from View_CM_FaHuo where CM_CONFIRM='3' {0} group by CM_FID", con);
            rbl_status.Items[2].Text = string.Format("已驳回（<font color='red'>{0}</font>）", DBCallCommon.GetDTUsingSqlText(sum).Rows.Count);
        }

        #region 分页初始化

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            InitPager();
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

        private void InitPager()
        {
            pager.TableName = "(select rowid=row_number() over(partition by CM_FID order by ID),* from View_CM_FaHuo)t";
            pager.PrimaryKey = "CM_FID";
            pager.ShowFields = "*";
            pager.OrderField = "CM_ZDTIME desc,CM_BIANHAO";
            pager.StrWhere = ConWhere();
            pager.OrderType = 1;
            pager.PageSize = 10;
            UCPaging1.PageSize = pager.PageSize;
        }

        private string ConWhere()
        {
            string StrWhere = string.Format("{0} like '%{1}%' and CM_CONFIRM='{2}' and rowid='1'", ddlBz.SelectedValue, txtBox.Text, rbl_status.SelectedValue);
            if (rbl_mytask.SelectedValue == "1")
            {
                StrWhere += string.Format("and ((CM_MANCLERK='{0}') or (CM_BMZG='{0}' and CM_YJ1='{1}') or (CM_GSLD='{0}' and CM_YJ2='{1}' and CM_YJ1='2')) ", UserID.Value, rbl_status.SelectedValue);
            }
            return StrWhere;
        } 

        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }

        protected void btn_del_Click(object sender, EventArgs e)
        {
            string strId = string.Empty;
            List<string> list = new List<string>();
            foreach (RepeaterItem rptItem in Repeater1.Items)
            {
                CheckBox chk = (CheckBox)rptItem.FindControl("chk");
                if (chk.Checked)
                {
                    strId += "'" + ((Label)rptItem.FindControl("CM_FID")).Text + "'" + ",";
                }
            }
            strId = strId.Substring(0, strId.Length - 1);
            string sql1 = "delete from TBCM_FHNOTICE where CM_FID in (" + strId + ")";
            string sql2 = "delete from TBCM_FHBASIC where CM_FID in (" + strId + ")";
            list.Add(sql1);
            list.Add(sql2);
            DBCallCommon.ExecuteTrans(list);
            bindGrid();
        }

        List<string> list = new List<string>();
        List<string> col = new List<string>() { "CM_BIANHAO", "CM_CUSNAME", "CM_PROJ", "CM_CONTR", "CM_SH", "CM_JH", "CM_LXR", "CM_LXFS", "CM_JHTIME", "CM_ZDTIME", "MANCLERK", "CM_CONFIRM" };
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.ToString() != "Header")
            {
                string fid = ((DataRowView)e.Item.DataItem).Row["CM_FID"].ToString();
                string sql = "select CM_MANCLERK,CM_BMZG,CM_CONFIRM,CM_YJ1,CM_GSLD from TBCM_FHNOTICE where CM_FID='" + fid + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    string id = UserID.Value;
                    if (id == dr[0].ToString())
                    {
                        if (dr[2].ToString() == "3" || dr[3].ToString() == "1")//未开始评审及驳回可修改
                        {
                            e.Item.FindControl("hyperlink1").Visible = true;
                        }
                    }
                    if ((id == dr[1].ToString() && dr[3].ToString() == "1") || (id == dr[4].ToString() && dr[3].ToString() == "2" && dr[2].ToString() == "1"))//签字
                    {
                        e.Item.FindControl("hyperlink3").Visible = true;
                    }

                }
                if (Session["UserID"].ToString()=="47")
                {
                    ((LinkButton)e.Item.FindControl("lbtnBackCheck")).Visible = true;
                }
                else
                {
                    ((LinkButton)e.Item.FindControl("lbtnBackCheck")).Visible = false;
                }
                if (list.Count == 0)
                {
                    list.Add(fid);
                }
                else if (list.Contains(fid))
                {
                    for (int i = 0; i < col.Count; i++)
                    {
                        ((DataRowView)e.Item.DataItem).Row[col[i]] = "";
                        e.Item.FindControl("hyperlink1").Visible = false;
                        e.Item.FindControl("hyperlink2").Visible = false;
                        e.Item.FindControl("hyperlink3").Visible = false;
                    }
                    e.Item.DataBind();
                    //((DataRowView)e.Item.DataItem).Row.AcceptChanges();
                }
                else
                {
                    list.Add(fid);
                }
            }
        }

        protected void rbl_mytask_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }

        protected void lbtnBackCheck_OnClick(object sender, EventArgs e)
        {
            string CM_FID= ((LinkButton)sender).CommandArgument.ToString();
            string sql = "update TBCM_FHNOTICE set CM_YJ1='1',CM_SJ1='',CM_NOTE1='',CM_YJ2='1',CM_SJ2='',CM_NOTE2='',CM_CONFIRM='1'";
            sql += " where CM_FID='" + CM_FID + "'";
            DBCallCommon.ExeSqlText(sql);
        }
    }
}
