using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.UI.MobileControls;
using System.Collections.Generic;

/****相关数据库：  TBPM_ORDER_CHECKREQUEST ****/
/****MengQingTong  2013年1月10日 11:23:09  ****/

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CR_NotContractOrder_Add : System.Web.UI.Page
    {
        string DDQKCode="";    //采购请款单号
        string DDCode = "";    //订单号
        string DDJE = "";      //已选订单总金额
        string Action = "";    //编辑查看状态
        string DDCSCode = "";  //供货商ID
        string BILLCODE = "";  //发票号码

        protected void Page_Load(object sender, EventArgs e)
        {
            /******获取请求状态********/
            Action = Request.QueryString["Action"];     
            DDCode = Request.QueryString["orderid"];    //订单号
            DDJE = Request.QueryString["ZJE"];          //总金额
            DDCSCode = Request.QueryString["csinfo"];   //厂商编号
            BILLCODE = Request.QueryString["bill"];
            

            if (!IsPostBack)
            {
                BindDep();
                if (Action == "add")
                {
                    
                    GetCSInfo();                                //获取厂商信息
                    lblDDCR.Text = "--->新建采购订单请款";  //标题
                    DDQKCode = Encode();                   //生成请款单号
                    rblDDState.SelectedIndex = 0;          //状态初始化为0
                    dplDDQKBM.SelectedValue = Session["UserDeptID"].ToString(); //请款部门为sessionID所在部门
                    txtDDQKRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");       //请款时间
                    btnDDPrint.Visible = false;                      //打印按钮不可见
                    lblDDCR_ID.Text = DDQKCode.ToString();           //生成请款单号
                    txtDDQKR.Text = Session["UserName"].ToString();  //请款人为sessionID
                    txtDDCode.Text = DDCode.ToString();              //订单号               
                    txtDDZJE.Text = DDJE.ToString();                 //采购订单总金额
                    txtDDCSCode.Text = DDCSCode.ToString();
                    //txtDDPZH.Text = BILLCODE.ToString();     
                }
                else if (Action == "Badd")
                {
                    GetCSInfo();                                //获取厂商信息
                    lblDDCR.Text = "--->新建采购订单请款";  //标题
                    DDQKCode = Encode();                   //生成请款单号
                    rblDDState.SelectedIndex = 0;          //状态初始化为0
                    dplDDQKBM.SelectedValue = Session["UserDeptID"].ToString(); //请款部门为sessionID所在部门
                    txtDDQKRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");       //请款时间
                    btnDDPrint.Visible = false;                      //打印按钮不可见
                    lblDDCR_ID.Text = DDQKCode.ToString();           //生成请款单号
                    txtDDQKR.Text = Session["UserName"].ToString();  //请款人为sessionID
                    txtDDCode.Text = DDCode.ToString();              //订单号               
                    txtDDZJE.Text = DDJE.ToString();                 //采购订单总金额
                    txtDDCSCode.Text = DDCSCode.ToString();
                    txtDDPZH.Text = BILLCODE.ToString();     
                }

                else if (Action == "Edit" || Action == "EditCW")
                {
                    Lbtn_ViewOrder.Visible = true;
                    Lbtn_ViewBill.Visible = true;
                    dplDDQKBM.Enabled = false;
                    txtDDQKR.Enabled = false;
                    DDQKCode = Request.QueryString["ddqkcode"]; //获取采购请款单号
                    lblDDCR.Text = "--->编辑采购订单请款单";     //标题
                    btnDDSave.Text = "修改";
                    this.BindDataEdit(DDQKCode);                //通过请款单号绑定修改数据
                    if (rblDDState.SelectedValue.ToString() == "2")
                    {
                        palDDQK.Enabled = false;
                    }
                }

                else if (Action == "View")
                {
                    Lbtn_ViewOrder.Visible = true;
                    Lbtn_ViewBill.Visible = true;
                    DDQKCode = Request.QueryString["ddqkcode"]; //获取采购请款单号
                    lblDDCR.Text = "--->查看采购订单请款";
                    this.BindDataEdit(DDQKCode);
                    this.ControlEnable();
                }
            }
            lblDDCR_ID.DataBind();
        }

        private void ControlEnable()
        {
            rblDDState.Enabled = false;
            btnDDSave.Visible = false;
            lblDDCR_ID.Enabled = false;
            dplDDQKBM.Enabled = false;
            txtDDQKR.Enabled = false;
            txtDDQKRQ.Enabled = false;
            txtDDQKSY.Enabled = false;
            txtDDCode.Enabled = false;
            txtDDZJE.Enabled = false;
            txtDDCSCode.Enabled = false;
            txtDDCSName.Enabled = false;
            txtDDCSBank.Enabled = false;
            txtDDCSAccount.Enabled = false;
            rblDDZFFS.Enabled = false;
            txtDDFKJE.Enabled = false;
            txtDDFKJEDX.Enabled = false;
            txtDDPZH.Enabled = false;
            txtNote.Enabled = false;
            txtDDBMYJ.Enabled = false;
            txtDDZGLD.Enabled = false;
            txtDDLD.Enabled = false;
            txtDDCWSH.Enabled = false;

            Lbtn_ViewOrder.Visible = true;          
        }

        //绑定部门 2013年1月10日 09:09:11
        private void BindDep()
        {
            string sqltext = "select DEP_NAME,DEP_CODE from  TBDS_DEPINFO where DEP_FATHERID='0'and DEP_CODE!='01'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            dplDDQKBM.DataSource = dt;
            dplDDQKBM.DataTextField = "DEP_NAME";
            dplDDQKBM.DataValueField = "DEP_CODE";
            dplDDQKBM.DataBind();
            dplDDQKBM.Items.Insert(0, new ListItem("-请选择-", "%"));
            dplDDQKBM.SelectedIndex = 0;
        }

        
        //获得厂商信息  2013年1月10日 09:09:21
        private void GetCSInfo()
        {
            string sql = "SELECT CS_NAME,CS_BANK,CS_ACCOUNT from TBCS_CUSUPINFO where CS_CODE='" + DDCSCode + "'";
            DataTable cs = DBCallCommon.GetDTUsingSqlText(sql);
            txtDDCSName.Text = cs.Rows[0]["CS_NAME"].ToString();
            txtDDCSBank.Text = cs.Rows[0]["CS_BANK"].ToString();
            txtDDCSAccount.Text = cs.Rows[0]["CS_ACCOUNT"].ToString();
        }

        
        //生成请款单号  2013年1月10日 09:10:15
        //格式为：ZCZJ.CG.DDQK.2013.01.0001
        private string Encode()
        {
            string DQ_ID = "";
            string str_time = DateTime.Now.ToString("yyyy.MM");
            string str = "select top 1 DQ_ID from TBPM_Order_CheckRequest where DQ_ID like '%" + str_time + "%' order by DQ_ID desc";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(str);
            string lsh = "";
            string ID = "";
            if (dr.HasRows)
            {
                dr.Read();
                ID = dr["DQ_ID"].ToString();
                string[] pr = ID.Split('-');
                lsh = (Convert.ToInt16(pr[pr.Length - 1].ToString()) + 1).ToString();
                lsh = lsh.PadLeft(4, '0');
                dr.Close();
            }
            else
            {
                lsh = "0001";
            }

            DQ_ID = "ZCZJ.CG.DDQK." + str_time + "-" + lsh;
            return DQ_ID;
        }
        
        
        //财务驳回后请款单作废
        protected void rblDDState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblDDState.SelectedValue == "4")
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "javascript:alert('驳回后该请款单将作废！');", true);
            }
        }

        
        //保存、修改按钮事件  2013年1月10日 09:56:11
        protected void btnDDSave_Click(object sender, EventArgs e)
        {
            //首先对供应商进行判断，如果不是同一个供应商，则不能修改
            string orderid = "";
            string orders = txtDDCode.Text;
            string[] order = orders.Split(',');
            for (int i = 0; i < order.Length; i++)
            {
                orderid += "'" + order[i] + "',";
            }
            orderid = orderid.Substring(0, orderid.Length - 1);
            string sql_checkSupplier = "select  DISTINCT (SUPPLIERID) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL WHERE ORDERNO IN (" + orderid + ")";
            System.Data.DataTable DT = DBCallCommon.GetDTUsingSqlText(sql_checkSupplier);
            if (DT.Rows.Count > 1)  //选择的订单包括多个供应商，不能添加
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择同一供应商的订单！');", true);
                return;
            }
            //请款部门必须选择
            if (dplDDQKBM.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择请款部门！');", true);
                return;
            }
            if (txtDDPZH.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写发票号！');", true);
                return;
            }
            else
            {
                this.ExecuteSQL();
                if (Action == "add" || Action == "Badd" || Action == "Edit" || Action == "EditCW")
                {
                    Response.Write("<script>window.opener.location.reload();</script>");//刷新
                }
            }
        }

        
        //显示请款单信息
        private void BindDataEdit(string DDQKCode)
        {
            string sqlstr = "select DQ_ID,DQ_DATA,DQ_QKBM,DQ_QKR,DQ_USE,DQ_DDCODE,DQ_DDZJE,DQ_CSCODE,DQ_CSNAME,DQ_CSBANK,DQ_CSACCOUNT,DQ_AMOUNT,DQ_AMOUNTDX,DQ_ZFFS,DQ_BILLCODE,DQ_STATE,DQ_BMYJ,DQ_ZGLD,DQ_GSLD,DQ_CWSH,DQ_NOTE from TBPM_ORDER_CHECKREQUEST where  DQ_ID='" + DDQKCode + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlstr);
            if (dr.HasRows)
            {
                dr.Read();
                
                //采购订单请款单号
                lblDDCR_ID.Text = dr["DQ_ID"].ToString();
                //请款日期
                txtDDQKRQ.Text = Convert.ToDateTime(dr["DQ_DATA"].ToString()).ToShortDateString();
                //请款部门
                dplDDQKBM.ClearSelection();
                    foreach (ListItem li in dplDDQKBM.Items)
                    {
                        if (li.Value.ToString() == dr["DQ_QKBM"].ToString())
                        {
                            li.Selected = true; break;
                        }
                    }

                //请款人
                txtDDQKR.Text = dr["DQ_QKR"].ToString();
                //请款事由
                txtDDQKSY.Text = dr["DQ_USE"].ToString();
                //采购订单号
                txtDDCode.Text = dr["DQ_DDCODE"].ToString();
                //订单总金额
                txtDDZJE.Text = dr["DQ_DDZJE"].ToString();
                //厂商编号
                txtDDCSCode.Text = dr["DQ_CSCode"].ToString();
                //厂商名称
                txtDDCSName.Text = dr["DQ_CSNAME"].ToString();
                //开户银行
                txtDDCSBank.Text = dr["DQ_CSBANK"].ToString();
                //银行账号
                txtDDCSAccount.Text = dr["DQ_CSACCOUNT"].ToString();
                //付款金额(小写)
                txtDDFKJE.Text = dr["DQ_AMOUNT"].ToString();
                //付款金额(大写)
                txtDDFKJEDX.Text = dr["DQ_AMOUNTDX"].ToString();
                
                //支付方式
                //rblDDZFFS.ClearSelection();
                foreach (ListItem li in rblDDZFFS.Items)
                {
                    if (dr["DQ_ZFFS"].ToString() == li.Text.ToString().Trim())
                    {
                        li.Selected = true; break;
                    }
                }
                //票证号
                txtDDPZH.Text = dr["DQ_BillCode"].ToString();

                //部门意见
                txtDDBMYJ.Text = dr["DQ_BMYJ"].ToString();
                //主管领导
                txtDDZGLD.Text = dr["DQ_ZGLD"].ToString();
                //公司领导
                txtDDLD.Text = dr["DQ_GSLD"].ToString();
                //财务审核
                txtDDCWSH.Text = dr["DQ_CWSH"].ToString();

                //备注
                txtNote.Text = dr["DQ_NOTE"].ToString();
                
                if (Action == "View")
                {
                    rblDDState.Items.Clear();
                    rblDDState.Items.Add(new ListItem("保存","0"));
                    rblDDState.Items.Add(new ListItem("正在签字", "1"));
                    rblDDState.Items.Add(new ListItem("提交财务--未付款", "2"));
                    rblDDState.Items.Add(new ListItem("提交财务--已付款", "3"));
                }

                if (Action == "EditCW")
                {
                    rblDDState.Items.Add(new ListItem("提交财务--未付款", "2"));
                    rblDDState.Items.Add(new ListItem("提交财务--已付款", "3"));
                }

                //在编辑状态，如果正在签字，无法修改状态
                if (Action == "Edit" && dr["DQ_STATE"].ToString() == "1")
                {
                    rblDDState.Enabled = false;
                }
                foreach (ListItem li in rblDDState.Items)
                {
                    if (dr["DQ_STATE"].ToString() == li.Value.ToString())
                    {
                        li.Selected = true; break;
                    }
                }
                dr.Close();
                double FKJE = Convert.ToDouble(txtDDFKJE.Text);  //付款金额
            }
            
        }

        
        //绑定数据，执行数据库操作   2013年1月10日 09:56:29
        private void ExecuteSQL()
        {
            #region  获取信息
            List<string> sqltext = new List<string>();
            //请款单号
            string dq_id = lblDDCR_ID.Text;
            //请款时间
            string dq_data = txtDDQKRQ.Text.Trim() == "" ? DateTime.Now.ToShortDateString() : txtDDQKRQ.Text.Trim();
            //请款部门
            string dq_qkbm = dplDDQKBM.SelectedValue.ToString();

            #region 获得部门名称
            string bn = "select DEP_NAME from TBDS_DEPINFO where DEP_CODE='" + dq_qkbm + "'";
            DataTable bndt = DBCallCommon.GetDTUsingSqlText(bn);
            string dq_bmname = bndt.Rows[0][0].ToString();
            #endregion
            
            //请款人
            string dq_qkr = txtDDQKR.Text.Trim();
            //请款事由
            string dq_use = txtDDQKSY.Text;
            //采购订单号
            string dq_ddcode = txtDDCode.Text.Trim();
            //采购订单总金额
            string dq_ddzje = txtDDZJE.Text.Trim();
            //厂商编码
            string dq_cscode = txtDDCSCode.Text.Trim();
            //厂商名
            string dq_csname = txtDDCSName.Text.Trim();
            //厂商开户银行
            string dq_csbank = txtDDCSBank.Text.Trim();
            //厂商银行账号
            string dq_csaccount = txtDDCSAccount.Text.Trim();

            //应付金额(小写)
            //string dq_amount = txtDDFKJE.Text.Trim();
            decimal dq_amount = txtDDFKJE.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtDDFKJE.Text.Trim());
            //应付金额(大写)
            string dq_amountdx = txtDDFKJEDX.Text.Trim();
            //支付方式
            string dq_zffs = rblDDZFFS.SelectedItem.Text.Trim();
            //票证号
            string dq_billcode = txtDDPZH.Text.Trim();

            //备注
            string dq_note=txtNote.Text.Trim();

            //部门意见
            string dq_bmyj = txtDDBMYJ.Text.Trim();
            //主管领导
            string dq_zgld = txtDDZGLD.Text.Trim();
            //公司领导
            string dq_gsld = txtDDLD.Text.Trim();
            //财务审核
            string dq_cwsh = txtDDCWSH.Text.Trim();


            //请款状态
            int dq_state = Convert.ToInt16(rblDDState.SelectedValue.ToString());

            #endregion

            if (Action == "add" || Action == "Badd")
            {
                //添加采购订单请款单 
                string str1 = "insert into TBPM_ORDER_CHECKREQUEST(DQ_ID,DQ_DATA,DQ_QKBM,DQ_BMNAME,DQ_QKR,DQ_USE,DQ_DDCODE,DQ_DDZJE,DQ_CSCODE,DQ_CSNAME,DQ_CSBANK,DQ_CSACCOUNT,DQ_AMOUNT,DQ_AMOUNTDX,DQ_ZFFS,DQ_BILLCODE,DQ_STATE,DQ_BMYJ,DQ_ZGLD,DQ_GSLD,DQ_CWSH,DQ_NOTE)" +
                    "values('" + dq_id + "','" + dq_data + "','" + dq_qkbm + "','" + dq_bmname + "','" + dq_qkr + "','" + dq_use + "','" + dq_ddcode + "','" + dq_ddzje + "','" + dq_cscode + "','" + dq_csname + "','" + dq_csbank + "','" + dq_csaccount + "','" + dq_amount + "','" + dq_amountdx + "','" + dq_zffs + "','" + dq_billcode + "','" + dq_state + "','" + dq_bmyj + "','" + dq_zgld + "','" + dq_gsld + "','" + dq_cwsh + "','" + dq_note + "')";
                sqltext.Add(str1);
            }
            if (Action == "Edit")
            {
                string str1 = "UPDATE TBPM_ORDER_CHECKREQUEST SET DQ_DATA='" + dq_data + "',DQ_QKBM='" + dq_qkbm + "',DQ_BMNAME='" + dq_bmname + "',DQ_QKR='" + dq_qkr + "',DQ_USE='" + dq_use + "',DQ_DDCODE='" + dq_ddcode + "',DQ_DDZJE='" + dq_ddzje + "',DQ_CSBANK='" + dq_csbank + "',DQ_CSACCOUNT='" + dq_csaccount + "',DQ_ZFFS='" + dq_zffs + "',DQ_BILLCODE='" + dq_billcode + "',DQ_STATE='" + dq_state + "',DQ_AMOUNT='" + dq_amount + "',DQ_AMOUNTDX='" + dq_amountdx + "',DQ_NOTE='" + dq_note + "',DQ_BMYJ='" + dq_bmyj + "',DQ_ZGLD='" + dq_zgld + "',DQ_GSLD='" + dq_gsld + "',DQ_CWSH='" + dq_cwsh + "' where DQ_ID='" + dq_id + "'";
                sqltext.Add(str1);
            }
            if (Action == "EditCW")
            {
                string str1 = "UPDATE TBPM_ORDER_CHECKREQUEST SET DQ_DATA='" + dq_data + "',DQ_QKBM='" + dq_qkbm + "',DQ_BMNAME='" + dq_bmname + "',DQ_QKR='" + dq_qkr + "',DQ_USE='" + dq_use + "',DQ_CSBANK='" + dq_csbank + "',DQ_CSACCOUNT='" + dq_csaccount + "',DQ_ZFFS='" + dq_zffs + "',DQ_BILLCODE='" + dq_billcode + "',DQ_STATE='" + dq_state + "',DQ_AMOUNT='" + dq_amount + "',DQ_AMOUNTDX='" + dq_amountdx + "',DQ_NOTE='" + dq_note + "',DQ_BMYJ='" + dq_bmyj + "',DQ_ZGLD='" + dq_zgld + "',DQ_GSLD='" + dq_gsld + "',DQ_CWSH='" + dq_cwsh + "' where DQ_ID='" + dq_id + "'";
                sqltext.Add(str1);
                if (dq_state == 3)
                {
                    SendMail();
                }

                ////检查请款单实际状态
                //string sqlcheckstate = "select DQ_STATE from TBPM_ORDER_CHECKREQUEST  where DQ_ID='" + dq_id + "'";
                //DataTable dtcheckstate = DBCallCommon.GetDTUsingSqlText(sqlcheckstate);
                //string yf=dtcheckstate.Rows[0][0].ToString().Trim();
                //if (yf == "4")
                //{
                //    SendMail();
                //}
                //if (dtcheckstate.Rows.Count > 0)
                //{
                //    string str_updataCON = "";

                //    //提交到财务，未付款
                //    if (rblDDState.SelectedValue == "2" && dtcheckstate.Rows[0][0].ToString() == "1") //
                //    { 
                    
                //    }
                //    //正在签字
                //    if (rblDDState.SelectedValue == "1" && dtcheckstate.Rows[0][0].ToString() == "2")
                //    { 
                    
                //    }
                //}
            }
            DBCallCommon.ExecuteTrans(sqltext);
            btnDDSave.Visible = false;
            lblDDRemind.Visible = true;
            btnDDPrint.Visible = true;
        }

        //付款后发送邮件给请款人
        private void SendMail()
        {
            string subject = "";
            List<string> copyto = new List<string>();
            subject="采购订单请款财务已付款";
            if(lblDDCR_ID.Text.Trim()!="")
            {
                subject+="———"+lblDDCR_ID.Text.Trim();
            }
            string body = "\r\n采购订单请款单号：" + lblDDCR_ID.Text.ToString() + "\r\n采购订单号：" + txtDDCode.Text.ToString() + "\r\n财务于" + DateTime.Now.ToString() + "已支付" + txtDDFKJE.Text.ToString() + "元于您，请查收确认!!!";
            string sql = "select DISTINCT [EMAIL] from TBDS_STAFFINFO where ST_NAME='" + txtDDQKR.Text.Trim() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                DBCallCommon.SendEmail(dt.Rows[0][0].ToString(), copyto, null, subject, body);
            }
        }

        
        //查看对应订单
        protected void btn_ViewOrder_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "View_PurOrder('" + txtDDCode.Text + "');", true);
        }

        //查看发票，对应发票的号码
        protected void btn_ViewBill_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "View_Bill('" + txtDDPZH.Text + "','DView');", true);
        }

        //付款金额发生变化
        protected void txtDDFKJE_TextChanged(object sender, EventArgs e)
        {
            //string ddze = txtDDZJE.Text.ToString();
            double ddze = Convert.ToDouble(txtDDZJE.Text.ToString());
            double yfke = Convert.ToDouble(txtDDFKJE.Text.ToString());
            this.ClientScript.RegisterStartupScript(GetType(), "js", "javascript:Actual_Payment_DX('" + txtDDFKJE.Text.ToString() + "');", true); //转大写
            //if (ddze != yfke)
            //{
            //    if (ddze > yfke)
            //    {
            //        this.ClientScript.RegisterStartupScript(GetType(), "js", "javascript:alert('订单总额大于应付款额!\\r\\r请检查！');", true); return;
            //    }
            //    else if (ddze < yfke)
            //    {
            //        this.ClientScript.RegisterStartupScript(GetType(), "js", "javascript:alert('订单总额小于应付款额!\\r\\r请检查！');", true); return;
            //    }
            //}
            //else
            //{
            //    this.ClientScript.RegisterStartupScript(GetType(), "js", "javascript:Actual_Payment_DX('" + txtDDFKJE.Text.ToString() + "');", true); //转大写
            //}
                 
        }
    }
}
