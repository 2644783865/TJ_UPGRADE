using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchange_all_detail : System.Web.UI.Page
    {
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

        string tsa = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initipageinfo();
                GetStaffBK();//储运备库人
                CheckBox1.Checked = false;
            }
        }

        private void initipageinfo()
        {
            string bgptcode = Request.QueryString["bgptcode"].ToString();
            string mpid = Request.QueryString["mpid"].ToString();
            tb_ptcode.Text = bgptcode;//变更计划跟踪号
            IEnumerable<string> tsa_id = bgptcode.Split('_').Reverse<string>();
            tsa = tsa_id.ElementAt(2).ToString();
            tb_id.Text = mpid;
            tb_pcode.Text = Request.QueryString["pcode"].ToString();
            tbpc_purbgdetailRepeaterdatabind();
            tbpc_purbgycldetailRepeaterdatabind();
            tbpc_purallyjhdetailRepeaterdatabind();
        }

        protected void GetStaffBK() //储运备库人
        {

            string sql = "SELECT DISTINCT ST_CODE,ST_NAME FROM TBDS_STAFFINFO WHERE st_state='0' and st_code like '0702%' ";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DropDownListBKPersom.DataTextField = "ST_NAME";
            DropDownListBKPersom.DataValueField = "ST_CODE";
            DropDownListBKPersom.DataSource = dt;
            DropDownListBKPersom.DataBind();
            ListItem item = new ListItem("--请选择--", "0");
            DropDownListBKPersom.Items.Insert(0, item);

        }

        //变更信息repeater
        private void tbpc_purbgdetailRepeaterdatabind()
        {
            string sqltext = "select  MP_ID,chpcode as BG_PCODE,chptcode as BG_PTCODE,marid as BG_MARID, marnm as BG_MARNAME, " +
                             "pjid as MP_PJID,pjnm as MP_PJNAME,engnm as MP_ENGNAME,margg as BG_MARNORM, marcz as BG_MARTERIAL, " +
                             "unit as BG_NUNIT,length as LENGTH,width as WIDTH,chfznum as BG_FZNUM, chnum as BG_NUM, chcgid as MP_CGID,chcgnm as MP_CGNAME, " +
                             "margb as BG_GUOBIAO, chstate as MP_STATE, chnote as BG_NOTE,zxnum as BG_ZXNUM,zxfznum as BG_ZXFZNUM  " +
                             "from View_TBPC_MPTEMPCHANGE  " +
                             "where MP_ID=" + tb_id.Text + " and chpcode='" + tb_pcode.Text + "' and chptcode='" + tb_ptcode.Text + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                tb_pcode.Text = dt.Rows[0]["BG_PCODE"].ToString();
                tb_pjid.Text = dt.Rows[0]["MP_PJID"].ToString();
                tb_pjname.Text = dt.Rows[0]["MP_PJNAME"].ToString();
                //tb_engid.Text = dt.Rows[0]["MP_ENGID"].ToString();
                tb_engname.Text = dt.Rows[0]["MP_ENGNAME"].ToString();
                tb_marid.Text = dt.Rows[0]["BG_MARID"].ToString();
                gloabstate = dt.Rows[0]["MP_STATE"].ToString();
            }
            tbpc_purbgdetailRepeater.DataSource = dt;
            tbpc_purbgdetailRepeater.DataBind();
        }
        //采购计划repeater
        private void tbpc_purbgycldetailRepeaterdatabind()
        {
            string sqltext = "SELECT planno as PUR_PCODE,pjid as PUR_PJID,pjnm as PUR_PJNAME,engid as PUR_ENGID,engnm as PUR_ENGNAME,marid as PUR_MARID," +
                             "marnm as PUR_MARNAME,margg as PUR_MARNORM,marcz as PUR_MARTERIAL,margb as PUR_GUOBIAO,num as PUR_NUM,rpnum as PUR_RPNUM," +
                             "marunit as PUR_NUNIT,length as PUR_LENGTH,width as PUR_WIDTH,ptcode as PUR_PTCODE,purnote as PUR_NOTE,cgrid as PUR_CGMAN,picno,orderno," +
                             "cgrnm as PUR_CGMANNM,purstate as PUR_STATE,PUR_CSTATE,PUR_MASHAPE  FROM  View_TBPC_PURCHASEPLAN_IRQ_ORDER   " +
                             "WHERE ptcode like '%" + tsa + "%' and marid='" + tb_marid.Text + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DBCallCommon.BindRepeater(tbpc_purbgyclRepeater, sqltext);
            if (tbpc_purbgyclRepeater.Items.Count == 0)
            {
                NoDataPanep.Visible = true;
            }
            else
            {
                NoDataPanep.Visible = false;
            }
            //DBCallCommon.BindRepeater(tbpc_purbgyclRepeater, sqltext);
        }
        //材料信息repeater
        private void tbpc_purallyjhdetailRepeaterdatabind()
        {
            string sqltext = "";
            string pcode = tb_pcode.Text;
            string engid = tb_engid.Text;
            string marid = tb_marid.Text;
            DataTable dt = new DataTable();
            //正常计划
            sqltext = "SELECT '' as STO,marid as  MARID, " +
                      "marnm as MARNAME,margg as GUIGE, marcz as CAIZHI, " +
                      "margb as GUOBIAO,rpnum as RPNUM, rpfznum as RPFZNUM, " +
                      "marunit as UNIT,length as LENGTH,width as WIDTH,ptcode as PTCODE,cgzgid as CGMAN,cgrnm as CGMANNM," +
                      "purstate as STATE, '正常采购' as NOTE  " +
                      "FROM View_TBPC_PURCHASEPLAN_RVW WHERE ptcode like '%" + tsa + "%' and marid='" + marid + "' and PUR_CSTATE='0'";
            dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            //占用备库
            sqltext = "SELECT '' as STO,marid as  MARID, marnm as MARNAME,margg as GUIGE," +
                     "marcz as CAIZHI,margb as GUOBIAO,num as RPNUM,fznum as  RPFZNUM," +
                     "marunit as UNIT, ptcode as PTCODE,'' as CGMAN,'' as CGMANNM," +
                     "'0' as STATE,'库存占用' as NOTE,length as LENGTH,width as WIDTH  " +
                     "FROM View_TBPC_MARSTOUSEALL  where ptcode like '%" + tsa + "%' and marid='" + marid + "'";
            dt.Merge(DBCallCommon.GetDTUsingSqlText(sqltext));
            dt.AcceptChanges();
            ////代用
            //sqltext = "SELECT '' as STO, marid as  MARID, " +
            //          "marnm as MARNAME,margg as GUIGE, marcz as CAIZHI, " +
            //          "margb as GUOBIAO,rpnum as RPNUM, rpfznum as RPFZNUM, " +
            //          "marunit as UNIT, ptcode as PTCODE,cgrid as CGMAN,cgrnm as CGMANNM," +
            //          "purstate as STATE, '代用采购' as NOTE,length as LENGTH,width as WIDTH  " +
            //          "FROM View_TBPC_PURCHASEPLAN " +
            //          "where ptcode in " +
            //          "(select ptcode FROM  View_TBPC_MARREPLACE_total_all_detail WHERE ptcode like  '" + tb_ptcode.Text + "%'  " +
            //          "and  substring(detailmpcode,5,1)='1')";
            //dt.Merge(DBCallCommon.GetDTUsingSqlText(sqltext));
            //dt.AcceptChanges();
            //相似物料占用
            sqltext = "SELECT '' as STO, marid as  MARID, " +
                     "marnm as MARNAME,margg as GUIGE, marcz as CAIZHI, " +
                     "margb as GUOBIAO,rpnum as RPNUM, rpfznum as RPFZNUM, " +
                     "marunit as UNIT, ptcode as PTCODE,cgrid as CGMAN,cgrnm as CGMANNM," +
                     "purstate as STATE, '相似占用' as NOTE,length as LENGTH,width as WIDTH  " +
                     "FROM View_TBPC_PURCHASEPLAN " +
                     "where ptcode in " +
                     "(select ptcode FROM  View_TBPC_MARREPLACE_total_all_detail WHERE ptcode like  '%" + tsa + "%' and marid='" + marid + "'" +
                     " and  substring(detailmpcode,5,1)='0')";
            dt.Merge(DBCallCommon.GetDTUsingSqlText(sqltext));
            dt.AcceptChanges();
            tbpc_purallyjhdetailRepeater.DataSource = dt;
            tbpc_purallyjhdetailRepeater.DataBind();
            if (tbpc_purallyjhdetailRepeater.Items.Count > 0)
            {
                NoDataPanex.Visible = false;
            }
            else
            {
                NoDataPanex.Visible = true;
            }
        }
        protected void btn_cancel_click(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            string sqltext = "";
            double bgnum = 0;
            double bgfznum = 0;
            string ptcode = "";
            string pur_state = "";
            string note = "";
            foreach (RepeaterItem reitem in tbpc_purbgdetailRepeater.Items)//变更信息表
            {
                bgnum = ((Label)reitem.FindControl("BG_ZXNUM")).Text == "" ? 0 : Convert.ToDouble(((Label)reitem.FindControl("BG_ZXNUM")).Text);
                bgfznum = ((Label)reitem.FindControl("BG_ZXFZNUM")).Text == "" ? 0 : Convert.ToDouble(((Label)reitem.FindControl("BG_ZXFZNUM")).Text);
            }
            if (gloabstate == "0")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('此条变更记录还未执行，无需取消！');", true);
            }
            else
            {
                foreach (RepeaterItem reitem in tbpc_purallyjhdetailRepeater.Items)//材料信息repeater
                {
                    ptcode = ((Label)reitem.FindControl("PTCODE")).Text;
                    pur_state = ((Label)reitem.FindControl("STATE")).Text;
                    note = ((Label)reitem.FindControl("NOTE")).Text;
                    if (Convert.ToInt32(pur_state) == 7 && (bgnum != 0 || bgfznum != 0))
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已经下订单且变更执行数量不为0，不能取消变更！');", true);
                        return;
                    }

                    sqltext = "delete from TBPC_MPCHANGEDETAIL where MP_CHPTCODE='" + ptcode + "'";
                    sqltextlist.Add(sqltext);

                    if (note == "正常采购" || note == "代用采购")
                    {
                        if (Convert.ToInt32(pur_state) < 4)
                        {
                            sqltext = "update TBPC_PURCHASEPLAN set PUR_NUM=PUR_NUM+" + bgnum + ",PUR_FZNUM=PUR_FZNUM+" + bgfznum + "," +
                                      "PUR_RPNUM=PUR_RPNUM+'" + bgnum + "',PUR_RPFZNUM=PUR_RPFZNUM+'" + bgfznum + "'  where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";
                            sqltextlist.Add(sqltext);
                        }
                        if (Convert.ToInt32(pur_state) == 5)
                        {
                            sqltext = "update TBPC_MARREPLACEALL set MP_NUM=MP_NUM+" + bgnum + ",MP_USENUM=MP_USENUM+" + bgnum + " where MP_PTCODE='" + ptcode + "'";
                            sqltextlist.Add(sqltext);
                            sqltext = "update TBPC_MARREPLACEDETAIL set MP_NEWNUMA=MP_NEWNUMA+" + bgnum + "  where MP_PTCODE='" + ptcode + "'";
                            sqltextlist.Add(sqltext);
                        }
                        //if (Convert.ToInt32(pur_state) >= 6)
                        //{
                        //    sqltext = "select PIC_PTCODE from TBPC_IQRCMPPRICE where PIC_PTCODE='" + ptcode + "'";
                        //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                        //    if (dt.Rows.Count == 1)
                        //    {
                        //        sqltext = "update TBPC_IQRCMPPRICE set PIC_QUANTITY=PIC_QUANTITY+" + bgnum + ",PIC_FZNUM=PIC_FZNUM+" + bgfznum + "," +
                        //                  "PIC_ZXNUM=PIC_ZXNUM+" + bgnum + ",PIC_ZXFUNUM=PIC_ZXFUNUM+" + bgfznum + " where PIC_PTCODE='" + ptcode + "' ";
                        //        sqltextlist.Add(sqltext);
                        //    }

                        //}
                    }
                    else if (note == "库存占用")
                    {
                        sqltext = "update TBPC_PURCHASEPLAN set PUR_NUM=PUR_NUM+" + bgnum + ",PUR_FZNUM=PUR_FZNUM+" + bgfznum + "," +
                                  "PUR_RPNUM=PUR_RPNUM+'" + bgnum + "',PUR_RPFZNUM=PUR_RPFZNUM+'" + bgfznum + "'  where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='1'";
                        sqltextlist.Add(sqltext);
                        sqltext = "update TBPC_MARSTOUSEALL set PUR_NUM=PUR_NUM+" + bgnum + ",PUR_FZNUM=PUR_FZNUM+" + bgfznum + "," +
                                  "PUR_USTNUM=PUR_USTNUM+" + bgnum + ",PUR_USTFZNUM=PUR_USTFZNUM+" + bgfznum + " where PUR_PTCODE='" + ptcode + "'";
                        sqltextlist.Add(sqltext);
                    }
                    else if (note == "相似占用")
                    {
                        sqltext = "update TBPC_PURCHASEPLAN set PUR_NUM=PUR_NUM+" + bgnum + ",PUR_FZNUM=PUR_FZNUM+" + bgfznum + "," +
                                  "PUR_RPNUM=PUR_RPNUM+'" + bgnum + "',PUR_RPFZNUM=PUR_RPFZNUM+'" + bgfznum + "'  where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='1'";
                        sqltextlist.Add(sqltext);
                        sqltext = "update TBPC_MARREPLACEALL set MP_NUM=MP_NUM+" + bgnum + ",MP_USENUM=MP_USENUM+" + bgnum + " where MP_PTCODE='" + ptcode + "'";
                        sqltextlist.Add(sqltext);
                        sqltext = "update TBPC_MARREPLACEDETAIL set MP_NEWNUMA=MP_NEWNUMA+" + bgnum + "  where MP_PTCODE='" + ptcode + "'";
                        sqltextlist.Add(sqltext);
                    }
                    sqltext = "UPDATE  TBPC_MPTEMPCHANGE  SET MP_BGZXFZNUM=MP_BGZXFZNUM-" + bgfznum + ",MP_BGZXNUM=MP_BGZXNUM-" + bgnum + "  WHERE  MP_ID=" + tb_id.Text + "";
                    sqltextlist.Add(sqltext);
                }
                sqltext = "update TBPC_MPTEMPCHANGE set MP_STATE='0',MP_CGID=''  " +
                         "WHERE  (MP_ID=" + tb_id.Text + ")";
                sqltextlist.Add(sqltext);
                sqltext = "IF not exists (SELECT * FROM TBPC_MPTEMPCHANGE where MP_STATE='0' and MP_CHPCODE='" + tb_pcode.Text + "') " +
                         "begin " +
                         "update TBPC_MPCHANGETOTAL set MP_CHSTATE='2' where MP_CHPCODE='" + tb_pcode.Text + "' " +
                         "end " +
                         "ELSE " +
                         "begin " +
                         "update TBPC_MPCHANGETOTAL set MP_CHSTATE='1' where MP_CHPCODE='" + tb_pcode.Text + "' " +
                         "end ";
                sqltextlist.Add(sqltext);
                DBCallCommon.ExecuteTrans(sqltextlist);
                tbpc_purbgdetailRepeaterdatabind();
                tbpc_purbgycldetailRepeaterdatabind();
                tbpc_purallyjhdetailRepeaterdatabind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('取消变更成功！');", true);

            }
        }
        //保存：正常采购、代用采购变更任务分给采购员执行；备库占用、代用占用直接修改占用备库量
        //正常采购、代用采购状态如果未分工直接修改采购计划，若分工由采购员执行。
        protected void btn_confirm_click(object sender, EventArgs e)
        {
            string sqltext = "";
            List<string> sqltextlist = new List<string>();
            string newpcode = tb_pcode.Text;
            string oldptcode = "";
            string newsqcode = "";
            string pjid = tb_pjid.Text;
            string pjname = tb_pjname.Text;
            string engid = tb_engid.Text;
            string engname = tb_engname.Text;
            string marid = "";
            string bgmarid = "";
            string cgid = "";
            string note = "";
            double num = 0;
            double fznum = 0;
            double bgnum = 0;
            double bgfznum = 0;
            double length = 0;
            double width = 0;
            string pur_state = "";
            string time = "";

            if (CheckBox1.Checked)
            {
                if (DropDownListBKPersom.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择【储运备库人】');", true);
                    return;
                }
            }
            double flage = save();

            if (flage == 0)//数据正确
            {
                bgmarid = tb_marid.Text;
                //材料信息repeater
                foreach (RepeaterItem reitem in tbpc_purallyjhdetailRepeater.Items)
                {
                    bgnum = ((TextBox)reitem.FindControl("BGNUM")).Text == "" ? 0 : Convert.ToDouble(((TextBox)reitem.FindControl("BGNUM")).Text);
                    bgfznum = ((TextBox)reitem.FindControl("BGFZNUM")).Text == "" ? 0 : Convert.ToDouble(((TextBox)reitem.FindControl("BGFZNUM")).Text);
                    if (bgnum != 0 || bgfznum != 0)
                    {
                        oldptcode = ((Label)reitem.FindControl("PTCODE")).Text;
                        newsqcode = ((Label)reitem.FindControl("STO")).Text;
                        marid = ((Label)reitem.FindControl("MARID")).Text;
                        num = ((Label)reitem.FindControl("RPNUM")).Text == "" ? 0 : Convert.ToDouble(((Label)reitem.FindControl("RPNUM")).Text);
                        fznum = ((Label)reitem.FindControl("RPFZNUM")).Text == "" ? 0 : Convert.ToDouble(((Label)reitem.FindControl("RPFZNUM")).Text);
                        length = ((Label)reitem.FindControl("LENGTH")).Text == "" ? 0 : Convert.ToDouble(((Label)reitem.FindControl("LENGTH")).Text);
                        width = ((Label)reitem.FindControl("WIDTH")).Text == "" ? 0 : Convert.ToDouble(((Label)reitem.FindControl("WIDTH")).Text);
                        cgid = ((Label)reitem.FindControl("CGMAN")).Text;
                        pur_state = ((Label)reitem.FindControl("STATE")).Text;
                        note = ((Label)reitem.FindControl("NOTE")).Text;
                        time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        if (Convert.ToInt32(pur_state) == 7 && (bgnum != 0 || bgfznum != 0))
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已经下订单，请输入0为变更数量！');", true);
                            return;
                        }
                        else if (gloabstate == "1")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('此变更已处理！');", true);
                            return;
                        }
                        else
                        {
                            //*************************未分工直接改变执行数量*******************

                            if (note == "正常采购" || note == "代用采购")
                            {
                                //如果未分工,则改变需用计划数量
                                if (Convert.ToInt32(pur_state) < 4)
                                {
                                    sqltext = "update TBPC_PURCHASEPLAN set PUR_NUM=PUR_NUM-" + bgnum + ",PUR_FZNUM=PUR_FZNUM-" + bgfznum + "," +
                                               "PUR_RPNUM=PUR_RPNUM-'" + bgnum + "',PUR_RPFZNUM=PUR_RPFZNUM-'" + bgfznum + "'  where PUR_PTCODE = '" + oldptcode + "' and PUR_CSTATE='0'";
                                    sqltextlist.Add(sqltext);
                                }
                                //更新变更明细表
                                    sqltext = "insert into TBPC_MPCHANGEDETAIL(MP_CHPCODE,MP_CHPTCODE,MP_OLDPTCODE,MP_MARID," +
                                             "MP_BGFZNUM,MP_BGNUM,MP_BGZXNUM,MP_BGZXFZNUM,MP_TIME,MP_LENGTH,MP_WIDTH,MP_STATE,MP_NOTE,MP_CGID) " +
                                             "values('" + newpcode + "','" + tb_ptcode.Text + "','" + oldptcode + "','" + marid + "'," +
                                             bgfznum + "," + bgnum + "," + bgnum + "," + bgfznum + ",'" + time + "'," + length + "," + width + ",'1','" + note + "','" + Session["UserID"].ToString() + "')";
                                    sqltextlist.Add(sqltext);
                                //**************已经生成代用单，修改代用单数量******************
                                if (Convert.ToInt32(pur_state) == 5)
                                {
                                    sqltext = "update TBPC_MARREPLACEALL set MP_NUM=MP_NUM-" + bgnum + ",MP_USENUM=MP_USENUM-" + bgnum + " where MP_PTCODE = '" + oldptcode + "'";
                                    sqltextlist.Add(sqltext);
                                    sqltext = "update TBPC_MARREPLACEDETAIL set MP_NEWNUMA=MP_NEWNUMA-" + bgnum + "  where MP_PTCODE = '" + oldptcode + "'";
                                    sqltextlist.Add(sqltext);
                                }

                                //**************已经生成比价单，修改比价单订单数量******************
                                //if (Convert.ToInt32(pur_state) >= 6)
                                //{
                                //    sqltext = "select PIC_PTCODE from TBPC_IQRCMPPRICE where PIC_PTCODE='" + oldptcode + "'";
                                //    DataTable dt10 = DBCallCommon.GetDTUsingSqlText(sqltext);
                                //    if (dt10.Rows.Count == 1)
                                //    {
                                //        sqltext = "update TBPC_IQRCMPPRICE set PIC_QUANTITY=PIC_QUANTITY-" + bgnum + ",PIC_FZNUM=PIC_FZNUM-" + bgfznum + "," +
                                //                    "PIC_ZXNUM=PIC_ZXNUM-" + bgnum + ",PIC_ZXFUNUM=PIC_ZXFUNUM-" + bgfznum + " where PIC_PTCODE='" + oldptcode + "' ";
                                //        sqltextlist.Add(sqltext);
                                //    }

                                //}

                            }
                            //*********备库直接执行*********************
                            else if (note == "库存占用")
                            {
                                sqltext = "update TBPC_PURCHASEPLAN set PUR_NUM=PUR_NUM-" + bgnum + ",PUR_FZNUM=PUR_FZNUM-" + bgfznum + "," +
                                          "PUR_RPNUM=PUR_RPNUM-'" + bgnum + "',PUR_RPFZNUM=PUR_RPFZNUM-'" + bgfznum + "'  where PUR_PTCODE = '" + oldptcode + "' and PUR_CSTATE='1'";
                                sqltextlist.Add(sqltext);
                                sqltext = "insert into TBPC_MPCHANGEDETAIL(MP_CHPCODE,MP_CHPTCODE,MP_OLDPTCODE,MP_MARID," +
                                          "MP_BGFZNUM,MP_BGNUM,MP_BGZXNUM,MP_BGZXFZNUM,MP_CGID,MP_TIME,MP_LENGTH,MP_WIDTH,MP_STATE,MP_NOTE) " +
                                          "values('" + newpcode + "','" + tb_ptcode.Text + "','" + oldptcode + "','" + marid + "'," +
                                          bgfznum + "," + bgnum + "," + bgnum + "," + bgfznum + ",'" + Session["UserID"].ToString() + "','" +
                                          time + "'," + length + "," + width + ",'1','" + note + "')";
                                sqltextlist.Add(sqltext);
                                //*********修改备库占用的数量信息*******************
                                sqltext = "update TBPC_MARSTOUSEALL set PUR_NUM=PUR_NUM-" + bgnum + ",PUR_FZNUM=PUR_FZNUM-" + bgfznum + "," +
                                          "PUR_USTNUM=PUR_USTNUM-" + bgnum + ",PUR_USTFZNUM=PUR_USTFZNUM-" + bgfznum + " where PUR_PTCODE = '" + oldptcode + "' ";
                                sqltextlist.Add(sqltext);
                                sqltext = "update TBPC_MARSTOUSEALLDETAIL set PUR_NUM=PUR_NUM-" + bgnum + ",PUR_FZNUM=PUR_FZNUM-" + bgfznum + "," +
                                          "PUR_USTNUM=PUR_USTNUM-" + bgnum + ",PUR_USTFZNUM=PUR_USTFZNUM-" + bgfznum + " where PUR_PTCODE = '" + oldptcode + "' ";
                                sqltextlist.Add(sqltext);

                            }
                            else if (note == "相似占用")
                            {
                                sqltext = "update TBPC_PURCHASEPLAN set PUR_NUM=PUR_NUM-" + bgnum + ",PUR_FZNUM=PUR_FZNUM-" + bgfznum + "," +
                                          "PUR_RPNUM=PUR_RPNUM-'" + bgnum + "',PUR_RPFZNUM=PUR_RPFZNUM-'" + bgfznum + "'  where PUR_PTCODE = '" + oldptcode + "' and PUR_CSTATE='1'";
                                sqltextlist.Add(sqltext);
                                sqltext = "insert into TBPC_MPCHANGEDETAIL(MP_CHPCODE,MP_CHPTCODE,MP_OLDPTCODE,MP_MARID," +
                                          "MP_BGFZNUM,MP_BGNUM,MP_BGZXNUM,MP_BGZXFZNUM,MP_CGID,MP_TIME,MP_LENGTH,MP_WIDTH,MP_STATE,MP_NOTE) " +
                                          "values('" + newpcode + "','" + tb_ptcode.Text + "','" + oldptcode + "','" + marid + "'," +
                                          bgfznum + "," + bgnum + "," + bgnum + "," + bgfznum + ",'" + Session["UserID"].ToString() + "','" +
                                          time + "'," + length + "," + width + ",'1','" + note + "')";
                                sqltextlist.Add(sqltext);
                                //代用单表中有代用和相似占用的数据
                                //**********************改变代用单表中数据*********************
                                sqltext = "update TBPC_MARREPLACEALL set MP_NUM=MP_NUM-" + bgnum + ",MP_USENUM=MP_USENUM-" + bgnum + " where MP_PTCODE = '" + oldptcode + "'";
                                sqltextlist.Add(sqltext);
                                sqltext = "update TBPC_MARREPLACEDETAIL set MP_NEWNUMA=MP_NEWNUMA-" + bgnum + "  where MP_PTCODE = '" + oldptcode + "'";
                                sqltextlist.Add(sqltext);
                            }

                            sqltext = "UPDATE  TBPC_MPTEMPCHANGE  " +
                                      "SET MP_BGZXFZNUM=MP_BGZXFZNUM+" + bgfznum + ",MP_BGZXNUM=MP_BGZXNUM+" + bgnum +
                                      "  WHERE  (MP_ID=" + tb_id.Text + ")";
                            sqltextlist.Add(sqltext);
                        }

                    }
                }
                sqltext = "update TBPC_MPTEMPCHANGE set MP_STATE='1',MP_CGID='" + Session["UserID"].ToString() + "'  " +
                          "WHERE  (MP_ID=" + tb_id.Text + ")";
                sqltextlist.Add(sqltext);
                sqltext = "IF not exists (SELECT * FROM TBPC_MPTEMPCHANGE where MP_STATE='0' and MP_CHPCODE='" + tb_pcode.Text + "') " +
                          "begin " +
                          "update TBPC_MPCHANGETOTAL set MP_CHSTATE='2' where MP_CHPCODE='" + tb_pcode.Text + "' " +
                          "end " +
                          "ELSE " +
                          "begin " +
                          "update TBPC_MPCHANGETOTAL set MP_CHSTATE='1' where MP_CHPCODE='" + tb_pcode.Text + "' " +
                          "end ";
                sqltextlist.Add(sqltext);
                if (CheckBox1.Checked) //选择通知储运备库
                {
                    //插入提交人，到储运
                    sqltext = "update TBPC_MPTEMPCHANGE set MP_EXECSTATE='1',MP_SUBID='" + Session["UserID"].ToString() + "',MP_SUBTIME='" + System.DateTime.Now.ToString("yyyy-MM-dd") + "',MP_EXECID='" + DropDownListBKPersom.SelectedValue + "' " +
                     "where MP_ID='" + tb_id.Text + "'";
                    sqltextlist.Add(sqltext);
                }
                DBCallCommon.ExecuteTrans(sqltextlist);
                Response.Redirect("PC_TBPC_Purchange_all.aspx?chpcd=" + tb_pcode.Text);
                //Response.Redirect("~/PC_Data/PC_TBPC_Purchange_all_detail.aspx?mpid=" + mpid + "&bgptcode=" + bgptcode + "&pcode=" + TextBox_pid.Text + "");
            }
            else if (flage == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('变更执行数量大于采购数量！');", true);
            }
            else if (flage == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('变更执行数量大于变更数量！');", true);
            }

        }
        protected void btn_back_click(object sender, EventArgs e)
        {
            Response.Redirect("PC_TBPC_Purchange_all.aspx?chpcd=" + tb_pcode.Text);
        }

        //repeater中数量和辅助数量汇总
        private double sum11 = 0;
        private double sum12 = 0;
        private double sum13 = 0;
        private double sum14 = 0;
        protected void tbpc_purbgdetailRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("BG_FZNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("BG_FZNUM")).Text.ToString() == System.String.Empty)
                {
                    ((Label)e.Item.FindControl("BG_FZNUM")).Text = "0";
                }
                sum11 += Convert.ToDouble(((Label)e.Item.FindControl("BG_FZNUM")).Text);
                if (((Label)e.Item.FindControl("BG_NUM")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("BG_NUM")).Text.ToString() == System.String.Empty)
                {
                    ((Label)e.Item.FindControl("BG_NUM")).Text = "0";
                }
                sum12 += Convert.ToDouble(((Label)e.Item.FindControl("BG_NUM")).Text);
                if (((Label)e.Item.FindControl("BG_ZXNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("BG_ZXNUM")).Text.ToString() == System.String.Empty)
                {
                    ((Label)e.Item.FindControl("BG_ZXNUM")).Text = "0";
                }
                sum13 += Convert.ToDouble(((Label)e.Item.FindControl("BG_ZXNUM")).Text);
                if (((Label)e.Item.FindControl("BG_ZXFZNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("BG_ZXFZNUM")).Text.ToString() == System.String.Empty)
                {
                    ((Label)e.Item.FindControl("BG_ZXFZNUM")).Text = "0";
                }
                sum14 += Convert.ToDouble(((Label)e.Item.FindControl("BG_ZXFZNUM")).Text);
                if (gloabstate == "0")
                {
                    ((Label)e.Item.FindControl("BG_STATE")).Text = "未执行";
                }
                else
                {
                    ((Label)e.Item.FindControl("BG_STATE")).Text = "已执行";
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                ((TextBox)(e.Item.FindControl("tb_bgnum"))).Text = Convert.ToString(sum12);
                ((TextBox)(e.Item.FindControl("tb_bgfznum"))).Text = Convert.ToString(sum11);
                ((TextBox)(e.Item.FindControl("tb_zxnum"))).Text = Convert.ToString(sum13);
                ((TextBox)(e.Item.FindControl("tb_zxfznum"))).Text = Convert.ToString(sum14);
            }
        }

        //repeater中数量和辅助数量汇总
        private double sum21 = 0;
        private double sum22 = 0;
        private double sum23 = 0;
        private double sum24 = 0;
        protected void tbpc_purallyjhdetailRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                if (gloabstate == "1")
                {
                    ((TextBox)e.Item.FindControl("BGNUM")).Enabled = false;
                    ((TextBox)e.Item.FindControl("BGFZNUM")).Enabled = false;
                }

                if (((Label)e.Item.FindControl("RPNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("RPNUM")).Text.ToString() == System.String.Empty)
                {
                    ((Label)e.Item.FindControl("RPNUM")).Text = "0";
                }
                sum21 += Convert.ToDouble(((Label)e.Item.FindControl("RPNUM")).Text);
                if (((Label)e.Item.FindControl("RPFZNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("RPFZNUM")).Text.ToString() == System.String.Empty)
                {
                    ((Label)e.Item.FindControl("RPFZNUM")).Text = "0";
                }
                sum22 += Convert.ToDouble(((Label)e.Item.FindControl("RPFZNUM")).Text);
                if (((TextBox)e.Item.FindControl("BGNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("BGNUM")).Text.ToString() == System.String.Empty)
                {
                    ((TextBox)e.Item.FindControl("BGNUM")).Text = "0";
                }
                sum23 += Convert.ToDouble(((TextBox)e.Item.FindControl("BGNUM")).Text);
                if (((TextBox)e.Item.FindControl("BGFZNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("BGFZNUM")).Text.ToString() == System.String.Empty)
                {
                    ((TextBox)e.Item.FindControl("BGFZNUM")).Text = "0";
                }
                sum24 += Convert.ToDouble(((TextBox)e.Item.FindControl("BGFZNUM")).Text);

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                ((TextBox)(e.Item.FindControl("tb_jhtotal1"))).Text = Convert.ToString(sum21);
                ((TextBox)(e.Item.FindControl("tb_jhtotal2"))).Text = Convert.ToString(sum22);
                ((TextBox)(e.Item.FindControl("tb_jhtotal3"))).Text = Convert.ToString(sum23);
                ((TextBox)(e.Item.FindControl("tb_jhtotal4"))).Text = Convert.ToString(sum24);
            }
        }
        protected double save()//判断数据是否正确
        {
            double flage = 0;
            //第一个变更信息repeater中数据
            TextBox tbjsbgnum = tbpc_purbgdetailRepeater.Controls[tbpc_purbgdetailRepeater.Controls.Count - 1].FindControl("tb_bgnum") as TextBox;//汇总的变更数量
            TextBox tbjsbgweight = tbpc_purbgdetailRepeater.Controls[tbpc_purbgdetailRepeater.Controls.Count - 1].FindControl("tb_bgfznum") as TextBox;//汇总的变更辅助数量
            //材料信息repeater中数据
            TextBox tbtcgrpnum = tbpc_purallyjhdetailRepeater.Controls[tbpc_purallyjhdetailRepeater.Controls.Count - 1].FindControl("tb_jhtotal1") as TextBox;//汇总的执行数量
            TextBox tbtcgrpweight = tbpc_purallyjhdetailRepeater.Controls[tbpc_purallyjhdetailRepeater.Controls.Count - 1].FindControl("tb_jhtotal2") as TextBox;//汇总的执行辅助数量
            TextBox tbtcgbgnum = tbpc_purallyjhdetailRepeater.Controls[tbpc_purallyjhdetailRepeater.Controls.Count - 1].FindControl("tb_jhtotal3") as TextBox;//汇总的变更数量
            TextBox tbtcgbgweight = tbpc_purallyjhdetailRepeater.Controls[tbpc_purallyjhdetailRepeater.Controls.Count - 1].FindControl("tb_jhtotal4") as TextBox;//汇总的变更辅助数量

            double jsbgnum = Math.Abs(Convert.ToDouble(tbjsbgnum.Text));
            double jsbgweight = Math.Abs(Convert.ToDouble(tbjsbgweight.Text));

            double tcgrpnum = Convert.ToDouble(tbtcgrpnum.Text);
            double tcgrpweight = Convert.ToDouble(tbtcgrpweight.Text);

            double tcgbgnum = Convert.ToDouble(tbtcgbgnum.Text);
            double tcgbgweight = Convert.ToDouble(tbtcgbgweight.Text);

            if (tcgbgnum > tcgrpnum || tcgbgweight > tcgrpweight)
            {
                flage = 1;//变更执行数量大于采购计划数量
            }
            if (tcgbgnum > jsbgnum || tcgbgweight > jsbgweight)
            {
                flage = 2;//变更执行数量，大于变更数量
            }
            return flage;
        }
        public string get_pur_bjd(string i)
        {
            string statestr = "";
            if (i != "")
            {
                statestr = "是";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }
        public string get_pur_dd(string i)
        {
            string statestr = "";
            if (i != "")
            {
                statestr = "是";
            }
            else
            {

                statestr = "否";

            }
            return statestr;
        }

        public string get_bjstate(string i)
        {
            string statestr = "";
            if (i == "6")
            {
                statestr = "<span style='color: Red'>是</span>";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }

        public string get_ddstate(string i)
        {
            string statestr = "";
            if (i == "7")
            {
                statestr = "<span style='color: Red'>是</span>";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }

        protected void tbpc_purbgyclRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string PID = "";
            string cstate = "";
            string mashape = "";
            string state = "";
            string bjdsheetno = "";
            string ddsheetno = "";
            string ptcode = "";
            string marid = "";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PID = ((Label)e.Item.FindControl("PUR_PCODE")).Text;
                cstate = ((Label)e.Item.FindControl("PUR_CSTATE")).Text;
                state = ((Label)e.Item.FindControl("PUR_STATE")).Text;
                ptcode = ((Label)e.Item.FindControl("PUR_PTCODE")).Text;
                if (Convert.ToInt32(state) >= 6)
                {
                    bjdsheetno = ((Label)e.Item.FindControl("PIC_SHEETNO")).Text;
                    if (bjdsheetno != "")
                    {
                        ((Label)e.Item.FindControl("PUR_BJD")).ForeColor = System.Drawing.Color.Red;
                        ((HyperLink)e.Item.FindControl("Hypbjd")).Attributes.Add("onClick", "javascript:window.open('TBPC_IQRCMPPRCLST_checked_detail.aspx?sheetno=" + bjdsheetno + "&ptc=" + ptcode + "')");
                    }
                }
                if (Convert.ToInt32(state) >= 7)
                {
                    ddsheetno = ((Label)e.Item.FindControl("PO_SHEETNO")).Text;
                    if (ddsheetno != "")
                    {
                        ((Label)e.Item.FindControl("PUR_ORDER")).ForeColor = System.Drawing.Color.Red;
                        ((HyperLink)e.Item.FindControl("Hyporder")).Attributes.Add("onClick", "javascript:window.open('PC_TBPC_PurOrder.aspx?orderno=" + ddsheetno + "&ptc=" + ptcode + "')");
                    }

                }
                if (Convert.ToInt32(cstate) == 1)
                {
                    PID = ((Label)e.Item.FindControl("PUR_PCODE")).Text;
                    mashape = ((Label)e.Item.FindControl("PUR_MASHAPE")).Text;
                    ((Label)e.Item.FindControl("PUR_CSTATE")).ForeColor = System.Drawing.Color.Red;
                    ((HyperLink)e.Item.FindControl("Hyporder")).Attributes.Add("onClick", "javascript:window.open('PC_Date_closemarshow.aspx?orderno=" + PID + "&shape=" + mashape + "')");
                }
            }
        }

        public string get_pur_cst(string i)
        {
            string statestr = "";
            if (Convert.ToInt32(i) == 1)
            {
                statestr = "是";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }
        public string get_pur_state(string i)
        {
            string statestr = "";
            int state = Convert.ToInt32(i);
            if (state < 4)
            {
                statestr = "未分工";
            }
            else if (state == 4)
            {
                statestr = "已分工";
            }
            else if (state == 5)
            {
                statestr = "代用审核中";
            }
            else if (state == 6)
            {
                statestr = "询比价";
            }
            else if (state == 7)
            {
                statestr = "订单";
            }
            return statestr;
        }
    }
}
