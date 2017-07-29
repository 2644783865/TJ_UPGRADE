using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_KaoHe : System.Web.UI.Page
    {
        string action;
        string id = string.Empty;
        string khTime = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
                action = Request.QueryString["action"];
            hidAction.Value = action;
            if (Request.QueryString["id"] != null)
                id = Request.QueryString["id"];
            if (Request.QueryString["khTime"] != null)
                khTime = Request.QueryString["khTime"];
            if (!IsPostBack)
            {
                InitP();
                ContralEnable();

            }
            lbtitle.Text = ddlType.SelectedValue.ToString().Trim();

            //审核部分
            lbtitlesh.Text = ddlType.SelectedValue.ToString().Trim();
            //0:未提交,1:被考核人填写完成情况,2:一级平分,3:二级平分,4:三级平分,5四级平分,6:被考核人反馈,7:流程结束
            //能否初始化
            string sqlinitable = "select * from TBDS_KaoHeList where Kh_Context='" + id + "'";
            System.Data.DataTable dtinitable = DBCallCommon.GetDTUsingSqlText(sqlinitable);
            if (dtinitable.Rows.Count > 0)
            {
                if (Session["UserID"].ToString().Trim() == dtinitable.Rows[0]["kh_AddPer"].ToString().Trim() && dtinitable.Rows[0]["Kh_shtoltalstate"].ToString().Trim() == "3")
                {
                    btninit.Visible = true;
                }
                else
                {
                    btninit.Visible = false;
                }
            }
            else
            {
                btninit.Visible = false;
            }
            if (lbtitle.Text == "合同续订考核")
            {
                Image5.Visible = true;
                Image6.Visible = true;
                Image7.Visible = true;
            }
        }

        private void ContralEnable()
        {
            if (hidAction.Value == "add")
            {
                btnAudit.Visible = false;
                //审核部分
                btnSave.Visible = false;
                btnbacklast.Visible = false;
                rblSHJS.Enabled = true;

                txtshname1.Enabled = true;
                Hylsh1.Visible = true;

                txtshname2.Enabled = true;
                Hylsh2.Visible = true;

                txtshname3.Enabled = true;
                Hylsh3.Visible = true;

                txtshname4.Enabled = true;
                Hylsh4.Visible = true;

                yjshh.Visible = true;
                ejshh.Visible = true;
                sjshh.Visible = false;
                foursh.Visible = false;
            }
            else
            {
                hlSelect1.Visible = false;
                hlSelect2.Visible = false;
                hlSelect3.Visible = false;
                hlSelect5.Visible = false;
                if (hidAction.Value == "view")
                {
                    btnAudit.Visible = false;
                    btnsubmit.Enabled = false;
                    Panel0.Enabled = false;
                    Panel1.Enabled = false;
                    Panel2.Enabled = false;
                    Panel3.Enabled = false;
                    Panel4.Enabled = false;
                    Panel5.Enabled = false;


                    //审核部分
                    btnSave.Visible = false;
                    btnbacklast.Visible = false;
                    rblSHJS.Enabled = false;
                    if (rblSHJS.SelectedIndex == 0)
                    {
                        yjshh.Visible = false;
                        ejshh.Visible = false;
                        sjshh.Visible = false;
                        foursh.Visible = false;
                    }
                    else if (rblSHJS.SelectedIndex == 1)
                    {
                        yjshh.Visible = true;
                        ejshh.Visible = false;
                        sjshh.Visible = false;
                        foursh.Visible = false;
                    }
                    else if (rblSHJS.SelectedIndex == 2)
                    {
                        yjshh.Visible = true;
                        ejshh.Visible = true;
                        sjshh.Visible = false;
                        foursh.Visible = false;
                    }
                    else if (rblSHJS.SelectedIndex == 3)
                    {
                        yjshh.Visible = true;
                        ejshh.Visible = true;
                        sjshh.Visible = true;
                        foursh.Visible = false;
                    }
                    else
                    {
                        yjshh.Visible = true;
                        ejshh.Visible = true;
                        sjshh.Visible = true;
                        foursh.Visible = true;
                    }
                    //审核部分end
                }
                else if (hidAction.Value == "edit")
                {
                    hlSelect1.Visible = true;
                    hlSelect2.Visible = true;
                    hlSelect3.Visible = true;
                    hlSelect5.Visible = true;
                    btnAudit.Visible = false;

                    //审核部分
                    btnSave.Visible = false;
                    btnbacklast.Visible = false;
                    rblSHJS.Enabled = true;
                    if (rblSHJS.SelectedIndex == 0)
                    {
                        txtshname1.Enabled = false;
                        Hylsh1.Visible = false;

                        txtshname2.Enabled = false;
                        Hylsh2.Visible = false;

                        txtshname3.Enabled = false;
                        Hylsh3.Visible = false;

                        txtshname4.Enabled = false;
                        Hylsh4.Visible = false;

                        yjshh.Visible = false;
                        ejshh.Visible = false;
                        sjshh.Visible = false;
                        foursh.Visible = false;
                    }
                    else if (rblSHJS.SelectedIndex == 1)
                    {
                        txtshname1.Enabled = true;
                        Hylsh1.Visible = true;

                        txtshname2.Enabled = false;
                        Hylsh2.Visible = false;

                        txtshname3.Enabled = false;
                        Hylsh3.Visible = false;

                        txtshname4.Enabled = false;
                        Hylsh4.Visible = false;

                        yjshh.Visible = true;
                        ejshh.Visible = false;
                        sjshh.Visible = false;
                        foursh.Visible = false;
                    }
                    else if (rblSHJS.SelectedIndex == 2)
                    {
                        txtshname1.Enabled = true;
                        Hylsh1.Visible = true;

                        txtshname2.Enabled = true;
                        Hylsh2.Visible = true;

                        txtshname3.Enabled = false;
                        Hylsh3.Visible = false;

                        txtshname4.Enabled = false;
                        Hylsh4.Visible = false;

                        yjshh.Visible = true;
                        ejshh.Visible = true;
                        sjshh.Visible = false;
                        foursh.Visible = false;
                    }
                    else if (rblSHJS.SelectedIndex == 3)
                    {
                        txtshname1.Enabled = true;
                        Hylsh1.Visible = true;

                        txtshname2.Enabled = true;
                        Hylsh2.Visible = true;

                        txtshname3.Enabled = true;
                        Hylsh3.Visible = true;

                        txtshname4.Enabled = false;
                        Hylsh4.Visible = false;


                        yjshh.Visible = true;
                        ejshh.Visible = true;
                        sjshh.Visible = true;
                        foursh.Visible = false;
                    }
                    else
                    {
                        txtshname1.Enabled = true;
                        Hylsh1.Visible = true;

                        txtshname2.Enabled = true;
                        Hylsh2.Visible = true;

                        txtshname3.Enabled = true;
                        Hylsh3.Visible = true;

                        txtshname4.Enabled = true;
                        Hylsh4.Visible = true;

                        yjshh.Visible = true;
                        ejshh.Visible = true;
                        sjshh.Visible = true;
                        foursh.Visible = true;
                    }
                    //审核部分end
                }


                else if (hidAction.Value == "audit")
                {
                    btnsubmit.Visible = false;
                    btnAudit.Visible = true;
                    Panel0.Enabled = false;
                    Panel1.Enabled = false;
                    Panel2.Enabled = false;
                    Panel3.Enabled = false;
                    Panel4.Enabled = false;
                    Panel5.Enabled = false;
                    if (hidState.Value == "2")
                    {
                        Panel1.Enabled = true;
                        hlSelect2.Visible = true;
                        hlSelect3.Visible = true;
                        hlSelect5.Visible = true;
                    }
                    else if (hidState.Value == "3")
                    {
                        hlSelect3.Visible = true;
                        hlSelect5.Visible = true;
                        Panel2.Enabled = true;
                    }
                    else if (hidState.Value == "4")
                    {
                        hlSelect5.Visible = true;
                        Panel3.Enabled = true;
                    }
                    else if (hidState.Value == "5")
                    {
                        Panel5.Enabled = true;
                    }
                    else if (hidState.Value == "6")
                    {
                        Panel4.Enabled = true;
                    }

                    //审核部分
                    btnSave.Visible = false;
                    btnbacklast.Visible = false;
                    rblSHJS.Enabled = false;
                    if (rblSHJS.SelectedIndex == 0)
                    {
                        yjshh.Visible = false;
                        ejshh.Visible = false;
                        sjshh.Visible = false;
                        foursh.Visible = false;
                    }
                    else if (rblSHJS.SelectedIndex == 1)
                    {
                        yjshh.Visible = true;
                        ejshh.Visible = false;
                        sjshh.Visible = false;
                        foursh.Visible = false;
                    }
                    else if (rblSHJS.SelectedIndex == 2)
                    {
                        yjshh.Visible = true;
                        ejshh.Visible = true;
                        sjshh.Visible = false;
                        foursh.Visible = false;
                    }
                    else if (rblSHJS.SelectedIndex == 3)
                    {
                        yjshh.Visible = true;
                        ejshh.Visible = true;
                        sjshh.Visible = true;
                        foursh.Visible = false;
                    }
                    else
                    {
                        yjshh.Visible = true;
                        ejshh.Visible = true;
                        sjshh.Visible = true;
                        foursh.Visible = true;
                    }
                    //审核部分end
                }
                //审核部分
                else if (action == "auditsh")
                {
                    btnAudit.Visible = false;
                    btnsubmit.Enabled = false;
                    Panel0.Enabled = false;
                    Panel1.Enabled = false;
                    Panel2.Enabled = false;
                    Panel3.Enabled = false;
                    Panel4.Enabled = false;
                    Panel5.Enabled = false;

                    if (rblSHJS.SelectedIndex == 0)
                    {
                        yjshh.Visible = false;
                        ejshh.Visible = false;
                        sjshh.Visible = false;
                        foursh.Visible = false;
                    }
                    else if (rblSHJS.SelectedIndex == 1)
                    {
                        yjshh.Visible = true;
                        ejshh.Visible = false;
                        sjshh.Visible = false;
                        foursh.Visible = false;
                    }
                    else if (rblSHJS.SelectedIndex == 2)
                    {
                        yjshh.Visible = true;
                        ejshh.Visible = true;
                        sjshh.Visible = false;
                        foursh.Visible = false;
                    }
                    else if (rblSHJS.SelectedIndex == 3)
                    {
                        yjshh.Visible = true;
                        ejshh.Visible = true;
                        sjshh.Visible = true;
                        foursh.Visible = false;
                    }
                    else
                    {
                        yjshh.Visible = true;
                        ejshh.Visible = true;
                        sjshh.Visible = true;
                        foursh.Visible = true;
                    }
                    string sqlgetshinfo = "select * from TBDS_KaoHeList where Kh_Context='" + id + "'";
                    System.Data.DataTable dtgetshinfo = DBCallCommon.GetDTUsingSqlText(sqlgetshinfo);
                    if (dtgetshinfo.Rows.Count > 0)
                    {
                        if (dtgetshinfo.Rows[0]["Kh_shtoltalstate"].ToString().Trim() == "1")
                        {
                            if (dtgetshinfo.Rows[0]["kh_shstate1"].ToString().Trim() == "0" && dtgetshinfo.Rows[0]["Kh_shrid1"].ToString().Trim() == Session["UserID"].ToString().Trim())
                            {
                                btnSave.Visible = true;
                                btnbacklast.Visible = true;
                                rblSHJS.Enabled = false;

                                txtshname1.Enabled = false;
                                Hylsh1.Visible = false;
                                rblsh1.Enabled = true;
                                opinionsh1.Enabled = true;
                            }
                            else if (dtgetshinfo.Rows[0]["kh_shstate1"].ToString().Trim() == "1" && dtgetshinfo.Rows[0]["kh_shstate2"].ToString().Trim() == "0" && dtgetshinfo.Rows[0]["Kh_shrid2"].ToString().Trim() == Session["UserID"].ToString().Trim())
                            {
                                btnSave.Visible = true;
                                btnbacklast.Visible = true;
                                rblSHJS.Enabled = false;

                                txtshname2.Enabled = false;
                                Hylsh2.Visible = false;
                                rblsh2.Enabled = true;
                                opinionsh2.Enabled = true;
                            }
                            else if (dtgetshinfo.Rows[0]["kh_shstate2"].ToString().Trim() == "1" && dtgetshinfo.Rows[0]["kh_shstate3"].ToString().Trim() == "0" && dtgetshinfo.Rows[0]["Kh_shrid3"].ToString().Trim() == Session["UserID"].ToString().Trim())
                            {
                                btnSave.Visible = true;
                                btnbacklast.Visible = true;
                                rblSHJS.Enabled = false;

                                txtshname3.Enabled = false;
                                Hylsh3.Visible = false;
                                rblsh3.Enabled = true;
                                opinionsh3.Enabled = true;
                            }
                            else if (dtgetshinfo.Rows[0]["kh_shstate3"].ToString().Trim() == "1" && dtgetshinfo.Rows[0]["kh_shstate4"].ToString().Trim() == "0" && dtgetshinfo.Rows[0]["Kh_shrid4"].ToString().Trim() == Session["UserID"].ToString().Trim())
                            {
                                btnSave.Visible = true;
                                btnbacklast.Visible = true;
                                rblSHJS.Enabled = false;

                                txtshname4.Enabled = false;
                                Hylsh4.Visible = false;
                                rblsh4.Enabled = true;
                                opinionsh4.Enabled = true;
                            }
                        }
                        else
                        {
                            btnSave.Visible = false;
                            btnbacklast.Visible = false;
                            rblSHJS.Enabled = false;


                            txtshname1.Enabled = false;
                            Hylsh1.Visible = false;
                            rblsh1.Enabled = false;
                            opinionsh1.Enabled = false;


                            txtshname2.Enabled = false;
                            Hylsh2.Visible = false;
                            rblsh2.Enabled = false;
                            opinionsh2.Enabled = false;


                            txtshname3.Enabled = false;
                            Hylsh3.Visible = false;
                            rblsh3.Enabled = false;
                            opinionsh3.Enabled = false;


                            txtshname4.Enabled = false;
                            Hylsh4.Visible = false;
                            rblsh4.Enabled = false;
                            opinionsh4.Enabled = false;
                        }
                    }
                }
            }

        }

        private void InitP()
        {
            if (action == "add")
            {
                lb1.Text = Session["UserName"].ToString();
                txtTime.Text = DateTime.Now.ToString("yyyy-MM-dd").Trim();
                txtKhNianYue.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM").Trim();
                BindPart();
                BindPosition();
                BindPeople();
                BindPartMB();
                BindMb();
            }
            else
            {
                string sql = "select * from dbo.View_KaoHList  where kh_Context='" + id + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    //ListItem item = new ListItem(dt.Rows[0]["DEP_NAME"].ToString(), dt.Rows[0]["ST_DEPID"].ToString());
                    //ddl_Depart.Items.Add(item);
                    BindPart();
                    BindPartMB();
                    ddl_Depart.SelectedValue = dt.Rows[0]["ST_DEPID"].ToString();

                    ddlType.SelectedValue = dt.Rows[0]["Kh_Type"].ToString();
                    ddl_zipstate.SelectedValue = dt.Rows[0]["Kh_zipingif"].ToString().Trim();
                    ListItem item = new ListItem(dt.Rows[0]["POSITION_NAME"].ToString(), dt.Rows[0]["ST_POSITION"].ToString());
                    ddl_Position.Items.Add(item);
                    item = new ListItem(dt.Rows[0]["ST_NAME"].ToString(), dt.Rows[0]["kh_Id"].ToString());
                    ddl_Person.Items.Add(item);

                    string sqltext = "select Kh_Name,kh_Fkey,kh_Dep from TBDS_KaoHeMBList where kh_Fkey='" + dt.Rows[0]["kh_Fkey"].ToString() + "'";
                    DataTable dtMB = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (dtMB.Rows.Count > 0)
                    {
                        ddlPartMB.SelectedValue = dtMB.Rows[0]["kh_Dep"].ToString();
                        item = new ListItem(dtMB.Rows[0]["Kh_Name"].ToString(), dtMB.Rows[0]["kh_Fkey"].ToString());
                        ddlKaohMB.Items.Add(item);
                    }
                    // BindPart();
                    // ddl_Depart.SelectedValue = dt.Rows[0]["ST_DEPID"].ToString();
                    //  BindPosition();
                    //  ddl_Position.SelectedValue = dt.Rows[0]["ST_POSITION"].ToString();
                    //   BindPeople();
                    //   BindMb();
                    // ddl_Person.SelectedValue = dt.Rows[0]["kh_Id"].ToString();
                    //   ddlKaohMB.SelectedValue = dt.Rows[0]["kh_Fkey"].ToString();

                    //kh_Id, kh_Score, kh_Time, kh_AddPer, kh_Fkey, kh_Context, Kh_State, Kh_fankui, Kh_SPRA, Kh_SPTIMEA, Kh_SPYJA, Kh_SPRB, Kh_SPTIMEB, Kh_SPYJB, Kh_SPRC, Kh_SPTIMEC, Kh_SPYJC, Kh_FKTIME, Kh_FKYJ, Kh_BL, ST_NAME, ST_DEPID, ST_POSITION, AddPerNM, SHRANM, SHRBNM, SHRCNM
                    ddlFankui.SelectedValue = dt.Rows[0]["Kh_Fankui"].ToString();
                    txt_first.Text = dt.Rows[0]["SHRANM"].ToString();
                    firstid.Value = dt.Rows[0]["Kh_SPRA"].ToString();
                    first_time.Text = dt.Rows[0]["Kh_SPTIMEA"].ToString();
                    first_opinion.Text = dt.Rows[0]["Kh_SPYJA"].ToString();

                    txt_second.Text = dt.Rows[0]["SHRBNM"].ToString();
                    secondid.Value = dt.Rows[0]["Kh_SPRB"].ToString();
                    second_time.Text = dt.Rows[0]["Kh_SPTIMEB"].ToString();
                    second_opinion.Text = dt.Rows[0]["Kh_SPYJB"].ToString();
                    txt_third.Text = dt.Rows[0]["SHRCNM"].ToString();
                    thirdid.Value = dt.Rows[0]["Kh_SPRC"].ToString();
                    third_time.Text = dt.Rows[0]["Kh_SPTIMEC"].ToString();
                    third_opinion.Text = dt.Rows[0]["Kh_SPYJC"].ToString();
                    txt_fourth.Text = dt.Rows[0]["ST_NAME"].ToString();
                    txtTime.Text = dt.Rows[0]["kh_Time"].ToString();
                    fourthId.Value = dt.Rows[0]["kh_Id"].ToString();
                    fourth_opinion.Text = dt.Rows[0]["Kh_FKYJ"].ToString();


                    txt_fifth.Text = dt.Rows[0]["SHRDNM"].ToString();
                    fifthId.Value = dt.Rows[0]["Kh_SPRD"].ToString();
                    fif_time.Text = dt.Rows[0]["Kh_SPTIMED"].ToString();
                    fif_opinion.Text = dt.Rows[0]["Kh_SPYJD"].ToString();

                    hidId.Value = dt.Rows[0]["Id"].ToString();
                    hidConext.Value = dt.Rows[0]["Kh_Context"].ToString();
                    btnAudit.Visible = false;
                    lb1.Text = dt.Rows[0]["AddPerNM"].ToString();
                    hidState.Value = dt.Rows[0]["Kh_State"].ToString();
                    txtResult0.Text = dt.Rows[0]["kh_ScoreOwn"].ToString();
                    txtResult1.Text = dt.Rows[0]["Kh_Score0"].ToString();
                    txtResult2.Text = dt.Rows[0]["Kh_Score1"].ToString();
                    txtResult3.Text = dt.Rows[0]["Kh_Score2"].ToString();

                    txtResult4.Text = dt.Rows[0]["Kh_Score3"].ToString();

                    txt_Result.Text = dt.Rows[0]["Kh_Score"].ToString();
                    txtKhNianYue.Text = dt.Rows[0]["Kh_Year"].ToString() + "-" + dt.Rows[0]["Kh_Month"].ToString();
                    txtPFBZ.Text = dt.Rows[0]["Kh_PFBZ"].ToString();
                    BindKh();


                    //审核部分
                    Bindsh();
                }

            }
        }

        private void BindMb()
        {


            string sql = string.Format("select Kh_Name,kh_Fkey from TBDS_KaoHeMBList where kh_dep='{0}' and kh_State='0'", ddlPartMB.SelectedValue);//按照表里面的顺序排。不然直接distinct会按拼音排序。
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddlKaohMB.DataSource = dt;
            ddlKaohMB.DataTextField = "Kh_Name";
            ddlKaohMB.DataValueField = "kh_Fkey";
            ddlKaohMB.DataBind();
            ListItem item = new ListItem();
            item.Text = "-请选择-";
            item.Value = "00";
            ddlKaohMB.Items.Insert(0, item);



        }


        private void BindPart()
        {
            string sql = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddl_Depart.Items.Insert(0, item);
            ddl_Depart.SelectedValue = Session["UserDeptID"].ToString().Trim();
        }



        private void BindPartMB()
        {
            string sql = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddlPartMB.DataSource = dt;
            ddlPartMB.DataTextField = "DEP_NAME";
            ddlPartMB.DataValueField = "DEP_CODE";
            ddlPartMB.DataBind();
            ListItem itemNew = new ListItem();
            itemNew.Text = "通用模板";
            itemNew.Value = "TongY";
            ddlPartMB.Items.Insert(0, itemNew);
            ListItem item = new ListItem();
            item.Text = "-请选择-";
            item.Value = "00";
            ddlPartMB.Items.Insert(0, item);
            ddlPartMB.SelectedValue = Session["UserDeptID"].ToString().Trim();
        }


        protected void BindPosition() //将职位信息绑定到职位下拉框
        {
            string sql = string.Format("select min(ST_ID) as ST_ID,ST_POSITION,d.DEP_NAME as DEP_POSITION from TBDS_STAFFINFO as a left join TBDS_DEPINFO as d on a.ST_POSITION = d.DEP_CODE where ST_DEPID='{0}'and a.ST_POSITION<>'' and a.ST_POSITION is not null group by d.DEP_NAME,ST_POSITION order by ST_ID", ddl_Depart.SelectedValue);//按照表里面的顺序排。不然直接distinct会按拼音排序。
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddl_Position.DataSource = dt;
            ddl_Position.DataTextField = "DEP_POSITION";
            ddl_Position.DataValueField = "ST_POSITION";
            ddl_Position.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddl_Position.Items.Insert(0, item);
        }

        protected void BindPeople() //绑定人员信息
        {
            string sql = string.Format("select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_POSITION='{0}' and ST_PD='0'", ddl_Position.SelectedValue);//按照表里面的顺序排。不然直接distinct会按拼音排序。
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddl_Person.DataSource = dt;
            ddl_Person.DataTextField = "ST_NAME";
            ddl_Person.DataValueField = "ST_ID";
            ddl_Person.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddl_Person.Items.Insert(0, item);
        }

        protected void ddl_Depart_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPosition();
            ddl_Person.Items.Clear();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddl_Person.Items.Insert(0, item);
        }

        protected void ddl_Position_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindPeople();

        }

        protected void ddl_Person_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindMb();
        }
        protected void btn_Click(object sender, EventArgs e)
        {
            Det_Repeater.DataSource = null;
            Det_Repeater.DataBind();
            downloadMB();

        }


        private void BindKh()
        {
            id = Request.QueryString["id"].ToString().Trim();


            string sql = "select * from TBDS_KaoHeDetail where kh_Context='" + hidConext.Value + "'  order by kh_Id asc";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {

                sql = "select * from TBDS_KaoHeColReal where kh_context='" + dt.Rows[0]["kh_Context"] + "'";
                DataTable col = DBCallCommon.GetDTUsingSqlText(sql);
                if (col.Rows.Count > 0)
                {
                    Det_Repeater.DataSource = dt;
                    Det_Repeater.DataBind();
                    //绑定列名
                    List<int> cols = new List<int>();
                    for (int i = 1; i < 13; i++)
                    {
                        Label lb = (Label)Det_Repeater.Controls[0].FindControl("kh_Col" + i);
                        string str = col.Rows[0]["kh_Col" + i].ToString();
                        lb.Text = str;
                        if (str == "")
                        {
                            cols.Add(i);
                            Control head = Det_Repeater.Controls[0];
                            HtmlTableCell htc = (HtmlTableCell)head.FindControl("col" + i);
                            htc.Visible = false;
                        }
                    }
                    foreach (RepeaterItem item in Det_Repeater.Items)
                    {
                        for (int i = 0; i < cols.Count; i++)
                        {
                            HtmlTableCell cont = (HtmlTableCell)item.FindControl("td" + cols[i]);
                            cont.Visible = false;
                        }
                        for (int i = 1; i < 13 - cols.Count; i++)
                        {
                            HtmlTableCell hcont = (HtmlTableCell)item.FindControl("td" + i);
                            if (!hcont.InnerText.Contains("<br />"))
                            {
                                hcont.Style.Add("text-align", "center");
                            }
                        }
                    }
                    sql = "select * from TBDS_KaoHeList where Kh_Context='" + id + "'";
                    DataTable bl = DBCallCommon.GetDTUsingSqlText(sql);
                    if (bl.Rows.Count > 0)
                    {
                        string txtbl = bl.Rows[0]["Kh_BL"].ToString();
                        txtPFBZ.Text = bl.Rows[0]["Kh_PFBZ"].ToString();
                        lblBL.Text = txtbl;
                        foreach (RepeaterItem item in Det_Repeater.Controls)
                        {
                            if (item.ItemType == ListItemType.Header)
                            {
                                for (int i = 0; i < txtbl.Split('|').Count(); i++)
                                {
                                    if (txtbl.Split('|')[i] == "0")
                                    {
                                        HtmlTableCell box = (HtmlTableCell)item.FindControl("thScore" + (i + 1));
                                        box.Visible = false;
                                    }

                                }
                            }

                            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                            {
                                for (int i = 0; i < txtbl.Split('|').Count(); i++)
                                {
                                    if (txtbl.Split('|')[i] == "0")
                                    {
                                        HtmlTableCell box = (HtmlTableCell)item.FindControl("tdScore" + (i + 1));
                                        box.Visible = false;
                                    }

                                }
                            }

                        }
                        Panel1.Visible = true;
                        Panel2.Visible = true;
                        Panel3.Visible = true;

                        if (txtbl.Split('|')[0] == "0")
                        {
                            tfResult1.Visible = false;
                            Panel1.Visible = false;
                        }
                        if (txtbl.Split('|')[1] == "0")
                        {
                            tfResult2.Visible = false;
                            Panel2.Visible = false;
                        }
                        if (txtbl.Split('|')[2] == "0")
                        {
                            tfResult3.Visible = false;
                            Panel3.Visible = false;
                        }
                        if (txtbl.Split('|')[3] == "0")
                        {
                            tfResult4.Visible = false;
                            Panel5.Visible = false;
                        }
                        if (ddlFankui.SelectedValue == "1")
                        {
                            Panel4.Visible = false;
                        }
                    }


                    foot.ColSpan = 13 - cols.Count;
                    sql = "select sum(kh_Weight) from TBDS_KaoHeDetail where kh_context='" + hidConext.Value + "'";
                    lb_Result.Text = DBCallCommon.GetDTUsingSqlText(sql).Rows[0][0].ToString();
                    tr_foot.Visible = true;
                    Det_Repeater.Visible = true;
                    NoDataPanel.Visible = false;
                }
                else
                {
                    // ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('考核模板列名出错！请重新添加模板或者联系管理员');", true);
                    Response.Write("<script>alert('考核模板列名出错！请重新添加模板或者联系管理员！');</script>");

                    Det_Repeater.Visible = false;
                    NoDataPanel.Visible = true;
                }
            }
            else
            {
                // ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('未成功加载考核模板！请添加模板或者联系管理员');", true);
                Response.Write("<script>alert('未成功加载考核模板！请添加模板或者联系管理员！');</script>");
                Det_Repeater.Visible = false;
                NoDataPanel.Visible = true;
            }
        }


        //审核部分（数据绑定）
        private void Bindsh()
        {
            string sqlgetshinfo = "select * from TBDS_KaoHeList where Kh_Context='" + id + "'";
            System.Data.DataTable dtgetshinfo = DBCallCommon.GetDTUsingSqlText(sqlgetshinfo);
            if (dtgetshinfo.Rows.Count > 0)
            {
                rblSHJS.SelectedValue = dtgetshinfo.Rows[0]["Kh_shjs"].ToString().Trim();
                if (dtgetshinfo.Rows[0]["Kh_shtoltalstate"].ToString().Trim() == "2")
                {
                    ImageVerify.Visible = true;
                }
                txtshname1.Text = dtgetshinfo.Rows[0]["Kh_shrname1"].ToString().Trim();
                txtshid1.Value = dtgetshinfo.Rows[0]["Kh_shrid1"].ToString().Trim();
                rblsh1.SelectedValue = dtgetshinfo.Rows[0]["kh_shstate1"].ToString().Trim();
                lbshtime1.Text = dtgetshinfo.Rows[0]["Kh_shtime1"].ToString().Trim();
                opinionsh1.Text = dtgetshinfo.Rows[0]["Kh_shnote1"].ToString().Trim();

                txtshname2.Text = dtgetshinfo.Rows[0]["Kh_shrname2"].ToString().Trim();
                txtshid2.Value = dtgetshinfo.Rows[0]["Kh_shrid2"].ToString().Trim();
                rblsh2.SelectedValue = dtgetshinfo.Rows[0]["kh_shstate2"].ToString().Trim();
                lbshtime2.Text = dtgetshinfo.Rows[0]["Kh_shtime2"].ToString().Trim();
                opinionsh2.Text = dtgetshinfo.Rows[0]["Kh_shnote2"].ToString().Trim();

                txtshname3.Text = dtgetshinfo.Rows[0]["Kh_shrname3"].ToString().Trim();
                txtshid3.Value = dtgetshinfo.Rows[0]["Kh_shrid3"].ToString().Trim();
                rblsh3.SelectedValue = dtgetshinfo.Rows[0]["kh_shstate3"].ToString().Trim();
                lbshtime3.Text = dtgetshinfo.Rows[0]["Kh_shtime3"].ToString().Trim();
                opinionsh3.Text = dtgetshinfo.Rows[0]["Kh_shnote3"].ToString().Trim();

                txtshname4.Text = dtgetshinfo.Rows[0]["Kh_shrname4"].ToString().Trim();
                txtshid4.Value = dtgetshinfo.Rows[0]["Kh_shrid4"].ToString().Trim();
                rblsh4.SelectedValue = dtgetshinfo.Rows[0]["kh_shstate4"].ToString().Trim();
                lbshtime4.Text = dtgetshinfo.Rows[0]["Kh_shtime4"].ToString().Trim();
                opinionsh4.Text = dtgetshinfo.Rows[0]["Kh_shnote4"].ToString().Trim();
            }
        }
        //审核部分end

        private void downloadMB()
        {
            string sql = "";
            if (action == "add")
            {
                sql = "select *,'' as kh_Note,'' as kh_Score1,'' as kh_Score2,'' as kh_Score3,'' as kh_Score4,'' as kh_ScoreOwn,'' as Id, '' as kh_BEIZHU from View_KaoHe where kh_Fkey='" + ddlKaohMB.SelectedValue + "' and kh_State='0' order by kh_Id asc";

            }
            else
            {
                sql = "select *,'' as kh_Note,'' as kh_Score1,'' as kh_Score2,'' as kh_Score3,'' as kh_Score4,'' as kh_ScoreOwn,'' as Id, '' as kh_BEIZHU  from View_KaoHe where kh_Fkey='" + ddlKaohMB.SelectedValue + "'  order by kh_Id asc";
            }

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {

                sql = "select * from TBDS_KaoHeCol where kh_Fkey='" + dt.Rows[0]["kh_Fkey"] + "'";
                DataTable col = DBCallCommon.GetDTUsingSqlText(sql);
                if (col.Rows.Count > 0)
                {
                    Det_Repeater.DataSource = dt;
                    Det_Repeater.DataBind();
                    //绑定列名
                    List<int> cols = new List<int>();
                    for (int i = 1; i < 13; i++)
                    {
                        Label lb = (Label)Det_Repeater.Controls[0].FindControl("kh_Col" + i);
                        string str = col.Rows[0]["kh_Col" + i].ToString();
                        lb.Text = str;
                        if (str == "")
                        {
                            cols.Add(i);
                            Control head = Det_Repeater.Controls[0];
                            HtmlTableCell htc = (HtmlTableCell)head.FindControl("col" + i);
                            htc.Visible = false;
                        }
                    }
                    foreach (RepeaterItem item in Det_Repeater.Items)
                    {
                        for (int i = 0; i < cols.Count; i++)
                        {
                            HtmlTableCell cont = (HtmlTableCell)item.FindControl("td" + cols[i]);
                            cont.Visible = false;
                        }
                        //for (int i = 1; i < 13 - cols.Count; i++)
                        //{
                        //    HtmlTableCell hcont = (HtmlTableCell)item.FindControl("td" + i);
                        //    hcont.InnerText.Replace("\r\n", "<br />");
                        //    hcont.InnerText.Replace("\n", "<br />");
                        //    if (!hcont.InnerText.Contains("<br />"))
                        //    {
                        //        hcont.Style.Add("text-align", "center");
                        //    }

                        //}
                    }
                    sql = "select * from TBDS_KaoHeMBList where kh_Fkey='" + ddlKaohMB.SelectedValue + "'";
                    DataTable bl = DBCallCommon.GetDTUsingSqlText(sql);
                    if (bl.Rows.Count > 0)
                    {
                        string txtbl = bl.Rows[0]["Kh_BL"].ToString();
                        txtPFBZ.Text = bl.Rows[0]["Kh_Note"].ToString();
                        lblBL.Text = txtbl;
                        foreach (RepeaterItem item in Det_Repeater.Controls)
                        {
                            if (item.ItemType == ListItemType.Header)
                            {
                                for (int i = 0; i < txtbl.Split('|').Count(); i++)
                                {
                                    if (txtbl.Split('|')[i] == "0")
                                    {
                                        HtmlTableCell box = (HtmlTableCell)item.FindControl("thScore" + (i + 1));
                                        box.Visible = false;
                                    }

                                }
                            }

                            if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                            {
                                for (int i = 0; i < txtbl.Split('|').Count(); i++)
                                {
                                    if (txtbl.Split('|')[i] == "0")
                                    {
                                        HtmlTableCell box = (HtmlTableCell)item.FindControl("tdScore" + (i + 1));
                                        box.Visible = false;
                                    }

                                }
                            }

                        }
                        Panel1.Visible = true;
                        Panel2.Visible = true;
                        Panel3.Visible = true;

                        if (txtbl.Split('|')[0] == "0")
                        {
                            tfResult1.Visible = false;
                            Panel1.Visible = false;
                        }
                        if (txtbl.Split('|')[1] == "0")
                        {
                            tfResult2.Visible = false;
                            Panel2.Visible = false;
                        }
                        if (txtbl.Split('|')[2] == "0")
                        {
                            tfResult3.Visible = false;
                            Panel3.Visible = false;
                        }
                        if (txtbl.Split('|')[3] == "0")
                        {
                            tfResult4.Visible = false;
                            Panel5.Visible = false;
                        }
                        if (ddlFankui.SelectedValue == "1")
                        {
                            Panel4.Visible = false;
                        }
                    }


                    foot.ColSpan = 13 - cols.Count;
                    sql = "select sum(kh_Weight) from TBDS_KaoHeDetail where kh_Context='" + ddlKaohMB.SelectedValue + "'";
                    lb_Result.Text = DBCallCommon.GetDTUsingSqlText(sql).Rows[0][0].ToString();
                    tr_foot.Visible = true;
                    Det_Repeater.Visible = true;
                    NoDataPanel.Visible = false;
                }
                else
                {
                    //   ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('考核模板列名出错！请重新添加模板或者联系管理员');", true);
                    Response.Write("<script>alert('考核模板列名出错！请重新添加模板或者联系管理员！');</script>");
                    Det_Repeater.Visible = false;
                    NoDataPanel.Visible = true;
                }
            }
            else
            {
                // ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('未成功加载考核模板！请添加模板或者联系管理员');", true);
                Response.Write("<script>alert('未成功加载考核模板！请添加模板或者联系管理员！');</script>");
                Det_Repeater.Visible = false;
                NoDataPanel.Visible = true;
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            string sqlDate = "select * from TBDS_KaoHeDate where kh_Type='" + ddlType.SelectedValue + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlDate);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["kh_Start"].ToString() != "" && dt.Rows[0]["kh_End"].ToString() != "")
                {
                    try
                    {
                        string now = DateTime.Now.ToString("yyyy-MM-dd");
                        DateTime nowDate = DateTime.Parse(now);
                        DateTime start = DateTime.Parse(dt.Rows[0]["kh_Start"].ToString());
                        DateTime end = DateTime.Parse(dt.Rows[0]["kh_End"].ToString());
                        if (DateTime.Compare(nowDate, start) < 0 || DateTime.Compare(nowDate, end) > 0)
                        {
                            Response.Write("<script>alert('当前时间不在允许的考核时间内，提交被终止！')</script>");
                            return;
                        }
                    }
                    catch (Exception)
                    {

                        Response.Write("<script>alert('允许考核时间设置有误，请联系管理员！')</script>");
                        return;
                    }

                }
            }

            if (Checked())
            {
                List<string> list = new List<string>();
                List<string> col = new List<string>();
                string sql = "";
                string year = txtKhNianYue.Text.Split('-')[0];
                string month = txtKhNianYue.Text.Split('-')[1].PadLeft(2, '0');
                string context = DateTime.Now.ToString("yyyyMMddhhmmss");
                if (action == "add")
                {
                    sql = string.Format("insert into TBDS_KaoHeList values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}','{51}','{52}','{53}','{54}')", ddl_Person.SelectedValue, txt_Result.Text, txtTime.Text, Session["UserID"], ddlKaohMB.SelectedValue, "0", ddlFankui.SelectedValue, firstid.Value, "", "", secondid.Value, "", "", thirdid.Value, "", "", "", "", lblBL.Text, ddlType.SelectedValue, context, fifthId.Value, "", "", "", "", "", "", "", year, month, txtPFBZ.Text, ddl_zipstate.SelectedValue.ToString().Trim(), "0", rblSHJS.SelectedValue.ToString().Trim(), txtshid1.Value.Trim(), txtshname1.Text.Trim(), "0", "", "", txtshid2.Value.Trim(), txtshname2.Text.Trim(), "0", "", "", txtshid3.Value.Trim(), txtshname3.Text.Trim(), "0", "", "", txtshid4.Value.Trim(), txtshname4.Text.Trim(), "0", "", "");
                    list.Add(sql);
                    for (int i = 1; i < 13; i++)
                    {
                        col.Add(((ITextControl)Det_Repeater.Controls[0].FindControl("kh_Col" + i)).Text);
                    }
                    sql = string.Format("insert into TBDS_KaoHeColReal values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')", col[0], col[1], col[2], col[3], col[4], col[5], col[6], col[7], col[8], col[9], col[10], col[11], context);
                    list.Add(sql);

                    foreach (RepeaterItem item in Det_Repeater.Items)
                    {
                        List<string> txt = new List<string>();
                        for (int j = 1; j < 13; j++)
                        {
                            txt.Add(((HtmlTableCell)item.FindControl("td" + j)).InnerText);
                        }
                        string weight = ((HtmlTableCell)item.FindControl("weight")).InnerText.Trim();

                        sql = string.Format("insert into TBDS_KaoHeDetail values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}')", item.ItemIndex, ((ITextControl)item.FindControl("kh_Score0")).Text, ((ITextControl)item.FindControl("kh_Note")).Text, context, ((ITextControl)item.FindControl("kh_Score1")).Text, ((ITextControl)item.FindControl("kh_Score2")).Text, txt[0], txt[1], txt[2], txt[3], txt[4], txt[5], txt[6], txt[7], txt[8], txt[9], txt[10], txt[11], weight, "", "", ((ITextControl)item.FindControl("kh_Note")).Text);
                        list.Add(sql);
                    }
                }
                else if (action == "edit")
                {
                    sql = string.Format("update TBDS_KaoHeList set kh_Fkey='{0}',kh_Id='{1}',kh_Time='{2}',Kh_Type='{3}',Kh_fankui='{4}',Kh_BL='{5}',Kh_SPRA='{6}',Kh_SPRB='{7}',Kh_SPRC='{8}',Kh_SPRD='{9}',Kh_Context='{10}',Kh_Year='{11}',Kh_Month='{12}',Kh_zipingif='{14}',Kh_shjs='{15}',Kh_shrid1='{16}',Kh_shrname1='{17}',Kh_shrid2='{18}',Kh_shrname2='{19}',Kh_shrid3='{20}',Kh_shrname3='{21}',Kh_shrid4='{22}',Kh_shrname4='{23}' where Id='{13}'", ddlKaohMB.SelectedValue, ddl_Person.SelectedValue, txtTime.Text.Trim(), ddlType.SelectedValue, ddlFankui.SelectedValue, lblBL.Text, firstid.Value, secondid.Value, thirdid.Value, fifthId.Value, context, year, month, hidId.Value, ddl_zipstate.SelectedValue.ToString().Trim(), rblSHJS.SelectedValue.ToString().Trim(), txtshid1.Value.Trim(), txtshname1.Text.Trim(), txtshid2.Value.Trim(), txtshname2.Text.Trim(), txtshid3.Value.Trim(), txtshname3.Text.Trim(), txtshid4.Value.Trim(), txtshname4.Text.Trim());
                    list.Add(sql);
                    for (int i = 1; i < 13; i++)
                    {
                        col.Add(((ITextControl)Det_Repeater.Controls[0].FindControl("kh_Col" + i)).Text);
                    }
                    sql = "delete from TBDS_KaoHeColReal where kh_context='" + hidConext.Value + "'";
                    list.Add(sql);
                    sql = string.Format("insert into TBDS_KaoHeColReal values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}')", col[0], col[1], col[2], col[3], col[4], col[5], col[6], col[7], col[8], col[9], col[10], col[11], context);
                    list.Add(sql);

                    sql = "delete from TBDS_KaoHeDetail where Kh_Context='" + hidConext.Value + "'";
                    list.Add(sql);
                    foreach (RepeaterItem item in Det_Repeater.Items)
                    {
                        List<string> txt = new List<string>();
                        for (int j = 1; j < 13; j++)
                        {
                            txt.Add(((HtmlTableCell)item.FindControl("td" + j)).InnerText);
                        }

                        string weight = ((HtmlTableCell)item.FindControl("weight")).InnerText.Trim();
                        sql = string.Format("insert into TBDS_KaoHeDetail values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}')", item.ItemIndex, ((ITextControl)item.FindControl("kh_Score0")).Text, ((ITextControl)item.FindControl("kh_Note")).Text, context, ((ITextControl)item.FindControl("kh_Score1")).Text, ((ITextControl)item.FindControl("kh_Score2")).Text, txt[0], txt[1], txt[2], txt[3], txt[4], txt[5], txt[6], txt[7], txt[8], txt[9], txt[10], txt[11], weight, "", "", ((ITextControl)item.FindControl("kh_Note")).Text);
                        list.Add(sql);
                    }
                }

                DBCallCommon.ExecuteTrans(list);
                hidConext.Value = context;
                hidState.Value = "0";
                Response.Write("<script>alert('保存成功！');</script>");
                //window.location.href='OM_KaoHeList.aspx';
                btnsubmit.Visible = false;
                btnAudit.Visible = true;
            }
        }

        private bool Checked()
        {
            bool result = true;
            if (txtTime.Text.Trim() == "")
            {
                // ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请填写考核时间！')", true);
                Response.Write("<script>alert('请填写考核时间！')</script>");
                result = false;
            }
            if (ddlKaohMB.SelectedValue == "00")
            {
                // ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择考核模板！')", true);
                Response.Write("<script>alert('请选择考核模板！')</script>");
                result = false;
            }
            if (ddl_Person.SelectedValue == "00")
            {
                //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择被考核人！')", true);
                Response.Write("<script>alert('请选择被考核人！')</script>");
                result = false;
            }

            if (txtKhNianYue.Text == "")
            {
                //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择主管经理！')", true);
                Response.Write("<script>alert('请选择考核年月！')</script>");
                result = false;
            }

            if (ddl_Person.SelectedValue == firstid.Value || ddl_Person.SelectedValue == secondid.Value || ddl_Person.SelectedValue == thirdid.Value)
            {
                Response.Write("<script>alert('考核人不能与评审人相同！')</script>");
                result = false;
            }

            //string datecheck = DateTime.Now.ToString("yyyy-MM").Trim() + "-10";
            //string datetimenow = DateTime.Now.ToString("yyyy-MM-dd").Trim();
            //string datetxt = txtTime.Text.Trim();
            //if ((string.Compare(datetimenow, datecheck))>0 || (string.Compare(datetxt,datecheck))>0)
            //{
            //    Response.Write("<script>alert('考核时间应在本月10号以前提交！')</script>");
            //    result = false;
            //}


            if (hidState.Value == "0" || hidState.Value == "")
            {
                if (lblBL.Text.Split('|')[0] != "0")
                {
                    if (firstid.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择主管考核人！')", true);
                        Response.Write("<script>alert('请选择一级考核人！')</script>");
                        result = false;
                    }
                }
                else if (lblBL.Text.Split('|')[1] != "0")
                {
                    if (secondid.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择部门负责人！')", true);
                        Response.Write("<script>alert('请选择二级考核人！')</script>");
                        result = false;
                    }
                }
                else if (lblBL.Text.Split('|')[2] != "0")
                {
                    if (thirdid.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择部门负责人！')", true);
                        Response.Write("<script>alert('请选择三级考核人！')</script>");
                        result = false;
                    }
                }
                else
                {
                    if (fifthId.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择部门负责人！')", true);
                        Response.Write("<script>alert('请选择四级考核人！')</script>");
                        result = false;
                    }
                }

                //审核部分
                if (rblSHJS.SelectedValue.ToString().Trim() == "1")
                {
                    if (txtshname1.Text.Trim() == "" || txtshid1.Value.Trim() == "")
                    {
                        Response.Write("<script>alert('请选择一级审核人！')</script>");
                        result = false;
                    }
                }
                else if (rblSHJS.SelectedValue.ToString().Trim() == "2")
                {
                    if (txtshname1.Text.Trim() == "" || txtshid1.Value.Trim() == "")
                    {
                        Response.Write("<script>alert('请选择一级审核人！')</script>");
                        result = false;
                    }
                    if (txtshname2.Text.Trim() == "" || txtshid2.Value.Trim() == "")
                    {
                        Response.Write("<script>alert('请选择二级审核人！')</script>");
                        result = false;
                    }
                }
                else if (rblSHJS.SelectedValue.ToString().Trim() == "3")
                {
                    if (txtshname1.Text.Trim() == "" || txtshid1.Value.Trim() == "")
                    {
                        Response.Write("<script>alert('请选择一级审核人！')</script>");
                        result = false;
                    }
                    if (txtshname2.Text.Trim() == "" || txtshid2.Value.Trim() == "")
                    {
                        Response.Write("<script>alert('请选择二级审核人！')</script>");
                        result = false;
                    }
                    if (txtshname3.Text.Trim() == "" || txtshid3.Value.Trim() == "")
                    {
                        Response.Write("<script>alert('请选择三级审核人！')</script>");
                        result = false;
                    }
                }
                else if (rblSHJS.SelectedValue.ToString().Trim() == "4")
                {
                    if (txtshname1.Text.Trim() == "" || txtshid1.Value.Trim() == "")
                    {
                        Response.Write("<script>alert('请选择一级审核人！')</script>");
                        result = false;
                    }
                    if (txtshname2.Text.Trim() == "" || txtshid2.Value.Trim() == "")
                    {
                        Response.Write("<script>alert('请选择二级审核人！')</script>");
                        result = false;
                    }
                    if (txtshname3.Text.Trim() == "" || txtshid3.Value.Trim() == "")
                    {
                        Response.Write("<script>alert('请选择三级审核人！')</script>");
                        result = false;
                    }
                    if (txtshname4.Text.Trim() == "" || txtshid4.Value.Trim() == "")
                    {
                        Response.Write("<script>alert('请选择四级审核人！')</script>");
                        result = false;
                    }
                }
                //审核部分end
            }
            else if (hidState.Value == "2")
            {
                if (lblBL.Text.Split('|')[1] != "0")
                {
                    if (secondid.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择部门负责人！')", true);
                        Response.Write("<script>alert('请选择二级考核人！')</script>");
                        result = false;
                    }

                }
                else if (lblBL.Text.Split('|')[2] != "0")
                {
                    if (thirdid.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择部门负责人！')", true);
                        Response.Write("<script>alert('请选择三级考核人！')</script>");
                        result = false;
                    }
                }
                else if (lblBL.Text.Split('|')[3] != "0")
                {
                    if (fifthId.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择部门负责人！')", true);
                        Response.Write("<script>alert('请选择四级考核人！')</script>");
                        result = false;
                    }
                }
            }
            else if (hidState.Value == "3")
            {
                if (lblBL.Text.Split('|')[2] != "0")
                {
                    if (thirdid.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择主管经理！')", true);
                        Response.Write("<script>alert('请选择三级考核人！')</script>");
                        result = false;
                    }

                }
                else if (lblBL.Text.Split('|')[3] != "0")
                {
                    if (fifthId.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择主管经理！')", true);
                        Response.Write("<script>alert('请选择四级考核人！')</script>");
                        result = false;
                    }
                }
            }
            else if (hidState.Value == "4")
            {
                if (lblBL.Text.Split('|')[3] != "0")
                {
                    if (fifthId.Value == "")
                    {
                        //  ScriptManager.RegisterStartupScript(this.Page, GetType(), "1", "alert('请选择主管经理！')", true);
                        Response.Write("<script>alert('请选择四级考核人！')</script>");
                        result = false;
                    }

                }
            }
            return result;
        }

        //审批
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            string sqlDate = "select * from TBDS_KaoHeDate where kh_Type='" + ddlType.SelectedValue + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlDate);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["kh_Start"].ToString() != "" && dt.Rows[0]["kh_End"].ToString() != "")
                {
                    try
                    {
                        string now = DateTime.Now.ToString("yyyy-MM-dd");
                        DateTime nowDate = DateTime.Parse(now);
                        DateTime start = DateTime.Parse(dt.Rows[0]["kh_Start"].ToString());
                        DateTime end = DateTime.Parse(dt.Rows[0]["kh_End"].ToString());
                        if (DateTime.Compare(nowDate, start) < 0 || DateTime.Compare(nowDate, end) > 0)
                        {
                            Response.Write("<script>alert('当前时间不在允许的考核时间内，提交被终止！')</script>");
                            return;
                        }
                    }
                    catch (Exception)
                    {

                        Response.Write("<script>alert('允许考核时间设置有误，请联系管理员！')</script>");
                        return;
                    }

                }
            }


            if (Checked())
            {


                string sql = "";
                if (hidAction.Value == "add" || hidAction.Value == "edit")
                {
                    if (ddl_zipstate.SelectedValue.ToString().Trim() == "1" && rblSHJS.SelectedValue.ToString().Trim() != "0")
                    {
                        sql = "update TBDS_KaoHeList set Kh_state='2',Kh_shtoltalstate='1' where kh_Context='" + hidConext.Value + "'";
                    }
                    else if (ddl_zipstate.SelectedValue.ToString().Trim() == "0" && rblSHJS.SelectedValue.ToString().Trim() != "0")
                    {
                        sql = "update TBDS_KaoHeList set Kh_state='1',Kh_shtoltalstate='1' where kh_Context='" + hidConext.Value + "'";
                    }
                    else if (ddl_zipstate.SelectedValue.ToString().Trim() == "1" && rblSHJS.SelectedValue.ToString().Trim() == "0")
                    {
                        sql = "update TBDS_KaoHeList set Kh_state='2',Kh_shtoltalstate='2' where kh_Context='" + hidConext.Value + "'";
                    }
                    else if (ddl_zipstate.SelectedValue.ToString().Trim() == "0" && rblSHJS.SelectedValue.ToString().Trim() == "0")
                    {
                        sql = "update TBDS_KaoHeList set Kh_state='1',Kh_shtoltalstate='2' where kh_Context='" + hidConext.Value + "'";
                    }
                    else
                    {
                        sql = "update TBDS_KaoHeList set Kh_state='1',Kh_shtoltalstate='1' where kh_Context='" + hidConext.Value + "'";
                    }
                    DBCallCommon.ExeSqlText(sql);

                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sprid = firstid.Value.Trim();
                    sptitle = "考核人评价项审批";
                    spcontent = ddl_Person.SelectedItem.Text.Trim() + "的" + txtKhNianYue.Text.Trim() + lbtitle.Text.Trim() + "需要您审批，请登录查看！";
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    Response.Write("<script>alert('保存成功！');window.location.href='OM_KaoHeAudit.aspx';</script>");

                }
                else if (action == "audit")
                {
                    int fk = CommonFun.ComTryInt(ddlFankui.SelectedValue);

                    string[] bl = lblBL.Text.Split('|');
                    int bl1 = bl[0] == "0" ? 1 : 0;
                    int bl2 = bl[1] == "0" ? 1 : 0;
                    int bl3 = bl[2] == "0" ? 1 : 0;
                    int bl4 = bl[3] == "0" ? 1 : 0;

                    #region 判断评分填写完整
                    if (Session["UserID"].ToString() == fourthId.Value.ToString())
                        for (int k = 0; k < Det_Repeater.Items.Count; k++)
                        {
                            TextBox txtvalid = (TextBox)Det_Repeater.Items[k].FindControl("kh_ScoreOwn");
                            if (string.IsNullOrEmpty(txtvalid.Text.ToString()))
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写完整自评分！');", true);
                                return;
                            }
                        }
                    else if (Session["UserID"].ToString() == firstid.Value.ToString())
                        for (int k = 0; k < Det_Repeater.Items.Count; k++)
                        {
                            TextBox txtvalid = (TextBox)Det_Repeater.Items[k].FindControl("kh_Score0");
                            TextBox txtOpinion = (TextBox)Panel1.FindControl("first_opinion");
                            if (string.IsNullOrEmpty(txtvalid.Text.ToString()) || string.IsNullOrEmpty(txtOpinion.Text.ToString()))
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写完整一级考核评分！');", true);
                                return;
                            }
                            
                        }

                    else if (Session["UserID"].ToString() == secondid.Value.ToString())
                        for (int k = 0; k < Det_Repeater.Items.Count; k++)
                        {
                            TextBox txtvalid = (TextBox)Det_Repeater.Items[k].FindControl("kh_Score1");
                            TextBox txtOpinion = (TextBox)Panel1.FindControl("second_opinion");
                            if (string.IsNullOrEmpty(txtvalid.Text.ToString()) || string.IsNullOrEmpty(txtOpinion.Text.ToString()))
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写完整二级考核评分！');", true);
                                return;
                            }
                        }
                    else if (Session["UserID"].ToString() == thirdid.Value.ToString())
                        for (int k = 0; k < Det_Repeater.Items.Count; k++)
                        {
                            TextBox txtvalid = (TextBox)Det_Repeater.Items[k].FindControl("kh_Score2");
                            TextBox txtOpinion = (TextBox)Panel1.FindControl("third_opinion");
                            if (string.IsNullOrEmpty(txtvalid.Text.ToString()) || string.IsNullOrEmpty(txtOpinion.Text.ToString()))
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写完整三级考核评分！');", true);
                                return;
                            }
                        }
                    else if (Session["UserID"].ToString() == fifthId.Value.ToString())
                        for (int k = 0; k < Det_Repeater.Items.Count; k++)
                        {
                            TextBox txtvalid = (TextBox)Det_Repeater.Items[k].FindControl("kh_Score3");
                            TextBox txtOpinion = (TextBox)Panel1.FindControl("fif_opinion");
                            if (string.IsNullOrEmpty(txtvalid.Text.ToString()) || string.IsNullOrEmpty(txtOpinion.Text.ToString()))
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写完整四级考核评分！');", true);
                                return;
                            }
                        }
                    #endregion

                    int state = 0;
                    List<string> list = new List<string>();

                    foreach (RepeaterItem item in Det_Repeater.Items)
                    {
                        //Id, kh_Id, kh_Score, kh_Time, kh_AddPer, kh_Fkey, Kh_State, Kh_fankui, Kh_SPRA, Kh_SPTIMEA, Kh_SPYJA, Kh_SPRB, Kh_SPTIMEB, Kh_SPYJB, Kh_SPRC, Kh_SPTIMEC, Kh_SPYJC, Kh_FKTIME, Kh_FKYJ, Kh_BL, Kh_Type, Kh_Context, Kh_SPRD, Kh_SPTIMED, Kh_SPYJD
                        sql = string.Format("update TBDS_KaoHeDetail set kh_Note='" + ((ITextControl)item.FindControl("kh_Note")).Text + "',kh_Score1='" + ((ITextControl)item.FindControl("kh_Score0")).Text + "',kh_Score2='" + ((ITextControl)item.FindControl("kh_Score1")).Text + "',kh_Score3='" + ((ITextControl)item.FindControl("kh_Score2")).Text + "',kh_ScoreOwn='" + ((ITextControl)item.FindControl("kh_ScoreOwn")).Text + "',kh_Score4='" + ((ITextControl)item.FindControl("kh_Score3")).Text + "',kh_BEIZHU='" + ((ITextControl)item.FindControl("kh_BEIZHU")).Text + "' where Id='" + ((HtmlInputHidden)item.FindControl("hidId")).Value + "' and kh_Context='" + hidConext.Value + "'");
                        list.Add(sql);
                    }
                    if (hidState.Value == "1" || hidState.Value == "")
                    {
                        // state = bl1 == 0 ? 1 : bl2 == 0 ? 2 : 3;
                        state = bl1 == 0 ? 1 : bl2 == 0 ? 2 : bl3 == 0 ? 3 : 4;

                    }
                    else if (hidState.Value == "2")
                    {
                        state = bl2 == 0 ? 1 : bl3 == 0 ? 2 : bl4 == 0 ? 3 : fk == 0 ? 4 : 5;
                        sql = "update TBDS_KaoHeList set Kh_SPTIMEA='" + DateTime.Now.ToString("yyyy-MM-dd") + "',Kh_SPYJA='" + first_opinion.Text.Trim() + "' where Id='" + hidId.Value + "'";
                        list.Add(sql);

                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sprid = secondid.Value.Trim();
                        sptitle = "考核人评价项";
                        spcontent = ddl_Person.SelectedItem.Text.Trim() + "的" + txtKhNianYue.Text.Trim() + lbtitle.Text.Trim() + "需要您评价，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                    else if (hidState.Value == "3")
                    {
                        state = bl3 == 0 ? 1 : bl4 == 0 ? 2 : fk == 0 ? 3 : 4;
                        sql = "update TBDS_KaoHeList set Kh_SPTIMEB='" + DateTime.Now.ToString("yyyy-MM-dd") + "',Kh_SPYJB='" + second_opinion.Text.Trim() + "' where Id='" + hidId.Value + "'";
                        list.Add(sql);

                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sprid = thirdid.Value.Trim();
                        sptitle = "考核人评价项";
                        spcontent = ddl_Person.SelectedItem.Text.Trim() + "的" + txtKhNianYue.Text.Trim() + lbtitle.Text.Trim() + "需要您评价，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }

                    else if (hidState.Value == "4")
                    {
                        state = bl4 == 0 ? 1 : fk == 0 ? 2 : 3;
                        sql = "update TBDS_KaoHeList set Kh_SPTIMEC='" + DateTime.Now.ToString("yyyy-MM-dd") + "',Kh_SPYJC='" + third_opinion.Text.Trim() + "' where Id='" + hidId.Value + "'";
                        list.Add(sql);

                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sprid = fifthId.Value.Trim();
                        sptitle = "考核人评价项";
                        spcontent = ddl_Person.SelectedItem.Text.Trim() + "的" + txtKhNianYue.Text.Trim() + lbtitle.Text.Trim() + "需要您评价，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }

                    else if (hidState.Value == "5")
                    {
                        state = fk == 0 ? 1 : 2;
                        sql = "update TBDS_KaoHeList set Kh_SPTIMEC='" + DateTime.Now.ToString("yyyy-MM-dd") + "',Kh_SPYJC='" + third_opinion.Text.Trim() + "' where Id='" + hidId.Value + "'";
                        list.Add(sql);

                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sprid = fifthId.Value.Trim();
                        sptitle = "考核人评价项";
                        spcontent = ddl_Person.SelectedItem.Text.Trim() + "的" + txtKhNianYue.Text.Trim() + lbtitle.Text.Trim() + "需要您评价，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                    else if (hidState.Value == "6")
                    {
                        state = 1;
                        sql = "update TBDS_KaoHeList set Kh_FKTIME=" + DateTime.Now.ToString("yyyy-MM-dd") + ",Kh_FKYJ='" + fourth_opinion.Text.Trim() + "' where Id='" + hidId.Value + "'";
                        list.Add(sql);
                    }
                    sql = "update TBDS_KaoHeList set Kh_SPRA='" + firstid.Value + "',Kh_SPRB='" + secondid.Value + "',Kh_SPRC='" + thirdid.Value + "',Kh_SPRD='" + fifthId.Value + "',Kh_state=Kh_state+" + state + ",kh_Score='" + CommonFun.ComTryDouble(txt_Result.Text.Trim()) + "',kh_ScoreOwn='" + txtResult0.Text.Trim() + "',kh_Score0='" + CommonFun.ComTryDouble(txtResult1.Text.Trim()) + "',kh_Score1='" + CommonFun.ComTryDouble(txtResult2.Text.Trim()) + "',kh_Score2='" + CommonFun.ComTryDouble(txtResult3.Text.Trim()) + "',kh_Score3='" + CommonFun.ComTryDouble(txtResult4.Text.Trim()) + "' where Id='" + hidId.Value + "'";
                    list.Add(sql);
                    DBCallCommon.ExecuteTrans(list);
                    //邮件提醒
                    string sqlgetstate = "select Kh_state from TBDS_KaoHeList where Id='" + hidId.Value + "'";
                    System.Data.DataTable dtgetstate = DBCallCommon.GetDTUsingSqlText(sqlgetstate);
                    if (dtgetstate.Rows.Count > 0 && (dtgetstate.Rows[0]["Kh_state"].ToString().Trim() == "6" || dtgetstate.Rows[0]["Kh_state"].ToString().Trim() == "7"))
                    {
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sprid = txtshid1.Value.Trim();
                        sptitle = "考核人评价项审批";
                        spcontent = ddl_Person.SelectedItem.Text.Trim() + "的" + txtKhNianYue.Text.Trim() + lbtitle.Text.Trim() + "需要您审批，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                    Response.Write("<script>alert('保存成功！');window.location.href='OM_KaoHeAudit.aspx';</script>");
                }
            }
        }

        //审核部分
        protected void rblSHJS_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblSHJS.SelectedIndex == 0)
            {
                yjshh.Visible = false;
                ejshh.Visible = false;
                sjshh.Visible = false;
                foursh.Visible = false;
            }
            else if (rblSHJS.SelectedIndex == 1)
            {
                yjshh.Visible = true;
                ejshh.Visible = false;
                sjshh.Visible = false;
                foursh.Visible = false;
            }
            else if (rblSHJS.SelectedIndex == 2)
            {
                yjshh.Visible = true;
                ejshh.Visible = true;
                sjshh.Visible = false;
                foursh.Visible = false;
            }
            else if (rblSHJS.SelectedIndex == 3)
            {
                yjshh.Visible = true;
                ejshh.Visible = true;
                sjshh.Visible = true;
                foursh.Visible = false;
            }
            else
            {
                yjshh.Visible = true;
                ejshh.Visible = true;
                sjshh.Visible = true;
                foursh.Visible = true;
            }
            txtshname1.Text = "";
            txtshid1.Value = "";
            txtshname2.Text = "";
            txtshid2.Value = "";
            txtshname3.Text = "";
            txtshid3.Value = "";
            txtshname4.Text = "";
            txtshid4.Value = "";
        }


        //坑爹的审核
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            string sqlupdate = "";
            string sqlgetshinfo = "select * from TBDS_KaoHeList where Kh_Context='" + id + "'";
            System.Data.DataTable dtgetshinfo = DBCallCommon.GetDTUsingSqlText(sqlgetshinfo);
            if (dtgetshinfo.Rows.Count > 0)
            {
                if (txtshid1.Value.Trim() == Session["UserID"].ToString().Trim())
                {
                    if (rblsh1.SelectedValue == "1")
                    {
                        if (dtgetshinfo.Rows[0]["Kh_shjs"].ToString().Trim() == "1")
                        {
                            sqlupdate = "update TBDS_KaoHeList set Kh_shtoltalstate='2',kh_shstate1='1',Kh_shtime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Kh_shnote1='" + opinionsh1.Text.Trim() + "' where Kh_Context='" + id + "'";
                            DBCallCommon.ExeSqlText(sqlupdate);
                        }
                        else if (dtgetshinfo.Rows[0]["Kh_shjs"].ToString().Trim() == "2")
                        {
                            sqlupdate = "update TBDS_KaoHeList set kh_shstate1='1',Kh_shtime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Kh_shnote1='" + opinionsh1.Text.Trim() + "' where Kh_Context='" + id + "'";
                            DBCallCommon.ExeSqlText(sqlupdate);
                        }
                        else if (dtgetshinfo.Rows[0]["Kh_shjs"].ToString().Trim() == "3")
                        {
                            sqlupdate = "update TBDS_KaoHeList set kh_shstate1='1',Kh_shtime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Kh_shnote1='" + opinionsh1.Text.Trim() + "' where Kh_Context='" + id + "'";
                            DBCallCommon.ExeSqlText(sqlupdate);
                        }
                        else if (dtgetshinfo.Rows[0]["Kh_shjs"].ToString().Trim() == "4")
                        {
                            sqlupdate = "update TBDS_KaoHeList set kh_shstate1='1',Kh_shtime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Kh_shnote1='" + opinionsh1.Text.Trim() + "' where Kh_Context='" + id + "'";
                            DBCallCommon.ExeSqlText(sqlupdate);
                        }
                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sprid = txtshid2.Value.Trim();
                        sptitle = "考核人评价项审批";
                        spcontent = ddl_Person.SelectedItem.Text.Trim() + "的" + txtKhNianYue.Text.Trim() + lbtitle.Text.Trim() + "需要您审批，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                    else if (rblsh1.SelectedValue == "2")
                    {
                        sqlupdate = "update TBDS_KaoHeList set Kh_shtoltalstate='3',kh_State='0',kh_shstate1='0',Kh_shtime1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Kh_shnote1='" + opinionsh1.Text.Trim() + "' where Kh_Context='" + id + "'";
                        DBCallCommon.ExeSqlText(sqlupdate);
                    }
                    else
                    {
                        Response.Write("<script>alert('请选择审核意见！')</script>");
                        return;
                    }
                }


                else if (txtshid2.Value.Trim() == Session["UserID"].ToString().Trim())
                {
                    if (rblsh2.SelectedValue == "1")
                    {
                        if (dtgetshinfo.Rows[0]["Kh_shjs"].ToString().Trim() == "2")
                        {
                            sqlupdate = "update TBDS_KaoHeList set Kh_shtoltalstate='2',kh_shstate2='1',Kh_shtime2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Kh_shnote2='" + opinionsh2.Text.Trim() + "' where Kh_Context='" + id + "'";
                            DBCallCommon.ExeSqlText(sqlupdate);
                        }
                        else if (dtgetshinfo.Rows[0]["Kh_shjs"].ToString().Trim() == "3")
                        {
                            sqlupdate = "update TBDS_KaoHeList set kh_shstate2='1',Kh_shtime2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Kh_shnote2='" + opinionsh2.Text.Trim() + "' where Kh_Context='" + id + "'";
                            DBCallCommon.ExeSqlText(sqlupdate);
                        }
                        else if (dtgetshinfo.Rows[0]["Kh_shjs"].ToString().Trim() == "4")
                        {
                            sqlupdate = "update TBDS_KaoHeList set kh_shstate2='1',Kh_shtime2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Kh_shnote2='" + opinionsh2.Text.Trim() + "' where Kh_Context='" + id + "'";
                            DBCallCommon.ExeSqlText(sqlupdate);
                        }
                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sprid = txtshid3.Value.Trim();
                        sptitle = "考核人评价项审批";
                        spcontent = ddl_Person.SelectedItem.Text.Trim() + "的" + txtKhNianYue.Text.Trim() + lbtitle.Text.Trim() + "需要您审批，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                    else if (rblsh2.SelectedValue == "2")
                    {
                        sqlupdate = "update TBDS_KaoHeList set Kh_shtoltalstate='3',kh_State='0',kh_shstate1='0',kh_shstate2='0',Kh_shtime2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Kh_shnote2='" + opinionsh2.Text.Trim() + "' where Kh_Context='" + id + "'";
                        DBCallCommon.ExeSqlText(sqlupdate);
                    }
                    else
                    {
                        Response.Write("<script>alert('请选择审核意见！')</script>");
                        return;
                    }
                }


                else if (txtshid3.Value.Trim() == Session["UserID"].ToString().Trim())
                {
                    if (rblsh3.SelectedValue == "1")
                    {
                        if (dtgetshinfo.Rows[0]["Kh_shjs"].ToString().Trim() == "3")
                        {
                            sqlupdate = "update TBDS_KaoHeList set Kh_shtoltalstate='2',kh_shstate3='1',Kh_shtime3='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Kh_shnote3='" + opinionsh3.Text.Trim() + "' where Kh_Context='" + id + "'";
                            DBCallCommon.ExeSqlText(sqlupdate);
                        }
                        else if (dtgetshinfo.Rows[0]["Kh_shjs"].ToString().Trim() == "4")
                        {
                            sqlupdate = "update TBDS_KaoHeList set kh_shstate3='1',Kh_shtime3='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Kh_shnote3='" + opinionsh3.Text.Trim() + "' where Kh_Context='" + id + "'";
                            DBCallCommon.ExeSqlText(sqlupdate);
                        }
                        //邮件提醒
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sprid = txtshid4.Value.Trim();
                        sptitle = "考核人评价项审批";
                        spcontent = ddl_Person.SelectedItem.Text.Trim() + "的" + txtKhNianYue.Text.Trim() + lbtitle.Text.Trim() + "需要您审批，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                    else if (rblsh3.SelectedValue == "2")
                    {
                        sqlupdate = "update TBDS_KaoHeList set Kh_shtoltalstate='3',kh_State='0',kh_shstate1='0',kh_shstate2='0',kh_shstate3='0',Kh_shtime3='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Kh_shnote3='" + opinionsh3.Text.Trim() + "' where Kh_Context='" + id + "'";
                        DBCallCommon.ExeSqlText(sqlupdate);
                    }
                    else
                    {
                        Response.Write("<script>alert('请选择审核意见！')</script>");
                        return;
                    }
                }
                else if (txtshid4.Value.Trim() == Session["UserID"].ToString().Trim())
                {
                    if (rblsh4.SelectedValue == "1")
                    {
                        sqlupdate = "update TBDS_KaoHeList set Kh_shtoltalstate='2',kh_shstate4='1',Kh_shtime4='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Kh_shnote4='" + opinionsh4.Text.Trim() + "' where Kh_Context='" + id + "'";
                        DBCallCommon.ExeSqlText(sqlupdate);
                    }
                    else if (rblsh4.SelectedValue == "2")
                    {
                        sqlupdate = "update TBDS_KaoHeList set Kh_shtoltalstate='3',kh_State='0',kh_shstate1='0',kh_shstate2='0',kh_shstate3='0，'kh_shstate4='0',Kh_shtime4='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Kh_shnote4='" + opinionsh4.Text.Trim() + "' where Kh_Context='" + id + "'";
                        DBCallCommon.ExeSqlText(sqlupdate);
                    }
                    else
                    {
                        Response.Write("<script>alert('请选择审核意见！')</script>");
                        return;
                    }
                }
            }
            btnSave.Visible = false;
            btnbacklast.Visible = false;
            ContralEnable();
        }


        //初始化
        protected void btninit_Click(object sender, EventArgs e)
        {
            string sqlinit = "update TBDS_KaoHeList set Kh_State='0',Kh_shtoltalstate='0',kh_shstate1='0',kh_shstate2='0',kh_shstate3='0',kh_shstate4='0' where Kh_Context='" + id + "'";
            DBCallCommon.ExeSqlText(sqlinit);
            btninit.Visible = false;
            ContralEnable();
        }




        //驳回到上一级
        protected void btnbacklast_OnClick(object sender, EventArgs e)
        {
            string sqlupdate = "";
            string sqlgetshinfo = "select * from TBDS_KaoHeList where Kh_Context='" + id + "'";
            System.Data.DataTable dtgetshinfo = DBCallCommon.GetDTUsingSqlText(sqlgetshinfo);
            if (dtgetshinfo.Rows.Count > 0)
            {
                if (txtshid1.Value.Trim() == Session["UserID"].ToString().Trim())
                {
                    if (rblsh1.SelectedValue == "1")
                    {
                        Response.Write("<script>alert('该操作仅用于将数据驳回到前一级修改！')</script>");
                        return;
                    }
                    else if (rblsh1.SelectedValue == "2")
                    {
                        sqlupdate = "update TBDS_KaoHeList set Kh_shtoltalstate='1' where Kh_Context='" + id + "'";
                        DBCallCommon.ExeSqlText(sqlupdate);
                    }
                    else
                    {
                        Response.Write("<script>alert('请选择审核意见！')</script>");
                        return;
                    }
                }


                else if (txtshid2.Value.Trim() == Session["UserID"].ToString().Trim())
                {
                    if (rblsh2.SelectedValue == "1")
                    {
                        Response.Write("<script>alert('该操作仅用于将数据驳回到前一级修改！')</script>");
                        return;
                    }
                    else if (rblsh2.SelectedValue == "2")
                    {
                        sqlupdate = "update TBDS_KaoHeList set Kh_shtoltalstate='1',kh_shstate1='0',Kh_shtime2='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Kh_shnote2='" + "已驳回，"+opinionsh2.Text.Trim() + "' where Kh_Context='" + id + "'";
                        DBCallCommon.ExeSqlText(sqlupdate);
                    }
                    else
                    {
                        Response.Write("<script>alert('请选择审核意见！')</script>");
                        return;
                    }
                }


                else if (txtshid3.Value.Trim() == Session["UserID"].ToString().Trim())
                {
                    if (rblsh3.SelectedValue == "1")
                    {
                        Response.Write("<script>alert('该操作仅用于将数据驳回到前一级修改！')</script>");
                        return;
                    }
                    else if (rblsh3.SelectedValue == "2")
                    {
                        sqlupdate = "update TBDS_KaoHeList set Kh_shtoltalstate='1',kh_shstate2='0',Kh_shtime3='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Kh_shnote3='" + "已驳回，" + opinionsh3.Text.Trim() + "' where Kh_Context='" + id + "'";
                        DBCallCommon.ExeSqlText(sqlupdate);
                    }
                    else
                    {
                        Response.Write("<script>alert('请选择审核意见！')</script>");
                        return;
                    }
                }
                else if (txtshid4.Value.Trim() == Session["UserID"].ToString().Trim())
                {
                    if (rblsh4.SelectedValue == "1")
                    {
                        Response.Write("<script>alert('该操作仅用于将数据驳回到前一级修改！')</script>");
                        return;
                    }
                    else if (rblsh4.SelectedValue == "2")
                    {
                        sqlupdate = "update TBDS_KaoHeList set Kh_shtoltalstate='1',kh_shstate3='0',Kh_shtime4='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',Kh_shnote4='" + "已驳回，" + opinionsh4.Text.Trim() + "' where Kh_Context='" + id + "'";
                        DBCallCommon.ExeSqlText(sqlupdate);
                    }
                    else
                    {
                        Response.Write("<script>alert('请选择审核意见！')</script>");
                        return;
                    }
                }
            }
            //2017.1.4修改
            Response.Write("<script>alert('驳回上一级成功！');window.location.href='OM_KaoHeAudit.aspx';</script>");
            //btnbacklast.Visible = false;
            //btnSave.Visible = false;
            //ContralEnable();
        }
    }
}
