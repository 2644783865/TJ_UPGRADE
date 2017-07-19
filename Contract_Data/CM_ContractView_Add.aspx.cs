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
using System.Text;
using System.Collections.Generic;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_ContractView_Add : System.Web.UI.Page
    {
        Dictionary<string, string> reviewer = new Dictionary<string, string>();//用于存储审核部门负责人的名单
        //Dictionary<int, string> reviewer_LD = new Dictionary<int, string>();//用于存储审核领导的名单
        string action = string.Empty;
        string id = string.Empty;
        string type = string.Empty;
        string conForm = "";//合同类别
        //用于显示评审信息
        Table tb = new Table();//由于其不为服务器控件，其的状态没有存储在VIEWSTATE里面，故再次请求服务器，table里面没有数据，要是想其有数据，则需要将其数据放在VIEWSTATE里面       
        Table t = new Table();
        string name = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Action"] != null)
                action = Request.QueryString["action"].ToString();

            if (Request.QueryString["ID"] != null)
                id = Request.QueryString["id"].ToString();

            if (Request.QueryString["Type"] != null)
                type = Request.QueryString["Type"].ToString();
            //从合同审批类型转换到合同登记类型
            GetConform();
            /***************由于视图的存在，必须先赋值*************************/
            getLeaderInfo();
            /****************************************/
            if (!this.IsPostBack)
            {
                //添加时先创建一个唯一标识，存储在一个lable中，用来关联合同与附件，避免因合同号占用而导致附件关联错误
                if (action == "add")
                {
                    if (type == "0")
                    {
                        TabPanel3.Visible = true;
                    }
                    Guid tempid = Guid.NewGuid();
                    lbl_UNID.Text = tempid.ToString();
                    lb_lock.Visible = true;
                    LinkLock.Visible = true;
                    lb_addtips.Visible = true;
                    LbtnNO.Visible = true;
                    //释放锁定超过60分钟还未提交的合同号，先找时间差大于60分钟的，再检查这些合同号是否存在于合同表，如果在表示已经提交，不在则表示未提交，对于这些未提交的释放合同号可供其他人使用
                    string sqltext = "DELETE FROM TBCM_TEMPCONNO WHERE DATEDIFF(MI,CREATETIME,'" + DateTime.Now + "')>60 AND " +
                                     "CON_NO NOT IN (SELECT PCON_BCODE AS CON_NO FROM TBPM_CONPCHSINFO)";
                    DBCallCommon.ExeSqlText(sqltext);
                }
                BindDep();
                //BindEngtype();
                this.InitPage();
            }
            this.InitUpload();
        }
        //初始化附件上传控件 
        //统一上传到合同文档附件中
        private void InitUpload()
        {
            UploadAttachments1.Visible = true;
            UploadAttachments1.at_htbh = lbl_UNID.Text;
            UploadAttachments1.at_type = 0;
            UploadAttachments1.at_sp = 0;

        }

        //检查合同是否登记，若未登记，显示提示信息
        private void Check_HT()
        {
            string sqltext = "select * from TBPM_CONPCHSINFO where PCON_REVID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该合同尚未登记，找不到相关信息！\\r\\r请立即到相应合同下登记此合同！\\r\\r并填写正确的合同评审单号');", true); return;
            }
        }

        //页面初始化
        private void InitPage()
        {
            //合同类别
            txtPCON_TYPE.Text = ContractType(type);

            if (type == "0") //销售合同
            {
                //tr_yzhth.Visible = true;    //显示业主合同号和发货时间行
                txtPCON_CUSTMNAME.Enabled = true;
                pal.Visible = false;
                pal1.Visible = true;
                TabPanel3.Visible = true;
                btnSave.Visible = true;
                //txtPCON_ENGNAME.Enabled = true;//销售合同——工程可选填，不是必填项
            }
            //else if (type == "1") //采购合同
            //{

            //    sp_multipjname.Visible = true;//显示所有项目名称
            //}
            if (action == "add")
            {
                lblState.Text = "添加评审合同信息-" + ContractType(type);
                LBpsdh.Text = DateTime.Now.ToString("yyyyMMddhhmmss");
                txtPCON_RESPONSER.Text = Session["UserName"].ToString();

                //判断是否从采购订单添加的合同审批
                if (Request.QueryString["orderid"] != null)
                {
                    string pord_id = Request.QueryString["orderid"].ToString();
                    //GetData_FromPorder(pord_id);
                }
                if (Request.QueryString["ZJE"] != null)
                {
                    string ZJE = Request.QueryString["ZJE"].ToString();
                    //txtPCON_JINE.Text = Math.Round(Convert.ToDouble(ZJE), 2).ToString();//合同总金额保留两位小数
                }

                //设定合同中的默认值
                CM_REQUEST.Text = "按照图纸要求制作";
                CM_CHECK.Text = "按照图纸或相关国家行业标准检验、验收";
                CM_ZHIBAO.Text = "设备正常运转12个月或者设备运抵买方指定地点18个月，二者以先到为准";
                CM_YOUQI.Text = "耐热银粉漆";
                CM_PACK.Text = "符合国家或行业相关包装标准";
                CM_YUNSHU.Text = "施工现场，具体已发货通知为准";
                CM_FUKUAN.Text = "";
                CM_DDFG.Text = "符合当地法律法规";
                CM_QITA.Text = "无";

                //设定责任部门默认值
                switch (Convert.ToInt16(type))
                {
                    case 0:  //销售合同
                        dplPCON_RSPDEPID.SelectedValue = "07";
                        dplPCON_RSPDEPID_SelectedIndexChanged(null, null);//创建合同号
                        this.LinkLock_Click(null, null);//锁定合同号
                        pal.Visible = false;
                        Panel2.Visible = true;
                        txtTask.Enabled = false;
                        pal1.Visible = true;
                        //tr_yzhth.Visible = true;
                        break;
                    case 1:  //采购合同
                        dplPCON_RSPDEPID.SelectedValue = "04";
                        dplPCON_RSPDEPID_SelectedIndexChanged(null, null);//创建合同号
                        this.LinkLock_Click(null, null);//锁定合同号
                        lbType.Text = "外协单据号";
                        break;
                    case 3:  //生产外协
                        dplPCON_RSPDEPID.SelectedValue = "05";
                        dplPCON_RSPDEPID_SelectedIndexChanged(null, null);//创建合同号
                        this.LinkLock_Click(null, null);//锁定合同号
                        lbType.Text = "订单号";
                        break;
                    case 2: //厂内分包
                        dplPCON_RSPDEPID.SelectedValue = "04";
                        dplPCON_RSPDEPID_SelectedIndexChanged(null, null);//创建合同号
                        this.LinkLock_Click(null, null);//锁定合同号
                        pal.Visible = false;
                        pal1.Visible = true;
                        break;
                    case 4:   //办公合同
                        dplPCON_RSPDEPID.SelectedValue = "02";
                        dplPCON_RSPDEPID_SelectedIndexChanged(null, null);//创建合同号
                        this.LinkLock_Click(null, null);//锁定合同号
                        pal.Visible = false;
                        pal1.Visible = true;
                        break;
                    //**************************************************
                    default:
                        dplPCON_RSPDEPID.Enabled = true;
                        break;
                }
            }
            else if (action == "edit")
            {
                lblState.Text = "修改评审合同信息-" + ContractType(type);
                BindBaseData();
                bindSelectReviewer();
                ControlEnabled();
                Check_HT();
                txtPCON_BCODE.Enabled = true;
                if (type == "0")
                {
                    Panel2.Visible = true;
                    string sqlstr = "select * from TBPM_CONTRACTPS where ID='" + id + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlstr);

                    foreach (Control control in PanelAll.Controls)
                    {
                        if (control is TextBox)
                        {
                            ((TextBox)control).Text = dt.Rows[0][control.ID].ToString();
                        }
                    }

                    sqlstr = "select * from TBPM_HESUAN where ID='" + id + "'";
                    GridHeSuan.DataSource = DBCallCommon.GetDTUsingSqlText(sqlstr);
                    GridHeSuan.DataBind();
                }
                txtPCON_BCODE.Enabled = false;
            }
        }

        //控制控件是否可用
        private void ControlEnabled()
        {
            //修改时只允许更改部分内容

            //项目名称
            tb_pjinfo.Enabled = false;
            //工程名称
            //ddl_ENGNAME.Enabled = false;
            //btn_clear_eng.Enabled = false;
            //责任部门
            dplPCON_RSPDEPID.Enabled = false;
            //设备类型
            //ddl_engtype.Enabled = false;
            //txtPCON_ENGTYPE.Enabled = false;
            //合同类别
            txtPCON_TYPE.Enabled = false;
            //合同名称
            //txtPCON_NAME.Enabled = false;
            //合同金额
            //txtPCON_JINE.Enabled = false;
            //其他币种
            //Other_MONUNIT.Enabled = false;
            //货币单位
            //ddl_PCONMONUNIT.Enabled = false;
            //厂商
            txtPCON_RESPONSER.Enabled = false;
            txtPCON_CUSTMNAME.Enabled = false;
        }

        //绑定设备类型
        //private void BindEngtype()
        //{
        //    ddl_engtype.Items.Clear();
        //    if (type == "0")
        //    {
        //        ddl_engtype.Items.Insert(0,new ListItem("-请选择-", "0"));
        //        ddl_engtype.Items.Insert(1,new ListItem("回转窑", "1"));
        //        ddl_engtype.Items.Insert(2,new ListItem("管墨机", "2"));
        //        ddl_engtype.Items.Insert(3,new ListItem("立磨", "3"));
        //        ddl_engtype.Items.Insert(4,new ListItem("篦冷机", "4"));
        //        ddl_engtype.Items.Insert(5,new ListItem("辊压机", "5"));
        //        ddl_engtype.Items.Insert(6,new ListItem("破碎机", "6"));
        //        ddl_engtype.Items.Insert(7,new ListItem("选粉机", "7"));
        //        ddl_engtype.Items.Insert(8,new ListItem("堆取料机", "8"));
        //        ddl_engtype.Items.Insert(9,new ListItem("电收尘器", "9"));
        //        ddl_engtype.Items.Insert(10,new ListItem("袋收尘器", "10"));
        //        ddl_engtype.Items.Insert(11,new ListItem("板喂机", "11"));
        //        ddl_engtype.Items.Insert(12,new ListItem("提升机", "12"));
        //        ddl_engtype.Items.Insert(13,new ListItem("预热器", "13"));
        //        ddl_engtype.Items.Insert(14,new ListItem("增湿塔", "14"));
        //        ddl_engtype.Items.Insert(15,new ListItem("承接的分交件", "15"));
        //        ddl_engtype.Items.Insert(16,new ListItem("钢结构及铆焊件", "16"));
        //        ddl_engtype.Items.Insert(17,new ListItem("备品备件", "17"));
        //        ddl_engtype.Items.Insert(18,new ListItem("其他", "18"));
        //        ddl_engtype.SelectedIndex = 0;
        //    }
        //    else
        //    {
        //        ddl_engtype.Items.Insert(0, new ListItem("-请选择-", "0"));
        //        ddl_engtype.Items.Insert(1, new ListItem("钢结构", "1"));
        //        ddl_engtype.Items.Insert(2, new ListItem("非标设备", "2"));
        //        ddl_engtype.Items.Insert(3, new ListItem("仓内分交", "3"));
        //        ddl_engtype.Items.Insert(4, new ListItem("输送设备", "4"));
        //        ddl_engtype.Items.Insert(5, new ListItem("提升设备", "5"));
        //        ddl_engtype.Items.Insert(6, new ListItem("烧成设备", "6"));
        //        ddl_engtype.Items.Insert(7, new ListItem("粉磨设备", "7"));
        //        ddl_engtype.Items.Insert(8, new ListItem("窑外分解", "8"));
        //        ddl_engtype.Items.Insert(9, new ListItem("圆形堆取料机", "9"));
        //        ddl_engtype.Items.Insert(10, new ListItem("桥式堆料机", "10"));
        //        ddl_engtype.Items.Insert(11, new ListItem("侧式取料机", "11"));
        //        ddl_engtype.Items.Insert(12, new ListItem("门式堆料机", "12"));
        //        ddl_engtype.Items.Insert(13, new ListItem("破碎设备", "13"));
        //        ddl_engtype.Items.Insert(14, new ListItem("阀门", "14"));
        //        ddl_engtype.Items.Insert(15, new ListItem("收尘设备", "15"));
        //        ddl_engtype.Items.Insert(16, new ListItem("包装设备", "16"));
        //        ddl_engtype.Items.Insert(17, new ListItem("电气设备", "17"));
        //        ddl_engtype.Items.Insert(18, new ListItem("其他", "18"));
        //        ddl_engtype.SelectedIndex = 0;
        //    }

        //}

        protected void GetConform()
        {
            switch (type)
            {
                case "0": //销售合同
                    conForm = "0";
                    break;
                case "1":  //委外合同  
                    conForm = "1";
                    break;
                case "2"://厂内分包
                    conForm = "2";
                    break;
                case "3":  //采购合同
                    conForm = "3";
                    break;
                case "4":  //办公合同
                    conForm = "4";
                    break;
                case "5":  //其他合同
                    conForm = "5";
                    break;
                default:
                    break;
            }
        }

        //绑定基本信息
        private void BindBaseData()
        {
            //基本信息
            string sqlcontractinfo = "select * from View_CM_CONTRACT_REV_INFO_ALL where cr_id='" + id + "'";
            DataTable dt_RevInfo = DBCallCommon.GetDTUsingSqlText(sqlcontractinfo);
            if (dt_RevInfo.Rows.Count > 0)
            {
                //唯一编号
                lbl_UNID.Text = dt_RevInfo.Rows[0]["GUID"].ToString();
                //评审状态
                lblPSZT.Text = dt_RevInfo.Rows[0]["CR_PSZT"].ToString();
                //评审单号
                LBpsdh.Text = dt_RevInfo.Rows[0]["CR_ID"].ToString();
                //项目名称
                tb_pjinfo.Text = dt_RevInfo.Rows[0]["CR_XMMC"].ToString();
                //设备名称
                //tb_SBMC.Text = dt_RevInfo.Rows[0]["CR_SBMC"].ToString();
                //制单人意见
                txt_zdrYJ.Text = dt_RevInfo.Rows[0]["CRD_NOTE"].ToString();
                //合同范围
                //tb_HTFW.Text = dt_RevInfo.Rows[0]["CR_FBFW"].ToString();                
                //订单编号
                //tb_orderid.Text = dt_RevInfo.Rows[0]["CR_ORDERID"].ToString(); 
                //合同编号
                txtPCON_BCODE.Text = dt_RevInfo.Rows[0]["PCON_BCODE"].ToString();
                Hidden.Value = txtPCON_BCODE.Text;
                //任务号
                txtTask.Text = dt_RevInfo.Rows[0]["PCON_TASK"].ToString();
                //业主合同号
                txt_YZHTH.Text = dt_RevInfo.Rows[0]["PCON_YZHTH"].ToString();
                //合同名称
                //txtPCON_NAME.Text = dt_RevInfo.Rows[0]["PCON_NAME"].ToString();
                //项目编号
                //tb_pjid.Text = dt_RevInfo.Rows[0]["PCON_PJID"].ToString();
                //项目名称
                tb_pjinfo.Text = dt_RevInfo.Rows[0]["PCON_ENGNAME"].ToString();
                //责任部门
                dplPCON_RSPDEPID.SelectedValue = dt_RevInfo.Rows[0]["PCON_RSPDEPID"].ToString();
                //工程编号
                //txtPCON_ENGID.Text = dt_RevInfo.Rows[0]["PCON_ENGID"].ToString();
                //工程名称
                //txtPCON_ENGNAME.Text = dt_RevInfo.Rows[0]["PCON_ENGNAME"].ToString();
                //合同类别
                txtPCON_TYPE.Text = dt_RevInfo.Rows[0]["PCON_TYPE"].ToString();
                //设备类型
                //txtPCON_ENGTYPE.Text = dt_RevInfo.Rows[0]["PCON_ENGTYPE"].ToString();
                //合同金额
                //txtPCON_JINE.Text = dt_RevInfo.Rows[0]["PCON_JINE"].ToString();
                //其他币种
                //Other_MONUNIT.Text = dt_RevInfo.Rows[0]["OTHER_MONUNIT"].ToString();
                //货币单位
                //ddl_PCONMONUNIT.SelectedValue = dt_RevInfo.Rows[0]["PCON_MONUNIT"].ToString();
                //签订日期
                txtPCON_FILLDATE.Text = dt_RevInfo.Rows[0]["PCON_FILLDATE"].ToString();
                //生效日期
                txtPCON_VALIDDATE.Text = dt_RevInfo.Rows[0]["PCON_VALIDDATE"].ToString();
                //客户名称
                txtPCON_CUSTMNAME.Text = dt_RevInfo.Rows[0]["PCON_CUSTMNAME"].ToString();
                //客户编号
                txtPCON_CUSTMID.Value = dt_RevInfo.Rows[0]["PCON_CUSTMID"].ToString();
                //负责人
                txtPCON_RESPONSER.Text = dt_RevInfo.Rows[0]["PCON_RESPONSER"].ToString();
                //结算金额
                //txtPCON_BALANCEACNT.Text = dt_RevInfo.Rows[0]["PCON_BALANCEACNT"].ToString();         
                //交货日期
                //txtPCON_DELIVERYDATE.Text = dt_RevInfo.Rows[0]["PCON_DELIVERYDATE"].ToString();
                //计入成本
                //txtPCON_COST.Text = dt_RevInfo.Rows[0]["PCON_COST"].ToString();
                //备注
                txtPCON_NOTE.Text = dt_RevInfo.Rows[0]["PCON_NOTE"].ToString();
                //发货时间
                //txt_FHSJ.Text = dt_RevInfo.Rows[0]["PCON_FHSJ"].ToString();
            }
        }

        //绑定采购订单相关信息
        //private void GetData_FromPorder(string orderid)
        //{
        //    tb_orderid.Text = orderid;
        //    string xmmc = "";
        //    string htfw = "";
        //    string str_orderid = "'" + orderid.Replace(",", "','") + "'";//将订单号重组
        //    //项目名称
        //    string sql_xmmc = "select distinct pjnm from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where orderno in (" + str_orderid + ")";
        //    DataTable dt_xmmc = DBCallCommon.GetDTUsingSqlText(sql_xmmc);
        //    if (dt_xmmc.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dt_xmmc.Rows)
        //        {
        //            if (dr["pjnm"].ToString() != "")
        //            {
        //                xmmc += dr["pjnm"].ToString() + ",";
        //            }
        //        }
        //        lbl_multipjname.Text = xmmc.Substring(0, xmmc.Length - 1);                
        //    }
        //    //供应商  订单只有一个则取全部，否则取第一个逗号前的
        //    string first_orderid = str_orderid.Substring(0, str_orderid.Contains(",") ? str_orderid.IndexOf(",") : str_orderid.Length);//截取第一个订单号，带单引号
        //    string sql_suppliernm = "select suppliernm,supplierid from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where orderno=" + first_orderid + "";
        //    DataTable dt_suppliernm = DBCallCommon.GetDTUsingSqlText(sql_suppliernm);
        //    if (dt_suppliernm.Rows.Count > 0)
        //    {
        //        txtPCON_CUSTMNAME.Text = dt_suppliernm.Rows[0]["suppliernm"].ToString();
        //        txtPCON_CUSTMID.Value = dt_suppliernm.Rows[0]["supplierid"].ToString();
        //    }

        //    //设备名称 工程名称？？

        //    //合同范围（采购物品）
        //    string sql_htfw = "select distinct marnm from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where orderno in (" + str_orderid + ")";
        //    DataTable dt_htfw = DBCallCommon.GetDTUsingSqlText(sql_htfw);
        //    if (dt_htfw.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dt_htfw.Rows)
        //        {
        //            htfw += dr["marnm"].ToString() + ",";
        //        }
        //        tb_HTFW.Text = htfw.Substring(0, htfw.Length - 1);
        //    }
        //}

        //类型转换
        private string ContractType(string PS_type)
        {
            string CH_type = "";
            switch (PS_type)
            {
                case "0": CH_type = "销售合同";
                    break;
                case "3": CH_type = "采购合同";
                    break;
                case "1": CH_type = "生产外协";
                    break;
                //case "3": CH_type = "厂内分包";
                //    break;
                case "4": CH_type = "办公合同";
                    break;
                case "5": CH_type = "其他合同";
                    break;
                case "6": CH_type = "补充协议";
                    break;
                default:
                    break;
            }
            return CH_type;
        }

        #region 得到部门领导信息

        protected void getLeaderInfo()
        {
            /******绑定人员信息*****/
            if (type == "4")
            {
                
            }
            getStaffInfo("03", "技术部", 0);
            getStaffInfo("04", "生产管理部", 1);
            getStaffInfo("05", "采购部", 2);
            getStaffInfo("06", "财务部", 3);
            getStaffInfo("07", "市场部", 4);
            getStaffInfo("12", "质量部", 5);
            getStaffInfo("01", "公司领导", 6);
            //得到领导信息，根据金额
            if (action == "add" || action == "edit")
            {
                Panel1.Controls.Add(t);
            }
        }

        protected void getStaffInfo(string st_id, string DEP_NAME, int i)
        {
            string sql = string.Format("select ST_NAME,ST_ID,ST_DEPID from TBDS_STAFFINFO as a inner join TBCM_HT_SETTING as b on a.ST_ID=b.per_id where a.ST_PD='0'and b.dep_id='{0}' and per_sfjy='0' and per_type='0'", st_id);
            //bindInfo(sql, i, DEP_NAME, st_id);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (st_id == "01")
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataTable ld = new DataTable();
                    ld.Columns.Add("ST_NAME");
                    ld.Columns.Add("ST_ID");
                    ld.Columns.Add("ST_DEPID");
                    ld.Rows.Add(dt.Rows[j][0].ToString(), dt.Rows[j][1].ToString(), dt.Rows[j][2].ToString());
                    bindInfo(ld, st_id + j.ToString(), DEP_NAME, i);
                    i++;
                }
            }
            else
            {
                bindInfo(dt, st_id, DEP_NAME, i);
            }
        }

        protected void bindInfo(DataTable dt, string st_id, string DEP_NAME, int i)
        {
            if (dt.Rows.Count != 0)
            {
                TableRow tr = new TableRow();
                TableCell td1 = new TableCell();//第一列为部门名称
                td1.Width = 85;
                Label lab = new Label();
                lab.Text = DEP_NAME + ":";
                Label lab1 = new Label();
                lab1.ID = "dep" + i.ToString();
                lab1.Text = st_id;
                lab1.Visible = false;
                td1.Controls.Add(lab);
                td1.Controls.Add(lab1);
                tr.Cells.Add(td1);

                CheckBoxList cki = new CheckBoxList();//第二列为领导的姓名
                cki.ID = "cki" + i.ToString();
                cki.DataSource = dt;
                cki.DataTextField = "ST_NAME";//领导的姓名
                cki.DataValueField = "ST_ID";//部门的编号
                cki.DataBind();
                for (int k = 0; k < cki.Items.Count; k++)
                {
                    cki.Items[k].Attributes.Add("Onclick", "CheckBoxList_Click(this)");//使用了javascript使其只能勾选一个
                }
                cki.RepeatColumns = 5;//获取显示的列数
                TableCell td2 = new TableCell();
                td2.Controls.Add(cki);
                tr.Cells.Add(td2);
                t.Controls.Add(tr);
            }
        }

        #endregion

        private class asd
        {
            public static string bcode;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //检查业主是否在基础数据中存在
            string sqlyezhu = "select * from TBCS_CUSUPINFO where (CS_NAME='" + txtPCON_CUSTMNAME.Text.Trim() + "' or CS_HRCODE='" + txtPCON_CUSTMNAME.Text.Trim() + "') and CS_TYPE='1'";
            DataTable dtyezhu = DBCallCommon.GetDTUsingSqlText(sqlyezhu);
            if (dtyezhu.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('业主名称不存在于基础数据客户表中！！！');", true);
                return;
            }

            string find = "select PCON_BCODE from TBPM_CONPCHSINFO where PCON_BCODE='" + txtPCON_BCODE.Text.Trim() + "'";
            bool ps = true;
            DataTable findtb = DBCallCommon.GetDTUsingSqlText(find);
            //判断合同号是否存在
            if (((Control)sender).ID == "btnSave")
            {
                if (action == "add")
                {
                    asd.bcode = txtPCON_BCODE.Text;
                    if (findtb.Rows.Count > 0)
                    {
                        Response.Write("<script>alert('已存在此合同号！请不要重复添加！')</script>");
                        ps = false;
                        return;
                    }
                }
                else if (action == "edit")
                {
                    DataTable getHt = DBCallCommon.GetDTUsingSqlText("select PCON_BCODE from TBPM_CONPCHSINFO where PCON_BCODE='" + Hidden.Value + "'");
                    if (findtb.Rows.Count > 0)
                    {
                        if (getHt.Rows.Count > 0)
                        {
                            if (getHt.Rows[0][0].ToString() != findtb.Rows[0][0].ToString())
                            {
                                Response.Write("<script>alert('已存在此合同号！')</script>");
                                ps = false;
                            }
                        }
                    }
                }
            }
            if (((Control)sender).ID == "LbtnSubmit")
            {
                if (action == "add")
                {
                    if (findtb.Rows.Count > 1)
                    {
                        Response.Write("<script>alert('已存在此合同号！请不要重复添加！')</script>");
                        ps = false;
                        return;
                    }
                }
                else if (action == "edit")
                {
                    DataTable getHt = DBCallCommon.GetDTUsingSqlText("select PCON_BCODE from TBPM_CONPCHSINFO where PCON_BCODE='" + Hidden.Value + "'");
                    if (findtb.Rows.Count > 0)
                    {
                        if (getHt.Rows.Count > 0)
                        {
                            if (getHt.Rows[0][0].ToString() != findtb.Rows[0][0].ToString())
                            {
                                Response.Write("<script>alert('已存在此合同号！')</script>");
                                ps = false;
                            }
                        }
                    }
                }
            }
            bindReviewer();//读出评审人员
            //检查必填项
            bool check = true;
            if (((Control)sender).ID != "btnSave")
            {
                check = check_MustPutIn();
            }
            if (check)
            {
                List<string> strb_sql = new List<string>();
                #region    ExecSQL_RevInfo  向合同审批表执行SQL语句，包括添加和修改
                //待插入数据*****************
                string cr_xmmc = tb_pjinfo.Text.Trim();//项目名称
                string cr_id = LBpsdh.Text;//	评审单号	     
                //string cr_sbmc = tb_SBMC.Text;//设备名称
                string cr_zdr = Session["UserID"].ToString();//	制单人	
                string cr_zdrq = DateTime.Now.ToString("yyyy-MM-dd");//起草时间
                string cr_zdrYJ = txt_zdrYJ.Text;//制单人意见
                //double cr_htje = Convert.ToDouble(txtPCON_JINE.Text.Trim()); //	评审合同金额	
                string cr_lb = type;//评审合同类别
                string cr_note = txtPCON_NOTE.Text.ToString();
                //string cr_orderid = tb_orderid.Text.ToString();//采购订单号

                string cr_fbs = txtPCON_CUSTMNAME.Text;//分包商
                //string cr_cbfw = tb_HTFW.Text.Trim();//分包范围
                //**********************

                string sql_insert = "";

                //需要特别注意——添加或更新前先将原来的记录删除，再重新插入一次记录
                string sql_del = "";
                sql_del = "delete from TBCR_CONTRACTREVIEW where CR_ID='" + cr_id + "'";
                strb_sql.Add(sql_del);
                sql_del = "delete from TBCR_CONTRACTREVIEW_DETAIL where CRD_ID='" + cr_id + "'";
                strb_sql.Add(sql_del);
                sql_del = "delete from TBPM_CONTRACTPS where ID='" + id + "'";
                strb_sql.Add(sql_del);
                sql_del = "delete from TBPM_HESUAN where ID='" + id + "'";
                strb_sql.Add(sql_del);

                string pszt = "1";
                if (((Control)sender).ID == "btnSave")
                {
                    pszt = "0";
                }
                string ercips = "0";
                if (lblPSZT.Text == "2")
                {
                    ercips = "1";
                }

                //信息总表
                sql_del = "insert into TBCR_CONTRACTREVIEW(CR_ID,CR_XMMC,CR_FBSMC,CR_HTLX,CR_ZDR,CR_ZDRQ,CR_PSZT,CR_NOTE,CR_ERCIPS) " +
                    " values('" + cr_id + "','" + cr_xmmc + "','" + cr_fbs + "','" + cr_lb + "','" + cr_zdr + "','" + cr_zdrq + "','" + pszt + "','" + cr_note + "','" + ercips + "')";
                strb_sql.Add(sql_del);

                //合同评审信息详细表(TBCR_CONTRACTREVIEW_DETAIL)
                sql_insert = "insert into TBCR_CONTRACTREVIEW_DETAIL (CRD_ID,CRD_PID,CRD_PSYJ,CRD_NOTE,CRD_SHSJ,CRD_DEP,CRD_PIDTYPE) values" +
                        " ('" + cr_id + "','" + cr_zdr + "','2','" + cr_zdrYJ + "','" + cr_zdrq + "','" + Session["UserDeptID"].ToString() + "','0')";
                strb_sql.Add(sql_insert);  //制单人意见

                for (int i = 0; i < reviewer.Count; i++)
                {
                    /******************通过键来找值******************************************************/
                    /**为了兼容领导同时为部门负责人的情况，评审人部门编号要以评审人员设置表中为准，而不以当前登录人部门编号为依据**/
                    string sql_dep = "select dep_id from TBCM_HT_SETTING where per_id='" + reviewer.Values.ElementAt(i) + "' and per_sfjy=0 and per_type='0'";
                    DataTable dt_dep = DBCallCommon.GetDTUsingSqlText(sql_dep);
                    if (dt_dep.Rows.Count > 0)
                    {
                        sql_insert = "insert into TBCR_CONTRACTREVIEW_DETAIL (CRD_ID,CRD_PID,CRD_DEP,CRD_PIDTYPE) values" +
                            " ('" + cr_id + "','" + reviewer.Values.ElementAt(i) + "','" + dt_dep.Rows[0]["dep_id"].ToString().Substring(0, 2) + "','1')";
                        strb_sql.Add(sql_insert);//其他人
                    }
                }
                #endregion

                #region   ExecSQL_ConInfo  向合同信息表执行SQL语句，包括添加和修改
                //唯一标识符
                string guid = lbl_UNID.Text.Trim();
                //合同号
                string pcon_bcode = txtPCON_BCODE.Text.Trim();
                //市场部合同号
                string pcon_task = txtTask.Text.Trim();
                //合同名称
                //string pcon_name = txtPCON_NAME.Text.Trim();
                //项目编号
                //string pcon_pjid = tb_pjid.Text.Trim();
                //项目名称
                string pcon_pjname = tb_pjinfo.Text;
                //责任部门
                string pcon_rspdepid = dplPCON_RSPDEPID.SelectedValue.ToString();
                //工程编号
                //string pcon_engid = txtPCON_ENGID.Text.Trim();
                //工程名称
                //string pcon_engname = txtPCON_ENGNAME.Text.Trim();
                //合同类别
                string pcon_type = txtPCON_TYPE.Text.Trim();
                //签订日期
                string pcon_filldate = txtPCON_FILLDATE.Text.ToString();
                //生效日期
                string pcon_validdate = txtPCON_VALIDDATE.Text.ToString();
                //业主名称
                string pcon_custmname = txtPCON_CUSTMNAME.Text.ToString();
                //业主编号
                string pcon_custmid = txtPCON_CUSTMID.Value.ToString();
                //负责人
                string pcon_responser = txtPCON_RESPONSER.Text.Trim();
                //备注
                string pcon_note = txtPCON_NOTE.Text.Trim();
                //合同类型
                string pcon_form = conForm;//合同的类别0、1、2、3、4 
                //业主合同号
                string pcon_yzhth = txt_YZHTH.Text.Trim();
                //发货时间
                //string pcon_fhsj = txt_FHSJ.Text.Trim();

                /*********添加订单号和合同评审单号字段*********/

                //string pcon_orderid = tb_orderid.Text.Trim();
                string pcon_revid = LBpsdh.Text.Trim();

                /*********添加投标评审编号*********/
                //string pcon_tbid = tbcode.Text.Trim();
                /*********************************************/
                string sqltext = "";
                #region  添加
                if (action == "add" && ps)
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
                            case "02":
                                dep_code = "BG"; break;//办公
                            case "03":
                                dep_code = "JS"; break;//技术
                            case "04":
                                dep_code = "SC"; break;//生产
                            case "05":
                                dep_code = "CG"; break;//质量
                            case "06":
                                dep_code = "CW"; break;//采购
                            case "07":
                                dep_code = ""; break;//储运
                            case "10":
                                dep_code = "SB"; break; //安全
                        }
                        string newid = this.CreateCode(dep_code);
                        txtPCON_BCODE.Text = newid;
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('因多人同时添加，合同号【" + oldcode + "】已经存在\\r\\r此合同号自动修改为【" + newid + "】\\r\\r请修改附件中的合同号并重新上传后再提交！');", true); return;
                    }
                    sqltext = "insert into TBPM_CONPCHSINFO(PCON_BCODE,PCON_ENGNAME,PCON_RSPDEPID,PCON_TYPE,PCON_FILLDATE,PCON_VALIDDATE,PCON_CUSTMNAME,PCON_CUSTMID,PCON_RESPONSER,PCON_NOTE,PCON_FORM,PCON_YZHTH,PCON_REVID,GUID,PCON_TASK,PCON_ORDERID) values('" + pcon_bcode + "','" + pcon_pjname + "','" + pcon_rspdepid + "','" + pcon_type + "','" + pcon_filldate + "','" + pcon_validdate + "','" + pcon_custmname + "','" + pcon_custmid + "','" + pcon_responser + "','" + pcon_note + "','" + pcon_form + "','" + pcon_yzhth + "','" + pcon_revid + "','" + guid + "','" + pcon_task + "','" + txtPCON_ORDERID.Text + "')";
                    strb_sql.Add(sqltext);
                #endregion
                }
                else if (action == "edit" && ps)
                {
                    pcon_bcode = txtPCON_BCODE.Text;
                    sqltext = "update TBPM_CONPCHSINFO set PCON_BCODE='" + pcon_bcode + "',PCON_FILLDATE='" + pcon_filldate + "',PCON_VALIDDATE='" + pcon_validdate + "',PCON_YZHTH='" + pcon_yzhth + "',PCON_ORDERID='" + txtPCON_ORDERID.Text + "' where PCON_BCODE='" + Hidden.Value + "'";
                    strb_sql.Add(sqltext);
                } if (!ps)
                {
                    sqltext = "update TBPM_CONPCHSINFO set PCON_REVID='" + cr_id + "' where PCON_BCODE='" + pcon_bcode + "'";
                    strb_sql.Add(sqltext);
                    sqltext = "select * from TBPM_CONPCHSINFO where PCON_BCODE='" + pcon_bcode + "'";
                    DataTable dtguid = DBCallCommon.GetDTUsingSqlText(sqltext);
                    string fjid = "";
                    if (dtguid.Rows.Count > 0)
                    {
                        fjid = dtguid.Rows[0]["GUID"].ToString();
                    }
                    if (fjid == "")
                    {
                        sqltext = "update TBPM_CONPCHSINFO set GUID='" + fjid + "' where PCON_BCODE='" + pcon_bcode + "'";
                        strb_sql.Add(sqltext);
                    }
                    else
                    {
                        sqltext = "update TBPM_ATTACHMENTS set AT_HTBH='" + fjid + "' where AT_HTBH='" + guid + "'";
                        strb_sql.Add(sqltext);
                    }
                }
                #endregion

                if (type == "0")
                {
                    string col = "";
                    string value = "";
                    foreach (Control control in PanelAll.Controls)
                    {
                        if (control is TextBox)
                        {
                            col += control.ID + ",";
                            value += string.Format("'{0}',", ((TextBox)control).Text);
                        }
                    }
                    col += "ID";
                    value += string.Format("'{0}'", pcon_revid);
                    string sqlstr = string.Format("insert into TBPM_CONTRACTPS({0}) values({1})", col, value);
                    strb_sql.Add(sqlstr);
                    foreach (GridViewRow gr in GridHeSuan.Rows)
                    {
                        sqlstr = string.Format("insert into TBPM_HESUAN(ID,CM_XM,CM_ZL,CM_DJ,CM_ZJ) values('{0}','{1}','{2}','{3}','{4}')", pcon_revid, ((TextBox)gr.Cells[0].FindControl("txt0")).Text, ((TextBox)gr.Cells[1].FindControl("txt1")).Text, ((TextBox)gr.Cells[2].FindControl("txt2")).Text, ((TextBox)gr.Cells[3].FindControl("txt3")).Text);
                        strb_sql.Add(sqlstr);
                    }
                }

                DBCallCommon.ExecuteTrans(strb_sql);
                if (((Control)sender).ID != "btnSave")
                {
                    string _body = "合同审批任务:"
                         + "\r\n合同号：" + txtPCON_BCODE.Text.Trim()
                         + "\r\n项目名称：" + tb_pjinfo.Text.Trim();
                    string _subject = "您有新的【合同】需要审批，请及时处理";
                    for (int i = 0; i < reviewer.Count; i++)
                    {
                        if (reviewer.Values.ElementAt(i) != "1" && reviewer.Values.ElementAt(i) != "2" && reviewer.Values.ElementAt(i) != "310" && reviewer.Values.ElementAt(i) != "311")
                        {
                            string _emailto = DBCallCommon.GetEmailAddressByUserID(reviewer.Values.ElementAt(i));
                            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        }
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交成功！！！');window.location.href='CM_ContractView.aspx';", true); return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！！！');", true); return;
                }
            }
        }

        //检查必填项
        private bool check_MustPutIn()
        {
            bool check = false;
            if (tb_pjinfo.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('项目不能为空！！');", true); return check;
            }
            if (dplPCON_RSPDEPID.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('责任部门不能为空！！');", true); return check;
            }
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                CheckBoxList ck = (CheckBoxList)Panel1.FindControl("cki" + i.ToString());
                if (ck != null)
                {
                    for (int j = 0; j < ck.Items.Count; j++)
                    {
                        if (ck.Items[j].Selected == true)
                        {
                            count++;
                        }
                    }
                }
            }
            if (count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('没有选择任何审批人！！');", true); return check;
            }
            if (txt_zdrYJ.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('没有填写制单人意见！！');", true); return check;
            }
            return true;
        }

        /****************************对评审人进行勾选登记*************************************/
        private void bindReviewer()
        {
            int count = 0;
            string sql = string.Format("select ST_NAME,ST_ID,ST_DEPID from TBDS_STAFFINFO as a inner join TBCM_HT_SETTING as b on a.ST_ID=b.per_id where a.ST_PD='0'and b.dep_id='{0}' and per_sfjy='0' and b.per_type='0'", "01");
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            int num = dt.Rows.Count;
            for (int i = 0; i < 6 + num; i++)
            {
                CheckBoxList ck = (CheckBoxList)Panel1.FindControl("cki" + i.ToString());
                Label lb = (Label)Panel1.FindControl("dep" + i.ToString());
                if (ck != null)
                {
                    for (int j = 0; j < ck.Items.Count; j++)
                    {
                        if (ck.Items[j].Selected == true)
                        {
                            reviewer.Add(lb.Text, ck.Items[j].Value.ToString());//字典，绑定部门领导的编号
                            count++;
                        }
                    }
                }
            }

            /**********对领导进行勾选===通过金额************/

            //double cr_htje = Convert.ToDouble(txtPCON_JINE.Text.Trim());

            //string sqltext = "select REV_PERNM,REV_PERID from POWER_REVIEW where REV_CATEGORY='" + type + "' and REV_MINAM<=" + cr_htje + " and REV_MAXAM>" + cr_htje + "";

            //DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        reviewer_LD.Add(i, dt.Rows[i]["REV_PERID"].ToString());//字典，绑定的领导的编号                    
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请检查合同金额！！！\\r\\r没有找到对应的审批领导！！！');", true);//对应的金额在领导合同审批相应的区间找不到人
            //    return;
            //}
        }

        /****************************对绑定已经勾选的评审人*************************************/
        private void bindSelectReviewer()
        {
            string cr_id = LBpsdh.Text;//	评审单号
            string check_select = "select CRD_PID from TBCR_CONTRACTREVIEW_DETAIL where CRD_ID='" + cr_id + "' and CRD_PIDTYPE!='0'";
            DataTable sele = DBCallCommon.GetDTUsingSqlText(check_select);
            string sql = string.Format("select ST_NAME,ST_ID,ST_DEPID from TBDS_STAFFINFO as a inner join TBCM_HT_SETTING as b on a.ST_ID=b.per_id where a.ST_PD='0'and b.dep_id='{0}' and per_sfjy='0' and b.per_type='0'", "01");
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            int num = dt.Rows.Count;
            for (int i = 0; i < 6 + num; i++)
            {
                CheckBoxList ck = (CheckBoxList)Panel1.FindControl("cki" + i.ToString());
                for (int j = 0; j < sele.Rows.Count; j++)
                {
                    for (int k = 0; k < ck.Items.Count; k++)
                    {
                        if (ck.Items[k].Value == sele.Rows[j][0].ToString())
                        {
                            ck.Items[k].Selected = true;
                        }
                    }
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("CM_ContractView.aspx");
        }

        //责任部门绑定
        public void BindDep()
        {
            string sqltext = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE like '[0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            dplPCON_RSPDEPID.DataSource = dt;
            dplPCON_RSPDEPID.DataTextField = "DEP_NAME";
            dplPCON_RSPDEPID.DataValueField = "DEP_CODE";
            dplPCON_RSPDEPID.DataBind();
            dplPCON_RSPDEPID.Items.Insert(0, new ListItem("-请选择-", "%"));
            dplPCON_RSPDEPID.SelectedIndex = 0;
        }

        //选定的项目发生变化时
        protected void tb_pjinfo_Textchanged(object sender, EventArgs e)
        {
            //string pjname = "";
            //string pjid = "";
            //if (tb_pjinfo.Text.ToString().Contains("|"))
            //{
            //    pjname = tb_pjinfo.Text.Substring(0, tb_pjinfo.Text.ToString().IndexOf("|"));
            //    pjid = tb_pjinfo.Text.Substring(tb_pjinfo.Text.ToString().IndexOf("|") + 1);
            //    tb_pjinfo.Text = pjname;
            //    tb_pjid.Text = pjid;

            //    if (type != "1" || lbl_multipjname.Text.Trim() == "") //不是采购合同或项目名称为空时
            //    {
            //        lbl_multipjname.Text = pjname;
            //    }
            //}
            //else
            //{
            //    tb_pjinfo.Text = "";
            //    tb_pjid.Text = "";
            //    if (type != "1")  //不是采购合同时
            //    {
            //        lbl_multipjname.Text = "";
            //    }
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请正确填写项目！');", true); return;
            //}

            ////选择的项目改变，对应项目编号改变，同时重新绑定工程称           
            //if (tb_pjid.Text.Trim() != "")
            //{
            //    //this.BindEngName();
            //txtPCON_ENGNAME.Text = "";
            //txtPCON_ENGID.Text = "";
            //}

            //当不是销售合同和其他合同时，检查该项目是否已建立销售合同
            //if (type != "0" && type != "6") 
            //{                
            //    string check_XSHT = "select * from TBPM_CONPCHSINFO where PCON_FORM='0' and PCON_PJID='" + tb_pjid.Text + "'";
            //    DataTable dt_XSHT = DBCallCommon.GetDTUsingSqlText(check_XSHT);
            //    if (dt_XSHT.Rows.Count == 0)
            //    {

            //        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该项目尚未建立销售合同！！！\\r\\r请及时添加相应的销售合同！');", true);
            //    }
            //}

        }

        //合同编号创建
        private string CreateCode(string depid)
        {
            //根据责任部门创建合同号            
            string id = "";
            switch (type)
            {
                case "0": //销售合同
                    id = "TECTJ";
                    break;
                //case "1":  //采购合同
                //    id = "TECTJCG";
                //    break;
                case "1":  //委外合同
                    id = "TECTJWX";
                    break;
                case "3":
                    id = "TECTJCG";
                    break;
                case "4": //设备合同
                    id = "TECTJBG";
                    break;
                case "5":  //其他合同
                    id = "TECTJ" + depid + ".QT";
                    break;
                default:
                    break;
            }
            id += DateTime.Now.Year.ToString().Substring(2);
            string sqlText = "SELECT TOP 1 PCON_BCODE FROM (SELECT PCON_BCODE FROM TBPM_CONPCHSINFO UNION SELECT CON_NO AS PCON_BCODE FROM TBCM_TEMPCONNO ) AS A WHERE PCON_BCODE LIKE '" + id + "%'  ORDER BY PCON_BCODE DESC";//找出该类合同的最大合同号
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.HasRows)
            {
                dr.Read();
                string bh = string.Empty;
                if (string.IsNullOrEmpty(dr["PCON_BCODE"].ToString()))
                {
                    bh = "0";
                }
                if (type == "0")
                {
                    bh = dr["PCON_BCODE"].ToString().Substring(7);
                }
                else
                {
                    bh = dr["PCON_BCODE"].ToString().Substring(9);
                }
                int maxID = Convert.ToInt16(bh.Substring(0, 3)) + 1;
                id += maxID.ToString().PadLeft(3, '0');
                dr.Close();
            }
            else
            {
                id += "001";
            }
            return id;
        }

        protected void dplPCON_RSPDEPID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //选择责任部门后创建合同号
            string dep_code = "";
            if (dplPCON_RSPDEPID.SelectedIndex != 0)
            {
                switch (dplPCON_RSPDEPID.SelectedValue)
                {
                    case "02":
                        dep_code = "BG"; break;//办公
                    case "03":
                        dep_code = "JS"; break;//技术
                    case "04":
                        dep_code = "SC"; break;//生产
                    case "05":
                        dep_code = "CG"; break;//质量
                    case "06":
                        dep_code = "CW"; break;//采购
                    case "07":
                        dep_code = "SC"; break;//储运
                    case "10":
                        dep_code = "SB"; break; //安全
                }
                txtPCON_BCODE.Text = this.CreateCode(dep_code);
                //******************************
            }
            else
            {
                txtPCON_BCODE.Text = "";
            }

            //初始附件上传控件
            if (txtPCON_BCODE.Text.Trim() != "")
            {
                UploadAttachments1.Visible = true;
                UploadAttachments1.at_htbh = txtPCON_BCODE.Text;
                UploadAttachments1.at_type = 0;
                UploadAttachments1.at_sp = 0;
            }
            else
            {
                UploadAttachments1.Visible = false;
            }
        }

        /// <summary>
        /// 绑定工程名称
        /// </summary>
        //public void BindEngName()
        //{
        //    string sqlText = "select TSA_ID+'|'+TSA_ENGNAME AS TSA_ENGNAME,TSA_ID from TBPM_TCTSASSGN ";
        //    sqlText += "where TSA_PJID='" + tb_pjid.Text + "' and TSA_ID not like '%-%'";
        //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

        //    ddl_ENGNAME.Items.Clear();
        //    ddl_ENGNAME.DataSource = dt;
        //    ddl_ENGNAME.DataTextField = "TSA_ENGNAME";
        //    ddl_ENGNAME.DataValueField = "TSA_ID";
        //    ddl_ENGNAME.DataBind();
        //    ddl_ENGNAME.Items.Insert(0, new ListItem("-请选择-", ""));
        //    ddl_ENGNAME.SelectedIndex = 0;
        //}

        //多选工程名称
        //protected void ddl_ENGNAME_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddl_ENGNAME.SelectedIndex != 0)
        //    {
        //        string engname = ddl_ENGNAME.SelectedItem.Text.ToString();
        //        engname = engname.Substring(engname.IndexOf('|')+1, engname.Length - engname.IndexOf('|') - 1);
        //        if (txtPCON_ENGNAME.Text.ToString().Contains(engname))
        //        {
        //            ddl_ENGNAME.SelectedIndex = 0;
        //            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('已选择该工程！！');", true);                                       
        //            return;
        //        }
        //        else
        //        {
        //            txtPCON_ENGNAME.Text += (txtPCON_ENGNAME.Text.Trim() == "" ? "" : "、") + engname;
        //            txtPCON_ENGID.Text += (txtPCON_ENGID.Text.Trim() == "" ? "" : "、") + ddl_ENGNAME.SelectedValue;
        //            ddl_ENGNAME.SelectedIndex = 0;
        //        }
        //    }            
        //}

        //清除所选工程
        //protected void btn_clear_eng_Click(object sender, EventArgs e)
        //{
        //    txtPCON_ENGNAME.Text = "";
        //    txtPCON_ENGID.Text = "";
        //    ddl_ENGNAME.SelectedIndex = 0;
        //}

        //选择设备类型
        //protected void ddl_engtype_Changed(object sender, EventArgs e)
        //{
        //    if (txtPCON_ENGTYPE.Text.ToString().Contains(ddl_engtype.SelectedItem.Text.ToString()))
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('重复选择！！');", true);
        //        ddl_engtype.SelectedIndex = 0;
        //        return;
        //    }
        //    else
        //    {
        //        txtPCON_ENGTYPE.Text += (txtPCON_ENGTYPE.Text.Trim() == "" ? "" : "、") + ddl_engtype.SelectedItem.Text.ToString();
        //        ddl_engtype.SelectedIndex = 0;
        //    }
        //}



        //锁定当前合同号60分钟        
        protected void LinkLock_Click(object sender, EventArgs e)
        {
            /*******锁定前应该先在数据库中搜索一次，以确保页面上待锁定的合同号没有被人使用或先锁定*********/
            string pcon_bcode = txtPCON_BCODE.Text.Trim();
            string check_code = " select * from TBPM_CONPCHSINFO where PCON_BCODE='" + pcon_bcode + "'";
            DataTable dt_check = DBCallCommon.GetDTUsingSqlText(check_code);
            if (dt_check.Rows.Count > 0)
            {
                string dep_code = "";
                switch (dplPCON_RSPDEPID.SelectedValue)
                {
                    case "02":
                        dep_code = "BG"; break;//办公
                    case "03":
                        dep_code = "JS"; break;//技术
                    case "04":
                        dep_code = "SC"; break;//生产
                    case "05":
                        dep_code = "CG"; break;//质量
                    case "06":
                        dep_code = "CW"; break;//采购
                    case "07":
                        dep_code = "SC"; break;//储运
                    case "10":
                        dep_code = "SB"; break; //安全
                }
                string newid = this.CreateCode(dep_code);
                txtPCON_BCODE.Text = newid;
            }

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

        //取消锁定，放弃添加
        protected void btnNO_Click(object sender, EventArgs e)
        {
            string pcon_bcode = txtPCON_BCODE.Text.Trim();
            if (pcon_bcode != "")
            {
                string sqltext = "DELETE FROM TBCM_TEMPCONNO WHERE USER_NAME='" + Session["UserName"].ToString() + "' AND CON_NO='" + pcon_bcode + "'";
                DBCallCommon.ExeSqlText(sqltext);
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('放弃添加\\r合同号【" + pcon_bcode + "】取消锁定！');window.location.href='CM_ContractView.aspx';", true); return;
            }
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "window.location.href='CM_ContractView.aspx';", true);
        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            DataTable dt = GetTable();
            foreach (GridViewRow gr in GridHeSuan.Rows)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < 4; i++)
                {
                    dr[i] = ((ITextControl)gr.Cells[i].FindControl("txt" + i)).Text;
                }
                dt.Rows.Add(dr);
            }
            for (int i = 0; i < int.Parse(num.Text); i++)
            {
                dt.Rows.Add(dt.NewRow());
            }
            GridHeSuan.DataSource = dt;
            GridHeSuan.DataBind();
        }

        protected DataTable GetTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CM_XM");
            dt.Columns.Add("CM_ZL");
            dt.Columns.Add("CM_DJ");
            dt.Columns.Add("CM_ZJ");
            return dt;
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((LinkButton)sender).CommandArgument) - 1;
            DataTable dt = GetTable();
            for (int i = 0; i < GridHeSuan.Rows.Count; i++)
            {
                if (i != id)
                {
                    GridViewRow gr = GridHeSuan.Rows[i];
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < 4; j++)
                    {
                        dr[j] = ((ITextControl)gr.Cells[j].FindControl("txt" + j)).Text;
                    }
                    dt.Rows.Add(dr);
                }
            }
            GridHeSuan.DataSource = dt;
            GridHeSuan.DataBind();
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
