using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_PXBM_SQ : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropdownList();
                asd.username = Session["UserName"].ToString();
                asd.userid = Session["UserID"].ToString();
                asd.action = Request.QueryString["action"];
                BindData();
                bindrpt();
            }
        }

        private class asd
        {
            public static string username;
            public static string userid;
            public static string action;
        }

        private void BindDropdownList()
        {
            string sql = "select  DEP_CODE,DEP_NAME from TBDS_DEPINFO where  DEP_FATHERID=0";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddlBM.Items.Add(new ListItem("-全部-", "0"));
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                ddlBM.Items.Add(new ListItem(dt.Rows[i]["DEP_NAME"].ToString(), dt.Rows[i]["DEP_NAME"].ToString()));
            }
            ddlBM1.Items.Add(new ListItem("-请选择-", "0"));
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                ddlBM1.Items.Add(new ListItem(dt.Rows[i]["DEP_NAME"].ToString(), dt.Rows[i]["DEP_CODE"].ToString()));
            }
        }

        private void bindrpt()
        {
            string sql = "select * from (select * from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID=b.SPFATHERID where SPZT='y' and PX_YEAR='" + DateTime.Now.ToString("yyyy") + "' and SPLX='LSPX' union all select * from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID1=b.SPFATHERID where SPZT='y'  and SPLX='NDPXJH' and PX_YEAR='" + DateTime.Now.ToString("yyyy") + "')t ";
            if (ddlBM.SelectedValue != "0")
            {
                sql += " where PX_BM like '%" + ddlBM.SelectedValue + "%'";
            }
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            rptPXJH.DataSource = dt;
            rptPXJH.DataBind();
        }

        protected void ddlBM1_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBM1.SelectedValue == "0")
            {
                return;
            }
            cbxRY.Items.Clear();
            string sql = "select ST_ID,ST_NAME,ST_DEPID from TBDS_STAFFINFO where ST_DEPID ='" + ddlBM1.SelectedValue + "' and ST_PD<>'1'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                cbxRY.Items.Add(new ListItem(dt.Rows[i]["ST_NAME"].ToString(), dt.Rows[i]["ST_ID"].ToString()));
            }
        }

        private void BindData()
        {
            if (asd.action == "add")
            {
                lbBM_TXR.Text = asd.username;
                hidBM_TXRID.Value = asd.userid;
                lbBM_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void rptPXJH_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }


        protected void btnSave_onserverclick(object sender, EventArgs e)
        {
            if (asd.action == "add")
            {
                for (int i = 0, length = rptPXJH.Items.Count; i < length; i++)
                {
                    CheckBox cbx = (CheckBox)rptPXJH.Items[i].FindControl("cbxXuHao");
                    if (cbx.Checked)
                    {
                        string PX_ID = ((HiddenField)rptPXJH.Items[i].FindControl("PX_ID")).Value;
                        foreach (ListItem item in cbxRY.Items)
                        {
                            if (item.Selected)
                            {
                                string sql = "select BM_BMR from OM_PXBM where BM_BMRID='" + item.Value + "' and BM_FATHERID='" + PX_ID + "'";
                                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                                if (dt.Rows.Count > 0)
                                {
                                    Response.Write("<script>alert('" + dt.Rows[0]["OM_PXBM"] + " 重复报名了，请不要勾选他！！！')</script>");
                                    return;
                                }
                            }
                        }
                    }
                }
                try
                {
                    List<string> list = addlist();
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('新增的sql语句出现问题，请与管理员联系！！！')</script>");
                    return;
                }

            }
            Response.Redirect("OM_PXBM_GL.aspx");
        }

        protected void btnSubmit_onserverclick(object sender, EventArgs e)
        {

        }

        private List<string> addlist()
        {
            List<string> list = new List<string>();
            for (int i = 0, length = rptPXJH.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptPXJH.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    string PX_ID = ((HiddenField)rptPXJH.Items[i].FindControl("PX_ID")).Value;
                    foreach (ListItem item in cbxRY.Items)
                    {
                        if (item.Selected)
                        {
                            string sql = "insert into OM_PXBM (BM_TXRID,BM_TXR,BM_TXSJ,BM_BM,BM_BMRID,BM_BMR,BM_FATHERID) values (";
                            sql += "'" + hidBM_TXRID.Value + "',";
                            sql += "'" + lbBM_TXR.Text + "',";
                            sql += "'" + lbBM_SJ.Text + "',";
                            sql += "'" + ddlBM1.SelectedItem.Text.Trim() + "',";
                            sql += "'" + item.Value + "',";
                            sql += "'" + item.Text + "',";
                            sql += "'" + PX_ID + "')";
                            list.Add(sql);
                        }
                    }
                }
            }
            return list;
        }

        protected void Query(object sender, EventArgs e)
        {
            bindrpt();
        }

    }
}
