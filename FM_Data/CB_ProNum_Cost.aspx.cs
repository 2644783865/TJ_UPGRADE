using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ZCZJ_DPF.FM_Data
{
    public partial class CB_ProNum_Cost :BasicPage
    {
        double gzhj = 0;

        double jjfyhj = 0;
        double jgyzfyhj = 0;
        double zjrgfhj = 0;


        double bzjhj = 0;
        double hclhj = 0;
        double hsjshj = 0;
        double zjhj = 0;
        double djhj = 0;
        double zchj = 0;
        double wgjhj = 0;
        double qtclhj = 0;
        double clhj = 0;


        double gdzzfyhj = 0;
        double kbzzfyhj = 0;
        double zzfyhj = 0;
        double wxfyhj = 0;
        double cnfbhj = 0;
        double yfhj = 0;
        double fjcbhj = 0;
        double qthj = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["StrWhere"] = "CWCB_STATE='1'";
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }
            if (IsPostBack)
            {
                this.InitVar();
            }
        }

        #region  分页
        PagerQueryParam pager_org = new PagerQueryParam();

        // 初始化分布信息
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        // 分页初始化
        private void InitPager()
        {
            pager_org.TableName = "TBCB_BMCONFIRM";
            pager_org.PrimaryKey = "TASK_ID";
            pager_org.ShowFields = "TASK_ID,PRJ,ENG,CWCB_GZ,CWCB_JJFY,CWCB_JGYZ,(CWCB_JJFY+CWCB_JGYZ) as ZJRGFXJ,CWCB_WGJ,CWCB_HSJS,CWCB_HCL,CWCB_ZJ,CWCB_DJ,CWCB_ZC,CWCB_BZJ,CWCB_QTL,(CWCB_WGJ+CWCB_HSJS+CWCB_HCL+CWCB_ZJ+CWCB_DJ+CWCB_ZC+CWCB_BZJ+CWCB_QTL) as CLXJ,CWCB_GDZZ,CWCB_KBZZ,(CWCB_GDZZ+CWCB_KBZZ) as ZZFYXJ,CWCB_WXFY,CWCB_CNFB,CWCB_YF,CWCB_FJCB,CWCB_QT";
            pager_org.OrderField = "TASK_ID";
            pager_org.StrWhere = ViewState["StrWhere"].ToString();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 50;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, rptProNumCost, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }


        }
        #endregion

        protected void btnQuery_OnClick(object sender,EventArgs e)
        {
            ViewState["StrWhere"] = " TASK_ID like '%" + txtrwh.Text.Trim() + "%' and CWCB_STATE='1'";
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }



        protected void rptProNumCost_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Web.UI.WebControls.Label lbgz = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbgz");
                System.Web.UI.WebControls.Label lbjjfy = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbjjfy");
                System.Web.UI.WebControls.Label lbjgyzfy = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbjgyzfy");
                System.Web.UI.WebControls.Label lbzjrgf = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzjrgf");

                System.Web.UI.WebControls.Label lbbzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbbzj");
                System.Web.UI.WebControls.Label lbhcl = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbhcl");
                System.Web.UI.WebControls.Label lbhsjs = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbhsjs");
                System.Web.UI.WebControls.Label lbzj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzj");
                System.Web.UI.WebControls.Label lbdj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbdj");
                System.Web.UI.WebControls.Label lbzc = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzc");
                System.Web.UI.WebControls.Label lbwgj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbwgj");
                System.Web.UI.WebControls.Label lbqtcl = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqtcl");
                System.Web.UI.WebControls.Label lbclxj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbclxj");

                System.Web.UI.WebControls.Label lbgdzzfy = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbgdzzfy");
                System.Web.UI.WebControls.Label lbkbzzfy = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbkbzzfy");
                System.Web.UI.WebControls.Label lbzzfyxj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzzfyxj");

                System.Web.UI.WebControls.Label lbwxfy = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbwxfy");
                System.Web.UI.WebControls.Label lbcnfb = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbcnfb");
                System.Web.UI.WebControls.Label lbyf = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbyf");
                System.Web.UI.WebControls.Label lbfjcb = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbfjcb");
                System.Web.UI.WebControls.Label lbqt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqt");
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select isnull(CAST(sum(CWCB_GZ) AS FLOAT),0) as GZHJ,isnull(CAST(sum(CWCB_JJFY) AS FLOAT),0) as JJFYHJ,isnull(CAST(sum(CWCB_JGYZ) AS FLOAT),0) as JGYZFYHJ,isnull(CAST(sum(CWCB_JJFY+CWCB_JGYZ) AS FLOAT),0) as ZJRGFHJ,isnull(CAST(sum(CWCB_BZJ) AS FLOAT),0) as BZJHJ,isnull(CAST(sum(CWCB_HCL) AS FLOAT),0) as HCLHJ,isnull(CAST(sum(CWCB_HSJS) AS FLOAT),0) as HSJSHJ,isnull(CAST(sum(CWCB_ZJ) AS FLOAT),0) as ZJHJ,isnull(CAST(sum(CWCB_DJ) AS FLOAT),0) as DJHJ,isnull(CAST(sum(CWCB_ZC) AS FLOAT),0) as ZCHJ,isnull(CAST(sum(CWCB_WGJ) AS FLOAT),0) as WGJHJ,isnull(CAST(sum(CWCB_QTL) AS FLOAT),0) as QTCLHJ,isnull(CAST(sum(CWCB_WGJ+CWCB_HSJS+CWCB_HCL+CWCB_ZJ+CWCB_DJ+CWCB_ZC+CWCB_BZJ+CWCB_QTL) AS FLOAT),0) as CLHJ,isnull(CAST(sum(CWCB_GDZZ) AS FLOAT),0) as GDZZFYHJ,isnull(CAST(sum(CWCB_KBZZ) AS FLOAT),0) as KBZZFYHJ,isnull(CAST(sum(CWCB_GDZZ+CWCB_KBZZ) AS FLOAT),0) as ZZFYHJ,isnull(CAST(sum(CWCB_WXFY) AS FLOAT),0) as WXFYHJ,isnull(CAST(sum(CWCB_CNFB) AS FLOAT),0) as CNFBHJ,isnull(CAST(sum(CWCB_YF) AS FLOAT),0) as YFHJ,isnull(CAST(sum(CWCB_FJCB) AS FLOAT),0) as FJCBHJ,isnull(CAST(sum(CWCB_QT) AS FLOAT),0) as QTHJ from TBCB_BMCONFIRM where " + ViewState["StrWhere"].ToString();

                SqlDataReader drhj = DBCallCommon.GetDRUsingSqlText(sqlhj);

                if (drhj.Read())
                {

                    gzhj = Convert.ToDouble(drhj["GZHJ"]);
                    jjfyhj = Convert.ToDouble(drhj["JJFYHJ"]);
                    jgyzfyhj = Convert.ToDouble(drhj["JGYZFYHJ"]);
                    zjrgfhj = Convert.ToDouble(drhj["ZJRGFHJ"]);

                    bzjhj = Convert.ToDouble(drhj["BZJHJ"]);
                    hclhj = Convert.ToDouble(drhj["HCLHJ"]);
                    hsjshj = Convert.ToDouble(drhj["HSJSHJ"]);
                    zjhj = Convert.ToDouble(drhj["ZJHJ"]);
                    djhj = Convert.ToDouble(drhj["DJHJ"]);
                    zchj = Convert.ToDouble(drhj["ZCHJ"]);
                    wgjhj = Convert.ToDouble(drhj["WGJHJ"]);
                    qtclhj = Convert.ToDouble(drhj["QTCLHJ"]);
                    clhj = Convert.ToDouble(drhj["CLHJ"]);
                    gdzzfyhj = Convert.ToDouble(drhj["GDZZFYHJ"]);
                    kbzzfyhj = Convert.ToDouble(drhj["KBZZFYHJ"]);
                    zzfyhj = Convert.ToDouble(drhj["ZZFYHJ"]);

                    wxfyhj = Convert.ToDouble(drhj["WXFYHJ"]);
                    cnfbhj = Convert.ToDouble(drhj["CNFBHJ"]);
                    yfhj = Convert.ToDouble(drhj["YFHJ"]);
                    fjcbhj = Convert.ToDouble(drhj["FJCBHJ"]);
                    qthj = Convert.ToDouble(drhj["QTHJ"]);
                }
                drhj.Close();



                System.Web.UI.WebControls.Label lbgzhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbgzhj");
                System.Web.UI.WebControls.Label lbjjfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbjjfyhj");
                System.Web.UI.WebControls.Label lbjgyzfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbjgyzfyhj");
                System.Web.UI.WebControls.Label lbzjrgfhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzjrgfhj");

                System.Web.UI.WebControls.Label lbbzjhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbbzjhj");
                System.Web.UI.WebControls.Label lbhclhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbhclhj");
                System.Web.UI.WebControls.Label lbhsjshj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbhsjshj");
                System.Web.UI.WebControls.Label lbzjhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzjhj");
                System.Web.UI.WebControls.Label lbdjhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbdjhj");
                System.Web.UI.WebControls.Label lbzchj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzchj");
                System.Web.UI.WebControls.Label lbwgjhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbwgjhj");
                System.Web.UI.WebControls.Label lbqtclhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqtclhj");
                System.Web.UI.WebControls.Label lbclhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbclhj");

                System.Web.UI.WebControls.Label lbgdzzfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbgdzzfyhj");
                System.Web.UI.WebControls.Label lbkbzzfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbkbzzfyhj");
                System.Web.UI.WebControls.Label lbzzfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbzzfyhj");

                System.Web.UI.WebControls.Label lbwxfyhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbwxfyhj");
                System.Web.UI.WebControls.Label lbcnfbhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbcnfbhj");
                System.Web.UI.WebControls.Label lbyfhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbyfhj");
                System.Web.UI.WebControls.Label lbfjcbhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbfjcbhj");
                System.Web.UI.WebControls.Label lbqthj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbqthj");

                lbgzhj.Text = gzhj.ToString();
                lbjjfyhj.Text = jjfyhj.ToString();
                lbjgyzfyhj.Text = jgyzfyhj.ToString();
                lbzjrgfhj.Text = zjrgfhj.ToString();

                lbbzjhj.Text = bzjhj.ToString();
                lbhclhj.Text = hclhj.ToString();
                lbhsjshj.Text = hsjshj.ToString();
                lbzjhj.Text = zjhj.ToString();
                lbdjhj.Text = djhj.ToString();
                lbzchj.Text = zchj.ToString();
                lbwgjhj.Text = wgjhj.ToString();
                lbqtclhj.Text = qtclhj.ToString();
                lbclhj.Text = clhj.ToString();

                lbgdzzfyhj.Text = gdzzfyhj.ToString();
                lbkbzzfyhj.Text = kbzzfyhj.ToString();
                lbzzfyhj.Text = zzfyhj.ToString();

                lbwxfyhj.Text = wxfyhj.ToString();
                lbcnfbhj.Text = cnfbhj.ToString();
                lbyfhj.Text = yfhj.ToString();
                lbfjcbhj.Text = fjcbhj.ToString();
                lbqthj.Text = qthj.ToString();

            }
        }






        //#region 导出数据


        //private int ifselect()
        //{
        //    int flag = 0;
        //    int i = 0;//是否选择数据
        //    foreach (RepeaterItem Reitem in rptProNumCost.Items)
        //    {
        //        System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("cbxSelect") as System.Web.UI.WebControls.CheckBox;//定义checkbox
        //        if (cbx.Checked)
        //        {
        //            i++;
        //        }
        //    }
        //    if (i == 0)//未选择数据
        //    {
        //        flag = 1;
        //    }
        //    else
        //    {
        //        flag = 0;
        //    }
        //    return flag;
        //}
        //protected void btn_export_Click(object sender, EventArgs e)
        //{
        //    int flag = ifselect();
        //    if (flag == 0)//判断是否有勾选框被勾选
        //    {
        //        string rwhdc = "";
        //        foreach (RepeaterItem Reitem in rptProNumCost.Items)
        //        {
        //            System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("cbxSelect") as System.Web.UI.WebControls.CheckBox;//定义checkbox
        //            if (cbx.Checked)
        //            {
        //                rwhdc += "'" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("lbqjrwh")).Text.ToString() + "'" + ",";
        //            }
        //        }
        //        rwhdc = rwhdc.Substring(0, rwhdc.LastIndexOf(",")).ToString();
        //        string sqltext = "";
        //        sqltext = "select PMS_TSAID as QJTSAID,sum(ISNULL(AYTJ_GZ,0)) as QJGZ,sum(ISNULL(AYTJ_JJFY,0)) as QJJJFY,sum(ISNULL(AYTJ_JGYZFY,0)) as QJJGYZFY,(sum(ISNULL(AYTJ_JJFY,0))+sum(ISNULL(AYTJ_JGYZFY,0))) as QJZJRGFXJ,sum(ISNULL(PMS_01_01,0)) as QJBZJ,sum(ISNULL(PMS_01_05,0)) as QJHCL,sum(ISNULL(PMS_01_07,0)) as QJHSJS,sum(ISNULL(PMS_01_08,0)) as QJZJ,sum(ISNULL(PMS_01_09,0)) as QJDJ,sum(ISNULL(PMS_01_10,0)) as QJZC,sum(ISNULL(PMS_01_11,0)) as QJWGJ,sum(PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_06+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_15+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) as QJQTCL,sum(PMS_01_01+PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_05+PMS_01_06+PMS_01_07+PMS_01_08+PMS_01_09+PMS_01_10+PMS_01_11+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_15+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) as QJCLXJ,sum(ISNULL(AYTJ_GDZZFY,0)) as QJGDZZFY,sum(ISNULL(AYTJ_KBZZFY,0)) as QJKBZZFY,(sum(ISNULL(AYTJ_GDZZFY,0))+sum(ISNULL(AYTJ_KBZZFY,0))) as QJZZFYXJ,sum(ISNULL(AYTJ_WXFY,0)) as QJWXFY,sum(ISNULL(AYTJ_CNFB,0)) as QJCNFB,sum(ISNULL(AYTJ_YF,0)) as QJYF,sum(ISNULL(AYTJ_FJCB,0)) as QJFJCB,sum(ISNULL(AYTJ_QT,0)) as QJQT from VIEW_FM_AYTJ where (PMS_YEAR+'-'+PMS_MONTH)>='" + startyearmonth + "' and (PMS_YEAR+'-'+PMS_MONTH)<='" + endyearmonth + "' and PMS_TSAID in(" + rwhdc + ") group by PMS_TSAID";
        //        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
        //        ExportDataItem(dt);
        //    }
        //    else if (flag == 1)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要导出的数据！！！');", true);
        //    }
        //}


        //private void ExportDataItem(System.Data.DataTable dt)
        //{

        //    string filename = "任务号按期间统计" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
        //    HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        //    HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
        //    HttpContext.Current.Response.Clear();
        //    //1.读取Excel到FileStream 
        //    using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("任务按期间统计.xls")))
        //    {
        //        IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
        //        ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet


        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            IRow row = sheet0.CreateRow(i + 1);
        //            for (int j = 0; j < dt.Columns.Count; j++)
        //            {
        //                row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
        //            }

        //        }

        //        for (int i = 0; i <= dt.Columns.Count; i++)
        //        {
        //            sheet0.AutoSizeColumn(i);
        //        }

        //        sheet0.ForceFormulaRecalculation = true;
        //        MemoryStream file = new MemoryStream();
        //        wk.Write(file);
        //        HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
        //        HttpContext.Current.Response.End();
        //    }
        //}
        //#endregion


        //#region 批量导出

        //protected void btn_plexport_Click(object sender, EventArgs e)
        //{
        //    string sqltext = "";
        //    sqltext = "select PMS_TSAID as QJTSAID,sum(ISNULL(AYTJ_GZ,0)) as QJGZ,ISNULL(AYTJ_JJFY,0) as QJJJFY,ISNULL(AYTJ_JGYZFY,0) as AYTJ_QJJGYZFY,(ISNULL(AYTJ_JJFY,0)+ISNULL(AYTJ_JGYZFY,0)) as QJZJRGFXJ,sum(ISNULL(PMS_01_01,0)) as QJBZJ,sum(ISNULL(PMS_01_05,0)) as QJHCL,sum(ISNULL(PMS_01_07,0)) as QJHSJS,sum(ISNULL(PMS_01_08,0)) as QJZJ,sum(ISNULL(PMS_01_09,0)) as QJDJ,sum(ISNULL(PMS_01_10,0)) as QJZC,sum(ISNULL(PMS_01_11,0)) as QJWGJ,sum(PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_06+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_15+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) as QJQTCL,sum(PMS_01_01+PMS_01_02+PMS_01_03+PMS_01_04+PMS_01_05+PMS_01_06+PMS_01_07+PMS_01_08+PMS_01_09+PMS_01_10+PMS_01_11+PMS_01_12+PMS_01_13+PMS_01_14+PMS_01_15+PMS_01_16+PMS_01_17+PMS_01_18+PMS_02_01+PMS_02_02+PMS_02_03+PMS_02_04+PMS_02_05+PMS_02_06+PMS_02_07+PMS_02_08+PMS_02_09) as QJCLXJ,sum(ISNULL(AYTJ_GDZZFY,0)) as QJGDZZFY,sum(ISNULL(AYTJ_KBZZFY,0)) as QJKBZZFY,(sum(ISNULL(AYTJ_GDZZFY,0))+sum(ISNULL(AYTJ_KBZZFY,0))) as QJZZFYXJ,sum(ISNULL(AYTJ_WXFY,0)) as QJWXFY,sum(ISNULL(AYTJ_CNFB,0)) as QJCNFB,sum(ISNULL(AYTJ_YF,0)) as QJYF,sum(ISNULL(AYTJ_FJCB,0)) as QJFJCB,sum(ISNULL(AYTJ_QT,0)) as QJQT from VIEW_FM_AYTJ where (PMS_YEAR+'-'+PMS_MONTH)>='" + startyearmonth + "' and (PMS_YEAR+'-'+PMS_MONTH)<='" + endyearmonth + "' group by PMS_TSAID";
        //    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
        //    string filename = "任务号按期间统计" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
        //    HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        //    HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
        //    HttpContext.Current.Response.Clear();
        //    //1.读取Excel到FileStream 
        //    using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("任务按期间统计.xls")))
        //    {
        //        IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
        //        ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet


        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            IRow row = sheet0.CreateRow(i + 2);
        //            for (int j = 0; j < dt.Columns.Count; j++)
        //            {
        //                row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
        //            }

        //        }
        //        for (int i = 0; i <= dt.Columns.Count; i++)
        //        {
        //            sheet0.AutoSizeColumn(i);
        //        }

        //        sheet0.ForceFormulaRecalculation = true;
        //        MemoryStream file = new MemoryStream();
        //        wk.Write(file);
        //        HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
        //        HttpContext.Current.Response.End();
        //    }
        //}
        //#endregion
        //private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet) //输出Excel文件并退出
        //{
        //    try
        //    {

        //        workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //        workbook.Close(Type.Missing, Type.Missing, Type.Missing);
        //        m_xlApp.Workbooks.Close();
        //        m_xlApp.Quit();
        //        m_xlApp.Application.Quit();
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);
        //        wksheet = null;
        //        workbook = null;
        //        m_xlApp = null;
        //        GC.Collect();
        //        //下载
        //        System.IO.FileInfo path = new System.IO.FileInfo(filename);
        //        //同步，异步都支持
        //        HttpResponse contextResponse = HttpContext.Current.Response;
        //        contextResponse.Redirect(string.Format("~/FM_Data/ExportFile/{0}", path.Name), false);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
    }
}
