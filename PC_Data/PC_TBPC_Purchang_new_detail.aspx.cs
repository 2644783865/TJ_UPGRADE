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
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchang_new_detail : System.Web.UI.Page
    {
        string ph = "";
        string name = "";
        string userid = "";
        string position = "";
        string cgrposition = "";
        string cgrid = "";
        string cgrname = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            name = Session["UserName"].ToString();
            userid = Session["UserID"].ToString();
            position = Session["POSITION"].ToString();
            ph = Request.QueryString["pid"];
            if (!IsPostBack)
            {
                binddata();
                zxztbind();
                kjifvisible();
            }
        }


        //物料执行状态绑定
        private void zxztbind()
        {
            for (int i = 0; i < rptProNumCost.Items.Count; i++)
            {
                TextBox zxzt = (TextBox)rptProNumCost.Items[i].FindControl("tbzxzt");
                TextBox ptc = (TextBox)rptProNumCost.Items[i].FindControl("Aptcode");
                string sql = "select PUR_STATE from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptc.Text.ToString().Trim() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["PUR_STATE"].ToString()) < 4)
                    {
                        zxzt.Text = "未分工";
                    }
                    else if (dt.Rows[0]["PUR_STATE"].ToString() == "4")
                    {
                        zxzt.Text = "已分工";
                    }
                    else if (dt.Rows[0]["PUR_STATE"].ToString() == "5")
                    {
                        zxzt.Text = "已代用";
                    }
                    else if (dt.Rows[0]["PUR_STATE"].ToString() == "6")
                    {
                        zxzt.Text = "已生成询价单";
                    }
                    else if (dt.Rows[0]["PUR_STATE"].ToString() == "7")
                    {
                        zxzt.Text = "已生成订单";
                    }
                    else
                    {
                        zxzt.Text = "已被变更";
                    }
                }
            }
        }



        //定义控件的可见性及可用性
        private void kjifvisible()
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText("select * from TBPC_BG where BG_PH='" + ph + "'");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["RESULT"].ToString() == "已执行")
                {
                    ImageVerify.Visible = true;
                }
                if (dt.Rows[0]["BG_NAME"].ToString() == name && (dt.Rows[0]["BG_STATE"].ToString() == "0" || dt.Rows[0]["BG_STATE"].ToString() == "1"))
                {
                    btntjsp.Visible = true;
                    btnsc.Visible = true;
                    for (int r = 0; r < rptProNumCost.Items.Count; r++)
                    {
                        (rptProNumCost.Items[r].FindControl("BG_NUM") as System.Web.UI.WebControls.TextBox).Enabled = true;
                        (rptProNumCost.Items[r].FindControl("BG_FZNUM") as System.Web.UI.WebControls.TextBox).Enabled = true;
                        (rptProNumCost.Items[r].FindControl("BG_NOTE") as System.Web.UI.WebControls.TextBox).Enabled = true;
                    }
                }
                if (name == dt.Rows[0]["BG_SHRA"].ToString() && dt.Rows[0]["BG_STATE"].ToString() == "1")
                {
                    btnsh1.Visible = true;
                    rblfirst.Enabled = true;
                    first_opinion.Enabled = true;
                }
                else if ((position == "0301" || userid == "67") && dt.Rows[0]["BG_STATE"].ToString() == "2")
                {
                    btnsh2.Visible = true;
                    rblsecond.Enabled = true;
                    second_opinion.Enabled = true;
                }
                else if (position == "0501" && dt.Rows[0]["BG_STATE"].ToString() == "3")
                {
                    btnsh3.Visible = true;
                    rblthird.Enabled = true;
                    third_opinion.Enabled = true;
                }
                else if ((dt.Rows[0]["BG_SHRD"].ToString() == name) && dt.Rows[0]["BG_STATE"].ToString() == "4" && dt.Rows[0]["RESULT"].ToString() == "审核中")
                {
                    btnsh4.Visible = true;
                    rblforth.Enabled = true;
                    forth_opinion.Enabled = true;
                }
                else
                {
                    btnsh1.Visible = false;
                    btnsh2.Visible = false;
                    btnsh3.Visible = false;
                    btnsh4.Visible = false;
                    rblfirst.Enabled = false;
                    first_opinion.Enabled = false;
                    rblsecond.Enabled = false;
                    second_opinion.Enabled = false;
                    rblthird.Enabled = false;
                    third_opinion.Enabled = false;
                    rblforth.Enabled = false;
                    forth_opinion.Enabled = false;
                }
            }

        }

        //数据绑定
        private void binddata()
        {
            string sql = "select * from TBPC_BG where BG_PH='" + ph + "'";
            DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt0.Rows.Count > 0)
            {
                txt_first.Text = dt0.Rows[0]["BG_SHRA"].ToString();
                if (dt0.Rows[0]["BG_STATE"].ToString() == "2")
                {
                    rblfirst.SelectedIndex = 0;
                }
                else if (dt0.Rows[0]["BG_STATE"].ToString() == "6")
                {
                    rblfirst.SelectedIndex = 1;
                }
                first_time.Text = dt0.Rows[0]["BG_SHDATEA"].ToString();
                first_opinion.Text = dt0.Rows[0]["BG_SHYJA"].ToString();

                txt_second.Text = dt0.Rows[0]["BG_SHRB"].ToString();
                if (dt0.Rows[0]["BG_STATE"].ToString() == "3")
                {
                    rblfirst.SelectedIndex = 0;
                    rblsecond.SelectedIndex = 0;
                }
                else if (dt0.Rows[0]["BG_STATE"].ToString() == "7")
                {
                    rblfirst.SelectedIndex = 0;
                    rblsecond.SelectedIndex = 1;
                }
                second_time.Text = dt0.Rows[0]["BG_SHDATEB"].ToString();
                second_opinion.Text = dt0.Rows[0]["BG_SHYJB"].ToString();

                txt_third.Text = dt0.Rows[0]["BG_SHRC"].ToString();
                if (dt0.Rows[0]["BG_STATE"].ToString() == "4")
                {
                    rblfirst.SelectedIndex = 0;
                    rblsecond.SelectedIndex = 0;
                    rblthird.SelectedIndex = 0;
                }
                else if (dt0.Rows[0]["BG_STATE"].ToString() == "8")
                {
                    rblfirst.SelectedIndex = 0;
                    rblsecond.SelectedIndex = 0;
                    rblthird.SelectedIndex = 1;
                }
                third_time.Text = dt0.Rows[0]["BG_SHDATEC"].ToString();
                third_opinion.Text = dt0.Rows[0]["BG_SHYJC"].ToString();


                txt_forth.Text = dt0.Rows[0]["BG_SHRD"].ToString();
                if (dt0.Rows[0]["BG_STATE"].ToString() == "5")
                {
                    rblfirst.SelectedIndex = 0;
                    rblsecond.SelectedIndex = 0;
                    rblthird.SelectedIndex = 0;
                    rblforth.SelectedIndex = 0;
                }
                else if (dt0.Rows[0]["BG_STATE"].ToString() == "9")
                {
                    rblfirst.SelectedIndex = 0;
                    rblsecond.SelectedIndex = 0;
                    rblthird.SelectedIndex = 0;
                    rblforth.SelectedIndex = 1;
                }
                forth_time.Text = dt0.Rows[0]["BG_SHDATED"].ToString();
                forth_opinion.Text = dt0.Rows[0]["BG_SHYJD"].ToString();
            }
            string sqltextbd = "select Aptcode,PUR_MASHAPE,marid,marnm,margg,marcz,margb,num,marunit,fznum,marfzunit,sqrnm,sqrtime,BG_NUM,BG_FZNUM,RESULT,case when BG_STATE='0' then '初始化' when BG_STATE='1' then '已提交' when BG_STATE>='6' then '已驳回' when (BG_STATE='5'and RESULT='已执行') then '已通过' when (BG_STATE='4' and RESULT='已执行') then '已通过'  else '审核中' end as shzt,BG_NOTE from ((select * from View_TBPC_PLAN_PLACE)a left join (select * from TBPC_BG)b on a.Aptcode=b.BG_PTC) where BG_PH='" + ph + "'";
            DataTable dtbd = DBCallCommon.GetDTUsingSqlText(sqltextbd);
            rptProNumCost.DataSource = dtbd;
            rptProNumCost.DataBind();
        }



        //提交变更
        protected void btn_tj_Click(object sender, EventArgs e)
        {
            string bgdate = DateTime.Now.ToString("yyyy-MM-dd");
            List<string> list0 = new List<string>();
            for (int t = 0; t < rptProNumCost.Items.Count; t++)
            {
                TextBox bgnum = (TextBox)rptProNumCost.Items[t].FindControl("BG_NUM");
                TextBox bgfznum = (TextBox)rptProNumCost.Items[t].FindControl("BG_FZNUM");
                Label num = (Label)rptProNumCost.Items[t].FindControl("num");
                Label fznum = (Label)rptProNumCost.Items[t].FindControl("fznum");
                if (bgnum.Text.ToString().Trim() == "" || bgfznum.Text.ToString().Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('不能为空!')", true);
                    return;
                }
                if (Convert.ToDouble(bgnum.Text.ToString().Trim()) < 0 || Convert.ToDouble(bgfznum.Text.ToString().Trim()) < 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请不要填写负数!')", true);
                    return;
                }
                if (Convert.ToDouble(bgnum.Text.ToString().Trim()) > Convert.ToDouble(num.Text.ToString().Trim()) || Convert.ToDouble(bgfznum.Text.ToString().Trim()) > Convert.ToDouble(fznum.Text.ToString().Trim()))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('变更数量不能大于计划数量!')", true);
                    return;
                }
            }
            for (int r = 0; r < rptProNumCost.Items.Count; r++)
            {
                TextBox bgnum = (TextBox)rptProNumCost.Items[r].FindControl("BG_NUM");
                TextBox bgfznum = (TextBox)rptProNumCost.Items[r].FindControl("BG_FZNUM");
                string ptcsave = (rptProNumCost.Items[r].FindControl("Aptcode") as System.Web.UI.WebControls.TextBox).Text;
                string sqlsave = "update TBPC_BG set BG_NUM='" + Convert.ToDouble(bgnum.Text.ToString().Trim()) + "',BG_FZNUM='" + Convert.ToDouble(bgfznum.Text.ToString().Trim()) + "',BG_DATE='" + bgdate + "' where BG_PTC='" + ptcsave + "' and BG_PH='" + ph + "'";
                list0.Add(sqlsave);
            }
            DBCallCommon.ExecuteTrans(list0);

            List<string> listtj = new List<string>();
            for (int r = 0; r < rptProNumCost.Items.Count; r++)
            {
                string ptctj = (rptProNumCost.Items[r].FindControl("Aptcode") as System.Web.UI.WebControls.TextBox).Text;
                string sqlsave = "update TBPC_BG set BG_STATE='1' where BG_PTC='" + ptctj + "' and BG_PH='" + ph + "'";
                listtj.Add(sqlsave);
            }
            DBCallCommon.ExecuteTrans(listtj);

            //邮件提醒
            string sprid = "";
            string sptitle = "";
            string spcontent = "";
            sptitle = "物料减少审批";
            spcontent = "有物料减少需要您审批，请登录查看！";
            string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_first.Text.Trim() + "'";
            System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
            if (dtgetstid.Rows.Count > 0)
            {
                sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
            }
            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

            binddata();
            zxztbind();
            kjifvisible();
            Response.Redirect("PC_TBPC_Purchange_new.aspx");
        }


        //审核1
        protected void btnsh1_click(object sender, EventArgs e)
        {
            if (rblfirst.SelectedValue == "0")
            {
                string sql1 = "update TBPC_BG set BG_STATE='2',RESULT='审核中',BG_SHRA='" + name + "',BG_SHRIDA='" + userid + "',BG_SHYJA='" + first_opinion.Text.ToString() + "',BG_SHDATEA='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' where BG_PH='" + ph + "'";
                DBCallCommon.ExeSqlText(sql1);

                //邮件提醒
                string sprid = "";
                string sptitle = "";
                string spcontent = "";
                sptitle = "物料减少审批";
                spcontent = "有物料减少需要您审批，请登录查看！";
                string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_second.Text.Trim() + "'";
                System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                if (dtgetstid.Rows.Count > 0)
                {
                    sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                }
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
            }
            if (rblfirst.SelectedValue == "1")
            {
                string sqltext1 = "update TBPC_BG set BG_STATE='6',RESULT='已驳回',BG_SHRA='" + name + "',BG_SHRIDA='" + userid + "',BG_SHYJA='" + first_opinion.Text.ToString() + "',BG_SHDATEA='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' where BG_PH='" + ph + "'";
                DBCallCommon.ExeSqlText(sqltext1);
                btnsh1.Visible = false;
            }
            binddata();
            zxztbind();
            kjifvisible();
            Response.Redirect("PC_TBPC_Purchange_new.aspx");
        }

        //审核2
        protected void btnsh2_click(object sender, EventArgs e)
        {
            if (rblsecond.SelectedValue == "0")
            {
                string sql2 = "update TBPC_BG set BG_STATE='3',RESULT='审核中',BG_SHRB='" + name + "',BG_SHRIDB='" + userid + "',BG_SHYJB='" + second_opinion.Text.ToString() + "',BG_SHDATEB='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' where BG_PH='" + ph + "'";
                DBCallCommon.ExeSqlText(sql2);

                //邮件提醒
                string sprid = "";
                string sptitle = "";
                string spcontent = "";
                sptitle = "物料减少审批";
                spcontent = "有物料减少需要您审批，请登录查看！";
                string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + rblthird.Text.Trim() + "'";
                System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                if (dtgetstid.Rows.Count > 0)
                {
                    sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                }
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
            }
            if (rblsecond.SelectedValue == "1")
            {
                string sqltext2 = "update TBPC_BG set BG_STATE='7',RESULT='已驳回',BG_SHRB='" + name + "',BG_SHRIDB='" + userid + "',BG_SHYJB='" + second_opinion.Text.ToString() + "',BG_SHDATEB='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' where BG_PH='" + ph + "'";
                DBCallCommon.ExeSqlText(sqltext2);
                btnsh2.Visible = false;
            }
            binddata();
            zxztbind();
            kjifvisible();
            Response.Redirect("PC_TBPC_Purchange_new.aspx");
        }



        //审核3
        protected void btnsh3_click(object sender, EventArgs e)
        {
            //2016.12.8修改
            System.Data.DataTable dt_cgr_1 = new System.Data.DataTable();
            dt_cgr_1.Columns.Add("CGR_ID", typeof(string));
            dt_cgr_1.Columns.Add("CG_JHGZH", typeof(string));

            if (rblthird.SelectedValue == "0")
            {
                //2016.12.8修改
                for (int w = 0; w < rptProNumCost.Items.Count; w++)
                {
                    string ptccgr_cgy = (rptProNumCost.Items[w].FindControl("Aptcode") as System.Web.UI.WebControls.TextBox).Text;
                    string sqlcgr_cgy = "select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptccgr_cgy + "'";
                    DataTable dtcgr_cgy = DBCallCommon.GetDTUsingSqlText(sqlcgr_cgy);
                    string getcgrposition_cgy = "select ST_POSITION,ST_ID,ST_NAME from TBDS_STAFFINFO where ST_ID='" + dtcgr_cgy.Rows[0]["PUR_CGMAN"] + "'";
                    DataTable dtgetposition_cgy = DBCallCommon.GetDTUsingSqlText(getcgrposition_cgy);
                    if (dtgetposition_cgy.Rows.Count > 0)
                    {
                        string cgrid_cgy = dtgetposition_cgy.Rows[0]["ST_ID"].ToString();
                        if (!string.IsNullOrEmpty(cgrid_cgy))
                        {
                            dt_cgr_1.Rows.Add(new object[] { cgrid_cgy, ptccgr_cgy });
                        }
                    }
                }
                if (dt_cgr_1.Rows.Count > 0)
                {
                    System.Data.DataTable dt_cgr_2 = dt_cgr_1.Clone();
                    dt_cgr_2.PrimaryKey = new DataColumn[] { dt_cgr_2.Columns["CGR_ID"] };
                    foreach (DataRow row in dt_cgr_1.Rows)
                    {
                        DataRow srow = dt_cgr_2.Rows.Find(new object[] { row["CGR_ID"].ToString() });
                        if (srow == null)
                        {
                            dt_cgr_2.Rows.Add(row.ItemArray);
                        }
                        else
                        {
                            srow["CG_JHGZH"] = srow["CG_JHGZH"].ToString() + "," + row["CG_JHGZH"].ToString();
                        }
                    }
                    if (dt_cgr_2.Rows.Count > 1)
                    {
                        string message_about_cgy = "";
                        string message_about_alert = "";
                        for (int v = 0; v < dt_cgr_2.Rows.Count; v++)
                        {
                            message_about_cgy += dt_cgr_2.Rows[v]["CG_JHGZH"].ToString() + "\r\n与\r\n";
                        }
                        message_about_cgy = message_about_cgy.Remove(message_about_cgy.Length - 3);
                        message_about_alert = message_about_cgy.Replace("\r\n", "\\n");
                        message_about_cgy += "有不同的采购员，请分别提交物料变更！";
                        //message_about_cgy = message_about_cgy.Replace("\r\n","");
                        message_about_alert += "\\n有不同的采购员，请驳回，并要求技术部分别提交物料变更！";
                        third_opinion.Text = message_about_cgy;
                        Response.Write("<script>alert('" + message_about_alert + "!')</script>");
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('" + message_about_alert + "')", true);
                        return;
                    }

                }



                int k = 0;
                for (int i = 0; i < rptProNumCost.Items.Count; i++)
                {
                    TextBox zxzt = (TextBox)rptProNumCost.Items[i].FindControl("tbzxzt");
                    if (zxzt.Text == "已分工" || zxzt.Text == "已代用" || zxzt.Text == "已生成询价单" || zxzt.Text == "已生成订单")
                    {
                        k++;
                    }
                }
                if (k > 0)
                {
                    for (int j = 0; j < rptProNumCost.Items.Count; j++)
                    {
                        string ptccgr = (rptProNumCost.Items[j].FindControl("Aptcode") as System.Web.UI.WebControls.TextBox).Text;
                        string sqlcgr = "select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptccgr + "'";
                        DataTable dtcgr = DBCallCommon.GetDTUsingSqlText(sqlcgr);
                        string getcgrposition = "select ST_POSITION,ST_ID,ST_NAME from TBDS_STAFFINFO where ST_ID='" + dtcgr.Rows[0]["PUR_CGMAN"] + "'";
                        DataTable dtgetposition = DBCallCommon.GetDTUsingSqlText(getcgrposition);
                        cgrposition = dtgetposition.Rows[0]["ST_POSITION"].ToString();
                        cgrid = dtgetposition.Rows[0]["ST_ID"].ToString();
                        cgrname = dtgetposition.Rows[0]["ST_NAME"].ToString();
                    }
                    string sql3 = "update TBPC_BG set BG_STATE='4',BG_SHJB='4',RESULT='审核中',BG_SHRC='" + name + "',BG_SHRIDC='" + userid + "',BG_SHYJC='" + third_opinion.Text.ToString() + "',BG_SHDATEC='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "',BG_SHRD='" + cgrname + "',BG_SHRIDD='" + cgrid + "' where BG_PH='" + ph + "'";
                    DBCallCommon.ExeSqlText(sql3);
                }
                else
                {
                    string sql4 = "update TBPC_BG set BG_STATE='4',RESULT='已执行',BG_SHJB='3',BG_SHRC='" + name + "',BG_SHRIDC='" + userid + "',BG_SHYJC='" + third_opinion.Text.ToString() + "',BG_SHDATEC='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' where BG_PH='" + ph + "'";
                    DBCallCommon.ExeSqlText(sql4);
                    updatedata();//三级审核更改数据；
                    hiddata();//三级审核隐藏数据；
                }


                //邮件提醒
                string sprid = "";
                string sptitle = "";
                string spcontent = "";
                sptitle = "物料减少审批";
                spcontent = "有物料减少需要您审批，请登录查看！";
                string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_forth.Text.Trim() + "'";
                System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                if (dtgetstid.Rows.Count > 0)
                {
                    sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                }
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
            }
            if (rblthird.SelectedValue == "1")
            {
                string sqltext3 = "update TBPC_BG set BG_STATE='8',RESULT='已驳回',BG_SHRC='" + name + "',BG_SHRIDC='" + userid + "',BG_SHYJC='" + third_opinion.Text.ToString() + "',BG_SHDATEC='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' where BG_PH='" + ph + "'";
                DBCallCommon.ExeSqlText(sqltext3);
                btnsh3.Visible = false;
            }
            binddata();
            zxztbind();
            kjifvisible();
            Response.Redirect("PC_TBPC_Purchange_new.aspx");
        }

        //三级审核更改数据
        private void updatedata()
        {
            List<string> list = new List<string>();
            for (int j = 0; j < rptProNumCost.Items.Count; j++)
            {
                string ptc = (rptProNumCost.Items[j].FindControl("Aptcode") as System.Web.UI.WebControls.TextBox).Text;
                string sqltext = "select BG_NUM,BG_FZNUM from TBPC_BG where BG_PTC='" + ptc + "' and BG_PH='" + ph + "'";
                DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt0.Rows.Count > 0)
                {
                    decimal num = Convert.ToDecimal(dt0.Rows[0]["BG_NUM"].ToString());
                    decimal fznum = Convert.ToDecimal(dt0.Rows[0]["BG_FZNUM"].ToString());
                    string sql = "update TBPC_PURCHASEPLAN set PUR_NUM=PUR_NUM-" + num + ",PUR_FZNUM=PUR_FZNUM-" + fznum + ",PUR_RPNUM=PUR_RPNUM-" + num + ",PUR_RPFZNUM=PUR_RPFZNUM-" + fznum + " where PUR_PTCODE='" + ptc + "'";
                    list.Add(sql);
                }
            }
            DBCallCommon.ExecuteTrans(list);
        }
        //三级审核隐藏数据
        private void hiddata()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < rptProNumCost.Items.Count; i++)
            {
                string ptc = (rptProNumCost.Items[i].FindControl("Aptcode") as System.Web.UI.WebControls.TextBox).Text;
                string sqltext = "select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptc + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (Convert.ToDouble(dt.Rows[0]["PUR_NUM"].ToString()) == 0 && Convert.ToDouble(dt.Rows[0]["PUR_RPNUM"].ToString()) == 0)
                {
                    string sql = "update TBPC_PURCHASEPLAN set PUR_STATE='9' where PUR_PTCODE='" + ptc + "'";
                    list.Add(sql);
                }
            }
            DBCallCommon.ExecuteTrans(list);
        }




        //审核4
        protected void btnsh4_click(object sender, EventArgs e)
        {
            if (rblforth.SelectedValue == "0")
            {
                string sql4 = "update TBPC_BG set BG_STATE='5',RESULT='已执行',BG_SHJB='4',BG_SHRD='" + name + "',BG_SHRIDD='" + userid + "',BG_SHYJD='" + forth_opinion.Text.ToString() + "',BG_SHDATED='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' where BG_PH='" + ph + "'";
                DBCallCommon.ExeSqlText(sql4);
                updatedata2();//四级审核更新数据
                hiddata2();//四级审核隐藏数据
            }
            if (rblforth.SelectedValue == "1")
            {
                string sqltext4 = "update TBPC_BG set BG_STATE='9',RESULT='已驳回',BG_SHRD='" + name + "',BG_SHRIDD='" + userid + "',BG_SHYJD='" + forth_opinion.Text.ToString() + "',BG_SHDATED='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "' where BG_PH='" + ph + "'";
                DBCallCommon.ExeSqlText(sqltext4);
                btnsh4.Visible = false;
            }
            binddata();
            zxztbind();
            kjifvisible();
            Response.Redirect("PC_TBPC_Purchange_new.aspx");
        }


        private void updatedata2()
        {
            List<string> list0 = new List<string>();
            string xjdh = "";
            for (int j = 0; j < rptProNumCost.Items.Count; j++)
            {
                string ptc = (rptProNumCost.Items[j].FindControl("Aptcode") as System.Web.UI.WebControls.TextBox).Text;
                string sqlupdate = "select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptc + "'";
                DataTable dtupdate = DBCallCommon.GetDTUsingSqlText(sqlupdate);
                string sqltext = "select BG_NUM,BG_FZNUM from TBPC_BG where BG_PTC='" + ptc + "' and BG_PH='" + ph + "'";
                DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqltext);
                //修改需用计划表
                if (Convert.ToInt32(dtupdate.Rows[0]["PUR_STATE"].ToString()) > 3 && dtupdate.Rows.Count > 0 && dt0.Rows.Count > 0)
                {
                    decimal num1 = Convert.ToDecimal(dt0.Rows[0]["BG_NUM"].ToString());
                    decimal fznum1 = Convert.ToDecimal(dt0.Rows[0]["BG_FZNUM"].ToString());
                    string sql1 = "update TBPC_PURCHASEPLAN set PUR_NUM=PUR_NUM-" + num1 + ",PUR_FZNUM=PUR_FZNUM-" + fznum1 + ",PUR_RPNUM=PUR_RPNUM-" + num1 + ",PUR_RPFZNUM=PUR_RPFZNUM-" + fznum1 + " where PUR_PTCODE='" + ptc + "'";
                    list0.Add(sql1);
                }
                //修改询价单明细表
                if (Convert.ToInt32(dtupdate.Rows[0]["PUR_STATE"].ToString()) > 5 && dtupdate.Rows.Count > 0 && dt0.Rows.Count > 0)
                {
                    decimal num2 = Convert.ToDecimal(dt0.Rows[0]["BG_NUM"].ToString());
                    decimal fznum2 = Convert.ToDecimal(dt0.Rows[0]["BG_FZNUM"].ToString());
                    string sqlxjdh = "select * from TBPC_IQRCMPPRICE where PIC_PTCODE='" + ptc + "'";
                    DataTable dtxjdh = DBCallCommon.GetDTUsingSqlText(sqlxjdh);
                    xjdh = dtxjdh.Rows[0]["PIC_SHEETNO"].ToString();
                    string sql2 = "update TBPC_IQRCMPPRICE set PIC_QUANTITY=PIC_QUANTITY-" + num2 + ",PIC_FZNUM=PIC_FZNUM-" + fznum2 + ",PIC_ZXNUM=PIC_ZXNUM-" + num2 + ",PIC_ZXFUNUM=PIC_ZXFUNUM-" + fznum2 + " where PIC_PTCODE='" + ptc + "'";
                    DBCallCommon.ExeSqlText(sql2);
                    string bsql2 = "update TBPC_IQRCMPPRICE set PIC_QOUTELSTSA=isnull(PIC_QOUTESCDSA,0)*isnull(PIC_ZXNUM,0),PIC_QOUTELSTSB=isnull(PIC_QOUTESCDSB,0)*isnull(PIC_ZXNUM,0),PIC_QOUTELSTSC=isnull(PIC_QOUTESCDSC,0)*isnull(PIC_ZXNUM,0),PIC_QOUTELSTSD=isnull(PIC_QOUTESCDSD,0)*isnull(PIC_ZXNUM,0),PIC_QOUTELSTSE=isnull(PIC_QOUTESCDSE,0)*isnull(PIC_ZXNUM,0),PIC_QOUTELSTSF=isnull(PIC_QOUTESCDSF,0)*isnull(PIC_ZXNUM,0) where PIC_PTCODE='" + ptc + "'";
                    list0.Add(bsql2);


                    //修改询价单总金额
                    string sqlcheck = "select * from TBPC_IQRCMPPRICE where PIC_SHEETNO='" + xjdh + "'";
                    DataTable dtcheck = DBCallCommon.GetDTUsingSqlText(sqlcheck);
                    if (dtcheck.Rows.Count > 0)
                    {
                        string sqljsze = "select sum(isnull(PIC_ZXNUM,0)*isnull(PIC_PRICE,0)) as xjdzje from TBPC_IQRCMPPRICE where PIC_SHEETNO='" + xjdh + "'";
                        DataTable dtxjdze = DBCallCommon.GetDTUsingSqlText(sqljsze);
                        double xjdzje = Convert.ToDouble(dtxjdze.Rows[0]["xjdzje"].ToString());
                        string sqlxjdze = "update TBPC_IQRCMPPRCRVW set ICL_AMOUT=" + xjdzje + " where ICL_SHEETNO='" + xjdh + "'";
                        DBCallCommon.ExeSqlText(sqlxjdze);
                    }
                }
                //修改订单明细
                if (Convert.ToInt32(dtupdate.Rows[0]["PUR_STATE"].ToString()) > 6 && dtupdate.Rows.Count > 0 && dt0.Rows.Count > 0)
                {
                    decimal num3 = Convert.ToDecimal(dt0.Rows[0]["BG_NUM"].ToString());
                    decimal fznum3 = Convert.ToDecimal(dt0.Rows[0]["BG_FZNUM"].ToString());
                    string sql3 = "update TBPC_PURORDERDETAIL set PO_QUANTITY=PO_QUANTITY-" + num3 + ",PO_FZNUM=PO_FZNUM-" + fznum3 + " where PO_PCODE='" + ptc + "'";
                    list0.Add(sql3);
                }
            }
            DBCallCommon.ExecuteTrans(list0);
        }

        //四级审核隐藏数据
        private void hiddata2()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < rptProNumCost.Items.Count; i++)
            {
                string ptc = (rptProNumCost.Items[i].FindControl("Aptcode") as System.Web.UI.WebControls.TextBox).Text;
                string sqltext = "select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptc + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToDouble(dt.Rows[0]["PUR_NUM"].ToString()) == 0 && Convert.ToDouble(dt.Rows[0]["PUR_RPNUM"].ToString()) == 0)
                    {
                        string sql = "update TBPC_PURCHASEPLAN set PUR_STATE='9' where PUR_PTCODE='" + ptc + "'";
                        list.Add(sql);
                    }
                }
                string sqltexta = "select * from TBPC_IQRCMPPRICE where PIC_PTCODE='" + ptc + "'";
                DataTable dta = DBCallCommon.GetDTUsingSqlText(sqltexta);
                if (dta.Rows.Count > 0)
                {
                    if (Convert.ToDouble(dta.Rows[0]["PIC_QUANTITY"].ToString()) == 0)
                    {
                        string sqla = "delete from TBPC_IQRCMPPRICE where PIC_PTCODE='" + ptc + "'";
                        list.Add(sqla);
                    }
                }
                string sqltextb = "select * from TBPC_PURORDERDETAIL where PO_PCODE='" + ptc + "'";
                DataTable dtb = DBCallCommon.GetDTUsingSqlText(sqltextb);
                if (dtb.Rows.Count > 0)
                {
                    if (Convert.ToDouble(dtb.Rows[0]["PO_QUANTITY"].ToString()) == 0)
                    {
                        string sqlb = "delete from TBPC_PURORDERDETAIL where PO_PCODE='" + ptc + "'";
                        list.Add(sqlb);
                    }
                }
            }
            DBCallCommon.ExecuteTrans(list);
        }





        protected void btnsc_click(object sender, EventArgs e)
        {
            List<string> listsc = new List<string>();
            for (int r = 0; r < rptProNumCost.Items.Count; r++)
            {
                if ((rptProNumCost.Items[r].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptcdelete = (rptProNumCost.Items[r].FindControl("Aptcode") as System.Web.UI.WebControls.TextBox).Text;
                    string sqldelete = "delete from TBPC_BG where BG_PTC='" + ptcdelete + "' and BG_PH='" + ph + "'";
                    listsc.Add(sqldelete);
                }
            }
            DBCallCommon.ExecuteTrans(listsc);
            binddata();
            zxztbind();
            kjifvisible();
        }
    }
}
