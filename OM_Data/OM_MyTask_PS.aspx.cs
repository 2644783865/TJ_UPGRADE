using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_MyTask_PS : System.Web.UI.Page
    {
        string id = string.Empty;
        string action = string.Empty;
        string type = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Action"] != null)
                action = Request.QueryString["action"].ToString();

            if (Request.QueryString["ID"] != null)
                id = Request.QueryString["ID"].ToString();

            if (Request.QueryString["Type"] != null)
                type = Request.QueryString["Type"].ToString();
            ShowData();
            if (!IsPostBack)
            {
                Check_Person();
                Control_Enable();
                if (action == "View")
                {
                    LbtnYes.Visible = false;
                    LbtnNO.Visible = false;
                }
            }
        }

        private void ShowData()
        {
            string sqlText = "select * from View_TBDS_PS where ST_ID='" + id + "' and ST_PIDTYPE='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            DataRow dr = dt.Rows[0];
            lb_Name.Text = dr["ST_NAME"].ToString();
            lb_Id.Text = dr["ST_PER"].ToString();
            switch (type)
            {
                case "0":
                    sqlText = "转正评审";
                    ZZ_PS.Visible = true;
                    lb_Zheng.Text = dr["ST_CONT"].ToString();
                    break;
                case "1":
                    sqlText = "合同评审";
                    HT_PS.Visible = true;
                    lb_Start.Text = dr["ST_CONT"].ToString();
                    lb_End.Text = dr["ST_CONT1"].ToString();
                    break;
                case "2":
                    sqlText = "人员流动评审";
                    LD_PS.Visible = true;
                    lb_Zchu.Text = dr["DEP_ZCHU"].ToString();
                    lb_Zru.Text = dr["DEP_ZRU"].ToString();
                    lb_DepId.Text = dr["ST_CONT1"].ToString();
                    break;
                case "3":
                    sqlText = "年假评审";
                    NJ_PS.Visible = true;
                    lb_Holiday.Text = dr["ST_CONT"].ToString();
                    break;
                default:
                    break;
            }
            lb_Item.Text = sqlText;
            lb_Remark.Text = dr["ST_REMARK"].ToString();
            lb_Zdr.Text = dr["NAME"].ToString();
            txt_zdryj.Text = dr["ST_NOTE"].ToString();
            BindSelectReviewer();
        }

        private void Check_Person()
        {
            string sqltext = "select * from TBDS_PSDETAIL where ST_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该人员尚未登记，找不到相关信息！\\r\\r请相关负责人登记后再评审');", true); return;
            }
        }

        private void Control_Enable()
        {
            string username = Session["UserName"].ToString();
            //部门负责人
            string sqlbmfzr = "select a.*,b.DEP_NAME from TBDS_PSVIEW as a left join TBDS_DEPINFO as b on a.ST_DEP=b.DEP_CODE where a.ST_ID='" + id + "'and ST_PIDTYPE ='1' and  ST_DEP!='01'";
            DataTable dt_bmfzr = DBCallCommon.GetDTUsingSqlText(sqlbmfzr);
            for (int i = 0; i < dt_bmfzr.Rows.Count; i++)
            {
                Panel pal = new Panel();
                pal = (Panel)TabContainer1.FindControl("Pan_ShenHe").FindControl("Panel" + i.ToString());
                TextBox tbyj = (TextBox)TabContainer1.FindControl("Pan_ShenHe").FindControl("txt_shyj" + i.ToString());

                Label lb = (Label)TabContainer1.FindControl("Pan_ShenHe").FindControl("lb_shr" + i.ToString());
                string[] lb_text = lb.Text.Trim().Split(':');
                if (username == lb_text[1])
                {
                    pal.Enabled = true;
                    tbyj.BorderColor = System.Drawing.Color.Orange;
                    tbyj.Focus();
                    LbtnYes.Visible = true;
                    LbtnNO.Visible = true;
                }
                else
                {
                    pal.Enabled = false;
                }
            }

            //公司领导
            string sqlgsld = "select a.*,b.DEP_NAME from TBDS_PSVIEW as a left join TBDS_DEPINFO as b on a.ST_DEP=b.DEP_CODE where a.ST_ID='" + id + "'and ST_PIDTYPE ='1' and  ST_DEP='01'";
            DataTable dt_gsld = DBCallCommon.GetDTUsingSqlText(sqlgsld);
            for (int i = 0; i < dt_gsld.Rows.Count; i++)
            {
                Panel pal = new Panel();
                pal = (Panel)TabContainer1.FindControl("Pan_Leader").FindControl("Panel" + i.ToString());
                TextBox tbyj = (TextBox)TabContainer1.FindControl("Pan_Leader").FindControl("txt_shyj_ld" + i.ToString());

                Label lb = (Label)TabContainer1.FindControl("Pan_Leader").FindControl("lb_shr_ld" + i.ToString());
                string[] lb_text = lb.Text.Trim().Split(':');
                if (username == lb_text[1])
                {
                    //对于公司领导，审计通过后才能审核
                    string check_sjyj = "select distinct ST_PSYJ FROM TBDS_PSVIEW WHERE ST_ID='" + id + "'and ST_DEP<>'01' AND ST_PSYJ='0'";
                    DataTable dt_check_sjyj = DBCallCommon.GetDTUsingSqlText(check_sjyj);
                    if ((Session["UserDeptID"].ToString() == "01" && dt_check_sjyj.Rows.Count == 0) || Session["UserDeptID"].ToString() != "01")
                    {
                        pal.Enabled = true;
                        tbyj.BorderColor = System.Drawing.Color.Orange;
                        tbyj.Focus();
                        LbtnYes.Visible = true;
                        LbtnNO.Visible = true;
                    }
                    else
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

        #region 动态绑定信息

        private void BindSelectReviewer()
        {
            //部门负责人
            string sqltext = "select a.*,b.DEP_NAME,c.ST_NAME from TBDS_PSVIEW as a left join TBDS_DEPINFO as b on a.ST_DEP=b.DEP_CODE left join TBDS_STAFFINFO as c on a.ST_PID=c.ST_ID where a.ST_ID='" + id + "'and ST_PIDTYPE ='1' and  ST_DEP!='01'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
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
                lab1.Text = "审核部门:" + dt.Rows[i]["DEP_NAME"].ToString();// 审核部门
                lab1.Font.Bold = true;
                lab1.Width = 150;
                td1.Controls.Add(lab1);

                lab2.ID = "lb_shr" + i;
                lab2.Text = "审核人:" + dt.Rows[i]["ST_NAME"].ToString(); // 审核人
                lab2.Font.Bold = true;
                lab2.Width = 150;
                td1.Controls.Add(lab2);

                lab3.ID = "lb_shjg" + i;
                lab3.Text = dt.Rows[i]["ST_PSYJ"].ToString() == "1" ? "审核结论:未审批" : dt.Rows[i]["ST_PSYJ"].ToString() == "3" ? "审核结论:不同意" : "审核结论:同意";//审核结论 
                lab3.Font.Bold = true;
                lab3.Width = 150;
                td1.Controls.Add(lab3);

                lab4.ID = "lb_shsj" + i;
                lab4.Text = "审核时间:" + dt.Rows[i]["ST_SHSJ"].ToString(); //  审核时间
                lab4.Font.Bold = true;
                lab4.Width = 150;
                td1.Controls.Add(lab4);
                td1.ColumnSpan = 2;

                tr1.Cells.Add(td1);

                //第二行
                lab5.Text = "审核意见：";
                td21.Controls.Add(lab5);

                TextBox tb = new TextBox();
                tb.ID = "txt_shyj" + i;
                tb.Text = dt.Rows[i]["ST_NOTE"].ToString(); //审核意见
                tb.Rows = 4;
                tb.Columns = 100;
                tb.TextMode = TextBoxMode.MultiLine;

                if (dt.Rows[i]["ST_PSYJ"].ToString() == "1") //不同意
                    tb.BorderColor = System.Drawing.Color.Red;
                else if (dt.Rows[i]["ST_PSYJ"].ToString() == "2") //同意
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


            //领导
            sqltext = "select a.*,b.DEP_NAME,c.ST_NAME from TBDS_PSVIEW as a left join TBDS_DEPINFO as b on a.ST_DEP=b.DEP_CODE left join TBDS_STAFFINFO as c on a.ST_PID=c.ST_ID where a.ST_ID='" + id + "'and ST_PIDTYPE ='1' and  ST_DEP='01'";
            dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                #region 绑定领导意见

                Panel pal = new Panel();
                pal.ID = "Panel" + i;
                TableRow tr1 = new TableRow();
                TableCell td1 = new TableCell();

                TableRow tr2 = new TableRow();
                TableCell td21 = new TableCell();
                TableCell td22 = new TableCell();

                td21.Width = 100;

                Label lab2 = new Label();
                Label lab3 = new Label();
                Label lab4 = new Label();
                Label lab5 = new Label();

                //第一行
                lab2.ID = "lb_shr_ld" + i;
                lab2.Text = "审核人:" + dt.Rows[i]["ST_NAME"].ToString(); // 审核人
                lab2.Font.Bold = true;
                lab2.Width = 150;
                td1.Controls.Add(lab2);

                lab3.ID = "lb_shjg_ld" + i;
                lab3.Text = dt.Rows[i]["ST_PSYJ"].ToString() == "1" ? "审核结论:未审批" : dt.Rows[i]["ST_PSYJ"].ToString() == "3" ? "审核结论:不同意" : "审核结论:同意";//审核结论 
                lab3.Font.Bold = true;
                lab3.Width = 150;
                td1.Controls.Add(lab3);

                lab4.ID = "lb_shsj_ld" + i;
                lab4.Text = "审核时间:" + dt.Rows[i]["ST_SHSJ"].ToString(); //  审核时间
                lab4.Font.Bold = true;
                lab4.Width = 150;
                td1.Controls.Add(lab4);
                tr1.Cells.Add(td1);
                td1.ColumnSpan = 2;

                //第二行
                lab5.Text = "审核意见：";
                td21.Controls.Add(lab5);

                TextBox tb = new TextBox();
                tb.ID = "txt_shyj_ld" + i;
                tb.Text = dt.Rows[i]["ST_NOTE"].ToString(); //审核意见
                tb.Rows = 4;
                tb.Columns = 100;
                tb.TextMode = TextBoxMode.MultiLine;

                if (dt.Rows[i]["ST_PSYJ"].ToString() == "1") //不同意
                    tb.BorderColor = System.Drawing.Color.Red;
                else if (dt.Rows[i]["ST_PSYJ"].ToString() == "2") //同意
                    tb.BorderColor = System.Drawing.Color.Aqua;
                td22.Controls.Add(tb);
                tr2.Cells.Add(td21);
                tr2.Cells.Add(td22);

                Table tb_gsld = new Table();
                tb_gsld.Controls.Add(tr1);
                tb_gsld.Controls.Add(tr2);
                tb_gsld.CellPadding = 4;
                tb_gsld.CellSpacing = 1;
                tb_gsld.BorderWidth = 1;
                tb_gsld.CssClass = "toptable grid";
                tb_gsld.Attributes.Add("style", "width=100%");
                pal.Controls.Add(tb_gsld);
                Pan_gsld.Controls.Add(pal);
                #endregion
            }

        }

        #endregion

        #region  提交审核意见

        protected void btnYes_Click(object sender, EventArgs e)
        {
            ExeSQL(2);
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('审批成功！');window.location.href='OM_ZhuanZPS.aspx';", true);
        }

        protected void btnNO_Click(object sender, EventArgs e)
        {
            ExeSQL(3);
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('已驳回，等待制单人修改！');window.location.href='OM_ZhuanZPS.aspx';", true);
        }

        private void ExeSQL(int j)
        {
            #region 评审代码

            List<string> strb_sql = new List<string>();
            string psyj = "";
            string txt_yj = "";//意见文本框中的文字
            string riqi = DateTime.Now.ToString("yyyy-MM-dd");//评审时间
            string username = Session["UserName"].ToString();

            //评审人是部门负责人            
            string sqlText = "select *,b.DEP_NAME from TBDS_PSVIEW as a left join TBDS_DEPINFO as b on a.ST_DEP=b.DEP_CODE where ST_ID='" + id + "'and ST_PIDTYPE ='1' and  ST_DEP!='01'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Label lb = (Label)TabContainer1.FindControl("Pan_ShenHe").FindControl("lb_shr" + i.ToString());
                string[] lb_text = lb.Text.Trim().Split(':');
                if (username == lb_text[1])
                {
                    TextBox txt_shyj = (TextBox)TabContainer1.FindControl("Pan_ShenHe").FindControl("txt_shyj" + i.ToString());
                    txt_yj = txt_shyj.Text;
                }
            }

            //评审人是公司领导            
            sqlText = "select *,b.DEP_NAME from TBDS_PSVIEW as a left join TBDS_DEPINFO as b on a.ST_DEP=b.DEP_CODE where ST_ID='" + id + "'and ST_PIDTYPE ='1' and  ST_DEP='01'";
            DataTable dt_gsld = DBCallCommon.GetDTUsingSqlText(sqlText);
            for (int i = 0; i < dt_gsld.Rows.Count; i++)
            {
                Label lb = (Label)TabContainer1.FindControl("Pan_Leader").FindControl("lb_shr_ld" + i.ToString());
                string[] lb_text = lb.Text.Trim().Split(':');
                if (username == lb_text[1])
                {
                    TextBox txt_shyj = (TextBox)TabContainer1.FindControl("Pan_Leader").FindControl("txt_shyj_ld" + i.ToString());
                    txt_yj = txt_shyj.Text;
                }
            }
            if (j == 3)
            {
                if (txt_yj == "")
                {
                    if (Session["UserDeptID"].ToString() != "01")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('驳回意见不能为空！');", true); return;
                    }
                    psyj = txt_yj;
                }
                else
                {
                    psyj = txt_yj;
                }

            }
            else
            {
                if (txt_yj == "")
                {
                    psyj = "同意";
                }
                else
                {
                    psyj = txt_yj;
                }

            }

            #endregion

            string sql = "update TBDS_PSVIEW set ST_PSYJ='" + j.ToString() + "',ST_NOTE='" + psyj + "' ,ST_SHSJ='" + riqi + "' where ST_ID='" + id + "' and ST_PID='" + Session["UserID"].ToString() + "'";
            strb_sql.Add(sql);

            if (j == 2)
            {
                //部门评审完之后，提醒领导需要审批
                sql = "select * from TBDS_PSVIEW where ST_DEP!='01' and ST_PSYJ='1'";
                int jud = DBCallCommon.GetDTUsingSqlText(sql).Rows.Count;
                sql = "select * from TBDS_PSVIEW where ST_DEP='01' and ST_PSYJ!='1'";//领导审批之后不需要重复发送邮件
                DataTable leader = DBCallCommon.GetDTUsingSqlText(sql);
                int jud1 = leader.Rows.Count;
                string item = string.Empty;
                if (jud != 0 && jud1 == 0)
                {
                    switch (type)
                    {
                        case "0":
                            item = "人员转正";
                            break;
                        case "1":
                            item = "合同签订";
                            break;
                        case "2":
                            item = "人员流动";
                            break;
                        case "3":
                            item = "人员年假";
                            break;
                        default:
                            break;
                    }
                    for (int i = 0; i < jud1; i++)
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(leader.Rows[i]["ST_PID"].ToString()), new List<string>(), new List<string>(), "部门评审完毕", string.Format("您有{0}审批！", item));
                    }
                }
            }

            //修改评审状态 评审完成2，已驳回3,
            //根据类型来修改相应的表
            if (j == 3) //不同意
            {
                sql = "update TBDS_PSDETAIL set ST_PSZT='3' where ST_ID='" + id + "'";
                strb_sql.Add(sql);
                //发送邮件通知——审批驳回
                //SendMail("no");

            }
            else //同意 如果除此人以外其他人都同意了，则审批状态改为审批完成
            {
                sql = "select * from TBDS_PSVIEW where ST_ID='" + id + "' and ST_PID!='" + Session["UserID"].ToString() + "' and ST_PSYJ!='2'";
                DataTable dt_check = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt_check.Rows.Count == 0)
                {
                    sql = "update TBDS_PSDETAIL set ST_PSZT='2' where ST_ID='" + id + "'";
                    strb_sql.Add(sql);
                    if (type == "0")
                    {
                        sql = "update TBDS_STAFFINFO ST_ZHUANZ='1' where ST_ID='" + lb_Id.Text + "'";
                        strb_sql.Add(sql);
                    }
                    else if (type == "1")
                    {
                        sql = "update TBDS_STAFFINFO set ST_CONTRSTART='" + lb_Start.Text + "',ST_CONTREND='" + lb_End.Text + "' where ST_ID='" + lb_Id.Text + "'";
                        strb_sql.Add(sql);
                        sql = string.Format("insert into TBDS_HETONG values('{0}','{1}','{2}','{3}')", lb_Id.Text, lb_Start.Text, lb_End.Text, DateTime.Now.ToString());
                        strb_sql.Add(sql);
                    }
                    else if (type == "2")
                    {
                        sql = "update TBDS_STAFFINFO set ST_DEPID='" + lb_DepId.Text + "'";
                    }
                    //发送邮件通知——审批通过
                    //SendMail("yes");
                }
            }
            DBCallCommon.ExecuteTrans(strb_sql);
        }

        #endregion

        //返回
        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("OM_ZhuanZPS.aspx");
        }
    }
}
