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

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_CHECKREQUEST : System.Web.UI.Page
    {
        string condetail_id = "";//合同号
        string action = "";//
        string contactform = "";//合同类别
        string crid = "";//请款单号

        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["Action"];
            condetail_id = Request.QueryString["condetail_id"];
            contactform = Request.QueryString["contactform"];
            crid = Request.QueryString["CRid"];
            if (!IsPostBack)
            {
                this.BindDep();
                if (action == "Add")
                {
                    lblCR.Text = "新建请款单";
                    this.Check();
                    rblState.SelectedIndex = 0;
                    dplQKBM.SelectedValue = Session["UserDeptID"].ToString();
                    txtQKRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    btnPrint.Visible = false;
                    lblCR_ID.Text = this.CreatCR_ID(condetail_id);
                    //根据合同号绑定某些数据
                    this.BindDataUseHTBH(condetail_id);
                    txtJBR.Text = Session["UserName"].ToString();
                }
                else if (action == "Edit" || action == "EditCW") // 保存状态下的修改 ||财务的修改
                {
                    lblCR.Text = "修改请款单";
                    btnSave.Text = "修 改";
                    //根据请款单号绑定数据
                    this.BindDataEdit(crid);
                    //状态为2时禁止修改内容，只能将状态改回去
                    if (rblState.SelectedValue.ToString() == "2")
                    {
                        palQK.Enabled = false;
                        cal.Visible = false;
                    }
                }
                else if (action == "View")//View
                {
                    lblCR.Text = "查看请款单";
                    this.BindDataEdit(crid);
                    this.ControlEnable();
                    rblState.Enabled = false;
                    btnSave.Visible = false;
                }
            }
            //lblCR_ID.DataBind();
        }
        //控件状态控制
        private void ControlEnable()
        {
            //合同名称
            //txtHTMC.Enabled = false;
            //承包供应商
            //txtCBGYS.Enabled = false;
            //开户银行
            txtKFYH.Enabled = false;
            //账号
            txtZH.Enabled = false;
            //请款部门1
            dplQKBM.Enabled = false;
            //请款用途2
            //txtQKYT.Enabled = false;
            //请款日期4
            txtQKRQ.Enabled = false;
            //支付方式5
            rblZFFS.Enabled = false;
            //票证号6
            txtPZH.Enabled = false;
            //主管领导17
            txtZGLG.Enabled = false;
            //部门负责人18
            txtBMFZR.Enabled = false;
            //验收人19
            txtYSR.Enabled = false;
            //领导20
            txtLD.Enabled = false;
            //财务审核21
            txtCWSH.Enabled = false;
            //经办人22
            txtJBR.Enabled = false;
            rblState.Enabled = false;
            cal.Visible = false;
        }

        /// <summary>
        /// 如有未提交财务请款则无法请款，如财务有未支付请款，则提示是否继续添加请款
        /// </summary>
        private void Check()
        {
            string sqlstr1 = "select CR_ID from TBPM_CHECKREQUEST where CR_STATE in ('0','1') and CR_HTBH='" + condetail_id + "'";
            SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sqlstr1);
            if (dr1.HasRows)
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('有未提交财务请款！\\r\\r提交财务后才能重新请款！！！');window.close();", true);
                return;
            }
            string sqlstr2 = "select CR_ID from TBPM_CHECKREQUEST where CR_BQSFK>CR_BQYZF and CR_HTBH='" + condetail_id + "'";
            SqlDataReader dr2 = DBCallCommon.GetDRUsingSqlText(sqlstr2);
            if (dr2.HasRows)
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('该合同有请款未支付完！\\r\\r请检查后确定是否继续添加新请款单！！！');", true);
            }

        }
        //部门绑定
        public void BindDep()
        {
            string sqltext = "select DEP_NAME,DEP_CODE from  TBDS_DEPINFO where DEP_FATHERID='0'and DEP_CODE!='01'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            dplQKBM.DataSource = dt;
            dplQKBM.DataTextField = "DEP_NAME";
            dplQKBM.DataValueField = "DEP_CODE";
            dplQKBM.DataBind();
            dplQKBM.Items.Insert(0, new ListItem("-请选择-", "%"));
            dplQKBM.SelectedIndex = 0;
        }
        /// <summary>
        /// 根据请款单号绑定数据
        /// </summary>
        private void BindDataEdit(string crid)
        {
            string sqlstr = "select * from TBPM_CHECKREQUEST as a inner join TBPM_CONPCHSINFO as b on a.CR_HTBH=b.PCON_BCODE AND A.CR_ID='" + crid + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlstr);
            if (dr.HasRows)
            {
                dr.Read();
                //合同编号
                //txtHTBH.Text = dr["PCON_BCODE"].ToString();
                //合同名称
                //txtHTMC.Text = dr["PCON_NAME"].ToString();
                //合同总价
                string jsje = dr["PCON_BALANCEACNT"].ToString();
                if (jsje == "" || Convert.ToDecimal(jsje) == 0)
                {
                    //txtHTZJ.Text = dr["PCON_JINE"].ToString();
                }
                else
                {
                    //txtHTZJ.Text = dr["PCON_BALANCEACNT"].ToString();
                }
                //承包供应商
                txtCBGYS.Text = dr["PCON_CUSTMNAME"].ToString();
                //开户银行
                txtKFYH.Text = dr["CR_DEPOSITBANK"].ToString();
                //账号
                txtZH.Text = dr["CR_BANKACUNUM"].ToString();
                //已付款
                //txtYFK.Text = dr["CR_YFK"].ToString();
                //请款单号0
                lblCR_ID.Text = dr["CR_ID"].ToString();
                //请款部门1
                dplQKBM.ClearSelection();
                foreach (ListItem li in dplQKBM.Items)
                {
                    if (li.Value.ToString() == dr["CR_QKBM"].ToString())
                    {
                        li.Selected = true; break;
                    }
                }
                //请款用途2
                //txtQKYT.Text = dr["CR_USE"].ToString();
                //请款日期4
                txtQKRQ.Text = Convert.ToDateTime(dr["CR_DATE"].ToString()).ToShortDateString();
                //支付方式5
                foreach (ListItem li in rblZFFS.Items)
                {
                    if (dr["CR_ZFFS"].ToString() == li.Text)
                    {
                        li.Selected = true; break;
                    }
                }
                //票证号6
                txtPZH.Text = dr["CR_PZH"].ToString();
                //主管领导17
                txtZGLG.Text = dr["CR_ZGLD"].ToString();
                //部门负责人18
                txtBMFZR.Text = dr["CR_BMFZR"].ToString();
                //验收人19
                txtYSR.Text = dr["CR_YSR"].ToString();
                //领导20
                txtLD.Text = dr["CR_LD"].ToString();
                //财务审核21
                txtCWSH.Text = dr["CR_CWSH"].ToString();
                //经办人22
                txtJBR.Text = dr["CR_JBR"].ToString();
                //请款状态23
                if (action == "View")
                {
                    rblState.Items.Clear();
                    rblState.Items.Add(new ListItem("保存", "0"));
                    rblState.Items.Add(new ListItem("正在签字", "1"));
                    rblState.Items.Add(new ListItem("提交财务-未付款", "2"));
                    rblState.Items.Add(new ListItem("提交财务-部分支付", "3"));
                    rblState.Items.Add(new ListItem("提交财务-已付款", "4"));

                }
                if (action == "EditCW")
                {
                    rblState.Items.Clear();
                    rblState.Items.Add(new ListItem("正在签字", "1"));
                    rblState.Items.Add(new ListItem("提交财务-未付款", "2"));

                }
                //在Edit状态下，如果状态为正在签字，无法修改状态
                if (action == "Edit" && dr["CR_STATE"].ToString() == "1")
                {
                    rblState.Enabled = false;
                }
                foreach (ListItem li in rblState.Items)
                {
                    if (dr["CR_STATE"].ToString() == li.Value.ToString())
                    {
                        li.Selected = true; break;
                    }
                }
                CR_BQSFK.Text = dr["CR_BQSFK"].ToString();
                bqsfk.Value = dr["CR_BQSFK"].ToString();
                CR_BQSFKDX.Text = dr["CR_BQSFKDX"].ToString();
                bqsfkdx.Value = dr["CR_BQSFKDX"].ToString();
                //请款合同类别24
                dr.Close();
                //************************************
                //double yfk = Convert.ToDouble(txtYFK.Text);//已付款
                //double bqyfk = Convert.ToDouble(txtBQYFK.Text);//本期应付款
                //double bqsfk = Convert.ToDouble(txtBQSFK.Text.Trim());//本期实付款
                //double yfklj = yfk + bqyfk;//应付款累计
                //double sfklj = yfklj;//实付款累计
                //txtYFKLJ.Text = yfklj.ToString();
                //txtSFKLJ.Text = sfklj.ToString();
                //txtBQYFK2.Text = txtBQYFK.Text.Trim();
                //txtBQSFK2.Text = txtBQSFK.Text.Trim();
                //txtBQSFK3.Text = txtBQSFK.Text.Trim();
                //***************************************
            }
            if (contactform == "3")
            {
                sqlstr = "select b.ID,b.CM_CONTR,a.PCON_PJNAME,a.PCON_ENGNAME,CASE WHEN PCON_BALANCEACNT=0 THEN PCON_JINE ELSE PCON_BALANCEACNT END AS PCON_HTZJ,PCON_YFK,b.CG_CONTR as CONTR,b.CM_MATERIAL,b.CM_COUNT,CASE WHEN CM_YIFU is NULL THEN '0' ELSE CM_YIFU END AS CM_YIFU,d.CM_APPLI,d.CM_NOW from TBPM_CONPCHSINFO as a right join TBPM_CGDETAIL as b on a.PCON_BCODE=b.CG_CONTR left join (select ID,sum(CM_NOW) as CM_YIFU from TBPM_FUKUAN where CR_ID<>'" + crid + "' and CR_ID like '" + crid.Substring(0, 12) + "%' and substring(CR_ID,17, 1)<" + crid.Substring(16) + " group by ID) as c on b.ID=C.ID left join TBPM_FUKUAN as d on b.ID=d.ID where d.CR_ID='" + crid + "'";
            }
            else if (contactform == "1")
            {
                sqlstr = "select b.ID,b.CM_CONTR,d.CM_PROJ as PCON_PJNAME,a.PCON_ENGNAME,CASE WHEN PCON_BALANCEACNT=0 THEN PCON_JINE ELSE PCON_BALANCEACNT END AS PCON_HTZJ,PCON_YFK,b.SC_CONTR as CONTR,b.CM_CONTENT as CM_MATERIAL,b.CM_COUNT,CASE WHEN CM_YIFU is NULL THEN '0' ELSE CM_YIFU END AS CM_YIFU,e.CM_APPLI,e.CM_NOW from TBPM_CONPCHSINFO as a right join TBPM_SCDETAIL as b on a.PCON_BCODE=b.SC_CONTR left join (select ID,sum(CM_NOW) as CM_YIFU from TBPM_FUKUAN where CR_ID<>'" + crid + "' and CR_ID like '" + crid.Substring(0, 12) + "%' and substring(CR_ID,17, 1)<" + crid.Substring(16) + " group by ID) as c on b.ID=c.ID left join TBCM_PLAN as d on b.CM_CONTR=d.CM_CONTR left join TBPM_FUKUAN as e on b.ID=e.ID where e.CR_ID='" + crid + "'";
            }
            else
            {
                sqlstr = "select b.ID,a.PCON_BCODE as CM_CONTR,a.PCON_PJNAME,a.PCON_ENGNAME,CASE WHEN PCON_BALANCEACNT=0 THEN PCON_JINE ELSE PCON_BALANCEACNT END AS PCON_HTZJ,PCON_YFK,NULL as CONTR,NULL as CM_MATERIAL,a.PCON_JINE as CM_COUNT,CASE WHEN CM_YIFU is NULL THEN '0' ELSE CM_YIFU END AS CM_YIFU,CM_NOW,CM_APPLI from TBPM_CONPCHSINFO as a left join (select ID,sum(CM_NOW) as CM_YIFU from TBPM_FUKUAN where CR_ID<>'" + crid + "' and CR_ID like '" + crid.Substring(0, 12) + "%' and substring(CR_ID,17, 1)<" + crid.Substring(16) + " group by ID) as b on a.PCON_BCODE=b.ID left join TBPM_CHECKREQUEST as c on c.CR_HTBH=a.PCON_BCODE left join TBPM_FUKUAN as d on c.CR_ID=d.CR_ID where c.CR_ID='" + crid + "'";
            }
            Det_Repeater.DataSource = DBCallCommon.GetDTUsingSqlText(sqlstr);
            Det_Repeater.DataBind();
        }

        /// <summary>
        /// 创建请款单号
        /// 规则：合同号+流水号
        /// </summary>
        private string CreatCR_ID(string hth)
        {
            string CR_ID = "";
            string str = "select top 1 CR_ID from TBPM_CHECKREQUEST where CR_HTBH='" + condetail_id + "' order by ID desc";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(str);
            string lsh = "";
            if (dr.HasRows)
            {
                dr.Read();
                string cr_id = dr["CR_ID"].ToString();
                string[] cr = cr_id.Split('-');
                lsh = (Convert.ToInt16(cr[cr.Length - 1].ToString()) + 1).ToString();
                dr.Close();
            }
            else
            {
                lsh = "1";
            }
            CR_ID = hth + ".QK" + "-" + lsh;
            return CR_ID;
        }

        /// <summary>
        /// 新建请款单时，根据合同编号绑定某些数据
        /// </summary>
        /// <param name="htbh"></param>
        private void BindDataUseHTBH(string htbh)
        {
            string strsql = "select A.* ,B.CS_Bank,B.CS_Account from TBPM_CONPCHSINFO AS A LEFT OUTER JOIN TBCS_CUSUPINFO AS B" +
                            " ON A.PCON_CUSTMID=B.CS_CODE WHERE A.PCON_BCODE='" + htbh + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(strsql);
            if (dr.HasRows)
            {
                dr.Read();
                //合同编号
                //txtHTBH.Text = dr["PCON_BCODE"].ToString();
                //txtHTBH.Enabled = false;
                //合同名称
                //txtHTMC.Text = dr["PCON_NAME"].ToString();
                //txtHTMC.Enabled = false;
                //合同总价:结算金额为空或者0时以合同金额为准，有结算金额时用结算金额
                string jsje = dr["PCON_BALANCEACNT"].ToString();
                if (jsje == "" || Convert.ToDecimal(jsje) == 0)
                {
                    //txtHTZJ.Text = dr["PCON_JINE"].ToString();
                }
                else
                {
                    //txtHTZJ.Text = dr["PCON_BALANCEACNT"].ToString();
                }

                //承包供应商
                txtCBGYS.Text = dr["PCON_CUSTMNAME"].ToString();

                //开户银行
                txtKFYH.Text = dr["CS_Bank"].ToString();
                //账号
                txtZH.Text = dr["CS_Account"].ToString();

                dr.Close();
            }
            string id = string.Empty;
            if (Request.QueryString["val"] != null)
            {
                string[] strs = Request.QueryString["val"].Split('/');
                for (int i = 0; i < strs.Length; i++)
                {
                    id += "'" + strs[i] + "',";
                }
                id = id.Substring(0, id.Length - 1);
            }
            DataTable dt = new DataTable();
            if (contactform == "3")
            {
                strsql = "select b.ID,b.CM_CONTR,a.PCON_PJNAME,a.PCON_ENGNAME,CASE WHEN PCON_BALANCEACNT=0 THEN PCON_JINE ELSE PCON_BALANCEACNT END AS PCON_HTZJ,PCON_YFK,b.CG_CONTR as CONTR,b.CM_MATERIAL,b.CM_COUNT,CASE WHEN CM_YIFU is NULL THEN '0' ELSE CM_YIFU END AS CM_YIFU,NULL as CM_NOW,0.00 as CM_APPLI from TBPM_CONPCHSINFO as a right join TBPM_CGDETAIL as b on a.PCON_BCODE=b.CG_CONTR left join (select ID, sum(CM_NOW) as CM_YIFU from TBPM_FUKUAN where CR_ID like '" + condetail_id + "%' group by ID) as c on convert(nvarchar(20),b.ID)=C.ID where b.ID in(" + id + ")";

            }
            else if (contactform == "1")
            {
                strsql = "select b.ID,b.CM_CONTR,d.CM_PROJ as PCON_PJNAME,a.PCON_ENGNAME,CASE WHEN PCON_BALANCEACNT=0 THEN PCON_JINE ELSE PCON_BALANCEACNT END AS PCON_HTZJ,PCON_YFK,b.SC_CONTR as CONTR,b.CM_CONTENT as CM_MATERIAL,b.CM_COUNT,CASE WHEN CM_YIFU is NULL THEN '0' ELSE CM_YIFU END AS CM_YIFU,NULL as CM_NOW,0.00 as CM_APPLI from TBPM_CONPCHSINFO as a right join TBPM_SCDETAIL as b on a.PCON_BCODE=b.SC_CONTR left join (select ID, sum(CM_NOW) as CM_YIFU from TBPM_FUKUAN where CR_ID like '" + condetail_id + "%'  group by ID) as c on convert(nvarchar(20),b.ID)=C.ID left join TBCM_PLAN as d on b.CM_CONTR=d.CM_CONTR where b.ID in(" + id + ")";
            }
            else
            {
                strsql = "select b.ID,a.PCON_BCODE as CM_CONTR,a.PCON_PJNAME,a.PCON_ENGNAME,CASE WHEN PCON_BALANCEACNT=0 THEN PCON_JINE ELSE PCON_BALANCEACNT END AS PCON_HTZJ,PCON_YFK,NULL as CONTR,NULL as CM_MATERIAL,a.PCON_JINE as CM_COUNT,CASE WHEN CM_YIFU is NULL THEN '0' ELSE CM_YIFU END AS CM_YIFU,NULL as CM_NOW,0.00 as CM_APPLI from TBPM_CONPCHSINFO as a left join (select ID, sum(CM_NOW) as CM_YIFU from TBPM_FUKUAN where CR_ID like '" + condetail_id + "%' group by ID) as b on a.PCON_BCODE=b.ID where a.PCON_BCODE='" + condetail_id + "'";
            }
            dt = DBCallCommon.GetDTUsingSqlText(strsql);
            Det_Repeater.DataSource = dt;
            Det_Repeater.DataBind();
            //strsql = "select sum(CR_BQYFK) as hjsum from TBPM_CHECKREQUEST where cr_id like '" + txtHTBH.Text.ToString() + "%' ";
            //dt = DBCallCommon.GetDTUsingSqlText(strsql);
            //if (dt.Rows.Count > 0)
            //{
            //    //已付款=合同请款金额
            //    //txtYFK.Text = dt.Rows[0]["hjsum"].ToString();
            //}
        }

        /// <summary>
        /// 执行SQL，添加，更新
        /// </summary>
        private void ExecSQL()
        {
            #region  获取信息
            List<string> sqltext = new List<string>();
            //请款单号0
            string cr_id = lblCR_ID.Text;
            //请款部门1
            string cr_qkbm = dplQKBM.SelectedValue.ToString();
            //请款用途2
            //string cr_use = txtQKYT.Text;
            //合同编号3
            //string cr_htbh = txtHTBH.Text.Trim();
            //请款日期4
            string cr_date = txtQKRQ.Text.Trim() == "" ? DateTime.Now.ToShortDateString() : txtQKRQ.Text.Trim();
            string cr_zffs = rblZFFS.SelectedItem.Text.Trim();
            //票证号6
            string cr_pzh = txtPZH.Text.Trim();
            //主管领导17
            string cr_zgld = txtZGLG.Text.Trim();
            //部门负责人18
            string cr_bmfzr = txtBMFZR.Text.Trim();
            //验收人19
            string cr_ysr = txtYSR.Text.Trim();
            //领导20
            string cr_ld = txtLD.Text.Trim();
            //财务审核21
            string cr_cwsh = txtCWSH.Text.Trim();
            //请款人
            string cr_jbr = txtJBR.Text.Trim();
            //请款状态23
            int cr_state = Convert.ToInt16(rblState.SelectedValue.ToString());
            string cr_htlb = contactform;
            /*********请款单中添加开户行及银行帐号**************/
            string cr_depositbank = txtKFYH.Text.Trim();
            string cr_bankacunum = txtZH.Text.Trim();
            /***************************************************/
            #endregion
            /*为减少添加、删除、作废时的金额变动，在提交到财务前不改变合同表中的金额信息
             提交到财务后影响的金额有：应付款累计，实付款累计，已付款*/
            if (action == "Add")
            {
                //添加请款单
                string str1 = "insert into TBPM_CHECKREQUEST(CR_ID,CR_QKBM,CR_HTBH,CR_DATE,CR_ZFFS,CR_BQSFK,CR_BQSFKDX,CR_PZH,CR_ZGLD,CR_BMFZR,CR_YSR,CR_LD,CR_CWSH,CR_JBR,CR_STATE,CR_HTLB,CR_DEPOSITBANK,CR_BANKACUNUM)" +
                    " values('" + cr_id + "','" + cr_qkbm + "','" + condetail_id + "','" + cr_date + "','" + cr_zffs + "','" + bqsfk.Value + "','" + bqsfkdx.Value + "','" + cr_pzh + "','" + cr_zgld + "','" + cr_bmfzr + "','" + cr_ysr + "','" + cr_ld + "','" + cr_cwsh + "','" + cr_jbr + "','" + cr_state + "','" + cr_htlb + "','" + cr_depositbank + "','" + cr_bankacunum + "')";
                sqltext.Add(str1);
                string dtime = DateTime.Now.ToString();
                foreach (RepeaterItem item in Det_Repeater.Items)
                {
                    string id = ((Label)item.FindControl("ID")).Text;
                    string now = ((TextBox)item.FindControl("CM_NOW")).Text;
                    string app = ((TextBox)item.FindControl("CM_APPLI")).Text;
                    str1 = string.Format("insert into TBPM_FUKUAN values('{0}','{1}','{2}','{3}','{4}','{5}')", id, lblCR_ID.Text, now, app, contactform, dtime);
                    sqltext.Add(str1);
                }
            }
            else if (action == "Edit") //修改请款单
            {
                string str1 = "update TBPM_CHECKREQUEST set CR_QKBM='" + cr_qkbm + "',CR_DATE='" + cr_date + "',CR_ZFFS='" + cr_zffs + "',CR_BQSFK='" + bqsfk.Value + "',CR_BQSFKDX='" + bqsfkdx.Value + "',CR_PZH='" + cr_pzh + "',CR_ZGLD='" + cr_zgld + "',CR_BMFZR='" + cr_bmfzr + "',CR_YSR='" + cr_ysr + "',CR_LD='" + cr_ld + "',CR_CWSH='" + cr_cwsh + "',CR_JBR='" + cr_jbr + "',CR_STATE='" + cr_state + "',CR_DEPOSITBANK='" + cr_depositbank + "',CR_BANKACUNUM='" + cr_bankacunum + "'" +
                    " Where CR_ID='" + cr_id + "'";
                sqltext.Add(str1);
                string dtime = DateTime.Now.ToString();
                foreach (RepeaterItem item in Det_Repeater.Items)
                {
                    string id = ((Label)item.FindControl("ID")).Text;
                    string now = ((TextBox)item.FindControl("CM_NOW")).Text;
                    string app = ((TextBox)item.FindControl("CM_APPLI")).Text;
                    str1 = string.Format("update TBPM_FUKUAN set CM_NOW='{0}',CM_APPLI='{1}',CM_DATE='{2}' where ID='{3}' and CR_ID='{4}'", now, app, dtime, id, cr_id);
                    sqltext.Add(str1);
                }
            }
            else if (action == "EditCW")//修改或提交到财务(未提交到财务前的请款单还没生效，所以不改变合同信息)
            {
                //有四种情况：状态变化
                //（1）一种是由正在签字提交到财务未付款
                //（2）一种是由财务未付款退回到正在签字
                //状态不变（3）在状态1上进行某些内容的修改，（4）在状态2上进行某些内容的修改
                //（3）情况可以随便改，因为数据只在该请款单中，不涉及其他变化
                //（4）禁止修改除状态以外的其他值，否则容易出现金额加减混乱,如果一定要修改，先把状态修改回1，再进行修改

                string str1 = "update TBPM_CHECKREQUEST set CR_QKBM='" + cr_qkbm + "',CR_DATE='" + cr_date + "',CR_ZFFS='" + cr_zffs + "',CR_BQSFK='" + bqsfk.Value + "',CR_BQSFKDX='" + bqsfkdx.Value + "',CR_PZH='" + cr_pzh + "',CR_ZGLD='" + cr_zgld + "',CR_BMFZR='" + cr_bmfzr + "',CR_YSR='" + cr_ysr + "',CR_LD='" + cr_ld + "',CR_CWSH='" + cr_cwsh + "',CR_JBR='" + cr_jbr + "',CR_STATE='" + cr_state + "',CR_DEPOSITBANK='" + cr_depositbank + "',CR_BANKACUNUM='" + cr_bankacunum + "'" +
                    " Where CR_ID='" + cr_id + "'";
                sqltext.Add(str1);
                //先查询该请款单的实际状态，再判断是属于哪种情况
                string sqlcheckstate = "select CR_STATE from TBPM_CHECKREQUEST where CR_ID='" + cr_id + "'";
                DataTable dtcheckstate = DBCallCommon.GetDTUsingSqlText(sqlcheckstate);
                if (dtcheckstate.Rows.Count > 0)
                {
                    string str_updateCON = "";
                    //第一种情况 ，状态由1变成了2
                    if (rblState.SelectedValue == "2" && dtcheckstate.Rows[0][0].ToString() == "1")//若提交到财务，则合同表中应付款累计和实付款累计增加，若该请款单删除或驳回，则还回这两项金额
                    {
                        //修改合同中的应付款累计，实付款累计，合同已付款(+扣款合计)
                        str_updateCON = "update TBPM_CONPCHSINFO set PCON_YFKLJ=PCON_YFKLJ+'" + Convert.ToDouble(bqsfk.Value) + "'" +
                        " ,PCON_SFKLJ=PCON_SFKLJ+'" + Convert.ToDouble(bqsfk.Value) + "'" +
                        " ,PCON_YFK=PCON_YFK+'" + Convert.ToDouble(bqsfk.Value) + "'" +
                        "  where PCON_BCODE='" + condetail_id + "'";
                        sqltext.Add(str_updateCON);

                        //按请款金额和扣款情况，修改合同中的应付尾款（合同中的尾款没有用，界面是显示的是用公式计算的）
                        /*******************************/
                    }
                    //第二种情况 状态由2变成了1
                    if (rblState.SelectedValue == "1" && dtcheckstate.Rows[0][0].ToString() == "2")
                    {
                        //修改合同中的应付款累计，实付款累计，合同已付款(-扣款合计)
                        str_updateCON = "update TBPM_CONPCHSINFO set PCON_YFKLJ=PCON_YFKLJ-(" + Convert.ToDouble(bqsfk.Value) + ")" +
                        " ,PCON_SFKLJ=PCON_SFKLJ-(" + Convert.ToDouble(bqsfk.Value) + ")" +
                        " ,PCON_YFK=PCON_YFK-(" + Convert.ToDouble(bqsfk.Value) + ")" +
                        "  where PCON_BCODE='" + condetail_id + "'";
                        sqltext.Add(str_updateCON);
                    }
                }
            }
            DBCallCommon.ExecuteTrans(sqltext);//执行事务
            btnSave.Visible = false;
            lblRemind.Visible = true;
            btnPrint.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (dplQKBM.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择请款部门！');", true); return;
            }
            else
            {
                this.ExecSQL();
                if (action == "Add" | action == "Edit" | action == "EditCW")
                {
                    Response.Write("<script>window.opener.location.reload();</script>");//刷新
                }
            }
        }

        //选择驳回时提示
        protected void rblState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblState.SelectedValue == "5")
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "javascript:alert('驳回后该请款单将作废！！！');", true);

            }
        }
    }
}
