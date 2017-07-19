using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ZCZJ_DPF.FM_Data
{
    public partial class CB_Confirm_detail : System.Web.UI.Page
    {
        string sql = "";
        string id = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request.QueryString["id"];
            if (!IsPostBack)
            {
                lblID.Text = id;
                InitVar();
            }
        }

        public void InitVar()
        {
            //if (Session["UserDeptID"].ToString() == "01")//公司领导
            //{
            //    confirm1.Visible = true;
            //    confirm2.Visible = true;
            //    confirm3.Visible = true;
            //    confirm4.Visible = true;
            //    btnCG.Visible = true;
            //    btnDQZZ.Visible = true;
            //}
            if (Session["UserDeptID"].ToString() == "12")//市场部
            {
                confirm1.Visible = true;
                confirm2.Visible = false;
                confirm3.Visible = false;
                confirm4.Visible = false;
                btnCG.Visible = false;
                btnDQZZ.Visible = false;
            }
            else if (Session["UserDeptID"].ToString() == "03")//技术
            {
                confirm1.Visible = false;
                confirm2.Visible = true;
                confirm3.Visible = false;
                confirm4.Visible = false;
                btnCG.Visible = false;
                btnDQZZ.Visible = false;

            }
            else if (Session["UserDeptID"].ToString() == "04")//生产部  
            {
                confirm1.Visible = false;
                confirm2.Visible = false;
                confirm3.Visible = false;
                confirm4.Visible = true;
                btnCG.Visible = false;
                btnDQZZ.Visible = false;

            }
            else if (Session["UserDeptID"].ToString() == "06")//采购部
            {
                confirm1.Visible = false;
                confirm2.Visible = false;
                confirm3.Visible = false;
                confirm4.Visible = false;
                btnCG.Visible = true;
                btnDQZZ.Visible = false;

            }
            else if (Session["UserDeptID"].ToString() == "07")//储运部 
            {
                confirm1.Visible = false;
                confirm2.Visible = false;
                confirm3.Visible = true;
                confirm4.Visible = false;
                btnCG.Visible = false;
                btnDQZZ.Visible = false;
            }
            else if (Session["UserDeptID"].ToString() == "08")//财务部 
            {
                confirm1.Visible = true;
                confirm2.Visible = true;
                confirm3.Visible = true;
                confirm4.Visible = true;
                btnCG.Visible = true;
                btnDQZZ.Visible = true;
            }
            else if (Session["UserDeptID"].ToString() == "09")//电气制造部
            {
                confirm1.Visible = true;
                confirm2.Visible = true;
                confirm3.Visible = true;
                confirm4.Visible = true;
                btnCG.Visible = true;
                btnDQZZ.Visible = true;
            }
            else
            {
                confirm1.Visible = false;
                confirm2.Visible = false;
                confirm3.Visible = false;
                confirm4.Visible = false;
                btnCG.Visible = false;
                btnDQZZ.Visible = false;
            }


            sql = " select * from TBCB_BMCONFIRM where TASK_ID='"+id+"' ";
            SqlDataReader dr=DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                //市场部
                tbht.Text = dr["HT_ID"].ToString();
                tbdanwei.Text = dr["DANWEI"].ToString();             
                tbjsl.Text = dr["JSL"].ToString();            
                tbdj.Text = dr["DANJIA"].ToString();
                tbjsje.Text = dr["PRICE"].ToString();
                if (tbht.Text.Trim() != "" && tbdanwei.Text.Trim() != "" && tbjsl.Text.Trim() != "0" && tbdj.Text.Trim() != "0" && tbjsje.Text.Trim() != "")
                {
                    confirm1.Visible = false;
                }
                //技术部
                rbfy.SelectedValue = convert(dr["JFY"].ToString());
                rbww.SelectedValue = convert(dr["JWW"].ToString());
                if (rbfy.SelectedValue.ToString() == "是" && rbww.SelectedValue.ToString() == "是")
                {
                    confirm2.Visible = false;
                }
              
                //储运部
                cfy.SelectedValue = convert(dr["CFY"].ToString());
                cysfp.SelectedValue = convert(dr["CFP"].ToString());
                cflck.SelectedValue = convert(dr["CCRK"].ToString());
                if (cfy.SelectedValue.ToString() == "是" && cysfp.SelectedValue.ToString()=="是" && cflck.SelectedValue.ToString()=="是")
                {
                    confirm3.Visible=false;
                }

                //生产部
                RadioButtonList1.SelectedValue = convert(dr["SJS"].ToString());
                if (RadioButtonList1.SelectedValue.ToString() == "是")
                {
                    confirm4.Visible = false;
                }
                //采购部
                rblCG.SelectedValue = convert(dr["CGFP"].ToString());
                if (rblCG.SelectedValue == "是")
                {
                    btnCG.Visible = false;
                }
                //电器制造部
                rblDQZZ.SelectedValue = convert(dr["DQFP"].ToString());
                if (rblCG.SelectedValue == "是")
                {
                    btnDQZZ.Visible = false;
                }
            }
            dr.Close();
        }

        protected void confirm1_Click(object sender, EventArgs e)
        {
            if (((Button)sender).CommandName == "btscb")
            {
                if (tbht.Text.Trim() == "" || tbdanwei.Text.Trim() == "" || tbjsl.Text.Trim() == "" || tbdj.Text.Trim() == "" || tbjsje.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, Page.GetType(), System.DateTime.Now.Ticks.ToString(), "alert('请将信息填写完整！！！');", true);
                }
                //else if (Convert.ToDouble(tbjsl.Text.Trim()) * Convert.ToDouble(tbdj.Text.Trim()) != Convert.ToDouble(tbjsje.Text.Trim()))
                //{
                //    ScriptManager.RegisterStartupScript(this.UpdatePanel1, Page.GetType(), System.DateTime.Now.Ticks.ToString(), "alert('数据填写不正确！！！');", true);
                //}
                else
                {
                    sql = " update TBCB_BMCONFIRM set HT_ID='" + tbht.Text + "',DANWEI='" + tbdanwei.Text + "', JSL='" + tbjsl.Text + "',DANJIA='" + tbdj.Text.ToString() + "' ,PRICE='" + tbjsje.Text + "' where TASK_ID='" + id + "' ";
                    DBCallCommon.GetDRUsingSqlText(sql);
                    this.checkOver();
                }
            }

            else if (((Button)sender).CommandName == "btjsb")
            {
                sql = " update TBCB_BMCONFIRM set JFY='" + rbfy.SelectedItem.Text + "',JWW='" + rbww.SelectedItem.Text + "' where TASK_ID='" + id + "' ";
                DBCallCommon.GetDRUsingSqlText(sql);
                this.checkOver();

                //string confirmtime = System.DateTime.Now.ToString("yyyy-MM-dd");
                //sql = DBCallCommon.GetStringValue("connectionStrings");
                //SqlConnection con = new SqlConnection(sql);
                //con.Open();
                //SqlCommand cmd = new SqlCommand("PROJOVER", con);
                //cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add("@TASK_ID", SqlDbType.VarChar, 50);			//增加参数
                //cmd.Parameters["@TASK_ID"].Value = lblID.Text;							//为参数初始化

                //cmd.Parameters.Add("@confirmtime", SqlDbType.VarChar, 50);			//确认时间
                //cmd.Parameters["@confirmtime"].Value = confirmtime;

                //cmd.ExecuteNonQuery();
                //con.Close();


            }

            else if (((Button)sender).CommandName == "btcyb")
            {
                sql = " update TBCB_BMCONFIRM set CFY='" + cfy.SelectedItem.Text + "',CFP='" + cflck.SelectedItem.Text + "',CCRK='" + cflck.SelectedItem.Text + "' where TASK_ID='" + id + "' ";
                DBCallCommon.GetDRUsingSqlText(sql);
                this.checkOver();

            }

            else  if (((Button)sender).CommandName == "btscb1")
            {
                sql = " update TBCB_BMCONFIRM set SJS='" + RadioButtonList1.SelectedItem.Text + "' where TASK_ID='" + id + "' ";
                DBCallCommon.GetDRUsingSqlText(sql);
                this.checkOver();
            }
            else if (((Button)sender).CommandName == "CG")
            {
                sql = " update TBCB_BMCONFIRM set CGFP='" + rblCG.SelectedValue.ToString() + "' where TASK_ID='" + id + "' ";
                DBCallCommon.GetDRUsingSqlText(sql);
                this.checkOver();
            }
            else if (((Button)sender).CommandName == "DQZZ")
            {
                sql = " update TBCB_BMCONFIRM set DQFP='是' where TASK_ID='" + id + "' ";
                DBCallCommon.GetDRUsingSqlText(sql);
                this.checkOver();
            }
        }

        private void checkOver()
        {
            //是否最终完工
            if (PD() == true)
            {
                string sqltet = " update TBCB_BMCONFIRM set STATUS='1',WGRQ='" + DateTime.Now.ToShortDateString() + "' where TASK_ID='" + id + "' ";
                DBCallCommon.GetDRUsingSqlText(sqltet);
            }
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, Page.GetType(), System.DateTime.Now.Ticks.ToString(), "window.returnValue=\"refresh\";window.close();", true);
        }
        public string convert(string m)
        {
            if (m.Trim() == "")
            {
                return "0";
            }
            else
            {
                return m.Trim();
            }
        }

        public bool PD()
        {
            bool pd=false;

            string sqltext = " select * from TBCB_BMCONFIRM where TASK_ID='" + id + "' ";
            SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sqltext);
            if(dr1.Read())
            {
                if (dr1["HT_ID"].ToString() != "" && dr1["DANWEI"].ToString() != ""
                    && dr1["JSL"].ToString() != "" && dr1["DANJIA"].ToString() != ""
                    && dr1["PRICE"].ToString() != "" && convert(dr1["JFY"].ToString()) == "是"
                    && convert(dr1["JWW"].ToString()) == "是" && convert(dr1["CFY"].ToString()) == "是"
                    && convert(dr1["CFP"].ToString()) == "是" && convert(dr1["CCRK"].ToString()) == "是"
                    && convert(dr1["SJS"].ToString()) == "是"
                    && convert(dr1["CGFP"].ToString()) == "是"
                    && convert(dr1["DQFP"].ToString()) == "是")
                {
                    pd = true;
                }
                else
                {
                    sqltext = "update TBCB_BMCONFIRM set status='0' where TASK_ID='" + id + "' ";
                    DBCallCommon.GetDRUsingSqlText(sqltext);
                }
            }
            dr1.Close();

            return pd;
        }

    }
}
