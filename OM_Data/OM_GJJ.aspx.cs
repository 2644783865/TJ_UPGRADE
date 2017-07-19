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
    public partial class OM_GJJ : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (Session["UserDeptID"].ToString().Trim() != "02" && Session["UserDeptID"].ToString().Trim() != "01")
                {
                    btnSave.Visible = false;
                    btnDelete.Visible = false;
                    FileUpload1.Visible = false;
                    btnimport.Visible = false;
                    btnexport.Visible = false;
                    hlUpdate.Visible = false;
                    hlBLXGJL.Visible = false;
                }
                BindbmData();
                this.BindYearMoth(dplYear, dplMoth);
                
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                InitVar();
                bindGrid();
            }
            CheckUser(ControlFinder);
            this.InitVar();
        }

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitPage()
        {
            txtname.Text = "";
            //显示当月
            dplYear.ClearSelection();
            foreach (ListItem li in dplYear.Items)
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }

            dplMoth.ClearSelection();
            string month = (DateTime.Now.Month - 1).ToString();
            if (DateTime.Now.Month < 10 || DateTime.Now.Month == 10)
            {
                month = "0" + month;
            }
            foreach (ListItem li in dplMoth.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
        }

        /// <summary>
        /// 绑定年月
        /// </summary>
        /// <param name="dpl_Year"></param>
        /// <param name="dpl_Month"></param>
        private void BindYearMoth(DropDownList dpl_Year, DropDownList dpl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-(i-2)).Year.ToString(), DateTime.Now.AddYears(-(i-2)).Year.ToString()));
            }
            dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                dpl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            dpl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }


        private void BindbmData()
        {

            string stId = Session["UserId"].ToString();
            System.Data.DataTable dt = DBCallCommon.GetPermeision(15, stId);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
        }

        #region
        PagerQueryParam pager_org = new PagerQueryParam();
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
        private void InitPager()
        {
            pager_org.TableName = "View_OM_GJJ";
            pager_org.PrimaryKey = "GJ_STID";
            pager_org.ShowFields = "*,(GJ_DW+GJ_GR) as GJ_HJ,(GJ_DWB+GJ_GRB)as GJ_HJB ";
            pager_org.OrderField = "DEP_NAME,GJ_STID,GJ_DATE";
            pager_org.StrWhere = Creatconstr();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 30;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_org);
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


        private string Creatconstr()
        {
            string sqlwhere = "1=1 and symbol is null";
            string sqltotalnum = "select count(*) as totalnum from View_OM_GJJ where ";
            if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
            {
                sqlwhere += " and GJ_DATE='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "' ";
            }
            else if (dplYear.SelectedIndex == 0 && dplMoth.SelectedIndex != 0)
            {
                sqlwhere += " and GJ_DATE like '%-" + dplMoth.SelectedValue.ToString().Trim() + "' ";
            }
            else if (dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex == 0)
            {
                sqlwhere += " and GJ_DATE like '" + dplYear.SelectedValue.ToString().Trim() + "-%' ";
            }
            else
            {
                sqlwhere += " and GJ_DATE like '%%' ";
            }
            if (txtname.Text != "")
            {
                sqlwhere += " and (ST_NAME like '%" + txtname.Text.ToString().Trim() + "%' or ST_WORKNO like '%" + txtname.Text.ToString().Trim() + "%') ";
            }
            if (ddl_Depart.SelectedValue != "00")
            {
                sqlwhere += " and ST_DEPID='" + ddl_Depart.SelectedValue + "'";
            }
            if (tb_CXstarttime.Text.Trim() != "" && tb_CXendtime.Text.Trim() != "")
            {
                sqlwhere += " and GJ_DATE>='" + tb_CXstarttime.Text.Trim() + "' and GJ_DATE<='" + tb_CXendtime.Text.Trim() + "'";
            }
            sqltotalnum = sqltotalnum + sqlwhere;
            System.Data.DataTable dtnum = DBCallCommon.GetDTUsingSqlText(sqltotalnum);
            lbtotalnum.Text = dtnum.Rows[0]["totalnum"].ToString().Trim();
            return sqlwhere;
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> sql_list = new List<string>();
            string sqlifsc = "select * from OM_GZHSB where GZ_YEARMONTH='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "' and OM_GZSCBZ='1'";
            System.Data.DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifsc);
            if (dtifsc.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月已生成工资，不能修改！！！');", true);
                return;
            }
            if (dplYear.SelectedIndex == 0 || dplMoth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择修改数据的年月份！！！');", true);
                return;
            }
            int k = 0;//记录选中的行数
            for (int j = 0; j < rptProNumCost.Items.Count; j++)
            {
                if (((System.Web.UI.WebControls.CheckBox)rptProNumCost.Items[j].FindControl("CKBOX_SELECT")).Checked)
                {
                    k++;
                }
            }
            if (k > 0 && dplYear.SelectedIndex != 0 && dplMoth.SelectedIndex != 0)
            {
                for (int i = 0; i < rptProNumCost.Items.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)rptProNumCost.Items[i].FindControl("CKBOX_SELECT")).Checked)
                    {
                        string stid = ((System.Web.UI.WebControls.Label)rptProNumCost.Items[i].FindControl("lb_stid")).Text.Trim();

                        string tb_jcjs = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_jcjs")).Text.Trim();//缴存基数
                        string tb_dw = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_dw")).Text.Trim();//单位
                        string tb_gr = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_gr")).Text.Trim();//个人
                        string tb_dwb = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_dwb")).Text.Trim();//单位补缴
                        string tb_grb = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_grb")).Text.Trim();//个人补缴
                        string tb_bz = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("tb_bz")).Text.Trim();//备注

                        string str2 = "update OM_GJJ set GJ_JCJS=" + CommonFun.ComTryDecimal(tb_jcjs) + ",GJ_DW=" + CommonFun.ComTryDecimal(tb_dw) + ",GJ_GR=" + CommonFun.ComTryDecimal(tb_gr) + ",";
                        str2 += " GJ_DWB=" + CommonFun.ComTryDecimal(tb_dwb) + ",GJ_GRB=" + CommonFun.ComTryDecimal(tb_grb) + ", ";
                        str2 += " GJ_BZ='" + tb_bz + "' ";
                        str2 += " where GJ_DATE='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "' AND GJ_STID='" + stid + "' and symbol is null";
                        sql_list.Add(str2);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要修改的数据！！！');", true);
                return;
            }

            //更新
            
            DBCallCommon.ExecuteTrans(sql_list);
            
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据已保存！！！');", true);
            sql_list.Clear();
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }




        private string existif()
        {
            string strexist = "N";
            string sqlText = "select * from OM_GJJ where GJ_DATE>='" + tb_StartTime.Text.ToString().Trim() + "' and GJ_DATE<='" + tb_EndTime.Text.ToString().Trim() + "' and symbol is null";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt.Rows.Count > 0)
            {
                strexist = "Y";
            }
            return strexist;
        }



        protected void btnimport_Click(object sender, EventArgs e)
        {
            if (QueryButton.Text == "确认" && existif() == "Y")
            {
                QueryButton.Text = "重新导入";
                message.Visible = true;
                message.Text = "提示：该月数据已存在,覆盖原数据请点击'重新导入'!";
                ModalPopupExtenderSearch.Show();
                return;
            }
            List<string> list = new List<string>();
            int num = 0;
            string strweidaoru = "";
            string sqlifsc = "select * from OM_GZHSB where GZ_YEARMONTH>='" + tb_StartTime.Text.ToString().Trim() + "' and GZ_YEARMONTH<='" + tb_EndTime.Text.ToString().Trim() + "' and OM_GZSCBZ='1'";
            System.Data.DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifsc);
            if (dtifsc.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('有月份已生成工资，不能导入！！！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }
            if (tb_StartTime.Text.ToString().Trim() != "" && tb_EndTime.Text.ToString().Trim() != "")
            {
                string sqlTextchk = "select * from OM_GJJ where GJ_DATE>='" + tb_StartTime.Text.ToString().Trim() + "' and GJ_DATE<='" + tb_EndTime.Text.ToString().Trim() + "' and symbol is null";
                System.Data.DataTable dtchk = DBCallCommon.GetDTUsingSqlText(sqlTextchk);
                if (dtchk.Rows.Count > 0)
                {
                    string sqldelete = "delete from OM_GJJ where GJ_DATE>='" + tb_StartTime.Text.ToString().Trim() + "' and GJ_DATE<='" + tb_EndTime.Text.ToString().Trim() + "' and symbol is null";
                    DBCallCommon.ExeSqlText(sqldelete);
                }
            }
            string FilePath = @"E:\公积金表\";
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
                    ModalPopupExtenderSearch.Hide();
                    return;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('文件上传过程中出现错误！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }

            using (FileStream fs = File.OpenRead(FilePath + "//" + System.IO.Path.GetFileName(UserHPF.FileName)))
            {
//根据文件流创建一个workbook
                IWorkbook wk = new HSSFWorkbook(fs);

                ISheet sheet1 = wk.GetSheetAt(0);
                decimal jsnum = 0;
                string sqlbl = "select * from OM_GJJBL";
                System.Data.DataTable dtbl = DBCallCommon.GetDTUsingSqlText(sqlbl);
                if (dtbl.Rows.Count > 0)
                {
                    if (tb_StartTime.Text.ToString().Trim() != "" && tb_EndTime.Text.ToString().Trim() != "")
                    {
                        for (int i = 3; i < sheet1.LastRowNum + 1; i++)
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
                                ICell cell2 = row.GetCell(2);
                                string strcell2 = cell2.ToString().Trim();
                                string sqltext = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + strcell2 + "'";
                                System.Data.DataTable dttext = DBCallCommon.GetDTUsingSqlText(sqltext);
                                if (dttext.Rows.Count > 0)
                                {
                                    sh_stid = dttext.Rows[0]["ST_ID"].ToString().Trim();
                                    ICell cell7 = row.GetCell(7);
                                    string strcell7 = "";
                                    try
                                    {
                                        strcell7 = cell7.NumericCellValue.ToString().Trim();
                                    }
                                    catch
                                    {
                                        strcell7 = cell7.ToString().Trim();
                                    }
                                    jsnum = CommonFun.ComTryDecimal(strcell7);
                                    DateTime datestart;
                                    DateTime dateend;
                                    datestart = DateTime.Parse(tb_StartTime.Text.ToString().Trim());
                                    dateend = DateTime.Parse(tb_EndTime.Text.ToString().Trim());
                                    int monthnum = (dateend.Year - datestart.Year) * 12 + dateend.Month - datestart.Month + 1;
                                    for (int j = 0; j < monthnum; j++)
                                    {
                                        string sql = "";
                                        sql += "'" + sh_stid + "'," + jsnum + "";//基数
                                        DateTime dateinsert = datestart.AddMonths(j);
                                        sql += "," + (jsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["GJJ_DWBL"].ToString().Trim())) + "";
                                        sql += "," + (jsnum * CommonFun.ComTryDecimal(dtbl.Rows[0]["GJJ_GRBL"].ToString().Trim())) + "";
                                        sql += ",'" + dateinsert.ToString("yyyy-MM").Trim() + "'";//年月
                                        sql += ",'" + tb_StartTime.Text.Trim() + "—" + tb_EndTime.Text.Trim() + "'";//区间
                                        string sqltext0 = "insert into OM_GJJ(GJ_STID,GJ_JCJS,GJ_DW,GJ_GR,GJ_DATE,GJ_SJQJ) values(" + sql + ")";
                                        list.Add(sqltext0);
                                        num++;
                                    }
                                }
                                else
                                {
                                    strweidaoru = strweidaoru + "," + strcell2;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择起止年月！！！');", true);
                        ModalPopupExtenderSearch.Hide();
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请添加缴费比例！！！');", true);
                    ModalPopupExtenderSearch.Hide();
                    return;
                }
            }
            DBCallCommon.ExecuteTrans(list);
            foreach (string fileName in Directory.GetFiles(FilePath))//清空该文件夹下的文件
            {
                string newName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                System.IO.File.Delete(FilePath + "\\" + newName);//删除文件下储存的文件
            }
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导入成功,共导入" + num.ToString().Trim() + "条数据,以下数据未导入:" + strweidaoru + "！！！');", true);
        }







        //删除功能建议在使用时隐藏
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> sql = new List<string>();
            string sqlifsc = "select * from OM_GZHSB where GZ_YEARMONTH='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "' and OM_GZSCBZ='1'";
            System.Data.DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifsc);
            if (dtifsc.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月已生成工资，不能删除！！！');", true);
                return;
            }
            string sqltext = "";
            foreach (RepeaterItem LabelID in rptProNumCost.Items)
            {
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)LabelID.FindControl("CKBOX_SELECT");
                if (chk.Checked)
                {
                    string check = ((System.Web.UI.WebControls.Label)LabelID.FindControl("lb_stid")).Text;
                    sqltext = "delete FROM OM_GJJ WHERE GJ_STID='" + check + "' and GJ_DATE='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "' and symbol is null";
                    sql.Add(sqltext);
                }
            }
            DBCallCommon.ExecuteTrans(sql);
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

        protected void rptProNumCost_itemdatabind(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select (sum(GJ_DW)+sum(GJ_GR)) as GJ_HJHJ,sum(GJ_DW) as GJ_DWHJ,sum(GJ_GR) as GJ_GRHJ,(sum(GJ_DWB)+sum(GJ_GRB)) as GJ_HJBHJ,sum(GJ_DWB) as GJ_DWBHJ,sum(GJ_GRB) as GJ_GRBHJ from View_OM_GJJ where " + Creatconstr();

                System.Data.DataTable dthj = DBCallCommon.GetDTUsingSqlText(sqlhj);
                if (dthj.Rows.Count > 0)
                {
                    string totalGJ_HJHJ = dthj.Rows[0]["GJ_HJHJ"].ToString().Trim();
                    string totalGJ_DWHJ = dthj.Rows[0]["GJ_DWHJ"].ToString().Trim();
                    string totalGJ_GRHJ = dthj.Rows[0]["GJ_GRHJ"].ToString().Trim();
                    string totalGJ_HJBHJ = dthj.Rows[0]["GJ_HJBHJ"].ToString().Trim();
                    string totalGJ_DWBHJ = dthj.Rows[0]["GJ_DWBHJ"].ToString().Trim();
                    string totalGJ_GRBHJ = dthj.Rows[0]["GJ_GRBHJ"].ToString().Trim();



                    System.Web.UI.WebControls.Label lb_GJ_HJhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_GJ_HJhj");
                    System.Web.UI.WebControls.Label lb_GJ_DWhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_GJ_DWhj");
                    System.Web.UI.WebControls.Label lb_GJ_GRhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_GJ_GRhj");
                    System.Web.UI.WebControls.Label lb_GJ_HJBhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_GJ_HJBhj");
                    System.Web.UI.WebControls.Label lb_GJ_DWBhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_GJ_DWBhj");
                    System.Web.UI.WebControls.Label lb_GJ_GRBhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lb_GJ_GRBhj");




                    lb_GJ_HJhj.Text = totalGJ_HJHJ;
                    lb_GJ_DWhj.Text = totalGJ_DWHJ;
                    lb_GJ_GRhj.Text = totalGJ_GRHJ;
                    lb_GJ_HJBhj.Text = totalGJ_HJBHJ;
                    lb_GJ_DWBhj.Text = totalGJ_DWBHJ;
                    lb_GJ_GRBhj.Text = totalGJ_GRBHJ;
                }
            }
        }


        protected void dplMoth_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

        //取消
        protected void btnqx_import_Click(object sender, EventArgs e)
        {
            QueryButton.Text = "确认";
            message.Visible = false;
            message.Text = "";
            ModalPopupExtenderSearch.Hide();
        }

        protected void dplbm_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }





        //导出
        protected void btnexport_Click(object sender, EventArgs e)
        {
            string sqlgjj = "select GJ_DATE,ST_WORKNO,ST_NAME,ST_CONTR,DEP_NAME,ST_IDCARD,GJ_JCJS,(GJ_DW+GJ_GR) as GJ_HJ,GJ_DW,GJ_GR,(GJ_DWB+GJ_GRB)as GJ_HJB,GJ_DWB,GJ_GRB,GJ_BZ from View_OM_GJJ where " + Creatconstr();
            System.Data.DataTable dtgjj = DBCallCommon.GetDTUsingSqlText(sqlgjj);
            string filename = "公积金导出.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("公积金导出模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < dtgjj.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dtgjj.Columns.Count; j++)
                    {
                        string str = dtgjj.Rows[i][j].ToString();
                        row.CreateCell(j + 1).SetCellValue(str);
                    }

                }
                for (int r = 0; r <= dtgjj.Columns.Count; r++)
                {
                    sheet1.AutoSizeColumn(r);
                }
                sheet1.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
    }
}
