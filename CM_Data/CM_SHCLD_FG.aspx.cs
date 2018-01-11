using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_SHCLD_FG : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        string username = string.Empty;
        string depid = string.Empty;
        string position = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            username = Session["UserName"].ToString();
            depid = Session["UserDeptID"].ToString();
            position = Session["POSITION"].ToString();
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
            pager_org.TableName = "CM_SHCLD ";
            pager_org.PrimaryKey = "CLD_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "CLD_ID";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 15;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptSHFWCLD, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }

            BindRbl();
        }

        private string StrWhere()
        {
            string sql = "CLD_ID is not null ";
            if (rblRW.SelectedValue == "1")
            {
                //技术部部长
                if (Session["POSITION"].ToString() == "0301" || Session["POSITION"].ToString() == "1205" || Session["POSITION"].ToString() == "1207")
                {
                    sql += " and (CLD_YYFX_TXR='' or CLD_YYFX_TXR is null) and CLD_SPZT='1y'";
                }
                else
                {
                    sql += " and 1=2";
                }

            }
            else if (rblRW.SelectedValue == "2")
            {
                //技术部部长，以及技术部部长助理
                if (Session["POSITION"].ToString() == "0301" || Session["POSITION"].ToString() == "0302" || Session["POSITION"].ToString() == "1207")
                {
                    sql += " and (CLD_CLYJ_TXR='' or CLD_CLYJ_TXR is null) and CLD_SPZT='1y' and (CLD_YYFX !='' and CLD_YYFX is not null)";
                }
                else
                {
                    sql += " and 1=2";
                }

            }
            else if (rblRW.SelectedValue == "3")
            {
                //市场部部长
                if (Session["POSITION"].ToString() == "0701")
                {
                    sql += " and (CLD_CLFA_TXR='' or CLD_CLFA_TXR is null) and CLD_SPZT='1y' and (CLD_CLYJ !='' and CLD_CLYJ is not null)";
                }
                else
                {
                    sql += " and 1=2";
                }
            }
            else if (rblRW.SelectedValue == "4")
            {
                sql += " and (CLD_CLJG_TXR='' or CLD_CLJG_TXR is null)  and CLD_SPZT='y'";
                if (Session["POSITION"].ToString() == "0301" || Session["UserID"].ToString() == "67" || Session["POSITION"].ToString() == "1207")
                {
                    sql += " and (CLD_FZBM like '%技术部%' )";
                }
                else if (Session["POSITION"].ToString() == "0301" || Session["POSITION"].ToString() == "1205" || Session["POSITION"].ToString() == "1207")
                {
                    sql += " and ( CLD_FZBM like '%质量部%')";
                }
                else if (Session["POSITION"].ToString() == "0401")
                {
                    sql += " and (CLD_FZBM like '%生产部%')";
                }
                else if (Session["POSITION"].ToString() == "0501")
                {
                    sql += " and (CLD_FZBM like '%采购部%')";

                }
                else
                {
                    sql += " and 1=2";
                }
            }
            else if (rblRW.SelectedValue == "5")
            {
                if (Session["POSITION"].ToString() == "0601")
                {
                    sql += " and (CLD_FWFY_TJR='' or CLD_FWFY_TJR is null) and (CLD_SPZT='cljg_y')";
                }
                else
                {
                    sql += " and 1=2";
                }

            }
            return sql;
        }
        #endregion

        private void BindRbl()
        {
            string sql;
            DataTable dt;
            if (Session["POSITION"].ToString() == "0701")
            {
                sql = "select count(CLD_ID) from CM_SHCLD where (CLD_CLFA_TXR='' or CLD_CLFA_TXR is null) and CLD_SPZT='1y' and (CLD_CLYJ !='' and CLD_CLYJ is not null) ";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                rblRW.Items[2].Text = string.Format("处理方案待分工(<font color='red'>{0}</font>)", dt.Rows[0][0].ToString());
            }
            else if (Session["POSITION"].ToString() == "0301" || Session["UserID"].ToString() == "67" || Session["POSITION"].ToString() == "1207")
            {

                sql = "select count(CLD_ID) from CM_SHCLD where (CLD_CLYJ_TXR='' or CLD_CLYJ_TXR is null) and CLD_SPZT='1y' and (CLD_YYFX !='' and CLD_YYFX is not null)";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                rblRW.Items[1].Text = string.Format("处理意见待分工(<font color='red'>{0}</font>)", dt.Rows[0][0].ToString());


                sql = "select count(CLD_ID) from CM_SHCLD where (CLD_CLJG_TXR='' or CLD_CLJG_TXR is null)  and CLD_SPZT='y' and (CLD_FZBM like '%技术部%' ) ";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                rblRW.Items[3].Text = string.Format("处理结果待分工(<font color='red'>{0}</font>)", dt.Rows[0][0].ToString());

            }
            else if (Session["POSITION"].ToString() == "0301" || Session["POSITION"].ToString() == "1205" || Session["POSITION"].ToString() == "1207")
            {
                sql = "select count(CLD_ID) from CM_SHCLD where (CLD_YYFX_TXR='' or CLD_YYFX_TXR is null) and CLD_SPZT='1y'";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                rblRW.Items[0].Text = string.Format("原因分析待分工(<font color='red'>{0}</font>)", dt.Rows[0][0].ToString());


                sql = "select count(CLD_ID) from CM_SHCLD where (CLD_CLJG_TXR='' or CLD_CLJG_TXR is null)  and CLD_SPZT='y' and ( CLD_FZBM like '%质量部%') ";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                rblRW.Items[3].Text = string.Format("处理结果待分工(<font color='red'>{0}</font>)", dt.Rows[0][0].ToString());

            }
            else if (Session["POSITION"].ToString() == "0401")
            {
                sql = "select count(CLD_ID) from CM_SHCLD where (CLD_CLJG_TXR='' or CLD_CLJG_TXR is null)  and CLD_SPZT='y' and (CLD_FZBM like '%生产部%') ";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                rblRW.Items[3].Text = string.Format("处理结果待分工(<font color='red'>{0}</font>)", dt.Rows[0][0].ToString());

            }
            else if (Session["POSITION"].ToString() == "0501")
            {
                sql = "select count(CLD_ID) from CM_SHCLD where (CLD_CLJG_TXR='' or CLD_CLJG_TXR is null)  and CLD_SPZT='y' and (CLD_FZBM like '%采购部%') ";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                rblRW.Items[3].Text = string.Format("处理结果待分工(<font color='red'>{0}</font>)", dt.Rows[0][0].ToString());

            }

            else if (Session["POSITION"].ToString() == "0601")
            {
                sql = "select count(CLD_ID) from CM_SHCLD where (CLD_FWFY_TJR='' or CLD_FWFY_TJR is null) and (CLD_SPZT='cljg_y')";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                rblRW.Items[4].Text = string.Format("服务费用待分工(<font color='red'>{0}</font>)", dt.Rows[0][0].ToString());
            }

            if (username == "曹卫亮" || username == "李小婷")
            {
                sql = string.Format("select ST_NAME,ST_ID from TBDS_STAFFINFO where ST_DEPID in ('03') and ST_PD='0'");
            }
            if (username == "曹卫亮" || username == "陈永秀")
            {
                sql = string.Format("select ST_NAME,ST_ID from TBDS_STAFFINFO where (ST_DEPID in ('12') and ST_PD='0') or ( r_name like '%质量通用角色%' and ST_PD='0')");
            }
            else
            {
                sql = string.Format("select ST_NAME,ST_ID from TBDS_STAFFINFO where  ST_PD='0' and ST_DEPID='{0}'", depid);
            }

            DBCallCommon.BindDDLData(ddlName, sql, "ST_NAME", "ST_NAME");
        }

        protected void rptSHFWCLD_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                Label CLD_SPZT1 = (Label)e.Item.FindControl("CLD_SPZT1");
                Label CLD_CLZT = (Label)e.Item.FindControl("CLD_CLZT");
                if (dr["CLD_SPZT"].ToString() == "0")
                {
                    CLD_SPZT1.Text = "未审批";
                    CLD_CLZT.Text = "未处理";
                }
                else if (dr["CLD_SPZT"].ToString() == "1y")
                {
                    CLD_SPZT1.Text = "审批中";
                    CLD_SPZT1.BackColor = Color.LightGoldenrodYellow;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "2y")
                {
                    CLD_SPZT1.Text = "审批中";
                    CLD_SPZT1.BackColor = Color.LightGoldenrodYellow;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "4y")
                {
                    CLD_SPZT1.Text = "审批中";
                    CLD_SPZT1.BackColor = Color.LightGoldenrodYellow;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "y")
                {
                    CLD_SPZT1.Text = "已通过";
                    CLD_SPZT1.BackColor = Color.LightGreen;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "1n" || dr["CLD_SPZT"].ToString() == "2n" || dr["CLD_SPZT"].ToString() == "4n" || dr["CLD_SPZT"].ToString() == "5n")
                {
                    CLD_SPZT1.Text = "未通过";
                    CLD_SPZT1.BackColor = Color.Red;
                }
                else if (dr["CLD_SPZT"].ToString() == "cljg_y")
                {
                    CLD_SPZT1.Text = "已通过";
                    CLD_SPZT1.BackColor = Color.LightGreen;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "fytj_y")
                {
                    CLD_SPZT1.Text = "已通过";
                    CLD_SPZT1.BackColor = Color.LightGreen;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "over")
                {
                    CLD_SPZT1.Text = "已通过";
                    CLD_SPZT1.BackColor = Color.LightGreen;
                    CLD_CLZT.Text = "已处理";
                    CLD_CLZT.BackColor = Color.LightGreen;
                }
            }

        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }

        protected void btnConfirm_OnClick(object sender, EventArgs e)
        {
            if (ddlName.SelectedValue == "请选择")
            {
                Response.Write("<script>alert('请选择人员！！！')</script>");
                return;
            }
            List<string> list = new List<string>();
            string sql = "";
            if (rblRW.SelectedValue == "1")
            {
                foreach (RepeaterItem item in rptSHFWCLD.Items)
                {
                    if (((CheckBox)item.FindControl("cbxXuHao")).Checked)
                    {
                        sql = string.Format("update CM_SHCLD set CLD_YYFX_TXR='{0}',CLD_YYFX_FGR='{1}' where CLD_ID='{2}'", ddlName.SelectedValue, username, ((HiddenField)item.FindControl("CLD_ID")).Value);
                        list.Add(sql);
                    }
                }
            }
            else if (rblRW.SelectedValue == "2")
            {
                foreach (RepeaterItem item in rptSHFWCLD.Items)
                {
                    if (((CheckBox)item.FindControl("cbxXuHao")).Checked)
                    {
                        sql = string.Format("update CM_SHCLD set CLD_CLYJ_TXR='{0}',CLD_CLYJ_FGR='{1}' where CLD_ID='{2}'", ddlName.SelectedValue, username, ((HiddenField)item.FindControl("CLD_ID")).Value);
                        list.Add(sql);
                    }
                }
            }
            else if (rblRW.SelectedValue == "3")
            {
                foreach (RepeaterItem item in rptSHFWCLD.Items)
                {
                    if (((CheckBox)item.FindControl("cbxXuHao")).Checked)
                    {
                        sql = string.Format("update CM_SHCLD set CLD_CLFA_TXR='{0}',CLD_CLFA_FGR='{1}' where CLD_ID='{2}'", ddlName.SelectedValue, username, ((HiddenField)item.FindControl("CLD_ID")).Value);
                        list.Add(sql);
                    }
                }
            }
            else if (rblRW.SelectedValue == "4")
            {
                foreach (RepeaterItem item in rptSHFWCLD.Items)
                {
                    if (((CheckBox)item.FindControl("cbxXuHao")).Checked)
                    {
                        sql = string.Format("update CM_SHCLD set CLD_CLJG_TXR='{0}',CLD_CLJG_FGR='{1}' where CLD_ID='{2}'", ddlName.SelectedValue, username, ((HiddenField)item.FindControl("CLD_ID")).Value);
                        list.Add(sql);
                    }
                }
            }
            else if (rblRW.SelectedValue == "5")
            {
                foreach (RepeaterItem item in rptSHFWCLD.Items)
                {
                    if (((CheckBox)item.FindControl("cbxXuHao")).Checked)
                    {
                        sql = string.Format("update CM_SHCLD set CLD_FWFY_TJR='{0}',CLD_FWFY_FGR='{1}' where CLD_ID='{2}'", ddlName.SelectedValue, username, ((HiddenField)item.FindControl("CLD_ID")).Value);
                        list.Add(sql);
                    }
                }
            }
            if (list.Count == 0)
            {
                Response.Write("<script>alert('请至少勾选一条项目！！')</script>");
                return;
            }
            else
            {
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('分工出现问题，请联系管理员！！')</script>");
                    return;
                }
            }
            bindrpt();
        }

    }
}
