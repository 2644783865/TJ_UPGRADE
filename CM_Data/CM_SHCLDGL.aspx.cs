using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Drawing;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_SHCLDGL : BasicPage
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
            PowerControl();
            string a = txtTXR.Text;
            CheckUser(ControlFinder);
        }

        private void PowerControl()
        {
            btnFG.Visible = false;
            btnFG1.Visible = false;
            btnFG2.Visible = false;
            btnFG3.Visible = false;
            btnDelete.Visible = false;
            btnAlter.Visible = false;
            btnRefuse.Visible = false;
            btnAdd.Visible = false;
            if (position == "0701")
            {
                btnRefuse.Visible = true;
                btnFG2.Visible = true;
            }
            if (depid == "07")
            {
                btnAdd.Visible = true;
                btnDelete.Visible = true;
                btnAlter.Visible = true;
            }
            if (position == "0301")
            {
                btnFG.Visible = true;
                btnFG1.Visible = true;
            }
            if (position == "0601")
            {
                btnFG3.Visible = true;
            }
        }

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
        }

        private string StrWhere()
        {
            string sql = "CLD_ID is not null";
            if (rblRW.SelectedValue == "2")
            {
                sql += " and ( CLD_YYFX_TXR='" + username + "'";
                sql += " or CLD_YYFX_FGR='" + username + "'";
                sql += " or CLD_CLYJ_TXR='" + username + "'";
                sql += " or CLD_CLYJ_FGR='" + username + "'";
                sql += " or CLD_CLFA_TXR='" + username + "'";
                sql += " or CLD_CLJG_TXR='" + username + "'";
                sql += " or CLD_CLFA_FGR='" + username + "'";
                sql += " or CLD_FWFY_TJR='" + username + "'";
                sql += " or CLD_FWFY_FGR='" + username + "'";
                sql += " or CLD_ZDR='" + username + "'";
                sql += " or CLD_SPR1='" + username + "'";
                sql += " or CLD_SPR2='" + username + "'";
                sql += " or CLD_SPR4='" + username + "'";
                sql += " or CLD_SPR5='" + username + "'";
                sql += " or CLD_FZBMID like '%" + depid + "%'";
                sql += ")";
            }
            if (cbxDFG.Checked)
            {
                if (username == "曹卫亮")
                {
                    sql = " (CLD_YYFX_TXR is null or CLD_YYFX_TXR='') or (CLD_CLYJ_TXR is null or CLD_CLYJ_TXR='')";
                }
                else if (username == "李利恒")
                {
                    sql = " (CLD_CLFA_TXR is null or CLD_CLFA_TXR='')";
                }
                else if (username == "叶宝松")
                {
                    sql = " (CLD_FWFY_TJR is null or CLD_FWFY_TJR='') and CLD_SPZT='cljg_y'";
                }
                else
                {
                    sql = " CLD_ID is not null";
                }
            }
            if (ddlSPZT.SelectedValue == "2")
            {
                sql += " and CLD_SPZT='0'";
            }
            else if (ddlSPZT.SelectedValue == "3")
            {
                sql += " and CLD_SPZT in('1y','2y','4y')";
            }
            else if (ddlSPZT.SelectedValue == "4")
            {
                sql += " and CLD_SPZT in('y','cljg_y','fytj_y')";
            }
            else if (ddlSPZT.SelectedValue == "5")
            {
                sql += " and CLD_SPZT in('1n','2n','4n','5n')";
            }
            if (ddlCLZT.SelectedValue == "2")
            {
                sql += " and CLD_SPZT='0'";
            }
            else if (ddlCLZT.SelectedValue == "3")
            {
                sql += " and CLD_SPZT not in ('0','over','1n','2n','4n','5n')";
            }
            else if (ddlCLZT.SelectedValue == "4")
            {
                sql += " and CLD_SPZT='over'";
            }
            if (ddlTX.SelectedValue == "2")
            {
                sql += " and (" + ddlTXLX.SelectedValue + " is null or " + ddlTXLX.SelectedValue + "='')";
            }
            else if (ddlTX.SelectedValue == "3")
            {
                sql += " and (" + ddlTXLX.SelectedValue + " is not null and " + ddlTXLX.SelectedValue + "!='')";
            }
            if (txtBH.Value != "")
            {
                sql += " and CLD_BH like '%" + txtBH.Value.Trim() + "%'";
            }
            if (txtHTH.Value != "")
            {
                sql += " and CLD_HTH like '%" + txtHTH.Value.Trim() + "%'";
            }
            if (txtXMMC.Value != "")
            {
                sql += " and CLD_XMMC like '%" + txtXMMC.Value.Trim() + "%'";
            }
            if (txtGKMC.Value != "")
            {
                sql += " and CLD_GKMC like '%" + txtGKMC.Value.Trim() + "%'";
            }
            if (txtRWH.Value != "")
            {
                sql += " and CLD_RWH like '%" + txtRWH.Value.Trim() + "%'";
            }
            return sql;
        }

        protected void rptSHFWCLD_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                string CLD_ID = ((HtmlInputHidden)e.Item.FindControl("CLD_ID")).Value;
                string sql = "select * from CM_SHCLD where CLD_ID='" + CLD_ID + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                DataRow dr = dt.Rows[0];
                HyperLink hplTXYY = (HyperLink)e.Item.FindControl("hplTXYY");
                HyperLink hplTXYJ = (HyperLink)e.Item.FindControl("hplTXYJ");
                HyperLink hplTXFA = (HyperLink)e.Item.FindControl("hplTXFA");
                HyperLink hplSP = (HyperLink)e.Item.FindControl("hplSP");
                HyperLink hplTXJG = (HyperLink)e.Item.FindControl("hplTXJG");
                HyperLink hplTJ = (HyperLink)e.Item.FindControl("hplTJ");
                Label CLD_SPZT1 = (Label)e.Item.FindControl("CLD_SPZT1");
                Label CLD_CLZT = (Label)e.Item.FindControl("CLD_CLZT");
                hplTXYY.Visible = false;
                hplTXYJ.Visible = false;
                hplTXFA.Visible = false;
                hplSP.Visible = false;
                hplTXJG.Visible = false;
                hplTJ.Visible = false;
                if (dr["CLD_SPZT"].ToString() == "0")
                {
                    if (username == dr["CLD_SPR1"].ToString())
                    {
                        hplSP.Visible = true;
                    }
                    CLD_SPZT1.Text = "未审批";
                    CLD_CLZT.Text = "未处理";
                }
                else if (dr["CLD_SPZT"].ToString() == "1y")
                {
                    if (username == dr["CLD_YYFX_TXR"].ToString())
                    {
                        hplTXYY.Visible = true;
                    }
                    if (username == dr["CLD_CLYJ_TXR"].ToString())
                    {
                        hplTXYJ.Visible = true;
                    }
                    if (username == dr["CLD_CLFA_TXR"].ToString())
                    {
                        hplTXFA.Visible = true;
                    }
                    CLD_SPZT1.Text = "审批中";
                    CLD_SPZT1.BackColor = Color.LightGoldenrodYellow;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                    if (dr["CLD_CLYJ"].ToString() != "" && dr["CLD_CLYJ"].ToString() != null && dr["CLD_CLFA_TXR"].ToString() != "" && dr["CLD_CLFA_TXR"].ToString() != null && username == dr["CLD_SPR2"].ToString())
                    {
                        hplSP.Visible = true;
                    }
                }
                else if (dr["CLD_SPZT"].ToString() == "2y")
                {
                    if (username == dr["CLD_YYFX_TXR"].ToString())
                    {
                        hplTXYY.Visible = true;
                    }
                    if (username == dr["CLD_SPR4"].ToString())
                    {
                        hplSP.Visible = true;
                    }
                    CLD_SPZT1.Text = "审批中";
                    CLD_SPZT1.BackColor = Color.LightGoldenrodYellow;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "4y")
                {
                    if (username == dr["CLD_YYFX_TXR"].ToString())
                    {
                        hplTXYY.Visible = true;
                    }
                    if (username == dr["CLD_SPR5"].ToString())
                    {
                        hplSP.Visible = true;
                    }
                    CLD_SPZT1.Text = "审批中";
                    CLD_SPZT1.BackColor = Color.LightGoldenrodYellow;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "y")
                {
                    if (username == dr["CLD_YYFX_TXR"].ToString())
                    {
                        hplTXYY.Visible = true;
                    }
                    if (dr["CLD_FZBMID"].ToString().Contains('|'))
                    {
                        string[] fzbmid = dr["CLD_FZBMID"].ToString().Split('|');
                        if (fzbmid.Contains(Session["UserDeptID"].ToString()))
                        {
                            hplTXJG.Visible = true;
                        }
                    }
                    else
                    {
                        string fzbmid = dr["CLD_FZBMID"].ToString().Trim();
                        if (fzbmid == Session["UserDeptID"].ToString())
                        {
                            hplTXJG.Visible = true;
                        }
                    }
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
                    if (username == dr["CLD_FWFY_TJR"].ToString())
                    {
                        hplTJ.Visible = true;
                    }
                    if (username == dr["CLD_YYFX_TXR"].ToString())
                    {
                        hplTXYY.Visible = true;
                    }
                    CLD_SPZT1.Text = "已通过";
                    CLD_SPZT1.BackColor = Color.LightGreen;
                    CLD_CLZT.Text = "处理中";
                    CLD_CLZT.BackColor = Color.LightGoldenrodYellow;
                }
                else if (dr["CLD_SPZT"].ToString() == "fytj_y")
                {
                    if (username == dr["CLD_YYFX_TXR"].ToString())
                    {
                        hplTXYY.Visible = true;
                    }
                    if (username == dr["CLD_FWFY_TJR"].ToString())
                    {
                        hplTJ.Visible = true;
                    }
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

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            string CLD_ID = "";
            string CLD_SPZT = "";
            for (int i = 0, length = rptSHFWCLD.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptSHFWCLD.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    CLD_ID = ((HtmlInputHidden)rptSHFWCLD.Items[i].FindControl("CLD_ID")).Value;
                    CLD_SPZT = ((HtmlInputHidden)rptSHFWCLD.Items[i].FindControl("CLD_SPZT")).Value;
                    break;
                }
            }
            if (CLD_SPZT == "0")
            {
                if (CLD_ID == "")
                {
                    Response.Write("<script type='text/javascript'>alert('请勾选一条进行修改！！！')</script>");
                    return;
                }
                else
                {
                    string sql = "delete from CM_SHCLD where CLD_ID='" + CLD_ID + "'";
                    try
                    {
                        DBCallCommon.ExeSqlText(sql);
                    }
                    catch
                    {

                        Response.Write("<script type='text/javascript'>alert('删除语句出现问题，请联系管理员！！！')</script>");
                        return;
                    }
                }
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('您选择的单据已不处于未审批状态，不能删除！！！')</script>");
                return;
            }
            bindrpt();
        }

        protected void btnAlter_OnClick(object sender, EventArgs e)
        {
            string CLD_ID = "";
            string CLD_SPZT = "";
            for (int i = 0, length = rptSHFWCLD.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptSHFWCLD.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    CLD_ID = ((HtmlInputHidden)rptSHFWCLD.Items[i].FindControl("CLD_ID")).Value;
                    CLD_SPZT = ((HtmlInputHidden)rptSHFWCLD.Items[i].FindControl("CLD_SPZT")).Value;
                    break;
                }
            }
            if (CLD_SPZT == "0")
            {
                if (CLD_ID == "")
                {
                    Response.Write("<script type='text/javascript'>alert('请勾选一条进行修改！！！')</script>");
                    return;
                }
                else
                {
                    Response.Redirect("CM_SHCLD.aspx?action=alter&id=" + CLD_ID);
                }
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('您选择的单据已不处于未审批状态，不能修改！！！')</script>");
                return;
            }

        }

        protected void btnRefuse_OnClick(object sender, EventArgs e)
        {
            string CLD_ID = "";
            string CLD_SPZT = "";
            for (int i = 0, length = rptSHFWCLD.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptSHFWCLD.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    CLD_ID = ((HtmlInputHidden)rptSHFWCLD.Items[i].FindControl("CLD_ID")).Value;
                    CLD_SPZT = ((HtmlInputHidden)rptSHFWCLD.Items[i].FindControl("CLD_SPZT")).Value;
                    break;
                }
            }
            if (CLD_ID == "")
            {
                Response.Write("<script type='text/javascript'>alert('请勾选一条进行修改！！！')</script>");
                return;
            }
            else
            {
                string sql1 = "select * from CM_SHCLD where CLD_ID=" + CLD_ID;
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
                List<string> list = new List<string>();
                List<string> listAll = new List<string>();
                for (int i = 0, length = dt.Columns.Count; i < length; i++)
                {
                    listAll.Add(dt.Columns[i].ColumnName.ToString());
                }
                listAll.Remove("CLD_ID");
                listAll.Remove("CLD_SJID");
                listAll.Remove("CLD_BH");
                listAll.Remove("CLD_XMMC");
                listAll.Remove("CLD_HTH");
                listAll.Remove("CLD_SBMC");
                listAll.Remove("CLD_GKMC");
                listAll.Remove("CLD_RWH");
                listAll.Remove("CLD_XXJJ");
                listAll.Remove("CLD_WTMS");
                listAll.Remove("CLD_YZDZ");
                listAll.Remove("CLD_LXR");
                listAll.Remove("CLD_LXFS");
                listAll.Remove("CLD_SJYQ");
                listAll.Remove("CLD_ZDR");
                listAll.Remove("CLD_ZDSJ");
                listAll.Remove("CLD_ZDJY");
                listAll.Remove("CLD_SPR1");
                listAll.Remove("CLD_SPZT");
                string sql = "update CM_SHCLD set ";
                for (int i = 0, length = listAll.Count; i < length; i++)
                {
                    sql += listAll[i] + "= null,";
                }
                sql += " CLD_SPZT='0'";
                sql += " where CLD_ID=" + CLD_ID;
                list.Add(sql);//改主表
                sql = "delete from CM_SHCLD_FY where FY_CLDID='" + CLD_ID + "'";
                list.Add(sql);
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script type='text/javascript'>alert('驳回的sql语句出现问题，请与管理员联系！！！')</script>");
                    return;
                }
            }
            Response.Write("<script type='text/javascript'>alert('您已成功驳回该条记录，该记录将初始化！！！')</script>");
        }

        protected void btnYYFX_OnClick(object sender, EventArgs e)
        {
            string CLD_ID = "";
            string CLD_SPZT = "";
            string CLD_BH = "";
            for (int i = 0, length = rptSHFWCLD.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptSHFWCLD.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    CLD_ID = ((HtmlInputHidden)rptSHFWCLD.Items[i].FindControl("CLD_ID")).Value;
                    CLD_SPZT = ((HtmlInputHidden)rptSHFWCLD.Items[i].FindControl("CLD_SPZT")).Value;
                    CLD_BH = ((Label)rptSHFWCLD.Items[i].FindControl("CLD_BH")).Text.Trim();
                    break;
                }
            }
            if (CLD_ID == "")
            {
                Response.Write("<script type='text/javascript'>alert('请勾选一条进行修改！！！')</script>");
                return;
            }
            else if (CLD_SPZT == "1y")
            {
                string sql = "update CM_SHCLD set CLD_YYFX_TXR='" + hidTXR.Value.Trim() + "',";
                sql += "CLD_YYFX_FGR='" + Session["UserName"].ToString() + "'";
                sql += " where CLD_ID='" + CLD_ID + "'";
                DBCallCommon.ExeSqlText(sql);
                Response.Write("<script type='text/javascript'>alert('您已成功将任务分给了“" + hidTXR.Value + "”！！！')</script>");
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(GetST_ID(hidTXR.Value)), new List<string>(), new List<string>(), "售后质量问题处理通知", "您有售后质量问题处理单“" + CLD_BH + "”需要填写“原因分析”，请登录系统,进入市场管理模块的“售后质量问题处理单页面”进行填写。");
            }
            else
            {
                Response.Write("<script type='text/javascript'>alert('市场部长审批通过后您才能分工！！！')</script>");
                return;
            }

        }

        protected void btnCLYJ_OnClick(object sender, EventArgs e)
        {
            string CLD_ID = "";
            string CLD_SPZT = "";
            string CLD_BH = "";
            for (int i = 0, length = rptSHFWCLD.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptSHFWCLD.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    CLD_ID = ((HtmlInputHidden)rptSHFWCLD.Items[i].FindControl("CLD_ID")).Value;
                    CLD_SPZT = ((HtmlInputHidden)rptSHFWCLD.Items[i].FindControl("CLD_SPZT")).Value;
                    CLD_BH = ((Label)rptSHFWCLD.Items[i].FindControl("CLD_BH")).Text.Trim();
                    break;
                }
            }
            if (CLD_ID == "")
            {
                Response.Write("<script type='text/javascript'>alert('请勾选一条进行修改！！！')</script>");
                return;
            }
            else
            {
                if (CLD_SPZT == "1y")
                {
                    string sql = "update CM_SHCLD set CLD_CLYJ_TXR='" + hidTXR.Value.Trim() + "',";
                    sql += "CLD_CLYJ_FGR='" + Session["UserName"].ToString() + "'";
                    sql += " where CLD_ID='" + CLD_ID + "'";
                    DBCallCommon.ExeSqlText(sql);
                    Response.Write("<script type='text/javascript'>alert('您已成功将任务分给了“" + hidTXR.Value + "”！！！')</script>");
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(GetST_ID(hidTXR.Value)), new List<string>(), new List<string>(), "售后质量问题处理通知", "您有售后质量问题处理单“" + CLD_BH + "”需要填写“处理意见”，请登录系统,进入市场管理模块的“售后质量问题处理单页面”进行填写。");
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('市场部长审批通过后您才能分工！！！')</script>");
                    return;
                }
            }
        }

        protected void btnCLFA_OnClick(object sender, EventArgs e)
        {
            string CLD_ID = "";
            string CLD_SPZT = "";
            string CLD_BH = "";
            for (int i = 0, length = rptSHFWCLD.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptSHFWCLD.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    CLD_ID = ((HtmlInputHidden)rptSHFWCLD.Items[i].FindControl("CLD_ID")).Value;
                    CLD_SPZT = ((HtmlInputHidden)rptSHFWCLD.Items[i].FindControl("CLD_SPZT")).Value;
                    CLD_BH = ((Label)rptSHFWCLD.Items[i].FindControl("CLD_BH")).Text.Trim();
                    break;
                }
            }
            if (CLD_ID == "")
            {
                Response.Write("<script type='text/javascript'>alert('请勾选一条进行修改！！！')</script>");
                return;
            }
            else
            {
                if (CLD_SPZT == "1y")
                {
                    string sql = "update CM_SHCLD set CLD_CLFA_TXR='" + hidTXR.Value.Trim() + "',";
                    sql += "CLD_CLFA_FGR='" + Session["UserName"].ToString() + "'";
                    sql += " where CLD_ID='" + CLD_ID + "'";
                    DBCallCommon.ExeSqlText(sql);
                    Response.Write("<script type='text/javascript'>alert('您已成功将任务分给了“" + hidTXR.Value + "”！！！')</script>");
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(GetST_ID(hidTXR.Value)), new List<string>(), new List<string>(), "售后质量问题处理通知", "您有售后质量问题处理单“" + CLD_BH + "”需要填写“处理方案”，请登录系统,进入市场管理模块的“售后质量问题处理单页面”进行填写。");
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('市场部长审批通过后您才能分工！！！')</script>");
                    return;
                }
            }
        }

        protected void btnFYTJ_OnClick(object sender, EventArgs e)
        {
            string CLD_ID = "";
            string CLD_SPZT = "";
            string CLD_BH = "";
            for (int i = 0, length = rptSHFWCLD.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptSHFWCLD.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    CLD_ID = ((HtmlInputHidden)rptSHFWCLD.Items[i].FindControl("CLD_ID")).Value;
                    CLD_SPZT = ((HtmlInputHidden)rptSHFWCLD.Items[i].FindControl("CLD_SPZT")).Value;
                    CLD_BH = ((Label)rptSHFWCLD.Items[i].FindControl("CLD_BH")).Text.Trim();
                    break;
                }
            }
            if (CLD_ID == "")
            {
                Response.Write("<script type='text/javascript'>alert('请勾选一条进行修改！！！')</script>");
                return;
            }
            else
            {
                if (CLD_SPZT == "cljg_y")
                {
                    string sql = "update CM_SHCLD set CLD_FWFY_TJR='" + hidTXR.Value.Trim() + "',";
                    sql += "CLD_FWFY_FGR='" + Session["UserName"].ToString() + "'";
                    sql += " where CLD_ID='" + CLD_ID + "'";
                    DBCallCommon.ExeSqlText(sql);
                    Response.Write("<script type='text/javascript'>alert('您已成功将任务分给了“" + hidTXR.Value + "”！！！')</script>");
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(GetST_ID(hidTXR.Value)), new List<string>(), new List<string>(), "售后质量问题处理通知", "您有售后质量问题处理单“" + CLD_BH + "”需要填写“费用统计”，请登录系统,进入市场管理模块的“售后质量问题处理单页面”进行填写。");
                }
                else
                {
                    Response.Write("<script type='text/javascript'>alert('处理结果填写后您才能分工！！！')</script>");
                    return;
                }
            }
        }

        private string GetST_ID(string ST_NAME)
        {
            string st_id = "";
            string sql = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_NAME='" + ST_NAME + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            st_id = dt.Rows[0]["ST_ID"].ToString();
            return st_id;
        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }

        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("CM_SHCLD.aspx?action=add");
        }

        #region 导出
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDaoChu_onclick(object sender, EventArgs e)
        {
            string sql = "select CLD_GKMC,CLD_BH,CLD_HTH,CLD_RWH,CLD_XMMC,CLD_SBMC,CLD_XXJJ,CLD_CLJG,CLD_FWZFY,CLD_ZDR,CLD_ZDSJ from CM_SHCLD ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ExportDataItem(dt);
        }

        private void ExportDataItem(DataTable dt)
        {
            string filename = "售后质量问题处理" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("售后质量问题处理.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                font1.FontName = "仿宋";//字体
                font1.FontHeightInPoints = 9;//字号
                ICellStyle cells = wk.CreateCellStyle();
                cells.SetFont(font1);
                cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;

                for (int i = 0, length = dt.Rows.Count; i < length; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2);
                    row.CreateCell(0).SetCellValue(i + 1);
                    row.Cells[0].CellStyle = cells;
                    for (int j = 0, m = dt.Columns.Count; j < m; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                        row.Cells[j + 1].CellStyle = cells;
                    }
                }

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        #endregion
    }
}
