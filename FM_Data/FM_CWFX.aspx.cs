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
using System.Collections.Generic;
using System.Data.SqlClient;
using ZCZJ_DPF;
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.FM_Data
{

    public partial class FM_CWFX : BasicPage
    {
        string sqltext11 = "";
        string sqltext12 = "";
        string sqltext21 = "";
        string sqltext22 = "";
        string sqltext31 = "";
        string sqltext32 = "";
        string sqltext = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                this.InitPage();
            }
            CheckUser(ControlFinder);
        }
        //初始化页面
        private void InitPage()
        {
            //显示当月
            dplYear.ClearSelection();
            foreach (ListItem li in dplYear.Items)
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }
            dplMoth.ClearSelection();
            string month = (DateTime.Now.Month - 1).ToString();
            if (DateTime.Now.Month < 10)
            {
                month = "0" + month;
            }
            foreach (ListItem li in dplMoth.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
            sqltext = "select * from TBFM_CWFX where RQBH like '" + DateTime.Now.Year.ToString() + "-" + month + "%' order by RQBH";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count == 0)
            {
                palNoData.Visible = true;
            }
            else
            {
                palNoData.Visible = false;
            }
                rptProNumCost.DataSource = dt;
                rptProNumCost.DataBind();
        }//
        
        
        
        //修改查看
        protected string editDq(string DqId)
        {
            return "javascript:window.showModalDialog('FM_CWFX_Detail.aspx?action=update&id=" + DqId + "','','DialogWidth=650px;DialogHeight=400px')";
        }
        protected string viewDq(string DqId)
        {
            return "javascript:window.showModalDialog('FM_CWFX_Detail.aspx?action=look&id=" + DqId + "','','DialogWidth=650px;DialogHeight=400px')";
        }//





        private void BindYearMoth(DropDownList dpl_Year, DropDownList dpl_Month)
        {
            for (int i = 0; i < 15; i++)
            {
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                dpl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            dpl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }
        /// <summary>
        /// 年、月份改变 查询
        /// </summary>
        //读取数据
        protected void OnSelectedIndexChanged_dplYearMoth(object sender, EventArgs e)
        {
            rptProNumCost.DataSource = null;
            rptProNumCost.DataBind();
            this.bindGrid();   
        }

        private void bindGrid()
        {
            string sqltextbind = "";
            string year = "";
            string month = "";
            if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex == 0)
            {
                Response.Write("<script>alert('请选择年月份！')</script>");
                return;
            }
            else if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex == 0)
            {
                year = dplYear.SelectedValue.ToString();
                sqltextbind = "select * from TBFM_CWFX where RQBH like'" + year + "%'";
            }
            else if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex != 0)
            {
                month = dplMoth.SelectedValue.ToString();
                sqltextbind = "select * from TBFM_CWFX where RQBH like'%" + month + "%'";
            }
            else
            {
                year = dplYear.SelectedValue.ToString();
                month = dplMoth.SelectedValue.ToString();
                sqltextbind = "select * from TBFM_CWFX where RQBH like'" + year + "-" + month + "%' order by RQBH";
            }
            System.Data.DataTable dtbind = DBCallCommon.GetDTUsingSqlText(sqltextbind);
            if (dtbind.Rows.Count == 0)
            {
                palNoData.Visible = true;
            }
            else
            {
                palNoData.Visible = false;
            }
                rptProNumCost.DataSource = dtbind;
                rptProNumCost.DataBind();
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                string year = DateTime.Now.AddYears(-i).Year.ToString();
                string yearsn = DateTime.Now.AddYears(-(i + 1)).Year.ToString();
                for (int k = 1; k < 13; k++)
                {
                    string j = k.ToString();
                    if (k < 10)
                    {
                        j = "0" + k.ToString();
                    }
                    //资产负债表的年初数查询
                    sqltext11 = "select * from TBFM_ZCFZ where RQBH like '" + year + "-" + j.ToString() + "%' and ZCFZ_TYPE ='年初数'";
                    System.Data.DataTable dt11 = DBCallCommon.GetDTUsingSqlText(sqltext11);
                    
                    //资产负债表的期末数查询
                    sqltext12 = "select * from TBFM_ZCFZ where RQBH like '" + year + "-" + j.ToString() + "%' and ZCFZ_TYPE ='期末数'";
                    System.Data.DataTable dt12 = DBCallCommon.GetDTUsingSqlText(sqltext12);
                    
                    //利润分配表的本期数查询
                    sqltext21 = "select * from TBFM_LRFP where RQBH like '" + year + "-" + j.ToString() + "%' and LRFP_TYPE='本期数'";
                    System.Data.DataTable dt21 = DBCallCommon.GetDTUsingSqlText(sqltext21);
                    
                    //利润分配表的本年累计数查询
                    sqltext22 = "select * from TBFM_LRFP where RQBH like '" + year + "-" + j.ToString() + "%' and LRFP_TYPE='本年累计数'";
                    System.Data.DataTable dt22 = DBCallCommon.GetDTUsingSqlText(sqltext22);
                    
                    //现金流量表的本期数查询
                    sqltext31 = "select * from TBFM_XJLL where RQBH like '" + year + "-" + j.ToString() + "%' and XJLL_TYPE='上月累计'";
                    System.Data.DataTable dt31 = DBCallCommon.GetDTUsingSqlText(sqltext31);
                    
                    //现金流量表的期末数查询
                    sqltext32 = "select * from TBFM_XJLL where RQBH like '" + year + "-" + j.ToString() + "%' and XJLL_TYPE='本年累计数'";
                    System.Data.DataTable dt32 = DBCallCommon.GetDTUsingSqlText(sqltext32);
                    


                    if (dt11.Rows.Count != 0 && dt12.Rows.Count != 0 && dt21.Rows.Count != 0 && dt22.Rows.Count != 0 && dt31.Rows.Count != 0 && dt32.Rows.Count != 0)
                    {
                        DataRow dr11 = dt11.Rows[0];
                        DataRow dr12 = dt12.Rows[0];
                        DataRow dr21 = dt21.Rows[0];
                        DataRow dr22 = dt22.Rows[0];
                        DataRow dr31 = dt31.Rows[0];
                        DataRow dr32 = dt32.Rows[0];

                        string rqbhtb = year + "-" + j.ToString();
                        string sqlTextisexist = "select * from TBFM_CWFX where RQBH like'" + rqbhtb + "%'";
                        System.Data.DataTable dtexist = DBCallCommon.GetDTUsingSqlText(sqlTextisexist);

                        //定义中间字符串，用于给财务分析表中的字段赋值
                        string strCWFX_xsjl = "";
                        string strCWFX_zcjl = "";
                        string strCWFX_xsml = "";
                        string strCWFX_qbhs = "";
                        string strCWFX_ylxj = "";
                        string strCWFX_xssx = "";
                        string strCWFX_jyzb = "";
                        string strCWFX_ldbl = "";
                        string strCWFX_sdbl = "";
                        string strCWFX_xjbl = "";


                        string strCWFX_xjgf = "";


                        string strCWFX_xjll = "";
                        string strCWFX_xjzz = "";
                        string strCWFX_yszz_cs = "";
                        string strCWFX_yszz_ts = "";
                        string strCWFX_yszz_bz = "";
                        string strCWFX_chzz_cs = "";
                        string strCWFX_chzz_ts = "";
                        string strCWFX_chzz_bz = "";
                        string strCWFX_z_cs = "";
                        string strCWFX_z_ts = "";
                        string strCWFX_z_bz = "";
                        string strCWFX_zczz = "";
                        string strCWFX_xszz = "";
                        string strCWFX_jlzz = "";
                        //定义数值类型变量，获取公式计算值
                        Double xsjl, zcjl, xsml, qbhs, ylxj, xssx, jyzb, ldbl, sdbl, xjbl, xjgf, xjll, xjzz, yszz_cs, yszz_ts, yszz_bz, chzz_cs, chzz_ts, chzz_bz, z_cs, z_ts, z_bz, zczz, xszz = 0, jlzz = 0;

                        if (dtexist.Rows.Count == 0)
                        {

                            //计算财务分析中各指标的数值
                            xsjl = Convert.ToDouble(dr22["LRFP_JLR"].ToString()) / Convert.ToDouble(dr22["LRFP_YYSR_ZYSR"].ToString());//利润分配净利润本年累计数/主营业收入本年累计数
                            zcjl = Convert.ToDouble(dr22["LRFP_JLR"].ToString()) / (((Convert.ToDouble(dr11["ZC_ZJ"].ToString())) + (Convert.ToDouble(dr12["ZC_ZJ"].ToString()))) / 2);//利润分配净利润本年累计数/资产负债表总资产平均
                            xsml = ((Convert.ToDouble(dr22["LRFP_YYSR_ZYSR"].ToString())) - (Convert.ToDouble(dr22["LRFP_YYSR_ZYCB"].ToString()))) / Convert.ToDouble(dr22["LRFP_YYSR_ZYCB"].ToString());//（利润分配主营收入-主营成本）本年累计数/主营成本本年累计数
                            qbhs = Convert.ToDouble(dr32["XJLL_JYHD_JE"].ToString()) / (((Convert.ToDouble(dr11["ZC_ZJ"].ToString())) + (Convert.ToDouble(dr12["ZC_ZJ"].ToString()))) / 2);//现金流量经营活动净额本年累计数/资产负债表总资产平均数
                            ylxj = Convert.ToDouble(dr32["XJLL_JYHD_JE"].ToString()) / Convert.ToDouble(dr22["LRFP_JLR"].ToString());//现金流量经营活动净额本年累计数/利润分配净利润本年累计数
                            xssx = Convert.ToDouble(dr32["XJLL_JYHD_XSTG"].ToString()) / Convert.ToDouble(dr22["LRFP_YYSR_ZYSR"].ToString());//现金流量销售商品或提供劳务收到的现金本年累计数/利润分配主营业务收入净额本年累计数
                            jyzb = ((Convert.ToDouble(dr11["ZC_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["ZC_LD_HJ"].ToString()))) / 2 - ((Convert.ToDouble(dr11["FZ_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["FZ_LD_HJ"].ToString()))) / 2;//流动资产平均数-流动负债平均数（资产负债表）
                            ldbl = (((Convert.ToDouble(dr11["ZC_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["ZC_LD_HJ"].ToString()))) / 2) / (((Convert.ToDouble(dr11["FZ_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["FZ_LD_HJ"].ToString()))) / 2);//流动资产平均数/流动负债平均数（资产负债表）
                            sdbl = ((((Convert.ToDouble(dr11["ZC_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["ZC_LD_HJ"].ToString()))) / 2) - (((Convert.ToDouble(dr11["ZC_LD_CH"].ToString())) + (Convert.ToDouble(dr12["ZC_LD_CH"].ToString()))) / 2)) / (((Convert.ToDouble(dr11["FZ_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["FZ_LD_HJ"].ToString()))) / 2);//（流动资产平均数-存货平均数）/流动负债平均数（资产负债表）
                            xjbl = ((Convert.ToDouble(dr11["ZC_LD_HBZJ"].ToString()) + Convert.ToDouble(dr12["ZC_LD_HBZJ"].ToString())) / 2 + (Convert.ToDouble(dr11["ZC_LD_JYJR"].ToString()) + Convert.ToDouble(dr12["ZC_LD_JYJR"].ToString())) / 2) / (((Convert.ToDouble(dr11["FZ_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["FZ_LD_HJ"].ToString()))) / 2);//（货币资金平均数+交易性金融资产平均数）/流动负债平均数（资产负债表）


                            xjgf = ((Convert.ToDouble(dr11["ZC_LD_HBZJ"].ToString()) + Convert.ToDouble(dr12["ZC_LD_HBZJ"].ToString())) / 2)/((((Convert.ToDouble(dr11["FZ_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["FZ_LD_HJ"].ToString()))) / 2)-(((Convert.ToDouble(dr11["FZ_LD_YSKX"].ToString())) + (Convert.ToDouble(dr12["FZ_LD_YSKX"].ToString()))) / 2));


                            xjll = (Convert.ToDouble(dr32["XJLL_JYHD"].ToString())) / (((Convert.ToDouble(dr11["FZ_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["FZ_LD_HJ"].ToString()))) / 2);//现金流量经营活动产生的现金流量本年累计数/资产负债表流动负债平均数
                            xjzz = (Convert.ToDouble(dr22["LRFP_YYSR_ZYSR"].ToString())) / ((Convert.ToDouble(dr11["ZC_LD_HBZJ"].ToString()) + Convert.ToDouble(dr12["ZC_LD_HBZJ"].ToString())) / 2);//利润分配主营收入本年累计数/资产负债货币资金平均数
                            yszz_cs = (Convert.ToDouble(dr22["LRFP_YYSR_ZYSR"].ToString())) / (((Convert.ToDouble(dr11["ZC_LD_YSZKYZ"].ToString())) + (Convert.ToDouble(dr12["ZC_LD_YSZKYZ"].ToString()))) / 2);//利润分配主营收入本年累计数/资产负债应收账款原值平均数
                            yszz_ts = 365 / yszz_cs;
                            yszz_bz = 1 / yszz_cs;
                            chzz_cs = (Convert.ToDouble(dr22["LRFP_YYSR_ZYCB"].ToString())) / (((Convert.ToDouble(dr11["ZC_LD_CH"].ToString())) + (Convert.ToDouble(dr12["ZC_LD_CH"].ToString()))) / 2);//利润分配主营成本本年累计数/资产负债存货平均数
                            chzz_ts = 365 / chzz_cs;
                            chzz_bz = 1 / chzz_cs;
                            z_cs = (Convert.ToDouble(dr22["LRFP_YYSR_ZYSR"].ToString())) / (((Convert.ToDouble(dr11["ZC_ZJ"].ToString())) + (Convert.ToDouble(dr12["ZC_ZJ"].ToString()))) / 2);//利润分配主营收入本年累计数/资产负债总资产平均数
                            z_ts = 365 / z_cs;
                            z_bz = 1 / z_cs;
                            zczz = ((Convert.ToDouble(dr12["ZC_ZJ"].ToString())) - (Convert.ToDouble(dr11["ZC_ZJ"].ToString()))) / (Convert.ToDouble(dr11["ZC_ZJ"].ToString()));//（资产负债总资产期末数-期初数）/期初数
                            if (k == 1)//营业收入本期数/营业收入上月值（利润分配表）
                            {
                                string sqltextsy = "";
                                int month = 12;
                                sqltextsy = "select * from TBFM_LRFP where RQBH like'" + yearsn + "-" + month.ToString() + "%' and LRFP_TYPE='本期数'";
                                System.Data.DataTable dtsy = DBCallCommon.GetDTUsingSqlText(sqltextsy);
                                if (dtsy.Rows.Count != 0)
                                {
                                    DataRow drsy = dtsy.Rows[0];
                                    xszz = (Convert.ToDouble(dr21["LRFP_YYSR"].ToString())) / (Convert.ToDouble(drsy["LRFP_YYSR"].ToString()));
                                }
                                else
                                {
                                    xszz = 0;
                                }
                            }
                            else
                            {
                                string sqltextsy = "";
                                if (k <= 10)
                                {
                                    sqltextsy = "select * from TBFM_LRFP where RQBH like'" + year + "-" + "0" + (k - 1).ToString() + "%' and LRFP_TYPE='本期数'";
                                }
                                else
                                {
                                    sqltextsy = "select * from TBFM_LRFP where RQBH like'" + year + "-" + (k - 1).ToString() + "%' and LRFP_TYPE='本期数'";
                                }
                                System.Data.DataTable dtsy = DBCallCommon.GetDTUsingSqlText(sqltextsy);
                                if (dtsy.Rows.Count != 0)
                                {
                                    DataRow drsy = dtsy.Rows[0];
                                    xszz = (Convert.ToDouble(dr21["LRFP_YYSR"].ToString())) / (Convert.ToDouble(drsy["LRFP_YYSR"].ToString()));
                                }
                                else
                                {
                                    xszz = 0;
                                }
                            }
                            if (k == 1)//净利润本期数/净利润上月值（利润分配表）
                            {
                                string sqltextsy = "";
                                int month = 12;
                                sqltextsy = "select * from TBFM_LRFP where RQBH like'" + yearsn + "-" + month.ToString() + "%' and LRFP_TYPE='本期数'";
                                System.Data.DataTable dtsy = DBCallCommon.GetDTUsingSqlText(sqltextsy);
                                if (dtsy.Rows.Count != 0)
                                {
                                    DataRow drsy = dtsy.Rows[0];
                                    jlzz = (Convert.ToDouble(dr21["LRFP_JLR"].ToString())) / (Convert.ToDouble(drsy["LRFP_JLR"].ToString()));
                                }
                                else
                                {
                                    jlzz = 0;
                                }
                            }
                            else
                            {
                                string sqltextsy = "";
                                if (k <= 10)
                                {
                                    sqltextsy = "select * from TBFM_LRFP where RQBH like '" + year + "-" + "0" + (k - 1).ToString() + "%' and LRFP_TYPE='本期数'";
                                }
                                else
                                {
                                    sqltextsy = "select * from TBFM_LRFP where RQBH like '" + year + "-" + (k - 1).ToString() + "%' and LRFP_TYPE='本期数'";
                                }
                                System.Data.DataTable dtsy = DBCallCommon.GetDTUsingSqlText(sqltextsy);
                                if (dtsy.Rows.Count != 0)
                                {
                                    DataRow drsy = dtsy.Rows[0];
                                    jlzz = (Convert.ToDouble(dr21["LRFP_JLR"].ToString())) / (Convert.ToDouble(drsy["LRFP_JLR"].ToString()));
                                }
                                else
                                {
                                    jlzz = 0;
                                }
                            }
                            //将数值转化为字符串赋给前面的中间字符串变量
                            strCWFX_xsjl = (xsjl * 100).ToString("0.00") + "%";
                            strCWFX_zcjl = (zcjl * 100).ToString("0.00") + "%";
                            strCWFX_xsml = (xsml * 100).ToString("0.00") + "%";
                            strCWFX_qbhs = (qbhs * 100).ToString("0.00") + "%";
                            strCWFX_ylxj = (ylxj * 100).ToString("0.00") + "%";
                            strCWFX_xssx = (xssx * 100).ToString("0.00") + "%";
                            strCWFX_jyzb = jyzb.ToString("0.00");
                            strCWFX_ldbl = (ldbl * 100).ToString("0.00") + "%";
                            strCWFX_sdbl = (sdbl * 100).ToString("0.00") + "%";
                            strCWFX_xjbl = (xjbl * 100).ToString("0.00") + "%";


                            strCWFX_xjgf = (xjgf * 100).ToString("0.00") + "%";


                            strCWFX_xjll = (xjll * 100).ToString("0.00") + "%";
                            strCWFX_xjzz = xjzz.ToString("0.00");
                            strCWFX_yszz_cs = yszz_cs.ToString("0.00");
                            strCWFX_yszz_ts = yszz_ts.ToString("0.00");
                            strCWFX_yszz_bz = yszz_bz.ToString("0.00");
                            strCWFX_chzz_cs = chzz_cs.ToString("0.00");
                            strCWFX_chzz_ts = chzz_ts.ToString("0.00");
                            strCWFX_chzz_bz = chzz_bz.ToString("0.00");
                            strCWFX_z_cs = z_cs.ToString("0.00");
                            strCWFX_z_ts = z_ts.ToString("0.00");
                            strCWFX_z_bz = z_bz.ToString("0.00");
                            strCWFX_zczz = (zczz * 100).ToString("0.00") + "%";
                            strCWFX_xszz = (xszz * 100).ToString("0.00") + "%";
                            strCWFX_jlzz = (jlzz * 100).ToString("0.00") + "%";


                            sqltext = "insert into TBFM_CWFX (RQBH,CWFX_XSJL,CWFX_ZCJL,CWFX_XSML,CWFX_QBHS,CWFX_YLXJ,CWFX_XSSX,CWFX_JYY,CWFX_LD,CWFX_SD,CWFX_XJ,CWFX_XJGF,CWFX_XJLL,CWFX_XJZZ,CWFX_YSZZ_CS,CWFX_YSZZ_TS,CWFX_YSZZ_BZ,CWFX_CHZZ_CS,CWFX_CHZZ_TS,CWFX_CHZZ_BZ,CWFX_Z_CS,CWFX_Z_TS,CWFX_Z_BZ,CWFX_ZCZZ,CWFX_XSZZ,CWFX_JLZZ) values ('" + rqbhtb + "','" + strCWFX_xsjl + "','" + strCWFX_zcjl + "','" + strCWFX_xsml + "','" + strCWFX_qbhs + "','" + strCWFX_ylxj + "','" + strCWFX_xssx + "','" + strCWFX_jyzb + "','" + strCWFX_ldbl + "','" + strCWFX_sdbl + "','" + strCWFX_xjbl + "','" + strCWFX_xjgf + "','" + strCWFX_xjll + "','" + strCWFX_xjzz + "','" + strCWFX_yszz_cs + "','" + strCWFX_yszz_ts + "','" + strCWFX_yszz_bz + "','" + strCWFX_chzz_cs + "','" + strCWFX_chzz_ts + "','" + strCWFX_chzz_bz + "','" + strCWFX_z_cs + "','" + strCWFX_z_ts + "','" + strCWFX_z_bz + "','" + strCWFX_zczz + "','" + strCWFX_xszz + "','" + strCWFX_jlzz + "')";
                            DBCallCommon.ExeSqlText(sqltext);
                        }
                        //若三个基本表中出现更改，那么也对财务分析表中的数据进行更新
                        else
                        {
                            //计算财务分析中各指标的数值
                            xsjl = Convert.ToDouble(dr22["LRFP_JLR"].ToString()) / Convert.ToDouble(dr22["LRFP_YYSR_ZYSR"].ToString());//利润分配净利润本年累计数/主营业收入本年累计数
                            zcjl = Convert.ToDouble(dr22["LRFP_JLR"].ToString()) / (((Convert.ToDouble(dr11["ZC_ZJ"].ToString())) + (Convert.ToDouble(dr12["ZC_ZJ"].ToString()))) / 2);//利润分配净利润本年累计数/资产负债表总资产平均
                            xsml = ((Convert.ToDouble(dr22["LRFP_YYSR_ZYSR"].ToString())) - (Convert.ToDouble(dr22["LRFP_YYSR_ZYCB"].ToString()))) / Convert.ToDouble(dr22["LRFP_YYSR_ZYCB"].ToString());//（利润分配主营收入-主营成本）本年累计数/主营成本本年累计数
                            qbhs = Convert.ToDouble(dr32["XJLL_JYHD_JE"].ToString()) / (((Convert.ToDouble(dr11["ZC_ZJ"].ToString())) + (Convert.ToDouble(dr12["ZC_ZJ"].ToString()))) / 2);//现金流量经营活动净额本年累计数/资产负债表总资产平均数
                            ylxj = Convert.ToDouble(dr32["XJLL_JYHD_JE"].ToString()) / Convert.ToDouble(dr22["LRFP_JLR"].ToString());//现金流量经营活动净额本年累计数/利润分配净利润本年累计数
                            xssx = Convert.ToDouble(dr32["XJLL_JYHD_XSTG"].ToString()) / Convert.ToDouble(dr22["LRFP_YYSR_ZYSR"].ToString());//现金流量销售商品或提供劳务收到的现金本年累计数/利润分配主营业务收入净额本年累计数
                            jyzb = ((Convert.ToDouble(dr11["ZC_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["ZC_LD_HJ"].ToString()))) / 2 - ((Convert.ToDouble(dr11["FZ_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["FZ_LD_HJ"].ToString()))) / 2;//流动资产平均数-流动负债平均数（资产负债表）
                            ldbl = (((Convert.ToDouble(dr11["ZC_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["ZC_LD_HJ"].ToString()))) / 2) / (((Convert.ToDouble(dr11["FZ_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["FZ_LD_HJ"].ToString()))) / 2);//流动资产平均数/流动负债平均数（资产负债表）
                            sdbl = ((((Convert.ToDouble(dr11["ZC_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["ZC_LD_HJ"].ToString()))) / 2) - (((Convert.ToDouble(dr11["ZC_LD_CH"].ToString())) + (Convert.ToDouble(dr12["ZC_LD_CH"].ToString()))) / 2)) / (((Convert.ToDouble(dr11["FZ_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["FZ_LD_HJ"].ToString()))) / 2);//（流动资产平均数-存货平均数）/流动负债平均数（资产负债表）
                            xjbl = ((Convert.ToDouble(dr11["ZC_LD_HBZJ"].ToString()) + Convert.ToDouble(dr12["ZC_LD_HBZJ"].ToString())) / 2 + (Convert.ToDouble(dr11["ZC_LD_JYJR"].ToString()) + Convert.ToDouble(dr12["ZC_LD_JYJR"].ToString())) / 2) / (((Convert.ToDouble(dr11["FZ_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["FZ_LD_HJ"].ToString()))) / 2);//（货币资金平均数+交易性金融资产平均数）/流动负债平均数（资产负债表）


                            xjgf = ((Convert.ToDouble(dr11["ZC_LD_HBZJ"].ToString()) + Convert.ToDouble(dr12["ZC_LD_HBZJ"].ToString())) / 2) / ((((Convert.ToDouble(dr11["FZ_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["FZ_LD_HJ"].ToString()))) / 2) - (((Convert.ToDouble(dr11["FZ_LD_YSKX"].ToString())) + (Convert.ToDouble(dr12["FZ_LD_YSKX"].ToString()))) / 2));


                            xjll = (Convert.ToDouble(dr32["XJLL_JYHD"].ToString())) / (((Convert.ToDouble(dr11["FZ_LD_HJ"].ToString())) + (Convert.ToDouble(dr12["FZ_LD_HJ"].ToString()))) / 2);//现金流量经营活动产生的现金流量本年累计数/资产负债表流动负债平均数
                            xjzz = (Convert.ToDouble(dr22["LRFP_YYSR_ZYSR"].ToString())) / ((Convert.ToDouble(dr11["ZC_LD_HBZJ"].ToString()) + Convert.ToDouble(dr12["ZC_LD_HBZJ"].ToString())) / 2);//利润分配主营收入本年累计数/资产负债货币资金平均数
                            yszz_cs = (Convert.ToDouble(dr22["LRFP_YYSR_ZYSR"].ToString())) / (((Convert.ToDouble(dr11["ZC_LD_YSZKYZ"].ToString())) + (Convert.ToDouble(dr12["ZC_LD_YSZKYZ"].ToString()))) / 2);//利润分配主营收入本年累计数/资产负债应收账款原值平均数
                            yszz_ts = 365 / yszz_cs;
                            yszz_bz = 1 / yszz_cs;
                            chzz_cs = (Convert.ToDouble(dr22["LRFP_YYSR_ZYCB"].ToString())) / (((Convert.ToDouble(dr11["ZC_LD_CH"].ToString())) + (Convert.ToDouble(dr12["ZC_LD_CH"].ToString()))) / 2);//利润分配主营成本本年累计数/资产负债存货平均数
                            chzz_ts = 365 / chzz_cs;
                            chzz_bz = 1 / chzz_cs;
                            z_cs = (Convert.ToDouble(dr22["LRFP_YYSR_ZYSR"].ToString())) / (((Convert.ToDouble(dr11["ZC_ZJ"].ToString())) + (Convert.ToDouble(dr12["ZC_ZJ"].ToString()))) / 2);//利润分配主营收入本年累计数/资产负债总资产平均数
                            z_ts = 365 / z_cs;
                            z_bz = 1 / z_cs;
                            zczz = ((Convert.ToDouble(dr12["ZC_ZJ"].ToString())) - (Convert.ToDouble(dr11["ZC_ZJ"].ToString()))) / (Convert.ToDouble(dr11["ZC_ZJ"].ToString()));//（资产负债总资产期末数-期初数）/期初数
                            if (k == 1)//营业收入本期数/营业收入上月值（利润分配表）
                            {
                                string sqltextsy = "";
                                int month = 12;
                                sqltextsy = "select * from TBFM_LRFP where RQBH like'" + yearsn + "-" + month.ToString() + "%' and LRFP_TYPE='本期数'";
                                System.Data.DataTable dtsy = DBCallCommon.GetDTUsingSqlText(sqltextsy);
                                if (dtsy.Rows.Count != 0)
                                {
                                    DataRow drsy = dtsy.Rows[0];
                                    xszz = (Convert.ToDouble(dr21["LRFP_YYSR"].ToString())) / (Convert.ToDouble(drsy["LRFP_YYSR"].ToString()));
                                }
                                else
                                {
                                    xszz = 0;
                                }
                            }
                            else
                            {
                                string sqltextsy = "";
                                if (k <= 10)
                                {
                                    sqltextsy = "select * from TBFM_LRFP where RQBH like'" + year + "-" + "0" + (k - 1).ToString() + "%' and LRFP_TYPE='本期数'";
                                }
                                else
                                {
                                    sqltextsy = "select * from TBFM_LRFP where RQBH like '" + year + "-" + (k - 1).ToString() + "%' and LRFP_TYPE='本期数'";
                                }
                                System.Data.DataTable dtsy = DBCallCommon.GetDTUsingSqlText(sqltextsy);
                                if (dtsy.Rows.Count != 0)
                                {
                                    DataRow drsy = dtsy.Rows[0];
                                    xszz = (Convert.ToDouble(dr21["LRFP_YYSR"].ToString())) / (Convert.ToDouble(drsy["LRFP_YYSR"].ToString()));
                                }
                                else
                                {
                                    xszz = 0;
                                }
                            }
                            if (k == 1)//净利润本期数/净利润上月值（利润分配表）
                            {
                                string sqltextsy = "";
                                int month = 12;
                                sqltextsy = "select * from TBFM_LRFP where RQBH like'" + yearsn + "-" + month.ToString() + "%' and LRFP_TYPE='本期数'";
                                System.Data.DataTable dtsy = DBCallCommon.GetDTUsingSqlText(sqltextsy);
                                if (dtsy.Rows.Count != 0)
                                {
                                    DataRow drsy = dtsy.Rows[0];
                                    jlzz = (Convert.ToDouble(dr21["LRFP_JLR"].ToString())) / (Convert.ToDouble(drsy["LRFP_JLR"].ToString()));
                                }
                                else
                                {
                                    jlzz = 0;
                                }
                            }
                            else
                            {
                                string sqltextsy = "";
                                if (k <= 10)
                                {
                                    sqltextsy = "select * from TBFM_LRFP where RQBH like '" + year + "-" + "0" + (k - 1).ToString() + "%' and LRFP_TYPE='本期数'";
                                }
                                else
                                {
                                    sqltextsy = "select * from TBFM_LRFP where RQBH like '" + year + "-" + (k - 1).ToString() + "%' and LRFP_TYPE='本期数'";
                                }
                                System.Data.DataTable dtsy = DBCallCommon.GetDTUsingSqlText(sqltextsy);
                                if (dtsy.Rows.Count != 0)
                                {
                                    DataRow drsy = dtsy.Rows[0];
                                    jlzz = (Convert.ToDouble(dr21["LRFP_JLR"].ToString())) / (Convert.ToDouble(drsy["LRFP_JLR"].ToString()));
                                }
                                else
                                {
                                    jlzz = 0;
                                }
                            }
                            //将数值转化为字符串赋给前面的中间字符串变量
                            strCWFX_xsjl = (xsjl * 100).ToString("0.00") + "%";
                            strCWFX_zcjl = (zcjl * 100).ToString("0.00") + "%";
                            strCWFX_xsml = (xsml * 100).ToString("0.00") + "%";
                            strCWFX_qbhs = (qbhs * 100).ToString("0.00") + "%";
                            strCWFX_ylxj = (ylxj * 100).ToString("0.00") + "%";
                            strCWFX_xssx = (xssx * 100).ToString("0.00") + "%";
                            strCWFX_jyzb = jyzb.ToString("0.00");
                            strCWFX_ldbl = (ldbl * 100).ToString("0.00") + "%";
                            strCWFX_sdbl = (sdbl * 100).ToString("0.00") + "%";
                            strCWFX_xjbl = (xjbl * 100).ToString("0.00") + "%";


                            strCWFX_xjgf = (xjgf * 100).ToString("0.00") + "%";


                            strCWFX_xjll = (xjll * 100).ToString("0.00") + "%";
                            strCWFX_xjzz = xjzz.ToString("0.00");
                            strCWFX_yszz_cs = yszz_cs.ToString("0.00");
                            strCWFX_yszz_ts = yszz_ts.ToString("0.00");
                            strCWFX_yszz_bz = yszz_bz.ToString("0.00");
                            strCWFX_chzz_cs = chzz_cs.ToString("0.00");
                            strCWFX_chzz_ts = chzz_ts.ToString("0.00");
                            strCWFX_chzz_bz = chzz_bz.ToString("0.00");
                            strCWFX_z_cs = z_cs.ToString("0.00");
                            strCWFX_z_ts = z_ts.ToString("0.00");
                            strCWFX_z_bz = z_bz.ToString("0.00");
                            strCWFX_zczz = (zczz * 100).ToString("0.00") + "%";
                            strCWFX_xszz = (xszz * 100).ToString("0.00") + "%";
                            strCWFX_jlzz = (jlzz * 100).ToString("0.00") + "%";


                            sqltext = "update TBFM_CWFX set CWFX_XSJL='" + strCWFX_xsjl + "',CWFX_ZCJL='" + strCWFX_zcjl + "',CWFX_XSML='" + strCWFX_xsml + "',CWFX_QBHS='" + strCWFX_qbhs + "',CWFX_YLXJ='" + strCWFX_ylxj + "',CWFX_XSSX='" + strCWFX_xssx + "',CWFX_JYY='" + strCWFX_jyzb + "',CWFX_LD='" + strCWFX_ldbl + "',CWFX_SD='" + strCWFX_sdbl + "',CWFX_XJ='" + strCWFX_xjbl + "',CWFX_XJGF='" + strCWFX_xjgf + "',CWFX_XJLL='" + strCWFX_xjll + "',CWFX_XJZZ='" + strCWFX_xjzz + "',CWFX_YSZZ_CS='" + strCWFX_yszz_cs + "',CWFX_YSZZ_TS='" + strCWFX_yszz_ts + "',CWFX_YSZZ_BZ='" + strCWFX_yszz_bz + "',CWFX_CHZZ_CS='" + strCWFX_chzz_cs + "',CWFX_CHZZ_TS='" + strCWFX_chzz_ts + "',CWFX_CHZZ_BZ='" + strCWFX_chzz_bz + "',CWFX_Z_CS='" + strCWFX_z_cs + "',CWFX_Z_TS='" + strCWFX_z_ts + "',CWFX_Z_BZ='" + strCWFX_z_bz + "',CWFX_ZCZZ='" + strCWFX_zczz + "',CWFX_XSZZ='" + strCWFX_xszz + "',CWFX_JLZZ='" + strCWFX_jlzz + "' where RQBH like'" + rqbhtb + "%'";
                            DBCallCommon.ExeSqlText(sqltext);
                        }
                   }
                   //以资产负债表为基准，如果财务分析表中存在某月的数据，而资产负债表中没有，则删除财务分析表中该月的数据
                    else if (dt11.Rows.Count == 0)
                    {
                        string rqbhtb = year + "-" + j.ToString() + "";
                        string sqlTextisexist = "select * from TBFM_CWFX where RQBH like'" + rqbhtb + "%'";
                        System.Data.DataTable dtexist = DBCallCommon.GetDTUsingSqlText(sqlTextisexist);
                        if (dtexist.Rows.Count != 0)
                        {
                            string sqltextsc = "delete from TBFM_CWFX where RQBH like '"+rqbhtb+"%'";
                            DBCallCommon.ExeSqlText(sqltextsc);
                        }
                    }
                }
            }
            Response.Write("<script>alert('更新完毕！')</script>");
        }





        #region 导出数据


        private int ifselect()
        {
            int flag = 0;
            int i = 0;//是否选择数据
            foreach (RepeaterItem Reitem in rptProNumCost.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("chkDel") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                }
            }
            if (i == 0)//未选择数据
            {
                flag = 1;
            }
            else
            {
                flag = 0;
            }
            return flag;
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            int flag = ifselect();
            if (flag == 0)//判断是否有勾选框被勾选
            {
                string rqbhdc = "";
                foreach (RepeaterItem Reitem in rptProNumCost.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("chkDel") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx.Checked)
                    {
                        rqbhdc += "'" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("RQBH")).Text.ToString() + "'" + ",";
                    }
                }
                rqbhdc = rqbhdc.Substring(0, rqbhdc.LastIndexOf(",")).ToString();
                string sqltext = "";
                sqltext = "select * from TBFM_CWFX where  RQBH in (" + rqbhdc + ") order by RQBH";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                ExportDataItem(dt);
            }
            else if (flag == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要导出的财务分析数据！！！');", true);
            }
        }
        private void ExportDataItem(System.Data.DataTable dt)
        {

            string filename = "财务分析表.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("财务分析表模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet

                #region 向excel表中写入数据

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                        IRow row1 = sheet0.GetRow(1);
                        row1.GetCell(i + 2).SetCellValue(dt.Rows[i]["RQBH"].ToString());

                        IRow row2 = sheet0.GetRow(2);
                        row2.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_XSJL"].ToString());

                        IRow row3 = sheet0.GetRow(3);
                        row3.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_ZCJL"].ToString());

                        IRow row4 = sheet0.GetRow(4);
                        row4.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_XSML"].ToString());

                        IRow row5 = sheet0.GetRow(5);
                        row5.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_YLNLFX"].ToString());

                        IRow row6 = sheet0.GetRow(6);
                        row6.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_QBHS"].ToString());

                        IRow row7 = sheet0.GetRow(7);
                        row7.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_YLXJ"].ToString());

                        IRow row8 = sheet0.GetRow(8);
                        row8.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_XSSX"].ToString());

                        IRow row9 = sheet0.GetRow(9);
                        row9.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_YLZLFX"].ToString());

                        IRow row10 = sheet0.GetRow(10);
                        row10.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_JYY"].ToString());

                        IRow row11 = sheet0.GetRow(11);
                        row11.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_LD"].ToString());

                        IRow row12 = sheet0.GetRow(12);
                        row12.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_SD"].ToString());

                        IRow row13 = sheet0.GetRow(13);
                        row13.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_XJ"].ToString());

                        IRow row14 = sheet0.GetRow(14);
                        row14.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_XJGF"].ToString());

                        IRow row15 = sheet0.GetRow(15);
                        row15.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_XJLL"].ToString());

                        IRow row16 = sheet0.GetRow(16);
                        row16.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_CZNLFX"].ToString());

                        IRow row17 = sheet0.GetRow(17);
                        row17.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_XJZZ"].ToString());

                        IRow row18 = sheet0.GetRow(18);
                        row18.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_YSZZ_CS"].ToString());

                        IRow row19 = sheet0.GetRow(19);
                        row19.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_YSZZ_TS"].ToString());

                        IRow row20 = sheet0.GetRow(20);
                        row20.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_YSZZ_BZ"].ToString());

                        IRow row21 = sheet0.GetRow(21);
                        row21.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_CHZZ_CS"].ToString());

                        IRow row22 = sheet0.GetRow(22);
                        row22.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_CHZZ_TS"].ToString());

                        IRow row23 = sheet0.GetRow(23);
                        row23.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_CHZZ_BZ"].ToString());

                        IRow row24 = sheet0.GetRow(24);
                        row24.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_Z_CS"].ToString());

                        IRow row25 = sheet0.GetRow(25);
                        row25.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_Z_TS"].ToString());

                        IRow row26 = sheet0.GetRow(26);
                        row26.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_Z_BZ"].ToString());

                        IRow row27 = sheet0.GetRow(27);
                        row27.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_YYNLFX"].ToString());

                        IRow row28 = sheet0.GetRow(28);
                        row28.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_ZCZZ"].ToString());

                        IRow row29 = sheet0.GetRow(29);
                        row29.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_XSZZ"].ToString());

                        IRow row30 = sheet0.GetRow(30);
                        row30.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_JLZZ"].ToString());

                        IRow row31 = sheet0.GetRow(31);
                        row31.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_FZNLFX"].ToString());

                        IRow row32 = sheet0.GetRow(32);
                        row32.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_GZZC"].ToString());

                        IRow row33 = sheet0.GetRow(33);
                        row33.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_SBCB"].ToString());

                        IRow row34 = sheet0.GetRow(34);
                        row34.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_GCLY"].ToString());

                        IRow row35 = sheet0.GetRow(35);
                        row35.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_DWHD"].ToString());

                        IRow row36 = sheet0.GetRow(36);
                        row36.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_DWZJ"].ToString());

                        IRow row37 = sheet0.GetRow(37);
                        row37.GetCell(i + 2).SetCellValue(dt.Rows[i]["CWFX_SCNLFX"].ToString());
                }

                #endregion

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

        private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet) //输出Excel文件并退出
        {
            try
            {

                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Workbooks.Close();
                m_xlApp.Quit();
                m_xlApp.Application.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);
                wksheet = null;
                workbook = null;
                m_xlApp = null;
                GC.Collect();
                //下载
                System.IO.FileInfo path = new System.IO.FileInfo(filename);
                //同步，异步都支持
                HttpResponse contextResponse = HttpContext.Current.Response;
                contextResponse.Redirect(string.Format("~/FM_Data/ExportFile/{0}", path.Name), false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
