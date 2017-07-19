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
using System.Drawing;
using System.Collections.Generic;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchange_all : BasicPage
    {
        string pcode;
        string submpid;
        public string gloabstr
        {
            get
            {
                object str = ViewState["gloabstr"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabstr"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                TextBox_pid.Text = Request.QueryString["chpcd"].ToString();//计划批号FER/KI/1.JSB MP/WPP/01 
                initpagemess();
            }
            CheckUser(ControlFinder);
        }
        private void initpagemess()
        {
            string sqltext = "";
            string pcode = TextBox_pid.Text;
            SqlDataReader dr = null;
            sqltext = "SELECT chpcode as MP_CHPCODE, pjid as MP_CHPJID,pjnm as  MP_CHPJNAME,engid as MP_CHENGID,engnm as MP_CHENGNAME, " +
                      "chsubmitid as MP_CHSUBMITNMID,chsubmitnm as MP_CHSUBMITNM,chsubmittime as MP_CHSUBMITTM,chreviewaid as MP_CHREVIEWA,chreviewanm as MP_CHREVIEWANAME, " +
                      "chreviewbid as MP_CHREVIEWB,chreviewbnm as MP_CHREVIEWBNAME,chadate as MP_CHADATE,chstate as MP_CHSTATE, " +
                      "chnote as MP_CHNOTE FROM View_TBPC_MPCHANGETOTAL where chpcode='" + pcode + "'";
            dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                tb_zh.Text = dr["MP_CHENGID"].ToString();
                tb_pjid.Text = dr["MP_CHPJID"].ToString();
                tb_pjname.Text = dr["MP_CHPJNAME"].ToString();
                tb_pjinfo.Text = tb_pjid.Text + "-" + tb_pjname.Text;
                tb_engid.Text = dr["MP_CHENGID"].ToString();
                tb_engname.Text = dr["MP_CHENGNAME"].ToString();
                tb_enginfo.Text = tb_engid.Text + "-" + tb_engname.Text;
                tb_pid.Text = dr["MP_CHSUBMITNMID"].ToString();
                tb_pname.Text = dr["MP_CHSUBMITNM"].ToString();
                Tb_shijian.Text = dr["MP_CHSUBMITTM"].ToString();
            }
            dr.Close();
            tbpc_purbgclallRepeaterdatabind();
        }
        private void tbpc_purbgyclRepeaterdatabind(string str)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText(str);
            DBCallCommon.BindRepeater(tbpc_purbgyclRepeater, str);
            if (tbpc_purbgyclRepeater.Items.Count == 0)
            {
                NoDataPanep.Visible = true;
            }
            else
            {
                NoDataPanep.Visible = false;
            }
        }
        private void tbpc_purbgclallRepeaterdatabind()//上面的那个repeater
        {

            string sqltext = "";
            pcode = TextBox_pid.Text;
            sqltext = "select MP_ID,chpcode as MP_NEWPID,chptcode as BG_PTCODE,marid as BG_MARID, marnm as BG_MARNAME,pjid as MP_PJID, " +
                      "pjnm as MP_PJNAME,engnm as MP_ENGNAME, margg as BG_MARNORM, marcz as BG_MARTERIAL, " +
                      "unit as BG_NUNIT, chnum as BG_NUM,chfznum as BG_FZNUM,length as LENGTH,width as WIDTH,chcgid as BG_ZXRENID, " +
                      "chcgnm as BG_ZXRENNM, margb as BG_GUOBIAO, chstate as BG_STATE,chnote as MP_NOTE,zxnum as BG_ZXNUM,zxfznum as BG_ZXFZNUM  " +
                      "from View_TBPC_MPTEMPCHANGE where chpcode='" + pcode + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DBCallCommon.BindRepeater(tbpc_purbgclallRepeater, sqltext);
            if (tbpc_purbgclallRepeater.Items.Count == 0)
            {
                NoDataPanebg.Visible = true;
            }
            else
            {
                NoDataPanebg.Visible = false;
            }


        }
        protected void tbpc_purbgclallRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string state = "";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                state = ((Label)e.Item.FindControl("BG_STATE")).Text;
                //if (state == "0")
                //{
                //    ((Button)e.Item.FindControl("lkb_bg")).Enabled = true;
                //}
                //else
                //{
                //    ((Button)e.Item.FindControl("lkb_bg")).Enabled = false;
                //}
            }
        }
        
        protected void CheckedChanged(object sender, EventArgs e)//当checkbox选中状态改变时，使用库存单元格状态改变
        {
            CheckBox cbx = (CheckBox)sender;//定义checkbox
            RepeaterItem Reitem = (RepeaterItem)cbx.Parent;
            if (cbx.Checked)//选中时
            {
                //((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text;
            }
            else//没有选中时
            {

            }
        }
        protected void lkb_look_Click(object sender, EventArgs e)
        {
            Button lbtn = (Button)sender;
            RepeaterItem Reitem = (RepeaterItem)lbtn.Parent;
            string pjid = tb_pjid.Text;
            string engid = tb_engid.Text;
            string marid = ((Label)Reitem.FindControl("BG_MARID")).Text;
            string pt_code = ((Label)Reitem.FindControl("BG_PTCODE")).Text;
            IEnumerable<string> tsa_id = pt_code.Split('_').Reverse<string>();
            string  tsa = tsa_id.ElementAt(2).ToString(); 
            double bglength = ((Label)Reitem.FindControl("LENGTH")).Text == "" ? 0 : Convert.ToDouble(((Label)Reitem.FindControl("LENGTH")).Text);
            double bgwidth = ((Label)Reitem.FindControl("WIDTH")).Text == "" ? 0 : Convert.ToDouble(((Label)Reitem.FindControl("WIDTH")).Text);

            string sqltext = "SELECT planno as PUR_PCODE,pjid as PUR_PJID,pjnm as PUR_PJNAME,engid as PUR_ENGID,engnm as PUR_ENGNAME,marid as PUR_MARID," +
                             "marnm as PUR_MARNAME,margg as PUR_MARNORM,marcz as PUR_MARTERIAL,margb as PUR_GUOBIAO,num as PUR_NUM,rpnum as PUR_RPNUM," +
                             "marunit as PUR_NUNIT,length as PUR_LENGTH,width as PUR_WIDTH,ptcode as PUR_PTCODE,purnote as PUR_NOTE,cgrid as PUR_CGMAN,picno,orderno," +
                             "cgrnm as PUR_CGMANNM,purstate as PUR_STATE,PUR_CSTATE,PUR_MASHAPE  FROM  View_TBPC_PURCHASEPLAN_IRQ_ORDER   " +
                             "WHERE ptcode like '%" + tsa + "%' and marid='" + marid + "'";
            tbpc_purbgyclRepeaterdatabind(sqltext);
            foreach (RepeaterItem allreitem in tbpc_purbgclallRepeater.Items)
            {
                if (((Label)allreitem.FindControl("BG_PTCODE")).Text == pt_code)
                {
                    ((Button)allreitem.FindControl("lkb_look")).BackColor = Color.Red;
                }
                else
                {
                    ((Button)allreitem.FindControl("lkb_look")).BackColor = Color.Empty;
                }
            }
        }
        protected void lkb_bg_Click(object sender, EventArgs e)
        {
            //LinkButton lbtn = (LinkButton)sender;
            Button lbtn = (Button)sender;
            RepeaterItem Reitem = (RepeaterItem)lbtn.Parent;
            string bgptcode = ((Label)Reitem.FindControl("BG_PTCODE")).Text;//变更的计划跟踪号
            string mpid = ((Label)Reitem.FindControl("MP_ID")).Text;
            Response.Redirect("~/PC_Data/PC_TBPC_Purchange_all_detail.aspx?mpid=" + mpid + "&bgptcode=" + bgptcode + "&pcode=" + TextBox_pid.Text + "");
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
        protected string get_bg_sta(string i)
        {
            string state = "";
            if (i == "0")
            {
                state = "否";
            }
            else
            {
                state = "是";
            }
            return state;
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("PC_TBPC_Purchange_all_list.aspx");
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
        public string get_pur_bjd(string i)
        {
            string statestr = "";
            if (i!="")
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
            if (i!="")
            {
                statestr = "是";
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            tb_bhyj.Text = "";
        }

        protected void QueryButton_Click(object sender, EventArgs e)
        {
            if (TM_Data.TM_BasicFun.PurRejectJudge(TextBox_pid.Text.Trim()) == "1")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该批计划中，技术部已发生变更，无法驳回！！！');", true);
                return;
            }
            List<string> sqltextlist = new List<string>();
            string ptcode = "";
            string state = "";
            int j = 0;
            string sql = "select PUR_PTCODE,PUR_STATE from TBPC_PURCHASEPLAN where PUR_PCODE='" + TextBox_pid.Text + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ptcode = dt.Rows[i]["PUR_PTCODE"].ToString();
                    state = dt.Rows[i]["PUR_STATE"].ToString();
                    if (Convert.ToInt32(state) >= 3)
                    {
                        j++;
                    }
                    string sqlbjd = "select PIC_SHEETNO from TBPC_IQRCMPPRICE where PIC_PTCODE='" + ptcode + "'";
                    System.Data.DataTable dtbjd = DBCallCommon.GetDTUsingSqlText(sqlbjd);//比价单中有没有
                    if (dtbjd.Rows.Count > 0)
                    {
                        j++;
                    }
                    string sqldd = "select PO_CODE from TBPC_PURORDERDETAIL where PO_PTCODE='" + ptcode + "'";
                    System.Data.DataTable dtdd = DBCallCommon.GetDTUsingSqlText(sqldd);//订单中有没有
                    if (dtdd.Rows.Count > 0)
                    {
                        j++;
                    }
                    string sqlzyd = "select PUR_PCODE from TBPC_MARSTOUSEALL where PUR_PTCODE='" + ptcode + "'";
                    System.Data.DataTable dtzyd = DBCallCommon.GetDTUsingSqlText(sqlzyd);//占用单中有没有
                    if (dtzyd.Rows.Count > 0)
                    {
                        j++;
                    }
                    string sqldyd = "select MP_CODE from TBPC_MARREPLACEALL where MP_PTCODE='" + ptcode + "'";
                    System.Data.DataTable dtdyd = DBCallCommon.GetDTUsingSqlText(sqldyd);//代用单中有没有
                    if (dtdyd.Rows.Count > 0)
                    {
                        j++;
                    }
                }
            }
            if (j > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('本变更计划已经下推或占用或代用或询比价或下订单，不能驳回！');", true);
            }
            else
            {
                if (tb_bhyj.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写驳回意见！');", true);
                }
                else
                {
                    string sqltext = "delete from TBPC_PCHSPLANRVW where PR_SHEETNO='" + TextBox_pid.Text + "'";
                    sqltextlist.Add(sqltext);
                    string sqltext1 = "delete from TBPC_PURCHASEPLAN where PUR_PCODE='" + TextBox_pid.Text + "'";
                    sqltextlist.Add(sqltext1);
                    string sqltext3 = "delete from TBPC_MPCHANGETOTAL where MP_CHPCODE='" + TextBox_pid.Text + "'";
                    sqltextlist.Add(sqltext3);
                    string sqltext4 = "delete from TBPC_MPTEMPCHANGE where MP_CHPCODE='" + TextBox_pid.Text + "'";
                    sqltextlist.Add(sqltext4);
                    string sqltext5 = "delete from TBPC_MPCHANGEDETAIL where MP_CHPCODE='" + TextBox_pid.Text + "'";
                    sqltextlist.Add(sqltext5);
                    string sqltext2 = "exec PRO_TM_PurPlanRejected '" + TextBox_pid.Text + "','" + tb_engid.Text + "','" + tb_bhyj.Text + "'";
                    sqltextlist.Add(sqltext2);

                    DBCallCommon.ExecuteTrans(sqltextlist);
                    Response.Redirect("PC_TBPC_Purchange_all_list.aspx");
                }
            }
        }
    }
}

