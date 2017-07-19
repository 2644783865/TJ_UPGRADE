using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_JDPXJH_SS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                asd.action = Request.QueryString["action"];
                asd.id = Request.QueryString["id"];
                asd.userid = Session["UserID"].ToString();
                asd.username = Session["UserName"].ToString();
                BindData();
            }
        }

        private class asd
        {
            public static string username;
            public static string userid;
            public static string action;
            public static string id;
            public static DataTable dt;
        }

        private void BindcbxBM()
        {
            string sql = "select  DEP_CODE,DEP_NAME from TBDS_DEPINFO where  DEP_FATHERID=0";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                cbxBM.Items.Add(new ListItem(dt.Rows[i]["DEP_NAME"].ToString(), dt.Rows[i]["DEP_CODE"].ToString()));
            }
        }

        protected void cbxBM_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            cbxRY.Items.Clear();
            foreach (ListItem item in cbxBM.Items)
            {
                if (item.Selected)
                {
                    string sql = " select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_DEPID = '" + item.Value + "' and ST_PD='0'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    for (int i = 0, length = dt.Rows.Count; i < length; i++)
                    {
                        cbxRY.Items.Add(new ListItem(dt.Rows[i]["ST_NAME"].ToString(), dt.Rows[i]["ST_ID"].ToString()));
                    }
                }
            }
        }

        private string GettxtPX_BH()
        {
            string pxbh = "";
            string pxlx = "";
            string bm = "";
            string nf = "";
            int lsh = 0;
            switch (asd.dt.Rows[0]["PX_FS"].ToString())
            {
                case "n":
                    pxlx = "I";
                    break;
                case "w":
                    pxlx = "O";
                    break;
                default:
                    break;
            }
            switch (asd.dt.Rows[0]["PX_BM"].ToString())
            {
                case "综合办公室":
                    bm = "G";
                    break;
                case "财务部":
                    bm = "F";
                    break;
                case "市场部":
                    bm = "M";
                    break;
                case "技术部":
                    bm = "T";
                    break;
                case "质量部":
                    bm = "Q";
                    break;
                case "设备安全管理部":
                    bm = "ES";
                    break;
                case "设备安全管理部A":
                    bm = "ES";
                    break;
                case "生产管理部":
                    bm = "PM";
                    break;
                case "生产管理部A":
                    bm = "PM";
                    break;
                case "生产管理部B":
                    bm = "PM";
                    break;
                case "工程师办公室":
                    bm = "E";
                    break;
                case "采购部":
                    bm = "P";
                    break;
                default:
                    break;
            }
            nf = DateTime.Now.ToString("yyyy");
            //string sql = "select count(PX_ID) from ( select * from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID=b.SPFATHERID where SPZT='y' and SPLX='LSPX' union all select * from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID1=b.SPFATHERID where SPZT='y'  and SPLX='NDPXJH')t where PX_BM  like '%" + asd.dt.Rows[0]["PX_BM"].ToString() + "%' and PX_FS='" + asd.dt.Rows[0]["PX_FS"].ToString() + "' and PX_BH like '%" + nf + "%'";
            string sql = "select count(PX_ID) from ( select * from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID=b.SPFATHERID where SPZT='y' and SPLX='LSPX' union all select * from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID1=b.SPFATHERID where SPZT='y'  and SPLX='NDPXJH')t where PX_BH like '%" + nf + "%'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            lsh = CommonFun.ComTryInt(dt.Rows[0][0].ToString()) + 1;
            if (lsh < 10)
            {
                pxbh = pxlx + "-" + bm + "-" + nf + "-" + lsh.ToString().PadLeft(2, '0');
            }
            else
            {
                pxbh = pxlx + "-" + bm + "-" + nf + "-" + lsh.ToString();
            }
            return pxbh;
        }

        private void BindData()
        {
            if (asd.action == "add")
            {
                string sql = "select * from OM_PXJH_SQ where PX_ID =" + asd.id;
                asd.dt = DBCallCommon.GetDTUsingSqlText(sql);
                BindcbxBM();
                txtPX_BH.Text = GettxtPX_BH();
                lbPX_SSZDR.Text = asd.username;
                hidPX_SSZDRID.Value = asd.userid;
                lbPX_SSZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else if (asd.action == "alter")
            {
                string sql = "select * from OM_PXJH_SQ where PX_ID =" + asd.id;
                asd.dt = DBCallCommon.GetDTUsingSqlText(sql);
                BindcbxBM();
                BindPanel(panJBXX);
            }
        }

        private void BindPanel(Panel panel)//绑定panel
        {
            DataTable dt = asd.dt;
            List<string> list_dc = new List<string>();
            foreach (DataColumn dc in dt.Columns)
            {
                list_dc.Add(dc.ColumnName);
            }
            DataRow dr = dt.Rows[0];
            foreach (Control ctr in panel.Controls)
            {
                if (ctr is TextBox)
                {
                    TextBox txt = (TextBox)ctr;
                    if (list_dc.Contains(txt.ID.Substring(3)))
                    {
                        txt.Text = dr[txt.ID.Substring(3)].ToString();
                    }
                }
                else if (ctr is Label)
                {
                    Label lb = (Label)ctr;
                    if (list_dc.Contains(lb.ID.Substring(2)))
                    {
                        lb.Text = dr[lb.ID.Substring(2)].ToString();
                    }
                }
                else if (ctr is DropDownList)
                {
                    DropDownList ddl = (DropDownList)ctr;
                    if (list_dc.Contains(ddl.ID.Substring(3)))
                    {
                        ddl.SelectedValue = dr[ddl.ID.Substring(3)].ToString();
                    }

                }
                else if (ctr is RadioButtonList)
                {
                    RadioButtonList rbl = (RadioButtonList)ctr;
                    if (list_dc.Contains(rbl.ID.Substring(3)))
                    {
                        if (dr[rbl.ID.Substring(3)].ToString() != "0")
                        {
                            rbl.SelectedValue = dr[rbl.ID.Substring(3)].ToString();
                        }
                    }
                }
                else if (ctr is Panel)
                {
                    Panel pan = (Panel)ctr;
                    BindPanel(pan);
                }
                else if (ctr is CheckBox)
                {
                    CheckBox cbx = (CheckBox)ctr;
                    if (list_dc.Contains(cbx.ID.Substring(3)))
                    {
                        if (dr[cbx.ID.Substring(3)].ToString() != "")
                        {
                            cbx.Checked = true;
                        }
                        else
                        {
                            cbx.Checked = false;
                        }
                    }
                }
            }
        }

        protected void btnSave_onserverclick(object sender, EventArgs e)
        {
            if (asd.action == "add")
            {
                List<string> list = addlist();
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('新增的sql语句出现问题，请与管理员联系！！！')</script>");
                    return;
                }
            }
            if (asd.action=="alter")
            {
                List<string> list = alterlist();
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('修改的sql语句出现问题，请与管理员联系！！！')</script>");
                    return;
                }
            }
            Response.Redirect("OM_JDPXJH.aspx");
        }

        private List<string> addlist()
        {
            List<string> list = new List<string>();
            string sql = "update OM_PXJH_SQ set PX_BH='" + txtPX_BH.Text.Trim() + "',";
            sql += "PX_SJSJ='" + txtPX_SJSJ.Text.Trim() + "',";
            sql += "PX_SJDD='" + txtPX_SJDD.Text.Trim() + "',";
            sql += "PX_SJXS='" + txtPX_SJXS.Text.Trim() + "',";
            sql += "PX_SSZDR='" + lbPX_SSZDR.Text + "',";
            sql += "PX_SSZDRID='" + hidPX_SSZDRID.Value + "',";
            sql += "PX_SSZDSJ='" + lbPX_SSZDSJ.Text + "',";
            sql += "PX_SJBZ='" + txtPX_SJBZ.Text.Trim() + "',";
            sql += "PX_SJRY='";
            foreach (ListItem item in cbxRY.Items)
            {
                if (item.Selected)
                {
                    sql += item.Text + ",";
                }
            }
            sql = sql.Trim(',');
            sql += "',";
            sql += "PX_SJRS='" + txtPX_SJRS.Text.Trim() + "'";
            sql += " where PX_ID=" + asd.id;
            list.Add(sql);
            sql = string.Empty;
            foreach (ListItem item in cbxRY.Items)
            {
                if (item.Selected)
                {
                    sql = "insert into OM_PXDA (DA_CXR,DA_CXRID,DA_XMID,DA_XMBH) values (";
                    sql += "'" + item.Text + "',";
                    sql += "'" + item.Value + "',";
                    sql += "'" + asd.id + "',";
                    sql += "'" + txtPX_BH.Text.Trim() + "')";
                    list.Add(sql);
                    sql = string.Empty;
                }
            }
            return list;
        }

        private List<string> alterlist()
        {
            List<string> list = new List<string>();
            string sql = " update OM_PXJH_SQ set ";
            sql += "PX_SJSJ='" + txtPX_SJSJ.Text.Trim() + "',";
            sql += "PX_SJDD='" + txtPX_SJDD.Text.Trim() + "',";
            sql += "PX_SJXS='" + txtPX_SJXS.Text.Trim() + "',";
            sql += "PX_SJBZ='" + txtPX_SJBZ.Text.Trim() + "',";
            sql += "PX_SJRY='";
            foreach (ListItem item in cbxRY.Items)
            {
                if (item.Selected)
                {
                    sql += item.Text + ",";
                }
            }
            sql = sql.Trim(',');
            sql += "',";
            sql += "PX_SJRS='" + txtPX_SJRS.Text.Trim() + "'";
            sql += " where PX_ID=" + asd.id;
            list.Add(sql);
            sql = string.Empty;
            sql = "delete from OM_PXDA where DA_XMID='"+asd.id+"'";
            list.Add(sql);
            sql = string.Empty;
            foreach (ListItem item in cbxRY.Items)
            {
                if (item.Selected)
                {
                    sql = "insert into OM_PXDA (DA_CXR,DA_CXRID,DA_XMID,DA_XMBH) values (";
                    sql += "'" + item.Text + "',";
                    sql += "'" + item.Value + "',";
                    sql += "'" + asd.id + "',";
                    sql += "'" + txtPX_BH.Text.Trim() + "')";
                    list.Add(sql);
                    sql = string.Empty;
                }
            }
            return list;
        }

        protected void btnBack_onserverclick(object sender, EventArgs e)
        {
            Response.Redirect("OM_JDPXJH.aspx");
        }

    }
}
