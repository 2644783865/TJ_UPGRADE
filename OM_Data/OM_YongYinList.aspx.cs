using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_YongYinList : BasicPage
    {
        List<string> codeList = new List<string>();
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                bindddl();
                databind();


            }
            ControlVisible();
            //  CheckUser(ControlFinder);
        }


        #region 初始化分页

        void Pager_PageChanged(int pageNumber)
        {
            databind();
            ControlVisible();
        }


        private void bindddl()
        {
            string sql = "select  DEP_CODE,DEP_NAME from TBDS_DEPINFO where  DEP_FATHERID=0";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddlLZ_BUMEN.Items.Add(new ListItem("-请选择-", "0"));
            for (int i = 0, length = dt.Rows.Count; i < length; i++)
            {
                ddlLZ_BUMEN.Items.Add(new ListItem(dt.Rows[i]["DEP_NAME"].ToString(), dt.Rows[i]["DEP_CODE"].ToString()));
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                string state = "";
                string applyid = "";
                string reviewid = "";
                string code = ((Label)e.Item.FindControl("lblCode")).Text;

                if (codeList.Contains(code))
                {
                    ((Label)e.Item.FindControl("lblCode")).Text = "";
                    ((Label)e.Item.FindControl("lblSQR")).Text = "";
                    ((Label)e.Item.FindControl("lblSQDEP")).Text = "";

                }
                else
                {
                    codeList.Add(code);
                }
            }
        }



        private void databind()
        {
            pager.TableName = "OM_YONGYINLIST as a left join dbo.OM_YONGYINDETAIL as b on a.Context=b.Context";
            pager.PrimaryKey = "a.Id";
            pager.ShowFields = "Code,SQRId,SQR,SqDep,SPLevel,SqTime,TaskId,Type,Num,GZR,a.Context,Name,SPJB";
            pager.OrderField = "a.Id";
            pager.StrWhere = strWhere();
            pager.OrderType = 1;
            pager.PageSize = 10;
            UCPaging1.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager);
            CommonFun.Paging(dt, rep_Kaohe, UCPaging1, NoDataPanel);
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



        private string strWhere()
        {
            string strWhere = "1=1 ";
            //2016.11.16添加
            if (txt_sqdh.Text.Trim() != "")//申请单号
            {
                strWhere += " and code like '%" + txt_sqdh.Text.Trim() + "%'";
            }
            if (txt_sqr.Text.Trim() != "")//申请人
            {
                strWhere += " and sqr like '%" + txt_sqr.Text.Trim() + "%'";
            }
            if (ddlLZ_BUMEN.SelectedValue!= "0")//所属部门
            {
                string dfasf = ddlLZ_BUMEN.SelectedItem.Text.Trim();
                strWhere += " and sqdep like '%" + ddlLZ_BUMEN.SelectedItem.Text.Trim() + "%'";
            }
            if (txt_ht.Text.Trim() != "")//合同号/任务号、项目名称
            {
                strWhere += " and taskid like '%" + txt_ht.Text.Trim() + "%'";
            }
            if (txt_yt.Text.Trim() != "")//文件用途
            {
                strWhere += " and Name like '%" + txt_yt.Text.Trim() + "%'";
            }

            if (txtStart.Text.Trim() != "")
            {
                strWhere += " and SqTime>='" + txtStart.Text.Trim() + "'";
            }
            if (txtEnd.Text.Trim() != "")
            {
                strWhere += " and SqTime<='" + txtEnd.Text.Trim() + "'";
            }

            if (rblState.SelectedValue == "0")
            {
                strWhere += " and State='0'";
            }
            else if (rblState.SelectedValue == "1")
            {
                strWhere += " and State in ('1','2','3','7')";
            }
            else if (rblState.SelectedValue == "2")
            {
                strWhere += " and State ='4'";
            }
            else if (rblState.SelectedValue == "3")
            {
                strWhere += " and State ='5'";
            }
            else if (rblState.SelectedValue == "4")
            {
                strWhere += " and ((State ='1' and SPRIDA='" + Session["UserId"].ToString() + "') or ( State='2' and SPRIDB='" + Session["UserId"].ToString() + "') or  ( State='3' and SPRIDC='" + Session["UserId"].ToString() + "') or (State ='7' and SPRIDD='" + Session["UserId"].ToString() + "'))  ";
            }
            else if (rblState.SelectedValue == "5")
            {
                strWhere += " and State ='6'";
            }
            return strWhere;
        }

        #endregion

        protected void ddl_Year_SelectedIndexChanged(object sender, EventArgs e)
        {

            UCPaging1.CurrentPage = 1;
            databind();
            ControlVisible();
        }

        private void ControlVisible()
        {
            foreach (RepeaterItem item in rep_Kaohe.Items)
            {
                HyperLink hlkEdit = item.FindControl("HyperLink2") as HyperLink;
                HyperLink hlkAudit = item.FindControl("HyperLink3") as HyperLink;
                LinkButton hlGZ = item.FindControl("hlGZ") as LinkButton;
                Label lbl_splevel_v = item.FindControl("lbl_splevel") as Label;
                Label lblSQRId_v = item.FindControl("lblSQRId") as Label;
                hlGZ.Visible = false;
                if ((rblState.SelectedValue == "0" || rblState.SelectedValue == "3")&& lblSQRId_v.Text == Session["UserId"].ToString())
                {
                    hlkEdit.Visible = true;
                }
                else
                {
                    hlkEdit.Visible = false;
                }
                if (rblState.SelectedValue == "4")
                {
                    hlkAudit.Visible = true;
                }
                else
                {
                    hlkAudit.Visible = false;
                }

                if (rblState.SelectedValue == "2" )
                {
                    if ((Session["UserGroup"].ToString().Contains("行政专员") && lbl_splevel_v.Text == "2") || (Session["UserGroup"].ToString().Contains("行政专员") && lbl_splevel_v.Text == "3") || (Session["UserGroup"].ToString().Contains("行政专员") && lbl_splevel_v.Text == "6") || (Session["UserGroup"].ToString().Contains("行政专员") && lbl_splevel_v.Text == "7") || (Session["UserGroup"].ToString().Contains("行政专员") && lbl_splevel_v.Text == "10") || (Session["UserGroup"].ToString().Contains("行政专员") && lbl_splevel_v.Text == "9"))
                    {
                        hlGZ.Visible = true;
                    }
                    else if ((Session["UserId"].ToString() == "171" && lbl_splevel_v.Text == "8"))
                    {
                        hlGZ.Visible = true;
                    }
                }

            }
        }

        protected void hlGZ_OnClick(object sender, EventArgs e)
        {
            string context = ((LinkButton)sender).CommandName;
            string sql = "update OM_YONGYINLIST set State='6',GZRId='" + Session["UserId"].ToString() + "',GZR='" + Session["UserName"].ToString() + "' where Context='" + context + "'";
            DBCallCommon.ExeSqlText(sql);
            UCPaging1.CurrentPage = 1;
            databind();
            ControlVisible();
        }



        protected void hlDelete_OnClick(object sender, EventArgs e)
        {

            if (Session["UserGroup"].ToString().Contains("行政专员") || Session["UserGroup"].ToString().Contains("管理员"))
            {
                string context = ((LinkButton)sender).CommandName;
                string sql = "delete from OM_YONGYINLIST  where Context='" + context + "'";
                DBCallCommon.ExeSqlText(sql);
                UCPaging1.CurrentPage = 1;
                databind();
                ControlVisible();
            }
            else
            {
                Response.Write("<script>alert('您无权删除该条记录')</script>");
            }

        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDaochu_OnClick(object sender, EventArgs e)
        {

            string sqltext = "";

            sqltext += "select *,case when Type='0' then '合同用章' when Type='1' then '公章' when Type='2' then '营业执照（正本）' when Type='3' then '营业执照（副本）' when Type='4' then '组织机构代码证（正本）' when Type='5' then '组织机构代码证（副本）' when Type='6' then '税务登记证（正本）' when Type='7' then '税务登记证（副本）' end as TypeNM  from OM_YONGYINLIST as a left join OM_YONGYINDETAIL as b on a.ConText=b.Context where " + strWhere();
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }




        private void ExportDataItem(System.Data.DataTable objdt)
        {
            string filename = "用印申请" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("用印申请.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));
                    row.CreateCell(1).SetCellValue("" + objdt.Rows[i]["SQR"].ToString());
                    row.CreateCell(2).SetCellValue("" + objdt.Rows[i]["SqDep"].ToString());
                    row.CreateCell(3).SetCellValue("" + objdt.Rows[i]["TypeNM"].ToString());
                    row.CreateCell(4).SetCellValue("" + objdt.Rows[i]["SqTime"].ToString());
                    row.CreateCell(5).SetCellValue("" + objdt.Rows[i]["Name"].ToString());
                    row.CreateCell(6).SetCellValue("" + objdt.Rows[i]["Num"].ToString());
                    row.CreateCell(7).SetCellValue("" + objdt.Rows[i]["GZR"].ToString());
                }

                for (int i = 0; i <= objdt.Columns.Count; i++)
                {
                    sheet0.AutoSizeColumn(i);
                }
                sheet0.ForceFormulaRecalculation = true;

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        //查看
        protected string viewSp(string yongyin_context)
        {
            string yongyin_detail_sql_view = "select context,splevel,spjb from OM_YONGYINLIST where context='" + yongyin_context + "'";
            System.Data.DataTable yongyin_detial_dt_view = DBCallCommon.GetDTUsingSqlText(yongyin_detail_sql_view);
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString()=="6")
            {
                return "javascript:void window.open('OM_YongYinDetial_cw_new1.aspx?action=view&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";
            
            }
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() == "7")
            {
                return "javascript:void window.open('OM_YongYinDetial_cw_new2.aspx?action=view&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";

            }
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() == "8")
            {
                return "javascript:void window.open('OM_YongYinDetial_cw_new3.aspx?action=view&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";

            }
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() == "9")
            {
                return "javascript:void window.open('OM_YongYinDetial_cw_new4.aspx?action=view&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";

            }
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() == "10")
            {
                return "javascript:void window.open('OM_YongYinDetial_cw_new5.aspx?action=view&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";

            }
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() == "4")
            {
                return "javascript:void window.open('OM_YongYinDetail_cw.aspx?action=view&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";
            }
            else 
            {
                return "javascript:void window.open('OM_YongYinDetail.aspx?action=view&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "','','')";
            }
        }
        //编辑
        protected string edit_yongyin_detail(string aduit_yongyin_context)
        {
            string yongyin_detail_sql_view = "select SQRId,context,splevel,spjb from OM_YONGYINLIST where context='" + aduit_yongyin_context + "'";
            System.Data.DataTable yongyin_detial_dt_view = DBCallCommon.GetDTUsingSqlText(yongyin_detail_sql_view);
            //if (yongyin_detial_dt_view.Rows[0]["SQRId"].ToString() != Session["UserId"].ToString())
            //{
            //    Response.Write("<script>alert('只有申请人能进行编辑');</script>");
            //    return "";
            //}
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() == "6")
            {
                return "javascript:void window.open('OM_YongYinDetial_cw_new1.aspx?action=edit&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";
            }
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() == "7")
            {
                return "javascript:void window.open('OM_YongYinDetial_cw_new2.aspx?action=edit&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";
            }
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() == "8")
            {
                return "javascript:void window.open('OM_YongYinDetial_cw_new3.aspx?action=edit&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";
            }
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() == "9")
            {
                return "javascript:void window.open('OM_YongYinDetial_cw_new4.aspx?action=edit&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";
            }
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() == "10")
            {
                return "javascript:void window.open('OM_YongYinDetial_cw_new5.aspx?action=edit&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";
            }
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() == "4")
            {
                return "javascript:void window.open('OM_YongYinDetail_cw.aspx?action=edit&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";
            }
            else 
            {
                return "javascript:void window.open('OM_YongYinDetail.aspx?action=edit&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "','','')";
            }
        }
        //审核
        protected string aduit_yongyin_detail(string aduit_yongyin_context)
        {
            string yongyin_detail_sql_view = "select context,splevel,spjb from OM_YONGYINLIST where context='" + aduit_yongyin_context + "'";
            System.Data.DataTable yongyin_detial_dt_view = DBCallCommon.GetDTUsingSqlText(yongyin_detail_sql_view);
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() == "6")
            {
                return "javascript:void window.open('OM_YongYinDetial_cw_new1.aspx?action=audit&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";
            }
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() == "7")
            {
                return "javascript:void window.open('OM_YongYinDetial_cw_new2.aspx?action=audit&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";
            }
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() == "8")
            {
                return "javascript:void window.open('OM_YongYinDetial_cw_new3.aspx?action=audit&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";

            }
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() == "9")
            {
                return "javascript:void window.open('OM_YongYinDetial_cw_new4.aspx?action=audit&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";
            }
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() == "10")
            {
                return "javascript:void window.open('OM_YongYinDetial_cw_new5.aspx?action=audit&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";
            }
            if (yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() == "4")
            {
                return "javascript:void window.open('OM_YongYinDetail_cw.aspx?action=audit&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "&spjb=" + yongyin_detial_dt_view.Rows[0]["SPJB"].ToString() + "','','')";
            }
            else 
            {
                return "javascript:void window.open('OM_YongYinDetail.aspx?action=audit&key=" + yongyin_detial_dt_view.Rows[0]["Context"].ToString() + "&type=" + yongyin_detial_dt_view.Rows[0]["SPLevel"].ToString() + "','','')";
            }
        }
    }
}
