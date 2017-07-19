using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using CrystalDecisions.Shared;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_CGHTGL : BasicPage
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
            PowerControl();
            CheckUser(ControlFinder);
        }

        private void BindrblSP()
        {
            string sql = "";
            DataTable dt = new DataTable();
            sql = "select count (HT_ID) from PC_CGHT where HT_SPZT is not null";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            rblSP.Items[0].Text = string.Format("全部合同（<font color='red'>{0}</font>）", dt.Rows[0][0].ToString());

            sql = "select count (HT_ID) from PC_CGHT where HT_SPZT ='0'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            rblSP.Items[1].Text = string.Format("待审批（<font color='red'>{0}</font>）", dt.Rows[0][0].ToString());

            sql = "select count (HT_ID) from PC_CGHT where HT_SPZT in ('1y','2y','5y')";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            rblSP.Items[2].Text = string.Format("审批中（<font color='red'>{0}</font>）", dt.Rows[0][0].ToString());


            sql = "select count (HT_ID) from PC_CGHT where HT_SPZT ='y'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            rblSP.Items[3].Text = string.Format("已通过（<font color='red'>{0}</font>）", dt.Rows[0][0].ToString());

            sql = "select count (HT_ID) from PC_CGHT where HT_SPZT ='n'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            rblSP.Items[4].Text = string.Format("已驳回（<font color='red'>{0}</font>）", dt.Rows[0][0].ToString());

            sql = string.Format("select count (HT_ID) from PC_CGHT where (HT_SHR1='{0}' and (HT_SHR1_JL is null or HT_SHR1_JL='') and HT_SPZT='0') or (HT_SHR2='{0}' and (HT_SHR2_JL='' or HT_SHR2_JL is null) and HT_SPZT='1y') or (HT_SHR3='{0}' and (HT_SHR3_JL='' or HT_SHR3_JL is null) and HT_SPZT='2y') or (HT_SHRCG='{0}' and (HT_SHRCG_JL is null or HT_SHRCG_JL='') and HT_SPZT='0') or (HT_SHRShenC='{0}' and (HT_SHRShenC_JL is null or HT_SHRShenC_JL='') and HT_SPZT='0') or (HT_SHRJS='{0}' and (HT_SHRJS_JL='' or HT_SHRJS_JL is null) and HT_SPZT='0') or (HT_SHRZ='{0}' and (HT_SHRZ_JL='' or HT_SHRZ_JL is null) and HT_SPZT='0') or (HT_SHRShiC ='{0}' and (HT_SHRShiC_JL is null or HT_SHRShiC_JL='') and  HT_SPZT='0') or (HT_SHRCW='{0}' and (HT_SHRCW_JL='' or HT_SHRCW_JL is null) and HT_SPZT='0') or (HT_SHRFZ='{0}' and (HT_SHRFZ_JL is null or HT_SHRFZ_JL ='') and HT_SPZT='5y')", username);//质量
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            rblSP.Items[5].Text = string.Format("我的审批任务（<font color='red'>{0}</font>）", dt.Rows[0][0].ToString());

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
                        string[] a = dr["HT_HTZJ"].ToString().Split(new char[] { '(', ')' }, StringSplitOptions.None);
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
            BindrblSP();
        }

        private string StrWhere()
        {
            string sql = "0=0";

            //if (rblSX.SelectedValue == "1")
            //{
            //    sql += " and HT_ZDR is not null ";
            //}
            //else if (rblSX.SelectedValue == "2")
            //{
            //    sql += " and (HT_ZDR ='" + username + "'";
            //    sql += " or HT_SHR1='" + username + "'";
            //    sql += " or HT_SHR2='" + username + "'";
            //    sql += " or HT_SHR3='" + username + "'";
            //    sql += " or HT_SHRCG='" + username + "'";
            //    sql += " or HT_SHRShenC='" + username + "'";
            //    sql += " or HT_SHRJS='" + username + "'";
            //    sql += " or HT_SHRShiC='" + username + "'";
            //    sql += " or HT_SHRCW='" + username + "'";
            //    sql += " or HT_SHRFZ='" + username + "'";
            //    sql += " or HT_SHRZ='" + username + "')";
            //}


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
            else if (rblSP.SelectedValue == "5")//我的审批任务
            {
                sql += string.Format(" and ((HT_SHR1='{0}' and (HT_SHR1_JL is null or HT_SHR1_JL='') and HT_SPZT='0') or (HT_SHR2='{0}' and (HT_SHR2_JL='' or HT_SHR2_JL is null) and HT_SPZT='1y') or (HT_SHR3='{0}' and (HT_SHR3_JL='' or HT_SHR3_JL is null) and HT_SPZT='2y') or (HT_SHRCG='{0}' and (HT_SHRCG_JL is null or HT_SHRCG_JL='') and HT_SPZT='0') or (HT_SHRShenC='{0}' and (HT_SHRShenC_JL is null or HT_SHRShenC_JL='') and HT_SPZT='0') or (HT_SHRJS='{0}' and (HT_SHRJS_JL='' or HT_SHRJS_JL is null) and HT_SPZT='0') or (HT_SHRZ='{0}' and (HT_SHRZ_JL='' or HT_SHRZ_JL is null) and HT_SPZT='0') or (HT_SHRShiC ='{0}' and (HT_SHRShiC_JL is null or HT_SHRShiC_JL='') and  HT_SPZT='0') or (HT_SHRCW='{0}' and (HT_SHRCW_JL='' or HT_SHRCW_JL is null) and HT_SPZT='0') or (HT_SHRFZ='{0}' and (HT_SHRFZ_JL is null or HT_SHRFZ_JL ='') and HT_SPZT='5y'))", username);
            }
            if (rblCL.SelectedValue == "n")
            {
                sql += " and HT_CLZT is null";
            }
            else if (rblCL.SelectedValue == "y")
            {
                sql += " and HT_CLZT='y'";
            }
            if (rblJC.SelectedValue == "n")
            {
                sql += " and HT_JCZT is null";
            }
            else if (rblJC.SelectedValue == "y")
            {
                sql += " and HT_JCZT='y'";
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
            return sql;
        }
        #endregion

        private void PowerControl()
        {
            if (Session["POSITION"].ToString() == "0501" || Session["UserID"].ToString() == "52" || Session["POSITION"].ToString() == "0502" || Session["POSITION"].ToString() == "0504")
            {
                btnChuLi.Visible = true;
                btnJiCai.Visible = true;
                btnCancel.Visible = true;
            }
            else
            {
                btnChuLi.Visible = false;
                btnJiCai.Visible = false;
                btnCancel.Visible = false;
            }
        }

        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("PC_CGHT.aspx?action=add");
        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }

        protected void btnChuLi_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0, length = rptCGHTGL.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptCGHTGL.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    string HT_SPZT = ((HiddenField)rptCGHTGL.Items[i].FindControl("HT_SPZT")).Value;
                    if (HT_SPZT != "y")
                    {
                        Response.Write("<script>alert('您所勾选的合同中存在还未审批通过的合同，请勾选已经审批通过的合同！！！')</script>");
                        return;
                    }
                    string HT_ID = ((Label)rptCGHTGL.Items[i].FindControl("lbHT_ID")).Text;
                    string sql = "update PC_CGHT set HT_CLZT ='y' where HT_ID=" + HT_ID;
                    list.Add(sql);
                }
            }
            if (list.Count == 0)
            {
                Response.Write("<script>alert('请至少勾选一条合同！！！')</script>");
                return;
            }
            try
            {
                DBCallCommon.ExecuteTrans(list);
            }
            catch
            {
                Response.Write("<script>alert('处理过程出现问题，请联系管理员！！！')</script>");
                return;
            }
            bindrpt();
        }

        protected void btnJiCai_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0, length = rptCGHTGL.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptCGHTGL.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    string HT_SPZT = ((HiddenField)rptCGHTGL.Items[i].FindControl("HT_SPZT")).Value;
                    //if (HT_SPZT != "y")
                    //{
                    //    Response.Write("<script>alert('您所勾选的合同中存在还未审批通过的合同，请勾选已经审批通过的合同！！！')</script>");
                    //    return;
                    //}
                    string HT_ID = ((Label)rptCGHTGL.Items[i].FindControl("lbHT_ID")).Text;
                    string sql = "update PC_CGHT set HT_JCZT ='y' where HT_ID=" + HT_ID;
                    list.Add(sql);
                }
            }
            if (list.Count == 0)
            {
                Response.Write("<script>alert('请至少勾选一条合同！！！')</script>");
                return;
            }
            try
            {
                DBCallCommon.ExecuteTrans(list);
            }
            catch
            {
                Response.Write("<script>alert('处理过程出现问题，请联系管理员！！！')</script>");
                return;
            }
            bindrpt();
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            for (int i = 0, length = rptCGHTGL.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptCGHTGL.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    string HT_SPZT = ((HiddenField)rptCGHTGL.Items[i].FindControl("HT_SPZT")).Value;
                    //if (HT_SPZT != "y")
                    //{
                    //    Response.Write("<script>alert('您所勾选的合同中存在还未审批通过的合同，请勾选已经审批通过的合同！！！')</script>");
                    //    return;
                    //}
                    string HT_ID = ((Label)rptCGHTGL.Items[i].FindControl("lbHT_ID")).Text;
                    string sql = "update PC_CGHT set HT_JCZT =null where HT_ID=" + HT_ID;
                    list.Add(sql);
                }
            }
            if (list.Count == 0)
            {
                Response.Write("<script>alert('请至少勾选一条合同！！！')</script>");
                return;
            }
            try
            {
                DBCallCommon.ExecuteTrans(list);
            }
            catch
            {
                Response.Write("<script>alert('处理过程出现问题，请联系管理员！！！')</script>");
                return;
            }
            bindrpt();
        }

        protected void rptCGHTGL_OnItemDataBound(object sender, RepeaterItemEventArgs e)//控制审批权限
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //string sql = "select * from PC_CGHT where HT_ID='" + ((Label)e.Item.FindControl("lbHT_ID")).Text + "'";
                //DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                //DataRow dr = dt.Rows[0];
                DataRowView dr = (DataRowView)e.Item.DataItem;
                HyperLink hplXiuGai = (HyperLink)e.Item.FindControl("hplXiuGai");
                LinkButton lbtnDelete = (LinkButton)e.Item.FindControl("lbtnDelete");
                HyperLink hplCheck = (HyperLink)e.Item.FindControl("hplCheck");
                LinkButton lbtnBackCheck = (LinkButton)e.Item.FindControl("lbtnBackCheck");
                HtmlTableCell HT_XFHTBH = (HtmlTableCell)e.Item.FindControl("HT_XFHTBH");
                HtmlTableCell tdJC = (HtmlTableCell)e.Item.FindControl("tdJC");
                hplCheck.Visible = false;
                hplXiuGai.Visible = false;
                lbtnBackCheck.Visible = false;
                lbtnDelete.Visible = false;
                if (dr["HT_CLZT"].ToString() == "y")
                {
                    HT_XFHTBH.BgColor = System.Drawing.Color.LightBlue.ToString();
                }
                if (dr["HT_JCZT"].ToString() == "y")
                {
                    //tdJC.BgColor = System.Drawing.Color.Green.ToString();
                }
                if (dr["HT_SPZT"].ToString() == "0")
                {
                    if (username == dr["HT_SHR1"].ToString())
                    {
                        hplCheck.Visible = true;
                    }
                    if (username == dr["HT_ZDR"].ToString())
                    {
                        hplXiuGai.Visible = true;
                        lbtnDelete.Visible = true;
                    }
                    if (username == dr["HT_SHRCG"].ToString() && dr["HT_SHRCG_JL"].ToString() == "")
                    {
                        hplCheck.Visible = true;
                    }
                    else if (username == dr["HT_SHRShenC"].ToString() && dr["HT_SHRShenC_JL"].ToString() == "")
                    {
                        hplCheck.Visible = true;
                    }
                    else if (username == dr["HT_SHRJS"].ToString() && dr["HT_SHRJS_JL"].ToString() == "")
                    {
                        hplCheck.Visible = true;
                    }
                    else if (username == dr["HT_SHRZ"].ToString() && dr["HT_SHRZ_JL"].ToString() == "")
                    {
                        hplCheck.Visible = true;
                    }
                    else if (username == dr["HT_SHRShiC"].ToString() && dr["HT_SHRShiC_JL"].ToString() == "")
                    {
                        hplCheck.Visible = true;
                    }
                    else if (username == dr["HT_SHRCW"].ToString() && dr["HT_SHRCW_JL"].ToString() == "")
                    {
                        hplCheck.Visible = true;
                    }
                }
                else if (dr["HT_SPZT"].ToString() == "1y")
                {
                    if (username == dr["HT_SHR2"].ToString())
                    {
                        hplCheck.Visible = true;
                    }
                }
                else if (dr["HT_SPZT"].ToString() == "2y")
                {
                    if (username == dr["HT_SHR3"].ToString())
                    {
                        hplCheck.Visible = true;
                    }
                }
                else if (dr["HT_SPZT"].ToString() == "5y")
                {
                    if (username == dr["HT_SHRFZ"].ToString())
                    {
                        hplCheck.Visible = true;
                    }
                }
                if (Session["POSITION"].ToString().Trim() == "0501")
                {
                    lbtnBackCheck.Visible = true;
                }
                if (Session["POSITION"].ToString().Trim() != "0501" && Session["POSITION"].ToString().Trim() != "0101" && Session["POSITION"].ToString().Trim() != "0102"&& username != dr["HT_ZDR"].ToString() && username != "李洪清")
                {
                    e.Item.FindControl("lbHT_HTZJ").Visible = false;
                }
            }
        }

        protected void lbtnDelete_OnClick(object sender, EventArgs e)//删除事件
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            string sql = "delete from PC_CGHT where HT_ID='" + id + "'";
            DBCallCommon.ExeSqlText(sql);
            Response.Write("<script>alert('该合同已被成功删除')</script>");
            bindrpt();
        }

        protected void lbtnBackCheck_OnClick(object sender, EventArgs e)//返审事件
        {
            string sqltext = "";
            string sql = "update PC_CGHT set  ";
            string id = ((LinkButton)sender).CommandArgument.ToString();
            sqltext = "select * from PC_CGHT where HT_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["HT_SPLX"].ToString() == "1")
                {
                    sql += "HT_SHR1_JL=null,HT_SHR1_SJ=null,HT_SHR1_JY=null,";
                }
                if (dt.Rows[0]["HT_SPLX"].ToString() == "2")
                {
                    sql += "HT_SHR1_JL=null,HT_SHR1_SJ=null,HT_SHR1_JY=null,";
                    sql += "HT_SHR2_JL=null,HT_SHR2_SJ=null,HT_SHR2_JY=null,";
                }
                if (dt.Rows[0]["HT_SPLX"].ToString() == "3")
                {
                    sql += "HT_SHR1_JL=null,HT_SHR1_SJ=null,HT_SHR1_JY=null,";
                    sql += "HT_SHR2_JL=null,HT_SHR2_SJ=null,HT_SHR2_JY=null,";
                    sql += "HT_SHR3_JL=null,HT_SHR3_SJ=null,HT_SHR3_JY=null,";
                }
                if (dt.Rows[0]["HT_SPLX"].ToString() == "4")
                {
                    sql += "HT_SHRCG_JL=null,HT_SHRCG_SJ=null,HT_SHRCG_JY=null,";
                    sql += "HT_SHRShenC_JL=null,HT_SHRShenC_SJ=null,HT_SHRShenC_JY=null,";
                    sql += "HT_SHRJS_JL =null,HT_SHRJS_SJ=null,HT_SHRJS_JY=null,";
                    sql += "HT_SHRShiC_JL =null,HT_SHRShiC_SJ=null,HT_SHRShiC_JY=null,";
                    sql += "HT_SHRCW_JL=null,HT_SHRCW_SJ=null,HT_SHRCW_JY=null,";
                    sql += "HT_SHRFZ_JL =null,HT_SHRFZ_SJ=null,HT_SHRFZ_JY=null,";
                    sql += "HT_SHRZ_JL =null,HT_SHRZ_SJ=null,HT_SHRZ_JY=null,";
                }
            }
            sql += "HT_SPZT='0'";
            sql += " where HT_ID='" + id + "'";
            DBCallCommon.ExeSqlText(sql);
            Response.Write("<script>alert('该合同已被成功返审')</script>");
            bindrpt();
        }


        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDaoChu_OnClick(object sender, EventArgs e)
        {
            string ht_id = "";
            string sql = "";
            for (int i = 0, length = rptCGHTGL.Items.Count; i < length; i++)
            {
                if (((CheckBox)rptCGHTGL.Items[i].FindControl("cbxXuHao")).Checked == true)
                {
                    ht_id = ((Label)rptCGHTGL.Items[i].FindControl("lbHT_ID")).Text;
                    sql = " select * from PC_CGHT where HT_ID='" + ht_id + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        ExportDataItem(dt);
                    }
                    else
                    {
                        Response.Write("<script>alert('该合同数据为空，不能打印！！！')</script>");
                        return;
                    }
                    break;
                }
            }
        }

        private void ExportDataItem(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            string sql = "";
            string[] ddbh = dr["HT_DDBH"].ToString().Split('|');
            sql = "select orderno,zdrnm,zdtime,PO_ZJE,marid,marnm,suppliernm,";
            sql += "PO_TUHAO= stuff((select '|'+PO_TUHAO from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as t where t.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and t.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno FOR xml path('')),1,1,''),";
            sql += "margg,marcz,margb,PO_MASHAPE,sum(convert(float,zxnum)) as zxnum,marunit,sum(convert(float,fznum))as fznum,PO_TECUNIT,";
            sql += "stuff((select '|'+convert(varchar(50),length) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a where a.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and a.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno FOR xml path('')),1,1,'') as length,";
            sql += "stuff((select '|'+convert(varchar(50),width) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as b where b.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and b.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno FOR xml path('')),1,1,'') as width,";
            sql += "stuff((select '|'+convert(varchar(50),PO_PZ) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as c where c.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and c.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno FOR xml path('')),1,1,'') as PO_PZ,";
            sql += "ctprice,sum(convert(float,ctamount))as ctamount,cgtimerq,";
            sql += "detailnote =stuff((select '|'+detailnote from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as d where d.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and d.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno FOR xml path('')),1,1,''),";
            sql += "ptcode=stuff((select ' | '+ptcode from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as e where e.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and e.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno FOR xml path('')),1,1,'')";
            sql += "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where orderno in (";
            //sql = "select * from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where orderno in (";
            for (int i = 0, length = ddbh.Length; i < length; i++)
            {
                sql += "'" + ddbh[i] + "',";
            }
            sql = sql.Trim(',');
            //sql += ")";
            sql += ") group by orderno,zdrnm,zdtime,suppliernm,PO_ZJE,marid,marnm,margg,marcz,margb,PO_MASHAPE,marunit,PO_TECUNIT,ctprice,cgtimerq order by ptcode";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);

            //string filename = "采购合同" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            string filename = "采购合同--" + dr["HT_GF"].ToString() + dr["HT_QDSJ"].ToString() + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("采购合同.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);

                #region //*********************导出到采购合同条款***************************//
                ISheet sheet0 = wk.GetSheetAt(0);
                IRow row1 = sheet0.GetRow(1);//买方合同号编号
                row1.GetCell(3).SetCellValue(dr["HT_XFHTBH"].ToString());

                IRow row2 = sheet0.GetRow(2);//卖方合同号编号
                row2.GetCell(3).SetCellValue(dr["HT_GFHTBH"].ToString());

                IRow row3 = sheet0.GetRow(3);//签订时间
                row3.GetCell(3).SetCellValue(dr["HT_QDSJ"].ToString().Substring(0, 4) + "年" + dr["HT_QDSJ"].ToString().Substring(5, 2) + "月" + dr["HT_QDSJ"].ToString().Substring(8, 2) + "日");

                IRow row4 = sheet0.GetRow(4);
                row4.GetCell(1).SetCellValue(dr["HT_GF"].ToString());//卖方（供方）

                IRow row7 = sheet0.GetRow(7);
                row7.GetCell(1).SetCellValue(dr["HT_HTZJ"].ToString());//合同总价

                IRow row22 = sheet0.GetRow(22);
                IRow row23 = sheet0.GetRow(23);
                IRow row24 = sheet0.GetRow(24);
                IRow row25 = sheet0.GetRow(25);
                IRow row26 = sheet0.GetRow(26);
                IRow row27 = sheet0.GetRow(27);
                if (dr["HT_JSFS"].ToString() == "1")//付款方式
                {
                    row22.GetCell(0).SetCellValue("货到付款型");
                    row23.GetCell(0).SetCellValue("备注：" + dr["HT_JSFSBZ"].ToString());
                }
                if (dr["HT_JSFS"].ToString() == "2")
                {
                    row22.GetCell(0).SetCellValue("款到发货型");
                    row23.GetCell(0).SetCellValue("备注：" + dr["HT_JSFSBZ"].ToString());
                }
                if (dr["HT_JSFS1BZ"].ToString() != "")
                {
                    row24.GetCell(0).SetCellValue(dr["HT_JSFS1BZ"].ToString());
                }
                else
                {
                    row24.HeightInPoints = 0;
                }
                if (dr["HT_JSFS2BZ"].ToString() != "")
                {
                    row25.GetCell(0).SetCellValue(dr["HT_JSFS2BZ"].ToString());
                }
                else
                {
                    row25.HeightInPoints = 0;
                }
                if (dr["HT_JSFS3"].ToString() != "")
                {
                    row26.GetCell(0).SetCellValue(dr["HT_JSFS3"].ToString());
                }
                else
                {
                    row26.HeightInPoints = 0;
                }

                row27.HeightInPoints = 0;

                IRow row52 = sheet0.GetRow(52);
                row52.GetCell(3).SetCellValue(dr["HT_GF"].ToString());//单位名称

                IRow row53 = sheet0.GetRow(53);
                row53.GetCell(3).SetCellValue(dr["HT_DZ"].ToString());//单位地址

                IRow row54 = sheet0.GetRow(54);
                row54.GetCell(3).SetCellValue(dr["HT_FDDBR"].ToString());//法定代表人

                IRow row55 = sheet0.GetRow(55);
                row55.GetCell(3).SetCellValue(dr["HT_WTDLR"].ToString());//委托代理人

                IRow row56 = sheet0.GetRow(56);
                row56.GetCell(3).SetCellValue(dr["HT_DH"].ToString());//电话

                IRow row57 = sheet0.GetRow(57);
                row57.GetCell(3).SetCellValue(dr["HT_CZ"].ToString());//传真

                IRow row58 = sheet0.GetRow(58);
                row58.GetCell(3).SetCellValue(dr["HT_KHYH"].ToString());//开户银行

                IRow row59 = sheet0.GetRow(59);
                row59.GetCell(3).SetCellValue(dr["HT_ZH"].ToString());//账号

                IRow row60 = sheet0.GetRow(60);
                row60.GetCell(3).SetCellValue(dr["HT_SH"].ToString());//税号

                IRow row61 = sheet0.GetRow(61);
                row61.GetCell(3).SetCellValue(dr["HT_YB"].ToString());//邮编

                sheet0.ForceFormulaRecalculation = true;
                #endregion

                #region //******************导出到采购合同附件***********************//

                //int[] a = new int[dt1.Rows.Count];
                //for (int i = 1,length=dt1.Rows.Count; i < length; i++)
                //{
                //    if (dt1.Rows[i]["orderno"].ToString()!=dt1.Rows[i-1]["orderno"].ToString())
                //    {
                //        a[i] = i;
                //    }
                //}
                int leixing = 0;
                for (int i = 0, length = dt1.Rows.Count; i < length; i++)
                {
                    if (dt1.Rows[i]["PO_MASHAPE"].ToString().Contains("板") || dt1.Rows[i]["PO_MASHAPE"].ToString().Contains("圆") || dt1.Rows[i]["PO_MASHAPE"].ToString().Contains("型"))
                    {
                        leixing = 1;
                    }
                }
                if (leixing == 0)//非钢材类
                {
                    ISheet sheet1 = wk.GetSheetAt(1);
                    IRow rowddbh = sheet1.GetRow(1);
                    rowddbh.GetCell(0).SetCellValue("订单编号:" + dr["HT_DDBH"].ToString());
                    for (int i = 0, length = dt1.Rows.Count; i < length; i++)
                    {
                        IRow row = sheet1.CreateRow(i + 3);
                        row.HeightInPoints = 14;

                        row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号
                        //row.CreateCell(1).SetCellValue(dt1.Rows[i]["orderno"].ToString());//计划单号
                        row.CreateCell(1).SetCellValue(dt1.Rows[i]["marid"].ToString());//物料编码
                        row.CreateCell(2).SetCellValue(dt1.Rows[i]["marnm"].ToString());//物料名称
                        row.CreateCell(3).SetCellValue(dt1.Rows[i]["PO_TUHAO"].ToString());//图号
                        row.CreateCell(4).SetCellValue(dt1.Rows[i]["margg"].ToString());//规格
                        row.CreateCell(5).SetCellValue(dt1.Rows[i]["marcz"].ToString());//材质
                        row.CreateCell(6).SetCellValue(dt1.Rows[i]["margb"].ToString());//国标
                        row.CreateCell(7).SetCellValue(dt1.Rows[i]["PO_MASHAPE"].ToString());//类型
                        row.CreateCell(8).SetCellValue(dt1.Rows[i]["zxnum"].ToString());//数（重）量
                        row.CreateCell(9).SetCellValue(dt1.Rows[i]["marunit"].ToString());//单位
                        row.CreateCell(10).SetCellValue(dt1.Rows[i]["fznum"].ToString());//辅助数量
                        row.CreateCell(11).SetCellValue(dt1.Rows[i]["PO_TECUNIT"].ToString());//辅助单位
                        //row.CreateCell(13).SetCellValue(dt1.Rows[i]["length"].ToString());//长度
                        //row.CreateCell(14).SetCellValue(dt1.Rows[i]["width"].ToString());//宽度
                        //row.CreateCell(15).SetCellValue(dt1.Rows[i]["PO_PZ"].ToString());//片支
                        row.CreateCell(12).SetCellValue(dt1.Rows[i]["ctprice"].ToString());//含税单价
                        row.CreateCell(13).SetCellValue(dt1.Rows[i]["ctamount"].ToString());//含税金额
                        row.CreateCell(14).SetCellValue(dt1.Rows[i]["cgtimerq"].ToString());//交货日期
                        row.CreateCell(15).SetCellValue(dt1.Rows[i]["detailnote"].ToString());//备注
                        row.CreateCell(16).SetCellValue(dt1.Rows[i]["ptcode"].ToString());//计划跟踪号

                        NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                        font1.FontName = "仿宋";//字体
                        font1.FontHeightInPoints = 9;//字号
                        ICellStyle cells = wk.CreateCellStyle();
                        cells.SetFont(font1);
                        cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                        cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                        cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                        cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                        for (int j = 0; j <= 16; j++)
                        {
                            row.Cells[j].CellStyle = cells;
                        }
                    }
                    double number = 0;
                    double money = 0;
                    for (int i = 0, length = dt1.Rows.Count; i < length; i++)
                    {
                        number += Convert.ToDouble(dt1.Rows[i]["zxnum"].ToString());
                        money += Convert.ToDouble(dt1.Rows[i]["ctamount"].ToString());
                    }

                    IRow rowhz = sheet1.CreateRow(dt1.Rows.Count + 3);
                    for (int i = 0; i <= 16; i++)
                    {
                        rowhz.CreateCell(i);
                    }
                    rowhz.GetCell(0).SetCellValue("合计");
                    rowhz.GetCell(8).SetCellValue(number);
                    rowhz.GetCell(13).SetCellValue(money);

                    NPOI.SS.UserModel.IFont font2 = wk.CreateFont();
                    font2.FontName = "仿宋";//字体
                    font2.FontHeightInPoints = 9;//字号
                    ICellStyle cells2 = wk.CreateCellStyle();
                    cells2.SetFont(font2);
                    cells2.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells2.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells2.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells2.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells2.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                    cells2.VerticalAlignment = VerticalAlignment.CENTER;
                    for (int i = 0; i <= 16; i++)
                    {
                        rowhz.Cells[i].CellStyle = cells2;
                    }

                    IRow rowbzts = sheet1.CreateRow(dt1.Rows.Count + 4);
                    rowbzts.CreateCell(0).SetCellValue("备注：");
                    rowbzts.Cells[0].CellStyle = cells2;

                    IRow rowbz = sheet1.CreateRow(dt1.Rows.Count + 5);
                    for (int i = 0; i <= 16; i++)
                    {
                        rowbz.CreateCell(i);
                    }
                    rowbz.GetCell(0).SetCellValue(dt.Rows[0]["HT_DDBZ"].ToString());
                    for (int i = 0; i <= 16; i++)
                    {
                        rowbz.Cells[i].CellStyle = cells2;
                    }

                    //ICellStyle cellstyle = wk.CreateCellStyle();
                    //cellstyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                    //cellstyle.VerticalAlignment = VerticalAlignment.CENTER;

                    //for (int i = 0; i <= 16; i++)
                    //{
                    //    rowhz.Cells[i].CellStyle = cellstyle;
                    //}

                    CellRangeAddress range1 = new CellRangeAddress(dt1.Rows.Count + 3, dt1.Rows.Count + 3, 0, 7);
                    sheet1.AddMergedRegion(range1);
                    CellRangeAddress range2 = new CellRangeAddress(dt1.Rows.Count + 3, dt1.Rows.Count + 3, 9, 12);
                    sheet1.AddMergedRegion(range2);
                    CellRangeAddress range3 = new CellRangeAddress(dt1.Rows.Count + 3, dt1.Rows.Count + 3, 14, 16);
                    sheet1.AddMergedRegion(range3);

                    CellRangeAddress range4 = new CellRangeAddress(dt1.Rows.Count + 5, dt1.Rows.Count + 9, 0, 16);
                    sheet1.AddMergedRegion(range4);

                    for (int i = 0; i <= 16; i++)
                    {
                        sheet1.AutoSizeColumn(i);
                    }
                    sheet1.ForceFormulaRecalculation = true;
                }
                if (leixing == 1)
                {
                    ISheet sheet2 = wk.GetSheetAt(2);
                    IRow rowddbh1 = sheet2.GetRow(1);
                    rowddbh1.GetCell(0).SetCellValue("订单编号：" + dr["HT_DDBH"].ToString());
                    for (int i = 0, length = dt1.Rows.Count; i < length; i++)
                    {
                        IRow row = sheet2.CreateRow(i + 3);
                        row.HeightInPoints = 14;

                        row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号
                        //row.CreateCell(1).SetCellValue(dt1.Rows[i]["orderno"].ToString());//计划单号
                        row.CreateCell(1).SetCellValue(dt1.Rows[i]["marid"].ToString());//物料编码
                        row.CreateCell(2).SetCellValue(dt1.Rows[i]["marnm"].ToString());//物料名称
                        row.CreateCell(3).SetCellValue(dt1.Rows[i]["PO_TUHAO"].ToString());//图号
                        row.CreateCell(4).SetCellValue(dt1.Rows[i]["margg"].ToString());//规格
                        row.CreateCell(5).SetCellValue(dt1.Rows[i]["marcz"].ToString());//材质
                        row.CreateCell(6).SetCellValue(dt1.Rows[i]["margb"].ToString());//国标
                        row.CreateCell(7).SetCellValue(dt1.Rows[i]["PO_MASHAPE"].ToString());//类型
                        row.CreateCell(8).SetCellValue(dt1.Rows[i]["zxnum"].ToString());//数（重）量
                        row.CreateCell(9).SetCellValue(dt1.Rows[i]["marunit"].ToString());//单位
                        //row.CreateCell(11).SetCellValue(dt1.Rows[i]["fznum"].ToString());//辅助数量
                        //row.CreateCell(12).SetCellValue(dt1.Rows[i]["marfzunit"].ToString());//辅助单位
                        row.CreateCell(10).SetCellValue(dt1.Rows[i]["length"].ToString());//长度
                        row.CreateCell(11).SetCellValue(dt1.Rows[i]["width"].ToString());//宽度
                        row.CreateCell(12).SetCellValue(dt1.Rows[i]["PO_PZ"].ToString());//片支
                        row.CreateCell(13).SetCellValue(dt1.Rows[i]["ctprice"].ToString());//含税单价
                        row.CreateCell(14).SetCellValue(dt1.Rows[i]["ctamount"].ToString());//含税金额
                        row.CreateCell(15).SetCellValue(dt1.Rows[i]["cgtimerq"].ToString());//交货日期
                        row.CreateCell(16).SetCellValue(dt1.Rows[i]["detailnote"].ToString());//备注
                        row.CreateCell(17).SetCellValue(dt1.Rows[i]["ptcode"].ToString());//计划跟踪号


                        NPOI.SS.UserModel.IFont font1 = wk.CreateFont();
                        font1.FontName = "仿宋";//字体
                        font1.FontHeightInPoints = 9;//字号
                        ICellStyle cells = wk.CreateCellStyle();
                        cells.SetFont(font1);
                        cells.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                        cells.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                        cells.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                        cells.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                        for (int j = 0; j <= 17; j++)
                        {
                            row.Cells[j].CellStyle = cells;
                        }
                    }
                    double number = 0;
                    double money = 0;
                    for (int i = 0, length = dt1.Rows.Count; i < length; i++)
                    {
                        number += Convert.ToDouble(dt1.Rows[i]["zxnum"].ToString());
                        money += Convert.ToDouble(dt1.Rows[i]["ctamount"].ToString());
                    }

                    IRow rowhz = sheet2.CreateRow(dt1.Rows.Count + 3);

                    NPOI.SS.UserModel.IFont font2 = wk.CreateFont();
                    font2.FontName = "仿宋";//字体
                    font2.FontHeightInPoints = 9;//字号
                    ICellStyle cells2 = wk.CreateCellStyle();
                    cells2.SetFont(font2);
                    cells2.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells2.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells2.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells2.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                    cells2.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                    cells2.VerticalAlignment = VerticalAlignment.CENTER;
                    for (int i = 0; i < 18; i++)
                    {
                        rowhz.CreateCell(i);
                    }
                    rowhz.GetCell(0).SetCellValue("合计");
                    rowhz.GetCell(8).SetCellValue(number);
                    rowhz.GetCell(14).SetCellValue(money);
                    for (int i = 0; i < 18; i++)
                    {
                        rowhz.Cells[i].CellStyle = cells2;
                    }


                    IRow rowbzts = sheet2.CreateRow(dt1.Rows.Count + 4);
                    rowbzts.CreateCell(0).SetCellValue("备注：");
                    rowbzts.Cells[0].CellStyle = cells2;

                    IRow rowbz = sheet2.CreateRow(dt1.Rows.Count + 5);
                    for (int i = 0; i < 18; i++)
                    {
                        rowbz.CreateCell(i);
                    }
                    rowbz.GetCell(0).SetCellValue(dt.Rows[0]["HT_DDBZ"].ToString());
                    for (int i = 0; i < 18; i++)
                    {
                        rowbz.Cells[i].CellStyle = cells2;
                    }

                    //ICellStyle cellstyle = wk.CreateCellStyle();
                    //cellstyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.CENTER;
                    //cellstyle.VerticalAlignment = VerticalAlignment.CENTER;

                    //for (int i = 0; i < 18; i++)
                    //{
                    //    rowhz.Cells[i].CellStyle = cellstyle;
                    //}

                    CellRangeAddress range1 = new CellRangeAddress(dt1.Rows.Count + 3, dt1.Rows.Count + 3, 0, 7);
                    sheet2.AddMergedRegion(range1);
                    CellRangeAddress range2 = new CellRangeAddress(dt1.Rows.Count + 3, dt1.Rows.Count + 3, 9, 13);
                    sheet2.AddMergedRegion(range2);
                    CellRangeAddress range3 = new CellRangeAddress(dt1.Rows.Count + 3, dt1.Rows.Count + 3, 15, 17);
                    sheet2.AddMergedRegion(range3);

                    CellRangeAddress range4 = new CellRangeAddress(dt1.Rows.Count + 5, dt1.Rows.Count + 9, 0, 17);
                    sheet2.AddMergedRegion(range4);

                    for (int i = 0; i < 18; i++)
                    {
                        sheet2.AutoSizeColumn(i);
                    }
                    sheet2.ForceFormulaRecalculation = true;
                }

                #endregion
                //sheet0.ProtectSheet("123456");
                //sheet1.ProtectSheet("123456");
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }




    }
}
