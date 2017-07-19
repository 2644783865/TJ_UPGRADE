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
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Collections.Generic;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_HTZHIBIAOANY : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
            CheckUser(ControlFinder);
        }
        //查询
        protected void btnsearch_click(object sender, EventArgs e)
        {
            if (datestart.Value.Trim() == "" || dateend.Value.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择起始日期！');", true);
                return;
            }
            else
            {
                DataTable dt = this.GetDataTable();
                rptdata0.DataSource = dt;
                rptdata0.DataBind();
            }
            drawingdata();
        }
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("newhte_jtwsh");
            dt.Columns.Add("newhte_jtwwsh");
            dt.Columns.Add("newhte_jtw");
            dt.Columns.Add("newhte_jtnsh");
            dt.Columns.Add("newhte_jtnwsh");
            dt.Columns.Add("newhte_jtn");
            dt.Columns.Add("newhte_zysh");
            dt.Columns.Add("newhte_zywsh");
            dt.Columns.Add("newhte_zy");
            dt.Columns.Add("newhtesh");
            dt.Columns.Add("newhtewsh");
            dt.Columns.Add("newhte");
            dt.Columns.Add("ht_yusuanhte");
            dt.Columns.Add("jzhtesh");
            dt.Columns.Add("jzhtewsh");
            dt.Columns.Add("jzhte");
            DataRow newRow = dt.NewRow();
            //集团外已审核合同
            string sql00 = "select sum(cast(PCON_JINE as float)) as newhte_jtwsh from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where ((PCON_FILLDATE>='" + datestart.Value.Trim() + "' and PCON_FILLDATE<='" + dateend.Value.Trim() + "') and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and CR_PSZT='2' and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where kehutype='2' and CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where kehutype='2' and CS_TYPE='1') or PCON_CUSTMNAME='')";
            System.Data.DataTable dt00 = DBCallCommon.GetDTUsingSqlText(sql00);
            if (dt00.Rows.Count > 0)
            {
                newRow[0] = dt00.Rows[0]["newhte_jtwsh"].ToString().Trim();
            }
            //集团外未审核合同
            string sql01 = "select sum(cast(PCON_JINE as float)) as newhte_jtwwsh from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where ((PCON_FILLDATE>='" + datestart.Value.Trim() + "' and PCON_FILLDATE<='" + dateend.Value.Trim() + "') and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and (CR_PSZT='1' or CR_PSZT is null or CR_PSZT='0') and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where kehutype='2' and CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where kehutype='2' and CS_TYPE='1') or PCON_CUSTMNAME='')";
            System.Data.DataTable dt01 = DBCallCommon.GetDTUsingSqlText(sql01);
            if (dt01.Rows.Count > 0)
            {
                newRow[1] = dt01.Rows[0]["newhte_jtwwsh"].ToString().Trim();
            }
            //集团外合同
            string sql02 = "select sum(cast(PCON_JINE as float)) as newhte_jtw from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where ((PCON_FILLDATE>='" + datestart.Value.Trim() + "' and PCON_FILLDATE<='" + dateend.Value.Trim() + "') and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and (CR_PSZT='1' or CR_PSZT is null or CR_PSZT='0' or CR_PSZT='2') and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where kehutype='2' and CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where kehutype='2' and CS_TYPE='1') or PCON_CUSTMNAME='')";
            System.Data.DataTable dt02 = DBCallCommon.GetDTUsingSqlText(sql02);
            if (dt02.Rows.Count > 0)
            {
                newRow[2] = dt02.Rows[0]["newhte_jtw"].ToString().Trim();
            }



            //集团内已审核合同
            string sql10 = "select sum(cast(PCON_JINE as float)) as newhte_jtnsh from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where ((PCON_FILLDATE>='" + datestart.Value.Trim() + "' and PCON_FILLDATE<='" + dateend.Value.Trim() + "') and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and CR_PSZT='2' and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where kehutype='1' and CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where kehutype='1' and CS_TYPE='1'))";
            System.Data.DataTable dt10 = DBCallCommon.GetDTUsingSqlText(sql10);
            if (dt10.Rows.Count > 0)
            {
                newRow[3] = dt10.Rows[0]["newhte_jtnsh"].ToString().Trim();
            }
            //集团内未审核合同
            string sql11 = "select sum(cast(PCON_JINE as float)) as newhte_jtnwsh from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where ((PCON_FILLDATE>='" + datestart.Value.Trim() + "' and PCON_FILLDATE<='" + dateend.Value.Trim() + "') and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and (CR_PSZT='1' or CR_PSZT is null or CR_PSZT='0') and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where kehutype='1' and CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where kehutype='1' and CS_TYPE='1'))";
            System.Data.DataTable dt11 = DBCallCommon.GetDTUsingSqlText(sql11);
            if (dt11.Rows.Count > 0)
            {
                newRow[4] = dt11.Rows[0]["newhte_jtnwsh"].ToString().Trim();
            }
            //集团内合同
            string sql12 = "select sum(cast(PCON_JINE as float)) as newhte_jtn from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where ((PCON_FILLDATE>='" + datestart.Value.Trim() + "' and PCON_FILLDATE<='" + dateend.Value.Trim() + "') and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and (CR_PSZT='1' or CR_PSZT is null or CR_PSZT='0' or CR_PSZT='2') and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where kehutype='1' and CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where kehutype='1' and CS_TYPE='1'))";
            System.Data.DataTable dt12 = DBCallCommon.GetDTUsingSqlText(sql12);
            if (dt12.Rows.Count > 0)
            {
                newRow[5] = dt12.Rows[0]["newhte_jtn"].ToString().Trim();
            }



            //自营已审核合同
            string sql20 = "select sum(cast(PCON_JINE as float)) as newhte_zysh from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where ((PCON_FILLDATE>='" + datestart.Value.Trim() + "' and PCON_FILLDATE<='" + dateend.Value.Trim() + "') and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and CR_PSZT='2' and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where kehutype='3' and CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where kehutype='3' and CS_TYPE='1'))";
            System.Data.DataTable dt20 = DBCallCommon.GetDTUsingSqlText(sql20);
            if (dt20.Rows.Count > 0)
            {
                newRow[6] = dt20.Rows[0]["newhte_zysh"].ToString().Trim();
            }
            //自营未审核合同
            string sql21 = "select sum(cast(PCON_JINE as float)) as newhte_zywsh from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where ((PCON_FILLDATE>='" + datestart.Value.Trim() + "' and PCON_FILLDATE<='" + dateend.Value.Trim() + "') and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and (CR_PSZT='1' or CR_PSZT is null or CR_PSZT='0') and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where kehutype='3' and CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where kehutype='3' and CS_TYPE='1'))";
            System.Data.DataTable dt21 = DBCallCommon.GetDTUsingSqlText(sql21);
            if (dt21.Rows.Count > 0)
            {
                newRow[7] = dt21.Rows[0]["newhte_zywsh"].ToString().Trim();
            }
            //自营合同
            string sql22 = "select sum(cast(PCON_JINE as float)) as newhte_zy from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where ((PCON_FILLDATE>='" + datestart.Value.Trim() + "' and PCON_FILLDATE<='" + dateend.Value.Trim() + "') and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and (CR_PSZT='1' or CR_PSZT is null or CR_PSZT='0' or CR_PSZT='2') and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where kehutype='3' and CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where kehutype='3' and CS_TYPE='1'))";
            System.Data.DataTable dt22 = DBCallCommon.GetDTUsingSqlText(sql22);
            if (dt22.Rows.Count > 0)
            {
                newRow[8] = dt22.Rows[0]["newhte_zy"].ToString().Trim();
            }



            //已审核合同合计
            string sql30 = "select sum(cast(PCON_JINE as float)) as newhtesh from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where ((PCON_FILLDATE>='" + datestart.Value.Trim() + "' and PCON_FILLDATE<='" + dateend.Value.Trim() + "') and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and CR_PSZT='2' and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where CS_TYPE='1'))";
            System.Data.DataTable  dt30= DBCallCommon.GetDTUsingSqlText(sql30);
            if (dt30.Rows.Count > 0)
            {
                newRow[9] = dt30.Rows[0]["newhtesh"].ToString().Trim();
            }
            //未审核合同合计
            string sql31 = "select sum(cast(PCON_JINE as float)) as newhtewsh from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where ((PCON_FILLDATE>='" + datestart.Value.Trim() + "' and PCON_FILLDATE<='" + dateend.Value.Trim() + "') and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and (CR_PSZT='1' or CR_PSZT is null or CR_PSZT='0') and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where CS_TYPE='1'))";
            System.Data.DataTable dt31 = DBCallCommon.GetDTUsingSqlText(sql31);
            if (dt31.Rows.Count > 0)
            {
                newRow[10] = dt31.Rows[0]["newhtewsh"].ToString().Trim();
            }

            //合同合计
            string sql32 = "select sum(cast(PCON_JINE as float)) as newhte from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where ((PCON_FILLDATE>='" + datestart.Value.Trim() + "' and PCON_FILLDATE<='" + dateend.Value.Trim() + "') and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and (CR_PSZT='1' or CR_PSZT is null or CR_PSZT='0' or CR_PSZT='2') and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where CS_TYPE='1'))";
            System.Data.DataTable dt32 = DBCallCommon.GetDTUsingSqlText(sql32);
            if (dt32.Rows.Count > 0)
            {
                newRow[11] = dt32.Rows[0]["newhte"].ToString().Trim();
            }



            //预算合同额
            string sql40 = "select ht_yusuanhte from FM_HTYUSUAN where ht_year='" + datestart.Value.Trim().Substring(0, 4).ToString().Trim() + "'";
            System.Data.DataTable  dt40= DBCallCommon.GetDTUsingSqlText(sql40);
            if (dt40.Rows.Count > 0)
            {
                newRow[12] = dt40.Rows[0]["ht_yusuanhte"].ToString().Trim();
            }

            double jzhtzeysh=0;
            double jzhtzewsh=0;
            double jzhtze=0;
            //已审核合同总额
            string sql50 = "select sum(cast(PCON_JINE as float)) as newhtesh from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where  (PCON_FILLDATE<='" + dateend.Value.Trim() + "' and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and CR_PSZT='2' and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where CS_TYPE='1'))";
            System.Data.DataTable dt50 = DBCallCommon.GetDTUsingSqlText(sql50);
            if (dt50.Rows.Count > 0)
            {
                jzhtzeysh=Math.Round(CommonFun.ComTryDouble(dt50.Rows[0]["newhtesh"].ToString().Trim()),2);
            }
            //未审核合同总额
            string sql51 = "select sum(cast(PCON_JINE as float)) as newhtewsh from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where (PCON_FILLDATE<='" + dateend.Value.Trim() + "' and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and (CR_PSZT='1' or CR_PSZT is null or CR_PSZT='0') and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where CS_TYPE='1'))";
            System.Data.DataTable dt51 = DBCallCommon.GetDTUsingSqlText(sql51);
            if (dt51.Rows.Count > 0)
            {
                jzhtzewsh=Math.Round(CommonFun.ComTryDouble(dt51.Rows[0]["newhtewsh"].ToString().Trim()),2);
            }
            //合同总额
            string sql52 = "select sum(cast(PCON_JINE as float)) as newhte from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where (PCON_FILLDATE<='" + dateend.Value.Trim() + "' and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and (CR_PSZT='1' or CR_PSZT is null or CR_PSZT='0' or CR_PSZT='2') and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where CS_TYPE='1'))";
            System.Data.DataTable dt52 = DBCallCommon.GetDTUsingSqlText(sql52);
            if (dt52.Rows.Count > 0)
            {
                jzhtze=Math.Round(CommonFun.ComTryDouble(dt52.Rows[0]["newhte"].ToString().Trim()),2);
            }


            double kpysh=0;
            double kpwsh=0;
            double kpze=0;
            //已审核开票
            string sql60 = "select sum(kpZongMoney) as yshkp from (select b.conId,b.Proj,b.Map,b.Unit,a.KP_KPNUMBER,a.KP_KPDATE,sum(cast(b.Money as float)) as ZongMoney,sum(cast(b.kpmoney as float)) as kpZongMoney from CM_KAIPIAO as a left join dbo.CM_KAIPIAO_DETAIL as b on a.KP_TaskID=b.cId where KP_SPSTATE='3' group by b.conId,b.Proj,b.Map,b.Unit,a.KP_KPNUMBER,a.KP_KPDATE)c where conId in(select PCON_BCODE from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where (PCON_FILLDATE<='" + dateend.Value.Trim() + "' and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and CR_PSZT='2' and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where CS_TYPE='1')))";
            System.Data.DataTable dt60 = DBCallCommon.GetDTUsingSqlText(sql60);
            if (dt60.Rows.Count > 0)
            {
                kpysh = Math.Round(CommonFun.ComTryDouble(dt60.Rows[0]["yshkp"].ToString().Trim()),2);
            }
            //未审核开票
            string sql61 = "select sum(kpZongMoney) as wshkp from (select b.conId,b.Proj,b.Map,b.Unit,a.KP_KPNUMBER,a.KP_KPDATE,sum(cast(b.Money as float)) as ZongMoney,sum(cast(b.kpmoney as float)) as kpZongMoney from CM_KAIPIAO as a left join dbo.CM_KAIPIAO_DETAIL as b on a.KP_TaskID=b.cId where KP_SPSTATE='3' group by b.conId,b.Proj,b.Map,b.Unit,a.KP_KPNUMBER,a.KP_KPDATE)c where conId in(select PCON_BCODE from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where (PCON_FILLDATE<='" + dateend.Value.Trim() + "' and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and (CR_PSZT='1' or CR_PSZT is null or CR_PSZT='0') and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where CS_TYPE='1')))";
            System.Data.DataTable dt61 = DBCallCommon.GetDTUsingSqlText(sql61);
            if (dt61.Rows.Count > 0)
            {
                kpwsh = Math.Round(CommonFun.ComTryDouble(dt61.Rows[0]["wshkp"].ToString().Trim()),2);
            }
            //已开票
            string sql62 = "select sum(kpZongMoney) as ykp from (select b.conId,b.Proj,b.Map,b.Unit,a.KP_KPNUMBER,a.KP_KPDATE,sum(cast(b.Money as float)) as ZongMoney,sum(cast(b.kpmoney as float)) as kpZongMoney from CM_KAIPIAO as a left join dbo.CM_KAIPIAO_DETAIL as b on a.KP_TaskID=b.cId where KP_SPSTATE='3' group by b.conId,b.Proj,b.Map,b.Unit,a.KP_KPNUMBER,a.KP_KPDATE)c where conId in(select PCON_BCODE from TBPM_CONPCHSINFO as a left join TBCR_CONTRACTREVIEW as b on a.PCON_REVID=b.CR_ID where (PCON_FILLDATE<='" + dateend.Value.Trim() + "' and PCON_FILLDATE!='' and PCON_FILLDATE is not null) and PCON_JINE !='0' and (CR_PSZT='1' or CR_PSZT is null or CR_PSZT='0' or CR_PSZT='2') and (PCON_CUSTMNAME in (select CS_HRCODE from TBCS_CUSUPINFO where CS_TYPE='1') or PCON_CUSTMNAME in (select CS_NAME from TBCS_CUSUPINFO where CS_TYPE='1')))";
            System.Data.DataTable dt62 = DBCallCommon.GetDTUsingSqlText(sql62);
            if (dt62.Rows.Count > 0)
            {
                kpze = Math.Round(CommonFun.ComTryDouble(dt62.Rows[0]["ykp"].ToString().Trim()), 2);
            }
            newRow[13] = (jzhtzeysh - kpysh).ToString().Trim();
            newRow[14] = (jzhtzewsh - kpwsh).ToString().Trim();
            newRow[15] = (jzhtze - kpze).ToString().Trim();

            dt.Rows.Add(newRow);

            dt.AcceptChanges();
            return dt;
        }
        //绘制图形
        private void drawingdata()
        {
            DataTable dt0 = this.GetDataTable();
            if (dt0.Rows.Count > 0)
            {
                DataView dv0 = new DataView(dt0);
                Chart0.Series["新签合同额"].Points.DataBindY(dv0, "newhte");
                //Chart0.Series["预计新签合同额"].Points.DataBindY(dv0, "ht_yusuanhte");
                Chart0.Series["结转合同额"].Points.DataBindY(dv0, "jzhte");

                Chart0.Series["新签合同额"].Points[0].AxisLabel = datestart.Value.Trim() + "至" + dateend.Value.Trim();

                Chart0.Series["新签合同额"].Label = "#VALY";
                //Chart0.Series["预计新签合同额"].Label = "#VALY";
                Chart0.Series["结转合同额"].Label = "#VALY";


                //新签合同额饼状图
                List<string> xData0 = new List<string>() {"装备集团外已审核","装备集团外未审核","装备集团内已审核","装备集团内未审核","自营合同已审核","自营合同未审核"};
                List<double> yData0 = new List<double>() { CommonFun.ComTryDouble(dt0.Rows[0]["newhte_jtwsh"].ToString().Trim()), CommonFun.ComTryDouble(dt0.Rows[0]["newhte_jtwwsh"].ToString().Trim()), CommonFun.ComTryDouble(dt0.Rows[0]["newhte_jtnsh"].ToString().Trim()), CommonFun.ComTryDouble(dt0.Rows[0]["newhte_jtnwsh"].ToString().Trim()), CommonFun.ComTryDouble(dt0.Rows[0]["newhte_zysh"].ToString().Trim()), CommonFun.ComTryDouble(dt0.Rows[0]["newhte_zywsh"].ToString().Trim()) };

                Chart1.Series["新签合同组成"].ToolTip = "#VALX:#VALY 万元";
                Chart1.Series["新签合同组成"].LegendText= "#VALX";
                Chart1.Series["新签合同组成"].Label = "#PERCENT";
                Chart1.Series["新签合同组成"].Points.DataBindXY(xData0, yData0);
                Chart1.Series["新签合同组成"]["PieLabelStyle"] = "Outside";


                //结转合同额饼状图
                List<string> xData1 = new List<string>() { "结转合同已审核", "结转合同未审核" };
                List<double> yData1 = new List<double>() { CommonFun.ComTryDouble(dt0.Rows[0]["jzhtesh"].ToString().Trim()), CommonFun.ComTryDouble(dt0.Rows[0]["jzhtewsh"].ToString().Trim()) };
                Chart2.Series["结转合同组成"].ToolTip = "#VALX:#VALY 万元";
                Chart2.Series["结转合同组成"].LegendText = "#VALX";
                Chart2.Series["结转合同组成"].Label = "#PERCENT";
                Chart2.Series["结转合同组成"].Points.DataBindXY(xData1, yData1);
                Chart2.Series["结转合同组成"]["PieLabelStyle"] = "Outside";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有数据！');", true);
                return;
            }
            Chart0.Series["新签合同额"].ChartType = SeriesChartType.Column;
            //Chart0.Series["预计新签合同额"].ChartType = SeriesChartType.Column;
            Chart0.Series["结转合同额"].ChartType = SeriesChartType.Column;

            Chart1.Series["新签合同组成"].ChartType = SeriesChartType.Pie;
            Chart1.Series["新签合同组成"]["PieLineColor"] = "Black";
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Rotation = 15;
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Inclination = 45;

            Chart2.Series["结转合同组成"].ChartType = SeriesChartType.Pie;
            Chart2.Series["结转合同组成"]["PieLineColor"] = "Black";
            Chart2.ChartAreas["ChartArea2"].Area3DStyle.Enable3D = true;
            Chart2.ChartAreas["ChartArea2"].Area3DStyle.Rotation = 15;
            Chart2.ChartAreas["ChartArea2"].Area3DStyle.Inclination = 45;
        }
    }
}
