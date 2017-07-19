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
using Microsoft.Office.Interop.MSProject;
using System.Collections.Generic;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_LZSQJSX : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                asd.userid = Session["UserID"].ToString();
                asd.username = Session["UserName"].ToString();
                Bindddl();
                BindData();
            }
        }

        private class asd
        {
            public static string username;
            public static string userid;
            public static string depname;
            public static string sjid;
        }

        private void Bindddl()
        {
            string sql = "select  DEP_CODE,DEP_NAME from TBDS_DEPINFO where  DEP_FATHERID=0";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddlBM.Items.Add(new ListItem("-请选择-", "0"));
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                ddlBM.Items.Add(new ListItem(dt.Rows[i]["DEP_NAME"].ToString(), dt.Rows[i]["DEP_CODE"].ToString()));
            }
        }

        #region  **************分页***********
        /// <summary>
        /// 换页事件
        /// </summary>
        /// <param name="pageNumber"></param>
        private void Pager_PageChanged(int pageNumber)
        {
            BindData();
        }
        /// <summary>
        /// 绑定repeater,分页控件,palNoData控件
        /// </summary>
        private void BindData()
        {
            InitPager();
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptLZSQJSX, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            if (Session["UserDeptID"].ToString() == "02")
            {
                sz_shr_an.Visible = true;
            }
            else
            {
                sz_shr_an.Visible = false;
            }
        }

        /// <summary>
        /// 绑定page
        /// </summary>
        /// <param name="where"></param>
        private void InitPager()
        {
            pager_org.TableName = "OM_LIZHISHOUXU";
            pager_org.PrimaryKey = "LZ_ID";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "LZ_RIQI";
            pager_org.StrWhere = CreateConStr();
            pager_org.OrderType = 1;//降序排列
            pager_org.PageSize = 25;
        }
        private string CreateConStr()
        {
            string sql = "LZ_ID is not null";
            if (rblLZSXZT.SelectedValue == "1")
            {
                sql += " and ((LZ_ZDRID='" + asd.userid + "' and (LZ_GZJJZMR='' or LZ_WPJJZMR='' or LZ_ZLZMR='') and LZ_SPZT='0') or (LZ_PERSONID='" + asd.userid + "' and (LZ_GZJJZMR='' or LZ_WPJJZMR='' or LZ_ZLZMR='') and LZ_SPZT='0') or (LZ_ZJLLID='" + asd.userid + "' and LZ_ZJLLZT='' and (LZ_SPZT='' or LZ_SPZT='0')) or (LZ_SCBZID='" + asd.userid + "' and LZ_SCBZZT='' and LZ_SPZT='1y') or (LZ_CGBZID='" + asd.userid + "' and LZ_CGBZZT='' and LZ_SPZT='1y') or (LZ_JSBZID='" + asd.userid + "' and LZ_JSBZZT='' and LZ_SPZT='1y') or (LZ_ZLBZID='" + asd.userid + "' and LZ_ZLBZZT='' and LZ_SPZT='1y') or (LZ_GCSBZID='" + asd.userid + "' and LZ_GCSBZZT='' and LZ_SPZT='1y') or (LZ_SCBBZID='" + asd.userid + "' and LZ_SCBBZZT='' and LZ_SPZT='1y') or (LZ_CWBZID='" + asd.userid + "' and LZ_CWBZZT='' and LZ_SPZT='1y') or (LZ_SBBZID='" + asd.userid + "' and LZ_SBBZZT='' and LZ_SPZT='1y') or (LZ_STJLID='" + asd.userid + "' and LZ_STJLZT='' and LZ_SPZT='1y') or (LZ_CKGLYID='" + asd.userid + "' and LZ_CKGLYZT='' and LZ_SPZT='2y') or (LZ_GKGLYID='" + asd.userid + "' and LZ_GKGLYZT='' and LZ_SPZT='2y') or (LZ_GDZCGLYID='" + asd.userid + "' and LZ_GDZCZT='' and LZ_SPZT='2y') or (LZ_TSGLYID='" + asd.userid + "' and LZ_TSGLYZT='' and LZ_SPZT='2y') or (LZ_DWGLYID='" + asd.userid + "' and LZ_DWGLYZT='' and LZ_SPZT='2y') or (LZ_DZSBGLYID='" + asd.userid + "' and LZ_DZSBZT='' and LZ_SPZT='2y') or (LZ_KQGLYID='" + asd.userid + "' and LZ_KQGLYZT='' and LZ_SPZT='2y') or (LZ_LDGXGLRID='" + asd.userid + "' and LZ_LDGXGLRZT='' and LZ_SPZT='2y') or (LZ_SXGLRID='" + asd.userid + "' and LZ_SXGLRZT='' and LZ_SPZT='2y') or (LZ_GJJGLRID='" + asd.userid + "' and LZ_GJJGLRZT='' and LZ_SPZT='2y') or (LZ_DAGLRID='" + asd.userid + "' and LZ_DAGLRZT='' and LZ_SPZT='2y') or (LZ_GRXXGLYID='" + asd.userid + "' and LZ_GRXXGLYZT='' and LZ_SPZT='2y') or (LZ_ZHBBZID='" + asd.userid + "' and LZ_ZHBSPZT='' and LZ_SPZT='3y') or (LZ_LDID='" + asd.userid + "' and LZ_LDSPZT='' and LZ_SPZT='4y'))";
            }
            else if (rblLZSXZT.SelectedValue=="2")
            {
                sql += " and LZ_SPZT in ('0','1y','2y','3y','4y')";
            }
            else if (rblLZSXZT.SelectedValue=="3")
            {
                sql += " and LZ_SPZT='y'";
            }
            else if (rblLZSXZT.SelectedValue == "4")
            {
                sql += " and LZ_SPZT='n'";
            }
            if (ddlBM.SelectedValue != "0")
            {
                sql += " and LZ_BUMENID='" + ddlBM.SelectedValue + "'";
            }
            if (txtNAME.Text.Trim() != "")
            {
                sql += " and LZ_PERSON like '%" + txtNAME.Text.Trim() + "%'";
            }
            return sql;
        }
        #endregion

        //查询
        protected void Query(object sender, EventArgs e)
        {
            BindData();
        }

        protected void rptLZSQJSX_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlTableCell tdBBMSH = e.Item.FindControl("tdBBMSH") as HtmlTableCell;
                HtmlTableCell tdAlter = e.Item.FindControl("tdAlter") as HtmlTableCell;
                HtmlTableCell tdDelete = e.Item.FindControl("tdDelete") as HtmlTableCell;
                tdAlter.Visible = false;
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableCell tdBBMSH1 = e.Item.FindControl("tdBBMSH1") as HtmlTableCell;
                HtmlTableCell tdAlter1 = e.Item.FindControl("tdAlter1") as HtmlTableCell;
                HtmlTableCell tdDelete1 = e.Item.FindControl("tdDelete1") as HtmlTableCell;
                tdAlter1.Visible = false;

                HyperLink hplBBMSH = e.Item.FindControl("hplBBMSH") as HyperLink;
                LinkButton lbtnDelete = e.Item.FindControl("lbtnDelete") as LinkButton;
                hplBBMSH.Visible = false;
                lbtnDelete.Visible = false;
                DataRowView drv = e.Item.DataItem as DataRowView;
                switch (drv["LZ_SPZT"].ToString())
                {
                    case "0":
                        if (asd.userid == drv["LZ_ZDRID"].ToString())
                        {
                            hplBBMSH.Visible = true;
                            lbtnDelete.Visible = true;
                        }
                        if (asd.userid == drv["LZ_PERSONID"].ToString() || asd.userid == drv["LZ_ZJLLID"].ToString())
                        {
                            hplBBMSH.Visible = true;
                        }
                        break;
                    case "1y":
                        if ((asd.userid == drv["LZ_SCBZID"].ToString() && drv["LZ_SCBZZT"].ToString() == "") || (asd.userid == drv["LZ_CGBZID"].ToString() && drv["LZ_CGBZZT"].ToString() == "") || (asd.userid == drv["LZ_JSBZID"].ToString() && drv["LZ_JSBZZT"].ToString() == "") || (asd.userid == drv["LZ_ZLBZID"].ToString() && drv["LZ_ZLBZZT"].ToString() == "") || (asd.userid == drv["LZ_GCSBZID"].ToString() && drv["LZ_GCSBZZT"].ToString() == "") || (asd.userid == drv["LZ_SCBBZID"].ToString() && drv["LZ_SCBBZZT"].ToString() == "") || (asd.userid == drv["LZ_CWBZID"].ToString() && drv["LZ_CWBZZT"].ToString() == "") || (asd.userid == drv["LZ_SBBZID"].ToString() && drv["LZ_SBBZZT"].ToString() == "") || (asd.userid == drv["LZ_STJLID"].ToString() && drv["LZ_STJLZT"].ToString() == ""))
                        {
                            hplBBMSH.Visible = true;
                        }
                        break;
                    case "2y":
                        if ((asd.userid == drv["LZ_CKGLYID"].ToString() && drv["LZ_CKGLYZT"].ToString() == "") || (asd.userid == drv["LZ_GKGLYID"].ToString() && drv["LZ_GKGLYZT"].ToString() == "") || (asd.userid == drv["LZ_GDZCGLYID"].ToString() && drv["LZ_GDZCZT"].ToString() == "") || (asd.userid == drv["LZ_TSGLYID"].ToString() && drv["LZ_TSGLYZT"].ToString() == "") || (asd.userid == drv["LZ_DWGLYID"].ToString() && drv["LZ_DWGLYZT"].ToString() == "") || (asd.userid == drv["LZ_DZSBGLYID"].ToString() && drv["LZ_DZSBZT"].ToString()=="") || (asd.userid == drv["LZ_KQGLYID"].ToString() && drv["LZ_KQGLYZT"].ToString() == "") || (asd.userid == drv["LZ_LDGXGLRID"].ToString() && drv["LZ_LDGXGLRZT"].ToString() == "") || (asd.userid == drv["LZ_SXGLRID"].ToString() && drv["LZ_SXGLRZT"].ToString() == "") || (asd.userid == drv["LZ_GJJGLRID"].ToString() && drv["LZ_GJJGLRZT"].ToString() == "") || (asd.userid == drv["LZ_DAGLRID"].ToString() && drv["LZ_DAGLRZT"].ToString() == "") || (asd.userid == drv["LZ_GRXXGLYID"].ToString() && drv["LZ_GRXXGLYZT"].ToString() == ""))
                        {
                            hplBBMSH.Visible = true;
                        }
                        break;
                    case "3y":
                        if (asd.userid == drv["LZ_ZHBBZID"].ToString())
                        {
                            hplBBMSH.Visible = true;
                        }
                        break;
                    case "4y":
                        if (asd.userid == drv["LZ_LDID"].ToString())
                        {
                            hplBBMSH.Visible = true;
                        }
                        break;
                    case "n":
                        if (asd.userid == drv["LZ_ZDRID"].ToString())
                        {
                            lbtnDelete.Visible = true;
                        }
                        break;
                    default:
                        break;
                }
            }

        }
        protected void rblLZSXZT_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void lbtnDelete_OnClick(object sender, EventArgs e)//***************删除**************
        {
            string id = ((LinkButton)sender).CommandArgument.ToString();
            string sql = "delete from OM_LIZHISHOUXU where LZ_ID='" + id + "'";
            DBCallCommon.ExeSqlText(sql);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示：请假申请已删除！！！')", true);
            BindData();
        }


    }
}
