using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_KaoHeList : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                BindData();
                databind();
                ControlVisible();
            }
            CheckUser(ControlFinder);
        }

        //绑定基本信息
        private void BindData()
        {

            string Stid = Session["UserId"].ToString();
            System.Data.DataTable dt = DBCallCommon.GetPermeision(20, Stid);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
        }

        #region 初始化分页

        void Pager_PageChanged(int pageNumber)
        {
            databind();
        }

        private void databind()
        {
            pager.TableName = "View_TBDS_KaoHe";
            pager.PrimaryKey = "kh_Id";
            pager.ShowFields = "*,(Kh_Year+'-'+Kh_Month) as KhYearMonth";
            pager.OrderField = "kh_Time";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;
            pager.PageSize = 100;
            UCPaging1.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
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
            string strWhere = "ST_NAME like '%" + txtName.Text.Trim() + "%' and ST_PD=0";

            if (ddl_Depart.SelectedValue != "00")
            {
                strWhere += " and ST_DEPID='" + ddl_Depart.SelectedValue + "'";
            }
            if (dplYear.SelectedValue != "-请选择-")
            {
                strWhere += " and kh_Year='" + dplYear.SelectedValue+ "'";
            }
            if (dplMoth.SelectedValue != "-请选择-")
            {
                strWhere += " and kh_Month='" + dplMoth.SelectedValue + "'";
            }
            if (ddlType.SelectedValue != "00")
            {
                strWhere += " and kh_Type='" + ddlType.SelectedValue + "'";
            }
            if (rblState.SelectedValue == "0")
            {
                strWhere += " and kh_State='0'";
            }
            else if (rblState.SelectedValue == "1")
            {
                strWhere += " and kh_State in ('1','2','3','4','5')";
            }
            else if (rblState.SelectedValue == "2")
            {
                strWhere += " and kh_State  in ('6','7') ";
            }
            if (rblshstate.SelectedValue == "1")
            {
                strWhere += " and Kh_shtoltalstate='1'";
            }
            else if (rblshstate.SelectedValue == "2")
            {
                strWhere += " and Kh_shtoltalstate='2'";
            }
            else if (rblshstate.SelectedValue == "3")
            {
                strWhere += " and Kh_shtoltalstate='3'";
            }
            return strWhere;
        }

        #endregion

        protected void ddl_Year_SelectedIndexChanged(object sender, EventArgs e)
        {

            UCPaging1.CurrentPage = 1;
            databind();
            ControlVisible();
        }

        protected void rblshstate_SelectedIndexChanged(object sender, EventArgs e)
        {

            UCPaging1.CurrentPage = 1;
            databind();
            ControlVisible();
        }



        private void ControlVisible()
        {
            //2016.12.23修改
            if (Session["POSITION"].ToString().Trim() == "0204" || Session["POSITION"].ToString().Trim() == "0205")
            {
                btnDelete.Visible = true;
            }
            //if (rblState.SelectedValue == "0" && Session["POSITION"].ToString().Trim()=="0205")
            //{
            //    btnDelete.Visible = true;
            //}
            else
            {
                btnDelete.Visible = false;
            }
            foreach (RepeaterItem item in rep_Kaohe.Items)
            {
                HyperLink hlk = item.FindControl("HyperLink2") as HyperLink;
                if (rblState.SelectedValue == "0")
                {
                    hlk.Visible = true;
                }
                else
                {
                    hlk.Visible = false;
                }
            }
        }


        //删除（管理员权限）
        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            List<string> sql = new List<string>();
            string sqltext = "";
            int k = 0;
            foreach (RepeaterItem item in rep_Kaohe.Items)
            {
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)item.FindControl("CKBOX_SELECT");
                if (chk.Checked)
                {
                    k++;
                }
            }
            if (k == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要删除的数据！！！');", true);
                return;
            }
            else
            {
                foreach (RepeaterItem item in rep_Kaohe.Items)
                {
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)item.FindControl("CKBOX_SELECT");
                    if (chk.Checked)
                    {
                        string kh_Context = ((System.Web.UI.WebControls.Label)item.FindControl("lbkh_Context")).Text.ToString().Trim();

                        //2016.12.23修改
                        string sql_kh_context = "select * from TBDS_KaoHeList  where Kh_Context='" + kh_Context + "'";
                        System.Data.DataTable dt_kh_context = DBCallCommon.GetDTUsingSqlText(sql_kh_context);
                        if (dt_kh_context.Rows.Count>0)
                        {
                            if (dt_kh_context.Rows[0]["kh_State"].ToString() == "6" || dt_kh_context.Rows[0]["kh_State"].ToString() == "7")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已完成评价，无法进行删除！！！');", true);
                                return;
                            }
                            if (dt_kh_context.Rows[0]["Kh_shtoltalstate"].ToString() == "2")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已完成审核，无法进行删除！！！');", true);
                                return;
                            }
                        }

                        sqltext = "delete from TBDS_KaoHeList where Kh_Context='" + kh_Context + "'";
                        sql.Add(sqltext);
                        sqltext = "delete from TBDS_KaoHeDetail where kh_Context='" + kh_Context + "'";
                        sql.Add(sqltext);
                        sqltext = "delete from TBDS_KaoHeColReal where kh_context='" + kh_Context + "'";
                        sql.Add(sqltext);
                    }
                }
            }
            DBCallCommon.ExecuteTrans(sql);

            //2016.12.23修改
            Response.Write("<script>alert('删除成功！');window.location.href='OM_KaoHeList.aspx';</script>");
            //UCPaging1.CurrentPage = 1;
            //databind();
            //ControlVisible();
        }
    }
}
