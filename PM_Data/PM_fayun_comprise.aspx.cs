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
using System.Collections.Generic;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_fayun_comprise : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            btn_cancel.Attributes.Add("OnClick", "Javascript:return confirm('确定取消？执行此操作将停止发运询比价！');");
            btn_cancel.Click += new EventHandler(btn_cancel_Click);
            btn_autobj.Attributes.Add("OnClick", "Javascript:return confirm('税率是否需要修改？若税率不是10%，请修改税率！');");
            btn_BZJ.Attributes.Add("OnClick", "Javascript:return confirm('税率是否需要修改？若税率不是10%，请修改税率！');");
            btn_confirm.Attributes.Add("OnClick", "Javascript:return confirm('税率是否需要修改？若税率不是10%，请修改税率！');");
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
                //Hyp_print.NavigateUrl = "TBPC_IQRCMPPRC_detailprint.aspx?sheetno=" + gloabsheetno;
                TextBoxNO.Text = gloabsheetno;
                Initpage();
                comparepriceRepeaterdatabind();
            }
            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang1"))).Text = PM_SUPPLIERANAME.Text;
            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang2"))).Text = PM_SUPPLIERBNAME.Text;
            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang3"))).Text = PM_SUPPLIERCNAME.Text;
            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang4"))).Text = PM_SUPPLIERDNAME.Text;
            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang5"))).Text = PM_SUPPLIERENAME.Text;
            ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang6"))).Text = PM_SUPPLIERFNAME.Text;
        }

        private void Initpage()
        {
            int num = 0;
            string sqltext = "";
            //负责人下拉框绑定
            //主管下拉框绑定
            sqltext = "select ST_NAME,ST_ID from TBDS_STAFFINFO WHERE ST_DEPID='" + Session["UserDeptID"].ToString() + "' and ST_PD=0";
            DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqltext);
            cob_fuziren.DataSource = dt0;
            cob_fuziren.DataTextField = "ST_NAME";
            cob_fuziren.DataValueField = "ST_ID";
            cob_fuziren.DataBind();
            //2016.8.2修改
            string sqltxt_bumenfuze = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_POSITION='0701'and ST_PD='0'";
            DataTable dt_bumenfuze = DBCallCommon.GetDTUsingSqlText(sqltxt_bumenfuze);
            cob_fuziren.SelectedValue = dt_bumenfuze.Rows[0]["ST_ID"].ToString();
            //cob_fuziren.SelectedValue = "95";
            sqltext = "select ST_NAME,ST_ID from TBDS_STAFFINFO WHERE ST_POSITION='0101' and ST_PD='0'";
            dt0 = DBCallCommon.GetDTUsingSqlText(sqltext);
            cob_zgjl.DataSource = dt0;
            cob_zgjl.DataTextField = "ST_NAME";
            cob_zgjl.DataValueField = "ST_ID";
            cob_zgjl.DataBind();
            sqltext = "select ICL_REVIEWA,zdrnm  as ICL_REVIEWANM,ICL_REVIEWATIME,ICL_REVIEWAADVC," +
                    "ICL_REVIEWB,shbnm as ICL_REVIEWBNM,ICL_REVIEWBTIME,ICL_REVIEWBADVC," +
                    "ICL_REVIEWC,shcnm as ICL_REVIEWCNM,ICL_REVIEWCTIME,ICL_REVIEWCADVC," +
                    "ICL_REVIEWD,shdnm as ICL_REVIEWDNM,ICL_REVIEWDTIME,ICL_REVIEWDADVC," +
                    "ICL_REVIEWE,shenm as ICL_REVIEWENM,ICL_REVIEWETIME,ICL_REVIEWEADVC," +
                    "ICL_REVIEWF,shfnm as ICL_REVIEWFNM,ICL_REVIEWFTIME,ICL_REVIEWFADVC," +
                    "ICL_REVIEWG,shgnm as ICL_REVIEWGNM,ICL_REVIEWGTIME,ICL_REVIEWGADVC," +
                    "ICL_FUZRID,shgnm as ICL_FUZRNAME,ICL_ZHUGUANID,shinm as ICL_ZHUGUANNAME," +
                    "isnull(ICL_STATE,0) as ICL_STATE,isnull(ICL_STATEA,0) as ICL_STATEA,isnull(ICL_STATEB,0) as ICL_STATEB,isnull(ICL_STATEC,0) as ICL_STATEC,isnull(ICL_STATED,0) as ICL_STATED " +
                    "from View_TBMP_FAYUNPRCRVW  where picno='" + TextBoxNO.Text.ToString() + "'";

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
            }

        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void comparepriceRepeaterdatabind()
        {
            //ItemTemplatedind();
            getArticle();
            HeaderTemplatebind();
        }

        private void HeaderTemplatebind()
        {
            string sqltext = "";
            sqltext = "SELECT distinct  PM_SUPPLIERAID as aid, supplieranm as anm, supplierarank as arnk, " +
                     "PM_SUPPLIERBID as bid, supplierbnm as bnm, supplierbrank  as brnk, PM_SUPPLIERCID as cid, " +
                     "suppliercnm as cnm, suppliercrank as crnk, PM_SUPPLIERDID as did, supplierdnm as dnm, " +
                     "supplierdrank as drnk, PM_SUPPLIEREID as eid, supplierenm as enm, suppliererank as ernk, " +
                     "PM_SUPPLIERFID as fid, supplierfnm as fnm,supplierfrank as frnk  " +
                     "FROM View_TBMP_FAYUNPRICE where PM_SHEETNO='" + TextBoxNO.Text.ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                PM_SUPPLIERANAME.Text = dt.Rows[0]["anm"].ToString();
                PM_SUPPLIERBNAME.Text = dt.Rows[0]["bnm"].ToString();
                PM_SUPPLIERCNAME.Text = dt.Rows[0]["cnm"].ToString();
                PM_SUPPLIERDNAME.Text = dt.Rows[0]["dnm"].ToString();
                PM_SUPPLIERENAME.Text = dt.Rows[0]["enm"].ToString();
                PM_SUPPLIERFNAME.Text = dt.Rows[0]["fnm"].ToString();
                ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang1"))).Text = dt.Rows[0]["anm"].ToString();
                ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang2"))).Text = dt.Rows[0]["bnm"].ToString();
                ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang3"))).Text = dt.Rows[0]["cnm"].ToString();
                ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang4"))).Text = dt.Rows[0]["dnm"].ToString();
                ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang5"))).Text = dt.Rows[0]["enm"].ToString();
                ((Label)(comparepriceRepeater.Controls[0].FindControl("GYShang6"))).Text = dt.Rows[0]["fnm"].ToString();

                PM_SUPPLIERAID.Text = dt.Rows[0]["aid"].ToString();
                PM_SUPPLIERBID.Text = dt.Rows[0]["bid"].ToString();
                PM_SUPPLIERCID.Text = dt.Rows[0]["cid"].ToString();
                PM_SUPPLIERDID.Text = dt.Rows[0]["did"].ToString();
                PM_SUPPLIEREID.Text = dt.Rows[0]["eid"].ToString();
                PM_SUPPLIERFID.Text = dt.Rows[0]["fid"].ToString();

                LbA_lei.Text = dt.Rows[0]["arnk"].ToString();
                LbB_lei.Text = dt.Rows[0]["brnk"].ToString();
                LbC_lei.Text = dt.Rows[0]["crnk"].ToString();
                LbD_lei.Text = dt.Rows[0]["drnk"].ToString();
                LbE_lei.Text = dt.Rows[0]["ernk"].ToString();
                LbF_lei.Text = dt.Rows[0]["frnk"].ToString();

                PM_SUPPLIERA.Text = dt.Rows[0]["aid"].ToString() + "|" + dt.Rows[0]["anm"].ToString() + "|" + dt.Rows[0]["arnk"].ToString();
                PM_SUPPLIERB.Text = dt.Rows[0]["bid"].ToString() + "|" + dt.Rows[0]["bnm"].ToString() + "|" + dt.Rows[0]["brnk"].ToString();
                PM_SUPPLIERC.Text = dt.Rows[0]["cid"].ToString() + "|" + dt.Rows[0]["cnm"].ToString() + "|" + dt.Rows[0]["crnk"].ToString();
                PM_SUPPLIERD.Text = dt.Rows[0]["did"].ToString() + "|" + dt.Rows[0]["dnm"].ToString() + "|" + dt.Rows[0]["drnk"].ToString();
                PM_SUPPLIERE.Text = dt.Rows[0]["eid"].ToString() + "|" + dt.Rows[0]["enm"].ToString() + "|" + dt.Rows[0]["ernk"].ToString();
                PM_SUPPLIERF.Text = dt.Rows[0]["fid"].ToString() + "|" + dt.Rows[0]["fnm"].ToString() + "|" + dt.Rows[0]["frnk"].ToString();
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
                providerid = PM_SUPPLIERAID.Text;//第一个发运商ID
                providernm = PM_SUPPLIERANAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox13")).Text.ToString();//发运商1的最终报价

                // if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                if (text != "" && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                    min = Convert.ToDouble(text);
                    selectindex = ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count - 1;
                }
                providerid = PM_SUPPLIERBID.Text;//第二家发运商ID
                providernm = PM_SUPPLIERBNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox23")).Text.ToString();//发运商2的最终报价
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
                providerid = PM_SUPPLIERCID.Text;//第三家发运商ID
                providernm = PM_SUPPLIERCNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox33")).Text.ToString();//发运商3的最终报价
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
                providerid = PM_SUPPLIERDID.Text;//第四家发运商
                providernm = PM_SUPPLIERDNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox43")).Text.ToString();//发运商4的最终报价
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
                providerid = PM_SUPPLIEREID.Text;//第五家发运商
                providernm = PM_SUPPLIERENAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox53")).Text.ToString();//发运商5的最终报价
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
                providerid = PM_SUPPLIERFID.Text;//第六家发运商
                providernm = PM_SUPPLIERFNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox63")).Text.ToString();//发运商6的最终报价
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
                break;
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
                providerid = PM_SUPPLIERAID.Text;//第一个发运商ID
                providernm = PM_SUPPLIERANAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox13")).Text.ToString();//发运商1的最终报价
                //if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                if (text != "" && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                    min = Convert.ToDouble(text);
                    selectindex = ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count - 1;
                }
                providerid = PM_SUPPLIERBID.Text;//第二家发运商ID
                providernm = PM_SUPPLIERBNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox23")).Text.ToString();//发运商2的最终报价
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
                providerid = PM_SUPPLIERCID.Text;//第三家发运商ID
                providernm = PM_SUPPLIERCNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox33")).Text.ToString();//发运商3的最终报价
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
                providerid = PM_SUPPLIERDID.Text;//第四家发运商
                providernm = PM_SUPPLIERDNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox43")).Text.ToString();//发运商4的最终报价
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
                providerid = PM_SUPPLIEREID.Text;//第五家发运商
                providernm = PM_SUPPLIERENAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox53")).Text.ToString();//发运商5的最终报价
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
                providerid = PM_SUPPLIERFID.Text;//第六家发运商
                providernm = PM_SUPPLIERFNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox63")).Text.ToString();//发运商6的最终报价
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
                if (((TextBox)reitem.FindControl("PM_SUPPLIERRESID")).Text != "" && ((DropDownList)reitem.FindControl("Drp_supplier")).Items.FindByValue(((TextBox)reitem.FindControl("PM_SUPPLIERRESID")).Text) != null)
                {
                    ((DropDownList)reitem.FindControl("Drp_supplier")).SelectedValue = ((TextBox)reitem.FindControl("PM_SUPPLIERRESID")).Text;
                }
            }
        }
        private void getArticle()      //取得Article数据
        {
            int cup = Convert.ToInt32(this.lb_CurrentPage.Text);  //当前页数,初始化为地1页
            ps = new PagedDataSource();
            ps.DataSource = CreateDataSource().DefaultView;
            ps.AllowPaging = true;
            ps.PageSize = 50;     //每页显示的数据的行数
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
                comparepriceRepeater.DataSource = CreateDataSource();
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
            sqltext = "select B.CM_CONTR,B.CM_PROJ,B.TSA_ID,B.TSA_ENGNAME,B.CM_BIANHAO,B.TSA_MAP,B.CM_JHTIME,B.CM_CUSNAME,A.PM_SUPPLIERRESID,A.PM_FHDETAIL,A.PM_SHUILV,A.supplierresnm1,A.supplierresrank,A.PM_WEIGHT,A.PM_LENGTH,PM_AVGA,A.PM_AVGB,A.PM_AVGC,A.PM_AVGD,A.PM_AVGE,A.PM_AVGF,A.PM_ADDRESS," +
                "A.PM_FYNUM,A.price,A.PM_QOUTEFSTSA,CONVERT(float, A.PM_QOUTELSTSA) AS PM_QOUTELSTSA,A.PM_QOUTEFSTSB,CONVERT(float, A.PM_QOUTELSTSB) AS PM_QOUTELSTSB," +
                "A.PM_QOUTEFSTSC,CONVERT(float, A.PM_QOUTELSTSC) AS PM_QOUTELSTSC,A.PM_QOUTEFSTSD,CONVERT(float, A.PM_QOUTELSTSD) AS PM_QOUTELSTSD," +
                "A.PM_QOUTEFSTSE,CONVERT(float, A.PM_QOUTELSTSE) AS PM_QOUTELSTSE,A.PM_QOUTEFSTSF,CONVERT(float, A.PM_QOUTELSTSF) AS PM_QOUTELSTSF," +
                "isnull(A.PM_STATE,0) as detailstate,isnull(A.PM_CSTATE,0) as detailcstate,A.PM_ID,A.PM_FATHERID FROM View_TBMP_FAYUNPRICE as A left outer join View_CM_FaHuo as B on  (A.TSA_ID=B.TSA_ID and A.PM_ZONGXU=B.ID and A.PM_FID=B.CM_FID)  where PM_SHEETNO='" + gloabsheetno + "'";
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
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);
            Response.Redirect("~/PM_Data/PM_fayun_check_detail.aspx?sheetno=" + gloabsheetno + "");
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
            provider1id = PM_SUPPLIERAID.Text;
            provider2id = PM_SUPPLIERBID.Text;
            provider3id = PM_SUPPLIERCID.Text;
            provider4id = PM_SUPPLIERDID.Text;
            provider5id = PM_SUPPLIEREID.Text;
            provider6id = PM_SUPPLIERFID.Text;
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
            //第一个发运商
            if (resetprovider(PM_SUPPLIERA, PM_SUPPLIERAID, PM_SUPPLIERANAME, LbA_lei))
            {
                initdropdprovider();
            }
            else
            {
                PM_SUPPLIERAID.Text = "";
                PM_SUPPLIERANAME.Text = "";
                PM_SUPPLIERA.Text = "";
                LbA_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第二个发运商
            if (resetprovider(PM_SUPPLIERB, PM_SUPPLIERBID, PM_SUPPLIERBNAME, LbB_lei))
            {
                initdropdprovider();
            }
            else
            {
                PM_SUPPLIERBID.Text = "";
                PM_SUPPLIERBNAME.Text = "";
                PM_SUPPLIERB.Text = "";
                LbB_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第三个发运商
            if (resetprovider(PM_SUPPLIERC, PM_SUPPLIERCID, PM_SUPPLIERCNAME, LbC_lei))
            {
                initdropdprovider();
            }
            else
            {
                PM_SUPPLIERCID.Text = "";
                PM_SUPPLIERCNAME.Text = "";
                PM_SUPPLIERC.Text = "";
                LbC_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第四个发运商
            if (resetprovider(PM_SUPPLIERD, PM_SUPPLIERDID, PM_SUPPLIERDNAME, LbD_lei))
            {
                initdropdprovider();
            }
            else
            {
                PM_SUPPLIERDID.Text = "";
                PM_SUPPLIERDNAME.Text = "";
                PM_SUPPLIERD.Text = "";
                LbD_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第五个发运商
            if (resetprovider(PM_SUPPLIERE, PM_SUPPLIEREID, PM_SUPPLIERENAME, LbE_lei))
            {
                initdropdprovider();
            }
            else
            {
                PM_SUPPLIEREID.Text = "";
                PM_SUPPLIERENAME.Text = "";
                PM_SUPPLIERE.Text = "";
                LbE_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第六个发运商
            if (resetprovider(PM_SUPPLIERF, PM_SUPPLIERFID, PM_SUPPLIERFNAME, LbF_lei))
            {
                initdropdprovider();
            }
            else
            {
                PM_SUPPLIERFID.Text = "";
                PM_SUPPLIERFNAME.Text = "";
                PM_SUPPLIERF.Text = "";
                LbF_lei.Text = "";
                initdropdprovider();
                i++;
            }
            if (i > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('你输入了相同的发运商，请重新输入！');", true);
                return;
            }

            Labelerror.Visible = false;
            Labelerror.Text = "";
            savedate();
            comparepriceRepeaterdatabind();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);
            Response.Redirect("~/PM_Data/PM_fayun_check_detail.aspx?sheetno=" + gloabsheetno + "");
        }

        protected void btn_cancel_Click(object sender, EventArgs e)//取消询比价，如果全部取消则删除询比价单
        {
            hcancel();
        }
        protected void hcancel()
        {
            int i = 0;
            string sqltext = "";
            List<string> list = new List<string>();
            foreach (RepeaterItem Reitem in comparepriceRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        string pmid = ((HiddenField)Reitem.FindControl("PM_ID")).Value;
                        string pmfatherid = ((HiddenField)Reitem.FindControl("PM_FATHERID")).Value;
                        string PM_FYNUM = ((Label)Reitem.FindControl("PM_FYNUM")).Text;
                        string CM_BIANHAO = ((Label)Reitem.FindControl("CM_BIANHAO")).Text;
                        //sqltext = "select PM_FATHERID,PM_SHEETNO from TBMP_FAYUNPRICE where PM_SHEETNO ='" + TextBoxNO.Text + "'";
                        //DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
                        //sqltext = "update TBPM_STRINFODQO set BM_FHSTATE='0' where BM_ID='" + dt1.Rows[0][0].ToString() + "'";
                        //list.Add(sqltext);
                        //sqltext = "DELETE FROM TBMP_FAYUNPRICE WHERE PM_SHEETNO='" + TextBoxNO.Text + "'";
                        
                        //DBCallCommon.ExeSqlText(sqltext);
                        //0511改

                        sqltext = "update TBPM_STRINFODQO set BM_FHSTATE='0',BM_YFNUM=BM_YFNUM-" + PM_FYNUM + " where BM_ID='" + pmfatherid + "'";
                        list.Add(sqltext);

                        sqltext = "update a set CM_BJZT='0', CM_YBJNUM=CM_YBJNUM-" + PM_FYNUM + " from  TBCM_FHBASIC as a left join TBCM_FHNOTICE as b on a.CM_FID=b.CM_FID   where CM_ID='" + pmfatherid + "' and b.CM_BIANHAO='" + CM_BIANHAO + "'";
                        list.Add(sqltext);
                        sqltext = "DELETE FROM TBMP_FAYUNPRICE WHERE PM_ID='" + pmid + "'";
                        list.Add(sqltext);
                    }
                }
            }
            try
            {
                DBCallCommon.ExecuteTrans(list);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('取消的sql语句出现问题，请联系管理员！');", true); return;
            }
            if (i > 0)
            {
                sqltext = "SELECT PM_ID FROM TBMP_FAYUNPRICE WHERE PM_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    sqltext = "DELETE FROM TBMP_FAYUNPRCRVW WHERE ICL_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    Response.Redirect("~/PM_Data/PM_FHList.aspx");
                }
                comparepriceRepeaterdatabind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);

            }
        }
        //protected void allcancel()
        //{
        //    string sqltext = "";
        //    List<string> sqltexts = new List<string>();
        //    sqltext = "update TBMP_PURCHASEPLAN set PUR_STATE='4' where PUR_TextBoxNO.Text in  " +
        //              "(select PM_SHEETNO from TBMP_FAYUNPRICE where PM_SHEETNO='" + TextBoxNO.Text + "')";
        //    sqltexts.Add(sqltext);
        //    sqltext = "delete from TBMP_IQRCMPPRCRVW WHERE ICL_SHEETNO='" + TextBoxNO.Text + "'";
        //    sqltexts.Add(sqltext);
        //    sqltext = "delete from TBMP_FAYUNPRICE WHERE PM_SHEETNO='" + TextBoxNO.Text + "'";
        //    sqltexts.Add(sqltext);
        //    DBCallCommon.ExecuteTrans(sqltexts);
        //}
        //protected void hclose()//行关闭
        //{
        //    int i = 0;
        //    string sqltext = "";
        //    foreach (RepeaterItem Reitem in comparepriceRepeater.Items)
        //    {
        //        CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
        //        if (cbx != null)//存在行
        //        {
        //            if (cbx.Checked)
        //            {
        //                i++;
        //                sqltext = "update TBMP_FAYUNPRICE set PM_CSTATE='1' WHERE PM_SHEETNO='" + TextBoxNO.Text + "' " +
        //                          "and PM_SHEETNO='" + ((Label)Reitem.FindControl("PM_PCODE")).Text.ToString() + "'";
        //                DBCallCommon.ExeSqlText(sqltext);
        //            }
        //        }
        //    }
        //    if (i > 0)
        //    {
        //        sqltext = "SELECT PM_ID FROM TBMP_FAYUNPRICE WHERE PM_SHEETNO='" + TextBoxNO.Text + "' and PM_CSTATE='0'";//是否还存在未关闭的，如果都关闭则整单关闭
        //        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
        //        if (dt.Rows.Count == 0)
        //        {
        //            sqltext = "update TBMP_IQRCMPPRCRVW set ICL_CSTATE='1'  WHERE ICL_SHEETNO='" + TextBoxNO.Text + "'";//单号关闭
        //            DBCallCommon.ExeSqlText(sqltext);
        //        }
        //        comparepriceRepeaterdatabind();
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);

        //    }
        //}
        //protected void fhclose()//行反关闭
        //{
        //    int i = 0;
        //    string sqltext = "";
        //    foreach (RepeaterItem Reitem in comparepriceRepeater.Items)
        //    {
        //        CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
        //        if (cbx != null)//存在行
        //        {
        //            if (cbx.Checked)
        //            {
        //                i++;
        //                sqltext = "update TBMP_FAYUNPRICE set PM_CSTATE='0' WHERE PM_SHEETNO='" + TextBoxNO.Text + "' " +
        //                          "and PM_SHEETNO='" + ((Label)Reitem.FindControl("PM_PCODE")).Text.ToString() + "'";
        //                DBCallCommon.ExeSqlText(sqltext);
        //            }
        //        }
        //    }
        //    if (i > 0)
        //    {
        //        sqltext = "SELECT PM_ID FROM TBMP_FAYUNPRICE WHERE PM_SHEETNO='" + TextBoxNO.Text + "' and PM_CSTATE='0'";//是否还存在未关闭的，如果存在则整单未关闭
        //        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
        //        if (dt.Rows.Count > 0)
        //        {
        //            sqltext = "update TBMP_IQRCMPPRCRVW set ICL_CSTATE='0'  WHERE ICL_SHEETNO='" + TextBoxNO.Text + "'";//单号反关闭
        //            DBCallCommon.ExeSqlText(sqltext);
        //        }
        //        comparepriceRepeaterdatabind();
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);

        //    }
        //}
        //protected void allclose()
        //{
        //    string sqltext = "";
        //    List<string> sqltexts = new List<string>();
        //    sqltext = "update TBMP_IQRCMPPRCRVW set ICL_CSTATE='1'  WHERE ICL_SHEETNO='" + TextBoxNO.Text + "'";//单号关闭
        //    sqltexts.Add(sqltext);
        //    sqltext = "update  TBMP_FAYUNPRICE set PM_CSTATE='1' WHERE PM_SHEETNO='" + TextBoxNO.Text + "'";//条目关闭
        //    sqltexts.Add(sqltext);
        //    DBCallCommon.ExecuteTrans(sqltexts);
        //}//单子关闭
        //protected void fallclose()
        //{
        //    string sqltext = "";
        //    List<string> sqltexts = new List<string>();
        //    sqltext = "update TBMP_IQRCMPPRCRVW set ICL_CSTATE='0'  WHERE ICL_SHEETNO='" + TextBoxNO.Text + "'";//单号反关闭
        //    sqltexts.Add(sqltext);
        //    sqltext = "update  TBMP_FAYUNPRICE set PM_CSTATE='0' WHERE PM_SHEETNO='" + TextBoxNO.Text + "'";//条目反关闭
        //    sqltexts.Add(sqltext);
        //    DBCallCommon.ExecuteTrans(sqltexts);
        //}
        //protected void btn_change_Click(object sender, EventArgs e)
        //{
        //    string sqltext = "";
        //    foreach (RepeaterItem Reitem in comparepriceRepeater.Items)
        //    {
        //        CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
        //        if (cbx != null)//存在行
        //        {
        //            if (cbx.Checked)
        //            {
        //                sqltext = "UPDATE a SET PM_QUANTITY = a.PM_QUANTITY+b.MP_BGZYNUM, " +
        //                          "PM_ZXNUM=a.PM_ZXNUM+b.MP_BGZYNUM,PM_ZXFUNUM=a.PM_ZXFUNUM+b.MP_BGFZNUM,PM_FZNUM=a.PM_FZNUM+b.MP_BGFZNUM  " +
        //                          "FROM  TBMP_FAYUNPRICE AS a INNER JOIN " +
        //                          "TBPC_MPCHANGEDETAIL AS b ON a.PM_PCODE = b.MP_OLDTextBoxNO.Text " +
        //                          "WHERE  (a.PM_PCODE = '" + ((Label)Reitem.FindControl("PM_PCODE")).Text + "')";
        //                DBCallCommon.ExeSqlText(sqltext);
        //                sqltext = "UPDATE a SET PUR_NUM = a.PUR_NUM + b.MP_BGNUM, " +
        //                          "PUR_RPNUM=a.PUR_RPNUM+b.MP_BGNUM,PUR_RPWEIGHT=a.PUR_RPWEIGHT+b.MP_WEIGHT,PUR_WEIGHT=a.PUR_WEIGHT+b.MP_WEIGHT  " +
        //                          "FROM  TBPC_PURCHASEPLAN AS a INNER JOIN " +
        //                          "TBPC_MPCHANGEDETAIL AS b ON a.PUR_TextBoxNO.Text = b.MP_OLDTextBoxNO.Text " +
        //                          "WHERE  (a.PUR_TextBoxNO.Text = '" + ((Label)Reitem.FindControl("PM_PCODE")).Text + "')";
        //                DBCallCommon.ExeSqlText(sqltext);
        //                sqltext = "UPDATE TBPC_MPCHANGEDETAIL SET MP_STATE='1' WHERE MP_OLDTextBoxNO.Text='" + ((Label)Reitem.FindControl("PM_PCODE")).Text + "'";
        //                DBCallCommon.ExeSqlText(sqltext);
        //            }
        //        }
        //    }
        //}

        //protected void btn_search_Click(object sender, EventArgs e)//搜索
        //{
        //    int i = 0;
        //    string marid = "";
        //    foreach (RepeaterItem Reitem in comparepriceRepeater.Items)
        //    {
        //        CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
        //        if (cbx != null)//存在行
        //        {
        //            if (cbx.Checked)
        //            {
        //                i++;
        //                marid = ((Label)Reitem.FindControl("PM_MARID")).Text.ToString();
        //                //marshijian = Tb_zdant.Text;
        //                //break;
        //            }
        //        }
        //    }
        //    if (i == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据');", true);
        //        return;
        //    }
        //    else if (i >= 2)
        //    {
        //        comparepriceRepeaterdatabind();    //刷新  2013年6月26日 11:52:30   Meng
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择一条数据');", true);
        //        return;
        //    }
        //    foreach (RepeaterItem Reitem in comparepriceRepeater.Items)
        //    {
        //        CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
        //        if (cbx != null)//存在行
        //        {
        //            if (cbx.Checked)
        //            {
        //                cbx.Checked = false;
        //            }
        //        }
        //    }
        //    comparepriceRepeaterdatabind();    //刷新  2013年6月26日 11:52:30   Meng
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "windowopen('PC_Date_historypriceshow.aspx?marid=" + marid + "');", true);
        //    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "windowopen('PC_Date_historypriceshow.aspx?marid=" + marid +"&marshijian=" + marshijian + "');", true);
        //}
        /// <summary>
        /// 保存数据
        /// </summary>
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

            double num = 0;
            double zxnum = 0;
            double fznum = 0;
            double zxfznum = 0;
            double shuilv = 0;
            string zxid = Session["UserID"].ToString();
            string zxname = Session["UserName"].ToString();
            string state = "";
            string note = "";
            string neirong = "";
            string engname = "";
            string tsa_id = "";
            string map = "";
            string weight = "";
            string length = "";
            string adress = "";
            string avga = "";
            string avgb = "";
            string avge = "";
            string avgd = "";
            string avgf = "";
            string avgc = "";
            //发运商A
            suppaid = PM_SUPPLIERAID.Text.ToString().Replace(" ", "");
            //发运商B
            suppbid = PM_SUPPLIERBID.Text.ToString().Replace(" ", "");
            //发运商C
            suppcid = PM_SUPPLIERCID.Text.ToString().Replace(" ", "");
            //发运商D
            suppdid = PM_SUPPLIERDID.Text.ToString().Replace(" ", "");
            //发运商E
            suppeid = PM_SUPPLIEREID.Text.ToString().Replace(" ", "");
            //发运商F
            suppfid = PM_SUPPLIERFID.Text.ToString().Replace(" ", "");
            //如果输入发运商信息更新公共数据
            foreach (RepeaterItem Retem in comparepriceRepeater.Items)
            {

                resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                shuilv = Convert.ToDouble(((TextBox)Retem.FindControl("PM_SHUILV")).Text.ToString());
                //neirong = ((TextBox)Retem.FindControl("txt_neirong")).Text.ToString();
                tsa_id = ((Label)Retem.FindControl("TSA_ID")).Text.ToString();
                engname = ((Label)Retem.FindControl("TSA_ENGNAME")).Text.ToString();
                map = ((Label)Retem.FindControl("TSA_MAP")).Text.ToString();
                weight = ((TextBox)Retem.FindControl("PM_WEIGHT")).Text.ToString();
                length = ((TextBox)Retem.FindControl("PM_LENGTH")).Text.ToString();
                adress = ((TextBox)Retem.FindControl("PM_ADDRESS")).Text.ToString();
                avga = ((TextBox)Retem.FindControl("PM_AVGA")).Text.ToString();
                avgb = ((TextBox)Retem.FindControl("PM_AVGB")).Text.ToString();
                avgc = ((TextBox)Retem.FindControl("PM_AVGC")).Text.ToString();
                avgd = ((TextBox)Retem.FindControl("PM_AVGD")).Text.ToString();
                avge = ((TextBox)Retem.FindControl("PM_AVGE")).Text.ToString();
                avgf = ((TextBox)Retem.FindControl("PM_AVGF")).Text.ToString();
                //sqltext = "UPDATE TBMP_FAYUNPRICE SET  " +
                //          "PM_SUPPLIERRESID='" +
                //          resproid + "',PM_SHUILV='" + shuilv + "',PM_NOTE='" +
                //note + "' WHERE PM_SHEETNO='" + sheetno + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                sqltext = "UPDATE TBMP_FAYUNPRICE SET  " +
                          "PM_SUPPLIERRESID='" +
                          resproid + "',PM_SHUILV='" + shuilv + "',PM_NOTE='" +
                          note + "' ,PM_WEIGHT='" + weight + "',PM_LENGTH='" + length + "',PM_ADDRESS='" + adress + "',PM_AVGA='" + avga + "',PM_AVGB='" + avgb + "',PM_AVGC='" + avgc + "',PM_AVGD='" + avgd + "',PM_AVGE='" + avge + "',PM_AVGF='" + avgf + "' WHERE PM_SHEETNO='" + sheetno + "'";
                //PM_STATE='1'已保存
                DBCallCommon.ExeSqlText(sqltext);
                break;
            }

            #region
            if (PM_SUPPLIERAID.Text.ToString().Replace(" ", "") != "" && PM_SUPPLIERANAME.Text.ToString().Replace(" ", "") != "")
            {
                //保存发运商1的数据
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    suppaid = PM_SUPPLIERAID.Text;
                    tsa_id = ((Label)Retem.FindControl("TSA_ID")).Text.ToString();
                    engname = ((Label)Retem.FindControl("TSA_ENGNAME")).Text.ToString();
                    map = ((Label)Retem.FindControl("TSA_MAP")).Text.ToString();
                    string fstsa = ((TextBox)Retem.FindControl("TextBox11")).Text.ToString();
                    double lassa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox13")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox13")).Text.ToString());

                    string sqltext1 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIERAID='" + suppaid + "'";
                    if (fstsa != "")
                    {
                        sqltext1 = sqltext1 + ",PM_QOUTEFSTSA='" + fstsa + "'";
                    }
                    else if (fstsa == "")
                    {
                        sqltext1 = sqltext1 + ",PM_QOUTEFSTSA='' ";
                    }

                    //if (lassa != 0)
                    //{
                    //    sqltext1 = sqltext1 + ",PM_QOUTELSTSA='" + lassa + "'";
                    //}
                    //else if (lassa == 0)
                    //{
                    //    sqltext1 = sqltext1 + ",PM_QOUTELSTSA=null";
                    //}
                    sqltext1 = sqltext1 + ",PM_QOUTELSTSA='" + lassa + "'";
                    //sqltext1 = sqltext1 + "  WHERE PM_SHEETNO='" + sheetno + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                    sqltext1 = sqltext1 + "  WHERE PM_SHEETNO='" + sheetno + "'";
                    DBCallCommon.ExeSqlText(sqltext1);
                    //sqltext1 = "update TBMP_FAYUNPRICE set PM_SUPPLIERAID='" + suppaid + "' where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                    sqltext1 = "update TBMP_FAYUNPRICE set PM_SUPPLIERAID='" + suppaid + "' where PM_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext1);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PM_SUPPLIERAID.Text)
                    {
                        //sqltext = "update TBMP_FAYUNPRICE set PM_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox13")).Text.ToString()) + "'where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                        sqltext = "update TBMP_FAYUNPRICE set PM_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox13")).Text.ToString()) + "'where PM_SHEETNO='" + TextBoxNO.Text + "'";

                        DBCallCommon.ExeSqlText(sqltext);
                    }
                    break;
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    tsa_id = ((Label)Retem.FindControl("TSA_ID")).Text.ToString();
                    engname = ((Label)Retem.FindControl("TSA_ENGNAME")).Text.ToString();
                    map = ((Label)Retem.FindControl("TSA_MAP")).Text.ToString();
                    //string sql1 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIERAID=null,PM_QOUTEFSTSA=null,PM_QOUTELSTSA=null where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                    string sql1 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIERAID=null,PM_QOUTEFSTSA=null,PM_QOUTELSTSA=null where PM_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sql1);
                    break;
                }
            }
            #endregion
            #region
            if (PM_SUPPLIERBID.Text.ToString().Replace(" ", "") != "" && PM_SUPPLIERBNAME.Text.ToString().Replace(" ", "") != "")
            {
                //保存发运商2的数据
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {

                    suppbid = PM_SUPPLIERBID.Text;
                    tsa_id = ((Label)Retem.FindControl("TSA_ID")).Text.ToString();
                    engname = ((Label)Retem.FindControl("TSA_ENGNAME")).Text.ToString();
                    map = ((Label)Retem.FindControl("TSA_MAP")).Text.ToString();
                    string fstsa = ((TextBox)Retem.FindControl("TextBox21")).Text.ToString();
                    double lassa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox23")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox23")).Text.ToString());
                    string sqltext2 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIERBID='" + suppbid + "'";
                    if (fstsa != "")
                    {
                        sqltext2 = sqltext2 + ",PM_QOUTEFSTSB='" + fstsa + "' ";
                    }
                    else if (fstsa == "")
                    {
                        sqltext2 = sqltext2 + ",PM_QOUTEFSTSB=null ";
                    }

                    //if (lassa != 0)
                    //{
                    //    sqltext2 = sqltext2 + ",PM_QOUTELSTSB=" + lassa + " ";
                    //}
                    //else if (lassa == 0)
                    //{
                    //    sqltext2 = sqltext2 + ",PM_QOUTELSTSB=null ";
                    //}
                    sqltext2 = sqltext2 + ",PM_QOUTELSTSB=" + lassa + " ";
                    //sqltext2 = sqltext2 + "  WHERE PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                    sqltext2 = sqltext2 + "  WHERE PM_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext2);
                    //sqltext2 = "update TBMP_FAYUNPRICE set PM_SUPPLIERBID='" + suppbid + "' where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                    sqltext2 = "update TBMP_FAYUNPRICE set PM_SUPPLIERBID='" + suppbid + "' where PM_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext2);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PM_SUPPLIERBID.Text)
                    {
                        //sqltext = "update TBMP_FAYUNPRICE set PM_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox23")).Text.ToString())+ "' " +
                        //          "where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                        sqltext = "update TBMP_FAYUNPRICE set PM_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox23")).Text.ToString()) + "' " +
                                 "where PM_SHEETNO='" + TextBoxNO.Text + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                    break;
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    TextBoxNO.Text = TextBoxNO.Text.ToString();
                    tsa_id = ((Label)Retem.FindControl("TSA_ID")).Text.ToString();
                    engname = ((Label)Retem.FindControl("TSA_ENGNAME")).Text.ToString();
                    map = ((Label)Retem.FindControl("TSA_MAP")).Text.ToString();
                    //string sql2 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIERBID=null,PM_QOUTEFSTSB=null,PM_QOUTELSTSB=null where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                    string sql2 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIERBID=null,PM_QOUTEFSTSB=null,PM_QOUTELSTSB=null where PM_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sql2);
                    break;
                }
            }
            #endregion
            #region
            if (PM_SUPPLIERCID.Text.ToString().Replace(" ", "") != "" && PM_SUPPLIERCNAME.Text.ToString().Replace(" ", "") != "")
            {
                //保存发运商3的数据

                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    TextBoxNO.Text = TextBoxNO.Text.ToString();
                    suppcid = PM_SUPPLIERCID.Text;
                    tsa_id = ((Label)Retem.FindControl("TSA_ID")).Text.ToString();
                    engname = ((Label)Retem.FindControl("TSA_ENGNAME")).Text.ToString();
                    map = ((Label)Retem.FindControl("TSA_MAP")).Text.ToString();
                    string fstsa = ((TextBox)Retem.FindControl("TextBox31")).Text.ToString();
                    double lassa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox33")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox33")).Text.ToString());
                    string sqltext3 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIERCID='" + suppcid + "'";
                    if (fstsa != "")
                    {
                        sqltext3 = sqltext3 + ",PM_QOUTEFSTSC='" + fstsa + "' ";
                    }
                    else if (fstsa == "")
                    {
                        sqltext3 = sqltext3 + ",PM_QOUTEFSTSC=null ";
                    }

                    //if (lassa != 0)
                    //{
                    //    sqltext3 = sqltext3 + ",PM_QOUTELSTSC=" + lassa + " ";
                    //}
                    //else if (lassa == 0)
                    //{
                    //    sqltext3 = sqltext3 + ",PM_QOUTELSTSC=null ";
                    //}
                    sqltext3 = sqltext3 + ",PM_QOUTELSTSC=" + lassa + " ";
                    sqltext3 = sqltext3 + "  WHERE PM_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext3);
                    //sqltext3 = "update TBMP_FAYUNPRICE set PM_SUPPLIERCID='" + suppcid + "' where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                    sqltext3 = "update TBMP_FAYUNPRICE set PM_SUPPLIERCID='" + suppcid + "' where PM_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext3);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PM_SUPPLIERCID.Text)
                    {
                        //sqltext = "update TBMP_FAYUNPRICE set PM_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox32")).Text.ToString())+ "' " +
                        //          "where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                        sqltext = "update TBMP_FAYUNPRICE set PM_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox33")).Text.ToString()) + "' " +
                                  "where PM_SHEETNO='" + TextBoxNO.Text + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                    break;
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    TextBoxNO.Text = TextBoxNO.Text.ToString();
                    tsa_id = ((Label)Retem.FindControl("TSA_ID")).Text.ToString();
                    engname = ((Label)Retem.FindControl("TSA_ENGNAME")).Text.ToString();
                    map = ((Label)Retem.FindControl("TSA_MAP")).Text.ToString();
                    //string sql3 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIERCID=null,PM_QOUTEFSTSC=null,PM_QOUTELSTSC=null where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                    string sql3 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIERCID=null,PM_QOUTEFSTSC=null,PM_QOUTELSTSC=null where PM_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sql3);
                    break;
                }
            }
            #endregion
            #region
            if (PM_SUPPLIERDID.Text.ToString().Replace(" ", "") != "" && PM_SUPPLIERDNAME.Text.ToString().Replace(" ", "") != "")
            {
                //保存发运商4的数据

                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    TextBoxNO.Text = TextBoxNO.Text.ToString();
                    suppdid = PM_SUPPLIERDID.Text;
                    tsa_id = ((Label)Retem.FindControl("TSA_ID")).Text.ToString();
                    engname = ((Label)Retem.FindControl("TSA_ENGNAME")).Text.ToString();
                    map = ((Label)Retem.FindControl("TSA_MAP")).Text.ToString();
                    string fstsa = ((TextBox)Retem.FindControl("TextBox41")).Text.ToString();
                    double lassa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox43")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox43")).Text.ToString());
                    string sqltext4 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIERDID='" + suppdid + "'";
                    if (fstsa != "")
                    {
                        sqltext4 = sqltext4 + ",PM_QOUTEFSTSD='" + fstsa + "' ";
                    }
                    else
                    {
                        sqltext4 = sqltext4 + ",PM_QOUTEFSTSD=null ";
                    }

                    //if (lassa != 0)
                    //{
                    //    sqltext4 = sqltext4 + ",PM_QOUTELSTSD=" + lassa + " ";
                    //}
                    //else
                    //{
                    //    sqltext4 = sqltext4 + ",PM_QOUTELSTSD=null ";
                    //}
                    sqltext4 = sqltext4 + ",PM_QOUTELSTSD=" + lassa + " ";
                    sqltext4 = sqltext4 + "  WHERE PM_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext4);
                    sqltext4 = "update TBMP_FAYUNPRICE set PM_SUPPLIERDID='" + suppdid + "' where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                    DBCallCommon.ExeSqlText(sqltext4);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PM_SUPPLIERDID.Text)
                    {
                        sqltext = "update TBMP_FAYUNPRICE set PM_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox43")).Text.ToString()) + "' " +
                                  "where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                    break;
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    TextBoxNO.Text = TextBoxNO.Text.ToString();
                    tsa_id = ((Label)Retem.FindControl("TSA_ID")).Text.ToString();
                    engname = ((Label)Retem.FindControl("TSA_ENGNAME")).Text.ToString();
                    map = ((Label)Retem.FindControl("TSA_MAP")).Text.ToString();
                    //string sql4 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIERDID=null,PM_QOUTEFSTSD=null,PM_QOUTELSTSD=null where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                    string sql4 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIERDID=null,PM_QOUTEFSTSD=null,PM_QOUTELSTSD=null where PM_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sql4);
                    break;
                }
            }
            #endregion
            #region
            if (PM_SUPPLIEREID.Text.ToString().Replace(" ", "") != "" && PM_SUPPLIERENAME.Text.ToString().Replace(" ", "") != "")
            {
                //保存发运商5的数据

                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    TextBoxNO.Text = TextBoxNO.Text.ToString();
                    suppeid = PM_SUPPLIEREID.Text;
                    tsa_id = ((Label)Retem.FindControl("TSA_ID")).Text.ToString();
                    engname = ((Label)Retem.FindControl("TSA_ENGNAME")).Text.ToString();
                    map = ((Label)Retem.FindControl("TSA_MAP")).Text.ToString();
                    string fstsa = ((TextBox)Retem.FindControl("TextBox51")).Text.ToString();
                    double lassa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox53")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox53")).Text.ToString());
                    string sqltext5 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIEREID='" + suppeid + "'";
                    if (fstsa != "")
                    {
                        sqltext5 = sqltext5 + ",PM_QOUTEFSTSE='" + fstsa + "' ";
                    }
                    else
                    {
                        sqltext5 = sqltext5 + ",PM_QOUTEFSTSE=null ";
                    }

                    //if (lassa != 0)
                    //{
                    //    sqltext5 = sqltext5 + ",PM_QOUTELSTSE=" + lassa + " ";
                    //}
                    //else
                    //{
                    //    sqltext5 = sqltext5 + ",PM_QOUTELSTSE=null ";
                    //}
                    sqltext5 = sqltext5 + ",PM_QOUTELSTSE=" + lassa + " ";
                    sqltext5 = sqltext5 + "  WHERE PM_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext5);
                    //sqltext5 = "update TBMP_FAYUNPRICE set PM_SUPPLIEREID='" + suppeid + "' where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                    sqltext5 = "update TBMP_FAYUNPRICE set PM_SUPPLIEREID='" + suppeid + "' where PM_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext5);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PM_SUPPLIEREID.Text)
                    {
                        //sqltext = "update TBMP_FAYUNPRICE set PM_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox53")).Text.ToString()) + "' " +
                        //          "where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                        sqltext = "update TBMP_FAYUNPRICE set PM_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox53")).Text.ToString()) + "' " +
                                  "where PM_SHEETNO='" + TextBoxNO.Text + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                    break;
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    TextBoxNO.Text = TextBoxNO.Text.ToString();
                    tsa_id = ((Label)Retem.FindControl("TSA_ID")).Text.ToString();
                    engname = ((Label)Retem.FindControl("TSA_ENGNAME")).Text.ToString();
                    map = ((Label)Retem.FindControl("TSA_MAP")).Text.ToString();
                    //string sql5 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIEREID=null,PM_QOUTEFSTSE=null,PM_QOUTELSTSE=null where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                    string sql5 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIEREID=null,PM_QOUTEFSTSE=null,PM_QOUTELSTSE=null where PM_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sql5);
                    break;
                }
            }
            #endregion
            #region
            if (PM_SUPPLIERFID.Text.ToString().Replace(" ", "") != "" && PM_SUPPLIERFNAME.Text.ToString().Replace(" ", "") != "")
            {
                //保存发运商6的数据
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    TextBoxNO.Text = TextBoxNO.Text.ToString();
                    suppfid = PM_SUPPLIERFID.Text;
                    tsa_id = ((Label)Retem.FindControl("TSA_ID")).Text.ToString();
                    engname = ((Label)Retem.FindControl("TSA_ENGNAME")).Text.ToString();
                    map = ((Label)Retem.FindControl("TSA_MAP")).Text.ToString();
                    string fstsa = ((TextBox)Retem.FindControl("TextBox61")).Text.ToString();
                    double lassa = Convert.ToDouble(((TextBox)Retem.FindControl("TextBox63")).Text.ToString() == "" ? "0.00" : ((TextBox)Retem.FindControl("TextBox63")).Text.ToString());
                    string sqltext6 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIERFID='" + suppfid + "'";
                    if (fstsa != "")
                    {
                        sqltext6 = sqltext6 + ",PM_QOUTEFSTSF='" + fstsa + "' ";
                    }
                    else
                    {
                        sqltext6 = sqltext6 + ",PM_QOUTEFSTSF=null ";
                    }

                    //if (lassa != 0)
                    //{
                    //    sqltext6 = sqltext6 + ",PM_QOUTELSTSF=" + lassa + " ";
                    //}
                    //else
                    //{
                    //    sqltext6 = sqltext6 + ",PM_QOUTELSTSF=null ";
                    //}
                    sqltext6 = sqltext6 + ",PM_QOUTELSTSF=" + lassa + " ";
                    sqltext6 = sqltext6 + "  WHERE PM_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext6);
                    //sqltext6 = "update TBMP_FAYUNPRICE set PM_SUPPLIERFID='" + suppfid + "' where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                    sqltext6 = "update TBMP_FAYUNPRICE set PM_SUPPLIERFID='" + suppfid + "' where PM_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sqltext6);
                    resproid = ((DropDownList)Retem.FindControl("Drp_supplier")).SelectedValue.ToString();
                    if (resproid == PM_SUPPLIERFID.Text)
                    {
                        //sqltext = "update TBMP_FAYUNPRICE set PM_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox62")).Text.ToString()) + "' " +
                        //          "where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                        sqltext = "update TBMP_FAYUNPRICE set PM_PRICE='" + Convert.ToDouble(((TextBox)Retem.FindControl("TextBox63")).Text.ToString()) + "' " +
                                 "where PM_SHEETNO='" + TextBoxNO.Text + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                    break;
                }
            }
            else
            {
                foreach (RepeaterItem Retem in comparepriceRepeater.Items)
                {
                    TextBoxNO.Text = TextBoxNO.Text.ToString();
                    tsa_id = ((Label)Retem.FindControl("TSA_ID")).Text.ToString();
                    engname = ((Label)Retem.FindControl("TSA_ENGNAME")).Text.ToString();
                    map = ((Label)Retem.FindControl("TSA_MAP")).Text.ToString();
                    //string sql5 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIERFID=null,PM_QOUTEFSTSF=null,PM_QOUTELSTSF=null where PM_SHEETNO='" + TextBoxNO.Text + "'AND TSA_ID='" + tsa_id + "'AND PM_ENGNAME='" + engname + "' AND PM_MAP='" + map + "'";
                    string sql5 = "UPDATE TBMP_FAYUNPRICE SET PM_SUPPLIERFID=null,PM_QOUTEFSTSF=null,PM_QOUTELSTSF=null where PM_SHEETNO='" + TextBoxNO.Text + "'";
                    DBCallCommon.ExeSqlText(sql5);
                    break;
                }
            }
            #endregion
            string sqltext7 = "UPDATE TBMP_FAYUNPRCRVW SET ICL_ZHUGUANID='" + cob_zgjl.SelectedValue.ToString() +
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
            Response.Redirect("PM_FHList.aspx");
        }
        //比总价保存
        protected void btn_BZJ_Click(object sender, EventArgs e)
        {
            string ptc = "";
            double price = 0;
            string providerid = "";
            string sqlt = "";
            int i = 0;
            //第一个发运商
            if (resetprovider(PM_SUPPLIERA, PM_SUPPLIERAID, PM_SUPPLIERANAME, LbA_lei))
            {
                initdropdprovider();
            }
            else
            {
                PM_SUPPLIERAID.Text = "";
                PM_SUPPLIERANAME.Text = "";
                PM_SUPPLIERA.Text = "";
                LbA_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第二个发运商
            if (resetprovider(PM_SUPPLIERB, PM_SUPPLIERBID, PM_SUPPLIERBNAME, LbB_lei))
            {
                initdropdprovider();
            }
            else
            {
                PM_SUPPLIERBID.Text = "";
                PM_SUPPLIERBNAME.Text = "";
                PM_SUPPLIERB.Text = "";
                LbB_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第三个发运商
            if (resetprovider(PM_SUPPLIERC, PM_SUPPLIERCID, PM_SUPPLIERCNAME, LbC_lei))
            {
                initdropdprovider();
            }
            else
            {
                PM_SUPPLIERCID.Text = "";
                PM_SUPPLIERCNAME.Text = "";
                PM_SUPPLIERC.Text = "";
                LbC_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第四个发运商
            if (resetprovider(PM_SUPPLIERD, PM_SUPPLIERDID, PM_SUPPLIERDNAME, LbD_lei))
            {
                initdropdprovider();
            }
            else
            {
                PM_SUPPLIERDID.Text = "";
                PM_SUPPLIERDNAME.Text = "";
                PM_SUPPLIERD.Text = "";
                LbD_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第五个发运商
            if (resetprovider(PM_SUPPLIERE, PM_SUPPLIEREID, PM_SUPPLIERENAME, LbE_lei))
            {
                initdropdprovider();
            }
            else
            {
                PM_SUPPLIEREID.Text = "";
                PM_SUPPLIERENAME.Text = "";
                PM_SUPPLIERE.Text = "";
                LbE_lei.Text = "";
                initdropdprovider();
                i++;
            }
            //第六个发运商
            if (resetprovider(PM_SUPPLIERF, PM_SUPPLIERFID, PM_SUPPLIERFNAME, LbF_lei))
            {
                initdropdprovider();
            }
            else
            {
                PM_SUPPLIERFID.Text = "";
                PM_SUPPLIERFNAME.Text = "";
                PM_SUPPLIERF.Text = "";
                LbF_lei.Text = "";
                initdropdprovider();
                i++;
            }
            if (i > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('你输入了相同的发运商，请重新输入！');", true);
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
                providerid = PM_SUPPLIERAID.Text;
                sqlt = "select PM_SHEETNO,PM_QOUTELSTSA from TBMP_FAYUNPRICE where PM_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PM_SHEETNO"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PM_QOUTELSTSA"].ToString() == "" ? "0" : dt.Rows[k]["PM_QOUTELSTSA"].ToString());
                        sqlt = "update TBMP_FAYUNPRICE  set PM_PRICE=" + price + " where PM_SHEETNO='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBMP_FAYUNPRICE set PM_SUPPLIERRESID='" + providerid + "' where PM_SHEETNO='" + TextBoxNO.Text + "'";
                DBCallCommon.ExeSqlText(sqlt);
            }
            else if (minje == ZJE2)
            {
                providerid = PM_SUPPLIERBID.Text;
                sqlt = "select PM_SHEETNO,PM_QOUTELSTSB from TBMP_FAYUNPRICE where PM_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PM_SHEETNO"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PM_QOUTELSTSB"].ToString() == "" ? "0" : dt.Rows[k]["PM_QOUTELSTSB"].ToString());
                        sqlt = "update TBMP_FAYUNPRICE  set PM_PRICE=" + price + " where PM_SHEETNO='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBMP_FAYUNPRICE set PM_SUPPLIERRESID='" + providerid + "'  where PM_SHEETNO='" + TextBoxNO.Text + "'";
                DBCallCommon.ExeSqlText(sqlt);
            }
            else if (minje == ZJE3)
            {
                providerid = PM_SUPPLIERCID.Text;
                sqlt = "select PM_SHEETNO,PM_QOUTELSTSC from TBMP_FAYUNPRICE where PM_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PM_SHEETNO"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PM_QOUTELSTSC"].ToString() == "" ? "0" : dt.Rows[k]["PM_QOUTELSTSC"].ToString());
                        sqlt = "update TBMP_FAYUNPRICE  set PM_PRICE=" + price + " where PM_SHEETNO='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBMP_FAYUNPRICE set PM_SUPPLIERRESID='" + providerid + "' where PM_SHEETNO='" + TextBoxNO.Text + "'";
                DBCallCommon.ExeSqlText(sqlt);
            }
            else if (minje == ZJE4)
            {
                providerid = PM_SUPPLIERDID.Text;
                sqlt = "select PM_SHEETNO,PM_QOUTELSTSD from TBMP_FAYUNPRICE where PM_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PM_SHEETNO"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PM_QOUTELSTSD"].ToString() == "" ? "0" : dt.Rows[k]["PM_QOUTELSTSD"].ToString());
                        sqlt = "update TBMP_FAYUNPRICE  set PM_PRICE=" + price + " where PM_SHEETNO='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBMP_FAYUNPRICE set PM_SUPPLIERRESID='" + providerid + "'  where PM_SHEETNO='" + TextBoxNO.Text + "'";
                DBCallCommon.ExeSqlText(sqlt);
            }
            else if (minje == ZJE5)
            {
                providerid = PM_SUPPLIEREID.Text;
                sqlt = "select PM_SHEETNO,PM_QOUTELSTSE from TBMP_FAYUNPRICE where PM_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PM_SHEETNO"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PM_QOUTELSTSE"].ToString() == "" ? "0" : dt.Rows[k]["PM_QOUTELSTSE"].ToString());
                        sqlt = "update TBMP_FAYUNPRICE  set PM_PRICE=" + price + " where PM_SHEETNO='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBMP_FAYUNPRICE set PM_SUPPLIERRESID='" + providerid + "'  where PM_SHEETNO='" + TextBoxNO.Text + "'";
                DBCallCommon.ExeSqlText(sqlt);
            }
            else if (minje == ZJE6)
            {
                providerid = PM_SUPPLIERFID.Text;
                sqlt = "select PM_SHEETNO,PM_QOUTELSTSE from TBMP_FAYUNPRICE where PM_SHEETNO='" + TextBoxNO.Text + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlt);
                if (dt.Rows.Count > 0)
                {
                    for (int k = 0; k < dt.Rows.Count; k++)
                    {
                        ptc = dt.Rows[k]["PM_SHEETNO"].ToString();
                        price = Convert.ToDouble(dt.Rows[k]["PM_QOUTELSTSF"].ToString() == "" ? "0" : dt.Rows[k]["PM_QOUTELSTSF"].ToString());
                        sqlt = "update TBMP_FAYUNPRICE  set PM_PRICE=" + price + " where PM_SHEETNO='" + ptc + "'";
                        DBCallCommon.ExeSqlText(sqlt);
                    }
                }
                sqlt = "update TBMP_FAYUNPRICE set PM_SUPPLIERRESID='" + providerid + "'  where PM_SHEETNO='" + TextBoxNO.Text + "'";
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
                providerid = PM_SUPPLIERAID.Text;//第一个发运商ID
                providernm = PM_SUPPLIERANAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox13")).Text.ToString();//发运商1的最终报价
                if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);

                }
                providerid = PM_SUPPLIERBID.Text;//第二家发运商ID
                providernm = PM_SUPPLIERBNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox23")).Text.ToString();//发运商2的最终报价
                if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);
                }
                providerid = PM_SUPPLIERCID.Text;//第三家发运商ID
                providernm = PM_SUPPLIERCNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox33")).Text.ToString();//发运商3的最终报价
                if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);

                }
                providerid = PM_SUPPLIERDID.Text;//第四家发运商
                providernm = PM_SUPPLIERDNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox43")).Text.ToString();//发运商4的最终报价
                if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);

                }
                providerid = PM_SUPPLIEREID.Text;//第五家发运商
                providernm = PM_SUPPLIERENAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox53")).Text.ToString();//发运商5的最终报价
                if (text != "" && Convert.ToDouble(text) != 0 && providernm != "")
                {
                    ListItem itemprovider1 = new ListItem(providernm, providerid);
                    ((DropDownList)reitem.FindControl("Drp_supplier")).Items.Insert(((DropDownList)reitem.FindControl("Drp_supplier")).Items.Count, itemprovider1);

                }
                providerid = PM_SUPPLIERFID.Text;//第六家发运商
                providernm = PM_SUPPLIERFNAME.Text;
                text = ((TextBox)reitem.FindControl("TextBox63")).Text.ToString();//发运商6的最终报价
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
    }
}
