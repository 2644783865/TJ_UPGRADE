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
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_Inspection_Add : System.Web.UI.Page
    {
        string flag = "";
        string id = "";
        string nm = "";

        protected void Page_Load(object sender, EventArgs e)
        {


            if (Request.QueryString["ACTION"] != null)
            {
                flag = Request.QueryString["ACTION"];
            }

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }

            if (Request.QueryString["NUM"] != null)
            {
                nm = Request.QueryString["NUM"];
            }

            if (!IsPostBack)
            {

                HiddenFieldContent.Value = System.Guid.NewGuid().ToString();//用于质检上传文件
                InitVar();

               ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;

            }

            GridView_BindProcessFile();

        }

        private void InitVar()
        {

            if (flag == "NEW")
            {
                hfduniqueid.Value = System.Guid.NewGuid().ToString();//用于报检父子条目同时插入

                BindNewInfo();
                Submit.Text = "提交";

                Panel_Inspec.Visible = false;
                Panel_Process.Visible = false;//过程检验不可用
            }

            if (flag == "UPDATE")
            {
                BindUpdateInfo();

                BindProcessResult();

            }

            if (flag == "VIEW")
            {
                BindViewInfo();
                InspecNewResult();
                if (LabelState.Text.ToString() == "1")
                {
                    //已质检
                    //绑定最新的质检结果
                    Panel_Inspec.Visible = true;//最新记录不可见
                }
                else
                {
                    //未质检
                    Panel_Inspec.Visible = false;//最新记录不可见
                }
                ControlVisual(false);//控制上传控件
                Submit.Enabled = false;
                //Cancel.Enabled = false;
                BindProcessResult();

            }

            if (flag == "INSPEC")
            {
                BindViewInfo();
                Submit.Text = "质检";
                LabelRecoder.Text = Session["UserName"].ToString();
                BindProcessResult();
            }

            if (flag == "INUPDATE")
            {
                LabelNotes.Visible = true;

                BindInUpdateInfo();
                BindProcessResult();
            }

        }
        private void BindUpdateInfo()
        {
            if (nm == "0")
            {
                BindViewInfo();
                Panel_Inspec.Visible = false;//最新记录不可见
                Submit.Text = "保存";
            }
            else
            {
                BindViewInfo();
                //绑定质检记录
                InspecNewResult();
                Panel_Inspec.Visible = true;//最新记录可见
                ControlVisual(false);//上传控件不可见
                Submit.Text = "再次报检";
            }

        }

        private void BindInUpdateInfo()
        {
            if (nm == "0")
            {
                //修改质检
                BindViewInfo();
                InspecNewResult();
                Panel_Inspec.Visible = true;//最新记录可见
                Submit.Text = "修改质检";
            }
            else
            {
                //重新质检

                BindViewInfo();

                //绑定质检记录
                //InspecNewResult();

                Panel_Inspec.Visible = true;//最新记录可见

                Submit.Text = "重新质检";
            }
        }
        private void HeaderInfo()
        {
            string sql = "select AFI_TSDEP,AFI_PJNAME,AFI_ENGID,AFI_ENGNAME,AFI_PARTNAME,AFI_DATACLCT,left(isnull(AFI_DATE,''),10) as AFI_DATE,AFI_RQSTCDATE,AFI_SUPPLERNM,AFI_ISPCTSITE,AFI_CONTACT,AFI_CONTEL,AFI_MAN,AFI_MANNM,AFI_NOTE,AFI_NUMBER,AFI_STATE,AFI_QCMAN,AFI_QCMANNM,AFI_ASSGSTATE,UNIQUEID from TBQM_APLYFORINSPCT  where AFI_ID='" + id + "'";

            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                LabelEngID.Text = dt.Rows[0]["AFI_ENGID"].ToString();
                LabelProName.Text = dt.Rows[0]["AFI_PJNAME"].ToString();
                LabelEngName.Text = dt.Rows[0]["AFI_ENGNAME"].ToString();
                LabelSupplier.Text = dt.Rows[0]["AFI_SUPPLERNM"].ToString();
                LabelDep.Text = dt.Rows[0]["AFI_TSDEP"].ToString();
                LabelMan.Text = dt.Rows[0]["AFI_MAN"].ToString();
                LabelManName.Text = dt.Rows[0]["AFI_MANNM"].ToString();
                TextBoxPartName.Text = dt.Rows[0]["AFI_PARTNAME"].ToString();
                LabelSupplier.Text = dt.Rows[0]["AFI_SUPPLERNM"].ToString();
                TextBoxSite.Text = dt.Rows[0]["AFI_ISPCTSITE"].ToString();
                TextBoxData.Text = dt.Rows[0]["AFI_DATACLCT"].ToString();
                TextBoxContracter.Text = dt.Rows[0]["AFI_CONTACT"].ToString();
                TextBoxTel.Text = dt.Rows[0]["AFI_CONTEL"].ToString();
                TextBoxDate.Text = dt.Rows[0]["AFI_RQSTCDATE"].ToString();
                TextBoxNote.Text = dt.Rows[0]["AFI_NOTE"].ToString();
                LabelQCMan.Text = dt.Rows[0]["AFI_QCMANNM"].ToString();
                HiddenFieldQCUniqueID.Value = dt.Rows[0]["UNIQUEID"].ToString();
                LabelState.Text = dt.Rows[0]["AFI_STATE"].ToString();//状态
                TextBoxBJData.Text = dt.Rows[0]["AFI_DATE"].ToString();//报检时间
                LabelZJBH.Visible = true;
                LabelZJBH.Text = "质检编号：" + id.PadLeft(5, '0');
            }

        }


        private void BindNewInfo()
        {
            string sql = "select top(1) engid,pjnm,engnm,suppliernm,phono,conname from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where PO_OperateState='PUSH" + Session["UserID"].ToString() + "'";

            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                LabelEngID.Text = dt.Rows[0]["engid"].ToString();
                LabelProName.Text = dt.Rows[0]["pjnm"].ToString();
                LabelEngName.Text = dt.Rows[0]["engnm"].ToString();
                LabelSupplier.Text = dt.Rows[0]["suppliernm"].ToString();
                TextBoxContracter.Text = dt.Rows[0]["conname"].ToString();
                TextBoxTel.Text = dt.Rows[0]["phono"].ToString();
            }

            TextBoxBJData.Text = System.DateTime.Now.ToString("yyyy-MM-dd");//报检时间

            LabelDep.Text = Session["UserDept"].ToString();
            LabelMan.Text = Session["UserID"].ToString();
            LabelManName.Text = Session["UserName"].ToString();
            sql = "select '" + System.DateTime.Now.ToString("yyyy-MM-dd") + "' AS BJSJ,'' AS ID,ptcode as PTC,marnm,PO_TUHAO as tuhao,margg,margb,marcz,num as cgnum,cast(fznum/num as decimal(18,4)) as singlewgh,fznum as sumwgh,num as bjnum,'' as CONT,'' as JHSTATE,'' as QCNUM, '' as ZJREPORT,'' as ZGCONTENT, '' as RESULT, '' as QCMANNM,'' as QCDATE,'1' as ISAGAIN,'0' as ISRESULT from View_TBPC_PURORDERDETAIL_PLAN_TOTAL  where PO_OperateState='PUSH" + Session["UserID"].ToString() + "'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                RepeaterItem.DataSource = dt;
                RepeaterItem.DataBind();
            }
            sql = "UPDATE TBPC_PURORDERDETAIL SET PO_OperateState=NULL WHERE  PO_OperateState='PUSH" + Session["UserID"].ToString() + "'";

            DBCallCommon.ExeSqlText(sql);
        }

        private void BindViewInfo()
        {
            HeaderInfo();
            GetTotalAmount();
            string sql = "select ID,PTC,PARTNM AS marnm,TUHAO AS tuhao,margg,margb,marcz,NUM AS cgnum,DANZHONG AS singlewgh,ZONGZHONG AS sumwgh,PJNUM as bjnum,CONT,JHSTATE,QCNUM,ZJREPORT,ZGCONTENT,isnull(RESULT,'') as RESULT,QCMANNM,QCDATE,ISAGAIN,ISRESULT,BJSJ from View_TBQM_APLYFORITEM  where UNIQUEID='" + HiddenFieldQCUniqueID.Value + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                RepeaterItem.DataSource = dt;
                RepeaterItem.DataBind();

            }
            BindColor();
        }

        //根据第几次质检，来控制
        private void BindColor()
        {
            List<int> ltcount = new List<int>();
            for (int i = 0; i < RepeaterItem.Items.Count; i++)
            {
                string IsAgain = (RepeaterItem.Items[i].FindControl("LabelAgain") as System.Web.UI.WebControls.Label).Text;
                //string IsResult = (RepeaterItem.Items[i].FindControl("LabelIsResult") as Label).Text;
                ltcount.Add(Convert.ToInt32(IsAgain));
                if (IsAgain == "1")
                {
                    //表示第二次报检
                    (RepeaterItem.Items[i].FindControl("LabelMarName") as System.Web.UI.WebControls.Label).BackColor = System.Drawing.Color.Orange;
                }
                if (Convert.ToInt32(IsAgain) > 1)
                {
                    //大于第二次报检
                    (RepeaterItem.Items[i].FindControl("LabelMarName") as System.Web.UI.WebControls.Label).BackColor = System.Drawing.Color.Red;
                }

            }
            for (int i = 0; i < RepeaterItem.Items.Count; i++)
            {
                string IsAgain = (RepeaterItem.Items[i].FindControl("LabelAgain") as System.Web.UI.WebControls.Label).Text;
                string IsResult = (RepeaterItem.Items[i].FindControl("LabelIsResult") as System.Web.UI.WebControls.Label).Text;
                if (Convert.ToInt32(IsAgain) < ltcount.Max() && IsResult == "1")
                {
                    //表示条目已质检而且是过去报检

                    (RepeaterItem.Items[i].FindControl("TextBoxQCNum") as System.Web.UI.WebControls.TextBox).Enabled = false;

                    (RepeaterItem.Items[i].FindControl("TextBoxReport") as System.Web.UI.WebControls.TextBox).Enabled = false;

                    (RepeaterItem.Items[i].FindControl("DropDownListResult") as DropDownList).Enabled = false;

                    (RepeaterItem.Items[i].FindControl("TextBoxContent") as System.Web.UI.WebControls.TextBox).Enabled = false;
                    (RepeaterItem.Items[i].FindControl("TextBoxQCMan") as System.Web.UI.WebControls.TextBox).Enabled = false;
                    (RepeaterItem.Items[i].FindControl("TextBoxDate") as System.Web.UI.WebControls.TextBox).Enabled = false;
                }
            }

        }




        private void InspecNewResult()
        {
            string sql = "SELECT * FROM TBQM_APLYFORINSPCTRESULT WHERE UNIQUEID='" + HiddenFieldQCUniqueID.Value + "' AND ISR_ISNEW='1'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                LabelRecoder.Text = dt.Rows[0]["ISR_INSPCTOR"].ToString();

                DropDownListEndResult.Items.FindByValue(dt.Rows[0]["ISR_RESULT"].ToString()).Selected = true;

                if (!string.IsNullOrEmpty(dt.Rows[0]["ISR_JGREPORT"].ToString()))
                {
                    RadioButtonListFileType.ClearSelection();
                    RadioButtonListFileType.Items[0].Selected = true;
                    lbreport.Text = dt.Rows[0]["ISR_JGREPORT"].ToString();
                    GVBind(dt.Rows[0]["ISR_JGREPORT"].ToString());
                }

                if (!string.IsNullOrEmpty(dt.Rows[0]["ISR_ZJREPORT"].ToString()))
                {
                    RadioButtonListFileType.ClearSelection();
                    RadioButtonListFileType.Items[1].Selected = true;
                    lbreport.Text = dt.Rows[0]["ISR_ZJREPORT"].ToString();
                    GVBind(dt.Rows[0]["ISR_ZJREPORT"].ToString());
                }

                TextBoxDecp.Text = dt.Rows[0]["ISR_INSPCTDSCP"].ToString();
                TextBoxResultNote.Text = dt.Rows[0]["ISR_NOTE"].ToString();
            }


        }

        private void ControlVisual(bool visual)
        {
            FileUploadupdate.Visible = visual;
            bntupload.Visible = visual;
            gvfileslist.Columns[2].Visible = visual;
        }



        //报检邮件提示功能
        protected void Submit_Command(object sender, CommandEventArgs e)
        {
            string returnvalue = "";
            string body = "";
            string zdrEmail = "";

            string bodymx2 = "";
            string qcmannm = "";
            string zjbh = id.PadLeft(5, '0');

            string sql = "select EMail from TBDS_STAFFINFO where ST_NAME='" + LabelManName.Text + "' and ST_PD='0' ";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                zdrEmail = dt.Rows[0][0].ToString();
            }

            for (int i = 0; i < RepeaterItem.Items.Count; i++)
            {
                string ptc = (RepeaterItem.Items[i].FindControl("LabelPTC") as System.Web.UI.WebControls.Label).Text;//计划跟踪号
                string marnm = (RepeaterItem.Items[i].FindControl("LabelMarName") as System.Web.UI.WebControls.Label).Text;//子项名称
                string bjnum = (RepeaterItem.Items[i].FindControl("TextBoxPJNum") as System.Web.UI.WebControls.TextBox).Text;//报检数量
                string jhstate = (RepeaterItem.Items[i].FindControl("TextBoxState") as System.Web.UI.WebControls.TextBox).Text;//交货状态
                string cont = (RepeaterItem.Items[i].FindControl("TextBoxControlContent") as System.Web.UI.WebControls.TextBox).Text;//检查内容

                string tuhao = (RepeaterItem.Items[i].FindControl("TextBoxDrawingNO") as System.Web.UI.WebControls.TextBox).Text;//图号
                string gg = (RepeaterItem.Items[i].FindControl("TextBOXgg") as System.Web.UI.WebControls.TextBox).Text;//规格
                string cz = (RepeaterItem.Items[i].FindControl("TextBOXcz") as System.Web.UI.WebControls.TextBox).Text;//材质
                string gb = (RepeaterItem.Items[i].FindControl("TextBOXgb") as System.Web.UI.WebControls.TextBox).Text;//国标

                string num = (RepeaterItem.Items[i].FindControl("TextBoxQCNum") as System.Web.UI.WebControls.TextBox).Text;//质检数量
                string selfrecord = (RepeaterItem.Items[i].FindControl("TextBoxReport") as System.Web.UI.WebControls.TextBox).Text;//自检报告
                string itemResult = (RepeaterItem.Items[i].FindControl("DropDownListResult") as DropDownList).SelectedValue;//检验结果
                string zgcontenyt = (RepeaterItem.Items[i].FindControl("TextBoxContent") as System.Web.UI.WebControls.TextBox).Text;//整改内容
                string iteminspec = Session["UserID"].ToString();//质检人ID
                string iteminspecor = (RepeaterItem.Items[i].FindControl("TextBoxQCMan") as System.Web.UI.WebControls.TextBox).Text;//质检人姓名
                string itemdate = (RepeaterItem.Items[i].FindControl("TextBoxDate") as System.Web.UI.WebControls.TextBox).Text;//日期
                int j = i;
                j++;
                qcmannm = iteminspecor;
                bodymx2 += "明细" + j + "\n" + "计划跟踪号:" + ptc + "\n" + "子项名称：" + marnm + "\n" + "图号：" + tuhao + "\n" + "规格：" + gg + "\n" + "材质：" + cz + "\n" + "国标：" + gb + "\n" + "报检数量：" + bjnum + "\n" + "交货状态：" + jhstate + "\n" + "检查内容：" + cont + "\n" + "检查地点：" + TextBoxSite.Text + "\n";
                bodymx2 += "质检数量：" + num + "\n" + "自检报告：" + selfrecord + "\n" + "检验结果：" + itemResult + "\n" + "整改内容：" + zgcontenyt + "\n" + "质检人：" + iteminspecor + "\n" + "检查日期：" + itemdate + "\n";
            }

            string to = "bianzhiwei@cbmi.com.cn";

            List<string> bjEmailCC = new List<string>();
            bjEmailCC.Add("zhangchaochen@cbmi.com.cn");
            bjEmailCC.Add("yangshuyun@cbmi.com.cn");
            bjEmailCC.Add("duanyonghui@cbmi.com.cn");
            

            List<string> zjEmailCC = new List<string>();
            zjEmailCC.Add("zhangchaochen@cbmi.com.cn");
            zjEmailCC.Add("yangshuyun@cbmi.com.cn");
            zjEmailCC.Add("duanyonghui@cbmi.com.cn");
            zjEmailCC.Add("liruiming@cbmi.com.cn");
            zjEmailCC.Add("gaozhaozeng@cbmi.com.cn");
            zjEmailCC.Add("zhangzhidong@cbmi.com.cn");
            zjEmailCC.Add("wangyongchao@cbmi.com.cn");
            zjEmailCC.Add("chenzesheng@cbmi.com.cn");
            
            if (LabelManName.Text.ToString() == "岳冀英")
            {
                zjEmailCC.Add("zhangguanghui@cbmi.com.cn");
            }

            sql = "select EMail from TBDS_STAFFINFO where ST_PD='0' and ST_CODE like '0702%' ";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    zjEmailCC.Add(dt.Rows[i][0].ToString());

                }

            }

            List<string> mfEmail = null;


            //新的报检单
            if (flag == "NEW")
            {
                SubmitCGInfo();


                body = "新的质量报检任务" + "\n" + "项  目  为:" + LabelProName.Text + "\n" + "工  程  为：" + LabelEngName.Text + "\n" + "供货单位为：" + LabelSupplier.Text + "\n" + "报  检  人：" + LabelManName.Text + "\n" + "报检部件为：" + TextBoxPartName.Text + "\n" + "生产制号为：" + LabelEngID.Text;
                returnvalue = DBCallCommon.SendEmail(to, bjEmailCC, mfEmail, LabelProName.Text + "-" + LabelEngName.Text + "/" + TextBoxPartName.Text + "数字平台质量报检", body);

            }

            //二次报检
            if (flag == "UPDATE")
            {
                string ret = SubmitUpdateInfo();
                if (ret == "yes")
                {
                    body = "新的二次质量报检任务" + "\n" + "质检编号为：" + zjbh + "\n" + "项  目  为:" + LabelProName.Text + "\n" + "工  程  为：" + LabelEngName.Text + "\n" + "供货单位为：" + LabelSupplier.Text + "\n" + "报  检  人：" + LabelManName.Text + "\n" + "报检部件为：" + TextBoxPartName.Text + "\n" + "生产制号为：" + LabelEngID.Text;
                    returnvalue = DBCallCommon.SendEmail(to, bjEmailCC, mfEmail, LabelProName.Text + "-" + LabelEngName.Text + "/" + TextBoxPartName.Text + "数字平台质量二次报检", body);
                }



            }

            //质检人结果反馈
            if (flag == "INSPEC")
            {
                SubmitInspecInfo();
                body = "质量报检结果反馈" + "\n" + "质检编号为：" + zjbh + "\n" + "项  目  为:" + LabelProName.Text + "\n" + "工  程  为：" + LabelEngName.Text + "\n" + "供货单位为：" + LabelSupplier.Text + "\n" + "报  检  人：" + LabelManName.Text + "\n" + "报检部件为：" + TextBoxPartName.Text + "\n" + "质  检  人：" + qcmannm + "\n" + "质检结果为：" + DropDownListEndResult.SelectedItem + "\n" + "生产制号为：" + LabelEngID.Text + "\n" + bodymx2;

                returnvalue = DBCallCommon.SendEmail(zdrEmail, zjEmailCC, mfEmail, LabelProName.Text + "-" + LabelEngName.Text + "/" + TextBoxPartName.Text + "数字平台质量报检结果反馈" + DropDownListEndResult.SelectedItem, body);

            }

            //修改质检结果
            if (flag == "INUPDATE")
            {
                string ret = SubmitInUpdateInfo();
                if (ret == "yes")
                {

                    body = "质量报检结果反馈" + "\n" + "质检编号为：" + zjbh + "\n" + "项  目  为:" + LabelProName.Text + "\n" + "工  程  为：" + LabelEngName.Text + "\n" + "供货单位为：" + LabelSupplier.Text + "\n" + "报  检  人：" + LabelManName.Text + "\n" + "报检部件为：" + TextBoxPartName.Text + "\n" + "质  检  人：" + qcmannm + "\n" + "质检结果为：" + DropDownListEndResult.SelectedItem + "\n" + "生产制号为：" + LabelEngID.Text + "\n" + bodymx2;

                    returnvalue = DBCallCommon.SendEmail(zdrEmail, zjEmailCC, mfEmail, LabelProName.Text + "-" + LabelEngName.Text + "/" + TextBoxPartName.Text + "数字平台质量报检结果反馈" + DropDownListEndResult.SelectedItem, body);
                }
            }

            if (returnvalue == "邮件已发送!")
            {
                //string jascript = @"alert('邮件发送成功!');";

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", jascript, true);

                Response.Write("<script>alert('邮件发送成功');</script>");
            }
            else if (returnvalue == "邮件发送失败!")
            {
                Response.Write("<script>alert('邮件发送不成功');</script>");

            }

        }


        private string SubmitInUpdateInfo()
        {
            string retvalue = "yes";
            int n = 0;
            if (nm == "0")
            {
                //修改质检结果
                List<string> sqlcount = new List<string>();

                string sql = "UPDATE TBQM_APLYFORINSPCTRESULT SET ISR_JGREPORT=NULL,ISR_ZJREPORT=NULL WHERE UNIQUEID='" + HiddenFieldQCUniqueID.Value + "' AND ISR_ISNEW='1'";
                sqlcount.Add(sql);
                //更新最新质检记录
                sql = "UPDATE TBQM_APLYFORINSPCTRESULT SET ISR_RESULT='" + DropDownListEndResult.SelectedValue + "',ISR_DATE='" + System.DateTime.Now.ToString("yyyy-MM-dd") + "'," + RadioButtonListFileType.SelectedValue + "='" + lbreport.Text + "',ISR_INSPCT='" + Session["UserID"].ToString() + "',ISR_INSPCTOR='" + Session["UserName"].ToString() + "',ISR_INSPCTDSCP='" + TextBoxDecp.Text + "',ISR_NOTE='" + TextBoxResultNote.Text + "'WHERE UNIQUEID='" + HiddenFieldQCUniqueID.Value + "' AND ISR_ISNEW='1'";
                sqlcount.Add(sql);

                sql = "update TBQM_APLYFORINSPCT set AFI_ISSH='0',AFI_STATE='1',AFI_ENDRESLUT='" + DropDownListEndResult.SelectedItem.Text + "',AFI_ENDDATE='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where AFI_ID='" + id + "'";
                sqlcount.Add(sql);

                for (int i = 0; i < RepeaterItem.Items.Count; i++)
                {
                    System.Web.UI.WebControls.CheckBox checkbox = RepeaterItem.Items[i].FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox;
                    if (checkbox.Checked)
                    {

                        string key = (RepeaterItem.Items[i].FindControl("LabelKey") as System.Web.UI.WebControls.Label).Text;//主键
                        //string uniqueid = (RepeaterItem.Items[i].FindControl("LabelUNIQUEID") as System.Web.UI.WebControls.Label).Text;
                        string ptc = (RepeaterItem.Items[i].FindControl("LabelPTC") as System.Web.UI.WebControls.Label).Text;

                        string num = (RepeaterItem.Items[i].FindControl("TextBoxQCNum") as System.Web.UI.WebControls.TextBox).Text;//质检数量
                        string selfrecord = (RepeaterItem.Items[i].FindControl("TextBoxReport") as System.Web.UI.WebControls.TextBox).Text;//自检记录
                        string itemResult = (RepeaterItem.Items[i].FindControl("DropDownListResult") as DropDownList).SelectedValue;//自检记录
                        string zgcontenyt = (RepeaterItem.Items[i].FindControl("TextBoxContent") as System.Web.UI.WebControls.TextBox).Text;//整改内容
                        string iteminspec = Session["UserID"].ToString();//质检人ID
                        string iteminspecor = (RepeaterItem.Items[i].FindControl("TextBoxQCMan") as System.Web.UI.WebControls.TextBox).Text;//质检人姓名
                        string itemdate = (RepeaterItem.Items[i].FindControl("TextBoxDate") as System.Web.UI.WebControls.TextBox).Text;//日期

                        string sql1 = "select distinct uniqueid from TBQM_APLYFORITEM  where PTC='" + ptc + "' ";
                        System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
                        if (dt1.Rows.Count > 1)
                        {
                            string sql2 = "select top 1 UNIQUEID as UNIQUEID from  TBQM_APLYFORITEM where PTC='" + ptc + "' order by ID desc ";
                            System.Data.DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql2);
                            string ID = dt2.Rows[0]["UNIQUEID"].ToString();
                            if (ID != HiddenFieldQCUniqueID.Value)
                            {
                                int m = i + 1;
                                string script = @"alert('第[" + m + "]条质检的计划跟踪号已经重新报检，此处不是最新报检，不能修改!');";
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", script, true);
                                retvalue = "no";

                                return retvalue;
                            }
                        }

                        sql = "UPDATE TBPC_PURORDERDETAIL SET PO_CGFS='" + itemResult + "' WHERE PO_PCODE='" + ptc + "'";
                        sqlcount.Add(sql);

                        sql = "UPDATE TBQM_APLYFORITEM SET QCNUM='" + num + "',ZJREPORT='" + selfrecord + "',ZGCONTENT='" + zgcontenyt + "',RESULT='" + itemResult + "',QCMAN='" + iteminspec + "',QCMANNM='" + iteminspecor + "',QCDATE='" + itemdate + "'WHERE ID='" + key + "'";
                        sqlcount.Add(sql);
                    }
                }



                DBCallCommon.ExecuteTrans(sqlcount);

                Response.Redirect("QC_Inspection_Add.aspx?ACTION=VIEW&&ID=" + id, false);
            }
            else
            {
                //重新质检
                List<string> sqlcount = new List<string>();

                //更新最新的质检记录的状态
                string sql = "UPDATE TBQM_APLYFORINSPCTRESULT SET ISR_ISNEW='0' WHERE UNIQUEID='" + HiddenFieldQCUniqueID.Value + "' AND ISR_ISNEW='1'";
                sqlcount.Add(sql);
                //插入新的质检记录，并标识为最新的
                sql = "INSERT INTO TBQM_APLYFORINSPCTRESULT (UNIQUEID,ISR_RESULT,ISR_DATE," + RadioButtonListFileType.SelectedValue + ",ISR_INSPCT,ISR_INSPCTOR,ISR_INSPCTDSCP,ISR_ISNEW,ISR_NOTE)";
                sql += " VALUES  ('" + HiddenFieldQCUniqueID.Value + "','" + DropDownListEndResult.SelectedValue + "','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "','" + HiddenFieldContent.Value + "','" + Session["UserID"].ToString() + "','" + Session["UserName"].ToString() + "','" + TextBoxDecp.Text + "','1','" + TextBoxResultNote.Text + "')";
                sqlcount.Add(sql);
                //更新总表的质检状态，最终结果
                sql = "update TBQM_APLYFORINSPCT set AFI_STATE='1',AFI_ENDRESLUT='" + DropDownListEndResult.SelectedItem.Text + "',AFI_ENDDATE='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where AFI_ID='" + id + "'";
                sqlcount.Add(sql);
                for (int i = 0; i < RepeaterItem.Items.Count; i++)
                {
                    //遍历更新
                    System.Web.UI.WebControls.CheckBox checkbox = RepeaterItem.Items[i].FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox;
                    if (checkbox.Checked)
                    {
                        string key = (RepeaterItem.Items[i].FindControl("LabelKey") as System.Web.UI.WebControls.Label).Text;//主键

                        string ptc = (RepeaterItem.Items[i].FindControl("LabelPTC") as System.Web.UI.WebControls.Label).Text;

                        string num = (RepeaterItem.Items[i].FindControl("TextBoxQCNum") as System.Web.UI.WebControls.TextBox).Text;//质检数量
                        string selfrecord = (RepeaterItem.Items[i].FindControl("TextBoxReport") as System.Web.UI.WebControls.TextBox).Text;//自检记录
                        string itemResult = (RepeaterItem.Items[i].FindControl("DropDownListResult") as DropDownList).SelectedValue;//自检记录
                        string zgcontenyt = (RepeaterItem.Items[i].FindControl("TextBoxContent") as System.Web.UI.WebControls.TextBox).Text;//整改内容
                        string iteminspec = Session["UserID"].ToString();//质检人ID
                        string iteminspecor = (RepeaterItem.Items[i].FindControl("TextBoxQCMan") as System.Web.UI.WebControls.TextBox).Text;//质检人姓名
                        string itemdate = (RepeaterItem.Items[i].FindControl("TextBoxDate") as System.Web.UI.WebControls.TextBox).Text;//日期



                        sql = "UPDATE TBPC_PURORDERDETAIL SET PO_CGFS='" + itemResult + "' WHERE PO_PCODE='" + ptc + "'";
                        sqlcount.Add(sql);


                        sql = "UPDATE TBQM_APLYFORITEM SET QCNUM='" + num + "',ZJREPORT='" + selfrecord + "',ZGCONTENT='" + zgcontenyt + "',RESULT='" + itemResult + "',QCMAN='" + iteminspec + "',QCMANNM='" + iteminspecor + "',QCDATE='" + itemdate + "',ISRESULT='1' WHERE ID='" + key + "'";
                        sqlcount.Add(sql);
                        n++;
                    }
                }
                if (n != 0)
                {
                    DBCallCommon.ExecuteTrans(sqlcount);

                    Response.Redirect("QC_Inspection_Add.aspx?ACTION=VIEW&&ID=" + id, false);
                }
                else
                {
                    string script = @"alert('请勾选需要再次质检的子项!!');";

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", script, true);
                    retvalue = "no";

                    return retvalue;
                }
            }
            return retvalue;
        }

        private string SubmitUpdateInfo()
        {
            string retvalue = "yes";
            if (nm == "0")
            {
                //修改
                List<string> sqlcount = new List<string>();
                string sql = "UPDATE TBQM_APLYFORINSPCT SET AFI_TSDEP='" + LabelDep.Text + "',AFI_PJNAME='" + LabelProName.Text + "',AFI_ENGID='" + LabelEngID.Text + "',AFI_ENGNAME='" + LabelEngName.Text + "',AFI_PARTNAME='" + TextBoxPartName.Text + "',AFI_DATACLCT='" + TextBoxData.Text + "',AFI_RQSTCDATE='" + TextBoxDate.Text + "',";
                sql += " AFI_SUPPLERNM='" + LabelSupplier.Text + "',AFI_ISPCTSITE='" + TextBoxSite.Text + "',AFI_CONTACT='" + TextBoxContracter.Text + "',AFI_CONTEL='" + TextBoxTel.Text + "',AFI_MAN='" + Session["UserID"].ToString() + "',AFI_MANNM='" + Session["UserName"].ToString() + "',AFI_NOTE='" + TextBoxNote.Text + "' WHERE UNIQUEID='" + HiddenFieldQCUniqueID.Value + "'";
                sqlcount.Add(sql);

                for (int i = 0; i < RepeaterItem.Items.Count; i++)
                {
                    //string IsAgain = (RepeaterItem.Items[i].FindControl("LabelAgain") as Label).Text;
                    //if (IsAgain!=1)
                    //{
                    string key = (RepeaterItem.Items[i].FindControl("LabelKey") as System.Web.UI.WebControls.Label).Text;//主键
                    string marnm = (RepeaterItem.Items[i].FindControl("LabelMarName") as System.Web.UI.WebControls.Label).Text;
                    string drawing = (RepeaterItem.Items[i].FindControl("TextBoxDrawingNO") as System.Web.UI.WebControls.TextBox).Text;
                    string num = (RepeaterItem.Items[i].FindControl("LabelNum") as System.Web.UI.WebControls.Label).Text;
                    string sw = (RepeaterItem.Items[i].FindControl("LabelSW") as System.Web.UI.WebControls.Label).Text;
                    string sumw = (RepeaterItem.Items[i].FindControl("LabelSumW") as System.Web.UI.WebControls.Label).Text;
                    string pjnum = (RepeaterItem.Items[i].FindControl("TextBoxPJNum") as System.Web.UI.WebControls.TextBox).Text;
                    string content = (RepeaterItem.Items[i].FindControl("TextBoxControlContent") as System.Web.UI.WebControls.TextBox).Text;
                    string jhstate = (RepeaterItem.Items[i].FindControl("TextBoxState") as System.Web.UI.WebControls.TextBox).Text;

                    sql = "UPDATE TBQM_APLYFORITEM SET PARTNM='" + marnm + "',TUHAO='" + drawing + "',DANZHONG='" + sw + "',NUM='" + num + "',ZONGZHONG='" + sumw + "',PJNUM='" + pjnum + "',CONT='" + content + "',JHSTATE='" + jhstate + "' WHERE ID='" + key + "'";

                    sqlcount.Add(sql);
                    //}
                }
                DBCallCommon.ExecuteTrans(sqlcount);
                Response.Redirect("QC_Inspection_Add.aspx?ACTION=VIEW&&ID=" + id, false);

            }
            else
            {
                List<string> sqlcount = new List<string>();

                string sql = "";

                //多次报检

                for (int i = 0; i < RepeaterItem.Items.Count; i++)
                {
                    if ((RepeaterItem.Items[i].FindControl("CheckBox1") as System.Web.UI.WebControls.CheckBox).Checked)
                    {
                        int IsAgain = Convert.ToInt32((RepeaterItem.Items[i].FindControl("LabelAgain") as System.Web.UI.WebControls.Label).Text.ToString()) + 1;

                        string ptc = (RepeaterItem.Items[i].FindControl("LabelPTC") as System.Web.UI.WebControls.Label).Text;

                        string marnm = (RepeaterItem.Items[i].FindControl("LabelMarName") as System.Web.UI.WebControls.Label).Text;
                        string drawing = (RepeaterItem.Items[i].FindControl("TextBoxDrawingNO") as System.Web.UI.WebControls.TextBox).Text;
                        string num = (RepeaterItem.Items[i].FindControl("LabelNum") as System.Web.UI.WebControls.Label).Text;
                        string sw = (RepeaterItem.Items[i].FindControl("LabelSW") as System.Web.UI.WebControls.Label).Text;
                        string sumw = (RepeaterItem.Items[i].FindControl("LabelSumW") as System.Web.UI.WebControls.Label).Text;
                        string pjnum = (RepeaterItem.Items[i].FindControl("TextBoxPJNum") as System.Web.UI.WebControls.TextBox).Text;
                        string content = (RepeaterItem.Items[i].FindControl("TextBoxControlContent") as System.Web.UI.WebControls.TextBox).Text;
                        string jhstate = (RepeaterItem.Items[i].FindControl("TextBoxState") as System.Web.UI.WebControls.TextBox).Text;

                        //将其报检项设置为第二次报检
                        sql = "INSERT INTO TBQM_APLYFORITEM (UNIQUEID,PARTNM,TUHAO ,NUM,DANZHONG,ZONGZHONG ,PJNUM,CONT,JHSTATE,ISAGAIN,PTC,BJSJ)";
                        sql += "  VALUES ('" + HiddenFieldQCUniqueID.Value + "','" + marnm + "','" + drawing + "','" + num + "','" + sw + "','" + sumw + "','" + pjnum + "','" + content + "','" + jhstate + "','" + IsAgain + "','" + ptc + "','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "')";

                        sqlcount.Add(sql);
                    }
                }


                if (sqlcount.Count > 0)
                {
                    //修改质检状态
                    sql = "update TBQM_APLYFORINSPCT set AFI_STATE='0',AFI_NUMBER=AFI_NUMBER+1,AFI_ENDRESLUT=NULL,AFI_ENDDATE=NULL ,AFI_RQSTCDATE='" + TextBoxDate.Text + "',AFI_PARTNAME='" + TextBoxPartName.Text + "',AFI_CONTACT='" + TextBoxContracter.Text + "',AFI_CONTEL='" + TextBoxTel.Text + "',AFI_ISPCTSITE='" + TextBoxSite.Text + "',AFI_NOTE='" + TextBoxNote.Text + "'  where AFI_ID='" + id + "'";
                    sqlcount.Add(sql);

                    //更新最新的质检记录的状态
                    sql = "UPDATE TBQM_APLYFORINSPCTRESULT SET ISR_ISNEW='0' WHERE UNIQUEID='" + HiddenFieldQCUniqueID.Value + "' AND ISR_ISNEW='1'";
                    sqlcount.Add(sql);
                    DBCallCommon.ExecuteTrans(sqlcount);

                    Response.Redirect("QC_Inspection_Add.aspx?ACTION=VIEW&&ID=" + id, false);
                }
                else
                {
                    string script = @"alert('请勾选需要重新报检的子项!!');";

                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", script, true);

                    retvalue = "no";

                }
            }
            return retvalue;
        }


        private void SubmitInspecInfo()
        {

            List<string> sqlcount = new List<string>();
            string sql = "INSERT INTO TBQM_APLYFORINSPCTRESULT (UNIQUEID,ISR_RESULT,ISR_DATE," + RadioButtonListFileType.SelectedValue + ",ISR_INSPCT,ISR_INSPCTOR,ISR_INSPCTDSCP,ISR_ISNEW,ISR_NOTE)";
            sql += " VALUES  ('" + HiddenFieldQCUniqueID.Value + "','" + DropDownListEndResult.SelectedValue + "','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "','" + HiddenFieldContent.Value + "','" + Session["UserID"].ToString() + "','" + Session["UserName"].ToString() + "','" + TextBoxDecp.Text + "','1','" + TextBoxResultNote.Text + "')";
            sqlcount.Add(sql);
            sql = "update TBQM_APLYFORINSPCT set AFI_STATE='1',AFI_ENDRESLUT='" + DropDownListEndResult.SelectedItem.Text + "',AFI_ENDDATE='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  where AFI_ID='" + id + "'";
            sqlcount.Add(sql);
            for (int i = 0; i < RepeaterItem.Items.Count; i++)
            {
                string key = (RepeaterItem.Items[i].FindControl("LabelKey") as System.Web.UI.WebControls.Label).Text;//主键

                string ptc = (RepeaterItem.Items[i].FindControl("LabelPTC") as System.Web.UI.WebControls.Label).Text;

                string num = (RepeaterItem.Items[i].FindControl("TextBoxQCNum") as System.Web.UI.WebControls.TextBox).Text;//质检数量
                string selfrecord = (RepeaterItem.Items[i].FindControl("TextBoxReport") as System.Web.UI.WebControls.TextBox).Text;//自检记录
                string itemResult = (RepeaterItem.Items[i].FindControl("DropDownListResult") as DropDownList).SelectedValue;//自检记录
                string zgcontenyt = (RepeaterItem.Items[i].FindControl("TextBoxContent") as System.Web.UI.WebControls.TextBox).Text;//整改内容
                string iteminspec = Session["UserID"].ToString();//质检人ID
                string iteminspecor = (RepeaterItem.Items[i].FindControl("TextBoxQCMan") as System.Web.UI.WebControls.TextBox).Text;//质检人姓名
                string itemdate = (RepeaterItem.Items[i].FindControl("TextBoxDate") as System.Web.UI.WebControls.TextBox).Text;//日期

                sql = "UPDATE TBPC_PURORDERDETAIL SET PO_CGFS='" + itemResult + "' WHERE PO_PCODE='" + ptc + "'";
                sqlcount.Add(sql);

                sql = "UPDATE TBQM_APLYFORITEM SET QCNUM='" + num + "',ZJREPORT='" + selfrecord + "',ZGCONTENT='" + zgcontenyt + "',RESULT='" + itemResult + "',QCMAN='" + iteminspec + "',QCMANNM='" + iteminspecor + "',QCDATE='" + itemdate + "',ISRESULT='1' WHERE ID='" + key + "'";
                sqlcount.Add(sql);
            }
            DBCallCommon.ExecuteTrans(sqlcount);

            Response.Redirect("QC_Inspection_Add.aspx?ACTION=VIEW&&ID=" + id, false);

        }
        private void SubmitCGInfo()
        {
            List<string> sqlcount = new List<string>();

            string sql = "DELETE FROM TBQM_APLYFORINSPCT WHERE UNIQUEID='" + hfduniqueid.Value + "'";

            sqlcount.Add(sql);

            sql = "DELETE FROM TBQM_APLYFORITEM WHERE UNIQUEID='" + hfduniqueid.Value + "'";

            sqlcount.Add(sql);

            sql = "INSERT INTO TBQM_APLYFORINSPCT (AFI_TSDEP,AFI_PJNAME,AFI_ENGID,AFI_ENGNAME,AFI_PARTNAME ,AFI_DATACLCT ,AFI_DATE,AFI_RQSTCDATE,AFI_SUPPLERNM,AFI_ISPCTSITE,AFI_CONTACT,AFI_CONTEL,AFI_MAN,AFI_MANNM,AFI_NOTE,AFI_NUMBER,UNIQUEID)";
            sql += " VALUES  ('" + LabelDep.Text + "','" + LabelProName.Text + "','" + LabelEngID.Text + "','" + LabelEngName.Text + "','" + TextBoxPartName.Text + "','" + TextBoxData.Text + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + TextBoxDate.Text + "','" + LabelSupplier.Text + "','" + TextBoxSite.Text + "','" + TextBoxContracter.Text + "','" + TextBoxTel.Text + "','" + LabelMan.Text + "','" + LabelManName.Text + "','" + TextBoxNote.Text + "',1,'" + hfduniqueid.Value + "')";

            sqlcount.Add(sql);

            for (int i = 0; i < RepeaterItem.Items.Count; i++)
            {
                string ptc = (RepeaterItem.Items[i].FindControl("LabelPTC") as System.Web.UI.WebControls.Label).Text;
                string marnm = (RepeaterItem.Items[i].FindControl("LabelMarName") as System.Web.UI.WebControls.Label).Text;

                string drawing = (RepeaterItem.Items[i].FindControl("TextBoxDrawingNO") as System.Web.UI.WebControls.TextBox).Text;
                string num = (RepeaterItem.Items[i].FindControl("LabelNum") as System.Web.UI.WebControls.Label).Text;
                string sw = (RepeaterItem.Items[i].FindControl("LabelSW") as System.Web.UI.WebControls.Label).Text;
                string sumw = (RepeaterItem.Items[i].FindControl("LabelSumW") as System.Web.UI.WebControls.Label).Text;
                string pjnum = (RepeaterItem.Items[i].FindControl("TextBoxPJNum") as System.Web.UI.WebControls.TextBox).Text;
                string content = (RepeaterItem.Items[i].FindControl("TextBoxControlContent") as System.Web.UI.WebControls.TextBox).Text;
                string jhstate = (RepeaterItem.Items[i].FindControl("TextBoxState") as System.Web.UI.WebControls.TextBox).Text;

                sql = "INSERT INTO TBQM_APLYFORITEM (UNIQUEID,PTC,PARTNM,TUHAO,NUM,DANZHONG,ZONGZHONG ,PJNUM,CONT,JHSTATE,ISAGAIN,BJSJ)";
                sql += "  VALUES ('" + hfduniqueid.Value + "','" + ptc + "','" + marnm + "','" + drawing + "','" + num + "','" + sw + "','" + sumw + "','" + pjnum + "','" + content + "','" + jhstate + "','0','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "')";

                sqlcount.Add(sql);

                sql = "UPDATE TBPC_PURORDERDETAIL SET PO_CGFS='待检' WHERE  PO_PCODE='" + ptc + "'";

                sqlcount.Add(sql);
            }

            DBCallCommon.ExecuteTrans(sqlcount);

            Response.Redirect("QC_Inspection_Manage.aspx", false);
        }

        private void BindProcessResult()
        {
            string sql = "select * from TBQM_APLYFORINSPCTRESULT where UNIQUEID='" + HiddenFieldQCUniqueID.Value + "' AND ISR_ISNEW='0' ORDER BY ISR_DATE ";
            DataSet ds = DBCallCommon.FillDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
        }
        protected void GridView_BindProcessFile()
        {
            foreach (GridViewRow gr in GridView1.Rows)
            {
                System.Web.UI.WebControls.Label jglb = (System.Web.UI.WebControls.Label)gr.FindControl("lb_jgreport");
                System.Web.UI.WebControls.Label zjlb = (System.Web.UI.WebControls.Label)gr.FindControl("lb_zjreport");
                string sql = "select * from TBQM_RPFILES where PR_CONTENT='" + jglb.Text.Trim() + "'order by RP_UPLOADDATE ";
                DataSet ds = DBCallCommon.FillDataSet(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ImageButton ib = new ImageButton();
                        ib.Height = 18;
                        ib.Width = 18;
                        jglb.Parent.Controls.Add(ib);
                        ib.ImageUrl = "~/Assets/images/pdf.jpg";
                        ib.ToolTip = ds.Tables[0].Rows[i]["RP_FILENAME"].ToString();
                        ib.ValidationGroup = ds.Tables[0].Rows[i]["RP_ID"].ToString();
                        ib.Click += new ImageClickEventHandler(ib_Click);//注册事件 
                        this.RegisterRequiresPostBack(ib);
                    }
                }
                sql = "select * from TBQM_RPFILES where PR_CONTENT='" + zjlb.Text.Trim() + "' and PR_CONTENT!='' order by RP_UPLOADDATE ";
                ds = DBCallCommon.FillDataSet(sql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ImageButton ib = new ImageButton();
                        ib.Height = 18;
                        ib.Width = 18;
                        zjlb.Parent.Controls.Add(ib);
                        ib.ImageUrl = "~/Assets/images/pdf.jpg";
                        ib.ToolTip = ds.Tables[0].Rows[i]["RP_FILENAME"].ToString();
                        ib.ValidationGroup = ds.Tables[0].Rows[i]["RP_ID"].ToString();
                        ib.Click += new ImageClickEventHandler(ib_Click);//注册事件 
                        this.RegisterRequiresPostBack(ib);
                    }
                }
            }
        }

        protected void ib_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtn = (ImageButton)sender;
            string sqlStr = "select RP_SAVEURL,RP_FILENAME from TBQM_RPFILES where RP_ID='" + imgbtn.ValidationGroup + "'";
            //打开数据库
            Response.Write(sqlStr);
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取文件路径
            string strFilePath = ds.Tables[0].Rows[0]["RP_SAVEURL"].ToString() + @"\" + ds.Tables[0].Rows[0]["RP_FILENAME"].ToString();
            //Response.Write(strFilePath);
            if (File.Exists(strFilePath))
            {
                System.IO.FileInfo file = new System.IO.FileInfo(strFilePath);
                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(file.Name));
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


        /// <summary>
        /// 找到上传文件的路径名
        /// </summary>
        /// <returns></returns>
        private string findPath()
        {
            string ulr = "";

            string sql = "select QSA_ENGADDR from TBQM_QTSASSGN  where  QSA_ZONGXU='0' and  QSA_ENGID='" + LabelEngID.Text.Trim().Split('-')[0] + "'";

            DataSet ds = DBCallCommon.FillDataSet(sql);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ulr = ds.Tables[0].Rows[0]["QSA_ENGADDR"].ToString();
            }
            else
            {
                //报检的内容在任务分工表中没有，则需要重新创建文件夹--需要后面继续做

            }
            return ulr;
        }

        private int IntIsUF = 0;
        /// <summary>
        /// 重点在于要给合同文本内容赋值BC_CONTEXT
        /// </summary>
        private void uploafFile()
        {
            //获取文件保存的路径 

            /************************************************************/
            //根据质检报告的类型来选择存储方式
            string filePath = findPath() + @"\质量报检" + @"\" + RadioButtonListFileType.SelectedItem.Text;
            /************************************************************/
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            HttpPostedFile hpf = FileUploadupdate.PostedFile;

            try
            {
                string fileContentType = hpf.ContentType;// 获取客户端发送的文件的 MIME 内容类型  

                if (fileContentType == "application/msword" || fileContentType == "application/vnd.ms-excel" || fileContentType == "application/pdf" || fileContentType == "image/pjpeg" || fileContentType == "image/gif" || fileContentType == "image/png" || fileContentType == "image/bmp" || fileContentType == "application/octet-stream" || fileContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")//传送文件类型
                {
                    if (hpf.ContentLength > 0)
                    {
                        if (hpf.ContentLength < (1024 * 1024))
                        {
                            string strFileName = System.IO.Path.GetFileName(hpf.FileName);
                            //string filename = System.DateTime.Now.ToString("yyyyMMddHHmmss") + strFileName;
                            if (!File.Exists(filePath + @"\" + strFileName))
                            {
                                string sqlStr = string.Empty;
                                //定义插入字符串，将上传文件信息保存在数据库中
                                if (flag == "INUPDATE" & nm == "0")
                                {
                                    //只是更新
                                    //报告内容、保存地址、上传日期、文件名
                                    sqlStr = "insert into TBQM_RPFILES (PR_CONTENT,RP_SAVEURL,RP_UPLOADDATE,RP_FILENAME)";
                                    sqlStr += "values('" + lbreport.Text + "'";
                                    sqlStr += ",'" + filePath + "'";
                                    sqlStr += ",'" + DateTime.Now.ToString() + "'";
                                    sqlStr += ",'" + strFileName + "')";
                                }
                                else
                                {
                                    //质检或第二次质检,则添加新的质检内容
                                    //hfdContent.Value---不管上载与否，都有值
                                    sqlStr = "insert into TBQM_RPFILES (PR_CONTENT,RP_SAVEURL,RP_UPLOADDATE,RP_FILENAME)";
                                    sqlStr += "values('" + HiddenFieldContent.Value + "'";
                                    sqlStr += ",'" + filePath + "'";
                                    sqlStr += ",'" + DateTime.Now.ToString() + "'";
                                    sqlStr += ",'" + strFileName + "')";
                                }
                                DBCallCommon.ExeSqlText(sqlStr);
                                hpf.SaveAs(filePath + @"\" + strFileName);
                                //isHasFile = true;
                                IntIsUF = 1;
                            }
                            else
                            {
                                filesError.Visible = true;
                                filesError.Text = "上传文件名与服务器文件名重名，请您核对后重新命名上传！";
                                IntIsUF = 1;
                            }
                        }
                        else
                        {
                            filesError.Text = "上传文件过大，上传文件必须小于1M!";
                            IntIsUF = 1;
                        }
                    }
                }
                else
                {
                    filesError.Visible = true;
                    filesError.Text = "文件类型不符合要求，请您核对后重新上传！";
                    IntIsUF = 1;
                }
            }
            catch (Exception e)
            {
                filesError.Text = "文件上传过程中出现错误！" + e.ToString();
                filesError.Visible = true;
                return;
            }
            if (IntIsUF == 1)
            {
                IntIsUF = 0;
            }
            else
            {
                filesError.Visible = true;
                filesError.Text = "请选择上传文件!";
            }
        }
        protected void bntupload_Click(object sender, EventArgs e)
        {
            //执行上传文件
            uploafFile();
            if (flag == "INUPDATE" & nm == "0")
            {
                //修改质检结果
                GVBind(lbreport.Text);
            }
            else
            {
                //质检或者第二次质检
                GVBind(HiddenFieldContent.Value);
            }
        }
        #region 删除文件、下载文件

        protected void imgbtndelete_Click(object sender, ImageClickEventArgs e)
        {
            //获取imgbtnDelete的ImageButton对象
            ImageButton imgbtn = (ImageButton)sender;
            //引用imgbtnDelete控件的父控件上一级控件
            GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
            GridView gv = (GridView)imgbtn.Parent.Parent.Parent.Parent;
            //获取文件真实姓名
            string sqlStr = "select RP_SAVEURL,RP_FILENAME from TBQM_RPFILES where RP_ID='" + gv.DataKeys[gvr.RowIndex].Value.ToString() + "'";
            //在文件夹Files下，删除该文件
            DeleteFile(sqlStr);
            string sqlDelStr = "delete from TBQM_RPFILES where RP_ID='" + gv.DataKeys[gvr.RowIndex].Value.ToString() + "'";//删除数据库中的记录
            DBCallCommon.ExeSqlText(sqlDelStr);
            if (flag == "INUPDATE" & nm == "0")
            {
                GVBind(lbreport.Text);
            }
            else
            {
                GVBind(HiddenFieldContent.Value);
            }
            //GVBind(hfdContent.Value);//删除添加的记录
        }


        protected void DeleteFile(string sqlStr)
        {
            //打开数据库
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取指定文件的路径
            string strFilePath = ds.Tables[0].Rows[0]["RP_SAVEURL"].ToString() + @"\" + ds.Tables[0].Rows[0]["RP_FILENAME"].ToString();
            //调用File类的Delete方法，删除指定文件
            File.Delete(strFilePath);//文件不存在也不会引发异常
        }
        protected void imgbtndownload_Click(object sender, ImageClickEventArgs e)
        {
            //获取imgbtnDelete的ImageButton对象
            ImageButton imgbtn = (ImageButton)sender;
            //引用imgbtnDelete控件的父控件上一级控件
            GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
            GridView gv = (GridView)imgbtn.Parent.Parent.Parent.Parent;
            //获取文件真实姓名
            string sqlStr = "select RP_SAVEURL,RP_FILENAME from TBQM_RPFILES where RP_ID='" + gv.DataKeys[gvr.RowIndex].Value.ToString() + "'";
            //打开数据库
            //Response.Write(sqlStr);         
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取文件路径
            string strFilePath = ds.Tables[0].Rows[0]["RP_SAVEURL"].ToString() + @"\" + ds.Tables[0].Rows[0]["RP_FILENAME"].ToString();
            //Response.Write(strFilePath);
            if (File.Exists(strFilePath))
            {
                System.IO.FileInfo file = new System.IO.FileInfo(strFilePath);
                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(file.Name));
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

        /// <summary>
        /// 根据唯一字段，去找相应的附件
        /// </summary>
        /// <param name="content"></param>
        private void GVBind(string content)
        {
            string sql = "select * from TBQM_RPFILES where PR_CONTENT='" + content + "'";
            DataSet ds = DBCallCommon.FillDataSet(sql);
            gvfileslist.DataSource = ds.Tables[0];
            gvfileslist.DataBind();
            gvfileslist.DataKeyNames = new string[] { "RP_ID" };
        }


        protected void Back_Click(object sender, EventArgs e)
        {

            string back = "";

            if (Request.QueryString["back"] != null)
            {
                back = Request.QueryString["back"];
            }

            if (back == "1")
            {

                string script = "window.close();";
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", script, true);
            }
            else
            {
                Response.Redirect("QC_Inspection_Manage.aspx");
            }

        }



        protected void btn_export_Click(object sender, EventArgs e)
        {
            string sql = "select AFI_PJNAME,AFI_ENGID,AFI_ENGNAME,AFI_TSDEP,AFI_MANNM,AFI_SUPPLERNM,AFI_ISPCTSITE,AFI_CONTACT,AFI_CONTEL,PARTNM,TUHAO,PJNUM,DANZHONG,CAST(PJNUM*DANZHONG AS FLOAT) AS ZONGZHONG,JHSTATE,CONT,ZJREPORT,ZGCONTENT,RESULT,QCMANNM,QCDATE from TBQM_APLYFORINSPCT as a left  OUTER JOIN TBQM_APLYFORITEM as b on a.UNIQUEID=b.UNIQUEID where AFI_ID='" + id + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ExportData(dt);
        }

        private void ExportData(System.Data.DataTable objdt)
        {
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("报检通知单模版") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            System.Data.DataTable dt = objdt;

            // 填充数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    //表头
                    wksheet.Cells[i + 1, 13] = dt.Rows[i]["AFI_ENGID"].ToString();//生产制号

                    wksheet.Cells[i + 2, 3] = dt.Rows[i]["AFI_PJNAME"].ToString();//项目名称

                    wksheet.Cells[i + 2, 7] = dt.Rows[i]["AFI_ENGNAME"].ToString();//工程名称

                    wksheet.Cells[i + 2, 10] = dt.Rows[i]["AFI_TSDEP"].ToString();//部门

                    wksheet.Cells[i + 2, 13] = dt.Rows[i]["AFI_MANNM"].ToString();//报检人

                    wksheet.Cells[i + 3, 3] = dt.Rows[i]["PARTNM"].ToString();//部件性质

                    wksheet.Cells[i + 3, 7] = dt.Rows[i]["AFI_SUPPLERNM"].ToString();//供货单位

                    wksheet.Cells[i + 3, 10] = dt.Rows[i]["AFI_ISPCTSITE"].ToString();//地点

                    wksheet.Cells[i + 3, 13] = dt.Rows[i]["AFI_CONTACT"].ToString() + "/" + dt.Rows[i]["AFI_CONTEL"].ToString();//质检人



                    //表体
                    wksheet.Cells[i + 6, 1] = Convert.ToString(i + 1);//序号

                    wksheet.Cells[i + 6, 2] = "'" + dt.Rows[i]["PARTNM"].ToString();//子项名称

                    wksheet.Cells[i + 6, 4] = "'" + dt.Rows[i]["TUHAO"].ToString();//标识/图号

                    wksheet.Cells[i + 6, 5] = dt.Rows[i]["PJNUM"].ToString();//数量

                    wksheet.Cells[i + 6, 6] =dt.Rows[i]["DANZHONG"].ToString();//单重

                    wksheet.Cells[i + 6, 7] =dt.Rows[i]["ZONGZHONG"].ToString();//总重

                    wksheet.Cells[i + 6, 8] = "'" + dt.Rows[i]["JHSTATE"].ToString();//交货状态

                    wksheet.Cells[i + 6, 9] = "'" + dt.Rows[i]["CONT"].ToString();//检查内容

                    wksheet.Cells[i + 6, 10] = "'" + dt.Rows[i]["ZJREPORT"].ToString();//自检记录

                    wksheet.Cells[i + 6, 11] = "'" + dt.Rows[i]["RESULT"].ToString();//质检结果

                    wksheet.Cells[i + 6, 12] = "'" + dt.Rows[i]["ZGCONTENT"].ToString();//整改内容

                    wksheet.Cells[i + 6, 13] = "'" + dt.Rows[i]["QCMANNM"].ToString();//检验员

                    wksheet.Cells[i + 6, 14] = "'" + dt.Rows[i]["QCDATE"].ToString();//检验日期

                }
                else
                {

                    //表体
                    wksheet.Cells[i + 6, 1] = Convert.ToString(i + 1);//序号

                    wksheet.Cells[i + 6, 2] = "'" + dt.Rows[i]["PARTNM"].ToString();//子项名称

                    wksheet.Cells[i + 6, 4] = "'" + dt.Rows[i]["TUHAO"].ToString();//标识/图号

                    wksheet.Cells[i + 6, 5] = dt.Rows[i]["PJNUM"].ToString();//数量

                    wksheet.Cells[i + 6, 6] = dt.Rows[i]["DANZHONG"].ToString();//单重

                    wksheet.Cells[i + 6, 7] =dt.Rows[i]["ZONGZHONG"].ToString();//总重

                    wksheet.Cells[i + 6, 8] = "'" + dt.Rows[i]["JHSTATE"].ToString();//交货状态

                    wksheet.Cells[i + 6, 9] = "'" + dt.Rows[i]["CONT"].ToString();//检查内容

                    wksheet.Cells[i + 6, 10] = "'" + dt.Rows[i]["ZJREPORT"].ToString();//自检记录

                    wksheet.Cells[i + 6, 11] = "'" + dt.Rows[i]["RESULT"].ToString();//质检结果

                    wksheet.Cells[i + 6, 12] = "'" + dt.Rows[i]["ZGCONTENT"].ToString();//整改内容

                    wksheet.Cells[i + 6, 13] = "'" + dt.Rows[i]["QCMANNM"].ToString();//检验员

                    wksheet.Cells[i + 6, 14] = "'" + dt.Rows[i]["QCDATE"].ToString();//检验日期

                }

                wksheet.get_Range(wksheet.Cells[i + 6, 1], wksheet.Cells[i + 6, 14]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 6, 1], wksheet.Cells[i + 6, 14]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 6, 1], wksheet.Cells[i + 6, 14]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            }
            //设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = Server.MapPath("/QC_Data/ExportFile/" + "报检通知单" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        /// <summary>
        /// 输出Excel文件并退出
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="workbook"></param>
        //private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
        //{
        //    try
        //    {

        //        workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

        //        workbook.Close(Type.Missing, Type.Missing, Type.Missing);

        //        #region kill excel process

        //        System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcessesByName("EXCEL");

        //        foreach (System.Diagnostics.Process p in procs)
        //        {
        //            int baseAdd = p.MainModule.BaseAddress.ToInt32();

        //            //oXL is Excel.ApplicationClass object 

        //            if (baseAdd == m_xlApp.Hinstance)
        //            {
        //                p.Kill();
        //                break;
        //            }

        //        }

        //        #endregion

        //        workbook = null;
        //        m_xlApp = null;

        //        GC.Collect();

        //        if (File.Exists(filename))
        //        {
        //            DownloadFile.Send(Context, filename);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}



        protected void DropDownListEndResult_SelecedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListEndResult.SelectedValue == "1")
            {
                for (int i = 0; i < RepeaterItem.Items.Count; i++)
                {
                    (RepeaterItem.Items[i].FindControl("DropDownListResult") as DropDownList).ClearSelection();
                    (RepeaterItem.Items[i].FindControl("DropDownListResult") as DropDownList).Items.FindByValue("合格").Selected = true;
                }
            }
        }

        //protected void Filljhstate(object sender, EventArgs e)
        //{

        //    RepeaterItem a = ((System.Web.UI.WebControls.TextBox)sender).NamingContainer as RepeaterItem;
        //    string b = a.ItemIndex.ToString();
        //    if (b == "0")
        //    {
        //        System.Web.UI.WebControls.TextBox txt = (System.Web.UI.WebControls.TextBox)a.FindControl("TextBoxState");
        //        string jhstate = txt.Text.ToString();
        //        if (jhstate != "")
        //        {
        //            foreach (RepeaterItem s in RepeaterItem.Items)
        //            {
        //                System.Web.UI.WebControls.TextBox txt1 = (System.Web.UI.WebControls.TextBox)s.FindControl("TextBoxState");
        //                if (txt1.Text == "")
        //                {
        //                    txt1.Text = jhstate;
        //                }
        //            }
        //        }
        //    }




        //}

        protected void RepeaterItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (flag == "NEW")
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    string ptc = (e.Item.FindControl("LabelPTC") as System.Web.UI.WebControls.Label).Text;
                    string sqlsameptc = "select * from TBQM_APLYFORITEM where PTC='" + ptc + "' ";
                    System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqlsameptc);
                    if (dt1.Rows.Count > 0)
                    {
                        System.Web.UI.WebControls.Label iszj = e.Item.FindControl("Labeliszj") as System.Web.UI.WebControls.Label;
                        iszj.Text = "已报检";

                    }
                }
            }

            else if (e.Item.ItemType == ListItemType.Footer)
            {

                ((System.Web.UI.WebControls.Label)e.Item.FindControl("labelshuliang")).Text = hfdshuliang.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("labeldanzhong")).Text = hfddanzhong.Value;
                ((System.Web.UI.WebControls.Label)e.Item.FindControl("labelzongzhong")).Text = hfdzongzhong.Value;

            }

        }

        //protected void Fillzjcontent(object sender, EventArgs e)
        //{
        //    RepeaterItem a = ((System.Web.UI.WebControls.TextBox)sender).NamingContainer as RepeaterItem;
        //    string b = a.ItemIndex.ToString();
        //    if (b == "0")
        //    {
        //        System.Web.UI.WebControls.TextBox txt = (System.Web.UI.WebControls.TextBox)a.FindControl("TextBoxControlContent");
        //        string content = txt.Text.ToString();
        //        if (content != "")
        //        {
        //            foreach (RepeaterItem s in RepeaterItem.Items)
        //            {
        //                System.Web.UI.WebControls.TextBox txt1 = (System.Web.UI.WebControls.TextBox)s.FindControl("TextBoxControlContent");
        //                if (txt1.Text == "")
        //                {
        //                    txt1.Text = content;
        //                }
        //            }
        //        }
        //    }
        //}

        private void GetTotalAmount()
        {
            //string sql = "select isnull(CAST(sum(NUM) AS FLOAT),0) as SHULIANG,isnull(CAST(sum(DANZHONG) AS FLOAT),0) as DANZHONG, isnull(CAST(sum(ZONGZHONG) AS FLOAT),0) as ZONGZHONG from View_TBQM_APLYFORITEM WHERE UNIQUEID='" + hfduniqueid.Value + "' ";
            string sql = "select isnull(sum(NUM),0) as SHULIANG,isnull(sum(DANZHONG),0) as DANZHONG, isnull(sum(ZONGZHONG),0) as ZONGZHONG from View_TBQM_APLYFORITEM WHERE UNIQUEID='" + HiddenFieldQCUniqueID.Value + "' ";


            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);

            if (sdr.Read())
            {

                //hfdshuliang.Value = string.Format("{0:c2}", Convert.ToDouble(sdr["SHULIANG"]));
                //hfddanzhong.Value = string.Format("{0:c2}", Convert.ToDouble(sdr["DANZHONG"]));
                //hfdzongzhong.Value = string.Format("{0:c2}", Convert.ToDouble(sdr["ZONGZHONG"]));
                hfdshuliang.Value = sdr["SHULIANG"].ToString();
                hfddanzhong.Value = sdr["DANZHONG"].ToString();
                hfdzongzhong.Value = sdr["ZONGZHONG"].ToString();
            }
            sdr.Close();

        }

    }
    
}
