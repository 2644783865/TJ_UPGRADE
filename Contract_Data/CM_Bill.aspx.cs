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

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_Bill : System.Web.UI.Page
    {
        string action = "";
        string billID = "";
        string htbh = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            action=Request.QueryString["Action"];
            billID=Request.QueryString["BillID"];
            htbh = Request.QueryString["condetail_id"];
            if (!IsPostBack)
            {
                this.InitPage();
            }
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {           
            if (action == "View")
            {
                btnConfirm.Visible = false;
                lblFP.Text = "查看发票单据";
                this.BindData(billID);
                this.hlSelect.Visible = false;//开票单位选择不可见
                btn_Add.Visible = false;
            }
            else if (action == "Add")
            {
                lblFP.Text = "添加发票单据";
                btnConfirm.Text = "添加并关闭";
                txtHTBH.Text = htbh;
                btn_Add.Visible = true;
                //根据合同编号带出生产制号，开票单位
                string sqltext = "select PCON_ENGID,PCON_CUSTMNAME from TBPM_CONPCHSINFO where PCON_BCODE='" + htbh + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                if (dr.HasRows)
                {
                    dr.Read();
                    txtSCZH.Text = dr["PCON_ENGID"].ToString();
                    txtKPDW.Value = dr["PCON_CUSTMNAME"].ToString();
                    dr.Close();
                }               
            }
            else if (action == "Edit")
            {
                lblFP.Text = "修改发票单据";
                btnConfirm.Text = "修 改";
                txtHTBH.Enabled = false;
                txtSCZH.Enabled = false;
                this.BindData(billID);
                btn_Add.Visible = false;
            }
            if (Session["UserDeptID"].ToString() == "08")
            {
                txtSPRQ.Enabled = true;
                txtSPRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                txtSPRQ.Enabled = false;
            }
        }

        //根据合同号绑定发票单据
        private void BindData(string  billid)
        {
            string sqlstr = "select * from TBPM_GATHINVDOC where GIV_ID='"+billid+"'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlstr);
            if (dr.HasRows)
            {
                dr.Read();
                //合同编号
                txtHTBH.Text = dr["GIV_HTBH"].ToString();
                //生产制号
                txtSCZH.Text = dr["GIV_SCZH"].ToString();
                //经办人
                txtJBR.Text = dr["GIV_JBR"].ToString();
                //开票单位
                txtKPDW.Value = dr["GIV_KPDW"].ToString();
                //开票日期
                txtKPRQ.Text =dr["GIV_KPRQ"].ToString();
                //发票单号
                txtFPDH.Text =dr["GIV_FPDH"].ToString();
                //开票金额
                txtKPJE.Text = dr["GIV_KPJE"].ToString();
                //加税合计
                //txtJSHJ.Text=dr["GIV_JSHJ"].ToString();
                //数量
                txtSL.Text=dr["GIV_SL"].ToString();
                //收票日期
                txtSPRQ.Text =dr["GIV_SPRQ"].ToString();
                //有无凭证
                 rblPZ.SelectedIndex=Convert.ToInt16(dr["GIV_PZ"].ToString());
                //凭证号
                txtPZH.Text = dr["GIV_PZH"].ToString();
                //备注
                txtBZ.Text=dr["GIV_BZ"].ToString();
                dr.Close();
            }
        }
        /// <summary>
        /// 添加发票记录
        /// </summary>
        private void AddFP()
        {
            //合同编号
            string giv_htbh = txtHTBH.Text.Trim();
            //生产制号
            string giv_sczh = txtSCZH.Text.Trim();
            //开票单位
            string giv_kpdw = txtKPDW.Value.Trim();
            //开票日期
            string giv_kprq = txtKPRQ.Text.Trim();
            //发票单号
            string giv_fpdh = txtFPDH.Text.Trim();
            //开票金额
            double giv_kpje =(txtKPJE.Text.Trim()==""?0:Convert.ToDouble(txtKPJE.Text.Trim()));
            //加税合计
            //double giv_jshj =(txtJSHJ.Text.Trim()==""?0:Convert.ToDouble(txtJSHJ.Text.Trim()));
            //数量
            int giv_sl =(txtSL.Text.Trim()==""?1:Convert.ToInt16(txtSL.Text.Trim()));
            //收票日期
            string giv_sprq = txtSPRQ.Text.Trim()!=""?txtSPRQ.Text.Trim():"";
            //有无凭证
            int giv_pz = Convert.ToInt16(rblPZ.SelectedValue.ToString());
            //凭证号
            string giv_pzh = txtPZH.Text.Trim();
            //经办人
            string giv_jbr = txtJBR.Text.Trim();
            //备注
            string giv_bz = txtBZ.Text.Trim();
            string sqlstr ="";
            if (action == "Add")
            {
                sqlstr = "insert into TBPM_GATHINVDOC(GIV_HTBH,GIV_SCZH,GIV_KPDW,GIV_FPDH,GIV_KPJE,GIV_SL,GIV_SPRQ,GIV_PZ,GIV_PZH,GIV_JBR,GIV_BZ,GIV_KPRQ)" +
                "values('" + giv_htbh + "','" + giv_sczh + "','" + giv_kpdw + "','" + giv_fpdh + "'," + giv_kpje + "," + giv_sl + ",'" + giv_sprq + "'," + giv_pz + ",'" + giv_pzh + "','" + giv_jbr + "','" + giv_bz + "','" + giv_kprq + "')";
            }
            else if(action=="Edit")
            {
                sqlstr = "update TBPM_GATHINVDOC set GIV_KPDW='" + giv_kpdw + "',GIV_KPRQ='" + giv_kprq + "',GIV_FPDH='" + giv_fpdh + "',GIV_KPJE=" + giv_kpje + ",GIV_SL=" + giv_sl + ",GIV_SPRQ='" + giv_sprq + "',GIV_PZ=" + giv_pz + ",GIV_PZH='" + giv_pzh + "',GIV_JBR='" + giv_jbr + "',GIV_BZ='" + giv_bz + "'"+
                    " where GIV_ID="+billID+"";
            }
            DBCallCommon.ExeSqlText(sqlstr);
            btnConfirm.Visible = false;
            lblRemind.Visible = true;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtPZH.Text != "" && txtPZH != null)
            { rblPZ.SelectedIndex = 1; }

            if (CheckMustPutIn())
            {
                this.AddFP();
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('操作成功！');window.close();", true);
            }
         
        }

        //保存并继续添加
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            if (txtPZH.Text != "" && txtPZH != null)
            { rblPZ.SelectedIndex = 1; }

            if (CheckMustPutIn())
            {
                //合同编号
                string giv_htbh = txtHTBH.Text.Trim();
                //生产制号
                string giv_sczh = txtSCZH.Text.Trim();
                //开票单位
                string giv_kpdw = txtKPDW.Value.Trim();
                //开票日期
                string giv_kprq = txtKPRQ.Text.Trim();
                //发票单号
                string giv_fpdh = txtFPDH.Text.Trim();
                //开票金额
                double giv_kpje = (txtKPJE.Text.Trim() == "" ? 0 : Convert.ToDouble(txtKPJE.Text.Trim()));
                //加税合计
                //double giv_jshj =(txtJSHJ.Text.Trim()==""?0:Convert.ToDouble(txtJSHJ.Text.Trim()));
                //数量
                int giv_sl = (txtSL.Text.Trim() == "" ? 1 : Convert.ToInt16(txtSL.Text.Trim()));
                //收票日期
                string giv_sprq = txtSPRQ.Text.Trim() != "" ? txtSPRQ.Text.Trim() : "";
                //有无凭证
                int giv_pz = Convert.ToInt16(rblPZ.SelectedValue.ToString());
                //凭证号
                string giv_pzh = txtPZH.Text.Trim();
                //经办人
                string giv_jbr = txtJBR.Text.Trim();
                //备注
                string giv_bz = txtBZ.Text.Trim();
                string sqlstr = "";

                sqlstr = "insert into TBPM_GATHINVDOC(GIV_HTBH,GIV_SCZH,GIV_KPDW,GIV_FPDH,GIV_KPJE,GIV_SL,GIV_SPRQ,GIV_PZ,GIV_PZH,GIV_JBR,GIV_BZ,GIV_KPRQ)" +
                    "values('" + giv_htbh + "','" + giv_sczh + "','" + giv_kpdw + "','" + giv_fpdh + "'," + giv_kpje + "," + giv_sl + ",'" + giv_sprq + "'," + giv_pz + ",'" + giv_pzh + "','" + giv_jbr + "','" + giv_bz + "','" + giv_kprq + "')";
                DBCallCommon.ExeSqlText(sqlstr);
                Random ra = new Random();
                int nouse = 1000 * ra.Next(0, 12);

                string path = "CM_Bill.aspx?Action=Add&condetail_id=" + htbh + "&NoUse =" + nouse;
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

            if (txtKPDW.Value.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请输入开票单位！');", true);
                return checkresult = false;
            }
            return checkresult;
        }

        protected void txtPZH_Changed(object sender, EventArgs e)
        {
            if (txtPZH.Text.Trim() != "")
            {
                rblPZ.SelectedIndex = 1;
            }
        }
    }
}
