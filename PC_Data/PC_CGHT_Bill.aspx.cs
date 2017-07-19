using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_CGHT_Bill : System.Web.UI.Page
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
                 
                    lblFP.Text = "查看发票单据";
                    this.BindData(billID);
                }
                else if (action == "Add")
                {
                    lblFP.Text = "添加发票单据";
                    txtHTBH.Text = htbh;
                  
                    ShowData();
                }
                else
                {
                    lblFP.Text = "修改发票单据";
                    this.BindData(billID);
                
                }

            }
        }

        private void ShowData()
        {
            string sql = "select * from PC_CGHT where HT_XFHTBH='" + htbh + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                txtDFBH.Text = dr["HT_GFHTBH"].ToString();
                txtKPDW.Text = dr["HT_GF"].ToString();
                txtHTBH.Text = htbh;
            }
        }
        //根据合同号绑定发票单据
        private void BindData(string billid)
        {
            string sqlstr = "select * from TBPC_PURBILLRECORD as a left join PC_CGHT as b on a.BR_HTBH=b.HT_XFHTBH where BR_ID='" + billid + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlstr);
            if (dr.HasRows)
            {
                dr.Read();
                txtHTBH.Text = dr["BR_HTBH"].ToString();
                txtDFBH.Text = dr["HT_GFHTBH"].ToString();
                txtKPDW.Text = dr["HT_GF"].ToString();
               
                txtEng.Text = dr["BR_ENGNAME"].ToString();
                txtKPJE.Text = dr["BR_KPJE"].ToString();
                txtSL.Text = dr["BR_SL"].ToString();
             
                txtFPDH.Text = dr["BR_FPDH"].ToString();
                txtKPRQ.Text = dr["BR_KPRQ"].ToString();
                txtLPSJ.Text = dr["BR_LPRQ"].ToString();
            
                txtJBR.Text = dr["BR_JBR"].ToString();
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
      
            string br_engname = txtEng.Text.Trim();
            //开票金额
            double br_kpje = txtKPJE.Text.Trim() == "" ? 0 : Convert.ToDouble(txtKPJE.Text.Trim());
            //数量
            int br_sl = txtSL.Text.Trim() == "" ? 0 : Convert.ToInt16(txtSL.Text.Trim());
         
            //发票单号
            string br_fpdh = txtFPDH.Text.Trim();
            //开票日期
            string br_kprq = txtKPRQ.Text.Trim();
            string br_lprq = txtLPSJ.Text.Trim();
            //加税合计
            //double br_jshj = txtJSHJ.Text.Trim() == "" ? 0 : Convert.ToDouble(txtJSHJ.Text.Trim());

         
            //经办人
            string br_jbr = txtJBR.Text.Trim();
            //备注
            string br_bz = txtBZ.Text.Trim();
            if (action == "Add")
            {
                string sqlstr = string.Format("insert into TBPC_PURBILLRECORD( BR_HTBH, BR_ENGNAME, BR_KPJE, BR_SL, BR_FPDH, BR_KPRQ, BR_LPRQ, BR_JBR, BR_BZ) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", br_htbh, br_engname, br_kpje, br_sl, br_fpdh, br_kprq, br_lprq, br_jbr, br_bz);
                DBCallCommon.ExeSqlText(sqlstr);
                btnConfirm.Visible = false;
                Label1.Visible = true;
            }
            else if (action == "Edit")
            {
                string sqlstr = string.Format("update TBPC_PURBILLRECORD set BR_HTBH='{0}',BR_ENGNAME='{1}',BR_KPJE='{2}',BR_SL='{3}',BR_FPDH='{4}',BR_KPRQ='{5}',BR_LPRQ='{6}',BR_JBR='{7}',BR_BZ='{8}'WHERE BR_ID='{9}'", br_htbh,  br_engname, br_kpje, br_sl, br_fpdh, br_kprq, br_lprq, br_jbr, br_bz, billID);
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

        //检查必填项
        private bool CheckMustPutIn()
        {
            bool checkresult = true;
            if (txtKPJE.Text.Trim() == "" || Convert.ToDouble(txtKPJE.Text.Trim()) <= 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请输入大于0的开票金额！');", true);
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
