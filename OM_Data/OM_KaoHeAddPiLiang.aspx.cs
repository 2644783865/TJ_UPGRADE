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
    public partial class OM_KaoHeAddPiLiang : System.Web.UI.Page
    {
        string action;
        string id = string.Empty;
        string khTime = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
                action = Request.QueryString["action"];
            hidAction.Value = action;
            if (!IsPostBack)
            {
                btnAudit.Visible = false;
                InitP();
                ContralEnable();

            }

            lbtitle.Text = ddlType.SelectedValue.ToString().Trim();
            //审核部分
            lbtitlesh.Text = ddlType.SelectedValue.ToString().Trim();
        }

        private void ContralEnable()
        {
            //审核部分
            rblSHJS.Enabled = true;

            txtshname1.Enabled = true;
            Hylsh1.Visible = true;

            txtshname2.Enabled = true;
            Hylsh2.Visible = true;

            txtshname3.Enabled = true;
            Hylsh3.Visible = true;

            txtshname4.Enabled = true;
            Hylsh4.Visible = true;

            yjshh.Visible = true;
            ejshh.Visible = true;
            sjshh.Visible = false;
            foursh.Visible = false;
        }


        private void InitP()
        {
            lb1.Text = Session["UserName"].ToString();
            txtTime.Text = DateTime.Now.ToString("yyyy-MM-dd").Trim();
            txtKhNianYue.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM").Trim();
            BindPart();
            BindPosition();
            BindPeople();
            BindPartMB();
            BindMb();
        }

        private void BindMb()
        {


            string sql = string.Format("select Kh_Name,kh_Fkey from TBDS_KaoHeMBList where kh_dep='{0}' and kh_State='0'", ddlPartMB.SelectedValue);//按照表里面的顺序排。不然直接distinct会按拼音排序。
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddlKaohMB.DataSource = dt;
            ddlKaohMB.DataTextField = "Kh_Name";
            ddlKaohMB.DataValueField = "kh_Fkey";
            ddlKaohMB.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddlKaohMB.Items.Insert(0, item);



        }


        private void BindPart()
        {
            string sql = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddl_Depart.Items.Insert(0, item);
            ddl_Depart.SelectedValue = Session["UserDeptID"].ToString().Trim();
        }



        private void BindPartMB()
        {
            string sql = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddlPartMB.DataSource = dt;
            ddlPartMB.DataTextField = "DEP_NAME";
            ddlPartMB.DataValueField = "DEP_CODE";
            ddlPartMB.DataBind();
            ListItem itemNew = new ListItem();
            itemNew.Text = "通用模板";
            itemNew.Value = "TongY";
            ddlPartMB.Items.Insert(0, itemNew);
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddlPartMB.Items.Insert(0, item);
            ddlPartMB.SelectedValue = Session["UserDeptID"].ToString().Trim();
        }


        protected void BindPosition() //将职位信息绑定到职位下拉框
        {
            string sql = string.Format("select min(ST_ID) as ST_ID,ST_POSITION,d.DEP_NAME as DEP_POSITION from TBDS_STAFFINFO as a left join TBDS_DEPINFO as d on a.ST_POSITION = d.DEP_CODE where ST_DEPID='{0}'and a.ST_POSITION<>'' and a.ST_POSITION is not null group by d.DEP_NAME,ST_POSITION order by ST_ID", ddl_Depart.SelectedValue);//按照表里面的顺序排。不然直接distinct会按拼音排序。
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddl_Position.DataSource = dt;
            ddl_Position.DataTextField = "DEP_POSITION";
            ddl_Position.DataValueField = "ST_POSITION";
            ddl_Position.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddl_Position.Items.Insert(0, item);
        }

        protected void BindPeople() //绑定人员信息
        {
            string sql = string.Format("select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_POSITION='{0}' and ST_PD='0'", ddl_Position.SelectedValue);//按照表里面的顺序排。不然直接distinct会按拼音排序。
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            cblPerson.DataSource = dt;
            cblPerson.DataTextField = "ST_NAME";
            cblPerson.DataValueField = "ST_ID";
            cblPerson.DataBind();
        }

        protected void ddl_Depart_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPosition();
            cblPerson.Items.Clear();

        }

        protected void ddl_Position_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPeople();

        }

        protected void ddl_Person_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindMb();
        }
        protected void btn_Click(object sender, EventArgs e)
        {
            Det_Repeater.DataSource = null;
            Det_Repeater.DataBind();
            downloadMB();

        }


        private void BindKh()
        {
            string sql = "select * from TBDS_KaoHeDetail where kh_Context='" + hidConext.Value + "'  order by kh_Id asc";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {

                sql = "select * from TBDS_KaoHeColReal where kh_context='" + dt.Rows[0]["kh_Context"] + "'";
                DataTable col = DBCallCommon.GetDTUsingSqlText(sql);
                if (col.Rows.Count > 0)
                {
                    Det_Repeater.DataSource = dt;
                    Det_Repeater.DataBind();
                    //绑定列名
                    List<int> cols = new List<int>();
                    for (int i = 1; i < 13; i++)
                    {
                        Label lb = (Label)Det_Repeater.Controls[0].FindControl("kh_Col" + i);
                        string str = col.Rows[0]["kh_Col" + i].ToString();
                        lb.Text = str;
                        if (str == "")
                        {
                            cols.Add(i);
                            Control head = Det_Repeater.Controls[0];
                            HtmlTableCell htc = (HtmlTableCell)head.FindControl("col" + i);
                            htc.Visible = false;
                        }
                    }
                    foreach (RepeaterItem item in Det_Repeater.Items)
                    {
                        for (int i = 0; i < cols.Count; i++)
                        {
                            HtmlTableCell cont = (HtmlTableCell)item.FindControl("td" + cols[i]);
                            cont.Visible = false;
                        }
                        for (int i = 1; i < 13 - cols.Count; i++)
                        {
                            HtmlTableCell hcont = (HtmlTableCell)item.FindControl("td" + i);
                            if (!hcont.InnerText.Contains("<br />"))
                            {
                                hcont.Style.Add("text-align", "center");
                            }
                        }
                    }
                    sql = "select * from TBDS_KaoHeList where kh_context='" + hidConext.Value + "'";
                    DataTable bl = DBCallCommon.GetDTUsingSqlText(sql);
                    if (bl.Rows.Count > 0)
                    {
                        string txtbl = bl.Rows[0]["Kh_BL"].ToString();
                        lblBL.Text = txtbl;
                        ddl_zipstate.SelectedValue = bl.Rows[0]["Kh_zipingif"].ToString().Trim();
                        foreach (RepeaterItem item in Det_Repeater.Items)
                        {
                            for (int i = 0; i < txtbl.Split('|').Count(); i++)
                            {
                                if (txtbl.Split('|')[i] == "0")
                                {
                                    TextBox box = (TextBox)item.FindControl("kh_Score" + i);
                                    box.Visible = false;
                                }

                            }
                        }

                        if (txtbl.Split('|')[0] == "0")
                        {
                            Panel1.Visible = false;
                            txtResult1.Visible = false;
                        }
                        if (txtbl.Split('|')[1] == "0")
                        {
                            txtResult2.Visible = false;
                            Panel2.Visible = false;
                        }
                        if (txtbl.Split('|')[2] == "0")
                        {
                            txtResult3.Visible = false;
                            Panel3.Visible = false;
                        }
                        if (txtbl.Split('|')[3] == "0")
                        {
                            txtResult4.Visible = false;
                            Panel5.Visible = false;
                        }

                    }


                    foot.ColSpan = 13 - cols.Count;
                    sql = "select sum(kh_Weight) from TBDS_KaoHeDetail where kh_context='" + hidConext.Value + "'";
                    lb_Result.Text = DBCallCommon.GetDTUsingSqlText(sql).Rows[0][0].ToString();
                    tr_foot.Visible = true;
                    Det_Repeater.Visible = true;
                    NoDataPanel.Visible = false;
                }
                else
                {
                    // ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('考核模板列名出错！请重新添加模板或者联系管理员');", true);
                    Response.Write("<script>alert('考核模板列名出错！请重新添加模板或者联系管理员！');</script>");

                    Det_Repeater.Visible = false;
                    NoDataPanel.Visible = true;
                }
            }
            else
            {
                // ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('未成功加载考核模板！请添加模板或者联系管理员');", true);
                Response.Write("<script>alert('未成功加载考核模板！请添加模板或者联系管理员！');</script>");
                Det_Repeater.Visible = false;
                NoDataPanel.Visible = true;
            }
        }


        private void downloadMB()
        {
            string sql = "";
            if (action == "add")
            {
                sql = "select *,'' as kh_Note,'' as kh_Score1,'' as kh_Score2,'' as kh_Score3,'' as kh_Score4,'' as kh_ScoreOwn,'' as Id from View_KaoHe where kh_Fkey='" + ddlKaohMB.SelectedValue + "' and kh_State='0' order by kh_Id asc";

            }
            else
            {
                sql = "select *,'' as kh_Note,'' as kh_Score1,'' as kh_Score2,'' as kh_Score3,'' as kh_Score4,'' as kh_ScoreOwn,'' as Id from View_KaoHe where kh_Fkey='" + ddlKaohMB.SelectedValue + "'  order by kh_Id asc";
            }

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {

                sql = "select * from TBDS_KaoHeCol where kh_Fkey='" + dt.Rows[0]["kh_Fkey"] + "'";
                DataTable col = DBCallCommon.GetDTUsingSqlText(sql);
                if (col.Rows.Count > 0)
                {
                    Det_Repeater.DataSource = dt;
                    Det_Repeater.DataBind();
                    //绑定列名
                    List<int> cols = new List<int>();
                    for (int i = 1; i < 13; i++)
                    {
                        Label lb = (Label)Det_Repeater.Controls[0].FindControl("kh_Col" + i);
                        string str = col.Rows[0]["kh_Col" + i].ToString();
                        lb.Text = str;
                        if (str == "")
                        {
                            cols.Add(i);
                            Control head = Det_Repeater.Controls[0];
                            HtmlTableCell htc = (HtmlTableCell)head.FindControl("col" + i);
                            htc.Visible = false;
                        }
                    }
                    foreach (RepeaterItem item in Det_Repeater.Items)
                    {
                        for (int i = 0; i < cols.Count; i++)
                        {
                            HtmlTableCell cont = (HtmlTableCell)item.FindControl("td" + cols[i]);
                            cont.Visible = false;
                        }
                        //for (int i = 1; i < 13 - cols.Count; i++)
                        //{
                        //    HtmlTableCell hcont = (HtmlTableCell)item.FindControl("td" + i);
                        //    hcont.InnerText.Replace("\r\n", "<br />");
                        //    hcont.InnerText.Replace("\n", "<br />");
                        //    if (!hcont.InnerText.Contains("<br />"))
                        //    {
                        //        hcont.Style.Add("text-align", "center");
                        //    }

                        //}
                    }
                    sql = "select * from TBDS_KaoHeMBList where kh_Fkey='" + ddlKaohMB.SelectedValue + "'";
                    DataTable bl = DBCallCommon.GetDTUsingSqlText(sql);
                    if (bl.Rows.Count > 0)
                    {
                        string txtbl = bl.Rows[0]["Kh_BL"].ToString();
                        txtPFBZ.Text = bl.Rows[0]["Kh_Note"].ToString();
                        lblBL.Text = txtbl;
                        foreach (RepeaterItem item in Det_Repeater.Items)
                        {
                            for (int i = 0; i < txtbl.Split('|').Count(); i++)
                            {
                                if (txtbl.Split('|')[i] == "0")
                                {
                                    TextBox box = (TextBox)item.FindControl("kh_Score" + i);
                                    box.Visible = false;
                                }

                            }
                        }
                        Panel1.Visible = true;
                        Panel2.Visible = true;
                        Panel3.Visible = true;

                        if (txtbl.Split('|')[0] == "0")
                        {
                            txtResult1.Visible = false;
                            Panel1.Visible = false;
                        }
                        if (txtbl.Split('|')[1] == "0")
                        {
                            txtResult2.Visible = false;
                            Panel2.Visible = false;
                        }
                        if (txtbl.Split('|')[2] == "0")
                        {
                            txtResult3.Visible = false;
                            Panel3.Visible = false;
                        }
                        if (txtbl.Split('|')[3] == "0")
                        {
                            txtResult4.Visible = false;
                            Panel5.Visible = false;
                        }
                        if (ddlFankui.SelectedValue == "1")
                        {
                            Panel4.Visible = false;
                        }
                    }


                    foot.ColSpan = 13 - cols.Count;
                    sql = "select sum(kh_Weight) from TBDS_KaoHeDetail where kh_Context='" + ddlKaohMB.SelectedValue + "'";
                    lb_Result.Text = DBCallCommon.GetDTUsingSqlText(sql).Rows[0][0].ToString();
                    tr_foot.Visible = true;
                    Det_Repeater.Visible = true;
                    NoDataPanel.Visible = false;
                }
                else
                {
                    //   ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('考核模板列名出错！请重新添加模板或者联系管理员');", true);
                    Response.Write("<script>alert('考核模板列名出错！请重新添加模板或者联系管理员！');</script>");
                    Det_Repeater.Visible = false;
                    NoDataPanel.Visible = true;
                }
            }
            else
            {
                // ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('未成功加载考核模板！请添加模板或者联系管理员');", true);
                Response.Write("<script>alert('未成功加载考核模板！请添加模板或者联系管理员！');</script>");
                Det_Repeater.Visible = false;
                NoDataPanel.Visible = true;
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            if (Checked())
            {
                List<string> list = new List<string>();
                for (int k = 0; k < cblPerson.Items.Count; k++)
                {
                    if (cblPerson.Items[k].Selected == true)
                    {
                        string person = cblPerson.Items[k].Value;

                        List<string> col = new List<string>();
                        string sql = "";
                        string year = txtKhNianYue.Text.Split('-')[0];
                        string month = txtKhNianYue.Text.Split('-')[1].PadLeft(2, '0');
                        string context = DateTime.Now.ToString("yyyyMMddhhmmss") + k.ToString();
                        if (action == "add")
                        {
                            sql = string.Format("insert into TBDS_KaoHeList values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}','{54}')", person, txt_Result.Text, txtTime.Text, Session["UserID"], ddlKaohMB.SelectedValue, "0", ddlFankui.SelectedValue, firstid.Value, "", "", secondid.Value, "", "", thirdid.Value, "", "", "", "", lblBL.Text, ddlType.SelectedValue, context, fifthId.Value, "", "", "", "", "", "", "", year, month, txtPFBZ.Text, ddl_zipstate.SelectedValue.ToString().Trim(), "0", rblSHJS.SelectedValue.ToString().Trim(), txtshid1.Value.Trim(), txtshname1.Text.Trim(), "0", "", "", txtshid2.Value.Trim(), txtshname2.Text.Trim(), "0", "", "", txtshid3.Value.Trim(), txtshname3.Text.Trim(), "0", "", "", txtshid4.Value.Trim(), txtshname4.Text.Trim(), "0", "", "");
                            list.Add(sql);
                            for (int i = 1; i < 13; i++)
                            {
                                col.Add(((ITextControl)Det_Repeater.Controls[0].FindControl("kh_Col" + i)).Text);
                            }
                            sql = string.Format("insert into TBDS_KaoHeColReal values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')", col[0], col[1], col[2], col[3], col[4], col[5], col[6], col[7], col[8], col[9], col[10], col[11], context);
                            list.Add(sql);

                            foreach (RepeaterItem item in Det_Repeater.Items)
                            {
                                List<string> txt = new List<string>();
                                for (int j = 1; j < 13; j++)
                                {
                                    txt.Add(((HtmlTableCell)item.FindControl("td" + j)).InnerText.Replace(Convert.ToString((char)13), "<br />"));
                                }
                                string weight = ((HtmlTableCell)item.FindControl("weight")).InnerText.Trim();

                                sql = string.Format("insert into TBDS_KaoHeDetail values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}')", item.ItemIndex, ((ITextControl)item.FindControl("kh_Score0")).Text, ((ITextControl)item.FindControl("kh_Note")).Text, context, ((ITextControl)item.FindControl("kh_Score1")).Text, ((ITextControl)item.FindControl("kh_Score2")).Text, txt[0], txt[1], txt[2], txt[3], txt[4], txt[5], txt[6], txt[7], txt[8], txt[9], txt[10], txt[11], weight, "", "", "");
                                list.Add(sql);
                            }
                        }
                        hidConext.Value += context + "|";
                    }
                }
                DBCallCommon.ExecuteTrans(list);

                hidState.Value = "0";
                Response.Write("<script>alert('保存成功！');</script>");
                //window.location.href='OM_KaoHeList.aspx';
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
            if (ddlKaohMB.SelectedValue == "00")
            {
                // ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择考核模板！')", true);
                Response.Write("<script>alert('请选择考核模板！')</script>");
                result = false;
            }


            if (txtKhNianYue.Text == "")
            {
                //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择主管经理！')", true);
                Response.Write("<script>alert('请选择考核年月！')</script>");
                result = false;
            }


            //string datecheck = DateTime.Now.ToString("yyyy-MM").Trim() + "-10";
            //string datetimenow = DateTime.Now.ToString("yyyy-MM-dd").Trim();
            //string datetxt = txtTime.Text.Trim();
            //if ((string.Compare(datetimenow, datecheck))>0 || (string.Compare(datetxt,datecheck))>0)
            //{
            //    Response.Write("<script>alert('考核时间应在本月10号以前提交！')</script>");
            //    result = false;
            //}

            List<string> list = new List<string>();
            for (int k = 0; k < cblPerson.Items.Count; k++)
            {
                if (cblPerson.Items[k].Selected == true)
                {
                    list.Add(cblPerson.Items[k].Value);
                }
            }
            if (list.Contains(firstid.Value) || list.Contains(secondid.Value) || list.Contains(thirdid.Value))
            {
                Response.Write("<script>alert('被考核人不能与评审人相同！')</script>");
                result = false;
            }


            if (hidState.Value == "0" || hidState.Value == "")
            {
                if (lblBL.Text.Split('|')[0] != "0")
                {
                    if (firstid.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择主管考核人！')", true);
                        Response.Write("<script>alert('请选择一级考核人！')</script>");
                        result = false;
                    }
                }
                else if (lblBL.Text.Split('|')[1] != "0")
                {
                    if (secondid.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择部门负责人！')", true);
                        Response.Write("<script>alert('请选择二级考核人！')</script>");
                        result = false;
                    }
                }
                else if (lblBL.Text.Split('|')[2] != "0")
                {
                    if (thirdid.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择部门负责人！')", true);
                        Response.Write("<script>alert('请选择三级考核人！')</script>");
                        result = false;
                    }
                }
                else
                {
                    if (fifthId.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择部门负责人！')", true);
                        Response.Write("<script>alert('请选择四级考核人！')</script>");
                        result = false;
                    }
                }


            }
            else if (hidState.Value == "2")
            {
                if (lblBL.Text.Split('|')[1] != "0")
                {
                    if (secondid.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择部门负责人！')", true);
                        Response.Write("<script>alert('请选择二级考核人！')</script>");
                        result = false;
                    }

                }
                else if (lblBL.Text.Split('|')[2] != "0")
                {
                    if (thirdid.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择部门负责人！')", true);
                        Response.Write("<script>alert('请选择三级考核人！')</script>");
                        result = false;
                    }
                }
                else if (lblBL.Text.Split('|')[3] != "0")
                {
                    if (fifthId.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择部门负责人！')", true);
                        Response.Write("<script>alert('请选择四级考核人！')</script>");
                        result = false;
                    }
                }
            }
            else if (hidState.Value == "3")
            {
                if (lblBL.Text.Split('|')[2] != "0")
                {
                    if (thirdid.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择主管经理！')", true);
                        Response.Write("<script>alert('请选择三级考核人！')</script>");
                        result = false;
                    }

                }
                else if (lblBL.Text.Split('|')[3] != "0")
                {
                    if (fifthId.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择主管经理！')", true);
                        Response.Write("<script>alert('请选择四级考核人！')</script>");
                        result = false;
                    }
                }
            }
            else if (hidState.Value == "4")
            {
                if (lblBL.Text.Split('|')[3] != "0")
                {
                    if (fifthId.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择主管经理！')", true);
                        Response.Write("<script>alert('请选择四级考核人！')</script>");
                        result = false;
                    }

                }
            }

            //审核部分
            if (rblSHJS.SelectedValue.ToString().Trim() == "1")
            {
                if (txtshname1.Text.Trim() == "" || txtshid1.Value.Trim() == "")
                {
                    Response.Write("<script>alert('请选择一级审核人！')</script>");
                    result = false;
                }
            }
            else if (rblSHJS.SelectedValue.ToString().Trim() == "2")
            {
                if (txtshname1.Text.Trim() == "" || txtshid1.Value.Trim() == "")
                {
                    Response.Write("<script>alert('请选择一级审核人！')</script>");
                    result = false;
                }
                if (txtshname2.Text.Trim() == "" || txtshid2.Value.Trim() == "")
                {
                    Response.Write("<script>alert('请选择二级审核人！')</script>");
                    result = false;
                }
            }
            else if (rblSHJS.SelectedValue.ToString().Trim() == "3")
            {
                if (txtshname1.Text.Trim() == "" || txtshid1.Value.Trim() == "")
                {
                    Response.Write("<script>alert('请选择一级审核人！')</script>");
                    result = false;
                }
                if (txtshname2.Text.Trim() == "" || txtshid2.Value.Trim() == "")
                {
                    Response.Write("<script>alert('请选择二级审核人！')</script>");
                    result = false;
                }
                if (txtshname3.Text.Trim() == "" || txtshid3.Value.Trim() == "")
                {
                    Response.Write("<script>alert('请选择三级审核人！')</script>");
                    result = false;
                }
            }
            else if (rblSHJS.SelectedValue.ToString().Trim() == "4")
            {
                if (txtshname1.Text.Trim() == "" || txtshid1.Value.Trim() == "")
                {
                    Response.Write("<script>alert('请选择一级审核人！')</script>");
                    result = false;
                }
                if (txtshname2.Text.Trim() == "" || txtshid2.Value.Trim() == "")
                {
                    Response.Write("<script>alert('请选择二级审核人！')</script>");
                    result = false;
                }
                if (txtshname3.Text.Trim() == "" || txtshid3.Value.Trim() == "")
                {
                    Response.Write("<script>alert('请选择三级审核人！')</script>");
                    result = false;
                }
                if (txtshname4.Text.Trim() == "" || txtshid4.Value.Trim() == "")
                {
                    Response.Write("<script>alert('请选择四级审核人！')</script>");
                    result = false;
                }
            }
            //审核部分end
            return result;
        }

        //审批
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            if (Checked())
            {


                string sql = "";
                if (hidAction.Value == "add")
                {
                    List<string> list = new List<string>();
                    for (int i = 0; i < hidConext.Value.Trim('|').Split('|').Length; i++)
                    {
                        if (ddl_zipstate.SelectedValue.ToString().Trim() == "1" && rblSHJS.SelectedValue.ToString().Trim() != "0")
                        {
                            sql = "update TBDS_KaoHeList set Kh_state='2',Kh_shtoltalstate='1' where kh_Context='" + hidConext.Value.Split('|')[i].ToString() + "'";
                        }
                        else if (ddl_zipstate.SelectedValue.ToString().Trim() == "0" && rblSHJS.SelectedValue.ToString().Trim() != "0")
                        {
                            sql = "update TBDS_KaoHeList set Kh_state='1',Kh_shtoltalstate='1' where kh_Context='" + hidConext.Value.Split('|')[i].ToString() + "'";
                        }
                        else if (ddl_zipstate.SelectedValue.ToString().Trim() == "1" && rblSHJS.SelectedValue.ToString().Trim() == "0")
                        {
                            sql = "update TBDS_KaoHeList set Kh_state='2',Kh_shtoltalstate='2' where kh_Context='" + hidConext.Value.Split('|')[i].ToString() + "'";
                        }
                        else if (ddl_zipstate.SelectedValue.ToString().Trim() == "0" && rblSHJS.SelectedValue.ToString().Trim() == "0")
                        {
                            sql = "update TBDS_KaoHeList set Kh_state='1',Kh_shtoltalstate='2' where kh_Context='" + hidConext.Value.Split('|')[i].ToString() + "'";
                        }
                        else
                        {
                            sql = "update TBDS_KaoHeList set Kh_state='1',Kh_shtoltalstate='1' where kh_Context='" + hidConext.Value.Split('|')[i].ToString() + "'";
                        }
                        list.Add(sql);
                    }


                    DBCallCommon.ExecuteTrans(list);

                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sprid = firstid.Value.Trim();
                    sptitle = "考核人评价项审批";
                    spcontent = "有" + txtKhNianYue.Text.Trim() + "的" + lbtitle.Text.Trim() + "需要您审批，请登录查看！";
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    Response.Write("<script>alert('保存成功！');window.location.href='OM_KaoHeList.aspx';</script>");

                }
            }
        }

        //审核部分
        protected void rblSHJS_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblSHJS.SelectedIndex == 0)
            {
                yjshh.Visible = false;
                ejshh.Visible = false;
                sjshh.Visible = false;
                foursh.Visible = false;
            }
            else if (rblSHJS.SelectedIndex == 1)
            {
                yjshh.Visible = true;
                ejshh.Visible = false;
                sjshh.Visible = false;
                foursh.Visible = false;
            }
            else if (rblSHJS.SelectedIndex == 2)
            {
                yjshh.Visible = true;
                ejshh.Visible = true;
                sjshh.Visible = false;
                foursh.Visible = false;
            }
            else if (rblSHJS.SelectedIndex == 3)
            {
                yjshh.Visible = true;
                ejshh.Visible = true;
                sjshh.Visible = true;
                foursh.Visible = false;
            }
            else
            {
                yjshh.Visible = true;
                ejshh.Visible = true;
                sjshh.Visible = true;
                foursh.Visible = true;
            }
            txtshname1.Text = "";
            txtshid1.Value = "";
            txtshname2.Text = "";
            txtshid2.Value = "";
            txtshname3.Text = "";
            txtshid3.Value = "";
            txtshname4.Text = "";
            txtshid4.Value = "";
        }
    }
}
