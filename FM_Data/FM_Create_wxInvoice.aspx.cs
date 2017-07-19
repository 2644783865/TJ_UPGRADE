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
    public partial class FM_Create_wxInvoice : System.Web.UI.Page
    {
        //string arrayJhgzh="";
        double zlhj = 0;
        double jehj = 0;
        double hsjehj = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //arrayJhgzh = Request.QueryString["arrayJhgzh"];
            //string  arrayJHGZH="('"+arrayJhgzh.Replace("/","','")+"')";
            if (!IsPostBack)
            {
                this.BindData();
            }
            string sqltext = "update TBMP_ACCOUNTS set TA_XTSTATE='0' where TA_XTSTATE='1'";
            DBCallCommon.ExeSqlText(sqltext);
        }
        /// <summary>
        /// 当前页面绑定数据
        /// </summary>
        private void BindData()
        {
            string sqltext = "select left(CONVERT(CHAR(10), TA_ZDTIME, 23),10) as TA_ZDDATE,TA_PTC,TA_ENGID,TA_DOCNUM,TA_SUPPLYNAME,TA_ZDRNAME,TA_TUHAO,TA_MNAME,isnull(cast(TA_NUM as int),0) as TA_NUM,isnull(cast(TA_WGHT as float),0) as TA_WGHT,cast(((isnull(cast(TA_PRICE as float),0))/(1+(isnull(PIC_SHUILV,0))/100)) as decimal(12,2)) as PRICE,cast(((isnull(cast(TA_MONEY as float),0))/(1+(isnull(PIC_SHUILV,0))/100)) as decimal(12,2)) as MONEY,(cast((isnull(PIC_SHUILV,0)) as char(6))+'%') as PIC_SHUILV,isnull(cast(TA_PRICE as float),0) as TA_PRICE,isnull(cast(TA_MONEY as float),0) as TA_MONEY,TA_WXTYPE from View_TBMP_ACCOUNTS where TA_XTSTATE='1'";
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
            string  fpbh="FP" + DateTime.Now.ToString("yyyyMMddHHmmss");
            this.CreateInvOpDataBase(fpbh);//创建发票，写入相关数据表中
            this.ClientScript.RegisterStartupScript(GetType(), "js", "location.href ='FM_wxInvoice.aspx?Action=Audit&WXGI_CODE=" + fpbh + "';", true);
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
                sql.Add(this.CreateGJrelation(Uniqcode,fpcode));//更新勾稽关系表    
            }



            //根据发票明细上的金额更新发票总表(总金额、含税金额)
            string str_je = "update TBFM_WXGHINVOICETOTAL set WXGI_MONEY=(select sum(isnull(WXGJ_JE,0)) from TBFM_WXGJRELATION where WXGJ_FPID='" + fpcode + "') where WXGI_CODE='" + fpcode + "'";
            string str_hsje = "update TBFM_WXGHINVOICETOTAL set WXGI_HSMONEY=(select sum(isnull(WXGJ_HSJE,0)) from TBFM_WXGJRELATION where WXGJ_FPID='" + fpcode + "') where WXGI_CODE='" + fpcode + "'";
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
            string  fp_code= fpcode;
            string arrayUniq_Code = arrayUniqCode;
            //供应商ID
            string wxgi_gysid = "";
            //供应商名称
            string wxgi_gysname = "";
            //从结算单获取供应商信息
            string sqljsd = "select TA_SUPPLYID,TA_SUPPLYNAME from View_TBMP_ACCOUNTS where TA_PTC='" + arrayUniq_Code + "'";
            SqlDataReader dr=DBCallCommon.GetDRUsingSqlText(sqljsd);
            if(dr.HasRows)
            {
                dr.Read();
                //供应商
                wxgi_gysid = dr["TA_SUPPLYID"].ToString();
                //供应商名称
                wxgi_gysname = dr["TA_SUPPLYNAME"].ToString();
            }
            dr.Close();
            //开户银行
            string wxgi_bank = "";
            //发票号码
            string wxgi_fpnum = "";
            //地址
            string wxgi_address = "";

            //日期-创建发票的日期
            //此日期需要根据系统是否关帐来判断发票日期
            string wxgi_date = string.Empty;
            string sql = "select count(*) from TBFM_WXHS where WXHS_YEAR='" + System.DateTime.Now.Year.ToString() + "' and WXHS_MONTH='" + System.DateTime.Now.Month.ToString().PadLeft(2, '0') + "' and WXHS_STATE='1'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (Convert.ToInt16(dt.Rows[0][0]) > 0)
            {
                if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 12)
                {
                    wxgi_date = DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01";
                }
                else if(Convert.ToInt32(DateTime.Now.Month.ToString())==12)
                {
                    wxgi_date = DateTime.Now.AddYears(1).AddMonths(-11).ToString("yyyy-MM") + "-01";
                }
            }
            else
            {
                wxgi_date = DateTime.Now.ToString("yyyy-MM-dd");
            }
            //勾稽标志
            string wxgi_gjflag = "0";
            //凭证号
            string  wxgi_pzh="";
            //部门 
            string wxgi_bmid = "";
            //部门名称
            string  wxgi_bmname="财务部";
            //记账人
            string  wxgi_jzrid="";
            //记账人姓名
            string  wxgi_jzname="";
            //制单人
            string  wxgi_zdrid="";
            //制单人姓名
            string  wxgi_zdrname="";
            //状态
            string  wxgi_state="0";//审核状态
            //备注
            string  wxgi_note="";
            //总金额	
            string wxgi_money= "0"; 
            //含税总金额	GI_CTAMTMNY
            string wxgi_hsmoney="0";
            sqltext = "insert into TBFM_WXGHINVOICETOTAL(WXGI_CODE,WXGI_GYSID,WXGI_GYSNAME,WXGI_KHBANK,WXGI_FPNUM,WXGI_ADDRESS,WXGI_DATE,WXGI_GJFLAG,WXGI_PZH,WXGI_DEPID,WXGI_DEPNAME,WXGI_JZID,WXGI_JZNAME,WXGI_ZDID,WXGI_ZDNAME,WXGI_STATE,WXGI_NOTE,WXGI_MONEY,WXGI_HSMONEY)" +
                "Values('" + fp_code + "','" + wxgi_gysid + "','" + wxgi_gysname + "','" + wxgi_bank + "','" + wxgi_fpnum + "','" + wxgi_address + "','" + wxgi_date + "','" + wxgi_gjflag + "','" + wxgi_pzh + "','" + wxgi_bmid + "','" + wxgi_bmname + "','" + wxgi_jzrid + "','" + wxgi_jzname + "','" + wxgi_zdrid + "','" + wxgi_zdrname + "','" + wxgi_state + "','" + wxgi_note + "','" + wxgi_money + "','" + wxgi_hsmoney + "')";
            return sqltext;
        }
        /// <summary>
        /// 更新结算单中的钩稽状态：0未生成发票；1：已生成发票；2：已审核；3：已勾稽
        /// </summary>
        /// <returns></returns>
        private string CreateJSD(string Uniqcode)
        {
            string sqltext = "update TBMP_ACCOUNTS set TA_GJSTATE='1' where TA_PTC='" + Uniqcode + "'";
            return sqltext;
        }
        /// <summary>
        /// 勾稽关系表-SqlText
        /// </summary>
        /// <returns></returns>
        private string CreateGJrelation(string Uniqcode,string fpcode)
        {
            string sqltext = "insert into TBFM_WXGJRELATION(WXGI_JSDID,WXGJ_FPID,WXGJ_JHGZH,WXGJ_WXPBH,WXGJ_WXPMC,WXGJ_GUIGE,WXGJ_CAIZHI,WXGJ_COUNT,WXGJ_WGHT,WXGJ_UPRICE,WXGJ_JE,WXGJ_SHUILV,WXGJ_HSUPRICE,WXGJ_HSJE)" +
                       " SELECT TA_DOCNUM,'" + fpcode + "',TA_PTC,TA_TUHAO,TA_MNAME,TA_GUIGE,TA_CAIZHI,TA_NUM,TA_WGHT,(isnull(cast(TA_PRICE as float),0))/(1+(isnull(PIC_SHUILV,0))/100),(isnull(cast(TA_MONEY as float),0))/(1+(isnull(PIC_SHUILV,0))/100),(isnull(PIC_SHUILV,0))/100,isnull(TA_PRICE,0),TA_MONEY FROM View_TBMP_ACCOUNTS where TA_PTC='" + Uniqcode + "' and (isnull(TA_GJSTATE,0)='0' or isnull(TA_GJSTATE,0)='1') ";
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
                Label lbdj = (Label)e.Item.FindControl("lbdj");
                Label lbje = (Label)e.Item.FindControl("lbje");
                Label lbshuilv = (Label)e.Item.FindControl("lbshuilv");
                Label lbhsdj = (Label)e.Item.FindControl("lbhsdj");
                Label lbhsje = (Label)e.Item.FindControl("lbhsje");
                Label lbwxtype = (Label)e.Item.FindControl("lbwxtype");
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
       



