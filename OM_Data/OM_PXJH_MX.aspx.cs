using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZCZJ_DPF.CommonClass;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_PXJH_MX : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                asd.Action = Request.QueryString["action"];
                asd.Pxbh = Request.QueryString["pxbh"];
                asd.Userid = Session["UserID"].ToString();
                asd.Username = Session["UserName"].ToString();
                BindData();
            }
        }

        //私有类，仿全局变量
        private class asd
        {
            //action
            private static string _action;
            public static string Action
            {
                get { return _action; }
                set { _action = value; }
            }

            //培训编号
            private static string _pxbh;
            public static string Pxbh
            {
                get { return _pxbh; }
                set { _pxbh = value; }
            }

            //userid
            private static string _userid;
            public static string Userid
            {
                get { return _userid; }
                set { _userid = value; }
            }

            //username
            private static string _username;
            public static string Username
            {
                get { return _username; }
                set { _username = value; }
            }

        }

        private void BindData()
        {
            if (asd.Action == "edit")
            {
                string sql = string.Format("select * from OM_PXJH_SQ where PX_BH='{0}'", asd.Pxbh);
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                PanelDic.BindPanel(panJBXX, dt);
                sql = string.Format("select a.*,b.ST_SEQUEN from OM_PXDA as a left join TBDS_STAFFINFO as b on a.DA_CXRID=b.ST_ID where DA_XMBH='{0}'", asd.Pxbh);
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                rptNR.DataSource = dt1;
                rptNR.DataBind();
                NoData();
                lb_PX_PJR.Text = asd.Username;
                lb_PX_PJSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else if (asd.Action == "read")
            {
                string sql = string.Format("select * from OM_PXJH_SQ where PX_BH='{0}'", asd.Pxbh);
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                PanelDic.BindPanel(panJBXX, dt);
                sql = string.Format("select a.*,b.ST_SEQUEN from OM_PXDA as a left join TBDS_STAFFINFO as b on a.DA_CXRID=b.ST_ID where DA_XMBH='{0}'", asd.Pxbh);
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                rptNR.DataSource = dt1;
                rptNR.DataBind();
                NoData();
                lb_PX_PJR.Text = asd.Username;
                lb_PX_PJSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            PowerControl();
        }

        private void PowerControl()
        {
            btnSave.Visible = false;
            btnBack.Visible = false;
            if (asd.Action == "edit")
            {
                btnSave.Visible = true;
                btnBack.Visible = true;
            }
            if (asd.Action=="read")
            {
                panJBXX.Enabled = false;
            }
        }

        private void NoData()
        {
            if (rptNR.Items.Count > 0)
            {
                palNoData.Visible = false;
            }
            else
            {
                palNoData.Visible = true;
            }
        }

        protected void btnSave_onserverclick(object sender, EventArgs e)
        {
            if (asd.Action == "edit")
            {
                List<string> list = editlist();
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('editlist出现错误，请联系管理员!!!')</script>");
                    return;
                }
            }
        }

        private List<string> editlist()
        {
            List<string> list = new List<string>();
            string sql = string.Format("update OM_PXJH_SQ set PX_ZYNR ='{0}',PX_CQL={1},PX_KSHGL={2},PX_PJR='{3}',PX_PJRID='{4}',PX_PJSJ='{5}',PX_XGPJ='{7}' where PX_BH='{6}'", txt_PX_ZYNR.Text.Trim(), txt_PX_CQL.Text, txt_PX_KSHGL.Text, asd.Username, asd.Userid, lb_PX_PJSJ.Text, asd.Pxbh, txt_PX_XGPJ.Text.Trim());
            list.Add(sql);
            foreach (RepeaterItem item in rptNR.Items)
            {
                sql = string.Format("update OM_PXDA set DA_CXRDF={0},DA_SFCQ='{1}' where DA_XMBH='{2}' and DA_CXRID='{3}'", ((TextBox)item.FindControl("DA_CXRDF")).Text.Trim(), ((CheckBox)item.FindControl("DA_SFCQ")).Checked == true ? "y" : "n", asd.Pxbh, ((HiddenField)item.FindControl("DA_CXRID")).Value);
                list.Add(sql);
            }
            return list;
        }

    }
}
