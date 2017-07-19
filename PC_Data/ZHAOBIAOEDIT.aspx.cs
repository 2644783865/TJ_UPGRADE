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
    public partial class ZHAOBIAOEDIT : System.Web.UI.Page
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
                TextBoxNO.Text = gloabsheetno;
                comparepriceRepeaterdatabind();
            }

            string userid = "";
            if (Session["supplyid"] != null && Session["supplyid"].ToString().Trim() != "")
            {
                userid = Session["supplyid"].ToString().Trim();
                string sqlgetpower = "select * from PC_POWERINFO where powersupplyid='" + userid + "' and poweriqcode='" + Request.QueryString["sheetno"].ToString().Trim() + "' and poweriqcode in(select ICL_SHEETNO from TBPC_IQRCMPPRCRVW where ICL_STATE='0')";
                System.Data.DataTable dtgetpower = DBCallCommon.GetDTUsingSqlText(sqlgetpower);
                if (dtgetpower.Rows.Count == 0)
                {
                    Response.Write("<script>alert('没有该单号的权限！！！');if(window.parent!=null)window.parent.location.href='http://111.160.8.74:888/PC_Data/PC_ZHAOBIAOLOGIN.aspx';else{window.location.href='http://111.160.8.74:888/PC_Data/PC_ZHAOBIAOLOGIN.aspx'} </script>");
                }
            }
            else
            {
                Response.Write("<script>alert('会话已过期,请重新登录！！！');if(window.parent!=null)window.parent.location.href='http://111.160.8.74:888/PC_Data/PC_ZHAOBIAOLOGIN.aspx';else{window.location.href='http://111.160.8.74:888/PC_Data/PC_ZHAOBIAOLOGIN.aspx'} </script>");
            }


            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang1"))).Text = PIC_SUPPLIERANAME.Text;
            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang2"))).Text = PIC_SUPPLIERBNAME.Text;
            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang3"))).Text = PIC_SUPPLIERCNAME.Text;
            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang4"))).Text = PIC_SUPPLIERDNAME.Text;
            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang5"))).Text = PIC_SUPPLIERENAME.Text;
            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang6"))).Text = PIC_SUPPLIERFNAME.Text;
        }

        private void comparepriceRepeaterdatabind()
        {
            getArticle();
            HeaderTemplatebind();
            controlvisible();
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
                          "(SELECT min(PRICE) FROM View_TBPC_IQRCMPPRICE_RVW GROUP BY MARID,totalstate having View_TBPC_IQRCMPPRICE.marid=MARID AND totalstate='4') AS minprice, " +
                          "(SELECT  top(1) PRICE  FROM View_TBPC_IQRCMPPRICE_RVW  GROUP BY MARID,PRICE,IRQDATA,totalstate  having View_TBPC_IQRCMPPRICE.marid=MARID AND totalstate='4' and price is not null order by IRQDATA DESC ) as lastprice, " +
                         "isnull(detailcstate,0) as detailcstate, detailnote,PIC_MAP,ST_SQR,PIC_CHILDENGNAME FROM View_TBPC_IQRCMPPRICE  where picno='" + TextBoxNO.Text.ToString() + "' order by ptcode ASC";

            }
            else//导入数据之后
            {
                sqltext = "SELECT picno, pjid, pjnm, engid, engnm, marid,PIC_MASHAPE, marnm, margg, margb, marcz, marunit, " +
                          "marfzunit, length, width, marnum, marfznum, marzxnum, marzxfznum, " +
                         "supplierresid, supplierresnm1, supplierresrank, shuilv, price, detamount, a.ptcode,b.jh1 as qoutefstsa, " +
                         "b.dj1 as qoutescdsa,b.je1 as qoutelstsa,b.jh2 as qoutefstsb,b.dj2 as qoutescdsb,b.je2 qoutelstsb,b.jh3 as qoutefstsc,b.dj3 as qoutescdsc, " +
                         "b.je3 as qoutelstsc,b.jh4 as qoutefstsd,b.dj4 as qoutescdsd,b.je4 as qoutelstsd,b.jh5 as qoutefstse,b.dj5 as qoutescdse,b.je5 as qoutelstse, " +
                         "b.jh6 as qoutefstsf,b.dj6 as qoutescdsf,b.je5 as qoutelstsf, pmode, keycoms, zdjprice, zdjnum, isnull(detailstate,0) as detailstate,PIC_TUHAO, " +
                          "(SELECT min(PRICE) FROM View_TBPC_IQRCMPPRICE_RVW GROUP BY MARID,totalstate having a.marid=MARID AND totalstate='4') AS minprice, " +
                          "(SELECT  top(1) PRICE  FROM View_TBPC_IQRCMPPRICE_RVW  GROUP BY MARID,PRICE,IRQDATA,totalstate  having a.marid=MARID AND totalstate='4' and price is not null order by IRQDATA DESC ) as lastprice, " +
                         "isnull(detailcstate,0) as detailcstate, detailnote,PIC_MAP,ST_SQR,PIC_CHILDENGNAME FROM View_TBPC_IQRCMPPRICE as a left join TBPC_BIJIADAN as b on a.ptcode=b.ptcode where picno='" + TextBoxNO.Text.ToString() + "' order by ptcode ASC";

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
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);
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
            double zxnum = 0;
            double zxfznum = 0;
            double shuilv = 0;
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
        }


        protected void comparepriceRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
            }
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (((Label)e.Item.FindControl("PIC_ZXNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("PIC_ZXNUM")).Text.ToString() == System.String.Empty)
                {
                    ((Label)e.Item.FindControl("PIC_ZXNUM")).Text = "0";
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



        private void controlvisible()
        {
            string userid = "";
            if (Session["supplyid"] != null && Session["supplyid"].ToString().Trim() != "")
            {
                userid = Session["supplyid"].ToString().Trim();
            }
            else
            {
                Response.Write("<script>alert('会话已过期,请重新登录！！！');if(window.parent!=null)window.parent.location.href='http://111.160.8.74:888/PC_Data/PC_ZHAOBIAOLOGIN.aspx';else{window.location.href='http://111.160.8.74:888/PC_Data/PC_ZHAOBIAOLOGIN.aspx'} </script>");
            }

            HtmlTableCell gys1 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("gys1");
            HtmlTableCell gys2 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("gys2");
            HtmlTableCell gys3 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("gys3");
            HtmlTableCell gys4 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("gys4");
            HtmlTableCell gys5 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("gys5");
            HtmlTableCell gys6 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("gys6");

            HtmlTableCell dyc1 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dyc1");
            HtmlTableCell dec1 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dec1");
            HtmlTableCell dsc1 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dsc1");
            HtmlTableCell dyc2 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dyc2");
            HtmlTableCell dec2 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dec2");
            HtmlTableCell dsc2 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dsc2");
            HtmlTableCell dyc3 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dyc3");
            HtmlTableCell dec3 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dec3");
            HtmlTableCell dsc3 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dsc3");
            HtmlTableCell dyc4 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dyc4");
            HtmlTableCell dec4 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dec4");
            HtmlTableCell dsc4 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dsc4");
            HtmlTableCell dyc5 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dyc5");
            HtmlTableCell dec5 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dec5");
            HtmlTableCell dsc5 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dsc5");
            HtmlTableCell dyc6 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dyc6");
            HtmlTableCell dec6 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dec6");
            HtmlTableCell dsc6 = (HtmlTableCell)comparepriceRepeater.Controls[0].FindControl("dsc6");

            if (PIC_SUPPLIERAID.Text.Trim() == userid)
            {
                gys1.Visible = true;
                gys2.Visible = false;
                gys3.Visible = false;
                gys4.Visible = false;
                gys5.Visible = false;
                gys6.Visible = false;

                dyc1.Visible = true;
                dec1.Visible = true;
                dsc1.Visible = true;
                dyc2.Visible = false;
                dec2.Visible = false;
                dsc2.Visible = false;
                dyc3.Visible = false;
                dec3.Visible = false;
                dsc3.Visible = false;
                dyc4.Visible = false;
                dec4.Visible = false;
                dsc4.Visible = false;
                dyc5.Visible = false;
                dec5.Visible = false;
                dsc5.Visible = false;
                dyc6.Visible = false;
                dec6.Visible = false;
                dsc6.Visible = false;
            }
            if (PIC_SUPPLIERBID.Text.Trim() == userid)
            {
                gys1.Visible = false;
                gys2.Visible = true;
                gys3.Visible = false;
                gys4.Visible = false;
                gys5.Visible = false;
                gys6.Visible = false;

                dyc1.Visible = false;
                dec1.Visible = false;
                dsc1.Visible = false;
                dyc2.Visible = true;
                dec2.Visible = true;
                dsc2.Visible = true;
                dyc3.Visible = false;
                dec3.Visible = false;
                dsc3.Visible = false;
                dyc4.Visible = false;
                dec4.Visible = false;
                dsc4.Visible = false;
                dyc5.Visible = false;
                dec5.Visible = false;
                dsc5.Visible = false;
                dyc6.Visible = false;
                dec6.Visible = false;
                dsc6.Visible = false;
            }
            if (PIC_SUPPLIERCID.Text.Trim() == userid)
            {
                gys1.Visible = false;
                gys2.Visible = false;
                gys3.Visible = true;
                gys4.Visible = false;
                gys5.Visible = false;
                gys6.Visible = false;

                dyc1.Visible = false;
                dec1.Visible = false;
                dsc1.Visible = false;
                dyc2.Visible = false;
                dec2.Visible = false;
                dsc2.Visible = false;
                dyc3.Visible = true;
                dec3.Visible = true;
                dsc3.Visible = true;
                dyc4.Visible = false;
                dec4.Visible = false;
                dsc4.Visible = false;
                dyc5.Visible = false;
                dec5.Visible = false;
                dsc5.Visible = false;
                dyc6.Visible = false;
                dec6.Visible = false;
                dsc6.Visible = false;
            }
            if (PIC_SUPPLIERDID.Text.Trim() == userid)
            {
                gys1.Visible = false;
                gys2.Visible = false;
                gys3.Visible = false;
                gys4.Visible = true;
                gys5.Visible = false;
                gys6.Visible = false;

                dyc1.Visible = false;
                dec1.Visible = false;
                dsc1.Visible = false;
                dyc2.Visible = false;
                dec2.Visible = false;
                dsc2.Visible = false;
                dyc3.Visible = false;
                dec3.Visible = false;
                dsc3.Visible = false;
                dyc4.Visible = true;
                dec4.Visible = true;
                dsc4.Visible = true;
                dyc5.Visible = false;
                dec5.Visible = false;
                dsc5.Visible = false;
                dyc6.Visible = false;
                dec6.Visible = false;
                dsc6.Visible = false;
            }
            if (PIC_SUPPLIEREID.Text.Trim() == userid)
            {
                gys1.Visible = false;
                gys2.Visible = false;
                gys3.Visible = false;
                gys4.Visible = false;
                gys5.Visible = true;
                gys6.Visible = false;

                dyc1.Visible = false;
                dec1.Visible = false;
                dsc1.Visible = false;
                dyc2.Visible = false;
                dec2.Visible = false;
                dsc2.Visible = false;
                dyc3.Visible = false;
                dec3.Visible = false;
                dsc3.Visible = false;
                dyc4.Visible = false;
                dec4.Visible = false;
                dsc4.Visible = false;
                dyc5.Visible = true;
                dec5.Visible = true;
                dsc5.Visible = true;
                dyc6.Visible = false;
                dec6.Visible = false;
                dsc6.Visible = false;
            }
            if (PIC_SUPPLIERFID.Text.Trim() == userid)
            {
                gys1.Visible = false;
                gys2.Visible = false;
                gys3.Visible = false;
                gys4.Visible = false;
                gys5.Visible = false;
                gys6.Visible = true;

                dyc1.Visible = false;
                dec1.Visible = false;
                dsc1.Visible = false;
                dyc2.Visible = false;
                dec2.Visible = false;
                dsc2.Visible = false;
                dyc3.Visible = false;
                dec3.Visible = false;
                dsc3.Visible = false;
                dyc4.Visible = false;
                dec4.Visible = false;
                dsc4.Visible = false;
                dyc5.Visible = false;
                dec5.Visible = false;
                dsc5.Visible = false;
                dyc6.Visible = true;
                dec6.Visible = true;
                dsc6.Visible = true;
            }


            foreach (RepeaterItem item in comparepriceRepeater.Items)
            {
                HtmlTableCell jg11 = (HtmlTableCell)item.FindControl("jg11");
                HtmlTableCell jg12 = (HtmlTableCell)item.FindControl("jg12");
                HtmlTableCell jg13 = (HtmlTableCell)item.FindControl("jg13");

                HtmlTableCell jg21 = (HtmlTableCell)item.FindControl("jg21");
                HtmlTableCell jg22 = (HtmlTableCell)item.FindControl("jg22");
                HtmlTableCell jg23 = (HtmlTableCell)item.FindControl("jg23");

                HtmlTableCell jg31 = (HtmlTableCell)item.FindControl("jg31");
                HtmlTableCell jg32 = (HtmlTableCell)item.FindControl("jg32");
                HtmlTableCell jg33 = (HtmlTableCell)item.FindControl("jg33");

                HtmlTableCell jg41 = (HtmlTableCell)item.FindControl("jg41");
                HtmlTableCell jg42 = (HtmlTableCell)item.FindControl("jg42");
                HtmlTableCell jg43 = (HtmlTableCell)item.FindControl("jg43");

                HtmlTableCell jg51 = (HtmlTableCell)item.FindControl("jg51");
                HtmlTableCell jg52 = (HtmlTableCell)item.FindControl("jg52");
                HtmlTableCell jg53 = (HtmlTableCell)item.FindControl("jg53");

                HtmlTableCell jg61 = (HtmlTableCell)item.FindControl("jg61");
                HtmlTableCell jg62 = (HtmlTableCell)item.FindControl("jg62");
                HtmlTableCell jg63 = (HtmlTableCell)item.FindControl("jg63");
                if (PIC_SUPPLIERAID.Text.Trim() == userid)
                {
                    jg11.Visible = true;
                    jg12.Visible = true;
                    jg13.Visible = true;
                    jg21.Visible = false;
                    jg22.Visible = false;
                    jg23.Visible = false;
                    jg31.Visible = false;
                    jg32.Visible = false;
                    jg33.Visible = false;
                    jg41.Visible = false;
                    jg42.Visible = false;
                    jg43.Visible = false;
                    jg51.Visible = false;
                    jg52.Visible = false;
                    jg53.Visible = false;
                    jg61.Visible = false;
                    jg62.Visible = false;
                    jg63.Visible = false;
                }

                if (PIC_SUPPLIERBID.Text.Trim() == userid)
                {
                    jg11.Visible = false;
                    jg12.Visible = false;
                    jg13.Visible = false;
                    jg21.Visible = true;
                    jg22.Visible = true;
                    jg23.Visible = true;
                    jg31.Visible = false;
                    jg32.Visible = false;
                    jg33.Visible = false;
                    jg41.Visible = false;
                    jg42.Visible = false;
                    jg43.Visible = false;
                    jg51.Visible = false;
                    jg52.Visible = false;
                    jg53.Visible = false;
                    jg61.Visible = false;
                    jg62.Visible = false;
                    jg63.Visible = false;
                }
                if (PIC_SUPPLIERCID.Text.Trim() == userid)
                {
                    jg11.Visible = false;
                    jg12.Visible = false;
                    jg13.Visible = false;
                    jg21.Visible = false;
                    jg22.Visible = false;
                    jg23.Visible = false;
                    jg31.Visible = true;
                    jg32.Visible = true;
                    jg33.Visible = true;
                    jg41.Visible = false;
                    jg42.Visible = false;
                    jg43.Visible = false;
                    jg51.Visible = false;
                    jg52.Visible = false;
                    jg53.Visible = false;
                    jg61.Visible = false;
                    jg62.Visible = false;
                    jg63.Visible = false;
                }
                if (PIC_SUPPLIERDID.Text.Trim() == userid)
                {
                    jg11.Visible = false;
                    jg12.Visible = false;
                    jg13.Visible = false;
                    jg21.Visible = false;
                    jg22.Visible = false;
                    jg23.Visible = false;
                    jg31.Visible = false;
                    jg32.Visible = false;
                    jg33.Visible = false;
                    jg41.Visible = true;
                    jg42.Visible = true;
                    jg43.Visible = true;
                    jg51.Visible = false;
                    jg52.Visible = false;
                    jg53.Visible = false;
                    jg61.Visible = false;
                    jg62.Visible = false;
                    jg63.Visible = false;
                }
                if (PIC_SUPPLIEREID.Text.Trim() == userid)
                {
                    jg11.Visible = false;
                    jg12.Visible = false;
                    jg13.Visible = false;
                    jg21.Visible = false;
                    jg22.Visible = false;
                    jg23.Visible = false;
                    jg31.Visible = false;
                    jg32.Visible = false;
                    jg33.Visible = false;
                    jg41.Visible = false;
                    jg42.Visible = false;
                    jg43.Visible = false;
                    jg51.Visible = true;
                    jg52.Visible = true;
                    jg53.Visible = true;
                    jg61.Visible = false;
                    jg62.Visible = false;
                    jg63.Visible = false;
                }
                if (PIC_SUPPLIERFID.Text.Trim() == userid)
                {
                    jg11.Visible = false;
                    jg12.Visible = false;
                    jg13.Visible = false;
                    jg21.Visible = false;
                    jg22.Visible = false;
                    jg23.Visible = false;
                    jg31.Visible = false;
                    jg32.Visible = false;
                    jg33.Visible = false;
                    jg41.Visible = false;
                    jg42.Visible = false;
                    jg43.Visible = false;
                    jg51.Visible = false;
                    jg52.Visible = false;
                    jg53.Visible = false;
                    jg61.Visible = true;
                    jg62.Visible = true;
                    jg63.Visible = true;
                }
            }
        }
    }
}
