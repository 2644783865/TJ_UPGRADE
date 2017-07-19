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
using System.Data.SqlClient;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_Claim_ZJYZ : System.Web.UI.Page
    {
        string action = "";
        string spdh = "";//索赔单号
        string splb = "";//索赔类别
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["Action"];
            spdh = Request.QueryString["Spid"];
            splb = Request.QueryString["Splb"];
            if (!IsPostBack)
            {
                //添加时先创建一个全球唯一标识，存储在一个lable中，用来关联合同与附件，避免因合同号占用而导致附件关联错误
                if (action == "Add")
                {
                    Guid tempid = Guid.NewGuid();
                    lbl_UNID.Text = tempid.ToString();
                }
                this.InitPage();
            }
            Contract_Data.ContractClass.InitUploadControls(AT_ZJYZ, lbl_UNID, 1);
        }

        //页面初始化
        private void InitPage()
        {
            this.KKControl(action);
            if (action == "Add")
            {                
                //是否回复
                rblSFHFZJYZ.SelectedIndex = 1;
                //是否处理
                rbLSSCLZJYZ.SelectedIndex = 0;
                //是否扣款
                rblSFKKZJYZ.SelectedIndex = 1;
                
            }
            else if (action == "Edit")
            {
                this.BindZJYZ(spdh, splb);
            }
            else if (action == "View")
            {
                this.BindZJYZ(spdh, splb);
                btnConfirmZJYZ.Enabled = false;
                palZJYZ.Enabled = false;
                Contract_Data.ContractClass.UploadControlSet(AT_ZJYZ, action);
            }
        }
        /// <summary>
        /// 对扣款的控制：添加时不能扣款，只有再修改时才能扣款
        /// </summary>
        private void KKControl(string op)
        {
            rblSFKKZJYZ.Enabled = false;
            if (op == "Add")
            {
                //ZJYZ
                rblSFKKZJYZ.SelectedIndex = 1;

                btnKKZJYZ.Visible = false;
            }
            else if (op == "View")
            {
                btnKKZJYZ.Visible = false;
            }
        }

        //添加重机-业主索赔信息
        protected void btnConfirmZJYZ_Click(object sender, EventArgs e)
        {
            if (txtSPDH.Text.Trim() != "")
            {
                this.ZJYZ_ExecSQL();
            }
            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert(\"请选择索赔合同！！！\");", true);
            }
        }
        private void ZJYZ_ExecSQL()
        {
            //全球唯一标识符
            string guid = lbl_UNID.Text.Trim();
            //	1	索赔编号	
            string sps_id = txtSPDH.Text.Trim();
            //	2	主合同编号	
            string sps_htbh = HTBH.Text;
            //	3	责任班组/责任分包商	
            string sps_zrbz = "";
            //	4	接受部门	
            int sps_jsbm = Convert.ToInt16(dplJSBMZJYZ.SelectedValue.ToString());
            //	5	索赔问题描述	
            string sps_spwtmx = txtSPWTMSZJYZ.Text.Trim();
            //	6	索赔金额	
            decimal sps_spje = Convert.ToDecimal(txtSPJEZJYZ.Text.Trim() == "" ? "0" : txtSPJEZJYZ.Text.Trim());
            //	7	最终索赔金额	
            decimal sps_zzspje = (txtZZSPJEZJYZ.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtZZSPJEZJYZ.Text.Trim()));
            //	8	索赔登记日期	
            string sps_spdjrq = txtSPDJRQZJYZ.Text.Trim();
            //	9	是否扣款	
            int sps_sfkk = Convert.ToInt16(rblSFKKZJYZ.SelectedValue.ToString());
            //	10	扣款日期	
            //string sps_kkrq = txtKKRQZJYZ.Text.Trim() == "" ? DateTime.Now.ToShortDateString() : txtKKRQZJYZ.Text.Trim();
            //	11	扣款备注	
            string sps_kkbz = txtKKBZZJYZ.Text.Trim();
            //	12	技术负责人	
            string sps_jsfzr = "";
            //	13	质量负责人	
            string sps_zzfzr = "";
            //	14	制作方	
            string sps_zzf = "";
            //	15	是否处理
            int sps_sscl = Convert.ToInt16(rbLSSCLZJYZ.SelectedValue.ToString());
            //	16	是否回复
            int sps_sfhf = Convert.ToInt16(rblSFHFZJYZ.SelectedValue.ToString());

            //	17	回复意见	
            string sps_hfyj = txtHFYJZJYZ.Text.Trim();
            //	18	反馈人	
            string sps_fkr = txtFKRZJYZ.Text.Trim();
            //	19	反馈日期	
            string sps_fkrq = txtFKRQZJYZ.Text.Trim();
            //	20	内部处理意见	
            string sps_nbclyj = "";
            //	21	问题描述	
            string sps_wtms = "";
            //	22	备注	
            string sps_bz = "";
            //	23	负责部门	
            string sps_fzbm = "";
            //	24	索赔类别	
            int sps_splb = Convert.ToInt32(splb);

            if (action == "Add")
            {
                //插入前再检查索赔单号，避免多人同时添加时单号重复
                this.GreatCode(HTBH.Text);
                sps_id = txtSPDH.Text;

                //合同索赔后，其合同信息表中的异常标识置1
                List<string> sql = new List<string>();
                string sqltext1 = "insert into TBPM_SUBCLAIM(SPS_ID,SPS_HTBH,SPS_ZRBZ,SPS_JSBM,SPS_SPWTMX,SPS_SPJE,SPS_ZZSPJE,SPS_SPDJRQ,SPS_SFKK,SPS_FKR,SPS_FKRQ,SPS_KKBZ,SPS_JSFZR,SPS_ZZFZR,SPS_ZZF,SPS_SSCL,SPS_SFHF,SPS_NBCLYJ,SPS_WTMS,SPS_BZ,SPS_FZBM,SPS_SPLB,GUID)" +
                "Values('" + sps_id + "','" + sps_htbh + "','" + sps_zrbz + "'," + sps_jsbm + ",'" + sps_spwtmx + "'," + sps_spje + "," + sps_zzspje + ",'" + sps_spdjrq + "'," + sps_sfkk + ",'" + sps_fkr + "','" + sps_fkrq + "','" + sps_kkbz + "','" + sps_jsfzr + "','" + sps_zzfzr + "','" + sps_zzf + "'," + sps_sscl + "," + sps_sfhf + ",'" + sps_nbclyj + "','" + sps_wtms + "','" + sps_bz + "','" + sps_fzbm + "'," + sps_splb + ",'" + guid + "')";
                sql.Add(sqltext1);
                string sqltext2 = "update TBPM_CONPCHSINFO set PCON_ERROR=1 where PCON_BCODE='" + sps_htbh + "'";
                sql.Add(sqltext2);

                DBCallCommon.ExecuteTrans(sql);

                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert(\"添加成功！！！\");", true);


            }
            else if (action == "Edit")
            {

                string sqltext = "update TBPM_SUBCLAIM SET SPS_HTBH='" + sps_htbh + "',SPS_ZRBZ='" + sps_zrbz + "',SPS_JSBM=" + sps_jsbm + ",SPS_SPWTMX='" + sps_spwtmx + "',SPS_HFYJ='" + sps_hfyj + "',SPS_FKR='" + sps_fkr + "',SPS_FKRQ='" + sps_fkrq + "',SPS_SPJE=" + sps_spje + ",SPS_ZZSPJE=" + sps_zzspje + ",SPS_SPDJRQ='" + sps_spdjrq + "',SPS_SFKK=" + sps_sfkk + ",SPS_KKBZ='" + sps_kkbz + "',SPS_JSFZR='" + sps_jsfzr + "',SPS_ZZFZR='" + sps_zzfzr + "',SPS_ZZF='" + sps_zzf + "',SPS_SSCL=" + sps_sscl + ",SPS_SFHF='" + sps_sfhf + "',SPS_NBCLYJ='" + sps_nbclyj + "',SPS_WTMS='" + sps_wtms + "',SPS_BZ='" + sps_bz + "',SPS_FZBM='" + sps_fzbm + "'" +
                       " where SPS_ID='" + spdh + "' AND SPS_SPLB=" + splb + "";


                DBCallCommon.ExeSqlText(sqltext);

                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert(\"修改成功!\");", true);

            }
            Response.Redirect("CM_Claim_Total.aspx?viewtype=2&splb=" + splb);
        }
        protected void HTBH_Textchanged(object sender, EventArgs e)
        {
            string htbh = "";

            if (HTBH.Text.ToString().Contains("|"))
            {
                htbh = HTBH.Text.Substring(0, HTBH.Text.ToString().IndexOf("|"));
                HTBH.Text = htbh;

                string a = "select PCON_BCODE,PCON_PJNAME,PCON_ENGNAME from TBPM_CONPCHSINFO where  PCON_BCODE='" + HTBH.Text.ToString() + "'";

                SqlDataReader dr2 = DBCallCommon.GetDRUsingSqlText(a);
                if (dr2.HasRows)
                {
                    dr2.Read();

                    txtXMMC.Text = dr2["PCON_PJNAME"].ToString();
                    txtGCMC.Text = dr2["PCON_ENGNAME"].ToString();

                    //索赔单号
                    this.GreatCode(HTBH.Text);
                    dr2.Close();
                }

            }
            else
            {
                txtXMMC.Text = "";
                txtGCMC.Text = "";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写有效的合同号！');", true); return;
            }

        }

        private void GreatCode(string htbh)
        {
            string sqltext = "select top 1 SPS_ID from TBPM_SUBCLAIM where SPS_HTBH='" + htbh + "' order by id desc";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                double aa = Convert.ToDouble(dt.Rows[0]["SPS_ID"].ToString().Split('-')[1]) + 1;
                txtSPDH.Text = htbh + "-" + aa.ToString();
            }
            else
            {
                txtSPDH.Text = htbh + "-1";
            }
        }

        //数据绑定
        private void BindZJYZ(string id, string op)
        {
            string sqltext = "select * from TBPM_SUBCLAIM where SPS_ID='" + id + "' and SPS_SPLB=" + op + "";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.HasRows)
            {
                dr.Read();
                //唯一编号
                lbl_UNID.Text = dr["GUID"].ToString();
                //	1	索赔编号	********
                txtSPDH.Text = dr["sps_id"].ToString();
                //	2	项目编号*****项目名称
                HTBH.Text = dr["sps_htbh"].ToString();
                string a = "select PCON_BCODE,PCON_PJNAME,PCON_ENGNAME from TBPM_CONPCHSINFO where  PCON_BCODE='" + HTBH.Text.Trim() + "'";

                SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(a);
                if (dr1.HasRows)
                {
                    dr1.Read();                   

                    txtXMMC.Text = dr1["PCON_PJNAME"].ToString();
                    txtGCMC.Text = dr1["PCON_ENGNAME"].ToString();
                    dr1.Close();
                }
                else
                {

                    txtXMMC.Text = "";
                    txtGCMC.Text = "";
                }
                //	4	接受部门	
                dplJSBMZJYZ.SelectedIndex = Convert.ToInt16(dr["sps_jsbm"].ToString());
                //	5	索赔问题描述	
                txtSPWTMSZJYZ.Text = dr["sps_spwtmx"].ToString();
                //	6	索赔金额	
                txtSPJEZJYZ.Text = dr["sps_spje"].ToString();
                txtSPJEZJYZ1.Text = dr["sps_spje"].ToString();
                //	7	最终索赔金额	
                txtZZSPJEZJYZ.Text = dr["sps_zzspje"].ToString();
                //	8	索赔登记日期	
                txtSPDJRQZJYZ.Text = dr["sps_spdjrq"].ToString();
                //	9	是否扣款	
                rblSFKKZJYZ.SelectedIndex = Convert.ToInt16(dr["sps_sfkk"].ToString());
                if (rblSFKKZJYZ.SelectedIndex == 0)
                {
                    rblSFKKZJYZ.Enabled = false;
                    
                    btnKKZJYZ.Visible = false;
                    txtKKRQZJYZ.Enabled = false;
                }
                //	10	扣款日期	
                txtKKRQZJYZ.Text = dr["sps_kkrq"].ToString();
                //	11	扣款备注	
                txtKKBZZJYZ.Text = dr["sps_kkbz"].ToString();
                //	12	技术负责人	
                //	13	质量负责人	
                //	14	制作方	
                //	15	是否处理	
                rbLSSCLZJYZ.SelectedIndex = Convert.ToInt16(dr["sps_sscl"].ToString());
                //	16	是否回复	
                rblSFHFZJYZ.SelectedIndex = Convert.ToInt16(dr["sps_sfhf"].ToString());
                //	17	回复意见	
                txtHFYJZJYZ.Text = dr["sps_hfyj"].ToString();
                //	18	反馈人	
                txtFKRZJYZ.Text = dr["sps_fkr"].ToString();
                //	19	反馈日期	
                txtFKRQZJYZ.Text = dr["sps_fkrq"].ToString();
                //	20	内部处理意见	
                //	21	问题描述	
                //	22	备注	
                //	23	负责部门	
                //	24	索赔类别	
                dr.Close();
            }
        }

        //扣款
        //YZ扣款 "-" OK
        protected void btnKKZJYZ_Click(object sender, EventArgs e)
        {
            decimal kkje = 0;
            string kkrq = DateTime.Now.ToString("yyyy-MM-dd");
            if (txtZZSPJEZJYZ.Text.Trim() != "")
            {
                kkje = Convert.ToDecimal(txtZZSPJEZJYZ.Text.Trim());
                string sqltext1 = "update TBPM_CONPCHSINFO set PCON_SPJE=PCON_SPJE+" + kkje + " where PCON_BCODE='" + HTBH.Text.Trim() + "'";
                string sqltext2 = "update TBPM_SUBCLAIM set  SPS_ZZSPJE=" + kkje + ",SPS_SFKK=0,SPS_KKRQ='" + kkrq + "' where SPS_HTBH='" + HTBH.Text.Trim() + "' and SPS_ID='" + txtSPDH.Text.Trim() + "' and SPS_SPLB=" + splb;
                List<string> sql = new List<string>();
                sql.Add(sqltext1);
                sql.Add(sqltext2);
                DBCallCommon.ExecuteTrans(sql);
                btnKKZJYZ.Visible = false;
                rblSFKKZJYZ.SelectedIndex = 0;

                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('扣款成功!已扣款" + kkje + "并添加到合同索赔金额中');", true);
            }
            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('索赔金额不能为空!');", true);
            }
        }
    }
}