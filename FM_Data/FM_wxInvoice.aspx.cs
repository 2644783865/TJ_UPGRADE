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
    public partial class FM_wxInvoice : System.Web.UI.Page
    {
        string action = "";//Audit：审核；Trick：钩稽
        string wxgicode = "";
        double jehj = 0;//金额合计
        double hsjehj = 0;//含税金额合计
        double sehj = 0;//税额合计
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["Action"];
            wxgicode = Request.QueryString["WXGI_CODE"];
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

                lblInvState.Text = "发票审核-" + wxgicode;
                this.BindInvHead(wxgicode);
                this.BindInvrpt(wxgicode);
            }
            //勾稽
            else if (action == "Trick")
            {
                lblInvState.Text = "发票钩稽-" + wxgicode;
                this.BindInvHead(wxgicode);
                this.BindInvrpt(wxgicode);
            }
            //反勾稽
            else if (action == "TrickReject")
            {
                lblInvState.Text = "反钩稽-" + wxgicode;
                this.BindInvHead(wxgicode);
                this.BindInvrpt(wxgicode);
            }
            //查看
            else
            {
                lblInvState.Text = "查看-" + wxgicode;
                this.BindInvHead(wxgicode);
                this.BindInvrpt(wxgicode);
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

            string sqltext = "select distinct * from View_wxInv where WXGI_CODE='" + fpbh + "' ";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.HasRows)
            {
                dr.Read();
                //发票编号
                txtfpbh.Text = dr["WXGI_CODE"].ToString();
                txtfpbh.Enabled = false;
                //供应商名称
                txtgysname.Text = dr["WXGI_GYSNAME"].ToString();
                txtgysname.Enabled = false;
                //开户银行
                txtkhbank.Text = dr["WXGI_KHBANK"].ToString();
                //发票号码
                txtfphm.Text = dr["WXGI_FPNUM"].ToString();
                //地址
                txtaddress.Text = dr["WXGI_ADDRESS"].ToString();
                //日期
                txtdate.Text = dr["WXGI_DATE"].ToString();//发票创建日期
                txtdate.Enabled = false;
                //核算标志
                rblhsflag.SelectedIndex = dr["WXGJ_HSSTATE"].ToString() == "0" ? 1 : 0;
                rblhsflag.Enabled = false;
                //勾稽标志
                rblgjflag.SelectedIndex = dr["WXGI_GJFLAG"].ToString() == "0" ? 1 : 0;
                rblgjflag.Enabled = false;
                //凭证号
                txtpzh.Text = dr["WXGI_PZH"].ToString();
                //部门名称
                txtdepartment.Text = dr["WXGI_DEPNAME"].ToString();
                txtdepartment.Enabled = false;
                //记账人姓名
                txtjzr.Text = dr["WXGI_JZNAME"].ToString();
                //制单人姓名
                txtzdr.Text = dr["WXGI_ZDNAME"].ToString();
                //审核状态
                rblshflag.SelectedIndex = dr["WXGI_STATE"].ToString() == "0" ? 1 : 0;
                rblshflag.Enabled=false;
                //备注
                txtnote.Text = dr["WXGI_NOTE"].ToString();
            }
            dr.Close();

            //2016.11.08修改
            if (action == "Audit")
            {
                string sqltext_pzh = "select WXGI_JSDID from TBFM_WXGJRELATION where WXGJ_FPID='" + fpbh + "'";
                DataTable dt_pzh = DBCallCommon.GetDTUsingSqlText(sqltext_pzh);
                if (dt_pzh.Rows.Count > 0)
                {
                    //凭证号
                    txtpzh.Text = dt_pzh.Rows[0]["WXGI_JSDID"].ToString();
                }
            }
        }

        private void BindInvrpt(string fpbh)
        {
            string sqltext = "select WXGJ_GJDATE as DATE,WXGJ_JHGZH as JHGZH,WXGI_JSDID as JSDH,WXGI_GYSNAME as GYS,WXGJ_WXPBH as WXJBH,WXGJ_WXPMC as WXJMC,WXGI_ZDNAME as ZDR,WXGJ_COUNT as SL,cast(WXGJ_UPRICE as decimal(12,2)) as DJ,cast(WXGJ_JE as decimal(12,2)) as JE,WXGJ_SHUILV as SHUILV,WXGJ_HSUPRICE as HSDJ,WXGJ_HSJE as HSJE,cast((isnull(WXGJ_HSJE,0)-isnull(WXGJ_JE,0)) as decimal(12,2)) as SE from View_wxInv where WXGJ_FPID='" + fpbh + "'";
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
                Label lbwxjbh = (Label)e.Item.FindControl("lbwxjbh");
                Label lbwxjmc = (Label)e.Item.FindControl("lbwxjmc");
                Label lbzdr = (Label )e.Item.FindControl("lbzdr");

                TextBox tbsl = (TextBox)e.Item.FindControl("tbsl");
                TextBox tbdj = (TextBox)e.Item.FindControl("tbdj");
                TextBox tbje = (TextBox)e.Item.FindControl("tbje");
                TextBox tbshuilv = (TextBox)e.Item.FindControl("tbshuilv");
                TextBox tbhsdj = (TextBox)e.Item.FindControl("tbdj");
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
            string jhgzh="";
            double sl = 0;
            double je = 0;
            double dj = 0;
            double shuilv = 0;
            double hsdj = 0;
            double se=0;
            double hsje = 0;
            string fpbh = txtfpbh.Text.ToString();
            string sqlstring = "";
            //将数据写入发票勾稽关系表
            for (int i = 0; i < rptProNumCost.Items.Count; i++)
            {
                jhgzh= ((Label)rptProNumCost.Items[i].FindControl("lbjhgzh")).Text.ToString();//计划跟踪号
                sl= Convert.ToDouble(((TextBox)rptProNumCost.Items[i].FindControl("tbsl")).Text.ToString());//数量
                dj= Convert.ToDouble(((TextBox)rptProNumCost.Items[i].FindControl("tbdj")).Text.ToString());//单价
                je= Convert.ToDouble(((TextBox)rptProNumCost.Items[i].FindControl("tbje")).Text.ToString());//金额
                shuilv= Convert.ToDouble(((TextBox)rptProNumCost.Items[i].FindControl("tbshuilv")).Text.ToString());//税率
                hsdj= Convert.ToDouble(((TextBox)rptProNumCost.Items[i].FindControl("tbhsdj")).Text.ToString());//含税单价
                hsje= Convert.ToDouble(((TextBox)rptProNumCost.Items[i].FindControl("tbhsje")).Text.ToString());//含税金额
                se= Convert.ToDouble (((TextBox)rptProNumCost.Items[i].FindControl("tbse")).Text.ToString());//税额
                if(sl.ToString()=="0")
                {
                    dj=0;
                    je=0;
                    hsdj=0;
                    hsje=0;
                    se=0;
                }
                string sqltextgjgx="update TBFM_WXGJRELATION set WXGJ_COUNT='"+sl+"',WXGJ_UPRICE='"+dj+"',WXGJ_JE='"+je+"',WXGJ_SHUILV='"+shuilv+"',WXGJ_HSUPRICE='"+hsdj+"',WXGJ_HSJE='"+hsje+"' where WXGJ_JHGZH='"+jhgzh+"'"; 
                sql.Add(sqltextgjgx);
                totalje += je;
                totalhsje += hsje;
            }
            //更新发票总表金额
            sqlstring = "update TBFM_WXGHINVOICETOTAL set WXGI_MONEY ='" + totalje + "',WXGI_HSMONEY ='" + totalhsje + "' where WXGI_CODE='" + fpbh + "'";
            sql.Add(sqlstring);
            //更新发票总表信息
            string fptotalsqltext=getfptotalsqltext();
            sql.Add(fptotalsqltext);
            DBCallCommon.ExecuteTrans(sql);
            //重新绑定发票明细
            this.BindInvrpt(wxgicode);
            this.BindInvHead(wxgicode);
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
            string wxgi_code = txtfpbh.Text.Trim();
            //供应商 
            string wxgigys = txtgysname.Text.Trim();
            //开户银行 
            string wxgibank = txtkhbank.Text.Trim();
            //发票号码 
            string wxginum = txtfphm.Text.Trim();
            //地址 
            string wxgiaddress = txtaddress.Text.Trim();
            //日期 
            string wxgidate = txtdate.Text.Trim();
            //勾稽标志 
            string wxgigjflag = rblgjflag.SelectedValue.ToString();
            //凭证号 
            string wxgipzh = txtpzh.Text.Trim();
            //部门名称 
            string wxgidepname = Session["UserDept"].ToString();
            //记账人姓名 
            string wxgijzname = Session["UserName"].ToString();
            //制单人姓名 
            string wxgizdname = Session["UserName"].ToString();
            //审核状态 
            string wxgistate = rblshflag.SelectedValue.ToString();
            //备注 
            string wxginote = txtnote.Text.Trim();
            string sqltext = "update TBFM_WXGHINVOICETOTAL set WXGI_GYSNAME='" + wxgigys + "',WXGI_KHBANK='" + wxgibank + "',WXGI_FPNUM='" + wxginum + "',WXGI_ADDRESS='" + wxgiaddress + "',WXGI_DATE='" + wxgidate + "',WXGI_GJFLAG='" + wxgigjflag + "',WXGI_PZH='" + wxgipzh + "',WXGI_DEPNAME='" + wxgidepname + "',WXGI_JZNAME='" + wxgijzname + "',WXGI_ZDNAME='" + wxgizdname + "',WXGI_STATE='" + wxgistate + "',WXGI_NOTE='" + wxginote + "'" +
                " where WXGI_CODE='" + wxgi_code + "'";
            return sqltext;
        }


        //审核通过
        protected void btnshpass_Click(object sender, EventArgs e)
        {
            List<string> sqlsh = new List<string>();
            string fpbh = txtfpbh.Text.Trim();
            string sqlgetjhgzh="select WXGJ_JHGZH from TBFM_WXGJRELATION where WXGJ_FPID='"+fpbh+"'";
            DataTable dtjhgzh=DBCallCommon.GetDTUsingSqlText(sqlgetjhgzh);//获取该发票编号的计划跟踪号
            string sqlrelationshstate = "update TBFM_WXGJRELATION set WXGJ_SHSTATE='1',WXGJ_SHRNAME='" + Session["UserName"] + "',WXGJ_SHRID='"+Session["UserID"]+"' where WXGJ_FPID='" + fpbh + "'";//勾稽关系表状态更改
            string sqltotalshstate="update TBFM_WXGHINVOICETOTAL set WXGI_STATE='1' where WXGI_CODE='"+fpbh+"'";//发票总表状态更改
            for(int i=0;i<dtjhgzh.Rows.Count;i++)
            {
                string jhgzh = dtjhgzh.Rows[i]["WXGJ_JHGZH"].ToString();
                string sqljsdstate="update TBMP_ACCOUNTS set TA_GJSTATE='2' where TA_PTC='"+jhgzh+"'";
                sqlsh.Add(sqljsdstate);
            }//结算单
            sqlsh.Add(sqlrelationshstate);//勾稽关系表
            sqlsh.Add(sqltotalshstate);//发票总表
            DBCallCommon.ExecuteTrans(sqlsh);
            btnbaocunxg.Visible = false;
            btnshpass.Visible = false;
            btnshreject.Visible = true;
            btngjpass.Visible=true;
            btngjreject.Visible=false;
            this.BindInvrpt(wxgicode);
            this.BindInvHead(wxgicode);
        }


        //审核驳回
        protected void btnshreject_Click(object sender, EventArgs e)
        {
            List<string> sqlshbh = new List<string>();
            string fpbh = txtfpbh.Text.Trim();
            string sqlgetjhgzh = "select WXGJ_JHGZH from TBFM_WXGJRELATION where WXGJ_FPID='" + fpbh + "'";
            DataTable dtjhgzh = DBCallCommon.GetDTUsingSqlText(sqlgetjhgzh);//获取该发票编号的计划跟踪号
            string sqlrelationshstate = "update TBFM_WXGJRELATION set WXGJ_SHSTATE='0',WXGJ_SHRNAME='',WXGJ_SHRID='' where WXGJ_FPID='" + fpbh + "'";//勾稽关系表状态更改
            string sqltotalshstate = "update TBFM_WXGHINVOICETOTAL set WXGI_STATE='0' where WXGI_CODE='" + fpbh + "'";//发票总表状态更改
            for (int i = 0; i < dtjhgzh.Rows.Count; i++)
            {
                string jhgzh = dtjhgzh.Rows[i]["WXGJ_JHGZH"].ToString();
                string sqljsdstate = "update TBMP_ACCOUNTS set TA_GJSTATE='1' where TA_PTC='" + jhgzh + "'";
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
            this.BindInvrpt(wxgicode);
            this.BindInvHead(wxgicode);
        }


        //勾稽通过
        protected void btngjpass_Click(object sender, EventArgs e)
        {
            List<string> sqlgj = new List<string>();
            string gjyear =string.Empty;
            string gjmonth = string.Empty;
            string gjdate = string.Empty;
            string sql = "select count(*) from TBFM_WXHS where WXHS_YEAR='" + System.DateTime.Now.Year.ToString() + "' and WXHS_MONTH='" + System.DateTime.Now.Month.ToString().PadLeft(2, '0') + "' and WXHS_STATE='1'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (Convert.ToInt16(dt.Rows[0][0]) > 0)
            {
                if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 12)
                {
                    gjyear = DateTime.Now.Year.ToString();
                    gjmonth = DateTime.Now.AddMonths(1).Month.ToString().PadLeft(2,'0');
                    gjdate = DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01";
                }
                else if (Convert.ToInt32(DateTime.Now.Month.ToString()) == 12)
                {
                    gjyear = DateTime.Now.AddYears(1).Year.ToString();
                    gjmonth = DateTime.Now.AddMonths(-11).Month.ToString().PadLeft(2,'0');
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
            string sqlgetjhgzh = "select WXGJ_JHGZH from TBFM_WXGJRELATION where WXGJ_FPID='" + fpbh + "'";
            DataTable dtjhgzh = DBCallCommon.GetDTUsingSqlText(sqlgetjhgzh);//获取该发票编号的计划跟踪号
            string sqlrelationshstate = "update TBFM_WXGJRELATION set WXGJ_MONTH='" + gjmonth + "',WXGJ_YEAR='" + gjyear + "',WXGJ_GJDATE='" + gjdate + "',WXGJ_GJRNAME='" + Session["UserName"] + "',WXGJ_GJRID='"+Session["UserID"]+"' where WXGJ_FPID='" + fpbh + "'";//勾稽关系表时间信息更改
            string sqltotalshstate = "update TBFM_WXGHINVOICETOTAL set WXGI_GJFLAG='1' where WXGI_CODE='" + fpbh + "'";//发票总表状态更改
            for (int i = 0; i < dtjhgzh.Rows.Count; i++)
            {
                string jhgzh = dtjhgzh.Rows[i]["WXGJ_JHGZH"].ToString();
                string sqljsdstate = "update TBMP_ACCOUNTS set TA_GJSTATE='3' where TA_PTC='" + jhgzh + "'";
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
            this.BindInvrpt(wxgicode);
            this.BindInvHead(wxgicode);
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
            string sqlgetjhgzh = "select WXGJ_JHGZH from TBFM_WXGJRELATION where WXGJ_FPID='" + fpbh + "'";
            DataTable dtjhgzh = DBCallCommon.GetDTUsingSqlText(sqlgetjhgzh);//获取该发票编号的计划跟踪号
            string sqlrelationshstate = "update TBFM_WXGJRELATION set WXGJ_MONTH='',WXGJ_YEAR='',WXGJ_GJDATE='',WXGJ_GJRNAME='',WXGJ_GJRID='' where WXGJ_FPID='" + fpbh + "'";//勾稽关系表时间信息更改
            string sqltotalshstate = "update TBFM_WXGHINVOICETOTAL set WXGI_GJFLAG='0' where WXGI_CODE='" + fpbh + "'";//发票总表状态更改
            for (int i = 0; i < dtjhgzh.Rows.Count; i++)
            {
                string jhgzh = dtjhgzh.Rows[i]["WXGJ_JHGZH"].ToString();
                string sqljsdstate = "update TBMP_ACCOUNTS set TA_GJSTATE='2' where TA_PTC='" + jhgzh + "'";
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
            this.BindInvrpt(wxgicode);
            this.BindInvHead(wxgicode);
        }
    }
}
