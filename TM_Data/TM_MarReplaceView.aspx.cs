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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MarReplaceView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindData();
            }
        }

        protected void BindData()
        {
            string _xuhao_engid_marid_table = Server.HtmlDecode(Request.QueryString["xuhao_engid_marid_table"].ToString());
            string[] a = _xuhao_engid_marid_table.Split(',');
            string tracknum = "";
            string sql_tracknum = "";

            string _xuhao = a[0];
            string _marid = a[2];
            ViewState["_engid"] = a[1];
            ViewState["_marid"] = a[2];

            if (a[3] == "View_TM_MPHZY")
            {
                sql_tracknum = "select MP_TRACKNUM from View_TM_MPHZY where MP_ENGID='" + ViewState["_engid"].ToString()+ "' AND MP_NEWXUHAO='" + _xuhao + "' and MP_MARID='" + _marid + "' and MP_STATERV='8' AND MP_STATUS='0'";
            }
            else if (a[3] == "View_TM_OUTSOURCELIST")
            {
                sql_tracknum = "select OSL_TRACKNUM from View_TM_OUTSOURCELIST where OSL_ENGID='" + ViewState["_engid"].ToString() + "' AND  OSL_NEWXUHAO='" + _xuhao + "' and OSL_MARID='" + _marid + "' and OST_STATE='8' AND OSL_STATUS='0'";
            }

            if (sql_tracknum != "")
            {
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_tracknum);
                if (dt.Rows.Count>0)
                {
                    tracknum = dt.Rows[0][0].ToString();
                    string sql_findmarreplace = "SELECT * from View_TBPC_MARREPLACE_total_all_detail where ptcode='" + tracknum + "'";
                    if (rblMarFirst.SelectedValue == "0")//未提交
                    {
                        sql_findmarreplace += " and totalstate='000'";
                    }
                    else if (rblMarFirst.SelectedValue == "1")//审核中
                    {
                        sql_findmarreplace += " and ((totalstate='111' or totalstate='211') or (totalstate='311' and (MP_CKSHRID is not null or MP_CKSHRID!='') and (MP_CKSHRTIME is null or MP_CKSHRTIME='')))";
                    }
                    else if (rblMarFirst.SelectedValue == "2")//已驳回
                    {
                        sql_findmarreplace += " and (totalstate='300' OR totalstate='200' or (MP_CKSTATE='2' and totalstate='311'))";
                    }
                    else if (rblMarFirst.SelectedValue == "3")//已通过
                    {
                        sql_findmarreplace += " and ((totalstate='311' and ((MP_CKSHRID is null or MP_CKSHRID='') and (MP_CKSHRTIME is null or MP_CKSHRTIME='')) or ((MP_CKSHRID is not null or MP_CKSHRID!='') and (MP_CKSHRTIME is not null or MP_CKSHRTIME!='') and MP_CKSTATE='1')))";
                    }
                    else if(rblMarFirst.SelectedValue == "4")//全部
                    {
                        ;
                    }
                    Marreplace_detail_repeater.DataSource = DBCallCommon.GetDTUsingSqlText(sql_findmarreplace);
                    Marreplace_detail_repeater.DataBind();
                    if (Marreplace_detail_repeater.Items.Count > 0)
                    {
                        NoDataPanel1.Visible = false;
                    }
                    else
                    {
                        NoDataPanel1.Visible = true;
                    }
                }
            }


        }
        /// <summary>
        /// 该物料其它代用计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOtherMar_OnClick(object sender, EventArgs e)
        {
            btnOtherMar.Visible = false;
            string sql = "select * from View_TBPC_MARREPLACE_total_all_detail where  marid='" + ViewState["_marid"].ToString() + "' and plancode like '" + ViewState["_engid"].ToString() + "%' ";
            if (rblMarSecond.SelectedValue == "0")//未提交
            {
                sql += " and totalstate='000'";
            }
            else if (rblMarSecond.SelectedValue == "1")//审核中
            {
                sql += " and ((totalstate='111' or totalstate='211') or (totalstate='311' and (MP_CKSHRID is not null or MP_CKSHRID!='') and (MP_CKSHRTIME is null or MP_CKSHRTIME='')))";
            }
            else if (rblMarSecond.SelectedValue == "2")//已驳回
            {
                sql += " and (totalstate='300' OR totalstate='200' or (MP_CKSTATE='2' and totalstate='311'))";
            }
            else if (rblMarSecond.SelectedValue == "3")//已通过
            {
                sql += " and ((totalstate='311' and ((MP_CKSHRID is null or MP_CKSHRID='') and (MP_CKSHRTIME is null or MP_CKSHRTIME='')) or ((MP_CKSHRID is not null or MP_CKSHRID!='') and (MP_CKSHRTIME is not null or MP_CKSHRTIME!='') and MP_CKSTATE='1')))";
            }
            
            Repeater1.DataSource = DBCallCommon.GetDTUsingSqlText(sql);
            Repeater1.DataBind();
            if (Repeater1.Items.Count > 0)
            {
                NoDataPanel2.Visible = false;
            }
            else
            {
                NoDataPanel2.Visible = true;
            }
        }


        protected void rblMarFirst_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindData();
        }
    }
}
