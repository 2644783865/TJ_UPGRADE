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
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_GZQDADD : System.Web.UI.Page
    {
        string action = string.Empty;
        string id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];
            if (!IsPostBack)
            {
                if (action=="update")
                {
                    this.BindYearMoth(ddlYear,ddlMoth);
                    id = Request.QueryString["id"];
                    GetDataByID(id);
                }
            }
        }


        /// <summary>
        /// 绑定年月
        /// </summary>
        /// <param name="dpl_Year"></param>
        /// <param name="dpl_Month"></param>
        private void BindYearMoth(DropDownList ddl_Year, DropDownList ddl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                ddl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            ddl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));

            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                ddl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            ddl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));

        }
        /// <summary>
        /// 修改数据库中的数据
        /// </summary>
        /// <param name="id"></param>
        protected void GetDataByID(string id)
        {
            string sqltext = "select *,(QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY) as QD_YFHJ,(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG) as QD_DaiKouXJ,((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_CLBT+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ+QD_ShuiDian+QD_GeShui+QD_KOUXIANG)) as QD_ShiFaJE from (select * from View_OM_GZQD left join OM_KQTJ on (View_OM_GZQD.QD_STID=OM_KQTJ.KQ_ST_ID and View_OM_GZQD.QD_YEARMONTH=OM_KQTJ.KQ_DATE))t where QD_ID = '" + id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            dr.Read();

            string str1 = dr["QD_YEARMONTH"].ToString().Trim().Substring(0, 4);
            string str2 = dr["QD_YEARMONTH"].ToString().Trim().Substring(5, 2);
            txtQD_Worknumber.Text = dr["ST_WORKNO"].ToString().Trim();
            lbQD_ID.Text = dr["QD_ID"].ToString().Trim();
            txtQD_Name.Text = dr["ST_NAME"].ToString().Trim();
            txtQD_QuFen.Text = dr["QD_QFBS"].ToString().Trim();
            txtQD_BuMen.Text = dr["DEP_NAME"].ToString().Trim();
            txtQD_JCGZ.Text = dr["QD_JCGZ"].ToString();
            txtQD_GZGL.Text = dr["QD_GZGL"].ToString();
            txtQD_GDGZ.Text = dr["QD_GDGZ"].ToString();
            txtQD_JXGZ.Text = dr["QD_JXGZ"].ToString();
            txtQD_JiangLi.Text = dr["QD_JiangLi"].ToString();
            txtQD_BingJiaGZ.Text = dr["QD_BingJiaGZ"].ToString();
            txtQD_JiaBanGZ.Text = dr["QD_JiaBanGZ"].ToString();
            txtQD_BFJB.Text = dr["QD_BFJB"].ToString();
            txtQD_ZYBF.Text = dr["QD_ZYBF"].ToString();
            txtQD_BFZYB.Text = dr["QD_BFZYB"].ToString();

            txtQD_NianJiaGZ.Text = dr["QD_NianJiaGZ"].ToString();
            txtQD_YKGW.Text = dr["QD_YKGW"].ToString();
            txtQD_TZBF.Text = dr["QD_TZBF"].ToString();
            txtQD_TZBK.Text = dr["QD_TZBK"].ToString();
            txtQD_QTFY.Text = dr["QD_QTFY"].ToString();

            txtQD_JTBT.Text = dr["QD_JTBT"].ToString();
            txtQD_FSJW.Text = dr["QD_FSJW"].ToString();
            txtQD_CLBT.Text = dr["QD_CLBT"].ToString();

            txtQD_YFHJ.Text = dr["QD_YFHJ"].ToString();//应发合计
            txtQD_YLBX.Text = dr["QD_YLBX"].ToString();
            txtQD_SYBX.Text = dr["QD_SYBX"].ToString();
            txtQD_YiLiaoBX.Text = dr["QD_YiLiaoBX"].ToString();
            txtQD_DEJZ.Text = dr["QD_DEJZ"].ToString();
            txtQD_BuBX.Text = dr["QD_BuBX"].ToString();
            txtQD_GJJ.Text = dr["QD_GJJ"].ToString();
            txtQD_BGJJ.Text = dr["QD_BGJJ"].ToString();
            txtQD_ShuiDian.Text = dr["QD_ShuiDian"].ToString();
            txtQD_KOUXIANG.Text = dr["QD_KOUXIANG"].ToString();

            txtQD_GeShui.Text = dr["QD_GeShui"].ToString();
            txtQD_DaiKouXJ.Text = dr["QD_DaiKouXJ"].ToString();//代扣小计
            txtQD_ShiFaJE.Text = dr["QD_ShiFaJE"].ToString();//实发金额
            txtQD_GangWei.Text = dr["DEP_NAME_POSITION"].ToString();
            tbnote.Text = dr["QD_NOTE"].ToString();

            dr.Close();
            ddlYear.Items.FindByText(str1).Selected = true;
            ddlMoth.Items.FindByText(str2).Selected = true;
        }



        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (ddlYear.SelectedIndex == 0 || ddlMoth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！');", true);
                return;
            }
            string sqlifsc = "select * from OM_GZHSB where GZ_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMoth.SelectedValue.ToString().Trim() + "' and OM_GZSCBZ='1'";
            System.Data.DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifsc);
            if (dtifsc.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月已生成工资，不能修改！！！');", true);
                return;
            }
            if (action == "update")
            {
                List<string> sqllist = new List<string>();
                sqllist.Clear();
                sqllist.Add("update OM_GZQD set QD_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMoth.SelectedValue.ToString().Trim() + "',QD_QFBS='" + txtQD_QuFen.Text.Trim() + "', QD_JCGZ=" + CommonFun.ComTryDecimal(txtQD_JCGZ.Text.Trim()) + ", QD_GZGL=" + CommonFun.ComTryDecimal(txtQD_GZGL.Text.Trim()) + ", QD_GDGZ=" + CommonFun.ComTryDecimal(txtQD_GDGZ.Text.Trim()) + ", QD_JXGZ=" + CommonFun.ComTryDecimal(txtQD_JXGZ.Text.Trim()) + ", QD_JiangLi=" + CommonFun.ComTryDecimal(txtQD_JiangLi.Text.Trim()) + ", QD_BingJiaGZ=" + CommonFun.ComTryDecimal(txtQD_BingJiaGZ.Text.Trim()) + ", QD_JiaBanGZ=" + CommonFun.ComTryDecimal(txtQD_JiaBanGZ.Text.Trim()) + ",QD_BFJB=" + CommonFun.ComTryDecimal(txtQD_BFJB.Text.Trim()) + ",QD_ZYBF=" + CommonFun.ComTryDecimal(txtQD_ZYBF.Text.Trim()) + ",QD_BFZYB=" + CommonFun.ComTryDecimal(txtQD_BFZYB.Text.Trim()) + ",QD_NianJiaGZ=" + CommonFun.ComTryDecimal(txtQD_NianJiaGZ.Text.Trim()) + ",QD_YKGW=" + CommonFun.ComTryDecimal(txtQD_YKGW.Text.Trim()) + ",QD_TZBF=" + CommonFun.ComTryDecimal(txtQD_TZBF.Text.Trim()) + ",QD_TZBK=" + CommonFun.ComTryDecimal(txtQD_TZBK.Text.Trim()) + ",QD_QTFY=" + CommonFun.ComTryDecimal(txtQD_QTFY.Text.Trim()) + ",QD_JTBT=" + CommonFun.ComTryDecimal(txtQD_JTBT.Text.Trim()) + ",QD_FSJW=" + CommonFun.ComTryDecimal(txtQD_FSJW.Text.Trim()) + ",QD_CLBT=" + CommonFun.ComTryDecimal(txtQD_CLBT.Text.Trim()) + ",QD_YLBX=" + CommonFun.ComTryDecimal(txtQD_YLBX.Text.Trim()) + ",QD_SYBX=" + CommonFun.ComTryDecimal(txtQD_SYBX.Text.Trim()) + ",QD_YiLiaoBX=" + CommonFun.ComTryDecimal(txtQD_YiLiaoBX.Text.Trim()) + ",QD_DEJZ=" + CommonFun.ComTryDecimal(txtQD_DEJZ.Text.Trim()) + ",QD_BuBX=" + CommonFun.ComTryDecimal(txtQD_BuBX.Text.Trim()) + ",QD_GJJ=" + CommonFun.ComTryDecimal(txtQD_GJJ.Text.Trim()) + ",QD_BGJJ=" + CommonFun.ComTryDecimal(txtQD_BGJJ.Text.Trim()) + ",QD_KOUXIANG=" + CommonFun.ComTryDecimal(txtQD_KOUXIANG.Text.Trim()) + ",QD_ShuiDian=" + CommonFun.ComTryDecimal(txtQD_ShuiDian.Text.Trim()) + ",QD_GeShui=" + CommonFun.ComTryDecimal(txtQD_GeShui.Text.Trim()) + ",QD_NOTE='" + tbnote.Text.Trim() + "' where QD_ID=" + CommonFun.ComTryInt(lbQD_ID.Text.ToString().Trim()) + "");
                
                DBCallCommon.ExecuteTrans(sqllist);
                updategeshui();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('修改成功！');", true);
                Response.Redirect("OM_GZQDJS.aspx");
            }
           
        }

        private void updategeshui()
        {
            string gesdatasource = "select *,((QD_JCGZ+QD_GZGL+QD_GDGZ+QD_JXGZ+QD_JiangLi+QD_BingJiaGZ+QD_JiaBanGZ+QD_BFJB+QD_ZYBF+QD_BFZYB+QD_NianJiaGZ+QD_YKGW+QD_TZBF+QD_TZBK+QD_JTBT+QD_FSJW+QD_QTFY)-(QD_YLBX+QD_SYBX+QD_YiLiaoBX+QD_DEJZ+QD_BuBX+QD_GJJ+QD_BGJJ)-QD_KOUXIANG) as QD_KOUSJS from OM_GZQD left join OM_KQTJ on KQ_ST_ID=QD_STID and KQ_DATE=QD_YEARMONTH where QD_ID=" + CommonFun.ComTryInt(lbQD_ID.Text.ToString().Trim()) + "";
            System.Data.DataTable dtdatasource = DBCallCommon.GetDTUsingSqlText(gesdatasource);
            List<string> listgeshui = new List<string>();
            //个税
            if (dtdatasource.Rows.Count > 0)
            {
                double ksjsmoney = CommonFun.ComTryDouble(dtdatasource.Rows[0]["QD_KOUSJS"].ToString().Trim());
                double jsmoney = ksjsmoney - 3500;
                double geshui = 0;
                if (jsmoney > 0)
                {
                    if (jsmoney < 1500)
                    {
                        geshui = jsmoney * 0.03;
                    }
                    else if (jsmoney >= 1500 && jsmoney < 4500)
                    {
                        geshui = jsmoney * 0.1 - 105;
                    }
                    else if (jsmoney >= 4500 && jsmoney < 9000)
                    {
                        geshui = jsmoney * 0.2 - 555;
                    }
                    else if (jsmoney >= 9000 && jsmoney < 35000)
                    {
                        geshui = jsmoney * 0.25 - 1005;
                    }
                    else
                    {
                        geshui = 0;
                    }
                }
                string insertgeshui = "update OM_GZQD set QD_GeShui=" + Math.Round(geshui, 2) + " where QD_ID=" + CommonFun.ComTryInt(lbQD_ID.Text.ToString().Trim()) + "";
                listgeshui.Add(insertgeshui);

                //插入修改记录

                string sqlgetdata = "select * from OM_GZQD where QD_ID=" + CommonFun.ComTryInt(lbQD_ID.Text.ToString().Trim()) + "";
                DataTable dtgetdata = DBCallCommon.GetDTUsingSqlText(sqlgetdata);
                if (dtgetdata.Rows.Count > 0)
                {
                    listgeshui.Add("insert into OM_GZQDeditJL(QD_SHBH,QD_YEARMONTH,QD_STID,QD_HTZT,QD_QFBS,QD_JCGZ,QD_GZGL,QD_GDGZ,QD_JXGZ,QD_JiangLi,QD_BingJiaGZ,QD_JiaBanGZ,QD_BFJB,QD_ZYBF,QD_BFZYB,QD_NianJiaGZ,QD_YKGW,QD_TZBF,QD_TZBK,QD_QTFY,QD_JTBT,QD_FSJW,QD_CLBT,QD_YLBX,QD_SYBX,QD_YiLiaoBX,QD_DEJZ,QD_BuBX,QD_GJJ,QD_BGJJ,QD_ShuiDian,QD_KOUXIANG,QD_GeShui,QD_edittime,QD_editname,QD_NOTE) values('" + dtgetdata.Rows[0]["QD_SHBH"].ToString().Trim() + "','" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMoth.SelectedValue.ToString().Trim() + "','" + dtgetdata.Rows[0]["QD_STID"].ToString().Trim() + "','" + dtgetdata.Rows[0]["QD_HTZT"].ToString().Trim() + "','" + txtQD_QuFen.Text.Trim() + "'," + CommonFun.ComTryDecimal(txtQD_JCGZ.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_GZGL.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_GDGZ.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_JXGZ.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_JiangLi.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_BingJiaGZ.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_JiaBanGZ.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_BFJB.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_ZYBF.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_BFZYB.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_NianJiaGZ.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_YKGW.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_TZBF.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_TZBK.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_QTFY.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_JTBT.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_FSJW.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_CLBT.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_YLBX.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_SYBX.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_YiLiaoBX.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_DEJZ.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_BuBX.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_GJJ.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_BGJJ.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_ShuiDian.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtQD_KOUXIANG.Text.Trim()) + "," + Math.Round(geshui, 2) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + tbnote.Text.Trim() + "')");

                }

                DBCallCommon.ExecuteTrans(listgeshui);
            }
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnBack_OnClick(object sender, EventArgs e)
        {

            Response.Redirect("OM_GZQDJS.aspx");
        }

    }
    
}
