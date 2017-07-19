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
    public partial class CM_FHT_Payment : System.Web.UI.Page
    {
        string qkdh = "";
        string action = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["Action"];
            if (action != "Add"&&action!=null)
            {
                qkdh = Request.QueryString["QKDH"];//请款单号 
                lblQKDH.Text = qkdh;
            }
            else if (action == "Add")
            {
                qkdh = Create_qkdh();
                lblQKDH.Text = qkdh;
            }
            if (!IsPostBack)
            {
                this.bindQKBM();
                if (action == "Add")
                {
                    lblAction.Text = "添加非合同请款";                    
                    txtQKRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    txtQKR.Text = Session["UserName"].ToString();
                }
                else if (action == "Edit"||action=="EditCW")
                {
                    lblAction.Text = "编辑非合同请款";                                     
                    this.BindData();
                } 
                else if (action=="View")
                {
                    lblAction.Text = "查看非合同请款" ;                   
                    this.BindData();
                }
                ControlEnabled();                
            }
        }

        //绑定部门
        private void bindQKBM()
        {
            string sqltext = "select DEP_NAME,DEP_CODE from  TBDS_DEPINFO where DEP_FATHERID='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ddl_qkbm.DataSource = dt;
            ddl_qkbm.DataTextField = "DEP_NAME";
            ddl_qkbm.DataValueField = "DEP_CODE";
            ddl_qkbm.DataBind();
            ddl_qkbm.Items.Insert(0, new ListItem("-请选择-", "%"));
            ddl_qkbm.SelectedIndex = 0;
        }
        //创建支出单号
        private string Create_qkdh()
        {
            string CR_QKDH = "";
            string str_time = DateTime.Now.ToString("yyyy.MM.dd");
            string str = "select top 1 CR_QKDH from TBPM_FHT_CheckRequest where CR_QKDH like '%" + str_time + "%' order by CR_QKDH desc";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(str);
            string lsh = "";
            string PR_ID = "";
            if (dr.HasRows)
            {
                dr.Read();
                PR_ID = dr["CR_QKDH"].ToString();
                string[] pr = PR_ID.Split('-');
                lsh = (Convert.ToInt16(pr[pr.Length - 1].ToString()) + 1).ToString();
                dr.Close();
            }
            else
            {
                lsh = "1";
            }

            CR_QKDH ="ZCZJ.FHTQK."+ str_time + "-"+lsh ;
            return CR_QKDH;
        }

        //根据请款单号绑定数据
        private void BindData()
        {
            string sql = "select * from TBPM_FHT_CheckRequest where CR_QKDH='"+lblQKDH.Text.ToString()+"'";
            SqlDataReader dr=DBCallCommon.GetDRUsingSqlText(sql);
            if(dr.Read())
            {
                //支付状态
                if (action == "View"||action=="EditCW")
                {
                    rblState.Items.Clear();
                    rblState.Items.Add(new ListItem("保存", "0"));
                    rblState.Items.Add(new ListItem("正在签字", "1"));
                    rblState.Items.Add(new ListItem("提交财务-未付款", "2"));
                    rblState.Items.Add(new ListItem("部分支付", "3"));
                    rblState.Items.Add(new ListItem("已付款", "4"));                    
                    rblState.Enabled = false;                    
                }
                if (action == "Edit"&&Session["UserDeptID"].ToString()=="08")
                {
                    rblState.Items.Clear();
                    rblState.Items.Add(new ListItem("正在签字", "1"));
                    rblState.Items.Add(new ListItem("提交财务-未付款", "2"));                   
                }
                rblState.SelectedValue = dr["CR_STATE"].ToString();
                //请款部门
                ddl_qkbm.SelectedValue=dr["CR_QKBM"].ToString();
                //请款用途
                txtCR_USE.Text=dr["CR_USE"].ToString();
                //请款金额
                txtQKJE.Text=dr["CR_QKJE"].ToString();
                //请款人
                txtQKR.Text=dr["CR_PERSON"].ToString();
                //请款日期
                txtQKRQ.Text=dr["CR_QKRQ"].ToString();
                //本次支付金额
                txtZFJE.Text=(Convert.ToDouble( dr["CR_QKJE"].ToString())-Convert.ToDouble( dr["CR_ZFJE"].ToString())).ToString();
                //支付日期
                txtZFRQ.Text=dr["CR_ZFRQ"].ToString();
                if (action == "EditCW")
                {
                    txtZFRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                //已支付金额
                txtYZF.Text = dr["CR_ZFJE"].ToString();
                //有无凭证
                rblPZ.SelectedValue=dr["CR_PZ"].ToString();
                //凭证号
                txtPZH.Text=dr["CR_PZH"].ToString();
                //备注
                txtBZ.Text=dr["CR_NOTE"].ToString();
                //收款单位
                txtSKDW.Text = dr["CR_SKDW"].ToString();
                
                dr.Close();
            }
 
        }

        protected void btnQRZC_Click(object sender, EventArgs e)
        {
            string cr_qkdh=lblQKDH.Text.ToString();
            
            string cr_qkbm=ddl_qkbm.SelectedValue.ToString(); // 请款部门
            string cr_use=txtCR_USE.Text.ToString(); //请款用途
            double cr_qkje=txtQKJE.Text.Trim()==""?0: Convert.ToDouble(txtQKJE.Text.ToString());//请款金额
            string cr_person=txtQKR.Text.ToString();//请款人
            string cr_qkrq=txtQKRQ.Text.ToString();//请款日期
            double cr_zfje = txtZFJE.Text.Trim() == "" ? 0 : Convert.ToDouble(txtZFJE.Text.ToString());// 本次支付金额
            string cr_zfrq=txtZFRQ.Text.ToString();//支付日期
            double cr_yzf = txtYZF.Text.Trim() == "" ? 0 : Convert.ToDouble(txtYZF.Text.ToString());//已支付金额
            string cr_pz=rblPZ.SelectedValue.ToString();//是否有凭证
            string cr_pzh=txtPZH.Text.ToString();//凭证号
            string cr_note=txtBZ.Text.ToString();//备注
            string cr_skdw = txtSKDW.Text.ToString();//收款单位

            string cr_state = rblState.SelectedValue.ToString();//请款状态

            List<string> sqlstr = new List<string>();            
            if(action=="Add")  //添加 
            {
                if (Check_InPut())
                {
                    cr_qkdh = Create_qkdh();//添加时再检查单号，避免多人同时添加时单号重复                    
                    string sql_insert = "insert into TBPM_FHT_CheckRequest (CR_QKDH,CR_STATE,CR_QKBM,CR_USE,CR_QKJE,CR_PERSON,CR_QKRQ,CR_NOTE,CR_SKDW)" +
                                        "values('" + cr_qkdh + "','" + cr_state + "','" + cr_qkbm + "','" + cr_use + "','" + cr_qkje + "','" + cr_person + "','" + cr_qkrq + "','" + cr_note + "','" + cr_skdw + "')";
                    sqlstr.Add(sql_insert);
                }
                else
                {
                    return;
                }

            }
            else if(action=="Edit") //请款人编辑
            {
                string sql_update="update TBPM_FHT_CheckRequest set CR_STATE='"+cr_state+"',CR_QKBM='"+cr_qkbm+"',CR_USE='"+cr_use+"',CR_QKJE="+cr_qkje+","+
                                   "CR_PERSON='" + cr_person + "',CR_QKRQ='" + cr_qkrq + "',CR_NOTE='" + cr_note + "',CR_SKDW='" + cr_skdw + "' where CR_QKDH='" + cr_qkdh + "'";
                sqlstr.Add(sql_update);
            }
            else if (action == "EditCW") //财务付款时编辑
            {
                //凭证号
                if (txtPZH.Text != "" && txtPZH != null)
                { 
                    rblPZ.SelectedIndex = 1; 
                }
                //付款时根据付款金额自动更改请款状态
                double checkFK = cr_qkje - cr_zfje - cr_yzf;
                if (checkFK > 0&&checkFK<cr_qkje)//部分支付
                {
                    cr_state = "3";
                }
                else if (checkFK == 0)
                {
                    cr_state = "4";
                }
                else if (checkFK < 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('付款金额大于请款金额，请检查');", true); return; 
                }

                //在备注中记录付款情况
                if (cr_note != "")
                    cr_note += "\r\n";
                    cr_note +=txtZFRQ.Text.ToString() + "支付金额" + txtZFJE.Text.ToString() + ";";
                string sql_updatecw = "update TBPM_FHT_CheckRequest set CR_STATE='" + cr_state + "',CR_ZFJE=CR_ZFJE+" + cr_zfje + ",CR_ZFRQ='" + cr_zfrq + "'" +
                                      ",CR_PZ='" + cr_pz + "',CR_PZH='" + cr_pzh + "',CR_NOTE='" + cr_note + "',CR_SKDW='" + cr_skdw + "' where CR_QKDH='" + cr_qkdh + "'";
                sqlstr.Add(sql_updatecw);
                SendMail();

            }
            DBCallCommon.ExecuteTrans(sqlstr);
            //lblRemind.Visible=true;
            //this.ClientScript.RegisterStartupScript(GetType(), "js", "window.returnValue=\"refresh\";setTimeout(\"AutoClose()\",1000);", true);
            this.ClientScript.RegisterStartupScript(GetType(), "js", "window.returnValue=\"refresh\";window.close();", true);
        
        }

        protected void txtZFJE_TextChanged(object sender, EventArgs e)
        {
            double check = 0;
            string sql = "select * from TBPM_FHT_CheckRequest where CR_QKDH='" + lblQKDH.Text.ToString() + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                check = Convert.ToDouble(txtQKJE.Text.Trim()) - Convert.ToDouble(txtZFJE.Text.Trim()) - Convert.ToDouble(dr["CR_ZFJE"].ToString());
                if (check < 0)
                {
                    this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('支付总金额大于请款总金额！\\r\\r请检查！！！');", true);
                }               
              dr.Close();
            }
        }

        //控制控件是否可用
        private void ControlEnabled()
        {
            if (action == "Add" || action == "Edit")
            {
                txtZFJE.Enabled = false;
                txtZFRQ.Enabled = false;
                rblPZ.Enabled = false;
                txtPZH.Enabled = false;
            }            
            else if (action == "EditCW")
            {
                ddl_qkbm.Enabled = false;
                txtCR_USE.Enabled = false;
                txtQKJE.Enabled = false;
                txtQKRQ.Enabled = false;
            }
            else if (action == "View")
            {
                palFKDetail.Enabled = false;
                btnQRZC.Visible = false;
            }
        }

        //检查必填项
        private bool Check_InPut()
        {
            bool check = true;

            //请款部门ddl_qkbm
            if(ddl_qkbm.SelectedIndex==0)
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择请款部门！');", true); return check;
            }
            //请款人txtQKR
            if (txtQKR.Text.Trim()=="")
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请款人不能为空！');", true); return check;
            }
            //请款日期txtQKRQ
            if (txtQKRQ.Text.Trim()=="")
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请款日期不能为空！');", true); return check;
            }
            //请款用途txtCR_USE
            if (txtCR_USE.Text.Trim() == "")
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请款用途不能为空！');", true); return check;
            }
            //请款金额txtQKJE
            if (txtQKJE.Text.Trim() == "0" || txtQKJE.Text.Trim() == null || txtQKJE.Text.Trim() == "")
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写请款金额！');", true); return check;
            }
            //收款单位txtSKDW
            if (txtSKDW.Text.Trim() == "")
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('收款单位不能为空！');", true); return check;
            }
            return check;
        }

        //发送邮件
        private void SendMail()
        {
            string subject = "非合同请款付款通知";
            string body = "您提交的请款单于" + DateTime.Now.ToString() + "付款：" + txtZFJE.Text.Trim() + "元" +
                        "\r\n请款日期：" + txtQKRQ.Text.ToString() +
                        "\r\n请款用途：" + txtCR_USE.Text.Trim() +
                        "\r\n收款单位：" + txtSKDW.Text.Trim();
            string sql = "select DISTINCT [EMAIL] from TBDS_STAFFINFO where ST_NAME='" + txtQKR.Text.Trim() + "' and ST_PD='0'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                DBCallCommon.SendEmail(dt.Rows[0][0].ToString(), null, null, subject, body);
            }
        }
    }
}
