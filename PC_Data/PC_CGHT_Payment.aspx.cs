using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_CGHT_Payment : System.Web.UI.Page
    {
        string contractID = "";//合同编号
        string action = "";//添加 修改
        string bp_id = "";//要款单号
        protected void Page_Load(object sender, EventArgs e)
        {
            contractID = Request.QueryString["condetail_id"];
            action = Request.QueryString["Action"];
            bp_id = Request.QueryString["BPid"];
            if (!IsPostBack)
            {
                if (action == "Add")
                {
                    lblAction.Text = "添加要款信息";
                    lblYKDH.Text = this.CreateBPID(contractID);
                    txtHTBH.Text = contractID;
                    txtYKRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");

                }
                else if (action == "Edit")
                {
                    lblAction.Text = "确认要款信息";
                    this.BindBPData(bp_id);
                    //商务组信息不可修改
                    lblKXMC.Enabled = false;
                    //txtJE.Enabled = false;
                    //txtYKRQ.Enabled = false;
                    //dplSKFS.Enabled = false;
                    //txtSWZ.Enabled = false;
                    //**********************                   
                }
                else if (action == "View")
                {
                    //商务组操作
                    //在财务未确认前，商务组可修改
                    //根据要款单号绑定数据                    
                    this.BindBPData(bp_id);
                    lblAction.Text = "查看要款信息";

                    btnConfirm.Visible = false;
                    lblState.Visible = false;
                    //合同编号
                    txtHTBH.Enabled = false;
                    //款项名称
                    lblKXMC.Enabled = false;
                    //要款日期
                    txtYKRQ.Enabled = false;

                    //要款金额
                    txtJE.Enabled = false;

                    //收款方式
                    dplSKFS.Enabled = false; ;

                    //备注1-商务组
                    txtSWZ.Enabled = false;
                    //备注2-财务部
                    txtCWB.Enabled = false;

                }
            }
        }


        /// <summary>
        /// 创建要款单号
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string CreateBPID(string ID)
        {
            string id = "";
            string sqlstr = "select top 1 BP_ID FROM TBPC_PURPAYMENTRECORD where BP_ID like '" + contractID + "%' ORDER BY ID desc";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlstr);
            if (dr.HasRows)
            {
                dr.Read();
                string[] a = dr["BP_ID"].ToString().Split('-');
                id = ID + ".FK-" + (Convert.ToInt16(a[a.Length - 1].ToString()) + 1).ToString();
            }
            else
            {
                id = ID + ".FK-1";
            }
            return id;
        }


        /// <summary>
        /// 根据要款单号绑定数据
        /// </summary>
        /// <param name="bpid"></param>
        private void BindBPData(string bpid)
        {
            string sqlstr = "select a.* from TBPC_PURPAYMENTRECORD a where BP_ID='" + bpid + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlstr);
            if (dr.HasRows)
            {
                dr.Read();
                //要款单号
                lblYKDH.Text = dr["BP_ID"].ToString();

                //合同编号
                txtHTBH.Text = dr["BP_HTBH"].ToString();
                //款项名称
                lblKXMC.SelectedValue = dr["BP_KXMC"].ToString();
                //要款日期
                txtYKRQ.Text = Convert.ToDateTime(dr["BP_YKRQ"].ToString()).ToString("yyyy-MM-dd");

                //要款金额
                txtJE.Text = dr["BP_JE"].ToString();
                hidJE.Value = dr["BP_JE"].ToString();
                //收款方式
                dplSKFS.SelectedIndex = Convert.ToInt16(dr["BP_SKFS"].ToString()) + 1;

                //支付比例
                txtSWZ.Text = dr["BP_ZFBL"].ToString();
                //备注
                txtCWB.Text = dr["BP_NOTE"].ToString();

                txtSCBHTH.Text = dr["BP_SCBHTH"].ToString();
                txtShebei.Text = dr["BP_SHEBEI"].ToString();
                dr.Close();
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {

            if (this.ExecSql())
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "window.returnValue=\"refresh\";window.close();", true);
            }
        }

        private bool CheckInput()
        {
            bool YesNO = true;
            if (dplSKFS.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择支付方式！');", true);
                return YesNO = false;
            }
            if (Convert.ToDouble(txtJE.Text.Trim()) == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写付款金额！');", true);
                return YesNO = false;
            }

            if (txtYKRQ.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写付款日期！');", true);
                return YesNO = false;
            }


            return YesNO;
        }

        private bool ExecSql()
        {
            bool state = false;
            if (CheckInput())
            {

                //要款单号
                string bp_id = lblYKDH.Text.Trim();
                //合同编号
                string bp_htbh = txtHTBH.Text.Trim();
                //款项名称
                string bp_kxmc = lblKXMC.SelectedValue;
                //要款日期
                string bp_ykrq = txtYKRQ.Text.Trim();

                //要款金额
                double bp_je = Convert.ToDouble(txtJE.Text.Trim());

                //收款方式
                int bp_skfs = Convert.ToInt16(dplSKFS.SelectedItem.Value.ToString());

                //支付比例
                string bp_notefst = txtSWZ.Text.Trim();
                //备注
                string bp_notesnd = txtCWB.Text.Trim();

                string scbHTH = txtSCBHTH.Text.Trim();
                string shebei = txtShebei.Text.Trim();

                List<string> sqlstr = new List<string>();
                string sql = "";


                if (action == "Add")
                {


                    string sqlstr1 = "insert into TBPC_PURPAYMENTRECORD(BP_ID,BP_HTBH,BP_KXMC,BP_YKRQ,BP_JE,BP_SKFS,BP_NOTE,BP_ZFBL,BP_SCBHTH,BP_SHEBEI)" +
                        "VALUES('" + bp_id + "','" + bp_htbh + "','" + bp_kxmc + "','" + bp_ykrq + "'," + bp_je + "," + bp_skfs + ",'" + bp_notesnd + "','" + bp_notefst + "','" + scbHTH + "','" + shebei + "')";
                    sqlstr.Add(sqlstr1);
                    DBCallCommon.ExecuteTrans(sqlstr);

                }
                else if (action == "Edit")
                {
                    //确认支付后，修改合同信息表中的已付款、已付款累计、实付款累计和已付尾款。
                    //修改前判断金额是否发生变化，如果实收金额发生变化，则合同信念表中相应金额改变
                    string sqlstr2 = "update TBPC_PURPAYMENTRECORD set BP_NOTE='" + bp_notesnd + "',BP_JE='" + bp_je + "',BP_ZFBL='" + bp_notefst + "',BP_YKRQ='" + bp_ykrq + "',BP_SCBHTH='" + scbHTH + "',BP_SHEBEI='" + shebei + "',BP_SKFS='" + bp_skfs + "' where BP_ID='" + bp_id + "'";
                    DBCallCommon.ExeSqlText(sqlstr2);


                  
                }
                sql = "select sum(BP_JE) from TBPC_PURPAYMENTRECORD where BP_HTBH='" + bp_htbh + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                string yfkje = dt.Rows[0][0].ToString();
                sql = "update PC_CGHT set HT_DKJE='" + yfkje + "' where HT_XFHTBH='" + bp_htbh + "'";
                DBCallCommon.ExeSqlText(sql);
                //操作成功
                btnConfirm.Visible = false;
                lblState.Visible = true;
                state = true;
            }
            return state;
        }
    }
}
