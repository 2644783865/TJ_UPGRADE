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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_KQlistsearch : System.Web.UI.Page
    {
        string flag = "";
        string stid = "";
        PagerQueryParam pager_org1 = new PagerQueryParam();
        PagerQueryParam pager_org2 = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            flag = Request.QueryString["flag"].ToString().Trim();
            if (!IsPostBack)
            {
                if (flag == "kqtotal")
                {
                    div_statistcs.Visible = true;
                }
                if (flag == "nianjia")
                {
                    divnianjia.Visible = true;
                }
                UCPaging1.CurrentPage = 1;
                InitVar1();
                bindrpt1();

                UCPaging2.CurrentPage = 1;
                InitVar2();
                bindrpt2();
            }
            InitVar1();
            InitVar2();
        }

        #region 历史考勤数据查询分页
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar1()
        {
            InitPager1();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged1);
            UCPaging1.PageSize = pager_org1.PageSize;    //每页显示的记录数

        }

        /// <summary>
        /// 分页初始化
        /// </summary>
        /// <param name="where"></param>
        private void InitPager1()
        {
            pager_org1.TableName = "View_OM_KQTJ";
            pager_org1.PrimaryKey = "id";
            pager_org1.ShowFields = "KQ_ST_ID,KQ_DATE,ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1,KQ_GNCC,KQ_GWCC,KQ_BINGJ,KQ_SHIJ,KQ_KUANGG,KQ_DAOXIU,KQ_CHANJIA,KQ_PEICHAN,KQ_HUNJIA,KQ_SANGJIA,KQ_GONGS,KQ_NIANX,KQ_BEIYONG1,KQ_BEIYONG2,KQ_BEIYONG3,KQ_BEIYONG4,KQ_BEIYONG5,KQ_BEIYONG6,KQ_QTJIA,KQ_JIEDIAO,KQ_ZMJBAN,KQ_JRJIAB,KQ_ZHIBAN,KQ_YEBAN,KQ_ZHONGB,KQ_CBTS,KQ_YSGZ,KQ_BEIZHU,KQ_XGTIME,KQ_CHUQIN";
            pager_org1.OrderField = "KQ_ST_ID";
            pager_org1.StrWhere = StrWhere1();
            pager_org1.OrderType = 0;//升序排列
            pager_org1.PageSize = 25;
        }
        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere1()
        {
            stid = Request.QueryString["stid"].ToString().Trim();
            string sql = "1=1 and KQ_ST_ID='" + stid + "'";
            if (radio_dangnian.Checked == true)
            {
                sql += " and KQ_DATE like '" + DateTime.Now.Year.ToString().Trim() + "%'";
            }
            return sql;
        }
        /// <summary>
        /// 换页事件
        /// </summary>
        private void Pager_PageChanged1(int pageNumber)
        {
            bindrpt1();
        }

        private void bindrpt1()
        {
            InitPager1();
            pager_org1.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt1 = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org1);
            CommonFun.Paging(dt1, rptKQTJ1, UCPaging1, palNoData1);
            if (palNoData1.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }


        protected void rptKQTJ1_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhjtotal = "select sum(KQ_GNCC) as KQ_GNCC,sum(KQ_GWCC) as KQ_GWCC,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_KUANGG) as KQ_KUANGG,sum(KQ_DAOXIU) as KQ_DAOXIU,sum(KQ_CHANJIA) as KQ_CHANJIA,sum(KQ_PEICHAN) as KQ_PEICHAN,sum(KQ_HUNJIA) as KQ_HUNJIA,sum(KQ_SANGJIA) as KQ_SANGJIA,sum(KQ_GONGS) as KQ_GONGS,sum(KQ_NIANX) as KQ_NIANX,sum(KQ_BEIYONG1) as KQ_BEIYONG1,sum(KQ_BEIYONG2) as KQ_BEIYONG2,sum(KQ_BEIYONG3) as KQ_BEIYONG3,sum(KQ_BEIYONG4) as KQ_BEIYONG4,sum(KQ_BEIYONG5) as KQ_BEIYONG5,sum(KQ_BEIYONG6) as KQ_BEIYONG6,sum(KQ_QTJIA) as KQ_QTJIA,sum(KQ_JIEDIAO) as KQ_JIEDIAO,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZHIBAN) as KQ_ZHIBAN,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_ZHONGB) as KQ_ZHONGB,sum(KQ_CBTS) as KQ_CBTS,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_CHUQIN) as KQ_CHUQIN from View_OM_KQTJ where " + StrWhere1() + "";
                System.Data.DataTable dthjtotal = DBCallCommon.GetDTUsingSqlText(sqlhjtotal);
                if (dthjtotal.Rows.Count > 0)
                {
                    System.Web.UI.WebControls.Label lbKQ_CHUQINhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_CHUQINhj");
                    System.Web.UI.WebControls.Label lbKQ_GNCChj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_GNCChj");
                    System.Web.UI.WebControls.Label lbKQ_GWCChj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_GWCChj");
                    System.Web.UI.WebControls.Label lbKQ_BINGJhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BINGJhj");
                    System.Web.UI.WebControls.Label lbKQ_SHIJhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_SHIJhj");
                    System.Web.UI.WebControls.Label lbKQ_KUANGGhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_KUANGGhj");
                    System.Web.UI.WebControls.Label lbKQ_DAOXIUhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_DAOXIUhj");
                    System.Web.UI.WebControls.Label lbKQ_CHANJIAhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_CHANJIAhj");
                    System.Web.UI.WebControls.Label lbKQ_PEICHANhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_PEICHANhj");
                    System.Web.UI.WebControls.Label lbKQ_HUNJIAhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_HUNJIAhj");
                    System.Web.UI.WebControls.Label lbKQ_SANGJIAhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_SANGJIAhj");
                    System.Web.UI.WebControls.Label lbKQ_GONGShj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_GONGShj");
                    System.Web.UI.WebControls.Label lbKQ_NIANXhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_NIANXhj");
                    System.Web.UI.WebControls.Label lbKQ_BEIYONG1hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG1hj");
                    System.Web.UI.WebControls.Label lbKQ_BEIYONG2hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG2hj");
                    System.Web.UI.WebControls.Label lbKQ_BEIYONG3hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG3hj");

                    System.Web.UI.WebControls.Label lbKQ_BEIYONG4hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG4hj");
                    System.Web.UI.WebControls.Label lbKQ_BEIYONG5hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG5hj");
                    System.Web.UI.WebControls.Label lbKQ_BEIYONG6hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG6hj");

                    System.Web.UI.WebControls.Label lbKQ_QTJIAhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_QTJIAhj");
                    System.Web.UI.WebControls.Label lbKQ_JIEDIAOhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_JIEDIAOhj");
                    System.Web.UI.WebControls.Label lbKQ_ZMJBANhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_ZMJBANhj");
                    System.Web.UI.WebControls.Label lbKQ_JRJIABhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_JRJIABhj");
                    System.Web.UI.WebControls.Label lbKQ_ZHIBANhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_ZHIBANhj");
                    System.Web.UI.WebControls.Label lbKQ_YEBANhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_YEBANhj");
                    System.Web.UI.WebControls.Label lbKQ_ZHONGBhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_ZHONGBhj");
                    System.Web.UI.WebControls.Label lbKQ_CBTShj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_CBTShj");
                    System.Web.UI.WebControls.Label lbKQ_YSGZhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_YSGZhj");

                    lbKQ_CHUQINhj.Text = dthjtotal.Rows[0]["KQ_CHUQIN"].ToString().Trim();
                    lbKQ_GNCChj.Text = dthjtotal.Rows[0]["KQ_GNCC"].ToString().Trim();
                    lbKQ_GWCChj.Text = dthjtotal.Rows[0]["KQ_GWCC"].ToString().Trim();
                    lbKQ_BINGJhj.Text = dthjtotal.Rows[0]["KQ_BINGJ"].ToString().Trim();
                    lbKQ_SHIJhj.Text = dthjtotal.Rows[0]["KQ_SHIJ"].ToString().Trim();
                    lbKQ_KUANGGhj.Text = dthjtotal.Rows[0]["KQ_KUANGG"].ToString().Trim();
                    lbKQ_DAOXIUhj.Text = dthjtotal.Rows[0]["KQ_DAOXIU"].ToString().Trim();
                    lbKQ_CHANJIAhj.Text = dthjtotal.Rows[0]["KQ_CHANJIA"].ToString().Trim();
                    lbKQ_PEICHANhj.Text = dthjtotal.Rows[0]["KQ_PEICHAN"].ToString().Trim();
                    lbKQ_HUNJIAhj.Text = dthjtotal.Rows[0]["KQ_HUNJIA"].ToString().Trim();
                    lbKQ_SANGJIAhj.Text = dthjtotal.Rows[0]["KQ_SANGJIA"].ToString().Trim();
                    lbKQ_GONGShj.Text = dthjtotal.Rows[0]["KQ_GONGS"].ToString().Trim();
                    lbKQ_NIANXhj.Text = dthjtotal.Rows[0]["KQ_NIANX"].ToString().Trim();
                    lbKQ_BEIYONG1hj.Text = dthjtotal.Rows[0]["KQ_BEIYONG1"].ToString().Trim();
                    lbKQ_BEIYONG2hj.Text = dthjtotal.Rows[0]["KQ_BEIYONG2"].ToString().Trim();
                    lbKQ_BEIYONG3hj.Text = dthjtotal.Rows[0]["KQ_BEIYONG3"].ToString().Trim();

                    lbKQ_BEIYONG4hj.Text = dthjtotal.Rows[0]["KQ_BEIYONG4"].ToString().Trim();
                    lbKQ_BEIYONG5hj.Text = dthjtotal.Rows[0]["KQ_BEIYONG5"].ToString().Trim();
                    lbKQ_BEIYONG6hj.Text = dthjtotal.Rows[0]["KQ_BEIYONG6"].ToString().Trim();

                    lbKQ_QTJIAhj.Text = dthjtotal.Rows[0]["KQ_QTJIA"].ToString().Trim();
                    lbKQ_JIEDIAOhj.Text = dthjtotal.Rows[0]["KQ_JIEDIAO"].ToString().Trim();
                    lbKQ_ZMJBANhj.Text = dthjtotal.Rows[0]["KQ_ZMJBAN"].ToString().Trim();
                    lbKQ_JRJIABhj.Text = dthjtotal.Rows[0]["KQ_JRJIAB"].ToString().Trim();
                    lbKQ_ZHIBANhj.Text = dthjtotal.Rows[0]["KQ_ZHIBAN"].ToString().Trim();
                    lbKQ_YEBANhj.Text = dthjtotal.Rows[0]["KQ_YEBAN"].ToString().Trim();
                    lbKQ_ZHONGBhj.Text = dthjtotal.Rows[0]["KQ_ZHONGB"].ToString().Trim();
                    lbKQ_CBTShj.Text = dthjtotal.Rows[0]["KQ_CBTS"].ToString().Trim();
                    lbKQ_YSGZhj.Text = dthjtotal.Rows[0]["KQ_YSGZ"].ToString().Trim();
                }
            }
        }
        #endregion

        #region 年假数据查询分页
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar2()
        {
            InitPager2();
            UCPaging2.PageChanged += new UCPaging.PageHandler(Pager_PageChanged2);
            UCPaging2.PageSize = pager_org2.PageSize;    //每页显示的记录数

        }

        /// <summary>
        /// 分页初始化
        /// </summary>
        /// <param name="where"></param>
        private void InitPager2()
        {
            pager_org2.TableName = "View_OM_KQTJ";
            pager_org2.PrimaryKey = "id";
            pager_org2.ShowFields = "KQ_ST_ID,KQ_DATE,ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1,KQ_GNCC,KQ_GWCC,KQ_BINGJ,KQ_SHIJ,KQ_KUANGG,KQ_DAOXIU,KQ_CHANJIA,KQ_PEICHAN,KQ_HUNJIA,KQ_SANGJIA,KQ_GONGS,KQ_NIANX,KQ_BEIYONG1,KQ_BEIYONG2,KQ_BEIYONG3,KQ_BEIYONG4,KQ_BEIYONG5,KQ_BEIYONG6,KQ_QTJIA,KQ_JIEDIAO,KQ_ZMJBAN,KQ_JRJIAB,KQ_ZHIBAN,KQ_YEBAN,KQ_ZHONGB,KQ_CBTS,KQ_YSGZ,KQ_BEIZHU,KQ_XGTIME,KQ_CHUQIN";
            pager_org2.OrderField = "KQ_ST_ID";
            pager_org2.StrWhere = StrWhere2();
            pager_org2.OrderType = 0;//升序排列
            pager_org2.PageSize = 25;
        }
        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere2()
        {
            stid = Request.QueryString["stid"].ToString().Trim();
            string sql = "1=1 and KQ_ST_ID='" + stid + "' and KQ_NIANX!=0";
            if (radio_dangnian.Checked == true)
            {
                sql += " and KQ_DATE like '" + DateTime.Now.Year.ToString().Trim() + "%'";
            }
            return sql;
        }
        /// <summary>
        /// 换页事件
        /// </summary>
        private void Pager_PageChanged2(int pageNumber)
        {
            bindrpt2();
        }

        private void bindrpt2()
        {
            InitPager2();
            pager_org2.PageIndex = UCPaging2.CurrentPage;
            System.Data.DataTable dt2 = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org2);
            CommonFun.Paging(dt2, rptKQTJ2, UCPaging2, palNoData2);
            if (palNoData2.Visible)
            {
                UCPaging2.Visible = false;
            }
            else
            {
                UCPaging2.Visible = true;
                UCPaging2.InitPageInfo();
            }
        }


        protected void rptKQTJ2_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhjnj = "select sum(KQ_GNCC) as KQ_GNCC,sum(KQ_GWCC) as KQ_GWCC,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_KUANGG) as KQ_KUANGG,sum(KQ_DAOXIU) as KQ_DAOXIU,sum(KQ_CHANJIA) as KQ_CHANJIA,sum(KQ_PEICHAN) as KQ_PEICHAN,sum(KQ_HUNJIA) as KQ_HUNJIA,sum(KQ_SANGJIA) as KQ_SANGJIA,sum(KQ_GONGS) as KQ_GONGS,sum(KQ_NIANX) as KQ_NIANX,sum(KQ_BEIYONG1) as KQ_BEIYONG1,sum(KQ_BEIYONG2) as KQ_BEIYONG2,sum(KQ_BEIYONG3) as KQ_BEIYONG3,sum(KQ_BEIYONG4) as KQ_BEIYONG4,sum(KQ_BEIYONG5) as KQ_BEIYONG5,sum(KQ_BEIYONG6) as KQ_BEIYONG6,sum(KQ_QTJIA) as KQ_QTJIA,sum(KQ_JIEDIAO) as KQ_JIEDIAO,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZHIBAN) as KQ_ZHIBAN,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_ZHONGB) as KQ_ZHONGB,sum(KQ_CBTS) as KQ_CBTS,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_CHUQIN) as KQ_CHUQIN from View_OM_KQTJ where " + StrWhere2() + "";
                System.Data.DataTable dthjnj = DBCallCommon.GetDTUsingSqlText(sqlhjnj);
                if (dthjnj.Rows.Count > 0)
                {
                    System.Web.UI.WebControls.Label lbnjKQ_NIANXhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbnjKQ_NIANXhj");

                    lbnjKQ_NIANXhj.Text = dthjnj.Rows[0]["KQ_NIANX"].ToString().Trim();
                }
            }
        }
        #endregion

        protected void radio_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt1();
            UCPaging2.CurrentPage = 1;
            bindrpt2();
        }
    }
}
