using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_Contract_Add_SC : System.Web.UI.Page
    {
        string condetail_id = "";//修改的合同编号
        string action = "";//Edit or Add
        string conForm = "";//合同类别
        string sqltext = "";
        private double kpje = 0;
        private double hjqkje1 = 0;
        private double hjfkje = 0;
        List<string> str = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["Action"];
            conForm = Request.QueryString["ConForm"];
            condetail_id = Request.QueryString["condetail_id"];
            //Page.DataBind();
            if (!IsPostBack)
            {
                //添加时先创建一个全球唯一标识，存储在一个lable中，用来关联合同与附件，避免因合同号占用而导致附件关联错误
                if (action == "Add")
                {
                    Guid tempid = Guid.NewGuid();
                    lbl_UNID.Text = tempid.ToString();

                    lb_lock.Visible = true;
                    LinkLock.Visible = true;
                    lb_addtips.Visible = true;
                    LbtnNO.Visible = true;
                    //释放锁定超过60分钟还未提交的合同号，先找时间差大于60分钟的，再检查这些合同号是否存在于合同表，如果在表示已经提交，不在则表示未提交，对于这些未提交的释放合同号可供其他人使用
                    string sqltext = "DELETE FROM TBCM_TEMPCONNO WHERE DATEDIFF(MI,CREATETIME,'" + DateTime.Now + "')>60 AND " +
                                     " CON_NO NOT IN (SELECT PCON_BCODE AS CON_NO FROM TBPM_CONPCHSINFO)";
                    DBCallCommon.ExeSqlText(sqltext);
                }
                this.InitPageInfo();
                Control_EditPower();
            }
            BindLowerData();
            InitUpload();
        }

        /// <summary>
        /// 根据上一页面传递值，初始化页面信息
        /// </summary>
        #region
        private void InitPageInfo()
        {
            string type = this.GetConForm(conForm); //0:商务合同；1：委外合同。。。。。
            this.BindDep();//部门名称绑定
            txtPCON_TYPE.Enabled = false;
            if (action == "Add") //如果新增
            {
                ViewState["qt_chg"] = 0;//其他变更
                rblState.SelectedIndex = 1;
                btnConfirm.Text = "确认添加";
                btnConfirm.Visible = true;
                lblConForm.Text = type;
                lblAddOREdit.Text = "新增" + type;
                UpdatePanel1.Visible = false; //在合同未添加到数据库时，无法进行其他操作
                btn_RevInfo.Visible = false;
                txtPCON_RESPONSER.Text = Session["UserName"].ToString();
                txtPCON_TYPE.Text = "生产外协";
                //委外合同多种类别
                //palWW.Visible = true;
                //palWW.Visible = false;
                dplPCON_RSPDEPID.SelectedValue = "04";
                //dplPCON_RSPDEPID_SelectedIndexChanged(null, null);
                this.LinkLock_Click(null, null);//锁定合同号
                dplPCON_RSPDEPID.Enabled = false;
            }
            else if (action == "Edit")
            {
                lblConForm.Text = type;
                lblConID.Text = "合同号:" + condetail_id;
                lblAddOREdit.Text = "修改" + type;
                btnConfirm.Text = "确认修改";
                this.BindContractDetail(condetail_id);

                if (Session["UserDeptID"].ToString() != "08") //只有财务的人才可以付款、修改凭证
                {
                    //无法付款
                    grvDBQK.Columns[grvDBQK.Columns.Count - 1].Visible = false;
                    //无法修竹凭证
                    grvFKJL.Columns[grvFKJL.Columns.Count - 1].Visible = false;
                }
            }
            else if (action == "View")//View
            {
                lblConForm.Text = type;
                lblConID.Text = "合同号:" + condetail_id;
                lblAddOREdit.Text = "查看" + type;
                btnConfirm.Visible = false;
                this.BindContractDetail(condetail_id);
                //合同基本信息不可修改
                this.ControlsEnabe();
                hlSelect.Visible = false;
                //无法请款
                btnADDCR.Visible = false;
                //无法修改请款
                grvQK.Columns[grvQK.Columns.Count - 2].Visible = false;
                //无法添加发票
                btnAddFP.Visible = false;
                //无法修改发票
                grvFP.Columns[grvFP.Columns.Count - 3].Visible = false;
                //无法删除发票
                grvFP.Columns[grvFP.Columns.Count - 1].Visible = false;
                //无法添加补充协议
                add_bcxy.Visible = false;
                //无法付款
                grvDBQK.Columns[grvDBQK.Columns.Count - 1].Visible = false;
                //无法修竹凭证
                grvFKJL.Columns[grvFKJL.Columns.Count - 1].Visible = false;
                ZCZJ_DPF.Contract_Data.ContractClass.UploadControlSet(UploadAttachments1, action);
            }
        }

        //控制编辑者权限
        private void Control_EditPower()
        {
            if (Session["UserName"].ToString() != txtPCON_RESPONSER.Text.Trim())//不是负责人
            {
                ZCZJ_DPF.Contract_Data.ContractClass.UploadControlSet(UploadAttachments1, "View");
            }
        }

        //初始附件上传控件
        private void InitUpload()
        {
            if (txtPCON_BCODE.Text.Trim() != "")
            {
                UploadAttachments1.Visible = true;
                UploadAttachments1.at_htbh = lbl_UNID.Text;
                UploadAttachments1.at_type = 0;
                UploadAttachments1.at_sp = 0;
            }
            else
            {
                UploadAttachments1.Visible = false;
            }
            //******************************
        }
        //控件状态控制
        private void ControlsEnabe()
        {
            ////合同编号,不可修改
            //txtPCON_BCODE.Enabled = false;
            ////合同名称
            //txtPCON_NAME.Enabled = false;
            ////项目编号,不可修改
            //txtPCON_PJID.Enabled = false;
            ////项目名称,不可修改
            //tb_pjinfo.Enabled = false; ;
            ////责任部门
            //dplPCON_RSPDEPID.Enabled = false;
            ////工程名称,不可修改
            //cmbPCON_ENGNAME.Enabled = false;
            //btn_clear_eng.Enabled = false;
            ////合同类别
            //txtPCON_TYPE.Enabled = false;
            ////设备类型
            //txtPCON_ENGTYPE.Enabled = false;
            //ddl_engtype.Enabled = false;
            ////合同金额
            //txtPCON_JINE.Enabled = false;
            ////其他币种
            ////Other_MONUNIT.Enabled = false;
            ////货币单位，不可修改
            ////ddl_PCONMONUNIT.Enabled = false;
            ////签订日期
            //txtPCON_FILLDATE.Enabled = false;
            ////生效日期
            ////txtPCON_VALIDDATE.Enabled=true;               

            //负责人
            txtPCON_RESPONSER.Enabled = false;

            ////结算金额
            //txtPCON_BALANCEACNT.Enabled = false;

            ////交货日期
            //txtPCON_DELIVERYDATE.Enabled = false;
            ////计入成本
            //txtPCON_COST.Enabled = false;
            ////备注
            //txtPCON_NOTE.Enabled = false;
            ////合同类别，不可修改 0、1、2、3、4
            ////合同状态
            //rblState.Enabled = false;
        }
        /// <summary>
        /// 绑定某一合同详细信息，包括基本信息、请款信息、付款记录、发票记录
        /// </summary>
        /// <param name="contractid"></param>
        private void BindContractDetail(string contractid)
        {
            ViewState["qt_chg"] = 0;
            this.BindConBasicData(contractid);

        }

        private void BindLowerData()
        {
            this.BindCRData(condetail_id);
            this.BindPayData(condetail_id);
            this.BindBillData(condetail_id);
            this.jsdbind();
            this.BindBCXYData(condetail_id);
            this.Bindbt();
        }

        private void Bindbt()
        {
            string sqltxt = "";
            sqltxt = "select * from TBPM_FUKUAN where CR_ID like '%" + txtPCON_BCODE.Text + "%'";
            if (DBCallCommon.GetDTUsingSqlText(sqltxt).Rows.Count > 0)
            {
                btnadd.Visible = false;
                txtPCON_SCH.Enabled = false;
                delete.Visible = false;
            }
        }

        #region
        /// 合同基本信息绑定
        private void BindConBasicData(string contractid)
        {
            string sqlStr = "select * from TBPM_CONPCHSINFO where PCON_BCODE='" + contractid + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlStr);
            if (dr.HasRows)
            {
                dr.Read();
                //唯一编号
                lbl_UNID.Text = dr["GUID"].ToString();
                //合同编号,不可修改
                txtPCON_BCODE.Text = dr["PCON_BCODE"].ToString();
                txtPCON_BCODE.Enabled = false;
                //合同名称
                txtPCON_NAME.Text = dr["PCON_NAME"].ToString();
                //项目名称,不可修改
                tb_pjinfo.Text = dr["PCON_PJNAME"].ToString();
                tb_pjinfo.Enabled = false;
                //责任部门
                dplPCON_RSPDEPID.ClearSelection();
                foreach (ListItem li in dplPCON_RSPDEPID.Items)
                {
                    if (li.Value.ToString() == dr["PCON_RSPDEPID"].ToString())
                    {
                        li.Selected = true; break;
                    }
                }


                //合同类别
                txtPCON_TYPE.Text = dr["PCON_TYPE"].ToString();
                //合同金额
                //txtPCON_JINE.Enabled = false;
                txtPCON_JINE.Text = dr["PCON_JINE"].ToString();
                //其他币种
                //Other_MONUNIT.Text = dr["OTHER_MONUNIT"].ToString();
                //货币单位，不可修改
                //ddl_PCONMONUNIT.SelectedValue=dr["PCON_MONUNIT"].ToString();
                //ddl_PCONMONUNIT.Enabled = false;
                //生效日期
                //txtPCON_VALIDDATE.Text =dr["PCON_VALIDDATE"].ToString();
                //客户名称
                txtPCON_CUSTMNAME.Value = dr["PCON_CUSTMNAME"].ToString();
                //客户编号
                txtPCON_CUSTMID.Value = dr["PCON_CUSTMID"].ToString();
                //负责人
                txtPCON_RESPONSER.Text = dr["PCON_RESPONSER"].ToString();
                //结算金额
                txtPCON_BALANCEACNT.Text = dr["PCON_BALANCEACNT"].ToString();
                //交货日期
                txtPCON_TASK.Text = dr["PCON_TASK"].ToString();
                //计入成本
                txtPCON_FILLDATE.Text = dr["PCON_FILLDATE"].ToString();
                //备注
                txtPCON_NOTE.Text = dr["PCON_NOTE"].ToString();
                //外协单据号
                txtPCON_ORDERID.Text = dr["PCON_ORDERID"].ToString();
                //合同类别，不可修改 0、1、2、3、4
                //合同状态
                rblState.SelectedIndex = Convert.ToInt16(dr["PCON_STATE"].ToString());
                if (rblState.SelectedIndex == 1)
                {
                    rblState.Items[0].Enabled = false;//如果项目已进行，则未开始项不可用
                }
                //Updatepanel
                //已付金额
                lblYFJE.Text = dr["PCON_YFK"].ToString();

                //合同金额，有结算金额时以结算金额为准                 
                string jsje = dr["PCON_BALANCEACNT"].ToString();
                string htzj = "";
                if (jsje == "" || Convert.ToDecimal(jsje) == 0)
                {
                    htzj = dr["PCON_JINE"].ToString();
                }
                else
                {
                    htzj = dr["PCON_BALANCEACNT"].ToString();
                }

                lblHTJE.Text = htzj;
                //支付比例
                string bl = string.Format("{0:N4}", Convert.ToDouble(lblYFJE.Text) / Convert.ToDouble(Convert.ToDouble(lblHTJE.Text) == 0 ? "1" : lblHTJE.Text)).ToString();
                lblZFBL.Text = (Convert.ToDouble(bl) * 100).ToString() + "%";

                //合同评审单号
                tb_revid.Text = dr["PCON_REVID"].ToString();
                //合同评审状态
                string sqlselectzt = "select CR_PSZT from TBCR_CONTRACTREVIEW where CR_ID='" + dr["PCON_REVID"].ToString() + "'";
                DataTable dt_zt = DBCallCommon.GetDTUsingSqlText(sqlselectzt);
                if (dt_zt.Rows.Count > 0)
                {
                    string pszt = dt_zt.Rows[0]["CR_PSZT"].ToString();
                    lbl_pszt.Text = pszt == "1" ? "审批中" : pszt == "2" ? "审批通过" : "已驳回";
                }

                btn_RevInfo.Visible = lbl_pszt.Text.Trim() == "" ? false : true;
                dr.Close();
            }
            sqlStr = "select * from TBPM_SCDETAIL where SC_CONTR='" + txtPCON_BCODE.Text + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlStr);
            addRep.DataSource = dt;
            addRep.DataBind();
            InitVar();
            this.InitControlUseState(rblState.SelectedValue.ToString());
        }

        //根据合同的状态对控件的可用性进行控制
        private void InitControlUseState(string state)
        {
            switch (state)
            {
                case "0"://未开始
                    UpdatePanel1.Visible = false;
                    break;
                case "1"://进行中
                    //palCHG.Visible = true;//变更
                    ////项目名称
                    //tb_pjinfo.Enabled = false;
                    ////工程名称
                    //cmbPCON_ENGNAME.Enabled = false;
                    //btn_clear_eng.Enabled = false;
                    ////责任部门
                    //dplPCON_RSPDEPID.Enabled = false;
                    ////设备类型
                    //txtPCON_ENGTYPE.Enabled = false;
                    //ddl_engtype.Enabled = false;
                    ////合同类别
                    //txtPCON_TYPE.Enabled = false;
                    ////生效日期
                    ////txtPCON_VALIDDATE.Enabled = true;
                    ////合同名称
                    //txtPCON_NAME.Enabled = false;
                    ////合同金额
                    //txtPCON_JINE.Enabled = false;
                    ////Other_MONUNIT.Enabled = false;
                    ////ddl_PCONMONUNIT.Enabled = false;
                    ////签订日期
                    //txtPCON_FILLDATE.Enabled = false;
                    //hlSelect.Visible = false;
                    ////负责人
                    //txtPCON_RESPONSER.Enabled = false;
                    ////交货日期
                    //txtPCON_DELIVERYDATE.Enabled = false;
                    break;
                case "2": //已完成
                    //基本信息
                    palBasicInfo.Enabled = false;
                    hlSelect.Visible = false;
                    //附件上传
                    UploadAttachments1.FindControl("btnUpload").Visible = false;
                    GridView gridView1 = (GridView)UploadAttachments1.FindControl("GridView1");
                    gridView1.Columns[4].Visible = false;
                    //无法请款
                    btnADDCR.Visible = false;
                    //无法修改请款
                    grvQK.Columns[grvQK.Columns.Count - 2].Visible = false;
                    //无法添加发票
                    btnAddFP.Visible = false;
                    break;
                default:
                    break;
            }
        }

        /// 合同请款信息绑定
        private void BindCRData(string contractid)
        {
            string sqlstr = "select * from TBPM_CHECKREQUEST where CR_HTBH='" + contractid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlstr);
            if (dt.Rows.Count > 0)
            {
                grvQK.DataSource = dt;
                grvQK.DataKeyNames = new string[] { "CR_ID" };
                grvQK.DataBind();
                NoDataPanelQK.Visible = false;
            }
            else
            {
                grvQK.DataSource = null;
                grvQK.DataBind();
                NoDataPanelQK.Visible = true;
            }

        }

        /// 合同付款记录绑定
        private void BindPayData(string contractid)
        {
            //请款单未支付记录绑定
            string sqlstr1 = "select * from TBPM_CHECKREQUEST a,TBDS_DEPINFO b  where a.CR_QKBM=b.DEP_CODE and CR_HTBH='" + condetail_id + "' and CR_STATE IN (2,3)";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlstr1);
            if (dt.Rows.Count > 0)
            {
                grvDBQK.DataSource = dt;
                grvDBQK.DataBind();
                NoDataPanelDBQK.Visible = false;
            }
            else
            {
                grvDBQK.DataSource = null;
                grvDBQK.DataBind();
                NoDataPanelDBQK.Visible = true;
            }

            if (Session["UserDeptID"].ToString() != "08") //只有财务的人才可以付款
            {
                //无法付款
                grvDBQK.Columns[grvDBQK.Columns.Count - 1].Visible = false;
            }

            //请款单已支付记录绑定
            string sqlstr2 = "SELECT PR_ID,SUBSTRING(PR_ID,CHARINDEX('-',PR_ID)+1,1) AS QKQC,PR_ZCRQ, PR_BCZFJE, PR_PZ, PR_PZH FROM TBPM_PAYMENTSRECORD  where  PR_HTBH='" + condetail_id + "'";
            DataTable ydt = DBCallCommon.GetDTUsingSqlText(sqlstr2);
            if (ydt.Rows.Count > 0)
            {
                grvFKJL.DataSource = ydt;
                grvFKJL.DataBind();
                NoDataPanelFKJL.Visible = false;
            }
            else
            {
                grvFKJL.DataSource = null;
                grvFKJL.DataBind();
                NoDataPanelFKJL.Visible = true;
            }
        }

        /// 合同发票记录绑定
        private void BindBillData(string contractid)
        {
            string sqlstr = "select * from TBPM_GATHINVDOC where GIV_HTBH='" + contractid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlstr);
            if (dt.Rows.Count > 0)
            {
                grvFP.DataSource = dt;
                grvFP.DataBind();
                NoDataPanelFPJL.Visible = false;
            }
            else
            {
                grvFP.DataSource = null;
                grvFP.DataBind();
                NoDataPanelFPJL.Visible = true;
            }

            if (action != "Edit")
            {
                grvFP.Columns[grvFP.Columns.Count - 3].Visible = false;
                grvFP.Columns[grvFP.Columns.Count - 1].Visible = false;
            }
        }

        //补充协议绑定
        private void BindBCXYData(string contractid)
        {
            string sqltext = "select * from TBCM_ADDCON where CON_ID like '" + contractid + "%'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                GV_AddCon.DataSource = dt;
                GV_AddCon.DataBind();
            }
            else
            {
                GV_AddCon.DataSource = null;
                GV_AddCon.DataBind();
            }
        }

        #endregion

        private string GetConForm(string bh)
        {
            switch (bh)
            {
                //case "0": return "商务合同"; 
                case "1": return "委外合同";
                case "2": return "厂内分包";
                case "3": return "采购合同";
                case "4": return "办公合同";
                case "5": return "其他合同";
                default: return null;
            }
        }
        #endregion

        //为所绑定的数据定义属性
        public string CondetailID
        {
            get
            {
                return condetail_id;
            }
        }

        public string ContactForm
        {
            get
            {
                return conForm;
            }
        }

        //责任部门绑定
        public void BindDep()
        {
            string sqltext = "select DEP_NAME,DEP_CODE from TBDS_DEPINFO where DEP_CODE like '[0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            dplPCON_RSPDEPID.DataSource = dt;
            dplPCON_RSPDEPID.DataTextField = "DEP_NAME";
            dplPCON_RSPDEPID.DataValueField = "DEP_CODE";
            dplPCON_RSPDEPID.DataBind();
            dplPCON_RSPDEPID.Items.Insert(0, new ListItem("-请选择-", "%"));
            dplPCON_RSPDEPID.SelectedIndex = 0;
        }

        //合同编号创建
        private string CreateCode(string depid)
        {
            //根据责任部门创建合同号            
            string id = "";
            switch (conForm)
            {
                case "1":  //委外合同
                    id = "TECTJWX";
                    break;
                case "2":  //采购合同  此处只包含外购合同
                    id = "TECTJCG";
                    break;
                case "3":  //运输合同
                    id = "TECTJSB.";
                    break;
                case "4":  //其他合同
                    id = "TECTJQT.";
                    break;
            }
            id += DateTime.Now.Year.ToString().Substring(2);
            string sqlText = "SELECT TOP 1 PCON_BCODE FROM (SELECT PCON_BCODE FROM TBPM_CONPCHSINFO UNION SELECT CON_NO AS PCON_BCODE FROM TBCM_TEMPCONNO ) AS A " +
                " WHERE PCON_BCODE LIKE '" + id + "%'  ORDER BY PCON_BCODE DESC";//找出该类合同的最大合同号
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.HasRows)
            {
                dr.Read();
                string bh = dr["PCON_BCODE"].ToString().Substring(9);
                int maxID = Convert.ToInt16(bh) + 1;
                dr.Close();
                id += maxID.ToString().PadLeft(3, '0');
            }
            else
            {
                id += "001";
            }
            return id;

        }
        /// <summary>
        /// 执行SQL语句，包括添加和修改
        /// </summary>
        private void ExecSQL()
        {
            //唯一标识符
            string guid = lbl_UNID.Text.Trim();
            //合同编号1
            string pcon_bcode = txtPCON_BCODE.Text.Trim();
            //合同名称2
            string pcon_name = txtPCON_NAME.Text.Trim();
            //项目名称4
            string pcon_pjname = tb_pjinfo.Text;
            //责任部门5
            string pcon_rspdepid = dplPCON_RSPDEPID.SelectedValue.ToString();
            //合同类别8
            string pcon_type = txtPCON_TYPE.Text.Trim();
            //合同金额10
            decimal pcon_jine = Convert.ToDecimal(txtPCON_JINE.Text.Trim());
            //生效日期13
            //string pcon_validdate = txtPCON_VALIDDATE.Text.ToString();
            //客户名称14
            string pcon_custmname = txtPCON_CUSTMNAME.Value.ToString();
            //客户编号15
            string pcon_custmid = txtPCON_CUSTMID.Value.ToString();
            //负责人18
            string pcon_responser = txtPCON_RESPONSER.Text.Trim();

            //结算金额21
            string js = txtPCON_BALANCEACNT.Text.Trim() == "" ? "0" : txtPCON_BALANCEACNT.Text.Trim();
            double pcon_balanceacnt = Convert.ToDouble(js);

            //交货日期23
            string pcon_task = txtPCON_TASK.Text.Trim();
            //计入成本24
            string pcon_fiidate = txtPCON_FILLDATE.Text.Trim();
            //备注25
            string pcon_note = txtPCON_NOTE.Text.Trim();
            //合同类型26
            string pcon_form = conForm;//合同的类别0、1、2、3、4
            //合同状态27
            int pcon_state = Convert.ToInt16(rblState.SelectedValue);
            //已付款28
            decimal pcon_yfk = 0;
            //应付款累计29
            decimal pcon_yfklj = 0;
            //实付款累计30
            decimal pcon_sfklj = 0;

            //异常标识32
            //添加时正常，标识的修改通过【合同索赔】界面
            int pcon_error = 0;//0:正常；1：异常
            //其他变更
            int qt_chg = Convert.ToInt16(ViewState["qt_chg"].ToString());
            //合同审批单号
            string pcon_revid = tb_revid.Text.Trim();
            List<string> list_sql = new List<string>();
            if (action == "Add" && conForm != "1")
            {

                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择委外类别！');", true); return;
            }
            else
            {
                if (action == "Add")
                {
                    //检查是否多人同时操作造成合同号重码，如果重码，则重新生成合同号
                    string check_code = " select * from TBPM_CONPCHSINFO where PCON_BCODE='" + pcon_bcode + "'";
                    DataTable dt_check = DBCallCommon.GetDTUsingSqlText(check_code);
                    if (dt_check.Rows.Count > 0)
                    {
                        string oldcode = pcon_bcode;
                        string dep_code = "";
                        switch (dplPCON_RSPDEPID.SelectedValue)
                        {
                            case "04":
                                dep_code = "WX"; break;//生产
                            case "05":
                                dep_code = "CG"; break;//质量
                            case "07":
                                dep_code = ""; break;//市场
                            case "10":
                                dep_code = "SB"; break;//设备
                            default:
                                dep_code = "QT"; break;
                        }
                        string newid = this.CreateCode(dep_code);
                        txtPCON_BCODE.Text = newid;
                        pcon_bcode = txtPCON_BCODE.Text.ToString();
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('因多人同时添加，合同号【" + oldcode + "】已经存在\\r\\r此合同号自动修改为【" + pcon_bcode + "】\\r\\r请修改合同文本中的合同号并重新上传后再保存！');", true); return;
                    }

                    sqltext = "insert into TBPM_CONPCHSINFO(PCON_BCODE,PCON_NAME,PCON_PJNAME,PCON_RSPDEPID,PCON_TYPE,PCON_JINE,PCON_CUSTMNAME,PCON_CUSTMID,PCON_RESPONSER,PCON_BALANCEACNT,PCON_TASK,PCON_FILLDATE,PCON_NOTE,PCON_FORM,PCON_STATE,PCON_YFK,PCON_YFKLJ,PCON_SFKLJ,PCON_ERROR,PCON_REVID,GUID)" +
                        "values('" + pcon_bcode + "','" + pcon_name + "','" + pcon_pjname + "','" + pcon_rspdepid + "','" + pcon_type + "','" + pcon_jine + "','" + pcon_custmname + "','" + pcon_custmid + "','" + pcon_responser + "','" + pcon_balanceacnt + "','" + pcon_task + "','" + pcon_fiidate + "','" + pcon_note + "','" + pcon_form + "','" + pcon_state + "','" + pcon_yfk + "','" + pcon_yfklj + "','" + pcon_sfklj + "','" + pcon_error + "','" + pcon_revid + "','" + guid + "')";
                    list_sql.Add(sqltext);
                }
                else if (action == "Edit")
                {
                    sqltext = "update TBPM_CONPCHSINFO set PCON_NAME='" + pcon_name + "',PCON_PJNAME='" + pcon_pjname + "',PCON_RSPDEPID='" + pcon_rspdepid + "',PCON_TYPE='" + pcon_type + "',PCON_JINE='" + pcon_jine + "',PCON_CUSTMNAME='" + pcon_custmname + "',PCON_CUSTMID='" + pcon_custmid + "',PCON_RESPONSER='" + pcon_responser + "',PCON_BALANCEACNT='" + pcon_balanceacnt + "',PCON_TASK='" + pcon_task + "',PCON_FILLDATE='" + pcon_fiidate + "',PCON_NOTE='" + pcon_note + "',PCON_FORM='" + pcon_form + "',PCON_STATE='" + pcon_state + "',PCON_QTCHG=PCON_QTCHG+" + qt_chg + ",PCON_REVID='" + pcon_revid + "' where PCON_BCODE='" + pcon_bcode + "'";
                    list_sql.Add(sqltext);
                    sqltext = "delete from TBPM_SCDETAIL where SC_CONTR='" + pcon_bcode + "'";
                    list_sql.Add(sqltext);
                }
                for (int i = 0; i < addRep.Items.Count; i++)
                {
                    List<string> val = new List<string>();
                    RepeaterItem retItem = addRep.Items[i];
                    string contr = ((Label)retItem.FindControl("TSA_ID")).Text;
                    if (contr == "")
                    {
                        contr = ((Label)addRep.Items[i - 1].FindControl("TSA_ID")).Text;
                    }
                    val.Add(txtPCON_BCODE.Text);
                    val.Add(contr);
                    val.Add(((Label)retItem.FindControl("CM_CONTR")).Text);
                    SetVal(val, retItem);
                    string value = string.Empty;
                    for (int j = 0; j < val.Count; j++)
                    {
                        value += "'" + val[j] + "',";
                    }
                    value = value.Substring(0, value.Length - 1);
                    sqltext = "insert into TBPM_SCDETAIL values(" + value + ");";
                    list_sql.Add(sqltext);
                }
                //DBCallCommon.ExeSqlText(sqltext);
                DBCallCommon.ExecuteTrans(list_sql);
                btnConfirm.Visible = false;
                if (action == "Edit")
                {
                    this.InitControlUseState(rblState.SelectedValue);
                    this.BindContractDetail(condetail_id);
                }
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('操作成功！');window.close();", true);
            }

        }

        private void SetVal(List<string> val, RepeaterItem retItem)
        {
            for (int i = 3; i < list.Count; i++)
            {
                val.Add(((TextBox)retItem.FindControl(list[i].ToString())).Text);
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (check_MustPutIn())
            {
                this.ExecSQL();
            }
        }

        //检查必填项是否填写
        private bool check_MustPutIn()
        {
            bool check = true;
            if (tb_pjinfo.Text.Trim() == "")
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('项目不能为空！！');", true); return check;

            }
            if (dplPCON_RSPDEPID.SelectedIndex == 0)
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('责任部门不能为空！！');", true); return check;

            }
            //if (txt_EngName.Text.Trim() == "")
            //{
            //    check = false;
            //    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('工程名称不能为空！！');", true); return check;

            //}
            //if (txtPCON_NAME.Text.Trim() == "")
            //{
            //    check = false;
            //    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('合同名称不能为空！！');", true); return check;

            //}
            //if (Convert.ToDecimal(txtPCON_JINE.Text.Trim()) == 0)
            //{
            //    check = false;
            //    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写合同金额！！');", true); return check;

            //}
            //if (txtPCON_DELIVERYDATE.Text.ToString() == "")
            //{
            //    check = false;
            //    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('交货日期不能为空！！');", true); return check;

            //}
            if (txtPCON_CUSTMNAME.Value.ToString() == "")
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('厂商不能为空！！');", true); return check;

            }
            if (txtPCON_TYPE.Text.Trim() == "")
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('合同类型不能为空！！');", true); return check;

            }
            if (txtPCON_BCODE.Text.Trim() == "")
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('合同编号不能为空！！');", true); return check;

            }
            if (txtPCON_RESPONSER.Text.Trim() == "")
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('负责人不能为空！！');", true); return check;

            }
            return check;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "window.close();", true);
        }

        //提示信息是否显示
        protected void rblState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblState.SelectedValue.ToString() == "2")
            {

                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('完成状态下不能修改合同所有信息！\\r\\r请确认！！');", true);
            }
        }

        //删除保存或正在签字的请款
        protected void grvQK_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                string crID = e.CommandArgument.ToString();
                string sqltext = "delete from TBPM_CHECKREQUEST where CR_ID='" + crID + "'";
                DBCallCommon.ExeSqlText(sqltext);
                this.BindCRData(condetail_id);
            }
        }

        //查看合同评审信息
        protected void btn_RevInfo_Click(object sender, EventArgs e)
        {
            //先检查评审单号是否存在，若存在则打开评审信息页面
            string revid = tb_revid.Text.Trim();
            string sqltext = "select * from TBCR_CONTRACTREVIEW where cr_id='" + revid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "js", "btnRevInfo_onclick(" + revid + ");", true); return;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('此评审单号不存在，请检查！');", true); return;

            }
        }

        //已经添加过的不能再添加，如果要修改，先删除再添加
        void jsdbind()
        {
            sqltext = " select * from TBCM_JSDDETAIL where CONID='" + condetail_id + "' ";
            DataTable dtjsd = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dtjsd.Rows.Count > 0)
            {
                btnADDJSD.Visible = false;
            }
            grvjsd.DataSource = dtjsd;
            grvjsd.DataBind();

            if (action != "Edit")
            {
                grvjsd.Columns[5].Visible = false;
            }
        }

        //删除结算单
        protected void Lbtn_Del_OnClick(object sender, EventArgs e)
        {
            List<string> sqlstr = new List<string>();
            //string conid = ((LinkButton)sender).CommandArgument.ToString();
            string sql_Del_jsd = "delete from TBCM_JSDDETAIL where CONID='" + condetail_id + "'";
            string sql_Del_jsdetail = "delete from TBCM_JSDMARINFO where conid='" + condetail_id + "'";
            sqlstr.Add(sql_Del_jsd);
            sqlstr.Add(sql_Del_jsdetail);
            DBCallCommon.ExecuteTrans(sqlstr);
            jsdbind();
            btnADDJSD.Visible = true;
        }

        //发票汇总
        protected void grvFP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                kpje += Convert.ToDouble(e.Row.Cells[4].Text);//开票金额  
                e.Row.Attributes["style"] = "Cursor:pointer";
                e.Row.Attributes.Add("onclick", "RowClick(this)");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[3].Text = "汇总：";
                e.Row.Cells[4].Text = string.Format("{0:c}", kpje);
            }
        }

        //请款汇总
        protected void grvQK_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                hjqkje1 += Convert.ToDouble(e.Row.Cells[3].Text);//请款金额                
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "汇总：";
                e.Row.Cells[3].Text = string.Format("{0:c}", hjqkje1);

            }
        }

        //付款汇总
        protected void grvFK_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                hjfkje += Convert.ToDouble(((Label)e.Row.FindControl("lbl_bczfje")).Text.Trim());//付款金额 
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[1].Text = "汇总：";
                e.Row.Cells[2].Text = string.Format("{0:c}", hjfkje);
            }
        }

        //查看订单
        protected void btn_ViewOrder_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "View_PurOrder('" + txtPCON_ORDERID.Text + "');", true);
        }

        //锁定当前合同号15分钟
        protected void LinkLock_Click(object sender, EventArgs e)
        {
            /*******锁定前应该先在数据库中搜索一次，以确保页面上待锁定的合同号没有被人使用或先锁定*********/
            //string pcon_bcode = txtPCON_BCODE.Text.Trim();
            //string check_code = " select * from TBPM_CONPCHSINFO where PCON_BCODE='" + pcon_bcode + "'";
            //DataTable dt_check = DBCallCommon.GetDTUsingSqlText(check_code);
            //if (dt_check.Rows.Count > 0)
            //{
            string dep_code = "";
            switch (dplPCON_RSPDEPID.SelectedValue)
            {
                case "04":
                    dep_code = "WX"; break;//生产
                case "05":
                    dep_code = "CG"; break;//采购
                case "07":
                    dep_code = ""; break;//市场
                case "10":
                    dep_code = "SB"; break;//设备
                default:
                    dep_code = "QT"; break;
            }
            string newid = this.CreateCode(dep_code);
            txtPCON_BCODE.Text = newid;
            //}

            string conid = txtPCON_BCODE.Text.Trim();
            DateTime cttime = DateTime.Now;
            if (conid != "")
            {
                LinkLock.Enabled = false;
                string sqltext = "INSERT INTO TBCM_TEMPCONNO (CON_NO,CREATETIME,USER_NAME) VALUES ('" + conid + "','" + cttime + "','" + Session["UserName"].ToString() + "')";
                DBCallCommon.ExeSqlText(sqltext);
                lb_lock.Text = "已锁定";
                lb_lock.ForeColor = System.Drawing.Color.Green;
                lb_addtips.Visible = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('已锁定合同号【" + conid + "】，\\r请在60分钟内完成添加过程，否则取消锁定！');AutoLock();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "js", "alert('请生成正确的合同号再锁定！');", true);
            }
        }

        //删除发票
        //删除前先做判断，对于非销售合同，如果财务已填写收票日期，不能删除
        //如果一定要删除，需要管理员到数据库中找到数据表TBPM_GATHINVDOC删除相应记录,主键GIV_ID
        protected void linkdel_FP_Click(object sender, EventArgs e)
        {
            string giv_id = ((LinkButton)sender).CommandArgument.ToString();

            string sql_check_sp = "SELECT GIV_SPRQ FROM TBPM_GATHINVDOC WHERE GIV_ID='" + giv_id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_check_sp);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() != "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该记录财务已填写收票日期，不能直接删除\\r如果一定要删除，请联系管理员');", true); return;

                }
                else
                {
                    string sql_del = "DELETE FROM TBPM_GATHINVDOC WHERE GIV_ID='" + giv_id + "'";
                    DBCallCommon.ExeSqlText(sql_del);

                    this.BindBillData(condetail_id);
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('删除成功！');", true); return;
                }

            }
        }

        //取消锁定，放弃添加
        protected void btnNO_Click(object sender, EventArgs e)
        {
            string pcon_bcode = txtPCON_BCODE.Text.Trim();
            if (pcon_bcode != "")
            {
                string sqltext = "DELETE FROM TBCM_TEMPCONNO WHERE USER_NAME='" + Session["UserName"].ToString() + "' AND CON_NO='" + pcon_bcode + "'";
                DBCallCommon.ExeSqlText(sqltext);
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('放弃添加\\r合同号【" + pcon_bcode + "】取消锁定！');window.close();", true); return;
            }
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "window.close();", true);
        }

        private void AddStr()
        {
            if (addRep.Items.Count > 0)
            {
                foreach (RepeaterItem retItem in addRep.Items)
                {
                    if (((Label)retItem.FindControl("CM_CONTR")).Text != "")
                    {
                        str.Add(((Label)retItem.FindControl("CM_CONTR")).Text);
                        str = str.Distinct<string>().ToList();
                    }
                }
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (txtPCON_TASK.Text != "")
            {
                int count = addRep.Items.Count;
                AddStr();
                CreateNewRow(Convert.ToInt32(1), count);
            }
        }

        #region 增加行

        private void CreateNewRow(int num, int count) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable(num);
            if (str.IndexOf(txtPCON_TASK.Text) + 1 == str.Count || !str.Contains(txtPCON_TASK.Text))
            {
                for (int i = count; i < num + count; i++)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[1] = txtPCON_TASK.Text;
                    newRow[2] = txtPCON_SCH.Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.addRep.DataSource = dt;
            this.addRep.DataBind();
            InitVar();
        }

        List<string> list = new List<string>() { "ID", "CM_CONTR", "TSA_ID", "CM_CUS", "CM_HEGE", "CM_CONTENT", "CM_MAP", "CM_BUJIAN", "CM_WEIGHT", "CM_COUNT" };
        private DataTable GetDataTable(int num)
        {
            DataTable dt = new DataTable();
            dt = GetTable(dt);
            for (int i = 0; i < addRep.Items.Count; i++)
            {
                RepeaterItem retItem = addRep.Items[i];
                DataRow newRow = dt.NewRow();
                newRow[1] = ((Label)retItem.FindControl("CM_CONTR")).Text;
                newRow[2] = ((Label)retItem.FindControl("TSA_ID")).Text;
                SetNewRow(newRow, retItem);
                dt.Rows.Add(newRow);
                if (str.IndexOf(txtPCON_TASK.Text) + 1 < str.Count && i + 1 < addRep.Items.Count)
                {
                    if (((Label)addRep.Items[i + 1].FindControl("CM_CONTR")).Text == str[str.IndexOf(txtPCON_TASK.Text) + 1].ToString())
                    {
                        for (int j = 0; j < num; j++)
                        {
                            DataRow newRow1 = dt.NewRow();
                            newRow1[1] = txtPCON_TASK.Text;
                            newRow1[2] = txtPCON_SCH.Text;
                            dt.Rows.Add(newRow1);
                        }
                    }
                }
            }
            dt.AcceptChanges();
            return dt;
        }

        protected DataTable GetTable(DataTable dt)
        {
            for (int i = 0; i < list.Count; i++)
            {
                dt.Columns.Add(list[i].ToString());
            }
            return dt;
        }

        private void SetNewRow(DataRow newRow, RepeaterItem retItem)
        {
            for (int i = 3; i < list.Count; i++)
            {
                newRow[i] = ((TextBox)retItem.FindControl(list[i].ToString())).Text;
            }
        }

        private void InitVar()
        {
            if (addRep.Items.Count == 0)
            {
                Panel4.Visible = true;
            }
            else
            {
                Panel4.Visible = false;
                delete.Visible = true;
            }
        }

        #endregion

        protected void delete_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = GetTable(dt);
            AddStr();
            str.Add("");//储存合同号
            foreach (RepeaterItem retItem in addRep.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                string contr = ((Label)retItem.FindControl("CM_CONTR")).Text;
                if (!chk.Checked || contr != "")
                {
                    if (contr != "" && chk.Checked)
                    {
                        str.Remove(contr);
                        str.Remove("");
                    }
                    if (str.Contains(contr))
                    {
                        DataRow newRow = dt.NewRow();
                        if (contr != "")
                        {
                            newRow[1] = ((Label)retItem.FindControl("CM_CONTR")).Text;
                        }
                        SetNewRow(newRow, retItem);
                        dt.Rows.Add(newRow);
                        str.Add("");
                        str = str.Distinct<string>().ToList();
                    }
                }
            }
            this.addRep.DataSource = dt;
            this.addRep.DataBind();
            InitVar();
        }

        List<string> strs = new List<string>();
        protected void addRep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.ToString() != "Header")
            {
                if (str.Count < 1)
                {
                    strs.Add(((DataRowView)e.Item.DataItem).Row[1].ToString());
                }
                else
                {
                    if (strs.Contains(((DataRowView)e.Item.DataItem).Row[1].ToString()))
                    {
                        ((DataRowView)e.Item.DataItem).Row[1] = "";
                        e.Item.DataBind();
                    }
                    else
                    {
                        strs.Add(((DataRowView)e.Item.DataItem).Row[1].ToString());
                    }
                }
            }
        }
    }
}
