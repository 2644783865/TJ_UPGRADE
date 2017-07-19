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
    public partial class CM_ContractView_Other_Add : System.Web.UI.Page
    {
        Dictionary<int, string> reviewer = new Dictionary<int, string>();//用于存储审核部门负责人的名单
        Dictionary<int, string> reviewer_LD = new Dictionary<int, string>();//用于存储审核领导的名单
        string action = string.Empty;
        string id = string.Empty;
        string type = string.Empty;
        string contractid = string.Empty;
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

            if (Request.QueryString["Conid"] != null)
                contractid = Request.QueryString["Conid"].ToString();

            /***************由于视图的存在，必须先赋值*************************/
            getLeaderInfo();
            /****************************************/
            if (!this.IsPostBack)
            {
                this.InitPage();

            }
            this.InitUpload();
        }
        //初始化附件上传控件
        private void InitUpload()
        {
            if (LBpsdh.Text.Trim() != "")
            {
                UploadAttachments1.Visible = true;
                UploadAttachments1.at_htbh = LBpsdh.Text.Trim();//评审单号
                UploadAttachments1.at_type = 2;
                UploadAttachments1.at_sp = 0;
            }
            else
            {
                UploadAttachments1.Visible = false;
            }
        }

        private void InitPage()
        {
            if (action == "add")
            {
                lblState.Text = "添加评审合同信息-" + ContractType(type);
                LBpsdh.Text = DateTime.Now.ToString("yyyyMMddhhmmss");

                //根据合同号带出相关信息
                if (contractid != null && contractid != "")
                {
                    string sql_coninfo = "SELECT PCON_PJNAME+'('+PCON_PJID+')' AS PJNAME,PCON_CUSTMNAME AS CUSTMNAME FROM TBPM_CONPCHSINFO WHERE PCON_BCODE='" + contractid + "'";
                    DataTable dt_coninfo = DBCallCommon.GetDTUsingSqlText(sql_coninfo);
                    if (dt_coninfo.Rows.Count > 0)
                    {
                        tb_conid.Text = this.CreateConid();
                        TextFBS.Text = dt_coninfo.Rows[0]["CUSTMNAME"].ToString();
                        TextXMMC.Text = dt_coninfo.Rows[0]["PJNAME"].ToString();
                    }
                }
            }
            else if (action == "edit")
            {
                lblState.Text = "修改评审合同信息-" + ContractType(type);
                BindBaseData();
                bindSelectReviewer();
            }
        }

        //生成协议编号
        private string CreateConid()
        {
            string id = "";
            string sqlText = "SELECT TOP 1 CON_ID FROM TBCM_ADDCON " +
                " WHERE CON_ID LIKE '" + contractid + "%'  ORDER BY CON_ID DESC";//找出最大号
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.HasRows)
            {
                dr.Read();
                string[] bh = dr["CON_ID"].ToString().Split('-');
                int maxID = Convert.ToInt16(bh[bh.Length - 1]) + 1;//取数组最后一个
                dr.Close();
                id += maxID.ToString();
            }
            else
            {
                id += "1";
            }
            id = contractid + "-" + id;
            return id;
        }

        //绑定基本信息
        private void BindBaseData()
        {
            //基本信息
            string sqltext = "select a.*,b.CRD_NOTE from TBCM_ADDCON as a,TBCR_CONTRACTREVIEW_DETAIL as b" +
                " where a.CR_ID='" + id + "' and a.CR_ID=b.CRD_ID and b.CRD_PID='" + Session["UserID"].ToString() + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.HasRows)
            {
                dr.Read();
                LBpsdh.Text = dr["CR_ID"].ToString();
                tb_conid.Text = dr["CON_ID"].ToString();
                TextXMMC.Text = dr["CON_XMMC"].ToString();
                TextSBMC.Text = dr["CON_SBMC"].ToString();

                TextJE.Text = dr["CON_JIN"].ToString();

                txt_zdrYJ.Text = dr["CRD_NOTE"].ToString();
                TextFBS.Text = dr["CON_FBSMC"].ToString();
                TextFBFW.Text = dr["CON_FBFW"].ToString();
                txt_BZ.Text = dr["CON_NOTE"].ToString();
                ddl_htlx.SelectedValue = dr["CON_TYPE"].ToString();

                dr.Close();
            }
        }

        //类型转换
        private string ContractType(string PS_type)
        {
            string CH_type = "";
            switch (PS_type)
            {
                case "0": CH_type = "销售合同";
                    break;
                case "1": CH_type = "生产外协";
                    break;
                case "2": CH_type = "厂内分包";
                    break;
                case "3": CH_type = "采购合同";
                    break;
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
            getStaffInfo("03", "技术质量部", 0);
            getStaffInfo("05", "采购部", 1);
            getStaffInfo("04", "生产管理部", 2);
            getStaffInfo("06", "财务部", 3);
            getStaffInfo("07", "市场部", 4);
            getStaffInfo("01", "公司领导", 5);
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bindReviewer();//将数据读出

            if (Check_MustPutIN())
            {
                //待插入数据*****************
                string con_id = tb_conid.Text.Trim();//补充协议编号
                string cr_xmmc = TextXMMC.Text.Trim();//项目名称
                string cr_id = LBpsdh.Text;//	评审单号	     
                string cr_sbmc = TextSBMC.Text;//设备名称
                string cr_zdr = Session["UserID"].ToString();//	制单人	
                string cr_zdrq = DateTime.Now.ToString("yyyy-MM-dd");//起草时间
                string cr_zdrYJ = txt_zdrYJ.Text;//制单人意见
                double cr_htje = Convert.ToDouble(TextJE.Text.Trim()); //	评审合同金额	
                string cr_lb = type;//评审合同类别
                string cr_note = txt_BZ.Text.ToString();
                string con_type = ddl_htlx.SelectedValue.ToString();
                string cr_fbs = TextFBS.Text.Trim();//分包商
                string cr_cbfw = TextFBFW.Text.Trim();//分包范围
                //**********************

                string sql_insert = "";
                List<string> strb_sql = new List<string>();
                //需要特别注意——添加或更新前先将原来的记录删除，再重新插入一次记录
                string sql_del = "";
                sql_del = "delete from TBCM_ADDCON where CR_ID='" + cr_id + "'";
                strb_sql.Add(sql_del);
                sql_del = "delete from TBCR_CONTRACTREVIEW_DETAIL where CRD_ID='" + cr_id + "'";
                strb_sql.Add(sql_del);
                DBCallCommon.ExecuteTrans(strb_sql);
                strb_sql.Clear();
                //要先执行删除再添加，否则出错，删除后清空list<string>


                //信息总表TBCM_ADDCON
                //插入前判断补充协议编号是否存在，如存在不能保存                

                string sqlcheckconid = "SELECT * FROM TBCM_ADDCON WHERE CON_ID='" + con_id + "'";
                DataTable dt_conid = DBCallCommon.GetDTUsingSqlText(sqlcheckconid);
                if (dt_conid.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('补充协议编号已存在，请修改编号，或检查是否重复添加！！！');", true); return;
                }
                else
                {

                    string sql_psd = "insert into TBCM_ADDCON(CR_ID,CON_ID,CON_XMMC,CON_SBMC,CON_FBSMC,CON_FBFW,CON_HTLX,CON_JIN,CON_ZDR,CON_ZDRQ,CON_PSZT,CON_NOTE,CON_TYPE) " +
                        " values('" + cr_id + "','" + con_id + "','" + cr_xmmc + "','" + cr_sbmc + "','" + cr_fbs + "','" + cr_cbfw + "','" + cr_lb + "'," + cr_htje + ",'" + cr_zdr + "','" + cr_zdrq + "','1','" + cr_note + "','" + con_type + "')";
                    strb_sql.Add(sql_psd);

                    //合同评审信息详细表(TBCR_CONTRACTREVIEW_DETAIL)
                    sql_insert = "insert into TBCR_CONTRACTREVIEW_DETAIL (CRD_ID,CRD_PID,CRD_PSYJ,CRD_NOTE,CRD_SHSJ,CRD_DEP,CRD_PIDTYPE) values" +
                            " ('" + cr_id + "','" + cr_zdr + "','2','" + cr_zdrYJ + "','" + cr_zdrq + "'," +
                            " '" + Session["UserDeptID"].ToString() + "','0')";
                    strb_sql.Add(sql_insert);  //制单人意见

                    for (int i = 0; i < reviewer.Count; i++)
                    {
                        /******************通过键来找值******************************************************/
                        /**为兼容领导同时为部门负责人的情况，评审人部门编号要以评审人员设置表中为准，而不以当前登录人部门编号为依据**/
                        string sql_dep = "select dep_id from TBCM_HT_SETTING where per_id='" + reviewer.Values.ElementAt(i) + "'";
                        DataTable dt_dep = DBCallCommon.GetDTUsingSqlText(sql_dep);
                        if (dt_dep.Rows.Count > 0)
                        {
                            sql_insert = "insert into TBCR_CONTRACTREVIEW_DETAIL (CRD_ID,CRD_PID,CRD_DEP,CRD_PIDTYPE) values" +
                                " ('" + cr_id + "','" + reviewer.Values.ElementAt(i) + "','" + dt_dep.Rows[0]["dep_id"].ToString() + "','1')";
                            strb_sql.Add(sql_insert);//其他人
                        }
                    }
                    //if (reviewer_LD.Count > 0)
                    //{

                    for (int i = 0; i < reviewer_LD.Count; i++)
                    {
                        /******************通过键来找值******************************************************/
                        sql_insert = "insert into TBCR_CONTRACTREVIEW_DETAIL (CRD_ID,CRD_PID,CRD_DEP,CRD_PIDTYPE) values" +
                                 " ('" + cr_id + "','" + reviewer_LD.Values.ElementAt(i) + "','" + reviewer_LD.Values.ElementAt(i).Substring(0, 2) + "','1')";
                        strb_sql.Add(sql_insert);//其他人

                    }
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('找不到对应的审批领导！！！\\r请检查合同金额与合同类型！');", true);//对应的金额在领导合同审批相应的区间找不到人
                    //    return;
                    //}

                    DBCallCommon.ExecuteTrans(strb_sql);

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交成功！！！');window.location.href='CM_ContractView.aspx';", true); return;
                }
            }

        }

        private bool Check_MustPutIN()
        {
            bool check = true;
            int count = 0;
            for (int i = 0; i < 7; i++)
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
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您还没有选择审批人！');", true); return check;
            }
            if (txt_zdrYJ.Text.Trim() == "")
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('没有填写制单人意见！！');", true); return check;
            }
            if (ddl_htlx.SelectedIndex == 0)
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择合同类型！！');", true); return check;
            }

            if (!tb_conid.Text.Trim().Contains('-'))
            {
                check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('为区分于主合同，请在补充协议编号后加上【-1】、【-2】等！！');", true); return check;
            }

            return check;


        }
        /****************************对评审人进行勾选登记*************************************/
        private void bindReviewer()
        {
            int count = 0;
            string sql_psbm = "select distinct dep_id,b.dep_name from TBCM_HT_SETTING as a left join TBDS_DEPINFO as b on a.dep_id=b.DEP_CODE";
            DataTable dt_psbm = DBCallCommon.GetDTUsingSqlText(sql_psbm);
            for (int i = 0; i < dt_psbm.Rows.Count; i++)
            {
                CheckBoxList ck = (CheckBoxList)Panel1.FindControl("cki" + i.ToString());
                if (ck != null)
                {
                    for (int j = 0; j < ck.Items.Count; j++)
                    {
                        if (ck.Items[j].Selected == true)
                        {
                            reviewer.Add(count, ck.Items[j].Value.ToString());//字典，绑定部门领导的编号
                            count++;
                        }
                    }
                }
            }

            /**********对领导进行勾选===通过金额************/

            double cr_htje = Convert.ToDouble(TextJE.Text.Trim());

            ////取金额的绝对值
            //cr_htje = Math.Abs(cr_htje);

            //string sqltext = "select REV_PERNM,REV_PERID from POWER_REVIEW where REV_CATEGORY='" + ddl_htlx.SelectedValue + "' and REV_MINAM<=" + cr_htje + " and REV_MAXAM>" + cr_htje + "";

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
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('找不到对应的审批领导！！！\\r请检查合同金额与合同类型！');", true);//对应的金额在领导合同审批相应的区间找不到人
            //    return;
            //}
        }

        /****************************对绑定已经勾选的评审人*************************************/
        private void bindSelectReviewer()
        {
            string cr_id = LBpsdh.Text;//	评审单号
            string sql_psbm = "select distinct dep_id,b.dep_name from TBCM_HT_SETTING as a left join TBDS_DEPINFO as b on a.dep_id=b.DEP_CODE";
            DataTable dt_psbm = DBCallCommon.GetDTUsingSqlText(sql_psbm);
            for (int i = 0; i < dt_psbm.Rows.Count; i++)
            {
                CheckBoxList ck = (CheckBoxList)Panel1.FindControl("cki" + i.ToString());
                if (ck != null)
                {
                    for (int j = 0; j < ck.Items.Count; j++)
                    {
                        string check_select = "select *  from TBCR_CONTRACTREVIEW_DETAIL where" +
                            " CRD_ID='" + cr_id + "' and CRD_PID='" + ck.Items[j].Value + "' and CRD_DEP!='01'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(check_select);
                        if (dt.Rows.Count > 0)
                        {
                            ck.Items[j].Selected = true;
                        }
                    }
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("CM_ContractView.aspx");
        }
    }
}