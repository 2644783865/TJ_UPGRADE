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
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_SW_Payment : System.Web.UI.Page
    {
        string contractID = "";//合同编号
        string action = "";//添加 修改
        string bp_id = "";//要款单号
        protected void Page_Load(object sender, EventArgs e)
        {
            contractID = Request.QueryString["condetail_id"];
            action = Request.QueryString["Action"];
            bp_id = Request.QueryString["BPid"];
            if (!IsPostBack)
            {
                if (action == "Add")
                {
                    lblAction.Text = "添加要款信息";
                    lblYKDH.Text = this.CreateBPID(contractID);
                    txtHTBH.Text = contractID;


                   
                    rblState.Enabled = false;
                    txtCWB.Enabled = false;
                    rblPZ.Enabled = false;
                    txtPZ.Enabled = false;
                    rblPZ.SelectedIndex = 1;
                    txtYKRQ.Text = DateTime.Now.ToString("yyyy-MM-dd");

                }
                else if (action == "Edit")
                {
                    lblAction.Text = "确认要款信息";
                    this.BindBPData(bp_id);
                    //商务组信息不可修改
                    lblKXMC.Enabled = false;
                    //txtJE.Enabled = false;
                    //txtYKRQ.Enabled = false;
                    //dplSKFS.Enabled = false;
                    //txtSWZ.Enabled = false;
                    //**********************                   
                }
                else if (action == "View")
                {
                    //商务组操作
                    //在财务未确认前，商务组可修改
                    //根据要款单号绑定数据                    
                    this.BindBPData(bp_id);
                    lblAction.Text = "查看要款信息";

                    btnConfirm.Visible = false;
                    lblState.Visible = false;
                    this.ControlsEnable();
                }

            }
        }
        //控件状态控制
        private void ControlsEnable()
        {
            //合同编号
            txtHTBH.Enabled = false;
            //款项名称
            lblKXMC.Enabled = false;
            //要款日期
            txtYKRQ.Enabled = false;
           
            //要款金额
            txtJE.Enabled = false;

            //收款方式
            dplSKFS.Enabled = false; ;
            //支付状态
            rblState.Enabled = false;
            //备注1-商务组
            txtSWZ.Enabled = false;
            //备注2-财务部
            txtCWB.Enabled = false;
            //凭证
            rblPZ.Enabled = false;
            txtPZ.Enabled = false;

        }

        /// <summary>
        /// 创建要款单号
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private string CreateBPID(string ID)
        {
            string id = "";
            string sqlstr = "select top 1 BP_ID FROM TBPM_BUSPAYMENTRECORD where BP_ID like '" + contractID + "%' ORDER BY ID desc";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlstr);
            if (dr.HasRows)
            {
                dr.Read();
                string[] a = dr["BP_ID"].ToString().Split('-');
                id = ID + ".YK-" + (Convert.ToInt16(a[a.Length - 1].ToString()) + 1).ToString();
            }
            else
            {
                id = ID + ".YK-1";
            }
            return id;
        }

        /// <summary>
        /// 根据要款单号绑定数据
        /// </summary>
        /// <param name="bpid"></param>
        private void BindBPData(string bpid)
        {
            string sqlstr = "select a.* from TBPM_BUSPAYMENTRECORD a where BP_ID='" + bpid + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlstr);
            if (dr.HasRows)
            {
                dr.Read();
                //要款单号
                lblYKDH.Text = dr["BP_ID"].ToString();

                //合同编号
                txtHTBH.Text = dr["BP_HTBH"].ToString();
                //款项名称
                lblKXMC.SelectedValue = dr["BP_KXMC"].ToString();
                //要款日期
                txtYKRQ.Text = Convert.ToDateTime(dr["BP_YKRQ"].ToString()).ToString("yyyy-MM-dd");
               
                //要款金额
                txtJE.Text = dr["BP_JE"].ToString();
                hidJE.Value = dr["BP_JE"].ToString();
                //收款方式
                dplSKFS.SelectedIndex = Convert.ToInt16(dr["BP_SKFS"].ToString()) + 1;
                //支付状态
                rblState.SelectedIndex = Convert.ToInt16(dr["BP_STATE"].ToString());
                //支付比例
                txtSWZ.Text = dr["BP_NOTEFST"].ToString() + "%";
                //备注2-财务部
                txtCWB.Text = dr["BP_NOTESND"].ToString();
                //凭证
                foreach (ListItem li in rblPZ.Items)
                {
                    if (li.Value.ToString() == dr["BP_PZ"].ToString())
                    {
                        li.Selected = true; break;
                    }
                }
                txtPZ.Text = dr["BP_PZH"].ToString();
                dr.Close();
            }
        }

        private bool ExecSql()
        {
            bool state = false;
            if (CheckInput())
            {
            
                    //要款单号
                    string bp_id = lblYKDH.Text.Trim();
                    //合同编号
                    string bp_htbh = txtHTBH.Text.Trim();
                    //款项名称
                    string bp_kxmc = lblKXMC.SelectedValue;
                    //要款日期
                    string bp_ykrq = txtYKRQ.Text.Trim();
                   
                    //要款金额
                    double bp_je = Convert.ToDouble(txtJE.Text.Trim());

                    //收款方式
                    int bp_skfs = Convert.ToInt16(dplSKFS.SelectedItem.Value.ToString());
                    //支付状态
                    int bp_state = Convert.ToInt16(rblState.SelectedItem.Value.ToString());
                    //支付比例
                    string bp_notefst = txtSWZ.Text.Trim('%');
                    //备注
                    string bp_notesnd = txtCWB.Text.Trim();
                    //凭证号
                    string bp_pz = rblPZ.SelectedValue.ToString();
                    string bp_pzh = txtPZ.Text.Trim();
                    List<string> sqlstr = new List<string>();

                    string sql = "select sum(BP_JE) from TBPM_BUSPAYMENTRECORD where BP_HTBH='" + bp_htbh + "'";
                    DataTable data1 = DBCallCommon.GetDTUsingSqlText(sql);
                    sql = "select PCON_JINE from TBPM_CONPCHSINFO where PCON_BCODE='" + bp_htbh + "'";
                    DataTable data2 = DBCallCommon.GetDTUsingSqlText(sql);
                    double je = 0;
                    double bp = 0;
                    bool i = true;
                    if (data2.Rows.Count > 0)
                    {
                        je = double.Parse(data2.Rows[0][0].ToString());
                    }
                    if (data1.Rows.Count > 0)
                    {
                        bp = double.Parse(data1.Rows[0][0].ToString() == "" ? "0" : data1.Rows[0][0].ToString());
                        bp = Math.Round(bp, 6, MidpointRounding.AwayFromZero);
                        if (action == "Add")
                        {
                            bp += bp_je;
                        }
                        else if (action == "Edit")
                        {

                            bp = bp + bp_je - double.Parse(hidJE.Value);
                            bp = Math.Round(bp, 6, MidpointRounding.AwayFromZero);
                        }

                        if (bp > je)
                        {
                            i = false;
                        }
                    }
                    else
                    {
                        if (bp_je > je)
                        {
                            i = false;
                        }
                    }
                    if (i)
                    {
                        if (action == "Add")
                        {

                            string sqlstr1 = "insert into TBPM_BUSPAYMENTRECORD(BP_ID,BP_HTBH,BP_KXMC,BP_YKRQ,BP_JE,BP_SKFS,BP_STATE,BP_NOTEFST)" +
                                "VALUES('" + bp_id + "','" + bp_htbh + "','" + bp_kxmc + "','" + bp_ykrq + "'," + bp_je + "," + bp_skfs + "," + bp_state + ",'" + bp_notefst + "')";
                            sqlstr.Add(sqlstr1);
                            DBCallCommon.ExecuteTrans(sqlstr);

                        }
                        else if (action == "Edit")
                        {
                            //确认支付后，修改合同信息表中的已付款、已付款累计、实付款累计和已付尾款。
                            //修改前判断金额是否发生变化，如果实收金额发生变化，则合同信念表中相应金额改变
                            string sqlstr2 = "update TBPM_BUSPAYMENTRECORD set BP_STATE=" + bp_state + ",BP_NOTESND='" + bp_notesnd + "',BP_PZ='" + bp_pz + "',BP_PZH='" + bp_pzh + "',BP_JE='" + bp_je + "',BP_NOTEFST='" + bp_notefst + "',BP_YKRQ='"+bp_ykrq+"' where BP_ID='" + bp_id + "'";
                            DBCallCommon.ExeSqlText(sqlstr2);

                            sql = "select sum(BP_JE),sum(BP_NOTEFST) from TBPM_BUSPAYMENTRECORD where BP_HTBH='" + bp_htbh + "' and BP_STATE='1'";
                            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                            double intYfkje = 0;
                            int intYfkbl = 0;
                            string yfkje = dt.Rows[0][0].ToString();
                            string yfkbl = dt.Rows[0][1].ToString();
                            double.TryParse(yfkje, out intYfkje);
                            int.TryParse(yfkbl, out intYfkbl);
                            sql = "select BP_JE,BP_NOTEFST from TBPM_BUSPAYMENTRECORD where BP_HTBH='" + bp_htbh + "' and BP_KXMC='质保金'";
                            dt = DBCallCommon.GetDTUsingSqlText(sql);
                            double intZbjje = 0;
                            int intZbjbl = 0;
                            if (dt.Rows.Count > 0)
                            {
                                double.TryParse(dt.Rows[0][0].ToString(), out intZbjje);
                                int.TryParse(dt.Rows[0][1].ToString(), out intZbjbl);
                            }
                            double wfk = je - intYfkje ;
                            int wfkbl = 100 - intYfkbl ;

                            sql = "update TBPM_CONPCHSINFO set PCON_YFK='" + intYfkje + "',PCON_YFKBL='" + intYfkbl + "',PCON_YFWK='" + wfk + "',PCON_WFKBL='" + wfkbl + "',PCON_ZBJ='" + intZbjje + "', PCON_ZBJBL='" + intZbjbl + "' where PCON_BCODE='" + bp_htbh + "'";
                            DBCallCommon.ExeSqlText(sql);
                            //string checkskje = "select BP_SFJE from TBPM_BUSPAYMENTRECORD where BP_ID='" + bp_id + "'";
                            //DataTable dt_checkskje = DBCallCommon.GetDTUsingSqlText(checkskje);
                            //if (dt_checkskje.Rows.Count > 0)
                            //{
                            //    //double chg_je = Convert.ToDouble(txtSFJE.Text.Trim()) - Convert.ToDouble(dt_checkskje.Rows[0]["BP_SFJE"].ToString());
                            //    //string sqlstr1 = "update TBPM_CONPCHSINFO set PCON_YFK=PCON_YFK+" + chg_je + ",PCON_YFKLJ=PCON_YFKLJ+" + chg_je + ",PCON_SFKLJ=PCON_SFKLJ+" + chg_je + "" +
                            //    //    " where PCON_BCODE='" + bp_htbh + "'";

                            //    //string sqlstr2 = "update TBPM_BUSPAYMENTRECORD set BP_SKRQ='" + bp_skrq + "',BP_SFJE=" + bp_sfje + ",BP_STATE=" + bp_state + ",BP_NOTESND='" + bp_notesnd + "',BP_PZ='" + bp_pz + "',BP_PZH='" + bp_pzh + "'" +
                            //    //"where BP_ID='" + bp_id + "'";
                            //    //sqlstr.Add(sqlstr1);
                            //    //sqlstr.Add(sqlstr2);
                            //    //DBCallCommon.ExecuteTrans(sqlstr);
                            //}
                        }
                        //操作成功
                        btnConfirm.Visible = false;
                        lblState.Visible = true;
                        state = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('要款总额大于合同总额！');", true);
                    }
                }
            

            return state;
        }

        private bool CheckInput()
        {
            bool YesNO = true;
            if (dplSKFS.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择支付方式！');", true);
                return YesNO = false;
            }
            if (Convert.ToDouble(txtJE.Text.Trim()) == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写要款金额！');", true);
                return YesNO = false;
            }

            if (txtYKRQ.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写要款日期！');", true);
                return YesNO = false;
            }
            string Reg = @"^(\d){1,3}%$";
            if (!Regex.IsMatch(txtSWZ.Text.Trim(), Reg))
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写正确的比例格式，如：50%！');", true);
                return YesNO = false;
            }

            return YesNO;
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtPZ.Text != "" && txtPZ != null)
            { rblPZ.SelectedIndex = 0; }
            if (this.ExecSql())
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "window.returnValue=\"refresh\";window.close();", true);
            }
        }

    }
}
