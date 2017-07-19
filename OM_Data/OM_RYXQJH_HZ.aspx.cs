using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_RYXQJH_HZ : System.Web.UI.Page
    {
        string username = string.Empty;
        string action = string.Empty;
        string sjid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];
            username = Session["UserName"].ToString();
            sjid = Request.QueryString["sjid"];

            if (!IsPostBack)
            {
                if (action == "collect")
                {
                    string sql = "select * from OM_ZPJH where JH_SFTJ='y' and JH_SFHZ<>'y' and JH_LX='ND' order by JH_ZPBM,JH_GWMCID";
                    asd.dts = DBCallCommon.GetDTUsingSqlText(sql);
                }
                else if (action == "check" || action == "alter" || action == "read")
                {
                    string sql = "select * from OM_ZPJH as a left join OM_SP as b on a.JH_HZSJ=b.SPFATHERID where JH_HZSJ='" + sjid + "' and JH_LX='ND' order by JH_ZPBM,JH_GWMCID";
                    asd.dts = DBCallCommon.GetDTUsingSqlText(sql);
                    asd.hzsj = sjid;
                }
                BindData();
                PowerControl();
            }
        }

        private class asd
        {
            public static string sfbc;
            public static string hzsj;
            public static DataTable dts;
        }

        private void BindData()
        {
            rptZPJH.DataSource = asd.dts;
            rptZPJH.DataBind();
            int zrs = 0;
            for (int i = 0, length = rptZPJH.Items.Count; i < length; i++)
            {
                int JH_ZPRS = Convert.ToInt32(((Label)rptZPJH.Items[i].FindControl("JH_ZPRS")).Text);
                zrs += JH_ZPRS;
            }
            foreach (RepeaterItem item in rptZPJH.Controls)//汇总
            {
                if (item.ItemType == ListItemType.Footer)
                {
                    ((Label)item.FindControl("lbZRS")).Text = zrs.ToString();
                    break;
                }
            }
            if (action == "collect")
            {
                txtZDR.Text = username;
                lbZDR_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else if (action == "check")
            {
                BindPanel(panZDR);
                BindPanel(panSPR1);
                BindPanel(panSPR2);
                BindPanel(panSPR3);
            }
            else if (action == "alter" || action == "read")
            {
                BindPanel(panZDR);
                BindPanel(panSPR1);
                BindPanel(panSPR2);
                BindPanel(panSPR3);
            }
            MergeCells();
        }

        private void PowerControl()
        {
            if (action == "read")
            {
                btnBack.Visible = false;
                btnSave.Visible = false;
                btnSubmit.Visible = false;
                btnDelete.Visible = false;
                panSPR1.Enabled = false;
                panSPR2.Enabled = false;
                panSPR3.Enabled = false;
                panZDR.Enabled = false;
            }
            else if (action == "check")
            {
                btnDelete.Visible = false;
                btnSubmit.Visible = false;
                panSPR1.Enabled = false;
                panSPR2.Enabled = false;
                panSPR3.Enabled = false;
                panZDR.Enabled = false;
                if (username == asd.dts.Rows[0]["SPR1"].ToString())
                {
                    panSPR1.Enabled = true;
                }
                else if (username == asd.dts.Rows[0]["SPR2"].ToString())
                {
                    panSPR2.Enabled = true;
                }
                else if (username == asd.dts.Rows[0]["SPR3"].ToString())
                {
                    panSPR3.Enabled = true;
                }
            }
        }

        private void BindPanel(Panel panel)//绑定panel
        {
            //string sql = "select * from OM_ZPJH as a left join OM_SP as b on a.JH_HZSJ=b.SPFATHERID where JH_HZSJ='" + sjid + "'";
            string sql = "select * from OM_ZPJH as a left join OM_SP as b on a.JH_HZSJ=b.SPFATHERID where JH_HZSJ='" + sjid + "' and JH_LX='ND' order by JH_ZPBM,JH_GWMCID";
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

        protected void rptZPJH_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem) return;
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    string JH_ID = ((HiddenField)e.Item.FindControl("JH_ID")).Value;
            //    string sql = "select * from ";

            //    //绑定ddl
            //    string JH_GWMCID = ((HiddenField)e.Item.FindControl("JH_GWMCID")).Value;
            //    string gwid = JH_GWMCID.Substring(0,2);
            //    string sql1 = "select DEP_CODE,DEP_NAME,DEP_FATHERID from TBDS_DEPINFO where DEP_FATHERID='" + gwid + "'";
            //    DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
            //    DropDownList ddlJH_GWMC = (DropDownList)e.Item.FindControl("ddlJH_GWMC");
            //    for (int i = 0,length=dt1.Rows.Count; i < length; i++)
            //    {
            //        ddlJH_GWMC.Items.Add(new ListItem(dt1.Rows[i]["DEP_NAME"].ToString(), dt1.Rows[i]["DEP_CODE"].ToString()));
            //    }

            //    RadioButtonList rblJH_XQLY = (RadioButtonList)e.Item.FindControl("rblJH_XQLY");
            //}
        }


        private void MergeCells()//合并单元格
        {
            //for (int i = rptZPJH.Items.Count - 1; i > 0; i--)
            //{
            //    HtmlTableCell tdBM = (HtmlTableCell)rptZPJH.Items[i].FindControl("tdBM");
            //    Label lbBM = (Label)rptZPJH.Items[i].FindControl("JH_ZPBM");
            //    HtmlTableCell tdBM1 = (HtmlTableCell)rptZPJH.Items[i - 1].FindControl("tdBM");
            //    Label lbBM1 = (Label)rptZPJH.Items[i - 1].FindControl("JH_ZPBM");
            //    tdBM.RowSpan = (tdBM.RowSpan == -1) ? 1 : tdBM.RowSpan;
            //    tdBM1.RowSpan = (tdBM1.RowSpan == -1) ? 1 : tdBM1.RowSpan;
            //    if (lbBM.Text == lbBM1.Text)
            //    {
            //        tdBM.Visible = false;
            //        tdBM1.RowSpan += tdBM.RowSpan;
            //    }
            //}
        }

        protected void btnDelete_onserverclick(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("JH_ID");
            dt.Columns.Add("JH_ZPBM");
            dt.Columns.Add("JH_GWMC");
            dt.Columns.Add("JH_XQLY");
            dt.Columns.Add("JH_ZPFS");
            dt.Columns.Add("JH_ZPRS");
            dt.Columns.Add("JH_ZPGW");
            dt.Columns.Add("JH_ZPZY");
            dt.Columns.Add("JH_ZPYX");
            dt.Columns.Add("JH_ZPXL");
            dt.Columns.Add("JH_ZPXB");
            dt.Columns.Add("JH_ZPNL");
            dt.Columns.Add("JH_ZPYQ");
            dt.Columns.Add("JH_QTYQ");
            dt.Columns.Add("JH_XWDGSJ");
            dt.Columns.Add("JH_NGZDD");
            dt.Columns.Add("JH_QT");
            List<string> list = new List<string>();
            foreach (RepeaterItem item in rptZPJH.Items)
            {
                CheckBox chk = (CheckBox)item.FindControl("cbxXuHao");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((HiddenField)item.FindControl("JH_ID")).Value;
                    newRow[1] = ((Label)item.FindControl("JH_ZPBM")).Text;
                    newRow[2] = ((Label)item.FindControl("JH_GWMC")).Text;
                    newRow[3] = ((Label)item.FindControl("JH_XQLY")).Text;
                    newRow[4] = ((Label)item.FindControl("JH_ZPFS")).Text;
                    newRow[5] = ((Label)item.FindControl("JH_ZPRS")).Text;
                    newRow[6] = ((Label)item.FindControl("JH_ZPGW")).Text;
                    newRow[7] = ((Label)item.FindControl("JH_ZPZY")).Text;
                    newRow[8] = ((Label)item.FindControl("JH_ZPYX")).Text;
                    newRow[9] = ((Label)item.FindControl("JH_ZPXL")).Text;
                    newRow[10] = ((Label)item.FindControl("JH_ZPXB")).Text;
                    newRow[11] = ((Label)item.FindControl("JH_ZPNL")).Text;
                    newRow[12] = ((Label)item.FindControl("JH_ZPYQ")).Text;
                    newRow[13] = ((Label)item.FindControl("JH_QTYQ")).Text;
                    newRow[14] = ((Label)item.FindControl("JH_XWDGSJ")).Text;
                    newRow[15] = ((Label)item.FindControl("JH_NGZDD")).Text;
                    newRow[16] = ((Label)item.FindControl("JH_QT")).Text;
                    dt.Rows.Add(newRow);
                }
                else
                {
                    string JH_ID = ((HiddenField)item.FindControl("JH_ID")).Value;
                    string sql = " update OM_ZPJH set JH_SFHZ='n',JH_HZSJ=null,JH_SFTJ='n' where JH_ID=" + JH_ID;
                    list.Add(sql);
                }
            }
            try
            {
                DBCallCommon.ExecuteTrans(list);
            }
            catch
            {
                Response.Write("<script>alert('删除的语句出现问题，请与管理员联系')</script>");
                return;
            }
            rptZPJH.DataSource = dt;
            rptZPJH.DataBind();
            NoDataPanelSee();
        }

        private void NoDataPanelSee()
        {
            if (rptZPJH.Items.Count > 0)
            {
                NoDataPanel.Visible = false;
            }
            else
            {
                NoDataPanel.Visible = true;
            }
        }

        /// <summary>
        /// 上面为数据绑定部分，以下为数据处理部分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnSubmit_onserverclick(object sender, EventArgs e)
        {
            if (rblSPJB.SelectedValue != "1" && rblSPJB.SelectedValue != "2" && rblSPJB.SelectedValue != "3")
            {
                Response.Write("<script>alert('请选择了审批级别后再提交')</script>");
                return;
            }
            if (asd.sfbc != "y" && action == "collect")
            {
                Response.Write("<script>alert('请先“保存”再提交审批')</script>");
                return;
            }
            string sql = "update OM_SP set SPZT='1' where SPFATHERID='" + asd.hzsj + "' and SPLX='ZPJH'";
            try
            {
                DBCallCommon.ExeSqlText(sql);
            }
            catch
            {
                Response.Write("<script>alert('提交审批的语句出现问题，请与管理员联系')</script>");
                return;
            }
            //DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("3"), new List<string>(), new List<string>(), "招聘计划审批", "您有招聘计划的审批任务未执行，请登录系统审批！");
            Response.Redirect("OM_RYXQJH_SP.aspx");
        }

        protected void btnSave_onserverclick(object sender, EventArgs e)
        {
            if (action == "collect")
            {
                if (rblSPJB.SelectedValue != "1" && rblSPJB.SelectedValue != "2" && rblSPJB.SelectedValue != "3")
                {
                    Response.Write("<script>alert('请选择了审批级别后再保存')</script>");
                    return;
                }
                asd.sfbc = "n";
                List<string> list = collectsql();
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('collectsql语句出现问题，请与管理员联系')</script>");
                    return;
                }
                btnSave.Visible = false;
                asd.sfbc = "y";
            }
            else if (action == "check")
            {
                if (username == asd.dts.Rows[0]["SPR1"].ToString())
                {
                    if (rblSPR1_JL.SelectedValue != "y" && rblSPR1_JL.SelectedValue != "n")
                    {
                        Response.Write("<script>alert('请选择“同意”或者“不同意”后再提交')</script>");
                        return;
                    }
                }
                else if (username == asd.dts.Rows[0]["SPR2"].ToString())
                {
                    if (rblSPR2_JL.SelectedValue != "y" && rblSPR2_JL.SelectedValue != "n")
                    {
                        Response.Write("<script>alert('请选择“同意”或者“不同意”后再提交')</script>");
                        return;
                    }
                }
                else if (username == asd.dts.Rows[0]["SPR3"].ToString())
                {
                    if (rblSPR3_JL.SelectedValue != "y" && rblSPR3_JL.SelectedValue != "n")
                    {
                        Response.Write("<script>alert('请选择“同意”或者“不同意”后再提交')</script>");
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
                    Response.Write("<script>alert('checklist语句出现问题，请与管理员联系')</script>");
                    return;
                }
            }
            else if (action == "alter")
            {
                if (rblSPJB.SelectedValue != "1" && rblSPJB.SelectedValue != "2" && rblSPJB.SelectedValue != "3")
                {
                    Response.Write("<script>alert('请选择了审批级别后再提交')</script>");
                    return;
                }
                List<string> list = altersql();
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('altersql语句出现问题，请与管理员联系')</script>");
                    return;
                }
            }
            SendEmail();
            if (action == "check")
            {
                Response.Redirect("OM_RYXQJH_SP.aspx");
            }
        }

        private List<string> collectsql()
        {
            List<string> list = new List<string>();
            string hzsj = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            asd.hzsj = hzsj;
            string sql = string.Format("insert into OM_SP (SPFATHERID,SPLX,SPJB,ZDR,ZDR_SJ,ZDR_JY,SPR1,SPR1_JL,SPR1_SJ,SPR1_JY,SPR2,SPR2_JL,SPR2_SJ,SPR2_JY,SPR3,SPR3_JL,SPR3_SJ,SPR3_JY,SPZT) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')", hzsj, "ZPJH", rblSPJB.SelectedValue, txtZDR.Text.Trim(), lbZDR_SJ.Text, txtZDR_JY.Text.Trim(), txtSPR1.Text, "", "", "", txtSPR2.Text, "", "", "", txtSPR3.Text, "", "", "", "0");
            list.Add(sql);
            sql = string.Empty;
            sql = "update OM_ZPJH set JH_SFHZ='y',JH_HZSJ='" + hzsj + "',JH_HZBH= case when ((select max(JH_HZBH) from OM_ZPJH) is null) then (datename(year,getdate())+'0001')else (datename(year,getdate())+substring(convert(varchar,(select max(JH_HZBH)+1 from OM_ZPJH) ),5,4)) end where JH_ID in (";
            for (int i = 0, length = rptZPJH.Items.Count; i < length; i++)
            {
                string JH_ID = ((HiddenField)rptZPJH.Items[i].FindControl("JH_ID")).Value;
                sql += "'" + JH_ID + "',";
            }
            sql = sql.Trim(',');
            sql += ")";
            list.Add(sql);
            return list;
        }

        private List<string> checklist()
        {
            List<string> list = new List<string>();
            string sql = string.Format("update OM_SP set SPR1_JL='{0}',SPR1_SJ='{1}',SPR1_JY='{2}',SPR2_JL='{3}',SPR2_SJ='{4}',SPR2_JY='{5}',SPR3_JL='{6}',SPR3_SJ='{7}',SPR3_JY='{8}' where SPFATHERID='{9}' and SPLX='ZPJH'", rblSPR1_JL.SelectedValue, lbSPR1_SJ.Text, txtSPR1_JY.Text.Trim(), rblSPR2_JL.SelectedValue, lbSPR2_SJ.Text, txtSPR2_JY.Text.Trim(), rblSPR3_JL.SelectedValue, lbSPR3_SJ.Text, txtSPR3_JY.Text.Trim(), sjid);
            list.Add(sql);
            //审批级别确定审批状态
            if (rblSPJB.SelectedValue == "1")
            {
                if (rblSPR1_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='y' where SPFATHERID='{0}' and SPLX='ZPJH'", sjid);
                    list.Add(sql);
                }
                else if (rblSPR1_JL.SelectedValue == "n")
                {
                    sql = string.Format("update OM_SP set SPZT='n' where SPFATHERID='{0}' and SPLX='ZPJH'", sjid);
                    list.Add(sql);
                }
            }
            else if (rblSPJB.SelectedValue == "2")
            {
                if (rblSPR1_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='1y' where SPFATHERID='{0}' and SPLX='ZPJH'", sjid);
                    list.Add(sql);
                }
                if (rblSPR2_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='y' where SPFATHERID='{0}' and SPLX='ZPJH'", sjid);
                    list.Add(sql);
                }
                if (rblSPR1_JL.SelectedValue == "n" || rblSPR2_JL.SelectedValue == "n")
                {
                    sql = string.Format("update OM_SP set SPZT='n' where SPFATHERID='{0}' and SPLX='ZPJH'", sjid);
                    list.Add(sql);
                }
            }
            else if (rblSPJB.SelectedValue == "3")
            {
                if (rblSPR1_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='1y' where SPFATHERID='{0}' and SPLX='ZPJH'", sjid);
                    list.Add(sql);
                }
                if (rblSPR2_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='2y' where SPFATHERID='{0}' and SPLX='ZPJH'", sjid);
                    list.Add(sql);
                }
                if (rblSPR2_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='y' where SPFATHERID='{0}' and SPLX='ZPJH'", sjid);
                    list.Add(sql);
                }
                if (rblSPR1_JL.SelectedValue == "n" || rblSPR2_JL.SelectedValue == "n" || rblSPR3_JL.SelectedValue == "n")
                {
                    sql = string.Format("update OM_SP set SPZT='n' where SPFATHERID='{0}' and SPLX='ZPJH'", sjid);
                    list.Add(sql);
                }
            }
            return list;
        }

        private List<string> altersql()
        {
            List<string> list = new List<string>();
            string sql = string.Format("update OM_SP set SPJB='{0}',ZDR_JY='{1}',SPR1='{2}',SPR2='{3}',SPR3='{4}',SPR1_JL='',SPR2_JL='',SPR3_JL='', SPZT='0' where SPFATHERID='{5}' and SPLX='ZPJH'", rblSPJB.SelectedValue, txtZDR_JY.Text.Trim(), txtSPR1.Text.Trim(), txtSPR2.Text.Trim(), txtSPR3.Text.Trim(), sjid);
            list.Add(sql);
            for (int i = 0, length = rptZPJH.Items.Count; i < length; i++)
            {
                sql = string.Empty;
                string JH_ID = ((HiddenField)rptZPJH.Items[i].FindControl("JH_ID")).Value;
                sql = " update OM_ZPJH set JH_SFHZ='y' where JH_ID=" + JH_ID;
                list.Add(sql);
            }
            return list;
        }

        private void SendEmail()
        {
            //if (action == "check")
            //{
            //    if (username == asd.dts.Rows[0]["SPR1"].ToString() && rblSPR1_JL.SelectedValue == "y")
            //    {
            //        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("1"), new List<string>(), new List<string>(), "招聘计划审批", "您有招聘计划的审批任务未执行，请登录系统审批！");
            //    }
            //    //if (username==asd.dts.Rows[0]["SPR2"].ToString()&&rblSPR1_JL.SelectedValue=="y")
            //    //{
            //    //     DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("151"), new List<string>(),new List<string>(),"招聘计划审批","您有招聘计划的审批任务未执行，请登录系统审批！");
            //    //}
            //}
        }


        protected void btnBack_onserverclick(object sender, EventArgs e)
        {
            Response.Redirect("OM_RYXQJH_SP.aspx");
        }


    }
}
