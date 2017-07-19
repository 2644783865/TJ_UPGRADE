using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_LZSXBLD_SZ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            asd.action = Request.QueryString["action"];
            asd.id = Request.QueryString["id"];
            if (!IsPostBack)
            {
                asd.userid = Session["UserID"].ToString();
                asd.username = Session["UserName"].ToString();

                BindData();
            }
        }

        private class asd
        {
            public static string id;
            public static string action;
            public static string username;
            public static string userid;
            //public static Dictionary<string, object> dic;
        }

        private void BindData()
        {
            BIND_SZ_SHR();
            PowerControl();
        }

        private void PowerControl()
        {

            if (asd.action == "read")
            {
                PanelEnable(panZ);
                PanelEnable_else();

            }

        }

        private void PanelEnable(Panel panel)
        {
            foreach (Control ctr in panel.Controls)
            {
                if (ctr is Panel)
                {
                    Panel pan = ctr as Panel;
                    pan.Enabled = false;
                    PanelEnable(pan);
                }
            }
        }
        //将23个审核员去掉enable=flase
        private void PanelEnable_else()
        {
            tbxLZ_SCBZ.Enabled = true;
            tbxLZ_CGBZ.Enabled = true;
            tbxLZ_JSBZ.Enabled = true;
            tbxLZ_ZLBZ.Enabled = true;
            tbxLZ_GCSBZ.Enabled = true;


            tbxLZ_SCBBZ.Enabled = true;
            tbxLZ_CWBZ.Enabled = true;
            tbxLZ_SBBZ.Enabled = true;
            tbxLZ_STJL.Enabled = true;
            tbxLZ_CKGLY.Enabled = true;

            tbxLZ_GKGLY.Enabled = true;
            txtLZ_GDZCGLY.Enabled = true;
            tbxLZ_TSGLY.Enabled = true;
            tbxLZ_DWGLY.Enabled = true;
            txtLZ_DZSBGLY.Enabled = true;

            tbxLZ_KQGLY.Enabled = true;
            tbxLZ_LDGXGLR.Enabled = true;
            tbxLZ_SXGLR.Enabled = true;
            tbxLZ_GJJGLR.Enabled = true;
            tbxLZ_DAGLR.Enabled = true;

            tbxLZ_GRXXGLY.Enabled = true;
            tbxLZ_ZHBBZ.Enabled = true;
            tbxLZ_LD.Enabled = true;
        }

        //绑定设置好的审核人
        private void BIND_SZ_SHR()
        {
            string SQL_SHR_SZ = "SELECT *  FROM OM_LIZHISHOUXU_SZ ";
            DataTable DT_SHR_SZ = DBCallCommon.GetDTUsingSqlText(SQL_SHR_SZ);
            if (DT_SHR_SZ.Rows.Count > 0)
            {
                tbxLZ_SCBZ.Text = DT_SHR_SZ.Rows[0]["tbxLZ_SCBZ"].ToString();
                hidLZ_SCBZID.Value = DT_SHR_SZ.Rows[0]["hidLZ_SCBZID"].ToString();
                tbxLZ_CGBZ.Text = DT_SHR_SZ.Rows[0]["tbxLZ_CGBZ"].ToString();
                hidLZ_CGBZID.Value = DT_SHR_SZ.Rows[0]["hidLZ_CGBZID"].ToString();
                tbxLZ_JSBZ.Text = DT_SHR_SZ.Rows[0]["tbxLZ_JSBZ"].ToString();
                hidLZ_JSBZID.Value = DT_SHR_SZ.Rows[0]["hidLZ_JSBZID"].ToString();
                tbxLZ_ZLBZ.Text = DT_SHR_SZ.Rows[0]["tbxLZ_ZLBZ"].ToString();
                hidLZ_ZLBZID.Value = DT_SHR_SZ.Rows[0]["hidLZ_ZLBZID"].ToString();
                tbxLZ_GCSBZ.Text = DT_SHR_SZ.Rows[0]["tbxLZ_GCSBZ"].ToString();
                hidLZ_GCSBZID.Value = DT_SHR_SZ.Rows[0]["hidLZ_GCSBZID"].ToString();

                tbxLZ_SCBBZ.Text = DT_SHR_SZ.Rows[0]["tbxLZ_SCBBZ"].ToString();
                hidLZ_SCBBZID.Value = DT_SHR_SZ.Rows[0]["hidLZ_SCBBZID"].ToString();
                tbxLZ_CWBZ.Text = DT_SHR_SZ.Rows[0]["tbxLZ_CWBZ"].ToString();
                hidLZ_CWBZID.Value = DT_SHR_SZ.Rows[0]["hidLZ_CWBZID"].ToString();
                tbxLZ_SBBZ.Text = DT_SHR_SZ.Rows[0]["tbxLZ_SBBZ"].ToString();
                hidLZ_SBBZID.Value = DT_SHR_SZ.Rows[0]["hidLZ_SBBZID"].ToString();
                tbxLZ_STJL.Text = DT_SHR_SZ.Rows[0]["tbxLZ_STJL"].ToString();
                hidLZ_STJLID.Value = DT_SHR_SZ.Rows[0]["hidLZ_STJLID"].ToString();
                tbxLZ_CKGLY.Text = DT_SHR_SZ.Rows[0]["tbxLZ_CKGLY"].ToString();
                hidLZ_CKGLYID.Value = DT_SHR_SZ.Rows[0]["hidLZ_CKGLYID"].ToString();

                tbxLZ_GKGLY.Text = DT_SHR_SZ.Rows[0]["tbxLZ_GKGLY"].ToString();
                hidLZ_GDZCGLYID.Value = DT_SHR_SZ.Rows[0]["hidLZ_GDZCGLYID"].ToString();
                txtLZ_GDZCGLY.Text = DT_SHR_SZ.Rows[0]["txtLZ_GDZCGLY"].ToString();
                hidLZ_GKGLYID.Value = DT_SHR_SZ.Rows[0]["hidLZ_GKGLYID"].ToString();
                tbxLZ_TSGLY.Text = DT_SHR_SZ.Rows[0]["tbxLZ_TSGLY"].ToString();
                hidLZ_TSGLYID.Value = DT_SHR_SZ.Rows[0]["hidLZ_TSGLYID"].ToString();
                tbxLZ_DWGLY.Text = DT_SHR_SZ.Rows[0]["tbxLZ_DWGLY"].ToString();
                hidLZ_DWGLYID.Value = DT_SHR_SZ.Rows[0]["hidLZ_DWGLYID"].ToString();
                txtLZ_DZSBGLY.Text = DT_SHR_SZ.Rows[0]["txtLZ_DZSBGLY"].ToString();
                hidLZ_DZSBGLYID.Value = DT_SHR_SZ.Rows[0]["hidLZ_DZSBGLYID"].ToString();

                tbxLZ_KQGLY.Text = DT_SHR_SZ.Rows[0]["tbxLZ_KQGLY"].ToString();
                hidLZ_KQGLYID.Value = DT_SHR_SZ.Rows[0]["hidLZ_KQGLYID"].ToString();
                tbxLZ_LDGXGLR.Text = DT_SHR_SZ.Rows[0]["tbxLZ_LDGXGLR"].ToString();
                hidLZ_LDGXGLRID.Value = DT_SHR_SZ.Rows[0]["hidLZ_LDGXGLRID"].ToString();
                tbxLZ_SXGLR.Text = DT_SHR_SZ.Rows[0]["tbxLZ_SXGLR"].ToString();
                hidLZ_SXGLRID.Value = DT_SHR_SZ.Rows[0]["hidLZ_SXGLRID"].ToString();
                tbxLZ_GJJGLR.Text = DT_SHR_SZ.Rows[0]["tbxLZ_GJJGLR"].ToString();
                hidLZ_GJJGLRID.Value = DT_SHR_SZ.Rows[0]["hidLZ_GJJGLRID"].ToString();
                tbxLZ_DAGLR.Text = DT_SHR_SZ.Rows[0]["tbxLZ_DAGLR"].ToString();
                hidLZ_DAGLRID.Value = DT_SHR_SZ.Rows[0]["hidLZ_DAGLRID"].ToString();

                tbxLZ_GRXXGLY.Text = DT_SHR_SZ.Rows[0]["tbxLZ_GRXXGLY"].ToString();
                hidLZ_GRXXGLYID.Value = DT_SHR_SZ.Rows[0]["hidLZ_GRXXGLYID"].ToString();
                tbxLZ_ZHBBZ.Text = DT_SHR_SZ.Rows[0]["tbxLZ_ZHBBZ"].ToString();
                hidLZ_ZHBBZID.Value = DT_SHR_SZ.Rows[0]["hidLZ_ZHBBZID"].ToString();
                tbxLZ_LD.Text = DT_SHR_SZ.Rows[0]["tbxLZ_LD"].ToString();
                hidLZ_LDID.Value = DT_SHR_SZ.Rows[0]["hidLZ_LDID"].ToString();
            }
        }



        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {

            string SQL_SHR_SZ = "SELECT *  FROM OM_LIZHISHOUXU_SZ ";
            DataTable DT_SHR_SZ = DBCallCommon.GetDTUsingSqlText(SQL_SHR_SZ);
            if (DT_SHR_SZ.Rows.Count > 0)
            {
                string sz_shr = "update OM_LIZHISHOUXU_SZ SET ";
                sz_shr += " tbxLZ_SCBZ='" + tbxLZ_SCBZ.Text + "',hidLZ_SCBZID='" + hidLZ_SCBZID.Value + "',";
                sz_shr += " tbxLZ_CGBZ='" + tbxLZ_CGBZ.Text + "',hidLZ_CGBZID='" + hidLZ_CGBZID.Value + "',";
                sz_shr += " tbxLZ_JSBZ='" + tbxLZ_JSBZ.Text + "',hidLZ_JSBZID='" + hidLZ_JSBZID.Value + "',";
                sz_shr += " tbxLZ_ZLBZ='" + tbxLZ_ZLBZ.Text + "',hidLZ_ZLBZID='" + hidLZ_ZLBZID.Value + "',";
                sz_shr += " tbxLZ_GCSBZ='" + tbxLZ_GCSBZ.Text + "',hidLZ_GCSBZID='" + hidLZ_GCSBZID.Value + "',";

                sz_shr += " tbxLZ_SCBBZ='" + tbxLZ_SCBBZ.Text + "',hidLZ_SCBBZID='" + hidLZ_SCBBZID.Value + "',";
                sz_shr += " tbxLZ_CWBZ='" + tbxLZ_CWBZ.Text + "',hidLZ_CWBZID='" + hidLZ_CWBZID.Value + "',";
                sz_shr += " tbxLZ_SBBZ='" + tbxLZ_SBBZ.Text + "',hidLZ_SBBZID='" + hidLZ_SBBZID.Value + "',";
                sz_shr += " tbxLZ_STJL='" + tbxLZ_STJL.Text + "',hidLZ_STJLID='" + hidLZ_STJLID.Value + "',";
                sz_shr += " tbxLZ_CKGLY='" + tbxLZ_CKGLY.Text + "',hidLZ_CKGLYID='" + hidLZ_CKGLYID.Value + "',";

                sz_shr += " tbxLZ_GKGLY='" + tbxLZ_GKGLY.Text + "',hidLZ_GDZCGLYID='" + hidLZ_GDZCGLYID.Value + "',";
                sz_shr += " txtLZ_GDZCGLY='" + txtLZ_GDZCGLY.Text + "',hidLZ_GKGLYID='" + hidLZ_GKGLYID.Value + "',";
                sz_shr += " tbxLZ_TSGLY='" + tbxLZ_TSGLY.Text + "',hidLZ_TSGLYID='" + hidLZ_TSGLYID.Value + "',";
                sz_shr += " tbxLZ_DWGLY='" + tbxLZ_DWGLY.Text + "',hidLZ_DWGLYID='" + hidLZ_DWGLYID.Value + "',";
                sz_shr += " txtLZ_DZSBGLY='" + txtLZ_DZSBGLY.Text + "',hidLZ_DZSBGLYID='" + hidLZ_DZSBGLYID.Value + "',";

                sz_shr += " tbxLZ_KQGLY='" + tbxLZ_KQGLY.Text + "',hidLZ_KQGLYID='" + hidLZ_KQGLYID.Value + "',";
                sz_shr += " tbxLZ_LDGXGLR='" + tbxLZ_LDGXGLR.Text + "',hidLZ_LDGXGLRID='" + hidLZ_LDGXGLRID.Value + "',";
                sz_shr += " tbxLZ_SXGLR='" + tbxLZ_SXGLR.Text + "',hidLZ_SXGLRID='" + hidLZ_SXGLRID.Value + "',";
                sz_shr += " tbxLZ_GJJGLR='" + tbxLZ_GJJGLR.Text + "',hidLZ_GJJGLRID='" + hidLZ_GJJGLRID.Value + "',";
                sz_shr += " tbxLZ_DAGLR='" + tbxLZ_DAGLR.Text + "',hidLZ_DAGLRID='" + hidLZ_DAGLRID.Value + "',";

                sz_shr += " tbxLZ_GRXXGLY='" + tbxLZ_GRXXGLY.Text + "',hidLZ_GRXXGLYID='" + hidLZ_GRXXGLYID.Value + "',";
                sz_shr += " tbxLZ_ZHBBZ='" + tbxLZ_ZHBBZ.Text + "',hidLZ_ZHBBZID='" + hidLZ_ZHBBZID.Value + "',";
                sz_shr += " tbxLZ_LD='" + tbxLZ_LD.Text + "',hidLZ_LDID='" + hidLZ_LDID.Value + "'";
                DBCallCommon.ExeSqlText(sz_shr);
            }
            else
            {
                string sql_sz_shr_instrt = " insert into OM_LIZHISHOUXU_SZ values ";
                sql_sz_shr_instrt += "('" + tbxLZ_SCBZ.Text + "'," + hidLZ_SCBZID.Value + ",";
                sql_sz_shr_instrt += "'" + tbxLZ_CGBZ.Text + "'," + hidLZ_CGBZID.Value + ",";
                sql_sz_shr_instrt += "'" + tbxLZ_JSBZ.Text + "'," + hidLZ_JSBZID.Value + ",";
                sql_sz_shr_instrt += "'" + tbxLZ_ZLBZ.Text + "'," + hidLZ_ZLBZID.Value + ",";
                sql_sz_shr_instrt += "'" + tbxLZ_GCSBZ.Text + "'," + hidLZ_GCSBZID.Value + ",";

                sql_sz_shr_instrt += "'" + tbxLZ_SCBBZ.Text + "'," + hidLZ_SCBBZID.Value + ",";
                sql_sz_shr_instrt += "'" + tbxLZ_CWBZ.Text + "'," + hidLZ_CWBZID.Value + ",";
                sql_sz_shr_instrt += "'" + tbxLZ_SBBZ.Text + "'," + hidLZ_SBBZID.Value + ",";
                sql_sz_shr_instrt += "'" + tbxLZ_STJL.Text + "'," + hidLZ_STJLID.Value + ",";
                sql_sz_shr_instrt += "'" + tbxLZ_CKGLY.Text + "'," + hidLZ_CKGLYID.Value + ",";

                sql_sz_shr_instrt += "'" + tbxLZ_GKGLY.Text + "'," + hidLZ_GDZCGLYID.Value + ",";
                sql_sz_shr_instrt += "'" + txtLZ_GDZCGLY.Text + "'," + hidLZ_GKGLYID.Value + ",";
                sql_sz_shr_instrt += "'" + tbxLZ_TSGLY.Text + "'," + hidLZ_TSGLYID.Value + ",";
                sql_sz_shr_instrt += "'" + tbxLZ_DWGLY.Text + "'," + hidLZ_DWGLYID.Value + ",";
                sql_sz_shr_instrt += "'" + txtLZ_DZSBGLY.Text + "'," + hidLZ_DZSBGLYID.Value + ",";

                sql_sz_shr_instrt += "'" + tbxLZ_KQGLY.Text + "'," + hidLZ_KQGLYID.Value + ",";
                sql_sz_shr_instrt += "'" + tbxLZ_LDGXGLR.Text + "'," + hidLZ_LDGXGLRID.Value + ",";
                sql_sz_shr_instrt += "'" + tbxLZ_SXGLR.Text + "'," + hidLZ_SXGLRID.Value + ",";
                sql_sz_shr_instrt += "'" + tbxLZ_GJJGLR.Text + "'," + hidLZ_GJJGLRID.Value + ",";
                sql_sz_shr_instrt += "'" + tbxLZ_DAGLR.Text + "'," + hidLZ_DAGLRID.Value + ",";

                sql_sz_shr_instrt += "'" + tbxLZ_GRXXGLY.Text + "'," + hidLZ_GRXXGLYID.Value + ",";
                sql_sz_shr_instrt += "'" + tbxLZ_ZHBBZ.Text + "'," + hidLZ_ZHBBZID.Value + ",";
                sql_sz_shr_instrt += "'" + tbxLZ_LD.Text + "'," + hidLZ_LDID.Value + ")";
                DBCallCommon.ExeSqlText(sql_sz_shr_instrt);
            }

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('设置成功！！！')", true);
            Response.Redirect("OM_LZSQJSX.aspx");
        }







        protected void btnQuit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("OM_LZSQJSX.aspx");
        }
    }
}
