using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_KaoHe_JXGZ_List : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUser(ControlFinder);
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                BindPart();
                this.BindYearMoth(dplYear, dplMoth);
                databind();

                zhuanzhnegtx();//转正提醒
            }

            ControlVisible();
        }


        private void BindPart()
        {
            string stId = Session["UserId"].ToString();

            DataTable dt = DBCallCommon.GetPermeision(3, stId);
            ddlDep.DataSource = dt;
            ddlDep.DataTextField = "DEP_NAME";
            ddlDep.DataValueField = "DEP_CODE";
            ddlDep.DataBind();
        }


        #region 初始化分页

        void Pager_PageChanged(int pageNumber)
        {
            databind();
            ControlVisible();
        }

        private void databind()
        {
            pager.TableName = "TBDS_KaoHe_JXList as a left join TBDS_DEPINFO as b on a.DepId=b.DEP_CODE";
            pager.PrimaryKey = "Id";
            pager.ShowFields = "*";
            pager.OrderField = "ZDTIME";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;
            pager.PageSize = 30;
            UCPaging1.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, rep_Kaohe, UCPaging1, NoDataPanel);
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


        /// <summary>
        /// 绑定年月
        /// </summary>
        /// <param name="dpl_Year"></param>
        /// <param name="dpl_Month"></param>
        private void BindYearMoth(DropDownList dpl_Year, DropDownList dpl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            //dpl_Year.SelectedIndex = 0;
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                dpl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            dpl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            //dpl_Month.SelectedIndex = 0;
        }

        private string strWhere()
        {
            string strWhere = "1=1 ";

            if (txtStart.Text.Trim() != "")
            {
                strWhere += " and ZDTIME>='" + txtStart.Text.Trim() + "'";
            }
            if (txtEnd.Text.Trim() != "")
            {
                strWhere += " and ZDTIME<='" + txtEnd.Text.Trim() + "'";
            }
            if (dplYear.SelectedIndex != 0)
            {
                strWhere += " and Year='" + dplYear.SelectedValue + "'";
            }
            if (dplMoth.SelectedIndex != 0)
            {
                strWhere += " and Month='" + dplMoth.SelectedValue + "'";
            }
            if (ddlDep.SelectedValue != "00")
            {
                strWhere += " and DepId='" + ddlDep.SelectedValue + "'";
            }
            if (rblState.SelectedValue == "0")
            {
                strWhere += " and State='0'";
            }
            else if (rblState.SelectedValue == "1")
            {
                strWhere += " and State ='1'";
            }
            else if (rblState.SelectedValue == "2")
            {
                strWhere += " and State ='2'";
            }
            else if (rblState.SelectedValue == "3")
            {
                strWhere += " and State ='3'";
            }
            else if (rblState.SelectedValue == "4")
            {
                strWhere += " and State ='1' and SPRID='" + Session["UserId"].ToString() + "'";
            }
            return strWhere;
        }

        #endregion

        protected void lnkDelete_OnClick(object sender, EventArgs e)
        {
            string lotnum = ((LinkButton)sender).CommandArgument;
            List<string> list = new List<string>();

            string sqlText = "delete from TBDS_KaoHe_JXList where Context='" + lotnum + "'";
            list.Add(sqlText);
            sqlText = "delete from TBDS_KaoHe_JXDetail where Context='" + lotnum + "'";
            list.Add(sqlText);
            try
            {
                DBCallCommon.ExecuteTrans(list);
            }
            catch (Exception)
            {

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据异常，请联系管理员！！！');", true);
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！！！');", true);
            UCPaging1.CurrentPage = 1;
            databind();
            ControlVisible();


        }
        protected void ddl_Year_SelectedIndexChanged(object sender, EventArgs e)
        {

            UCPaging1.CurrentPage = 1;
            databind();
            ControlVisible();
            zhuanzhnegtx();//转正提醒
        }

        private void ControlVisible()
        {
            foreach (RepeaterItem item in rep_Kaohe.Items)
            {
               
                HyperLink hlkAudit = item.FindControl("HyperLink3") as HyperLink;
                HyperLink hlkEdit = item.FindControl("HyperLink4") as HyperLink;
                LinkButton btnDelete = item.FindControl("lnkDelete") as LinkButton;
                if (rblState.SelectedValue == "4")
                {
                    hlkAudit.Visible = true;
                }
                else
                {
                    hlkAudit.Visible = false;
                }
                if (rblState.SelectedValue == "0" || rblState.SelectedValue == "3")
                {
                    hlkEdit.Visible = true;
                    btnDelete.Visible = true;
                }
                else
                {
                    hlkEdit.Visible = false;
                    btnDelete.Visible = false;
                }
            }
        }


        private void zhuanzhnegtx()
        {
            if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
            {
                string strnamezhengry = "";
                string lastyear = "";
                string lastmonth = "";
                try
                {
                    if (CommonFun.ComTryInt(dplMoth.SelectedValue.ToString().Trim()) == 1)
                    {
                        lastyear = (CommonFun.ComTryInt(dplYear.SelectedValue.ToString().Trim()) - 1).ToString().Trim();
                        lastmonth = "12";
                    }
                    else
                    {
                        lastyear = dplYear.SelectedValue.ToString().Trim();
                        lastmonth = (CommonFun.ComTryInt(dplMoth.SelectedValue.ToString().Trim()) - 1).ToString("00").Trim();
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('获取年月份出错！');", true);
                    return;
                }
                string sqlzhengry = "select ST_NAME from TBDS_STAFFINFO where ST_ZHENG>='" + lastyear + "-" + lastmonth + "-21' and ST_ZHENG<='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "-20' and ST_ID in(select QD_STID from OM_GZQD where QD_YEARMONTH='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "')";
                System.Data.DataTable dtzhengry = DBCallCommon.GetDTUsingSqlText(sqlzhengry);
                if (dtzhengry.Rows.Count > 0)
                {
                    for (int m = 0; m < dtzhengry.Rows.Count; m++)
                    {
                        strnamezhengry += dtzhengry.Rows[m]["ST_NAME"].ToString().Trim() + ",";
                    }
                    strnamezhengry = strnamezhengry.Substring(0, strnamezhengry.Length - 1);
                    ipt_zhuanzheng.Value = strnamezhengry;
                    ipt_zhuanzheng.Attributes.Add("title", strnamezhengry);
                }
            }
        }
    }
}
