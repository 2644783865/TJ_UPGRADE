using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_Pur_inform_commit_detial : System.Web.UI.Page
    {
        public string action = "";
        Table t = new Table();
        Dictionary<string, string> reviewer = new Dictionary<string, string>();//用于存储审核的名单
        int rowsum = 0;
        string filePath = "";
        public string PC_PFT_ID_code = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];

            if (action == "Delete" || action == "Review" || action == "View" || action == "Edit") //停用，审批，查看审批情况
            {
                PC_PFT_ID_code = Request.QueryString["id"];//

            }
            else
            {
                GetCSCODE();
            }

            GVBind();

            getLeaderInfo();

            if (!IsPostBack)
            {
                BindSelectReviewer();
                InitP();

            }



            ContralEnable();
        }
        private void InitP()
        {
            string initp_sql = "select * from PC_purinformcommitall where PC_PFT_ID='" + PC_PFT_ID_code + "'";
            DataTable initp_dt = DBCallCommon.GetDTUsingSqlText(initp_sql);
            if (initp_dt.Rows.Count > 0)
            {

                Text_contextmain.Text = initp_dt.Rows[0]["PC_PFT_TEXT"].ToString();
                //string PC_PFT_ZDR_ID = "";
                //string PC_PFT_ZDR_NAME = "";
                //string PC_PFT_TIME = "";
                //string PC_PFT_STATE = "";

                text_zdrid.Text = initp_dt.Rows[0]["PC_PFT_ZDR_ID"].ToString();
                text_spjb.Text = initp_dt.Rows[0]["PC_PFT_SPJB"].ToString();
                text_state.Text = initp_dt.Rows[0]["PC_PFT_STATE"].ToString();

                if (!string.IsNullOrEmpty(text_spjb.Text))
                {
                    rblSPJB.SelectedValue = text_spjb.Text;
                }

                txt_first.Text = initp_dt.Rows[0]["PC_PFT_SPRA_NAME"].ToString();
                firstid.Value = initp_dt.Rows[0]["PC_PFT_SPRA_ID"].ToString();
                first_time.Text = initp_dt.Rows[0]["PC_PFT_SPRA_TIME"].ToString();
                if (!string.IsNullOrEmpty(initp_dt.Rows[0]["PC_PFT_SPRA_RESULT"].ToString()))
                {
                    if (initp_dt.Rows[0]["PC_PFT_SPRA_RESULT"].ToString() == "0")
                    {
                        rblResult1.SelectedValue = "0";
                    }
                    if (initp_dt.Rows[0]["PC_PFT_SPRA_RESULT"].ToString() == "1")
                    {
                        rblResult1.SelectedValue = "1";
                    }
                    first_opinion.Text = initp_dt.Rows[0]["PC_PFT_SPRA_SUG"].ToString();
                }

                txt_second.Text = initp_dt.Rows[0]["PC_PFT_SPRB_NAME"].ToString();
                secondid.Value = initp_dt.Rows[0]["PC_PFT_SPRB_ID"].ToString();
                second_time.Text = initp_dt.Rows[0]["PC_PFT_SPRB_TIME"].ToString();
                if (!string.IsNullOrEmpty(initp_dt.Rows[0]["PC_PFT_SPRB_RESULT"].ToString()))
                {
                    if (initp_dt.Rows[0]["PC_PFT_SPRB_RESULT"].ToString() == "0")
                    {
                        rblResult2.SelectedValue = "0";
                    }
                    if (initp_dt.Rows[0]["PC_PFT_SPRB_RESULT"].ToString() == "1")
                    {
                        rblResult2.SelectedValue = "1";
                    }
                    second_opinion.Text = initp_dt.Rows[0]["PC_PFT_SPRB_SUG"].ToString();
                }

                txt_third.Text = initp_dt.Rows[0]["PC_PFT_SPRC_NAME"].ToString();
                thirdid.Value = initp_dt.Rows[0]["PC_PFT_SPRC_ID"].ToString();
                third_time.Text = initp_dt.Rows[0]["PC_PFT_SPRC_TIME"].ToString();
                if (!string.IsNullOrEmpty(initp_dt.Rows[0]["PC_PFT_SPRC_RESULT"].ToString()))
                {
                    if (initp_dt.Rows[0]["PC_PFT_SPRC_RESULT"].ToString() == "0")
                    {
                        rblResult3.SelectedValue = "0";
                    }
                    if (initp_dt.Rows[0]["PC_PFT_SPRC_RESULT"].ToString() == "1")
                    {
                        rblResult3.SelectedValue = "1";
                    }
                    third_opinion.Text = initp_dt.Rows[0]["PC_PFT_SPRC_SUG"].ToString();
                }

                zdr_pur_commit.Text = initp_dt.Rows[0]["PC_PFT_ZDR_NAME"].ToString();
                zdsj_pur_commit.Text = initp_dt.Rows[0]["PC_PFT_TIME"].ToString();

                hth_pur_commit.Text = initp_dt.Rows[0]["PC_PFT_HTH"].ToString();
                xmmc_pur_commit.Text = initp_dt.Rows[0]["PC_PFT_XMMC"].ToString();
                rwh_pur_commit.Text = initp_dt.Rows[0]["PC_PFT_RWH"].ToString();
            }
            if(action=="Add")
            {
                zdr_pur_commit.Text = Session["UserName"].ToString();
            }
        }
        //对绑定已经勾选的评审人
        private void BindSelectReviewer()
        {

            string check_select = "select pc_pid from PC_PurInformCommit_Email where PC_PFT_ID='" + PC_PFT_ID_code + "' ";
            DataTable sele = DBCallCommon.GetDTUsingSqlText(check_select);
            for (int i = 0; i < 4; i++)
            {
                CheckBoxList ck = (CheckBoxList)Panel2.FindControl("cki" + i.ToString());
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

        //0未提交 1审批中 2通过 3驳回
        private void ContralEnable()
        {
            if (action == "Add")
            {
                rblResult1.Enabled = false;
                first_opinion.Enabled = false;

                rblResult2.Enabled = false;
                second_opinion.Enabled = false;

                rblResult3.Enabled = false;
                third_opinion.Enabled = false;
            }
            if (action == "View")
            {
                hth_pur_commit.Enabled = false;
                xmmc_pur_commit.Enabled = false;
                rwh_pur_commit.Enabled = false;
                
                Text_contextmain.Enabled = false;
                Panel2.Enabled = false;
                panel_pro.Enabled = false;
                Panel1.Enabled = false;
                Panel3.Enabled = false;
                Panel4.Enabled = false;
                btnLoad.Visible = false;
                hlSelect1.Visible = false;
                hlSelect2.Visible = false;
                hlSelect3.Visible = false;

                bntupload.Enabled = false;
                for (int i = 0; i <= gvfileslist.Rows.Count - 1; i++)
                {
                    ImageButton imgbtn = (ImageButton)gvfileslist.Rows[i].FindControl("imgbtndelete");
                    imgbtn.Visible = false;
                }
            }
            if (action == "Review")
            {
                hth_pur_commit.Enabled = false;
                xmmc_pur_commit.Enabled = false;
                rwh_pur_commit.Enabled = false;

                string currentguy_name = Session["UserName"].ToString();
                string currentguy_dep = Session["UserDeptID"].ToString();
                Text_contextmain.Enabled = false;

                bntupload.Enabled = false;
                for (int i = 0; i <= gvfileslist.Rows.Count - 1; i++)
                {
                    ImageButton imgbtn = (ImageButton)gvfileslist.Rows[i].FindControl("imgbtndelete");
                    imgbtn.Visible = false;
                }

                if (currentguy_name == txt_first.Text.ToString())
                {
                    Text_contextmain.Enabled = false;
                    panel_pro.Enabled = false;
                    Panel3.Enabled = false;
                    Panel4.Enabled = false;
                    hlSelect1.Visible = false;
                    hlSelect2.Visible = false;
                    hlSelect3.Visible = false;
                }
                else if (currentguy_name == txt_second.Text.ToString())
                {
                    Text_contextmain.Enabled = false;
                    panel_pro.Enabled = false;
                    Panel1.Enabled = false;
                    Panel4.Enabled = false;
                    hlSelect1.Visible = false;
                    hlSelect2.Visible = false;
                    hlSelect3.Visible = false;
                }
                else if (currentguy_name == txt_third.Text.ToString())
                {
                    Text_contextmain.Enabled = false;
                    panel_pro.Enabled = false;
                    Panel1.Enabled = false;
                    Panel3.Enabled = false;
                    hlSelect1.Visible = false;
                    hlSelect2.Visible = false;
                    hlSelect3.Visible = false;
                }
                else
                {
                    Text_contextmain.Enabled = false;
                    Panel2.Enabled = false;
                    panel_pro.Enabled = false;
                    Panel1.Enabled = false;
                    Panel3.Enabled = false;
                    Panel4.Enabled = false;
                    btnLoad.Visible = false;
                    hlSelect1.Visible = false;
                    hlSelect2.Visible = false;
                    hlSelect3.Visible = false;

                }


            }
        }
        protected void GetCSCODE()
        {
            string sqltext = "select (PC_PFT_ID+0) as index_top from PC_purinformcommitall ORDER BY index_top  DESC ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int index;
            if (dt.Rows.Count > 0)
            {
                index = int.Parse(dt.Rows[0]["index_top"].ToString());
            }
            else
            {
                index = 0;
            }
            string code = (index + 1).ToString();
            txtCS_CODE.Text = code;

        }
        protected void btnYes_Click(object sender, EventArgs e)
        {

            string newState = "0";//审核之后的新状态
            if (CheckPs(true, new List<string>() { "cki0", "cki1", "cki2", "cki3"}))
            {
                Response.Write("<script>alert('请选择抄送人！');</script>"); return;//后台验证
            }

            List<string> list_sql = new List<string>();

            string PC_PFT_ID = txtCS_CODE.Text.Trim();

            string PC_PFT_TEXT = "";
            string PC_PFT_ZDR_ID = "";
            string PC_PFT_ZDR_NAME = "";
            string PC_PFT_TIME = "";
            string PC_PFT_STATE = "";
            string PC_PFT_SPJB = "";

            string PC_PFT_SPRA_ID = "";
            string PC_PFT_SPRA_NAME = "";
            string PC_PFT_SPRA_RESULT = "";
            string PC_PFT_SPRA_TIME = "";
            string PC_PFT_SPRA_SUG = "";

            string PC_PFT_SPRB_ID = "";
            string PC_PFT_SPRB_NAME = "";
            string PC_PFT_SPRB_RESULT = "";
            string PC_PFT_SPRB_TIME = "";
            string PC_PFT_SPRB_SUG = "";

            string PC_PFT_SPRC_ID = "";
            string PC_PFT_SPRC_NAME = "";
            string PC_PFT_SPRC_RESULT = "";
            string PC_PFT_SPRC_TIME = "";
            string PC_PFT_SPRC_SUG = "";

            string PC_PFT_HTH = "";
            string PC_PFT_XMMC = "";
            string PC_PFT_RWH = "";

            PC_PFT_HTH = hth_pur_commit.Text;
            PC_PFT_XMMC = xmmc_pur_commit.Text;
            PC_PFT_RWH = rwh_pur_commit.Text;

            PC_PFT_TEXT = Text_contextmain.Text;
            PC_PFT_ZDR_ID = Session["UserID"].ToString();
            PC_PFT_ZDR_NAME = Session["UserName"].ToString();
            PC_PFT_TIME = DateTime.Now.ToString();

            PC_PFT_SPJB = rblSPJB.SelectedValue;

            if (rblSPJB.SelectedValue == "1")
            {
                if (txt_first.Text == "")
                {
                    Response.Write("<script>alert('请选择审核人！');</script>"); return;//后台验证
                }
                else
                {
                    PC_PFT_SPRA_NAME = txt_first.Text;
                    PC_PFT_SPRA_ID = firstid.Value;
                }
            }
            else if (rblSPJB.SelectedValue == "2")
            {
                if (txt_first.Text == "" || txt_second.Text=="")
                {
                    Response.Write("<script>alert('请选择审核人！');</script>"); return;//后台验证
                }
                else
                {
                    PC_PFT_SPRA_NAME = txt_first.Text;
                    PC_PFT_SPRA_ID = firstid.Value;

                    PC_PFT_SPRB_NAME = txt_second.Text;
                    PC_PFT_SPRB_ID = secondid.Value;
                }
            }
            else if (rblSPJB.SelectedValue == "3")
            {
                if (txt_first.Text == "" || txt_second.Text == "" || txt_third.Text=="")
                {
                    Response.Write("<script>alert('请选择审核人！');</script>"); return;//后台验证
                }
                else
                {
                    PC_PFT_SPRA_NAME = txt_first.Text;
                    PC_PFT_SPRA_ID = firstid.Value;

                    PC_PFT_SPRB_NAME = txt_second.Text;
                    PC_PFT_SPRB_ID = secondid.Value;

                    PC_PFT_SPRC_NAME = txt_third.Text;
                    PC_PFT_SPRC_ID = thirdid.Value;
                }
            }


            #region 抄送人

            bindReviewer();

            if (action == "Review")
            {
                string sqp_email_change = "select *  from PC_PurInformCommit_Email where PC_PFT_ID='" + PC_PFT_ID_code + "' ";
                DataTable initp_email_change = DBCallCommon.GetDTUsingSqlText(sqp_email_change);
                if (initp_email_change.Rows.Count > 0)
                {
                    string sqp_email_delete = "delete  from PC_PurInformCommit_Email where PC_PFT_ID='" + PC_PFT_ID_code + "' ";
                    list_sql.Add(sqp_email_delete);//删除
                }
            }

            for (int i = 0; i < reviewer.Count; i++)
            {
                    ///******************通过键来找值******************************************************/
                    ///**为了兼容领导同时为部门负责人的情况，评审人部门编号要以评审人员设置表中为准，而不以当前登录人部门编号为依据**/
                    //string sql_dep = "select dep_id from TBCM_HT_SETTING where per_id='" + reviewer.Values.ElementAt(i) + "' and per_sfjy=0 and per_type='2'";
                    //DataTable dt_dep = DBCallCommon.GetDTUsingSqlText(sql_dep);
                    //if (dt_dep.Rows.Count > 0)
                    //{
                        string sql_email = "insert into PC_PurInformCommit_Email (PC_PFT_ID,pc_pid,pc_dep,pc_pidtype) values ('" + PC_PFT_ID + "','" + reviewer.Values.ElementAt(i) + "','','')";
                        list_sql.Add(sql_email);//其他人
                    //}
            }
            #endregion

            if (action == "Add")
            {
                string PC_CONTEXT_SQL = "insert into PC_purinformcommitall (PC_PFT_ID,PC_PFT_TEXT,PC_PFT_ZDR_ID,PC_PFT_ZDR_NAME,PC_PFT_TIME,PC_PFT_SPJB,PC_PFT_STATE,PC_PFT_SPRA_ID,PC_PFT_SPRA_NAME,PC_PFT_SPRB_ID,PC_PFT_SPRB_NAME,PC_PFT_SPRC_ID,PC_PFT_SPRC_NAME,PC_PFT_HTH,PC_PFT_XMMC,PC_PFT_RWH)" +
                     " VALUES(\'" + PC_PFT_ID + "\',\'" + PC_PFT_TEXT + "\',\'" + PC_PFT_ZDR_ID + "\',\'" + PC_PFT_ZDR_NAME + "\',\'" + PC_PFT_TIME + "\',\'" + PC_PFT_SPJB + "\','0',\'" + PC_PFT_SPRA_ID + "\',\'" + PC_PFT_SPRA_NAME + "\',\'" + PC_PFT_SPRB_ID + "\',\'" + PC_PFT_SPRB_NAME + "\',\'" + PC_PFT_SPRC_ID + "\',\'" + PC_PFT_SPRC_NAME + "\',\'" + PC_PFT_HTH + "\',\'" + PC_PFT_XMMC + "\',\'" + PC_PFT_RWH + "\')";
                list_sql.Add(PC_CONTEXT_SQL);
            }
            if (action == "Review")
            {
                #region  审批
                string csr_person = Session["UserID"].ToString();
                string csr_time = DateTime.Now.ToShortDateString();
                string csr_jg = "";
                string csr_note = "";
                string sql_state = "";
                string state = text_state.Text;
                string text_spjb_hd = text_spjb.Text;
                if (state == "0")
                {
                    if (rblResult1.SelectedIndex == -1)
                    {
                        Response.Write("<script>alert('请选择审核结果！')</script>");
                        return;
                    }
                }
                if (state == "1")
                {
                    if (rblResult2.SelectedIndex == -1)
                    {
                        Response.Write("<script>alert('请选择审核结果！')</script>");
                        return;
                    }
                }
                if (state == "2")
                {
                    if (rblResult3.SelectedIndex == -1)
                    {
                        Response.Write("<script>alert('请选择审核结果！')</script>");
                        return;
                    }
                }
                //0初始化，1审批中，2一级通过，3二级通过，4全部通过 ，5驳回

                if (text_spjb_hd == "1")
                {
                    if (rblResult1.SelectedValue == "0")
                    {
                        newState = "4";
                    }
                    else
                    {
                        newState = "5";
                    }
                }

                if (text_spjb_hd == "2")
                {
                    if (state == "0")
                    {
                        if (rblResult1.SelectedValue == "0")
                        {
                            newState = "1";
                        }
                        else
                        {
                            newState = "5";
                        }
                    }
                    if (state == "1")
                    {
                        if (rblResult2.SelectedValue == "0")
                        {
                            newState = "4";
                        }
                        else
                        {
                            newState = "5";
                        }
                    }
                }
                else if (text_spjb_hd == "3")
                {
                    if (state == "0")
                    {
                        if (rblResult1.SelectedValue == "0")
                        {
                            newState = "1";
                        }
                        else
                        {
                            newState = "5";
                        }
                    }
                    if (state == "1")
                    {
                        if (rblResult2.SelectedValue == "0")
                        {
                            newState = "2";
                        }
                        else
                        {
                            newState = "5";
                        }
                    }
                    if (state == "2")
                    {
                        if (rblResult3.SelectedValue == "0")
                        {
                            newState = "4";
                        }
                        else
                        {
                            newState = "5";
                        }
                    }
                }

                if (state == "0")
                {
                    sql_state = "update PC_purinformcommitall set PC_PFT_STATE='" + newState + "',PC_PFT_SPRA_RESULT='" + rblResult1.SelectedValue + "',PC_PFT_SPRA_TIME='" + DateTime.Now.ToString() + "',PC_PFT_SPRA_SUG='" + first_opinion.Text.Trim() + "'  where PC_PFT_ID='" + PC_PFT_ID_code + "'";
                }
                else if (state == "1")
                {
                    sql_state = "update PC_purinformcommitall set PC_PFT_STATE='" + newState + "',PC_PFT_SPRB_RESULT='" + rblResult2.SelectedValue + "',PC_PFT_SPRB_TIME='" + DateTime.Now.ToString() + "',PC_PFT_SPRB_SUG='" + second_opinion.Text.Trim() + "'  where PC_PFT_ID='" + PC_PFT_ID_code + "'";
                }
                else if (state == "2")
                {
                    sql_state = "update PC_purinformcommitall set PC_PFT_STATE='" + newState + "',PC_PFT_SPRC_RESULT='" + rblResult3.SelectedValue + "',PC_PFT_SPRC_TIME='" + DateTime.Now.ToString() + "',PC_PFT_SPRC_SUG='" + third_opinion.Text.Trim() + "'  where PC_PFT_ID='" + PC_PFT_ID_code + "'";
                }
                list_sql.Add(sql_state);


                //string csr_spr = "select * from PC_purinformcommitall where PC_PFT_ID='" + PC_PFT_ID_code + "'";
                //DataTable initp_dt = DBCallCommon.GetDTUsingSqlText(csr_spr);
                //if(initp_dt.Rows[0]["PC_PFT_SPRA_ID"].ToString()==csr_person)
                //{
                //    csr_jg=rblResult1.SelectedValue;
                //    csr_note=first_opinion.Text;
                //    string csr_spr_a="update PC_purinformcommitall set PC_PFT_SPRA_RESULT='"+csr_jg+"', PC_PFT_SPRA_TIME='"+csr_time+"', PC_PFT_SPRA_SUG='"+csr_note+"' where PC_PFT_ID='" + PC_PFT_ID_code + "'";
                //    list_sql.Add(csr_spr_a);
                //}
                //else if(initp_dt.Rows[0][PC_PFT_SPRB_ID].ToString()==csr_person)
                //{
                //    string csr_spr_b="update PC_purinformcommitall set PC_PFT_SPRB_RESULT='"+csr_jg+"', PC_PFT_SPRB_TIME='"+csr_time+"', PC_PFT_SPRB_SUG='"+csr_note+"' where PC_PFT_ID='" + PC_PFT_ID_code + "'";
                //    list_sql.Add(csr_spr_b);              
                //}
                //else if (initp_dt.Rows[0][PC_PFT_SPRC_ID].ToString() == csr_person)
                //{
                //    string csr_spr_c = "update PC_purinformcommitall set PC_PFT_SPRC_RESULT='" + csr_jg + "', PC_PFT_SPRC_TIME='" + csr_time + "', PC_PFT_SPRC_SUG='" + csr_note + "' where PC_PFT_ID='" + PC_PFT_ID_code + "'";
                //    list_sql.Add(csr_spr_c);           
                //}

                #endregion
            }
            try
            {
                DBCallCommon.ExecuteTrans(list_sql);
                if (newState == "4")
                {
                    for (int i = 0; i < reviewer.Count; i++)
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(reviewer.Values.ElementAt(i)), new List<string>(), new List<string>(), "采购信息交流", "您有一条采购信息交流单需要处理，编号为'" + PC_PFT_ID_code + "'。");
                    }
                }
                if (newState == "5")
                {
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(text_zdrid.Text.Trim()), new List<string>(), new List<string>(), "采购信息交流", "您有一条采购信息交流单被驳回。");
                }
                if (action == "Review")
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "window.close();", true); return;
                }
                else
                {
                    Response.Redirect("PC_Pur_inform_commit.aspx");
                }
            }
            catch
            {
                Response.Write("<script>alert('提交出现问题，请联系管理员！！');</script>");
                return;
            }


        }
        protected void getLeaderInfo()
        {
            /******绑定人员信息*****/
            //getStaffInfo("07", "市场部", 4);
            getStaffInfo("03", "技术质量部", 3);
            getStaffInfo("05", "采购部", 0);
            getStaffInfo("04", "生产管理部", 1);
            getStaffInfo("12", "质量部", 2);
            //getStaffInfo("06", "财务部", 3);
            //getStaffInfo("01", "公司领导", 5);
            //得到领导信息，根据金额
            Panel2.Controls.Add(t);
        }

        protected void getStaffInfo(string st_id, string DEP_NAME, int i)
        {
            string sql = string.Format("select ST_NAME,ST_ID,ST_DEPID from TBDS_STAFFINFO as a  where a.ST_PD='0'and a.st_depid='{0}' ", st_id);
            bindInfo(sql, i, DEP_NAME, st_id);
        }
        /**********************动态的绑定评审人员的信息*************************************/
        private void bindInfo(string sql, int i, string DEP_NAME, string st_id)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count != 0)
            {
                TableRow tr = new TableRow();
                TableCell td = new TableCell();
                td.Width = 100;
                TableCell td1 = new TableCell();//第一列为部门名称
                if (i == 3)
                {
                    td.Text = "抄送至:";
                }
                td1.Width = 85;
                Label lab = new Label();
                lab.Text = DEP_NAME + ":";
                Label lab1 = new Label();
                lab1.ID = "dep" + i.ToString();
                lab1.Text = st_id;
                lab1.Visible = false;
                td1.Controls.Add(lab);
                td1.Controls.Add(lab1);
                tr.Cells.Add(td);
                tr.Cells.Add(td1);

                CheckBoxList cki = new CheckBoxList();//第二列为领导的姓名
                cki.ID = "cki" + i.ToString();
                cki.DataSource = dt;
                cki.DataTextField = "ST_NAME";//领导的姓名
                cki.DataValueField = "ST_ID";//部门的编号
                cki.DataBind();
                if (i == 4)
                {
                    for (int k = 0; k < cki.Items.Count; k++)
                    {
                        //cki.Items[k].Attributes.Add("Onclick", "CheckBoxList_Click(this)");//使用了javascript使其只能勾选一个
                    }
                }
                cki.RepeatColumns = 5;//获取显示的列数
                TableCell td2 = new TableCell();
                td2.Controls.Add(cki);
                tr.Cells.Add(td2);
                t.Controls.Add(tr);
                rowsum++;
            }
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
            filePath = @"E:/采购信息交流";//附件上传位置            

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

                            sqlStr = "insert into PC_PurInforCom_Attach (PC_PFT_ID,SAVEURL,UPLOADDATE,FILENAME)";
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
        private void GVBind()
        {
            if (txtCS_CODE.Text == "")
            {
                txtCS_CODE.Text = PC_PFT_ID_code;
            }

            string sql = "select * from PC_PurInforCom_Attach where PC_PFT_ID='" + txtCS_CODE.Text + "' ";
            DataSet ds = DBCallCommon.FillDataSet(sql);
            gvfileslist.DataSource = ds.Tables[0];
            gvfileslist.DataBind();
            gvfileslist.DataKeyNames = new string[] { "ID" };
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
            string sqlStr = "select SAVEURL,FILENAME from PC_PurInforCom_Attach where ID='" + idd + "'";
            //在文件夹Files下，停用该文件
            DeleteFile(sqlStr);
            string sqlDelStr = "delete from PC_PurInforCom_Attach where ID='" + idd + "'";//停用数据库中的记录
            DBCallCommon.ExeSqlText(sqlDelStr);
            GVBind();

        }

        protected void DeleteFile(string sqlStr)
        {
            //打开数据库
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取指定文件的路径
            string strFilePath = ds.Tables[0].Rows[0]["SAVEURL"].ToString() + @"\" + ds.Tables[0].Rows[0]["FILENAME"].ToString();
            //调用File类的Delete方法，停用指定文件
            File.Delete(strFilePath);//文件不存在也不会引发异常
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
            string sqlStr = "select SAVEURL,FILENAME from PC_PurInforCom_Attach where ID='" + idd + "'";
            //打开数据库
            //Response.Write(sqlStr);         
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取文件路径
            string strFilePath = ds.Tables[0].Rows[0]["SAVEURL"].ToString() + @"\" + ds.Tables[0].Rows[0]["FILENAME"].ToString();
            Response.Write(strFilePath);





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

        private void bindReviewer()
        {
            List<string> list = new List<string>();
            foreach (Control item in Panel2.Controls)
            {
                list.Add(item.ID);
            }
            int count = 0;
            for (int i = 0; i < 4; i++)
            {
                CheckBoxList ck = (CheckBoxList)Panel2.FindControl("cki" + i.ToString());
                Label lb = (Label)Panel2.FindControl("dep" + i.ToString());
                if (ck != null)
                {
                    for (int j = 0; j < ck.Items.Count; j++)
                    {
                        if (ck.Items[j].Selected == true)
                        {
                            reviewer.Add(lb.Text + ck.Items[j].Value.ToString(), ck.Items[j].Value.ToString());//字典，绑定部门领导的编号
                            count++;
                        }
                    }
                }
            }
        }


        protected bool CheckPs(bool n, List<string> id)
        {
            for (int i = 0; i < id.Count; i++)
            {
                CheckBoxList cbl = (CheckBoxList)Panel2.FindControl(id[i]);
                if (cbl != null)
                {
                    for (int j = 0; j < cbl.Items.Count; j++)
                    {
                        if (cbl.Items[j].Selected)
                        {
                            n = false;
                        }
                    }
                }
            }
            return n;
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            if (action == "View" || action == "Review")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "window.close();", true); return;
            }
            else
            {
            Response.Redirect("PC_Pur_inform_commit.aspx");
            }
        }




    }
}
