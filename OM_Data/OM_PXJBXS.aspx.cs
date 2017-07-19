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
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel; 

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_PXJBXS : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                binddepartment();
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                InitVar();
                bindrpt();
            }
            InitVar();
        }
        #region 分页
        /// <summary>
        /// 初始化页面
        /// </summary>

        private void InitPage()
        {

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
            //pager_org.TableName = "OM_PXJBXS as d left join (select DA_CXR,DA_CXRID,sum(convert(float,PX_SJXS)) as SJXS from OM_PXDA as a left join OM_PXJH_SQ as b on a.DA_XMID=b.PX_ID left join dbo.View_TBDS_STAFFINFO as c on a.DA_CXRID = c.ST_ID group by DA_CXR,DA_CXRID) as e on d.XS_STID=e.DA_CXRID";
            pager_org.TableName = "(OM_PXJBXS as d left join (select DA_CXR,DA_CXRID,sum(convert(float,PX_SJXS)) as SJXS from OM_PXDA as a left join OM_PXJH_SQ as b on a.DA_XMID=b.PX_ID left join dbo.View_TBDS_STAFFINFO as c on a.DA_CXRID = c.ST_ID group by DA_CXR,DA_CXRID) as e on d.XS_STID=e.DA_CXRID ) left join TBDS_STAFFINFO as g on d.XS_STID=g.ST_ID";
            pager_org.PrimaryKey = "XS_ID";
            pager_org.ShowFields = "*";
            pager_org.OrderField = "XS_BMID";
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
            if (ddldeparment.SelectedValue != "0")
            {
                sql += " and XS_BM='" + ddldeparment.SelectedValue + "'";
            }
            if (txtName.Text != "")
            {
                sql += "and XS_NAME='" + txtName.Text.ToString().Trim() + "'";
            }
            if (ddlgangwei.SelectedValue != "0")
            {
                sql += " and ST_SEQUEN='" + ddlgangwei.SelectedValue + "'";
            }
            return sql;
        }
        /// <summary>
        /// 换页事件
        /// </summary>
        private void Pager_PageChanged(int pageNumber)
        {
            bindrpt();
        }

        private void bindrpt()
        {
            InitPager();
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rpt1, UCPaging1, palNoData);
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
        #endregion

        protected void jbxs_gx(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (RepeaterItem item in rpt1.Items)
            {
                if (((CheckBox)item.FindControl("cbxNumber")).Checked)
                {
                    string XS_ID = ((HiddenField)item.FindControl("XS_ID")).Value;
                    string XS_JBXS = txtJBXS_GX.Text.Trim();
                    string sql = "";
                    sql = "update OM_PXJBXS set XS_JBXS='" + XS_JBXS + "' where XS_ID='" + XS_ID + "'";
                    list.Add(sql);
                }
            }
            if (list.Count == 0)
            {
                Response.Write("<script type='text/javascript'>alert('请勾选序号！！！')</script>"); 
                return;
            }
            else
            {
                DBCallCommon.ExecuteTrans(list);
            }
           //if(((CheckBox)item.FindControl("quanxuan")).Checked)
           // {
           //     string sqltxt = "";
           //     sqltxt = "select * from OM_PXJBXS where " + StrWhere();
           //     DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltxt);
           //     if (dt.Rows.Count > 0)
           //     {
           //         for (int i = 0; i < dt.Rows.Count; i++)
           //         {
           //             string sql = "";
           //             sql = "update OM_PXJBXS set XS_JBXS='" + XS_JBXS + "' where XS_ID='" + XS_ID + "'";
           //         }
           //     }
               

           // }
            bindrpt();

        }

        protected void binddepartment()
        {
            string sql = "SELECT DISTINCT DEP_NAME,DEP_CODE  FROM TBDS_DEPINFO WHERE len(DEP_CODE)=2";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddldeparment.DataValueField = "DEP_NAME";
            ddldeparment.DataTextField = "DEP_NAME";
            ddldeparment.DataSource = dt;
            ddldeparment.DataBind();

            ListItem item = new ListItem("--请选择--", "0");
            ddldeparment.Items.Insert(0, item);

            string sqltxt = "SELECT DISTINCT ST_SEQUEN  FROM TBDS_STAFFINFO where (ST_SEQUEN<>'' and ST_SEQUEN is not null) ";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltxt);
            ddlgangwei.DataValueField = "ST_SEQUEN";
            ddlgangwei.DataTextField = "ST_SEQUEN";
            ddlgangwei.DataSource = dt1;
            ddlgangwei.DataBind();
            ListItem item1 = new ListItem("--请选择--", "0");
            ddlgangwei.Items.Insert(0, item1);

        }
        protected void ddldeparment_selectchanged(object sender, EventArgs e)
        {
            bindrpt();
        }
        protected void ddlgangwei_selectchanged(object sender, EventArgs e)
        {
            bindrpt();
        }
        protected void cxname_click(object sender, EventArgs e)
        {
            bindrpt();
        }
        protected void btndaochu_click(object sender, EventArgs e)
        {
            string sqltext = ""; ;

            sqltext += "select d.*,e.* from OM_PXJBXS as d left join (select DA_CXR,DA_CXRID,sum(convert(float,PX_SJXS)) as SJXS from OM_PXDA as a left join OM_PXJH_SQ as b on a.DA_XMID=b.PX_ID left join dbo.View_TBDS_STAFFINFO as c on a.DA_CXRID = c.ST_ID group by DA_CXR,DA_CXRID) as e on d.XS_STID=e.DA_CXRID ";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string filename = "培训学时统计" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("培训学时统计.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));
                    row.CreateCell(1).SetCellValue("" + dt.Rows[i]["XS_NAME"].ToString());
                    row.CreateCell(2).SetCellValue("" + dt.Rows[i]["XS_BM"].ToString());
                    row.CreateCell(3).SetCellValue("" + dt.Rows[i]["XS_JBXS"].ToString());
                    row.CreateCell(4).SetCellValue("" + dt.Rows[i]["SJXS"].ToString());

                }

                for (int i = 0; i <= dt.Columns.Count; i++)
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

        protected void rpt1_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
            {
                return;
            }
            Label a = (Label)e.Item.FindControl("lbsjxs");
            Label b = (Label)e.Item.FindControl("lbJBXS");
            double sjxs = CommonFun.ComTryDouble(a.Text.ToString());
            double jbxs = CommonFun.ComTryDouble(b.Text.ToString());
            double c = 100*sjxs/jbxs;
            ((Label)e.Item.FindControl("lbwcl")).Text = c.ToString("0.00")+"%";
        }
    }
}
