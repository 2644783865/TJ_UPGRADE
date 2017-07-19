using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Office.Interop.MSProject;
using ZCZJ_DPF.CommonClass;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_CGHT : System.Web.UI.Page
    {
        string id = string.Empty;
        string action = string.Empty;
        string username = string.Empty;
        string orderno = string.Empty;
        string gysid = string.Empty;
        string hth = string.Empty;
        DataTable dts = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];
            id = Request.QueryString["id"];
            orderno = Request.QueryString["ddbh"];
            gysid = Request.QueryString["gysid"];
            username = Session["UserName"].ToString();
            hth = Request.QueryString["hth"];
            if (action != "add" && action != "add1" && action != "bcxy")
            {
                string sql1 = "select * from PC_CGHT where HT_ID='" + id + "'";
                dts = DBCallCommon.GetDTUsingSqlText(sql1);
            }
            if (!IsPostBack)
            {
                BindData();
                PowerControl();
                if (action != "add" && action != "add1" && action != "bcxy")
                {
                    bindBCXY();
                    BindFP(id);
                    BindFK(id);
                }
            }
        }

        private void BindData()
        {
            if (action == "add")
            {
                lb_HT_ZDR.Text = username;
                lb_HT_ZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txt_HT_XFHTBH.Text = GetHT_XFHTBH();
            }
            else if (action == "add1")
            {
                string sql = "select * from TBCS_CUSUPINFO where CS_CODE='" + gysid + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                txt_HT_GF.Text = dt.Rows[0]["CS_NAME"].ToString();
                txt_HT_DZ.Text = dt.Rows[0]["CS_ADDRESS"].ToString();
                txt_HT_FDDBR.Text = dt.Rows[0]["CS_ADDRESS"].ToString();
                txt_HT_DH.Text = dt.Rows[0]["CS_PHONO"].ToString();
                txt_HT_CZ.Text = dt.Rows[0]["CS_FAX"].ToString();
                txt_HT_KHYH.Text = dt.Rows[0]["CS_Bank"].ToString();
                txt_HT_ZH.Text = dt.Rows[0]["CS_Account"].ToString();
                txt_HT_SH.Text = dt.Rows[0]["CS_TAX"].ToString();
                txt_HT_YB.Text = dt.Rows[0]["CS_ZIP"].ToString();

                lb_HT_ZDR.Text = username;
                lb_HT_ZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txt_HT_XFHTBH.Text = GetHT_XFHTBH();
                Bindrpt();
            }
            else if (action == "bcxy")
            {
                string sql = "select * from PC_CGHT where HT_XFHTBH like '%" + hth + "%'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                DataRow dr = dt.Rows[0];
                txt_HT_GF.Text = dr["HT_GF"].ToString();
                txt_HT_DZ.Text = dr["HT_DZ"].ToString();
                txt_HT_FDDBR.Text = dr["HT_FDDBR"].ToString();
                txt_HT_DH.Text = dr["HT_DH"].ToString();
                txt_HT_CZ.Text = dr["HT_CZ"].ToString();
                txt_HT_KHYH.Text = dr["HT_KHYH"].ToString();
                txt_HT_ZH.Text = dr["HT_ZH"].ToString();
                txt_HT_SH.Text = dr["HT_SH"].ToString();
                txt_HT_YB.Text = dr["HT_YB"].ToString();

                lb_HT_ZDR.Text = username;
                lb_HT_ZDSJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txt_HT_XFHTBH.Text = hth + "-" + dt.Rows.Count.ToString();
            }
            else if (action == "check")
            {
                string sql = "select * from PC_CGHT where HT_ID='" + id + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                PanelDic.BindPanel(Panel1, dt);
                PanelDic.BindPanel(panSP, dt);
                Bindrpt();
                if (username == dt.Rows[0]["HT_SHR1"].ToString())
                {
                    lb_HT_SHR1_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (username == dt.Rows[0]["HT_SHR2"].ToString())
                {
                    lb_HT_SHR2_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (username == dt.Rows[0]["HT_SHR3"].ToString())
                {
                    lb_HT_SHR3_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (username == dt.Rows[0]["HT_SHRCG"].ToString())
                {
                    lb_HT_SHRCG_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (username == dt.Rows[0]["HT_SHRShenC"].ToString())
                {
                    lb_HT_SHRShenC_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (username == dt.Rows[0]["HT_SHRJS"].ToString())
                {
                    lb_HT_SHRJS_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (username == dt.Rows[0]["HT_SHRShiC"].ToString())
                {
                    lb_HT_SHRShiC_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (username == dt.Rows[0]["HT_SHRCW"].ToString())
                {
                    lb_HT_SHRCW_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (username == dt.Rows[0]["HT_SHRFZ"].ToString())
                {
                    lb_HT_SHRFZ_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                //质量
                if (username == dt.Rows[0]["HT_SHRZ"].ToString())
                {
                    lb_HT_SHRZ_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else if (action == "alter")
            {
                string sql = "select * from PC_CGHT where HT_ID='" + id + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                PanelDic.BindPanel(Panel1, dt);
                PanelDic.BindPanel(panSP, dt);
                Bindrpt();
            }
            else if (action == "read")
            {
                string sql = "select * from PC_CGHT where HT_ID='" + id + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                PanelDic.BindPanel(Panel1, dt);
                PanelDic.BindPanel(panSP, dt);
                Bindrpt();
                this.BindFP(id);
                this.BindFK(id);
            }
            txt_HT_GF1.Text = txt_HT_GF.Text;//这两个txt_共用一个字段
        }

        private void PowerControl()
        {
            btnSubmit.Visible = false;
            btnAdd.Visible = false;
            btnBack.Visible = false;
            btnZhuiJia.Visible = false;
            btnDelete.Visible = false;
            Panel1.Enabled = false;
            tb.Enabled = false;
            tb0.Enabled = false;
            tb1.Enabled = false;
            tb2.Enabled = false;
            tb3.Enabled = false;
            tb4.Enabled = false;
            tb5.Enabled = false;
            tb6.Enabled = false;
            tb7.Enabled = false;
            tb8.Enabled = false;
            tb9.Enabled = false;
            tb10.Enabled = false;
            if (action == "add" || action == "alter" || action == "add1" || action == "bcxy")
            {
                btnSubmit.Visible = true;
                btnAdd.Visible = true;
                btnBack.Visible = true;
                btnZhuiJia.Visible = true;
                btnDelete.Visible = true;
                Panel1.Enabled = true;
                tb.Enabled = true;
                tb0.Enabled = true;
                tb1.Enabled = true;
                tb2.Enabled = true;
                tb3.Enabled = true;
                tb4.Enabled = true;
                tb5.Enabled = true;
                tb6.Enabled = true;
                tb7.Enabled = true;
                tb8.Enabled = true;
                tb9.Enabled = true;
                tb10.Enabled = true;
                rbl_HT_SHR1_JL.Enabled = false;
                rbl_HT_SHR2_JL.Enabled = false;
                rbl_HT_SHR3_JL.Enabled = false;
                rbl_HT_SHRCG_JL.Enabled = false;
                rbl_HT_SHRCW_JL.Enabled = false;
                rbl_HT_SHRFZ_JL.Enabled = false;
                rbl_HT_SHRJS_JL.Enabled = false;
                rbl_HT_SHRShenC_JL.Enabled = false;
                rbl_HT_SHRShiC_JL.Enabled = false;
                rbl_HT_SHRZ_JL.Enabled = false;
            }
            else if (action == "check")
            {
                btnSubmit.Visible = true;
                btnBack.Visible = true;
                if (dts.Rows[0]["HT_SPLX"].ToString() != "4")
                {
                    if (username == dts.Rows[0]["HT_SHR1"].ToString())
                    {
                        tb1.Enabled = true;
                    }
                    else if (username == dts.Rows[0]["HT_SHR2"].ToString())
                    {
                        tb2.Enabled = true;
                    }
                    else if (username == dts.Rows[0]["HT_SHR3"].ToString())
                    {
                        tb3.Enabled = true;
                    }
                }
                else
                {
                    if (username == dts.Rows[0]["HT_SHRCG"].ToString())
                    {
                        tb4.Enabled = true;
                    }
                    else if (username == dts.Rows[0]["HT_SHRShenC"].ToString())
                    {
                        tb5.Enabled = true;
                    }
                    else if (username == dts.Rows[0]["HT_SHRJS"].ToString())
                    {
                        tb6.Enabled = true;
                    }
                    else if (username == dts.Rows[0]["HT_SHRShiC"].ToString())
                    {
                        tb7.Enabled = true;
                    }
                    else if (username == dts.Rows[0]["HT_SHRCW"].ToString())
                    {
                        tb8.Enabled = true;
                    }
                    else if (username == dts.Rows[0]["HT_SHRFZ"].ToString())
                    {
                        tb9.Enabled = true;
                    }
                    else if (username == dts.Rows[0]["HT_SHRZ"].ToString())
                    {
                        tb10.Enabled = true;
                    }
                }
            }
            //限制只有采购部长与老总可看合同总价
            if (Session["POSITION"].ToString().Trim() != "0501" && Session["POSITION"].ToString().Trim() != "0101" && Session["POSITION"].ToString().Trim() != "0102" && username != lb_HT_ZDR.Text && username != "李洪清" && Session["POSITION"].ToString().Trim() != "0401" && Session["POSITION"].ToString().Trim() != "0301" && Session["POSITION"].ToString().Trim() != "0601" && Session["POSITION"].ToString().Trim() != "0701")
            {
                txt_HT_HTZJ.Visible = false;
            }
        }

        private void Bindrpt()//绑定订单的repeater
        {
            if (action == "add1")
            {
                //2017.12修改，改成加板类型的6个合并
                string sql = "";
                sql = "select orderno,zdrnm,zdtime,PO_ZJE,marid,marnm,suppliernm,";
                sql += "PO_TUHAO= stuff((select '|'+PO_TUHAO from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as t where t.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and t.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and t.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,''),";
                sql += "margg,marcz,margb,PO_MASHAPE,sum(zxnum) as zxnum,marunit,sum(fznum)as fznum,marfzunit,";
                sql += "stuff((select '|'+convert(varchar(50),length)from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a where a.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and a.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and a.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE  FOR xml path('')),1,1,'') as length,";
                sql += "stuff((select '|'+convert(varchar(50),width) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as b where b.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and b.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and b.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,'') as width,";
                sql += "stuff((select '|'+convert(varchar(50),PO_PZ) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as c where c.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and c.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno  and c.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,'') as PO_PZ,";
                sql += "ctprice,sum(convert(float,ctamount))as ctamount,cgtimerq,";
                sql += "detailnote =stuff((select '|'+detailnote from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as d where d.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and d.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno  and d.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,''),";
                sql += "ptcode=stuff((select ' | '+ptcode from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as e where e.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and e.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and e.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,'')";
                sql += "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where orderno in (";
                if (orderno.Contains('|'))
                {
                    string[] ddbh = orderno.Split('|');
                    for (int i = 0, length = ddbh.Length; i < length; i++)
                    {
                        sql += "'" + ddbh[i] + "',";
                    }
                }
                else
                {
                    sql += "'" + orderno + "',";
                }
                sql = sql.Trim(',');
                sql += ") group by orderno,zdrnm,zdtime,suppliernm,PO_ZJE,marid,marnm,margg,marcz,margb,PO_MASHAPE,marunit,marfzunit,ctprice,cgtimerq order by orderno,ptcode";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    rptDD.DataSource = dt;
                    rptDD.DataBind();
                    rptControl();
                    NoDataPane.Visible = false;
                }
            }
            else
            {
                //2017.12修改，改成加板类型的6个合并
                string sql = "";
                string sql1 = "";
                sql1 = "select HT_DDBH,HT_DDBZ from PC_CGHT where HT_ID='" + id + "'";
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
                txt_HT_DDBZ.Text = dt1.Rows[0]["HT_DDBZ"].ToString();
                //sql = "select a.*,b.RESULT,d.PO_PZ from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a left join (select PTC,RESULT,ISAGAIN,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc) as rn from View_TBQM_APLYFORITEM) as c where rn<=1) as b on a.ptcode=b.PTC left join TBPC_PURORDERDETAIL as d on a.ptcode=d.PO_PCODE where ";
                sql = "select orderno,zdrnm,zdtime,PO_ZJE,marid,marnm,suppliernm,";
                sql += "PO_TUHAO= stuff((select '|'+PO_TUHAO from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as t where t.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and t.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno  and t.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,''),";
                sql += "margg,marcz,margb,PO_MASHAPE,sum(zxnum) as zxnum,marunit,sum(fznum)as fznum,marfzunit,";
                sql += "stuff((select '|'+convert(varchar(50),length)from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a where a.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and a.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and a.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,'') as length,";
                sql += "stuff((select '|'+convert(varchar(50),width) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as b where b.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and b.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and b.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,'') as width,";
                sql += "stuff((select '|'+convert(varchar(50),PO_PZ) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as c where c.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and c.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and c.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,'') as PO_PZ,";
                sql += "ctprice,sum(convert(float,ctamount))as ctamount,cgtimerq,";
                sql += "detailnote =stuff((select '|'+detailnote from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as d where d.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and d.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and d.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,''),";
                sql += "ptcode=stuff((select ' | '+ptcode from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as e where e.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and e.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and e.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,'')";
                sql += "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where ";
                if (dt1.Rows[0]["HT_DDBH"].ToString().Contains('|'))
                {
                    string[] pc_cght = dt1.Rows[0]["HT_DDBH"].ToString().Split('|');
                    for (int i = 0, length = pc_cght.Length; i < length; i++)
                    {
                        if (i == 0)
                        {
                            sql += " orderno in('" + pc_cght[i] + "'";
                        }
                        else
                        {
                            sql += ",'" + pc_cght[i] + "'";
                        }
                    }
                }
                else
                {
                    string pc_cght = dt1.Rows[0]["HT_DDBH"].ToString();
                    sql += " orderno in('" + pc_cght + "'";
                }
                sql += ") group by orderno,zdrnm,zdtime,suppliernm,PO_ZJE,marid,marnm,margg,marcz,margb,PO_MASHAPE,marunit,marfzunit,ctprice,cgtimerq order by orderno,ptcode";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    rptDD.DataSource = dt;
                    rptDD.DataBind();
                    rptControl();
                    NoDataPane.Visible = false;
                }
            }
        }

        private string GetHT_XFHTBH()//获取采购合同编号
        {
            string HT_XFHTBH = "";
            string sql = "select max(convert(int,substring(HT_XFHTBH,8,5)))  from PC_CGHT ";
            DataTable dt = new DataTable();
            try
            {
                dt = DBCallCommon.GetDTUsingSqlText(sql);
            }
            catch
            {
                Response.Write("<script>alert('数据库中存在不规范的合同编号！！！请与管理员联系')</script>");
            }
            string maxHT_XFHTBH = dt.Rows[0][0].ToString();
            string nowyear = DateTime.Now.Year.ToString().Substring(2, 2);
            if (maxHT_XFHTBH.Substring(0, 2) == nowyear)
            {
                HT_XFHTBH = (Convert.ToInt32(maxHT_XFHTBH) + 1).ToString();
            }
            else
            {
                HT_XFHTBH = nowyear + "000";
            }
            HT_XFHTBH = "TECTJCG" + HT_XFHTBH;
            return HT_XFHTBH;
        }


        #region 对订单的操作
        protected void btnAdd_OnClick(object sender, EventArgs e)//新增订单
        {
            string sql = "";
            if (rptDD.Items.Count > 0)
            {
                Response.Write("<script>alert('您已经添加了一项订单，如果还需要添加其他订单，请点击“追加订单”按钮！！！')</script>");
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您已经添加了一项订单，如果还需要添加其他订单，请点击“追加订单”按钮！！！')", true);
                return;
            }
            if (txt_orderno.Text.Trim() == "")
            {
                Response.Write("<script>alert('请您输入单据编号！！！')</script>");
                return;
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请您输入单据编号！！！')", true);
            }
            else if (txt_orderno.Text.Trim() != "")
            {
                if (AddControl(txt_orderno.Text.Trim()) == false)
                {
                    Response.Write("<script>alert('您输入的单据编号已经生成合同号，请重新输入！！！')</script>");
                    return;
                }
                //2017.12修改，改成加板类型的6个合并
                //sql = "select a.*,b.RESULT,d.PO_PZ from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a left join (select PTC,RESULT,ISAGAIN,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc) as rn from View_TBQM_APLYFORITEM) as c where rn<=1) as b on a.ptcode=b.PTC left join TBPC_PURORDERDETAIL as d on a.ptcode=d.PO_PCODE where orderno= '" + txt_orderno.Text.Trim() + "'";
                sql = "select orderno,zdrnm,zdtime,PO_ZJE,marid,marnm,suppliernm,";
                sql += "PO_TUHAO= stuff((select '|'+PO_TUHAO from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as t where t.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and t.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and t.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,''),";
                sql += "margg,marcz,margb,PO_MASHAPE,sum(zxnum) as zxnum,marunit,sum(fznum)as fznum,marfzunit,";
                sql += "stuff((select '|'+convert(varchar(50),length)from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a where a.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and a.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and a.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,'') as length,";
                sql += "stuff((select '|'+convert(varchar(50),width) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as b where b.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and b.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and b.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,'') as width,";
                sql += "stuff((select '|'+convert(varchar(50),PO_PZ) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as c where c.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and c.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and c.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,'') as PO_PZ,";
                sql += "ctprice,sum(convert(float,ctamount))as ctamount,cgtimerq,";
                sql += "detailnote =stuff((select '|'+detailnote from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as d where d.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and d.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and d.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,''),";
                sql += "ptcode=stuff((select ' | '+ptcode from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as e where e.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and e.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and e.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,'')";
                sql += "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where orderno='" + txt_orderno.Text.Trim() + "'group by orderno,zdrnm,zdtime,suppliernm,PO_ZJE,marid,marnm,margg,marcz,margb,PO_MASHAPE,marunit,marfzunit,ctprice,cgtimerq order by orderno,ptcode";
            }
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                rptDD.DataSource = dt;
                rptDD.DataBind();
                NoDataPane.Visible = false;
                rptControl();
            }
            else if (dt.Rows.Count == 0)
            {
                Response.Write("<script>alert('您输入的单据编号不存在，请重新输入！！！')</script>");
                return;
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您输入的单据编号不存在，请重新输入！！！');", true);
            }

        }

        protected void btnZhuiJia_OnClick(object sender, EventArgs e)//追加订单
        {
            string sql = "";
            List<string> lb_orderno = new List<string>();
            lb_orderno.Add(txt_orderno.Text.Trim());
            if (txt_orderno.Text.Trim() == "")
            {
                Response.Write("<script>alert('请您输入单据编号！！！')</script>");
                return;
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请您输入单据编号！！！')", true);
            }
            else
            {
                if (AddControl(txt_orderno.Text.Trim()) == false)
                {
                    Response.Write("<script>alert('您输入的单据编号已经生成合同号，请重新输入！！！')</script>");
                    return;
                }
                for (int i = 0, length = rptDD.Items.Count; i < length; i++)
                {
                    if (((Label)rptDD.Items[i].FindControl("lb_orderno")).Text == txt_orderno.Text.Trim())
                    {
                        Response.Write("<script>alert('您填写的单据编号已经添加至订单列表，请不要重复添加订单！！！')</script>");
                        return;
                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您填写的单据编号已经添加至订单列表，请不要重复添加订单！！！')", true);
                    }
                    if (i == 0)
                    {
                        lb_orderno.Add(((Label)rptDD.Items[i].FindControl("lb_orderno")).Text);
                    }
                    else
                    {
                        if (((Label)rptDD.Items[i].FindControl("lb_orderno")).Text != ((Label)rptDD.Items[i - 1].FindControl("lb_orderno")).Text)
                        {
                            lb_orderno.Add(((Label)rptDD.Items[i].FindControl("lb_orderno")).Text);
                        }
                    }
                }
                //2017.12修改，改成加板类型的6个合并
                //sql = "select a.*,b.RESULT,d.PO_PZ from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a left join (select PTC,RESULT,ISAGAIN,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc) as rn from View_TBQM_APLYFORITEM) as c where rn<=1) as b on a.ptcode=b.PTC left join TBPC_PURORDERDETAIL as d on a.ptcode=d.PO_PCODE where orderno= '" + lb_orderno[0] + "'";
                sql = "select orderno,zdrnm,zdtime,PO_ZJE,marid,marnm,suppliernm,";
                sql += "PO_TUHAO= stuff((select '|'+PO_TUHAO from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as t where t.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and t.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and t.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,''),";
                sql += "margg,marcz,margb,PO_MASHAPE,sum(zxnum) as zxnum,marunit,sum(fznum)as fznum,marfzunit,";
                sql += "stuff((select '|'+convert(varchar(50),length)from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a where a.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and a.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and a.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,'') as length,";
                sql += "stuff((select '|'+convert(varchar(50),width) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as b where b.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and b.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and b.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,'') as width,";
                sql += "stuff((select '|'+convert(varchar(50),PO_PZ) from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as c where c.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and c.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and c.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,'') as PO_PZ,";
                sql += "ctprice,sum(convert(float,ctamount))as ctamount,cgtimerq,";
                sql += "detailnote =stuff((select '|'+detailnote from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as d where d.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and d.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and d.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,''),";
                sql += "ptcode=stuff((select ' | '+ptcode from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as e where e.marid=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.marid and e.orderno=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.orderno and e.PO_MASHAPE=View_TBPC_PURORDERDETAIL_PLAN_TOTAL.PO_MASHAPE FOR xml path('')),1,1,'')";
                sql += "from View_TBPC_PURORDERDETAIL_PLAN_TOTAL ";
                sql += "where orderno in('" + lb_orderno[0] + "'";
                for (int i = 1, length = lb_orderno.Count; i < length; i++)
                {
                    sql += ",'" + lb_orderno[i] + "'";
                }
                sql += ") group by orderno,zdrnm,zdtime,suppliernm,PO_ZJE,marid,marnm,margg,marcz,margb,PO_MASHAPE,marunit,marfzunit,ctprice,cgtimerq order by orderno,ptcode";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1, length = dt.Rows.Count; i < length; i++)
                    {
                        if (dt.Rows[i]["suppliernm"].ToString() != dt.Rows[i - 1]["suppliernm"].ToString())
                        {
                            Response.Write("<script>alert('您选择的订单的供应商不一致，不能追加！！！')</script>");
                            return;
                            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您选择的订单的供应商不一致，不能追加！！！')", true);
                        }
                    }
                    rptDD.DataSource = dt;
                    rptDD.DataBind();
                    rptControl();
                    NoDataPane.Visible = false;
                }
            }
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)//删除订单
        {
            string sql = "";
            List<string> lb_orderno = new List<string>();
            if (txt_orderno.Text.Trim() == "")
            {
                Response.Write("<script>alert('请您输入单据编号！！！')</script>");
                return;
            }
            else
            {
                if (rptDD.Items.Count == 0)
                {
                    Response.Write("<script>alert('还未添加单据，不能删除！！！')</script>");
                    return;
                }
                else
                {
                    if (((Label)rptDD.Items[0].FindControl("lb_orderno")).Text != txt_orderno.Text.Trim())
                    {
                        lb_orderno.Add(((Label)rptDD.Items[0].FindControl("lb_orderno")).Text);
                    }
                    for (int i = 1, length = rptDD.Items.Count; i < length; i++)
                    {
                        if (((Label)rptDD.Items[i].FindControl("lb_orderno")).Text != txt_orderno.Text.Trim() && ((Label)rptDD.Items[i].FindControl("lb_orderno")).Text != ((Label)rptDD.Items[i - 1].FindControl("lb_orderno")).Text)
                        {
                            lb_orderno.Add(((Label)rptDD.Items[i].FindControl("lb_orderno")).Text);
                        }
                    }
                }

            }
            if (lb_orderno.Count > 0)
            {
                //sql = "select a.*,b.RESULT,d.PO_PZ from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a left join (select PTC,RESULT,ISAGAIN,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc) as rn from View_TBQM_APLYFORITEM) as c where rn<=1) as b on a.ptcode=b.PTC left join TBPC_PURORDERDETAIL as d on a.ptcode=d.PO_PCODE where orderno= '" + lb_orderno[0] + "'";
                sql = "select * from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where orderno= '" + lb_orderno[0] + "'";

                for (int i = 1, length = lb_orderno.Count; i < length; i++)
                {
                    sql += " or orderno='" + lb_orderno[i] + "'";
                }
                sql += " order by orderno";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);

                rptDD.DataSource = dt;
                rptDD.DataBind();
                rptControl();
                NoDataPane.Visible = false;
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Clear();
                rptDD.DataSource = dt;
                rptDD.DataBind();
                NoDataPane.Visible = true;
            }

        }

        private bool AddControl(string ddnum)//判断合同号是否添加过该订单
        {
            bool add = true;
            string sql = "";
            sql = "select HT_ID,HT_XFHTBH,HT_DDBH from PC_CGHT";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                string[] sqltext = dt.Rows[i]["HT_DDBH"].ToString().Split('|');
                if (sqltext.Contains(ddnum))
                {
                    add = false;
                    break;
                }
            }
            return add;
        }

        private void rptControl()//控制repeater的汇总和影藏项
        {
            decimal number = 0;
            decimal money = 0;
            if (rptDD.Items.Count > 0)
            {
                foreach (RepeaterItem item in rptDD.Items)
                {
                    number += Convert.ToDecimal(((Label)item.FindControl("lb_zxnum")).Text.Trim());
                    money += Convert.ToDecimal(((Label)item.FindControl("lb_ctamount")).Text.Trim());
                }
                foreach (RepeaterItem item in rptDD.Controls)//汇总
                {
                    if (item.ItemType == ListItemType.Footer)
                    {
                        ((Label)item.FindControl("lb_HZSL")).Text = Math.Round(number, 4, MidpointRounding.AwayFromZero).ToString();
                        ((Label)item.FindControl("lb_HZJE")).Text = Math.Round(money, 2, MidpointRounding.AwayFromZero).ToString();
                        break;
                    }
                }

                foreach (RepeaterItem item in rptDD.Controls)
                {
                    if (item.ItemType == ListItemType.Item)
                    {
                        if (username != "高浩" && username != "王福泉" && username != "姜中毅" && username != "王自清" && username != "周文轶" && username != lb_HT_ZDR.Text && username != "李洪清" && username != "于来义" && username != "曹卫亮" && username != "叶宝松" && username != "李利恒" && username != "陈永秀" && username != "李小婷")
                        {
                            ((Label)item.FindControl("lb_ctprice")).Visible = false;
                            ((Label)item.FindControl("lb_ctamount")).Visible = false;
                        }
                    }

                    if (item.ItemType == ListItemType.Footer)
                    {
                        //限制只有采购部长与老总可看合同价格
                        if (username != "高浩" && username != "王福泉" && username != "姜中毅" && username != "王自清" && username != "周文轶" && username != lb_HT_ZDR.Text && username != "李洪清" && username != "于来义" && username != "曹卫亮" && username != "叶宝松" && username != "李利恒" && username != "陈永秀" && username != "李小婷")
                        {
                            ((Label)item.FindControl("lb_HZSL")).Visible = false;
                            ((Label)item.FindControl("lb_HZJE")).Visible = false;
                        }
                    }

                }

                for (int i = 0, length = rptDD.Items.Count; i < length; i++)//隐藏重复的项
                {
                    if (i > 0)
                    {
                        if (((Label)rptDD.Items[i].FindControl("lb_orderno")).Text == ((Label)rptDD.Items[i - 1].FindControl("lb_orderno")).Text)
                        {
                            ((Label)rptDD.Items[i].FindControl("lb_orderno")).Visible = false;
                            ((Label)rptDD.Items[i].FindControl("lb_zdrnm")).Visible = false;
                            ((Label)rptDD.Items[i].FindControl("lb_zdtime")).Visible = false;
                            ((Label)rptDD.Items[i].FindControl("lb_PO_ZJE")).Visible = false;
                        }
                    }
                }
            }

        }

        #endregion

        protected void btnSubmit_OnClick(object sender, EventArgs e)//********************提交*******************
        {
            int a = SubmitControl();
            if (a == 1)
            {
                Response.Write("<script type='text/javascript'>alert('您还未选择审批人，请先选择审批人后再提交！！！')</script>");
                return;
            }
            else if (a == 2)
            {
                Response.Write("<script type='text/javascript'>alert('请选择“同意”或“不同意”后再提交！！！')</script>");
                return;
            }
            if (action == "add")
            {
                txt_HT_XFHTBH.Text = GetHT_XFHTBH();
                string sql = Add();
                try
                {
                    DBCallCommon.ExeSqlText(sql);

                    //邮件提醒
                    if (rbl_HT_SPLX.SelectedValue == "4")
                    {
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sptitle = "采购合同审批";
                        spcontent = "合同号为" + txt_HT_XFHTBH.Text.Trim() + "的采购合同需要您审批，请登录查看！";

                        string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRCG.Text.Trim() + "'";
                        System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRShenC.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRJS.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRShiC.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRCW.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                        //质量
                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRZ.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                    else
                    {
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHR1.Text.Trim() + "'";
                        System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        sptitle = "采购合同审批";
                        spcontent = "合同号为" + txt_HT_XFHTBH.Text.Trim() + "的采购合同需要您审批，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('Addsql语句出现'" + sql + "'问题,请与管理员联系')</script>");
                    return;
                }
            }
            else if (action == "add1")
            {
                txt_HT_XFHTBH.Text = GetHT_XFHTBH();
                string sql = Add();
                try
                {
                    DBCallCommon.ExeSqlText(sql);

                    //邮件提醒
                    if (rbl_HT_SPLX.SelectedValue == "4")
                    {
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sptitle = "采购合同审批";
                        spcontent = "合同号为" + txt_HT_XFHTBH.Text.Trim() + "的采购合同需要您审批，请登录查看！";

                        string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRCG.Text.Trim() + "'";
                        System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRShenC.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRJS.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRShiC.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRCW.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        //质量
                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRZ.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                    else
                    {
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHR1.Text.Trim() + "'";
                        System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        sptitle = "采购合同审批";
                        spcontent = "合同号为" + txt_HT_XFHTBH.Text.Trim() + "的采购合同需要您审批，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('Add1sql语句出现'" + sql + "'问题,请与管理员联系')</script>");
                    return;
                }
            }
            else if (action == "bcxy")
            {
                string sql = Add();
                try
                {
                    DBCallCommon.ExeSqlText(sql);

                    //邮件提醒
                    if (rbl_HT_SPLX.SelectedValue == "4")
                    {
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sptitle = "采购合同审批";
                        spcontent = "合同号为" + txt_HT_XFHTBH.Text.Trim() + "的采购合同需要您审批，请登录查看！";

                        string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRCG.Text.Trim() + "'";
                        System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRShenC.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRJS.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRShiC.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRCW.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        //质量
                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRZ.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                    else
                    {
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHR1.Text.Trim() + "'";
                        System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        sptitle = "采购合同审批";
                        spcontent = "合同号为" + txt_HT_XFHTBH.Text.Trim() + "的采购合同需要您审批，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('bcxysql语句出现'" + sql + "'问题,请与管理员联系')</script>");
                    return;
                }
            }
            else if (action == "check")
            {
                List<string> list = Check();
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                }
                catch
                {
                    Response.Write("<script>alert('sql语句出现问题，请联系管理员，Check()出现了问题')</script>");
                    return;
                }
            }
            else if (action == "alter")
            {
                string sql = Alter();
                try
                {
                    DBCallCommon.ExeSqlText(sql);

                    //邮件提醒
                    if (rbl_HT_SPLX.SelectedValue == "4")
                    {
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        sptitle = "采购合同审批";
                        spcontent = "合同号为" + txt_HT_XFHTBH.Text.Trim() + "的采购合同需要您审批，请登录查看！";

                        string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRCG.Text.Trim() + "'";
                        System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRShenC.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRJS.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRShiC.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRCW.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                        //质量
                        sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRZ.Text.Trim() + "'";
                        dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                    else
                    {
                        string sprid = "";
                        string sptitle = "";
                        string spcontent = "";
                        string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHR1.Text.Trim() + "'";
                        System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                        if (dtgetstid.Rows.Count > 0)
                        {
                            sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                        }
                        sptitle = "采购合同审批";
                        spcontent = "合同号为" + txt_HT_XFHTBH.Text.Trim() + "的采购合同需要您审批，请登录查看！";
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                    }
                }
                catch (Exception)
                {

                    Response.Write("<script>alert('修改时sql语句出现问题'" + sql + "',请与管理员联系')</script>");
                    return;
                }
                Response.Write("<script>alert('已修改成功！！！！')</script>");
            }

            //判断是否为集采
            if (rptDD.Items.Count > 0)
            {
                string dingdancode="";
                for (int i = 1; i < rptDD.Items.Count; i++)//取到所有的订单编号
                {
                    if (((Label)rptDD.Items[i].FindControl("lb_orderno")).Text.Trim() != "")
                    {
                        if (dingdancode == "")
                        {
                            dingdancode = ((Label)rptDD.Items[i].FindControl("lb_orderno")).Text.Trim();
                        }
                    }
                }
                string sqlcheckifjc = "select * from (select t.*,s.ICL_TYPE from ((select a.*,b.RESULT from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a left join (select PTC,RESULT,ISAGAIN,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc) as rn from View_TBQM_APLYFORITEM) as c where rn<=1) as b on a.ptcode=b.PTC)t left join (select * from TBPC_IQRCMPPRCRVW as m left join TBPC_IQRCMPPRICE as n on m.ICL_SHEETNO=n.PIC_SHEETNO)s on t.ptcode=s.PIC_PTCODE) where s.PIC_PTCODE is not null)q where orderno='" + dingdancode + "' and ICL_TYPE='1'";
                DataTable dtcheckifjc = DBCallCommon.GetDTUsingSqlText(sqlcheckifjc);
                List<string> listupdatejc = new List<string>();
                if (dtcheckifjc.Rows.Count > 0)
                {
                    string sqlupdatejc = "update PC_CGHT set HT_JCZT ='y' where HT_XFHTBH='" + txt_HT_XFHTBH.Text.Trim() + "'";
                    listupdatejc.Add(sqlupdatejc);
                    DBCallCommon.ExecuteTrans(listupdatejc);
                }
            }


            SendMail();
            Response.Redirect("PC_CGHTGL.aspx");
        }

        private string Add()//**********增加合同*********
        {
            string key = "";
            string value = "";
            string sqltext = "";
            Dictionary<string, string> dic1 = PanelDic.DicPan(Panel1, "PC_CGHT", new Dictionary<string, string>());
            Dictionary<string, string> dic2 = PanelDic.DicPan(panSP, "PC_CGHT", new Dictionary<string, string>());
            foreach (KeyValuePair<string, string> pair in dic1)
            {
                key += pair.Key.ToString() + ",";
                value += "'" + pair.Value.ToString() + "',";
            }
            foreach (KeyValuePair<string, string> pair in dic2)
            {
                key += pair.Key.ToString() + ",";
                value += "'" + pair.Value.ToString() + "',";
            }
            if (rptDD.Items.Count > 0)
            {
                key += "HT_DDBH,";
                value += "'" + ((Label)rptDD.Items[0].FindControl("lb_orderno")).Text;
                for (int i = 1, length = rptDD.Items.Count; i < length; i++)//取到所有的订单编号
                {
                    if (((Label)rptDD.Items[i].FindControl("lb_orderno")).Text != ((Label)rptDD.Items[i - 1].FindControl("lb_orderno")).Text)
                    {
                        value += "|" + ((Label)rptDD.Items[i].FindControl("lb_orderno")).Text;
                    }
                }
                value += "',";
            }
            key += "HT_DDBZ,";
            value += "'" + txt_HT_DDBZ.Text.Trim() + "',";
            key += "HT_SPZT";//审批总状态0-待审，1-审批中，2-通过，3-驳回
            value += "'0'";
            sqltext = "insert into PC_CGHT (" + key + ") values (" + value + ")";
            return sqltext;
        }

        private string Alter() //**********修改**********
        {
            string key = "";
            string value = "";
            string sqltext = "";
            sqltext = "update PC_CGHT set ";
            Dictionary<string, string> dic1 = PanelDic.DicPan(Panel1, "PC_CGHT", new Dictionary<string, string>());
            Dictionary<string, string> dic2 = PanelDic.DicPan(panSP, "PC_CGHT", new Dictionary<string, string>());
            foreach (KeyValuePair<string, string> pair in dic1)
            {
                sqltext += pair.Key.ToString() + "='" + pair.Value.ToString() + "',";
            }
            foreach (KeyValuePair<string, string> pair in dic2)
            {
                sqltext += pair.Key.ToString() + "='" + pair.Value.ToString() + "',";
            }
            if (rptDD.Items.Count > 0)
            {
                key += "HT_DDBH";
                value += ((Label)rptDD.Items[0].FindControl("lb_orderno")).Text;
                for (int i = 1, length = rptDD.Items.Count; i < length; i++)//取到所有的订单编号
                {
                    if (((Label)rptDD.Items[i].FindControl("lb_orderno")).Text != ((Label)rptDD.Items[i - 1].FindControl("lb_orderno")).Text)
                    {
                        value += "|" + ((Label)rptDD.Items[i].FindControl("lb_orderno")).Text;
                    }
                }
                sqltext += key + "='" + value + "',";
            }
            if (rptDD.Items.Count == 0)
            {
                key += "HT_DDBH";
                value += "";
                sqltext += key + "='" + value + "',";
            }
            sqltext = sqltext.Trim(',');
            sqltext += ",HT_DDBZ='" + txt_HT_DDBZ.Text.Trim() + "' where HT_ID='" + id + "'";
            return sqltext;
        }

        private List<string> Check()//**********审核**********
        {
            Dictionary<string, string> dic = PanelDic.DicPan(panSP, "PC_CGHT", new Dictionary<string, string>());
            List<string> list = new List<string>();
            string sql = "";
            sql = "update PC_CGHT set ";
            foreach (KeyValuePair<string, string> pair in dic)
            {
                sql += pair.Key + "='" + pair.Value + "',";
            }
            sql = sql.Trim(',');
            sql += " where HT_ID='" + id + "'";
            list.Add(sql);
            if (rbl_HT_SPLX.SelectedValue == "1")
            {
                if (rbl_HT_SHR1_JL.SelectedValue == "y" && Session["UserName"].ToString().Trim() == txt_HT_SHR1.Text.Trim())
                {
                    sql = "update PC_CGHT set HT_SPZT='y' where HT_ID='" + id + "'";
                    list.Add(sql);
                }
            }
            else if (rbl_HT_SPLX.SelectedValue == "2")
            {
                if (rbl_HT_SHR1_JL.SelectedValue == "y" && Session["UserName"].ToString().Trim() == txt_HT_SHR1.Text.Trim())
                {
                    sql = "update PC_CGHT set HT_SPZT='1y' where HT_ID='" + id + "'";
                    list.Add(sql);
                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sptitle = "采购合同审批";
                    spcontent = "合同号为" + txt_HT_XFHTBH.Text.Trim() + "的采购合同需要您审批，请登录查看！";
                    string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHR2.Text.Trim() + "'";
                    System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                    if (dtgetstid.Rows.Count > 0)
                    {
                        sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                    }
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                }
                if (rbl_HT_SHR2_JL.SelectedValue == "y" && Session["UserName"].ToString().Trim() == txt_HT_SHR2.Text.Trim())
                {
                    sql = "update PC_CGHT set HT_SPZT='y' where HT_ID='" + id + "'";
                    list.Add(sql);
                }
            }
            else if (rbl_HT_SPLX.SelectedValue == "3")
            {
                if (rbl_HT_SHR1_JL.SelectedValue == "y" && Session["UserName"].ToString().Trim() == txt_HT_SHR1.Text.Trim())
                {
                    sql = "update PC_CGHT set HT_SPZT='1y' where HT_ID='" + id + "'";
                    list.Add(sql);
                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sptitle = "采购合同审批";
                    spcontent = "合同号为" + txt_HT_XFHTBH.Text.Trim() + "的采购合同需要您审批，请登录查看！";
                    string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHR2.Text.Trim() + "'";
                    System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                    if (dtgetstid.Rows.Count > 0)
                    {
                        sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                    }
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                }
                if (rbl_HT_SHR2_JL.SelectedValue == "y" && Session["UserName"].ToString().Trim() == txt_HT_SHR2.Text.Trim())
                {
                    sql = "update PC_CGHT set HT_SPZT='2y' where HT_ID='" + id + "'";
                    list.Add(sql);
                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sptitle = "采购合同审批";
                    spcontent = "合同号为" + txt_HT_XFHTBH.Text.Trim() + "的采购合同需要您审批，请登录查看！";
                    string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHR3.Text.Trim() + "'";
                    System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                    if (dtgetstid.Rows.Count > 0)
                    {
                        sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                    }
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                }
                if (rbl_HT_SHR3_JL.SelectedValue == "y" && Session["UserName"].ToString().Trim() == txt_HT_SHR3.Text.Trim())
                {
                    sql = "update PC_CGHT set HT_SPZT='y' where HT_ID='" + id + "'";
                    list.Add(sql);
                }
            }
            else if (rbl_HT_SPLX.SelectedValue == "4")
            {
                if (rbl_HT_SHRCG_JL.SelectedValue == "y" && rbl_HT_SHRShenC_JL.SelectedValue == "y" && rbl_HT_SHRJS_JL.SelectedValue == "y" && rbl_HT_SHRShiC_JL.SelectedValue == "y" && rbl_HT_SHRCW_JL.SelectedValue == "y" && rbl_HT_SHRZ_JL.SelectedValue == "y" && Session["UserName"].ToString().Trim() != txt_HT_SHRFZ.Text.Trim())//质量
                {
                    sql = "update PC_CGHT set HT_SPZT='5y' where HT_ID='" + id + "'";
                    list.Add(sql);
                    //邮件提醒
                    string sprid = "";
                    string sptitle = "";
                    string spcontent = "";
                    sptitle = "采购合同审批";
                    spcontent = "合同号为" + txt_HT_XFHTBH.Text.Trim() + "的采购合同需要您审批，请登录查看！";
                    string sqlgetstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + txt_HT_SHRFZ.Text.Trim() + "'";
                    System.Data.DataTable dtgetstid = DBCallCommon.GetDTUsingSqlText(sqlgetstid);
                    if (dtgetstid.Rows.Count > 0)
                    {
                        sprid = dtgetstid.Rows[0]["ST_ID"].ToString().Trim();
                    }
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                }
                if (rbl_HT_SHRFZ_JL.SelectedValue == "y")
                {
                    sql = "update PC_CGHT set HT_SPZT='y' where HT_ID='" + id + "'";
                    list.Add(sql);
                }
            }
            if (rbl_HT_SHR1_JL.SelectedValue == "n" || rbl_HT_SHR2_JL.SelectedValue == "n" || rbl_HT_SHR3_JL.SelectedValue == "n" || rbl_HT_SHRCG_JL.SelectedValue == "n" || rbl_HT_SHRShenC_JL.SelectedValue == "n" || rbl_HT_SHRJS_JL.SelectedValue == "n" || rbl_HT_SHRShiC_JL.SelectedValue == "n" || rbl_HT_SHRCW_JL.SelectedValue == "n" || rbl_HT_SHRFZ_JL.SelectedValue == "n")
            {
                sql = "update PC_CGHT set HT_SPZT='n' where HT_ID='" + id + "'";
                list.Add(sql);
            }
            return list;
        }


        private int SubmitControl()//控制能够提交的条件
        {
            int a = 0;
            if (action == "add" || action == "alter" || action == "add1")
            {
                string rbl_ = rbl_HT_SPLX.SelectedValue;
                if (rbl_ != "1" && rbl_ != "2" && rbl_ != "3" && rbl_ != "4")
                {
                    a = 1;
                }
            }
            if (action == "check")
            {
                string sql1 = "select * from PC_CGHT where HT_ID='" + id + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
                DataRow dr = dt.Rows[0];
                if (rbl_HT_SPLX.SelectedValue == "1")
                {
                    if (dr["HT_SHR1"].ToString() == username)
                    {
                        if (rbl_HT_SHR1_JL.SelectedValue != "y" && rbl_HT_SHR1_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                }
                if (rbl_HT_SPLX.SelectedValue == "2")
                {
                    if (dr["HT_SHR1"].ToString() == username)
                    {
                        if (rbl_HT_SHR1_JL.SelectedValue != "y" && rbl_HT_SHR1_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                    else if (dr["HT_SHR2"].ToString() == username)
                    {
                        if (rbl_HT_SHR2_JL.SelectedValue != "y" && rbl_HT_SHR2_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                }
                if (rbl_HT_SPLX.SelectedValue == "3")
                {
                    if (dr["HT_SHR1"].ToString() == username)
                    {
                        if (rbl_HT_SHR1_JL.SelectedValue != "y" && rbl_HT_SHR1_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                    else if (dr["HT_SHR2"].ToString() == username)
                    {
                        if (rbl_HT_SHR2_JL.SelectedValue != "y" && rbl_HT_SHR2_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                    else if (dr["HT_SHR3"].ToString() == username)
                    {
                        if (rbl_HT_SHR3_JL.SelectedValue != "y" && rbl_HT_SHR3_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                }
                if (rbl_HT_SPLX.SelectedValue=="4")
                {
                    if (dr["HT_SHRCG"].ToString() == username)
                    {
                        if (rbl_HT_SHRCG_JL.SelectedValue != "y" && rbl_HT_SHRCG_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                    else if (dr["HT_SHRShenC"].ToString() == username)
                    {
                        if (rbl_HT_SHRShenC_JL.SelectedValue != "y" && rbl_HT_SHRShenC_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                    else if (dr["HT_SHRJS"].ToString() == username)
                    {
                        if (rbl_HT_SHRJS_JL.SelectedValue != "y" && rbl_HT_SHRJS_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                    else if (dr["HT_SHRShiC"].ToString() == username)
                    {
                        if (rbl_HT_SHRShiC_JL.SelectedValue != "y" && rbl_HT_SHRShiC_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                    else if (dr["HT_SHRCW"].ToString() == username)
                    {
                        if (rbl_HT_SHRCW_JL.SelectedValue != "y" && rbl_HT_SHRCW_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                    else if (dr["HT_SHRFZ"].ToString() == username)
                    {
                        if (rbl_HT_SHRFZ_JL.SelectedValue != "y" && rbl_HT_SHRFZ_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    }
                    else if (dr["HT_SHRZ"].ToString() == username)
                    {
                        if (rbl_HT_SHRZ_JL.SelectedValue != "y" && rbl_HT_SHRZ_JL.SelectedValue != "n")
                        {
                            a = 2;
                        }
                    } 
                }
            }
            return a;
        }

        private void SendMail()//************发送邮件**********
        {
            //string a = hidSHR1.Value;
            //string b = hidSHR2.Value;
            //string c = hidSHR3.Value;
            // 72-高浩 2-王福泉 1-周文轶 47-李立恒 95-于来义 146-曹卫亮 256-叶宝松

        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("PC_CGHTGL.aspx");
        }


        #region 发票管理
        private void BindFP(string id)
        {
            string sqlstr = "select BR_ID, BR_HTBH, BR_ENGNAME, BR_KPJE, BR_SL, BR_FPDH, BR_KPRQ, BR_LPRQ, BR_JBR, BR_BZ from TBPC_PURBILLRECORD as a left join PC_CGHT as b on a.BR_HTBH=b.HT_XFHTBH  where b.HT_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlstr);
            if (dt.Rows.Count > 0)
            {
                grvFP.DataSource = dt;
                grvFP.DataBind();
                palFPJL.Visible = false;
            }
            else
            {
                grvFP.DataSource = null;
                grvFP.DataBind();
                palFPJL.Visible = true;
            }
        }

        private void BindFK(string id)
        {
            string sqlstr = "select a.* from TBPC_PURPAYMENTRECORD as a left join PC_CGHT as b on a.BP_HTBH=b.HT_XFHTBH where b.HT_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlstr);
            if (dt.Rows.Count > 0)
            {
                grvYKJL.DataSource = dt;
                grvYKJL.DataBind();
            }
        }

        //删除要款
        protected void Lbtn_Del_OnClick(object sender, EventArgs e)
        {
            List<string> sqlstr = new List<string>();
            string bp_id = ((LinkButton)sender).CommandArgument.ToString();


            string str_del = "delete from TBPC_PURPAYMENTRECORD WHERE BP_ID='" + bp_id + "'";
            sqlstr.Add(str_del);
            DBCallCommon.ExecuteTrans(sqlstr);
            BindFK(id);

        }
        //删除发票        
        protected void linkdel_FP_Click(object sender, EventArgs e)
        {
            string br_id = ((LinkButton)sender).CommandArgument.ToString();
            string sql_del = "DELETE FROM TBPC_PURBILLRECORD WHERE BR_ID='" + br_id + "'";
            DBCallCommon.ExeSqlText(sql_del);
            this.BindFP(id);
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('删除成功！');", true); return;

        }

        //绑定补充协议
        private void bindBCXY()
        {
            string sql = "select * from PC_CGHT where HT_XFHTBH<>'" + dts.Rows[0]["HT_XFHTBH"].ToString().Substring(0, 12) + "' and HT_XFHTBH like '%" + dts.Rows[0]["HT_XFHTBH"].ToString().Substring(0, 12) + "%' and HT_SPZT='y'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                gvBCXY.DataSource = dt;
                gvBCXY.DataBind();
            }
        }

        #endregion
    }
}
