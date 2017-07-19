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
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Mar_Statistics : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                bindcaigy();
                GetBoundData();
            }
            CheckUser(ControlFinder);
        }
        #region 分页
        PagerQueryParam pager = new PagerQueryParam();
        private void InitPager()
        {
            pager.TableName = "(select a.*,b.RESULT,d.* from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a left join (select PTC,RESULT,ISAGAIN,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc) as rn from View_TBQM_APLYFORITEM) as c where rn<=1) as b on a.ptcode=b.PTC left join (select * from (select m.OP_TSAID,n.OP_PTCODE,n.OP_ID,n.OP_REALNUM,n.OP_UPRICE,n.OP_AMOUNT from TBWS_OUT as m left join TBWS_OUTDETAIL as n on m.OP_CODE=n.OP_CODE)q) as d on a.ptcode=d.OP_PTCODE)t";
            pager.PrimaryKey = "";
            pager.ShowFields = "orderno,supplierid,suppliernm,zdtime,cgtimerq,pjid,pjnm,engid,engnm,whstate,convert(float,zxnum) as zxnum,recgdnum,detailnote,detailstate,detailcstate,marunit,convert(float,ctamount) as ctamount," +
                         "zdrid,zdrnm,shrid,shrnm,totalnote,totalstate,totalcstate,ptcode,OP_TSAID,OP_PTCODE,OP_ID,OP_REALNUM,OP_UPRICE,cast(((isnull(OP_UPRICE,0))*1.17) as decimal(12,4)) as OP_HSUPRICE,OP_AMOUNT,cast(((isnull(OP_AMOUNT,0))*1.17) as decimal(12,4)) as OP_HSAMOUNT,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,marid,marnm,margg,marcz,margb,PO_MASHAPE,length,width,PO_ZJE,RESULT as PO_CGFS,ctprice,recdate,PO_MAP,PO_TECUNIT,fznum,PO_CHILDENGNAME ";
            pager.OrderField = "zdtime desc,ptcode,marnm,margg";
            pager.StrWhere = CreateConStr();
            pager.OrderType = 0;
            pager.PageSize = 100;
            UCPaging1.PageSize = pager.PageSize;
        }
        protected void GetBoundData()
        {
            InitPager();
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, Purordertotal_list_Repeater, UCPaging1, NoDataPane);
            if (NoDataPane.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }


        private void bindcaigy()
        {
            string sqltext = "";
            sqltext = "select distinct b.ST_NAME,b.ST_ID from TBPC_PURCHASEPLAN as a left join TBDS_STAFFINFO as b on a.PUR_CGMAN=b.ST_ID WHERE a.PUR_CGMAN<>'' and b.ST_PD='0'";
            string dataText = "ST_NAME";
            string dataValue = "ST_ID";
            DBCallCommon.BindDdl(DropDownListcgy, sqltext, dataText, dataValue);
            DropDownListcgy.SelectedIndex = 0;
        }
        //查询条件
        private string CreateConStr()
        {
            string sqlwhere = "1=1 ";
            string sqltext = "";
            string sqltextck = "";
            sqltext = "SELECT orderno,supplierid,suppliernm,zdtime,cgtimerq,pjid,pjnm,engid,engnm,whstate,convert(float,zxnum) as zxnum,recgdnum,detailnote,detailstate,detailcstate,marunit,convert(float,ctamount) as ctamount," +
                         "zdrid,zdrnm,shrid,shrnm,totalnote,totalstate,totalcstate,ptcode,OP_TSAID,OP_PTCODE,OP_ID,OP_REALNUM,OP_UPRICE,cast(((isnull(OP_UPRICE,0))*1.17) as decimal(12,2)) as OP_HSUPRICE,OP_AMOUNT,cast(((isnull(OP_AMOUNT,0))*1.17) as decimal(12,2)) as OP_HSAMOUNT,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,marid,marnm,margg,marcz,margb,PO_MASHAPE,length,width,PO_ZJE,PO_CGFS,ctprice,recdate  " +
                         " FROM (select a.*,b.RESULT,d.* from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a left join (select PTC,RESULT,ISAGAIN,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc) as rn from View_TBQM_APLYFORITEM) as c where rn<=1) as b on a.ptcode=b.PTC left join (select * from (select m.OP_TSAID,n.OP_PTCODE,n.OP_ID,n.OP_REALNUM,n.OP_UPRICE,n.OP_AMOUNT from TBWS_OUT as m left join TBWS_OUTDETAIL as n on m.OP_CODE=n.OP_CODE where OP_ID in(select min(OP_ID) from TBWS_OUTDETAIL group by OP_PTCODE))q) as d on a.ptcode=d.OP_PTCODE)t where ";
            sqltextck = "SELECT orderno,supplierid,suppliernm,zdtime,cgtimerq,pjid,pjnm,engid,engnm,whstate,convert(float,zxnum) as zxnum,recgdnum,detailnote,detailstate,detailcstate,marunit,convert(float,ctamount) as ctamount," +
                         "zdrid,zdrnm,shrid,shrnm,totalnote,totalstate,totalcstate,ptcode,OP_TSAID,OP_PTCODE,OP_ID,OP_REALNUM,OP_UPRICE,cast(((isnull(OP_UPRICE,0))*1.17) as decimal(12,2)) as OP_HSUPRICE,OP_AMOUNT,cast(((isnull(OP_AMOUNT,0))*1.17) as decimal(12,2)) as OP_HSAMOUNT,case when margb='' then PO_TUHAO else '' end as PO_TUHAO,marid,marnm,margg,marcz,margb,PO_MASHAPE,length,width,PO_ZJE,PO_CGFS,ctprice,recdate  " +
                         " FROM (select a.*,b.RESULT,d.* from View_TBPC_PURORDERDETAIL_PLAN_TOTAL as a left join (select PTC,RESULT,ISAGAIN,rn from (select *,row_number() over(partition by PTC order by ISAGAIN desc) as rn from View_TBQM_APLYFORITEM) as c where rn<=1) as b on a.ptcode=b.PTC left join (select * from (select m.OP_TSAID,n.OP_PTCODE,n.OP_ID,n.OP_REALNUM,n.OP_UPRICE,n.OP_AMOUNT from TBWS_OUT as m left join TBWS_OUTDETAIL as n on m.OP_CODE=n.OP_CODE)q) as d on a.ptcode=d.OP_PTCODE)t where ";
            if (tb_orderno.Text.ToString().Trim() != "")
            {
                sqlwhere = sqlwhere + " and orderno like '%" + tb_orderno.Text.Trim() + "%'";
            }
            if (tb_supply.Text.ToString().Trim() != "")
            {
                sqlwhere = sqlwhere + " and suppliernm like '%" + tb_supply.Text.Trim() + "%'";
            }
            if (tb_StartTime.Text.ToString().Trim() != "" && tb_EndTime.Text.ToString().Trim() != "")
            {
                sqlwhere = sqlwhere + " and zdtime>='" + tb_StartTime.Text.Trim() + "'";
                string enddate = tb_EndTime.Text.ToString() == "" ? "2100-01-01" : tb_EndTime.Text.ToString();
                enddate = enddate + " 23:59:59";
                sqlwhere = sqlwhere + " and zdtime<='" + enddate + "'";
            }
            if (tb_name.Text.ToString().Trim() != "")
            {
                sqlwhere = sqlwhere + " and marnm like '%" + tb_name.Text.Trim() + "%'";
            }
            if (tb_marid.Text.ToString().Trim() != "")
            {
                sqlwhere = sqlwhere + " and marid like '%" + tb_marid.Text.Trim() + "%'";
            }
            if (tb_ptc.Text.ToString().Trim() != "")
            {
                sqlwhere = sqlwhere + " and ptcode like '%" + tb_ptc.Text.Trim() + "%'";
            }
            if (tb_caizhi.Text.ToString().Trim() != "")
            {
                sqlwhere = sqlwhere + " and marcz like '%" + tb_caizhi.Text.Trim() + "%'";
            }
            if (tb_type.Text.Trim() != "")
            {
                sqlwhere = sqlwhere + " and PO_MASHAPE like '%" + tb_type.Text.Trim() + "%'";
            }
            if (tb_gg.Text.ToString().Trim() != "")
            {
                sqlwhere = sqlwhere + " and margg like '%" + tb_gg.Text.Trim() + "%'";
            }

            //多规格物料查询
            if (txt_multiguige.Text.Trim() != "")
            {
                string arrayguige = "('" + txt_multiguige.Text.Trim().Replace("/", "','") + "')";
                sqlwhere = sqlwhere + " and margg in" + arrayguige + "";
            }

            if (tb_outrwh.Text.Trim() != "")
            {
                sqlwhere = sqlwhere + " and OP_TSAID like '%" + tb_outrwh.Text.Trim() + "%'";
            }
            if (DropDownListckjg.SelectedValue == "0")
            {
                sqlwhere = sqlwhere + " and ptcode not like '%'+OP_TSAID+'%' and ptcode not like '%BEIKU%' and ptcode not like '%备库%' and OP_TSAID is not null and OP_TSAID!=''";
            }
            else if (DropDownListckjg.SelectedValue == "1")
            {
                sqlwhere = sqlwhere + " and ptcode like '%'+OP_TSAID+'%' and ptcode not like '%BEIKU%' and ptcode not like '%备库%' and OP_TSAID is not null and OP_TSAID!=''";
            }
            if (DropDownListcgy.SelectedIndex != 0)
            {
                sqlwhere = sqlwhere + " and zdrid='" + DropDownListcgy.SelectedValue.ToString().Trim() + "'";
            }
            string a = rbl_state.SelectedValue;
            if (DropDownList3.SelectedIndex != 0)
            {
                if (DropDownList3.SelectedItem.Text == "未报检")
                {
                    sqlwhere = sqlwhere + " and PO_CGFS= '——' and RESULT is null";
                }
                else if (DropDownList3.SelectedItem.Text == "免检")
                {
                    sqlwhere = sqlwhere + " and RESULT='免检'";
                }
                else
                {
                    sqlwhere = sqlwhere + " and PO_CGFS = '" + DropDownList3.SelectedItem.Text + "'";
                }
            }
            switch (a)
            {
                case "0":
                    sqlwhere += "";
                    break;
                case "1":
                    sqlwhere = sqlwhere + " and detailstate='1' and recgdnum=0 and detailcstate='0'";
                    break;
                case "2":
                    sqlwhere = sqlwhere + " and (detailstate='3' or detailstate='2') and detailcstate='0'";
                    break;
                default:
                    break;
            }
            sqltext = sqltext + sqlwhere + " order by orderno desc,ptcode asc";
            sqltextck = sqltextck + sqlwhere + " order by orderno desc,ptcode asc";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            System.Data.DataTable dtck = DBCallCommon.GetDTUsingSqlText(sqltextck);
            double tot_num = 0;
            double tot_money = 0;
            double tot_cknum = 0;
            double tot_ckmoney = 0;
            double tot_ckhsmoney = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string ctanum = dt.Rows[i]["zxnum"].ToString().Trim();
                string cta = dt.Rows[i]["ctamount"].ToString().Trim();
                
                ctanum = ctanum == "" ? "0" : ctanum;
                cta = cta == "" ? "0" : cta;
                
                tot_num += Convert.ToDouble(ctanum);
                tot_money += Convert.ToDouble(cta);
            }
            for (int j = 0; j < dtck.Rows.Count; j++)
            {
                string ckctanum = dtck.Rows[j]["OP_REALNUM"].ToString().Trim();
                string ckcta = dtck.Rows[j]["OP_AMOUNT"].ToString().Trim();
                string ckctahs = dtck.Rows[j]["OP_HSAMOUNT"].ToString().Trim();

                ckctanum = ckctanum == "" ? "0" : ckctanum;
                ckcta = ckcta == "" ? "0" : ckcta;
                ckctahs = ckctahs == "" ? "0" : ckctahs;

                tot_cknum += Convert.ToDouble(ckctanum);
                tot_ckmoney += Convert.ToDouble(ckcta);
                tot_ckhsmoney += Convert.ToDouble(ckctahs);
            }
            lb_select_num.Text = Convert.ToString(dt.Rows.Count);
            lb_select_sl.Text = string.Format("{0:c}", tot_num);
            lb_select_money.Text = string.Format("{0:c}", tot_money);
            if (tot_num > 0)
            {
                lbdduprice.Text = (tot_money / tot_num).ToString("0.00").Trim();
            }
            lb_ckslhj.Text = string.Format("{0:c}", tot_cknum);
            lb_ckjehj.Text = string.Format("{0:c}", tot_ckmoney);
            lb_ckhsjehj.Text = string.Format("{0:c}", tot_ckhsmoney);
            if (tot_cknum > 0)
            {
                lbckprice.Text = (tot_ckhsmoney / tot_cknum).ToString("0.00").Trim();
            }
            return sqlwhere;
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetBoundData();
        }
        #endregion
        public string get_ordlistsh_state(string i)
        {
            string statestr = "";
            if (Convert.ToInt32(i) >= 1)
            {
                statestr = "是";
            }
            else
            {

                statestr = "否";

            }
            return statestr;
        }
        public string get_zlbj(string i)
        {
            string statestr = "";
            if (i == "")
            {
                statestr = "未报检";
            }
            else
            {
                statestr = i;
            }
            return statestr;
        }
        protected void DropDownList3_click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetBoundData();
        }
        protected void btn_search_click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetBoundData();
        }
        protected void tb_supply_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (tb_supply.Text.ToString().Contains("|"))
            {
                Cname = tb_supply.Text.Substring(0, tb_supply.Text.ToString().IndexOf("|"));
                tb_supply.Text = Cname.Trim();
            }
            else if (tb_supply.Text == "")
            {

            }
        }
        protected void btnNameQuery_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetBoundData();
        }
        protected void DropDownListckjg_click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetBoundData();
        }
        protected void DropDownListcgy_click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetBoundData();
        }


        private double zxnum = 0;
        private double dnum = 0;
        private string state = "";
        private string date = "";
        private string baojian = "";
        List<string> list = new List<string>();
        protected void Purordertotal_list_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string zlbjjg = ((System.Web.UI.WebControls.Label)e.Item.FindControl("zlbj")).Text;//报检信息
                state = ((System.Web.UI.WebControls.Label)e.Item.FindControl("detailstate")).Text;
                zxnum = Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("zxnum")).Text);
                dnum = Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("recgdnum")).Text);
                date = ((System.Web.UI.WebControls.Label)e.Item.FindControl("cgtimerq")).Text == "" ? "2100-01-01" : ((System.Web.UI.WebControls.Label)e.Item.FindControl("cgtimerq")).Text;
                baojian = ((System.Web.UI.WebControls.Label)e.Item.FindControl("zlbj")).Text;
                string datime = DateTime.Now.ToString("yyyy-MM-dd");
                if (state == "2")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).Text = "已入库";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).Text = "已到货";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).ForeColor = System.Drawing.Color.Red;
                }
                else if (state == "3")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).Text = "已到货";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).ForeColor = System.Drawing.Color.Red;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).Text = "未入库";
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).ForeColor = System.Drawing.Color.Red;
                }
                else if (state == "1")
                {
                    if (dnum > 0 && dnum < zxnum)
                    {
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).Text = "部分入库";
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).ForeColor = System.Drawing.Color.Red;
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).Text = "部分到货";
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).ForeColor = System.Drawing.Color.Red;
                    }
                    else if (dnum == 0)
                    {
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).Text = "未到货";
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).ForeColor = System.Drawing.Color.Red;
                        if (zlbjjg != "未报检")
                        {
                            ((System.Web.UI.WebControls.Label)e.Item.FindControl("daohuoF")).Text = "已到货";
                        }
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).Text = "未入库";
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("rukuF")).ForeColor = System.Drawing.Color.Red;
                        HtmlTableCell td2 = (HtmlTableCell)e.Item.FindControl("td2");
                        if (Convert.ToDateTime(date) < Convert.ToDateTime(datime))
                        {
                            td2.BgColor = "Red";
                        }
                    }
                }
                string code = ((System.Web.UI.WebControls.HiddenField)e.Item.FindControl("PO_CODE")).Value;
                //查看合同信息
                //string sqltext = "select HT_ID,HT_XFHTBH,HT_DDBH from PC_CGHT where HT_DDBH like '%" + code + "%'";
                //System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                //if (dt.Rows.Count > 0)
                //{
                //    HtmlTableCell tdHT = (HtmlTableCell)e.Item.FindControl("tdHT");
                //    ((System.Web.UI.WebControls.Label)e.Item.FindControl("hetonghao")).Text = dt.Rows[0]["HT_XFHTBH"].ToString();
                //    tdHT.BgColor = "Green";
                //    ((System.Web.UI.WebControls.Label)e.Item.FindControl("hetonghao")).ForeColor = System.Drawing.Color.Red;
                //    ((HyperLink)e.Item.FindControl("Hyphth")).NavigateUrl = "~/PC_Data/PC_CGHT.aspx?action=read&id=" + dt.Rows[0]["HT_ID"].ToString() + "";
                //}
                //else
                //{
                //    string ptcodejc = ((System.Web.UI.WebControls.TextBox)e.Item.FindControl("txt_ptcode")).Text.ToString().Trim();
                //    string sqljc = "select * from (select * from TBPC_IQRCMPPRCRVW as m left join TBPC_IQRCMPPRICE as n on m.ICL_SHEETNO=n.PIC_SHEETNO where ICL_TYPE='1')s where PIC_PTCODE='" + ptcodejc + "'";
                //    System.Data.DataTable dtjc = DBCallCommon.GetDTUsingSqlText(sqljc);
                //    if (dtjc.Rows.Count > 0)
                //    {
                //        ((System.Web.UI.WebControls.Label)e.Item.FindControl("hetonghao")).Text = "集采";
                //    }
                //    else
                //    {
                //        ((System.Web.UI.WebControls.Label)e.Item.FindControl("hetonghao")).Text = "未添加";
                //    }
                //}

                //报检信息
                if (zlbjjg != "未报检")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("zlbj")).ForeColor = System.Drawing.Color.Red;
                    string ptcode = ((System.Web.UI.WebControls.Label)e.Item.FindControl("ptcode")).Text;
                    string sql = "select AFI_ID from TBQM_APLYFORINSPCT  where UNIQUEID=(select top 1 UNIQUEID from  TBQM_APLYFORITEM where PTC='" + ptcode + "' order by id desc)";
                    System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt1.Rows.Count > 0)
                    {
                        string afiid = dt1.Rows[0]["AFI_ID"].ToString();
                        ((HyperLink)e.Item.FindControl("Hypzlbj")).NavigateUrl = "~/QC_Data/QC_Inspection_Add.aspx?ACTION=UPDATE&NUM=1&ID=" + afiid + "";
                    }
                }

                System.Web.UI.WebControls.HiddenField lb1 = (System.Web.UI.WebControls.HiddenField)e.Item.FindControl("PO_CODE");
                System.Web.UI.WebControls.Label lb2 = (System.Web.UI.WebControls.Label)e.Item.FindControl("PO_ZDNM");
                System.Web.UI.WebControls.Label lb3 = (System.Web.UI.WebControls.Label)e.Item.FindControl("PO_SHTIME");
                System.Web.UI.WebControls.Label lb4 = (System.Web.UI.WebControls.Label)e.Item.FindControl("zje");
                System.Web.UI.WebControls.Label lb5 = (System.Web.UI.WebControls.Label)e.Item.FindControl("PO_SUPPLIERNM");
                if (list.Count == 0)
                {
                    list.Add(lb1.Value);
                }
                else
                {
                    if (list.Contains(lb1.Value))
                    {
                        lb1.Visible= false;
                        lb2.Visible = false;
                        lb3.Visible = false;
                        lb4.Visible = false;
                        lb5.Visible = false;
                    }
                    else
                    {
                        list.Add(lb1.Value);
                    }
                }

                if (e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "white";
                }
                else
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "#EFF3FB";
                }
            }
            if (e.Item.ItemType == ListItemType.Header)
            {

            }
        }
    }
}
