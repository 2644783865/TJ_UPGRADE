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
using Microsoft.Office.Interop.MSProject;
using System.Collections.Generic;
using ZCZJ_DPF.CommonClass;
using System.Data.SqlClient;

namespace ZCZJ_DPF.ESM_Data
{
    public partial class EQU_GXHT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                asd.action = Request.QueryString["action"];
                asd.username = Session["UserName"].ToString();
                asd.userid = Session["UserID"].ToString();
                asd.id = Request.QueryString["id"];
                BindData();
                PowerControl();

            }
        }

        protected void SHBXX_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

            }
        }
        private void BindData()
        {
            if (asd.action == "add")
            {
                lb_HT_ZDR.Text = asd.username;
                lb_HT_ZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                string sql = "select ST_NAME,ST_ID,ST_POSITION from TBDS_STAFFINFO where ST_POSITION='1001'or ST_POSITION= '0401'or ST_POSITION= '0301'or ST_POSITION= '0102'or ST_POSITION= '0101' and ST_PD='0' order by ST_POSITION desc,ST_ID desc";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                txt_HT_SHR1.Text = dt.Rows[0][0].ToString();
                txt_HT_SHR2.Text = dt.Rows[1][0].ToString();
                txt_HT_SHR3.Text = dt.Rows[2][0].ToString();
                txt_HT_SHR4.Text = dt.Rows[3][0].ToString();
                hid_HT_SHR1ID.Value = dt.Rows[0][1].ToString();
                hid_HT_SHR2ID.Value = dt.Rows[1][1].ToString();
                hid_HT_SHR3ID.Value = dt.Rows[2][1].ToString();
                hid_HT_SHR4ID.Value = dt.Rows[3][1].ToString();
                hid_HT_ZDRID.Value = asd.userid.ToString();

                txt_HT_SHR5.Text = dt.Rows[dt.Rows.Count - 1][0].ToString();
                hid_HT_SHR5ID.Value = dt.Rows[dt.Rows.Count - 1][1].ToString();

                txt_HT_HTBH.Text = GetHTBH();
                DataTable dt1 = new DataTable();
                rptSHBXX.DataSource = dt1;
                rptSHBXX.DataBind();
            }
            if (asd.action == "alter")
            {
                string sql = "select * from EQU_GXHT where HT_HTBH='" + asd.id + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                PanelDic.BindPanel(panGXHT, dt);
                PanelDic.BindPanel(panSP, dt);
                string sql1 = "select EQU_ID,EQU_Type,EQU_Name,EQU_Unit,EQU_UPrice,EQU_Num,EQU_TMoney,EQU_Note from EQU_GX_Detail where EQU_FATHERID='" + asd.id + "' order by EQU_ID";
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
                rptSHBXX.DataSource = dt1;
                rptSHBXX.DataBind();
            }
            if (asd.action == "read")
            {
                string sql = "select * from EQU_GXHT where HT_HTBH='" + asd.id + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                PanelDic.BindPanel(panGXHT, dt);
                PanelDic.BindPanel(panSP, dt);
                string sql1 = "select EQU_ID,EQU_Type,EQU_Name,EQU_Unit,EQU_UPrice,EQU_Num,EQU_TMoney,EQU_Note from EQU_GX_Detail where EQU_FATHERID='" + asd.id + "' order by EQU_ID";
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
                rptSHBXX.DataSource = dt1;
                rptSHBXX.DataBind();
            }
            if (asd.action == "check")
            {
                string sql = "select * from EQU_GXHT where HT_HTBH='" + asd.id + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                PanelDic.BindPanel(panGXHT, dt);
                PanelDic.BindPanel(panSP, dt);
                if (asd.userid == dt.Rows[0]["HT_SHR1ID"].ToString())
                {
                    lb_HT_SHR1_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (asd.userid == dt.Rows[0]["HT_SHR2ID"].ToString())
                {
                    lb_HT_SHR2_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (asd.userid == dt.Rows[0]["HT_SHR3ID"].ToString())
                {
                    lb_HT_SHR3_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (asd.userid == dt.Rows[0]["HT_SHR4ID"].ToString())
                {
                    lb_HT_SHR4_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (asd.userid == dt.Rows[0]["HT_SHR5ID"].ToString())
                {
                    lb_HT_SHR5_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                string sql1 = "select EQU_ID,EQU_Type,EQU_Name,EQU_Unit,EQU_UPrice,EQU_Num,EQU_TMoney,EQU_Note from EQU_GX_Detail where EQU_FATHERID='" + asd.id + "' order by EQU_ID";
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
                rptSHBXX.DataSource = dt1;
                rptSHBXX.DataBind();
            }
        }
        private void PowerControl()
        {
            panGXHT.Enabled = false;
            btnSave.Visible = false;
            btnSubmit.Visible = false;
            btnAdd.Visible = false;
            btnBack.Visible = false;
            btnDelete.Visible = false;
            lbNum.Visible = false;
            txtNum.Visible = false;
            tb.Enabled = false;
            tb0.Enabled = false;
            tb1.Enabled = false;
            tb2.Enabled = false;
            tb3.Enabled = false;
            tb4.Enabled = false;
            tb5.Enabled = false;
            if (asd.action == "add" || asd.action == "alter")
            {
                panGXHT.Enabled = true;
                btnSave.Visible = true;
                btnAdd.Visible = true;
                btnBack.Visible = true;
                btnDelete.Visible = true;
                lbNum.Visible = true;
                txtNum.Visible = true;
                tb.Enabled = true;
                tb0.Enabled = true;
                tb1.Enabled = true;
                tb2.Enabled = true;
                tb3.Enabled = true;
                tb4.Enabled = true;
                tb5.Enabled = true;
            }
            if (asd.action == "check")
            {
                string sql = "select * from EQU_GXHT where HT_HTBH='" + asd.id + "'";
                DataTable dts = DBCallCommon.GetDTUsingSqlText(sql);
                btnSave.Visible = true;
                btnBack.Visible = true;
                if (dts.Rows[0]["HT_SPLX"].ToString() != "4")
                {
                    if (asd.userid == dts.Rows[0]["HT_SHR1ID"].ToString())
                    {
                        tb1.Enabled = true;
                    }
                    else if (asd.userid == dts.Rows[0]["HT_SHR2ID"].ToString())
                    {
                        tb2.Enabled = true;
                    }
                    else if (asd.userid == dts.Rows[0]["HT_SHR3ID"].ToString())
                    {
                        tb3.Enabled = true;
                    }
                }
                else
                {
                    if (asd.userid == dts.Rows[0]["HT_SHR1ID"].ToString())
                    {
                        tb1.Enabled = true;
                    }
                    else if (asd.userid == dts.Rows[0]["HT_SHR2ID"].ToString())
                    {
                        tb2.Enabled = true;
                    }
                    else if (asd.userid == dts.Rows[0]["HT_SHR3ID"].ToString())
                    {
                        tb3.Enabled = true;
                    }
                    if (asd.userid == dts.Rows[0]["HT_SHR4ID"].ToString())
                    {
                        tb4.Enabled = true;
                    }
                    else if (asd.userid == dts.Rows[0]["HT_SHR5ID"].ToString())
                    {
                        tb5.Enabled = true;
                    }
                }
            }
        }
        private class asd
        {
            public static string action;
            public static string username;
            public static string userid;
            public static string id;
        }
        #region 增加、删除行
        protected void btnAdd_OnClick(object sender, EventArgs e) //增加行的函数
        {
            int a = 0;
            if (int.TryParse(txtNum.Text, out a))
            {
                CreateNewRow(a);
            }
            else
            {
                Response.Write("<script>alert('行数未填写，默认添加1行！')</script>");
                txtNum.Text = "1";
                CreateNewRow(1);
            }
        }

        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            rptSHBXX.DataSource = dt;
            rptSHBXX.DataBind();
            //InitVar();
        }

        private DataTable GetDataTable() //临时表
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EQU_ID");
            dt.Columns.Add("EQU_Type");
            dt.Columns.Add("EQU_Name");
            dt.Columns.Add("EQU_Unit");
            dt.Columns.Add("EQU_Num");
            dt.Columns.Add("EQU_UPrice");
            dt.Columns.Add("EQU_TMoney");
            dt.Columns.Add("EQU_Note");
            foreach (RepeaterItem retItem in rptSHBXX.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((HiddenField)retItem.FindControl("hid_EQU_ID")).Value;
                //for (int i = 1; i < 8; i++)
                //{
                //    newRow[i] = ((TextBox)retItem.FindControl("txt" + i)).Text;
                //}
                //dt.Rows.Add(newRow);
                //DataRow newRow = dt.NewRow();
                //newRow[0] = ((HiddenField)retItem.FindControl("hid_EQU_ID")).Value;
                newRow[1] = ((TextBox)retItem.FindControl("txt_EQU_Type")).Text;
                newRow[2] = ((TextBox)retItem.FindControl("txt_EQU_Name")).Text;
                newRow[3] = ((TextBox)retItem.FindControl("txt_EQU_Unit")).Text;
                newRow[4] = ((TextBox)retItem.FindControl("txt_EQU_Num")).Text;
                newRow[5] = ((TextBox)retItem.FindControl("txt_EQU_UPrice")).Text;
                newRow[6] = ((TextBox)retItem.FindControl("txt_EQU_TMoney")).Text;
                newRow[7] = ((TextBox)retItem.FindControl("txt_EQU_Note")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)//删除一行
        {
            int s = rptSHBXX.Items.Count;
            DataTable dt = new DataTable();
            dt.Columns.Add("EQU_ID");
            dt.Columns.Add("EQU_Type");
            dt.Columns.Add("EQU_Name");
            dt.Columns.Add("EQU_Unit");
            dt.Columns.Add("EQU_Num");
            dt.Columns.Add("EQU_UPrice");
            dt.Columns.Add("EQU_TMoney");
            dt.Columns.Add("EQU_Note");
            foreach (RepeaterItem retItem in rptSHBXX.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("shbXuHao");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((HiddenField)retItem.FindControl("hid_EQU_ID")).Value;
                    newRow[1] = ((TextBox)retItem.FindControl("txt_EQU_Type")).Text;
                    newRow[2] = ((TextBox)retItem.FindControl("txt_EQU_Name")).Text;
                    newRow[3] = ((TextBox)retItem.FindControl("txt_EQU_Unit")).Text;
                    newRow[4] = ((TextBox)retItem.FindControl("txt_EQU_Num")).Text;
                    newRow[5] = ((TextBox)retItem.FindControl("txt_EQU_UPrice")).Text;
                    newRow[6] = ((TextBox)retItem.FindControl("txt_EQU_TMoney")).Text;
                    newRow[7] = ((TextBox)retItem.FindControl("txt_EQU_Note")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            if (dt.Rows.Count == s)
            {
                Response.Write("<script>alert('请勾选要删除的行序号！')</script>");
                this.rptSHBXX.DataSource = dt;
                this.rptSHBXX.DataBind();
            }
            else
            {
                this.rptSHBXX.DataSource = dt;
                this.rptSHBXX.DataBind();
                Response.Write("<script>alert('请务必点击任一非空的合计金额，修正总金额！')</script>");
            }
            //for (int j = 0; j < rptSHBXX.Items.Count; j++)
            //{
            //    TextBox s = (TextBox)rptSHBXX.Items[j].FindControl("txt_EQU_TMoney");
            //    s.Focus();
            //}
        }
        #endregion


        private string GetHTBH()
        {
            string htbh;
            string sql = "select max(HT_HTBH) from EQU_GXHT ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() == "")
            {
                htbh = "ZCZJSB-" + DateTime.Now.Year.ToString() + "-001";
            }
            else
            {
                string[] a = dt.Rows[0][0].ToString().Split('-');
                int b = CommonFun.ComTryInt(a[2]) + 1;
                if (a[1] == DateTime.Now.Year.ToString())
                {
                    htbh = a[0] + "-" + a[1] + "-" + b.ToString().PadLeft(3, '0');
                }
                else
                {
                    htbh = a[0] + "-" + DateTime.Now.Year.ToString() + "001";
                }
            }
            return htbh;
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)//********************提交*******************
        {
            string sql = "update EQU_GXHT set HT_SPZT='1' where HT_HTBH='" + txt_HT_HTBH.Text.Trim() + "'";
            try
            {
                DBCallCommon.ExeSqlText(sql);
                string _emailto = DBCallCommon.GetEmailAddressByUserID(hid_HT_SHR1ID.Value);
                string _body = "设备购销合同审批任务:"
                      + "\r\n合同编号：" + txt_HT_HTBH.Text.Trim()
                      + "\r\n制单人：" + lb_HT_ZDR.Text.Trim()
                      + "\r\n制单日期：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                string _subject = "您有新的【设备购销合同】需要审批，请及时处理:" + txt_HT_HTBH.Text.Trim();
                DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
            }
            catch
            {
                Response.Write("<script>alert('提交审批时数据操作错误，请与管理员联系！')</script>");
                return;
            }
            Response.Redirect("EQU_GXHT_GL.aspx");
        }

        protected void btnSave_OnClick(object sender, EventArgs e)//********************保存*******************
        {
            int a = SubmitControl();
            if (txt_HT_HTZJ.ToString().Trim() == "")
            {
                Response.Write("<script>alert('总金额为空，请双击击任一合计金额修改！！！')</script>");
                return;
            }
            if (txt_HT_QDSJ.ToString().Trim() == "")
            {
                Response.Write("<script>alert('请填写合同签订时间！！！')</script>");
                return;
            }
            if (a == 1)
            {
                Response.Write("<script>alert('您还未选择审批人，请先选择审批人后再提交！！！')</script>");
                return;
            }
            if (a == 2)
            {
                Response.Write("<script>alert('请选择“同意”或“不同意”后再提交！！！')</script>");
                return;
            }
            if (asd.action == "add")
            {
                txt_HT_HTBH.Text = GetHTBH();
                if (rptSHBXX.Items.Count > 0)
                {
                    List<string> list = addlist();
                    if (list != null)
                    {
                        try
                        {
                            DBCallCommon.ExecuteTrans(list);
                            btnSave.Visible = false;
                            btnSubmit.Visible = true;
                        }
                        catch
                        {
                            Response.Write("<script>alert('addlist数据操作错误，请与管理员联系！')</script>");
                            return;
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('请输入合计金额！')</script>");
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('产品表中没有数据！')</script>");
                    txtNum.Text = "1";
                    return;
                }
                btnSubmit.Visible = true;
            }
            if (asd.action == "alter")
            {
                List<string> list = alterlist();
                if (list != null)
                {
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        btnSave.Visible = false;
                        btnSubmit.Visible = true;
                    }
                    catch
                    {
                        Response.Write("<script>alert('alterlist数据操作错误，请与管理员联系！')</script>");
                        return;
                    }
                }
                else
                {
                    Response.Write("<script>alert('请输入合计金额！')</script>");
                    return;
                }
            }
            if (asd.action == "check")
            {
                List<string> list = checklist();
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                    Response.Redirect("EQU_GXHT_GL.aspx");
                }
                catch
                {
                    Response.Write("<script>alert('checklist数据操作错误，请与管理员联系！')</script>");
                    return;
                }
            }
        }
        private int SubmitControl()//控制能够提交的条件
        {
            int a = 0;
            if (asd.action == "add" || asd.action == "alter")
            {
                //string rbl_ = rbl_HT_SPLX.SelectedValue;
                //if (rbl_ != "1" && rbl_ != "2" && rbl_ != "3" && rbl_ != "4")
                //{
                //    a = 1;
                //}
                if (txt_HT_SHR1.Text.Trim() == "" || txt_HT_SHR2.Text.Trim() == "" || txt_HT_SHR3.Text.Trim() == "" || txt_HT_SHR4.Text.Trim() == "" || txt_HT_SHR5.Text.Trim() == "")
                {
                    a = 1;
                }
            }
            if (asd.action == "check")
            {
                string sql = "select * from EQU_GXHT where HT_HTBH='" + asd.id + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                DataRow dr = dt.Rows[0];
                if (rbl_HT_SPLX.SelectedValue == "1")
                {
                    if (dr["HT_SHR1ID"].ToString() == asd.userid)
                    {
                        if (rbl_HT_SHR1_JL.SelectedValue != "y" && rbl_HT_SHR1_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                }
                if (rbl_HT_SPLX.SelectedValue == "2")
                {
                    if (dr["HT_SHR1ID"].ToString() == asd.userid)
                    {
                        if (rbl_HT_SHR1_JL.SelectedValue != "y" && rbl_HT_SHR1_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                    else if (dr["HT_SHR2ID"].ToString() == asd.userid)
                    {
                        if (rbl_HT_SHR2_JL.SelectedValue != "y" && rbl_HT_SHR2_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                }
                if (rbl_HT_SPLX.SelectedValue == "3")
                {
                    if (dr["HT_SHR1ID"].ToString() == asd.userid)
                    {
                        if (rbl_HT_SHR1_JL.SelectedValue != "y" && rbl_HT_SHR1_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                    else if (dr["HT_SHR2ID"].ToString() == asd.userid)
                    {
                        if (rbl_HT_SHR2_JL.SelectedValue != "y" && rbl_HT_SHR2_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                    else if (dr["HT_SHR3ID"].ToString() == asd.userid)
                    {
                        if (rbl_HT_SHR3_JL.SelectedValue != "y" && rbl_HT_SHR3_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                }
                //if (rbl_HT_SPLX.SelectedValue == "4")
                //{
                if (dr["HT_SHR1ID"].ToString() == asd.userid)
                {
                    if (rbl_HT_SHR1_JL.SelectedValue != "y" && rbl_HT_SHR1_JL.SelectedValue != "n")
                    {
                        a = 2;
                    }
                } if (dr["HT_SHR2ID"].ToString() == asd.userid)
                {
                    if (rbl_HT_SHR2_JL.SelectedValue != "y" && rbl_HT_SHR2_JL.SelectedValue != "n")
                    {
                        a = 2;
                    }
                } if (dr["HT_SHR3ID"].ToString() == asd.userid)
                {
                    if (rbl_HT_SHR3_JL.SelectedValue != "y" && rbl_HT_SHR3_JL.SelectedValue != "n")
                    {
                        a = 2;
                    }
                } if (dr["HT_SHR4ID"].ToString() == asd.userid)
                {
                    if (rbl_HT_SHR4_JL.SelectedValue != "y" && rbl_HT_SHR4_JL.SelectedValue != "n")
                    {
                        a = 2;
                    }
                } if (dr["HT_SHR5ID"].ToString() == asd.userid)
                {
                    if (rbl_HT_SHR5_JL.SelectedValue != "y" && rbl_HT_SHR5_JL.SelectedValue != "n")
                    {
                        a = 2;
                    }
                }
                //}
            }
            return a;
        }
        private List<string> addlist()
        {
            List<string> list = new List<string>();
            Dictionary<string, string> dic1 = PanelDic.DicPan(panGXHT, "EQU_GXHT", new Dictionary<string, string>());
            Dictionary<string, string> dic2 = PanelDic.DicPan(panSP, "EQU_GXHT", new Dictionary<string, string>());
            string key = "";
            string value = "";
            foreach (KeyValuePair<string, string> pair in dic1)
            {
                key += pair.Key.ToString() + ",";
                value += "'" + pair.Value.ToString() + "',";
            }
            foreach (KeyValuePair<string, string> pair in dic2)
            {
                key += pair.Key.ToString() + ",";
                value += "'" + pair.Value.ToString() + "',";
            }
            key += "HT_SPZT";//审批总状态0-初始化，1-待审批,1y,2.1y,2.2y,2y,3y-审批中,3-已通过,4-已驳回，
            value += "'0'";
            string sql = "insert into EQU_GXHT (" + key + ") values (" + value + ")";
            list.Add(sql);
            for (int j = 0; j < rptSHBXX.Items.Count; j++)
            {
                if (((TextBox)rptSHBXX.Items[j].FindControl("txt_EQU_TMoney")).Text.Trim() != "")
                {
                    TextBox txt1 = (TextBox)rptSHBXX.Items[j].FindControl("txt_EQU_Type");
                    TextBox txt2 = (TextBox)rptSHBXX.Items[j].FindControl("txt_EQU_Name");
                    TextBox txt3 = (TextBox)rptSHBXX.Items[j].FindControl("txt_EQU_Unit");
                    TextBox txt4 = (TextBox)rptSHBXX.Items[j].FindControl("txt_EQU_Num");
                    TextBox txt5 = (TextBox)rptSHBXX.Items[j].FindControl("txt_EQU_UPrice");
                    TextBox txt6 = (TextBox)rptSHBXX.Items[j].FindControl("txt_EQU_TMoney");
                    TextBox txt7 = (TextBox)rptSHBXX.Items[j].FindControl("txt_EQU_Note");
                    string sqlText1 = "insert into EQU_GX_Detail(EQU_FATHERID,EQU_Type,EQU_Name,EQU_Unit,EQU_Num,EQU_UPrice,EQU_TMoney,EQU_Note) values ('" + txt_HT_HTBH.Text + "','" + txt1.Text.Trim() + "' ,'" + txt2.Text.Trim() + "' ,'" + txt3.Text.Trim() + "' ,'" + txt4.Text.Trim() + "' ,'" + txt5.Text.Trim() + "' ,'" + txt6.Text.Trim() + "' ,'" + txt7.Text.Trim() + "')";
                    list.Add(sqlText1);
                }
                else
                {
                    list = null;
                }
            }
            return list;
        }

        private List<string> alterlist()
        {
            List<string> list = new List<string>();
            Dictionary<string, string> dic1 = PanelDic.DicPan(panGXHT, "EQU_GXHT", new Dictionary<string, string>());
            Dictionary<string, string> dic2 = PanelDic.DicPan(panSP, "EQU_GXHT", new Dictionary<string, string>());
            string sql = "update EQU_GXHT set ";
            foreach (KeyValuePair<string, string> pair in dic1)
            {
                sql += pair.Key + "='" + pair.Value + "',";
            }
            foreach (KeyValuePair<string, string> pair in dic2)
            {
                sql += pair.Key + "='" + pair.Value + "',";
            }
            sql = sql.Trim(',') + " where HT_HTBH='" + asd.id + "'";
            list.Add(sql);

            string sqlText = "delete from EQU_GX_Detail where EQU_FATHERID='" + txt_HT_HTBH.Text.Trim() + "' ";
            list.Add(sqlText);

            for (int j = 0; j < rptSHBXX.Items.Count; j++)
            {
                if (((TextBox)rptSHBXX.Items[j].FindControl("txt_EQU_TMoney")).Text.Trim() != "")
                {
                    TextBox txt1 = (TextBox)rptSHBXX.Items[j].FindControl("txt_EQU_Type");
                    TextBox txt2 = (TextBox)rptSHBXX.Items[j].FindControl("txt_EQU_Name");
                    TextBox txt3 = (TextBox)rptSHBXX.Items[j].FindControl("txt_EQU_Unit");
                    TextBox txt4 = (TextBox)rptSHBXX.Items[j].FindControl("txt_EQU_Num");
                    TextBox txt5 = (TextBox)rptSHBXX.Items[j].FindControl("txt_EQU_UPrice");
                    TextBox txt6 = (TextBox)rptSHBXX.Items[j].FindControl("txt_EQU_TMoney");
                    TextBox txt7 = (TextBox)rptSHBXX.Items[j].FindControl("txt_EQU_Note");
                    string sqlText1 = "insert into EQU_GX_Detail(EQU_FATHERID,EQU_Type,EQU_Name,EQU_Unit,EQU_Num,EQU_UPrice,EQU_TMoney,EQU_Note) values ('" + txt_HT_HTBH.Text + "','" + txt1.Text.Trim() + "' ,'" + txt2.Text.Trim() + "' ,'" + txt3.Text.Trim() + "' ,'" + txt4.Text.Trim() + "' ,'" + txt5.Text.Trim() + "' ,'" + txt6.Text.Trim() + "' ,'" + txt7.Text.Trim() + "')";
                    list.Add(sqlText1);
                }
                else
                {
                    list = null;
                }
            }
            return list;
        }
        private List<string> checklist()
        {
            Dictionary<string, string> dic = PanelDic.DicPan(panSP, "EQU_GXHT", new Dictionary<string, string>());
            List<string> list = new List<string>();
            string sql = "";
            sql = "update EQU_GXHT set ";
            foreach (KeyValuePair<string, string> pair in dic)
            {
                sql += pair.Key + "='" + pair.Value + "',";
            }
            sql = sql.Trim(',');
            sql += " where HT_HTBH='" + asd.id + "'";
            list.Add(sql);
            if (rbl_HT_SPLX.SelectedValue == "1")
            {
                if (rbl_HT_SHR1_JL.SelectedValue == "y")
                {
                    sql = "update EQU_GXHT set HT_SPZT='3' where HT_HTBH='" + asd.id + "'";
                    list.Add(sql);
                }
            }
            if (rbl_HT_SPLX.SelectedValue == "2")
            {
                if (rbl_HT_SHR1_JL.SelectedValue == "y")
                {
                    sql = "update EQU_GXHT set HT_SPZT='1y' where HT_HTBH='" + asd.id + "'";
                    list.Add(sql);
                }
                if (rbl_HT_SHR2_JL.SelectedValue == "y")
                {
                    sql = "update EQU_GXHT set HT_SPZT='3' where HT_HTBH='" + asd.id + "'";
                    list.Add(sql);
                }
            }
            if (rbl_HT_SPLX.SelectedValue == "3")
            {
                if (rbl_HT_SHR1_JL.SelectedValue == "y")
                {
                    sql = "update EQU_GXHT set HT_SPZT='1y' where HT_HTBH='" + asd.id + "'";
                    list.Add(sql);
                }
                if (rbl_HT_SHR2_JL.SelectedValue == "y")
                {
                    sql = "update EQU_GXHT set HT_SPZT='2y' where HT_HTBH='" + asd.id + "'";
                    list.Add(sql);
                }
                if (rbl_HT_SHR3_JL.SelectedValue == "y")
                {
                    sql = "update EQU_GXHT set HT_SPZT='3' where HT_HTBH='" + asd.id + "'";
                    list.Add(sql);
                }
            }
            //else if (rbl_HT_SPLX.SelectedValue == "4")
            //{
            if (rbl_HT_SHR1_JL.SelectedValue == "y")
            {
                sql = "update EQU_GXHT set HT_SPZT='1y' where HT_HTBH='" + asd.id + "'";
                list.Add(sql);
                if (asd.userid == hid_HT_SHR1ID.Value)
                {
                    string _emailto = DBCallCommon.GetEmailAddressByUserID(hid_HT_SHR2ID.Value);
                    string _body = "设备购销合同审批任务:"
                          + "\r\n合同编号：" + txt_HT_HTBH.Text.Trim()
                          + "\r\n制单人：" + lb_HT_ZDR.Text.Trim()
                          + "\r\n制单日期：" + lb_HT_ZDSJ.Text.Trim();

                    string _subject = "您有新的【设备购销合同】需要审批，请及时处理:" + txt_HT_HTBH.Text.Trim();
                    DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                    _emailto = DBCallCommon.GetEmailAddressByUserID(hid_HT_SHR3ID.Value);
                    DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                }
            }
            if (rbl_HT_SHR2_JL.SelectedValue == "y" && rbl_HT_SHR3_JL.SelectedValue == "y")
            {
                sql = "update EQU_GXHT set HT_SPZT='2y' where HT_HTBH='" + asd.id + "'";
                list.Add(sql);
                if (asd.userid == hid_HT_SHR2ID.Value || asd.userid == hid_HT_SHR3ID.Value)
                {
                    string _emailto = DBCallCommon.GetEmailAddressByUserID(hid_HT_SHR4ID.Value);
                    string _body = "设备购销合同审批任务:"
                          + "\r\n合同编号：" + txt_HT_HTBH.Text.Trim()
                          + "\r\n制单人：" + lb_HT_ZDR.Text.Trim()
                          + "\r\n制单日期：" + lb_HT_ZDSJ.Text.Trim();

                    string _subject = "您有新的【设备购销合同】需要审批，请及时处理:" + txt_HT_HTBH.Text.Trim();
                    DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                }
            }
            if (rbl_HT_SHR2_JL.SelectedValue == "y" && rbl_HT_SHR3_JL.SelectedValue == "")
            {
                sql = "update EQU_GXHT set HT_SPZT='2.1y' where HT_HTBH='" + asd.id + "'";
                list.Add(sql);
            }
            if (rbl_HT_SHR2_JL.SelectedValue == "" && rbl_HT_SHR3_JL.SelectedValue == "y")
            {
                sql = "update EQU_GXHT set HT_SPZT='2.2y' where HT_HTBH='" + asd.id + "'";
                list.Add(sql);
            }
            if (rbl_HT_SHR4_JL.SelectedValue == "y")
            {
                sql = "update EQU_GXHT set HT_SPZT='3y' where HT_HTBH='" + asd.id + "'";
                list.Add(sql);
                if (asd.userid == hid_HT_SHR4ID.Value)
                {
                    string _emailto = DBCallCommon.GetEmailAddressByUserID(hid_HT_SHR5ID.Value);
                    string _body = "设备购销合同审批任务:"
                          + "\r\n合同编号：" + txt_HT_HTBH.Text.Trim()
                          + "\r\n制单人：" + lb_HT_ZDR.Text.Trim()
                          + "\r\n制单日期：" + lb_HT_ZDSJ.Text.Trim();

                    string _subject = "您有新的【设备购销合同】需要审批，请及时处理:" + txt_HT_HTBH.Text.Trim();
                    DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                }
            }
            if (rbl_HT_SHR5_JL.SelectedValue == "y")
            {
                sql = "update EQU_GXHT set HT_SPZT='3' where HT_HTBH='" + asd.id + "'";
                list.Add(sql);
            }
            //}
            if (rbl_HT_SHR1_JL.SelectedValue == "n" || rbl_HT_SHR2_JL.SelectedValue == "n" || rbl_HT_SHR3_JL.SelectedValue == "n" || rbl_HT_SHR4_JL.SelectedValue == "n" || rbl_HT_SHR5_JL.SelectedValue == "n")
            {
                sql = "update EQU_GXHT set HT_SPZT='4' where HT_HTBH='" + asd.id + "'";
                list.Add(sql);
            }
            return list;
        }
        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("EQU_GXHT_GL.aspx");
        }

        // protected void txtName_TextChanged(object sender, EventArgs e)
        // {
        //  txt_HT_QYSJ1.Text = txt_HT_QYSJ.Text;
        //}
    }
}
