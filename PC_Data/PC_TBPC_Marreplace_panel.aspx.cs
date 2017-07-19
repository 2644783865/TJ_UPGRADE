using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Collections.Generic;
namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Marreplace_panel : System.Web.UI.Page
    {
        public string gloabsheetno
        {
            get
            {
                object str = ViewState["gloabsheetno"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabsheetno"] = value;
            }
        }
        public string gloabptc
        {
            get
            {
                object str = ViewState["gloabptc"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabptc"] = value;
            }
        }
        public string gloabstate
        {
            get
            {
                object str = ViewState["gloabstate"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabstate"] = value;
            }
        }
        public string gloabmarid
        {
            get
            {
                object str = ViewState["gloabmarid"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabmarid"] = value;
            }
        }

        public string gloabpocode
        {
            get
            {
                object str = ViewState["gloabpocode"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabpocode"] = value;
            }
        }
        //public string gloabckstate
        //{
        //    get
        //    {
        //        object str = ViewState["gloabckstate"];
        //        return str == null ? null : (string)str;
        //    }
        //    set
        //    {
        //        ViewState["gloabckstate"] = value;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_delete.Attributes.Add("OnClick", "Javascript:return confirm('是否确定删除?');");
            btn_DYQR.Attributes.Add("OnClick", "Javascript:return confirm('是否进行打印确认?确认之后将表示本单据已经打印过！');");
            btn_THBZ.Attributes.Add("OnClick", "Javascript:return confirm('是否确定替换备注?确认之后将计划中的备注替换成代用单中输入的备注！');");

            if (!IsPostBack)
            {
                if (Request.QueryString["mpno"] != null)
                {
                    gloabsheetno = Request.QueryString["mpno"].ToString();//单号
                }
                else
                {
                    gloabsheetno = "";
                }
                if (Request.QueryString["ptc"] != null)
                {
                    gloabptc = Request.QueryString["ptc"].ToString();//计划号
                }
                else
                {
                    gloabptc = "";
                }

                Tb_Code.Text = gloabsheetno;//单号
                Hyp_print.NavigateUrl = "PC_TBPC_MARPLACE_PRINT.aspx?sheetno=" + gloabsheetno;
                initpager();
                initpower();
            }
            //CheckUser(ControlFinder);
        }
        protected void initpager()
        {

            string sqltext = "";
            sqltext = "SELECT mpcode, plancode, zdreson, zdrid, zdtime, zdwctime, shraid," +
                      "shratime, shrayj, shrbid, shrbtime, shrbyj, totalstate,totalnote, " +
                      "zdrnm, shranm, shrbnm, engid, pjid,pjnm, engnm,MP_CKSHRID,MP_CKSHRTIME,MP_CKSHRDVC,ST_NAME,MP_CKSTATE,leader,MP_LEADER,MP_LEADTIME,MP_LEADADVC  " +
                      "FROM View_TBPC_MARREPLACE_total_planrvw where mpcode='" + Tb_Code.Text + "'";
            DataTable dt = new DataTable();
            dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                string DLRid = Session["UserID"].ToString();
                //string ckstate = dt.Rows[0]["MP_CKSTATE"].ToString();
                //gloabckstate = ckstate;
                Tb_pjid.Text = dt.Rows[0]["pjid"].ToString();
                Tb_pjname.Text = dt.Rows[0]["pjnm"].ToString();
                Tb_engname.Text = dt.Rows[0]["engnm"].ToString();
                Tb_Date.Text = dt.Rows[0]["zdtime"].ToString();
                Tb_Abstract.Text = dt.Rows[0]["totalnote"].ToString();

                tb_Document.Text = dt.Rows[0]["zdrnm"].ToString();
                lb_DocumentID.Text = dt.Rows[0]["zdrid"].ToString();

                tb_shenhe.Text = dt.Rows[0]["shranm"].ToString();
                lb_shenheID.Text = dt.Rows[0]["shraid"].ToString();

                tb_Manager.Text = dt.Rows[0]["shrbnm"].ToString();
                lb_ManagerID.Text = dt.Rows[0]["shrbid"].ToString();

                shenhe_note1.Text = dt.Rows[0]["zdreson"].ToString();
                TextBoxdatatime1.Text = dt.Rows[0]["zdwctime"].ToString();
                TextBoxp1.Text = dt.Rows[0]["zdrnm"].ToString();
                TextBoxp1id.Text = dt.Rows[0]["zdrid"].ToString();

                shenhe_note2.Text = dt.Rows[0]["shrayj"].ToString();
                TextBoxdatatime2.Text = dt.Rows[0]["shratime"].ToString();
                TextBoxp2.Text = dt.Rows[0]["shranm"].ToString();
                TextBoxp2id.Text = dt.Rows[0]["shraid"].ToString();

                shenhe_note3.Text = dt.Rows[0]["shrbyj"].ToString();
                TextBoxdatatime3.Text = dt.Rows[0]["shrbtime"].ToString();
                TextBoxp3.Text = dt.Rows[0]["shrbnm"].ToString();
                TextBoxp3id.Text = dt.Rows[0]["shrbid"].ToString();

                //shenhe_note4.Text = dt.Rows[0]["MP_CKSHRDVC"].ToString();
                //TextBoxdatatime4.Text = dt.Rows[0]["MP_CKSHRTIME"].ToString();
                //TextBoxp4.Text = dt.Rows[0]["ST_NAME"].ToString();
                //TextBoxp4id.Text = dt.Rows[0]["MP_CKSHRID"].ToString();
                //tb_ckshr.Text = dt.Rows[0]["ST_NAME"].ToString();
                //tb_ckshrid.Text = dt.Rows[0]["MP_CKSHRID"].ToString();
                gloabstate = dt.Rows[0]["totalstate"].ToString();
                //string a= dt.Rows[0]["MP_SFYZ"].ToString().Trim();

                shenhe_lead.Text = dt.Rows[0]["MP_LEADADVC"].ToString();
                TextBoxp11.Text = dt.Rows[0]["leader"].ToString();
                TextBoxp11id.Text = dt.Rows[0]["MP_LEADER"].ToString();
                TextBoxdatatime11.Text = dt.Rows[0]["MP_LEADTIME"].ToString();

                //if (ckstate == "1")
                //{
                //    rd_agree4.Checked = true;
                //}
                //else if (ckstate == "2")
                //{
                //    rd_disagree4.Checked = true;
                //}
                //else
                //{
                //    rd_agree4.Checked = false;
                //    rd_disagree4.Checked = false;
                //}

                if (gloabstate == "211")
                {
                    rd_agree1.Checked = true;
                    rd_agree2.Checked = true;
                }
                else if (gloabstate == "200")
                {
                    rd_disagree2.Checked = true;
                }
                else if (gloabstate == "311")
                {
                    rd_agree1.Checked = true;
                    rd_agree2.Checked = true;
                    rd_agree3.Checked = true;
                }
                else if (gloabstate == "300")
                {
                    rd_agree2.Checked = true;
                    rd_disagree3.Checked = true;
                }
                else if (gloabstate == "002")
                {
                    rd_disagree1.Checked = true;
                }
                else if (gloabstate == "111")
                {
                    rd_agree1.Checked = true;
                }
                if (DLRid == TextBoxp3id.Text)
                {
                    if (gloabstate == "311" || gloabstate == "300")
                    {
                        btn_fanshen.Enabled = true;
                    }
                    else
                    {
                        btn_fanshen.Enabled = false;
                    }
                }
                else if (DLRid == TextBoxp2id.Text)
                {
                    if (gloabstate == "211" || gloabstate == "200")
                    {
                        btn_fanshen.Enabled = true;
                    }
                    else
                    {
                        btn_fanshen.Enabled = false;
                    }
                }
                else if (DLRid == TextBoxp1id.Text)
                {
                    if (gloabstate == "111")
                    {
                        btn_fanshen.Enabled = true;
                    }
                    else
                    {
                        btn_fanshen.Enabled = false;
                    }
                }
                //else if (DLRid == TextBoxp4id.Text)
                //{
                //    if (gloabstate == "311" && TextBoxdatatime4.Text != "")
                //    {
                //        btn_fanshen.Enabled = true;
                //    }
                //    else
                //    {
                //        btn_fanshen.Enabled = false;
                //    }
                //}
                else
                {
                    btn_fanshen.Enabled = false;
                }
                //if ((gloabstate == "000" && Tb_Code.Text.Substring(4, 1).ToString() == "0") || TextBoxp4id.Text != "")
                //{
                //    Panel_shenhe4body.Visible = false;
                //    ckshr.Visible = true;
                //}
                //else
                //{
                //    Panel_shenhe4body.Visible = false;
                //    ckshr.Visible = false;
                //}
                if (Session["UserDeptID"].ToString() != "06")
                {
                    btn_DYQR.Visible = false;
                }
            }
            if (lb_DocumentID.Text != "8")
                label1.Text = "库管员：";
            else
                label1.Text = "采购员：";
            Marreplace_detail_repeaterbind();
        }
        //判断确定和编辑按钮是否可用

        protected void rd_agree2_checkedchanged(object sender, EventArgs e)
        {
            if (rd_agree2.Checked)
            {
                if (shenhe_note2.Text.Replace(" ", "") == "")
                {
                    shenhe_note2.Text = "同意";
                }
            }
        }

        protected void rd_agree3_checkedchanged(object sender, EventArgs e)
        {
            if (rd_agree3.Checked)
            {
                if (shenhe_note3.Text.Replace(" ", "") == "")
                {
                    shenhe_note3.Text = "同意";
                }
            }
        }

        //protected void rd_agree4_checkedchanged(object sender, EventArgs e)
        //{
        //    if (rd_agree4.Checked)
        //    {
        //        if (shenhe_note4.Text.Replace(" ", "") == "")
        //        {
        //            shenhe_note4.Text = "同意";
        //        }
        //    }
        //}

        protected void rd_agree5_checkedchanged(object sender, EventArgs e)
        {
            if (rd_agree1.Checked)
            {
                if (shenhe_lead.Text.Replace(" ", "") == "")
                {
                    shenhe_lead.Text = "同意";
                }
            }
        }


        private void initpower()
        {
            string userid = Session["UserID"].ToString();
            if (gloabstate == "000" || gloabstate == "200" || gloabstate == "002" || gloabstate == "300")
            {
                //当状态为未提交和驳回时，并且登录人为制单人时“编辑”按钮可用
                if (userid == lb_DocumentID.Text)
                {
                    //btn_confirm.Enabled = true;
                    btn_edit.Enabled = true;
                    btn_delete.Enabled = true;
                    //btn_fanclose.Enabled = true;
                }
                else
                {
                    //btn_confirm.Enabled = false;
                    btn_delete.Enabled = false;
                    btn_edit.Enabled = false;
                    //btn_fanclose.Enabled = false;
                }
            }
            //else if (gloabstate == "311")
            //{
            //    if (gloabckstate == "2")
            //    {
            //        btn_edit.Enabled = true;
            //        btn_delete.Enabled = true;
            //    }
            //    else
            //    {
            //        btn_delete.Enabled = false;
            //    }
            //}
            else
            {
                btn_delete.Enabled = false;
                //btn_fanclose.Enabled = false;
            }

            if ((gloabstate == "000" || gloabstate == "200" || gloabstate == "300" || gloabstate == "002") && userid == lb_DocumentID.Text)//状态为未提交，登录人为制单人
            {
                Panel_shen1body.Enabled = true;
                btn_confirm.Enabled = true;
                shenhe_note1.BackColor = System.Drawing.Color.Coral;//一级审核的textbox背景为珊瑚红
            }
            else if (gloabstate == "001" && userid == TextBoxp11id.Text)//提交至部长审核
            {
                btn_delete.Enabled = false;
                Panel_lead.Enabled = true;
                btn_confirm.Enabled = true;
                shenhe_lead.BackColor = System.Drawing.Color.Coral;//二级审核的textbox背景为珊瑚红
            }
            else if (gloabstate == "111" && userid == lb_shenheID.Text)//状态为一审通过，登录人为制单人
            {
                btn_delete.Enabled = false;
                Panel_shenhe2body.Enabled = true;
                btn_confirm.Enabled = true;
                shenhe_note2.BackColor = System.Drawing.Color.Coral;//二级审核的textbox背景为珊瑚红
            }
            else if (gloabstate == "211" && userid == lb_ManagerID.Text)//状态为二审通过，登录人为制单人
            {
                btn_delete.Enabled = false;
                Panel_shenhe3body.Enabled = true;
                btn_confirm.Enabled = true;
                shenhe_note3.BackColor = System.Drawing.Color.Coral;//三级审核的textbox背景为珊瑚红
            }
            //else if (gloabstate == "311" && userid == TextBoxp4id.Text && TextBoxdatatime4.Text == "")
            //{
            //    btn_delete.Enabled = false;
            //    Panel_shenhe4body.Enabled = true;
            //    btn_confirm.Enabled = true;
            //    shenhe_note4.BackColor = System.Drawing.Color.Coral;//四级审核的textbox背景为珊瑚红
            //}
            else
            {
                //Panel_shenhe4body.Enabled = false;
                btn_confirm.Enabled = false;
            }

        }

        //Repeater绑定视图View_TBPC_MARREPLACE_total_all_detail，材料代用详细信息表
        private void Marreplace_detail_repeaterbind()
        {
            string sqltext = "";

            sqltext = "SELECT marid, num, ptcode, usenum, allstate, allshstate, alloption, allnote, marnm, " +
                    "marguige, marguobiao, marcaizhi, marcgunit, mpcode, plancode, zdreson, zdrid, " +
                    "zdtime, shraid, shratime, shrayj, shrbid, shrbtime, shrbyj, totalstate, totalnote, zdrnm, " +
                    "shranm, shrbnm, engid, pjnm, engnm, detailmarnm, detailmarguige, " +
                    "detailmarguobiao, detailmarcaizhi, detailmarunit, detailmpcode, detailmarid, " +
                    "detailmarnuma, detailmarnumb, detailnote, detailoldsqcode, detailnewsqcode, " +
                    "detailstate, fzunit, length, width, detaillength, detailwidth, detailfzunit, fznum, " +
                    "usefznum, pjid, zdwctime " +
                    "FROM View_TBPC_MARREPLACE_total_all_detail " +
                    "WHERE mpcode='" + Tb_Code.Text + "' order by ptcode asc";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                string oldmarid = dt.Rows[0]["marid"].ToString();
                gloabmarid = oldmarid.Substring(0, 5).ToString();
            }

            DBCallCommon.BindRepeater(Marreplace_detail_repeater, sqltext);
            if (Marreplace_detail_repeater.Items.Count == 0)
            {
                NoDataPane.Visible = true;
            }
            else
            {
                NoDataPane.Visible = false;
            }
        }

        protected void Tb_newmarnum_Textchanged(object sender, EventArgs e)
        {
            string s_reg = @"^(-?\d+)(\.\d+)?$";
            TextBox Tb_newmarnum = (TextBox)sender;//定义TextBox
            string s = Tb_newmarnum.Text;
            Regex reg = new Regex(s_reg);
            if (!reg.IsMatch(s))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能输入数字，请重新输入！');", true);
                Tb_newmarnum.Text = "0";
            }
        }

        //根据单号判断跳转到那个页面
        protected void btn_edit_Click(object sender, EventArgs e)
        {
            string repcode = Tb_Code.Text;
            Response.Redirect("~/PC_Data/PC_TBPC_Marreplace_edit.aspx?mpcode=" + repcode);
        }

        //protected void btn_bohuiconcel_Click(object sender, EventArgs e)
        //{
        //    string sqltext = "";
        //    string ptcode = "";
        //    string mtoptcode = "";
        //    string newmarid = "";
        //    string newmarname = "";
        //    string newguige = "";
        //    string newcaizhi = "";
        //    string newguobiao = "";
        //    double num = 0;
        //    double weight = 0;
        //    double newnum = 0;
        //    double newweight = 0;
        //    double usenum = 0;
        //    string state = "";
        //    string prstate = "";
        //    string prnode = "";
        //    string purprnode = "";
        //    string purstate = "";
        //    purprnode = "1";
        //    purstate = "0";
        //    foreach (RepeaterItem Retem in Marreplace_detail_repeater.Items)
        //    {
        //        CheckBox cbk = Retem.FindControl("CKBOX_SELECT") as CheckBox;
        //        if (cbk.Checked)
        //        {
        //            ptcode = ((Label)Retem.FindControl("MP_PTCODE")).Text.ToString();
        //            mtoptcode = ptcode.Substring(0, ptcode.Length - 4) + "CDY" + ptcode.Substring(ptcode.Length - 4);
        //            newmarid = ((TextBox)Retem.FindControl("MP_NEWMARID")).Text.ToString();
        //            newmarname = ((Label)Retem.FindControl("MP_NEWMARNAME")).Text.ToString();
        //            newguige = ((Label)Retem.FindControl("MP_NEWGUIGE")).Text.ToString();
        //            newcaizhi = ((Label)Retem.FindControl("MP_NEWCAIZHI")).Text.ToString();
        //            newguobiao = ((Label)Retem.FindControl("MP_NEWGUOBIAO")).Text.ToString();
        //            newnum = Convert.ToDouble(((TextBox)Retem.FindControl("MP_NEWNUMA")).Text.ToString());
        //            newweight = Convert.ToDouble(((TextBox)Retem.FindControl("MP_NEWWEIGHT")).Text.ToString());
        //            usenum = newmarid.Substring(0, 4) == "01.07" ? newweight : newnum;
        //            num = Convert.ToDouble(((Label)Retem.FindControl("MP_OLDNUMA")).Text.ToString()) - Convert.ToDouble(((TextBox)Retem.FindControl("MP_NEWNUMA")).Text.ToString());
        //            weight = Convert.ToDouble(((Label)Retem.FindControl("MP_OLDWEIGHT")).Text.ToString()) - Convert.ToDouble(((TextBox)Retem.FindControl("MP_NEWWEIGHT")).Text.ToString());
        //            sqltext = "delect from TBPC_MARREPLACEDETAIL where MP_PTCODE='" + ptcode + "'";
        //            DBCallCommon.ExeSqlText(sqltext);
        //            sqltext = "update TBPC_PURCHASEPLAN set PUR_PRONODE='" + purprnode + "',PUR_STATE='" + purstate + "' where PUR_PTCODE='" + ptcode + "'";
        //            DBCallCommon.ExeSqlText(sqltext);
        //            ////**********************改变备库量*********************
        //            //sqltext = "select * from TBWS_STORAGE where SQ_MARID='" + marid + "' AND SQ_STANDARD='" + guige + "' AND SQ_PTC=''";
        //            //DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
        //            //if (dt.Rows.Count > 0)
        //            //{
        //            //    sqltext = "update TBWS_STORAGE set SQ_NUM=SQ_NUM-" + bgnum + " where SQ_MARID='" + marid + "' AND SQ_STANDARD='" + guige + "' AND SQ_PTC=''";
        //            //    DBCallCommon.ExeSqlText(sqltext);
        //            //    if (num == -bgnum)
        //            //    {
        //            //        sqltext = "delete from TBWS_STORAGE where SQ_PTC='" + oldptcode + "'";
        //            //    }
        //            //    else
        //            //    {
        //            //        sqltext = "update TBWS_STORAGE set SQ_NUM=SQ_NUM+" + bgnum + " where SQ_PTC='" + oldptcode + "'";
        //            //    }
        //            //    DBCallCommon.ExeSqlText(sqltext);
        //            //}
        //            //else
        //            //{
        //            //    if (num == -bgnum)
        //            //    {
        //            //        sqltext = "update TBWS_STORAGE set SQ_PTC='' where SQ_PTC='" + oldptcode + "'";
        //            //        DBCallCommon.ExeSqlText(sqltext);
        //            //    }
        //            //    else
        //            //    {
        //            //        sqltext = "insert into TBWS_STORAGE( SQ_CODE,SQ_MARID,SQ_MARNAME,SQ_ATTRIBUTE," +
        //            //                  "SQ_GB,SQ_STANDARD,SQ_LENGTH,SQ_WIDTH,SQ_LOTNUM,SQ_PMODE,SQ_PTC,SQ_WAREHOUSEID," +
        //            //                  "SQ_WAREHOUSENAME, SQ_POSITIONID, SQ_POSITIONNAME, SQ_UNIT, SQ_NUM, SQ_UNITPRICE, " +
        //            //                  "SQ_AMOUNT, SQ_INCODE, SQ_OUTCODE, SQ_NOTE) values (select '',SQ_MARID,SQ_MARNAME," +
        //            //                  "SQ_ATTRIBUTE,SQ_GB,SQ_STANDARD,SQ_LENGTH,SQ_WIDTH,SQ_LOTNUM,SQ_PMODE,SQ_PTC,SQ_WAREHOUSEID," +
        //            //                  "SQ_WAREHOUSENAME, SQ_POSITIONID, SQ_POSITIONNAME, SQ_UNIT, " + (-bgnum) + ", SQ_UNITPRICE, SQ_AMOUNT, " +
        //            //                  "SQ_INCODE, SQ_OUTCODE, SQ_NOTE from TBWS_STORAGE where SQ_PTC='" + oldptcode + "')";
        //            //        DBCallCommon.ExeSqlText(sqltext);
        //            //        sqltext = "update TBWS_STORAGE set SQ_NUM=SQ_NUM+" + bgnum + " where SQ_PTC='" + oldptcode + "'";
        //            //        DBCallCommon.ExeSqlText(sqltext);
        //            //    }
        //            //}
        //        }
        //    }
        //}
        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            string sqltext = "";
            foreach (RepeaterItem reitem in Marreplace_detail_repeater.Items)
            {
                string newmarid = ((Label)reitem.FindControl("MP_NEWMARID")).Text.ToString().Trim();
                if (newmarid == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入代用的物料编码！');", true);
                    return;
                }
                else
                {
                    string sqlnewmarid = "select * from TBMA_MATERIAL where ID='" + newmarid + "'";
                    DataTable dtnewmarid = DBCallCommon.GetDTUsingSqlText(sqlnewmarid);
                    if (dtnewmarid.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('物料编码不存在！');", true);
                        return;
                    }
                }
            }
            if (gloabstate == "000" || gloabstate == "200" || gloabstate == "300" || gloabstate == "002")//一审
            {
                if (shenhe_note1.Text.ToString().Replace(" ", "") == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写代用理由！');", true);
                    shenhe_note1.Focus();
                    return;
                }
                else
                {
                    sqltext = "UPDATE TBPC_MARREPLACETOTAL SET MP_STATE='001',MP_REASON='" + shenhe_note1.Text + "'," +
                                    "MP_FILLWCTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',MP_FILLFMTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  " +
                                    "WHERE MP_CODE='" + Tb_Code.Text + "'";
                    sqltextlist.Add(sqltext);
                    string mailstr = "select MP_LEADER from TBPC_MARREPLACETOTAL WHERE MP_CODE='" + Tb_Code.Text + "'";
                    DataTable mail = DBCallCommon.GetDTUsingSqlText(mailstr);
                    if (mail.Rows.Count > 0)
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(mail.Rows[0][0].ToString()), new List<string>(), new List<string>(), "代用单审批", "您有代用单需要审批，请登录查看。");
                    }
                }
            }
            else if (gloabstate == "001")//提交至部长
            {
                //string code = Tb_Code.Text.Substring(4, 1);
                if (rd_agree1.Checked)
                {

                    sqltext = "UPDATE TBPC_MARREPLACETOTAL SET MP_STATE='111',MP_LEADADVC='" + shenhe_lead.Text + "'," +
                                 "MP_LEADTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'  " +
                                 "WHERE MP_CODE='" + Tb_Code.Text + "'";
                    sqltextlist.Add(sqltext);

                    string mailstr = "select MP_REVIEWAID from TBPC_MARREPLACETOTAL WHERE MP_CODE='" + Tb_Code.Text + "'";
                    DataTable mail = DBCallCommon.GetDTUsingSqlText(mailstr);
                    if (mail.Rows.Count > 0)
                    {
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(mail.Rows[0][0].ToString()), new List<string>(), new List<string>(), "代用单审批", "您有代用单需要审批，请登录查看。");
                    }
                }
                else
                {
                    if (rd_disagree1.Checked)
                    {
                        if (shenhe_lead.Text.ToString().Replace(" ", "") == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写拒绝理由！');", true);
                            shenhe_note2.Focus();
                            return;
                        }
                        else
                        {
                            sqltext = "UPDATE TBPC_MARREPLACETOTAL SET MP_STATE='002',MP_LEADADVC='" + shenhe_lead.Text + "'," +
                                        "MP_LEADTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                                        "WHERE MP_CODE='" + Tb_Code.Text + "'";
                            sqltextlist.Add(sqltext);


                            string content = "代用单编号：" + Tb_Code.Text.Trim() + "，驳回理由：" + shenhe_lead.Text.Trim() + "";
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lb_DocumentID.Text.Trim()), new List<string>(), new List<string>(), "代用单驳回提醒", content);
                        }
                    }
                }
            }
            else if (gloabstate == "111")//二审
            {

                if (rd_agree2.Checked)
                {
                    sqltext = "UPDATE TBPC_MARREPLACETOTAL SET MP_REVIEWATIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                              "MP_REVIEWAADVC='" + shenhe_note2.Text.ToString() + "',MP_STATE='211' WHERE MP_CODE='" + Tb_Code.Text + "'";
                    sqltextlist.Add(sqltext);

                    string mailstr = "select MP_CHARGEID from TBPC_MARREPLACETOTAL WHERE MP_CODE='" + Tb_Code.Text + "'";
                    DataTable mail = DBCallCommon.GetDTUsingSqlText(mailstr);
                    if (mail.Rows.Count > 0)
                    {
                        if (mail.Rows[0][0].ToString() == "146")//曹卫亮转李小婷,2016.11.21
                        {
                            string lxt_id = "67";
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lxt_id), new List<string>(), new List<string>(), "代用单审批", "原任务负责人为曹卫亮的代用单需要审批，请登录查看。");
                        }
                        else
                        {
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(mail.Rows[0][0].ToString()), new List<string>(), new List<string>(), "代用单审批", "您有代用单需要审批，请登录查看。");
                        }
                    }
                }
                else
                {
                    if (rd_disagree2.Checked)
                    {
                        if (shenhe_note2.Text.ToString().Replace(" ", "") == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写拒绝理由！');", true);
                            shenhe_note2.Focus();
                            return;
                        }
                        else
                        {
                            sqltext = "UPDATE TBPC_MARREPLACETOTAL SET MP_REVIEWATIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                     "MP_REVIEWAADVC='" + shenhe_note2.Text.ToString() + "',MP_STATE='200' WHERE MP_CODE='" + Tb_Code.Text + "'";
                            sqltextlist.Add(sqltext);

                            string content = "代用单编号：" + Tb_Code.Text.Trim() + "，驳回理由：" + shenhe_note2.Text.Trim() + "";
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lb_DocumentID.Text.Trim()), new List<string>(), new List<string>(), "代用单驳回提醒", content);
                        }
                    }
                }

            }
            else if (gloabstate == "211")//三审
            {
                if (rd_agree3.Checked)
                {
                    sqltext = "UPDATE TBPC_MARREPLACETOTAL SET MP_CHARGETIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                              "MP_CHARGEADVC='" + shenhe_note3.Text.ToString() + "',MP_STATE='311' , mp_ck_bt='1'  WHERE MP_CODE='" + Tb_Code.Text + "'";
                    sqltextlist.Add(sqltext);
                    string ss = Tb_Code.Text.Substring(4, 1);
                    if (Tb_Code.Text.Substring(4, 1) == "1")
                    {
                        changemaasige("1");
                    }
                    else if (Tb_Code.Text.Substring(4, 1) == "2")
                    {
                        changemaasige("2");
                        changeDD();
                    }
                    else if (Tb_Code.Text.Substring(4, 1) == "0")
                    {
                        changexsdy();
                        string mailstr = "SELECT zdrid FROM View_TBPC_MARREPLACE_total_planrvw where mpcode='" + Tb_Code.Text + "'";
                        DataTable mail = DBCallCommon.GetDTUsingSqlText(mailstr);
                        if (mail.Rows.Count > 0)
                        {
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(mail.Rows[0][0].ToString()), new List<string>(), new List<string>(), "代用单审批", "您有代用单审批通过，请登录查看。");
                            string mailsql = "select ST_ID from TBDS_STAFFINFO where  ST_POSITION= '1202' or ST_POSITION= '1205' or ST_POSITION= '1201' or ST_POSITION= '0404'or ST_POSITION= '0504' and ST_PD='0' ";
                            DataTable mailadd = DBCallCommon.GetDTUsingSqlText(mailsql);
                            if (mailadd.Rows.Count > 0)
                            {
                                string mailbody = "单号为" + Tb_Code.Text.Trim() + "的代用单已审批通过，请注意处理后续流程。";
                                for (int i = 0; i < mailadd.Rows.Count; i++)
                                {
                                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(mailadd.Rows[i][0].ToString()), new List<string>(), new List<string>(), "代用单审批通过", mailbody);
                                }
                            }
                        }
                        //发送邮件给技术主管艾广修、下料编程员
                        string sqltextmc = "select marnm from View_TBPC_MARREPLACE_total_all_detail where marnm like '%钢板%' and mpcode='" + Tb_Code.Text + "'";
                        DataTable dtmc = DBCallCommon.GetDTUsingSqlText(sqltextmc);
                        if (dtmc.Rows.Count > 0)
                        {
                            string mailsql = "select ST_ID from View_TBDS_STAFFINFO where dep_position like '%下料编程员%' or st_name='艾广修'";
                            DataTable mailadd = DBCallCommon.GetDTUsingSqlText(mailsql);
                            if (mailadd.Rows.Count > 0)
                            {
                                string mailbody = "单号为" + Tb_Code.Text.Trim() + "的代用单已审批通过，请注意处理后续流程。";
                                for (int i = 0; i < mailadd.Rows.Count; i++)
                                {
                                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(mailadd.Rows[i][0].ToString()), new List<string>(), new List<string>(), "代用单审批通过", mailbody);
                                }
                            }
                        }
                    }

                }
                else
                {
                    if (rd_disagree3.Checked)
                    {
                        if (shenhe_note3.Text.ToString().Replace(" ", "") == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写拒绝理由！');", true);
                            shenhe_note3.Focus();
                            return;
                        }
                        else
                        {
                            sqltext = "UPDATE TBPC_MARREPLACETOTAL SET MP_CHARGETIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                      "MP_CHARGEADVC='" + shenhe_note3.Text.ToString() + "',MP_STATE='300' WHERE MP_CODE='" + Tb_Code.Text + "'";
                            sqltextlist.Add(sqltext);


                            string content = "代用单编号：" + Tb_Code.Text.Trim() + "，驳回理由：" + shenhe_note3.Text.Trim() + "";
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lb_DocumentID.Text.Trim()), new List<string>(), new List<string>(), "代用单驳回提醒", content);
                        }
                    }
                }
            }
            //else if (gloabstate == "311")//仓库审核
            //{
            //    if (rd_agree4.Checked)
            //    {
            //        sqltext = "UPDATE TBPC_MARREPLACETOTAL SET MP_CKSHRTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
            //                  "MP_CKSHRDVC='" + shenhe_note4.Text.ToString() + "',MP_STATE='311',MP_CKSTATE='1'  WHERE MP_CODE='" + Tb_Code.Text + "'";
            //        sqltextlist.Add(sqltext);
            //        changexsdy();
            //    }
            //    else if (rd_disagree4.Checked)
            //    {
            //        if (shenhe_note4.Text.ToString().Replace(" ", "") == "")
            //        {
            //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写拒绝理由！');", true);
            //            shenhe_note4.Focus();
            //            return;
            //        }
            //        else
            //        {
            //            sqltext = "UPDATE TBPC_MARREPLACETOTAL SET MP_CKSHRTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
            //                        "MP_CKSHRDVC='" + shenhe_note4.Text.ToString() + "',MP_STATE='311',MP_CKSTATE='2' WHERE MP_CODE='" + Tb_Code.Text + "'";
            //            sqltextlist.Add(sqltext);
            //        }
            //    }
            //}
            DBCallCommon.ExecuteTrans(sqltextlist);
            save_advc();//保存审核意见
            Response.Redirect("~/PC_Data/PC_TBPC_Marreplace_list.aspx");
        }
        protected void save_advc()
        {
            string sqltext = "";
            string ptcode = "";
            string advc = "";
            List<string> sqltextlist = new List<string>();
            foreach (RepeaterItem reitem in Marreplace_detail_repeater.Items)
            {
                ptcode = ((Label)(reitem.FindControl("MP_PTCODE"))).Text.ToString();
                advc = ((TextBox)(reitem.FindControl("MP_OPTION"))).Text.ToString();
                sqltext = "update TBPC_MARREPLACEALL set MP_OPTION='" + advc + "' where MP_PTCODE='" + ptcode + "'";
                sqltextlist.Add(sqltext);
            }
            DBCallCommon.ExecuteTrans(sqltextlist);
        }
        //修改采购计划表中的数量
        //protected void changeplannum()
        //{
        //    string sqltext = "";
        //    sqltext = "update TBPC_PURCHASEPLAN set PUR_RPNUM=case when PUR_RPNUM-MP_USENUM<0 then 0 " +
        //                                                          "else PUR_RPNUM-MP_USENUM end," +
        //              "PUR_RPFZNUM= case when PUR_RPFZNUM-MP_USEFZNUM<0 then 0 " +
        //                                  "else PUR_RPFZNUM-MP_USEFZNUM end," +
        //                                  "PUR_STATE='3' from TBPC_PURCHASEPLAN as a left join " +
        //              "TBPC_MARREPLACEALL as b on a.PUR_PTCODE=b.MP_PTCODE  where b.MP_CODE='" + Tb_Code.Text + "'";
        //    DBCallCommon.ExeSqlText(sqltext);
        //}
        //修改采购计划表中的物料信息
        private void changemaasige(string a)//修改我的任务（代用）
        {
            string sqltext = "";

            if (a == "1")
            {
                foreach (RepeaterItem Retem in Marreplace_detail_repeater.Items)
                {
                    string ptcode = ((Label)Retem.FindControl("MP_PTCODE")).Text.ToString();
                    string sqlbjdy = "select * from TBPC_PURCHASEPLAN where PUR_STATE='6' and PUR_PTCODE='" + ptcode + "'";
                    DataTable dtbjdy = DBCallCommon.GetDTUsingSqlText(sqlbjdy);
                    if (dtbjdy.Rows.Count == 0)
                    {
                        sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='4' where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }

                sqltext = "update TBPC_PURCHASEPLAN set PUR_MARID=b.MP_NEWMARID,PUR_LENGTH=b.MP_LENGTH," +
                          "PUR_WIDTH=b.MP_WIDTH,PUR_RPNUM=b.MP_NEWNUMA,PUR_RPFZNUM=b.MP_NEWNUMB,PUR_NUM=b.MP_NEWNUMA,PUR_FZNUM=b.MP_NEWNUMB  from TBPC_PURCHASEPLAN as a left join " +
                          "TBPC_MARREPLACEDETAIL as b on a.PUR_PTCODE=b.MP_PTCODE  where b.MP_CODE='" + Tb_Code.Text + "' and a.PUR_CSTATE='0'";
            }
            else
            {
                sqltext = "update TBPC_PURCHASEPLAN set PUR_MARID=b.MP_NEWMARID,PUR_LENGTH=b.MP_LENGTH," +
                          "PUR_WIDTH=b.MP_WIDTH,PUR_RPNUM=b.MP_NEWNUMA,PUR_RPFZNUM=b.MP_NEWNUMB,PUR_NUM=b.MP_NEWNUMA,PUR_FZNUM=b.MP_NEWNUMB  from TBPC_PURCHASEPLAN as a left join " +
                          "TBPC_MARREPLACEDETAIL as b on a.PUR_PTCODE=b.MP_PTCODE  where b.MP_CODE='" + Tb_Code.Text + "' and a.PUR_CSTATE='0'";

            }
            DBCallCommon.ExeSqlText(sqltext);
            sqltext = "update a set a.PIC_MARID=b.MP_NEWMARID ,a.PIC_LENGTH=b.MP_LENGTH,a.PIC_WIDTH=b.MP_WIDTH,a.PIC_QUANTITY=b.MP_NEWNUMA,a.PIC_FZNUM=b.MP_NEWNUMB,a.PIC_ZXNUM=b.MP_NEWNUMA,a.PIC_ZXFUNUM=b.MP_NEWNUMB from TBPC_IQRCMPPRICE as a left join  TBPC_MARREPLACEDETAIL as b on a.PIC_PTCODE=b.MP_PTCODE   where b.MP_CODE='" + Tb_Code.Text + "'";

            //"update TBPC_IQRCMPPRICE set PIC_MARID='" + marid + "',PIC_LENGTH='" + length + "',PIC_WIDTH='" + width
            //       + "' where PIC_PTCODE='" + ptcode + "'";
            DBCallCommon.ExeSqlText(sqltext);
        }

        private void changexsdy()//修改计划中物料（相似代用）
        {
            string sqltext = "";
            List<string> list = new List<string>();
            foreach (RepeaterItem Retem in Marreplace_detail_repeater.Items)
            {
                string ptcode = ((Label)Retem.FindControl("MP_PTCODE")).Text.ToString();
                string marid = ((Label)Retem.FindControl("MP_NEWMARID")).Text.ToString();
                double length = Convert.ToDouble(((Label)Retem.FindControl("MP_NEWLENGTH")).Text);
                double width = Convert.ToDouble(((Label)Retem.FindControl("MP_NEWWIDTH")).Text);

                sqltext = "update TBPC_PURCHASEPLAN set PUR_MARID='" + marid + "',PUR_LENGTH=" + length + ",PUR_WIDTH=" + width + "  where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='1'";
                list.Add(sqltext);
                //sqltext = "update TBPC_IQRCMPPRICE set PIC_MARID='" + marid + "',PIC_LENGTH='" + length + "',PIC_WIDTH='" + width
                //    + "' where PIC_PTCODE='" + ptcode + "'";
                //list.Add(sqltext);
            }
            DBCallCommon.ExecuteTrans(list);
        }
        private void fschangexsdy()//反审之后改回原来物料(相似)
        {
            string sqltext = "";
            foreach (RepeaterItem Retem in Marreplace_detail_repeater.Items)
            {
                string ptcode = ((Label)Retem.FindControl("MP_PTCODE")).Text.ToString();
                string marid = ((Label)Retem.FindControl("MP_OLDMARID")).Text.ToString();
                double length = Convert.ToDouble(((Label)Retem.FindControl("MP_OLDLENGTH")).Text);
                double width = Convert.ToDouble(((Label)Retem.FindControl("MP_OLDWIDTH")).Text);
                double num = Convert.ToDouble(((Label)Retem.FindControl("MP_OLDNUMA")).Text);
                double fznum = Convert.ToDouble(((Label)Retem.FindControl("MP_OLDNUMB")).Text);
                sqltext = "update TBPC_PURCHASEPLAN set PUR_MARID='" + marid + "',PUR_LENGTH=" + length + ",PUR_WIDTH=" + width + ",PUR_NUM=" + num + ",PUR_FZNUM=" + fznum + "  where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='1'";
                DBCallCommon.ExeSqlText(sqltext);
            }
        }
        private void fschangedy()//反审之后改回原来物料(代用)
        {
            string sqltext = "";
            foreach (RepeaterItem Retem in Marreplace_detail_repeater.Items)
            {
                string ptcode = ((Label)Retem.FindControl("MP_PTCODE")).Text.ToString();
                string marid = ((Label)Retem.FindControl("MP_OLDMARID")).Text.ToString();
                double length = Convert.ToDouble(((Label)Retem.FindControl("MP_OLDLENGTH")).Text);
                double width = Convert.ToDouble(((Label)Retem.FindControl("MP_OLDWIDTH")).Text);
                double num = Convert.ToDouble(((Label)Retem.FindControl("MP_OLDNUMA")).Text);
                double fznum = Convert.ToDouble(((Label)Retem.FindControl("MP_OLDNUMB")).Text);
                sqltext = "update TBPC_PURCHASEPLAN set PUR_MARID='" + marid + "',PUR_LENGTH=" + length + ",PUR_WIDTH=" + width + ",PUR_NUM=" + num + ",PUR_FZNUM=" + fznum + "  where PUR_PTCODE='" + ptcode + "'";
                DBCallCommon.ExeSqlText(sqltext);
            }
        }
        //修改订单计划表中的物料信息
        private void changeDD()
        {
            string sqltext = "";
            string ptcode = "";
            sqltext = "update TBPC_PURORDERDETAIL set PO_MARID=b.MP_NEWMARID,PO_LENGTH=b.MP_LENGTH," +
                      "PO_WIDTH=b.MP_WIDTH,PO_QUANTITY=b.MP_NEWNUMA,PO_FZNUM=b.MP_NEWNUMB,PO_ZXNUM=b.MP_NEWNUMA,PO_ZXFZNUM=b.MP_NEWNUMB  from TBPC_PURORDERDETAIL as a left join " +
                      "TBPC_MARREPLACEDETAIL as b on a.PO_PTCODE=b.MP_PTCODE  where b.MP_CODE='" + Tb_Code.Text + "'";
            DBCallCommon.ExeSqlText(sqltext);
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            foreach (RepeaterItem Retem in Marreplace_detail_repeater.Items)
            {
                ptcode = ((Label)Retem.FindControl("MP_PTCODE")).Text.ToString();
                break;
            }
            sqltext = "select PO_CODE from TBPC_PURORDERDETAIL where PO_PTCODE='" + ptcode + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                gloabpocode = dt.Rows[0]["PO_CODE"].ToString();
            }
            sqltext = "update TBPC_PURORDERTOTAL set PO_ZDDATE='" + date + "' where PO_CODE='" + gloabpocode + "'";
            DBCallCommon.ExeSqlText(sqltext);
        }


        protected void btn_delete_Click(object sender, EventArgs e)
        {
            int temp = candelete();
            if (temp == 0)
            {
                foreach (RepeaterItem Retem in Marreplace_detail_repeater.Items)
                {
                    CheckBox cbk = Retem.FindControl("CKBOX_SELECT") as CheckBox;
                    if (cbk.Checked)
                    {
                        string code = Tb_Code.Text;
                        string leixin = code.Substring(4, 1).ToString();
                        string ptcode = ((Label)Retem.FindControl("MP_PTCODE")).Text.ToString();
                        if (leixin == "0")
                        {
                            string sql = "select * from TBPC_PURCHASEPLAN where PUR_STATE!='6' and PUR_STATE!='7' and PUR_PTCODE='" + ptcode + "'";
                            DataTable dt5 = DBCallCommon.GetDTUsingSqlText(sql);
                            if (dt5.Rows.Count > 0)
                            {
                                string cgr = dt5.Rows[0]["PUR_CGMAN"].ToString();
                                if (dt5.Rows.Count == 2)
                                {
                                    double num1 = Convert.ToDouble(dt5.Rows[0]["PUR_NUM"].ToString());
                                    double fznum1 = Convert.ToDouble(dt5.Rows[0]["PUR_FZNUM"].ToString());
                                    sql = "delete from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='1'";
                                    DBCallCommon.ExeSqlText(sql);
                                    if (cgr == "")
                                    {
                                        sql = "update TBPC_PURCHASEPLAN set PUR_STATE='0',PUR_RPNUM=" + num1 + ",PUR_RPFZNUM=" + fznum1 + ",PUR_PTASMAN='',PUR_PTASTIME='',PUR_CGMAN=''  where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";
                                    }
                                    else
                                    {
                                        sql = "update TBPC_PURCHASEPLAN set PUR_STATE='4',PUR_RPNUM=" + num1 + ",PUR_RPFZNUM=" + fznum1 + "  where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";
                                    }
                                    DBCallCommon.ExeSqlText(sql);
                                }
                                else
                                {
                                    if (cgr == "")
                                    {
                                        sql = "update TBPC_PURCHASEPLAN set PUR_STATE='0',PUR_PTASMAN='',PUR_PTASTIME='',PUR_CGMAN='',PUR_CSTATE='0'  where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='1'";
                                    }
                                    else
                                    {
                                        sql = "update TBPC_PURCHASEPLAN set PUR_STATE='4',PUR_CSTATE='0'  where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='1'";
                                    }
                                    DBCallCommon.ExeSqlText(sql);
                                }
                            }
                        }
                        else if (leixin == "1")
                        {
                            string sql1 = "update TBPC_PURCHASEPLAN set PUR_STATE='4' where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0' and PUR_STATE!='6' and PUR_STATE!='7'";
                            DBCallCommon.ExeSqlText(sql1);
                        }
                        string sqltext4 = "delete from TBPC_MARREPLACEALL where MP_CODE='" + code + "' and MP_PTCODE='" + ptcode + "'";
                        string sqltext5 = "delete from TBPC_MARREPLACEDETAIL where MP_CODE='" + code + "' and MP_PTCODE='" + ptcode + "'";
                        DBCallCommon.ExeSqlText(sqltext4);
                        DBCallCommon.ExeSqlText(sqltext5);
                        string sql2 = "select * from TBPC_MARREPLACEALL where MP_CODE='" + code + "'";
                        DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql2);
                        if (dt2.Rows.Count == 0)
                        {
                            string sqltext6 = "delete from TBPC_MARREPLACETOTAL where MP_CODE='" + code + "'";
                            DBCallCommon.ExeSqlText(sqltext6);
                            Response.Redirect("~/PC_Data/PC_TBPC_Marreplace_list.aspx");
                        }

                    }
                }
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您不是制单人，无权删除！');", true);
            }
            Marreplace_detail_repeaterbind();
        }

        //protected void btn_fanclose_Click(object sender, EventArgs e)
        //{
        //    string ptcode = "";
        //    string sql = "";
        //    int temp = canclose();
        //    if (temp == 0)
        //    {
        //        foreach (RepeaterItem Retem in Marreplace_detail_repeater.Items)
        //        {
        //            CheckBox cbk = Retem.FindControl("CKBOX_SELECT") as CheckBox;
        //            if (cbk.Checked)
        //            {
        //                ptcode = ((Label)Retem.FindControl("MP_PTCODE")).Text;

        //                sql = "select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptcode + "'";
        //                DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql);
        //                if (dt2.Rows.Count == 2)
        //                {
        //                    double num = Convert.ToDouble(dt2.Rows[0]["PUR_NUM"].ToString());
        //                    double fznum = Convert.ToDouble(dt2.Rows[0]["PUR_FZNUM"].ToString());
        //                    sql = "delete from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='1'";
        //                    DBCallCommon.ExeSqlText(sql);
        //                    sql = "update TBPC_PURCHASEPLAN set PUR_RPNUM=" + num + ",PUR_RPFZNUM=" + fznum + " where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";
        //                    DBCallCommon.ExeSqlText(sql);
        //                }
        //                else
        //                {
        //                    double num = Convert.ToDouble(dt2.Rows[0]["PUR_NUM"].ToString());
        //                    double fznum = Convert.ToDouble(dt2.Rows[0]["PUR_FZNUM"].ToString());
        //                    sql = "update TBPC_PURCHASEPLAN set PUR_RPNUM=" + num + ",PUR_RPFZNUM=" + fznum + ",PUR_CSTATE='0'  where PUR_PTCODE='" + ptcode + "'";
        //                    DBCallCommon.ExeSqlText(sql);
        //                }
        //                sql = "delete from TBPC_MARREPLACEALL where MP_PTCODE='" + ptcode + "'";
        //                DBCallCommon.ExeSqlText(sql);
        //                sql = "delete from TBPC_MARREPLACEDETAIL where MP_PTCODE='" + ptcode + "'";
        //                DBCallCommon.ExeSqlText(sql);
        //            }
        //        }
        //        sql = "select MP_CODE from TBPC_MARREPLACEALL where MP_CODE='" + Tb_Code.Text + "'";
        //        DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
        //        if (dt1.Rows.Count == 0)
        //        {
        //            sql = "delete from TBPC_MARREPLACETOTAL where MP_CODE='" + Tb_Code.Text + "'";
        //            DBCallCommon.ExeSqlText(sql);
        //        }
        //        sql = "select PUR_MASHAPE,PUR_PCODE from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptcode + "'";
        //        DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
        //        if (dt.Rows.Count > 0)
        //        {
        //            string shape = dt.Rows[0]["PUR_MASHAPE"].ToString();
        //            string pcode = dt.Rows[0]["PUR_PCODE"].ToString();
        //            sql = "update TBPC_PCHSPLANRVW set PR_STATE='0' where PR_SHEETNO='" + pcode + "'";
        //            DBCallCommon.ExeSqlText(sql);
        //            //Response.Write("<script>javascript:window.open('PC_TBPC_Purchaseplan_startcontent.aspx?shape=" + shape + "&mp_id=" + pcode + "')</script>");
        //            Response.Redirect("PC_TBPC_Purchaseplan_startcontent.aspx?shape=" + shape + "&mp_id=" + pcode + "");
        //        }
        //    }
        //    else if (temp == 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
        //    }
        //    else if (temp == 2)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您不是制单人，无权删除！');", true);
        //    }
        //    else if (temp == 3)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('不是行关闭物料，无法进行反关闭！');", true);
        //    }

        //}
        //判断能否删除
        private int candelete()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            int j = 0;//制单是否为登录用户
            string postid = "";
            string userid = Session["UserID"].ToString();
            string ptcode = "";
            foreach (RepeaterItem Reitem in Marreplace_detail_repeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        postid = lb_DocumentID.Text;
                        if (postid != userid)//登录人不是制单人
                        {
                            j++;
                            break;
                        }
                        ptcode = ((Label)Reitem.FindControl("MP_PTCODE")).Text;

                    }
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else if (j > 0)//登录人不是制单人
            {
                temp = 2;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }
        //判断能否行关闭
        private int canclose()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            int j = 0;//制单是否为登录用户
            int k = 0;//是不是行关闭的物料，是的话可以反行关闭
            string postid = "";
            string userid = Session["UserID"].ToString();
            string ptcode = "";
            foreach (RepeaterItem Reitem in Marreplace_detail_repeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        postid = lb_DocumentID.Text;
                        if (postid != userid)//登录人不是制单人
                        {
                            j++;
                            break;
                        }
                        ptcode = ((Label)Reitem.FindControl("MP_PTCODE")).Text;
                        string sql = "select PUR_CSTATE from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='1'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        if (dt.Rows.Count == 0)
                        {
                            k++;
                        }

                    }
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else if (j > 0)//登录人不是制单人
            {
                temp = 2;
            }
            else if (k > 0)
            {
                temp = 3;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }

        protected void Marreplace_detail_repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                if (gloabmarid == "01.01")
                {
                    HtmlTableCell ycljh = (HtmlTableCell)e.Item.FindControl("ycljh");
                    HtmlTableCell dycljh = (HtmlTableCell)e.Item.FindControl("dycljh");
                    HtmlTableCell fznum1 = (HtmlTableCell)e.Item.FindControl("fznum1");
                    HtmlTableCell length1 = (HtmlTableCell)e.Item.FindControl("length1");
                    HtmlTableCell width1 = (HtmlTableCell)e.Item.FindControl("width1");
                    HtmlTableCell fzunit1 = (HtmlTableCell)e.Item.FindControl("fzunit1");
                    HtmlTableCell fznum2 = (HtmlTableCell)e.Item.FindControl("fznum2");
                    HtmlTableCell length2 = (HtmlTableCell)e.Item.FindControl("length2");
                    HtmlTableCell width2 = (HtmlTableCell)e.Item.FindControl("width2");
                    HtmlTableCell fzunit2 = (HtmlTableCell)e.Item.FindControl("fzunit2");
                    ycljh.ColSpan = 8;
                    dycljh.ColSpan = 8;
                    fznum1.Visible = false;
                    length1.Visible = false;
                    width1.Visible = false;
                    fzunit1.Visible = false;
                    fznum2.Visible = false;
                    length2.Visible = false;
                    width2.Visible = false;
                    fzunit2.Visible = false;
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (gloabmarid == "01.01")
                {
                    HtmlTableCell fznum3 = (HtmlTableCell)e.Item.FindControl("fznum3");
                    HtmlTableCell length3 = (HtmlTableCell)e.Item.FindControl("length3");
                    HtmlTableCell width3 = (HtmlTableCell)e.Item.FindControl("width3");
                    HtmlTableCell fzunit3 = (HtmlTableCell)e.Item.FindControl("fzunit3");
                    HtmlTableCell fznum4 = (HtmlTableCell)e.Item.FindControl("fznum4");
                    HtmlTableCell length4 = (HtmlTableCell)e.Item.FindControl("length4");
                    HtmlTableCell width4 = (HtmlTableCell)e.Item.FindControl("width4");
                    HtmlTableCell fzunit4 = (HtmlTableCell)e.Item.FindControl("fzunit4");
                    fznum3.Visible = false;
                    length3.Visible = false;
                    width3.Visible = false;
                    fzunit3.Visible = false;
                    fznum4.Visible = false;
                    length4.Visible = false;
                    width4.Visible = false;
                    fzunit4.Visible = false;
                }
            }

        }

        protected void btn_fanshen_Click(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            string sqrid = "";
            string jsyid = "";
            string jszgid = "";
            string ckshrid = "";
            string DQRid = Session["UserID"].ToString();
            string sqltext = "select MP_FILLFMID,MP_REVIEWAID,MP_CHARGEID,MP_CKSHRID  from TBPC_MARREPLACETOTAL where MP_CODE='" + Tb_Code.Text + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                sqrid = dt.Rows[0]["MP_FILLFMID"].ToString();
                jsyid = dt.Rows[0]["MP_REVIEWAID"].ToString();
                jszgid = dt.Rows[0]["MP_CHARGEID"].ToString();
                ckshrid = dt.Rows[0]["MP_CKSHRID"].ToString();
            }
            //if (DQRid == ckshrid)
            //{
            //    if (gloabstate == "311")
            //    {
            //        sqltext = "UPDATE TBPC_MARREPLACETOTAL SET MP_CKSHRTIME=''," +
            //         "MP_CKSHRDVC='',MP_STATE='311',MP_CKSTATE='0'  WHERE MP_CODE='" + Tb_Code.Text + "'";
            //        sqltextlist.Add(sqltext);
            //        fschangexsdy();
            //    }
            //}
            if (DQRid == jszgid)
            {
                if (gloabstate == "311" || gloabstate == "300")
                {
                    sqltext = "UPDATE TBPC_MARREPLACETOTAL SET MP_CHARGETIME=''," +
                              "MP_CHARGEADVC='',MP_STATE='211' WHERE MP_CODE='" + Tb_Code.Text + "'";
                    sqltextlist.Add(sqltext);
                    if (ckshrid == "")
                    {
                        fschangedy();
                        fschangexsdy();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('此单据您还未审核，无需反审！');", true);
                }
            }
            else if (DQRid == jsyid)
            {
                if (gloabstate == "211" || gloabstate == "200")
                {
                    sqltext = "UPDATE TBPC_MARREPLACETOTAL SET MP_REVIEWATIME=''," +
                              "MP_REVIEWAADVC='',MP_STATE='111' WHERE MP_CODE='" + Tb_Code.Text + "'";
                    sqltextlist.Add(sqltext);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权反审！');", true);
                }
            }
            else if (DQRid == sqrid)
            {
                if (gloabstate == "111")
                {
                    sqltext = "UPDATE TBPC_MARREPLACETOTAL SET MP_FILLFMTIME=''," +
                              "MP_FILLWCTIME='',MP_STATE='000' WHERE MP_CODE='" + Tb_Code.Text + "'";
                    sqltextlist.Add(sqltext);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权反审！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权反审！');", true);
            }
            DBCallCommon.ExecuteTrans(sqltextlist);
            initpager();
            initpower();
        }
        protected void btn_DYQR_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext = "select MP_SFDY from TBPC_MARREPLACETOTAL where MP_CODE='" + Tb_Code.Text + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                int cs = Convert.ToInt32(dt.Rows[0]["MP_SFDY"].ToString());
                cs = cs + 1;
                string dycs = Convert.ToString(cs);
                sqltext = "update TBPC_MARREPLACETOTAL set MP_SFDY='" + dycs + "' where MP_CODE='" + Tb_Code.Text + "'";
                DBCallCommon.ExeSqlText(sqltext);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('打印确认成功！');", true);
            }
        }

        protected void btn_THBZ_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem Retem in Marreplace_detail_repeater.Items)
            {
                string ptcode = ((Label)Retem.FindControl("MP_PTCODE")).Text;
                string newnote = ((Label)Retem.FindControl("MP_NEWNOTE")).Text;
                string sqltext = "update TBPC_PURCHASEPLAN set PUR_NOTE='" + newnote + "' where PUR_PTCODE='" + ptcode + "'";
                DBCallCommon.ExeSqlText(sqltext);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('采购计划中的备注替换成功！');", true);
            }
        }
    }
}
