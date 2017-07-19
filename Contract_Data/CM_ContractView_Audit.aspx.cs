using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_ContractView_Audit : System.Web.UI.Page
    {
        string id = string.Empty;
        string action = string.Empty;
        string content = string.Empty;
        string name = string.Empty;
        string reviewx = string.Empty;
        string review = string.Empty;
        string type = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["Action"] != null)
                action = Request.QueryString["action"].ToString();

            if (Request.QueryString["ID"] != null)
                id = Request.QueryString["ID"].ToString();

            if (Request.QueryString["Type"] != null)
                type = Request.QueryString["Type"].ToString();

            showInfo();//呈现合同基本信息,有动态生成控件，每次刷新页面都要执行
            this.InitUpload();
            if (!IsPostBack)
            {
                LbtnYes.Visible = false;
                LbtnNO.Visible = false;

                Control_All_Pal_Enable();

                //if (type == "1")
                //{
                //    tr_orderid.Visible = true;
                //}
                //else
                //{
                //    tr_orderid.Visible = false;
                //}
                //if (type == "0" && lb_tbcode.Text !="")
                //{
                //    tr_tbcode.Visible = true;
                //}
                //else
                //{
                //    tr_tbcode.Visible = false;
                //} 
                Contract_Data.ContractClass.UploadControlSet(UploadAttachments1, "View");
                if (type != "8")
                {
                    Check_HT();
                }
                //else
                //{
                //    //tr_htbh.Visible = false;
                //    tr_gcmc.Visible = false;
                //}
            }
        }

        //检查合同是否登记，若未登记，显示提示信息
        private void Check_HT()
        {
            string sqltext = "select * from TBPM_CONPCHSINFO where PCON_REVID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            sqltext = "select * from TBCM_ADDCON where CR_ID='" + id + "'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count == 0 && dt1.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该合同尚未登记，找不到相关信息！\\r\\r请相关负责人登记后再评审');", true); return;
            }
        }

        private void Control_All_Pal_Enable()
        {
            string username = Session["UserName"].ToString();

            //部门负责人
            string sqlbmfzr = "select DISTINCT ST_NAME,DEP_NAME,CRD_ID,CRD_PID,CRD_PSYJ,CRD_NOTE,CRD_SHSJ" +
           " ,CRD_DEP from VIEW_TBCR_CONTRACTVIEW_AUDIT where CRD_ID='" + id + "'and CRD_PIDTYPE ='1' and  CRD_DEP!='01'";
            DataTable dt_bmfzr = DBCallCommon.GetDTUsingSqlText(sqlbmfzr);
            for (int i = 0; i < dt_bmfzr.Rows.Count; i++)
            {
                Panel pal = new Panel();
                pal = (Panel)TabContainer1.FindControl("TabPanel2").FindControl("Panel" + i.ToString());
                TextBox tbyj = (TextBox)TabContainer1.FindControl("TabPanel2").FindControl("txt_shyj" + i.ToString());

                Label lb = (Label)TabContainer1.FindControl("TabPanel2").FindControl("lb_shr" + i.ToString());
                string[] lb_text = lb.Text.Trim().Split(':');
                if (username == lb_text[1] && action == "audit")
                {
                    //对于审计部的人，其他部门都通过后审计才能审核
                    string check_bmyj = "select distinct CRD_PSYJ FROM VIEW_TBCR_CONTRACTVIEW_AUDIT WHERE CRD_ID='" + id + "'and CRD_DEP not in ('01','14') AND CRD_PSYJ='0'";
                    DataTable dt_check_bmyj = DBCallCommon.GetDTUsingSqlText(check_bmyj);
                    if ((Session["UserDeptID"].ToString() == "14" && dt_check_bmyj.Rows.Count == 0) || Session["UserDeptID"].ToString() != "14")
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

            //公司领导
            string sqlgsld = "select DISTINCT ST_NAME,DEP_NAME,CRD_ID,CRD_PID,CRD_PSYJ,CRD_NOTE,CRD_SHSJ" +
           " ,CRD_DEP from VIEW_TBCR_CONTRACTVIEW_AUDIT where CRD_ID='" + id + "'and CRD_PIDTYPE ='1' and  CRD_DEP='01'";
            DataTable dt_gsld = DBCallCommon.GetDTUsingSqlText(sqlgsld);
            for (int i = 0; i < dt_gsld.Rows.Count; i++)
            {
                Panel pal = new Panel();
                pal = (Panel)TabContainer1.FindControl("TabPanel3").FindControl("Panel" + i.ToString());
                TextBox tbyj = (TextBox)TabContainer1.FindControl("TabPanel3").FindControl("txt_shyj_ld" + i.ToString());

                Label lb = (Label)TabContainer1.FindControl("TabPanel3").FindControl("lb_shr_ld" + i.ToString());
                string[] lb_text = lb.Text.Trim().Split(':');
                if (username == lb_text[1] && action == "audit")
                {
                    //对于公司领导，审计通过后才能审核
                    string check_sjyj = "select distinct CRD_PSYJ FROM VIEW_TBCR_CONTRACTVIEW_AUDIT WHERE CRD_ID='" + id + "'and CRD_DEP<>'01' AND CRD_PSYJ='0'";
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

        //初始化附件上传控件
        private void InitUpload()
        {
            if (LBpsdh.Text.Trim() != "")
            {
                UploadAttachments1.Visible = true;
                UploadAttachments1.at_type = type == "8" ? 2 : 0;
                UploadAttachments1.at_htbh = type == "8" ? LBpsdh.Text : lbl_UNID.Text;  //唯一编号
                UploadAttachments1.at_sp = 0;

            }
            else
            {
                UploadAttachments1.Visible = false;
            }
        }

        #region 显示Lable的信息，显示的是查看的信息

        private void showInfo()
        {
            string sqlselect = "select * from View_CM_CONTRACT_REV_INFO_ALL where cr_id='" + id + "'";
            DataSet ds = DBCallCommon.FillDataSet(sqlselect);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbl_UNID.Text = ds.Tables[0].Rows[0]["GUID"].ToString();//唯一编号

                LBpsdh.Text = ds.Tables[0].Rows[0]["CR_ID"].ToString();//评审单号

                lb_HTBH.Text = ds.Tables[0].Rows[0]["PCON_BCODE"].ToString();//合同号

                lb_XMMC.Text = ds.Tables[0].Rows[0]["CR_XMMC"].ToString();// 项目名称

                //lb_GCMC.Text = ds.Tables[0].Rows[0]["PCON_ENGNAME"].ToString();// 工程名称

                //lb_SBMC.Text = ds.Tables[0].Rows[0]["CR_SBMC"].ToString(); //  设备名称

                //lb_FBFW.Text = ds.Tables[0].Rows[0]["CR_FBFW"].ToString(); // 分包范围

                //lb_JE.Text = string.Format("{0:C}",Convert.ToDecimal(ds.Tables[0].Rows[0]["PCON_JINE"].ToString()));//  金额

                //lb_JE.Text = Convert.ToDecimal(ds.Tables[0].Rows[0]["PCON_JINE"].ToString()) == 0 ? string.Format("{0:C}", Convert.ToDecimal(ds.Tables[0].Rows[0]["CR_JIN"].ToString())) : string.Format("{0:C}", Convert.ToDecimal(ds.Tables[0].Rows[0]["PCON_JINE"].ToString()));
                //if (lb_HTBH.Text!=""&&Convert.ToDecimal(ds.Tables[0].Rows[0]["OTHER_MONUNIT"].ToString()) > 0)  //有其他币种时
                //{
                //    lb_OtherMonunit.Text = ds.Tables[0].Rows[0]["OTHER_MONUNIT"].ToString() + ds.Tables[0].Rows[0]["PCON_MONUNIT"].ToString();
                //}

                lb_FBS.Text = ds.Tables[0].Rows[0]["CR_FBSMC"].ToString();//业主供应商

                lb_ZDR.Text = ds.Tables[0].Rows[0]["ST_NAME"].ToString(); // 制单人                

                //lb_Orderid.Text = ds.Tables[0].Rows[0]["PCON_ORDERID"].ToString();//采购订单号

                //lb_tbcode.Text = ds.Tables[0].Rows[0]["PCON_TBCODE"].ToString(); //投标评审号（2012年12月13日 Meng）

                //lb_BZ.Text = ds.Tables[0].Rows[0]["CR_NOTE"].ToString();//备注 以前的审批和合同是分开的，现在合并了，审批表中的备注不要了
                lb_BZ.Text = ds.Tables[0].Rows[0]["PCON_NOTE"].ToString();//备注

                /********************************************/

                /********************************************/
                BindSelectReviewer();//绑定已选审批人信息

                if (type == "0")
                {
                    TabPanel4.Visible = true;
                    string sqlstr = "select * from TBPM_CONTRACTPS where ID='" + id + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlstr);

                    foreach (Control control in PanelAll.Controls)
                    {
                        if (control is TextBox)
                        {
                            ((TextBox)control).Text = dt.Rows[0][control.ID].ToString();
                        }
                    }

                    sqlstr = "select * from TBPM_HESUAN where ID='" + id + "'";
                    GridHeSuan.DataSource = DBCallCommon.GetDTUsingSqlText(sqlstr);
                    GridHeSuan.DataBind();
                }
            }
        }

        //根据选择的审核人动态生成审核意见
        private void BindSelectReviewer()
        {
            string sqltext = "select DISTINCT ST_NAME,DEP_NAME,CRD_ID,CRD_PID,CRD_PSYJ,CRD_NOTE,CRD_SHSJ" +
            " ,CRD_DEP,CRD_PIDTYPE from VIEW_TBCR_CONTRACTVIEW_AUDIT where CRD_ID='" + id + "'";
            //制单人
            string sqltext1 = "select DISTINCT ST_NAME,DEP_NAME,CRD_ID,CRD_PID,CRD_PSYJ,CRD_NOTE,CRD_SHSJ" +
           " ,CRD_DEP from VIEW_TBCR_CONTRACTVIEW_AUDIT where CRD_ID='" + id + "'and CRD_PIDTYPE ='0'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext1);
            if (dt1.Rows.Count > 0)
            {
                txt_zdryj.Text = dt1.Rows[0]["CRD_NOTE"].ToString();
            }
            //部门负责人
            string sqltext2 = "select DISTINCT ST_NAME,DEP_NAME,CRD_ID,CRD_PID,CRD_PSYJ,CRD_NOTE,CRD_SHSJ" +
           " ,CRD_DEP from VIEW_TBCR_CONTRACTVIEW_AUDIT where CRD_ID='" + id + "'and CRD_PIDTYPE ='1' and  CRD_DEP!='01'";
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext2);

            for (int i = 0; i < dt2.Rows.Count; i++)
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
                lab1.Text = "审核部门:" + dt2.Rows[i]["DEP_NAME"].ToString();// 审核部门
                lab1.Font.Bold = true;
                lab1.Width = 150;
                td1.Controls.Add(lab1);

                lab2.ID = "lb_shr" + i;
                lab2.Text = "审核人:" + dt2.Rows[i]["ST_NAME"].ToString(); // 审核人
                lab2.Font.Bold = true;
                lab2.Width = 150;
                td1.Controls.Add(lab2);

                lab3.ID = "lb_shjg" + i;
                lab3.Text = dt2.Rows[i]["CRD_PSYJ"].ToString() == "0" ? "审核结论:未审批" : dt2.Rows[i]["CRD_PSYJ"].ToString() == "1" ? "审核结论:不同意" : "审核结论:同意";//审核结论 
                lab3.Font.Bold = true;
                lab3.Width = 150;
                td1.Controls.Add(lab3);

                lab4.ID = "lb_shsj" + i;
                lab4.Text = "审核时间:" + dt2.Rows[i]["CRD_SHSJ"].ToString(); //  审核时间
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
                tb.Text = dt2.Rows[i]["CRD_NOTE"].ToString(); //审核意见
                tb.Rows = 4;
                tb.Columns = 100;
                tb.TextMode = TextBoxMode.MultiLine;

                if (dt2.Rows[i]["CRD_PSYJ"].ToString() == "1") //不同意
                    tb.BorderColor = System.Drawing.Color.Red;
                else if (dt2.Rows[i]["CRD_PSYJ"].ToString() == "2") //同意
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
                Pan_shenheren.Controls.Add(pal);
                #endregion


            }


            //领导
            string sqltext3 = "select DISTINCT ST_NAME,DEP_NAME,CRD_ID,CRD_PID,CRD_PSYJ,CRD_NOTE,CRD_SHSJ" +
          " ,CRD_DEP from VIEW_TBCR_CONTRACTVIEW_AUDIT where CRD_ID='" + id + "'and CRD_PIDTYPE ='1' and  CRD_DEP='01'";
            DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sqltext3);

            for (int i = 0; i < dt3.Rows.Count; i++)
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
                lab2.Text = "审核人:" + dt3.Rows[i]["ST_NAME"].ToString(); // 审核人
                lab2.Font.Bold = true;
                lab2.Width = 150;
                td1.Controls.Add(lab2);

                lab3.ID = "lb_shjg_ld" + i;
                lab3.Text = dt3.Rows[i]["CRD_PSYJ"].ToString() == "0" ? "审核结论:未审批" : dt3.Rows[i]["CRD_PSYJ"].ToString() == "1" ? "审核结论:不同意" : "审核结论:同意";//审核结论 
                lab3.Font.Bold = true;
                lab3.Width = 150;
                td1.Controls.Add(lab3);

                lab4.ID = "lb_shsj_ld" + i;
                lab4.Text = "审核时间:" + dt3.Rows[i]["CRD_SHSJ"].ToString(); //  审核时间
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
                tb.Text = dt3.Rows[i]["CRD_NOTE"].ToString(); //审核意见
                tb.Rows = 4;
                tb.Columns = 100;
                tb.TextMode = TextBoxMode.MultiLine;

                if (dt3.Rows[i]["CRD_PSYJ"].ToString() == "1") //不同意
                    tb.BorderColor = System.Drawing.Color.Red;
                else if (dt3.Rows[i]["CRD_PSYJ"].ToString() == "2") //同意
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
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('审批成功！');window.location.href='CM_MyContractReviewTask.aspx';", true);
        }
        protected void btnNO_Click(object sender, EventArgs e)
        {
            ExeSQL(1);
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('已驳回，等待制单人修改！');window.location.href='CM_MyContractReviewTask.aspx';", true);
        }
        private void ExeSQL(int j)
        {
            List<string> strb_sql = new List<string>();
            string psyj = "";
            string txt_yj = "";//意见文本框中的文字
            string riqi = DateTime.Now.ToString("yyyy-MM-dd");//评审时间
            string username = Session["UserName"].ToString();

            //评审人是部门负责人            
            string sqlbmfzr = "select DISTINCT ST_NAME,DEP_NAME,CRD_ID,CRD_PID,CRD_PSYJ,CRD_NOTE,CRD_SHSJ" +
           " ,CRD_DEP from VIEW_TBCR_CONTRACTVIEW_AUDIT where CRD_ID='" + id + "'and CRD_PIDTYPE ='1' and  CRD_DEP!='01'";
            DataTable dt_bmfzr = DBCallCommon.GetDTUsingSqlText(sqlbmfzr);
            for (int i = 0; i < dt_bmfzr.Rows.Count; i++)
            {
                Label lb = (Label)TabContainer1.FindControl("TabPanel2").FindControl("lb_shr" + i.ToString());
                string[] lb_text = lb.Text.Trim().Split(':');
                if (username == lb_text[1])
                {
                    TextBox txt_shyj = (TextBox)TabContainer1.FindControl("TabPanel2").FindControl("txt_shyj" + i.ToString());
                    txt_yj = txt_shyj.Text;
                }
            }

            //评审人是公司领导            
            string sqlgsld = "select DISTINCT ST_NAME,DEP_NAME,CRD_ID,CRD_PID,CRD_PSYJ,CRD_NOTE,CRD_SHSJ" +
           " ,CRD_DEP from VIEW_TBCR_CONTRACTVIEW_AUDIT where CRD_ID='" + id + "'and CRD_PIDTYPE ='1' and  CRD_DEP='01'";
            DataTable dt_gsld = DBCallCommon.GetDTUsingSqlText(sqlgsld);
            for (int i = 0; i < dt_gsld.Rows.Count; i++)
            {
                Label lb = (Label)TabContainer1.FindControl("TabPanel3").FindControl("lb_shr_ld" + i.ToString());
                string[] lb_text = lb.Text.Trim().Split(':');
                if (username == lb_text[1])
                {
                    TextBox txt_shyj = (TextBox)TabContainer1.FindControl("TabPanel3").FindControl("txt_shyj_ld" + i.ToString());
                    txt_yj = txt_shyj.Text;
                }
            }
            if (j == 1)
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
            string sql = "update TBCR_CONTRACTREVIEW_DETAIL set CRD_PSYJ='" + j.ToString() + "',CRD_NOTE='" + psyj + "'" +
                      " ,CRD_SHSJ='" + riqi + "' where CRD_ID='" + id + "' and CRD_PID='" + Session["UserID"].ToString() + "'";
            strb_sql.Add(sql);

            //修改合同评审状态 评审完成2，已驳回3,
            //根据合同类型来修改相应的表
            string tbname = type == "8" ? "" : "TBCR_CONTRACTREVIEW";
            if (j == 1) //不同意
            {
                string str_back = "";
                if (type == "8")
                {
                    str_back = "update TBCM_ADDCON set CON_PSZT='3' where CR_ID='" + id + "'";
                }
                else
                {
                    str_back = "update TBCR_CONTRACTREVIEW set CR_PSZT='3' where CR_ID='" + id + "'";
                }
                strb_sql.Add(str_back);
                //发送邮件通知——审批驳回
                SendMail("no");

            }
            else //同意 如果除此人以外其他人都同意了，则审批状态改为审批完成
            {
                string str_check_over = "select * from TBCR_CONTRACTREVIEW_DETAIL where CRD_ID='" + id + "'" +
                " and CRD_PID!='" + Session["UserID"].ToString() + "' and CRD_PSYJ!='2'";
                DataTable dt_check = DBCallCommon.GetDTUsingSqlText(str_check_over);
                if (dt_check.Rows.Count == 0)
                {
                    string str_over = "";
                    if (type == "8")
                    {
                        str_over = "update TBCM_ADDCON set CON_PSZT='2' where CR_ID='" + id + "'";
                    }
                    else
                    {
                        str_over = "update TBCR_CONTRACTREVIEW set CR_PSZT='2' where CR_ID='" + id + "'";
                    }
                    strb_sql.Add(str_over);

                    //审批完成后将对应的合同签订日期改为评审通过日期
                    if (type != "8")
                    {
                        string str_updatevalidate = "update TBPM_CONPCHSINFO set PCON_FILLDATE ='" + riqi + "' where PCON_REVID='" + id + "'";
                        strb_sql.Add(str_updatevalidate);
                    }

                    //发送邮件通知——审批通过
                    SendMail("yes");
                }
            }
            DBCallCommon.ExecuteTrans(strb_sql);
            string sqlcomcount = "select distinct(CRD_PID) from View_TBCR_View_Detail_ALL where  CRD_PID in ('1','2') and CR_PSZT in ('1','2') and CRD_PIDTYPE='1'and CR_ID='" + id + "'";
            DataTable dtcomcount = DBCallCommon.GetDTUsingSqlText(sqlcomcount);

            string sqlcom = "select CR_ID from View_TBCR_View_Detail_ALL where CRD_PSYJ='0' and CRD_PID  in ('1','2') and CR_PSZT in ('1','2') and CRD_PIDTYPE='1'and CR_ID='" + id + "'";
            DataTable dtcom = DBCallCommon.GetDTUsingSqlText(sqlcom);
            if (dtcom.Rows.Count > 0)
            {
                if (dtcomcount.Rows.Count == dtcom.Rows.Count)
                {
                    string sqldep = "select distinct(CR_ID) from View_TBCR_View_Detail_ALL where CRD_PSYJ='0' and CRD_PID not in ('1','2') and CR_PSZT in ('1','2') and CRD_PIDTYPE='1'and CR_ID='" + id + "'";
                    DataTable dtdep = DBCallCommon.GetDTUsingSqlText(sqldep);
                    if (dtdep.Rows.Count == 0)
                    {
                        string _body = "合同审批任务:"
                             + "\r\n合同号：" + lb_HTBH.Text.Trim()
                             + "\r\n项目名称：" + lb_XMMC.Text.Trim();
                        string _subject = "您有新的【合同】需要审批，请及时处理";
                        string _emailto = "";
                        for (int k = 0; k < dtcom.Rows.Count; k++)
                        {
                            _emailto = DBCallCommon.GetEmailAddressByUserID(dtcomcount.Rows[k][0].ToString());
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                    }
                }
            }
        }

        #endregion
        /***********************************绑定其是否可以评审**************************************************/

        //返回
        protected void btn_back_Click(object sender, EventArgs e)
        {
            if (action == "view")
            {
                if (Request.QueryString["Win"] == null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "window.close();", true);
                }
                else
                {
                    Response.Redirect("CM_ContractView.aspx");
                }
            }
            else
            {
                Response.Redirect("CM_MyContractReviewTask.aspx");
            }

        }

        //查看订单
        //protected void btn_ViewOrder_Click(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "View_PurOrder('"+lb_Orderid.Text+"');", true);
        //}

        //查看投标信息
        //protected void btn_ViewBid_Click(object sender, EventArgs e)
        //{
        //    string bidnum = "";
        //    string bidsql = "select BC_ID from TBBS_CONREVIEW where BC_CODE='" + lb_tbcode.Text + "'";
        //    DataTable dr = DBCallCommon.GetDTUsingSqlText(bidsql);
        //    if (dr.Rows.Count > 0)
        //    {
        //        bidnum = dr.Rows[0][0].ToString();            
        //    }
        //    //Response.Redirect("~/CM_Data/PD_DocpinshenInfo.aspx?action=look&id=" + bidnum );
        //    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "View_Bid('" + bidnum + "');", true);
        //}

        //发送邮件

        private void SendMail(string yesno)
        {
            string subject = "";
            string result = "";
            List<string> copyto = new List<string>();
            copyto.Add("erp@ztsm.net");
            if (yesno == "no")
            {
                subject = "已驳回合同审批";
                result = "被驳回";
            }
            else if (yesno == "yes")
            {
                subject = "合同审批已通过";
                result = "审批通过";
            }

            if (lb_HTBH.Text.Trim() != "")
            {
                subject += "——" + lb_HTBH.Text.Trim();
            }
            string body = "您提交的合同审批于" + DateTime.Now.ToString() + result + "，请您知悉并处理" +
                        "\r\n合同号：" + lb_HTBH.Text.ToString() +
                        "\r\n项目名称：" + lb_XMMC.Text.Trim() +
                //"\r\n工程名称：" + lb_GCMC.Text.Trim() +
                        "\r\n签订单位：" + lb_FBS.Text.Trim();
            string sql = "select DISTINCT [EMAIL] from TBDS_STAFFINFO where ST_NAME='" + lb_ZDR.Text.Trim() + "'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                if (lb_ZDR.Text.Trim() == "李文超")
                {
                    copyto.Add("changlifu@cbmi.com.cn");
                }
                DBCallCommon.SendEmail(dt.Rows[0][0].ToString(), copyto, null, subject, body);
            }
        }
    }
}
