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
using ZCZJ_DPF;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_GDGZ : BasicPage
    {

        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserDeptID"].ToString().Trim() != "02" && Session["UserDeptID"].ToString().Trim() != "01")
                {
                    HyperLinkAdd.Visible = false;
                    Button1.Visible = false;
                }
                if (Session["UserName"].ToString().Trim() == "管理员")
                {
                    FileUpload1.Visible = true;
                    btnDaoRu.Visible = true;
                }
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindrpt();
            }
            CheckUser(ControlFinder);
            this.InitVar();

        }

        #region 分页
        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            txtName.Text = "";
            txtgh.Text = "";
            BindbmData();
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
            pager_org.TableName = "View_OM_GDGZ";
            pager_org.PrimaryKey = "ST_ID";
            pager_org.ShowFields = "ST_ID,ST_DEPID,Person_GH,ST_NAME,DEP_NAME,(isnull(GDGZ,0)-isnull(tzedu,0)) as lastgdgz,tzedu,GDGZ,ST_SEQUEN,XGRST_NAME,XGTIME,NOTE,ST_PD";
            pager_org.OrderField = "DEP_NAME";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 200;
        }
        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            string sql = "1=1";
            if (txtName.Text != "")
            {
                sql += " and ST_NAME like '%" + txtName.Text + "%'";
            }
            if (txtgh.Text != "")
            {
                sql += " and Person_GH like '%" + txtgh.Text.ToString().Trim() + "%'";
            }
            if (ddl_Depart.SelectedValue != "00")
            {
                sql += " and ST_DEPID='" + ddl_Depart.SelectedValue + "'";
            }
            if (txtgwxl.Text.Trim() != "")
            {
                sql += " and ST_SEQUEN like '%" + txtgwxl.Text.Trim() + "%'";
            }
            if (radio_zaizhi.Checked)
            {
                sql += " and ST_PD='0'";
            }
            else if (radio_lizhi.Checked)
            {
                sql += " and ST_PD='1'";
            }
            else if (radio_other.Checked)
            {
                sql += " and ST_PD not in('0','1')";
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
            CommonFun.Paging(dt, rptGDGZ, UCPaging1, palNoData);
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

        private void BindbmData()
        {

            string stId = Session["UserId"].ToString();
            System.Data.DataTable dt = DBCallCommon.GetPermeision(12, stId);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
        }
        protected void dplbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btncx_click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void radiozhistate_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            //List<string> sqltext = new List<string>();
            //sqltext.Clear();
            //foreach (RepeaterItem rptitem in rptGDGZ.Items)
            //{
            //    System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)rptitem.FindControl("cbxSelect");
            //    System.Web.UI.WebControls.Label lbstid = (System.Web.UI.WebControls.Label)rptitem.FindControl("lbstid");
            //    if (cbx.Checked == true)
            //    {
            //        string sql = "";
            //        sql = "delete from OM_GDGZ where ST_ID='" + lbstid.Text.ToString() + "'";
            //        sqltext.Add(sql);
            //    }
            //}
            //DBCallCommon.ExecuteTrans(sqltext);
            //UCPaging1.CurrentPage = 1;
            //bindrpt();

            List<string> sqltext = new List<string>();
            sqltext.Clear();
            int times = 0;
            string stid = "";
            foreach (RepeaterItem rptitem in rptGDGZ.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)rptitem.FindControl("cbxSelect");
                System.Web.UI.WebControls.Label lbstid = (System.Web.UI.WebControls.Label)rptitem.FindControl("lbstid");
                if (cbx.Checked == true)
                {
                    stid += lbstid.Text.ToString() + ",";
                    times++;
                }
            }
            if (times == 0)
            {
                Response.Write("<script>alert('请勾选要删除的岗位人员固定工资项！')</script>");
                return;
            }
            else
            {
                Response.Write("<script>window.open('OM_GDGZSCSP_detail.aspx?action=add&id=" + stid + "','_blank','height=500px,width=1200px')</script>");
            }
        }

        protected string editDq(string stid)
        {
            return "javascript:window.showModalDialog('OM_GDGZAdd.aspx?FLAG=edit&stid=" + stid + "','','DialogWidth=1200px;DialogHeight=500px')";
        }
        protected string CKDq(string st_id)
        {
            return "javascript:window.showModalDialog('OM_GDGZJLCK.aspx?id=" + st_id + "','','DialogWidth=1200px;DialogHeight=750px')";
        }




        protected void rptGDGZ_itemdatabind(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    System.Web.UI.WebControls.Label lbstid = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbstid");
            //    System.Web.UI.WebControls.Label lbtzqgdgz = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbtzqgdgz");
            //    string sqltext = "select GDGZ from OM_GDGZrecord where ID in(select max(ID) from OM_GDGZrecord where ST_ID='" + lbstid.Text.Trim() + "' and GDGZ!=(select GDGZ from OM_GDGZ where ST_ID='" + lbstid.Text.Trim() + "'))";
            //    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            //    if (dt.Rows.Count > 0)
            //    {
            //        lbtzqgdgz.Text = dt.Rows[0]["GDGZ"].ToString().Trim();
            //    }
            //}
        }
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDaoRu_OnClick(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string sqlTextchk = "select * from OM_GDGZ";
            System.Data.DataTable dtchk = DBCallCommon.GetDTUsingSqlText(sqlTextchk);
            if (dtchk.Rows.Count > 0)
            {
                string sqldelete = "delete from OM_GDGZ";
                DBCallCommon.ExeSqlText(sqldelete);
            }
            string FilePath = @"E:\固定工资表\";
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            //将文件上传到服务器
            HttpPostedFile UserHPF = FileUpload1.PostedFile;
            try
            {
                string fileContentType = UserHPF.ContentType;// 获取客户端发送的文件的 MIME 内容类型   
                if (fileContentType == "application/vnd.ms-excel")
                {
                    if (UserHPF.ContentLength > 0)
                    {
                        UserHPF.SaveAs(FilePath + "//" + System.IO.Path.GetFileName(UserHPF.FileName));//将上传的文件存放在指定的文件夹中 
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('文件类型不符合要求，请您核对后重新上传！');", true);
                    return;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('文件上传过程中出现错误！');", true);
                return;
            }

            using (FileStream fs = File.OpenRead(FilePath + "//" + System.IO.Path.GetFileName(UserHPF.FileName)))
            {
                //根据文件流创建一个workbook
                IWorkbook wk = new HSSFWorkbook(fs);

                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 1; i < sheet1.LastRowNum + 1; i++)
                {
                    string sh_stid = "";
                    string strcell02 = "";
                    IRow row = sheet1.GetRow(i);
                    ICell cell02 = row.GetCell(2);
                    try
                    {
                        strcell02 = cell02.ToString().Trim();
                    }
                    catch
                    {
                        strcell02 = "";
                    }
                    if (strcell02 != "")
                    {
                        string sqltext = "select ST_ID,ST_WORKNO from TBDS_STAFFINFO where ST_NAME='" + strcell02 + "'";
                        System.Data.DataTable dttext = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dttext.Rows.Count > 0)
                        {
                            sh_stid = dttext.Rows[0]["ST_ID"].ToString().Trim();
                            ICell cell03 = row.GetCell(3);
                            string strcell03 = "";
                            try
                            {
                                strcell03 = cell03.NumericCellValue.ToString().Trim();
                            }
                            catch
                            {
                                strcell03 = cell03.ToString().Trim();
                            }
                            string sqlinsert = "insert into OM_GDGZ(Person_GH,GDGZ,DATE,ST_ID) values('" + dttext.Rows[0]["ST_WORKNO"].ToString().Trim() + "','" + CommonFun.ComTryDecimal(strcell03) + "','" + DateTime.Now.ToString("yyyy-MM-dd").Trim() + "','" + sh_stid + "')";
                            list.Add(sqlinsert);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            DBCallCommon.ExecuteTrans(list);
            foreach (string fileName in Directory.GetFiles(FilePath))//清空该文件夹下的文件
            {
                string newName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                System.IO.File.Delete(FilePath + "\\" + newName);//删除文件下储存的文件
            }
            UCPaging1.CurrentPage = 1;
            bindrpt();
            Response.Redirect(Request.Url.ToString());
        }


        #region 批量导出

        protected void btn_plexport_OnClick(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext = "select Person_GH,ST_NAME,DEP_NAME,ST_SEQUEN,(isnull(GDGZ,0)-isnull(tzedu,0)) as lastgdgz,tzedu,GDGZ,XGRST_NAME,XGTIME,NOTE from View_OM_GDGZ where " + StrWhere() + " order by Person_GH,XGTIME";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string filename = "固定工资" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("固定工资导出.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                    }

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
        #endregion
    }
}
