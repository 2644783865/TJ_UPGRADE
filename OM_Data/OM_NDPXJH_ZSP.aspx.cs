using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_NDPXJH_ZSP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                asd.action = Request.QueryString["action"];
                asd.username = Session["UserName"].ToString();
                asd.userid = Session["UserID"].ToString();
                asd.depname = Session["UserDept"].ToString();

                BindData();
                PowerControl();
                asd.sfdjbc = "n";
            }
        }

        private class asd
        {
            public static string pxid;
            public static string action;
            public static string username;
            public static string userid;
            public static string depname;
            public static string sjid;
            public static string sfdjbc;//记录是否执行了保存事件
            public static DataTable dt;


            public static string bumen;
            public static string time;
        }

        private void BindData()
        {
            if (asd.action == "hz")
            {
                asd.sjid = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string sql = "select * from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID=b.SPFATHERID where SPLX='NDPXJH' and PX_SJID1 is null and SPZT='y' and PX_YEAR='" + DateTime.Now.ToString("yyyy") + "' order by PX_SJID,PX_BM";
                asd.dt = DBCallCommon.GetDTUsingSqlText(sql);
                BindPanel(panJBXX);
                BindRepeater();
                txtZDR.Text = asd.username;
                hidZDRID.Value = asd.userid;
                lbZDR_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else if (asd.action == "alter")
            {
                asd.sjid = Request.QueryString["id"];
                string sql = " select * from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID1=b.SPFATHERID where SPLX='NDPXJH' and PX_SJID1='" + asd.sjid + "' order by PX_SJID,PX_BM";
                asd.dt = DBCallCommon.GetDTUsingSqlText(sql);
                BindPanel(panJBXX);
                BindRepeater();
                BindPanel(panSPR1);
                BindPanel(panSPR2);
                BindPanel(panSPR3);
                BindPanel(panZDR);
            }
            else if (asd.action == "read")
            {
                asd.sjid = Request.QueryString["id"];
                string sql = " select * from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID1=b.SPFATHERID and b.SPLX='NDPXJH' where PX_SJID1='" + asd.sjid + "' order by PX_SJID,PX_BM";
                asd.dt = DBCallCommon.GetDTUsingSqlText(sql);
                BindPanel(panJBXX);
                BindRepeater();
                BindPanel(panSPR1);
                BindPanel(panSPR2);
                BindPanel(panSPR3);
                BindPanel(panZDR);
            }
            else if (asd.action == "check")
            {
                asd.sjid = Request.QueryString["id"];
                string sql = " select * from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID1=b.SPFATHERID and b.SPLX='NDPXJH' where PX_SJID1='" + asd.sjid + "' order by PX_SJID,PX_BM";
                asd.dt = DBCallCommon.GetDTUsingSqlText(sql);
                BindPanel(panJBXX);
                BindRepeater();
                BindPanel(panSPR1);
                BindPanel(panSPR2);
                BindPanel(panSPR3);
                BindPanel(panZDR);
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

        private void BindRepeater()
        {
            rptPXJH.DataSource = asd.dt;
            rptPXJH.DataBind();
        }

        private void PowerControl()
        {
            btnBack.Visible = false;
            btnSave.Visible = false;
            btnSubmit.Visible = false;
            btnDelete.Visible = false;
            btnAdd.Visible = false;
            cbxQX.Visible = false;
            panJBXX.Enabled = false;
            panZDR.Enabled = false;
            panSPR1.Enabled = false;
            panSPR2.Enabled = false;
            panSPR3.Enabled = false;
            if (asd.action == "read")
            {
                if (asd.dt.Rows[0]["SPZT"].ToString() == "0" && asd.dt.Rows[0]["ZDRID"].ToString() == asd.userid)
                {
                    btnSubmit.Visible = true;
                }
            }
            else if (asd.action == "check")
            {
                btnBack.Visible = true;
                btnSave.Visible = true;
                if (asd.dt.Rows[0]["SPR1ID"].ToString() == asd.userid)
                {
                    panSPR1.Enabled = true;
                }
                if (asd.dt.Rows[0]["SPR2ID"].ToString() == asd.userid)
                {
                    panSPR2.Enabled = true;
                }
                if (asd.dt.Rows[0]["SPR3ID"].ToString() == asd.userid)
                {
                    panSPR3.Enabled = true;
                }
            }
            else if (asd.action == "hz" || asd.action == "alter")
            {
                btnSubmit.Visible = true;
                btnBack.Visible = true;
                btnSave.Visible = true;
                //cbxQX.Visible = true;
                btnDelete.Visible = true;
                panJBXX.Enabled = true;
                panZDR.Enabled = true;
                panSPR1.Enabled = true;
                panSPR2.Enabled = true;
                panSPR3.Enabled = true;
            }
        }

        protected void cbxQX_OnCheckedChanged(object sender, EventArgs e)
        {
            if (cbxQX.Checked)
            {
                foreach (RepeaterItem item in rptPXJH.Items)
                {
                    ((CheckBox)item.FindControl("cbxXuHao")).Checked = true;
                }
            }
            else
            {
                foreach (RepeaterItem item in rptPXJH.Items)
                {
                    ((CheckBox)item.FindControl("cbxXuHao")).Checked = false;
                }
            }
        }

        #region 增加、删除行
        protected void btnAdd_OnClick(object sender, EventArgs e) //增加行的函数
        {
            CreateNewRow(1);
            NoDataPanelSee();
        }

        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                newRow[2] = asd.depname;
                dt.Rows.Add(newRow);
            }
            this.rptPXJH.DataSource = dt;
            this.rptPXJH.DataBind();
            //InitVar();
        }

        private DataTable GetDataTable() //临时表
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PX_ID");
            dt.Columns.Add("PX_SJID");
            dt.Columns.Add("PX_FS");
            dt.Columns.Add("PX_BM");
            dt.Columns.Add("PX_XMMC");
            dt.Columns.Add("PX_SJ");
            dt.Columns.Add("PX_DD");
            dt.Columns.Add("PX_ZJR");
            dt.Columns.Add("PX_DX");
            dt.Columns.Add("PX_RS");
            dt.Columns.Add("PX_XS");
            dt.Columns.Add("PX_FYYS");
            dt.Columns.Add("PX_BZ");
            foreach (RepeaterItem retItem in rptPXJH.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((HiddenField)retItem.FindControl("PX_ID")).Value;
                newRow[1] = ((HiddenField)retItem.FindControl("PX_SJID")).Value;
                newRow[2] = ((DropDownList)retItem.FindControl("PX_FS")).SelectedValue;
                newRow[3] = ((TextBox)retItem.FindControl("PX_BM")).Text;
                newRow[4] = ((TextBox)retItem.FindControl("PX_XMMC")).Text;
                newRow[5] = ((DropDownList)retItem.FindControl("PX_SJ")).SelectedValue;
                newRow[6] = ((TextBox)retItem.FindControl("PX_DD")).Text;
                newRow[7] = ((TextBox)retItem.FindControl("PX_ZJR")).Text;
                newRow[8] = ((TextBox)retItem.FindControl("PX_DX")).Text;
                newRow[9] = ((TextBox)retItem.FindControl("PX_RS")).Text;
                newRow[10] = ((TextBox)retItem.FindControl("PX_XS")).Text;
                newRow[11] = ((TextBox)retItem.FindControl("PX_FYYS")).Text;
                newRow[12] = ((TextBox)retItem.FindControl("PX_BZ")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)//删除一行
        {
            //Response.Write("<script>if(confirm('驳回后，该条数据将会重新进入审批流程，确实要驳回吗？')){return true;}else { return false;}</script>");
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "if(confirm('驳回后，该条数据将会重新进入审批流程，确实要驳回吗？')){return true;}else { return false;}",true);

            string sjid = "";
            List<string> list = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("PX_ID");
            dt.Columns.Add("PX_SJID");
            dt.Columns.Add("PX_FS");
            dt.Columns.Add("PX_BM");
            dt.Columns.Add("PX_XMMC");
            dt.Columns.Add("PX_SJ");
            dt.Columns.Add("PX_DD");
            dt.Columns.Add("PX_ZJR");
            dt.Columns.Add("PX_DX");
            dt.Columns.Add("PX_RS");
            dt.Columns.Add("PX_XS");
            dt.Columns.Add("PX_FYYS");
            dt.Columns.Add("PX_BZ");
            foreach (RepeaterItem retItem in rptPXJH.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("cbxXuHao");
                if (chk.Checked)
                {
                    if (sjid != ((HiddenField)retItem.FindControl("PX_SJID")).Value && ((HiddenField)retItem.FindControl("PX_SJID")).Value != "")
                    {
                        sjid = ((HiddenField)retItem.FindControl("PX_SJID")).Value;
                        list.Add(sjid);
                    }
                }
            }
            foreach (RepeaterItem retItem in rptPXJH.Items)
            {
                if (!list.Contains(((HiddenField)retItem.FindControl("PX_SJID")).Value))
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((HiddenField)retItem.FindControl("PX_ID")).Value;
                    newRow[1] = ((HiddenField)retItem.FindControl("PX_SJID")).Value;
                    newRow[2] = ((DropDownList)retItem.FindControl("PX_FS")).SelectedValue;
                    newRow[3] = ((TextBox)retItem.FindControl("PX_BM")).Text;
                    newRow[4] = ((TextBox)retItem.FindControl("PX_XMMC")).Text;
                    newRow[5] = ((DropDownList)retItem.FindControl("PX_SJ")).SelectedValue;
                    newRow[6] = ((TextBox)retItem.FindControl("PX_DD")).Text;
                    newRow[7] = ((TextBox)retItem.FindControl("PX_ZJR")).Text;
                    newRow[8] = ((TextBox)retItem.FindControl("PX_DX")).Text;
                    newRow[9] = ((TextBox)retItem.FindControl("PX_RS")).Text;
                    newRow[10] = ((TextBox)retItem.FindControl("PX_XS")).Text;
                    newRow[11] = ((TextBox)retItem.FindControl("PX_FYYS")).Text;
                    newRow[12] = ((TextBox)retItem.FindControl("PX_BZ")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.rptPXJH.DataSource = dt;
            this.rptPXJH.DataBind();
            NoDataPanelSee();
            //驳回操作
            List<string> list1 = new List<string>();
            for (int i = 0,length=list.Count; i < length; i++)
            {
                string sql = "update OM_SP set SPZT ='n' where SPFATHERID ='" + list[i] + "'";
                list1.Add(sql);
            }
            try
            {
                DBCallCommon.ExecuteTrans(list1);
            }
            catch 
            {
                Response.Write("<script>alert('驳回的sql语句出现问题，请与管理员联系！！！')</script>");
                return;
            }
        }
        private void NoDataPanelSee()
        {
            if (rptPXJH.Items.Count > 0)
            {
                NoDataPanel.Visible = false;
            }
            else
            {
                NoDataPanel.Visible = true;
            }
        }
        #endregion

        protected void rptPXJH_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                asd.bumen = string.Empty;
                return;
            };
            DataRowView drv = (DataRowView)e.Item.DataItem;
            TextBox bumen = (TextBox)e.Item.FindControl("PX_BM");
            if (bumen.Text == asd.bumen && asd.time == drv["PX_SJID"].ToString())
            {
                bumen.Visible = false;
            }
            else
            {
                asd.bumen = bumen.Text;
                asd.time = drv["PX_SJID"].ToString();
            }
            ((DropDownList)e.Item.FindControl("PX_FS")).SelectedValue = drv["PX_FS"].ToString();
            ((DropDownList)e.Item.FindControl("PX_SJ")).SelectedValue = drv["PX_SJ"].ToString();

        }

        protected void btnSubmit_onserverclick(object sender, EventArgs e)
        {
            if (asd.action == "hz" || asd.action == "alter")
            {
                if (asd.sfdjbc != "y")
                {
                    Response.Write("<script>alert('您还未保存数据，请先保存数据之后再提交！！！')</script>");
                    return;
                }
                string sql = "update OM_SP set SPZT='1',SPJB='" + rblSPJB.SelectedValue + "'";
                if (rblSPJB.SelectedValue == "1")
                {
                    if (txtSPR1.Text.Trim() == "")
                    {
                        Response.Write("<script>alert('请先选择审批人再提交审批！！！')</script>");
                        return;
                    }
                    sql += ",SPR1='" + txtSPR1.Text.Trim() + "',SPR1ID='" + hidSPR1ID.Value + "'";
                }
                else if (rblSPJB.SelectedValue == "2")
                {
                    if (txtSPR2.Text.Trim() == "" || txtSPR1.Text.Trim() == "")
                    {
                        Response.Write("<script>alert('请先选择审批人再提交审批！！！')</script>");
                        return;
                    }
                    sql += ",SPR1='" + txtSPR1.Text.Trim() + "',SPR1ID='" + hidSPR1ID.Value + "'";
                    sql += ",SPR2='" + txtSPR2.Text.Trim() + "',SPR2ID='" + hidSPR2ID.Value + "'";
                }
                else if (rblSPJB.SelectedValue == "3")
                {
                    if (txtSPR2.Text.Trim() == "" || txtSPR1.Text.Trim() == "" || txtSPR3.Text.Trim() == "")
                    {
                        Response.Write("<script>alert('请先选择审批人再提交审批！！！')</script>");
                        return;
                    }
                    sql += ",SPR1='" + txtSPR1.Text.Trim() + "',SPR1ID='" + hidSPR1ID.Value + "'";
                    sql += ",SPR2='" + txtSPR2.Text.Trim() + "',SPR2ID='" + hidSPR2ID.Value + "'";
                    sql += ",SPR3='" + txtSPR3.Text.Trim() + "',SPR3ID='" + hidSPR3ID.Value + "'";
                }
                sql += " where SPFATHERID='" + asd.sjid + "' and SPLX='NDPXJH'";
                try
                {
                    DBCallCommon.ExeSqlText(sql);

                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sprid = hidSPR1ID.Value.Trim();
                    sptitle = "年度培训计划汇总审批";
                    spcontent = "有年度培训计划汇总需要您审批，请登录查看！";
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                }
                catch
                {
                    Response.Write("<script>alert('提交审批的sql语句出现问题，请与管理员联系！！！')</script>");
                    return;
                }
                Response.Redirect("OM_NDPXJJH_HZSP.aspx");
            }
        }

        protected void btnSave_onserverclick(object sender, EventArgs e)
        {
            asd.sfdjbc = "n";
            int a = SaveControl();
            if (a == 1)
            {
                Response.Write("<script>alert('请选择“同意”或者“不同意”再提交！！！')</script>");
                return;
            }
            if (asd.action == "check")
            {
                try
                {
                    List<string> list = checklist();
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('审批的sql语句出现问题，请与管理员联系！！！')</script>");
                    return;
                }
                Response.Redirect("OM_NDPXJJH_HZSP.aspx");
            }
            else if (asd.action == "hz")
            {
                try
                {
                    List<string> list = hzlist();
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('汇总的sql语句出现问题，请与管理员联系！！！')</script>");
                    return;
                }
                btnSave.Visible = false;
            }
            else if (asd.action == "alter")
            {
                updatepxsq();
                asd.sjid = Request.QueryString["id"];
                string sql = " select * from OM_PXJH_SQ as a left join OM_SP as b on a.PX_SJID1=b.SPFATHERID where SPLX='NDPXJH' and PX_SJID1='" + asd.sjid + "' order by PX_SJID,PX_BM";
                asd.dt = DBCallCommon.GetDTUsingSqlText(sql);
                BindRepeater();
            }
            asd.sfdjbc = "y";
        }

        //更新培训申请
        private void updatepxsq()
        {
            List<string> listsql = new List<string>();
            string sqlupdatesqinfo = "";
            for (int i = 0; i < rptPXJH.Items.Count; i++)
            {
                sqlupdatesqinfo = "update OM_PXJH_SQ set PX_FS='" + ((DropDownList)rptPXJH.Items[i].FindControl("PX_FS")).SelectedValue.Trim() + "',PX_XMMC='" + ((TextBox)rptPXJH.Items[i].FindControl("PX_XMMC")).Text.Trim() + "',PX_SJ='" + ((DropDownList)rptPXJH.Items[i].FindControl("PX_SJ")).SelectedValue.Trim() + "',PX_DD='" + ((TextBox)rptPXJH.Items[i].FindControl("PX_DD")).Text.Trim() + "',PX_ZJR='" + ((TextBox)rptPXJH.Items[i].FindControl("PX_ZJR")).Text.Trim() + "',PX_DX='" + ((TextBox)rptPXJH.Items[i].FindControl("PX_DX")).Text.Trim() + "',PX_RS='" + ((TextBox)rptPXJH.Items[i].FindControl("PX_RS")).Text.Trim() + "',PX_XS='" + ((TextBox)rptPXJH.Items[i].FindControl("PX_XS")).Text.Trim() + "',PX_FYYS='" + ((TextBox)rptPXJH.Items[i].FindControl("PX_FYYS")).Text.Trim() + "',PX_BZ='" + ((TextBox)rptPXJH.Items[i].FindControl("PX_BZ")).Text.Trim() + "' where PX_ID='" + ((HiddenField)rptPXJH.Items[i].FindControl("PX_ID")).Value.Trim() + "'";
                listsql.Add(sqlupdatesqinfo);
            }
            DBCallCommon.ExecuteTrans(listsql);
        }




        private int SaveControl()
        {
            int a = 0;
            if (asd.action == "check")
            {
                if (asd.username == asd.dt.Rows[0]["SPR1"].ToString())
                {
                    if (rblSPR1_JL.SelectedValue != "y" && rblSPR1_JL.SelectedValue != "n")
                    {
                        a = 1;
                    }
                }
                if (asd.username == asd.dt.Rows[0]["SPR2"].ToString())
                {
                    if (rblSPR2_JL.SelectedValue != "y" && rblSPR2_JL.SelectedValue != "n")
                    {
                        a = 1;
                    }
                }
                if (asd.username == asd.dt.Rows[0]["SPR3"].ToString())
                {
                    if (rblSPR3_JL.SelectedValue != "y" && rblSPR3_JL.SelectedValue != "n")
                    {
                        a = 1;
                    }
                }
            }
            return a;
        }

        private List<string> checklist()
        {
            List<string> list = new List<string>();
            string sql = "update OM_SP set ";
            if (asd.dt.Rows[0]["SPJB"].ToString() == "1")
            {
                if (asd.username == asd.dt.Rows[0]["SPR1"].ToString())
                {
                    sql += " SPR1_JL='" + rblSPR1_JL.SelectedValue + "',SPR1_SJ='" + lbSPR1_SJ.Text.Trim() + "',SPR1_JY='" + txtSPR1_JY.Text + "',";
                    if (rblSPR1_JL.SelectedValue == "n")
                    {
                        sql += " SPZT='n'";
                    }
                    else
                    {
                        sql += " SPZT='y'";
                    }
                }

            }
            else if (asd.dt.Rows[0]["SPJB"].ToString() == "2")
            {
                if (asd.username == asd.dt.Rows[0]["SPR1"].ToString())
                {
                    sql += " SPR1_JL='" + rblSPR1_JL.SelectedValue + "',SPR1_SJ='" + lbSPR1_SJ.Text.Trim() + "',SPR1_JY='" + txtSPR1_JY.Text + "',";
                    if (rblSPR1_JL.SelectedValue == "n")
                    {
                        sql += " SPZT='n'";
                    }
                    else
                    {
                        sql += " SPZT='1y'";
                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sprid = hidSPR2ID.Value.Trim();
                        sptitle = "年度培训计划汇总审批";
                        spcontent = "有年度培训计划汇总需要您审批，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                }
                else if (asd.username == asd.dt.Rows[0]["SPR2"].ToString())
                {
                    sql += " SPR2_JL='" + rblSPR2_JL.SelectedValue + "',SPR2_SJ='" + lbSPR2_SJ.Text.Trim() + "',SPR2_JY='" + txtSPR2_JY.Text + "',";
                    if (rblSPR2_JL.SelectedValue == "n")
                    {
                        sql += " SPZT='n'";
                    }
                    else
                    {
                        sql += " SPZT='y'";
                    }
                }
            }
            else if (asd.dt.Rows[0]["SPJB"].ToString() == "3")
            {
                if (asd.username == asd.dt.Rows[0]["SPR1"].ToString())
                {
                    sql += " SPR1_JL='" + rblSPR1_JL.SelectedValue + "',SPR1_SJ='" + lbSPR1_SJ.Text.Trim() + "',SPR1_JY='" + txtSPR1_JY.Text + "',";
                    if (rblSPR1_JL.SelectedValue == "n")
                    {
                        sql += " SPZT='n'";
                    }
                    else
                    {
                        sql += " SPZT='1y'";
                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sprid = hidSPR2ID.Value.Trim();
                        sptitle = "年度培训计划汇总审批";
                        spcontent = "有年度培训计划汇总需要您审批，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                }
                else if (asd.username == asd.dt.Rows[0]["SPR2"].ToString())
                {
                    sql += " SPR2_JL='" + rblSPR2_JL.SelectedValue + "',SPR2_SJ='" + lbSPR2_SJ.Text.Trim() + "',SPR2_JY='" + txtSPR2_JY.Text + "',";
                    if (rblSPR2_JL.SelectedValue == "n")
                    {
                        sql += " SPZT='n'";
                    }
                    else
                    {
                        sql += " SPZT='2y'";
                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sprid = hidSPR3ID.Value.Trim();
                        sptitle = "年度培训计划汇总审批";
                        spcontent = "有年度培训计划汇总需要您审批，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                }
                else if (asd.username == asd.dt.Rows[0]["SPR3"].ToString())
                {
                    sql += " SPR3_JL='" + rblSPR3_JL.SelectedValue + "',SPR3_SJ='" + lbSPR3_SJ.Text.Trim() + "',SPR3_JY='" + txtSPR3_JY.Text + "',";
                    if (rblSPR3_JL.SelectedValue == "n")
                    {
                        sql += " SPZT='n'";
                    }
                    else
                    {
                        sql += " SPZT='y'";
                    }
                }
            }
            sql += " where SPFATHERID='" + asd.sjid + "'";
            list.Add(sql);
            return list;
        }

        private List<string> hzlist()
        {
            List<string> list = new List<string>();
            string sql = "insert into OM_SP (SPFATHERID,SPLX,SPJB,ZDR,ZDRID,ZDR_SJ,SPZT) values (";
            sql += "'" + asd.sjid + "',";
            sql += "'NDPXJH',";
            sql += "'" + rblSPJB.SelectedValue + "',";
            sql += "'" + txtZDR.Text + "',";
            sql += "'" + hidZDRID.Value + "',";
            sql += "'" + lbZDR_SJ.Text + "',";
            sql += "'0')";
            list.Add(sql);
            foreach (RepeaterItem item in rptPXJH.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbxXuHao");
                //if (cbx.Checked)
                //{
                //    string PX_ID = ((HiddenField)item.FindControl("PX_ID")).Value;
                //    sql = "update OM_PXJH_SQ set PX_SJID1='" + asd.sjid + "' where PX_ID=" + PX_ID;
                //    list.Add(sql);
                //}
                string PX_ID = ((HiddenField)item.FindControl("PX_ID")).Value;
                sql = "update OM_PXJH_SQ set PX_SJID1='" + asd.sjid + "' where PX_ID=" + PX_ID;
                list.Add(sql);
            }
            return list;
        }

        private List<string> alterlist()
        {
            List<string> list = new List<string>();
            string sql = "delete from OM_PXJH_SQ where PX_SJID1='" + asd.sjid + "'";
            list.Add(sql);
            sql = "insert into OM_SP (SPFATHERID,SPLX,SPJB,ZDR,ZDRID,ZDR_SJ,SPZT) values (";
            sql += "'" + asd.sjid + "',";
            sql += "'NDPXJH',";
            sql += "'" + rblSPJB.SelectedValue + "',";
            sql += "'" + txtZDR.Text + "',";
            sql += "'" + hidZDRID.Value + "',";
            sql += "'" + lbZDR_SJ.Text + "',";
            sql += "'0')";
            list.Add(sql);
            foreach (RepeaterItem item in rptPXJH.Items)
            {
                CheckBox cbx = (CheckBox)item.FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    string PX_ID = ((HiddenField)item.FindControl("PX_ID")).Value;
                    sql = "update OM_PXJH_SQ set PX_SJID1='" + asd.sjid + "' where PX_ID=" + PX_ID;
                    list.Add(sql);
                }
            }
            return list;
        }

        protected void btnBack_onserverclick(object sender, EventArgs e)
        {
            Response.Redirect("OM_NDPXJH_GL.aspx");
        }
    }
}
