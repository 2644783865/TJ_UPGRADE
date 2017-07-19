using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using ZCZJ_DPF;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_KaoheRecord : BasicPage
    {
        string year = "";
        string month = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            year = (DateTime.Now.Year).ToString();
            month = (DateTime.Now.Month).ToString();

            if (!IsPostBack)
            {
                this.BindYearMoth(dplYear, dplMoth);
                BindData();
                this.InitPage();
                UCPaging1.CurrentPage = 1;

                InitVar();
                bindGrid();

            }
            InitVar();
            CheckUser(ControlFinder);
        }


        //绑定基本信息
        private void BindData()
        {
            string Stid = Session["UserId"].ToString();
            System.Data.DataTable dt = DBCallCommon.GetPermeision(1, Stid);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
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
                dpl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            dpl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            //dpl_Year.SelectedIndex = 0;
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
            //dpl_Month.SelectedIndex = 0;
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


        #region
        PagerQueryParam pager_org = new PagerQueryParam();
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager("1=1");
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }


        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager(string where)
        {
            pager_org.TableName = "TBDS_View_KaoHeTotal";
            pager_org.PrimaryKey = "kh_Id";
            pager_org.ShowFields = "* ";
            pager_org.OrderField = "Kh_Year,Kh_Month,DEP_NAME,Kh_ScoreTotal";
            pager_org.StrWhere = where;
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 300;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            string sqlwhere = "1=1";


            if (dplYear.SelectedIndex != 0)
            {
                sqlwhere += " and Kh_Year='" + dplYear.SelectedValue + "' ";
            }
            if (dplMoth.SelectedIndex != 0)
            {
                sqlwhere += " and Kh_Month='" + dplMoth.SelectedValue + "' ";
            }

            if (txtname.Text != "")
            {
                sqlwhere += " and (ST_NAME like '%" + txtname.Text.ToString().Trim() + "%' or ST_WORKNO like '%" + txtname.Text.ToString().Trim() + "%') ";
            }
            if (ddl_Depart.SelectedValue != "00")
            {
                sqlwhere += " and ST_DEPID='" + ddl_Depart.SelectedValue + "'";
            }


            InitPager(sqlwhere);

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

        protected void dplMoth_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            bindGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> sql_list = new List<string>();



            for (int i = 0; i < rptProNumCost.Items.Count; i++)
            {

                string Id = ((System.Web.UI.WebControls.Label)rptProNumCost.Items[i].FindControl("lblId")).Text.Trim();
                string scoreHp = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("txtScoreHP")).Text.Trim();
                string txtlScoreLD = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("txtlScoreLD")).Text.Trim();
                string lblScoreZong = ((HtmlInputHidden)rptProNumCost.Items[i].FindControl("hidScoreZong")).Value.Trim();
                string txtNote = ((System.Web.UI.WebControls.TextBox)rptProNumCost.Items[i].FindControl("txtNote")).Text.Trim();
                //Id, Kh_Year, Kh_Month, Kh_ScoreHP, Kh_ScoreLD, Kh_ScoreTotal, Kh_BL, Kh_Id, Kh_BeiZhu, ST_NAME, ST_SEQUEN, ST_GENDER, DEP_NAME, POSITION, ST_WORKNO
                string sqlText = "update TBDS_KaoHeTotal set Kh_ScoreHP=" + CommonFun.ComTryDecimal(scoreHp) + ",Kh_ScoreLD=" + CommonFun.ComTryDecimal(txtlScoreLD) + ",Kh_ScoreTotal=" + CommonFun.ComTryDecimal(lblScoreZong) + ",Kh_BeiZhu='" + txtNote + "' where Id='" + Id + "'";
                sql_list.Add(sqlText);
            }



            //更新
            DBCallCommon.ExecuteTrans(sql_list);

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据已保存！！！');", true);
            sql_list.Clear();

            bindGrid();
        }  //删除功能建议在使用时隐藏
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> sql = new List<string>();

            string sqltext = "";
            foreach (RepeaterItem LabelID in rptProNumCost.Items)
            {
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)LabelID.FindControl("CKBOX_SELECT");
                if (chk.Checked)
                {
                    string Id = ((System.Web.UI.WebControls.Label)LabelID.FindControl("lblId")).Text;


                    sqltext = "delete FROM TBDS_KaoHeTotal WHERE Id='" + Id + "'";
                    sql.Add(sqltext);
                }
            }
            DBCallCommon.ExecuteTrans(sql);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功！！！');", true);
            bindGrid();
        }


        protected void btnCreat_Click(object sender, EventArgs e)
        {
            string year = dplYear.SelectedValue;
            string month = dplMoth.SelectedValue;
            string bl1 = txtHP.Text.Trim();
            string bl2 = txtLD.Text.Trim();
            string bl = bl1 + ":" + bl2;
            List<string> list = new List<string>();
            if (CommonFun.ComTryInt(bl1) + CommonFun.ComTryInt(bl2) != 100)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入正确的比例，使和为100！！！');", true);
                return;
            }
            if (dplYear.SelectedIndex == 0 || dplMoth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！！！');", true);
                return;
            }
            string sql = "delete a  from TBDS_KaoHeTotal as a left join dbo.TBDS_STAFFINFO as b on a.Kh_Id=b.ST_ID where b.ST_DEPID='" + ddl_Depart.SelectedValue + "' and  a.Kh_Year='" + year + "' and a.Kh_Month='" + month + "'";
            list.Add(sql);
            if (bl1 != "0")
            {
                sql = "insert into dbo.TBDS_KaoHeTotal select '" + year + "','" + month + "','0',case when kh_Score is null then '0' else kh_Score end ,'0','" + bl + "',ST_ID,'' from dbo.TBDS_STAFFINFO as a left join (select * from dbo.TBDS_KaoHeList where (Kh_Type='人员月度考核' or Kh_Type is null) and Kh_Year='" + year + "' and Kh_Month='" + month + "'  and Kh_State in ('6','7'))b  on a.ST_ID=b.kh_Id where ST_PD='0' and ST_DEPID='" + ddl_Depart.SelectedValue + "'";
            }
            else
            {
                sql = "insert into dbo.TBDS_KaoHeTotal select '" + year + "','" + month + "','0',case when kh_Score is null then '0' else kh_Score end ,(isnull(kh_Score,0.00))/100.00*(" + CommonFun.ComTryDecimal(bl2) + "),'" + bl + "',ST_ID,'' from dbo.TBDS_STAFFINFO as a left join (select * from dbo.TBDS_KaoHeList where (Kh_Type='人员月度考核' or Kh_Type is null) and Kh_Year='" + year + "' and Kh_Month='" + month + "'  and Kh_State in ('6','7'))b  on a.ST_ID=b.kh_Id where ST_PD='0' and ST_DEPID='" + ddl_Depart.SelectedValue + "'";
            }
            list.Add(sql);

            DBCallCommon.ExecuteTrans(list);

            //更新总分


            bindGrid();
        }


        //导入
        protected void btnimport_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string sqlifsc = "select * from OM_GZHSB where GZ_YEARMONTH='" + dplYear.SelectedValue.ToString().Trim() + "-" + dplMoth.SelectedValue.ToString().Trim() + "' and OM_GZSCBZ='1'";
            System.Data.DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifsc);
            if (dtifsc.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月已生成工资，不能导入！！！');", true);
                ModalPopupExtenderSearch.Hide();
                return;
            }
            string FilePath = @"E:\考核成绩记录表\";
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
                if (tb_Time.Text.ToString().Trim() != "")
                {
                    for (int i = 1; i < sheet1.LastRowNum + 1; i++)
                    {
                        string sh_stid = "";
                        string stdepid = "";
                        string strcell01 = "";
                        IRow row = sheet1.GetRow(i);
                        ICell cell01 = row.GetCell(1);
                        try
                        {
                            strcell01 = cell01.ToString().Trim();
                        }
                        catch
                        {
                            strcell01 = "";
                        }
                        if (strcell01 != "")
                        {
                            ICell cell1 = row.GetCell(1);
                            string strcell1 = cell1.ToString().Trim();
                            string sqltext = "select ST_ID,ST_DEPID from TBDS_STAFFINFO where ST_WORKNO='" + strcell1 + "'";
                            System.Data.DataTable dttext = DBCallCommon.GetDTUsingSqlText(sqltext);
                            if (dttext.Rows.Count > 0)
                            {
                                ICell cell3 = row.GetCell(3);
                                ICell cell4 = row.GetCell(4);
                                string strcell3 = "";
                                string strcell4 = "";
                                try
                                {
                                    strcell3 = cell3.NumericCellValue.ToString().Trim();
                                    strcell4 = cell4.NumericCellValue.ToString().Trim();
                                }
                                catch
                                {

                                    try
                                    {
                                        strcell3 = cell3.ToString().Trim();
                                        strcell4 = cell4.ToString().Trim();
                                    }
                                    catch
                                    {
                                        strcell3 = "";
                                        strcell4 = "";
                                    }
                                }
                                string sqlupdate = "";
                                sh_stid = dttext.Rows[0]["ST_ID"].ToString().Trim();
                                stdepid = dttext.Rows[0]["ST_DEPID"].ToString().Trim();
                                if (stdepid == "13" || stdepid == "08" || stdepid == "09")
                                {
                                    sqlupdate = "update TBDS_KaoHeTotal set Kh_ScoreHP='" + CommonFun.ComTryDecimal(strcell3) + "',Kh_ScoreLD='" + CommonFun.ComTryDecimal(strcell4) + "' where Kh_Id='" + sh_stid + "' and Kh_Year='" + tb_Time.Text.ToString().Trim().Substring(0, 4) + "' and Kh_Month='" + tb_Time.Text.ToString().Trim().Substring(5, 2) + "'";
                                }
                                else
                                {
                                    sqlupdate = "update TBDS_KaoHeTotal set Kh_ScoreHP='" + CommonFun.ComTryDecimal(strcell3) + "' where Kh_Id='" + sh_stid + "' and Kh_Year='" + tb_Time.Text.ToString().Trim().Substring(0, 4) + "' and Kh_Month='" + tb_Time.Text.ToString().Trim().Substring(5, 2) + "'";
                                }
                                list.Add(sqlupdate);
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
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！！！');", true);
                    ModalPopupExtenderSearch.Hide();
                    return;
                }
            }
            DBCallCommon.ExecuteTrans(list);
            updatetotalscore();//更新总分数
            foreach (string fileName in Directory.GetFiles(FilePath))//清空该文件夹下的文件
            {
                string newName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                System.IO.File.Delete(FilePath + "\\" + newName);//删除文件下储存的文件
            }
            ModalPopupExtenderSearch.Hide();
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }


        private void updatetotalscore()
        {
            List<string> listupdate = new List<string>();
            string sqldata0 = "select * from TBDS_KaoHeTotal where Kh_Year='" + tb_Time.Text.ToString().Trim().Substring(0, 4) + "' and Kh_Month='" + tb_Time.Text.ToString().Trim().Substring(5, 2) + "'";
            System.Data.DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqldata0);
            for (int k = 0; k < dt0.Rows.Count; k++)
            {
                string sqlgetbl = "select * from TBDS_KaoHeTotal where Kh_Id='" + dt0.Rows[k]["Kh_Id"] + "' and Kh_Year='" + tb_Time.Text.ToString().Trim().Substring(0, 4) + "' and Kh_Month='" + tb_Time.Text.ToString().Trim().Substring(5, 2) + "'";
                System.Data.DataTable dtgetbl = DBCallCommon.GetDTUsingSqlText(sqlgetbl);
                if (dtgetbl.Rows.Count > 0 && dtgetbl.Rows[0]["Kh_BL"].ToString().Trim() != "" && dtgetbl.Rows[0]["Kh_BL"].ToString().Trim().Contains(":"))
                {
                    int mhindex = dtgetbl.Rows[0]["Kh_BL"].ToString().Trim().IndexOf(":", 0);
                    int bllength = dtgetbl.Rows[0]["Kh_BL"].ToString().Trim().Length;
                    string getbl1 = dtgetbl.Rows[0]["Kh_BL"].ToString().Trim().Substring(0, mhindex);
                    string getbl2 = dtgetbl.Rows[0]["Kh_BL"].ToString().Trim().Substring(mhindex + 1, bllength - mhindex - 1);
                    string sqltext1 = "update TBDS_KaoHeTotal set Kh_ScoreTotal=Kh_ScoreHP*" + ((CommonFun.ComTryDecimal(getbl1)) / 100) + "+Kh_ScoreLD*" + ((CommonFun.ComTryDecimal(getbl2)) / 100) + " where Kh_Id='" + dt0.Rows[k]["Kh_Id"] + "' and Kh_Year='" + tb_Time.Text.ToString().Trim().Substring(0, 4) + "' and Kh_Month='" + tb_Time.Text.ToString().Trim().Substring(5, 2) + "'";
                    listupdate.Add(sqltext1);
                }
            }
            DBCallCommon.ExecuteTrans(listupdate);
        }


        //取消
        protected void btnqx_import_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }
    }
}
