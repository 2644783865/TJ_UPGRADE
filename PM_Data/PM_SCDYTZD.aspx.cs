using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_SCDYTZD : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        string action = string.Empty;
        string ms_id = string.Empty;//制作明细的id
        string id = string.Empty;//通知单id
        string username = string.Empty;
        DataTable dts = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            action = Request.QueryString["action"];
            ms_id = Request.QueryString["ms_id"];
            id = Request.QueryString["id"];
            username = Session["UserName"].ToString();
            if (action != "add" && action != "read1")
            {
                string sql = "select * from PM_SCDYTZ where TZD_ID=" + id;
                dts = DBCallCommon.GetDTUsingSqlText(sql);
            }
            if (!IsPostBack)
            {
                BindData();
                PowerControl();
            }
        }
        #region 分页
        private void Pager_PageChanged(int pageNumber)//换页事件
        {
            bindrpt();
        }

        private void bindrpt()
        {
            pager_org.TableName = "View_TM_TASKDQO";
            pager_org.PrimaryKey = "MS_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "MS_ZONGXU";
            //pager_org.OrderField = "dbo.f_formatstr(MS_ZONGXU, '.')";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 20;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptZZMX, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        private string StrWhere()
        {
            string sql = "MS_PID='" + ms_id + "' ";
            return sql;
        }
        #endregion

        private void BindrptZZMX()
        {
            pager_org.TableName = "(select a.*,b.MS_ID,b.MS_TUHAO,b.MS_ZONGXU,b.MS_NAME,b.MS_GUIGE,b.MS_CAIZHI,b.MS_UNUM,b.MS_NUM,b.MS_TUWGHT,b.MS_TUTOTALWGHT,b.MS_MASHAPE,b.MS_TECHUNIT,b.MS_YONGLIANG,b.MS_MATOTALWGHT,b.MS_LEN,b.MS_WIDTH,b.MS_NOTE,b.MS_XIALIAO,b.MS_PROCESS,b.MS_KU,b.MS_ALLBEIZHU from PM_SCDYTZ_MX as a left join View_TM_TASKDQO as b on (a.MX_MS_ID=b.MS_ID and a.MX_MS_ZONGXU=b.MS_ZONGXU) where MX_FATHERID='" + hidTZD_SJID.Value + "')t";
            pager_org.PrimaryKey = "TZD_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "MS_ZONGXU";
            //pager_org.OrderField = "dbo.f_formatstr(MS_ZONGXU, '.')";
            pager_org.StrWhere = "";
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 20;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptZZMX, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        private void BindData()
        {
            if (action == "add")
            {
                hidTZD_SJID.Value = DateTime.Now.ToString("yyyy-MM-dd:HH-mm-ss-fff");
                string sql = "select MS_ID,MS_PJID,MS_ENGID,MS_ENGNAME,MS_SUBMITNM,MS_CHILDENGNAME,MS_SUBMITTM,MS_ADATE,MS_PJNAME from View_TM_MSFORALLRVW where MS_ID='" + ms_id + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                lbTZD_RWH.Text = dt.Rows[0]["MS_ENGID"].ToString();
                //engid = dt.Rows[0]["MS_ENGID"].ToString();
                lbTZD_HTMC.Text = dt.Rows[0]["MS_PJID"].ToString();
                lab_pjid.Text = dt.Rows[0]["MS_PJID"].ToString();
                lbTZD_SBMC.Text = dt.Rows[0]["MS_CHILDENGNAME"].ToString();
                lbTZD_PH.Text = dt.Rows[0]["MS_ID"].ToString();
                lbTZD_BZRQ.Text = dt.Rows[0]["MS_SUBMITTM"].ToString();
                lbTZD_BZR.Text = dt.Rows[0]["MS_SUBMITNM"].ToString();
                lbTZD_XMMC.Text = dt.Rows[0]["MS_PJNAME"].ToString();//*******

                txtTZD_ZDR.Text = username;
                lbTZD_ZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtTZD_SPR1.Text = dt.Rows[0]["MS_SUBMITNM"].ToString();
                txtTZD_SPR2.Text = "李小婷";

                bindrpt();
            }
            else if (action == "read1")
            {
                string sql = "select MS_ID,MS_PJID,MS_ENGID,MS_ENGNAME,MS_SUBMITNM,MS_CHILDENGNAME,MS_SUBMITTM,MS_ADATE,MS_PJNAME from View_TM_MSFORALLRVW where MS_ID='" + ms_id + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                lbTZD_RWH.Text = dt.Rows[0]["MS_ENGID"].ToString();
                //engid = dt.Rows[0]["MS_ENGID"].ToString();
                lbTZD_HTMC.Text = dt.Rows[0]["MS_PJID"].ToString();
                lab_pjid.Text = dt.Rows[0]["MS_PJID"].ToString();
                lbTZD_SBMC.Text = dt.Rows[0]["MS_CHILDENGNAME"].ToString();
                lbTZD_PH.Text = dt.Rows[0]["MS_ID"].ToString();
                lbTZD_BZRQ.Text = dt.Rows[0]["MS_SUBMITTM"].ToString();
                lbTZD_BZR.Text = dt.Rows[0]["MS_SUBMITNM"].ToString();
                lbTZD_XMMC.Text = dt.Rows[0]["MS_PJNAME"].ToString();//*******
                bindrpt();
            }
            else if (action == "check")
            {
                hidTZD_SJID.Value = dts.Rows[0]["TZD_SJID"].ToString();
                BindPanel(panJBXX);
                BindPanel(panZDR);
                BindPanel(panSPR1);
                BindPanel(panSPR2);
                if (username == dts.Rows[0]["TZD_SPR1"].ToString())
                {
                    lbTZD_SPR1_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (username == dts.Rows[0]["TZD_SPR2"].ToString())
                {
                    lbTZD_SPR2_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }

                BindrptZZMX();
            }
            else if (action == "read")
            {
                hidTZD_SJID.Value = dts.Rows[0]["TZD_SJID"].ToString();
                BindPanel(panJBXX);
                BindPanel(panZDR);
                BindPanel(panSPR1);
                BindPanel(panSPR2);
                BindrptZZMX();
            }
            else if (action == "alter")
            {
                hidTZD_SJID.Value = dts.Rows[0]["TZD_SJID"].ToString();
                BindPanel(panJBXX);
                BindPanel(panZDR);
                BindPanel(panSPR1);
                BindPanel(panSPR2);
                BindrptZZMX();
            }
        }

        private void BindPanel(Panel panel)
        {
            foreach (Control ctr in panel.Controls)
            {
                if (ctr is TextBox)
                {
                    TextBox txt = (TextBox)ctr;
                    if (dts.Columns.Contains(txt.ID.Substring(3)))
                    {
                        txt.Text = dts.Rows[0][txt.ID.Substring(3)].ToString();
                    }
                }
                if (ctr is Label)
                {
                    Label lb = (Label)ctr;
                    if (dts.Columns.Contains(lb.ID.Substring(2)))
                    {
                        lb.Text = dts.Rows[0][lb.ID.Substring(2)].ToString();
                    }
                }
                if (ctr is RadioButtonList)
                {
                    RadioButtonList rbl = (RadioButtonList)ctr;
                    if (dts.Columns.Contains(rbl.ID.Substring(3)))
                    {
                        rbl.SelectedValue = dts.Rows[0][rbl.ID.Substring(3)].ToString();
                    }
                }
                if (ctr is CheckBoxList)
                {
                    CheckBoxList cbxl = (CheckBoxList)ctr;
                    if (dts.Columns.Contains(cbxl.ID.Substring(4)))
                    {
                        if (dts.Rows[0][cbxl.ID.Substring(4)].ToString().Contains('|'))
                        {
                            string[] str = dts.Rows[0][cbxl.ID.Substring(4)].ToString().Split('|');
                            for (int j = 0, a = str.Length; j < a; j++)
                            {
                                for (int k = 0, b = cbxl.Items.Count; k < b; k++)
                                {
                                    if (cbxl.Items[k].Text == str[j])
                                    {
                                        cbxl.Items[k].Selected = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int k = 0, b = cbxl.Items.Count; k < b; k++)
                            {
                                if (cbxl.Items[k].Text == dts.Rows[0][cbxl.ID.Substring(4)].ToString())
                                {
                                    cbxl.Items[k].Selected = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void PowerControl()
        {
            btnSubmit.Visible = false;
            btnBack.Visible = false;
            btnSearch.Visible = false;
            panJBXX.Enabled = false;
            panSPR1.Enabled = false;
            panSPR2.Enabled = false;
            panZDR.Enabled = false;
            if (action == "add")
            {
                btnSubmit.Visible = true;
                btnBack.Visible = true;
                btnSearch.Visible = true;
                panJBXX.Enabled = true;
                panSPR1.Enabled = true;
                panSPR2.Enabled = true;
                panZDR.Enabled = true;
            }
            else if (action == "check")
            {
                btnSubmit.Visible = true;
                btnBack.Visible = true;
                if (username == dts.Rows[0]["TZD_SPR1"].ToString())
                {
                    panSPR1.Enabled = true;
                }
                if (username == dts.Rows[0]["TZD_SPR2"].ToString())
                {
                    panSPR2.Enabled = true;
                }
            }
            else if (action == "read1")
            {
                TabPanel2.Visible = false;
            }
            else if (action == "alter")
            {
                btnSubmit.Visible = true;
                btnBack.Visible = true;
                panJBXX.Enabled = true;
                panSPR1.Enabled = true;
                panSPR2.Enabled = true;
                panZDR.Enabled = true;
            }
        }

        protected void btnSubmit_onserverclick(object sender, EventArgs e)
        {
            if (action == "add"||action == "alter")
            {
                int a = 0;
                foreach (RepeaterItem item in rptZZMX.Items)
                {
                    CheckBox cbx = item.FindControl("cbxXuHao") as CheckBox;
                    if (cbx.Checked)
                    {
                        a++;
                    }
                }
                if (a == 0)
                {
                    Response.Write("<script>alert('您未选择任何项进行代用，请至少勾选一行！！！')</script>");
                    return;
                }
            }
            if (action == "add")
            {
                List<string> list = addlist();
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('addlist语句出现问题，请与管理员联系！！！')</script>");
                    return;
                }
            }
            else if (action == "alter")
            {
                List<string> list = alterlist();
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('alterlist语句出现问题，请与管理员联系！！！')</script>");
                    return;
                }
            }
            else if (action == "check")
            {
                if (username == dts.Rows[0]["TZD_SPR1"].ToString())
                {
                    if (rblTZD_SPR1_JL.SelectedValue != "1" && rblTZD_SPR1_JL.SelectedValue != "2")
                    {
                        Response.Write("<script>alert('请选择“同意”或者“不同意”后再提交！！！')</script>");
                        return;
                    }
                }
                if (username == dts.Rows[0]["TZD_SPR2"].ToString())
                {
                    if (rblTZD_SPR2_JL.SelectedValue != "1" && rblTZD_SPR2_JL.SelectedValue != "2")
                    {
                        Response.Write("<script>alert('请选择“同意”或者“不同意”后再提交！！！')</script>");
                        return;
                    }
                }
                string sql = checksql();
                try
                {
                    DBCallCommon.ExeSqlText(sql);
                }
                catch
                {
                    Response.Write("<script>alert('checksql语句出现问题，请与管理员联系！！！')</script>");
                    return;
                }
            }
            SendEmail();
            Response.Redirect("PM_SCDYTZSP.aspx");
        }

        private List<string> addlist()
        {
            List<string> list = new List<string>();
            string sql = "insert into PM_SCDYTZ (";
            sql += "TZD_SJID,";
            sql += "TZD_RWH,";
            sql += "TZD_HTMC,";
            sql += "TZD_XMMC,";
            sql += "TZD_SBMC,";
            sql += "TZD_PH,";
            sql += "TZD_BZR,";
            sql += "TZD_BZRQ,";
            sql += "TZD_ZDR,";
            sql += "TZD_ZDJY,";
            sql += "TZD_ZDSJ,";
            sql += "TZD_SPR1,";
            sql += "TZD_SPR2,";
            sql += "TZD_SPZT,";
            sql += "TZD_CLZT";
            sql += ") values (";
            sql += "'" + hidTZD_SJID.Value + "',";
            sql += "'" + lbTZD_RWH.Text + "',";
            sql += "'" + lbTZD_HTMC.Text + "',";
            sql += "'" + lbTZD_XMMC.Text + "',";
            sql += "'" + lbTZD_SBMC.Text + "',"; ;
            sql += "'" + lbTZD_PH.Text + "',";
            sql += "'" + lbTZD_BZR.Text + "',";
            sql += "'" + lbTZD_BZRQ.Text + "',";
            sql += "'" + txtTZD_ZDR.Text + "',";
            sql += "'" + txtTZD_ZDJY.Text + "',";
            sql += "'" + lbTZD_ZDSJ.Text + "',";
            sql += "'" + txtTZD_SPR1.Text + "',";
            sql += "'" + txtTZD_SPR2.Text + "',";
            sql += "'0',";
            sql += "'0')";
            list.Add(sql);
            for (int i = 0, length = rptZZMX.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptZZMX.Items[i].FindControl("cbxXuHao");
                string MS_ID = ((HiddenField)rptZZMX.Items[i].FindControl("MS_ID")).Value;
                string ZONGXU = ((Label)rptZZMX.Items[i].FindControl("MS_ZONGXU")).Text;
                string SCBZ = ((TextBox)rptZZMX.Items[i].FindControl("MS_DYTZBZ")).Text.Trim();
                if (cbx.Checked)
                {
                    string sql1 = "insert into PM_SCDYTZ_MX (";
                    sql1 += "MX_FATHERID,";
                    sql1 += "MX_MS_ID,";
                    sql1 += "MX_MS_ZONGXU,";
                    sql1 += "MS_DYTZBZ";
                    sql1 += ") values (";
                    sql1 += "'" + hidTZD_SJID.Value + "',";
                    sql1 += "'" + MS_ID + "',";
                    sql1 += "'" + ZONGXU + "',";
                    sql1 += "'" + SCBZ + "')";
                    list.Add(sql1);
                }
            }
            return list;
        }

        private List<string> alterlist()
        {
            List<string> list = new List<string>();
            string sql = string.Format("update PM_SCDYTZ set TZD_ZDJY='{0}',TZD_SPR1_JL=null,TZD_SPR1_SJ=null,TZD_SPR1_JY=null,TZD_SPR2_JL=null,TZD_SPR2_SJ=null,TZD_SPR2_JY=null,TZD_SPZT='0' where TZD_ID={1}", txtTZD_ZDJY.Text.Trim(), id);
            list.Add(sql);
            sql = string.Format("delete from PM_SCDYTZ_MX where MX_FATHERID='{0}'", hidTZD_SJID.Value);
            list.Add(sql);
            for (int i = 0, length = rptZZMX.Items.Count; i < length; i++)
            {
                CheckBox cbx = (CheckBox)rptZZMX.Items[i].FindControl("cbxXuHao");
                if (cbx.Checked)
                {
                    string MS_ID = ((HiddenField)rptZZMX.Items[i].FindControl("MS_ID")).Value;
                    string ZONGXU = ((Label)rptZZMX.Items[i].FindControl("MS_ZONGXU")).Text;
                    string SCBZ = ((TextBox)rptZZMX.Items[i].FindControl("MS_DYTZBZ")).Text.Trim();
                    string sql1 = "insert into PM_SCDYTZ_MX (";
                    sql1 += "MX_FATHERID,";
                    sql1 += "MX_MS_ID,";
                    sql1 += "MX_MS_ZONGXU,";
                    sql1 += "MS_DYTZBZ";
                    sql1 += ") values (";
                    sql1 += "'" + hidTZD_SJID.Value + "',";
                    sql1 += "'" + MS_ID + "',";
                    sql1 += "'" + ZONGXU + "',";
                    sql1 += "'" + SCBZ + "')";
                    list.Add(sql1);
                }
            }
            return list;
        }

        private string checksql()
        {
            string sql = "update PM_SCDYTZ set ";
            if (username == dts.Rows[0]["TZD_SPR1"].ToString())
            {
                sql += "TZD_SPR1_JL='" + rblTZD_SPR1_JL.SelectedValue + "',";
                sql += "TZD_SPR1_SJ='" + lbTZD_SPR1_SJ.Text + "',";
                sql += "TZD_SPR1_JY='" + txtTZD_SPR1_JY.Text.Trim() + "',";
                if (rblTZD_SPR1_JL.SelectedValue == "1")
                {
                    sql += "TZD_SPZT='1y'";
                }
                if (rblTZD_SPR1_JL.SelectedValue == "2")
                {
                    sql += "TZD_SPZT='1n'";
                }
            }
            if (username == dts.Rows[0]["TZD_SPR2"].ToString())
            {
                sql += "TZD_SPR2_JL='" + rblTZD_SPR2_JL.SelectedValue + "',";
                sql += "TZD_SPR2_SJ='" + lbTZD_SPR2_SJ.Text + "',";
                sql += "TZD_SPR2_JY='" + txtTZD_SPR2_JY.Text.Trim() + "',";
                if (rblTZD_SPR2_JL.SelectedValue == "1")
                {
                    sql += "TZD_SPZT='2y'";
                }
                if (rblTZD_SPR2_JL.SelectedValue == "2")
                {
                    sql += "TZD_SPZT='2n'";
                }
            }
            sql += " where TZD_ID=" + id;
            return sql;
        }

        private void SendEmail()
        {
            if (action == "add")
            {
                string UserID = GetUserID(txtTZD_SPR1.Text.Trim());
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(UserID), new List<string>(), new List<string>(), "生产代用通知", "您有生产代用通知“" + lbTZD_PH.Text.Trim() + "”需要审批，请登录系统,进入技术管理模块的“生产通知审批”进行查看。");
            }
            if (action == "check")
            {
                string UserID = GetUserID(txtTZD_SPR2.Text.Trim());
                if (username == dts.Rows[0]["TZD_SPR1"].ToString() && rblTZD_SPR1_JL.SelectedValue == "1")
                {
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(UserID), new List<string>(), new List<string>(), "生产代用通知", "您有生产代用通知“" + dts.Rows[0]["TZD_PH"].ToString() + "”需要审批，请登录系统,进入技术管理模块的“生产通知审批”进行查看。");
                }
            }
        }

        private string GetUserID(string UserName)
        {
            string UserID = "";
            string sql = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_NAME='" + UserName + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            UserID = dt.Rows[0][0].ToString();
            return UserID;
        }

        protected void btnBack_onserverclick(object sender, EventArgs e)
        {
            Response.Redirect("PM_SCDYTZSP.aspx");
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            string sql = "MS_PID='" + ms_id + "' ";
            List<string> listZX = new List<string>();
            if (txtZX1.Text.Trim() != "")
            {
                listZX.Add(txtZX1.Text.Trim());
            }
            if (txtZX2.Text.Trim() != "")
            {
                listZX.Add(txtZX2.Text.Trim());
            }
            if (txtZX3.Text.Trim() != "")
            {
                listZX.Add(txtZX3.Text.Trim());
            }
            if (txtZX4.Text.Trim() != "")
            {
                listZX.Add(txtZX4.Text.Trim());
            }
            if (txtZX5.Text.Trim() != "")
            {
                listZX.Add(txtZX5.Text.Trim());
            }
            if (txtZX6.Text.Trim() != "")
            {
                listZX.Add(txtZX6.Text.Trim());
            }
            if (txtZX7.Text.Trim() != "")
            {
                listZX.Add(txtZX7.Text.Trim());
            }
            if (txtZX8.Text.Trim() != "")
            {
                listZX.Add(txtZX8.Text.Trim());
            }
            if (txtZX9.Text.Trim() != "")
            {
                listZX.Add(txtZX9.Text.Trim());
            }
            if (txtZX10.Text.Trim() != "")
            {
                listZX.Add(txtZX10.Text.Trim());
            }
            if (txtZX11.Text.Trim() != "")
            {
                listZX.Add(txtZX11.Text.Trim());
            }
            if (txtZX12.Text.Trim() != "")
            {
                listZX.Add(txtZX12.Text.Trim());
            }
            if (listZX.Count > 0)
            {
                sql += " and MS_ZONGXU in (";
                for (int i = 0, length = listZX.Count; i < length; i++)
                {
                    sql += "'" + listZX[i] + "',";
                }
                sql = sql.Trim(',');
                sql += ")";
            }
            //*****************
            pager_org.TableName = "View_TM_TASKDQO";
            pager_org.PrimaryKey = "MS_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "MS_ZONGXU";
            //pager_org.OrderField = "dbo.f_formatstr(MS_ZONGXU, '.')";
            pager_org.StrWhere = sql;
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 20;
            UCPaging1.PageSize = pager_org.PageSize;
            pager_org.PageIndex = 1;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptZZMX, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }
    }
}
