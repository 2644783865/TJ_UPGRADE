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
using System.Data.SqlClient;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_Create_YFInvoice : System.Web.UI.Page
    {
        double zlhj = 0;
        double jehj = 0;
        double hsjehj = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData();
            }
            string sqltext = "update PM_CPFYJSD set JS_XTSTATE='0' where JS_XTSTATE='1'";
            DBCallCommon.ExeSqlText(sqltext);
        }
        /// <summary>
        /// 当前页面绑定数据
        /// </summary>
        private void BindData()
        {
            string sqltext = "select left(CONVERT(CHAR(10), JS_RQ, 23),10) as JS_RQ,JS_JHGZH,JS_RWH,JS_BH,JS_GYS,JS_ZDR,JS_TUHAO,JS_SBMC,JS_BJSL,JS_DANZ,cast(((isnull(cast(JS_HSJE as float),0))/(1+(isnull(JS_SHUIL,0))/100)) as decimal(12,2)) as MONEY,(cast(isnull(JS_SHUIL,0) as char(6))+'%') as JS_SHUIL,isnull(cast(JS_HSJE as float),0) as JS_HSJE from PM_CPFYJSD where JS_XTSTATE='1'";
            rptProNumCost.DataSource = DBCallCommon.GetDTUsingSqlText(sqltext);
            rptProNumCost.DataBind();
            for (int i = 0; i < rptProNumCost.Items.Count - 1; i++)
            {
                if (i == rptProNumCost.Items.Count - 2)
                {
                    for (int j = 0; j < rptProNumCost.Items.Count - 2; j++)
                    {
                        Label lbjsd1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                        Label lbdat1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbdate");
                        Label lbgysm1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbgysmc");
                        Label lbzdrx1 = (Label)rptProNumCost.Items[i + 1].FindControl("lbzdrxm");
                        Label lbjsd2 = (Label)rptProNumCost.Items[j].FindControl("lbjsdh");
                        if (lbjsd1.Text.ToString() == lbjsd2.Text.ToString())
                        {
                            lbjsd1.Visible = false;
                            lbdat1.Visible = false;
                            lbgysm1.Visible = false;
                            lbzdrx1.Visible = false;
                        }
                    }
                }
                Label lbjsdh1 = (Label)rptProNumCost.Items[i].FindControl("lbjsdh");
                Label lbjsdh2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbjsdh");
                Label lbdate2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbdate");
                Label lbgysmc2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbgysmc");
                Label lbzdrxm2 = (Label)rptProNumCost.Items[i + 1].FindControl("lbzdrxm");
                if (lbjsdh1.Text.ToString() == lbjsdh2.Text.ToString())
                {
                    lbjsdh2.Visible = false;
                    lbdate2.Visible = false;
                    lbgysmc2.Visible = false;
                    lbzdrxm2.Visible = false;
                }
            }
        }

        //生成发票事件
        protected void btnCreatInv_Click(object sender, EventArgs e)
        {
            string fpbh = "FP" + DateTime.Now.ToString("yyyyMMddHHmmss");
            this.CreateInvOpDataBase(fpbh);//创建发票，写入相关数据表中
            this.ClientScript.RegisterStartupScript(GetType(), "js", "location.href ='FM_YFInvoice.aspx?Action=Audit&YFGI_CODE=" + fpbh + "';", true);
        }

        /// <summary>
        /// 创建发票,写入相关数据表中
        /// </summary>
        private void CreateInvOpDataBase(string fpbh)
        {
            List<string> sql = new List<string>();
            string fpcode = fpbh;//发票编号
            string jhgzhcode = "";
            //string[] arrayUniqCode = arrayJhgzh.Split('/');//计划跟踪号数组
            foreach (RepeaterItem Reitem in rptProNumCost.Items)
            {
                jhgzhcode = ((Label)Reitem.FindControl("lbjhgzh")).Text.Trim().ToString();
            }
            sql.Add(this.CreateInvTOTAL(jhgzhcode, fpcode));//发票总表，参数传递（结算单计划跟踪号(主要为从结算单获取供应商信息)，发票编号）

            foreach (RepeaterItem Reitem in rptProNumCost.Items)
            {
                string Uniqcode = ((Label)Reitem.FindControl("lbjhgzh")).Text.Trim().ToString();
                sql.Add(this.CreateJSD(Uniqcode));//更新结算单结算状态
                sql.Add(this.CreateGJrelation(Uniqcode, fpcode));//更新勾稽关系表    
            }



            //根据发票明细上的金额更新发票总表(总金额、含税金额)
            string str_je = "update TBFM_YFFPTOTAL set YFGI_MONEY=(select sum(isnull(YFGJ_JE,0)) from TBFM_YFFPDETAIL where YFGJ_FPID='" + fpcode + "') where YFGI_CODE='" + fpcode + "'";
            string str_hsje = "update TBFM_YFFPTOTAL set YFGI_HSMONEY=(select sum(isnull(YFGJ_HSJE,0)) from TBFM_YFFPDETAIL where YFGJ_FPID='" + fpcode + "') where YFGI_CODE='" + fpcode + "'";
            sql.Add(str_je);
            sql.Add(str_hsje);
            DBCallCommon.ExecuteTrans(sql);
        }
        #region
        /// <summary>
        /// 向购货发票总表-SqlText中写入数据
        /// </summary>
        /// <returns></returns>
        private string CreateInvTOTAL(string arrayUniqCode, string fpcode)
        {
            string sqltext = "";
            //编号
            string fp_code = fpcode;
            string arrayUniq_Code = arrayUniqCode;
            //供应商ID
            string yfgi_gysid = "";
            //供应商名称
            string yfgi_gysname = "";
            //从结算单获取供应商信息
            string sqljsd = "select JS_GYS from PM_CPFYJSD where JS_JHGZH='" + arrayUniq_Code + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqljsd);
            if (dr.HasRows)
            {
                dr.Read();
                //供应商名称
                yfgi_gysname = dr["JS_GYS"].ToString();
            }
            dr.Close();
            //开户银行
            string yfgi_bank = "";
            //发票号码
            string yfgi_fpnum = "";
            //地址
            string yfgi_address = "";

            //日期-创建发票的日期
            //此日期需要根据系统是否关帐来判断发票日期
            string yfgi_date = string.Empty;
            string sql = "select count(*) from TBFM_YFHS where YFHS_YEAR='" + System.DateTime.Now.Year.ToString() + "' and YFHS_MONTH='" + System.DateTime.Now.Month.ToString().PadLeft(2, '0') + "' and YFHS_STATE='1'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (Convert.ToInt16(dt.Rows[0][0]) > 0)
            {
                if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 12)
                {
                    yfgi_date = DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01";
                }
                else if (Convert.ToInt32(DateTime.Now.Month.ToString()) == 12)
                {
                    yfgi_date = DateTime.Now.AddYears(1).AddMonths(-11).ToString("yyyy-MM") + "-01";
                }
            }
            else
            {
                yfgi_date = DateTime.Now.ToString("yyyy-MM-dd");
            }
            //勾稽标志
            string yfgi_gjflag = "0";
            //凭证号
            string yfgi_pzh = "";
            //部门 
            string yfgi_bmid = "";
            //部门名称
            string yfgi_bmname = "财务部";
            //记账人
            string yfgi_jzrid = "";
            //记账人姓名
            string yfgi_jzname = "";
            //制单人
            string yfgi_zdrid = "";
            //制单人姓名
            string yfgi_zdrname = "";
            //状态
            string yfgi_state = "0";//审核状态
            //备注
            string yfgi_note = "";
            //总金额	
            string yfgi_money = "0";
            //含税总金额	GI_CTAMTMNY
            string yfgi_hsmoney = "0";
            sqltext = "insert into TBFM_YFFPTOTAL(YFGI_CODE,YFGI_GYSNAME,YFGI_KHBANK,YFGI_FPNUM,YFGI_ADDRESS,YFGI_DATE,YFGI_GJFLAG,YFGI_PZH,YFGI_DEPID,YFGI_DEPNAME,YFGI_JZID,YFGI_JZNAME,YFGI_ZDID,YFGI_ZDNAME,YFGI_STATE,YFGI_NOTE,YFGI_MONEY,YFGI_HSMONEY)" +
                "Values('" + fp_code + "','" + yfgi_gysname + "','" + yfgi_bank + "','" + yfgi_fpnum + "','" + yfgi_address + "','" + yfgi_date + "','" + yfgi_gjflag + "','" + yfgi_pzh + "','" + yfgi_bmid + "','" + yfgi_bmname + "','" + yfgi_jzrid + "','" + yfgi_jzname + "','" + yfgi_zdrid + "','" + yfgi_zdrname + "','" + yfgi_state + "','" + yfgi_note + "','" + yfgi_money + "','" + yfgi_hsmoney + "')";
            return sqltext;
        }
        /// <summary>
        /// 更新结算单中的钩稽状态：0未生成发票；1：已生成发票；2：已审核；3：已勾稽
        /// </summary>
        /// <returns></returns>
        private string CreateJSD(string Uniqcode)
        {
            string sqltext = "update PM_CPFYJSD set JS_GJSTATE='1' where JS_JHGZH='" + Uniqcode + "'";
            return sqltext;
        }
        /// <summary>
        /// 勾稽关系表-SqlText
        /// </summary>
        /// <returns></returns>
        private string CreateGJrelation(string Uniqcode, string fpcode)
        {
            string sqltext = "insert into TBFM_YFFPDETAIL(YFGI_JSDID,YFGJ_FPID,YFGJ_JHGZH,YFGJ_CPBH,YFGJ_CPMC,YFGJ_NUM,YFGJ_WGHT,YFGJ_JE,YFGJ_SHUILV,YFGJ_HSJE)" +
                       " SELECT JS_BH,'" + fpcode + "',JS_JHGZH,JS_TUHAO,JS_SBMC,JS_BJSL,JS_DANZ,(isnull(cast(JS_HSJE as float),0))/(1+(isnull(JS_SHUIL,0))/100),(isnull(JS_SHUIL,0))/100,JS_HSJE FROM PM_CPFYJSD where JS_JHGZH='" + Uniqcode + "' and (isnull(JS_GJSTATE,0)='0' or isnull(JS_GJSTATE,0)='1') ";
            return sqltext;
        }
        #endregion





        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)//计算合计值
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbdate = (Label)e.Item.FindControl("lbdate");
                Label lbjhgzh = (Label)e.Item.FindControl("lbjhgzh");
                Label lbrwh = (Label)e.Item.FindControl("lbrwh");
                Label lbjsdh = (Label)e.Item.FindControl("lbjsdh");
                Label lbgysmc = (Label)e.Item.FindControl("lbgysmc");
                Label lbzdrxm = (Label)e.Item.FindControl("lbzdrxm");
                Label lbtuhao = (Label)e.Item.FindControl("lbtuhao");
                Label lbmname = (Label)e.Item.FindControl("lbmname");
                Label lbsl = (Label)e.Item.FindControl("lbsl");
                Label lbzl = (Label)e.Item.FindControl("lbzl");
                Label lbje = (Label)e.Item.FindControl("lbje");
                Label lbshuilv = (Label)e.Item.FindControl("lbshuilv");
                Label lbhsje = (Label)e.Item.FindControl("lbhsje");
                if (lbzl.Text == "")
                {
                    lbzl.Text = "0";
                }
                zlhj += Convert.ToDouble(lbzl.Text);
                if (lbje.Text == "")
                {
                    lbje.Text = "0";
                }
                jehj += Convert.ToDouble(lbje.Text);
                if (lbhsje.Text == "")
                {
                    lbhsje.Text = "0";
                }
                hsjehj += Convert.ToDouble(lbhsje.Text);
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lbzlhj = (Label)e.Item.FindControl("lbzlhj");
                Label lbjehj = (Label)e.Item.FindControl("lbjehj");
                Label lbhsjehj = (Label)e.Item.FindControl("lbhsjehj");
                lbzlhj.Text = zlhj.ToString();
                lbjehj.Text = jehj.ToString("0.00");
                lbhsjehj.Text = hsjehj.ToString();
            }
        }
    }
}
