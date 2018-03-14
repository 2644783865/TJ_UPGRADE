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
namespace ZCZJ_DPF.PC_Data
{
    public partial class TBPC_IQRCMPPRCLST_checked_detail : System.Web.UI.Page
    {
        #region  全局变量定义
        //全局变量定义

        DataTable dt = new DataTable();
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
        public string gloabflag
        {
            get
            {
                object str = ViewState["gloabflag"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabflag"] = value;
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
        public string gloabptcode
        {
            get
            {
                object str = ViewState["gloabptcode"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabptcode"] = value;
            }
        }

        public string NUM1
        {
            get
            {
                object str = ViewState["NUM1"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["NUM1"] = value;
            }
        }
        public string NUM2
        {
            get
            {
                object str = ViewState["NUM2"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["NUM2"] = value;
            }
        }
        public string Gys1
        {
            get
            {
                object str = ViewState["Gys1"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["Gys1"] = value;
            }
        }
        public string Gys2
        {
            get
            {
                object str = ViewState["Gys2"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["Gys2"] = value;
            }
        }
        public string Gys3
        {
            get
            {
                object str = ViewState["Gys3"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["Gys3"] = value;
            }
        }
        public string Gys4
        {
            get
            {
                object str = ViewState["Gys4"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["Gys4"] = value;
            }
        }
        public string Gys5
        {
            get
            {
                object str = ViewState["Gys5"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["Gys5"] = value;
            }
        }
        public string Gys6
        {
            get
            {
                object str = ViewState["Gys6"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["Gys6"] = value;
            }
        }
        public string Globgysnum
        {
            get
            {
                object str = ViewState["Globgysnum"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["Globgysnum"] = value;
            }
        }
        #endregion

        string url;
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_nosto.Attributes.Add("style", "display:none");
            url = Request.Url.AbsoluteUri;
            if (!IsPostBack)
            {
                Initpage();
                gysnum();
                checked_detailRepeaterdatabind();
                tbpc_comparepriceresultRepeaterdatabind();
                initpower();
                foreach (RepeaterItem item in checked_detailRepeater.Items)//点击物料代用返回上一页 仍然会勾选 这里取消勾选
                {
                    ((CheckBox)item.FindControl("CKBOX_SELECT")).Checked = false;
                }
            }
            //CheckUser(ControlFinder);
        }
        //供应商数量
        private void gysnum()
        {
            int i = 0;
            string sql = "select distinct PIC_SUPPLIERAID,PIC_SUPPLIERBID,PIC_SUPPLIERCID,PIC_SUPPLIERDID,PIC_SUPPLIEREID,PIC_SUPPLIERFID from  TBPC_IQRCMPPRICE where PIC_SHEETNO='" + TextBoxNO.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                string SA = dr["PIC_SUPPLIERAID"].ToString();
                string SB = dr["PIC_SUPPLIERBID"].ToString();
                string SC = dr["PIC_SUPPLIERCID"].ToString();
                string SD = dr["PIC_SUPPLIERDID"].ToString();
                string SE = dr["PIC_SUPPLIEREID"].ToString();
                string SF = dr["PIC_SUPPLIERFID"].ToString();
                dr.Close();
                if (SA != "" && SA != null)
                {
                    i++;
                    Gys1 = "yes";
                }
                else
                {
                    Gys1 = "no";
                }
                if (SB != "" && SB != null)
                {
                    i++;
                    Gys2 = "yes";
                }
                else
                {
                    Gys2 = "no";
                }
                if (SC != "" && SC != null)
                {
                    i++;
                    Gys3 = "yes";
                }
                else
                {
                    Gys3 = "no";
                }
                if (SD != "" && SD != null)
                {
                    i++;
                    Gys4 = "yes";
                }
                else
                {
                    Gys4 = "no";
                }
                if (SE != "" && SE != null)
                {
                    i++;
                    Gys5 = "yes";
                }
                else
                {
                    Gys5 = "no";
                }
                if (SF != "" && SF != null)
                {
                    i++;
                    Gys6 = "yes";
                }
                else
                {
                    Gys6 = "no";
                }
                if (i == 0)
                {
                    TextBox1.Text = "3";
                    Globgysnum = "0";
                }
                else
                {
                    TextBox1.Text = Convert.ToString(i);
                    Globgysnum = Convert.ToString(i);
                }

            }
        }

        //显示比价单信息、审核信息
        private void Initpage()
        {
            if (Request.QueryString["ptc"] != null)
            {
                gloabptc = Server.UrlDecode(Request.QueryString["ptc"].ToString());
            }
            else
            {
                gloabptc = "";
            }
            if (Request.QueryString["sheetno"] != null)
            {
                gloabsheetno = Request.QueryString["sheetno"].ToString();
            }
            else
            {
                gloabsheetno = "";
            }
            if (Request.QueryString["num1"] != null)
            {
                NUM1 = Request.QueryString["num1"].ToString();
            }
            else
            {
                NUM1 = "";
            }
            if (Request.QueryString["num2"] != null)
            {
                NUM2 = Request.QueryString["num2"].ToString();
            }
            else
            {
                NUM2 = "";
            }

            Hyp_print.NavigateUrl = "TBPC_IQRCMPPRC_resultprint2.aspx?sheetno=" + gloabsheetno;
            TextBoxNO.Text = gloabsheetno;//单号
            string sqltext = "";
            sqltext = "select ICL_ZBYN,zdrid  as ICL_REVIEWA,zdrnm  as ICL_REVIEWANM,zdtime  as ICL_REVIEWATIME, zdryj as ICL_REVIEWAADVC," +
                     "shbid as ICL_REVIEWB,shbnm as ICL_REVIEWBNM,shbtime as ICL_REVIEWBTIME,shbyj as ICL_REVIEWBADVC," +
                     "shcid as ICL_REVIEWC,shcnm as ICL_REVIEWCNM,shctime as ICL_REVIEWCTIME,shcyj as ICL_REVIEWCADVC," +
                     "shdid as ICL_REVIEWD,shdnm as ICL_REVIEWDNM,shdtime as ICL_REVIEWDTIME,shdyj as ICL_REVIEWDADVC," +
                     "sheid as ICL_REVIEWE,shenm as ICL_REVIEWENM,shetime as ICL_REVIEWETIME,sheyi as ICL_REVIEWEADVC," +
                     "shfid as ICL_REVIEWF,shfnm as ICL_REVIEWFNM,shftime as ICL_REVIEWFTIME,shfyj as ICL_REVIEWFADVC," +
                     "shgid as ICL_REVIEWG,shgnm as ICL_REVIEWGNM,shgtime as ICL_REVIEWGTIME,shgyj as ICL_REVIEWGADVC," +
                     "isnull(totalstate,0) as ICL_STATE,isnull(statea,0) as ICL_STATEA,isnull(stateb,0) as ICL_STATEB,isnull(statec,0) as ICL_STATEC,isnull(stated,0) as ICL_STATED,isnull(statee,0) as ICL_STATEE,isnull(statef,0) as ICL_STATEF,ICL_TYPE " +
                     "from View_TBPC_IQRCMPPRCRVW  where picno='" + gloabsheetno + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ICL_ZBYN"].ToString() == "1")
                {
                    cbpower.Checked = true;
                }
                else
                {
                    cbpower.Checked = false;
                }
                TB_zdanrenid.Text = dt.Rows[0]["ICL_REVIEWA"].ToString();
                if (dt.Rows[0]["ICL_TYPE"].ToString() == "1")
                {
                    Chb_Jc.Checked = true;
                    if (TB_zdanrenid.Text == Session["UserID"].ToString())
                    {
                        CancelJc.Visible = true;
                    }
                }
                if (dt.Rows[0]["ICL_TYPE"].ToString() == "2")
                {
                    Chb_Zb.Checked = true;
                    if (TB_zdanrenid.Text == Session["UserID"].ToString())
                    {
                        CancelZb.Visible = true;
                    }
                }
                Tb_zdanren.Text = dt.Rows[0]["ICL_REVIEWANM"].ToString();
                Tb_tjiaot.Text = dt.Rows[0]["ICL_REVIEWATIME"].ToString();
                Tb_zdanyj.Text = dt.Rows[0]["ICL_REVIEWAADVC"].ToString();
                if (TB_zdanrenid.Text == Session["UserID"].ToString() && dt.Rows[0]["ICL_STATE"].ToString() == "1" && dt.Rows[0]["ICL_STATEA"].ToString() == "0")
                {
                    btn_fanshen.Enabled = true;
                }
      
                Tb_shenheren1.Text = dt.Rows[0]["ICL_REVIEWBNM"].ToString();
            
                Tb_shenherencode1.Text = dt.Rows[0]["ICL_REVIEWB"].ToString();
                Tb_shenhet1.Text = dt.Rows[0]["ICL_REVIEWBTIME"].ToString();
                Tb_shenheyj1.Text = dt.Rows[0]["ICL_REVIEWBADVC"].ToString();
                if (dt.Rows[0]["ICL_STATE"].ToString() == "2" && dt.Rows[0]["ICL_STATEA"].ToString() == "0" && dt.Rows[0]["ICL_STATEB"].ToString() == "0" && dt.Rows[0]["ICL_STATEC"].ToString() == "0" && dt.Rows[0]["ICL_STATED"].ToString() == "0" && dt.Rows[0]["ICL_STATEE"].ToString() == "0" && dt.Rows[0]["ICL_STATEF"].ToString() == "0")
                {
                    Rad_butongyi1.Checked = true;
                }
                else if (dt.Rows[0]["ICL_STATE"].ToString() == "3" || dt.Rows[0]["ICL_STATE"].ToString() == "4")
                {
                    Rad_tongyi1.Checked = true;
                }
                else
                {
                    Rad_butongyi1.Checked = false;
                    Rad_tongyi1.Checked = false;
                }
                if (Tb_shenherencode1.Text == Session["UserID"].ToString() && dt.Rows[0]["ICL_STATEA"].ToString() != "0")
                {
                    btn_fanshen.Enabled = true;
                }

                Tb_shenheren2.Text = dt.Rows[0]["ICL_REVIEWCNM"].ToString();
                Tb_shenherencode2.Text = dt.Rows[0]["ICL_REVIEWC"].ToString();
                Tb_shenhet2.Text = dt.Rows[0]["ICL_REVIEWCTIME"].ToString();
                Tb_shenheyj2.Text = dt.Rows[0]["ICL_REVIEWCADVC"].ToString();
                if (dt.Rows[0]["ICL_STATEB"].ToString() == "1")
                {
                    Rad_butongyi2.Checked = true;

                }
                else if (dt.Rows[0]["ICL_STATEB"].ToString() == "2")
                {
                    Rad_tongyi2.Checked = true;
                }
                else
                {
                    Rad_butongyi2.Checked = false;
                    Rad_tongyi2.Checked = false;
                }
                if (Tb_shenherencode2.Text == Session["UserID"].ToString() && dt.Rows[0]["ICL_STATEB"].ToString() != "0")
                {
                    btn_fanshen.Enabled = true;
                }

                //三级审核
                Tb_shenheren3.Text = dt.Rows[0]["ICL_REVIEWDNM"].ToString();
                Tb_shenherencode3.Text = dt.Rows[0]["ICL_REVIEWD"].ToString();
                Tb_shenhet3.Text = dt.Rows[0]["ICL_REVIEWDTIME"].ToString();
                Tb_shenheyj3.Text = dt.Rows[0]["ICL_REVIEWDADVC"].ToString();
                if (dt.Rows[0]["ICL_STATEC"].ToString() == "1")
                {
                    Rad_butongyi3.Checked = true;

                }
                else if (dt.Rows[0]["ICL_STATEC"].ToString() == "2")
                {
                    Rad_tongyi3.Checked = true;
                }
                else
                {
                    Rad_butongyi3.Checked = false;
                    Rad_tongyi3.Checked = false;
                }
                if (Tb_shenherencode3.Text == Session["UserID"].ToString() && dt.Rows[0]["ICL_STATEC"].ToString() != "0" && dt.Rows[0]["ICL_STATED"].ToString() == "0" && dt.Rows[0]["ICL_STATEE"].ToString() == "0")
                {
                    btn_fanshen.Enabled = true;
                }
            }
            if ((dt.Rows[0]["ICL_STATE"].ToString() == "0" || dt.Rows[0]["ICL_STATE"].ToString() == "2") && Session["UserID"].ToString() == dt.Rows[0]["ICL_REVIEWA"].ToString())
            {
                btn_edit.Enabled = true;
                btnReplace.Enabled = true;
                btn_chaifen.Enabled = true;
                btn_edit.ForeColor = System.Drawing.Color.Black;
                btnReplace.ForeColor = System.Drawing.Color.Black;
                cbpower.Enabled = true;
            }
            else
            {
                btn_edit.Enabled = false;
                btnReplace.Enabled = false;
                btn_edit.ForeColor = System.Drawing.Color.Gray;
                btnReplace.ForeColor = System.Drawing.Color.Gray;
                btn_chaifen.Enabled = false;
            }
            string sqltext1 = "select * from TBPC_BJDYC where name='" + Session["UserID"].ToString() + "'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext1);
            if (dt1.Rows.Count > 0)
            {
                string ptc = dt1.Rows[0]["ptc"].ToString();
                string marid = dt1.Rows[0]["marid"].ToString();
                string marth = dt1.Rows[0]["marth"].ToString();
                string marcz = dt1.Rows[0]["marcz"].ToString();
                string margb = dt1.Rows[0]["margb"].ToString();
                string cgsl = dt1.Rows[0]["cgsl"].ToString();
                string unit = dt1.Rows[0]["unit"].ToString();
                string shuilv = dt1.Rows[0]["shuilv"].ToString();
                string jine = dt1.Rows[0]["jine"].ToString();
                string length = dt1.Rows[0]["length"].ToString();
                string width = dt1.Rows[0]["width"].ToString();
                string note = dt1.Rows[0]["note"].ToString();
                string fznum = dt1.Rows[0]["fznum"].ToString();
                string fzunit = dt1.Rows[0]["fzunit"].ToString();

                if (ptc == "1") { CheckBox1.Checked = true; }
                if (marid == "1") { CheckBox2.Checked = true; }
                if (marth == "1") { CheckBox3.Checked = true; }
                if (marcz == "1") { CheckBox4.Checked = true; }
                if (margb == "1") { CheckBox5.Checked = true; }
                if (cgsl == "1") { CheckBox6.Checked = true; }
                if (unit == "1") { CheckBox7.Checked = true; }
                if (shuilv == "1") { CheckBox8.Checked = true; }
                if (jine == "1") { CheckBox9.Checked = true; }
                if (length == "1") { CheckBox10.Checked = true; }
                if (width == "1") { CheckBox11.Checked = true; }
                if (note == "1") { CheckBox12.Checked = true; }
                if (fznum == "1") { CheckBox13.Checked = true; }
                if (fzunit == "1") { CheckBox14.Checked = true; }
            }
        }

        //添加审批人
        private void initpower()
        {
            //
            string sqltext = "";
            string ptcode = "";
            double amount = Convert.ToDouble(((Label)(tbpc_comparepriceresultRepeater.Controls[tbpc_comparepriceresultRepeater.Controls.Count - 1].FindControl("totalamount"))).Text);

            if (cbpower.Checked == true)
            {
                amount = 5000000;
            }

            int num = 0;
            string shren1id = "";
            string shren2id = "";
            string shren3id = "";

            string shren1nm = "";
            string shren2nm = "";
            string shren3nm = "";

            string[] strsid1 = { };
            string[] strsid2 = { };
            string[] strsid3 = { };
            string[] strolds = { };

            num = 2;
            tb_pnum.Text = "2";
            if (num > 0)
            {
                shren1id = "47";
                shren1nm = "李利恒";
                shren2nm = "姜中毅";
                shren3nm = "赵宏观";

                if (shren1nm == Tb_shenheren1.Text | Tb_shenheren1.Text == "")
                {
                    Tb_shenheren1.Text = shren1nm;
                    Tb_shenherencode1.Text = shren1id;
                }
                if (Tb_shenheren2.Text == "")
                {
                    Tb_shenheren2.Text = shren2nm;
                    Tb_shenherencode2.Text = shren2id;
                }
                if (Tb_shenheren3.Text == "")
                {
                    Tb_shenheren3.Text = shren3nm;
                    Tb_shenherencode3.Text = shren3id;
                }

                Pan_shenheren1.Visible = true;
                Pan_shenheren2.Visible = true;
                Pan_shenheren3.Visible = true;

            }
            sqltext = "select zdrid  as ICL_REVIEWA,zdrnm  as ICL_REVIEWANM,zdtime  as ICL_REVIEWATIME, zdryj as ICL_REVIEWAADVC," +
                     "shbid as ICL_REVIEWB,shbnm as ICL_REVIEWBNM,shbtime as ICL_REVIEWBTIME,shbyj as ICL_REVIEWBADVC," +
                     "shcid as ICL_REVIEWC,shcnm as ICL_REVIEWCNM,shctime as ICL_REVIEWCTIME,shcyj as ICL_REVIEWCADVC," +
                     "shdid as ICL_REVIEWD,shdnm as ICL_REVIEWDNM,shdtime as ICL_REVIEWDTIME,shdyj as ICL_REVIEWDADVC," +
                     "sheid as ICL_REVIEWE,shenm as ICL_REVIEWENM,shetime as ICL_REVIEWETIME,sheyi as ICL_REVIEWEADVC," +
                     "isnull(totalstate,0) as ICL_STATE,isnull(statea,0) as ICL_STATEA,isnull(stateb,0) as ICL_STATEB,isnull(statec,0) as ICL_STATEC,isnull(stated,0) as ICL_STATED " +
                     "from View_TBPC_IQRCMPPRCRVW  where picno='" + gloabsheetno + "'";
            dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            {
                //strolds = new string[] { Tb_shenherencode2.Text, Tb_shenherencode3.Text, Tb_shenherencode4.Text, Tb_shenherencode5.Text };
                if (dt.Rows[0]["ICL_STATE"].ToString() == "4" && dt.Rows[0]["ICL_REVIEWD"].ToString() == "")
                {
                    Pan_shenheren3.Visible = false;
                }

                if (dt.Rows[0]["ICL_STATE"].ToString() != "0" && dt.Rows[0]["ICL_STATE"].ToString() != "2")
                {
                    Panel_zd.Enabled = false;
                }
                else
                {
                    if (Session["UserID"].ToString() == dt.Rows[0]["ICL_REVIEWA"].ToString())
                    {
                        Panel_zd.Enabled = true;
                        Tb_zdanyj.BackColor = System.Drawing.Color.LightCoral;
                        btn_confirm.Enabled = true;
                        if (Tb_tjiaot.Text == "")
                        {
                            Tb_tjiaot.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    }
                    else
                    {
                        Panel_zd.Enabled = false;
                    }
                }

                if (dt.Rows[0]["ICL_STATE"].ToString() != "1")
                {
                    Pan_shenheren1.Enabled = false;
                }
                else
                {
                    if (Session["UserID"].ToString() == shren1id)
                    {
                        Pan_shenheren1.Enabled = true;
                        btn_confirm.Enabled = true;
                        Tb_shenheyj1.BackColor = System.Drawing.Color.LightCoral;
                        Tb_shenheren1.Text = shren1nm;
                        Tb_shenherencode1.Text = shren1id;
                        if (Tb_shenhet1.Text == "")
                        {
                            Tb_shenhet1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    }
                    else
                    {
                        Pan_shenheren1.Enabled = false;
                    }
                }

                if (dt.Rows[0]["ICL_STATE"].ToString() != "3")
                {
                    Pan_shenheren2.Enabled = false;
                    Pan_shenheren3.Enabled = false;

                }
                else
                {

                    if (dt.Rows[0]["ICL_STATEA"].ToString() == "2" && dt.Rows[0]["ICL_STATEB"].ToString() == "0")
                    {
                        if (Session["UserID"].ToString() == "2")
                        {
                            Tb_shenheren2.Text = Session["UserName"].ToString();
                            Tb_shenherencode2.Text = Session["UserID"].ToString();
                            Pan_shenheren2.Enabled = true;
                            btn_confirm.Enabled = true;
                            Tb_shenheyj2.BackColor = System.Drawing.Color.LightCoral;
                            if (Tb_shenhet2.Text == "")
                            {
                                Tb_shenhet2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                        else
                        {
                            Pan_shenheren2.Enabled = false;
                        }
                    }
                    else if (dt.Rows[0]["ICL_STATEB"].ToString() == "2" && dt.Rows[0]["ICL_STATEC"].ToString() == "0")
                    {
                        if (Session["UserID"].ToString() == "311")
                        {
                            Tb_shenheren3.Text = Session["UserName"].ToString();
                            Tb_shenherencode3.Text = Session["UserID"].ToString();
                            Pan_shenheren3.Enabled = true;
                            btn_confirm.Enabled = true;
                            Tb_shenheyj3.BackColor = System.Drawing.Color.LightCoral;
                            if (Tb_shenhet3.Text == "")
                            {
                                Tb_shenhet3.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                        else
                        {
                            Pan_shenheren3.Enabled = false;
                        }

                    }

                }
            }
        }

        private void checked_detailRepeaterdatabind()
        {
            ItemTemplatedind();
            HeaderTemplatebind();
        }
        private void tbpc_comparepriceresultRepeaterdatabind()
        {

        }
        private void HeaderTemplatebind()
        {
            //初始化供应商的值
            string sqltext = "";
            sqltext = "SELECT distinct  supplieraid as aid, supplieranm as anm, supplierarank as arnk, " +
                         "supplierbid as bid, supplierbnm as bnm, supplierbrank  as brnk, suppliercid as cid, " +
                         "suppliercnm as cnm, suppliercrank as crnk, supplierdid as did, supplierdnm as dnm, " +
                         "supplierdrank as drnk, suppliereid as eid, supplierenm as enm, suppliererank as ernk, " +
                         "supplierfid as fid, supplierfnm as fnm,supplierfrank as frnk  " +
                         "FROM View_TBPC_IQRCMPPRICE where picno='" + TextBoxNO.Text.ToString() + "'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt1.Rows.Count > 0)
            {
                ((Label)(checked_detailRepeater.Controls[0].FindControl("PIC_SUPPLIERAID"))).Text = dt1.Rows[0]["aid"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("PIC_SUPPLIERANAME"))).Text = dt1.Rows[0]["anm"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("LbA_lei"))).Text = dt1.Rows[0]["arnk"].ToString();


                ((Label)(checked_detailRepeater.Controls[0].FindControl("PIC_SUPPLIERBID"))).Text = dt1.Rows[0]["bid"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("PIC_SUPPLIERBNAME"))).Text = dt1.Rows[0]["bnm"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("LbB_lei"))).Text = dt1.Rows[0]["brnk"].ToString();


                ((Label)(checked_detailRepeater.Controls[0].FindControl("PIC_SUPPLIERCID"))).Text = dt1.Rows[0]["cid"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("PIC_SUPPLIERCNAME"))).Text = dt1.Rows[0]["cnm"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("LbC_lei"))).Text = dt1.Rows[0]["crnk"].ToString();


                ((Label)(checked_detailRepeater.Controls[0].FindControl("PIC_SUPPLIERDID"))).Text = dt1.Rows[0]["did"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("PIC_SUPPLIERDNAME"))).Text = dt1.Rows[0]["dnm"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("LbD_lei"))).Text = dt1.Rows[0]["drnk"].ToString();


                ((Label)(checked_detailRepeater.Controls[0].FindControl("PIC_SUPPLIEREID"))).Text = dt1.Rows[0]["eid"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("PIC_SUPPLIERENAME"))).Text = dt1.Rows[0]["enm"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("LbE_lei"))).Text = dt1.Rows[0]["ernk"].ToString();


                ((Label)(checked_detailRepeater.Controls[0].FindControl("PIC_SUPPLIERFID"))).Text = dt1.Rows[0]["fid"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("PIC_SUPPLIERFNAME"))).Text = dt1.Rows[0]["fnm"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("LbF_lei"))).Text = dt1.Rows[0]["frnk"].ToString();
            }
        }

        private void ItemTemplatedind()
        {
            string sqltext = "";
            sqltext = "SELECT picno, pjid, pjnm, engid, engnm, marid,PIC_MASHAPE ,marnm, margg, margb, marcz, marunit, " +
                     "marfzunit, length, width, CONVERT(float, marnum) AS marnum, CONVERT(float, marfznum) AS marfznum, CONVERT(float, marzxnum) AS marzxnum, CONVERT(float, marzxfznum) AS marzxfznum, " +
                    "supplierresid, supplierresnm, supplierresrank, shuilv, CONVERT(float, price) AS price, cast(detamount as decimal(18,2)) AS detamount, ptcode, qoutefstsa, " +
                    "CONVERT(float, qoutescdsa) AS qoutescdsa, CONVERT(float, qoutelstsa) AS qoutelstsa, qoutefstsb, CONVERT(float, qoutescdsb) AS qoutescdsb, CONVERT(float, qoutelstsb) AS qoutelstsb, qoutefstsc, CONVERT(float, qoutescdsc) AS qoutescdsc, " +
                    "CONVERT(float, qoutelstsc) AS qoutelstsc, qoutefstsd, CONVERT(float, qoutescdsd) AS qoutescdsd, CONVERT(float, qoutelstsd) AS qoutelstsd, qoutefstse, CONVERT(float, qoutescdse) AS qoutescdse, CONVERT(float, qoutelstse) AS qoutelstse, " +
                    "qoutefstsf, CONVERT(float, qoutescdsf) AS qoutescdsf, CONVERT(float, qoutelstsf) AS qoutelstsf, pmode, keycoms, zdjprice, zdjnum, isnull(detailstate,0) as detailstate,case when margb='' then PIC_TUHAO else '' end as PIC_TUHAO,PIC_CHILDENGNAME,PIC_PICODE, " +
                    "(SELECT min(PRICE) FROM View_TBPC_IQRCMPPRICE_RVW GROUP BY MARID,totalstate having View_TBPC_IQRCMPPRICE.marid=MARID AND totalstate='4') AS minprice, " +
                     "(SELECT  top(1) PRICE  FROM View_TBPC_IQRCMPPRICE_RVW  GROUP BY MARID,PRICE,IRQDATA,totalstate  having View_TBPC_IQRCMPPRICE.marid=MARID AND totalstate='4' and price is not null order by IRQDATA DESC ) as lastprice, " +
                    "isnull(detailcstate,0) as detailcstate, detailnote,PIC_MAP,ST_SQR,PIC_ARRAY,PIC_IFFAST AS 'IFFAST' FROM View_TBPC_IQRCMPPRICE  where picno='" + TextBoxNO.Text.ToString() + "' order by ptcode ASC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            checked_detailRepeater.DataSource = dt;
            checked_detailRepeater.DataBind();
            tbpc_comparepriceresultRepeater.DataSource = dt;
            tbpc_comparepriceresultRepeater.DataBind();

            if (tbpc_comparepriceresultRepeater.Items.Count > 0)
            {
                NoDataPane2.Visible = false;
            }
            else
            {
                NoDataPane2.Visible = true;
            }
            TabContainer1.ActiveTabIndex = 0;
        }
        protected void btn_edit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_compareprice.aspx?sheetno=" + gloabsheetno + "");
        }
        protected void Rad_tongyi1_checkedchanged(object sender, EventArgs e)
        {
            settbtongyitext(Rad_tongyi1, Tb_shenheyj1);
        }
        protected void Rad_tongyi2_checkedchanged(object sender, EventArgs e)
        {
            settbtongyitext(Rad_tongyi2, Tb_shenheyj2);
        }
        protected void Rad_tongyi3_checkedchanged(object sender, EventArgs e)
        {
            settbtongyitext(Rad_tongyi3, Tb_shenheyj3);
        }

        protected void settbtongyitext(RadioButton rbt, TextBox tb)
        {
            if (rbt.Checked)
            {
                if (tb.Text.Replace(" ", "") == "")
                {
                    tb.Text = "同意";
                }
            }
        }


        protected int Get_zero_itemcount()
        {
            int zero_itemcount = 0;
            //检查是否有单价为0的项
            for (int i = 0; i < tbpc_comparepriceresultRepeater.Items.Count; i++)
            {
                if (CommonFun.ComTryDecimal(((Label)tbpc_comparepriceresultRepeater.Items[i].FindControl("PIC_PRICE")).Text.Trim()) == 0 && ((Label)tbpc_comparepriceresultRepeater.Items[i].FindControl("lbarray")).Text.Trim() == "")
                {
                    zero_itemcount++;
                }
            }

            return zero_itemcount;
        }


        // TODO: 确定,提交审核信息
        protected void btn_confirm_Click(object sender, EventArgs e)
        {



            List<string> sqltextlist = new List<string>();
            string sqltext = "";
            string state = "";
            string statea = "";
            string stateb = "";
            string statec = "";

            sqltext = "select ICL_STATE,ICL_STATEA,ICL_STATEB,ICL_STATEC " +
                      "from TBPC_IQRCMPPRCRVW where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                state = dr["ICL_STATE"].ToString();
                statea = dr["ICL_STATEA"].ToString();
                stateb = dr["ICL_STATEB"].ToString();
                statec = dr["ICL_STATEC"].ToString();

            }
            dr.Close();


            if (state == "0" || state == "2")
            {
                Label amounttex = checked_detailRepeater.Controls[checked_detailRepeater.Controls.Count - 1].FindControl("Lb_amount") as Label;
                double amount = Convert.ToDouble(amounttex.Text);
                if (Tb_zdanyj.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写意见！');", true);
                    ItemTemplatedind();
                    return;
                }
                else
                {
                    if (Chb_Zb.Checked)
                    {
                        sqltext = "update TBPC_IQRCMPPRCRVW set ICL_STATE='4',ICL_REVIEWAADVC='" + Tb_zdanyj.Text.ToString() + "',ICL_AMOUT=" + amount + ",ICL_TYPE='2' where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                    }
                    else if (Chb_Jc.Checked)
                    {
                        sqltext = "update TBPC_IQRCMPPRCRVW set ICL_STATE='4',ICL_REVIEWAADVC='" + Tb_zdanyj.Text.ToString() + "',ICL_AMOUT=" + amount + ",ICL_TYPE='1' where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                    }
                    else
                    {
                        //string str = "select ST_ID from TBDS_STAFFINFO where ST_POSITION='0501' and ST_PD='0'";
                        //System.Data.DataTable leader = DBCallCommon.GetDTUsingSqlText(str);
                        //string lead = "";
                        //if (leader.Rows.Count > 0)
                        //{
                        //    lead = leader.Rows[0][0].ToString();
                        //}

                        //if (!string.IsNullOrEmpty(lead))

                    
                        
                         string lead = Tb_shenherencode1.Text; 
                      
                        {
                            sqltext = "UPDATE TBPC_IQRCMPPRCRVW SET ICL_REVIEWATIME=(case when (ICL_REVIEWATIME='' or ICL_REVIEWATIME is null) then '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' else  ICL_REVIEWATIME end)," +
                                      "ICL_REVIEWAADVC='" + Tb_zdanyj.Text.ToString() + "',ICL_REVIEWB=" + lead + ",ICL_REVIEWBTIME=''," +
                                      "ICL_REVIEWBADVC='',ICL_REVIEWC='',ICL_REVIEWCADVC='',ICL_REVIEWCTIME='',ICL_REVIEWD=''," +
                                      "ICL_REVIEWDADVC='',ICL_REVIEWDTIME=''," +
                                      "ICL_STATEA='0'," +
                                      "ICL_STATEB='0',ICL_STATEC='0',ICL_STATED='0',ICL_STATEE='0',ICL_STATEF='0',ICL_STATE='1',ICL_AMOUT=" + amount + "  " +
                                      "WHERE ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(lead), new List<string>(), new List<string>(), "采购比价单审批", "您有采购比价单需要审批，请登录系统查看");
                        }
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('采购部暂时无部长，无法进行审批操作，请及时与负责人沟通！');", true);
                        //    return;
                        //}
                    }
                }
            }

            //如果审核人2和审核人3为同一人，则同时更新两项内容。
            else if (state == "1")
            {
                if (Rad_tongyi1.Checked)
                {
                    sqltext = "UPDATE TBPC_IQRCMPPRCRVW SET ICL_REVIEWB='" + Tb_shenherencode1.Text + "'," +
                          "ICL_REVIEWBTIME='" + Tb_shenhet1.Text.ToString() + "'," +
                          "ICL_REVIEWBADVC='" + Tb_shenheyj1.Text.ToString() + "',ICL_STATE='3',ICL_STATEA='2' " +
                          "WHERE ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";

                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("2"), new List<string>(), new List<string>(), "采购比价单审批", "您有采购比价单需要审批，请登录系统查看");
                }
                else
                {
                    if (Tb_shenheyj1.Text.ToString().Replace(" ", "") == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写拒绝理由！');", true);
                        ItemTemplatedind();
                        return;
                    }
                    else
                    {
                        //sqltext = "UPDATE TBPC_IQRCMPPRCRVW SET ICL_REVIEWB='" + Tb_shenherencode1.Text + "'," +
                        //                                 "ICL_REVIEWBTIME='" + Tb_shenhet1.Text.ToString() + "'," +
                        //                                 "ICL_REVIEWBADVC='" + Tb_shenheyj1.Text.ToString() + "',ICL_REVIEWCTIME='" + Tb_shenhet1.Text.ToString() + "',ICL_REVIEWCADVC='" + Tb_shenheyj1.Text.ToString() + "',ICL_STATE='2',ICL_STATEA='1',ICL_STATEB='1' " +
                        //                                 "WHERE ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                        sqltext = "UPDATE TBPC_IQRCMPPRCRVW SET ICL_REVIEWB='" + Tb_shenherencode1.Text + "'," +
                                                         "ICL_REVIEWBTIME='" + Tb_shenhet1.Text.ToString() + "'," +
                                                         "ICL_REVIEWBADVC='" + Tb_shenheyj1.Text.ToString() + "',ICL_STATE='2',ICL_STATEA='1',ICL_STATEB='0' " +
                                                         "WHERE ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";


                        //发驳回邮件
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(TB_zdanrenid.Text.ToString()), new List<string>(), new List<string>(), "采购比价单审批", "您有采购比价单被驳回，请登录系统查看");



                    }

                }
            }
            else if (state == "3")
            {
                if (Pan_shenheren2.Enabled == true)
                {
                    if (Rad_tongyi2.Checked)
                    {
                        sqltext = "UPDATE TBPC_IQRCMPPRCRVW SET ICL_REVIEWC='" + Tb_shenherencode2.Text + "'," +
                                  "ICL_REVIEWCTIME='" + Tb_shenhet2.Text.ToString() + "'," +
                                  "ICL_REVIEWCADVC='" + Tb_shenheyj2.Text.ToString() + "',ICL_STATE='3',ICL_STATEB='2' " +
                                  "WHERE ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("311"), new List<string>(), new List<string>(), "采购比价单审批", "您有采购比价单需要审批，请登录系统查看");
                    }
                    else
                    {
                        if (Tb_shenheyj2.Text.ToString().Replace(" ", "") == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写拒绝理由！');", true);
                            ItemTemplatedind();
                            return;
                        }
                        else
                        {
                            sqltext = "UPDATE TBPC_IQRCMPPRCRVW SET ICL_REVIEWC='" + Tb_shenherencode2.Text + "'," +
                                                                "ICL_REVIEWCTIME='" + Tb_shenhet2.Text.ToString() + "'," +
                                                                "ICL_REVIEWCADVC='" + Tb_shenheyj2.Text.ToString() + "',ICL_STATE='2',ICL_STATEB='1'  " +
                                                                "WHERE ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";




                            //发驳回邮件
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(TB_zdanrenid.Text.ToString()), new List<string>(), new List<string>(), "采购比价单审批", "您有采购比价单被驳回，请登录系统查看");
                        }

                    }
                }


                //审核人3
                if (Pan_shenheren3.Enabled == true)
                {
                    if (Rad_tongyi3.Checked)
                    {
                        sqltext = "UPDATE TBPC_IQRCMPPRCRVW SET ICL_REVIEWD='" + Tb_shenherencode3.Text + "'," +
                                  "ICL_REVIEWDTIME='" + Tb_shenhet3.Text.ToString() + "'," +
                                  "ICL_REVIEWDADVC='" + Tb_shenheyj3.Text.ToString() + "',ICL_STATE='3',ICL_STATEC='2' " +
                                  "WHERE ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                    }
                    else
                    {
                        if (Tb_shenheyj3.Text.ToString().Replace(" ", "") == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写拒绝理由！');", true);
                            ItemTemplatedind();
                            return;
                        }
                        else
                        {
                            sqltext = "UPDATE TBPC_IQRCMPPRCRVW SET ICL_REVIEWD='" + Tb_shenherencode3.Text + "'," +
                                                                "ICL_REVIEWDTIME='" + Tb_shenhet3.Text.ToString() + "'," +
                                                                "ICL_REVIEWDADVC='" + Tb_shenheyj3.Text.ToString() + "',ICL_STATE='2',ICL_STATEC='1'  " +
                                                                "WHERE ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";




                            //发驳回邮件
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(TB_zdanrenid.Text.ToString()), new List<string>(), new List<string>(), "采购比价单审批", "您有采购比价单被驳回，请登录系统查看");
                        }

                    }
                }
            }
            if (Chb_Jc.Checked || Chb_Zb.Checked)
            {
                DBCallCommon.ExeSqlText(sqltext);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "test", "<script language=javascript>if(confirm('提交成功! 是否要返回前一页面?'))document.getElementById('" + btn_nosto.ClientID + "').click();</script>");
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交审核成功！');", true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener.location = window.opener.location.href;window.close();", true);
            }
            else
            {
                sqltextlist.Add(sqltext);

                sqltext = "update TBPC_IQRCMPPRCRVW set ICL_STATE= case when ICL_STATEA='2' and ICL_STATEB='2' and ICL_STATEC='2' then '4' " +
                                                                           "when (ICL_STATEA='1' or ICL_STATEB='1' or ICL_STATEC='1') and  " +
                                                                           "(ICL_STATEA<>'0' and ICL_STATEB<>'0' and ICL_STATEC<>'0') then '2' " +
                                                                           "else ICL_STATE " +
                                                                           "end " +
                              "where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                sqltextlist.Add(sqltext);

                DBCallCommon.ExecuteTrans(sqltextlist);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交审核成功！');", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "test", "<script language=javascript>if(confirm('提交审核成功! 是否要返回前一页面?'))document.getElementById('" + btn_nosto.ClientID + "').click();</script>");
                //ItemTemplatedind();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener.location = window.opener.location.href;window.close();", true);
            }
        }

