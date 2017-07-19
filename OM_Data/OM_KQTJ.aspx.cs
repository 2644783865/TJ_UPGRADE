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
    public partial class OM_KQTJ : BasicPage
    {

        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            string NJ_ST_ID = Request.QueryString["stidnj"];


            if (!IsPostBack)
            {
                //BindbmData();
                bindgouxuanbm();
                bindddlbz();
                if (Session["UserDeptID"].ToString().Trim() != "02" && Session["UserDeptID"].ToString().Trim() != "01")
                {
                    btnSave.Visible = false;
                    btn_importclient.Visible = false;
                    FileUpload1.Visible = false;
                    btnDelete.Visible = false;
                }
                this.BindYearMoth(ddlYear, ddlMonth);
                if (NJ_ST_ID != "" && NJ_ST_ID != null)
                {
                    string sqltextnj = "select ST_NAME from TBDS_STAFFINFO where ST_ID=" + NJ_ST_ID + "";
                    System.Data.DataTable dtnj = DBCallCommon.GetDTUsingSqlText(sqltextnj);
                    if (dtnj.Rows.Count > 0)
                    {
                        string njmaneid = dtnj.Rows[0][0].ToString();
                        txtName.Text = njmaneid;

                    }
                }
                this.InitPage();
                UCPaging1.CurrentPage = 1;
                InitVar();
                bindrpt();
            }
            CheckUser(ControlFinder);
            InitVar();
        }




        /// <summary>
        /// 绑定年月

        private void BindYearMoth(DropDownList ddl_Year, DropDownList ddl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                ddl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            ddl_Year.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                ddl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            ddl_Month.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
        }



        //private void BindbmData()
        //{

        //    string stId = Session["UserId"].ToString();
        //    System.Data.DataTable dt = DBCallCommon.GetPermeision(7, stId);
        //    ddl_Depart.DataSource = dt;
        //    ddl_Depart.DataTextField = "DEP_NAME";
        //    ddl_Depart.DataValueField = "DEP_CODE";
        //    ddl_Depart.DataBind();
        //}

        //勾选部门绑定
        private void bindgouxuanbm()
        {
            string sqlbumen = "SELECT DISTINCT DEP_CODE,DEP_NAME FROM TBDS_DEPINFO WHERE len(DEP_CODE)=2";
            System.Data.DataTable dtbumen = DBCallCommon.GetDTUsingSqlText(sqlbumen);
            listdepartment.DataTextField = "DEP_NAME";
            listdepartment.DataValueField = "DEP_CODE";
            listdepartment.DataSource = dtbumen;
            listdepartment.DataBind();
            ListItem item = new ListItem("全部", "");
            listdepartment.Items.Insert(0, item);
        }

        private void bindddlbz()
        {
            string sql = "SELECT DISTINCT ST_DEPID1 FROM TBDS_STAFFINFO where ST_DEPID1!='' and ST_DEPID1 not in('0','1','2','3','4','5','6','7','8','9')";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddlbz.DataTextField = "ST_DEPID1";
            ddlbz.DataValueField = "ST_DEPID1";
            ddlbz.DataSource = dt;
            ddlbz.DataBind();
            ListItem item = new ListItem("--请选择--", "--请选择--");
            ddlbz.Items.Insert(0, item);
        }


        #region 分页
        /// <summary>
        /// 初始化页面
        /// </summary>

        private void InitPage()
        {
            //txtName.Text = "";
            ddlYear.ClearSelection();
            foreach (ListItem li in ddlYear.Items)//显示当前年份
            {
                if (li.Value.ToString() == DateTime.Now.Year.ToString())
                {
                    li.Selected = true; break;
                }
            }

            ddlMonth.ClearSelection();
            string month = (DateTime.Now.Month - 1).ToString();
            string NJ_ST_ID = Request.QueryString["stidnj"];
            if (NJ_ST_ID != "" && NJ_ST_ID != null)
            {
                month = "-请选择-";

            }
            else if (DateTime.Now.Month < 10 || DateTime.Now.Month == 10)//显示当前月份
           {
                month = "0" + month;
           }

            foreach (ListItem li in ddlMonth.Items)
            {
                if (li.Value.ToString() == month)
                {
                    li.Selected = true; break;
                }
            }
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
            pager_org.TableName = "View_OM_KQTJ";
            pager_org.PrimaryKey = "id";
            pager_org.ShowFields = "KQ_ST_ID,KQ_DATE,ST_WORKNO,ST_NAME,DEP_NAME,ST_DEPID1,KQ_GNCC,KQ_GWCC,KQ_BINGJ,KQ_SHIJ,KQ_KUANGG,KQ_DAOXIU,KQ_CHANJIA,KQ_PEICHAN,KQ_HUNJIA,KQ_SANGJIA,KQ_GONGS,KQ_NIANX,KQ_BEIYONG1,KQ_BEIYONG2,KQ_BEIYONG3,KQ_BEIYONG4,KQ_BEIYONG5,KQ_BEIYONG6,KQ_QTJIA,KQ_JIEDIAO,KQ_ZMJBAN,KQ_JRJIAB,KQ_ZHIBAN,KQ_YEBAN,KQ_ZHONGB,KQ_CBTS,KQ_YSGZ,KQ_BEIZHU,KQ_XGTIME,KQ_CHUQIN";
            pager_org.OrderField = "KQ_ST_ID";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 25;
        }
        /// <summary>
        /// 定义查询条件
        /// </summary>
        /// <returns></returns>
        private string StrWhere()
        {
            int num = 0;
            string sql = "1=1";
            if (ddlYear.SelectedIndex != 0 && ddlMonth.SelectedIndex != 0)
            {
                sql += " and KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "'";
            }
            else if (ddlYear.SelectedIndex == 0 && ddlMonth.SelectedIndex != 0)
            {
                sql += " and KQ_DATE like '%-" + ddlMonth.SelectedValue.ToString().Trim() + "%'";
            }
            else if (ddlYear.SelectedIndex != 0 && ddlMonth.SelectedIndex == 0)
            {
                sql += " and KQ_DATE like '%" + ddlYear.SelectedValue.ToString().Trim() + "-%'";
            }
            if (txtName.Text != "")
            {
                sql += " and ST_NAME like '%" + txtName.Text.ToString().Trim() + "%'";
            }
            //if (ddl_Depart.SelectedValue != "00")
            //{
            //    sql += " and ST_DEPID='" + ddl_Depart.SelectedValue + "'";
            //}
            if (ddlbz.SelectedIndex != 0)
            {
                sql += " and ST_DEPID1='" + ddlbz.SelectedValue + "'";
            }
            if (radio_kuayue.Checked == true)
            {
                sql += " and KQ_TYPE='0'";
            }
            if (radio_zhengyue.Checked == true)
            {
                sql += " and KQ_TYPE='1'";
            }
            
            for (int i = 0; i < listdepartment.Items.Count; i++)
            {
                if (listdepartment.Items[i].Selected == true)
                {
                    num++;
                }
            }
            if (num > 0)
            {
                sql += " and (ST_DEPID=''";
                for (int i = 0; i < listdepartment.Items.Count; i++)
                {
                    if (listdepartment.Items[i].Selected == true)
                    {
                        sql += " or ST_DEPID like '%" + listdepartment.Items[i].Value + "%'";
                    }
                }
                sql += ")";
            }


            int num2 = 0;
            for (int j = 0; j < CheckBoxListhj.Items.Count; j++)
            {
                if (CheckBoxListhj.Items[j].Selected == true)
                {
                    num2++;
                }
            }
            if (num2 > 0)
            {
                string strgetziduan="(0";
                for (int j = 0; j < CheckBoxListhj.Items.Count; j++)
                {
                    if (CheckBoxListhj.Items[j].Selected == true)
                    {
                        strgetziduan += "+" + CheckBoxListhj.Items[j].Value.ToString().Trim() + "";
                    }
                }
                strgetziduan += ")";
                if (txt_hjmin.Text.Trim() != "")
                {
                    sql += " and " + strgetziduan.ToString().Trim() + ">=" + CommonFun.ComTryDecimal(txt_hjmin.Text.Trim()) + "";
                }
                if (txt_hjmax.Text.Trim() != "")
                {
                    sql += " and " + strgetziduan.ToString().Trim() + "<=" + CommonFun.ComTryDecimal(txt_hjmax.Text.Trim()) + "";
                }
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
            CommonFun.Paging(dt, rptKQTJ, UCPaging1, palNoData);
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

        //protected void dplbm_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    UCPaging1.CurrentPage = 1;
        //    bindrpt();
        //}

        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }


        protected void btn_clear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < listdepartment.Items.Count; i++)
            {
                listdepartment.Items[i].Selected = false;
            }
            for (int j = 0; j < CheckBoxListhj.Items.Count; j++)
            {
                CheckBoxListhj.Items[j].Selected = false;
            }
            txt_hjmin.Text = "";
            txt_hjmax.Text = "";
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void ddlbz_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void radio_all_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void radio_kuayue_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void radio_zhengyue_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> sql_list = new List<string>();
            if (ddlYear.SelectedIndex == 0 || ddlMonth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择年月！！！');", true);
                return;
            }
            string sqlifsc = "select * from OM_GZHSB where GZ_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and OM_GZSCBZ='1'";
            System.Data.DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifsc);
            if (dtifsc.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月已生成工资，不能修改！！！');", true);
                return;
            }
            int k = 0;//记录选中的行数
            for (int j = 0; j < rptKQTJ.Items.Count; j++)
            {
                if (((System.Web.UI.WebControls.CheckBox)rptKQTJ.Items[j].FindControl("CKBOX_SELECT")).Checked)
                {
                    k++;
                }
            }
            if (k > 0 && ddlYear.SelectedIndex != 0 && ddlMonth.SelectedIndex != 0)
            {
                for (int i = 0; i < rptKQTJ.Items.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)rptKQTJ.Items[i].FindControl("CKBOX_SELECT")).Checked)
                    {
                        string stid = ((System.Web.UI.WebControls.Label)rptKQTJ.Items[i].FindControl("lbKQ_ST_ID")).Text.Trim();//员工id
                        string lb_gh = ((System.Web.UI.WebControls.Label)rptKQTJ.Items[i].FindControl("lb_gh")).Text.Trim();//工号
                        string tb_chuqin = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_chuqin")).Text.Trim();//出勤
                        string tb_gncc = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_gncc")).Text.Trim();//国内出差
                        string tb_gwcc = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_gwcc")).Text.Trim();//国外出差
                        string tb_bingj = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_bingj")).Text.Trim();//病假
                        string tb_shij = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_shij")).Text.Trim();//事假
                        string tb_kuangg = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_kuangg")).Text.Trim();//旷工
                        string tb_daoxiu = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_daoxiu")).Text.Trim();//倒休
                        string tb_chanjia = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_chanjia")).Text.Trim();//产假
                        string tb_peichan = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_peichan")).Text.Trim();//陪产假
                        string tb_hunjia = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_hunjia")).Text.Trim();//婚假

                        string tb_sangjia = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_sangjia")).Text.Trim();//丧假
                        string tb_gongs = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_gongs")).Text.Trim();//工伤
                        string tb_nianx = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_nianx")).Text.Trim();//年休假
                        string tb_beiyong1 = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_beiyong1")).Text.Trim();//备用1
                        string tb_beiyong2 = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_beiyong2")).Text.Trim();//备用2
                        string tb_beiyong3 = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_beiyong3")).Text.Trim();//备用3
                        string tb_beiyong4 = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_beiyong4")).Text.Trim();//备用4
                        string tb_beiyong5 = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_beiyong5")).Text.Trim();//备用5
                        string tb_beiyong6 = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_beiyong6")).Text.Trim();//备用6
                        string tb_qtjia = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_qtjia")).Text.Trim();//其他

                        string tb_jiediao = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_jiediao")).Text.Trim();//借调
                        string tb_zmjban = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_zmjban")).Text.Trim();//周末加班
                        string tb_jrjiab = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_jrjiab")).Text.Trim();//节日加班
                        string tb_zhiban = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_zhiban")).Text.Trim();//值班
                        string tb_yeban = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_yeban")).Text.Trim();//夜班
                        string tb_zhongb = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_zhongb")).Text.Trim();//中班
                        string tb_KQ_CBTS = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_KQ_CBTS")).Text.Trim();//延时工作
                        string tb_ysgz = ((System.Web.UI.WebControls.TextBox)rptKQTJ.Items[i].FindControl("tb_ysgz")).Text.Trim();//延时工作


                        string str2 = "update OM_KQTJ set KQ_CHUQIN='" + tb_chuqin + "',KQ_GNCC='" + tb_gncc + "',KQ_GWCC='" + tb_gwcc + "',KQ_BINGJ='" + tb_bingj + "',KQ_SHIJ='" + tb_shij + "',KQ_KUANGG='" + tb_kuangg + "',KQ_DAOXIU='" + tb_daoxiu + "',KQ_CHANJIA='" + tb_chanjia + "',KQ_PEICHAN='" + tb_peichan + "',  ";
                        str2 += " KQ_HUNJIA='" + tb_hunjia + "',KQ_SANGJIA='" + tb_sangjia + "',KQ_GONGS='" + tb_gongs + "',KQ_NIANX='" + tb_nianx + "',KQ_BEIYONG1='" + tb_beiyong1 + "',KQ_BEIYONG2='" + tb_beiyong2 + "',KQ_BEIYONG3='" + tb_beiyong3 + "',KQ_BEIYONG4='" + tb_beiyong4 + "',KQ_BEIYONG5='" + tb_beiyong5 + "',KQ_BEIYONG6='" + tb_beiyong6 + "',KQ_QTJIA='" + tb_qtjia + "', ";
                        str2 += " KQ_JIEDIAO='" + tb_jiediao + "',KQ_ZMJBAN='" + tb_zmjban + "',KQ_JRJIAB='" + tb_jrjiab + "',KQ_ZHIBAN='" + tb_zhiban + "',KQ_YEBAN='" + tb_yeban + "',KQ_ZHONGB='" + tb_zhongb + "',KQ_CBTS='" + tb_KQ_CBTS + "',KQ_YSGZ='" + tb_ysgz + "',KQ_XGTIME='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "'";
                        str2 += " where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' AND KQ_ST_ID='" + stid + "'";

                        string strnianjia = "select * from OM_KQTJ where KQ_ST_ID='" + stid + "' and KQ_DATE='" +ddlYear.SelectedValue.ToString() + "-" + ddlMonth.SelectedValue.ToString() +"'";
                        System.Data.DataTable dtsavenj = DBCallCommon.GetDTUsingSqlText(strnianjia);
                        string strupdatenj = "";
                        if (dtsavenj.Rows.Count > 0)
                        {
                            strupdatenj = "update OM_NianJiaTJ set NJ_YSY=NJ_YSY+" + CommonFun.ComTryDecimal(tb_nianx) + "-" + CommonFun.ComTryDecimal(dtsavenj.Rows[0]["KQ_NIANX"].ToString().Trim()) + " where NJ_ST_ID='" + stid + "'";
                            sql_list.Add(strupdatenj);
                        }

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
            bindrpt();

        }



        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            List<string> sql = new List<string>();
            string sqlifsc = "select * from OM_GZHSB where GZ_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and OM_GZSCBZ='1'";
            System.Data.DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifsc);
            if (dtifsc.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月已生成工资，不能删除！！！');", true);
                return;
            }
            string sqltext = "";
            string sqlupdatenj = "";
            int k = 0;
            foreach (RepeaterItem LabelID in rptKQTJ.Items)
            {
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)LabelID.FindControl("CKBOX_SELECT");
                if (chk.Checked)
                {
                    k++;
                }
            }
            if (k == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要删除的数据！！！');", true);
                return;
            }
            else
            {
                foreach (RepeaterItem LabelID in rptKQTJ.Items)
                {
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)LabelID.FindControl("CKBOX_SELECT");
                    if (chk.Checked)
                    {
                        string kqstid = ((System.Web.UI.WebControls.Label)LabelID.FindControl("lbKQ_ST_ID")).Text.ToString().Trim();
                        string yearmonth = ((System.Web.UI.WebControls.Label)LabelID.FindControl("lb_yf")).Text.ToString().Trim();
                        string sqlnianjia = "select * from OM_KQTJ where KQ_ST_ID='" + kqstid + "' and KQ_DATE='" + yearmonth + "'";
                        System.Data.DataTable dtnianjia = DBCallCommon.GetDTUsingSqlText(sqlnianjia);
                        if (dtnianjia.Rows.Count > 0)
                        {
                            sqlupdatenj = "update OM_NianJiaTJ set NJ_YSY=NJ_YSY-" + CommonFun.ComTryDecimal(dtnianjia.Rows[0]["KQ_NIANX"].ToString().Trim()) + " where NJ_ST_ID='" + kqstid + "'";
                            sql.Add(sqlupdatenj);
                        }
                        sqltext = "delete from OM_KQTJ where KQ_ST_ID='" + kqstid + "' and KQ_DATE='" + yearmonth + "'";
                        sql.Add(sqltext);
                    }
                }
            }
            DBCallCommon.ExecuteTrans(sql);

            bindrpt();
        }


        private string existif()
        {
            string strexist = "N";
            string sql = "select * from OM_KQTJ where KQ_DATE='" + tb_yearmonth.Text.ToString().Trim() + "' and KQ_TYPE='0'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                strexist = "Y";
            }
            return strexist;
        }

        //导入数据
        protected void btn_import_Click(object sender, EventArgs e)
        {
            if (QueryButton.Text == "确认" && existif() == "Y")
            {
                QueryButton.Text = "重新导入";
                message.Visible = true;
                message.Text = "提示：该月数据已存在,覆盖原数据请点击'重新导入'!";
                ModalPopupExtenderSearch.Show();
                return;
            }
            List<string> listsql = new List<string>();
            int num = 0;
            string strweidaoru = "";
            string sqlifscgz = "select * from OM_GZHSB where GZ_YEARMONTH='" + tb_yearmonth.Text.ToString().Trim() + "'";
            System.Data.DataTable dtisscgz = DBCallCommon.GetDTUsingSqlText(sqlifscgz);
            if (dtisscgz.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月已生成工资！！！');", true);
                return;
            }
            string sqldysj = "select * from OM_KQTJ where KQ_DATE='" + tb_yearmonth.Text.ToString().Trim() + "' and KQ_TYPE='0'";
            System.Data.DataTable dtdysj = DBCallCommon.GetDTUsingSqlText(sqldysj);
            if (dtdysj.Rows.Count > 0)
            {
                List<string> listdelete = new List<string>();
                string sqldrdeletenj = "";
                string sqldrnj = "select * from OM_KQTJ where KQ_DATE='" + tb_yearmonth.Text.ToString().Trim() + "' and KQ_TYPE='0'";
                System.Data.DataTable dtdrnj = DBCallCommon.GetDTUsingSqlText(sqldrnj);
                if (dtdrnj.Rows.Count > 0)
                {
                    for (int t = 0; t < dtdrnj.Rows.Count; t++)
                    {
                        sqldrdeletenj = "update OM_NianJiaTJ set NJ_YSY=NJ_YSY-" + CommonFun.ComTryDecimal(dtdrnj.Rows[t]["KQ_NIANX"].ToString().Trim()) + " where NJ_ST_ID='" + dtdrnj.Rows[t]["KQ_ST_ID"] + "'";
                        listdelete.Add(sqldrdeletenj);
                    }
                }
                string sqldeldykqsj = "delete from OM_KQTJ where KQ_DATE='" + tb_yearmonth.Text.ToString().Trim() + "' and KQ_TYPE='0'";
                string sqldeldytiyle = "delete from OM_KQtitle where BT_YEARMONTH='" + tb_yearmonth.Text.ToString().Trim() + "'";
                string sqldeldydetail = "delete from OM_KQdetail where MX_YEARMONTH='" + tb_yearmonth.Text.ToString().Trim() + "'";
                listdelete.Add(sqldeldykqsj);
                listdelete.Add(sqldeldytiyle);
                listdelete.Add(sqldeldydetail);
                DBCallCommon.ExecuteTrans(listdelete);
            }


            //导入数据
            string FilePath = @"E:\考勤明细\";
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
                if (tb_yearmonth.Text.Trim()!="")
                {
                    //导入表头
                    string sqltitledate = "";
                    string sqltitleweek = "";
                    string strdate = "";
                    string strweek = "";
                    IRow rowdateweek = sheet1.GetRow(3);
                    sqltitledate += "'" + tb_yearmonth.Text.Trim() + "','0'";
                    sqltitleweek += "'" + tb_yearmonth.Text.Trim() + "','1'";
                    for (int k = 5; k < 36; k++)
                    {
                        ICell celldateweek = rowdateweek.GetCell(5); 
                        ICellStyle cellStyle = wk.CreateCellStyle();
                        IDataFormat format = wk.CreateDataFormat();
                        cellStyle.DataFormat = format.GetFormat("yyyy-MM-d");
                        celldateweek.CellStyle = cellStyle;
                        try
                        {

                            strdate = (DateTime.Parse(celldateweek.ToString().Trim())).AddDays(k - 5).ToString("yyyy-MM-d").Substring(8);
                            strweek = (DateTime.Parse(celldateweek.ToString().Trim())).AddDays(k - 5).ToString("ddd").Trim();
                        }
                        catch
                        {
                            strdate = "";
                            strweek = "";
                        }
                        sqltitledate += ",'" + strdate + "'";
                        sqltitleweek += ",'" + strweek + "'";
                    }
                    string sqlkqtitledate = "insert into OM_KQtitle(BT_YEARMONTH,BT_TYPE,BT_1,BT_2,BT_3,BT_4,BT_5,BT_6,BT_7,BT_8,BT_9,BT_10,BT_11,BT_12,BT_13,BT_14,BT_15,BT_16,BT_17,BT_18,BT_19,BT_20,BT_21,BT_22,BT_23,BT_24,BT_25,BT_26,BT_27,BT_28,BT_29,BT_30,BT_31) values(" + sqltitledate + ")";
                    string sqlkqtitleweek = "insert into OM_KQtitle(BT_YEARMONTH,BT_TYPE,BT_1,BT_2,BT_3,BT_4,BT_5,BT_6,BT_7,BT_8,BT_9,BT_10,BT_11,BT_12,BT_13,BT_14,BT_15,BT_16,BT_17,BT_18,BT_19,BT_20,BT_21,BT_22,BT_23,BT_24,BT_25,BT_26,BT_27,BT_28,BT_29,BT_30,BT_31) values(" + sqltitleweek + ")";
                    listsql.Add(sqlkqtitledate);
                    listsql.Add(sqlkqtitleweek);




                    //导入考勤明细
                    string strname = "";
                    string sqltext = "";
                    string kq_stid = "";
                    string strgzrcq = "";
                    for (int i = 6; i < sheet1.LastRowNum + 1; i++)
                    {
                        System.Data.DataTable dtfindstid;
                        if (i%3==0)
                        {
                            sqltext = "";
                            strname = "";
                            strgzrcq = "";
                            IRow row02 = sheet1.GetRow(i);
                            ICell cell02 = row02.GetCell(2);
                            ICell cell36 = row02.GetCell(36);
                            try
                            {
                                strname = cell02.ToString().Trim();
                                strgzrcq = cell36.NumericCellValue.ToString().Trim();
                            }
                            catch
                            {
                                strname = "";
                                try
                                {
                                    strgzrcq = cell36.ToString().Trim();
                                }
                                catch
                                {
                                    strgzrcq = "";
                                }
                            }
                            if (strname != "")
                            {
                                string sqlfindstid = "select * from TBDS_STAFFINFO where ST_NAME='" + strname + "'";
                                dtfindstid = DBCallCommon.GetDTUsingSqlText(sqlfindstid);
                                if (dtfindstid.Rows.Count > 0)
                                {
                                    kq_stid = dtfindstid.Rows[0]["ST_ID"].ToString().Trim();
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        sqltext = "'" + kq_stid.Trim() + "','" + tb_yearmonth.Text.Trim() + "'";
                        IRow rowdetail = sheet1.GetRow(i);
                        for (int m = 4; m < 36; m++)
                        {
                            string kq_detail="";
                            ICell celldetail=rowdetail.GetCell(m);
                            try
                            {
                                kq_detail=celldetail.ToString().Trim();
                            }
                            catch
                            {
                                kq_detail="";
                            }
                            sqltext += ",'" + kq_detail.Trim() + "'";
                        }
                        sqltext += ",'" + strgzrcq.Trim() + "'";
                        string sqlkqdetail = "insert into OM_KQdetail(MX_STID,MX_YEARMONTH,MX_SJD,MX_1,MX_2,MX_3,MX_4,MX_5,MX_6,MX_7,MX_8,MX_9,MX_10,MX_11,MX_12,MX_13,MX_14,MX_15,MX_16,MX_17,MX_18,MX_19,MX_20,MX_21,MX_22,MX_23,MX_24,MX_25,MX_26,MX_27,MX_28,MX_29,MX_30,MX_31,MX_GZRCQ) values(" + sqltext + ")";
                        listsql.Add(sqlkqdetail);
                    }


                    //导入考勤数据
                    for (int n = 6; n < sheet1.LastRowNum + 1; n+=3)
                    {
                        string kqtoatal_stid = "";
                        string strkqname = "";
                        string sqlkqdata = "";
                        IRow rowkqtotal = sheet1.GetRow(n);
                        ICell cell002 = rowkqtotal.GetCell(2);
                        try
                        {
                            strkqname = cell002.ToString().Trim();
                        }
                        catch
                        {
                            strkqname = "";
                        }
                        if (strkqname != "")
                        {
                            string sqlkqstid = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + strkqname + "'";
                            System.Data.DataTable dtkqstid = DBCallCommon.GetDTUsingSqlText(sqlkqstid);
                            if (dtkqstid.Rows.Count > 0)
                            {
                                kqtoatal_stid = dtkqstid.Rows[0]["ST_ID"].ToString().Trim();
                                sqlkqdata += "'" + kqtoatal_stid + "','" + tb_yearmonth.Text.Trim() + "'";
                                for (int t = 44; t < 73; t++)
                                {
                                    string strkqdata = "";
                                    ICell cellkqdata = rowkqtotal.GetCell(t);
                                    try
                                    {
                                        strkqdata = cellkqdata.NumericCellValue.ToString();
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            strkqdata = cellkqdata.ToString().Trim();
                                        }
                                        catch
                                        {
                                            strkqdata = "";
                                        }
                                    }
                                    sqlkqdata += ",'" + strkqdata + "'";
                                }
                                sqlkqdata += ",'0'";
                                string sqlkqdetail = "insert into OM_KQTJ(KQ_ST_ID,KQ_DATE,KQ_CHUQIN,KQ_GNCC,KQ_GWCC,KQ_BINGJ,KQ_SHIJ,KQ_KUANGG,KQ_DAOXIU,KQ_CHANJIA,KQ_PEICHAN,KQ_HUNJIA,KQ_SANGJIA,KQ_GONGS,KQ_NIANX,KQ_BEIYONG1,KQ_BEIYONG2,KQ_BEIYONG3,KQ_BEIYONG4,KQ_BEIYONG5,KQ_BEIYONG6,KQ_QTJIA,KQ_JIEDIAO,KQ_ZMJBAN,KQ_JRJIAB,KQ_ZHIBAN,KQ_YEBAN,KQ_ZHONGB,KQ_CBTS,KQ_YSGZ,KQ_BEIZHU,KQ_TYPE) values(" + sqlkqdata + ")";
                                listsql.Add(sqlkqdetail);
                                num++;
                            }
                            else
                            {
                                strweidaoru = strweidaoru+"," + strkqname;
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
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择导入年月！！！');", true);
                    ModalPopupExtenderSearch.Hide();
                    return;
                }
            }

            DBCallCommon.ExecuteTrans(listsql);

            drupdateysynianj();//导入更新已使用年假信息
            updatecbts();//餐补天数去小数

            nianjiatz();//因为达到第五条规定条件而调整年假的调整天数

            foreach (string fileName in Directory.GetFiles(FilePath))//清空该文件夹下的文件
            {
                string newName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                System.IO.File.Delete(FilePath + "\\" + newName);//删除文件下储存的文件
            }
            UCPaging1.CurrentPage = 1;
            bindrpt();
            //Response.Redirect(Request.Url.ToString());
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导入成功,共导入" + num.ToString().Trim() + "条数据,以下数据未导入:" + strweidaoru + "！！！');", true);
        }


        //餐补天数去小数
        private void updatecbts()
        {
            List<string> listcbts = new List<string>();
            string sqlcbts = "";
            string sqlgetdata = "select * from OM_KQTJ where KQ_DATE='" + tb_yearmonth.Text.Trim() + "' and KQ_TYPE='0'";
            System.Data.DataTable dtgetdata = DBCallCommon.GetDTUsingSqlText(sqlgetdata);
            if (dtgetdata.Rows.Count > 0)
            {
                for (int i = 0; i < dtgetdata.Rows.Count; i++)
                {
                    if (dtgetdata.Rows[i]["KQ_CBTS"].ToString().Trim().Contains("."))
                    {
                        sqlcbts = "update OM_KQTJ set KQ_CBTS=" + Math.Floor(CommonFun.ComTryDecimal(dtgetdata.Rows[i]["KQ_CBTS"].ToString().Trim())) + " where KQ_DATE='" + tb_yearmonth.Text.Trim() + "' and KQ_ST_ID='" + dtgetdata.Rows[i]["KQ_ST_ID"].ToString().Trim() + "' and KQ_TYPE='0'";
                        listcbts.Add(sqlcbts);
                    }
                }
                DBCallCommon.ExecuteTrans(listcbts);
            }
        }



        //导入更新年假信息
        private void drupdateysynianj()
        {
            List<string> listupdate = new List<string>();
            string sqlupdate = "";
            string sqlgetdata = "select KQ_ST_ID,sum(KQ_NIANX) as KQ_NIANXnew from OM_KQTJ where KQ_DATE='" + tb_yearmonth.Text.ToString().Trim() + "' and KQ_TYPE='0' group by KQ_ST_ID";
            System.Data.DataTable dtgetdata = DBCallCommon.GetDTUsingSqlText(sqlgetdata);
            if (dtgetdata.Rows.Count > 0)
            {
                for (int i = 0; i < dtgetdata.Rows.Count; i++)
                {
                    sqlupdate = "update OM_NianJiaTJ set NJ_YSY=NJ_YSY+" + CommonFun.ComTryDecimal(dtgetdata.Rows[i]["KQ_NIANXnew"].ToString().Trim()) + " where NJ_ST_ID='" + dtgetdata.Rows[i]["KQ_ST_ID"] + "'";
                    listupdate.Add(sqlupdate);
                }
            }
            DBCallCommon.ExecuteTrans(listupdate);
        }



        #region
        //生成考勤清单
        //protected void btn_scdata_Click(object sender, EventArgs e)
        //{
        //    List<string> list_sql=new List<string>();
        //    string sqlifsc = "select * from OM_GZHSB where GZ_YEARMONTH='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and OM_GZSCBZ='1'";
        //    DataTable dtifsc = DBCallCommon.GetDTUsingSqlText(sqlifsc);
        //    if (dtifsc.Rows.Count > 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该月已生成工资，不能重新生成！！！');", true);
        //        return;
        //    }
        //    if (ddlYear.SelectedIndex == 0 || ddlMonth.SelectedIndex == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要生成数据的年月份!');", true);
        //        return;
        //    }
        //    if (tbworkdays.Text.ToString().Trim() == "")
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写本月工作日天数!');", true);
        //        return;
        //    }
        //    string sqlsfsh = "select * from OM_QJSQSP where QJ_SPZT='1' and QJ_QJQSSJ<='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "-20" + "'";
        //    DataTable dtsfsh = DBCallCommon.GetDTUsingSqlText(sqlsfsh);
        //    if (dtsfsh.Rows.Count > 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('存在之前申请未审核!');", true);
        //        return;
        //    }

        //    string sqlifcz = "select * from OM_KQTJ where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "'";
        //    DataTable dtifcz = DBCallCommon.GetDTUsingSqlText(sqlifcz);
        //    //获取人员信息表信息
        //    string sqlstaffinfo = "select * from View_TBDS_STAFFINFO where ST_PD='0'";
        //    DataTable dtstaffinfo = DBCallCommon.GetDTUsingSqlText(sqlstaffinfo);
        //    if (dtifcz.Rows.Count > 0)
        //    {
        //        string sqldel = "delete from OM_KQTJ where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "'";
        //        DBCallCommon.ExeSqlText(sqldel);
        //    }


        //    //向表中插入人员信息
        //    if (dtstaffinfo.Rows.Count > 0)
        //    {
        //        for (int k = 0; k < dtstaffinfo.Rows.Count; k++)
        //        {
        //            string sqlinsert = "insert into OM_KQTJ(KQ_ST_ID,KQ_DATE,KQ_PersonID,KQ_Name,KQ_SCTIME,KQ_WORKDAY) values('" + dtstaffinfo.Rows[k]["ST_ID"].ToString().Trim() + "','" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "','" + dtstaffinfo.Rows[k]["ST_WORKNO"].ToString().Trim() + "','" + dtstaffinfo.Rows[k]["ST_NAME"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','" + CommonFun.ComTryDecimal(tbworkdays.Text.ToString().Trim()) + "')";
        //            list_sql.Add(sqlinsert);
        //        }
        //    }
        //    DBCallCommon.ExecuteTrans(list_sql);


        //    //更新考勤表
        //    List<string> list_sqlupdate=new List<string>();
        //    string getstaffinfo = "select * from OM_KQTJ where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "'";
        //    DataTable dtgetstaffinfo = DBCallCommon.GetDTUsingSqlText(getstaffinfo);
        //    if (dtgetstaffinfo.Rows.Count > 0)
        //    {
        //        string lastdate = "";
        //        //获取上一周期年月
        //        if (ddlMonth.SelectedValue.ToString().Trim() == "01")
        //        {
        //            string lastyear = (CommonFun.ComTryInt(ddlYear.SelectedValue.ToString().Trim())-1).ToString().Trim();
        //            lastdate = lastyear + "-12";
        //        }
        //        else
        //        {
        //            string lastmonth = (CommonFun.ComTryInt(ddlMonth.SelectedValue.ToString().Trim()) - 1).ToString("00").Trim();
        //            lastdate = ddlYear.SelectedValue.ToString().Trim() + "-" + lastmonth;
        //        }

        //        for (int i = 0; i < dtgetstaffinfo.Rows.Count; i++)
        //        {
        //            string sqlgetqjinfo = "select * from OM_QJSQSP where QJ_SQRID='" + dtgetstaffinfo.Rows[i]["KQ_ST_ID"].ToString().Trim() + "' and QJ_QJQSSJ<='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "-20" + "' and QJ_QJQSSJ>'" + lastdate + "'";
        //            DataTable dtgetqjinfo = DBCallCommon.GetDTUsingSqlText(sqlgetqjinfo);
        //            if (dtgetqjinfo.Rows.Count > 0)
        //            {
        //                for (int j = 0; j < dtgetqjinfo.Rows.Count; j++)
        //                {
        //                    if (dtgetqjinfo.Rows[j]["QJ_TYPE"].ToString().Trim() == "0")
        //                    {
        //                        string sqlupdatets = "update OM_KQTJ set KQ_BINGJ=KQ_BINGJ+" + CommonFun.ComTryDecimal(dtgetqjinfo.Rows[j]["QJ_QJTIANSHU"].ToString().Trim()) + " where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and KQ_ST_ID='" + dtgetstaffinfo.Rows[i]["KQ_ST_ID"].ToString().Trim() + "'";
        //                        list_sqlupdate.Add(sqlupdatets);
        //                    }
        //                    else if (dtgetqjinfo.Rows[j]["QJ_TYPE"].ToString().Trim() == "1")
        //                    {
        //                        string sqlupdatets = "update OM_KQTJ set KQ_SHIJ=KQ_SHIJ+" + CommonFun.ComTryDecimal(dtgetqjinfo.Rows[j]["QJ_QJTIANSHU"].ToString().Trim()) + " where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and KQ_ST_ID='" + dtgetstaffinfo.Rows[i]["KQ_ST_ID"].ToString().Trim() + "'";
        //                        list_sqlupdate.Add(sqlupdatets);
        //                    }
        //                    else if (dtgetqjinfo.Rows[j]["QJ_TYPE"].ToString().Trim() == "2")
        //                    {
        //                        string sqlupdatets = "update OM_KQTJ set KQ_CHANJIA=KQ_CHANJIA+" + CommonFun.ComTryDecimal(dtgetqjinfo.Rows[j]["QJ_QJTIANSHU"].ToString().Trim()) + " where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and KQ_ST_ID='" + dtgetstaffinfo.Rows[i]["KQ_ST_ID"].ToString().Trim() + "'";
        //                        list_sqlupdate.Add(sqlupdatets);
        //                    }
        //                    else if (dtgetqjinfo.Rows[j]["QJ_TYPE"].ToString().Trim() == "3")
        //                    {
        //                        string sqlupdatets = "update OM_KQTJ set KQ_PEICHAN=KQ_PEICHAN+" + CommonFun.ComTryDecimal(dtgetqjinfo.Rows[j]["QJ_QJTIANSHU"].ToString().Trim()) + " where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and KQ_ST_ID='" + dtgetstaffinfo.Rows[i]["KQ_ST_ID"].ToString().Trim() + "'";
        //                        list_sqlupdate.Add(sqlupdatets);
        //                    }
        //                    else if (dtgetqjinfo.Rows[j]["QJ_TYPE"].ToString().Trim() == "4")
        //                    {
        //                        string sqlupdatets = "update OM_KQTJ set KQ_HUNJIA=KQ_HUNJIA+" + CommonFun.ComTryDecimal(dtgetqjinfo.Rows[j]["QJ_QJTIANSHU"].ToString().Trim()) + " where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and KQ_ST_ID='" + dtgetstaffinfo.Rows[i]["KQ_ST_ID"].ToString().Trim() + "'";
        //                        list_sqlupdate.Add(sqlupdatets);
        //                    }
        //                    else if (dtgetqjinfo.Rows[j]["QJ_TYPE"].ToString().Trim() == "5")
        //                    {
        //                        string sqlupdatets = "update OM_KQTJ set KQ_SANGJIA=KQ_SANGJIA+" + CommonFun.ComTryDecimal(dtgetqjinfo.Rows[j]["QJ_QJTIANSHU"].ToString().Trim()) + " where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and KQ_ST_ID='" + dtgetstaffinfo.Rows[i]["KQ_ST_ID"].ToString().Trim() + "'";
        //                        list_sqlupdate.Add(sqlupdatets);
        //                    }
        //                    else if (dtgetqjinfo.Rows[j]["QJ_TYPE"].ToString().Trim() == "6")
        //                    {
        //                        string sqlupdatets = "update OM_KQTJ set KQ_NIANX=KQ_NIANX+" + CommonFun.ComTryDecimal(dtgetqjinfo.Rows[j]["QJ_QJTIANSHU"].ToString().Trim()) + " where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and KQ_ST_ID='" + dtgetstaffinfo.Rows[i]["KQ_ST_ID"].ToString().Trim() + "'";
        //                        list_sqlupdate.Add(sqlupdatets);
        //                    }
        //                    else if (dtgetqjinfo.Rows[j]["QJ_TYPE"].ToString().Trim() == "8")
        //                    {
        //                        string sqlupdatets = "update OM_KQTJ set KQ_JIEDIAO=KQ_JIEDIAO+" + CommonFun.ComTryDecimal(dtgetqjinfo.Rows[j]["QJ_QJTIANSHU"].ToString().Trim()) + " where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and KQ_ST_ID='" + dtgetstaffinfo.Rows[i]["KQ_ST_ID"].ToString().Trim() + "'";
        //                        list_sqlupdate.Add(sqlupdatets);
        //                    }
        //                    else if (dtgetqjinfo.Rows[j]["QJ_TYPE"].ToString().Trim() == "9")
        //                    {
        //                        string sqlupdatets = "update OM_KQTJ set KQ_ZMJBAN=KQ_ZMJBAN+" + CommonFun.ComTryDecimal(dtgetqjinfo.Rows[j]["QJ_QJTIANSHU"].ToString().Trim()) + " where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and KQ_ST_ID='" + dtgetstaffinfo.Rows[i]["KQ_ST_ID"].ToString().Trim() + "'";
        //                        list_sqlupdate.Add(sqlupdatets);
        //                    }
        //                    else if (dtgetqjinfo.Rows[j]["QJ_TYPE"].ToString().Trim() == "10")
        //                    {
        //                        string sqlupdatets = "update OM_KQTJ set KQ_JRJIAB=KQ_JRJIAB+" + CommonFun.ComTryDecimal(dtgetqjinfo.Rows[j]["QJ_QJTIANSHU"].ToString().Trim()) + " where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and KQ_ST_ID='" + dtgetstaffinfo.Rows[i]["KQ_ST_ID"].ToString().Trim() + "'";
        //                        list_sqlupdate.Add(sqlupdatets);
        //                    }
        //                    else if (dtgetqjinfo.Rows[j]["QJ_TYPE"].ToString().Trim() == "11")
        //                    {
        //                        string sqlupdatets = "update OM_KQTJ set KQ_ZHIBAN=KQ_ZHIBAN+" + CommonFun.ComTryDecimal(dtgetqjinfo.Rows[j]["QJ_QJTIANSHU"].ToString().Trim()) + " where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and KQ_ST_ID='" + dtgetstaffinfo.Rows[i]["KQ_ST_ID"].ToString().Trim() + "'";
        //                        list_sqlupdate.Add(sqlupdatets);
        //                    }
        //                    else if (dtgetqjinfo.Rows[j]["QJ_TYPE"].ToString().Trim() == "12")
        //                    {
        //                        string sqlupdatets = "update OM_KQTJ set KQ_YEBAN=KQ_YEBAN+" + CommonFun.ComTryDecimal(dtgetqjinfo.Rows[j]["QJ_QJTIANSHU"].ToString().Trim()) + " where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and KQ_ST_ID='" + dtgetstaffinfo.Rows[i]["KQ_ST_ID"].ToString().Trim() + "'";
        //                        list_sqlupdate.Add(sqlupdatets);
        //                    }
        //                    else if (dtgetqjinfo.Rows[j]["QJ_TYPE"].ToString().Trim() == "13")
        //                    {
        //                        string sqlupdatets = "update OM_KQTJ set KQ_ZHONGB=KQ_ZHONGB+" + CommonFun.ComTryDecimal(dtgetqjinfo.Rows[j]["QJ_QJTIANSHU"].ToString().Trim()) + " where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and KQ_ST_ID='" + dtgetstaffinfo.Rows[i]["KQ_ST_ID"].ToString().Trim() + "'";
        //                        list_sqlupdate.Add(sqlupdatets);
        //                    }
        //                    else if (dtgetqjinfo.Rows[j]["QJ_TYPE"].ToString().Trim() == "14")
        //                    {
        //                        string sqlupdatets = "update OM_KQTJ set KQ_YSGZ=KQ_YSGZ+" + CommonFun.ComTryDecimal(dtgetqjinfo.Rows[j]["QJ_QJTIANSHU"].ToString().Trim()) + " where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and KQ_ST_ID='" + dtgetstaffinfo.Rows[i]["KQ_ST_ID"].ToString().Trim() + "'";
        //                        list_sqlupdate.Add(sqlupdatets);
        //                    }
        //                    else
        //                    {
        //                        string sqlupdatets = "update OM_KQTJ set KQ_QTJIA=KQ_QTJIA+" + CommonFun.ComTryDecimal(dtgetqjinfo.Rows[j]["QJ_QJTIANSHU"].ToString().Trim()) + " where KQ_DATE='" + ddlYear.SelectedValue.ToString().Trim() + "-" + ddlMonth.SelectedValue.ToString().Trim() + "' and KQ_ST_ID='" + dtgetstaffinfo.Rows[i]["KQ_ST_ID"].ToString().Trim() + "'";
        //                        list_sqlupdate.Add(sqlupdatets);
        //                    }
        //                }
        //            }

        //        }
        //        DBCallCommon.ExecuteTrans(list_sqlupdate);
        //    }
        //    ModalPopupExtenderSearch.Hide();
        //    UCPaging1.CurrentPage = 1;
        //    bindrpt();
        //}
        #endregion


        //取消
        protected void btnClose_Click(object sender, EventArgs e)
        {
            QueryButton.Text = "确认";
            message.Visible = false;
            message.Text = "";
            ModalPopupExtenderSearch.Hide();
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
        /// <summary>
        /// 年份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlYear_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }
        /// <summary>
        /// 月份查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMonth_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindrpt();
        }

        protected void rptKQTJ_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                if (cb_cc.Checked == true)
                {
                    HtmlTableCell cc = e.Item.FindControl("tdCCTG") as HtmlTableCell;
                    HtmlTableCell ccgn1 = e.Item.FindControl("tdGuonei") as HtmlTableCell;
                    HtmlTableCell ccgn2 = e.Item.FindControl("tdGuowai") as HtmlTableCell;

                    cc.Visible = false;
                    ccgn1.Visible = false;
                    ccgn2.Visible = false;

                }

                if (cb_qj.Checked == true)
                {
                    HtmlTableCell tdGLQJTJ = e.Item.FindControl("tdGLQJTJ") as HtmlTableCell;
                    HtmlTableCell tdBingjia = e.Item.FindControl("tdBingjia") as HtmlTableCell;
                    HtmlTableCell tdShijia = e.Item.FindControl("tdShijia") as HtmlTableCell;
                    HtmlTableCell tdKuanggong = e.Item.FindControl("tdKuanggong") as HtmlTableCell;
                    HtmlTableCell tdDaoxiu = e.Item.FindControl("tdDaoxiu") as HtmlTableCell;
                    HtmlTableCell tdChanjia = e.Item.FindControl("tdChanjia") as HtmlTableCell;
                    HtmlTableCell tdPeichanjia = e.Item.FindControl("tdPeichanjia") as HtmlTableCell;
                    HtmlTableCell tdHunjia = e.Item.FindControl("tdHunjia") as HtmlTableCell;
                    HtmlTableCell tdSangjia = e.Item.FindControl("tdSangjia") as HtmlTableCell;
                    HtmlTableCell tdGongshang = e.Item.FindControl("tdGongshang") as HtmlTableCell;
                    HtmlTableCell tdNianxiu = e.Item.FindControl("tdNianxiu") as HtmlTableCell;
                    HtmlTableCell tdBeiyong1 = e.Item.FindControl("tdBeiyong1") as HtmlTableCell;
                    HtmlTableCell tdBeiyong2 = e.Item.FindControl("tdBeiyong2") as HtmlTableCell;
                    HtmlTableCell tdBeiyong3 = e.Item.FindControl("tdBeiyong3") as HtmlTableCell;
                    HtmlTableCell tdBeiyong4 = e.Item.FindControl("tdBeiyong4") as HtmlTableCell;
                    HtmlTableCell tdBeiyong5 = e.Item.FindControl("tdBeiyong5") as HtmlTableCell;
                    HtmlTableCell tdBeiyong6 = e.Item.FindControl("tdBeiyong6") as HtmlTableCell;
                    HtmlTableCell tdQita = e.Item.FindControl("tdQita") as HtmlTableCell;

                    tdGLQJTJ.Visible = false;
                    tdBingjia.Visible = false;
                    tdShijia.Visible = false;
                    tdKuanggong.Visible = false;
                    tdDaoxiu.Visible = false;
                    tdChanjia.Visible = false;
                    tdPeichanjia.Visible = false;
                    tdHunjia.Visible = false;
                    tdSangjia.Visible = false;
                    tdGongshang.Visible = false;
                    tdNianxiu.Visible = false;
                    tdBeiyong1.Visible = false;
                    tdBeiyong2.Visible = false;
                    tdBeiyong3.Visible = false;
                    tdBeiyong4.Visible = false;
                    tdBeiyong5.Visible = false;
                    tdBeiyong6.Visible = false;
                    tdQita.Visible = false;
                }

            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                int num = 0;
                for (int i = 0; i < CheckBoxListhj.Items.Count; i++)
                {
                    if (CheckBoxListhj.Items[i].Selected == true)
                    {
                        num++;
                    }
                }
                if (num > 0)
                {
                    string kqstid = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_ST_ID")).Text.ToString().Trim();
                    string yearmonth = ((System.Web.UI.WebControls.Label)e.Item.FindControl("lb_yf")).Text.ToString().Trim();
                    string sqlsxhj = "select (0";
                    for (int i = 0; i < CheckBoxListhj.Items.Count; i++)
                    {
                        if (CheckBoxListhj.Items[i].Selected == true)
                        {
                            sqlsxhj += "+" + CheckBoxListhj.Items[i].Value.ToString().Trim() + "";
                        }
                    }
                    sqlsxhj += ") as searchhj from View_OM_KQTJ where KQ_ST_ID='" + kqstid + "' and KQ_DATE='" + yearmonth + "'";
                    System.Data.DataTable dtsxhj = DBCallCommon.GetDTUsingSqlText(sqlsxhj);
                    if (dtsxhj.Rows.Count > 0)
                    {
                        System.Web.UI.WebControls.Label lbsearchhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbsearchhj");
                        lbsearchhj.Text = dtsxhj.Rows[0]["searchhj"].ToString().Trim();
                    }
                }
                if (cb_cc.Checked == true)
                {
                    HtmlTableCell ccgn = e.Item.FindControl("tdGuonei1") as HtmlTableCell;
                    HtmlTableCell ccgw = e.Item.FindControl("tdGuowai1") as HtmlTableCell;

                    ccgn.Visible = false;
                    ccgw.Visible = false;


                }
                if (cb_qj.Checked == true)
                {
                    HtmlTableCell tdBingjia1 = e.Item.FindControl("tdBingjia1") as HtmlTableCell;
                    HtmlTableCell tdShijia1 = e.Item.FindControl("tdShijia1") as HtmlTableCell;
                    HtmlTableCell tdKuanggong1 = e.Item.FindControl("tdKuanggong1") as HtmlTableCell;
                    HtmlTableCell tdDaoxiu1 = e.Item.FindControl("tdDaoxiu1") as HtmlTableCell;
                    HtmlTableCell tdChanjia1 = e.Item.FindControl("tdChanjia1") as HtmlTableCell;
                    HtmlTableCell tdPeichanjia1 = e.Item.FindControl("tdPeichanjia1") as HtmlTableCell;
                    HtmlTableCell tdHunjia1 = e.Item.FindControl("tdHunjia1") as HtmlTableCell;
                    HtmlTableCell tdSangjia1 = e.Item.FindControl("tdSangjia1") as HtmlTableCell;
                    HtmlTableCell tdGongshang1 = e.Item.FindControl("tdGongshang1") as HtmlTableCell;
                    HtmlTableCell tdNianxiu1 = e.Item.FindControl("tdNianxiu1") as HtmlTableCell;
                    HtmlTableCell tdBeiyong11 = e.Item.FindControl("tdBeiyong11") as HtmlTableCell;
                    HtmlTableCell tdBeiyong21 = e.Item.FindControl("tdBeiyong21") as HtmlTableCell;
                    HtmlTableCell tdBeiyong31 = e.Item.FindControl("tdBeiyong31") as HtmlTableCell;
                    HtmlTableCell tdBeiyong41 = e.Item.FindControl("tdBeiyong41") as HtmlTableCell;
                    HtmlTableCell tdBeiyong51 = e.Item.FindControl("tdBeiyong51") as HtmlTableCell;
                    HtmlTableCell tdBeiyong61 = e.Item.FindControl("tdBeiyong61") as HtmlTableCell;
                    HtmlTableCell tdQita1 = e.Item.FindControl("tdQita1") as HtmlTableCell;



                    tdBingjia1.Visible = false;
                    tdShijia1.Visible = false;
                    tdKuanggong1.Visible = false;
                    tdDaoxiu1.Visible = false;
                    tdChanjia1.Visible = false;
                    tdPeichanjia1.Visible = false;
                    tdHunjia1.Visible = false;
                    tdSangjia1.Visible = false;
                    tdGongshang1.Visible = false;
                    tdNianxiu1.Visible = false;
                    tdBeiyong11.Visible = false;
                    tdBeiyong21.Visible = false;
                    tdBeiyong31.Visible = false;
                    tdBeiyong41.Visible = false;
                    tdBeiyong51.Visible = false;
                    tdBeiyong61.Visible = false;
                    tdQita1.Visible = false;


                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                string sqlhj = "select sum(KQ_GNCC) as KQ_GNCC,sum(KQ_GWCC) as KQ_GWCC,sum(KQ_BINGJ) as KQ_BINGJ,sum(KQ_SHIJ) as KQ_SHIJ,sum(KQ_KUANGG) as KQ_KUANGG,sum(KQ_DAOXIU) as KQ_DAOXIU,sum(KQ_CHANJIA) as KQ_CHANJIA,sum(KQ_PEICHAN) as KQ_PEICHAN,sum(KQ_HUNJIA) as KQ_HUNJIA,sum(KQ_SANGJIA) as KQ_SANGJIA,sum(KQ_GONGS) as KQ_GONGS,sum(KQ_NIANX) as KQ_NIANX,sum(KQ_BEIYONG1) as KQ_BEIYONG1,sum(KQ_BEIYONG2) as KQ_BEIYONG2,sum(KQ_BEIYONG3) as KQ_BEIYONG3,sum(KQ_BEIYONG4) as KQ_BEIYONG4,sum(KQ_BEIYONG5) as KQ_BEIYONG5,sum(KQ_BEIYONG6) as KQ_BEIYONG6,sum(KQ_QTJIA) as KQ_QTJIA,sum(KQ_JIEDIAO) as KQ_JIEDIAO,sum(KQ_ZMJBAN) as KQ_ZMJBAN,sum(KQ_JRJIAB) as KQ_JRJIAB,sum(KQ_ZHIBAN) as KQ_ZHIBAN,sum(KQ_YEBAN) as KQ_YEBAN,sum(KQ_ZHONGB) as KQ_ZHONGB,sum(KQ_CBTS) as KQ_CBTS,sum(KQ_YSGZ) as KQ_YSGZ,sum(KQ_CHUQIN) as KQ_CHUQIN from View_OM_KQTJ where " + StrWhere() + "";
                System.Data.DataTable dthj = DBCallCommon.GetDTUsingSqlText(sqlhj);
                if (dthj.Rows.Count > 0)
                {
                    System.Web.UI.WebControls.Label lbKQ_CHUQINhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_CHUQINhj");
                    System.Web.UI.WebControls.Label lbKQ_GNCChj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_GNCChj");
                    System.Web.UI.WebControls.Label lbKQ_GWCChj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_GWCChj");
                    System.Web.UI.WebControls.Label lbKQ_BINGJhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BINGJhj");
                    System.Web.UI.WebControls.Label lbKQ_SHIJhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_SHIJhj");
                    System.Web.UI.WebControls.Label lbKQ_KUANGGhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_KUANGGhj");
                    System.Web.UI.WebControls.Label lbKQ_DAOXIUhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_DAOXIUhj");
                    System.Web.UI.WebControls.Label lbKQ_CHANJIAhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_CHANJIAhj");
                    System.Web.UI.WebControls.Label lbKQ_PEICHANhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_PEICHANhj");
                    System.Web.UI.WebControls.Label lbKQ_HUNJIAhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_HUNJIAhj");
                    System.Web.UI.WebControls.Label lbKQ_SANGJIAhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_SANGJIAhj");
                    System.Web.UI.WebControls.Label lbKQ_GONGShj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_GONGShj");
                    System.Web.UI.WebControls.Label lbKQ_NIANXhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_NIANXhj");
                    System.Web.UI.WebControls.Label lbKQ_BEIYONG1hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG1hj");
                    System.Web.UI.WebControls.Label lbKQ_BEIYONG2hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG2hj");
                    System.Web.UI.WebControls.Label lbKQ_BEIYONG3hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG3hj");

                    System.Web.UI.WebControls.Label lbKQ_BEIYONG4hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG4hj");
                    System.Web.UI.WebControls.Label lbKQ_BEIYONG5hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG5hj");
                    System.Web.UI.WebControls.Label lbKQ_BEIYONG6hj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_BEIYONG6hj");

                    System.Web.UI.WebControls.Label lbKQ_QTJIAhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_QTJIAhj");
                    System.Web.UI.WebControls.Label lbKQ_JIEDIAOhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_JIEDIAOhj");
                    System.Web.UI.WebControls.Label lbKQ_ZMJBANhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_ZMJBANhj");
                    System.Web.UI.WebControls.Label lbKQ_JRJIABhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_JRJIABhj");
                    System.Web.UI.WebControls.Label lbKQ_ZHIBANhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_ZHIBANhj");
                    System.Web.UI.WebControls.Label lbKQ_YEBANhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_YEBANhj");
                    System.Web.UI.WebControls.Label lbKQ_ZHONGBhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_ZHONGBhj");
                    System.Web.UI.WebControls.Label lbKQ_CBTShj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_CBTShj");
                    System.Web.UI.WebControls.Label lbKQ_YSGZhj = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbKQ_YSGZhj");

                    lbKQ_CHUQINhj.Text = dthj.Rows[0]["KQ_CHUQIN"].ToString().Trim();
                    lbKQ_GNCChj.Text = dthj.Rows[0]["KQ_GNCC"].ToString().Trim();
                    lbKQ_GWCChj.Text = dthj.Rows[0]["KQ_GWCC"].ToString().Trim();
                    lbKQ_BINGJhj.Text = dthj.Rows[0]["KQ_BINGJ"].ToString().Trim();
                    lbKQ_SHIJhj.Text = dthj.Rows[0]["KQ_SHIJ"].ToString().Trim();
                    lbKQ_KUANGGhj.Text = dthj.Rows[0]["KQ_KUANGG"].ToString().Trim();
                    lbKQ_DAOXIUhj.Text = dthj.Rows[0]["KQ_DAOXIU"].ToString().Trim();
                    lbKQ_CHANJIAhj.Text = dthj.Rows[0]["KQ_CHANJIA"].ToString().Trim();
                    lbKQ_PEICHANhj.Text = dthj.Rows[0]["KQ_PEICHAN"].ToString().Trim();
                    lbKQ_HUNJIAhj.Text = dthj.Rows[0]["KQ_HUNJIA"].ToString().Trim();
                    lbKQ_SANGJIAhj.Text = dthj.Rows[0]["KQ_SANGJIA"].ToString().Trim();
                    lbKQ_GONGShj.Text = dthj.Rows[0]["KQ_GONGS"].ToString().Trim();
                    lbKQ_NIANXhj.Text = dthj.Rows[0]["KQ_NIANX"].ToString().Trim();
                    lbKQ_BEIYONG1hj.Text = dthj.Rows[0]["KQ_BEIYONG1"].ToString().Trim();
                    lbKQ_BEIYONG2hj.Text = dthj.Rows[0]["KQ_BEIYONG2"].ToString().Trim();
                    lbKQ_BEIYONG3hj.Text = dthj.Rows[0]["KQ_BEIYONG3"].ToString().Trim();

                    lbKQ_BEIYONG4hj.Text = dthj.Rows[0]["KQ_BEIYONG4"].ToString().Trim();
                    lbKQ_BEIYONG5hj.Text = dthj.Rows[0]["KQ_BEIYONG5"].ToString().Trim();
                    lbKQ_BEIYONG6hj.Text = dthj.Rows[0]["KQ_BEIYONG6"].ToString().Trim();

                    lbKQ_QTJIAhj.Text = dthj.Rows[0]["KQ_QTJIA"].ToString().Trim();
                    lbKQ_JIEDIAOhj.Text = dthj.Rows[0]["KQ_JIEDIAO"].ToString().Trim();
                    lbKQ_ZMJBANhj.Text = dthj.Rows[0]["KQ_ZMJBAN"].ToString().Trim();
                    lbKQ_JRJIABhj.Text = dthj.Rows[0]["KQ_JRJIAB"].ToString().Trim();
                    lbKQ_ZHIBANhj.Text = dthj.Rows[0]["KQ_ZHIBAN"].ToString().Trim();
                    lbKQ_YEBANhj.Text = dthj.Rows[0]["KQ_YEBAN"].ToString().Trim();
                    lbKQ_ZHONGBhj.Text = dthj.Rows[0]["KQ_ZHONGB"].ToString().Trim();
                    lbKQ_CBTShj.Text = dthj.Rows[0]["KQ_CBTS"].ToString().Trim();
                    lbKQ_YSGZhj.Text = dthj.Rows[0]["KQ_YSGZ"].ToString().Trim();
                }
                if (cb_cc.Checked == true)
                {
                    HtmlTableCell ccgnhj = e.Item.FindControl("tdKQ_GNCChj") as HtmlTableCell;
                    HtmlTableCell ccgwhj = e.Item.FindControl("tdKQ_GWCChj") as HtmlTableCell;

                    ccgnhj.Visible = false;
                    ccgwhj.Visible = false;
                }
                if (cb_qj.Checked == true)
                {
                    HtmlTableCell tdKQ_BINGJhj = e.Item.FindControl("tdKQ_BINGJhj") as HtmlTableCell;
                    HtmlTableCell tdKQ_SHIJhj = e.Item.FindControl("tdKQ_SHIJhj") as HtmlTableCell;
                    HtmlTableCell tdKQ_KUANGGhj = e.Item.FindControl("tdKQ_KUANGGhj") as HtmlTableCell;
                    HtmlTableCell tdKQ_DAOXIUhj = e.Item.FindControl("tdKQ_DAOXIUhj") as HtmlTableCell;
                    HtmlTableCell tdKQ_CHANJIAhj = e.Item.FindControl("tdKQ_CHANJIAhj") as HtmlTableCell;
                    HtmlTableCell tdKQ_PEICHANhj = e.Item.FindControl("tdKQ_PEICHANhj") as HtmlTableCell;
                    HtmlTableCell tdKQ_HUNJIAhj = e.Item.FindControl("tdKQ_HUNJIAhj") as HtmlTableCell;
                    HtmlTableCell tdKQ_SANGJIAhj = e.Item.FindControl("tdKQ_SANGJIAhj") as HtmlTableCell;
                    HtmlTableCell tdKQ_GONGShj = e.Item.FindControl("tdKQ_GONGShj") as HtmlTableCell;
                    HtmlTableCell tdKQ_NIANXhj = e.Item.FindControl("tdKQ_NIANXhj") as HtmlTableCell;
                    HtmlTableCell tdKQ_BEIYONG1hj = e.Item.FindControl("tdKQ_BEIYONG1hj") as HtmlTableCell;
                    HtmlTableCell tdKQ_BEIYONG2hj = e.Item.FindControl("tdKQ_BEIYONG2hj") as HtmlTableCell;
                    HtmlTableCell tdKQ_BEIYONG3hj = e.Item.FindControl("tdKQ_BEIYONG3hj") as HtmlTableCell;
                    HtmlTableCell tdBeiyong41 = e.Item.FindControl("tdBeiyong41") as HtmlTableCell;
                    HtmlTableCell tdBeiyong51 = e.Item.FindControl("tdBeiyong51") as HtmlTableCell;
                    HtmlTableCell tdBeiyong61 = e.Item.FindControl("tdBeiyong61") as HtmlTableCell;
                    HtmlTableCell tdKQ_QTJIAhj = e.Item.FindControl("tdKQ_QTJIAhj") as HtmlTableCell;



                    tdKQ_BINGJhj.Visible = false;
                    tdKQ_SHIJhj.Visible = false;
                    tdKQ_KUANGGhj.Visible = false;
                    tdKQ_DAOXIUhj.Visible = false;
                    tdKQ_CHANJIAhj.Visible = false;
                    tdKQ_PEICHANhj.Visible = false;
                    tdKQ_HUNJIAhj.Visible = false;
                    tdKQ_SANGJIAhj.Visible = false;
                    tdKQ_GONGShj.Visible = false;
                    tdKQ_NIANXhj.Visible = false;
                    tdKQ_BEIYONG1hj.Visible = false;
                    tdKQ_BEIYONG2hj.Visible = false;
                    tdKQ_BEIYONG3hj.Visible = false;
                    tdBeiyong41.Visible = false;
                    tdBeiyong51.Visible = false;
                    tdBeiyong61.Visible = false;
                    tdKQ_QTJIAhj.Visible = false;
                }
            }

        }

        protected void cb_cc_CheckedChanged(object sender, EventArgs e)
        {
            bindrpt();
        }

        protected void cb_qj_CheckedChanged(object sender, EventArgs e)
        {
            bindrpt();
        }


        //因为达到第五条规定条件而调整年假的调整天数
        private void nianjiatz()
        {
            if (tb_yearmonth.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写截止月份！！！');", true);
                return;
            }

            if (CommonFun.ComTryInt(tb_yearmonth.Text.Trim().Substring(0, 4)) > CommonFun.ComTryInt(DateTime.Now.ToString("yyyy").Trim()))
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('所选年份不能超过当前年份！！！');", true);
                return;
            }
            string sqltexttz = "";

            string sqldetail = "";
            List<string> listsql = new List<string>();
            double yearnowtzdays = 0;//当年可休年假
            double yearnexttzdays = 0;//下一年可休年假

            //获取下一年份
            int nowyear = CommonFun.ComTryInt(tb_yearmonth.Text.Trim().Substring(0, 4));
            int nextyear = nowyear + 1;

            double shijiadays = 0;
            double bingjiadays = 0;
            double kuanggongdays = 0;
            double yishiyong = 0;

            //更新上一年需要在今年扣除的数据
            sqltexttz = "update OM_NianJiaTJ set NJ_QINGL=NJ_QINGL+NJ_TZDAYS,NJ_IFTZ='1',NJ_TZYEAR=NULL,NJ_TYPE='扣除上一年需要在当前年份扣除的年假' where NJ_IFTZ is null and NJ_TZYEAR='" + nowyear + "'";
            DBCallCommon.ExeSqlText(sqltexttz);

            sqltexttz = "insert into OM_NianJiaTJtzjl(TZJL_ST_ID,TZJL_NAME,TZJL_WORKNUMBER,TZJL_BUMEN,TZJL_BUMENID,TZJL_TZTS,TZJL_TZR,TZJL_TZTIME,TZJL_TZREASON) select NJ_ST_ID,NJ_NAME,NJ_WORKNUMBER,NJ_BUMEN,NJ_BUMENID,NJ_TZDAYS,'" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',NJ_TYPE from OM_NianJiaTJ where NJ_IFTZ is null and NJ_TZYEAR='" + nowyear + "'";
            DBCallCommon.ExeSqlText(sqltexttz);
            //获取要循环的数据
            sqltexttz = "select * from (select * from OM_NianJiaTJ left join TBDS_STAFFINFO on OM_NianJiaTJ.NJ_ST_ID=TBDS_STAFFINFO.ST_ID)t where (ST_PD='0' or ST_PD='1' or ST_PD='4') and ((NJ_TZYEAR is null and NJ_IFTZ is null) or (NJ_IFTZ='1' and NJ_TZYEAR!='" + tb_yearmonth.Text.Trim().Substring(0, 4) + "' and NJ_TZYEAR is not null) or (NJ_IFTZ='1' and NJ_TZYEAR is null))";
            System.Data.DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sqltexttz);
            if (dt0.Rows.Count > 0)
            {
                //循环数据
                for (int i = 0; i < dt0.Rows.Count; i++)
                {
                    sqldetail = "select sum(KQ_BINGJ) as bingjiadays,sum(KQ_SHIJ) as shijiadays,sum(KQ_NIANX) as yishiyong,sum(KQ_KUANGG) as kuanggongdays from OM_KQTJ where KQ_DATE like '" + tb_yearmonth.Text.Trim().Substring(0, 5) + "%' and KQ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                    System.Data.DataTable dtdetail = DBCallCommon.GetDTUsingSqlText(sqldetail);
                    if (dtdetail.Rows.Count > 0)
                    {
                        shijiadays = CommonFun.ComTryDouble(dtdetail.Rows[0]["shijiadays"].ToString().Trim());
                        bingjiadays = CommonFun.ComTryDouble(dtdetail.Rows[0]["bingjiadays"].ToString().Trim());
                        kuanggongdays = CommonFun.ComTryDouble(dtdetail.Rows[0]["kuanggongdays"].ToString().Trim());
                        yishiyong = CommonFun.ComTryDouble(dtdetail.Rows[0]["yishiyong"].ToString().Trim());
                        //获取工龄
                        DateTime datemin;
                        DateTime datemax;
                        try
                        {
                            datemin = DateTime.Parse(dt0.Rows[i]["NJ_RUZHITIME"].ToString().Trim());
                            if (dt0.Rows[i]["NJ_LIZHITIME"].ToString().Trim() != "")
                            {
                                datemax = DateTime.Parse(dt0.Rows[i]["NJ_LIZHITIME"].ToString().Trim());
                            }
                            else
                            {
                                datemax = DateTime.Parse(tb_yearmonth.Text.Trim().Substring(0, 4) + ".12.30");
                            }
                        }
                        catch
                        {
                            datemin = DateTime.Parse(tb_yearmonth.Text.Trim().Substring(0, 4) + ".12.30");
                            datemax = DateTime.Parse(tb_yearmonth.Text.Trim().Substring(0, 4) + ".12.30");
                        }
                        double monthnum = datemax.Month - datemin.Month;
                        double yearnum = datemax.Year - datemin.Year;
                        double totalmonthlast = yearnum * 12 + monthnum - 12;
                        double totalmonthnum = yearnum * 12 + monthnum;
                        double totalmonthnext = yearnum * 12 + monthnum + 12;

                        double lastljkx = 0;
                        double nowljkx = 0;
                        double nextljkx = 0;
                        //计算上一年累计可休年假
                        //小于一年
                        if (totalmonthlast < 12)
                        {
                            lastljkx = 0;
                        }
                        //大于一年小于十年
                        else if (totalmonthlast >= 12 && totalmonthlast < 120)
                        {
                            lastljkx = Math.Floor((totalmonthlast - 12) * 5 / 12);
                        }
                        //大于十年小于二十年
                        else if (totalmonthlast >= 120 && totalmonthlast < 240)
                        {
                            lastljkx = Math.Floor((120 - 12) * 5 / 12 + (totalmonthlast - 120) * 10 / 12);
                        }
                        //二十年以上
                        else
                        {
                            lastljkx = Math.Floor((120 - 12) * 5 / 12 + (240 - 120) * 10 / 12 + (totalmonthlast - 240) * 15 / 12);
                        }
                        //计算当年累计可休年假
                        if (totalmonthnum < 12)
                        {
                            nowljkx = 0;
                        }
                        //大于一年小于十年
                        else if (totalmonthnum >= 12 && totalmonthnum < 120)
                        {
                            nowljkx = Math.Floor((totalmonthnum - 12) * 5 / 12);
                        }
                        //大于十年小于二十年
                        else if (totalmonthnum >= 120 && totalmonthnum < 240)
                        {
                            nowljkx = Math.Floor((120 - 12) * 5 / 12 + (totalmonthnum - 120) * 10 / 12);
                        }
                        //二十年以上
                        else
                        {
                            nowljkx = Math.Floor((120 - 12) * 5 / 12 + (240 - 120) * 10 / 12 + (totalmonthnum - 240) * 15 / 12);
                        }
                        //计算下一年累计可休年假
                        if (totalmonthnext < 12)
                        {
                            nextljkx = 0;
                        }
                        //大于一年小于十年
                        else if (totalmonthnext >= 12 && totalmonthnext < 120)
                        {
                            nextljkx = Math.Floor((totalmonthnext - 12) * 5 / 12);
                        }
                        //大于十年小于二十年
                        else if (totalmonthnext >= 120 && totalmonthnext < 240)
                        {
                            nextljkx = Math.Floor((120 - 12) * 5 / 12 + (totalmonthnext - 120) * 10 / 12);
                        }
                        //二十年以上
                        else
                        {
                            nextljkx = Math.Floor((120 - 12) * 5 / 12 + (240 - 120) * 10 / 12 + (totalmonthnext - 240) * 15 / 12);
                        }

                        //得到当年可休年假和下一年可休年假
                        yearnowtzdays = nowljkx - lastljkx;
                        yearnexttzdays = nextljkx - nowljkx;

                        //事假超过20天
                        if (shijiadays >= 20)
                        {
                            if (yishiyong >= yearnowtzdays)
                            {
                                sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ=NULL,NJ_TZYEAR='" + nextyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnexttzdays + ",NJ_TYPE='事假累计20天,且已享受当年年假，下一年度不再享受带薪年休假" + yearnexttzdays.ToString().Trim() + "天，将在下一年度扣除；' where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                listsql.Add(sqltexttz);
                            }
                            else
                            {
                                sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ='1',NJ_TZYEAR='" + nowyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnowtzdays + ",NJ_TYPE='事假累计20天,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；',NJ_QINGL=NJ_QINGL+" + yearnowtzdays + " where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                listsql.Add(sqltexttz);

                                listsql.Add("insert into OM_NianJiaTJtzjl(TZJL_ST_ID,TZJL_NAME,TZJL_WORKNUMBER,TZJL_BUMEN,TZJL_BUMENID,TZJL_TZTS,TZJL_TZR,TZJL_TZTIME,TZJL_TZREASON) values('" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_NAME"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_WORKNUMBER"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMEN"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMENID"].ToString().Trim() + "'," + yearnowtzdays + ",'" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','事假累计20天,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；')");
                            }
                        }
                        //旷工超过3天
                        else if (kuanggongdays >= 3)
                        {
                            if (yishiyong >= yearnowtzdays)
                            {
                                sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ=NULL,NJ_TZYEAR='" + nextyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnexttzdays + ",NJ_TYPE='累计旷工3天及以上,且已享受当年年假，下一年度不再享受带薪年休假" + yearnexttzdays.ToString().Trim() + "天，将在下一年度扣除；' where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                listsql.Add(sqltexttz);
                            }
                            else
                            {
                                sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ='1',NJ_TZYEAR='" + nowyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnowtzdays + ",NJ_TYPE='累计旷工3天及以上,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；',NJ_QINGL=NJ_QINGL+" + yearnowtzdays + " where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                listsql.Add(sqltexttz);
                                listsql.Add("insert into OM_NianJiaTJtzjl(TZJL_ST_ID,TZJL_NAME,TZJL_WORKNUMBER,TZJL_BUMEN,TZJL_BUMENID,TZJL_TZTS,TZJL_TZR,TZJL_TZTIME,TZJL_TZREASON) values('" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_NAME"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_WORKNUMBER"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMEN"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMENID"].ToString().Trim() + "'," + yearnowtzdays + ",'" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','累计旷工3天及以上,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；')");
                            }
                        }
                        //病假时间超过上限(每月按20.83天算，最小单位0.5天)
                        else
                        {
                            //小于一年
                            if (totalmonthnum < 12)
                            {

                            }
                            //大于一年小于十年
                            else if (totalmonthnum >= 12 && totalmonthnum < 120)
                            {
                                if (bingjiadays >= 41.5)//两个月
                                {
                                    if (yishiyong >= yearnowtzdays)
                                    {
                                        sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ=NULL,NJ_TZYEAR='" + nextyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnexttzdays + ",NJ_TYPE='病假累计2个月以上,且已享受当年年假，下一年度不再享受带薪年休假" + yearnexttzdays.ToString().Trim() + "天，将在下一年度扣除；' where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                        listsql.Add(sqltexttz);
                                    }
                                    else
                                    {
                                        sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ='1',NJ_TZYEAR='" + nowyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnowtzdays + ",NJ_TYPE='病假累计2个月以上,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；',NJ_QINGL=NJ_QINGL+" + yearnowtzdays + " where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                        listsql.Add(sqltexttz);
                                        listsql.Add("insert into OM_NianJiaTJtzjl(TZJL_ST_ID,TZJL_NAME,TZJL_WORKNUMBER,TZJL_BUMEN,TZJL_BUMENID,TZJL_TZTS,TZJL_TZR,TZJL_TZTIME,TZJL_TZREASON) values('" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_NAME"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_WORKNUMBER"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMEN"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMENID"].ToString().Trim() + "'," + yearnowtzdays + ",'" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','病假累计2个月以上,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；')");
                                    }
                                }
                            }
                            //大于十年小于二十年
                            else if (totalmonthnum >= 120 && totalmonthnum < 240)
                            {
                                if (bingjiadays >= 62)//三个月
                                {
                                    if (yishiyong >= yearnowtzdays)
                                    {
                                        sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ=NULL,NJ_TZYEAR='" + nextyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnexttzdays + ",NJ_TYPE='病假累计3个月以上,且已享受当年年假，下一年度不再享受带薪年休假" + yearnexttzdays.ToString().Trim() + "天，将在下一年度扣除；' where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                        listsql.Add(sqltexttz);
                                    }
                                    else
                                    {
                                        sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ='1',NJ_TZYEAR='" + nowyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnowtzdays + ",NJ_TYPE='病假累计3个月以上,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；',NJ_QINGL=NJ_QINGL+" + yearnowtzdays + " where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                        listsql.Add(sqltexttz);
                                        listsql.Add("insert into OM_NianJiaTJtzjl(TZJL_ST_ID,TZJL_NAME,TZJL_WORKNUMBER,TZJL_BUMEN,TZJL_BUMENID,TZJL_TZTS,TZJL_TZR,TZJL_TZTIME,TZJL_TZREASON) values('" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_NAME"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_WORKNUMBER"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMEN"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMENID"].ToString().Trim() + "'," + yearnowtzdays + ",'" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','病假累计3个月以上,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；')");
                                    }
                                }
                            }
                            //二十年以上
                            else
                            {
                                if (bingjiadays >= 83)//四个月
                                {
                                    if (yishiyong >= yearnowtzdays)
                                    {
                                        sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ=NULL,NJ_TZYEAR='" + nextyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnexttzdays + ",NJ_TYPE='病假累计4个月以上,且已享受当年年假，下一年度不再享受带薪年休假" + yearnexttzdays.ToString().Trim() + "天，将在下一年度扣除；' where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                        listsql.Add(sqltexttz);
                                    }
                                    else
                                    {
                                        sqltexttz = "update OM_NianJiaTJ set NJ_IFTZ='1',NJ_TZYEAR='" + nowyear.ToString().Trim() + "',NJ_TZDAYS=" + yearnowtzdays + ",NJ_TYPE='病假累计4个月以上,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；',NJ_QINGL=NJ_QINGL+" + yearnowtzdays + " where NJ_ST_ID='" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "'";
                                        listsql.Add(sqltexttz);

                                        listsql.Add("insert into OM_NianJiaTJtzjl(TZJL_ST_ID,TZJL_NAME,TZJL_WORKNUMBER,TZJL_BUMEN,TZJL_BUMENID,TZJL_TZTS,TZJL_TZR,TZJL_TZTIME,TZJL_TZREASON) values('" + dt0.Rows[i]["NJ_ST_ID"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_NAME"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_WORKNUMBER"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMEN"].ToString().Trim() + "','" + dt0.Rows[i]["NJ_BUMENID"].ToString().Trim() + "'," + yearnowtzdays + ",'" + Session["UserName"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "','病假累计4个月以上,扣除当年年假" + yearnowtzdays.ToString().Trim() + "天；')");
                                    }
                                }
                            }
                        }
                    }
                }

                DBCallCommon.ExecuteTrans(listsql);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有数据！！！');", true);
                return;
            }
        }
    }
}
