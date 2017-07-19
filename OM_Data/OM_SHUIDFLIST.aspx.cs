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
    public partial class OM_SHUIDFLIST : BasicPage
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UCPaging1.CurrentPage = 1;
                InitVar();
                bindrpt();
                danyuangehebing();
            }
            CheckUser(ControlFinder);
            InitVar();
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
            pager_org.TableName = "(select IDsdmx,IDSDF,ssrens,ssnum,OM_SDFY.startdate,OM_SDFY.enddate,stratdf,enddf,pricedf,startsf,endsf,pricesf,ST_NAME,note,peopleid,fangjnum,gscddl,gscdsl,realmoney from OM_SDFY left join OM_SDFdetail on (OM_SDFY.ssnum=OM_SDFdetail.fangjnum and OM_SDFY.startdate=OM_SDFdetail.startdate and OM_SDFY.enddate=OM_SDFdetail.enddate) left join TBDS_STAFFINFO on OM_SDFdetail.peopleid=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t";
            pager_org.PrimaryKey = "IDsdmx";
            pager_org.ShowFields = "IDsdmx,IDSDF,ssrens,ssnum,startdate,enddate,stratdf,enddf,cast((enddf-stratdf-gscddl) as decimal(12,2)) as shiydl,pricedf,cast(((enddf-stratdf-gscddl)*pricedf) as decimal(12,2)) as dianfje,startsf,endsf,cast((endsf-startsf-gscdsl) as decimal(12,2)) as shiysl,pricesf,cast(((endsf-startsf-gscdsl)*pricesf) as decimal(12,2)) as shuifje,ST_NAME,cast((((enddf-stratdf-gscddl)*pricedf)/ssrens+((endsf-startsf-gscdsl)*pricesf)/ssrens) as decimal(12,2)) as renjunfy,realmoney,note,gscddl,gscdsl";
            pager_org.OrderField = "startdate,enddate,ssnum";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;
            pager_org.PageSize = 30;
        }


        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string sql = "1=1";
            if (drp_state.SelectedIndex != 0)
            {
                sql += " and ssnum like '" + drp_state.SelectedValue.ToString().Trim() + "%'";
            }
            if (tbfjnum.Text.Trim() != "")
            {
                sql += " and ssnum like '%" + tbfjnum.Text.Trim() + "%'";
            }
            if (tbname.Text.Trim() != "")
            {
                sql += " and ST_NAME like '%" + tbname.Text.Trim() + "%'";
            }
            return sql;
        }

        /// <summary>
        /// 换页事件
        /// </summary>
        private void Pager_PageChanged(int pageNumber)
        {
            bindrpt();
            danyuangehebing();
        }


        private void bindrpt()
        {
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
        }


        protected void drp_state_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
            danyuangehebing();
        }


        protected void btncx_click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
            danyuangehebing();
        }

        //提交审批
        protected void btnsp_click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string sql = "select * from OM_SDFY where spbh!=null and state='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                Response.Write("<script>alert('审批已提交，请勿重复添加！')</script>");
            }
            else
            {
                string sql1 = "select startdate,enddate,state from OM_SDFY where state='0'";
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
                string creattime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (dt1.Rows.Count > 0)
                {
                    string[] a = dt1.Rows[0]["startdate"].ToString().Split('-');
                    string[] b = a[0].Split('0');
                    string[] c = dt1.Rows[0]["enddate"].ToString().Split('-');
                    string[] d = c[0].Split('0');
                    string spbh = "ZSSDF-" + b[1] + a[1] + d[1] + c[1];

                    string sqlText = "update OM_SDFY set spbh='" + spbh + "',state='1' where state='0' ";
                    list.Add(sqlText);
                    string sqlText1 = "insert into OM_SDFYSP (spbh,state,creattime,creatstname,creatstid) values('" + spbh + "','1','" + creattime + "','" + Session["UserName"].ToString() + "','" + Session["UserID"].ToString() + "') ";
                    list.Add(sqlText1);
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        Response.Write("<script>alert('提交成功！')</script>");
                    }
                    catch
                    {
                        Response.Write("<script>alert('数据操作失败，请联系管理员！')</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('没有待审批的水电费数据！')</script>");
                }
            }
        }
        //保存
        protected void btnsave_click(object sender, EventArgs e)
        {
            List<string> sql_list = new List<string>();
            int k = 0;//记录选中的行数
            for (int j = 0; j < rptsushe.Items.Count; j++)
            {
                if (((System.Web.UI.WebControls.CheckBox)rptsushe.Items[j].FindControl("CKBOX_SELECT")).Checked)
                {
                    k++;
                }
            }
            if (k > 0)
            {
                for (int i = 0; i < rptsushe.Items.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)rptsushe.Items[i].FindControl("CKBOX_SELECT")).Checked)
                    {
                        string IDsdmx = ((System.Web.UI.WebControls.Label)rptsushe.Items[i].FindControl("IDsdmx")).Text.Trim();//员工id
                        string strrealmoney = ((System.Web.UI.WebControls.TextBox)rptsushe.Items[i].FindControl("txt_realmoney")).Text.Trim();//工号
                        string sqlupdate = "update OM_SDFdetail set realmoney=" + CommonFun.ComTryDecimal(strrealmoney) + " where IDsdmx=" + CommonFun.ComTryInt(IDsdmx) + "";
                        sql_list.Add(sqlupdate); 
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要修改的数据！！！');", true);
                return;
            }

            //更新
            DBCallCommon.ExecuteTrans(sql_list);
            UCPaging1.CurrentPage = 1;
            bindrpt();
            danyuangehebing();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据已保存！！！');", true);
        }
        //删除
        protected void btnfjh_click(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            int num = 0;
            foreach (RepeaterItem rptitem in rptsushe.Items)
            {
                System.Web.UI.WebControls.CheckBox CKBOX_SELECT = (System.Web.UI.WebControls.CheckBox)rptitem.FindControl("CKBOX_SELECT");
                if (CKBOX_SELECT.Checked == true)
                {
                    num++;
                }
            }
            if (num == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要删除的数据！');", true);
                return;
            }
            foreach (RepeaterItem rptitem in rptsushe.Items)
            {
                System.Web.UI.WebControls.CheckBox CKBOX_SELECT = (System.Web.UI.WebControls.CheckBox)rptitem.FindControl("CKBOX_SELECT");
                System.Web.UI.WebControls.Label IDSDF = (System.Web.UI.WebControls.Label)rptitem.FindControl("IDSDF");
                System.Web.UI.WebControls.Label ssnum = (System.Web.UI.WebControls.Label)rptitem.FindControl("ssnum");
                System.Web.UI.WebControls.Label startdate = (System.Web.UI.WebControls.Label)rptitem.FindControl("startdate");
                System.Web.UI.WebControls.Label enddate = (System.Web.UI.WebControls.Label)rptitem.FindControl("enddate");
                if (CKBOX_SELECT.Checked == true)
                {
                    string sqldelete1 = "delete from OM_SDFY where IDSDF='" + IDSDF.Text.Trim() + "'";
                    string sqldelete2 = "delete from OM_SDFdetail where fangjnum='" + ssnum.Text.Trim() + "' and startdate='" + startdate.Text.Trim() + "' and enddate='" + enddate.Text.Trim() + "'";
                    sqltextlist.Add(sqldelete1);
                    sqltextlist.Add(sqldelete2);
                }
            }
            DBCallCommon.ExecuteTrans(sqltextlist);
            UCPaging1.CurrentPage = 1;
            bindrpt();
            danyuangehebing();
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

                HtmlTableCell oCell_previous4 = rptsushe.Items[i - 1].FindControl("td_stratdf") as HtmlTableCell;
                HtmlTableCell oCell4 = rptsushe.Items[i].FindControl("td_stratdf") as HtmlTableCell;

                HtmlTableCell oCell_previous5 = rptsushe.Items[i - 1].FindControl("td_enddf") as HtmlTableCell;
                HtmlTableCell oCell5 = rptsushe.Items[i].FindControl("td_enddf") as HtmlTableCell;

                HtmlTableCell oCell_previous6 = rptsushe.Items[i - 1].FindControl("td_shiydl") as HtmlTableCell;
                HtmlTableCell oCell6 = rptsushe.Items[i].FindControl("td_shiydl") as HtmlTableCell;

                HtmlTableCell oCell_previous7 = rptsushe.Items[i - 1].FindControl("td_pricedf") as HtmlTableCell;
                HtmlTableCell oCell7 = rptsushe.Items[i].FindControl("td_pricedf") as HtmlTableCell;

                HtmlTableCell oCell_previous8 = rptsushe.Items[i - 1].FindControl("td_dianfje") as HtmlTableCell;
                HtmlTableCell oCell8 = rptsushe.Items[i].FindControl("td_dianfje") as HtmlTableCell;

                HtmlTableCell oCell_previous9 = rptsushe.Items[i - 1].FindControl("td_startsf") as HtmlTableCell;
                HtmlTableCell oCell9 = rptsushe.Items[i].FindControl("td_startsf") as HtmlTableCell;

                HtmlTableCell oCell_previous10 = rptsushe.Items[i - 1].FindControl("td_endsf") as HtmlTableCell;
                HtmlTableCell oCell10 = rptsushe.Items[i].FindControl("td_endsf") as HtmlTableCell;

                HtmlTableCell oCell_previous11 = rptsushe.Items[i - 1].FindControl("td_shiysl") as HtmlTableCell;
                HtmlTableCell oCell11 = rptsushe.Items[i].FindControl("td_shiysl") as HtmlTableCell;

                HtmlTableCell oCell_previous12 = rptsushe.Items[i - 1].FindControl("td_pricesf") as HtmlTableCell;
                HtmlTableCell oCell12 = rptsushe.Items[i].FindControl("td_pricesf") as HtmlTableCell;

                HtmlTableCell oCell_previous13 = rptsushe.Items[i - 1].FindControl("td_shuifje") as HtmlTableCell;
                HtmlTableCell oCell13 = rptsushe.Items[i].FindControl("td_shuifje") as HtmlTableCell;

                HtmlTableCell oCell_previous14 = rptsushe.Items[i - 1].FindControl("td_renjunfy") as HtmlTableCell;
                HtmlTableCell oCell14 = rptsushe.Items[i].FindControl("td_renjunfy") as HtmlTableCell;

                HtmlTableCell oCell_previous15 = rptsushe.Items[i - 1].FindControl("td_edit") as HtmlTableCell;
                HtmlTableCell oCell15 = rptsushe.Items[i].FindControl("td_edit") as HtmlTableCell;

                HtmlTableCell oCell_previous16 = rptsushe.Items[i - 1].FindControl("td_note") as HtmlTableCell;
                HtmlTableCell oCell16 = rptsushe.Items[i].FindControl("td_note") as HtmlTableCell;

                HtmlTableCell oCell_previous17 = rptsushe.Items[i - 1].FindControl("td_gscddl") as HtmlTableCell;
                HtmlTableCell oCell17 = rptsushe.Items[i].FindControl("td_gscddl") as HtmlTableCell;

                HtmlTableCell oCell_previous18 = rptsushe.Items[i - 1].FindControl("td_gscdsl") as HtmlTableCell;
                HtmlTableCell oCell18 = rptsushe.Items[i].FindControl("td_gscdsl") as HtmlTableCell;

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

                oCell7.RowSpan = (oCell7.RowSpan == -1) ? 1 : oCell7.RowSpan;
                oCell_previous7.RowSpan = (oCell_previous7.RowSpan == -1) ? 1 : oCell_previous7.RowSpan;

                oCell8.RowSpan = (oCell8.RowSpan == -1) ? 1 : oCell8.RowSpan;
                oCell_previous8.RowSpan = (oCell_previous8.RowSpan == -1) ? 1 : oCell_previous8.RowSpan;

                oCell9.RowSpan = (oCell9.RowSpan == -1) ? 1 : oCell9.RowSpan;
                oCell_previous9.RowSpan = (oCell_previous9.RowSpan == -1) ? 1 : oCell_previous9.RowSpan;

                oCell10.RowSpan = (oCell10.RowSpan == -1) ? 1 : oCell10.RowSpan;
                oCell_previous10.RowSpan = (oCell_previous10.RowSpan == -1) ? 1 : oCell_previous10.RowSpan;

                oCell11.RowSpan = (oCell11.RowSpan == -1) ? 1 : oCell11.RowSpan;
                oCell_previous11.RowSpan = (oCell_previous11.RowSpan == -1) ? 1 : oCell_previous11.RowSpan;

                oCell12.RowSpan = (oCell12.RowSpan == -1) ? 1 : oCell12.RowSpan;
                oCell_previous12.RowSpan = (oCell_previous12.RowSpan == -1) ? 1 : oCell_previous12.RowSpan;

                oCell13.RowSpan = (oCell13.RowSpan == -1) ? 1 : oCell13.RowSpan;
                oCell_previous13.RowSpan = (oCell_previous13.RowSpan == -1) ? 1 : oCell_previous13.RowSpan;

                oCell14.RowSpan = (oCell14.RowSpan == -1) ? 1 : oCell14.RowSpan;
                oCell_previous14.RowSpan = (oCell_previous14.RowSpan == -1) ? 1 : oCell_previous14.RowSpan;

                oCell15.RowSpan = (oCell15.RowSpan == -1) ? 1 : oCell15.RowSpan;
                oCell_previous15.RowSpan = (oCell_previous15.RowSpan == -1) ? 1 : oCell_previous15.RowSpan;

                oCell16.RowSpan = (oCell16.RowSpan == -1) ? 1 : oCell16.RowSpan;
                oCell_previous16.RowSpan = (oCell_previous16.RowSpan == -1) ? 1 : oCell_previous16.RowSpan;

                oCell17.RowSpan = (oCell17.RowSpan == -1) ? 1 : oCell17.RowSpan;
                oCell_previous17.RowSpan = (oCell_previous17.RowSpan == -1) ? 1 : oCell_previous17.RowSpan;

                oCell18.RowSpan = (oCell18.RowSpan == -1) ? 1 : oCell18.RowSpan;
                oCell_previous18.RowSpan = (oCell_previous18.RowSpan == -1) ? 1 : oCell_previous18.RowSpan;


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

                    oCell7.Visible = false;
                    oCell_previous7.RowSpan += oCell7.RowSpan;

                    oCell8.Visible = false;
                    oCell_previous8.RowSpan += oCell8.RowSpan;

                    oCell9.Visible = false;
                    oCell_previous9.RowSpan += oCell9.RowSpan;

                    oCell10.Visible = false;
                    oCell_previous10.RowSpan += oCell10.RowSpan;

                    oCell11.Visible = false;
                    oCell_previous11.RowSpan += oCell11.RowSpan;

                    oCell12.Visible = false;
                    oCell_previous12.RowSpan += oCell12.RowSpan;

                    oCell13.Visible = false;
                    oCell_previous13.RowSpan += oCell13.RowSpan;

                    oCell14.Visible = false;
                    oCell_previous14.RowSpan += oCell14.RowSpan;

                    oCell15.Visible = false;
                    oCell_previous15.RowSpan += oCell15.RowSpan;

                    oCell16.Visible = false;
                    oCell_previous16.RowSpan += oCell16.RowSpan;

                    oCell17.Visible = false;
                    oCell_previous17.RowSpan += oCell17.RowSpan;

                    oCell18.Visible = false;
                    oCell_previous18.RowSpan += oCell18.RowSpan;
                }
            }
        }

        protected void rpt_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqltext = "select sum(cast(((enddf-stratdf-gscddl)*pricedf/ssrens) as decimal(12,4))) as dfhj,sum(cast(((endsf-startsf-gscdsl)*pricesf/ssrens) as decimal(12,4))) as sfhj,sum(cast((((enddf-stratdf-gscddl)*pricedf)/ssrens+((endsf-startsf-gscdsl)*pricesf)/ssrens) as decimal(12,4))) as rjhj,sum(realmoney) as realhj,sum(cast((gscddl/ssrens) as decimal(12,4))) as gscddlhj,sum(cast(((enddf-stratdf-gscddl)/ssrens) as decimal(12,4))) as grdlhj,sum(cast((gscdsl/ssrens) as decimal(12,4))) as gscdslhj,sum(cast(((endsf-startsf-gscdsl)/ssrens) as decimal(12,4))) as grslhj from (select IDsdmx,IDSDF,ssrens,ssnum,OM_SDFY.startdate,OM_SDFY.enddate,stratdf,enddf,pricedf,startsf,endsf,pricesf,ST_NAME,note,peopleid,fangjnum,gscddl,gscdsl,realmoney from OM_SDFY left join OM_SDFdetail on (OM_SDFY.ssnum=OM_SDFdetail.fangjnum and OM_SDFY.startdate=OM_SDFdetail.startdate and OM_SDFY.enddate=OM_SDFdetail.enddate) left join TBDS_STAFFINFO on OM_SDFdetail.peopleid=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t where " + StrWhere();
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    System.Web.UI.WebControls.Label lb_hjdf = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_hjdf");
                    System.Web.UI.WebControls.Label lb_hjsf = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_hjsf");
                    System.Web.UI.WebControls.Label lb_rjhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_rjhj");
                    System.Web.UI.WebControls.Label lb_realhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_realhj");

                    System.Web.UI.WebControls.Label lb_gsdlhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_gsdlhj");
                    System.Web.UI.WebControls.Label lb_grdlhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_grdlhj");
                    System.Web.UI.WebControls.Label lb_gsslhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_gsslhj");
                    System.Web.UI.WebControls.Label lb_grslhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_grslhj");

                    lb_hjdf.Text = (Math.Round(CommonFun.ComTryDecimal(dt.Rows[0]["dfhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_hjsf.Text = (Math.Round(CommonFun.ComTryDecimal(dt.Rows[0]["sfhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_rjhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt.Rows[0]["rjhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_realhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt.Rows[0]["realhj"].ToString().Trim()),2)).ToString().Trim();

                    lb_gsdlhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt.Rows[0]["gscddlhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_grdlhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt.Rows[0]["grdlhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_gsslhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt.Rows[0]["gscdslhj"].ToString().Trim()),2)).ToString().Trim();
                    lb_grslhj.Text = (Math.Round(CommonFun.ComTryDecimal(dt.Rows[0]["grslhj"].ToString().Trim()), 2)).ToString().Trim();
                }
            }
        }
    }
}
