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
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_InspecManage : BasicPage
    {

        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {

            InitVar();

            if (!IsPostBack)
            {
                bindGrid();
            }
        }

        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数      
        }

        //初始化分布信息
        private void InitPager()
        {
            pager.TableName = "TBQM_APLYFORINSPCT as a inner join TBPM_TCTSASSGN as b on a.AFI_ENGID=b.TSA_ID";
            pager.PrimaryKey = "AFI_ID";
            pager.ShowFields = "AFI_ID*1 AS AFI_ID,AFI_TSDEP,AFI_PJNAME,AFI_ENGID,AFI_ENGNAME,AFI_PARTNAME ,AFI_DATACLCT ,left(AFI_DATE,10) as AFI_DATE,AFI_RQSTCDATE,AFI_SUPPLERNM,AFI_ISPCTSITE,AFI_CONTACT,AFI_CONTEL,AFI_MAN,AFI_MANNM,AFI_NOTE,AFI_NUMBER,UNIQUEID,AFI_QCMANNM,isnull(AFI_ENDRESLUT,'') as AFI_ENDRESLUT,left(AFI_ENDDATE,10) as AFI_ENDDATE,b.TSA_PJID as TSA_PJID";
            pager.OrderField = DropDownListType.SelectedValue;//按报检时间排序
            pager.StrWhere = CreateConStr();
            pager.OrderType = Convert.ToInt32(RadioButtonListOrderBy.SelectedValue);//按时间降序排列
            pager.PageSize = 10;
        }

        void Pager_PageChanged(int pageNumber)
        {

            bindGrid();

        }

        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            CheckUser(ControlFinder);
        }



        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#e4ecf7'");//当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");//当鼠标移开时还原背景色
                (e.Row.FindControl("hytask") as HyperLink).Attributes.Add("onClick", "ShowUpdateModal('" + GridView1.DataKeys[e.Row.RowIndex]["AFI_ID"].ToString() + "');");
                (e.Row.FindControl("hydetail") as HyperLink).Attributes.Add("onClick", "ShowViewModal('" + GridView1.DataKeys[e.Row.RowIndex]["AFI_ID"].ToString() + "');");
            }
        }

        protected void ReloadGrid_Click(object sender, EventArgs e)
        {
            bindGrid();
        }


        protected void QueryButton_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;

            bindGrid();

            UpdatePanelBody.Update();
        }

        /// <summary>
        /// 质检状态的查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }

        protected void TypeOrOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }

        private string CreateConStr()
        {
            //需要改变
            string status = rblstatus.SelectedValue.ToString();
            string condition = "";
            if (status == "1")
            {
                condition = "AFI_ASSGSTATE='1'";
            }
            else
            {
                condition = "AFI_ASSGSTATE='0'";
            }
            //质检结果
            if (rblresult.SelectedIndex != -1)
            {
                if (rblresult.SelectedValue != "0")
                {
                    if (rblresult.SelectedValue == "1")
                    {
                        condition += "AND AFI_ENDRESLUT='合格'";
                    }
                    else if (rblresult.SelectedValue == "2")
                    {
                        condition += "AND AFI_ENDRESLUT='不合格'";
                    }
                    else if (rblresult.SelectedValue == "3")
                    {
                        condition += "AND AFI_ENDRESLUT='关闭'";
                    }
                    else
                    {

                    }
                }
                else
                {
                    condition += "AND AFI_ENDRESLUT is NULL";
                }
            }

            //项目名称
            if (TextBoxProj.Text.Trim() != "" && (condition != ""))
            {
                condition += " AND AFI_PJNAME LIKE '%" + TextBoxProj.Text.Trim() + "%'";
            }

            //报检人
            if ((TextBoxInspecMan.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " AFI_MANNM LIKE '%" + TextBoxInspecMan.Text.Trim() + "%'";
            }
            else if ((TextBoxInspecMan.Text != "") && (condition == ""))
            {
                condition += " AFI_MANNM LIKE '%" + TextBoxInspecMan.Text.Trim() + "%'";
            }

            //质检人
            if ((TextBoxMan.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " AFI_QCMANNM LIKE'%" + TextBoxMan.Text.Trim() + "%'";
            }
            else if ((TextBoxMan.Text.Trim() != "") && (condition == ""))
            {
                condition += " AFI_QCMANNM LIKE '%" + TextBoxMan.Text.Trim() + "%'";
            }


            //工程名称
            if ((TextBoxEng.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " AFI_ENGNAME LIKE '%" + TextBoxEng.Text.Trim() + "%'";
            }
            else if ((TextBoxEng.Text != "") && (condition == ""))
            {
                condition += " AFI_ENGNAME LIKE '%" + TextBoxEng.Text.Trim() + "%'";
            }

            //质检编号
            if ((TextBoxZJID.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND AFI_ID LIKE '%" + Convert.ToInt32(TextBoxZJID.Text.Trim()) + "%'";
            }
            else if ((TextBoxZJID.Text.Trim() != "") && (condition == ""))
            {
                condition += " AFI_ID LIKE '%" + Convert.ToInt32(TextBoxZJID.Text.Trim()) + "%'";
            }

            //部件名称
            if ((TextBoxPart.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " AFI_PARTNAME LIKE '%" + TextBoxPart.Text.Trim() + "%'";
            }
            else if ((TextBoxPart.Text != "") && (condition == ""))
            {
                condition += " AFI_PARTNAME LIKE '%" + TextBoxPart.Text.Trim() + "%'";
            }

            //报检部门
            if (DropDownListDep.SelectedValue != "")
            {
                if (condition != "")
                {
                    condition += " AND " + " AFI_TSDEP='" + DropDownListDep.SelectedItem.Text + "'";
                }
                else
                {
                    condition += " AFI_TSDEP='" + DropDownListDep.SelectedItem.Text + "'";
                }
            }
            //生产制号
            if ((TextBoxENGID.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " AFI_ENGID LIKE '%" + TextBoxENGID.Text.Trim() + "%'";
            }
            else if ((TextBoxENGID.Text.Trim() != "") && (condition == ""))
            {
                condition += " AFI_ENGID LIKE '%" + TextBoxENGID.Text.Trim() + "%'";
            }

            //合同号
            if ((TextBoxheT.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " TSA_PJID LIKE '%" + TextBoxheT.Text.Trim() + "%'";
            }
            else if ((TextBoxheT.Text.Trim() != "") && (condition == ""))
            {
                condition += " TSA_PJID LIKE '%" + TextBoxheT.Text.Trim() + "%'";
            }


            //供货商
            if ((TextBoxSUPPLERNM.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " AFI_SUPPLERNM LIKE '%" + TextBoxSUPPLERNM.Text.Trim() + "%'";
            }
            else if ((TextBoxSUPPLERNM.Text.Trim() != "") && (condition == ""))
            {
                condition += " AFI_SUPPLERNM LIKE '%" + TextBoxSUPPLERNM.Text.Trim() + "%'";
            }

            if (txtStartTime.Text.Trim() != "")
            {
                condition += " and  AFI_ENDDATE >='" + txtStartTime.Text.Trim() + "'";
            }
            if (txtEndTime.Text.Trim() != "")
            {
                condition += " and AFI_ENDDATE <='" + txtEndTime.Text.Trim() + "'";
            }
            return condition;
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridView1.Rows)
            {
                Label lb = (Label)gr.FindControl("lbafiid");
                //Label lbsj = (Label)gr.FindControl("lbafisj");
                Label lbid = (Label)gr.FindControl("lbzjid");
                //string sj = lbsj.Text.Substring(0, 4);
                string id = lb.Text.ToString();
                lbid.Text = id.PadLeft(5, '0');
            }
        }
        protected void btnOutPutExcel_Click(object sender, EventArgs e)
        {
            string sqlText = "select  a.AFI_SUPPLERNM,PTC,a.AFI_PJNAME,right('0000'+ltrim(a.AFI_ID),5) as AFI_ID,a.AFI_RQSTCDATE,a.AFI_ENDDATE, a.AFI_ENGID,b.TUHAO,GB,b.PARTNM,b.PJNUM,b.QCNUM,'' as aa,'' as bb,'' as cc,'' as dd,b.RESULT,b.BHGITEM,b.ZGCONTENT,a.AFI_QCMANNM,case when b.ISAGAIN='1' then '二' else '' end as ISAGAIN  from TBQM_APLYFORINSPCT as a left join TBQM_APLYFORITEM as b  on a.uniqueid=b.uniqueid left join TBMA_MATERIAL as c on b.Marid=c.ID where " + CreateConStr() + " order by " + DropDownListType.SelectedValue + " desc";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqlText);

            string filename = "报检单.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("报检单.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet = wk.GetSheetAt(0);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    IRow row = sheet.CreateRow(i + 3);
                    for (int j = 0; j < dt1.Columns.Count; j++)
                    {
                        row.CreateCell(0).SetCellValue(i + 1);
                        row.CreateCell(j + 1).SetCellValue(dt1.Rows[i][j].ToString());
                    }
                }
                sheet.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }


    }
}
