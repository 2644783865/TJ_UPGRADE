using System;
using System.Data;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

/********数据库：TBPM_ORDER_BILL*************/
/********MnegQingTong 2013年1月17日**********/

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CR_OrderBill_Add : System.Web.UI.Page
    {
        string DDCode = "";    //订单号
        string DDJE = "";      //已选订单总金额
        string Action = "";    //编辑查看状态
        string DDCSCode = "";  //供货商ID
        string OBCode = "";    //发票记录编号
        string Bill = "";      //发票号

        protected void Page_Load(object sender, EventArgs e)
        {
            /******获取请求状态********/
            Action = Request.QueryString["Action"];
            DDCode = Request.QueryString["orderid"];    //订单号
            DDJE = Request.QueryString["ZJE"];          //总金额
            DDCSCode = Request.QueryString["csinfo"];   //厂商编号
            
            if (!IsPostBack)
            {
                if (Action == "add")
                {
                    GetCSInfo();
                    lblFP.Text = "-->添加采购订单发票信息";
                    txtJLID.Text = EnDDBillCode();        //发票记录编号                
                    txtKPRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");   //开票时间
                    txtDDCode.Text = DDCode.ToString();   //订单号
                    txtDDZJE.Text = DDJE.ToString();      //订单总金额
                    txtCSCode.Text = DDCSCode.ToString(); //厂商编码
                    txtJBR.Text = Session["UserName"].ToString();   //经办人为当前人
                    this.ControlEnable(4);
                }
                if (Action == "Edit")
                {
                    lblFP.Text = "-->编辑采购订单发票信息";
                    btn_Add.Text = "修改并保存";
                    Lbtn_ViewOrder.Visible = true;
                    OBCode = Request.QueryString["billcode"];
                    this.BindDataEdit(OBCode);
                    this.ControlEnable(1);   //制单人编辑
                    
                }
                if (Action == "EditCW")
                {
                    lblFP.Text = "-->编辑采购订单发票信息";
                    btn_Add.Text = "修改并保存";
                    Lbtn_ViewOrder.Visible = true;
                    OBCode = Request.QueryString["billcode"];
                    this.BindDataEdit(OBCode);
                    this.ControlEnable(2);   //财务编辑
                }
                if (Action == "View")
                {
                    lblFP.Text = "-->查看采购订单发票信息";
                    Lbtn_ViewOrder.Visible = true;
                    OBCode = Request.QueryString["billcode"];
                    this.BindDataEdit(OBCode);
                    this.ControlEnable(3);   //查看
                }
                if (Action == "DView")
                {
                    lblFP.Text = "-->查看采购订单发票信息";
                    Lbtn_ViewOrder.Visible = true;
                    Bill = Request.QueryString["bill"];
                    this.BindDataView(Bill);
                    this.ControlEnable(3);   //查看
                }
            }
        }

        //输入控制
        private void ControlEnable(int status)
        {
            switch(status)
            {
                case 1: //制单人编辑
                    txtJLID.Enabled = false;   //记录编号
                    txtKPRQ.Enabled = true;   //开票日期
                    txtDDCode.Enabled = true; //订单号
                    txtDDZJE.Enabled = true;
                    txtCSCode.Enabled = false;
                    txtCSName.Enabled = false; //开票单位
                    txtFPDH.Enabled = true;   //发票单号
                    txtFPSL.Enabled = true;   //发票数量
                    txtDDZJE.Enabled = true;  //订单总金额
                    txtKPJE.Enabled = true;   //开票金额
                    txtJBR.Enabled = true;    //经办人
                    txtSPRQ.Enabled = false;   //收票时间
                    txtNote.Enabled = true;   //备注
                    rblPZ.Enabled = true;     //票证
                    txtPZH.Enabled = true;    //票证号
                    hlSelect.Visible = true;  //厂商选择
                    btn_CRAdd.Visible = true;
                    break;

                case 2: //财务编辑
                    txtJLID.Enabled = false;   //记录编号
                    txtKPRQ.Enabled = true;    //开票日期
                    txtDDCode.Enabled = false; //订单号
                    txtCSCode.Enabled = false; //单位编码
                    txtCSName.Enabled = false; //开票单位
                    txtFPDH.Enabled = true;   //发票单号
                    txtFPSL.Enabled = true;   //发票数量
                    txtDDZJE.Enabled = false;  //订单总金额
                    txtKPJE.Enabled = true;   //开票金额
                    txtJBR.Enabled = false;    //经办人
                    txtSPRQ.Enabled = true;   //收票时间
                    txtNote.Enabled = true;   //备注
                    rblPZ.Enabled = true;     //票证
                    txtPZH.Enabled = true;    //票证号
                    hlSelect.Visible = false;
                    break;

                case 3: //查看

                    btn_Add.Visible = false;   //保存按钮不可见

                    txtJLID.Enabled = false;   //记录编号
                    txtKPRQ.Enabled = false;   //开票日期
                    txtDDCode.Enabled = false; //订单号
                    txtCSCode.Enabled = false;
                    txtCSName.Enabled = false; //开票单位
                    txtFPDH.Enabled = false;   //发票单号
                    txtFPSL.Enabled = false;   //发票数量
                    txtDDZJE.Enabled = false;  //订单总金额
                    txtKPJE.Enabled = false;   //开票金额
                    txtJBR.Enabled = false;    //经办人
                    txtSPRQ.Enabled = false;   //收票时间
                    txtNote.Enabled = false;   //备注
                    rblPZ.Enabled = false;     //票证
                    txtPZH.Enabled = false;    //票证号
                    hlSelect.Visible = false;
                    btn_CRAdd.Visible = true;
                    break;

                case 4:  //添加
                    txtJLID.Enabled = false;   //记录编号
                    txtKPRQ.Enabled = true;   //开票日期
                    txtDDCode.Enabled = true; //订单号
                    txtDDZJE.Enabled = true;
                    txtCSCode.Enabled = false;
                    txtCSName.Enabled = false; //开票单位
                    txtFPDH.Enabled = true;   //发票单号
                    txtFPSL.Enabled = true;   //发票数量
                    txtDDZJE.Enabled = true;  //订单总金额
                    txtKPJE.Enabled = true;   //开票金额
                    txtJBR.Enabled = true;    //经办人
                    txtSPRQ.Enabled = false;   //收票时间
                    txtNote.Enabled = true;   //备注
                    rblPZ.Enabled = true;     //票证
                    txtPZH.Enabled = true;    //票证号
                    txtSPRQ.Enabled = false;
                    hlSelect.Visible = true;
                    break;
            }
        }

        //获取厂商信息
        private void GetCSInfo()
        {
            string sql = "SELECT CS_NAME,CS_BANK,CS_ACCOUNT from TBCS_CUSUPINFO where CS_CODE='" + DDCSCode + "'";
            DataTable cs = DBCallCommon.GetDTUsingSqlText(sql);
            txtCSName.Text = cs.Rows[0]["CS_NAME"].ToString();
        }

        //生成发票记录编号
        private string EnDDBillCode()
        {
            string OB_CODE = "";
            string str_time = DateTime.Now.ToString("yyyy.MM");
            string str = "select top 1 OB_CODE from TBPM_Order_BILL where OB_CODE like '%" + str_time + "%' order by OB_CODE desc";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(str);
            string lsh = "";
            string ID = "";
            if (dr.HasRows)
            {
                dr.Read();
                ID = dr["OB_CODE"].ToString();
                string[] pr = ID.Split('-');
                lsh = (Convert.ToInt16(pr[pr.Length - 1].ToString()) + 1).ToString();
                lsh = lsh.PadLeft(4, '0');
                dr.Close();
            }
            else
            {
                lsh = "0001";
            }

            OB_CODE = "ZCZJ.CG.BILL." + str_time + "-" + lsh;
            return OB_CODE;
        }

        //通过发票记录编号获取数据
        private void BindDataEdit(string BillCode)
        {
            string sqlstr = "select OB_CODE,OB_DDCODE,OB_DDZJE,OB_BILLCODE,OB_BILLNUM,OB_BILLJE,OB_KPDATE,OB_SPDATE,OB_CSCODE,OB_CSNAME,OB_JBR,OB_PZ,OB_PZH,OB_NOTE from TBPM_ORDER_Bill where  OB_CODE='" + OBCode + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlstr);
            if (dr.HasRows)
            {
                dr.Read();
                txtJLID.Text = dr["OB_CODE"].ToString().Trim();
                txtKPRQ.Text = Convert.ToDateTime(dr["OB_KPDATE"].ToString()).ToShortDateString();
                //txtDDQKRQ.Text = Convert.ToDateTime(dr["DQ_DATA"].ToString()).ToShortDateString();
                txtDDCode.Text = dr["OB_DDCODE"].ToString().Trim();
                txtDDZJE.Text = dr["OB_DDZJE"].ToString().Trim();
                txtCSCode.Text = dr["OB_CSCODE"].ToString().Trim();
                txtCSName.Text = dr["OB_CSNAME"].ToString().Trim();
                txtFPDH.Text = dr["OB_BILLCODE"].ToString().Trim();
                txtFPSL.Text = dr["OB_BILLNUM"].ToString().Trim();
                txtKPJE.Text = dr["OB_BILLJE"].ToString().Trim();
                txtJBR.Text = dr["OB_JBR"].ToString().Trim();
                //txtSPRQ.Text = Convert.ToDateTime(dr["OB_SPDATE"].ToString()).ToShortDateString();
                txtSPRQ.Text = (dr["OB_SPDATE"].ToString().Trim() == "" ? "" : Convert.ToDateTime(dr["OB_SPDATE"].ToString().Trim()).ToShortDateString());
                foreach (ListItem li in rblPZ.Items)
                {
                    if (dr["OB_PZ"].ToString() == li.Value.ToString())
                    {
                        li.Selected = true; break;
                    }
                }
                txtNote.Text = dr["OB_NOTE"].ToString().Trim();
                dr.Close();
            }
        }

        //通过发票号码查看发票信息
        private void BindDataView(string Bill)
        {
            string sqlstr = "select OB_CODE,OB_DDCODE,OB_DDZJE,OB_BILLCODE,OB_BILLNUM,OB_BILLJE,OB_KPDATE,OB_SPDATE,OB_CSCODE,OB_CSNAME,OB_JBR,OB_PZ,OB_PZH,OB_NOTE from TBPM_ORDER_Bill where  OB_BILLCODE='" + Bill + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlstr);
            if (dr.HasRows)
            {
                dr.Read();
                txtJLID.Text = dr["OB_CODE"].ToString().Trim();
                txtKPRQ.Text = Convert.ToDateTime(dr["OB_KPDATE"].ToString()).ToShortDateString();
                //txtDDQKRQ.Text = Convert.ToDateTime(dr["DQ_DATA"].ToString()).ToShortDateString();
                txtDDCode.Text = dr["OB_DDCODE"].ToString().Trim();
                txtDDZJE.Text = dr["OB_DDZJE"].ToString().Trim();
                txtCSCode.Text = dr["OB_CSCODE"].ToString().Trim();
                txtCSName.Text = dr["OB_CSNAME"].ToString().Trim();
                txtFPDH.Text = dr["OB_BILLCODE"].ToString().Trim();
                txtFPSL.Text = dr["OB_BILLNUM"].ToString().Trim();
                txtKPJE.Text = dr["OB_BILLJE"].ToString().Trim();
                txtJBR.Text = dr["OB_JBR"].ToString().Trim();
                txtSPRQ.Text = (dr["OB_SPDATE"].ToString().Trim()==""?"":Convert.ToDateTime(dr["OB_SPDATE"].ToString().Trim()).ToShortDateString());
                //(txtKPJE.Text.Trim() == "" ? 0 : Convert.ToDouble(txtKPJE.Text.Trim()));
                //txtSPRQ.Text = dr["OB_SPDATE"].ToString();
                foreach (ListItem li in rblPZ.Items)
                {
                    if (dr["OB_PZ"].ToString() == li.Value.ToString())
                    {
                        li.Selected = true; break;
                    }
                }
                txtNote.Text = dr["OB_NOTE"].ToString().Trim();
                dr.Close();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有相关发票信息！');", true);
                return;
            }
        }

        //保存（按钮事件）
        protected void btn_Add_Click(object sender, EventArgs e)
        {
            if (txtPZH.Text != "" && txtPZH != null)
            { 
                rblPZ.SelectedIndex = 1; 
            }
            if (CheckMustPutIn())
            {
                this.AddDDFP();
                //this.ClientScript.RegisterStartupScript(GetType(), "js", "window.returnValue=\"refresh\";window.close();", true);
                Response.Write("<script>window.opener.location.reload();</script>");//刷新
            }
        }
        //生成请款单
        protected void btn_CRAdd_Click(object sender, EventArgs e)
        {
            string orders = txtDDCode.Text.ToString().Trim();
            string ddmoney = txtDDZJE.Text.ToString().Trim();
            string ddcscode = txtCSCode.Text.ToString().Trim();
            string bill = txtFPDH.Text.ToString().Trim();
            string sql = "select DQ_BillCode from TBPM_ORDER_CHECKREQUEST WHERE DQ_BillCode='" + bill + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            string sqltext = "select DQ_DDCode from TBPM_ORDER_CHECKREQUEST where DQ_DDCODE like '%" + orders + "%'";
            System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);

            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已存在相关发票的请款！！！');", true);
            }
            else if (dt1.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已存在相关订单的请款！！！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "js", "Add_DDQK('" + orders + "','" + ddmoney + "','" + ddcscode + "','" + bill + "');", true);
            }
            
        }

        //将数据保存或修改到数据库
        private void AddDDFP()
        {
            //发票记录编号
            string ob_code = txtJLID.Text.Trim();
            //订单号
            string ob_ddcode = txtDDCode.Text.Trim();
            //订单总金额
            string ob_ddzje = txtDDZJE.Text.Trim();
            //发票号码
            string ob_billcode = txtFPDH.Text.Trim();
            //发票数量
            string ob_billnum = txtFPSL.Text.Trim();
            //发票金额（含税）
            string ob_billje = txtKPJE.Text.Trim();
            //开票时间
            string ob_kpdate = txtKPRQ.Text.Trim();
            //收票时间
            string ob_spdate = txtSPRQ.Text.Trim() != "" ? txtSPRQ.Text.Trim() : "";
            //厂商编码
            string ob_cscode = txtCSCode.Text.Trim();
            //收票单位
            string ob_csname = txtCSName.Text.Trim();
            //经办人
            string ob_jbr = txtJBR.Text.Trim();
            //票证（有/暂无）
            int ob_pz = Convert.ToInt16(rblPZ.SelectedValue.ToString());
            //票证号
            string ob_pzh = txtPZH.Text.Trim();
            //备注
            string ob_note = txtNote.Text.Trim();

            if (Action == "add")
            {
                string sqlstr = "insert into TBPM_ORDER_BILL (OB_CODE,OB_DDCODE,OB_DDZJE,OB_BILLCODE,OB_BILLNUM,OB_BILLJE,OB_KPDATE,OB_CSCODE,OB_CSNAME,OB_JBR,OB_PZ,OB_PZH,OB_NOTE)" +
                    "Values ('" + ob_code + "','" + ob_ddcode + "','" + ob_ddzje + "','" + ob_billcode + "','" + ob_billnum + "','" + ob_billje + "','" + ob_kpdate + "','" + ob_cscode + "','" + ob_csname + "','" + ob_jbr + "','" + ob_pz + "','" + ob_pzh + "','" + ob_note + "')";
                DBCallCommon.ExeSqlText(sqlstr);
                btn_Add.Visible = false;
                Label1.Visible = true;
            }
            if (Action == "Edit")
            {
                string sqlstr = "update TBPM_ORDER_BILL set OB_DDCODE='" + ob_ddcode + "',OB_DDZJE='" + ob_ddzje + "', OB_BILLCODE='" + ob_billcode + "',OB_BILLNUM='" + ob_billnum + "',OB_BILLJE='" + ob_billje + "',OB_KPDATE='" + ob_kpdate + "',OB_CSCODE='" + ob_cscode + "',OB_CSNAME='" + ob_csname + "',OB_JBR='" + ob_jbr + "',OB_PZ='" + ob_pz + "',OB_PZH='" + ob_pzh + "',OB_NOTE='" + ob_note + "' WHERE OB_CODE='" + ob_code + "'";
                DBCallCommon.ExeSqlText(sqlstr);
                btn_Add.Visible = false;
                Label1.Visible = true;
            }
            if (Action == "EditCW")
            {
                string sqlstr = "update TBPM_ORDER_BILL set OB_BILLCODE='" + ob_billcode + "',OB_BILLNUM='" + ob_billnum + "',OB_BILLJE='" + ob_billje + "',OB_KPDATE='" + ob_kpdate + "',OB_SPDATE='" + ob_spdate + "',OB_PZ='" + ob_pz + "',OB_PZH='" + ob_pzh + "',OB_NOTE='" + ob_note + "' WHERE OB_CODE='" + ob_code + "'";
                DBCallCommon.ExeSqlText(sqlstr);
                btn_Add.Visible = false;
                Label1.Visible = true;
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

        //查看相关订单
        protected void btn_ViewOrder_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "View_PurOrder('" + txtDDCode.Text + "');", true);
        }
        //输入票证号后票证状态改变
        protected void txtPZH_Changed(object sender, EventArgs e)
        {
            if (txtPZH.Text.Trim() != "")
            {
                rblPZ.SelectedIndex = 1;
            }
        }

        //protected void OrderTextChanged(object sender, EventArgs e)
        //{
        //    string orders = txtDDCode.Text.ToString();
        //    string[] allorder = orders.Split(',');
        //    double orderzje=0;
        //    for (int i = 0; i < allorder.Length; i++)
        //    {
        //        string sql = "select PO_ZJE FROM View_TBPC_PURORDERDETAIL_AMOUNT WHERE orderno='" + allorder[i].ToString() + "'";
        //        DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
        //        double orderje = Convert.ToDouble(dt.Rows[0][0].ToString().Trim());
        //        orderzje += orderje;
        //    }
        //    //Page.ClientScript.RegisterClientScriptBlock(this, GetType(), "", "Reddje('" + orderje.ToString() + "');", true);
        //    this.ClientScript.RegisterStartupScript(GetType(), "js", "javascript:Re_DDJE('" +orderzje.ToString()+ "');", true); 
        //}
    }
}
