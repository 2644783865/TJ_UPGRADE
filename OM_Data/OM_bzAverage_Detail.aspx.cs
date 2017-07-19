using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_bzAverage_Detail : System.Web.UI.Page
    {
        string action = string.Empty;
        string key = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
                action = Request.QueryString["action"];
            if (Request.QueryString["key"] != null)
                key = Request.QueryString["key"];

            if (!IsPostBack)
            {
                hidAction.Value = action;
                if (action == "add")
                {

                    DataTable dt = new DataTable();
                    dt.Columns.Add("BZ");
                    dt.Columns.Add("JXGZ");
                    dt.Columns.Add("RENSHU");
                    dt.Columns.Add("PJJXGZ");
                    dt.Columns.Add("Note");
                    DataRow row1 = dt.NewRow();
                    DataRow row2 = dt.NewRow();
                    DataRow row3 = dt.NewRow();
                    DataRow row4 = dt.NewRow();
                    DataRow row5 = dt.NewRow();
                    DataRow row6 = dt.NewRow();
                    dt.Rows.Add(row1);
                    dt.Rows.Add(row2);
                    dt.Rows.Add(row3);
                    dt.Rows.Add(row4);
                    dt.Rows.Add(row5);
                    dt.Rows.Add(row6);
                    this.Det_Repeater.DataSource = dt;
                    this.Det_Repeater.DataBind();

                }
                else
                {

                    ShowData();
                }
                InitP();
                ContralEnable();
            }
        }
        //0未提交 1审批中 2通过 3驳回
        private void ContralEnable()
        {
            if (hidAction.Value == "add")
            {
                NoDataPanel.Visible = true;
                btnAudit.Visible = false;
                rblResult1.Enabled = false;
                first_opinion.Enabled = false;
                rblResult2.Enabled = false;
                first_opinion.Enabled = false;
            }
            else
            {
                tr_foot.Visible = true;


                if (hidAction.Value == "view")
                {
                    hlSelect1.Visible = false;
                    hlSelect2.Visible = false;
                    btnAudit.Visible = false;
                    btnsubmit.Visible = false;
                    Panel0.Enabled = false;
                    Panel1.Enabled = false;
                    Panel2.Enabled = false;
                    Panel3.Enabled = false;

                }
                else if (hidAction.Value == "edit")
                {
                    hlSelect1.Visible = true;
                    Panel0.Enabled = true;
                    btnAudit.Visible = false;
                }
                else if (hidAction.Value == "audit")
                {
                    hlSelect1.Visible = false;
                    hlSelect2.Visible = false;
                    btnsubmit.Visible = false;
                    btnAudit.Visible = true;
                    Panel0.Enabled = false;
                    Panel2.Enabled = false;
                    if (hidState.Value == "1")
                    {
                        Panel3.Enabled = false;

                    }
                    else if (hidState.Value == "2")
                    {
                        Panel2.Enabled = false;
                    }

                }
            }

        }

        private void ShowData()
        {

            string sql = "select * from TBDS_BZAVERAGE_DETAIL where Context='" + key + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            Det_Repeater.DataSource = dt;
            Det_Repeater.DataBind();


        }



        private void InitP()
        {
            if (action == "add")
            {
                lb1.Text = Session["UserName"].ToString();

            }
            else
            {

                //Year, Month, State, SPRID, SPRNM, ZDRID, ZDRNM, RESULTA, ZDTIME, TIMEA, OPTIONA, MONEY, JXZong, PeoNum, ConText
                string sql = "select * from TBDS_BZAVERAGE  where Context='" + key + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    txt_first.Text = dt.Rows[0]["SPRNM"].ToString();
                    firstid.Value = dt.Rows[0]["SPRID"].ToString();
                    first_time.Text = dt.Rows[0]["TIMEA"].ToString();
                    first_opinion.Text = dt.Rows[0]["OPTIONA"].ToString();
                    rblResult1.SelectedValue = dt.Rows[0]["RESULTA"].ToString();
                    txtTime.Text = dt.Rows[0]["ZDTIME"].ToString();
                    lblJXZong.Text = dt.Rows[0]["JXZong"].ToString();
                    txtJXFen.Text = dt.Rows[0]["MONEY"].ToString();
                    txtPeoNum.Text = dt.Rows[0]["MONEY"].ToString();
                    hidId.Value = dt.Rows[0]["Id"].ToString();
                    hidConext.Value = key;
                    txtPeoNum.Text = dt.Rows[0]["PeoNum"].ToString();
                    btnAudit.Visible = false;
                    lb1.Text = dt.Rows[0]["ZDRNM"].ToString();
                    hidState.Value = dt.Rows[0]["State"].ToString();

                    txtKhNianYue.Text = dt.Rows[0]["Year"].ToString() + "-" + dt.Rows[0]["Month"].ToString();


                    txt_second.Text = dt.Rows[0]["SPRNMB"].ToString();
                    secondId.Value = dt.Rows[0]["SPRIDB"].ToString();
                    second_time.Text = dt.Rows[0]["TIMEB"].ToString();
                    second_opinion.Text = dt.Rows[0]["OPTIONB"].ToString();
                    rblResult2.SelectedValue = dt.Rows[0]["RESULTB"].ToString();


                }

            }
        }




        #region 增加删除行

        protected void btnadd_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (int.TryParse(txtNum.Text, out a))
            {
                CreateNewRow(a);
                tr_foot.Visible = true;
                NoDataPanel.Visible = false;
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

            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();

        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("BZ");
            dt.Columns.Add("JXGZ");
            dt.Columns.Add("RENSHU");
            dt.Columns.Add("PJJXGZ");
            dt.Columns.Add("Note");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                DataRow newRow = dt.NewRow();
                for (int i = 1; i < 6; i++)
                {
                    newRow[i - 1] = ((TextBox)retItem.FindControl("txt" + i)).Text;
                }
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }


        protected void delete_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("BZ");
            dt.Columns.Add("JXGZ");
            dt.Columns.Add("RENSHU");
            dt.Columns.Add("PJJXGZ");
            dt.Columns.Add("Note");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    for (int i = 1; i < 6; i++)
                    {
                        newRow[i - 1] = ((TextBox)retItem.FindControl("txt" + i)).Text;
                    }
                    dt.Rows.Add(newRow);
                }
            }
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();

        }
        #endregion

        //审批
        protected void btnAudit_Click(object sender, EventArgs e)
        {

            string sql = "";
            string state = "";
            if (hidAction.Value == "add" || hidAction.Value == "edit")
            {
                sql = "update TBDS_BZAVERAGE set state='1' where Context='" + hidConext.Value + "'";
                DBCallCommon.ExeSqlText(sql);

                //邮件提醒
                string sprid = "";
                string sptitle = "";
                string spcontent = "";
                sprid = firstid.Value.Trim();
                sptitle = "一线班组平均工资审批";
                spcontent = txtKhNianYue.Text.Trim() + "一线班组平均工资需要您审批，请登录查看！";
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                Response.Write("<script>alert('保存成功！');window.location.href='OM_bzAverageList.aspx';</script>");

            }
            else if (action == "audit")
            {
                if (hidState.Value == "1")
                {
                    if (rblResult1.SelectedIndex == -1)
                    {
                        Response.Write("<script>alert('请选择审核结果！')</script>");
                        return;
                    }
                    else
                    {
                        if (rblResult1.SelectedValue == "0")
                        {
                            state = "2";

                            //邮件提醒
                            string sprid = "";
                            string sptitle = "";
                            string spcontent = "";
                            sprid = secondId.Value.Trim();
                            sptitle = "一线班组平均工资审批";
                            spcontent = txtKhNianYue.Text.Trim() + "一线班组平均工资需要您审批，请登录查看！";
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                        }
                        else if (rblResult1.SelectedValue == "1")
                        {
                            state = "3";
                        }
                        sql = "update TBDS_BZAVERAGE set state='" + state + "',RESULTA='" + rblResult1.SelectedValue + "',TIMEA='" + DateTime.Now.ToString() + "',OPTIONA='" + first_opinion.Text.Trim() + "'  where Context='" + hidConext.Value + "'";
                        DBCallCommon.ExeSqlText(sql);
                        Response.Write("<script>alert('保存成功！');window.location.href='OM_bzAverageList.aspx';</script>");

                    }
                }
                else if (hidState.Value == "2")
                {
                    if (rblResult2.SelectedIndex == -1)
                    {
                        Response.Write("<script>alert('请选择审核结果！')</script>");
                        return;
                    }
                    else
                    {
                        if (rblResult2.SelectedValue == "0")
                        {
                            state = "4";
                        }
                        else if (rblResult2.SelectedValue == "1")
                        {
                            state = "3";
                        }
                        sql = "update TBDS_BZAVERAGE set state='" + state + "',RESULTB='" + rblResult2.SelectedValue + "',TIMEB='" + DateTime.Now.ToString() + "',OPTIONB='" + second_opinion.Text.Trim() + "'  where Context='" + hidConext.Value + "'";
                        DBCallCommon.ExeSqlText(sql);
                        Response.Write("<script>alert('保存成功！');window.location.href='OM_bzAverageList.aspx';</script>");

                    }
                }

            }
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (Checked())
            {
                List<string> list = new List<string>();

                string sql = "";
                string year = txtKhNianYue.Text.Split('-')[0];
                string month = txtKhNianYue.Text.Split('-')[1].PadLeft(2, '0');
                string context = DateTime.Now.ToString("yyyyMMddhhmmss");
                string zdTime = txtTime.Text.Trim();
                if (action == "add")
                {//Year, Month, State, SPRID, SPRNM, ZDRID, ZDRNM, RESULTA, ZDTIME, TIMEA, OPTIONA, MONEY
                    //  Year, Month, State, SPRID, SPRNM, ZDRID, ZDRNM, RESULTA, ZDTIME, TIMEA, OPTIONA, MONEY, JXZong, PeoNum, ConText
                    sql = string.Format("insert into TBDS_BZAVERAGE values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')", year, month, '0', firstid.Value, txt_first.Text.Trim(), Session["UserId"].ToString(), Session["UserName"].ToString(), "", DateTime.Now.ToString("yyyy-MM-dd"), "", "", txtJXFen.Text.Trim(), lblJXZong.Text.Trim(), txtPeoNum.Text.Trim(), context, secondId.Value, txt_second.Text.Trim(), "", "", "");
                    list.Add(sql);
                    foreach (RepeaterItem item in Det_Repeater.Items)
                    {
                        List<string> txt = new List<string>();
                        for (int j = 1; j < 6; j++)
                        {
                            txt.Add(((TextBox)item.FindControl("txt" + j)).Text);
                        }


                        sql = string.Format("insert into TBDS_BZAVERAGE_DETAIL values('{0}','{1}','{2}','{3}','{4}','{5}')", context, txt[0], txt[1], txt[2], txt[3], txt[4]);
                        list.Add(sql);
                    }
                    hidConext.Value = context;
                }
                else if (action == "edit")
                {
                    sql = string.Format("update TBDS_BZAVERAGE set Year='{0}',Month='{1}',State='{2}',SPRID='{3}',SPRNM='{4}',MONEY='{5}',JXZong='{6}',PeoNum='{7}',SPRIDB='{8}',SPRNMB='{9}',ZDTIME='{10}'  where Context='{11}'", year, month, "0", firstid.Value, txt_first.Text.Trim(), txtJXFen.Text.Trim(), lblJXZong.Text.Trim(), txtPeoNum.Text.Trim(), secondId.Value, txt_second.Text, txtTime.Text.Trim(), hidConext.Value);
                    list.Add(sql);
                    sql = "delete from TBDS_BZAVERAGE_DETAIL where Context='" + hidConext.Value + "'";
                    list.Add(sql);

                    foreach (RepeaterItem item in Det_Repeater.Items)
                    {
                        List<string> txt = new List<string>();

                        for (int j = 1; j < 6; j++)
                        {
                            txt.Add(((TextBox)item.FindControl("txt" + j)).Text);
                        }


                        sql = string.Format("insert into TBDS_BZAVERAGE_DETAIL values('{0}','{1}','{2}','{3}','{4}','{5}')", hidConext.Value, txt[0], txt[1], txt[2], txt[3], txt[4]);
                        list.Add(sql);
                    }
                }
                DBCallCommon.ExecuteTrans(list);
               
                hidState.Value = "0";
                Response.Write("<script>alert('保存成功！');</script>");

                btnsubmit.Visible = false;
                btnAudit.Visible = true;
            }
        }

        private bool Checked()
        {
            bool result = true;
            if (txtTime.Text.Trim() == "")
            {
                // ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请填写考核时间！')", true);
                Response.Write("<script>alert('请填写考核时间！')</script>");
                result = false;
            }
            if (txtKhNianYue.Text.Trim() == "")
            {
                // ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择考核模板！')", true);
                Response.Write("<script>alert('请填写年月！')</script>");
                result = false;
            }
            if (firstid.Value == "")
            {
                Response.Write("<script>alert('请选择审批人！')</script>");
                result = false;
            }
            return result;
        }



        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDaoChu_Click(object sender, EventArgs e)
        {

            string sqltext = "";
            sqltext = "select * from TBDS_BZAVERAGE_DETAIL  where Context='" + hidConext.Value + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }




        private void ExportDataItem(System.Data.DataTable objdt)
        {
            string filename = "一线班组平均工资" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("一线班组平均工资.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                sheet0.GetRow(1).GetCell(0).SetCellValue("" + txtKhNianYue.Text.Trim() + " 一线班组平均工资");
                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    IRow row = sheet0.GetRow(i + 3);
                    row.GetCell(0).SetCellValue(Convert.ToString(i + 1));
                    row.GetCell(1).SetCellValue("" + objdt.Rows[i]["BZ"].ToString());
                    row.GetCell(2).SetCellValue("" + objdt.Rows[i]["JXGZ"].ToString());
                    row.GetCell(3).SetCellValue("" + objdt.Rows[i]["RENSHU"].ToString());
                    row.GetCell(4).SetCellValue("" + objdt.Rows[i]["PJJXGZ"].ToString());
                    row.GetCell(5).SetCellValue("" + objdt.Rows[i]["Note"].ToString());
                }
                sheet0.GetRow(11).GetCell(2).SetCellValue("" + lblJXZong.Text.Trim());
                sheet0.GetRow(11).GetCell(3).SetCellValue("" + txtPeoNum.Text.Trim());
                sheet0.GetRow(11).GetCell(4).SetCellValue("" + txtJXFen.Text.Trim());

                for (int i = 0; i <= objdt.Columns.Count; i++)
                {
                    sheet0.AutoSizeColumn(i);
                }
                sheet0.ForceFormulaRecalculation = true;

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }

    }
}