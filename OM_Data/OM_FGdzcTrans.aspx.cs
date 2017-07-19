using System;
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
using System.Data.SqlClient;
using System.Collections.Generic;


namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_FGdzcTrans : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                asd.action = Request.QueryString["action"];
                asd.id = Request.QueryString["id"];
                asd.userid = Session["UserID"].ToString();
                asd.uesrname = Session["UserName"].ToString();
                BindData();
            }
        }

        private void BindData()
        {
            if (asd.action == "add")
            {
                txtZDR.Text = asd.uesrname;
                hidZDRID.Value = asd.userid;
                lbZDR_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                InitRepeaterView();
            }
            else if (asd.action == "check")
            {
                string sql = string.Format("select * from TBOM_GDZCTRANSFER as a left join OM_SP as b on a.DH=b.SPFATHERID where TRANSFTYPE='1' and SPFATHERID ='{0}'", asd.id);
                asd.dts = DBCallCommon.GetDTUsingSqlText(sql);
                Repeater1.DataSource = asd.dts;
                Repeater1.DataBind();
                BindPanel(panSP);
                if (asd.userid == asd.dts.Rows[0]["SPR1ID"].ToString())
                {
                    lbSPR1_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (asd.userid == asd.dts.Rows[0]["SPR2ID"].ToString())
                {
                    lbSPR2_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (asd.userid == asd.dts.Rows[0]["SPR3ID"].ToString())
                {
                    lbSPR3_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }

            }
            else if (asd.action == "read")
            {
                string sql = string.Format("select * from TBOM_GDZCTRANSFER as a left join OM_SP as b on a.DH=b.SPFATHERID where TRANSFTYPE='1' and SPFATHERID ='{0}'", asd.id);
                asd.dts = DBCallCommon.GetDTUsingSqlText(sql);
                Repeater1.DataSource = asd.dts;
                Repeater1.DataBind();
                BindPanel(panSP);
            }
            PowerControl();
        }

        private void PowerControl()
        {
            if (asd.action == "check")
            {
                panJBXX.Enabled = false;
                panSPR1.Enabled = false;
                panSPR2.Enabled = false;
                panSPR3.Enabled = false;
                panZDR.Enabled = false;
                if (asd.userid == asd.dts.Rows[0]["SPR1ID"].ToString())
                {
                    panSPR1.Enabled = true;
                }
                if (asd.userid == asd.dts.Rows[0]["SPR2ID"].ToString())
                {
                    panSPR2.Enabled = true;
                }
                if (asd.userid == asd.dts.Rows[0]["SPR3ID"].ToString())
                {
                    panSPR3.Enabled = true;
                }
            }
            else if (asd.action == "read")
            {
                panJBXX.Enabled = false;
                panSPR1.Enabled = false;
                panSPR2.Enabled = false;
                panSPR3.Enabled = false;
                panZDR.Enabled = false;
                btn_submit.Visible = false;
                btnBack.Visible = false;
            }
        }

        private class asd
        {
            public static string action;
            public static string id;
            public static string uesrname;
            public static string userid;
            public static DataTable dts;
        }

        private void BindPanel(Panel panel)//绑定panel
        {
            DataTable dt = asd.dts;
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
                        ddl.SelectedValue = dr[ddl.ID.Substring(3) + "ID"].ToString();
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
                else if (ctr is HiddenField)
                {
                    HiddenField hid = (HiddenField)ctr;
                    if (list_dc.Contains(hid.ID.Substring(3)))
                    {
                        hid.Value = dr[hid.ID.Substring(3)].ToString();
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

        private void InitRepeaterView()
        {
            string sqltext = "select NAME,MODEL,SYR as FORMERNAME,SYRID as FORMERID,BUMEN as FBM ,BUMENID as FBMID,BH as ZYBIANHAO,DATE,PLACE as FPLACE,'' as TIME2,'' as LPLACE,'' as REASON,'' as LATTERID,'' as LATTERNAME,'' as LBM,'' as LBMID from TBOM_TRANSBH";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }

        private void GetBUMEN(DropDownList ddlobject, string value)
        {
            ddlobject.Items.Clear();
            string sql2 = "select DEP_NAME,DEP_CODE FROM TBDS_DEPINFO WHERE DEP_CODE LIKE '[0-9][0-9]'";
            DBCallCommon.BindDdl(ddlobject, sql2, "DEP_NAME", "DEP_CODE");
            ddlobject.SelectedValue = value;
        }
        protected void Repeater1_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                DropDownList ddl_bumen = (DropDownList)e.Item.FindControl("ddl_bumen2");
                HiddenField hfbumen = (HiddenField)e.Item.FindControl("hfbumen2");
                GetBUMEN(ddl_bumen, hfbumen.Value);
                DropDownList ddl_user2 = (DropDownList)e.Item.FindControl("ddl_user2");
                HtmlInputHidden hidSyr2 = e.Item.FindControl("hidSyr2") as HtmlInputHidden;
                HtmlInputHidden hidSyrId2 = e.Item.FindControl("hidSyrId2") as HtmlInputHidden;
                DataRowView drv = (DataRowView)e.Item.DataItem;
                if (drv["LBMID"].ToString() != "")
                {
                    ddl_bumen.SelectedValue = drv["LBMID"].ToString();
                    ddl_bumen.SelectedItem.Text = drv["LBM"].ToString();
                }
                if (drv["LATTERID"].ToString() != "")
                {
                    hidSyrId2.Value = drv["LATTERID"].ToString();
                    hidSyr2.Value = drv["LATTERNAME"].ToString();

                    ddl_user2.Items.Add(new ListItem(drv["LATTERNAME"].ToString(), drv["LATTERID"].ToString()));

                    ddl_user2.SelectedValue = drv["LATTERID"].ToString();
                    ddl_user2.SelectedItem.Text = drv["LATTERNAME"].ToString();
                }
            }
        }

        protected void btn_submit_click(object sender, EventArgs e)
        {
            if (asd.action == "add")
            {
                List<string> list = addlist();
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                    string _emailto = DBCallCommon.GetEmailAddressByUserID(hidSPR1ID.Value);
                    string _body = "非固定资产转移审批任务:"
                           + "\r\n制单人：" + txtZDR.Text.Trim()
                           + "\r\n制单日期：" + lbZDR_SJ.Text.Trim();

                    string _subject = "您有新的【非固定资产转移】需要审批，请及时处理";
                    DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                }
                catch
                {
                    Response.Write("<script>alert('添加转移成功');</script>");
                    return;
                }
                Response.Redirect("OM_FGdzcTransSum.aspx");
            }
            else if (asd.action == "check")
            {
                List<string> list = checklist();
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('审批语句出现问题，请与管理员联系！！！');</script>");
                    return;
                }
                list.Clear();
                string sql = string.Format("select * from TBOM_GDZCTRANSFER as a left join OM_SP as b on a.DH=b.SPFATHERID where TRANSFTYPE='1' and SPFATHERID ='{0}'", asd.id);
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows[0]["SPZT"].ToString() == "y")
                {
                    list = zylist();
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                    }
                    catch
                    {
                        Response.Write("<script>alert('转移语句出现问题，请与管理员联系！！！');</script>");
                        return;
                    }
                }
                Response.Redirect("OM_FGDZCZY_SP.aspx");
            }
        }

        private List<string> addlist()
        {
            List<string> list = new List<string>();
            string sqltxt = "";
            string dh = GetDH();
            string bh = "";
            string name = "";
            string model = "";
            string former = "";
            string formerid = "";
            string latter = "";
            string latterid = "";
            string reason = "";
            string time1 = "";
            string bumen1 = "";
            string bumen1id = "";
            string bumen2 = "";
            string bumen2id = "";
            string time2 = "";
            string place1 = "";
            string place2 = "";
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                bh = ((Label)Repeater1.Items[i].FindControl("lblbh")).Text;
                name = ((Label)Repeater1.Items[i].FindControl("lblName")).Text;
                model = ((Label)Repeater1.Items[i].FindControl("lblModel")).Text;
                former = ((Label)Repeater1.Items[i].FindControl("lblsyr")).Text;
                formerid = ((Label)Repeater1.Items[i].FindControl("lblsyrid")).Text;
                bumen1 = ((Label)Repeater1.Items[i].FindControl("lblsybm")).Text;
                bumen1id = ((Label)Repeater1.Items[i].FindControl("syrbmid")).Text;
                time1 = ((Label)Repeater1.Items[i].FindControl("lbldate")).Text;
                latter = ((HtmlInputHidden)Repeater1.Items[i].FindControl("hidSyr2")).Value.ToString();
                latterid = ((HtmlInputHidden)Repeater1.Items[i].FindControl("hidSyrId2")).Value.ToString();
                bumen2 = ((DropDownList)Repeater1.Items[i].FindControl("ddl_bumen2")).SelectedItem.Text;
                bumen2id = ((DropDownList)Repeater1.Items[i].FindControl("ddl_bumen2")).SelectedValue.ToString();
                time2 = ((TextBox)Repeater1.Items[i].FindControl("txtdate2")).Text;
                reason = ((TextBox)Repeater1.Items[i].FindControl("txtreason")).Text;
                place1 = ((Label)Repeater1.Items[i].FindControl("lblplace1")).Text;
                place2 = ((TextBox)Repeater1.Items[i].FindControl("txtplace2")).Text;
                sqltxt = "insert into TBOM_GDZCTRANSFER (DH,ZYBIANHAO,NAME,MODEL,FORMERNAME,FORMERID,LATTERID,LATTERNAME,REASON,TIME1,TIME2,JBR,JBRID,FBM,FBMID,LBM,LBMID,FPLACE,LPLACE,TRANSFTYPE) VALUES ('" + dh + "','" + bh + "','" + name + "','" + model + "','" + former + "','" + formerid + "','" + latterid + "','" + latter + "','" + reason + "','" + time1 + "','" + time2 + "','" + Session["UserName"].ToString() + "','" + Session["UserID"].ToString() + "','" + bumen1 + "','" + bumen1id + "','" + bumen2 + "','" + bumen2id + "','" + place1 + "','" + place2 + "','1')";
                list.Add(sqltxt);
                //sqltxt = "update TBOM_GDZCIN SET SYR='" + latter + "',SYRID='" + latterid + "',SYBUMEN='" + bumen2 + "',SYBUMENID='" + bumen2id + "',SYDATE='" + time2 + "',PLACE='" + place2 + "' WHERE BIANHAO='" + bh + "'";
                //list.Add(sqltxt);

            }
            sqltxt = string.Format("insert into OM_SP (SPFATHERID,SPLX,SPJB,ZDR,ZDRID,ZDR_SJ,ZDR_JY,SPR1,SPR1ID,SPR1_JL,SPR1_SJ,SPR1_JY,SPR2,SPR2ID,SPR2_JL,SPR2_SJ,SPR2_JY,SPR3,SPR3ID,SPR3_JL,SPR3_SJ,SPR3_JY,SPZT) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')", dh, "FGDZCZY", rblSPJB.SelectedValue, txtZDR.Text.Trim(), hidZDRID.Value, lbZDR_SJ.Text, txtZDR_JY.Text.Trim(), txtSPR1.Text.Trim(), hidSPR1ID.Value, "", "", "", txtSPR2.Text.Trim(), hidSPR2ID.Value, "", "", "", txtSPR3.Text.Trim(), hidSPR3ID.Value, "", "", "", "0");
            list.Add(sqltxt);
            return list;
        }

        private string GetDH()
        {
            string dh = "";
            string sql = "select max(DH) as DH from TBOM_GDZCTRANSFER where TRANSFTYPE='1'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() == "")
            {
                dh = "FG-ZY00001";
            }
            else
            {
                dh = dt.Rows[0][0].ToString().Split('Y')[0] + "Y" + (CommonFun.ComTryInt(dt.Rows[0][0].ToString().Split('Y')[1]) + 1).ToString().PadLeft(5, '0');
            }
            return dh;
        }//获取转移单号

        private List<string> checklist()
        {
            List<string> list = new List<string>();
            string sql = string.Format("update OM_SP set SPR1_JL='{0}',SPR1_SJ='{1}',SPR1_JY='{2}',SPR2_JL='{3}',SPR2_SJ='{4}',SPR2_JY='{5}',SPR3_JL='{6}',SPR3_SJ='{7}',SPR3_JY='{8}' where SPFATHERID='{9}'", rblSPR1_JL.SelectedValue, lbSPR1_SJ.Text, txtSPR1_JY.Text.Trim(), rblSPR2_JL.SelectedValue, lbSPR2_SJ.Text, txtSPR2_JY.Text.Trim(), rblSPR3_JL.SelectedValue, lbSPR3_SJ.Text, txtSPR3_JY.Text, asd.id);
            list.Add(sql);
            if (asd.dts.Rows[0]["SPJB"].ToString() == "1")
            {
                if (rblSPR1_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='y' where SPFATHERID='{0}'", asd.id);
                    list.Add(sql);

                }
            }
            else if (asd.dts.Rows[0]["SPJB"].ToString() == "2")
            {
                if (rblSPR1_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='1y' where SPFATHERID='{0}'", asd.id);
                    list.Add(sql);
                    if (asd.userid == hidSPR1ID.Value)
                    {
                        string _emailto = DBCallCommon.GetEmailAddressByUserID(hidSPR2ID.Value);
                        string _body = "非固定资产转移审批任务:"
                              + "\r\n制单人：" + txtZDR.Text.Trim()
                              + "\r\n制单日期：" + lbZDR_SJ.Text.Trim();

                        string _subject = "您有新的【非固定资产转移】需要审批，请及时处理";
                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                    }
                }
                if (rblSPR2_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='y' where SPFATHERID='{0}'", asd.id);
                    list.Add(sql);
                }
            }
            else if (asd.dts.Rows[0]["SPJB"].ToString() == "3")
            {
                if (rblSPR1_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='1y' where SPFATHERID='{0}'", asd.id);
                    list.Add(sql);
                    if (asd.userid == hidSPR1ID.Value)
                    {
                        string _emailto = DBCallCommon.GetEmailAddressByUserID(hidSPR2ID.Value);
                        string _body = "非固定资产转移审批任务:"
                              + "\r\n制单人：" + txtZDR.Text.Trim()
                              + "\r\n制单日期：" + lbZDR_SJ.Text.Trim();

                        string _subject = "您有新的【非固定资产转移】需要审批，请及时处理";
                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                    }
                }
                if (rblSPR2_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='2y' where SPFATHERID='{0}'", asd.id);
                    list.Add(sql);
                    if (asd.userid == hidSPR2ID.Value)
                    {
                        string _emailto = DBCallCommon.GetEmailAddressByUserID(hidSPR3ID.Value);
                        string _body = "非固定资产转移审批任务:"
                              + "\r\n制单人：" + txtZDR.Text.Trim()
                              + "\r\n制单日期：" + lbZDR_SJ.Text.Trim();

                        string _subject = "您有新的【非固定资产转移】需要审批，请及时处理";
                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                    }
                }
                if (rblSPR3_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='y' where SPFATHERID='{0}'", asd.id);
                    list.Add(sql);
                }
            }
            if (rblSPR1_JL.SelectedValue == "n" || rblSPR2_JL.SelectedValue == "n" || rblSPR3_JL.SelectedValue == "n")
            {
                sql = string.Format("update OM_SP set SPZT='n' where SPFATHERID='{0}'", asd.id);
                list.Add(sql);
            }
            return list;
        }

        private List<string> zylist()
        {
            List<string> list = new List<string>();
            string sqltxt = "";
            string bh = "";
            string name = "";
            string model = "";
            string former = "";
            string formerid = "";
            string latter = "";
            string latterid = "";
            string reason = "";
            string time1 = "";
            string bumen1 = "";
            string bumen1id = "";
            string bumen2 = "";
            string bumen2id = "";
            string time2 = "";
            string place1 = "";
            string place2 = "";
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                bh = ((Label)Repeater1.Items[i].FindControl("lblbh")).Text;
                name = ((Label)Repeater1.Items[i].FindControl("lblName")).Text;
                model = ((Label)Repeater1.Items[i].FindControl("lblModel")).Text;
                former = ((Label)Repeater1.Items[i].FindControl("lblsyr")).Text;
                formerid = ((Label)Repeater1.Items[i].FindControl("lblsyrid")).Text;
                bumen1 = ((Label)Repeater1.Items[i].FindControl("lblsybm")).Text;
                bumen1id = ((Label)Repeater1.Items[i].FindControl("syrbmid")).Text;
                time1 = ((Label)Repeater1.Items[i].FindControl("lbldate")).Text;
                latter = ((HtmlInputHidden)Repeater1.Items[i].FindControl("hidSyr2")).Value.ToString();
                latterid = ((HtmlInputHidden)Repeater1.Items[i].FindControl("hidSyrId2")).Value.ToString();
                bumen2 = ((DropDownList)Repeater1.Items[i].FindControl("ddl_bumen2")).SelectedItem.Text;
                bumen2id = ((DropDownList)Repeater1.Items[i].FindControl("ddl_bumen2")).SelectedValue.ToString();
                time2 = ((TextBox)Repeater1.Items[i].FindControl("txtdate2")).Text;
                reason = ((TextBox)Repeater1.Items[i].FindControl("txtreason")).Text;
                place1 = ((Label)Repeater1.Items[i].FindControl("lblplace1")).Text;
                place2 = ((TextBox)Repeater1.Items[i].FindControl("txtplace2")).Text;
                //sqltxt = "insert into TBOM_GDZCTRANSFER (DH,ZYBIANHAO,NAME,MODEL,FORMERNAME,FORMERID,LATTERID,LATTERNAME,REASON,TIME1,TIME2,JBR,JBRID,FBM,FBMID,LBM,LBMID,FPLACE,LPLACE,TRANSFTYPE) VALUES ('" + dh + "','" + bh + "','" + name + "','" + model + "','" + former + "','" + formerid + "','" + latterid + "','" + latter + "','" + reason + "','" + time1 + "','" + time2 + "','" + Session["UserName"].ToString() + "','" + Session["UserID"].ToString() + "','" + bumen1 + "','" + bumen1id + "','" + bumen2 + "','" + bumen2id + "','" + place1 + "','" + place2 + "','0')";
                //list.Add(sqltxt);
                sqltxt = "update TBOM_GDZCIN SET SYR='" + latter + "',SYRID='" + latterid + "',SYBUMEN='" + bumen2 + "',SYBUMENID='" + bumen2id + "',SYDATE='" + time2 + "',PLACE='" + place2 + "' WHERE BIANHAO='" + bh + "'";
                list.Add(sqltxt);
            }
            return list;
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("OM_FGdzcStore.aspx");
        }
    }
}
