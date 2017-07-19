using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_ZengBuView : System.Web.UI.Page
    {
        string id = string.Empty;//全局变量id
        string action = string.Empty;
        string chId = string.Empty;
        List<string> str = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
                id = Request.QueryString["id"].ToString();//识别号
            if (Request.QueryString["chId"] != null)
                chId = Request.QueryString["chid"].ToString();//变更识别号
            if (Request.QueryString["action"] != null)
                action = Request.QueryString["action"].ToString();//操作
            BindSelectReviewer();
            if (!IsPostBack)
            {
                UserID.Value = Session["UserID"].ToString();
                this.Title = "计划单增补";
                ShowData();
                Control_Enable();
                if (action == "look")
                {
                    LbtnYes.Visible = false;
                    LbtnNO.Visible = false;
                    Pan_ShenHe.Visible = false;
                }
            }
        }

        #region 将数据绑定
        private void ShowData()
        {
            string str1 = "select a.*,b.ST_NAME from TBCM_PLAN as a left join TBDS_STAFFINFO as b on b.ST_ID=a.CM_MANCLERK where ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(str1);
            if (dt.Rows.Count > 0)
            {
                Assign(Panel, dt);
                Assign(Panel1, dt);
            }
            else
            {
                LbtnYes.Visible = false;
                LbtnNO.Visible = false;
                Response.Write("<script>alert('项目被删除，请查看原项目！')</script>");
                return;
            }
            string str2 = "select a.CM_ID,TSA_ID,TSA_ENGNAME,TSA_MAP,TSA_NUMBER,TSA_UNIT,TSA_MATERIAL,TSA_IDNOTE,b.CM_REFER as TSA_TYPENAME from TBCM_CHANGE as a left join TBCM_TYPE as b on a.TSA_TYPE=b.CM_TYPE where CH_ID='" + chId + "'";
            DataTable gr = DBCallCommon.GetDTUsingSqlText(str2);
            this.GridView1.DataSource = gr;
            this.GridView1.DataBind();
            str1 = "select a.CM_NOTE,b.ST_NAME from TBCM_PSVIEW as a left join TBDS_STAFFINFO as b on a.CM_PID=b.ST_ID where CM_ID='" + chId + "' and CM_PIDTYPE='0'";
            dt = DBCallCommon.GetDTUsingSqlText(str1);
            if (dt.Rows.Count > 0)
            {
                lb_Zdr.Text = dt.Rows[0]["ST_NAME"].ToString();
                txt_zdryj.Text = dt.Rows[0]["CM_NOTE"].ToString();
            }
            if (chId != "")
            {
                Hidden.Value = chId;
                CM_Context = chId;
            }
        }

        private void Control_Enable()
        {
            string sql = "select * from TBCM_ChANLIST where CH_ID='" + chId + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                int n = int.Parse(dr["CM_PSJB"].ToString());
                for (int i = 0; i < n; i++)
                {
                    Panel pal = new Panel();
                    pal = (Panel)TabContainer1.FindControl("Pan_ShenHe").FindControl("Panel" + i.ToString());
                    TextBox tbyj = (TextBox)TabContainer1.FindControl("Pan_ShenHe").FindControl("txt_shyj" + i.ToString());

                    Label lb = (Label)TabContainer1.FindControl("Pan_ShenHe").FindControl("lb_shr" + i.ToString());
                    HiddenField hd = (HiddenField)TabContainer1.FindControl("Pan_ShenHe").FindControl("lb_shrid" + i.ToString());
                    if (UserID.Value == hd.Value && tbyj.Text == "")
                    {
                        pal.Enabled = true;
                        tbyj.BorderColor = System.Drawing.Color.Orange;
                        tbyj.Focus();
                        LbtnYes.Visible = true;
                        LbtnNO.Visible = true;
                        if (action == "look")
                        {
                            pal.Enabled = false;
                        }
                    }
                    else
                    {
                        pal.Enabled = false;
                    }
                }
            }
        }

        public string CM_Context
        {
            get;
            set;
        }

        private void Assign(Panel panel, DataTable dt)
        {
            foreach (Control control in panel.Controls)
            {
                if (control is Label)
                {
                    if (dt.Columns.Contains(((Label)control).ID))
                    {
                        ((Label)control).Text = dt.Rows[0][control.ID].ToString();
                    }
                }
            }
        }
        #endregion

        List<string> list = new List<string>();
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.ToString() == "DataRow")
            {
                Label tb = (Label)e.Row.FindControl("TSA_ID");
                string tsa = tb.Text;
                if (list.Count < 1)
                {
                    list.Add(tsa);
                }
                else
                {
                    if (list.Contains(tsa))
                    {
                        tb.Text = "";
                        e.Row.FindControl("TSA_TYPENAME").Visible = false; ;
                    }
                    else
                    {
                        list.Add(tsa);
                    }
                }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("CM_ZengBuPs.aspx");
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            ExcuteSql(2);
        }

        protected void btnNO_Click(object sender, EventArgs e)
        {
            ExcuteSql(3);
        }

        private void ExcuteSql(int j)
        {
            List<string> str_sql = new List<string>();
            string sql = "";
            string psyj = "";
            string txt_yj = "";//意见文本框中的文字
            string riqi = DateTime.Now.ToString("yyyy-MM-dd");//评审时间
            //评审人意见
            sql = "select * from TBCM_CHANLIST where CH_ID='" + chId + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            int psr = 0;
            int n = 0;
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                n = int.Parse(dr["CM_PSJB"].ToString());
                for (int i = 0; i < n; i++)
                {
                    Label lb = (Label)TabContainer1.FindControl("Pan_ShenHe").FindControl("lb_shr" + i);
                    TextBox txt_shyj = (TextBox)TabContainer1.FindControl("Pan_ShenHe").FindControl("txt_shyj" + i);
                    HiddenField hd = (HiddenField)TabContainer1.FindControl("Pan_ShenHe").FindControl("lb_shrid" + i.ToString());
                    if (UserID.Value == hd.Value)
                    {
                        txt_yj = txt_shyj.Text;
                        psr = i + 1;
                    }
                }

                //修改评审状态 评审完成2，已驳回3,
                //根据类型来修改相应的表
                if (j == 2)
                {
                    if (txt_yj == "")
                    {
                        psyj = "同意";
                    }
                    else
                    {
                        psyj = txt_yj;
                    }
                    //评审完之后，邮件提示下一个人有计划单需要审批
                    if (psr < 3 && psr < n)
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(dt.Rows[0]["CM_PS" + (psr + 1)].ToString()), new List<string>(), new List<string>(), "经营计划单评审", "您有经营计划单需要评审！");
                    }

                    if (psr == n && dt.Rows[0]["CM_STATE"].ToString() == "1")
                    {
                        sql = "update TBCM_CHANLIST set CM_STATE='2' where CH_ID='" + chId + "'";
                        str_sql.Add(sql);

                        //计划单增补
                        List<string> tsaId = new List<string>();
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        string strsql = "select * from TBCM_CHANGE where CH_ID='" + chId + "'";
                        DataTable newId = DBCallCommon.GetDTUsingSqlText(strsql);
                        foreach (DataRow drow in newId.Rows)
                        {
                            tsaId.Add(drow["TSA_ID"].ToString());
                            sql = string.Format("insert into TBCM_BASIC(ID,TSA_ID,TSA_ENGNAME,TSA_MAP,TSA_NUMBER,TSA_UNIT,TSA_MATERIAL,TSA_IDNOTE,TSA_TYPE) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", id, drow["TSA_ID"].ToString(), drow["TSA_ENGNAME"].ToString(), drow["TSA_MAP"].ToString(), drow["TSA_NUMBER"].ToString(), drow["TSA_UNIT"].ToString(), drow["TSA_MATERIAL"].ToString(), drow["TSA_IDNOTE"].ToString(), drow["TSA_TYPE"].ToString());
                            str_sql.Add(sql);
                        }
                        //附件添加
                        strsql = "select * from tb_files where BC_CONTEXT='" + chId + "'";
                        DataTable tbfile = DBCallCommon.GetDTUsingSqlText(strsql);
                        strsql = "select * from TBCM_PLAN where ID='" + id + "'";
                        DataTable context = DBCallCommon.GetDTUsingSqlText(strsql);
                        for (int i = 0; i < tbfile.Rows.Count; i++)
                        {
                            if (context.Rows.Count > 0)
                            {
                                DataRow tbrow = tbfile.Rows[i];
                                sql = string.Format("insert into tb_files values('{0}','{1}','{2}','{3}','{4}')", context.Rows[0]["CM_CONTEXT"].ToString(), tbrow["fileload"].ToString(), tbrow["fileUpDate"].ToString(), tbrow["fileName"].ToString(), tbrow["ShowName"].ToString());
                                str_sql.Add(sql);
                            }
                        }

                        tsaId.Distinct().ToList();
                        foreach (string item in tsaId)
                        {
                            strsql = "select * from TBCM_BASIC where TSA_ID='" + item + "'";
                            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(strsql);
                            strsql = "select * from TBCM_CHANGE where CH_ID='" + chId + "' and TSA_ID='" + item + "'";
                            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(strsql);
                            string engName = "";
                            if (dt1.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt1.Rows.Count; i++)
                                {
                                    engName += "," + dt1.Rows[i]["TSA_ENGNAME"].ToString();
                                }
                                for (int i = 0; i < dt2.Rows.Count; i++)
                                {
                                    engName += "," + dt2.Rows[i]["TSA_ENGNAME"].ToString();
                                }
                                engName = engName.Substring(1);
                            }
                        }
                        //计划单增补审核通过插入销售合同
                        string tsa_id = "";
                        string sqltext = "";
                        string jhdtype = "";

                        foreach (GridViewRow gr in GridView1.Rows)
                        {
                            string TSA_ID = ((System.Web.UI.WebControls.Label)gr.FindControl("TSA_ID")).Text.ToString();
                            if (TSA_ID == "" || TSA_ID == null)
                                TSA_ID = tsa_id;
                            else
                                tsa_id = TSA_ID;
                            string CM_ENGNAME = ((System.Web.UI.WebControls.Label)gr.FindControl("TSA_ENGNAME")).Text.ToString();
                            string CM_MAP = ((System.Web.UI.WebControls.Label)gr.FindControl("TSA_MAP")).Text.ToString();
                            string CM_NUMBER = ((System.Web.UI.WebControls.Label)gr.FindControl("TSA_NUMBER")).Text.ToString();
                            string CM_UNIT = ((System.Web.UI.WebControls.Label)gr.FindControl("TSA_UNIT")).Text.ToString();
                            string CM_MATERIAL = ((System.Web.UI.WebControls.Label)gr.FindControl("TSA_MATERIAL")).Text.ToString();
                            string sqlstid = "select TSA_ID from TBPM_DETAIL where CM_CONTR='" + CM_CONTR.Text.Trim() + "'and TSA_ID='" + tsa_id.Trim() + "' and CM_JHDTYPE='0'";
                            DataTable stiddata = DBCallCommon.GetDTUsingSqlText(sqlstid);
                            if (stiddata.Rows.Count > 0)
                                jhdtype = "1";
                            else
                                jhdtype = "3";

                            sqltext = "insert into TBPM_DETAIL(CM_CONTR,TSA_ID,CM_PROJ,CM_YZHTH,CM_ENGNAME,CM_MATERIAL,CM_MAP,CM_NUMBER,CM_UNIT,CM_JHDTYPE) values('" + CM_CONTR.Text.Trim() + "','" + tsa_id.Trim() + "','" + CM_PROJ.Text.Trim() + "','" + CM_DFCONTR.Text.Trim() + "','" + CM_ENGNAME.Trim() + "','" + CM_MATERIAL.Trim() + "','" + CM_MAP.Trim() + "','" + CM_NUMBER.Trim() + "','" + CM_UNIT.Trim() + "','" + jhdtype + "')";
                            str_sql.Add(sqltext);
                        }

                        //2017.1.5修改，添加合同号和项目名称
                        string cs = "select CM_PID,CM_CONTR,CM_PROJ from TBCM_PSVIEW left join TBCM_CHANGE on TBCM_PSVIEW.CM_ID=TBCM_CHANGE.CH_ID left join TBCM_PLAN on TBCM_CHANGE.ID=TBCM_PLAN.ID where TBCM_PSVIEW.CM_ID='" + chId + "'  and CM_PIDTYPE<>''and CM_PIDTYPE is not null";
                        //string cs = "select CM_PID from TBCM_PSVIEW where CM_ID='" + chId + "' and CM_PIDTYPE<>''and CM_PIDTYPE is not null";
                        DataTable cs_dt = DBCallCommon.GetDTUsingSqlText(cs);
                        List<string> cs_list = new List<string>();
                        for (int i = 0; i < cs_dt.Rows.Count; i++)
                        {
                            cs_list.Add(DBCallCommon.GetEmailAddressByUserID(cs_dt.Rows[i][0].ToString()));
                        }
                        if (!cs_list.Contains(DBCallCommon.GetEmailAddressByUserID("202")))
                            cs_list.Add(DBCallCommon.GetEmailAddressByUserID("202"));
  
                        if (cs_list.Count != 0)
                        {
                            cs_list = cs_list.Distinct().ToList();
                            for (int k = 0; k < cs_list.Count; k++)
                            {

                                string fa_all_mail = cs_list[k];
                                //2017.1.5修改，添加合同号和项目名称
                                DBCallCommon.SendEmail(fa_all_mail, new List<string>(), new List<string>(), "经营计划单增补", "您有合同号为" + cs_dt.Rows[0]["CM_CONTR"].ToString() + ",项目名称为" + cs_dt.Rows[0]["CM_PROJ"].ToString() + "的经营计划单增补需要查看！");
                                //DBCallCommon.SendEmail(fa_all_mail, new List<string>(), new List<string>(), "经营计划单增补", "您有经营计划单增补需要查看！");
                            }
                        }
                       // DBCallCommon.SendEmail("", cs_list, new List<string>(), "经营计划单增补", "您有经营计划单增补需要查看！");
                    }
                }
                else  //不同意
                {
                    if (txt_yj == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('驳回意见不能为空！');", true); return;
                    }
                    else
                    {
                        psyj = txt_yj;
                    }
                    sql = "update TBCM_CHANLIST set CM_STATE='3' where CH_ID='" + chId + "'";
                    str_sql.Add(sql);
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(dt.Rows[0]["CM_MANCLERK"].ToString()), new List<string>(), new List<string>(), "经营计划单被驳回", "您提交的经营计划单被驳回！请修改后再次提交");
                }
                sql = string.Format("update TBCM_CHANLIST set CM_PSYJ{0}='{1}',CM_PSSJ{0}='{2}',CM_NOTE{0}='{3}' where CH_ID='{4}'", psr, j, riqi, psyj, chId);
                str_sql.Add(sql);
                DBCallCommon.ExecuteTrans(str_sql);
                Response.Redirect("CM_ZengBuPs.aspx");
            }
        }

        private void BindSelectReviewer()
        {
            List<List<string>> dic = new List<List<string>>();
            //绑定市场部的评审人
            string sqltext = "select * from View_CM_Change where CH_ID='" + chId + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                DataRow tr = dt.Rows[0];
                for (int i = 1; i < 4; i++)
                {
                    if (tr["CM_PS" + i].ToString() != "")
                    {
                        List<string> ps = new List<string>() { "市场部", tr["CM_NAME" + i].ToString(), tr["CM_PSYJ" + i].ToString(), tr["CM_PSSJ" + i].ToString(), tr["CM_NOTE" + i].ToString(), tr["CM_PS" + i].ToString() };
                        dic.Add(ps);
                    }
                }
            }
            for (int i = 0; i < dic.Count; i++)
            {
                List<string> li = dic[i];

                #region 绑定部门负责人意见

                Panel pal = new Panel();
                pal.ID = "Panel" + i;
                TableRow tr1 = new TableRow();
                TableCell td1 = new TableCell();

                TableRow tr2 = new TableRow();
                TableCell td21 = new TableCell();
                TableCell td22 = new TableCell();

                td21.Width = 100;
                Label lab1 = new Label();
                Label lab2 = new Label();
                Label lab3 = new Label();
                Label lab4 = new Label();
                Label lab5 = new Label();

                //第一行
                lab1.ID = "lb_shbm" + i;
                lab1.Text = "审核部门:" + li[0];// 审核部门
                lab1.Font.Bold = true;
                lab1.Width = 150;
                td1.Controls.Add(lab1);

                lab2.ID = "lb_shr" + i;
                lab2.Text = "审核人:" + li[1]; // 审核人
                lab2.Font.Bold = true;
                lab2.Width = 150;
                td1.Controls.Add(lab2);

                HiddenField hd = new HiddenField();
                hd.ID = "lb_shrid" + i;
                hd.Value = li[5].ToString();
                td1.Controls.Add(hd);

                lab3.ID = "lb_shjg" + i;
                lab3.Text = li[2] == "1" ? "审核结论:未审批" : li[2] == "3" ? "审核结论:不同意" : "审核结论:同意";//审核结论 
                lab3.Font.Bold = true;
                lab3.Width = 150;
                td1.Controls.Add(lab3);

                lab4.ID = "lb_shsj" + i;
                lab4.Text = "审核时间:" + li[3]; //  审核时间
                lab4.Font.Bold = true;
                lab4.Width = 150;
                td1.Controls.Add(lab4);
                td1.ColumnSpan = 2;

                tr1.Cells.Add(td1);

                //第二行
                lab5.Text = "审核意见：";
                td21.Controls.Add(lab5);
                td21.Attributes.Add("align", "center");

                TextBox tb = new TextBox();
                tb.ID = "txt_shyj" + i;
                tb.Text = li[4]; //审核意见
                tb.Rows = 4;
                tb.Columns = 100;
                tb.TextMode = TextBoxMode.MultiLine;

                if (li[2] == "3") //不同意
                    tb.BorderColor = System.Drawing.Color.Red;
                else if (li[2] == "2") //同意
                    tb.BorderColor = System.Drawing.Color.Aqua;
                td22.Controls.Add(tb);
                tr2.Cells.Add(td21);
                tr2.Cells.Add(td22);

                Table tb_bmshr = new Table();
                tb_bmshr.Controls.Add(tr1);
                tb_bmshr.Controls.Add(tr2);
                tb_bmshr.CellPadding = 4;
                tb_bmshr.CellSpacing = 1;
                tb_bmshr.BorderWidth = 1;
                tb_bmshr.CssClass = "toptable grid";
                tb_bmshr.Attributes.Add("style", "width=100%");
                pal.Controls.Add(tb_bmshr);
                Pan_ShenHe.Controls.Add(pal);
                #endregion
            }
        }
    }
}
