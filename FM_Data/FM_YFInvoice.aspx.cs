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
using System.Collections.Generic;

namespace ZCZJ_DPF.FM_Data
{
    public partial class FM_YFInvoice : System.Web.UI.Page
    {
        string action = "";//Audit：审核；Trick：钩稽
        string yfgicode = "";
        double jehj = 0;//金额合计
        double hsjehj = 0;//含税金额合计
        double sehj = 0;//税额合计
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["Action"];
            yfgicode = Request.QueryString["YFGI_CODE"];
            if (!IsPostBack)
            {
                this.InitPage();
                this.InitControl();//控件可用性初始化
            }
        }
        /// <summary>
        /// 初始化页面，根据所执行的操作设置按钮可见性
        /// </summary>
        private void InitPage()
        {
            //审核
            if (action == "Audit")
            {

                lblInvState.Text = "发票审核-" + yfgicode;
                this.BindInvHead(yfgicode);
                this.BindInvrpt(yfgicode);
            }
            //勾稽
            else if (action == "Trick")
            {
                lblInvState.Text = "发票钩稽-" + yfgicode;
                this.BindInvHead(yfgicode);
                this.BindInvrpt(yfgicode);
            }
            //反勾稽
            else if (action == "TrickReject")
            {
                lblInvState.Text = "反钩稽-" + yfgicode;
                this.BindInvHead(yfgicode);
                this.BindInvrpt(yfgicode);
            }
            //查看
            else
            {
                lblInvState.Text = "查看-" + yfgicode;
                this.BindInvHead(yfgicode);
                this.BindInvrpt(yfgicode);
            }
        }
        //控件可用性初始化
        private void InitControl()
        {
            if (action == "Audit")
            {
                //保存修改
                btnbaocunxg.Visible = true;
                //审核通过
                btnshpass.Visible = true;
                //审核驳回
                btnshreject.Visible = false;
                //钩稽通过
                btngjpass.Visible = false;
                //反勾稽
                btngjreject.Visible = false;

                txtjzr.Text = Session["UserName"].ToString();

                txtzdr.Text = Session["UserName"].ToString();
            }
            else if (action == "Trick")//勾稽通过
            {
                btnbaocunxg.Visible = false;
                btnshpass.Visible = false;
                btnshreject.Visible = true;//审核驳回，只修改结算单和发票的审核状态
                btngjpass.Visible = true;
                btngjreject.Visible = false;
            }
            else if (action == "TrickReject")//反勾稽
            {
                btnbaocunxg.Visible = false;
                btnshpass.Visible = false;
                btnshreject.Visible = false;
                btngjpass.Visible = false;
                btngjreject.Visible = true;
            }
            else//查看
            {
                btnbaocunxg.Visible = false;
                btnshpass.Visible = false;
                btnshreject.Visible = false;
                btngjpass.Visible = false;
                btngjreject.Visible = false;
            }
        }
        /// <summary>
        /// 发票基本信息
        /// </summary>
        private void BindInvHead(string fpbh)
        {

            string sqltext = "select distinct * from View_yfInv where YFGI_CODE='" + fpbh + "' ";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.HasRows)
            {
                dr.Read();
                //发票编号
                txtfpbh.Text = dr["YFGI_CODE"].ToString();
                txtfpbh.Enabled = false;
                //供应商名称
                txtgysname.Text = dr["YFGI_GYSNAME"].ToString();
                txtgysname.Enabled = false;
                //开户银行
                txtkhbank.Text = dr["YFGI_KHBANK"].ToString();
                //发票号码
                txtfphm.Text = dr["YFGI_FPNUM"].ToString();
                //地址
                txtaddress.Text = dr["YFGI_ADDRESS"].ToString();
                //日期
                txtdate.Text = dr["YFGI_DATE"].ToString();//发票创建日期
                txtdate.Enabled = false;
                //核算标志
                rblhsflag.SelectedIndex = dr["YFGJ_HSSTATE"].ToString() == "0" ? 1 : 0;
                rblhsflag.Enabled = false;
                //勾稽标志
                rblgjflag.SelectedIndex = dr["YFGI_GJFLAG"].ToString() == "0" ? 1 : 0;
                rblgjflag.Enabled = false;
                //凭证号
                txtpzh.Text = dr["YFGI_PZH"].ToString();
                //部门名称
                txtdepartment.Text = dr["YFGI_DEPNAME"].ToString();
                txtdepartment.Enabled = false;
                //记账人姓名
                txtjzr.Text = dr["YFGI_JZNAME"].ToString();
                //制单人姓名
                txtzdr.Text = dr["YFGI_ZDNAME"].ToString();
                //审核状态
                rblshflag.SelectedIndex = dr["YFGI_STATE"].ToString() == "0" ? 1 : 0;
                rblshflag.Enabled = false;
                //备注
                txtnote.Text = dr["YFGI_NOTE"].ToString();
            }
            dr.Close();

