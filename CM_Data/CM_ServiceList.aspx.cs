using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_ServiceList : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        string depid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (Request.QueryString["Dep"] != null)
            {
                depid = Request.QueryString["Dep"].ToString();
            }
            else
            {
                depid = "07";
            }
            if (!IsPostBack)
            {
                //string depid = Session["UserDeptID"].ToString();
                UserID.Value = Session["UserID"].ToString();

                if (depid != "07" && Session["POSITION"].ToString() != "0102")
                {
                    tb_myTask.Visible = false;
                    Lb_Hint.Visible = false;
                    tb_status.Visible = false;
                    btChuLi.Visible = false;
                    tb_add.Visible = false;
                    rbl_mytask.SelectedValue = "0";
                    rbl_status.SelectedValue = "2";
                    rbl_chuli.SelectedIndex = 0;
                }
                if (depid == "031")
                {
                    tb_myTask.Visible = false;
                    Lb_Hint.Visible = false;
                    tb_status.Visible = false;
                    btChuLi.Visible = false;
                    tb_add.Visible = false;
                    rbl_depchuli.Visible = true;
                }
                bindGrid();
            }
            Warn();
            CheckUser(ControlFinder);
        }

        private void Warn()//数目提醒
        {
            string con = "";
            if (rbl_mytask.SelectedValue == "1")
            {
                if (Session["POSITION"].ToString() == "0102")
                {
                    con = string.Format(" and (CM_LEADER='{0}' and CM_YJ11='2')", UserID.Value);
                }
                else
                {
                    con = string.Format(" and ((CM_MANCLERK='{0}') or (CM_DIRECTOR='{0}' and CM_YJ1='1'))", UserID.Value);
                }
            }
            string str = "";
            if (Session["POSITION"].ToString() == "0102")
            {
                str = "and CM_STATE='1'";
            }
            else
            {
                str = "and CM_STATUS='1'";
            }
            string sum = string.Format("select CM_ID from TBCM_APPLICA where {0} like '%{1}%' {2} {3}", ddlBz.SelectedValue, txtBox.Text, con, str);
            rbl_status.Items[0].Text = string.Format("待审核（<font color='red'>{0}</font>）", DBCallCommon.GetDTUsingSqlText(sum).Rows.Count);
            if (rbl_mytask.SelectedValue == "1")
            {
                con = string.Format(" and ((CM_MANCLERK='{0}') or (CM_DIRECTOR='{0}' and CM_YJ1='3') or (CM_LEADER='{0}' and CM_YJ1='2' and CM_YJ2='3'))", UserID.Value);
            }
            sum = string.Format("select CM_ID from TBCM_APPLICA where {0} like '%{1}%' {2} and CM_STATUS='3'", ddlBz.SelectedValue, txtBox.Text, con);
            rbl_status.Items[1].Text = string.Format("已驳回（<font color='red'>{0}</font>）", DBCallCommon.GetDTUsingSqlText(sum).Rows.Count);
            if (rbl_mytask.SelectedValue == "1")
            {
                con = string.Format(" and ((CM_MANCLERK='{0}') or (CM_DIRECTOR='{0}' and CM_YJ1='2') or (CM_LEADER='{0}' and CM_YJ1='2' and CM_YJ2='2'))", UserID.Value);
            }
            sum = string.Format("select CM_ID from TBCM_APPLICA where {0} like '%{1}%' {2} and CM_STATUS='2'", ddlBz.SelectedValue, txtBox.Text, con);
            rbl_status.Items[2].Text = string.Format("已通过（<font color='red'>{0}</font>）", DBCallCommon.GetDTUsingSqlText(sum).Rows.Count);
        }

        #region 分页

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
            pager.TableName = "TBCM_APPLICA as a left join TBDS_STAFFINFO as b on a.CM_MANCLERK=b.ST_ID";
            pager.PrimaryKey = "CM_ID";
            pager.ShowFields = "a.*,b.ST_NAME,(case when CM_STATUS='3' then '已驳回' when CM_STATUS='1' then '待审核' when CM_STATUS='2' then '审核通过' end) as CMSTATUS";
            pager.OrderField = "CM_ZDTIME";
            pager.StrWhere = ConWhere();
            pager.OrderType = 1;
            pager.PageSize = 10;
            UCPaging1.PageSize = pager.PageSize;
        }

        private string ConWhere()
        {
            string StrWhere = string.Format("{0} like '%{1}%'", ddlBz.SelectedValue, txtBox.Text);
            if (rbl_mytask.SelectedValue == "0")
            {
                StrWhere += " and CM_STATUS='" + rbl_status.SelectedValue + "'";
            }
            if (rbl_mytask.SelectedValue == "1")
            {
                StrWhere += string.Format(" and ((CM_MANCLERK='{0}' and CM_STATUS='{1}') or (CM_DIRECTOR='{0}' and CM_YJ1='{1}'))", UserID.Value, rbl_status.SelectedValue);
            }
            if (depid == "031")
            {
                StrWhere = "CM_PART like '%03%' and CM_STATUS='2'";
                if (rbl_depchuli.SelectedValue == "0")
                {
                    StrWhere += " and (CM_CLSTATE='1')";
                }
                else
                {
                    StrWhere += " and (CM_CLSTATE='2')";
                }
            }
            if (depid != "07" && depid != "031" && Session["POSITION"].ToString() != "0102")
            {
                StrWhere = "CM_CLPART like '%" + depid + "%' ";
            }
            if (Session["POSITION"].ToString() == "0102")//王总登陆
            {
                StrWhere = string.Format("{0} like '%{1}%'", ddlBz.SelectedValue, txtBox.Text);
                if (rbl_mytask.SelectedValue == "0")
                {
                    StrWhere += " and CM_STATE='" + rbl_status.SelectedValue + "'";
                }
                if (rbl_mytask.SelectedValue == "1")
                {
                    StrWhere += string.Format(" and CM_YJ2='{0}' and CM_LEADER='{1}' and CM_YJ11='2'", rbl_status.SelectedValue, Session["UserID"].ToString());
                }
            }
            if (Session["POSITION"].ToString() == "0701")//市场部部长登陆
            {
                Rbl_LianXiState.Visible = true;
                if (Rbl_LianXiState.SelectedValue != "")
                {
                    StrWhere += " and CM_YJ11='" + Rbl_LianXiState.SelectedValue + "' and CM_BUMEN='" + UserID.Value + "' ";
                }
            }
            if (rbl_chuli.SelectedValue != null)//是否处理
            {
                switch (rbl_chuli.SelectedValue)
                {
                    case "Y":
                        StrWhere += " and CM_CHULI='Y'";
                        break;
                    case "N":
                        StrWhere += " and CM_CHULI='N'";
                        break;
                }
            }
            if (Rbl_LianXi.SelectedValue != "")
            {
                if (Rbl_LianXi.SelectedValue != "0")
                {
                    StrWhere += " and CM_STATE='" + Rbl_LianXi.SelectedValue + "'";
                }
            }
            return StrWhere;
        } 

        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }

        protected void rbl_mytask_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }

        protected void btn_del_Click(object sender, EventArgs e)
        {
            string strId = string.Empty;
            string context = string.Empty;
            List<string> list = new List<string>();
            foreach (RepeaterItem rptItem in Repeater1.Items)
            {
                CheckBox chk = (CheckBox)rptItem.FindControl("chk");
                if (chk.Checked)
                {
                    strId += "'" + ((Label)rptItem.FindControl("CM_ID")).Text + "'" + ",";
                    context += "'" + ((Label)rptItem.FindControl("CM_CONTEXT")).Text + "'" + ",";
                }
            }
            strId = strId.Substring(0, strId.Length - 1);
            context = context.Substring(0, context.Length - 1);
            #region 删除文件
            string sqlfile = "select fileload,fileName from tb_files where BC_CONTEXT in (" + context + ")";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlfile);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strFilePath = dt.Rows[i]["fileload"].ToString() + @"\" + dt.Rows[i]["fileName"].ToString();
                    File.Delete(strFilePath);
                }
            }
            #endregion

            string sqlTxt = "delete from TBCM_APPLICA where CM_ID in (" + strId + ")";
            list.Add(sqlTxt);
            string sqlTxt1 = "delete from tb_files where BC_CONTEXT in (" + context + ")";
            list.Add(sqlTxt1);
            DBCallCommon.ExecuteTrans(list);
            bindGrid();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string person = UserID.Value;
            if (e.Item.ItemType.ToString() != "Header")
            {
                DataRowView drv = (DataRowView)e.Item.DataItem;
                string part = string.Format("select CM_PART=stuff((select ','+DEP_NAME from (select DEP_NAME from TBDS_DEPINFO where DEP_CODE in ({0}))t for xml path('')),1,1,''), CM_CLPART=stuff((select ','+DEP_NAME from (select DEP_NAME from TBDS_DEPINFO where DEP_CODE in ({1}))t for xml path('')),1,1,'')", drv["CM_PART"].ToString() == "" ? "''" : drv["CM_PART"].ToString(), drv["CM_CLPART"].ToString() == "" ? "''" : drv["CM_CLPART"].ToString());
                drv["CM_PART"] = DBCallCommon.GetDTUsingSqlText(part).Rows[0][0].ToString();
                drv["CM_CLPART"] = DBCallCommon.GetDTUsingSqlText(part).Rows[0][1].ToString();
                e.Item.DataBind();
                //if (drv["CM_MANCLERK"].ToString() == person)
                //{
                if (depid == "07")
                {
                    if ((drv["CM_DIRECTYJ"].ToString() == "" && drv["CM_LEADYJ"].ToString() == "") || drv["CM_STATUS"].ToString() == "3")//未审和驳回的可修改
                    {
                        e.Item.FindControl("hyperlink2").Visible = true;
                    }
                }
                if (depid == "031")
                {
                    e.Item.FindControl("hyperlink5").Visible = false;
                }
                //}
                if ((drv["CM_DIRECTOR"].ToString() == person && drv["CM_DIRECTYJ"].ToString() == ""))
                {
                    e.Item.FindControl("hyperlink3").Visible = true;
                }
                if (drv["CM_PART"].ToString() == "")
                {
                    e.Item.FindControl("hyperlink4").Visible = false;
                }
                if (!string.IsNullOrEmpty(drv["CM_RESULT"].ToString()) || !string.IsNullOrEmpty(drv["CM_URESULT"].ToString()))//填写之后颜色变红
                {
                    ((HyperLink)e.Item.FindControl("hyperlink4")).ForeColor = System.Drawing.Color.Red;
                }
                if (drv["CM_YJ11"].ToString() == "1")
                {
                    ((HyperLink)e.Item.FindControl("hyperlink5")).ForeColor = System.Drawing.Color.Red;
                }
                if (drv["CM_STATUS"].ToString() == "1")
                {
                    e.Item.FindControl("hyperlink5").Visible = false;
                }
                HtmlTableCell tdZT = e.Item.FindControl("tdZT") as HtmlTableCell;
                if (drv["CM_CHULI"].ToString()== "Y")
                {
                    tdZT.BgColor = "Green";
                }
                if (drv["CM_CHULI"].ToString() == "N")
                {
                    tdZT.BgColor = "Red";
                }
            }
        }

        protected void SeleAll_Click(object sender, EventArgs e)
        {
            Response.Redirect("CM_ServiceList.aspx");
        }

        protected void btChuLi_Click(object sender, EventArgs e)
        {
            string strId = string.Empty;
            foreach (RepeaterItem rptItem in Repeater1.Items)
            {
                CheckBox chk = (CheckBox)rptItem.FindControl("chk");
                if (chk.Checked)
                {
                    strId += "'" + ((Label)rptItem.FindControl("CM_ID")).Text + "'" + ",";
                }
            }
            if (strId != "")
            {
                strId = strId.Substring(0, strId.Length - 1);
                string sql = "select * from TBCM_APPLICA where CM_ID in (" + strId + ")";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                bool b = true;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    if (dr["CM_STATUS"].ToString() != "2")
                    {
                        b = false;
                    }
                }
                if (b)
                {
                    sql = "update TBCM_APPLICA set CM_CHULI='Y' where CM_ID in (" + strId + ")";
                    DBCallCommon.ExeSqlText(sql);
                    bindGrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "1", "alert('有申请未审核通过，请检查！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "2", "alert('请勾选项目！');", true);
            }
        }
    }
}
