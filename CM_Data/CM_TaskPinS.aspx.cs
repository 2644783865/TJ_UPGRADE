using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_TaskPinS : System.Web.UI.Page
    {
        string id = string.Empty;
        string action = string.Empty;
        string content = string.Empty;
        string name = string.Empty;
        string reviewx = string.Empty;
        string review = string.Empty;
        List<string> str = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
                id = Request.QueryString["id"].Trim().ToString();//识别号
            if (Request.QueryString["action"] != null)
                action = Request.QueryString["action"].ToString();
            BindSelectReviewer();
            if (!IsPostBack)
            {
                UserID.Value = Session["UserID"].ToString();
                showInfo();//呈现合同基本信息
                Check_Person();
                Control_Enable();
                if (action == "look")
                {
                    LbtnYes.Visible = false;
                    LbtnNO.Visible = false;
                }
            }
        }

        #region 显示Lable的信息，显示的是查看的信息

        private void showInfo()
        {
            string sqlselect = "select a.*,b.ST_NAME from TBCM_PLAN as a left join TBDS_STAFFINFO as b on b.ST_ID=a.CM_MANCLERK where ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlselect);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                foreach (Control item in Panel.Controls)
                {
                    if ((item is Label || item is TextBox) && item.ID != "filesError")
                    {
                        ((ITextControl)item).Text = dr[item.ID].ToString();
                    }
                }
                CM_MANCLERK.Text = dr["ST_NAME"].ToString();
                string sqldata = "select a.*,b.CM_REFER as TSA_TYPENAME from TBCM_BASIC as a left join TBCM_TYPE as b on a.TSA_TYPE=b.CM_TYPE where ID='" + id + "' order by a.TSA_ID,a.CM_ID asc";
                DataTable data = DBCallCommon.GetDTUsingSqlText(sqldata);
                Repeater1.DataSource = data;
                Repeater1.DataBind();
                GVBind(GridView1, "tb_files", dr["CM_CONTEXT"].ToString());
                lb_Zdr.Text = dr["ST_NAME"].ToString();
                sqlselect = "select CM_NOTE from TBCM_PSVIEW where CM_ID='" + id + "' and CM_PIDTYPE='0'";
                data = DBCallCommon.GetDTUsingSqlText(sqlselect);
                if (data.Rows.Count > 0)
                {
                    txt_zdryj.Text = data.Rows[0][0].ToString();
                }
            }
        }

        #endregion

        private void Check_Person()
        {
            string sqltext = "select * from TBCM_PLAN where ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该计划单未登记，找不到相关信息！\\r\\r请相关负责人登记后再评审');", true); return;
            }
        }

        private void Control_Enable()
        {
            string sql = "select * from TBCM_PLAN where ID='" + id + "'";
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
                        break;
                    }
                    else
                    {
                        pal.Enabled = false;
                    }
                }
            }
        }

        #region 动态绑定信息

        private void BindSelectReviewer()
        {
            List<List<string>> dic = new List<List<string>>();
            //绑定市场部的评审人
            string sqltext = "select * from View_CM_Task where ID='" + id + "'";
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

        #endregion

        #region  提交审核意见

        protected void btnYes_Click(object sender, EventArgs e)
        {
            ExeSQL(2);
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('审批成功！');window.location.href='CM_PJinfo.aspx';", true);
        }

        protected void btnNO_Click(object sender, EventArgs e)
        {
            ExeSQL(3);
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('已驳回，等待制单人修改！');window.location.href='CM_PJinfo.aspx';", true);
        }

        private void ExeSQL(int j)
        {
            List<string> str_sql = new List<string>();
            string sql = "";
            string psyj = "";
            string txt_yj = "";//意见文本框中的文字
            string riqi = DateTime.Now.ToString("yyyy-MM-dd");//评审时间
            //评审人意见
            sql = "select * from TBCM_PLAN where ID='" + id + "'";
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
                        break;
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
                    //2016.9.20
                    //评审完之后，邮件提示下一个人有计划单需要审批
                    //if (psr < 3 && psr < n)
                    //{
                    //    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(dt.Rows[0]["CM_PS" + (psr + 1)].ToString()), new List<string>(), new List<string>(), "经营计划单评审", "您有经营计划单需要评审！");
                    //}

                    if (psr == n && dt.Rows[0]["CM_SPSTATUS"].ToString() == "1")
                    {
                        sql = "update TBCM_PLAN set CM_SPSTATUS='2' where ID='" + id + "'";
                        DBCallCommon.ExeSqlText(sql);
                       // str_sql.Add(sql);

                        #region 存入各表

                        //计划单审核通过插入销售合同
                        string jhdtype = "";
                        string sqlstid = "select TSA_ID from TBPM_DETAIL where CM_CONTR='" + CM_CONTR.Text.Trim() + "'";
                        DataTable stiddata = DBCallCommon.GetDTUsingSqlText(sqlstid);
                        if (stiddata.Rows.Count > 0)
                            jhdtype = "2";
                        else
                            jhdtype = "-1";
                        string sqlshdata = "select a.*,b.CM_REFER as TSA_TYPENAME from TBCM_BASIC as a left join TBCM_TYPE as b on a.TSA_TYPE=b.CM_TYPE where ID='" + id + "' order by a.TSA_ID,a.CM_ID asc";
                        DataTable shdata = DBCallCommon.GetDTUsingSqlText(sqlshdata);
                        for (int i = 0; i < shdata.Rows.Count; i++)
                        {
                            string TSA_ID = shdata.Rows[i]["TSA_ID"].ToString();
                            string TSA_ENGNAME = shdata.Rows[i]["TSA_ENGNAME"].ToString();
                            string TSA_MAP = shdata.Rows[i]["TSA_MAP"].ToString();
                            string TSA_NUMBER = shdata.Rows[i]["TSA_NUMBER"].ToString();
                            string TSA_UNIT = shdata.Rows[i]["TSA_UNIT"].ToString();
                            string TSA_MATERIAL = shdata.Rows[i]["TSA_MATERIAL"].ToString();

                            string sqltext = "insert into TBPM_DETAIL(CM_CONTR,TSA_ID,CM_PROJ,CM_YZHTH,CM_ENGNAME,CM_MATERIAL,CM_MAP,CM_NUMBER,CM_UNIT,CM_JHDTYPE) values('" + CM_CONTR.Text.Trim() + "','" + TSA_ID.Trim() + "','" + CM_PROJ.Text.Trim() + "','" + CM_DFCONTR.Text.Trim() + "','" + TSA_ENGNAME.Trim() + "','" + TSA_MATERIAL.Trim() + "','" + TSA_MAP.Trim() + "','" + TSA_NUMBER.Trim() + "','" + TSA_UNIT.Trim() + "','" + jhdtype + "')";
                            str_sql.Add(sqltext);
                        }

                        //string cs = "select CM_PID from TBCM_PSVIEW where CM_ID='" + id + "' and CM_PIDTYPE='1'";
                        //DataTable cs_dt = DBCallCommon.GetDTUsingSqlText(cs);
                        //List<string> cs_list = new List<string>();
                        //for (int i = 0; i < cs_dt.Rows.Count; i++)
                        //{
                        //    cs_list.Add(DBCallCommon.GetEmailAddressByUserID(cs_dt.Rows[i][0].ToString()));
                        //}
                        //for (int i = 0; i < cs_list.Count; i++)
                        //{
                        //    DBCallCommon.SendEmail(cs_list[i], new List<string>(), new List<string>(), "经营计划单查看", "您有经营计划单需要查看！,合同号为" + CM_CONTR.Text);
                        //}

                        string engname = string.Empty;
                        sql = "select TSA_ID,TSA_ENGNAME,TSA_TYPE from TBCM_BASIC where ID='" + id + "' order by TSA_ID";
                        DataTable task = DBCallCommon.GetDTUsingSqlText(sql);
                        List<string> liststr = new List<string>();
                        string fhdate = dt.Rows[0]["CM_FHDATE"].ToString();
                        string dhpart = dt.Rows[0]["CM_COMP"].ToString();
                        if (task.Rows.Count > 0)
                        {
                            //for (int i = 0; i < task.Rows.Count; i++)
                            //{
                            //    liststr.Contains(task.Rows[i][0].ToString());
                            //}
                            //liststr = liststr.Distinct<string>().ToList();
                            liststr.Add(task.Rows[0][0].ToString());
                            string[] engs;
                            for (int i = 0; i < task.Rows.Count; i++)
                            {
                                if (liststr.Contains(task.Rows[i][0].ToString()))
                                {
                                    engname += "/" + task.Rows[i]["TSA_ENGNAME"].ToString();
                                }
                                else
                                {
                                    liststr.Add(task.Rows[i][0].ToString());
                                    engs = engname.Substring(1).Split('/');
                                    if (engs.Length > 3)
                                    {
                                        engname = "/" + engs[0] + "/" + engs[1] + "/" + engs[2] + "/...";
                                    }
                                    str_sql.Add(string.Format("insert into TBCB_BMCONFIRM(TASK_ID,PRJ,ENG,HT_ID) values('{0}','{1}','{2}','{3}')", task.Rows[i - 1][0], CM_PROJ.Text, engname.Substring(1), CM_CONTR.Text));
                                    str_sql.Add(string.Format("insert into TBPM_TCTSASSGN(ID,TSA_ID,TSA_PJID,TSA_ENGNAME,TSA_BUYER,TSA_CONTYPE,TSA_DELIVERYTIME,TSA_DESIGNCOM) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", id, task.Rows[i - 1][0], CM_CONTR.Text, engname.Substring(1), CM_COMP.Text, task.Rows[i - 1]["TSA_TYPE"].ToString(), fhdate, dhpart));
                                    str_sql.Add(string.Format("insert into TBMP_MANUTSASSGN(MTA_ID,MTA_PJID,MTA_ENGNAME) values('{0}','{1}','{2}')", task.Rows[i - 1][0], CM_CONTR.Text, engname.Substring(1)));
                                    str_sql.Add(string.Format("insert into TBQM_QTSASSGN(QSA_ENGID) values('{0}')", task.Rows[i - 1][0]));
                                    str_sql.Add(string.Format("insert into TBMP_MAINPLANTOTAL(MP_CODE,MP_PJID,MP_PJNAME,MP_ENGNAME,MP_STARTDATE,MP_DELIVERYDATE) values('{0}','{1}','{2}','{3}','{4}','{5}')", task.Rows[i - 1][0], CM_CONTR.Text, CM_PROJ.Text, engname.Substring(1), DateTime.Now.ToString("yyyy-MM-dd"), fhdate));
                                    str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','材料计划','技术准备',0)", task.Rows[i - 1][0]));
                                    str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','制作明细','技术准备',0)", task.Rows[i - 1][0]));
                                    str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','成品入库','生产周期',0)", task.Rows[i - 1][0]));
                                    str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','非定尺板','采购周期',0)", task.Rows[i - 1][0]));
                                    str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','定尺板','采购周期',0)", task.Rows[i - 1][0]));
                                    str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','耐磨板','采购周期',0)", task.Rows[i - 1][0]));
                                    str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','型材','采购周期',0)", task.Rows[i - 1][0]));
                                    str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','圆钢','采购周期',0)", task.Rows[i - 1][0]));
                                    str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','标准件','采购周期',0)", task.Rows[i - 1][0]));
                                    str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','铸件','采购周期',0)", task.Rows[i - 1][0]));
                                    str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','锻件','采购周期',0)", task.Rows[i - 1][0]));
                                    str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','采购成品','采购周期',0)", task.Rows[i - 1][0]));
                                    engname = "/" + task.Rows[i]["TSA_ENGNAME"].ToString();
                                }
                            }
                            engs = engname.Substring(1).Split('/');
                            if (engs.Length > 3)
                            {
                                engname = "/" + engs[0] + "/" + engs[1] + "/" + engs[2] + "/...";
                            }
                            str_sql.Add(string.Format("insert into TBCB_BMCONFIRM(TASK_ID,PRJ,ENG,HT_ID) values('{0}','{1}','{2}','{3}')", task.Rows[task.Rows.Count - 1][0], CM_PROJ.Text, engname.Substring(1), CM_CONTR.Text));
                            str_sql.Add(string.Format("insert into TBPM_TCTSASSGN(ID,TSA_ID,TSA_PJID,TSA_ENGNAME,TSA_BUYER,TSA_CONTYPE,TSA_DELIVERYTIME,TSA_DESIGNCOM) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", id, task.Rows[task.Rows.Count - 1][0], CM_CONTR.Text, engname.Substring(1), CM_COMP.Text, task.Rows[task.Rows.Count - 1]["TSA_TYPE"].ToString(), fhdate, dhpart));
                            str_sql.Add(string.Format("insert into TBMP_MANUTSASSGN(MTA_ID,MTA_PJID,MTA_ENGNAME) values('{0}','{1}','{2}')", task.Rows[task.Rows.Count - 1][0], CM_CONTR.Text, engname.Substring(1)));
                            str_sql.Add(string.Format("insert into TBQM_QTSASSGN(QSA_ENGID) values('{0}')", task.Rows[task.Rows.Count - 1][0]));
                            str_sql.Add(string.Format("insert into TBMP_MAINPLANTOTAL(MP_CODE,MP_PJID,MP_PJNAME,MP_ENGNAME,MP_STARTDATE,MP_DELIVERYDATE) values('{0}','{1}','{2}','{3}','{4}','{5}')", task.Rows[task.Rows.Count - 1][0], CM_CONTR.Text, CM_PROJ.Text, engname.Substring(1), DateTime.Now.ToString("yyyy-MM-dd"), fhdate));//
                            str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','材料计划','技术准备',0)", task.Rows[0][0]));
                            str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','制作明细','技术准备',0)", task.Rows[0][0]));
                            str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','成品入库','生产周期',0)", task.Rows[0][0]));
                            str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','非定尺板','采购周期',0)", task.Rows[0][0]));
                            str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','定尺板','采购周期',0)", task.Rows[0][0]));
                            str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','耐磨板','采购周期',0)", task.Rows[0][0]));
                            str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','型材','采购周期',0)", task.Rows[0][0]));
                            str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','圆钢','采购周期',0)", task.Rows[0][0]));
                            str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','标准件','采购周期',0)", task.Rows[0][0]));
                            str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','铸件','采购周期',0)", task.Rows[0][0]));
                            str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','锻件','采购周期',0)", task.Rows[0][0]));
                            str_sql.Add(string.Format("insert into TBMP_MAINPLANDETAIL(MP_ENGID,MP_PLNAME,MP_TYPE,MP_STATE) values('{0}','采购成品','采购周期',0)", task.Rows[0][0]));
                        }

                        #endregion
                    }

                    //评审完之后，邮件提示下一个人有计划单需要审批，全部结束，发送抄送人
                    if (psr < 3 && psr < n)
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(dt.Rows[0]["CM_PS" + (psr + 1)].ToString()), new List<string>(), new List<string>(), "经营计划单评审", "您有经营计划单需要评审！");
                    }

                    if (psr == n && dt.Rows[0]["CM_SPSTATUS"].ToString() == "1")
                    {
                        string cs = "select CM_PID from TBCM_PSVIEW where CM_ID='" + id + "' and CM_PIDTYPE='1'";
                        DataTable cs_dt = DBCallCommon.GetDTUsingSqlText(cs);
                        List<string> cs_list = new List<string>();
                        for (int i = 0; i < cs_dt.Rows.Count; i++)
                        {
                            cs_list.Add(DBCallCommon.GetEmailAddressByUserID(cs_dt.Rows[i][0].ToString()));
                        }
                        for (int i = 0; i < cs_list.Count; i++)
                        {
                            DBCallCommon.SendEmail(cs_list[i], new List<string>(), new List<string>(), "经营计划单查看", "您有经营计划单需要查看！,合同号为" + CM_CONTR.Text);
                        }
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
                    sql = "update TBCM_PLAN set CM_SPSTATUS='3' where ID='"+id+"'";
                    //sql = "update TBCM_PLAN set CM_SPSTATUS='3' where ID='"+id+"'";
                    str_sql.Add(sql);
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(dt.Rows[0]["CM_MANCLERK"].ToString()), new List<string>(), new List<string>(), "经营计划单被驳回", "您提交的经营计划单被驳回！请修改后再次提交");
                }
                sql = string.Format("update TBCM_PLAN set CM_PSYJ{0}='{1}',CM_PSSJ{0}='{2}',CM_NOTE{0}='{3}' where ID='{4}'", psr, j, riqi, psyj, id);
                str_sql.Add(sql);
                DBCallCommon.ExecuteTrans(str_sql);
            }
        }

        #endregion

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("CM_PJinfo.aspx");
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.ToString() != "Header")
            {
                //取消计划单变色
                string TSA_CANCEL = ((HiddenField)e.Item.FindControl("TSA_CANCEL")).Value;
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("tr1");
                if (TSA_CANCEL == "1")
                {
                    tr.BgColor = "#FF0000";
                    tr.Attributes.Add("title", "红色背景行为该计划单取消项目");
                }
                if (str.Count < 1)
                {
                    str.Add(((DataRowView)e.Item.DataItem).Row[2].ToString());
                }
                else
                {
                    if (str.Contains(((DataRowView)e.Item.DataItem).Row[2].ToString()))
                    {
                        ((DataRowView)e.Item.DataItem).Row[2] = "";
                        ((DataRowView)e.Item.DataItem).Row["TSA_TYPENAME"] = "";
                        e.Item.DataBind();
                    }
                    else
                    {
                        str.Add(((DataRowView)e.Item.DataItem).Row[2].ToString());
                    }
                }
                ((DataRowView)e.Item.DataItem).Row.AcceptChanges();
            }
        }

        protected void imgbtnDF_Init(object sender, EventArgs e)
        {
            ToolkitScriptManager1.RegisterPostBackControl((Control)sender);
        }

        #region 下载文件

        protected void GVBind(GridView gv, string table, string context)
        {
            string sql = "select * from " + table + " where BC_CONTEXT='" + context + "'";
            DataSet ds = DBCallCommon.FillDataSet(sql);
            gv.DataSource = ds.Tables[0];
            gv.DataBind();
            gv.DataKeyNames = new string[] { "fileID" };
        }

        protected void imgbtnDF_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtn = (ImageButton)sender;
            GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
            GridView gv = (GridView)TabContainer1.FindControl("TabPanel1").FindControl("GridView1");
            string sqlStr = "select fileload,fileName,showName from " + ((ImageButton)sender).ID + " where fileID='" + gv.DataKeys[gvr.RowIndex].Value.ToString() + "'";
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            string strFilePath = ds.Tables[0].Rows[0]["fileload"].ToString() + @"\" + ds.Tables[0].Rows[0]["fileName"].ToString();
            if (File.Exists(strFilePath))
            {
                string showName = ds.Tables[0].Rows[0]["showName"].ToString();
                System.IO.FileInfo file = new System.IO.FileInfo(strFilePath);
                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(showName));
                Response.AppendHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(file.FullName);
                Response.Flush();
                Response.End();
            }
            else
            {
                filesError.Visible = true;
                filesError.Text = "文件已被删除，请通知相关人员上传文件！";
            }
        }

        #endregion
    }
}
