using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_YOUQI : System.Web.UI.Page
    {
        string plan_id;
        string sqlText;
        string tablename;
        string fields;
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            //plan_id = Request.QueryString["paint"];
            //ViewState["plan_id"] = plan_id;
            InitVar();
            if (!IsPostBack)
            {
                InitInfo();
            }
        }
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        void Pager_PageChanged(int pageNumber)
        {
            GetTechRequireData();
        }
        //初始化参数
        private void InitParameter()
        {
            //plan_id = Request.QueryString["paint"];
           //tablename = "VIEW_TM_PAINTSCHEME";
            tablename = "VIEW_TM_PAINTSCHEME AS A left join  TBMP_MANUTSASSGN AS B on A.PS_ENGID=B.MTA_ID ";
            sqlText = "A.PS_STATE='8'";
            if (cb_myjob.Checked)
            {
                sqlText += " and MTA_DUY='" + Session["UserName"].ToString() + "'";
            }
            if (txt_psid.Text != "")
            {
                sqlText += "and PS_ID='" + txt_psid.Text.ToString() + "'";
            }
            if (txt_pjname.Text != "")
            {
                sqlText += "and CM_PROJ like '%" + txt_pjname.Text.ToString() + "%'";
            }
            //fields = "PS_ID,PS_SUBMITTM,PS_ADATE,PS_STATE,PS_ID+'.'+PS_STATE as PS_NO ";
            fields = "A.PS_ID ,A.CM_PROJ as PS_PJNAME,A.TSA_ENGNAME as PS_ENGNAME,";
            fields += "A.PS_SUBMITNM as SUBMITNM,A.PS_SUBMITTM as SUBMITTM,A.PS_ADATE ,";
            fields += "A.PS_STATE as STATE,'' as MAP,'' as SHAPE,B.MTA_DUY,A.PS_LOOKSTATUS";
        }
        //初始化分页信息
        private void InitPager()
        {
            InitParameter();
            pager.TableName = tablename;
            pager.PrimaryKey = "PS_ID";
            pager.ShowFields = fields;
            pager.OrderField = "PS_ADATE";
            pager.StrWhere = sqlText;
            pager.OrderType = 1;//按下发时间降序排列
            pager.PageSize = 10;
        }
        //初始化信息（给页面控件赋值）
        private void InitInfo()
        {
            //绑定数据
            GetTechRequireData();
        }
        protected void gridview_OnDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                string lookstatus = ((Label)gvr.FindControl("PS_LOOKSTATUS")).Text;
                if (lookstatus == "1")
                {
                    gvr.Cells[0].BackColor = System.Drawing.Color.LightGreen;
                }
            
            }
        
        
        }
        protected void GetTechRequireData()
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
        }

        #region 导出功能

        protected void btnexport_Click(object sender, EventArgs e) //导出
        {
            int i = 0;
            string psid = "";
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                CheckBox cb = (CheckBox)gvr.FindControl("cb_1");
                if (cb != null)//存在行
                {
                    if (cb.Checked)
                    {
                        i++;
                        psid = ((Label)gvr.FindControl("PS_ID")).Text; 
                    }
                }
            }
            if (i == 1)
            {
                string sqltext = "select PS_ENGID,PS_NAME,PS_TUHAO,PS_LEVEL,PS_MIANJI,PS_BOTSHAPE,PS_BOTHOUDU,PS_BOTYONGLIANG,PS_BOTXISHIJI,PS_MIDSHAPE,PS_MIDHOUDU,PS_MIDYONGLIANG,PS_MIDXISHIJI,PS_TOPSHAPE,PS_TOPHOUDU,PS_TOPYONGLIANG,PS_TOPXISHIJI,PS_TOPCOLOR,PS_TOPCOLORLABEL,PS_TOTALHOUDU,PS_BEIZHU,PS_BGBEIZHU from View_TM_PAINTSCHEMEDETAIL where PS_PID='" + psid + "'";

                ExportDataItem(sqltext, psid);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择需要导出制作明细的批号！');", true);
            }

        }

        private void ExportDataItem(string sqltext, string lotnum)
        {
            //string revtable = "View_TM_PAINTSCHEMEDETAIL";

            string filename = "油漆涂装方案.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("油漆涂装方案.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count == 0)
                {
                    System.Web.HttpContext.Current.Response.Write("<script type='text/javascript' language='javascript'>alert('没有可导出数据！！！');window.close();</script>");
                    return;
                }
                string basic_sql = "select PS_SUBMITNM,PS_REVIEWANM,PS_REVIEWBNM,CM_PROJ,PS_PJID,TSA_ENGNAME,PS_PAINTBRAND from VIEW_TM_PAINTSCHEME  where PS_ID='" + lotnum + "'";
                System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(basic_sql);
                IRow row3 = sheet0.GetRow(3);
                row3.GetCell(17).SetCellValue(dt1.Rows[0]["PS_SUBMITNM"].ToString());
                IRow row4 = sheet0.GetRow(4);
                
                row4.GetCell(17).SetCellValue(dt1.Rows[0]["PS_REVIEWANM"].ToString());
                IRow row5 = sheet0.GetRow(5);
                row5.GetCell(17).SetCellValue(dt1.Rows[0]["PS_REVIEWBNM"].ToString());
                IRow row6 = sheet0.GetRow(6);
                row6.GetCell(2).SetCellValue(dt1.Rows[0]["CM_PROJ"].ToString());
                row6.GetCell(8).SetCellValue(dt1.Rows[0]["PS_PJID"].ToString());
                row6.GetCell(13).SetCellValue(dt1.Rows[0]["TSA_ENGNAME"].ToString());
                row6.GetCell(17).SetCellValue(dt1.Rows[0]["PS_PAINTBRAND"].ToString());
               
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 9);
                    for (int j = 0; j < 22; j++)
                    {
                        row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }

                sheet0.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        #endregion

        protected void cb_myjob_occ(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            GetTechRequireData();

        }
        protected void lbtn_look_oc(object sender, EventArgs e)
        {
            string psid = ((Button)sender).CommandArgument;

            string sqltxt = "select A.*,B.MTA_DUY from VIEW_TM_PAINTSCHEME AS A left join  TBMP_MANUTSASSGN AS B on A.PS_ENGID=B.MTA_ID where PS_ID='" + psid + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltxt);
                string mtaduy = dt.Rows[0]["MTA_DUY"].ToString();
                string username = Session["UserName"].ToString();
                if (mtaduy == username)
                {
                    //string sqltext = "update TBPM_PAINTSCHEME set PS_LOOKSTATUS='1' where PS_ID='" + psid + "'";
                    //DBCallCommon.ExeSqlText(sqltext);
                    //Response.Redirect("PM_YOUQI_datail.aspx?action=look&id=" + psid + "");
                    Response.Write("<script>window.open('PM_YOUQI_datail.aspx?action=look&id=" + psid + "','_blank')</script>");
                }
                else
                {
                    //Response.Redirect("PM_YOUQI_datail.aspx?action=look&id=" + psid + "");
                    Response.Write("<script>window.open('PM_YOUQI_datail.aspx?action=look&id=" + psid + "','_blank')</script>");

                }
        }
        protected void btn_query_OnClick(object sender ,EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            GetTechRequireData();
        }
    }
}
