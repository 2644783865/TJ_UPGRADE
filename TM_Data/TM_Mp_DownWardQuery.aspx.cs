﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Mp_DownWardQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData();
            }
        }

        protected void BindData()
        {
            string xuhao_engid = Server.HtmlDecode(Request.QueryString["xuhao_engid"].ToString());
            string[] a = xuhao_engid.Split('_');
            ViewState["xuhao"] = a[0];
            ViewState["engid"] = a[1];
            string sql = "select * from View_TM_MPHZY where MP_ENGID='"+a[1]+"' and MP_NEWXUHAO='"+a[0]+"' and MP_STATERV!='9' and MP_STATUS='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            SmartGridView1.DataSource = dt;
            SmartGridView1.DataBind();
            if (SmartGridView1.Rows.Count > 0)
            {
                NoDataPanel1.Visible = false;

                btnShowMpChange.Visible = true;
                btnShowSameLot.Visible = true;
                btnShowAll.Visible = true;
            }
            else
            {
                btnShowMpChange.Visible = false;
                btnShowSameLot.Visible = false;
                btnShowAll.Visible = false;
                NoDataPanel1.Visible = true;
            }
        }
        /// <summary>
        /// 关联变更计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowMpChange_OnClick(object sender, EventArgs e)
        {
            btnShowMpChange.Visible = false;
            string chgpid=((Label)SmartGridView1.Rows[0].FindControl("lblLotNum")).Text.Trim();
            string sql = "select * from View_TM_MPHZY where MP_ENGID='" + ViewState["engid"].ToString() + "' and MP_NEWXUHAO='" + ViewState["xuhao"] + "' and MP_CHGPID='"+chgpid+"'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            SmartGridView2.DataSource = dt;
            SmartGridView2.DataBind();
            if (SmartGridView2.Rows.Count > 0)
            {
                NoDataPanel2.Visible = false;
            }
            else
            {
                NoDataPanel2.Visible = true;
            }
        }
        /// <summary>
        /// 同批相同计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowSameLot_OnClick(object sender, EventArgs e)
        {
            btnShowSameLot.Visible = false;
            string marid = ((Label)SmartGridView1.Rows[0].FindControl("lblMarid")).Text.Trim();
            string pid = ((Label)SmartGridView1.Rows[0].FindControl("lblLotNum")).Text.Trim();
            string sql = "select * from View_TM_MPHZY where MP_ENGID='" + ViewState["engid"].ToString() + "' and MP_MARID='" + marid + "' and MP_PID='" + pid + "' and MP_NEWXUHAO!='" + ViewState["xuhao"].ToString()+ "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            SmartGridView3.DataSource = dt;
            SmartGridView3.DataBind();
            if (SmartGridView3.Rows.Count > 0)
            {
                NoDataPanel3.Visible = false;
            }
            else
            {
                NoDataPanel3.Visible = true;
            }
        }

        /// <summary>
        /// 所有相同计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnShowAll_OnClick(object sender, EventArgs e)
        {
            btnShowAll.Visible = false;
            string marid = ((Label)SmartGridView1.Rows[0].FindControl("lblMarid")).Text.Trim();
            string pid = ((Label)SmartGridView1.Rows[0].FindControl("lblLotNum")).Text.Trim();
            string sql = "select * from View_TM_MPHZY where MP_ENGID='" + ViewState["engid"].ToString() + "' and MP_MARID='" + marid + "' and MP_STATERV='8' and MP_STATUS='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            SmartGridView4.DataSource = dt;
            SmartGridView4.DataBind();
            if (SmartGridView4.Rows.Count > 0)
            {
                NoDataPanel4.Visible = false;
            }
            else
            {
                NoDataPanel4.Visible = true;
            }
        }

        private double sum = 0;//11
        private double sumnum = 0;//12
        private double sumcd = 0;//8
        /// <summary>
        /// 所有相同计划汇总
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SmartGridView4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                sum += Convert.ToDouble(e.Row.Cells[11].Text == "" ? "0" : e.Row.Cells[11].Text);
                sumnum += Convert.ToDouble(e.Row.Cells[12].Text == "" ? "0" : e.Row.Cells[12].Text);
                sumcd += Convert.ToDouble(e.Row.Cells[8].Text == "" ? "0" : e.Row.Cells[8].Text);
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[7].Text = "合计：";
                e.Row.Cells[8].Text = sumcd.ToString();
                e.Row.Cells[11].Text = sum.ToString();
                e.Row.Cells[12].Text = sumnum.ToString();
            }
        }
    }
}
