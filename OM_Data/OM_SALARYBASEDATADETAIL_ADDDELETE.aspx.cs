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
    public partial class OM_SALARYBASEDATADETAIL_ADDDELETE : System.Web.UI.Page
    {
        string flag = "";
        string stid = "";
        string spbh = "";
        string datatype = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                flag = Request.QueryString["FLAG"].ToString().Trim();
                if (flag == "delete")
                {
                    bindmxdata();
                    for (int i = 0; i < rptProNumCost.Items.Count; i++)
                    {
                        ((TextBox)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATANEW")).Attributes["onfocus"] = "this.blur()";
                        ((TextBox)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATANEW")).Attributes["onfocus"] = "this.blur()";
                        ((TextBox)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATANEW")).Attributes["onfocus"] = "this.blur()";
                        ((TextBox)rptProNumCost.Items[i].FindControl("YKGW_BASEDATANEW")).Attributes["onfocus"] = "this.blur()";
                    }
                }
                if (flag == "audit")
                {
                    bindspmxdata();
                    bindspdata();
                    btnadd.Visible = false;
                    for (int i = 0; i < rptProNumCost.Items.Count; i++)
                    {
                        ((TextBox)rptProNumCost.Items[i].FindControl("txt_name")).Enabled = false;
                    }
                }
                if (flag == "add")
                {
                    lbmanutype.Text = "增加";
                    CreateNewRow(8);
                }
                controlvisiblebind();//控件可见性和可用性控制
            }

        }

        //删除数据绑定
        private void bindmxdata()
        {
            stid = Request.QueryString["stid"].ToString().Trim();
            string sql = "select ST_NAME,ST_ID,DEP_NAME,PERSON_GH,BINGJIA_BASEDATANEW,BINGJIA_BASEDATAOLD,JIABAN_BASEDATANEW,JIABAN_BASEDATAOLD,NIANJIA_BASEDATANEW,NIANJIA_BASEDATAOLD,YKGW_BASEDATANEW,YKGW_BASEDATAOLD,'' as NOTE from View_OM_SALARYBASEDATA where ST_ID in(" + stid.ToString().Trim() + ")";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                rptProNumCost.DataSource = dt;
                rptProNumCost.DataBind();
            }

            lbmanutype.Text = "删除";
        }

        //审批明细数据绑定
        private void bindspmxdata()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sql = "select * from OM_SALARYBASEDATASP as a left join OM_SALARYBASEDATASPADDDELETEDETAIL as b on a.TOL_BH=b.spbh left join TBDS_STAFFINFO as c on b.ST_ID=c.ST_ID left join TBDS_DEPINFO as d on c.ST_DEPID=d.DEP_CODE where spbh='" + spbh.ToString().Trim() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                rptProNumCost.DataSource = dt;
                rptProNumCost.DataBind();
            }
            lbmanutype.Text = dt.Rows[0]["TOL_CZTTYPE"].ToString().Trim();
        }


        //审批数据绑定
        private void bindspdata()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sql = "select * from OM_SALARYBASEDATASP where TOL_BH='" + spbh.ToString().Trim() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["TOL_SHJS"].ToString().Trim() != "")
                {
                    rblSHJS.SelectedValue = dt.Rows[0]["TOL_SHJS"].ToString().Trim();
                }
                lbzdr.Text = dt.Rows[0]["CZRST_NAME"].ToString().Trim();
                lbzdtime.Text = dt.Rows[0]["CZTIME"].ToString().Trim();
                txt_first.Text = dt.Rows[0]["TOL_SHRNAME1"].ToString().Trim();
                firstid.Value = dt.Rows[0]["TOL_SHRID1"].ToString().Trim();
                if (dt.Rows[0]["TOL_SHRZT1"].ToString().Trim() != "0" && dt.Rows[0]["TOL_SHRZT1"].ToString().Trim() != "")
                {
                    rblfirst.SelectedValue = dt.Rows[0]["TOL_SHRZT1"].ToString().Trim();
                }
                first_time.Text = dt.Rows[0]["TOL_SHTIME1"].ToString().Trim();
                opinion1.Text = dt.Rows[0]["TOL_SHRYJ1"].ToString().Trim();

                txt_second.Text = dt.Rows[0]["TOL_SHRNAME2"].ToString().Trim();
                secondid.Value = dt.Rows[0]["TOL_SHRID2"].ToString().Trim();
                if (dt.Rows[0]["TOL_SHRZT2"].ToString().Trim() != "0" && dt.Rows[0]["TOL_SHRZT2"].ToString().Trim() != "")
                {
                    rblsecond.SelectedValue = dt.Rows[0]["TOL_SHRZT2"].ToString().Trim();
                }
                second_time.Text = dt.Rows[0]["TOL_SHTIME2"].ToString().Trim();
                opinion2.Text = dt.Rows[0]["TOL_SHRYJ2"].ToString().Trim();

                txt_third.Text = dt.Rows[0]["TOL_SHRNAME3"].ToString().Trim();
                thirdid.Value = dt.Rows[0]["TOL_SHRID3"].ToString().Trim();
                if (dt.Rows[0]["TOL_SHRZT3"].ToString().Trim() != "0" && dt.Rows[0]["TOL_SHRZT3"].ToString().Trim() != "")
                {
                    rblthird.SelectedValue = dt.Rows[0]["TOL_SHRZT3"].ToString().Trim();
                }
                third_time.Text = dt.Rows[0]["TOL_SHTIME3"].ToString().Trim();
                opinion3.Text = dt.Rows[0]["TOL_SHRYJ3"].ToString().Trim();
            }
        }

        //控件可用性和可见性
        private void controlvisiblebind()
        {
            flag = Request.QueryString["FLAG"].ToString().Trim();
            if (flag == "add"||flag=="delete")
            {
                btnSave.Visible = true;
                rblSHJS.Enabled = true;
                if (rblSHJS.SelectedValue.ToString().Trim() == "1")
                {
                    txt_first.Enabled = true;
                    hlSelect1.Visible = true;
                    yjshh.Visible = true;

                    txt_second.Enabled = false;
                    hlSelect2.Visible = false;
                    ejshh.Visible = false;

                    txt_third.Enabled = false;
                    hlSelect3.Visible = false;
                    sjshh.Visible = false;
                }
                if (rblSHJS.SelectedValue.ToString().Trim() == "2")
                {
                    txt_first.Enabled = true;
                    hlSelect1.Visible = true;
                    yjshh.Visible = true;

                    txt_second.Enabled = true;
                    hlSelect2.Visible = true;
                    ejshh.Visible = true;

                    txt_third.Enabled = false;
                    hlSelect3.Visible = false;
                    sjshh.Visible = false;
                }
                if (rblSHJS.SelectedValue.ToString().Trim() == "3")
                {
                    txt_first.Enabled = true;
                    hlSelect1.Visible = true;
                    yjshh.Visible = true;

                    txt_second.Enabled = true;
                    hlSelect2.Visible = true;
                    ejshh.Visible = true;

                    txt_third.Enabled = true;
                    hlSelect3.Visible = true;
                    sjshh.Visible = true;
                }
            }
            else if (flag == "audit")
            {
                spbh = Request.QueryString["spid"].ToString().Trim();
                string sql = "select * from OM_SALARYBASEDATASP where TOL_BH='" + spbh.ToString().Trim() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    //审核级数为1
                    if (rblSHJS.SelectedValue.ToString().Trim() == "1")
                    {
                        yjshh.Visible = true;
                        if (dt.Rows[0]["TOL_TOLSTATE"].ToString().Trim() == "0" && dt.Rows[0]["CZRST_ID"].ToString().Trim() == Session["UserID"].ToString().Trim())
                        {
                            btnSave.Visible = true;
                            btnaudit.Visible = true;
                            rblSHJS.Enabled = true;
                            txt_first.Enabled = true;
                            hlSelect1.Visible = true;
                        }
                        else if (dt.Rows[0]["TOL_TOLSTATE"].ToString().Trim() == "1" && dt.Rows[0]["TOL_SHRID1"].ToString().Trim() == Session["UserID"].ToString().Trim() && dt.Rows[0]["TOL_SHRZT1"].ToString().Trim() == "0")
                        {
                            btnaudit.Visible = true;
                            rblfirst.Enabled = true;
                            opinion1.Enabled = true;
                        }
                        else if (dt.Rows[0]["TOL_TOLSTATE"].ToString().Trim() == "2")
                        {
                            ImageVerify.Visible = true;
                        }
                        else if (dt.Rows[0]["TOL_TOLSTATE"].ToString().Trim() == "3" && dt.Rows[0]["CZRST_ID"].ToString().Trim() == Session["UserID"].ToString().Trim())
                        {
                            btnSave.Visible = true;
                        }
                    }


                    //审核级数为2
                    else if (rblSHJS.SelectedValue.ToString().Trim() == "2")
                    {
                        yjshh.Visible = true;
                        ejshh.Visible = true;
                        if (dt.Rows[0]["TOL_TOLSTATE"].ToString().Trim() == "0" && dt.Rows[0]["CZRST_ID"].ToString().Trim() == Session["UserID"].ToString().Trim())
                        {
                            btnSave.Visible = true;
                            btnaudit.Visible = true;
                            rblSHJS.Enabled = true;
                            txt_first.Enabled = true;
                            hlSelect1.Visible = true;
                            txt_second.Enabled = true;
                            hlSelect2.Visible = true;
                        }
                        else if (dt.Rows[0]["TOL_TOLSTATE"].ToString().Trim() == "1" && dt.Rows[0]["TOL_SHRID1"].ToString().Trim() == Session["UserID"].ToString().Trim() && dt.Rows[0]["TOL_SHRZT1"].ToString().Trim() == "0")
                        {
                            btnaudit.Visible = true;
                            rblfirst.Enabled = true;
                            opinion1.Enabled = true;
                        }
                        else if (dt.Rows[0]["TOL_TOLSTATE"].ToString().Trim() == "1" && dt.Rows[0]["TOL_SHRID2"].ToString().Trim() == Session["UserID"].ToString().Trim() && dt.Rows[0]["TOL_SHRZT1"].ToString().Trim() == "1" && dt.Rows[0]["TOL_SHRZT2"].ToString().Trim() == "0")
                        {
                            btnaudit.Visible = true;
                            rblsecond.Enabled = true;
                            opinion2.Enabled = true;
                        }
                        else if (dt.Rows[0]["TOL_TOLSTATE"].ToString().Trim() == "2")
                        {
                            ImageVerify.Visible = true;
                        }
                        else if (dt.Rows[0]["TOL_TOLSTATE"].ToString().Trim() == "3" && dt.Rows[0]["CZRST_ID"].ToString().Trim() == Session["UserID"].ToString().Trim())
                        {
                            btnSave.Visible = true;
                        }
                    }


                    else if (rblSHJS.SelectedValue.ToString().Trim() == "3")
                    {
                        yjshh.Visible = true;
                        ejshh.Visible = true;
                        sjshh.Visible = true;
                        if (dt.Rows[0]["TOL_TOLSTATE"].ToString().Trim() == "0" && dt.Rows[0]["CZRST_ID"].ToString().Trim() == Session["UserID"].ToString().Trim())
                        {
                            btnaudit.Visible = true;
                            btnSave.Visible = true;
                            rblSHJS.Enabled = true;
                            txt_first.Enabled = true;
                            hlSelect1.Visible = true;
                            txt_second.Enabled = true;
                            hlSelect2.Visible = true;
                            txt_third.Enabled = true;
                            hlSelect3.Visible = true;
                        }
                        else if (dt.Rows[0]["TOL_TOLSTATE"].ToString().Trim() == "1" && dt.Rows[0]["TOL_SHRID1"].ToString().Trim() == Session["UserID"].ToString().Trim() && dt.Rows[0]["TOL_SHRZT1"].ToString().Trim() == "0")
                        {
                            btnaudit.Visible = true;
                            rblfirst.Enabled = true;
                            opinion1.Enabled = true;
                        }
                        else if (dt.Rows[0]["TOL_TOLSTATE"].ToString().Trim() == "1" && dt.Rows[0]["TOL_SHRID2"].ToString().Trim() == Session["UserID"].ToString().Trim() && dt.Rows[0]["TOL_SHRZT1"].ToString().Trim() == "1" && dt.Rows[0]["TOL_SHRZT2"].ToString().Trim() == "0")
                        {
                            btnaudit.Visible = true;
                            rblsecond.Enabled = true;
                            opinion2.Enabled = true;
                        }
                        else if (dt.Rows[0]["TOL_TOLSTATE"].ToString().Trim() == "1" && dt.Rows[0]["TOL_SHRID3"].ToString().Trim() == Session["UserID"].ToString().Trim() && dt.Rows[0]["TOL_SHRZT2"].ToString().Trim() == "1" && dt.Rows[0]["TOL_SHRZT3"].ToString().Trim() == "0")
                        {
                            btnaudit.Visible = true;
                            rblthird.Enabled = true;
                            opinion3.Enabled = true;
                        }
                        else if (dt.Rows[0]["TOL_TOLSTATE"].ToString().Trim() == "2")
                        {
                            ImageVerify.Visible = true;
                        }
                        else if (dt.Rows[0]["TOL_TOLSTATE"].ToString().Trim() == "3" && dt.Rows[0]["CZRST_ID"].ToString().Trim() == Session["UserID"].ToString().Trim())
                        {
                            btnSave.Visible = true;
                        }
                    }
                }
            }

        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            string tolbh = "";
            tolbh = DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + Session["UserID"].ToString().Trim();
            List<string> list_sql = new List<string>();
            list_sql.Clear();
            flag = Request.QueryString["FLAG"].ToString().Trim();
            int num = 0;
            for (int i = 0; i < rptProNumCost.Items.Count; i++)
            {
                if (((TextBox)rptProNumCost.Items[i].FindControl("txt_name")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() != "")
                {
                    num++;
                }
            }
            if (num == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请填写要添加或删除的数据！');", true);
                return;
            }

            if (flag == "add")
            {
                for (int i = 0; i < rptProNumCost.Items.Count; i++)
                {
                    if (((TextBox)rptProNumCost.Items[i].FindControl("txt_name")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() != "")
                    {
                        string sqltext0 = "select * from OM_SALARYBASEDATA where ST_ID='" + ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() + "'";
                        DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqltext0);
                        if (dt0.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已添加过" + ((TextBox)rptProNumCost.Items[i].FindControl("txt_name")).Text.Trim() + "！！！');", true);
                            return;
                        }
                    }
                }
                if (rblSHJS.SelectedValue == "1")
                {
                    if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！！！');", true);
                        return;
                    }
                    else
                    {
                        list_sql.Add("insert into OM_SALARYBASEDATASP(TOL_BH,CZRST_ID,CZRST_NAME,CZTIME,TOL_SHJS,TOL_SHRNAME1,TOL_SHRID1,TOL_TYPE,TOL_CZTTYPE) values('" + tolbh.Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + rblSHJS.SelectedValue.ToString().Trim() + "','" + txt_first.Text.Trim() + "','" + firstid.Value.Trim() + "','" + datatype + "','" + lbmanutype.Text.Trim() + "')");
                        for (int i = 0; i < rptProNumCost.Items.Count; i++)
                        {
                            if (((TextBox)rptProNumCost.Items[i].FindControl("txt_name")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() != "")
                            {
                                list_sql.Add("insert into OM_SALARYBASEDATASPADDDELETEDETAIL(spbh,PERSON_GH,ST_ID,BINGJIA_BASEDATAOLD,BINGJIA_BASEDATANEW,JIABAN_BASEDATAOLD,JIABAN_BASEDATANEW,NIANJIA_BASEDATAOLD,NIANJIA_BASEDATANEW,YKGW_BASEDATAOLD,YKGW_BASEDATANEW,NOTE) values('" + tolbh.Trim() + "','" + ((TextBox)rptProNumCost.Items[i].FindControl("txt_gh")).Text.Trim() + "','" + ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("YKGW_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("YKGW_BASEDATANEW")).Text.Trim()) + ",'" + ((TextBox)rptProNumCost.Items[i].FindControl("NOTE")).Text.Trim() + "')");

                            }
                        }
                    }
                }
                else if (rblSHJS.SelectedValue == "2")
                {
                    if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "" || txt_second.Text.Trim() == "" || secondid.Value.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！！！');", true);
                        return;
                    }
                    else
                    {
                        list_sql.Add("insert into OM_SALARYBASEDATASP(TOL_BH,CZRST_ID,CZRST_NAME,CZTIME,TOL_SHJS,TOL_SHRNAME1,TOL_SHRID1,TOL_SHRNAME2,TOL_SHRID2,TOL_TYPE,TOL_CZTTYPE) values('" + tolbh.Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + rblSHJS.SelectedValue.ToString().Trim() + "','" + txt_first.Text.Trim() + "','" + firstid.Value.Trim() + "','" + txt_second.Text.Trim() + "','" + secondid.Value.Trim() + "','" + datatype + "','" + lbmanutype.Text.Trim() + "')");
                        for (int i = 0; i < rptProNumCost.Items.Count; i++)
                        {
                            if (((TextBox)rptProNumCost.Items[i].FindControl("txt_name")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() != "")
                            {
                                list_sql.Add("insert into OM_SALARYBASEDATASPADDDELETEDETAIL(spbh,PERSON_GH,ST_ID,BINGJIA_BASEDATAOLD,BINGJIA_BASEDATANEW,JIABAN_BASEDATAOLD,JIABAN_BASEDATANEW,NIANJIA_BASEDATAOLD,NIANJIA_BASEDATANEW,YKGW_BASEDATAOLD,YKGW_BASEDATANEW,NOTE) values('" + tolbh.Trim() + "','" + ((TextBox)rptProNumCost.Items[i].FindControl("txt_gh")).Text.Trim() + "','" + ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("YKGW_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("YKGW_BASEDATANEW")).Text.Trim()) + ",'" + ((TextBox)rptProNumCost.Items[i].FindControl("NOTE")).Text.Trim() + "')");

                            }
                        }
                    }
                }
                else if (rblSHJS.SelectedValue == "3")
                {
                    if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "" || txt_second.Text.Trim() == "" || secondid.Value.Trim() == "" || txt_third.Text.Trim() == "" || thirdid.Value.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！！！');", true);
                        return;
                    }
                    else
                    {
                        list_sql.Add("insert into OM_SALARYBASEDATASP(TOL_BH,CZRST_ID,CZRST_NAME,CZTIME,TOL_SHJS,TOL_SHRNAME1,TOL_SHRID1,TOL_SHRNAME2,TOL_SHRID2,TOL_SHRNAME3,TOL_SHRID3,TOL_TYPE,TOL_CZTTYPE) values('" + tolbh.Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + rblSHJS.SelectedValue.ToString().Trim() + "','" + txt_first.Text.Trim() + "','" + firstid.Value.Trim() + "','" + txt_second.Text.Trim() + "','" + secondid.Value.Trim() + "','" + txt_third.Text.Trim() + "','" + thirdid.Value.Trim() + "','" + datatype + "','" + lbmanutype.Text.Trim() + "')");
                        for (int i = 0; i < rptProNumCost.Items.Count; i++)
                        {
                            if (((TextBox)rptProNumCost.Items[i].FindControl("txt_name")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() != "")
                            {
                                list_sql.Add("insert into OM_SALARYBASEDATASPADDDELETEDETAIL(spbh,PERSON_GH,ST_ID,BINGJIA_BASEDATAOLD,BINGJIA_BASEDATANEW,JIABAN_BASEDATAOLD,JIABAN_BASEDATANEW,NIANJIA_BASEDATAOLD,NIANJIA_BASEDATANEW,YKGW_BASEDATAOLD,YKGW_BASEDATANEW,NOTE) values('" + tolbh.Trim() + "','" + ((TextBox)rptProNumCost.Items[i].FindControl("txt_gh")).Text.Trim() + "','" + ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("YKGW_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("YKGW_BASEDATANEW")).Text.Trim()) + ",'" + ((TextBox)rptProNumCost.Items[i].FindControl("NOTE")).Text.Trim() + "')");

                            }
                        }
                    }
                }
            }


            else if (flag == "delete")
            {
                for (int i = 0; i < rptProNumCost.Items.Count; i++)
                {
                    if (((TextBox)rptProNumCost.Items[i].FindControl("txt_name")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() != "")
                    {
                        string sqltext0 = "select * from OM_SALARYBASEDATA where ST_ID='" + ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() + "'";
                        DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqltext0);
                        if (dt0.Rows.Count == 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('薪酬基数表中不包含" + ((TextBox)rptProNumCost.Items[i].FindControl("txt_name")).Text.Trim() + "！！！');", true);
                            return;
                        }
                    }
                }
                if (rblSHJS.SelectedValue == "1")
                {
                    if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！！！');", true);
                        return;
                    }
                    else
                    {
                        list_sql.Add("insert into OM_SALARYBASEDATASP(TOL_BH,CZRST_ID,CZRST_NAME,CZTIME,TOL_SHJS,TOL_SHRNAME1,TOL_SHRID1,TOL_TYPE,TOL_CZTTYPE) values('" + tolbh.Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + rblSHJS.SelectedValue.ToString().Trim() + "','" + txt_first.Text.Trim() + "','" + firstid.Value.Trim() + "','" + datatype + "','" + lbmanutype.Text.Trim() + "')");
                        for (int i = 0; i < rptProNumCost.Items.Count; i++)
                        {
                            if (((TextBox)rptProNumCost.Items[i].FindControl("txt_name")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() != "")
                            {
                                list_sql.Add("insert into OM_SALARYBASEDATASPADDDELETEDETAIL(spbh,PERSON_GH,ST_ID,BINGJIA_BASEDATAOLD,BINGJIA_BASEDATANEW,JIABAN_BASEDATAOLD,JIABAN_BASEDATANEW,NIANJIA_BASEDATAOLD,NIANJIA_BASEDATANEW,YKGW_BASEDATAOLD,YKGW_BASEDATANEW,NOTE) values('" + tolbh.Trim() + "','" + ((TextBox)rptProNumCost.Items[i].FindControl("txt_gh")).Text.Trim() + "','" + ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("YKGW_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("YKGW_BASEDATANEW")).Text.Trim()) + ",'" + ((TextBox)rptProNumCost.Items[i].FindControl("NOTE")).Text.Trim() + "')");

                            }
                        }
                    }
                }
                else if (rblSHJS.SelectedValue == "2")
                {
                    if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "" || txt_second.Text.Trim() == "" || secondid.Value.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！！！');", true);
                        return;
                    }
                    else
                    {
                        list_sql.Add("insert into OM_SALARYBASEDATASP(TOL_BH,CZRST_ID,CZRST_NAME,CZTIME,TOL_SHJS,TOL_SHRNAME1,TOL_SHRID1,TOL_SHRNAME2,TOL_SHRID2,TOL_TYPE,TOL_CZTTYPE) values('" + tolbh.Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + rblSHJS.SelectedValue.ToString().Trim() + "','" + txt_first.Text.Trim() + "','" + firstid.Value.Trim() + "','" + txt_second.Text.Trim() + "','" + secondid.Value.Trim() + "','" + datatype + "','" + lbmanutype.Text.Trim() + "')");
                        for (int i = 0; i < rptProNumCost.Items.Count; i++)
                        {
                            if (((TextBox)rptProNumCost.Items[i].FindControl("txt_name")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() != "")
                            {
                                list_sql.Add("insert into OM_SALARYBASEDATASPADDDELETEDETAIL(spbh,PERSON_GH,ST_ID,BINGJIA_BASEDATAOLD,BINGJIA_BASEDATANEW,JIABAN_BASEDATAOLD,JIABAN_BASEDATANEW,NIANJIA_BASEDATAOLD,NIANJIA_BASEDATANEW,YKGW_BASEDATAOLD,YKGW_BASEDATANEW,NOTE) values('" + tolbh.Trim() + "','" + ((TextBox)rptProNumCost.Items[i].FindControl("txt_gh")).Text.Trim() + "','" + ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("YKGW_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("YKGW_BASEDATANEW")).Text.Trim()) + ",'" + ((TextBox)rptProNumCost.Items[i].FindControl("NOTE")).Text.Trim() + "')");

                            }
                        }
                    }
                }
                else if (rblSHJS.SelectedValue == "3")
                {
                    if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "" || txt_second.Text.Trim() == "" || secondid.Value.Trim() == "" || txt_third.Text.Trim() == "" || thirdid.Value.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！！！');", true);
                        return;
                    }
                    else
                    {
                        list_sql.Add("insert into OM_SALARYBASEDATASP(TOL_BH,CZRST_ID,CZRST_NAME,CZTIME,TOL_SHJS,TOL_SHRNAME1,TOL_SHRID1,TOL_SHRNAME2,TOL_SHRID2,TOL_SHRNAME3,TOL_SHRID3,TOL_TYPE,TOL_CZTTYPE) values('" + tolbh.Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + rblSHJS.SelectedValue.ToString().Trim() + "','" + txt_first.Text.Trim() + "','" + firstid.Value.Trim() + "','" + txt_second.Text.Trim() + "','" + secondid.Value.Trim() + "','" + txt_third.Text.Trim() + "','" + thirdid.Value.Trim() + "','" + datatype + "','" + lbmanutype.Text.Trim() + "')");
                        for (int i = 0; i < rptProNumCost.Items.Count; i++)
                        {
                            if (((TextBox)rptProNumCost.Items[i].FindControl("txt_name")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() != "")
                            {
                                list_sql.Add("insert into OM_SALARYBASEDATASPADDDELETEDETAIL(spbh,PERSON_GH,ST_ID,BINGJIA_BASEDATAOLD,BINGJIA_BASEDATANEW,JIABAN_BASEDATAOLD,JIABAN_BASEDATANEW,NIANJIA_BASEDATAOLD,NIANJIA_BASEDATANEW,YKGW_BASEDATAOLD,YKGW_BASEDATANEW,NOTE) values('" + tolbh.Trim() + "','" + ((TextBox)rptProNumCost.Items[i].FindControl("txt_gh")).Text.Trim() + "','" + ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATANEW")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("YKGW_BASEDATAOLD")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("YKGW_BASEDATANEW")).Text.Trim()) + ",'" + ((TextBox)rptProNumCost.Items[i].FindControl("NOTE")).Text.Trim() + "')");

                            }
                        }
                    }
                }
            }
            else if (flag == "audit")
            {
                spbh = Request.QueryString["spid"].ToString().Trim();
                tolbh = spbh;
                if (rblSHJS.SelectedValue == "1")
                {
                    if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！！！');", true);
                        return;
                    }
                    else
                    {
                        list_sql.Add("update OM_SALARYBASEDATASP set CZRST_ID='" + Session["UserID"].ToString().Trim() + "',CZRST_NAME='" + Session["UserName"].ToString().Trim() + "',CZTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',TOL_SHJS='" + rblSHJS.SelectedValue.ToString().Trim() + "',TOL_SHRNAME1='" + txt_first.Text.Trim() + "',TOL_SHRID1='" + firstid.Value.Trim() + "',TOL_SHRNAME2='',TOL_SHRID2='',TOL_SHRNAME3='',TOL_SHRID3='',TOL_TOLSTATE='0',TOL_SHRZT1='0',TOL_SHRYJ1='',TOL_SHTIME1='',TOL_SHRZT2='0',TOL_SHRYJ2='',TOL_SHTIME2='',TOL_SHRZT3='0',TOL_SHRYJ3='',TOL_SHTIME3='' where TOL_BH='" + spbh + "'");

                        for (int i = 0; i < rptProNumCost.Items.Count; i++)
                        {
                            if (((TextBox)rptProNumCost.Items[i].FindControl("txt_name")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() != "")
                            {
                                list_sql.Add("update OM_SALARYBASEDATASPADDDELETEDETAIL set PERSON_GH='" + ((TextBox)rptProNumCost.Items[i].FindControl("txt_gh")).Text.Trim() + "',BINGJIA_BASEDATAOLD=" + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATAOLD")).Text.Trim()) + ",BINGJIA_BASEDATANEW=" + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATANEW")).Text.Trim()) + ",JIABAN_BASEDATAOLD=" + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATAOLD")).Text.Trim()) + ",JIABAN_BASEDATANEW=" + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATANEW")).Text.Trim()) + ",NIANJIA_BASEDATAOLD=" + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATAOLD")).Text.Trim()) + ",NIANJIA_BASEDATANEW=" + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATANEW")).Text.Trim()) + ",YKGW_BASEDATAOLD=" + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("YKGW_BASEDATAOLD")).Text.Trim()) + ",YKGW_BASEDATANEW=" + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("YKGW_BASEDATANEW")).Text.Trim()) + ",NOTE='" + ((TextBox)rptProNumCost.Items[i].FindControl("NOTE")).Text.Trim() + "' where spbh='" + spbh + "' and ST_ID='" + ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() + "'");

                            }
                        }
                    }
                }
                else if (rblSHJS.SelectedValue == "2")
                {
                    if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "" || txt_second.Text.Trim() == "" || secondid.Value.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！！！');", true);
                        return;
                    }
                    else
                    {
                        list_sql.Add("update OM_SALARYBASEDATASP set CZRST_ID='" + Session["UserID"].ToString().Trim() + "',CZRST_NAME='" + Session["UserName"].ToString().Trim() + "',CZTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',TOL_SHJS='" + rblSHJS.SelectedValue.ToString().Trim() + "',TOL_SHRNAME1='" + txt_first.Text.Trim() + "',TOL_SHRID1='" + firstid.Value.Trim() + "',TOL_SHRNAME2='" + txt_second.Text.Trim() + "',TOL_SHRID2='" + secondid.Value.Trim() + "',TOL_SHRNAME3='',TOL_SHRID3='',TOL_TOLSTATE='0',TOL_SHRZT1='0',TOL_SHRYJ1='',TOL_SHTIME1='',TOL_SHRZT2='0',TOL_SHRYJ2='',TOL_SHTIME2='',TOL_SHRZT3='0',TOL_SHRYJ3='',TOL_SHTIME3='' where TOL_BH='" + spbh + "'");
                        for (int i = 0; i < rptProNumCost.Items.Count; i++)
                        {
                            if (((TextBox)rptProNumCost.Items[i].FindControl("txt_name")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() != "")
                            {
                                list_sql.Add("update OM_SALARYBASEDATASPADDDELETEDETAIL set PERSON_GH='" + ((TextBox)rptProNumCost.Items[i].FindControl("txt_gh")).Text.Trim() + "',BINGJIA_BASEDATAOLD=" + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATAOLD")).Text.Trim()) + ",BINGJIA_BASEDATANEW=" + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATANEW")).Text.Trim()) + ",JIABAN_BASEDATAOLD=" + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATAOLD")).Text.Trim()) + ",JIABAN_BASEDATANEW=" + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATANEW")).Text.Trim()) + ",NIANJIA_BASEDATAOLD=" + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATAOLD")).Text.Trim()) + ",NIANJIA_BASEDATANEW=" + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATANEW")).Text.Trim()) + ",YKGW_BASEDATAOLD=" + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("YKGW_BASEDATAOLD")).Text.Trim()) + ",YKGW_BASEDATANEW=" + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("YKGW_BASEDATANEW")).Text.Trim()) + ",NOTE='" + ((TextBox)rptProNumCost.Items[i].FindControl("NOTE")).Text.Trim() + "' where spbh='" + spbh + "' and ST_ID='" + ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() + "'");

                            }
                        }
                    }
                }
                else if (rblSHJS.SelectedValue == "3")
                {
                    if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "" || txt_second.Text.Trim() == "" || secondid.Value.Trim() == "" || txt_third.Text.Trim() == "" || thirdid.Value.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！！！');", true);
                        return;
                    }
                    else
                    {
                        list_sql.Add("update OM_SALARYBASEDATASP set CZRST_ID='" + Session["UserID"].ToString().Trim() + "',CZRST_NAME='" + Session["UserName"].ToString().Trim() + "',CZTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',TOL_SHJS='" + rblSHJS.SelectedValue.ToString().Trim() + "',TOL_SHRNAME1='" + txt_first.Text.Trim() + "',TOL_SHRID1='" + firstid.Value.Trim() + "',TOL_SHRNAME2='" + txt_second.Text.Trim() + "',TOL_SHRID2='" + secondid.Value.Trim() + "',TOL_SHRNAME3='" + txt_third.Text.Trim() + "',TOL_SHRID3='" + thirdid.Value.Trim() + "',TOL_TOLSTATE='0',TOL_SHRZT1='0',TOL_SHRYJ1='',TOL_SHTIME1='',TOL_SHRZT2='0',TOL_SHRYJ2='',TOL_SHTIME2='',TOL_SHRZT3='0',TOL_SHRYJ3='',TOL_SHTIME3='' where TOL_BH='" + spbh + "'");
                        for (int i = 0; i < rptProNumCost.Items.Count; i++)
                        {
                            if (((TextBox)rptProNumCost.Items[i].FindControl("txt_name")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() != "")
                            {

                                list_sql.Add("update OM_SALARYBASEDATASPADDDELETEDETAIL set PERSON_GH='" + ((TextBox)rptProNumCost.Items[i].FindControl("txt_gh")).Text.Trim() + "',BINGJIA_BASEDATAOLD=" + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATAOLD")).Text.Trim()) + ",BINGJIA_BASEDATANEW=" + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("BINGJIA_BASEDATANEW")).Text.Trim()) + ",JIABAN_BASEDATAOLD=" + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATAOLD")).Text.Trim()) + ",JIABAN_BASEDATANEW=" + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("JIABAN_BASEDATANEW")).Text.Trim()) + ",NIANJIA_BASEDATAOLD=" + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATAOLD")).Text.Trim()) + ",NIANJIA_BASEDATANEW=" + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("NIANJIA_BASEDATANEW")).Text.Trim()) + ",YKGW_BASEDATAOLD=" + CommonFun.ComTryDecimal(((Label)rptProNumCost.Items[i].FindControl("YKGW_BASEDATAOLD")).Text.Trim()) + ",YKGW_BASEDATANEW=" + CommonFun.ComTryDecimal(((TextBox)rptProNumCost.Items[i].FindControl("YKGW_BASEDATANEW")).Text.Trim()) + ",NOTE='" + ((TextBox)rptProNumCost.Items[i].FindControl("NOTE")).Text.Trim() + "' where spbh='" + spbh + "' and ST_ID='" + ((TextBox)rptProNumCost.Items[i].FindControl("txtstid")).Text.Trim() + "'");
                            }
                        }
                    }
                }
            }


            DBCallCommon.ExecuteTrans(list_sql);
            Response.Redirect("OM_SALARYBASEDATADETAIL_ADDDELETE.aspx?FLAG=audit&spid=" + tolbh);
        }


        //提交
        protected void btnaudit_OnClick(object sender, EventArgs e)
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqlupdatestate = "";
            List<string> list_sql = new List<string>();
            list_sql.Clear();

            string sqltext = "select * from OM_SALARYBASEDATASP where TOL_BH='" + spbh.ToString().Trim() + "'";
            DataTable dttext = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dttext.Rows.Count > 0)
            {
                if (dttext.Rows[0]["TOL_TOLSTATE"].ToString().Trim() == "0")
                {
                    if (rblSHJS.SelectedValue.Trim() == "1")
                    {
                        if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！！！');", true);
                            return;
                        }
                        else
                        {
                            sqlupdatestate = "update OM_SALARYBASEDATASP set TOL_SHJS='" + rblSHJS.SelectedValue.ToString().Trim() + "',TOL_SHRNAME1='" + txt_first.Text.Trim() + "',TOL_SHRID1='" + firstid.Value.Trim() + "',TOL_SHRNAME2='',TOL_SHRID2='',TOL_SHRNAME3='',TOL_SHRID3='',TOL_TOLSTATE='1',TOL_SHRZT1='0',TOL_SHRYJ1='',TOL_SHTIME1='',TOL_SHRZT2='0',TOL_SHRYJ2='',TOL_SHTIME2='',TOL_SHRZT3='0',TOL_SHRYJ3='',TOL_SHTIME3='' where TOL_BH='" + spbh + "'";
                        }
                    }
                    else if (rblSHJS.SelectedValue.Trim() == "2")
                    {
                        if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "" || txt_second.Text.Trim() == "" || secondid.Value.Trim() == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！！！');", true);
                            return;
                        }
                        else
                        {
                            sqlupdatestate = "update OM_SALARYBASEDATASP set TOL_SHJS='" + rblSHJS.SelectedValue.ToString().Trim() + "',TOL_SHRNAME1='" + txt_first.Text.Trim() + "',TOL_SHRID1='" + firstid.Value.Trim() + "',TOL_SHRNAME2='" + txt_second.Text.Trim() + "',TOL_SHRID2='" + secondid.Value.Trim() + "',TOL_SHRNAME3='',TOL_SHRID3='',TOL_TOLSTATE='1',TOL_SHRZT1='0',TOL_SHRYJ1='',TOL_SHTIME1='',TOL_SHRZT2='0',TOL_SHRYJ2='',TOL_SHTIME2='',TOL_SHRZT3='0',TOL_SHRYJ3='',TOL_SHTIME3='' where TOL_BH='" + spbh + "'";
                        }
                    }
                    else if (rblSHJS.SelectedValue.Trim() == "3")
                    {
                        if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "" || txt_second.Text.Trim() == "" || secondid.Value.Trim() == "" || txt_third.Text.Trim() == "" || thirdid.Value.Trim() == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！！！');", true);
                            return;
                        }
                        else
                        {
                            sqlupdatestate = "update OM_SALARYBASEDATASP set TOL_SHJS='" + rblSHJS.SelectedValue.ToString().Trim() + "',TOL_SHRNAME1='" + txt_first.Text.Trim() + "',TOL_SHRID1='" + firstid.Value.Trim() + "',TOL_SHRNAME2='" + txt_second.Text.Trim() + "',TOL_SHRID2='" + secondid.Value.Trim() + "',TOL_SHRNAME3='" + txt_third.Text.Trim() + "',TOL_SHRID3='" + thirdid.Value.Trim() + "',TOL_TOLSTATE='1',TOL_SHRZT1='0',TOL_SHRYJ1='',TOL_SHTIME1='',TOL_SHRZT2='0',TOL_SHRYJ2='',TOL_SHTIME2='',TOL_SHRZT3='0',TOL_SHRYJ3='',TOL_SHTIME3='' where TOL_BH='" + spbh + "'";
                        }
                    }
                    list_sql.Add(sqlupdatestate);

                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sprid = firstid.Value.Trim();
                    sptitle = "薪酬系数" + lbmanutype + "审批";
                    spcontent = "编号" + spbh + "的薪酬系数" + lbmanutype + "需要您审批，请登录查看！";
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                }
                else if (dttext.Rows[0]["TOL_TOLSTATE"].ToString().Trim() == "1")
                {
                    //一级
                    if (dttext.Rows[0]["TOL_SHRZT1"].ToString().Trim() == "0")
                    {
                        if (rblfirst.SelectedValue != "1" && rblfirst.SelectedValue != "2")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核结论！');", true);
                            return;
                        }
                        if (rblSHJS.SelectedValue.Trim() == "1")
                        {
                            if (rblfirst.SelectedValue == "1")
                            {
                                sqlupdatestate = "update OM_SALARYBASEDATASP set TOL_TOLSTATE='2',TOL_SHRZT1='1',TOL_SHRYJ1='" + opinion1.Text.Trim() + "',TOL_SHTIME1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "' where TOL_BH='" + spbh + "'";
                                updatexcxs();
                            }
                            else if (rblfirst.SelectedValue == "2")
                            {
                                sqlupdatestate = "update OM_SALARYBASEDATASP set TOL_TOLSTATE='3',TOL_SHRZT1='2',TOL_SHRYJ1='" + opinion1.Text.Trim() + "',TOL_SHTIME1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "' where TOL_BH='" + spbh + "'";
                            }
                        }
                        else if (rblSHJS.SelectedValue.Trim() == "2" || rblSHJS.SelectedValue.Trim() == "3")
                        {
                            if (rblfirst.SelectedValue == "1")
                            {
                                sqlupdatestate = "update OM_SALARYBASEDATASP set TOL_SHRZT1='1',TOL_SHRYJ1='" + opinion1.Text.Trim() + "',TOL_SHTIME1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "' where TOL_BH='" + spbh + "'";
                                //邮件提醒
                                string sprid = "";
                                string sptitle = "";
                                string spcontent = "";
                                sprid = secondid.Value.Trim();
                                sptitle = "薪酬系数" + lbmanutype + "审批";
                                spcontent = "编号" + spbh + "的薪酬系数" + lbmanutype + "需要您审批，请登录查看！";
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                            }
                            else if (rblfirst.SelectedValue == "2")
                            {
                                sqlupdatestate = "update OM_SALARYBASEDATASP set TOL_TOLSTATE='3',TOL_SHRZT1='2',TOL_SHRYJ1='" + opinion1.Text.Trim() + "',TOL_SHTIME1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "' where TOL_BH='" + spbh + "'";
                            }
                        }

                    }
                    //二级
                    if (dttext.Rows[0]["TOL_SHRZT1"].ToString().Trim() == "1" && dttext.Rows[0]["TOL_SHRZT2"].ToString().Trim() == "0")
                    {
                        if (rblsecond.SelectedValue != "1" && rblsecond.SelectedValue != "2")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核结论！');", true);
                            return;
                        }
                        if (rblSHJS.SelectedValue.Trim() == "2")
                        {
                            if (rblsecond.SelectedValue == "1")
                            {
                                sqlupdatestate = "update OM_SALARYBASEDATASP set TOL_TOLSTATE='2',TOL_SHRZT2='1',TOL_SHRYJ2='" + opinion2.Text.Trim() + "',TOL_SHTIME2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "' where TOL_BH='" + spbh + "'";
                                updatexcxs();
                            }
                            else if (rblsecond.SelectedValue == "2")
                            {
                                sqlupdatestate = "update OM_SALARYBASEDATASP set TOL_TOLSTATE='3',TOL_SHRZT2='2',TOL_SHRYJ2='" + opinion2.Text.Trim() + "',TOL_SHTIME2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "' where TOL_BH='" + spbh + "'";
                            }
                        }
                        else if (rblSHJS.SelectedValue.Trim() == "3")
                        {
                            if (rblsecond.SelectedValue == "1")
                            {
                                sqlupdatestate = "update OM_SALARYBASEDATASP set TOL_SHRZT2='1',TOL_SHRYJ2='" + opinion2.Text.Trim() + "',TOL_SHTIME2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "' where TOL_BH='" + spbh + "'";

                                //邮件提醒
                                string sprid = "";
                                string sptitle = "";
                                string spcontent = "";
                                sprid = thirdid.Value.Trim();
                                sptitle = "薪酬系数" + lbmanutype + "审批";
                                spcontent = "编号" + spbh + "的薪酬系数" + lbmanutype + "需要您审批，请登录查看！";
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                            }
                            else if (rblsecond.SelectedValue == "2")
                            {
                                sqlupdatestate = "update OM_SALARYBASEDATASP set TOL_TOLSTATE='3',TOL_SHRZT2='2',TOL_SHRYJ2='" + opinion2.Text.Trim() + "',TOL_SHTIME2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "' where TOL_BH='" + spbh + "'";
                            }
                        }
                    }

                    //三级
                    if (dttext.Rows[0]["TOL_SHRZT1"].ToString().Trim() == "1" && dttext.Rows[0]["TOL_SHRZT2"].ToString().Trim() == "1" && dttext.Rows[0]["TOL_SHRZT3"].ToString().Trim() == "0")
                    {
                        if (rblthird.SelectedValue != "1" && rblthird.SelectedValue != "2")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核结论！');", true);
                            return;
                        }
                        if (rblthird.SelectedValue == "1")
                        {
                            sqlupdatestate = "update OM_SALARYBASEDATASP set TOL_TOLSTATE='2',TOL_SHRZT3='1',TOL_SHRYJ3='" + opinion3.Text.Trim() + "',TOL_SHTIME3='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "' where TOL_BH='" + spbh + "'";
                            updatexcxs();
                        }
                        else if (rblthird.SelectedValue == "2")
                        {
                            sqlupdatestate = "update OM_SALARYBASEDATASP set TOL_TOLSTATE='3',TOL_SHRZT3='2',TOL_SHRYJ3='" + opinion3.Text.Trim() + "',TOL_SHTIME3='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "' where TOL_BH='" + spbh + "'";
                        }
                    }
                    list_sql.Add(sqlupdatestate);
                }
            }
            DBCallCommon.ExecuteTrans(list_sql);
            Response.Redirect("OM_SALARYBASEDATADETAIL_ADDDELETE.aspx?FLAG=audit&spid=" + spbh);
        }


        private void updatexcxs()
        {
            List<string> list_sql = new List<string>();
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqlsp = "select * from OM_SALARYBASEDATASP as a left join OM_SALARYBASEDATASPADDDELETEDETAIL as b on a.TOL_BH=b.spbh where spbh='" + spbh + "'";
            DataTable dtsp = DBCallCommon.GetDTUsingSqlText(sqlsp);
            if (dtsp.Rows.Count > 0)
            {
                for (int i = 0; i < dtsp.Rows.Count; i++)
                {
                    if (lbmanutype.Text.Trim() == "删除")
                    {
                        list_sql.Add("delete from OM_SALARYBASEDATA where ST_ID='" + dtsp.Rows[i]["ST_ID"].ToString().Trim() + "'");
                    }
                    else if (lbmanutype.Text.Trim() == "增加")
                    {
                        list_sql.Add("insert into OM_SALARYBASEDATA(PERSON_GH,ST_ID,BINGJIA_BASEDATAOLD,BINGJIA_BASEDATANEW,BINGJIA_CZTIME,BINGJIA_CZRID,BINGJIA_CZRNAME,JIABAN_BASEDATAOLD,JIABAN_BASEDATANEW,JIABAN_CZTIME,JIABAN_CZRID,JIABAN_CZRNAME,NIANJIA_BASEDATAOLD, NIANJIA_BASEDATANEW,NIANJIA_CZTIME,NIANJIA_CZRID,NIANJIA_CZRNAME,YKGW_BASEDATAOLD,YKGW_BASEDATANEW,YKGW_CZTIME,YKGW_CZRID,YKGW_CZRNAME) Values('" + dtsp.Rows[i]["PERSON_GH"].ToString().Trim() + "','" + dtsp.Rows[i]["ST_ID"].ToString().Trim() + "'," + CommonFun.ComTryDecimal(dtsp.Rows[i]["BINGJIA_BASEDATAOLD"].ToString().Trim()) + "," + CommonFun.ComTryDecimal(dtsp.Rows[i]["BINGJIA_BASEDATANEW"].ToString().Trim()) + ",'" + dtsp.Rows[i]["CZTIME"].ToString().Trim() + "','" + dtsp.Rows[i]["CZRST_ID"].ToString().Trim() + "','" + dtsp.Rows[i]["CZRST_NAME"].ToString().Trim() + "'," + CommonFun.ComTryDecimal(dtsp.Rows[i]["JIABAN_BASEDATAOLD"].ToString().Trim()) + "," + CommonFun.ComTryDecimal(dtsp.Rows[i]["JIABAN_BASEDATANEW"].ToString().Trim()) + ",'" + dtsp.Rows[i]["CZTIME"].ToString().Trim() + "','" + dtsp.Rows[i]["CZRST_ID"].ToString().Trim() + "','" + dtsp.Rows[i]["CZRST_NAME"].ToString().Trim() + "'," + CommonFun.ComTryDecimal(dtsp.Rows[i]["NIANJIA_BASEDATAOLD"].ToString().Trim()) + "," + CommonFun.ComTryDecimal(dtsp.Rows[i]["NIANJIA_BASEDATANEW"].ToString().Trim()) + ",'" + dtsp.Rows[i]["CZTIME"].ToString().Trim() + "','" + dtsp.Rows[i]["CZRST_ID"].ToString().Trim() + "','" + dtsp.Rows[i]["CZRST_NAME"].ToString().Trim() + "'," + CommonFun.ComTryDecimal(dtsp.Rows[i]["YKGW_BASEDATAOLD"].ToString().Trim()) + "," + CommonFun.ComTryDecimal(dtsp.Rows[i]["YKGW_BASEDATANEW"].ToString().Trim()) + ",'" + dtsp.Rows[i]["CZTIME"].ToString().Trim() + "','" + dtsp.Rows[i]["CZRST_ID"].ToString().Trim() + "','" + dtsp.Rows[i]["CZRST_NAME"].ToString().Trim() + "')");
                    }
                }
                DBCallCommon.ExecuteTrans(list_sql);
            }
        }



        protected void Textname_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            TextBox Tb_newstid = (TextBox)sender;
            RepeaterItem Reitem = (RepeaterItem)Tb_newstid.Parent;
            if (num > 0)
            {
                string stid = (sender as TextBox).Text.Trim().Substring(0, num);

                string sqlText = "select * from View_TBDS_STAFFINFO where ST_ID='" + stid + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                if (dt.Rows.Count > 0)
                {
                    ((TextBox)Reitem.FindControl("txt_name")).Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txtstid")).Text = dt.Rows[0]["ST_ID"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txtbm")).Text = dt.Rows[0]["DEP_NAME"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txt_gh")).Text = dt.Rows[0]["ST_WORKNO"].ToString().Trim();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员不存在，请重新输入！');", true);
                }

                string sqlifexist = "select * from OM_SALARYBASEDATA where ST_ID='" + stid + "'";
                DataTable dtifexist = DBCallCommon.GetDTUsingSqlText(sqlifexist);
                if (dtifexist.Rows.Count > 0)
                {
                    ((TextBox)Reitem.FindControl("BINGJIA_BASEDATANEW")).Text = dtifexist.Rows[0]["BINGJIA_BASEDATANEW"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("BINGJIA_BASEDATAOLD")).Text = dtifexist.Rows[0]["BINGJIA_BASEDATAOLD"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("JIABAN_BASEDATANEW")).Text = dtifexist.Rows[0]["JIABAN_BASEDATANEW"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("JIABAN_BASEDATAOLD")).Text = dtifexist.Rows[0]["JIABAN_BASEDATAOLD"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("NIANJIA_BASEDATANEW")).Text = dtifexist.Rows[0]["NIANJIA_BASEDATANEW"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("NIANJIA_BASEDATAOLD")).Text = dtifexist.Rows[0]["NIANJIA_BASEDATAOLD"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("YKGW_BASEDATANEW")).Text = dtifexist.Rows[0]["YKGW_BASEDATANEW"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("YKGW_BASEDATAOLD")).Text = dtifexist.Rows[0]["YKGW_BASEDATAOLD"].ToString().Trim();
                }
            }

        }

        protected void rblSHJS_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            controlvisiblebind();
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (int.TryParse(txtNum.Text, out a))
            {
                CreateNewRow(a);
            }
            else
            {
                Response.Write("<script>alert('请输入数字！')</script>");
            }
        }


        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            List<string> col = new List<string>();
            this.rptProNumCost.DataSource = dt;
            this.rptProNumCost.DataBind();
            InitVar(col);
        }


        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ST_NAME");
            dt.Columns.Add("ST_ID");
            dt.Columns.Add("DEP_NAME");
            dt.Columns.Add("PERSON_GH");
            dt.Columns.Add("BINGJIA_BASEDATANEW");
            dt.Columns.Add("BINGJIA_BASEDATAOLD");
            dt.Columns.Add("JIABAN_BASEDATANEW");
            dt.Columns.Add("JIABAN_BASEDATAOLD");
            dt.Columns.Add("NIANJIA_BASEDATANEW");
            dt.Columns.Add("NIANJIA_BASEDATAOLD");
            dt.Columns.Add("YKGW_BASEDATANEW");
            dt.Columns.Add("YKGW_BASEDATAOLD");
            dt.Columns.Add("NOTE");
            foreach (RepeaterItem retItem in rptProNumCost.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((TextBox)retItem.FindControl("txt_name")).Text;
                newRow[1] = ((TextBox)retItem.FindControl("txtstid")).Text;
                newRow[2] = ((TextBox)retItem.FindControl("txtbm")).Text;
                newRow[3] = ((TextBox)retItem.FindControl("txt_gh")).Text;
                newRow[4] = ((TextBox)retItem.FindControl("BINGJIA_BASEDATANEW")).Text;
                newRow[5] = ((Label)retItem.FindControl("BINGJIA_BASEDATAOLD")).Text;
                newRow[6] = ((TextBox)retItem.FindControl("JIABAN_BASEDATANEW")).Text;
                newRow[7] = ((Label)retItem.FindControl("JIABAN_BASEDATAOLD")).Text;
                newRow[8] = ((TextBox)retItem.FindControl("NIANJIA_BASEDATANEW")).Text;
                newRow[9] = ((Label)retItem.FindControl("NIANJIA_BASEDATAOLD")).Text;
                newRow[10] = ((TextBox)retItem.FindControl("YKGW_BASEDATANEW")).Text;
                newRow[11] = ((Label)retItem.FindControl("YKGW_BASEDATAOLD")).Text;
                newRow[12] = ((TextBox)retItem.FindControl("NOTE")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        private void InitVar(List<string> col)
        {
            if (rptProNumCost.Items.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            else
            {
                NoDataPanel.Visible = false;
                delete.Visible = true;
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            List<string> col = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("ST_NAME");
            dt.Columns.Add("ST_ID");
            dt.Columns.Add("DEP_NAME");
            dt.Columns.Add("PERSON_GH");
            dt.Columns.Add("BINGJIA_BASEDATANEW");
            dt.Columns.Add("BINGJIA_BASEDATAOLD");
            dt.Columns.Add("JIABAN_BASEDATANEW");
            dt.Columns.Add("JIABAN_BASEDATAOLD");
            dt.Columns.Add("NIANJIA_BASEDATANEW");
            dt.Columns.Add("NIANJIA_BASEDATAOLD");
            dt.Columns.Add("YKGW_BASEDATANEW");
            dt.Columns.Add("YKGW_BASEDATAOLD");
            dt.Columns.Add("NOTE");
            foreach (RepeaterItem retItem in rptProNumCost.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((TextBox)retItem.FindControl("txt_name")).Text;
                    newRow[1] = ((TextBox)retItem.FindControl("txtstid")).Text;
                    newRow[2] = ((TextBox)retItem.FindControl("txtbm")).Text;
                    newRow[3] = ((TextBox)retItem.FindControl("txt_gh")).Text;
                    newRow[4] = ((TextBox)retItem.FindControl("BINGJIA_BASEDATANEW")).Text;
                    newRow[5] = ((Label)retItem.FindControl("BINGJIA_BASEDATAOLD")).Text;
                    newRow[6] = ((TextBox)retItem.FindControl("JIABAN_BASEDATANEW")).Text;
                    newRow[7] = ((Label)retItem.FindControl("JIABAN_BASEDATAOLD")).Text;
                    newRow[8] = ((TextBox)retItem.FindControl("NIANJIA_BASEDATANEW")).Text;
                    newRow[9] = ((Label)retItem.FindControl("NIANJIA_BASEDATAOLD")).Text;
                    newRow[10] = ((TextBox)retItem.FindControl("YKGW_BASEDATANEW")).Text;
                    newRow[11] = ((Label)retItem.FindControl("YKGW_BASEDATAOLD")).Text;
                    newRow[12] = ((TextBox)retItem.FindControl("NOTE")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.rptProNumCost.DataSource = dt;
            this.rptProNumCost.DataBind();
            InitVar(col);
        }
    }
}
