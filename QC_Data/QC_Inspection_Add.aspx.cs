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
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_Inspection_Add : System.Web.UI.Page
    {
        string flag = "";
        string id = "";
        string nm = "";
        string type = "";
        string sqladdfield = "";
        DataTable dtaddfield;
        DataView dvaddfield;
        DataTable dtaddfieldnew;
        string sqladdfield2 = "";
        DataTable dtaddfield2;
        DataView dvaddfield2;
        DataTable dtaddfieldnew2;
        protected void Page_Load(object sender, EventArgs e)
        {
            sqladdfield = "select ptcode,marid,marunit,length,width,pur_mashape from View_TBPC_PURCHASEPLAN ";
            dtaddfield = DBCallCommon.GetDTUsingSqlText(sqladdfield);
            dvaddfield = new DataView(dtaddfield);
            sqladdfield2 = "select mp_tracknum,mp_marid,isnull(mp_fixedsize,'')as mp_fixedsize from TBPM_MPPLAN ";
            dtaddfield2 = DBCallCommon.GetDTUsingSqlText(sqladdfield2);
            dvaddfield2 = new DataView(dtaddfield2);

            if (Request.QueryString["ACTION"] != null)
            {
                flag = Request.QueryString["ACTION"];
            }

            if (Request.QueryString["ID"] != null)
            {
                id = Request.QueryString["ID"];
            }
            ViewState["viewtable"] = "View_TBPC_PURORDERDETAIL_PLAN_TOTAL";
            if (Request.QueryString["TYPE"] != null)
            {
                type = Request.QueryString["TYPE"];
                if (type == "PUR")
                {
                    ViewState["viewtable"] = "View_TBPC_PURORDERDETAIL_PLAN_TOTAL";
                    ViewState["type"] = "pur";
                }
                else if (type == "WX")
                {
                    ViewState["type"] = "WX";
                }

            }

            if (Request.QueryString["NUM"] != null)
            {
                nm = Request.QueryString["NUM"];
            }

            if (!IsPostBack)
            {


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
            if (flag == "WXNEW")
            {
                hfduniqueid.Value = System.Guid.NewGuid().ToString();//用于报检父子条目同时插入

                BindNewInfo1();
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
                if (Session["POSITION"].ToString() != "1205" && Session["POSITION"].ToString() != "1201")//质量主管可以修改质检
                {
                    for (int i = 0; i < RepeaterItem.Items.Count; i++)
                    {
                        DropDownList DropDownListResult = RepeaterItem.Items[i].FindControl("DropDownListResult") as DropDownList;//检验结果
                        DropDownListResult.Enabled = false;
                        DropDownList DropDownListBHGITEM = RepeaterItem.Items[i].FindControl("DropDownListBHGITEM") as DropDownList;//不合格项
                        DropDownListBHGITEM.Enabled = false;

                        //2017.1.5修改,质检员不能修改每一项是否合格，只能修改总结果，陈永秀可以修改单项
                        CheckBox CheckBox1_cs = RepeaterItem.Items[i].FindControl("CheckBox1") as CheckBox;//不合格项
                        CheckBox1_cs.Enabled = false;
                    }
                }
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

                lbreport.Text = dt.Rows[0]["UNIQUEID"].ToString();

                LabelState.Text = dt.Rows[0]["AFI_STATE"].ToString();//状态
                TextBoxBJData.Text = dt.Rows[0]["AFI_DATE"].ToString();//报检时间
                LabelZJBH.Visible = true;
                LabelZJBH.Text = "质检编号：" + id.PadLeft(5, '0');
            }

        }


        private void BindNewInfo()
        {
            string sql = "select top(1) engid,pjnm,engnm,suppliernm,phono,conname,ptcode from " + ViewState["viewtable"] + " where PO_OperateState='PUSH" + Session["UserID"].ToString() + "'";

            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                IEnumerable<string> tsa_id = dt.Rows[0]["ptcode"].ToString().Split('_').Reverse<string>();
                LabelEngID.Text = tsa_id.ElementAt(2).ToString();

                string sqlt = "SELECT distinct  ISNULL(b.CM_PROJ,'') as proj,ISNULL(a.TSA_ENGNAME,'') as engname FROM TBCM_Basic a, TBCM_Plan b WHERE a.ID=b.ID and a.tsa_id='" + LabelEngID.Text + "' ";
                System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt1.Rows.Count > 0)
                {
                    LabelProName.Text = dt1.Rows[0]["proj"].ToString();
                    LabelEngName.Text = dt1.Rows[0]["engname"].ToString();
                }

                LabelSupplier.Text = dt.Rows[0]["suppliernm"].ToString();
                TextBoxContracter.Text = dt.Rows[0]["conname"].ToString();
                TextBoxTel.Text = dt.Rows[0]["phono"].ToString();
            }

            TextBoxBJData.Text = System.DateTime.Now.ToString("yyyy-MM-dd");//报检时间

            LabelDep.Text = Session["UserDept"].ToString();
            LabelMan.Text = Session["UserID"].ToString();
            LabelManName.Text = Session["UserName"].ToString();
            sql = "select marid,'" + System.DateTime.Now.ToString("yyyy-MM-dd") + "' AS BJSJ,'' AS ID,ptcode as PTC,marnm,PO_TUHAO as tuhao,margg,margb,marcz,case when marnm like '%把钉%' then fznum else num end as cgnum,case when marnm like '%把钉%' then cast(num/fznum as decimal(18,4)) else cast(fznum/num as decimal(18,4)) end as singlewgh,case when marnm like '%把钉%' then num else fznum end as sumwgh,po_pz as pjpz, num as bjnum,'' as CONT,'' as JHSTATE,'' as QCNUM, '' as ZJREPORT,'' as ZGCONTENT, '' as BHGITEM,'' as RESULT, '' as QCMANNM,'' as QCDATE,'1' as ISAGAIN,'0' as ISRESULT,detailnote as note from  " + ViewState["viewtable"] + "  where PO_OperateState='PUSH" + Session["UserID"].ToString() + "'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                RepeaterItem.DataSource = dt;
                RepeaterItem.DataBind();
            }
            sql = "UPDATE TBPC_PURORDERDETAIL SET PO_OperateState=NULL WHERE  PO_OperateState='PUSH" + Session["UserID"].ToString() + "'";

            DBCallCommon.ExeSqlText(sql);
        }
        private void BindNewInfo1()
        {
            //string sql = "select top(1) engid,pjnm,engnm,suppliernm,phono,conname,ptcode from View_TBMP_IQRCMPPRICE  where PO_OperateState='PUSH" + Session["UserID"].ToString() + "'";
            string sql = "select top(1) engid,pjnm,engnm,supplierresnm,PTC as ptcode from View_TBMP_IQRCMPPRICE  where PO_OperateState='PUSH" + Session["UserID"].ToString() + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                // IEnumerable<string> tsa_id = dt.Rows[0]["ptcode"].ToString().Split('_').Reverse<string>();
                // LabelEngID.Text = tsa_id.ElementAt(0).ToString();
                LabelEngID.Text = dt.Rows[0]["engid"].ToString();
                string sqlt = "SELECT distinct  ISNULL(b.CM_PROJ,'') as proj,ISNULL(a.TSA_ENGNAME,'') as engname FROM TBCM_Basic a, TBCM_Plan b WHERE a.ID=b.ID and a.tsa_id='" + LabelEngID.Text + "' ";
                System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt1.Rows.Count > 0)
                {
                    LabelProName.Text = dt1.Rows[0]["proj"].ToString();
                    LabelEngName.Text = dt1.Rows[0]["engname"].ToString();
                }
                LabelSupplier.Text = dt.Rows[0]["supplierresnm"].ToString();
                //TextBoxContracter.Text = dt.Rows[0]["conname"].ToString();
                //TextBoxTel.Text = dt.Rows[0]["phono"].ToString();
            }
            TextBoxBJData.Text = System.DateTime.Now.ToString("yyyy-MM-dd");//报检时间
            LabelDep.Text = Session["UserDept"].ToString();
            LabelMan.Text = Session["UserID"].ToString();
            LabelManName.Text = Session["UserName"].ToString();
            sql = "select '" + System.DateTime.Now.ToString("yyyy-MM-dd") + "' AS BJSJ,'' AS ID,''as marid, PTC, PIC_MNAME as marnm,PIC_TUHAO as tuhao,PIC_GUIGE AS margg,margb,PIC_CAIZHI AS marcz,marzxnum as cgnum, MS_UWGHT as singlewgh,MS_TLWGHT as sumwgh,marzxnum as bjnum,'' as CONT,'' as JHSTATE,'' as QCNUM, '' as ZJREPORT,'' as ZGCONTENT,'' as BHGITEM, '' as RESULT, '' as QCMANNM,'' as QCDATE,'1' as ISAGAIN,'0' as ISRESULT,'' as note,'' as pjpz from View_TBMP_IQRCMPPRICE  where PO_OperateState='PUSH" + Session["UserID"].ToString() + "'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                RepeaterItem.DataSource = dt;
                RepeaterItem.DataBind();
            }
            sql = "UPDATE TBMP_IQRCMPPRICE  SET PO_OperateState=NULL WHERE  PO_OperateState='PUSH" + Session["UserID"].ToString() + "'";
            DBCallCommon.ExeSqlText(sql);

        }
        private void BindViewInfo()
        {
            HeaderInfo();
            GetTotalAmount();
            string sql = "select ID,PTC,PARTNM AS marnm,TUHAO AS tuhao,margg,margb,marcz,NUM AS cgnum,DANZHONG AS singlewgh,ZONGZHONG AS sumwgh,PJNUM as bjnum,PJPZ as pjpz,CONT,JHSTATE,QCNUM,ZJREPORT,ZGCONTENT,isnull(BHGITEM,'')as BHGITEM,isnull(RESULT,'') as RESULT,QCMANNM,QCDATE,ISAGAIN,ISRESULT,BJSJ,marid,note from View_TBQM_APLYFORITEM  where UNIQUEID='" + HiddenFieldQCUniqueID.Value + "'";
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
            //for (int i = 0; i < RepeaterItem.Items.Count; i++)
            //{
            //    string IsAgain = (RepeaterItem.Items[i].FindControl("LabelAgain") as System.Web.UI.WebControls.Label).Text;
            //    string IsResult = (RepeaterItem.Items[i].FindControl("LabelIsResult") as System.Web.UI.WebControls.Label).Text;
            //    if (Convert.ToInt32(IsAgain) < ltcount.Max() && IsResult == "1")
            //    {
            //        //表示条目已质检而且是过去报检

            //        (RepeaterItem.Items[i].FindControl("TextBoxQCNum") as System.Web.UI.WebControls.TextBox).Enabled = false;

            //        (RepeaterItem.Items[i].FindControl("TextBoxReport") as System.Web.UI.WebControls.TextBox).Enabled = false;

            //        (RepeaterItem.Items[i].FindControl("DropDownListResult") as DropDownList).Enabled = false;

            //        (RepeaterItem.Items[i].FindControl("TextBoxContent") as System.Web.UI.WebControls.TextBox).Enabled = false;
            //        (RepeaterItem.Items[i].FindControl("TextBoxQCMan") as System.Web.UI.WebControls.TextBox).Enabled = false;
            //        (RepeaterItem.Items[i].FindControl("TextBoxDate") as System.Web.UI.WebControls.TextBox).Enabled = false;
            //    }
            //}

        }




        private void InspecNewResult()
        {
            string sql = "SELECT * FROM TBQM_APLYFORINSPCTRESULT WHERE UNIQUEID='" + HiddenFieldQCUniqueID.Value + "' AND ISR_ISNEW='1'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                LabelRecoder.Text = dt.Rows[0]["ISR_INSPCTOR"].ToString();

                DropDownListEndResult.Items.FindByValue(dt.Rows[0]["ISR_RESULT"].ToString()).Selected = true;


                GVBind(HiddenFieldQCUniqueID.Value);
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
                string bhgitem = (RepeaterItem.Items[i].FindControl("DropDownListBHGITEM") as DropDownList).SelectedValue;//不合格项
                string zgcontenyt = (RepeaterItem.Items[i].FindControl("TextBoxContent") as System.Web.UI.WebControls.TextBox).Text;//整改内容
                string iteminspec = Session["UserID"].ToString();//质检人ID
                string iteminspecor = (RepeaterItem.Items[i].FindControl("TextBoxQCMan") as System.Web.UI.WebControls.TextBox).Text;//质检人姓名
                string itemdate = (RepeaterItem.Items[i].FindControl("TextBoxDate") as System.Web.UI.WebControls.TextBox).Text;//日期
                int j = i;
                j++;
                qcmannm = iteminspecor;
                bodymx2 += "明细" + j + "\n" + "计划跟踪号:" + ptc + "\n" + "子项名称：" + marnm + "\n" + "图号：" + tuhao + "\n" + "规格：" + gg + "\n" + "材质：" + cz + "\n" + "国标：" + gb + "\n" + "报检数量：" + bjnum + "\n" + "交货状态：" + jhstate + "\n" + "检查内容：" + cont + "\n" + "检查地点：" + TextBoxSite.Text + "\n";
                bodymx2 += "质检数量：" + num + "\n" + "自检报告：" + selfrecord + "\n" + "检验结果：" + itemResult + "不合格项：" + bhgitem + "\n" + "整改内容：" + zgcontenyt + "\n" + "质检人：" + iteminspecor + "\n" + "检查日期：" + itemdate + "\n";
            }

            //string to = "erp@ztsm.net";

            //List<string> bjEmailCC = new List<string>();
            //bjEmailCC.Add("erp@ztsm.net");



            //List<string> zjEmailCC = new List<string>();
            //zjEmailCC.Add("erp@ztsm.net");




            //sql = "select EMail from TBDS_STAFFINFO where ST_PD='0' and ST_ID like '0702%' ";
            //dt = DBCallCommon.GetDTUsingSqlText(sql);
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count - 1; i++)
            //    {
            //        zjEmailCC.Add(dt.Rows[i][0].ToString());

            //    }

            //}

            //List<string> mfEmail = null;


            //新的报检单
            if (flag == "NEW" || flag == "WXNEW")
            {
                SubmitCGInfo();


                //    body = "新的质量报检任务" + "\n" + "项  目  为:" + LabelProName.Text + "\n" + "设 备 为：" + LabelEngName.Text + "\n" + "供货单位为：" + LabelSupplier.Text + "\n" + "报  检  人：" + LabelManName.Text + "\n" + "报检部件为：" + TextBoxPartName.Text + "\n" + "任务号为：" + LabelEngID.Text;
                //    returnvalue = DBCallCommon.SendEmail(to, bjEmailCC, mfEmail, LabelProName.Text + "-" + LabelEngName.Text + "/" + TextBoxPartName.Text + "数字平台质量报检", body);

            }

            //二次报检
            if (flag == "UPDATE")
            {
                string ret = SubmitUpdateInfo();
                //    if (ret == "yes")
                //    {
                //        body = "新的二次质量报检任务" + "\n" + "质检编号为：" + zjbh + "\n" + "设 备 为:" + LabelProName.Text + "\n" + "工  程  为：" + LabelEngName.Text + "\n" + "供货单位为：" + LabelSupplier.Text + "\n" + "报  检  人：" + LabelManName.Text + "\n" + "报检部件为：" + TextBoxPartName.Text + "\n" + "任务号为：" + LabelEngID.Text;
                //        returnvalue = DBCallCommon.SendEmail(to, bjEmailCC, mfEmail, LabelProName.Text + "-" + LabelEngName.Text + "/" + TextBoxPartName.Text + "数字平台质量二次报检", body);
            }



            //}

            //质检人结果反馈
            if (flag == "INSPEC")
            {
                SubmitInspecInfo();
                //    body = "质量报检结果反馈" + "\n" + "质检编号为：" + zjbh + "\n" + "项  目  为:" + LabelProName.Text + "\n" + "设 备 为：" + LabelEngName.Text + "\n" + "供货单位为：" + LabelSupplier.Text + "\n" + "报  检  人：" + LabelManName.Text + "\n" + "报检部件为：" + TextBoxPartName.Text + "\n" + "质  检  人：" + qcmannm + "\n" + "质检结果为：" + DropDownListEndResult.SelectedItem + "\n" + "任务号：" + LabelEngID.Text + "\n" + bodymx2;

                //    returnvalue = DBCallCommon.SendEmail(zdrEmail, zjEmailCC, mfEmail, LabelProName.Text + "-" + LabelEngName.Text + "/" + TextBoxPartName.Text + "数字平台质量报检结果反馈" + DropDownListEndResult.SelectedItem, body);

            }

            //修改质检结果
            if (flag == "INUPDATE")
            {
                string ret = SubmitInUpdateInfo();
                //    if (ret == "yes")
                //    {

                //        body = "质量报检结果反馈" + "\n" + "质检编号为：" + zjbh + "\n" + "项  目  为:" + LabelProName.Text + "\n" + "设 备 为：" + LabelEngName.Text + "\n" + "供货单位为：" + LabelSupplier.Text + "\n" + "报  检  人：" + LabelManName.Text + "\n" + "报检部件为：" + TextBoxPartName.Text + "\n" + "质  检  人：" + qcmannm + "\n" + "质检结果为：" + DropDownListEndResult.SelectedItem + "\n" + "任务号：" + LabelEngID.Text + "\n" + bodymx2;

                //        returnvalue = DBCallCommon.SendEmail(zdrEmail, zjEmailCC, mfEmail, LabelProName.Text + "-" + LabelEngName.Text + "/" + TextBoxPartName.Text + "数字平台质量报检结果反馈" + DropDownListEndResult.SelectedItem, body);
                //    }
            }

            //if (returnvalue == "邮件已发送!")
            //{
            //    //string jascript = @"alert('邮件发送成功!');";

            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", jascript, true);

            //    Response.Write("<script>alert('邮件发送成功');</script>");
            //    Response.Redirect("QC_Inspection_Manage.aspx", false);

            //}
            //else if (returnvalue == "邮件发送失败!")
            //{
            //    Response.Write("<script>alert('邮件发送不成功');</script>");

            //}

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
                sql = "UPDATE TBQM_APLYFORINSPCTRESULT SET ISR_RESULT='" + DropDownListEndResult.SelectedValue + "',ISR_DATE='" + System.DateTime.Now.ToString("yyyy-MM-dd") + "',ISR_INSPCT='" + Session["UserID"].ToString() + "',ISR_INSPCTOR='" + Session["UserName"].ToString() + "',ISR_INSPCTDSCP='" + TextBoxDecp.Text + "',ISR_NOTE='" + TextBoxResultNote.Text + "'WHERE UNIQUEID='" + HiddenFieldQCUniqueID.Value + "' AND ISR_ISNEW='1'";
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
                        string bhgitem = (RepeaterItem.Items[i].FindControl("DropDownListBHGITEM") as DropDownList).SelectedValue;//不合格项
                        string iteminspec = Session["UserID"].ToString();//质检人ID
                        string iteminspecor = (RepeaterItem.Items[i].FindControl("TextBoxQCMan") as System.Web.UI.WebControls.TextBox).Text;//质检人姓名
                        string itemdate = (RepeaterItem.Items[i].FindControl("TextBoxDate") as System.Web.UI.WebControls.TextBox).Text;//日期
                        string content = (RepeaterItem.Items[i].FindControl("TextBoxControlContent") as System.Web.UI.WebControls.TextBox).Text;//检查内容
                        //  string sql1 = "select distinct uniqueid from TBQM_APLYFORITEM  where PTC='" + ptc + "' ";
                        //  System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
                        //if (dt1.Rows.Count > 1)
                        //{
                        //    string sql2 = "select top 1 UNIQUEID as UNIQUEID from  TBQM_APLYFORITEM where PTC='" + ptc + "' order by ID desc ";
                        //    System.Data.DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql2);
                        //    string ID = dt2.Rows[0]["UNIQUEID"].ToString();
                        //    if (ID != HiddenFieldQCUniqueID.Value)
                        //    {
                        //        int m = i + 1;
                        //        string script = @"alert('第[" + m + "]条质检的计划跟踪号已经重新报检，此处不是最新报检，不能修改!');";
                        //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", script, true);
                        //        retvalue = "no";

                        //        return retvalue;
                        //    }
                        //}

                        sql = "UPDATE TBPC_PURORDERDETAIL SET PO_CGFS='" + itemResult + "' WHERE PO_PCODE='" + ptc + "'";
                        sqlcount.Add(sql);

                        sql = "UPDATE TBMP_IQRCMPPRICE SET PIC_BJSTATUS='" + itemResult + "' WHERE PTC='" + ptc + "'";
                        sqlcount.Add(sql);


                        sql = "UPDATE TBQM_APLYFORITEM SET QCNUM='" + num + "',ZJREPORT='" + selfrecord + "',BHGITEM='" + bhgitem + "',ZGCONTENT='" + zgcontenyt + "',RESULT='" + itemResult + "',QCMAN='" + iteminspec + "',QCMANNM='" + iteminspecor + "',QCDATE='" + itemdate + "',CONT='" + content + "' WHERE ID='" + key + "'";
                        sqlcount.Add(sql);
                    }
                }



                DBCallCommon.ExecuteTrans(sqlcount);
                Response.Write("<script>alert('保存成功');</script>");
                //  Response.Redirect("QC_Inspection_Add.aspx?ACTION=VIEW&&ID=" + id, false);
            }
            else
            {
                //重新质检
                List<string> sqlcount = new List<string>();

                //更新最新的质检记录的状态
                string sql = "UPDATE TBQM_APLYFORINSPCTRESULT SET ISR_ISNEW='0' WHERE UNIQUEID='" + HiddenFieldQCUniqueID.Value + "' AND ISR_ISNEW='1'";
                sqlcount.Add(sql);
                //插入新的质检记录，并标识为最新的
                sql = "INSERT INTO TBQM_APLYFORINSPCTRESULT (UNIQUEID,ISR_RESULT,ISR_DATE,ISR_INSPCT,ISR_INSPCTOR,ISR_INSPCTDSCP,ISR_ISNEW,ISR_NOTE)";
                sql += " VALUES  ('" + HiddenFieldQCUniqueID.Value + "','" + DropDownListEndResult.SelectedValue + "','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "','" + Session["UserID"].ToString() + "','" + Session["UserName"].ToString() + "','" + TextBoxDecp.Text + "','1','" + TextBoxResultNote.Text + "')";
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
                        string bhgitem = (RepeaterItem.Items[i].FindControl("DropDownListBHGITEM") as DropDownList).SelectedValue;//不合格项
                        string zgcontenyt = (RepeaterItem.Items[i].FindControl("TextBoxContent") as System.Web.UI.WebControls.TextBox).Text;//整改内容
                        string iteminspec = Session["UserID"].ToString();//质检人ID
                        string iteminspecor = (RepeaterItem.Items[i].FindControl("TextBoxQCMan") as System.Web.UI.WebControls.TextBox).Text;//质检人姓名
                        string itemdate = (RepeaterItem.Items[i].FindControl("TextBoxDate") as System.Web.UI.WebControls.TextBox).Text;//日期






                        sql = "UPDATE TBPC_PURORDERDETAIL SET PO_CGFS='" + itemResult + "' WHERE PO_PCODE='" + ptc + "'";
                        sqlcount.Add(sql);


                        sql = "UPDATE TBMP_IQRCMPPRICE SET PIC_BJSTATUS='" + itemResult + "' WHERE  PTC='" + ptc + "'";
                        sqlcount.Add(sql);




                        sql = "UPDATE TBQM_APLYFORITEM SET QCNUM='" + num + "',ZJREPORT='" + selfrecord + "',BHGITEM='" + bhgitem + "',ZGCONTENT='" + zgcontenyt + "',RESULT='" + itemResult + "',QCMAN='" + iteminspec + "',QCMANNM='" + iteminspecor + "',QCDATE='" + itemdate + "',ISRESULT='1' WHERE ID='" + key + "'";
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
                    //  string sw = (RepeaterItem.Items[i].FindControl("LabelSW") as System.Web.UI.WebControls.Label).Text;
                    // string sumw = (RepeaterItem.Items[i].FindControl("LabelSumW") as System.Web.UI.WebControls.Label).Text;
                    string sw = "";
                    string sumw = "";
                    string pjnum = (RepeaterItem.Items[i].FindControl("TextBoxPJNum") as System.Web.UI.WebControls.TextBox).Text;
                    string pjpz = (RepeaterItem.Items[i].FindControl("TextBoxPJPZ") as System.Web.UI.WebControls.TextBox).Text;
                    string content = (RepeaterItem.Items[i].FindControl("TextBoxControlContent") as System.Web.UI.WebControls.TextBox).Text;
                    string jhstate = (RepeaterItem.Items[i].FindControl("TextBoxState") as System.Web.UI.WebControls.TextBox).Text;

                    sql = "UPDATE TBQM_APLYFORITEM SET PARTNM='" + marnm + "',TUHAO='" + drawing + "',DANZHONG='" + sw + "',NUM='" + num + "',ZONGZHONG='" + sumw + "',PJNUM='" + pjnum + "',CONT='" + content + "',JHSTATE='" + jhstate + "',PJPZ='" + pjpz + "' WHERE ID='" + key + "'";

                    sqlcount.Add(sql);
                    //}
                }
                DBCallCommon.ExecuteTrans(sqlcount);
                Response.Write("<script>alert('保存成功');</script>");
                ///    Response.Redirect("QC_Inspection_Add.aspx?ACTION=VIEW&&ID=" + id, false);

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
                        //   string sw = (RepeaterItem.Items[i].FindControl("LabelSW") as System.Web.UI.WebControls.Label).Text;
                        // string sumw = (RepeaterItem.Items[i].FindControl("LabelSumW") as System.Web.UI.WebControls.Label).Text;
                        string sw = "";
                        string sumw = "";
                        string pjnum = (RepeaterItem.Items[i].FindControl("TextBoxPJNum") as System.Web.UI.WebControls.TextBox).Text;
                        string content = (RepeaterItem.Items[i].FindControl("TextBoxControlContent") as System.Web.UI.WebControls.TextBox).Text;
                        string jhstate = (RepeaterItem.Items[i].FindControl("TextBoxState") as System.Web.UI.WebControls.TextBox).Text;

                        //将其报检项设置为第二次报检
                        sql = "INSERT INTO TBQM_APLYFORITEM (UNIQUEID,PARTNM,TUHAO ,NUM,DANZHONG,ZONGZHONG ,PJNUM,CONT,JHSTATE,ISAGAIN,PTC,BJSJ,RESULT)";
                        sql += "  VALUES ('" + HiddenFieldQCUniqueID.Value + "','" + marnm + "','" + drawing + "','" + num + "','" + sw + "','" + sumw + "','" + pjnum + "','" + content + "','" + jhstate + "','" + IsAgain + "','" + ptc + "','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "','待检')";

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

        //质检
        private void SubmitInspecInfo()
        {

            if (DropDownListEndResult.SelectedValue == "" || DropDownListEndResult.SelectedValue == "2")
            {
                Response.Write("<script>alert('质检结果请选择合格或者不合格！');</script>");
                return;
            }

            List<string> sqlcount = new List<string>();
            string sql = "INSERT INTO TBQM_APLYFORINSPCTRESULT (UNIQUEID,ISR_RESULT,ISR_DATE,ISR_INSPCT,ISR_INSPCTOR,ISR_INSPCTDSCP,ISR_ISNEW,ISR_NOTE)";
            sql += " VALUES  ('" + HiddenFieldQCUniqueID.Value + "','" + DropDownListEndResult.SelectedValue + "','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "','" + Session["UserID"].ToString() + "','" + Session["UserName"].ToString() + "','" + TextBoxDecp.Text + "','1','" + TextBoxResultNote.Text + "')";
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
                string bhgitem = (RepeaterItem.Items[i].FindControl("DropDownListBHGITEM") as DropDownList).SelectedValue;//不合格项
                string zgcontenyt = (RepeaterItem.Items[i].FindControl("TextBoxContent") as System.Web.UI.WebControls.TextBox).Text;//整改内容
                string iteminspec = Session["UserID"].ToString();//质检人ID
                string iteminspecor = (RepeaterItem.Items[i].FindControl("TextBoxQCMan") as System.Web.UI.WebControls.TextBox).Text;//质检人姓名
                string itemdate = (RepeaterItem.Items[i].FindControl("TextBoxDate") as System.Web.UI.WebControls.TextBox).Text;//日期
                string content = (RepeaterItem.Items[i].FindControl("TextBoxControlContent") as System.Web.UI.WebControls.TextBox).Text;//质检内容


                if (iteminspecor == "")
                {
                    Response.Write("<script>alert('第" + (i + 1).ToString() + "行请填写质检人姓名！');</script>");
                    return;
                }
                if (itemdate == "")
                {
                    Response.Write("<script>alert('第" + (i + 1).ToString() + "行请填写检查日期！');</script>");
                    return;
                }

                sql = "UPDATE TBPC_PURORDERDETAIL SET PO_CGFS='" + itemResult + "' WHERE PO_PCODE='" + ptc + "'";
                sqlcount.Add(sql);

                sql = "UPDATE TBMP_IQRCMPPRICE SET PIC_BJSTATUS='" + itemResult + "' WHERE  PTC='" + ptc + "'";
                sqlcount.Add(sql);

                sql = "UPDATE TBQM_APLYFORITEM SET QCNUM='" + num + "',ZJREPORT='" + selfrecord + "',BHGITEM='" + bhgitem + "',ZGCONTENT='" + zgcontenyt + "',RESULT='" + itemResult + "',QCMAN='" + iteminspec + "',QCMANNM='" + iteminspecor + "',QCDATE='" + itemdate + "',ISRESULT='1',CONT='" + content + "' WHERE ID='" + key + "'";
                sqlcount.Add(sql);
            }
            DBCallCommon.ExecuteTrans(sqlcount);
            Response.Write("<script>alert('保存成功');</script>");
            //  Response.Redirect("QC_Inspection_Add.aspx?ACTION=VIEW&&ID=" + id, false);

        }
        private void SubmitCGInfo()
        {
            string sql = "";
            string qcState = "0";
            string qcMan = "";
            string qcManName = "";
            //string qcManName = "";
            if (ViewState["type"].ToString() == "pur")
            {
                string marid = (RepeaterItem.Items[0].FindControl("TextMarid") as System.Web.UI.WebControls.TextBox).Text.Trim();
                string maridshort = marid.Split('.')[0] + "." + marid.Split('.')[1];
                //for (int i = 0; i < RepeaterItem.Items.Count; i++)
                //{
                //    string marid1 = (RepeaterItem.Items[i].FindControl("TextMarid") as System.Web.UI.WebControls.TextBox).Text.Trim();
                //    string maridshort1 = marid1.Split('.')[0] + "." + marid1.Split('.')[1];
                //    if (maridshort1 != maridshort)
                //    {
                //        Response.Write("<script>alert('请选择同一类物料编码进行报检！');return;</script>");
                //        return;
                //    }
                //}
                sql = "select IsDiret,InspectPerson,ST_NAME from View_TBQC_SetInspectPerson where num='" + maridshort + "'";
                DataTable dtSetPer = DBCallCommon.GetDTUsingSqlText(sql);
                if (dtSetPer.Rows.Count > 0)
                {
                    qcState = dtSetPer.Rows[0][0].ToString();
                    qcMan = dtSetPer.Rows[0][1].ToString();
                    qcManName = dtSetPer.Rows[0][2].ToString();
                }
                else
                {   //如果不是需要质检部质检的物料，则有报检人自己质检
                    string ptCode = (RepeaterItem.Items[0].FindControl("LabelPTC") as System.Web.UI.WebControls.Label).Text.Trim();
                    string sqlText = "select * from View_TBPC_PLAN_PLACE where Aptcode='" + ptCode + "' ";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                    if (dt.Rows.Count > 0)
                    {
                        qcState = "1";
                        qcMan = dt.Rows[0]["sqrid"].ToString();
                        qcManName = dt.Rows[0]["sqrnm"].ToString();
                    }

                }

            }
            else
            {
                //sql = "select QSA_QCCLERK,QSA_QCCLERKNM from dbo.TBQM_QTSASSGN where QSA_ENGID='" + LabelEngID.Text.Trim() + "' ";
                //System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);


                //if (dt.Rows.Count>0)
                //{
                //    if (!string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                //    {
                //        qcMan = dt.Rows[0][0].ToString();
                //        qcManName = dt.Rows[0][1].ToString();
                //        qcState = "1";
                //    }
                //}          
                qcMan = "";
                qcManName = "";
                qcState = "0";

            }



            List<string> sqlcount = new List<string>();

            sql = "DELETE FROM TBQM_APLYFORINSPCT WHERE UNIQUEID='" + hfduniqueid.Value + "'";

            sqlcount.Add(sql);

            sql = "DELETE FROM TBQM_APLYFORITEM WHERE UNIQUEID='" + hfduniqueid.Value + "'";

            sqlcount.Add(sql);

            sql = "INSERT INTO TBQM_APLYFORINSPCT (AFI_TSDEP,AFI_PJNAME,AFI_ENGID,AFI_ENGNAME,AFI_PARTNAME ,AFI_DATACLCT ,AFI_DATE,AFI_RQSTCDATE,AFI_SUPPLERNM,AFI_ISPCTSITE,AFI_CONTACT,AFI_CONTEL,AFI_MAN,AFI_MANNM,AFI_NOTE,AFI_NUMBER,UNIQUEID,AFI_QCMAN,AFI_QCMANNM,AFI_ASSGSTATE)";
            sql += " VALUES  ('" + LabelDep.Text + "','" + LabelProName.Text + "','" + LabelEngID.Text + "','" + LabelEngName.Text + "','" + TextBoxPartName.Text + "','" + TextBoxData.Text + "','" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + TextBoxDate.Text + "','" + LabelSupplier.Text + "','" + TextBoxSite.Text + "','" + TextBoxContracter.Text + "','" + TextBoxTel.Text + "','" + LabelMan.Text + "','" + LabelManName.Text + "','" + TextBoxNote.Text + "',1,'" + hfduniqueid.Value + "','" + qcMan + "','" + qcManName + "','" + qcState + "')";

            sqlcount.Add(sql);

            for (int i = 0; i < RepeaterItem.Items.Count; i++)
            {
                string ptc = (RepeaterItem.Items[i].FindControl("LabelPTC") as System.Web.UI.WebControls.Label).Text;
                string marnm = (RepeaterItem.Items[i].FindControl("LabelMarName") as System.Web.UI.WebControls.Label).Text;
                string marid = (RepeaterItem.Items[i].FindControl("TextMarid") as System.Web.UI.WebControls.TextBox).Text;
                string drawing = (RepeaterItem.Items[i].FindControl("TextBoxDrawingNO") as System.Web.UI.WebControls.TextBox).Text;
                string num = (RepeaterItem.Items[i].FindControl("LabelNum") as System.Web.UI.WebControls.Label).Text;
                //    string sw = (RepeaterItem.Items[i].FindControl("LabelSW") as System.Web.UI.WebControls.Label).Text;
                //   string sumw = (RepeaterItem.Items[i].FindControl("LabelSumW") as System.Web.UI.WebControls.Label).Text;
                string sw = "";
                string sumw = "";
                string pjnum = (RepeaterItem.Items[i].FindControl("TextBoxPJNum") as System.Web.UI.WebControls.TextBox).Text;
                string note = (RepeaterItem.Items[i].FindControl("TextBoxPJNote") as System.Web.UI.WebControls.TextBox).Text;
                string content = (RepeaterItem.Items[i].FindControl("TextBoxControlContent") as System.Web.UI.WebControls.TextBox).Text;
                string jhstate = (RepeaterItem.Items[i].FindControl("TextBoxState") as System.Web.UI.WebControls.TextBox).Text;
                string pjpz = (RepeaterItem.Items[i].FindControl("TextBoxPJPZ") as System.Web.UI.WebControls.TextBox).Text;
                if (pjnum == "" || jhstate == "")
                {
                    Response.Write("<script>alert('第" + i + 1 + "行交货状态和数量不允许为空！');return;</script>");
                    return;
                }

                sql = "INSERT INTO TBQM_APLYFORITEM (UNIQUEID,PTC,PARTNM,TUHAO,NUM,DANZHONG,ZONGZHONG ,PJNUM,CONT,JHSTATE,ISAGAIN,BJSJ,Marid,RESULT,note,PJPZ)";
                sql += "  VALUES ('" + hfduniqueid.Value + "','" + ptc + "','" + marnm + "','" + drawing + "','" + num + "','" + sw + "','" + sumw + "','" + pjnum + "','" + content + "','" + jhstate + "','0','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "','" + marid + "','待检','" + note + "','" + pjpz + "')";

                sqlcount.Add(sql);

                sql = "UPDATE TBPC_PURORDERDETAIL SET PO_CGFS='待检' WHERE  PO_PCODE='" + ptc + "'";
                sqlcount.Add(sql);

                sql = "UPDATE TBMP_IQRCMPPRICE SET PIC_BJSTATUS='待检' WHERE  PTC='" + ptc + "'";
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

        private int IntIsUF = 0;
        /// <summary>
        /// 重点在于要给合同文本内容赋值BC_CONTEXT
        /// </summary>
        private void uploafFile()
        {
            //获取文件保存的路径 

            /************************************************************/
            //根据质检报告的类型来选择存储方式
            string filePath = @"E:\质量管理附件\质量报检";
            /************************************************************/
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            HttpPostedFile hpf = FileUploadupdate.PostedFile;

            try
            {
                string fileContentType = hpf.ContentType;// 获取客户端发送的文件的 MIME 内容类型  

                if (fileContentType == "application/msword" || fileContentType == "application/vnd.ms-excel" || fileContentType == "application/pdf" || fileContentType == "image/jpeg" || fileContentType == "image/jpg" || fileContentType == "image/gif" || fileContentType == "image/png" || fileContentType == "image/bmp" || fileContentType == "application/octet-stream" || fileContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")//传送文件类型
                {
                    if (hpf.ContentLength > 0)
                    {
                        if (hpf.ContentLength < (1024 * 1024 * 5))
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
                                    sqlStr += "values('" + HiddenFieldQCUniqueID.Value + "'";
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
                            filesError.Text = "上传文件过大，上传文件必须小于5M!";
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
                GVBind(HiddenFieldQCUniqueID.Value);
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
                GVBind(HiddenFieldQCUniqueID.Value);
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

            string sql = "select PTC,PARTNM,'',TUHAO,NUM,DANZHONG,ZONGZHONG,margg,marcz,margb,JHSTATE,RESULT,BHGITEM,ZGCONTENT,QCDATE,NOTE from View_TBQM_APLYFORITEM  where UNIQUEID='" + HiddenFieldQCUniqueID.Value + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            sql = "select AFI_PJNAME,AFI_ENGID,AFI_ENGNAME,AFI_TSDEP,AFI_MANNM,AFI_PARTNAME,AFI_SUPPLERNM,AFI_ISPCTSITE,AFI_CONTACT,AFI_CONTEL from TBQM_APLYFORINSPCT where AFI_ID='" + id + "'";
            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
            ExportData(dt, dt1);
        }

        private void ExportData(System.Data.DataTable objdt, System.Data.DataTable dt1)
        {

            string filename = dt1.Rows[0]["AFI_PJNAME"] + "_" + dt1.Rows[0]["AFI_ENGID"] + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("报检通知单模版.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet = wk.GetSheetAt(0);

                IRow row0 = sheet.GetRow(0);
                row0.GetCell(12).SetCellValue(dt1.Rows[0]["AFI_ENGID"].ToString());
                IRow row1 = sheet.GetRow(1);
                row1.GetCell(2).SetCellValue(dt1.Rows[0]["AFI_PJNAME"].ToString());
                row1.GetCell(6).SetCellValue(dt1.Rows[0]["AFI_ENGNAME"].ToString());
                row1.GetCell(9).SetCellValue(dt1.Rows[0]["AFI_TSDEP"].ToString());
                row1.GetCell(12).SetCellValue(dt1.Rows[0]["AFI_MANNM"].ToString());
                IRow row2 = sheet.GetRow(2);
                row2.GetCell(2).SetCellValue(dt1.Rows[0]["AFI_PARTNAME"].ToString());
                row2.GetCell(6).SetCellValue(dt1.Rows[0]["AFI_SUPPLERNM"].ToString());
                row2.GetCell(9).SetCellValue(dt1.Rows[0]["AFI_ISPCTSITE"].ToString());
                row2.GetCell(12).SetCellValue(dt1.Rows[0]["AFI_CONTACT"].ToString() + "/" + dt1.Rows[0]["AFI_CONTEL"].ToString());
                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    IRow row = sheet.GetRow(i + 5);
                    for (int j = 0; j < objdt.Columns.Count; j++)
                    {
                        row.GetCell(j).SetCellValue(objdt.Rows[i][j].ToString());
                    }
                }
                sheet.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }

        }




        protected void DropDownListEndResult_SelecedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownListEndResult.SelectedValue == "1")
            {
                for (int i = 0; i < RepeaterItem.Items.Count; i++)
                {
                    string IsResult = (RepeaterItem.Items[i].FindControl("LabelIsResult") as System.Web.UI.WebControls.Label).Text;

                    if (IsResult != "1")
                    {
                        (RepeaterItem.Items[i].FindControl("DropDownListResult") as DropDownList).ClearSelection();
                        (RepeaterItem.Items[i].FindControl("DropDownListResult") as DropDownList).Items.FindByValue("合格").Selected = true;
                        (RepeaterItem.Items[i].FindControl("DropDownListBHGITEM") as DropDownList).ClearSelection();
                        (RepeaterItem.Items[i].FindControl("DropDownListBHGITEM") as DropDownList).Items.FindByValue("无").Selected = true;
                    }
                }
            }
        }


        protected void RepeaterItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label LabelPTC = (Label)e.Item.FindControl("LabelPTC");
                TextBox TextMarid = (TextBox)e.Item.FindControl("TextMarid");
                dvaddfield.RowFilter = "ptcode='" + LabelPTC.Text + "'and marid='" + TextMarid.Text + "'";
                dtaddfieldnew = dvaddfield.ToTable();

                dvaddfield2.RowFilter = "mp_tracknum='" + LabelPTC.Text + "'and mp_marid='" + TextMarid.Text + "'";
                dtaddfieldnew2 = dvaddfield2.ToTable();

                if (dtaddfieldnew.Rows.Count > 0)
                {
                    string tooltips = "长*宽：" + dtaddfieldnew.Rows[0]["length"].ToString() + "*" + dtaddfieldnew.Rows[0]["width"].ToString() + "(" + dtaddfieldnew.Rows[0]["marunit"].ToString() + ")/材料类别：" + dtaddfieldnew.Rows[0]["pur_mashape"].ToString();

                    if (dtaddfieldnew2.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtaddfieldnew2.Rows[0]["mp_fixedsize"].ToString()))
                        {
                            tooltips = tooltips + "/是否定尺:" + dtaddfieldnew2.Rows[0]["mp_fixedsize"].ToString();
                        }
                    }
                    Label LabelMarName = e.Item.FindControl("LabelMarName") as System.Web.UI.WebControls.Label;
                    TextBox TextBoxDrawingNO = e.Item.FindControl("TextBoxDrawingNO") as System.Web.UI.WebControls.TextBox;
                    LabelPTC.ToolTip = tooltips;
                    LabelMarName.ToolTip = tooltips;
                    TextMarid.ToolTip = tooltips;
                    TextBoxDrawingNO.ToolTip = tooltips;

                    TextBox length = e.Item.FindControl("TextBOlength") as System.Web.UI.WebControls.TextBox;
                    TextBox width = e.Item.FindControl("TextBOXwidth") as System.Web.UI.WebControls.TextBox;
                    TextBox unit = e.Item.FindControl("TextBOXunit") as System.Web.UI.WebControls.TextBox;
                    TextBox shape = e.Item.FindControl("TextBOXshape") as System.Web.UI.WebControls.TextBox;
                    length.Text = dtaddfieldnew.Rows[0]["length"].ToString();
                    width.Text = dtaddfieldnew.Rows[0]["width"].ToString();
                    unit.Text = dtaddfieldnew.Rows[0]["marunit"].ToString();
                    shape.Text = dtaddfieldnew.Rows[0]["pur_mashape"].ToString();

                    HtmlTableCell tdlength = (HtmlTableCell)e.Item.FindControl("tdlength");
                    HtmlTableCell tdwidth = (HtmlTableCell)e.Item.FindControl("tdwidth");
                    HtmlTableCell tdunit = (HtmlTableCell)e.Item.FindControl("tdunit");
                    HtmlTableCell tdshape = (HtmlTableCell)e.Item.FindControl("tdshape");
                    HtmlTableCell tdisdc = (HtmlTableCell)e.Item.FindControl("tdisdc");

                    tdlength.Visible = true;
                    tdwidth.Visible = true;
                    tdunit.Visible = true;
                    tdshape.Visible = true;
                    tdisdc.Visible = true;
                    if (dtaddfieldnew2.Rows.Count > 0)
                    {
                        TextBox isdc = e.Item.FindControl("TextBOXdc") as System.Web.UI.WebControls.TextBox;
                        isdc.Text = dtaddfieldnew2.Rows[0]["mp_fixedsize"].ToString();
                    }
                }

                if (flag == "NEW")
                {
                    string sqlsameptc = "select * from TBQM_APLYFORITEM where PTC='" + LabelPTC.Text + "' ";
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
