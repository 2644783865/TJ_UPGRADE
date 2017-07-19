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
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_Claim_Add : System.Web.UI.Page
    {
        string action = "";
        string spdh = "";//索赔单号
        string splb = "";//索赔类别
        string Zhujian_ID = "";//主键
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["Action"];
            spdh=Request.QueryString["Spid"];
            splb=Request.QueryString["Splb"];
            Zhujian_ID = Request.QueryString["ID"];
            this.InitUploadControls();
            if (!IsPostBack)
            {
                this.InitAllHide();
                this.InitialPage();                
            }
        }
        /// <summary>
        /// 初始化附件上传控件
        /// </summary>
        private void InitUploadControls()
        {
            switch (dplSPLB.SelectedValue.ToString())
            {
                case "1": this.InitUploadControls_Child(at_YZ, txtZHTH, 0);
                    break;
                case "2": InitUploadControls_Child(AT_ZJYZ, lblZHTBHZJYZ, 1);
                    break;
                case "3": InitUploadControls_Child(AT_ZJFBS, lblZJFBHTH,2);
                    break;
                case "4":
                    this.InitUploadControls_Child(AT_FBS,txtFBHTH, 3);
                    break;
                default: break;
            }
        }
        private void InitUploadControls_Child(ZCZJ_DPF.Controls.UploadAttachments at_control, Label txt, int sp_type)
        {
            at_control.at_htbh = txt.Text;
            if (at_control.at_htbh == "")
            {
                at_control.Visible = false;//如何合同编号不存在则无法显示Upload控件
            }
            else
            {
                at_control.Visible = true;
                at_control.at_sp = sp_type;//0:业主；1、2：重机索赔；3：分包商
                at_control.at_type = 1;
                at_control.InitData();//重新绑定Upload数据
            }

        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitialPage()
        {
            this.KKControl(action);
            if (action == "Add")//添加
            #region
            {
                at_YZ.at_htbh = "";
                at_YZ.at_sp = 2;
                at_YZ.at_type = 2;
                lblSPLB.Text = "添加索赔信息";

                //业主
                this.BindPrjName(dplXMMC_YZ);
                this.BindSP_Reason(cblWTMS);
                //重机——业主
                this.BindPrjName(dplXMMCZJYZ);
                //重机——分包商
                this.BindPrjName(dplXMMCZJFBS);
                //分包商
                this.BindPrjName(dplXMMCFBS);
                this.BindSP_Reason(cblYYMSFBS);
            }
            #endregion
            else if (action == "Edit")
            {
                lblSPLB.Text = "修改索赔信息";
                this.BindDataUsingSpdh_Splb(spdh, splb, action);
            }
            else//View
            {
                lblSPLB.Text = "查看索赔信息";
                this.BindDataUsingSpdh_Splb(spdh, splb, action);
            }
        }
        private void BindDataUsingSpdh_Splb(string id,string lb,string op)
        {
            //页面显示、隐藏及可用性控制-及数据读取操作-附件上传控件
            #region
            switch (lb)
            {
                case "0"://YZ
                    palYZ.Visible = true;
                    palYZ.Enabled = (op == "Edit"?true:false);
                    dplSPLB.SelectedIndex = 1;
                    dplSPLB.Enabled = false;
                    this.BindYZ(id,lb);
                    ContractClass.UploadControlSet(at_YZ, op);
                    break;
                case "1"://ZJYZ
                    palZJYZ.Visible = true;
                    palZJYZ.Enabled = (op == "Edit" ? true : false);
                    dplSPLB.SelectedIndex = 2;
                    dplSPLB.Enabled = false;
                    this.BindZJYZ(id,lb);
                    ContractClass.UploadControlSet(AT_ZJYZ, op);
                    break;
                case "2":
                    palZJFBS.Visible = true;
                    palZJFBS.Enabled = (op == "Edit" ? true : false);
                    dplSPLB.SelectedIndex = 3;
                    dplSPLB.Enabled = false;
                    this.BindZJFBS(id,lb);
                    ContractClass.UploadControlSet(AT_ZJFBS, op);
                    break;
                case "3":
                    palFBS.Visible = true;
                    dplSPLB.SelectedIndex = 4;
                    palFBS.Enabled = (op == "Edit" ? true : false);
                    dplSPLB.Enabled = false;
                    this.BindFBS(id,lb);
                    ContractClass.UploadControlSet(AT_FBS, op);
                    break;
                default:
                    dplSPLB.SelectedIndex = 0;
                    dplSPLB.Enabled = false;
                    break;
            }
            this.InitUploadControls();
            #endregion
        }
        //数据读取
        #region
        private void BindYZ(string id,string op)
        {
            this.BindSP_Reason(cblWTMS);
            string sqltext = "select * from TBPM_MAINCLAIM where SPM_ID='" + id + "' and SPM_SPLB="+op+" and ID="+Zhujian_ID+"";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.HasRows)
            {
                dr.Read();
                //	1	索赔编号	*****
                txtSPBH.Text = dr["SPM_ID"].ToString();
                //	2	项目编号*****项目名称
                //	3	主合同编号******	根据主合同号查询合同名称
                txtZHTH.Text = dr["spm_htbh"].ToString();
                string a = "select PCON_NAME,PCON_BCODE,PCON_PJNAME,PCON_PJID from TBPM_CONPCHSINFO where PCON_BCODE='" + txtZHTH.Text.Trim() + "'";
                SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(a);
                dr1.Read();
                dplZHTMC.Items.Add(new ListItem(dr1["PCON_NAME"].ToString(), dr1["PCON_BCODE"].ToString()));
                dplXMMC_YZ.Items.Add(new ListItem(dr1["PCON_PJNAME"].ToString(), dr1["PCON_PJID"].ToString()));
                dr1.Close();
                //	4	接受部门	********
                dplSLBM.SelectedIndex = Convert.ToInt16(dr["SPM_JSBM"].ToString());
                //	5	索赔问题描述	***
                txtSPWTMS.Text = dr["spm_spwtmx"].ToString();
                //	6	索赔金额	****
                txtSPJE.Text = dr["spm_spje"].ToString();
                txtYZSPJE1.Text = dr["spm_spje"].ToString();
                //	7	最终索赔金额	****
                txtZZSPJE.Text=dr["spm_zzspje"].ToString();
                //	8	索赔登记日期	
                txtSPDJRQ.Text=dr["spm_spdjrq"].ToString();
                //	9	是否扣款	
                rblSSKK.SelectedIndex = Convert.ToInt16(dr["spm_sfkk"].ToString());
                if (rblSSKK.SelectedIndex == 0)
                {
                    rblSSKK.Enabled = false;
                    btnKK.Visible = false;
                    txtZZSPJE.Enabled = false;
                    txtKKRQ.Enabled = false;
                }
                //	10	扣款日期	
                txtKKRQ.Text=dr["spm_kkrq"].ToString();
                //	11	扣款备注	
                txtKKBZ.Text=dr["spm_kkbz"].ToString();
                //	12	技术负责人
	            txtJSFZR.Text=dr["spm_jsfzr"].ToString();
                //	13	质量负责人	
                txtZZFZR.Text=dr["spm_zlfzr"].ToString();
                //	14	制作方???????????????????????
	            foreach(ListItem li in dplZZF.Items)
                {
                    if (li.Value.ToString() == dr["spm_zzf"].ToString())
                    {
                        li.Selected = true; break;
                    }
                }
                //	15	是否处理	
                rblSFCL.SelectedIndex = Convert.ToInt16(dr["spm_sscl"].ToString());
                //	16	是否回复	
                rblSFHF.SelectedIndex = Convert.ToInt16(dr["spm_sshf"].ToString());
                //	17	回复意见	
                txtHFYJ.Text=dr["spm_hfyj"].ToString();
                //	18	反馈人
	            txtFKR.Text=dr["spm_fkr"].ToString();
                //	19	反馈日期	
                txtFKRQ.Text=dr["spm_fkrq"].ToString();
                //	20	内部处理意见	
                txtNBCLYJ.Text=dr["spm_nbclyj"].ToString();
                //	21	问题描述	
                string spm_wtms = dr["spm_wtms"].ToString();
                string[] aa = spm_wtms.Split('-');
                foreach (string chr in aa)
                {
                        foreach (ListItem li in cblWTMS.Items)
                        {
                            if (li.Value.ToString() == chr.ToString())
                            {
                                li.Selected = true;
                                break;
                            }
                        }
                }
                //	22	备注	
                txtBZ.Text = dr["spm_bz"].ToString();
                //	23	负责部门	
                string spm_fzbm = dr["spm_fzbm"].ToString();
                foreach (char chr in spm_fzbm)
                {
                    if (char.IsNumber(chr))
                    {
                        foreach (ListItem li in cblFZBM.Items)
                        {
                            if (li.Value.ToString() == chr.ToString())
                            {
                                li.Selected = true;
                                break;
                            }
                        }
                    }
                }
                //	24	索赔类别	
                dr.Close();
            }
        }
        private void BindZJYZ(string id,string op)
        {
            string sqltext = "select * from TBPM_MAINCLAIM where SPM_ID='" + id + "' and SPM_SPLB=" + op + " AND ID="+Zhujian_ID+"";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.HasRows)
            {
                dr.Read();
                //	1	索赔编号	********
                txtSPDHZJYZ.Text = dr["spm_id"].ToString();
                //	3	主合同编号	
                lblZHTBHZJYZ.Text = dr["spm_htbh"].ToString();
                //	2	项目编号
                string a = "select PCON_NAME,PCON_BCODE,PCON_PJNAME,PCON_PJID from TBPM_CONPCHSINFO where PCON_BCODE='" + lblZHTBHZJYZ.Text + "'";
                SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(a);
                dr1.Read();
                dplZHTMCZJYZ.Items.Add(new ListItem(dr1["PCON_NAME"].ToString(), dr1["PCON_BCODE"].ToString()));
                dplXMMCZJYZ.Items.Add(new ListItem(dr1["PCON_PJNAME"].ToString(), dr1["PCON_PJID"].ToString()));
                dr1.Close();
                //	4	接受部门	
                dplJSBMZJYZ.SelectedIndex = Convert.ToInt16(dr["spm_jsbm"].ToString());
                //	5	索赔问题描述	
                txtSPWTMSZJYZ.Text=dr["spm_spwtmx"].ToString();
                //	6	索赔金额	
                txtSPJEZJYZ.Text = dr["spm_spje"].ToString();
                txtSPJEZJYZ1.Text = dr["spm_spje"].ToString();
                //	7	最终索赔金额	
                txtZZSPJEZJYZ.Text=dr["spm_zzspje"].ToString();
                //	8	索赔登记日期	
                txtSPDJRQZJYZ.Text=dr["spm_spdjrq"].ToString();
                //	9	是否扣款	
                rblSFKKZJYZ.SelectedIndex = Convert.ToInt16(dr["spm_sfkk"].ToString());
                if (rblSFKKZJYZ.SelectedIndex == 0)
                {
                    rblSFKKZJYZ.Enabled = false;
                    txtZZSPJEZJYZ.Enabled = false;
                    btnKKZJYZ.Visible = false;
                    txtKKRQZJYZ.Enabled = false;
                }
                //	10	扣款日期	
                txtKKRQZJYZ.Text = dr["spm_kkrq"].ToString();
                //	11	扣款备注	
                txtKKBZZJYZ.Text=dr["spm_kkbz"].ToString();
                //	12	技术负责人	
                //	13	质量负责人	
                //	14	制作方	
                //	15	是否处理	
                rblCLJGFBS.SelectedIndex = Convert.ToInt16(dr["spm_sscl"].ToString());
                //	16	是否回复	
                rblSFHFZJYZ.SelectedIndex=Convert.ToInt16(dr["spm_sshf"].ToString());
                //	17	回复意见	
                txtHFYJZJYZ.Text=dr["spm_hfyj"].ToString();
                //	18	反馈人	
                txtFKRZJYZ.Text = dr["spm_fkr"].ToString();
                //	19	反馈日期	
                txtFKRQZJYZ.Text=dr["spm_fkrq"].ToString();
                //	20	内部处理意见	
                //	21	问题描述	
                //	22	备注	
                //	23	负责部门	
                //	24	索赔类别	
                dr.Close();
            }
        }
        private void BindZJFBS(string id,string op)
        {
            string sqltext = "select * from TBPM_SUBCLAIM where SPS_ID='" + id + "' and SPS_SPLB=" + op + " and ID="+Zhujian_ID+"";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.HasRows)
            {
                dr.Read();
                //	1	索赔编号	
                txtSPDHZJFBS.Text=dr["sps_id"].ToString();
                //根据索赔单号查询主合同名称
                string a = "select b.PCON_NAME,b.PCON_BCODE from TBPM_MAINCLAIM a,TBPM_CONPCHSINFO b where a.SPM_HTBH=b.PCON_BCODE and a.SPM_ID='" + txtSPDHZJFBS.Text + "'";
                SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(a);
                if (dr1.HasRows)
                {
                    dr1.Read();
                    dplZHTMCZJFBS.Items.Add(new ListItem(dr1["PCON_NAME"].ToString(), dr1["PCON_BCODE"].ToString()));
                    dr1.Close();
                }
                //	2	分包合同号
	            lblZJFBHTH.Text=dr["sps_htbh"].ToString();
                //根据分包合同查询分包合同名称、工程、项目
                string b = "select b.PCON_NAME,b.PCON_BCODE,b.PCON_ENGNAME,b.PCON_ENGID,b.PCON_PJNAME,b.PCON_PJID from TBPM_SUBCLAIM a,TBPM_CONPCHSINFO b where a.SPS_HTBH=b.PCON_BCODE and a.SPS_ID='" + txtSPDHZJFBS.Text + "' and a.SPS_SPLB=" + op + "";
                SqlDataReader dr2 = DBCallCommon.GetDRUsingSqlText(b);
                if (dr2.HasRows)
                {
                    dr2.Read();
                    dplZJFBHTMC.Items.Add(new ListItem(dr2["PCON_NAME"].ToString(),dr2["PCON_BCODE"].ToString()));
                    dplGCMCZJFBS.Items.Add(new ListItem(dr2["PCON_ENGNAME"].ToString(), dr2["PCON_ENGID"].ToString()));
                    dplXMMCZJFBS.Items.Add(new ListItem(dr2["PCON_PJNAME"].ToString(), dr2["PCON_PJID"].ToString()));
                    dr2.Close();
                }
                //	3	责任班组/责任分包商	
                //	4	接受部门	
                dplJSBMZJFBS.SelectedIndex = Convert.ToInt16(dr["sps_jsbm"].ToString());
                //	5	索赔问题描述	
                txtSPWTMSZJFBS.Text = dr["sps_spwtmx"].ToString();
                //	6	索赔金额	
                txtSPJEZJFBS.Text = dr["sps_spje"].ToString();
                txtSPJEZJFBS1.Text = dr["sps_spje"].ToString();
                //	7	最终索赔金额	
                txtZZSPJEZJFBS.Text = dr["sps_zzspje"].ToString();
                //	8	索赔登记日期	
                txtSPDJRQZJFBS.Text = dr["sps_spdjrq"].ToString();
                //	9	是否扣款	
                rblSFKKZJFBS.SelectedIndex = Convert.ToInt16(dr["sps_sfkk"].ToString());
                if (rblSFKKZJFBS.SelectedIndex == 0)
                {
                    rblSFKKZJFBS.Enabled = false;
                    txtZZSPJE.Enabled = false;
                    btnKKZJFBS.Visible = false;
                    txtKKRQZJFBS.Enabled = false;
                }
                //	10	扣款日期	
                txtKKRQZJFBS.Text = dr["sps_kkrq"].ToString();
                //	11	扣款备注	
                txtKKBZZJFBS.Text = dr["sps_kkbz"].ToString();
                //	12	技术负责人	
                //	13	质量负责人	
                //	14	制作方	
                //	15	是否处理
                rblSFHFZJFBS.SelectedIndex = Convert.ToInt16(dr["sps_sscl"].ToString());
                //	16	是否回复
                rblSFHFZJFBS.SelectedIndex = Convert.ToInt16(dr["sps_sfhf"].ToString());
                //	17	回复意见	
                txtHFYJZJFBS.Text = dr["sps_hfyj"].ToString();
                //	18	反馈人
                txtDJRZJFBS.Text = dr["sps_fkr"].ToString();
                //	19	反馈日期	
                txtDJRQZJFBS.Text = dr["sps_fkrq"].ToString();
                //	20	内部处理意见	
                //	21	问题描述	
                //	22	备注	
                //	23	负责部门	
                //	24	索赔类别	
                dr.Close();
            }

        }
        private void BindFBS(string id,string op)
        {
            this.BindSP_Reason(cblYYMSFBS);
            string sqltext = "select * from TBPM_SUBCLAIM where SPS_ID='" + id + "' and SPS_SPLB=" + op + " AND ID="+Zhujian_ID+"";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.HasRows)
            {
                dr.Read();
                //	1	索赔编号	
                txtSPDHFBS.Text=dr["sps_id"].ToString();
                //根据索赔单号查询主合同名称
                string a = "select b.PCON_NAME,b.PCON_BCODE from TBPM_MAINCLAIM a,TBPM_CONPCHSINFO b where a.SPM_HTBH=b.PCON_BCODE and a.SPM_ID='" + txtSPDHFBS.Text + "'";
                SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(a);
                if (dr1.HasRows)
                {
                    dr1.Read();
                    dplZHTMCFBS.Items.Add(new ListItem(dr1["PCON_NAME"].ToString(), dr1["PCON_BCODE"].ToString()));
                    dr1.Close();
                }
                //	2	分包合同号	
                txtFBHTH.Text = dr["sps_htbh"].ToString();
                //根据分包合同查询分包合同名称、工程、项目
                string b = "select b.PCON_NAME,b.PCON_BCODE,b.PCON_ENGNAME,b.PCON_ENGID,b.PCON_PJNAME,b.PCON_PJID from TBPM_SUBCLAIM a,TBPM_CONPCHSINFO b where a.SPS_HTBH=b.PCON_BCODE and a.SPS_ID='" + txtSPDHFBS.Text + "' and a.SPS_SPLB=" + op + "";
                SqlDataReader dr2 = DBCallCommon.GetDRUsingSqlText(b);
                if (dr2.HasRows)
                {
                    dr2.Read();
                    dplFBHTMC.Items.Add(new ListItem(dr2["PCON_NAME"].ToString(), dr2["PCON_BCODE"].ToString()));
                    dplGCMCFBS.Items.Add(new ListItem(dr2["PCON_ENGNAME"].ToString(), dr2["PCON_ENGID"].ToString()));
                    dplXMMCFBS.Items.Add(new ListItem(dr2["PCON_PJNAME"].ToString(), dr2["PCON_PJID"].ToString()));
                    dr2.Close();
                }
                //	3	责任班组/责任分包商
                foreach (ListItem li in dplZZBZFBS.Items)
                {
                    if (li.Value.ToString() == dr["sps_zrbz"].ToString())
                    {
                        li.Selected = true; break;
                    }
                }
                //	4	接受部门	
                dplJSBMFBS.SelectedIndex = Convert.ToInt16(dr["sps_jsbm"].ToString());
                //	5	索赔问题描述	
                txtSPWTMSFBS.Text = dr["sps_spwtmx"].ToString();
                //	6	索赔金额	
                txtFBSSPJEFBS.Text = dr["sps_spje"].ToString();
                txtFBSSPJEFBS1.Text = dr["sps_spje"].ToString();
                //	7	最终索赔金额	
                txtZZSPJEFBS.Text = dr["sps_zzspje"].ToString();
                //	8	索赔登记日期	
                txtSPDJRQFBS.Text = dr["sps_spdjrq"].ToString();
                //	9	是否扣款	
                rblSFKKFBS.SelectedIndex = Convert.ToInt16(dr["sps_sfkk"].ToString());
                if (rblSFKKFBS.SelectedIndex == 0)
                {
                    rblSFKKFBS.Enabled = false;
                    txtZZSPJEFBS.Enabled = false;
                    btnKKFBS.Visible = false;
                    txtKKRQFBS.Enabled = false;
                }
                //	10	扣款日期	
                txtKKRQFBS.Text = dr["sps_kkrq"].ToString();
                //	11	扣款备注	
                txtKKBZFBS.Text = dr["sps_kkbz"].ToString();
                //	12	技术负责人	
                txtJSFZRFBS.Text = dr["sps_jsfzr"].ToString();
                //	13	质量负责人	
                txtZLFZRFBS.Text = dr["sps_zzfzr"].ToString();
                //	14	制作方
                foreach (ListItem li in dplZZFFBS.Items)
                {
                    if (li.Value.ToString() == dr["sps_zzf"].ToString())
                    {
                        li.Selected = true;
                        break;
                    }
                }
                //	15	是否处理
                rblCLJGFBS.SelectedIndex = Convert.ToInt16(dr["sps_sscl"].ToString());
                //	16	是否回复
                rblSFHFFBS.SelectedIndex = Convert.ToInt16(dr["sps_sfhf"].ToString());
                //	17	回复意见	
                txtHFYJFBS.Text = dr["sps_hfyj"].ToString();
                //	18	反馈人	
                txtFKRFBS.Text = dr["sps_fkr"].ToString();
                //	19	反馈日期	
                txtFKRQFBS.Text = dr["sps_fkrq"].ToString();
                //	20	内部处理意见	
                txtNBCLYJFBS.Text = dr["sps_nbclyj"].ToString();
                //	21	问题描述	
                string[] aa=dr["sps_wtms"].ToString().Split('-');
                foreach (string t in aa)
                {
                    foreach (ListItem li in cblYYMSFBS.Items)
                    {
                        if (t == li.Value.ToString())
                        {
                            li.Selected = true;
                            break;
                        }
                    }
                }
                //	22	备注	
                txtBZFBS.Text = dr["sps_bz"].ToString();
                //	23	负责部门	
                string bb=dr["sps_fzbm"].ToString();
                foreach(char chr in bb)
                {
                    if (char.IsNumber(chr))
                    {
                        foreach (ListItem li in cblFZBMFBS.Items)
                        {
                            if (li.Value.ToString() == chr.ToString())
                            {
                                li.Selected = true;
                                break;
                            }
                        }
                    }
                }
                //	24	索赔类别	
                dr.Close();
            }
        }
        #endregion

        //*************************************业主索赔****************************************
        #region
        //添加业主索赔信息
        protected void btnConfirmYZ_Click(object sender, EventArgs e)
        {

            //	1	索赔编号	
            string spm_id=txtSPBH.Text.Trim();
            //	2	项目编号
            string spm_xmbh = dplXMMC_YZ.SelectedValue.ToString();
            //	3	主合同编号	
            string	spm_htbh=txtZHTH.Text;
            //	4	接受部门	
            int	spm_jsbm=Convert.ToInt16(dplSLBM.SelectedValue.ToString());
            //	5	索赔问题描述	
            string	spm_spwtmx=txtSPWTMS.Text.Trim();
            //	6	索赔金额	
            decimal spm_spje=(txtSPJE.Text.Trim()==""?0:Convert.ToDecimal(txtSPJE.Text.Trim()));
            //	7	最终索赔金额	
            decimal spm_zzspje=(txtZZSPJE.Text.Trim()==""?0:Convert.ToDecimal(txtZZSPJE.Text.Trim()));
            //	8	索赔登记日期	
            string	spm_spdjrq=txtSPDJRQ.Text.Trim();
            //	9	是否扣款	
            int spm_sfkk=Convert.ToInt16(rblSSKK.SelectedValue.ToString());
            //	10	扣款日期	
            string	spm_kkrq=txtKKRQ.Text.Trim();
            //	11	扣款备注	
            string	spm_kkbz=txtKKBZ.Text.Trim();
            //	12	技术负责人	
            string	spm_jsfzr=txtJSFZR.Text.Trim();
            //	13	质量负责人	
            string	spm_zlfzr=txtZZFZR.Text.Trim();
            //	14	制作方	
            string	spm_zzf=dplZZF.SelectedItem.Text;
            //	15	是否处理	
            int spm_sscl=Convert.ToInt16(rblSFCL.SelectedValue.ToString());
            //	16	是否回复	
            int spm_sshf=Convert.ToInt16(rblSFHF.SelectedValue.ToString());
            //	17	回复意见	
            string	spm_hfyj=txtHFYJ.Text.Trim();
            //	18	反馈人	
            string	spm_fkr=txtFKR.Text.Trim();
            //	19	反馈日期	
            string	spm_fkrq=txtFKRQ.Text.Trim();
            //	20	内部处理意见	
            string	spm_nbclyj=txtNBCLYJ.Text.Trim();
            //	21	问题描述	
            string	spm_wtms="";
             foreach(ListItem li in cblWTMS.Items)
             {
                 if(li.Selected)
                 {
                     spm_wtms+="-"+li.Value.ToString();
                 }
             }
             if (spm_wtms != "")
             {
                 spm_wtms = spm_wtms.Substring(1, spm_wtms.Length - 1);
             }
            //	22	备注	
            string	spm_bz=txtBZ.Text.Trim();
            //	23	负责部门	
            string	spm_fzbm="";
            foreach(ListItem li in cblFZBM.Items)
            {
                if(li.Selected)
                {
                    spm_fzbm+=li.Value.ToString();
                }
            }
            //	24	索赔类别	
            int spm_splb = 0;

            if (action == "Add")
            {
                if (!CheckMainExist(spm_htbh, 0))
                {
                    //如果某一合同索赔，合同信息表中异常标识置1
                    List<string> sql = new List<string>();
                    string sqltext1 = "insert into TBPM_MAINCLAIM(SPM_ID,SPM_XMBH,SPM_HTBH,SPM_JSBM,SPM_SPWTMX,SPM_SPJE,SPM_ZZSPJE,SPM_SPDJRQ,SPM_SFKK,SPM_KKRQ,SPM_KKBZ,SPM_JSFZR,SPM_ZLFZR,SPM_ZZF,SPM_SSCL,SPM_SSHF,SPM_HFYJ,SPM_FKR,SPM_FKRQ,SPM_NBCLYJ,SPM_WTMS,SPM_BZ,SPM_FZBM,SPM_SPLB)" +
                        "Values('" + spm_id + "','" + spm_xmbh + "','" + spm_htbh + "'," + spm_jsbm + ",'" + spm_spwtmx + "','" + spm_spje + "','" + spm_zzspje + "','" + spm_spdjrq + "'," + spm_sfkk + ",'" + spm_kkrq + "','" + spm_kkbz + "','" + spm_jsfzr + "','" + spm_zlfzr + "','" + spm_zzf + "'," + spm_sscl + "," + spm_sshf + ",'" + spm_hfyj + "','" + spm_fkr + "','" + spm_fkrq + "','" + spm_nbclyj + "','" + spm_wtms + "',	'" + spm_bz + "','" + spm_fzbm + "'," + spm_splb + ")";
                    string sqltext2 = "update TBPM_CONPCHSINFO set PCON_ERROR=1 where PCON_BCODE='" + spm_htbh + "'";
                    sql.Add(sqltext1);
                    sql.Add(sqltext2);
                    DBCallCommon.ExecuteTrans(sql);
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert(\"添加成功！\")", true);
                    //Response.Write("<script type=\"text/javascript\" language=\"javascript\">alert(\"添加成功！\")</script>");
                }
                else
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert(\"已添加该合同下业主索赔信息记录，无法添加!\\r\\r请在原记录上修改！\")", true);
                    //Response.Write("<script type=\"text/javascript\" language=\"javascript\">alert(\"已添加该合同下业主索赔信息记录，无法添加!\\r\\r请在原记录上修改！\")</script>");
                }
            }
            else if (action == "Edit")
            {
                string sqltext = "update TBPM_MAINCLAIM set SPM_ID='" + spm_id + "',SPM_XMBH='" + spm_xmbh + "',SPM_HTBH='" + spm_htbh + "',SPM_JSBM=" + spm_jsbm + ",SPM_SPWTMX='" + spm_spwtmx + "',SPM_SPJE=" + spm_spje + ",SPM_ZZSPJE=" + spm_zzspje + ",SPM_SPDJRQ='" + spm_spdjrq + "',SPM_SFKK=" + spm_sfkk + ",SPM_KKRQ='" + spm_kkrq + "',SPM_KKBZ='" + spm_kkbz + "',SPM_JSFZR='" + spm_jsfzr + "',SPM_ZLFZR='" + spm_zlfzr + "',SPM_ZZF='" + spm_zzf + "',SPM_SSCL=" + spm_sscl + ",SPM_SSHF=" + spm_sshf + ",SPM_HFYJ='" + spm_hfyj + "',SPM_FKR='" + spm_fkr + "',SPM_FKRQ='" + spm_fkrq + "',SPM_NBCLYJ='" + spm_nbclyj + "',SPM_WTMS='" + spm_wtms + "',SPM_BZ='" + spm_bz + "',SPM_FZBM='" + spm_fzbm + "'"+
                    " where ID="+Zhujian_ID+"";
                DBCallCommon.ExeSqlText(sqltext);
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert(\"修改成功！\")", true);
                //Response.Write("<script type=\"text/javascript\" language=\"javascript\">alert(\"修改成功！\")</script>");
            }
        }

        /// <summary>
        /// 创建索赔单号,如SP201107-1
        /// </summary>
        /// <returns></returns>
        private string CreateSPDH()
        {
            string month = "";
            if (DateTime.Now.Month < 9)
            {
                month = "0" + DateTime.Now.Month.ToString();
            }
            else
            {
                month = DateTime.Now.Month.ToString();
            }
            string spdh = "SP" + DateTime.Now.Year.ToString() + month+"-";
            string sqltext = "select TOP 1 SPM_ID from TBPM_MAINCLAIM  order by ID desc";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.HasRows)
            {
                dr.Read();
                string[] tt = dr["SPM_ID"].ToString().Split('-');
                string a = (Convert.ToInt16(tt[1].ToString()) + 1).ToString();
                spdh += a;
            }
            else
            {
                spdh += "1";
            }
            dr.Close();
            return spdh;
        }
        
        /// <summary>
        /// 绑定项目名称
        /// </summary>
        private void BindPrjName(DropDownList cmbpj)
        {
            cmbpj.Items.Clear();
            string sqlText = "select PJ_ID+'/'+PJ_NAME as PJ_NAME,PJ_ID from TBPM_PJINFO";//随着项目的增多，下拉框数据多，考虑将项目是否完工加入查询条件
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            cmbpj.DataSource = dt;
            cmbpj.DataTextField = "PJ_NAME";
            cmbpj.DataValueField = "PJ_ID";
            cmbpj.DataBind();
            cmbpj.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            cmbpj.SelectedIndex = 0;
        }
        /// <summary>
        /// 绑定索赔原因
        /// </summary>
        /// <param name="cbl"></param>
        private void BindSP_Reason(CheckBoxList cbl)
        {
            cbl.Items.Clear();
            string sqltext = "select SPR_ID,SPR_DESCRIBLE from TBPM_REASONCONTROL";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            cbl.DataSource = dt;
            cbl.DataTextField = "SPR_DESCRIBLE";
            cbl.DataValueField = "SPR_ID";
            cbl.DataBind();
        }

        //显示相应的索赔
        protected void dplSPLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedText = dplSPLB.SelectedValue.ToString();
            switch (selectedText)
            {
                case "1": this.InitYZ(); break;
                case "2": this.InitZJYZ(); break;
                case "3": this.InitZJFBS(); break;
                case "4": this.InitFBS(); break;
                default: this.InitAllHide(); break;
            }
            this.InitUploadControls();
        }

        //各索赔信息下对应的初始化操作
        /// <summary>
        /// 添加业主索赔信息初始化
        /// </summary>
        private void InitYZ()
        {
            palYZ.Visible = true;
            at_YZ.Visible = true; 
         
            palZJYZ.Visible = false;
            AT_ZJYZ.Visible = false;

            palZJFBS.Visible = false;
            AT_ZJFBS.Visible = false;

            palFBS.Visible = false;
            AT_FBS.Visible = false;

            txtSPBH.Text = this.CreateSPDH();
        }
        /// <summary>
        /// 添加重机对业主索赔信息初始化
        /// </summary>
        private void InitZJYZ()
        {
            palYZ.Visible = false;
            at_YZ.Visible = false;

            palZJYZ.Visible = true;
            AT_ZJYZ.Visible = true;

            palZJFBS.Visible = false;
            AT_ZJFBS.Visible = false;

            palFBS.Visible = false;
            AT_FBS.Visible = false;

            txtSPDHZJYZ.Text = this.CreateSPDH();
        }
        /// <summary>
        /// 添加重机对分包商索赔信息初始化
        /// </summary>
        private void InitZJFBS()
        {
            palYZ.Visible = false;
            at_YZ.Visible = false;

            palZJYZ.Visible = false;
            AT_ZJYZ.Visible = false;

            palZJFBS.Visible = true;
            AT_ZJFBS.Visible = true;

            palFBS.Visible = false;
            AT_FBS.Visible = false;
        }
        /// <summary>
        /// 添加分包商索赔信息初始化
        /// </summary>
        private void InitFBS()
        {
            palYZ.Visible = false;
            at_YZ.Visible = false;

            palZJYZ.Visible = false;
            AT_ZJYZ.Visible = false;

            palZJFBS.Visible = false;
            AT_ZJFBS.Visible = false;

            palFBS.Visible = true;
            AT_FBS.Visible = true;
        }
        private void InitAllHide()
        {
            palYZ.Visible = false;
            at_YZ.Visible = false;

            palZJYZ.Visible = false;
            AT_ZJYZ.Visible = false;

            palZJFBS.Visible = false;
            AT_ZJFBS.Visible = false;

            palFBS.Visible = false;
            AT_FBS.Visible = false;
        }

        protected void dplXMMC_YZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtZHTH.Text = "";//主合同编号
            if (dplXMMC_YZ.SelectedIndex != 0)
            {
                string pjID = dplXMMC_YZ.SelectedValue.ToString();
                string sqltext = "select PCON_BCODE,PCON_NAME from TBPM_CONPCHSINFO where PCON_PJID='" + pjID + "' and PCON_FORM=0";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                dplZHTMC.DataSource = dt;
                dplZHTMC.DataTextField = "PCON_NAME";
                dplZHTMC.DataValueField = "PCON_BCODE";
                dplZHTMC.DataBind();
                dplZHTMC.Items.Insert(0, new ListItem("-请选择-", ""));
                dplZHTMC.SelectedIndex = 0;
            }
            else
            {
                dplZHTMC.Items.Clear();
            }
            this.InitUploadControls();
        }

        protected void dplZHTMC_SelectedIndexChanged(object sender, EventArgs e)
        {
           txtZHTH.Text = dplZHTMC.SelectedValue.ToString();
           if (CheckMainExist(txtZHTH.Text.Trim(), 0))
           {
               this.ClientScript.RegisterStartupScript(GetType(), "js", "alert(\"已添加该合同下业主索赔信息记录，无法添加!\\r\\r请在原记录上修改！\")", true);
               btnConfirmYZ.Visible = false;
           }
           else
           {
               btnConfirmYZ.Visible = true;
               this.InitUploadControls();
           }
        }
        #endregion
        //*******************************************分包商*************************************
        #region
        protected void dplXMMCFBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dplXMMCFBS.SelectedIndex != 0)
            {
                //根据项目名称绑定工程名称，主合同名称，同时分包合同清空,索赔单号
                this.BindGC_Name(dplXMMCFBS, dplGCMCFBS);
                this.BindMainName(dplXMMCFBS, dplZHTMCFBS);
                this.dplFBHTMC.Items.Clear();
                this.txtFBHTH.Text = "";
                this.txtSPDHFBS.Text = "";
                //因为合同号不存在，也不能上传附件
                AT_FBS.Visible = false;
            }
            else
            {
                //项目名称未选择
                txtSPDHFBS.Text = "";//索赔单号为空
                dplGCMCFBS.Items.Clear();//工程
                dplFBHTMC.Items.Clear();
                txtFBHTH.Text = "";
                dplZHTMCFBS.Items.Clear();
                txtSPDHFBS.Text = "";
            }
            this.InitUploadControls();
        }
        /// <summary>
        /// 根据项目名称绑定主合同号
        /// </summary>
        /// <param name="xmmc"></param>
        /// <param name="watingBind"></param>
        private void BindMainName(DropDownList xmmc, DropDownList watingBind)
        {
            if (xmmc.SelectedIndex != 0)
            {
                string pjID = xmmc.SelectedValue.ToString();
                string sqltext = "select PCON_BCODE,PCON_NAME from TBPM_CONPCHSINFO where PCON_PJID='" + pjID + "' and PCON_FORM=0";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                watingBind.DataSource = dt;
                watingBind.DataTextField = "PCON_NAME";
                watingBind.DataValueField = "PCON_BCODE";
                watingBind.DataBind();
                watingBind.Items.Insert(0, new ListItem("-请选择-", ""));
                watingBind.SelectedIndex = 0;
            }
            else
            {
                watingBind.Items.Clear();
            }
        }
        //根据主合同名称确定索赔合同号
        protected void dplZHTMCFBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dplZHTMCFBS.SelectedIndex != 0)
            {
                //主合同号
                string zhtbh = dplZHTMCFBS.SelectedValue.ToString();
                //查询该项目商务合同下是否建立索赔记录
                //如果建立索赔记录：直接读出，否则重新创建
                string sqltext = "select SPM_ID from TBPM_MAINCLAIM where SPM_HTBH='" + zhtbh + "' AND SPM_SPLB=1";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                if (dr.HasRows)
                {
                    dr.Read();
                    txtSPDHFBS.Text = dr["SPM_ID"].ToString();
                    dr.Close();
                }
                else
                {
                    txtSPDHFBS.Text = this.CreateSPDH();
                }
            }
            else
            {
                txtSPDHFBS.Text = "";
            }
        }
        //根据项目名称绑定工程名称
        private void BindGC_Name(DropDownList dplxmmc,DropDownList dplgcmc)
        {
            dplgcmc.Items.Clear();
            if (dplxmmc.SelectedIndex != 0)
            {
                string sqlText = "select TSA_STFORCODE+'/'+TSA_ENGNAME AS TSA_ENGNAME,TSA_ID from TBPM_TCTSASSGN ";
                sqlText += "where TSA_PJID='" + dplxmmc.SelectedValue.ToString() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                dplgcmc.DataSource = dt;
                dplgcmc.DataSource = dt;
                dplgcmc.DataTextField = "TSA_ENGNAME";
                dplgcmc.DataValueField = "TSA_ID";
                dplgcmc.DataBind();
                dplgcmc.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
                dplgcmc.SelectedIndex = 0;
            }

        }
        //根据项目工程名称确定分包合同号
        protected void dplGCMCFBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindHTBH(dplXMMCFBS, dplGCMCFBS, dplFBHTMC);
        }

       /// <summary>
       /// 根据项目工程名称绑定其他合同
       /// </summary>
       /// <param name="dplxmmc"></param>
       /// <param name="dplgcmc"></param>
       /// <param name="dplhtmc"></param>
        private void BindHTBH(DropDownList dplxmmc,DropDownList dplgcmc,DropDownList dplhtmc)
        {
            string sqltext = "select PCON_NAME,PCON_BCODE FROM TBPM_CONPCHSINFO WHERE PCON_PJID='" + dplxmmc.SelectedValue.ToString() + "' and PCON_ENGID='" + dplgcmc.SelectedValue.ToString() + "' and PCON_FORM!=0";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            dplhtmc.DataSource = dt;
            dplhtmc.DataTextField = "PCON_NAME";
            dplhtmc.DataValueField = "PCON_BCODE";
            dplhtmc.DataBind();
            dplhtmc.Items.Insert(0, new ListItem("-请选择-", ""));
            dplhtmc.SelectedIndex = 0;
        }

        //根据分包合同名称确定分包合同号
        protected void dplFBHTMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFBHTH.Text = dplFBHTMC.SelectedValue.ToString();
            this.InitUploadControls();
        }
        private void FBS_ExecSQL()
        {
            //	1	索赔编号	
            string	sps_id=txtSPDHFBS.Text;
            //	2	分包合同号	
            string	sps_htbh=txtFBHTH.Text;
            //	3	责任班组/责任分包商	
            string	sps_zrbz=dplZZBZFBS.SelectedValue.ToString();
            //	4	接受部门	
            int	sps_jsbm=Convert.ToInt16(dplJSBMFBS.SelectedValue.ToString());
            //	5	索赔问题描述	
            string	sps_spwtmx=txtSPWTMSFBS.Text.Trim();
            //	6	索赔金额	
            decimal sps_spje=(txtFBSSPJEFBS.Text.Trim()==""?0:Convert.ToDecimal(txtFBSSPJEFBS.Text.Trim()));
            //	7	最终索赔金额	
            decimal	sps_zzspje=(txtZZSPJEFBS.Text.Trim()==""?0:Convert.ToDecimal(txtZZSPJEFBS.Text.Trim()));
            //	8	索赔登记日期	
            string	sps_spdjrq=txtSPDJRQFBS.Text.Trim();
            //	9	是否扣款	
            int	sps_sfkk=Convert.ToInt16(rblSFKKFBS.SelectedValue.ToString());
            //	10	扣款日期	
            string	sps_kkrq=txtKKRQFBS.Text.Trim();
            //	11	扣款备注	
            string	sps_kkbz=txtKKBZFBS.Text.Trim();
            //	12	技术负责人	
            string	sps_jsfzr=txtJSFZRFBS.Text.Trim();
            //	13	质量负责人	
            string	sps_zzfzr=txtZLFZRFBS.Text.Trim();
            //	14	制作方	
            string	sps_zzf=dplZZFFBS.SelectedValue.ToString();
            //	15	是否处理
            int	sps_sscl=Convert.ToInt16(rblCLJGFBS.SelectedValue.ToString());
            //	16	是否回复
            int	sps_sfhf=Convert.ToInt16(rblSFHFFBS.SelectedValue.ToString());
            //	17	回复意见	
            string	sps_hfyj=txtHFYJFBS.Text.Trim();
            //	18	反馈人	
            string	sps_fkr=txtFKRFBS.Text.Trim();
            //	19	反馈日期	
            string	sps_fkrq=txtFKRQFBS.Text.Trim();
            //	20	内部处理意见	
            string	sps_nbclyj=txtNBCLYJFBS.Text.Trim();
            //	21	问题描述	
            string	sps_wtms="";
            foreach(ListItem li in cblYYMSFBS.Items)
            {
                if(li.Selected)
                {
                    sps_wtms+="-"+li.Value.ToString();
                }
            }
            if (sps_wtms != "")
            {
                sps_wtms = sps_wtms.Substring(1, sps_wtms.Length - 1);
            }
            //	22	备注	
            string	sps_bz=txtBZFBS.Text.Trim();
            //	23	负责部门	
            string	sps_fzbm="";
            foreach(ListItem li in cblFZBMFBS.Items)
            {
                if(li.Selected)
                {
                    sps_fzbm+=li.Value.ToString();
                }
            }
            //	24	索赔类别	
            int	sps_splb=3;
            if(action=="Add")
            {
               if(!CheckSubExist(sps_htbh,3))
                {
                   //合同索赔后，其合同信息表中的异常标识置1
                   List<string> sql=new List<string>();
                   string sqltext1="insert into TBPM_SUBCLAIM(SPS_ID,SPS_HTBH,SPS_ZRBZ,SPS_JSBM,SPS_SPWTMX,SPS_SPJE,SPS_ZZSPJE,SPS_SPDJRQ,SPS_SFKK,SPS_KKRQ,SPS_KKBZ,SPS_JSFZR,SPS_ZZFZR,SPS_ZZF,	SPS_SSCL,SPS_SFHF,SPS_HFYJ,SPS_FKR,SPS_FKRQ,SPS_NBCLYJ,SPS_WTMS,SPS_BZ,SPS_FZBM,SPS_SPLB)"+
                     "Values('"+sps_id+"','"+sps_htbh+"','"+sps_zrbz+"',"+sps_jsbm+",'"+sps_spwtmx+"',"+sps_spje+","+sps_zzspje+",'"+sps_spdjrq+"',"+sps_sfkk+",'"+sps_kkrq+"','"+sps_kkbz+"','"+sps_jsfzr+"','"+sps_zzfzr+"','"+sps_zzf+"',"+sps_sscl+","+sps_sfhf+",'"+sps_hfyj+"','"+sps_fkr+"','"+sps_fkrq+"','"+sps_nbclyj+"','"+sps_wtms+"','"+sps_bz+"','"+sps_fzbm+"',"+sps_splb+")";
                   sql.Add(sqltext1);
                   string sqltext2 = "update TBPM_CONPCHSINFO set PCON_ERROR=1 where PCON_BCODE='"+sps_htbh+"'";
                   sql.Add(sqltext2);
                   //主合同未索赔，则要针对主合同建立索赔信息，同时将对应合同信息表中的异常标识位置1
                   string zhth = dplZHTMCFBS.SelectedValue.ToString();//对应的主合同号
                   if(!CheckMainExist(zhth,1))
                    {
                       string spbh=sps_id;
                       string xmbh=dplXMMCFBS.SelectedValue.ToString();
                       string mthbh=dplZHTMCFBS.SelectedValue.ToString();
                       int type=1;
                       string sqltext3=this.SubOperateMain(spbh,xmbh,mthbh,type);
                       sql.Add(sqltext3);

                       string sqltext4 = "update TBPM_CONPCHSINFO set PCON_ERROR=1 where PCON_BCODE='" + mthbh + "'";
                      sql.Add(sqltext4);
                    }
                    DBCallCommon.ExecuteTrans(sql);
                    Response.Write("<script type=\"text/javascript\" language=\"javascript\">alert(\"添加成功！\")</script>");
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\" language=\"javascript\">alert(\"已添加该合同下分包商索赔信息，无法添加!\\r\\r请在原记录上修改！\")</script>");
                }
            }
            else if(action=="Edit")
            {
                string sqltext = "update TBPM_SUBCLAIM SET SPS_HTBH='" + sps_htbh + "',SPS_ZRBZ='" + sps_zrbz + "',SPS_JSBM=" + sps_jsbm + ",SPS_SPWTMX='" + sps_spwtmx + "',SPS_SPJE=" + sps_spje + ",SPS_ZZSPJE=" + sps_zzspje + ",SPS_SPDJRQ='" + sps_spdjrq + "',SPS_SFKK=" + sps_sfkk + ",SPS_KKRQ='" + sps_kkrq + "',SPS_KKBZ='" + sps_kkbz + "',SPS_JSFZR='" + sps_jsfzr + "',SPS_ZZFZR='" + sps_zzfzr + "',SPS_ZZF='" + sps_zzf + "',SPS_SSCL=" + sps_sscl + ",SPS_SFHF=" + sps_sfhf + ",SPS_HFYJ='" + sps_hfyj + "',SPS_FKR='" + sps_fkr + "',SPS_FKRQ='" + sps_fkrq + "',SPS_NBCLYJ='" + sps_nbclyj + "',SPS_WTMS='" + sps_wtms + "',SPS_BZ='" + sps_bz + "',SPS_FZBM='" + sps_fzbm + "'" +
                    " where ID="+Zhujian_ID+"";
                DBCallCommon.ExeSqlText(sqltext);
                Response.Write("<script type=\"text/javascript\" language=\"javascript\">alert(\"修改成功!\")</script>");
            }

        }
        private bool CheckMainExist(string id,int splb)
        {
            string sqltext = "select SPM_ID from TBPM_MAINCLAIM where SPM_HTBH='" + id + "' and SPM_SPLB="+splb+"";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckSubExist(string id, int splb)
        {
            string sqltext = "select SPS_ID from TBPM_SUBCLAIM where SPS_HTBH='" + id + "' and SPS_SPLB=" + splb + "";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 由于主合同不存在索赔记录，需要添加
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string SubOperateMain(string spbh,string xmbh,string mhtbh,int type)
        {
            //	1	索赔编号	
            string spm_id = spbh;
            //	2	项目编号
            string spm_xmbh =xmbh;
            //	3	主合同编号	
            string spm_htbh = mhtbh;
            //	4	接受部门	
            int spm_jsbm = 0;
            //	5	索赔问题描述	
            string spm_spwtmx ="";
            //	6	索赔金额	
            decimal spm_spje =0;
            //	7	最终索赔金额	
            decimal spm_zzspje = 0;
            //	8	索赔登记日期	
            string spm_spdjrq = "";
            //	9	是否扣款	
            int spm_sfkk = 1;
            //	10	扣款日期	
            string spm_kkrq = "";
            //	11	扣款备注	
            string spm_kkbz ="";
            //	12	技术负责人	
            string spm_jsfzr = "";
            //	13	质量负责人	
            string spm_zlfzr ="";
            //	14	制作方	
            string spm_zzf ="0";
            //	15	是否处理	
            int spm_sscl = 1;
            //	16	是否回复	
            int spm_sshf = 1;
            //	17	回复意见	
            string spm_hfyj = "";
            //	18	反馈人	
            string spm_fkr = "";
            //	19	反馈日期	
            string spm_fkrq ="";
            //	20	内部处理意见	
            string spm_nbclyj = "";
            //	21	问题描述	
            string spm_wtms = "";
            //	22	备注	
            string spm_bz = "";
            //	23	负责部门	
            string spm_fzbm = "";
            //	24	索赔类别	
            int spm_splb =type;
            string sqltext = "insert into TBPM_MAINCLAIM(SPM_ID,SPM_XMBH,SPM_HTBH,SPM_JSBM,SPM_SPWTMX,SPM_SPJE,SPM_ZZSPJE,SPM_SPDJRQ,SPM_SFKK,SPM_KKRQ,SPM_KKBZ,SPM_JSFZR,SPM_ZLFZR,SPM_ZZF,SPM_SSCL,SPM_SSHF,SPM_HFYJ,SPM_FKR,SPM_FKRQ,SPM_NBCLYJ,SPM_WTMS,SPM_BZ,SPM_FZBM,SPM_SPLB) " +
                         "Values('" + spm_id + "','" + spm_xmbh + "','" + spm_htbh + "'," + spm_jsbm + ",'" + spm_spwtmx + "','" + spm_spje + "','" + spm_zzspje + "','" + spm_spdjrq + "'," + spm_sfkk + ",'" + spm_kkrq + "','" + spm_kkbz + "','" + spm_jsfzr + "','" + spm_zlfzr + "','" + spm_zzf + "'," + spm_sscl + "," + spm_sshf + ",'" + spm_hfyj + "','" + spm_fkr + "','" + spm_fkrq + "','" + spm_nbclyj + "','" + spm_wtms + "',	'" + spm_bz + "','" + spm_fzbm + "'," + spm_splb + ")";
            return sqltext;
        }

        protected void btnConfirmFBS_Click(object sender, EventArgs e)
        {
            this.FBS_ExecSQL();
        }
#endregion
        //****************************************重机向分包商索赔*****************************
        #region
        protected void dplXMMCZJFBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dplXMMCZJFBS.SelectedIndex != 0)
            {
                //根据项目名称绑定工程名称，主合同名称，同时分包合同清空,索赔单号
                this.BindGC_Name(dplXMMCZJFBS, dplGCMCZJFBS);
                this.BindMainName(dplXMMCZJFBS, dplZHTMCZJFBS);
                this.dplZJFBHTMC.Items.Clear();
                this.lblZJFBHTH.Text = "";
                this.txtSPDHZJFBS.Text = "";
                //因为合同号不存在，也不能上传附件
                AT_ZJFBS.Visible = false;
            }
            else
            {
                //项目名称未选择
                txtSPDHZJFBS.Text = "";//索赔单号为空
                dplGCMCZJFBS.Items.Clear();//工程
                dplZJFBHTMC.Items.Clear();
                lblZJFBHTH.Text = "";
                dplZHTMCZJFBS.Items.Clear();
                txtSPDHZJFBS.Text = "";
            }
            this.InitUploadControls();
        }
        //根据主合同名称确定索赔合同号
        protected void dplZHTMCZJFBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dplZHTMCZJFBS.SelectedIndex != 0)
            {
                //主合同号
                string zhtbh = dplZHTMCZJFBS.SelectedValue.ToString();
                //查询该项目商务合同下是否建立索赔记录
                //如果建立索赔记录：直接读出，否则重新创建
                string sqltext = "select SPM_ID from TBPM_MAINCLAIM where SPM_HTBH='" + zhtbh + "' AND SPM_SPLB=0";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                if (dr.HasRows)
                {
                    dr.Read();
                    txtSPDHZJFBS.Text = dr["SPM_ID"].ToString();
                    dr.Close();
                }
                else
                {
                    txtSPDHZJFBS.Text = this.CreateSPDH();
                }
            }
            else
            {
                txtSPDHZJFBS.Text = "";
            }
        }
        //根据项目工程名称确定分包合同号
        protected void dplGCMCZJFBS_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindHTBH(dplXMMCZJFBS, dplGCMCZJFBS, dplZJFBHTMC);
        }
        //根据分包合同名称确定分包合同号
        protected void dplZJFBHTMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblZJFBHTH.Text = dplZJFBHTMC.SelectedValue.ToString();
            this.InitUploadControls();
        }
        //提交
        protected void btnConfirmZJFBS_Click(object sender, EventArgs e)
        {
            this.ZJ_FBS_ExecSQL();
        }

        private void ZJ_FBS_ExecSQL()
        {
            //	1	索赔编号	
            string sps_id = txtSPDHZJFBS.Text;
            //	2	分包合同号	
            string sps_htbh = lblZJFBHTH.Text;
            //	3	责任班组/责任分包商	
            string sps_zrbz = "";
            //	4	接受部门	
            int sps_jsbm = Convert.ToInt16(dplJSBMZJFBS.SelectedValue.ToString());
            //	5	索赔问题描述	
            string sps_spwtmx = txtSPWTMSZJFBS.Text.Trim();
            //	6	索赔金额	
            decimal sps_spje = (txtSPJEZJFBS.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtSPJEZJFBS.Text.Trim()));
            //	7	最终索赔金额	
            decimal sps_zzspje = (txtZZSPJEZJFBS.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtZZSPJEZJFBS.Text.Trim()));
            //	8	索赔登记日期	
            string sps_spdjrq = txtSPDJRQZJFBS.Text.Trim();
            //	9	是否扣款	
            int sps_sfkk = Convert.ToInt16(rblSFKKZJFBS.SelectedValue.ToString());
            //	10	扣款日期	
            string sps_kkrq = txtKKRQZJFBS.Text.Trim();
            //	11	扣款备注	
            string sps_kkbz = txtKKBZZJFBS.Text.Trim();
            //	12	技术负责人	
            string sps_jsfzr = "";
            //	13	质量负责人	
            string sps_zzfzr ="";
            //	14	制作方	
            string sps_zzf = "";
            //	15	是否处理
            int sps_sscl =Convert.ToInt16(rblSFHFZJFBS.SelectedValue.ToString());
            //	16	是否回复
            int sps_sfhf = Convert.ToInt16(rblSFHFZJFBS.SelectedValue.ToString());
            //	17	回复意见	
            string sps_hfyj = txtHFYJZJFBS.Text.Trim();
            //	18	反馈人	
            string sps_fkr = txtDJRZJFBS.Text.Trim();
            //	19	反馈日期	
            string sps_fkrq = txtDJRQZJFBS.Text.Trim();
            //	20	内部处理意见	
            string sps_nbclyj = "";
            //	21	问题描述	
            string sps_wtms = "";
            //	22	备注	
            string sps_bz = "";
            //	23	负责部门	
            string sps_fzbm = "";
            //	24	索赔类别	
            int sps_splb = 2;
            if (action == "Add")
            {
                if(!CheckSubExist(sps_htbh,2))
                {
                   //合同索赔后，其合同信息表中的异常标识置1
                   List<string> sql = new List<string>();
                   string sqltext1 = "insert into TBPM_SUBCLAIM(SPS_ID,SPS_HTBH,SPS_ZRBZ,SPS_JSBM,SPS_SPWTMX,SPS_SPJE,SPS_ZZSPJE,SPS_SPDJRQ,SPS_SFKK,SPS_KKRQ,SPS_KKBZ,SPS_JSFZR,SPS_ZZFZR,SPS_ZZF,	SPS_SSCL,SPS_SFHF,SPS_HFYJ,SPS_FKR,SPS_FKRQ,SPS_NBCLYJ,SPS_WTMS,SPS_BZ,SPS_FZBM,SPS_SPLB)" +
                      "Values('" + sps_id + "','" + sps_htbh + "','" + sps_zrbz + "'," + sps_jsbm + ",'" + sps_spwtmx + "'," + sps_spje + "," + sps_zzspje + ",'" + sps_spdjrq + "'," + sps_sfkk + ",'" + sps_kkrq + "','" + sps_kkbz + "','" + sps_jsfzr + "','" + sps_zzfzr + "','" + sps_zzf + "'," + sps_sscl + "," + sps_sfhf + ",'" + sps_hfyj + "','" + sps_fkr + "','" + sps_fkrq + "','" + sps_nbclyj + "','" + sps_wtms + "','" + sps_bz + "','" + sps_fzbm + "'," + sps_splb + ")";
                   sql.Add(sqltext1);
                   string sqltext2 = "update TBPM_CONPCHSINFO set PCON_ERROR=1 where PCON_BCODE='" + sps_htbh + "'";
                   sql.Add(sqltext2);
                   //主合同未索赔，则要针对主合同建立索赔信息，同时将对应合同信息表中的异常标识位置1
                   string zhth = dplZHTMCZJFBS.SelectedValue.ToString();
                   if (!CheckMainExist(zhth,0))
                   {
                      string spbh = sps_id;
                      string xmbh = dplXMMCZJFBS.SelectedValue.ToString();
                      string mthbh = dplZHTMCZJFBS.SelectedValue.ToString();
                      int type = 0;
                      string sqltext3 = this.SubOperateMain(spbh, xmbh, mthbh, type);
                      sql.Add(sqltext3);

                      string sqltext4 = "update TBPM_CONPCHSINFO set PCON_ERROR=1 where PCON_BCODE='" + mthbh + "'";
                      sql.Add(sqltext4);
                    }
                    DBCallCommon.ExecuteTrans(sql);
                    Response.Write("<script type=\"text/javascript\" language=\"javascript\">alert(\"添加成功！\")</script>");
                  }
                else
                 {
                     Response.Write("<script type=\"text/javascript\" language=\"javascript\">alert(\"已添加该合同下的索赔信息，无法再添加!\\r\\r请在原记录上修改！\")</script>");
                 }
            }
            else if(action=="Edit")
            {
                string sqltext = "update TBPM_SUBCLAIM SET SPS_HTBH='" + sps_htbh + "',SPS_ZRBZ='" + sps_zrbz + "',SPS_JSBM=" + sps_jsbm + ",SPS_SPWTMX='" + sps_spwtmx + "',SPS_SPJE=" + sps_spje + ",SPS_ZZSPJE=" + sps_zzspje + ",SPS_SPDJRQ='" + sps_spdjrq + "',SPS_SFKK=" + sps_sfkk + ",SPS_KKRQ='" + sps_kkrq + "',SPS_KKBZ='" + sps_kkbz + "',SPS_JSFZR='" + sps_jsfzr + "',SPS_ZZFZR='" + sps_zzfzr + "',SPS_ZZF='" + sps_zzf + "',SPS_SSCL=" + sps_sscl + ",SPS_SFHF=" + sps_sfhf + ",SPS_HFYJ='" + sps_hfyj + "',SPS_FKR='" + sps_fkr + "',SPS_FKRQ='" + sps_fkrq + "',SPS_NBCLYJ='" + sps_nbclyj + "',SPS_WTMS='" + sps_wtms + "',SPS_BZ='" + sps_bz + "',SPS_FZBM='" + sps_fzbm + "'" +
                       " where ID="+Zhujian_ID+"";
                DBCallCommon.ExeSqlText(sqltext);
                Response.Write("<script type=\"text/javascript\" language=\"javascript\">alert(\"修改成功!\")</script>");

            }
        }
        #endregion
        //***************************************重机业主******************************
        #region
        protected void dplXMMCZJYZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dplXMMCZJYZ.SelectedIndex != 0)
            {
                string pjID = dplXMMCZJYZ.SelectedValue.ToString();
                string sqltext = "select PCON_BCODE,PCON_NAME from TBPM_CONPCHSINFO where PCON_PJID='" + pjID + "' and PCON_FORM=0";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                dplZHTMCZJYZ.DataSource = dt;
                dplZHTMCZJYZ.DataTextField = "PCON_NAME";
                dplZHTMCZJYZ.DataValueField = "PCON_BCODE";
                dplZHTMCZJYZ.DataBind();
                dplZHTMCZJYZ.Items.Insert(0, new ListItem("-请选择-", ""));
                dplZHTMCZJYZ.SelectedIndex = 0;
            }
            else
            {
                lblZHTBHZJYZ.Text = "";
                dplZHTMCZJYZ.Items.Clear();
            }
            this.InitUploadControls();
        }

        protected void dplZHTMCZJYZ_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblZHTBHZJYZ.Text = dplZHTMCZJYZ.SelectedValue.ToString();
            this.InitUploadControls();
        }
        //添加重机-业主索赔信息
        protected void btnConfirmZJYZ_Click(object sender, EventArgs e)
        {

            //	1	索赔编号	
            string spm_id = txtSPDHZJYZ.Text.Trim();
            //	2	项目编号
            string spm_xmbh = dplXMMCZJYZ.SelectedValue.ToString();
            //	3	主合同编号	
            string spm_htbh = lblZHTBHZJYZ.Text;
            //	4	接受部门	
            int spm_jsbm = Convert.ToInt16(dplJSBMZJYZ.SelectedValue.ToString());
            //	5	索赔问题描述	
            string spm_spwtmx = txtSPWTMSZJYZ.Text.Trim();
            //	6	索赔金额	
            decimal spm_spje = Convert.ToDecimal(txtSPJEZJYZ.Text.Trim());
            //	7	最终索赔金额	
            decimal spm_zzspje = (txtZZSPJEZJYZ.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtZZSPJEZJYZ.Text.Trim()));
            //	8	索赔登记日期	
            string spm_spdjrq = txtSPDJRQZJYZ.Text.Trim();
            //	9	是否扣款	
            int spm_sfkk = Convert.ToInt16(rblSFKKZJYZ.SelectedValue.ToString());
            //	10	扣款日期	
            string spm_kkrq = txtKKRQZJYZ.Text.Trim();
            //	11	扣款备注	
            string spm_kkbz = txtKKBZZJYZ.Text.Trim();
            //	12	技术负责人	
            string spm_jsfzr = "";
            //	13	质量负责人	
            string spm_zlfzr = "";
            //	14	制作方	
            string spm_zzf = "";
            //	15	是否处理	
            int spm_sscl =Convert.ToInt16(rblCLJGFBS.SelectedValue.ToString());
            //	16	是否回复	
            int spm_sshf = Convert.ToInt16(rblSFHFZJYZ.SelectedValue.ToString());
            //	17	回复意见	
            string spm_hfyj = txtHFYJZJYZ.Text.Trim();
            //	18	反馈人	
            string spm_fkr = txtFKRZJYZ.Text.Trim();
            //	19	反馈日期	
            string spm_fkrq = txtFKRQZJYZ.Text.Trim();
            //	20	内部处理意见	
            string spm_nbclyj ="";
            //	21	问题描述	
            string spm_wtms = "";
            //	22	备注	
            string spm_bz = "";
            //	23	负责部门	
            string spm_fzbm = "";
            //	24	索赔类别	
            int spm_splb = 1;

            if (action == "Add")
            {
                //如果该索赔单号已经存在对应索赔记录，则无法添加
                if (!CheckMainExist(spm_htbh, 1))
                {
                    //如果某一合同索赔，合同信息表中异常标识置1
                    List<string> sql = new List<string>();
                    string sqltext1 = "insert into TBPM_MAINCLAIM(SPM_ID,SPM_XMBH,SPM_HTBH,SPM_JSBM,SPM_SPWTMX,SPM_SPJE,SPM_ZZSPJE,SPM_SPDJRQ,SPM_SFKK,SPM_KKRQ,SPM_KKBZ,SPM_JSFZR,SPM_ZLFZR,SPM_ZZF,SPM_SSCL,SPM_SSHF,SPM_HFYJ,SPM_FKR,SPM_FKRQ,SPM_NBCLYJ,SPM_WTMS,SPM_BZ,SPM_FZBM,SPM_SPLB)" +
                        "Values('" + spm_id + "','" + spm_xmbh + "','" + spm_htbh + "'," + spm_jsbm + ",'" + spm_spwtmx + "','" + spm_spje + "','" + spm_zzspje + "','" + spm_spdjrq + "'," + spm_sfkk + ",'" + spm_kkrq + "','" + spm_kkbz + "','" + spm_jsfzr + "','" + spm_zlfzr + "','" + spm_zzf + "'," + spm_sscl + "," + spm_sshf + ",'" + spm_hfyj + "','" + spm_fkr + "','" + spm_fkrq + "','" + spm_nbclyj + "','" + spm_wtms + "',	'" + spm_bz + "','" + spm_fzbm + "'," + spm_splb + ")";
                    string sqltext2 = "update TBPM_CONPCHSINFO set PCON_ERROR=1 where PCON_BCODE='" + spm_htbh + "'";
                    sql.Add(sqltext1);
                    sql.Add(sqltext2);
                    DBCallCommon.ExecuteTrans(sql);
                }
                else
                {
                    Response.Write("<script type=\"text/javascript\" language=\"javascript\">alert(\"已添加该合同下对业主的索赔信息，无法添加!\\r\\r请在原记录上修改！\")</script>");
                }
            }
            else if (action == "Edit")
            {
                string sqltext = "update TBPM_MAINCLAIM set SPM_ID='" + spm_id + "',SPM_XMBH='" + spm_xmbh + "',SPM_HTBH='" + spm_htbh + "',SPM_JSBM=" + spm_jsbm + ",SPM_SPWTMX='" + spm_spwtmx + "',SPM_SPJE=" + spm_spje + ",SPM_ZZSPJE=" + spm_zzspje + ",SPM_SPDJRQ='" + spm_spdjrq + "',SPM_SFKK=" + spm_sfkk + ",SPM_KKRQ='" + spm_kkrq + "',SPM_KKBZ='" + spm_kkbz + "',SPM_JSFZR='" + spm_jsfzr + "',SPM_ZLFZR='" + spm_zlfzr + "',SPM_ZZF='" + spm_zzf + "',SPM_SSCL=" + spm_sscl + ",SPM_SSHF=" + spm_sshf + ",SPM_HFYJ='" + spm_hfyj + "',SPM_FKR='" + spm_fkr + "',SPM_FKRQ='" + spm_fkrq + "',SPM_NBCLYJ='" + spm_nbclyj + "',SPM_WTMS='" + spm_wtms + "',SPM_BZ='" + spm_bz + "',SPM_FZBM='" + spm_fzbm + "'" +
                    " where ID=" + Zhujian_ID + "";
                DBCallCommon.ExeSqlText(sqltext);
                Response.Write("<script type=\"text/javascript\" language=\"javascript\">alert(\"修改成功！\")</script>");

            }
        }
        #endregion

        //********************************扣款*****************************************
        /// <summary>
        /// 对扣款的控制：添加时不能扣款，只有再修改时才能扣款
        /// </summary>
        /// <param name="rbl"></param>
        /// <param name="op"></param>
        private void KKControl(string op)
        {
           rblSSKK.Enabled = false;
           rblSFKKZJYZ.Enabled = false;
           rblSFKKFBS.Enabled = false;
           rblSFKKZJFBS.Enabled = false;           
            if (op == "Add")
            {
                //YZ
                rblSSKK.SelectedIndex = 1;//是否扣款-否
                btnKK.Visible = false;//扣款
                txtZZSPJE.Enabled = false;//最终索赔金额
                //FBS
                rblSFKKFBS.SelectedIndex = 1;
                txtZZSPJEFBS.Enabled = false;
                btnKKFBS.Visible = false;
                //ZJYZ
                rblSFKKZJYZ.SelectedIndex = 1;
                txtZZSPJEZJFBS.Enabled = false;
                btnKKZJYZ.Visible = false;
                //ZJFBS
                rblSFKKZJFBS.SelectedIndex = 1;
                txtZZSPJEZJFBS.Enabled = false;
                btnKKZJFBS.Visible = false;

            }
            else if(op=="View")
            {
                btnKK.Visible = false;

                btnKKFBS.Visible = false;

                btnKKZJYZ.Visible = false;

                btnKKZJFBS.Visible = false;
            }
        }

        //更新合同信息表和索赔记录表
        //YZ扣款 "-" OK
        protected void btnKK_Click(object sender, EventArgs e)
        {
            decimal kkje = 0;
            if (txtZZSPJE.Text.Trim() != "")
            {

                kkje = Convert.ToDecimal(txtZZSPJE.Text.Trim());
                string sqltext1 = "update TBPM_CONPCHSINFO set PCON_SPJE=PCON_SPJE-" + kkje + " where PCON_BCODE='" + txtZHTH.Text + "'";
                string sqltext2 = "update TBPM_MAINCLAIM set  SPM_ZZSPJE=" + kkje + ",SPM_SFKK=0 where SPM_HTBH='" + txtZHTH.Text + "' and SPM_ID='" + txtSPBH.Text + "' and SPM_SPLB=0";
                List<string> sql = new List<string>();
                sql.Add(sqltext1);
                sql.Add(sqltext2);
                DBCallCommon.ExecuteTrans(sql);
                txtZZSPJE.Enabled = false;
                rblSSKK.SelectedIndex = 0;
                //txtKKRQ.Enabled = false;
                btnKK.Visible = false;
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('扣款成功!')", true);
            }
            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('索赔金额不能为空!')", true);
            }
            
            //Response.Write("<script>alert('扣款成功!')</script>");
            
        }
        //ZJYZ扣款 "+"
        protected void btnKKZJYZ_Click(object sender, EventArgs e)
        {
            decimal kkje = 0;
            if (txtZZSPJEZJYZ.Text.Trim() != "")
            {
                kkje = Convert.ToDecimal(txtZZSPJEZJYZ.Text.Trim());
                string sqltext1 = "update TBPM_CONPCHSINFO set PCON_SPJE=PCON_SPJE+" + kkje + " where PCON_BCODE='" + lblZHTBHZJYZ.Text + "'";
                string sqltext2 = "update TBPM_MAINCLAIM set  SPM_ZZSPJE=" + kkje + ",SPM_SFKK=0 where SPM_HTBH='" + lblZHTBHZJYZ.Text + "' and SPM_ID='" + txtSPDHZJYZ.Text + "' and SPM_SPLB=1";
                List<string> sql = new List<string>();
                sql.Add(sqltext1);
                sql.Add(sqltext2);
                DBCallCommon.ExecuteTrans(sql);
                txtZZSPJEZJYZ.Enabled = false;
                rblSFKKZJYZ.SelectedIndex = 0;
                //txtKKRQZJYZ.Enabled = false;
                btnKKZJYZ.Visible = false;
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('扣款成功!')", true);
            }
            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('索赔金额不能为空!')", true);
            }
        }
        //ZJFBS扣款"+"
        protected void btnKKZJFBS_Click(object sender, EventArgs e)
        {
            decimal kkje = 0;
            if (txtZZSPJEZJFBS.Text.Trim() != "")
            {
                kkje = Convert.ToDecimal(txtZZSPJEZJFBS.Text.Trim());
                string sqltext1 = "update TBPM_CONPCHSINFO set PCON_SPJE=PCON_SPJE+" + kkje + " where PCON_BCODE='" + lblZJFBHTH.Text.Trim() + "'";
                string sqltext2 = "update TBPM_SUBCLAIM set  SPS_ZZSPJE=" + kkje + ",SPS_SFKK=0 where SPS_HTBH='" + lblZJFBHTH.Text.Trim() + "' and SPS_ID='" +txtSPDHZJFBS.Text.Trim()+ "' and SPS_SPLB=2";
                List<string> sql = new List<string>();
                sql.Add(sqltext1);
                sql.Add(sqltext2);
                DBCallCommon.ExecuteTrans(sql);
                btnKKZJFBS.Visible = false;
                rblSFKKZJFBS.SelectedIndex = 0;
                txtSPJEZJFBS.Enabled = false;
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('扣款成功!')", true);
            }
            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('索赔金额不能为空!')", true);
            }
        }
        // FBS扣款"-"
        protected void btnKKFBS_Click(object sender, EventArgs e)
        {
            decimal kkje = 0;
            if (txtZZSPJEFBS.Text.Trim()!= "")
            {
                kkje =Convert.ToDecimal(txtZZSPJEFBS.Text.Trim());
                string sqltext1 = "update TBPM_CONPCHSINFO set PCON_SPJE=PCON_SPJE-" + kkje + " where PCON_BCODE='" + txtFBHTH.Text.Trim() + "'";
                string sqltext2 = "update TBPM_SUBCLAIM set  SPS_ZZSPJE=" + kkje + ",SPS_SFKK=0 where SPS_HTBH='" + txtFBHTH.Text.Trim() + "' and SPS_ID='" + txtSPDHFBS.Text.Trim() + "' and SPS_SPLB=3";
                List<string> sql = new List<string>();
                sql.Add(sqltext1);
                sql.Add(sqltext2);
                DBCallCommon.ExecuteTrans(sql);
                btnKKFBS.Visible = false;
                rblSFKKFBS.SelectedIndex = 0;
                txtZZSPJEFBS.Enabled = false;
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('扣款成功!')", true);
            }
            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('索赔金额不能为空!')", true);
            }
        }

    }
}
