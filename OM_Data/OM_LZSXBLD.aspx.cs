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
using System.Collections.Generic;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_LZSXBLD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            asd.action = Request.QueryString["action"];
            asd.id = Request.QueryString["id"];
            if (!IsPostBack)
            {
                asd.userid = Session["UserID"].ToString();
                asd.username = Session["UserName"].ToString();
                bindddl();
                BindData();
            }
        }

        private class asd
        {
            public static string id;
            public static string action;
            public static string username;
            public static string userid;
            public static Dictionary<string, object> dic;
        }

        private void BindData()
        {
            if (asd.action == "add")
            {
                lbLZ_ZDR.Text = asd.username;
                hidLZ_ZDRID.Value = asd.userid;
                lbLZ_ZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                if (ddlLZ_PERSON.SelectedValue != "0")
                {
                    BindrptGDZC();
                    BindrptFGDZC();
                }
            }
            else if (asd.action == "check")
            {
                BindPanel(panZ);
                BindrptGDZC();
                BindrptFGDZC();
            }
            if (asd.action == "read")
            {
                BindPanel(panZ);
                BindrptGDZC();
                BindrptFGDZC();
            }
            PowerControl();
        }

        private void PowerControl()
        {
            if (asd.action == "add")
            {
                PanelEnable(panZ);
                btnSubmit.Visible = true;
                btnQuit.Visible = true;
                panJBXX.Enabled = true;
                panBBM.Enabled = true;
                rblLZ_ZJLLZT.Enabled = false;
            }
            else if (asd.action == "read")
            {
                btnSubmit.Visible = false;
                btnQuit.Visible = false;
                PanelEnable(panZ);
            }
            else if (asd.action == "check")
            {
                PanelEnable(panZ);
                string sql = "select * from OM_LIZHISHOUXU where LZ_ID=" + asd.id;
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                DataRow dr = dt.Rows[0];
                if (asd.userid == dr["LZ_ZDRID"].ToString())
                {
                    panJBXX.Enabled = true;
                    panBBM.Enabled = true;
                    tbxLZ_RIQI.Enabled = false;
                    rblLZ_ZJLLZT.Enabled = false;
                }
                if (asd.userid == dr["LZ_PERSONID"].ToString())
                {
                    btnCK1.Visible = true;
                    btnCKJL.Visible = true;
                    btnGDZCZY.Visible = true;
                    btnFGDZCZY.Visible = true;
                    panBBM.Enabled = true;
                    tbxLZ_RIQI.Enabled = false;
                    rblLZ_ZJLLZT.Enabled = false;
                }
                if (asd.userid == dr["LZ_ZJLLID"].ToString())
                {
                    panBBM.Enabled = true;
                    tbxLZ_RIQI.Enabled = true;
                    rblLZ_ZJLLZT.Enabled = true;
                }
                if (asd.userid == dr["LZ_SCBZID"].ToString())
                {
                    panSC.Enabled = true;
                }
                if (asd.userid == dr["LZ_CGBZID"].ToString())
                {
                    panCG.Enabled = true;
                }
                if (asd.userid == dr["LZ_JSBZID"].ToString())
                {
                    panJS.Enabled = true;
                }
                if (asd.userid == dr["LZ_ZLBZID"].ToString())
                {
                    panZL.Enabled = true;
                }
                if (asd.userid == dr["LZ_GCSBZID"].ToString())
                {
                    panGCS.Enabled = true;
                }
                if (asd.userid == dr["LZ_SCBBZID"].ToString())
                {
                    panSHIC.Enabled = true;
                }
                if (asd.userid == dr["LZ_CWBZID"].ToString())
                {
                    panCW.Enabled = true;
                }
                if (asd.userid == dr["LZ_SBBZID"].ToString())
                {
                    panSB.Enabled = true;
                }
                if (asd.userid == dr["LZ_STJLID"].ToString())
                {
                    panST.Enabled = true;
                }
                if (asd.userid == dr["LZ_CKGLYID"].ToString())
                {
                    panCK.Enabled = true;
                }
                if (asd.userid == dr["LZ_GKGLYID"].ToString())
                {
                    panGK.Enabled = true;
                }
                if (asd.userid == dr["LZ_GDZCGLYID"].ToString())  //固定资产转移
                {
                    panGDZC.Enabled = true;
                }
                if (asd.userid == dr["LZ_TSGLYID"].ToString())
                {
                    panZS.Enabled = true;
                }
                if (asd.userid == dr["LZ_DWGLYID"].ToString())
                {
                    panDZ.Enabled = true;
                }
                if (asd.userid == dr["LZ_DZSBGLYID"].ToString())
                {
                    panDZSB.Enabled = true;
                }
                if (asd.userid == dr["LZ_KQGLYID"].ToString())
                {
                    panKQ.Enabled = true;
                }
                if (asd.userid == dr["LZ_LDGXGLRID"].ToString())
                {
                    panLDGX.Enabled = true;
                }
                if (asd.userid == dr["LZ_SXGLRID"].ToString())
                {
                    panSX.Enabled = true;
                }
                if (asd.userid == dr["LZ_GJJGLRID"].ToString())
                {
                    panGJJ.Enabled = true;
                }
                if (asd.userid == dr["LZ_DAGLRID"].ToString())
                {
                    panDA.Enabled = true;
                }
                if (asd.userid == dr["LZ_GRXXGLYID"].ToString())
                {
                    panXX.Enabled = true;
                }
                if (asd.userid == dr["LZ_ZHBBZID"].ToString()) //综合办公室负责人
                {
                    if (dr["LZ_SPZT"].ToString()=="3y") 
                    {
                        panC.Enabled = true;
                    }
                }
                if (asd.userid == dr["LZ_LDID"].ToString())
                {
                    panL.Enabled = true;
                }
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

        private void bindddl()
        {
            string sql = "select  DEP_CODE,DEP_NAME from TBDS_DEPINFO where  DEP_FATHERID=0";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddlLZ_BUMEN.Items.Add(new ListItem("-请选择-", "0"));
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                ddlLZ_BUMEN.Items.Add(new ListItem(dt.Rows[i]["DEP_NAME"].ToString(), dt.Rows[i]["DEP_CODE"].ToString()));
            }

            if (asd.action != "add")
            {

                string sql1 = "select LZ_PERSON,LZ_PERSONID from OM_LIZHISHOUXU where LZ_ID=" + asd.id;
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
                ddlLZ_PERSON.Items.Add(new ListItem("-请选择-", "0"));
                for (int i = 0, length = dt1.Rows.Count; i < length; i++)
                {
                    ddlLZ_PERSON.Items.Add(new ListItem(dt1.Rows[i]["LZ_PERSON"].ToString(), dt1.Rows[i]["LZ_PERSONID"].ToString()));
                }
            }
        }

        protected void ddlLZ_BUMEN_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLZ_BUMEN.SelectedValue != "0")
            {
                ddlLZ_PERSON.Items.Clear();
                string sql = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_DEPID='" + ddlLZ_BUMEN.SelectedValue + "' and ST_PD!='1'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                ddlLZ_PERSON.Items.Add(new ListItem("-请选择-", "0"));
                for (int i = 0, length = dt.Rows.Count; i < length; i++)
                {
                    ddlLZ_PERSON.Items.Add(new ListItem(dt.Rows[i]["ST_NAME"].ToString(), dt.Rows[i]["ST_ID"].ToString()));
                }
            }
        }

        protected void ddlLZ_PERSON_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLZ_PERSON.SelectedValue != "0")
            {
                string sql = "select * from View_TBDS_STAFFINFO where ST_ID=" + ddlLZ_PERSON.SelectedValue;
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                tbxLZ_BANZU.Text = dt.Rows[0]["ST_DEPID1"].ToString();
                tbxLZ_GANGWEI.Text = dt.Rows[0]["DEP_POSITION"].ToString();
                tbxLZ_RUZHISJ.Text = dt.Rows[0]["ST_INTIME"].ToString();
                tbxLZ_HTNX.Text = dt.Rows[0]["ST_CONTRTIME"].ToString();
                tbxLZ_SQLZSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtLZ_GENDER.Text = dt.Rows[0]["ST_GENDER"].ToString();

                //查询自己部门的部长
                string sql_boss = "select ST_NAME,ST_ID from  TBDS_STAFFINFO where ST_DEPID='" + dt.Rows[0]["ST_DEPID"].ToString() + "' and ST_PD='0' and ST_POSITION LIKE '%01'";
                DataTable dt_boss = DBCallCommon.GetDTUsingSqlText(sql_boss);
                if (dt_boss.Rows.Count>0)
	            {
                    tbxLZ_ZJLL.Text = dt_boss.Rows[0]["ST_NAME"].ToString();
                    hidLZ_ZJLLID.Value = dt_boss.Rows[0]["ST_ID"].ToString();          		 
	            }

                //2017.04.12修改
                string SQL_SHR_SZ = "SELECT * FROM  OM_LIZHISHOUXU_SZ ";
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
                else
                {
                    Response.Write("<script>alert('未设置审核人员，请与管理员联系！！！')</script>");
                    return;
                }

                BindrptGDZC();
                BindrptFGDZC();
            }

        }

        private void BindPanel(Panel panel)
        {
            string sql = "select * from OM_LIZHISHOUXU where LZ_ID=" + asd.id;
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
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
                else if (ctr is HiddenField)
                {
                    HiddenField hid = (HiddenField)ctr;
                    if (list_dc.Contains(hid.ID.Substring(3)))
                    {
                        hid.Value = dr[hid.ID.Substring(3)].ToString();
                    }
                }
                else if (ctr is DropDownList)
                {
                    DropDownList ddl = (DropDownList)ctr;
                    if (ddl.ID == "ddlLZ_BUMEN")
                    {
                        ddl.SelectedValue = dr["LZ_BUMENID"].ToString();
                    }
                    if (ddl.ID == "ddlLZ_PERSON")
                    {
                        ddl.SelectedValue = dr["LZ_PERSONID"].ToString();
                    }
                }
                else if (ctr is RadioButtonList)
                {
                    RadioButtonList rbl = (RadioButtonList)ctr;
                    if (list_dc.Contains(rbl.ID.Substring(3)))
                    {
                        rbl.SelectedValue = dr[rbl.ID.Substring(3)].ToString();
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
                        if (dr[cbx.ID.Substring(3)].ToString() == "y")
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

        #region 固定资产
        private void BindrptGDZC()
        {
            string sql = "select * from TBOM_GDZCIN where BIANHAO !='' and INTYPE='0' and SYRID='" + ddlLZ_PERSON.SelectedValue + "'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);

            string sqltext = "select * from TBOM_GDZCTRANSFER as a left join OM_SP as b on a.DH=b.SPFATHERID where FORMERID='" + ddlLZ_PERSON.SelectedValue + "' and TRANSFTYPE='0' and SPZT='y' and ZYLX='LZ'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            if (dt1.Rows.Count == 0 && dt.Rows.Count == 0)
            {
                NoDataPanel.Visible = true;
                lbdt1.Text = "该人员名下无固定资产！！！无需转移！！！";
                btnGDZCZY.Visible = false;
                btnCKJL.Visible = false;
                return;
            }
            rptGDZC.DataSource = dt;
            rptGDZC.DataBind();
            if (dt.Rows.Count > 0)
            {
                NoDataPanel.Visible = false;
                btnGDZCZY.Visible = false;
                btnCKJL.Visible = false;
            }
            else
            {
                NoDataPanel.Visible = true;
                lbdt1.Text = "该人员名下有固定资产需要转移！！！请点击“固定资产转移按钮！！！”";
                btnGDZCZY.Visible = true;
                btnCKJL.Visible = true;
            }
        }

        protected void btnGDZCZY_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string sql = "select * from TBOM_GDZCIN where BIANHAO !='' and INTYPE='0' and SYRID='" + ddlLZ_PERSON.SelectedValue + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            sql = "DELETE FROM TBOM_TRANSBH";
            list.Add(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                sql = "insert into TBOM_TRANSBH (BH,NAME,MODEL,SYR,SYRID,BUMEN,BUMENID,DATE,PLACE,NX,JIAZHI,NOTE,TRANTYPE) VALUES ('" + dr["BIANHAO"].ToString() + "','" + dr["NAME"].ToString() + "','" + dr["MODEL"].ToString() + "','" + dr["SYR"].ToString() + "','" + dr["SYRID"].ToString() + "','" + dr["SYBUMEN"].ToString() + "','" + dr["SYBUMENID"].ToString() + "','" + dr["SYDATE"].ToString() + "','" + dr["PLACE"].ToString() + "','" + dr["NX"].ToString() + "','" + dr["JIAZHI"].ToString() + "','" + dr["NOTE"].ToString() + "','0')";
                list.Add(sql);
            }
            DBCallCommon.ExecuteTrans(list);
            Response.Write("<script>window.open('OM_LZGDZC.aspx?action=LZSX')</script>");
        }

        protected void btnCKJL_OnClick(object sender, EventArgs e)
        {
            BindrptGDZC();
        }
        #endregion

        #region 非固定资产
        private void BindrptFGDZC()
        {
            string sql = "select * from TBOM_GDZCIN where BIANHAO !='' and INTYPE='1' and SYRID='" + ddlLZ_PERSON.SelectedValue + "'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);

            string sqltext = "select * from TBOM_GDZCTRANSFER as a left join OM_SP as b on a.DH=b.SPFATHERID where FORMERID='" + ddlLZ_PERSON.SelectedValue + "' and TRANSFTYPE='1' and SPZT='y' and ZYLX='LZ'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            if (dt1.Rows.Count == 0 && dt.Rows.Count == 0)
            {
                NoDataPanel1.Visible = true;
                lbdt2.Text = "该人员名下无非固定资产！！！无需转移！！！";
                btnFGDZCZY.Visible = false;
                btnCK1.Visible = false;
                return;
            }
            rptFGDZC.DataSource = dt;
            rptFGDZC.DataBind();
            if (dt.Rows.Count > 0)
            {
                NoDataPanel1.Visible = false;
                btnFGDZCZY.Visible = false;
                btnCK1.Visible = false;
            }
            else
            {
                NoDataPanel1.Visible = true;
                lbdt2.Text = "该人员名下有非固定资产需要转移！！！请点击“非固定资产转移按钮！！！”";
                btnFGDZCZY.Visible = true;
                btnCK1.Visible = true;
            }
        }

        protected void btnFGDZCZY_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string sql = "select * from TBOM_GDZCIN where BIANHAO !='' and INTYPE='1' and SYRID='" + ddlLZ_PERSON.SelectedValue + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            sql = "DELETE FROM TBOM_TRANSBH";
            list.Add(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                sql = "insert into TBOM_TRANSBH (BH,NAME,MODEL,SYR,SYRID,BUMEN,BUMENID,DATE,PLACE,NX,JIAZHI,NOTE,TRANTYPE) VALUES ('" + dr["BIANHAO"].ToString() + "','" + dr["NAME"].ToString() + "','" + dr["MODEL"].ToString() + "','" + dr["SYR"].ToString() + "','" + dr["SYRID"].ToString() + "','" + dr["SYBUMEN"].ToString() + "','" + dr["SYBUMENID"].ToString() + "','" + dr["SYDATE"].ToString() + "','" + dr["PLACE"].ToString() + "','" + dr["NX"].ToString() + "','" + dr["JIAZHI"].ToString() + "','" + dr["NOTE"].ToString() + "','1')";
                list.Add(sql);
            }
            DBCallCommon.ExecuteTrans(list);
            Response.Write("<script>window.open('OM_LZFGDZC.aspx?action=LZSX')</script>");
        }

        protected void btnCK1_OnClick(object sender, EventArgs e)
        {
            BindrptFGDZC();
        }
        #endregion

        /// <summary>
        /// 提交按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_OnClick(object sender, EventArgs e)
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
                    Response.Write("<script>alert('addlist出现问题！！！请与管理员联系！！！')</script>");
                    return;
                }
            }
            else if (asd.action == "check")
            {
                string sql1 = "select * from OM_LIZHISHOUXU where LZ_ID=" + asd.id;
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
                DataRow dr = dt.Rows[0];
                if (asd.userid == dr["LZ_ZJLLID"].ToString())
                {
                    if (rblLZ_ZJLLZT.SelectedValue == "y" && tbxLZ_RIQI.Text.Trim() == "")
                    {
                        Response.Write("<script>alert('您还未确认离职时间！！！请填写离职时间后再提交！！！')</script>");
                        return;
                    }
                }
                List<string> list = checklist();
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('checklist出现问题！！！请与管理员联系！！！')</script>");
                    return;
                }
            }
            Response.Redirect("OM_LZSQJSX.aspx");
        }

        private Dictionary<string, object> save(Panel panel)
        {
            //获取列名
            string sql = "select * from OM_LIZHISHOUXU where LZ_ID is null";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            List<string> col = new List<string>();
            foreach (DataColumn dc in dt.Columns)
            {
                col.Add(dc.ColumnName);
            }

            foreach (Control ctr in panel.Controls)
            {
                if (ctr is Panel)
                {
                    Panel pan = (Panel)ctr;
                    save(pan);
                    //Dictionary<string, object> dic1 = save(pan);
                    //foreach (KeyValuePair<string, object> pair in dic1)
                    //{
                    //    asd.dic.Add(pair.Key.ToString(), pair.Value.ToString());
                    //}
                }
                else if (ctr is TextBox)
                {
                    TextBox txt = (TextBox)ctr;
                    if (col.Contains(txt.ID.Substring(3)))
                    {
                        asd.dic.Add(txt.ID.Substring(3), txt.Text.Trim());
                    }
                }
                else if (ctr is Label)
                {
                    Label lb = (Label)ctr;
                    if (col.Contains(lb.ID.Substring(2)))
                    {
                        asd.dic.Add(lb.ID.Substring(2), lb.Text);
                    }
                }
                else if (ctr is HiddenField)
                {
                    HiddenField hid = (HiddenField)ctr;
                    if (col.Contains(hid.ID.Substring(3)))
                    {
                        asd.dic.Add(hid.ID.Substring(3), hid.Value);
                    }
                }
                else if (ctr is CheckBox)
                {
                    CheckBox cbx = (CheckBox)ctr;
                    if (col.Contains(cbx.ID.Substring(3)))
                    {
                        if (cbx.Checked)
                        {
                            asd.dic.Add(cbx.ID.Substring(3), "y");
                        }
                        else
                        {
                            asd.dic.Add(cbx.ID.Substring(3), "n");
                        }
                    }
                }
                else if (ctr is RadioButtonList)
                {
                    RadioButtonList rbl = (RadioButtonList)ctr;
                    if (col.Contains(rbl.ID.Substring(3)))
                    {
                        asd.dic.Add(rbl.ID.Substring(3), rbl.SelectedValue);
                    }
                }
                else if (ctr is DropDownList)
                {
                    DropDownList ddl = (DropDownList)ctr;
                    if (ddl.ID == "ddlLZ_BUMEN" || ddl.ID == "ddlLZ_PERSON")
                    {
                        asd.dic.Add(ddl.ID.Substring(3), ddl.SelectedItem.Text);
                        asd.dic.Add(ddl.ID.Substring(3) + "id", ddl.SelectedValue);
                    }
                }
            }
            return asd.dic;
        }

        private List<string> addlist()
        {
            List<string> list = new List<string>();
            asd.dic = new Dictionary<string, object>();
            Dictionary<string, object> dic = save(panZ);

            string sql = "insert into OM_LIZHISHOUXU (";
            foreach (KeyValuePair<string, object> pair in dic)
            {
                sql += pair.Key.ToString() + ",";
            }
            sql += "LZ_SPZT";
            sql += ") values (";
            foreach (KeyValuePair<string, object> pair in dic)
            {
                sql += "'" + pair.Value.ToString() + "',";
            }
            sql += '0' + ")";
            list.Add(sql);
            return list;
        }

        private List<string> checklist()
        {
            List<string> list = new List<string>();
            asd.dic = new Dictionary<string, object>();
            Dictionary<string, object> dic = save(panZ);
            string sql = "update OM_LIZHISHOUXU set ";
            foreach (KeyValuePair<string, object> pair in dic)
            {
                sql += pair.Key.ToString() + "=" + "'" + pair.Value.ToString() + "',";
            }
            sql = sql.Trim(',');
            sql += " where LZ_ID=" + asd.id;
            list.Add(sql);
            string sql1 = "select * from OM_LIZHISHOUXU where LZ_ID=" + asd.id;
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
            DataRow dr = dt.Rows[0];
            if (asd.userid == dr["LZ_ZDRID"].ToString() || asd.userid == dr["LZ_PERSONID"].ToString())
            {
                sql = "update OM_LIZHISHOUXU set LZ_BBMSHZT='0' where LZ_ID=" + asd.id;
                list.Add(sql);

                //邮件提醒
                string sprid = "";
                string sptitle = "";
                string spcontent = "";
                sptitle = "离职申请审批";
                spcontent = ddlLZ_PERSON.SelectedItem.Text.Trim() + "的离职申请需要您审批，请登录查看！";
                string getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + tbxLZ_ZJLL.Text.Trim() + "'";
                System.Data.DataTable dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                if (dtgetsprid.Rows.Count > 0)
                {
                    sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                }
            }
            if (asd.userid == dr["LZ_ZJLLID"].ToString())
            {
                if (rblLZ_ZJLLZT.SelectedValue == "y")
                {
                    sql = "update OM_LIZHISHOUXU set LZ_SPZT='1y',LZ_BBMSHZT='y',LZ_GBMSHZT='0' where LZ_ID=" + asd.id;
                    list.Add(sql);
                    sql = "update TBDS_STAFFINFO set ST_LZSJ='" + tbxLZ_RIQI.Text.Trim() + "' where ST_ID='" + ddlLZ_PERSON.SelectedValue + "'";
                    list.Add(sql);

                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sptitle = "离职申请审批";
                    spcontent = ddlLZ_PERSON.SelectedItem.Text.Trim() + "的离职申请需要您审批，请登录查看！";
                    string getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + tbxLZ_SCBZ.Text.Trim() + "'";
                    System.Data.DataTable dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }

                    getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + tbxLZ_CGBZ.Text.Trim() + "'";
                    dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }

                    getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + tbxLZ_JSBZ.Text.Trim() + "'";
                    dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }

                    //质量部部长
                    getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + tbxLZ_ZLBZ.Text.Trim() + "'";
                    dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }

                    getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + tbxLZ_GCSBZ.Text.Trim() + "'";
                    dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }

                    getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + tbxLZ_SCBBZ.Text.Trim() + "'";
                    dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }

                    getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + tbxLZ_CWBZ.Text.Trim() + "'";
                    dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }

                    getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + tbxLZ_SBBZ.Text.Trim() + "'";
                    dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }

                    getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + tbxLZ_STJL.Text.Trim() + "'";
                    dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                }
            }
            if (asd.userid == dr["LZ_SCBZID"].ToString() || asd.userid == dr["LZ_CGBZID"].ToString() || asd.userid == dr["LZ_JSBZID"].ToString() || asd.userid == dr["LZ_GCSBZID"].ToString() || asd.userid == dr["LZ_SCBBZID"].ToString() || asd.userid == dr["LZ_CWBZID"].ToString() || asd.userid == dr["LZ_SBBZID"].ToString() || asd.userid == dr["LZ_STJLID"].ToString())
            {
                if (rblLZ_SCBZZT.SelectedValue == "y" && rblLZ_CGBZZT.SelectedValue == "y" && rblLZ_JSBZZT.SelectedValue == "y" && rblLZ_ZLBZZT.SelectedValue == "y" && rblLZ_GCSBZZT.SelectedValue == "y" && rblLZ_SCBBZZT.SelectedValue == "y" && rblLZ_CWBZZT.SelectedValue == "y" && rblLZ_SBBZZT.SelectedValue == "y" && rblLZ_STJLZT.SelectedValue == "y")
                {
                    sql = "update OM_LIZHISHOUXU set LZ_SPZT='2y',LZ_GBMSHZT='y',LZ_ZHBSHZT='0' where LZ_ID=" + asd.id;
                    list.Add(sql);

                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sptitle = "离职申请审批";
                    spcontent = ddlLZ_PERSON.SelectedItem.Text.Trim() + "的离职申请需要您审批，请登录查看！";
                    string getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + tbxLZ_CKGLY.Text.Trim() + "'";
                    System.Data.DataTable dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }

                    getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + tbxLZ_GKGLY.Text.Trim() + "'";
                    dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }

                    getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txtLZ_GDZCGLY.Text.Trim() + "'";
                    dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }

                    getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + tbxLZ_TSGLY.Text.Trim() + "'";
                    dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }

                    getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + tbxLZ_DWGLY.Text.Trim() + "'";
                    dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }

                    getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txtLZ_DZSBGLY.Text.Trim() + "'";
                    dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }

                    getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + tbxLZ_KQGLY.Text.Trim() + "'";
                    dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                }
            }
            if (asd.userid == dr["LZ_CKGLYID"].ToString() || asd.userid == dr["LZ_GKGLYID"].ToString() || asd.userid == dr["LZ_GDZCGLYID"].ToString() || asd.userid == dr["LZ_TSGLYID"].ToString() || asd.userid == dr["LZ_DWGLYID"].ToString() || asd.userid == dr["LZ_DZSBGLYID"].ToString() || asd.userid == dr["LZ_KQGLYID"].ToString() || asd.userid == dr["LZ_LDGXGLRID"].ToString() || asd.userid == dr["LZ_SXGLRID"].ToString() || asd.userid == dr["LZ_GJJGLRID"].ToString() || asd.userid == dr["LZ_DAGLRID"].ToString() || asd.userid == dr["LZ_GRXXGLYID"].ToString())
            {
                if (rblLZ_CKGLYZT.SelectedValue == "y" && rblLZ_GKGLYZT.SelectedValue == "y" && rblLZ_GDZCZT.SelectedValue == "y" && rblLZ_TSGLYZT.SelectedValue == "y" && rblLZ_DWGLYZT.SelectedValue == "y" && rblLZ_DZSBZT.SelectedValue == "y" && rblLZ_KQGLYZT.SelectedValue == "y" && rblLZ_LDGXGLRZT.SelectedValue == "y" && rblLZ_SXGLRZT.SelectedValue == "y" && rblLZ_GJJGLRZT.SelectedValue == "y" && rblLZ_DAGLRZT.SelectedValue == "y" && rblLZ_GRXXGLYZT.SelectedValue == "y")
                {
                    sql = "update OM_LIZHISHOUXU set LZ_SPZT='3y',LZ_ZHBSHZT='y' where LZ_ID=" + asd.id;
                    list.Add(sql);

                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sptitle = "离职申请审批";
                    spcontent = ddlLZ_PERSON.SelectedItem.Text.Trim() + "的离职申请需要您审批，请登录查看！";
                    string getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + tbxLZ_ZHBBZ.Text.Trim() + "'";
                    System.Data.DataTable dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                }
            }
            if (asd.userid == dr["LZ_ZHBBZID"].ToString())
            {
                if (rblLZ_ZHBSPZT.SelectedValue == "y")
                {
                    sql = "update OM_LIZHISHOUXU set LZ_SPZT='4y' where LZ_ID=" + asd.id;
                    list.Add(sql);

                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sptitle = "离职申请审批";
                    spcontent = ddlLZ_PERSON.SelectedItem.Text.Trim() + "的离职申请需要您审批，请登录查看！";
                    string getsprid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + tbxLZ_LD.Text.Trim() + "'";
                    System.Data.DataTable dtgetsprid = DBCallCommon.GetDTUsingSqlText(getsprid);
                    if (dtgetsprid.Rows.Count > 0)
                    {
                        sprid = dtgetsprid.Rows[0]["ST_ID"].ToString().Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                }
            }
            if (asd.userid == dr["LZ_LDID"].ToString())
            {
                if (rblLZ_LDSPZT.SelectedValue == "y")
                {
                    sql = "update OM_LIZHISHOUXU set LZ_SPZT='y' where LZ_ID=" + asd.id;
                    list.Add(sql);
                    sql = "update TBDS_STAFFINFO set ST_PD='1' where ST_ID='" + ddlLZ_PERSON.SelectedValue + "'";
                    list.Add(sql);
                }
            }
            foreach (Control ctr in panZ.Controls)
            {
                if (ctr is RadioButtonList)
                {
                    RadioButtonList rbl = ctr as RadioButtonList;
                    if (rbl.SelectedValue == "n")
                    {
                        sql = "update OM_LIZHISHOUXU set LZ_SPZT='n' where LZ_ID=" + asd.id;
                        list.Add(sql);
                        break;
                    }
                }
                if (ctr is Panel)
                {
                    Panel pan = (Panel)ctr;
                    foreach (Control ctr1 in pan.Controls)
                    {
                        if (ctr1 is RadioButtonList)
                        {
                            RadioButtonList rbl = ctr1 as RadioButtonList;
                            if (rbl.SelectedValue == "n")
                            {
                                sql = "update OM_LIZHISHOUXU set LZ_SPZT='n' where LZ_ID=" + asd.id;
                                list.Add(sql);
                                break;
                            }
                        }
                    }
                }
            }
            return list;
        }

        protected void btnQuit_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("OM_LZSQJSX.aspx");
        }

    }
}
