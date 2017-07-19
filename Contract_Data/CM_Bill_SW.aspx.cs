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
    public partial class CM_Bill_SW : System.Web.UI.Page
    {
        string action = "";
        string billID = "";
        string htbh = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["Action"];
            billID = Request.QueryString["BRid"];
            htbh = Request.QueryString["condetail_id"];
            if (!IsPostBack)
            {
                if (action == "View")
                {
                    btnConfirm.Visible = false;
                    btn_Add.Visible = false;
                    lblFP.Text = "查看发票单据";
                    this.BindData(billID);
                }
                else if (action == "Add")
                {
                    lblFP.Text = "添加发票单据";
                    txtHTBH.Text = htbh;
                    btn_Add.Visible = true;
                    ShowData();
                }
                else
                {
                    lblFP.Text = "修改发票单据";
                    this.BindData(billID);
                    btn_Add.Visible = false;
                }

            }
        }

        private void ShowData()
        {
            string sql = "select * from TBPM_CONPCHSINFO where PCON_BCODE='" + htbh + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                txtDFBH.Text = dr["PCON_YZHTH"].ToString();
                txtKPDW.Text = dr["PCON_CUSTMNAME"].ToString();
                txtProj.Text = dr["PCON_ENGNAME"].ToString();
                txtEng.Text = dr["PCON_ENGTYPE"].ToString();
            }
        }

        //根据合同号绑定发票单据
        private void BindData(string billid)
        {
            string sqlstr = "select * from TBPM_BUSBILLRECORD where BR_ID='" + billid + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlstr);
            if (dr.HasRows)
            {
                dr.Read();
                txtHTBH.Text = dr["BR_HTBH"].ToString();
                txtDFBH.Text = dr["BR_DFBH"].ToString();
                txtKPDW.Text = dr["BR_PART"].ToString();
                txtProj.Text = dr["BR_PROJ"].ToString();
                txtEng.Text = dr["BR_ENGNAME"].ToString();
                txtKPJE.Text = dr["BR_KPJE"].ToString();
                txtSL.Text = dr["BR_SL"].ToString();
                txtDW.Text = dr["BR_DANWEI"].ToString();
                txtWeight.Text = dr["BR_WEIGHT"].ToString();
                txtFPDH.Text = dr["BR_FPDH"].ToString();
                txtKPRQ.Text = dr["BR_KPRQ"].ToString();
                txtLPSJ.Text = dr["BR_LPRQ"].ToString();

                txtPZH.Text = dr["BR_PZH"].ToString();

                txtBZ.Text = dr["BR_BZ"].ToString();
                dr.Close();
            }
            //查看发票收据不允许修改
            if (action == "View")
            {
                palFP.Enabled = false;
            }
        }
        /// <summary>
        /// 添加发票记录
        /// </summary>
        private void AddFP()
        {
            //合同编号
            string br_htbh = txtHTBH.Text.Trim();
            //对方合同号
            string br_dfbh = txtDFBH.Text.Trim();
            //开票单位
            string br_part = txtKPDW.Text.Trim();
            string br_proj = txtProj.Text.Trim();
            string br_engname = txtEng.Text.Trim();
            //开票金额
            double br_kpje = txtKPJE.Text.Trim() == "" ? 0 : Convert.ToDouble(txtKPJE.Text.Trim());
            //数量
            int br_sl = txtSL.Text.Trim() == "" ? 0 : Convert.ToInt16(txtSL.Text.Trim());
            //单位
            string br_danwei = txtDW.Text.Trim();
            //重量
            string br_weight = txtWeight.Text.Trim();
            //发票单号
            string br_fpdh = txtFPDH.Text.Trim();
            //开票日期
            string br_kprq = txtKPRQ.Text.Trim();
            string br_lprq = txtLPSJ.Text.Trim();
            //加税合计
            //double br_jshj = txtJSHJ.Text.Trim() == "" ? 0 : Convert.ToDouble(txtJSHJ.Text.Trim());


            //凭证号
            string br_pzh = txtPZH.Text.Trim();

            //备注
            string br_bz = txtBZ.Text.Trim();
            if (action == "Add")
            {
                string sqlstr = string.Format("insert into TBPM_BUSBILLRECORD values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')", br_htbh, br_dfbh, br_part, br_proj, br_engname, br_kpje, br_sl, br_danwei, br_weight, br_fpdh, br_kprq, br_lprq, "", br_pzh, "", br_bz);
                DBCallCommon.ExeSqlText(sqlstr);
                btnConfirm.Visible = false;
                Label1.Visible = true;
            }
            else if (action == "Edit")
            {
                string sqlstr = string.Format("update TBPM_BUSBILLRECORD set BR_HTBH='{0}',BR_DFBH='{1}',BR_PART='{2}',BR_PROJ='{3}',BR_ENGNAME='{4}',BR_KPJE='{5}',BR_SL='{6}',BR_DANWEI='{7}',BR_WEIGHT='{8}',BR_FPDH='{9}',BR_KPRQ='{10}',BR_LPRQ='{11}',BR_PZ='{12}',BR_PZH='{13}',BR_JBR='{14}',BR_BZ='{15}'WHERE BR_ID='{16}'", br_htbh, br_dfbh, br_part, br_proj, br_engname, br_kpje, br_sl, br_danwei, br_weight, br_fpdh, br_kprq, br_lprq, "", br_pzh, "", br_bz, billID);
                DBCallCommon.ExeSqlText(sqlstr);
                btnConfirm.Visible = false;
                Label1.Visible = true;
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {

            if (CheckMustPutIn())
            {
                this.AddFP();
                this.ClientScript.RegisterStartupScript(GetType(), "js", "window.returnValue=\"refresh\";window.close();", true);
            }
        }

        //保存并继续添加
        protected void btn_Add_Click(object sender, EventArgs e)
        {

            if (CheckMustPutIn())
            {
                //合同编号
                string br_htbh = txtHTBH.Text.Trim();
                //对方合同号
                string br_dfbh = txtDFBH.Text.Trim();
                //开票单位
                string br_part = txtKPDW.Text.Trim();
                string br_proj = txtProj.Text.Trim();
                string br_engname = txtEng.Text.Trim();
                //开票金额
                double br_kpje = txtKPJE.Text.Trim() == "" ? 0 : Convert.ToDouble(txtKPJE.Text.Trim());
                //数量
                int br_sl = txtSL.Text.Trim() == "" ? 0 : Convert.ToInt16(txtSL.Text.Trim());
                //单位
                string br_danwei = txtDW.Text.Trim();
                //重量
                string br_weight = txtWeight.Text.Trim();
                //发票单号
                string br_fpdh = txtFPDH.Text.Trim();
                //开票日期
                string br_kprq = txtKPRQ.Text.Trim();
                string br_lprq = txtLPSJ.Text.Trim();
                //加税合计
                //double br_jshj = txtJSHJ.Text.Trim() == "" ? 0 : Convert.ToDouble(txtJSHJ.Text.Trim());


                //凭证号
                string br_pzh = txtPZH.Text.Trim();

                //备注
                string br_bz = txtBZ.Text.Trim();
                string sqlstr = string.Format("insert into TBPM_BUSBILLRECORD values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')", br_htbh, br_dfbh, br_part, br_proj, br_engname, br_kpje, br_sl, br_danwei, br_weight, br_fpdh, br_kprq, br_lprq, "", br_pzh, "", br_bz);
                DBCallCommon.ExeSqlText(sqlstr);
                Random ra = new Random();
                int nouse = 1000 * ra.Next(0, 12);
                string path = "CM_Bill_SW.aspx?Action=Add&condetail_id=" + htbh + "&NoUse =" + nouse;
                Response.Redirect(path);
            }
        }

        //检查必填项
        private bool CheckMustPutIn()
        {
            bool checkresult = true;
            if (txtKPJE.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请输入开票金额！');", true);
                return checkresult = false;
            }

            if (txtKPRQ.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择开票日期！');", true);
                return checkresult = false;
            }

            return checkresult;
        }


    }
}
