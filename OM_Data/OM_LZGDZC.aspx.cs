using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_LZGDZC : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        string action = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //InitVar();
            action = Request.QueryString["action"];
            if (!IsPostBack)
            {
                //GetBoundData();
                InitRepeaterView();
            }
        }
        private void InitRepeaterView()
        {
            string sqltext = "select NAME,MODEL,SYR,SYRID,BUMEN,BUMENID,BH,DATE,PLACE from TBOM_TRANSBH";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }
        //#region 分页
        //private void InitVar()
        //{
        //    InitPager();
        //    UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
        //    UCPaging1.PageSize = pager.PageSize;
        //}
        //private void InitPager()
        //{
        //    pager.TableName = "TBOM_TRANSBH";
        //    pager.PrimaryKey = "ID";
        //    pager.ShowFields = "ID*1 as ID,NAME,MODEL,SYR,SYRID,BUMEN,BUMENID,BH,DATE";
        //    pager.OrderField = "";
        //    pager.StrWhere = "";
        //    pager.OrderType = 0;
        //    pager.PageSize = 50;
        //}
        //void Pager_PageChanged(int pageNumber)
        //{
        //    ReGetBoundData();
        //}
        //protected void GetBoundData()
        //{
        //    pager.PageIndex = UCPaging1.CurrentPage;
        //    DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
        //    CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);
        //    if (NoDataPanel.Visible)
        //    {
        //        UCPaging1.Visible = false;
        //    }
        //    else
        //    {
        //        UCPaging1.Visible = true;
        //        UCPaging1.InitPageInfo();
        //    }
        //}
        //private void ReGetBoundData()
        //{
        //    InitPager();
        //    GetBoundData();
        //}
        //#endregion
        private void GetBUMEN(DropDownList ddlobject, string value)
        {
            ddlobject.Items.Clear();
            string sql2 = "select DEP_NAME,DEP_CODE FROM TBDS_DEPINFO WHERE DEP_CODE LIKE '[0-9][0-9]'";
            DBCallCommon.BindDdl(ddlobject, sql2, "DEP_NAME", "DEP_CODE");
            ddlobject.SelectedValue = value;
        }
        protected void Repeater1_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                DropDownList ddl_bumen = (DropDownList)e.Item.FindControl("ddl_bumen2");
                HiddenField hfbumen = (HiddenField)e.Item.FindControl("hfbumen2");
                GetBUMEN(ddl_bumen, hfbumen.Value);
            }
        }
        protected void btn_submit_click(object sender, EventArgs e)
        {
            string dh = GetDH();
            string sqltxt = "";
            string bh = "";
            string name = "";
            string model = "";
            string former = "";
            string formerid = "";
            string latter = "";
            string latterid = "";
            string reason = "";
            string time1 = "";
            string bumen1 = "";
            string bumen1id = "";
            string bumen2 = "";
            string bumen2id = "";
            string time2 = "";
            string place1 = "";
            string place2 = "";

            List<string> list = new List<string>();
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                bh = ((Label)Repeater1.Items[i].FindControl("lblbh")).Text;
                name = ((Label)Repeater1.Items[i].FindControl("lblName")).Text;
                model = ((Label)Repeater1.Items[i].FindControl("lblModel")).Text;
                former = ((Label)Repeater1.Items[i].FindControl("lblsyr")).Text;
                formerid = ((Label)Repeater1.Items[i].FindControl("lblsyrid")).Text;
                bumen1 = ((Label)Repeater1.Items[i].FindControl("lblsybm")).Text;
                bumen1id = ((Label)Repeater1.Items[i].FindControl("syrbmid")).Text;
                time1 = ((Label)Repeater1.Items[i].FindControl("lbldate")).Text;
                latter = ((HtmlInputHidden)Repeater1.Items[i].FindControl("hidSyr2")).Value.ToString();
                latterid = ((HtmlInputHidden)Repeater1.Items[i].FindControl("hidSyrId2")).Value.ToString();
                bumen2 = ((DropDownList)Repeater1.Items[i].FindControl("ddl_bumen2")).SelectedItem.Text;
                bumen2id = ((DropDownList)Repeater1.Items[i].FindControl("ddl_bumen2")).SelectedValue.ToString();
                time2 = ((TextBox)Repeater1.Items[i].FindControl("txtdate2")).Text;
                reason = ((TextBox)Repeater1.Items[i].FindControl("txtreason")).Text;
                place1 = ((Label)Repeater1.Items[i].FindControl("lblplace1")).Text;
                place2 = ((TextBox)Repeater1.Items[i].FindControl("txtplace2")).Text;
                if (action == "LZSX")
                {
                    sqltxt = "insert into TBOM_GDZCTRANSFER (DH,ZYBIANHAO,NAME,MODEL,FORMERNAME,FORMERID,LATTERID,LATTERNAME,REASON,TIME1,TIME2,JBR,JBRID,FBM,FBMID,LBM,LBMID,FPLACE,LPLACE,TRANSFTYPE,ZYLX) VALUES ('" + dh + "','" + bh + "','" + name + "','" + model + "','" + former + "','" + formerid + "','" + latterid + "','" + latter + "','" + reason + "','" + time1 + "','" + time2 + "','" + Session["UserName"].ToString() + "','" + Session["UserID"].ToString() + "','" + bumen1 + "','" + bumen1id + "','" + bumen2 + "','" + bumen2id + "','" + place1 + "','" + place2 + "','0','LZ')";
                    list.Add(sqltxt);
                    sqltxt = "update TBOM_GDZCIN SET SYR='" + latter + "',SYRID='" + latterid + "',SYBUMEN='" + bumen2 + "',SYBUMENID='" + bumen2id + "',SYDATE='" + time2 + "',PLACE='" + place2 + "' WHERE BIANHAO='" + bh + "'";
                    list.Add(sqltxt);
                }
                else
                {
                    sqltxt = "insert into TBOM_GDZCTRANSFER (ZYBIANHAO,NAME,MODEL,FORMERNAME,FORMERID,LATTERID,LATTERNAME,REASON,TIME1,TIME2,JBR,JBRID,FBM,FBMID,LBM,LBMID,FPLACE,LPLACE,TRANSFTYPE) VALUES ('" + bh + "','" + name + "','" + model + "','" + former + "','" + formerid + "','" + latterid + "','" + latter + "','" + reason + "','" + time1 + "','" + time2 + "','" + Session["UserName"].ToString() + "','" + Session["UserID"].ToString() + "','" + bumen1 + "','" + bumen1id + "','" + bumen2 + "','" + bumen2id + "','" + place1 + "','" + place2 + "','0')";
                    list.Add(sqltxt);
                    sqltxt = "update TBOM_GDZCIN SET SYR='" + latter + "',SYRID='" + latterid + "',SYBUMEN='" + bumen2 + "',SYBUMENID='" + bumen2id + "',SYDATE='" + time2 + "',PLACE='" + place2 + "' WHERE BIANHAO='" + bh + "'";
                    list.Add(sqltxt);
                }
            }
            sqltxt = string.Format("insert into OM_SP (SPFATHERID,SPLX,SPJB,ZDR,ZDRID,ZDR_SJ,ZDR_JY,SPR1,SPR1ID,SPR1_JL,SPR1_SJ,SPR1_JY,SPR2,SPR2ID,SPR2_JL,SPR2_SJ,SPR2_JY,SPR3,SPR3ID,SPR3_JL,SPR3_SJ,SPR3_JY,SPZT) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')", dh, "GDZCZY", "", "", "", "", "", "","", "", "", "", "", "", "", "", "", "", "", "", "", "", "y");
            list.Add(sqltxt);
            DBCallCommon.ExecuteTrans(list);
            if (action == "LZSX")
            {
                Response.Write("<script>alert('添加转移成功');window.close();</script>");
            }
            else
            {
                Response.Write("<script>alert('添加转移成功');window.location.href='OM_GdzcStore.aspx';</script>");
            }
        }

        private string GetDH()
        {
            string dh = "";
            string sql = "select max(DH) as DH from TBOM_GDZCTRANSFER where TRANSFTYPE='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() == "")
            {
                dh = "G-ZY00001";
            }
            else
            {
                dh = dt.Rows[0][0].ToString().Split('Y')[0] + "Y" + (CommonFun.ComTryInt(dt.Rows[0][0].ToString().Split('Y')[1]) + 1).ToString().PadLeft(5, '0');
            }
            return dh;
        }//获取转移单号
    }
}
