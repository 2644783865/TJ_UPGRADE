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
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchaseplan_compareprice : System.Web.UI.Page
    {
        PagedDataSource ps = null;

        //全局变量定义
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
        public DataTable gloabt
        {
            get
            {
                object dt = ViewState["gloabt"];
                return dt == null ? null : (DataTable)dt;
            }
            set
            {
                ViewState["gloabt"] = value;
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
        public string gloabding
        {
            get
            {
                object str = ViewState["gloabding"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabding"] = value;
            }
        }
        public string globnum
        {
            get
            {
                object str = ViewState["globnum"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["globnum"] = value;
            }
        }
        public string globnum1
        {
            get
            {
                object str = ViewState["globnum1"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["globnum1"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_cancel.Attributes.Add("OnClick", "Javascript:return confirm('确定取消？执行此操作将停止对选中物料的询比价！');");
            btn_cancel.Click += new EventHandler(btn_cancel_Click);
            btn_autobj.Attributes.Add("OnClick", "Javascript:return confirm('税率是否需要修改？若税率不是17%，请修改税率！');");
            btn_BZJ.Attributes.Add("OnClick", "Javascript:return confirm('税率是否需要修改？若税率不是17%，请修改税率！');");
            btn_confirm.Attributes.Add("OnClick", "Javascript:return confirm('税率是否需要修改？若税率不是17%，请修改税率！');");
            if (!IsPostBack)
            {
                if (Request.QueryString["sheetno"] != null)
                {
                    gloabsheetno = Request.QueryString["sheetno"].ToString();
                }
                else
                {
                    gloabsheetno = "";
                }
                Hyp_print.NavigateUrl = "TBPC_IQRCMPPRC_detailprint.aspx?sheetno=" + gloabsheetno;
                TextBoxNO.Text = gloabsheetno;
                Initpage();
                comparepriceRepeaterdatabind();
            }
            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang1"))).Text = PIC_SUPPLIERANAME.Text;
            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang2"))).Text = PIC_SUPPLIERBNAME.Text;
            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang3"))).Text = PIC_SUPPLIERCNAME.Text;
            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang4"))).Text = PIC_SUPPLIERDNAME.Text;
            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang5"))).Text = PIC_SUPPLIERENAME.Text;
            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang6"))).Text = PIC_SUPPLIERFNAME.Text;
        }

        private void Initpage()
        {
            int num = 0;
            string sqltext = "";
            //负责人下拉框绑定
            //主管下拉框绑定
            sqltext = "select ST_NAME,ST_CODE from TBDS_STAFFINFO WHERE ST_DEPID='" + Session["UserDeptID"].ToString() + "' and ST_PD=0";
            DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqltext);
            cob_fuziren.DataSource = dt0;
            cob_fuziren.DataTextField = "ST_NAME";
            cob_fuziren.DataValueField = "ST_CODE";
            cob_fuziren.DataBind();
            sqltext = "select ST_NAME,ST_CODE from TBDS_STAFFINFO WHERE ST_DEPID='05'";
            dt0 = DBCallCommon.GetDTUsingSqlText(sqltext);
            cob_zgjl.DataSource = dt0;
            cob_zgjl.DataTextField = "ST_NAME";
            cob_zgjl.DataValueField = "ST_CODE";
            cob_zgjl.DataBind();
            sqltext = "select zdrid  as ICL_REVIEWA,zdrnm  as ICL_REVIEWANM,zdtime  as ICL_REVIEWATIME, zdryj as ICL_REVIEWAADVC," +
                    "shbid as ICL_REVIEWB,shbnm as ICL_REVIEWBNM,shbtime as ICL_REVIEWBTIME,shbyj as ICL_REVIEWBADVC," +
                    "shcid as ICL_REVIEWC,shcnm as ICL_REVIEWCNM,shctime as ICL_REVIEWCTIME,shcyj as ICL_REVIEWCADVC," +
                    "shdid as ICL_REVIEWD,shdnm as ICL_REVIEWDNM,shdtime as ICL_REVIEWDTIME,shdyj as ICL_REVIEWDADVC," +
                    "sheid as ICL_REVIEWE,shenm as ICL_REVIEWENM,shetime as ICL_REVIEWETIME,sheyi as ICL_REVIEWEADVC," +
                    "shfid as ICL_REVIEWF,shfnm as ICL_REVIEWFNM,shftime as ICL_REVIEWFTIME,shfyj as ICL_REVIEWFADVC," +
                    "shgid as ICL_REVIEWG,shgnm as ICL_REVIEWGNM,shgtime as ICL_REVIEWGTIME,shgyj as ICL_REVIEWGADVC," +
                    "iclfzrid as ICL_FUZRID,iclfzrnm as ICL_FUZRNAME,iclzgid as ICL_ZHUGUANID,iclzgnm as ICL_ZHUGUANNAME," +
                    "isnull(totalstate,0) as ICL_STATE,isnull(statea,0) as ICL_STATEA,isnull(stateb,0) as ICL_STATEB,isnull(statec,0) as ICL_STATEC,isnull(stated,0) as ICL_STATED,ICL_TYPE " +
                    "from View_TBPC_IQRCMPPRCRVW  where picno='" + TextBoxNO.Text.ToString() + "'";


            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                //制单
                ListItem listfz = new ListItem(dt.Rows[0]["ICL_FUZRNAME"].ToString(), dt.Rows[0]["ICL_FUZRID"].ToString());
                ListItem listzgjl = new ListItem(dt.Rows[0]["ICL_ZHUGUANNAME"].ToString(), dt.Rows[0]["ICL_ZHUGUANID"].ToString());
                tb_zd.Text = dt.Rows[0]["ICL_REVIEWANM"].ToString();
                tb_time.Text = dt.Rows[0]["ICL_REVIEWATIME"].ToString();
                if (cob_fuziren.Items.Contains(listfz))
                {
                    cob_fuziren.SelectedValue = dt.Rows[0]["ICL_FUZRID"].ToString();
                }
                if (cob_zgjl.Items.Contains(listzgjl))
                {
                    cob_zgjl.SelectedValue = dt.Rows[0]["ICL_ZHUGUANID"].ToString();
                }

                if (dt.Rows[0]["ICL_TYPE"].ToString().Trim() == "2")
                {
                    Chb_Zb.Checked = true;
                }
                else
                {
                    Chb_Zb.Checked = false;
                }
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

                if (ptc == "1") { CheckBox1.Checked = true; num++; }
                if (marid == "1") { CheckBox2.Checked = true; num++; }
                if (marth == "1") { CheckBox3.Checked = true; num++; }
                if (marcz == "1") { CheckBox4.Checked = true; num++; }
                if (margb == "1") { CheckBox5.Checked = true; num++; }
                if (cgsl == "1") { CheckBox6.Checked = true; num++; }
                if (unit == "1") { CheckBox7.Checked = true; num++; }
                if (shuilv == "1") { CheckBox8.Checked = true; num++; }
                if (jine == "1") { CheckBox9.Checked = true; num++; }
                if (length == "1") { CheckBox10.Checked = true; num++; }
                if (width == "1") { CheckBox11.Checked = true; num++; }
                if (note == "1") { CheckBox12.Checked = true; num++; }
                if (fznum == "1") { CheckBox13.Checked = true; num++; }
                if (fzunit == "1") { CheckBox14.Checked = true; num++; }

                globnum = Convert.ToString(num);
                globnum1 = Convert.ToString(18 - Convert.ToDouble(globnum));
            }
        }
        private void comparepriceRepeaterdatabind()
        {
            //ItemTemplatedind();
            getArticle();
            HeaderTemplatebind();
        }

        private void HeaderTemplatebind()
        {
            string sqltext = "";
            sqltext = "SELECT distinct  supplieraid as aid, supplieranm1 as anm, supplierarank as arnk, " +
                     "supplierbid as bid, supplierbnm1 as bnm, supplierbrank  as brnk, suppliercid as cid, " +
                     "suppliercnm1 as cnm, suppliercrank as crnk, supplierdid as did, supplierdnm1 as dnm, " +
                     "supplierdrank as drnk, suppliereid as eid, supplierenm1 as enm, suppliererank as ernk, " +
                     "supplierfid as fid, supplierfnm1 as fnm,supplierfrank as frnk  " +
                     "FROM View_TBPC_IQRCMPPRICE where picno='" + TextBoxNO.Text.ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                PIC_SUPPLIERANAME.Text = dt.Rows[0]["anm"].ToString();
                PIC_SUPPLIERBNAME.Text = dt.Rows[0]["bnm"].ToString();
                PIC_SUPPLIERCNAME.Text = dt.Rows[0]["cnm"].ToString();
                PIC_SUPPLIERDNAME.Text = dt.Rows[0]["dnm"].ToString();
                PIC_SUPPLIERENAME.Text = dt.Rows[0]["enm"].ToString();
                PIC_SUPPLIERFNAME.Text = dt.Rows[0]["fnm"].ToString();
                ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang1"))).Text = dt.Rows[0]["anm"].ToString();
                ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang2"))).Text = dt.Rows[0]["bnm"].ToString();
                ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang3"))).Text = dt.Rows[0]["cnm"].ToString();
                ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang4"))).Text = dt.Rows[0]["dnm"].ToString();
                ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang5"))).Text = dt.Rows[0]["enm"].ToString();
                ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang6"))).Text = dt.Rows[0]["fnm"].ToString();

                PIC_SUPPLIERAID.Text = dt.Rows[0]["aid"].ToString();
                PIC_SUPPLIERBID.Text = dt.Rows[0]["bid"].ToString();
                PIC_SUPPLIERCID.Text = dt.Rows[0]["cid"].ToString();
                PIC_SUPPLIERDID.Text = dt.Rows[0]["did"].ToString();
                PIC_SUPPLIEREID.Text = dt.Rows[0]["eid"].ToString();
                PIC_SUPPLIERFID.Text = dt.Rows[0]["fid"].ToString();

                LbA_lei.Text = dt.Rows[0]["arnk"].ToString();
                LbB_lei.Text = dt.Rows[0]["brnk"].ToString();
                LbC_lei.Text = dt.Rows[0]["crnk"].ToString();
                LbD_lei.Text = dt.Rows[0]["drnk"].ToString();
                LbE_lei.Text = dt.Rows[0]["ernk"].ToString();
                LbF_lei.Text = dt.Rows[0]["frnk"].ToString();

                PIC_SUPPLIERA.Text = dt.Rows[0]["aid"].ToString() + "|" + dt.Rows[0]["anm"].ToString() + "|" + dt.Rows[0]["arnk"].ToString();
                PIC_SUPPLIERB.Text = dt.Rows[0]["bid"].ToString() + "|" + dt.Rows[0]["bnm"].ToString() + "|" + dt.Rows[0]["brnk"].ToString();
                PIC_SUPPLIERC.Text = dt.Rows[0]["cid"].ToString() + "|" + dt.Rows[0]["cnm"].ToString() + "|" + dt.Rows[0]["crnk"].ToString();
                PIC_SUPPLIERD.Text = dt.Rows[0]["did"].ToString() + "|" + dt.Rows[0]["dnm"].ToString() + "|" + dt.Rows[0]["drnk"].ToString();
                PIC_SUPPLIERE.Text = dt.Rows[0]["eid"].ToString() + "|" + dt.Rows[0]["enm"].ToString() + "|" + dt.Rows[0]["ernk"].ToString();
                PIC_SUPPLIERF.Text = dt.Rows[0]["fid"].ToString() + "|" + dt.Rows[0]["fnm"].ToString() + "|" + dt.Rows[0]["frnk"].ToString();
                initdropdprovider1();
            }

        }
        private void initdropdprovider()
        {
            double min = 1000000000;
            int selectindex = 0;
            string providerid = "";
            string providernm = "";
            string text = "";
            foreach (RepeaterItem reitem in comparepriceRepeater.Items)
            {
                selectindex = 0;
                min = 1000000000;
                ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Clear();
                providerid = PIC_SUPPLIERAID.Text;//第一个供应商ID
                providernm = PIC_SUPPLIERANAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox12")).Text.ToString();//供应商1的最终报价

                if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                    min = Convert.ToDouble(text);
                    selectindex = ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count - 1;
                }
                providerid = PIC_SUPPLIERBID.Text;//第二家供应商ID
                providernm = PIC_SUPPLIERBNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox22")).Text.ToString();//供应商2的最终报价
                if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                    if (Convert.ToDouble(text) < min)
                    {
                        min = Convert.ToDouble(text);
                        selectindex = ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count - 1;
                    }
                }
                providerid = PIC_SUPPLIERCID.Text;//第三家供应商ID
                providernm = PIC_SUPPLIERCNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox32")).Text.ToString();//供应商3的最终报价
                if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                    if (Convert.ToDouble(text) < min)
                    {
                        min = Convert.ToDouble(text);
                        selectindex = ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count - 1;
                    }
                }
                providerid = PIC_SUPPLIERDID.Text;//第四家供应商
                providernm = PIC_SUPPLIERDNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox42")).Text.ToString();//供应商4的最终报价
                if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                    if (Convert.ToDouble(text) < min)
                    {
                        min = Convert.ToDouble(text);
                        selectindex = ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count - 1;
                    }
                }
                providerid = PIC_SUPPLIEREID.Text;//第五家供应商
                providernm = PIC_SUPPLIERENAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox52")).Text.ToString();//供应商5的最终报价
                if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                    if (Convert.ToDouble(text) < min)
                    {
                        min = Convert.ToDouble(text);
                        selectindex = ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count - 1;
                    }
                }
                providerid = PIC_SUPPLIERFID.Text;//第六家供应商
                providernm = PIC_SUPPLIERFNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox62")).Text.ToString();//供应商6的最终报价
                if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                    if (Convert.ToDouble(text) < min)
                    {
                        min = Convert.ToDouble(text);
                        selectindex = ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count - 1;
                    }
                }

                if (((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count > 0)
                {
                    ((DropDownList)reitem.FindControl("Drp_supplier")).SelectedIndex = selectindex;
                }

            }
        }

        private void initdropdprovider1()
        {
            double min = 1000000000;
            int selectindex = 0;
            string providerid = "";
            string providernm = "";
            string text = "";
            foreach (RepeaterItem reitem in comparepriceRepeater.Items)
            {
                selectindex = 0;
                min = 1000000000;
                ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Clear();
                providerid = PIC_SUPPLIERAID.Text;//第一个供应商ID
                providernm = PIC_SUPPLIERANAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox12")).Text.ToString();//供应商1的最终报价
                if (providernm != "")//text != "" && Convert.ToDouble(text) != 0 && 
                {
                    text = text == "" ? "0" : text;
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                    min = Convert.ToDouble(text);
                    selectindex = ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count - 1;
                }
                providerid = PIC_SUPPLIERBID.Text;//第二家供应商ID
                providernm = PIC_SUPPLIERBNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox22")).Text.ToString();//供应商2的最终报价
                if (providernm != "")//text != "" && Convert.ToDouble(text) != 0 && 
                {
                    text = text == "" ? "0" : text;
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                    if (Convert.ToDouble(text) < min)
                    {
                        min = Convert.ToDouble(text);
                        selectindex = ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count - 1;
                    }
                }
                providerid = PIC_SUPPLIERCID.Text;//第三家供应商ID
                providernm = PIC_SUPPLIERCNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox32")).Text.ToString();//供应商3的最终报价
                if (providernm != "")//text != "" && Convert.ToDouble(text) != 0 &&
                {
                    text = text == "" ? "0" : text;
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                    if (Convert.ToDouble(text) < min)
                    {
                        min = Convert.ToDouble(text);
                        selectindex = ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count - 1;
                    }
                }
                providerid = PIC_SUPPLIERDID.Text;//第四家供应商
                providernm = PIC_SUPPLIERDNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox42")).Text.ToString();//供应商4的最终报价
                if (providernm != "")//text != "" && Convert.ToDouble(text) != 0 &&
                {
                    text = text == "" ? "0" : text;
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                    if (Convert.ToDouble(text) < min)
                    {
                        min = Convert.ToDouble(text);
                        selectindex = ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count - 1;
                    }
                }
                providerid = PIC_SUPPLIEREID.Text;//第五家供应商
                providernm = PIC_SUPPLIERENAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox52")).Text.ToString();//供应商5的最终报价
                if (providernm != "")//text != "" && Convert.ToDouble(text) != 0 &&
                {
                    text = text == "" ? "0" : text;
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                    if (Convert.ToDouble(text) < min)
                    {
                        min = Convert.ToDouble(text);
                        selectindex = ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count - 1;
                    }
                }
                providerid = PIC_SUPPLIERFID.Text;//第六家供应商
                providernm = PIC_SUPPLIERFNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox62")).Text.ToString();//供应商6的最终报价
                if (providernm != "")//text != "" && Convert.ToDouble(text) != 0 && 
                {
                    text = text == "" ? "0" : text;
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                    if (Convert.ToDouble(text) < min)
                    {
                        min = Convert.ToDouble(text);
                        selectindex = ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count - 1;
                    }
                }
                if (((TextBox)reitem.FindControl("PIC_SUPPLIERRESID")).Text != "" && ((DropDownList)reitem.FindControl("Drp_supplier")).Items.FindByValue(((TextBox)reitem.FindControl("PIC_SUPPLIERRESID")).Text) != null)
                {
                    ((DropDownList)reitem.FindControl("Drp_supplier")).SelectedValue = ((TextBox)reitem.FindControl("PIC_SUPPLIERRESID")).Text;
                }
            }
        }
        private void getArticle()      //取得Article数据
        {
            int cup = Convert.ToInt32(this.lb_CurrentPage.Text);  //当前页数,初始化为地1页
            ps = new PagedDataSource();
            ps.DataSource = CreateDataSource().DefaultView;
            ps.AllowPaging = true;
            ps.PageSize = Convert.ToInt32(DDLPage.SelectedValue);     //每页显示的数据的行数
            ps.CurrentPageIndex = cup - 1;
            lb_count.Text = ps.DataSourceCount.ToString(); //获取记录总数
            lb_page.Text = ps.PageCount.ToString(); //获取总页数

            this.DropDownList1.Items.Clear();
            for (int i = 1; i < ps.PageCount + 1; i++)
            {
                this.DropDownList1.Items.Add(i.ToString());
            }
            LinkUp.Enabled = true;
            LinkDown.Enabled = true;

            try
            {
                DropDownList1.SelectedIndex = Convert.ToInt32(cup.ToString()) - 1;
                comparepriceRepeater.DataSource = ps;
                comparepriceRepeater.DataBind();
                if (comparepriceRepeater.Items.Count > 0)
                {
                    NoDataPane1.Visible = false;
                }
                else
                {
                    NoDataPane1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //绑定信息
        public System.Data.DataTable CreateDataSource()
        {
            System.Data.DataTable dt = new DataTable();
            string sqltext = "";
            if (HidType.Value == "0")//未导入数据
            {

                sqltext = "SELECT picno, pjid, pjnm, engid, engnm, marid,PIC_MASHAPE, marnm, margg, margb, marcz, marunit, " +
                          "marfzunit, length, width, marnum, marfznum, marzxnum, marzxfznum, " +
                         "supplierresid, supplierresnm1, supplierresrank, shuilv, price, detamount, ptcode, qoutefstsa, " +
                         "qoutescdsa, qoutelstsa, qoutefstsb, qoutescdsb, qoutelstsb, qoutefstsc, qoutescdsc, " +
                         "qoutelstsc, qoutefstsd, qoutescdsd, qoutelstsd, qoutefstse, qoutescdse, qoutelstse, " +
                         "qoutefstsf, qoutescdsf, qoutelstsf, pmode, keycoms, zdjprice, zdjnum, isnull(detailstate,0) as detailstate,case when margb='' then PIC_TUHAO else '' end as PIC_TUHAO, " +
                    //"isnull(detailcstate,0) as detailcstate, detailnote FROM View_TBPC_IQRCMPPRICE  where picno='" + TextBoxNO.Text.ToString() + "' order by ptcode ASC";
                          "(SELECT min(PRICE) FROM View_TBPC_IQRCMPPRICE_RVW GROUP BY MARID,totalstate having View_TBPC_IQRCMPPRICE.marid=MARID AND totalstate='4') AS minprice, " +
                    //"(SELECT  top(1) PRICE  FROM View_TBPC_IQRCMPPRICE_RVW  GROUP BY MARID,PRICE,IRQDATA,totalstate  having View_TBPC_IQRCMPPRICE.marid=MARID AND totalstate='4' order by IRQDATA DESC ) as lastprice, " +
                          "(SELECT  top(1) PRICE  FROM View_TBPC_IQRCMPPRICE_RVW  GROUP BY MARID,PRICE,IRQDATA,totalstate  having View_TBPC_IQRCMPPRICE.marid=MARID AND totalstate='4' and price is not null order by IRQDATA DESC ) as lastprice, " +
                         "isnull(detailcstate,0) as detailcstate, detailnote,PIC_MAP,ST_SQR,PIC_CHILDENGNAME FROM View_TBPC_IQRCMPPRICE  where picno='" + TextBoxNO.Text.ToString() + "' order by " + rad_Paixu.SelectedValue.ToString() + " ASC";

            }
            else//导入数据之后
            {
                sqltext = "SELECT picno, pjid, pjnm, engid, engnm, marid,PIC_MASHAPE, marnm, margg, margb, marcz, marunit, " +
                          "marfzunit, length, width, marnum, marfznum, marzxnum, marzxfznum, " +
                         "supplierresid, supplierresnm1, supplierresrank, shuilv, price, detamount, a.ptcode,b.jh1 as qoutefstsa, " +
                         "b.dj1 as qoutescdsa,b.je1 as qoutelstsa,b.jh2 as qoutefstsb,b.dj2 as qoutescdsb,b.je2 qoutelstsb,b.jh3 as qoutefstsc,b.dj3 as qoutescdsc, " +
                         "b.je3 as qoutelstsc,b.jh4 as qoutefstsd,b.dj4 as qoutescdsd,b.je4 as qoutelstsd,b.jh5 as qoutefstse,b.dj5 as qoutescdse,b.je5 as qoutelstse, " +
                         "b.jh6 as qoutefstsf,b.dj6 as qoutescdsf,b.je5 as qoutelstsf, pmode, keycoms, zdjprice, zdjnum, isnull(detailstate,0) as detailstate,PIC_TUHAO, " +
                    //"isnull(detailcstate,0) as detailcstate, detailnote FROM View_TBPC_IQRCMPPRICE  where picno='" + TextBoxNO.Text.ToString() + "' order by ptcode ASC";
                          "(SELECT min(PRICE) FROM View_TBPC_IQRCMPPRICE_RVW GROUP BY MARID,totalstate having a.marid=MARID AND totalstate='4') AS minprice, " +
                    //"(SELECT  top(1) PRICE  FROM View_TBPC_IQRCMPPRICE_RVW  GROUP BY MARID,PRICE,IRQDATA,totalstate  having View_TBPC_IQRCMPPRICE.marid=MARID AND totalstate='4' order by IRQDATA DESC ) as lastprice, " +
                          "(SELECT  top(1) PRICE  FROM View_TBPC_IQRCMPPRICE_RVW  GROUP BY MARID,PRICE,IRQDATA,totalstate  having a.marid=MARID AND totalstate='4' and price is not null order by IRQDATA DESC ) as lastprice, " +
                         "isnull(detailcstate,0) as detailcstate, detailnote,PIC_MAP,ST_SQR,PIC_CHILDENGNAME FROM View_TBPC_IQRCMPPRICE as a left join TBPC_BIJIADAN as b on a.ptcode=b.ptcode where picno='" + TextBoxNO.Text.ToString() + "' order by " + rad_Paixu.SelectedValue.ToString() + " ASC";

            }
            dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            return dt;
        }
        protected void LinkDown_Click(object sender, EventArgs e) //下一页按钮代码
        {
            if (lb_CurrentPage.Text.ToString() != lb_page.Text.ToString())
            {
                lb_CurrentPage.Text = Convert.ToString(Convert.ToInt32(lb_CurrentPage.Text) + 1);
                DropDownList1.SelectedIndex = Convert.ToInt32(lb_CurrentPage.Text) - 1;
                comparepriceRepeaterdatabind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已经是最后一页');", true);
            }

        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e) //跳转到指定页代码
        {
            int page = Convert.ToInt16((DropDownList1.SelectedItem.Value));
            lb_CurrentPage.Text = page.ToString();
            comparepriceRepeaterdatabind();
        }
        protected void LinkUp_Click(object sender, EventArgs e)  //上一页按钮代码
        {
            if (Convert.ToInt32(lb_CurrentPage.Text) > 1)
            {
                lb_CurrentPage.Text = Convert.ToString(Convert.ToInt32(lb_CurrentPage.Text) - 1);
                DropDownList1.SelectedIndex = Convert.ToInt32(lb_CurrentPage.Text) - 1;
                comparepriceRepeaterdatabind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已经是第一页');", true);
            }
        }
        protected void LinkFirst_Click(object sender, EventArgs e)  //跳到第一页代码
        {
            if (lb_CurrentPage.Text != "1")
            {
                lb_CurrentPage.Text = "1";
                comparepriceRepeaterdatabind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已经是第一页');", true);
            }
        }
        protected void LinkLast_Click(object sender, EventArgs e)  //跳到最后一页代码
        {
            if (lb_CurrentPage.Text.ToString() != lb_page.Text.ToString())
            {
                lb_CurrentPage.Text = lb_page.Text.ToString();
                comparepriceRepeaterdatabind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已经是最后一页');", true);
            }

        }

        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            Labelerror.Visible = false;
            Labelerror.Text = "";
            savedate();


            //2016.6.25修改
            //Response.Redirect("SM_WarehouseIn_ActionResult.aspx?IDP=" + pcode + "&&IDC=" + code + "&&RES=M");//无用，仿照
            var num1 = 2;
            var num2 = 2;


            Response.Redirect("~/PC_Data/TBPC_IQRCMPPRCLST_checked_detail.aspx?num1=" + num1 + "&&num2=" + num2 + "&&sheetno=" + TextBoxNO.Text);
            //Initpage();
            //comparepriceRepeaterdatabind();
            //Response.Redirect(Request.Url.AbsoluteUri);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);
        }
        protected bool resetprovider(TextBox conprovider, TextBox conproviderid, TextBox conprovidernm, Label conrank)
        {
            bool temp = true;
            string provider1id = "";
            string provider2id = "";
            string provider3id = "";
            string provider4id = "";
            string provider5id = "";
            string provider6id = "";
            string provider = "";
            string providerid = "";
            string providernm = "";
            string rank = "";
            string[] strs = { };
            provider = conprovidernm.Text.Replace(" ", "");
            if (provider.Contains("|"))
            {
                strs = provider.Split('|');
                if (strs.Length == 3)
                {
                    conprovider.Text = provider;
                }
            }
            else if (provider == "")
            {
                conprovider.Text = "";
            }
            if (conprovider.Text == "")
            {
                providernm = "";
                providerid = "";
                providerid = "";
            }
            else
            {
                strs = conprovider.Text.Split('|');
                providerid = strs[0];
                providernm = strs[1];
                rank = strs[2];
            }
            conprovidernm.Text = providernm;
            conproviderid.Text = providerid;
            conrank.Text = rank;
            provider1id = PIC_SUPPLIERAID.Text;
            provider2id = PIC_SUPPLIERBID.Text;
            provider3id = PIC_SUPPLIERCID.Text;
            provider4id = PIC_SUPPLIERDID.Text;
            provider5id = PIC_SUPPLIEREID.Text;
            provider6id = PIC_SUPPLIERFID.Text;
            string[] providers = new string[] { provider1id, provider2id, provider3id, provider4id, provider5id, provider6id };
            if (providerid != "")
            {
                int i = providers.Count(p => p == providerid);
                if (i > 1)
                {
                    temp = false;
                }

            }
            return temp;
        }
        protected void btn_autobj_Click(object sender, EventArgs e)//自动比价
        {
            int i = 0;
            //第一个供应商
            if (resetprovider(PIC_SUPPLIERA, PIC_SUPPLIERAID, PIC_SUPPLIERANAME, LbA_lei))
            {
                initdropdprovider();
            }
            else
            {
                PIC_SUPPLIERAID.Text = "";
                PIC_SUPPLIERANAME.Text = "";
                PIC_SUPPLIERA.Text = "";
                LbA_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第二个供应商
            if (resetprovider(PIC_SUPPLIERB, PIC_SUPPLIERBID, PIC_SUPPLIERBNAME, LbB_lei))
            {
                initdropdprovider();
            }
            else
            {
                PIC_SUPPLIERBID.Text = "";
                PIC_SUPPLIERBNAME.Text = "";
                PIC_SUPPLIERB.Text = "";
                LbB_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第三个供应商
            if (resetprovider(PIC_SUPPLIERC, PIC_SUPPLIERCID, PIC_SUPPLIERCNAME, LbC_lei))
            {
                initdropdprovider();
            }
            else
            {
                PIC_SUPPLIERCID.Text = "";
                PIC_SUPPLIERCNAME.Text = "";
                PIC_SUPPLIERC.Text = "";
                LbC_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第四个供应商
            if (resetprovider(PIC_SUPPLIERD, PIC_SUPPLIERDID, PIC_SUPPLIERDNAME, LbD_lei))
            {
                initdropdprovider();
            }
            else
            {
                PIC_SUPPLIERDID.Text = "";
                PIC_SUPPLIERDNAME.Text = "";
                PIC_SUPPLIERD.Text = "";
                LbD_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第五个供应商
            if (resetprovider(PIC_SUPPLIERE, PIC_SUPPLIEREID, PIC_SUPPLIERENAME, LbE_lei))
            {
                initdropdprovider();
            }
            else
            {
                PIC_SUPPLIEREID.Text = "";
                PIC_SUPPLIERENAME.Text = "";
                PIC_SUPPLIERE.Text = "";
                LbE_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第六个供应商
            if (resetprovider(PIC_SUPPLIERF, PIC_SUPPLIERFID, PIC_SUPPLIERFNAME, LbF_lei))
            {
                initdropdprovider();
            }
            else
            {
                PIC_SUPPLIERFID.Text = "";
                PIC_SUPPLIERFNAME.Text = "";
                PIC_SUPPLIERF.Text = "";
                LbF_lei.Text = "";
                initdropdprovider();
                i++;
            }
            if (i > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('你输入了相同的供应商，请重新输入！');", true);
                return;
            }

            Labelerror.Visible = false;
            Labelerror.Text = "";
            savedate();
            Initpage();
            comparepriceRepeaterdatabind();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);
        }

        protected void btn_cancel_Click(object sender, EventArgs e)//取消询比价，如果全部取消则删除询比价单
        {
            hcancel();
        }
        protected void hcancel()
        {
            int i = 0;
            string sqltext = "";
            string ptcode = "";
            string PTCODE = "";
            foreach (RepeaterItem Reitem in comparepriceRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        PTCODE = ((Label)Reitem.FindControl("PIC_PCODE")).Text.ToString();
                        if (PTCODE.Contains("#") && (!PTCODE.Contains("@")))
                        {
                            ptcode = PTCODE.Substring(0, PTCODE.IndexOf("#"));
                        }
                        else
                        {
                            ptcode = PTCODE;
                        }
                        sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE='4' WHERE PUR_PTCODE='" + ptcode + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                        sqltext = "DELETE FROM TBPC_IQRCMPPRICE WHERE PIC_SHEETNO='" + TextBoxNO.Text + "' AND PIC_PTCODE like '" + ptcode + "%'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            if (i > 0)
            {
                sqltext = "SELECT PIC_ID FROM TBPC_IQRCMPPRICE WHERE PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    sqltext = "DELETE FROM TBPC_IQRCMPPRCRVW WHERE ICL_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    sqltext = "delete from PC_POWERINFO where poweriqcode='" + TextBoxNO.Text.Trim() + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    Response.Redirect("~/PC_Data/TBPC_IQRCMPPRCLST_checked.aspx");
                }
                comparepriceRepeaterdatabind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);

            }
        }
        protected void allcancel()
        {
            string sqltext = "";
            List<string> sqltexts = new List<string>();
            sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='4' where PUR_PTCODE in  " +
                      "(select PIC_PTCODE from TBPC_IQRCMPPRICE where PIC_SHEETNO='" + TextBoxNO.Text + "')";
            sqltexts.Add(sqltext);
            sqltext = "delete from TBPC_IQRCMPPRCRVW WHERE ICL_SHEETNO='" + TextBoxNO.Text + "'";
            sqltexts.Add(sqltext);
            sqltext = "delete from TBPC_IQRCMPPRICE WHERE PIC_SHEETNO='" + TextBoxNO.Text + "'";
            sqltexts.Add(sqltext);
            DBCallCommon.ExecuteTrans(sqltexts);
        }
        protected void hclose()//行关闭
        {
            int i = 0;
            string sqltext = "";
            foreach (RepeaterItem Reitem in comparepriceRepeater.Items)
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
                comparepriceRepeaterdatabind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);

            }
        }
        protected void fhclose()//行反关闭
        {
            int i = 0;
            string sqltext = "";
            foreach (RepeaterItem Reitem in comparepriceRepeater.Items)
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
                sqltext = "SELECT PIC_ID FROM TBPC_IQRCMPPRICE WHERE PIC_SHEETNO='" + TextBoxNO.Text + "' and PIC_CSTATE='0'";//是否还存在未关闭的，如果存在则整单未关闭
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    sqltext = "update TBPC_IQRCMPPRCRVW set ICL_CSTATE='0'  WHERE ICL_SHEETNO='" + TextBoxNO.Text + "'";//单号反关闭
                    DBCallCommon.ExeSqlText(sqltext);
                }
                comparepriceRepeaterdatabind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);

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
        }
        protected void btn_change_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            foreach (RepeaterItem Reitem in comparepriceRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        sqltext = "UPDATE a SET PIC_QUANTITY = a.PIC_QUANTITY+b.MP_BGZYNUM, " +
                                  "PIC_ZXNUM=a.PIC_ZXNUM+b.MP_BGZYNUM,PIC_ZXFUNUM=a.PIC_ZXFUNUM+b.MP_BGFZNUM,PIC_FZNUM=a.PIC_FZNUM+b.MP_BGFZNUM  " +
                                  "FROM  TBPC_IQRCMPPRICE AS a INNER JOIN " +
                                  "TBPC_MPCHANGEDETAIL AS b ON a.PIC_PCODE = b.MP_OLDPTCODE " +
                                  "WHERE  (a.PIC_PCODE = '" + ((Label)Reitem.FindControl("PIC_PCODE")).Text + "')";
                        DBCallCommon.ExeSqlText(sqltext);
                        sqltext = "UPDATE a SET PUR_NUM = a.PUR_NUM + b.MP_BGNUM, " +
                                  "PUR_RPNUM=a.PUR_RPNUM+b.MP_BGNUM,PUR_RPWEIGHT=a.PUR_RPWEIGHT+b.MP_WEIGHT,PUR_WEIGHT=a.PUR_WEIGHT+b.MP_WEIGHT  " +
                                  "FROM  TBPC_PURCHASEPLAN AS a INNER JOIN " +
                                  "TBPC_MPCHANGEDETAIL AS b ON a.PUR_PTCODE = b.MP_OLDPTCODE " +
                                  "WHERE  (a.PUR_PTCODE = '" + ((Label)Reitem.FindControl("PIC_PCODE")).Text + "')";
                        DBCallCommon.ExeSqlText(sqltext);
                        sqltext = "UPDATE TBPC_MPCHANGEDETAIL SET MP_STATE='1' WHERE MP_OLDPTCODE='" + ((Label)Reitem.FindControl("PIC_PCODE")).Text + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
        }

        protected void btn_search_Click(object sender, EventArgs e)//搜索
        {
            int i = 0;
            string marid = "";
            foreach (RepeaterItem Reitem in comparepriceRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        marid = ((Label)Reitem.FindControl("PIC_MARID")).Text.ToString();
                        //marshijian = Tb_zdant.Text;
                        //break;
                    }
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据');", true);
                return;
            }
            else if (i >= 2)
            {
                comparepriceRepeaterdatabind();    //刷新  2013年6月26日 11:52:30   Meng
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择一条数据');", true);
                return;
            }
            foreach (RepeaterItem Reitem in comparepriceRepeater.Items)
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
            comparepriceRepeaterdatabind();    //刷新  2013年6月26日 11:52:30   Meng
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "windowopen('PC_Date_historypriceshow.aspx?marid=" + marid + "');", true);
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "windowopen('PC_Date_historypriceshow.aspx?marid=" + marid +"&marshijian=" + marshijian + "');", true);
        }
        protected void savedate()
        {
            string sqltext = "";
            string suppaid = "";
            string suppbid = "";
            string suppcid = "";
            string suppdid = "";
            string suppeid = "";
            string suppfid = "";
            string resproid = "";
            string sheetno = TextBoxNO.Text;
            string ptcode = "";
            string marid = "";
            double num = 0;
            double zxnum = 0;
            double fznum = 0;
            double zxfznum = 0;
            double shuilv = 0;
            string zxid = Session["UserID"].ToString();
            string zxname = Session["UserName"].ToString();
            string state = "";
            string note = "";
            //供应商A
            suppaid = PIC_SUPPLIERAID.Text.ToString().Replace(" ", "");
            //供应商B
            suppbid = PIC_SUPPLIERBID.Text.ToString().Replace(" ", "");
            //供应商C
            suppcid = PIC_SUPPLIERCID.Text.ToString().Replace(" ", "");
            //供应商D
            suppdid = PIC_SUPPLIERDID.Text.ToString().Replace(" ", "");
            //供应商E
            suppeid = PIC_SUPPLIEREID.Text.ToString().Replace(" ", "");
            //供应商F
            suppfid = PIC_SUPPLIERFID.Text.ToString().Replace(" ", "");
            //如果输入供应商信息更新公共数据

            foreach (RepeaterItem Retem in comparepriceRepeater.Items)
            {
                zxnum = Convert.ToDouble(((Label)Retem.FindControl("PIC_ZXNUM")).Text.ToString());
                zxfznum = Convert.ToDouble(((Label)Retem.FindControl("PIC_ZXFUNUM")).Text.ToString());
                resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                shuilv = Convert.ToDouble(((TextBox)Retem.FindControl("PIC_SHUILV")).Text.ToString());
                note = ((Label)Retem.FindControl("PIC_NOTE")).Text.ToString();
                ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                sqltext = "UPDATE TBPC_IQRCMPPRICE SET  " +
                          "PIC_QUANTITY=" + zxnum + ",PIC_FZNUM=" +
                          zxfznum + ",PIC_ZXNUM=" + zxnum + ",PIC_ZXFUNUM=" +
                          zxfznum + ",PIC_SUPPLIERRESID='" + resproid +
                          "',PIC_SHUILV=" + shuilv + ",PIC_NOTE='" +
                          note + "'  WHERE PIC_PTCODE='" + ptcode + "'";
                //PIC_STATE='1'已保存
                DBCallCommon.ExeSqlText(sqltext);
            }

            #region
            if (PIC_SUPPLIERAID.Text.ToString().Replace(" ", "") != "" && PIC_SUPPLIERANAME.Text.ToString().Replace(" ", "") != "")
            {
                //保存供应商1的数据
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    suppaid = PIC_SUPPLIERAID.Text;
                    string fstsa = ((TextBox)Retem.FindControl("TextBox11")).Text.ToString();
                    double secsa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox12")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox12")).Text.ToString());
                    double lassa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox13")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox13")).Text.ToString());

                    string sqltext1 = "UPDATE TBPC_IQRCMPPRICE SET PIC_SUPPLIERAID='" + suppaid + "'";
                    if (fstsa != "")
                    {
                        sqltext1 = sqltext1 + ",PIC_QOUTEFSTSA='" + fstsa + "'";
                    }
                    else if (fstsa == "")
                    {
                        sqltext1 = sqltext1 + ",PIC_QOUTEFSTSA=null";
                    }
                    if (secsa != 0)
                    {
                        sqltext1 = sqltext1 + ",PIC_QOUTESCDSA=" + secsa + "";
                    }
                    else if (secsa == 0)
                    {
                        sqltext1 = sqltext1 + ",PIC_QOUTESCDSA=null";
                    }
                    if (lassa != 0)
                    {
                        sqltext1 = sqltext1 + ",PIC_QOUTELSTSA=" + lassa + "";
                    }
                    else if (lassa == 0)
                    {
                        sqltext1 = sqltext1 + ",PIC_QOUTELSTSA=null";
                    }
                    sqltext1 = sqltext1 + "  WHERE PIC_PTCODE='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sqltext1);
                    sqltext1 = "update TBPC_IQRCMPPRICE set PIC_SUPPLIERAID='" + suppaid + "' where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext1);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PIC_SUPPLIERAID.Text)
                    {
                        sqltext = "update TBPC_IQRCMPPRICE set PIC_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox12")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox12")).Text.ToString()) + "' " +
                                  "where PIC_PTCODE='" + ((Label)Retem.FindControl("PIC_PCODE")).Text + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    string sql1 = "UPDATE TBPC_IQRCMPPRICE SET PIC_SUPPLIERAID=null,PIC_QOUTEFSTSA=null,PIC_QOUTESCDSA=null,PIC_QOUTELSTSA=null where PIC_PTCODE='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sql1);
                }
            }
            #endregion
            #region
            if (PIC_SUPPLIERBID.Text.ToString().Replace(" ", "") != "" && PIC_SUPPLIERBNAME.Text.ToString().Replace(" ", "") != "")
            {
                //保存供应商2的数据
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    suppbid = PIC_SUPPLIERBID.Text;
                    string fstsa = ((TextBox)Retem.FindControl("TextBox21")).Text.ToString();
                    double secsa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox22")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox22")).Text.ToString());
                    double lassa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox23")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox23")).Text.ToString());
                    string sqltext2 = "UPDATE TBPC_IQRCMPPRICE SET PIC_SUPPLIERBID='" + suppbid + "'";
                    if (fstsa != "")
                    {
                        sqltext2 = sqltext2 + ",PIC_QOUTEFSTSB='" + fstsa + "' ";
                    }
                    else if (fstsa == "")
                    {
                        sqltext2 = sqltext2 + ",PIC_QOUTEFSTSB=null ";
                    }
                    if (secsa != 0)
                    {
                        sqltext2 = sqltext2 + ",PIC_QOUTESCDSB=" + secsa + " ";
                    }
                    else if (secsa == 0)
                    {
                        sqltext2 = sqltext2 + ",PIC_QOUTESCDSB=null ";
                    }
                    if (lassa != 0)
                    {
                        sqltext2 = sqltext2 + ",PIC_QOUTELSTSB=" + lassa + " ";
                    }
                    else if (lassa == 0)
                    {
                        sqltext2 = sqltext2 + ",PIC_QOUTELSTSB=null ";
                    }
                    sqltext2 = sqltext2 + "  WHERE PIC_PTCODE='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sqltext2);
                    sqltext2 = "update TBPC_IQRCMPPRICE set PIC_SUPPLIERBID='" + suppbid + "' where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext2);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PIC_SUPPLIERBID.Text)
                    {
                        sqltext = "update TBPC_IQRCMPPRICE set PIC_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox22")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox22")).Text.ToString()) + "' " +
                                  "where PIC_PTCODE='" + ((Label)Retem.FindControl("PIC_PCODE")).Text + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    string sql2 = "UPDATE TBPC_IQRCMPPRICE SET PIC_SUPPLIERBID=null,PIC_QOUTEFSTSB=null,PIC_QOUTESCDSB=null,PIC_QOUTELSTSB=null where PIC_PTCODE='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sql2);
                }
            }
            #endregion
            #region
            if (PIC_SUPPLIERCID.Text.ToString().Replace(" ", "") != "" && PIC_SUPPLIERCNAME.Text.ToString().Replace(" ", "") != "")
            {
                //保存供应商3的数据

                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    suppcid = PIC_SUPPLIERCID.Text;
                    string fstsa = ((TextBox)Retem.FindControl("TextBox31")).Text.ToString();
                    double secsa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox32")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox32")).Text.ToString());
                    double lassa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox33")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox33")).Text.ToString());
                    string sqltext3 = "UPDATE TBPC_IQRCMPPRICE SET PIC_SUPPLIERCID='" + suppcid + "'";
                    if (fstsa != "")
                    {
                        sqltext3 = sqltext3 + ",PIC_QOUTEFSTSC='" + fstsa + "' ";
                    }
                    else if (fstsa == "")
                    {
                        sqltext3 = sqltext3 + ",PIC_QOUTEFSTSC=null ";
                    }
                    if (secsa != 0)
                    {
                        sqltext3 = sqltext3 + ",PIC_QOUTESCDSC=" + secsa + " ";
                    }
                    else if (secsa == 0)
                    {
                        sqltext3 = sqltext3 + ",PIC_QOUTESCDSC=null ";
                    }
                    if (lassa != 0)
                    {
                        sqltext3 = sqltext3 + ",PIC_QOUTELSTSC=" + lassa + " ";
                    }
                    else if (lassa == 0)
                    {
                        sqltext3 = sqltext3 + ",PIC_QOUTELSTSC=null ";
                    }
                    sqltext3 = sqltext3 + "  WHERE PIC_PTCODE='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sqltext3);
                    sqltext3 = "update TBPC_IQRCMPPRICE set PIC_SUPPLIERCID='" + suppcid + "' where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext3);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PIC_SUPPLIERCID.Text)
                    {
                        sqltext = "update TBPC_IQRCMPPRICE set PIC_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox32")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox32")).Text.ToString()) + "' " +
                                  "where PIC_PTCODE='" + ((Label)Retem.FindControl("PIC_PCODE")).Text + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    string sql3 = "UPDATE TBPC_IQRCMPPRICE SET PIC_SUPPLIERCID=null,PIC_QOUTEFSTSC=null,PIC_QOUTESCDSC=null,PIC_QOUTELSTSC=null where PIC_PTCODE='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sql3);
                }
            }
            #endregion
            #region
            if (PIC_SUPPLIERDID.Text.ToString().Replace(" ", "") != "" && PIC_SUPPLIERDNAME.Text.ToString().Replace(" ", "") != "")
            {
                //保存供应商4的数据

                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    suppdid = PIC_SUPPLIERDID.Text;
                    string fstsa = ((TextBox)Retem.FindControl("TextBox41")).Text.ToString();
                    double secsa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox42")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox42")).Text.ToString());
                    double lassa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox43")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox43")).Text.ToString());
                    string sqltext4 = "UPDATE TBPC_IQRCMPPRICE SET PIC_SUPPLIERDID='" + suppdid + "'";
                    if (fstsa != "")
                    {
                        sqltext4 = sqltext4 + ",PIC_QOUTEFSTSD='" + fstsa + "' ";
                    }
                    else
                    {
                        sqltext4 = sqltext4 + ",PIC_QOUTEFSTSD=null ";
                    }
                    if (secsa != 0)
                    {
                        sqltext4 = sqltext4 + ",PIC_QOUTESCDSD=" + secsa + " ";
                    }
                    else
                    {
                        sqltext4 = sqltext4 + ",PIC_QOUTESCDSD=null";
                    }
                    if (lassa != 0)
                    {
                        sqltext4 = sqltext4 + ",PIC_QOUTELSTSD=" + lassa + " ";
                    }
                    else
                    {
                        sqltext4 = sqltext4 + ",PIC_QOUTELSTSD=null ";
                    }
                    sqltext4 = sqltext4 + "  WHERE PIC_PTCODE='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sqltext4);
                    sqltext4 = "update TBPC_IQRCMPPRICE set PIC_SUPPLIERDID='" + suppdid + "' where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext4);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PIC_SUPPLIERDID.Text)
                    {
                        sqltext = "update TBPC_IQRCMPPRICE set PIC_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox42")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox42")).Text.ToString()) + "' " +
                                  "where PIC_PTCODE='" + ((Label)Retem.FindControl("PIC_PCODE")).Text + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    string sql4 = "UPDATE TBPC_IQRCMPPRICE SET PIC_SUPPLIERDID=null,PIC_QOUTEFSTSD=null,PIC_QOUTESCDSD=null,PIC_QOUTELSTSD=null where PIC_PTCODE='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sql4);
                }
            }
            #endregion
            #region
            if (PIC_SUPPLIEREID.Text.ToString().Replace(" ", "") != "" && PIC_SUPPLIERENAME.Text.ToString().Replace(" ", "") != "")
            {
                //保存供应商5的数据

                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    suppeid = PIC_SUPPLIEREID.Text;
                    string fstsa = ((TextBox)Retem.FindControl("TextBox51")).Text.ToString();
                    double secsa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox52")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox52")).Text.ToString());
                    double lassa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox53")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox53")).Text.ToString());
                    string sqltext5 = "UPDATE TBPC_IQRCMPPRICE SET PIC_SUPPLIEREID='" + suppeid + "'";
                    if (fstsa != "")
                    {
                        sqltext5 = sqltext5 + ",PIC_QOUTEFSTSE='" + fstsa + "' ";
                    }
                    else
                    {
                        sqltext5 = sqltext5 + ",PIC_QOUTEFSTSE=null ";
                    }
                    if (secsa != 0)
                    {
                        sqltext5 = sqltext5 + ",PIC_QOUTESCDSE=" + secsa + " ";
                    }
                    else
                    {
                        sqltext5 = sqltext5 + ",PIC_QOUTESCDSE=null ";
                    }
                    if (lassa != 0)
                    {
                        sqltext5 = sqltext5 + ",PIC_QOUTELSTSE=" + lassa + " ";
                    }
                    else
                    {
                        sqltext5 = sqltext5 + ",PIC_QOUTELSTSE=null ";
                    }
                    sqltext5 = sqltext5 + "  WHERE PIC_PTCODE='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sqltext5);
                    sqltext5 = "update TBPC_IQRCMPPRICE set PIC_SUPPLIEREID='" + suppeid + "' where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext5);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PIC_SUPPLIEREID.Text)
                    {
                        sqltext = "update TBPC_IQRCMPPRICE set PIC_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox53")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox52")).Text.ToString()) + "' " +
                                  "where PIC_PTCODE='" + ((Label)Retem.FindControl("PIC_PCODE")).Text + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    string sql5 = "UPDATE TBPC_IQRCMPPRICE SET PIC_SUPPLIEREID=null,PIC_QOUTEFSTSE=null,PIC_QOUTESCDSE=null,PIC_QOUTELSTSE=null where PIC_PTCODE='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sql5);
                }
            }
            #endregion
            #region
            if (PIC_SUPPLIERFID.Text.ToString().Replace(" ", "") != "" && PIC_SUPPLIERFNAME.Text.ToString().Replace(" ", "") != "")
            {
                //保存供应商6的数据
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    suppfid = PIC_SUPPLIERFID.Text;
                    string fstsa = ((TextBox)Retem.FindControl("TextBox61")).Text.ToString();
                    double secsa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox62")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox62")).Text.ToString());
                    double lassa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox63")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox63")).Text.ToString());
                    string sqltext6 = "UPDATE TBPC_IQRCMPPRICE SET PIC_SUPPLIERFID='" + suppfid + "'";
                    if (fstsa != "")
                    {
                        sqltext6 = sqltext6 + ",PIC_QOUTEFSTSF='" + fstsa + "' ";
                    }
                    else
                    {
                        sqltext6 = sqltext6 + ",PIC_QOUTEFSTSF=null ";
                    }
                    if (secsa != 0)
                    {
                        sqltext6 = sqltext6 + ",PIC_QOUTESCDSF=" + secsa + " ";
                    }
                    else
                    {
                        sqltext6 = sqltext6 + ",PIC_QOUTESCDSF=null ";
                    }
                    if (lassa != 0)
                    {
                        sqltext6 = sqltext6 + ",PIC_QOUTELSTSF=" + lassa + " ";
                    }
                    else
                    {
                        sqltext6 = sqltext6 + ",PIC_QOUTELSTSF=null ";
                    }
                    sqltext6 = sqltext6 + "  WHERE PIC_PTCODE='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sqltext6);
                    sqltext6 = "update TBPC_IQRCMPPRICE set PIC_SUPPLIERFID='" + suppfid + "' where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext6);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PIC_SUPPLIERFID.Text)
                    {
                        sqltext = "update TBPC_IQRCMPPRICE set PIC_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox62")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox62")).Text.ToString()) + "' " +
                                  "where PIC_PTCODE='" + ((Label)Retem.FindControl("PIC_PCODE")).Text + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    string sql5 = "UPDATE TBPC_IQRCMPPRICE SET PIC_SUPPLIERFID=null,PIC_QOUTEFSTSF=null,PIC_QOUTESCDSF=null,PIC_QOUTELSTSF=null where PIC_PTCODE='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sql5);
                }
            }
            #endregion
            string sqltext7 = "UPDATE TBPC_IQRCMPPRCRVW SET ICL_ZHUGUANID='" + cob_zgjl.SelectedValue.ToString() +
                              "',ICL_FUZRID='" + cob_fuziren.SelectedValue.ToString() + "' where ICL_SHEETNO='" + TextBoxNO.Text + "'";
            DBCallCommon.ExeSqlText(sqltext7);
        }
        #region
        private double sum12 = 0;
        private double sum13 = 0;
        private double sum22 = 0;
        private double sum23 = 0;
        private double sum32 = 0;
        private double sum33 = 0;
        private double sum42 = 0;
        private double sum43 = 0;
        private double sum52 = 0;
        private double sum53 = 0;
        private double sum62 = 0;
        private double sum63 = 0;
        private int num = 0;
        #endregion
        protected void comparepriceRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlTableCell wlxx = (HtmlTableCell)e.Item.FindControl("wlxx");
                HtmlTableCell jhhao = (HtmlTableCell)e.Item.FindControl("jhhao");
                HtmlTableCell marbianma = (HtmlTableCell)e.Item.FindControl("marbianma");
                HtmlTableCell martuhao = (HtmlTableCell)e.Item.FindControl("martuhao");
                HtmlTableCell marcaizhi = (HtmlTableCell)e.Item.FindControl("marcaizhi");
                HtmlTableCell marguobiao = (HtmlTableCell)e.Item.FindControl("marguobiao");
                HtmlTableCell cgshuliang = (HtmlTableCell)e.Item.FindControl("cgshuliang");
                HtmlTableCell danwei = (HtmlTableCell)e.Item.FindControl("danwei");
                HtmlTableCell shuilv = (HtmlTableCell)e.Item.FindControl("shuilv");
                HtmlTableCell jine = (HtmlTableCell)e.Item.FindControl("jine");
                HtmlTableCell length = (HtmlTableCell)e.Item.FindControl("length");
                HtmlTableCell width = (HtmlTableCell)e.Item.FindControl("width");
                HtmlTableCell beizhu = (HtmlTableCell)e.Item.FindControl("beizhu");
                HtmlTableCell zxfznum = (HtmlTableCell)e.Item.FindControl("zxfznum1");
                HtmlTableCell fzunit = (HtmlTableCell)e.Item.FindControl("fzunit1");

                if (CheckBox1.Checked) { num++; jhhao.Visible = false; }
                if (CheckBox2.Checked) { num++; marbianma.Visible = false; }
                if (CheckBox3.Checked) { num++; martuhao.Visible = false; }
                if (CheckBox4.Checked) { num++; marcaizhi.Visible = false; }
                if (CheckBox5.Checked) { num++; marguobiao.Visible = false; }
                if (CheckBox6.Checked) { num++; cgshuliang.Visible = false; }
                if (CheckBox7.Checked) { num++; danwei.Visible = false; }
                if (CheckBox8.Checked) { num++; shuilv.Visible = false; }
                if (CheckBox9.Checked) { num++; jine.Visible = false; }
                if (CheckBox10.Checked) { num++; length.Visible = false; }
                if (CheckBox11.Checked) { num++; width.Visible = false; }
                if (CheckBox12.Checked) { num++; beizhu.Visible = false; }
                if (CheckBox13.Checked) { num++; zxfznum.Visible = false; }
                if (CheckBox14.Checked) { num++; fzunit.Visible = false; }
                wlxx.ColSpan = 22 - Convert.ToInt32(globnum);


            }
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (((Label)e.Item.FindControl("PIC_ZXNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("PIC_ZXNUM")).Text.ToString() == System.String.Empty)
                {
                    ((Label)e.Item.FindControl("PIC_ZXNUM")).Text = "0";
                }

                string sql = "SELECT  PIC_SHEETNO, sum(PIC_QOUTESCDSA) as sum12, sum(PIC_QOUTELSTSA) as sum13 ,sum(PIC_QOUTESCDSB) as sum22 , " +
                                " sum(PIC_QOUTELSTSB) as sum23,sum(PIC_QOUTESCDSC) as sum32 ,sum(PIC_QOUTELSTSC) as sum33,sum(PIC_QOUTESCDSD) as sum42 ,sum(PIC_ZXNUM*PIC_QOUTELSTSD) as sum43 , " +
                               " sum(PIC_QOUTESCDSE) as sum52 ,sum(PIC_QOUTELSTSE) as sum53 ,sum(PIC_QOUTESCDSF) as sum62 ,sum(PIC_QOUTELSTSF) as sum63  " +
                               " FROM   TBPC_IQRCMPPRICE  where PIC_SHEETNO='" + TextBoxNO.Text + "' group by PIC_SHEETNO";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {

                    if (dt.Rows[0]["sum12"].ToString() == "")
                    {
                        sum12 = 0;
                    }
                    else
                    {
                        sum12 = Convert.ToDouble(dt.Rows[0]["sum12"].ToString());
                    }

                    if (dt.Rows[0]["sum13"].ToString() == "")
                    {
                        sum13 = 0;
                    }
                    else
                    {
                        sum13 = Convert.ToDouble(dt.Rows[0]["sum13"].ToString());
                    }


                    if (dt.Rows[0]["sum22"].ToString() == "")
                    {
                        sum22 = 0;
                    }
                    else
                    {
                        sum22 = Convert.ToDouble(dt.Rows[0]["sum22"].ToString());
                    }
                    if (dt.Rows[0]["sum23"].ToString() == "")
                    {
                        sum23 = 0;
                    }
                    else
                    {
                        sum23 = Convert.ToDouble(dt.Rows[0]["sum23"].ToString());
                    }

                    if (dt.Rows[0]["sum32"].ToString() == "")
                    {
                        sum32 = 0;
                    }
                    else
                    {
                        sum32 = Convert.ToDouble(dt.Rows[0]["sum32"].ToString());
                    }
                    if (dt.Rows[0]["sum33"].ToString() == "")
                    {
                        sum33 = 0;
                    }
                    else
                    {
                        sum33 = Convert.ToDouble(dt.Rows[0]["sum33"].ToString());
                    }

                    if (dt.Rows[0]["sum42"].ToString() == "")
                    {
                        sum42 = 0;
                    }
                    else
                    {
                        sum42 = Convert.ToDouble(dt.Rows[0]["sum42"].ToString());
                    }
                    if (dt.Rows[0]["sum43"].ToString() == "")
                    {
                        sum43 = 0;
                    }
                    else
                    {
                        sum43 = Convert.ToDouble(dt.Rows[0]["sum43"].ToString());
                    }

                    if (dt.Rows[0]["sum52"].ToString() == "")
                    {
                        sum52 = 0;
                    }
                    else
                    {
                        sum52 = Convert.ToDouble(dt.Rows[0]["sum52"].ToString());
                    }
                    if (dt.Rows[0]["sum53"].ToString() == "")
                    {
                        sum53 = 0;
                    }
                    else
                    {
                        sum53 = Convert.ToDouble(dt.Rows[0]["sum53"].ToString());
                    }

                    if (dt.Rows[0]["sum62"].ToString() == "")
                    {
                        sum62 = 0;
                    }
                    else
                    {
                        sum62 = Convert.ToDouble(dt.Rows[0]["sum62"].ToString());
                    }
                    if (dt.Rows[0]["sum63"].ToString() == "")
                    {
                        sum63 = 0;
                    }
                    else
                    {
                        sum63 = Convert.ToDouble(dt.Rows[0]["sum63"].ToString());
                    }
                }
                #region
                if (((TextBox)e.Item.FindControl("TextBox11")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox11")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox11")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox11")).Text = "";
                }
                #endregion
                #region
                if (((TextBox)e.Item.FindControl("TextBox12")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox12")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox12")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox12")).Text = "";
                }
                #endregion
                #region
                if (((TextBox)e.Item.FindControl("TextBox13")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox13")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox13")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox13")).Text = "";
                }
                #endregion
                #region
                if (((TextBox)e.Item.FindControl("TextBox21")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox21")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox21")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox21")).Text = "";
                }
                #endregion
                #region
                if (((TextBox)e.Item.FindControl("TextBox22")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox22")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox22")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox22")).Text = "";
                }
                #endregion
                #region
                if (((TextBox)e.Item.FindControl("TextBox23")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox23")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox23")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox23")).Text = "";
                }
                #endregion
                #region
                if (((TextBox)e.Item.FindControl("TextBox31")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox31")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox31")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox31")).Text = "";
                }
                #endregion
                #region
                if (((TextBox)e.Item.FindControl("TextBox32")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox32")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox32")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox32")).Text = "";
                }
                #endregion
                #region
                if (((TextBox)e.Item.FindControl("TextBox33")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox33")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox33")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox33")).Text = "";
                }
                #endregion
                #region
                if (((TextBox)e.Item.FindControl("TextBox41")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox41")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox41")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox41")).Text = "";
                }
                #endregion
                #region
                if (((TextBox)e.Item.FindControl("TextBox42")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox42")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox42")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox42")).Text = "";
                }
                #endregion
                #region
                if (((TextBox)e.Item.FindControl("TextBox43")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox43")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox43")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox43")).Text = "";
                }
                #endregion
                #region
                if (((TextBox)e.Item.FindControl("TextBox51")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox51")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox51")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox51")).Text = "";
                }
                #endregion
                #region
                if (((TextBox)e.Item.FindControl("TextBox52")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox52")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox52")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox52")).Text = "";
                }
                #endregion
                #region
                if (((TextBox)e.Item.FindControl("TextBox53")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox53")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox53")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox53")).Text = "";
                }
                #endregion
                #region
                if (((TextBox)e.Item.FindControl("TextBox61")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox61")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox61")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox61")).Text = "";
                }
                #endregion
                #region
                if (((TextBox)e.Item.FindControl("TextBox62")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox62")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox62")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox62")).Text = "";
                }
                #endregion
                #region
                if (((TextBox)e.Item.FindControl("TextBox63")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("TextBox63")).Text.ToString() == System.String.Empty || ((TextBox)e.Item.FindControl("TextBox63")).Text.ToString() == "0")
                {
                    ((TextBox)e.Item.FindControl("TextBox63")).Text = "";
                }
                #endregion
                HtmlTableCell jhhao1 = (HtmlTableCell)e.Item.FindControl("jhhao1");
                HtmlTableCell marbianma1 = (HtmlTableCell)e.Item.FindControl("marbianma1");
                HtmlTableCell martuhao1 = (HtmlTableCell)e.Item.FindControl("martuhao1");
                HtmlTableCell marcaizhi1 = (HtmlTableCell)e.Item.FindControl("marcaizhi1");
                HtmlTableCell marguobiao1 = (HtmlTableCell)e.Item.FindControl("marguobiao1");
                HtmlTableCell cgshuliang1 = (HtmlTableCell)e.Item.FindControl("cgshuliang1");
                HtmlTableCell danwwei1 = (HtmlTableCell)e.Item.FindControl("danwwei1");
                HtmlTableCell shuilv1 = (HtmlTableCell)e.Item.FindControl("shuilv1");
                HtmlTableCell jine1 = (HtmlTableCell)e.Item.FindControl("jine1");
                HtmlTableCell length1 = (HtmlTableCell)e.Item.FindControl("length1");
                HtmlTableCell width1 = (HtmlTableCell)e.Item.FindControl("width1");
                HtmlTableCell beizhu1 = (HtmlTableCell)e.Item.FindControl("beizhu1");
                HtmlTableCell zxfznum2 = (HtmlTableCell)e.Item.FindControl("zxfznum2");
                HtmlTableCell fzunit2 = (HtmlTableCell)e.Item.FindControl("fzunit2");

                if (CheckBox1.Checked) { jhhao1.Visible = false; }
                if (CheckBox2.Checked) { marbianma1.Visible = false; }
                if (CheckBox3.Checked) { martuhao1.Visible = false; }
                if (CheckBox4.Checked) { marcaizhi1.Visible = false; }
                if (CheckBox5.Checked) { marguobiao1.Visible = false; }
                if (CheckBox6.Checked) { cgshuliang1.Visible = false; }
                if (CheckBox7.Checked) { danwwei1.Visible = false; }
                if (CheckBox8.Checked) { shuilv1.Visible = false; }
                if (CheckBox9.Checked) { jine1.Visible = false; }
                if (CheckBox10.Checked) { length1.Visible = false; }
                if (CheckBox11.Checked) { width1.Visible = false; }
                if (CheckBox12.Checked) { beizhu1.Visible = false; }
                if (CheckBox13.Checked) { zxfznum2.Visible = false; }
                if (CheckBox14.Checked) { fzunit2.Visible = false; }

                if (i == 0)
                {
                    ((TextBox)e.Item.FindControl("TextBox11")).Attributes.Add("onblur", "drapDown(this,'.rq1')");
                    ((TextBox)e.Item.FindControl("TextBox21")).Attributes.Add("onblur", "drapDown(this,'.rq2')");
                    ((TextBox)e.Item.FindControl("TextBox31")).Attributes.Add("onblur", "drapDown(this,'.rq3')");
                    ((TextBox)e.Item.FindControl("TextBox41")).Attributes.Add("onblur", "drapDown(this,'.rq4')");
                    ((TextBox)e.Item.FindControl("TextBox51")).Attributes.Add("onblur", "drapDown(this,'.rq5')");
                    ((TextBox)e.Item.FindControl("TextBox61")).Attributes.Add("onblur", "drapDown(this,'.rq6')");
                    i++;
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                #region
                ((TextBox)(e.Item.FindControl("totalTextBox13"))).Text = Convert.ToString(sum13);
                ((TextBox)(e.Item.FindControl("totalTextBox23"))).Text = Convert.ToString(sum23);
                ((TextBox)(e.Item.FindControl("totalTextBox33"))).Text = Convert.ToString(sum33);
                ((TextBox)(e.Item.FindControl("totalTextBox43"))).Text = Convert.ToString(sum43);
                ((TextBox)(e.Item.FindControl("totalTextBox53"))).Text = Convert.ToString(sum53);
                ((TextBox)(e.Item.FindControl("totalTextBox63"))).Text = Convert.ToString(sum63);
                sum13 = 0;
                sum23 = 0;
                sum33 = 0;
                sum43 = 0;
                sum53 = 0;
                sum63 = 0;
                #endregion
                HtmlTableCell foot1 = (HtmlTableCell)e.Item.FindControl("foot1");
                foot1.ColSpan = 23 - Convert.ToInt32(globnum);   //汇总

            }
        }

        int i = 0;
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

        //返回按钮
        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("TBPC_IQRCMPPRCLST_checked_detail.aspx?sheetno=" + TextBoxNO.Text);
        }
        //比总价保存
        protected void btn_BZJ_Click(object sender, EventArgs e)
        {
            string ptc = "";
            double price = 0;
            string providerid = "";
            string sqlt = "";
            int i = 0;
            //第一个供应商
            if (resetprovider(PIC_SUPPLIERA, PIC_SUPPLIERAID, PIC_SUPPLIERANAME, LbA_lei))
            {
                initdropdprovider();
            }
            else
            {
                PIC_SUPPLIERAID.Text = "";
                PIC_SUPPLIERANAME.Text = "";
                PIC_SUPPLIERA.Text = "";
                LbA_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第二个供应商
            if (resetprovider(PIC_SUPPLIERB, PIC_SUPPLIERBID, PIC_SUPPLIERBNAME, LbB_lei))
            {
                initdropdprovider();
            }
            else
            {
                PIC_SUPPLIERBID.Text = "";
                PIC_SUPPLIERBNAME.Text = "";
                PIC_SUPPLIERB.Text = "";
                LbB_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第三个供应商
            if (resetprovider(PIC_SUPPLIERC, PIC_SUPPLIERCID, PIC_SUPPLIERCNAME, LbC_lei))
            {
                initdropdprovider();
            }
            else
            {
                PIC_SUPPLIERCID.Text = "";
                PIC_SUPPLIERCNAME.Text = "";
                PIC_SUPPLIERC.Text = "";
                LbC_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第四个供应商
            if (resetprovider(PIC_SUPPLIERD, PIC_SUPPLIERDID, PIC_SUPPLIERDNAME, LbD_lei))
            {
                initdropdprovider();
            }
            else
            {
                PIC_SUPPLIERDID.Text = "";
                PIC_SUPPLIERDNAME.Text = "";
                PIC_SUPPLIERD.Text = "";
                LbD_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第五个供应商
            if (resetprovider(PIC_SUPPLIERE, PIC_SUPPLIEREID, PIC_SUPPLIERENAME, LbE_lei))
            {
                initdropdprovider();
            }
            else
            {
                PIC_SUPPLIEREID.Text = "";
                PIC_SUPPLIERENAME.Text = "";
                PIC_SUPPLIERE.Text = "";
                LbE_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第六个供应商
            if (resetprovider(PIC_SUPPLIERF, PIC_SUPPLIERFID, PIC_SUPPLIERFNAME, LbF_lei))
            {
                initdropdprovider();
            }
            else
            {
                PIC_SUPPLIERFID.Text = "";
                PIC_SUPPLIERFNAME.Text = "";
                PIC_SUPPLIERF.Text = "";
                LbF_lei.Text = "";
                initdropdprovider();
                i++;
            }
            if (i > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('你输入了相同的供应商，请重新输入！');", true);
            }
            savedate();
            comparepriceRepeaterdatabind();

            double ZJE1 = Convert.ToDouble(((TextBox)(comparepriceRepeater.Controls[comparepriceRepeater.Controls.Count - 1].FindControl("totalTextBox13"))).Text);
            double ZJE2 = Convert.ToDouble(((TextBox)(comparepriceRepeater.Controls[comparepriceRepeater.Controls.Count - 1].FindControl("totalTextBox23"))).Text);
            double ZJE3 = Convert.ToDouble(((TextBox)(comparepriceRepeater.Controls[comparepriceRepeater.Controls.Count - 1].FindControl("totalTextBox33"))).Text);
            double ZJE4 = Convert.ToDouble(((TextBox)(comparepriceRepeater.Controls[comparepriceRepeater.Controls.Count - 1].FindControl("totalTextBox43"))).Text);
            double ZJE5 = Convert.ToDouble(((TextBox)(comparepriceRepeater.Controls[comparepriceRepeater.Controls.Count - 1].FindControl("totalTextBox53"))).Text);
            double ZJE6 = Convert.ToDouble(((TextBox)(comparepriceRepeater.Controls[comparepriceRepeater.Controls.Count - 1].FindControl("totalTextBox63"))).Text);

            double minje = 0;
            if (ZJE1 != 0)
            {
                double zje1 = ZJE1;
                if (ZJE2 != 0)
                {
                    double zje2 = ZJE2;
                    minje = Math.Min(zje1, zje2);
                    if (ZJE3 != 0)
                    {
                        double zje3 = ZJE3;
                        minje = Math.Min(minje, zje3);
                        if (ZJE4 != 0)
                        {
                            double zje4 = ZJE4;
                            minje = Math.Min(minje, zje4);
                            if (ZJE5 != 0)
                            {
                                double zje5 = ZJE5;
                                minje = Math.Min(minje, zje5);
                                if (ZJE6 != 0)
                                {
                                    double zje6 = ZJE6;
                                    minje = Math.Min(minje, zje6);
                                }
                            }

                        }

                    }

                }

            }
            if (minje == ZJE1)
            {
                providerid = PIC_SUPPLIERAID.Text;
                sqlt = "select PIC_PTCODE,PIC_QOUTELSTSA from TBPC_IQRCMPPRICE where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PIC_PTCODE"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PIC_QOUTELSTSA"].ToString() == "" ? "0" : dt.Rows[k]["PIC_QOUTELSTSA"].ToString());
                        sqlt = "update TBPC_IQRCMPPRICE  set PIC_PRICE=" + price + " where PIC_PTCODE='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBPC_IQRCMPPRICE set PIC_SUPPLIERRESID='" + providerid + "' where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DBCallCommon.ExeSqlText(sqlt);
            }
            else if (minje == ZJE2)
            {
                providerid = PIC_SUPPLIERBID.Text;
                sqlt = "select PIC_PTCODE,PIC_QOUTELSTSB from TBPC_IQRCMPPRICE where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PIC_PTCODE"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PIC_QOUTELSTSB"].ToString() == "" ? "0" : dt.Rows[k]["PIC_QOUTELSTSB"].ToString());
                        sqlt = "update TBPC_IQRCMPPRICE  set PIC_PRICE=" + price + " where PIC_PTCODE='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBPC_IQRCMPPRICE set PIC_SUPPLIERRESID='" + providerid + "'  where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DBCallCommon.ExeSqlText(sqlt);
            }
            else if (minje == ZJE3)
            {
                providerid = PIC_SUPPLIERCID.Text;
                sqlt = "select PIC_PTCODE,PIC_QOUTELSTSC from TBPC_IQRCMPPRICE where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PIC_PTCODE"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PIC_QOUTELSTSC"].ToString() == "" ? "0" : dt.Rows[k]["PIC_QOUTELSTSC"].ToString());
                        sqlt = "update TBPC_IQRCMPPRICE  set PIC_PRICE=" + price + " where PIC_PTCODE='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBPC_IQRCMPPRICE set PIC_SUPPLIERRESID='" + providerid + "' where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DBCallCommon.ExeSqlText(sqlt);
            }
            else if (minje == ZJE4)
            {
                providerid = PIC_SUPPLIERDID.Text;
                sqlt = "select PIC_PTCODE,PIC_QOUTELSTSD from TBPC_IQRCMPPRICE where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PIC_PTCODE"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PIC_QOUTELSTSD"].ToString() == "" ? "0" : dt.Rows[k]["PIC_QOUTELSTSD"].ToString());
                        sqlt = "update TBPC_IQRCMPPRICE  set PIC_PRICE=" + price + " where PIC_PTCODE='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBPC_IQRCMPPRICE set PIC_SUPPLIERRESID='" + providerid + "'  where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DBCallCommon.ExeSqlText(sqlt);
            }
            else if (minje == ZJE5)
            {
                providerid = PIC_SUPPLIEREID.Text;
                sqlt = "select PIC_PTCODE,PIC_QOUTELSTSE from TBPC_IQRCMPPRICE where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PIC_PTCODE"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PIC_QOUTELSTSE"].ToString() == "" ? "0" : dt.Rows[k]["PIC_QOUTELSTSE"].ToString());
                        sqlt = "update TBPC_IQRCMPPRICE  set PIC_PRICE=" + price + " where PIC_PTCODE='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBPC_IQRCMPPRICE set PIC_SUPPLIERRESID='" + providerid + "'  where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DBCallCommon.ExeSqlText(sqlt);
            }
            else if (minje == ZJE6)
            {
                providerid = PIC_SUPPLIERFID.Text;
                sqlt = "select PIC_PTCODE,PIC_QOUTELSTSE from TBPC_IQRCMPPRICE where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PIC_PTCODE"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PIC_QOUTELSTSF"].ToString() == "" ? "0" : dt.Rows[k]["PIC_QOUTELSTSF"].ToString());
                        sqlt = "update TBPC_IQRCMPPRICE  set PIC_PRICE=" + price + " where PIC_PTCODE='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBPC_IQRCMPPRICE set PIC_SUPPLIERRESID='" + providerid + "'  where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DBCallCommon.ExeSqlText(sqlt);
            }
            comparepriceRepeaterdatabind();
            savedate();
            Initpage();
            comparepriceRepeaterdatabind();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);
        }
        private void initdropdprovider2(int selectindex)
        {
            string providerid = "";
            string providernm = "";
            string text = "";
            foreach (RepeaterItem reitem in comparepriceRepeater.Items)
            {

                ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Clear();
                providerid = PIC_SUPPLIERAID.Text;//第一个供应商ID
                providernm = PIC_SUPPLIERANAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox13")).Text.ToString();//供应商1的最终报价
                if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);

                }
                providerid = PIC_SUPPLIERBID.Text;//第二家供应商ID
                providernm = PIC_SUPPLIERBNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox23")).Text.ToString();//供应商2的最终报价
                if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                }
                providerid = PIC_SUPPLIERCID.Text;//第三家供应商ID
                providernm = PIC_SUPPLIERCNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox33")).Text.ToString();//供应商3的最终报价
                if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);

                }
                providerid = PIC_SUPPLIERDID.Text;//第四家供应商
                providernm = PIC_SUPPLIERDNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox43")).Text.ToString();//供应商4的最终报价
                if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);

                }
                providerid = PIC_SUPPLIEREID.Text;//第五家供应商
                providernm = PIC_SUPPLIERENAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox53")).Text.ToString();//供应商5的最终报价
                if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);

                }
                providerid = PIC_SUPPLIERFID.Text;//第六家供应商
                providernm = PIC_SUPPLIERFNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox63")).Text.ToString();//供应商6的最终报价
                if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);

                }
                if (((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count > 0)
                {
                    ((DropDownList)reitem.FindControl("Drp_supplier")).SelectedIndex = selectindex;
                }
            }
        }

        //排序方式改变
        protected void rad_Paixu_Changed(object sender, EventArgs e)
        {
            RebindGrid();
        }
        //重新绑定数据
        private void RebindGrid()
        {
            Initpage();
            comparepriceRepeaterdatabind();
        }

        protected void btn_Export_Click(object sender, EventArgs e)
        {
            //获取表头
            string sqltext = "SELECT distinct  supplieraid as aid, supplieranm1 as anm, supplierarank as arnk, " +
                        "supplierbid as bid, supplierbnm1 as bnm, supplierbrank  as brnk, suppliercid as cid, " +
                        "suppliercnm1 as cnm, suppliercrank as crnk, supplierdid as did, supplierdnm1 as dnm, " +
                        "supplierdrank as drnk, suppliereid as eid, supplierenm1 as enm, suppliererank as ernk, " +
                        "supplierfid as fid, supplierfnm1 as fnm,supplierfrank as frnk  " +
                        "FROM View_TBPC_IQRCMPPRICE where picno='" + TextBoxNO.Text.ToString() + "'";
            DataTable head = DBCallCommon.GetDTUsingSqlText(sqltext);

            DataTable dt = CreateDataSource();

            string filename = "采购比价单导出" + TextBoxNO.Text;
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("采购比价单.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据
                //绑定表头
                IRow row = sheet0.GetRow(0);
                row.Cells[18].SetCellValue(head.Rows[0]["anm"].ToString());
                row.Cells[21].SetCellValue(head.Rows[0]["bnm"].ToString());
                row.Cells[24].SetCellValue(head.Rows[0]["cnm"].ToString());
                row.Cells[27].SetCellValue(head.Rows[0]["dnm"].ToString());
                row.Cells[30].SetCellValue(head.Rows[0]["enm"].ToString());
                row.Cells[33].SetCellValue(head.Rows[0]["fnm"].ToString());

                //写入表体数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    row = sheet0.CreateRow(i + 2);
                    row.CreateCell(0).SetCellValue(i + 1);

                    //dt.Rows[i]["PIC_MAP"].ToString()  dt.Rows[i]["PIC_MAP"].ToString()  
                    row.CreateCell(1).SetCellValue(dt.Rows[i]["ptcode"].ToString());//计划跟踪号
                    row.CreateCell(2).SetCellValue(dt.Rows[i]["PIC_CHILDENGNAME"].ToString());//部件名称
                    row.CreateCell(3).SetCellValue(dt.Rows[i]["PIC_MAP"].ToString());//部件图号
                    row.CreateCell(4).SetCellValue(dt.Rows[i]["margb"].ToString());//国标
                    row.CreateCell(5).SetCellValue(dt.Rows[i]["PIC_TUHAO"].ToString());//图号
                    row.CreateCell(6).SetCellValue(dt.Rows[i]["marnm"].ToString());//物料名称
                    row.CreateCell(7).SetCellValue(dt.Rows[i]["margg"].ToString());//规格
                    row.CreateCell(8).SetCellValue(dt.Rows[i]["marcz"].ToString());//材质
                    row.CreateCell(9).SetCellValue(dt.Rows[i]["length"].ToString());//长
                    row.CreateCell(10).SetCellValue(dt.Rows[i]["width"].ToString());//宽
                    row.CreateCell(11).SetCellValue(dt.Rows[i]["marunit"].ToString());//单位
                    row.CreateCell(12).SetCellValue(dt.Rows[i]["marnum"].ToString());//数量
                    row.CreateCell(13).SetCellValue(dt.Rows[i]["marfzunit"].ToString());//辅助单位
                    row.CreateCell(14).SetCellValue(dt.Rows[i]["marfznum"].ToString());//辅助数量
                    row.CreateCell(15).SetCellValue(dt.Rows[i]["detailnote"].ToString());//备注
                    row.CreateCell(16).SetCellValue(dt.Rows[i]["PIC_MASHAPE"].ToString());//类型
                    row.CreateCell(17).SetCellValue(dt.Rows[i]["ST_SQR"].ToString());//申请人

                    row.CreateCell(18).SetCellValue(dt.Rows[i]["qoutefstsa"].ToString());
                    row.CreateCell(19).SetCellValue(FormatStr(dt.Rows[i]["qoutescdsa"].ToString()));
                    row.CreateCell(20).SetCellValue(FormatStr(dt.Rows[i]["qoutelstsa"].ToString()));
                    row.CreateCell(21).SetCellValue(dt.Rows[i]["qoutefstsb"].ToString());
                    row.CreateCell(22).SetCellValue(FormatStr(dt.Rows[i]["qoutescdsb"].ToString()));
                    row.CreateCell(23).SetCellValue(FormatStr(dt.Rows[i]["qoutelstsb"].ToString()));
                    row.CreateCell(24).SetCellValue(dt.Rows[i]["qoutefstsc"].ToString());
                    row.CreateCell(25).SetCellValue(FormatStr(dt.Rows[i]["qoutescdsc"].ToString()));
                    row.CreateCell(26).SetCellValue(FormatStr(dt.Rows[i]["qoutelstsc"].ToString()));
                    row.CreateCell(27).SetCellValue(dt.Rows[i]["qoutefstsd"].ToString());
                    row.CreateCell(28).SetCellValue(FormatStr(dt.Rows[i]["qoutescdsd"].ToString()));
                    row.CreateCell(29).SetCellValue(FormatStr(dt.Rows[i]["qoutelstsd"].ToString()));
                    row.CreateCell(30).SetCellValue(dt.Rows[i]["qoutefstse"].ToString());
                    row.CreateCell(31).SetCellValue(FormatStr(dt.Rows[i]["qoutescdse"].ToString()));
                    row.CreateCell(32).SetCellValue(FormatStr(dt.Rows[i]["qoutelstse"].ToString()));
                    row.CreateCell(33).SetCellValue(dt.Rows[i]["qoutefstsf"].ToString());
                    row.CreateCell(34).SetCellValue(FormatStr(dt.Rows[i]["qoutescdsf"].ToString()));
                    row.CreateCell(35).SetCellValue(FormatStr(dt.Rows[i]["qoutelstsf"].ToString()));

                    IFont font1 = wk.CreateFont();
                    font1.FontName = "仿宋";//字体
                    font1.FontHeightInPoints = 11;//字号
                    ICellStyle cells = wk.CreateCellStyle();
                    cells.SetFont(font1);

                    for (int j = 1; j < 18; j++)
                    {
                        row.Cells[j].CellStyle = cells;
                    }

                    ICellStyle stylecell = wk.CreateCellStyle();
                    stylecell.SetFont(font1);
                    stylecell.WrapText = true;//自动换行
                    row.Cells[16].CellStyle = stylecell;

                    //row.CreateCell(2).SetCellValue(dt.Rows[i]["marid"].ToString());//物料编码
                }

                #endregion

                for (int i = 0; i <= 33; i++)
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

        protected string FormatStr(string a)
        {
            if (a.Length > 0)
            {
                a = a.Substring(0, a.Length - 2);
            }
            return a;
        }

        protected void btn_Import_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string sql = "delete from TBPC_BIJIADAN where manclerk='" + Session["UserID"].ToString() + "'";//删除表中其他数据，避免数据混乱
            list.Add(sql);

            string FilePath = @"E:\比价单\" + Session["UserName"].ToString();
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            //将文件上传到服务器
            HttpPostedFile UserHPF = FileUpload1.PostedFile;
            try
            {
                string fileContentType = UserHPF.ContentType;// 获取客户端发送的文件的 MIME 内容类型   
                if (fileContentType == "application/vnd.ms-excel")
                {
                    if (UserHPF.ContentLength > 0)
                    {
                        UserHPF.SaveAs(FilePath + "//" + System.IO.Path.GetFileName(UserHPF.FileName));//将上传的文件存放在指定的文件夹中 
                    }
                }
                else
                {
                    Response.Write("<script>alert('文件类型不符合要求，请您核对后重新上传！');</script>"); return;
                }
            }
            catch
            {
                Response.Write("<script>alert('文件上传过程中出现错误！');</script>"); return;
            }

            using (FileStream fs = File.OpenRead(FilePath + "//" + System.IO.Path.GetFileName(UserHPF.FileName)))
            {
                //根据文件流创建一个workbook
                IWorkbook wk = new HSSFWorkbook(fs);

                //获取第一个工作表
                ISheet sheet = wk.GetSheetAt(0);

                //循环读取每一行数据，由于execel有列名以及序号，从1开始
                for (int r = 2; r <= sheet.LastRowNum; r++)
                {
                    string val = "";
                    IRow row = sheet.GetRow(r);

                    #region 存入数据库

                    val = ",'" + row.GetCell(1) + "'";
                    List<int> date = new List<int>() { 18, 21, 24, 27, 30, 33 };
                    for (int i = 18; i < 36; i++)
                    {
                        string str = "";
                        ICell cell = row.GetCell(i);
                        if (cell != null)
                        {
                            str = cell.ToString();
                            if (row.GetCell(i).CellType == CellType.FORMULA || row.GetCell(i).CellType == CellType.NUMERIC)
                            {
                                str = row.GetCell(i).NumericCellValue.ToString();
                            }
                            if (date.Contains(i))
                            {
                                try
                                {
                                    if (cell.CellType == CellType.STRING)
                                    {
                                        if (cell.StringCellValue != "")
                                        {
                                            str = DateTime.Parse(cell.StringCellValue).ToString("yyyy-MM-dd");
                                        }
                                    }
                                    else
                                    {
                                        if (cell.CellType == CellType.BLANK)
                                        {
                                            str = "";
                                        }
                                        else
                                        {
                                            str = cell.DateCellValue.ToString("yyyy-MM-dd");
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    Response.Write("<script>alert('上传文件的交货期有误\\n请确定时间格式（如2012-08-08或者2012/08/08）')</script>");
                                    return;
                                }

                            }
                        }
                        val += ",'" + str + "'";
                    }
                    string sqlTxt = string.Format("insert into TBPC_BIJIADAN values({0},'{1}')", val.Substring(1), Session["UserID"].ToString());
                    list.Add(sqlTxt);

                    #endregion
                }
            }
            DBCallCommon.ExecuteTrans(list);
            foreach (string fileName in Directory.GetFiles(FilePath))//清空该文件夹下的文件
            {
                string newName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                System.IO.File.Delete(FilePath + "\\" + newName);//删除文件下储存的文件
            }
            HidType.Value = "1";//为了避免导入出现错误，先将表导入一张临时表，然后改变hidden，让控件绑定新的数据源

            Initpage();
            comparepriceRepeaterdatabind();
        }


        //招标物料标记
        protected void zbcheck_CheckedChanged(object sender, EventArgs e)
        {
            List<string> listsqlsetzb = new List<string>();
            string sqlsetzb = "";
            string sqlpowerinsert = "";
            string sqlpowerdelete = "";
            if (Chb_Zb.Checked)
            {
                sqlsetzb = "update TBPC_IQRCMPPRCRVW set ICL_TYPE='2' where ICL_SHEETNO='" + TextBoxNO.Text.Trim() + "'";
                listsqlsetzb.Add(sqlsetzb);
                if (PIC_SUPPLIERAID.Text.Trim() != "" && PIC_SUPPLIERANAME.Text.Trim() != "")
                {
                    sqlpowerinsert = "insert into PC_POWERINFO(poweriqcode,powersupplyid) values('" + TextBoxNO.Text.Trim() + "','" + PIC_SUPPLIERAID.Text.Trim() + "')";
                    listsqlsetzb.Add(sqlpowerinsert);
                }
                if (PIC_SUPPLIERBID.Text.Trim() != "" && PIC_SUPPLIERBNAME.Text.Trim() != "")
                {
                    sqlpowerinsert = "insert into PC_POWERINFO(poweriqcode,powersupplyid) values('" + TextBoxNO.Text.Trim() + "','" + PIC_SUPPLIERBID.Text.Trim() + "')";
                    listsqlsetzb.Add(sqlpowerinsert);
                }
                if (PIC_SUPPLIERCID.Text.Trim() != "" && PIC_SUPPLIERCNAME.Text.Trim() != "")
                {
                    sqlpowerinsert = "insert into PC_POWERINFO(poweriqcode,powersupplyid) values('" + TextBoxNO.Text.Trim() + "','" + PIC_SUPPLIERCID.Text.Trim() + "')";
                    listsqlsetzb.Add(sqlpowerinsert);
                }
                if (PIC_SUPPLIERDID.Text.Trim() != "" && PIC_SUPPLIERDNAME.Text.Trim() != "")
                {
                    sqlpowerinsert = "insert into PC_POWERINFO(poweriqcode,powersupplyid) values('" + TextBoxNO.Text.Trim() + "','" + PIC_SUPPLIERDID.Text.Trim() + "')";
                    listsqlsetzb.Add(sqlpowerinsert);
                }
                if (PIC_SUPPLIEREID.Text.Trim() != "" && PIC_SUPPLIERENAME.Text.Trim() != "")
                {
                    sqlpowerinsert = "insert into PC_POWERINFO(poweriqcode,powersupplyid) values('" + TextBoxNO.Text.Trim() + "','" + PIC_SUPPLIEREID.Text.Trim() + "')";
                    listsqlsetzb.Add(sqlpowerinsert);
                }
                if (PIC_SUPPLIERFID.Text.Trim() != "" && PIC_SUPPLIERFNAME.Text.Trim() != "")
                {
                    sqlpowerinsert = "insert into PC_POWERINFO(poweriqcode,powersupplyid) values('" + TextBoxNO.Text.Trim() + "','" + PIC_SUPPLIERFID.Text.Trim() + "')";
                    listsqlsetzb.Add(sqlpowerinsert);
                }
            }
            else
            {
                sqlsetzb = "update TBPC_IQRCMPPRCRVW set ICL_TYPE='0' where ICL_SHEETNO='" + TextBoxNO.Text.Trim() + "'";
                listsqlsetzb.Add(sqlsetzb);

                sqlpowerdelete = "delete from PC_POWERINFO where poweriqcode='" + TextBoxNO.Text.Trim() + "'";
                listsqlsetzb.Add(sqlpowerdelete);
            }

            DBCallCommon.ExecuteTrans(listsqlsetzb);
        }
    }
}
