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
    public partial class OM_CARCK_detail : System.Web.UI.Page
    {
      
            string flag = string.Empty;
            string id = string.Empty;
            string mingchen = "";
            string guige = "";
            string shuliang = "";
            string danjia = "";
            string danwei = "";
            protected void Page_Load(object sender, EventArgs e)
            {
                flag = Request.QueryString["FLAG"].ToString();
                id = Request.QueryString["id"].ToString();
                if (!IsPostBack)
                {
                    asd.userid = Session["UserID"].ToString();
                    BindData();
                }

            }

            protected void btnReturn_OnClick(object sender, EventArgs e)
            {
                Response.Redirect("OM_CARRK.aspx");
            }
            private class asd
            {
                public static string userid;
                public static string bh;
                public static DataTable dts;
            }
            private void BindData()
            {
                if (flag == "add")
                {
                    lbl_creater.Text = Session["UserName"].ToString();
                    lblInDate.Text = DateTime.Now.ToString();
                    txtZDR.Text = Session["UserName"].ToString();
                    lbZDR_SJ.Text = DateTime.Now.ToString();
                    hidZDRID.Value = Session["UserID"].ToString();
                    InitRepeaterView();
                }
                else if (flag == "check")
                {
                    string sql = string.Format("select * from OM_CARCK_SP WHERE ZDR_SJ='{0}'", id);
                    asd.dts = DBCallCommon.GetDTUsingSqlText(sql);
                    Repeater1.DataSource = asd.dts;
                    Repeater1.DataBind();
                    lbl_creater.Text = asd.dts.Rows[0]["ZDR"].ToString();
                    lblInDate.Text = asd.dts.Rows[0]["ZDR_SJ"].ToString();
                    BindPanel(panSP);



                }
                else if (flag == "read")
                {
                    string sql = string.Format("select * from OM_CARCK_SP WHERE ZDR_SJ='{0}'", id);
                    asd.dts = DBCallCommon.GetDTUsingSqlText(sql);
                    Repeater1.DataSource = asd.dts;
                    Repeater1.DataBind();
                    lbl_creater.Text = asd.dts.Rows[0]["ZDR"].ToString();
                    lblInDate.Text =asd.dts.Rows[0]["ZDR_SJ"].ToString();
                    BindPanel(panSP);
                }
                PowerControl();
            }
            private void InitRepeaterView()
            {
                string sql = "select *,'' as SP_SL from OM_CAR_ZZB ";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
                lbl_creater.Text = Session["UserName"].ToString();
                lblInDate.Text = DateTime.Now.ToString();

            }
            private void BindPanel(Panel panel)//绑定panel
            {
                string sql = "select * from OM_CARCK_SP WHERE ZDR_SJ='" + id + "'";
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
            private void PowerControl()
            {
                if (flag == "check")
                {
                    panJBXX.Enabled = false;
                    panZDR.Enabled = false;
                    panSPR1.Enabled = false;
                    panSPR2.Enabled = false;
                    if (asd.userid == asd.dts.Rows[0]["SPR1_ID"].ToString())
                    {
                        panSPR1.Enabled = true;
                    }
                    if (asd.userid == asd.dts.Rows[0]["SPR2_ID"].ToString())
                    {
                        panSPR2.Enabled = true;
                    }
                }
                else if (flag == "read")
                {
                    btnSave.Visible = false;
                    btnReturn.Visible = false;


                    panZDR.Enabled = false;
                    panSPR1.Enabled = false;
                    panSPR2.Enabled = false;

                }
            }
            private void writedata()
            {
                List<string> list_sql = new List<string>();
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {


                    if (((TextBox)Repeater1.Items[i].FindControl("txt_shuliang")).Text != "")
                    {

                        mingchen = ((Label)Repeater1.Items[i].FindControl("lblbh")).Text;
                        guige = ((Label)Repeater1.Items[i].FindControl("lblName")).Text;;
                        danjia = ((Label)Repeater1.Items[i].FindControl("lbldanjia")).Text;
                        danwei = ((Label)Repeater1.Items[i].FindControl("lbdanwei")).Text;
                        shuliang = ((TextBox)Repeater1.Items[i].FindControl("txt_shuliang")).Text;
                        double dj = CommonFun.ComTryDouble(danjia);
                        double sl = CommonFun.ComTryDouble(shuliang);
                        //比较库存数量与出库数量大小
                        string sql = "select * from OM_CARKUCUN where KC_MC='" + mingchen + "' and KC_GG='" + guige + "'and KC_DJ='" + dj + "'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        string kc_sl=dt.Rows[0]["KC_SL"].ToString();
                        double kcsl = CommonFun.ComTryDouble(kc_sl);
                        if (kcsl >= sl)
                        {
                            double zongjia = dj * sl;
                            string zj = zongjia.ToString();

                            //string sqltext = "insert into OM_CARKUCUN(KC_MC,KC_GG,KC_SL,KC_DJ,KC_ZJ,KC_SJ)";
                            //sqltext += "values('" + mingchen + "','" + guige + "','" + shuliang + "','" + danjia + "','" + zongjia + "','" + lblInDate.Text + "')";
                            //list_sql.Add(sqltext);


                            string sqltext1 = string.Format("insert into OM_CARCK_SP (SP_MC,SP_GG,SP_SL,ZDR,ZDRID,ZDR_SJ,ZDR_JY,SPR1,SPR1_ID,SPR1_JL,SPR1_JY,SPR2,SPR2_ID,SPR2_JL,SPR2_JY,SP_DJ,SP_DANWEI,SPJB,SPZT,SP_ZJ) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')", mingchen, guige, shuliang, txtZDR.Text.Trim(), hidZDRID.Value, lbZDR_SJ.Text, txtZDR_JY.Text.Trim(), txtSPR1.Text.Trim(), hidSPR1_ID.Value, "", "", txtSPR2.Text.Trim(), hidSPR2_ID.Value, "", "", danjia, danwei, rblSPJB.SelectedValue, "0", zj);
                            list_sql.Add(sqltext1);
                        }
                        else
                        {
                            Response.Write("<script>alert('库存数量不足，请查看正确数量！！！')</script>");
                            Response.Redirect("OM_CarKuCun.aspx");
                        }


                    }
                    else
                    {
                        Response.Write("<script>alert('请选择“名称、数量”再提交！！！')</script>");
                    }
                   
                }
                DBCallCommon.ExecuteTrans(list_sql);
                string _emailto = DBCallCommon.GetEmailAddressByUserID(hidSPR1_ID.Value);
                string _body = "车品出库审批任务:"
                      + "\r\n制单人：" + lbl_creater.Text.Trim()
                      + "\r\n制单日期：" + lblInDate.Text.Trim();

                string _subject = "您有新的【车品出库】需要审批，请及时处理";
                DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
            }
            protected void btnSave_OnClick(object sender, EventArgs e)
            {
                string st = "OK";
                if (Repeater1.Items.Count == 0)
                {
                    st = "NoData";
                }
                if (st == "OK")
                {
                    if (flag == "add")
                    {
                        writedata();
                        Response.Write("<script>alert('保存成功！');window.location.href='OM_CARRK.aspx';</script>");
                    }
                    if (flag == "check")
                    {
                        if (asd.userid == asd.dts.Rows[0]["SPR1_ID"].ToString())
                        {
                            if (rblSPR1_JL.SelectedValue != "y" && rblSPR1_JL.SelectedValue != "n")
                            {
                                Response.Write("<script>alert('请选择“同意”或“不同意”后再提交！！！')</script>");
                                return;
                            }
                        }
                        else if (asd.userid == asd.dts.Rows[0]["SPR2_ID"].ToString())
                        {
                            if (rblSPR2_JL.SelectedValue != "y" && rblSPR2_JL.SelectedValue != "n")
                            {
                                Response.Write("<script>alert('请选择“同意”或“不同意”后再提交！！！')</script>");
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
                            Response.Write("<script>alert('审批语句出现错误，请与管理员联系！！！')</script>");
                            return;
                        }
                        Response.Redirect("OM_CARCK_SP.aspx");
                    }

                }
                else if (st == "NoData")
                {
                    Response.Write("<script>alert('提示:无法保存！！！没有入库数据！！！')</script>");
                    return;
                }
            }
            private List<string> checklist()
            {
               
                List<string> list = new List<string>();
                string sql = string.Format("update OM_CARCK_SP set SPR1_JL='{0}',SPR1_JY='{1}',SPR2_JL='{2}',SPR2_JY='{3}' where ZDR_SJ='{4}'", rblSPR1_JL.SelectedValue, txtSPR1_JY.Text.Trim(), rblSPR2_JL.SelectedValue, txtSPR2_JY.Text.Trim(), id);
                list.Add(sql);
                if (asd.dts.Rows[0]["SPJB"].ToString() == "1")
                {
                    if (rblSPR1_JL.SelectedValue == "y")
                    {
                        sql = string.Format("update OM_CARCK_SP set SPZT='y' where ZDR_SJ='{0}'", id);
                        list.Add(sql);
                        string sqlsp = "select * from OM_CARCK_SP where ZDR_SJ='" + id + "'";
                        DataTable dtsp = DBCallCommon.GetDTUsingSqlText(sqlsp);
                        for (int i = 0; i < dtsp.Rows.Count; i++)
                        {
                            string mc = dtsp.Rows[i]["SP_MC"].ToString();
                            string gg = dtsp.Rows[i]["SP_GG"].ToString();
                            string sl = dtsp.Rows[i]["SP_SL"].ToString();
                            string dj = dtsp.Rows[i]["SP_DJ"].ToString();
                            string zj = dtsp.Rows[i]["SP_ZJ"].ToString();
                            string danwei = dtsp.Rows[0]["SP_DANWEI"].ToString();
                            string sqlkc = "select * from OM_CARKUCUN where KC_MC='" + mc + "' and KC_GG='" + gg + "'and KC_DJ='" + dj + "'";
                            DataTable dtkc = DBCallCommon.GetDTUsingSqlText(sqlkc);
                            string kc_sl = dtkc.Rows[0]["KC_SL"].ToString();
                            string kc_zj = dtkc.Rows[0]["KC_ZJ"].ToString();
                            double a = CommonFun.ComTryDouble(sl);
                            double b = CommonFun.ComTryDouble(kc_sl);
                            double c = b - a;
                            double d = CommonFun.ComTryDouble(zj);
                            double e = CommonFun.ComTryDouble(kc_zj);
                            double f = e - d;

                            string sqltext = "update OM_CARKUCUN set KC_SL='" + c.ToString() + "' ,KC_ZJ='" + f.ToString() + "' where KC_MC='" + mc + "' and KC_GG='" + gg + "'and KC_DJ='" + dj + "'";
                            DBCallCommon.ExeSqlText(sqltext);
                        }
                       
                    }
                }
                else if (asd.dts.Rows[0]["SPJB"].ToString() == "2")
                {
                    if (rblSPR1_JL.SelectedValue == "y")
                    {
                        sql = string.Format("update OM_CARCK_SP set SPZT='1y' where ZDR_SJ='{0}'", id);
                        list.Add(sql);
                        if (asd.userid == hidSPR1_ID.Value)
                        {
                            string _emailto = DBCallCommon.GetEmailAddressByUserID(hidSPR2_ID.Value);
                            string _body = "车品出库审批任务:"
                                  + "\r\n制单人：" + lbl_creater.Text.Trim()
                                  + "\r\n制单日期：" + lblInDate.Text.Trim();

                            string _subject = "您有新的【车品出库】需要审批，请及时处理";
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                    }
                    if (rblSPR2_JL.SelectedValue == "y")
                    {
                        sql = string.Format("update OM_CARCK_SP set SPZT='y' where ZDR_SJ='{0}'", id);
                        list.Add(sql);
                        string sqlsp = "select * from OM_CARCK_SP where ZDR_SJ='" + id + "'";
                        DataTable dtsp = DBCallCommon.GetDTUsingSqlText(sqlsp);
                        for (int i = 0; i < dtsp.Rows.Count; i++)
                        {
                            string mc = dtsp.Rows[i]["SP_MC"].ToString();
                            string gg = dtsp.Rows[i]["SP_GG"].ToString();
                            string sl = dtsp.Rows[i]["SP_SL"].ToString();
                            string dj = dtsp.Rows[i]["SP_DJ"].ToString();
                            string zj = dtsp.Rows[i]["SP_ZJ"].ToString();
                            string danwei = dtsp.Rows[0]["SP_DANWEI"].ToString();
                            string sqlkc = "select * from OM_CARKUCUN where KC_MC='" + mc + "' and KC_GG='" + gg + "'and KC_DJ='" + dj + "'";
                            DataTable dtkc = DBCallCommon.GetDTUsingSqlText(sqlkc);
                            string kc_sl = dtkc.Rows[0]["KC_SL"].ToString();
                            string kc_zj = dtkc.Rows[0]["KC_ZJ"].ToString();
                            double a = CommonFun.ComTryDouble(sl);
                            double b = CommonFun.ComTryDouble(kc_sl);
                            double c = b - a;
                            double d = CommonFun.ComTryDouble(zj);
                            double e = CommonFun.ComTryDouble(kc_zj);
                            double f = e - d;

                            string sqltext = "update OM_CARKUCUN set KC_SL='" + c.ToString() + "' ,KC_ZJ='" + f.ToString() + "' where KC_MC='" + mc + "' and KC_GG='" + gg + "'and KC_DJ='" + dj + "'";
                            DBCallCommon.ExeSqlText(sqltext);
                        }
                    }
                }

                if (rblSPR1_JL.SelectedValue == "n" || rblSPR2_JL.SelectedValue == "n")
                {
                    sql = string.Format("update OM_CARCK_SP set SPZT='n' where ZDR_SJ='{0}'", id);
                    list.Add(sql);
                }
                return list;
            }
 
            protected void Repeater1_OnItemDataBound(object sender, RepeaterItemEventArgs e)
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {

                }
            }
        }
    }

