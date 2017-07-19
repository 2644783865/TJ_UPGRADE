using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;


namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Xie_check_detail : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            //btn_nosto.Attributes.Add("style", "display:none");
            btn_delete.Attributes.Add("OnClick","Javascript:return confirm('确定要删除吗！');");
            if (!IsPostBack)
            {
                Initpage();
                gysnum();
                checked_detailRepeaterdatabind();
                tbpc_comparepriceresultRepeaterdatabind();
                initpower();
            }
            //CheckUser(ControlFinder);
        }
        //供应商数量
        private void gysnum()
        {
            int i = 0;
            string sql = "select distinct PIC_SUPPLIERAID,PIC_SUPPLIERBID,PIC_SUPPLIERCID,PIC_SUPPLIERDID,PIC_SUPPLIEREID,PIC_SUPPLIERFID from  TBMP_IQRCMPPRICE where PIC_SHEETNO='" + TextBoxNO.Text + "'";
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

            TextBoxNO.Text = gloabsheetno;//单号
            string sqltext = "";
            sqltext = "select PIC_MNAME as marnm, PIC_GUIGE as margg, PIC_CAIZHI as marcz,ICL_ZBYN,zdrid  as ICL_REVIEWA,zdrnm  as ICL_REVIEWANM,zdtime  as ICL_REVIEWATIME, zdryj as ICL_REVIEWAADVC," +
                     "shbid as ICL_REVIEWB,shbnm as ICL_REVIEWBNM,shbtime as ICL_REVIEWBTIME,shbyj as ICL_REVIEWBADVC," +
                     "shcid as ICL_REVIEWC,shcnm as ICL_REVIEWCNM,shctime as ICL_REVIEWCTIME,shcyj as ICL_REVIEWCADVC," +
                     "shdid as ICL_REVIEWD,shdnm as ICL_REVIEWDNM,shdtime as ICL_REVIEWDTIME,shdyj as ICL_REVIEWDADVC," +
                     "sheid as ICL_REVIEWE,shenm as ICL_REVIEWENM,shetime as ICL_REVIEWETIME,sheyi as ICL_REVIEWEADVC," +
                     "shfid as ICL_REVIEWF,shfnm as ICL_REVIEWFNM,shftime as ICL_REVIEWFTIME,shfyj as ICL_REVIEWFADVC," +
                     "shgid as ICL_REVIEWG,shgnm as ICL_REVIEWGNM,shgtime as ICL_REVIEWGTIME,shgyj as ICL_REVIEWGADVC," +
                     "isnull(totalstate,0) as ICL_STATE,isnull(statea,0) as ICL_STATEA,isnull(stateb,0) as ICL_STATEB,isnull(statec,0) as ICL_STATEC,isnull(stated,0) as ICL_STATED,isnull(statee,0) as ICL_STATEE,isnull(statef,0) as ICL_STATEF " +
                     "from View_TBMP_IQRCMPPRCRVW  where picno='" + gloabsheetno + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                TB_zdanrenid.Text = dt.Rows[0]["ICL_REVIEWA"].ToString();
                Tb_zdanren.Text = dt.Rows[0]["ICL_REVIEWANM"].ToString();
                Tb_tjiaot.Text = dt.Rows[0]["ICL_REVIEWATIME"].ToString();
                Tb_zdanyj.Text = dt.Rows[0]["ICL_REVIEWAADVC"].ToString();
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
            }
            if ((dt.Rows[0]["ICL_STATE"].ToString() == "0" || dt.Rows[0]["ICL_STATE"].ToString() == "2") && Session["UserID"].ToString() == dt.Rows[0]["ICL_REVIEWA"].ToString())
            {
                btn_edit.Enabled = true;
                btn_split.Enabled = true;
                btn_delete.Enabled = true;
                btn_cancel.Enabled = true;
            }
            else
            {
                btn_edit.Enabled = false;
                btn_split.Enabled = false;
                btn_delete.Enabled = false;
                btn_cancel.Enabled = false;
            }
            //2015-04-02加
            if (dt.Rows[0]["ICL_STATE"].ToString()=="4")
            {
                 btn_split.Enabled = true;
            }
        }

        //添加审批人
        private void initpower()
        {
            //
            string sqltext = "";
            string ptcode = "";
            double amount = Convert.ToDouble(((Label)(tbpc_comparepriceresultRepeater.Controls[tbpc_comparepriceresultRepeater.Controls.Count - 1].FindControl("totalamount"))).Text);
            int num = 0;
            string shren1id = "";
            string shren2id = "";
            //string shren3id = "";

            string shren1nm = "";
            string shren2nm = "";
            //string shren3nm = "";

            string[] strsid1 = { };
            string[] strsid2 = { };
            string[] strsid3 = { };
            string[] strolds = { };

            num = 3;
            tb_pnum.Text = "3";
            if (num > 0)
            {
                //shren1id = "7";
                //shren1nm = "董治收";
                ////shren2id = "2";
                ////shren2nm = "王福泉";
                //shren2id = "95";
                //shren2nm = "于来义";
                string sqltext1 = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_POSITION='0401' and ST_PD='0'";//生产部门负责人
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext1);
                shren1id = dt1.Rows[0]["ST_ID"].ToString();
                shren1nm = dt1.Rows[0]["ST_NAME"].ToString();
                string sqltext2 = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_POSITION='0102' and ST_PD='0'order by ST_ID desc";//公司经理
                DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext2);
                shren2id = dt2.Rows[0]["ST_ID"].ToString();
                shren2nm = dt2.Rows[0]["ST_NAME"].ToString();

                if (shren1nm == Tb_shenheren1.Text | Tb_shenheren1.Text == "")
                {
                    Tb_shenheren1.Text = shren1nm;
                    Tb_shenherencode1.Text = shren1id;
                }
                if (shren2nm == Tb_shenheren2.Text | Tb_shenheren2.Text == "")
                {
                    Tb_shenheren2.Text = shren2nm;
                    Tb_shenherencode2.Text = shren2id;
                }
                Pan_shenheren2.Visible = true;
            }
            sqltext = "select PIC_MNAME as marnm, PIC_GUIGE as margg, PIC_CAIZHI as marcz, zdrid  as ICL_REVIEWA,zdrnm  as ICL_REVIEWANM,zdtime  as ICL_REVIEWATIME, zdryj as ICL_REVIEWAADVC," +
                     "shbid as ICL_REVIEWB,shbnm as ICL_REVIEWBNM,shbtime as ICL_REVIEWBTIME,shbyj as ICL_REVIEWBADVC," +
                     "shcid as ICL_REVIEWC,shcnm as ICL_REVIEWCNM,shctime as ICL_REVIEWCTIME,shcyj as ICL_REVIEWCADVC," +
                     "shdid as ICL_REVIEWD,shdnm as ICL_REVIEWDNM,shdtime as ICL_REVIEWDTIME,shdyj as ICL_REVIEWDADVC," +
                     "sheid as ICL_REVIEWE,shenm as ICL_REVIEWENM,shetime as ICL_REVIEWETIME,sheyi as ICL_REVIEWEADVC," +
                     "isnull(totalstate,0) as ICL_STATE,isnull(statea,0) as ICL_STATEA,isnull(stateb,0) as ICL_STATEB,isnull(statec,0) as ICL_STATEC,isnull(stated,0) as ICL_STATED " +
                     "from View_TBMP_IQRCMPPRCRVW  where picno='" + gloabsheetno + "'";
            dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            {
                //strolds = new string[] { Tb_shenherencode2.Text, Tb_shenherencode3.Text, Tb_shenherencode4.Text, Tb_shenherencode5.Text };
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

                }
                else
                {
                    if (dt.Rows[0]["ICL_STATEA"].ToString() == "2" && dt.Rows[0]["ICL_STATEB"].ToString() == "0")
                    {
                        if (Session["UserID"].ToString() == Tb_shenherencode2.Text)
                        {
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
                         "FROM View_TBMP_IQRCMPPRICE where picno='" + TextBoxNO.Text.ToString() + "'";
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
            sqltext = "SELECT PTC,PIC_ZONGXU, MS_CODE,MS_UWGHT,MS_PROCESS,PIC_WXTYPE, PIC_MNAME as marnm,PIC_MASHAPE,PIC_ID,PIC_TASKID, PIC_GUIGE as margg, PIC_CAIZHI as marcz, picno, pjid, pjnm, engid, engnm, marid,PIC_MASHAPE ,marnm, margg, margb, marcz, marunit, " +
                     "marfzunit, length, width, CONVERT(float, marnum) AS marnum, CONVERT(float, marfznum) AS marfznum, CONVERT(float, marzxnum) AS marzxnum, CONVERT(float, marzxfznum) AS marzxfznum, " +
                    "supplierresid, supplierresnm, supplierresrank, shuilv, CONVERT(float, price) AS price, CONVERT(float, detamount) AS detamount, ptcode, qoutefstsa, " +
                    "CONVERT(float, qoutescdsa) AS qoutescdsa, CONVERT(float, qoutelstsa) AS qoutelstsa, qoutefstsb, CONVERT(float, qoutescdsb) AS qoutescdsb, CONVERT(float, qoutelstsb) AS qoutelstsb, qoutefstsc, CONVERT(float, qoutescdsc) AS qoutescdsc, " +
                    "CONVERT(float, qoutelstsc) AS qoutelstsc, qoutefstsd, CONVERT(float, qoutescdsd) AS qoutescdsd, CONVERT(float, qoutelstsd) AS qoutelstsd, qoutefstse, CONVERT(float, qoutescdse) AS qoutescdse, CONVERT(float, qoutelstse) AS qoutelstse, " +
                    "qoutefstsf, CONVERT(float, qoutescdsf) AS qoutescdsf, CONVERT(float, qoutelstsf) AS qoutelstsf, pmode, keycoms, zdjprice, zdjnum, isnull(detailstate,0) as detailstate,PIC_TUHAO, " +
                    " ''AS minprice, " +
                     " ''as lastprice, " +
                    "isnull(detailcstate,0) as detailcstate, (MS_NOTE+'/'+CAST(MS_XHBZ as varchar(8000))+'/'+MS_ALLBEIZHU)as detailnote  FROM View_TBMP_IQRCMPPRICE  where picno='" + TextBoxNO.Text.ToString() + "' and    (PIC_CFSTATE='0' or PIC_CFSTATE='2') order by ptcode ASC";
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
        }
        protected void btn_edit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PM_Data/PM_Xie_comprise.aspx?sheetno=" + gloabsheetno + "");
        }
        protected void Rad_tongyi1_checkedchanged(object sender, EventArgs e)
        {
            settbtongyitext(Rad_tongyi1, Tb_shenheyj1);
        }
        protected void Rad_tongyi2_checkedchanged(object sender, EventArgs e)
        {
            settbtongyitext(Rad_tongyi2, Tb_shenheyj2);
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

        /// <summary>
        ///  TODO: 确定,提交审核信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            string sqltext = "";
            string state = "";
            string statea = "";
            string stateb = "";
            string statec = "";

            sqltext = "select ICL_STATE,ICL_STATEA,ICL_STATEB,ICL_STATEC " +
                      "from TBMP_IQRCMPPRCRVW where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
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
                double amount = 0;
                Label amounttex = checked_detailRepeater.Controls[checked_detailRepeater.Controls.Count - 1].FindControl("Lb_amount") as Label;
                if (amounttex.Text != "")
                {
                    amount = Convert.ToDouble(amounttex.Text);
                }
                if (Tb_zdanyj.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写意见！');", true);
                    return;
                }
                else
                {
                    sqltext = "UPDATE TBMP_IQRCMPPRCRVW SET ICL_REVIEWATIME=(case when (ICL_REVIEWATIME='' or ICL_REVIEWATIME is null) then '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' else  ICL_REVIEWATIME end)," +
                              "ICL_REVIEWAADVC='" + Tb_zdanyj.Text.ToString() + "',ICL_REVIEWB='" + Tb_shenherencode1.Text + "'," +
                              "ICL_REVIEWBADVC='',ICL_REVIEWC='" + Tb_shenherencode2.Text + "',ICL_REVIEWCADVC=''," +
                              "ICL_REVIEWDADVC=''," +
                              "ICL_STATEA='0'," +
                              "ICL_STATEB='0',ICL_STATEC='0',ICL_STATED='0',ICL_STATEE='0',ICL_STATEF='0',ICL_STATE='1',ICL_AMOUT=" + amount + "  " +
                              "WHERE ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Tb_shenherencode1.Text), new List<string>(), new List<string>(), "外协比价单审批", "您有新的外协比价单（"+TextBoxNO.Text.ToString()+"）需要审批，请登录系统查看");
                }
            }
            //如果审核人2和审核人3为同一人，则同时更新两项内容。
            else if (state == "1")
            {
                if (Rad_tongyi1.Checked)
                {
                    sqltext = "UPDATE TBMP_IQRCMPPRCRVW SET ICL_REVIEWB='" + Tb_shenherencode1.Text + "'," +
                          "ICL_REVIEWBTIME='" + Tb_shenhet1.Text.ToString() + "'," +
                          "ICL_REVIEWBADVC='" + Tb_shenheyj1.Text.ToString() + "',ICL_STATE='3',ICL_STATEA='2' " +
                          "WHERE ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                }
                else
                {
                    if (Tb_shenheyj1.Text.ToString().Replace(" ", "") == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写拒绝理由！');", true);
                        return;
                    }
                    else
                    {
                        //sqltext = "UPDATE TBMP_IQRCMPPRCRVW SET ICL_REVIEWB='" + Tb_shenherencode1.Text + "'," +
                        //                                 "ICL_REVIEWBTIME='" + Tb_shenhet1.Text.ToString() + "'," +
                        //                                 "ICL_REVIEWBADVC='" + Tb_shenheyj1.Text.ToString() + "',ICL_REVIEWCTIME='" + Tb_shenhet1.Text.ToString() + "',ICL_REVIEWCADVC='" + Tb_shenheyj1.Text.ToString() + "',ICL_STATE='2',ICL_STATEA='1',ICL_STATEB='1' " +
                        //                                 "WHERE ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                        sqltext = "UPDATE TBMP_IQRCMPPRCRVW SET ICL_REVIEWB='" + Tb_shenherencode1.Text + "'," +
                                                         "ICL_REVIEWBTIME='" + Tb_shenhet1.Text.ToString() + "'," +
                                                         "ICL_REVIEWBADVC='" + Tb_shenheyj1.Text.ToString() + "',ICL_STATE='2',ICL_STATEA='1',ICL_STATEB='1' " + "WHERE ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(Tb_shenherencode2.Text), new List<string>(), new List<string>(), "外协比价单审批", "您有新的外协比价单（" + TextBoxNO.Text.ToString() + "）需要审批，请登录系统查看");

                    }
                }
            }
            else if (state == "3")
            {
                if (Pan_shenheren2.Enabled == true)
                {
                    if (Rad_tongyi2.Checked)
                    {
                        sqltext = "UPDATE TBMP_IQRCMPPRCRVW SET ICL_REVIEWC='" + Tb_shenherencode2.Text + "'," +
                                  "ICL_REVIEWCTIME='" + Tb_shenhet2.Text.ToString() + "'," +
                                  "ICL_REVIEWCADVC='" + Tb_shenheyj2.Text.ToString() + "',ICL_STATE='4',ICL_STATEB='2' " +
                                  "WHERE ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                    }
                    else
                    {
                        if (Tb_shenheyj2.Text.ToString().Replace(" ", "") == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写拒绝理由！');", true);
                            return;
                        }
                        else
                        {
                            sqltext = "UPDATE TBMP_IQRCMPPRCRVW SET ICL_REVIEWC='" + Tb_shenherencode2.Text + "'," +
                                                                "ICL_REVIEWCTIME='" + Tb_shenhet2.Text.ToString() + "'," +
                                                                "ICL_REVIEWCADVC='" + Tb_shenheyj2.Text.ToString() + "',ICL_STATE='2',ICL_STATEB='1'  " +
                                                                "WHERE ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
                        }

                    }
                }
                //2013年2月27日 08:31:09 +++++++++
            }
            sqltextlist.Add(sqltext);
            sqltext = "update TBMP_IQRCMPPRCRVW set ICL_STATE= case when ICL_STATEA='2' and ICL_STATEB='2'  then '4' " +
                                                                       "when (ICL_STATEA='1' or ICL_STATEB='1') and  " +
                                                                       "(ICL_STATEA<>'0' and ICL_STATEB<>'0') then '2' " +
                                                                       "else ICL_STATE " +
                                                                       "end " +
                          "where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
            sqltextlist.Add(sqltext);
            DBCallCommon.ExecuteTrans(sqltextlist);
            //bool bohui = false;
            //string sqltxt1 = "select * from TBMP_IQRCMPPRCRVW where ICL_STATE='2'and ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
            //DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltxt1);
            //if (dt1.Rows.Count >= 1)
            //{
            //    bohui = true;
            //}
            //if (bohui)//如果单据被驳回
            //{
            //    foreach (RepeaterItem reitem in checked_detailRepeater.Items)
            //    {
            //        string msid = ((Label)reitem.FindControl("PIC_TASKID")).Text.ToString();
            //        string mscode = ((Label)reitem.FindControl("MS_CODE")).Text.ToString();
            //        //string sqltxt = "update View_TM_TASKDQO set MS_scwaixie='1' where MS_ID='" + msid + "'";
            //        //DBCallCommon.ExeSqlText(sqltxt);
            //        string sqltxt10 = "update TBPM_WXDetail set MS_scwaixie='1' where MS_ID='" + msid + "'or MS_CODE='"+mscode+"'";
            //        DBCallCommon.ExeSqlText(sqltxt10);
            //    }
            //}                 
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交审核成功！');", true);
           // Page.ClientScript.RegisterStartupScript(this.GetType(), "test", "<script language=javascript>if(confirm('提交审核成功! 是否要返回前一页面?'))document.getElementById('" + btn_nosto.ClientID + "').click();</script>");
        }
        protected void btn_nosto_Click(object sender, EventArgs e)//无库存可用
        {
        }

        //返审   2013年3月1日 14:41:42
        //protected void btn_fanshen_Click(object sender, EventArgs e)
        //{

        //    string sqltext = "";
        //    string ptcode = "";
        //    string state = "";
        //    string statea = "";
        //    string stateb = "";
        //    string statec = "";
        //    string stated = "";
        //    //2013年2月27日 08:34:24
        //    int i = 0;
        //    List<string> sqltextlist = new List<string>();

        //    foreach (RepeaterItem reitem in checked_detailRepeater.Items)
        //    {
        //        ptcode = ((Label)reitem.FindControl("PIC_PCODE")).Text;
        //        sqltext = "select * from TBMP_PURORDERDETAIL where PO_PTCODE='" + ptcode + "'";
        //        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
        //        if (dt.Rows.Count == 0)
        //        {
        //            i++;
        //        }
        //    }
        //    if (i > 0)
        //    {
        //        sqltext = "select ICL_STATE,ICL_STATEA,ICL_STATEB,ICL_STATEC,ICL_STATED,ICL_STATEE,ICL_STATEF " +
        //                             "from TBMP_IQRCMPPRCRVW where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
        //        SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
        //        while (dr.Read())
        //        {
        //            state = dr["ICL_STATE"].ToString();
        //            statea = dr["ICL_STATEA"].ToString();
        //            stateb = dr["ICL_STATEB"].ToString();
        //            statec = dr["ICL_STATEC"].ToString();
        //            stated = dr["ICL_STATED"].ToString();
        //        }
        //        dr.Close();

        //        //为了避免两个相同评审人的情况出现，造成返审失败，这里做了改动  2013年3月1日 15:00:43
        //        List<string> sqlfs = new List<string>();
        //        if (Session["UserID"].ToString() == TB_zdanrenid.Text && state == "1")//制单人
        //        {
        //            string sqltext0 = "update TBMP_IQRCMPPRCRVW set ICL_STATE='0',ICL_REVIEWAADVC='',ICL_REVIEWATIME='' where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
        //            sqlfs.Add(sqltext0);
        //        }

        //        else if (Session["UserID"].ToString() == "0601002" && (state == "2" || state == "4" || state == "3") && statea != "0" && stateb != "0" && statec == "0" && stated == "0")
        //        {
        //            string sqltext7 = "update TBMP_IQRCMPPRCRVW set ICL_STATE='1',ICL_STATEA='0',ICL_STATEB='0',ICL_REVIEWBADVC='',ICL_REVIEWBTIME='',ICL_REVIEWCADVC='',ICL_REVIEWCTIME='' where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
        //            sqlfs.Add(sqltext7);
        //        }
        //        else if (Session["UserID"].ToString() == Tb_shenherencode1.Text && (state == "2" || state == "4" || state == "3") && statea != "0" && stateb == "0" && statec == "0" && stated == "0")
        //        {
        //            string sqltext1 = "update TBMP_IQRCMPPRCRVW set ICL_STATE='1',ICL_STATEA='0',ICL_REVIEWBADVC='',ICL_REVIEWBTIME=''   where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
        //            sqlfs.Add(sqltext1);
        //        }
        //        else if (Session["UserID"].ToString() == Tb_shenherencode2.Text && (state == "2" || state == "4" || state == "3") && stateb != "0")
        //        {
        //            string sqltext2 = "update TBMP_IQRCMPPRCRVW set ICL_STATE='3',ICL_STATEB='0',ICL_REVIEWCADVC='',ICL_REVIEWCTIME=''   where ICL_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
        //            sqlfs.Add(sqltext2);
        //        }


        //        DBCallCommon.ExecuteTrans(sqlfs); //执行数据库
        //        Initpage();
        //        initpower();
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('此比价单中数据已经全部下推订单，不能反审！');", true);
        //    }

        //}

        /// <summary>
        /// 根据用户ID获取邮件地址
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static string GetEmailAddressByUserID(string userid)
        {
            string retEmail = "";
            string sql = "SELECT [EMAIL] FROM [dbo].[TBDS_STAFFINFO] WHERE [ST_ID]='" + userid + "'";
            SqlDataReader dr_email = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr_email.HasRows)
            {
                dr_email.Read();
                retEmail = dr_email["EMAIL"].ToString();
            }
            dr_email.Close();
            return retEmail;
        }
        protected void btn_back_Click(object sender, EventArgs e)//返回
        {
            Response.Redirect("PM_Xie_Mana_List.aspx");
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
            if (e.Item.ItemIndex >= 0)
            {
                //if (((Label)e.Item.FindControl("PIC_ZXNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("PIC_ZXNUM")).Text.ToString() == System.String.Empty)
                //{
                //    //((Label)e.Item.FindControl("PIC_ZXNUM")).Text = "0";
                //    cgnum = 0;
                //}
                //else
                //{
                //    cgnum = Convert.ToDouble(((Label)e.Item.FindControl("PIC_ZXNUM")).Text);
                //}
                //sumzxnum += cgnum;

                if (((Label)e.Item.FindControl("Label12")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("Label12")).Text.ToString() == System.String.Empty)
                {
                    //((Label)e.Item.FindControl("Label13")).Text = "0";
                    num13 = 0;
                }
                else
                {
                    num13 = Convert.ToDouble(((Label)e.Item.FindControl("Label12")).Text);
                }
                sum1 += cgnum * num13;

                if (((Label)e.Item.FindControl("Label22")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("Label22")).Text.ToString() == System.String.Empty)
                {
                    //((Label)e.Item.FindControl("Label23")).Text = "0";
                    num23 = 0;
                }
                else
                {
                    num23 = Convert.ToDouble(((Label)e.Item.FindControl("Label22")).Text);
                }
                sum2 += cgnum * num23;

                if (((Label)e.Item.FindControl("Label32")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("Label32")).Text.ToString() == System.String.Empty)
                {
                    //((Label)e.Item.FindControl("Label33")).Text = "0";
                    num33 = 0;
                }
                else
                {
                    num33 = Convert.ToDouble(((Label)e.Item.FindControl("Label32")).Text);
                }
                sum3 += cgnum * num33;

                if (((Label)e.Item.FindControl("Label42")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("Label42")).Text.ToString() == System.String.Empty)
                {
                    //((Label)e.Item.FindControl("Label43")).Text = "0";
                    num43 = 0;
                }
                else
                {
                    num43 = Convert.ToDouble(((Label)e.Item.FindControl("Label42")).Text);
                }
                sum4 += cgnum * num43;

                if (((Label)e.Item.FindControl("Label52")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("Label52")).Text.ToString() == System.String.Empty)
                {
                    //((Label)e.Item.FindControl("Label53")).Text = "0";
                    num53 = 0;
                }
                else
                {
                    num53 = Convert.ToDouble(((Label)e.Item.FindControl("Label52")).Text);
                }
                sum5 += cgnum * num53;

                if (((Label)e.Item.FindControl("Label63")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("Label62")).Text.ToString() == System.String.Empty)
                {
                    //((Label)e.Item.FindControl("Label63")).Text = "0";
                    num63 = 0;
                }
                else
                {
                    num63 = Convert.ToDouble(((Label)e.Item.FindControl("Label62")).Text);
                }
                sum6 += cgnum * num63;


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

            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                //((Label)(e.Item.FindControl("Lb_zxnum"))).Text = Convert.ToString(sumzxnum);
                ((Label)(e.Item.FindControl("Lb_amount"))).Text = Convert.ToString(sumamount);
                //((Label)(e.Item.FindControl("Labeltotal11"))).Text = Convert.ToString(sum1);
                //((Label)(e.Item.FindControl("Labeltotal12"))).Text = Convert.ToString(sum2);
                //((Label)(e.Item.FindControl("Labeltotal13"))).Text = Convert.ToString(sum3);
                //((Label)(e.Item.FindControl("Labeltotal14"))).Text = Convert.ToString(sum4);
                //((Label)(e.Item.FindControl("Labeltotal15"))).Text = Convert.ToString(sum5);
                //((Label)(e.Item.FindControl("Labeltotal16"))).Text = Convert.ToString(sum6);
            }
        }

        private double sumtotalmoney = 0;
        private double sumtotalnum = 0;
        private double sumtotalzxnum = 0;
        protected void tbpc_comparepriceresultRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemIndex >= 0)
            {

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
                //((Label)(e.Item.FindControl("totalnum"))).Text = Convert.ToString(sumtotalnum);
                //((Label)(e.Item.FindControl("totalzxnum"))).Text = Convert.ToString(sumtotalzxnum);

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

        /// <summary>
        /// 拆分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_onclick(object sender, EventArgs e)
        {
            int i = 0;
            foreach (RepeaterItem Reitem in checked_detailRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                }
            }
            if (i > 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勿选择多条数据！');", true);
            }
            else if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择需要拆分的数据！');", true);
            }
            else if (i == 1)
            {
                string sqltext = "";
                string sqltext1="";
                string sqltext2="";
                string sqltext3="";
                string sqltext4="";
                int txt1 = 0;
                int txt2 = 0;
                int txt3 = 0;
                int txt4 = 0;
                List<string> list=new List<string>();
                foreach (RepeaterItem Reitem in checked_detailRepeater.Items)
                {
                    CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                    string picid = ((Label)Reitem.FindControl("PIC_ID")).Text ;
                    int num = Convert.ToInt32(((Label)Reitem.FindControl("PIC_ZXNUM")).Text);

                    if (cbx.Checked)
                    { 
                        list.Clear();
                        if(isselect())
                        {
                            switch (ddl_split.SelectedValue)
                            {
                                case "2":
                                    txt1 = Convert.ToInt32(txt_1.Text.Trim().ToString());
                                    txt2 = Convert.ToInt32(txt_2.Text.Trim().ToString());
                                    if (num == (txt1 + txt2))
                                    {
                                        //sqltext1 = "INSERT INTO  TBMP_IQRCMPPRICE select PIC_CODE,'',PIC_SHEETNO, PIC_ZONGXU, PIC_JGNUM, PIC_JSNUM, PIC_BJSTATUS, PIC_RKSTATUS, PIC_TASKID, PIC_WXTYPE, PIC_TUHAO, PIC_PJID, PIC_ENGID, PIC_ENGNAME, PIC_MARID, PIC_MNAME, PIC_GUIGE, PIC_CAIZHI, PIC_MASHAPE, PIC_QUANTITY, PIC_FZNUM, PIC_ZXNUM='" + txt_1.Text.Trim().ToString() + "', PIC_ZXFZNUM, PIC_LENGTH, PIC_WIDTH, PIC_SUPPLIERAID, PIC_SUPPLIERBID, PIC_SUPPLIERCID, PIC_SUPPLIERDID, PIC_SUPPLIEREID, PIC_SUPPLIERFID, PIC_SUPPLIERRESID, PIC_SUPPLYTIME, PIC_SHUILV, PIC_PRICE, PIC_PTCODE, PIC_QOUTEFSTSA, PIC_QOUTESCDSA, PIC_QOUTELSTSA, PIC_QOUTEFSTSB, PIC_QOUTESCDSB, PIC_QOUTELSTSB, PIC_QOUTEFSTSC, PIC_QOUTESCDSC, PIC_QOUTELSTSC, PIC_QOUTEFSTSD, PIC_QOUTESCDSD, PIC_QOUTELSTSD, PIC_QOUTEFSTSE, PIC_QOUTESCDSE, PIC_QOUTELSTSE, PIC_QOUTEFSTSF, PIC_QOUTESCDSF, PIC_QOUTELSTSF, PIC_PMODE, PIC_KEYCOMS, PIC_ZDJPRICE, PIC_ZDJNUM, PIC_STATE, PIC_CSTATE, PIC_NOTE,PIC_CFSTATE='2',PIC_ORDERSTATE,PO_OperateState=null from TBMP_IQRCMPPRICE where  PIC_ID='" + picid + "'";
                                        sqltext1 = "INSERT INTO  TBMP_IQRCMPPRICE select PIC_CODE,'',PIC_SHEETNO, PIC_ZONGXU, PIC_JGNUM, PIC_JSNUM, PIC_BJSTATUS, PIC_RKSTATUS, PIC_TASKID, PIC_WXTYPE, PIC_TUHAO, PIC_PJID, PIC_ENGID, PIC_ENGNAME, PIC_MARID, PIC_MNAME, PIC_GUIGE, PIC_CAIZHI, PIC_MASHAPE, PIC_QUANTITY, PIC_FZNUM, PIC_ZXNUM='" + txt_1.Text.Trim().ToString() + "', PIC_ZXFZNUM, PIC_LENGTH, PIC_WIDTH, PIC_SUPPLIERAID, PIC_SUPPLIERBID, PIC_SUPPLIERCID, PIC_SUPPLIERDID, PIC_SUPPLIEREID, PIC_SUPPLIERFID, PIC_SUPPLIERRESID, PIC_SUPPLYTIME, PIC_SHUILV, PIC_PRICE, PIC_PTCODE, PIC_QOUTEFSTSA, PIC_QOUTESCDSA, PIC_QOUTESCDSA*" + txt_1.Text.Trim().ToString() + ", PIC_QOUTEFSTSB, PIC_QOUTESCDSB, PIC_QOUTELSTSB, PIC_QOUTEFSTSC, PIC_QOUTESCDSC, PIC_QOUTELSTSC, PIC_QOUTEFSTSD, PIC_QOUTESCDSD, PIC_QOUTELSTSD, PIC_QOUTEFSTSE, PIC_QOUTESCDSE, PIC_QOUTELSTSE, PIC_QOUTEFSTSF, PIC_QOUTESCDSF, PIC_QOUTELSTSF, PIC_PMODE, PIC_KEYCOMS, PIC_ZDJPRICE, PIC_ZDJNUM, PIC_STATE, PIC_CSTATE, PIC_NOTE,PIC_CFSTATE='2',PIC_ORDERSTATE,PO_OperateState=null from TBMP_IQRCMPPRICE where  PIC_ID='" + picid + "'";

                                        //sqltext2 = "INSERT INTO  TBMP_IQRCMPPRICE select PIC_CODE,'', PIC_SHEETNO, PIC_ZONGXU, PIC_JGNUM, PIC_JSNUM, PIC_BJSTATUS, PIC_RKSTATUS, PIC_TASKID, PIC_WXTYPE, PIC_TUHAO, PIC_PJID, PIC_ENGID, PIC_ENGNAME, PIC_MARID, PIC_MNAME, PIC_GUIGE, PIC_CAIZHI, PIC_MASHAPE, PIC_QUANTITY, PIC_FZNUM, PIC_ZXNUM='" + txt_2.Text.Trim().ToString() + "', PIC_ZXFZNUM, PIC_LENGTH, PIC_WIDTH, PIC_SUPPLIERAID, PIC_SUPPLIERBID, PIC_SUPPLIERCID, PIC_SUPPLIERDID, PIC_SUPPLIEREID, PIC_SUPPLIERFID, PIC_SUPPLIERRESID, PIC_SUPPLYTIME, PIC_SHUILV, PIC_PRICE, PIC_PTCODE, PIC_QOUTEFSTSA, PIC_QOUTESCDSA, PIC_QOUTELSTSA, PIC_QOUTEFSTSB, PIC_QOUTESCDSB, PIC_QOUTELSTSB, PIC_QOUTEFSTSC, PIC_QOUTESCDSC, PIC_QOUTELSTSC, PIC_QOUTEFSTSD, PIC_QOUTESCDSD, PIC_QOUTELSTSD, PIC_QOUTEFSTSE, PIC_QOUTESCDSE, PIC_QOUTELSTSE, PIC_QOUTEFSTSF, PIC_QOUTESCDSF, PIC_QOUTELSTSF, PIC_PMODE, PIC_KEYCOMS, PIC_ZDJPRICE, PIC_ZDJNUM, PIC_STATE, PIC_CSTATE, PIC_NOTE,PIC_CFSTATE='2',PIC_ORDERSTATE,PO_OperateState=null from TBMP_IQRCMPPRICE where  PIC_ID='" + picid + "'";
                                        sqltext2 = "INSERT INTO  TBMP_IQRCMPPRICE select PIC_CODE,'', PIC_SHEETNO, PIC_ZONGXU, PIC_JGNUM, PIC_JSNUM, PIC_BJSTATUS, PIC_RKSTATUS, PIC_TASKID, PIC_WXTYPE, PIC_TUHAO, PIC_PJID, PIC_ENGID, PIC_ENGNAME, PIC_MARID, PIC_MNAME, PIC_GUIGE, PIC_CAIZHI, PIC_MASHAPE, PIC_QUANTITY, PIC_FZNUM, PIC_ZXNUM='" + txt_2.Text.Trim().ToString() + "', PIC_ZXFZNUM, PIC_LENGTH, PIC_WIDTH, PIC_SUPPLIERAID, PIC_SUPPLIERBID, PIC_SUPPLIERCID, PIC_SUPPLIERDID, PIC_SUPPLIEREID, PIC_SUPPLIERFID, PIC_SUPPLIERRESID, PIC_SUPPLYTIME, PIC_SHUILV, PIC_PRICE, PIC_PTCODE, PIC_QOUTEFSTSA, PIC_QOUTESCDSA, PIC_QOUTESCDSA*" + txt_2.Text.Trim().ToString() + ", PIC_QOUTEFSTSB, PIC_QOUTESCDSB, PIC_QOUTELSTSB, PIC_QOUTEFSTSC, PIC_QOUTESCDSC, PIC_QOUTELSTSC, PIC_QOUTEFSTSD, PIC_QOUTESCDSD, PIC_QOUTELSTSD, PIC_QOUTEFSTSE, PIC_QOUTESCDSE, PIC_QOUTELSTSE, PIC_QOUTEFSTSF, PIC_QOUTESCDSF, PIC_QOUTELSTSF, PIC_PMODE, PIC_KEYCOMS, PIC_ZDJPRICE, PIC_ZDJNUM, PIC_STATE, PIC_CSTATE, PIC_NOTE,PIC_CFSTATE='2',PIC_ORDERSTATE,PO_OperateState=null from TBMP_IQRCMPPRICE where  PIC_ID='" + picid + "'";
                                        sqltext = "update TBMP_IQRCMPPRICE set PIC_CFSTATE='1' where PIC_ID='" + picid + "'";
                                        string sqltext0 = "update TBMP_IQRCMPPRICE set PTC=(PIC_ENGID+'-'+PIC_TUHAO+'('+PIC_ZONGXU+')'+PIC_MNAME+'-'+convert(varchar,PIC_ID)) where PIC_SHEETNO='" + TextBoxNO.Text.Trim()+"'";
                                        list.Add(sqltext1);
                                        list.Add(sqltext2);
                                        list.Add(sqltext);
                                        list.Add(sqltext0);
                                        DBCallCommon.ExecuteTrans(list);
                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('拆分成功！');location=location; ", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您填写的拆分数量有误，请核实！！！'); ", true);
                                    
                                    }
                                    break;
                                case "3":
                                    txt1 = Convert.ToInt32(txt_1.Text.Trim().ToString());
                                    txt2 = Convert.ToInt32(txt_2.Text.Trim().ToString());
                                    txt3 = Convert.ToInt32(txt_3.Text.Trim().ToString());
                                    if (num == (txt1 + txt2 + txt3))
                                    {
                                        sqltext1 = "INSERT INTO  TBMP_IQRCMPPRICE select PIC_CODE,'',PIC_SHEETNO, PIC_ZONGXU, PIC_JGNUM, PIC_JSNUM, PIC_BJSTATUS, PIC_RKSTATUS, PIC_TASKID, PIC_WXTYPE, PIC_TUHAO, PIC_PJID, PIC_ENGID, PIC_ENGNAME, PIC_MARID, PIC_MNAME, PIC_GUIGE, PIC_CAIZHI, PIC_MASHAPE, PIC_QUANTITY, PIC_FZNUM, PIC_ZXNUM='" + txt_1.Text.Trim().ToString() + "', PIC_ZXFZNUM, PIC_LENGTH, PIC_WIDTH, PIC_SUPPLIERAID, PIC_SUPPLIERBID, PIC_SUPPLIERCID, PIC_SUPPLIERDID, PIC_SUPPLIEREID, PIC_SUPPLIERFID, PIC_SUPPLIERRESID, PIC_SUPPLYTIME, PIC_SHUILV, PIC_PRICE, PIC_PTCODE, PIC_QOUTEFSTSA, PIC_QOUTESCDSA, PIC_QOUTESCDSA*" + txt_1.Text.Trim().ToString() + ", PIC_QOUTEFSTSB, PIC_QOUTESCDSB, PIC_QOUTELSTSB, PIC_QOUTEFSTSC, PIC_QOUTESCDSC, PIC_QOUTELSTSC, PIC_QOUTEFSTSD, PIC_QOUTESCDSD, PIC_QOUTELSTSD, PIC_QOUTEFSTSE, PIC_QOUTESCDSE, PIC_QOUTELSTSE, PIC_QOUTEFSTSF, PIC_QOUTESCDSF, PIC_QOUTELSTSF, PIC_PMODE, PIC_KEYCOMS, PIC_ZDJPRICE, PIC_ZDJNUM, PIC_STATE, PIC_CSTATE, PIC_NOTE,PIC_CFSTATE='2',PIC_ORDERSTATE,PO_OperateState=null from TBMP_IQRCMPPRICE where  PIC_ID='" + picid + "'";


                                        sqltext2 = "INSERT INTO  TBMP_IQRCMPPRICE select PIC_CODE,'', PIC_SHEETNO, PIC_ZONGXU, PIC_JGNUM, PIC_JSNUM, PIC_BJSTATUS, PIC_RKSTATUS, PIC_TASKID, PIC_WXTYPE, PIC_TUHAO, PIC_PJID, PIC_ENGID, PIC_ENGNAME, PIC_MARID, PIC_MNAME, PIC_GUIGE, PIC_CAIZHI, PIC_MASHAPE, PIC_QUANTITY, PIC_FZNUM, PIC_ZXNUM='" + txt_2.Text.Trim().ToString() + "', PIC_ZXFZNUM, PIC_LENGTH, PIC_WIDTH, PIC_SUPPLIERAID, PIC_SUPPLIERBID, PIC_SUPPLIERCID, PIC_SUPPLIERDID, PIC_SUPPLIEREID, PIC_SUPPLIERFID, PIC_SUPPLIERRESID, PIC_SUPPLYTIME, PIC_SHUILV, PIC_PRICE, PIC_PTCODE, PIC_QOUTEFSTSA, PIC_QOUTESCDSA, PIC_QOUTESCDSA*" + txt_2.Text.Trim().ToString() + ", PIC_QOUTEFSTSB, PIC_QOUTESCDSB, PIC_QOUTELSTSB, PIC_QOUTEFSTSC, PIC_QOUTESCDSC, PIC_QOUTELSTSC, PIC_QOUTEFSTSD, PIC_QOUTESCDSD, PIC_QOUTELSTSD, PIC_QOUTEFSTSE, PIC_QOUTESCDSE, PIC_QOUTELSTSE, PIC_QOUTEFSTSF, PIC_QOUTESCDSF, PIC_QOUTELSTSF, PIC_PMODE, PIC_KEYCOMS, PIC_ZDJPRICE, PIC_ZDJNUM, PIC_STATE, PIC_CSTATE, PIC_NOTE,PIC_CFSTATE='2',PIC_ORDERSTATE,PO_OperateState=null from TBMP_IQRCMPPRICE where  PIC_ID='" + picid + "'";

                                        sqltext3 = "INSERT INTO  TBMP_IQRCMPPRICE select PIC_CODE,'', PIC_SHEETNO, PIC_ZONGXU, PIC_JGNUM, PIC_JSNUM, PIC_BJSTATUS, PIC_RKSTATUS, PIC_TASKID, PIC_WXTYPE, PIC_TUHAO, PIC_PJID, PIC_ENGID, PIC_ENGNAME, PIC_MARID, PIC_MNAME, PIC_GUIGE, PIC_CAIZHI, PIC_MASHAPE, PIC_QUANTITY, PIC_FZNUM, PIC_ZXNUM='" + txt_3.Text.Trim().ToString() + "', PIC_ZXFZNUM, PIC_LENGTH, PIC_WIDTH, PIC_SUPPLIERAID, PIC_SUPPLIERBID, PIC_SUPPLIERCID, PIC_SUPPLIERDID, PIC_SUPPLIEREID, PIC_SUPPLIERFID, PIC_SUPPLIERRESID, PIC_SUPPLYTIME, PIC_SHUILV, PIC_PRICE, PIC_PTCODE, PIC_QOUTEFSTSA, PIC_QOUTESCDSA,PIC_QOUTESCDSA*" + txt_3.Text.Trim().ToString() + ", PIC_QOUTEFSTSB, PIC_QOUTESCDSB, PIC_QOUTELSTSB, PIC_QOUTEFSTSC, PIC_QOUTESCDSC, PIC_QOUTELSTSC, PIC_QOUTEFSTSD, PIC_QOUTESCDSD, PIC_QOUTELSTSD, PIC_QOUTEFSTSE, PIC_QOUTESCDSE, PIC_QOUTELSTSE, PIC_QOUTEFSTSF, PIC_QOUTESCDSF, PIC_QOUTELSTSF, PIC_PMODE, PIC_KEYCOMS, PIC_ZDJPRICE, PIC_ZDJNUM, PIC_STATE, PIC_CSTATE, PIC_NOTE,PIC_CFSTATE='2',PIC_ORDERSTATE,PO_OperateState=null from TBMP_IQRCMPPRICE where  PIC_ID='" + picid + "'";
                                        sqltext = "update TBMP_IQRCMPPRICE set PIC_CFSTATE='1' where PIC_ID='" + picid + "'";
                                        string sqltext0 = "update TBMP_IQRCMPPRICE set PTC=(PIC_ENGID+'-'+PIC_TUHAO+'('+PIC_ZONGXU+')'+PIC_MNAME+'-'+convert(varchar,PIC_ID)) where PIC_SHEETNO='" + TextBoxNO.Text.Trim() + "'";
                                        list.Add(sqltext1);
                                        list.Add(sqltext2);
                                        list.Add(sqltext3);
                                        list.Add(sqltext0);
                                        list.Add(sqltext);
                                        DBCallCommon.ExecuteTrans(list);
                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('拆分成功!');location=location; ", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您填写的拆分数量有误，请核实！！！'); ", true);
                                    }
                                    break;
                                case "4":
                                    txt1 = Convert.ToInt32(txt_1.Text.Trim().ToString());
                                    txt2 = Convert.ToInt32(txt_2.Text.Trim().ToString());
                                    txt3 = Convert.ToInt32(txt_3.Text.Trim().ToString());
                                    txt4 = Convert.ToInt32(txt_4.Text.Trim().ToString());
                                    if (num == (txt1 + txt2 + txt3 + txt4))
                                    {

                                        sqltext1 = "INSERT INTO  TBMP_IQRCMPPRICE select PIC_CODE,'', PIC_SHEETNO, PIC_ZONGXU, PIC_JGNUM, PIC_JSNUM, PIC_BJSTATUS, PIC_RKSTATUS, PIC_TASKID, PIC_WXTYPE, PIC_TUHAO, PIC_PJID, PIC_ENGID, PIC_ENGNAME, PIC_MARID, PIC_MNAME, PIC_GUIGE, PIC_CAIZHI, PIC_MASHAPE, PIC_QUANTITY, PIC_FZNUM, PIC_ZXNUM='" + txt_1.Text.Trim().ToString() + "', PIC_ZXFZNUM, PIC_LENGTH, PIC_WIDTH, PIC_SUPPLIERAID, PIC_SUPPLIERBID, PIC_SUPPLIERCID, PIC_SUPPLIERDID, PIC_SUPPLIEREID, PIC_SUPPLIERFID, PIC_SUPPLIERRESID, PIC_SUPPLYTIME, PIC_SHUILV, PIC_PRICE, PIC_PTCODE, PIC_QOUTEFSTSA, PIC_QOUTESCDSA,PIC_QOUTESCDSA*" + txt_1.Text.Trim().ToString() + ", PIC_QOUTEFSTSB, PIC_QOUTESCDSB, PIC_QOUTELSTSB, PIC_QOUTEFSTSC, PIC_QOUTESCDSC, PIC_QOUTELSTSC, PIC_QOUTEFSTSD, PIC_QOUTESCDSD, PIC_QOUTELSTSD, PIC_QOUTEFSTSE, PIC_QOUTESCDSE, PIC_QOUTELSTSE, PIC_QOUTEFSTSF, PIC_QOUTESCDSF, PIC_QOUTELSTSF, PIC_PMODE, PIC_KEYCOMS, PIC_ZDJPRICE, PIC_ZDJNUM, PIC_STATE, PIC_CSTATE, PIC_NOTE,PIC_CFSTATE='2',PIC_ORDERSTATE,PO_OperateState=null from TBMP_IQRCMPPRICE where  PIC_ID='" + picid + "'";

                                        sqltext2 = "INSERT INTO  TBMP_IQRCMPPRICE select PIC_CODE,'', PIC_SHEETNO, PIC_ZONGXU, PIC_JGNUM, PIC_JSNUM, PIC_BJSTATUS, PIC_RKSTATUS, PIC_TASKID, PIC_WXTYPE, PIC_TUHAO, PIC_PJID, PIC_ENGID, PIC_ENGNAME, PIC_MARID, PIC_MNAME, PIC_GUIGE, PIC_CAIZHI, PIC_MASHAPE, PIC_QUANTITY, PIC_FZNUM, PIC_ZXNUM='" + txt_2.Text.Trim().ToString() + "', PIC_ZXFZNUM, PIC_LENGTH, PIC_WIDTH, PIC_SUPPLIERAID, PIC_SUPPLIERBID, PIC_SUPPLIERCID, PIC_SUPPLIERDID, PIC_SUPPLIEREID, PIC_SUPPLIERFID, PIC_SUPPLIERRESID, PIC_SUPPLYTIME, PIC_SHUILV, PIC_PRICE, PIC_PTCODE, PIC_QOUTEFSTSA, PIC_QOUTESCDSA, PIC_QOUTESCDSA*" + txt_2.Text.Trim().ToString() + ", PIC_QOUTEFSTSB, PIC_QOUTESCDSB, PIC_QOUTELSTSB, PIC_QOUTEFSTSC, PIC_QOUTESCDSC, PIC_QOUTELSTSC, PIC_QOUTEFSTSD, PIC_QOUTESCDSD, PIC_QOUTELSTSD, PIC_QOUTEFSTSE, PIC_QOUTESCDSE, PIC_QOUTELSTSE, PIC_QOUTEFSTSF, PIC_QOUTESCDSF, PIC_QOUTELSTSF, PIC_PMODE, PIC_KEYCOMS, PIC_ZDJPRICE, PIC_ZDJNUM, PIC_STATE, PIC_CSTATE, PIC_NOTE,PIC_CFSTATE='2',PIC_ORDERSTATE,PO_OperateState=null from TBMP_IQRCMPPRICE where  PIC_ID='" + picid + "'";

                                        sqltext3 = "INSERT INTO  TBMP_IQRCMPPRICE select PIC_CODE,'', PIC_SHEETNO, PIC_ZONGXU, PIC_JGNUM, PIC_JSNUM, PIC_BJSTATUS, PIC_RKSTATUS, PIC_TASKID, PIC_WXTYPE, PIC_TUHAO, PIC_PJID, PIC_ENGID, PIC_ENGNAME, PIC_MARID, PIC_MNAME, PIC_GUIGE, PIC_CAIZHI, PIC_MASHAPE, PIC_QUANTITY, PIC_FZNUM, PIC_ZXNUM='" + txt_3.Text.Trim().ToString() + "', PIC_ZXFZNUM, PIC_LENGTH, PIC_WIDTH, PIC_SUPPLIERAID, PIC_SUPPLIERBID, PIC_SUPPLIERCID, PIC_SUPPLIERDID, PIC_SUPPLIEREID, PIC_SUPPLIERFID, PIC_SUPPLIERRESID, PIC_SUPPLYTIME, PIC_SHUILV, PIC_PRICE, PIC_PTCODE, PIC_QOUTEFSTSA, PIC_QOUTESCDSA,PIC_QOUTESCDSA*" + txt_3.Text.Trim().ToString() + ", PIC_QOUTEFSTSB, PIC_QOUTESCDSB, PIC_QOUTELSTSB, PIC_QOUTEFSTSC, PIC_QOUTESCDSC, PIC_QOUTELSTSC, PIC_QOUTEFSTSD, PIC_QOUTESCDSD, PIC_QOUTELSTSD, PIC_QOUTEFSTSE, PIC_QOUTESCDSE, PIC_QOUTELSTSE, PIC_QOUTEFSTSF, PIC_QOUTESCDSF, PIC_QOUTELSTSF, PIC_PMODE, PIC_KEYCOMS, PIC_ZDJPRICE, PIC_ZDJNUM, PIC_STATE, PIC_CSTATE, PIC_NOTE,PIC_CFSTATE='2',PIC_ORDERSTATE,PO_OperateState=null from TBMP_IQRCMPPRICE where  PIC_ID='" + picid + "'";

                                        sqltext4 = "INSERT INTO  TBMP_IQRCMPPRICE select PIC_CODE,'', PIC_SHEETNO, PIC_ZONGXU, PIC_JGNUM, PIC_JSNUM, PIC_BJSTATUS, PIC_RKSTATUS, PIC_TASKID, PIC_WXTYPE, PIC_TUHAO, PIC_PJID, PIC_ENGID, PIC_ENGNAME, PIC_MARID, PIC_MNAME, PIC_GUIGE, PIC_CAIZHI, PIC_MASHAPE, PIC_QUANTITY, PIC_FZNUM, PIC_ZXNUM='" + txt_4.Text.Trim().ToString() + "', PIC_ZXFZNUM, PIC_LENGTH, PIC_WIDTH, PIC_SUPPLIERAID, PIC_SUPPLIERBID, PIC_SUPPLIERCID, PIC_SUPPLIERDID, PIC_SUPPLIEREID, PIC_SUPPLIERFID, PIC_SUPPLIERRESID, PIC_SUPPLYTIME, PIC_SHUILV, PIC_PRICE, PIC_PTCODE, PIC_QOUTEFSTSA, PIC_QOUTESCDSA, PIC_QOUTESCDSA*" + txt_4.Text.Trim().ToString() + ", PIC_QOUTEFSTSB, PIC_QOUTESCDSB, PIC_QOUTELSTSB, PIC_QOUTEFSTSC, PIC_QOUTESCDSC, PIC_QOUTELSTSC, PIC_QOUTEFSTSD, PIC_QOUTESCDSD, PIC_QOUTELSTSD, PIC_QOUTEFSTSE, PIC_QOUTESCDSE, PIC_QOUTELSTSE, PIC_QOUTEFSTSF, PIC_QOUTESCDSF, PIC_QOUTELSTSF, PIC_PMODE, PIC_KEYCOMS, PIC_ZDJPRICE, PIC_ZDJNUM, PIC_STATE, PIC_CSTATE, PIC_NOTE,PIC_CFSTATE='2',PIC_ORDERSTATE,PO_OperateState=null from TBMP_IQRCMPPRICE where  PIC_ID='" + picid + "'";

                                        sqltext = "update TBMP_IQRCMPPRICE set PIC_CFSTATE='1' where PIC_ID='" + picid + "'";

                                        string sqltext0 = "update TBMP_IQRCMPPRICE set PTC=(PIC_ENGID+'-'+PIC_TUHAO+'('+PIC_ZONGXU+')'+PIC_MNAME+'-'+convert(varchar,PIC_ID)) where PIC_SHEETNO='" + TextBoxNO.Text.Trim() + "'";
                                        list.Add(sqltext1);
                                        list.Add(sqltext2);
                                        list.Add(sqltext3);
                                        list.Add(sqltext4);
                                        list.Add(sqltext);
                                        list.Add(sqltext0);
                                        DBCallCommon.ExecuteTrans(list);
                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('拆分成功!');location=location; ", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您填写的拆分数量有误，请核实！！！'); ", true);
                                    }
                                    break;
                            }
                     }
                   }
                }
            }
        }

        private bool isselect()
        {
            bool bol_1 = false;
            switch (ddl_split.SelectedValue)
            {

                case "2": if (txt_1.Text.Trim().ToString() != "" && txt_2.Text.Trim().ToString() != "")
                    { bol_1 = true; }
                    else { ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写需要拆分的数量！');", true); break; }
                    break;
                case "3": if (txt_1.Text.Trim().ToString() != "" && txt_2.Text.Trim().ToString() != "" && txt_3.Text.Trim().ToString() != "")
                    { bol_1 = true; }
                    else { ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写需要拆分的数量！');", true); break; }
                    break;
                case "4": if (txt_1.Text.Trim().ToString() != "" && txt_2.Text.Trim().ToString() != "" && txt_3.Text.Trim().ToString() != "" && txt_4.Text.Trim().ToString() != "")
                    { bol_1 = true; }
                    else { ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写需要拆分的数量！');", true); break; }
                    break;
            }
            return bol_1;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_delete_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sqltext = "";
            List<string> list = new List<string>();
            foreach (RepeaterItem reitem in checked_detailRepeater.Items)
            {
                
                CheckBox cb_1 = (CheckBox)reitem.FindControl("CKBOX_SELECT");
                string picid = ((Label)reitem.FindControl("PIC_ID")).Text;
                string ms_code = ((Label)reitem.FindControl("MS_CODE")).Text;
                if (cb_1.Checked)
                {
                    i++;
                    sqltext = "delete from TBMP_IQRCMPPRICE WHERE PIC_ID='" + picid + "'";
                    list.Add(sqltext);
                    sqltext = "update TBPM_WXDetail set MS_scwaixie='4' where MS_CODE='" + ms_code + "'";
                    list.Add(sqltext);
                }
            }
            if (i > 0 && i < checked_detailRepeater.Items.Count)
            {
                DBCallCommon.ExecuteTrans(list);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功！！');window.location.reload();", true);  
            }
            else if (i == checked_detailRepeater.Items.Count)
            {
                DBCallCommon.ExecuteTrans(list);
                sqltext = "delete from TBMP_IQRCMPPRCRVW where ICL_SHEETNO='" + gloabsheetno + "'";
                DBCallCommon.ExeSqlText(sqltext);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功！！');window.close();window.opener.location.reload();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择需要删除的数据！！');", true);
            }
        }
        /// <summary>
        /// 单据取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            List<string> list_1 = new List<string>();
            string sqltext = "delete from TBMP_IQRCMPPRICE where PIC_SHEETNO='" + gloabsheetno + "'";
            list_1.Add(sqltext);
            sqltext = "delete from TBMP_IQRCMPPRCRVW where ICL_SHEETNO='" + gloabsheetno + "'";
            list_1.Add(sqltext);
            //int sheetno = Convert.ToInt32(gloabsheetno);
            //sqltext = "update TBPM_WXDetail set MS_scwaixie='1' where MS_WSID="+sheetno+"";
            foreach (RepeaterItem reitem in checked_detailRepeater.Items)
            {
                string ms_code = ((Label)reitem.FindControl("MS_CODE")).Text;
                sqltext = "update TBPM_WXDetail set MS_scwaixie='4' where MS_CODE='" + ms_code + "'";
                list_1.Add(sqltext);
            }
            DBCallCommon.ExecuteTrans(list_1);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('取消单据成功！！');window.close();window.opener.location.reload();", true);  
        }

        protected void btn_shangcha_OnClick(object sender, EventArgs e)
        {
            int i = 0;
            string piccode = "";
            string wxsheetno = "";//外协单号
            string sqltext = "";
            foreach (RepeaterItem retim in checked_detailRepeater.Items)
            {
                CheckBox cbx = retim.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        piccode = ((Label)retim.FindControl("MS_CODE")).Text;
                        sqltext = "select * from TBPM_WXDETAIL where MS_CODE='" + piccode + "'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dt.Rows.Count >= 1)
                        {
                            wxsheetno = dt.Rows[0]["MS_WSID"].ToString();
                        }
                    }
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
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('PM_Xie_check_detail.aspx?num1=1&num2=3&sheetno=" + bjdsheetno + ";", true);
                // Response.Write("<script>window.open('PM_Xie_check_detail.aspx?num1=1&num2=3&sheetno=" + bjdsheetno + "','toolbar=yes')</script>");
                Response.Redirect("~/PM_Data/PM_Xie_Audit.aspx?action=view&id=" + wxsheetno + "");
            }
        }
        protected void btn_xiacha_OnClick(object sender, EventArgs e)
        {

            int i = 0;
            string ptc = "";

            string wxsheetno = "";//订单号
            string sqltext = "";
            foreach (RepeaterItem retim in checked_detailRepeater.Items)
            {
                CheckBox cbx = retim.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        ptc = ((Label)retim.FindControl("PTC")).Text;
                        sqltext = "select * from TBMP_Order where TO_PTC='" + ptc + "'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dt.Rows.Count >= 1)
                        {
                            wxsheetno = dt.Rows[0]["TO_DOCNUM"].ToString();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该数据尚未生成订单！');", true);
                            return;
                        }
                    }
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
                Response.Redirect("~/PM_Data/PM_Xie_IntoOrder.aspx?&orderno=" + wxsheetno + "");
            }
        
        }
    }    
}
