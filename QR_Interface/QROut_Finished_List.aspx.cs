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
using System.Text;

namespace ZCZJ_DPF.QR_Interface
{
    public partial class QROut_Finished_List : System.Web.UI.Page
    {
        string flag;
        PagerQueryParam pager = new PagerQueryParam();
        List<string> list = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                string sql = "select distinct TFO_ENGNAME,TSA_ID,TFO_ZONGXU,TFO_MAP from TBMP_FINISHED_OUT ";
                asd.dt = DBCallCommon.GetDTUsingSqlText(sql);
                GetCode();
                GetBoundData();
            }
        }

        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }
        private void InitPager()
        {

            pager.TableName = "midTable_Finished_QROut as S left join TBPM_STRINFODQO as  A on (S.CPQROut_TaskID=A.BM_ENGID and S.CPQROut_Zongxu=A.BM_ZONGXU) left join TBMP_FINISHED_STORE as B ON (A.BM_ENGID=B.KC_TSA AND A.BM_ZONGXU=B.KC_ZONGXU ) left join View_CM_FaHuo as C  on A.BM_ENGID=C.TSA_ID and A.BM_ZONGXU=C.ID";
            pager.PrimaryKey = "";
            pager.ShowFields = "S.*,A.*,C.*,B.KC_KCNUM,B.KC_ZONGXU,B.KC_SINGNUMBER,B.KC_PROJ";
            pager.OrderField = "CPQROut_TaskID,CPQROut_Zongxu";
            pager.StrWhere = GetWhere();//发运比价审核通过后才能出库
            pager.OrderType = 0;
            pager.PageSize = 50;
        }
        private string GetWhere()
        {
            StringBuilder strWhere = new StringBuilder();
            if (flag == "ToStore")
            {
                strWhere.Append("(BM_FHSTATE='2' or BM_FHSTATE='4') and KC_KCNUM>0 and CPQROut_State='0'");//比价通过审批并且库存数量大于0的可以发
            }
            else
            {
                strWhere.Append("1=1");
            }
            return strWhere.ToString();
        }
        void Pager_PageChanged(int pageNumber)
        {
            ReGetBoundData();
        }

        protected void GetBoundData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        private void ReGetBoundData()
        {
            InitPager();
            GetBoundData();
        }

        private class asd
        {
            public static DataTable dt;
        }

        protected void Repeater1_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }
            DataRowView drv = e.Item.DataItem as DataRowView;
            foreach (DataRow dr in asd.dt.Rows)
            {
                if (drv["BM_CHANAME"].ToString() == dr["TFO_ENGNAME"].ToString() && drv["BM_ENGID"].ToString() == dr["TSA_ID"].ToString() && drv["KC_ZONGXU"].ToString() == dr["TFO_ZONGXU"].ToString() && drv["BM_TUHAO"].ToString() == dr["TFO_MAP"].ToString())
                {
                    ((Label)e.Item.FindControl("CM_BIANHAO")).BackColor = System.Drawing.Color.LightGreen;
                }
            }
        }

        private void GetCode()
        {
            string sqltext;
            sqltext = "select TOP 1 TFO_DOCNUM AS TopIndex from TBMP_FINISHED_OUT ORDER BY TFO_DOCNUM DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int index;
            if (dt.Rows.Count > 0)
            {
                index = Convert.ToInt16(dt.Rows[0]["TopIndex"].ToString());
            }
            else
            {
                index = 0;
            }
            string code = (index + 1).ToString();
            docnum.Value = code.PadLeft(8, '0');
        }
        //下推出库
        protected void btnPush_OnClick(object sender, EventArgs e)
        {
            string tsaid = "";
            string sql = "";
            string sql2 = "";
            int j = 0;
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {

                if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    tsaid = ((Label)Repeater1.Items[i].FindControl("lbCPQROut_TaskID")).Text;
                    string engname = ((Label)Repeater1.Items[i].FindControl("lblEngname")).Text;
                    string zongxu = ((Label)Repeater1.Items[i].FindControl("CPQROut_Zongxu")).Text;
                    string map = ((Label)Repeater1.Items[i].FindControl("lblMap")).Text;
                    int kcnum = Convert.ToInt32(((Label)Repeater1.Items[i].FindControl("lblkcnum")).Text);
                    int cknum = CommonFun.ComTryInt(((TextBox)Repeater1.Items[i].FindControl("txtCPQROut_Num")).Text);
                    string fid = ((Label)Repeater1.Items[i].FindControl("CM_FID")).Text;
                    string bianhao = ((Label)Repeater1.Items[i].FindControl("CM_BIANHAO")).Text;
                    string singlenum = ((Label)Repeater1.Items[i].FindControl("KC_SINGNUM")).Text;
                    string yfnum = ((Label)Repeater1.Items[i].FindControl("txtYfnum")).Text;
                    int yf = CommonFun.ComTryInt(yfnum);
                    if (cknum == 0)
                    {
                        string alert = "<script>alert('请填写出库数量！！！')</script>";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                        return;
                    }
                    if (cknum > kcnum || cknum > yf)
                    {
                        string alert = "<script>alert('出库数量不得大于已比价数量或库存数量！！！')</script>";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                        return;
                    }
                    sql = " insert into TBMP_FINISHED_OUT (TFO_ZONGXU,TFO_DOCNUM,TFO_ENGNAME,TFO_MAP,TSA_ID,TFO_KCNUM,TFO_CKNUM,TFO_BIANHAO,TFO_FID,TFO_SINGNUM,QROutUniqCode) VALUES ('" + zongxu + "','" + docnum.Value.ToString() + "','" + engname + "','" + map + "','" + tsaid + "'," + kcnum + "," + cknum + ",'" + bianhao + "','" + fid + "','" + singlenum + "'," + Convert.ToInt32(((Label)Repeater1.Items[i].FindControl("CPQROut_ID")).Text.ToString()) + ")";
                    list.Add(sql);

                    sql2 = "update midTable_Finished_QROut set CPQROut_State='1' where CPQROut_ID=" + Convert.ToInt32(((Label)Repeater1.Items[i].FindControl("CPQROut_ID")).Text.ToString()) + "";
                    list.Add(sql2);

                    j++;
                }
            }
            if (j < 1)
            {
                string alert = "<script>alert('请选择下推条目！！！')</script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                return;
            }
            DBCallCommon.ExecuteTrans(list);
            Response.Redirect("~/PM_Data/PM_Finished_OUTBILL.aspx?FLAG=PUSH&docnum=" + docnum.Value.ToString() + "");
        }
    }
}
