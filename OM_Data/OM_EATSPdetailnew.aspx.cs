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
using System.IO;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_EATSPdetailnew : System.Web.UI.Page
    {
        string spbh = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindmxdata();//绑定数据
                bindspdata();//绑定审批数据
                contrlkjx();//控件可见性和可用性
            }
        }
        //明细数据
        private void bindmxdata()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqltext = "select * from OM_EATNEW where eatbh='" + spbh.ToString().Trim() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                txtCode.Text = dt.Rows[0]["eatbh"].ToString().Trim();
                lbsqdate.Text = dt.Rows[0]["eatsqtime"].ToString().Trim();
                rad_yctype.SelectedValue = dt.Rows[0]["eattype"].ToString().Trim();
                txt_apply.Text = dt.Rows[0]["eatsqrname"].ToString().Trim();
                txt_phone.Text = dt.Rows[0]["eatsqrphone"].ToString().Trim();
                tb_ycdate.Text = dt.Rows[0]["eatyctime"].ToString().Trim();
                txt_contents.Text = dt.Rows[0]["eatsqrnote"].ToString().Trim();
            }
            //用餐
            string sqltextmx = "select *,(detailnum*detailprice) as detailmoney from OM_EATNEWDETAIL where detailbh='" + spbh + "' and detailifYP='2'";
            DataTable dtmx = DBCallCommon.GetDTUsingSqlText(sqltextmx);
            if (dtmx.Rows.Count > 0)
            {
                tbycrens.Text = dtmx.Rows[0]["detailnum"].ToString().Trim();
                tbycguige.Text = dtmx.Rows[0]["detailprice"].ToString().Trim();
            }
            //饮品
            string sqltextmxyp = "select *,(detailnum*detailprice) as detailmoney from OM_EATNEWDETAIL where detailbh='" + spbh + "' and detailifYP='1'";
            DataTable dtmxyp = DBCallCommon.GetDTUsingSqlText(sqltextmxyp);
            if (dtmxyp.Rows.Count > 0)
            {
                rptycsqspdetail.DataSource = dtmxyp;
                rptycsqspdetail.DataBind();
            }
        }

        //审批数据
        private void bindspdata()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqlshdata = "select * from OM_EATNEW where eatbh='" + spbh + "'";
            DataTable dtsh = DBCallCommon.GetDTUsingSqlText(sqlshdata);
            if (dtsh.Rows.Count > 0)
            {
                txt_first.Text = dtsh.Rows[0]["eatshrname1"].ToString().Trim();
                firstid.Value = dtsh.Rows[0]["eatshrid1"].ToString().Trim();
                if (dtsh.Rows[0]["eatshstate1"].ToString().Trim() == "1" || dtsh.Rows[0]["eatshstate1"].ToString().Trim() == "2")
                {
                    rblfirst.SelectedValue = dtsh.Rows[0]["eatshstate1"].ToString().Trim();
                }
                first_time.Text = dtsh.Rows[0]["eatshtime1"].ToString().Trim();
                opinion1.Text = dtsh.Rows[0]["eatshnote1"].ToString().Trim();

                if (dtsh.Rows[0]["eatshrname2"].ToString().Trim() != "" && dtsh.Rows[0]["eatshrid2"].ToString().Trim() != "")
                {
                    txt_second.Text = dtsh.Rows[0]["eatshrname2"].ToString().Trim();
                    secondid.Value = dtsh.Rows[0]["eatshrid2"].ToString().Trim();
                }
                else
                {
                    txt_second.Text = "赵洪新";
                    secondid.Value = "260";
                }
                if (dtsh.Rows[0]["eatshstate2"].ToString().Trim() == "1" || dtsh.Rows[0]["eatshstate2"].ToString().Trim() == "2")
                {
                    rblsecond.SelectedValue = dtsh.Rows[0]["eatshstate2"].ToString().Trim();
                }
                second_time.Text = dtsh.Rows[0]["eatshtime2"].ToString().Trim();
                opinion2.Text = dtsh.Rows[0]["eatshnote2"].ToString().Trim();
            }
        }


        //控件可用性
        private void contrlkjx()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqlkjdata = "select * from OM_EATNEW where eatbh='" + spbh + "'";
            DataTable dtkj = DBCallCommon.GetDTUsingSqlText(sqlkjdata);
            if (dtkj.Rows.Count > 0)
            {
                if (dtkj.Rows[0]["eatstate"].ToString().Trim() == "2")
                {
                    ImageVerify.Visible = true;
                }
                if (dtkj.Rows[0]["eatstate"].ToString().Trim() == "0")//初始化
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["eatsqrid"].ToString().Trim())
                    {
                        rad_yctype.Enabled = true;
                        txt_phone.Enabled = true;
                        tb_ycdate.Enabled = true;
                        txt_contents.Enabled = true;
                        tbycrens.Enabled = true;
                        tbycguige.Enabled = true;

                        btndelete.Visible = true;
                        btnSave.Visible = true;
                        txt_first.Enabled = true;
                        hlSelect1.Visible = true;
                        txt_second.Enabled = true;
                        hlSelect2.Visible = true;
                    }
                }
                else if (dtkj.Rows[0]["eatstate"].ToString().Trim() == "1" && dtkj.Rows[0]["eatshstate1"].ToString().Trim() == "0")//提交未审
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["eatshrid1"].ToString().Trim())
                    {
                        btnSave.Visible = true;
                        rblfirst.Enabled = true;
                        opinion1.Enabled = true;
                    }
                }
                else if (dtkj.Rows[0]["eatstate"].ToString().Trim() == "1" && dtkj.Rows[0]["eatshstate1"].ToString().Trim() == "1" && dtkj.Rows[0]["eatshstate2"].ToString().Trim() == "0")//一级审核通过
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["eatshrid2"].ToString().Trim())
                    {
                        btnSave.Visible = true;
                        rblsecond.Enabled = true;
                        opinion2.Enabled = true;
                    }
                }

                else if (dtkj.Rows[0]["eatstate"].ToString().Trim() == "3")
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["eatsqrid"].ToString().Trim())
                    {
                        tbycrens.Enabled = true;
                        tbycguige.Enabled = true;

                        btnSave.Visible = true;
                        txt_contents.Enabled = true;
                        btndelete.Visible = true;
                    }
                }
            }
        }


        //删除
        protected void btndelete_OnClick(object sender, EventArgs e)
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            List<string> list = new List<string>();
            for (int i = 0; i < rptycsqspdetail.Items.Count; i++)
            {
                if (((CheckBox)rptycsqspdetail.Items[i].FindControl("chk")).Checked)
                {
                    string sqldelete = "delete from OM_EATNEWDETAIL where IDdetail='" + ((Label)rptycsqspdetail.Items[i].FindControl("IDdetail")).Text.Trim() + "'";
                    list.Add(sqldelete);
                }
            }
            DBCallCommon.ExecuteTrans(list);
            Response.Redirect("OM_EATSPdetailnew.aspx?spid=" + spbh);
        }


         //提交
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string sql000 = "";
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqlsavedata = "select * from OM_EATNEW where eatbh='" + spbh + "'";
            DataTable dtsave = DBCallCommon.GetDTUsingSqlText(sqlsavedata);
            if (dtsave.Rows.Count > 0)
            {
                if (dtsave.Rows[0]["eatsqrid"].ToString().Trim() == Session["UserID"].ToString().Trim() && dtsave.Rows[0]["eatstate"].ToString().Trim() == "0")
                {
                    if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "" || txt_second.Text.Trim() == "" || secondid.Value.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                        return;
                    }
                    if (tb_ycdate.Text.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请选择用餐时间！');", true);
                        return;
                    }
                    if (rad_yctype.SelectedValue != "1" && rad_yctype.SelectedValue != "2")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请选择用餐类型！');", true);
                        return;
                    }
                    if (tbycrens.Text.Trim() == "" || tbycguige.Text.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请填写用餐规格和用餐人数！');", true);
                        return;
                    }
                    else
                    {
                        sql000 = "update OM_EATNEW set eatyctime='" + tb_ycdate.Text.Trim() + "',eattype='" + rad_yctype.SelectedValue.Trim() + "',eatsqrphone='" + txt_phone.Text.Trim() + "',eatsqrnote='" + txt_contents.Text.Trim() + "',eatstate='1',eatshrid1='" + firstid.Value.Trim() + "',eatshrname1='" + txt_first.Text.Trim() + "',eatshrid2='" + secondid.Value.Trim() + "',eatshrname2='" + txt_second.Text.Trim() + "' where eatbh='" + spbh + "'";
                        list.Add(sql000);


                        string sqlupdateyc = "update OM_EATNEWDETAIL set detailnum=" + CommonFun.ComTryDecimal(tbycrens.Text.Trim()) + ",detailprice=" + CommonFun.ComTryDecimal(tbycguige.Text.Trim()) + " where detailbh='" + spbh + "' and detailifYP='2'";
                        list.Add(sqlupdateyc);

                        for (int k = 0; k < rptycsqspdetail.Items.Count; k++)
                        {
                            if (CommonFun.ComTryDecimal(((TextBox)rptycsqspdetail.Items[k].FindControl("txt3")).Text.Trim()) > 0 || CommonFun.ComTryDecimal(((TextBox)rptycsqspdetail.Items[k].FindControl("txt5")).Text.Trim()) > 0)
                            {
                                TextBox txt1 = (TextBox)rptycsqspdetail.Items[k].FindControl("txt1");
                                TextBox txt2 = (TextBox)rptycsqspdetail.Items[k].FindControl("txt2");
                                TextBox txt3 = (TextBox)rptycsqspdetail.Items[k].FindControl("txt3");
                                TextBox txt4 = (TextBox)rptycsqspdetail.Items[k].FindControl("txt4");
                                TextBox txt5 = (TextBox)rptycsqspdetail.Items[k].FindControl("txt5");
                                string sqlupdatedetail = "update OM_EATNEWDETAIL set detailthing='" + txt1.Text.Trim() + "',detailclass='" + txt2.Text.Trim() + "',detailnum=" + CommonFun.ComTryDecimal(txt3.Text.Trim()) + ",detailunit='" + txt4.Text.Trim() + "',detailprice=" + CommonFun.ComTryDecimal(txt5.Text.Trim()) + " where IDdetail='" + ((Label)rptycsqspdetail.Items[k].FindControl("IDdetail")).Text.Trim() + "'";
                                list.Add(sqlupdatedetail);
                            }
                        }
                    }
                }

                if (dtsave.Rows[0]["eatshrid1"].ToString().Trim() == Session["UserID"].ToString().Trim()&&dtsave.Rows[0]["eatstate"].ToString().Trim() == "1")
                {
                    if (rblfirst.SelectedValue.ToString().Trim() == "1")
                    {
                        sql000 = "update OM_EATNEW set eatshstate1='1',eatshtime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',eatshnote1='" + opinion1.Text.Trim() + "' where eatbh='" + spbh + "'";
                        list.Add(sql000);
                    }
                    else if (rblfirst.SelectedValue.ToString().Trim() == "2")
                    {
                        sql000 = "update OM_EATNEW set eatstate='3',eatshstate1='2',eatshtime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',eatshnote1='" + opinion1.Text.Trim() + "' where eatbh='" + spbh + "'";
                        list.Add(sql000);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核意见！');", true);
                        return;
                    }
                }

                if (dtsave.Rows[0]["eatshrid2"].ToString().Trim() == Session["UserID"].ToString().Trim() && dtsave.Rows[0]["eatstate"].ToString().Trim() == "1")
                {
                    if (rblsecond.SelectedValue.ToString().Trim() == "1")
                    {
                        sql000 = "update OM_EATNEW set eatstate='2',eatshstate2='1',eatshtime2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',eatshnote2='" + opinion2.Text.Trim() + "' where eatbh='" + spbh + "'";
                        list.Add(sql000);
                    }
                    else if (rblsecond.SelectedValue.ToString().Trim() == "2")
                    {
                        sql000 = "update OM_EATNEW set eatstate='3',eatshstate2='2',eatshtime2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',eatshnote2='" + opinion2.Text.Trim() + "' where eatbh='" + spbh + "'";
                        list.Add(sql000);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核意见！');", true);
                        return;
                    }
                }

                if (dtsave.Rows[0]["eatsqrid"].ToString().Trim() == Session["UserID"].ToString().Trim() && dtsave.Rows[0]["eatstate"].ToString().Trim() == "3")
                {
                    if (tbycrens.Text.Trim() == "" || tbycguige.Text.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请填写用餐规格和用餐人数！');", true);
                        return;
                    }
                    else
                    {
                        sql000 = "update OM_EATNEW set eatsqrnote='" + txt_contents.Text.Trim() + "',eatstate='1',eatshstate1='0',eatshstate2='0' where eatbh='" + spbh + "'";
                        list.Add(sql000);

                        string sqlupdateyc = "update OM_EATNEWDETAIL set detailnum=" + CommonFun.ComTryDecimal(tbycrens.Text.Trim()) + ",detailprice=" + CommonFun.ComTryDecimal(tbycguige.Text.Trim()) + " where detailbh='" + spbh + "' and detailifYP='2'";
                        list.Add(sqlupdateyc);

                        for (int k = 0; k < rptycsqspdetail.Items.Count; k++)
                        {
                            if (CommonFun.ComTryDecimal(((TextBox)rptycsqspdetail.Items[k].FindControl("txt3")).Text.Trim()) > 0 || CommonFun.ComTryDecimal(((TextBox)rptycsqspdetail.Items[k].FindControl("txt5")).Text.Trim()) > 0)
                            {
                                TextBox txt1 = (TextBox)rptycsqspdetail.Items[k].FindControl("txt1");
                                TextBox txt2 = (TextBox)rptycsqspdetail.Items[k].FindControl("txt2");
                                TextBox txt3 = (TextBox)rptycsqspdetail.Items[k].FindControl("txt3");
                                TextBox txt4 = (TextBox)rptycsqspdetail.Items[k].FindControl("txt4");
                                TextBox txt5 = (TextBox)rptycsqspdetail.Items[k].FindControl("txt5");
                                string sqlupdatedetail = "update OM_EATNEWDETAIL set detailthing='" + txt1.Text.Trim() + "',detailclass='" + txt2.Text.Trim() + "',detailnum=" + CommonFun.ComTryDecimal(txt3.Text.Trim()) + ",detailunit='" + txt4.Text.Trim() + "',detailprice=" + CommonFun.ComTryDecimal(txt5.Text.Trim()) + " where IDdetail='" + ((Label)rptycsqspdetail.Items[k].FindControl("IDdetail")).Text.Trim() + "'";
                                list.Add(sqlupdatedetail);
                            }
                        }
                    }
                }
            }
            DBCallCommon.ExecuteTrans(list);
            Response.Redirect("OM_EATSPdetailnew.aspx?spid=" + spbh);
        }
    }
}