            //2016.11.08修改
            if (action == "Audit")
            {
                string sqltext_pzh = "select YFGI_JSDID from TBFM_YFFPDETAIL where YFGJ_FPID='" + fpbh + "'";
                DataTable dt_pzh = DBCallCommon.GetDTUsingSqlText(sqltext_pzh);
                if (dt_pzh.Rows.Count > 0)
                {
                    //凭证号
                    txtpzh.Text = dt_pzh.Rows[0]["YFGI_JSDID"].ToString();
                }
            }
        }

        private void BindInvrpt(string fpbh)
        {
            string sqltext = "select YFGJ_GJDATE,YFGJ_JHGZH,YFGI_JSDID,YFGI_GYSNAME,YFGJ_CPBH,YFGJ_CPMC,YFGI_ZDNAME,YFGJ_NUM,YFGJ_WGHT,cast(YFGJ_JE as decimal(12,2)) as YFGJ_JE,YFGJ_SHUILV,YFGJ_HSJE,cast((isnull(YFGJ_HSJE,0)-isnull(YFGJ_JE,0)) as decimal(12,2)) as SE from View_yfInv where YFGJ_FPID='" + fpbh + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            rptProNumCost.DataSource = dt;
            rptProNumCost.DataBind();

        }

        //数据汇总

        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbdate = (Label)e.Item.FindControl("lbdate");
                Label lbjhgzh = (Label)e.Item.FindControl("lbjhgzh");
                Label lbjsdh = (Label)e.Item.FindControl("lbjsdh");
                Label lbgys = (Label)e.Item.FindControl("lbgys");
                Label lbcpbh = (Label)e.Item.FindControl("lbcpbh");
                Label lbcpmc = (Label)e.Item.FindControl("lbcpmc");
                Label lbzdr = (Label)e.Item.FindControl("lbzdr");
                Label lbzl = (Label)e.Item.FindControl("lbzl");  
                Label lbsl = (Label)e.Item.FindControl("lbsl");
                TextBox tbje = (TextBox)e.Item.FindControl("tbje");
                TextBox tbshuilv = (TextBox)e.Item.FindControl("tbshuilv");
                TextBox tbhsje = (TextBox)e.Item.FindControl("tbhsje");
                TextBox tbse = (TextBox)e.Item.FindControl("tbse");
                if (tbje.Text == "")
                {
                    tbje.Text = "0";
                }
                jehj += Convert.ToDouble(tbje.Text);
                if (tbhsje.Text == "")
                {
                    tbhsje.Text = "0";
                }
                hsjehj += Convert.ToDouble(tbhsje.Text);
                if (tbse.Text == "")
                {
                    tbse.Text = "0";
                }
                sehj += Convert.ToDouble(tbse.Text);
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lbjehj = (Label)e.Item.FindControl("lbjehj");
                Label lbhsjehj = (Label)e.Item.FindControl("lbhsjehj");
                Label lbsehj = (Label)e.Item.FindControl("lbsehj");
                lbjehj.Text = jehj.ToString();
                lbhsjehj.Text = hsjehj.ToString();
                lbsehj.Text = sehj.ToString();
            }

        }

        //保存修改
        protected void btnbaocunxg_Click(object sender, EventArgs e)
        {
            List<string> sql = new List<string>();

            double totalje = 0;
            double totalhsje = 0;
            string jhgzh = "";
            double sl = 0;
            double je = 0;
            double zl = 0;
            double shuilv = 0;
            double se = 0;
            double hsje = 0;
            string fpbh = txtfpbh.Text.ToString();
            string sqlstring = "";
            //将数据写入发票勾稽关系表
            for (int i = 0; i < rptProNumCost.Items.Count; i++)
            {
                jhgzh = ((Label)rptProNumCost.Items[i].FindControl("lbjhgzh")).Text.ToString();//计划跟踪号
                sl = CommonFun.ComTryDouble(((Label)rptProNumCost.Items[i].FindControl("lbsl")).Text.ToString());//数量
                zl = CommonFun.ComTryDouble(((Label)rptProNumCost.Items[i].FindControl("lbzl")).Text.ToString());//重量
                je = CommonFun.ComTryDouble(((TextBox)rptProNumCost.Items[i].FindControl("tbje")).Text.ToString());//金额
                shuilv = CommonFun.ComTryDouble(((TextBox)rptProNumCost.Items[i].FindControl("tbshuilv")).Text.ToString());//税率
                hsje = CommonFun.ComTryDouble(((TextBox)rptProNumCost.Items[i].FindControl("tbhsje")).Text.ToString());//含税金额
                se = CommonFun.ComTryDouble(((TextBox)rptProNumCost.Items[i].FindControl("tbse")).Text.ToString());//税额
                //if (sl.ToString() == "0")
                //{
                //    zl = 0;
                //    je = 0;
                //    hsje = 0;
                //    se = 0;
                //}
                string sqltextgjgx = "update TBFM_YFFPDETAIL set YFGJ_NUM='" + sl + "',YFGJ_WGHT='" + zl + "',YFGJ_JE='" + je + "',YFGJ_SHUILV='" + shuilv + "',YFGJ_HSJE='" + hsje + "' where YFGJ_JHGZH='" + jhgzh + "'";
                sql.Add(sqltextgjgx);
                totalje += je;
                totalhsje += hsje;
            }
            //更新发票总表金额
            sqlstring = "update TBFM_YFFPTOTAL set YFGI_MONEY ='" + totalje + "',YFGI_HSMONEY ='" + totalhsje + "' where YFGI_CODE='" + fpbh + "'";
            sql.Add(sqlstring);
            //更新发票总表信息
            string fptotalsqltext = getfptotalsqltext();
            sql.Add(fptotalsqltext);
            DBCallCommon.ExecuteTrans(sql);
            //重新绑定发票明细
            this.BindInvrpt(yfgicode);
            this.BindInvHead(yfgicode);
            btnbaocunxg.Visible = true;
            btnshpass.Visible = true;
            btnshreject.Visible = false;
            btngjpass.Visible = false;
            btngjreject.Visible = false;

        }


        //获取修改发票总表信息的字符串
        private string getfptotalsqltext()
        {
            //发票编号 
            string yfgi_code = txtfpbh.Text.Trim();
            //供应商 
            string yfgigys = txtgysname.Text.Trim();
            //开户银行 
            string yfgibank = txtkhbank.Text.Trim();
            //发票号码 
            string yfginum = txtfphm.Text.Trim();
            //地址 
            string yfgiaddress = txtaddress.Text.Trim();
            //日期 
            string yfgidate = txtdate.Text.Trim();
            //勾稽标志 
            string yfgigjflag = rblgjflag.SelectedValue.ToString();
            //凭证号 
            string yfgipzh = txtpzh.Text.Trim();
            //部门名称 
            string yfgidepname = Session["UserDept"].ToString();
            //记账人姓名 
            string yfgijzname = Session["UserName"].ToString();
            //制单人姓名 
            string yfgizdname = Session["UserName"].ToString();
            //审核状态 
            string yfgistate = rblshflag.SelectedValue.ToString();
            //备注 
            string yfginote = txtnote.Text.Trim();
            string sqltext = "update TBFM_YFFPTOTAL set YFGI_GYSNAME='" + yfgigys + "',YFGI_KHBANK='" + yfgibank + "',YFGI_FPNUM='" + yfginum + "',YFGI_ADDRESS='" + yfgiaddress + "',YFGI_DATE='" + yfgidate + "',YFGI_GJFLAG='" + yfgigjflag + "',YFGI_PZH='" + yfgipzh + "',YFGI_DEPNAME='" + yfgidepname + "',YFGI_JZNAME='" + yfgijzname + "',YFGI_ZDNAME='" + yfgizdname + "',YFGI_STATE='" + yfgistate + "',YFGI_NOTE='" + yfginote + "'" +
                " where YFGI_CODE='" + yfgi_code + "'";
            return sqltext;
        }


        //审核通过
        protected void btnshpass_Click(object sender, EventArgs e)
        {
            List<string> sqlsh = new List<string>();
            string fpbh = txtfpbh.Text.Trim();
            string sqlgetjhgzh = "select YFGJ_JHGZH from TBFM_YFFPDETAIL where YFGJ_FPID='" + fpbh + "'";
            DataTable dtjhgzh = DBCallCommon.GetDTUsingSqlText(sqlgetjhgzh);//获取该发票编号的计划跟踪号
            string sqlrelationshstate = "update TBFM_YFFPDETAIL set YFGJ_SHSTATE='1',YFGJ_SHRNAME='" + Session["UserName"] + "',YFGJ_SHRID='" + Session["UserID"] + "' where YFGJ_FPID='" + fpbh + "'";//勾稽关系表状态更改
            string sqltotalshstate = "update TBFM_YFFPTOTAL set YFGI_STATE='1' where YFGI_CODE='" + fpbh + "'";//发票总表状态更改
            for (int i = 0; i < dtjhgzh.Rows.Count; i++)
            {
                string jhgzh = dtjhgzh.Rows[i]["YFGJ_JHGZH"].ToString();
                string sqljsdstate = "update PM_CPFYJSD set JS_GJSTATE='2' where JS_JHGZH='" + jhgzh + "'";
                sqlsh.Add(sqljsdstate);
            }//结算单
            sqlsh.Add(sqlrelationshstate);//勾稽关系表
            sqlsh.Add(sqltotalshstate);//发票总表
            DBCallCommon.ExecuteTrans(sqlsh);
            btnbaocunxg.Visible = false;
            btnshpass.Visible = false;
            btnshreject.Visible = true;
            btngjpass.Visible = true;
            btngjreject.Visible = false;
            this.BindInvrpt(yfgicode);
            this.BindInvHead(yfgicode);
        }


        //审核驳回
        protected void btnshreject_Click(object sender, EventArgs e)
        {
            List<string> sqlshbh = new List<string>();
            string fpbh = txtfpbh.Text.Trim();
            string sqlgetjhgzh = "select YFGJ_JHGZH from TBFM_YFFPDETAIL where YFGJ_FPID='" + fpbh + "'";
            DataTable dtjhgzh = DBCallCommon.GetDTUsingSqlText(sqlgetjhgzh);//获取该发票编号的计划跟踪号
            string sqlrelationshstate = "update TBFM_YFFPDETAIL set YFGJ_SHSTATE='0',YFGJ_SHRNAME='',YFGJ_SHRID='' where YFGJ_FPID='" + fpbh + "'";//勾稽关系表状态更改
            string sqltotalshstate = "update TBFM_YFFPTOTAL set YFGI_STATE='0' where YFGI_CODE='" + fpbh + "'";//发票总表状态更改
            for (int i = 0; i < dtjhgzh.Rows.Count; i++)
            {
                string jhgzh = dtjhgzh.Rows[i]["YFGJ_JHGZH"].ToString();
                string sqljsdstate = "update PM_CPFYJSD set JS_GJSTATE='1' where JS_JHGZH='" + jhgzh + "'";
                sqlshbh.Add(sqljsdstate);
            }//结算单
            sqlshbh.Add(sqlrelationshstate);//勾稽关系表
            sqlshbh.Add(sqltotalshstate);//发票总表
            DBCallCommon.ExecuteTrans(sqlshbh);
            btnbaocunxg.Visible = true;
            btnshpass.Visible = true;
            btnshreject.Visible = false;
            btngjpass.Visible = false;
            btngjreject.Visible = false;
            this.BindInvrpt(yfgicode);
            this.BindInvHead(yfgicode);
        }


        //勾稽通过
        protected void btngjpass_Click(object sender, EventArgs e)
        {
            List<string> sqlgj = new List<string>();
            string gjyear = string.Empty;
            string gjmonth = string.Empty;
            string gjdate = string.Empty;
            string sql = "select count(*) from TBFM_YFHS where YFHS_YEAR='" + System.DateTime.Now.Year.ToString() + "' and YFHS_MONTH='" + System.DateTime.Now.Month.ToString().PadLeft(2, '0') + "' and YFHS_STATE='1'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (Convert.ToInt16(dt.Rows[0][0]) > 0)
            {
                if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 12)
                {
                    gjyear = DateTime.Now.Year.ToString();
                    gjmonth = DateTime.Now.AddMonths(1).Month.ToString().PadLeft(2, '0');
                    gjdate = DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01";
                }
                else if (Convert.ToInt32(DateTime.Now.Month.ToString()) == 12)
                {
                    gjyear = DateTime.Now.AddYears(1).Year.ToString();
                    gjmonth = DateTime.Now.AddMonths(-11).Month.ToString().PadLeft(2, '0');
                    gjdate = DateTime.Now.AddYears(1).AddMonths(-11).ToString("yyyy-MM") + "-01";
                }
            }
            else
            {
                gjyear = DateTime.Now.Year.ToString();
                gjmonth = DateTime.Now.Month.ToString().PadLeft(2, '0');
                gjdate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            string fpbh = txtfpbh.Text.Trim();
            string sqlgetjhgzh = "select YFGJ_JHGZH from TBFM_YFFPDETAIL where YFGJ_FPID='" + fpbh + "'";
            DataTable dtjhgzh = DBCallCommon.GetDTUsingSqlText(sqlgetjhgzh);//获取该发票编号的计划跟踪号
            string sqlrelationshstate = "update TBFM_YFFPDETAIL set YFGJ_MONTH='" + gjmonth + "',YFGJ_YEAR='" + gjyear + "',YFGJ_GJDATE='" + gjdate + "',YFGJ_GJRNAME='" + Session["UserName"] + "',YFGJ_GJRID='" + Session["UserID"] + "' where YFGJ_FPID='" + fpbh + "'";//勾稽关系表时间信息更改
            string sqltotalshstate = "update TBFM_YFFPTOTAL set YFGI_GJFLAG='1' where YFGI_CODE='" + fpbh + "'";//发票总表状态更改
            for (int i = 0; i < dtjhgzh.Rows.Count; i++)
            {
                string jhgzh = dtjhgzh.Rows[i]["YFGJ_JHGZH"].ToString();
                string sqljsdstate = "update PM_CPFYJSD set JS_GJSTATE='3' where JS_JHGZH='" + jhgzh + "'";
                sqlgj.Add(sqljsdstate);
            }//结算单
            sqlgj.Add(sqlrelationshstate);//勾稽关系表
            sqlgj.Add(sqltotalshstate);//发票总表
            DBCallCommon.ExecuteTrans(sqlgj);
            btnbaocunxg.Visible = false;
            btnshpass.Visible = false;
            btnshreject.Visible = false;
            btngjpass.Visible = false;
            btngjreject.Visible = true;
            this.BindInvrpt(yfgicode);
            this.BindInvHead(yfgicode);
        }

        //勾稽驳回
        protected void btngjreject_Click(object sender, EventArgs e)
        {
            List<string> sqlgjbh = new List<string>();
            string gjyear = DateTime.Now.Year.ToString();
            string gjmonth = DateTime.Now.Month.ToString();
            string gjdate = DateTime.Now.ToString("yyyy-MM-dd");
            if (Convert.ToInt32(gjmonth) < 10)
            {
                gjmonth = "0" + gjmonth;
            }
            string fpbh = txtfpbh.Text.Trim();
            string sqlgetjhgzh = "select YFGJ_JHGZH from TBFM_YFFPDETAIL where YFGJ_FPID='" + fpbh + "'";
            DataTable dtjhgzh = DBCallCommon.GetDTUsingSqlText(sqlgetjhgzh);//获取该发票编号的计划跟踪号
            string sqlrelationshstate = "update TBFM_YFFPDETAIL set YFGJ_MONTH='',YFGJ_YEAR='',YFGJ_GJDATE='',YFGJ_GJRNAME='',YFGJ_GJRID='' where YFGJ_FPID='" + fpbh + "'";//勾稽关系表时间信息更改
            string sqltotalshstate = "update TBFM_YFFPTOTAL set YFGI_GJFLAG='0' where YFGI_CODE='" + fpbh + "'";//发票总表状态更改
            for (int i = 0; i < dtjhgzh.Rows.Count; i++)
            {
                string jhgzh = dtjhgzh.Rows[i]["YFGJ_JHGZH"].ToString();
                string sqljsdstate = "update PM_CPFYJSD set JS_GJSTATE='2' where JS_JHGZH='" + jhgzh + "'";
                sqlgjbh.Add(sqljsdstate);
            }//结算单
            sqlgjbh.Add(sqlrelationshstate);//勾稽关系表
            sqlgjbh.Add(sqltotalshstate);//发票总表
            DBCallCommon.ExecuteTrans(sqlgjbh);
            btnbaocunxg.Visible = false;
            btnshpass.Visible = false;
            btnshreject.Visible = true;
            btngjpass.Visible = true;
            btngjreject.Visible = false;
            this.BindInvrpt(yfgicode);
            this.BindInvHead(yfgicode);
        }
    }
}