        protected void btn_nosto_Click(object sender, EventArgs e)//无库存可用
        {
            Response.Redirect("~/PC_Data/TBPC_IQRCMPPRCLST_checked.aspx?num1=" + NUM1 + "&num2=" + NUM2 + "");
        }

        //返审   2013年3月1日 14:41:42
        protected void btn_fanshen_Click(object sender, EventArgs e)
        {

            string sqltext = "";
            string ptcode = "";
            string state = "";
            string statea = "";
            string stateb = "";
            string statec = "";
            string stated = "";
            //2013年2月27日 08:34:24
            int i = 0;
            List<string> sqltextlist = new List<string>();

            foreach (RepeaterItem reitem in checked_detailRepeater.Items)
            {
                ptcode = ((Label)reitem.FindControl("PIC_PCODE")).Text;
                sqltext = "select * from TBPC_PURORDERDETAIL where PO_PTCODE='" + ptcode + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    i++;
                }
            }
            if (i > 0)
            {
                sqltext = "select ICL_STATE,ICL_STATEA,ICL_STATEB,ICL_STATEC,ICL_STATED,ICL_STATEE,ICL_STATEF " +
                                     "from TBPC_IQRCMPPRCRVW where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr.Read())
                {
                    state = dr["ICL_STATE"].ToString();
                    statea = dr["ICL_STATEA"].ToString();
                    stateb = dr["ICL_STATEB"].ToString();
                    statec = dr["ICL_STATEC"].ToString();
                    stated = dr["ICL_STATED"].ToString();
                }
                dr.Close();

                //为了避免两个相同评审人的情况出现，造成返审失败，这里做了改动  2013年3月1日 15:00:43
                List<string> sqlfs = new List<string>();
                if (Session["UserID"].ToString() == TB_zdanrenid.Text && state == "1")//制单人
                {
                    string sqltext0 = "update TBPC_IQRCMPPRCRVW set ICL_STATE='0',ICL_REVIEWAADVC='',ICL_REVIEWATIME='' where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                    sqlfs.Add(sqltext0);
                }

                else if (Session["UserID"].ToString() == "311" && (state == "2" || state == "4" || state == "3") && statea != "0" && stateb != "0" && statec != "0" && stated == "0")
                {
                    string sqltext7 = "update TBPC_IQRCMPPRCRVW set ICL_STATE='1',ICL_STATEA='0',ICL_STATEB='0',ICL_STATEC='0',ICL_REVIEWBADVC='',ICL_REVIEWBTIME='',ICL_REVIEWCADVC='',ICL_REVIEWCTIME='',ICL_REVIEWDADVC='',ICL_REVIEWDTIME='' where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                    sqlfs.Add(sqltext7);
                }
                else if (Session["UserID"].ToString() == Tb_shenherencode1.Text && (state == "2" || state == "4" || state == "3") && statea != "0" && stated == "0")
                {
                    //2016.11.30
                    string sqltext1 = "update TBPC_IQRCMPPRCRVW set ICL_STATE='1',ICL_STATEA='0',ICL_STATEB='0',ICL_STATEC='0',ICL_REVIEWBADVC='',ICL_REVIEWBTIME='',ICL_REVIEWCADVC='',ICL_REVIEWCTIME='',ICL_REVIEWDADVC='',ICL_REVIEWDTIME='' where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                    //string sqltext1 = "update TBPC_IQRCMPPRCRVW set ICL_STATE='1',ICL_STATEA='0',ICL_REVIEWBADVC='',ICL_REVIEWBTIME='' where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                    sqlfs.Add(sqltext1);
                }
                else if (Session["UserID"].ToString() == Tb_shenherencode2.Text && (state == "2" || state == "4" || state == "3") && stateb != "0")
                {
                    string sqltext2 = "update TBPC_IQRCMPPRCRVW set ICL_STATE='3',ICL_STATEB='0',ICL_REVIEWCADVC='',ICL_REVIEWCTIME='' where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                    sqlfs.Add(sqltext2);
                }
                else if (Session["UserID"].ToString() == Tb_shenherencode3.Text && (state == "2" || state == "4" || state == "3") && statec != "0")
                {
                    string sqltext5 = "update TBPC_IQRCMPPRCRVW set ICL_STATE='3',ICL_STATEC='0',ICL_REVIEWDADVC='',ICL_REVIEWDTIME='' where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                    sqlfs.Add(sqltext5);
                }
                DBCallCommon.ExecuteTrans(sqlfs); //执行数据库
                Initpage();
                initpower();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('此比价单中数据已经全部下推订单，不能反审！');", true);
                ItemTemplatedind();
            }

        }
        protected void btn_shangcha_Click(object sender, EventArgs e)
        {
            int i = 0;
            string ptcode = "";
            string sqltext = "";
            string plancode = "";
            foreach (RepeaterItem reitem in checked_detailRepeater.Items)
            {
                CheckBox cbx = reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        ptcode = ((Label)reitem.FindControl("PIC_PCODE")).Text;
                    }
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
                ItemTemplatedind();
                checked_detailRepeaterdatabind();
            }
            else if (i > 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择一条记录！');", true);
                ItemTemplatedind();
                checked_detailRepeaterdatabind();
            }
            else
            {
                //btn_shangcha.Attributes.Add("onclick", "form.target='_blank'");
                if (ptcode.Contains("#"))
                {
                    ptcode = ptcode.Substring(0, ptcode.IndexOf("#")).ToString();
                }
                sqltext = "select planno from View_TBPC_PURCHASEPLAN_RVW where ptcode='" + ptcode + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    plancode = dt.Rows[0][0].ToString();
                    checked_detailRepeaterdatabind();
                    Response.Write("<script language='javascript'>window.open('../PC_Data/PC_TBPC_Purchaseplan_startcontent.aspx?ptc=" + Server.UrlEncode(ptcode) + "&mp_id=" + Server.UrlEncode(plancode) + "');</script>");

                    //Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_startcontent.aspx?ptc=" + Server.UrlEncode(ptcode) + "&mp_id=" + Server.UrlEncode(plancode) + "");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无关联单据！');", true);
                    ItemTemplatedind();
                    checked_detailRepeaterdatabind();
                }
            }
        }
        protected void btn_xiacha_Click(object sender, EventArgs e)
        {
            int i = 0;
            string ptcode = "";
            string orderno = "";
            string sqltext = "";
            foreach (RepeaterItem reitem in checked_detailRepeater.Items)
            {
                CheckBox cbx = reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        ptcode = ((Label)reitem.FindControl("PIC_PCODE")).Text;
                    }
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
                ItemTemplatedind();
                checked_detailRepeaterdatabind();
            }
            else if (i > 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择一条记录！');", true);
                ItemTemplatedind();
                checked_detailRepeaterdatabind();
            }
            else
            {
                //btn_xiacha.Attributes.Add("onclick", "form.target='_blank'");
                sqltext = "select orderno from View_TBPC_PURORDERDETAIL where ptcode='" + ptcode + "' and detailcstate!='2'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 1)
                {
                    orderno = dt.Rows[0][0].ToString();
                    //Response.Redirect("~/PC_Data/PC_TBPC_PurOrder.aspx?ptc=" + Server.UrlEncode(ptcode) + "&orderno=" + orderno + "");
                    Response.Write("<script language='javascript'>window.open('../PC_Data/PC_TBPC_PurOrder.aspx?ptc=" + Server.UrlEncode(ptcode) + "&orderno=" + orderno + "');</script>");
                    checked_detailRepeaterdatabind();
                }
                else if (dt.Rows.Count > 1)
                {
                    sqltext = "select orderno from View_TBPC_PURORDERDETAIL where ptcode='" + ptcode + "' and detailcstate='0'";
                    DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (dt1.Rows.Count == 1)
                    {
                        orderno = dt1.Rows[0][0].ToString();
                        //Response.Redirect("~/PC_Data/PC_TBPC_PurOrder.aspx?ptc=" + Server.UrlEncode(ptcode) + "&orderno=" + orderno + "");
                        Response.Write("<script language='javascript'>window.open('../PC_Data/PC_TBPC_PurOrder.aspx?ptc=" + Server.UrlEncode(ptcode) + "&orderno=" + orderno + "');</script>");
                        checked_detailRepeaterdatabind();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无关联单据！');", true);
                    ItemTemplatedind();
                    checked_detailRepeaterdatabind();
                }
            }
        }
        protected void hclose()//行关闭
        {
            int i = 0;
            string sqltext = "";
            foreach (RepeaterItem Reitem in checked_detailRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        sqltext = "update TBPC_IQRCMPPRICE set PIC_CSTATE='1' WHERE PIC_SHEETNO='" + TextBoxNO.Text + "' " +
                                  "and PIC_PTCODE='" + ((Label)Reitem.FindControl("PIC_PCODE")).Text.ToString() + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            if (i > 0)
            {
                sqltext = "SELECT PIC_ID FROM TBPC_IQRCMPPRICE WHERE PIC_SHEETNO='" + TextBoxNO.Text + "' and PIC_CSTATE='0'";//是否还存在未关闭的，如果都关闭则整单关闭
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    sqltext = "update TBPC_IQRCMPPRCRVW set ICL_CSTATE='1'  WHERE ICL_SHEETNO='" + TextBoxNO.Text + "'";//单号关闭
                    DBCallCommon.ExeSqlText(sqltext);
                }
                checked_detailRepeaterdatabind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
                ItemTemplatedind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
                ItemTemplatedind();
            }
        }
        protected void fhclose()//行反关闭
        {
            int i = 0;
            string sqltext = "";
            foreach (RepeaterItem Reitem in checked_detailRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        sqltext = "update TBPC_IQRCMPPRICE set PIC_CSTATE='0' WHERE PIC_SHEETNO='" + TextBoxNO.Text + "' " +
                                  "and PIC_PTCODE='" + ((Label)Reitem.FindControl("PIC_PCODE")).Text.ToString() + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            if (i > 0)
            {
                sqltext = "SELECT PIC_ID FROM TBPC_IQRCMPPRICE WHERE PIC_SHEETNO='" + TextBoxNO.Text + "' and PIC_CSTATE='0'";//是否还存在未关闭的，如果都关闭则整单关闭
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    sqltext = "update TBPC_IQRCMPPRCRVW set ICL_CSTATE='0'  WHERE ICL_SHEETNO='" + TextBoxNO.Text + "'";//单号关闭
                    DBCallCommon.ExeSqlText(sqltext);
                }
                checked_detailRepeaterdatabind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
                ItemTemplatedind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
                ItemTemplatedind();
            }
        }
        protected void allclose()
        {
            string sqltext = "";
            List<string> sqltexts = new List<string>();
            sqltext = "update TBPC_IQRCMPPRCRVW set ICL_CSTATE='1'  WHERE ICL_SHEETNO='" + TextBoxNO.Text + "'";//单号关闭
            sqltexts.Add(sqltext);
            sqltext = "update  TBPC_IQRCMPPRICE set PIC_CSTATE='1' WHERE PIC_SHEETNO='" + TextBoxNO.Text + "'";//条目关闭
            sqltexts.Add(sqltext);
            DBCallCommon.ExecuteTrans(sqltexts);
        }//单子关闭
        protected void fallclose()
        {
            string sqltext = "";
            List<string> sqltexts = new List<string>();
            sqltext = "update TBPC_IQRCMPPRCRVW set ICL_CSTATE='0'  WHERE ICL_SHEETNO='" + TextBoxNO.Text + "'";//单号反关闭
            sqltexts.Add(sqltext);
            sqltext = "update  TBPC_IQRCMPPRICE set PIC_CSTATE='0' WHERE PIC_SHEETNO='" + TextBoxNO.Text + "'";//条目反关闭
            sqltexts.Add(sqltext);
            DBCallCommon.ExecuteTrans(sqltexts);
        }//单子反关闭

        //变更查询
        protected void btn_biangeng_Click(object sender, EventArgs e)
        {
            int i = 0;
            string ptcode = "";
            foreach (RepeaterItem reitem in checked_detailRepeater.Items)
            {
                CheckBox cbx = reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        ptcode = ((Label)reitem.FindControl("PIC_PCODE")).Text;
                    }
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
                ItemTemplatedind();
                return;
            }
            else if (i > 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择一条记录！');", true);
                ItemTemplatedind();
                return;
            }
            else
            {
                Response.Redirect("~/PC_Data/PC_TBPC_Purchange_detail.aspx?ptcode=" + ptcode + "");
            }
        }
        //protected void tb1_textchanged(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "3", "hiddenprovcol()", true);
        //}
        protected void btn_back_Click(object sender, EventArgs e)//返回
        {
            Response.Redirect("~/PC_Data/TBPC_IQRCMPPRCLST_checked.aspx?num1=" + NUM1 + "&num2=" + NUM2 + "");
        }
        protected void btn_search_Click(object sender, EventArgs e)//搜索
        {
            int i = 0;
            string marid = "";
            //string marguige = "";
            //string marcaizhi = "";
            //string marshijian = "";
            foreach (RepeaterItem Reitem in checked_detailRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                    marid = ((Label)Reitem.FindControl("PIC_MARID")).Text.ToString();
                    //marguige = ((Label)Reitem.FindControl("PIC_MARGUIGE")).Text.ToString();
                    //marcaizhi = ((Label)Reitem.FindControl("PIC_MARCAIZ")).Text.ToString();
                    //marshijian = Tb_tjiaot.Text;
                    //break;
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "1", "alert('未选择数据');", true);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "2", "hiddencol()", true);
                ItemTemplatedind();
                return;
            }
            else if (i >= 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择一条数据');", true);
                ItemTemplatedind();
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "windowopen('PC_Date_historypriceshow.aspx?marid=" + marid + "');", true);
                ItemTemplatedind();
            }
            foreach (RepeaterItem Reitem in checked_detailRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        cbx.Checked = false;
                    }
                }
            }
        }
        #region
        private double sumzxnum = 0;
        private double sumamount = 0;
        private double sum1 = 0;
        private double sum2 = 0;
        private double sum3 = 0;
        private double sum4 = 0;
        private double sum5 = 0;
        private double sum6 = 0;

        private double num13 = 0;
        private double num23 = 0;
        private double num33 = 0;
        private double num43 = 0;
        private double num53 = 0;
        private double num63 = 0;
        #endregion
        int num = 0;
        double cgnum = 0;
        double zdjprice = 0;
        double zdjnum = 0;

        protected void checked_detailRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            num = Convert.ToInt32(TextBox1.Text);
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (CheckBox1.Checked)
                {
                    ((HtmlTableCell)e.Item.FindControl("tt1")).Visible = false;
                }
                if (CheckBox2.Checked)
                {
                    ((HtmlTableCell)e.Item.FindControl("tt2")).Visible = false;
                }
                if (CheckBox3.Checked)
                {
                    ((HtmlTableCell)e.Item.FindControl("tt3")).Visible = false;
                }
                if (CheckBox4.Checked)
                {
                    ((HtmlTableCell)e.Item.FindControl("tt4")).Visible = false;
                }
                if (CheckBox5.Checked)
                {
                    ((HtmlTableCell)e.Item.FindControl("tt5")).Visible = false;
                }
                if (CheckBox6.Checked)
                {
                    ((HtmlTableCell)e.Item.FindControl("tt6")).Visible = false;
                }
                if (CheckBox7.Checked)
                {
                    ((HtmlTableCell)e.Item.FindControl("tt7")).Visible = false;
                }
                if (CheckBox9.Checked)
                {
                    ((HtmlTableCell)e.Item.FindControl("tt8")).Visible = false;
                }
                if (CheckBox10.Checked)
                {
                    ((HtmlTableCell)e.Item.FindControl("tt9")).Visible = false;
                }
                if (CheckBox11.Checked)
                {
                    ((HtmlTableCell)e.Item.FindControl("tt10")).Visible = false;
                }
                if (CheckBox12.Checked)
                {
                    ((HtmlTableCell)e.Item.FindControl("tt11")).Visible = false;
                }
                string IFFAST = ((Label)e.Item.FindControl("IFFAST")).Text.Trim();
                if (IFFAST == "1")
                {
                    ((Label)e.Item.FindControl("lbUrgency")).Visible = true;
                }
            }
            if (e.Item.ItemType == ListItemType.Header)
            {
                int j = 21;
                if (CheckBox1.Checked)
                {
                    j--;
                    ((HtmlTableCell)e.Item.FindControl("td1")).Visible = false;
                }
                if (CheckBox2.Checked)
                {
                    j--;
                    ((HtmlTableCell)e.Item.FindControl("td2")).Visible = false;
                }
                if (CheckBox3.Checked)
                {
                    j--;
                    ((HtmlTableCell)e.Item.FindControl("td3")).Visible = false;
                }
                if (CheckBox4.Checked)
                {
                    j--;
                    ((HtmlTableCell)e.Item.FindControl("td4")).Visible = false;
                }
                if (CheckBox5.Checked)
                {
                    j--;
                    ((HtmlTableCell)e.Item.FindControl("td5")).Visible = false;
                }
                if (CheckBox6.Checked)
                {
                    j--;
                    ((HtmlTableCell)e.Item.FindControl("td6")).Visible = false;
                }
                if (CheckBox7.Checked)
                {
                    j--;
                    ((HtmlTableCell)e.Item.FindControl("td7")).Visible = false;
                }
                if (CheckBox9.Checked)
                {
                    j--;
                    ((HtmlTableCell)e.Item.FindControl("td8")).Visible = false;
                }
                if (CheckBox10.Checked)
                {
                    j--;
                    ((HtmlTableCell)e.Item.FindControl("td9")).Visible = false;
                }
                if (CheckBox11.Checked)
                {
                    j--;
                    ((HtmlTableCell)e.Item.FindControl("td10")).Visible = false;
                }
                if (CheckBox12.Checked)
                {
                    j--;
                    ((HtmlTableCell)e.Item.FindControl("td11")).Visible = false;
                }
                ((HtmlTableCell)e.Item.FindControl("wlxx")).ColSpan = j;
                HtmlTableCell GYS = (HtmlTableCell)e.Item.FindControl("GYS");
                GYS.ColSpan = num * 3;

                HtmlTableCell gys1 = (HtmlTableCell)e.Item.FindControl("gys1");
                HtmlTableCell gys2 = (HtmlTableCell)e.Item.FindControl("gys2");
                HtmlTableCell gys3 = (HtmlTableCell)e.Item.FindControl("gys3");
                HtmlTableCell gys4 = (HtmlTableCell)e.Item.FindControl("gys4");
                HtmlTableCell gys5 = (HtmlTableCell)e.Item.FindControl("gys5");
                HtmlTableCell gys6 = (HtmlTableCell)e.Item.FindControl("gys6");

                HtmlTableCell gysnm1 = (HtmlTableCell)e.Item.FindControl("gysnm1");
                HtmlTableCell gysnm2 = (HtmlTableCell)e.Item.FindControl("gysnm2");
                HtmlTableCell gysnm3 = (HtmlTableCell)e.Item.FindControl("gysnm3");
                HtmlTableCell gysnm4 = (HtmlTableCell)e.Item.FindControl("gysnm4");
                HtmlTableCell gysnm5 = (HtmlTableCell)e.Item.FindControl("gysnm5");
                HtmlTableCell gysnm6 = (HtmlTableCell)e.Item.FindControl("gysnm6");

                HtmlTableCell dyc1 = (HtmlTableCell)e.Item.FindControl("dyc1");
                HtmlTableCell dec1 = (HtmlTableCell)e.Item.FindControl("dec1");
                HtmlTableCell zz1 = (HtmlTableCell)e.Item.FindControl("zz1");

                HtmlTableCell dyc2 = (HtmlTableCell)e.Item.FindControl("dyc2");
                HtmlTableCell dec2 = (HtmlTableCell)e.Item.FindControl("dec2");
                HtmlTableCell zz2 = (HtmlTableCell)e.Item.FindControl("zz2");

                HtmlTableCell dyc3 = (HtmlTableCell)e.Item.FindControl("dyc3");
                HtmlTableCell dec3 = (HtmlTableCell)e.Item.FindControl("dec3");
                HtmlTableCell zz3 = (HtmlTableCell)e.Item.FindControl("zz3");

                HtmlTableCell dyc4 = (HtmlTableCell)e.Item.FindControl("dyc4");
                HtmlTableCell dec4 = (HtmlTableCell)e.Item.FindControl("dec4");
                HtmlTableCell zz4 = (HtmlTableCell)e.Item.FindControl("zz4");

                HtmlTableCell dyc5 = (HtmlTableCell)e.Item.FindControl("dyc5");
                HtmlTableCell dec5 = (HtmlTableCell)e.Item.FindControl("dec5");
                HtmlTableCell zz5 = (HtmlTableCell)e.Item.FindControl("zz5");

                HtmlTableCell dyc6 = (HtmlTableCell)e.Item.FindControl("dyc6");
                HtmlTableCell dec6 = (HtmlTableCell)e.Item.FindControl("dec6");
                HtmlTableCell zz6 = (HtmlTableCell)e.Item.FindControl("zz6");
                if (Globgysnum == "0")
                {
                    gys4.Visible = false; gys5.Visible = false; gys6.Visible = false;
                    gysnm4.Visible = false; gysnm5.Visible = false; gysnm6.Visible = false;
                    dyc4.Visible = false; dec4.Visible = false; zz4.Visible = false;
                    dyc5.Visible = false; dec5.Visible = false; zz5.Visible = false;
                    dyc6.Visible = false; dec6.Visible = false; zz6.Visible = false;
                }
                else
                {
                    if (Gys1 == "no")
                    {
                        gys1.Visible = false;
                        gysnm1.Visible = false;
                        dyc1.Visible = false; dec1.Visible = false; zz1.Visible = false;
                    }
                    if (Gys2 == "no")
                    {
                        gys2.Visible = false;
                        gysnm2.Visible = false;
                        dyc2.Visible = false; dec2.Visible = false; zz2.Visible = false;
                    }
                    if (Gys3 == "no")
                    {
                        gys3.Visible = false;
                        gysnm3.Visible = false;
                        dyc3.Visible = false; dec3.Visible = false; zz3.Visible = false;
                    }
                    if (Gys4 == "no")
                    {
                        gys4.Visible = false;
                        gysnm4.Visible = false;
                        dyc4.Visible = false; dec4.Visible = false; zz4.Visible = false;
                    }
                    if (Gys5 == "no")
                    {
                        gys5.Visible = false;
                        gysnm5.Visible = false;
                        dyc5.Visible = false; dec5.Visible = false; zz5.Visible = false;
                    }
                    if (Gys6 == "no")
                    {
                        gys6.Visible = false;
                        gysnm6.Visible = false;
                        dyc6.Visible = false; dec6.Visible = false; zz6.Visible = false;
                    }
                }

            }
            if (e.Item.ItemIndex >= 0)
            {
                if (((Label)e.Item.FindControl("PIC_ZXNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("PIC_ZXNUM")).Text.ToString() == System.String.Empty)
                {
                    //((Label)e.Item.FindControl("PIC_ZXNUM")).Text = "0";
                    cgnum = 0;
                }
                else
                {
                    cgnum = Convert.ToDouble(((Label)e.Item.FindControl("PIC_ZXNUM")).Text);
                }
                sumzxnum += cgnum;

                if (((Label)e.Item.FindControl("Label12")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("Label12")).Text.ToString() == System.String.Empty)
                {
                    //((Label)e.Item.FindControl("Label13")).Text = "0";
                    num13 = 0;
                }
                else
                {
                    num13 = Convert.ToDouble(((Label)e.Item.FindControl("Label13")).Text == "" ? "0" : ((Label)e.Item.FindControl("Label13")).Text);
                }
                sum1 += num13;

                if (((Label)e.Item.FindControl("Label22")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("Label22")).Text.ToString() == System.String.Empty)
                {
                    //((Label)e.Item.FindControl("Label23")).Text = "0";
                    num23 = 0;
                }
                else
                {
                    num23 = Convert.ToDouble(((Label)e.Item.FindControl("Label23")).Text == "" ? "0" : ((Label)e.Item.FindControl("Label23")).Text);
                }
                sum2 += num23;

                if (((Label)e.Item.FindControl("Label32")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("Label32")).Text.ToString() == System.String.Empty)
                {
                    //((Label)e.Item.FindControl("Label33")).Text = "0";
                    num33 = 0;
                }
                else
                {
                    num33 = Convert.ToDouble(((Label)e.Item.FindControl("Label33")).Text == "" ? "0" : ((Label)e.Item.FindControl("Label33")).Text);
                }
                sum3 += num33;

                if (((Label)e.Item.FindControl("Label42")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("Label42")).Text.ToString() == System.String.Empty)
                {
                    //((Label)e.Item.FindControl("Label43")).Text = "0";
                    num43 = 0;
                }
                else
                {
                    num43 = Convert.ToDouble(((Label)e.Item.FindControl("Label43")).Text == "" ? "0" : ((Label)e.Item.FindControl("Label43")).Text);
                }
                sum4 += num43;

                if (((Label)e.Item.FindControl("Label52")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("Label52")).Text.ToString() == System.String.Empty)
                {
                    //((Label)e.Item.FindControl("Label53")).Text = "0";
                    num53 = 0;
                }
                else
                {
                    num53 = Convert.ToDouble(((Label)e.Item.FindControl("Label53")).Text == "" ? "0" : ((Label)e.Item.FindControl("Label53")).Text);
                }
                sum5 += num53;

                if (((Label)e.Item.FindControl("Label63")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("Label62")).Text.ToString() == System.String.Empty)
                {
                    //((Label)e.Item.FindControl("Label63")).Text = "0";
                    num63 = 0;
                }
                else
                {
                    num63 = Convert.ToDouble(((Label)e.Item.FindControl("Label63")).Text == "" ? "0" : ((Label)e.Item.FindControl("Label63")).Text);
                }
                sum6 += num63;


                if (((Label)e.Item.FindControl("Amount")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("Amount")).Text.ToString() == System.String.Empty)
                {
                    ((Label)e.Item.FindControl("Amount")).Text = "0";
                }
                sumamount += Convert.ToDouble(((Label)e.Item.FindControl("Amount")).Text);


                HtmlTableCell dycbj1 = (HtmlTableCell)e.Item.FindControl("dycbj1");
                HtmlTableCell decbj1 = (HtmlTableCell)e.Item.FindControl("decbj1");
                HtmlTableCell zzbj1 = (HtmlTableCell)e.Item.FindControl("zzbj1");

                HtmlTableCell dycbj2 = (HtmlTableCell)e.Item.FindControl("dycbj2");
                HtmlTableCell decbj2 = (HtmlTableCell)e.Item.FindControl("decbj2");
                HtmlTableCell zzbj2 = (HtmlTableCell)e.Item.FindControl("zzbj2");

                HtmlTableCell dycbj3 = (HtmlTableCell)e.Item.FindControl("dycbj3");
                HtmlTableCell decbj3 = (HtmlTableCell)e.Item.FindControl("decbj3");
                HtmlTableCell zzbj3 = (HtmlTableCell)e.Item.FindControl("zzbj3");

                HtmlTableCell dycbj4 = (HtmlTableCell)e.Item.FindControl("dycbj4");
                HtmlTableCell decbj4 = (HtmlTableCell)e.Item.FindControl("decbj4");
                HtmlTableCell zzbj4 = (HtmlTableCell)e.Item.FindControl("zzbj4");

                HtmlTableCell dycbj5 = (HtmlTableCell)e.Item.FindControl("dycbj5");
                HtmlTableCell decbj5 = (HtmlTableCell)e.Item.FindControl("decbj5");
                HtmlTableCell zzbj5 = (HtmlTableCell)e.Item.FindControl("zzbj5");

                HtmlTableCell dycbj6 = (HtmlTableCell)e.Item.FindControl("dycbj6");
                HtmlTableCell decbj6 = (HtmlTableCell)e.Item.FindControl("decbj6");
                HtmlTableCell zzbj6 = (HtmlTableCell)e.Item.FindControl("zzbj6");
                if (Globgysnum == "0")
                {
                    dycbj4.Visible = false; decbj4.Visible = false; zzbj4.Visible = false;
                    dycbj5.Visible = false; decbj5.Visible = false; zzbj5.Visible = false;
                    dycbj6.Visible = false; decbj6.Visible = false; zzbj6.Visible = false;
                }
                else
                {
                    if (Gys1 == "no")
                    {
                        dycbj1.Visible = false; decbj1.Visible = false; zzbj1.Visible = false;
                    }
                    if (Gys2 == "no")
                    {
                        dycbj2.Visible = false; decbj2.Visible = false; zzbj2.Visible = false;
                    }
                    if (Gys3 == "no")
                    {
                        dycbj3.Visible = false; decbj3.Visible = false; zzbj3.Visible = false;
                    }
                    if (Gys4 == "no")
                    {
                        dycbj4.Visible = false; decbj4.Visible = false; zzbj4.Visible = false;
                    }
                    if (Gys5 == "no")
                    {
                        dycbj5.Visible = false; decbj5.Visible = false; zzbj5.Visible = false;
                    }
                    if (Gys6 == "no")
                    {
                        dycbj6.Visible = false; decbj6.Visible = false; zzbj6.Visible = false;
                    }
                }

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {

                ((Label)(e.Item.FindControl("Lb_zxnum"))).Text = Convert.ToString(sumzxnum);
                ((Label)(e.Item.FindControl("Lb_amount"))).Text = Convert.ToString(sumamount);

                ((Label)(e.Item.FindControl("Labeltotal11"))).Text = Convert.ToString(sum1);
                ((Label)(e.Item.FindControl("Labeltotal12"))).Text = Convert.ToString(sum2);
                ((Label)(e.Item.FindControl("Labeltotal13"))).Text = Convert.ToString(sum3);
                ((Label)(e.Item.FindControl("Labeltotal14"))).Text = Convert.ToString(sum4);
                ((Label)(e.Item.FindControl("Labeltotal15"))).Text = Convert.ToString(sum5);
                ((Label)(e.Item.FindControl("Labeltotal16"))).Text = Convert.ToString(sum6);

                HtmlTableCell Td1 = (HtmlTableCell)e.Item.FindControl("Td1");
                HtmlTableCell Td2 = (HtmlTableCell)e.Item.FindControl("Td2");
                HtmlTableCell Td3 = (HtmlTableCell)e.Item.FindControl("Td3");
                HtmlTableCell Td4 = (HtmlTableCell)e.Item.FindControl("Td4");
                HtmlTableCell Td5 = (HtmlTableCell)e.Item.FindControl("Td5");
                HtmlTableCell Td6 = (HtmlTableCell)e.Item.FindControl("Td6");
                if (Globgysnum == "0")
                {
                    Td4.Visible = false; Td5.Visible = false; Td6.Visible = false;
                }
                else
                {
                    if (Gys1 == "no")
                    {
                        Td1.Visible = false;
                    }
                    if (Gys2 == "no")
                    {
                        Td2.Visible = false;
                    }
                    if (Gys3 == "no")
                    {
                        Td3.Visible = false;
                    }
                    if (Gys4 == "no")
                    {
                        Td4.Visible = false;
                    }
                    if (Gys5 == "no")
                    {
                        Td5.Visible = false;
                    }
                    if (Gys6 == "no")
                    {
                        Td6.Visible = false;
                    }
                }
                int i = 10;
                if (CheckBox1.Checked)
                {
                    i--;
                }
                if (CheckBox2.Checked)
                {
                    i--;
                }
                if (CheckBox3.Checked)
                {
                    i--;
                }
                if (CheckBox4.Checked)
                {
                    i--;
                }
                if (CheckBox5.Checked)
                {
                    i--;
                }
                ((HtmlTableCell)e.Item.FindControl("foottd1")).ColSpan = i;
                if (CheckBox6.Checked)
                {
                    ((HtmlTableCell)e.Item.FindControl("tk1")).Visible = false;
                    ((Label)e.Item.FindControl("Lb_zxnum")).Visible = false;
                }
                if (CheckBox9.Checked)
                {
                    ((HtmlTableCell)e.Item.FindControl("tk2")).Visible = false;
                    ((Label)e.Item.FindControl("Lb_amount")).Visible = false;
                }
                if (CheckBox7.Checked)
                {
                    ((HtmlTableCell)e.Item.FindControl("tk3")).Visible = false;
                    ((Label)e.Item.FindControl("Label1")).Visible = false;
                }
                if (CheckBox10.Checked)
                {
                    ((HtmlTableCell)e.Item.FindControl("tk4")).Visible = false;
                    ((Label)e.Item.FindControl("Label9")).Visible = false;
                }
                if (CheckBox11.Checked)
                {
                    ((HtmlTableCell)e.Item.FindControl("tk5")).Visible = false;
                    ((Label)e.Item.FindControl("Label10")).Visible = false;
                }
                if (CheckBox12.Checked)
                {
                    ((HtmlTableCell)e.Item.FindControl("tk6")).Visible = false;
                    ((Label)e.Item.FindControl("Label2")).Visible = false;
                }
            }
        }

        private double sumtotalmoney = 0;
        private double sumtotalnum = 0;
        private double sumtotalzxnum = 0;
        protected void tbpc_comparepriceresultRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string IFFAST2 = ((Label)e.Item.FindControl("IFFAST2")).Text.Trim();
                if (IFFAST2 == "1")
                {
                    ((Label)e.Item.FindControl("lbUrgency2")).Visible = true;
                }
            }

            if (e.Item.ItemIndex >= 0)
            {
                if (((Label)e.Item.FindControl("lbarray")).Text.ToString().Trim() != "")
                {
                    ((Label)e.Item.FindControl("lbarray")).BackColor = System.Drawing.Color.Yellow;
                }

                if (((Label)e.Item.FindControl("Amount")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("Amount")).Text.ToString() == System.String.Empty)
                {
                    ((Label)e.Item.FindControl("Amount")).Text = "0";
                }
                sumtotalmoney += Convert.ToDouble(((Label)e.Item.FindControl("Amount")).Text);
                if (((Label)e.Item.FindControl("PIC_QUANTITY")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("PIC_QUANTITY")).Text.ToString() == System.String.Empty)
                {
                    ((Label)e.Item.FindControl("PIC_QUANTITY")).Text = "0";
                }
                sumtotalnum += Convert.ToDouble(((Label)e.Item.FindControl("PIC_QUANTITY")).Text);
                if (((Label)e.Item.FindControl("PIC_ZXNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("PIC_ZXNUM")).Text.ToString() == System.String.Empty)
                {
                    ((Label)e.Item.FindControl("PIC_ZXNUM")).Text = "0";
                }
                sumtotalzxnum += Convert.ToDouble(((Label)e.Item.FindControl("PIC_ZXNUM")).Text);                
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)(e.Item.FindControl("totalamount"))).Text = Convert.ToString(sumtotalmoney);
                ((Label)(e.Item.FindControl("totalnum"))).Text = Convert.ToString(sumtotalnum);
                ((Label)(e.Item.FindControl("totalzxnum"))).Text = Convert.ToString(sumtotalzxnum);

            }
        }

        protected void btn_chaifen_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sql = "";
            foreach (RepeaterItem Reitem in checked_detailRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                    gloabptcode = ((Label)Reitem.FindControl("PIC_PCODE")).Text.ToString();
                    sql = "select PIC_MASHAPE from TBPC_IQRCMPPRICE where PIC_PTCODE='" + gloabptcode + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        gloabshape = dt.Rows[0]["PIC_MASHAPE"].ToString();
                    }
                }
            }
            if (i == 1)
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "BJDchaifen();", true);
                Response.Redirect("PC_Date_BJDchaifen.aspx?ptcode=" + Server.UrlEncode(gloabptcode) + "&sheetno=" + Server.UrlEncode(TextBoxNO.Text) + "&shape=" + Server.UrlEncode(gloabshape) + "");
            }
            else if (i > 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择一条数据！');", true);
            }
            else if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择一条数据！');", true);
            }
            ItemTemplatedind();
        }

        protected void btn_fuzhi_Click(object sender, EventArgs e)
        {
            int i = 0;
            string ptcode = "";
            foreach (RepeaterItem Reitem in checked_detailRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                    ptcode += ((Label)Reitem.FindControl("PIC_PCODE")).Text.ToString() + ",";
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择数据！');", true);
                ItemTemplatedind();
            }
            else
            {
                ptcode = ptcode.Substring(0, ptcode.LastIndexOf(","));
                Response.Redirect("PC_Data_fuzhiBJD.aspx?ptcode=" + ptcode + "&sheetno=" + TextBoxNO.Text + "");
            }
        }

        public string get_pur_dd(string i)
        {
            string statestr = "";
            if (Convert.ToInt32(i) >= 1)
            {
                statestr = "是";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }
        public string get_pur_hgb(string i)
        {
            string statestr = "";
            if (Convert.ToInt32(i) == 2)
            {
                statestr = "是";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }

        //重置条件
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;
            CheckBox4.Checked = false;
            CheckBox5.Checked = false;
            CheckBox6.Checked = false;
            CheckBox7.Checked = false;
            CheckBox8.Checked = false;
            CheckBox9.Checked = false;
            CheckBox10.Checked = false;
            CheckBox11.Checked = false;
            CheckBox12.Checked = false;
        }

        protected void QueryButton_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext = "select * from TBPC_BJDYC where name='" + Session["UserID"].ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                sqltext = "update TBPC_BJDYC set ";
                if (CheckBox1.Checked)
                {
                    sqltext = sqltext + " ptc='1',";
                }
                else
                {
                    sqltext = sqltext + " ptc='0',";
                }
                if (CheckBox2.Checked)
                {
                    sqltext = sqltext + " marid='1',";
                }
                else
                {
                    sqltext = sqltext + " marid='0',";
                }
                if (CheckBox3.Checked)
                {
                    sqltext = sqltext + " marth='1',";
                }
                else
                {
                    sqltext = sqltext + " marth='0',";
                }
                if (CheckBox4.Checked)
                {
                    sqltext = sqltext + " marcz='1',";
                }
                else
                {
                    sqltext = sqltext + " marcz='0',";
                }
                if (CheckBox5.Checked)
                {
                    sqltext = sqltext + " margb='1',";
                }
                else
                {
                    sqltext = sqltext + " margb='0',";
                }
                if (CheckBox6.Checked)
                {
                    sqltext = sqltext + " cgsl='1',";
                }
                else
                {
                    sqltext = sqltext + " cgsl='0',";
                }
                if (CheckBox7.Checked)
                {
                    sqltext = sqltext + " unit='1',";
                }
                else
                {
                    sqltext = sqltext + " unit='0',";
                }
                if (CheckBox8.Checked)
                {
                    sqltext = sqltext + " shuilv='1',";
                }
                else
                {
                    sqltext = sqltext + " shuilv='0',";
                }
                if (CheckBox9.Checked)
                {
                    sqltext = sqltext + " jine='1',";
                }
                else
                {
                    sqltext = sqltext + " jine='0',";
                }
                if (CheckBox10.Checked)
                {
                    sqltext = sqltext + " length='1',";
                }
                else
                {
                    sqltext = sqltext + " length='0',";
                }
                if (CheckBox11.Checked)
                {
                    sqltext = sqltext + " width='1',";
                }
                else
                {
                    sqltext = sqltext + " width='0',";
                }
                if (CheckBox12.Checked)
                {
                    sqltext = sqltext + " note='1',";
                }
                else
                {
                    sqltext = sqltext + " note='0',";
                }
                if (CheckBox13.Checked)
                {
                    sqltext = sqltext + " fznum='1',";
                }
                else
                {
                    sqltext = sqltext + " fznum='0',";
                }
                if (CheckBox14.Checked)
                {
                    sqltext = sqltext + " fzunit='1',";
                }
                else
                {
                    sqltext = sqltext + " fzunit='0',";
                }
                sqltext = sqltext.Substring(0, sqltext.LastIndexOf(',')).ToString();
                sqltext = sqltext + " where name='" + Session["UserID"].ToString() + "'";
            }
            else
            {
                sqltext = "insert into TBPC_BJDYC (name, ptc, marid, marth, marcz, margb, cgsl, unit, shuilv, jine, length, width, note,fznum,fzunit) values ('" + Session["UserID"].ToString() + "'";
                if (CheckBox1.Checked)
                {
                    sqltext = sqltext + ",'1'";
                }
                else
                {
                    sqltext = sqltext + ",'0'";
                }
                if (CheckBox2.Checked)
                {
                    sqltext = sqltext + ",'1'";
                }
                else
                {
                    sqltext = sqltext + ",'0'";
                }
                if (CheckBox3.Checked)
                {
                    sqltext = sqltext + ",'1'";
                }
                else
                {
                    sqltext = sqltext + ",'0'";
                }
                if (CheckBox4.Checked)
                {
                    sqltext = sqltext + ",'1'";
                }
                else
                {
                    sqltext = sqltext + ",'0'";
                }
                if (CheckBox5.Checked)
                {
                    sqltext = sqltext + ",'1'";
                }
                else
                {
                    sqltext = sqltext + ",'0'";
                }
                if (CheckBox6.Checked)
                {
                    sqltext = sqltext + ",'1'";
                }
                else
                {
                    sqltext = sqltext + ",'0'";
                }
                if (CheckBox7.Checked)
                {
                    sqltext = sqltext + ",'1'";
                }
                else
                {
                    sqltext = sqltext + ",'0'";
                }
                if (CheckBox8.Checked)
                {
                    sqltext = sqltext + ",'1'";
                }
                else
                {
                    sqltext = sqltext + ",'0'";
                }
                if (CheckBox9.Checked)
                {
                    sqltext = sqltext + ",'1'";
                }
                else
                {
                    sqltext = sqltext + ",'0'";
                }
                if (CheckBox10.Checked)
                {
                    sqltext = sqltext + ",'1'";
                }
                else
                {
                    sqltext = sqltext + ",'0'";
                }
                if (CheckBox11.Checked)
                {
                    sqltext = sqltext + ",'1'";
                }
                else
                {
                    sqltext = sqltext + ",'0'";
                }
                if (CheckBox12.Checked)
                {
                    sqltext = sqltext + ",'1'";
                }
                else
                {
                    sqltext = sqltext + ",'0'";
                }
                if (CheckBox13.Checked)
                {
                    sqltext = sqltext + ",'1'";
                }
                else
                {
                    sqltext = sqltext + ",'0'";
                }
                if (CheckBox14.Checked)
                {
                    sqltext = sqltext + ",'1')";
                }
                else
                {
                    sqltext = sqltext + ",'0')";
                }
            }

            DBCallCommon.ExeSqlText(sqltext);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('设置成功！');", true);
            ItemTemplatedind();
            ModalPopupExtenderSearch.Hide();
        }

        protected void cbpower_CheckedChanged(object sender, EventArgs e)
        {
            string a = "0";
            if (cbpower.Checked == true)
            {
                a = "1";
            }

            string sql = " update TBPC_IQRCMPPRCRVW set ICL_ZBYN='" + a + "' where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "' ";
            DBCallCommon.GetDRUsingSqlText(sql);

            Initpage();
            gysnum();
            checked_detailRepeaterdatabind();
            tbpc_comparepriceresultRepeaterdatabind();
            initpower();

        }

        //取消集采
        protected void CancelJc_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            string ptcode = "";
            int i = 0;
            List<string> sqltextlist = new List<string>();

            foreach (RepeaterItem reitem in checked_detailRepeater.Items)
            {
                ptcode = ((Label)reitem.FindControl("PIC_PCODE")).Text;
                sqltext = "select * from TBPC_PURORDERDETAIL where PO_PTCODE='" + ptcode + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    i++;
                }
            }
            if (i > 0)
            {
                sqltext = "update TBPC_IQRCMPPRCRVW set ICL_STATE='0',ICL_TYPE='0' where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                DBCallCommon.ExeSqlText(sqltext); //执行数据库
                Initpage();
                initpower();
                Response.Redirect(url);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('此比价单中数据已经全部下推订单，不能反审！');", true);
                ItemTemplatedind();
            }
        }

        //生成代用单
        protected void btnReplace_Click(object sender, EventArgs e)
        {
            //取消数据
            int i = 0;
            //string sqlstr = "";
            //string ptcode = "";
            //string PTCODE = "";
            List<string> liststr = new List<string>();
            foreach (RepeaterItem Reitem in checked_detailRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        //PTCODE = ((Label)Reitem.FindControl("PIC_PCODE")).Text.ToString();
                        //if (PTCODE.Contains("#") && (!PTCODE.Contains("@")))
                        //{
                        //    ptcode = PTCODE.Substring(0, PTCODE.IndexOf("#"));
                        //}
                        //else
                        //{
                        //    ptcode = PTCODE;
                        //}
                        //sqlstr = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE='4' WHERE PUR_PTCODE='" + ptcode + "'";
                        //DBCallCommon.ExeSqlText(sqlstr);
                        //liststr.Add(sqlstr);
                        //sqlstr = "DELETE FROM TBPC_IQRCMPPRICE WHERE PIC_SHEETNO='" + TextBoxNO.Text + "' AND PIC_PTCODE like '" + ptcode + "%'";
                        //DBCallCommon.ExeSqlText(sqlstr);
                        //liststr.Add(sqlstr);
                    }
                }
            }
            //if (i > 0)
            //{
            //    sqlstr = "SELECT PIC_ID FROM TBPC_IQRCMPPRICE WHERE PIC_SHEETNO='" + TextBoxNO.Text + "'";
            //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlstr);
            //    if (dt.Rows.Count == 0)
            //    {
            //sqlstr = "DELETE FROM TBPC_IQRCMPPRCRVW WHERE ICL_SHEETNO='" + TextBoxNO.Text + "'";
            //DBCallCommon.ExeSqlText(sqlstr);
            //liststr.Add(sqlstr);
            //}
            //}
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
                ItemTemplatedind();
            }
            //生成代用单
            int temp = isselected();
            if (temp == 0)//是否选择数据
            {
                if (is_sameeng())//是否同一批同一工程
                {
                    string sqltext = "";
                    string planpcode = "";
                    string dyptcode = "";
                    string marid = "";
                    double num = 0;
                    double fznum = 0;
                    string note = "";
                    string mpcode = generatecode();
                    string length = "";
                    string width = "";
                    foreach (RepeaterItem Reitem in checked_detailRepeater.Items)
                    {
                        System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                        if (cbx != null)//存在行
                        {
                            if (cbx.Checked)
                            {
                                planpcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("hid_picode")).Text;
                                dyptcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PIC_PCODE")).Text;
                                marid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PIC_MARID")).Text;
                                num = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("PIC_ZXNUM")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("PIC_ZXNUM")).Text);
                                fznum = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("marfznum")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("marfznum")).Text);
                                note = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PIC_NOTE")).Text;
                                length = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PIC_LENGTH")).Text;
                                length = length == "" ? "0" : length;
                                width = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PIC_WIDTH")).Text;
                                width = width == "" ? "0" : width;
                                //sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE='5' WHERE PUR_PTCODE='" + dyptcode + "'";//计划状态改为5为代用
                                //liststr.Add(sqltext);
                                sqltext = "INSERT INTO TBPC_MARREPLACEALL(MP_CODE,MP_PTCODE,MP_MARID,MP_NUM,MP_FZNUM,MP_NOTE,MP_LENGTH,MP_WIDTH) " +
                                           "VALUES('" + mpcode + "','" + dyptcode + "','" + marid + "','" + num + "','" + fznum + "','" + note + "','" + length + "','" + width + "')";
                                liststr.Add(sqltext);
                            }
                        }
                    }

                    //string str = "select ST_ID from TBDS_STAFFINFO where ST_POSITION='0501' and ST_PD='0'";
                    //System.Data.DataTable leader = DBCallCommon.GetDTUsingSqlText(str);
                    //string lead = "";
                    //if (leader.Rows.Count > 0)
                    //{
                    //    lead = leader.Rows[0][0].ToString();
                    //}
                    //if (!string.IsNullOrEmpty(lead))

                    string lead = Tb_shenherencode1.Text;
                  
                    {
                        sqltext = "INSERT INTO TBPC_MARREPLACETOTAL(MP_CODE,MP_PLANPCODE,MP_PJID,MP_ENGID," +
                                 "MP_FILLFMID,MP_FILLFMTIME,MP_REVIEWAID,MP_CHARGEID,MP_LEADER)  " +
                                 "select '" + mpcode + "',PR_SHEETNO,PR_PJID,PR_ENGID,'" + Session["UserID"].ToString() + "','" +
                                 DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',PR_SQREID,PR_FZREID," + lead + " " +
                                 "from TBPC_PCHSPLANRVW where PR_SHEETNO='" + planpcode + "'";

                        liststr.Add(sqltext);
                        DBCallCommon.ExecuteTrans(liststr);
                        Response.Redirect("~/PC_Data/PC_TBPC_Marreplace_panel.aspx?mpno=" + mpcode);//转到代用页面
                    }
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('采购部暂时无部长，无法进行审批操作，请及时与负责人沟通！');", true);
                    //    return;
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择同一批同一工程下的物料,本次操作无效！');", true);
                }
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
            }
            else if (temp == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您选择了代用审核中的数据,本次操作无效！');", true);
            }
            ItemTemplatedind();
        }

        protected int isselected()//判断数据状态
        {
            int temp = 0;
            int i = 0;//是否选择数据
            int k = 0;
            //int count = 0;
            foreach (RepeaterItem Reitem in checked_detailRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        string ptcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PIC_PCODE")).Text;
                        string sql = "select PUR_STATE from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";
                        System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                        if (dt1.Rows.Count > 0)
                        {
                            string state = dt1.Rows[0]["PUR_STATE"].ToString();
                            if (state == "5")
                            {
                                k++;
                            }
                        }
                    }
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else if (k > 0)
            {
                temp = 3;
            }
            else
            {
                temp = 0;//可以下推
            }
            return temp;
        }

        private bool is_sameeng()//判断是否同一批同一工程
        {
            string temppcode = "";
            bool temp = true;
            int i = 0;
            foreach (RepeaterItem Reitem in checked_detailRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        if (i == 1)
                        {
                            temppcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("hid_picode")).Text.ToString();
                        }
                        else
                        {
                            if (temppcode != ((System.Web.UI.WebControls.Label)Reitem.FindControl("hid_picode")).Text.ToString())
                            {
                                temp = false;
                                break;
                            }
                        }
                    }
                }
            }
            return temp;
        }

        private string generatecode()//生成代用单号
        {
            string subcode = "";
            string mpcode = "";
            string sqltext = "SELECT TOP 1 MP_CODE FROM TBPC_MARREPLACETOTAL WHERE MP_CODE LIKE '" + DateTime.Now.Year.ToString() + "1" + "%' ORDER BY MP_CODE DESC";
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
            mpcode = DateTime.Now.Year.ToString() + "1" + subcode;
            return mpcode;
        }

        //成套标识
        protected void chk_array_CheckedChanged(object sender, EventArgs e)
        {
            List<string> listsqlsetarray = new List<string>();
            int num = 0;
            string arraybh = DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + Session["UserId"].ToString().Trim();
            for (int i = 0; i < checked_detailRepeater.Items.Count; i++)
            {
                if (((CheckBox)checked_detailRepeater.Items[i].FindControl("CKBOX_SELECT")).Checked)
                {
                    num++;
                    if (chk_array.Checked)
                    {
                        listsqlsetarray.Add("update TBPC_IQRCMPPRICE set PIC_ARRAY='" + arraybh + "' where PIC_PTCODE='" + ((Label)checked_detailRepeater.Items[i].FindControl("PIC_PCODE")).Text.Trim() + "'");
                    }
                }
            }
            if (num == 0 && chk_array.Checked)
            {
                chk_array.Checked = false;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要标识的数据！');", true);
                ItemTemplatedind();
                return;
            }
            else if (num > 0 && chk_array.Checked)
            {
                chk_array.Checked = false;
                DBCallCommon.ExecuteTrans(listsqlsetarray);
                ItemTemplatedind();
            }
        }

        //取消标识
        protected void chk_qxarray_CheckedChanged(object sender, EventArgs e)
        {
            List<string> listsqlsetqxarray = new List<string>();
            int num = 0;
            for (int i = 0; i < checked_detailRepeater.Items.Count; i++)
            {
                if (((CheckBox)checked_detailRepeater.Items[i].FindControl("CKBOX_SELECT")).Checked)
                {
                    num++;
                    if (chk_qxarray.Checked)
                    {
                        listsqlsetqxarray.Add("update TBPC_IQRCMPPRICE set PIC_ARRAY=NULL where PIC_PTCODE='" + ((Label)checked_detailRepeater.Items[i].FindControl("PIC_PCODE")).Text.Trim() + "'");
                    }
                }
            }
            if (num == 0 && chk_qxarray.Checked)
            {
                chk_qxarray.Checked = false;
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要标识的数据！');", true);
                ItemTemplatedind();
                return;
            }
            else if (num > 0 && chk_qxarray.Checked)
            {
                chk_qxarray.Checked = false;
                DBCallCommon.ExecuteTrans(listsqlsetqxarray);
                ItemTemplatedind();
            }
        }
    }
}
