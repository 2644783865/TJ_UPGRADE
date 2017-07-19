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
    public partial class OM_SHUIDFSP_detail : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                asd.action = Request.QueryString["action"].ToString();
                asd.spbh = Request.QueryString["spbh"].ToString();
                asd.userid = Session["UserID"].ToString();
                asd.username = Session["UserName"].ToString();
                contrlkjx();//控件可见性和可用性
                UCPaging1.CurrentPage = 1;
                InitVar();
                bind();
            }
            InitVar();
        }
        private class asd
        {
            public static string spbh;
            public static string userid;
            public static string username;
            public static string action;
        }

        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }

        /// <summary>
        /// 分页初始化
        /// </summary>
        /// <param name="where"></param>
        private void InitPager()
        {
            pager_org.TableName = "(select IDsdmx,IDSDF,ssrens,ssnum,OM_SDFY.startdate,OM_SDFYSP.spbh,OM_SDFY.state,OM_SDFY.enddate,stratdf,enddf,pricedf,startsf,endsf,pricesf,ST_NAME,note,peopleid,fangjnum,gscddl,gscdsl,realmoney from OM_SDFY left join OM_SDFdetail on (OM_SDFY.ssnum=OM_SDFdetail.fangjnum and OM_SDFY.startdate=OM_SDFdetail.startdate and OM_SDFY.enddate=OM_SDFdetail.enddate)left join OM_SDFYSP on OM_SDFY.spbh=OM_SDFYSP.spbh left join TBDS_STAFFINFO on OM_SDFdetail.peopleid=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t";
            pager_org.PrimaryKey = "";
            pager_org.ShowFields = "IDSDF,ssnum,startdate,enddate,cast((enddf-stratdf-gscddl) as decimal(12,2)) as shiydl,pricedf,cast(((enddf-stratdf-gscddl)*pricedf) as decimal(12,2)) as dianfje,cast((endsf-startsf-gscdsl) as decimal(12,2)) as shiysl,pricesf,cast(((endsf-startsf-gscdsl)*pricesf) as decimal(12,2)) as shuifje,ST_NAME,cast((((enddf-stratdf-gscddl)*pricedf)/ssrens+((endsf-startsf-gscdsl)*pricesf)/ssrens) as decimal(12,2)) as renjunfy,realmoney,note";
            pager_org.OrderField = "ssnum,startdate,enddate";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;
            pager_org.PageSize = 60;
        }

        /// <summary>
        /// 换页事件
        /// </summary>
        private void Pager_PageChanged(int pageNumber)
        {
            bind();
        }
        private void bind()
        {
            if (asd.action == "add")
            {
                hid_creatstid.Value = asd.userid;
                txtcreatename.Text = asd.username;
                txtcreattime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                txtspbh.Text = Getspbh();
                if (txtspbh.Text == "")
                    btnsave.Visible = false;
            }
            if (asd.action == "read")
            {
                string sql = "select OM_SDFYSP.spbh as spbh,creatstname,creattime,dairumonth,shrname,shjl,shtime,shnote from OM_SDFYSP left join OM_SDFY on  OM_SDFY.spbh=OM_SDFYSP.spbh where OM_SDFYSP.spbh='" + asd.spbh + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    txtspbh.Text = dr["spbh"].ToString();
                    txtcreatename.Text = dr["creatstname"].ToString();
                    txtcreattime.Text = dr["creattime"].ToString();
                    txtdairumonth.Value = dr["dairumonth"].ToString();
                    txt_Shr.Text = dr["shrname"].ToString();
                    if (dr["shjl"].ToString() != "0")
                    {
                        rblShyj.SelectedValue = dr["shjl"].ToString();
                    }
                    lbShtime.Text = dr["shtime"].ToString();
                    txtShnote.Text = dr["shnote"].ToString();
                }
            }
            if (asd.action == "check" || asd.action == "alert")
            {
                string sql = "select OM_SDFYSP.spbh as spbh,creatstname,creattime,dairumonth,shrname from OM_SDFYSP left join OM_SDFY on  OM_SDFY.spbh=OM_SDFYSP.spbh where OM_SDFYSP.spbh='" + asd.spbh + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    txtspbh.Text = dr["spbh"].ToString();
                    txtcreatename.Text = dr["creatstname"].ToString();
                    txtcreattime.Text = dr["creattime"].ToString();
                    txtdairumonth.Value = dr["dairumonth"].ToString();
                    txt_Shr.Text = dr["shrname"].ToString();
                }
            }
            InitPager();
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptsushe, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            for (int j = 0; j < rptsushe.Items.Count; j++)
            {
                Label s = (Label)rptsushe.Items[j].FindControl("lbXuHao");
                s.Text = (j + 1 + (pager_org.PageIndex - 1) * UCPaging1.PageSize).ToString();
            }
            danyuangehebing();
        }
        //绑定审批编号
        protected string Getspbh()
        {
            string spbh = "";
            string sql1 = "select startdate,enddate,state from OM_SDFY where spbh is NULL and state='0'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
            if (dt1.Rows.Count > 0)
            {
                string[] a = dt1.Rows[0]["startdate"].ToString().Split('-');
                string[] b = a[0].Split('0');
                string[] c = dt1.Rows[0]["enddate"].ToString().Split('-');
                string[] d = c[0].Split('0');
                spbh = "ZSSDF-" + b[1] + a[1] + d[1] + c[1];
            }
            return spbh;
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (txtdairumonth.Value.Trim() == "")
            {
                Response.Write("<script>alert('请填写代入月份！')</script>");
                return;
            }
            if (txt_Shr.Text.Trim() == "" || hid_shrid.Value.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择审批人！');", true);
                return;
            }
            if (asd.action == "add")
            {
                if (rptsushe.Items.Count > 0)
                {
                    List<string> list = addlist();
                    if (list != null)
                    {
                        try
                        {
                            DBCallCommon.ExecuteTrans(list);
                            Response.Write("<script>alert('添加成功！')</script>");
                            btnsave.Visible = false;
                            btnsubmit.Visible = true;
                        }
                        catch
                        {
                            Response.Write("<script>alert('addlist数据操作错误，请联系管理员！')</script>");
                        }
                    }
                }
                else
                {
                    Response.Write("<script>alert('没有待审批的水电费数据！')</script>");
                    btnsave.Visible = false;
                }
            }
            if (asd.action == "alert")
            {
                List<string> list = alertlist();
                if (list != null)
                {
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        Response.Write("<script>alert('修改成功！')</script>");
                        btnsave.Visible = false;
                        btnsubmit.Visible = true;
                    }
                    catch
                    {
                        Response.Write("<script>alert('alertlist数据操作错误，请联系管理员！')</script>");
                    }
                }
            }
        }

        //提交
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            if (asd.action == "add")
            {
                string sqlText = "update OM_SDFY set state='1' where spbh='" + txtspbh.Text + "'and state='0' ";
                list.Add(sqlText);
                string sqlText1 = "update OM_SDFYSP set state='1' where spbh='" + txtspbh.Text + "'and state='0' ";
                list.Add(sqlText1);
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                    //邮件提醒
                    string _emailto = DBCallCommon.GetEmailAddressByUserID(hid_shrid.Value);
                    string _body = "住宿水电费审批任务:"
                          + "\r\n制单人：" + txtcreatename.Text.Trim()
                          + "\r\n制单日期：" + txtcreattime.Text.Trim();

                    string _subject = "您有新的【住宿水电费】需要审批，请及时处理";
                    DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                    Response.Write("<script>window.close()</script>");
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交审批出现问题，请联系管理员！');", true);
                }
            }
            if (asd.action == "alert")
            {
                string sqlText = "update OM_SDFY set state='1' where spbh='" + txtspbh.Text + "' ";
                list.Add(sqlText);
                string sqlText1 = "update OM_SDFYSP set state='1' where spbh='" + txtspbh.Text + "' ";
                list.Add(sqlText1);
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                    Response.Redirect("OM_SHUIDFSP.aspx");
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交审批出现问题，请联系管理员！');", true);
                }
            }
            if (asd.action == "check")
            {
                if (rblShyj.SelectedValue != "1" && rblShyj.SelectedValue != "2")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选同意或不同意！');", true);
                    return;
                }
                List<string> list1 = checklist();
                if (list1 != null)
                {
                    try
                    {
                        DBCallCommon.ExecuteTrans(list1);
                        Response.Redirect("OM_SHUIDFSP.aspx");
                    }
                    catch
                    {
                        Response.Write("<script>alert('checklist数据操作错误，请与管理员联系！')</script>");
                        return;
                    }
                }
            }
        }
        protected void btnback_Click(object sender, EventArgs e)
        {
            if (asd.action == "add")
                Response.Write("<script>window.close()</script>");
            else
                Response.Redirect("OM_SHUIDFSP.aspx");
        }
        private void contrlkjx()
        {
            if (asd.action == "add" || asd.action == "alert")
            {
                txtdairumonth.Disabled = false;
                lbtishi.Visible = true;
                panDetail.Enabled = true;
                panShenhe.Enabled = true;
                hlSelect.Visible = true;
                txtShnote.Enabled = false;
                rblShyj.Enabled = false;
                btnsave.Visible = true;
            }
            if (asd.action == "read")
            {
                btnback.Visible = false;
            }
            if (asd.action == "check")
            {
                panShenhe.Enabled = true;
                txt_Shr.Enabled = false;
                btnsubmit.Visible = true;
            }
        }
        private List<string> addlist()
        {
            List<string> list = new List<string>();
            string sqlText = "update OM_SDFY set spbh='" + txtspbh.Text.ToString() + "',dairumonth='" + txtdairumonth.Value.Trim() + "' where state='0' ";
            list.Add(sqlText);
            string sqlText1 = "insert into OM_SDFYSP (spbh,creattime,creatstname,creatstid,shrname,shrid) values('" + txtspbh.Text.ToString() + "','" + txtcreattime.Text.ToString() + "','" + asd.username + "','" + asd.userid + "','" + txt_Shr.Text.ToString() + "','" + hid_shrid.Value.Trim() + "') ";
            list.Add(sqlText1);
            return list;
        }
        private List<string> alertlist()
        {
            List<string> list = new List<string>();
            string sql = "update OM_SDFY set dairumonth='" + txtdairumonth.Value.Trim() + "' where spbh='" + txtspbh.Text + "'";
            list.Add(sql);
            string sql1 = "update OM_SDFYSP set  creattime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "', shrname='" + txt_Shr.Text.ToString() + "',shrid='" + hid_shrid.Value.Trim() + "' where spbh='" + txtspbh.Text + "'";
            list.Add(sql1);
            return list;
        }
        private List<string> checklist()
        {
            List<string> list = new List<string>();
            string state = "";//审批状态0-初始化，1-待审批,2-已通过,3-已驳回
            string shTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (rblShyj.SelectedValue == "1")
            {
                state = "2";
            }
            if (rblShyj.SelectedValue == "2")
            {
                state = "3";
            }
            string sql = "update OM_SDFYSP set state='" + state + "',shjl='" + rblShyj.SelectedValue.ToString() + "',shtime='" + shTime + "',shnote='" + txtShnote.Text.ToString() + "' where spbh='" + txtspbh.Text + "'and state='1'";
            list.Add(sql);
            string sql1 = "update OM_SDFY set state='" + state + "' where spbh='" + txtspbh.Text + "'and state='1'";
            list.Add(sql1);
            return list;
        }
        private string StrWhere()
        {
            string strWhere = "state='0' and spbh is NULL";
            if (asd.action != "add")
            {
                strWhere = " spbh='" + asd.spbh + "'";
            }
            return strWhere;
        }

        //合并单元格
        private void danyuangehebing()
        {
            for (int i = rptsushe.Items.Count - 1; i > 0; i--)
            {
                HtmlTableCell oCell_previous0 = rptsushe.Items[i - 1].FindControl("td_IDSDF") as HtmlTableCell;
                HtmlTableCell oCell0 = rptsushe.Items[i].FindControl("td_IDSDF") as HtmlTableCell;

                HtmlTableCell oCell_previous1 = rptsushe.Items[i - 1].FindControl("td_ssnum") as HtmlTableCell;
                HtmlTableCell oCell1 = rptsushe.Items[i].FindControl("td_ssnum") as HtmlTableCell;

                HtmlTableCell oCell_previous2 = rptsushe.Items[i - 1].FindControl("td_startdate") as HtmlTableCell;
                HtmlTableCell oCell2 = rptsushe.Items[i].FindControl("td_startdate") as HtmlTableCell;

                HtmlTableCell oCell_previous3 = rptsushe.Items[i - 1].FindControl("td_enddate") as HtmlTableCell;
                HtmlTableCell oCell3 = rptsushe.Items[i].FindControl("td_enddate") as HtmlTableCell;

                HtmlTableCell oCell_previous4 = rptsushe.Items[i - 1].FindControl("td_dianfje") as HtmlTableCell;
                HtmlTableCell oCell4 = rptsushe.Items[i].FindControl("td_dianfje") as HtmlTableCell;

                HtmlTableCell oCell_previous5 = rptsushe.Items[i - 1].FindControl("td_shuifje") as HtmlTableCell;
                HtmlTableCell oCell5 = rptsushe.Items[i].FindControl("td_shuifje") as HtmlTableCell;

                HtmlTableCell oCell_previous6 = rptsushe.Items[i - 1].FindControl("td_renjunfy") as HtmlTableCell;
                HtmlTableCell oCell6 = rptsushe.Items[i].FindControl("td_renjunfy") as HtmlTableCell;

                oCell0.RowSpan = (oCell0.RowSpan == -1) ? 1 : oCell0.RowSpan;
                oCell_previous0.RowSpan = (oCell_previous0.RowSpan == -1) ? 1 : oCell_previous0.RowSpan;

                oCell1.RowSpan = (oCell1.RowSpan == -1) ? 1 : oCell1.RowSpan;
                oCell_previous1.RowSpan = (oCell_previous1.RowSpan == -1) ? 1 : oCell_previous1.RowSpan;

                oCell2.RowSpan = (oCell2.RowSpan == -1) ? 1 : oCell2.RowSpan;
                oCell_previous2.RowSpan = (oCell_previous2.RowSpan == -1) ? 1 : oCell_previous2.RowSpan;

                oCell3.RowSpan = (oCell3.RowSpan == -1) ? 1 : oCell3.RowSpan;
                oCell_previous3.RowSpan = (oCell_previous3.RowSpan == -1) ? 1 : oCell_previous3.RowSpan;

                oCell4.RowSpan = (oCell4.RowSpan == -1) ? 1 : oCell4.RowSpan;
                oCell_previous4.RowSpan = (oCell_previous4.RowSpan == -1) ? 1 : oCell_previous4.RowSpan;

                oCell5.RowSpan = (oCell5.RowSpan == -1) ? 1 : oCell5.RowSpan;
                oCell_previous5.RowSpan = (oCell_previous5.RowSpan == -1) ? 1 : oCell_previous5.RowSpan;

                oCell6.RowSpan = (oCell6.RowSpan == -1) ? 1 : oCell6.RowSpan;
                oCell_previous6.RowSpan = (oCell_previous6.RowSpan == -1) ? 1 : oCell_previous6.RowSpan;

                if (oCell0.InnerText == oCell_previous0.InnerText)
                {
                    oCell1.Visible = false;
                    oCell_previous1.RowSpan += oCell1.RowSpan;

                    oCell2.Visible = false;
                    oCell_previous2.RowSpan += oCell2.RowSpan;

                    oCell3.Visible = false;
                    oCell_previous3.RowSpan += oCell3.RowSpan;

                    oCell4.Visible = false;
                    oCell_previous4.RowSpan += oCell4.RowSpan;

                    oCell5.Visible = false;
                    oCell_previous5.RowSpan += oCell5.RowSpan;

                    oCell6.Visible = false;
                    oCell_previous6.RowSpan += oCell6.RowSpan;
                }
            }
        }

        protected void rpt_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                if (txtspbh.Text != "")
                {
                    string monthstart = txtspbh.Text.ToString().Substring(8, 2);
                    string monthend = txtspbh.Text.ToString().Substring(12, 2);
                    string sqltext = "select sum(cast(((enddf-stratdf-gscddl)*pricedf/ssrens) as decimal(12,4))) as dfhj,sum(cast(((endsf-startsf-gscdsl)*pricesf/ssrens) as decimal(12,4))) as sfhj,sum(cast((((enddf-stratdf-gscddl)*pricedf)/ssrens+((endsf-startsf-gscdsl)*pricesf)/ssrens) as decimal(12,4))) as rjhj,sum(realmoney) as realhj from (select IDsdmx,IDSDF,ssrens,ssnum,OM_SDFY.startdate,OM_SDFYSP.spbh,OM_SDFY.state,OM_SDFY.enddate,stratdf,enddf,pricedf,startsf,endsf,pricesf,ST_NAME,note,peopleid,fangjnum,gscddl,gscdsl,realmoney from OM_SDFY left join OM_SDFdetail on (OM_SDFY.ssnum=OM_SDFdetail.fangjnum and OM_SDFY.startdate=OM_SDFdetail.startdate and OM_SDFY.enddate=OM_SDFdetail.enddate)left join OM_SDFYSP on OM_SDFY.spbh=OM_SDFY.spbh left join TBDS_STAFFINFO on OM_SDFdetail.peopleid=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t where startdate LIKE '%-" + monthstart + "-%' and enddate LIKE '%-" + monthend + "-%'and " + StrWhere();
                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (dt.Rows.Count > 0)
                    {
                        System.Web.UI.WebControls.Label lb_hjdf = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_hjdf");
                        System.Web.UI.WebControls.Label lb_hjsf = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_hjsf");
                        System.Web.UI.WebControls.Label lb_rjhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_rjhj");
                        System.Web.UI.WebControls.Label lb_realhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_realhj");

                        lb_hjdf.Text = (Math.Round(CommonFun.ComTryDecimal(dt.Rows[0]["dfhj"].ToString().Trim()), 2)).ToString().Trim();
                        lb_hjsf.Text = (Math.Round(CommonFun.ComTryDecimal(dt.Rows[0]["sfhj"].ToString().Trim()), 2)).ToString().Trim();
                        lb_rjhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt.Rows[0]["rjhj"].ToString().Trim()), 2)).ToString().Trim();
                        lb_realhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt.Rows[0]["realhj"].ToString().Trim()), 2)).ToString().Trim();
                    }
                }
            }
        }
    }
}
