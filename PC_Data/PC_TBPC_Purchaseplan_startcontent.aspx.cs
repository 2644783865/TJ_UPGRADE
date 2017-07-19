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
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchaseplan_startcontent : BasicPage
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
        public string gloabshape
        {
            get
            {
                object str = ViewState["gloabshape"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabshape"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_ph.Attributes.Add("OnClick", "Javascript:return confirm('是否确定下推?\\r下推之后的物料将不能进行相似代用！！');");
            QueryButton.Attributes.Add("OnClick", "Javascript:return confirm('是否确定行驳回?\\r驳回之后将在采购模块中删除本批计划！！');");
            if (!IsPostBack)
            {
                btn_nosto.Attributes.Add("style", "display:none");
                initpagemess();
                initpower();
                if (gloabstate == "5")
                {
                    radio_WXT.Checked = false;
                    radio_YXT.Checked = true;
                    btn_ph.Enabled = false;
                }
                Purchaseplan_startcontentRepeaterdatabind();
            }

           // CheckUser(ControlFinder);
        }
        private void initpagemess()
        {
            if (Request.QueryString["ptc"] != null)
            {
                gloabptc = Server.UrlDecode(Request.QueryString["ptc"].ToString());
            }
            else
            {
                gloabptc = "";
            }
            if (Request.QueryString["mp_id"] != null)
            {
                gloabsheetno = Server.UrlDecode(Request.QueryString["mp_id"].ToString());
            }
            else
            {
                gloabsheetno = "";
            }
            if (Request.QueryString["shape"] != null)
            {
                gloabshape = Server.UrlDecode(Request.QueryString["shape"].ToString());
            }
            else
            {
                gloabshape = "";
            }

            Tb_mpid.Text = gloabsheetno;
            string sqltext = "";
            sqltext = "select  pjid,depid,depnm,reviewaid," +
                      "reviewanm,reviewatime,sqrid,sqrnm,sqrtime,fzrid,fzrnm,prnote," +
                      "prstate  from View_TBPC_PCHSPLANRVW  where planno='" + Tb_mpid.Text + "'";
            SqlDataReader dr0 = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr0.Read())
            {
                tb_dep.Text = dr0["depnm"].ToString();
                tb_depid.Text = dr0["depid"].ToString();
                tb_pj.Text = dr0["pjid"].ToString();
                tb_pjid.Text = dr0["pjid"].ToString();
                tb_pjinfo.Text = dr0["pjid"].ToString();
                tb_eng.Text = "";
                tb_engid.Text = "";
                tb_enginfo.Text = "";
                Tb_fuziren.Text = dr0["fzrnm"].ToString();
                Tb_fuzirenid.Text = dr0["fzrid"].ToString();
                tb_note.Text = dr0["prnote"].ToString();
                TextBoxexecutor.Text = dr0["sqrnm"].ToString();
                TextBoxexecutorid.Text = dr0["sqrid"].ToString();
                Tb_shijian.Text = dr0["sqrtime"].ToString();
                Tb_shenqingren.Text = dr0["sqrnm"].ToString();
                Tb_shenqingrenid.Text = dr0["sqrid"].ToString();
                gloabstate = dr0["prstate"].ToString();
            }
            dr0.Close();
        }

        protected void radio_WXT_CheckedChanged(object sender, EventArgs e)
        {
            Purchaseplan_startcontentRepeaterdatabind();
            initpower();
            btn_hclose.Enabled = true;
        }
        protected void radio_YXT_CheckedChanged(object sender, EventArgs e)
        {
            btn_hclose.Enabled = false;
            btn_zy.Enabled = false;
            btn_rep.Enabled = false;
            btn_ph.Enabled = false;
            Purchaseplan_startcontentRepeaterdatabind();
        }
        private void initpower()
        {
            if (gloabstate == "0")//初始化
            {
                btn_hclose.Enabled = true;
                btn_zy.Enabled = true;
                btn_rep.Enabled = true;
                btn_ph.Enabled = true;
            }
            else if (gloabstate == "1")
            {
                btn_zy.Enabled = false;
                btn_rep.Enabled = true;
                btn_ph.Enabled = true;
            }
            else if (gloabstate == "4")//审核通过可以相似代用或下推
            {
                btn_hclose.Enabled = true;
                btn_zy.Enabled = false;
                btn_rep.Enabled = true;
                btn_ph.Enabled = true;
            }
            else if (gloabstate == "5")//已下推不能代用、相似代用或下推
            {
                btn_zy.Enabled = false;
                btn_rep.Enabled = false;
                btn_ph.Enabled = false;
                btn_hclose.Enabled = false;
            }
            else//其他情况不能代用、相似代用或下推
            {
                btn_hclose.Enabled = true;
                btn_zy.Enabled = false;
                btn_rep.Enabled = true;
                btn_ph.Enabled = true;
            }
            if (gloabshape == "标(发运)" || gloabshape == "标(组装)")
            {
                //btn_zy.Visible = true;
                //btn_rep.Visible = true;
                //btn_CKclose.Visible = false;
                //btn_hclose.Visible = false;
            }
            else
            {
                btn_zy.Visible = false;
                btn_rep.Visible = false;
                btn_ph.Enabled = true;
            }
        }
        protected void selectall_CheckedChanged(object sender, EventArgs e)
        {
            if (selectall.Checked)
            {
                foreach (RepeaterItem Reitem in Purchaseplan_startcontentRepeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        if (cbx.Enabled != false)
                        {
                            cbx.Checked = true;
                        }
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Reitem in Purchaseplan_startcontentRepeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = false;
                    }
                }
            }
        }
        protected void btn_LX_click(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;
            int start = 0;
            int finish = 0;
            int k = 0;
            foreach (RepeaterItem Reitem in Purchaseplan_startcontentRepeater.Items)
            {
                j++;
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                if (cbx.Checked)
                {
                    i++;
                    if (start == 0)
                    {
                        start = j;
                    }
                    else
                    {
                        finish = j;
                    }
                }
            }
            if (i == 2)
            {
                foreach (RepeaterItem Reitem in Purchaseplan_startcontentRepeater.Items)
                {
                    k++;
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                    if (k >= start && k <= finish)
                    {
                        cbx.Checked = true;
                    }
                    if (k > finish)
                    {
                        cbx.Checked = false;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择连续的区间！');", true);
            }
        }

        protected void btn_QX_click(object sender, EventArgs e)
        {
            foreach (RepeaterItem Reitem in Purchaseplan_startcontentRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                cbx.Checked = false;
            }
        }
        private void Purchaseplan_startcontentRepeaterdatabind()
        {
            string sqltext = "";
            sqltext = "SELECT planno, pjid, pjnm, engid, engnm, ptcode,PUR_ID,sqrnm,sqrid, " +
                           "marid, marnm, margg, marcz, margb, marunit,marfzunit,length,width,num,fznum, " +
                           "rpnum, rpfznum,jstimerq,cgrid, cgrnm, isnull(purstate,0) as purstate, purnote,PUR_MASHAPE,PUR_CSTATE,PUR_ZYDY,PUR_TUHAO  " +
                           "FROM  View_TBPC_PURCHASEPLAN_RVW   where planno='" + Tb_mpid.Text + "' and  PUR_CSTATE='0' and purstate!='8' and purstate!='9'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DataView dv = null;
            dv = dt.DefaultView;
            dv.RowFilter = "";
            dv.Sort = "ptcode";
            dt = dv.ToTable();
            if (radio_WXT.Checked)//未下推
            {
                dv = dt.DefaultView;
                dv.RowFilter = "purstate<'3'";
                dv.Sort = "ptcode";
                dt = dv.ToTable();
            }
            if (radio_YXT.Checked)//一下推
            {
                dv = dt.DefaultView;
                dv.RowFilter = "purstate>='3'";
                dv.Sort = "ptcode";
                dt = dv.ToTable();
            }
            Purchaseplan_startcontentRepeater.DataSource = dt;
            Purchaseplan_startcontentRepeater.DataBind();

        }
        //下推操作
        protected void btn_ph_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            int k = 0;
            int j = 0;
            foreach (RepeaterItem Reitem in Purchaseplan_startcontentRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    j++;
                }
            }
            if (j == 0)//没有勾选则全部下推
            {
                sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='3',PUR_XTR='" + Session["UserName"].ToString().Trim() + "',PUR_XTTIME='" + DateTime.Now.ToString().Trim() + "' where PUR_PCODE='" + Tb_mpid.Text + "' and PUR_CSTATE='0' and PUR_STATE<=3";//可以分工
                DBCallCommon.ExeSqlText(sqltext);
            }
            else//有勾选则下推勾选的
            {
                foreach (RepeaterItem Reitem in Purchaseplan_startcontentRepeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx.Checked)
                    {
                        string ptcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PTCODE")).Text.ToString();
                        sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='3',PUR_XTR='" + Session["UserName"].ToString().Trim() + "',PUR_XTTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "' where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0' and PUR_STATE<=3";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }

            string sql = "select PUR_STATE from TBPC_PURCHASEPLAN where PUR_PCODE='" + Tb_mpid.Text + "' and PUR_CSTATE='0'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    double State = Convert.ToDouble(dt.Rows[i]["PUR_STATE"].ToString());
                    if (State < 3)
                    {
                        k++;
                    }
                }
            }
            if (k == 0)
            {
                sqltext = "update TBPC_PCHSPLANRVW set PR_STATE='5' where PR_SHEETNO='" + Tb_mpid.Text + "'";
                DBCallCommon.ExeSqlText(sqltext);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已经全部下推！');", true);
                btn_zy.Enabled = false;
                btn_rep.Enabled = false;
                btn_ph.Enabled = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('还有部分物料并没有下推！\\r还可以继续下推！');", true);
            }
            Purchaseplan_startcontentRepeaterdatabind();
        }
        //占用操作
        protected void btn_zy_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            string zdid = "";
            string zdnm = "";
            string zdtm = "";
            List<string> sqltextlist = new List<string>();
            string sql = "update TBPC_PURCHASEPLAN set PUR_STATE='1' where PUR_PTCODE not in(select ptcode from View_TBPC_PURCHASEPLAN_STO where planno='" + Tb_mpid.Text + "') and PUR_PCODE='" + Tb_mpid.Text + "'";
            DBCallCommon.ExeSqlText(sql);
            sqltext = "select * from View_TBPC_PURCHASEPLAN_STO  where  planno='" + Tb_mpid.Text + "'";
            if (DBCallCommon.GetDTUsingSqlText(sqltext).Rows.Count > 0)//库存存在可以占用的物料，生成占用单号
            {
                zdid = Session["UserID"].ToString();
                zdnm = Session["UserName"].ToString();
                zdtm = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                sqltext = "insert into TBPC_MARSTOUSEALL(PUR_PCODE,PUR_PJID,PUR_ENGID," +
                          "PUR_PTCODE, PUR_MARID, PUR_LENGTH,PUR_WIDTH,PUR_NUM, PUR_FZNUM) " +
                         "select  planno, pjid, engid,ptcode, marid, length, width, num, fznum  " +
                         "from View_TBPC_PURCHASEPLAN_STO   where planno='" + Tb_mpid.Text + "'";
                sqltextlist.Add(sqltext);
                //sqltext = "update TBPC_PURCHASEPLAN set PUR_ZYDY='库存占用' where PUR_PTCODE in (select ptcode from View_TBPC_PURCHASEPLAN_STO where planno='" + Tb_mpid.Text + "') and PUR_PCODE='" + Tb_mpid.Text + "'";
                //sqltextlist.Add(sqltext);
                sqltext = "insert into TBPC_MARSTOUSETOTAL (PR_PCODE,PR_PJID,PR_ENGID,PUR_MASHAPE,PR_NOTE) select PR_SHEETNO,PR_PJID,PR_ENGID,PUR_MASHAPE,PR_NOTE from TBPC_PCHSPLANRVW where PR_SHEETNO='" + Tb_mpid.Text + "' ";
                sqltextlist.Add(sqltext);
                sqltext = "update TBPC_MARSTOUSETOTAL set PR_REVIEWA='" + zdid + "', " +
                              "PR_REVIEWATIME='" + zdtm + "',PR_STATE='1' " +
                              "where  PR_PCODE='" + Tb_mpid.Text + "'";
                sqltextlist.Add(sqltext);
                sqltext = "update TBPC_PCHSPLANRVW set PR_STATE='1' where  PR_SHEETNO='" + Tb_mpid.Text + "'";
                sqltextlist.Add(sqltext);
                DBCallCommon.ExecuteTrans(sqltextlist);
                Response.Redirect("~/PC_Data/PC_TBPC_marstouseall.aspx?pcode=" + Tb_mpid.Text);
            }
            else//否者调过该步，状态改为该批材料已通过，可以进行下步操作（相似占用）
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "test", "<script language=javascript>if(confirm('无库存量可用,确定跳过该步吗?'))document.getElementById('" + btn_nosto.ClientID + "').click();</script>");
            }
        }
        //相似代用
        protected void btn_rep_Click(object sender, EventArgs e)
        {
            if (isselected())//是否选择数据
            {
                List<string> sqltextlist = new List<string>();
                string sqltext = "";
                string repcode = generatecode();
                string plancode = gloabsheetno;
                string pjid = tb_pjid.Text;
                string pjnm = tb_pj.Text;
                string engid = tb_engid.Text;
                string engnm = tb_eng.Text;
                string fillid = Session["UserID"].ToString();
                string fillnm = Session["UserName"].ToString();
                string filltime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string reviewaid = Tb_shenqingrenid.Text;
                string reviewanm = Tb_shenqingren.Text;
                string chargeid = Tb_fuzirenid.Text;
                string chargenm = Tb_fuziren.Text;
                sqltext = "INSERT INTO TBPC_MARREPLACETOTAL(MP_CODE,MP_PLANPCODE,MP_PJID," +
                          "MP_ENGID,MP_FILLFMID,MP_FILLFMTIME,MP_REVIEWAID,MP_CHARGEID)  " +
                          "VALUES('" + repcode + "','" + plancode + "','" + pjid + "','" + engid + "','" + fillid + "'," +
                          "'" + filltime + "','" + reviewaid + "','" + chargeid + "')";
                sqltextlist.Add(sqltext);
                foreach (RepeaterItem Reitem in Purchaseplan_startcontentRepeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        if (cbx.Checked)
                        {
                            sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE='2',PUR_ZYDY='相似代用'  WHERE PUR_PTCODE='" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PTCODE")).Text + "'";//相似代用
                            sqltextlist.Add(sqltext);
                            //DBCallCommon.ExeSqlText(sqltext1);
                        }
                    }
                }
                foreach (RepeaterItem Reitem in Purchaseplan_startcontentRepeater.Items)
                {
                    string ptcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PTCODE")).Text.ToString();
                    string marid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_MARID")).Text.ToString();
                    double num = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_NUM")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_NUM")).Text);
                    double fznum = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_FZNUM")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_FZNUM")).Text);
                    double length = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_LENGTH")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_LENGTH")).Text);
                    double width = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_WIDTH")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_WIDTH")).Text);

                    //double num = Convert.ToDouble(((Label)Reitem.FindControl("PUR_NUM")).Text);
                    //double fznum = Convert.ToDouble(((Label)Reitem.FindControl("PUR_FZNUM")).Text);
                    //double length = Convert.ToDouble(((Label)Reitem.FindControl("PUR_LENGTH")).Text);
                    //double width = Convert.ToDouble(((Label)Reitem.FindControl("PUR_WIDTH")).Text);
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        if (cbx.Checked)
                        {
                            sqltext = "INSERT INTO TBPC_MARREPLACEALL(MP_CODE,MP_PTCODE,MP_MARID," +
                                           "MP_NUM,MP_FZNUM,MP_LENGTH,MP_WIDTH) " +
                                           "VALUES('" + repcode + "','" + ptcode + "','" + marid + "'," +
                                            +num + "," + fznum + "," + length + "," + width + ")";
                            sqltextlist.Add(sqltext);
                        }
                    }
                }
                DBCallCommon.ExecuteTrans(sqltextlist);
                Response.Redirect("~/PC_Data/PC_TBPC_Marreplace_all.aspx?mpcode=" + repcode);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
            }

        }
        //判断是否选择数据
        protected bool isselected()
        {
            int count = 0;
            bool temp = false;
            foreach (RepeaterItem Reitem in Purchaseplan_startcontentRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        count++;
                    }
                }
            }
            if (count > 0)
            {
                temp = true;
            }
            return temp;
        }
        //代用单号
        private string generatecode()
        {
            string repcode = "";
            string subcode = "";
            string sqltext = "SELECT TOP 1 MP_CODE FROM TBPC_MARREPLACETOTAL WHERE MP_CODE LIKE '" + DateTime.Now.Year.ToString() + "0" + "%' ORDER BY MP_CODE DESC";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                subcode = Convert.ToString(dt.Rows[0][0]).Substring(Convert.ToString(dt.Rows[0][0]).ToString().Length - 5, 5);//后五位流水号
                subcode = Convert.ToString(Convert.ToInt32(subcode) + 1);
                subcode = subcode.PadLeft(5, '0');
            }
            else
            {
                subcode = "00001";
            }
            repcode = DateTime.Now.Year.ToString() + "0" + subcode;
            return repcode;
        }
        protected void btn_back_Click(object sender, EventArgs e)//返回
        {
            if (gloabptc != "")
            {
                Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_start.aspx?ptc=" + gloabptc + "");
            }
            else
            {
                Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_start.aspx");
            }
        }

        protected void btn_nosto_Click(object sender, EventArgs e)//无库存可用
        {
            string sqltext = "";
            List<string> sqltextlist = new List<string>();
            sqltext = "update TBPC_PCHSPLANRVW set PR_STATE='4' where PR_SHEETNO='" + Tb_mpid.Text + "'";
            sqltextlist.Add(sqltext);
            sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='1' where PUR_PCODE='" + Tb_mpid.Text.ToString() + "'";
            sqltextlist.Add(sqltext);
            DBCallCommon.ExecuteTrans(sqltextlist);
            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_startcontent.aspx?mp_id=" + Tb_mpid.Text + "&shape=" + gloabshape + "");
        }
        double num = 0;
        double fznum = 0;
        double cgnum = 0;
        double cgfznum = 0;
        protected void Purchaseplan_startcontentRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string state = "";
            string bjdsheetno = "";
            string ddsheetno = "";
            string ptcode = "";
            string marid = "";
            if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlTableCell Length1 = (HtmlTableCell)e.Item.FindControl("length1");
                HtmlTableCell Width1 = (HtmlTableCell)e.Item.FindControl("width1");
                HtmlTableCell FZnum1 = (HtmlTableCell)e.Item.FindControl("fznum1");
                HtmlTableCell FZunit1 = (HtmlTableCell)e.Item.FindControl("fzunit1");
                HtmlTableCell RPfznum1 = (HtmlTableCell)e.Item.FindControl("rpfznum1");
                if (gloabshape == "非定尺板" || gloabshape == "标(发运)" || gloabshape == "标(组装)" || gloabshape == "标(工艺)")
                {
                    Length1.Visible = false;
                    Width1.Visible = false;
                    FZnum1.Visible = false;
                    FZunit1.Visible = false;
                    RPfznum1.Visible = false;
                }
                if (gloabshape == "型材")
                {
                    Length1.Visible = false;
                    Width1.Visible = false;
                }
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_NUM")).Text.ToString() == System.DBNull.Value.ToString() || ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_NUM")).Text.ToString() == System.String.Empty || ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_NUM")).Text.ToString() == "0")
                {
                    num = num + 0;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_NUM")).Text = "0";
                }
                else
                {
                    num += Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_NUM")).Text.ToString());
                }
                if (((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_FZNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_FZNUM")).Text.ToString() == System.String.Empty || ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_FZNUM")).Text.ToString() == "0")
                {
                    fznum = fznum + 0;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_FZNUM")).Text = "0";
                }
                else
                {
                    fznum += Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_FZNUM")).Text.ToString());
                }
                if (((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_RPNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_RPNUM")).Text.ToString() == System.String.Empty || ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_RPNUM")).Text.ToString() == "0")
                {
                    cgnum = cgnum + 0;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_RPNUM")).Text = "0";
                }
                else
                {
                    cgnum += Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_RPNUM")).Text.ToString());
                }
                if (((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_RPFZNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_RPFZNUM")).Text.ToString() == System.String.Empty || ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_RPFZNUM")).Text.ToString() == "0")
                {
                    cgfznum = cgfznum + 0;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_RPFZNUM")).Text = "0";
                }
                else
                {
                    cgfznum += Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_RPFZNUM")).Text.ToString());
                }
                state = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_STATE")).Text;
                //ptcode = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_PTCODE")).Text;
                marid = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_MARID")).Text;

                //string ptc = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_PTCODE")).Text;
                //string str = "select PIC_SHEETNO from TBPC_IQRCMPPRICE where PIC_PTCODE='" + ptc + "'";
                //System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(str);
                //if (dt.Rows.Count > 0)
                //{
                //    bjdsheetno = dt.Rows[0]["PIC_SHEETNO"].ToString();
                //    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_BJD")).Text = "是";
                //    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_BJD")).ForeColor = System.Drawing.Color.Red;
                //    ((HyperLink)e.Item.FindControl("Hypbjd")).NavigateUrl = "TBPC_IQRCMPPRCLST_checked_detail.aspx?sheetno=" + bjdsheetno + "&ptc=" + ptcode + "";
                //}
                //else
                //{
                //    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_BJD")).Text = "否";
                //}

                //if (Convert.ToInt32(state) >= 7)
                //{
                //    ddsheetno = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PO_SHEETNO")).Text;
                //    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_DD")).ForeColor = System.Drawing.Color.Red;
                //    ((HyperLink)e.Item.FindControl("Hypdd")).NavigateUrl = "PC_TBPC_PurOrder.aspx?orderno=" + ddsheetno + "&ptc=" + ptcode + "";
                //}
                HtmlTableCell Length2 = (HtmlTableCell)e.Item.FindControl("length2");
                HtmlTableCell Width2 = (HtmlTableCell)e.Item.FindControl("width2");
                HtmlTableCell FZnum2 = (HtmlTableCell)e.Item.FindControl("fznum2");
                HtmlTableCell FZunit2 = (HtmlTableCell)e.Item.FindControl("fzunit2");
                HtmlTableCell RPfznum2 = (HtmlTableCell)e.Item.FindControl("rpfznum2");
                if (gloabshape == "非定尺板" || gloabshape == "标(发运)" || gloabshape == "标(组装)" || gloabshape == "标(工艺)")
                {
                    Length2.Visible = false;
                    Width2.Visible = false;
                    FZnum2.Visible = false;
                    FZunit2.Visible = false;
                    RPfznum2.Visible = false;
                }
                if (gloabshape == "型材")
                {
                    Length2.Visible = false;
                    Width2.Visible = false;
                }

                //库存数目
                string sql_kc = "select sum(Number) from View_SM_Storage where MaterialCode='" + marid + "' and (SQCODE like '%备库%' or SQCODE like '%BEIKU%')";
                System.Data.DataTable dtkc = DBCallCommon.GetDTUsingSqlText(sql_kc);
                System.Web.UI.WebControls.Label lbkc = (System.Web.UI.WebControls.Label)e.Item.FindControl("SM_KUCUN");
                if (dtkc.Rows.Count == 0)
                {
                    lbkc.Text = "0.0000";
                }
                else
                {
                    lbkc.Text = dtkc.Rows[0][0].ToString();
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {

                ((System.Web.UI.WebControls.Label)(e.Item.FindControl("lb_fznum"))).Text = Convert.ToString(fznum);
                ((System.Web.UI.WebControls.Label)(e.Item.FindControl("lb_cgnum"))).Text = Convert.ToString(cgnum);
                ((System.Web.UI.WebControls.Label)(e.Item.FindControl("lb_cgfznum"))).Text = Convert.ToString(cgfznum);
                HtmlTableCell Lenwid = (HtmlTableCell)e.Item.FindControl("lenwid");
                HtmlTableCell Ftfznum = (HtmlTableCell)e.Item.FindControl("ftfznum");
                HtmlTableCell Ftfzunit = (HtmlTableCell)e.Item.FindControl("ftfzunit");
                HtmlTableCell Ftcgfznum = (HtmlTableCell)e.Item.FindControl("ftcgfznum");
                if (gloabshape == "非定尺板" || gloabshape == "标(发运)" || gloabshape == "标(组装)" || gloabshape == "标(工艺)")
                {
                    Lenwid.ColSpan = 10;
                    Ftfznum.Visible = false;
                    Ftfzunit.Visible = false;
                    Ftcgfznum.Visible = false;
                }
                if (gloabshape == "型材")
                {
                    Lenwid.ColSpan = 10;
                }
            }
        }
        public string get_pur_bjd(string i)
        {
            string statestr = "";
            if (Convert.ToInt32(i) >= 6)
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
            if (Convert.ToInt32(i) >= 7)
            {
                statestr = "是";
            }
            else
            {

                statestr = "否";

            }
            return statestr;
        }

        protected void btn_xiacha_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sqltext = "";
            string piccode = "";
            string ptcode = "";
            foreach (RepeaterItem Reitem in Purchaseplan_startcontentRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                    ptcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PTCODE")).Text;
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
                return;
            }
            else if (i > 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择一条记录！');", true);
                return;
            }
            else
            {
                sqltext = "select picno from View_TBPC_IQRCMPPRICE where ptcode like '" + ptcode + "%'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                sqltext = "select PO_CODE from TBPC_PURORDERDETAIL where PO_PTCODE like '" + ptcode + "%'";
                System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    piccode = dt.Rows[0]["picno"].ToString();
                    Response.Redirect("~/PC_Data/TBPC_IQRCMPPRCLST_checked_detail.aspx?ptc=" + Server.UrlEncode(ptcode) + "&sheetno=" + piccode + "");
                }
                else if (dt1.Rows.Count > 0)
                {
                    string pocode = dt1.Rows[0]["PO_CODE"].ToString();
                    Response.Redirect("~/PC_Data/PC_TBPC_PurOrder.aspx?ptc=" + Server.UrlEncode(ptcode) + "&orderno=" + pocode + "");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无关联单据！');", true);
                }
            }
        }

        //判断是否选择数据
        protected int isselectedtrue()
        {
            int temp = 0;
            foreach (RepeaterItem Reitem in Purchaseplan_startcontentRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        temp++;
                    }
                }
            }
            return temp;
        }

        protected void btn_CKclose_Click(object sender, EventArgs e)
        {
            int j = 0;
            string ptcode = "";
            string jhgzhbg="";
            string sqltextbg="";
            foreach (RepeaterItem Reitem in Purchaseplan_startcontentRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    j++;
                    ptcode += ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_ID")).Text + ",";
                    jhgzhbg = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PTCODE")).Text.Trim();
                    sqltextbg = "select * from TBPC_BG where BG_PTC='" + jhgzhbg.ToString().Trim() + "' and RESULT!='已执行' and RESULT!='已驳回'";
                    System.Data.DataTable dtbg=DBCallCommon.GetDTUsingSqlText(sqltextbg);
                    if(dtbg.Rows.Count>0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('有物料正在变更中，无法关闭!');", true);
                        return;
                    }
                }
            }
            if (j == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择数据！');", true);
            }
            else
            {
                ptcode = ptcode.Substring(0, ptcode.LastIndexOf(","));
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "Showclose('" + ptcode + "');", true);
                //Response.Write("<script>javascript:window.open('PC_Data_hangclose.aspx?shape=" + shape + "&orderno=" + pcode2 + "&arry=" + ptcode + "'); </script>");
                Response.Redirect("~/PC_Data/PC_Data_hangclose.aspx?num=1&shape=" + Server.HtmlEncode(gloabshape) + "&orderno=" + gloabsheetno + "&arry=" + ptcode + "");
            }

        }

        protected void Btn_daochu_Click(object sender, EventArgs e)
        {
            string sqltext = "SELECT margb,marnm+(case when margg is null then '' else margg end),marcz,PUR_MASHAPE,length,width,num,marunit,fznum,marfzunit,PUR_ZYDY,purnote FROM  View_TBPC_PURCHASEPLAN_RVW  where planno='" + Tb_mpid.Text.ToString() + "' ";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string sqltext1 = "SELECT top 1 ptcode FROM  View_TBPC_PURCHASEPLAN_RVW  where planno='" + Tb_mpid.Text.ToString() + "' ";
            System.Data.DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext1);
            string name = dt2.Rows[0]["ptcode"].ToString();
            string filename = name.Split('_')[0].ToString() + '_' + name.Split('_')[1].ToString() + '_' + name.Split('_')[2].ToString() + '_' + name.Split('_')[3].ToString() + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("材料需用计划模版.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ICellStyle style2 = wk.CreateCellStyle();
                style2.BorderBottom = NPOI.SS.UserModel.BorderStyle.THIN;
                style2.BorderLeft = NPOI.SS.UserModel.BorderStyle.THIN;
                style2.BorderRight = NPOI.SS.UserModel.BorderStyle.THIN;
                style2.BorderTop = NPOI.SS.UserModel.BorderStyle.THIN;
                ISheet sheet0 = wk.GetSheetAt(0);
                string sql = "select CM_CONTR,pjid,planno,PR_CHILDENGNAME,sqrnm+'/'+sqrtime as stuff,fzrnm,PR_MAP FROM  View_TBPC_PCHSPLANRVW_1  where planno='" + Tb_mpid.Text.ToString() + "'";
                System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                ////填充数据
                IRow row2 = sheet0.GetRow(2);
                row2.GetCell(2).SetCellValue(dt1.Rows[0]["pjid"].ToString());
               
                row2.GetCell(5).SetCellValue(dt1.Rows[0]["CM_CONTR"].ToString());
                row2.GetCell(9).SetCellValue(dt1.Rows[0]["stuff"].ToString());
                IRow row3 = sheet0.GetRow(3);
                row3.GetCell(2).SetCellValue(dt1.Rows[0]["planno"].ToString());
                row3.GetCell(5).SetCellValue(dt1.Rows[0]["PR_CHILDENGNAME"].ToString());
                row3.GetCell(9).SetCellValue(dt1.Rows[0]["fzrnm"].ToString());
                IRow row4 = sheet0.GetRow(4);
                row4.GetCell(2).SetCellValue(dt1.Rows[0]["PR_MAP"].ToString());
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 6);
                    row.CreateCell(0).SetCellValue(i + 1);
                    row.Cells[0].CellStyle = style2;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                        row.Cells[j+1].CellStyle = style2;
                    }
                }
               
                for (int i = 0; i <= dt.Columns.Count; i++)
                {
                    sheet0.AutoSizeColumn(i);
                }
                sheet0.ForceFormulaRecalculation = true;

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        private static void CloseExeclProcess(Application excelApp)
        {
            #region kill excel process

            System.Diagnostics.Process[] procs = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            foreach (System.Diagnostics.Process p in procs)
            {
                int baseAdd = p.MainModule.BaseAddress.ToInt32();
                //oXL is Excel.ApplicationClass object 
                if (baseAdd == excelApp.Hinstance)
                {
                    p.Kill();
                    break;
                }
            }
            #endregion
        }
        protected void QueryButton_Click(object sender, EventArgs e)
        {
            if (TM_Data.TM_BasicFun.PurRejectJudge(Tb_mpid.Text.Trim()) == "1")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该批计划中，技术部已发生变更，无法驳回！！！');", true);
                return;
            }
            List<string> sqltextlist = new List<string>();
            string ptcode = "";
            string state = "";
            int j = 0;
            string sql = "select PUR_PTCODE,PUR_STATE from TBPC_PURCHASEPLAN where PUR_PCODE='" + Tb_mpid.Text + "'";
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
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('本批计划已经下推或占用或代用或询比价或下订单，不能驳回！');", true);
            }
            else
            {
                if (tb_bhyj.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写驳回意见！');", true);
                }
                else
                {
                    string sqltext = "delete from TBPC_PCHSPLANRVW where PR_SHEETNO='" + Tb_mpid.Text + "'";
                    sqltextlist.Add(sqltext);
                    string sqltext1 = "delete from TBPC_PURCHASEPLAN where PUR_PCODE='" + Tb_mpid.Text + "'";
                    sqltextlist.Add(sqltext1);
                    string sqltext3 = "delete from TBPC_MPCHANGETOTAL where MP_CHPCODE='" + Tb_mpid.Text + "'";
                    sqltextlist.Add(sqltext3);
                    string sqltext4 = "delete from TBPC_MPTEMPCHANGE where MP_CHPCODE='" + Tb_mpid.Text + "'";
                    sqltextlist.Add(sqltext4);
                    string sqltext5 = "delete from TBPC_MPCHANGEDETAIL where MP_CHPCODE='" + Tb_mpid.Text + "'";
                    sqltextlist.Add(sqltext5);
                    string sqltext2 = "exec PRO_TM_PurPlanRejected '" + Tb_mpid.Text + "','" + tb_engid.Text + "','" + tb_bhyj.Text + "'";
                    sqltextlist.Add(sqltext2);
                    string sqltextjl = "insert into TBPC_BOHUIJL(bohuirenid,bohuirenname,bohuitime,mppcode,bohuinote) values('" + Session["UserID"].ToString().Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + Tb_mpid.Text.Trim() + "','" + tb_bhyj.Text.Trim() + "')";
                    sqltextlist.Add(sqltextjl);
                    DBCallCommon.ExecuteTrans(sqltextlist);
                    Response.Redirect("PC_TBPC_Purchaseplan_start.aspx");
                }
            }


            //发送邮件通知储运部
            string returnvalue = "";
            string body = "";
            //string zdrEmail = "";
            //string zcrEmail = "";
            string bodymx2 = "";

            //string sql0 = "select EMail from TBDS_STAFFINFO where ST_NAME='" + Session["UserName"].ToString() + "' and ST_PD='0' ";
            //System.Data.DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sql0);
            //if (dt.Rows.Count > 0)
            //{
            //    zcrEmail = dt0.Rows[0][0].ToString();
            //}

            for (int i = 0; i < Purchaseplan_startcontentRepeater.Items.Count; i++)
            {
                string ptc = (Purchaseplan_startcontentRepeater.Items[i].FindControl("PUR_PTCODE") as System.Web.UI.WebControls.Label).Text;//计划跟踪号
                //string marnm = (Purchaseplan_startcontentRepeater.Items[i].FindControl("LabelMarName") as System.Web.UI.WebControls.Label).Text;//子项名称
                //string bjnum = (Purchaseplan_startcontentRepeater.Items[i].FindControl("TextBoxPJNum") as System.Web.UI.WebControls.TextBox).Text;//报检数量
                //string jhstate = (Purchaseplan_startcontentRepeater.Items[i].FindControl("TextBoxState") as System.Web.UI.WebControls.TextBox).Text;//交货状态
                //string cont = (Purchaseplan_startcontentRepeater.Items[i].FindControl("TextBoxControlContent") as System.Web.UI.WebControls.TextBox).Text;//检查内容

                //string tuhao = (Purchaseplan_startcontentRepeater.Items[i].FindControl("TextBoxDrawingNO") as System.Web.UI.WebControls.TextBox).Text;//图号
                //string gg = (Purchaseplan_startcontentRepeater.Items[i].FindControl("TextBOXgg") as System.Web.UI.WebControls.TextBox).Text;//规格
                //string cz = (Purchaseplan_startcontentRepeater.Items[i].FindControl("TextBOXcz") as System.Web.UI.WebControls.TextBox).Text;//材质
                //string gb = (Purchaseplan_startcontentRepeater.Items[i].FindControl("TextBOXgb") as System.Web.UI.WebControls.TextBox).Text;//国标

                int k = i;
                k++;

                bodymx2 += "明细" + k + "\n" + "计划跟踪号:" + ptc + "\n";
                //    + "子项名称：" + marnm + "\n" + "图号：" + tuhao + "\n" + "规格：" + gg + "\n" + "材质：" + cz + "\n" + "国标：" + gb + "\n" + "报检数量：" + bjnum + "\n" + "交货状态：" + jhstate + "\n" + "检查内容：" + cont + "\n" + "检查地点：" + TextBoxSite.Text + "\n";

            }


            //List<string> bjEmailCC = new List<string>();
            //bjEmailCC.Add("zhangchaochen@cbmi.com.cn");
            //bjEmailCC.Add("duanyonghui@cbmi.com.cn");

            List<string> zjEmailCC = new List<string>();
            //zjEmailCC.Add("zhangchaochen@cbmi.com.cn");
            //zjEmailCC.Add("duanyonghui@cbmi.com.cn");
            //zjEmailCC.Add("liruiming@cbmi.com.cn");
            //zjEmailCC.Add("yangshuyun@cbmi.com.cn");
            //zjEmailCC.Add("wangyongchao@cbmi.com.cn");
            //zjEmailCC.Add("chenzesheng@cbmi.com.cn");

            sql = "select EMail from TBDS_STAFFINFO where ST_PD='0' and ST_CODE like '0702%' ";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    zjEmailCC.Add(dt.Rows[i][0].ToString());

                }

            }
            string to = "";
            List<string> mfEmail = null;


            //采购驳回邮件通知储运部

            body = "采购需用计划驳回" + "\n" + "项  目  为:" + tb_pjinfo.Text + "\n" + "工  程  为：" + tb_enginfo.Text + "\n" + "编 号：" + Tb_mpid.Text + "\n" + "驳 回 人：" + Session["UserName"].ToString() + "\n" + bodymx2;
            returnvalue = DBCallCommon.SendEmail(to, zjEmailCC, mfEmail, tb_enginfo.Text + "-" + Tb_mpid.Text + "/" + "数字平台需用计划驳回", body);


            if (returnvalue == "邮件已发送!")
            {
                //string jascript = @"alert('邮件发送成功!');";

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", jascript, true);

                Response.Write("<script>alert('邮件发送成功');</script>");
            }
            else if (returnvalue == "邮件发送失败!")
            {
                Response.Write("<script>alert('邮件发送不成功');</script>");

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            tb_bhyj.Text = "";
        }
    }
}
