using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Drawing;
using System.Collections.Generic;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Xie_comprise : System.Web.UI.Page
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
            //btn_cancel.Attributes.Add("OnClick", "Javascript:return confirm('确定取消？执行此操作将停止对选中物料的询比价！');");
           //btn_cancel.Click += new EventHandler(btn_cancel_Click);
            btn_autobj.Attributes.Add("OnClick", "Javascript:return confirm('税率是否需要修改？若税率不是17%，请修改税率！');");
           //btn_BZJ.Attributes.Add("OnClick", "Javascript:return confirm('税率是否需要修改？若税率不是17%，请修改税率！');");
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
               // Hyp_print.NavigateUrl = "TBPC_IQRCMPPRC_detailprint.aspx?sheetno=" + gloabsheetno;
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
            //int num = 0;
            string sqltext = "";
            //负责人下拉框绑定
            //主管下拉框绑定
            sqltext = "select ST_NAME,ST_ID from TBDS_STAFFINFO WHERE ST_POSITION='0401' and ST_PD=0";
            DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqltext);
            ddl_fuziren.DataSource = dt0;
            ddl_fuziren.DataTextField = "ST_NAME";
            ddl_fuziren.DataValueField = "ST_ID";
            ddl_fuziren.DataBind();
            sqltext = "select ST_NAME,ST_ID from TBDS_STAFFINFO WHERE ST_DEPID='01'and ST_PD='0'";
            dt0 = DBCallCommon.GetDTUsingSqlText(sqltext);
            ddl_zgjl.DataSource = dt0;
            ddl_zgjl.DataTextField = "ST_NAME";
            ddl_zgjl.DataValueField = "ST_ID";
            ddl_zgjl.DataBind();
            ddl_zgjl.SelectedValue = "2";
            sqltext = "select zdrid  as ICL_REVIEWA,zdrnm  as ICL_REVIEWANM,zdtime  as ICL_REVIEWATIME, zdryj as ICL_REVIEWAADVC," +
                    "shbid as ICL_REVIEWB,shbnm as ICL_REVIEWBNM,shbtime as ICL_REVIEWBTIME,shbyj as ICL_REVIEWBADVC," +
                    "shcid as ICL_REVIEWC,shcnm as ICL_REVIEWCNM,shctime as ICL_REVIEWCTIME,shcyj as ICL_REVIEWCADVC," +
                    "shdid as ICL_REVIEWD,shdnm as ICL_REVIEWDNM,shdtime as ICL_REVIEWDTIME,shdyj as ICL_REVIEWDADVC," +
                    "sheid as ICL_REVIEWE,shenm as ICL_REVIEWENM,shetime as ICL_REVIEWETIME,sheyi as ICL_REVIEWEADVC," +
                    "shfid as ICL_REVIEWF,shfnm as ICL_REVIEWFNM,shftime as ICL_REVIEWFTIME,shfyj as ICL_REVIEWFADVC," +
                    "shgid as ICL_REVIEWG,shgnm as ICL_REVIEWGNM,shgtime as ICL_REVIEWGTIME,shgyj as ICL_REVIEWGADVC," +
                    "iclfzrid as ICL_FUZRID,iclfzrnm as ICL_FUZRNAME,iclzgid as ICL_ZHUGUANID,iclzgnm as ICL_ZHUGUANNAME," +
                    "isnull(totalstate,0) as ICL_STATE,isnull(statea,0) as ICL_STATEA,isnull(stateb,0) as ICL_STATEB,isnull(statec,0) as ICL_STATEC,isnull(stated,0) as ICL_STATED " +
                    "from View_TBMP_IQRCMPPRCRVW  where picno='" + TextBoxNO.Text.ToString() + "' ";


            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                //制单
                ListItem listfz = new ListItem(dt.Rows[0]["ICL_FUZRNAME"].ToString(), dt.Rows[0]["ICL_FUZRID"].ToString());
                ListItem listzgjl = new ListItem(dt.Rows[0]["ICL_ZHUGUANNAME"].ToString(), dt.Rows[0]["ICL_ZHUGUANID"].ToString());
                tb_zd.Text = dt.Rows[0]["ICL_REVIEWANM"].ToString();
                tb_time.Text = dt.Rows[0]["ICL_REVIEWATIME"].ToString();
                if (ddl_fuziren.Items.Contains(listfz))
                {
                    ddl_fuziren.SelectedValue = dt.Rows[0]["ICL_FUZRID"].ToString();
                }
                if (ddl_zgjl.Items.Contains(listzgjl))
                {
                    ddl_zgjl.SelectedValue = dt.Rows[0]["ICL_ZHUGUANID"].ToString();
                }
            }
           
        }
        private void comparepriceRepeaterdatabind()
        {
            getArticle();
            HeaderTemplatebind();
        }

        private void HeaderTemplatebind()
        {
            string sqltext = "";
            sqltext = "SELECT distinct  supplieraid as aid, supplieranm as anm, supplierarank as arnk, " +
                     "supplierbid as bid, supplierbnm as bnm, supplierbrank  as brnk, suppliercid as cid, " +
                     "suppliercnm as cnm, suppliercrank as crnk, supplierdid as did, supplierdnm as dnm, " +
                     "supplierdrank as drnk, suppliereid as eid, supplierenm as enm, suppliererank as ernk, " +
                     "supplierfid as fid, supplierfnm as fnm,supplierfrank as frnk  " +
                     "FROM View_TBMP_IQRCMPPRICE where picno='" + TextBoxNO.Text.ToString() + "'";
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

               // if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                if (text != "" && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                    min = Convert.ToDouble(text);
                    selectindex = ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count - 1;
                }
                providerid = PIC_SUPPLIERBID.Text;//第二家供应商ID
                providernm = PIC_SUPPLIERBNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox22")).Text.ToString();//供应商2的最终报价
                //if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                if (text != "" && providernm != "")
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
                //if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                if (text != "" && providernm != "")
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
               // if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                if (text != "" && providernm != "")
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
               // if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                if (text != "" && providernm != "")
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
               // if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                if (text != "" && providernm != "")
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
               // if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                if (text != "" && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                    min = Convert.ToDouble(text);
                    selectindex = ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count - 1;
                }
                providerid = PIC_SUPPLIERBID.Text;//第二家供应商ID
                providernm = PIC_SUPPLIERBNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox22")).Text.ToString();//供应商2的最终报价
                //if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                if (text != "" && providernm != "")
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
                //if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                if (text != "" && providernm != "")
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
                //if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                if (text != "" && providernm != "")
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
                //if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                if (text != "" && providernm != "")
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
                //if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                if (text != "" && providernm != "")
                {
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
            ps.PageSize = 200;     //每页显示的数据的行数
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
            string sqltext = "";
            sqltext = "SELECT PIC_ID, PIC_MNAME as marnm, PIC_GUIGE as margg, PIC_CAIZHI as marcz, picno, pjid, pjnm, engid, engnm, marid,PIC_MASHAPE, marnm, margg, margb, marcz, marunit, " +
                      "marfzunit, isnull(length,0) as length ,isnull(width,0) as width , isnull(marnum,0) as marnum, isnull(marfznum,0) as marfznum, isnull(marzxnum,0) as marzxnum, isnull(marzxfznum,0) as marzxfznum, " +
                     "supplierresid, supplierresnm1, supplierresrank, isnull(shuilv,0) as shuilv, price, detamount, ptcode, qoutefstsa, " +
                     "qoutescdsa, qoutelstsa, qoutefstsb, qoutescdsb, qoutelstsb, qoutefstsc, qoutescdsc, " +
                     "qoutelstsc, qoutefstsd, qoutescdsd, qoutelstsd, qoutefstse, qoutescdse, qoutelstse, " +
                     "qoutefstsf, qoutescdsf, qoutelstsf, pmode, keycoms, zdjprice, zdjnum, isnull(detailstate,0) as detailstate,PIC_TUHAO, " +
                //"isnull(detailcstate,0) as detailcstate, detailnote FROM View_TBPC_IQRCMPPRICE  where picno='" + TextBoxNO.Text.ToString() + "' order by ptcode ASC";
                      " ''AS minprice, " +
                //"(SELECT  top(1) PRICE  FROM View_TBPC_IQRCMPPRICE_RVW  GROUP BY MARID,PRICE,IRQDATA,totalstate  having View_TBPC_IQRCMPPRICE.marid=MARID AND totalstate='4' order by IRQDATA DESC ) as lastprice, " +
                      " ''as lastprice, " +
                     "isnull(detailcstate,0) as detailcstate, detailnote FROM View_TBMP_IQRCMPPRICE  where picno='" + TextBoxNO.Text.ToString() + "'and  ( PIC_CFSTATE='0'or PIC_CFSTATE='2')";

            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
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
            comparepriceRepeaterdatabind();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');window.location.href='PM_Xie_check_detail.aspx?num1=2&num2=2&sheetno="+gloabsheetno+"'",true);
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
            comparepriceRepeaterdatabind();
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');window.close()", true);
           // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);
           // Response.Redirect("PM_Xie_check_detail.aspx?sheetno=" + TextBoxNO.Text.ToString() + "");
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');window.location.href='PM_Xie_check_detail.aspx?num1=2&num2=2&sheetno=" + gloabsheetno + "'", true);
        }
        protected void allclose()
        {
            string sqltext = "";
            List<string> sqltexts = new List<string>();
            sqltext = "update TBMP_IQRCMPPRCRVW set ICL_CSTATE='1'  WHERE ICL_SHEETNO='" + TextBoxNO.Text + "'";//单号关闭
            sqltexts.Add(sqltext);
            sqltext = "update  TBMP_IQRCMPPRICE set PIC_CSTATE='1' WHERE PIC_SHEETNO='" + TextBoxNO.Text + "'";//条目关闭
            sqltexts.Add(sqltext);
            DBCallCommon.ExecuteTrans(sqltexts);
        }//单子关闭
        protected void fallclose()
        {
            string sqltext = "";
            List<string> sqltexts = new List<string>();
            sqltext = "update TBMP_IQRCMPPRCRVW set ICL_CSTATE='0'  WHERE ICL_SHEETNO='" + TextBoxNO.Text + "'";//单号反关闭
            sqltexts.Add(sqltext);
            sqltext = "update  TBMP_IQRCMPPRICE set PIC_CSTATE='0' WHERE PIC_SHEETNO='" + TextBoxNO.Text + "'";//条目反关闭
            sqltexts.Add(sqltext);
            DBCallCommon.ExecuteTrans(sqltexts);
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
                sqltext = "UPDATE TBMP_IQRCMPPRICE SET  " +
                          "PIC_QUANTITY='" + zxnum + "',PIC_FZNUM='" +
                          zxfznum + "',PIC_ZXNUM='" + zxnum + "',PIC_SUPPLIERRESID='" + 
                          resproid +"',PIC_SHUILV='" + shuilv + "',PIC_NOTE='" +
                          note + "'  WHERE PIC_ID='" + ptcode + "'";
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

                    string sqltext1 = "UPDATE TBMP_IQRCMPPRICE SET PIC_SUPPLIERAID='" + suppaid + "'";
                    if (fstsa != "")
                    {
                        sqltext1 = sqltext1 + ",PIC_QOUTEFSTSA='" + fstsa + "'";
                    }
                    else if (fstsa == "")
                    {
                        sqltext1 = sqltext1 + ",PIC_QOUTEFSTSA='' ";
                    }
                    //if (secsa != 0)
                    //{
                    //    sqltext1 = sqltext1 + ",PIC_QOUTESCDSA='" + secsa + "'";
                    //}
                    //else if (secsa == 0)
                    //{
                    //    sqltext1 = sqltext1 + ",PIC_QOUTESCDSA=null";
                    //}
                    sqltext1 = sqltext1 + ",PIC_QOUTESCDSA='" + secsa + "'";
                    //if (lassa != 0)
                    //{
                    //    sqltext1 = sqltext1 + ",PIC_QOUTELSTSA='" + lassa + "'";
                    //}
                    //else if (lassa == 0)
                    //{
                    //    sqltext1 = sqltext1 + ",PIC_QOUTELSTSA=null";
                    //}
                    sqltext1 = sqltext1 + ",PIC_QOUTELSTSA='" + lassa + "'";
                    sqltext1 = sqltext1 + "  WHERE PIC_ID='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sqltext1);
                    sqltext1 = "update TBMP_IQRCMPPRICE set PIC_SUPPLIERAID='" + suppaid + "' where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext1);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PIC_SUPPLIERAID.Text)
                    {
                        sqltext = "update TBMP_IQRCMPPRICE set PIC_SUPPLYTIME='" + ((TextBox)Retem.FindControl("TextBox11")).Text.ToString() + "' ,PIC_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox12")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox12")).Text.ToString()) + "' " +
                                  "where PIC_ID='" + ((Label)Retem.FindControl("PIC_PCODE")).Text + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    string sql1 = "UPDATE TBMP_IQRCMPPRICE SET PIC_SUPPLIERAID=null,PIC_QOUTEFSTSA=null,PIC_QOUTESCDSA=null,PIC_QOUTELSTSA=null where PIC_ID='" + ptcode + "'";
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
                    string sqltext2 = "UPDATE TBMP_IQRCMPPRICE SET PIC_SUPPLIERBID='" + suppbid + "'";
                    if (fstsa != "")
                    {
                        sqltext2 = sqltext2 + ",PIC_QOUTEFSTSB='" + fstsa + "' ";
                    }
                    else if (fstsa == "")
                    {
                        sqltext2 = sqltext2 + ",PIC_QOUTEFSTSB=null ";
                    }
                    //if (secsa != 0)
                    //{
                    //    sqltext2 = sqltext2 + ",PIC_QOUTESCDSB=" + secsa + " ";
                    //}
                    //else if (secsa == 0)
                    //{
                    //    sqltext2 = sqltext2 + ",PIC_QOUTESCDSB=null ";
                    //}
                    //if (lassa != 0)
                    //{
                    //    sqltext2 = sqltext2 + ",PIC_QOUTELSTSB=" + lassa + " ";
                    //}
                    //else if (lassa == 0)
                    //{
                    //    sqltext2 = sqltext2 + ",PIC_QOUTELSTSB=null ";
                    //}
                    sqltext2 = sqltext2 + ",PIC_QOUTESCDSB=" + secsa + " ";
                    sqltext2 = sqltext2 + ",PIC_QOUTELSTSB=" + lassa + " ";
                    sqltext2 = sqltext2 + "  WHERE PIC_ID='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sqltext2);
                    sqltext2 = "update TBMP_IQRCMPPRICE set PIC_SUPPLIERBID='" + suppbid + "' where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext2);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PIC_SUPPLIERBID.Text)
                    {
                        sqltext = "update TBMP_IQRCMPPRICE set PIC_SUPPLYTIME='" + ((TextBox)Retem.FindControl("TextBox21")).Text.ToString() + "' , PIC_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox22")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox22")).Text.ToString()) + "' " +
                                  "where PIC_ID='" + ((Label)Retem.FindControl("PIC_PCODE")).Text + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    string sql2 = "UPDATE TBMP_IQRCMPPRICE SET PIC_SUPPLIERBID=null,PIC_QOUTEFSTSB=null,PIC_QOUTESCDSB=null,PIC_QOUTELSTSB=null where PIC_ID='" + ptcode + "'";
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
                    string sqltext3 = "UPDATE TBMP_IQRCMPPRICE SET PIC_SUPPLIERCID='" + suppcid + "'";
                    if (fstsa != "")
                    {
                        sqltext3 = sqltext3 + ",PIC_QOUTEFSTSC='" + fstsa + "' ";
                    }
                    else if (fstsa == "")
                    {
                        sqltext3 = sqltext3 + ",PIC_QOUTEFSTSC=null ";
                    }
                    //if (secsa != 0)
                    //{
                    //    sqltext3 = sqltext3 + ",PIC_QOUTESCDSC=" + secsa + " ";
                    //}
                    //else if (secsa == 0)
                    //{
                    //    sqltext3 = sqltext3 + ",PIC_QOUTESCDSC=null ";
                    //}
                    //if (lassa != 0)
                    //{
                    //    sqltext3 = sqltext3 + ",PIC_QOUTELSTSC=" + lassa + " ";
                    //}
                    //else if (lassa == 0)
                    //{
                    //    sqltext3 = sqltext3 + ",PIC_QOUTELSTSC=null ";
                    //}
                    sqltext3 = sqltext3 + ",PIC_QOUTESCDSC=" + secsa + " ";
                    sqltext3 = sqltext3 + ",PIC_QOUTELSTSC=" + lassa + " ";
                    sqltext3 = sqltext3 + "  WHERE PIC_ID='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sqltext3);
                    sqltext3 = "update TBMP_IQRCMPPRICE set PIC_SUPPLIERCID='" + suppcid + "' where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext3);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PIC_SUPPLIERCID.Text)
                    {
                        sqltext = "update TBMP_IQRCMPPRICE set PIC_SUPPLYTIME='" + ((TextBox)Retem.FindControl("TextBox31")).Text.ToString() + "' , PIC_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox32")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox32")).Text.ToString()) + "' " +
                                  "where PIC_ID='" + ((Label)Retem.FindControl("PIC_PCODE")).Text + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    string sql3 = "UPDATE TBMP_IQRCMPPRICE SET PIC_SUPPLIERCID=null,PIC_QOUTEFSTSC=null,PIC_QOUTESCDSC=null,PIC_QOUTELSTSC=null where PIC_ID='" + ptcode + "'";
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
                    string sqltext4 = "UPDATE TBMP_IQRCMPPRICE SET PIC_SUPPLIERDID='" + suppdid + "'";
                    if (fstsa != "")
                    {
                        sqltext4 = sqltext4 + ",PIC_QOUTEFSTSD='" + fstsa + "' ";
                    }
                    else
                    {
                        sqltext4 = sqltext4 + ",PIC_QOUTEFSTSD=null ";
                    }
                    //if (secsa != 0)
                    //{
                    //    sqltext4 = sqltext4 + ",PIC_QOUTESCDSD=" + secsa + " ";
                    //}
                    //else
                    //{
                    //    sqltext4 = sqltext4 + ",PIC_QOUTESCDSD=null";
                    //}
                    //if (lassa != 0)
                    //{
                    //    sqltext4 = sqltext4 + ",PIC_QOUTELSTSD=" + lassa + " ";
                    //}
                    //else
                    //{
                    //    sqltext4 = sqltext4 + ",PIC_QOUTELSTSD=null ";
                    //}
                    sqltext4 = sqltext4 + ",PIC_QOUTESCDSD=" + secsa + " ";
                    sqltext4 = sqltext4 + ",PIC_QOUTELSTSD=" + lassa + " ";
                    sqltext4 = sqltext4 + "  WHERE PIC_ID='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sqltext4);
                    sqltext4 = "update TBMP_IQRCMPPRICE set PIC_SUPPLIERDID='" + suppdid + "' where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext4);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PIC_SUPPLIERDID.Text)
                    {
                        sqltext = "update TBMP_IQRCMPPRICE set PIC_SUPPLYTIME='" + ((TextBox)Retem.FindControl("TextBox41")).Text.ToString() + "' , PIC_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox42")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox42")).Text.ToString()) + "' " +
                                  "where PIC_ID='" + ((Label)Retem.FindControl("PIC_PCODE")).Text + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    string sql4 = "UPDATE TBMP_IQRCMPPRICE SET PIC_SUPPLIERDID=null,PIC_QOUTEFSTSD=null,PIC_QOUTESCDSD=null,PIC_QOUTELSTSD=null where PIC_ID='" + ptcode + "'";
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
                    string sqltext5 = "UPDATE TBMP_IQRCMPPRICE SET PIC_SUPPLIEREID='" + suppeid + "'";
                    if (fstsa != "")
                    {
                        sqltext5 = sqltext5 + ",PIC_QOUTEFSTSE='" + fstsa + "' ";
                    }
                    else
                    {
                        sqltext5 = sqltext5 + ",PIC_QOUTEFSTSE=null ";
                    }
                    //if (secsa != 0)
                    //{
                    //    sqltext5 = sqltext5 + ",PIC_QOUTESCDSE=" + secsa + " ";
                    //}
                    //else
                    //{
                    //    sqltext5 = sqltext5 + ",PIC_QOUTESCDSE=null ";
                    //}
                    //if (lassa != 0)
                    //{
                    //    sqltext5 = sqltext5 + ",PIC_QOUTELSTSE=" + lassa + " ";
                    //}
                    //else
                    //{
                    //    sqltext5 = sqltext5 + ",PIC_QOUTELSTSE=null ";
                    //}
                    sqltext5 = sqltext5 + ",PIC_QOUTESCDSE=" + secsa + " ";
                    sqltext5 = sqltext5 + ",PIC_QOUTELSTSE=" + lassa + " ";
                    sqltext5 = sqltext5 + "  WHERE PIC_ID='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sqltext5);
                    sqltext5 = "update TBMP_IQRCMPPRICE set PIC_SUPPLIEREID='" + suppeid + "' where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext5);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PIC_SUPPLIEREID.Text)
                    {
                        sqltext = "update TBMP_IQRCMPPRICE set PIC_SUPPLYTIME='" + ((TextBox)Retem.FindControl("TextBox51")).Text.ToString() + "' , PIC_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox53")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox52")).Text.ToString()) + "' " +
                                  "where PIC_ID='" + ((Label)Retem.FindControl("PIC_PCODE")).Text + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    string sql5 = "UPDATE TBMP_IQRCMPPRICE SET PIC_SUPPLIEREID=null,PIC_QOUTEFSTSE=null,PIC_QOUTESCDSE=null,PIC_QOUTELSTSE=null where PIC_ID='" + ptcode + "'";
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
                    string sqltext6 = "UPDATE TBMP_IQRCMPPRICE SET PIC_SUPPLIERFID='" + suppfid + "'";
                    if (fstsa != "")
                    {
                        sqltext6 = sqltext6 + ",PIC_QOUTEFSTSF='" + fstsa + "' ";
                    }
                    else
                    {
                        sqltext6 = sqltext6 + ",PIC_QOUTEFSTSF=null ";
                    }
                    //if (secsa != 0)
                    //{
                    //    sqltext6 = sqltext6 + ",PIC_QOUTESCDSF=" + secsa + " ";
                    //}
                    //else
                    //{
                    //    sqltext6 = sqltext6 + ",PIC_QOUTESCDSF=null ";
                    //}
                    //if (lassa != 0)
                    //{
                    //    sqltext6 = sqltext6 + ",PIC_QOUTELSTSF=" + lassa + " ";
                    //}
                    //else
                    //{
                    //    sqltext6 = sqltext6 + ",PIC_QOUTELSTSF=null ";
                    //}
                    sqltext6 = sqltext6 + ",PIC_QOUTESCDSF=" + secsa + " ";
                    sqltext6 = sqltext6 + ",PIC_QOUTELSTSF=" + lassa + " ";
                    sqltext6 = sqltext6 + "  WHERE PIC_ID='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sqltext6);
                    sqltext6 = "update TBMP_IQRCMPPRICE set PIC_SUPPLIERFID='" + suppfid + "' where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext6);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PIC_SUPPLIERFID.Text)
                    {
                        sqltext = "update TBMP_IQRCMPPRICE set PIC_SUPPLYTIME='" + ((TextBox)Retem.FindControl("TextBox61")).Text.ToString() + "' , PIC_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox62")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox62")).Text.ToString()) + "' " +
                                  "where PIC_ID='" + ((Label)Retem.FindControl("PIC_PCODE")).Text + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();
                    string sql5 = "UPDATE TBMP_IQRCMPPRICE SET PIC_SUPPLIERFID=null,PIC_QOUTEFSTSF=null,PIC_QOUTESCDSF=null,PIC_QOUTELSTSF=null where PIC_ID='" + ptcode + "'";
                    DBCallCommon.ExeSqlText(sql5);
                }
            }
            #endregion
            string sqltext7 = "UPDATE TBMP_IQRCMPPRCRVW SET ICL_ZHUGUANID='" + ddl_zgjl.SelectedValue.ToString()+
                              "',ICL_FUZRID='" + ddl_fuziren.SelectedValue.ToString() + "' where ICL_SHEETNO='" + TextBoxNO.Text + "'";
            DBCallCommon.ExeSqlText(sqltext7);
            foreach (RepeaterItem Retem in comparepriceRepeater.Items)
            {
                ptcode = ((Label)Retem.FindControl("PIC_PCODE")).Text.ToString();

                string sqltext8 = "update TBMP_IQRCMPPRICE set PTC=PIC_ENGID+'-'+PIC_TUHAO+'('+PIC_ZONGXU+')'+PIC_MNAME+'-'+cast(PIC_ID as varchar) where PIC_ID='" + ptcode + "'";
                DBCallCommon.ExeSqlText(sqltext8);
            }
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
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                ((TextBox)e.Item.FindControl("TextBox11")).Attributes.Add("onblur", "drapDown(this,'.rq1')");
                ((TextBox)e.Item.FindControl("TextBox21")).Attributes.Add("onblur", "drapDown(this,'.rq2')");
                ((TextBox)e.Item.FindControl("TextBox31")).Attributes.Add("onblur", "drapDown(this,'.rq3')");
                ((TextBox)e.Item.FindControl("TextBox41")).Attributes.Add("onblur", "drapDown(this,'.rq4')");
                ((TextBox)e.Item.FindControl("TextBox51")).Attributes.Add("onblur", "drapDown(this,'.rq5')");
                ((TextBox)e.Item.FindControl("TextBox61")).Attributes.Add("onblur", "drapDown(this,'.rq6')");
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
            Response.Redirect("PM_Xie_Mana_List.aspx");
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
                sqlt = "select PIC_PTCODE,PIC_QOUTELSTSA from TBMP_IQRCMPPRICE where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PIC_PTCODE"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PIC_QOUTELSTSA"].ToString() == "" ? "0" : dt.Rows[k]["PIC_QOUTELSTSA"].ToString());
                        sqlt = "update TBMP_IQRCMPPRICE  set PIC_PRICE=" + price + " where PIC_PTCODE='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBMP_IQRCMPPRICE set PIC_SUPPLIERRESID='" + providerid + "' where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DBCallCommon.ExeSqlText(sqlt);
            }
            else if (minje == ZJE2)
            {
                providerid = PIC_SUPPLIERBID.Text;
                sqlt = "select PIC_PTCODE,PIC_QOUTELSTSB from TBMP_IQRCMPPRICE where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PIC_PTCODE"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PIC_QOUTELSTSB"].ToString() == "" ? "0" : dt.Rows[k]["PIC_QOUTELSTSB"].ToString());
                        sqlt = "update TBMP_IQRCMPPRICE  set PIC_PRICE=" + price + " where PIC_PTCODE='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBMP_IQRCMPPRICE set PIC_SUPPLIERRESID='" + providerid + "'  where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DBCallCommon.ExeSqlText(sqlt);
            }
            else if (minje == ZJE3)
            {
                providerid = PIC_SUPPLIERCID.Text;
                sqlt = "select PIC_PTCODE,PIC_QOUTELSTSC from TBMP_IQRCMPPRICE where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PIC_PTCODE"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PIC_QOUTELSTSC"].ToString() == "" ? "0" : dt.Rows[k]["PIC_QOUTELSTSC"].ToString());
                        sqlt = "update TBMP_IQRCMPPRICE  set PIC_PRICE=" + price + " where PIC_PTCODE='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBMP_IQRCMPPRICE set PIC_SUPPLIERRESID='" + providerid + "' where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DBCallCommon.ExeSqlText(sqlt);
            }
            else if (minje == ZJE4)
            {
                providerid = PIC_SUPPLIERDID.Text;
                sqlt = "select PIC_PTCODE,PIC_QOUTELSTSD from TBMP_IQRCMPPRICE where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PIC_PTCODE"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PIC_QOUTELSTSD"].ToString() == "" ? "0" : dt.Rows[k]["PIC_QOUTELSTSD"].ToString());
                        sqlt = "update TBMP_IQRCMPPRICE  set PIC_PRICE=" + price + " where PIC_PTCODE='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBMP_IQRCMPPRICE set PIC_SUPPLIERRESID='" + providerid + "'  where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DBCallCommon.ExeSqlText(sqlt);
            }
            else if (minje == ZJE5)
            {
                providerid = PIC_SUPPLIEREID.Text;
                sqlt = "select PIC_PTCODE,PIC_QOUTELSTSE from TBMP_IQRCMPPRICE where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PIC_PTCODE"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PIC_QOUTELSTSE"].ToString() == "" ? "0" : dt.Rows[k]["PIC_QOUTELSTSE"].ToString());
                        sqlt = "update TBMP_IQRCMPPRICE  set PIC_PRICE=" + price + " where PIC_PTCODE='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBMP_IQRCMPPRICE set PIC_SUPPLIERRESID='" + providerid + "'  where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DBCallCommon.ExeSqlText(sqlt);
            }
            else if (minje == ZJE6)
            {
                providerid = PIC_SUPPLIERFID.Text;
                sqlt = "select PIC_PTCODE,PIC_QOUTELSTSE from TBMP_IQRCMPPRICE where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PIC_PTCODE"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PIC_QOUTELSTSF"].ToString() == "" ? "0" : dt.Rows[k]["PIC_QOUTELSTSF"].ToString());
                        sqlt = "update TBMP_IQRCMPPRICE  set PIC_PRICE=" + price + " where PIC_PTCODE='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBMP_IQRCMPPRICE set PIC_SUPPLIERRESID='" + providerid + "'  where PIC_SHEETNO='" + TextBoxNO.Text + "'";
                DBCallCommon.ExeSqlText(sqlt);
            }
            comparepriceRepeaterdatabind();
            savedate();
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
        //重新绑定数据
        private void RebindGrid()
        {
            Initpage();
            comparepriceRepeaterdatabind();
        }
       
    }
}
