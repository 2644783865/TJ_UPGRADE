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
using ZCZJ_DPF;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_shuidfdetail : System.Web.UI.Page
    {
        string action = "";
        string bh = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"].ToString().Trim();
            if (!IsPostBack)
            {
                if (action == "edit")
                {
                    bh = Request.QueryString["id"].ToString().Trim();
                    lblastdate.Visible = false;
                    bindssdata();
                }
                else if (action == "add")
                {
                    ssnum.Enabled = true;
                }
            }
        }

        private void bindssdata()
        {
            string sqltext = "select *,cast((enddf-stratdf-gscddl) as decimal(12,2)) as shiydl,cast(((enddf-stratdf-gscddl)*pricedf) as decimal(12,2)) as dianfje,cast((endsf-startsf-gscdsl) as decimal(12,2)) as shiysl,cast(((endsf-startsf-gscdsl)*pricesf) as decimal(12,2)) as shuifje,cast((((enddf-stratdf-gscddl)*pricedf)+((endsf-startsf-gscdsl)*pricesf)) as decimal(12,2)) as zongje,cast((((enddf-stratdf-gscddl)*pricedf)/ssrens+((endsf-startsf-gscdsl)*pricesf)/ssrens) as decimal(12,2)) as renjunfy,realmoney from (select IDsdmx,IDSDF,ssrens,ssnum,OM_SDFY.startdate,OM_SDFY.enddate,stratdf,enddf,pricedf,startsf,endsf,pricesf,ST_NAME,note,peopleid,fangjnum,gscddl,gscdsl,realmoney from OM_SDFY left join OM_SDFdetail on (OM_SDFY.ssnum=OM_SDFdetail.fangjnum and OM_SDFY.startdate=OM_SDFdetail.startdate and OM_SDFY.enddate=OM_SDFdetail.enddate) left join TBDS_STAFFINFO on OM_SDFdetail.peopleid=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t where IDSDF='" + bh + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                lbdprice.Text = dt.Rows[0]["pricedf"].ToString().Trim();
                lbsprice.Text = dt.Rows[0]["pricesf"].ToString().Trim();
                ssnum.Text = dt.Rows[0]["ssnum"].ToString().Trim();
                ssrens.Text = dt.Rows[0]["ssrens"].ToString().Trim();
                startdate.Text = dt.Rows[0]["startdate"].ToString().Trim();
                enddate.Text = dt.Rows[0]["enddate"].ToString().Trim();
                stratdf.Text = dt.Rows[0]["stratdf"].ToString().Trim();
                enddf.Text = dt.Rows[0]["enddf"].ToString().Trim();

                gscddl.Text = dt.Rows[0]["gscddl"].ToString().Trim();
                gscdsl.Text = dt.Rows[0]["gscdsl"].ToString().Trim();

                shiydus.Text = dt.Rows[0]["shiydl"].ToString().Trim();
                dianfje.Text = dt.Rows[0]["dianfje"].ToString().Trim();
                startsf.Text = dt.Rows[0]["startsf"].ToString().Trim();
                endsf.Text = dt.Rows[0]["endsf"].ToString().Trim();
                shiyduns.Text = dt.Rows[0]["shiysl"].ToString().Trim();
                shuifje.Text = dt.Rows[0]["shuifje"].ToString().Trim();
                shuidianhj.Text = dt.Rows[0]["zongje"].ToString().Trim();
                renjunfy.Text = dt.Rows[0]["renjunfy"].ToString().Trim();
                note.Text = dt.Rows[0]["note"].ToString().Trim();
            }
        }

        protected void ssnum_textchange(object sender, EventArgs e)
        {
            string sqlgetdata = "select * from OM_SUSHE where housenum='" + ssnum.Text.Trim() + "'";
            DataTable dtgetdata = DBCallCommon.GetDTUsingSqlText(sqlgetdata);
            if (dtgetdata.Rows.Count > 0)
            {
                ssrens.Text = dtgetdata.Rows[0]["xyrs"].ToString().Trim();
                string sqllasttime = "select * from OM_SDFY where ssnum='" + ssnum.Text.Trim() + "' and IDSDF in(select max(IDSDF) from OM_SDFY where ssnum='" + ssnum.Text.Trim() + "')";
                DataTable dtlasttime = DBCallCommon.GetDTUsingSqlText(sqllasttime);
                if (dtlasttime.Rows.Count > 0)
                {
                    lblastdate.Text = dtlasttime.Rows[0]["startdate"].ToString().Trim() + "—" + dtlasttime.Rows[0]["enddate"].ToString().Trim();
                    startdate.Text = dtlasttime.Rows[0]["enddate"].ToString().Trim();
                    stratdf.Text = dtlasttime.Rows[0]["enddf"].ToString().Trim();
                    startsf.Text = dtlasttime.Rows[0]["endsf"].ToString().Trim();
                }
                else
                {
                    lblastdate.Text = "";
                    startdate.Text = "";
                    stratdf.Text = "";
                    startsf.Text = "";
                }

                string sqlprice = "select * from OM_SDPRICE";
                DataTable dtprice = DBCallCommon.GetDTUsingSqlText(sqlprice);
                if (dtprice.Rows.Count > 0)
                {
                    lbdprice.Text = dtprice.Rows[0]["dianprice"].ToString().Trim();
                    lbsprice.Text = dtprice.Rows[0]["shuiprice"].ToString().Trim();
                }

                //公司承担部分
                string sqlgscd = "select gscddl,gscdsl from OM_SDFY where IDSDF in(select max(IDSDF) from OM_SDFY)";
                DataTable dtgscd = DBCallCommon.GetDTUsingSqlText(sqlgscd);
                if (dtgscd.Rows.Count > 0)
                {
                    gscddl.Text = dtgscd.Rows[0]["gscddl"].ToString().Trim();
                    gscdsl.Text = dtgscd.Rows[0]["gscdsl"].ToString().Trim();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('房间号不正确！');", true);
                return;
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            if (ssrens.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('宿舍现有人数为0，不能添加水电费数据！');", true);
                return;
            }
            string sqlgetprice0 = "select * from OM_SDPRICE";
            DataTable dtprice0 = DBCallCommon.GetDTUsingSqlText(sqlgetprice0);
            if (dtprice0.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请添加水电费价格！');", true);
                return;
            }
            action = Request.QueryString["action"].ToString().Trim();
            List<string> sqltextlist = new List<string>();
            string sqlfjnum = "select * from OM_SUSHE where housenum='" + ssnum.Text.Trim() + "'";
            DataTable dtfjnum = DBCallCommon.GetDTUsingSqlText(sqlfjnum);
            if (dtfjnum.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('房间号不正确！');", true);
                return;
            }
            if (startdate.Text.Trim() == "" || enddate.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写起始和截止日期！');", true);
                return;
            }
            if (stratdf.Text.Trim() == "" || enddf.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写电表读数！');", true);
                return;
            }
            if (startsf.Text.Trim() == "" || endsf.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写水表读数！');", true);
                return;
            }


            if (startdate.Text.Trim() != "" && enddate.Text.Trim() != "" && (string.Compare(startdate.Text.Trim(), enddate.Text.Trim()) > 0))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('截止日期应大于起始日期！');", true);
                return;
            }
            if (stratdf.Text.Trim() != "" && enddf.Text.Trim() != "" && (CommonFun.ComTryDecimal(enddf.Text.Trim()) < CommonFun.ComTryDecimal(stratdf.Text.Trim())))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('电表截止读数应大于起始读数！');", true);
                return;
            }
            if (startsf.Text.Trim() != "" && endsf.Text.Trim() != "" && (CommonFun.ComTryDecimal(endsf.Text.Trim()) < CommonFun.ComTryDecimal(startsf.Text.Trim())))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('水表截止读数应大于起始读数！');", true);
                return;
            }
            //编辑
            if (action == "edit")
            {
                bh = Request.QueryString["id"].ToString().Trim();
                string sqldeleteru = "delete from OM_SDFdetail where fangjnum='" + ssnum.Text.Trim() + "' and startdate='" + startdate.Text.Trim() + "' and enddate='" + enddate.Text.Trim() + "'";
                DBCallCommon.ExeSqlText(sqldeleteru);
                string sqlupdatesdf = "update OM_SDFY set ssrens=" + CommonFun.ComTryInt(ssrens.Text.Trim()) + ",stratdf=" + CommonFun.ComTryDecimal(stratdf.Text.Trim()) + ",enddf=" + CommonFun.ComTryDecimal(enddf.Text.Trim()) + ",gscddl=" + CommonFun.ComTryDecimal(gscddl.Text.Trim()) + ",pricedf=" + CommonFun.ComTryDecimal(lbdprice.Text.Trim()) + ",startsf=" + CommonFun.ComTryDecimal(startsf.Text.Trim()) + ",endsf=" + CommonFun.ComTryDecimal(endsf.Text.Trim()) + ",gscdsl=" + CommonFun.ComTryDecimal(gscdsl.Text.Trim()) + ",pricesf=" + CommonFun.ComTryDecimal(lbsprice.Text.Trim()) + ",startdate='" + startdate.Text.Trim() + "',enddate='" + enddate.Text.Trim() + "',note='" + note.Text.Trim() + "' where IDSDF='" + bh + "'";
                sqltextlist.Add(sqlupdatesdf);
                string sqlgetmxupdate = "select * from OM_SSDEtail where SUSHEnum='" + ssnum.Text.Trim() + "'";
                DataTable dtgetmxupdate = DBCallCommon.GetDTUsingSqlText(sqlgetmxupdate);
                if (dtgetmxupdate.Rows.Count > 0)
                {
                    for (int i = 0; i < dtgetmxupdate.Rows.Count; i++)
                    {
                        string sqlinsertmxupdate = "insert into OM_SDFdetail(fangjnum,peopleid,startdate,enddate,realmoney) values('" + ssnum.Text.Trim() + "','" + dtgetmxupdate.Rows[i]["stid"].ToString().Trim() + "','" + startdate.Text.Trim() + "','" + enddate.Text.Trim() + "'," + CommonFun.ComTryDecimal(renjunfy.Text.Trim()) + ")";
                        sqltextlist.Add(sqlinsertmxupdate);
                    }
                }
            }
            //添加
            else if (action == "add")
            {
                string sql = "select * from OM_SDFY where ssnum='" + ssnum.Text.Trim() + "' and startdate='" + startdate.Text.Trim() + "' and enddate='" + enddate.Text.Trim() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据重复！');", true);
                    return;
                }
                else
                {
                    string sqlinsertsdf = "insert into OM_SDFY(ssnum,ssrens,stratdf,enddf,gscddl,pricedf,startsf,endsf,gscdsl,pricesf,startdate,enddate,note) values('" + ssnum.Text.Trim() + "'," + CommonFun.ComTryInt(ssrens.Text.Trim()) + "," + CommonFun.ComTryDecimal(stratdf.Text.Trim()) + "," + CommonFun.ComTryDecimal(enddf.Text.Trim()) + "," + CommonFun.ComTryDecimal(gscddl.Text.Trim()) + "," + CommonFun.ComTryDecimal(lbdprice.Text.Trim()) + "," + CommonFun.ComTryDecimal(startsf.Text.Trim()) + "," + CommonFun.ComTryDecimal(endsf.Text.Trim()) + "," + CommonFun.ComTryDecimal(gscdsl.Text.Trim()) + "," + CommonFun.ComTryDecimal(lbsprice.Text.Trim()) + ",'" + startdate.Text.Trim() + "','" + enddate.Text.Trim() + "','" + note.Text.Trim() + "')";
                    sqltextlist.Add(sqlinsertsdf);
                    string sqlgetmx = "select * from OM_SSDEtail where SUSHEnum='" + ssnum.Text.Trim() + "'";
                    DataTable dtgetmx = DBCallCommon.GetDTUsingSqlText(sqlgetmx);
                    if (dtgetmx.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtgetmx.Rows.Count; i++)
                        {
                            string sqlinsertmx = "insert into OM_SDFdetail(fangjnum,peopleid,startdate,enddate,realmoney) values('" + ssnum.Text.Trim() + "','" + dtgetmx.Rows[i]["stid"].ToString().Trim() + "','" + startdate.Text.Trim() + "','" + enddate.Text.Trim() + "'," + CommonFun.ComTryDecimal(renjunfy.Text.Trim()) + ")";
                            sqltextlist.Add(sqlinsertmx);
                        }
                    }
                }
            }
            DBCallCommon.ExecuteTrans(sqltextlist);
            Response.Write("<script>alert('操作已成功!');window.close();</script>");
        }
    }
}
