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



namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_CarHUICHANG : System.Web.UI.Page
    {
        string IDD = "";
        protected string action = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            IDD = Request.QueryString["ID"];
            action = Request.QueryString["action"];
            if (!IsPostBack)
            {
                SJDDL();
                if (action == "fache")
                {
                    TR1.Visible = false;
                    TR5.Visible = false;
                    TR7.Visible = false;
                    //TR6.Visible = false;
                    TR8.Visible = false;
                }
                if (action == "huiche")
                {
                    TR2.Visible = false;
                    TR3.Visible = false;
                    TR4.Visible = false;
                    TR7.Visible = false;
                    lbllc1.Visible = true;
                    jstime.Value = DateTime.Now.ToString();
                    string tt = "select LICHENG1 FROM TBOM_CARAPPLY WHERE ID='" + IDD.ToString() + "'";
                    DataTable DT = DBCallCommon.GetDTUsingSqlText(tt);
                    if (DT.Rows.Count > 0)
                    {
                        lbllc1.Text = DT.Rows[0]["LICHENG1"].ToString();
                        lbllc1.Text = "开始里程数（千米）：" + lbllc1.Text.ToString();
                    }
                }
                if (action == "cancel")
                {
                    TR1.Visible = false;
                    TR5.Visible = false;
                    //TR6.Visible = false;
                    TR2.Visible = false;
                    TR3.Visible = false;
                    TR4.Visible = false;
                    TR8.Visible = false;
                    //TR7.Visible = true;
                }
            }
        }

        protected string get_fache_mile()
        {
            string fa_hui_kai = "";
            if (action == "huiche")
            {
                string tt_huiche = "select LICHENG1 FROM TBOM_CARAPPLY WHERE ID='" + IDD.ToString() + "'";
                DataTable DT_huiche = DBCallCommon.GetDTUsingSqlText(tt_huiche);
                if (DT_huiche.Rows.Count > 0)
                {
                    fa_hui_kai = DT_huiche.Rows[0]["LICHENG1"].ToString();
                }
            }
            return fa_hui_kai;

        }

        private void SJDDL()
        {
            sj.Items.Clear();
            string sql = "select ST_NAME,ST_ID FROM TBDS_STAFFINFO WHERE ST_POSITION='0202'";
            DBCallCommon.BindDdl(sj, sql, "ST_NAME", "ST_ID");
        }
        protected void sjchanged(object sender, EventArgs e)
        {
            string sql = "select ST_TELE FROM TBDS_STAFFINFO WHERE ST_ID='" + sj.SelectedValue.Trim() + "'";
            DataTable DT = DBCallCommon.GetDTUsingSqlText(sql);
            if (DT.Rows.Count > 0)
            {
                sjcall.Text = DT.Rows[0]["ST_TELE"].ToString();
            }
        }
        protected void OK_CLICK(object sender, EventArgs e)
        {
            if (action == "huiche")
            {
                string hclc = jslc.Text.ToString();
                string tt = "select CARNUM,FACHEONLY,YDTIME,LICHENG1 FROM TBOM_CARAPPLY WHERE ID='" + IDD.ToString() + "'";
                DataTable DT = DBCallCommon.GetDTUsingSqlText(tt);
                if (DT.Rows.Count > 0)
                {
                    string jstm = jstime.Value.ToString();
                    string ydtime = DT.Rows[0]["YDTIME"].ToString();
                    string licheng1 = DT.Rows[0]["LICHENG1"].ToString();
                    string carnum2 = DT.Rows[0]["CARNUM"].ToString();
                    carnum2 = carnum2.Substring(0, 7);
                    if ((Convert.ToInt32(hclc) - Convert.ToInt32(licheng1)) <= 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写合适回程里程数！');", true);
                        return;
                    }

                    else
                    {
                        ////2016.6.7修改
                        //if ((Convert.ToInt32(hclc) - Convert.ToInt32(licheng1)) >= 200)
                        //{
                        //    MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                        //    DialogResult drqr = MessageBox.Show("行驶里程数大于200，可能是结束里程数填写错误，确定要继续吗？", "请确认里程数", messButton);
                        //    if (drqr == DialogResult.Cancel)//如果点击取消
                        //    {
                        //        return;
                        //    }

                        //}
                        licheng1 = Convert.ToString(Convert.ToInt32(hclc) - Convert.ToInt32(licheng1));
                    }

                    DateTime tm1 = Convert.ToDateTime(ydtime);
                    DateTime tm2 = Convert.ToDateTime(jstm);
                    TimeSpan jgtm = tm2 - tm1;

                    string dd = jgtm.Days.ToString();
                    string hh = jgtm.Hours.ToString();
                    string mm = jgtm.Minutes.ToString();
                    string ss = jgtm.Seconds.ToString();
                    if (Convert.ToInt32(hh) < 10)
                    {
                        hh = "0" + hh;
                    }
                    if (Convert.ToInt32(mm) < 10)
                    {
                        mm = "0" + mm;
                    }
                    if (Convert.ToInt32(ss) < 10)
                    {
                        ss = "0" + ss;
                    }
                    hh = Convert.ToString(Convert.ToInt32(dd) * 24 + Convert.ToInt32(hh));

                    string tm22 = hh + ':' + mm + ':' + ss;

                    string sqltext = "update  TBOM_CARINFO set STATE='0',MILEAGE='" + hclc.Trim() + "',MUDIDI='',NOWSIJI=''  where CARNUM='" + carnum2 + "'";
                    DBCallCommon.ExeSqlText(sqltext);
                    string sql = "update TBOM_CARAPPLY set TIME1='',WHOLETIME='" + tm22.ToString() + "',WHOLEJULI='" + licheng1.Trim() + "',TIME2='" + jstime.Value.Trim() + "',HUICHE=1,LICHENG2='" + hclc.Trim() + "',SJNOTE='" + note.Text.ToString() + "'  where FACHEONLY='" + DT.Rows[0]["FACHEONLY"].ToString() + "'";
                    DBCallCommon.ExeSqlText(sql);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:车辆回厂成功！！！');window.close();", true);
                }

                //Response.Write("<script>window.opener=null;window.close();</script>");
            }
            if (action == "fache")
            {
                string sql = "update TBOM_CARAPPLY set YDTIME='" + ydtime.Value.Trim() + "',SJNAME='" + sj.SelectedItem.Text.ToString() + "',SJID='" + sj.SelectedValue.Trim() + "',SJCALL='" + sjcall.Text.Trim() + "' WHERE FACHEONLY='" + IDD.ToString() + "'";
                DBCallCommon.ExeSqlText(sql);
                //修改
                string sqltext = "select carnum from TBOM_CARAPPLY where FACHEONLY='" + IDD.ToString() + "'";
                if (sqltext != "" || sqltext != null)
                {
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                    string carnumquan = dt.Rows[0][0].ToString();
                    string carnum = carnumquan.Split('/')[0];
                    string sqltextCL = "update TBOM_CARINFO set [STATE]=1 WHERE CARNUM='" + carnum + "'";
                    DBCallCommon.ExeSqlText(sqltextCL);


                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:发车-反馈成功！！！');window.close();", true);
            }

            if (action == "cancel")
            {
                string tt = "select CARNUM,FACHEONLY FROM TBOM_CARAPPLY WHERE CODE='" + IDD.ToString() + "'";
                DataTable DT = DBCallCommon.GetDTUsingSqlText(tt);
                if (DT.Rows.Count > 0)
                {
                    string count = "select count(FACHEONLY) from TBOM_CARAPPLY where FACHEONLY='" + DT.Rows[0]["FACHEONLY"].ToString() + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(count);
                    if (dt.Rows.Count > 1)
                    {
                        string sql = "update TBOM_CARALLRVW set STATE='9' WHERE CODE='" + IDD.ToString() + "'";
                        DBCallCommon.ExeSqlText(sql);
                        string sqltxt = "update TBOM_CARAPPLY set CANCEL='" + cancel.Text.Trim() + "',YDTIME='" + DateTime.Now.AddYears(100).ToShortDateString() + "' WHERE CODE='" + IDD.ToString() + "'";
                        DBCallCommon.ExeSqlText(sqltxt);
                    }
                    if (dt.Rows.Count == 1)
                    {
                        string sql = "update TBOM_CARALLRVW set STATE='9' WHERE CODE='" + IDD.ToString() + "'";
                        DBCallCommon.ExeSqlText(sql);
                        string sqll = "update TBOM_CARAPPLY set CANCEL='" + cancel.Text.Trim() + "',YDTIME='" + DateTime.Now.AddYears(100).ToShortDateString() + "' WHERE CODE='" + IDD.ToString() + "'";
                        DBCallCommon.ExeSqlText(sqll);
                        string carnum3 = DT.Rows[0]["CARNUM"].ToString();
                        if (carnum3 != "")
                        {
                            carnum3 = carnum3.Substring(0, 7);
                            string sqltext = "update  TBOM_CARINFO set STATE=0 where CARNUM='" + carnum3 + "'";
                            DBCallCommon.ExeSqlText(sqltext);
                        }
                    }
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:发车取消成功！！！');window.close();", true);
            }

        }
    }
}
