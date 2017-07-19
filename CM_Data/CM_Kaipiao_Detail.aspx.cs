using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_Kaipiao_Detail : System.Web.UI.Page
    {
        string id = string.Empty;//全局变量id
        string action = string.Empty;
        string sqlText = "";
        List<string> str = new List<string>();
        List<string> strEngId = new List<string>();
        Table t = new Table();
        Dictionary<string, string> reviewer = new Dictionary<string, string>();//用于存储审核的名单
        int rowsum = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
                id = Request.QueryString["id"].ToString();//识别号
            if (Request.QueryString["action"] != null)
            {
                action = Request.QueryString["action"].ToString();//操作
                hidAction.Value = action;
            }
            Get_Shr();
            if (!IsPostBack)
            {
                if (action == "add")
                {
                    btnTicket.Visible = false;
                    btnShenH.Visible = false;
                    hidTaskId.Value = DateTime.Now.ToString("yyyyMMddHHmmssff");
                    KP_ZDRNM.Text = Session["UserName"].ToString();
                    KP_ZDRID.Value = Session["UserID"].ToString();
                    KP_ZDTIME.Text = DateTime.Now.ToString("yyyy-MM-dd");

                    #region 默认审核人
                    txt_fourth.Text = "刘慧颖";
                    fourthid.Value = "206";
                    txt_fifth.Text = "宋倩娜";
                    fifthid.Value = "77";
                    txt_sixth.Text = "王昌盛";
                    sixthid.Value = "103";
                    txt_seventh.Text = "常颖";
                    seventhId.Value = "257";

                    txt_eighth.Text = "张鹏辉";
                    eighthId.Value = "85";
                    txt_third.Text = "李秋英";
                    thirdid.Value = "171";

                    #endregion
                }
                else if (action == "edit")
                {
                    btnTicket.Visible = false;
                    btnShenH.Visible = false;
                    BindInfo();
                    BindDetail();
                }
                else if (action == "audit")
                {
                    btnTicket.Visible = false;
                    btn_Submit.Visible = false;
                    hlSelect1.Visible = false;
                    hlSelect2.Visible = false;
                    hlSelect3.Visible = false;
                    hlSelect4.Visible = false;
                    hlSelect5.Visible = false;
                    hlSelect6.Visible = false;
                    hlSelect7.Visible = false;
                    hlSelect8.Visible = false;
                    BindInfo();
                    BindDetail();
                    ControlEnabled();
                }
                else if (action == "view")
                {
                    btnTicket.Visible = false;
                    hlSelect1.Visible = false;
                    hlSelect2.Visible = false;
                    hlSelect3.Visible = false;
                    hlSelect4.Visible = false;
                    hlSelect5.Visible = false;
                    hlSelect6.Visible = false;
                    hlSelect7.Visible = false;
                    hlSelect8.Visible = false;
                    btn_Submit.Visible = false;
                    btnShenH.Visible = false;
                    BindInfo();
                    BindDetail();
                }
                else if (action == "ticket")
                {
                    hlSelect1.Visible = false;
                    hlSelect2.Visible = false;
                    hlSelect3.Visible = false;
                    hlSelect4.Visible = false;
                    hlSelect5.Visible = false;
                    hlSelect6.Visible = false;
                    hlSelect7.Visible = false;
                    hlSelect8.Visible = false;
                    btn_Submit.Visible = false;
                    btnShenH.Visible = false;
                    BindInfo();
                    BindDetail();
                }
            }
            InitUpload();
        }


        //初始附件上传控件
        private void InitUpload()
        {
            if (action == "audit" || action == "edit" || action == "add" || action == "ticket" || action == "view")
            {
                UploadAttachments1.Visible = true;
                UploadAttachments1.uf_code = hidTaskId.Value;
                UploadAttachments1.uf_type = 1;
            }
            else
            {
                UploadAttachments1.Visible = false;

            }
            //******************************
            //2017.1.10修改
            if (action == "add" || action == "edit")
            {
                UploadAttachments1.uf_upload_del = true;
            }
            if (action == "view" || action == "audit" || action == "ticket")
            {
                UploadAttachments1.uf_upload_del = false;
            }
        }

        private void ControlEnabled()
        {
            if (hidHSState.Value == "1")
            {
                if (thirdid.Value == Session["UserID"].ToString())
                {
                    rbl_third.Enabled = true;
                    third_opinion.ReadOnly = false;
                }
                if (fourthid.Value == Session["UserID"].ToString())
                {
                    rbl_fourth.Enabled = true;
                    fourth_opinion.ReadOnly = false;
                }
                if (fifthid.Value == Session["UserID"].ToString())
                {
                    rbl_fifth.Enabled = true;
                    fifth_opinion.ReadOnly = false;
                }
                if (sixthid.Value == Session["UserID"].ToString())
                {
                    rbl_sixth.Enabled = true;
                    sixth_opinion.ReadOnly = false;
                }
                if (seventhId.Value == Session["UserID"].ToString())
                {
                    rbl_seventh.Enabled = true;
                    seventh_opinion.ReadOnly = false;
                }
            }
            if (hidSPState.Value == "1" && firstid.Value == Session["UserID"].ToString())
            {
                rbl_first.Enabled = true;
                first_opinion.ReadOnly = false;
            }
            else if (hidSPState.Value == "2" && secondid.Value == Session["UserID"].ToString())
            {
                rbl_second.Enabled = true;
                second_opinion.ReadOnly = false;
            }
        }

        private void BindInfo()
        {
            sqlText = "select * from CM_KAIPIAO where Id='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                hidId.Value = id;
                hidTaskId.Value = row["KP_TaskID"].ToString();
                KP_CODE.Text = row["KP_CODE"].ToString();
                KP_ZDRNM.Text = row["KP_ZDRNM"].ToString();
                KP_COM.Text = row["KP_COM"].ToString();
                KP_ADDRESS.Text = row["KP_ADDRESS"].ToString();
                KP_SHEBEI.Text = row["KP_SHEBEI"].ToString();
                KP_ACCOUNT.Text = row["KP_ACCOUNT"].ToString();
                KP_SHUIHAO.Text = row["KP_SHUIHAO"].ToString();
                KP_BANK.Text = row["KP_BANK"].ToString();
                KP_TEL.Text = row["KP_TEL"].ToString();
                KP_CONZONGJIA.Text = row["KP_CONZONGJIA"].ToString();
                KP_DAOKUANJE.Text = row["KP_DAOKUANJE"].ToString();
                KP_CONDITION.Text = row["KP_CONDITION"].ToString();
                KP_YIKAIJE.Text = row["KP_YIKAIJE"].ToString();
                KP_BENCIJE.Text = row["KP_BENCIJE"].ToString();
                KP_JIAOFUFS.Text = row["KP_JIAOFUFS"].ToString();
                KP_TIQIANKP.SelectedValue = row["KP_TIQIANKP"].ToString();
                KP_ZDTIME.Text = row["KP_ZDTIME"].ToString();
                hidHSState.Value = row["KP_HSSTATE"].ToString();
                hidSPState.Value = row["KP_SPSTATE"].ToString();
                KP_KPNUMBER.Text = row["KP_KPNUMBER"].ToString();
                KP_NOTE.Text = row["KP_NOTE"].ToString();

                //审核信息
                txt_first.Text = row["KP_SHRNMA"].ToString();
                firstid.Value = row["KP_SHRIDA"].ToString();
                rbl_first.SelectedValue = row["KP_RESULTA"].ToString();
                first_time.Text = row["KP_SHTIMEA"].ToString();
                first_opinion.Text = row["KP_NOTEA"].ToString();
                txt_second.Text = row["KP_SHRNMB"].ToString();
                secondid.Value = row["KP_SHRIDB"].ToString();
                rbl_second.SelectedValue = row["KP_RESULTB"].ToString();
                second_opinion.Text = row["KP_NOTEB"].ToString();
                second_time.Text = row["KP_SHTIMEB"].ToString();
                sqlText = "select * from CM_KAIPIAO_HUISHEN where cId='" + hidTaskId.Value + "' order by Id";
                DataTable dtHS = DBCallCommon.GetDTUsingSqlText(sqlText);
                if (dtHS.Rows.Count == 6)
                {
                    txt_third.Text = dtHS.Rows[0]["sprName"].ToString();
                    thirdid.Value = dtHS.Rows[0]["sprId"].ToString();
                    rbl_third.SelectedValue = dtHS.Rows[0]["Result"].ToString();
                    third_opinion.Text = dtHS.Rows[0]["Note"].ToString();
                    third_time.Text = dtHS.Rows[0]["Time"].ToString();
                    hidNum3.Value = dtHS.Rows[0]["Id"].ToString();

                    txt_fourth.Text = dtHS.Rows[1]["sprName"].ToString();
                    fourthid.Value = dtHS.Rows[1]["sprId"].ToString();
                    rbl_fourth.SelectedValue = dtHS.Rows[1]["Result"].ToString();
                    fourth_opinion.Text = dtHS.Rows[1]["Note"].ToString();
                    fourth_time.Text = dtHS.Rows[1]["Time"].ToString();
                    hidNum4.Value = dtHS.Rows[1]["Id"].ToString();

                    txt_fifth.Text = dtHS.Rows[2]["sprName"].ToString();
                    fifthid.Value = dtHS.Rows[2]["sprId"].ToString();
                    rbl_fifth.SelectedValue = dtHS.Rows[2]["Result"].ToString();
                    fifth_opinion.Text = dtHS.Rows[2]["Note"].ToString();
                    fifth_time.Text = dtHS.Rows[2]["Time"].ToString();
                    hidNum5.Value = dtHS.Rows[2]["Id"].ToString();

                    txt_sixth.Text = dtHS.Rows[3]["sprName"].ToString();
                    sixthid.Value = dtHS.Rows[3]["sprId"].ToString();
                    rbl_sixth.SelectedValue = dtHS.Rows[3]["Result"].ToString();
                    sixth_opinion.Text = dtHS.Rows[3]["Note"].ToString();
                    sixth_time.Text = dtHS.Rows[3]["Time"].ToString();
                    hidNum6.Value = dtHS.Rows[3]["Id"].ToString();

                    txt_seventh.Text = dtHS.Rows[4]["sprName"].ToString();
                    seventhId.Value = dtHS.Rows[4]["sprId"].ToString();
                    rbl_seventh.SelectedValue = dtHS.Rows[4]["Result"].ToString();
                    seventh_opinion.Text = dtHS.Rows[4]["Note"].ToString();
                    seventh_time.Text = dtHS.Rows[4]["Time"].ToString();
                    hidNum7.Value = dtHS.Rows[4]["Id"].ToString();



                    txt_eighth.Text = dtHS.Rows[5]["sprName"].ToString();
                    eighthId.Value = dtHS.Rows[5]["sprId"].ToString();
                    rbl_eighth.SelectedValue = dtHS.Rows[5]["Result"].ToString();
                    eighth_opinion.Text = dtHS.Rows[5]["Note"].ToString();
                    eighth_time.Text = dtHS.Rows[5]["Time"].ToString();
                    hidNum8.Value = dtHS.Rows[5]["Id"].ToString();
                }
            }
        }

        private void BindDetail()
        {
            sqlText = "select * from CM_KAIPIAO_DETAIL where cId='" + hidTaskId.Value + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            decimal tot_money = 0;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tot_money += CommonFun.ComTryDecimal(dt.Rows[i]["kpmoney"].ToString());
                }
                lb_select_money.Text = tot_money.ToString();
            }
        }

        protected void btn_shr_Click(object sender, EventArgs e)
        {
            string i = ((Button)sender).ID.Substring(7);

            CheckBoxList ck = (CheckBoxList)Pan_ShenHe.FindControl("cki" + i);
            CheckBoxList ck1 = (CheckBoxList)Pan_HuiShen.FindControl("cki" + i);
            if (ck != null)
            {
                for (int j = 0; j < ck.Items.Count; j++)
                {
                    if (ck.Items[j].Selected == true)
                    {
                        if (i == "1")
                        {
                            firstid.Value = ck.Items[j].Value.ToString();
                            txt_first.Text = ck.Items[j].Text.ToString();
                        }
                        if (i == "2")
                        {
                            secondid.Value = ck.Items[j].Value.ToString();
                            txt_second.Text = ck.Items[j].Text.ToString();
                        }

                        return;
                    }
                }
                if (i == "1")
                {
                    firstid.Value = "";
                    txt_first.Text = "";
                }
                if (i == "2")
                {
                    secondid.Value = "";
                    txt_second.Text = "";
                }

            }
            if (ck1 != null)
            {
                for (int j = 0; j < ck1.Items.Count; j++)
                {
                    if (ck1.Items[j].Selected == true)
                    {

                        if (i == "3")
                        {
                            thirdid.Value = ck1.Items[j].Value.ToString();
                            txt_third.Text = ck1.Items[j].Text.ToString();
                        }
                        if (i == "4")
                        {
                            fourthid.Value = ck1.Items[j].Value.ToString();
                            txt_fourth.Text = ck1.Items[j].Text.ToString();
                        }
                        if (i == "5")
                        {
                            fifthid.Value = ck1.Items[j].Value.ToString();
                            txt_fifth.Text = ck1.Items[j].Text.ToString();
                        }
                        if (i == "6")
                        {
                            sixthid.Value = ck1.Items[j].Value.ToString();
                            txt_sixth.Text = ck1.Items[j].Text.ToString();
                        }
                        if (i == "7")
                        {
                            seventhId.Value = ck1.Items[j].Value.ToString();
                            txt_seventh.Text = ck1.Items[j].Text.ToString();
                        }
                        if (i == "8")
                        {
                            eighthId.Value = ck1.Items[j].Value.ToString();
                            txt_eighth.Text = ck1.Items[j].Text.ToString();
                        }
                        return;
                    }
                    if (i == "3")
                    {
                        thirdid.Value = "";
                        txt_third.Text = "";
                    } if (i == "4")
                    {
                        fourthid.Value = "";
                        txt_fourth.Text = "";

                    } if (i == "5")
                    {
                        fifthid.Value = "";
                        txt_fifth.Text = "";
                    }
                    if (i == "6")
                    {
                        sixthid.Value = "";
                        txt_sixth.Text = "";
                    }
                    if (i == "7")
                    {
                        seventhId.Value = "";
                        txt_seventh.Text = "";
                    }
                    if (i == "8")
                    {
                        eighthId.Value = "";
                        txt_eighth.Text = "";
                    }
                }
            }
        }

        private void Get_Shr()
        {
            //审核人1
            pal_select1_inner.Controls.Add(ShrTable("1"));
            // 审核人2
            pal_select2_inner.Controls.Add(ShrTable("2"));
            //审核人3
            pal_select3_inner.Controls.Add(ShrTable("3"));
            // 审核人4
            pal_select4_inner.Controls.Add(ShrTable("4"));
            //审核人5
            pal_select5_inner.Controls.Add(ShrTable("5"));
            // 审核人6
            pal_select6_inner.Controls.Add(ShrTable("6"));
            //审核人7
            pal_select7_inner.Controls.Add(ShrTable("7"));
            //审核人8
            pal_select8_inner.Controls.Add(ShrTable("8"));
        }

        private Table ShrTable(string i)
        {
            string sql = "";
            if (i == "1")
            {
                sql = "select st_ID,st_name from TBDS_STAFFINFO where st_depid='07' and ST_PD='0'";
            }
            else if (i == "2")
            {
                sql = "select st_ID,st_name from TBDS_STAFFINFO where st_depid='01' and ST_PD='0'";
            }
            else if (i == "3")
            {
                sql = "select st_ID,st_name from TBDS_STAFFINFO where st_depid='06' and ST_PD='0'";
            }
            else if (i == "4")
            {
                sql = "select st_ID,st_name from TBDS_STAFFINFO where st_depid='05' and ST_PD='0'";
            }
            else if (i == "5" || i == "6" || i == "7" || i == "8")
            {
                sql = "select st_ID,st_name from TBDS_STAFFINFO where st_depid='04' and ST_PD='0'";
            }

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            Table tctrl = new Table();
            if (dt.Rows.Count != 0)
            {
                TableRow tr = new TableRow();
                TableCell td = new TableCell();

                CheckBoxList cki = new CheckBoxList();
                cki.ID = "cki" + i;
                cki.DataSource = dt;
                cki.DataTextField = "ST_NAME";//领导的姓名
                cki.DataValueField = "ST_ID";//领导的编号
                cki.DataBind();
                for (int k = 0; k < cki.Items.Count; k++)
                {
                    cki.Items[k].Attributes.Add("Onclick", "CheckBoxList_Click(this)");//使用了javascript使其只能勾选一个
                }
                cki.RepeatColumns = 5;//获取显示的列数
                td.Controls.Add(cki);
                tr.Cells.Add(td);
                tctrl.Controls.Add(tr);
            }
            return tctrl;
        }

        #region 增加行
        protected void btnadd_Click(object sender, EventArgs e)
        {

            if (txtConId.Value != "")
            {
                //CM_CONTR, TSA_ID, CM_PROJ, CM_ENGNAME, CM_MAP, CM_MATERIAL, CM_PRICE, CM_COUNT, CM_NUMBER, CM_UNIT,
                sqlText = "insert into CM_KAIPIAO_DETAIL(cId, TaskId, ConId, Proj, Engname, Map, Number, Unit, Money,kpmoney,dfConId) select '" + hidTaskId.Value + "' as cId,TSA_ID,CM_CONTR,CM_PROJ,CM_ENGNAME, CM_MAP,CM_NUMBER,CM_UNIT,CM_COUNT,CM_COUNT,PCON_YZHTH from TBPM_CONPCHSINFO as a left join TBPM_DETAIL as b on a.PCON_BCODE=b.CM_CONTR where CM_CONTR like '%" + txtConId.Value.Trim() + "%'";
                DBCallCommon.ExeSqlText(sqlText);
                BindDetail();

            }
            else
            {
                Response.Write("<script>alert('请输入合同号！');</script>");//后台验证
            }

        }
        #endregion
        protected void delete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gr.FindControl("chk");
                string Id = ((HiddenField)gr.FindControl("hide")).Value;
                if (chk.Checked && Id != "")//删除勾选且不为任务号节点的项
                {
                    sqlText = "delete from CM_KAIPIAO_DETAIL where Id='" + Id + "'";
                    str.Add(sqlText);
                }
            }
            DBCallCommon.ExecuteTrans(str);
            BindDetail();
        }


        protected void save_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridView1.Rows)
            {

                string Id = ((HiddenField)gr.FindControl("hide")).Value;
                string dfTaskId = ((TextBox)gr.FindControl("dfTaskId")).Text;
                string dfProName = ((TextBox)gr.FindControl("dfProName")).Text;
                string kpmoney = ((TextBox)gr.FindControl("kpMoney")).Text;
                string engname = ((TextBox)gr.FindControl("txtEngname")).Text;
                sqlText = "update CM_KAIPIAO_DETAIL set dfTaskId='" + dfTaskId + "',dfProName='" + dfProName + "',kpmoney='" + kpmoney + "',Engname='" + engname + "'  where Id='" + Id + "'";
                str.Add(sqlText);
            }
            DBCallCommon.ExecuteTrans(str);
            BindDetail();
            Response.Write("<script>alert('保存成功！');</script>");

        }



        #region 保存操作
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            string reg = @"^[0-9]+(\.[0-9]{0,6})?$";
            if (firstid.Value == "" || secondid.Value == "" || thirdid.Value == "" || fourthid.Value == "" || fifthid.Value == "" || sixthid.Value == "" || seventhId.Value == "")
            {
                Response.Write("<script>alert('请选择评审人及会审人！');</script>"); return;//后台验证
            }
            else if (!Regex.IsMatch(KP_CONZONGJIA.Text.Trim(), reg))
            {
                Response.Write("<script>alert('请输入正确格式的合同金额！');</script>"); return;//后台验证
            }

            else
            {
                string code = KP_CODE.Text.Trim();
                string zdrId = KP_ZDRID.Value;
                string zdrNM = KP_ZDRNM.Text.Trim();
                string com = KP_COM.Text.Trim();
                string address = KP_ADDRESS.Text.Trim();
                string shebei = KP_SHEBEI.Text.Trim();
                string account = KP_ACCOUNT.Text.Trim();
                string shuihao = KP_SHUIHAO.Text.Trim();
                string bank = KP_BANK.Text.Trim();
                string tel = KP_TEL.Text.Trim();
                string conZongjia = KP_CONZONGJIA.Text.Trim();
                string daokuanJE = KP_DAOKUANJE.Text.Trim();
                string condition = KP_CONDITION.Text.Trim();
                string yikaiJE = KP_YIKAIJE.Text.Trim();
                string benciJe = KP_BENCIJE.Text.Trim();
                string jiaofuFS = KP_JIAOFUFS.Text.Trim();
                string tiqianKP = KP_TIQIANKP.SelectedValue;
                string zhidanTime = KP_ZDTIME.Text.Trim();
                string firstNM = txt_first.Text.Trim();
                string firstId = firstid.Value;
                string secondNM = txt_second.Text.Trim();
                string secondID = secondid.Value;
                string taskId = hidTaskId.Value;
                string note = KP_NOTE.Text;
                string conId = "";
                if (GridView1.Rows.Count > 0)
                {
                    conId = GridView1.Rows[0].Cells[3].Text.ToString();
                }
                if (hidAction.Value == "add")
                {
                    sqlText = "insert into CM_KAIPIAO(KP_CODE, KP_COM, KP_ADDRESS, KP_SHEBEI, KP_ACCOUNT, KP_SHUIHAO, KP_BANK, KP_TEL, KP_DAOKUANJE, KP_CONDITION, KP_YIKAIJE, KP_BENCIJE, KP_JIAOFUFS, KP_TIQIANKP, KP_ZDRID, KP_ZDRNM, KP_ZDTIME, KP_SHRIDA, KP_SHRNMA, KP_NOTE, KP_SHRIDB, KP_SHRNMB, KP_SPSTATE, KP_CONZONGJIA, KP_TaskID,KP_HSSTATE,KP_ZONGSTATE,KP_CONID) values('" + code + "','" + com + "','" + address + "','" + shebei + "','" + account + "','" + shuihao + "','" + bank + "','" + tel + "','" + daokuanJE + "','" + condition + "','" + yikaiJE + "','" + benciJe + "','" + jiaofuFS + "','" + tiqianKP + "','" + zdrId + "','" + zdrNM + "','" + zhidanTime + "','" + firstId + "','" + firstNM + "','" + note + "','" + secondID + "','" + secondNM + "','0','" + conZongjia + "','" + taskId + "','0','0','" + conId + "')";
                    str.Add(sqlText);
                    sqlText = "insert into CM_KAIPIAO_HUISHEN(cId, sprId, sprName) values ('" + taskId + "','" + thirdid.Value + "','" + txt_third.Text + "')";
                    str.Add(sqlText);
                    sqlText = "insert into CM_KAIPIAO_HUISHEN(cId, sprId, sprName) values ('" + taskId + "','" + fourthid.Value + "','" + txt_fourth.Text + "')";
                    str.Add(sqlText);
                    sqlText = "insert into CM_KAIPIAO_HUISHEN(cId, sprId, sprName) values ('" + taskId + "','" + fifthid.Value + "','" + txt_fifth.Text + "')";
                    str.Add(sqlText);
                    sqlText = "insert into CM_KAIPIAO_HUISHEN(cId, sprId, sprName) values ('" + taskId + "','" + sixthid.Value + "','" + txt_sixth.Text + "')";
                    str.Add(sqlText);
                    sqlText = "insert into CM_KAIPIAO_HUISHEN(cId, sprId, sprName) values ('" + taskId + "','" + seventhId.Value + "','" + txt_seventh.Text + "')";
                    str.Add(sqlText);
                    sqlText = "insert into CM_KAIPIAO_HUISHEN(cId, sprId, sprName) values ('" + taskId + "','" + eighthId.Value + "','" + txt_eighth.Text + "')";
                    str.Add(sqlText);

                }
                else if (hidAction.Value == "edit")
                {
                    sqlText = "update CM_KAIPIAO set KP_CODE='" + code + "', KP_COM='" + com + "', KP_ADDRESS='" + address + "', KP_SHEBEI='" + shebei + "', KP_ACCOUNT='" + account + "', KP_SHUIHAO='" + shuihao + "', KP_BANK='" + bank + "', KP_TEL='" + tel + "', KP_DAOKUANJE='" + daokuanJE + "', KP_CONDITION='" + condition + "', KP_YIKAIJE='" + yikaiJE + "', KP_BENCIJE='" + benciJe + "', KP_JIAOFUFS='" + jiaofuFS + "', KP_TIQIANKP='" + tiqianKP + "', KP_SHRIDA='" + firstId + "', KP_SHRNMA='" + firstNM + "', KP_NOTE='" + note + "', KP_SHRIDB='" + secondID + "', KP_SHRNMB='" + secondNM + "', KP_CONZONGJIA='" + conZongjia + "',KP_CONID='" + conId + "' where Id='" + hidId.Value + "'";
                    str.Add(sqlText);
                    sqlText = "update CM_KAIPIAO_HUISHEN set sprId='" + thirdid.Value + "',sprName='" + txt_third.Text + "' where Id='" + hidNum3.Value + "'";
                    str.Add(sqlText);
                    sqlText = "update CM_KAIPIAO_HUISHEN set sprId='" + fourthid.Value + "',sprName='" + txt_fourth.Text + "' where Id='" + hidNum4.Value + "'";
                    str.Add(sqlText);
                    sqlText = "update CM_KAIPIAO_HUISHEN set sprId='" + fifthid.Value + "',sprName='" + txt_fifth.Text + "' where Id='" + hidNum5.Value + "'";
                    str.Add(sqlText);
                    sqlText = "update CM_KAIPIAO_HUISHEN set sprId='" + sixthid.Value + "',sprName='" + txt_sixth.Text + "' where Id='" + hidNum6.Value + "'";
                    str.Add(sqlText);
                    sqlText = "update CM_KAIPIAO_HUISHEN set sprId='" + seventhId.Value + "',sprName='" + txt_seventh.Text + "' where Id='" + hidNum7.Value + "'";
                    str.Add(sqlText);
                    sqlText = "update CM_KAIPIAO_HUISHEN set sprId='" + eighthId.Value + "',sprName='" + txt_eighth.Text + "' where Id='" + hidNum8.Value + "'";
                    str.Add(sqlText);

                }
                DBCallCommon.ExecuteTrans(str);
                Response.Write("<script>alert('保存成功！');</script>");
                btnShenH.Visible = true;
            }
        }
        #endregion

        #region 审核操作
        protected void btnShenH_Click(object sender, EventArgs e)
        {
            #region 会审审批
            if (hidAction.Value == "add" || hidAction.Value == "edit")
            {
                sqlText = "update CM_KAIPIAO set KP_SPSTATE='1',KP_HSSTATE='1',KP_ZONGSTATE='1' where KP_TaskID='" + hidTaskId.Value + "'";
                DBCallCommon.ExeSqlText(sqlText);

                SendEmailTo(firstid.Value);
                SendEmailTo(secondid.Value);
                SendEmailTo(thirdid.Value);
                SendEmailTo(fourthid.Value);
                SendEmailTo(fifthid.Value);
                SendEmailTo(sixthid.Value);
                SendEmailTo(seventhId.Value);
                SendEmailTo(eighthId.Value);
                Response.Redirect("CM_Kaipiao_List.aspx");
            }
            else if (hidAction.Value == "audit")
            {
                if (hidHSState.Value == "1")
                {
                    if (thirdid.Value == Session["UserID"].ToString())
                    {

                        sqlText = "update CM_KAIPIAO_HUISHEN set Result='" + rbl_third.SelectedValue + "', Note='" + third_opinion.Text + "', Time='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where Id='" + hidNum3.Value + "'";
                        str.Add(sqlText);
                    }
                    if (fourthid.Value == Session["UserID"].ToString())
                    {
                        if (rbl_fourth.SelectedValue == "0")
                        {
                            strEngId = GetTaskIds();
                            foreach (string engId in strEngId)
                            {
                                sqlText = "select PO_PTCODE from TBPC_PURORDERDETAIL where PO_PTCODE like '%" + engId + "%' and PO_PTCODE not in (select WG_PTCODE from View_SM_IN where WG_PTCODE like '%" + engId + "%')";
                                DataTable dtPTCODE = DBCallCommon.GetDTUsingSqlText(sqlText);
                                if (dtPTCODE.Rows.Count > 0)
                                {
                                    Response.Write("<script>alert('该计划号【" + dtPTCODE.Rows[0][0].ToString() + "】未完成入库，无法审批通过！');</script>");
                                    return;
                                }
                                sqlText = "select PTC from View_SM_Storage where PTC like '%" + engId + "%'";
                                dtPTCODE = DBCallCommon.GetDTUsingSqlText(sqlText);
                                if (dtPTCODE.Rows.Count > 0)
                                {
                                    Response.Write("<script>alert('该计划号【" + dtPTCODE.Rows[0][0].ToString() + "】未完成出库，请先将其出库或转移至备库在进行审批！');</script>");
                                    return;
                                }
                            }
                        }
                        sqlText = "update CM_KAIPIAO_HUISHEN set Result='" + rbl_fourth.SelectedValue + "', Note='" + fourth_opinion.Text + "', Time='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where Id='" + hidNum4.Value + "'";
                        str.Add(sqlText);
                    }
                    if (fifthid.Value == Session["UserID"].ToString())
                    {
                        sqlText = "update CM_KAIPIAO_HUISHEN set Result='" + rbl_fifth.SelectedValue + "', Note='" + fifth_opinion.Text + "', Time='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where Id='" + hidNum5.Value + "'";
                        str.Add(sqlText);
                    }
                    if (sixthid.Value == Session["UserID"].ToString())
                    {
                        sqlText = "update CM_KAIPIAO_HUISHEN set Result='" + rbl_sixth.SelectedValue + "', Note='" + sixth_opinion.Text + "', Time='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where Id='" + hidNum6.Value + "'";
                        str.Add(sqlText);
                    }
                    if (seventhId.Value == Session["UserID"].ToString())
                    {
                        strEngId = GetTaskIds();
                        foreach (string engId in strEngId)
                        {
                            sqlText = "select * from  TBPM_STRINFODQO as a left join TBMP_FINISHED_STORE as b on a.BM_ENGID=b.KC_TSA and a.BM_ZONGXU=b.KC_ZONGXU left join View_CM_TSAJOINPROJ as c on a.BM_ENGID=c.TSA_ID where dbo.Splitnum(BM_ZONGXU,'.')=0 and (kc_rknum<bm_number or kc_rknum is null) and BM_ENGID='" + engId + "' ";
                            DataTable dtEng = DBCallCommon.GetDTUsingSqlText(sqlText);
                            if (dtEng.Rows.Count > 0)
                            {
                                Response.Write("<script>alert('该任务号【" + engId + "】未完成入库，无法审批通过！');</script>");
                                return;
                            }
                        }

                        sqlText = "update CM_KAIPIAO_HUISHEN set Result='" + rbl_seventh.SelectedValue + "', Note='" + seventh_opinion.Text + "', Time='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where Id='" + hidNum7.Value + "'";
                        str.Add(sqlText);
                    }

                    if (eighthId.Value == Session["UserID"].ToString())
                    {
                        strEngId = GetTaskIds();
                        foreach (string engId in strEngId)
                        {
                            sqlText = "select * from PM_CPFYJSD JS_RWH='" + engId + "' ";
                            DataTable dtEng = DBCallCommon.GetDTUsingSqlText(sqlText);
                            if (dtEng.Rows.Count == 0)
                            {
                                Response.Write("<script>alert('该任务号【" + engId + "】未完成费用均摊，无法审批通过！');</script>");
                                return;
                            }
                        }

                        sqlText = "update CM_KAIPIAO_HUISHEN set Result='" + rbl_eighth.SelectedValue + "', Note='" + eighth_opinion.Text + "', Time='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where Id='" + hidNum8.Value + "'";
                        str.Add(sqlText);
                    }
                    DBCallCommon.ExecuteTrans(str);
                    sqlText = "select count(1) from CM_KAIPIAO_HUISHEN where Result='0' and cId='" + hidTaskId.Value + "' union all select count(1) from CM_KAIPIAO_HUISHEN where Result='1' and cId='" + hidTaskId.Value + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0][0].ToString() == "6")
                        {
                            //会审通过
                            sqlText = "update CM_KAIPIAO set KP_HSSTATE='2' where Id='" + hidId.Value + "'";
                            DBCallCommon.ExeSqlText(sqlText);
                        }
                        else
                        {
                            if (dt.Rows[1][0].ToString() != "0")
                            {
                                //会审驳回
                                sqlText = "update CM_KAIPIAO set KP_HSSTATE='3',KP_ZONGSTATE='3' where Id='" + hidId.Value + "'";
                                DBCallCommon.ExeSqlText(sqlText);
                            }
                        }
                    }

                }
                else
                {
                    Response.Write("<script>alert('该条状态无法审批！');</script>");
                    return;
                }

            }
            #endregion

            if (hidSPState.Value == "1")
            {
                //0:初始化，1:一级审批，2：一级通过，3：已通过，4：已驳回
                if (rbl_first.SelectedValue == "0")
                {
                    sqlText = "update CM_KAIPIAO set KP_SPSTATE='2',KP_RESULTA='0',KP_NOTEA='" + first_opinion.Text + "',KP_SHTIMEA='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id='" + hidId.Value + "'";
                    DBCallCommon.ExeSqlText(sqlText);
                }
                else if (rbl_first.SelectedValue == "1")
                {
                    sqlText = "update CM_KAIPIAO set KP_ZONGSTATE='3',KP_SPSTATE='4',KP_RESULTA='1',KP_NOTEA='" + first_opinion.Text + "',KP_SHTIMEA='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id='" + hidId.Value + "'";
                    DBCallCommon.ExeSqlText(sqlText);
                }
            }
            else if (hidSPState.Value == "2")
            {
                //0:初始化，1:一级审批，2：一级通过，3：已通过，4：已驳回
                if (rbl_second.SelectedValue == "0")
                {
                    sqlText = "update CM_KAIPIAO set KP_SPSTATE='3',KP_RESULTB='0',KP_NOTEB='" + second_opinion.Text + "',KP_SHTIMEB='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id='" + hidId.Value + "'";
                    DBCallCommon.ExeSqlText(sqlText);
                }
                else if (rbl_second.SelectedValue == "1")
                {
                    sqlText = "update CM_KAIPIAO set KP_ZONGSTATE='3',KP_SPSTATE='4',KP_RESULTB='1',KP_NOTEB='" + second_opinion.Text + "',KP_SHTIMEB='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where Id='" + hidId.Value + "'";
                    DBCallCommon.ExeSqlText(sqlText);
                }
            }

            sqlText = "select count(1) from CM_KAIPIAO where Id='" + hidId.Value + "' and KP_SPSTATE='3' and KP_HSSTATE='2'";
            DataTable dtCount = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dtCount.Rows[0][0].ToString() == "1")
            {
                str.Clear();
                sqlText = "update CM_KAIPIAO set KP_ZONGSTATE='2' where Id='" + hidId.Value + "' and KP_SPSTATE='3' and KP_HSSTATE='2'";
                str.Add(sqlText);
                strEngId = GetTaskIds();
                foreach (var engid in strEngId)
                {
                    sqlText = "update TBPM_TCTSASSGN set TSA_STATE='2' where TSA_ID='" + engid + "'";
                    str.Add(sqlText);
                    sqlText = "update TBQM_QTSASSGN set QSA_STATE='2' where QSA_ENGID='" + engid + "'";
                    str.Add(sqlText);
                }
                DBCallCommon.ExecuteTrans(str);
            }

            Response.Write("<script>alert('保存成功！');window.location='CM_Kaipiao_List.aspx'</script>");
        }

        private void SendEmailTo(string p)
        {
            string _emailto = DBCallCommon.GetEmailAddressByUserID(p);
            string _body = "开票管理审批任务:"
                  + "\r\n编    号：" + KP_CODE.Text.Trim()
                  + "\r\n制    单：" + KP_ZDRNM.Text.Trim() + "";

            string _subject = "您有新的【增值税发票申请单】需要审批，请及时处理";
            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
        }

        private List<string> GetTaskIds()
        {
            List<string> list = new List<string>();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {

                string engid = GridView1.Rows[i].Cells[2].Text;
                if (!list.Contains(engid))
                {
                    list.Add(engid);
                }
            }
            return list;
        }
        #endregion

        protected void btn_Back_Click(object sender, EventArgs e)
        {
            Response.Redirect("CM_Kaipiao_List.aspx");
        }


        protected void KP_COM_TextChanged(object sender, EventArgs e)
        {
            string Code = KP_COM.Text.Split('|')[0];
            sqlText = "SELECT * from TBCS_CUSUPINFO where CS_CODE='" + Code + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                KP_COM.Text = row["CS_NAME"].ToString();
                KP_ADDRESS.Text = row["CS_ADDRESS"].ToString();
                KP_ACCOUNT.Text = row["CS_Account"].ToString();
                KP_SHUIHAO.Text = row["CS_TAX"].ToString();
                KP_BANK.Text = row["CS_Bank"].ToString();
                KP_TEL.Text = row["CS_PHONO"].ToString();
            }
        }

        protected void btnTicket_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(KP_KPNUMBER.Text))
            {
                sqlText = "update CM_KAIPIAO set KP_KPNUMBER='" + KP_KPNUMBER.Text.Trim() + "',KP_KPDATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "' where  Id='" + hidId.Value + "'";
                DBCallCommon.ExeSqlText(sqlText);
                Response.Write("<script>alert('开票成功！')</script>");
            }
            else
            {
                Response.Write("<script>alert('发票号不能为空！');</script>"); return;//后台验证
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {

            //Id, cId, TaskId, ConId, Proj, Engname, Map, Number, Unit, Money, dfTaskId, dfProName, kpmoney, dfConId
            sqlText = "select TaskId, ConId, Proj, Engname, Map, Number, Unit, kpmoney, Money, dfTaskId, dfProName, dfConId from CM_KAIPIAO_DETAIL where cId='" + hidTaskId.Value + "' ";

            string filename = "开票明细导出.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("开票明细导出.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet = wk.GetSheetAt(0);

                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                if (dt.Rows.Count == 0)
                {

                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
                    return;
                }
                ////填充数据

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet.CreateRow(i + 1);

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }

                sheet.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }

        }


    }
}
