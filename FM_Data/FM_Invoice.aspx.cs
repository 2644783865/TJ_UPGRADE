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
    public partial class FM_Invoice : System.Web.UI.Page
    {
        string action = "";//Audit：审核；Trick：钩稽
        string gicode = "";
        double je = 0;//汇总金额
        double se = 0;//税额
        double hsje = 0;//汇总含税金额

        //Meng 2013年3月5日 16:24:25
        double num = 0;//数量
        double dj = 0; //单价
        double hsdj = 0;//含税单价

        double wje = 0;
        double wse = 0;
        double whsje = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["Action"];
            gicode = Request.QueryString["GI_CODE"];
            if (!IsPostBack)
            {
                this.InitPage();
                this.InitControl();
            }
        }
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            //审核
            if (action == "Audit")
            {

                lblInvState.Text = "发票审核-" + gicode;
                this.BindInvTotal(gicode);
                this.BindInvDetail(gicode);
                BindInvWIDetail(gicode);
                PanelIn.Visible = false;
            }
            //勾稽
            else if (action == "Trick")
            {
                lblInvState.Text = "发票钩稽-" + gicode;
                this.BindInvTotal(gicode);
                this.BindInvDetail(gicode);
                BindInvWIDetail(gicode);
            }
            //取消勾稽
            else if (action == "TrickOff")
            {
                lblInvState.Text = "取消钩稽-" + gicode;
                this.BindInvTotal(gicode);
                this.BindInvDetail(gicode);
                btnsave.Visible = false;
                BindInvWIDetail(gicode);
                
            }
            //查看
            else
            {
                lblInvState.Text = "查看-" + gicode;
                this.BindInvTotal(gicode);
                this.BindInvDetail(gicode);
                btnsave.Visible = false;
                BindInvWIDetail(gicode);
            }
        }
        //控件可用性初始化
        private void InitControl()
        {
            if (action == "Audit")
            {
                //取消钩稽
                btnTrickOff.Visible = false;

                //审核通过
                btnAuditPass.Visible = true;
                //审核驳回
                btnAuditReject.Visible = true;
                //钩稽通过
                btnTrickPass.Visible = false;
                //钩稽驳回
                btnTrickReject.Visible = false;

                txtGI_JZNM.Text = Session["UserName"].ToString();

                txtGI_ZDNM.Text = Session["UserName"].ToString();
            }
            else if (action == "Trick")
            {
                //保存
                btnbaocun.Visible = false;
                //修改单价
                btnsave.Visible = false;
                //取消钩稽
                btnTrickOff.Visible = false;
                //审核通过
                btnAuditPass.Visible = false;
                //审核通过
                btnAuditReject.Visible = false;
                //钩稽通过
                btnTrickPass.Visible = true;
                //钩稽驳回
                btnTrickReject.Visible = true;
            }
            else if (action == "TrickOff")
            {
                //保存
                btnbaocun.Visible = false;
                //修改单价
                btnsave.Visible = false;
                //取消钩稽
                btnTrickOff.Visible = true;
                //审核通过
                btnAuditPass.Visible = false;
                //审核通过
                btnAuditReject.Visible = false;
                //钩稽通过
                btnTrickPass.Visible = false;
                //钩稽驳回
                btnTrickReject.Visible = false;
            }
            else//查看
            {
                //修改单价
                btnsave.Visible = false;
                //取消钩稽
                btnTrickOff.Visible = false;
                //审核通过
                btnAuditPass.Visible = false;
                //审核通过
                btnAuditReject.Visible = false;
                //钩稽通过
                btnTrickPass.Visible = false;
                //钩稽驳回
                btnTrickReject.Visible = false;
                //详细信息
                //grvInvDetail.Columns[grvInvDetail.Columns.Count - 1].Visible = false;
            }
        }
        /// <summary>
        /// 发票基本信息
        /// </summary>
        /// <param name="fpdh"></param>
        private void BindInvTotal(string fpdh)
        {

            string sqltext = "select * from Inv_View where GI_CODE='" + fpdh + "' ";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.HasRows)
            {
                dr.Read();
                //编号
                txtGI_CODE.Text = dr["GI_CODE"].ToString();
                txtGI_CODE.Enabled = false;
                //供应商名称
                txtGI_SUPPLIERNM.Text = dr["GI_SUPPLIERNM"].ToString();
                txtGI_SUPPLIERNM.Enabled = false;
                //开户银行
                txtGI_ACCBANK.Text = dr["GI_ACCBANK"].ToString();
                //发票号码
                txtGI_INVOICENO.Text = dr["GI_INVOICENO"].ToString();
                //地址
                txtGI_ADDRESS.Text = dr["GI_ADDRESS"].ToString();
                //日期
                txtGI_DATE.Text = dr["GI_DATE"].ToString();//发票创建日期
                //txtGI_DATE.Enabled = false;

                rblSSHS.SelectedIndex = dr["GJ_HSSTATE"].ToString() == "0" ? 1 : 0;
                rblSSHS.Enabled = false;
                //勾稽标志
                rblGI_GJFLAG.SelectedIndex = dr["GI_GJFLAG"].ToString() == "0" ? 1 : 0;

                //凭证号
                txtGI_PZH.Text = dr["GI_PZH"].ToString();
                //部门名称
                txtGI_DEPNM.Text = dr["GI_DEPNM"].ToString();
                txtGI_DEPNM.Enabled = false;
                //记账人姓名
                txtGI_JZNM.Text = dr["GI_JZNM"].ToString();
                //制单人姓名
                txtGI_ZDNM.Text = dr["GI_ZDNM"].ToString();
                //状态
                rblGI_STATE.SelectedIndex = dr["GI_STATE"].ToString() == "0" ? 1 : 0;
                //备注
                txtGI_NOTE.Text = dr["GI_NOTE"].ToString();
            }
            dr.Close();
        }
        /// <summary>
        /// 发票明细
        /// </summary>
        /// <param name="fpdh"></param>
        private void BindInvDetail(string fpdh)
        {
            string sqltext = "select UNIQUEID,WG_CODE,WG_MARID,MNAME,GUIGE,PURCUNIT,cast(WG_RSNUM as float) as WG_RSNUM,WG_TAXRATE,cast(round(WG_UPRICE,4) as float) as WG_UPRICE,cast(round(WG_CTAXUPRICE,4) as float) as WG_CTAXUPRICE,cast(round(WG_AMOUNT,2) as float) as WG_AMOUNT,cast(round(WG_CTAMTMNY,2) as float) as WG_CTAMTMNY,round((round(WG_CTAMTMNY,2)-round(WG_AMOUNT,2)),2) as WG_SE,WG_CTYPE from dbo.MS_IN_GJFP ('" + fpdh + "') order by UNIQUEID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            grvInvDetail.DataSource = dt;
            grvInvDetail.DataBind();

        }

        //数据汇总

        protected void grvInvDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtJE = (TextBox)e.Row.FindControl("txtGI_je");
                je += Convert.ToDouble(txtJE.Text);//金额

                TextBox txtSE = (TextBox)e.Row.FindControl("txtGI_se");
                se += Convert.ToDouble(txtSE.Text);//税额

                TextBox txtHSJE = (TextBox)e.Row.FindControl("txtGI_HJPRICE");
                hsje += Convert.ToDouble(txtHSJE.Text);//含税金额

                //MENG 2013年3月5日 16:22:10
                TextBox txtNUM = (TextBox)e.Row.FindControl("text_sj");
                num += Convert.ToDouble(txtNUM.Text);//数量

                TextBox txtDJ = (TextBox)e.Row.FindControl("txtGI_dj");
                dj += Convert.ToDouble(txtDJ.Text);//单价

                TextBox txtHSDJ = (TextBox)e.Row.FindControl("txtGI_TADJ");
                hsdj += Convert.ToDouble(txtHSDJ.Text);//含税单价




                //显示物料的国标
                Label materialcode0 = (Label)e.Row.FindControl("lbmid");
                string sql000 = "select GB from TBMA_MATERIAL where ID='" + materialcode0.Text.Trim() + "'";
                DataTable dt000 = DBCallCommon.GetDTUsingSqlText(sql000);
                if (dt000.Rows.Count > 0)
                {
                    Label guobiao0 = (Label)e.Row.FindControl("lbguobiao");
                    guobiao0.Text = dt000.Rows[0]["GB"].ToString().Trim();
                }
            }

            else if (e.Row.RowType == DataControlRowType.Footer)
            {

                Label lb_je = (Label)e.Row.FindControl("lbje");

                lb_je.Text = string.Format("{0:c2}", je);

                Label lb_se = (Label)e.Row.FindControl("lbse");

                lb_se.Text = string.Format("{0:c2}", se);

                Label lb_hsje = (Label)e.Row.FindControl("lbhsje");

                lb_hsje.Text = string.Format("{0:c2}", hsje);

                //MENG  2013年3月5日 16:27:50
                Label lb_dj = (Label)e.Row.FindControl("lbdj");
                lb_dj.Text = string.Format("{0:c2}", dj);

                Label lb_num = (Label)e.Row.FindControl("lbnum");
                lb_num.Text = string.Format("{0:c2}", num);

                Label lb_hsdj = (Label)e.Row.FindControl("lbhsdj");
                lb_hsdj.Text = string.Format("{0:c2}", hsdj);

            }

            //lb_jesum1.Value = string.Format("{0:c2}", je);
            //lb_sesum.Value = string.Format("{0:c2}", se);
            //lb_hsjehj.Value = string.Format("{0:c2}", hsje);

        }

        //取消钩稽(购货发票总表的勾稽状态=0，外购入库总表中的勾稽标识=1)
        protected void btnTrickOff_Click(object sender, EventArgs e)
        {

            string strsql = "exec [GJACTIONANTI] @fpcode='" + gicode + "'";

            DBCallCommon.ExeSqlText(strsql);

            Response.Redirect("FM_Invoice.aspx?Action=Audit&GI_CODE=" + gicode + " ");

        }
       
        //审核通过
        protected void btnAuditPass_Click(object sender, EventArgs e)
        {
            string Code = txtGI_CODE.Text.Trim();

            string sqlstate = "select GI_GJFLAG from TBFM_GHINVOICETOTAL where GI_CODE='" + Code + "'";

            if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows.Count > 0)
            {
                if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows[0]["GI_GJFLAG"].ToString() == "1")
                {

                    string script = @"alert('单据已钩稽！');";

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    this.ClientScript.RegisterStartupScript(this.GetType(), "js", script, true);

                    return;
                }
            }


            List<string> sql2 = new List<string>();
            int w = 0;
            for (int i = 0; i < grvInvDetail.Rows.Count; i++)
            {
                double shu2 = Convert.ToDouble(((TextBox)grvInvDetail.Rows[i].FindControl("text_sj")).Text.ToString());//数量
                if (shu2.ToString() != "0")
                {
                    w++;
                }
            }
            if (w != 0)
            {
                string sqltext = "delete from TBFM_GHINVOICEDETAIL where GI_NUM='0' and  GI_CODE='" + gicode + "' ";
                sql2.Add(sqltext);

                sqltext = "update TBWS_IN set WG_GJSTATE='0' where WG_CODE in (select GI_INSTOREID from TBFM_GJRELATION where  GI_INSTOREID not in ( select GI_INCOED from TBFM_GHINVOICEDETAIL where GI_CODE='" + gicode + "') and GJ_INVOICEID= '" + gicode + "')  ";
                sql2.Add(sqltext);
                
                string  sqltext2 = " delete from TBFM_GJRELATION where  GI_INSTOREID not in ( select GI_INCOED from TBFM_GHINVOICEDETAIL where GI_CODE='" + gicode + "') and GJ_INVOICEID= '" + gicode + "'";
                sql2.Add(sqltext2);
            }
            else
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('勾稽数量必须有不为0条目');", true);
                return;
            }

            rblGI_STATE.SelectedIndex = 0;//审核  
            //sqltext
            string sqltext1 = this.GetSqlText_Edit();
            sql2.Add(sqltext1);

            DBCallCommon.ExecuteTrans(sql2);
            //明细编辑隐藏
            grvInvDetail.Columns[grvInvDetail.Columns.Count - 1].Visible = false;
            //审核按钮隐藏
            btnAuditPass.Visible = false;
            btnAuditReject.Visible = false;
            this.ClientScript.RegisterStartupScript(GetType(), "js", "location.href ='FM_Invoice.aspx?Action=Trick&GI_CODE=" + gicode + "';", true);

            //this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('操作成功！\\r\\r请确认钩稽！！！');location.href ='FM_Invoice.aspx?Action=Trick&GI_CODE=" + gicode + "';", true);

        }
        //审核驳回
        protected void btnAuditReject_Click(object sender, EventArgs e)
        {
            this.DeleteAll();
        }


        //钩稽通过
        protected void btnTrickPass_Click(object sender, EventArgs e)
        {
            string Code = txtGI_CODE.Text.Trim();

            string sqlstate = "select GI_GJFLAG from TBFM_GHINVOICETOTAL where GI_CODE='" + Code + "'";

            if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows.Count > 0)
            {
                if (DBCallCommon.GetDTUsingSqlText(sqlstate).Rows[0]["GI_GJFLAG"].ToString() == "1")
                {

                    string script = @"alert('单据已钩稽！');";

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);

                    this.ClientScript.RegisterStartupScript(this.GetType(), "js", script, true);

                    return;
                }
            }



            List<string> sql = new List<string>();
            List<string> sql_1 = new List<string>();

            //修改：购货发票总表(TBFM_GHINVOICETOTAL)中的钩稽状态

            rblGI_GJFLAG.SelectedIndex = 0; //表示已勾稽

            string str1 = this.GetSqlText_Edit();
            sql.Add(str1);


            //修改本期勾稽金额

            string str2 = "exec [GJACTION] @fpcode='" + gicode+"'";

            sql.Add(str2);


            //修改：钩稽关系表
            //勾稽期间	

            string GJ_PERIOD = "";

            string GJ_YEAR = "";

            if (DateTime.Now.Month.ToString().Length == 1)
            {
                GJ_PERIOD = "0" + DateTime.Now.Month.ToString();
            }
            else
            {
                GJ_PERIOD = DateTime.Now.Month.ToString();
            }

            GJ_YEAR = DateTime.Now.Year.ToString();
        

            //勾稽年度

            //勾稽日期	
            string GJ_GJDATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string GJ_GJERID = Session["UserID"].ToString();

            string GJ_GJERNM = Session["UserName"].ToString();

            //部门	GJ_DEPID
            //部门名称	GJ_DEPNM
            //勾稽人	GJ_GJERID
            //勾稽人姓名	GJ_GJERNM
            string str3 = "update TBFM_GJRELATION set GJ_PERIOD='" + GJ_PERIOD + "',GJ_GJERID='" + GJ_GJERID + "',GJ_GJERNM='" + GJ_GJERNM + "'," +
                "GJ_YEAR='" + GJ_YEAR + "',GJ_GJDATE='" + GJ_GJDATE + "' where GJ_INVOICEID='" + gicode + "'";
            sql.Add(str3);
        
            DBCallCommon.ExecuteTrans(sql);

            //钩稽按钮隐藏
            btnTrickPass.Visible = false;

            btnTrickReject.Visible = false;

            btnsave.Visible = false;//修改价格隐藏

            this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('钩稽成功！待核算！\\r\\r提示:核算后不能反钩稽！');", true);
            



            BindInvWIDetail(gicode);

        }

        //获取系统入库核算时间
        private bool ClosingAccountDate()
        {
            bool IsHS = false;

            string NowDate = DateTime.Now.ToString("yyyy-MM-dd");

            string sqlText = "select count(*) from TBFM_HSTAOTALIN where  HS_YEAR='" + NowDate.Substring(0, 4) + "' and HS_MONTH='" + NowDate.Substring(5, 2) + "'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

            if (dt.Rows.Count > 0)
            {
                //未核算
                IsHS = false;

                if (Convert.ToInt32(dt.Rows[0][0]) > 0)
                {
                    IsHS = true;
                }
            }
            return IsHS;
        }


        //钩稽驳回
        protected void btnTrickReject_Click(object sender, EventArgs e)
        {
            //驳回修改：购货发票总表(TBFM_GHINVOICETOTAL)中的状态为待审核0
            string sqltext = "update TBFM_GHINVOICETOTAL set GI_STATE=0 where GI_CODE='" + gicode + "'";
            DBCallCommon.ExeSqlText(sqltext);
            Response.Redirect("FM_Invoice.aspx?Action=Audit&GI_CODE=" + gicode);
        }
        /// <summary>
        /// 对发票基本信息修改时获取Sqltext
        /// </summary>
        /// <returns></returns>
        private string GetSqlText_Edit()
        {
            //编号 
            string gi_code = txtGI_CODE.Text.Trim();
            //供应商名称 
            string gi_suppliernm = txtGI_SUPPLIERNM.Text.Trim();
            //开户银行 
            string gi_accbank = txtGI_ACCBANK.Text.Trim();
            //发票号码 
            string gi_invoiceno = txtGI_INVOICENO.Text.Trim();
            //地址 
            string gi_address = txtGI_ADDRESS.Text.Trim();
            //日期 
            string gi_date = txtGI_DATE.Text.Trim();
            ////红蓝字 
            //string gi_rob = rblGI_ROB.SelectedValue.ToString();
            //勾稽标志 
            string gi_gjflag = rblGI_GJFLAG.SelectedValue.ToString();
            ////核销标志 
            //string gi_cavflag = rblGI_CAVFLAG.SelectedValue.ToString();//,GI_CAVFLAG='" + gi_cavflag + "'
            //凭证号 
            string gi_pzh = txtGI_PZH.Text.Trim();
            //部门名称 
            string gi_depnm = txtGI_DEPNM.Text.Trim();
            //记账人姓名 
            string gi_jznm = txtGI_JZNM.Text.Trim();
            //制单人姓名 
            string gi_zdnm = txtGI_ZDNM.Text.Trim();


            //审核状态 
            string gi_state = rblGI_STATE.SelectedValue.ToString();
            //备注 
            string gi_note = txtGI_NOTE.Text.Trim();

            //ID号未添加
            string sqltext = "update TBFM_GHINVOICETOTAL set GI_SUPPLIERNM='" + gi_suppliernm + "',GI_ACCBANK='" + gi_accbank + "',GI_INVOICENO='" + gi_invoiceno + "',GI_ADDRESS='" + gi_address + "',GI_DATE='" + gi_date + "',GI_GJFLAG='" + gi_gjflag + "',GI_PZH='" + gi_pzh + "',GI_DEPNM='" + gi_depnm + "',GI_JZNM='" + gi_jznm + "',GI_ZDNM='" + gi_zdnm + "',GI_STATE='" + gi_state + "',GI_NOTE='" + gi_note + "' " +
                " where GI_CODE='" + gicode + "'";
            return sqltext;
        }


        /// <summary>
        ///DeleteAll (审核驳回)
        /// </summary>
        private void DeleteAll()
        {
            List<string> sql = new List<string>();
            //根据发票编号查询入库单号
            string sqltext = "select GI_INSTOREID from TBFM_GJRELATION where GJ_INVOICEID='" + gicode + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            //外购入库总表(TBWS_IN)及明细（TBWS_INSTWGDETAIL）
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string wgcode = dt.Rows[i]["GI_INSTOREID"].ToString();
                    string wg_code_red = wgcode + "R";
                    string str1 = "update TBWS_IN set WG_GJSTATE='0' where WG_CODE='" + wgcode + "'";//更新入库总表，WG_GJSTATE=0表示无发票
                    sql.Add(str1);
                    ////string str1_red = "delete from TBWS_IN where WG_CODE='" + wg_code_red +"'";//非本会计期间
                    ////sql.Add(str1_red);
                    ////string str1_red_detail = "delete from TBWS_INDETAIL where WG_CODE='" + wg_code_red + "'";//非本会计期间
                    ////sql.Add(str1_red_detail);
                }

                //购货发票总表(TBFM_GHINVOICETOTAL)
                string str2 = "delete from TBFM_GHINVOICETOTAL where GI_CODE='" + gicode + "'";
                sql.Add(str2);
                //购货发票明细表(TBFM_GHINVOICEDETAIL) 
                string str3 = "delete from TBFM_GHINVOICEDETAIL where GI_CODE='" + gicode + "'";
                sql.Add(str3);
                //勾稽关系表(TBFM_GJRELATION)
                string str4 = "delete from TBFM_GJRELATION where GJ_INVOICEID='" + gicode + "'";
                sql.Add(str4);
            }
            DBCallCommon.ExecuteTrans(sql);
            this.ClientScript.RegisterStartupScript(GetType(), "js", "location.href =\"FM_Invoice_Managemnt.aspx\";", true);

            //this.ClientScript.RegisterStartupScript(GetType(), "js", "alert(\"操作成功！\\r\\r返回到 发票管理界面！\");location.href =\"FM_Invoice_Managemnt.aspx\";", true);
        }
        //保存
        protected void btnbaocun_Click(object sender, EventArgs e)
        {
            btnbaocun.Visible = false;
            btnsave.Visible = false;
            btnAuditPass.Visible = true;
            btnAuditReject.Visible = true;

        }
        //修改价格
        protected void btnsave_Click(object sender, EventArgs e)
        {
            List<string> sql = new List<string>();

            decimal totalje = 0;
            decimal totalsje = 0;
            string sqlstring = "";
            string mid = "";
            decimal taxrate = 0;
            decimal shul = 0;
            decimal je = 0;
            decimal hsje = 0;
            decimal hsdj = 0;
            decimal dj = 0;

            string fp = txtGI_CODE.Text.ToString();

            for (int i = 0; i < grvInvDetail.Rows.Count; i++)
            {
                   //string ordercode = ((Label)grvInvDetail.Rows[i].FindControl("lborder")).Text.ToString();

                    mid = ((Label)grvInvDetail.Rows[i].FindControl("lbmid")).Text.ToString();

                    //double dj = Convert.ToDouble(((TextBox)grvInvDetail.Rows[i].FindControl("txtGI_dj")).Text.ToString());//单价
                    
                    taxrate = Convert.ToDecimal(((TextBox)grvInvDetail.Rows[i].FindControl("txtGI_TAXRATE")).Text.ToString());//税率

                    //double hsdj = Convert.ToDouble(((TextBox)grvInvDetail.Rows[i].FindControl("txtGI_TADJ")).Text.ToString());//含税单价

                    //double se = Convert.ToDouble(((TextBox)grvInvDetail.Rows[i].FindControl("txtGI_se")).Text.ToString());//税额

                    shul = Convert.ToDecimal(((TextBox)grvInvDetail.Rows[i].FindControl("text_sj")).Text.ToString());//数量

                    ////金额

                    je = Convert.ToDecimal(((TextBox)grvInvDetail.Rows[i].FindControl("txtGI_je")).Text.ToString());//金额

                    ////含税金额
                    hsje = Convert.ToDecimal(((TextBox)grvInvDetail.Rows[i].FindControl("txtGI_HJPRICE")).Text.ToString());//含税金额
                   
                    if (((CheckBox)grvInvDetail.Rows[i].FindControl("CheckBox1")).Checked)
                    {
                        if (shul.ToString() == "0")
                        {
                            hsdj = 0;

                            dj = 0;
                        }
                        else
                        {
                            hsdj = hsje / shul;

                            dj = je / shul;
                        }

                        //求出单价，更新单价，
                        //if (mid == "01.18.006892")
                        //{
                        //    dj = dj;
                        //}

                 
                        #region 六月

                        if (grvInvDetail.DataKeys[i]["WG_CTYPE"].ToString() == "0")
                        {
                            //没汇总
                            sqlstring = " update TBFM_GHINVOICEDETAIL set GI_NUM='" + shul + "', GI_CTAXUPRICE=round(" + hsdj + ",4) , GI_UNITPRICE=round(" + dj + ",4),GI_TAXRATE='" + taxrate + "', GI_AMTMNY=round(" + je + ",2),GI_CTAMTMNY=round(" + hsje + ",2)  where GI_CODE='" + fp + "' and  GI_MATCODE='" + mid + "' and GI_UNICODE='" + grvInvDetail.DataKeys[i]["UNIQUEID"].ToString() + "'";

                            sql.Add(sqlstring);
                           
                        }
                        else if (grvInvDetail.DataKeys[i]["WG_CTYPE"].ToString() == "1")
                        {
                            //汇总

                            #region 6-9修改之后

                            List<string> keyid = new List<string>();

                            sqlstring = "select GI_ID from TBFM_GHINVOICEDETAIL  where GI_CODE='" + fp + "' and  GI_MATCODE='" + mid + "' and  round(GI_CTAXUPRICE,4) ='" + grvInvDetail.DataKeys[i]["WG_CTAXUPRICE"].ToString() + "' and GI_INCOED='" + grvInvDetail.DataKeys[i]["WG_CODE"].ToString() + "'";
                            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlstring);
                            if (dt.Rows.Count > 0)
                            {
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    keyid.Add(dt.Rows[j]["GI_ID"].ToString());
                                }
                            }

                            if (shul.ToString()== "0")
                            {
                                sqlstring = " update TBFM_GHINVOICEDETAIL set GI_NUM='0', GI_CTAXUPRICE=round(" + hsdj + ",4) , GI_UNITPRICE=round(" + dj + ",4),GI_TAXRATE='" + taxrate + "'  where GI_CODE='" + fp + "' and  GI_MATCODE='" + mid + "' and  round(GI_CTAXUPRICE,4) ='" + grvInvDetail.DataKeys[i]["WG_CTAXUPRICE"].ToString() + "' and GI_INCOED='" + grvInvDetail.DataKeys[i]["WG_CODE"].ToString() + "'";

                            }
                            else
                            {
                                sqlstring = " update TBFM_GHINVOICEDETAIL set  GI_CTAXUPRICE=round(" + hsdj + ",4) , GI_UNITPRICE=round(" + dj + ",4),GI_TAXRATE='" + taxrate + "'  where GI_CODE='" + fp + "' and  GI_MATCODE='" + mid + "' and  round(GI_CTAXUPRICE,4) ='" + grvInvDetail.DataKeys[i]["WG_CTAXUPRICE"].ToString() + "' and GI_INCOED='" + grvInvDetail.DataKeys[i]["WG_CODE"].ToString() + "'";

                            }
                                sql.Add(sqlstring);

                            //更新发票明细金额
                            foreach (string id in keyid)
                            {
                                sqlstring = " update TBFM_GHINVOICEDETAIL set GI_AMTMNY=round(" + dj + "*GI_NUM,2),GI_CTAMTMNY=round(" + hsdj + "*GI_NUM,2)  where GI_ID='" + id + "'";

                                sql.Add(sqlstring);

                            }


                            //更新发票的最后的差价
                            sqlstring = " update TBFM_GHINVOICEDETAIL set GI_AMTMNY=round(GI_AMTMNY+(" + je + "-(select round(sum(GI_AMTMNY),2) from TBFM_GHINVOICEDETAIL where ";


                            for (int j = 0; j < keyid.Count; j++)
                            {
                                sqlstring += " GI_ID='" + keyid[j] + "' ";
                                if (j != keyid.Count - 1)
                                {
                                    sqlstring += "OR";
                                }
                            }

                            //sqlstring = sqlstring.Substring(0, sqlstring.Length-2);

                            sqlstring += ")),2)";

                            sqlstring += ",GI_CTAMTMNY=round(GI_CTAMTMNY+(" + hsje + "-(select round(sum(GI_CTAMTMNY),2) from TBFM_GHINVOICEDETAIL where ";

                            for (int j = 0; j < keyid.Count; j++)
                            {
                                sqlstring += " GI_ID='" + keyid[j] + "' ";

                                if (j != keyid.Count - 1)
                                {
                                    sqlstring += "OR";
                                }
                            }

                            //sqlstring = sqlstring.Substring(0, sqlstring.Length - 2);

                            sqlstring += ")),2)";


                            sqlstring += " where GI_ID='" + keyid[0] + "' ";

                            sql.Add(sqlstring);

                            #endregion
                        }

                        #endregion
                    }


                    totalje += je;

                    totalsje += hsje;
                
            }

           
            //更新发票总表金额

            sqlstring = " update TBFM_GHINVOICETOTAL  set   GI_AMTMNY ='" + totalje + "' , GI_CTAMTMNY ='" + totalsje + "' where  GI_CODE='" + fp + "' ";
            sql.Add(sqlstring);

            //更新发票信息
            sqlstring = this.GetSqlText_Edit();
            sql.Add(sqlstring);

            DBCallCommon.ExecuteTrans(sql);

            //重新绑定发票明细
            this.BindInvDetail(gicode);
            btnbaocun.Visible = false;
            btnsave.Visible=true;
            btnAuditPass.Visible = true;
            btnAuditReject.Visible = true;

        }

        //到打印页面
        protected void btnprint_click(object sender, EventArgs e)
        {
            string fpbh = txtGI_CODE.Text.ToString().Trim();
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "fpdy('" + fpbh + "');", true);
        }




        /// <summary>
        /// 入库明细
        /// </summary>
        /// <param name="fpdh"></param>
        private void BindInvWIDetail(string fpdh)
        {
            string sqltext = "select UNIQUEID,WG_CODE,WG_MARID,MNAME,GUIGE,PURCUNIT,cast(WG_RSNUM as float) as WG_RSNUM,WG_TAXRATE,cast(round(WG_UPRICE,4) as float) as WG_UPRICE,cast(round(WG_CTAXUPRICE,4) as float) as WG_CTAXUPRICE,cast(round(WG_AMOUNT,2) as float) as WG_AMOUNT,cast(round(WG_CTAMTMNY,2) as float) as WG_CTAMTMNY,";
            sqltext += " cast(round((WG_CTAMTMNY-WG_AMOUNT),2) as float) as WG_SE ,cast(WG_GJNUM as float) as WG_GJNUM,cast(round(WG_GJCTAMTMNY,4) as float) as WG_GJCTAMTMNY,cast(round(WG_GJAMOUNT,2) as float) as WG_GJAMOUNT from dbo.MS_IN_GJ ('" + fpdh + "') order by UNIQUEID ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            grvInvWI.DataSource = dt;
            grvInvWI.DataBind();
        }


        protected void grvInvWI_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double d = 0;

                Label lblJE = (Label)e.Row.FindControl("txtGI_je");
                Label lblSE = (Label)e.Row.FindControl("txtGI_se");
                Label lblSJE = (Label)e.Row.FindControl("txtGI_HJPRICE");
                //je += Convert.ToDouble(e.Row.Cells[10].Text.Trim());//金额
                if (double.TryParse(lblJE.Text, out d))
                {
                    wje += d;
                }

                //hsje += Convert.ToDouble(e.Row.Cells[11].Text.Trim());//含税金额
                if (double.TryParse(lblSE.Text, out d))
                {
                    wse += d;
                }
                if (double.TryParse(lblSJE.Text, out d))
                {
                    whsje += d;
                }
                //((TextBox)e.Row.Cells[8].FindControl("txtGI_UNITPRICE")).ReadOnly = true;
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[9].Text = "合计:";
                e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[10].Text = string.Format("{0:c2}", wje);
                e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[11].Text = string.Format("{0:c2}", wse);
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[12].Text = string.Format("{0:c2}", whsje);
                e.Row.Cells[12].HorizontalAlign = HorizontalAlign.Left;
            }
        }


    }

}
