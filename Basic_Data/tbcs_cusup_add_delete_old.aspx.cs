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
using System.IO;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class tbcs_cusup_add_delete_old : System.Web.UI.Page
    {
        public string action = "";
        public string id = "";
        public string cs_action = "";
        string idid = "";
        string filePath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"].ToString();
            /*因添加删除可能是同一公司，公司ID不唯一，所以要区分审批类别 0:添加；1:删除                 
                */
            cs_action = Request.QueryString["cs_action"];
            if (action == "Delete" || action == "Review" || action == "View" || action == "Edit") //删除，审批，查看审批情况
            {
                id = Request.QueryString["id"].ToString();//获取公司ID

            }
            else
            {
                GetCSCODE();
            }

            GVBind();

            if (!IsPostBack)
            {
                this.InitPage();
            }
        }

        //初始化页面
        private void InitPage()
        {
            if (action == "View")
            {
                //在第一次加载时，如果不为添加，则需要根据id绑定数据
                this.GetDataByID(id);
                Pal_info.Enabled = false;
                // btnCreatID.Visible = false;//创建公司编号不可见
                Pal_person.Visible = false;//选择审批人不可见
                dopCS_TYPE.Visible = false;//选择客户类型不可见
                dopCL_LOCATION.Visible = false;
                dopCL_LOCATION_NEXT.Visible = false;//地区选择不可见
                Pal_Review.Enabled = false;
                //dopCS_RANK.Visible = false;//重要等级选择框不可见
                btnLoad.Visible = false;//查看时不可提交

                FileUpload1.Visible = false;
                bntupload.Visible = false;
                for (int i = 0; i <= gvfileslist.Rows.Count - 1; i++)
                {
                    ImageButton imgbtn = (ImageButton)gvfileslist.Rows[i].FindControl("imgbtndelete");
                    imgbtn.Visible = false;
                }

                SHRXXJZ();

            }
            else if (action == "Delete")
            {
                if (Session["UserGroup"].ToString().Contains("管理员"))
                {
                    Tab_Review.Visible = false;
                    this.GetDataByID(id);
                    Pal_info.Enabled = false;
                    // btnCreatID.Visible = false;//创建公司编号不可见
                    //dopCS_RANK.Visible = false;//重要等级选择框不可见
                    dopCS_TYPE.Visible = false;//选择客户类型不可见
                    dopCL_LOCATION.Visible = false;
                    dopCL_LOCATION_NEXT.Visible = false;//地区选择不可见
                    Pal_Result.Visible = false;//删除时意见不可见
                    //添加申请人和申请时间，添加后不可更改
                    txtCS_MANCLERK.Text = Session["UserName"].ToString();
                    txtCS_FILLDATE.Value = System.DateTime.Now.ToString();
                }
                else
                {
                    //在第一次加载时，如果不为添加，则需要根据id绑定数据
                    this.GetDataByID(id);
                    Pal_info.Enabled = false;
                    // btnCreatID.Visible = false;//创建公司编号不可见
                    //dopCS_RANK.Visible = false;//重要等级选择框不可见
                    dopCS_TYPE.Visible = false;//选择客户类型不可见
                    dopCL_LOCATION.Visible = false;
                    dopCL_LOCATION_NEXT.Visible = false;//地区选择不可见
                    Pal_Result.Visible = false;//删除时意见不可见


                    shanchu_beizhu_table.Visible = true;//删除原因


                    bind_Per();

                    //添加申请人和申请时间，添加后不可更改
                    txtCS_MANCLERK.Text = Session["UserName"].ToString();
                    txtCS_FILLDATE.Value = System.DateTime.Now.ToString();
                }
            }
            else if (action == "Edit")
            {
                //在第一次加载时，如果不为添加，则需要根据id绑定数据
                this.GetDataByID(id);
                // btnCreatID.Visible = false;//创建公司编号不可见
                dopCL_LOCATION.Visible = false;
                dopCL_LOCATION_NEXT.Visible = false;//地区选择不可见
                /****公司编号，公司地区等不可编辑****/
                txtCS_CODE.Enabled = false;
                txtCS_LOCATION.Enabled = false;
                bind_Per();
                SHRXXJZ();
                if (cs_action == "1")
                {
                    Pal_info.Enabled = false;
                }
                Pal_Result.Enabled = false;
            }
            else if (action == "Review")
            {
                //在第一次加载时，如果不为添加，则需要根据id绑定数据
                this.GetDataByID(id);
                Pal_info.Enabled = false;
                //dopCS_RANK.Visible = false;//重要等级选择框不可见
                //btnCreatID.Visible = false;//创建公司编号不可见
                Pal_person.Visible = false; ;//选择审批人不可见
                dopCS_TYPE.Visible = false;//选择客户类型不可见
                dopCL_LOCATION.Visible = false;
                dopCL_LOCATION_NEXT.Visible = false;//地区选择不可见

                shanchu_beizhu_table.Visible = true;
                shanchu_beizhu.Enabled= false;

                SHRXXJZ();
                string currentguy_name = Session["UserName"].ToString();
                string currentguy_dep = Session["UserDeptID"].ToString();
                string tcbm = "";
                //找出提出部门ID
                string selectBM = "select ST_DEPID from TBDS_STAFFINFO a,TBCS_CUSUP_ReView b where a.st_id=b.csr_person and b.fatherid='" + id + "'";
                SqlDataReader drbm = DBCallCommon.GetDRUsingSqlText(selectBM);
                if (drbm.Read())
                {
                    tcbm = drbm["st_depid"].ToString(); drbm.Close();
                }
                if (currentguy_dep == tcbm && currentguy_name == TextSHR1.Text.ToString())
                {
                    EJSHEnalbedF();
                    SJSHEnabledF();
                }
                else if (currentguy_dep == "01" && currentguy_name == TextSHR2.Text.ToString())
                {
                    BMSHEnabledF();
                    SJSHEnabledF();
                }
                else if (currentguy_dep == "01" && currentguy_name == TextSHR3.Text.ToString())
                {
                    BMSHEnabledF();
                    EJSHEnalbedF();
                }
                else
                {
                    BMSHEnabledF();
                    EJSHEnalbedF();
                    SJSHEnabledF();
                }
                shenheliucheng();
            }

            else if (action == "Add")
            {
                // GetCSCODE();
                if (Session["UserGroup"].ToString().Contains("管理员"))
                {
                    Tab_Review.Visible = false;
                    //绑定地区
                    this.GetLocationData();
                    this.GetLocationNextData();

                    //添加申请人和申请时间，添加后不可更改
                    txtCS_MANCLERK.Text = Session["UserName"].ToString();
                    txtCS_FILLDATE.Value = System.DateTime.Now.ToString();
                }
                else
                {
                    Pal_Result.Visible = false;//审批结果不可见
                    //绑定地区
                    this.GetLocationData();
                    this.GetLocationNextData();
                    bind_Per();

                    //添加申请人和申请时间，添加后不可更改
                    txtCS_MANCLERK.Text = Session["UserName"].ToString();
                    txtCS_FILLDATE.Value = System.DateTime.Now.ToString();
                    if (Session["UserDeptID"].ToString() == "06")
                    {
                        dopCS_TYPE.SelectedValue = "2";
                        txtCS_TYPE.Text = dopCS_TYPE.SelectedItem.Text;
                    }
                    if (Session["UserDeptID"].ToString() == "03")
                    {
                        dopCS_TYPE.SelectedValue = "4";
                        txtCS_TYPE.Text = dopCS_TYPE.SelectedItem.Text;
                    }
                    if (Session["UserDeptID"].ToString() == "04")
                    {
                        dopCS_TYPE.SelectedValue = "5";
                        txtCS_TYPE.Text = dopCS_TYPE.SelectedItem.Text;
                    }
                    if (Session["UserDeptID"].ToString() == "07")
                    {
                        dopCS_TYPE.SelectedValue = "6";
                        txtCS_TYPE.Text = dopCS_TYPE.SelectedItem.Text;
                    }
                }

            }
        }

        protected void GetCSCODE()
        {
            string sqltext = "select TOP 1 (CS_CODE+0) AS index1 from TBCS_CUSUP_ADD_DELETE ORDER BY index1 DESC ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int index;
            if (dt.Rows.Count > 0)
            {
                index = Convert.ToInt32(dt.Rows[0]["index1"].ToString());
            }
            else
            {
                index = 0;
            }
            string code = (index + 1).ToString();
            txtCS_CODE.Text = code;
        }
        //绑定Pal_per中指定审批人中的信息
        private void bind_Per()
        {
            //绑定部门
            string sqlBM = "select DEP_NAME,DEP_CODE from  TBDS_DEPINFO where DEP_FATHERID='0'and DEP_CODE not in('01','02')";
            DataTable dtBM = DBCallCommon.GetDTUsingSqlText(sqlBM);
            ddl_oneBM.DataSource = dtBM;
            ddl_oneBM.DataTextField = "DEP_NAME";
            ddl_oneBM.DataValueField = "DEP_CODE";
            ddl_oneBM.DataBind();
            ddl_oneBM.Items.Insert(0, new ListItem("-请选择-", "%"));
            ddl_oneBM.SelectedIndex = 0;

            //绑定部门审核人，从部门领导中筛选
            string sql1 = "select ST_NAME,ST_ID from  TBDS_STAFFINFO where ST_DEPID like '" + ddl_oneBM.SelectedValue + "%' and ST_PD=0";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
            ddl_onePer.DataSource = dt1;
            ddl_onePer.DataTextField = "ST_NAME";
            ddl_onePer.DataValueField = "ST_ID";
            ddl_onePer.DataBind();
            ddl_onePer.Items.Insert(0, new ListItem("-请选择-", "%"));
            ddl_onePer.SelectedIndex = 0;

            //绑定二级审批人，从领导中选取
            string sql2 = "select ST_NAME,ST_ID from  TBDS_STAFFINFO where (ST_DEPID ='01' or ST_DEPID='" + ddl_oneBM.SelectedValue.Trim() + "') and ST_PD=0";
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql2);
            ddl_two.DataSource = dt2;
            ddl_two.DataTextField = "ST_NAME";
            ddl_two.DataValueField = "ST_ID";
            ddl_two.DataBind();
            ddl_two.Items.Insert(0, new ListItem("-请选择-", "%"));
            ddl_two.SelectedIndex = 0;

            //绑定三级审批人，从领导人选取
            string sql3 = "select ST_NAME,ST_ID from  TBDS_STAFFINFO where (ST_DEPID ='01' or ST_DEPID='" + ddl_oneBM.SelectedValue.Trim() + "') and ST_PD=0";
            DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sql3);
            ddl_three.DataSource = dt3;
            ddl_three.DataTextField = "ST_NAME";
            ddl_three.DataValueField = "ST_ID";
            ddl_three.DataBind();
            ddl_three.Items.Insert(0, new ListItem("-请选择-", "%"));
            ddl_three.SelectedIndex = 0;
        }

        //根据部门绑定一级评审人
        protected void ddl_oneBM_SelectedIndexChanged(object sender, EventArgs e)
        {
            //绑定部门审核人，从部门领导中筛选
            string sql1 = "select ST_NAME,ST_ID from  TBDS_STAFFINFO where ST_DEPID like '" + ddl_oneBM.SelectedValue + "%' and ST_PD=0";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
            ddl_onePer.DataSource = dt1;
            ddl_onePer.DataTextField = "ST_NAME";
            ddl_onePer.DataValueField = "ST_ID";
            ddl_onePer.DataBind();
            ddl_onePer.Items.Insert(0, new ListItem("-请选择-", "%"));
            ddl_onePer.SelectedIndex = 0;


            //绑定二级审批人，从领导中选取
            string sql2 = "select ST_NAME,ST_ID from  TBDS_STAFFINFO where (ST_DEPID ='01' or ST_DEPID='" + ddl_oneBM.SelectedValue.Trim() + "') and ST_PD=0";
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql2);
            ddl_two.DataSource = dt2;
            ddl_two.DataTextField = "ST_NAME";
            ddl_two.DataValueField = "ST_ID";
            ddl_two.DataBind();
            ddl_two.Items.Insert(0, new ListItem("-请选择-", "%"));
            ddl_two.SelectedIndex = 0;

            //绑定三级审批人，从领导人选取
            string sql3 = "select ST_NAME,ST_ID from  TBDS_STAFFINFO where (ST_DEPID ='01' or ST_DEPID='" + ddl_oneBM.SelectedValue.Trim() + "') and ST_PD=0";
            DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sql3);
            ddl_three.DataSource = dt3;
            ddl_three.DataTextField = "ST_NAME";
            ddl_three.DataValueField = "ST_ID";
            ddl_three.DataBind();
            ddl_three.Items.Insert(0, new ListItem("-请选择-", "%"));
            ddl_three.SelectedIndex = 0;
        }

        //加载审批人信息
        private void SHRXXJZ()
        {
            string sqltext = "select ST_ID,ST_NAME,DEP_CODE,DEP_NAME,CSR_TIME,CSR_YJ,CSR_NOTE,CSR_TYPE from TBCS_CUSUP_ReView a,TBDS_STAFFINFO b,TBDS_DEPINFO c" +
                             " where a.CSR_PERSON=b.ST_ID and b.ST_DEPID=c.DEP_CODE and fatherid=(select id from TBCS_CUSUP_ADD_DELETE where id='" + id + "' and CS_ACTION='" + cs_action + "')";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string person = dt.Rows[i]["ST_NAME"].ToString();
                    string time = dt.Rows[i]["CSR_TIME"].ToString();
                    string yj = dt.Rows[i]["CSR_YJ"].ToString();
                    string note = dt.Rows[i]["CSR_NOTE"].ToString();
                    string type = dt.Rows[i]["CSR_TYPE"].ToString();
                    string bumen = dt.Rows[i]["DEP_NAME"].ToString();

                    if (type == "1")//部门审批
                    {
                        ddl_oneBM.SelectedValue = dt.Rows[i]["DEP_CODE"].ToString();
                        TextSHR1.Text = person;
                        ddl_onePer.SelectedValue = dt.Rows[i]["ST_ID"].ToString();
                        TextSHRQ1.Text = time != "" ? time : "";
                        TextBZ1.Text = note;
                        lbl_tcbm.Text = bumen;
                        if (yj == "1")//同意
                        {
                            RadioTY1.Checked = true;
                        }
                        else if (yj == "2")
                        {
                            RadioJJ1.Checked = true;
                        }
                    }
                    else if (type == "2")//二级审批
                    {
                        TextSHR2.Text = person;
                        ddl_two.SelectedValue = dt.Rows[i]["ST_ID"].ToString();
                        TextSHRQ2.Text = time != "" ? time : "";
                        TextBZ2.Text = note;
                        if (yj == "1")//同意
                        {
                            RadioTY2.Checked = true;
                        }
                        else if (yj == "2")
                        {
                            RadioJJ2.Checked = true;
                        }
                    }
                    else if (type == "3") //二级审批
                    {
                        TextSHR3.Text = person;
                        ddl_three.SelectedValue = dt.Rows[i]["ST_ID"].ToString();
                        TextSHRQ3.Text = time != "" ? time : "";
                        TextBZ3.Text = note;
                        if (yj == "1")//同意
                        {
                            RadioTY3.Checked = true;
                        }
                        else if (yj == "2")
                        {
                            RadioJJ3.Checked = true;
                        }
                    }
                }
            }
        }


        //控制审批流程
        private void shenheliucheng()
        {
            string sqltext = "select CS_CODE,CSR_YJ,CSR_TYPE from TBCS_CUSUP_ReView " +
                             " where  CSR_PERSON='" + Session["UserID"].ToString() + "' and  fatherid='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                string CS_CODE = dt.Rows[0]["CS_CODE"].ToString();
                string psyj = dt.Rows[0]["CSR_YJ"].ToString();
                string type = dt.Rows[0]["CSR_TYPE"].ToString();
                if (type == "2")//二级审核
                {
                    string sql = "select CSR_YJ from TBCS_CUSUP_ReView where fatherid='" + id + "' and CSR_TYPE='1'";
                    SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sql);

                    if (dr1.Read() && dr1["CSR_YJ"].ToString().Trim() != "1")//如果部门负责人没审核或者没同意，则不能审核
                    {
                        ERJI.Enabled = false; btnLoad.Enabled = false;
                    }
                    dr1.Close();
                }
                else if (type == "3")//三级审核
                {
                    string sql = "select CSR_YJ from TBCS_CUSUP_ReView where fatherid='" + id + "' and CSR_TYPE='2'";
                    SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sql);

                    if (dr1.Read() && dr1["CSR_YJ"].ToString().Trim() != "1")//如果二级负责人没审核或者没同意，则不能审核
                    {
                        SANJI.Enabled = false; btnLoad.Enabled = false;
                    }
                    dr1.Close();
                }
            }
        }

        /// <summary>
        /// 从地区信息表中绑定地区信息
        /// </summary>
        protected void GetLocationData()
        {
            string sqltext = "select distinct CL_NAME,CL_CODE from TBCS_LOCINFO where CL_FATHERCODE='ROOT'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            dopCL_LOCATION.Items.Add(new ListItem("-未选择-", "请选择省份和市/区信息！"));
            while (dr.Read())
            {
                //分别将CL_NAME和CL_CODE绑定到Text和Value
                dopCL_LOCATION.Items.Add(new ListItem(dr["CL_NAME"].ToString(), dr["CL_CODE"].ToString()));
            }
            dr.Close();

        }
        /// <summary>
        /// 从地区表中绑定二级地区信息
        /// </summary>
        protected void GetLocationNextData()
        {
            dopCL_LOCATION_NEXT.Items.Clear();
            dopCL_LOCATION_NEXT.Items.Add(new ListItem("-未选择-", "请选择省份和市/区信息！"));
            if (dopCL_LOCATION.SelectedIndex != 0)
            {
                string fathercode = dopCL_LOCATION.SelectedValue;
                string sqltext = "select distinct CL_NAME,CL_CODE from TBCS_LOCINFO where CL_FATHERCODE='" + fathercode + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr.Read())
                {
                    dopCL_LOCATION_NEXT.Items.Add(new ListItem(dr["CL_NAME"].ToString(), dr["CL_CODE"].ToString()));
                }
                dr.Close();
            }
            dopCL_LOCATION_NEXT.SelectedIndex = 0;
        }

        protected void dopCL_LOCATION_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetLocationNextData();
            txtCS_LOCATION.Text = "";
            txtCS_CODE.Text = "";
            if (dopCL_LOCATION.SelectedIndex == 0)
            {
                Response.Write("<script>alert('请选择省份！')</script>");
                txtCS_LOCATION.Text = "";
            }
        }

        protected void dopCL_LOCATION_NEXT_TextChanged(object sender, EventArgs e)
        {
            if (dopCL_LOCATION_NEXT.SelectedIndex == 0)
            {
                Response.Write("<script>alert('请选择（市/区）！')</script>");
                txtCS_LOCATION.Text = "";
                txtCS_CODE.Text = "";
            }
            else
            {
                txtCS_LOCATION.Text = dopCL_LOCATION.SelectedItem.Text + dopCL_LOCATION_NEXT.SelectedItem.Text;
                txtCS_CODE.Text = "";
            }
        }

        protected void dopCS_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dopCS_TYPE.SelectedIndex == 0)
            {
                Response.Write("<script>alert('请选择公司所属类别！')</script>");
                txtCS_TYPE.Text = "";
            }
            else
            {
                txtCS_TYPE.Text = dopCS_TYPE.SelectedItem.Text;
            }
        }

        //protected void dopCS_RANK_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (dopCS_RANK.SelectedIndex == 0)
        //    {
        //        txtCS_RANK.Text = "";
        //    }
        //    else
        //    {
        //        txtCS_RANK.Text = dopCS_RANK.SelectedItem.Text;
        //    }
        //}
        /// <summary>
        /// 按id读出数据
        /// </summary>
        protected void GetDataByID(string id)
        {
            string sqltext = "";
            if (action == "Review" || action == "View" || action == "Edit")
            {
                sqltext = "select * from TBCS_CUSUP_Add_DELETE where id='" + id + "'";
            }
            else if (action == "Delete")
            {
                sqltext = "select * from TBCS_CUSUPINFO where CS_CODE='" + id + "'";
            }
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            string[] date = new string[2];
            while (dr.Read())
            {
                if (action == "Review" || action == "View" || action == "Edit")
                {
                    txtCS_CODE.Text = dr["CS_CODE"].ToString();//次数说表示的值与状态有关
                }
                else
                {
                    txtCS_CODE.Text = id;
                }
                txtCS_NAME.Text = dr["CS_NAME"].ToString();
                txtCS_LOCATION.Text = dr["CS_LOCATION"].ToString();
                txtCS_HRCODE.Text = dr["CS_HRCODE"].ToString();
                dopCS_TYPE.SelectedValue = dr["CS_TYPE"].ToString();
                switch (dr["CS_TYPE"].ToString())
                {
                    case "1":
                        txtCS_TYPE.Text = "客户";
                        break;
                    case "2":
                        txtCS_TYPE.Text = "采购供应商";
                        break;
                    case "3":
                        txtCS_TYPE.Text = "运输公司";
                        break;
                    case "4":
                        txtCS_TYPE.Text = "技术外协分包商";
                        break;
                    case "5":
                        txtCS_TYPE.Text = "生产外协分包商";
                        break;
                    case "6":
                        txtCS_TYPE.Text = "原材料销售供应商";
                        break;
                    case "7":
                        txtCS_TYPE.Text = "其它";
                        break;
                    //case "3":
                    //    txtCS_TYPE.Text = "客户和供应商";
                    //    break;
                }

                txtCS_COREBS.Text = dr["CS_COREBS"].ToString();
                txtCS_ADDRESS.Text = dr["CS_ADDRESS"].ToString();
                txtCS_PHONO.Text = dr["CS_PHONO"].ToString();
                //txtCS_RANK.Text = dr["CS_RANK"].ToString();
                //dopCS_RANK.SelectedValue = dr["CS_RANK"].ToString();
                txtCS_ZIP.Text = dr["CS_ZIP"].ToString();
                txtCS_FAX.Text = dr["CS_FAX"].ToString();
                txtCS_NOTE.Text = dr["CS_NOTE"].ToString();
                shanchu_beizhu.Text = dr["CS_NOTEDELETE"].ToString();
                txtCS_MANCLERK.Text = dr["CS_MANCLERK"].ToString();
                txtCS_CONNAME.Text = dr["CS_CONNAME"].ToString();
                txtCS_FILLDATE.Value = dr["CS_FILLDATE"].ToString();
                txtCS_MAIL.Text = dr["CS_MAIL"].ToString();
                txtCS_BANK.Text = dr["CS_BANK"].ToString();
                txtCS_ACCOUNT.Text = dr["CS_ACCOUNT"].ToString();
                txtCS_TAX.Text = dr["CS_TAX"].ToString();
                TB_Scope.Text = dr["CS_Scope"].ToString();
            }
            dr.Close();
        }
        /// <summary>
        /// 提交事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            //if (Session["UserGroup"].ToString() == "'管理员'")
            //{
            //    EXESQL();
            //}
            //检查审批人
            //else
            //{
            if (check_PER())
            {
                EXESQL();
            }

            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('请选择审批人！');", true);

            }
            //}
        }

        private bool check_PER()
        {
            bool HasPer;
            //插入前判断是否选择审批人
            if (ddl_onePer.SelectedIndex != 0 && ddl_two.SelectedIndex != 0 && ddl_three.SelectedIndex != 0)
            {
                HasPer = true;
            }
            else
            {
                HasPer = false;
            }
            return HasPer;
        }

        private void EXESQL()
        {
            List<string> list_sql = new List<string>();
            //获取输入信息
            #region
            string cs_code = txtCS_CODE.Text.Trim();
            string cs_name = txtCS_NAME.Text.Trim();
            string cs_location = txtCS_LOCATION.Text.Trim();
            string cs_hrcode = txtCS_HRCODE.Text.Trim();
            string cs_type = dopCS_TYPE.SelectedValue;

            string cs_address = txtCS_ADDRESS.Text.Trim();
            string cs_phono = txtCS_PHONO.Text.Trim();
            string cs_conname = txtCS_CONNAME.Text.Trim();
            string cs_mail = txtCS_MAIL.Text.Trim();
            string cs_zip = txtCS_ZIP.Text.Trim();
            string cs_fax = txtCS_FAX.Text.Trim();
            string cs_corebs = txtCS_COREBS.Text.Trim();
            //string cs_rank = txtCS_RANK.Text.Trim();
            string cs_manclerk = txtCS_MANCLERK.Text.Trim();
            string cs_filldate = txtCS_FILLDATE.Value.Trim();
            string cs_note = txtCS_NOTE.Text.Trim();
            //string cs_note = txtCS_NOTE.Text.Trim();
            string cs_shanchu_beizhu = shanchu_beizhu.Text.Trim();
            string cs_bank = txtCS_BANK.Text.Trim();
            string cs_account = txtCS_ACCOUNT.Text.Trim();
            string cs_tax = txtCS_TAX.Text.Trim();
            #endregion

            //添加删除
            if (action == "Add" || action == "Delete" || action == "Edit")
            {
                if (Check_MustPutIn())
                {
                    string spr1 = ddl_onePer.SelectedValue.ToString();
                    string spr2 = ddl_two.SelectedValue.ToString();
                    string spr3 = ddl_three.SelectedValue.ToString();
                    string sqltextid = "select max(ID) from TBCS_CUSUP_ADD_DELETE";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltextid);
                    int idid = 1;
                    if (dt.Rows[0][0].ToString() != "")
                    {
                        idid = int.Parse(dt.Rows[0][0].ToString()) + 1;
                    }
                    if (action == "Add")
                    {
                        //string sqltextid = "select max(ID) from TBCS_CUSUP_ADD_DELETE";
                        //DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltextid);
                        //int idid = 1;
                        //if (dt.Rows[0][0].ToString() != "")
                        //{
                        //    idid = int.Parse(dt.Rows[0][0].ToString()) + 1;
                        //}
                        //string sqltext = "select CS_NAME,CS_LOCATION,CS_TYPE from TBCS_CUSUPINFO where CS_NAME='" + cs_name + "'and CS_LOCATION='" + cs_location + "'and CS_TYPE='" + cs_type + "'and CS_State='0' ";
                        string sqltext = "select CS_NAME,CS_LOCATION,CS_TYPE from TBCS_CUSUPINFO where CS_NAME='" + cs_name + "' and CS_TYPE='" + cs_type + "' and CS_State='0' ";
                        DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dt1.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该厂商已成功添加，并正在使用中，请勿重复添加！！');", true); return;
                        }

                        string sqltext_state = "select CS_NAME,CS_LOCATION,CS_TYPE from TBCS_CUSUPINFO where CS_NAME='" + cs_name + "' and CS_TYPE='" + cs_type + "' and CS_State='1' ";
                        DataTable dt1_state = DBCallCommon.GetDTUsingSqlText(sqltext_state);
                        if (dt1_state.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该厂商已停用，若想再次使用，请直接启用！！');", true); return;
                        }
                        string sqltext_add_ag = "select CS_NAME from TBCS_CUSUP_ADD_DELETE where CS_NAME='" + cs_name + "' and CS_TYPE='" + cs_type + "' and cs_spjg in('0','1') ";
                        DataTable dt1_add_ag = DBCallCommon.GetDTUsingSqlText(sqltext_add_ag);
                        if (dt1_add_ag.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该厂商已添加，并处于审核状态，请勿重复添加！！');", true); return;
                        }
                        else
                        {

                            string sqlText1 = "insert into TBCS_CUSUP_ADD_DELETE(ID,CS_CODE,CS_NAME,CS_LOCATION,CS_HRCODE,CS_TYPE,CS_ADDRESS,CS_PHONO,CS_CONNAME,CS_MAIL,CS_ZIP,CS_FAX,CS_COREBS,CS_MANCLERK,CS_FILLDATE,CS_NOTE,CS_ACTION,CS_SPJG,CS_BANK,CS_ACCOUNT,CS_TAX,CS_Scope)" +
                                            " VALUES(\'" + idid + "\',\'" + cs_code + "\',\'" + cs_name + "\',\'" + cs_location + "\',\'" + cs_hrcode + "\',\'" + cs_type + "\',\'" + cs_address + "\',\'" + cs_phono + "\',\'" + cs_conname + "\',\'" + cs_mail + "\',\'" + cs_zip + "\',\'" + cs_fax + "\',\'" + cs_corebs + "\',\'" + cs_manclerk + "\',\'" + cs_filldate + "\',\'" + cs_note + "\','0','0',\'" + cs_bank + "\',\'" + cs_account + "\',\'" + cs_tax + "\','" + TB_Scope.Text + "')";//action:0为添加，1为删除,spjg:0为未审批
                            list_sql.Add(sqlText1);
                        }
                            //DBCallCommon.ExecuteTrans(list_sql);
                            //ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('添加成功！！');window.close();", true); return;

                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sptitle = "厂商添加/修改审批";
                        spcontent = "有厂商添加/修改需要您审批，请登录查看！";
                        sprid = ddl_onePer.SelectedValue.Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                    else if (action == "Delete")   //删除
                    {
                       string sfas = Request.QueryString["cs_action"];
                        string sdfafaf = Request.QueryString["id"];
                        //2016.8.4修改，此处为将已经停用的厂商再次启用，参数判断，启用
                        if (Request.QueryString["cs_action"] == "0" && (!string.IsNullOrEmpty(Request.QueryString["id"])))
                        {
                            string sqltext_qia = "select * from TBCS_CUSUP_ADD_DELETE where cs_code='" + id + "' and CS_TYPE='" + cs_type + "'  and cs_action='0' and cs_spjg in('0','1')";
                            DataTable DTDELE_qia = DBCallCommon.GetDTUsingSqlText(sqltext_qia);
                            if (DTDELE_qia.Rows.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该厂商已进行启用处理，正在审批中，请勿重复提交！！');", true); return;
                            }

                            string sqlText1 = "insert into TBCS_CUSUP_ADD_DELETE(ID,CS_CODE,CS_NAME,CS_LOCATION,CS_HRCODE,CS_TYPE,CS_ADDRESS,CS_PHONO,CS_CONNAME,CS_MAIL,CS_ZIP,CS_FAX,CS_COREBS,CS_MANCLERK,CS_FILLDATE,CS_NOTE,CS_NOTEDELETE,CS_ACTION,CS_SPJG,CS_BANK,CS_ACCOUNT,CS_TAX,CS_Scope)" +
                                           " VALUES(\'" + idid + "\',\'" + cs_code + "\',\'" + cs_name + "\',\'" + cs_location + "\',\'" + cs_hrcode + "\',\'" + cs_type + "\',\'" + cs_address + "\',\'" + cs_phono + "\',\'" + cs_conname + "\',\'" + cs_mail + "\',\'" + cs_zip + "\',\'" + cs_fax + "\',\'" + cs_corebs + "\',\'" + cs_manclerk + "\',\'" + cs_filldate + "\',\'" + cs_note + "\',\'" + cs_shanchu_beizhu + "\','1','0',\'" + cs_bank + "\',\'" + cs_account + "\',\'" + cs_tax + "\','" + TB_Scope.Text + "')";//action:0为添加，1为删除,spjg:0为未审批
                            list_sql.Add(sqlText1);
                        }

                        else
                        {
                            string sqltextdele = "select * from TBCS_CUSUP_ADD_DELETE where cs_code='" + id + "'and CS_TYPE='" + cs_type + "'  and cs_action='1' and cs_spjg in('0','1') ";
                            DataTable DTDELE = DBCallCommon.GetDTUsingSqlText(sqltextdele);
                            if (DTDELE.Rows.Count >= 1)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该厂商已进行停用处理，正在审批中，请勿重复提交！！');", true); return;
                            }
                            else
                            {

                                string sqlText1 = "insert into TBCS_CUSUP_ADD_DELETE(ID,CS_CODE,CS_NAME,CS_LOCATION,CS_HRCODE,CS_TYPE,CS_ADDRESS,CS_PHONO,CS_CONNAME,CS_MAIL,CS_ZIP,CS_FAX,CS_COREBS,CS_MANCLERK,CS_FILLDATE,CS_NOTE,CS_NOTEDELETE,CS_ACTION,CS_SPJG,CS_BANK,CS_ACCOUNT,CS_TAX,CS_Scope)" +
                                               " VALUES(\'" + idid + "\',\'" + cs_code + "\',\'" + cs_name + "\',\'" + cs_location + "\',\'" + cs_hrcode + "\',\'" + cs_type + "\',\'" + cs_address + "\',\'" + cs_phono + "\',\'" + cs_conname + "\',\'" + cs_mail + "\',\'" + cs_zip + "\',\'" + cs_fax + "\',\'" + cs_corebs + "\',\'" + cs_manclerk + "\',\'" + cs_filldate + "\',\'" + cs_note + "\',\'" + cs_shanchu_beizhu + "\','1','0',\'" + cs_bank + "\',\'" + cs_account + "\',\'" + cs_tax + "\','" + TB_Scope.Text + "')";//action:0为添加，1为删除,spjg:0为未审批
                                list_sql.Add(sqlText1);
                            }
                        }
                    }

                    else if (action == "Edit")    //编辑
                    {
                        string sqltextcheck = "select CS_NAME,CS_LOCATION,CS_TYPE from TBCS_CUSUPINFO where CS_NAME='" + cs_name + "'and CS_TYPE='" + cs_type + "'and CS_State='0' ";
                        DataTable dtcheck = DBCallCommon.GetDTUsingSqlText(sqltextcheck);
                        if (dtcheck.Rows.Count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('厂商已添加，请勿重复添加！！');", true); return;
                        }
                        string sqlselect_id = "select id from TBCS_CUSUP_ADD_DELETE where CS_CODE='" + id + "' and CS_ACTION='" + cs_action + "'";
                        DataTable dt_edit = DBCallCommon.GetDTUsingSqlText(sqlselect_id);
                        if (dt_edit.Rows.Count > 0)
                        {
                            string sqlEdit_Info = "update TBCS_CUSUP_ADD_DELETE set CS_NAME=\'" + cs_name + "\',CS_HRCODE=\'" + cs_hrcode + "\',CS_TYPE=\'" + cs_type + "\',CS_ADDRESS=\'" + cs_address + "\'," +
                                "CS_PHONO=\'" + cs_phono + "\',CS_CONNAME=\'" + cs_conname + "\',CS_MAIL=\'" + cs_mail + "\',CS_ZIP=\'" + cs_zip + "\',CS_FAX=\'" + cs_fax + "\',CS_COREBS=\'" + cs_corebs + "\',CS_NOTE=\'" + cs_note + "\',CS_SPJG='0',CS_BANK=\'" + cs_bank + "\',CS_ACCOUNT=\'" + cs_account + "\',CS_TAX=\'" + cs_tax + "\'" +
                            ",CS_Scope='" + TB_Scope.Text + "' where ID=" + dt_edit.Rows[0]["id"].ToString() + "";
                            string sqlEdit_Rev1 = "update TBCS_CUSUP_ReView SET CSR_PERSON='" + spr1 + "',CSR_TIME='',CSR_YJ='0',CSR_NOTE='' WHERE FATHERID=" + dt_edit.Rows[0]["id"].ToString() + " and CSR_TYPE='1'";
                            string sqlEdit_Rev2 = "update TBCS_CUSUP_ReView SET CSR_PERSON='" + spr2 + "',CSR_TIME='',CSR_YJ='0',CSR_NOTE='' WHERE FATHERID=" + dt_edit.Rows[0]["id"].ToString() + " and CSR_TYPE='2'";
                            string sqlEdit_Rev3 = "update TBCS_CUSUP_ReView SET CSR_PERSON='" + spr3 + "',CSR_TIME='',CSR_YJ='0',CSR_NOTE='' WHERE FATHERID=" + dt_edit.Rows[0]["id"].ToString() + " and CSR_TYPE='3'";
                            list_sql.Add(sqlEdit_Info);
                            list_sql.Add(sqlEdit_Rev1);
                            list_sql.Add(sqlEdit_Rev2);
                            list_sql.Add(sqlEdit_Rev3);
                            DBCallCommon.ExecuteTrans(list_sql);
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('修改成功，历史评审记录清空\\r\\r重新进入评审');", true); return;
                        }

                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sptitle = "厂商添加/修改审批";
                        spcontent = "有厂商添加/修改需要您审批，请登录查看！";
                        sprid = ddl_onePer.SelectedValue.Trim();
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                    }


                    DBCallCommon.ExecuteTrans(list_sql);
                    list_sql.Clear();



                    //string sqlcs_codeid = "select id from TBCS_CUSUP_ADD_DELETE where id='" + id+ "' and CS_ACTION='" + cs_action + "'";
                    //int cs_codeid = 0;
                    //SqlDataReader dr_cscodeid = DBCallCommon.GetDRUsingSqlText(sqlcs_codeid);
                    //if (dr_cscodeid.Read())
                    //{
                    //    cs_codeid = Convert.ToInt32(dr_cscodeid["id"].ToString()); dr_cscodeid.Close();
                    //}
                    //向审批表中插入数据

                    string sqlText2 = "insert into TBCS_CUSUP_ReView(fatherid,CS_CODE,CSR_PERSON,CSR_TYPE,CSR_YJ)" +
                                      " Values('" + idid + "','" + cs_code + "','" + spr1 + "','1','0')";
                    list_sql.Add(sqlText2);

                    string sqlText3 = "insert into TBCS_CUSUP_ReView(fatherid,CS_CODE,CSR_PERSON,CSR_TYPE,CSR_YJ)" +
                                      " Values('" + idid + "','" + cs_code + "','" + spr2 + "','2','0')";
                    list_sql.Add(sqlText3);

                    string sqlText4 = "insert into TBCS_CUSUP_ReView(fatherid,CS_CODE,CSR_PERSON,CSR_TYPE,CSR_YJ)" +
                                      " Values('" + idid + "','" + cs_code + "','" + spr3 + "','3','0')";
                    list_sql.Add(sqlText4);
                    DBCallCommon.ExecuteTrans(list_sql);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('已提交申请！\\r\\r该记录将在审批全部通过后生效！');window.close();", true);
                }
            }


            else if (action == "Review")
            {
                #region  审批
                string csr_person = Session["UserID"].ToString();
                string csr_time = DateTime.Now.ToShortDateString();
                string csr_yj = "";
                string csr_type = "";
                string csr_note = "";

                string current_guy = "select CSR_TYPE from TBCS_CUSUP_ReView where fatherid='" + id + "' AND CSR_PERSON='" + csr_person + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(current_guy);
                if (dr.Read())
                {
                    csr_type = dr["CSR_TYPE"].ToString();
                    dr.Close();

                    #region 审批结果
                    if (csr_type == "1") //部门意见，一级审核
                    {
                        if (RadioTY1.Checked == true)
                        {
                            csr_yj = "1";

                            //邮件提醒
                            string sprid = "";
                            string sptitle = "";
                            string spcontent = "";
                            sptitle = "厂商添加/修改审批";
                            spcontent = "有厂商添加/修改需要您审批，请登录查看！";
                            string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + TextSHR2.Text.Trim() + "'";
                            System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                            if (dtgetstid.Rows.Count > 0)
                            {
                                sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                            }
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                        }
                        if (RadioJJ1.Checked == true)
                        {
                            csr_yj = "2";
                            if (TextBZ1.Text.Trim() == "")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('请填写拒绝理由！！！');", true);
                                return;
                            }

                            //邮件提醒
                            string sprid = "";
                            string sptitle = "";
                            string spcontent = "";
                            sptitle = "厂商添加/修改驳回";
                            spcontent = "厂商添加/修改被驳回，请登录查看！";
                            string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txtCS_MANCLERK.Text.Trim() + "'";
                            System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                            if (dtgetstid.Rows.Count > 0)
                            {
                                sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                            }
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                        }
                        csr_note = TextBZ1.Text.ToString();
                    }
                    else if (csr_type == "2")//二级审核
                    {

                        if (RadioTY2.Checked == true)
                        {
                            csr_yj = "1";
                        }
                        if (RadioJJ2.Checked == true)
                        {
                            csr_yj = "2";
                            if (TextBZ2.Text.Trim() == "")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('请填写拒绝理由！！！');", true);
                                return;
                            }

                            //邮件提醒
                            string sprid = "";
                            string sptitle = "";
                            string spcontent = "";
                            sptitle = "厂商添加/修改驳回";
                            spcontent = "厂商添加/修改被驳回，请登录查看！";
                            string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txtCS_MANCLERK.Text.Trim() + "'";
                            System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                            if (dtgetstid.Rows.Count > 0)
                            {
                                sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                            }
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                        }
                        csr_note = TextBZ2.Text.ToString();
                    }
                    else if (csr_type == "3")//三级审核
                    {
                        if (RadioTY3.Checked == true)
                        {
                            csr_yj = "1";
                        }
                        if (RadioJJ3.Checked == true)
                        {
                            csr_yj = "2";
                            if (TextBZ3.Text.Trim() == "")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('请填写拒绝理由！！！');", true);
                                return;
                            }

                            //邮件提醒
                            string sprid = "";
                            string sptitle = "";
                            string spcontent = "";
                            sptitle = "厂商添加/修改驳回";
                            spcontent = "厂商添加/修改被驳回，请登录查看！";
                            string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txtCS_MANCLERK.Text.Trim() + "'";
                            System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                            if (dtgetstid.Rows.Count > 0)
                            {
                                sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                            }
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                        }
                        csr_note = TextBZ3.Text.ToString();
                    }
                    #endregion

                }

                if (csr_yj != "")
                {
                    //插入评审意见审批表
                    string update = "update TBCS_CUSUP_ReView set CSR_TIME='" + csr_time + "', CSR_YJ='" + csr_yj + "',CSR_NOTE='" + csr_note + "'" +
                                    " where fatherid='" + id + "' and CSR_PERSON='" + csr_person + "'";
                    list_sql.Add(update);
                    //同意或拒绝
                    string update2 = "";
                    if (csr_yj == "1")  //同意
                    {
                        update2 = "update TBCS_CUSUP_ADD_DELETE set CS_SPJG='1' where id='" + id + "'"; //审批中
                    }
                    else if (csr_yj == "2") //拒绝
                    {
                        update2 = "update TBCS_CUSUP_ADD_DELETE set CS_SPJG='3' where id='" + id + "'"; //不同意，驳回
                    }
                    list_sql.Add(update2);
                    DBCallCommon.ExecuteTrans(list_sql);
                    list_sql.Clear();
                    //如果所有人都同意，则执行申请--添加删除
                    string sql_check = "select * from TBCS_CUSUP_ReView where fatherid='" + id + "'";
                    bool check = true;
                    DataTable dt_check = DBCallCommon.GetDTUsingSqlText(sql_check);
                    if (dt_check.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt_check.Rows.Count; i++)
                        {
                            if (dt_check.Rows[i]["CSR_YJ"].ToString() != "1")
                            {
                                check = false; break;  //有一个人不同意，则不执行申请
                            }
                        }
                    }
                    if (check)          //全部通过
                    {
                        string update_state = "update TBCS_CUSUP_ADD_DELETE set CS_SPJG='2' where id='" + id + "'";
                        list_sql.Add(update_state);
                        string exe_action = "";

                        if (cs_action == "0") //添加
                        {
                            //string sqltextid = "select max(CS_ID) from TBCS_CUSUPINFO";
                            //DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltextid);
                            //int idid = 1;
                            //if (dt.Rows[0][0].ToString() != "")
                            //{
                            //    idid = int.Parse(dt.Rows[0][0].ToString()) + 1;
                            //}
                            string sql = "select * from  TBCS_CUSUPINFO where CS_CODE='" + id + "'and CS_NAME='" + cs_name + "'and CS_TYPE='" + cs_type + "' ";
                            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                            if (dt1.Rows.Count == 0)
                            {

                                exe_action = "insert into TBCS_CUSUPINFO(CS_ID,CS_CODE,CS_NAME,CS_LOCATION,CS_HRCODE,CS_TYPE,CS_ADDRESS,CS_PHONO,CS_CONNAME,CS_MAIL,CS_ZIP,CS_FAX,CS_COREBS,CS_MANCLERK,CS_FILLDATE,CS_NOTE,CS_Bank,CS_ACCOUNT,CS_TAX,CS_Scope)" +
                                                 " VALUES(\'" + cs_code + "\',\'" + cs_code + "\',\'" + cs_name + "\',\'" + cs_location + "\',\'" + cs_hrcode + "\',\'" + cs_type + "\',\'" + cs_address + "\',\'" + cs_phono + "\',\'" + cs_conname + "\',\'" + cs_mail + "\',\'" + cs_zip + "\',\'" + cs_fax + "\',\'" + cs_corebs + "\',\'" + cs_manclerk + "\',\'" + cs_filldate + "\',\'" + cs_note + "\',\'" + cs_bank + "\',\'" + cs_account + "\',\'" + cs_tax + "\','" + TB_Scope.Text + "')";
                                list_sql.Add(exe_action);
                            }
                            else
                            {
                                string chage_action = "update TBCS_CUSUPINFO set cs_state='0',CS_MANCLERK='" + cs_manclerk + "' where CS_CODE='" + cs_code + "'";
                                list_sql.Add(chage_action);
                            }
                        }
                        else if (cs_action == "1")  //删除
                        {
                            exe_action = "update TBCS_CUSUPINFO set cs_state='1' where CS_CODE='" + cs_code + "'";
                            list_sql.Add(exe_action);
                        }

                    }
                    DBCallCommon.ExecuteTrans(list_sql);
                    // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('审批成功！！！');window.close();", true);
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('审批成功！');window.opener=null;window.open('','_self');window.close();", true);
                    // Response.Write("<script>window.opener.location.reload();</script>");//刷新
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert ('请选择同意或拒绝！！！');", true);
                    return;
                }
                #endregion

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener.location = window.opener.location.href;window.close();", true);
            }
        }

        private bool Check_MustPutIn()
        {
            bool Check = true;
            if (txtCS_CODE.Text.Trim() == "")
            {
                Check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('公司编号不能为空！');", true); return Check;
            }

            if (txtCS_NAME.Text.Trim() == "")
            {
                Check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('公司名称不能为空！');", true); return Check;
            }
            if (action == "Add" || action == "Edit")
            {
                if (txtCS_LOCATION.Text.Trim() == "")
                {
                    Check = false;
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('公司所在地不能为空！');", true); return Check;
                }
            }

            if (txtCS_HRCODE.Text.Trim() == "")
            {
                Check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('助记码不能为空！');", true); return Check;
            }

            if (txtCS_TYPE.Text.Trim() == "")
            {
                Check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('公司类型不能为空！');", true); return Check;
            }

            if (txtCS_COREBS.Text.Trim() == "")
            {
                Check = false;
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('主要业务不能为空！');", true); return Check;
            }
            return Check;
        }

        /// <summary>
        /// 生成ID号,公司编号ID为8位，前4位为地区号，后4位为流水号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreatID_Click(object sender, EventArgs e)
        {
            if (txtCS_LOCATION.Text == "")
            {
                Response.Write("<script>alert('请选择省、（市/区）！')</script>");
            }
            else
            {
                //需要创建的编号
                string createdCS_CODE = "";
                //由于地区编号的限制，直接获取TOP CS_CODE不行
                //采用的方法为读出与地区编号匹配的记录
                string locationCode = dopCL_LOCATION_NEXT.SelectedValue.ToString();
                string sqltext = "SELECT substring(CS_CODE,5,4) AS ID FROM " +
                              " ( select distinct CS_CODE from (select CS_CODE from TBCS_CUSUPINFO Union select CS_CODE from TBCS_CUSUP_ADD_DELETE) a)b" +
                              " Where CS_CODE LIKE \'" + locationCode + "%\' ORDER BY ID ASC";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                createdCS_CODE = locationCode.ToString() + this.CreatedID(dt).ToString();
                txtCS_CODE.Text = createdCS_CODE.ToString();
            }

        }
        /// <summary>
        /// 找出中间空缺的最小编号
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string CreatedID(DataTable dt)
        {
            string creatID = "";
            int recordAmount = dt.Rows.Count;

            string[] id_string = new string[recordAmount];
            int[] id_integer = new int[recordAmount];
            //获取编号字符串，并将其转换为可运算整型
            for (int i = 0; i < recordAmount; i++)
            {
                id_string[i] = dt.Rows[i]["ID"].ToString();
                id_integer[i] = Convert.ToInt32(id_string[i].ToString());
            }
            //分不同的情况进行判断
            //主要分为三大类：记录为0；为1；大于1（每一种还要细分）
            if (recordAmount == 0 || (recordAmount == 1 && id_integer[0] > 1))
            {
                creatID = "0001";
            }
            else if (recordAmount == 1 && id_integer[0] == 1)
            {
                creatID = string.Format("{0:0000}", id_integer[0] + 1);

            }
            else
            {
                if (recordAmount == id_integer[recordAmount - 1])
                {
                    creatID = string.Format("{0:0000}", id_integer[recordAmount - 1] + 1);
                }
                else if (id_integer[0] - 1 > 0)
                {
                    creatID = "0001";
                }
                else
                {
                    for (int m = 0; m < recordAmount - 1; m++)
                    {
                        int n = m + 1;
                        if (id_integer[n] - id_integer[m] - 1 > 0)
                        {
                            creatID = string.Format("{0:0000}", id_integer[m] + 1);
                            break;
                        }
                    }
                }
            }
            return creatID;
        }

        //一级评审EnabledF
        private void BMSHEnabledF()
        {
            RadioJJ1.Enabled = false;
            RadioTY1.Enabled = false;
            TextBZ1.Enabled = false;
        }
        //二级评审EnabledF
        private void EJSHEnalbedF()
        {
            RadioTY2.Enabled = false;
            RadioJJ2.Enabled = false;
            TextBZ2.Enabled = false;
        }
        //三级评审EnabledF
        private void SJSHEnabledF()
        {
            RadioJJ3.Enabled = false;
            RadioTY3.Enabled = false;
            TextBZ3.Enabled = false;
        }
        //选择同意或拒绝自动带出“同意”或“拒绝理由”

        #region

        protected void BMYJ_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioJJ1.Checked)
            {
                TextBZ1.Text = "拒绝理由：";
            }
            if (RadioTY1.Checked)
            {
                TextBZ1.Text = "同意";
            }
        }
        protected void EJYJ_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioJJ2.Checked)
            {
                TextBZ2.Text = "拒绝理由：";
            }
            if (RadioTY2.Checked)
            {
                TextBZ2.Text = "同意";
            }
        }
        protected void SJYJ_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioJJ3.Checked)
            {
                TextBZ3.Text = "拒绝理由：";
            }
            if (RadioTY3.Checked)
            {
                TextBZ3.Text = "同意";
            }
        }

        #endregion

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "", "window.close()", true);

        }

        protected void bntupload_Click(object sender, EventArgs e)
        {
            //if (txtCS_CODE.Text == "")
            //{
            //    Response.Write("<script>alert('先生成供货商编号！')</script>");
            //    return;
            //}

            //执行上传文件
            uploafFile();

        }

        private void uploafFile()
        {
            int IntIsUF = 0;

            //获取文件保存的路径 
            filePath = @"E:/基础数据";//附件上传位置            

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            try
            {
                HttpPostedFile hpf = FileUpload1.PostedFile;

                string fileContentType = hpf.ContentType.ToLower();// 获取客户端发送的文件的 MIME 内容类型  

                if (fileContentType == "application/msword" || fileContentType == "application/vnd.ms-excel" || fileContentType == "application/pdf" || fileContentType == "image/pjpeg" || fileContentType == "image/jpg" || fileContentType == "image/jpeg" || fileContentType == "image/gif" || fileContentType == "image/png" || fileContentType == "image/bmp" || fileContentType == "application/octet-stream" || fileContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")//传送文件类型
                {
                    if (hpf.ContentLength > 0)
                    {
                        //if (hpf.ContentLength < (2048 * 1024))
                        //{
                            string strFileName = System.IO.Path.GetFileName(hpf.FileName);
                            //string filename = System.DateTime.Now.ToString("yyyyMMddHHmmss") + strFileName;
                            if (!File.Exists(filePath + @"\" + strFileName))
                            {
                                string sqlStr = string.Empty;
                                //定义插入字符串，将上传文件信息保存在数据库中

                                sqlStr = "insert into TBMA_FILES (SUPPLYID,SAVEURL,UPLOADDATE,FILENAME)";
                                sqlStr += "values('" + txtCS_CODE.Text + "' ";
                                sqlStr += ",'" + filePath + "'";
                                sqlStr += ",'" + DateTime.Now.ToString() + "'";
                                sqlStr += ",'" + strFileName + "')";

                                DBCallCommon.ExeSqlText(sqlStr);
                                hpf.SaveAs(filePath + @"\" + strFileName);
                                //isHasFile = true;
                                IntIsUF = 1;
                            }
                            else
                            {
                                filesError.Visible = true;
                                filesError.Text = "上传文件名与服务器文件名重名，请您核对后重新命名上传！";
                                IntIsUF = 1;
                            }
                        //}
                        //else
                        //{
                        //    filesError.Text = "上传文件过大，上传文件必须小于1M!";
                        //    IntIsUF = 1;
                        //}
                    }
                }
                else
                {
                    filesError.Visible = true;
                    filesError.Text = "文件类型不符合要求，请您核对后重新上传！";
                    IntIsUF = 1;
                }
            }
            catch (Exception e)
            {
                filesError.Text = "文件上传过程中出现错误！" + e.ToString();
                filesError.Visible = true;
                return;
            }
            if (IntIsUF == 1)
            {
                IntIsUF = 0;
            }
            else
            {
                filesError.Visible = true;
                filesError.Text = "请选择上传文件!";
            }
            GVBind();
        }

        protected void imgbtndelete_Click(object sender, ImageClickEventArgs e)
        {
            //获取imgbtnDelete的ImageButton对象
            ImageButton imgbtn = (ImageButton)sender;
            //引用imgbtnDelete控件的父控件上一级控件
            GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
            GridView gv = (GridView)imgbtn.Parent.Parent.Parent.Parent;
            string idd = ((Label)gvr.FindControl("lbid")).Text;
            //获取文件真实姓名
            string sqlStr = "select SAVEURL,FILENAME from TBMA_FILES where ID='" + idd + "'";
            //在文件夹Files下，删除该文件
            DeleteFile(sqlStr);
            string sqlDelStr = "delete from TBMA_FILES where ID='" + idd + "'";//删除数据库中的记录
            DBCallCommon.ExeSqlText(sqlDelStr);
            GVBind();

        }

        protected void DeleteFile(string sqlStr)
        {
            //打开数据库
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取指定文件的路径
            string strFilePath = ds.Tables[0].Rows[0]["SAVEURL"].ToString() + @"\" + ds.Tables[0].Rows[0]["FILENAME"].ToString();
            //调用File类的Delete方法，删除指定文件
            File.Delete(strFilePath);//文件不存在也不会引发异常
        }


        private void GVBind()
        {
            if (txtCS_CODE.Text == "")
            {
                txtCS_CODE.Text = id;
            }

            string sql = "select * from TBMA_FILES where SUPPLYID='" + txtCS_CODE.Text + "' ";
            DataSet ds = DBCallCommon.FillDataSet(sql);
            gvfileslist.DataSource = ds.Tables[0];
            gvfileslist.DataBind();
            gvfileslist.DataKeyNames = new string[] { "ID" };
            
        }


        protected void imgbtndownload_Click(object sender, ImageClickEventArgs e)
        {
            //获取imgbtnDelete的ImageButton对象
            ImageButton imgbtn = (ImageButton)sender;
            //引用imgbtnDelete控件的父控件上一级控件
            GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
            GridView gv = (GridView)imgbtn.Parent.Parent.Parent.Parent;
            string idd = ((Label)gvr.FindControl("lbid")).Text;

            //获取文件真实姓名
            string sqlStr = "select SAVEURL,FILENAME from TBMA_FILES where ID='" + idd + "'";
            //打开数据库
            //Response.Write(sqlStr);         
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取文件路径
            string strFilePath = ds.Tables[0].Rows[0]["SAVEURL"].ToString() + @"\" + ds.Tables[0].Rows[0]["FILENAME"].ToString();
            Response.Write(strFilePath);
            //if (File.Exists(strFilePath))
            //{
            //    System.IO.FileInfo file = new System.IO.FileInfo(strFilePath);
            //    Response.Clear();
            //    Response.ClearHeaders();
            //    Response.Buffer = true;
            //    Response.ContentType = "application/octet-stream";
            //    Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(file.Name));
            //    Response.AppendHeader("Content-Length", file.Length.ToString());
            //    Response.WriteFile(file.FullName);
            //    //Response.Flush();
            //    //Response.End();
            //}
            //else
            //{
            //    filesError.Visible = true;
            //    filesError.Text = "文件已被删除，请通知相关人员上传文件！";
            //}




            //判断文件是否存在，如果不存在提示重新上传
            if (System.IO.File.Exists(strFilePath))
            {
                System.IO.FileInfo file = new System.IO.FileInfo(strFilePath);
                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(file.Name));
                Response.AppendHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(file.FullName);
                Response.Flush();
                Response.End();
            }
            else
            {
                filesError.Visible = true;
                filesError.Text = "文件已被删除，请通知相关人员上传文件！";
            }
        }



    }
}
