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
    public partial class CM_PAYMENT : System.Web.UI.Page
    {
        string pr_id = "";
        string action = "";
        string cr_id = "";        
        
        protected void Page_Load(object sender, EventArgs e)
        {  
            action = Request.QueryString["Action"];
            this.Form.DefaultButton = defaultBtn.UniqueID;
            if (action == "AddFK")
            {
                cr_id = Request.QueryString["CRid"];//请款单号
                lbl_PZ.Visible = true;
            }
            
            if (action == "Edit"||action=="View")
            {
                pr_id = Request.QueryString["PRid"];//添加修改凭证时传支出单号绑定数据
                string sql_CRid = "select PR_QKDH from TBPM_PAYMENTSRECORD where pr_id='"+pr_id+"'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_CRid);
                if (dr.Read())
                {
                    cr_id = dr["PR_QKDH"].ToString();
                    dr.Close();
                }
                lblPRid.Text = pr_id;
            }
            Page.DataBind();
           
            if (!IsPostBack)
            {
                if (action=="AddFK")
                {
                    lblAction.Text = "添加请款记录"; 
                    pr_id=CreatPR_ID();
                    lblPRid.Text = pr_id;
                    hlk_ChangeJE.Visible = false;
                }
                else if (action == "View")
                {
                    lblAction.Text = "查看付款记录—" + pr_id;
                    palFKDetail.Enabled = false; //不可修改
                    btnQRZC.Visible = false;
                    hlk_ChangeJE.Visible = false;
                }
                else if(action=="Edit")
                {
                    lblAction.Text = "修改请款记录";
                    txtBCZFJE.Enabled = false;                    
                    txtZCRQ.Enabled=false;
                }
                this.BindData();
                //if(Math.Round(Convert.ToDecimal(txtQKJE.Text.Trim()))==Math.Round(Convert.ToDecimal(txtBQYZF.Text.Trim())))
                //{
                //    btnQRZC.Enabled=false;
                //}
            }
        }

        //创建支出单号
        private string CreatPR_ID()
        {
            string PR_ID = "";
            string str = "select top 1 PR_ID from TBPM_PAYMENTSRECORD where PR_QKDH='" + cr_id + "' order by PR_ID desc";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(str);
            string lsh = "";
            if (dr.HasRows)
            {
                dr.Read();
                PR_ID = dr["PR_ID"].ToString();
                string[] pr = PR_ID.Split('-');
                lsh = (Convert.ToInt16(pr[pr.Length - 1].ToString()) + 1).ToString();
                dr.Close();
            }
            else
            {
                lsh = "1";
            }
            PR_ID = cr_id + "F" + "-" + lsh;
            return PR_ID;
        }

        public  string CRid
        {
            get
            {
                return cr_id;
            }
            set
            {
                cr_id=value;
            }
        }

        //根据请款单号绑定数据
        private void BindData()
        {            
            #region
            string sql_checkrequest = "select * from TBPM_CHECKREQUEST a inner join TBPM_CONPCHSINFO b on a.CR_HTBH=b.PCON_BCODE and a.CR_ID='" + cr_id + "'";
            SqlDataReader dr_checkrequest = DBCallCommon.GetDRUsingSqlText(sql_checkrequest);
            if (dr_checkrequest.HasRows)
            {
                //绑定请款单数据
                dr_checkrequest.Read();
                //货款名称
                txtXKMC.Text = dr_checkrequest["CR_USE"].ToString();
                //所属项目
                txtSSXM.Text = dr_checkrequest["PCON_PJNAME"].ToString();
                //设备名称
                txtSBMC.Text = dr_checkrequest["PCON_ENGTYPE"].ToString();
                //供货商名称
                txtGHSMC.Text = dr_checkrequest["PCON_CUSTMNAME"].ToString();                

                //合同金额:结算金额为空或者0时以合同金额为准，有结算金额时用结算金额
                string jsje = dr_checkrequest["PCON_BALANCEACNT"].ToString();
                if (jsje == "" || Convert.ToDecimal(jsje) == 0)
                {
                    txtHTJE.Text = dr_checkrequest["PCON_JINE"].ToString();
                }
                else
                {
                    txtHTJE.Text = dr_checkrequest["PCON_BALANCEACNT"].ToString();
                }               
                
                //已支付金额
                txtYZFJE.Text = dr_checkrequest["PCON_YFK"].ToString();
                //请款日期
                txtQKRQ.Text = dr_checkrequest["CR_DATE"].ToString();
                //请款金额
                txtQKJE.Text = dr_checkrequest["CR_BQSFK"].ToString();
                //本期已支付
                txtBQYZF.Text = dr_checkrequest["CR_BQYZF"].ToString();

                if(Convert.ToDouble(dr_checkrequest["CR_BQSFK"].ToString())==Convert.ToDouble(dr_checkrequest["CR_BQYZF"].ToString()))                
                pal_yzf.Visible=true;                
                else
                    pal_yzf.Visible=false;
                //请款人
                txtQKR.Text = dr_checkrequest["CR_JBR"].ToString();
                //合同编号
                lblHTBH.Text = dr_checkrequest["CR_HTBH"].ToString();

                //累积支付金额
                txtLJZFJE.Text = dr_checkrequest["PCON_YFK"].ToString();
                //未支付金额
                txtWFJE.Text = (Convert.ToDouble(txtHTJE.Text.Trim()) - Convert.ToDouble(dr_checkrequest["PCON_YFK"].ToString())).ToString();

                //绑定支出单数据
                if (action == "AddFK")
                {
                    //支出日期
                    txtZCRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    //本次支付金额，添加时默认为请款金额-本期已付款
                    txtBCZFJE.Text = (Convert.ToDouble(txtQKJE.Text.ToString()) - Convert.ToDouble(txtBQYZF.Text.ToString())).ToString();
                    
                }
                if (action == "Edit"||action=="View")
                {
                    string sql_paymentrecord = "select * from TBPM_PAYMENTSRECORD where PR_ID='"+pr_id+"'";
                    SqlDataReader dr_paymentrecord = DBCallCommon.GetDRUsingSqlText(sql_paymentrecord);
                    if (dr_paymentrecord.Read())
                    {
                        //支出日期
                        txtZCRQ.Text = dr_paymentrecord["PR_ZCRQ"].ToString();
                        //本次支付金额
                        txtBCZFJE.Text = dr_paymentrecord["PR_BCZFJE"].ToString();
                       
                        //变更前金额
                        txt_Before_CHG.Text = dr_paymentrecord["PR_BCZFJE"].ToString();

                        //是否有凭证
                        rblPZ.SelectedIndex = dr_paymentrecord["PR_PZH"].ToString()==""?0:1;
                        //凭证号
                        txtPZH.Text = dr_paymentrecord["PR_PZH"].ToString();
                        //备注
                        txtBZ.Text = dr_paymentrecord["PR_NOTE"].ToString();
                    }
                    dr_paymentrecord.Close();
                }

                dr_checkrequest.Close();
            }
            #endregion            
        }

        //更改请款支付状态，同时更新合同信息及请款记录表
        private void ExecSQL()
        {
                List<string> sqlstr = new List<string>();

                //本次支付金额
                double pr_bczfje = txtBCZFJE.Text.ToString() == "" ? 0 : Convert.ToDouble(txtBCZFJE.Text.Trim());
                //合同编号
                string htbh = lblHTBH.Text;
                //支出日期
                string pr_zcrq = txtZCRQ.Text.ToString() == "" ? DateTime.Now.ToString("yyyy-MM-dd") : txtZCRQ.Text.Trim();
                //有无票证号
                int pr_pz = Convert.ToInt16(rblPZ.SelectedValue.ToString());
                //票证号
                string pr_pzh = txtPZH.Text.Trim();
                //备注
                string pr_bz = txtBZ.Text.Trim();

                string str1 = "";

                //检查本次支付金额+本期已支付，如果大于请款金额，不允许提交

                double check_money =Math.Round( Convert.ToDouble(txtBQYZF.Text.Trim()) + Convert.ToDouble(txtBCZFJE.Text.Trim()) - Convert.ToDouble(txtQKJE.Text.Trim()),2);
                if (action == "AddFK")//付款，可添加凭证
                {
                      if (check_money <= 0)
                      {

                        //付款记录表                    
                        str1 = "insert into TBPM_PAYMENTSRECORD(PR_ID,PR_ZCRQ,PR_QKDH,PR_HTBH,PR_BCZFJE,PR_NOTE,PR_PZ,PR_PZH)" +
                                                                       " Values('" + lblPRid.Text + "','" + pr_zcrq + "','" + cr_id + "','" + htbh + "'," + pr_bczfje + ",'" + pr_bz + "'," + pr_pz + ",'" + pr_pzh + "')";

                        //合同信息表中：已付款=已付款+本次支付金额
                        string str2 = "update TBPM_CONPCHSINFO set PCON_YFK=PCON_YFK+" + pr_bczfje + " where PCON_BCODE='" + htbh + "'";//,PCON_YFWK=PCON_YFWK-" + pr_bqzcje + "
                        //请款信息表中：支付状态

                        //修改本期已支付=本期已支付+本次支付金额。如果本期已支付=请款金额，则请款单状态改为4已支付，如果本期已支付<请款金额，则请款单状态改为3部分支付
                        string str3 = "";
                        double check_num =Math.Round( Convert.ToDouble(txtQKJE.Text.ToString()) - Convert.ToDouble(txtBQYZF.Text.ToString()) - Convert.ToDouble(txtBCZFJE.Text.ToString()),2);
                        if (check_num > 0)
                        {
                            str3 = "update TBPM_CHECKREQUEST set CR_STATE=3,CR_BQYZF=CR_BQYZF+" + pr_bczfje + " where CR_ID='" + cr_id + "'";
                        }
                        else
                        {
                            str3 = "update TBPM_CHECKREQUEST set CR_STATE=4,CR_BQYZF=CR_BQYZF+" + pr_bczfje + " where CR_ID='" + cr_id + "'";
                        }
                        sqlstr.Add(str1);
                        sqlstr.Add(str2);
                        sqlstr.Add(str3);

                        //支付完成后向请款人发送邮件（发送前先检查本次支付金额pr_bczfje是否大于0，只有大于0才表示付款）
                        if (Math.Abs( pr_bczfje) > 0)
                        {
                            SendMail();
                        }
                      }

                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('【本次支出金额】+【本期已支付】》【请款金额】\\r请检查再提交');", true);
                        return;
                    }

                }
                else if (action == "Edit")
                {
                    str1 = "update TBPM_PAYMENTSRECORD set PR_NOTE='" + txtBZ.Text.Trim() + "', PR_PZ=" + rblPZ.SelectedValue.ToString() + ",PR_PZH='" + txtPZH.Text.Trim() + "'" +
                       " where PR_ID='" + pr_id + "'";
                    sqlstr.Add(str1);
                }

                DBCallCommon.ExecuteTrans(sqlstr);
                btnQRZC.Visible = false;
                lblRemind.Visible = true;
                this.ClientScript.RegisterStartupScript(GetType(), "js", "window.returnValue=\"refresh\";window.close();", true);
            
              
        }

        protected void btnQRZC_Click(object sender, EventArgs e)
        {
            if (txtPZH.Text != "" && txtPZH != null)
            { rblPZ.SelectedIndex = 1; }
            this.ExecSQL();
        }

        protected void txtBCZFJE_TextChanged(object sender, EventArgs e)
        {
            double check=0;
            check=Math.Round((Convert.ToDouble(txtQKJE.Text.ToString())-Convert.ToDouble(txtBQYZF.Text.ToString())-Convert.ToDouble(txtBCZFJE.Text.ToString())),2);
            if(check<0)
            {
                this.ClientScript.RegisterStartupScript(GetType(), "", "alert('本次支付金额超出本请款单未付金额！\\r\\r请核对！！！');", true);
           
            }
            else
            {
                string str_sql = "select b.PCON_JINE,b.PCON_YFK from TBPM_CHECKREQUEST a inner join TBPM_CONPCHSINFO b on a.CR_HTBH=b.PCON_BCODE and a.CR_ID='" + cr_id + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(str_sql);
                if (dr.Read())
                {
                    //累积支付金额
                    txtLJZFJE.Text = (Convert.ToDouble(dr["PCON_YFK"].ToString()) + Convert.ToDouble(txtBCZFJE.Text.ToString())).ToString();
                    //未支付金额
                    txtWFJE.Text = (Convert.ToDouble(txtQKJE.Text.ToString()) - Convert.ToDouble(dr["PCON_YFK"].ToString()) - Convert.ToDouble(txtBCZFJE.Text.ToString())).ToString();
                    dr.Close();
                }
                
            }
        }
        
         //修改支付金额
        protected void btnJECHG_Click(object sender, EventArgs e)
        {
            decimal bqyzf = Convert.ToDecimal(txtBQYZF.Text.Trim());
            decimal qkje = Convert.ToDecimal(txtQKJE.Text.Trim());
            decimal jeCHG = Convert.ToDecimal(txtJE_CHG.Text.Trim());
            decimal bczfje = Convert.ToDecimal(txtBCZFJE.Text.Trim()) + jeCHG;
            
            List<string> sqlstr = new List<string>();

            if (bczfje <= 0)
            {
                this.ClientScript.RegisterStartupScript(GetType(), "", "alert('本次支出金额必须大于0！\\r\\r请核对！！！');", true); return;
            }
            else
            {
                if (bqyzf + jeCHG <= qkje)
                {
                    //修改支出单中的支出金额
                    string str1 = "update TBPM_PAYMENTSRECORD set PR_BCZFJE=PR_BCZFJE+" + jeCHG + "" +
                              " where PR_ID='" + pr_id + "'";
                    sqlstr.Add(str1);
                    //修改合同信息中的已支付金额
                    string str2 = "update TBPM_CONPCHSINFO set PCON_YFK=PCON_YFK+" + jeCHG + " where PCON_BCODE='" + lblHTBH.Text.ToString() + "'";
                    sqlstr.Add(str2);
                    //修改请款单中本期已支付金额
                    string str3 = "";
                    if (bqyzf + jeCHG < qkje) //请款单未支付完
                    {
                        str3 = "update TBPM_CHECKREQUEST set CR_STATE=3,CR_BQYZF=CR_BQYZF+'" + jeCHG + "' where CR_ID='" + cr_id + "'";

                    }
                    else   //请款单已支付完
                    {
                        str3 = "update TBPM_CHECKREQUEST set CR_STATE=4,CR_BQYZF=CR_BQYZF+'" + jeCHG + "' where CR_ID='" + cr_id + "'";
                    }
                    sqlstr.Add(str3);
                    DBCallCommon.ExecuteTrans(sqlstr);
                    this.BindData();
                }
                else
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "", "alert('本次支付金额超出本请款单未付金额！\\r\\r请核对！！！');", true);
                }
            }
            
        }


        //发送邮件
        private void SendMail()
        {
            string subject="付款通知——"+lblHTBH.Text.ToString();
            string body = "您提交的请款单于"+DateTime.Now.ToString()+"付款：" + txtBCZFJE.Text.Trim() + "元" +
                        "\r\n合同号：" + lblHTBH.Text.ToString() +
                        "\r\n请款期次：第" + cr_id.Substring(cr_id.IndexOf('-') + 1, cr_id.Length - cr_id.IndexOf('-') - 1)+"期"+
                        "\r\n项目名称："+txtSSXM.Text.Trim()+
                        "\r\n收款单位："+txtGHSMC.Text.Trim();
            string sql = "select DISTINCT [EMAIL] from TBDS_STAFFINFO where ST_NAME='" + txtQKR.Text.Trim() + "' and ST_PD='0'";
            
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                DBCallCommon.SendEmail(dt.Rows[0][0].ToString(),null,null,subject,body);
            }
        }
       
    }
}
