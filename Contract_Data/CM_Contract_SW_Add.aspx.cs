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

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_Contract_SW_Add : System.Web.UI.Page
    {
        string condetail_id = "";//修改的合同编号
        string action = "";//Edit or Add
        private decimal kpje = 0; //发票金额汇总
        private decimal hjykje1 = 0;//要款金额汇总
        private decimal hjykje2 = 0;//要款金额汇总
        private decimal hjskje = 0;//收款金额汇总
        List<string> str = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            condetail_id = Request.QueryString["condetail_id"];
            action = Request.QueryString["Action"];
            //Page.DataBind();
            if (!IsPostBack)
            {
                //添加时先创建一个唯一标识，存储在一个lable中，用来关联合同与附件，避免因合同号占用而导致附件关联错误
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
            this.InitLower();

            //初始附件上传控件
            UploadAttachments1.at_htbh = lbl_UNID.Text;
            UploadAttachments1.at_type = 0;
            UploadAttachments1.at_sp = 0;
            //******************************

        }


        /// <summary>
        /// 根据上一页面传递值，初始化页面信息
        /// </summary>
        #region
        private void InitPageInfo()
        {
            ZCZJ_DPF.Contract_Data.ContractClass.UploadControlSet(UploadAttachments1, action);
            lblRemind.Visible = false;

            this.BindDep();
            if (action == "Add") //如果新增
            {
                lblConForm.Text = "销售合同";
                lblAddOREdit.Text = "新增销售合同";
                btnConfirm.Text = "确认添加";
                rblState.SelectedIndex = 1;
                UpdatePanel1.Visible = false; //在合同未添加到数据库时，无法要款
                //palCHG.Visible = false;//变更不可见
                //palJECHG.Visible = false;
                btn_RevInfo.Visible = false;
                ViewState["qt_chg"] = 0;
                this.CreateCode();//创建合同编号
                this.LinkLock_Click(null, null);//自动锁定
                txtPCON_RESPONSER.Text = Session["UserName"].ToString();
            }
            else if (action == "Edit")
            {
                lblConForm.Text = "销售合同";
                lblConID.Text = "--" + condetail_id;
                lblAddOREdit.Text = "修改销售合同";
                btnConfirm.Text = "确认修改";
                this.BindContractDetail(condetail_id);
                string tsaidvalue0 = string.Empty;
                if (addRep.Items.Count > 0)
                {
                    foreach (RepeaterItem rptItem in addRep.Items)
                    {
                        if (((ITextControl)rptItem.FindControl("TSA_ID")).Text != "")
                        {
                            tsaidvalue0 += "'" + ((ITextControl)rptItem.FindControl("TSA_ID")).Text + "',";
                        }
                    }
                    tsaidvalue0 = tsaidvalue0.Substring(0, tsaidvalue0.Length - 1);
                    string shvalidsql = string.Format("select TSA_ID from TBCM_CHANGE where TSA_ID in ({0}) and (CH_ID in (select CH_ID from TBCM_CHANLIST where CM_STATE='1') or ID in (select ID from TBCM_CHANLIST where CM_STATE='1'))", tsaidvalue0);
                    DataTable shvaliddt = DBCallCommon.GetDTUsingSqlText(shvalidsql);
                    if (shvaliddt.Rows.Count > 0)
                    {
                        TipsError.Visible = true;
                    }
                }
            }
            else if (action == "View")// View
            {
                lblConForm.Text = "销售合同";
                lblConID.Text = "--" + condetail_id;
                lblAddOREdit.Text = "查看销售合同";
                btnConfirm.Visible = false;
                this.BindContractDetail(condetail_id);
                foreach (Control item in addRep.Controls)
                {
                    foreach (Control ctrl in item.Controls)
                    {
                        if (ctrl is TextBox)
                        {
                            ((TextBox)ctrl).ReadOnly = true;
                        }
                    }
                }
                //变更
                //palCHG.Enabled = false;
                //btnJECHG.Disabled = true;

                //基本只能查看
                //palHT.Enabled = false;
                this.ControlsEnable();
                //不可要款
                btnADDCR.Visible = false;
                //不可添加发票
                btnAddFP.Visible = false;
                //待确认要款不可修改
                grvDBSK.Columns[grvDBSK.Columns.Count - 1].Visible = false;
                //发票不可修改/删除
                grvFP.Columns[grvFP.Columns.Count - 3].Visible = false;
                grvFP.Columns[grvFP.Columns.Count - 1].Visible = false;
                //要款不可删除
                grvYKJL.Columns[grvYKJL.Columns.Count - 1].Visible = false;
                //待办要款
                grvYBYK.Columns[grvYBYK.Columns.Count - 2].Visible = false;
                //无法添加补充协议
                add_bcxy.Visible = false;
            }

        }

        //控制编辑者权限
        private void Control_EditPower()
        {
            if (Session["UserName"].ToString() != txtPCON_RESPONSER.Text.Trim())//不是负责人
            {

                //palCHG.Enabled = false;//不能进行金额变更和其他变更，只能修改少数字段
                ZCZJ_DPF.Contract_Data.ContractClass.UploadControlSet(UploadAttachments1, "View");

            }
        }

        //控件状态控制
        private void ControlsEnable()
        {
            //合同编号
            //txtPCON_BCODE.Enabled = false;
            //主业合同号
            //txt_YZHTH.Enabled = false;
            //合同名称
            //txtPCON_NAME.Enabled = false;
            //项目编号
            txtPCON_PJID.Enabled = false;
            //项目名称
            //tb_pjinfo.Enabled = false;
            //责任部门
            dplPCON_RSPDEPID.Enabled = false;
            //工程名称
            txtPCON_ENGNAME.Enabled = false;
            //合同类别
            txtPCON_TYPE.Enabled = false;
            //设备类型
            //ddl_engtype.Enabled = false;
            //txtPCON_ENGTYPE.Enabled = false;
            //合同金额
            //txtPCON_JINE.Enabled = false;
            //货币单位
            txtPCON_VALIDDATE.Enabled = false;
            //签订日期
            txtPCON_FILLDATE.Enabled = false;
            //生效日期
            txtPCON_VALIDDATE.Enabled = false;
            //客户名称
            txtPCON_CUSTMNAME.Enabled = true;
            //客户编号

            //负责人
            txtPCON_RESPONSER.Enabled = false;
            //结算金额
            txtPCON_BALANCEACNT.Enabled = false;
            //交货日期
            //txtPCON_DELIVERYDATE.Enabled=false;
            //计入成本
            //txtPCON_COST.Enabled = false;
            //生产制号
            txtPCON_SCH.Enabled = false;
            //备注
            txtPCON_NOTE.Enabled = false;
            //合同类别，不可修改 0、1、2、3、4
            //合同状态
            rblState.Enabled = false;
            //发货时间
            //txt_FHSJ.Enabled = false;
        }
        /// <summary>
        /// 绑定某一合同详细信息
        /// </summary>
        /// <param name="contractid"></param>
        private void BindContractDetail(string contractid)
        {
            ViewState["qt_chg"] = 0;
            string sqlStr = "select * from TBPM_CONPCHSINFO where PCON_BCODE='" + contractid + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlStr);
            if (dr.HasRows)
            {
                dr.Read();
                //唯一编号
                lbl_UNID.Text = dr["GUID"].ToString();
                //合同编号
                txtPCON_BCODE.Text = dr["PCON_BCODE"].ToString();
                Hid_Contr.Value = dr["PCON_BCODE"].ToString();
                //业主合同号
                txt_YZHTH.Text = dr["PCON_YZHTH"].ToString();
                //txtPCON_BCODE.Enabled = false;
                //合同名称
                //txtPCON_NAME.Text = dr["PCON_NAME"].ToString();
                //项目编号
                txtPCON_PJID.Text = dr["PCON_PJID"].ToString();
                //项目名称
                //tb_pjinfo.Text = dr["PCON_PJNAME"].ToString();
                //责任部门
                dplPCON_RSPDEPID.ClearSelection();
                foreach (ListItem li in dplPCON_RSPDEPID.Items)
                {
                    if (li.Value.ToString() == dr["PCON_RSPDEPID"].ToString())
                    {
                        li.Selected = true; break;
                    }
                }
                //工程名称
                txtPCON_ENGNAME.Text = dr["PCON_ENGNAME"].ToString();
                //合同类别
                txtPCON_TYPE.Text = dr["PCON_TYPE"].ToString();
                //设备类型
                txtPCON_ENGTYPE.Text = dr["PCON_ENGTYPE"].ToString();
                //合同金额
                txtPCON_JINE.Text = dr["PCON_JINE"].ToString();
                //txtHTJE.Text = dr["PCON_JINE"].ToString();//变更
                //Other_MONUNIT.Text = dr["OTHER_MONUNIT"].ToString();
                //货币单位
                //ddl_PCONMONUNIT.SelectedValue = dr["PCON_MONUNIT"].ToString();

                //签订日期
                txtPCON_FILLDATE.Text = dr["PCON_FILLDATE"].ToString();
                //生效日期
                txtPCON_VALIDDATE.Text = dr["PCON_VALIDDATE"].ToString();
                //客户名称
                txtPCON_CUSTMNAME.Text = dr["PCON_CUSTMNAME"].ToString();
                //客户编号

                //负责人
                txtPCON_RESPONSER.Text = dr["PCON_RESPONSER"].ToString();
                //结算金额
                txtPCON_BALANCEACNT.Text = dr["PCON_BALANCEACNT"].ToString();
                //交货日期
                //txtPCON_DELIVERYDATE.Text =dr["PCON_DELIVERYDATE"].ToString();
                //计入成本
                //txtPCON_COST.Text = dr["PCON_COST"].ToString();
                //生产制号
                //txtPCON_SCH.Text = dr["PCON_SCH"].ToString();
                //备注
                txtPCON_NOTE.Text = dr["PCON_NOTE"].ToString();
                //最后一批发货时间
                //txt_FHSJ.Text = dr["PCON_FHSJ"].ToString();
                //合同类别，不可修改 0、1、2、3、4
                //合同状态
                foreach (ListItem li in rblState.Items)
                {
                    if (li.Value.ToString() == dr["PCON_STATE"].ToString())
                    {
                        li.Selected = true;
                        break;
                    }
                }
                if (action == "Edit")
                {
                    if (rblState.SelectedValue.ToString() == "1" || rblState.SelectedValue.ToString() == "2")
                    {
                        rblState.Items[0].Enabled = false;//如果项目已进行,或完成
                    }
                }
                //合同金额变更
                //btnJECHG.Value = "金额变更(" + dr["PCON_JECHG"].ToString() + ")";
                //其它变更
                //btnQTCHG.Text = "其它变更(" + dr["PCON_QTCHG"].ToString() + ")";

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
                else
                {
                    lbl_pszt.Text = "审批通过";
                    btn_RevInfo.Visible = false;
                }
                btn_RevInfo.Visible = lbl_pszt.Text.Trim() == "" ? false : true;
                dr.Close();
            }
            this.InitControlUseState(rblState.SelectedValue.ToString());
            sqlStr = "select * from TBPM_DETAIL where CM_CONTR='" + contractid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlStr);
            if (dt.Rows.Count > 0)
            {
                string taskId = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    taskId += ",'" + dt.Rows[i]["TSA_ID"] + "'";
                }
                taskId = taskId.Substring(1);
                //sqlStr = string.Format("select TSA_ID,CM_PROJ,CM_YZHTH,CM_ENGNAME,CM_MATERIAL,CM_MAP,CM_PRICE,CM_COUNT,CM_NUMBER,CM_UNIT,CM_WEIGHT,CM_ALL,CM_SIGN,CM_JIAO,CM_DUTY,CM_NOTE,CM_DTSJ,CM_CPRK,CM_TZFH,CM_CKSJ,CM_DFSM,CM_JHDTYPE,row_number()over(partition by TSA_ID order by TSA_ID)c from TBPM_DETAIL where CM_CONTR='{0}'and (CM_JHDTYPE IS NULL OR CM_JHDTYPE<>'2')union all select TSA_ID,CM_PROJ,'' as CM_YZHTH, TSA_ENGNAME as CM_ENGNAME,TSA_MAP as CM_MAP,TSA_MATERIAL as CM_MATERIAL,'0' as CM_PRICE,'0' as CM_COUNT,TSA_NUMBER as CM_NUMBER,TSA_UNIT as CM_UNIT,'' as CM_WEIGHT,'' as CM_ALL,'' as CM_SIGN,'' as CM_JIAO,'' as CM_DUTY,'' as CM_NOTE,'' as CM_DTSJ,'' as CM_CPRK,''as CM_TZFH,'' as CM_CKSJ,'' as CM_DFSM,'' as CM_JHDTYPE,'' as c from View_CM_Task where CM_CONTR='{0}' and TSA_ID not in ({1}) and CM_PSYJ2!='3' and CM_CANCEL<>'1' order by CM_JHDTYPE", condetail_id, taskId);
                sqlStr = string.Format("select a.TSA_ID,a.CM_PROJ,a.CM_YZHTH,a.CM_ENGNAME,a.CM_MATERIAL,a.CM_MAP,a.CM_PRICE,a.CM_COUNT,a.CM_NUMBER,a.CM_UNIT,a.CM_WEIGHT,a.CM_ALL,a.CM_SIGN,a.CM_JIAO,a.CM_DUTY,a.CM_NOTE,a.CM_DTSJ,a.CM_CPRK,a.CM_TZFH,a.CM_CKSJ,a.CM_DFSM,a.CM_JHDTYPE,c.jhdtype from (select  top 100 percent TSA_ID,min(CM_JHDTYPE)as jhdtype from TBPM_DETAIL group by TSA_ID order by jhdtype asc)c inner join TBPM_DETAIL as a on a.TSA_ID=c.TSA_ID where a.CM_CONTR='{0}'and (a.CM_JHDTYPE IS NULL OR a.CM_JHDTYPE<>'4') union all select TSA_ID,CM_PROJ,'' as CM_YZHTH, TSA_ENGNAME as CM_ENGNAME,TSA_MAP as CM_MAP,TSA_MATERIAL as CM_MATERIAL,'0' as CM_PRICE,'0' as CM_COUNT,TSA_NUMBER as CM_NUMBER,TSA_UNIT as CM_UNIT,'' as CM_WEIGHT,'' as CM_ALL,'' as CM_SIGN,'' as CM_JIAO,'' as CM_DUTY,'' as CM_NOTE,'' as CM_DTSJ,'' as CM_CPRK,''as CM_TZFH,'' as CM_CKSJ,'' as CM_DFSM,'' as CM_JHDTYPE,'' as jhdtype from View_CM_Task where CM_CONTR='{0}' and TSA_ID not in ({1}) and CM_PSYJ2!='3' and CM_CANCEL<>'1' order by jhdtype asc,TSA_ID,CM_JHDTYPE", condetail_id, taskId);
            }
            else
            {
                sqlStr = "select TSA_ID,CM_PROJ,'' as CM_YZHTH ,TSA_ENGNAME as CM_ENGNAME,TSA_MAP as CM_MAP,TSA_MATERIAL as CM_MATERIAL,'' as CM_PRICE,'' as CM_COUNT,TSA_NUMBER as CM_NUMBER,TSA_UNIT as CM_UNIT,'' as CM_WEIGHT,'' as CM_ALL,'' as CM_SIGN,'' as CM_JIAO,'' as CM_DUTY,'' as CM_NOTE,'' as CM_DTSJ,'' as CM_CPRK,''as CM_TZFH,'' as CM_CKSJ,'' as CM_DFSM,'' as CM_JHDTYPE from View_CM_Task where CM_CONTR='" + condetail_id + "'and CM_SPSTATUS='2' and (CM_CANCEL IS NULL or CM_CANCEL <> 1)";
            }
            addRep.DataSource = DBCallCommon.GetDTUsingSqlText(sqlStr);
            addRep.DataBind();
            InitVar();
            CalcuHZ();
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

        //责任部门绑定
        public void BindDep()
        {
            string sqltext = "select DEP_NAME,DEP_CODE from  TBDS_DEPINFO where DEP_FATHERID='0'and DEP_CODE!='01'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            dplPCON_RSPDEPID.DataSource = dt;
            dplPCON_RSPDEPID.DataTextField = "DEP_NAME";
            dplPCON_RSPDEPID.DataValueField = "DEP_CODE";
            dplPCON_RSPDEPID.DataBind();
            dplPCON_RSPDEPID.Items.Insert(0, new ListItem("-请选择-", "%"));
            dplPCON_RSPDEPID.SelectedIndex = 0;
        }
        //选择的项目改变，对应项目编号改变

        protected void tb_pjinfo_Textchanged(object sender, EventArgs e)
        {
            //选择的项目改变，对应项目编号改变，同时重新绑定工程称           
            if (txtPCON_PJID.Text.Trim() != "")
            {

                txtPCON_ENGNAME.Text = "";
            }
        }

        //合同编号创建
        private void CreateCode()
        {
            #region Old_code
            //string id = "ZCZJ.SW.";
            //id += DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + ".";
            //string sqlText = "select Top 1 PCON_BCODE from TBPM_CONPCHSINFO where PCON_BCODE like '" + id + "%' AND PCON_FORM='0' Order by PCON_BCODE desc";//找出当天的最大合同号
            //SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            //if (dr.HasRows)
            //{
            //    dr.Read();
            //    string[] bh = dr["PCON_BCODE"].ToString().Split('.');
            //    int maxID = Convert.ToInt16(bh[bh.Length - 1]) + 1;//取数组最后一个
            //    dr.Close();
            //    id += maxID.ToString();
            //}
            //else
            //{
            //    id += "1";
            //}
            //txtPCON_BCODE.Text = id;
            //lblConID.Text = "--" + id;
            #endregion

            #region New_Code TECTJ*****
            string id = "TECTJ";
            id += DateTime.Now.Year.ToString().Substring(2);

            string sqlText = "SELECT TOP 1 PCON_BCODE FROM (SELECT PCON_BCODE FROM TBPM_CONPCHSINFO UNION SELECT CON_NO AS PCON_BCODE FROM TBCM_TEMPCONNO ) AS A " +
                " WHERE PCON_BCODE LIKE '" + id + "%'  ORDER BY PCON_BCODE DESC";//找出该类合同的最大合同号
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.HasRows)
            {
                dr.Read();
                string bh = dr["PCON_BCODE"].ToString().Substring(7);
                int maxID = Convert.ToInt16(bh.Substring(0, 3)) + 1;
                dr.Close();
                id += maxID.ToString().PadLeft(3, '0');
            }
            else
            {
                id += "001";
            }
            txtPCON_BCODE.Text = id;
            lblConID.Text = "--" + id;
            #endregion
        }
        /// <summary>
        /// 执行SQL语句，包括添加和修改
        /// </summary>
        private void ExecSQL()
        {
            string sqltext = "";
            //全球唯一标识符
            string guid = lbl_UNID.Text.Trim();
            //合同编号1
            string pcon_bcode = txtPCON_BCODE.Text.Trim();
            //业主合同号
            string pcon_yzhth = txt_YZHTH.Text.Trim();
            //合同名称2
            //string pcon_name = txtPCON_NAME.Text.Trim();
            //项目编号3
            string pcon_pjid = txtPCON_PJID.Text.Trim();
            //项目名称4
            //string pcon_pjname = tb_pjinfo.Text;
            //责任部门5
            string pcon_rspdepid = dplPCON_RSPDEPID.SelectedValue.ToString();
            //工程编号6
            string pcon_engid = "";//销售合同不存在工程编号，为空
            //工程名称7
            string pcon_engname = txtPCON_ENGNAME.Text.Trim();
            //合同类别8
            string pcon_type = txtPCON_TYPE.Text.Trim();
            //设备类型9
            string pcon_engtype = txtPCON_ENGTYPE.Text.Trim();
            //合同金额10
            decimal pcon_jine = Convert.ToDecimal(txtPCON_JINE.Text.Trim());
            //其他金额
            //decimal other_monunit = Convert.ToDecimal(Other_MONUNIT.Text.Trim());
            //货币单位11
            //string pcon_monunit = other_monunit == 0 ? "" : ddl_PCONMONUNIT.SelectedItem.Text;
            //签订日期12
            string pcon_filldate = txtPCON_FILLDATE.Text.ToString();
            //生效日期13
            string pcon_validdate = txtPCON_VALIDDATE.Text.ToString();
            //客户名称14
            string pcon_custmname = txtPCON_CUSTMNAME.Text.ToString().Trim();
            //客户编号15
            string pcon_custmid = "";//为空           
            //负责人18
            string pcon_responser = txtPCON_RESPONSER.Text.Trim();

            //结算金额21
            string js = txtPCON_BALANCEACNT.Text.Trim() == "" ? "0" : txtPCON_BALANCEACNT.Text.Trim();
            double pcon_balanceacnt = Convert.ToDouble(js);

            //交货日期23
            //string pcon_deliverydate = txtPCON_DELIVERYDATE.Text.Trim();
            //计入成本24
            //string pcon_cost = txtPCON_COST.Text.Trim();
            //备注25
            string pcon_note = txtPCON_NOTE.Text.Trim();
            //合同类型26
            string pcon_form = "0";//合同的类别0、1、2、3、4
            //合同状态27
            int pcon_state = Convert.ToInt16(rblState.SelectedValue.ToString());
            //已付款28
            decimal pcon_yfk = 0;
            //应付款累计29
            decimal pcon_yfklj = 0;
            //实付款累计30
            decimal pcon_sfklj = 0;
            //应付尾款31 -暂不用
            decimal pcon_yfwk = pcon_jine;
            //异常标识32
            //添加时正常，标识的修改通过【合同索赔】界面
            int pcon_error = 0;//0:正常；1：异常
            //其他变更
            int qt_chg = Convert.ToInt16(ViewState["qt_chg"].ToString());
            //最后一批发货时间
            //string pcon_fhsj = txt_FHSJ.Text.ToString();
            //合同审批单号
            string pcon_revid = tb_revid.Text.Trim();
            //生产制号
            //string pcon_sch = txtPCON_SCH.Text.Trim();
            List<string> list_sql = new List<string>();
            //检查是否多人同时操作造成合同号重码，如果重码，则重新生成合同号
            if (pcon_bcode != Hid_Contr.Value)
            {
                string check_code = " select * from TBPM_CONPCHSINFO where PCON_BCODE='" + pcon_bcode + "'";
                DataTable dt_check = DBCallCommon.GetDTUsingSqlText(check_code);
                if (dt_check.Rows.Count > 0)
                {
                    pcon_bcode = Hid_Contr.Value;
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('合同号【" + pcon_bcode + "】已经存在\\r\\r此合同号自动修改为【" + Hid_Contr.Value + "】\\r\\r请同步更新合同文本中的合同号再保存！');", true); return;
                }
            }
            if (action == "Add")
            {
                sqltext = "insert into TBPM_CONPCHSINFO(PCON_BCODE,PCON_PJID,PCON_RSPDEPID,PCON_ENGID,PCON_ENGNAME,PCON_TYPE,PCON_ENGTYPE,PCON_JINE,PCON_FILLDATE,PCON_VALIDDATE,PCON_CUSTMNAME,PCON_CUSTMID,PCON_RESPONSER,PCON_BALANCEACNT,PCON_NOTE,PCON_FORM,PCON_STATE,PCON_YFK,PCON_YFKLJ,PCON_SFKLJ,PCON_YFWK,PCON_ERROR,PCON_YZHTH,PCON_REVID,GUID)" +
                    "values('" + pcon_bcode + "','" + pcon_pjid + "','" + pcon_rspdepid + "','" + pcon_engid + "','" + pcon_engname + "','" + pcon_type + "','" + pcon_engtype + "','" + pcon_jine + "','" + pcon_filldate + "','" + pcon_validdate + "','" + pcon_custmname + "','" + pcon_custmid + "','" + pcon_responser + "','" + pcon_balanceacnt + "','" + pcon_note + "','" + pcon_form + "'," + pcon_state + "," + pcon_yfk + "," + pcon_yfklj + "," + pcon_sfklj + "," + pcon_yfwk + "," + pcon_error + ",'" + pcon_yzhth + "','" + pcon_revid + "','" + guid + "')";
                list_sql.Add(sqltext);
            }
            else if (action == "Edit")
            {
                sqltext = "update TBPM_CONPCHSINFO set PCON_BCODE='" + pcon_bcode + "',PCON_PJID='" + pcon_pjid + "',PCON_RSPDEPID='" + pcon_rspdepid + "',PCON_ENGID='" + pcon_engid + "',PCON_ENGNAME='" + pcon_engname + "',PCON_TYPE='" + pcon_type + "',PCON_ENGTYPE='" + pcon_engtype + "',PCON_JINE='" + pcon_jine + "',PCON_FILLDATE='" + pcon_filldate + "',PCON_VALIDDATE='" + pcon_validdate + "',PCON_CUSTMNAME='" + pcon_custmname + "',PCON_CUSTMID='" + pcon_custmid + "',PCON_RESPONSER='" + pcon_responser + "',PCON_BALANCEACNT='" + pcon_balanceacnt + "',PCON_NOTE='" + pcon_note + "',PCON_FORM='" + pcon_form + "',PCON_STATE='" + pcon_state + "',PCON_QTCHG=PCON_QTCHG+" + qt_chg + ",PCON_YZHTH='" + pcon_yzhth + "',PCON_REVID='" + pcon_revid + "'" +
                    " where PCON_BCODE='" + Hid_Contr.Value + "'";
                list_sql.Add(sqltext);
                sqltext = "delete from TBPM_DETAIL where CM_CONTR='" + pcon_bcode + "'";
                list_sql.Add(sqltext);
            }
            for (int i = 0; i < addRep.Items.Count; i++)
            {
                List<string> val = new List<string>();
                RepeaterItem retItem = addRep.Items[i];
                string contr = ((ITextControl)retItem.FindControl("TSA_ID")).Text.Trim();
                if (contr == "")
                {
                    ((ITextControl)retItem.FindControl("TSA_ID")).Text = ((ITextControl)addRep.Items[i - 1].FindControl("TSA_ID")).Text;
                    ((TextBox)retItem.FindControl("TSA_ID")).Visible = false;
                    contr = ((ITextControl)addRep.Items[i - 1].FindControl("TSA_ID")).Text.Trim();
                }
                val.Add(pcon_bcode);
                val.Add(contr);
                SetVal(val, retItem);
                string value = string.Empty;
                for (int j = 0; j < val.Count; j++)
                {
                    value += "'" + val[j] + "',";
                }
                value = value.Substring(0, value.Length - 1);
                value = value.Substring(0, value.LastIndexOf(','));
                string CM_JHDTYPE = ((TextBox)addRep.Items[i].FindControl("CM_JHDTYPE")).Text.ToString();
                if (CM_JHDTYPE == "-1")
                {
                    value += ",'-1'";
                }
                else if (CM_JHDTYPE == "1")
                {
                    value += ",'1'";
                }
                else if (CM_JHDTYPE == "2")
                {
                    value += ",'2'";
                }
                else if (CM_JHDTYPE == "3")
                {
                    value += ",'3'";
                }
                else if (CM_JHDTYPE == "4")
                {
                    value += ",'4'";
                }
                else
                {
                    value += ",'0'";
                }
                sqltext = "insert into TBPM_DETAIL values(" + value + ");";
                list_sql.Add(sqltext);
            }
            DBCallCommon.ExecuteTrans(list_sql);//执行事务
            lblRemind.Visible = true;
            btnConfirm.Visible = false;
            if (action == "Edit")
            {
                //this.InitControlUseState(rblState.SelectedValue.ToString());
                //this.BindContractDetail(condetail_id);
                //this.InitLower();
            }
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('操作成功！');window.opener.location.reload();;window.close();", true);
        }

        private void SetVal(List<string> val, RepeaterItem retItem)
        {
            for (int i = 1; i < list.Count; i++)
            {
                val.Add(((TextBox)retItem.FindControl(list[i].ToString())).Text.Trim());
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            //检查业主是否在基础数据中存在
            string sqlyezhu = "select * from TBCS_CUSUPINFO where (CS_NAME='" + txtPCON_CUSTMNAME.Text.Trim() + "' or CS_HRCODE='" + txtPCON_CUSTMNAME.Text.Trim() + "') and CS_TYPE='1'";
            DataTable dtyezhu = DBCallCommon.GetDTUsingSqlText(sqlyezhu);
            if (dtyezhu.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('业主名称不存在于基础数据客户表中！！！');", true);
                return;
            }

            //检查必填项
            //if (txtPCON_PJID.Text.Trim() == "")
            //{

            //    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择项目！！');", true);
            //    return;
            //}

            if (dplPCON_RSPDEPID.SelectedIndex == 0)
            {

                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择责任部门！！');", true);
                return;
            }

            //if (Convert.ToDecimal(txtPCON_JINE.Text) == 0)
            //{

            //    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写合同金额！！');", true);
            //    return;
            //}

            //if (txtPCON_DELIVERYDATE.Text.Trim() == "")
            //{

            //    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('交货日期不能为空！！');", true);
            //    return;
            //}

            if (txtPCON_RESPONSER.Text.Trim() == "")
            {

                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('负责人不能为空！！');", true);
                return;

            }
            string tsaidvalue1 = string.Empty;
            if (addRep.Items.Count > 0)
            {
                foreach (RepeaterItem rptItem in addRep.Items)
                {
                    if (((ITextControl)rptItem.FindControl("TSA_ID")).Text != "")
                    {
                        tsaidvalue1 += "'" + ((ITextControl)rptItem.FindControl("TSA_ID")).Text + "',";
                    }
                }
                tsaidvalue1 = tsaidvalue1.Substring(0, tsaidvalue1.Length - 1);
                string shvalidsql = string.Format("select TSA_ID from TBCM_CHANGE where TSA_ID in ({0}) and (CH_ID in (select CH_ID from TBCM_CHANLIST where CM_STATE='1') or ID in (select ID from TBCM_CHANLIST where CM_STATE='1'))", tsaidvalue1);
                DataTable shvaliddt = DBCallCommon.GetDTUsingSqlText(shvalidsql);
                if (shvaliddt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('此合同还有增补项和变更项未经审批，请等待审批完成再进行修改！');", true);
                    return;
                }
            }
            this.ExecSQL();
        }
        //返回
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "window.close();", true);
        }

        //根据合同的状态对控件的可用性进行控制
        private void InitControlUseState(string state)
        {
            switch (state)
            {
                case "0"://未开始
                    //palCHG.Enabled = false;//变更
                    // btnJECHG.Disabled = true;
                    UpdatePanel1.Visible = false;
                    break;
                case "1"://进行中
                    //palCHG.Visible = true;//变更
                    //业主合同号
                    //txt_YZHTH.Enabled = false;
                    //项目名称
                    //tb_pjinfo.Enabled = false;
                    //工程名称
                    //txtPCON_ENGNAME.Enabled = false;
                    //责任部门
                    dplPCON_RSPDEPID.Enabled = false;
                    //设备类型
                    //ddl_engtype.Enabled = false;
                    //txtPCON_ENGTYPE.Enabled = false;
                    //合同类别
                    //txtPCON_TYPE.Enabled = false;
                    //生效日期
                    //txtPCON_VALIDDATE.Enabled = false;
                    //合同名称
                    //txtPCON_NAME.Enabled = false;
                    //合同金额
                    //txtPCON_JINE.Enabled = false;
                    //签订日期
                    //txtPCON_FILLDATE.Enabled = true;
                    //供货商
                    //txtPCON_CUSTMNAME.Enabled = true;
                    //生产制号
                    //txtPCON_SCH.Enabled = false;
                    //交货日期
                    //txtPCON_DELIVERYDATE.Enabled = false;

                    //负责人
                    txtPCON_RESPONSER.Enabled = false;
                    break;
                case "2": //已完成
                    //变更记录
                    //palCHG.Enabled = false;
                    //btnJECHG.Disabled = true;

                    this.ControlsEnable();
                    //要款
                    btnADDCR.Visible = false;
                    //发票
                    btnAddFP.Visible = false;
                    //附件上传
                    UploadAttachments1.FindControl("btnUpload").Visible = false;
                    GridView gridView1 = (GridView)UploadAttachments1.FindControl("GridView1");
                    gridView1.Columns[4].Visible = false;
                    break;
                default:
                    break;
            }
        }
        //**************************************************UpdatePanel**********************************************
        /// <summary>
        /// 初始化UpdatePanel
        /// </summary>
        private void InitLower()
        {
            this.BindYKRecord(condetail_id);
            this.BindYK_NeedCfrm(condetail_id);
            this.BindYK_Confirmed(condetail_id);
            this.BindFP_Record(condetail_id);
            this.BindBCXYData(condetail_id);
        }

        /// <summary>
        /// 绑定要款记录
        /// </summary>
        private void BindYKRecord(string contractid)
        {
            string sqlstr = "select a.BP_ID,a.BP_KXMC,a.BP_JE,a.BP_YKRQ,a.BP_STATE,b.PCON_YFKBL,b.PCON_YFK,b.PCON_JINE,BP_NOTEFST from TBPM_BUSPAYMENTRECORD a,TBPM_CONPCHSINFO b where a.BP_HTBH='" + contractid + "' and b.PCON_BCODE='" + contractid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlstr);
            if (dt.Rows.Count > 0)
            {
                grvYKJL.DataSource = dt;
                grvYKJL.DataBind();
                palSWYK.Visible = false;
            }
            else
            {
                grvYKJL.DataSource = null;
                grvYKJL.DataBind();
                palSWYK.Visible = true;
            }
            if (dt.Rows.Count > 0)
            {
                //已付金额
                lblYFJE.Text = dt.Rows[0]["PCON_YFK"].ToString();
                //合同金额
                lblHTJE.Text = dt.Rows[0]["PCON_JINE"].ToString();
                //支付比例

                lblZFBL.Text = dt.Rows[0]["PCON_YFKBL"].ToString() + "%";
            }
            else
            {
                string sqlstr1 = "select b.PCON_YFK,b.PCON_MONUNIT,b.PCON_JINE from TBPM_CONPCHSINFO b where PCON_BCODE='" + contractid + "'";
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqlstr1);
                if (dt1.Rows.Count > 0)
                {
                    //已付金额
                    lblYFJE.Text = dt1.Rows[0]["PCON_YFK"].ToString();
                    //合同金额
                    lblHTJE.Text = dt1.Rows[0]["PCON_JINE"].ToString();
                    //支付比例
                    string bl = string.Format("{0:N4}", Convert.ToDouble(lblYFJE.Text) / Convert.ToDouble(Convert.ToDouble(lblHTJE.Text) == 0 ? "1" : lblHTJE.Text)).ToString();
                    lblZFBL.Text = (Convert.ToDouble(bl) * 100).ToString() + "%";
                }
            }

        }

        //删除要款，删除之前要判断是否已收款，如果已收款，不允许删除，须财务修改收款金额为0，再删除
        protected void Lbtn_Del_OnClick(object sender, EventArgs e)
        {
            List<string> sqlstr = new List<string>();
            string bp_id = ((LinkButton)sender).CommandArgument.ToString();
            string sql_delyk = "SELECT BP_SFJE FROM TBPM_BUSPAYMENTRECORD WHERE BP_ID='" + bp_id + "'";
            DataTable dt_delyk = DBCallCommon.GetDTUsingSqlText(sql_delyk);
            if (dt_delyk.Rows.Count > 0)
            {
                if (Convert.ToDouble(dt_delyk.Rows[0][0].ToString()) != 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该记录财务已确认收款，不能删除！');", true);
                    return;
                }
                else
                {
                    string str_del = "delete from TBPM_BUSPAYMENTRECORD WHERE BP_ID='" + bp_id + "'";
                    sqlstr.Add(str_del);
                    DBCallCommon.ExecuteTrans(sqlstr);
                    InitLower();
                }
            }
        }
        /// <summary>
        /// 待确认的要款记录
        /// </summary>
        private void BindYK_NeedCfrm(string contractid)
        {
            string sqlstr = "select a.BP_ID,a.BP_KXMC,a.BP_JE,a.BP_YKRQ,b.PCON_YFK,b.PCON_JINE from TBPM_BUSPAYMENTRECORD a,TBPM_CONPCHSINFO b where a.BP_HTBH='" + contractid + "' and b.PCON_BCODE='" + contractid + "' and a.BP_STATE=0";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlstr);
            if (dt.Rows.Count > 0)
            {
                grvDBSK.DataSource = dt;
                grvDBSK.DataBind();
                palDQRYK.Visible = false;
            }
            else
            {
                grvDBSK.DataSource = null;
                grvDBSK.DataBind();
                palDQRYK.Visible = true;
            }
        }
        /// <summary>
        /// 已确认的要款记录
        /// </summary>
        private void BindYK_Confirmed(string contractid)
        {
            string sqlstr = "select a.BP_ID,a.BP_KXMC,a.BP_JE,a.BP_SFJE,a.BP_YKRQ,a.BP_SKRQ,a.BP_PZ,a.BP_PZH,a.BP_STATE,b.PCON_YFK,b.PCON_JINE from TBPM_BUSPAYMENTRECORD a,TBPM_CONPCHSINFO b where a.BP_HTBH='" + contractid + "' and b.PCON_BCODE='" + contractid + "' and a.BP_STATE=1";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlstr);
            if (dt.Rows.Count > 0)
            {
                grvYBYK.DataSource = dt;
                grvYBYK.DataBind();
                palYKJL.Visible = false;
            }
            else
            {
                grvYBYK.DataSource = null;
                grvYBYK.DataBind();
                palYKJL.Visible = true;
            }
        }
        /// <summary>
        /// 发票记录
        /// </summary>
        private void BindFP_Record(string contractid)
        {
            string sqlstr = "select BR_ID,BR_HTBH,BR_KPRQ,BR_KPJE,BR_PZ,BR_SL from TBPM_BUSBILLRECORD where BR_HTBH='" + contractid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlstr);
            if (dt.Rows.Count > 0)
            {
                grvFP.DataSource = dt;
                grvFP.DataBind();
                palFPJL.Visible = false;
            }
            else
            {
                grvFP.DataSource = null;
                grvFP.DataBind();
                palFPJL.Visible = true;
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
        //***********************************************************************************************************

        //提示信息是否显示
        protected void rblState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblState.SelectedValue.ToString() == "2")
            {

                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('完成状态下不能修改合同所有信息！\\r\\r请确认！！');", true);
            }
        }

        //选择设备类型
        protected void ddl_engtype_Changed(object sender, EventArgs e)
        {
            if (txtPCON_ENGTYPE.Text.ToString().Contains(ddl_engtype.SelectedItem.Text.ToString()))
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('重复选择！！');", true);
                ddl_engtype.SelectedIndex = 0;
                return;
            }
            else
            {
                txtPCON_ENGTYPE.Text += (txtPCON_ENGTYPE.Text.Trim() == "" ? "" : "、") + ddl_engtype.SelectedItem.Text.ToString();
                ddl_engtype.SelectedIndex = 0;
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

        //发票汇总
        protected void grvFP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                kpje += decimal.Parse(e.Row.Cells[3].Text);//开票金额
                e.Row.Attributes["style"] = "Cursor:pointer";
                e.Row.Attributes.Add("onclick", "RowClick(this)");
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "汇总：";
                e.Row.Cells[3].Text = kpje.ToString();

            }
        }

        //要款汇总
        protected void grvYK_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                hjykje1 += decimal.Parse(e.Row.Cells[3].Text);//要款金额                
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "汇总：";
                e.Row.Cells[3].Text = hjykje1.ToString();

            }
        }

        //收款汇总
        protected void grvSK_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                hjykje2 += decimal.Parse(e.Row.Cells[3].Text);//要款金额
                // hjskje += decimal.Parse(((Label)e.Row.FindControl("lbl_SFJE")).Text.Trim());//收款金额
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[2].Text = "汇总：";
                e.Row.Cells[3].Text = hjykje2.ToString();
                // e.Row.Cells[4].Text = hjskje.ToString();
            }
        }

        //锁定当前合同号15分钟
        protected void LinkLock_Click(object sender, EventArgs e)
        {
            /*******锁定前应该先在数据库中搜索一次，以确保页面上待锁定的合同号没有被人使用或先锁定*********/
            string pcon_bcode = txtPCON_BCODE.Text.Trim();
            string check_code = " select * from TBPM_CONPCHSINFO where PCON_BCODE='" + pcon_bcode + "'";
            DataTable dt_check = DBCallCommon.GetDTUsingSqlText(check_code);
            if (dt_check.Rows.Count > 0)
            {
                CreateCode();
            }

            string conid = txtPCON_BCODE.Text.Trim();
            DateTime cttime = DateTime.Now;
            if (conid != "")
            {
                //LinkLock.Enabled = false;

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
        protected void linkdel_FP_Click(object sender, EventArgs e)
        {
            string br_id = ((LinkButton)sender).CommandArgument.ToString();
            string sql_del = "DELETE FROM TBPM_BUSBILLRECORD WHERE BR_ID='" + br_id + "'";
            DBCallCommon.ExeSqlText(sql_del);
            this.BindFP_Record(condetail_id);
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('删除成功！');", true); return;

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
                    if (((ITextControl)retItem.FindControl("TSA_ID")).Text != "")
                    {
                        str.Add(((ITextControl)retItem.FindControl("TSA_ID")).Text);
                        str = str.Distinct<string>().ToList();
                    }
                }
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (txtPCON_SCH.Text.Trim() != "")
            {
                ScriptManager.RegisterStartupScript(this.Page, typeof(string), "aa", "aa();", true);
                int count = addRep.Items.Count;
                AddStr();
                CreateNewRow(Convert.ToInt32(1), count);
            }
            ScriptManager.RegisterStartupScript(this.Page, GetType(), DateTime.Now.ToString("ssffff"), "autosize();", true);
        }

        #region 增加行

        private void CreateNewRow(int num, int count) // 生成输入行函数
        {
            bool b = true;
            DataTable dt = new DataTable();
            dt = GetTable(dt);
            if (!str.Contains(txtPCON_SCH.Text.Trim()))//如果是新任务号，在之前添加一行
            {
                b = false;
                for (int i = 0; i < num; i++)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[8] = 1;
                    dt.Rows.Add(newRow);
                    if (!str.Contains(txtPCON_SCH.Text.Trim()))
                    {
                        dt.Rows[0][0] = txtPCON_SCH.Text.Trim();
                    }
                }
            }
            dt = this.GetDataTable(num, dt);
            if (b)//如果不是新的一行，就在旧的下面添加一行
            {
                if (str.IndexOf(txtPCON_SCH.Text.Trim()) + 1 == str.Count || !str.Contains(txtPCON_SCH.Text.Trim()))
                {
                    for (int i = 0; i < num; i++)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow[8] = 1;
                        dt.Rows.Add(newRow);
                        if (!str.Contains(txtPCON_SCH.Text.Trim()))
                        {
                            dt.Rows[0][0] = txtPCON_SCH.Text.Trim();
                        }
                    }
                }
            }
            this.addRep.DataSource = dt;
            this.addRep.DataBind();
            InitVar();
            CalcuHZ();
        }

        List<string> list = new List<string>() { "TSA_ID", "CM_PROJ", "CM_YZHTH", "CM_ENGNAME", "CM_MAP", "CM_MATERIAL", "CM_PRICE", "CM_COUNT", "CM_NUMBER", "CM_UNIT", "CM_WEIGHT", "CM_ALL", "CM_SIGN", "CM_JIAO", "CM_DUTY", "CM_NOTE", "CM_DTSJ", "CM_CPRK", "CM_TZFH", "CM_CKSJ", "CM_DFSM", "CM_JHDTYPE" };
        private DataTable GetDataTable(int num, DataTable dt)
        {
            for (int i = 0; i < addRep.Items.Count; i++)
            {
                RepeaterItem retItem = addRep.Items[i];
                DataRow newRow = dt.NewRow();
                TextBox tsa_id = (TextBox)retItem.FindControl("TSA_ID");
                if (tsa_id.Text.Trim() != "")
                {
                    newRow[0] = tsa_id.Text.Trim();
                    tsa_id.Enabled = false;
                }
                else
                {
                    tsa_id.Enabled = true;
                    tsa_id.BorderStyle = BorderStyle.None;
                    tsa_id.BackColor = System.Drawing.Color.Transparent;
                }
                SetNewRow(newRow, retItem);
                dt.Rows.Add(newRow);
                if (str.IndexOf(txtPCON_SCH.Text.Trim()) + 1 < str.Count && i + 1 < addRep.Items.Count)
                {
                    if (((ITextControl)addRep.Items[i + 1].FindControl("TSA_ID")).Text.Trim() == str[str.IndexOf(txtPCON_SCH.Text.Trim()) + 1].ToString())
                    {
                        for (int j = 0; j < num; j++)
                        {
                            DataRow newRow1 = dt.NewRow();
                            newRow1[8] = 1;
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
            for (int i = 1; i < list.Count; i++)
            {
                newRow[i] = ((TextBox)retItem.FindControl(list[i].ToString())).Text.Trim();
            }
        }

        private void InitVar()
        {
            if (addRep.Items.Count == 0)
            {
                Panel1.Visible = true;
            }
            else
            {
                Panel1.Visible = false;
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
                string contr = ((ITextControl)retItem.FindControl("TSA_ID")).Text;
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
                            newRow[0] = ((ITextControl)retItem.FindControl("TSA_ID")).Text;
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
            CalcuHZ();
            ScriptManager.RegisterStartupScript(this.Page, GetType(), DateTime.Now.ToString("ssffff"), "autosize();", true);
        }

        List<string> strlist = new List<string>();
        Dictionary<string, string> blurDic = new Dictionary<string, string>() { { "CM_PROJ", "proj" }, { "CM_JIAO", "jiao" }, { "CM_SIGN", "sign" }, { "CM_DUTY", "duty" }, { "CM_DTSJ", "dtsj" }, { "CM_CPRK", "cprk" }, { "CM_TZFH", "tzfh" }, { "CM_CKSJ", "cksj" }, { "CM_DFSM", "sfsm" } };
        protected void addRep_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox txtTsaId = (TextBox)e.Item.FindControl("TSA_ID");
                string tsaId = ((DataRowView)e.Item.DataItem).Row[0].ToString();
                if (strlist.Count < 1)
                {
                    strlist.Add(tsaId);
                    txtTsaId.Enabled = true;
                    foreach (var item in blurDic)
                    {
                        string str = "";
                        TextBox txtBox = (TextBox)e.Item.FindControl(item.Key);
                        if (item.Key == "CM_SIGN")
                            str = "dateCheck(this)";
                        txtBox.Attributes.Add("onblur", string.Format("setRows('.{0}');{1}", item.Value, str));
                    }
                }
                else
                {
                    if (strlist.Contains(tsaId) || tsaId == "")
                    {
                        ((DataRowView)e.Item.DataItem).Row[0] = "";
                        //((DataRowView)e.Item.DataItem).Row[1] = "";
                        e.Item.DataBind();
                        txtTsaId.Enabled = false;
                        txtTsaId.BorderStyle = BorderStyle.None;
                        txtTsaId.BackColor = System.Drawing.Color.Transparent;
                    }
                    else
                    {
                        strlist.Add(tsaId);
                        txtTsaId.Enabled = true;
                    }
                    ((TextBox)e.Item.FindControl("CM_SIGN")).Attributes.Add("onblur", "dateCheck(this)");
                }
                //((TextBox)e.Item.FindControl("CM_JIAO")).Attributes.Add("onblur", "dateCheck(this)");
                TextBox price = (TextBox)e.Item.FindControl("CM_PRICE");
                TextBox count = (TextBox)e.Item.FindControl("CM_COUNT");
                price.Text = (price.Text == "" ? "0.00" : price.Text);
                count.Text = (count.Text == "" ? "0.00" : count.Text);
                string CM_JHDTYPE = ((TextBox)e.Item.FindControl("CM_JHDTYPE")).Text.ToString();
                if (CM_JHDTYPE == "1" || CM_JHDTYPE == "3")
                {
                    TextBox CM_PROJ = (TextBox)e.Item.FindControl("CM_PROJ");
                    CM_PROJ.BackColor = System.Drawing.Color.Yellow;
                    CM_PROJ.Attributes.Add("title", "黄色背景为增补项");
                }
            }
        }

        protected void CalcuHZ()
        {
            decimal djhzc = 0;
            decimal hthzc = 0;
            decimal smhzc = 0;
            decimal zzhzc = 0;
            foreach (RepeaterItem item in addRep.Items)
            {
                TextBox tb1 = (TextBox)item.FindControl("CM_PRICE");
                TextBox tb2 = (TextBox)item.FindControl("CM_COUNT");
                TextBox tb3 = (TextBox)item.FindControl("CM_NUMBER");
                TextBox tb4 = (TextBox)item.FindControl("CM_ALL");
                string str1 = (tb1.Text == "" ? "0" : tb1.Text.Trim());
                string str2 = (tb2.Text == "" ? "0" : tb2.Text.Trim());
                string str3 = (tb3.Text == "" ? "0" : tb3.Text.Trim());
                string str4 = (tb4.Text == "" ? "0" : tb4.Text.Trim());
                //djhzc += decimal.Parse(str1);
                //hthzc += decimal.Parse(str2);
                //smhzc += decimal.Parse(str3);
                //zzhzc += decimal.Parse(str4);
                djhzc += CommonFun.ComTryDecimal(str1);
                hthzc += CommonFun.ComTryDecimal(str2);
                smhzc += CommonFun.ComTryDecimal(str3);
                zzhzc += CommonFun.ComTryDecimal(str4);
            }
            djhz.Text = djhzc.ToString();
            hthz.Text = hthzc.ToString();
            smhz.Text = smhzc.ToString();
            zzhz.Text = zzhzc.ToString();
        }

        /// <summary> 
        /// 自定义数字解析函数 
        /// 作者：三角猫 
        /// 说明：如果输入的字符串为空或非数字开头则返回0，否则返回解析结果 
        /// </summary> 
        /// <param name="v">输入的字符串</param> 
        /// <returns>解析后的结果</returns> 
        static double CustomNumericParse(string v)
        {
            if (string.IsNullOrEmpty(v)) return 0d; //如果输入的字符串为空或NULL，则直接返回0 
            if (!char.IsDigit(v[0])) return 0d; //如果输入的字符串是非数字开头，直接返回0 
            string subV = string.Empty;
            for (int i = 0; i < v.Length; i++)
            {
                if (char.IsDigit(v[i]) || (v[i].Equals('.') && !subV.Contains("."))) //从左至右，判断字符串的每位字符是否是数字或小数点，小数点只保留第一个 
                    subV += v[i];
                else
                    break;
            }

            subV.TrimEnd(new char[] { '.' }); // 如果解析后的子字符串的末位是小数点，则去掉它 

            double returnV = 0d;
            if (subV.Contains(".")) // 如果解析结果包含小数点，则根据小数点分两段求值 
            {
                string strPointRight = subV.Substring(subV.IndexOf('.') + 1); //小数点右侧部分 
                subV = subV.Substring(0, subV.IndexOf('.')); //小数点左侧部分 

                //计算小数点右侧的部分 
                for (int i = 0; i < strPointRight.Length; i++)
                {
                    returnV += ((int)strPointRight[i] - 48) / Math.Pow(10, i + 1); //(int)strPointRight[i] 是取该字符的ASCII码 
                }
            }

            //计算小数点左侧的部分 
            int iLen = subV.Length; //小数点左侧部分的长度 

            for (int i = 0; i < iLen; i++)
            {
                returnV += ((int)subV[i] - 48) * Math.Pow(10, iLen - 1 - i); //按位乘以10的幂，并和小数点右侧结果相加 
            }

            return returnV;
        }


        //业主信息
        protected void Textkehu_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            TextBox Tb_newkehucode = (TextBox)sender;
            if (num > 0)
            {
                string kehucode = (sender as TextBox).Text.Trim().Substring(0, num);
                string sqltext = "select * from TBCS_CUSUPINFO where CS_CODE='" + kehucode + "' and CS_TYPE='1'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    txtPCON_CUSTMNAME.Text = dt.Rows[0]["CS_NAME"].ToString().Trim();
                }
            }
        }
    }
}
