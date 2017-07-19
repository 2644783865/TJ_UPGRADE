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
    public partial class OM_CanBuYDSPdetail : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
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
        private void bindmxdata()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqltextmx = "select * from OM_CanBuYDSP as a left join OM_CanBuYDdetal as b on a.SPBH=b.CBYD_SPBH left join TBDS_STAFFINFO as c on b.CBYD_STID=c.ST_ID left join TBDS_DEPINFO as d on c.ST_DEPID=d.DEP_CODE where SPBH='" + spbh.ToString().Trim() + "'";
            DataTable dtmx = DBCallCommon.GetDTUsingSqlText(sqltextmx);
            if (dtmx.Rows.Count > 0)
            {
                rptProNumCost.DataSource = dtmx;
                rptProNumCost.DataBind();
            }
            double tot_money = 0;
            for (int i = 0; i < dtmx.Rows.Count; i++)
            {
                string cta = dtmx.Rows[i]["CBYD_BF"].ToString();
                cta = cta == "" ? "0" : cta;
                tot_money += Convert.ToDouble(cta);
            }
            lb_select_money.Text = string.Format("{0:c}", tot_money);
        }
        private void bindspdata()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqltextsp = "select * from OM_CanBuYDSP where SPBH='" + spbh.ToString().Trim() + "'";
            DataTable dtsp = DBCallCommon.GetDTUsingSqlText(sqltextsp);
            if (dtsp.Rows.Count > 0)
            {
                lbtitle_zdr.Text = dtsp.Rows[0]["SQR_Name"].ToString().Trim();
                lbtitle_zdsj.Text = dtsp.Rows[0]["SQ_Time"].ToString().Trim();
                lb_title.Text = "(" + dtsp.Rows[0]["YearMonth"].ToString().Trim() + ")";


                tb_yearmonth.Text = dtsp.Rows[0]["YearMonth"].ToString().Trim();
                lbzdr.Text = dtsp.Rows[0]["SQR_Name"].ToString().Trim();
                lbzdtime.Text = dtsp.Rows[0]["SQ_Time"].ToString().Trim();
                txtfqryj.Text = dtsp.Rows[0]["SQR_Note"].ToString().Trim();
                txt_first.Text = dtsp.Rows[0]["SPR_Name"].ToString().Trim();
                firstid.Value = dtsp.Rows[0]["SPR_ID"].ToString().Trim();
                if (dtsp.Rows[0]["SPR_State"].ToString().Trim() == "1" || dtsp.Rows[0]["SPR_State"].ToString().Trim() == "2")
                {
                    rblfirst.SelectedValue = dtsp.Rows[0]["SPR_State"].ToString().Trim();
                }
                first_time.Text = dtsp.Rows[0]["SP_Time"].ToString().Trim();
                opinion1.Text = dtsp.Rows[0]["SPR_Note"].ToString().Trim();
            }
        }
        private void contrlkjx()
        {
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqltextkj = "select * from OM_CanBuYDSP where SPBH='" + spbh.ToString().Trim() + "'";
            DataTable dtkj = DBCallCommon.GetDTUsingSqlText(sqltextkj);
            if (dtkj.Rows.Count > 0)
            {
                if (dtkj.Rows[0]["TotalState"].ToString().Trim() == "2")
                {
                    ImageVerify.Visible = true;
                }
                if (dtkj.Rows[0]["TotalState"].ToString().Trim() == "0" && dtkj.Rows[0]["SPR_State"].ToString().Trim() == "0")//初始化
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["SQR_ID"].ToString().Trim())
                    {
                        btnadd.Visible = true;
                        btnSave.Visible = true;
                        tb_yearmonth.Enabled = true;
                        txt_first.Enabled = true;
                        hlSelect1.Visible = true;
                        txtfqryj.Enabled = true;
                        btndelete.Visible = true;
                    }
                }
                else if (dtkj.Rows[0]["TotalState"].ToString().Trim() == "1" && dtkj.Rows[0]["SPR_State"].ToString().Trim() == "0")//提交未审
                {
                    if (Session["UserID"].ToString().Trim() == dtkj.Rows[0]["SPR_ID"].ToString().Trim())
                    {
                        btnSubmit.Visible = true;
                        rblfirst.Enabled = true;
                        opinion1.Enabled = true;
                        for (int i = 0; i < rptProNumCost.Items.Count; i++)
                        {
                            TextBox txtCBYD_BF = (TextBox)rptProNumCost.Items[i].FindControl("txtCBYD_BF");
                            TextBox txtCBYD_CBBZ = (TextBox)rptProNumCost.Items[i].FindControl("txtCBYD_CBBZ");
                            TextBox txtCBYD_TZTS = (TextBox)rptProNumCost.Items[i].FindControl("txtCBYD_TZTS");
                            TextBox txtCBYD_Note = (TextBox)rptProNumCost.Items[i].FindControl("txtCBYD_Note");

                            txtCBYD_BF.Enabled = false;
                            txtCBYD_CBBZ.Enabled = false;
                            txtCBYD_TZTS.Enabled = false;
                            txtCBYD_Note.Enabled = false;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < rptProNumCost.Items.Count; j++)
                    {
                        TextBox txtCBYD_BF = (TextBox)rptProNumCost.Items[j].FindControl("txtCBYD_BF");
                        TextBox txtCBYD_CBBZ = (TextBox)rptProNumCost.Items[j].FindControl("txtCBYD_CBBZ");
                        TextBox txtCBYD_TZTS = (TextBox)rptProNumCost.Items[j].FindControl("txtCBYD_TZTS");
                        TextBox txtCBYD_Note = (TextBox)rptProNumCost.Items[j].FindControl("txtCBYD_Note");

                        txtCBYD_BF.Enabled = false;
                        txtCBYD_CBBZ.Enabled = false;
                        txtCBYD_TZTS.Enabled = false;
                        txtCBYD_Note.Enabled = false;
                    }
                    if (dtkj.Rows[0]["TotalState"].ToString().Trim() == "3" && Session["UserID"].ToString().Trim() == dtkj.Rows[0]["SQR_ID"].ToString().Trim())
                    {
                        btnfanshen.Visible = true;
                    }
                    else
                    {
                        btnfanshen.Visible = false;
                    }
                }
            }
        }

        //保存
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            int num = 0;
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sql000 = "";
            for (int i = 0; i < rptProNumCost.Items.Count; i++)
            {
                if (((Label)rptProNumCost.Items[i].FindControl("lbCBYD_STID")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("txtCBYD_BF")).Text.Trim() != "" && ((TextBox)rptProNumCost.Items[i].FindControl("txtCBYD_BF")).Text.Trim() != "0")
                {
                    num++;
                }
            }
            if (num == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "3", "alert('请填写完整要调整的人员信息和补发数据！');", true);
                return;
            }
            if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                return;
            }
            else if (tb_yearmonth.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月份！');", true);
                return;
            }
            else
            {
                string sqlText = "select * from OM_CanBu where CB_YearMonth ='" + tb_yearmonth.Text.Trim() + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                if (dt.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月餐补数据未生成！');", true);
                    return;
                }

                //删除已有数据
                string sqldelete = "delete from OM_CanBuYDdetal where CBYD_SPBH='" + spbh + "'";
                list.Add(sqldelete);
                //重新插入数据
                for (int k = 0; k < rptProNumCost.Items.Count; k++)
                {
                    Label lbCBYD_STID = (Label)rptProNumCost.Items[k].FindControl("lbCBYD_STID");
                    TextBox txtCBYD_BF = (TextBox)rptProNumCost.Items[k].FindControl("txtCBYD_BF");
                    TextBox txtCBYD_CBBZ = (TextBox)rptProNumCost.Items[k].FindControl("txtCBYD_CBBZ");
                    TextBox txtCBYD_TZTS = (TextBox)rptProNumCost.Items[k].FindControl("txtCBYD_TZTS");
                    TextBox txtCBYD_Note = (TextBox)rptProNumCost.Items[k].FindControl("txtCBYD_Note");

                    if (((Label)rptProNumCost.Items[k].FindControl("lbCBYD_STID")).Text.Trim() != "" || ((TextBox)rptProNumCost.Items[k].FindControl("lbST_NAME")).Text.Trim() != "")
                    {
                        sql000 = "insert into OM_CanBuYDdetal(CBYD_SPBH,CBYD_YearMonth,CBYD_STID,CBYD_TZTS,CBYD_CBBZ,CBYD_BF,CBYD_Note) values('" + spbh + "','" + tb_yearmonth.Text.Trim() + "','" + lbCBYD_STID.Text.Trim() + "'," + CommonFun.ComTryDecimal(txtCBYD_TZTS.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtCBYD_CBBZ.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtCBYD_BF.Text.Trim()) + ",'" + txtCBYD_Note.Text.Trim() + "')";
                        list.Add(sql000);
                    }
                }
                string sqlupdatesp = "update OM_CanBuYDSP set YearMonth='" + tb_yearmonth.Text.Trim() + "', TotalState='0',SQR_Note='" + txtfqryj.Text.Trim() + "',SPR_ID='" + firstid.Value.Trim() + "',SPR_Name='" + txt_first.Text.Trim() + "' where SPBH='" + spbh + "'";
                list.Add(sqlupdatesp);
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据更新成功！');", true);
                    btnSave.Visible = false;
                    btnSubmit.Visible = true;
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据更新失败！');", true);
                    return;
                }
            }
        }

        //提交
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string sql001 = "";
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqltext = "select * from OM_CanBuYDSP where SPBH='" + spbh.ToString().Trim() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["SQR_ID"].ToString().Trim() == Session["UserID"].ToString().Trim())
                {
                    if (txt_first.Text.Trim() == "" || firstid.Value.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                        return;
                    }
                    else
                    {
                        sql001 = "update OM_CanBuYDSP set TotalState='1',SQR_Note='" + txtfqryj.Text.Trim() + "',SPR_ID='" + firstid.Value.Trim() + "',SPR_Name='" + txt_first.Text.Trim() + "' where SPBH='" + spbh.ToString().Trim() + "'";
                        list.Add(sql001);

                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sprid = firstid.Value.Trim();
                        sptitle = "餐补异动审批";
                        spcontent = lb_title.Text.Trim() + "的餐补异动需要您审批，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                }
                if (dt.Rows[0]["SPR_ID"].ToString().Trim() == Session["UserID"].ToString().Trim())
                {
                    if (rblfirst.SelectedValue.ToString().Trim() == "1")
                    {
                        sql001 = "update OM_CanBuYDSP set SPR_State='" + rblfirst.SelectedValue.ToString().Trim() + "',TotalState='2',SP_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SPR_Note='" + opinion1.Text.Trim() + "' where SPBH='" + spbh.ToString().Trim() + "'";
                        list.Add(sql001);
                        updatecbsj(spbh);
                    }
                    else if (rblfirst.SelectedValue.ToString().Trim() == "2")
                    {
                        sql001 = "update OM_CanBuYDSP set SPR_State='" + rblfirst.SelectedValue.ToString().Trim() + "',TotalState='3',SP_Time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',SPR_Note='" + opinion1.Text.Trim() + "' where SPBH='" + spbh.ToString().Trim() + "'";
                        list.Add(sql001);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审核意见！');", true);
                        return;
                    }
                }
            }
            DBCallCommon.ExecuteTrans(list);
            Response.Redirect("OM_CanBuYDSPdetail.aspx?spid=" + spbh);
        }

        //更改餐补数据
        private void updatecbsj(string spbh)
        {
            List<string> list = new List<string>();
            //string stid = "";
            string detailbh = "";
            string sqlText = "";
            string sql = "";
            string sql0 = "";
            string sql1 = "";
            //string CB_STID = "";
            //string cb_stid = "";
            //for (int k = 0; k < rptProNumCost.Items.Count; k++)
            //{
            //    Label lbCBYD_STID = (Label)rptProNumCost.Items[k].FindControl("lbCBYD_STID");
            //    stid += CommonFun.ComTryInt(lbCBYD_STID.Text.Trim()) + ",";
            //}
            //string sqltext = "select CB_STID,detailbh from OM_CanBu where CB_STID in (" + stid.Substring(0, stid.Length - 1) + ") and detailbh is not NULL";
            //DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            //if (dt.Rows.Count > 0)
            //{
            sql0 = "select detailbh from OM_CanBu where  CB_YearMonth ='" + tb_yearmonth.Text.Trim() + "'";
            DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sql0);
            detailbh = dt0.Rows[0][0].ToString();
            for (int j = 0; j < rptProNumCost.Items.Count; j++)
            {
                Label lbCBYD_STID = (Label)rptProNumCost.Items[j].FindControl("lbCBYD_STID");
                //stid += CommonFun.ComTryInt(lbCBYD_STID.Text.Trim()) + ",";
                TextBox txtCBYD_BF = (TextBox)rptProNumCost.Items[j].FindControl("txtCBYD_BF");
                TextBox txtCBYD_CBBZ = (TextBox)rptProNumCost.Items[j].FindControl("txtCBYD_CBBZ");
                TextBox txtCBYD_TZTS = (TextBox)rptProNumCost.Items[j].FindControl("txtCBYD_TZTS");
                TextBox txtCBYD_Note = (TextBox)rptProNumCost.Items[j].FindControl("txtCBYD_Note");

                sqlText = "select * from OM_CanBu where CB_STID='" + lbCBYD_STID.Text.Trim() + "' and CB_YearMonth ='" + tb_yearmonth.Text.Trim() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                if (dt.Rows.Count > 0)
                {
                    sql = "update OM_CanBu set CB_BuShangYue='" + CommonFun.ComTryDecimal(txtCBYD_BF.Text.Trim()) + "',CB_BeiZhu='" + txtCBYD_Note.Text.Trim() + "'where CB_STID='" + lbCBYD_STID.Text.Trim() + "' and CB_YearMonth ='" + tb_yearmonth.Text.Trim() + "'";
                    list.Add(sql);
                }
                else
                {
                    sql1 = "insert into OM_CanBu (CB_YearMonth,CB_STID, CB_BuShangYue,CB_BIAOZ,CB_TZTS,CB_BeiZhu,detailbh) values ('" + tb_yearmonth.Text.Trim() + "','" + lbCBYD_STID.Text.Trim() + "','" + CommonFun.ComTryDecimal(txtCBYD_BF.Text.Trim()) + "','" + CommonFun.ComTryDecimal(txtCBYD_CBBZ.Text.Trim()) + "','" + CommonFun.ComTryDecimal(txtCBYD_TZTS.Text.Trim()) + "','" + txtCBYD_Note.Text.Trim() + "','" + detailbh + "')";
                    list.Add(sql1);
                }
            }
            //}
            DBCallCommon.ExecuteTrans(list);
        }

        //添加行
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
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CBYD_ID");
            dt.Columns.Add("ST_NAME");
            dt.Columns.Add("CBYD_STID");
            dt.Columns.Add("ST_WORKNO");
            dt.Columns.Add("DEP_NAME");

            dt.Columns.Add("CBYD_TZTS");
            dt.Columns.Add("CBYD_CBBZ");

            dt.Columns.Add("CBYD_BF");
            dt.Columns.Add("CBYD_Note");

            foreach (RepeaterItem retItem in rptProNumCost.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((Label)retItem.FindControl("lbCBYD_ID")).Text;
                newRow[1] = ((TextBox)retItem.FindControl("lbST_NAME")).Text;
                newRow[2] = ((Label)retItem.FindControl("lbCBYD_STID")).Text;
                newRow[3] = ((TextBox)retItem.FindControl("txtST_WORKNO")).Text;
                newRow[4] = ((TextBox)retItem.FindControl("txtDEP_NAME")).Text;

                newRow[5] = ((TextBox)retItem.FindControl("txtCBYD_TZTS")).Text;
                newRow[6] = ((TextBox)retItem.FindControl("txtCBYD_CBBZ")).Text;

                newRow[7] = ((TextBox)retItem.FindControl("txtCBYD_BF")).Text;
                newRow[8] = ((TextBox)retItem.FindControl("txtCBYD_Note")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
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
                    ((Label)Reitem.FindControl("lbCBYD_STID")).Text = stid;
                    ((TextBox)Reitem.FindControl("lbST_NAME")).Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txtDEP_NAME")).Text = dt.Rows[0]["DEP_NAME"].ToString().Trim();
                    ((TextBox)Reitem.FindControl("txtST_WORKNO")).Text = dt.Rows[0]["ST_WORKNO"].ToString().Trim();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员不存在，请重新输入！');", true);
                }
            }
        }

        //删除
        protected void btndelete_OnClick(object sender, EventArgs e)
        {
            List<string> col = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("CBYD_ID");
            dt.Columns.Add("ST_NAME");
            dt.Columns.Add("CBYD_STID");
            dt.Columns.Add("ST_WORKNO");
            dt.Columns.Add("DEP_NAME");

            dt.Columns.Add("CBYD_TZTS");
            dt.Columns.Add("CBYD_CBBZ");

            dt.Columns.Add("CBYD_BF");
            dt.Columns.Add("CBYD_Note");

            foreach (RepeaterItem retItem in rptProNumCost.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("CKBOX_SELECT");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((Label)retItem.FindControl("lbCBYD_ID")).Text;
                    newRow[1] = ((TextBox)retItem.FindControl("lbST_NAME")).Text;
                    newRow[2] = ((Label)retItem.FindControl("lbCBYD_STID")).Text;
                    newRow[3] = ((TextBox)retItem.FindControl("txtST_WORKNO")).Text;
                    newRow[4] = ((TextBox)retItem.FindControl("txtDEP_NAME")).Text;

                    newRow[5] = ((TextBox)retItem.FindControl("txtCBYD_TZTS")).Text;
                    newRow[6] = ((TextBox)retItem.FindControl("txtCBYD_CBBZ")).Text;

                    newRow[7] = ((TextBox)retItem.FindControl("txtCBYD_BF")).Text;
                    newRow[8] = ((TextBox)retItem.FindControl("txtCBYD_Note")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.rptProNumCost.DataSource = dt;
            this.rptProNumCost.DataBind();
        }

        //反审
        protected void btnfanshen_OnClick(object sender, EventArgs e)
        {
            List<string> list0 = new List<string>();
            spbh = Request.QueryString["spid"].ToString().Trim();
            string sqltext = "update OM_CanBuYDSP set TotalState='0',SPR_State='0' where SPBH='" + spbh + "'";
            list0.Add(sqltext);
            DBCallCommon.ExecuteTrans(list0);
            Response.Redirect("OM_CanBuYDSPdetail.aspx?spid=" + spbh);
        }
    }
}
